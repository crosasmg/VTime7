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

Partial Class Maintenance_TabGeographicZoneRelation
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

#Region "TabGeographicZoneRelation Events"
    
    Protected Sub TabGeographicZoneRelation_CustomColumnDisplayText(sender As Object, e As DevExpress.Web.ASPxGridView.ASPxGridViewColumnDisplayTextEventArgs) Handles TabGeographicZoneRelation.CustomColumnDisplayText
          Dim data As DataTable
          Dim rows() As DataRow
           
          Select Case e.Column.FieldName

               Case Else              
           End Select
    End Sub
  
    Protected Sub TabGeographicZoneRelation_DataBinding(ByVal sender As Object, ByVal e As EventArgs) Handles TabGeographicZoneRelation.DataBinding
        If (Not IsNothing(Request.Params("__CALLBACKID")) AndAlso Request.Params("__CALLBACKID").EndsWith("TabGeographicZoneRelation")) Or _internalCall Then
                       If Caching.Exist("TabCountry") Then
                DirectCast(TabGeographicZoneRelation.Columns("COUNTRYID"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = Caching.GetItem("TabCountry")

            Else
                Dim source As DataTable = Nothing

                With New DataManagerFactory("SELECT  TABCOUNTRY.COUNTRYID, TABCOUNTRY.RECORDSTATUS, TRANSCOUNTRY.LANGUAGEID, TRANSCOUNTRY.DESCRIPTION FROM COMMON.TABCOUNTRY TABCOUNTRY JOIN COMMON.TRANSCOUNTRY TRANSCOUNTRY ON TRANSCOUNTRY.COUNTRYID = TABCOUNTRY.COUNTRYID  WHERE TABCOUNTRY.RECORDSTATUS = '1' AND TRANSCOUNTRY.LANGUAGEID = @:LANGUAGEID ", "TabCountry", "Linked.Address")
                    .AddParameter("LANGUAGEID", DbType.Decimal, 5, False, CurrentState.Get("Language"))
 
                    source = .QueryExecuteToTable(True)
                    DirectCast(TabGeographicZoneRelation.Columns("COUNTRYID"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = source
                End With

                If Not IsNothing(source) AndAlso source.Rows.Count > 0 Then
                    Caching.SetItem("TabCountry", source)
                End If
            End If 
             If Caching.Exist("TabGeographicZoneNames") Then
                DirectCast(TabGeographicZoneRelation.Columns("GEOGRAPHICZONELEVELID"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = Caching.GetItem("TabGeographicZoneNames")

            Else
                Dim source As DataTable = Nothing

                With New DataManagerFactory("SELECT  TABGEOGRAPHICZONENAMES.COUNTRYID, TABGEOGRAPHICZONENAMES.GEOGRAPHICZONELEVELID, TABGEOGRAPHICZONENAMES.RECORDSTATUS, TRANSGEOGRAPHICZONENAMES.LANGUAGEID, TRANSGEOGRAPHICZONENAMES.DESCRIPTION FROM ADDRESS.TABGEOGRAPHICZONENAMES TABGEOGRAPHICZONENAMES JOIN ADDRESS.TRANSGEOGRAPHICZONENAMES TRANSGEOGRAPHICZONENAMES ON TRANSGEOGRAPHICZONENAMES.COUNTRYID = TABGEOGRAPHICZONENAMES.COUNTRYID  AND TRANSGEOGRAPHICZONENAMES.GEOGRAPHICZONELEVELID = TABGEOGRAPHICZONENAMES.GEOGRAPHICZONELEVELID  WHERE TABGEOGRAPHICZONENAMES.RECORDSTATUS = '1' AND TRANSGEOGRAPHICZONENAMES.LANGUAGEID = @:LANGUAGEID ", "TabGeographicZoneNames", "Linked.Address")
                    .AddParameter("LANGUAGEID", DbType.Decimal, 5, False, CurrentState.Get("Language"))
 
                    source = .QueryExecuteToTable(True)
                    DirectCast(TabGeographicZoneRelation.Columns("GEOGRAPHICZONELEVELID"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = source
                End With

                If Not IsNothing(source) AndAlso source.Rows.Count > 0 Then
                    Caching.SetItem("TabGeographicZoneNames", source)
                End If
            End If 
             If Caching.Exist("TabGeographicZone") Then
                DirectCast(TabGeographicZoneRelation.Columns("GEOGRAPHICZONEID"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = Caching.GetItem("TabGeographicZone")

            Else
                Dim source As DataTable = Nothing

                With New DataManagerFactory("SELECT  TABGEOGRAPHICZONE.COUNTRYID, TABGEOGRAPHICZONE.GEOGRAPHICZONETABLEID, TABGEOGRAPHICZONE.GEOGRAPHICZONELEVELID, TABGEOGRAPHICZONE.GEOGRAPHICZONEID, TABGEOGRAPHICZONE.RECORDSTATUS, TRANSGEOGRAPHICZONE.LANGUAGEID, TRANSGEOGRAPHICZONE.DESCRIPTION FROM ADDRESS.TABGEOGRAPHICZONE TABGEOGRAPHICZONE JOIN ADDRESS.TRANSGEOGRAPHICZONE TRANSGEOGRAPHICZONE ON TRANSGEOGRAPHICZONE.COUNTRYID = TABGEOGRAPHICZONE.COUNTRYID  AND TRANSGEOGRAPHICZONE.GEOGRAPHICZONETABLEID = TABGEOGRAPHICZONE.GEOGRAPHICZONETABLEID  AND TRANSGEOGRAPHICZONE.GEOGRAPHICZONELEVELID = TABGEOGRAPHICZONE.GEOGRAPHICZONELEVELID  AND TRANSGEOGRAPHICZONE.GEOGRAPHICZONEID = TABGEOGRAPHICZONE.GEOGRAPHICZONEID  WHERE TABGEOGRAPHICZONE.RECORDSTATUS = '1' AND TRANSGEOGRAPHICZONE.LANGUAGEID = @:LANGUAGEID ", "TabGeographicZone", "Linked.Address")
                    .AddParameter("LANGUAGEID", DbType.Decimal, 5, False, CurrentState.Get("Language"))
 
                    source = .QueryExecuteToTable(True)
                    DirectCast(TabGeographicZoneRelation.Columns("GEOGRAPHICZONEID"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = source
                End With

                If Not IsNothing(source) AndAlso source.Rows.Count > 0 Then
                    Caching.SetItem("TabGeographicZone", source)
                End If
            End If 
             If Caching.Exist("TabGeographicZoneNames") Then
                DirectCast(TabGeographicZoneRelation.Columns("GEOGRAPHICZONELEVELCHILDID"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = Caching.GetItem("TabGeographicZoneNames")

            Else
                Dim source As DataTable = Nothing

                With New DataManagerFactory("SELECT  TABGEOGRAPHICZONENAMES.COUNTRYID, TABGEOGRAPHICZONENAMES.GEOGRAPHICZONELEVELID, TABGEOGRAPHICZONENAMES.RECORDSTATUS, TRANSGEOGRAPHICZONENAMES.LANGUAGEID, TRANSGEOGRAPHICZONENAMES.DESCRIPTION FROM ADDRESS.TABGEOGRAPHICZONENAMES TABGEOGRAPHICZONENAMES JOIN ADDRESS.TRANSGEOGRAPHICZONENAMES TRANSGEOGRAPHICZONENAMES ON TRANSGEOGRAPHICZONENAMES.COUNTRYID = TABGEOGRAPHICZONENAMES.COUNTRYID  AND TRANSGEOGRAPHICZONENAMES.GEOGRAPHICZONELEVELID = TABGEOGRAPHICZONENAMES.GEOGRAPHICZONELEVELID  WHERE TABGEOGRAPHICZONENAMES.RECORDSTATUS = '1' AND TRANSGEOGRAPHICZONENAMES.LANGUAGEID = @:LANGUAGEID ", "TabGeographicZoneNames", "Linked.Address")
                    .AddParameter("LANGUAGEID", DbType.Decimal, 5, False, CurrentState.Get("Language"))
 
                    source = .QueryExecuteToTable(True)
                    DirectCast(TabGeographicZoneRelation.Columns("GEOGRAPHICZONELEVELCHILDID"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = source
                End With

                If Not IsNothing(source) AndAlso source.Rows.Count > 0 Then
                    Caching.SetItem("TabGeographicZoneNames", source)
                End If
            End If 
             If Caching.Exist("TabGeographicZone") Then
                DirectCast(TabGeographicZoneRelation.Columns("GEOGRAPHICZONECHILDID"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = Caching.GetItem("TabGeographicZone")

            Else
                Dim source As DataTable = Nothing

                With New DataManagerFactory("SELECT  TABGEOGRAPHICZONE.COUNTRYID, TABGEOGRAPHICZONE.GEOGRAPHICZONETABLEID, TABGEOGRAPHICZONE.GEOGRAPHICZONELEVELID, TABGEOGRAPHICZONE.GEOGRAPHICZONEID, TABGEOGRAPHICZONE.RECORDSTATUS, TRANSGEOGRAPHICZONE.LANGUAGEID, TRANSGEOGRAPHICZONE.DESCRIPTION FROM ADDRESS.TABGEOGRAPHICZONE TABGEOGRAPHICZONE JOIN ADDRESS.TRANSGEOGRAPHICZONE TRANSGEOGRAPHICZONE ON TRANSGEOGRAPHICZONE.COUNTRYID = TABGEOGRAPHICZONE.COUNTRYID  AND TRANSGEOGRAPHICZONE.GEOGRAPHICZONETABLEID = TABGEOGRAPHICZONE.GEOGRAPHICZONETABLEID  AND TRANSGEOGRAPHICZONE.GEOGRAPHICZONELEVELID = TABGEOGRAPHICZONE.GEOGRAPHICZONELEVELID  AND TRANSGEOGRAPHICZONE.GEOGRAPHICZONEID = TABGEOGRAPHICZONE.GEOGRAPHICZONEID  WHERE TABGEOGRAPHICZONE.RECORDSTATUS = '1' AND TRANSGEOGRAPHICZONE.LANGUAGEID = @:LANGUAGEID ", "TabGeographicZone", "Linked.Address")
                    .AddParameter("LANGUAGEID", DbType.Decimal, 5, False, CurrentState.Get("Language"))
 
                    source = .QueryExecuteToTable(True)
                    DirectCast(TabGeographicZoneRelation.Columns("GEOGRAPHICZONECHILDID"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = source
                End With

                If Not IsNothing(source) AndAlso source.Rows.Count > 0 Then
                    Caching.SetItem("TabGeographicZone", source)
                End If
            End If 
             If Caching.Exist("EnumRecordStatus") Then
                DirectCast(TabGeographicZoneRelation.Columns("RECORDSTATUS"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = Caching.GetItem("EnumRecordStatus")

            Else
                Dim source As DataTable = Nothing

                With New DataManagerFactory("SELECT  ENUMRECORDSTATUS.RECORDSTATUS, ETRANRECORDSTATUS.LANGUAGEID, ETRANRECORDSTATUS.DESCRIPTION FROM COMMON.ENUMRECORDSTATUS ENUMRECORDSTATUS JOIN COMMON.ETRANRECORDSTATUS ETRANRECORDSTATUS ON ETRANRECORDSTATUS.RECORDSTATUS = ENUMRECORDSTATUS.RECORDSTATUS  WHERE ETRANRECORDSTATUS.LANGUAGEID = @:LANGUAGEID ", "EnumRecordStatus", "Linked.Address")
                    .AddParameter("LANGUAGEID", DbType.Decimal, 5, False, CurrentState.Get("Language"))
 
                    source = .QueryExecuteToTable(True)
                    DirectCast(TabGeographicZoneRelation.Columns("RECORDSTATUS"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = source
                End With

                If Not IsNothing(source) AndAlso source.Rows.Count > 0 Then
                    Caching.SetItem("EnumRecordStatus", source)
                End If
            End If 
 

                 With New DataManagerFactory("SELECT  TABGEOGRAPHICZONERELATION.COUNTRYID, TABGEOGRAPHICZONERELATION.GEOGRAPHICZONETABLEID, TABGEOGRAPHICZONERELATION.GEOGRAPHICZONELEVELID, TABGEOGRAPHICZONERELATION.GEOGRAPHICZONEID, TABGEOGRAPHICZONERELATION.GEOGRAPHICZONELEVELCHILDID, TABGEOGRAPHICZONERELATION.GEOGRAPHICZONECHILDID, TABGEOGRAPHICZONERELATION.RECORDSTATUS FROM ADDRESS.TABGEOGRAPHICZONERELATION TABGEOGRAPHICZONERELATION  ", "TabGeographicZoneRelation", "Linked.Address")                 
                                                   
                                  
                      TabGeographicZoneRelation.DataSource = .QueryExecuteToTable(True)
                 End With 
        End If     
    End Sub

    Protected Sub TabGeographicZoneRelation_CellEditorInitialize(sender As Object, e As ASPxGridViewEditorEventArgs) Handles TabGeographicZoneRelation.CellEditorInitialize
        If TabGeographicZoneRelation.IsNewRowEditing Then
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
Case "GEOGRAPHICZONELEVELID"
     e.Editor.Enabled = False
Case "GEOGRAPHICZONEID"
     e.Editor.Enabled = False
Case "GEOGRAPHICZONELEVELCHILDID"
     e.Editor.Enabled = False
Case "GEOGRAPHICZONECHILDID"
     e.Editor.Enabled = False
                   
                
                
                Case "RECORDSTATUS"                     
                     e.Editor.Focus()
            End Select
        End If
        
       Select Case e.Column.FieldName
           Case "COUNTRYID"
                 DirectCast(e.Editor, ASPxComboBox).DataBindItems()
                 
           Case "GEOGRAPHICZONELEVELID"
     DirectCast(e.Editor, ASPxComboBox).DataBindItems() 
Case "GEOGRAPHICZONEID"
     DirectCast(e.Editor, ASPxComboBox).DataBindItems() 
Case "GEOGRAPHICZONELEVELCHILDID"
     DirectCast(e.Editor, ASPxComboBox).DataBindItems() 
Case "GEOGRAPHICZONECHILDID"
     DirectCast(e.Editor, ASPxComboBox).DataBindItems() 
Case "RECORDSTATUS"
     DirectCast(e.Editor, ASPxComboBox).DataBindItems() 

        End Select
    End Sub

    Protected Sub TabGeographicZoneRelation_RowInserting(ByVal sender As Object, ByVal e As ASPxDataInsertingEventArgs) Handles TabGeographicZoneRelation.RowInserting 
        Dim isNullResult As Boolean = True
        
             With New DataManagerFactory("INSERT INTO ADDRESS.TabGeographicZoneRelation (COUNTRYID, GEOGRAPHICZONETABLEID, GEOGRAPHICZONELEVELID, GEOGRAPHICZONEID, GEOGRAPHICZONELEVELCHILDID, GEOGRAPHICZONECHILDID, RECORDSTATUS, CREATIONDATE, CREATORUSERCODE, UPDATEDATE, UPDATEUSERCODE) VALUES (@:COUNTRYID, @:GEOGRAPHICZONETABLEID, @:GEOGRAPHICZONELEVELID, @:GEOGRAPHICZONEID, @:GEOGRAPHICZONELEVELCHILDID, @:GEOGRAPHICZONECHILDID, @:RECORDSTATUS, SYSDATE, @:CREATORUSERCODE, SYSDATE, @:UPDATEUSERCODE)", "TabGeographicZoneRelation", "Linked.Address")                 
                                                   
                       .AddParameter("COUNTRYID", DbType.Decimal, 0, False, e.NewValues("COUNTRYID"))
.AddParameter("GEOGRAPHICZONETABLEID", DbType.AnsiString, 0, False, e.NewValues("GEOGRAPHICZONETABLEID"))
.AddParameter("GEOGRAPHICZONELEVELID", DbType.Decimal, 0, False, e.NewValues("GEOGRAPHICZONELEVELID"))
.AddParameter("GEOGRAPHICZONEID", DbType.AnsiString, 0, False, e.NewValues("GEOGRAPHICZONEID"))
.AddParameter("GEOGRAPHICZONELEVELCHILDID", DbType.Decimal, 0, False, e.NewValues("GEOGRAPHICZONELEVELCHILDID"))
.AddParameter("GEOGRAPHICZONECHILDID", DbType.AnsiString, 0, False, e.NewValues("GEOGRAPHICZONECHILDID"))
.AddParameter("RECORDSTATUS", DbType.AnsiString, 0, False, e.NewValues("RECORDSTATUS"))
.AddParameter("CREATORUSERCODE", DbType.Decimal, 0, False, Session("nUsercode"))
.AddParameter("UPDATEUSERCODE", DbType.Decimal, 0, False, Session("nUsercode"))
            
                       .CommandExecute()
                End With
               
        e.Cancel = True
        TabGeographicZoneRelation.CancelEdit()
    End Sub

    Protected Sub TabGeographicZoneRelation_RowUpdating(ByVal sender As Object, ByVal e As ASPxDataUpdatingEventArgs) Handles TabGeographicZoneRelation.RowUpdating
        Dim isNullResult As Boolean = True
          
             With New DataManagerFactory("UPDATE ADDRESS.TabGeographicZoneRelation SET RECORDSTATUS = @:RECORDSTATUS, UPDATEDATE = SYSDATE, UPDATEUSERCODE = @:UPDATEUSERCODE WHERE COUNTRYID = @:COUNTRYID AND GEOGRAPHICZONETABLEID = @:GEOGRAPHICZONETABLEID AND GEOGRAPHICZONELEVELID = @:GEOGRAPHICZONELEVELID AND GEOGRAPHICZONEID = @:GEOGRAPHICZONEID AND GEOGRAPHICZONELEVELCHILDID = @:GEOGRAPHICZONELEVELCHILDID AND GEOGRAPHICZONECHILDID = @:GEOGRAPHICZONECHILDID", "TabGeographicZoneRelation", "Linked.Address")                 
                                                   
                       .AddParameter("RECORDSTATUS", DbType.AnsiString, 0, False, e.NewValues("RECORDSTATUS"))
.AddParameter("UPDATEUSERCODE", DbType.Decimal, 0, False, Session("nUsercode"))
.AddParameter("COUNTRYID", DbType.Decimal, 0, False, e.Keys("COUNTRYID"))
.AddParameter("GEOGRAPHICZONETABLEID", DbType.AnsiString, 0, False, e.Keys("GEOGRAPHICZONETABLEID"))
.AddParameter("GEOGRAPHICZONELEVELID", DbType.Decimal, 0, False, e.Keys("GEOGRAPHICZONELEVELID"))
.AddParameter("GEOGRAPHICZONEID", DbType.AnsiString, 0, False, e.Keys("GEOGRAPHICZONEID"))
.AddParameter("GEOGRAPHICZONELEVELCHILDID", DbType.Decimal, 0, False, e.Keys("GEOGRAPHICZONELEVELCHILDID"))
.AddParameter("GEOGRAPHICZONECHILDID", DbType.AnsiString, 0, False, e.Keys("GEOGRAPHICZONECHILDID"))
            
                       .CommandExecute()
                End With         
      
         e.Cancel = True
        TabGeographicZoneRelation.CancelEdit()
    End Sub
    
    Protected Sub TabGeographicZoneRelation_CustomCallback(sender As Object, e As ASPxGridViewCustomCallbackEventArgs) Handles TabGeographicZoneRelation.CustomCallback     
           Dim isNullResult As Boolean = True
           
           Select Case e.Parameters.ToString.ToLower
               Case "delete"
                       Dim COUNTRYIDKey As Generic.List(Of Object) = TabGeographicZoneRelation.GetSelectedFieldValues("COUNTRYID")
 Dim GEOGRAPHICZONETABLEIDKey As Generic.List(Of Object) = TabGeographicZoneRelation.GetSelectedFieldValues("GEOGRAPHICZONETABLEID")
 Dim GEOGRAPHICZONELEVELIDKey As Generic.List(Of Object) = TabGeographicZoneRelation.GetSelectedFieldValues("GEOGRAPHICZONELEVELID")
 Dim GEOGRAPHICZONEIDKey As Generic.List(Of Object) = TabGeographicZoneRelation.GetSelectedFieldValues("GEOGRAPHICZONEID")
 Dim GEOGRAPHICZONELEVELCHILDIDKey As Generic.List(Of Object) = TabGeographicZoneRelation.GetSelectedFieldValues("GEOGRAPHICZONELEVELCHILDID")
 Dim GEOGRAPHICZONECHILDIDKey As Generic.List(Of Object) = TabGeographicZoneRelation.GetSelectedFieldValues("GEOGRAPHICZONECHILDID")
        
               For index As Integer = 0 To COUNTRYIDKey.Count - 1
                  With New DataManagerFactory("DELETE FROM ADDRESS.TabGeographicZoneRelation WHERE COUNTRYID = @:COUNTRYID AND GEOGRAPHICZONETABLEID = @:GEOGRAPHICZONETABLEID AND GEOGRAPHICZONELEVELID = @:GEOGRAPHICZONELEVELID AND GEOGRAPHICZONEID = @:GEOGRAPHICZONEID AND GEOGRAPHICZONELEVELCHILDID = @:GEOGRAPHICZONELEVELCHILDID AND GEOGRAPHICZONECHILDID = @:GEOGRAPHICZONECHILDID ", "TabGeographicZoneRelation", "Linked.Address")                 
                                                   
               .AddParameter("COUNTRYID", DbType.Decimal, 0, False, COUNTRYIDKey(index))
.AddParameter("GEOGRAPHICZONETABLEID", DbType.AnsiString, 0, False, GEOGRAPHICZONETABLEIDKey(index))
.AddParameter("GEOGRAPHICZONELEVELID", DbType.Decimal, 0, False, GEOGRAPHICZONELEVELIDKey(index))
.AddParameter("GEOGRAPHICZONEID", DbType.AnsiString, 0, False, GEOGRAPHICZONEIDKey(index))
.AddParameter("GEOGRAPHICZONELEVELCHILDID", DbType.Decimal, 0, False, GEOGRAPHICZONELEVELCHILDIDKey(index))
.AddParameter("GEOGRAPHICZONECHILDID", DbType.AnsiString, 0, False, GEOGRAPHICZONECHILDIDKey(index))
            
               .CommandExecute()
          End With 
                       
              Next           
           
              TabGeographicZoneRelation.DataBind()
                 
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
    
    Protected Sub TabGeographicZoneRelation_RowValidating(sender As Object, e As ASPxDataValidationEventArgs) Handles TabGeographicZoneRelation.RowValidating

        
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