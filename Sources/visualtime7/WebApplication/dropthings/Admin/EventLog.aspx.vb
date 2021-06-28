Imports InMotionGIT.Common.Proxy
Imports System.Web.Services
Imports System.Data
Imports System.Globalization

Partial Class dropthings_Admin_EventLog
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (Not IsPostBack) Then
            LoadEnumetrationsTraceType()
        End If
    End Sub

#Region "Methods Loads"
    Public Sub LoadEnumetrationsTraceType()
        Dim names() As String = [Enum].GetNames(GetType(InMotionGIT.Common.Enumerations.EnumTraceType))
        ddlTypeTrace.Items.Add(New ListItem("All", -1, True))
        Dim cont As Integer = 0
        For Each name In names
            ddlTypeTrace.Items.Add(New ListItem(name, cont, True))
            ddlTypeTrace.Items(cont).Attributes.Add("onclick", "onSplInstructionChecked(this);")
            ddlTypeTrace.Items(cont).Selected = True
            cont = cont + 1
        Next
    End Sub
#End Region

#Region "Methos data access"
    <WebMethod(EnableSession:=True)> _
    Public Shared Function EventLogListByFilter(DateFrom As String, DateTo As String, typeTraceName As String, filter As String, IsLike As String, jtStartIndex As Integer, jtPageSize As Integer, jtSorting As String) As Object
        Return EventLogListByFilterMethod(DateFrom, DateTo, typeTraceName, filter, IsLike, jtSorting, jtStartIndex, jtPageSize, jtSorting)
    End Function

    <WebMethod(EnableSession:=True)> _
    Public Shared Function EventLogDetaild(EventLogId As Integer) As Object
        Try
            Dim ListDetaild As New List(Of EventLogDetailClass)
            Dim Result As New EventLogDetailClass
            With New DataManagerFactory(String.Format(" SELECT * FROM EVENTLOGDETAIL " & _
                                                      " WHERE EVENTLOGDETAIL.ID = {0} ", EventLogId), "EVENTLOGDETAIL", "FrontOfficeConnectionString")
                Dim ResultDataTable = .QueryExecuteToTable(True)
                If Not IsNothing(Result) AndAlso ResultDataTable.Rows.Count <> 0 Then
                    For Each ItemRow As DataRow In ResultDataTable.Rows
                        Result.Detail = ItemRow.StringValue("DETAIL")
                        Result.Id = ItemRow.IntegerValue("ID")
                    Next
                End If
            End With
            ListDetaild.Add(Result)
            Return New With {.Result = "OK", .Records = ListDetaild}
        Catch ex As Exception
            Return New With {.Result = "ERROR", .Message = ex.Message}
        End Try
    End Function

    Public Shared Function EventLogListByFilterMethod(DateFrom As String, DateTo As String, typeTrace As String, filter As String, IsLike As Boolean, order As String, jtStartIndex As Integer, jtPageSize As Integer, jtSorting As String) As Object
        Try
            ''Get data from database
            Dim eventList As List(Of EventLogClass) = EventLogList(DateFrom, DateTo, typeTrace, filter, IsLike, jtStartIndex, jtPageSize, order)

            Dim count As Integer = EventLogCount(DateFrom, DateTo, typeTrace, filter, IsLike)

            'Return result to jTable
            Return New With {.Result = "OK", .Records = eventList, .TotalRecordCount = count}
        Catch ex As Exception
            Return New With {.Result = "ERROR", .Message = ex.Message}
        End Try
    End Function

    Public Shared Function EventLogList(DateFrom As String, DateTo As String, TypeTrece As String, filter As String, IsLike As Boolean, jtstartIndex As Integer, jtPageSize As Integer, order As String) As List(Of EventLogClass)
        Dim Result As New List(Of EventLogClass)
        Dim ResultDataTable As DataTable
        If Not String.IsNullOrEmpty(DateFrom) AndAlso Not String.IsNullOrEmpty(DateFrom) Then

            Dim formats As String() = {"MM/dd/yyyy"}

            Dim tempDateFrom As Date = DateTime.ParseExact(DateFrom, formats, New CultureInfo("en-US"), DateTimeStyles.None)
            Dim tempDateTo As Date = DateTime.ParseExact(DateTo, formats, New CultureInfo("en-US"), DateTimeStyles.None).AddDays(1)

            Dim sql As String = String.Empty

            If filter.IsNotEmpty Then
                If IsLike Then
                    sql += String.Format(" SELECT " & _
                                                     "	EVENTLOG.*,  (SELECT COUNT(*) FROM EVENTLOGDETAIL WHERE ID = EVENTLOG.ID ) ISEXIST " & _
                                                     " FROM " & _
                                                     "	EVENTLOG " & _
                                                     " WHERE (EVENTLOG.FACTTIME  between @:DATEFROM " & _
                                                     " AND @:DATETO ) AND EVENTLOG.CODE LIKE '%{0}%'  ", filter)
                Else
                    sql += String.Format(" SELECT " & _
                                                     "	EVENTLOG.*,  (SELECT COUNT(*) FROM EVENTLOGDETAIL WHERE ID = EVENTLOG.ID ) ISEXIST " & _
                                                     " FROM " & _
                                                     "	EVENTLOG " & _
                                                     " WHERE (EVENTLOG.FACTTIME  between @:DATEFROM " & _
                                                     " AND @:DATETO ) AND EVENTLOG.CODE LIKE '{0}'  ", filter)
                End If
            Else
                sql += String.Format(" SELECT " & _
                                                   "	EVENTLOG.*,  (SELECT COUNT(*) FROM EVENTLOGDETAIL WHERE ID = EVENTLOG.ID ) ISEXIST " & _
                                                   " FROM " & _
                                                   "	EVENTLOG " & _
                                                   " WHERE (EVENTLOG.FACTTIME  between @:DATEFROM " & _
                                                   " AND @:DATETO ) ")
            End If

            If Not TypeTrece.Equals("-1") Then
                sql = sql + String.Format(" AND EVENTLOG.TYPETRACE IN ({0})  ", TypeTrece.Replace("-1,", ""))
            End If

            If Not String.IsNullOrEmpty(order) Then
                Dim vertor As String() = order.Split(" ")
                If vertor(0).Equals("Source") Then
                    sql = sql + "ORDER BY SOURCE " + vertor(1)
                ElseIf vertor(0).Equals("TypeTrace") Then
                    sql = sql + "ORDER BY TYPETRACE " + vertor(1)
                ElseIf vertor(0).Equals("FactTime") Then
                    sql = sql + "ORDER BY FACTTIME " + vertor(1)
                ElseIf vertor(0).Equals("HostSource") Then
                    sql = sql + "ORDER BY HOSTSOURCE " + vertor(1)
                ElseIf vertor(0).Equals("Code") Then
                    sql = sql + "ORDER BY CODE " + vertor(1)
                End If
            End If

            With New DataManagerFactory(sql, "EVENTLOG", "FrontOfficeConnectionString")
                .AddParameter("DATEFROM", DbType.DateTimeOffset, 10, False, tempDateFrom)
                .AddParameter("DATETO", DbType.DateTimeOffset, 10, False, tempDateTo)
                ResultDataTable = .QueryExecuteToTable(True)
                If Not IsNothing(Result) AndAlso ResultDataTable.Rows.Count <> 0 Then
                    For Each ItemRow As DataRow In ResultDataTable.Rows
                        Result.Add(ProcessRowEvenLogItem(ItemRow))
                    Next
                End If
            End With
        End If

        If Result.Count > jtPageSize Then
            Return Result.Skip(jtstartIndex).Take(jtPageSize).ToList()
        Else
            Return Result
        End If

    End Function

    Private Shared Function ProcessRowEvenLogItem(ItemRow As DataRow) As EventLogClass
        Dim itemList As New EventLogClass
        With ItemRow
            itemList.Id = ItemRow.IntegerValue("ID")
            itemList.FactTime = ItemRow.DateTimeValue("FACTTIME")
            itemList.HostSource = ItemRow.StringValue("HOSTSOURCE")
            itemList.TypeTrace = ItemRow.StringValue("TYPETRACE")
            itemList.Source = ItemRow.StringValue("SOURCE")
            itemList.Entry = ItemRow.StringValue("ENTRY")
            itemList.IsActive = ItemRow.BooleanValue("ISEXIST")
            itemList.Code = ItemRow.StringValue("CODE")
        End With
        Return itemList
    End Function
#End Region

    Private Shared Function EventLogCount(DateFrom As String, DateTo As String, ErrorTypes As String, filter As String, IsLike As Boolean) As Integer
        Dim Result As Integer = 0


        Dim formats As String() = {"MM/dd/yyyy"}

        Dim tempDateFrom As Date = DateTime.ParseExact(DateFrom, formats, New CultureInfo("en-US"), DateTimeStyles.None)
        Dim tempDateTo As Date = DateTime.ParseExact(DateTo, formats, New CultureInfo("en-US"), DateTimeStyles.None).AddDays(1)

        Dim sql As String = String.Empty

        If filter.IsNotEmpty Then
            If IsLike Then
                sql += String.Format(" SELECT " & _
                                                       "	COUNT(*) " & _
                                                       " FROM " & _
                                                       "	EVENTLOG " & _
                                                       " WHERE (EVENTLOG.FACTTIME  between @:DATEFROM" & _
                                                       "       AND @:DATETO ) AND EVENTLOG.CODE LIKE '%{0}%'  ", filter)
            Else
                sql += String.Format(" SELECT " & _
                                                    "	COUNT(*) " & _
                                                    " FROM " & _
                                                    "	EVENTLOG " & _
                                                    " WHERE (EVENTLOG.FACTTIME  between @:DATEFROM" & _
                                                    "       AND @:DATETO )  AND EVENTLOG.CODE LIKE '{0}'  ", filter)
            End If
        Else
            sql += String.Format(" SELECT " & _
                                                    "	COUNT(*) " & _
                                                    " FROM " & _
                                                    "	EVENTLOG " & _
                                                    " WHERE (EVENTLOG.FACTTIME  between @:DATEFROM" & _
                                                    "       AND @:DATETO )  ")

        End If

        If Not ErrorTypes.Equals("-1") Then
            sql = sql + String.Format(" AND EVENTLOG.TYPETRACE IN ({0})  ", ErrorTypes.Replace("-1,", ""))
        End If

        With New DataManagerFactory(sql, "EVENTLOG", "FrontOfficeConnectionString")
            .AddParameter("DATEFROM", DbType.DateTimeOffset, 10, False, tempDateFrom)
            .AddParameter("DATETO", DbType.DateTimeOffset, 10, False, tempDateTo)
            Result = .QueryExecuteScalarToInteger
        End With
        Return Result
    End Function

End Class

Public Class EventLogClass
    Public Property Id As Integer
    Public Property FactTime As Date
    Public Property HostSource As String
    Public Property TypeTrace As Integer
    Public Property Source As String
    Public Property Entry As String
    Public Property Count As Integer
    Public Property IsActive As Boolean
    Public Property Code As String
End Class

Public Class EventLogDetailClass
    Public Property Id As Integer
    Public Property Detail As String
End Class