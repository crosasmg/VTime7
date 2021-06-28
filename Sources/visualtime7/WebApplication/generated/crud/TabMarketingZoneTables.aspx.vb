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

Partial Class Maintenance_TabMarketingZoneTables
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

#Region "TabMarketingZoneTables Events"
    
    Protected Sub TabMarketingZoneTables_CustomColumnDisplayText(sender As Object, e As DevExpress.Web.ASPxGridView.ASPxGridViewColumnDisplayTextEventArgs) Handles TabMarketingZoneTables.CustomColumnDisplayText
          Dim data As DataTable
          Dim rows() As DataRow
           
          Select Case e.Column.FieldName

               Case Else              
           End Select
    End Sub
  
    Protected Sub TabMarketingZoneTables_DataBinding(ByVal sender As Object, ByVal e As EventArgs) Handles TabMarketingZoneTables.DataBinding
        If (Not IsNothing(Request.Params("__CALLBACKID")) AndAlso Request.Params("__CALLBACKID").EndsWith("TabMarketingZoneTables")) Or _internalCall Then
                       If Caching.Exist("TabCompany") Then
                DirectCast(TabMarketingZoneTables.Columns("COMPANYID"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = Caching.GetItem("TabCompany")

            Else
                Dim source As DataTable = Nothing

                With New DataManagerFactory("SELECT  TABCOMPANY.COMPANYID, TABCOMPANY.RECORDSTATUS, TRANSCOMPANY.LANGUAGEID, TRANSCOMPANY.DESCRIPTION FROM COMMON.TABCOMPANY TABCOMPANY JOIN COMMON.TRANSCOMPANY TRANSCOMPANY ON TRANSCOMPANY.COMPANYID = TABCOMPANY.COMPANYID  WHERE TABCOMPANY.RECORDSTATUS = '1' AND TRANSCOMPANY.LANGUAGEID = @:LANGUAGEID ", "TabCompany", "Linked.Address")
                    .AddParameter("LANGUAGEID", DbType.Decimal, 5, False, CurrentState.Get("Language"))
 
                    source = .QueryExecuteToTable(True)
                    DirectCast(TabMarketingZoneTables.Columns("COMPANYID"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = source
                End With

                If Not IsNothing(source) AndAlso source.Rows.Count > 0 Then
                    Caching.SetItem("TabCompany", source)
                End If
            End If 
             If Caching.Exist("EnumRecordStatus") Then
                DirectCast(TabMarketingZoneTables.Columns("RECORDSTATUS"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = Caching.GetItem("EnumRecordStatus")

            Else
                Dim source As DataTable = Nothing

                With New DataManagerFactory("SELECT  ENUMRECORDSTATUS.RECORDSTATUS, ETRANRECORDSTATUS.LANGUAGEID, ETRANRECORDSTATUS.DESCRIPTION FROM COMMON.ENUMRECORDSTATUS ENUMRECORDSTATUS JOIN COMMON.ETRANRECORDSTATUS ETRANRECORDSTATUS ON ETRANRECORDSTATUS.RECORDSTATUS = ENUMRECORDSTATUS.RECORDSTATUS  WHERE ETRANRECORDSTATUS.LANGUAGEID = @:LANGUAGEID ", "EnumRecordStatus", "Linked.Address")
                    .AddParameter("LANGUAGEID", DbType.Decimal, 5, False, CurrentState.Get("Language"))
 
                    source = .QueryExecuteToTable(True)
                    DirectCast(TabMarketingZoneTables.Columns("RECORDSTATUS"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = source
                End With

                If Not IsNothing(source) AndAlso source.Rows.Count > 0 Then
                    Caching.SetItem("EnumRecordStatus", source)
                End If
            End If 
 

                 With New DataManagerFactory("SELECT  TABMARKETINGZONETABLES.COMPANYID, TABMARKETINGZONETABLES.MARKETINGZONETABLEID, TABMARKETINGZONETABLES.RECORDSTATUS, TABMARKETINGZONETABLES.CANCELLATIONDATE, TABMARKETINGZONETABLES.EFFECTIVEDATE FROM ADDRESS.TABMARKETINGZONETABLES TABMARKETINGZONETABLES  ", "TabMarketingZoneTables", "Linked.Address")                 
                                                   
                                  
                      TabMarketingZoneTables.DataSource = .QueryExecuteToTable(True)
                 End With 
        End If     
    End Sub

    Protected Sub TabMarketingZoneTables_CellEditorInitialize(sender As Object, e As ASPxGridViewEditorEventArgs) Handles TabMarketingZoneTables.CellEditorInitialize
        If TabMarketingZoneTables.IsNewRowEditing Then
            Select Case e.Column.FieldName
                
                
                
                Case "COMPANYID"                     
                       e.Editor.Focus()               
            End Select

        Else

            Select Case e.Column.FieldName
                Case "COMPANYID"
     e.Editor.Enabled = False
Case "MARKETINGZONETABLEID"
     e.Editor.Enabled = False
                   
                
                
                Case "RECORDSTATUS"                     
                     e.Editor.Focus()
            End Select
        End If
        
       Select Case e.Column.FieldName
           Case "COMPANYID"
                 DirectCast(e.Editor, ASPxComboBox).DataBindItems()
                 
           Case "RECORDSTATUS"
     DirectCast(e.Editor, ASPxComboBox).DataBindItems() 

        End Select
    End Sub

    Protected Sub TabMarketingZoneTables_RowInserting(ByVal sender As Object, ByVal e As ASPxDataInsertingEventArgs) Handles TabMarketingZoneTables.RowInserting 
        Dim isNullResult As Boolean = True
        
             With New DataManagerFactory("INSERT INTO ADDRESS.TabMarketingZoneTables (COMPANYID, MARKETINGZONETABLEID, RECORDSTATUS, CANCELLATIONDATE, EFFECTIVEDATE, CREATIONDATE, CREATORUSERCODE, UPDATEDATE, UPDATEUSERCODE) VALUES (@:COMPANYID, @:MARKETINGZONETABLEID, @:RECORDSTATUS, @:CANCELLATIONDATE, @:EFFECTIVEDATE, SYSDATE, @:CREATORUSERCODE, SYSDATE, @:UPDATEUSERCODE)", "TabMarketingZoneTables", "Linked.Address")                 
                                                   
                       .AddParameter("COMPANYID", DbType.Decimal, 0, False, e.NewValues("COMPANYID"))
.AddParameter("MARKETINGZONETABLEID", DbType.Decimal, 0, False, e.NewValues("MARKETINGZONETABLEID"))
.AddParameter("RECORDSTATUS", DbType.AnsiString, 0, False, e.NewValues("RECORDSTATUS"))
.AddParameter("CANCELLATIONDATE", DbType.DateTime, 0, (e.NewValues("CANCELLATIONDATE") = Date.MinValue), e.NewValues("CANCELLATIONDATE"))
.AddParameter("EFFECTIVEDATE", DbType.DateTime, 0, False, e.NewValues("EFFECTIVEDATE"))
.AddParameter("CREATORUSERCODE", DbType.Decimal, 0, False, Session("nUsercode"))
.AddParameter("UPDATEUSERCODE", DbType.Decimal, 0, False, Session("nUsercode"))
            
                       .CommandExecute()
                End With
               
        e.Cancel = True
        TabMarketingZoneTables.CancelEdit()
    End Sub

    Protected Sub TabMarketingZoneTables_RowUpdating(ByVal sender As Object, ByVal e As ASPxDataUpdatingEventArgs) Handles TabMarketingZoneTables.RowUpdating
        Dim isNullResult As Boolean = True
          
             With New DataManagerFactory("UPDATE ADDRESS.TabMarketingZoneTables SET RECORDSTATUS = @:RECORDSTATUS, CANCELLATIONDATE = @:CANCELLATIONDATE, EFFECTIVEDATE = @:EFFECTIVEDATE, UPDATEDATE = SYSDATE, UPDATEUSERCODE = @:UPDATEUSERCODE WHERE COMPANYID = @:COMPANYID AND MARKETINGZONETABLEID = @:MARKETINGZONETABLEID", "TabMarketingZoneTables", "Linked.Address")                 
                                                   
                       .AddParameter("RECORDSTATUS", DbType.AnsiString, 0, False, e.NewValues("RECORDSTATUS"))
.AddParameter("CANCELLATIONDATE", DbType.DateTime, 0, (e.NewValues("CANCELLATIONDATE") = Date.MinValue), e.NewValues("CANCELLATIONDATE"))
.AddParameter("EFFECTIVEDATE", DbType.DateTime, 0, False, e.NewValues("EFFECTIVEDATE"))
.AddParameter("UPDATEUSERCODE", DbType.Decimal, 0, False, Session("nUsercode"))
.AddParameter("COMPANYID", DbType.Decimal, 0, False, e.Keys("COMPANYID"))
.AddParameter("MARKETINGZONETABLEID", DbType.Decimal, 0, False, e.Keys("MARKETINGZONETABLEID"))
            
                       .CommandExecute()
                End With         
      
         e.Cancel = True
        TabMarketingZoneTables.CancelEdit()
    End Sub
    
    Protected Sub TabMarketingZoneTables_CustomCallback(sender As Object, e As ASPxGridViewCustomCallbackEventArgs) Handles TabMarketingZoneTables.CustomCallback     
           Dim isNullResult As Boolean = True
           
           Select Case e.Parameters.ToString.ToLower
               Case "delete"
                       Dim COMPANYIDKey As Generic.List(Of Object) = TabMarketingZoneTables.GetSelectedFieldValues("COMPANYID")
 Dim MARKETINGZONETABLEIDKey As Generic.List(Of Object) = TabMarketingZoneTables.GetSelectedFieldValues("MARKETINGZONETABLEID")
        
               For index As Integer = 0 To COMPANYIDKey.Count - 1
                  With New DataManagerFactory("DELETE FROM ADDRESS.TabMarketingZoneTables WHERE COMPANYID = @:COMPANYID AND MARKETINGZONETABLEID = @:MARKETINGZONETABLEID ", "TabMarketingZoneTables", "Linked.Address")                 
                                                   
               .AddParameter("COMPANYID", DbType.Decimal, 0, False, COMPANYIDKey(index))
.AddParameter("MARKETINGZONETABLEID", DbType.Decimal, 0, False, MARKETINGZONETABLEIDKey(index))
            
               .CommandExecute()
          End With 
                       
              Next           
           
              TabMarketingZoneTables.DataBind()
                 
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
    
    Protected Sub TabMarketingZoneTables_RowValidating(sender As Object, e As ASPxDataValidationEventArgs) Handles TabMarketingZoneTables.RowValidating

        
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