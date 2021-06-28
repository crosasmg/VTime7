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

Partial Class Maintenance_TabPartsOfAddressNamesDetail
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

            TabPartsOfAddressNamesDetail_Grid.DataBind()
        End If      
    End Sub

#End Region

#Region "Controls Events"

         
    
 

#End Region

#Region "TabPartsOfAddressNamesDetail_Grid Events"
    
    Protected Sub TabPartsOfAddressNamesDetail_Grid_CustomColumnDisplayText(sender As Object, e As DevExpress.Web.ASPxGridView.ASPxGridViewColumnDisplayTextEventArgs) Handles TabPartsOfAddressNamesDetail_Grid.CustomColumnDisplayText
          Dim data As DataTable
          Dim rows() As DataRow
           
          Select Case e.Column.FieldName

               Case Else              
           End Select
    End Sub
  
    Protected Sub TabPartsOfAddressNamesDetail_Grid_DataBinding(ByVal sender As Object, ByVal e As EventArgs) Handles TabPartsOfAddressNamesDetail_Grid.DataBinding
        If (Not IsNothing(Request.Params("__CALLBACKID")) AndAlso Request.Params("__CALLBACKID").EndsWith("TabPartsOfAddressNamesDetail_Grid")) Or _internalCall Then
                       If Caching.Exist("TabCountry") Then
                DirectCast(TabPartsOfAddressNamesDetail_Grid.Columns("COUNTRYID"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = Caching.GetItem("TabCountry")

            Else
                Dim source As DataTable = Nothing

                With New DataManagerFactory("SELECT  TABCOUNTRY.COUNTRYID, TABCOUNTRY.RECORDSTATUS, TRANSCOUNTRY.LANGUAGEID, TRANSCOUNTRY.DESCRIPTION FROM COMMON.TABCOUNTRY TABCOUNTRY JOIN COMMON.TRANSCOUNTRY TRANSCOUNTRY ON TRANSCOUNTRY.COUNTRYID = TABCOUNTRY.COUNTRYID  WHERE TABCOUNTRY.RECORDSTATUS = '1' AND TRANSCOUNTRY.LANGUAGEID = @:LANGUAGEID ", "TabCountry", "Linked.Address")
                    .AddParameter("LANGUAGEID", DbType.Decimal, 5, False, CurrentState.Get("Language"))
 
                    source = .QueryExecuteToTable(True)
                    DirectCast(TabPartsOfAddressNamesDetail_Grid.Columns("COUNTRYID"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = source
                End With

                If Not IsNothing(source) AndAlso source.Rows.Count > 0 Then
                    Caching.SetItem("TabCountry", source)
                End If
            End If 
             If Caching.Exist("EnumTypeOfRoute") Then
                DirectCast(TabPartsOfAddressNamesDetail_Grid.Columns("TYPEOFROUTE"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = Caching.GetItem("EnumTypeOfRoute")

            Else
                Dim source As DataTable = Nothing

                With New DataManagerFactory("SELECT  ENUMTYPEOFROUTE.TYPEOFROUTE, ENUMTYPEOFROUTE.RECORDSTATUS, ETRANTYPEOFROUTE.LANGUAGEID, ETRANTYPEOFROUTE.DESCRIPTION FROM ADDRESS.ENUMTYPEOFROUTE ENUMTYPEOFROUTE JOIN ADDRESS.ETRANTYPEOFROUTE ETRANTYPEOFROUTE ON ETRANTYPEOFROUTE.TYPEOFROUTE = ENUMTYPEOFROUTE.TYPEOFROUTE  WHERE ENUMTYPEOFROUTE.RECORDSTATUS = '1' AND ETRANTYPEOFROUTE.LANGUAGEID = @:LANGUAGEID ", "EnumTypeOfRoute", "Linked.Address")
                    .AddParameter("LANGUAGEID", DbType.Decimal, 5, False, CurrentState.Get("Language"))
 
                    source = .QueryExecuteToTable(True)
                    DirectCast(TabPartsOfAddressNamesDetail_Grid.Columns("TYPEOFROUTE"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = source
                End With

                If Not IsNothing(source) AndAlso source.Rows.Count > 0 Then
                    Caching.SetItem("EnumTypeOfRoute", source)
                End If
            End If 
             If Caching.Exist("EnumRecordStatus") Then
                DirectCast(TabPartsOfAddressNamesDetail_Grid.Columns("RECORDSTATUS"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = Caching.GetItem("EnumRecordStatus")

            Else
                Dim source As DataTable = Nothing

                With New DataManagerFactory("SELECT  ENUMRECORDSTATUS.RECORDSTATUS, ETRANRECORDSTATUS.LANGUAGEID, ETRANRECORDSTATUS.DESCRIPTION FROM COMMON.ENUMRECORDSTATUS ENUMRECORDSTATUS JOIN COMMON.ETRANRECORDSTATUS ETRANRECORDSTATUS ON ETRANRECORDSTATUS.RECORDSTATUS = ENUMRECORDSTATUS.RECORDSTATUS  WHERE ETRANRECORDSTATUS.LANGUAGEID = @:LANGUAGEID ", "EnumRecordStatus", "Linked.Address")
                    .AddParameter("LANGUAGEID", DbType.Decimal, 5, False, CurrentState.Get("Language"))
 
                    source = .QueryExecuteToTable(True)
                    DirectCast(TabPartsOfAddressNamesDetail_Grid.Columns("RECORDSTATUS"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = source
                End With

                If Not IsNothing(source) AndAlso source.Rows.Count > 0 Then
                    Caching.SetItem("EnumRecordStatus", source)
                End If
            End If 
 

                 With New DataManagerFactory("SELECT  TABPARTSOFADDRESSNAMESDETAIL.COUNTRYID, TABPARTSOFADDRESSNAMESDETAIL.TYPEOFROUTE, TABPARTSOFADDRESSNAMESDETAIL.PARTNAMEID, TABPARTSOFADDRESSNAMESDETAIL.PARTNAMEDETAILID, TABPARTSOFADDRESSNAMESDETAIL.RECORDSTATUS, TRANSPARTSOFADDRESSNAMESDETAIL.COUNTRYID, TRANSPARTSOFADDRESSNAMESDETAIL.TYPEOFROUTE, TRANSPARTSOFADDRESSNAMESDETAIL.PARTNAMEID, TRANSPARTSOFADDRESSNAMESDETAIL.PARTNAMEDETAILID, TRANSPARTSOFADDRESSNAMESDETAIL.LANGUAGEID, TRANSPARTSOFADDRESSNAMESDETAIL.DESCRIPTION, TRANSPARTSOFADDRESSNAMESDETAIL.SHORTDESCRIPTION FROM ADDRESS.TABPARTSOFADDRESSNAMESDETAIL TABPARTSOFADDRESSNAMESDETAIL JOIN ADDRESS.TRANSPARTSOFADDRESSNAMESDETAIL TRANSPARTSOFADDRESSNAMESDETAIL ON TRANSPARTSOFADDRESSNAMESDETAIL.COUNTRYID = TABPARTSOFADDRESSNAMESDETAIL.COUNTRYID  AND TRANSPARTSOFADDRESSNAMESDETAIL.TYPEOFROUTE = TABPARTSOFADDRESSNAMESDETAIL.TYPEOFROUTE  AND TRANSPARTSOFADDRESSNAMESDETAIL.PARTNAMEID = TABPARTSOFADDRESSNAMESDETAIL.PARTNAMEID  AND TRANSPARTSOFADDRESSNAMESDETAIL.PARTNAMEDETAILID = TABPARTSOFADDRESSNAMESDETAIL.PARTNAMEDETAILID  WHERE TRANSPARTSOFADDRESSNAMESDETAIL.LANGUAGEID = @:LANGUAGEID", "TabPartsOfAddressNamesDetail", "Linked.Address")                 
                                                   
                      .AddParameter("LANGUAGEID", DbType.Decimal, 5, False, CurrentState.Get("Language"))
            
                      TabPartsOfAddressNamesDetail_Grid.DataSource = .QueryExecuteToTable(True)
                 End With 
        End If     
    End Sub

    Protected Sub TabPartsOfAddressNamesDetail_Grid_CellEditorInitialize(sender As Object, e As ASPxGridViewEditorEventArgs) Handles TabPartsOfAddressNamesDetail_Grid.CellEditorInitialize
        If TabPartsOfAddressNamesDetail_Grid.IsNewRowEditing Then
            Select Case e.Column.FieldName
                
                
                
                Case "COUNTRYID"                     
                       e.Editor.Focus()               
            End Select

        Else

            Select Case e.Column.FieldName
                Case "COUNTRYID"
     e.Editor.Enabled = False
Case "TYPEOFROUTE"
     e.Editor.Enabled = False
Case "PARTNAMEID"
     e.Editor.Enabled = False
Case "PARTNAMEDETAILID"
     e.Editor.Enabled = False
                   
                
                
                Case "RECORDSTATUS"                     
                     e.Editor.Focus()
            End Select
        End If
        
       Select Case e.Column.FieldName
           Case "COUNTRYID"
                 DirectCast(e.Editor, ASPxComboBox).DataBindItems()
                 
           Case "TYPEOFROUTE"
     DirectCast(e.Editor, ASPxComboBox).DataBindItems() 
Case "RECORDSTATUS"
     DirectCast(e.Editor, ASPxComboBox).DataBindItems() 

        End Select
    End Sub

    Protected Sub TabPartsOfAddressNamesDetail_Grid_RowInserting(ByVal sender As Object, ByVal e As ASPxDataInsertingEventArgs) Handles TabPartsOfAddressNamesDetail_Grid.RowInserting 
        Dim isNullResult As Boolean = True
        
             With New DataManagerFactory("INSERT INTO ADDRESS.TabPartsOfAddressNamesDetail (COUNTRYID, TYPEOFROUTE, PARTNAMEID, PARTNAMEDETAILID, RECORDSTATUS, CREATIONDATE, CREATORUSERCODE, UPDATEDATE, UPDATEUSERCODE) VALUES (@:COUNTRYID, @:TYPEOFROUTE, @:PARTNAMEID, @:PARTNAMEDETAILID, @:RECORDSTATUS, SYSDATE, @:CREATORUSERCODE, SYSDATE, @:UPDATEUSERCODE)", "TabPartsOfAddressNamesDetail", "Linked.Address")                 
                                                   
                       .AddParameter("COUNTRYID", DbType.Decimal, 0, False, e.NewValues("COUNTRYID"))
.AddParameter("TYPEOFROUTE", DbType.Decimal, 0, False, e.NewValues("TYPEOFROUTE"))
.AddParameter("PARTNAMEID", DbType.Decimal, 0, False, e.NewValues("PARTNAMEID"))
.AddParameter("PARTNAMEDETAILID", DbType.Decimal, 0, False, e.NewValues("PARTNAMEDETAILID"))
.AddParameter("RECORDSTATUS", DbType.AnsiString, 0, False, e.NewValues("RECORDSTATUS"))
.AddParameter("CREATORUSERCODE", DbType.Decimal, 0, False, Session("nUsercode"))
.AddParameter("UPDATEUSERCODE", DbType.Decimal, 0, False, Session("nUsercode"))
            
                       .CommandExecute()
                End With
        For Each languageItem In InMotionGIT.Common.Proxy.Helpers.Language.LanguageToDictionary


            With New DataManagerFactory("INSERT INTO ADDRESS.TransPartsOfAddressNamesDetail (COUNTRYID, TYPEOFROUTE, PARTNAMEID, PARTNAMEDETAILID, LANGUAGEID, DESCRIPTION, SHORTDESCRIPTION, CREATIONDATE, CREATORUSERCODE, UPDATEDATE, UPDATEUSERCODE) VALUES (@:COUNTRYID, @:TYPEOFROUTE, @:PARTNAMEID, @:PARTNAMEDETAILID, @:LANGUAGEID, @:DESCRIPTION, @:SHORTDESCRIPTION, SYSDATE, @:CREATORUSERCODE, SYSDATE, @:UPDATEUSERCODE)", "TransPartsOfAddressNamesDetail", "Linked.Address")

                .AddParameter("COUNTRYID", DbType.Decimal, 0, False, e.NewValues("COUNTRYID"))
                .AddParameter("TYPEOFROUTE", DbType.Decimal, 0, False, e.NewValues("TYPEOFROUTE"))
                .AddParameter("PARTNAMEID", DbType.Decimal, 0, False, e.NewValues("PARTNAMEID"))
                .AddParameter("PARTNAMEDETAILID", DbType.Decimal, 0, False, e.NewValues("PARTNAMEDETAILID"))
                .AddParameter("LANGUAGEID", DbType.Decimal, 0, False, languageItem.Key)
                .AddParameter("DESCRIPTION", DbType.AnsiString, 0, False, e.NewValues("DESCRIPTION"))
                .AddParameter("SHORTDESCRIPTION", DbType.AnsiString, 0, False, e.NewValues("SHORTDESCRIPTION"))
                .AddParameter("CREATORUSERCODE", DbType.Decimal, 0, False, Session("nUsercode"))
                .AddParameter("UPDATEUSERCODE", DbType.Decimal, 0, False, Session("nUsercode"))

                .CommandExecute()
            End With
        Next

               
        e.Cancel = True
        TabPartsOfAddressNamesDetail_Grid.CancelEdit()
    End Sub

    Protected Sub TabPartsOfAddressNamesDetail_Grid_RowUpdating(ByVal sender As Object, ByVal e As ASPxDataUpdatingEventArgs) Handles TabPartsOfAddressNamesDetail_Grid.RowUpdating
        Dim isNullResult As Boolean = True
          
             With New DataManagerFactory("UPDATE ADDRESS.TabPartsOfAddressNamesDetail SET RECORDSTATUS = @:RECORDSTATUS, UPDATEDATE = SYSDATE, UPDATEUSERCODE = @:UPDATEUSERCODE WHERE COUNTRYID = @:COUNTRYID AND TYPEOFROUTE = @:TYPEOFROUTE AND PARTNAMEID = @:PARTNAMEID AND PARTNAMEDETAILID = @:PARTNAMEDETAILID", "TabPartsOfAddressNamesDetail", "Linked.Address")                 
                                                   
                       .AddParameter("RECORDSTATUS", DbType.AnsiString, 0, False, e.NewValues("RECORDSTATUS"))
.AddParameter("UPDATEUSERCODE", DbType.Decimal, 0, False, Session("nUsercode"))
.AddParameter("COUNTRYID", DbType.Decimal, 0, False, e.Keys("COUNTRYID"))
.AddParameter("TYPEOFROUTE", DbType.Decimal, 0, False, e.Keys("TYPEOFROUTE"))
.AddParameter("PARTNAMEID", DbType.Decimal, 0, False, e.Keys("PARTNAMEID"))
.AddParameter("PARTNAMEDETAILID", DbType.Decimal, 0, False, e.Keys("PARTNAMEDETAILID"))
            
                       .CommandExecute()
                End With
     With New DataManagerFactory("UPDATE ADDRESS.TransPartsOfAddressNamesDetail SET DESCRIPTION = @:DESCRIPTION, SHORTDESCRIPTION = @:SHORTDESCRIPTION, UPDATEDATE = SYSDATE, UPDATEUSERCODE = @:UPDATEUSERCODE WHERE COUNTRYID = @:COUNTRYID AND TYPEOFROUTE = @:TYPEOFROUTE AND PARTNAMEID = @:PARTNAMEID AND PARTNAMEDETAILID = @:PARTNAMEDETAILID AND LANGUAGEID = @:LANGUAGEID", "TransPartsOfAddressNamesDetail", "Linked.Address")                 
                                                   
                       .AddParameter("DESCRIPTION", DbType.AnsiString, 0, False, e.NewValues("DESCRIPTION"))
.AddParameter("SHORTDESCRIPTION", DbType.AnsiString, 0, False, e.NewValues("SHORTDESCRIPTION"))
.AddParameter("UPDATEUSERCODE", DbType.Decimal, 0, False, Session("nUsercode"))
.AddParameter("COUNTRYID", DbType.Decimal, 0, False, e.Keys("COUNTRYID"))
.AddParameter("TYPEOFROUTE", DbType.Decimal, 0, False, e.Keys("TYPEOFROUTE"))
.AddParameter("PARTNAMEID", DbType.Decimal, 0, False, e.Keys("PARTNAMEID"))
.AddParameter("PARTNAMEDETAILID", DbType.Decimal, 0, False, e.Keys("PARTNAMEDETAILID"))
.AddParameter("LANGUAGEID", DbType.Decimal, 0, False, CurrentState.Get("Language"))
            
                       .CommandExecute()
                End With         
      
         e.Cancel = True
        TabPartsOfAddressNamesDetail_Grid.CancelEdit()
    End Sub
    
    Protected Sub TabPartsOfAddressNamesDetail_Grid_CustomCallback(sender As Object, e As ASPxGridViewCustomCallbackEventArgs) Handles TabPartsOfAddressNamesDetail_Grid.CustomCallback     
           Dim isNullResult As Boolean = True
           
           Select Case e.Parameters.ToString.ToLower
               Case "delete"
                       Dim COUNTRYIDKey As Generic.List(Of Object) = TabPartsOfAddressNamesDetail_Grid.GetSelectedFieldValues("COUNTRYID")
 Dim TYPEOFROUTEKey As Generic.List(Of Object) = TabPartsOfAddressNamesDetail_Grid.GetSelectedFieldValues("TYPEOFROUTE")
 Dim PARTNAMEIDKey As Generic.List(Of Object) = TabPartsOfAddressNamesDetail_Grid.GetSelectedFieldValues("PARTNAMEID")
 Dim PARTNAMEDETAILIDKey As Generic.List(Of Object) = TabPartsOfAddressNamesDetail_Grid.GetSelectedFieldValues("PARTNAMEDETAILID")
        
               For index As Integer = 0 To COUNTRYIDKey.Count - 1
                  With New DataManagerFactory("DELETE FROM ADDRESS.TransPartsOfAddressNamesDetail WHERE COUNTRYID = @:COUNTRYID AND TYPEOFROUTE = @:TYPEOFROUTE AND PARTNAMEID = @:PARTNAMEID AND PARTNAMEDETAILID = @:PARTNAMEDETAILID ", "TransPartsOfAddressNamesDetail", "Linked.Address")                 
                                                   
               .AddParameter("COUNTRYID", DbType.Decimal, 0, False, COUNTRYIDKey(index))
.AddParameter("TYPEOFROUTE", DbType.Decimal, 0, False, TYPEOFROUTEKey(index))
.AddParameter("PARTNAMEID", DbType.Decimal, 0, False, PARTNAMEIDKey(index))
.AddParameter("PARTNAMEDETAILID", DbType.Decimal, 0, False, PARTNAMEDETAILIDKey(index))
            
               .CommandExecute()
          End With 
 With New DataManagerFactory("DELETE FROM ADDRESS.TabPartsOfAddressNamesDetail WHERE COUNTRYID = @:COUNTRYID AND TYPEOFROUTE = @:TYPEOFROUTE AND PARTNAMEID = @:PARTNAMEID AND PARTNAMEDETAILID = @:PARTNAMEDETAILID ", "TabPartsOfAddressNamesDetail", "Linked.Address")                 
                                                   
               .AddParameter("COUNTRYID", DbType.Decimal, 0, False, COUNTRYIDKey(index))
.AddParameter("TYPEOFROUTE", DbType.Decimal, 0, False, TYPEOFROUTEKey(index))
.AddParameter("PARTNAMEID", DbType.Decimal, 0, False, PARTNAMEIDKey(index))
.AddParameter("PARTNAMEDETAILID", DbType.Decimal, 0, False, PARTNAMEDETAILIDKey(index))
            
               .CommandExecute()
          End With 
                       
              Next           
           
              TabPartsOfAddressNamesDetail_Grid.DataBind()
                 
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
    
    Protected Sub TabPartsOfAddressNamesDetail_Grid_RowValidating(sender As Object, e As ASPxDataValidationEventArgs) Handles TabPartsOfAddressNamesDetail_Grid.RowValidating

        
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
 
#Region "TransPartsOfAddressNamesDetail Events"
    
    Protected Sub TransPartsOfAddressNamesDetail_CustomColumnDisplayText(sender As Object, e As DevExpress.Web.ASPxGridView.ASPxGridViewColumnDisplayTextEventArgs) Handles TransPartsOfAddressNamesDetail.CustomColumnDisplayText
          Dim data As DataTable
          Dim rows() As DataRow
           
          Select Case e.Column.FieldName

               Case Else              
           End Select
    End Sub
  
    Protected Sub TransPartsOfAddressNamesDetail_DataBinding(ByVal sender As Object, ByVal e As EventArgs) Handles TransPartsOfAddressNamesDetail.DataBinding
        If (Not IsNothing(Request.Params("__CALLBACKID")) AndAlso Request.Params("__CALLBACKID").EndsWith("TransPartsOfAddressNamesDetail")) Or _internalCall Then
                       If Caching.Exist("TabCountry") Then
                DirectCast(TransPartsOfAddressNamesDetail.Columns("COUNTRYID"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = Caching.GetItem("TabCountry")

            Else
                Dim source As DataTable = Nothing

                With New DataManagerFactory("SELECT  TABCOUNTRY.COUNTRYID, TABCOUNTRY.RECORDSTATUS, TRANSCOUNTRY.LANGUAGEID, TRANSCOUNTRY.DESCRIPTION FROM COMMON.TABCOUNTRY TABCOUNTRY JOIN COMMON.TRANSCOUNTRY TRANSCOUNTRY ON TRANSCOUNTRY.COUNTRYID = TABCOUNTRY.COUNTRYID  WHERE TABCOUNTRY.RECORDSTATUS = '1' AND TRANSCOUNTRY.LANGUAGEID = @:LANGUAGEID ", "TabCountry", "Linked.Address")
                    .AddParameter("LANGUAGEID", DbType.Decimal, 5, False, CurrentState.Get("Language"))
 
                    source = .QueryExecuteToTable(True)
                    DirectCast(TransPartsOfAddressNamesDetail.Columns("COUNTRYID"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = source
                End With

                If Not IsNothing(source) AndAlso source.Rows.Count > 0 Then
                    Caching.SetItem("TabCountry", source)
                End If
            End If 
             If Caching.Exist("EnumTypeOfRoute") Then
                DirectCast(TransPartsOfAddressNamesDetail.Columns("TYPEOFROUTE"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = Caching.GetItem("EnumTypeOfRoute")

            Else
                Dim source As DataTable = Nothing

                With New DataManagerFactory("SELECT  ENUMTYPEOFROUTE.TYPEOFROUTE, ENUMTYPEOFROUTE.RECORDSTATUS, ETRANTYPEOFROUTE.LANGUAGEID, ETRANTYPEOFROUTE.DESCRIPTION FROM ADDRESS.ENUMTYPEOFROUTE ENUMTYPEOFROUTE JOIN ADDRESS.ETRANTYPEOFROUTE ETRANTYPEOFROUTE ON ETRANTYPEOFROUTE.TYPEOFROUTE = ENUMTYPEOFROUTE.TYPEOFROUTE  WHERE ENUMTYPEOFROUTE.RECORDSTATUS = '1' AND ETRANTYPEOFROUTE.LANGUAGEID = @:LANGUAGEID ", "EnumTypeOfRoute", "Linked.Address")
                    .AddParameter("LANGUAGEID", DbType.Decimal, 5, False, CurrentState.Get("Language"))
 
                    source = .QueryExecuteToTable(True)
                    DirectCast(TransPartsOfAddressNamesDetail.Columns("TYPEOFROUTE"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = source
                End With

                If Not IsNothing(source) AndAlso source.Rows.Count > 0 Then
                    Caching.SetItem("EnumTypeOfRoute", source)
                End If
            End If 
             If Caching.Exist("EnumRecordStatus") Then
                DirectCast(TransPartsOfAddressNamesDetail.Columns("RECORDSTATUS"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = Caching.GetItem("EnumRecordStatus")

            Else
                Dim source As DataTable = Nothing

                With New DataManagerFactory("SELECT  ENUMRECORDSTATUS.RECORDSTATUS, ETRANRECORDSTATUS.LANGUAGEID, ETRANRECORDSTATUS.DESCRIPTION FROM COMMON.ENUMRECORDSTATUS ENUMRECORDSTATUS JOIN COMMON.ETRANRECORDSTATUS ETRANRECORDSTATUS ON ETRANRECORDSTATUS.RECORDSTATUS = ENUMRECORDSTATUS.RECORDSTATUS  WHERE ETRANRECORDSTATUS.LANGUAGEID = @:LANGUAGEID ", "EnumRecordStatus", "Linked.Address")
                    .AddParameter("LANGUAGEID", DbType.Decimal, 5, False, CurrentState.Get("Language"))
 
                    source = .QueryExecuteToTable(True)
                    DirectCast(TransPartsOfAddressNamesDetail.Columns("RECORDSTATUS"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = source
                End With

                If Not IsNothing(source) AndAlso source.Rows.Count > 0 Then
                    Caching.SetItem("EnumRecordStatus", source)
                End If
            End If 
             If Caching.Exist("TabLanguage") Then
                DirectCast(TransPartsOfAddressNamesDetail.Columns("LANGUAGEID"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = Caching.GetItem("TabLanguage")

            Else
                Dim source As DataTable = Nothing

                With New DataManagerFactory("SELECT  TABLANGUAGE.LANGUAGEID, TABLANGUAGE.RECORDSTATUS, TRANSLANGUAGE.LANGUAGECODEID, TRANSLANGUAGE.DESCRIPTION FROM COMMON.TABLANGUAGE TABLANGUAGE JOIN COMMON.TRANSLANGUAGE TRANSLANGUAGE ON TRANSLANGUAGE.LANGUAGECODEID = TABLANGUAGE.LANGUAGEID  WHERE TABLANGUAGE.RECORDSTATUS = '1' AND TRANSLANGUAGE.LANGUAGECODEID = @:LANGUAGECODEID ", "TabLanguage", "Linked.Address")
                    .AddParameter("LANGUAGECODEID", DbType.Decimal, 5, False, CurrentState.Get("Language"))
 
                    source = .QueryExecuteToTable(True)
                    DirectCast(TransPartsOfAddressNamesDetail.Columns("LANGUAGEID"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = source
                End With

                If Not IsNothing(source) AndAlso source.Rows.Count > 0 Then
                    Caching.SetItem("TabLanguage", source)
                End If
            End If 
 

                 With New DataManagerFactory("SELECT  TABPARTSOFADDRESSNAMESDETAIL.COUNTRYID, TABPARTSOFADDRESSNAMESDETAIL.TYPEOFROUTE, TABPARTSOFADDRESSNAMESDETAIL.PARTNAMEID, TABPARTSOFADDRESSNAMESDETAIL.PARTNAMEDETAILID, TABPARTSOFADDRESSNAMESDETAIL.RECORDSTATUS, TRANSPARTSOFADDRESSNAMESDETAIL.COUNTRYID, TRANSPARTSOFADDRESSNAMESDETAIL.TYPEOFROUTE, TRANSPARTSOFADDRESSNAMESDETAIL.PARTNAMEID, TRANSPARTSOFADDRESSNAMESDETAIL.PARTNAMEDETAILID, TRANSPARTSOFADDRESSNAMESDETAIL.LANGUAGEID, TRANSPARTSOFADDRESSNAMESDETAIL.DESCRIPTION, TRANSPARTSOFADDRESSNAMESDETAIL.SHORTDESCRIPTION FROM ADDRESS.TABPARTSOFADDRESSNAMESDETAIL TABPARTSOFADDRESSNAMESDETAIL JOIN ADDRESS.TRANSPARTSOFADDRESSNAMESDETAIL TRANSPARTSOFADDRESSNAMESDETAIL ON TRANSPARTSOFADDRESSNAMESDETAIL.COUNTRYID = TABPARTSOFADDRESSNAMESDETAIL.COUNTRYID  AND TRANSPARTSOFADDRESSNAMESDETAIL.TYPEOFROUTE = TABPARTSOFADDRESSNAMESDETAIL.TYPEOFROUTE  AND TRANSPARTSOFADDRESSNAMESDETAIL.PARTNAMEID = TABPARTSOFADDRESSNAMESDETAIL.PARTNAMEID  AND TRANSPARTSOFADDRESSNAMESDETAIL.PARTNAMEDETAILID = TABPARTSOFADDRESSNAMESDETAIL.PARTNAMEDETAILID  ", "TabPartsOfAddressNamesDetail", "Linked.Address")                 
                                                   
                                  
                      TransPartsOfAddressNamesDetail.DataSource = .QueryExecuteToTable(True)
                 End With 
        End If     
    End Sub

    Protected Sub TransPartsOfAddressNamesDetail_CellEditorInitialize(sender As Object, e As ASPxGridViewEditorEventArgs) Handles TransPartsOfAddressNamesDetail.CellEditorInitialize
        If TransPartsOfAddressNamesDetail.IsNewRowEditing Then
            Select Case e.Column.FieldName
                
                
                
                Case "COUNTRYID"                     
                       e.Editor.Focus()               
            End Select

        Else

            Select Case e.Column.FieldName
                Case "COUNTRYID"
     e.Editor.Enabled = False
Case "TYPEOFROUTE"
     e.Editor.Enabled = False
Case "PARTNAMEID"
     e.Editor.Enabled = False
Case "PARTNAMEDETAILID"
     e.Editor.Enabled = False
Case "LANGUAGEID"
     e.Editor.Enabled = False
                   
                
                
                Case "RECORDSTATUS"                     
                     e.Editor.Focus()
            End Select
        End If
        
       Select Case e.Column.FieldName
           Case "COUNTRYID"
                 DirectCast(e.Editor, ASPxComboBox).DataBindItems()
                 
           Case "TYPEOFROUTE"
     DirectCast(e.Editor, ASPxComboBox).DataBindItems() 
Case "RECORDSTATUS"
     DirectCast(e.Editor, ASPxComboBox).DataBindItems() 
Case "LANGUAGEID"
     DirectCast(e.Editor, ASPxComboBox).DataBindItems() 

        End Select
    End Sub

    Protected Sub TransPartsOfAddressNamesDetail_RowInserting(ByVal sender As Object, ByVal e As ASPxDataInsertingEventArgs) Handles TransPartsOfAddressNamesDetail.RowInserting 
        Dim isNullResult As Boolean = True
        
        
               
        e.Cancel = True
        TransPartsOfAddressNamesDetail.CancelEdit()
    End Sub

    Protected Sub TransPartsOfAddressNamesDetail_RowUpdating(ByVal sender As Object, ByVal e As ASPxDataUpdatingEventArgs) Handles TransPartsOfAddressNamesDetail.RowUpdating
        Dim isNullResult As Boolean = True
          
             With New DataManagerFactory("UPDATE ADDRESS.TransPartsOfAddressNamesDetail SET DESCRIPTION = @:DESCRIPTION, SHORTDESCRIPTION = @:SHORTDESCRIPTION, UPDATEDATE = SYSDATE, UPDATEUSERCODE = @:UPDATEUSERCODE WHERE COUNTRYID = @:COUNTRYID AND TYPEOFROUTE = @:TYPEOFROUTE AND PARTNAMEID = @:PARTNAMEID AND PARTNAMEDETAILID = @:PARTNAMEDETAILID AND LANGUAGEID = @:LANGUAGEID", "TransPartsOfAddressNamesDetail", "Linked.Address")                 
                                                   
                       .AddParameter("DESCRIPTION", DbType.AnsiString, 0, False, e.NewValues("DESCRIPTION"))
.AddParameter("SHORTDESCRIPTION", DbType.AnsiString, 0, False, e.NewValues("SHORTDESCRIPTION"))
.AddParameter("UPDATEUSERCODE", DbType.Decimal, 0, False, Session("nUsercode"))
.AddParameter("COUNTRYID", DbType.Decimal, 0, False, e.Keys("COUNTRYID"))
.AddParameter("TYPEOFROUTE", DbType.Decimal, 0, False, e.Keys("TYPEOFROUTE"))
.AddParameter("PARTNAMEID", DbType.Decimal, 0, False, e.Keys("PARTNAMEID"))
.AddParameter("PARTNAMEDETAILID", DbType.Decimal, 0, False, e.Keys("PARTNAMEDETAILID"))
.AddParameter("LANGUAGEID", DbType.Decimal, 0, False, e.Keys("LANGUAGEID"))
            
                       .CommandExecute()
                End With         
      
         e.Cancel = True
        TransPartsOfAddressNamesDetail.CancelEdit()
    End Sub
    
    Protected Sub TransPartsOfAddressNamesDetail_CustomCallback(sender As Object, e As ASPxGridViewCustomCallbackEventArgs) Handles TransPartsOfAddressNamesDetail.CustomCallback     
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
    
    Protected Sub TransPartsOfAddressNamesDetail_RowValidating(sender As Object, e As ASPxDataValidationEventArgs) Handles TransPartsOfAddressNamesDetail.RowValidating

        
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