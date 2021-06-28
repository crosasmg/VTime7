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

Partial Class Maintenance_TabRequirementType
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

            TabRequirementType_Grid.DataBind()
        End If      
    End Sub

#End Region

#Region "Controls Events"

         
    
 

#End Region

#Region "TabRequirementType_Grid Events"
    
    Protected Sub TabRequirementType_Grid_CustomColumnDisplayText(sender As Object, e As DevExpress.Web.ASPxGridView.ASPxGridViewColumnDisplayTextEventArgs) Handles TabRequirementType_Grid.CustomColumnDisplayText
          Dim data As DataTable
          Dim rows() As DataRow
           
          Select Case e.Column.FieldName

               Case Else              
           End Select
    End Sub
  
    Protected Sub TabRequirementType_Grid_DataBinding(ByVal sender As Object, ByVal e As EventArgs) Handles TabRequirementType_Grid.DataBinding
        If (Not IsNothing(Request.Params("__CALLBACKID")) AndAlso Request.Params("__CALLBACKID").EndsWith("TabRequirementType_Grid")) Or _internalCall Then
                       If Caching.Exist("TabProcessType") Then
                DirectCast(TabRequirementType_Grid.Columns("PROCESSTYPE"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = Caching.GetItem("TabProcessType")

            Else
                Dim source As DataTable = Nothing

                With New DataManagerFactory("SELECT  TABPROCESSTYPE.PROCESSTYPE, TABPROCESSTYPE.RECORDSTATUS, TRANSPROCESSTYPE.LANGUAGEID, TRANSPROCESSTYPE.DESCRIPTION FROM UNDERWRITING.TABPROCESSTYPE TABPROCESSTYPE JOIN TRANSPROCESSTYPE TRANSPROCESSTYPE ON TRANSPROCESSTYPE.PROCESSTYPE = TABPROCESSTYPE.PROCESSTYPE  WHERE TABPROCESSTYPE.RECORDSTATUS = 1 AND TRANSPROCESSTYPE.LANGUAGEID = @:LANGUAGEID  ORDER BY TransProcessType.Description ASC", "TabProcessType", "Linked.Underwriting")
                    .AddParameter("LANGUAGEID", DbType.Decimal, 5, False, CurrentState.Get("LanguageId"))
 
                    source = .QueryExecuteToTable(True)
                    DirectCast(TabRequirementType_Grid.Columns("PROCESSTYPE"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = source
                End With

                If Not IsNothing(source) AndAlso source.Rows.Count > 0 Then
                    Caching.SetItem("TabProcessType", source)
                End If
            End If 
             If Caching.Exist("TabUnderwritingAreaType") Then
                DirectCast(TabRequirementType_Grid.Columns("UNDERWRITINGAREA"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = Caching.GetItem("TabUnderwritingAreaType")

            Else
                Dim source As DataTable = Nothing

                With New DataManagerFactory("SELECT  TABUNDERWRITINGAREATYPE.UNDERWRITINGAREA, TABUNDERWRITINGAREATYPE.RECORDSTATUS, TRANSUNDERWRITINGAREATYPE.LANGUAGEID, TRANSUNDERWRITINGAREATYPE.DESCRIPTION FROM UNDERWRITING.TABUNDERWRITINGAREATYPE TABUNDERWRITINGAREATYPE JOIN TRANSUNDERWRITINGAREATYPE TRANSUNDERWRITINGAREATYPE ON TRANSUNDERWRITINGAREATYPE.UNDERWRITINGAREA = TABUNDERWRITINGAREATYPE.UNDERWRITINGAREA  WHERE TABUNDERWRITINGAREATYPE.RECORDSTATUS = 1 AND TRANSUNDERWRITINGAREATYPE.LANGUAGEID = @:LANGUAGEID  ORDER BY TransUnderwritingAreaType.Description ASC", "TabUnderwritingAreaType", "Linked.Underwriting")
                    .AddParameter("LANGUAGEID", DbType.Decimal, 5, False, CurrentState.Get("LanguageId"))
 
                    source = .QueryExecuteToTable(True)
                    DirectCast(TabRequirementType_Grid.Columns("UNDERWRITINGAREA"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = source
                End With

                If Not IsNothing(source) AndAlso source.Rows.Count > 0 Then
                    Caching.SetItem("TabUnderwritingAreaType", source)
                End If
            End If 
             If Caching.Exist("TabPayableByType") Then
                DirectCast(TabRequirementType_Grid.Columns("PAYER"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = Caching.GetItem("TabPayableByType")

            Else
                Dim source As DataTable = Nothing

                With New DataManagerFactory("SELECT  TABPAYABLEBYTYPE.PAYER, TABPAYABLEBYTYPE.RECORDSTATUS, TRANSPAYABLEBYTYPE.LANGUAGEID, TRANSPAYABLEBYTYPE.DESCRIPTION FROM UNDERWRITING.TABPAYABLEBYTYPE TABPAYABLEBYTYPE JOIN TRANSPAYABLEBYTYPE TRANSPAYABLEBYTYPE ON TRANSPAYABLEBYTYPE.PAYER = TABPAYABLEBYTYPE.PAYER  WHERE TABPAYABLEBYTYPE.RECORDSTATUS = 1 AND TRANSPAYABLEBYTYPE.LANGUAGEID = @:LANGUAGEID  ORDER BY TransPayableByType.Description ASC", "TabPayableByType", "Linked.Underwriting")
                    .AddParameter("LANGUAGEID", DbType.Decimal, 5, False, CurrentState.Get("LanguageId"))
 
                    source = .QueryExecuteToTable(True)
                    DirectCast(TabRequirementType_Grid.Columns("PAYER"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = source
                End With

                If Not IsNothing(source) AndAlso source.Rows.Count > 0 Then
                    Caching.SetItem("TabPayableByType", source)
                End If
            End If 
             If Caching.Exist("EnumRecordStatus") Then
                DirectCast(TabRequirementType_Grid.Columns("RECORDSTATUS"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = Caching.GetItem("EnumRecordStatus")

            Else
                Dim source As DataTable = Nothing

                With New DataManagerFactory("SELECT  ENUMRECORDSTATUS.RECORDSTATUS, ENUMRECORDSTATUS.RECORDSTATUSCODE, ETRANRECORDSTATUS.LANGUAGEID, ETRANRECORDSTATUS.DESCRIPTION FROM COMMON.ENUMRECORDSTATUS ENUMRECORDSTATUS JOIN ETRANRECORDSTATUS ETRANRECORDSTATUS ON ETRANRECORDSTATUS.RECORDSTATUS = ENUMRECORDSTATUS.RECORDSTATUS  WHERE ENUMRECORDSTATUS.RECORDSTATUSCODE = '1' AND ETRANRECORDSTATUS.LANGUAGEID = @:LANGUAGEID  ORDER BY ETranRecordStatus.Description ASC", "EnumRecordStatus", "Linked.Common")
                    .AddParameter("LANGUAGEID", DbType.Decimal, 5, False, CurrentState.Get("LanguageId"))
 
                    source = .QueryExecuteToTable(True)
                    DirectCast(TabRequirementType_Grid.Columns("RECORDSTATUS"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = source
                End With

                If Not IsNothing(source) AndAlso source.Rows.Count > 0 Then
                    Caching.SetItem("EnumRecordStatus", source)
                End If
            End If 
 

                 With New DataManagerFactory("SELECT  TABREQUIREMENTTYPE.REQUIREMENTTYPE, TABREQUIREMENTTYPE.PROCESSTYPE, TABREQUIREMENTTYPE.UNDERWRITINGAREA, TABREQUIREMENTTYPE.PAYER, TABREQUIREMENTTYPE.COST, TABREQUIREMENTTYPE.LINK, TABREQUIREMENTTYPE.ACORDREQUIREMENTCODE, TABREQUIREMENTTYPE.RECORDSTATUS, TRANSREQUIREMENTTYPE.REQUIREMENTTYPE, TRANSREQUIREMENTTYPE.LANGUAGEID, TRANSREQUIREMENTTYPE.DESCRIPTION, TRANSREQUIREMENTTYPE.SHORTDESCRIPTION FROM UNDERWRITING.TABREQUIREMENTTYPE TABREQUIREMENTTYPE JOIN UNDERWRITING.TRANSREQUIREMENTTYPE TRANSREQUIREMENTTYPE ON TRANSREQUIREMENTTYPE.REQUIREMENTTYPE = TABREQUIREMENTTYPE.REQUIREMENTTYPE  WHERE TRANSREQUIREMENTTYPE.LANGUAGEID = @:LANGUAGEID ORDER BY TabRequirementType.RequirementType ASC", "TabRequirementType", "Linked.Underwriting")                 
                                                   
                      .AddParameter("LANGUAGEID", DbType.Decimal, 5, False, CurrentState.Get("LanguageId"))
            
                      TabRequirementType_Grid.DataSource = .QueryExecuteToTable(True)
                 End With 
        End If     
    End Sub

    Protected Sub TabRequirementType_Grid_CellEditorInitialize(sender As Object, e As ASPxGridViewEditorEventArgs) Handles TabRequirementType_Grid.CellEditorInitialize
        If TabRequirementType_Grid.IsNewRowEditing Then
            Select Case e.Column.FieldName
                
                
                
                Case "REQUIREMENTTYPE"                     
                       e.Editor.Focus()               
            End Select

        Else

            Select Case e.Column.FieldName
                Case "REQUIREMENTTYPE"
     e.Editor.Enabled = False
                   
                
                
                Case "PROCESSTYPE"                     
                     e.Editor.Focus()
            End Select
        End If
        
       Select Case e.Column.FieldName
           Case "REQUIREMENTTYPE"
                 
                 
           Case "PROCESSTYPE"
     DirectCast(e.Editor, ASPxComboBox).DataBindItems() 
Case "UNDERWRITINGAREA"
     DirectCast(e.Editor, ASPxComboBox).DataBindItems() 
Case "PAYER"
     DirectCast(e.Editor, ASPxComboBox).DataBindItems() 
Case "RECORDSTATUS"
     DirectCast(e.Editor, ASPxComboBox).DataBindItems() 

        End Select
    End Sub

    Protected Sub TabRequirementType_Grid_RowInserting(ByVal sender As Object, ByVal e As ASPxDataInsertingEventArgs) Handles TabRequirementType_Grid.RowInserting 
        Dim isNullResult As Boolean = True
        
             With New DataManagerFactory("INSERT INTO UNDERWRITING.TabRequirementType (REQUIREMENTTYPE, PROCESSTYPE, UNDERWRITINGAREA, PAYER, COST, LINK, ACORDREQUIREMENTCODE, RECORDSTATUS, CREATORUSERCODE, CREATIONDATE, UPDATEUSERCODE, UPDATEDATE) VALUES (@:REQUIREMENTTYPE, @:PROCESSTYPE, @:UNDERWRITINGAREA, @:PAYER, @:COST, @:LINK, @:ACORDREQUIREMENTCODE, @:RECORDSTATUS, @:CREATORUSERCODE, SYSDATE, @:UPDATEUSERCODE, SYSDATE)", "TabRequirementType", "Linked.Underwriting")                 
                                                   
                       .AddParameter("REQUIREMENTTYPE", DbType.Decimal, 0, False, e.NewValues("REQUIREMENTTYPE"))
.AddParameter("PROCESSTYPE", DbType.Decimal, 0, False, e.NewValues("PROCESSTYPE"))
.AddParameter("UNDERWRITINGAREA", DbType.Decimal, 0, False, e.NewValues("UNDERWRITINGAREA"))
.AddParameter("PAYER", DbType.Decimal, 0, False, e.NewValues("PAYER"))
.AddParameter("COST", DbType.Decimal, 0, (e.NewValues("COST") = 0), e.NewValues("COST"))
.AddParameter("LINK", DbType.AnsiString, 0, (e.NewValues("LINK") = String.Empty), e.NewValues("LINK"))
.AddParameter("ACORDREQUIREMENTCODE", DbType.Decimal, 0, (e.NewValues("ACORDREQUIREMENTCODE") = 0), e.NewValues("ACORDREQUIREMENTCODE"))
.AddParameter("RECORDSTATUS", DbType.Decimal, 0, False, e.NewValues("RECORDSTATUS"))
.AddParameter("CREATORUSERCODE", DbType.Decimal, 0, False, Session("nUserCode"))
.AddParameter("UPDATEUSERCODE", DbType.Decimal, 0, False, Session("nUserCode"))
            
                       .CommandExecute()
                End With
            For Each languageItem As KeyValuePair(Of Integer, String) In LanguageToDictionary(LanguageId)
                     With New DataManagerFactory("INSERT INTO UNDERWRITING.TransRequirementType (REQUIREMENTTYPE, LANGUAGEID, DESCRIPTION, SHORTDESCRIPTION, CREATORUSERCODE, CREATIONDATE, UPDATEUSERCODE, UPDATEDATE) VALUES (@:REQUIREMENTTYPE, @:LANGUAGEID, @:DESCRIPTION, @:SHORTDESCRIPTION, @:CREATORUSERCODE, SYSDATE, @:UPDATEUSERCODE, SYSDATE)", "TransRequirementType", "Linked.Underwriting")                 
                                                   
                       .AddParameter("REQUIREMENTTYPE", DbType.Decimal, 0, False, e.NewValues("REQUIREMENTTYPE"))
.AddParameter("LANGUAGEID", DbType.Decimal, 0, False, languageItem.Key)
.AddParameter("DESCRIPTION", DbType.AnsiString, 0, False, e.NewValues("DESCRIPTION"))
.AddParameter("SHORTDESCRIPTION", DbType.AnsiString, 0, False, e.NewValues("SHORTDESCRIPTION"))
.AddParameter("CREATORUSERCODE", DbType.Decimal, 0, False, Session("nUserCode"))
.AddParameter("UPDATEUSERCODE", DbType.Decimal, 0, False, Session("nUserCode"))
            
                       .CommandExecute()
                End With
           Next

               
        e.Cancel = True
        TabRequirementType_Grid.CancelEdit()
    End Sub

    Protected Sub TabRequirementType_Grid_RowUpdating(ByVal sender As Object, ByVal e As ASPxDataUpdatingEventArgs) Handles TabRequirementType_Grid.RowUpdating
        Dim isNullResult As Boolean = True
          
             With New DataManagerFactory("UPDATE UNDERWRITING.TabRequirementType SET PROCESSTYPE = @:PROCESSTYPE, UNDERWRITINGAREA = @:UNDERWRITINGAREA, PAYER = @:PAYER, COST = @:COST, LINK = @:LINK, ACORDREQUIREMENTCODE = @:ACORDREQUIREMENTCODE, RECORDSTATUS = @:RECORDSTATUS, UPDATEUSERCODE = @:UPDATEUSERCODE WHERE REQUIREMENTTYPE = @:REQUIREMENTTYPE", "TabRequirementType", "Linked.Underwriting")                 
                                                   
                       .AddParameter("PROCESSTYPE", DbType.Decimal, 0, False, e.NewValues("PROCESSTYPE"))
.AddParameter("UNDERWRITINGAREA", DbType.Decimal, 0, False, e.NewValues("UNDERWRITINGAREA"))
.AddParameter("PAYER", DbType.Decimal, 0, False, e.NewValues("PAYER"))
.AddParameter("COST", DbType.Decimal, 0, (e.NewValues("COST") = 0), e.NewValues("COST"))
.AddParameter("LINK", DbType.AnsiString, 0, (e.NewValues("LINK") = String.Empty), e.NewValues("LINK"))
.AddParameter("ACORDREQUIREMENTCODE", DbType.Decimal, 0, (e.NewValues("ACORDREQUIREMENTCODE") = 0), e.NewValues("ACORDREQUIREMENTCODE"))
.AddParameter("RECORDSTATUS", DbType.Decimal, 0, False, e.NewValues("RECORDSTATUS"))
.AddParameter("UPDATEUSERCODE", DbType.Decimal, 0, False, Session("nUserCode"))
.AddParameter("REQUIREMENTTYPE", DbType.Decimal, 0, False, e.Keys("REQUIREMENTTYPE"))
            
                       .CommandExecute()
                End With
     With New DataManagerFactory("UPDATE UNDERWRITING.TransRequirementType SET DESCRIPTION = @:DESCRIPTION, SHORTDESCRIPTION = @:SHORTDESCRIPTION, UPDATEUSERCODE = @:UPDATEUSERCODE WHERE REQUIREMENTTYPE = @:REQUIREMENTTYPE AND LANGUAGEID = @:LANGUAGEID", "TransRequirementType", "Linked.Underwriting")                 
                                                   
                       .AddParameter("DESCRIPTION", DbType.AnsiString, 0, False, e.NewValues("DESCRIPTION"))
.AddParameter("SHORTDESCRIPTION", DbType.AnsiString, 0, False, e.NewValues("SHORTDESCRIPTION"))
.AddParameter("UPDATEUSERCODE", DbType.Decimal, 0, False, Session("nUserCode"))
.AddParameter("REQUIREMENTTYPE", DbType.Decimal, 0, False, e.Keys("REQUIREMENTTYPE"))
.AddParameter("LANGUAGEID", DbType.Decimal, 0, False, CurrentState.Get("LanguageId"))
            
                       .CommandExecute()
                End With         
      
         e.Cancel = True
        TabRequirementType_Grid.CancelEdit()
    End Sub
    
    Protected Sub TabRequirementType_Grid_CustomCallback(sender As Object, e As ASPxGridViewCustomCallbackEventArgs) Handles TabRequirementType_Grid.CustomCallback     
           Dim isNullResult As Boolean = True
           
           Select Case e.Parameters.ToString.ToLower
               Case "delete"
                       Dim REQUIREMENTTYPEKey As Generic.List(Of Object) = TabRequirementType_Grid.GetSelectedFieldValues("REQUIREMENTTYPE")
        
               For index As Integer = 0 To REQUIREMENTTYPEKey.Count - 1
                  With New DataManagerFactory("DELETE FROM UNDERWRITING.TransRequirementType WHERE REQUIREMENTTYPE = @:REQUIREMENTTYPE ", "TransRequirementType", "Linked.Underwriting")                 
                                                   
               .AddParameter("REQUIREMENTTYPE", DbType.Decimal, 0, False, REQUIREMENTTYPEKey(index))
            
               .CommandExecute()
          End With 
 With New DataManagerFactory("DELETE FROM UNDERWRITING.TabRequirementType WHERE REQUIREMENTTYPE = @:REQUIREMENTTYPE ", "TabRequirementType", "Linked.Underwriting")                 
                                                   
               .AddParameter("REQUIREMENTTYPE", DbType.Decimal, 0, False, REQUIREMENTTYPEKey(index))
            
               .CommandExecute()
          End With 
                       
              Next           
           
              TabRequirementType_Grid.DataBind()
                 
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
    
    Protected Sub TabRequirementType_Grid_RowValidating(sender As Object, e As ASPxDataValidationEventArgs) Handles TabRequirementType_Grid.RowValidating
        Dim errorMessage As String = "<ol style='font-weight:lighter'>"
        
        If IsNothing(e.NewValues("REQUIREMENTTYPE")) OrElse e.NewValues("REQUIREMENTTYPE") = 0  
   e.Errors(TabRequirementType_Grid.Columns("REQUIREMENTTYPE")) = GetLocalResourceObject("REQUIREMENTTYPEMessageErrorRequired1Resource").ToString
End If
 
        
        If e.IsNewRow Then
           Dim rowCountKey As System.Int32
  With New DataManagerFactory("SELECT  TABREQUIREMENTTYPE.REQUIREMENTTYPE ROWCOUNT FROM UNDERWRITING.TABREQUIREMENTTYPE TABREQUIREMENTTYPE  WHERE TABREQUIREMENTTYPE.REQUIREMENTTYPE = @:REQUIREMENTTYPE", "TabRequirementType", "Linked.Underwriting")
             .AddParameter("REQUIREMENTTYPE", DbType.Decimal, 5, False, e.NewValues("REQUIREMENTTYPE"))
 
             rowCountKey = .QueryExecuteScalarToInteger()  
        End With
        If rowCountKey > 0 Then
                errorMessage += String.Format(CultureInfo.InvariantCulture, "</ol><ul style='font-weight:bold'>{0}</ul>", 
                                                                            GetLocalResourceObject("TabRequirementType_GridMessageErrorGeneralValidator0Resource").ToString)                
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
 
#Region "TransRequirementType_Grid Events"
    
    Protected Sub TransRequirementType_Grid_CustomColumnDisplayText(sender As Object, e As DevExpress.Web.ASPxGridView.ASPxGridViewColumnDisplayTextEventArgs) Handles TransRequirementType_Grid.CustomColumnDisplayText
          Dim data As DataTable
          Dim rows() As DataRow
           
          Select Case e.Column.FieldName

               Case Else              
           End Select
    End Sub
  
    Protected Sub TransRequirementType_Grid_DataBinding(ByVal sender As Object, ByVal e As EventArgs) Handles TransRequirementType_Grid.DataBinding
        If (Not IsNothing(Request.Params("__CALLBACKID")) AndAlso Request.Params("__CALLBACKID").EndsWith("TransRequirementType_Grid")) Or _internalCall Then
                       If Caching.Exist("TabProcessType") Then
                DirectCast(TransRequirementType_Grid.Columns("PROCESSTYPE"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = Caching.GetItem("TabProcessType")

            Else
                Dim source As DataTable = Nothing

                With New DataManagerFactory("SELECT  TABPROCESSTYPE.PROCESSTYPE, TABPROCESSTYPE.RECORDSTATUS, TRANSPROCESSTYPE.LANGUAGEID, TRANSPROCESSTYPE.DESCRIPTION FROM UNDERWRITING.TABPROCESSTYPE TABPROCESSTYPE JOIN TRANSPROCESSTYPE TRANSPROCESSTYPE ON TRANSPROCESSTYPE.PROCESSTYPE = TABPROCESSTYPE.PROCESSTYPE  WHERE TABPROCESSTYPE.RECORDSTATUS = 1 AND TRANSPROCESSTYPE.LANGUAGEID = @:LANGUAGEID  ORDER BY TransProcessType.Description ASC", "TabProcessType", "Linked.Underwriting")
                    .AddParameter("LANGUAGEID", DbType.Decimal, 5, False, CurrentState.Get("LanguageId"))
 
                    source = .QueryExecuteToTable(True)
                    DirectCast(TransRequirementType_Grid.Columns("PROCESSTYPE"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = source
                End With

                If Not IsNothing(source) AndAlso source.Rows.Count > 0 Then
                    Caching.SetItem("TabProcessType", source)
                End If
            End If 
             If Caching.Exist("TabUnderwritingAreaType") Then
                DirectCast(TransRequirementType_Grid.Columns("UNDERWRITINGAREA"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = Caching.GetItem("TabUnderwritingAreaType")

            Else
                Dim source As DataTable = Nothing

                With New DataManagerFactory("SELECT  TABUNDERWRITINGAREATYPE.UNDERWRITINGAREA, TABUNDERWRITINGAREATYPE.RECORDSTATUS, TRANSUNDERWRITINGAREATYPE.LANGUAGEID, TRANSUNDERWRITINGAREATYPE.DESCRIPTION FROM UNDERWRITING.TABUNDERWRITINGAREATYPE TABUNDERWRITINGAREATYPE JOIN TRANSUNDERWRITINGAREATYPE TRANSUNDERWRITINGAREATYPE ON TRANSUNDERWRITINGAREATYPE.UNDERWRITINGAREA = TABUNDERWRITINGAREATYPE.UNDERWRITINGAREA  WHERE TABUNDERWRITINGAREATYPE.RECORDSTATUS = 1 AND TRANSUNDERWRITINGAREATYPE.LANGUAGEID = @:LANGUAGEID  ORDER BY TransUnderwritingAreaType.Description ASC", "TabUnderwritingAreaType", "Linked.Underwriting")
                    .AddParameter("LANGUAGEID", DbType.Decimal, 5, False, CurrentState.Get("LanguageId"))
 
                    source = .QueryExecuteToTable(True)
                    DirectCast(TransRequirementType_Grid.Columns("UNDERWRITINGAREA"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = source
                End With

                If Not IsNothing(source) AndAlso source.Rows.Count > 0 Then
                    Caching.SetItem("TabUnderwritingAreaType", source)
                End If
            End If 
             If Caching.Exist("TabPayableByType") Then
                DirectCast(TransRequirementType_Grid.Columns("PAYER"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = Caching.GetItem("TabPayableByType")

            Else
                Dim source As DataTable = Nothing

                With New DataManagerFactory("SELECT  TABPAYABLEBYTYPE.PAYER, TABPAYABLEBYTYPE.RECORDSTATUS, TRANSPAYABLEBYTYPE.LANGUAGEID, TRANSPAYABLEBYTYPE.DESCRIPTION FROM UNDERWRITING.TABPAYABLEBYTYPE TABPAYABLEBYTYPE JOIN TRANSPAYABLEBYTYPE TRANSPAYABLEBYTYPE ON TRANSPAYABLEBYTYPE.PAYER = TABPAYABLEBYTYPE.PAYER  WHERE TABPAYABLEBYTYPE.RECORDSTATUS = 1 AND TRANSPAYABLEBYTYPE.LANGUAGEID = @:LANGUAGEID  ORDER BY TransPayableByType.Description ASC", "TabPayableByType", "Linked.Underwriting")
                    .AddParameter("LANGUAGEID", DbType.Decimal, 5, False, CurrentState.Get("LanguageId"))
 
                    source = .QueryExecuteToTable(True)
                    DirectCast(TransRequirementType_Grid.Columns("PAYER"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = source
                End With

                If Not IsNothing(source) AndAlso source.Rows.Count > 0 Then
                    Caching.SetItem("TabPayableByType", source)
                End If
            End If 
             If Caching.Exist("EnumRecordStatus") Then
                DirectCast(TransRequirementType_Grid.Columns("RECORDSTATUS"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = Caching.GetItem("EnumRecordStatus")

            Else
                Dim source As DataTable = Nothing

                With New DataManagerFactory("SELECT  ENUMRECORDSTATUS.RECORDSTATUS, ENUMRECORDSTATUS.RECORDSTATUSCODE, ETRANRECORDSTATUS.LANGUAGEID, ETRANRECORDSTATUS.DESCRIPTION FROM COMMON.ENUMRECORDSTATUS ENUMRECORDSTATUS JOIN ETRANRECORDSTATUS ETRANRECORDSTATUS ON ETRANRECORDSTATUS.RECORDSTATUS = ENUMRECORDSTATUS.RECORDSTATUS  WHERE ENUMRECORDSTATUS.RECORDSTATUSCODE = '1' AND ETRANRECORDSTATUS.LANGUAGEID = @:LANGUAGEID  ORDER BY ETranRecordStatus.Description ASC", "EnumRecordStatus", "Linked.Common")
                    .AddParameter("LANGUAGEID", DbType.Decimal, 5, False, CurrentState.Get("LanguageId"))
 
                    source = .QueryExecuteToTable(True)
                    DirectCast(TransRequirementType_Grid.Columns("RECORDSTATUS"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = source
                End With

                If Not IsNothing(source) AndAlso source.Rows.Count > 0 Then
                    Caching.SetItem("EnumRecordStatus", source)
                End If
            End If 
             If Caching.Exist("TabLanguage") Then
                DirectCast(TransRequirementType_Grid.Columns("LANGUAGEID"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = Caching.GetItem("TabLanguage")

            Else
                Dim source As DataTable = Nothing

                With New DataManagerFactory("SELECT  TABLANGUAGE.LANGUAGEID, TABLANGUAGE.RECORDSTATUS, TRANSLANGUAGE.LANGUAGEID, TRANSLANGUAGE.DESCRIPTION FROM COMMON.TABLANGUAGE TABLANGUAGE JOIN TRANSLANGUAGE TRANSLANGUAGE ON TRANSLANGUAGE.LANGUAGECODEID = TABLANGUAGE.LANGUAGEID  WHERE TABLANGUAGE.RECORDSTATUS = '1' AND TRANSLANGUAGE.LANGUAGEID = @:LANGUAGEID  ORDER BY TransLanguage.Description ASC", "TabLanguage", "Linked.Common")
                    .AddParameter("LANGUAGEID", DbType.Decimal, 5, False, CurrentState.Get("LanguageId"))
 
                    source = .QueryExecuteToTable(True)
                    DirectCast(TransRequirementType_Grid.Columns("LANGUAGEID"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = source
                End With

                If Not IsNothing(source) AndAlso source.Rows.Count > 0 Then
                    Caching.SetItem("TabLanguage", source)
                End If
            End If 
 

                 With New DataManagerFactory("SELECT  TABREQUIREMENTTYPE.REQUIREMENTTYPE, TABREQUIREMENTTYPE.PROCESSTYPE, TABREQUIREMENTTYPE.UNDERWRITINGAREA, TABREQUIREMENTTYPE.PAYER, TABREQUIREMENTTYPE.COST, TABREQUIREMENTTYPE.LINK, TABREQUIREMENTTYPE.ACORDREQUIREMENTCODE, TABREQUIREMENTTYPE.RECORDSTATUS, TRANSREQUIREMENTTYPE.REQUIREMENTTYPE, TRANSREQUIREMENTTYPE.LANGUAGEID, TRANSREQUIREMENTTYPE.DESCRIPTION, TRANSREQUIREMENTTYPE.SHORTDESCRIPTION FROM UNDERWRITING.TABREQUIREMENTTYPE TABREQUIREMENTTYPE JOIN UNDERWRITING.TRANSREQUIREMENTTYPE TRANSREQUIREMENTTYPE ON TRANSREQUIREMENTTYPE.REQUIREMENTTYPE = TABREQUIREMENTTYPE.REQUIREMENTTYPE   ORDER BY TabRequirementType.RequirementType ASC", "TabRequirementType", "Linked.Underwriting")                 
                                                   
                                  
                      TransRequirementType_Grid.DataSource = .QueryExecuteToTable(True)
                 End With 
        End If     
    End Sub

    Protected Sub TransRequirementType_Grid_CellEditorInitialize(sender As Object, e As ASPxGridViewEditorEventArgs) Handles TransRequirementType_Grid.CellEditorInitialize
        If TransRequirementType_Grid.IsNewRowEditing Then
            Select Case e.Column.FieldName
                
                
                
                Case "REQUIREMENTTYPE"                     
                       e.Editor.Focus()               
            End Select

        Else

            Select Case e.Column.FieldName
                Case "REQUIREMENTTYPE"
     e.Editor.Enabled = False
Case "LANGUAGEID"
     e.Editor.Enabled = False
                   
                
                
                Case "PROCESSTYPE"                     
                     e.Editor.Focus()
            End Select
        End If
        
       Select Case e.Column.FieldName
           Case "REQUIREMENTTYPE"
                 
                 
           Case "PROCESSTYPE"
     DirectCast(e.Editor, ASPxComboBox).DataBindItems() 
Case "UNDERWRITINGAREA"
     DirectCast(e.Editor, ASPxComboBox).DataBindItems() 
Case "PAYER"
     DirectCast(e.Editor, ASPxComboBox).DataBindItems() 
Case "RECORDSTATUS"
     DirectCast(e.Editor, ASPxComboBox).DataBindItems() 
Case "LANGUAGEID"
     DirectCast(e.Editor, ASPxComboBox).DataBindItems() 

        End Select
    End Sub

    Protected Sub TransRequirementType_Grid_RowInserting(ByVal sender As Object, ByVal e As ASPxDataInsertingEventArgs) Handles TransRequirementType_Grid.RowInserting 
        Dim isNullResult As Boolean = True
        
        
               
        e.Cancel = True
        TransRequirementType_Grid.CancelEdit()
    End Sub

    Protected Sub TransRequirementType_Grid_RowUpdating(ByVal sender As Object, ByVal e As ASPxDataUpdatingEventArgs) Handles TransRequirementType_Grid.RowUpdating
        Dim isNullResult As Boolean = True
          
             With New DataManagerFactory("UPDATE UNDERWRITING.TransRequirementType SET DESCRIPTION = @:DESCRIPTION, SHORTDESCRIPTION = @:SHORTDESCRIPTION, UPDATEUSERCODE = @:UPDATEUSERCODE WHERE REQUIREMENTTYPE = @:REQUIREMENTTYPE AND LANGUAGEID = @:LANGUAGEID", "TransRequirementType", "Linked.Underwriting")                 
                                                   
                       .AddParameter("DESCRIPTION", DbType.AnsiString, 0, False, e.NewValues("DESCRIPTION"))
.AddParameter("SHORTDESCRIPTION", DbType.AnsiString, 0, False, e.NewValues("SHORTDESCRIPTION"))
.AddParameter("UPDATEUSERCODE", DbType.Decimal, 0, False, Session("nUserCode"))
.AddParameter("REQUIREMENTTYPE", DbType.Decimal, 0, False, e.Keys("REQUIREMENTTYPE"))
.AddParameter("LANGUAGEID", DbType.Decimal, 0, False, e.Keys("LANGUAGEID"))
            
                       .CommandExecute()
                End With         
      
         e.Cancel = True
        TransRequirementType_Grid.CancelEdit()
    End Sub
    
    Protected Sub TransRequirementType_Grid_CustomCallback(sender As Object, e As ASPxGridViewCustomCallbackEventArgs) Handles TransRequirementType_Grid.CustomCallback     
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
    
    Protected Sub TransRequirementType_Grid_RowValidating(sender As Object, e As ASPxDataValidationEventArgs) Handles TransRequirementType_Grid.RowValidating
        Dim errorMessage As String = "<ol style='font-weight:lighter'>"
        
        If IsNothing(e.NewValues("REQUIREMENTTYPE")) OrElse e.NewValues("REQUIREMENTTYPE") = 0  
   e.Errors(TransRequirementType_Grid.Columns("REQUIREMENTTYPE")) = GetLocalResourceObject("REQUIREMENTTYPEMessageErrorRequired1Resource").ToString
End If
 
        
        If e.IsNewRow Then
           Dim rowCountKey As System.Int32
  With New DataManagerFactory("SELECT  TRANSREQUIREMENTTYPE.REQUIREMENTTYPE ROWCOUNT, TRANSREQUIREMENTTYPE.LANGUAGEID FROM UNDERWRITING.TRANSREQUIREMENTTYPE TRANSREQUIREMENTTYPE  WHERE TRANSREQUIREMENTTYPE.REQUIREMENTTYPE = @:REQUIREMENTTYPE AND TRANSREQUIREMENTTYPE.LANGUAGEID = @:LANGUAGEID", "TransRequirementType", "Linked.Underwriting")
             .AddParameter("REQUIREMENTTYPE", DbType.Decimal, 5, False, e.NewValues("REQUIREMENTTYPE"))
.AddParameter("LANGUAGEID", DbType.Decimal, 5, False, e.NewValues("LANGUAGEID"))
 
             rowCountKey = .QueryExecuteScalarToInteger()  
        End With
        If rowCountKey > 0 Then
                errorMessage += String.Format(CultureInfo.InvariantCulture, "</ol><ul style='font-weight:bold'>{0}</ul>", 
                                                                            GetLocalResourceObject("TransRequirementType_GridMessageErrorGeneralValidator0Resource").ToString)                
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