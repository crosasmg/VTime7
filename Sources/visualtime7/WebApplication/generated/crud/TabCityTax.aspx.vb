#Region "using"

Imports System.Globalization
Imports GIT.Core
Imports DevExpress.Web.ASPxGridView
Imports DevExpress.Web.ASPxEditors
Imports System
Imports DevExpress.Web.Data
Imports InMotionGIT.Common.Proxy
Imports InMotionGIT.Common.Helpers.Language
Imports InMotionGIT.Common.Helpers
Imports InMotionGIT.Data
Imports System.IO
Imports DevExpress.Web.ASPxClasses
Imports System.Data
Imports System.Data.Common
Imports DevExpress.Web.ASPxUploadControl

#End Region

Partial Class Maintenance_TabCityTax
    Inherits PageBase

#Region "Private fields"

    Private _internalCall As Boolean

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

#Region "TabCityTax Events"
    
    Protected Sub TabCityTax_CustomColumnDisplayText(sender As Object, e As DevExpress.Web.ASPxGridView.ASPxGridViewColumnDisplayTextEventArgs) Handles TabCityTax.CustomColumnDisplayText
          Dim data As DataTable
          Dim rows() As DataRow
           
          Select Case e.Column.FieldName

               Case Else              
           End Select
    End Sub
  
    Protected Sub TabCityTax_DataBinding(ByVal sender As Object, ByVal e As EventArgs) Handles TabCityTax.DataBinding
        If (Not IsNothing(Request.Params("__CALLBACKID")) AndAlso Request.Params("__CALLBACKID").EndsWith("TabCityTax")) Or _internalCall Then
                       If Caching.Exist("EnumRecordStatus") Then
                DirectCast(TabCityTax.Columns("RECORDSTATUS"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = Caching.GetItem("EnumRecordStatus")

            Else
                Dim source As DataTable = Nothing

                With New DataManagerFactory("SELECT  ENUMRECORDSTATUS.RECORDSTATUS, ETRANRECORDSTATUS.LANGUAGEID, ETRANRECORDSTATUS.DESCRIPTION FROM COMMON.ENUMRECORDSTATUS ENUMRECORDSTATUS JOIN COMMON.ETRANRECORDSTATUS ETRANRECORDSTATUS ON ETRANRECORDSTATUS.RECORDSTATUS = ENUMRECORDSTATUS.RECORDSTATUS  WHERE ETRANRECORDSTATUS.LANGUAGEID = @:LANGUAGEID ", "EnumRecordStatus", "Linked.Address")
                    .AddParameter("LANGUAGEID", DbType.Decimal, 5, False, CurrentState.Get("Language"))
 
                    source = .QueryExecuteToTable(True)
                    DirectCast(TabCityTax.Columns("RECORDSTATUS"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = source
                End With

                If Not IsNothing(source) AndAlso source.Rows.Count > 0 Then
                    Caching.SetItem("EnumRecordStatus", source)
                End If
            End If 
 

                 With New DataManagerFactory("SELECT  TABCITYTAX.CITYTAXCODE, TABCITYTAX.CITYTAX, TABCITYTAX.RECORDSTATUS FROM ADDRESS.TABCITYTAX TABCITYTAX  ", "TabCityTax", "Linked.Address")                 
                                                   
                                  
                      TabCityTax.DataSource = .QueryExecuteToTable(True)
                 End With 
        End If     
    End Sub

    Protected Sub TabCityTax_CellEditorInitialize(sender As Object, e As ASPxGridViewEditorEventArgs) Handles TabCityTax.CellEditorInitialize
        If TabCityTax.IsNewRowEditing Then
            Select Case e.Column.FieldName
                
                
                
                Case "CITYTAXCODE"                     
                       e.Editor.Focus()               
            End Select

        Else

            Select Case e.Column.FieldName
                Case "CITYTAXCODE"
     e.Editor.Enabled = False
                   
                
                
                Case "CITYTAX"                     
                     e.Editor.Focus()
            End Select
        End If
        
       Select Case e.Column.FieldName
           Case "CITYTAXCODE"
                 
                 
           Case "RECORDSTATUS"
     DirectCast(e.Editor, ASPxComboBox).DataBindItems() 

        End Select
    End Sub

    Protected Sub TabCityTax_RowInserting(ByVal sender As Object, ByVal e As ASPxDataInsertingEventArgs) Handles TabCityTax.RowInserting 
        Dim isNullResult As Boolean = True
        
             With New DataManagerFactory("INSERT INTO ADDRESS.TabCityTax (CITYTAXCODE, CITYTAX, RECORDSTATUS, CREATIONDATE, CREATORUSERCODE, UPDATEDATE, UPDATEUSERCODE) VALUES (@:CITYTAXCODE, @:CITYTAX, @:RECORDSTATUS, SYSDATE, @:CREATORUSERCODE, SYSDATE, @:UPDATEUSERCODE)", "TabCityTax", "Linked.Address")                 
                                                   
                       .AddParameter("CITYTAXCODE", DbType.Decimal, 0, False, e.NewValues("CITYTAXCODE"))
.AddParameter("CITYTAX", DbType.Decimal, 0, (e.NewValues("CITYTAX") = 0), e.NewValues("CITYTAX"))
.AddParameter("RECORDSTATUS", DbType.AnsiString, 0, False, e.NewValues("RECORDSTATUS"))
.AddParameter("CREATORUSERCODE", DbType.Decimal, 0, False, Session("nUsercode"))
.AddParameter("UPDATEUSERCODE", DbType.Decimal, 0, False, Session("nUsercode"))
            
                       .CommandExecute()
                End With
               
        e.Cancel = True
        TabCityTax.CancelEdit()
    End Sub

    Protected Sub TabCityTax_RowUpdating(ByVal sender As Object, ByVal e As ASPxDataUpdatingEventArgs) Handles TabCityTax.RowUpdating
        Dim isNullResult As Boolean = True
          
             With New DataManagerFactory("UPDATE ADDRESS.TabCityTax SET CITYTAX = @:CITYTAX, RECORDSTATUS = @:RECORDSTATUS, UPDATEDATE = SYSDATE, UPDATEUSERCODE = @:UPDATEUSERCODE WHERE CITYTAXCODE = @:CITYTAXCODE", "TabCityTax", "Linked.Address")                 
                                                   
                       .AddParameter("CITYTAX", DbType.Decimal, 0, (e.NewValues("CITYTAX") = 0), e.NewValues("CITYTAX"))
.AddParameter("RECORDSTATUS", DbType.AnsiString, 0, False, e.NewValues("RECORDSTATUS"))
.AddParameter("UPDATEUSERCODE", DbType.Decimal, 0, False, Session("nUsercode"))
.AddParameter("CITYTAXCODE", DbType.Decimal, 0, False, e.Keys("CITYTAXCODE"))
            
                       .CommandExecute()
                End With         
      
         e.Cancel = True
        TabCityTax.CancelEdit()
    End Sub
    
    Protected Sub TabCityTax_CustomCallback(sender As Object, e As ASPxGridViewCustomCallbackEventArgs) Handles TabCityTax.CustomCallback     
           Dim isNullResult As Boolean = True
           
           Select Case e.Parameters.ToString.ToLower
               Case "delete"
                       Dim CITYTAXCODEKey As Generic.List(Of Object) = TabCityTax.GetSelectedFieldValues("CITYTAXCODE")
        
               For index As Integer = 0 To CITYTAXCODEKey.Count - 1
                  With New DataManagerFactory("DELETE FROM ADDRESS.TabCityTax WHERE CITYTAXCODE = @:CITYTAXCODE ", "TabCityTax", "Linked.Address")                 
                                                   
               .AddParameter("CITYTAXCODE", DbType.Decimal, 0, False, CITYTAXCODEKey(index))
            
               .CommandExecute()
          End With 
                       
              Next           
           
              TabCityTax.DataBind()
                 
               Case Else
                   Dim fileName As String = String.Empty
                
                   If e.Parameters.ToString.ToLower.StartsWith("export") Then
                       Dim extension As String = e.Parameters.ToString.ToLower.Split("_")(1)
                       fileName = String.Format(CultureInfo.InvariantCulture, "{0}.{1}", IO.Path.GetRandomFileName, extension)

                       ASPxGridViewExporter.GridViewID = sender.ClientInstanceName

                       Using fs As FileStream = New FileStream(String.Format(CultureInfo.InvariantCulture, "{0}\temp\{1}", Server.MapPath("/"), fileName), FileMode.Create)
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

                      ASPxWebControl.RedirectOnCallback(String.Format(CultureInfo.InvariantCulture, "~/dropthings/download.ashx?Directory=temp&File={0}", fileName))
                               
                  End If
         End Select
     End Sub
    
    Protected Sub TabCityTax_RowValidating(sender As Object, e As ASPxDataValidationEventArgs) Handles TabCityTax.RowValidating

        
        If e.Errors.Count > 0 Then
            Dim errorMessage As String = "<ol style='font-weight:lighter'>"

            For Each item As KeyValuePair(Of GridViewColumn, String) In e.Errors
                errorMessage += String.Format("<li>{0}</li>", item.Value)
            Next

           errorMessage += String.Format(CultureInfo.InvariantCulture, "</ol><ul style='font-weight:bold'>{0}</ul>", GetLocalResourceObject("MessageErrorText").ToString)

            e.RowError = errorMessage
        End If

    End Sub

#End Region
 


End Class