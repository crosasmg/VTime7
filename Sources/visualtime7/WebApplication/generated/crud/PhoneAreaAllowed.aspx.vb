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

Partial Class Maintenance_PhoneAreaAllowed
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

#Region "PhoneAreaAllowed Events"
    
    Protected Sub PhoneAreaAllowed_CustomColumnDisplayText(sender As Object, e As DevExpress.Web.ASPxGridView.ASPxGridViewColumnDisplayTextEventArgs) Handles PhoneAreaAllowed.CustomColumnDisplayText
          Dim data As DataTable
          Dim rows() As DataRow
           
          Select Case e.Column.FieldName

               Case Else              
           End Select
    End Sub
  
    Protected Sub PhoneAreaAllowed_DataBinding(ByVal sender As Object, ByVal e As EventArgs) Handles PhoneAreaAllowed.DataBinding
        If (Not IsNothing(Request.Params("__CALLBACKID")) AndAlso Request.Params("__CALLBACKID").EndsWith("PhoneAreaAllowed")) Or _internalCall Then
                       If Caching.Exist("TabCountry") Then
                DirectCast(PhoneAreaAllowed.Columns("COUNTRYID"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = Caching.GetItem("TabCountry")

            Else
                Dim source As DataTable = Nothing

                With New DataManagerFactory("SELECT  TABCOUNTRY.COUNTRYID, TABCOUNTRY.RECORDSTATUS, TRANSCOUNTRY.LANGUAGEID, TRANSCOUNTRY.DESCRIPTION FROM COMMON.TABCOUNTRY TABCOUNTRY JOIN COMMON.TRANSCOUNTRY TRANSCOUNTRY ON TRANSCOUNTRY.COUNTRYID = TABCOUNTRY.COUNTRYID  WHERE TABCOUNTRY.RECORDSTATUS = '1' AND TRANSCOUNTRY.LANGUAGEID = @:LANGUAGEID ", "TabCountry", "Linked.Phone")
                    .AddParameter("LANGUAGEID", DbType.Decimal, 5, False, CurrentState.Get("Language"))
 
                    source = .QueryExecuteToTable(True)
                    DirectCast(PhoneAreaAllowed.Columns("COUNTRYID"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = source
                End With

                If Not IsNothing(source) AndAlso source.Rows.Count > 0 Then
                    Caching.SetItem("TabCountry", source)
                End If
            End If 
 

                 With New DataManagerFactory("SELECT  PHONEAREAALLOWED.COUNTRYID, PHONEAREAALLOWED.PHONEAREA, PHONEAREAALLOWED.MOBILEINDICATOR FROM PHONE.PHONEAREAALLOWED PHONEAREAALLOWED  ", "PhoneAreaAllowed", "Linked.Phone")                 
                                                   
                                  
                      PhoneAreaAllowed.DataSource = .QueryExecuteToTable(True)
                 End With 
        End If     
    End Sub

    Protected Sub PhoneAreaAllowed_CellEditorInitialize(sender As Object, e As ASPxGridViewEditorEventArgs) Handles PhoneAreaAllowed.CellEditorInitialize
        If PhoneAreaAllowed.IsNewRowEditing Then
            Select Case e.Column.FieldName
                
                
                
                Case "COUNTRYID"                     
                       e.Editor.Focus()               
            End Select

        Else

            Select Case e.Column.FieldName
                Case "COUNTRYID"
     e.Editor.Enabled = False
Case "PHONEAREA"
     e.Editor.Enabled = False
                   
                
                
                Case "MOBILEINDICATOR"                     
                     e.Editor.Focus()
            End Select
        End If
        
       Select Case e.Column.FieldName
           Case "COUNTRYID"
                 DirectCast(e.Editor, ASPxComboBox).DataBindItems()
                 
           
        End Select
    End Sub

    Protected Sub PhoneAreaAllowed_RowInserting(ByVal sender As Object, ByVal e As ASPxDataInsertingEventArgs) Handles PhoneAreaAllowed.RowInserting 
        Dim isNullResult As Boolean = True
        
             With New DataManagerFactory("INSERT INTO PHONE.PhoneAreaAllowed (COUNTRYID, PHONEAREA, MOBILEINDICATOR, CREATIONDATE, CREATORUSERCODE, UPDATEDATE, UPDATEUSERCODE) VALUES (@:COUNTRYID, @:PHONEAREA, @:MOBILEINDICATOR, SYSDATE, @:CREATORUSERCODE, SYSDATE, @:UPDATEUSERCODE)", "PhoneAreaAllowed", "Linked.Phone")                 
                                                   
                       .AddParameter("COUNTRYID", DbType.Decimal, 0, False, e.NewValues("COUNTRYID"))
.AddParameter("PHONEAREA", DbType.Decimal, 0, False, e.NewValues("PHONEAREA"))
.AddParameter("MOBILEINDICATOR", DbType.Decimal, 0, False, IIf(IsNothing(e.NewValues("MOBILEINDICATOR")), 0, e.NewValues("MOBILEINDICATOR")))
.AddParameter("CREATORUSERCODE", DbType.Decimal, 0, False, Session("nUsercode"))
.AddParameter("UPDATEUSERCODE", DbType.Decimal, 0, False, Session("nUsercode"))
            
                       .CommandExecute()
                End With
               
        e.Cancel = True
        PhoneAreaAllowed.CancelEdit()
    End Sub

    Protected Sub PhoneAreaAllowed_RowUpdating(ByVal sender As Object, ByVal e As ASPxDataUpdatingEventArgs) Handles PhoneAreaAllowed.RowUpdating
        Dim isNullResult As Boolean = True
          
             With New DataManagerFactory("UPDATE PHONE.PhoneAreaAllowed SET MOBILEINDICATOR = @:MOBILEINDICATOR, UPDATEDATE = SYSDATE, UPDATEUSERCODE = @:UPDATEUSERCODE WHERE COUNTRYID = @:COUNTRYID AND PHONEAREA = @:PHONEAREA", "PhoneAreaAllowed", "Linked.Phone")                 
                                                   
                       .AddParameter("MOBILEINDICATOR", DbType.Decimal, 0, False, IIf(IsNothing(e.NewValues("MOBILEINDICATOR")), 0, e.NewValues("MOBILEINDICATOR")))
.AddParameter("UPDATEUSERCODE", DbType.Decimal, 0, False, Session("nUsercode"))
.AddParameter("COUNTRYID", DbType.Decimal, 0, False, e.Keys("COUNTRYID"))
.AddParameter("PHONEAREA", DbType.Decimal, 0, False, e.Keys("PHONEAREA"))
            
                       .CommandExecute()
                End With         
      
         e.Cancel = True
        PhoneAreaAllowed.CancelEdit()
    End Sub
    
    Protected Sub PhoneAreaAllowed_CustomCallback(sender As Object, e As ASPxGridViewCustomCallbackEventArgs) Handles PhoneAreaAllowed.CustomCallback     
           Dim isNullResult As Boolean = True
           
           Select Case e.Parameters.ToString.ToLower
               Case "delete"
                       Dim COUNTRYIDKey As Generic.List(Of Object) = PhoneAreaAllowed.GetSelectedFieldValues("COUNTRYID")
 Dim PHONEAREAKey As Generic.List(Of Object) = PhoneAreaAllowed.GetSelectedFieldValues("PHONEAREA")
        
               For index As Integer = 0 To COUNTRYIDKey.Count - 1
                  With New DataManagerFactory("DELETE FROM PHONE.PhoneAreaAllowed WHERE COUNTRYID = @:COUNTRYID AND PHONEAREA = @:PHONEAREA ", "PhoneAreaAllowed", "Linked.Phone")                 
                                                   
               .AddParameter("COUNTRYID", DbType.Decimal, 0, False, COUNTRYIDKey(index))
.AddParameter("PHONEAREA", DbType.Decimal, 0, False, PHONEAREAKey(index))
            
               .CommandExecute()
          End With 
                       
              Next           
           
              PhoneAreaAllowed.DataBind()
                 
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
    
    Protected Sub PhoneAreaAllowed_RowValidating(sender As Object, e As ASPxDataValidationEventArgs) Handles PhoneAreaAllowed.RowValidating

        
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