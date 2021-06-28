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

Partial Class Maintenance_TabAddressAllowed
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

#Region "TabAddressAllowed Events"
    
    Protected Sub TabAddressAllowed_CustomColumnDisplayText(sender As Object, e As DevExpress.Web.ASPxGridView.ASPxGridViewColumnDisplayTextEventArgs) Handles TabAddressAllowed.CustomColumnDisplayText
          Dim data As DataTable
          Dim rows() As DataRow
           
          Select Case e.Column.FieldName

               Case Else              
           End Select
    End Sub
  
    Protected Sub TabAddressAllowed_DataBinding(ByVal sender As Object, ByVal e As EventArgs) Handles TabAddressAllowed.DataBinding
        If (Not IsNothing(Request.Params("__CALLBACKID")) AndAlso Request.Params("__CALLBACKID").EndsWith("TabAddressAllowed")) Or _internalCall Then
                       If Caching.Exist("TabCompany") Then
                DirectCast(TabAddressAllowed.Columns("COMPANYID"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = Caching.GetItem("TabCompany")

            Else
                Dim source As DataTable = Nothing

                With New DataManagerFactory("SELECT  TABCOMPANY.COMPANYID, TABCOMPANY.RECORDSTATUS, TRANSCOMPANY.LANGUAGEID, TRANSCOMPANY.DESCRIPTION FROM COMMON.TABCOMPANY TABCOMPANY JOIN COMMON.TRANSCOMPANY TRANSCOMPANY ON TRANSCOMPANY.COMPANYID = TABCOMPANY.COMPANYID  WHERE TABCOMPANY.RECORDSTATUS = '1' AND TRANSCOMPANY.LANGUAGEID = @:LANGUAGEID ", "TabCompany", "Linked.Address")
                    .AddParameter("LANGUAGEID", DbType.Decimal, 5, False, CurrentState.Get("Language"))
 
                    source = .QueryExecuteToTable(True)
                    DirectCast(TabAddressAllowed.Columns("COMPANYID"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = source
                End With

                If Not IsNothing(source) AndAlso source.Rows.Count > 0 Then
                    Caching.SetItem("TabCompany", source)
                End If
            End If 
             If Caching.Exist("EnumTypeOfAddress") Then
                DirectCast(TabAddressAllowed.Columns("TYPEOFADDRESS"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = Caching.GetItem("EnumTypeOfAddress")

            Else
                Dim source As DataTable = Nothing

                With New DataManagerFactory("SELECT  ENUMTYPEOFADDRESS.TYPEOFADDRESS, ENUMTYPEOFADDRESS.RECORDSTATUS, ETRANTYPEOFADDRESS.LANGUAGEID, ETRANTYPEOFADDRESS.DESCRIPTION FROM ADDRESS.ENUMTYPEOFADDRESS ENUMTYPEOFADDRESS JOIN ADDRESS.ETRANTYPEOFADDRESS ETRANTYPEOFADDRESS ON ETRANTYPEOFADDRESS.TYPEOFADDRESS = ENUMTYPEOFADDRESS.TYPEOFADDRESS  WHERE ENUMTYPEOFADDRESS.RECORDSTATUS = '1' AND ETRANTYPEOFADDRESS.LANGUAGEID = @:LANGUAGEID ", "EnumTypeOfAddress", "Linked.Address")
                    .AddParameter("LANGUAGEID", DbType.Decimal, 5, False, CurrentState.Get("Language"))
 
                    source = .QueryExecuteToTable(True)
                    DirectCast(TabAddressAllowed.Columns("TYPEOFADDRESS"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = source
                End With

                If Not IsNothing(source) AndAlso source.Rows.Count > 0 Then
                    Caching.SetItem("EnumTypeOfAddress", source)
                End If
            End If 
             If Caching.Exist("EnumRecordStatus") Then
                DirectCast(TabAddressAllowed.Columns("RECORDSTATUS"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = Caching.GetItem("EnumRecordStatus")

            Else
                Dim source As DataTable = Nothing

                With New DataManagerFactory("SELECT  ENUMRECORDSTATUS.RECORDSTATUS, ETRANRECORDSTATUS.LANGUAGEID, ETRANRECORDSTATUS.DESCRIPTION FROM COMMON.ENUMRECORDSTATUS ENUMRECORDSTATUS JOIN COMMON.ETRANRECORDSTATUS ETRANRECORDSTATUS ON ETRANRECORDSTATUS.RECORDSTATUS = ENUMRECORDSTATUS.RECORDSTATUS  WHERE ETRANRECORDSTATUS.LANGUAGEID = @:LANGUAGEID ", "EnumRecordStatus", "Linked.Address")
                    .AddParameter("LANGUAGEID", DbType.Decimal, 5, False, CurrentState.Get("Language"))
 
                    source = .QueryExecuteToTable(True)
                    DirectCast(TabAddressAllowed.Columns("RECORDSTATUS"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = source
                End With

                If Not IsNothing(source) AndAlso source.Rows.Count > 0 Then
                    Caching.SetItem("EnumRecordStatus", source)
                End If
            End If 
 

                 With New DataManagerFactory("SELECT  TABADDRESSALLOWED.COMPANYID, TABADDRESSALLOWED.TYPEOFADDRESS, TABADDRESSALLOWED.RECORDOWNERTYPE, TABADDRESSALLOWED.RECORDSTATUS FROM ADDRESS.TABADDRESSALLOWED TABADDRESSALLOWED  ", "TabAddressAllowed", "Linked.Address")                 
                                                   
                                  
                      TabAddressAllowed.DataSource = .QueryExecuteToTable(True)
                 End With 
        End If     
    End Sub

    Protected Sub TabAddressAllowed_CellEditorInitialize(sender As Object, e As ASPxGridViewEditorEventArgs) Handles TabAddressAllowed.CellEditorInitialize
        If TabAddressAllowed.IsNewRowEditing Then
            Select Case e.Column.FieldName
                
                
                
                Case "COMPANYID"                     
                       e.Editor.Focus()               
            End Select

        Else

            Select Case e.Column.FieldName
                Case "COMPANYID"
     e.Editor.Enabled = False
Case "TYPEOFADDRESS"
     e.Editor.Enabled = False
Case "RECORDOWNERTYPE"
     e.Editor.Enabled = False
                   
                
                
                Case "RECORDSTATUS"                     
                     e.Editor.Focus()
            End Select
        End If
        
       Select Case e.Column.FieldName
           Case "COMPANYID"
                 DirectCast(e.Editor, ASPxComboBox).DataBindItems()
                 
           Case "TYPEOFADDRESS"
     DirectCast(e.Editor, ASPxComboBox).DataBindItems() 
Case "RECORDSTATUS"
     DirectCast(e.Editor, ASPxComboBox).DataBindItems() 

        End Select
    End Sub

    Protected Sub TabAddressAllowed_RowInserting(ByVal sender As Object, ByVal e As ASPxDataInsertingEventArgs) Handles TabAddressAllowed.RowInserting 
        Dim isNullResult As Boolean = True
        
             With New DataManagerFactory("INSERT INTO ADDRESS.TabAddressAllowed (COMPANYID, TYPEOFADDRESS, RECORDOWNERTYPE, RECORDSTATUS, CREATIONDATE, CREATORUSERCODE, UPDATEDATE, UPDATEUSERCODE) VALUES (@:COMPANYID, @:TYPEOFADDRESS, @:RECORDOWNERTYPE, @:RECORDSTATUS, SYSDATE, @:CREATORUSERCODE, SYSDATE, @:UPDATEUSERCODE)", "TabAddressAllowed", "Linked.Address")                 
                                                   
                       .AddParameter("COMPANYID", DbType.Decimal, 0, False, e.NewValues("COMPANYID"))
.AddParameter("TYPEOFADDRESS", DbType.Decimal, 0, False, e.NewValues("TYPEOFADDRESS"))
.AddParameter("RECORDOWNERTYPE", DbType.Decimal, 0, False, e.NewValues("RECORDOWNERTYPE"))
.AddParameter("RECORDSTATUS", DbType.AnsiString, 0, False, e.NewValues("RECORDSTATUS"))
.AddParameter("CREATORUSERCODE", DbType.Decimal, 0, False, Session("nUsercode"))
.AddParameter("UPDATEUSERCODE", DbType.Decimal, 0, False, Session("nUsercode"))
            
                       .CommandExecute()
                End With
               
        e.Cancel = True
        TabAddressAllowed.CancelEdit()
    End Sub

    Protected Sub TabAddressAllowed_RowUpdating(ByVal sender As Object, ByVal e As ASPxDataUpdatingEventArgs) Handles TabAddressAllowed.RowUpdating
        Dim isNullResult As Boolean = True
          
             With New DataManagerFactory("UPDATE ADDRESS.TabAddressAllowed SET RECORDSTATUS = @:RECORDSTATUS, UPDATEDATE = SYSDATE, UPDATEUSERCODE = @:UPDATEUSERCODE WHERE COMPANYID = @:COMPANYID AND TYPEOFADDRESS = @:TYPEOFADDRESS AND RECORDOWNERTYPE = @:RECORDOWNERTYPE", "TabAddressAllowed", "Linked.Address")                 
                                                   
                       .AddParameter("RECORDSTATUS", DbType.AnsiString, 0, False, e.NewValues("RECORDSTATUS"))
.AddParameter("UPDATEUSERCODE", DbType.Decimal, 0, False, Session("nUsercode"))
.AddParameter("COMPANYID", DbType.Decimal, 0, False, e.Keys("COMPANYID"))
.AddParameter("TYPEOFADDRESS", DbType.Decimal, 0, False, e.Keys("TYPEOFADDRESS"))
.AddParameter("RECORDOWNERTYPE", DbType.Decimal, 0, False, e.Keys("RECORDOWNERTYPE"))
            
                       .CommandExecute()
                End With         
      
         e.Cancel = True
        TabAddressAllowed.CancelEdit()
    End Sub
    
    Protected Sub TabAddressAllowed_CustomCallback(sender As Object, e As ASPxGridViewCustomCallbackEventArgs) Handles TabAddressAllowed.CustomCallback     
           Dim isNullResult As Boolean = True
           
           Select Case e.Parameters.ToString.ToLower
               Case "delete"
                       Dim COMPANYIDKey As Generic.List(Of Object) = TabAddressAllowed.GetSelectedFieldValues("COMPANYID")
 Dim TYPEOFADDRESSKey As Generic.List(Of Object) = TabAddressAllowed.GetSelectedFieldValues("TYPEOFADDRESS")
 Dim RECORDOWNERTYPEKey As Generic.List(Of Object) = TabAddressAllowed.GetSelectedFieldValues("RECORDOWNERTYPE")
        
               For index As Integer = 0 To COMPANYIDKey.Count - 1
                  With New DataManagerFactory("DELETE FROM ADDRESS.TabAddressAllowed WHERE COMPANYID = @:COMPANYID AND TYPEOFADDRESS = @:TYPEOFADDRESS AND RECORDOWNERTYPE = @:RECORDOWNERTYPE ", "TabAddressAllowed", "Linked.Address")                 
                                                   
               .AddParameter("COMPANYID", DbType.Decimal, 0, False, COMPANYIDKey(index))
.AddParameter("TYPEOFADDRESS", DbType.Decimal, 0, False, TYPEOFADDRESSKey(index))
.AddParameter("RECORDOWNERTYPE", DbType.Decimal, 0, False, RECORDOWNERTYPEKey(index))
            
               .CommandExecute()
          End With 
                       
              Next           
           
              TabAddressAllowed.DataBind()
                 
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
    
    Protected Sub TabAddressAllowed_RowValidating(sender As Object, e As ASPxDataValidationEventArgs) Handles TabAddressAllowed.RowValidating

        
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