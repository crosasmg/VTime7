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

Partial Class Maintenance_TabBestTimeToCall
    Inherits PageBase

#Region "Private fields"

    Private _internalCall As Boolean

#End Region

#Region "Events Page"

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not IsCallback AndAlso Not IsPostBack Then
            Dim newItem As DevExpress.Web.ASPxMenu.MenuItem

            For Each languageItem In InMotionGIT.Common.Proxy.Helpers.Language.LanguageToDictionary



                newItem = New DevExpress.Web.ASPxMenu.MenuItem

                With newItem
                    .Name = String.Format(CultureInfo.InvariantCulture, "{0}Item", languageItem.Value)
                    .Text = languageItem.Value
                    .Image.Url = String.Format(CultureInfo.InvariantCulture, "/images/16x16/Flags/{0}.png", languageItem.Value.ToLower)

                   If languageItem.Key = LanguageId Then
                        MainMenu.Items(4).Text = String.Format(CultureInfo.InvariantCulture, "{0} {1}", GetLocalResourceObject("LanguageItemMenu").ToString(), languageItem.Value)
                        MainMenu.Items(4).Image.Url = String.Format(CultureInfo.InvariantCulture, "/images/16x16/Flags/{0}.png", languageItem.Value.ToLower)

                        .Visible = False
                    Else
                        .Visible = True
                    End If
                End With

                MainMenu.Items(4).Items.Add(newItem)
            Next

        End If
        
        If Not CurrentState.Contains("LanguageId") Then
            CurrentState.Add("LanguageId", LanguageId)
        End If
    End Sub

#End Region

#Region "MainMenu Events"

    Protected Sub MainMenu_ItemClick(source As Object, e As DevExpress.Web.ASPxMenu.MenuItemEventArgs) Handles MainMenu.ItemClick
        If String.Equals(e.Item.Parent.Name, "LanguageItem", StringComparison.CurrentCultureIgnoreCase) Then
            e.Item.Parent.Text = String.Format(CultureInfo.InvariantCulture, "{0} {1}", GetLocalResourceObject("LanguageItemMenu").ToString(), e.Item.Text)
            e.Item.Parent.Image.Url = String.Format(CultureInfo.InvariantCulture, "/images/16x16/Flags/{0}.png", e.Item.Text.ToLower)

            e.Item.Visible = False

            For Each item As DevExpress.Web.ASPxMenu.MenuItem In e.Item.Parent.Items
                If Not String.Equals(item.Text, e.Item.Text, StringComparison.CurrentCultureIgnoreCase) Then
                    item.Visible = True
                End If
            Next

            CurrentState.Set("LanguageId", InMotionGIT.Common.Proxy.Helpers.Language.GetLanguageIdCurrentContext())
            _internalCall = True

            TabBestTimeToCall_Grid.DataBind()
        End If      
    End Sub

#End Region

#Region "Controls Events"

         
    
 

#End Region

#Region "TabBestTimeToCall_Grid Events"
    
    Protected Sub TabBestTimeToCall_Grid_CustomColumnDisplayText(sender As Object, e As DevExpress.Web.ASPxGridView.ASPxGridViewColumnDisplayTextEventArgs) Handles TabBestTimeToCall_Grid.CustomColumnDisplayText
          Dim data As DataTable
          Dim rows() As DataRow
           
          Select Case e.Column.FieldName

               Case Else              
           End Select
    End Sub
  
    Protected Sub TabBestTimeToCall_Grid_DataBinding(ByVal sender As Object, ByVal e As EventArgs) Handles TabBestTimeToCall_Grid.DataBinding
        If (Not IsNothing(Request.Params("__CALLBACKID")) AndAlso Request.Params("__CALLBACKID").EndsWith("TabBestTimeToCall_Grid")) Or _internalCall Then
                       If Caching.Exist("EnumRecordStatus") Then
                DirectCast(TabBestTimeToCall_Grid.Columns("RECORDSTATUS"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = Caching.GetItem("EnumRecordStatus")

            Else
                Dim source As DataTable = Nothing

                With New DataManagerFactory("SELECT  ENUMRECORDSTATUS.RECORDSTATUS, ETRANRECORDSTATUS.LANGUAGEID, ETRANRECORDSTATUS.DESCRIPTION FROM COMMON.ENUMRECORDSTATUS ENUMRECORDSTATUS JOIN COMMON.ETRANRECORDSTATUS ETRANRECORDSTATUS ON ETRANRECORDSTATUS.RECORDSTATUS = ENUMRECORDSTATUS.RECORDSTATUS  WHERE ETRANRECORDSTATUS.LANGUAGEID = @:LANGUAGEID ", "EnumRecordStatus", "Linked.Phone")
                    .AddParameter("LANGUAGEID", DbType.Decimal, 5, False, CurrentState.Get("Language"))
 
                    source = .QueryExecuteToTable(True)
                    DirectCast(TabBestTimeToCall_Grid.Columns("RECORDSTATUS"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = source
                End With

                If Not IsNothing(source) AndAlso source.Rows.Count > 0 Then
                    Caching.SetItem("EnumRecordStatus", source)
                End If
            End If 
 

                 With New DataManagerFactory("SELECT  TABBESTTIMETOCALL.BESTTIMETOCALL, TABBESTTIMETOCALL.RECORDSTATUS, TRANSBESTTIMETOCALL.BESTTIMETOCALL, TRANSBESTTIMETOCALL.LANGUAGEID, TRANSBESTTIMETOCALL.DESCRIPTION, TRANSBESTTIMETOCALL.SHORTDESCRIPTION FROM PHONE.TABBESTTIMETOCALL TABBESTTIMETOCALL JOIN PHONE.TRANSBESTTIMETOCALL TRANSBESTTIMETOCALL ON TRANSBESTTIMETOCALL.BESTTIMETOCALL = TABBESTTIMETOCALL.BESTTIMETOCALL  WHERE TRANSBESTTIMETOCALL.LANGUAGEID = @:LANGUAGEID", "TabBestTimeToCall", "Linked.Phone")                 
                                                   
                      .AddParameter("LANGUAGEID", DbType.Decimal, 5, False, CurrentState.Get("Language"))
            
                      TabBestTimeToCall_Grid.DataSource = .QueryExecuteToTable(True)
                 End With 
        End If     
    End Sub

    Protected Sub TabBestTimeToCall_Grid_CellEditorInitialize(sender As Object, e As ASPxGridViewEditorEventArgs) Handles TabBestTimeToCall_Grid.CellEditorInitialize
        If TabBestTimeToCall_Grid.IsNewRowEditing Then
            Select Case e.Column.FieldName
                
                
                
                Case "BESTTIMETOCALL"                     
                       e.Editor.Focus()               
            End Select

        Else

            Select Case e.Column.FieldName
                Case "BESTTIMETOCALL"
     e.Editor.Enabled = False
                   
                
                
                Case "RECORDSTATUS"                     
                     e.Editor.Focus()
            End Select
        End If
        
       Select Case e.Column.FieldName
           Case "BESTTIMETOCALL"
                 
                 
           Case "RECORDSTATUS"
     DirectCast(e.Editor, ASPxComboBox).DataBindItems() 

        End Select
    End Sub

    Protected Sub TabBestTimeToCall_Grid_RowInserting(ByVal sender As Object, ByVal e As ASPxDataInsertingEventArgs) Handles TabBestTimeToCall_Grid.RowInserting 
        Dim isNullResult As Boolean = True
        
             With New DataManagerFactory("INSERT INTO PHONE.TabBestTimeToCall (BESTTIMETOCALL, RECORDSTATUS, CREATIONDATE, CREATORUSERCODE, UPDATEUSERCODE, UPDATEDATE) VALUES (@:BESTTIMETOCALL, @:RECORDSTATUS, SYSDATE, @:CREATORUSERCODE, @:UPDATEUSERCODE, SYSDATE)", "TabBestTimeToCall", "Linked.Phone")                 
                                                   
                       .AddParameter("BESTTIMETOCALL", DbType.AnsiString, 0, False, e.NewValues("BESTTIMETOCALL"))
.AddParameter("RECORDSTATUS", DbType.AnsiString, 0, False, e.NewValues("RECORDSTATUS"))
.AddParameter("CREATORUSERCODE", DbType.Decimal, 0, False, Session("nUsercode"))
.AddParameter("UPDATEUSERCODE", DbType.AnsiString, 0, False, Session("nUsercode"))
            
                       .CommandExecute()
                End With
        For Each languageItem In InMotionGIT.Common.Proxy.Helpers.Language.LanguageToDictionary


            With New DataManagerFactory("INSERT INTO PHONE.TransBestTimeToCall (BESTTIMETOCALL, LANGUAGEID, DESCRIPTION, SHORTDESCRIPTION, CREATIONDATE, CREATORUSERCODE, UPDATEDATE, UPDATEUSERCODE) VALUES (@:BESTTIMETOCALL, @:LANGUAGEID, @:DESCRIPTION, @:SHORTDESCRIPTION, SYSDATE, @:CREATORUSERCODE, SYSDATE, @:UPDATEUSERCODE)", "TransBestTimeToCall", "Linked.Phone")

                .AddParameter("BESTTIMETOCALL", DbType.AnsiString, 0, False, e.NewValues("BESTTIMETOCALL"))
                .AddParameter("LANGUAGEID", DbType.Decimal, 0, False, languageItem.Key)
                .AddParameter("DESCRIPTION", DbType.AnsiString, 0, False, e.NewValues("DESCRIPTION"))
                .AddParameter("SHORTDESCRIPTION", DbType.AnsiString, 0, False, e.NewValues("SHORTDESCRIPTION"))
                .AddParameter("CREATORUSERCODE", DbType.Decimal, 0, False, Session("nUsercode"))
                .AddParameter("UPDATEUSERCODE", DbType.Decimal, 0, False, Session("nUsercode"))

                .CommandExecute()
            End With
        Next

               
        e.Cancel = True
        TabBestTimeToCall_Grid.CancelEdit()
    End Sub

    Protected Sub TabBestTimeToCall_Grid_RowUpdating(ByVal sender As Object, ByVal e As ASPxDataUpdatingEventArgs) Handles TabBestTimeToCall_Grid.RowUpdating
        Dim isNullResult As Boolean = True
          
             With New DataManagerFactory("UPDATE PHONE.TabBestTimeToCall SET RECORDSTATUS = @:RECORDSTATUS, UPDATEUSERCODE = @:UPDATEUSERCODE, UPDATEDATE = SYSDATE WHERE BESTTIMETOCALL = @:BESTTIMETOCALL", "TabBestTimeToCall", "Linked.Phone")                 
                                                   
                       .AddParameter("RECORDSTATUS", DbType.AnsiString, 0, False, e.NewValues("RECORDSTATUS"))
.AddParameter("UPDATEUSERCODE", DbType.AnsiString, 0, False, Session("nUsercode"))
.AddParameter("BESTTIMETOCALL", DbType.AnsiString, 0, False, e.Keys("BESTTIMETOCALL"))
            
                       .CommandExecute()
                End With
     With New DataManagerFactory("UPDATE PHONE.TransBestTimeToCall SET DESCRIPTION = @:DESCRIPTION, SHORTDESCRIPTION = @:SHORTDESCRIPTION, UPDATEDATE = SYSDATE, UPDATEUSERCODE = @:UPDATEUSERCODE WHERE BESTTIMETOCALL = @:BESTTIMETOCALL AND LANGUAGEID = @:LANGUAGEID", "TransBestTimeToCall", "Linked.Phone")                 
                                                   
                       .AddParameter("DESCRIPTION", DbType.AnsiString, 0, False, e.NewValues("DESCRIPTION"))
.AddParameter("SHORTDESCRIPTION", DbType.AnsiString, 0, False, e.NewValues("SHORTDESCRIPTION"))
.AddParameter("UPDATEUSERCODE", DbType.Decimal, 0, False, Session("nUsercode"))
.AddParameter("BESTTIMETOCALL", DbType.AnsiString, 0, False, e.Keys("BESTTIMETOCALL"))
.AddParameter("LANGUAGEID", DbType.Decimal, 0, False, CurrentState.Get("Language"))
            
                       .CommandExecute()
                End With         
      
         e.Cancel = True
        TabBestTimeToCall_Grid.CancelEdit()
    End Sub
    
    Protected Sub TabBestTimeToCall_Grid_CustomCallback(sender As Object, e As ASPxGridViewCustomCallbackEventArgs) Handles TabBestTimeToCall_Grid.CustomCallback     
           Dim isNullResult As Boolean = True
           
           Select Case e.Parameters.ToString.ToLower
               Case "delete"
                       Dim BESTTIMETOCALLKey As Generic.List(Of Object) = TabBestTimeToCall_Grid.GetSelectedFieldValues("BESTTIMETOCALL")
        
               For index As Integer = 0 To BESTTIMETOCALLKey.Count - 1
                  With New DataManagerFactory("DELETE FROM PHONE.TransBestTimeToCall WHERE BESTTIMETOCALL = @:BESTTIMETOCALL ", "TransBestTimeToCall", "Linked.Phone")                 
                                                   
               .AddParameter("BESTTIMETOCALL", DbType.AnsiString, 0, False, BESTTIMETOCALLKey(index))
            
               .CommandExecute()
          End With 
 With New DataManagerFactory("DELETE FROM PHONE.TabBestTimeToCall WHERE BESTTIMETOCALL = @:BESTTIMETOCALL ", "TabBestTimeToCall", "Linked.Phone")                 
                                                   
               .AddParameter("BESTTIMETOCALL", DbType.AnsiString, 0, False, BESTTIMETOCALLKey(index))
            
               .CommandExecute()
          End With 
                       
              Next           
           
              TabBestTimeToCall_Grid.DataBind()
                 
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
    
    Protected Sub TabBestTimeToCall_Grid_RowValidating(sender As Object, e As ASPxDataValidationEventArgs) Handles TabBestTimeToCall_Grid.RowValidating

        
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
 
#Region "TransBestTimeToCall Events"
    
    Protected Sub TransBestTimeToCall_CustomColumnDisplayText(sender As Object, e As DevExpress.Web.ASPxGridView.ASPxGridViewColumnDisplayTextEventArgs) Handles TransBestTimeToCall.CustomColumnDisplayText
          Dim data As DataTable
          Dim rows() As DataRow
           
          Select Case e.Column.FieldName

               Case Else              
           End Select
    End Sub
  
    Protected Sub TransBestTimeToCall_DataBinding(ByVal sender As Object, ByVal e As EventArgs) Handles TransBestTimeToCall.DataBinding
        If (Not IsNothing(Request.Params("__CALLBACKID")) AndAlso Request.Params("__CALLBACKID").EndsWith("TransBestTimeToCall")) Or _internalCall Then
                       If Caching.Exist("EnumRecordStatus") Then
                DirectCast(TransBestTimeToCall.Columns("RECORDSTATUS"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = Caching.GetItem("EnumRecordStatus")

            Else
                Dim source As DataTable = Nothing

                With New DataManagerFactory("SELECT  ENUMRECORDSTATUS.RECORDSTATUS, ETRANRECORDSTATUS.LANGUAGEID, ETRANRECORDSTATUS.DESCRIPTION FROM COMMON.ENUMRECORDSTATUS ENUMRECORDSTATUS JOIN COMMON.ETRANRECORDSTATUS ETRANRECORDSTATUS ON ETRANRECORDSTATUS.RECORDSTATUS = ENUMRECORDSTATUS.RECORDSTATUS  WHERE ETRANRECORDSTATUS.LANGUAGEID = @:LANGUAGEID ", "EnumRecordStatus", "Linked.Phone")
                    .AddParameter("LANGUAGEID", DbType.Decimal, 5, False, CurrentState.Get("Language"))
 
                    source = .QueryExecuteToTable(True)
                    DirectCast(TransBestTimeToCall.Columns("RECORDSTATUS"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = source
                End With

                If Not IsNothing(source) AndAlso source.Rows.Count > 0 Then
                    Caching.SetItem("EnumRecordStatus", source)
                End If
            End If 
             If Caching.Exist("TabLanguage") Then
                DirectCast(TransBestTimeToCall.Columns("LANGUAGEID"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = Caching.GetItem("TabLanguage")

            Else
                Dim source As DataTable = Nothing

                With New DataManagerFactory("SELECT  TABLANGUAGE.LANGUAGEID, TABLANGUAGE.RECORDSTATUS, TRANSLANGUAGE.LANGUAGECODEID, TRANSLANGUAGE.DESCRIPTION FROM COMMON.TABLANGUAGE TABLANGUAGE JOIN COMMON.TRANSLANGUAGE TRANSLANGUAGE ON TRANSLANGUAGE.LANGUAGECODEID = TABLANGUAGE.LANGUAGEID  WHERE TABLANGUAGE.RECORDSTATUS = '1' AND TRANSLANGUAGE.LANGUAGECODEID = @:LANGUAGECODEID ", "TabLanguage", "Linked.Phone")
                    .AddParameter("LANGUAGECODEID", DbType.Decimal, 5, False, CurrentState.Get("Language"))
 
                    source = .QueryExecuteToTable(True)
                    DirectCast(TransBestTimeToCall.Columns("LANGUAGEID"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = source
                End With

                If Not IsNothing(source) AndAlso source.Rows.Count > 0 Then
                    Caching.SetItem("TabLanguage", source)
                End If
            End If 
 

                 With New DataManagerFactory("SELECT  TABBESTTIMETOCALL.BESTTIMETOCALL, TABBESTTIMETOCALL.RECORDSTATUS, TRANSBESTTIMETOCALL.BESTTIMETOCALL, TRANSBESTTIMETOCALL.LANGUAGEID, TRANSBESTTIMETOCALL.DESCRIPTION, TRANSBESTTIMETOCALL.SHORTDESCRIPTION FROM PHONE.TABBESTTIMETOCALL TABBESTTIMETOCALL JOIN PHONE.TRANSBESTTIMETOCALL TRANSBESTTIMETOCALL ON TRANSBESTTIMETOCALL.BESTTIMETOCALL = TABBESTTIMETOCALL.BESTTIMETOCALL  ", "TabBestTimeToCall", "Linked.Phone")                 
                                                   
                                  
                      TransBestTimeToCall.DataSource = .QueryExecuteToTable(True)
                 End With 
        End If     
    End Sub

    Protected Sub TransBestTimeToCall_CellEditorInitialize(sender As Object, e As ASPxGridViewEditorEventArgs) Handles TransBestTimeToCall.CellEditorInitialize
        If TransBestTimeToCall.IsNewRowEditing Then
            Select Case e.Column.FieldName
                
                
                
                Case "BESTTIMETOCALL"                     
                       e.Editor.Focus()               
            End Select

        Else

            Select Case e.Column.FieldName
                Case "BESTTIMETOCALL"
     e.Editor.Enabled = False
Case "LANGUAGEID"
     e.Editor.Enabled = False
                   
                
                
                Case "RECORDSTATUS"                     
                     e.Editor.Focus()
            End Select
        End If
        
       Select Case e.Column.FieldName
           Case "BESTTIMETOCALL"
                 
                 
           Case "RECORDSTATUS"
     DirectCast(e.Editor, ASPxComboBox).DataBindItems() 
Case "LANGUAGEID"
     DirectCast(e.Editor, ASPxComboBox).DataBindItems() 

        End Select
    End Sub

    Protected Sub TransBestTimeToCall_RowInserting(ByVal sender As Object, ByVal e As ASPxDataInsertingEventArgs) Handles TransBestTimeToCall.RowInserting 
        Dim isNullResult As Boolean = True
        
        
               
        e.Cancel = True
        TransBestTimeToCall.CancelEdit()
    End Sub

    Protected Sub TransBestTimeToCall_RowUpdating(ByVal sender As Object, ByVal e As ASPxDataUpdatingEventArgs) Handles TransBestTimeToCall.RowUpdating
        Dim isNullResult As Boolean = True
          
             With New DataManagerFactory("UPDATE PHONE.TransBestTimeToCall SET DESCRIPTION = @:DESCRIPTION, SHORTDESCRIPTION = @:SHORTDESCRIPTION, UPDATEDATE = SYSDATE, UPDATEUSERCODE = @:UPDATEUSERCODE WHERE BESTTIMETOCALL = @:BESTTIMETOCALL AND LANGUAGEID = @:LANGUAGEID", "TransBestTimeToCall", "Linked.Phone")                 
                                                   
                       .AddParameter("DESCRIPTION", DbType.AnsiString, 0, False, e.NewValues("DESCRIPTION"))
.AddParameter("SHORTDESCRIPTION", DbType.AnsiString, 0, False, e.NewValues("SHORTDESCRIPTION"))
.AddParameter("UPDATEUSERCODE", DbType.Decimal, 0, False, Session("nUsercode"))
.AddParameter("BESTTIMETOCALL", DbType.AnsiString, 0, False, e.Keys("BESTTIMETOCALL"))
.AddParameter("LANGUAGEID", DbType.Decimal, 0, False, e.Keys("LANGUAGEID"))
            
                       .CommandExecute()
                End With         
      
         e.Cancel = True
        TransBestTimeToCall.CancelEdit()
    End Sub
    
    Protected Sub TransBestTimeToCall_CustomCallback(sender As Object, e As ASPxGridViewCustomCallbackEventArgs) Handles TransBestTimeToCall.CustomCallback     
           Dim isNullResult As Boolean = True
           
           Select Case e.Parameters.ToString.ToLower
               Case "delete"
                   
                 
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
    
    Protected Sub TransBestTimeToCall_RowValidating(sender As Object, e As ASPxDataValidationEventArgs) Handles TransBestTimeToCall.RowValidating

        
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