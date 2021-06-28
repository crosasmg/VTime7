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

Partial Class Maintenance_Grupo_Acceso
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

#Region "Grupo_Acceso Events"
    
    Protected Sub Grupo_Acceso_CustomColumnDisplayText(sender As Object, e As DevExpress.Web.ASPxGridView.ASPxGridViewColumnDisplayTextEventArgs) Handles Grupo_Acceso.CustomColumnDisplayText
          Dim data As DataTable
          Dim rows() As DataRow
           
          Select Case e.Column.FieldName

               Case Else              
           End Select
    End Sub
  
    Protected Sub Grupo_Acceso_DataBinding(ByVal sender As Object, ByVal e As EventArgs) Handles Grupo_Acceso.DataBinding
        If (Not IsNothing(Request.Params("__CALLBACKID")) AndAlso Request.Params("__CALLBACKID").EndsWith("Grupo_Acceso")) Or _internalCall Then
           

                 With New DataManagerFactory("SELECT  GRUPO_ACCESO.ID_GRUPO_ACCESO, GRUPO_ACCESO.DESCRIPCION, GRUPO_ACCESO.DESCRIPCION_CORTA, GRUPO_ACCESO.ESTADO_REGISTRO FROM SEGURIDAD.GRUPO_ACCESO GRUPO_ACCESO   ORDER BY Grupo_Acceso.Id_Grupo_Acceso ASC", "Grupo_Acceso", "Linked.Seguridad")                 
                                                   
                                  
                      Grupo_Acceso.DataSource = .QueryExecuteToTable(True)
                 End With 
        End If     
    End Sub

    Protected Sub Grupo_Acceso_CellEditorInitialize(sender As Object, e As ASPxGridViewEditorEventArgs) Handles Grupo_Acceso.CellEditorInitialize
        If Grupo_Acceso.IsNewRowEditing Then
            Select Case e.Column.FieldName
                              
                
                Case "DESCRIPCION"                     
                       e.Editor.Focus()               
            End Select

        Else

            Select Case e.Column.FieldName
                                   
                
                
                Case "DESCRIPCION"                     
                     e.Editor.Focus()
            End Select
        End If
        
       Select Case e.Column.FieldName
           Case "DESCRIPCION"
                 
                 
           
        End Select
    End Sub

    Protected Sub Grupo_Acceso_RowInserting(ByVal sender As Object, ByVal e As ASPxDataInsertingEventArgs) Handles Grupo_Acceso.RowInserting 
        Dim isNullResult As Boolean = True
        
             With New DataManagerFactory("INSERT INTO SEGURIDAD.Grupo_Acceso (ID_GRUPO_ACCESO, DESCRIPCION, DESCRIPCION_CORTA, ESTADO_REGISTRO, CREATIONDATE, CREATORUSERCODE, UPDATEDATE, UPDATEUSERCODE) VALUES (9999, @:DESCRIPCION, @:DESCRIPCION_CORTA, @:ESTADO_REGISTRO, SYSDATE, @:CREATORUSERCODE, SYSDATE, @:UPDATEUSERCODE)", "Grupo_Acceso", "Linked.Seguridad")                 
                                                   
                       .AddParameter("DESCRIPCION", DbType.AnsiString, 75, False, e.NewValues("DESCRIPCION"))
.AddParameter("DESCRIPCION_CORTA", DbType.AnsiString, 40, False, e.NewValues("DESCRIPCION_CORTA"))
.AddParameter("ESTADO_REGISTRO", DbType.AnsiString, 1, False, e.NewValues("ESTADO_REGISTRO"))
.AddParameter("CREATORUSERCODE", DbType.Decimal, 9, False, Session("nUserCode"))
.AddParameter("UPDATEUSERCODE", DbType.Decimal, 9, False, Session("nUserCode"))
            
                       .CommandExecute()
                End With
               
        e.Cancel = True
        Grupo_Acceso.CancelEdit()
    End Sub

    Protected Sub Grupo_Acceso_RowUpdating(ByVal sender As Object, ByVal e As ASPxDataUpdatingEventArgs) Handles Grupo_Acceso.RowUpdating
        Dim isNullResult As Boolean = True
          
             With New DataManagerFactory("UPDATE SEGURIDAD.Grupo_Acceso SET DESCRIPCION = @:DESCRIPCION, DESCRIPCION_CORTA = @:DESCRIPCION_CORTA, ESTADO_REGISTRO = @:ESTADO_REGISTRO, UPDATEDATE = SYSDATE, UPDATEUSERCODE = @:UPDATEUSERCODE WHERE ID_GRUPO_ACCESO = @:ID_GRUPO_ACCESO", "Grupo_Acceso", "Linked.Seguridad")                 
                                                   
                       .AddParameter("DESCRIPCION", DbType.AnsiString, 0, False, e.NewValues("DESCRIPCION"))
.AddParameter("DESCRIPCION_CORTA", DbType.AnsiString, 0, False, e.NewValues("DESCRIPCION_CORTA"))
.AddParameter("ESTADO_REGISTRO", DbType.AnsiString, 0, False, e.NewValues("ESTADO_REGISTRO"))
.AddParameter("UPDATEUSERCODE", DbType.Decimal, 0, False, Session("nUserCode"))
.AddParameter("ID_GRUPO_ACCESO", DbType.Decimal, 0, False, e.Keys("ID_GRUPO_ACCESO"))
            
                       .CommandExecute()
                End With         
      
         e.Cancel = True
        Grupo_Acceso.CancelEdit()
    End Sub
    
    Protected Sub Grupo_Acceso_CustomCallback(sender As Object, e As ASPxGridViewCustomCallbackEventArgs) Handles Grupo_Acceso.CustomCallback     
           Dim isNullResult As Boolean = True
           
           Select Case e.Parameters.ToString.ToLower
               Case "delete"
                       Dim ID_GRUPO_ACCESOKey As Generic.List(Of Object) = Grupo_Acceso.GetSelectedFieldValues("ID_GRUPO_ACCESO")
        
               For index As Integer = 0 To ID_GRUPO_ACCESOKey.Count - 1
                  With New DataManagerFactory("DELETE FROM SEGURIDAD.Grupo_Acceso WHERE ID_GRUPO_ACCESO = @:ID_GRUPO_ACCESO ", "Grupo_Acceso", "Linked.Seguridad")                 
                                                   
               .AddParameter("ID_GRUPO_ACCESO", DbType.Decimal, 0, False, ID_GRUPO_ACCESOKey(index))
            
               .CommandExecute()
          End With 
                       
              Next           
           
              Grupo_Acceso.DataBind()
                 
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
    
    Protected Sub Grupo_Acceso_RowValidating(sender As Object, e As ASPxDataValidationEventArgs) Handles Grupo_Acceso.RowValidating
        Dim errorMessage As String = "<ol style='font-weight:lighter'>"
        
        
        
        If e.IsNewRow Then
           Dim rowCountKey As System.Int32
  With New DataManagerFactory("SELECT  GRUPO_ACCESO.ID_GRUPO_ACCESO ROWCOUNT FROM SEGURIDAD.GRUPO_ACCESO GRUPO_ACCESO  WHERE GRUPO_ACCESO.ID_GRUPO_ACCESO = @:ID_GRUPO_ACCESO", "Grupo_Acceso", "Linked.Seguridad")
             .AddParameter("ID_GRUPO_ACCESO", DbType.Decimal, 10, False, e.NewValues("ID_GRUPO_ACCESO"))
 
             rowCountKey = .QueryExecuteScalarToInteger()  
        End With
        If rowCountKey > 0 Then
                errorMessage += String.Format(CultureInfo.InvariantCulture, "</ol><ul style='font-weight:bold'>{0}</ul>", 
                                                                            GetLocalResourceObject("Grupo_AccesoMessageErrorGeneralValidator0Resource").ToString)                
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