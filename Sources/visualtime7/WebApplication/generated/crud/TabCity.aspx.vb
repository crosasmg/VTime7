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

Partial Class Maintenance_TabCity
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

#Region "TabCity Events"
    
    Protected Sub TabCity_CustomColumnDisplayText(sender As Object, e As DevExpress.Web.ASPxGridView.ASPxGridViewColumnDisplayTextEventArgs) Handles TabCity.CustomColumnDisplayText
          Dim data As DataTable
          Dim rows() As DataRow
           
          Select Case e.Column.FieldName
            Case "ZIPCODE"
                data = DirectCast(TabCity.Columns("ZIPCODE"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource 
                rows = data.Select(String.Format("COUNTRYID = {0} AND ZIPCODE = '{1}'", e.GetFieldValue("COUNTRYID"), e.Value)) 
                If rows.Count > 0 Then 
                    e.DisplayText = rows(0)("ZIPCODE") 
                End If 

               Case Else              
           End Select
    End Sub
  
    Protected Sub TabCity_DataBinding(ByVal sender As Object, ByVal e As EventArgs) Handles TabCity.DataBinding
        If (Not IsNothing(Request.Params("__CALLBACKID")) AndAlso Request.Params("__CALLBACKID").EndsWith("TabCity")) Or _internalCall Then
                       If Caching.Exist("TabCountry") Then
                DirectCast(TabCity.Columns("COUNTRYID"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = Caching.GetItem("TabCountry")

            Else
                Dim source As DataTable = Nothing

                With New DataManagerFactory("SELECT  TABCOUNTRY.COUNTRYID, TABCOUNTRY.RECORDSTATUS, TRANSCOUNTRY.LANGUAGEID, TRANSCOUNTRY.DESCRIPTION FROM COMMON.TABCOUNTRY TABCOUNTRY JOIN COMMON.TRANSCOUNTRY TRANSCOUNTRY ON TRANSCOUNTRY.COUNTRYID = TABCOUNTRY.COUNTRYID  WHERE TABCOUNTRY.RECORDSTATUS = '1' AND TRANSCOUNTRY.LANGUAGEID = @:LANGUAGEID ", "TabCountry", "Linked.Address")
                    .AddParameter("LANGUAGEID", DbType.Decimal, 5, False, CurrentState.Get("Language"))
 
                    source = .QueryExecuteToTable(True)
                    DirectCast(TabCity.Columns("COUNTRYID"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = source
                End With

                If Not IsNothing(source) AndAlso source.Rows.Count > 0 Then
                    Caching.SetItem("TabCountry", source)
                End If
            End If 
             If Caching.Exist("TabZipCode") Then
                DirectCast(TabCity.Columns("ZIPCODE"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = Caching.GetItem("TabZipCode")

            Else
                Dim source As DataTable = Nothing

                With New DataManagerFactory("SELECT  TABZIPCODE.COUNTRYID, TABZIPCODE.ZIPCODE, TABZIPCODE.RECORDSTATUS FROM ADDRESS.TABZIPCODE TABZIPCODE  WHERE TABZIPCODE.RECORDSTATUS = '1' ", "TabZipCode", "Linked.Address")
                     
                    source = .QueryExecuteToTable(True)
                    DirectCast(TabCity.Columns("ZIPCODE"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = source
                End With

                If Not IsNothing(source) AndAlso source.Rows.Count > 0 Then
                    Caching.SetItem("TabZipCode", source)
                End If
            End If 
             If Caching.Exist("EnumRecordStatus") Then
                DirectCast(TabCity.Columns("RECORDSTATUS"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = Caching.GetItem("EnumRecordStatus")

            Else
                Dim source As DataTable = Nothing

                With New DataManagerFactory("SELECT  ENUMRECORDSTATUS.RECORDSTATUS, ETRANRECORDSTATUS.LANGUAGEID, ETRANRECORDSTATUS.DESCRIPTION FROM COMMON.ENUMRECORDSTATUS ENUMRECORDSTATUS JOIN COMMON.ETRANRECORDSTATUS ETRANRECORDSTATUS ON ETRANRECORDSTATUS.RECORDSTATUS = ENUMRECORDSTATUS.RECORDSTATUS  WHERE ETRANRECORDSTATUS.LANGUAGEID = @:LANGUAGEID ", "EnumRecordStatus", "Linked.Address")
                    .AddParameter("LANGUAGEID", DbType.Decimal, 5, False, CurrentState.Get("Language"))
 
                    source = .QueryExecuteToTable(True)
                    DirectCast(TabCity.Columns("RECORDSTATUS"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = source
                End With

                If Not IsNothing(source) AndAlso source.Rows.Count > 0 Then
                    Caching.SetItem("EnumRecordStatus", source)
                End If
            End If 
             If Caching.Exist("TabCityTax") Then
                DirectCast(TabCity.Columns("CITYTAXCODE"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = Caching.GetItem("TabCityTax")

            Else
                Dim source As DataTable = Nothing

                With New DataManagerFactory("SELECT  TABCITYTAX.CITYTAXCODE, TABCITYTAX.RECORDSTATUS FROM ADDRESS.TABCITYTAX TABCITYTAX  WHERE TABCITYTAX.RECORDSTATUS = '1' ", "TabCityTax", "Linked.Address")
                     
                    source = .QueryExecuteToTable(True)
                    DirectCast(TabCity.Columns("CITYTAXCODE"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = source
                End With

                If Not IsNothing(source) AndAlso source.Rows.Count > 0 Then
                    Caching.SetItem("TabCityTax", source)
                End If
            End If 
 

                 With New DataManagerFactory("SELECT  TABCITY.COUNTRYID, TABCITY.ZIPCODE, TABCITY.CITYID, TABCITY.RECORDSTATUS, TABCITY.CITYTAXCODE FROM ADDRESS.TABCITY TABCITY  ", "TabCity", "Linked.Address")                 
                                                   
                                  
                      TabCity.DataSource = .QueryExecuteToTable(True)
                 End With 
        End If     
    End Sub

    Protected Sub TabCity_CellEditorInitialize(sender As Object, e As ASPxGridViewEditorEventArgs) Handles TabCity.CellEditorInitialize
        If TabCity.IsNewRowEditing Then
            Select Case e.Column.FieldName
                
                Case "ZIPCODE"
     AddHandler DirectCast(e.Editor, ASPxComboBox).Callback, AddressOf ZipCode_OnCallback 

                
                Case "COUNTRYID"                     
                       e.Editor.Focus()               
            End Select

        Else

            Select Case e.Column.FieldName
                Case "COUNTRYID"
     e.Editor.Enabled = False
Case "ZIPCODE"
     e.Editor.Enabled = False
     ZipCode_Fill(e.Editor, TabCity.GetRowValues(e.VisibleIndex, "COUNTRYID")) 
     AddHandler DirectCast(e.Editor, ASPxComboBox).Callback, AddressOf ZipCode_OnCallback 
Case "CITYID"
     e.Editor.Enabled = False
                   
                
                
                Case "RECORDSTATUS"                     
                     e.Editor.Focus()
            End Select
        End If
        
       Select Case e.Column.FieldName
           Case "COUNTRYID"
                 DirectCast(e.Editor, ASPxComboBox).DataBindItems()
                 
           Case "ZIPCODE"
     DirectCast(e.Editor, ASPxComboBox).DataBindItems() 
Case "RECORDSTATUS"
     DirectCast(e.Editor, ASPxComboBox).DataBindItems() 
Case "CITYTAXCODE"
     DirectCast(e.Editor, ASPxComboBox).DataBindItems() 

        End Select
    End Sub

    Protected Sub TabCity_RowInserting(ByVal sender As Object, ByVal e As ASPxDataInsertingEventArgs) Handles TabCity.RowInserting 
        Dim isNullResult As Boolean = True
        
             With New DataManagerFactory("INSERT INTO ADDRESS.TabCity (COUNTRYID, ZIPCODE, CITYID, RECORDSTATUS, CITYTAXCODE, CREATIONDATE, CREATORUSERCODE, UPDATEDATE, UPDATEUSERCODE) VALUES (@:COUNTRYID, @:ZIPCODE, @:CITYID, @:RECORDSTATUS, @:CITYTAXCODE, SYSDATE, @:CREATORUSERCODE, SYSDATE, @:UPDATEUSERCODE)", "TabCity", "Linked.Address")                 
                                                   
                       .AddParameter("COUNTRYID", DbType.Decimal, 0, False, e.NewValues("COUNTRYID"))
.AddParameter("ZIPCODE", DbType.AnsiString, 0, False, e.NewValues("ZIPCODE"))
.AddParameter("CITYID", DbType.AnsiString, 0, False, e.NewValues("CITYID"))
.AddParameter("RECORDSTATUS", DbType.AnsiString, 0, (e.NewValues("RECORDSTATUS") = String.Empty), e.NewValues("RECORDSTATUS"))
.AddParameter("CITYTAXCODE", DbType.Decimal, 0, (e.NewValues("CITYTAXCODE") = 0), e.NewValues("CITYTAXCODE"))
.AddParameter("CREATORUSERCODE", DbType.Decimal, 0, False, Session("nUsercode"))
.AddParameter("UPDATEUSERCODE", DbType.Decimal, 0, False, Session("nUsercode"))
            
                       .CommandExecute()
                End With
               
        e.Cancel = True
        TabCity.CancelEdit()
    End Sub

    Protected Sub TabCity_RowUpdating(ByVal sender As Object, ByVal e As ASPxDataUpdatingEventArgs) Handles TabCity.RowUpdating
        Dim isNullResult As Boolean = True
          
             With New DataManagerFactory("UPDATE ADDRESS.TabCity SET RECORDSTATUS = @:RECORDSTATUS, CITYTAXCODE = @:CITYTAXCODE, UPDATEDATE = SYSDATE, UPDATEUSERCODE = @:UPDATEUSERCODE WHERE COUNTRYID = @:COUNTRYID AND ZIPCODE = @:ZIPCODE AND CITYID = @:CITYID", "TabCity", "Linked.Address")                 
                                                   
                       .AddParameter("RECORDSTATUS", DbType.AnsiString, 0, (e.NewValues("RECORDSTATUS") = String.Empty), e.NewValues("RECORDSTATUS"))
.AddParameter("CITYTAXCODE", DbType.Decimal, 0, (e.NewValues("CITYTAXCODE") = 0), e.NewValues("CITYTAXCODE"))
.AddParameter("UPDATEUSERCODE", DbType.Decimal, 0, False, Session("nUsercode"))
.AddParameter("COUNTRYID", DbType.Decimal, 0, False, e.Keys("COUNTRYID"))
.AddParameter("ZIPCODE", DbType.AnsiString, 0, False, e.Keys("ZIPCODE"))
.AddParameter("CITYID", DbType.AnsiString, 0, False, e.Keys("CITYID"))
            
                       .CommandExecute()
                End With         
      
         e.Cancel = True
        TabCity.CancelEdit()
    End Sub
    
    Protected Sub TabCity_CustomCallback(sender As Object, e As ASPxGridViewCustomCallbackEventArgs) Handles TabCity.CustomCallback     
           Dim isNullResult As Boolean = True
           
           Select Case e.Parameters.ToString.ToLower
               Case "delete"
                       Dim COUNTRYIDKey As Generic.List(Of Object) = TabCity.GetSelectedFieldValues("COUNTRYID")
 Dim ZIPCODEKey As Generic.List(Of Object) = TabCity.GetSelectedFieldValues("ZIPCODE")
 Dim CITYIDKey As Generic.List(Of Object) = TabCity.GetSelectedFieldValues("CITYID")
        
               For index As Integer = 0 To COUNTRYIDKey.Count - 1
                  With New DataManagerFactory("DELETE FROM ADDRESS.TabCity WHERE COUNTRYID = @:COUNTRYID AND ZIPCODE = @:ZIPCODE AND CITYID = @:CITYID ", "TabCity", "Linked.Address")                 
                                                   
               .AddParameter("COUNTRYID", DbType.Decimal, 0, False, COUNTRYIDKey(index))
.AddParameter("ZIPCODE", DbType.AnsiString, 0, False, ZIPCODEKey(index))
.AddParameter("CITYID", DbType.AnsiString, 0, False, CITYIDKey(index))
            
               .CommandExecute()
          End With 
                       
              Next           
           
              TabCity.DataBind()
                 
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
    
    Protected Sub TabCity_RowValidating(sender As Object, e As ASPxDataValidationEventArgs) Handles TabCity.RowValidating

        
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
 

#Region "ZipCode Events"

    Private Sub ZipCode_OnCallback(ByVal source As Object, ByVal e As CallbackEventArgsBase)
        ZipCode_Fill(DirectCast(source, ASPxComboBox), e.Parameter)
    End Sub

    Private Sub ZipCode_Fill(control As ASPxComboBox, CountryID As Integer)
          With New DataManagerFactory("SELECT  TABZIPCODE.COUNTRYID, TABZIPCODE.ZIPCODE, TABZIPCODE.RECORDSTATUS FROM ADDRESS.TABZIPCODE TABZIPCODE  WHERE TABZIPCODE.RECORDSTATUS = '1'  AND TABZIPCODE.COUNTRYID = @:DependencyTABZIPCODECOUNT", "TabZipCode", "Linked.Address")                 
                                                   
               .AddParameter("DependencyTabZipCodeCount", DbType.Int32, 0, False, CountryID)
  
               
               control.DataSource = .QueryExecuteToTable(True)
               control.DataBindItems()
          End With         
    End Sub
    
#End Region 
 
 
End Class