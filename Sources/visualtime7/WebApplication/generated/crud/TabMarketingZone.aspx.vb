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

Partial Class Maintenance_TabMarketingZone
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

            TabMarketingZone_Grid.DataBind()
        End If      
    End Sub

#End Region

#Region "Controls Events"

         
    
 

#End Region

#Region "TabMarketingZone_Grid Events"
    
    Protected Sub TabMarketingZone_Grid_CustomColumnDisplayText(sender As Object, e As DevExpress.Web.ASPxGridView.ASPxGridViewColumnDisplayTextEventArgs) Handles TabMarketingZone_Grid.CustomColumnDisplayText
          Dim data As DataTable
          Dim rows() As DataRow
           
          Select Case e.Column.FieldName
            Case "MARKETINGZONELEVELID"
                data = DirectCast(TabMarketingZone_Grid.Columns("MARKETINGZONELEVELID"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource 
                rows = data.Select(String.Format("COUNTRYID = {0} AND MARKETINGZONELEVELID = {1}", e.GetFieldValue("COUNTRYID"), e.Value)) 
                If rows.Count > 0 Then 
                    e.DisplayText = rows(0)("DESCRIPTION") 
                End If 

               Case Else              
           End Select
    End Sub
  
    Protected Sub TabMarketingZone_Grid_DataBinding(ByVal sender As Object, ByVal e As EventArgs) Handles TabMarketingZone_Grid.DataBinding
        If (Not IsNothing(Request.Params("__CALLBACKID")) AndAlso Request.Params("__CALLBACKID").EndsWith("TabMarketingZone_Grid")) Or _internalCall Then
                       If Caching.Exist("TabCompany") Then
                DirectCast(TabMarketingZone_Grid.Columns("COMPANYID"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = Caching.GetItem("TabCompany")

            Else
                Dim source As DataTable = Nothing

                With New DataManagerFactory("SELECT  TABCOMPANY.COMPANYID, TABCOMPANY.RECORDSTATUS, TRANSCOMPANY.LANGUAGEID, TRANSCOMPANY.DESCRIPTION FROM COMMON.TABCOMPANY TABCOMPANY JOIN COMMON.TRANSCOMPANY TRANSCOMPANY ON TRANSCOMPANY.COMPANYID = TABCOMPANY.COMPANYID  WHERE TABCOMPANY.RECORDSTATUS = '1' AND TRANSCOMPANY.LANGUAGEID = @:LANGUAGEID ", "TabCompany", "Linked.Address")
                    .AddParameter("LANGUAGEID", DbType.Decimal, 5, False, CurrentState.Get("Language"))
 
                    source = .QueryExecuteToTable(True)
                    DirectCast(TabMarketingZone_Grid.Columns("COMPANYID"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = source
                End With

                If Not IsNothing(source) AndAlso source.Rows.Count > 0 Then
                    Caching.SetItem("TabCompany", source)
                End If
            End If 
             If Caching.Exist("TabCountry") Then
                DirectCast(TabMarketingZone_Grid.Columns("COUNTRYID"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = Caching.GetItem("TabCountry")

            Else
                Dim source As DataTable = Nothing

                With New DataManagerFactory("SELECT  TABCOUNTRY.COUNTRYID, TABCOUNTRY.RECORDSTATUS, TRANSCOUNTRY.LANGUAGEID, TRANSCOUNTRY.DESCRIPTION FROM COMMON.TABCOUNTRY TABCOUNTRY JOIN COMMON.TRANSCOUNTRY TRANSCOUNTRY ON TRANSCOUNTRY.COUNTRYID = TABCOUNTRY.COUNTRYID  WHERE TABCOUNTRY.RECORDSTATUS = '1' AND TRANSCOUNTRY.LANGUAGEID = @:LANGUAGEID ", "TabCountry", "Linked.Address")
                    .AddParameter("LANGUAGEID", DbType.Decimal, 5, False, CurrentState.Get("Language"))
 
                    source = .QueryExecuteToTable(True)
                    DirectCast(TabMarketingZone_Grid.Columns("COUNTRYID"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = source
                End With

                If Not IsNothing(source) AndAlso source.Rows.Count > 0 Then
                    Caching.SetItem("TabCountry", source)
                End If
            End If 
             If Caching.Exist("TabMarketingZoneNames") Then
                DirectCast(TabMarketingZone_Grid.Columns("MARKETINGZONELEVELID"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = Caching.GetItem("TabMarketingZoneNames")

            Else
                Dim source As DataTable = Nothing

                With New DataManagerFactory("SELECT  TABMARKETINGZONENAMES.COUNTRYID, TABMARKETINGZONENAMES.MARKETINGZONELEVELID, TABMARKETINGZONENAMES.RECORDSTATUS, TRANSMARKETINGZONENAMES.LANGUAGEID, TRANSMARKETINGZONENAMES.DESCRIPTION FROM ADDRESS.TABMARKETINGZONENAMES TABMARKETINGZONENAMES JOIN ADDRESS.TRANSMARKETINGZONENAMES TRANSMARKETINGZONENAMES ON TRANSMARKETINGZONENAMES.COUNTRYID = TABMARKETINGZONENAMES.COUNTRYID  AND TRANSMARKETINGZONENAMES.MARKETINGZONELEVELID = TABMARKETINGZONENAMES.MARKETINGZONELEVELID  WHERE TABMARKETINGZONENAMES.RECORDSTATUS = '1' AND TRANSMARKETINGZONENAMES.LANGUAGEID = @:LANGUAGEID ", "TabMarketingZoneNames", "Linked.Address")
                    .AddParameter("LANGUAGEID", DbType.Decimal, 5, False, CurrentState.Get("Language"))
 
                    source = .QueryExecuteToTable(True)
                    DirectCast(TabMarketingZone_Grid.Columns("MARKETINGZONELEVELID"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = source
                End With

                If Not IsNothing(source) AndAlso source.Rows.Count > 0 Then
                    Caching.SetItem("TabMarketingZoneNames", source)
                End If
            End If 
             If Caching.Exist("EnumRecordStatus") Then
                DirectCast(TabMarketingZone_Grid.Columns("RECORDSTATUS"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = Caching.GetItem("EnumRecordStatus")

            Else
                Dim source As DataTable = Nothing

                With New DataManagerFactory("SELECT  ENUMRECORDSTATUS.RECORDSTATUS, ETRANRECORDSTATUS.LANGUAGEID, ETRANRECORDSTATUS.DESCRIPTION FROM COMMON.ENUMRECORDSTATUS ENUMRECORDSTATUS JOIN COMMON.ETRANRECORDSTATUS ETRANRECORDSTATUS ON ETRANRECORDSTATUS.RECORDSTATUS = ENUMRECORDSTATUS.RECORDSTATUS  WHERE ETRANRECORDSTATUS.LANGUAGEID = @:LANGUAGEID ", "EnumRecordStatus", "Linked.Address")
                    .AddParameter("LANGUAGEID", DbType.Decimal, 5, False, CurrentState.Get("Language"))
 
                    source = .QueryExecuteToTable(True)
                    DirectCast(TabMarketingZone_Grid.Columns("RECORDSTATUS"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = source
                End With

                If Not IsNothing(source) AndAlso source.Rows.Count > 0 Then
                    Caching.SetItem("EnumRecordStatus", source)
                End If
            End If 
 

                 With New DataManagerFactory("SELECT  TABMARKETINGZONE.COMPANYID, TABMARKETINGZONE.MARKETINGZONETABLEID, TABMARKETINGZONE.COUNTRYID, TABMARKETINGZONE.MARKETINGZONELEVELID, TABMARKETINGZONE.MARKETINGZONEID, TABMARKETINGZONE.RECORDSTATUS, TRANSMARKETINGZONE.COMPANYID, TRANSMARKETINGZONE.MARKETINGZONETABLEID, TRANSMARKETINGZONE.COUNTRYID, TRANSMARKETINGZONE.MARKETINGZONELEVELID, TRANSMARKETINGZONE.MARKETINGZONEID, TRANSMARKETINGZONE.LANGUAGEID, TRANSMARKETINGZONE.DESCRIPTION, TRANSMARKETINGZONE.SHORTDESCRIPTION, TRANSMARKETINGZONE.CANCELLATIONDATE FROM ADDRESS.TABMARKETINGZONE TABMARKETINGZONE JOIN ADDRESS.TRANSMARKETINGZONE TRANSMARKETINGZONE ON TRANSMARKETINGZONE.COMPANYID = TABMARKETINGZONE.COMPANYID  AND TRANSMARKETINGZONE.MARKETINGZONETABLEID = TABMARKETINGZONE.MARKETINGZONETABLEID  AND TRANSMARKETINGZONE.COUNTRYID = TABMARKETINGZONE.COUNTRYID  AND TRANSMARKETINGZONE.MARKETINGZONELEVELID = TABMARKETINGZONE.MARKETINGZONELEVELID  AND TRANSMARKETINGZONE.MARKETINGZONEID = TABMARKETINGZONE.MARKETINGZONEID  WHERE TRANSMARKETINGZONE.LANGUAGEID = @:LANGUAGEID", "TabMarketingZone", "Linked.Address")                 
                                                   
                      .AddParameter("LANGUAGEID", DbType.Decimal, 5, False, CurrentState.Get("Language"))
            
                      TabMarketingZone_Grid.DataSource = .QueryExecuteToTable(True)
                 End With 
        End If     
    End Sub

    Protected Sub TabMarketingZone_Grid_CellEditorInitialize(sender As Object, e As ASPxGridViewEditorEventArgs) Handles TabMarketingZone_Grid.CellEditorInitialize
        If TabMarketingZone_Grid.IsNewRowEditing Then
            Select Case e.Column.FieldName
                
                Case "MARKETINGZONELEVELID"
     AddHandler DirectCast(e.Editor, ASPxComboBox).Callback, AddressOf MarketingZoneLevelID_OnCallback 

                
                Case "COMPANYID"                     
                       e.Editor.Focus()               
            End Select

        Else

            Select Case e.Column.FieldName
                Case "COMPANYID"
     e.Editor.Enabled = False
Case "MARKETINGZONETABLEID"
     e.Editor.Enabled = False
Case "COUNTRYID"
     e.Editor.Enabled = False
Case "MARKETINGZONELEVELID"
     e.Editor.Enabled = False
     MarketingZoneLevelID_Fill(e.Editor, TabMarketingZone_Grid.GetRowValues(e.VisibleIndex, "COUNTRYID")) 
     AddHandler DirectCast(e.Editor, ASPxComboBox).Callback, AddressOf MarketingZoneLevelID_OnCallback 
Case "MARKETINGZONEID"
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
Case "MARKETINGZONELEVELID"
     DirectCast(e.Editor, ASPxComboBox).DataBindItems() 
Case "RECORDSTATUS"
     DirectCast(e.Editor, ASPxComboBox).DataBindItems() 

        End Select
    End Sub

    Protected Sub TabMarketingZone_Grid_RowInserting(ByVal sender As Object, ByVal e As ASPxDataInsertingEventArgs) Handles TabMarketingZone_Grid.RowInserting 
        Dim isNullResult As Boolean = True
        
             With New DataManagerFactory("INSERT INTO ADDRESS.TabMarketingZone (COMPANYID, MARKETINGZONETABLEID, COUNTRYID, MARKETINGZONELEVELID, MARKETINGZONEID, RECORDSTATUS, CREATIONDATE, CREATORUSERCODE, UPDATEDATE, UPDATEUSERCODE) VALUES (@:COMPANYID, @:MARKETINGZONETABLEID, @:COUNTRYID, @:MARKETINGZONELEVELID, @:MARKETINGZONEID, @:RECORDSTATUS, SYSDATE, @:CREATORUSERCODE, SYSDATE, @:UPDATEUSERCODE)", "TabMarketingZone", "Linked.Address")                 
                                                   
                       .AddParameter("COMPANYID", DbType.Decimal, 0, False, e.NewValues("COMPANYID"))
.AddParameter("MARKETINGZONETABLEID", DbType.Decimal, 0, False, e.NewValues("MARKETINGZONETABLEID"))
.AddParameter("COUNTRYID", DbType.Decimal, 0, False, e.NewValues("COUNTRYID"))
.AddParameter("MARKETINGZONELEVELID", DbType.Decimal, 0, False, e.NewValues("MARKETINGZONELEVELID"))
.AddParameter("MARKETINGZONEID", DbType.AnsiString, 0, False, e.NewValues("MARKETINGZONEID"))
.AddParameter("RECORDSTATUS", DbType.AnsiString, 0, False, e.NewValues("RECORDSTATUS"))
.AddParameter("CREATORUSERCODE", DbType.Decimal, 0, False, Session("nUsercode"))
.AddParameter("UPDATEUSERCODE", DbType.Decimal, 0, False, Session("nUsercode"))
            
                       .CommandExecute()
                End With
        For Each languageItem In InMotionGIT.Common.Proxy.Helpers.Language.LanguageToDictionary


            With New DataManagerFactory("INSERT INTO ADDRESS.TransMarketingZone (COMPANYID, MARKETINGZONETABLEID, COUNTRYID, MARKETINGZONELEVELID, MARKETINGZONEID, LANGUAGEID, DESCRIPTION, SHORTDESCRIPTION, CANCELLATIONDATE, CREATIONDATE, CREATORUSERCODE, UPDATEDATE, UPDATEUSERCODE) VALUES (@:COMPANYID, @:MARKETINGZONETABLEID, @:COUNTRYID, @:MARKETINGZONELEVELID, @:MARKETINGZONEID, @:LANGUAGEID, @:DESCRIPTION, @:SHORTDESCRIPTION, @:CANCELLATIONDATE, SYSDATE, @:CREATORUSERCODE, SYSDATE, @:UPDATEUSERCODE)", "TransMarketingZone", "Linked.Address")

                .AddParameter("COMPANYID", DbType.Decimal, 0, False, e.NewValues("COMPANYID"))
                .AddParameter("MARKETINGZONETABLEID", DbType.Decimal, 0, False, e.NewValues("MARKETINGZONETABLEID"))
                .AddParameter("COUNTRYID", DbType.Decimal, 0, False, e.NewValues("COUNTRYID"))
                .AddParameter("MARKETINGZONELEVELID", DbType.Decimal, 0, False, e.NewValues("MARKETINGZONELEVELID"))
                .AddParameter("MARKETINGZONEID", DbType.AnsiString, 0, False, e.NewValues("MARKETINGZONEID"))
                .AddParameter("LANGUAGEID", DbType.Decimal, 0, False, languageItem.Key)
                .AddParameter("DESCRIPTION", DbType.AnsiString, 0, False, e.NewValues("DESCRIPTION"))
                .AddParameter("SHORTDESCRIPTION", DbType.AnsiString, 0, False, e.NewValues("SHORTDESCRIPTION"))
                .AddParameter("CANCELLATIONDATE", DbType.DateTime, 0, (e.NewValues("CANCELLATIONDATE") = Date.MinValue), e.NewValues("CANCELLATIONDATE"))
                .AddParameter("CREATORUSERCODE", DbType.Decimal, 0, False, Session("nUsercode"))
                .AddParameter("UPDATEUSERCODE", DbType.Decimal, 0, False, Session("nUsercode"))

                .CommandExecute()
            End With
        Next

               
        e.Cancel = True
        TabMarketingZone_Grid.CancelEdit()
    End Sub

    Protected Sub TabMarketingZone_Grid_RowUpdating(ByVal sender As Object, ByVal e As ASPxDataUpdatingEventArgs) Handles TabMarketingZone_Grid.RowUpdating
        Dim isNullResult As Boolean = True
          
             With New DataManagerFactory("UPDATE ADDRESS.TabMarketingZone SET RECORDSTATUS = @:RECORDSTATUS, UPDATEDATE = SYSDATE, UPDATEUSERCODE = @:UPDATEUSERCODE WHERE COMPANYID = @:COMPANYID AND MARKETINGZONETABLEID = @:MARKETINGZONETABLEID AND COUNTRYID = @:COUNTRYID AND MARKETINGZONELEVELID = @:MARKETINGZONELEVELID AND MARKETINGZONEID = @:MARKETINGZONEID", "TabMarketingZone", "Linked.Address")                 
                                                   
                       .AddParameter("RECORDSTATUS", DbType.AnsiString, 0, False, e.NewValues("RECORDSTATUS"))
.AddParameter("UPDATEUSERCODE", DbType.Decimal, 0, False, Session("nUsercode"))
.AddParameter("COMPANYID", DbType.Decimal, 0, False, e.Keys("COMPANYID"))
.AddParameter("MARKETINGZONETABLEID", DbType.Decimal, 0, False, e.Keys("MARKETINGZONETABLEID"))
.AddParameter("COUNTRYID", DbType.Decimal, 0, False, e.Keys("COUNTRYID"))
.AddParameter("MARKETINGZONELEVELID", DbType.Decimal, 0, False, e.Keys("MARKETINGZONELEVELID"))
.AddParameter("MARKETINGZONEID", DbType.AnsiString, 0, False, e.Keys("MARKETINGZONEID"))
            
                       .CommandExecute()
                End With
     With New DataManagerFactory("UPDATE ADDRESS.TransMarketingZone SET DESCRIPTION = @:DESCRIPTION, SHORTDESCRIPTION = @:SHORTDESCRIPTION, CANCELLATIONDATE = @:CANCELLATIONDATE, UPDATEDATE = SYSDATE, UPDATEUSERCODE = @:UPDATEUSERCODE WHERE COMPANYID = @:COMPANYID AND MARKETINGZONETABLEID = @:MARKETINGZONETABLEID AND COUNTRYID = @:COUNTRYID AND MARKETINGZONELEVELID = @:MARKETINGZONELEVELID AND MARKETINGZONEID = @:MARKETINGZONEID AND LANGUAGEID = @:LANGUAGEID", "TransMarketingZone", "Linked.Address")                 
                                                   
                       .AddParameter("DESCRIPTION", DbType.AnsiString, 0, False, e.NewValues("DESCRIPTION"))
.AddParameter("SHORTDESCRIPTION", DbType.AnsiString, 0, False, e.NewValues("SHORTDESCRIPTION"))
.AddParameter("CANCELLATIONDATE", DbType.DateTime, 0, (e.NewValues("CANCELLATIONDATE") = Date.MinValue), e.NewValues("CANCELLATIONDATE"))
.AddParameter("UPDATEUSERCODE", DbType.Decimal, 0, False, Session("nUsercode"))
.AddParameter("COMPANYID", DbType.Decimal, 0, False, e.Keys("COMPANYID"))
.AddParameter("MARKETINGZONETABLEID", DbType.Decimal, 0, False, e.Keys("MARKETINGZONETABLEID"))
.AddParameter("COUNTRYID", DbType.Decimal, 0, False, e.Keys("COUNTRYID"))
.AddParameter("MARKETINGZONELEVELID", DbType.Decimal, 0, False, e.Keys("MARKETINGZONELEVELID"))
.AddParameter("MARKETINGZONEID", DbType.AnsiString, 0, False, e.Keys("MARKETINGZONEID"))
.AddParameter("LANGUAGEID", DbType.Decimal, 0, False, CurrentState.Get("Language"))
            
                       .CommandExecute()
                End With         
      
         e.Cancel = True
        TabMarketingZone_Grid.CancelEdit()
    End Sub
    
    Protected Sub TabMarketingZone_Grid_CustomCallback(sender As Object, e As ASPxGridViewCustomCallbackEventArgs) Handles TabMarketingZone_Grid.CustomCallback     
           Dim isNullResult As Boolean = True
           
           Select Case e.Parameters.ToString.ToLower
               Case "delete"
                       Dim COMPANYIDKey As Generic.List(Of Object) = TabMarketingZone_Grid.GetSelectedFieldValues("COMPANYID")
 Dim MARKETINGZONETABLEIDKey As Generic.List(Of Object) = TabMarketingZone_Grid.GetSelectedFieldValues("MARKETINGZONETABLEID")
 Dim COUNTRYIDKey As Generic.List(Of Object) = TabMarketingZone_Grid.GetSelectedFieldValues("COUNTRYID")
 Dim MARKETINGZONELEVELIDKey As Generic.List(Of Object) = TabMarketingZone_Grid.GetSelectedFieldValues("MARKETINGZONELEVELID")
 Dim MARKETINGZONEIDKey As Generic.List(Of Object) = TabMarketingZone_Grid.GetSelectedFieldValues("MARKETINGZONEID")
        
               For index As Integer = 0 To COMPANYIDKey.Count - 1
                  With New DataManagerFactory("DELETE FROM ADDRESS.TransMarketingZone WHERE COMPANYID = @:COMPANYID AND MARKETINGZONETABLEID = @:MARKETINGZONETABLEID AND COUNTRYID = @:COUNTRYID AND MARKETINGZONELEVELID = @:MARKETINGZONELEVELID AND MARKETINGZONEID = @:MARKETINGZONEID ", "TransMarketingZone", "Linked.Address")                 
                                                   
               .AddParameter("COMPANYID", DbType.Decimal, 0, False, COMPANYIDKey(index))
.AddParameter("MARKETINGZONETABLEID", DbType.Decimal, 0, False, MARKETINGZONETABLEIDKey(index))
.AddParameter("COUNTRYID", DbType.Decimal, 0, False, COUNTRYIDKey(index))
.AddParameter("MARKETINGZONELEVELID", DbType.Decimal, 0, False, MARKETINGZONELEVELIDKey(index))
.AddParameter("MARKETINGZONEID", DbType.AnsiString, 0, False, MARKETINGZONEIDKey(index))
            
               .CommandExecute()
          End With 
 With New DataManagerFactory("DELETE FROM ADDRESS.TabMarketingZone WHERE COMPANYID = @:COMPANYID AND MARKETINGZONETABLEID = @:MARKETINGZONETABLEID AND COUNTRYID = @:COUNTRYID AND MARKETINGZONELEVELID = @:MARKETINGZONELEVELID AND MARKETINGZONEID = @:MARKETINGZONEID ", "TabMarketingZone", "Linked.Address")                 
                                                   
               .AddParameter("COMPANYID", DbType.Decimal, 0, False, COMPANYIDKey(index))
.AddParameter("MARKETINGZONETABLEID", DbType.Decimal, 0, False, MARKETINGZONETABLEIDKey(index))
.AddParameter("COUNTRYID", DbType.Decimal, 0, False, COUNTRYIDKey(index))
.AddParameter("MARKETINGZONELEVELID", DbType.Decimal, 0, False, MARKETINGZONELEVELIDKey(index))
.AddParameter("MARKETINGZONEID", DbType.AnsiString, 0, False, MARKETINGZONEIDKey(index))
            
               .CommandExecute()
          End With 
                       
              Next           
           
              TabMarketingZone_Grid.DataBind()
                 
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
    
    Protected Sub TabMarketingZone_Grid_RowValidating(sender As Object, e As ASPxDataValidationEventArgs) Handles TabMarketingZone_Grid.RowValidating

        
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
 
#Region "TransMarketingZone Events"
    
    Protected Sub TransMarketingZone_CustomColumnDisplayText(sender As Object, e As DevExpress.Web.ASPxGridView.ASPxGridViewColumnDisplayTextEventArgs) Handles TransMarketingZone.CustomColumnDisplayText
          Dim data As DataTable
          Dim rows() As DataRow
           
          Select Case e.Column.FieldName

               Case Else              
           End Select
    End Sub
  
    Protected Sub TransMarketingZone_DataBinding(ByVal sender As Object, ByVal e As EventArgs) Handles TransMarketingZone.DataBinding
        If (Not IsNothing(Request.Params("__CALLBACKID")) AndAlso Request.Params("__CALLBACKID").EndsWith("TransMarketingZone")) Or _internalCall Then
                       If Caching.Exist("TabCompany") Then
                DirectCast(TransMarketingZone.Columns("COMPANYID"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = Caching.GetItem("TabCompany")

            Else
                Dim source As DataTable = Nothing

                With New DataManagerFactory("SELECT  TABCOMPANY.COMPANYID, TABCOMPANY.RECORDSTATUS, TRANSCOMPANY.LANGUAGEID, TRANSCOMPANY.DESCRIPTION FROM COMMON.TABCOMPANY TABCOMPANY JOIN COMMON.TRANSCOMPANY TRANSCOMPANY ON TRANSCOMPANY.COMPANYID = TABCOMPANY.COMPANYID  WHERE TABCOMPANY.RECORDSTATUS = '1' AND TRANSCOMPANY.LANGUAGEID = @:LANGUAGEID ", "TabCompany", "Linked.Address")
                    .AddParameter("LANGUAGEID", DbType.Decimal, 5, False, CurrentState.Get("Language"))
 
                    source = .QueryExecuteToTable(True)
                    DirectCast(TransMarketingZone.Columns("COMPANYID"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = source
                End With

                If Not IsNothing(source) AndAlso source.Rows.Count > 0 Then
                    Caching.SetItem("TabCompany", source)
                End If
            End If 
             If Caching.Exist("TabCountry") Then
                DirectCast(TransMarketingZone.Columns("COUNTRYID"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = Caching.GetItem("TabCountry")

            Else
                Dim source As DataTable = Nothing

                With New DataManagerFactory("SELECT  TABCOUNTRY.COUNTRYID, TABCOUNTRY.RECORDSTATUS, TRANSCOUNTRY.LANGUAGEID, TRANSCOUNTRY.DESCRIPTION FROM COMMON.TABCOUNTRY TABCOUNTRY JOIN COMMON.TRANSCOUNTRY TRANSCOUNTRY ON TRANSCOUNTRY.COUNTRYID = TABCOUNTRY.COUNTRYID  WHERE TABCOUNTRY.RECORDSTATUS = '1' AND TRANSCOUNTRY.LANGUAGEID = @:LANGUAGEID ", "TabCountry", "Linked.Address")
                    .AddParameter("LANGUAGEID", DbType.Decimal, 5, False, CurrentState.Get("Language"))
 
                    source = .QueryExecuteToTable(True)
                    DirectCast(TransMarketingZone.Columns("COUNTRYID"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = source
                End With

                If Not IsNothing(source) AndAlso source.Rows.Count > 0 Then
                    Caching.SetItem("TabCountry", source)
                End If
            End If 
             If Caching.Exist("TabMarketingZoneNames") Then
                DirectCast(TransMarketingZone.Columns("MARKETINGZONELEVELID"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = Caching.GetItem("TabMarketingZoneNames")

            Else
                Dim source As DataTable = Nothing

                With New DataManagerFactory("SELECT  TABMARKETINGZONENAMES.COUNTRYID, TABMARKETINGZONENAMES.MARKETINGZONELEVELID, TABMARKETINGZONENAMES.RECORDSTATUS, TRANSMARKETINGZONENAMES.LANGUAGEID, TRANSMARKETINGZONENAMES.DESCRIPTION FROM ADDRESS.TABMARKETINGZONENAMES TABMARKETINGZONENAMES JOIN ADDRESS.TRANSMARKETINGZONENAMES TRANSMARKETINGZONENAMES ON TRANSMARKETINGZONENAMES.COUNTRYID = TABMARKETINGZONENAMES.COUNTRYID  AND TRANSMARKETINGZONENAMES.MARKETINGZONELEVELID = TABMARKETINGZONENAMES.MARKETINGZONELEVELID  WHERE TABMARKETINGZONENAMES.RECORDSTATUS = '1' AND TRANSMARKETINGZONENAMES.LANGUAGEID = @:LANGUAGEID ", "TabMarketingZoneNames", "Linked.Address")
                    .AddParameter("LANGUAGEID", DbType.Decimal, 5, False, CurrentState.Get("Language"))
 
                    source = .QueryExecuteToTable(True)
                    DirectCast(TransMarketingZone.Columns("MARKETINGZONELEVELID"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = source
                End With

                If Not IsNothing(source) AndAlso source.Rows.Count > 0 Then
                    Caching.SetItem("TabMarketingZoneNames", source)
                End If
            End If 
             If Caching.Exist("EnumRecordStatus") Then
                DirectCast(TransMarketingZone.Columns("RECORDSTATUS"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = Caching.GetItem("EnumRecordStatus")

            Else
                Dim source As DataTable = Nothing

                With New DataManagerFactory("SELECT  ENUMRECORDSTATUS.RECORDSTATUS, ETRANRECORDSTATUS.LANGUAGEID, ETRANRECORDSTATUS.DESCRIPTION FROM COMMON.ENUMRECORDSTATUS ENUMRECORDSTATUS JOIN COMMON.ETRANRECORDSTATUS ETRANRECORDSTATUS ON ETRANRECORDSTATUS.RECORDSTATUS = ENUMRECORDSTATUS.RECORDSTATUS  WHERE ETRANRECORDSTATUS.LANGUAGEID = @:LANGUAGEID ", "EnumRecordStatus", "Linked.Address")
                    .AddParameter("LANGUAGEID", DbType.Decimal, 5, False, CurrentState.Get("Language"))
 
                    source = .QueryExecuteToTable(True)
                    DirectCast(TransMarketingZone.Columns("RECORDSTATUS"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = source
                End With

                If Not IsNothing(source) AndAlso source.Rows.Count > 0 Then
                    Caching.SetItem("EnumRecordStatus", source)
                End If
            End If 
             If Caching.Exist("TabLanguage") Then
                DirectCast(TransMarketingZone.Columns("LANGUAGEID"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = Caching.GetItem("TabLanguage")

            Else
                Dim source As DataTable = Nothing

                With New DataManagerFactory("SELECT  TABLANGUAGE.LANGUAGEID, TABLANGUAGE.RECORDSTATUS, TRANSLANGUAGE.LANGUAGECODEID, TRANSLANGUAGE.DESCRIPTION FROM COMMON.TABLANGUAGE TABLANGUAGE JOIN COMMON.TRANSLANGUAGE TRANSLANGUAGE ON TRANSLANGUAGE.LANGUAGECODEID = TABLANGUAGE.LANGUAGEID  WHERE TABLANGUAGE.RECORDSTATUS = '1' AND TRANSLANGUAGE.LANGUAGECODEID = @:LANGUAGECODEID ", "TabLanguage", "Linked.Address")
                    .AddParameter("LANGUAGECODEID", DbType.Decimal, 5, False, CurrentState.Get("Language"))
 
                    source = .QueryExecuteToTable(True)
                    DirectCast(TransMarketingZone.Columns("LANGUAGEID"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = source
                End With

                If Not IsNothing(source) AndAlso source.Rows.Count > 0 Then
                    Caching.SetItem("TabLanguage", source)
                End If
            End If 
 

                 With New DataManagerFactory("SELECT  TABMARKETINGZONE.COMPANYID, TABMARKETINGZONE.MARKETINGZONETABLEID, TABMARKETINGZONE.COUNTRYID, TABMARKETINGZONE.MARKETINGZONELEVELID, TABMARKETINGZONE.MARKETINGZONEID, TABMARKETINGZONE.RECORDSTATUS, TRANSMARKETINGZONE.COMPANYID, TRANSMARKETINGZONE.MARKETINGZONETABLEID, TRANSMARKETINGZONE.COUNTRYID, TRANSMARKETINGZONE.MARKETINGZONELEVELID, TRANSMARKETINGZONE.MARKETINGZONEID, TRANSMARKETINGZONE.LANGUAGEID, TRANSMARKETINGZONE.DESCRIPTION, TRANSMARKETINGZONE.SHORTDESCRIPTION, TRANSMARKETINGZONE.CANCELLATIONDATE FROM ADDRESS.TABMARKETINGZONE TABMARKETINGZONE JOIN ADDRESS.TRANSMARKETINGZONE TRANSMARKETINGZONE ON TRANSMARKETINGZONE.COMPANYID = TABMARKETINGZONE.COMPANYID  AND TRANSMARKETINGZONE.MARKETINGZONETABLEID = TABMARKETINGZONE.MARKETINGZONETABLEID  AND TRANSMARKETINGZONE.COUNTRYID = TABMARKETINGZONE.COUNTRYID  AND TRANSMARKETINGZONE.MARKETINGZONELEVELID = TABMARKETINGZONE.MARKETINGZONELEVELID  AND TRANSMARKETINGZONE.MARKETINGZONEID = TABMARKETINGZONE.MARKETINGZONEID  ", "TabMarketingZone", "Linked.Address")                 
                                                   
                                  
                      TransMarketingZone.DataSource = .QueryExecuteToTable(True)
                 End With 
        End If     
    End Sub

    Protected Sub TransMarketingZone_CellEditorInitialize(sender As Object, e As ASPxGridViewEditorEventArgs) Handles TransMarketingZone.CellEditorInitialize
        If TransMarketingZone.IsNewRowEditing Then
            Select Case e.Column.FieldName
                
                
                
                Case "COMPANYID"                     
                       e.Editor.Focus()               
            End Select

        Else

            Select Case e.Column.FieldName
                Case "COMPANYID"
     e.Editor.Enabled = False
Case "MARKETINGZONETABLEID"
     e.Editor.Enabled = False
Case "COUNTRYID"
     e.Editor.Enabled = False
Case "MARKETINGZONELEVELID"
     e.Editor.Enabled = False
Case "MARKETINGZONEID"
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
Case "MARKETINGZONELEVELID"
     DirectCast(e.Editor, ASPxComboBox).DataBindItems() 
Case "RECORDSTATUS"
     DirectCast(e.Editor, ASPxComboBox).DataBindItems() 
Case "LANGUAGEID"
     DirectCast(e.Editor, ASPxComboBox).DataBindItems() 

        End Select
    End Sub

    Protected Sub TransMarketingZone_RowInserting(ByVal sender As Object, ByVal e As ASPxDataInsertingEventArgs) Handles TransMarketingZone.RowInserting 
        Dim isNullResult As Boolean = True
        
        
               
        e.Cancel = True
        TransMarketingZone.CancelEdit()
    End Sub

    Protected Sub TransMarketingZone_RowUpdating(ByVal sender As Object, ByVal e As ASPxDataUpdatingEventArgs) Handles TransMarketingZone.RowUpdating
        Dim isNullResult As Boolean = True
          
             With New DataManagerFactory("UPDATE ADDRESS.TransMarketingZone SET DESCRIPTION = @:DESCRIPTION, SHORTDESCRIPTION = @:SHORTDESCRIPTION, CANCELLATIONDATE = @:CANCELLATIONDATE, UPDATEDATE = SYSDATE, UPDATEUSERCODE = @:UPDATEUSERCODE WHERE COMPANYID = @:COMPANYID AND MARKETINGZONETABLEID = @:MARKETINGZONETABLEID AND COUNTRYID = @:COUNTRYID AND MARKETINGZONELEVELID = @:MARKETINGZONELEVELID AND MARKETINGZONEID = @:MARKETINGZONEID AND LANGUAGEID = @:LANGUAGEID", "TransMarketingZone", "Linked.Address")                 
                                                   
                       .AddParameter("DESCRIPTION", DbType.AnsiString, 0, False, e.NewValues("DESCRIPTION"))
.AddParameter("SHORTDESCRIPTION", DbType.AnsiString, 0, False, e.NewValues("SHORTDESCRIPTION"))
.AddParameter("CANCELLATIONDATE", DbType.DateTime, 0, (e.NewValues("CANCELLATIONDATE") = Date.MinValue), e.NewValues("CANCELLATIONDATE"))
.AddParameter("UPDATEUSERCODE", DbType.Decimal, 0, False, Session("nUsercode"))
.AddParameter("COMPANYID", DbType.Decimal, 0, False, e.Keys("COMPANYID"))
.AddParameter("MARKETINGZONETABLEID", DbType.Decimal, 0, False, e.Keys("MARKETINGZONETABLEID"))
.AddParameter("COUNTRYID", DbType.Decimal, 0, False, e.Keys("COUNTRYID"))
.AddParameter("MARKETINGZONELEVELID", DbType.Decimal, 0, False, e.Keys("MARKETINGZONELEVELID"))
.AddParameter("MARKETINGZONEID", DbType.AnsiString, 0, False, e.Keys("MARKETINGZONEID"))
.AddParameter("LANGUAGEID", DbType.Decimal, 0, False, e.Keys("LANGUAGEID"))
            
                       .CommandExecute()
                End With         
      
         e.Cancel = True
        TransMarketingZone.CancelEdit()
    End Sub
    
    Protected Sub TransMarketingZone_CustomCallback(sender As Object, e As ASPxGridViewCustomCallbackEventArgs) Handles TransMarketingZone.CustomCallback     
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
    
    Protected Sub TransMarketingZone_RowValidating(sender As Object, e As ASPxDataValidationEventArgs) Handles TransMarketingZone.RowValidating

        
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
 

#Region "MarketingZoneLevelID Events"

    Private Sub MarketingZoneLevelID_OnCallback(ByVal source As Object, ByVal e As CallbackEventArgsBase)
        MarketingZoneLevelID_Fill(DirectCast(source, ASPxComboBox), e.Parameter)
    End Sub

    Private Sub MarketingZoneLevelID_Fill(control As ASPxComboBox, CountryID As Integer)
          With New DataManagerFactory("SELECT  TABMARKETINGZONENAMES.COUNTRYID, TABMARKETINGZONENAMES.MARKETINGZONELEVELID, TABMARKETINGZONENAMES.RECORDSTATUS, TRANSMARKETINGZONENAMES.LANGUAGEID, TRANSMARKETINGZONENAMES.DESCRIPTION FROM ADDRESS.TABMARKETINGZONENAMES TABMARKETINGZONENAMES JOIN ADDRESS.TRANSMARKETINGZONENAMES TRANSMARKETINGZONENAMES ON TRANSMARKETINGZONENAMES.COUNTRYID = TABMARKETINGZONENAMES.COUNTRYID  AND TRANSMARKETINGZONENAMES.MARKETINGZONELEVELID = TABMARKETINGZONENAMES.MARKETINGZONELEVELID  WHERE TABMARKETINGZONENAMES.RECORDSTATUS = '1' AND TRANSMARKETINGZONENAMES.LANGUAGEID = @:LANGUAGEID  AND TABMARKETINGZONENAMES.COUNTRYID = @:DependencyTABMARKETINGZON", "TabMarketingZoneNames", "Linked.Address")                 
                                                   
               .AddParameter("LANGUAGEID", DbType.Decimal, 5, False, CurrentState.Get("Language"))
.AddParameter("DependencyTabMarketingZon", DbType.Int32, 0, False, CountryID)
  
               
               control.DataSource = .QueryExecuteToTable(True)
               control.DataBindItems()
          End With         
    End Sub
    
#End Region 
 
 
End Class