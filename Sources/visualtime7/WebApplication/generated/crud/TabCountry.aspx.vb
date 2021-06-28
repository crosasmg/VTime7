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

Partial Class Maintenance_TabCountry
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

            TabCountry_Grid.DataBind()
        End If      
    End Sub

#End Region

#Region "Controls Events"

         
    
 

#End Region

#Region "TabCountry_Grid Events"
    
    Protected Sub TabCountry_Grid_CustomColumnDisplayText(sender As Object, e As DevExpress.Web.ASPxGridView.ASPxGridViewColumnDisplayTextEventArgs) Handles TabCountry_Grid.CustomColumnDisplayText
          Dim data As DataTable
          Dim rows() As DataRow
           
          Select Case e.Column.FieldName

               Case Else              
           End Select
    End Sub
  
    Protected Sub TabCountry_Grid_DataBinding(ByVal sender As Object, ByVal e As EventArgs) Handles TabCountry_Grid.DataBinding
        If (Not IsNothing(Request.Params("__CALLBACKID")) AndAlso Request.Params("__CALLBACKID").EndsWith("TabCountry_Grid")) Or _internalCall Then
                       If Caching.Exist("EnumRecordStatus") Then
                DirectCast(TabCountry_Grid.Columns("RECORDSTATUS"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = Caching.GetItem("EnumRecordStatus")

            Else
                Dim source As DataTable = Nothing

                With New DataManagerFactory("SELECT  ENUMRECORDSTATUS.RECORDSTATUS, ENUMRECORDSTATUS.RECORDSTATUSCODE, ETRANRECORDSTATUS.LANGUAGEID, ETRANRECORDSTATUS.DESCRIPTION FROM COMMON.ENUMRECORDSTATUS ENUMRECORDSTATUS JOIN COMMON.ETRANRECORDSTATUS ETRANRECORDSTATUS ON ETRANRECORDSTATUS.RECORDSTATUS = ENUMRECORDSTATUS.RECORDSTATUS  WHERE ENUMRECORDSTATUS.RECORDSTATUSCODE = '1' AND ETRANRECORDSTATUS.LANGUAGEID = @:LANGUAGEID ", "EnumRecordStatus", "Linked.Common")
                    .AddParameter("LANGUAGEID", DbType.Decimal, 5, False, CurrentState.Get("Language"))
 
                    source = .QueryExecuteToTable(True)
                    DirectCast(TabCountry_Grid.Columns("RECORDSTATUS"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = source
                End With

                If Not IsNothing(source) AndAlso source.Rows.Count > 0 Then
                    Caching.SetItem("EnumRecordStatus", source)
                End If
            End If 
             If Caching.Exist("EnumUsePostalCode") Then
                DirectCast(TabCountry_Grid.Columns("USEPOSTALCODEINDICATOR"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = Caching.GetItem("EnumUsePostalCode")

            Else
                Dim source As DataTable = Nothing

                With New DataManagerFactory("SELECT  ENUMUSEPOSTALCODE.USEPOSTALCODE, ENUMUSEPOSTALCODE.RECORDSTATUS, ETRANUSEPOSTALCODE.LANGUAGEID, ETRANUSEPOSTALCODE.DESCRIPTION FROM COMMON.ENUMUSEPOSTALCODE ENUMUSEPOSTALCODE JOIN COMMON.ETRANUSEPOSTALCODE ETRANUSEPOSTALCODE ON ETRANUSEPOSTALCODE.USEPOSTALCODE = ENUMUSEPOSTALCODE.USEPOSTALCODE  WHERE ENUMUSEPOSTALCODE.RECORDSTATUS = '1' AND ETRANUSEPOSTALCODE.LANGUAGEID = @:LANGUAGEID ", "EnumUsePostalCode", "Linked.Common")
                    .AddParameter("LANGUAGEID", DbType.Decimal, 5, False, CurrentState.Get("Language"))
 
                    source = .QueryExecuteToTable(True)
                    DirectCast(TabCountry_Grid.Columns("USEPOSTALCODEINDICATOR"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = source
                End With

                If Not IsNothing(source) AndAlso source.Rows.Count > 0 Then
                    Caching.SetItem("EnumUsePostalCode", source)
                End If
            End If 
 

                 With New DataManagerFactory("SELECT  TABCOUNTRY.COUNTRYID, TABCOUNTRY.RECORDSTATUS, TABCOUNTRY.CITYFROMZIPCODE, TABCOUNTRY.CITYLEVEL, TABCOUNTRY.COUNTRYPHONEAREA, TABCOUNTRY.COUNTRYCODEISO3166, TABCOUNTRY.COUNTRYCODEISO3166TWOCHAR, TABCOUNTRY.COUNTRYCODEISO3166THREECHAR, TABCOUNTRY.MAXIMUMNUMBERGEOGRAPHICALAREAS, TABCOUNTRY.STATEORPROVINCELEVEL, TABCOUNTRY.USEPOSTALCODEINDICATOR, TRANSCOUNTRY.COUNTRYID, TRANSCOUNTRY.LANGUAGEID, TRANSCOUNTRY.DESCRIPTION, TRANSCOUNTRY.SHORTDESCRIPTION FROM COMMON.TABCOUNTRY TABCOUNTRY JOIN COMMON.TRANSCOUNTRY TRANSCOUNTRY ON TRANSCOUNTRY.COUNTRYID = TABCOUNTRY.COUNTRYID  WHERE TRANSCOUNTRY.LANGUAGEID = @:LANGUAGEID", "TabCountry", "Linked.Common")                 
                                                   
                      .AddParameter("LANGUAGEID", DbType.Decimal, 5, False, CurrentState.Get("Language"))
            
                      TabCountry_Grid.DataSource = .QueryExecuteToTable(True)
                 End With 
        End If     
    End Sub

    Protected Sub TabCountry_Grid_CellEditorInitialize(sender As Object, e As ASPxGridViewEditorEventArgs) Handles TabCountry_Grid.CellEditorInitialize
        If TabCountry_Grid.IsNewRowEditing Then
            Select Case e.Column.FieldName
                
                
                
                Case "COUNTRYID"                     
                       e.Editor.Focus()               
            End Select

        Else

            Select Case e.Column.FieldName
                Case "COUNTRYID"
     e.Editor.Enabled = False
                   
                
                
                Case "RECORDSTATUS"                     
                     e.Editor.Focus()
            End Select
        End If
        
       Select Case e.Column.FieldName
           Case "COUNTRYID"
                 
                 
           Case "RECORDSTATUS"
     DirectCast(e.Editor, ASPxComboBox).DataBindItems() 
Case "USEPOSTALCODEINDICATOR"
     DirectCast(e.Editor, ASPxComboBox).DataBindItems() 

        End Select
    End Sub

    Protected Sub TabCountry_Grid_RowInserting(ByVal sender As Object, ByVal e As ASPxDataInsertingEventArgs) Handles TabCountry_Grid.RowInserting 
        Dim isNullResult As Boolean = True
        
             With New DataManagerFactory("INSERT INTO COMMON.TabCountry (COUNTRYID, RECORDSTATUS, CITYFROMZIPCODE, CITYLEVEL, COUNTRYPHONEAREA, COUNTRYCODEISO3166, COUNTRYCODEISO3166TWOCHAR, COUNTRYCODEISO3166THREECHAR, MAXIMUMNUMBERGEOGRAPHICALAREAS, STATEORPROVINCELEVEL, USEPOSTALCODEINDICATOR, CREATIONDATE, CREATORUSERCODE, UPDATEDATE, UPDATEUSERCODE) VALUES (@:COUNTRYID, @:RECORDSTATUS, @:CITYFROMZIPCODE, @:CITYLEVEL, @:COUNTRYPHONEAREA, @:COUNTRYCODEISO3166, @:COUNTRYCODEISO3166TWOCHAR, @:COUNTRYCODEISO3166THREECHAR, @:MAXIMUMNUMBERGEOGRAPHICALAREAS, @:STATEORPROVINCELEVEL, @:USEPOSTALCODEINDICATOR, SYSDATE, @:CREATORUSERCODE, SYSDATE, @:UPDATEUSERCODE)", "TabCountry", "Linked.Common")                 
                                                   
                       .AddParameter("COUNTRYID", DbType.Decimal, 0, False, e.NewValues("COUNTRYID"))
.AddParameter("RECORDSTATUS", DbType.AnsiString, 0, False, e.NewValues("RECORDSTATUS"))
.AddParameter("CITYFROMZIPCODE", DbType.AnsiString, 0, False, e.NewValues("CITYFROMZIPCODE"))
.AddParameter("CITYLEVEL", DbType.Decimal, 0, False, e.NewValues("CITYLEVEL"))
.AddParameter("COUNTRYPHONEAREA", DbType.Decimal, 0, False, e.NewValues("COUNTRYPHONEAREA"))
.AddParameter("COUNTRYCODEISO3166", DbType.Decimal, 0, False, e.NewValues("COUNTRYCODEISO3166"))
.AddParameter("COUNTRYCODEISO3166TWOCHAR", DbType.AnsiString, 0, False, e.NewValues("COUNTRYCODEISO3166TWOCHAR"))
.AddParameter("COUNTRYCODEISO3166THREECHAR", DbType.AnsiString, 0, False, e.NewValues("COUNTRYCODEISO3166THREECHAR"))
.AddParameter("MAXIMUMNUMBERGEOGRAPHICALAREAS", DbType.Decimal, 0, False, e.NewValues("MAXIMUMNUMBERGEOGRAPHICALAREAS"))
.AddParameter("STATEORPROVINCELEVEL", DbType.Decimal, 0, False, e.NewValues("STATEORPROVINCELEVEL"))
.AddParameter("USEPOSTALCODEINDICATOR", DbType.Decimal, 0, False, e.NewValues("USEPOSTALCODEINDICATOR"))
.AddParameter("CREATORUSERCODE", DbType.Decimal, 0, False, Session("nUsercode"))
.AddParameter("UPDATEUSERCODE", DbType.Decimal, 0, False, Session("nUsercode"))
            
                       .CommandExecute()
                End With
        For Each languageItem In InMotionGIT.Common.Proxy.Helpers.Language.LanguageToDictionary


            With New DataManagerFactory("INSERT INTO COMMON.TransCountry (COUNTRYID, LANGUAGEID, DESCRIPTION, SHORTDESCRIPTION, CREATIONDATE, CREATORUSERCODE, UPDATEDATE, UPDATEUSERCODE) VALUES (@:COUNTRYID, @:LANGUAGEID, @:DESCRIPTION, @:SHORTDESCRIPTION, SYSDATE, @:CREATORUSERCODE, SYSDATE, @:UPDATEUSERCODE)", "TransCountry", "Linked.Common")

                .AddParameter("COUNTRYID", DbType.Decimal, 0, False, e.NewValues("COUNTRYID"))
                .AddParameter("LANGUAGEID", DbType.Decimal, 0, False, languageItem.Key)
                .AddParameter("DESCRIPTION", DbType.AnsiString, 0, False, e.NewValues("DESCRIPTION"))
                .AddParameter("SHORTDESCRIPTION", DbType.AnsiString, 0, False, e.NewValues("SHORTDESCRIPTION"))
                .AddParameter("CREATORUSERCODE", DbType.Decimal, 0, False, Session("nUsercode"))
                .AddParameter("UPDATEUSERCODE", DbType.Decimal, 0, False, Session("nUsercode"))

                .CommandExecute()
            End With
        Next

               
        e.Cancel = True
        TabCountry_Grid.CancelEdit()
    End Sub

    Protected Sub TabCountry_Grid_RowUpdating(ByVal sender As Object, ByVal e As ASPxDataUpdatingEventArgs) Handles TabCountry_Grid.RowUpdating
        Dim isNullResult As Boolean = True
          
             With New DataManagerFactory("UPDATE COMMON.TabCountry SET RECORDSTATUS = @:RECORDSTATUS, CITYFROMZIPCODE = @:CITYFROMZIPCODE, CITYLEVEL = @:CITYLEVEL, COUNTRYPHONEAREA = @:COUNTRYPHONEAREA, COUNTRYCODEISO3166 = @:COUNTRYCODEISO3166, COUNTRYCODEISO3166TWOCHAR = @:COUNTRYCODEISO3166TWOCHAR, COUNTRYCODEISO3166THREECHAR = @:COUNTRYCODEISO3166THREECHAR, MAXIMUMNUMBERGEOGRAPHICALAREAS = @:MAXIMUMNUMBERGEOGRAPHICALAREAS, STATEORPROVINCELEVEL = @:STATEORPROVINCELEVEL, USEPOSTALCODEINDICATOR = @:USEPOSTALCODEINDICATOR, UPDATEDATE = SYSDATE, UPDATEUSERCODE = @:UPDATEUSERCODE WHERE COUNTRYID = @:COUNTRYID", "TabCountry", "Linked.Common")                 
                                                   
                       .AddParameter("RECORDSTATUS", DbType.AnsiString, 0, False, e.NewValues("RECORDSTATUS"))
.AddParameter("CITYFROMZIPCODE", DbType.AnsiString, 0, False, e.NewValues("CITYFROMZIPCODE"))
.AddParameter("CITYLEVEL", DbType.Decimal, 0, False, e.NewValues("CITYLEVEL"))
.AddParameter("COUNTRYPHONEAREA", DbType.Decimal, 0, False, e.NewValues("COUNTRYPHONEAREA"))
.AddParameter("COUNTRYCODEISO3166", DbType.Decimal, 0, False, e.NewValues("COUNTRYCODEISO3166"))
.AddParameter("COUNTRYCODEISO3166TWOCHAR", DbType.AnsiString, 0, False, e.NewValues("COUNTRYCODEISO3166TWOCHAR"))
.AddParameter("COUNTRYCODEISO3166THREECHAR", DbType.AnsiString, 0, False, e.NewValues("COUNTRYCODEISO3166THREECHAR"))
.AddParameter("MAXIMUMNUMBERGEOGRAPHICALAREAS", DbType.Decimal, 0, False, e.NewValues("MAXIMUMNUMBERGEOGRAPHICALAREAS"))
.AddParameter("STATEORPROVINCELEVEL", DbType.Decimal, 0, False, e.NewValues("STATEORPROVINCELEVEL"))
.AddParameter("USEPOSTALCODEINDICATOR", DbType.Decimal, 0, False, e.NewValues("USEPOSTALCODEINDICATOR"))
.AddParameter("UPDATEUSERCODE", DbType.Decimal, 0, False, Session("nUsercode"))
.AddParameter("COUNTRYID", DbType.Decimal, 0, False, e.Keys("COUNTRYID"))
            
                       .CommandExecute()
                End With
     With New DataManagerFactory("UPDATE COMMON.TransCountry SET DESCRIPTION = @:DESCRIPTION, SHORTDESCRIPTION = @:SHORTDESCRIPTION, UPDATEDATE = SYSDATE, UPDATEUSERCODE = @:UPDATEUSERCODE WHERE COUNTRYID = @:COUNTRYID AND LANGUAGEID = @:LANGUAGEID", "TransCountry", "Linked.Common")                 
                                                   
                       .AddParameter("DESCRIPTION", DbType.AnsiString, 0, False, e.NewValues("DESCRIPTION"))
.AddParameter("SHORTDESCRIPTION", DbType.AnsiString, 0, False, e.NewValues("SHORTDESCRIPTION"))
.AddParameter("UPDATEUSERCODE", DbType.Decimal, 0, False, Session("nUsercode"))
.AddParameter("COUNTRYID", DbType.Decimal, 0, False, e.Keys("COUNTRYID"))
.AddParameter("LANGUAGEID", DbType.Decimal, 0, False, CurrentState.Get("Language"))
            
                       .CommandExecute()
                End With         
      
         e.Cancel = True
        TabCountry_Grid.CancelEdit()
    End Sub
    
    Protected Sub TabCountry_Grid_CustomCallback(sender As Object, e As ASPxGridViewCustomCallbackEventArgs) Handles TabCountry_Grid.CustomCallback     
           Dim isNullResult As Boolean = True
           
           Select Case e.Parameters.ToString.ToLower
               Case "delete"
                       Dim COUNTRYIDKey As Generic.List(Of Object) = TabCountry_Grid.GetSelectedFieldValues("COUNTRYID")
        
               For index As Integer = 0 To COUNTRYIDKey.Count - 1
                  With New DataManagerFactory("DELETE FROM COMMON.TransCountry WHERE COUNTRYID = @:COUNTRYID ", "TransCountry", "Linked.Common")                 
                                                   
               .AddParameter("COUNTRYID", DbType.Decimal, 0, False, COUNTRYIDKey(index))
            
               .CommandExecute()
          End With 
 With New DataManagerFactory("DELETE FROM COMMON.TabCountry WHERE COUNTRYID = @:COUNTRYID ", "TabCountry", "Linked.Common")                 
                                                   
               .AddParameter("COUNTRYID", DbType.Decimal, 0, False, COUNTRYIDKey(index))
            
               .CommandExecute()
          End With 
                       
              Next           
           
              TabCountry_Grid.DataBind()
                 
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
    
    Protected Sub TabCountry_Grid_RowValidating(sender As Object, e As ASPxDataValidationEventArgs) Handles TabCountry_Grid.RowValidating

        
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
 
#Region "TransCountry Events"
    
    Protected Sub TransCountry_CustomColumnDisplayText(sender As Object, e As DevExpress.Web.ASPxGridView.ASPxGridViewColumnDisplayTextEventArgs) Handles TransCountry.CustomColumnDisplayText
          Dim data As DataTable
          Dim rows() As DataRow
           
          Select Case e.Column.FieldName

               Case Else              
           End Select
    End Sub
  
    Protected Sub TransCountry_DataBinding(ByVal sender As Object, ByVal e As EventArgs) Handles TransCountry.DataBinding
        If (Not IsNothing(Request.Params("__CALLBACKID")) AndAlso Request.Params("__CALLBACKID").EndsWith("TransCountry")) Or _internalCall Then
                       If Caching.Exist("EnumRecordStatus") Then
                DirectCast(TransCountry.Columns("RECORDSTATUS"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = Caching.GetItem("EnumRecordStatus")

            Else
                Dim source As DataTable = Nothing

                With New DataManagerFactory("SELECT  ENUMRECORDSTATUS.RECORDSTATUS, ENUMRECORDSTATUS.RECORDSTATUSCODE, ETRANRECORDSTATUS.LANGUAGEID, ETRANRECORDSTATUS.DESCRIPTION FROM COMMON.ENUMRECORDSTATUS ENUMRECORDSTATUS JOIN COMMON.ETRANRECORDSTATUS ETRANRECORDSTATUS ON ETRANRECORDSTATUS.RECORDSTATUS = ENUMRECORDSTATUS.RECORDSTATUS  WHERE ENUMRECORDSTATUS.RECORDSTATUSCODE = '1' AND ETRANRECORDSTATUS.LANGUAGEID = @:LANGUAGEID ", "EnumRecordStatus", "Linked.Common")
                    .AddParameter("LANGUAGEID", DbType.Decimal, 5, False, CurrentState.Get("Language"))
 
                    source = .QueryExecuteToTable(True)
                    DirectCast(TransCountry.Columns("RECORDSTATUS"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = source
                End With

                If Not IsNothing(source) AndAlso source.Rows.Count > 0 Then
                    Caching.SetItem("EnumRecordStatus", source)
                End If
            End If 
             If Caching.Exist("EnumUsePostalCode") Then
                DirectCast(TransCountry.Columns("USEPOSTALCODEINDICATOR"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = Caching.GetItem("EnumUsePostalCode")

            Else
                Dim source As DataTable = Nothing

                With New DataManagerFactory("SELECT  ENUMUSEPOSTALCODE.USEPOSTALCODE, ENUMUSEPOSTALCODE.RECORDSTATUS, ETRANUSEPOSTALCODE.LANGUAGEID, ETRANUSEPOSTALCODE.DESCRIPTION FROM COMMON.ENUMUSEPOSTALCODE ENUMUSEPOSTALCODE JOIN COMMON.ETRANUSEPOSTALCODE ETRANUSEPOSTALCODE ON ETRANUSEPOSTALCODE.USEPOSTALCODE = ENUMUSEPOSTALCODE.USEPOSTALCODE  WHERE ENUMUSEPOSTALCODE.RECORDSTATUS = '1' AND ETRANUSEPOSTALCODE.LANGUAGEID = @:LANGUAGEID ", "EnumUsePostalCode", "Linked.Common")
                    .AddParameter("LANGUAGEID", DbType.Decimal, 5, False, CurrentState.Get("Language"))
 
                    source = .QueryExecuteToTable(True)
                    DirectCast(TransCountry.Columns("USEPOSTALCODEINDICATOR"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = source
                End With

                If Not IsNothing(source) AndAlso source.Rows.Count > 0 Then
                    Caching.SetItem("EnumUsePostalCode", source)
                End If
            End If 
             If Caching.Exist("TabLanguage") Then
                DirectCast(TransCountry.Columns("LANGUAGEID"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = Caching.GetItem("TabLanguage")

            Else
                Dim source As DataTable = Nothing

                With New DataManagerFactory("SELECT  TABLANGUAGE.LANGUAGEID, TABLANGUAGE.RECORDSTATUS, TRANSLANGUAGE.LANGUAGEID, TRANSLANGUAGE.DESCRIPTION FROM COMMON.TABLANGUAGE TABLANGUAGE JOIN COMMON.TRANSLANGUAGE TRANSLANGUAGE ON TRANSLANGUAGE.LANGUAGECODEID = TABLANGUAGE.LANGUAGEID  WHERE TABLANGUAGE.RECORDSTATUS = '1' AND TRANSLANGUAGE.LANGUAGEID = @:LANGUAGEID ", "TabLanguage", "Linked.Common")
                    .AddParameter("LANGUAGEID", DbType.Decimal, 5, False, CurrentState.Get("Language"))
 
                    source = .QueryExecuteToTable(True)
                    DirectCast(TransCountry.Columns("LANGUAGEID"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = source
                End With

                If Not IsNothing(source) AndAlso source.Rows.Count > 0 Then
                    Caching.SetItem("TabLanguage", source)
                End If
            End If 
 

                 With New DataManagerFactory("SELECT  TABCOUNTRY.COUNTRYID, TABCOUNTRY.RECORDSTATUS, TABCOUNTRY.CITYFROMZIPCODE, TABCOUNTRY.CITYLEVEL, TABCOUNTRY.COUNTRYPHONEAREA, TABCOUNTRY.COUNTRYCODEISO3166, TABCOUNTRY.COUNTRYCODEISO3166TWOCHAR, TABCOUNTRY.COUNTRYCODEISO3166THREECHAR, TABCOUNTRY.MAXIMUMNUMBERGEOGRAPHICALAREAS, TABCOUNTRY.STATEORPROVINCELEVEL, TABCOUNTRY.USEPOSTALCODEINDICATOR, TRANSCOUNTRY.COUNTRYID, TRANSCOUNTRY.LANGUAGEID, TRANSCOUNTRY.DESCRIPTION, TRANSCOUNTRY.SHORTDESCRIPTION FROM COMMON.TABCOUNTRY TABCOUNTRY JOIN COMMON.TRANSCOUNTRY TRANSCOUNTRY ON TRANSCOUNTRY.COUNTRYID = TABCOUNTRY.COUNTRYID  ", "TabCountry", "Linked.Common")                 
                                                   
                                  
                      TransCountry.DataSource = .QueryExecuteToTable(True)
                 End With 
        End If     
    End Sub

    Protected Sub TransCountry_CellEditorInitialize(sender As Object, e As ASPxGridViewEditorEventArgs) Handles TransCountry.CellEditorInitialize
        If TransCountry.IsNewRowEditing Then
            Select Case e.Column.FieldName
                
                
                
                Case "COUNTRYID"                     
                       e.Editor.Focus()               
            End Select

        Else

            Select Case e.Column.FieldName
                Case "COUNTRYID"
     e.Editor.Enabled = False
Case "LANGUAGEID"
     e.Editor.Enabled = False
                   
                
                
                Case "RECORDSTATUS"                     
                     e.Editor.Focus()
            End Select
        End If
        
       Select Case e.Column.FieldName
           Case "COUNTRYID"
                 
                 
           Case "RECORDSTATUS"
     DirectCast(e.Editor, ASPxComboBox).DataBindItems() 
Case "USEPOSTALCODEINDICATOR"
     DirectCast(e.Editor, ASPxComboBox).DataBindItems() 
Case "LANGUAGEID"
     DirectCast(e.Editor, ASPxComboBox).DataBindItems() 

        End Select
    End Sub

    Protected Sub TransCountry_RowInserting(ByVal sender As Object, ByVal e As ASPxDataInsertingEventArgs) Handles TransCountry.RowInserting 
        Dim isNullResult As Boolean = True
        
        
               
        e.Cancel = True
        TransCountry.CancelEdit()
    End Sub

    Protected Sub TransCountry_RowUpdating(ByVal sender As Object, ByVal e As ASPxDataUpdatingEventArgs) Handles TransCountry.RowUpdating
        Dim isNullResult As Boolean = True
          
             With New DataManagerFactory("UPDATE COMMON.TransCountry SET DESCRIPTION = @:DESCRIPTION, SHORTDESCRIPTION = @:SHORTDESCRIPTION, UPDATEDATE = SYSDATE, UPDATEUSERCODE = @:UPDATEUSERCODE WHERE COUNTRYID = @:COUNTRYID AND LANGUAGEID = @:LANGUAGEID", "TransCountry", "Linked.Common")                 
                                                   
                       .AddParameter("DESCRIPTION", DbType.AnsiString, 0, False, e.NewValues("DESCRIPTION"))
.AddParameter("SHORTDESCRIPTION", DbType.AnsiString, 0, False, e.NewValues("SHORTDESCRIPTION"))
.AddParameter("UPDATEUSERCODE", DbType.Decimal, 0, False, Session("nUsercode"))
.AddParameter("COUNTRYID", DbType.Decimal, 0, False, e.Keys("COUNTRYID"))
.AddParameter("LANGUAGEID", DbType.Decimal, 0, False, e.Keys("LANGUAGEID"))
            
                       .CommandExecute()
                End With         
      
         e.Cancel = True
        TransCountry.CancelEdit()
    End Sub
    
    Protected Sub TransCountry_CustomCallback(sender As Object, e As ASPxGridViewCustomCallbackEventArgs) Handles TransCountry.CustomCallback     
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
    
    Protected Sub TransCountry_RowValidating(sender As Object, e As ASPxDataValidationEventArgs) Handles TransCountry.RowValidating

        
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