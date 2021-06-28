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

Partial Class Maintenance_EnumRiskZone
    Inherits PageBase

#Region "Private fields"

    Private _internalCall As Boolean

#End Region

#Region "Events Page"

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not IsCallback AndAlso Not IsPostBack Then
            Dim newItem As DevExpress.Web.ASPxMenu.MenuItem

            For Each languageItem In InMotionGIT.Common.Proxy.Helpers.Language.LanguageToDictionary

                newItem = New DevExpress.Web.ASPxMenu.MenuItem

                With newItem
                    .Name = String.Format(CultureInfo.InvariantCulture, "{0}Item", languageItem.Value)
                    .Text = languageItem.Value
                    .Image.Url = String.Format(CultureInfo.InvariantCulture, "/images/16x16/Flags/{0}.png", languageItem.Value.ToLower)

                   If languageItem.Key = LanguageId Then
                        MainMenu.Items(4).Text = String.Format(CultureInfo.InvariantCulture, "{0} {1}", GetLocalResourceObject("LanguageItemMenu").ToString(), languageItem.Value)
                        MainMenu.Items(4).Image.Url = String.Format(CultureInfo.InvariantCulture, "/images/16x16/Flags/{0}.png", languageItem.Value.ToLower)

                        .Visible = False
                    Else
                        .Visible = True
                    End If
                End With

                MainMenu.Items(4).Items.Add(newItem)
            Next

        End If
        
        If Not CurrentState.Contains("LanguageId") Then
            CurrentState.Add("LanguageId", LanguageId)
        End If
    End Sub

#End Region

#Region "MainMenu Events"

    Protected Sub MainMenu_ItemClick(source As Object, e As DevExpress.Web.ASPxMenu.MenuItemEventArgs) Handles MainMenu.ItemClick
        If String.Equals(e.Item.Parent.Name, "LanguageItem", StringComparison.CurrentCultureIgnoreCase) Then
            e.Item.Parent.Text = String.Format(CultureInfo.InvariantCulture, "{0} {1}", GetLocalResourceObject("LanguageItemMenu").ToString(), e.Item.Text)
            e.Item.Parent.Image.Url = String.Format(CultureInfo.InvariantCulture, "/images/16x16/Flags/{0}.png", e.Item.Text.ToLower)

            e.Item.Visible = False

            For Each item As DevExpress.Web.ASPxMenu.MenuItem In e.Item.Parent.Items
                If Not String.Equals(item.Text, e.Item.Text, StringComparison.CurrentCultureIgnoreCase) Then
                    item.Visible = True
                End If
            Next

            CurrentState.Set("LanguageId", InMotionGIT.Common.Proxy.Helpers.Language.GetLanguageIdCurrentContext())
            _internalCall = True

            EnumRiskZone_Grid.DataBind()
        End If      
    End Sub

#End Region

#Region "Controls Events"

         
    
 

#End Region

#Region "EnumRiskZone_Grid Events"
    
    Protected Sub EnumRiskZone_Grid_CustomColumnDisplayText(sender As Object, e As DevExpress.Web.ASPxGridView.ASPxGridViewColumnDisplayTextEventArgs) Handles EnumRiskZone_Grid.CustomColumnDisplayText
          Dim data As DataTable
          Dim rows() As DataRow
           
          Select Case e.Column.FieldName

               Case Else              
           End Select
    End Sub
  
    Protected Sub EnumRiskZone_Grid_DataBinding(ByVal sender As Object, ByVal e As EventArgs) Handles EnumRiskZone_Grid.DataBinding
        If (Not IsNothing(Request.Params("__CALLBACKID")) AndAlso Request.Params("__CALLBACKID").EndsWith("EnumRiskZone_Grid")) Or _internalCall Then
                       If Caching.Exist("TabCompany") Then
                DirectCast(EnumRiskZone_Grid.Columns("COMPANYID"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = Caching.GetItem("TabCompany")

            Else
                Dim source As DataTable = Nothing

                With New DataManagerFactory("SELECT  TABCOMPANY.COMPANYID, TABCOMPANY.RECORDSTATUS, TRANSCOMPANY.LANGUAGEID, TRANSCOMPANY.DESCRIPTION FROM COMMON.TABCOMPANY TABCOMPANY JOIN COMMON.TRANSCOMPANY TRANSCOMPANY ON TRANSCOMPANY.COMPANYID = TABCOMPANY.COMPANYID  WHERE TABCOMPANY.RECORDSTATUS = '1' AND TRANSCOMPANY.LANGUAGEID = @:LANGUAGEID ", "TabCompany", "Linked.Address")
                    .AddParameter("LANGUAGEID", DbType.Decimal, 5, False, CurrentState.Get("Language"))
 
                    source = .QueryExecuteToTable(True)
                    DirectCast(EnumRiskZone_Grid.Columns("COMPANYID"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = source
                End With

                If Not IsNothing(source) AndAlso source.Rows.Count > 0 Then
                    Caching.SetItem("TabCompany", source)
                End If
            End If 
             If Caching.Exist("TabCountry") Then
                DirectCast(EnumRiskZone_Grid.Columns("COUNTRYID"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = Caching.GetItem("TabCountry")

            Else
                Dim source As DataTable = Nothing

                With New DataManagerFactory("SELECT  TABCOUNTRY.COUNTRYID, TABCOUNTRY.RECORDSTATUS, TRANSCOUNTRY.LANGUAGEID, TRANSCOUNTRY.DESCRIPTION FROM COMMON.TABCOUNTRY TABCOUNTRY JOIN COMMON.TRANSCOUNTRY TRANSCOUNTRY ON TRANSCOUNTRY.COUNTRYID = TABCOUNTRY.COUNTRYID  WHERE TABCOUNTRY.RECORDSTATUS = '1' AND TRANSCOUNTRY.LANGUAGEID = @:LANGUAGEID ", "TabCountry", "Linked.Address")
                    .AddParameter("LANGUAGEID", DbType.Decimal, 5, False, CurrentState.Get("Language"))
 
                    source = .QueryExecuteToTable(True)
                    DirectCast(EnumRiskZone_Grid.Columns("COUNTRYID"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = source
                End With

                If Not IsNothing(source) AndAlso source.Rows.Count > 0 Then
                    Caching.SetItem("TabCountry", source)
                End If
            End If 
             If Caching.Exist("EnumRecordStatus") Then
                DirectCast(EnumRiskZone_Grid.Columns("RECORDSTATUS"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = Caching.GetItem("EnumRecordStatus")

            Else
                Dim source As DataTable = Nothing

                With New DataManagerFactory("SELECT  ENUMRECORDSTATUS.RECORDSTATUS, ETRANRECORDSTATUS.LANGUAGEID, ETRANRECORDSTATUS.DESCRIPTION FROM COMMON.ENUMRECORDSTATUS ENUMRECORDSTATUS JOIN COMMON.ETRANRECORDSTATUS ETRANRECORDSTATUS ON ETRANRECORDSTATUS.RECORDSTATUS = ENUMRECORDSTATUS.RECORDSTATUS  WHERE ETRANRECORDSTATUS.LANGUAGEID = @:LANGUAGEID ", "EnumRecordStatus", "Linked.Address")
                    .AddParameter("LANGUAGEID", DbType.Decimal, 5, False, CurrentState.Get("Language"))
 
                    source = .QueryExecuteToTable(True)
                    DirectCast(EnumRiskZone_Grid.Columns("RECORDSTATUS"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = source
                End With

                If Not IsNothing(source) AndAlso source.Rows.Count > 0 Then
                    Caching.SetItem("EnumRecordStatus", source)
                End If
            End If 
 

                 With New DataManagerFactory("SELECT  ENUMRISKZONE.COMPANYID, ENUMRISKZONE.COUNTRYID, ENUMRISKZONE.RISKZONE, ENUMRISKZONE.RECORDSTATUS, ETRANRISKZONE.COMPANYID, ETRANRISKZONE.COUNTRYID, ETRANRISKZONE.RISKZONE, ETRANRISKZONE.LANGUAGEID, ETRANRISKZONE.DESCRIPTION, ETRANRISKZONE.SHORTDESCRIPTION FROM ADDRESS.ENUMRISKZONE ENUMRISKZONE JOIN ADDRESS.ETRANRISKZONE ETRANRISKZONE ON ETRANRISKZONE.COMPANYID = ENUMRISKZONE.COMPANYID  AND ETRANRISKZONE.COUNTRYID = ENUMRISKZONE.COUNTRYID  AND ETRANRISKZONE.RISKZONE = ENUMRISKZONE.RISKZONE  WHERE ETRANRISKZONE.LANGUAGEID = @:LANGUAGEID", "EnumRiskZone", "Linked.Address")                 
                                                   
                      .AddParameter("LANGUAGEID", DbType.Decimal, 5, False, CurrentState.Get("Language"))
            
                      EnumRiskZone_Grid.DataSource = .QueryExecuteToTable(True)
                 End With 
        End If     
    End Sub

    Protected Sub EnumRiskZone_Grid_CellEditorInitialize(sender As Object, e As ASPxGridViewEditorEventArgs) Handles EnumRiskZone_Grid.CellEditorInitialize
        If EnumRiskZone_Grid.IsNewRowEditing Then
            Select Case e.Column.FieldName
                
                
                
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
                   
                
                
                Case "RECORDSTATUS"                     
                     e.Editor.Focus()
            End Select
        End If
        
       Select Case e.Column.FieldName
           Case "COMPANYID"
                 DirectCast(e.Editor, ASPxComboBox).DataBindItems()
                 
           Case "COUNTRYID"
     DirectCast(e.Editor, ASPxComboBox).DataBindItems() 
Case "RECORDSTATUS"
     DirectCast(e.Editor, ASPxComboBox).DataBindItems() 

        End Select
    End Sub

    Protected Sub EnumRiskZone_Grid_RowInserting(ByVal sender As Object, ByVal e As ASPxDataInsertingEventArgs) Handles EnumRiskZone_Grid.RowInserting 
        Dim isNullResult As Boolean = True
        
             With New DataManagerFactory("INSERT INTO ADDRESS.EnumRiskZone (COMPANYID, COUNTRYID, RISKZONE, RECORDSTATUS, CREATIONDATE, CREATORUSERCODE, UPDATEDATE, UPDATEUSERCODE) VALUES (@:COMPANYID, @:COUNTRYID, @:RISKZONE, @:RECORDSTATUS, SYSDATE, @:CREATORUSERCODE, SYSDATE, @:UPDATEUSERCODE)", "EnumRiskZone", "Linked.Address")                 
                                                   
                       .AddParameter("COMPANYID", DbType.Decimal, 0, False, e.NewValues("COMPANYID"))
.AddParameter("COUNTRYID", DbType.Decimal, 0, False, e.NewValues("COUNTRYID"))
.AddParameter("RISKZONE", DbType.Decimal, 0, False, e.NewValues("RISKZONE"))
.AddParameter("RECORDSTATUS", DbType.AnsiString, 0, False, e.NewValues("RECORDSTATUS"))
.AddParameter("CREATORUSERCODE", DbType.Decimal, 0, False, Session("nUsercode"))
.AddParameter("UPDATEUSERCODE", DbType.Decimal, 0, False, Session("nUsercode"))
            
                       .CommandExecute()
                End With
        For Each languageItem In InMotionGIT.Common.Proxy.Helpers.Language.LanguageToDictionary
            With New DataManagerFactory("INSERT INTO ADDRESS.ETranRiskZone (COMPANYID, COUNTRYID, RISKZONE, LANGUAGEID, DESCRIPTION, SHORTDESCRIPTION, CREATIONDATE, CREATORUSERCODE, UPDATEDATE, UPDATEUSERCODE) VALUES (@:COMPANYID, @:COUNTRYID, @:RISKZONE, @:LANGUAGEID, @:DESCRIPTION, @:SHORTDESCRIPTION, SYSDATE, @:CREATORUSERCODE, SYSDATE, @:UPDATEUSERCODE)", "ETranRiskZone", "Linked.Address")

                .AddParameter("COMPANYID", DbType.Decimal, 0, False, e.NewValues("COMPANYID"))
                .AddParameter("COUNTRYID", DbType.Decimal, 0, False, e.NewValues("COUNTRYID"))
                .AddParameter("RISKZONE", DbType.Decimal, 0, False, e.NewValues("RISKZONE"))
                .AddParameter("LANGUAGEID", DbType.Decimal, 0, False, languageItem.Key)
                .AddParameter("DESCRIPTION", DbType.AnsiString, 0, (e.NewValues("DESCRIPTION") = String.Empty), e.NewValues("DESCRIPTION"))
                .AddParameter("SHORTDESCRIPTION", DbType.AnsiString, 0, (e.NewValues("SHORTDESCRIPTION") = String.Empty), e.NewValues("SHORTDESCRIPTION"))
                .AddParameter("CREATORUSERCODE", DbType.Decimal, 0, False, Session("nUsercode"))
                .AddParameter("UPDATEUSERCODE", DbType.Decimal, 0, False, Session("nUsercode"))

                .CommandExecute()
            End With
        Next

               
        e.Cancel = True
        EnumRiskZone_Grid.CancelEdit()
    End Sub

    Protected Sub EnumRiskZone_Grid_RowUpdating(ByVal sender As Object, ByVal e As ASPxDataUpdatingEventArgs) Handles EnumRiskZone_Grid.RowUpdating
        Dim isNullResult As Boolean = True
          
             With New DataManagerFactory("UPDATE ADDRESS.EnumRiskZone SET RECORDSTATUS = @:RECORDSTATUS, UPDATEDATE = SYSDATE, UPDATEUSERCODE = @:UPDATEUSERCODE WHERE COMPANYID = @:COMPANYID AND COUNTRYID = @:COUNTRYID AND RISKZONE = @:RISKZONE", "EnumRiskZone", "Linked.Address")                 
                                                   
                       .AddParameter("RECORDSTATUS", DbType.AnsiString, 0, False, e.NewValues("RECORDSTATUS"))
.AddParameter("UPDATEUSERCODE", DbType.Decimal, 0, False, Session("nUsercode"))
.AddParameter("COMPANYID", DbType.Decimal, 0, False, e.Keys("COMPANYID"))
.AddParameter("COUNTRYID", DbType.Decimal, 0, False, e.Keys("COUNTRYID"))
.AddParameter("RISKZONE", DbType.Decimal, 0, False, e.Keys("RISKZONE"))
            
                       .CommandExecute()
                End With
     With New DataManagerFactory("UPDATE ADDRESS.ETranRiskZone SET DESCRIPTION = @:DESCRIPTION, SHORTDESCRIPTION = @:SHORTDESCRIPTION, UPDATEDATE = SYSDATE, UPDATEUSERCODE = @:UPDATEUSERCODE WHERE COMPANYID = @:COMPANYID AND COUNTRYID = @:COUNTRYID AND RISKZONE = @:RISKZONE AND LANGUAGEID = @:LANGUAGEID", "ETranRiskZone", "Linked.Address")                 
                                                   
                       .AddParameter("DESCRIPTION", DbType.AnsiString, 0, (e.NewValues("DESCRIPTION") = String.Empty), e.NewValues("DESCRIPTION"))
.AddParameter("SHORTDESCRIPTION", DbType.AnsiString, 0, (e.NewValues("SHORTDESCRIPTION") = String.Empty), e.NewValues("SHORTDESCRIPTION"))
.AddParameter("UPDATEUSERCODE", DbType.Decimal, 0, False, Session("nUsercode"))
.AddParameter("COMPANYID", DbType.Decimal, 0, False, e.Keys("COMPANYID"))
.AddParameter("COUNTRYID", DbType.Decimal, 0, False, e.Keys("COUNTRYID"))
.AddParameter("RISKZONE", DbType.Decimal, 0, False, e.Keys("RISKZONE"))
.AddParameter("LANGUAGEID", DbType.Decimal, 0, False, CurrentState.Get("Language"))
            
                       .CommandExecute()
                End With         
      
         e.Cancel = True
        EnumRiskZone_Grid.CancelEdit()
    End Sub
    
    Protected Sub EnumRiskZone_Grid_CustomCallback(sender As Object, e As ASPxGridViewCustomCallbackEventArgs) Handles EnumRiskZone_Grid.CustomCallback     
           Dim isNullResult As Boolean = True
           
           Select Case e.Parameters.ToString.ToLower
               Case "delete"
                       Dim COMPANYIDKey As Generic.List(Of Object) = EnumRiskZone_Grid.GetSelectedFieldValues("COMPANYID")
 Dim COUNTRYIDKey As Generic.List(Of Object) = EnumRiskZone_Grid.GetSelectedFieldValues("COUNTRYID")
 Dim RISKZONEKey As Generic.List(Of Object) = EnumRiskZone_Grid.GetSelectedFieldValues("RISKZONE")
        
               For index As Integer = 0 To COMPANYIDKey.Count - 1
                  With New DataManagerFactory("DELETE FROM ADDRESS.ETranRiskZone WHERE COMPANYID = @:COMPANYID AND COUNTRYID = @:COUNTRYID AND RISKZONE = @:RISKZONE ", "ETranRiskZone", "Linked.Address")                 
                                                   
               .AddParameter("COMPANYID", DbType.Decimal, 0, False, COMPANYIDKey(index))
.AddParameter("COUNTRYID", DbType.Decimal, 0, False, COUNTRYIDKey(index))
.AddParameter("RISKZONE", DbType.Decimal, 0, False, RISKZONEKey(index))
            
               .CommandExecute()
          End With 
 With New DataManagerFactory("DELETE FROM ADDRESS.EnumRiskZone WHERE COMPANYID = @:COMPANYID AND COUNTRYID = @:COUNTRYID AND RISKZONE = @:RISKZONE ", "EnumRiskZone", "Linked.Address")                 
                                                   
               .AddParameter("COMPANYID", DbType.Decimal, 0, False, COMPANYIDKey(index))
.AddParameter("COUNTRYID", DbType.Decimal, 0, False, COUNTRYIDKey(index))
.AddParameter("RISKZONE", DbType.Decimal, 0, False, RISKZONEKey(index))
            
               .CommandExecute()
          End With 
                       
              Next           
           
              EnumRiskZone_Grid.DataBind()
                 
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
    
    Protected Sub EnumRiskZone_Grid_RowValidating(sender As Object, e As ASPxDataValidationEventArgs) Handles EnumRiskZone_Grid.RowValidating

        
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
 
#Region "ETranRiskZone Events"
    
    Protected Sub ETranRiskZone_CustomColumnDisplayText(sender As Object, e As DevExpress.Web.ASPxGridView.ASPxGridViewColumnDisplayTextEventArgs) Handles ETranRiskZone.CustomColumnDisplayText
          Dim data As DataTable
          Dim rows() As DataRow
           
          Select Case e.Column.FieldName

               Case Else              
           End Select
    End Sub
  
    Protected Sub ETranRiskZone_DataBinding(ByVal sender As Object, ByVal e As EventArgs) Handles ETranRiskZone.DataBinding
        If (Not IsNothing(Request.Params("__CALLBACKID")) AndAlso Request.Params("__CALLBACKID").EndsWith("ETranRiskZone")) Or _internalCall Then
                       If Caching.Exist("TabCompany") Then
                DirectCast(ETranRiskZone.Columns("COMPANYID"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = Caching.GetItem("TabCompany")

            Else
                Dim source As DataTable = Nothing

                With New DataManagerFactory("SELECT  TABCOMPANY.COMPANYID, TABCOMPANY.RECORDSTATUS, TRANSCOMPANY.LANGUAGEID, TRANSCOMPANY.DESCRIPTION FROM COMMON.TABCOMPANY TABCOMPANY JOIN COMMON.TRANSCOMPANY TRANSCOMPANY ON TRANSCOMPANY.COMPANYID = TABCOMPANY.COMPANYID  WHERE TABCOMPANY.RECORDSTATUS = '1' AND TRANSCOMPANY.LANGUAGEID = @:LANGUAGEID ", "TabCompany", "Linked.Address")
                    .AddParameter("LANGUAGEID", DbType.Decimal, 5, False, CurrentState.Get("Language"))
 
                    source = .QueryExecuteToTable(True)
                    DirectCast(ETranRiskZone.Columns("COMPANYID"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = source
                End With

                If Not IsNothing(source) AndAlso source.Rows.Count > 0 Then
                    Caching.SetItem("TabCompany", source)
                End If
            End If 
             If Caching.Exist("TabCountry") Then
                DirectCast(ETranRiskZone.Columns("COUNTRYID"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = Caching.GetItem("TabCountry")

            Else
                Dim source As DataTable = Nothing

                With New DataManagerFactory("SELECT  TABCOUNTRY.COUNTRYID, TABCOUNTRY.RECORDSTATUS, TRANSCOUNTRY.LANGUAGEID, TRANSCOUNTRY.DESCRIPTION FROM COMMON.TABCOUNTRY TABCOUNTRY JOIN COMMON.TRANSCOUNTRY TRANSCOUNTRY ON TRANSCOUNTRY.COUNTRYID = TABCOUNTRY.COUNTRYID  WHERE TABCOUNTRY.RECORDSTATUS = '1' AND TRANSCOUNTRY.LANGUAGEID = @:LANGUAGEID ", "TabCountry", "Linked.Address")
                    .AddParameter("LANGUAGEID", DbType.Decimal, 5, False, CurrentState.Get("Language"))
 
                    source = .QueryExecuteToTable(True)
                    DirectCast(ETranRiskZone.Columns("COUNTRYID"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = source
                End With

                If Not IsNothing(source) AndAlso source.Rows.Count > 0 Then
                    Caching.SetItem("TabCountry", source)
                End If
            End If 
             If Caching.Exist("EnumRecordStatus") Then
                DirectCast(ETranRiskZone.Columns("RECORDSTATUS"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = Caching.GetItem("EnumRecordStatus")

            Else
                Dim source As DataTable = Nothing

                With New DataManagerFactory("SELECT  ENUMRECORDSTATUS.RECORDSTATUS, ETRANRECORDSTATUS.LANGUAGEID, ETRANRECORDSTATUS.DESCRIPTION FROM COMMON.ENUMRECORDSTATUS ENUMRECORDSTATUS JOIN COMMON.ETRANRECORDSTATUS ETRANRECORDSTATUS ON ETRANRECORDSTATUS.RECORDSTATUS = ENUMRECORDSTATUS.RECORDSTATUS  WHERE ETRANRECORDSTATUS.LANGUAGEID = @:LANGUAGEID ", "EnumRecordStatus", "Linked.Address")
                    .AddParameter("LANGUAGEID", DbType.Decimal, 5, False, CurrentState.Get("Language"))
 
                    source = .QueryExecuteToTable(True)
                    DirectCast(ETranRiskZone.Columns("RECORDSTATUS"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = source
                End With

                If Not IsNothing(source) AndAlso source.Rows.Count > 0 Then
                    Caching.SetItem("EnumRecordStatus", source)
                End If
            End If 
             If Caching.Exist("TabLanguage") Then
                DirectCast(ETranRiskZone.Columns("LANGUAGEID"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = Caching.GetItem("TabLanguage")

            Else
                Dim source As DataTable = Nothing

                With New DataManagerFactory("SELECT  TABLANGUAGE.LANGUAGEID, TABLANGUAGE.RECORDSTATUS, TRANSLANGUAGE.LANGUAGECODEID, TRANSLANGUAGE.DESCRIPTION FROM COMMON.TABLANGUAGE TABLANGUAGE JOIN COMMON.TRANSLANGUAGE TRANSLANGUAGE ON TRANSLANGUAGE.LANGUAGECODEID = TABLANGUAGE.LANGUAGEID  WHERE TABLANGUAGE.RECORDSTATUS = '1' AND TRANSLANGUAGE.LANGUAGECODEID = @:LANGUAGECODEID ", "TabLanguage", "Linked.Address")
                    .AddParameter("LANGUAGECODEID", DbType.Decimal, 5, False, CurrentState.Get("Language"))
 
                    source = .QueryExecuteToTable(True)
                    DirectCast(ETranRiskZone.Columns("LANGUAGEID"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = source
                End With

                If Not IsNothing(source) AndAlso source.Rows.Count > 0 Then
                    Caching.SetItem("TabLanguage", source)
                End If
            End If 
 

                 With New DataManagerFactory("SELECT  ENUMRISKZONE.COMPANYID, ENUMRISKZONE.COUNTRYID, ENUMRISKZONE.RISKZONE, ENUMRISKZONE.RECORDSTATUS, ETRANRISKZONE.COMPANYID, ETRANRISKZONE.COUNTRYID, ETRANRISKZONE.RISKZONE, ETRANRISKZONE.LANGUAGEID, ETRANRISKZONE.DESCRIPTION, ETRANRISKZONE.SHORTDESCRIPTION FROM ADDRESS.ENUMRISKZONE ENUMRISKZONE JOIN ADDRESS.ETRANRISKZONE ETRANRISKZONE ON ETRANRISKZONE.COMPANYID = ENUMRISKZONE.COMPANYID  AND ETRANRISKZONE.COUNTRYID = ENUMRISKZONE.COUNTRYID  AND ETRANRISKZONE.RISKZONE = ENUMRISKZONE.RISKZONE  ", "EnumRiskZone", "Linked.Address")                 
                                                   
                                  
                      ETranRiskZone.DataSource = .QueryExecuteToTable(True)
                 End With 
        End If     
    End Sub

    Protected Sub ETranRiskZone_CellEditorInitialize(sender As Object, e As ASPxGridViewEditorEventArgs) Handles ETranRiskZone.CellEditorInitialize
        If ETranRiskZone.IsNewRowEditing Then
            Select Case e.Column.FieldName
                
                
                
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
Case "LANGUAGEID"
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
Case "RECORDSTATUS"
     DirectCast(e.Editor, ASPxComboBox).DataBindItems() 
Case "LANGUAGEID"
     DirectCast(e.Editor, ASPxComboBox).DataBindItems() 

        End Select
    End Sub

    Protected Sub ETranRiskZone_RowInserting(ByVal sender As Object, ByVal e As ASPxDataInsertingEventArgs) Handles ETranRiskZone.RowInserting 
        Dim isNullResult As Boolean = True
        
        
               
        e.Cancel = True
        ETranRiskZone.CancelEdit()
    End Sub

    Protected Sub ETranRiskZone_RowUpdating(ByVal sender As Object, ByVal e As ASPxDataUpdatingEventArgs) Handles ETranRiskZone.RowUpdating
        Dim isNullResult As Boolean = True
          
             With New DataManagerFactory("UPDATE ADDRESS.ETranRiskZone SET DESCRIPTION = @:DESCRIPTION, SHORTDESCRIPTION = @:SHORTDESCRIPTION, UPDATEDATE = SYSDATE, UPDATEUSERCODE = @:UPDATEUSERCODE WHERE COMPANYID = @:COMPANYID AND COUNTRYID = @:COUNTRYID AND RISKZONE = @:RISKZONE AND LANGUAGEID = @:LANGUAGEID", "ETranRiskZone", "Linked.Address")                 
                                                   
                       .AddParameter("DESCRIPTION", DbType.AnsiString, 0, (e.NewValues("DESCRIPTION") = String.Empty), e.NewValues("DESCRIPTION"))
.AddParameter("SHORTDESCRIPTION", DbType.AnsiString, 0, (e.NewValues("SHORTDESCRIPTION") = String.Empty), e.NewValues("SHORTDESCRIPTION"))
.AddParameter("UPDATEUSERCODE", DbType.Decimal, 0, False, Session("nUsercode"))
.AddParameter("COMPANYID", DbType.Decimal, 0, False, e.Keys("COMPANYID"))
.AddParameter("COUNTRYID", DbType.Decimal, 0, False, e.Keys("COUNTRYID"))
.AddParameter("RISKZONE", DbType.Decimal, 0, False, e.Keys("RISKZONE"))
.AddParameter("LANGUAGEID", DbType.Decimal, 0, False, e.Keys("LANGUAGEID"))
            
                       .CommandExecute()
                End With         
      
         e.Cancel = True
        ETranRiskZone.CancelEdit()
    End Sub
    
    Protected Sub ETranRiskZone_CustomCallback(sender As Object, e As ASPxGridViewCustomCallbackEventArgs) Handles ETranRiskZone.CustomCallback     
           Dim isNullResult As Boolean = True
           
           Select Case e.Parameters.ToString.ToLower
               Case "delete"
                   
                 
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
    
    Protected Sub ETranRiskZone_RowValidating(sender As Object, e As ASPxDataValidationEventArgs) Handles ETranRiskZone.RowValidating

        
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