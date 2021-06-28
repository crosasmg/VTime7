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

Partial Class Maintenance_TabStageCase
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

            TabStageCase_Grid.DataBind()
        End If      
    End Sub

#End Region

#Region "Controls Events"

         
    
 

#End Region

#Region "TabStageCase_Grid Events"
    
    Protected Sub TabStageCase_Grid_CustomColumnDisplayText(sender As Object, e As DevExpress.Web.ASPxGridView.ASPxGridViewColumnDisplayTextEventArgs) Handles TabStageCase_Grid.CustomColumnDisplayText
          Dim data As DataTable
          Dim rows() As DataRow
           
          Select Case e.Column.FieldName

               Case Else              
           End Select
    End Sub
  
    Protected Sub TabStageCase_Grid_DataBinding(ByVal sender As Object, ByVal e As EventArgs) Handles TabStageCase_Grid.DataBinding
        If (Not IsNothing(Request.Params("__CALLBACKID")) AndAlso Request.Params("__CALLBACKID").EndsWith("TabStageCase_Grid")) Or _internalCall Then
                       If Caching.Exist("TabUnderwritingCaseType") Then
                DirectCast(TabStageCase_Grid.Columns("UNDERWRITINGCASETYPE"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = Caching.GetItem("TabUnderwritingCaseType")

            Else
                Dim source As DataTable = Nothing

                With New DataManagerFactory("SELECT  TABUNDERWRITINGCASETYPE.UNDERWRITINGCASETYPE, TABUNDERWRITINGCASETYPE.RECORDSTATUS, TRANSUNDERWRITINGCASETYPE.LANGUAGEID, TRANSUNDERWRITINGCASETYPE.DESCRIPTION FROM UNDERWRITING.TABUNDERWRITINGCASETYPE TABUNDERWRITINGCASETYPE JOIN TRANSUNDERWRITINGCASETYPE TRANSUNDERWRITINGCASETYPE ON TRANSUNDERWRITINGCASETYPE.UNDERWRITINGCASETYPE = TABUNDERWRITINGCASETYPE.UNDERWRITINGCASETYPE  WHERE TABUNDERWRITINGCASETYPE.RECORDSTATUS = 1 AND TRANSUNDERWRITINGCASETYPE.LANGUAGEID = @:LANGUAGEID  ORDER BY TransUnderwritingCaseType.Description ASC", "TabUnderwritingCaseType", "Linked.Underwriting")
                    .AddParameter("LANGUAGEID", DbType.Decimal, 5, False, CurrentState.Get("LanguageId"))
 
                    source = .QueryExecuteToTable(True)
                    DirectCast(TabStageCase_Grid.Columns("UNDERWRITINGCASETYPE"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = source
                End With

                If Not IsNothing(source) AndAlso source.Rows.Count > 0 Then
                    Caching.SetItem("TabUnderwritingCaseType", source)
                End If
            End If 
             If Caching.Exist("EnumRecordStatus") Then
                DirectCast(TabStageCase_Grid.Columns("RECORDSTATUS"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = Caching.GetItem("EnumRecordStatus")

            Else
                Dim source As DataTable = Nothing

                With New DataManagerFactory("SELECT  ENUMRECORDSTATUS.RECORDSTATUS, ENUMRECORDSTATUS.RECORDSTATUSCODE, ETRANRECORDSTATUS.LANGUAGEID, ETRANRECORDSTATUS.DESCRIPTION FROM COMMON.ENUMRECORDSTATUS ENUMRECORDSTATUS JOIN ETRANRECORDSTATUS ETRANRECORDSTATUS ON ETRANRECORDSTATUS.RECORDSTATUS = ENUMRECORDSTATUS.RECORDSTATUS  WHERE ENUMRECORDSTATUS.RECORDSTATUSCODE = '1' AND ETRANRECORDSTATUS.LANGUAGEID = @:LANGUAGEID  ORDER BY ETranRecordStatus.Description ASC", "EnumRecordStatus", "Linked.Common")
                    .AddParameter("LANGUAGEID", DbType.Decimal, 5, False, CurrentState.Get("LanguageId"))
 
                    source = .QueryExecuteToTable(True)
                    DirectCast(TabStageCase_Grid.Columns("RECORDSTATUS"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = source
                End With

                If Not IsNothing(source) AndAlso source.Rows.Count > 0 Then
                    Caching.SetItem("EnumRecordStatus", source)
                End If
            End If 
 

                 With New DataManagerFactory("SELECT  TABSTAGECASE.STAGE, TABSTAGECASE.LINEOFBUSINESS, TABSTAGECASE.PRODUCT, TABSTAGECASE.UNDERWRITINGCASETYPE, TABSTAGECASE.RECORDSTATUS, TRANSSTAGECASE.STAGE, TRANSSTAGECASE.LANGUAGEID, TRANSSTAGECASE.DESCRIPTION, TRANSSTAGECASE.SHORTDESCRIPTION FROM UNDERWRITING.TABSTAGECASE TABSTAGECASE JOIN UNDERWRITING.TRANSSTAGECASE TRANSSTAGECASE ON TRANSSTAGECASE.STAGE = TABSTAGECASE.STAGE  WHERE TRANSSTAGECASE.LANGUAGEID = @:LANGUAGEID ORDER BY TabStageCase.Stage ASC", "TabStageCase", "Linked.Underwriting")                 
                                                   
                      .AddParameter("LANGUAGEID", DbType.Decimal, 5, False, CurrentState.Get("LanguageId"))
            
                      TabStageCase_Grid.DataSource = .QueryExecuteToTable(True)
                 End With 
        End If     
    End Sub

    Protected Sub TabStageCase_Grid_CellEditorInitialize(sender As Object, e As ASPxGridViewEditorEventArgs) Handles TabStageCase_Grid.CellEditorInitialize
        If TabStageCase_Grid.IsNewRowEditing Then
            Select Case e.Column.FieldName
                
                
                
                Case "STAGE"                     
                       e.Editor.Focus()               
            End Select

        Else

            Select Case e.Column.FieldName
                Case "STAGE"
     e.Editor.Enabled = False
                   
                
                
                Case "LINEOFBUSINESS"                     
                     e.Editor.Focus()
            End Select
        End If
        
       Select Case e.Column.FieldName
           Case "STAGE"
                 
                 
           Case "UNDERWRITINGCASETYPE"
     DirectCast(e.Editor, ASPxComboBox).DataBindItems() 
Case "RECORDSTATUS"
     DirectCast(e.Editor, ASPxComboBox).DataBindItems() 

        End Select
    End Sub

    Protected Sub TabStageCase_Grid_RowInserting(ByVal sender As Object, ByVal e As ASPxDataInsertingEventArgs) Handles TabStageCase_Grid.RowInserting 
        Dim isNullResult As Boolean = True
        
             With New DataManagerFactory("INSERT INTO UNDERWRITING.TabStageCase (STAGE, LINEOFBUSINESS, PRODUCT, UNDERWRITINGCASETYPE, RECORDSTATUS, CREATORUSERCODE, CREATIONDATE, UPDATEUSERCODE, UPDATEDATE) VALUES (@:STAGE, @:LINEOFBUSINESS, @:PRODUCT, @:UNDERWRITINGCASETYPE, @:RECORDSTATUS, @:CREATORUSERCODE, SYSDATE, @:UPDATEUSERCODE, SYSDATE)", "TabStageCase", "Linked.Underwriting")                 
                                                   
                       .AddParameter("STAGE", DbType.Decimal, 0, False, e.NewValues("STAGE"))
.AddParameter("LINEOFBUSINESS", DbType.Decimal, 0, (e.NewValues("LINEOFBUSINESS") = 0), e.NewValues("LINEOFBUSINESS"))
.AddParameter("PRODUCT", DbType.Decimal, 0, (e.NewValues("PRODUCT") = 0), e.NewValues("PRODUCT"))
.AddParameter("UNDERWRITINGCASETYPE", DbType.Decimal, 0, False, e.NewValues("UNDERWRITINGCASETYPE"))
.AddParameter("RECORDSTATUS", DbType.Decimal, 0, False, e.NewValues("RECORDSTATUS"))
.AddParameter("CREATORUSERCODE", DbType.Decimal, 0, False, Session("nUserCode"))
.AddParameter("UPDATEUSERCODE", DbType.Decimal, 0, False, Session("nUserCode"))
            
                       .CommandExecute()
                End With
            For Each languageItem As KeyValuePair(Of Integer, String) In LanguageToDictionary(LanguageId)
                     With New DataManagerFactory("INSERT INTO UNDERWRITING.TransStageCase (STAGE, LANGUAGEID, DESCRIPTION, SHORTDESCRIPTION, CREATORUSERCODE, CREATIONDATE, UPDATEUSERCODE, UPDATEDATE) VALUES (@:STAGE, @:LANGUAGEID, @:DESCRIPTION, @:SHORTDESCRIPTION, @:CREATORUSERCODE, SYSDATE, @:UPDATEUSERCODE, SYSDATE)", "TransStageCase", "Linked.Underwriting")                 
                                                   
                       .AddParameter("STAGE", DbType.Decimal, 0, False, e.NewValues("STAGE"))
.AddParameter("LANGUAGEID", DbType.Decimal, 0, False, languageItem.Key)
.AddParameter("DESCRIPTION", DbType.AnsiString, 0, False, e.NewValues("DESCRIPTION"))
.AddParameter("SHORTDESCRIPTION", DbType.AnsiString, 0, False, e.NewValues("SHORTDESCRIPTION"))
.AddParameter("CREATORUSERCODE", DbType.Decimal, 0, False, Session("nUserCode"))
.AddParameter("UPDATEUSERCODE", DbType.Decimal, 0, False, Session("nUserCode"))
            
                       .CommandExecute()
                End With
           Next

               
        e.Cancel = True
        TabStageCase_Grid.CancelEdit()
    End Sub

    Protected Sub TabStageCase_Grid_RowUpdating(ByVal sender As Object, ByVal e As ASPxDataUpdatingEventArgs) Handles TabStageCase_Grid.RowUpdating
        Dim isNullResult As Boolean = True
          
             With New DataManagerFactory("UPDATE UNDERWRITING.TabStageCase SET LINEOFBUSINESS = @:LINEOFBUSINESS, PRODUCT = @:PRODUCT, UNDERWRITINGCASETYPE = @:UNDERWRITINGCASETYPE, RECORDSTATUS = @:RECORDSTATUS, UPDATEUSERCODE = @:UPDATEUSERCODE WHERE STAGE = @:STAGE", "TabStageCase", "Linked.Underwriting")                 
                                                   
                       .AddParameter("LINEOFBUSINESS", DbType.Decimal, 0, (e.NewValues("LINEOFBUSINESS") = 0), e.NewValues("LINEOFBUSINESS"))
.AddParameter("PRODUCT", DbType.Decimal, 0, (e.NewValues("PRODUCT") = 0), e.NewValues("PRODUCT"))
.AddParameter("UNDERWRITINGCASETYPE", DbType.Decimal, 0, False, e.NewValues("UNDERWRITINGCASETYPE"))
.AddParameter("RECORDSTATUS", DbType.Decimal, 0, False, e.NewValues("RECORDSTATUS"))
.AddParameter("UPDATEUSERCODE", DbType.Decimal, 0, False, Session("nUserCode"))
.AddParameter("STAGE", DbType.Decimal, 0, False, e.Keys("STAGE"))
            
                       .CommandExecute()
                End With
     With New DataManagerFactory("UPDATE UNDERWRITING.TransStageCase SET DESCRIPTION = @:DESCRIPTION, SHORTDESCRIPTION = @:SHORTDESCRIPTION, UPDATEUSERCODE = @:UPDATEUSERCODE WHERE STAGE = @:STAGE AND LANGUAGEID = @:LANGUAGEID", "TransStageCase", "Linked.Underwriting")                 
                                                   
                       .AddParameter("DESCRIPTION", DbType.AnsiString, 0, False, e.NewValues("DESCRIPTION"))
.AddParameter("SHORTDESCRIPTION", DbType.AnsiString, 0, False, e.NewValues("SHORTDESCRIPTION"))
.AddParameter("UPDATEUSERCODE", DbType.Decimal, 0, False, Session("nUserCode"))
.AddParameter("STAGE", DbType.Decimal, 0, False, e.Keys("STAGE"))
.AddParameter("LANGUAGEID", DbType.Decimal, 0, False, CurrentState.Get("LanguageId"))
            
                       .CommandExecute()
                End With         
      
         e.Cancel = True
        TabStageCase_Grid.CancelEdit()
    End Sub
    
    Protected Sub TabStageCase_Grid_CustomCallback(sender As Object, e As ASPxGridViewCustomCallbackEventArgs) Handles TabStageCase_Grid.CustomCallback     
           Dim isNullResult As Boolean = True
           
           Select Case e.Parameters.ToString.ToLower
               Case "delete"
                       Dim STAGEKey As Generic.List(Of Object) = TabStageCase_Grid.GetSelectedFieldValues("STAGE")
        
               For index As Integer = 0 To STAGEKey.Count - 1
                  With New DataManagerFactory("DELETE FROM UNDERWRITING.TransStageCase WHERE STAGE = @:STAGE ", "TransStageCase", "Linked.Underwriting")                 
                                                   
               .AddParameter("STAGE", DbType.Decimal, 0, False, STAGEKey(index))
            
               .CommandExecute()
          End With 
 With New DataManagerFactory("DELETE FROM UNDERWRITING.TabStageCase WHERE STAGE = @:STAGE ", "TabStageCase", "Linked.Underwriting")                 
                                                   
               .AddParameter("STAGE", DbType.Decimal, 0, False, STAGEKey(index))
            
               .CommandExecute()
          End With 
                       
              Next           
           
              TabStageCase_Grid.DataBind()
                 
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
    
    Protected Sub TabStageCase_Grid_RowValidating(sender As Object, e As ASPxDataValidationEventArgs) Handles TabStageCase_Grid.RowValidating
        Dim errorMessage As String = "<ol style='font-weight:lighter'>"
        
        If IsNothing(e.NewValues("STAGE")) OrElse e.NewValues("STAGE") = 0  
   e.Errors(TabStageCase_Grid.Columns("STAGE")) = GetLocalResourceObject("STAGEMessageErrorRequired1Resource").ToString
End If
 
        
        If e.IsNewRow Then
           Dim rowCountKey As System.Int32
  With New DataManagerFactory("SELECT  TABSTAGECASE.STAGE ROWCOUNT FROM UNDERWRITING.TABSTAGECASE TABSTAGECASE  WHERE TABSTAGECASE.STAGE = @:STAGE", "TabStageCase", "Linked.Underwriting")
             .AddParameter("STAGE", DbType.Decimal, 5, False, e.NewValues("STAGE"))
 
             rowCountKey = .QueryExecuteScalarToInteger()  
        End With
        If rowCountKey > 0 Then
                errorMessage += String.Format(CultureInfo.InvariantCulture, "</ol><ul style='font-weight:bold'>{0}</ul>", 
                                                                            GetLocalResourceObject("TabStageCase_GridMessageErrorGeneralValidator0Resource").ToString)                
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
 
#Region "TransStageCase_Grid Events"
    
    Protected Sub TransStageCase_Grid_CustomColumnDisplayText(sender As Object, e As DevExpress.Web.ASPxGridView.ASPxGridViewColumnDisplayTextEventArgs) Handles TransStageCase_Grid.CustomColumnDisplayText
          Dim data As DataTable
          Dim rows() As DataRow
           
          Select Case e.Column.FieldName

               Case Else              
           End Select
    End Sub
  
    Protected Sub TransStageCase_Grid_DataBinding(ByVal sender As Object, ByVal e As EventArgs) Handles TransStageCase_Grid.DataBinding
        If (Not IsNothing(Request.Params("__CALLBACKID")) AndAlso Request.Params("__CALLBACKID").EndsWith("TransStageCase_Grid")) Or _internalCall Then
                       If Caching.Exist("TabUnderwritingCaseType") Then
                DirectCast(TransStageCase_Grid.Columns("UNDERWRITINGCASETYPE"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = Caching.GetItem("TabUnderwritingCaseType")

            Else
                Dim source As DataTable = Nothing

                With New DataManagerFactory("SELECT  TABUNDERWRITINGCASETYPE.UNDERWRITINGCASETYPE, TABUNDERWRITINGCASETYPE.RECORDSTATUS, TRANSUNDERWRITINGCASETYPE.LANGUAGEID, TRANSUNDERWRITINGCASETYPE.DESCRIPTION FROM UNDERWRITING.TABUNDERWRITINGCASETYPE TABUNDERWRITINGCASETYPE JOIN TRANSUNDERWRITINGCASETYPE TRANSUNDERWRITINGCASETYPE ON TRANSUNDERWRITINGCASETYPE.UNDERWRITINGCASETYPE = TABUNDERWRITINGCASETYPE.UNDERWRITINGCASETYPE  WHERE TABUNDERWRITINGCASETYPE.RECORDSTATUS = 1 AND TRANSUNDERWRITINGCASETYPE.LANGUAGEID = @:LANGUAGEID  ORDER BY TransUnderwritingCaseType.Description ASC", "TabUnderwritingCaseType", "Linked.Underwriting")
                    .AddParameter("LANGUAGEID", DbType.Decimal, 5, False, CurrentState.Get("LanguageId"))
 
                    source = .QueryExecuteToTable(True)
                    DirectCast(TransStageCase_Grid.Columns("UNDERWRITINGCASETYPE"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = source
                End With

                If Not IsNothing(source) AndAlso source.Rows.Count > 0 Then
                    Caching.SetItem("TabUnderwritingCaseType", source)
                End If
            End If 
             If Caching.Exist("EnumRecordStatus") Then
                DirectCast(TransStageCase_Grid.Columns("RECORDSTATUS"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = Caching.GetItem("EnumRecordStatus")

            Else
                Dim source As DataTable = Nothing

                With New DataManagerFactory("SELECT  ENUMRECORDSTATUS.RECORDSTATUS, ENUMRECORDSTATUS.RECORDSTATUSCODE, ETRANRECORDSTATUS.LANGUAGEID, ETRANRECORDSTATUS.DESCRIPTION FROM COMMON.ENUMRECORDSTATUS ENUMRECORDSTATUS JOIN ETRANRECORDSTATUS ETRANRECORDSTATUS ON ETRANRECORDSTATUS.RECORDSTATUS = ENUMRECORDSTATUS.RECORDSTATUS  WHERE ENUMRECORDSTATUS.RECORDSTATUSCODE = '1' AND ETRANRECORDSTATUS.LANGUAGEID = @:LANGUAGEID  ORDER BY ETranRecordStatus.Description ASC", "EnumRecordStatus", "Linked.Common")
                    .AddParameter("LANGUAGEID", DbType.Decimal, 5, False, CurrentState.Get("LanguageId"))
 
                    source = .QueryExecuteToTable(True)
                    DirectCast(TransStageCase_Grid.Columns("RECORDSTATUS"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = source
                End With

                If Not IsNothing(source) AndAlso source.Rows.Count > 0 Then
                    Caching.SetItem("EnumRecordStatus", source)
                End If
            End If 
             If Caching.Exist("TabLanguage") Then
                DirectCast(TransStageCase_Grid.Columns("LANGUAGEID"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = Caching.GetItem("TabLanguage")

            Else
                Dim source As DataTable = Nothing

                With New DataManagerFactory("SELECT  TABLANGUAGE.LANGUAGEID, TABLANGUAGE.RECORDSTATUS, TRANSLANGUAGE.LANGUAGEID, TRANSLANGUAGE.DESCRIPTION FROM COMMON.TABLANGUAGE TABLANGUAGE JOIN TRANSLANGUAGE TRANSLANGUAGE ON TRANSLANGUAGE.LANGUAGECODEID = TABLANGUAGE.LANGUAGEID  WHERE TABLANGUAGE.RECORDSTATUS = '1' AND TRANSLANGUAGE.LANGUAGEID = @:LANGUAGEID  ORDER BY TransLanguage.Description ASC", "TabLanguage", "Linked.Common")
                    .AddParameter("LANGUAGEID", DbType.Decimal, 5, False, CurrentState.Get("LanguageId"))
 
                    source = .QueryExecuteToTable(True)
                    DirectCast(TransStageCase_Grid.Columns("LANGUAGEID"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = source
                End With

                If Not IsNothing(source) AndAlso source.Rows.Count > 0 Then
                    Caching.SetItem("TabLanguage", source)
                End If
            End If 
 

                 With New DataManagerFactory("SELECT  TABSTAGECASE.STAGE, TABSTAGECASE.LINEOFBUSINESS, TABSTAGECASE.PRODUCT, TABSTAGECASE.UNDERWRITINGCASETYPE, TABSTAGECASE.RECORDSTATUS, TRANSSTAGECASE.STAGE, TRANSSTAGECASE.LANGUAGEID, TRANSSTAGECASE.DESCRIPTION, TRANSSTAGECASE.SHORTDESCRIPTION FROM UNDERWRITING.TABSTAGECASE TABSTAGECASE JOIN UNDERWRITING.TRANSSTAGECASE TRANSSTAGECASE ON TRANSSTAGECASE.STAGE = TABSTAGECASE.STAGE   ORDER BY TabStageCase.Stage ASC", "TabStageCase", "Linked.Underwriting")                 
                                                   
                                  
                      TransStageCase_Grid.DataSource = .QueryExecuteToTable(True)
                 End With 
        End If     
    End Sub

    Protected Sub TransStageCase_Grid_CellEditorInitialize(sender As Object, e As ASPxGridViewEditorEventArgs) Handles TransStageCase_Grid.CellEditorInitialize
        If TransStageCase_Grid.IsNewRowEditing Then
            Select Case e.Column.FieldName
                
                
                
                Case "STAGE"                     
                       e.Editor.Focus()               
            End Select

        Else

            Select Case e.Column.FieldName
                Case "STAGE"
     e.Editor.Enabled = False
Case "LANGUAGEID"
     e.Editor.Enabled = False
                   
                
                
                Case "LINEOFBUSINESS"                     
                     e.Editor.Focus()
            End Select
        End If
        
       Select Case e.Column.FieldName
           Case "STAGE"
                 
                 
           Case "UNDERWRITINGCASETYPE"
     DirectCast(e.Editor, ASPxComboBox).DataBindItems() 
Case "RECORDSTATUS"
     DirectCast(e.Editor, ASPxComboBox).DataBindItems() 
Case "LANGUAGEID"
     DirectCast(e.Editor, ASPxComboBox).DataBindItems() 

        End Select
    End Sub

    Protected Sub TransStageCase_Grid_RowInserting(ByVal sender As Object, ByVal e As ASPxDataInsertingEventArgs) Handles TransStageCase_Grid.RowInserting 
        Dim isNullResult As Boolean = True
        
        
               
        e.Cancel = True
        TransStageCase_Grid.CancelEdit()
    End Sub

    Protected Sub TransStageCase_Grid_RowUpdating(ByVal sender As Object, ByVal e As ASPxDataUpdatingEventArgs) Handles TransStageCase_Grid.RowUpdating
        Dim isNullResult As Boolean = True
          
             With New DataManagerFactory("UPDATE UNDERWRITING.TransStageCase SET DESCRIPTION = @:DESCRIPTION, SHORTDESCRIPTION = @:SHORTDESCRIPTION, UPDATEUSERCODE = @:UPDATEUSERCODE WHERE STAGE = @:STAGE AND LANGUAGEID = @:LANGUAGEID", "TransStageCase", "Linked.Underwriting")                 
                                                   
                       .AddParameter("DESCRIPTION", DbType.AnsiString, 0, False, e.NewValues("DESCRIPTION"))
.AddParameter("SHORTDESCRIPTION", DbType.AnsiString, 0, False, e.NewValues("SHORTDESCRIPTION"))
.AddParameter("UPDATEUSERCODE", DbType.Decimal, 0, False, Session("nUserCode"))
.AddParameter("STAGE", DbType.Decimal, 0, False, e.Keys("STAGE"))
.AddParameter("LANGUAGEID", DbType.Decimal, 0, False, e.Keys("LANGUAGEID"))
            
                       .CommandExecute()
                End With         
      
         e.Cancel = True
        TransStageCase_Grid.CancelEdit()
    End Sub
    
    Protected Sub TransStageCase_Grid_CustomCallback(sender As Object, e As ASPxGridViewCustomCallbackEventArgs) Handles TransStageCase_Grid.CustomCallback     
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
    
    Protected Sub TransStageCase_Grid_RowValidating(sender As Object, e As ASPxDataValidationEventArgs) Handles TransStageCase_Grid.RowValidating
        Dim errorMessage As String = "<ol style='font-weight:lighter'>"
        
        If IsNothing(e.NewValues("STAGE")) OrElse e.NewValues("STAGE") = 0  
   e.Errors(TransStageCase_Grid.Columns("STAGE")) = GetLocalResourceObject("STAGEMessageErrorRequired1Resource").ToString
End If
 
        
        If e.IsNewRow Then
           Dim rowCountKey As System.Int32
  With New DataManagerFactory("SELECT  TRANSSTAGECASE.STAGE ROWCOUNT, TRANSSTAGECASE.LANGUAGEID FROM UNDERWRITING.TRANSSTAGECASE TRANSSTAGECASE  WHERE TRANSSTAGECASE.STAGE = @:STAGE AND TRANSSTAGECASE.LANGUAGEID = @:LANGUAGEID", "TransStageCase", "Linked.Underwriting")
             .AddParameter("STAGE", DbType.Decimal, 5, False, e.NewValues("STAGE"))
.AddParameter("LANGUAGEID", DbType.Decimal, 5, False, e.NewValues("LANGUAGEID"))
 
             rowCountKey = .QueryExecuteScalarToInteger()  
        End With
        If rowCountKey > 0 Then
                errorMessage += String.Format(CultureInfo.InvariantCulture, "</ol><ul style='font-weight:bold'>{0}</ul>", 
                                                                            GetLocalResourceObject("TransStageCase_GridMessageErrorGeneralValidator0Resource").ToString)                
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