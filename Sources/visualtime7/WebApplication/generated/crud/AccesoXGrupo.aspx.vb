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

Partial Class Maintenance_AccesoXGrupo
    Inherits PageBase

#Region "Private fields"

    Private _internalCall As Boolean

#End Region

#Region "Web Methods Dependency"

    
#End Region

#Region "Events Page"

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not IsCallback AndAlso Not IsPostBack Then
 

        End If
        
        If Not CurrentState.Contains("LanguageId") Then
            CurrentState.Add("LanguageId", LanguageId)
        End If
    End Sub

#End Region

#Region "MainMenu Events"

    Protected Sub MainMenu_ItemClick(source As Object, e As DevExpress.Web.ASPxMenu.MenuItemEventArgs) Handles MainMenu.ItemClick
      
    End Sub

#End Region

#Region "Controls Events"

         
    
 

#End Region

#Region "AccesoXGrupo Events"
    
    Protected Sub AccesoXGrupo_CustomColumnDisplayText(sender As Object, e As DevExpress.Web.ASPxGridView.ASPxGridViewColumnDisplayTextEventArgs) Handles AccesoXGrupo.CustomColumnDisplayText
          Dim data As DataTable
          Dim rows() As DataRow
           
          Select Case e.Column.FieldName

               Case Else              
           End Select
    End Sub
  
    Protected Sub AccesoXGrupo_DataBinding(ByVal sender As Object, ByVal e As EventArgs) Handles AccesoXGrupo.DataBinding
        If (Not IsNothing(Request.Params("__CALLBACKID")) AndAlso Request.Params("__CALLBACKID").EndsWith("AccesoXGrupo")) Or _internalCall Then
           With New DataManagerFactory("SELECT  ACCESO.ID_ACCESO, ACCESO.DESCRIPCION FROM SEGURIDAD.ACCESO ACCESO   ", "Acceso", "Linked.Seguridad")                 
                                                   
                  
               
                DirectCast(AccesoXGrupo.Columns("ID_ACCESO"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = .QueryExecuteToTable(True)
           End With 
 With New DataManagerFactory("SELECT  GRUPO_ACCESO.ID_GRUPO_ACCESO, GRUPO_ACCESO.DESCRIPCION FROM SEGURIDAD.GRUPO_ACCESO GRUPO_ACCESO   ", "Grupo_Acceso", "Linked.Seguridad")                 
                                                   
                  
               
                DirectCast(AccesoXGrupo.Columns("ID_GRUPO_ACCESO"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = .QueryExecuteToTable(True)
           End With 
 

                 With New DataManagerFactory("SELECT  ACCESOXGRUPO.ID_ACCESO, ACCESOXGRUPO.ID_GRUPO_ACCESO FROM SEGURIDAD.ACCESOXGRUPO ACCESOXGRUPO   ORDER BY AccesoXGrupo.Id_Acceso ASC, AccesoXGrupo.Id_Grupo_Acceso ASC", "AccesoXGrupo", "Linked.Seguridad")                 
                                                   
                                  
                      AccesoXGrupo.DataSource = .QueryExecuteToTable(True)
                 End With 
        End If     
    End Sub

    Protected Sub AccesoXGrupo_CellEditorInitialize(sender As Object, e As ASPxGridViewEditorEventArgs) Handles AccesoXGrupo.CellEditorInitialize
        If AccesoXGrupo.IsNewRowEditing Then
            Select Case e.Column.FieldName
                              
                
                Case "ID_ACCESO"                     
                       e.Editor.Focus()               
            End Select

        Else

            Select Case e.Column.FieldName
                Case "ID_ACCESO"
                     e.Editor.Enabled = True
                     e.Editor.ClientEnabled = False
Case "ID_GRUPO_ACCESO"
                     e.Editor.Enabled = True
                     e.Editor.ClientEnabled = False
                   
                
                
                Case ""                     
                     e.Editor.Focus()
            End Select
        End If
        
       Select Case e.Column.FieldName
           Case "ID_ACCESO"
                 DirectCast(e.Editor, ASPxComboBox).DataBindItems()
                 
           Case "ID_GRUPO_ACCESO"
     DirectCast(e.Editor, ASPxComboBox).DataBindItems() 

        End Select
    End Sub

    Protected Sub AccesoXGrupo_RowInserting(ByVal sender As Object, ByVal e As ASPxDataInsertingEventArgs) Handles AccesoXGrupo.RowInserting 
        Dim isNullResult As Boolean = True
        
             With New DataManagerFactory("INSERT INTO SEGURIDAD.AccesoXGrupo (ID_ACCESO, ID_GRUPO_ACCESO) VALUES (@:ID_ACCESO, @:ID_GRUPO_ACCESO)", "AccesoXGrupo", "Linked.Seguridad")                 
                                                   
                       .AddParameter("ID_ACCESO", DbType.Decimal, 0, False, e.NewValues("ID_ACCESO"))
.AddParameter("ID_GRUPO_ACCESO", DbType.Decimal, 0, False, e.NewValues("ID_GRUPO_ACCESO"))
            
                       .CommandExecute()
                End With
               
        e.Cancel = True
        AccesoXGrupo.CancelEdit()
    End Sub

    Protected Sub AccesoXGrupo_RowUpdating(ByVal sender As Object, ByVal e As ASPxDataUpdatingEventArgs) Handles AccesoXGrupo.RowUpdating
        Dim isNullResult As Boolean = True
          
             With New DataManagerFactory("UPDATE SEGURIDAD.AccesoXGrupo SET  WHERE ID_ACCESO = @:ID_ACCESO AND ID_GRUPO_ACCESO = @:ID_GRUPO_ACCESO", "AccesoXGrupo", "Linked.Seguridad")                 
                                                   
                       .AddParameter("ID_ACCESO", DbType.Decimal, 0, False, e.Keys("ID_ACCESO"))
.AddParameter("ID_GRUPO_ACCESO", DbType.Decimal, 0, False, e.Keys("ID_GRUPO_ACCESO"))
            
                       .CommandExecute()
                End With         
      
         e.Cancel = True
        AccesoXGrupo.CancelEdit()
    End Sub
    
    Protected Sub AccesoXGrupo_CustomCallback(sender As Object, e As ASPxGridViewCustomCallbackEventArgs) Handles AccesoXGrupo.CustomCallback     
           Dim isNullResult As Boolean = True
           
           Select Case e.Parameters.ToString.ToLower
               Case "delete"
                       Dim ID_ACCESOKey As Generic.List(Of Object) = AccesoXGrupo.GetSelectedFieldValues("ID_ACCESO")
 Dim ID_GRUPO_ACCESOKey As Generic.List(Of Object) = AccesoXGrupo.GetSelectedFieldValues("ID_GRUPO_ACCESO")
        
               For index As Integer = 0 To ID_ACCESOKey.Count - 1
                  With New DataManagerFactory("DELETE FROM SEGURIDAD.AccesoXGrupo WHERE ID_ACCESO = @:ID_ACCESO AND ID_GRUPO_ACCESO = @:ID_GRUPO_ACCESO ", "AccesoXGrupo", "Linked.Seguridad")                 
                                                   
               .AddParameter("ID_ACCESO", DbType.Decimal, 0, False, ID_ACCESOKey(index))
.AddParameter("ID_GRUPO_ACCESO", DbType.Decimal, 0, False, ID_GRUPO_ACCESOKey(index))
            
               .CommandExecute()
          End With 
                       
              Next           
           
              AccesoXGrupo.DataBind()
                 
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
    
    Protected Sub AccesoXGrupo_RowValidating(sender As Object, e As ASPxDataValidationEventArgs) Handles AccesoXGrupo.RowValidating
        Dim errorMessage As String = "<ol style='font-weight:lighter'>"
        
        
        
        If e.IsNewRow Then
           Dim rowCountKey As System.Int32
  With New DataManagerFactory("SELECT  ACCESOXGRUPO.ID_ACCESO ROWCOUNT, ACCESOXGRUPO.ID_GRUPO_ACCESO FROM SEGURIDAD.ACCESOXGRUPO ACCESOXGRUPO  WHERE ACCESOXGRUPO.ID_ACCESO = @:ID_ACCESO AND ACCESOXGRUPO.ID_GRUPO_ACCESO = @:ID_GRUPO_ACCESO", "AccesoXGrupo", "Linked.Seguridad")
             .AddParameter("ID_ACCESO", DbType.Decimal, 10, False, e.NewValues("ID_ACCESO"))
.AddParameter("ID_GRUPO_ACCESO", DbType.Decimal, 10, False, e.NewValues("ID_GRUPO_ACCESO"))
 
             rowCountKey = .QueryExecuteScalarToInteger()  
        End With
        If rowCountKey > 0 Then
                errorMessage += String.Format(CultureInfo.InvariantCulture, "</ol><ul style='font-weight:bold'>{0}</ul>", 
                                                                            GetLocalResourceObject("AccesoXGrupoMessageErrorGeneralValidator0Resource").ToString)                
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