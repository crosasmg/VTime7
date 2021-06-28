Imports Dropthings.Web.Framework
Imports System.Globalization
Imports System.Reflection
Imports System.Drawing
Imports System.Data
Imports System.Web.Services
Imports System.Web.Script.Services
Imports InMotionGIT.Common.Extensions
Imports InMotionGIT.Common
Imports InMotionGIT.Common.Proxy
Imports System.Web.Script.Serialization

Partial Class Support_uet
    Inherits System.Web.UI.Page
#Region "Properties"
   
#End Region

#Region "Method"

    <WebMethod()>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function Execute(query As String, environment As Boolean) As String
        Dim resultQuery As DataTable
        Dim result As String = String.Empty
        Try
            With New InMotionGIT.Common.Proxy.DataManagerFactory(query,
                                                                 "GENERIC",
                                                                 If(environment,
                                                                                      "BackOfficeConnectionString",
                                                                                      "FrontOfficeConnectionString").ToString)
                resultQuery = .QueryExecuteToTable(True)
                If resultQuery.Rows.Count >= 200 Then
                    For index = 200 To resultQuery.Rows.Count - 1
                        resultQuery.Rows(index).Delete()
                    Next
                    resultQuery.AcceptChanges()
                End If
                result = DataSetToJSON(resultQuery)
            End With
        Catch ex As Exception
            Dim messageUser As String = "An error occurred while trying to execute the query, please check details in the error log"
            InMotionGIT.Common.Helpers.LogHandler.ErrorLog("ue.aspx", messageUser, ex)
            Throw New Exception(messageUser)
        End Try
        Return result
    End Function

#End Region

    Public Shared Function DataSetToJSON(dt As DataTable) As String
        Dim dict As New Dictionary(Of String, List(Of ResponseContainer))()
        Dim listValues As New List(Of ResponseContainer)

        For i As Integer = 0 To dt.Rows.Count - 1
            Dim temporalContainer As New ResponseContainer
            temporalContainer.Container = dt.Rows(i).ItemArray
            If i = 0 Then
                temporalContainer.Columns = (From itemColumn As DataColumn In dt.Columns Select itemColumn.ColumnName).ToArray
            End If
            listValues.Add(temporalContainer)
        Next

        dict.Add(dt.TableName, listValues)
        Dim json As New JavaScriptSerializer()
        json.MaxJsonLength = Int32.MaxValue
        Return json.Serialize(dict)
    End Function

#Region "Event page"

#End Region

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Request.QueryString.IsNotEmpty AndAlso Request.QueryString("Key").IsNotEmpty Then
                If InMotionGIT.Common.Helpers.KeyValidator.KeyValidator(Request.QueryString("Key")) Then
                    keyValid.Value = True
                Else
                    keyValid.Value = False
                End If
            End If
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "ShowControl",
                                            "<script type=text/javascript>ShowControl();</script>", False)
        End If
    End Sub

End Class

Public Class ResponseContainer
    Public Property Container() As Object
    Public Property Columns() As Object
End Class