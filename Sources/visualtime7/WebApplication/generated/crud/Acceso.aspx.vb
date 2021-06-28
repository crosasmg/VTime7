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

Partial Class Maintenance_Acceso
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

#Region "Acceso Events"
    
    Protected Sub Acceso_CustomColumnDisplayText(sender As Object, e As DevExpress.Web.ASPxGridView.ASPxGridViewColumnDisplayTextEventArgs) Handles Acceso.CustomColumnDisplayText
          Dim data As DataTable
          Dim rows() As DataRow
           
          Select Case e.Column.FieldName

               Case Else              
           End Select
    End Sub
  
    Protected Sub Acceso_DataBinding(ByVal sender As Object, ByVal e As EventArgs) Handles Acceso.DataBinding
        If (Not IsNothing(Request.Params("__CALLBACKID")) AndAlso Request.Params("__CALLBACKID").EndsWith("Acceso")) Or _internalCall Then
           With New DataManagerFactory("SELECT  ENUM_TIPO_ACCESO.ID_TIPO_ACCESO, LTRIM(RTRIM(ENUM_TIPO_ACCESO.ESTADO_REGISTRO)) ESTADO_REGISTRO, ETRANS_TIPO_ACCESO.ID_TIPO_ACCESO, ETRANS_TIPO_ACCESO.DESCRIPCION, ETRANS_TIPO_ACCESO.ID_LENGUAJE FROM SEGURIDAD.ENUM_TIPO_ACCESO ENUM_TIPO_ACCESO JOIN ETRANS_TIPO_ACCESO ETRANS_TIPO_ACCESO ON ETRANS_TIPO_ACCESO.ID_TIPO_ACCESO = ENUM_TIPO_ACCESO.ID_TIPO_ACCESO  WHERE ENUM_TIPO_ACCESO.ESTADO_REGISTRO = '1' AND ETRANS_TIPO_ACCESO.ID_LENGUAJE = @:ID_LENGUAJE ", "Enum_Tipo_Acceso", "Linked.Seguridad")                 
                                                   
                .AddParameter("ID_LENGUAJE", DbType.Decimal, 5, False, CurrentState.Get("LanguageId"))
  
               
                DirectCast(Acceso.Columns("ID_TIPO_ACCESO"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = .QueryExecuteToTable(True)
           End With 
 

                 With New DataManagerFactory("SELECT  ACCESO.ID_ACCESO, ACCESO.ID_TIPO_ACCESO, ACCESO.DESCRIPCION, ACCESO.DESCRIPCION_CORTA, ACCESO.ESTADO_REGISTRO FROM SEGURIDAD.ACCESO ACCESO   ORDER BY Acceso.Id_Acceso ASC", "Acceso", "Linked.Seguridad")                 
                                                   
                                  
                      Acceso.DataSource = .QueryExecuteToTable(True)
                 End With 
        End If     
    End Sub

    Protected Sub Acceso_CellEditorInitialize(sender As Object, e As ASPxGridViewEditorEventArgs) Handles Acceso.CellEditorInitialize
        If Acceso.IsNewRowEditing Then
            Select Case e.Column.FieldName
                              
                
                Case "ID_TIPO_ACCESO"                     
                       e.Editor.Focus()               
            End Select

        Else

            Select Case e.Column.FieldName
                                   
                
                
                Case "ID_TIPO_ACCESO"                     
                     e.Editor.Focus()
            End Select
        End If
        
       Select Case e.Column.FieldName
           Case "ID_TIPO_ACCESO"
                 DirectCast(e.Editor, ASPxComboBox).DataBindItems()
                 
           
        End Select
    End Sub

    Protected Sub Acceso_RowInserting(ByVal sender As Object, ByVal e As ASPxDataInsertingEventArgs) Handles Acceso.RowInserting 
        Dim isNullResult As Boolean = True
        
             With New DataManagerFactory("INSERT INTO SEGURIDAD.Acceso (ID_TIPO_ACCESO, DESCRIPCION, DESCRIPCION_CORTA, ESTADO_REGISTRO, CREATIONDATE, CREATORUSERCODE, UPDATEDATE, UPDATEUSERCODE) VALUES (@:ID_TIPO_ACCESO, @:DESCRIPCION, @:DESCRIPCION_CORTA, @:ESTADO_REGISTRO, SYSDATE, @:CREATORUSERCODE, SYSDATE, @:UPDATEUSERCODE)", "Acceso", "Linked.Seguridad")                 
                                                   
                       .AddParameter("ID_TIPO_ACCESO", DbType.Decimal, 10, False, e.NewValues("ID_TIPO_ACCESO"))
.AddParameter("DESCRIPCION", DbType.AnsiString, 75, False, e.NewValues("DESCRIPCION"))
.AddParameter("DESCRIPCION_CORTA", DbType.AnsiString, 50, False, e.NewValues("DESCRIPCION_CORTA"))
.AddParameter("ESTADO_REGISTRO", DbType.AnsiString, 1, False, e.NewValues("ESTADO_REGISTRO"))
.AddParameter("CREATORUSERCODE", DbType.Decimal, 9, False, Session("nUserCode"))
.AddParameter("UPDATEUSERCODE", DbType.Decimal, 9, False, Session("nUserCode"))
            
                       .CommandExecute()
                End With
               
        e.Cancel = True
        Acceso.CancelEdit()
    End Sub

    Protected Sub Acceso_RowUpdating(ByVal sender As Object, ByVal e As ASPxDataUpdatingEventArgs) Handles Acceso.RowUpdating
        Dim isNullResult As Boolean = True
          
             With New DataManagerFactory("UPDATE SEGURIDAD.Acceso SET ID_TIPO_ACCESO = @:ID_TIPO_ACCESO, DESCRIPCION = @:DESCRIPCION, DESCRIPCION_CORTA = @:DESCRIPCION_CORTA, ESTADO_REGISTRO = @:ESTADO_REGISTRO, UPDATEDATE = SYSDATE, UPDATEUSERCODE = @:UPDATEUSERCODE WHERE ID_ACCESO = @:ID_ACCESO", "Acceso", "Linked.Seguridad")                 
                                                   
                       .AddParameter("ID_TIPO_ACCESO", DbType.Decimal, 0, False, e.NewValues("ID_TIPO_ACCESO"))
.AddParameter("DESCRIPCION", DbType.AnsiString, 0, False, e.NewValues("DESCRIPCION"))
.AddParameter("DESCRIPCION_CORTA", DbType.AnsiString, 0, False, e.NewValues("DESCRIPCION_CORTA"))
.AddParameter("ESTADO_REGISTRO", DbType.AnsiString, 0, False, e.NewValues("ESTADO_REGISTRO"))
.AddParameter("UPDATEUSERCODE", DbType.Decimal, 0, False, Session("nUserCode"))
.AddParameter("ID_ACCESO", DbType.Decimal, 0, False, e.Keys("ID_ACCESO"))
            
                       .CommandExecute()
                End With         
      
         e.Cancel = True
        Acceso.CancelEdit()
    End Sub
    
    Protected Sub Acceso_CustomCallback(sender As Object, e As ASPxGridViewCustomCallbackEventArgs) Handles Acceso.CustomCallback     
           Dim isNullResult As Boolean = True
           
           Select Case e.Parameters.ToString.ToLower
               Case "delete"
                       Dim ID_ACCESOKey As Generic.List(Of Object) = Acceso.GetSelectedFieldValues("ID_ACCESO")
        
               For index As Integer = 0 To ID_ACCESOKey.Count - 1
                  With New DataManagerFactory("DELETE FROM SEGURIDAD.Acceso WHERE ID_ACCESO = @:ID_ACCESO ", "Acceso", "Linked.Seguridad")                 
                                                   
               .AddParameter("ID_ACCESO", DbType.Decimal, 0, False, ID_ACCESOKey(index))
            
               .CommandExecute()
          End With 
                       
              Next           
           
              Acceso.DataBind()
                 
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
    
    Protected Sub Acceso_RowValidating(sender As Object, e As ASPxDataValidationEventArgs) Handles Acceso.RowValidating
        Dim errorMessage As String = "<ol style='font-weight:lighter'>"
        
        
        
        If e.IsNewRow Then
           Dim rowCountKey As System.Int32
  With New DataManagerFactory("SELECT  ACCESO.ID_ACCESO ROWCOUNT FROM SEGURIDAD.ACCESO ACCESO  WHERE ACCESO.ID_ACCESO = @:ID_ACCESO", "Acceso", "Linked.Seguridad")
             .AddParameter("ID_ACCESO", DbType.Decimal, 10, False, e.NewValues("ID_ACCESO"))
 
             rowCountKey = .QueryExecuteScalarToInteger()  
        End With
        If rowCountKey > 0 Then
                errorMessage += String.Format(CultureInfo.InvariantCulture, "</ol><ul style='font-weight:bold'>{0}</ul>", 
                                                                            GetLocalResourceObject("AccesoMessageErrorGeneralValidator0Resource").ToString)                
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