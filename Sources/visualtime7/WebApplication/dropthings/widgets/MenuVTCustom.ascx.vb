#Region "using"

Imports Dropthings.Widget.Framework
Imports System.Data
Imports System.Xml.Linq
Imports System
Imports InMotionGIT.FrontOffice.Proxy
Imports InMotionGIT.FrontOffice.Proxy.MenuService
Imports DevExpress.Web.ASPxTreeView
Imports GIT.Core

#End Region

Namespace Dropthings.Widgets

    Partial Class MenuUserControl
        Inherits System.Web.UI.UserControl
        Implements IWidget

#Region "Private fields, to hold the state of the entity"

        Private _backOfficePath As String = String.Empty
        Private _menuImagesExtension As String = String.Empty
        Private _menuImagesExtensionModule As String = String.Empty
        Private _defaultVersionName As String = String.Empty
        Private _backOfficeMenuPath As Boolean
        Private config As InMotionGIT.Core.Configuration.VisualTIME = DirectCast(ConfigurationManager.GetSection("VisualTIMEConfigurationGroup/VisualTIMESection"), InMotionGIT.Core.Configuration.VisualTIME)
#End Region

#Region "Public properties"

        Private _Host As IWidgetHost
        Private _State As XElement
        Private _Id As Integer

        Private ReadOnly Property State() As XElement
            Get
                If (IsNothing(_State)) Then _State = XElement.Parse(Me._Host.GetState())
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
            _Id = _Host.ID

            HLinkWindows.NavigateUrl = String.Format("MenuVTCustomDetail.aspx?id={0}&List={1}", _Id.ToString, State.Element("Modules").Value.Replace("','", ","))

            LoadWindows(State.Element("Modules").Value, Session("sSche_Code"))
        End Sub

        Public Sub ShowSettings() Implements IWidget.ShowSettings
            Dim Current As PageBase = TryCast(HttpContext.Current.Handler, PageBase)

            If Current.UserInfo.IsEmployee Then
                pnlEdit.Visible = True
                MessageLabel.Visible = False

            Else
                pnlEdit.Visible = False

                With MessageLabel
                    .Visible = True
                    .Text = Me.GetLocalResourceObject("TransactionMessage").ToString()
                End With
            End If

        End Sub

        Public Sub HideSettings() Implements IWidget.HideSettings

            LoadWindows(State.Element("Modules").Value, Session("sSche_Code"))
            pnlEdit.Visible = False
            MessageLabel.Visible = False
        End Sub

        Public Sub Minimized() Implements IWidget.Minimized
        End Sub

        Public Sub Maximized() Implements IWidget.Maximized
        End Sub

        Public Sub Closed() Implements IWidget.Closed
        End Sub

#End Region

#Region "Page Events"

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            If IsPostBack Then
                LoadWindows(State.Element("Modules").Value, Session("sSche_Code"))
            End If
        End Sub
#End Region

#Region "Methods"

        Sub LoadWindows(ByVal sModule As String, ByVal sSche_Code As String)
            Dim Current As PageBase = TryCast(HttpContext.Current.Handler, PageBase)
            If DashboardBusiness.Helpers.Security.IsEmployee(Current.UserInfo.UserName) Then
                MessageLabel.Visible = False

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

                LoadMenuCustom(sModule, sSche_Code)

            Else
                With MessageLabel
                    .Visible = True
                    .Text = GetLocalResourceObject("EmployeeRoleMessage").ToString()
                End With
            End If
        End Sub

        Private Sub LoadMenuCustom(ByVal sModule As String, ByVal sSche_Code As String)
            MenuTreeView.Nodes.Clear()

            If Not String.IsNullOrEmpty(sModule) AndAlso
               Not String.IsNullOrEmpty(sSche_Code) Then
                MenuTreeView.Visible = True

                Dim modules As MenuInformationList
                Dim companyId As Integer = CompanyIdSelect()

                modules = InMotionGIT.FrontOffice.Proxy.Helpers.Menu.FullWindowsList(sModule, sSche_Code, Session("CompanyId"))

                If Not IsNothing(modules) Then
                    For Each item As MenuInformation In modules
                        ' Fill TreeView
                        Dim newNode As TreeViewNode = New TreeViewNode() With {.Text = item.Description, _
                                                                               .Name = item.WindowLogicalCode, _
                                                                               .Expanded = True}
                        With item
                            '+ Si la transacción está permitida por el esquema de seguridad
                            If .Permitted Then
                                newNode.Image.Url = InMotionGIT.FrontOffice.Proxy.Helper.PathImages(.ImageId, _backOfficePath, _menuImagesExtension, _defaultVersionName, _backOfficeMenuPath)
                                newNode.NavigateUrl = String.Format("javascript:insGoTo(""{0}"", ""{1}"");", .URLAccessLink, item.WindowLogicalCode)

                            Else
                                '<MR: mejorar, enviar mensaje igual que en el portal>
                                With newNode
                                    .Image.Url = InMotionGIT.FrontOffice.Proxy.Helper.PathImages(9, _backOfficePath, _menuImagesExtension, _defaultVersionName, _backOfficeMenuPath)
                                    .NavigateUrl = "javascript:popupMessage.Show();"
                                End With
                            End If

                            MenuTreeView.Nodes.Add(newNode)
                        End With
                    Next
                End If
            End If

        End Sub

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

#End Region

    End Class
End Namespace