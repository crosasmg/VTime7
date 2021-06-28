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

Partial Class Maintenance_TabPartsOfAddressNames
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

#Region "TabPartsOfAddressNames Events"
    
    Protected Sub TabPartsOfAddressNames_CustomColumnDisplayText(sender As Object, e As DevExpress.Web.ASPxGridView.ASPxGridViewColumnDisplayTextEventArgs) Handles TabPartsOfAddressNames.CustomColumnDisplayText
          Dim data As DataTable
          Dim rows() As DataRow
           
          Select Case e.Column.FieldName

               Case Else              
           End Select
    End Sub
  
    Protected Sub TabPartsOfAddressNames_DataBinding(ByVal sender As Object, ByVal e As EventArgs) Handles TabPartsOfAddressNames.DataBinding
        If (Not IsNothing(Request.Params("__CALLBACKID")) AndAlso Request.Params("__CALLBACKID").EndsWith("TabPartsOfAddressNames")) Or _internalCall Then
                       If Caching.Exist("TabCountry") Then
                DirectCast(TabPartsOfAddressNames.Columns("COUNTRYID"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = Caching.GetItem("TabCountry")

            Else
                Dim source As DataTable = Nothing

                With New DataManagerFactory("SELECT  TABCOUNTRY.COUNTRYID, TABCOUNTRY.RECORDSTATUS, TRANSCOUNTRY.LANGUAGEID, TRANSCOUNTRY.DESCRIPTION FROM COMMON.TABCOUNTRY TABCOUNTRY JOIN COMMON.TRANSCOUNTRY TRANSCOUNTRY ON TRANSCOUNTRY.COUNTRYID = TABCOUNTRY.COUNTRYID  WHERE TABCOUNTRY.RECORDSTATUS = '1' AND TRANSCOUNTRY.LANGUAGEID = @:LANGUAGEID ", "TabCountry", "Linked.Address")
                    .AddParameter("LANGUAGEID", DbType.Decimal, 5, False, CurrentState.Get("Language"))
 
                    source = .QueryExecuteToTable(True)
                    DirectCast(TabPartsOfAddressNames.Columns("COUNTRYID"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = source
                End With

                If Not IsNothing(source) AndAlso source.Rows.Count > 0 Then
                    Caching.SetItem("TabCountry", source)
                End If
            End If 
             If Caching.Exist("EnumTypeOfRoute") Then
                DirectCast(TabPartsOfAddressNames.Columns("TYPEOFROUTE"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = Caching.GetItem("EnumTypeOfRoute")

            Else
                Dim source As DataTable = Nothing

                With New DataManagerFactory("SELECT  ENUMTYPEOFROUTE.TYPEOFROUTE, ENUMTYPEOFROUTE.RECORDSTATUS, ETRANTYPEOFROUTE.LANGUAGEID, ETRANTYPEOFROUTE.DESCRIPTION FROM ADDRESS.ENUMTYPEOFROUTE ENUMTYPEOFROUTE JOIN ADDRESS.ETRANTYPEOFROUTE ETRANTYPEOFROUTE ON ETRANTYPEOFROUTE.TYPEOFROUTE = ENUMTYPEOFROUTE.TYPEOFROUTE  WHERE ENUMTYPEOFROUTE.RECORDSTATUS = '1' AND ETRANTYPEOFROUTE.LANGUAGEID = @:LANGUAGEID ", "EnumTypeOfRoute", "Linked.Address")
                    .AddParameter("LANGUAGEID", DbType.Decimal, 5, False, CurrentState.Get("Language"))
 
                    source = .QueryExecuteToTable(True)
                    DirectCast(TabPartsOfAddressNames.Columns("TYPEOFROUTE"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = source
                End With

                If Not IsNothing(source) AndAlso source.Rows.Count > 0 Then
                    Caching.SetItem("EnumTypeOfRoute", source)
                End If
            End If 
             If Caching.Exist("EnumRecordStatus") Then
                DirectCast(TabPartsOfAddressNames.Columns("RECORDSTATUS"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = Caching.GetItem("EnumRecordStatus")

            Else
                Dim source As DataTable = Nothing

                With New DataManagerFactory("SELECT  ENUMRECORDSTATUS.RECORDSTATUS, ETRANRECORDSTATUS.LANGUAGEID, ETRANRECORDSTATUS.DESCRIPTION FROM COMMON.ENUMRECORDSTATUS ENUMRECORDSTATUS JOIN COMMON.ETRANRECORDSTATUS ETRANRECORDSTATUS ON ETRANRECORDSTATUS.RECORDSTATUS = ENUMRECORDSTATUS.RECORDSTATUS  WHERE ETRANRECORDSTATUS.LANGUAGEID = @:LANGUAGEID ", "EnumRecordStatus", "Linked.Address")
                    .AddParameter("LANGUAGEID", DbType.Decimal, 5, False, CurrentState.Get("Language"))
 
                    source = .QueryExecuteToTable(True)
                    DirectCast(TabPartsOfAddressNames.Columns("RECORDSTATUS"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = source
                End With

                If Not IsNothing(source) AndAlso source.Rows.Count > 0 Then
                    Caching.SetItem("EnumRecordStatus", source)
                End If
            End If 
 

                 With New DataManagerFactory("SELECT  TABPARTSOFADDRESSNAMES.COUNTRYID, TABPARTSOFADDRESSNAMES.TYPEOFROUTE, TABPARTSOFADDRESSNAMES.PARTNAMEID, TABPARTSOFADDRESSNAMES.RECORDSTATUS, TABPARTSOFADDRESSNAMES.LINEATWHICHMUSTBESHOWN FROM ADDRESS.TABPARTSOFADDRESSNAMES TABPARTSOFADDRESSNAMES  ", "TabPartsOfAddressNames", "Linked.Address")                 
                                                   
                                  
                      TabPartsOfAddressNames.DataSource = .QueryExecuteToTable(True)
                 End With 
        End If     
    End Sub

    Protected Sub TabPartsOfAddressNames_CellEditorInitialize(sender As Object, e As ASPxGridViewEditorEventArgs) Handles TabPartsOfAddressNames.CellEditorInitialize
        If TabPartsOfAddressNames.IsNewRowEditing Then
            Select Case e.Column.FieldName
                
                
                
                Case "COUNTRYID"                     
                       e.Editor.Focus()               
            End Select

        Else

            Select Case e.Column.FieldName
                Case "COUNTRYID"
     e.Editor.Enabled = False
Case "TYPEOFROUTE"
     e.Editor.Enabled = False
Case "PARTNAMEID"
     e.Editor.Enabled = False
                   
                
                
                Case "RECORDSTATUS"                     
                     e.Editor.Focus()
            End Select
        End If
        
       Select Case e.Column.FieldName
           Case "COUNTRYID"
                 DirectCast(e.Editor, ASPxComboBox).DataBindItems()
                 
           Case "TYPEOFROUTE"
     DirectCast(e.Editor, ASPxComboBox).DataBindItems() 
Case "RECORDSTATUS"
     DirectCast(e.Editor, ASPxComboBox).DataBindItems() 

        End Select
    End Sub

    Protected Sub TabPartsOfAddressNames_RowInserting(ByVal sender As Object, ByVal e As ASPxDataInsertingEventArgs) Handles TabPartsOfAddressNames.RowInserting 
        Dim isNullResult As Boolean = True
        
             With New DataManagerFactory("INSERT INTO ADDRESS.TabPartsOfAddressNames (COUNTRYID, TYPEOFROUTE, PARTNAMEID, RECORDSTATUS, LINEATWHICHMUSTBESHOWN, CREATIONDATE, CREATORUSERCODE, UPDATEDATE, UPDATEUSERCODE) VALUES (@:COUNTRYID, @:TYPEOFROUTE, @:PARTNAMEID, @:RECORDSTATUS, @:LINEATWHICHMUSTBESHOWN, SYSDATE, @:CREATORUSERCODE, SYSDATE, @:UPDATEUSERCODE)", "TabPartsOfAddressNames", "Linked.Address")                 
                                                   
                       .AddParameter("COUNTRYID", DbType.Decimal, 0, False, e.NewValues("COUNTRYID"))
.AddParameter("TYPEOFROUTE", DbType.Decimal, 0, False, e.NewValues("TYPEOFROUTE"))
.AddParameter("PARTNAMEID", DbType.Decimal, 0, False, e.NewValues("PARTNAMEID"))
.AddParameter("RECORDSTATUS", DbType.AnsiString, 0, (e.NewValues("RECORDSTATUS") = String.Empty), e.NewValues("RECORDSTATUS"))
.AddParameter("LINEATWHICHMUSTBESHOWN", DbType.Decimal, 0, (e.NewValues("LINEATWHICHMUSTBESHOWN") = 0), e.NewValues("LINEATWHICHMUSTBESHOWN"))
.AddParameter("CREATORUSERCODE", DbType.Decimal, 0, False, Session("nUsercode"))
.AddParameter("UPDATEUSERCODE", DbType.Decimal, 0, False, Session("nUsercode"))
            
                       .CommandExecute()
                End With
               
        e.Cancel = True
        TabPartsOfAddressNames.CancelEdit()
    End Sub

    Protected Sub TabPartsOfAddressNames_RowUpdating(ByVal sender As Object, ByVal e As ASPxDataUpdatingEventArgs) Handles TabPartsOfAddressNames.RowUpdating
        Dim isNullResult As Boolean = True
          
             With New DataManagerFactory("UPDATE ADDRESS.TabPartsOfAddressNames SET RECORDSTATUS = @:RECORDSTATUS, LINEATWHICHMUSTBESHOWN = @:LINEATWHICHMUSTBESHOWN, UPDATEDATE = SYSDATE, UPDATEUSERCODE = @:UPDATEUSERCODE WHERE COUNTRYID = @:COUNTRYID AND TYPEOFROUTE = @:TYPEOFROUTE AND PARTNAMEID = @:PARTNAMEID", "TabPartsOfAddressNames", "Linked.Address")                 
                                                   
                       .AddParameter("RECORDSTATUS", DbType.AnsiString, 0, (e.NewValues("RECORDSTATUS") = String.Empty), e.NewValues("RECORDSTATUS"))
.AddParameter("LINEATWHICHMUSTBESHOWN", DbType.Decimal, 0, (e.NewValues("LINEATWHICHMUSTBESHOWN") = 0), e.NewValues("LINEATWHICHMUSTBESHOWN"))
.AddParameter("UPDATEUSERCODE", DbType.Decimal, 0, False, Session("nUsercode"))
.AddParameter("COUNTRYID", DbType.Decimal, 0, False, e.Keys("COUNTRYID"))
.AddParameter("TYPEOFROUTE", DbType.Decimal, 0, False, e.Keys("TYPEOFROUTE"))
.AddParameter("PARTNAMEID", DbType.Decimal, 0, False, e.Keys("PARTNAMEID"))
            
                       .CommandExecute()
                End With         
      
         e.Cancel = True
        TabPartsOfAddressNames.CancelEdit()
    End Sub
    
    Protected Sub TabPartsOfAddressNames_CustomCallback(sender As Object, e As ASPxGridViewCustomCallbackEventArgs) Handles TabPartsOfAddressNames.CustomCallback     
           Dim isNullResult As Boolean = True
           
           Select Case e.Parameters.ToString.ToLower
               Case "delete"
                       Dim COUNTRYIDKey As Generic.List(Of Object) = TabPartsOfAddressNames.GetSelectedFieldValues("COUNTRYID")
 Dim TYPEOFROUTEKey As Generic.List(Of Object) = TabPartsOfAddressNames.GetSelectedFieldValues("TYPEOFROUTE")
 Dim PARTNAMEIDKey As Generic.List(Of Object) = TabPartsOfAddressNames.GetSelectedFieldValues("PARTNAMEID")
        
               For index As Integer = 0 To COUNTRYIDKey.Count - 1
                  With New DataManagerFactory("DELETE FROM ADDRESS.TabPartsOfAddressNames WHERE COUNTRYID = @:COUNTRYID AND TYPEOFROUTE = @:TYPEOFROUTE AND PARTNAMEID = @:PARTNAMEID ", "TabPartsOfAddressNames", "Linked.Address")                 
                                                   
               .AddParameter("COUNTRYID", DbType.Decimal, 0, False, COUNTRYIDKey(index))
.AddParameter("TYPEOFROUTE", DbType.Decimal, 0, False, TYPEOFROUTEKey(index))
.AddParameter("PARTNAMEID", DbType.Decimal, 0, False, PARTNAMEIDKey(index))
            
               .CommandExecute()
          End With 
                       
              Next           
           
              TabPartsOfAddressNames.DataBind()
                 
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
    
    Protected Sub TabPartsOfAddressNames_RowValidating(sender As Object, e As ASPxDataValidationEventArgs) Handles TabPartsOfAddressNames.RowValidating

        
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