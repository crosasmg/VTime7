#Region "using"

Imports System.Globalization
Imports GIT.Core
Imports DevExpress.Web.ASPxGridView
Imports DevExpress.Web.ASPxEditors
Imports System
Imports DevExpress.Web.Data
Imports InMotionGIT.Common.Proxy
Imports InMotionGIT.Common.Helpers.Language
Imports InMotionGIT.Common.Helpers
Imports InMotionGIT.Data
Imports System.IO
Imports DevExpress.Web.ASPxClasses
Imports System.Data
Imports System.Data.Common
Imports DevExpress.Web.ASPxUploadControl

#End Region

Partial Class Maintenance_TabGeographicZoneTables
    Inherits PageBase

#Region "Private fields"

    Private _internalCall As Boolean

#End Region

#Region "Events Page"

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not IsCallback AndAlso Not IsPostBack Then
 

        End If
        
        If Not CurrentState.Contains("LanguageId") Then
            CurrentState.Add("LanguageId", LanguageId)
        End If
    End Sub

#End Region

#Region "MainMenu Events"

    Protected Sub MainMenu_ItemClick(source As Object, e As DevExpress.Web.ASPxMenu.MenuItemEventArgs) Handles MainMenu.ItemClick
      
    End Sub

#End Region

#Region "Controls Events"

         
    
 

#End Region

#Region "TabGeographicZoneTables Events"
    
    Protected Sub TabGeographicZoneTables_CustomColumnDisplayText(sender As Object, e As DevExpress.Web.ASPxGridView.ASPxGridViewColumnDisplayTextEventArgs) Handles TabGeographicZoneTables.CustomColumnDisplayText
          Dim data As DataTable
          Dim rows() As DataRow
           
          Select Case e.Column.FieldName

               Case Else              
           End Select
    End Sub
  
    Protected Sub TabGeographicZoneTables_DataBinding(ByVal sender As Object, ByVal e As EventArgs) Handles TabGeographicZoneTables.DataBinding
        If (Not IsNothing(Request.Params("__CALLBACKID")) AndAlso Request.Params("__CALLBACKID").EndsWith("TabGeographicZoneTables")) Or _internalCall Then
                       If Caching.Exist("TabCountry") Then
                DirectCast(TabGeographicZoneTables.Columns("COUNTRYID"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = Caching.GetItem("TabCountry")

            Else
                Dim source As DataTable = Nothing

                With New DataManagerFactory("SELECT  TABCOUNTRY.COUNTRYID, TABCOUNTRY.RECORDSTATUS, TRANSCOUNTRY.LANGUAGEID, TRANSCOUNTRY.DESCRIPTION FROM COMMON.TABCOUNTRY TABCOUNTRY JOIN COMMON.TRANSCOUNTRY TRANSCOUNTRY ON TRANSCOUNTRY.COUNTRYID = TABCOUNTRY.COUNTRYID  WHERE TABCOUNTRY.RECORDSTATUS = '1' AND TRANSCOUNTRY.LANGUAGEID = @:LANGUAGEID ", "TabCountry", "Linked.Address")
                    .AddParameter("LANGUAGEID", DbType.Decimal, 5, False, CurrentState.Get("Language"))
 
                    source = .QueryExecuteToTable(True)
                    DirectCast(TabGeographicZoneTables.Columns("COUNTRYID"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = source
                End With

                If Not IsNothing(source) AndAlso source.Rows.Count > 0 Then
                    Caching.SetItem("TabCountry", source)
                End If
            End If 
             If Caching.Exist("EnumRecordStatus") Then
                DirectCast(TabGeographicZoneTables.Columns("RECORDSTATUS"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = Caching.GetItem("EnumRecordStatus")

            Else
                Dim source As DataTable = Nothing

                With New DataManagerFactory("SELECT  ENUMRECORDSTATUS.RECORDSTATUS, ETRANRECORDSTATUS.LANGUAGEID, ETRANRECORDSTATUS.DESCRIPTION FROM COMMON.ENUMRECORDSTATUS ENUMRECORDSTATUS JOIN COMMON.ETRANRECORDSTATUS ETRANRECORDSTATUS ON ETRANRECORDSTATUS.RECORDSTATUS = ENUMRECORDSTATUS.RECORDSTATUS  WHERE ETRANRECORDSTATUS.LANGUAGEID = @:LANGUAGEID ", "EnumRecordStatus", "Linked.Address")
                    .AddParameter("LANGUAGEID", DbType.Decimal, 5, False, CurrentState.Get("Language"))
 
                    source = .QueryExecuteToTable(True)
                    DirectCast(TabGeographicZoneTables.Columns("RECORDSTATUS"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = source
                End With

                If Not IsNothing(source) AndAlso source.Rows.Count > 0 Then
                    Caching.SetItem("EnumRecordStatus", source)
                End If
            End If 
 

                 With New DataManagerFactory("SELECT  TABGEOGRAPHICZONETABLES.COUNTRYID, TABGEOGRAPHICZONETABLES.GEOGRAPHICZONETABLEID, TABGEOGRAPHICZONETABLES.RECORDSTATUS, TABGEOGRAPHICZONETABLES.EFFECTIVEDATE, TABGEOGRAPHICZONETABLES.CANCELLATIONDATE FROM ADDRESS.TABGEOGRAPHICZONETABLES TABGEOGRAPHICZONETABLES  ", "TabGeographicZoneTables", "Linked.Address")                 
                                                   
                                  
                      TabGeographicZoneTables.DataSource = .QueryExecuteToTable(True)
                 End With 
        End If     
    End Sub

    Protected Sub TabGeographicZoneTables_CellEditorInitialize(sender As Object, e As ASPxGridViewEditorEventArgs) Handles TabGeographicZoneTables.CellEditorInitialize
        If TabGeographicZoneTables.IsNewRowEditing Then
            Select Case e.Column.FieldName
                
                
                
                Case "COUNTRYID"                     
                       e.Editor.Focus()               
            End Select

        Else

            Select Case e.Column.FieldName
                Case "COUNTRYID"
     e.Editor.Enabled = False
Case "GEOGRAPHICZONETABLEID"
     e.Editor.Enabled = False
                   
                
                
                Case "RECORDSTATUS"                     
                     e.Editor.Focus()
            End Select
        End If
        
       Select Case e.Column.FieldName
           Case "COUNTRYID"
                 DirectCast(e.Editor, ASPxComboBox).DataBindItems()
                 
           Case "RECORDSTATUS"
     DirectCast(e.Editor, ASPxComboBox).DataBindItems() 

        End Select
    End Sub

    Protected Sub TabGeographicZoneTables_RowInserting(ByVal sender As Object, ByVal e As ASPxDataInsertingEventArgs) Handles TabGeographicZoneTables.RowInserting 
        Dim isNullResult As Boolean = True
        
             With New DataManagerFactory("INSERT INTO ADDRESS.TabGeographicZoneTables (COUNTRYID, GEOGRAPHICZONETABLEID, RECORDSTATUS, EFFECTIVEDATE, CANCELLATIONDATE, CREATIONDATE, CREATORUSERCODE, UPDATEDATE, UPDATEUSERCODE) VALUES (@:COUNTRYID, @:GEOGRAPHICZONETABLEID, @:RECORDSTATUS, @:EFFECTIVEDATE, @:CANCELLATIONDATE, SYSDATE, @:CREATORUSERCODE, SYSDATE, @:UPDATEUSERCODE)", "TabGeographicZoneTables", "Linked.Address")                 
                                                   
                       .AddParameter("COUNTRYID", DbType.Decimal, 0, False, e.NewValues("COUNTRYID"))
.AddParameter("GEOGRAPHICZONETABLEID", DbType.AnsiString, 0, False, e.NewValues("GEOGRAPHICZONETABLEID"))
.AddParameter("RECORDSTATUS", DbType.AnsiString, 0, False, e.NewValues("RECORDSTATUS"))
.AddParameter("EFFECTIVEDATE", DbType.DateTime, 0, False, e.NewValues("EFFECTIVEDATE"))
.AddParameter("CANCELLATIONDATE", DbType.DateTime, 0, (e.NewValues("CANCELLATIONDATE") = Date.MinValue), e.NewValues("CANCELLATIONDATE"))
.AddParameter("CREATORUSERCODE", DbType.Decimal, 0, False, Session("nUsercode"))
.AddParameter("UPDATEUSERCODE", DbType.Decimal, 0, False, Session("nUsercode"))
            
                       .CommandExecute()
                End With
               
        e.Cancel = True
        TabGeographicZoneTables.CancelEdit()
    End Sub

    Protected Sub TabGeographicZoneTables_RowUpdating(ByVal sender As Object, ByVal e As ASPxDataUpdatingEventArgs) Handles TabGeographicZoneTables.RowUpdating
        Dim isNullResult As Boolean = True
          
             With New DataManagerFactory("UPDATE ADDRESS.TabGeographicZoneTables SET RECORDSTATUS = @:RECORDSTATUS, EFFECTIVEDATE = @:EFFECTIVEDATE, CANCELLATIONDATE = @:CANCELLATIONDATE, UPDATEDATE = SYSDATE, UPDATEUSERCODE = @:UPDATEUSERCODE WHERE COUNTRYID = @:COUNTRYID AND GEOGRAPHICZONETABLEID = @:GEOGRAPHICZONETABLEID", "TabGeographicZoneTables", "Linked.Address")                 
                                                   
                       .AddParameter("RECORDSTATUS", DbType.AnsiString, 0, False, e.NewValues("RECORDSTATUS"))
.AddParameter("EFFECTIVEDATE", DbType.DateTime, 0, False, e.NewValues("EFFECTIVEDATE"))
.AddParameter("CANCELLATIONDATE", DbType.DateTime, 0, (e.NewValues("CANCELLATIONDATE") = Date.MinValue), e.NewValues("CANCELLATIONDATE"))
.AddParameter("UPDATEUSERCODE", DbType.Decimal, 0, False, Session("nUsercode"))
.AddParameter("COUNTRYID", DbType.Decimal, 0, False, e.Keys("COUNTRYID"))
.AddParameter("GEOGRAPHICZONETABLEID", DbType.AnsiString, 0, False, e.Keys("GEOGRAPHICZONETABLEID"))
            
                       .CommandExecute()
                End With         
      
         e.Cancel = True
        TabGeographicZoneTables.CancelEdit()
    End Sub
    
    Protected Sub TabGeographicZoneTables_CustomCallback(sender As Object, e As ASPxGridViewCustomCallbackEventArgs) Handles TabGeographicZoneTables.CustomCallback     
           Dim isNullResult As Boolean = True
           
           Select Case e.Parameters.ToString.ToLower
               Case "delete"
                       Dim COUNTRYIDKey As Generic.List(Of Object) = TabGeographicZoneTables.GetSelectedFieldValues("COUNTRYID")
 Dim GEOGRAPHICZONETABLEIDKey As Generic.List(Of Object) = TabGeographicZoneTables.GetSelectedFieldValues("GEOGRAPHICZONETABLEID")
        
               For index As Integer = 0 To COUNTRYIDKey.Count - 1
                  With New DataManagerFactory("DELETE FROM ADDRESS.TabGeographicZoneTables WHERE COUNTRYID = @:COUNTRYID AND GEOGRAPHICZONETABLEID = @:GEOGRAPHICZONETABLEID ", "TabGeographicZoneTables", "Linked.Address")                 
                                                   
               .AddParameter("COUNTRYID", DbType.Decimal, 0, False, COUNTRYIDKey(index))
.AddParameter("GEOGRAPHICZONETABLEID", DbType.AnsiString, 0, False, GEOGRAPHICZONETABLEIDKey(index))
            
               .CommandExecute()
          End With 
                       
              Next           
           
              TabGeographicZoneTables.DataBind()
                 
               Case Else
                   Dim fileName As String = String.Empty
                
                   If e.Parameters.ToString.ToLower.StartsWith("export") Then
                       Dim extension As String = e.Parameters.ToString.ToLower.Split("_")(1)
                       fileName = String.Format(CultureInfo.InvariantCulture, "{0}.{1}", IO.Path.GetRandomFileName, extension)

                       ASPxGridViewExporter.GridViewID = sender.ClientInstanceName

                       Using fs As FileStream = New FileStream(String.Format(CultureInfo.InvariantCulture, "{0}\temp\{1}", Server.MapPath("/"), fileName), FileMode.Create)
                           Select Case extension
                               Case "pdf"
                                   ASPxGridViewExporter.WritePdf(fs)
                               Case "xls"
                                   ASPxGridViewExporter.WriteXls(fs)
                               Case "xlsx"
                                   ASPxGridViewExporter.WriteXlsx(fs)
                               Case "rtf"
                                   ASPxGridViewExporter.WriteRtf(fs)
                               Case Else
                           End Select
                      End Using

                      ASPxWebControl.RedirectOnCallback(String.Format(CultureInfo.InvariantCulture, "~/dropthings/download.ashx?Directory=temp&File={0}", fileName))
                               
                  End If
         End Select
     End Sub
    
    Protected Sub TabGeographicZoneTables_RowValidating(sender As Object, e As ASPxDataValidationEventArgs) Handles TabGeographicZoneTables.RowValidating

        
        If e.Errors.Count > 0 Then
            Dim errorMessage As String = "<ol style='font-weight:lighter'>"

            For Each item As KeyValuePair(Of GridViewColumn, String) In e.Errors
                errorMessage += String.Format("<li>{0}</li>", item.Value)
            Next

           errorMessage += String.Format(CultureInfo.InvariantCulture, "</ol><ul style='font-weight:bold'>{0}</ul>", GetLocalResourceObject("MessageErrorText").ToString)

            e.RowError = errorMessage
        End If

    End Sub

#End Region
 


End Class