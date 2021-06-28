#Region "using"

Imports System.Data
Imports System.Web.Script.Services
Imports System.Web.Services
Imports GIT.Core
Imports InMotionGIT.Common.Proxy

#End Region

Partial Class WorkFlowManager
    Inherits PageBase

    <WebMethod>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function ViewHistory(Id As String) As Object
        Dim result As New List(Of Object)
        Dim Query As String = String.Format(" SELECT " +
                                            " 	WORKFLOWTRACKING.RECORDNUMBER, " +
                                            " 	ACTIVITYNAME, " +
                                            " 	ACTIVITYTYPE, " +
                                            " 	ACTIVITYSTATE AS State, " +
                                            " 	STARTDATE AS TIMESTART, " +
                                            " 	FINISHDATE AS TIMEFINISH, " +
                                            " 	NVL(EXTRACT ( " +
                                            " 		SECOND " +
                                            " 		FROM " +
                                            " 			(FINISHDATE - STARTDATE) " +
                                            " 	),0) AS DURATION, " +
                                            " 	WORKFLOWCUSTOMTRACK. DATA " +
                                            " FROM " +
                                            " 	frontoffice.WORKFLOWTRACKING " +
                                            " LEFT JOIN frontoffice.WORKFLOWCUSTOMTRACK ON WORKFLOWCUSTOMTRACK.WORKFLOWINSTANCEID = WORKFLOWTRACKING.WORKFLOWINSTANCEID " +
                                            " AND WORKFLOWCUSTOMTRACK.RECORDNUMBER = WORKFLOWTRACKING.RECORDNUMBER " +
                                            " WHERE " +
                                            " 	WORKFLOWTRACKING.WORKFLOWINSTANCEID = '{0}' " +
                                            " ORDER BY " +
                                            " 	WORKFLOWTRACKING.RECORDNUMBER ASC ", Id)

        With New DataManagerFactory(Query, "GENERAL", "FrontOfficeConnectionString")
            Dim table = .QueryExecuteToTable(True)
            If table.IsNotEmpty() AndAlso table.Rows.Count <> 0 Then
                For Each row As Data.DataRow In table.Rows
                    result.Add(New With {Key .RecordNumber = row.IntegerValue("RECORDNUMBER"),
                                         Key .ActivityName = row.StringValue("ACTIVITYNAME"),
                                         Key .ActivityType = row.StringValue("ACTIVITYTYPE"),
                                         Key .State = row.StringValue("State"),
                                         Key .TimeStart = row.DateTimeValue("TIMESTART"),
                                         Key .TimeFinish = row.DateTimeValue("TIMEFINISH"),
                                         Key .Duration = TimeSpan.FromSeconds(Decimal.Parse(row.StringValue("DURATION"))).ToString("ss\:fff"),
                                         Key .Data = row.StringValue("DATA")})
                Next
            End If
        End With



        Return result
    End Function



    <WebMethod>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function ViewDetail(Id As String) As Object
        Dim reason As String = ""
        Dim data As String = ""
        Dim Query As String = ""
        Dim ExistData As Boolean = False

        Query = String.Format(" SELECT" +
                                " 	REASON" +
                                " FROM" +
                                " 	workflowinstance" +
                                " WHERE" +
                                " 	workflowinstance.WORKFLOWINSTANCEID = '{0}' ", Id)

        With New DataManagerFactory(Query, "GENERAL", "FrontOfficeConnectionString")
            Dim table = .QueryExecuteToTable(True)
            If table.IsNotEmpty() AndAlso table.Rows.Count <> 0 Then
                reason = table.Rows(0).StringValue("REASON")
                If reason.IsNotEmpty() Then
                    ExistData = True
                End If
            End If
        End With

        If ExistData Then
            Query = String.Format(" SELECT" +
                  " 	DATA" +
                  " FROM" +
                  " 	FRONTOFFICE.WORKFLOWEXCEPTION" +
                  " WHERE" +
                  " 	WORKFLOWINSTANCEID = '{0}'", Id)
            With New DataManagerFactory(Query, "GENERAL", "FrontOfficeConnectionString")
                Dim table = .QueryExecuteToTable(True)
                If table.IsNotEmpty() AndAlso table.Rows.Count <> 0 Then
                    data = table.Rows(0).StringValue("DATA")
                End If
            End With


        End If

        Return New With {Key .ExistData = ExistData, Key .Reason = reason, Key .Data = data}
    End Function

    <WebMethod>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function Workflows(dateStart As Date, dateEnd As Date, filter As String) As Object
        dateStart = New Date(dateStart.Year, dateStart.Month, dateStart.Day, 0, 0, 0)
        dateEnd = New Date(dateEnd.Year, dateEnd.Month, dateEnd.Day, 23, 59, 59)
        Dim result As New List(Of Object)
        Dim Query As String = " SELECT" +
                            " 	TIMECREATED," +
                            " 	WORKFLOWINSTANCEID," +
                            " 	NVL(IDENTIFY, NAME) AS IDENTIFY," +
                            " 	NAME," +
                            " 	WORKFLOWSTATE," +
                            " 	REASON," +
                            " 	STARTDATE," +
                            " 	FINISHDATE," +
                            " 	NVL(EXTRACT (" +
                            " 		SECOND" +
                            " 		FROM" +
                            " 			(FINISHDATE - STARTDATE)" +
                            " 	),0) AS DURATION" +
                            " FROM" +
                            " 	(" +
                            " 		SELECT" +
                            " 			TIMECREATED," +
                            " 			workflowinstance.WORKFLOWINSTANCEID," +
                            " 			IDENTIFY," +
                            " 			NAME," +
                            " 			WORKFLOWSTATE," +
                            " 			REASON," +
                            " 			STARTDATE," +
                            " 			FINISHDATE," +
                            " 			ROW_NUMBER () OVER (ORDER BY TIMECREATED DESC) ROW_NUM" +
                            " 		FROM" +
                            " 			frontoffice.workflowinstance" +
                            "      WHERE " +
                            "           frontoffice.workflowinstance.TIMECREATED  between @:DATEFROM" &
                            "           AND @:DATETO  <<FILTER>> " +
                            " 	)" +
                            " WHERE" +
                            " 	ROW_NUM BETWEEN 1" +
                            " AND 200 "

        If filter.IsNotEmpty() Then
            If filter.Contains("%") Then
                Query = Query.Replace("<<FILTER>>", String.Format(" AND (IDENTIFY like '{0}' OR NAME like '{0}')", filter))
            Else
                Query = Query.Replace("<<FILTER>>", String.Format(" AND (IDENTIFY like '%{0}%' OR NAME like '%{0}%')", filter))
            End If
        Else
            Query = Query.Replace("<<FILTER>>", String.Empty)
        End If

        With New DataManagerFactory(Query, "GENERAL", "FrontOfficeConnectionString")
            dateStart = dateStart.AddDays(-1)
            .AddParameter("DATEFROM", DbType.DateTimeOffset, 10, False, dateStart)
            .AddParameter("DATETO", DbType.DateTimeOffset, 10, False, dateEnd)
            Dim table = .QueryExecuteToTable(True)
            If table.IsNotEmpty() AndAlso table.Rows.IsNotEmpty() Then
                For Each row As Data.DataRow In table.Rows
                    result.Add(New With {Key .TimeCreated = row.DateTimeValue("TIMECREATED"),
                                         Key .WorkflowinstanceId = row.StringValue("WORKFLOWINSTANCEID"),
                                         Key .Identify = row.StringValue("IDENTIFY"),
                                         Key .Name = row.StringValue("NAME"),
                                         Key .WorkflowState = row.StringValue("WORKFLOWSTATE"),
                                         Key .Reason = row.StringValue("REASON"),
                                         Key .StartDate = row.DateTimeValue("STARTDATE"),
                                         Key .FinishDate = row.DateTimeValue("FINISHDATE"),
                                         Key .Duration = TimeSpan.FromSeconds(Decimal.Parse(row.StringValue("DURATION"))).ToString("ss\:fff")})
                Next
            End If
        End With
        Return result
    End Function

End Class