#Region "using"

Imports Dropthings.Widget.Framework
Imports System.Data
Imports System.Xml.Linq
Imports InMotionGIT.FrontOffice.Proxy
Imports InMotionGIT.FrontOffice.Proxy.MenuService
Imports DevExpress.Web.ASPxTreeView
Imports GIT.Core
Imports InMotionGIT.Core.Configuration

#End Region

Namespace Dropthings.Widgets

    Partial Class MenuUserVTControl
        Inherits System.Web.UI.UserControl
        Implements IWidget

#Region "Private fields, to hold the state of the entity"

        Private _Host As IWidgetHost
        Private _State As XElement

        Dim config As VisualTIME = TryCast(ConfigurationManager.GetSection("VisualTIMEConfigurationGroup/VisualTIMESection"), VisualTIME)

        Private _backOfficePath As String = String.Empty
        Private _menuImagesExtension As String = String.Empty
        Private _menuImagesExtensionModule As String = String.Empty
        Private _defaultVersionName As String = String.Empty
        Private _backOfficeMenuPath As Boolean

#End Region

#Region "Public properties"

        Private ReadOnly Property State() As XElement
            Get
                If (IsNothing(_State)) Then
                    Try
                        _State = XElement.Parse(_Host.GetState())
                    Catch ex As Exception
                        InMotionGIT.Common.Helpers.LogHandler.ErrorLog("MenuUserVTControl => State", ex.Message, ex)
                        Throw New Exception("Ocurrió un error casteando: " + _Host.GetState())
                    End Try
                End If
                Return _State
            End Get

        End Property

        Private Property Modules() As String
            Get
                Return State.Element("Modules").Value
            End Get
            Set(ByVal value As String)
                State.Element("Modules").Value = value
            End Set
        End Property

#End Region

#Region "IWidget Members"

        Public Sub Init1(ByVal host As IWidgetHost) Implements IWidget.Init
            _Host = host

            _backOfficePath = ConfigurationManager.AppSettings("BackOfficePath").Trim
            _menuImagesExtension = ConfigurationManager.AppSettings("MenuImagesExtension").Trim
            _menuImagesExtensionModule = ConfigurationManager.AppSettings("MenuImagesExtensionModule").Trim
            _defaultVersionName = config.General.DefaultVersion

            If IsNothing(ConfigurationManager.AppSettings("BackOfficeMenuPath")) Then
                _backOfficeMenuPath = False
            Else
                _backOfficeMenuPath = ConfigurationManager.AppSettings("BackOfficeMenuPath").Trim
            End If

            If _backOfficeMenuPath Then
                _menuImagesExtension = "png"
                _menuImagesExtensionModule = "png"
            End If
        End Sub

        Public Sub ShowSettings() Implements IWidget.ShowSettings
        End Sub

        Public Sub HideSettings() Implements IWidget.HideSettings
            SaveState()
        End Sub

        Public Sub Minimized() Implements IWidget.Minimized
        End Sub

        Public Sub Maximized() Implements IWidget.Maximized
        End Sub

        Public Sub Closed() Implements IWidget.Closed
        End Sub

#End Region

#Region "TreeView Events"


        Public Function CompanyIdSelect() As Integer
            Dim Result As Integer = 0
            If Not IsNothing(ConfigurationManager.AppSettings("BackOffice.IsMultiCompany")) AndAlso
               (Boolean.Parse(ConfigurationManager.AppSettings("BackOffice.IsMultiCompany").ToString) = True) Then
                If Not IsNothing(HttpContext.Current) Then
                    If Not IsNothing(HttpContext.Current.Session) Then
                        Result = HttpContext.Current.Session("CompanyId")
                    End If
                End If
            End If
            Return Result
        End Function

        Protected Sub MenuTreeView_VirtualModeCreateChildren(source As Object, e As TreeViewVirtualModeCreateChildrenEventArgs) Handles MenuTreeView.VirtualModeCreateChildren
            Dim companyId As Integer = CompanyIdSelect()
            Dim currentModule As MenuInformation
            Dim moduleName As String = State.Element("Modules").Value
            Dim schemaCode As String = Session("sSche_Code")
            Dim children As New List(Of TreeViewVirtualNode)()
            Dim key As String = String.Format("Menu_{0}_{1}_{2}", schemaCode, moduleName, companyId)

            Dim Current As PageBase = TryCast(HttpContext.Current.Handler, PageBase)

            Dim isEmployee As Boolean = Current.UserInfo.User.IsEmployee

            If Not IsNothing(Current) AndAlso isEmployee Then
                MessageLabel.Visible = False

                If InMotionGIT.Common.Helpers.Caching.NotExist(key) Then
                    Using service As MenuService.MenuClient = New MenuService.MenuClient()
                        Try
                            With service
                                currentModule = .FullMenuInformation(moduleName, schemaCode, Session("CompanyId"))
                                .Close()
                            End With
                        Catch ex As Exception
                            Throw New InMotionGIT.Common.Exceptions.InMotionGITException(String.Format("There was an error Calling Service Menu with the following parameters: Module Name: {0}, Schema Code:{1}, Company Id:{2}", moduleName, schemaCode, IIf(IsNothing(Session("CompanyId")), "Vacio", Session("CompanyId").ToString)))
                        End Try

                    End Using
                    If Not IsNothing(currentModule) Then
                        InMotionGIT.Common.Helpers.Caching.SetItem(key, currentModule)
                    End If

                Else
                    currentModule = CType(InMotionGIT.Common.Helpers.Caching.GetItem(key), MenuInformation)
                End If

                If Not IsNothing(e.NodeName) Then
                    currentModule = FindChildrenModule(e.NodeName, currentModule)
                End If

                If Not IsNothing(currentModule) AndAlso Not IsNothing(currentModule.Items) AndAlso currentModule.Items.Count > 0 Then
                    For Each itemMenu As MenuInformation In currentModule.Items

                        With itemMenu
                            Dim childNode As New TreeViewVirtualNode(.WindowLogicalCode, .Description)

                            '+Si la transaccion es tipo menu(subcarpeta), se carga la descendencia
                            If .WindowType = 8 Then

                                If IsNothing(itemMenu.Items) OrElse itemMenu.Items.Count = 0 Then
                                    Continue For
                                End If

                                With childNode
                                    .Image.Url = InMotionGIT.FrontOffice.Proxy.Helper.PathImages(8, _backOfficePath, _menuImagesExtension, _defaultVersionName, _backOfficeMenuPath)
                                    .IsLeaf = False
                                End With

                            Else
                                ''+ Si la transacción está permitida por el esquema de seguridad
                                If .Permitted Then
                                    childNode.Image.Url = InMotionGIT.FrontOffice.Proxy.Helper.PathImages(.ImageId, _backOfficePath, _menuImagesExtension, _defaultVersionName, _backOfficeMenuPath)
                                    childNode.NavigateUrl = String.Format("javascript:insGoTo(""{0}"",""{1}"");", .URLAccessLink, .WindowLogicalCode)

                                Else
                                    With childNode
                                        .Image.Url = InMotionGIT.FrontOffice.Proxy.Helper.PathImages(9, _backOfficePath, _menuImagesExtension, _defaultVersionName, _backOfficeMenuPath)
                                        .NavigateUrl = "javascript:popupMessage.Show();"
                                    End With
                                End If

                                childNode.IsLeaf = True
                            End If

                            children.Add(childNode)
                        End With
                    Next
                End If

                e.Children = children

            Else
                With MessageLabel
                    .Visible = True
                    .Text = GetLocalResourceObject("EmployeeRoleMessage").ToString()
                End With
            End If
        End Sub

#End Region

#Region "Methods"

        Sub SaveState()
            Dim xml = State.Xml()
            _Host.SaveState(xml)
        End Sub

        Private Shared Function FindChildrenModule(nodeName As String, currentModule As MenuInformation) As MenuInformation
            Dim result As MenuInformation = Nothing

            If String.Equals(currentModule.WindowLogicalCode, nodeName) Then
                result = currentModule

            ElseIf Not IsNothing(currentModule.Items) Then
                For Each moduleItem As MenuInformation In currentModule.Items
                    If String.Equals(moduleItem.WindowLogicalCode, nodeName) Then
                        result = moduleItem
                        Exit For
                    End If

                    If moduleItem.WindowType = 8 Then
                        result = FindChildrenModule(nodeName, moduleItem)

                        If Not IsNothing(result) Then
                            Exit For
                        End If
                    End If
                Next
            End If

            Return result
        End Function

#End Region

    End Class
End Namespace
