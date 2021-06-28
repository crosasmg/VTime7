#Region "using"

Imports System.Globalization
Imports GIT.Core
Imports DevExpress.Web.ASPxGridView
Imports DevExpress.Web.ASPxEditors
Imports System
Imports DevExpress.Web.Data
Imports InMotionGIT.Common.Proxy
Imports InMotionGIT.Common.Proxy.Helpers.Language
Imports InMotionGIT.Common.Helpers
Imports InMotionGIT.Data
Imports System.IO
Imports DevExpress.Web.ASPxClasses
Imports System.Data
Imports System.Data.Common
Imports DevExpress.Web.ASPxUploadControl

#End Region

Partial Class Maintenance_TabEntryType
    Inherits PageBase

#Region "Private fields"

    Private _internalCall As Boolean

#End Region

#Region "Events Page"

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not IsCallback AndAlso Not IsPostBack Then
            Dim newItem As DevExpress.Web.ASPxMenu.MenuItem

            For Each languageItem As KeyValuePair(Of Integer, String) In LanguageToDictionary(LanguageId)

                newItem = New DevExpress.Web.ASPxMenu.MenuItem

                With newItem
                    .Name = String.Format(CultureInfo.InvariantCulture, "{0}Item", languageItem.Value)
                    .Text = languageItem.Value
                    .Image.Url = String.Format(CultureInfo.InvariantCulture, "/images/16x16/Flags/{0}.png", languageItem.Key)
                    .Index = languageItem.Key
                     
                    If languageItem.Key = LanguageId Then
                        MainMenu.Items(4).Text = String.Format(CultureInfo.InvariantCulture, "{0} {1}", GetLocalResourceObject("LanguageItemMenu").ToString(), languageItem.Value)
                        MainMenu.Items(4).Image.Url = String.Format(CultureInfo.InvariantCulture, "/images/16x16/Flags/{0}.png", languageItem.Key)

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
            e.Item.Parent.Image.Url = String.Format(CultureInfo.InvariantCulture, "/images/16x16/Flags/{0}.png", e.Item.Index)

            e.Item.Visible = False

            For Each item As DevExpress.Web.ASPxMenu.MenuItem In e.Item.Parent.Items
                If Not String.Equals(item.Text, e.Item.Text, StringComparison.CurrentCultureIgnoreCase) Then
                    item.Visible = True
                End If
            Next

            CurrentState.Set("LanguageId", DescriptionToEnumLanguage(e.Item.Text, LanguageId))
            _internalCall = True

            TabEntryType_Grid.DataBind()
        End If      
    End Sub

#End Region

#Region "Controls Events"

         
    
 

#End Region

#Region "TabEntryType_Grid Events"
    
    Protected Sub TabEntryType_Grid_CustomColumnDisplayText(sender As Object, e As DevExpress.Web.ASPxGridView.ASPxGridViewColumnDisplayTextEventArgs) Handles TabEntryType_Grid.CustomColumnDisplayText
          Dim data As DataTable
          Dim rows() As DataRow
           
          Select Case e.Column.FieldName

               Case Else              
           End Select
    End Sub
  
    Protected Sub TabEntryType_Grid_DataBinding(ByVal sender As Object, ByVal e As EventArgs) Handles TabEntryType_Grid.DataBinding
        If (Not IsNothing(Request.Params("__CALLBACKID")) AndAlso Request.Params("__CALLBACKID").EndsWith("TabEntryType_Grid")) Or _internalCall Then
                       If Caching.Exist("EnumRecordStatus") Then
                DirectCast(TabEntryType_Grid.Columns("RECORDSTATUS"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = Caching.GetItem("EnumRecordStatus")

            Else
                Dim source As DataTable = Nothing

                With New DataManagerFactory("SELECT  ENUMRECORDSTATUS.RECORDSTATUS, ENUMRECORDSTATUS.RECORDSTATUSCODE, ETRANRECORDSTATUS.LANGUAGEID, ETRANRECORDSTATUS.DESCRIPTION FROM COMMON.ENUMRECORDSTATUS ENUMRECORDSTATUS JOIN ETRANRECORDSTATUS ETRANRECORDSTATUS ON ETRANRECORDSTATUS.RECORDSTATUS = ENUMRECORDSTATUS.RECORDSTATUS  WHERE ENUMRECORDSTATUS.RECORDSTATUSCODE = '1' AND ETRANRECORDSTATUS.LANGUAGEID = @:LANGUAGEID  ORDER BY ETranRecordStatus.Description ASC", "EnumRecordStatus", "Linked.Common")
                    .AddParameter("LANGUAGEID", DbType.Decimal, 5, False, CurrentState.Get("LanguageId"))
 
                    source = .QueryExecuteToTable(True)
                    DirectCast(TabEntryType_Grid.Columns("RECORDSTATUS"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = source
                End With

                If Not IsNothing(source) AndAlso source.Rows.Count > 0 Then
                    Caching.SetItem("EnumRecordStatus", source)
                End If
            End If 
 

                 With New DataManagerFactory("SELECT  TABENTRYTYPE.ENTRYTYPE, TABENTRYTYPE.RECORDSTATUS, TRANSENTRYTYPE.ENTRYTYPE, TRANSENTRYTYPE.LANGUAGEID, TRANSENTRYTYPE.DESCRIPTION, TRANSENTRYTYPE.SHORTDESCRIPTION FROM UNDERWRITING.TABENTRYTYPE TABENTRYTYPE JOIN UNDERWRITING.TRANSENTRYTYPE TRANSENTRYTYPE ON TRANSENTRYTYPE.ENTRYTYPE = TABENTRYTYPE.ENTRYTYPE  WHERE TRANSENTRYTYPE.LANGUAGEID = @:LANGUAGEID ORDER BY TabEntryType.EntryType ASC", "TabEntryType", "Linked.Underwriting")                 
                                                   
                      .AddParameter("LANGUAGEID", DbType.Decimal, 5, False, CurrentState.Get("LanguageId"))
            
                      TabEntryType_Grid.DataSource = .QueryExecuteToTable(True)
                 End With 
        End If     
    End Sub

    Protected Sub TabEntryType_Grid_CellEditorInitialize(sender As Object, e As ASPxGridViewEditorEventArgs) Handles TabEntryType_Grid.CellEditorInitialize
        If TabEntryType_Grid.IsNewRowEditing Then
            Select Case e.Column.FieldName
                
                
                
                Case "ENTRYTYPE"                     
                       e.Editor.Focus()               
            End Select

        Else

            Select Case e.Column.FieldName
                Case "ENTRYTYPE"
     e.Editor.Enabled = False
                   
                
                
                Case "RECORDSTATUS"                     
                     e.Editor.Focus()
            End Select
        End If
        
       Select Case e.Column.FieldName
           Case "ENTRYTYPE"
                 
                 
           Case "RECORDSTATUS"
     DirectCast(e.Editor, ASPxComboBox).DataBindItems() 

        End Select
    End Sub

    Protected Sub TabEntryType_Grid_RowInserting(ByVal sender As Object, ByVal e As ASPxDataInsertingEventArgs) Handles TabEntryType_Grid.RowInserting 
        Dim isNullResult As Boolean = True
        
             With New DataManagerFactory("INSERT INTO UNDERWRITING.TabEntryType (ENTRYTYPE, RECORDSTATUS, CREATORUSERCODE, CREATIONDATE, UPDATEUSERCODE, UPDATEDATE) VALUES (@:ENTRYTYPE, @:RECORDSTATUS, @:CREATORUSERCODE, SYSDATE, @:UPDATEUSERCODE, SYSDATE)", "TabEntryType", "Linked.Underwriting")                 
                                                   
                       .AddParameter("ENTRYTYPE", DbType.Decimal, 0, False, e.NewValues("ENTRYTYPE"))
.AddParameter("RECORDSTATUS", DbType.Decimal, 0, False, e.NewValues("RECORDSTATUS"))
.AddParameter("CREATORUSERCODE", DbType.Decimal, 0, False, Session("nUserCode"))
.AddParameter("UPDATEUSERCODE", DbType.Decimal, 0, False, Session("nUserCode"))
            
                       .CommandExecute()
                End With
            For Each languageItem As KeyValuePair(Of Integer, String) In LanguageToDictionary(LanguageId)
                     With New DataManagerFactory("INSERT INTO UNDERWRITING.TransEntryType (ENTRYTYPE, LANGUAGEID, DESCRIPTION, SHORTDESCRIPTION, CREATORUSERCODE, CREATIONDATE, UPDATEUSERCODE, UPDATEDATE) VALUES (@:ENTRYTYPE, @:LANGUAGEID, @:DESCRIPTION, @:SHORTDESCRIPTION, @:CREATORUSERCODE, SYSDATE, @:UPDATEUSERCODE, SYSDATE)", "TransEntryType", "Linked.Underwriting")                 
                                                   
                       .AddParameter("ENTRYTYPE", DbType.Decimal, 0, False, e.NewValues("ENTRYTYPE"))
.AddParameter("LANGUAGEID", DbType.Decimal, 0, False, languageItem.Key)
.AddParameter("DESCRIPTION", DbType.AnsiString, 0, False, e.NewValues("DESCRIPTION"))
.AddParameter("SHORTDESCRIPTION", DbType.AnsiString, 0, False, e.NewValues("SHORTDESCRIPTION"))
.AddParameter("CREATORUSERCODE", DbType.Decimal, 0, False, Session("nUserCode"))
.AddParameter("UPDATEUSERCODE", DbType.Decimal, 0, False, Session("nUserCode"))
            
                       .CommandExecute()
                End With
           Next

               
        e.Cancel = True
        TabEntryType_Grid.CancelEdit()
    End Sub

    Protected Sub TabEntryType_Grid_RowUpdating(ByVal sender As Object, ByVal e As ASPxDataUpdatingEventArgs) Handles TabEntryType_Grid.RowUpdating
        Dim isNullResult As Boolean = True
          
             With New DataManagerFactory("UPDATE UNDERWRITING.TabEntryType SET RECORDSTATUS = @:RECORDSTATUS, UPDATEUSERCODE = @:UPDATEUSERCODE WHERE ENTRYTYPE = @:ENTRYTYPE", "TabEntryType", "Linked.Underwriting")                 
                                                   
                       .AddParameter("RECORDSTATUS", DbType.Decimal, 0, False, e.NewValues("RECORDSTATUS"))
.AddParameter("UPDATEUSERCODE", DbType.Decimal, 0, False, Session("nUserCode"))
.AddParameter("ENTRYTYPE", DbType.Decimal, 0, False, e.Keys("ENTRYTYPE"))
            
                       .CommandExecute()
                End With
     With New DataManagerFactory("UPDATE UNDERWRITING.TransEntryType SET DESCRIPTION = @:DESCRIPTION, SHORTDESCRIPTION = @:SHORTDESCRIPTION, UPDATEUSERCODE = @:UPDATEUSERCODE WHERE ENTRYTYPE = @:ENTRYTYPE AND LANGUAGEID = @:LANGUAGEID", "TransEntryType", "Linked.Underwriting")                 
                                                   
                       .AddParameter("DESCRIPTION", DbType.AnsiString, 0, False, e.NewValues("DESCRIPTION"))
.AddParameter("SHORTDESCRIPTION", DbType.AnsiString, 0, False, e.NewValues("SHORTDESCRIPTION"))
.AddParameter("UPDATEUSERCODE", DbType.Decimal, 0, False, Session("nUserCode"))
.AddParameter("ENTRYTYPE", DbType.Decimal, 0, False, e.Keys("ENTRYTYPE"))
.AddParameter("LANGUAGEID", DbType.Decimal, 0, False, CurrentState.Get("LanguageId"))
            
                       .CommandExecute()
                End With         
      
         e.Cancel = True
        TabEntryType_Grid.CancelEdit()
    End Sub
    
    Protected Sub TabEntryType_Grid_CustomCallback(sender As Object, e As ASPxGridViewCustomCallbackEventArgs) Handles TabEntryType_Grid.CustomCallback     
           Dim isNullResult As Boolean = True
           
           Select Case e.Parameters.ToString.ToLower
               Case "delete"
                       Dim ENTRYTYPEKey As Generic.List(Of Object) = TabEntryType_Grid.GetSelectedFieldValues("ENTRYTYPE")
        
               For index As Integer = 0 To ENTRYTYPEKey.Count - 1
                  With New DataManagerFactory("DELETE FROM UNDERWRITING.TransEntryType WHERE ENTRYTYPE = @:ENTRYTYPE ", "TransEntryType", "Linked.Underwriting")                 
                                                   
               .AddParameter("ENTRYTYPE", DbType.Decimal, 0, False, ENTRYTYPEKey(index))
            
               .CommandExecute()
          End With 
 With New DataManagerFactory("DELETE FROM UNDERWRITING.TabEntryType WHERE ENTRYTYPE = @:ENTRYTYPE ", "TabEntryType", "Linked.Underwriting")                 
                                                   
               .AddParameter("ENTRYTYPE", DbType.Decimal, 0, False, ENTRYTYPEKey(index))
            
               .CommandExecute()
          End With 
                       
              Next           
           
              TabEntryType_Grid.DataBind()
                 
               Case Else
                   Dim fileName As String = String.Empty
                
                   If e.Parameters.ToString.ToLower.StartsWith("export") Then
                       Dim extension As String = e.Parameters.ToString.ToLower.Split("_")(1)
                       fileName = String.Format(CultureInfo.InvariantCulture, "{0}.{1}", IO.Path.GetRandomFileName, extension)

                       ASPxGridViewExporter.GridViewID = sender.ClientInstanceName

                       Using fs As FileStream = New FileStream(String.Format(CultureInfo.InvariantCulture, "{0}\generated\{1}", Server.MapPath("/"), fileName), FileMode.Create)
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

                      ASPxWebControl.RedirectOnCallback(String.Format(CultureInfo.InvariantCulture, "~/dropthings/download.ashx?Directory=generated&File={0}", fileName))
                               
                  End If
         End Select
     End Sub
    
    Protected Sub TabEntryType_Grid_RowValidating(sender As Object, e As ASPxDataValidationEventArgs) Handles TabEntryType_Grid.RowValidating
        Dim errorMessage As String = "<ol style='font-weight:lighter'>"
        
        If IsNothing(e.NewValues("ENTRYTYPE")) OrElse e.NewValues("ENTRYTYPE") = 0  
   e.Errors(TabEntryType_Grid.Columns("ENTRYTYPE")) = GetLocalResourceObject("ENTRYTYPEMessageErrorRequired1Resource").ToString
End If
 
        
        If e.IsNewRow Then
           Dim rowCountKey As System.Int32
  With New DataManagerFactory("SELECT  TABENTRYTYPE.ENTRYTYPE ROWCOUNT FROM UNDERWRITING.TABENTRYTYPE TABENTRYTYPE  WHERE TABENTRYTYPE.ENTRYTYPE = @:ENTRYTYPE", "TabEntryType", "Linked.Underwriting")
             .AddParameter("ENTRYTYPE", DbType.Decimal, 5, False, e.NewValues("ENTRYTYPE"))
 
             rowCountKey = .QueryExecuteScalarToInteger()  
        End With
        If rowCountKey > 0 Then
                errorMessage += String.Format(CultureInfo.InvariantCulture, "</ol><ul style='font-weight:bold'>{0}</ul>", 
                                                                            GetLocalResourceObject("TabEntryType_GridMessageErrorGeneralValidator0Resource").ToString)                
                e.RowError = errorMessage
        End If


           
        Else        
            If e.Errors.Count > 0 Then          
                For Each item As KeyValuePair(Of GridViewColumn, String) In e.Errors
                    errorMessage += String.Format("<li>{0}</li>", item.Value)
                Next

                errorMessage += String.Format(CultureInfo.InvariantCulture, "</ol><ul style='font-weight:bold'>{0}</ul>", 
                                                                            GetLocalResourceObject("MessageErrorText").ToString)
                e.RowError = errorMessage
            End If
        End If
    End Sub

#End Region
 
#Region "TransEntryType_Grid Events"
    
    Protected Sub TransEntryType_Grid_CustomColumnDisplayText(sender As Object, e As DevExpress.Web.ASPxGridView.ASPxGridViewColumnDisplayTextEventArgs) Handles TransEntryType_Grid.CustomColumnDisplayText
          Dim data As DataTable
          Dim rows() As DataRow
           
          Select Case e.Column.FieldName

               Case Else              
           End Select
    End Sub
  
    Protected Sub TransEntryType_Grid_DataBinding(ByVal sender As Object, ByVal e As EventArgs) Handles TransEntryType_Grid.DataBinding
        If (Not IsNothing(Request.Params("__CALLBACKID")) AndAlso Request.Params("__CALLBACKID").EndsWith("TransEntryType_Grid")) Or _internalCall Then
                       If Caching.Exist("EnumRecordStatus") Then
                DirectCast(TransEntryType_Grid.Columns("RECORDSTATUS"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = Caching.GetItem("EnumRecordStatus")

            Else
                Dim source As DataTable = Nothing

                With New DataManagerFactory("SELECT  ENUMRECORDSTATUS.RECORDSTATUS, ENUMRECORDSTATUS.RECORDSTATUSCODE, ETRANRECORDSTATUS.LANGUAGEID, ETRANRECORDSTATUS.DESCRIPTION FROM COMMON.ENUMRECORDSTATUS ENUMRECORDSTATUS JOIN ETRANRECORDSTATUS ETRANRECORDSTATUS ON ETRANRECORDSTATUS.RECORDSTATUS = ENUMRECORDSTATUS.RECORDSTATUS  WHERE ENUMRECORDSTATUS.RECORDSTATUSCODE = '1' AND ETRANRECORDSTATUS.LANGUAGEID = @:LANGUAGEID  ORDER BY ETranRecordStatus.Description ASC", "EnumRecordStatus", "Linked.Common")
                    .AddParameter("LANGUAGEID", DbType.Decimal, 5, False, CurrentState.Get("LanguageId"))
 
                    source = .QueryExecuteToTable(True)
                    DirectCast(TransEntryType_Grid.Columns("RECORDSTATUS"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = source
                End With

                If Not IsNothing(source) AndAlso source.Rows.Count > 0 Then
                    Caching.SetItem("EnumRecordStatus", source)
                End If
            End If 
             If Caching.Exist("TabLanguage") Then
                DirectCast(TransEntryType_Grid.Columns("LANGUAGEID"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = Caching.GetItem("TabLanguage")

            Else
                Dim source As DataTable = Nothing

                With New DataManagerFactory("SELECT  TABLANGUAGE.LANGUAGEID, TABLANGUAGE.RECORDSTATUS, TRANSLANGUAGE.LANGUAGEID, TRANSLANGUAGE.DESCRIPTION FROM COMMON.TABLANGUAGE TABLANGUAGE JOIN TRANSLANGUAGE TRANSLANGUAGE ON TRANSLANGUAGE.LANGUAGECODEID = TABLANGUAGE.LANGUAGEID  WHERE TABLANGUAGE.RECORDSTATUS = '1' AND TRANSLANGUAGE.LANGUAGEID = @:LANGUAGEID  ORDER BY TransLanguage.Description ASC", "TabLanguage", "Linked.Common")
                    .AddParameter("LANGUAGEID", DbType.Decimal, 5, False, CurrentState.Get("LanguageId"))
 
                    source = .QueryExecuteToTable(True)
                    DirectCast(TransEntryType_Grid.Columns("LANGUAGEID"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = source
                End With

                If Not IsNothing(source) AndAlso source.Rows.Count > 0 Then
                    Caching.SetItem("TabLanguage", source)
                End If
            End If 
 

                 With New DataManagerFactory("SELECT  TABENTRYTYPE.ENTRYTYPE, TABENTRYTYPE.RECORDSTATUS, TRANSENTRYTYPE.ENTRYTYPE, TRANSENTRYTYPE.LANGUAGEID, TRANSENTRYTYPE.DESCRIPTION, TRANSENTRYTYPE.SHORTDESCRIPTION FROM UNDERWRITING.TABENTRYTYPE TABENTRYTYPE JOIN UNDERWRITING.TRANSENTRYTYPE TRANSENTRYTYPE ON TRANSENTRYTYPE.ENTRYTYPE = TABENTRYTYPE.ENTRYTYPE   ORDER BY TabEntryType.EntryType ASC", "TabEntryType", "Linked.Underwriting")                 
                                                   
                                  
                      TransEntryType_Grid.DataSource = .QueryExecuteToTable(True)
                 End With 
        End If     
    End Sub

    Protected Sub TransEntryType_Grid_CellEditorInitialize(sender As Object, e As ASPxGridViewEditorEventArgs) Handles TransEntryType_Grid.CellEditorInitialize
        If TransEntryType_Grid.IsNewRowEditing Then
            Select Case e.Column.FieldName
                
                
                
                Case "ENTRYTYPE"                     
                       e.Editor.Focus()               
            End Select

        Else

            Select Case e.Column.FieldName
                Case "ENTRYTYPE"
     e.Editor.Enabled = False
Case "LANGUAGEID"
     e.Editor.Enabled = False
                   
                
                
                Case "RECORDSTATUS"                     
                     e.Editor.Focus()
            End Select
        End If
        
       Select Case e.Column.FieldName
           Case "ENTRYTYPE"
                 
                 
           Case "RECORDSTATUS"
     DirectCast(e.Editor, ASPxComboBox).DataBindItems() 
Case "LANGUAGEID"
     DirectCast(e.Editor, ASPxComboBox).DataBindItems() 

        End Select
    End Sub

    Protected Sub TransEntryType_Grid_RowInserting(ByVal sender As Object, ByVal e As ASPxDataInsertingEventArgs) Handles TransEntryType_Grid.RowInserting 
        Dim isNullResult As Boolean = True
        
        
               
        e.Cancel = True
        TransEntryType_Grid.CancelEdit()
    End Sub

    Protected Sub TransEntryType_Grid_RowUpdating(ByVal sender As Object, ByVal e As ASPxDataUpdatingEventArgs) Handles TransEntryType_Grid.RowUpdating
        Dim isNullResult As Boolean = True
          
             With New DataManagerFactory("UPDATE UNDERWRITING.TransEntryType SET DESCRIPTION = @:DESCRIPTION, SHORTDESCRIPTION = @:SHORTDESCRIPTION, UPDATEUSERCODE = @:UPDATEUSERCODE WHERE ENTRYTYPE = @:ENTRYTYPE AND LANGUAGEID = @:LANGUAGEID", "TransEntryType", "Linked.Underwriting")                 
                                                   
                       .AddParameter("DESCRIPTION", DbType.AnsiString, 0, False, e.NewValues("DESCRIPTION"))
.AddParameter("SHORTDESCRIPTION", DbType.AnsiString, 0, False, e.NewValues("SHORTDESCRIPTION"))
.AddParameter("UPDATEUSERCODE", DbType.Decimal, 0, False, Session("nUserCode"))
.AddParameter("ENTRYTYPE", DbType.Decimal, 0, False, e.Keys("ENTRYTYPE"))
.AddParameter("LANGUAGEID", DbType.Decimal, 0, False, e.Keys("LANGUAGEID"))
            
                       .CommandExecute()
                End With         
      
         e.Cancel = True
        TransEntryType_Grid.CancelEdit()
    End Sub
    
    Protected Sub TransEntryType_Grid_CustomCallback(sender As Object, e As ASPxGridViewCustomCallbackEventArgs) Handles TransEntryType_Grid.CustomCallback     
           Dim isNullResult As Boolean = True
           
           Select Case e.Parameters.ToString.ToLower
               Case "delete"
                   
                 
               Case Else
                   Dim fileName As String = String.Empty
                
                   If e.Parameters.ToString.ToLower.StartsWith("export") Then
                       Dim extension As String = e.Parameters.ToString.ToLower.Split("_")(1)
                       fileName = String.Format(CultureInfo.InvariantCulture, "{0}.{1}", IO.Path.GetRandomFileName, extension)

                       ASPxGridViewExporter.GridViewID = sender.ClientInstanceName

                       Using fs As FileStream = New FileStream(String.Format(CultureInfo.InvariantCulture, "{0}\generated\{1}", Server.MapPath("/"), fileName), FileMode.Create)
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

                      ASPxWebControl.RedirectOnCallback(String.Format(CultureInfo.InvariantCulture, "~/dropthings/download.ashx?Directory=generated&File={0}", fileName))
                               
                  End If
         End Select
     End Sub
    
    Protected Sub TransEntryType_Grid_RowValidating(sender As Object, e As ASPxDataValidationEventArgs) Handles TransEntryType_Grid.RowValidating
        Dim errorMessage As String = "<ol style='font-weight:lighter'>"
        
        If IsNothing(e.NewValues("ENTRYTYPE")) OrElse e.NewValues("ENTRYTYPE") = 0  
   e.Errors(TransEntryType_Grid.Columns("ENTRYTYPE")) = GetLocalResourceObject("ENTRYTYPEMessageErrorRequired1Resource").ToString
End If
 
        
        If e.IsNewRow Then
           Dim rowCountKey As System.Int32
  With New DataManagerFactory("SELECT  TRANSENTRYTYPE.ENTRYTYPE ROWCOUNT, TRANSENTRYTYPE.LANGUAGEID FROM UNDERWRITING.TRANSENTRYTYPE TRANSENTRYTYPE  WHERE TRANSENTRYTYPE.ENTRYTYPE = @:ENTRYTYPE AND TRANSENTRYTYPE.LANGUAGEID = @:LANGUAGEID", "TransEntryType", "Linked.Underwriting")
             .AddParameter("ENTRYTYPE", DbType.Decimal, 5, False, e.NewValues("ENTRYTYPE"))
.AddParameter("LANGUAGEID", DbType.Decimal, 5, False, e.NewValues("LANGUAGEID"))
 
             rowCountKey = .QueryExecuteScalarToInteger()  
        End With
        If rowCountKey > 0 Then
                errorMessage += String.Format(CultureInfo.InvariantCulture, "</ol><ul style='font-weight:bold'>{0}</ul>", 
                                                                            GetLocalResourceObject("TransEntryType_GridMessageErrorGeneralValidator0Resource").ToString)                
                e.RowError = errorMessage
        End If


           
        Else        
            If e.Errors.Count > 0 Then          
                For Each item As KeyValuePair(Of GridViewColumn, String) In e.Errors
                    errorMessage += String.Format("<li>{0}</li>", item.Value)
                Next

                errorMessage += String.Format(CultureInfo.InvariantCulture, "</ol><ul style='font-weight:bold'>{0}</ul>", 
                                                                            GetLocalResourceObject("MessageErrorText").ToString)
                e.RowError = errorMessage
            End If
        End If
    End Sub

#End Region
 


End Class