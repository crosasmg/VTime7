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

Partial Class Maintenance_GrupoXRol
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

#Region "GrupoXRol Events"
    
    Protected Sub GrupoXRol_CustomColumnDisplayText(sender As Object, e As DevExpress.Web.ASPxGridView.ASPxGridViewColumnDisplayTextEventArgs) Handles GrupoXRol.CustomColumnDisplayText
          Dim data As DataTable
          Dim rows() As DataRow
           
          Select Case e.Column.FieldName

               Case Else              
           End Select
    End Sub
  
    Protected Sub GrupoXRol_DataBinding(ByVal sender As Object, ByVal e As EventArgs) Handles GrupoXRol.DataBinding
        If (Not IsNothing(Request.Params("__CALLBACKID")) AndAlso Request.Params("__CALLBACKID").EndsWith("GrupoXRol")) Or _internalCall Then
           With New DataManagerFactory("SELECT  ROLE.ROLEID, ROLE.ROLENAME FROM FRONTOFFICE.ROLE ROLE   ", "ROLE", "Linked.FrontOffice")                 
                                                   
                  
               
                DirectCast(GrupoXRol.Columns("ID_ROL"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = .QueryExecuteToTable(True)
           End With 
 With New DataManagerFactory("SELECT  GRUPO_ACCESO.ID_GRUPO_ACCESO, GRUPO_ACCESO.DESCRIPCION FROM SEGURIDAD.GRUPO_ACCESO GRUPO_ACCESO   ", "Grupo_Acceso", "Linked.Seguridad")                 
                                                   
                  
               
                DirectCast(GrupoXRol.Columns("ID_GRUPO_ACCESO"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = .QueryExecuteToTable(True)
           End With 
 

                 With New DataManagerFactory("SELECT  GRUPOXROL.ID_ROL, GRUPOXROL.ID_GRUPO_ACCESO, GRUPOXROL.ID_COMPANIA, GRUPOXROL.EXTERNALUSER FROM SEGURIDAD.GRUPOXROL GRUPOXROL   ORDER BY GrupoXRol.Id_Rol ASC, GrupoXRol.Id_Grupo_Acceso ASC, GrupoXRol.Id_Compania ASC, GrupoXRol.ExternalUser ASC", "GrupoXRol", "Linked.Seguridad")                 
                                                   
                                  
                      GrupoXRol.DataSource = .QueryExecuteToTable(True)
                 End With 
        End If     
    End Sub

    Protected Sub GrupoXRol_CellEditorInitialize(sender As Object, e As ASPxGridViewEditorEventArgs) Handles GrupoXRol.CellEditorInitialize
        If GrupoXRol.IsNewRowEditing Then
            Select Case e.Column.FieldName
                              
                
                Case "ID_ROL"                     
                       e.Editor.Focus()               
            End Select

        Else

            Select Case e.Column.FieldName
                Case "ID_ROL"
                     e.Editor.Enabled = True
                     e.Editor.ClientEnabled = False
Case "ID_GRUPO_ACCESO"
                     e.Editor.Enabled = True
                     e.Editor.ClientEnabled = False
Case "ID_COMPANIA"
                     e.Editor.Enabled = True
                     e.Editor.ClientEnabled = False
                   
                
                
                Case "EXTERNALUSER"                     
                     e.Editor.Focus()
            End Select
        End If
        
       Select Case e.Column.FieldName
           Case "ID_ROL"
                 DirectCast(e.Editor, ASPxComboBox).DataBindItems()
                 
           Case "ID_GRUPO_ACCESO"
     DirectCast(e.Editor, ASPxComboBox).DataBindItems() 

        End Select
    End Sub

    Protected Sub GrupoXRol_RowInserting(ByVal sender As Object, ByVal e As ASPxDataInsertingEventArgs) Handles GrupoXRol.RowInserting 
        Dim isNullResult As Boolean = True
        
             With New DataManagerFactory("INSERT INTO SEGURIDAD.GrupoXRol (ID_ROL, ID_GRUPO_ACCESO, ID_COMPANIA, EXTERNALUSER) VALUES (@:ID_ROL, @:ID_GRUPO_ACCESO, @:ID_COMPANIA, @:EXTERNALUSER)", "GrupoXRol", "Linked.Seguridad")                 
                                                   
                       .AddParameter("ID_ROL", DbType.Decimal, 10, False, e.NewValues("ID_ROL"))
.AddParameter("ID_GRUPO_ACCESO", DbType.Decimal, 10, False, e.NewValues("ID_GRUPO_ACCESO"))
.AddParameter("ID_COMPANIA", DbType.Decimal, 5, False, e.NewValues("ID_COMPANIA"))
.AddParameter("EXTERNALUSER", DbType.AnsiString, 5, False, e.NewValues("EXTERNALUSER"))
            
                       .CommandExecute()
                End With
               
        e.Cancel = True
        GrupoXRol.CancelEdit()
    End Sub

    Protected Sub GrupoXRol_RowUpdating(ByVal sender As Object, ByVal e As ASPxDataUpdatingEventArgs) Handles GrupoXRol.RowUpdating
        Dim isNullResult As Boolean = True
          
             With New DataManagerFactory("UPDATE SEGURIDAD.GrupoXRol SET  WHERE ID_ROL = @:ID_ROL AND ID_GRUPO_ACCESO = @:ID_GRUPO_ACCESO AND ID_COMPANIA = @:ID_COMPANIA AND EXTERNALUSER = @:EXTERNALUSER", "GrupoXRol", "Linked.Seguridad")                 
                                                   
                       .AddParameter("ID_ROL", DbType.Decimal, 0, False, e.Keys("ID_ROL"))
.AddParameter("ID_GRUPO_ACCESO", DbType.Decimal, 0, False, e.Keys("ID_GRUPO_ACCESO"))
.AddParameter("ID_COMPANIA", DbType.Decimal, 0, False, e.Keys("ID_COMPANIA"))
.AddParameter("EXTERNALUSER", DbType.AnsiString, 0, False, e.Keys("EXTERNALUSER"))
            
                       .CommandExecute()
                End With         
      
         e.Cancel = True
        GrupoXRol.CancelEdit()
    End Sub
    
    Protected Sub GrupoXRol_CustomCallback(sender As Object, e As ASPxGridViewCustomCallbackEventArgs) Handles GrupoXRol.CustomCallback     
           Dim isNullResult As Boolean = True
           
           Select Case e.Parameters.ToString.ToLower
               Case "delete"
                       Dim ID_ROLKey As Generic.List(Of Object) = GrupoXRol.GetSelectedFieldValues("ID_ROL")
 Dim ID_GRUPO_ACCESOKey As Generic.List(Of Object) = GrupoXRol.GetSelectedFieldValues("ID_GRUPO_ACCESO")
 Dim ID_COMPANIAKey As Generic.List(Of Object) = GrupoXRol.GetSelectedFieldValues("ID_COMPANIA")
 Dim EXTERNALUSERKey As Generic.List(Of Object) = GrupoXRol.GetSelectedFieldValues("EXTERNALUSER")
        
               For index As Integer = 0 To ID_ROLKey.Count - 1
                  With New DataManagerFactory("DELETE FROM SEGURIDAD.GrupoXRol WHERE ID_ROL = @:ID_ROL AND ID_GRUPO_ACCESO = @:ID_GRUPO_ACCESO AND ID_COMPANIA = @:ID_COMPANIA AND EXTERNALUSER = @:EXTERNALUSER ", "GrupoXRol", "Linked.Seguridad")                 
                                                   
               .AddParameter("ID_ROL", DbType.Decimal, 0, False, ID_ROLKey(index))
.AddParameter("ID_GRUPO_ACCESO", DbType.Decimal, 0, False, ID_GRUPO_ACCESOKey(index))
.AddParameter("ID_COMPANIA", DbType.Decimal, 0, False, ID_COMPANIAKey(index))
.AddParameter("EXTERNALUSER", DbType.AnsiString, 0, False, EXTERNALUSERKey(index))
            
               .CommandExecute()
          End With 
                       
              Next           
           
              GrupoXRol.DataBind()
                 
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
    
    Protected Sub GrupoXRol_RowValidating(sender As Object, e As ASPxDataValidationEventArgs) Handles GrupoXRol.RowValidating
        Dim errorMessage As String = "<ol style='font-weight:lighter'>"
        
        If IsNothing(e.NewValues("ID_COMPANIA")) OrElse e.NewValues("ID_COMPANIA") = 0  
   e.Errors(GrupoXRol.Columns("ID_COMPANIA")) = GetLocalResourceObject("ID_COMPANIAMessageErrorRequired1Resource").ToString
End If
 
        
        If e.IsNewRow Then
           Dim rowCountKey As System.Int32
  With New DataManagerFactory("SELECT  GRUPOXROL.ID_ROL ROWCOUNT, GRUPOXROL.ID_GRUPO_ACCESO, GRUPOXROL.ID_COMPANIA, GRUPOXROL.EXTERNALUSER FROM SEGURIDAD.GRUPOXROL GRUPOXROL  WHERE GRUPOXROL.ID_ROL = @:ID_ROL AND GRUPOXROL.ID_GRUPO_ACCESO = @:ID_GRUPO_ACCESO AND GRUPOXROL.ID_COMPANIA = @:ID_COMPANIA AND GRUPOXROL.EXTERNALUSER = @:EXTERNALUSER", "GrupoXRol", "Linked.Seguridad")
             .AddParameter("ID_ROL", DbType.Decimal, 10, False, e.NewValues("ID_ROL"))
.AddParameter("ID_GRUPO_ACCESO", DbType.Decimal, 10, False, e.NewValues("ID_GRUPO_ACCESO"))
.AddParameter("ID_COMPANIA", DbType.Decimal, 5, False, e.NewValues("ID_COMPANIA"))
.AddParameter("EXTERNALUSER", DbType.AnsiString, 5, False, e.NewValues("EXTERNALUSER"))
 
             rowCountKey = .QueryExecuteScalarToInteger()  
        End With
        If rowCountKey > 0 Then
                errorMessage += String.Format(CultureInfo.InvariantCulture, "</ol><ul style='font-weight:bold'>{0}</ul>", 
                                                                            GetLocalResourceObject("GrupoXRolMessageErrorGeneralValidator0Resource").ToString)                
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