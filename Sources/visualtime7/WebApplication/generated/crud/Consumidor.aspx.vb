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

Partial Class Maintenance_Consumidor
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

#Region "Consumidor Events"
    
    Protected Sub Consumidor_CustomColumnDisplayText(sender As Object, e As DevExpress.Web.ASPxGridView.ASPxGridViewColumnDisplayTextEventArgs) Handles Consumidor.CustomColumnDisplayText
          Dim data As DataTable
          Dim rows() As DataRow
           
          Select Case e.Column.FieldName

               Case Else              
           End Select
    End Sub
  
    Protected Sub Consumidor_DataBinding(ByVal sender As Object, ByVal e As EventArgs) Handles Consumidor.DataBinding
        If (Not IsNothing(Request.Params("__CALLBACKID")) AndAlso Request.Params("__CALLBACKID").EndsWith("Consumidor")) Or _internalCall Then
           

                 With New DataManagerFactory("SELECT  CONSUMIDOR.ID_CONSUMIDOR, CONSUMIDOR.ID_COMPANIA, CONSUMIDOR.NOMBRE_CONSUMIDOR, CONSUMIDOR.IDENTIFICADOR_CONSUMIDOR, CONSUMIDOR.VIDA_TOKEN_ACCESO FROM SEGURIDAD.CONSUMIDOR CONSUMIDOR   ORDER BY Consumidor.Id_Consumidor ASC, Consumidor.Id_Compania ASC", "Consumidor", "Linked.Seguridad")                 
                                                   
                                  
                      Consumidor.DataSource = .QueryExecuteToTable(True)
                 End With 
        End If     
    End Sub

    Protected Sub Consumidor_CellEditorInitialize(sender As Object, e As ASPxGridViewEditorEventArgs) Handles Consumidor.CellEditorInitialize
        If Consumidor.IsNewRowEditing Then
            Select Case e.Column.FieldName
                              
                
                Case "ID_CONSUMIDOR"                     
                       e.Editor.Focus()               
            End Select

        Else

            Select Case e.Column.FieldName
                Case "ID_CONSUMIDOR"
                     e.Editor.Enabled = True
                     e.Editor.ClientEnabled = False
Case "ID_COMPANIA"
                     e.Editor.Enabled = True
                     e.Editor.ClientEnabled = False
Case "NOMBRE_CONSUMIDOR"
                     e.Editor.Enabled = True
                     e.Editor.ClientEnabled = False
Case "IDENTIFICADOR_CONSUMIDOR"
                     e.Editor.Enabled = True
                     e.Editor.ClientEnabled = False
                   
                
                
                Case "VIDA_TOKEN_ACCESO"                     
                     e.Editor.Focus()
            End Select
        End If
        
       Select Case e.Column.FieldName
           Case "ID_CONSUMIDOR"
                 
                 
           
        End Select
    End Sub

    Protected Sub Consumidor_RowInserting(ByVal sender As Object, ByVal e As ASPxDataInsertingEventArgs) Handles Consumidor.RowInserting 
        Dim isNullResult As Boolean = True
        
             With New DataManagerFactory("INSERT INTO SEGURIDAD.Consumidor (ID_CONSUMIDOR, ID_COMPANIA, NOMBRE_CONSUMIDOR, IDENTIFICADOR_CONSUMIDOR, VIDA_TOKEN_ACCESO, CREATIONDATE, CREATORUSERCODE, UPDATEDATE, UPDATEUSERCODE) VALUES (@:ID_CONSUMIDOR, @:ID_COMPANIA, @:NOMBRE_CONSUMIDOR, @:IDENTIFICADOR_CONSUMIDOR, @:VIDA_TOKEN_ACCESO, SYSDATE, @:CREATORUSERCODE, SYSDATE, @:UPDATEUSERCODE)", "Consumidor", "Linked.Seguridad")                 
                                                   
                       .AddParameter("ID_CONSUMIDOR", DbType.Decimal, 0, False, e.NewValues("ID_CONSUMIDOR"))
.AddParameter("ID_COMPANIA", DbType.Decimal, 0, False, e.NewValues("ID_COMPANIA"))
.AddParameter("NOMBRE_CONSUMIDOR", DbType.AnsiString, 0, False, e.NewValues("NOMBRE_CONSUMIDOR"))
.AddParameter("IDENTIFICADOR_CONSUMIDOR", DbType.AnsiString, 0, False, e.NewValues("IDENTIFICADOR_CONSUMIDOR"))
.AddParameter("VIDA_TOKEN_ACCESO", DbType.Decimal, 0, False, e.NewValues("VIDA_TOKEN_ACCESO"))
.AddParameter("CREATORUSERCODE", DbType.Decimal, 0, False, Session("nUserCode"))
.AddParameter("UPDATEUSERCODE", DbType.Decimal, 0, False, Session("nUserCode"))
            
                       .CommandExecute()
                End With
               
        e.Cancel = True
        Consumidor.CancelEdit()
    End Sub

    Protected Sub Consumidor_RowUpdating(ByVal sender As Object, ByVal e As ASPxDataUpdatingEventArgs) Handles Consumidor.RowUpdating
        Dim isNullResult As Boolean = True
          
             With New DataManagerFactory("UPDATE SEGURIDAD.Consumidor SET NOMBRE_CONSUMIDOR = @:NOMBRE_CONSUMIDOR, IDENTIFICADOR_CONSUMIDOR = @:IDENTIFICADOR_CONSUMIDOR, VIDA_TOKEN_ACCESO = @:VIDA_TOKEN_ACCESO, UPDATEDATE = SYSDATE, UPDATEUSERCODE = @:UPDATEUSERCODE WHERE ID_CONSUMIDOR = @:ID_CONSUMIDOR AND ID_COMPANIA = @:ID_COMPANIA", "Consumidor", "Linked.Seguridad")                 
                                                   
                       .AddParameter("NOMBRE_CONSUMIDOR", DbType.AnsiString, 0, False, e.NewValues("NOMBRE_CONSUMIDOR"))
.AddParameter("IDENTIFICADOR_CONSUMIDOR", DbType.AnsiString, 0, False, e.NewValues("IDENTIFICADOR_CONSUMIDOR"))
.AddParameter("VIDA_TOKEN_ACCESO", DbType.Decimal, 0, False, e.NewValues("VIDA_TOKEN_ACCESO"))
.AddParameter("UPDATEUSERCODE", DbType.Decimal, 0, False, Session("nUserCode"))
.AddParameter("ID_CONSUMIDOR", DbType.Decimal, 0, False, e.Keys("ID_CONSUMIDOR"))
.AddParameter("ID_COMPANIA", DbType.Decimal, 0, False, e.Keys("ID_COMPANIA"))
            
                       .CommandExecute()
                End With         
      
         e.Cancel = True
        Consumidor.CancelEdit()
    End Sub
    
    Protected Sub Consumidor_CustomCallback(sender As Object, e As ASPxGridViewCustomCallbackEventArgs) Handles Consumidor.CustomCallback     
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
    
    Protected Sub Consumidor_RowValidating(sender As Object, e As ASPxDataValidationEventArgs) Handles Consumidor.RowValidating
        Dim errorMessage As String = "<ol style='font-weight:lighter'>"
        
        If IsNothing(e.NewValues("ID_CONSUMIDOR")) OrElse e.NewValues("ID_CONSUMIDOR") = 0  
   e.Errors(Consumidor.Columns("ID_CONSUMIDOR")) = GetLocalResourceObject("ID_CONSUMIDORMessageErrorRequired1Resource").ToString
End If
 If IsNothing(e.NewValues("ID_COMPANIA")) OrElse e.NewValues("ID_COMPANIA") = 0  
   e.Errors(Consumidor.Columns("ID_COMPANIA")) = GetLocalResourceObject("ID_COMPANIAMessageErrorRequired2Resource").ToString
End If
 If IsNothing(e.NewValues("VIDA_TOKEN_ACCESO")) OrElse e.NewValues("VIDA_TOKEN_ACCESO") = 0  
   e.Errors(Consumidor.Columns("VIDA_TOKEN_ACCESO")) = GetLocalResourceObject("VIDA_TOKEN_ACCESOMessageErrorRequired3Resource").ToString
End If
 
        
        If e.IsNewRow Then
           Dim rowCountKey As System.Int32
  With New DataManagerFactory("SELECT  CONSUMIDOR.ID_CONSUMIDOR ROWCOUNT, CONSUMIDOR.ID_COMPANIA FROM SEGURIDAD.CONSUMIDOR CONSUMIDOR  WHERE CONSUMIDOR.ID_CONSUMIDOR = @:ID_CONSUMIDOR AND CONSUMIDOR.ID_COMPANIA = @:ID_COMPANIA", "Consumidor", "Linked.Seguridad")
             .AddParameter("ID_CONSUMIDOR", DbType.Decimal, 10, False, e.NewValues("ID_CONSUMIDOR"))
.AddParameter("ID_COMPANIA", DbType.Decimal, 5, False, e.NewValues("ID_COMPANIA"))
 
             rowCountKey = .QueryExecuteScalarToInteger()  
        End With
        If rowCountKey > 0 Then
                errorMessage += String.Format(CultureInfo.InvariantCulture, "</ol><ul style='font-weight:bold'>{0}</ul>", 
                                                                            GetLocalResourceObject("ConsumidorMessageErrorGeneralValidator0Resource").ToString)                
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