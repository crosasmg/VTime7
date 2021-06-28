Imports Dropthings.Web.Framework
Imports System.Globalization
Imports System.Reflection
Imports System.Drawing

Partial Class Support_PI
    Inherits System.Web.UI.Page
#Region "Properties"
    Private _UserInfo As InMotionGIT.Membership.Providers.MemberContext

    Public Property UserInfo() As InMotionGIT.Membership.Providers.MemberContext
        Get
            If _UserInfo.IsEmpty Then
                _UserInfo = New InMotionGIT.Membership.Providers.MemberContext
            End If
            Return _UserInfo
        End Get
        Set(ByVal value As InMotionGIT.Membership.Providers.MemberContext)
            _UserInfo = value
        End Set
    End Property

    Dim rowCnt As Integer = 5
    Dim rowCtr As Integer
    Dim cellCtr As Integer = 5
    Dim cellCnt As Integer = 3
#End Region

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            lblKey.Text = InMotionGIT.Common.Helpers.KeyValidator.GenerateKey
            If Request.QueryString.IsNotEmpty AndAlso Request.QueryString("Key").IsNotEmpty Then
                If InMotionGIT.Common.Helpers.KeyValidator.KeyValidator(Request.QueryString("Key")) Then
                    GenerateViewerReport()
                End If
            End If

        End If
    End Sub

    Public Sub GenerateViewerReport()

        If UserInfo.IsNotEmpty Then
            lblUser.Text = String.Format("Current User:{0} ", UserInfo.UserName)
            Dim rowHeader As New TableRow()
            Dim cellName As New TableCell()
            cellName.HorizontalAlign = HorizontalAlign.Center
            cellName.Text = "Name"
            Dim cellValue As New TableCell()
            cellValue.HorizontalAlign = HorizontalAlign.Center
            cellValue.Text = "Value"

            With rowHeader
                cellName.BorderWidth = 1
                cellName.BorderColor = Color.Black

                cellValue.BorderWidth = 1
                cellValue.BorderColor = Color.Black
                .Cells.Add(cellName)
                .Cells.Add(cellValue)
            End With
            valuesUser.Rows.Add(rowHeader)
            ObjectGetValueAndName(UserInfo.User, valuesUser)
        Else
            lblUser.Text = "Not found user in profile"
        End If
    End Sub

    Public Sub ObjectGetValueAndName(profile As InMotionGIT.Membership.Providers.FrontOfficeMembershipUser, table As Table)

        Try

            Dim type As Type = GetType(InMotionGIT.Membership.Providers.FrontOfficeMembershipUser)
            Dim value As Object = Nothing
            Dim name As String
            Dim IsRetrive As Boolean = False
            For Each propertyInfo As PropertyInfo In type.GetProperties()

                IsRetrive = False
                If propertyInfo.Name.IsNotEmpty Then
                    Dim rowItem As New TableRow()
                    Dim cellName As New TableCell()
                    Dim cellValue As New TableCell()
                    name = propertyInfo.Name
                    Try
                        value = propertyInfo.GetValue(profile, Nothing)
                    Catch ex As Exception
                        IsRetrive = True
                    End Try
                    cellName.Text = name
                    If IsRetrive = False Then
                        If Not IsNothing(value) Then
                            Select Case value.GetType
                                Case GetType(Integer), GetType(String), GetType(DateTime), GetType(Date)
                                    cellValue.Text = IIf(value.ToString.IsEmpty, "Empty", value.ToString)
                                Case GetType(Boolean)
                                    cellValue.Text = IIf(DirectCast(value, Boolean), "Si", "No")
                                Case GetType(InMotionGIT.Membership.Providers.Enumerations.enumUserType)
                                    cellValue.Text = [Enum].GetName(GetType(InMotionGIT.Membership.Providers.Enumerations.enumUserType), DirectCast(value, InMotionGIT.Membership.Providers.Enumerations.enumUserType))
                                Case GetType(System.Configuration.SettingsContext)
                                    Dim CollectionItems As System.Configuration.SettingsContext = value
                                    Dim valuer As New StringBuilder
                                    For Each itemValue As DictionaryEntry In CollectionItems
                                        valuer.AppendLine(String.Format("Name:{0}, Value:{1},", itemValue.Key, itemValue.Value.ToString))
                                    Next
                                    Dim temporal As String = String.Empty
                                    temporal = valuer.ToString.Trim
                                    If temporal.EndsWith(",") Then
                                        temporal = temporal.Remove(temporal.ToString.Length - 1)
                                    End If
                                    cellValue.Text = temporal
                                Case Else
                                    cellValue.Text = "Type:" + value.GetType.ToString
                            End Select
                        Else
                            cellValue.Text = "Nothing"
                        End If

                    Else
                        cellValue.Text = "Cannot retrieve data"
                    End If

                    With rowItem
                        cellValue.BorderWidth = 1
                        cellValue.BorderColor = Color.Black
                        cellValue.HorizontalAlign = HorizontalAlign.Center

                        cellName.BorderWidth = 1
                        cellName.BorderColor = Color.Black
                        .Cells.Add(cellName)
                        .Cells.Add(cellValue)
                    End With

                    If Request.QueryString("full").IsEmpty() Then
                        If Not name.ToLower.Contains("pass") Then
                            table.Rows.Add(rowItem)
                        End If
                    Else
                        table.Rows.Add(rowItem)
                    End If

                End If
            Next
        Catch ex As Exception

        End Try
    End Sub

End Class