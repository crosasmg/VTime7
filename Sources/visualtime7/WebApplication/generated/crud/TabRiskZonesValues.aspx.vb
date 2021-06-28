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

Partial Class Maintenance_TabRiskZonesValues
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

#Region "TabRiskZonesValues Events"
    
    Protected Sub TabRiskZonesValues_CustomColumnDisplayText(sender As Object, e As DevExpress.Web.ASPxGridView.ASPxGridViewColumnDisplayTextEventArgs) Handles TabRiskZonesValues.CustomColumnDisplayText
          Dim data As DataTable
          Dim rows() As DataRow
           
          Select Case e.Column.FieldName
            Case "RISKZONE"
                data = DirectCast(TabRiskZonesValues.Columns("RISKZONE"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource 
                rows = data.Select(String.Format("COUNTRYID = {0} AND RISKZONE = {1}", e.GetFieldValue("COUNTRYID"), e.Value)) 
                If rows.Count > 0 Then 
                    e.DisplayText = rows(0)("DESCRIPTION") 
                End If 

               Case Else              
           End Select
    End Sub
  
    Protected Sub TabRiskZonesValues_DataBinding(ByVal sender As Object, ByVal e As EventArgs) Handles TabRiskZonesValues.DataBinding
        If (Not IsNothing(Request.Params("__CALLBACKID")) AndAlso Request.Params("__CALLBACKID").EndsWith("TabRiskZonesValues")) Or _internalCall Then
                       If Caching.Exist("TabCompany") Then
                DirectCast(TabRiskZonesValues.Columns("COMPANYID"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = Caching.GetItem("TabCompany")

            Else
                Dim source As DataTable = Nothing

                With New DataManagerFactory("SELECT  TABCOMPANY.COMPANYID, TABCOMPANY.RECORDSTATUS, TRANSCOMPANY.LANGUAGEID, TRANSCOMPANY.DESCRIPTION FROM COMMON.TABCOMPANY TABCOMPANY JOIN COMMON.TRANSCOMPANY TRANSCOMPANY ON TRANSCOMPANY.COMPANYID = TABCOMPANY.COMPANYID  WHERE TABCOMPANY.RECORDSTATUS = '1' AND TRANSCOMPANY.LANGUAGEID = @:LANGUAGEID ", "TabCompany", "Linked.Address")
                    .AddParameter("LANGUAGEID", DbType.Decimal, 5, False, CurrentState.Get("Language"))
 
                    source = .QueryExecuteToTable(True)
                    DirectCast(TabRiskZonesValues.Columns("COMPANYID"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = source
                End With

                If Not IsNothing(source) AndAlso source.Rows.Count > 0 Then
                    Caching.SetItem("TabCompany", source)
                End If
            End If 
             If Caching.Exist("TabCountry") Then
                DirectCast(TabRiskZonesValues.Columns("COUNTRYID"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = Caching.GetItem("TabCountry")

            Else
                Dim source As DataTable = Nothing

                With New DataManagerFactory("SELECT  TABCOUNTRY.COUNTRYID, TABCOUNTRY.RECORDSTATUS, TRANSCOUNTRY.LANGUAGEID, TRANSCOUNTRY.DESCRIPTION FROM COMMON.TABCOUNTRY TABCOUNTRY JOIN COMMON.TRANSCOUNTRY TRANSCOUNTRY ON TRANSCOUNTRY.COUNTRYID = TABCOUNTRY.COUNTRYID  WHERE TABCOUNTRY.RECORDSTATUS = '1' AND TRANSCOUNTRY.LANGUAGEID = @:LANGUAGEID ", "TabCountry", "Linked.Address")
                    .AddParameter("LANGUAGEID", DbType.Decimal, 5, False, CurrentState.Get("Language"))
 
                    source = .QueryExecuteToTable(True)
                    DirectCast(TabRiskZonesValues.Columns("COUNTRYID"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = source
                End With

                If Not IsNothing(source) AndAlso source.Rows.Count > 0 Then
                    Caching.SetItem("TabCountry", source)
                End If
            End If 
             If Caching.Exist("EnumRiskZone") Then
                DirectCast(TabRiskZonesValues.Columns("RISKZONE"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = Caching.GetItem("EnumRiskZone")

            Else
                Dim source As DataTable = Nothing

                With New DataManagerFactory("SELECT  ENUMRISKZONE.COMPANYID, ENUMRISKZONE.COUNTRYID, ENUMRISKZONE.RISKZONE, ENUMRISKZONE.RECORDSTATUS, ETRANRISKZONE.LANGUAGEID, ETRANRISKZONE.DESCRIPTION FROM ADDRESS.ENUMRISKZONE ENUMRISKZONE JOIN ADDRESS.ETRANRISKZONE ETRANRISKZONE ON ETRANRISKZONE.COMPANYID = ENUMRISKZONE.COMPANYID  AND ETRANRISKZONE.COUNTRYID = ENUMRISKZONE.COUNTRYID  AND ETRANRISKZONE.RISKZONE = ENUMRISKZONE.RISKZONE  WHERE ENUMRISKZONE.RECORDSTATUS = '1' AND ETRANRISKZONE.LANGUAGEID = @:LANGUAGEID ", "EnumRiskZone", "Linked.Address")
                    .AddParameter("LANGUAGEID", DbType.Decimal, 5, False, CurrentState.Get("Language"))
 
                    source = .QueryExecuteToTable(True)
                    DirectCast(TabRiskZonesValues.Columns("RISKZONE"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = source
                End With

                If Not IsNothing(source) AndAlso source.Rows.Count > 0 Then
                    Caching.SetItem("EnumRiskZone", source)
                End If
            End If 
             If Caching.Exist("EnumRecordStatus") Then
                DirectCast(TabRiskZonesValues.Columns("RECORDSTATUS"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = Caching.GetItem("EnumRecordStatus")

            Else
                Dim source As DataTable = Nothing

                With New DataManagerFactory("SELECT  ENUMRECORDSTATUS.RECORDSTATUS, ETRANRECORDSTATUS.LANGUAGEID, ETRANRECORDSTATUS.DESCRIPTION FROM COMMON.ENUMRECORDSTATUS ENUMRECORDSTATUS JOIN COMMON.ETRANRECORDSTATUS ETRANRECORDSTATUS ON ETRANRECORDSTATUS.RECORDSTATUS = ENUMRECORDSTATUS.RECORDSTATUS  WHERE ETRANRECORDSTATUS.LANGUAGEID = @:LANGUAGEID ", "EnumRecordStatus", "Linked.Address")
                    .AddParameter("LANGUAGEID", DbType.Decimal, 5, False, CurrentState.Get("Language"))
 
                    source = .QueryExecuteToTable(True)
                    DirectCast(TabRiskZonesValues.Columns("RECORDSTATUS"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = source
                End With

                If Not IsNothing(source) AndAlso source.Rows.Count > 0 Then
                    Caching.SetItem("EnumRecordStatus", source)
                End If
            End If 
 

                 With New DataManagerFactory("SELECT  TABRISKZONESVALUES.COMPANYID, TABRISKZONESVALUES.COUNTRYID, TABRISKZONESVALUES.RISKZONE, TABRISKZONESVALUES.RISKVALUE, TABRISKZONESVALUES.RECORDSTATUS FROM ADDRESS.TABRISKZONESVALUES TABRISKZONESVALUES  ", "TabRiskZonesValues", "Linked.Address")                 
                                                   
                                  
                      TabRiskZonesValues.DataSource = .QueryExecuteToTable(True)
                 End With 
        End If     
    End Sub

    Protected Sub TabRiskZonesValues_CellEditorInitialize(sender As Object, e As ASPxGridViewEditorEventArgs) Handles TabRiskZonesValues.CellEditorInitialize
        If TabRiskZonesValues.IsNewRowEditing Then
            Select Case e.Column.FieldName
                
                Case "RISKZONE"
     AddHandler DirectCast(e.Editor, ASPxComboBox).Callback, AddressOf RiskZone_OnCallback 

                
                Case "COMPANYID"                     
                       e.Editor.Focus()               
            End Select

        Else

            Select Case e.Column.FieldName
                Case "COMPANYID"
     e.Editor.Enabled = False
Case "COUNTRYID"
     e.Editor.Enabled = False
Case "RISKZONE"
     e.Editor.Enabled = False
     RiskZone_Fill(e.Editor, TabRiskZonesValues.GetRowValues(e.VisibleIndex, "COUNTRYID")) 
     AddHandler DirectCast(e.Editor, ASPxComboBox).Callback, AddressOf RiskZone_OnCallback 
Case "RISKVALUE"
     e.Editor.Enabled = False
                   
                
                
                Case "RECORDSTATUS"                     
                     e.Editor.Focus()
            End Select
        End If
        
       Select Case e.Column.FieldName
           Case "COMPANYID"
                 DirectCast(e.Editor, ASPxComboBox).DataBindItems()
                 
           Case "COUNTRYID"
     DirectCast(e.Editor, ASPxComboBox).DataBindItems() 
Case "RISKZONE"
     DirectCast(e.Editor, ASPxComboBox).DataBindItems() 
Case "RECORDSTATUS"
     DirectCast(e.Editor, ASPxComboBox).DataBindItems() 

        End Select
    End Sub

    Protected Sub TabRiskZonesValues_RowInserting(ByVal sender As Object, ByVal e As ASPxDataInsertingEventArgs) Handles TabRiskZonesValues.RowInserting 
        Dim isNullResult As Boolean = True
        
             With New DataManagerFactory("INSERT INTO ADDRESS.TabRiskZonesValues (COMPANYID, COUNTRYID, RISKZONE, RISKVALUE, RECORDSTATUS, CREATIONDATE, CREATORUSERCODE, UPDATEDATE, UPDATEUSERCODE) VALUES (@:COMPANYID, @:COUNTRYID, @:RISKZONE, @:RISKVALUE, @:RECORDSTATUS, SYSDATE, @:CREATORUSERCODE, SYSDATE, @:UPDATEUSERCODE)", "TabRiskZonesValues", "Linked.Address")                 
                                                   
                       .AddParameter("COMPANYID", DbType.Decimal, 0, False, e.NewValues("COMPANYID"))
.AddParameter("COUNTRYID", DbType.Decimal, 0, False, e.NewValues("COUNTRYID"))
.AddParameter("RISKZONE", DbType.Decimal, 0, False, e.NewValues("RISKZONE"))
.AddParameter("RISKVALUE", DbType.AnsiString, 0, False, e.NewValues("RISKVALUE"))
.AddParameter("RECORDSTATUS", DbType.AnsiString, 0, False, e.NewValues("RECORDSTATUS"))
.AddParameter("CREATORUSERCODE", DbType.Decimal, 0, False, Session("nUsercode"))
.AddParameter("UPDATEUSERCODE", DbType.Decimal, 0, False, Session("nUsercode"))
            
                       .CommandExecute()
                End With
               
        e.Cancel = True
        TabRiskZonesValues.CancelEdit()
    End Sub

    Protected Sub TabRiskZonesValues_RowUpdating(ByVal sender As Object, ByVal e As ASPxDataUpdatingEventArgs) Handles TabRiskZonesValues.RowUpdating
        Dim isNullResult As Boolean = True
          
             With New DataManagerFactory("UPDATE ADDRESS.TabRiskZonesValues SET RECORDSTATUS = @:RECORDSTATUS, UPDATEDATE = SYSDATE, UPDATEUSERCODE = @:UPDATEUSERCODE WHERE COMPANYID = @:COMPANYID AND COUNTRYID = @:COUNTRYID AND RISKZONE = @:RISKZONE AND RISKVALUE = @:RISKVALUE", "TabRiskZonesValues", "Linked.Address")                 
                                                   
                       .AddParameter("RECORDSTATUS", DbType.AnsiString, 0, False, e.NewValues("RECORDSTATUS"))
.AddParameter("UPDATEUSERCODE", DbType.Decimal, 0, False, Session("nUsercode"))
.AddParameter("COMPANYID", DbType.Decimal, 0, False, e.Keys("COMPANYID"))
.AddParameter("COUNTRYID", DbType.Decimal, 0, False, e.Keys("COUNTRYID"))
.AddParameter("RISKZONE", DbType.Decimal, 0, False, e.Keys("RISKZONE"))
.AddParameter("RISKVALUE", DbType.AnsiString, 0, False, e.Keys("RISKVALUE"))
            
                       .CommandExecute()
                End With         
      
         e.Cancel = True
        TabRiskZonesValues.CancelEdit()
    End Sub
    
    Protected Sub TabRiskZonesValues_CustomCallback(sender As Object, e As ASPxGridViewCustomCallbackEventArgs) Handles TabRiskZonesValues.CustomCallback     
           Dim isNullResult As Boolean = True
           
           Select Case e.Parameters.ToString.ToLower
               Case "delete"
                       Dim COMPANYIDKey As Generic.List(Of Object) = TabRiskZonesValues.GetSelectedFieldValues("COMPANYID")
 Dim COUNTRYIDKey As Generic.List(Of Object) = TabRiskZonesValues.GetSelectedFieldValues("COUNTRYID")
 Dim RISKZONEKey As Generic.List(Of Object) = TabRiskZonesValues.GetSelectedFieldValues("RISKZONE")
 Dim RISKVALUEKey As Generic.List(Of Object) = TabRiskZonesValues.GetSelectedFieldValues("RISKVALUE")
        
               For index As Integer = 0 To COMPANYIDKey.Count - 1
                  With New DataManagerFactory("DELETE FROM ADDRESS.TabRiskZonesValues WHERE COMPANYID = @:COMPANYID AND COUNTRYID = @:COUNTRYID AND RISKZONE = @:RISKZONE AND RISKVALUE = @:RISKVALUE ", "TabRiskZonesValues", "Linked.Address")                 
                                                   
               .AddParameter("COMPANYID", DbType.Decimal, 0, False, COMPANYIDKey(index))
.AddParameter("COUNTRYID", DbType.Decimal, 0, False, COUNTRYIDKey(index))
.AddParameter("RISKZONE", DbType.Decimal, 0, False, RISKZONEKey(index))
.AddParameter("RISKVALUE", DbType.AnsiString, 0, False, RISKVALUEKey(index))
            
               .CommandExecute()
          End With 
                       
              Next           
           
              TabRiskZonesValues.DataBind()
                 
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
    
    Protected Sub TabRiskZonesValues_RowValidating(sender As Object, e As ASPxDataValidationEventArgs) Handles TabRiskZonesValues.RowValidating

        
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
 

#Region "RiskZone Events"

    Private Sub RiskZone_OnCallback(ByVal source As Object, ByVal e As CallbackEventArgsBase)
        RiskZone_Fill(DirectCast(source, ASPxComboBox), e.Parameter)
    End Sub

    Private Sub RiskZone_Fill(control As ASPxComboBox, CountryID As Integer)
          With New DataManagerFactory("SELECT  ENUMRISKZONE.COMPANYID, ENUMRISKZONE.COUNTRYID, ENUMRISKZONE.RISKZONE, ENUMRISKZONE.RECORDSTATUS, ETRANRISKZONE.LANGUAGEID, ETRANRISKZONE.DESCRIPTION FROM ADDRESS.ENUMRISKZONE ENUMRISKZONE JOIN ADDRESS.ETRANRISKZONE ETRANRISKZONE ON ETRANRISKZONE.COMPANYID = ENUMRISKZONE.COMPANYID  AND ETRANRISKZONE.COUNTRYID = ENUMRISKZONE.COUNTRYID  AND ETRANRISKZONE.RISKZONE = ENUMRISKZONE.RISKZONE  WHERE ENUMRISKZONE.RECORDSTATUS = '1' AND ETRANRISKZONE.LANGUAGEID = @:LANGUAGEID  AND ENUMRISKZONE.COUNTRYID = @:DependencyENUMRISKZONECOU", "EnumRiskZone", "Linked.Address")                 
                                                   
               .AddParameter("LANGUAGEID", DbType.Decimal, 5, False, CurrentState.Get("Language"))
.AddParameter("DependencyEnumRiskZoneCou", DbType.Int32, 0, False, CountryID)
  
               
               control.DataSource = .QueryExecuteToTable(True)
               control.DataBindItems()
          End With         
    End Sub
    
#End Region 
 
 
End Class