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

Partial Class Maintenance_EnumPhoneType
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

            EnumPhoneType_Grid.DataBind()
        End If      
    End Sub

#End Region

#Region "Controls Events"

         
    
 

#End Region

#Region "EnumPhoneType_Grid Events"
    
    Protected Sub EnumPhoneType_Grid_CustomColumnDisplayText(sender As Object, e As DevExpress.Web.ASPxGridView.ASPxGridViewColumnDisplayTextEventArgs) Handles EnumPhoneType_Grid.CustomColumnDisplayText
          Dim data As DataTable
          Dim rows() As DataRow
           
          Select Case e.Column.FieldName

               Case Else              
           End Select
    End Sub
  
    Protected Sub EnumPhoneType_Grid_DataBinding(ByVal sender As Object, ByVal e As EventArgs) Handles EnumPhoneType_Grid.DataBinding
        If (Not IsNothing(Request.Params("__CALLBACKID")) AndAlso Request.Params("__CALLBACKID").EndsWith("EnumPhoneType_Grid")) Or _internalCall Then
                       If Caching.Exist("EnumRecordStatus") Then
                DirectCast(EnumPhoneType_Grid.Columns("RECORDSTATUS"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = Caching.GetItem("EnumRecordStatus")

            Else
                Dim source As DataTable = Nothing

                With New DataManagerFactory("SELECT  ENUMRECORDSTATUS.RECORDSTATUS, ETRANRECORDSTATUS.LANGUAGEID, ETRANRECORDSTATUS.DESCRIPTION FROM COMMON.ENUMRECORDSTATUS ENUMRECORDSTATUS JOIN COMMON.ETRANRECORDSTATUS ETRANRECORDSTATUS ON ETRANRECORDSTATUS.RECORDSTATUS = ENUMRECORDSTATUS.RECORDSTATUS  WHERE ETRANRECORDSTATUS.LANGUAGEID = @:LANGUAGEID ", "EnumRecordStatus", "Linked.Phone")
                    .AddParameter("LANGUAGEID", DbType.Decimal, 5, False, CurrentState.Get("Language"))
 
                    source = .QueryExecuteToTable(True)
                    DirectCast(EnumPhoneType_Grid.Columns("RECORDSTATUS"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = source
                End With

                If Not IsNothing(source) AndAlso source.Rows.Count > 0 Then
                    Caching.SetItem("EnumRecordStatus", source)
                End If
            End If 
 

                 With New DataManagerFactory("SELECT  ENUMPHONETYPE.PHONETYPE, ENUMPHONETYPE.RECORDSTATUS, ETRANPHONETYPE.PHONETYPE, ETRANPHONETYPE.LANGUAGEID, ETRANPHONETYPE.DESCRIPTION, ETRANPHONETYPE.SHORTDESCRIPTION FROM PHONE.ENUMPHONETYPE ENUMPHONETYPE JOIN PHONE.ETRANPHONETYPE ETRANPHONETYPE ON ETRANPHONETYPE.PHONETYPE = ENUMPHONETYPE.PHONETYPE  WHERE ETRANPHONETYPE.LANGUAGEID = @:LANGUAGEID", "EnumPhoneType", "Linked.Phone")                 
                                                   
                      .AddParameter("LANGUAGEID", DbType.Decimal, 5, False, CurrentState.Get("Language"))
            
                      EnumPhoneType_Grid.DataSource = .QueryExecuteToTable(True)
                 End With 
        End If     
    End Sub

    Protected Sub EnumPhoneType_Grid_CellEditorInitialize(sender As Object, e As ASPxGridViewEditorEventArgs) Handles EnumPhoneType_Grid.CellEditorInitialize
        If EnumPhoneType_Grid.IsNewRowEditing Then
            Select Case e.Column.FieldName
                
                
                
                Case "PHONETYPE"                     
                       e.Editor.Focus()               
            End Select

        Else

            Select Case e.Column.FieldName
                Case "PHONETYPE"
     e.Editor.Enabled = False
                   
                
                
                Case "RECORDSTATUS"                     
                     e.Editor.Focus()
            End Select
        End If
        
       Select Case e.Column.FieldName
           Case "PHONETYPE"
                 
                 
           Case "RECORDSTATUS"
     DirectCast(e.Editor, ASPxComboBox).DataBindItems() 

        End Select
    End Sub

    Protected Sub EnumPhoneType_Grid_RowInserting(ByVal sender As Object, ByVal e As ASPxDataInsertingEventArgs) Handles EnumPhoneType_Grid.RowInserting 
        Dim isNullResult As Boolean = True
        
             With New DataManagerFactory("INSERT INTO PHONE.EnumPhoneType (PHONETYPE, RECORDSTATUS, CREATIONDATE, CREATORUSERCODE, UPDATEDATE, UPDATEUSERCODE) VALUES (@:PHONETYPE, @:RECORDSTATUS, SYSDATE, @:CREATORUSERCODE, SYSDATE, @:UPDATEUSERCODE)", "EnumPhoneType", "Linked.Phone")                 
                                                   
                       .AddParameter("PHONETYPE", DbType.Decimal, 0, False, e.NewValues("PHONETYPE"))
.AddParameter("RECORDSTATUS", DbType.AnsiString, 0, False, e.NewValues("RECORDSTATUS"))
.AddParameter("CREATORUSERCODE", DbType.Decimal, 0, False, Session("nUsercode"))
.AddParameter("UPDATEUSERCODE", DbType.Decimal, 0, False, Session("nUsercode"))
            
                       .CommandExecute()
                End With
        For Each languageItem In InMotionGIT.Common.Proxy.Helpers.Language.LanguageToDictionary


            With New DataManagerFactory("INSERT INTO PHONE.ETranPhoneType (PHONETYPE, LANGUAGEID, DESCRIPTION, SHORTDESCRIPTION, CREATIONDATE, CREATORUSERCODE, UPDATEDATE, UPDATEUSERCODE) VALUES (@:PHONETYPE, @:LANGUAGEID, @:DESCRIPTION, @:SHORTDESCRIPTION, SYSDATE, @:CREATORUSERCODE, SYSDATE, @:UPDATEUSERCODE)", "ETranPhoneType", "Linked.Phone")

                .AddParameter("PHONETYPE", DbType.Decimal, 0, False, e.NewValues("PHONETYPE"))
                .AddParameter("LANGUAGEID", DbType.Decimal, 0, False, languageItem.Key)
                .AddParameter("DESCRIPTION", DbType.AnsiString, 0, False, e.NewValues("DESCRIPTION"))
                .AddParameter("SHORTDESCRIPTION", DbType.AnsiString, 0, False, e.NewValues("SHORTDESCRIPTION"))
                .AddParameter("CREATORUSERCODE", DbType.Decimal, 0, False, Session("nUsercode"))
                .AddParameter("UPDATEUSERCODE", DbType.Decimal, 0, False, Session("nUsercode"))

                .CommandExecute()
            End With
        Next

               
        e.Cancel = True
        EnumPhoneType_Grid.CancelEdit()
    End Sub

    Protected Sub EnumPhoneType_Grid_RowUpdating(ByVal sender As Object, ByVal e As ASPxDataUpdatingEventArgs) Handles EnumPhoneType_Grid.RowUpdating
        Dim isNullResult As Boolean = True
          
             With New DataManagerFactory("UPDATE PHONE.EnumPhoneType SET RECORDSTATUS = @:RECORDSTATUS, UPDATEDATE = SYSDATE, UPDATEUSERCODE = @:UPDATEUSERCODE WHERE PHONETYPE = @:PHONETYPE", "EnumPhoneType", "Linked.Phone")                 
                                                   
                       .AddParameter("RECORDSTATUS", DbType.AnsiString, 0, False, e.NewValues("RECORDSTATUS"))
.AddParameter("UPDATEUSERCODE", DbType.Decimal, 0, False, Session("nUsercode"))
.AddParameter("PHONETYPE", DbType.Decimal, 0, False, e.Keys("PHONETYPE"))
            
                       .CommandExecute()
                End With
     With New DataManagerFactory("UPDATE PHONE.ETranPhoneType SET DESCRIPTION = @:DESCRIPTION, SHORTDESCRIPTION = @:SHORTDESCRIPTION, UPDATEDATE = SYSDATE, UPDATEUSERCODE = @:UPDATEUSERCODE WHERE PHONETYPE = @:PHONETYPE AND LANGUAGEID = @:LANGUAGEID", "ETranPhoneType", "Linked.Phone")                 
                                                   
                       .AddParameter("DESCRIPTION", DbType.AnsiString, 0, False, e.NewValues("DESCRIPTION"))
.AddParameter("SHORTDESCRIPTION", DbType.AnsiString, 0, False, e.NewValues("SHORTDESCRIPTION"))
.AddParameter("UPDATEUSERCODE", DbType.Decimal, 0, False, Session("nUsercode"))
.AddParameter("PHONETYPE", DbType.Decimal, 0, False, e.Keys("PHONETYPE"))
.AddParameter("LANGUAGEID", DbType.Decimal, 0, False, CurrentState.Get("Language"))
            
                       .CommandExecute()
                End With         
      
         e.Cancel = True
        EnumPhoneType_Grid.CancelEdit()
    End Sub
    
    Protected Sub EnumPhoneType_Grid_CustomCallback(sender As Object, e As ASPxGridViewCustomCallbackEventArgs) Handles EnumPhoneType_Grid.CustomCallback     
           Dim isNullResult As Boolean = True
           
           Select Case e.Parameters.ToString.ToLower
               Case "delete"
                       Dim PHONETYPEKey As Generic.List(Of Object) = EnumPhoneType_Grid.GetSelectedFieldValues("PHONETYPE")
        
               For index As Integer = 0 To PHONETYPEKey.Count - 1
                  With New DataManagerFactory("DELETE FROM PHONE.ETranPhoneType WHERE PHONETYPE = @:PHONETYPE ", "ETranPhoneType", "Linked.Phone")                 
                                                   
               .AddParameter("PHONETYPE", DbType.Decimal, 0, False, PHONETYPEKey(index))
            
               .CommandExecute()
          End With 
 With New DataManagerFactory("DELETE FROM PHONE.EnumPhoneType WHERE PHONETYPE = @:PHONETYPE ", "EnumPhoneType", "Linked.Phone")                 
                                                   
               .AddParameter("PHONETYPE", DbType.Decimal, 0, False, PHONETYPEKey(index))
            
               .CommandExecute()
          End With 
                       
              Next           
           
              EnumPhoneType_Grid.DataBind()
                 
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
    
    Protected Sub EnumPhoneType_Grid_RowValidating(sender As Object, e As ASPxDataValidationEventArgs) Handles EnumPhoneType_Grid.RowValidating

        
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
 
#Region "ETranPhoneType Events"
    
    Protected Sub ETranPhoneType_CustomColumnDisplayText(sender As Object, e As DevExpress.Web.ASPxGridView.ASPxGridViewColumnDisplayTextEventArgs) Handles ETranPhoneType.CustomColumnDisplayText
          Dim data As DataTable
          Dim rows() As DataRow
           
          Select Case e.Column.FieldName

               Case Else              
           End Select
    End Sub
  
    Protected Sub ETranPhoneType_DataBinding(ByVal sender As Object, ByVal e As EventArgs) Handles ETranPhoneType.DataBinding
        If (Not IsNothing(Request.Params("__CALLBACKID")) AndAlso Request.Params("__CALLBACKID").EndsWith("ETranPhoneType")) Or _internalCall Then
                       If Caching.Exist("EnumRecordStatus") Then
                DirectCast(ETranPhoneType.Columns("RECORDSTATUS"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = Caching.GetItem("EnumRecordStatus")

            Else
                Dim source As DataTable = Nothing

                With New DataManagerFactory("SELECT  ENUMRECORDSTATUS.RECORDSTATUS, ETRANRECORDSTATUS.LANGUAGEID, ETRANRECORDSTATUS.DESCRIPTION FROM COMMON.ENUMRECORDSTATUS ENUMRECORDSTATUS JOIN COMMON.ETRANRECORDSTATUS ETRANRECORDSTATUS ON ETRANRECORDSTATUS.RECORDSTATUS = ENUMRECORDSTATUS.RECORDSTATUS  WHERE ETRANRECORDSTATUS.LANGUAGEID = @:LANGUAGEID ", "EnumRecordStatus", "Linked.Phone")
                    .AddParameter("LANGUAGEID", DbType.Decimal, 5, False, CurrentState.Get("Language"))
 
                    source = .QueryExecuteToTable(True)
                    DirectCast(ETranPhoneType.Columns("RECORDSTATUS"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = source
                End With

                If Not IsNothing(source) AndAlso source.Rows.Count > 0 Then
                    Caching.SetItem("EnumRecordStatus", source)
                End If
            End If 
             If Caching.Exist("TabLanguage") Then
                DirectCast(ETranPhoneType.Columns("LANGUAGEID"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = Caching.GetItem("TabLanguage")

            Else
                Dim source As DataTable = Nothing

                With New DataManagerFactory("SELECT  TABLANGUAGE.LANGUAGEID, TABLANGUAGE.RECORDSTATUS, TRANSLANGUAGE.LANGUAGECODEID, TRANSLANGUAGE.DESCRIPTION FROM COMMON.TABLANGUAGE TABLANGUAGE JOIN COMMON.TRANSLANGUAGE TRANSLANGUAGE ON TRANSLANGUAGE.LANGUAGECODEID = TABLANGUAGE.LANGUAGEID  WHERE TABLANGUAGE.RECORDSTATUS = '1' AND TRANSLANGUAGE.LANGUAGECODEID = @:LANGUAGECODEID ", "TabLanguage", "Linked.Phone")
                    .AddParameter("LANGUAGECODEID", DbType.Decimal, 5, False, CurrentState.Get("Language"))
 
                    source = .QueryExecuteToTable(True)
                    DirectCast(ETranPhoneType.Columns("LANGUAGEID"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = source
                End With

                If Not IsNothing(source) AndAlso source.Rows.Count > 0 Then
                    Caching.SetItem("TabLanguage", source)
                End If
            End If 
 

                 With New DataManagerFactory("SELECT  ENUMPHONETYPE.PHONETYPE, ENUMPHONETYPE.RECORDSTATUS, ETRANPHONETYPE.PHONETYPE, ETRANPHONETYPE.LANGUAGEID, ETRANPHONETYPE.DESCRIPTION, ETRANPHONETYPE.SHORTDESCRIPTION FROM PHONE.ENUMPHONETYPE ENUMPHONETYPE JOIN PHONE.ETRANPHONETYPE ETRANPHONETYPE ON ETRANPHONETYPE.PHONETYPE = ENUMPHONETYPE.PHONETYPE  ", "EnumPhoneType", "Linked.Phone")                 
                                                   
                                  
                      ETranPhoneType.DataSource = .QueryExecuteToTable(True)
                 End With 
        End If     
    End Sub

    Protected Sub ETranPhoneType_CellEditorInitialize(sender As Object, e As ASPxGridViewEditorEventArgs) Handles ETranPhoneType.CellEditorInitialize
        If ETranPhoneType.IsNewRowEditing Then
            Select Case e.Column.FieldName
                
                
                
                Case "PHONETYPE"                     
                       e.Editor.Focus()               
            End Select

        Else

            Select Case e.Column.FieldName
                Case "PHONETYPE"
     e.Editor.Enabled = False
Case "LANGUAGEID"
     e.Editor.Enabled = False
                   
                
                
                Case "RECORDSTATUS"                     
                     e.Editor.Focus()
            End Select
        End If
        
       Select Case e.Column.FieldName
           Case "PHONETYPE"
                 
                 
           Case "RECORDSTATUS"
     DirectCast(e.Editor, ASPxComboBox).DataBindItems() 
Case "LANGUAGEID"
     DirectCast(e.Editor, ASPxComboBox).DataBindItems() 

        End Select
    End Sub

    Protected Sub ETranPhoneType_RowInserting(ByVal sender As Object, ByVal e As ASPxDataInsertingEventArgs) Handles ETranPhoneType.RowInserting 
        Dim isNullResult As Boolean = True
        
        
               
        e.Cancel = True
        ETranPhoneType.CancelEdit()
    End Sub

    Protected Sub ETranPhoneType_RowUpdating(ByVal sender As Object, ByVal e As ASPxDataUpdatingEventArgs) Handles ETranPhoneType.RowUpdating
        Dim isNullResult As Boolean = True
          
             With New DataManagerFactory("UPDATE PHONE.ETranPhoneType SET DESCRIPTION = @:DESCRIPTION, SHORTDESCRIPTION = @:SHORTDESCRIPTION, UPDATEDATE = SYSDATE, UPDATEUSERCODE = @:UPDATEUSERCODE WHERE PHONETYPE = @:PHONETYPE AND LANGUAGEID = @:LANGUAGEID", "ETranPhoneType", "Linked.Phone")                 
                                                   
                       .AddParameter("DESCRIPTION", DbType.AnsiString, 0, False, e.NewValues("DESCRIPTION"))
.AddParameter("SHORTDESCRIPTION", DbType.AnsiString, 0, False, e.NewValues("SHORTDESCRIPTION"))
.AddParameter("UPDATEUSERCODE", DbType.Decimal, 0, False, Session("nUsercode"))
.AddParameter("PHONETYPE", DbType.Decimal, 0, False, e.Keys("PHONETYPE"))
.AddParameter("LANGUAGEID", DbType.Decimal, 0, False, e.Keys("LANGUAGEID"))
            
                       .CommandExecute()
                End With         
      
         e.Cancel = True
        ETranPhoneType.CancelEdit()
    End Sub
    
    Protected Sub ETranPhoneType_CustomCallback(sender As Object, e As ASPxGridViewCustomCallbackEventArgs) Handles ETranPhoneType.CustomCallback     
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
    
    Protected Sub ETranPhoneType_RowValidating(sender As Object, e As ASPxDataValidationEventArgs) Handles ETranPhoneType.RowValidating

        
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