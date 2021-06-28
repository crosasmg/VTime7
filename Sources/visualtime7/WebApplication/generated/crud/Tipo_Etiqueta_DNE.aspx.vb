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
Imports System.Web.Services

#End Region

Partial Class Maintenance_Tipo_Etiqueta_DNE
    Inherits PageBase

#Region "Private fields"

    Private _internalCall As Boolean

#End Region

#Region "Web Methods Dependency"

    
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
                    .Target = languageItem.Key
                     
                    If languageItem.Key = LanguageId Then
                         MainMenu.Items.FindByName("LanguageItem").Text = String.Format(CultureInfo.InvariantCulture, "{0} {1}", GetLocalResourceObject("LanguageItemMenu").ToString(), languageItem.Value)
                         MainMenu.Items.FindByName("LanguageItem").Image.Url = String.Format(CultureInfo.InvariantCulture, "/images/16x16/Flags/{0}.png", languageItem.Key)

                        .Visible = False
                    Else
                        .Visible = True
                    End If
                End With

                MainMenu.Items.FindByName("LanguageItem").Items.Add(newItem)
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
            e.Item.Parent.Image.Url = String.Format(CultureInfo.InvariantCulture, "/images/16x16/Flags/{0}.png", e.Item.Target)

            e.Item.Visible = False

            For Each item As DevExpress.Web.ASPxMenu.MenuItem In e.Item.Parent.Items
                If Not String.Equals(item.Text, e.Item.Text, StringComparison.CurrentCultureIgnoreCase) Then
                    item.Visible = True
                End If
            Next

            CurrentState.Set("LanguageId", DescriptionToEnumLanguage(e.Item.Text, LanguageId))
            _internalCall = True

            TAB_TIPO_ETIQUETA_Grid.DataBind()
        End If      
    End Sub

#End Region

#Region "Controls Events"

         
    
 

#End Region

#Region "TAB_TIPO_ETIQUETA_Grid Events"
    
    Protected Sub TAB_TIPO_ETIQUETA_Grid_CustomColumnDisplayText(sender As Object, e As DevExpress.Web.ASPxGridView.ASPxGridViewColumnDisplayTextEventArgs) Handles TAB_TIPO_ETIQUETA_Grid.CustomColumnDisplayText
          Dim data As DataTable
          Dim rows() As DataRow
           
          Select Case e.Column.FieldName

               Case Else              
           End Select
    End Sub
  
    Protected Sub TAB_TIPO_ETIQUETA_Grid_DataBinding(ByVal sender As Object, ByVal e As EventArgs) Handles TAB_TIPO_ETIQUETA_Grid.DataBinding
        If (Not IsNothing(Request.Params("__CALLBACKID")) AndAlso Request.Params("__CALLBACKID").EndsWith("TAB_TIPO_ETIQUETA_Grid")) Or _internalCall Then
                       If Caching.Exist("EnumRecordStatus") Then
                DirectCast(TAB_TIPO_ETIQUETA_Grid.Columns("ESTADO_REGISTRO"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = Caching.GetItem("EnumRecordStatus")

            Else
                Dim source As DataTable = Nothing

                With New DataManagerFactory("SELECT  ENUMRECORDSTATUS.RECORDSTATUS, ETRANRECORDSTATUS.LANGUAGEID, ETRANRECORDSTATUS.DESCRIPTION FROM COMMON.ENUMRECORDSTATUS ENUMRECORDSTATUS JOIN ETRANRECORDSTATUS ETRANRECORDSTATUS ON ETRANRECORDSTATUS.RECORDSTATUS = ENUMRECORDSTATUS.RECORDSTATUS  WHERE ETRANRECORDSTATUS.LANGUAGEID = @:LANGUAGEID  ORDER BY ETranRecordStatus.Description ASC", "EnumRecordStatus", "Linked.Common")
                    .AddParameter("LANGUAGEID", DbType.Decimal, 5, False, CurrentState.Get("LanguageId"))
 
                    source = .QueryExecuteToTable(True)
                    DirectCast(TAB_TIPO_ETIQUETA_Grid.Columns("ESTADO_REGISTRO"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = source
                End With

                If Not IsNothing(source) AndAlso source.Rows.Count > 0 Then
                    Caching.SetItem("EnumRecordStatus", source)
                End If
            End If 
 

                 With New DataManagerFactory("SELECT  TAB_TIPO_ETIQUETA.ID_TIPO_ETIQUETA, TAB_TIPO_ETIQUETA.ESTADO_REGISTRO, TRANS_TIPO_ETIQUETA.ID_LENGUAJE, TRANS_TIPO_ETIQUETA.DESCRIPCION_CORTA, TRANS_TIPO_ETIQUETA.DESCRIPCION_LARGA FROM DNE.TAB_TIPO_ETIQUETA TAB_TIPO_ETIQUETA JOIN DNE.TRANS_TIPO_ETIQUETA TRANS_TIPO_ETIQUETA ON TRANS_TIPO_ETIQUETA.ID_TIPO_ETIQUETA = TAB_TIPO_ETIQUETA.ID_TIPO_ETIQUETA   ORDER BY TAB_TIPO_ETIQUETA.ID_TIPO_ETIQUETA ASC", "TAB_TIPO_ETIQUETA", "Linked.DNE")                 
                                                   
                                  
                      TAB_TIPO_ETIQUETA_Grid.DataSource = .QueryExecuteToTable(True)
                 End With 
        End If     
    End Sub

    Protected Sub TAB_TIPO_ETIQUETA_Grid_CellEditorInitialize(sender As Object, e As ASPxGridViewEditorEventArgs) Handles TAB_TIPO_ETIQUETA_Grid.CellEditorInitialize
        If TAB_TIPO_ETIQUETA_Grid.IsNewRowEditing Then
            Select Case e.Column.FieldName
                              
                
                Case "ID_TIPO_ETIQUETA"                     
                       e.Editor.Focus()               
            End Select

        Else

            Select Case e.Column.FieldName
                Case "ID_TIPO_ETIQUETA"
                     e.Editor.Enabled = True
                     e.Editor.ClientEnabled = False
                   
                
                
                Case "ESTADO_REGISTRO"                     
                     e.Editor.Focus()
            End Select
        End If
        
       Select Case e.Column.FieldName
           Case "ID_TIPO_ETIQUETA"
                 
                 
           Case "ESTADO_REGISTRO"
     DirectCast(e.Editor, ASPxComboBox).DataBindItems() 

        End Select
    End Sub

    Protected Sub TAB_TIPO_ETIQUETA_Grid_RowInserting(ByVal sender As Object, ByVal e As ASPxDataInsertingEventArgs) Handles TAB_TIPO_ETIQUETA_Grid.RowInserting 
        Dim isNullResult As Boolean = True
        
             With New DataManagerFactory("INSERT INTO DNE.TAB_TIPO_ETIQUETA (ID_TIPO_ETIQUETA, ESTADO_REGISTRO, CREATIONDATE, CREATORUSERCODE, UPDATEDATE, UPDATEUSERCODE) VALUES (@:ID_TIPO_ETIQUETA, @:ESTADO_REGISTRO, SYSDATE, @:CREATORUSERCODE, SYSDATE, @:UPDATEUSERCODE)", "TAB_TIPO_ETIQUETA", "Linked.DNE")                 
                                                   
                       .AddParameter("ID_TIPO_ETIQUETA", DbType.Decimal, 0, False, e.NewValues("ID_TIPO_ETIQUETA"))
.AddParameter("ESTADO_REGISTRO", DbType.AnsiString, 0, False, e.NewValues("ESTADO_REGISTRO"))
.AddParameter("CREATORUSERCODE", DbType.Decimal, 0, False, Session("nUserCode"))
.AddParameter("UPDATEUSERCODE", DbType.Decimal, 0, False, Session("nUserCode"))
            
                       .CommandExecute()
                End With
     With New DataManagerFactory("INSERT INTO DNE.TRANS_TIPO_ETIQUETA (ID_TIPO_ETIQUETA, ID_LENGUAJE, DESCRIPCION_CORTA, DESCRIPCION_LARGA, CREATIONDATE, CREATORUSERCODE, UPDATEDATE, UPDATEUSERCODE) VALUES (@:ID_TIPO_ETIQUETA, @:ID_LENGUAJE, @:DESCRIPCION_CORTA, @:DESCRIPCION_LARGA, SYSDATE, @:CREATORUSERCODE, SYSDATE, @:UPDATEUSERCODE)", "TRANS_TIPO_ETIQUETA", "Linked.DNE")                 
                                                   
                       .AddParameter("ID_TIPO_ETIQUETA", DbType.Decimal, 0, False, e.NewValues("ID_TIPO_ETIQUETA"))
.AddParameter("ID_LENGUAJE", DbType.Decimal, 0, False, e.NewValues("ID_LENGUAJE"))
.AddParameter("DESCRIPCION_CORTA", DbType.AnsiString, 0, (e.NewValues("DESCRIPCION_CORTA") = String.Empty), e.NewValues("DESCRIPCION_CORTA"))
.AddParameter("DESCRIPCION_LARGA", DbType.AnsiString, 0, (e.NewValues("DESCRIPCION_LARGA") = String.Empty), e.NewValues("DESCRIPCION_LARGA"))
.AddParameter("CREATORUSERCODE", DbType.Decimal, 0, False, Session("nUserCode"))
.AddParameter("UPDATEUSERCODE", DbType.Decimal, 0, False, Session("nUserCode"))
            
                       .CommandExecute()
                End With
               
        e.Cancel = True
        TAB_TIPO_ETIQUETA_Grid.CancelEdit()
    End Sub

    Protected Sub TAB_TIPO_ETIQUETA_Grid_RowUpdating(ByVal sender As Object, ByVal e As ASPxDataUpdatingEventArgs) Handles TAB_TIPO_ETIQUETA_Grid.RowUpdating
        Dim isNullResult As Boolean = True
          
             With New DataManagerFactory("UPDATE DNE.TAB_TIPO_ETIQUETA SET ESTADO_REGISTRO = @:ESTADO_REGISTRO, UPDATEDATE = SYSDATE, UPDATEUSERCODE = @:UPDATEUSERCODE WHERE ID_TIPO_ETIQUETA = @:ID_TIPO_ETIQUETA", "TAB_TIPO_ETIQUETA", "Linked.DNE")                 
                                                   
                       .AddParameter("ESTADO_REGISTRO", DbType.AnsiString, 0, False, e.NewValues("ESTADO_REGISTRO"))
.AddParameter("UPDATEUSERCODE", DbType.Decimal, 0, False, Session("nUserCode"))
.AddParameter("ID_TIPO_ETIQUETA", DbType.Decimal, 0, False, e.Keys("ID_TIPO_ETIQUETA"))
            
                       .CommandExecute()
                End With
     With New DataManagerFactory("UPDATE DNE.TRANS_TIPO_ETIQUETA SET DESCRIPCION_CORTA = @:DESCRIPCION_CORTA, DESCRIPCION_LARGA = @:DESCRIPCION_LARGA, UPDATEDATE = SYSDATE, UPDATEUSERCODE = @:UPDATEUSERCODE WHERE ID_TIPO_ETIQUETA = @:ID_TIPO_ETIQUETA", "TRANS_TIPO_ETIQUETA", "Linked.DNE")                 
                                                   
                       .AddParameter("DESCRIPCION_CORTA", DbType.AnsiString, 0, (e.NewValues("DESCRIPCION_CORTA") = String.Empty), e.NewValues("DESCRIPCION_CORTA"))
.AddParameter("DESCRIPCION_LARGA", DbType.AnsiString, 0, (e.NewValues("DESCRIPCION_LARGA") = String.Empty), e.NewValues("DESCRIPCION_LARGA"))
.AddParameter("UPDATEUSERCODE", DbType.Decimal, 0, False, Session("nUserCode"))
.AddParameter("ID_TIPO_ETIQUETA", DbType.Decimal, 0, False, e.Keys("ID_TIPO_ETIQUETA"))
            
                       .CommandExecute()
                End With         
      
         e.Cancel = True
        TAB_TIPO_ETIQUETA_Grid.CancelEdit()
    End Sub
    
    Protected Sub TAB_TIPO_ETIQUETA_Grid_CustomCallback(sender As Object, e As ASPxGridViewCustomCallbackEventArgs) Handles TAB_TIPO_ETIQUETA_Grid.CustomCallback     
           Dim isNullResult As Boolean = True
           
           Select Case e.Parameters.ToString.ToLower
               Case "delete"
                       Dim ID_TIPO_ETIQUETAKey As Generic.List(Of Object) = TAB_TIPO_ETIQUETA_Grid.GetSelectedFieldValues("ID_TIPO_ETIQUETA")
        
               For index As Integer = 0 To ID_TIPO_ETIQUETAKey.Count - 1
                  With New DataManagerFactory("DELETE FROM DNE.TRANS_TIPO_ETIQUETA WHERE ID_TIPO_ETIQUETA = @:ID_TIPO_ETIQUETA ", "TRANS_TIPO_ETIQUETA", "Linked.DNE")                 
                                                   
               .AddParameter("ID_TIPO_ETIQUETA", DbType.Decimal, 0, False, ID_TIPO_ETIQUETAKey(index))
            
               .CommandExecute()
          End With 
 With New DataManagerFactory("DELETE FROM DNE.TAB_TIPO_ETIQUETA WHERE ID_TIPO_ETIQUETA = @:ID_TIPO_ETIQUETA ", "TAB_TIPO_ETIQUETA", "Linked.DNE")                 
                                                   
               .AddParameter("ID_TIPO_ETIQUETA", DbType.Decimal, 0, False, ID_TIPO_ETIQUETAKey(index))
            
               .CommandExecute()
          End With 
                       
              Next           
           
              TAB_TIPO_ETIQUETA_Grid.DataBind()
                 
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
    
    Protected Sub TAB_TIPO_ETIQUETA_Grid_RowValidating(sender As Object, e As ASPxDataValidationEventArgs) Handles TAB_TIPO_ETIQUETA_Grid.RowValidating
        Dim errorMessage As String = "<ol style='font-weight:lighter'>"
        
        If IsNothing(e.NewValues("ID_TIPO_ETIQUETA")) OrElse e.NewValues("ID_TIPO_ETIQUETA") = 0  
   e.Errors(TAB_TIPO_ETIQUETA_Grid.Columns("ID_TIPO_ETIQUETA")) = GetLocalResourceObject("ID_TIPO_ETIQUETAMessageErrorRequired1Resource").ToString
End If
 
        
        If e.IsNewRow Then
           Dim rowCountKey As System.Int32
  With New DataManagerFactory("SELECT  TAB_TIPO_ETIQUETA.ID_TIPO_ETIQUETA ROWCOUNT FROM DNE.TAB_TIPO_ETIQUETA TAB_TIPO_ETIQUETA  WHERE TAB_TIPO_ETIQUETA.ID_TIPO_ETIQUETA = @:ID_TIPO_ETIQUETA", "TAB_TIPO_ETIQUETA", "Linked.DNE")
             .AddParameter("ID_TIPO_ETIQUETA", DbType.Decimal, 5, False, e.NewValues("ID_TIPO_ETIQUETA"))
 
             rowCountKey = .QueryExecuteScalarToInteger()  
        End With
        If rowCountKey > 0 Then
                errorMessage += String.Format(CultureInfo.InvariantCulture, "</ol><ul style='font-weight:bold'>{0}</ul>", 
                                                                            GetLocalResourceObject("TAB_TIPO_ETIQUETA_GridMessageErrorGeneralValidator0Resource").ToString)                
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
 
#Region "TRANS_TIPO_ETIQUETA_Grid Events"
    
    Protected Sub TRANS_TIPO_ETIQUETA_Grid_CustomColumnDisplayText(sender As Object, e As DevExpress.Web.ASPxGridView.ASPxGridViewColumnDisplayTextEventArgs) Handles TRANS_TIPO_ETIQUETA_Grid.CustomColumnDisplayText
          Dim data As DataTable
          Dim rows() As DataRow
           
          Select Case e.Column.FieldName

               Case Else              
           End Select
    End Sub
  
    Protected Sub TRANS_TIPO_ETIQUETA_Grid_DataBinding(ByVal sender As Object, ByVal e As EventArgs) Handles TRANS_TIPO_ETIQUETA_Grid.DataBinding
        If (Not IsNothing(Request.Params("__CALLBACKID")) AndAlso Request.Params("__CALLBACKID").EndsWith("TRANS_TIPO_ETIQUETA_Grid")) Or _internalCall Then
                       If Caching.Exist("EnumRecordStatus") Then
                DirectCast(TRANS_TIPO_ETIQUETA_Grid.Columns("ESTADO_REGISTRO"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = Caching.GetItem("EnumRecordStatus")

            Else
                Dim source As DataTable = Nothing

                With New DataManagerFactory("SELECT  ENUMRECORDSTATUS.RECORDSTATUS, ETRANRECORDSTATUS.LANGUAGEID, ETRANRECORDSTATUS.DESCRIPTION FROM COMMON.ENUMRECORDSTATUS ENUMRECORDSTATUS JOIN ETRANRECORDSTATUS ETRANRECORDSTATUS ON ETRANRECORDSTATUS.RECORDSTATUS = ENUMRECORDSTATUS.RECORDSTATUS  WHERE ETRANRECORDSTATUS.LANGUAGEID = @:LANGUAGEID  ORDER BY ETranRecordStatus.Description ASC", "EnumRecordStatus", "Linked.Common")
                    .AddParameter("LANGUAGEID", DbType.Decimal, 5, False, CurrentState.Get("LanguageId"))
 
                    source = .QueryExecuteToTable(True)
                    DirectCast(TRANS_TIPO_ETIQUETA_Grid.Columns("ESTADO_REGISTRO"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = source
                End With

                If Not IsNothing(source) AndAlso source.Rows.Count > 0 Then
                    Caching.SetItem("EnumRecordStatus", source)
                End If
            End If 
             If Caching.Exist("TabLanguage") Then
                DirectCast(TRANS_TIPO_ETIQUETA_Grid.Columns("ID_LENGUAJE"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = Caching.GetItem("TabLanguage")

            Else
                Dim source As DataTable = Nothing

                With New DataManagerFactory("SELECT  TABLANGUAGE.LANGUAGEID, TABLANGUAGE.RECORDSTATUS, TRANSLANGUAGE.LANGUAGECODEID, TRANSLANGUAGE.DESCRIPTION FROM COMMON.TABLANGUAGE TABLANGUAGE JOIN TRANSLANGUAGE TRANSLANGUAGE ON TRANSLANGUAGE.LANGUAGECODEID = TABLANGUAGE.LANGUAGEID  WHERE TABLANGUAGE.RECORDSTATUS = '1' AND TRANSLANGUAGE.LANGUAGECODEID = @:LANGUAGECODEID  ORDER BY TransLanguage.Description ASC", "TabLanguage", "Linked.Common")
                    .AddParameter("LANGUAGECODEID", DbType.Decimal, 5, False, CurrentState.Get("LanguageId"))
 
                    source = .QueryExecuteToTable(True)
                    DirectCast(TRANS_TIPO_ETIQUETA_Grid.Columns("ID_LENGUAJE"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = source
                End With

                If Not IsNothing(source) AndAlso source.Rows.Count > 0 Then
                    Caching.SetItem("TabLanguage", source)
                End If
            End If 
 

                 With New DataManagerFactory("SELECT  TAB_TIPO_ETIQUETA.ID_TIPO_ETIQUETA, TAB_TIPO_ETIQUETA.ESTADO_REGISTRO, TRANS_TIPO_ETIQUETA.ID_LENGUAJE, TRANS_TIPO_ETIQUETA.DESCRIPCION_CORTA, TRANS_TIPO_ETIQUETA.DESCRIPCION_LARGA FROM DNE.TAB_TIPO_ETIQUETA TAB_TIPO_ETIQUETA JOIN DNE.TRANS_TIPO_ETIQUETA TRANS_TIPO_ETIQUETA ON TRANS_TIPO_ETIQUETA.ID_TIPO_ETIQUETA = TAB_TIPO_ETIQUETA.ID_TIPO_ETIQUETA   ORDER BY TAB_TIPO_ETIQUETA.ID_TIPO_ETIQUETA ASC", "TAB_TIPO_ETIQUETA", "Linked.DNE")                 
                                                   
                                  
                      TRANS_TIPO_ETIQUETA_Grid.DataSource = .QueryExecuteToTable(True)
                 End With 
        End If     
    End Sub

    Protected Sub TRANS_TIPO_ETIQUETA_Grid_CellEditorInitialize(sender As Object, e As ASPxGridViewEditorEventArgs) Handles TRANS_TIPO_ETIQUETA_Grid.CellEditorInitialize
        If TRANS_TIPO_ETIQUETA_Grid.IsNewRowEditing Then
            Select Case e.Column.FieldName
                              
                
                Case "ID_TIPO_ETIQUETA"                     
                       e.Editor.Focus()               
            End Select

        Else

            Select Case e.Column.FieldName
                Case "ID_TIPO_ETIQUETA"
                     e.Editor.Enabled = True
                     e.Editor.ClientEnabled = False
Case "ID_LENGUAJE"
                     e.Editor.Enabled = True
                     e.Editor.ClientEnabled = False
                   
                
                
                Case "ESTADO_REGISTRO"                     
                     e.Editor.Focus()
            End Select
        End If
        
       Select Case e.Column.FieldName
           Case "ID_TIPO_ETIQUETA"
                 
                 
           Case "ESTADO_REGISTRO"
     DirectCast(e.Editor, ASPxComboBox).DataBindItems() 
Case "ID_LENGUAJE"
     DirectCast(e.Editor, ASPxComboBox).DataBindItems() 

        End Select
    End Sub

    Protected Sub TRANS_TIPO_ETIQUETA_Grid_RowInserting(ByVal sender As Object, ByVal e As ASPxDataInsertingEventArgs) Handles TRANS_TIPO_ETIQUETA_Grid.RowInserting 
        Dim isNullResult As Boolean = True
        
        
               
        e.Cancel = True
        TRANS_TIPO_ETIQUETA_Grid.CancelEdit()
    End Sub

    Protected Sub TRANS_TIPO_ETIQUETA_Grid_RowUpdating(ByVal sender As Object, ByVal e As ASPxDataUpdatingEventArgs) Handles TRANS_TIPO_ETIQUETA_Grid.RowUpdating
        Dim isNullResult As Boolean = True
          
             With New DataManagerFactory("UPDATE DNE.TRANS_TIPO_ETIQUETA SET DESCRIPCION_CORTA = @:DESCRIPCION_CORTA, DESCRIPCION_LARGA = @:DESCRIPCION_LARGA, UPDATEDATE = SYSDATE, UPDATEUSERCODE = @:UPDATEUSERCODE WHERE ID_TIPO_ETIQUETA = @:ID_TIPO_ETIQUETA AND ID_LENGUAJE = @:ID_LENGUAJE", "TRANS_TIPO_ETIQUETA", "Linked.DNE")                 
                                                   
                       .AddParameter("DESCRIPCION_CORTA", DbType.AnsiString, 0, (e.NewValues("DESCRIPCION_CORTA") = String.Empty), e.NewValues("DESCRIPCION_CORTA"))
.AddParameter("DESCRIPCION_LARGA", DbType.AnsiString, 0, (e.NewValues("DESCRIPCION_LARGA") = String.Empty), e.NewValues("DESCRIPCION_LARGA"))
.AddParameter("UPDATEUSERCODE", DbType.Decimal, 0, False, Session("nUserCode"))
.AddParameter("ID_TIPO_ETIQUETA", DbType.Decimal, 0, False, e.Keys("ID_TIPO_ETIQUETA_T"))
.AddParameter("ID_LENGUAJE", DbType.Decimal, 0, False, e.Keys("ID_LENGUAJE_T"))
            
                       .CommandExecute()
                End With         
      
         e.Cancel = True
        TRANS_TIPO_ETIQUETA_Grid.CancelEdit()
    End Sub
    
    Protected Sub TRANS_TIPO_ETIQUETA_Grid_CustomCallback(sender As Object, e As ASPxGridViewCustomCallbackEventArgs) Handles TRANS_TIPO_ETIQUETA_Grid.CustomCallback     
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
    
    Protected Sub TRANS_TIPO_ETIQUETA_Grid_RowValidating(sender As Object, e As ASPxDataValidationEventArgs) Handles TRANS_TIPO_ETIQUETA_Grid.RowValidating
        Dim errorMessage As String = "<ol style='font-weight:lighter'>"
        
        If IsNothing(e.NewValues("ID_TIPO_ETIQUETA")) OrElse e.NewValues("ID_TIPO_ETIQUETA") = 0  
   e.Errors(TRANS_TIPO_ETIQUETA_Grid.Columns("ID_TIPO_ETIQUETA")) = GetLocalResourceObject("ID_TIPO_ETIQUETAMessageErrorRequired1Resource").ToString
End If
 
        
        If e.IsNewRow Then
           Dim rowCountKey As System.Int32
  With New DataManagerFactory("SELECT  TRANS_TIPO_ETIQUETA.ID_TIPO_ETIQUETA ROWCOUNT, TRANS_TIPO_ETIQUETA.ID_LENGUAJE FROM DNE.TRANS_TIPO_ETIQUETA TRANS_TIPO_ETIQUETA  WHERE TRANS_TIPO_ETIQUETA.ID_TIPO_ETIQUETA = @:ID_TIPO_ETIQUETA AND TRANS_TIPO_ETIQUETA.ID_LENGUAJE = @:ID_LENGUAJE", "TRANS_TIPO_ETIQUETA", "Linked.DNE")
             .AddParameter("ID_TIPO_ETIQUETA", DbType.Decimal, 5, False, e.NewValues("ID_TIPO_ETIQUETA"))
.AddParameter("ID_LENGUAJE", DbType.Decimal, 10, False, e.NewValues("ID_LENGUAJE"))
 
             rowCountKey = .QueryExecuteScalarToInteger()  
        End With
        If rowCountKey > 0 Then
                errorMessage += String.Format(CultureInfo.InvariantCulture, "</ol><ul style='font-weight:bold'>{0}</ul>", 
                                                                            GetLocalResourceObject("TRANS_TIPO_ETIQUETA_GridMessageErrorGeneralValidator0Resource").ToString)                
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