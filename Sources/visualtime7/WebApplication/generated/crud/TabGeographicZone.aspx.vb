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

Partial Class Maintenance_TabGeographicZone
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

            TabGeographicZone_Grid.DataBind()
        End If      
    End Sub

#End Region

#Region "Controls Events"

         
    
 

#End Region

#Region "TabGeographicZone_Grid Events"
    
    Protected Sub TabGeographicZone_Grid_CustomColumnDisplayText(sender As Object, e As DevExpress.Web.ASPxGridView.ASPxGridViewColumnDisplayTextEventArgs) Handles TabGeographicZone_Grid.CustomColumnDisplayText
          Dim data As DataTable
          Dim rows() As DataRow
           
          Select Case e.Column.FieldName
            Case "ZIPCODE"
                data = DirectCast(TabGeographicZone_Grid.Columns("ZIPCODE"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource 
                rows = data.Select(String.Format("COUNTRYID = {0} AND ZIPCODE = '{1}'", e.GetFieldValue("COUNTRYID"), e.Value)) 
                If rows.Count > 0 Then 
                    e.DisplayText = rows(0)("ZIPCODE") 
                End If 

               Case Else              
           End Select
    End Sub
  
    Protected Sub TabGeographicZone_Grid_DataBinding(ByVal sender As Object, ByVal e As EventArgs) Handles TabGeographicZone_Grid.DataBinding
        If (Not IsNothing(Request.Params("__CALLBACKID")) AndAlso Request.Params("__CALLBACKID").EndsWith("TabGeographicZone_Grid")) Or _internalCall Then
                       If Caching.Exist("TabCountry") Then
                DirectCast(TabGeographicZone_Grid.Columns("COUNTRYID"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = Caching.GetItem("TabCountry")

            Else
                Dim source As DataTable = Nothing

                With New DataManagerFactory("SELECT  TABCOUNTRY.COUNTRYID, TABCOUNTRY.RECORDSTATUS, TRANSCOUNTRY.LANGUAGEID, TRANSCOUNTRY.DESCRIPTION FROM COMMON.TABCOUNTRY TABCOUNTRY JOIN COMMON.TRANSCOUNTRY TRANSCOUNTRY ON TRANSCOUNTRY.COUNTRYID = TABCOUNTRY.COUNTRYID  WHERE TABCOUNTRY.RECORDSTATUS = '1' AND TRANSCOUNTRY.LANGUAGEID = @:LANGUAGEID ", "TabCountry", "Linked.Address")
                    .AddParameter("LANGUAGEID", DbType.Decimal, 5, False, CurrentState.Get("Language"))
 
                    source = .QueryExecuteToTable(True)
                    DirectCast(TabGeographicZone_Grid.Columns("COUNTRYID"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = source
                End With

                If Not IsNothing(source) AndAlso source.Rows.Count > 0 Then
                    Caching.SetItem("TabCountry", source)
                End If
            End If 
             If Caching.Exist("TabGeographicZoneNames") Then
                DirectCast(TabGeographicZone_Grid.Columns("GEOGRAPHICZONELEVELID"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = Caching.GetItem("TabGeographicZoneNames")

            Else
                Dim source As DataTable = Nothing

                With New DataManagerFactory("SELECT  TABGEOGRAPHICZONENAMES.COUNTRYID, TABGEOGRAPHICZONENAMES.GEOGRAPHICZONELEVELID, TABGEOGRAPHICZONENAMES.RECORDSTATUS, TRANSGEOGRAPHICZONENAMES.LANGUAGEID, TRANSGEOGRAPHICZONENAMES.DESCRIPTION FROM ADDRESS.TABGEOGRAPHICZONENAMES TABGEOGRAPHICZONENAMES JOIN ADDRESS.TRANSGEOGRAPHICZONENAMES TRANSGEOGRAPHICZONENAMES ON TRANSGEOGRAPHICZONENAMES.COUNTRYID = TABGEOGRAPHICZONENAMES.COUNTRYID  AND TRANSGEOGRAPHICZONENAMES.GEOGRAPHICZONELEVELID = TABGEOGRAPHICZONENAMES.GEOGRAPHICZONELEVELID  WHERE TABGEOGRAPHICZONENAMES.RECORDSTATUS = '1' AND TRANSGEOGRAPHICZONENAMES.LANGUAGEID = @:LANGUAGEID ", "TabGeographicZoneNames", "Linked.Address")
                    .AddParameter("LANGUAGEID", DbType.Decimal, 5, False, CurrentState.Get("Language"))
 
                    source = .QueryExecuteToTable(True)
                    DirectCast(TabGeographicZone_Grid.Columns("GEOGRAPHICZONELEVELID"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = source
                End With

                If Not IsNothing(source) AndAlso source.Rows.Count > 0 Then
                    Caching.SetItem("TabGeographicZoneNames", source)
                End If
            End If 
             If Caching.Exist("TabZipCode") Then
                DirectCast(TabGeographicZone_Grid.Columns("ZIPCODE"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = Caching.GetItem("TabZipCode")

            Else
                Dim source As DataTable = Nothing

                With New DataManagerFactory("SELECT  TABZIPCODE.COUNTRYID, TABZIPCODE.ZIPCODE, TABZIPCODE.RECORDSTATUS FROM ADDRESS.TABZIPCODE TABZIPCODE  WHERE TABZIPCODE.RECORDSTATUS = '1' ", "TabZipCode", "Linked.Address")
                     
                    source = .QueryExecuteToTable(True)
                    DirectCast(TabGeographicZone_Grid.Columns("ZIPCODE"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = source
                End With

                If Not IsNothing(source) AndAlso source.Rows.Count > 0 Then
                    Caching.SetItem("TabZipCode", source)
                End If
            End If 
             If Caching.Exist("EnumRecordStatus") Then
                DirectCast(TabGeographicZone_Grid.Columns("RECORDSTATUS"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = Caching.GetItem("EnumRecordStatus")

            Else
                Dim source As DataTable = Nothing

                With New DataManagerFactory("SELECT  ENUMRECORDSTATUS.RECORDSTATUS, ETRANRECORDSTATUS.LANGUAGEID, ETRANRECORDSTATUS.DESCRIPTION FROM COMMON.ENUMRECORDSTATUS ENUMRECORDSTATUS JOIN COMMON.ETRANRECORDSTATUS ETRANRECORDSTATUS ON ETRANRECORDSTATUS.RECORDSTATUS = ENUMRECORDSTATUS.RECORDSTATUS  WHERE ETRANRECORDSTATUS.LANGUAGEID = @:LANGUAGEID ", "EnumRecordStatus", "Linked.Address")
                    .AddParameter("LANGUAGEID", DbType.Decimal, 5, False, CurrentState.Get("Language"))
 
                    source = .QueryExecuteToTable(True)
                    DirectCast(TabGeographicZone_Grid.Columns("RECORDSTATUS"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = source
                End With

                If Not IsNothing(source) AndAlso source.Rows.Count > 0 Then
                    Caching.SetItem("EnumRecordStatus", source)
                End If
            End If 
 

                 With New DataManagerFactory("SELECT  TABGEOGRAPHICZONE.COUNTRYID, TABGEOGRAPHICZONE.GEOGRAPHICZONETABLEID, TABGEOGRAPHICZONE.GEOGRAPHICZONELEVELID, TABGEOGRAPHICZONE.GEOGRAPHICZONEID, TABGEOGRAPHICZONE.ZIPCODE, TABGEOGRAPHICZONE.RECORDSTATUS, TRANSGEOGRAPHICZONE.COUNTRYID, TRANSGEOGRAPHICZONE.GEOGRAPHICZONETABLEID, TRANSGEOGRAPHICZONE.GEOGRAPHICZONELEVELID, TRANSGEOGRAPHICZONE.GEOGRAPHICZONEID, TRANSGEOGRAPHICZONE.LANGUAGEID, TRANSGEOGRAPHICZONE.DESCRIPTION, TRANSGEOGRAPHICZONE.SHORTDESCRIPTION FROM ADDRESS.TABGEOGRAPHICZONE TABGEOGRAPHICZONE JOIN ADDRESS.TRANSGEOGRAPHICZONE TRANSGEOGRAPHICZONE ON TRANSGEOGRAPHICZONE.COUNTRYID = TABGEOGRAPHICZONE.COUNTRYID  AND TRANSGEOGRAPHICZONE.GEOGRAPHICZONETABLEID = TABGEOGRAPHICZONE.GEOGRAPHICZONETABLEID  AND TRANSGEOGRAPHICZONE.GEOGRAPHICZONELEVELID = TABGEOGRAPHICZONE.GEOGRAPHICZONELEVELID  AND TRANSGEOGRAPHICZONE.GEOGRAPHICZONEID = TABGEOGRAPHICZONE.GEOGRAPHICZONEID  WHERE TRANSGEOGRAPHICZONE.LANGUAGEID = @:LANGUAGEID", "TabGeographicZone", "Linked.Address")                 
                                                   
                      .AddParameter("LANGUAGEID", DbType.Decimal, 5, False, CurrentState.Get("Language"))
            
                      TabGeographicZone_Grid.DataSource = .QueryExecuteToTable(True)
                 End With 
        End If     
    End Sub

    Protected Sub TabGeographicZone_Grid_CellEditorInitialize(sender As Object, e As ASPxGridViewEditorEventArgs) Handles TabGeographicZone_Grid.CellEditorInitialize
        If TabGeographicZone_Grid.IsNewRowEditing Then
            Select Case e.Column.FieldName
                
                Case "ZIPCODE"
     AddHandler DirectCast(e.Editor, ASPxComboBox).Callback, AddressOf ZipCode_OnCallback 

                
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
                   
                
                
                Case "ZIPCODE"                     
                     e.Editor.Focus()
            End Select
        End If
        
       Select Case e.Column.FieldName
           Case "COUNTRYID"
                 DirectCast(e.Editor, ASPxComboBox).DataBindItems()
                 
           Case "GEOGRAPHICZONELEVELID"
     DirectCast(e.Editor, ASPxComboBox).DataBindItems() 
Case "ZIPCODE"
     DirectCast(e.Editor, ASPxComboBox).DataBindItems() 
Case "RECORDSTATUS"
     DirectCast(e.Editor, ASPxComboBox).DataBindItems() 

        End Select
    End Sub

    Protected Sub TabGeographicZone_Grid_RowInserting(ByVal sender As Object, ByVal e As ASPxDataInsertingEventArgs) Handles TabGeographicZone_Grid.RowInserting 
        Dim isNullResult As Boolean = True
        
             With New DataManagerFactory("INSERT INTO ADDRESS.TabGeographicZone (COUNTRYID, GEOGRAPHICZONETABLEID, GEOGRAPHICZONELEVELID, GEOGRAPHICZONEID, ZIPCODE, RECORDSTATUS, CREATIONDATE, CREATORUSERCODE, UPDATEDATE, UPDATEUSERCODE) VALUES (@:COUNTRYID, @:GEOGRAPHICZONETABLEID, @:GEOGRAPHICZONELEVELID, @:GEOGRAPHICZONEID, @:ZIPCODE, @:RECORDSTATUS, SYSDATE, @:CREATORUSERCODE, SYSDATE, @:UPDATEUSERCODE)", "TabGeographicZone", "Linked.Address")                 
                                                   
                       .AddParameter("COUNTRYID", DbType.Decimal, 0, False, e.NewValues("COUNTRYID"))
.AddParameter("GEOGRAPHICZONETABLEID", DbType.AnsiString, 0, False, e.NewValues("GEOGRAPHICZONETABLEID"))
.AddParameter("GEOGRAPHICZONELEVELID", DbType.Decimal, 0, False, e.NewValues("GEOGRAPHICZONELEVELID"))
.AddParameter("GEOGRAPHICZONEID", DbType.AnsiString, 0, False, e.NewValues("GEOGRAPHICZONEID"))
.AddParameter("ZIPCODE", DbType.AnsiString, 0, (e.NewValues("ZIPCODE") = String.Empty), e.NewValues("ZIPCODE"))
.AddParameter("RECORDSTATUS", DbType.AnsiString, 0, False, e.NewValues("RECORDSTATUS"))
.AddParameter("CREATORUSERCODE", DbType.Decimal, 0, False, Session("nUsercode"))
.AddParameter("UPDATEUSERCODE", DbType.Decimal, 0, False, Session("nUsercode"))
            
                       .CommandExecute()
                End With
        For Each languageItem In InMotionGIT.Common.Proxy.Helpers.Language.LanguageToDictionary


            With New DataManagerFactory("INSERT INTO ADDRESS.TransGeographicZone (COUNTRYID, GEOGRAPHICZONETABLEID, GEOGRAPHICZONELEVELID, GEOGRAPHICZONEID, LANGUAGEID, DESCRIPTION, SHORTDESCRIPTION, CREATORUSERCODE, CREATIONDATE, UPDATEDATE, UPDATEUSERCODE) VALUES (@:COUNTRYID, @:GEOGRAPHICZONETABLEID, @:GEOGRAPHICZONELEVELID, @:GEOGRAPHICZONEID, @:LANGUAGEID, @:DESCRIPTION, @:SHORTDESCRIPTION, @:CREATORUSERCODE, SYSDATE, SYSDATE, @:UPDATEUSERCODE)", "TransGeographicZone", "Linked.Address")

                .AddParameter("COUNTRYID", DbType.Decimal, 0, False, e.NewValues("COUNTRYID"))
                .AddParameter("GEOGRAPHICZONETABLEID", DbType.AnsiString, 0, False, e.NewValues("GEOGRAPHICZONETABLEID"))
                .AddParameter("GEOGRAPHICZONELEVELID", DbType.Decimal, 0, False, e.NewValues("GEOGRAPHICZONELEVELID"))
                .AddParameter("GEOGRAPHICZONEID", DbType.AnsiString, 0, False, e.NewValues("GEOGRAPHICZONEID"))
                .AddParameter("LANGUAGEID", DbType.Decimal, 0, False, languageItem.Key)
                .AddParameter("DESCRIPTION", DbType.AnsiString, 0, False, e.NewValues("DESCRIPTION"))
                .AddParameter("SHORTDESCRIPTION", DbType.AnsiString, 0, False, e.NewValues("SHORTDESCRIPTION"))
                .AddParameter("CREATORUSERCODE", DbType.Decimal, 0, False, Session("nUsercode"))
                .AddParameter("UPDATEUSERCODE", DbType.Decimal, 0, False, Session("nUsercode"))

                .CommandExecute()
            End With
        Next

               
        e.Cancel = True
        TabGeographicZone_Grid.CancelEdit()
    End Sub

    Protected Sub TabGeographicZone_Grid_RowUpdating(ByVal sender As Object, ByVal e As ASPxDataUpdatingEventArgs) Handles TabGeographicZone_Grid.RowUpdating
        Dim isNullResult As Boolean = True
          
             With New DataManagerFactory("UPDATE ADDRESS.TabGeographicZone SET ZIPCODE = @:ZIPCODE, RECORDSTATUS = @:RECORDSTATUS, UPDATEDATE = SYSDATE, UPDATEUSERCODE = @:UPDATEUSERCODE WHERE COUNTRYID = @:COUNTRYID AND GEOGRAPHICZONETABLEID = @:GEOGRAPHICZONETABLEID AND GEOGRAPHICZONELEVELID = @:GEOGRAPHICZONELEVELID AND GEOGRAPHICZONEID = @:GEOGRAPHICZONEID", "TabGeographicZone", "Linked.Address")                 
                                                   
                       .AddParameter("ZIPCODE", DbType.AnsiString, 0, (e.NewValues("ZIPCODE") = String.Empty), e.NewValues("ZIPCODE"))
.AddParameter("RECORDSTATUS", DbType.AnsiString, 0, False, e.NewValues("RECORDSTATUS"))
.AddParameter("UPDATEUSERCODE", DbType.Decimal, 0, False, Session("nUsercode"))
.AddParameter("COUNTRYID", DbType.Decimal, 0, False, e.Keys("COUNTRYID"))
.AddParameter("GEOGRAPHICZONETABLEID", DbType.AnsiString, 0, False, e.Keys("GEOGRAPHICZONETABLEID"))
.AddParameter("GEOGRAPHICZONELEVELID", DbType.Decimal, 0, False, e.Keys("GEOGRAPHICZONELEVELID"))
.AddParameter("GEOGRAPHICZONEID", DbType.AnsiString, 0, False, e.Keys("GEOGRAPHICZONEID"))
            
                       .CommandExecute()
                End With
     With New DataManagerFactory("UPDATE ADDRESS.TransGeographicZone SET DESCRIPTION = @:DESCRIPTION, SHORTDESCRIPTION = @:SHORTDESCRIPTION, UPDATEDATE = SYSDATE, UPDATEUSERCODE = @:UPDATEUSERCODE WHERE COUNTRYID = @:COUNTRYID AND GEOGRAPHICZONETABLEID = @:GEOGRAPHICZONETABLEID AND GEOGRAPHICZONELEVELID = @:GEOGRAPHICZONELEVELID AND GEOGRAPHICZONEID = @:GEOGRAPHICZONEID AND LANGUAGEID = @:LANGUAGEID", "TransGeographicZone", "Linked.Address")                 
                                                   
                       .AddParameter("DESCRIPTION", DbType.AnsiString, 0, False, e.NewValues("DESCRIPTION"))
.AddParameter("SHORTDESCRIPTION", DbType.AnsiString, 0, False, e.NewValues("SHORTDESCRIPTION"))
.AddParameter("UPDATEUSERCODE", DbType.Decimal, 0, False, Session("nUsercode"))
.AddParameter("COUNTRYID", DbType.Decimal, 0, False, e.Keys("COUNTRYID"))
.AddParameter("GEOGRAPHICZONETABLEID", DbType.AnsiString, 0, False, e.Keys("GEOGRAPHICZONETABLEID"))
.AddParameter("GEOGRAPHICZONELEVELID", DbType.Decimal, 0, False, e.Keys("GEOGRAPHICZONELEVELID"))
.AddParameter("GEOGRAPHICZONEID", DbType.AnsiString, 0, False, e.Keys("GEOGRAPHICZONEID"))
.AddParameter("LANGUAGEID", DbType.Decimal, 0, False, CurrentState.Get("Language"))
            
                       .CommandExecute()
                End With         
      
         e.Cancel = True
        TabGeographicZone_Grid.CancelEdit()
    End Sub
    
    Protected Sub TabGeographicZone_Grid_CustomCallback(sender As Object, e As ASPxGridViewCustomCallbackEventArgs) Handles TabGeographicZone_Grid.CustomCallback     
           Dim isNullResult As Boolean = True
           
           Select Case e.Parameters.ToString.ToLower
               Case "delete"
                       Dim COUNTRYIDKey As Generic.List(Of Object) = TabGeographicZone_Grid.GetSelectedFieldValues("COUNTRYID")
 Dim GEOGRAPHICZONETABLEIDKey As Generic.List(Of Object) = TabGeographicZone_Grid.GetSelectedFieldValues("GEOGRAPHICZONETABLEID")
 Dim GEOGRAPHICZONELEVELIDKey As Generic.List(Of Object) = TabGeographicZone_Grid.GetSelectedFieldValues("GEOGRAPHICZONELEVELID")
 Dim GEOGRAPHICZONEIDKey As Generic.List(Of Object) = TabGeographicZone_Grid.GetSelectedFieldValues("GEOGRAPHICZONEID")
        
               For index As Integer = 0 To COUNTRYIDKey.Count - 1
                  With New DataManagerFactory("DELETE FROM ADDRESS.TransGeographicZone WHERE COUNTRYID = @:COUNTRYID AND GEOGRAPHICZONETABLEID = @:GEOGRAPHICZONETABLEID AND GEOGRAPHICZONELEVELID = @:GEOGRAPHICZONELEVELID AND GEOGRAPHICZONEID = @:GEOGRAPHICZONEID ", "TransGeographicZone", "Linked.Address")                 
                                                   
               .AddParameter("COUNTRYID", DbType.Decimal, 0, False, COUNTRYIDKey(index))
.AddParameter("GEOGRAPHICZONETABLEID", DbType.AnsiString, 0, False, GEOGRAPHICZONETABLEIDKey(index))
.AddParameter("GEOGRAPHICZONELEVELID", DbType.Decimal, 0, False, GEOGRAPHICZONELEVELIDKey(index))
.AddParameter("GEOGRAPHICZONEID", DbType.AnsiString, 0, False, GEOGRAPHICZONEIDKey(index))
            
               .CommandExecute()
          End With 
 With New DataManagerFactory("DELETE FROM ADDRESS.TabGeographicZone WHERE COUNTRYID = @:COUNTRYID AND GEOGRAPHICZONETABLEID = @:GEOGRAPHICZONETABLEID AND GEOGRAPHICZONELEVELID = @:GEOGRAPHICZONELEVELID AND GEOGRAPHICZONEID = @:GEOGRAPHICZONEID ", "TabGeographicZone", "Linked.Address")                 
                                                   
               .AddParameter("COUNTRYID", DbType.Decimal, 0, False, COUNTRYIDKey(index))
.AddParameter("GEOGRAPHICZONETABLEID", DbType.AnsiString, 0, False, GEOGRAPHICZONETABLEIDKey(index))
.AddParameter("GEOGRAPHICZONELEVELID", DbType.Decimal, 0, False, GEOGRAPHICZONELEVELIDKey(index))
.AddParameter("GEOGRAPHICZONEID", DbType.AnsiString, 0, False, GEOGRAPHICZONEIDKey(index))
            
               .CommandExecute()
          End With 
                       
              Next           
           
              TabGeographicZone_Grid.DataBind()
                 
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
    
    Protected Sub TabGeographicZone_Grid_RowValidating(sender As Object, e As ASPxDataValidationEventArgs) Handles TabGeographicZone_Grid.RowValidating

        
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
 
#Region "TransGeographicZone Events"
    
    Protected Sub TransGeographicZone_CustomColumnDisplayText(sender As Object, e As DevExpress.Web.ASPxGridView.ASPxGridViewColumnDisplayTextEventArgs) Handles TransGeographicZone.CustomColumnDisplayText
          Dim data As DataTable
          Dim rows() As DataRow
           
          Select Case e.Column.FieldName

               Case Else              
           End Select
    End Sub
  
    Protected Sub TransGeographicZone_DataBinding(ByVal sender As Object, ByVal e As EventArgs) Handles TransGeographicZone.DataBinding
        If (Not IsNothing(Request.Params("__CALLBACKID")) AndAlso Request.Params("__CALLBACKID").EndsWith("TransGeographicZone")) Or _internalCall Then
                       If Caching.Exist("TabCountry") Then
                DirectCast(TransGeographicZone.Columns("COUNTRYID"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = Caching.GetItem("TabCountry")

            Else
                Dim source As DataTable = Nothing

                With New DataManagerFactory("SELECT  TABCOUNTRY.COUNTRYID, TABCOUNTRY.RECORDSTATUS, TRANSCOUNTRY.LANGUAGEID, TRANSCOUNTRY.DESCRIPTION FROM COMMON.TABCOUNTRY TABCOUNTRY JOIN COMMON.TRANSCOUNTRY TRANSCOUNTRY ON TRANSCOUNTRY.COUNTRYID = TABCOUNTRY.COUNTRYID  WHERE TABCOUNTRY.RECORDSTATUS = '1' AND TRANSCOUNTRY.LANGUAGEID = @:LANGUAGEID ", "TabCountry", "Linked.Address")
                    .AddParameter("LANGUAGEID", DbType.Decimal, 5, False, CurrentState.Get("Language"))
 
                    source = .QueryExecuteToTable(True)
                    DirectCast(TransGeographicZone.Columns("COUNTRYID"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = source
                End With

                If Not IsNothing(source) AndAlso source.Rows.Count > 0 Then
                    Caching.SetItem("TabCountry", source)
                End If
            End If 
             If Caching.Exist("TabGeographicZoneNames") Then
                DirectCast(TransGeographicZone.Columns("GEOGRAPHICZONELEVELID"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = Caching.GetItem("TabGeographicZoneNames")

            Else
                Dim source As DataTable = Nothing

                With New DataManagerFactory("SELECT  TABGEOGRAPHICZONENAMES.COUNTRYID, TABGEOGRAPHICZONENAMES.GEOGRAPHICZONELEVELID, TABGEOGRAPHICZONENAMES.RECORDSTATUS, TRANSGEOGRAPHICZONENAMES.LANGUAGEID, TRANSGEOGRAPHICZONENAMES.DESCRIPTION FROM ADDRESS.TABGEOGRAPHICZONENAMES TABGEOGRAPHICZONENAMES JOIN ADDRESS.TRANSGEOGRAPHICZONENAMES TRANSGEOGRAPHICZONENAMES ON TRANSGEOGRAPHICZONENAMES.COUNTRYID = TABGEOGRAPHICZONENAMES.COUNTRYID  AND TRANSGEOGRAPHICZONENAMES.GEOGRAPHICZONELEVELID = TABGEOGRAPHICZONENAMES.GEOGRAPHICZONELEVELID  WHERE TABGEOGRAPHICZONENAMES.RECORDSTATUS = '1' AND TRANSGEOGRAPHICZONENAMES.LANGUAGEID = @:LANGUAGEID ", "TabGeographicZoneNames", "Linked.Address")
                    .AddParameter("LANGUAGEID", DbType.Decimal, 5, False, CurrentState.Get("Language"))
 
                    source = .QueryExecuteToTable(True)
                    DirectCast(TransGeographicZone.Columns("GEOGRAPHICZONELEVELID"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = source
                End With

                If Not IsNothing(source) AndAlso source.Rows.Count > 0 Then
                    Caching.SetItem("TabGeographicZoneNames", source)
                End If
            End If 
             If Caching.Exist("TabZipCode") Then
                DirectCast(TransGeographicZone.Columns("ZIPCODE"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = Caching.GetItem("TabZipCode")

            Else
                Dim source As DataTable = Nothing

                With New DataManagerFactory("SELECT  TABZIPCODE.COUNTRYID, TABZIPCODE.ZIPCODE, TABZIPCODE.RECORDSTATUS FROM ADDRESS.TABZIPCODE TABZIPCODE  WHERE TABZIPCODE.RECORDSTATUS = '1' ", "TabZipCode", "Linked.Address")
                     
                    source = .QueryExecuteToTable(True)
                    DirectCast(TransGeographicZone.Columns("ZIPCODE"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = source
                End With

                If Not IsNothing(source) AndAlso source.Rows.Count > 0 Then
                    Caching.SetItem("TabZipCode", source)
                End If
            End If 
             If Caching.Exist("EnumRecordStatus") Then
                DirectCast(TransGeographicZone.Columns("RECORDSTATUS"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = Caching.GetItem("EnumRecordStatus")

            Else
                Dim source As DataTable = Nothing

                With New DataManagerFactory("SELECT  ENUMRECORDSTATUS.RECORDSTATUS, ETRANRECORDSTATUS.LANGUAGEID, ETRANRECORDSTATUS.DESCRIPTION FROM COMMON.ENUMRECORDSTATUS ENUMRECORDSTATUS JOIN COMMON.ETRANRECORDSTATUS ETRANRECORDSTATUS ON ETRANRECORDSTATUS.RECORDSTATUS = ENUMRECORDSTATUS.RECORDSTATUS  WHERE ETRANRECORDSTATUS.LANGUAGEID = @:LANGUAGEID ", "EnumRecordStatus", "Linked.Address")
                    .AddParameter("LANGUAGEID", DbType.Decimal, 5, False, CurrentState.Get("Language"))
 
                    source = .QueryExecuteToTable(True)
                    DirectCast(TransGeographicZone.Columns("RECORDSTATUS"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = source
                End With

                If Not IsNothing(source) AndAlso source.Rows.Count > 0 Then
                    Caching.SetItem("EnumRecordStatus", source)
                End If
            End If 
             If Caching.Exist("TabLanguage") Then
                DirectCast(TransGeographicZone.Columns("LANGUAGEID"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = Caching.GetItem("TabLanguage")

            Else
                Dim source As DataTable = Nothing

                With New DataManagerFactory("SELECT  TABLANGUAGE.LANGUAGEID, TABLANGUAGE.RECORDSTATUS, TRANSLANGUAGE.LANGUAGECODEID, TRANSLANGUAGE.DESCRIPTION FROM COMMON.TABLANGUAGE TABLANGUAGE JOIN COMMON.TRANSLANGUAGE TRANSLANGUAGE ON TRANSLANGUAGE.LANGUAGECODEID = TABLANGUAGE.LANGUAGEID  WHERE TABLANGUAGE.RECORDSTATUS = '1' AND TRANSLANGUAGE.LANGUAGECODEID = @:LANGUAGECODEID ", "TabLanguage", "Linked.Address")
                    .AddParameter("LANGUAGECODEID", DbType.Decimal, 5, False, CurrentState.Get("Language"))
 
                    source = .QueryExecuteToTable(True)
                    DirectCast(TransGeographicZone.Columns("LANGUAGEID"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = source
                End With

                If Not IsNothing(source) AndAlso source.Rows.Count > 0 Then
                    Caching.SetItem("TabLanguage", source)
                End If
            End If 
 

                 With New DataManagerFactory("SELECT  TABGEOGRAPHICZONE.COUNTRYID, TABGEOGRAPHICZONE.GEOGRAPHICZONETABLEID, TABGEOGRAPHICZONE.GEOGRAPHICZONELEVELID, TABGEOGRAPHICZONE.GEOGRAPHICZONEID, TABGEOGRAPHICZONE.ZIPCODE, TABGEOGRAPHICZONE.RECORDSTATUS, TRANSGEOGRAPHICZONE.COUNTRYID, TRANSGEOGRAPHICZONE.GEOGRAPHICZONETABLEID, TRANSGEOGRAPHICZONE.GEOGRAPHICZONELEVELID, TRANSGEOGRAPHICZONE.GEOGRAPHICZONEID, TRANSGEOGRAPHICZONE.LANGUAGEID, TRANSGEOGRAPHICZONE.DESCRIPTION, TRANSGEOGRAPHICZONE.SHORTDESCRIPTION FROM ADDRESS.TABGEOGRAPHICZONE TABGEOGRAPHICZONE JOIN ADDRESS.TRANSGEOGRAPHICZONE TRANSGEOGRAPHICZONE ON TRANSGEOGRAPHICZONE.COUNTRYID = TABGEOGRAPHICZONE.COUNTRYID  AND TRANSGEOGRAPHICZONE.GEOGRAPHICZONETABLEID = TABGEOGRAPHICZONE.GEOGRAPHICZONETABLEID  AND TRANSGEOGRAPHICZONE.GEOGRAPHICZONELEVELID = TABGEOGRAPHICZONE.GEOGRAPHICZONELEVELID  AND TRANSGEOGRAPHICZONE.GEOGRAPHICZONEID = TABGEOGRAPHICZONE.GEOGRAPHICZONEID  ", "TabGeographicZone", "Linked.Address")                 
                                                   
                                  
                      TransGeographicZone.DataSource = .QueryExecuteToTable(True)
                 End With 
        End If     
    End Sub

    Protected Sub TransGeographicZone_CellEditorInitialize(sender As Object, e As ASPxGridViewEditorEventArgs) Handles TransGeographicZone.CellEditorInitialize
        If TransGeographicZone.IsNewRowEditing Then
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
Case "LANGUAGEID"
     e.Editor.Enabled = False
                   
                
                
                Case "ZIPCODE"                     
                     e.Editor.Focus()
            End Select
        End If
        
       Select Case e.Column.FieldName
           Case "COUNTRYID"
                 DirectCast(e.Editor, ASPxComboBox).DataBindItems()
                 
           Case "GEOGRAPHICZONELEVELID"
     DirectCast(e.Editor, ASPxComboBox).DataBindItems() 
Case "ZIPCODE"
     DirectCast(e.Editor, ASPxComboBox).DataBindItems() 
Case "RECORDSTATUS"
     DirectCast(e.Editor, ASPxComboBox).DataBindItems() 
Case "LANGUAGEID"
     DirectCast(e.Editor, ASPxComboBox).DataBindItems() 

        End Select
    End Sub

    Protected Sub TransGeographicZone_RowInserting(ByVal sender As Object, ByVal e As ASPxDataInsertingEventArgs) Handles TransGeographicZone.RowInserting 
        Dim isNullResult As Boolean = True
        
        
               
        e.Cancel = True
        TransGeographicZone.CancelEdit()
    End Sub

    Protected Sub TransGeographicZone_RowUpdating(ByVal sender As Object, ByVal e As ASPxDataUpdatingEventArgs) Handles TransGeographicZone.RowUpdating
        Dim isNullResult As Boolean = True
          
             With New DataManagerFactory("UPDATE ADDRESS.TransGeographicZone SET DESCRIPTION = @:DESCRIPTION, SHORTDESCRIPTION = @:SHORTDESCRIPTION, UPDATEDATE = SYSDATE, UPDATEUSERCODE = @:UPDATEUSERCODE WHERE COUNTRYID = @:COUNTRYID AND GEOGRAPHICZONETABLEID = @:GEOGRAPHICZONETABLEID AND GEOGRAPHICZONELEVELID = @:GEOGRAPHICZONELEVELID AND GEOGRAPHICZONEID = @:GEOGRAPHICZONEID AND LANGUAGEID = @:LANGUAGEID", "TransGeographicZone", "Linked.Address")                 
                                                   
                       .AddParameter("DESCRIPTION", DbType.AnsiString, 0, False, e.NewValues("DESCRIPTION"))
.AddParameter("SHORTDESCRIPTION", DbType.AnsiString, 0, False, e.NewValues("SHORTDESCRIPTION"))
.AddParameter("UPDATEUSERCODE", DbType.Decimal, 0, False, Session("nUsercode"))
.AddParameter("COUNTRYID", DbType.Decimal, 0, False, e.Keys("COUNTRYID"))
.AddParameter("GEOGRAPHICZONETABLEID", DbType.AnsiString, 0, False, e.Keys("GEOGRAPHICZONETABLEID"))
.AddParameter("GEOGRAPHICZONELEVELID", DbType.Decimal, 0, False, e.Keys("GEOGRAPHICZONELEVELID"))
.AddParameter("GEOGRAPHICZONEID", DbType.AnsiString, 0, False, e.Keys("GEOGRAPHICZONEID"))
.AddParameter("LANGUAGEID", DbType.Decimal, 0, False, e.Keys("LANGUAGEID"))
            
                       .CommandExecute()
                End With         
      
         e.Cancel = True
        TransGeographicZone.CancelEdit()
    End Sub
    
    Protected Sub TransGeographicZone_CustomCallback(sender As Object, e As ASPxGridViewCustomCallbackEventArgs) Handles TransGeographicZone.CustomCallback     
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
    
    Protected Sub TransGeographicZone_RowValidating(sender As Object, e As ASPxDataValidationEventArgs) Handles TransGeographicZone.RowValidating

        
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
 

#Region "ZipCode Events"

    Private Sub ZipCode_OnCallback(ByVal source As Object, ByVal e As CallbackEventArgsBase)
        ZipCode_Fill(DirectCast(source, ASPxComboBox), e.Parameter)
    End Sub

    Private Sub ZipCode_Fill(control As ASPxComboBox, CountryID As Integer)
          With New DataManagerFactory("SELECT  TABZIPCODE.COUNTRYID, TABZIPCODE.ZIPCODE, TABZIPCODE.RECORDSTATUS FROM ADDRESS.TABZIPCODE TABZIPCODE  WHERE TABZIPCODE.RECORDSTATUS = '1'  AND TABZIPCODE.COUNTRYID = @:DependencyTABZIPCODECOUNT", "TabZipCode", "Linked.Address")                 
                                                   
               .AddParameter("DependencyTabZipCodeCount", DbType.Int32, 0, False, CountryID)
  
               
               control.DataSource = .QueryExecuteToTable(True)
               control.DataBindItems()
          End With         
    End Sub
    
#End Region 
 
 
End Class