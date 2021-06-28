<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eClaim" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 11.34.12
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Se define la variable mobjGrid para el manejo del Grid de la ventana
Dim mobjGrid As eFunctions.Grid

'- Objeto para el manejo de las zonas de la página
Dim mobjMenu As eFunctions.Menues

'-Variable que totalizara el monto total de los siniestros
Dim mdblAmount As Object



'%insDefineHeader: Se definen las columnas del grid
'------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'------------------------------------------------------------------------------
	Dim lobjColumn As eFunctions.Column
	Dim lintCertif As Object
	Dim lclsClaim As eClaim.Claim
	Dim lstrQueryString As String
	lclsClaim = New eClaim.Claim
	mobjGrid = New eFunctions.Grid
	'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.34.12
	mobjGrid.sSessionID = Session.SessionID
	mobjGrid.nUsercode = Session("nUsercode")
	'~End Body Block VisualTimer Utility
	
	mobjGrid.sCodisplPage = "si737"
	Call mobjGrid.SetWindowParameters(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), Request.QueryString.Item("nWindowTy"))
	
	'+Se definen todas las columnas del Grid
	With mobjGrid.Columns
		If mobjValues.StringToType(Request.QueryString.Item("tcnRelat"), eFunctions.Values.eTypeData.etdInteger) = eRemoteDB.Constants.intNull Then
			Call .AddNumericColumn(0, GetLocalResourceObject("tcnGroupColumnCaption"), "tcnGroup", 6, CStr(0), True, GetLocalResourceObject("tcnGroupColumnToolTip"), False, 0,  ,  , "inschangevalue(this);", False)
			Call .AddNumericColumn(0, GetLocalResourceObject("tcnRelationColumnCaption"), "tcnRelation", 5, "", True, GetLocalResourceObject("tcnRelationColumnToolTip"), False, 0,  ,  ,  , True)
		Else
			Call .AddHiddenColumn("tcnGroup", CStr(0))
			Call .AddHiddenColumn("tcnRelation", Request.QueryString.Item("tcnRelat"))
		End If
		If mobjValues.StringToType(Request.QueryString.Item("tcnPolicyHeader"), eFunctions.Values.eTypeData.etdDouble) = eRemoteDB.Constants.intNull Then
			Call .AddNumericColumn(0, GetLocalResourceObject("tcnPolicyColumnCaption"), "tcnPolicy", 10, "", True, GetLocalResourceObject("tcnPolicyColumnToolTip"), False, 0,  ,  , "inschangevalue(this);", False)
		Else
			Call .AddHiddenColumn("tcnPolicy", Request.QueryString.Item("tcnPolicy"))
		End If
		
		If CDbl(Request.QueryString.Item("hddPolitype")) <> 1 And mobjValues.StringToType(Request.QueryString.Item("tcnPolicyHeader"), eFunctions.Values.eTypeData.etdDouble) <> eRemoteDB.Constants.intNull Then
			
			lintCertif = mobjValues.StringToType(Request.QueryString.Item("tcnCertif"), eFunctions.Values.eTypeData.etdDouble)
			
			If mobjValues.StringToType(Request.QueryString.Item("tcnCertif"), eFunctions.Values.eTypeData.etdDouble) = eRemoteDB.Constants.intNull Then
				lintCertif = 0
			End If
			
			Call .AddNumericColumn(0, GetLocalResourceObject("tcnCertifColumnCaption"), "tcnCertif", 10, lintCertif, True, GetLocalResourceObject("tcnCertifColumnToolTip"), False, 0,  ,  , "ChangeValues();", False)
			Call .AddClientColumn(40981, GetLocalResourceObject("tctCodeAsegColumnCaption"), "tctCodeAseg", vbNullString,  , GetLocalResourceObject("tctCodeAsegColumnToolTip"), "ShowChangeValues(""Client"")", False)
			Call .AddAnimatedColumn(0, GetLocalResourceObject("btnClientPolicyColumnCaption"), "btnClientPolicy", "/VTimeNet/images/btn_ValuesOff.png", GetLocalResourceObject("btnClientPolicyColumnToolTip"),  , "ShowPolicies();", False)
		Else
			Call .AddNumericColumn(0, GetLocalResourceObject("tcnCertifColumnCaption"), "tcnCertif", 10, CStr(0), True, GetLocalResourceObject("tcnCertifColumnToolTip"), False, 0,  ,  , "inschangevalue(this);", True)
			Call .AddClientColumn(40981, GetLocalResourceObject("tctCodeAsegColumnCaption"), "tctCodeAseg", vbNullString,  , GetLocalResourceObject("tctCodeAsegColumnToolTip"), "ShowChangeValues(""Client"")", False)
			Call .AddAnimatedColumn(0, GetLocalResourceObject("btnClientPolicyColumnCaption"), "btnClientPolicy", "/VTimeNet/images/btn_ValuesOff.png", GetLocalResourceObject("btnClientPolicyColumnToolTip"),  , "ShowPolicies();", False)
			
			If mobjValues.StringToType(Request.QueryString.Item("tcnCertif"), eFunctions.Values.eTypeData.etdDouble) = eRemoteDB.Constants.intNull Then
				lintCertif = 0
			End If
		End If
		
		Call .AddDateColumn(0, GetLocalResourceObject("tcdBirthdatColumnCaption"), "tcdBirthdat", "",  , GetLocalResourceObject("tcdBirthdatColumnCaption"),  ,  ,  , True)
		
		If CDbl(Request.QueryString.Item("hddBrancht")) = 1 And CDbl(Request.QueryString.Item("hddPolitype")) <> 1 And CDbl(Request.QueryString.Item("tcnPolicyHeader")) > 0 Then
			Call .AddTextColumn(0, GetLocalResourceObject("tctCreditColumnCaption"), "tctCredit", 20, "", False, GetLocalResourceObject("tctCreditColumnToolTip"),  ,  ,  , True)
			Call .AddTextColumn(0, GetLocalResourceObject("tctAccountColumnCaption"), "tctAccount", 20, "", False, GetLocalResourceObject("tctAccountColumnToolTip"),  ,  ,  , True)
		Else
			Call .AddHiddenColumn("tctCredit", CStr(0))
			Call .AddHiddenColumn("tctAccount", CStr(0))
		End If
		
		Call .AddPossiblesColumn(9481, GetLocalResourceObject("cboRtypeColumnCaption"), "cboRtype", "Table692", eFunctions.Values.eValuesType.clngComboType, CStr(0),  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cboRtypeColumnToolTip"))
		Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeRoleColumnCaption"), "cbeRole", "Table184", eFunctions.Values.eValuesType.clngComboType, CStr(14),  ,  ,  ,  ,  ,  , 4, GetLocalResourceObject("cbeRoleColumnToolTip"), eFunctions.Values.eTypeCode.eNumeric)
		
		If mobjValues.StringToType(Request.QueryString.Item("tcnPolicyHeader"), eFunctions.Values.eTypeData.etdDouble) <> eRemoteDB.Constants.intNull And mobjValues.StringToType(Request.QueryString.Item("tcnPolicyHeader"), eFunctions.Values.eTypeData.etdDouble) > 0 Then
			
			lstrQueryString = "&sCertype=2" & "&nBranch=" & mobjValues.StringToType(Request.QueryString.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble) & "&nProduct=" & mobjValues.StringToType(Request.QueryString.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble) & "&nPolicy=" & mobjValues.StringToType(Request.QueryString.Item("tcnPolicyHeader"), eFunctions.Values.eTypeData.etdDouble) & "&nCertif="" + self.document.forms[0].tcnCertif.value + """ & "&dEffecdate=" & Request.QueryString.Item("tcdEffecdate")
			
			Call .AddClientColumn(0, GetLocalResourceObject("tctClientColumnCaption"), "tctClient", Session("sClient"), True, GetLocalResourceObject("tctClientColumnToolTip"),  ,  ,  ,  ,  ,  ,  ,  ,  , lstrQueryString,  , eFunctions.Values.eTypeClient.SearchClientPolicy)
		Else
			
			lstrQueryString = "&sCertype=2" & "&nBranch=" & mobjValues.StringToType(Request.QueryString.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble) & "&nProduct=" & mobjValues.StringToType(Request.QueryString.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble) & "&nPolicy="" + self.document.forms[0].tcnPolicy.value + """ & "&nCertif="" + self.document.forms[0].tcnCertif.value + """ & "&dEffecdate=" & mobjValues.StringToType(Request.QueryString.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate)
			
			Call .AddClientColumn(0, GetLocalResourceObject("tctClientColumnCaption"), "tctClient", Session("sClient"), True, GetLocalResourceObject("tctClientColumnToolTip"),  ,  ,  ,  ,  ,  ,  ,  ,  , lstrQueryString,  , eFunctions.Values.eTypeClient.SearchClientPolicy)
		End If
		
		Call .AddDateColumn(0, GetLocalResourceObject("tcdOccurDateColumnCaption"), "tcdOccurDate", CStr(Today), True, GetLocalResourceObject("tcdOccurDateColumnToolTip"),  ,  ,  , False)
            Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeCauseColumnCaption"), "cbeCause", "tabclaim_caus", eFunctions.Values.eValuesType.clngComboType, , True, , , , "inschangevalue(this);", , 2, GetLocalResourceObject("cbeCauseColumnToolTip"), eFunctions.Values.eTypeCode.eNumeric)
            'Call .AddPossiblesColumn(0, GetLocalResourceObject("valIdCatasColumnCaption"), "valIdCatas", "table6072", eFunctions.Values.eValuesType.clngWindowType, , True, , , , , , 2, GetLocalResourceObject("valIdCatasColumnToolTip"), eFunctions.Values.eTypeCode.eNumeric)
            
		If Request.QueryString.Item("Type") = "PopUp" Then
			Call .AddCheckColumn(0, GetLocalResourceObject("chkTotalLossColumnCaption"), "chkTotalLoss", "",  , CStr(2),  , False)
		Else
			Call .AddCheckColumn(0, GetLocalResourceObject("chkTotalLossColumnCaption"), "chkTotalLoss", "",  , CStr(1),  , True)
		End If
		
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnAmountColumnCaption"), "tcnAmount", 18, "", False, GetLocalResourceObject("tcnAmountColumnToolTip"), True, 6,  ,  ,  , True)
		
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnClaimColumnCaption"), "tcnClaim", 10, "", False, GetLocalResourceObject("tcnClaimColumnToolTip"), False, 0,  ,  ,  , True)
		
		lobjColumn = .AddPossiblesColumn(0, GetLocalResourceObject("cbeStateColumnCaption"), "cbeState", "Table135", eFunctions.Values.eValuesType.clngComboType, CStr(2),  ,  ,  ,  ,  , False, 4, GetLocalResourceObject("cbeStateColumnToolTip"), eFunctions.Values.eTypeCode.eNumeric)
		lobjColumn.TypeList = CShort("1")
		lobjColumn.List = "2,6,8"
		lobjColumn.GridVisible = False
		
            Call .AddPossiblesColumn(0, GetLocalResourceObject("valCoverColumnCaption"), "valCover", "TabCover_Pol", eFunctions.Values.eValuesType.clngWindowType, Request.QueryString.Item("valCover"), True, , , , "inschangevalue(this);", , , GetLocalResourceObject("valCoverColumnToolTip"), eFunctions.Values.eTypeCode.eNumeric)
            
            With mobjGrid
                .Columns("valCover").Parameters.Add("dEffecdate", mobjValues.StringToType(Request.QueryString.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Columns("valCover").Parameters.Add("sCertype", "2", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Columns("valCover").Parameters.Add("nBranch", Request.QueryString.Item("cbeBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Columns("valCover").Parameters.Add("nProduct", Request.QueryString.Item("valProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Columns("valCover").Parameters.Add("nPolicy", Request.QueryString.Item("tcnPolicyHeader"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Columns("valCover").Parameters.Add("nCertif", Request.QueryString.Item("tcnCertif"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Columns("valCover").Parameters.ReturnValue("nCapital", True, "Capital", True)
            End With
		
		Call .AddPossiblesColumn(0, GetLocalResourceObject("valIllnessColumnCaption"), "valIllness", "tabtab_am_ill", eFunctions.Values.eValuesType.clngWindowType,  , True,  ,  ,  ,  ,  ,  , GetLocalResourceObject("valIllnessColumnToolTip"), eFunctions.Values.eTypeCode.eString)
		With mobjGrid
			.Columns("valIllness").Parameters.Add("nBranch", Request.QueryString.Item("cbeBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Columns("valIllness").Parameters.Add("nProduct", Request.QueryString.Item("valProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Columns("valIllness").Parameters.Add("nPolicy", mobjValues.StringToType(Request.QueryString.Item("tcnPolicyHeader"), eFunctions.Values.eTypeData.etdDouble, True), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Columns("valIllness").Parameters.Add("nCertif", mobjValues.StringToType(Request.QueryString.Item("tcnCertif"), eFunctions.Values.eTypeData.etdDouble, True), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Columns("valIllness").Parameters.Add("dEffecdate", mobjValues.StringToType(Request.QueryString.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Columns("valIllness").Parameters.Add("sClient", vbNullString, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		End With
		
		Call .AddHiddenColumn("tcdEffecdate", Request.QueryString.Item("tcdEffecdate"))
		Call .AddHiddenColumn("cbeOffice", Request.QueryString.Item("cbeOffice"))
		Call .AddHiddenColumn("cbeOfficeAgen", Request.QueryString.Item("cbeOfficeAgen"))
		Call .AddHiddenColumn("cbeAgency", Request.QueryString.Item("cbeAgency"))
		Call .AddHiddenColumn("cbeBranch", Request.QueryString.Item("cbeBranch"))
		Call .AddHiddenColumn("valProduct", Request.QueryString.Item("valProduct"))
		Call .AddHiddenColumn("tcnPolicyHeader", Request.QueryString.Item("tcnPolicyHeader"))
		Call .AddHiddenColumn("cbeCurrency", Request.QueryString.Item("cbeCurrency"))
		Call .AddHiddenColumn("tcnRelat", Request.QueryString.Item("tcnRelat"))
		Call .AddHiddenColumn("tcdLedgerdat", Request.QueryString.Item("tcdLedgerdat"))
		Call .AddHiddenColumn("tctClientCollect", Request.QueryString.Item("tctClientCollect"))
		Call .AddHiddenColumn("hddBrancht", Request.QueryString.Item("hddBRancht"))
		Call .AddHiddenColumn("hddPolitype", Request.QueryString.Item("hddPolitype"))
		
	End With
	
	With mobjGrid
		.Columns("cbeCause").Parameters.Add("nBranch", Request.QueryString.Item("cbeBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Columns("cbeCause").Parameters.Add("nProduct", Request.QueryString.Item("valProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Columns("cboRtype").BlankPosition = False
		.Columns("btnClientPolicy").GridVisible = False
		.Columns("cboRtype").List = lclsClaim.Deman_typeList(CInt(Request.QueryString.Item("cbeBranch")), CInt(Request.QueryString.Item("valProduct")))
		
		.Columns("cboRtype").TypeList = lclsClaim.DTypeList
		.DeleteButton = False
		.AddButton = True
		.nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
		.Codispl = "SI737"
		.Codisp = "SI737"
		.Top = 100
		.Height = 630
		.Width = 550
		.ActionQuery = mobjValues.ActionQuery
		.bOnlyForQuery = Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionQuery)
		.Columns("Sel").GridVisible = Not .ActionQuery
		.sDelRecordParam = "tcdEffecdate=" & Request.QueryString.Item("tcdEffecdate") & "&cbeOffice=" & Request.QueryString.Item("cbeOffice") & "&cbeBranch=" & Request.QueryString.Item("cbeBranch") & "&valProduct=" & Request.QueryString.Item("valProduct") & "&tcnPolicyHeader=" & Request.QueryString.Item("tcnPolicyHeader") & "&valCover=" & Request.QueryString.Item("valCover") & "&cbeCurrency=" & Request.QueryString.Item("cbeCurrency") & "&tcnRelat=" & Request.QueryString.Item("tcnRelat") & "&hddBrancht=" & Request.QueryString.Item("hddBrancht") & "&hddPolitype=" & Request.QueryString.Item("hddPolitype") & "&tcnClaim='+ marrArray[lintIndex].tcnClaim + '"
		.sEditRecordParam = "tcdEffecdate=" & Request.QueryString.Item("tcdEffecdate") & "&cbeOffice=" & Request.QueryString.Item("cbeOffice") & "&cbeOfficeAgen=" & Request.QueryString.Item("cbeOfficeAgen") & "&cbeAgency=" & Request.QueryString.Item("cbeAgency") & "&cbeBranch=" & Request.QueryString.Item("cbeBranch") & "&valProduct=" & Request.QueryString.Item("valProduct") & "&tcnPolicyHeader=" & Request.QueryString.Item("tcnPolicyHeader") & "&valCover=" & Request.QueryString.Item("valCover") & "&cbeCurrency=" & Request.QueryString.Item("cbeCurrency") & "&tcnRelat=" & Request.QueryString.Item("tcnRelat") & "&hddBrancht=" & Request.QueryString.Item("hddBrancht") & "&hddPolitype=" & Request.QueryString.Item("hddPolitype") & "&tctClientCollect=" & Request.QueryString.Item("tctClientCollect")
		
		.Columns("Sel").GridVisible = False
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
		If Request.QueryString.Item("Type") <> "PopUp" Then
			Session("cbeBranch") = Request.QueryString.Item("cbeBranch")
		End If
	End With
	
End Sub

'%insPreSI737. Se crea la ventana madre (Principal)
'------------------------------------------------------------------------------
Private Sub insPreSI737()
	'------------------------------------------------------------------------------
	Dim lclsClaim_master As eClaim.Claim_Master
    Dim lclcClaim_window As Dictionary(Of String, String).KeyCollection

	Dim lintCount As Integer
	
	lclsClaim_master = New eClaim.Claim_Master
	
	mdblAmount = 0

	If IsNothing(Session("mobjCollecRelation")) Then
		Session("mobjCollecRelation") = New Dictionary(Of String, String)
	End If

	lclcClaim_window = Session("mobjCollecRelation").Keys
	
	For lintCount = 0 To Session("mobjCollecRelation").count - 1
		With mobjGrid
			If IsNumeric(lclcClaim_window(lintCount)) Then
				If lclsClaim_master.FindSI737(lclcClaim_window(lintCount)) Then
					.Columns("tcnClaim").DefValue = CStr(lclcClaim_window(lintCount))
					.Columns("tcnGroup").DefValue = CStr(Session("mobjCollecRelation")(lclcClaim_window(lintCount)))
					.Columns("tcnRelation").DefValue = CStr(lclsClaim_master.nBordereaux_cl)
					.Columns("tcnPolicy").DefValue = CStr(lclsClaim_master.nPolicy)
					'.Columns("valCover").Parameters("nCertif").DefValue = lclsClaim_master.nCertif
					.Columns("tcnCertif").DefValue = CStr(lclsClaim_master.nCertif)
					.Columns("cboRtype").DefValue = CStr(lclsClaim_master.nBene_type)
					.Columns("tctCredit").DefValue = lclsClaim_master.sCredit
					.Columns("tctAccount").DefValue = lclsClaim_master.sAccount
					.Columns("cbeRole").DefValue = CStr(lclsClaim_master.nBene_type)
					.Columns("tctClient").DefValue = lclsClaim_master.sClient
					.Columns("tctClientCollect").DefValue = Request.QueryString.Item("tctClientCollect")
					.Columns("tcdOccurdate").DefValue = CStr(lclsClaim_master.dOccurdate)
					.Columns("cbeCause").DefValue = CStr(lclsClaim_master.nClaim_caus)
					.Columns("cboRtype").DefValue = CStr(lclsClaim_master.nDeman_type)
					
					.Columns("tctCodeAseg").DefValue = lclsClaim_master.sClientAseg
					.Columns("tcdBirthdat").DefValue = CStr(lclsClaim_master.dBirthdate)
					
					If CDbl(lclsClaim_master.nTotalLoss) = 1 Then
						.Columns("chkTotalLoss").Checked = 2 '+ Pérdida Total
					Else
						.Columns("chkTotalLoss").Checked = 1 '+ Pérdida Parcial
					End If
					.Columns("tcnAmount").DefValue = CStr(lclsClaim_master.nAmount)
					If lclsClaim_master.nAmount > 0 Then
						mdblAmount = mdblAmount + lclsClaim_master.nAmount
					End If
					.Columns("cbeState").DefValue = CStr(lclsClaim_master.nStatClaim)
                        '.Columns("valIdCatas").DefValue = CStr(lclsClaim_master.nIdCatas)
                        
					.Columns("tcdEffecdate").DefValue = Request.QueryString.Item("tcdEffecdate")
					.Columns("cbeOffice").DefValue = Request.QueryString.Item("cbeOffice")
					.Columns("cbeBranch").DefValue = Request.QueryString.Item("cbeBranch")
					.Columns("valProduct").DefValue = Request.QueryString.Item("valProduct")
					.Columns("tcnPolicyHeader").DefValue = Request.QueryString.Item("tcnPolicyHeader")
					.Columns("valCover").DefValue = CStr(lclsClaim_master.nCover)
					.Columns("cbeCurrency").DefValue = Request.QueryString.Item("cbeCurrency")
					.Columns("tcnRelat").DefValue = Request.QueryString.Item("tcnRelat")
					.Columns("tcdLedgerdat").DefValue = Request.QueryString.Item("tcdLedgerdat")
					.Columns("tctClientCollect").DefValue = Request.QueryString.Item("tctClientCollect")
					.Columns("hddBrancht").DefValue = Request.QueryString.Item("hddBRancht")
					.Columns("hddPolitype").DefValue = Request.QueryString.Item("hddPolitype")
					Response.Write(.DoRow)
				End If
			End If
		End With
	Next 
	Response.Write(mobjGrid.closeTable())
	
	If Request.QueryString.Item("Type") <> "PopUp" Then
		If mobjValues.StringToType(Request.QueryString.Item("valCover"), eFunctions.Values.eTypeData.etdDouble) <> eRemoteDB.Constants.intNull Then
			
Response.Write("" & vbCrLf)
Response.Write("            <BR><BR>" & vbCrLf)
Response.Write("            <TABLE WIDTH=100%>" & vbCrLf)
Response.Write("            <TR>" & vbCrLf)
Response.Write("                <TD ALIGN=""RIGHT""><LABEL ID=0>" & GetLocalResourceObject("tcnTotalAmountCaption") & " </LABEL>" & vbCrLf)
Response.Write("                ")

			Response.Write(mobjValues.NumericControl("tcnTotalAmount", 18, mdblAmount,  , GetLocalResourceObject("tcnTotalAmountToolTip"), True, 6,  ,  ,  ,  , True))
Response.Write("</TD>" & vbCrLf)
Response.Write("            </TR>" & vbCrLf)
Response.Write("            </TABLE>" & vbCrLf)
Response.Write("            <BR><BR>" & vbCrLf)
Response.Write("        ")

			
		End If
	End If
	
	Response.Write(mobjValues.BeginPageButton)
	lclsClaim_master = Nothing
End Sub

'% insPreSI737Upd. Se define esta funcion para contruir el contenido de la 
'%                     ventana UPD de los archivos de datos particulares
'------------------------------------------------------------------------------
Private Sub insPreSI737Upd()
	'------------------------------------------------------------------------------
	With Request
		Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "ValClaim.aspx", .QueryString.Item("sCodispl"), .QueryString.Item("nMainAction"), mobjGrid.ActionQuery, CShort(.QueryString.Item("Index"))))
	End With
	
	Response.Write("<SCRIPT>ShowChangeCert(""Account"");</" & "Script>")
	
End Sub

</script>

<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("si737")

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.34.12
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "si737"
%>





<SCRIPT    LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT>

    //% ChangeValues: se controla el cambio de valor de los campos puntuales de la página
    /*---------------------------------------------------------------------------------------------------------*/
    function ChangeValues() {
        /*---------------------------------------------------------------------------------------------------------*/
        var lstrURL = "";
        lstrURL += document.location;

        lstrURL = lstrURL.replace(/&tcdEffecdate=.*/, "");

        with (self.document.forms[0]) {
            lstrURL = lstrURL + "&tcdEffecdate=" + '<%=Request.QueryString.Item("tcdEffecdate")%>' +
		                    "&cbeOffice=" + '<%=Request.QueryString.Item("cbeOffice")%>' +
		                    "&cbeOfficeAgen=" + '<%=Request.QueryString.Item("cbeOfficeAgen")%>' +
		                    "&cbeAgency=" + '<%=Request.QueryString.Item("cbeAgency")%>' +
		                    "&cbeBranch=" + '<%=Request.QueryString.Item("cbeBranch")%>' +
		                    "&valProduct=" + '<%=Request.QueryString.Item("valProduct")%>' +
		                    "&tcnPolicyHeader=" + '<%=Request.QueryString.Item("tcnPolicyHeader")%>' +
		                    "&valCover=" + '<%=Request.QueryString.Item("valCover")%>' +
		                    "&cbeCurrency=" + '<%=Request.QueryString.Item("cbeCurrency")%>' +
		                    "&tcnRelat=" + '<%=Request.QueryString.Item("tcnRelat")%>' +
		                    "&hddBrancht=" + '<%=Request.QueryString.Item("hddBrancht")%>' +
		                    "&hddPolitype=" + '<%=Request.QueryString.Item("hddPolitype")%>' +
		                    "&tctClientCollect=" + '<%=Request.QueryString.Item("tctClientCollect")%>' +
		                    "&sCodisp=" + '<%=Request.QueryString.Item("sCodisp")%>' +
		                    "&sWindowDescript=" + '<%=Request.QueryString.Item("sWindowDescript")%>' +
		                    "&nWindowTy=" + '<%=Request.QueryString.Item("nWindowTy")%>';
        }
        lstrURL = lstrURL + "&tcnCertif=" + self.document.forms[0].tcnCertif.value +
		                "&lblnReload=true";

        self.document.location = lstrURL;
    }

    // insOnChangeBranch: Esta función se encarga de pasar el parametro BRANCH a los valores 
    //                    posibles que lo requieran y habilitar los campos que dependan del ramo.
    //-------------------------------------------------------------------------------------------------------------------
    function inschangevalue(Field) {
        //-------------------------------------------------------------------------------------------------------------------
        var lstrQString;

        switch (Field.name) {
            case "tcnGroup":
                with (self.document.forms[0]) {
                    if (Field.value > 0) {
                        lstrQString = 'Form=PopUp&tcnGroup=' + Field.value;
                        insDefValues('Relation', lstrQString, '/VTimeNet/Claim/Claim');
                    }
                    else
                        tcnRelation.value = '';
                }
                break;

            case "tcnCertif":
                with (self.document.forms[0]) {
                    valIllness.Parameters.Param3.sValue = Field.value;
                    if (tcnPolicyHeader.value > 0) {
                        lstrQString = 'dEffecdate=' + tcdEffecdate.value +
                                  '&nBranch=' + cbeBranch.value +
                                  '&nProduct=' + valProduct.value +
                                  '&nPolicy=' + tcnPolicyHeader.value +
                                  '&nCertif=' + Field.value +
					              '&sClient=' + tctCodeAseg.value
                        insDefValues('Account', lstrQString, '/VTimeNet/Claim/Claim');
                    }
                    else {
                        lstrQString = 'dEffecdate=' + tcdEffecdate.value +
                                  '&nBranch=' + cbeBranch.value +
                                  '&nProduct=' + valProduct.value +
                                  '&nPolicy=' + tcnPolicy.value +
                                  '&nCertif=' + Field.value +
					              '&sClient=' + tctCodeAseg.value
                        insDefValues('Account', lstrQString, '/VTimeNet/Claim/Claim');
                        valCover.Parameters.Param5.sValue = tcnPolicy.value;
                        valCover.Parameters.Param6.sValue = Field.value;
                    }
                }
                break;

            case "tcnPolicy":
                with (self.document.forms[0]) {
                    valIllness.Parameters.Param3.sValue = Field.value;
                    lstrQString = 'dEffecdate=' + tcdEffecdate.value +
                              '&nBranch=' + cbeBranch.value +
                              '&nProduct=' + valProduct.value +
                              '&nPolicy=' + Field.value;

                    insDefValues('Policy', lstrQString, '/VTimeNet/Claim/Claim');
                }
                break;

            case "cbeCause":
                with (self.document.forms[0])
                    if (Field.value > 0) {
                        lstrQString = 'nBranch=' + cbeBranch.value +
				                  '&nProduct=' + valProduct.value +
				                  '&nClaimCaus=' + Field.value
                        insDefValues('ClaimCaus', lstrQString, '/VTimeNet/Claim/Claim');
                    }
                break;

            case "valCover":
                with (self.document.forms[0]) {
                    if (Field.value > 0)
                        tcnAmount.disabled = false;
                    if (tcnAmount.value == '') {
                        tcnAmount.value = valCover_nCapital.value;
                    }
                    break;
                }

        }
    }

    // insOnChangeBranch: Esta función se encarga de pasar el parametro BRANCH a los valores 
    //                    posibles que lo requieran y habilitar los campos que dependan del ramo.
    //-------------------------------------------------------------------------------------------------------------------
    function ShowChangeValues(ControlName) {
        //-------------------------------------------------------------------------------------------------------------------

        with (self.document.forms[0]) {
            switch (ControlName) {
                case "Client":
                    if (tcnPolicyHeader.value != -32768) {
                        lstrQString = "nPolicy=" + tcnPolicyHeader.value +
						          "&dEffecdate=" + tcdEffecdate.value +
						          "&nBranch=" + cbeBranch.value +
						          "&nProduct=" + valProduct.value +
						          "&nCertif=" + tcnCertif.value +
						          "&sClient=" + tctCodeAseg.value
                        insDefValues('ClaimClient', lstrQString, '/VTimeNet/Claim/Claim')
                        break;
                    }
                    else {
                        lstrQString = "nPolicy=" + tcnPolicy.value +
						          "&dEffecdate=" + tcdEffecdate.value +
						          "&nBranch=" + cbeBranch.value +
						          "&nProduct=" + valProduct.value +
						          "&nCertif=" + tcnCertif.value +
						          "&sClient=" + tctCodeAseg.value
                        insDefValues('ClaimClient', lstrQString, '/VTimeNet/Claim/Claim')
                        tctClientCollect.value = tctCodeAseg.value;
                        valCover.Parameters.Param5.sValue = tcnPolicyHeader.value;
                        valCover.Parameters.Param6.sValue = tcnCertif.value;
                        break;
                    }
            }
        }
    }

    // insOnChangeBranch: Esta función se encarga de pasar el parametro BRANCH a los valores 
    //                    posibles que lo requieran y habilitar los campos que dependan del ramo.
    //-------------------------------------------------------------------------------------------------------------------
    function ShowChangeCert(ControlName) {
        //-------------------------------------------------------------------------------------------------------------------

        with (self.document.forms[0]) {
            switch (ControlName) {
                case "Account":
                    if (tcnPolicyHeader.value != "") {
                        lstrQString = "nPolicy=" + tcnPolicyHeader.value +
					        "&dEffecdate=" + tcdEffecdate.value +
					        "&nBranch=" + cbeBranch.value +
					        "&nProduct=" + valProduct.value +
					        "&nCertif=" + tcnCertif.value +
					        "&sClient=" + tctCodeAseg.value
                        insDefValues('Account', lstrQString, '/VTimeNet/Claim/Claim')
                        break;
                    }
            }
        }
    }

    //% ShowPolicies: Muestra pólizas de un asegurado
    //-----------------------------------------------------------------------------------------------------------------
    function ShowPolicies() {
        //-----------------------------------------------------------------------------------------------------------------
        with (self.document.forms[0]) {
            if (tctCodeAseg.value != '')
                ShowPopUp('/VTimeNet/Common/PoldataSI001.aspx?sCertype=2' + "&nBranch=" + cbeBranch.value +
																"&nProduct=" + valProduct.value +
																"&sClient=" + tctCodeAseg.value +
																"&dEffecdate=" + tcdEffecdate.value +
																"&sCodispl=SI737", 'PolicyData', 800, 450, "yes", "no", 100, 50)
        }
    }
</SCRIPT>
<HTML>
  <HEAD>

    <META NAME="GENERATOR" CONTENT="eTransaction Designer for Visual TIME">
<%
mobjValues.ActionQuery = (Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionQuery))
With Response
	.Write(mobjValues.StyleSheet())
	.Write("<SCRIPT>var  nMainAction = " & CShort("0" & Request.QueryString.Item("nMainAction")) & "</SCRIPT>")
	If Request.QueryString.Item("Type") <> "PopUp" Then
		mobjMenu = New eFunctions.Menues
		'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.34.12
		mobjMenu.sSessionID = Session.SessionID
		mobjMenu.nUsercode = Session("nUsercode")
		'~End Body Block VisualTimer Utility
		.Write(mobjMenu.setZone(2, "SI737", Request.QueryString.Item("sWindowDescript"), CShort(Request.QueryString.Item("nWindowTy"))))
		mobjMenu = Nothing
	End If
End With
%>

<SCRIPT>
    //+ Esta línea guarda la versión procedente de VSS 
    document.VssVersion = "$$Revision: 3 $|$$Date: 9/02/04 18:14 $"
</SCRIPT>

</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST"    ID="FORM" NAME="frmSI737" ACTION="ValClaim.aspx?sZone=2">
<%
Response.Write(mobjValues.ShowWindowsName("SI737", Request.QueryString.Item("sWindowDescript")))
Call insDefineHeader()

If Request.QueryString.Item("Type") <> "PopUp" Then
	Call insPreSI737()
Else
	Call insPreSI737Upd()
End If

mobjValues = Nothing
mobjGrid = Nothing

With Response
	If Not IsNothing(Request.QueryString.Item("lblnReload")) AndAlso CBool(Request.QueryString.Item("lblnReload")) Then
		.Write("<SCRIPT>inschangevalue(self.document.forms[0].tcnCertif);</SCRIPT>")
	End If
End With
%>      
</FORM>
</BODY>
</HTML>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 11.34.12
Call mobjNetFrameWork.FinishPage("si737")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




