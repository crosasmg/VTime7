Imports System
Imports System.Data
Imports System.Configuration
Imports System.Collections
Imports System.Web
Imports System.Web.Security
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts
Imports System.Web.UI.HtmlControls
Imports System.Xml
Imports Dropthings.Widget.Framework
Imports System.Data.SqlClient
Imports GIT.Core
Imports InMotionGIT.Common.Proxy
Imports InMotionGIT.Common.Enumerations

Namespace Dropthings.Widgets

    Partial Class UserSurveyUserControl
        Inherits System.Web.UI.UserControl
        Implements IWidget

        Protected Sub SubmitButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles SubmitButton.Click
            If RadioButtonList1.SelectedValue = String.Empty Then
                MessageLabel.Text = "You should choose some option!"
            Else
                If RadioButtonList1.Items(0).Selected = True Then
                    UpdateResult("1")
                End If
                If RadioButtonList1.Items(1).Selected = True Then
                    UpdateResult("2")
                End If
                If RadioButtonList1.Items(2).Selected = True Then
                    UpdateResult("3")
                End If
                If RadioButtonList1.Items(3).Selected = True Then
                    UpdateResult("4")
                End If
                If RadioButtonList1.Items(4).Selected = True Then
                    UpdateResult("5")
                End If
                MultiViewQuestionnaire.ActiveViewIndex = 1
            End If
        End Sub

        Private Sub UpdateResult(ByVal opcion As String)
            Dim conn As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("FrontOfficeConnectionString").ConnectionString)
            Dim qryUpdate As String = "UPDATE dbo.Poll" & _
                                      " SET Poll.Response" + opcion + " = Poll.Response" + opcion + " + 1" + _
                                      " WHERE Poll.Id = 1"
            conn.Open()
            Dim ss As New System.Data.SqlClient.SqlCommand(qryUpdate, conn)
            ss.ExecuteNonQuery()
        End Sub

        Protected Sub MultiViewQuestionnaire_ActiveViewChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles MultiViewQuestionnaire.ActiveViewChanged
            Dim _lannguageId As Integer = IIf(GetCurrentLanguage() = EnumLanguage.Spanish, 2, 1)

            Select Case MultiViewQuestionnaire.ActiveViewIndex
                Case 0
                    '                    If Not IsPostBack Then
                    RadioButtonList1.Items.Clear()
                    Dim dt As DataTable = Nothing
                    Dim _clienFactory As New DataManagerFactory(String.Format("SELECT      POLL.ID,  POLLDETAIL.LANGUAGE,  POLL.STARDATE,  POLL.EXPIRDATE,  POLL.PROFILE,  POLLDETAIL.QUESTION,  POLLDETAIL.OPTION1,  POLL.RESPONSE1, " & _
                                                                                "                       POLLDETAIL.OPTION2,  POLL.RESPONSE2,  POLLDETAIL.OPTION3,  POLL.RESPONSE3,  POLLDETAIL.OPTION4,  POLL.RESPONSE4,  POLLDETAIL.OPTION5, " & _
                                                                                "                       POLL.RESPONSE5 " & _
                                                                                "FROM         POLL " & _
                                                                                "INNER JOIN   POLLDETAIL  " & _
                                                                                "ON POLL.ID = POLLDETAIL.ID " & _
                                                                                "WHERE POLLDETAIL.LANGUAGE = {0} ", _lannguageId.ToString()),
                                                                            "POLL", "FrontOfficeConnectionString")
                    dt = _clienFactory.QueryExecuteToTable(True)

                    If dt.Rows.Count > 0 Then
                        Dim row As DataRow = dt.Rows(0)
                        Dim optionvalue As String

                        QuestionLabel.Text = row.Field(Of String)("Question")

                        optionvalue = row.Field(Of String)("Option1")
                        If optionvalue.Length > 0 Then
                            RadioButtonList1.Items.Add(New ListItem(optionvalue, "1"))
                        End If

                        optionvalue = row.Field(Of String)("Option2")
                        If optionvalue.Length > 0 Then
                            RadioButtonList1.Items.Add(New ListItem(optionvalue, "2"))
                        End If

                        optionvalue = row.Field(Of String)("Option3")
                        If optionvalue.Length > 0 Then
                            RadioButtonList1.Items.Add(New ListItem(optionvalue, "3"))
                        End If

                        optionvalue = row.Field(Of String)("Option4")
                        If optionvalue.Length > 0 Then
                            RadioButtonList1.Items.Add(New ListItem(optionvalue, "4"))
                        End If

                        optionvalue = row.Field(Of String)("Option5")
                        If optionvalue.Length > 0 Then
                            RadioButtonList1.Items.Add(New ListItem(optionvalue, "5"))
                        End If
                    End If

                    'End If
                    ViewState("aaa") = 1
                    RadioButtonList1.SelectedIndex = -1
                Case 1
                    MessageLabel.Text = String.Empty
                    Dim dt As DataTable = Nothing
                    Dim _clienFactory As New DataManagerFactory(String.Format("SELECT      POLL.ID,  POLLDETAIL.LANGUAGE,  POLL.STARDATE,  POLL.EXPIRDATE,  POLL.PROFILE,  POLLDETAIL.QUESTION,  POLLDETAIL.OPTION1,  POLL.RESPONSE1, " & _
                                                                               "                       POLLDETAIL.OPTION2,  POLL.RESPONSE2,  POLLDETAIL.OPTION3,  POLL.RESPONSE3,  POLLDETAIL.OPTION4,  POLL.RESPONSE4,  POLLDETAIL.OPTION5, " & _
                                                                               "                       POLL.RESPONSE5 " & _
                                                                               "FROM         POLL " & _
                                                                               "INNER JOIN   POLLDETAIL  " & _
                                                                               "ON POLL.ID = POLLDETAIL.ID " & _
                                                                               "WHERE POLLDETAIL.LANGUAGE = {0} ", _lannguageId.ToString()),
                                                                           "POLL", "FrontOfficeConnectionString")
                    dt = _clienFactory.QueryExecuteToTable(True)

                    If dt.Rows.Count > 0 Then
                        Dim row As DataRow = dt.Rows(0)
                        Dim optionvalue As String

                        Question1Label.Text = row.Field(Of String)("Question")

                        TotalLabel.Text = (row.Field(Of Decimal)("Response1") + _
                                         row.Field(Of Decimal)("Response2") + _
                                         row.Field(Of Decimal)("Response3") + _
                                         row.Field(Of Decimal)("Response4") + _
                                         row.Field(Of Decimal)("Response5")).ToString

                        optionvalue = row.Field(Of String)("Option1")
                        If optionvalue.Length > 0 Then
                            Option1Label.Text = optionvalue
                            Response1Label.Text = "( " + row.Field(Of Decimal)("Response1").ToString + " )"
                        End If

                        optionvalue = row.Field(Of String)("Option2")
                        If optionvalue.Length > 0 Then
                            Option2Label.Text = optionvalue
                            Response2Label.Text = "( " + row.Field(Of Decimal)("Response2").ToString + " )"
                        End If

                        optionvalue = row.Field(Of String)("Option3")
                        If optionvalue.Length > 0 Then
                            Option3Label.Text = optionvalue
                            Response3Label.Text = "( " + row.Field(Of Decimal)("Response3").ToString + " )"
                        End If

                        optionvalue = row.Field(Of String)("Option4")
                        If optionvalue.Length > 0 Then
                            Option4Label.Text = optionvalue
                            Response4Label.Text = "( " + row.Field(Of Decimal)("Response4").ToString + " )"
                        End If

                        optionvalue = row.Field(Of String)("Option5")
                        If optionvalue.Length > 0 Then
                            Option5Label.Text = optionvalue
                            Response5Label.Text = "( " + row.Field(Of Decimal)("Response5").ToString + " )"
                        End If
                    End If
            End Select
        End Sub

        Protected Sub ResultsButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ResultsButton.Click
            MultiViewQuestionnaire.ActiveViewIndex = 1
        End Sub

        Protected Sub QuestionButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles QuestionButton.Click
            MultiViewQuestionnaire.ActiveViewIndex = 0
        End Sub

        Public Sub Closed() Implements Dropthings.Widget.Framework.IWidget.Closed

        End Sub

        Public Sub HideSettings() Implements Dropthings.Widget.Framework.IWidget.HideSettings

        End Sub

        Public Sub Init1(ByVal host As Dropthings.Widget.Framework.IWidgetHost) Implements Dropthings.Widget.Framework.IWidget.Init

        End Sub

        Public Sub Maximized() Implements Dropthings.Widget.Framework.IWidget.Maximized

        End Sub

        Public Sub Minimized() Implements Dropthings.Widget.Framework.IWidget.Minimized

        End Sub

        Public Sub ShowSettings() Implements Dropthings.Widget.Framework.IWidget.ShowSettings

        End Sub

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

            If ViewState("aaa") <> 1 Then
                MultiViewQuestionnaire_ActiveViewChanged(Nothing, Nothing)
            End If
        End Sub

        Public Shared Function GetCurrentLanguage() As Integer
            If System.Threading.Thread.CurrentThread.CurrentCulture.Name.ToLower.StartsWith("es-") Then
                Return EnumLanguage.Spanish
            Else
                Return EnumLanguage.English
            End If
        End Function
    End Class
End Namespace
