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

Partial Class Maintenance_TabMarketingZoneRelation
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

#Region "TabMarketingZoneRelation Events"
    
    Protected Sub TabMarketingZoneRelation_CustomColumnDisplayText(sender As Object, e As DevExpress.Web.ASPxGridView.ASPxGridViewColumnDisplayTextEventArgs) Handles TabMarketingZoneRelation.CustomColumnDisplayText
          Dim data As DataTable
          Dim rows() As DataRow
           
          Select Case e.Column.FieldName
            Case "MARKETINGZONEID"
                data = DirectCast(TabMarketingZoneRelation.Columns("MARKETINGZONEID"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource 
                rows = data.Select(String.Format("COUNTRYID = {0} AND MARKETINGZONEID = '{1}'", e.GetFieldValue("COUNTRYID"), e.Value)) 
                If rows.Count > 0 Then 
                    e.DisplayText = rows(0)("DESCRIPTION") 
                End If 
            Case "ZIPCODE"
                data = DirectCast(TabMarketingZoneRelation.Columns("ZIPCODE"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource 
                rows = data.Select(String.Format("COUNTRYID = {0} AND ZIPCODE = '{1}'", e.GetFieldValue("COUNTRYID"), e.Value)) 
                If rows.Count > 0 Then 
                    e.DisplayText = rows(0)("ZIPCODE") 
                End If 

               Case Else              
           End Select
    End Sub
  
    Protected Sub TabMarketingZoneRelation_DataBinding(ByVal sender As Object, ByVal e As EventArgs) Handles TabMarketingZoneRelation.DataBinding
        If (Not IsNothing(Request.Params("__CALLBACKID")) AndAlso Request.Params("__CALLBACKID").EndsWith("TabMarketingZoneRelation")) Or _internalCall Then
                       If Caching.Exist("TabCompany") Then
                DirectCast(TabMarketingZoneRelation.Columns("COMPANYID"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = Caching.GetItem("TabCompany")

            Else
                Dim source As DataTable = Nothing

                With New DataManagerFactory("SELECT  TABCOMPANY.COMPANYID, TABCOMPANY.RECORDSTATUS, TRANSCOMPANY.LANGUAGEID, TRANSCOMPANY.DESCRIPTION FROM COMMON.TABCOMPANY TABCOMPANY JOIN COMMON.TRANSCOMPANY TRANSCOMPANY ON TRANSCOMPANY.COMPANYID = TABCOMPANY.COMPANYID  WHERE TABCOMPANY.RECORDSTATUS = '1' AND TRANSCOMPANY.LANGUAGEID = @:LANGUAGEID ", "TabCompany", "Linked.Address")
                    .AddParameter("LANGUAGEID", DbType.Decimal, 5, False, CurrentState.Get("Language"))
 
                    source = .QueryExecuteToTable(True)
                    DirectCast(TabMarketingZoneRelation.Columns("COMPANYID"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = source
                End With

                If Not IsNothing(source) AndAlso source.Rows.Count > 0 Then
                    Caching.SetItem("TabCompany", source)
                End If
            End If 
             If Caching.Exist("TabCountry") Then
                DirectCast(TabMarketingZoneRelation.Columns("COUNTRYID"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = Caching.GetItem("TabCountry")

            Else
                Dim source As DataTable = Nothing

                With New DataManagerFactory("SELECT  TABCOUNTRY.COUNTRYID, TABCOUNTRY.RECORDSTATUS, TRANSCOUNTRY.LANGUAGEID, TRANSCOUNTRY.DESCRIPTION FROM COMMON.TABCOUNTRY TABCOUNTRY JOIN COMMON.TRANSCOUNTRY TRANSCOUNTRY ON TRANSCOUNTRY.COUNTRYID = TABCOUNTRY.COUNTRYID  WHERE TABCOUNTRY.RECORDSTATUS = '1' AND TRANSCOUNTRY.LANGUAGEID = @:LANGUAGEID ", "TabCountry", "Linked.Address")
                    .AddParameter("LANGUAGEID", DbType.Decimal, 5, False, CurrentState.Get("Language"))
 
                    source = .QueryExecuteToTable(True)
                    DirectCast(TabMarketingZoneRelation.Columns("COUNTRYID"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = source
                End With

                If Not IsNothing(source) AndAlso source.Rows.Count > 0 Then
                    Caching.SetItem("TabCountry", source)
                End If
            End If 
             If Caching.Exist("TabMarketingZone") Then
                DirectCast(TabMarketingZoneRelation.Columns("MARKETINGZONEID"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = Caching.GetItem("TabMarketingZone")

            Else
                Dim source As DataTable = Nothing

                With New DataManagerFactory("SELECT  TABMARKETINGZONE.COMPANYID, TABMARKETINGZONE.MARKETINGZONETABLEID, TABMARKETINGZONE.COUNTRYID, TABMARKETINGZONE.MARKETINGZONELEVELID, TABMARKETINGZONE.MARKETINGZONEID, TABMARKETINGZONE.RECORDSTATUS, TRANSMARKETINGZONE.LANGUAGEID, TRANSMARKETINGZONE.DESCRIPTION FROM ADDRESS.TABMARKETINGZONE TABMARKETINGZONE JOIN ADDRESS.TRANSMARKETINGZONE TRANSMARKETINGZONE ON TRANSMARKETINGZONE.COMPANYID = TABMARKETINGZONE.COMPANYID  AND TRANSMARKETINGZONE.MARKETINGZONETABLEID = TABMARKETINGZONE.MARKETINGZONETABLEID  AND TRANSMARKETINGZONE.COUNTRYID = TABMARKETINGZONE.COUNTRYID  AND TRANSMARKETINGZONE.MARKETINGZONELEVELID = TABMARKETINGZONE.MARKETINGZONELEVELID  AND TRANSMARKETINGZONE.MARKETINGZONEID = TABMARKETINGZONE.MARKETINGZONEID  WHERE TABMARKETINGZONE.RECORDSTATUS = '1' AND TRANSMARKETINGZONE.LANGUAGEID = @:LANGUAGEID ", "TabMarketingZone", "Linked.Address")
                    .AddParameter("LANGUAGEID", DbType.Decimal, 5, False, CurrentState.Get("Language"))
 
                    source = .QueryExecuteToTable(True)
                    DirectCast(TabMarketingZoneRelation.Columns("MARKETINGZONEID"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = source
                End With

                If Not IsNothing(source) AndAlso source.Rows.Count > 0 Then
                    Caching.SetItem("TabMarketingZone", source)
                End If
            End If 
             If Caching.Exist("TabZipCode") Then
                DirectCast(TabMarketingZoneRelation.Columns("ZIPCODE"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = Caching.GetItem("TabZipCode")

            Else
                Dim source As DataTable = Nothing

                With New DataManagerFactory("SELECT  TABZIPCODE.COUNTRYID, TABZIPCODE.ZIPCODE, TABZIPCODE.RECORDSTATUS FROM ADDRESS.TABZIPCODE TABZIPCODE  WHERE TABZIPCODE.RECORDSTATUS = '1' ", "TabZipCode", "Linked.Address")
                     
                    source = .QueryExecuteToTable(True)
                    DirectCast(TabMarketingZoneRelation.Columns("ZIPCODE"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = source
                End With

                If Not IsNothing(source) AndAlso source.Rows.Count > 0 Then
                    Caching.SetItem("TabZipCode", source)
                End If
            End If 
             If Caching.Exist("EnumRecordStatus") Then
                DirectCast(TabMarketingZoneRelation.Columns("RECORDSTATUS"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = Caching.GetItem("EnumRecordStatus")

            Else
                Dim source As DataTable = Nothing

                With New DataManagerFactory("SELECT  ENUMRECORDSTATUS.RECORDSTATUS, ETRANRECORDSTATUS.LANGUAGEID, ETRANRECORDSTATUS.DESCRIPTION FROM COMMON.ENUMRECORDSTATUS ENUMRECORDSTATUS JOIN COMMON.ETRANRECORDSTATUS ETRANRECORDSTATUS ON ETRANRECORDSTATUS.RECORDSTATUS = ENUMRECORDSTATUS.RECORDSTATUS  WHERE ETRANRECORDSTATUS.LANGUAGEID = @:LANGUAGEID ", "EnumRecordStatus", "Linked.Address")
                    .AddParameter("LANGUAGEID", DbType.Decimal, 5, False, CurrentState.Get("Language"))
 
                    source = .QueryExecuteToTable(True)
                    DirectCast(TabMarketingZoneRelation.Columns("RECORDSTATUS"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = source
                End With

                If Not IsNothing(source) AndAlso source.Rows.Count > 0 Then
                    Caching.SetItem("EnumRecordStatus", source)
                End If
            End If 
             If Caching.Exist("TabGeographicZone") Then
                DirectCast(TabMarketingZoneRelation.Columns("GEOGRAPHICZONEID"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = Caching.GetItem("TabGeographicZone")

            Else
                Dim source As DataTable = Nothing

                With New DataManagerFactory("SELECT  TABGEOGRAPHICZONE.COUNTRYID, TABGEOGRAPHICZONE.GEOGRAPHICZONETABLEID, TABGEOGRAPHICZONE.GEOGRAPHICZONELEVELID, TABGEOGRAPHICZONE.GEOGRAPHICZONEID, TABGEOGRAPHICZONE.RECORDSTATUS, TRANSGEOGRAPHICZONE.LANGUAGEID, TRANSGEOGRAPHICZONE.DESCRIPTION FROM ADDRESS.TABGEOGRAPHICZONE TABGEOGRAPHICZONE JOIN ADDRESS.TRANSGEOGRAPHICZONE TRANSGEOGRAPHICZONE ON TRANSGEOGRAPHICZONE.COUNTRYID = TABGEOGRAPHICZONE.COUNTRYID  AND TRANSGEOGRAPHICZONE.GEOGRAPHICZONETABLEID = TABGEOGRAPHICZONE.GEOGRAPHICZONETABLEID  AND TRANSGEOGRAPHICZONE.GEOGRAPHICZONELEVELID = TABGEOGRAPHICZONE.GEOGRAPHICZONELEVELID  AND TRANSGEOGRAPHICZONE.GEOGRAPHICZONEID = TABGEOGRAPHICZONE.GEOGRAPHICZONEID  WHERE TABGEOGRAPHICZONE.RECORDSTATUS = '1' AND TRANSGEOGRAPHICZONE.LANGUAGEID = @:LANGUAGEID ", "TabGeographicZone", "Linked.Address")
                    .AddParameter("LANGUAGEID", DbType.Decimal, 5, False, CurrentState.Get("Language"))
 
                    source = .QueryExecuteToTable(True)
                    DirectCast(TabMarketingZoneRelation.Columns("GEOGRAPHICZONEID"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = source
                End With

                If Not IsNothing(source) AndAlso source.Rows.Count > 0 Then
                    Caching.SetItem("TabGeographicZone", source)
                End If
            End If 
 

                 With New DataManagerFactory("SELECT  TABMARKETINGZONERELATION.COMPANYID, TABMARKETINGZONERELATION.MARKETINGZONETABLEID, TABMARKETINGZONERELATION.COUNTRYID, TABMARKETINGZONERELATION.MARKETINGZONELEVELID, TABMARKETINGZONERELATION.MARKETINGZONEID, TABMARKETINGZONERELATION.CONSECUTIVE, TABMARKETINGZONERELATION.ZIPCODE, TABMARKETINGZONERELATION.RECORDSTATUS, TABMARKETINGZONERELATION.GEOGRAPHICZONETABLEID, TABMARKETINGZONERELATION.GEOGRAPHICZONELEVELID, TABMARKETINGZONERELATION.GEOGRAPHICZONEID FROM ADDRESS.TABMARKETINGZONERELATION TABMARKETINGZONERELATION  ", "TabMarketingZoneRelation", "Linked.Address")                 
                                                   
                                  
                      TabMarketingZoneRelation.DataSource = .QueryExecuteToTable(True)
                 End With 
        End If     
    End Sub

    Protected Sub TabMarketingZoneRelation_CellEditorInitialize(sender As Object, e As ASPxGridViewEditorEventArgs) Handles TabMarketingZoneRelation.CellEditorInitialize
        If TabMarketingZoneRelation.IsNewRowEditing Then
            Select Case e.Column.FieldName
                
                Case "MARKETINGZONEID"
     AddHandler DirectCast(e.Editor, ASPxComboBox).Callback, AddressOf MarketingZoneID_OnCallback 
Case "ZIPCODE"
     AddHandler DirectCast(e.Editor, ASPxComboBox).Callback, AddressOf ZipCode_OnCallback 

                
                Case "COMPANYID"                     
                       e.Editor.Focus()               
            End Select

        Else

            Select Case e.Column.FieldName
                Case "COMPANYID"
     e.Editor.Enabled = False
Case "MARKETINGZONETABLEID"
     e.Editor.Enabled = False
Case "COUNTRYID"
     e.Editor.Enabled = False
Case "MARKETINGZONELEVELID"
     e.Editor.Enabled = False
Case "MARKETINGZONEID"
     e.Editor.Enabled = False
     MarketingZoneID_Fill(e.Editor, TabMarketingZoneRelation.GetRowValues(e.VisibleIndex, "COUNTRYID")) 
     AddHandler DirectCast(e.Editor, ASPxComboBox).Callback, AddressOf MarketingZoneID_OnCallback 
Case "CONSECUTIVE"
     e.Editor.Enabled = False
                   
                
                
                Case "ZIPCODE"                     
                     e.Editor.Focus()
            End Select
        End If
        
       Select Case e.Column.FieldName
           Case "COMPANYID"
                 DirectCast(e.Editor, ASPxComboBox).DataBindItems()
                 
           Case "COUNTRYID"
     DirectCast(e.Editor, ASPxComboBox).DataBindItems() 
Case "MARKETINGZONEID"
     DirectCast(e.Editor, ASPxComboBox).DataBindItems() 
Case "ZIPCODE"
     DirectCast(e.Editor, ASPxComboBox).DataBindItems() 
Case "RECORDSTATUS"
     DirectCast(e.Editor, ASPxComboBox).DataBindItems() 
Case "GEOGRAPHICZONEID"
     DirectCast(e.Editor, ASPxComboBox).DataBindItems() 

        End Select
    End Sub

    Protected Sub TabMarketingZoneRelation_RowInserting(ByVal sender As Object, ByVal e As ASPxDataInsertingEventArgs) Handles TabMarketingZoneRelation.RowInserting 
        Dim isNullResult As Boolean = True
        
             With New DataManagerFactory("INSERT INTO ADDRESS.TabMarketingZoneRelation (COMPANYID, MARKETINGZONETABLEID, COUNTRYID, MARKETINGZONELEVELID, MARKETINGZONEID, CONSECUTIVE, ZIPCODE, RECORDSTATUS, GEOGRAPHICZONETABLEID, GEOGRAPHICZONELEVELID, GEOGRAPHICZONEID, CREATIONDATE, CREATORUSERCODE, UPDATEDATE, UPDATEUSERCODE) VALUES (@:COMPANYID, @:MARKETINGZONETABLEID, @:COUNTRYID, @:MARKETINGZONELEVELID, @:MARKETINGZONEID, @:CONSECUTIVE, @:ZIPCODE, @:RECORDSTATUS, @:GEOGRAPHICZONETABLEID, @:GEOGRAPHICZONELEVELID, @:GEOGRAPHICZONEID, SYSDATE, @:CREATORUSERCODE, SYSDATE, @:UPDATEUSERCODE)", "TabMarketingZoneRelation", "Linked.Address")                 
                                                   
                       .AddParameter("COMPANYID", DbType.Decimal, 0, False, e.NewValues("COMPANYID"))
.AddParameter("MARKETINGZONETABLEID", DbType.Decimal, 0, False, e.NewValues("MARKETINGZONETABLEID"))
.AddParameter("COUNTRYID", DbType.Decimal, 0, False, e.NewValues("COUNTRYID"))
.AddParameter("MARKETINGZONELEVELID", DbType.Decimal, 0, False, e.NewValues("MARKETINGZONELEVELID"))
.AddParameter("MARKETINGZONEID", DbType.AnsiString, 0, False, e.NewValues("MARKETINGZONEID"))
.AddParameter("CONSECUTIVE", DbType.Decimal, 0, False, e.NewValues("CONSECUTIVE"))
.AddParameter("ZIPCODE", DbType.AnsiString, 0, (e.NewValues("ZIPCODE") = String.Empty), e.NewValues("ZIPCODE"))
.AddParameter("RECORDSTATUS", DbType.AnsiString, 0, False, e.NewValues("RECORDSTATUS"))
.AddParameter("GEOGRAPHICZONETABLEID", DbType.AnsiString, 0, (e.NewValues("GEOGRAPHICZONETABLEID") = String.Empty), e.NewValues("GEOGRAPHICZONETABLEID"))
.AddParameter("GEOGRAPHICZONELEVELID", DbType.Decimal, 0, (e.NewValues("GEOGRAPHICZONELEVELID") = 0), e.NewValues("GEOGRAPHICZONELEVELID"))
.AddParameter("GEOGRAPHICZONEID", DbType.AnsiString, 0, (e.NewValues("GEOGRAPHICZONEID") = String.Empty), e.NewValues("GEOGRAPHICZONEID"))
.AddParameter("CREATORUSERCODE", DbType.Decimal, 0, False, Session("nUsercode"))
.AddParameter("UPDATEUSERCODE", DbType.Decimal, 0, False, Session("nUsercode"))
            
                       .CommandExecute()
                End With
               
        e.Cancel = True
        TabMarketingZoneRelation.CancelEdit()
    End Sub

    Protected Sub TabMarketingZoneRelation_RowUpdating(ByVal sender As Object, ByVal e As ASPxDataUpdatingEventArgs) Handles TabMarketingZoneRelation.RowUpdating
        Dim isNullResult As Boolean = True
          
             With New DataManagerFactory("UPDATE ADDRESS.TabMarketingZoneRelation SET ZIPCODE = @:ZIPCODE, RECORDSTATUS = @:RECORDSTATUS, GEOGRAPHICZONETABLEID = @:GEOGRAPHICZONETABLEID, GEOGRAPHICZONELEVELID = @:GEOGRAPHICZONELEVELID, GEOGRAPHICZONEID = @:GEOGRAPHICZONEID, UPDATEDATE = SYSDATE, UPDATEUSERCODE = @:UPDATEUSERCODE WHERE COMPANYID = @:COMPANYID AND MARKETINGZONETABLEID = @:MARKETINGZONETABLEID AND COUNTRYID = @:COUNTRYID AND MARKETINGZONELEVELID = @:MARKETINGZONELEVELID AND MARKETINGZONEID = @:MARKETINGZONEID AND CONSECUTIVE = @:CONSECUTIVE", "TabMarketingZoneRelation", "Linked.Address")                 
                                                   
                       .AddParameter("ZIPCODE", DbType.AnsiString, 0, (e.NewValues("ZIPCODE") = String.Empty), e.NewValues("ZIPCODE"))
.AddParameter("RECORDSTATUS", DbType.AnsiString, 0, False, e.NewValues("RECORDSTATUS"))
.AddParameter("GEOGRAPHICZONETABLEID", DbType.AnsiString, 0, (e.NewValues("GEOGRAPHICZONETABLEID") = String.Empty), e.NewValues("GEOGRAPHICZONETABLEID"))
.AddParameter("GEOGRAPHICZONELEVELID", DbType.Decimal, 0, (e.NewValues("GEOGRAPHICZONELEVELID") = 0), e.NewValues("GEOGRAPHICZONELEVELID"))
.AddParameter("GEOGRAPHICZONEID", DbType.AnsiString, 0, (e.NewValues("GEOGRAPHICZONEID") = String.Empty), e.NewValues("GEOGRAPHICZONEID"))
.AddParameter("UPDATEUSERCODE", DbType.Decimal, 0, False, Session("nUsercode"))
.AddParameter("COMPANYID", DbType.Decimal, 0, False, e.Keys("COMPANYID"))
.AddParameter("MARKETINGZONETABLEID", DbType.Decimal, 0, False, e.Keys("MARKETINGZONETABLEID"))
.AddParameter("COUNTRYID", DbType.Decimal, 0, False, e.Keys("COUNTRYID"))
.AddParameter("MARKETINGZONELEVELID", DbType.Decimal, 0, False, e.Keys("MARKETINGZONELEVELID"))
.AddParameter("MARKETINGZONEID", DbType.AnsiString, 0, False, e.Keys("MARKETINGZONEID"))
.AddParameter("CONSECUTIVE", DbType.Decimal, 0, False, e.Keys("CONSECUTIVE"))
            
                       .CommandExecute()
                End With         
      
         e.Cancel = True
        TabMarketingZoneRelation.CancelEdit()
    End Sub
    
    Protected Sub TabMarketingZoneRelation_CustomCallback(sender As Object, e As ASPxGridViewCustomCallbackEventArgs) Handles TabMarketingZoneRelation.CustomCallback     
           Dim isNullResult As Boolean = True
           
           Select Case e.Parameters.ToString.ToLower
               Case "delete"
                       Dim COMPANYIDKey As Generic.List(Of Object) = TabMarketingZoneRelation.GetSelectedFieldValues("COMPANYID")
 Dim MARKETINGZONETABLEIDKey As Generic.List(Of Object) = TabMarketingZoneRelation.GetSelectedFieldValues("MARKETINGZONETABLEID")
 Dim COUNTRYIDKey As Generic.List(Of Object) = TabMarketingZoneRelation.GetSelectedFieldValues("COUNTRYID")
 Dim MARKETINGZONELEVELIDKey As Generic.List(Of Object) = TabMarketingZoneRelation.GetSelectedFieldValues("MARKETINGZONELEVELID")
 Dim MARKETINGZONEIDKey As Generic.List(Of Object) = TabMarketingZoneRelation.GetSelectedFieldValues("MARKETINGZONEID")
 Dim CONSECUTIVEKey As Generic.List(Of Object) = TabMarketingZoneRelation.GetSelectedFieldValues("CONSECUTIVE")
        
               For index As Integer = 0 To COMPANYIDKey.Count - 1
                  With New DataManagerFactory("DELETE FROM ADDRESS.TabMarketingZoneRelation WHERE COMPANYID = @:COMPANYID AND MARKETINGZONETABLEID = @:MARKETINGZONETABLEID AND COUNTRYID = @:COUNTRYID AND MARKETINGZONELEVELID = @:MARKETINGZONELEVELID AND MARKETINGZONEID = @:MARKETINGZONEID AND CONSECUTIVE = @:CONSECUTIVE ", "TabMarketingZoneRelation", "Linked.Address")                 
                                                   
               .AddParameter("COMPANYID", DbType.Decimal, 0, False, COMPANYIDKey(index))
.AddParameter("MARKETINGZONETABLEID", DbType.Decimal, 0, False, MARKETINGZONETABLEIDKey(index))
.AddParameter("COUNTRYID", DbType.Decimal, 0, False, COUNTRYIDKey(index))
.AddParameter("MARKETINGZONELEVELID", DbType.Decimal, 0, False, MARKETINGZONELEVELIDKey(index))
.AddParameter("MARKETINGZONEID", DbType.AnsiString, 0, False, MARKETINGZONEIDKey(index))
.AddParameter("CONSECUTIVE", DbType.Decimal, 0, False, CONSECUTIVEKey(index))
            
               .CommandExecute()
          End With 
                       
              Next           
           
              TabMarketingZoneRelation.DataBind()
                 
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
    
    Protected Sub TabMarketingZoneRelation_RowValidating(sender As Object, e As ASPxDataValidationEventArgs) Handles TabMarketingZoneRelation.RowValidating

        
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
 

#Region "MarketingZoneID Events"

    Private Sub MarketingZoneID_OnCallback(ByVal source As Object, ByVal e As CallbackEventArgsBase)
        MarketingZoneID_Fill(DirectCast(source, ASPxComboBox), e.Parameter)
    End Sub

    Private Sub MarketingZoneID_Fill(control As ASPxComboBox, CountryID As Integer)
          With New DataManagerFactory("SELECT  TABMARKETINGZONE.COMPANYID, TABMARKETINGZONE.MARKETINGZONETABLEID, TABMARKETINGZONE.COUNTRYID, TABMARKETINGZONE.MARKETINGZONELEVELID, TABMARKETINGZONE.MARKETINGZONEID, TABMARKETINGZONE.RECORDSTATUS, TRANSMARKETINGZONE.LANGUAGEID, TRANSMARKETINGZONE.DESCRIPTION FROM ADDRESS.TABMARKETINGZONE TABMARKETINGZONE JOIN ADDRESS.TRANSMARKETINGZONE TRANSMARKETINGZONE ON TRANSMARKETINGZONE.COMPANYID = TABMARKETINGZONE.COMPANYID  AND TRANSMARKETINGZONE.MARKETINGZONETABLEID = TABMARKETINGZONE.MARKETINGZONETABLEID  AND TRANSMARKETINGZONE.COUNTRYID = TABMARKETINGZONE.COUNTRYID  AND TRANSMARKETINGZONE.MARKETINGZONELEVELID = TABMARKETINGZONE.MARKETINGZONELEVELID  AND TRANSMARKETINGZONE.MARKETINGZONEID = TABMARKETINGZONE.MARKETINGZONEID  WHERE TABMARKETINGZONE.RECORDSTATUS = '1' AND TRANSMARKETINGZONE.LANGUAGEID = @:LANGUAGEID  AND TABMARKETINGZONE.COUNTRYID = @:DependencyTABMARKETINGZON", "TabMarketingZone", "Linked.Address")                 
                                                   
               .AddParameter("LANGUAGEID", DbType.Decimal, 5, False, CurrentState.Get("Language"))
.AddParameter("DependencyTabMarketingZon", DbType.Int32, 0, False, CountryID)
  
               
               control.DataSource = .QueryExecuteToTable(True)
               control.DataBindItems()
          End With         
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