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

Partial Class Maintenance_ImpairmentRules
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

            ImpairmentRules_Grid.DataBind()
        End If      
    End Sub

#End Region

#Region "Controls Events"

         
    
 

#End Region

#Region "ImpairmentRules_Grid Events"
    
    Protected Sub ImpairmentRules_Grid_CustomColumnDisplayText(sender As Object, e As DevExpress.Web.ASPxGridView.ASPxGridViewColumnDisplayTextEventArgs) Handles ImpairmentRules_Grid.CustomColumnDisplayText
          Dim data As DataTable
          Dim rows() As DataRow
           
          Select Case e.Column.FieldName

               Case Else              
           End Select
    End Sub
  
    Protected Sub ImpairmentRules_Grid_DataBinding(ByVal sender As Object, ByVal e As EventArgs) Handles ImpairmentRules_Grid.DataBinding
        If (Not IsNothing(Request.Params("__CALLBACKID")) AndAlso Request.Params("__CALLBACKID").EndsWith("ImpairmentRules_Grid")) Or _internalCall Then
                       If Caching.Exist("TabIllnessType") Then
                DirectCast(ImpairmentRules_Grid.Columns("IMPAIRMENTCODE"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = Caching.GetItem("TabIllnessType")

            Else
                Dim source As DataTable = Nothing

                With New DataManagerFactory("SELECT  TABILLNESSTYPE.IMPAIRMENTCODE, TABILLNESSTYPE.RECORDSTATUS, TRANSILLNESSTYPE.LANGUAGEID, TRANSILLNESSTYPE.DESCRIPTION FROM UNDERWRITING.TABILLNESSTYPE TABILLNESSTYPE JOIN TRANSILLNESSTYPE TRANSILLNESSTYPE ON TRANSILLNESSTYPE.IMPAIRMENTCODE = TABILLNESSTYPE.IMPAIRMENTCODE  WHERE TABILLNESSTYPE.RECORDSTATUS = 1 AND TRANSILLNESSTYPE.LANGUAGEID = @:LANGUAGEID  ORDER BY TransIllnessType.Description ASC", "TabIllnessType", "Linked.Underwriting")
                    .AddParameter("LANGUAGEID", DbType.Decimal, 5, False, CurrentState.Get("LanguageId"))
 
                    source = .QueryExecuteToTable(True)
                    DirectCast(ImpairmentRules_Grid.Columns("IMPAIRMENTCODE"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = source
                End With

                If Not IsNothing(source) AndAlso source.Rows.Count > 0 Then
                    Caching.SetItem("TabIllnessType", source)
                End If
            End If 
             If Caching.Exist("TabDegree") Then
                DirectCast(ImpairmentRules_Grid.Columns("DEGREEID"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = Caching.GetItem("TabDegree")

            Else
                Dim source As DataTable = Nothing

                With New DataManagerFactory("SELECT  TABDEGREE.DEGREEID, TABDEGREE.RECORDSTATUS, TRANSDEGREE.LANGUAGEID, TRANSDEGREE.DESCRIPTION FROM UNDERWRITING.TABDEGREE TABDEGREE JOIN TRANSDEGREE TRANSDEGREE ON TRANSDEGREE.DEGREEID = TABDEGREE.DEGREEID  WHERE TABDEGREE.RECORDSTATUS = 1 AND TRANSDEGREE.LANGUAGEID = @:LANGUAGEID  ORDER BY TransDegree.Description ASC", "TabDegree", "Linked.Underwriting")
                    .AddParameter("LANGUAGEID", DbType.Decimal, 5, False, CurrentState.Get("LanguageId"))
 
                    source = .QueryExecuteToTable(True)
                    DirectCast(ImpairmentRules_Grid.Columns("DEGREEID"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = source
                End With

                If Not IsNothing(source) AndAlso source.Rows.Count > 0 Then
                    Caching.SetItem("TabDegree", source)
                End If
            End If 
             If Caching.Exist("TabAlarmType") Then
                DirectCast(ImpairmentRules_Grid.Columns("ALARMTYPE"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = Caching.GetItem("TabAlarmType")

            Else
                Dim source As DataTable = Nothing

                With New DataManagerFactory("SELECT  TABALARMTYPE.ALARMTYPE, TABALARMTYPE.RECORDSTATUS, TRANSALARMTYPE.LANGUAGEID, TRANSALARMTYPE.DESCRIPTION FROM UNDERWRITING.TABALARMTYPE TABALARMTYPE JOIN TRANSALARMTYPE TRANSALARMTYPE ON TRANSALARMTYPE.ALARMTYPE = TABALARMTYPE.ALARMTYPE  WHERE TABALARMTYPE.RECORDSTATUS = 1 AND TRANSALARMTYPE.LANGUAGEID = @:LANGUAGEID  ORDER BY TransAlarmType.Description ASC", "TabAlarmType", "Linked.Underwriting")
                    .AddParameter("LANGUAGEID", DbType.Decimal, 5, False, CurrentState.Get("LanguageId"))
 
                    source = .QueryExecuteToTable(True)
                    DirectCast(ImpairmentRules_Grid.Columns("ALARMTYPE"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = source
                End With

                If Not IsNothing(source) AndAlso source.Rows.Count > 0 Then
                    Caching.SetItem("TabAlarmType", source)
                End If
            End If 
             If Caching.Exist("TabDecisionType") Then
                DirectCast(ImpairmentRules_Grid.Columns("DECISION"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = Caching.GetItem("TabDecisionType")

            Else
                Dim source As DataTable = Nothing

                With New DataManagerFactory("SELECT  TABDECISIONTYPE.DECISION, TABDECISIONTYPE.RECORDSTATUS, TRANSDECISIONTYPE.LANGUAGEID, TRANSDECISIONTYPE.DESCRIPTION FROM UNDERWRITING.TABDECISIONTYPE TABDECISIONTYPE JOIN TRANSDECISIONTYPE TRANSDECISIONTYPE ON TRANSDECISIONTYPE.DECISION = TABDECISIONTYPE.DECISION  WHERE TABDECISIONTYPE.RECORDSTATUS = 1 AND TRANSDECISIONTYPE.LANGUAGEID = @:LANGUAGEID  ORDER BY TransDecisionType.Description ASC", "TabDecisionType", "Linked.Underwriting")
                    .AddParameter("LANGUAGEID", DbType.Decimal, 5, False, CurrentState.Get("LanguageId"))
 
                    source = .QueryExecuteToTable(True)
                    DirectCast(ImpairmentRules_Grid.Columns("DECISION"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = source
                End With

                If Not IsNothing(source) AndAlso source.Rows.Count > 0 Then
                    Caching.SetItem("TabDecisionType", source)
                End If
            End If 
             If Caching.Exist("TabRestrictionType") Then
                DirectCast(ImpairmentRules_Grid.Columns("RESTRICTIONTYPE"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = Caching.GetItem("TabRestrictionType")

            Else
                Dim source As DataTable = Nothing

                With New DataManagerFactory("SELECT  TABRESTRICTIONTYPE.RESTRICTIONTYPE, TABRESTRICTIONTYPE.RECORDSTATUS, TRANSRESTRICTIONTYPE.LANGUAGEID, TRANSRESTRICTIONTYPE.DESCRIPTION FROM UNDERWRITING.TABRESTRICTIONTYPE TABRESTRICTIONTYPE JOIN TRANSRESTRICTIONTYPE TRANSRESTRICTIONTYPE ON TRANSRESTRICTIONTYPE.RESTRICTIONTYPE = TABRESTRICTIONTYPE.RESTRICTIONTYPE  WHERE TABRESTRICTIONTYPE.RECORDSTATUS = 1 AND TRANSRESTRICTIONTYPE.LANGUAGEID = @:LANGUAGEID  ORDER BY TransRestrictionType.Description ASC", "TabRestrictionType", "Linked.Underwriting")
                    .AddParameter("LANGUAGEID", DbType.Decimal, 5, False, CurrentState.Get("LanguageId"))
 
                    source = .QueryExecuteToTable(True)
                    DirectCast(ImpairmentRules_Grid.Columns("RESTRICTIONTYPE"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = source
                End With

                If Not IsNothing(source) AndAlso source.Rows.Count > 0 Then
                    Caching.SetItem("TabRestrictionType", source)
                End If
            End If 
             If Caching.Exist("TabRequirementType") Then
                DirectCast(ImpairmentRules_Grid.Columns("REQUIREMENTTYPE"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = Caching.GetItem("TabRequirementType")

            Else
                Dim source As DataTable = Nothing

                With New DataManagerFactory("SELECT  TABREQUIREMENTTYPE.REQUIREMENTTYPE, TABREQUIREMENTTYPE.RECORDSTATUS, TRANSREQUIREMENTTYPE.LANGUAGEID, TRANSREQUIREMENTTYPE.DESCRIPTION FROM UNDERWRITING.TABREQUIREMENTTYPE TABREQUIREMENTTYPE JOIN TRANSREQUIREMENTTYPE TRANSREQUIREMENTTYPE ON TRANSREQUIREMENTTYPE.REQUIREMENTTYPE = TABREQUIREMENTTYPE.REQUIREMENTTYPE  WHERE TABREQUIREMENTTYPE.RECORDSTATUS = 1 AND TRANSREQUIREMENTTYPE.LANGUAGEID = @:LANGUAGEID  ORDER BY TransRequirementType.Description ASC", "TabRequirementType", "Linked.Underwriting")
                    .AddParameter("LANGUAGEID", DbType.Decimal, 5, False, CurrentState.Get("LanguageId"))
 
                    source = .QueryExecuteToTable(True)
                    DirectCast(ImpairmentRules_Grid.Columns("REQUIREMENTTYPE"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = source
                End With

                If Not IsNothing(source) AndAlso source.Rows.Count > 0 Then
                    Caching.SetItem("TabRequirementType", source)
                End If
            End If 
             If Caching.Exist("TabExclusionPeriodType") Then
                DirectCast(ImpairmentRules_Grid.Columns("EXCLUSIONPERIODTYPE"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = Caching.GetItem("TabExclusionPeriodType")

            Else
                Dim source As DataTable = Nothing

                With New DataManagerFactory("SELECT  TABEXCLUSIONPERIODTYPE.EXCLUSIONPERIODTYPE, TABEXCLUSIONPERIODTYPE.RECORDSTATUS, TRANSEXCLUSIONPERIODTYPE.LANGUAGEID, TRANSEXCLUSIONPERIODTYPE.DESCRIPTION FROM UNDERWRITING.TABEXCLUSIONPERIODTYPE TABEXCLUSIONPERIODTYPE JOIN TRANSEXCLUSIONPERIODTYPE TRANSEXCLUSIONPERIODTYPE ON TRANSEXCLUSIONPERIODTYPE.EXCLUSIONPERIODTYPE = TABEXCLUSIONPERIODTYPE.EXCLUSIONPERIODTYPE  WHERE TABEXCLUSIONPERIODTYPE.RECORDSTATUS = 1 AND TRANSEXCLUSIONPERIODTYPE.LANGUAGEID = @:LANGUAGEID  ORDER BY TransExclusionPeriodType.Description ASC", "TabExclusionPeriodType", "Linked.Underwriting")
                    .AddParameter("LANGUAGEID", DbType.Decimal, 5, False, CurrentState.Get("LanguageId"))
 
                    source = .QueryExecuteToTable(True)
                    DirectCast(ImpairmentRules_Grid.Columns("EXCLUSIONPERIODTYPE"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = source
                End With

                If Not IsNothing(source) AndAlso source.Rows.Count > 0 Then
                    Caching.SetItem("TabExclusionPeriodType", source)
                End If
            End If 
             If Caching.Exist("TabExclusionType") Then
                DirectCast(ImpairmentRules_Grid.Columns("EXCLUSIONTYPE"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = Caching.GetItem("TabExclusionType")

            Else
                Dim source As DataTable = Nothing

                With New DataManagerFactory("SELECT  TABEXCLUSIONTYPE.EXCLUSIONTYPE, TABEXCLUSIONTYPE.RECORDSTATUS, TRANSEXCLUSIONTYPE.LANGUAGEID, TRANSEXCLUSIONTYPE.DESCRIPTION FROM UNDERWRITING.TABEXCLUSIONTYPE TABEXCLUSIONTYPE JOIN TRANSEXCLUSIONTYPE TRANSEXCLUSIONTYPE ON TRANSEXCLUSIONTYPE.EXCLUSIONTYPE = TABEXCLUSIONTYPE.EXCLUSIONTYPE  WHERE TABEXCLUSIONTYPE.RECORDSTATUS = 1 AND TRANSEXCLUSIONTYPE.LANGUAGEID = @:LANGUAGEID  ORDER BY TransExclusionType.Description ASC", "TabExclusionType", "Linked.Underwriting")
                    .AddParameter("LANGUAGEID", DbType.Decimal, 5, False, CurrentState.Get("LanguageId"))
 
                    source = .QueryExecuteToTable(True)
                    DirectCast(ImpairmentRules_Grid.Columns("EXCLUSIONTYPE"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = source
                End With

                If Not IsNothing(source) AndAlso source.Rows.Count > 0 Then
                    Caching.SetItem("TabExclusionType", source)
                End If
            End If 
             If Caching.Exist("EnumRecordStatus") Then
                DirectCast(ImpairmentRules_Grid.Columns("RECORDSTATUS"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = Caching.GetItem("EnumRecordStatus")

            Else
                Dim source As DataTable = Nothing

                With New DataManagerFactory("SELECT  ENUMRECORDSTATUS.RECORDSTATUS, ENUMRECORDSTATUS.RECORDSTATUSCODE, ETRANRECORDSTATUS.LANGUAGEID, ETRANRECORDSTATUS.DESCRIPTION FROM COMMON.ENUMRECORDSTATUS ENUMRECORDSTATUS JOIN ETRANRECORDSTATUS ETRANRECORDSTATUS ON ETRANRECORDSTATUS.RECORDSTATUS = ENUMRECORDSTATUS.RECORDSTATUS  WHERE ENUMRECORDSTATUS.RECORDSTATUSCODE = '1' AND ETRANRECORDSTATUS.LANGUAGEID = @:LANGUAGEID  ORDER BY ETranRecordStatus.Description ASC", "EnumRecordStatus", "Linked.Common")
                    .AddParameter("LANGUAGEID", DbType.Decimal, 5, False, CurrentState.Get("LanguageId"))
 
                    source = .QueryExecuteToTable(True)
                    DirectCast(ImpairmentRules_Grid.Columns("RECORDSTATUS"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = source
                End With

                If Not IsNothing(source) AndAlso source.Rows.Count > 0 Then
                    Caching.SetItem("EnumRecordStatus", source)
                End If
            End If 
 

                 With New DataManagerFactory("SELECT  IMPAIRMENTRULES.IMPAIRMENTRULEID, IMPAIRMENTRULES.LINEOFBUSINESS, IMPAIRMENTRULES.PRODUCT, IMPAIRMENTRULES.COVERAGECODE, IMPAIRMENTRULES.IMPAIRMENTCODE, IMPAIRMENTRULES.DEGREEID, IMPAIRMENTRULES.MORTALITYDEBITS, IMPAIRMENTRULES.FLATEXTRAPREMIUM, IMPAIRMENTRULES.DOFFLATEXTRAPREMIUMDAYS, IMPAIRMENTRULES.DOFFLATEXTRAPREMIUMMONTHS, IMPAIRMENTRULES.DOFFLATEXTRAPREMIUMYEARS, IMPAIRMENTRULES.ALARMTYPE, IMPAIRMENTRULES.DECISION, IMPAIRMENTRULES.DECISIONCOMPLEMENT, IMPAIRMENTRULES.WAITINGPERIODDAYS, IMPAIRMENTRULES.WAITINGPERIODMONTHS, IMPAIRMENTRULES.WAITINGPERIODYEARS, IMPAIRMENTRULES.MAXIMUMINSUREDAMOUNT, IMPAIRMENTRULES.RESTRICTIONTYPE, IMPAIRMENTRULES.REQUIREMENTTYPE, IMPAIRMENTRULES.EXCLUSIONPERIODTYPE, IMPAIRMENTRULES.EXCLUSIONTYPE, IMPAIRMENTRULES.RECORDSTATUS, IMPAIRMENTRULETRANS.IMPAIRMENTRULEID, IMPAIRMENTRULETRANS.LANGUAGEID, IMPAIRMENTRULETRANS.DESCRIPTION, IMPAIRMENTRULETRANS.SHORTDESCRIPTION FROM UNDERWRITING.IMPAIRMENTRULES IMPAIRMENTRULES JOIN UNDERWRITING.IMPAIRMENTRULETRANS IMPAIRMENTRULETRANS ON IMPAIRMENTRULETRANS.IMPAIRMENTRULEID = IMPAIRMENTRULES.IMPAIRMENTRULEID  WHERE IMPAIRMENTRULETRANS.LANGUAGEID = @:LANGUAGEID ORDER BY ImpairmentRules.ImpairmentRuleId ASC", "ImpairmentRules", "Linked.Underwriting")                 
                                                   
                      .AddParameter("LANGUAGEID", DbType.Decimal, 5, False, CurrentState.Get("LanguageId"))
            
                      ImpairmentRules_Grid.DataSource = .QueryExecuteToTable(True)
                 End With 
        End If     
    End Sub

    Protected Sub ImpairmentRules_Grid_CellEditorInitialize(sender As Object, e As ASPxGridViewEditorEventArgs) Handles ImpairmentRules_Grid.CellEditorInitialize
        If ImpairmentRules_Grid.IsNewRowEditing Then
            Select Case e.Column.FieldName
                
                
                
                Case "IMPAIRMENTRULEID"                     
                       e.Editor.Focus()               
            End Select

        Else

            Select Case e.Column.FieldName
                Case "IMPAIRMENTRULEID"
     e.Editor.Enabled = False
                   
                
                
                Case "LINEOFBUSINESS"                     
                     e.Editor.Focus()
            End Select
        End If
        
       Select Case e.Column.FieldName
           Case "IMPAIRMENTRULEID"
                 
                 
           Case "IMPAIRMENTCODE"
     DirectCast(e.Editor, ASPxComboBox).DataBindItems() 
Case "DEGREEID"
     DirectCast(e.Editor, ASPxComboBox).DataBindItems() 
Case "ALARMTYPE"
     DirectCast(e.Editor, ASPxComboBox).DataBindItems() 
Case "DECISION"
     DirectCast(e.Editor, ASPxComboBox).DataBindItems() 
Case "RESTRICTIONTYPE"
     DirectCast(e.Editor, ASPxComboBox).DataBindItems() 
Case "REQUIREMENTTYPE"
     DirectCast(e.Editor, ASPxComboBox).DataBindItems() 
Case "EXCLUSIONPERIODTYPE"
     DirectCast(e.Editor, ASPxComboBox).DataBindItems() 
Case "EXCLUSIONTYPE"
     DirectCast(e.Editor, ASPxComboBox).DataBindItems() 
Case "RECORDSTATUS"
     DirectCast(e.Editor, ASPxComboBox).DataBindItems() 

        End Select
    End Sub

    Protected Sub ImpairmentRules_Grid_RowInserting(ByVal sender As Object, ByVal e As ASPxDataInsertingEventArgs) Handles ImpairmentRules_Grid.RowInserting 
        Dim isNullResult As Boolean = True
        
             With New DataManagerFactory("INSERT INTO UNDERWRITING.ImpairmentRules (IMPAIRMENTRULEID, LINEOFBUSINESS, PRODUCT, COVERAGECODE, IMPAIRMENTCODE, DEGREEID, MORTALITYDEBITS, FLATEXTRAPREMIUM, DOFFLATEXTRAPREMIUMDAYS, DOFFLATEXTRAPREMIUMMONTHS, DOFFLATEXTRAPREMIUMYEARS, ALARMTYPE, DECISION, DECISIONCOMPLEMENT, WAITINGPERIODDAYS, WAITINGPERIODMONTHS, WAITINGPERIODYEARS, MAXIMUMINSUREDAMOUNT, RESTRICTIONTYPE, REQUIREMENTTYPE, EXCLUSIONPERIODTYPE, EXCLUSIONTYPE, RECORDSTATUS, CREATORUSERCODE, CREATIONDATE, UPDATEUSERCODE, UPDATEDATE) VALUES (@:IMPAIRMENTRULEID, @:LINEOFBUSINESS, @:PRODUCT, @:COVERAGECODE, @:IMPAIRMENTCODE, @:DEGREEID, @:MORTALITYDEBITS, @:FLATEXTRAPREMIUM, @:DOFFLATEXTRAPREMIUMDAYS, @:DOFFLATEXTRAPREMIUMMONTHS, @:DOFFLATEXTRAPREMIUMYEARS, @:ALARMTYPE, @:DECISION, @:DECISIONCOMPLEMENT, @:WAITINGPERIODDAYS, @:WAITINGPERIODMONTHS, @:WAITINGPERIODYEARS, @:MAXIMUMINSUREDAMOUNT, @:RESTRICTIONTYPE, @:REQUIREMENTTYPE, @:EXCLUSIONPERIODTYPE, @:EXCLUSIONTYPE, @:RECORDSTATUS, @:CREATORUSERCODE, SYSDATE, @:UPDATEUSERCODE, SYSDATE)", "ImpairmentRules", "Linked.Underwriting")                 
                                                   
                       .AddParameter("IMPAIRMENTRULEID", DbType.Decimal, 0, False, e.NewValues("IMPAIRMENTRULEID"))
.AddParameter("LINEOFBUSINESS", DbType.Decimal, 0, (e.NewValues("LINEOFBUSINESS") = 0), e.NewValues("LINEOFBUSINESS"))
.AddParameter("PRODUCT", DbType.Decimal, 0, (e.NewValues("PRODUCT") = 0), e.NewValues("PRODUCT"))
.AddParameter("COVERAGECODE", DbType.Decimal, 0, (e.NewValues("COVERAGECODE") = 0), e.NewValues("COVERAGECODE"))
.AddParameter("IMPAIRMENTCODE", DbType.AnsiString, 0, (e.NewValues("IMPAIRMENTCODE") = String.Empty), e.NewValues("IMPAIRMENTCODE"))
.AddParameter("DEGREEID", DbType.Decimal, 0, (e.NewValues("DEGREEID") = 0), e.NewValues("DEGREEID"))
.AddParameter("MORTALITYDEBITS", DbType.Decimal, 0, (e.NewValues("MORTALITYDEBITS") = 0), e.NewValues("MORTALITYDEBITS"))
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
.AddParameter("MAXIMUMINSUREDAMOUNT", DbType.Decimal, 0, (e.NewValues("MAXIMUMINSUREDAMOUNT") = 0), e.NewValues("MAXIMUMINSUREDAMOUNT"))
.AddParameter("RESTRICTIONTYPE", DbType.Decimal, 0, (e.NewValues("RESTRICTIONTYPE") = 0), e.NewValues("RESTRICTIONTYPE"))
.AddParameter("REQUIREMENTTYPE", DbType.Decimal, 0, (e.NewValues("REQUIREMENTTYPE") = 0), e.NewValues("REQUIREMENTTYPE"))
.AddParameter("EXCLUSIONPERIODTYPE", DbType.Decimal, 0, (e.NewValues("EXCLUSIONPERIODTYPE") = 0), e.NewValues("EXCLUSIONPERIODTYPE"))
.AddParameter("EXCLUSIONTYPE", DbType.Decimal, 0, (e.NewValues("EXCLUSIONTYPE") = 0), e.NewValues("EXCLUSIONTYPE"))
.AddParameter("RECORDSTATUS", DbType.Decimal, 0, False, e.NewValues("RECORDSTATUS"))
.AddParameter("CREATORUSERCODE", DbType.Decimal, 0, False, Session("nUserCode"))
.AddParameter("UPDATEUSERCODE", DbType.Decimal, 0, False, Session("nUserCode"))
            
                       .CommandExecute()
                End With
            For Each languageItem As KeyValuePair(Of Integer, String) In LanguageToDictionary(LanguageId)
                     With New DataManagerFactory("INSERT INTO UNDERWRITING.ImpairmentRuleTrans (IMPAIRMENTRULEID, LANGUAGEID, DESCRIPTION, SHORTDESCRIPTION, CREATORUSERCODE, CREATIONDATE, UPDATEUSERCODE, UPDATEDATE) VALUES (@:IMPAIRMENTRULEID, @:LANGUAGEID, @:DESCRIPTION, @:SHORTDESCRIPTION, @:CREATORUSERCODE, SYSDATE, @:UPDATEUSERCODE, SYSDATE)", "ImpairmentRuleTrans", "Linked.Underwriting")                 
                                                   
                       .AddParameter("IMPAIRMENTRULEID", DbType.Decimal, 0, False, e.NewValues("IMPAIRMENTRULEID"))
.AddParameter("LANGUAGEID", DbType.Decimal, 0, False, languageItem.Key)
.AddParameter("DESCRIPTION", DbType.AnsiString, 0, False, e.NewValues("DESCRIPTION"))
.AddParameter("SHORTDESCRIPTION", DbType.AnsiString, 0, False, e.NewValues("SHORTDESCRIPTION"))
.AddParameter("CREATORUSERCODE", DbType.Decimal, 0, False, Session("nUserCode"))
.AddParameter("UPDATEUSERCODE", DbType.Decimal, 0, False, Session("nUserCode"))
            
                       .CommandExecute()
                End With
           Next

               
        e.Cancel = True
        ImpairmentRules_Grid.CancelEdit()
    End Sub

    Protected Sub ImpairmentRules_Grid_RowUpdating(ByVal sender As Object, ByVal e As ASPxDataUpdatingEventArgs) Handles ImpairmentRules_Grid.RowUpdating
        Dim isNullResult As Boolean = True
          
             With New DataManagerFactory("UPDATE UNDERWRITING.ImpairmentRules SET LINEOFBUSINESS = @:LINEOFBUSINESS, PRODUCT = @:PRODUCT, COVERAGECODE = @:COVERAGECODE, IMPAIRMENTCODE = @:IMPAIRMENTCODE, DEGREEID = @:DEGREEID, MORTALITYDEBITS = @:MORTALITYDEBITS, FLATEXTRAPREMIUM = @:FLATEXTRAPREMIUM, DOFFLATEXTRAPREMIUMDAYS = @:DOFFLATEXTRAPREMIUMDAYS, DOFFLATEXTRAPREMIUMMONTHS = @:DOFFLATEXTRAPREMIUMMONTHS, DOFFLATEXTRAPREMIUMYEARS = @:DOFFLATEXTRAPREMIUMYEARS, ALARMTYPE = @:ALARMTYPE, DECISION = @:DECISION, DECISIONCOMPLEMENT = @:DECISIONCOMPLEMENT, WAITINGPERIODDAYS = @:WAITINGPERIODDAYS, WAITINGPERIODMONTHS = @:WAITINGPERIODMONTHS, WAITINGPERIODYEARS = @:WAITINGPERIODYEARS, MAXIMUMINSUREDAMOUNT = @:MAXIMUMINSUREDAMOUNT, RESTRICTIONTYPE = @:RESTRICTIONTYPE, REQUIREMENTTYPE = @:REQUIREMENTTYPE, EXCLUSIONPERIODTYPE = @:EXCLUSIONPERIODTYPE, EXCLUSIONTYPE = @:EXCLUSIONTYPE, RECORDSTATUS = @:RECORDSTATUS, UPDATEUSERCODE = @:UPDATEUSERCODE WHERE IMPAIRMENTRULEID = @:IMPAIRMENTRULEID", "ImpairmentRules", "Linked.Underwriting")                 
                                                   
                       .AddParameter("LINEOFBUSINESS", DbType.Decimal, 0, (e.NewValues("LINEOFBUSINESS") = 0), e.NewValues("LINEOFBUSINESS"))
.AddParameter("PRODUCT", DbType.Decimal, 0, (e.NewValues("PRODUCT") = 0), e.NewValues("PRODUCT"))
.AddParameter("COVERAGECODE", DbType.Decimal, 0, (e.NewValues("COVERAGECODE") = 0), e.NewValues("COVERAGECODE"))
.AddParameter("IMPAIRMENTCODE", DbType.AnsiString, 0, (e.NewValues("IMPAIRMENTCODE") = String.Empty), e.NewValues("IMPAIRMENTCODE"))
.AddParameter("DEGREEID", DbType.Decimal, 0, (e.NewValues("DEGREEID") = 0), e.NewValues("DEGREEID"))
.AddParameter("MORTALITYDEBITS", DbType.Decimal, 0, (e.NewValues("MORTALITYDEBITS") = 0), e.NewValues("MORTALITYDEBITS"))
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
.AddParameter("MAXIMUMINSUREDAMOUNT", DbType.Decimal, 0, (e.NewValues("MAXIMUMINSUREDAMOUNT") = 0), e.NewValues("MAXIMUMINSUREDAMOUNT"))
.AddParameter("RESTRICTIONTYPE", DbType.Decimal, 0, (e.NewValues("RESTRICTIONTYPE") = 0), e.NewValues("RESTRICTIONTYPE"))
.AddParameter("REQUIREMENTTYPE", DbType.Decimal, 0, (e.NewValues("REQUIREMENTTYPE") = 0), e.NewValues("REQUIREMENTTYPE"))
.AddParameter("EXCLUSIONPERIODTYPE", DbType.Decimal, 0, (e.NewValues("EXCLUSIONPERIODTYPE") = 0), e.NewValues("EXCLUSIONPERIODTYPE"))
.AddParameter("EXCLUSIONTYPE", DbType.Decimal, 0, (e.NewValues("EXCLUSIONTYPE") = 0), e.NewValues("EXCLUSIONTYPE"))
.AddParameter("RECORDSTATUS", DbType.Decimal, 0, False, e.NewValues("RECORDSTATUS"))
.AddParameter("UPDATEUSERCODE", DbType.Decimal, 0, False, Session("nUserCode"))
.AddParameter("IMPAIRMENTRULEID", DbType.Decimal, 0, False, e.Keys("IMPAIRMENTRULEID"))
            
                       .CommandExecute()
                End With
     With New DataManagerFactory("UPDATE UNDERWRITING.ImpairmentRuleTrans SET DESCRIPTION = @:DESCRIPTION, SHORTDESCRIPTION = @:SHORTDESCRIPTION, UPDATEUSERCODE = @:UPDATEUSERCODE WHERE IMPAIRMENTRULEID = @:IMPAIRMENTRULEID AND LANGUAGEID = @:LANGUAGEID", "ImpairmentRuleTrans", "Linked.Underwriting")                 
                                                   
                       .AddParameter("DESCRIPTION", DbType.AnsiString, 0, False, e.NewValues("DESCRIPTION"))
.AddParameter("SHORTDESCRIPTION", DbType.AnsiString, 0, False, e.NewValues("SHORTDESCRIPTION"))
.AddParameter("UPDATEUSERCODE", DbType.Decimal, 0, False, Session("nUserCode"))
.AddParameter("IMPAIRMENTRULEID", DbType.Decimal, 0, False, e.Keys("IMPAIRMENTRULEID"))
.AddParameter("LANGUAGEID", DbType.Decimal, 0, False, CurrentState.Get("LanguageId"))
            
                       .CommandExecute()
                End With         
      
         e.Cancel = True
        ImpairmentRules_Grid.CancelEdit()
    End Sub
    
    Protected Sub ImpairmentRules_Grid_CustomCallback(sender As Object, e As ASPxGridViewCustomCallbackEventArgs) Handles ImpairmentRules_Grid.CustomCallback     
           Dim isNullResult As Boolean = True
           
           Select Case e.Parameters.ToString.ToLower
               Case "delete"
                       Dim IMPAIRMENTRULEIDKey As Generic.List(Of Object) = ImpairmentRules_Grid.GetSelectedFieldValues("IMPAIRMENTRULEID")
        
               For index As Integer = 0 To IMPAIRMENTRULEIDKey.Count - 1
                  With New DataManagerFactory("DELETE FROM UNDERWRITING.ImpairmentRuleTrans WHERE IMPAIRMENTRULEID = @:IMPAIRMENTRULEID ", "ImpairmentRuleTrans", "Linked.Underwriting")                 
                                                   
               .AddParameter("IMPAIRMENTRULEID", DbType.Decimal, 0, False, IMPAIRMENTRULEIDKey(index))
            
               .CommandExecute()
          End With 
 With New DataManagerFactory("DELETE FROM UNDERWRITING.ImpairmentRules WHERE IMPAIRMENTRULEID = @:IMPAIRMENTRULEID ", "ImpairmentRules", "Linked.Underwriting")                 
                                                   
               .AddParameter("IMPAIRMENTRULEID", DbType.Decimal, 0, False, IMPAIRMENTRULEIDKey(index))
            
               .CommandExecute()
          End With 
                       
              Next           
           
              ImpairmentRules_Grid.DataBind()
                 
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
    
    Protected Sub ImpairmentRules_Grid_RowValidating(sender As Object, e As ASPxDataValidationEventArgs) Handles ImpairmentRules_Grid.RowValidating
        Dim errorMessage As String = "<ol style='font-weight:lighter'>"
        
        If IsNothing(e.NewValues("IMPAIRMENTRULEID")) OrElse e.NewValues("IMPAIRMENTRULEID") = 0  
   e.Errors(ImpairmentRules_Grid.Columns("IMPAIRMENTRULEID")) = GetLocalResourceObject("IMPAIRMENTRULEIDMessageErrorRequired1Resource").ToString
End If
 
        
        If e.IsNewRow Then
           Dim rowCountKey As System.Int32
  With New DataManagerFactory("SELECT  IMPAIRMENTRULES.IMPAIRMENTRULEID ROWCOUNT FROM UNDERWRITING.IMPAIRMENTRULES IMPAIRMENTRULES  WHERE IMPAIRMENTRULES.IMPAIRMENTRULEID = @:IMPAIRMENTRULEID", "ImpairmentRules", "Linked.Underwriting")
             .AddParameter("IMPAIRMENTRULEID", DbType.Decimal, 5, False, e.NewValues("IMPAIRMENTRULEID"))
 
             rowCountKey = .QueryExecuteScalarToInteger()  
        End With
        If rowCountKey > 0 Then
                errorMessage += String.Format(CultureInfo.InvariantCulture, "</ol><ul style='font-weight:bold'>{0}</ul>", 
                                                                            GetLocalResourceObject("ImpairmentRules_GridMessageErrorGeneralValidator0Resource").ToString)                
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
 
#Region "ImpairmentRuleTrans_Grid Events"
    
    Protected Sub ImpairmentRuleTrans_Grid_CustomColumnDisplayText(sender As Object, e As DevExpress.Web.ASPxGridView.ASPxGridViewColumnDisplayTextEventArgs) Handles ImpairmentRuleTrans_Grid.CustomColumnDisplayText
          Dim data As DataTable
          Dim rows() As DataRow
           
          Select Case e.Column.FieldName

               Case Else              
           End Select
    End Sub
  
    Protected Sub ImpairmentRuleTrans_Grid_DataBinding(ByVal sender As Object, ByVal e As EventArgs) Handles ImpairmentRuleTrans_Grid.DataBinding
        If (Not IsNothing(Request.Params("__CALLBACKID")) AndAlso Request.Params("__CALLBACKID").EndsWith("ImpairmentRuleTrans_Grid")) Or _internalCall Then
                       If Caching.Exist("TabIllnessType") Then
                DirectCast(ImpairmentRuleTrans_Grid.Columns("IMPAIRMENTCODE"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = Caching.GetItem("TabIllnessType")

            Else
                Dim source As DataTable = Nothing

                With New DataManagerFactory("SELECT  TABILLNESSTYPE.IMPAIRMENTCODE, TABILLNESSTYPE.RECORDSTATUS, TRANSILLNESSTYPE.LANGUAGEID, TRANSILLNESSTYPE.DESCRIPTION FROM UNDERWRITING.TABILLNESSTYPE TABILLNESSTYPE JOIN TRANSILLNESSTYPE TRANSILLNESSTYPE ON TRANSILLNESSTYPE.IMPAIRMENTCODE = TABILLNESSTYPE.IMPAIRMENTCODE  WHERE TABILLNESSTYPE.RECORDSTATUS = 1 AND TRANSILLNESSTYPE.LANGUAGEID = @:LANGUAGEID  ORDER BY TransIllnessType.Description ASC", "TabIllnessType", "Linked.Underwriting")
                    .AddParameter("LANGUAGEID", DbType.Decimal, 5, False, CurrentState.Get("LanguageId"))
 
                    source = .QueryExecuteToTable(True)
                    DirectCast(ImpairmentRuleTrans_Grid.Columns("IMPAIRMENTCODE"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = source
                End With

                If Not IsNothing(source) AndAlso source.Rows.Count > 0 Then
                    Caching.SetItem("TabIllnessType", source)
                End If
            End If 
             If Caching.Exist("TabDegree") Then
                DirectCast(ImpairmentRuleTrans_Grid.Columns("DEGREEID"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = Caching.GetItem("TabDegree")

            Else
                Dim source As DataTable = Nothing

                With New DataManagerFactory("SELECT  TABDEGREE.DEGREEID, TABDEGREE.RECORDSTATUS, TRANSDEGREE.LANGUAGEID, TRANSDEGREE.DESCRIPTION FROM UNDERWRITING.TABDEGREE TABDEGREE JOIN TRANSDEGREE TRANSDEGREE ON TRANSDEGREE.DEGREEID = TABDEGREE.DEGREEID  WHERE TABDEGREE.RECORDSTATUS = 1 AND TRANSDEGREE.LANGUAGEID = @:LANGUAGEID  ORDER BY TransDegree.Description ASC", "TabDegree", "Linked.Underwriting")
                    .AddParameter("LANGUAGEID", DbType.Decimal, 5, False, CurrentState.Get("LanguageId"))
 
                    source = .QueryExecuteToTable(True)
                    DirectCast(ImpairmentRuleTrans_Grid.Columns("DEGREEID"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = source
                End With

                If Not IsNothing(source) AndAlso source.Rows.Count > 0 Then
                    Caching.SetItem("TabDegree", source)
                End If
            End If 
             If Caching.Exist("TabAlarmType") Then
                DirectCast(ImpairmentRuleTrans_Grid.Columns("ALARMTYPE"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = Caching.GetItem("TabAlarmType")

            Else
                Dim source As DataTable = Nothing

                With New DataManagerFactory("SELECT  TABALARMTYPE.ALARMTYPE, TABALARMTYPE.RECORDSTATUS, TRANSALARMTYPE.LANGUAGEID, TRANSALARMTYPE.DESCRIPTION FROM UNDERWRITING.TABALARMTYPE TABALARMTYPE JOIN TRANSALARMTYPE TRANSALARMTYPE ON TRANSALARMTYPE.ALARMTYPE = TABALARMTYPE.ALARMTYPE  WHERE TABALARMTYPE.RECORDSTATUS = 1 AND TRANSALARMTYPE.LANGUAGEID = @:LANGUAGEID  ORDER BY TransAlarmType.Description ASC", "TabAlarmType", "Linked.Underwriting")
                    .AddParameter("LANGUAGEID", DbType.Decimal, 5, False, CurrentState.Get("LanguageId"))
 
                    source = .QueryExecuteToTable(True)
                    DirectCast(ImpairmentRuleTrans_Grid.Columns("ALARMTYPE"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = source
                End With

                If Not IsNothing(source) AndAlso source.Rows.Count > 0 Then
                    Caching.SetItem("TabAlarmType", source)
                End If
            End If 
             If Caching.Exist("TabDecisionType") Then
                DirectCast(ImpairmentRuleTrans_Grid.Columns("DECISION"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = Caching.GetItem("TabDecisionType")

            Else
                Dim source As DataTable = Nothing

                With New DataManagerFactory("SELECT  TABDECISIONTYPE.DECISION, TABDECISIONTYPE.RECORDSTATUS, TRANSDECISIONTYPE.LANGUAGEID, TRANSDECISIONTYPE.DESCRIPTION FROM UNDERWRITING.TABDECISIONTYPE TABDECISIONTYPE JOIN TRANSDECISIONTYPE TRANSDECISIONTYPE ON TRANSDECISIONTYPE.DECISION = TABDECISIONTYPE.DECISION  WHERE TABDECISIONTYPE.RECORDSTATUS = 1 AND TRANSDECISIONTYPE.LANGUAGEID = @:LANGUAGEID  ORDER BY TransDecisionType.Description ASC", "TabDecisionType", "Linked.Underwriting")
                    .AddParameter("LANGUAGEID", DbType.Decimal, 5, False, CurrentState.Get("LanguageId"))
 
                    source = .QueryExecuteToTable(True)
                    DirectCast(ImpairmentRuleTrans_Grid.Columns("DECISION"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = source
                End With

                If Not IsNothing(source) AndAlso source.Rows.Count > 0 Then
                    Caching.SetItem("TabDecisionType", source)
                End If
            End If 
             If Caching.Exist("TabRestrictionType") Then
                DirectCast(ImpairmentRuleTrans_Grid.Columns("RESTRICTIONTYPE"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = Caching.GetItem("TabRestrictionType")

            Else
                Dim source As DataTable = Nothing

                With New DataManagerFactory("SELECT  TABRESTRICTIONTYPE.RESTRICTIONTYPE, TABRESTRICTIONTYPE.RECORDSTATUS, TRANSRESTRICTIONTYPE.LANGUAGEID, TRANSRESTRICTIONTYPE.DESCRIPTION FROM UNDERWRITING.TABRESTRICTIONTYPE TABRESTRICTIONTYPE JOIN TRANSRESTRICTIONTYPE TRANSRESTRICTIONTYPE ON TRANSRESTRICTIONTYPE.RESTRICTIONTYPE = TABRESTRICTIONTYPE.RESTRICTIONTYPE  WHERE TABRESTRICTIONTYPE.RECORDSTATUS = 1 AND TRANSRESTRICTIONTYPE.LANGUAGEID = @:LANGUAGEID  ORDER BY TransRestrictionType.Description ASC", "TabRestrictionType", "Linked.Underwriting")
                    .AddParameter("LANGUAGEID", DbType.Decimal, 5, False, CurrentState.Get("LanguageId"))
 
                    source = .QueryExecuteToTable(True)
                    DirectCast(ImpairmentRuleTrans_Grid.Columns("RESTRICTIONTYPE"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = source
                End With

                If Not IsNothing(source) AndAlso source.Rows.Count > 0 Then
                    Caching.SetItem("TabRestrictionType", source)
                End If
            End If 
             If Caching.Exist("TabRequirementType") Then
                DirectCast(ImpairmentRuleTrans_Grid.Columns("REQUIREMENTTYPE"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = Caching.GetItem("TabRequirementType")

            Else
                Dim source As DataTable = Nothing

                With New DataManagerFactory("SELECT  TABREQUIREMENTTYPE.REQUIREMENTTYPE, TABREQUIREMENTTYPE.RECORDSTATUS, TRANSREQUIREMENTTYPE.LANGUAGEID, TRANSREQUIREMENTTYPE.DESCRIPTION FROM UNDERWRITING.TABREQUIREMENTTYPE TABREQUIREMENTTYPE JOIN TRANSREQUIREMENTTYPE TRANSREQUIREMENTTYPE ON TRANSREQUIREMENTTYPE.REQUIREMENTTYPE = TABREQUIREMENTTYPE.REQUIREMENTTYPE  WHERE TABREQUIREMENTTYPE.RECORDSTATUS = 1 AND TRANSREQUIREMENTTYPE.LANGUAGEID = @:LANGUAGEID  ORDER BY TransRequirementType.Description ASC", "TabRequirementType", "Linked.Underwriting")
                    .AddParameter("LANGUAGEID", DbType.Decimal, 5, False, CurrentState.Get("LanguageId"))
 
                    source = .QueryExecuteToTable(True)
                    DirectCast(ImpairmentRuleTrans_Grid.Columns("REQUIREMENTTYPE"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = source
                End With

                If Not IsNothing(source) AndAlso source.Rows.Count > 0 Then
                    Caching.SetItem("TabRequirementType", source)
                End If
            End If 
             If Caching.Exist("TabExclusionPeriodType") Then
                DirectCast(ImpairmentRuleTrans_Grid.Columns("EXCLUSIONPERIODTYPE"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = Caching.GetItem("TabExclusionPeriodType")

            Else
                Dim source As DataTable = Nothing

                With New DataManagerFactory("SELECT  TABEXCLUSIONPERIODTYPE.EXCLUSIONPERIODTYPE, TABEXCLUSIONPERIODTYPE.RECORDSTATUS, TRANSEXCLUSIONPERIODTYPE.LANGUAGEID, TRANSEXCLUSIONPERIODTYPE.DESCRIPTION FROM UNDERWRITING.TABEXCLUSIONPERIODTYPE TABEXCLUSIONPERIODTYPE JOIN TRANSEXCLUSIONPERIODTYPE TRANSEXCLUSIONPERIODTYPE ON TRANSEXCLUSIONPERIODTYPE.EXCLUSIONPERIODTYPE = TABEXCLUSIONPERIODTYPE.EXCLUSIONPERIODTYPE  WHERE TABEXCLUSIONPERIODTYPE.RECORDSTATUS = 1 AND TRANSEXCLUSIONPERIODTYPE.LANGUAGEID = @:LANGUAGEID  ORDER BY TransExclusionPeriodType.Description ASC", "TabExclusionPeriodType", "Linked.Underwriting")
                    .AddParameter("LANGUAGEID", DbType.Decimal, 5, False, CurrentState.Get("LanguageId"))
 
                    source = .QueryExecuteToTable(True)
                    DirectCast(ImpairmentRuleTrans_Grid.Columns("EXCLUSIONPERIODTYPE"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = source
                End With

                If Not IsNothing(source) AndAlso source.Rows.Count > 0 Then
                    Caching.SetItem("TabExclusionPeriodType", source)
                End If
            End If 
             If Caching.Exist("TabExclusionType") Then
                DirectCast(ImpairmentRuleTrans_Grid.Columns("EXCLUSIONTYPE"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = Caching.GetItem("TabExclusionType")

            Else
                Dim source As DataTable = Nothing

                With New DataManagerFactory("SELECT  TABEXCLUSIONTYPE.EXCLUSIONTYPE, TABEXCLUSIONTYPE.RECORDSTATUS, TRANSEXCLUSIONTYPE.LANGUAGEID, TRANSEXCLUSIONTYPE.DESCRIPTION FROM UNDERWRITING.TABEXCLUSIONTYPE TABEXCLUSIONTYPE JOIN TRANSEXCLUSIONTYPE TRANSEXCLUSIONTYPE ON TRANSEXCLUSIONTYPE.EXCLUSIONTYPE = TABEXCLUSIONTYPE.EXCLUSIONTYPE  WHERE TABEXCLUSIONTYPE.RECORDSTATUS = 1 AND TRANSEXCLUSIONTYPE.LANGUAGEID = @:LANGUAGEID  ORDER BY TransExclusionType.Description ASC", "TabExclusionType", "Linked.Underwriting")
                    .AddParameter("LANGUAGEID", DbType.Decimal, 5, False, CurrentState.Get("LanguageId"))
 
                    source = .QueryExecuteToTable(True)
                    DirectCast(ImpairmentRuleTrans_Grid.Columns("EXCLUSIONTYPE"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = source
                End With

                If Not IsNothing(source) AndAlso source.Rows.Count > 0 Then
                    Caching.SetItem("TabExclusionType", source)
                End If
            End If 
             If Caching.Exist("EnumRecordStatus") Then
                DirectCast(ImpairmentRuleTrans_Grid.Columns("RECORDSTATUS"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = Caching.GetItem("EnumRecordStatus")

            Else
                Dim source As DataTable = Nothing

                With New DataManagerFactory("SELECT  ENUMRECORDSTATUS.RECORDSTATUS, ENUMRECORDSTATUS.RECORDSTATUSCODE, ETRANRECORDSTATUS.LANGUAGEID, ETRANRECORDSTATUS.DESCRIPTION FROM COMMON.ENUMRECORDSTATUS ENUMRECORDSTATUS JOIN ETRANRECORDSTATUS ETRANRECORDSTATUS ON ETRANRECORDSTATUS.RECORDSTATUS = ENUMRECORDSTATUS.RECORDSTATUS  WHERE ENUMRECORDSTATUS.RECORDSTATUSCODE = '1' AND ETRANRECORDSTATUS.LANGUAGEID = @:LANGUAGEID  ORDER BY ETranRecordStatus.Description ASC", "EnumRecordStatus", "Linked.Common")
                    .AddParameter("LANGUAGEID", DbType.Decimal, 5, False, CurrentState.Get("LanguageId"))
 
                    source = .QueryExecuteToTable(True)
                    DirectCast(ImpairmentRuleTrans_Grid.Columns("RECORDSTATUS"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = source
                End With

                If Not IsNothing(source) AndAlso source.Rows.Count > 0 Then
                    Caching.SetItem("EnumRecordStatus", source)
                End If
            End If 
             If Caching.Exist("TabLanguage") Then
                DirectCast(ImpairmentRuleTrans_Grid.Columns("LANGUAGEID"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = Caching.GetItem("TabLanguage")

            Else
                Dim source As DataTable = Nothing

                With New DataManagerFactory("SELECT  TABLANGUAGE.LANGUAGEID, TABLANGUAGE.RECORDSTATUS, TRANSLANGUAGE.LANGUAGEID, TRANSLANGUAGE.DESCRIPTION FROM COMMON.TABLANGUAGE TABLANGUAGE JOIN TRANSLANGUAGE TRANSLANGUAGE ON TRANSLANGUAGE.LANGUAGECODEID = TABLANGUAGE.LANGUAGEID  WHERE TABLANGUAGE.RECORDSTATUS = '1' AND TRANSLANGUAGE.LANGUAGEID = @:LANGUAGEID  ORDER BY TransLanguage.Description ASC", "TabLanguage", "Linked.Common")
                    .AddParameter("LANGUAGEID", DbType.Decimal, 5, False, CurrentState.Get("LanguageId"))
 
                    source = .QueryExecuteToTable(True)
                    DirectCast(ImpairmentRuleTrans_Grid.Columns("LANGUAGEID"), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = source
                End With

                If Not IsNothing(source) AndAlso source.Rows.Count > 0 Then
                    Caching.SetItem("TabLanguage", source)
                End If
            End If 
 

                 With New DataManagerFactory("SELECT  IMPAIRMENTRULES.IMPAIRMENTRULEID, IMPAIRMENTRULES.LINEOFBUSINESS, IMPAIRMENTRULES.PRODUCT, IMPAIRMENTRULES.COVERAGECODE, IMPAIRMENTRULES.IMPAIRMENTCODE, IMPAIRMENTRULES.DEGREEID, IMPAIRMENTRULES.MORTALITYDEBITS, IMPAIRMENTRULES.FLATEXTRAPREMIUM, IMPAIRMENTRULES.DOFFLATEXTRAPREMIUMDAYS, IMPAIRMENTRULES.DOFFLATEXTRAPREMIUMMONTHS, IMPAIRMENTRULES.DOFFLATEXTRAPREMIUMYEARS, IMPAIRMENTRULES.ALARMTYPE, IMPAIRMENTRULES.DECISION, IMPAIRMENTRULES.DECISIONCOMPLEMENT, IMPAIRMENTRULES.WAITINGPERIODDAYS, IMPAIRMENTRULES.WAITINGPERIODMONTHS, IMPAIRMENTRULES.WAITINGPERIODYEARS, IMPAIRMENTRULES.MAXIMUMINSUREDAMOUNT, IMPAIRMENTRULES.RESTRICTIONTYPE, IMPAIRMENTRULES.REQUIREMENTTYPE, IMPAIRMENTRULES.EXCLUSIONPERIODTYPE, IMPAIRMENTRULES.EXCLUSIONTYPE, IMPAIRMENTRULES.RECORDSTATUS, IMPAIRMENTRULETRANS.IMPAIRMENTRULEID, IMPAIRMENTRULETRANS.LANGUAGEID, IMPAIRMENTRULETRANS.DESCRIPTION, IMPAIRMENTRULETRANS.SHORTDESCRIPTION FROM UNDERWRITING.IMPAIRMENTRULES IMPAIRMENTRULES JOIN UNDERWRITING.IMPAIRMENTRULETRANS IMPAIRMENTRULETRANS ON IMPAIRMENTRULETRANS.IMPAIRMENTRULEID = IMPAIRMENTRULES.IMPAIRMENTRULEID   ORDER BY ImpairmentRules.ImpairmentRuleId ASC", "ImpairmentRules", "Linked.Underwriting")                 
                                                   
                                  
                      ImpairmentRuleTrans_Grid.DataSource = .QueryExecuteToTable(True)
                 End With 
        End If     
    End Sub

    Protected Sub ImpairmentRuleTrans_Grid_CellEditorInitialize(sender As Object, e As ASPxGridViewEditorEventArgs) Handles ImpairmentRuleTrans_Grid.CellEditorInitialize
        If ImpairmentRuleTrans_Grid.IsNewRowEditing Then
            Select Case e.Column.FieldName
                
                
                
                Case "IMPAIRMENTRULEID"                     
                       e.Editor.Focus()               
            End Select

        Else

            Select Case e.Column.FieldName
                Case "IMPAIRMENTRULEID"
     e.Editor.Enabled = False
Case "LANGUAGEID"
     e.Editor.Enabled = False
                   
                
                
                Case "LINEOFBUSINESS"                     
                     e.Editor.Focus()
            End Select
        End If
        
       Select Case e.Column.FieldName
           Case "IMPAIRMENTRULEID"
                 
                 
           Case "IMPAIRMENTCODE"
     DirectCast(e.Editor, ASPxComboBox).DataBindItems() 
Case "DEGREEID"
     DirectCast(e.Editor, ASPxComboBox).DataBindItems() 
Case "ALARMTYPE"
     DirectCast(e.Editor, ASPxComboBox).DataBindItems() 
Case "DECISION"
     DirectCast(e.Editor, ASPxComboBox).DataBindItems() 
Case "RESTRICTIONTYPE"
     DirectCast(e.Editor, ASPxComboBox).DataBindItems() 
Case "REQUIREMENTTYPE"
     DirectCast(e.Editor, ASPxComboBox).DataBindItems() 
Case "EXCLUSIONPERIODTYPE"
     DirectCast(e.Editor, ASPxComboBox).DataBindItems() 
Case "EXCLUSIONTYPE"
     DirectCast(e.Editor, ASPxComboBox).DataBindItems() 
Case "RECORDSTATUS"
     DirectCast(e.Editor, ASPxComboBox).DataBindItems() 
Case "LANGUAGEID"
     DirectCast(e.Editor, ASPxComboBox).DataBindItems() 

        End Select
    End Sub

    Protected Sub ImpairmentRuleTrans_Grid_RowInserting(ByVal sender As Object, ByVal e As ASPxDataInsertingEventArgs) Handles ImpairmentRuleTrans_Grid.RowInserting 
        Dim isNullResult As Boolean = True
        
        
               
        e.Cancel = True
        ImpairmentRuleTrans_Grid.CancelEdit()
    End Sub

    Protected Sub ImpairmentRuleTrans_Grid_RowUpdating(ByVal sender As Object, ByVal e As ASPxDataUpdatingEventArgs) Handles ImpairmentRuleTrans_Grid.RowUpdating
        Dim isNullResult As Boolean = True
          
             With New DataManagerFactory("UPDATE UNDERWRITING.ImpairmentRuleTrans SET DESCRIPTION = @:DESCRIPTION, SHORTDESCRIPTION = @:SHORTDESCRIPTION, UPDATEUSERCODE = @:UPDATEUSERCODE WHERE IMPAIRMENTRULEID = @:IMPAIRMENTRULEID AND LANGUAGEID = @:LANGUAGEID", "ImpairmentRuleTrans", "Linked.Underwriting")                 
                                                   
                       .AddParameter("DESCRIPTION", DbType.AnsiString, 0, False, e.NewValues("DESCRIPTION"))
.AddParameter("SHORTDESCRIPTION", DbType.AnsiString, 0, False, e.NewValues("SHORTDESCRIPTION"))
.AddParameter("UPDATEUSERCODE", DbType.Decimal, 0, False, Session("nUserCode"))
.AddParameter("IMPAIRMENTRULEID", DbType.Decimal, 0, False, e.Keys("IMPAIRMENTRULEID"))
.AddParameter("LANGUAGEID", DbType.Decimal, 0, False, e.Keys("LANGUAGEID"))
            
                       .CommandExecute()
                End With         
      
         e.Cancel = True
        ImpairmentRuleTrans_Grid.CancelEdit()
    End Sub
    
    Protected Sub ImpairmentRuleTrans_Grid_CustomCallback(sender As Object, e As ASPxGridViewCustomCallbackEventArgs) Handles ImpairmentRuleTrans_Grid.CustomCallback     
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
    
    Protected Sub ImpairmentRuleTrans_Grid_RowValidating(sender As Object, e As ASPxDataValidationEventArgs) Handles ImpairmentRuleTrans_Grid.RowValidating
        Dim errorMessage As String = "<ol style='font-weight:lighter'>"
        
        If IsNothing(e.NewValues("IMPAIRMENTRULEID")) OrElse e.NewValues("IMPAIRMENTRULEID") = 0  
   e.Errors(ImpairmentRuleTrans_Grid.Columns("IMPAIRMENTRULEID")) = GetLocalResourceObject("IMPAIRMENTRULEIDMessageErrorRequired1Resource").ToString
End If
 
        
        If e.IsNewRow Then
           Dim rowCountKey As System.Int32
  With New DataManagerFactory("SELECT  IMPAIRMENTRULETRANS.IMPAIRMENTRULEID ROWCOUNT, IMPAIRMENTRULETRANS.LANGUAGEID FROM UNDERWRITING.IMPAIRMENTRULETRANS IMPAIRMENTRULETRANS  WHERE IMPAIRMENTRULETRANS.IMPAIRMENTRULEID = @:IMPAIRMENTRULEID AND IMPAIRMENTRULETRANS.LANGUAGEID = @:LANGUAGEID", "ImpairmentRuleTrans", "Linked.Underwriting")
             .AddParameter("IMPAIRMENTRULEID", DbType.Decimal, 5, False, e.NewValues("IMPAIRMENTRULEID"))
.AddParameter("LANGUAGEID", DbType.Decimal, 5, False, e.NewValues("LANGUAGEID"))
 
             rowCountKey = .QueryExecuteScalarToInteger()  
        End With
        If rowCountKey > 0 Then
                errorMessage += String.Format(CultureInfo.InvariantCulture, "</ol><ul style='font-weight:bold'>{0}</ul>", 
                                                                            GetLocalResourceObject("ImpairmentRuleTrans_GridMessageErrorGeneralValidator0Resource").ToString)                
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