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

#End Region

Partial Class Maintenance_TabUnderwritingRule
    Inherits PageBase

#Region "Private fields"

    Private _internalCall As Boolean

#End Region

#Region "Events Page"

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not IsCallback AndAlso Not IsPostBack Then
            Dim newItem As DevExpress.Web.ASPxMenu.MenuItem

            For Each languageItem As KeyValuePair(Of Integer, String) In LanguageToDictionary(LanguageId)

                newItem = New DevExpress.Web.ASPxMenu.MenuItem

                With newItem
                    .Name = String.Format(CultureInfo.InvariantCulture, "{0}Item", languageItem.Value)
                    .Text = languageItem.Value
                    .Image.Url = String.Format(CultureInfo.InvariantCulture, "/images/16x16/Flags/{0}.png", languageItem.Key)
                    .Index = languageItem.Key
                     
                    If languageItem.Key = LanguageId Then
                        MainMenu.Items(4).Text = String.Format(CultureInfo.InvariantCulture, "{0} {1}", GetLocalResourceObject("LanguageItemMenu").ToString(), languageItem.Value)
                        MainMenu.Items(4).Image.Url = String.Format(CultureInfo.InvariantCulture, "/images/16x16/Flags/{0}.png", languageItem.Key)

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
            e.Item.Parent.Image.Url = String.Format(CultureInfo.InvariantCulture, "/images/16x16/Flags/{0}.png", e.Item.Index)

            e.Item.Visible = False

            For Each item As DevExpress.Web.ASPxMenu.MenuItem In e.Item.Parent.Items
                If Not String.Equals(item.Text, e.Item.Text, StringComparison.CurrentCultureIgnoreCase) Then
                    item.Visible = True
                End If
            Next

            CurrentState.Set("LanguageId", DescriptionToEnumLanguage(e.Item.Text, LanguageId))
            _internalCall = True

            TabUnderwritingRule_Grid.DataBind()
        End If      
    End Sub

#End Region

#Region "Controls Events"

         
    
 

#End Region

#Region "TabUnderwritingRule_Grid Events"
    
    Protected Sub TabUnderwritingRule_Grid_CustomColumnDisplayText(sender As Object, e As DevExpress.Web.ASPxGridView.ASPxGridViewColumnDisplayTextEventArgs) Handles TabUnderwritingRule_Grid.CustomColumnDisplayText
          Dim data As DataTable
          Dim rows() As DataRow
           
          Select Case e.Column.FieldName

               Case Else              
           End Select
    End Sub
  
    Protected Sub TabUnderwritingRule_Grid_DataBinding(ByVal sender As Object, ByVal e As EventArgs) Handles TabUnderwritingRule_Grid.DataBinding
        If (Not IsNothing(Request.Params("__CALLBACKID")) AndAlso Request.Params("__CALLBACKID").EndsWith("TabUnderwritingRule_Grid")) Or _internalCall Then
                       If Caching.Exist("TabRestrictionType") Then
                DirectCast(TabUnderwritingRule_Grid.Columns("RESTRICTIONTYPE"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = Caching.GetItem("TabRestrictionType")

            Else
                Dim source As DataTable = Nothing

                With New DataManagerFactory("SELECT  TABRESTRICTIONTYPE.RESTRICTIONTYPE, TABRESTRICTIONTYPE.RECORDSTATUS, TRANSRESTRICTIONTYPE.LANGUAGEID, TRANSRESTRICTIONTYPE.DESCRIPTION FROM UNDERWRITING.TABRESTRICTIONTYPE TABRESTRICTIONTYPE JOIN TRANSRESTRICTIONTYPE TRANSRESTRICTIONTYPE ON TRANSRESTRICTIONTYPE.RESTRICTIONTYPE = TABRESTRICTIONTYPE.RESTRICTIONTYPE  WHERE TABRESTRICTIONTYPE.RECORDSTATUS = 1 AND TRANSRESTRICTIONTYPE.LANGUAGEID = @:LANGUAGEID  ORDER BY TransRestrictionType.Description ASC", "TabRestrictionType", "Linked.Underwriting")
                    .AddParameter("LANGUAGEID", DbType.Decimal, 5, False, CurrentState.Get("LanguageId"))
 
                    source = .QueryExecuteToTable(True)
                    DirectCast(TabUnderwritingRule_Grid.Columns("RESTRICTIONTYPE"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = source
                End With

                If Not IsNothing(source) AndAlso source.Rows.Count > 0 Then
                    Caching.SetItem("TabRestrictionType", source)
                End If
            End If 
             If Caching.Exist("TabDegree") Then
                DirectCast(TabUnderwritingRule_Grid.Columns("DEGREEID"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = Caching.GetItem("TabDegree")

            Else
                Dim source As DataTable = Nothing

                With New DataManagerFactory("SELECT  TABDEGREE.DEGREEID, TABDEGREE.RECORDSTATUS, TRANSDEGREE.LANGUAGEID, TRANSDEGREE.DESCRIPTION FROM UNDERWRITING.TABDEGREE TABDEGREE JOIN TRANSDEGREE TRANSDEGREE ON TRANSDEGREE.DEGREEID = TABDEGREE.DEGREEID  WHERE TABDEGREE.RECORDSTATUS = 1 AND TRANSDEGREE.LANGUAGEID = @:LANGUAGEID  ORDER BY TransDegree.Description ASC", "TabDegree", "Linked.Underwriting")
                    .AddParameter("LANGUAGEID", DbType.Decimal, 5, False, CurrentState.Get("LanguageId"))
 
                    source = .QueryExecuteToTable(True)
                    DirectCast(TabUnderwritingRule_Grid.Columns("DEGREEID"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = source
                End With

                If Not IsNothing(source) AndAlso source.Rows.Count > 0 Then
                    Caching.SetItem("TabDegree", source)
                End If
            End If 
             If Caching.Exist("TabAlarmType") Then
                DirectCast(TabUnderwritingRule_Grid.Columns("ALARMTYPE"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = Caching.GetItem("TabAlarmType")

            Else
                Dim source As DataTable = Nothing

                With New DataManagerFactory("SELECT  TABALARMTYPE.ALARMTYPE, TABALARMTYPE.RECORDSTATUS, TRANSALARMTYPE.LANGUAGEID, TRANSALARMTYPE.DESCRIPTION FROM UNDERWRITING.TABALARMTYPE TABALARMTYPE JOIN TRANSALARMTYPE TRANSALARMTYPE ON TRANSALARMTYPE.ALARMTYPE = TABALARMTYPE.ALARMTYPE  WHERE TABALARMTYPE.RECORDSTATUS = 1 AND TRANSALARMTYPE.LANGUAGEID = @:LANGUAGEID  ORDER BY TransAlarmType.Description ASC", "TabAlarmType", "Linked.Underwriting")
                    .AddParameter("LANGUAGEID", DbType.Decimal, 5, False, CurrentState.Get("LanguageId"))
 
                    source = .QueryExecuteToTable(True)
                    DirectCast(TabUnderwritingRule_Grid.Columns("ALARMTYPE"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = source
                End With

                If Not IsNothing(source) AndAlso source.Rows.Count > 0 Then
                    Caching.SetItem("TabAlarmType", source)
                End If
            End If 
             If Caching.Exist("TabDecisionType") Then
                DirectCast(TabUnderwritingRule_Grid.Columns("DECISION"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = Caching.GetItem("TabDecisionType")

            Else
                Dim source As DataTable = Nothing

                With New DataManagerFactory("SELECT  TABDECISIONTYPE.DECISION, TABDECISIONTYPE.RECORDSTATUS, TRANSDECISIONTYPE.LANGUAGEID, TRANSDECISIONTYPE.DESCRIPTION FROM UNDERWRITING.TABDECISIONTYPE TABDECISIONTYPE JOIN TRANSDECISIONTYPE TRANSDECISIONTYPE ON TRANSDECISIONTYPE.DECISION = TABDECISIONTYPE.DECISION  WHERE TABDECISIONTYPE.RECORDSTATUS = 1 AND TRANSDECISIONTYPE.LANGUAGEID = @:LANGUAGEID  ORDER BY TransDecisionType.Description ASC", "TabDecisionType", "Linked.Underwriting")
                    .AddParameter("LANGUAGEID", DbType.Decimal, 5, False, CurrentState.Get("LanguageId"))
 
                    source = .QueryExecuteToTable(True)
                    DirectCast(TabUnderwritingRule_Grid.Columns("DECISION"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = source
                End With

                If Not IsNothing(source) AndAlso source.Rows.Count > 0 Then
                    Caching.SetItem("TabDecisionType", source)
                End If
            End If 
             If Caching.Exist("TabExclusionPeriodType") Then
                DirectCast(TabUnderwritingRule_Grid.Columns("EXCLUSIONPERIODTYPE"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = Caching.GetItem("TabExclusionPeriodType")

            Else
                Dim source As DataTable = Nothing

                With New DataManagerFactory("SELECT  TABEXCLUSIONPERIODTYPE.EXCLUSIONPERIODTYPE, TABEXCLUSIONPERIODTYPE.RECORDSTATUS, TRANSEXCLUSIONPERIODTYPE.LANGUAGEID, TRANSEXCLUSIONPERIODTYPE.DESCRIPTION FROM UNDERWRITING.TABEXCLUSIONPERIODTYPE TABEXCLUSIONPERIODTYPE JOIN TRANSEXCLUSIONPERIODTYPE TRANSEXCLUSIONPERIODTYPE ON TRANSEXCLUSIONPERIODTYPE.EXCLUSIONPERIODTYPE = TABEXCLUSIONPERIODTYPE.EXCLUSIONPERIODTYPE  WHERE TABEXCLUSIONPERIODTYPE.RECORDSTATUS = 1 AND TRANSEXCLUSIONPERIODTYPE.LANGUAGEID = @:LANGUAGEID  ORDER BY TransExclusionPeriodType.Description ASC", "TabExclusionPeriodType", "Linked.Underwriting")
                    .AddParameter("LANGUAGEID", DbType.Decimal, 5, False, CurrentState.Get("LanguageId"))
 
                    source = .QueryExecuteToTable(True)
                    DirectCast(TabUnderwritingRule_Grid.Columns("EXCLUSIONPERIODTYPE"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = source
                End With

                If Not IsNothing(source) AndAlso source.Rows.Count > 0 Then
                    Caching.SetItem("TabExclusionPeriodType", source)
                End If
            End If 
             If Caching.Exist("TabExclusionType") Then
                DirectCast(TabUnderwritingRule_Grid.Columns("EXCLUSIONTYPE"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = Caching.GetItem("TabExclusionType")

            Else
                Dim source As DataTable = Nothing

                With New DataManagerFactory("SELECT  TABEXCLUSIONTYPE.EXCLUSIONTYPE, TABEXCLUSIONTYPE.RECORDSTATUS, TRANSEXCLUSIONTYPE.LANGUAGEID, TRANSEXCLUSIONTYPE.DESCRIPTION FROM UNDERWRITING.TABEXCLUSIONTYPE TABEXCLUSIONTYPE JOIN TRANSEXCLUSIONTYPE TRANSEXCLUSIONTYPE ON TRANSEXCLUSIONTYPE.EXCLUSIONTYPE = TABEXCLUSIONTYPE.EXCLUSIONTYPE  WHERE TABEXCLUSIONTYPE.RECORDSTATUS = 1 AND TRANSEXCLUSIONTYPE.LANGUAGEID = @:LANGUAGEID  ORDER BY TransExclusionType.Description ASC", "TabExclusionType", "Linked.Underwriting")
                    .AddParameter("LANGUAGEID", DbType.Decimal, 5, False, CurrentState.Get("LanguageId"))
 
                    source = .QueryExecuteToTable(True)
                    DirectCast(TabUnderwritingRule_Grid.Columns("EXCLUSIONTYPE"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = source
                End With

                If Not IsNothing(source) AndAlso source.Rows.Count > 0 Then
                    Caching.SetItem("TabExclusionType", source)
                End If
            End If 
             If Caching.Exist("ImpairmentRules") Then
                DirectCast(TabUnderwritingRule_Grid.Columns("IMPAIRMENTRULEID"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = Caching.GetItem("ImpairmentRules")

            Else
                Dim source As DataTable = Nothing

                With New DataManagerFactory("SELECT  IMPAIRMENTRULES.IMPAIRMENTRULEID, IMPAIRMENTRULES.RECORDSTATUS, IMPAIRMENTRULETRANS.LANGUAGEID, IMPAIRMENTRULETRANS.DESCRIPTION FROM UNDERWRITING.IMPAIRMENTRULES IMPAIRMENTRULES JOIN IMPAIRMENTRULETRANS IMPAIRMENTRULETRANS ON IMPAIRMENTRULETRANS.IMPAIRMENTRULEID = IMPAIRMENTRULES.IMPAIRMENTRULEID  WHERE IMPAIRMENTRULES.RECORDSTATUS = 1 AND IMPAIRMENTRULETRANS.LANGUAGEID = @:LANGUAGEID  ORDER BY ImpairmentRuleTrans.Description ASC", "ImpairmentRules", "Linked.Underwriting")
                    .AddParameter("LANGUAGEID", DbType.Decimal, 5, False, CurrentState.Get("LanguageId"))
 
                    source = .QueryExecuteToTable(True)
                    DirectCast(TabUnderwritingRule_Grid.Columns("IMPAIRMENTRULEID"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = source
                End With

                If Not IsNothing(source) AndAlso source.Rows.Count > 0 Then
                    Caching.SetItem("ImpairmentRules", source)
                End If
            End If 
             If Caching.Exist("TabIllnessType") Then
                DirectCast(TabUnderwritingRule_Grid.Columns("IMPAIRMENTCODE"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = Caching.GetItem("TabIllnessType")

            Else
                Dim source As DataTable = Nothing

                With New DataManagerFactory("SELECT  TABILLNESSTYPE.IMPAIRMENTCODE, TABILLNESSTYPE.RECORDSTATUS, TRANSILLNESSTYPE.LANGUAGEID, TRANSILLNESSTYPE.DESCRIPTION FROM UNDERWRITING.TABILLNESSTYPE TABILLNESSTYPE JOIN TRANSILLNESSTYPE TRANSILLNESSTYPE ON TRANSILLNESSTYPE.IMPAIRMENTCODE = TABILLNESSTYPE.IMPAIRMENTCODE  WHERE TABILLNESSTYPE.RECORDSTATUS = 1 AND TRANSILLNESSTYPE.LANGUAGEID = @:LANGUAGEID  ORDER BY TransIllnessType.Description ASC", "TabIllnessType", "Linked.Underwriting")
                    .AddParameter("LANGUAGEID", DbType.Decimal, 5, False, CurrentState.Get("LanguageId"))
 
                    source = .QueryExecuteToTable(True)
                    DirectCast(TabUnderwritingRule_Grid.Columns("IMPAIRMENTCODE"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = source
                End With

                If Not IsNothing(source) AndAlso source.Rows.Count > 0 Then
                    Caching.SetItem("TabIllnessType", source)
                End If
            End If 
             If Caching.Exist("EnumRecordStatus") Then
                DirectCast(TabUnderwritingRule_Grid.Columns("RECORDSTATUS"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = Caching.GetItem("EnumRecordStatus")

            Else
                Dim source As DataTable = Nothing

                With New DataManagerFactory("SELECT  ENUMRECORDSTATUS.RECORDSTATUS, ENUMRECORDSTATUS.RECORDSTATUSCODE, ETRANRECORDSTATUS.LANGUAGEID, ETRANRECORDSTATUS.DESCRIPTION FROM COMMON.ENUMRECORDSTATUS ENUMRECORDSTATUS JOIN ETRANRECORDSTATUS ETRANRECORDSTATUS ON ETRANRECORDSTATUS.RECORDSTATUS = ENUMRECORDSTATUS.RECORDSTATUS  WHERE ENUMRECORDSTATUS.RECORDSTATUSCODE = '1' AND ETRANRECORDSTATUS.LANGUAGEID = @:LANGUAGEID  ORDER BY ETranRecordStatus.Description ASC", "EnumRecordStatus", "Linked.Common")
                    .AddParameter("LANGUAGEID", DbType.Decimal, 5, False, CurrentState.Get("LanguageId"))
 
                    source = .QueryExecuteToTable(True)
                    DirectCast(TabUnderwritingRule_Grid.Columns("RECORDSTATUS"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = source
                End With

                If Not IsNothing(source) AndAlso source.Rows.Count > 0 Then
                    Caching.SetItem("EnumRecordStatus", source)
                End If
            End If 
 

                 With New DataManagerFactory("SELECT  TABUNDERWRITINGRULE.UNDERWRITINGRULEID, TABUNDERWRITINGRULE.RESTRICTIONTYPE, TABUNDERWRITINGRULE.DEGREEID, TABUNDERWRITINGRULE.LINEOFBUSINESS, TABUNDERWRITINGRULE.PRODUCT, TABUNDERWRITINGRULE.COVERAGECODE, TABUNDERWRITINGRULE.MORTALITYDEBITS, TABUNDERWRITINGRULE.MAXIMUMINSUREDAMOUNT, TABUNDERWRITINGRULE.FLATEXTRAPREMIUM, TABUNDERWRITINGRULE.DOFFLATEXTRAPREMIUMDAYS, TABUNDERWRITINGRULE.DOFFLATEXTRAPREMIUMMONTHS, TABUNDERWRITINGRULE.DOFFLATEXTRAPREMIUMYEARS, TABUNDERWRITINGRULE.ALARMTYPE, TABUNDERWRITINGRULE.DECISION, TABUNDERWRITINGRULE.DECISIONCOMPLEMENT, TABUNDERWRITINGRULE.WAITINGPERIODDAYS, TABUNDERWRITINGRULE.WAITINGPERIODMONTHS, TABUNDERWRITINGRULE.WAITINGPERIODYEARS, TABUNDERWRITINGRULE.EXCLUSIONPERIODTYPE, TABUNDERWRITINGRULE.EXCLUSIONTYPE, TABUNDERWRITINGRULE.IMPAIRMENTRULEID, TABUNDERWRITINGRULE.IMPAIRMENTCODE, TABUNDERWRITINGRULE.RECORDSTATUS, TRANSUNDERWRITINGRULE.UNDERWRITINGRULEID, TRANSUNDERWRITINGRULE.LANGUAGEID, TRANSUNDERWRITINGRULE.DESCRIPTION, TRANSUNDERWRITINGRULE.EXPLANATION FROM UNDERWRITING.TABUNDERWRITINGRULE TABUNDERWRITINGRULE JOIN UNDERWRITING.TRANSUNDERWRITINGRULE TRANSUNDERWRITINGRULE ON TRANSUNDERWRITINGRULE.UNDERWRITINGRULEID = TABUNDERWRITINGRULE.UNDERWRITINGRULEID  WHERE TRANSUNDERWRITINGRULE.LANGUAGEID = @:LANGUAGEID ORDER BY TabUnderwritingRule.UnderwritingRuleId ASC", "TabUnderwritingRule", "Linked.Underwriting")                 
                                                   
                      .AddParameter("LANGUAGEID", DbType.Decimal, 5, False, CurrentState.Get("LanguageId"))
            
                      TabUnderwritingRule_Grid.DataSource = .QueryExecuteToTable(True)
                 End With 
        End If     
    End Sub

    Protected Sub TabUnderwritingRule_Grid_CellEditorInitialize(sender As Object, e As ASPxGridViewEditorEventArgs) Handles TabUnderwritingRule_Grid.CellEditorInitialize
        If TabUnderwritingRule_Grid.IsNewRowEditing Then
            Select Case e.Column.FieldName
                
                
                
                Case "UNDERWRITINGRULEID"                     
                       e.Editor.Focus()               
            End Select

        Else

            Select Case e.Column.FieldName
                Case "UNDERWRITINGRULEID"
     e.Editor.Enabled = False
                   
                
                
                Case "RESTRICTIONTYPE"                     
                     e.Editor.Focus()
            End Select
        End If
        
       Select Case e.Column.FieldName
           Case "UNDERWRITINGRULEID"
                 
                 
           Case "RESTRICTIONTYPE"
     DirectCast(e.Editor, ASPxComboBox).DataBindItems() 
Case "DEGREEID"
     DirectCast(e.Editor, ASPxComboBox).DataBindItems() 
Case "ALARMTYPE"
     DirectCast(e.Editor, ASPxComboBox).DataBindItems() 
Case "DECISION"
     DirectCast(e.Editor, ASPxComboBox).DataBindItems() 
Case "EXCLUSIONPERIODTYPE"
     DirectCast(e.Editor, ASPxComboBox).DataBindItems() 
Case "EXCLUSIONTYPE"
     DirectCast(e.Editor, ASPxComboBox).DataBindItems() 
Case "IMPAIRMENTRULEID"
     DirectCast(e.Editor, ASPxComboBox).DataBindItems() 
Case "IMPAIRMENTCODE"
     DirectCast(e.Editor, ASPxComboBox).DataBindItems() 
Case "RECORDSTATUS"
     DirectCast(e.Editor, ASPxComboBox).DataBindItems() 

        End Select
    End Sub

    Protected Sub TabUnderwritingRule_Grid_RowInserting(ByVal sender As Object, ByVal e As ASPxDataInsertingEventArgs) Handles TabUnderwritingRule_Grid.RowInserting 
        Dim isNullResult As Boolean = True
        
             With New DataManagerFactory("INSERT INTO UNDERWRITING.TabUnderwritingRule (UNDERWRITINGRULEID, RESTRICTIONTYPE, DEGREEID, LINEOFBUSINESS, PRODUCT, COVERAGECODE, MORTALITYDEBITS, MAXIMUMINSUREDAMOUNT, FLATEXTRAPREMIUM, DOFFLATEXTRAPREMIUMDAYS, DOFFLATEXTRAPREMIUMMONTHS, DOFFLATEXTRAPREMIUMYEARS, ALARMTYPE, DECISION, DECISIONCOMPLEMENT, WAITINGPERIODDAYS, WAITINGPERIODMONTHS, WAITINGPERIODYEARS, EXCLUSIONPERIODTYPE, EXCLUSIONTYPE, IMPAIRMENTRULEID, IMPAIRMENTCODE, RECORDSTATUS, CREATORUSERCODE, CREATIONDATE, UPDATEUSERCODE, UPDATEDATE) VALUES (@:UNDERWRITINGRULEID, @:RESTRICTIONTYPE, @:DEGREEID, @:LINEOFBUSINESS, @:PRODUCT, @:COVERAGECODE, @:MORTALITYDEBITS, @:MAXIMUMINSUREDAMOUNT, @:FLATEXTRAPREMIUM, @:DOFFLATEXTRAPREMIUMDAYS, @:DOFFLATEXTRAPREMIUMMONTHS, @:DOFFLATEXTRAPREMIUMYEARS, @:ALARMTYPE, @:DECISION, @:DECISIONCOMPLEMENT, @:WAITINGPERIODDAYS, @:WAITINGPERIODMONTHS, @:WAITINGPERIODYEARS, @:EXCLUSIONPERIODTYPE, @:EXCLUSIONTYPE, @:IMPAIRMENTRULEID, @:IMPAIRMENTCODE, @:RECORDSTATUS, @:CREATORUSERCODE, SYSDATE, @:UPDATEUSERCODE, SYSDATE)", "TabUnderwritingRule", "Linked.Underwriting")                 
                                                   
                       .AddParameter("UNDERWRITINGRULEID", DbType.Decimal, 0, False, e.NewValues("UNDERWRITINGRULEID"))
.AddParameter("RESTRICTIONTYPE", DbType.Decimal, 0, (e.NewValues("RESTRICTIONTYPE") = 0), e.NewValues("RESTRICTIONTYPE"))
.AddParameter("DEGREEID", DbType.Decimal, 0, (e.NewValues("DEGREEID") = 0), e.NewValues("DEGREEID"))
.AddParameter("LINEOFBUSINESS", DbType.Decimal, 0, (e.NewValues("LINEOFBUSINESS") = 0), e.NewValues("LINEOFBUSINESS"))
.AddParameter("PRODUCT", DbType.Decimal, 0, (e.NewValues("PRODUCT") = 0), e.NewValues("PRODUCT"))
.AddParameter("COVERAGECODE", DbType.Decimal, 0, (e.NewValues("COVERAGECODE") = 0), e.NewValues("COVERAGECODE"))
.AddParameter("MORTALITYDEBITS", DbType.Decimal, 0, (e.NewValues("MORTALITYDEBITS") = 0), e.NewValues("MORTALITYDEBITS"))
.AddParameter("MAXIMUMINSUREDAMOUNT", DbType.Decimal, 0, (e.NewValues("MAXIMUMINSUREDAMOUNT") = 0), e.NewValues("MAXIMUMINSUREDAMOUNT"))
.AddParameter("FLATEXTRAPREMIUM", DbType.Decimal, 0, (e.NewValues("FLATEXTRAPREMIUM") = 0), e.NewValues("FLATEXTRAPREMIUM"))
.AddParameter("DOFFLATEXTRAPREMIUMDAYS", DbType.Decimal, 0, (e.NewValues("DOFFLATEXTRAPREMIUMDAYS") = 0), e.NewValues("DOFFLATEXTRAPREMIUMDAYS"))
.AddParameter("DOFFLATEXTRAPREMIUMMONTHS", DbType.Decimal, 0, (e.NewValues("DOFFLATEXTRAPREMIUMMONTHS") = 0), e.NewValues("DOFFLATEXTRAPREMIUMMONTHS"))
.AddParameter("DOFFLATEXTRAPREMIUMYEARS", DbType.Decimal, 0, (e.NewValues("DOFFLATEXTRAPREMIUMYEARS") = 0), e.NewValues("DOFFLATEXTRAPREMIUMYEARS"))
.AddParameter("ALARMTYPE", DbType.Decimal, 0, (e.NewValues("ALARMTYPE") = 0), e.NewValues("ALARMTYPE"))
.AddParameter("DECISION", DbType.Decimal, 0, (e.NewValues("DECISION") = 0), e.NewValues("DECISION"))
.AddParameter("DECISIONCOMPLEMENT", DbType.AnsiString, 0, (e.NewValues("DECISIONCOMPLEMENT") = String.Empty), e.NewValues("DECISIONCOMPLEMENT"))
.AddParameter("WAITINGPERIODDAYS", DbType.Decimal, 0, (e.NewValues("WAITINGPERIODDAYS") = 0), e.NewValues("WAITINGPERIODDAYS"))
.AddParameter("WAITINGPERIODMONTHS", DbType.Decimal, 0, (e.NewValues("WAITINGPERIODMONTHS") = 0), e.NewValues("WAITINGPERIODMONTHS"))
.AddParameter("WAITINGPERIODYEARS", DbType.Decimal, 0, (e.NewValues("WAITINGPERIODYEARS") = 0), e.NewValues("WAITINGPERIODYEARS"))
.AddParameter("EXCLUSIONPERIODTYPE", DbType.Decimal, 0, (e.NewValues("EXCLUSIONPERIODTYPE") = 0), e.NewValues("EXCLUSIONPERIODTYPE"))
.AddParameter("EXCLUSIONTYPE", DbType.Decimal, 0, (e.NewValues("EXCLUSIONTYPE") = 0), e.NewValues("EXCLUSIONTYPE"))
.AddParameter("IMPAIRMENTRULEID", DbType.Decimal, 0, (e.NewValues("IMPAIRMENTRULEID") = 0), e.NewValues("IMPAIRMENTRULEID"))
.AddParameter("IMPAIRMENTCODE", DbType.AnsiString, 0, (e.NewValues("IMPAIRMENTCODE") = String.Empty), e.NewValues("IMPAIRMENTCODE"))
.AddParameter("RECORDSTATUS", DbType.Decimal, 0, False, e.NewValues("RECORDSTATUS"))
.AddParameter("CREATORUSERCODE", DbType.Decimal, 0, False, Session("nUserCode"))
.AddParameter("UPDATEUSERCODE", DbType.Decimal, 0, False, Session("nUserCode"))
            
                       .CommandExecute()
                End With
            For Each languageItem As KeyValuePair(Of Integer, String) In LanguageToDictionary(LanguageId)
                     With New DataManagerFactory("INSERT INTO UNDERWRITING.TransUnderwritingRule (UNDERWRITINGRULEID, LANGUAGEID, DESCRIPTION, EXPLANATION, CREATORUSERCODE, CREATIONDATE, UPDATEUSERCODE, UPDATEDATE) VALUES (@:UNDERWRITINGRULEID, @:LANGUAGEID, @:DESCRIPTION, @:EXPLANATION, @:CREATORUSERCODE, SYSDATE, @:UPDATEUSERCODE, SYSDATE)", "TransUnderwritingRule", "Linked.Underwriting")                 
                                                   
                       .AddParameter("UNDERWRITINGRULEID", DbType.Decimal, 0, False, e.NewValues("UNDERWRITINGRULEID"))
.AddParameter("LANGUAGEID", DbType.Decimal, 0, False, languageItem.Key)
.AddParameter("DESCRIPTION", DbType.AnsiString, 0, False, e.NewValues("DESCRIPTION"))
.AddParameter("EXPLANATION", DbType.AnsiString, 0, (e.NewValues("EXPLANATION") = String.Empty), e.NewValues("EXPLANATION"))
.AddParameter("CREATORUSERCODE", DbType.Decimal, 0, False, Session("nUserCode"))
.AddParameter("UPDATEUSERCODE", DbType.Decimal, 0, False, Session("nUserCode"))
            
                       .CommandExecute()
                End With
           Next

               
        e.Cancel = True
        TabUnderwritingRule_Grid.CancelEdit()
    End Sub

    Protected Sub TabUnderwritingRule_Grid_RowUpdating(ByVal sender As Object, ByVal e As ASPxDataUpdatingEventArgs) Handles TabUnderwritingRule_Grid.RowUpdating
        Dim isNullResult As Boolean = True
          
             With New DataManagerFactory("UPDATE UNDERWRITING.TabUnderwritingRule SET RESTRICTIONTYPE = @:RESTRICTIONTYPE, DEGREEID = @:DEGREEID, LINEOFBUSINESS = @:LINEOFBUSINESS, PRODUCT = @:PRODUCT, COVERAGECODE = @:COVERAGECODE, MORTALITYDEBITS = @:MORTALITYDEBITS, MAXIMUMINSUREDAMOUNT = @:MAXIMUMINSUREDAMOUNT, FLATEXTRAPREMIUM = @:FLATEXTRAPREMIUM, DOFFLATEXTRAPREMIUMDAYS = @:DOFFLATEXTRAPREMIUMDAYS, DOFFLATEXTRAPREMIUMMONTHS = @:DOFFLATEXTRAPREMIUMMONTHS, DOFFLATEXTRAPREMIUMYEARS = @:DOFFLATEXTRAPREMIUMYEARS, ALARMTYPE = @:ALARMTYPE, DECISION = @:DECISION, DECISIONCOMPLEMENT = @:DECISIONCOMPLEMENT, WAITINGPERIODDAYS = @:WAITINGPERIODDAYS, WAITINGPERIODMONTHS = @:WAITINGPERIODMONTHS, WAITINGPERIODYEARS = @:WAITINGPERIODYEARS, EXCLUSIONPERIODTYPE = @:EXCLUSIONPERIODTYPE, EXCLUSIONTYPE = @:EXCLUSIONTYPE, IMPAIRMENTRULEID = @:IMPAIRMENTRULEID, IMPAIRMENTCODE = @:IMPAIRMENTCODE, RECORDSTATUS = @:RECORDSTATUS, UPDATEUSERCODE = @:UPDATEUSERCODE WHERE UNDERWRITINGRULEID = @:UNDERWRITINGRULEID", "TabUnderwritingRule", "Linked.Underwriting")                 
                                                   
                       .AddParameter("RESTRICTIONTYPE", DbType.Decimal, 0, (e.NewValues("RESTRICTIONTYPE") = 0), e.NewValues("RESTRICTIONTYPE"))
.AddParameter("DEGREEID", DbType.Decimal, 0, (e.NewValues("DEGREEID") = 0), e.NewValues("DEGREEID"))
.AddParameter("LINEOFBUSINESS", DbType.Decimal, 0, (e.NewValues("LINEOFBUSINESS") = 0), e.NewValues("LINEOFBUSINESS"))
.AddParameter("PRODUCT", DbType.Decimal, 0, (e.NewValues("PRODUCT") = 0), e.NewValues("PRODUCT"))
.AddParameter("COVERAGECODE", DbType.Decimal, 0, (e.NewValues("COVERAGECODE") = 0), e.NewValues("COVERAGECODE"))
.AddParameter("MORTALITYDEBITS", DbType.Decimal, 0, (e.NewValues("MORTALITYDEBITS") = 0), e.NewValues("MORTALITYDEBITS"))
.AddParameter("MAXIMUMINSUREDAMOUNT", DbType.Decimal, 0, (e.NewValues("MAXIMUMINSUREDAMOUNT") = 0), e.NewValues("MAXIMUMINSUREDAMOUNT"))
.AddParameter("FLATEXTRAPREMIUM", DbType.Decimal, 0, (e.NewValues("FLATEXTRAPREMIUM") = 0), e.NewValues("FLATEXTRAPREMIUM"))
.AddParameter("DOFFLATEXTRAPREMIUMDAYS", DbType.Decimal, 0, (e.NewValues("DOFFLATEXTRAPREMIUMDAYS") = 0), e.NewValues("DOFFLATEXTRAPREMIUMDAYS"))
.AddParameter("DOFFLATEXTRAPREMIUMMONTHS", DbType.Decimal, 0, (e.NewValues("DOFFLATEXTRAPREMIUMMONTHS") = 0), e.NewValues("DOFFLATEXTRAPREMIUMMONTHS"))
.AddParameter("DOFFLATEXTRAPREMIUMYEARS", DbType.Decimal, 0, (e.NewValues("DOFFLATEXTRAPREMIUMYEARS") = 0), e.NewValues("DOFFLATEXTRAPREMIUMYEARS"))
.AddParameter("ALARMTYPE", DbType.Decimal, 0, (e.NewValues("ALARMTYPE") = 0), e.NewValues("ALARMTYPE"))
.AddParameter("DECISION", DbType.Decimal, 0, (e.NewValues("DECISION") = 0), e.NewValues("DECISION"))
.AddParameter("DECISIONCOMPLEMENT", DbType.AnsiString, 0, (e.NewValues("DECISIONCOMPLEMENT") = String.Empty), e.NewValues("DECISIONCOMPLEMENT"))
.AddParameter("WAITINGPERIODDAYS", DbType.Decimal, 0, (e.NewValues("WAITINGPERIODDAYS") = 0), e.NewValues("WAITINGPERIODDAYS"))
.AddParameter("WAITINGPERIODMONTHS", DbType.Decimal, 0, (e.NewValues("WAITINGPERIODMONTHS") = 0), e.NewValues("WAITINGPERIODMONTHS"))
.AddParameter("WAITINGPERIODYEARS", DbType.Decimal, 0, (e.NewValues("WAITINGPERIODYEARS") = 0), e.NewValues("WAITINGPERIODYEARS"))
.AddParameter("EXCLUSIONPERIODTYPE", DbType.Decimal, 0, (e.NewValues("EXCLUSIONPERIODTYPE") = 0), e.NewValues("EXCLUSIONPERIODTYPE"))
.AddParameter("EXCLUSIONTYPE", DbType.Decimal, 0, (e.NewValues("EXCLUSIONTYPE") = 0), e.NewValues("EXCLUSIONTYPE"))
.AddParameter("IMPAIRMENTRULEID", DbType.Decimal, 0, (e.NewValues("IMPAIRMENTRULEID") = 0), e.NewValues("IMPAIRMENTRULEID"))
.AddParameter("IMPAIRMENTCODE", DbType.AnsiString, 0, (e.NewValues("IMPAIRMENTCODE") = String.Empty), e.NewValues("IMPAIRMENTCODE"))
.AddParameter("RECORDSTATUS", DbType.Decimal, 0, False, e.NewValues("RECORDSTATUS"))
.AddParameter("UPDATEUSERCODE", DbType.Decimal, 0, False, Session("nUserCode"))
.AddParameter("UNDERWRITINGRULEID", DbType.Decimal, 0, False, e.Keys("UNDERWRITINGRULEID"))
            
                       .CommandExecute()
                End With
     With New DataManagerFactory("UPDATE UNDERWRITING.TransUnderwritingRule SET DESCRIPTION = @:DESCRIPTION, EXPLANATION = @:EXPLANATION, UPDATEUSERCODE = @:UPDATEUSERCODE WHERE UNDERWRITINGRULEID = @:UNDERWRITINGRULEID AND LANGUAGEID = @:LANGUAGEID", "TransUnderwritingRule", "Linked.Underwriting")                 
                                                   
                       .AddParameter("DESCRIPTION", DbType.AnsiString, 0, False, e.NewValues("DESCRIPTION"))
.AddParameter("EXPLANATION", DbType.AnsiString, 0, (e.NewValues("EXPLANATION") = String.Empty), e.NewValues("EXPLANATION"))
.AddParameter("UPDATEUSERCODE", DbType.Decimal, 0, False, Session("nUserCode"))
.AddParameter("UNDERWRITINGRULEID", DbType.Decimal, 0, False, e.Keys("UNDERWRITINGRULEID"))
.AddParameter("LANGUAGEID", DbType.Decimal, 0, False, CurrentState.Get("LanguageId"))
            
                       .CommandExecute()
                End With         
      
         e.Cancel = True
        TabUnderwritingRule_Grid.CancelEdit()
    End Sub
    
    Protected Sub TabUnderwritingRule_Grid_CustomCallback(sender As Object, e As ASPxGridViewCustomCallbackEventArgs) Handles TabUnderwritingRule_Grid.CustomCallback     
           Dim isNullResult As Boolean = True
           
           Select Case e.Parameters.ToString.ToLower
               Case "delete"
                       Dim UNDERWRITINGRULEIDKey As Generic.List(Of Object) = TabUnderwritingRule_Grid.GetSelectedFieldValues("UNDERWRITINGRULEID")
        
               For index As Integer = 0 To UNDERWRITINGRULEIDKey.Count - 1
                  With New DataManagerFactory("DELETE FROM UNDERWRITING.TransUnderwritingRule WHERE UNDERWRITINGRULEID = @:UNDERWRITINGRULEID ", "TransUnderwritingRule", "Linked.Underwriting")                 
                                                   
               .AddParameter("UNDERWRITINGRULEID", DbType.Decimal, 0, False, UNDERWRITINGRULEIDKey(index))
            
               .CommandExecute()
          End With 
 With New DataManagerFactory("DELETE FROM UNDERWRITING.TabUnderwritingRule WHERE UNDERWRITINGRULEID = @:UNDERWRITINGRULEID ", "TabUnderwritingRule", "Linked.Underwriting")                 
                                                   
               .AddParameter("UNDERWRITINGRULEID", DbType.Decimal, 0, False, UNDERWRITINGRULEIDKey(index))
            
               .CommandExecute()
          End With 
                       
              Next           
           
              TabUnderwritingRule_Grid.DataBind()
                 
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
    
    Protected Sub TabUnderwritingRule_Grid_RowValidating(sender As Object, e As ASPxDataValidationEventArgs) Handles TabUnderwritingRule_Grid.RowValidating
        Dim errorMessage As String = "<ol style='font-weight:lighter'>"
        
        If IsNothing(e.NewValues("UNDERWRITINGRULEID")) OrElse e.NewValues("UNDERWRITINGRULEID") = 0  
   e.Errors(TabUnderwritingRule_Grid.Columns("UNDERWRITINGRULEID")) = GetLocalResourceObject("UNDERWRITINGRULEIDMessageErrorRequired1Resource").ToString
End If
 
        
        If e.IsNewRow Then
           Dim rowCountKey As System.Int32
  With New DataManagerFactory("SELECT  TABUNDERWRITINGRULE.UNDERWRITINGRULEID ROWCOUNT FROM UNDERWRITING.TABUNDERWRITINGRULE TABUNDERWRITINGRULE  WHERE TABUNDERWRITINGRULE.UNDERWRITINGRULEID = @:UNDERWRITINGRULEID", "TabUnderwritingRule", "Linked.Underwriting")
             .AddParameter("UNDERWRITINGRULEID", DbType.Decimal, 5, False, e.NewValues("UNDERWRITINGRULEID"))
 
             rowCountKey = .QueryExecuteScalarToInteger()  
        End With
        If rowCountKey > 0 Then
                errorMessage += String.Format(CultureInfo.InvariantCulture, "</ol><ul style='font-weight:bold'>{0}</ul>", 
                                                                            GetLocalResourceObject("TabUnderwritingRule_GridMessageErrorGeneralValidator0Resource").ToString)                
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
 
#Region "TransUnderwritingRule_Grid Events"
    
    Protected Sub TransUnderwritingRule_Grid_CustomColumnDisplayText(sender As Object, e As DevExpress.Web.ASPxGridView.ASPxGridViewColumnDisplayTextEventArgs) Handles TransUnderwritingRule_Grid.CustomColumnDisplayText
          Dim data As DataTable
          Dim rows() As DataRow
           
          Select Case e.Column.FieldName

               Case Else              
           End Select
    End Sub
  
    Protected Sub TransUnderwritingRule_Grid_DataBinding(ByVal sender As Object, ByVal e As EventArgs) Handles TransUnderwritingRule_Grid.DataBinding
        If (Not IsNothing(Request.Params("__CALLBACKID")) AndAlso Request.Params("__CALLBACKID").EndsWith("TransUnderwritingRule_Grid")) Or _internalCall Then
                       If Caching.Exist("TabRestrictionType") Then
                DirectCast(TransUnderwritingRule_Grid.Columns("RESTRICTIONTYPE"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = Caching.GetItem("TabRestrictionType")

            Else
                Dim source As DataTable = Nothing

                With New DataManagerFactory("SELECT  TABRESTRICTIONTYPE.RESTRICTIONTYPE, TABRESTRICTIONTYPE.RECORDSTATUS, TRANSRESTRICTIONTYPE.LANGUAGEID, TRANSRESTRICTIONTYPE.DESCRIPTION FROM UNDERWRITING.TABRESTRICTIONTYPE TABRESTRICTIONTYPE JOIN TRANSRESTRICTIONTYPE TRANSRESTRICTIONTYPE ON TRANSRESTRICTIONTYPE.RESTRICTIONTYPE = TABRESTRICTIONTYPE.RESTRICTIONTYPE  WHERE TABRESTRICTIONTYPE.RECORDSTATUS = 1 AND TRANSRESTRICTIONTYPE.LANGUAGEID = @:LANGUAGEID  ORDER BY TransRestrictionType.Description ASC", "TabRestrictionType", "Linked.Underwriting")
                    .AddParameter("LANGUAGEID", DbType.Decimal, 5, False, CurrentState.Get("LanguageId"))
 
                    source = .QueryExecuteToTable(True)
                    DirectCast(TransUnderwritingRule_Grid.Columns("RESTRICTIONTYPE"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = source
                End With

                If Not IsNothing(source) AndAlso source.Rows.Count > 0 Then
                    Caching.SetItem("TabRestrictionType", source)
                End If
            End If 
             If Caching.Exist("TabDegree") Then
                DirectCast(TransUnderwritingRule_Grid.Columns("DEGREEID"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = Caching.GetItem("TabDegree")

            Else
                Dim source As DataTable = Nothing

                With New DataManagerFactory("SELECT  TABDEGREE.DEGREEID, TABDEGREE.RECORDSTATUS, TRANSDEGREE.LANGUAGEID, TRANSDEGREE.DESCRIPTION FROM UNDERWRITING.TABDEGREE TABDEGREE JOIN TRANSDEGREE TRANSDEGREE ON TRANSDEGREE.DEGREEID = TABDEGREE.DEGREEID  WHERE TABDEGREE.RECORDSTATUS = 1 AND TRANSDEGREE.LANGUAGEID = @:LANGUAGEID  ORDER BY TransDegree.Description ASC", "TabDegree", "Linked.Underwriting")
                    .AddParameter("LANGUAGEID", DbType.Decimal, 5, False, CurrentState.Get("LanguageId"))
 
                    source = .QueryExecuteToTable(True)
                    DirectCast(TransUnderwritingRule_Grid.Columns("DEGREEID"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = source
                End With

                If Not IsNothing(source) AndAlso source.Rows.Count > 0 Then
                    Caching.SetItem("TabDegree", source)
                End If
            End If 
             If Caching.Exist("TabAlarmType") Then
                DirectCast(TransUnderwritingRule_Grid.Columns("ALARMTYPE"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = Caching.GetItem("TabAlarmType")

            Else
                Dim source As DataTable = Nothing

                With New DataManagerFactory("SELECT  TABALARMTYPE.ALARMTYPE, TABALARMTYPE.RECORDSTATUS, TRANSALARMTYPE.LANGUAGEID, TRANSALARMTYPE.DESCRIPTION FROM UNDERWRITING.TABALARMTYPE TABALARMTYPE JOIN TRANSALARMTYPE TRANSALARMTYPE ON TRANSALARMTYPE.ALARMTYPE = TABALARMTYPE.ALARMTYPE  WHERE TABALARMTYPE.RECORDSTATUS = 1 AND TRANSALARMTYPE.LANGUAGEID = @:LANGUAGEID  ORDER BY TransAlarmType.Description ASC", "TabAlarmType", "Linked.Underwriting")
                    .AddParameter("LANGUAGEID", DbType.Decimal, 5, False, CurrentState.Get("LanguageId"))
 
                    source = .QueryExecuteToTable(True)
                    DirectCast(TransUnderwritingRule_Grid.Columns("ALARMTYPE"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = source
                End With

                If Not IsNothing(source) AndAlso source.Rows.Count > 0 Then
                    Caching.SetItem("TabAlarmType", source)
                End If
            End If 
             If Caching.Exist("TabDecisionType") Then
                DirectCast(TransUnderwritingRule_Grid.Columns("DECISION"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = Caching.GetItem("TabDecisionType")

            Else
                Dim source As DataTable = Nothing

                With New DataManagerFactory("SELECT  TABDECISIONTYPE.DECISION, TABDECISIONTYPE.RECORDSTATUS, TRANSDECISIONTYPE.LANGUAGEID, TRANSDECISIONTYPE.DESCRIPTION FROM UNDERWRITING.TABDECISIONTYPE TABDECISIONTYPE JOIN TRANSDECISIONTYPE TRANSDECISIONTYPE ON TRANSDECISIONTYPE.DECISION = TABDECISIONTYPE.DECISION  WHERE TABDECISIONTYPE.RECORDSTATUS = 1 AND TRANSDECISIONTYPE.LANGUAGEID = @:LANGUAGEID  ORDER BY TransDecisionType.Description ASC", "TabDecisionType", "Linked.Underwriting")
                    .AddParameter("LANGUAGEID", DbType.Decimal, 5, False, CurrentState.Get("LanguageId"))
 
                    source = .QueryExecuteToTable(True)
                    DirectCast(TransUnderwritingRule_Grid.Columns("DECISION"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = source
                End With

                If Not IsNothing(source) AndAlso source.Rows.Count > 0 Then
                    Caching.SetItem("TabDecisionType", source)
                End If
            End If 
             If Caching.Exist("TabExclusionPeriodType") Then
                DirectCast(TransUnderwritingRule_Grid.Columns("EXCLUSIONPERIODTYPE"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = Caching.GetItem("TabExclusionPeriodType")

            Else
                Dim source As DataTable = Nothing

                With New DataManagerFactory("SELECT  TABEXCLUSIONPERIODTYPE.EXCLUSIONPERIODTYPE, TABEXCLUSIONPERIODTYPE.RECORDSTATUS, TRANSEXCLUSIONPERIODTYPE.LANGUAGEID, TRANSEXCLUSIONPERIODTYPE.DESCRIPTION FROM UNDERWRITING.TABEXCLUSIONPERIODTYPE TABEXCLUSIONPERIODTYPE JOIN TRANSEXCLUSIONPERIODTYPE TRANSEXCLUSIONPERIODTYPE ON TRANSEXCLUSIONPERIODTYPE.EXCLUSIONPERIODTYPE = TABEXCLUSIONPERIODTYPE.EXCLUSIONPERIODTYPE  WHERE TABEXCLUSIONPERIODTYPE.RECORDSTATUS = 1 AND TRANSEXCLUSIONPERIODTYPE.LANGUAGEID = @:LANGUAGEID  ORDER BY TransExclusionPeriodType.Description ASC", "TabExclusionPeriodType", "Linked.Underwriting")
                    .AddParameter("LANGUAGEID", DbType.Decimal, 5, False, CurrentState.Get("LanguageId"))
 
                    source = .QueryExecuteToTable(True)
                    DirectCast(TransUnderwritingRule_Grid.Columns("EXCLUSIONPERIODTYPE"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = source
                End With

                If Not IsNothing(source) AndAlso source.Rows.Count > 0 Then
                    Caching.SetItem("TabExclusionPeriodType", source)
                End If
            End If 
             If Caching.Exist("TabExclusionType") Then
                DirectCast(TransUnderwritingRule_Grid.Columns("EXCLUSIONTYPE"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = Caching.GetItem("TabExclusionType")

            Else
                Dim source As DataTable = Nothing

                With New DataManagerFactory("SELECT  TABEXCLUSIONTYPE.EXCLUSIONTYPE, TABEXCLUSIONTYPE.RECORDSTATUS, TRANSEXCLUSIONTYPE.LANGUAGEID, TRANSEXCLUSIONTYPE.DESCRIPTION FROM UNDERWRITING.TABEXCLUSIONTYPE TABEXCLUSIONTYPE JOIN TRANSEXCLUSIONTYPE TRANSEXCLUSIONTYPE ON TRANSEXCLUSIONTYPE.EXCLUSIONTYPE = TABEXCLUSIONTYPE.EXCLUSIONTYPE  WHERE TABEXCLUSIONTYPE.RECORDSTATUS = 1 AND TRANSEXCLUSIONTYPE.LANGUAGEID = @:LANGUAGEID  ORDER BY TransExclusionType.Description ASC", "TabExclusionType", "Linked.Underwriting")
                    .AddParameter("LANGUAGEID", DbType.Decimal, 5, False, CurrentState.Get("LanguageId"))
 
                    source = .QueryExecuteToTable(True)
                    DirectCast(TransUnderwritingRule_Grid.Columns("EXCLUSIONTYPE"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = source
                End With

                If Not IsNothing(source) AndAlso source.Rows.Count > 0 Then
                    Caching.SetItem("TabExclusionType", source)
                End If
            End If 
             If Caching.Exist("ImpairmentRules") Then
                DirectCast(TransUnderwritingRule_Grid.Columns("IMPAIRMENTRULEID"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = Caching.GetItem("ImpairmentRules")

            Else
                Dim source As DataTable = Nothing

                With New DataManagerFactory("SELECT  IMPAIRMENTRULES.IMPAIRMENTRULEID, IMPAIRMENTRULES.RECORDSTATUS, IMPAIRMENTRULETRANS.LANGUAGEID, IMPAIRMENTRULETRANS.DESCRIPTION FROM UNDERWRITING.IMPAIRMENTRULES IMPAIRMENTRULES JOIN IMPAIRMENTRULETRANS IMPAIRMENTRULETRANS ON IMPAIRMENTRULETRANS.IMPAIRMENTRULEID = IMPAIRMENTRULES.IMPAIRMENTRULEID  WHERE IMPAIRMENTRULES.RECORDSTATUS = 1 AND IMPAIRMENTRULETRANS.LANGUAGEID = @:LANGUAGEID  ORDER BY ImpairmentRuleTrans.Description ASC", "ImpairmentRules", "Linked.Underwriting")
                    .AddParameter("LANGUAGEID", DbType.Decimal, 5, False, CurrentState.Get("LanguageId"))
 
                    source = .QueryExecuteToTable(True)
                    DirectCast(TransUnderwritingRule_Grid.Columns("IMPAIRMENTRULEID"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = source
                End With

                If Not IsNothing(source) AndAlso source.Rows.Count > 0 Then
                    Caching.SetItem("ImpairmentRules", source)
                End If
            End If 
             If Caching.Exist("TabIllnessType") Then
                DirectCast(TransUnderwritingRule_Grid.Columns("IMPAIRMENTCODE"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = Caching.GetItem("TabIllnessType")

            Else
                Dim source As DataTable = Nothing

                With New DataManagerFactory("SELECT  TABILLNESSTYPE.IMPAIRMENTCODE, TABILLNESSTYPE.RECORDSTATUS, TRANSILLNESSTYPE.LANGUAGEID, TRANSILLNESSTYPE.DESCRIPTION FROM UNDERWRITING.TABILLNESSTYPE TABILLNESSTYPE JOIN TRANSILLNESSTYPE TRANSILLNESSTYPE ON TRANSILLNESSTYPE.IMPAIRMENTCODE = TABILLNESSTYPE.IMPAIRMENTCODE  WHERE TABILLNESSTYPE.RECORDSTATUS = 1 AND TRANSILLNESSTYPE.LANGUAGEID = @:LANGUAGEID  ORDER BY TransIllnessType.Description ASC", "TabIllnessType", "Linked.Underwriting")
                    .AddParameter("LANGUAGEID", DbType.Decimal, 5, False, CurrentState.Get("LanguageId"))
 
                    source = .QueryExecuteToTable(True)
                    DirectCast(TransUnderwritingRule_Grid.Columns("IMPAIRMENTCODE"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = source
                End With

                If Not IsNothing(source) AndAlso source.Rows.Count > 0 Then
                    Caching.SetItem("TabIllnessType", source)
                End If
            End If 
             If Caching.Exist("EnumRecordStatus") Then
                DirectCast(TransUnderwritingRule_Grid.Columns("RECORDSTATUS"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = Caching.GetItem("EnumRecordStatus")

            Else
                Dim source As DataTable = Nothing

                With New DataManagerFactory("SELECT  ENUMRECORDSTATUS.RECORDSTATUS, ENUMRECORDSTATUS.RECORDSTATUSCODE, ETRANRECORDSTATUS.LANGUAGEID, ETRANRECORDSTATUS.DESCRIPTION FROM COMMON.ENUMRECORDSTATUS ENUMRECORDSTATUS JOIN ETRANRECORDSTATUS ETRANRECORDSTATUS ON ETRANRECORDSTATUS.RECORDSTATUS = ENUMRECORDSTATUS.RECORDSTATUS  WHERE ENUMRECORDSTATUS.RECORDSTATUSCODE = '1' AND ETRANRECORDSTATUS.LANGUAGEID = @:LANGUAGEID  ORDER BY ETranRecordStatus.Description ASC", "EnumRecordStatus", "Linked.Common")
                    .AddParameter("LANGUAGEID", DbType.Decimal, 5, False, CurrentState.Get("LanguageId"))
 
                    source = .QueryExecuteToTable(True)
                    DirectCast(TransUnderwritingRule_Grid.Columns("RECORDSTATUS"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = source
                End With

                If Not IsNothing(source) AndAlso source.Rows.Count > 0 Then
                    Caching.SetItem("EnumRecordStatus", source)
                End If
            End If 
             If Caching.Exist("TabLanguage") Then
                DirectCast(TransUnderwritingRule_Grid.Columns("LANGUAGEID"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = Caching.GetItem("TabLanguage")

            Else
                Dim source As DataTable = Nothing

                With New DataManagerFactory("SELECT  TABLANGUAGE.LANGUAGEID, TABLANGUAGE.RECORDSTATUS, TRANSLANGUAGE.LANGUAGEID, TRANSLANGUAGE.DESCRIPTION FROM COMMON.TABLANGUAGE TABLANGUAGE JOIN TRANSLANGUAGE TRANSLANGUAGE ON TRANSLANGUAGE.LANGUAGECODEID = TABLANGUAGE.LANGUAGEID  WHERE TABLANGUAGE.RECORDSTATUS = '1' AND TRANSLANGUAGE.LANGUAGEID = @:LANGUAGEID  ORDER BY TransLanguage.Description ASC", "TabLanguage", "Linked.Common")
                    .AddParameter("LANGUAGEID", DbType.Decimal, 5, False, CurrentState.Get("LanguageId"))
 
                    source = .QueryExecuteToTable(True)
                    DirectCast(TransUnderwritingRule_Grid.Columns("LANGUAGEID"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = source
                End With

                If Not IsNothing(source) AndAlso source.Rows.Count > 0 Then
                    Caching.SetItem("TabLanguage", source)
                End If
            End If 
 

                 With New DataManagerFactory("SELECT  TABUNDERWRITINGRULE.UNDERWRITINGRULEID, TABUNDERWRITINGRULE.RESTRICTIONTYPE, TABUNDERWRITINGRULE.DEGREEID, TABUNDERWRITINGRULE.LINEOFBUSINESS, TABUNDERWRITINGRULE.PRODUCT, TABUNDERWRITINGRULE.COVERAGECODE, TABUNDERWRITINGRULE.MORTALITYDEBITS, TABUNDERWRITINGRULE.MAXIMUMINSUREDAMOUNT, TABUNDERWRITINGRULE.FLATEXTRAPREMIUM, TABUNDERWRITINGRULE.DOFFLATEXTRAPREMIUMDAYS, TABUNDERWRITINGRULE.DOFFLATEXTRAPREMIUMMONTHS, TABUNDERWRITINGRULE.DOFFLATEXTRAPREMIUMYEARS, TABUNDERWRITINGRULE.ALARMTYPE, TABUNDERWRITINGRULE.DECISION, TABUNDERWRITINGRULE.DECISIONCOMPLEMENT, TABUNDERWRITINGRULE.WAITINGPERIODDAYS, TABUNDERWRITINGRULE.WAITINGPERIODMONTHS, TABUNDERWRITINGRULE.WAITINGPERIODYEARS, TABUNDERWRITINGRULE.EXCLUSIONPERIODTYPE, TABUNDERWRITINGRULE.EXCLUSIONTYPE, TABUNDERWRITINGRULE.IMPAIRMENTRULEID, TABUNDERWRITINGRULE.IMPAIRMENTCODE, TABUNDERWRITINGRULE.RECORDSTATUS, TRANSUNDERWRITINGRULE.UNDERWRITINGRULEID, TRANSUNDERWRITINGRULE.LANGUAGEID, TRANSUNDERWRITINGRULE.DESCRIPTION, TRANSUNDERWRITINGRULE.EXPLANATION FROM UNDERWRITING.TABUNDERWRITINGRULE TABUNDERWRITINGRULE JOIN UNDERWRITING.TRANSUNDERWRITINGRULE TRANSUNDERWRITINGRULE ON TRANSUNDERWRITINGRULE.UNDERWRITINGRULEID = TABUNDERWRITINGRULE.UNDERWRITINGRULEID   ORDER BY TabUnderwritingRule.UnderwritingRuleId ASC", "TabUnderwritingRule", "Linked.Underwriting")                 
                                                   
                                  
                      TransUnderwritingRule_Grid.DataSource = .QueryExecuteToTable(True)
                 End With 
        End If     
    End Sub

    Protected Sub TransUnderwritingRule_Grid_CellEditorInitialize(sender As Object, e As ASPxGridViewEditorEventArgs) Handles TransUnderwritingRule_Grid.CellEditorInitialize
        If TransUnderwritingRule_Grid.IsNewRowEditing Then
            Select Case e.Column.FieldName
                
                
                
                Case "UNDERWRITINGRULEID"                     
                       e.Editor.Focus()               
            End Select

        Else

            Select Case e.Column.FieldName
                Case "UNDERWRITINGRULEID"
     e.Editor.Enabled = False
Case "LANGUAGEID"
     e.Editor.Enabled = False
                   
                
                
                Case "RESTRICTIONTYPE"                     
                     e.Editor.Focus()
            End Select
        End If
        
       Select Case e.Column.FieldName
           Case "UNDERWRITINGRULEID"
                 
                 
           Case "RESTRICTIONTYPE"
     DirectCast(e.Editor, ASPxComboBox).DataBindItems() 
Case "DEGREEID"
     DirectCast(e.Editor, ASPxComboBox).DataBindItems() 
Case "ALARMTYPE"
     DirectCast(e.Editor, ASPxComboBox).DataBindItems() 
Case "DECISION"
     DirectCast(e.Editor, ASPxComboBox).DataBindItems() 
Case "EXCLUSIONPERIODTYPE"
     DirectCast(e.Editor, ASPxComboBox).DataBindItems() 
Case "EXCLUSIONTYPE"
     DirectCast(e.Editor, ASPxComboBox).DataBindItems() 
Case "IMPAIRMENTRULEID"
     DirectCast(e.Editor, ASPxComboBox).DataBindItems() 
Case "IMPAIRMENTCODE"
     DirectCast(e.Editor, ASPxComboBox).DataBindItems() 
Case "RECORDSTATUS"
     DirectCast(e.Editor, ASPxComboBox).DataBindItems() 
Case "LANGUAGEID"
     DirectCast(e.Editor, ASPxComboBox).DataBindItems() 

        End Select
    End Sub

    Protected Sub TransUnderwritingRule_Grid_RowInserting(ByVal sender As Object, ByVal e As ASPxDataInsertingEventArgs) Handles TransUnderwritingRule_Grid.RowInserting 
        Dim isNullResult As Boolean = True
        
        
               
        e.Cancel = True
        TransUnderwritingRule_Grid.CancelEdit()
    End Sub

    Protected Sub TransUnderwritingRule_Grid_RowUpdating(ByVal sender As Object, ByVal e As ASPxDataUpdatingEventArgs) Handles TransUnderwritingRule_Grid.RowUpdating
        Dim isNullResult As Boolean = True
          
             With New DataManagerFactory("UPDATE UNDERWRITING.TransUnderwritingRule SET DESCRIPTION = @:DESCRIPTION, EXPLANATION = @:EXPLANATION, UPDATEUSERCODE = @:UPDATEUSERCODE WHERE UNDERWRITINGRULEID = @:UNDERWRITINGRULEID AND LANGUAGEID = @:LANGUAGEID", "TransUnderwritingRule", "Linked.Underwriting")                 
                                                   
                       .AddParameter("DESCRIPTION", DbType.AnsiString, 0, False, e.NewValues("DESCRIPTION"))
.AddParameter("EXPLANATION", DbType.AnsiString, 0, (e.NewValues("EXPLANATION") = String.Empty), e.NewValues("EXPLANATION"))
.AddParameter("UPDATEUSERCODE", DbType.Decimal, 0, False, Session("nUserCode"))
.AddParameter("UNDERWRITINGRULEID", DbType.Decimal, 0, False, e.Keys("UNDERWRITINGRULEID"))
.AddParameter("LANGUAGEID", DbType.Decimal, 0, False, e.Keys("LANGUAGEID"))
            
                       .CommandExecute()
                End With         
      
         e.Cancel = True
        TransUnderwritingRule_Grid.CancelEdit()
    End Sub
    
    Protected Sub TransUnderwritingRule_Grid_CustomCallback(sender As Object, e As ASPxGridViewCustomCallbackEventArgs) Handles TransUnderwritingRule_Grid.CustomCallback     
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
    
    Protected Sub TransUnderwritingRule_Grid_RowValidating(sender As Object, e As ASPxDataValidationEventArgs) Handles TransUnderwritingRule_Grid.RowValidating
        Dim errorMessage As String = "<ol style='font-weight:lighter'>"
        
        If IsNothing(e.NewValues("UNDERWRITINGRULEID")) OrElse e.NewValues("UNDERWRITINGRULEID") = 0  
   e.Errors(TransUnderwritingRule_Grid.Columns("UNDERWRITINGRULEID")) = GetLocalResourceObject("UNDERWRITINGRULEIDMessageErrorRequired1Resource").ToString
End If
 
        
        If e.IsNewRow Then
           Dim rowCountKey As System.Int32
  With New DataManagerFactory("SELECT  TRANSUNDERWRITINGRULE.UNDERWRITINGRULEID ROWCOUNT, TRANSUNDERWRITINGRULE.LANGUAGEID FROM UNDERWRITING.TRANSUNDERWRITINGRULE TRANSUNDERWRITINGRULE  WHERE TRANSUNDERWRITINGRULE.UNDERWRITINGRULEID = @:UNDERWRITINGRULEID AND TRANSUNDERWRITINGRULE.LANGUAGEID = @:LANGUAGEID", "TransUnderwritingRule", "Linked.Underwriting")
             .AddParameter("UNDERWRITINGRULEID", DbType.Decimal, 5, False, e.NewValues("UNDERWRITINGRULEID"))
.AddParameter("LANGUAGEID", DbType.Decimal, 5, False, e.NewValues("LANGUAGEID"))
 
             rowCountKey = .QueryExecuteScalarToInteger()  
        End With
        If rowCountKey > 0 Then
                errorMessage += String.Format(CultureInfo.InvariantCulture, "</ol><ul style='font-weight:bold'>{0}</ul>", 
                                                                            GetLocalResourceObject("TransUnderwritingRule_GridMessageErrorGeneralValidator0Resource").ToString)                
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