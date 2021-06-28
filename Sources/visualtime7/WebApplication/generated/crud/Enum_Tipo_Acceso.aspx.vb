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

Partial Class Maintenance_Enum_Tipo_Acceso
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

            Enum_Tipo_Acceso_Grid.DataBind()
        End If      
    End Sub

#End Region

#Region "Controls Events"

         
    
 

#End Region

#Region "Enum_Tipo_Acceso_Grid Events"
    
    Protected Sub Enum_Tipo_Acceso_Grid_CustomColumnDisplayText(sender As Object, e As DevExpress.Web.ASPxGridView.ASPxGridViewColumnDisplayTextEventArgs) Handles Enum_Tipo_Acceso_Grid.CustomColumnDisplayText
          Dim data As DataTable
          Dim rows() As DataRow
           
          Select Case e.Column.FieldName

               Case Else              
           End Select
    End Sub
  
    Protected Sub Enum_Tipo_Acceso_Grid_DataBinding(ByVal sender As Object, ByVal e As EventArgs) Handles Enum_Tipo_Acceso_Grid.DataBinding
        If (Not IsNothing(Request.Params("__CALLBACKID")) AndAlso Request.Params("__CALLBACKID").EndsWith("Enum_Tipo_Acceso_Grid")) Or _internalCall Then
           

                 With New DataManagerFactory("SELECT  ENUM_TIPO_ACCESO.ID_TIPO_ACCESO, ENUM_TIPO_ACCESO.ESTADO_REGISTRO, ETRANS_TIPO_ACCESO.ID_LENGUAJE, ETRANS_TIPO_ACCESO.DESCRIPCION, ETRANS_TIPO_ACCESO.DESCRIPCION_CORTA FROM SEGURIDAD.ENUM_TIPO_ACCESO ENUM_TIPO_ACCESO JOIN SEGURIDAD.ETRANS_TIPO_ACCESO ETRANS_TIPO_ACCESO ON ETRANS_TIPO_ACCESO.ID_TIPO_ACCESO = ENUM_TIPO_ACCESO.ID_TIPO_ACCESO   ORDER BY Enum_Tipo_Acceso.Id_Tipo_Acceso ASC", "Enum_Tipo_Acceso", "Linked.Seguridad")                 
                                                   
                                  
                      Enum_Tipo_Acceso_Grid.DataSource = .QueryExecuteToTable(True)
                 End With 
        End If     
    End Sub

    Protected Sub Enum_Tipo_Acceso_Grid_CellEditorInitialize(sender As Object, e As ASPxGridViewEditorEventArgs) Handles Enum_Tipo_Acceso_Grid.CellEditorInitialize
        If Enum_Tipo_Acceso_Grid.IsNewRowEditing Then
            Select Case e.Column.FieldName
                              
                
                Case "ID_TIPO_ACCESO"                     
                       e.Editor.Focus()               
            End Select

        Else

            Select Case e.Column.FieldName
                Case "ID_TIPO_ACCESO"
                     e.Editor.Enabled = True
                     e.Editor.ClientEnabled = False
                   
                
                
                Case "ESTADO_REGISTRO"                     
                     e.Editor.Focus()
            End Select
        End If
        
       Select Case e.Column.FieldName
           Case "ID_TIPO_ACCESO"
                 
                 
           
        End Select
    End Sub

    Protected Sub Enum_Tipo_Acceso_Grid_RowInserting(ByVal sender As Object, ByVal e As ASPxDataInsertingEventArgs) Handles Enum_Tipo_Acceso_Grid.RowInserting 
        Dim isNullResult As Boolean = True
        
        
               
        e.Cancel = True
        Enum_Tipo_Acceso_Grid.CancelEdit()
    End Sub

    Protected Sub Enum_Tipo_Acceso_Grid_RowUpdating(ByVal sender As Object, ByVal e As ASPxDataUpdatingEventArgs) Handles Enum_Tipo_Acceso_Grid.RowUpdating
        Dim isNullResult As Boolean = True
          
                 
      
         e.Cancel = True
        Enum_Tipo_Acceso_Grid.CancelEdit()
    End Sub
    
    Protected Sub Enum_Tipo_Acceso_Grid_CustomCallback(sender As Object, e As ASPxGridViewCustomCallbackEventArgs) Handles Enum_Tipo_Acceso_Grid.CustomCallback     
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
    
    Protected Sub Enum_Tipo_Acceso_Grid_RowValidating(sender As Object, e As ASPxDataValidationEventArgs) Handles Enum_Tipo_Acceso_Grid.RowValidating
        Dim errorMessage As String = "<ol style='font-weight:lighter'>"
        
        If IsNothing(e.NewValues("ID_TIPO_ACCESO")) OrElse e.NewValues("ID_TIPO_ACCESO") = 0  
   e.Errors(Enum_Tipo_Acceso_Grid.Columns("ID_TIPO_ACCESO")) = GetLocalResourceObject("ID_TIPO_ACCESOMessageErrorRequired1Resource").ToString
End If
 
        
        If e.IsNewRow Then
           Dim rowCountKey As System.Int32
  With New DataManagerFactory("SELECT  ENUM_TIPO_ACCESO.ID_TIPO_ACCESO ROWCOUNT FROM SEGURIDAD.ENUM_TIPO_ACCESO ENUM_TIPO_ACCESO  WHERE ENUM_TIPO_ACCESO.ID_TIPO_ACCESO = @:ID_TIPO_ACCESO", "Enum_Tipo_Acceso", "Linked.Seguridad")
             .AddParameter("ID_TIPO_ACCESO", DbType.Decimal, 10, False, e.NewValues("ID_TIPO_ACCESO"))
 
             rowCountKey = .QueryExecuteScalarToInteger()  
        End With
        If rowCountKey > 0 Then
                errorMessage += String.Format(CultureInfo.InvariantCulture, "</ol><ul style='font-weight:bold'>{0}</ul>", 
                                                                            GetLocalResourceObject("Enum_Tipo_Acceso_GridMessageErrorGeneralValidator0Resource").ToString)                
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
 
#Region "ETrans_Tipo_Acceso_Grid Events"
    
    Protected Sub ETrans_Tipo_Acceso_Grid_CustomColumnDisplayText(sender As Object, e As DevExpress.Web.ASPxGridView.ASPxGridViewColumnDisplayTextEventArgs) Handles ETrans_Tipo_Acceso_Grid.CustomColumnDisplayText
          Dim data As DataTable
          Dim rows() As DataRow
           
          Select Case e.Column.FieldName

               Case Else              
           End Select
    End Sub
  
    Protected Sub ETrans_Tipo_Acceso_Grid_DataBinding(ByVal sender As Object, ByVal e As EventArgs) Handles ETrans_Tipo_Acceso_Grid.DataBinding
        If (Not IsNothing(Request.Params("__CALLBACKID")) AndAlso Request.Params("__CALLBACKID").EndsWith("ETrans_Tipo_Acceso_Grid")) Or _internalCall Then
           

                 With New DataManagerFactory("SELECT  ENUM_TIPO_ACCESO.ID_TIPO_ACCESO, ENUM_TIPO_ACCESO.ESTADO_REGISTRO, ETRANS_TIPO_ACCESO.ID_LENGUAJE, ETRANS_TIPO_ACCESO.DESCRIPCION, ETRANS_TIPO_ACCESO.DESCRIPCION_CORTA FROM SEGURIDAD.ENUM_TIPO_ACCESO ENUM_TIPO_ACCESO JOIN SEGURIDAD.ETRANS_TIPO_ACCESO ETRANS_TIPO_ACCESO ON ETRANS_TIPO_ACCESO.ID_TIPO_ACCESO = ENUM_TIPO_ACCESO.ID_TIPO_ACCESO   ORDER BY Enum_Tipo_Acceso.Id_Tipo_Acceso ASC", "Enum_Tipo_Acceso", "Linked.Seguridad")                 
                                                   
                                  
                      ETrans_Tipo_Acceso_Grid.DataSource = .QueryExecuteToTable(True)
                 End With 
        End If     
    End Sub

    Protected Sub ETrans_Tipo_Acceso_Grid_CellEditorInitialize(sender As Object, e As ASPxGridViewEditorEventArgs) Handles ETrans_Tipo_Acceso_Grid.CellEditorInitialize
        If ETrans_Tipo_Acceso_Grid.IsNewRowEditing Then
            Select Case e.Column.FieldName
                              
                
                Case "ID_TIPO_ACCESO"                     
                       e.Editor.Focus()               
            End Select

        Else

            Select Case e.Column.FieldName
                Case "ID_TIPO_ACCESO"
                     e.Editor.Enabled = True
                     e.Editor.ClientEnabled = False
                   
                
                
                Case "ESTADO_REGISTRO"                     
                     e.Editor.Focus()
            End Select
        End If
        
       Select Case e.Column.FieldName
           Case "ID_TIPO_ACCESO"
                 
                 
           
        End Select
    End Sub

    Protected Sub ETrans_Tipo_Acceso_Grid_RowInserting(ByVal sender As Object, ByVal e As ASPxDataInsertingEventArgs) Handles ETrans_Tipo_Acceso_Grid.RowInserting 
        Dim isNullResult As Boolean = True
        
        
               
        e.Cancel = True
        ETrans_Tipo_Acceso_Grid.CancelEdit()
    End Sub

    Protected Sub ETrans_Tipo_Acceso_Grid_RowUpdating(ByVal sender As Object, ByVal e As ASPxDataUpdatingEventArgs) Handles ETrans_Tipo_Acceso_Grid.RowUpdating
        Dim isNullResult As Boolean = True
          
                 
      
         e.Cancel = True
        ETrans_Tipo_Acceso_Grid.CancelEdit()
    End Sub
    
    Protected Sub ETrans_Tipo_Acceso_Grid_CustomCallback(sender As Object, e As ASPxGridViewCustomCallbackEventArgs) Handles ETrans_Tipo_Acceso_Grid.CustomCallback     
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
    
    Protected Sub ETrans_Tipo_Acceso_Grid_RowValidating(sender As Object, e As ASPxDataValidationEventArgs) Handles ETrans_Tipo_Acceso_Grid.RowValidating
        Dim errorMessage As String = "<ol style='font-weight:lighter'>"
        
        If IsNothing(e.NewValues("ID_TIPO_ACCESO")) OrElse e.NewValues("ID_TIPO_ACCESO") = 0  
   e.Errors(ETrans_Tipo_Acceso_Grid.Columns("ID_TIPO_ACCESO")) = GetLocalResourceObject("ID_TIPO_ACCESOMessageErrorRequired1Resource").ToString
End If
 
        
        If e.IsNewRow Then
           Dim rowCountKey As System.Int32
  With New DataManagerFactory("SELECT  ETRANS_TIPO_ACCESO.ID_TIPO_ACCESO ROWCOUNT, ETRANS_TIPO_ACCESO.ID_LENGUAJE FROM SEGURIDAD.ETRANS_TIPO_ACCESO ETRANS_TIPO_ACCESO  WHERE ETRANS_TIPO_ACCESO.ID_TIPO_ACCESO = @:ID_TIPO_ACCESO AND ETRANS_TIPO_ACCESO.ID_LENGUAJE = @:ID_LENGUAJE", "ETrans_Tipo_Acceso", "Linked.Seguridad")
             .AddParameter("ID_TIPO_ACCESO", DbType.Decimal, 10, False, e.NewValues("ID_TIPO_ACCESO"))
.AddParameter("ID_LENGUAJE", DbType.Decimal, 5, False, e.NewValues("ID_LENGUAJE"))
 
             rowCountKey = .QueryExecuteScalarToInteger()  
        End With
        If rowCountKey > 0 Then
                errorMessage += String.Format(CultureInfo.InvariantCulture, "</ol><ul style='font-weight:bold'>{0}</ul>", 
                                                                            GetLocalResourceObject("ETrans_Tipo_Acceso_GridMessageErrorGeneralValidator0Resource").ToString)                
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