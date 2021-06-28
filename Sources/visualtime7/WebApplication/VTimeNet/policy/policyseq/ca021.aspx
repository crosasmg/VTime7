<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eProduct" %>
<%@ Import namespace="ePolicy" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 31/3/03 19.42.04
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

'- Objetos/Variables para el manejo de la transacción 
Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues
Dim mobjGridCov As eFunctions.Grid
Dim mobjGridC As eFunctions.Grid
Dim mobjGridF As eFunctions.Grid
Dim mobjCoverDesc As String
Dim mobjsCliename As String
Dim primera As String



'%insDefineHeaderCov.Esta funcion se encarga de definir las caracteristicas del Grid de los contratos de retencion
'------------------------------------------------------------------------------------------------------------------------------------------------------
Private Sub insDefineHeaderCov()
	'------------------------------------------------------------------------------------------------------------------------------------------------------
	mobjGridCov = New eFunctions.Grid
	'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.42.04
	mobjGridCov.sSessionID = Session.SessionID
	mobjGridCov.nUsercode = Session("nUsercode")
	'~End Body Block VisualTimer Utility
	
	mobjGridCov.sCodisplPage = "CA021"
	
	mobjGridCov.sArrayName = "marrArrayCov"
	
        Dim lclsOptionsInstallation As eGeneral.OptionsInstallation
        lclsOptionsInstallation = New eGeneral.OptionsInstallation
        Call lclsOptionsInstallation.insPreMCR002()
	
        With mobjGridCov.Columns
            If Request.QueryString.Item("Type") <> "PopUp" Then
                Call .AddAnimatedColumn(0, vbNullString, "btnSelCov", "/VTimeNet/images/clfolder.png", GetLocalResourceObject("btnSelCovColumnToolTip"), , , False)
            End If
		
            If CStr(Session("sBrancht")) = "1" Then
                Call .AddClientColumn(0, GetLocalResourceObject("valClientColumnCaption"), "valClient", vbNullString, , , , True)
            Else
                Call .AddHiddenColumn("valClient", "")
            End If
		
            '+ Cuando el tipo de reaseguro es por ramo de reaseguiro se oculta la información de las coberturas
            'If lclsOptionsInstallation.sDistType = "2" Then
            Call .AddPossiblesColumn(0, GetLocalResourceObject("valModulecColumnCaption"), "valModulec", "TABTABMODUL_CO_PG", eFunctions.Values.eValuesType.clngComboType, , True, , , , , True)
            Call .AddHiddenColumn("tcnModulec", "")
            'Else
            'Call .AddHiddenColumn("valModulec", "")
            'Call .AddHiddenColumn("tcnModulec", "")
            'End If
		
            '+ Cuando el tipo de reaseguro es por ramo de reaseguiro se ocultya la información de las coberturas
            'If lclsOptionsInstallation.sDistType = "2" Then
            Call .AddPossiblesColumn(0, GetLocalResourceObject("valCoverColumnCaption"), "valCover", "TABCOVER_POLCA021", eFunctions.Values.eValuesType.clngComboType, , True, , , , , True)
            'Else
            'Call .AddHiddenColumn("valCover", "")
            'End If
            
            Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeCurren_polColumnCaption"), "cbeCurren_pol", "Table11", eFunctions.Values.eValuesType.clngComboType, , , , , , , True)
            
            '+ Cuando el tipo de reaseguro es por ramo de reaseguiro se ocultya la información de las coberturas
            ' If lclsOptionsInstallation.sDistType = "2" Then
            Call .AddNumericColumn(0, GetLocalResourceObject("tcnCapitalColumnCaption"), "tcnCapital", 18, CStr(0), , GetLocalResourceObject("tcnCapitalColumnToolTip"), True, 6, , , , True, 0)
            'Else
            ' Call .AddHiddenColumn("tcnCapital", "")
            ' End If
		
            If CStr(Session("sBrancht")) = "1" Then
                Call .AddNumericColumn(0, GetLocalResourceObject("tcnReserveColumnCaption"), "tcnReserve", 18, CStr(0), , GetLocalResourceObject("tcnReserveColumnToolTip"), True, 6, , , , True, 0)
            Else
                Call .AddHiddenColumn("tcnReserve", "")
            End If
            Call .AddNumericColumn(0, GetLocalResourceObject("tcnReinCapitalColumnCaption"), "tcnReinCapital", 18, CStr(0), , GetLocalResourceObject("tcnReinCapitalColumnToolTip"), True, 6, , , , True, 0)
            Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeChangeColumnCaption"), "cbeChange", "Table5531", eFunctions.Values.eValuesType.clngComboType, , , , , , , False)
            Call .AddHiddenColumn("hddChange", CStr(0))
		
            If Request.QueryString.Item("Type") <> "PopUp" Then
                Call .AddNumericColumn(0, GetLocalResourceObject("tcnRetentionColumnCaption"), "tcnRetention", 18, CStr(0), , GetLocalResourceObject("tcnRetentionColumnToolTip"), True, 6)
            Else
                Call .AddNumericColumn(0, GetLocalResourceObject("tcnRetentionColumnCaption"), "tcnRetention", 18, CStr(0), , GetLocalResourceObject("tcnRetentionColumnToolTip"), True, 6, , , "ChangeValue();")
            End If
		
            Call .AddNumericColumn(0, GetLocalResourceObject("tcnAmountColumnCaption"), "tcnAmount", 18, CStr(0), , GetLocalResourceObject("tcnAmountColumnToolTip"), True, 6, , , , True, 0)
		
            Call .AddTextColumn(0, GetLocalResourceObject("tctHeapCodeColumnCaption"), "tctHeapCode", 14, " ", , GetLocalResourceObject("tctHeapCodeColumnToolTip"), , , , True)
		
            Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeBranchreiColumnCaption"), "cbeBranchrei", "Table5000", eFunctions.Values.eValuesType.clngComboType, , , , , , , True)
            Call .AddHiddenColumn("tctBrancht", Session("sBrancht"))
        End With
	
        With mobjGridCov
            .Columns("valModulec").Parameters.Add("sCertype", Session("sCertype"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Columns("valModulec").Parameters.Add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Columns("valModulec").Parameters.Add("nProduct", Session("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Columns("valModulec").Parameters.Add("nPolicy", Session("nPolicy"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Columns("valModulec").Parameters.Add("nCertif", Session("nCertif"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Columns("valModulec").Parameters.Add("dEffecdate", Session("dEffecdate"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Columns("valModulec").Parameters.Add("nGroup", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		
            .Columns("valCover").Parameters.Add("dEffecdate", Session("dEffecdate"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Columns("valCover").Parameters.Add("sCertype", Session("sCertype"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Columns("valCover").Parameters.Add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Columns("valCover").Parameters.Add("nProduct", Session("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Columns("valCover").Parameters.Add("nPolicy", Session("nPolicy"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Columns("valCover").Parameters.Add("nCertif", Session("nCertif"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		
            .Codispl = Request.QueryString.Item("sCodispl")
            .Width = 500
            .Height = 500
            .Top = 100
		
            .Columns("cbeChange").BlankPosition = False
            .Columns("Sel").GridVisible = False
		
            If Request.QueryString.Item("Type") <> "PopUp" Then
                .Columns("btnSelCov").Height = 20
            End If
		
            .DeleteButton = False
            .AddButton = False
            .ActionQuery = Session("bQuery")
		
            If Request.QueryString.Item("Reload") = "1" And Request.QueryString.Item("sPopupT") = "Cov" Then
                .sReloadIndex = Request.QueryString.Item("ReloadIndex")
            End If
        End With

        lclsOptionsInstallation = Nothing
	
End Sub

'%insDefineHeaderC.Esta funcion se encarga de definir las caracteristicas del Grid de los contratos obligatorios
'------------------------------------------------------------------------------------------------------------------------------------------------------
Private Sub insDefineHeaderC()
	'------------------------------------------------------------------------------------------------------------------------------------------------------
	mobjGridC = New eFunctions.Grid
	'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.42.04
	mobjGridC.sSessionID = Session.SessionID
	mobjGridC.nUsercode = Session("nUsercode")
	'~End Body Block VisualTimer Utility
	
	mobjGridC.sCodisplPage = "CA021"
	mobjGridC.sArrayName = "marrArrayC"
	
	With mobjGridC.Columns
		Call .AddTextColumn(0, GetLocalResourceObject("cbeContractColumnCaption"), "cbeContract", 30, " ",  , GetLocalResourceObject("cbeContractColumnToolTip"),  ,  ,  , True)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnParticipColumnCaption"), "tcnParticip", 18, CStr(0),  , GetLocalResourceObject("tcnParticipColumnCaption"), True, 6,  ,  , "insShowShare(this.value)")
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnPercentageColumnCaption"), "tcnPercentage", 9, CStr(0),  ,  ,  , 6,  ,  ,  , True)
		'Call .AddTextColumn(0, GetLocalResourceObject("tctCompanyColumnCaption"),"tctCompany",30," ",, GetLocalResourceObject("tctCompanyColumnToolTip"),,,,True)
		Call .AddHiddenColumn("tcnNumber", CStr(0))
		Call .AddHiddenColumn("tcnRetention", "")
		Call .AddHiddenColumn("tcnCapitalMax", "")
		Call .AddHiddenColumn("tctBrancht", "")
		Call .AddHiddenColumn("tcnAllowedChange", "")
		Call .AddHiddenColumn("tcnType", CStr(0))
		Call .AddHiddenColumn("tcnCapital", "")
	End With
	
	With mobjGridC
		.Codispl = Request.QueryString.Item("sCodispl")
		.Height = 300
		.Width = 500
		.Columns("Sel").GridVisible = False
		.DeleteButton = False
		.AddButton = False
		.sEditRecordParam = "sIsFACOB=2"
		If Request.QueryString.Item("Reload") = "1" And Request.QueryString.Item("sPopupT") = "C" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
		.actionQuery = Session("bQuery")
	End With
End Sub

'%insDefineHeaderF.Esta funcion se encarga de definir las caracteristicas del GriD de los contratos facultativo
'------------------------------------------------------------------------------------------------------------------------------------------------------
Private Sub insDefineHeaderF()
	'------------------------------------------------------------------------------------------------------------------------------------------------------
	mobjGridF = New eFunctions.Grid
	'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.42.04
	mobjGridF.sSessionID = Session.SessionID
	mobjGridF.nUsercode = Session("nUsercode")
	'~End Body Block VisualTimer Utility
	
	mobjGridF.sCodisplPage = "CA021"
	
	mobjGridF.sArrayName = "marrArrayF"
	
	With mobjGridF.Columns
		Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeCompanyColumnCaption"), "cbeCompany", "tabCompanyClient", eFunctions.Values.eValuesType.clngComboType,  , True,  ,  ,  ,  , Request.QueryString.Item("Action") = "Update")
		Call .AddPossiblesColumn(0, GetLocalResourceObject("tcnClasificColumnCaption"), "tcnClasific", "Table5563", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("tcnClasificColumnToolTip"), eFunctions.Values.eTypeCode.eNumeric)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnParticipColumnCaption"), "tcnParticip", 18, CStr(0),  , GetLocalResourceObject("tcnParticipColumnCaption"), True, 6,  ,  , "insShowShare(this.value)")
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnPercentageColumnCaption"), "tcnPercentage", 9, CStr(0),  , GetLocalResourceObject("tcnPercentageColumnToolTip"), True, 6,  ,  ,  , True)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnComissionColumnCaption"), "tcnComission", 8, CStr(0),  , GetLocalResourceObject("tcnComissionColumnToolTip"), True, 6)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnReser_rateColumnCaption"), "tcnReser_rate", 8, CStr(0),  , GetLocalResourceObject("tcnReser_rateColumnToolTip"), True, 6)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnInter_rateColumnCaption"), "tcnInter_rate", 8, CStr(0),  , GetLocalResourceObject("tcnInter_rateColumnToolTip"), True, 6)
		Call .AddDateColumn(0, GetLocalResourceObject("tcdAcceptDateColumnCaption"), "tcdAcceptDate", Session("dEffecdate"),  , GetLocalResourceObject("tcdAcceptDateColumnToolTip"))
		Call .AddHiddenColumn("tcnType", "4")
		Call .AddHiddenColumn("tcnNumber", CStr(0))
		Call .AddHiddenColumn("tcnRest", "")
		Call .AddHiddenColumn("tcnCapital", "")
	End With
	With mobjGridF
		.Height = 350
		.Width = 520
		.AddButton = True
		.Columns("cbeCompany").Parameters.Add("nCompany", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Columns("cbeCompany").EditRecord = True
		.Columns("cbeCompany").BlankPosition = False
		.Codispl = Request.QueryString.Item("sCodispl")
		.sDelRecordParam = "sIsFACOB=1&nType=' + marrArrayF[lintIndex].tcnType + '&nNumber=0&nCompany=' + marrArrayF[lintIndex].cbeCompany + '"
		
		If Request.QueryString.Item("Action") = "Add" Then
			.Columns("tcnNumber").DefValue = Request.QueryString.Item("nNumber")
			.Columns("tcnRest").DefValue = Request.QueryString.Item("nRest")
		End If
		
		.sEditRecordParam = "sIsFACOB=1"
		
		If Request.QueryString.Item("Reload") = "1" And Request.QueryString.Item("sPopupT") = "F" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
		
		.actionQuery = Session("bQuery")
	End With
End Sub

'%insPreCA021:
'------------------------------------------------------------------------------------------------------------------------------------------------------
Private Sub insPreCA021()
	'------------------------------------------------------------------------------------------------------------------------------------------------------
	Dim lcolReinsurans As ePolicy.Reinsurans
	Dim lclsReinsuran As ePolicy.Reinsuran
	
	Dim lcolBranchrs As ePolicy.Branchrs
	Dim lclsBranchr As ePolicy.Branchr
	Dim lclsCoinsuran As ePolicy.Coinsuran
	Dim mclsProduct As eProduct.Product
	Dim lstrCumulCode As String
	Dim ldlbCoinShare As Object
	Dim lblnReinsuran As Boolean
	
	'- Se definen las variables ldlbCapitalRein, ldblRest, ldblRest2 para contener el valor que se va a mostrar en la 
	'-ventana 
	Dim ldlbCapitalRein As Double
	Dim ldblRest As Object
	Dim ldblRest2 As Object
	
	'- Se definen  Las variables lstrFCOB, lstrCover y lstrContract  para contener el código HTML de los
	'- Grid's de la ventana
	Dim lstrCover As String
	Dim lstrFCOB As String
	Dim lstrContract As String
	
	'- Variables contadores  de registro.
	Dim lintCount As Double
	Dim lintCountCov As Double
	Dim lintCountAUX As Integer
	
	'- Indica si se realizara el re-calculo de la distribucion
	Dim lintQueryMode As Object
	Dim lintChange As Object
	
	'-Variables para el caso que no existan contratos para la cobertura
	Dim lclsError As eFunctions.Errors
	Dim lintError As Byte
	Dim sNamePopup As Object
	Dim nCountPopup As Integer
	Dim nQueryMode As Object
	Dim nMode As Object
        
	Dim lnModulec As Integer
	Dim lnCover As Integer
	Dim lnBranchrei As Integer
	Dim lsClient As String
	
	'+ Se inicializan las variables 
	ldlbCapitalRein = 0
	ldblRest = 0
	ldblRest2 = 0
	
	'+ Indicadores para que no se realice la distribucion y lea de la tabla temporal TREINSURAN (lintQueryMode, lintchange)
	With Request
		If  Not .QueryString.GetValues("sPopupT") Is  Nothing AndAlso .QueryString.GetValues("sPopupT").Count > 1 Then

			For nCountPopup = 0 To .QueryString.GetValues("sPopupT").Count-1
				sNamePopup = .QueryString.GetValues("sPopupT").GetValue(nCountPopup)
			Next 
		Else
			sNamePopup = .QueryString.Item("sPopupT")
			If CStr(Session("sPopupT")) = "F" Then
				sNamePopup = Session("sPopupT")
				Session("sPopupT") = ""
			End If
		End If
            
		If Not IsNothing(.QueryString("sPopupT")) AndAlso .QueryString.GetValues("sPopupT").Length > 1 Then
			For nCountPopup = 1 To .QueryString.GetValues("sPopupT").Length
				sNamePopup = .QueryString.GetValues("sPopupT").GetValue(nCountPopup - 1)
			Next 
		Else
			sNamePopup = .QueryString.Item("sPopupT")
			If CStr(Session("sPopupT")) = "F" Then
				sNamePopup = Session("sPopupT")
				Session("sPopupT") = ""
			End If
		End If            

		If  Not .QueryString.GetValues("nQueryModeF") Is  Nothing AndAlso .QueryString.GetValues("nQueryModeF").Count > 1 Then
			For nCountPopup = 0 To .QueryString.GetValues("nQueryModeF").Count-1
				nQueryMode = .QueryString.GetValues("nQueryModeF").GetValue(nCountPopup)
			Next 
		Else
			nQueryMode = .QueryString.Item("nQueryModeF")
		End If

		If Not IsNothing(.QueryString("nQueryModeF")) AndAlso .QueryString.GetValues("nQueryModeF").Length > 1 Then
			For nCountPopup = 1 To .QueryString.GetValues("nQueryModeF").Length
				nQueryMode = .QueryString.GetValues("nQueryModeF").GetValue(nCountPopup - 1)
			Next 
		Else
			nQueryMode = .QueryString.Item("nQueryModeF")
		End If            


		If  Not .QueryString.GetValues("nMode") Is  Nothing AndAlso .QueryString.GetValues("nMode").Count > 1 Then
			For nCountPopup = 0 To .QueryString.GetValues("nMode").Count-1
				nMode = .QueryString.GetValues("nMode").GetValue(nCountPopup)
			Next 
		Else
			nMode = .QueryString.Item("nQueryModeF")
		End If
            
		If  Not IsNothing(.QueryString("nMode")) AndAlso .QueryString.GetValues("nMode").Length > 1 Then
			For nCountPopup = 1 To .QueryString.GetValues("nMode").Length
				nMode = .QueryString.GetValues("nMode").GetValue(nCountPopup - 1)
			Next 
		Else
			nMode = .QueryString.Item("nMode")
		End If
            
		
		If sNamePopup <> "F" Then
			If nQueryMode = 4 Or sNamePopup = "Cov" Or nMode = 4 Then
				lintQueryMode = 2
			Else
				lintQueryMode = 1
			End If
			
			If .QueryString.Item("nChange") > vbNullString Then
				lintChange = .QueryString.Item("nChange")
			Else
				lintChange = 1
			End If
		Else
			lintQueryMode = 2
			lintChange = 4
		End If
	End With
	
	'+si es consulta al sp se le pasa para que no realice 3
	If Session("bQuery") Then
		lintQueryMode = 3
	End If
	
	With Server
		mclsProduct = New eProduct.Product
		lclsCoinsuran = New ePolicy.Coinsuran
		lcolBranchrs = New ePolicy.Branchrs
		lclsBranchr = New ePolicy.Branchr
	End With
	lclsBranchr = Nothing
	
	'+Busca el porcentaje de la compañia usuaria.
	If lclsCoinsuran.Find(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCompanyUser"), Session("dEffecdate")) Then
		ldlbCoinShare = lclsCoinsuran.nShare
	Else
		ldlbCoinShare = 100
	End If
	
	Call mclsProduct.Find(Session("nBranch"), Session("nProduct"), Session("dEffecdate"))
	
	With Response
		.Write(mobjValues.HiddenControl("tcnCoinsuShare", mobjValues.TypeToString(ldlbCoinShare, eFunctions.Values.eTypeData.etdDouble, True, 6)))
		.Write(mobjValues.HiddenControl("tctCumReint", mclsProduct.sCumreint))
		.Write(mobjValues.HiddenControl("tctBrancht", CStr(mclsProduct.sBrancht)))
	End With
	

	
	'+Se ejecuta el metodo FindReinsuranPol que se encarga de distribuir el reaseguro de la póliza
	lblnReinsuran = lcolBranchrs.FindReinsuranPol(Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), CStr(mclsProduct.sBrancht), mclsProduct.sCumreint, lstrCumulCode, mobjValues.StringToType(ldlbCoinShare, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(lintChange, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(lintQueryMode, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nTransaction"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nBranchRei"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nModulec"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nCover"), eFunctions.Values.eTypeData.etdDouble), Request.QueryString.Item("sClient"), mobjValues.StringToType(Request.QueryString.Item("nCompany"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nType"), eFunctions.Values.eTypeData.etdDouble))
	
	If lblnReinsuran Then
		lstrCover = vbNullString
		lintCountCov = 0
		
		If Request.QueryString.Item("nBranchRei") = vbNullString Then
			lintCountCov = 1
		End If
		
        For lintCount = 1 To lcolBranchrs.count 
			ldblRest = lcolBranchrs.Item(lintCount).nCapital
			'ldblRest = lcolBranchrs.item(lintCount).nCapital_cov
			With mobjGridCov
				If Request.QueryString.Item("nBranchRei") <> vbNullString Then
					If lcolBranchrs.item(lintCount).nModulec = mobjValues.StringToType(Request.QueryString.Item("nModulec"), eFunctions.Values.eTypeData.etdDouble) And lcolBranchrs.item(lintCount).nCover = mobjValues.StringToType(Request.QueryString.Item("nCover"), eFunctions.Values.eTypeData.etdDouble) And lcolBranchrs.item(lintCount).sClient = Request.QueryString.Item("sClient") And lcolBranchrs.item(lintCount).nBranchRei = mobjValues.StringToType(Request.QueryString.Item("nBranchRei"), eFunctions.Values.eTypeData.etdDouble) Then
						lintCountCov = lintCount
					Else
						lintCountCov = 0
					End If
				End If
				
				If CStr(mclsProduct.sBrancht) = "1" Then
					.Columns("valClient").EditRecord = True
				Else
					.Columns("valCover").EditRecord = True
				End If
				
				If lintCount = lintCountCov Then
					lstrCumulCode = lcolBranchrs.item(lintCount).sHeapcode
					lnModulec = lcolBranchrs.item(lintCount).nModulec
					lnCover = lcolBranchrs.item(lintCount).nCover
					lnBranchrei = lcolBranchrs.item(lintCount).nBranchRei
					lsClient = lcolBranchrs.item(lintCount).sClient
					.Columns("btnSelCov").Src = "/VTimeNet/images/Opfolder.png"
					.Columns("btnSelCov").HRefScript = ""
				Else
					.Columns("btnSelCov").Src = "/VTimeNet/images/clfolder.png"
					.Columns("btnSelCov").HRefScript = "insMakeURLCA021(" & lcolBranchrs.item(lintCount).nBranchRei & ", " & lcolBranchrs.item(lintCount).nModulec & ", " & lcolBranchrs.item(lintCount).nCover & ",'" & lcolBranchrs.item(lintCount).sClient & "')"
					
				End If
				'+Se carga el grid de las coberturas				
				.Columns("valModulec").DefValue = CStr(lcolBranchrs.item(lintCount).nModulec)
				.Columns("valModulec").Descript = lcolBranchrs.item(lintCount).sModuDesc
				.Columns("tcnModulec").DefValue = CStr(lcolBranchrs.item(lintCount).nModulec)
				
				.Columns("valCover").DefValue = CStr(lcolBranchrs.item(lintCount).nCover)
				.Columns("valCover").Descript = lcolBranchrs.item(lintCount).sGridCovDesc
				
				.Columns("valClient").DefValue = lcolBranchrs.item(lintCount).sClient
				.Columns("valClient").Descript = lcolBranchrs.item(lintCount).sCliename
				.Columns("valClient").Digit = lcolBranchrs.item(lintCount).sDigit
				
				If Request.QueryString.Item("nCapital_cov") = vbNullString Then
					
					If lcolBranchrs.item(lintCount).nCapital = eRemoteDB.Constants.intNull Then
						.Columns("tcnCapital").DefValue = CStr(0)
					Else
						.Columns("tcnCapital").DefValue = CStr(lcolBranchrs.item(lintCount).nCapital_cov)
					End If
				Else
					.Columns("tcnCapital").DefValue = Request.QueryString.Item("nCapital_cov")
				End If
				
				.Columns("cbeCurren_pol").DefValue = CStr(lcolBranchrs.item(lintCount).nCurrency)
				.Columns("cbeCurren_pol").Descript = lcolBranchrs.item(lintCount).sCurrDes
				
				'+Monto de Reserva				
				.Columns("tcnReserve").DefValue = CStr(lcolBranchrs.item(lintCount).nReserve)
				
				If lcolBranchrs.item(lintCount).nCapital = eRemoteDB.Constants.intNull Then
					.Columns("tcnReinCapital").DefValue = CStr(0)
					.Columns("tcnRetention").DefValue = CStr(0)
				Else
					.Columns("tcnReinCapital").DefValue = CStr(lcolBranchrs.item(lintCount).nCapital)
					.Columns("tcnRetention").DefValue = CStr(lcolBranchrs.item(lintCount).nRetention)
				End If
				
				.Columns("cbeChange").DefValue = CStr(lcolBranchrs.item(lintCount).nChange)
				.Columns("cbeChange").Descript = lcolBranchrs.item(lintCount).sChangeDes
				.Columns("hddChange").DefValue = CStr(lcolBranchrs.item(lintCount).nChange)
				.Columns("tctBrancht").DefValue = CStr(mclsProduct.sBrancht)
				.Columns("tctHeapCode").DefValue = lcolBranchrs.item(lintCount).sHeapcode
				
				'+ Si existe algun movimiento manual, se debe habilitar los link para modificar los contratos
				If lcolBranchrs.item(lintCount).nChange = 4 And lintCount = lintCountCov Then
					mobjGridC.Columns("cbeContract").EditRecord = True
				End If
				
				.Columns("cbeBranchrei").DefValue = CStr(lcolBranchrs.item(lintCount).nBranchRei)
				.Columns("cbeBranchrei").Descript = lcolBranchrs.item(lintCount).sBranch_reiDes
				
				'+Por cada cobertura se cargan los contratos relacionados a la misma.
				'lclsReinsuran = New ePolicy.Reinsuran
				lcolReinsurans = lcolBranchrs.item(lintCount).Reinsurans
		
				If lcolReinsurans.count > 0 Then
					For lintCountAUX = 1 To lcolReinsurans.count
						If lcolReinsurans.item(lintCountAUX).nType = 1 Then
							ldblRest = mobjValues.StringToType(ldblRest, eFunctions.Values.eTypeData.etdDouble) - mobjValues.StringToType(CStr(lcolBranchrs.item(lintCount).nRetention), eFunctions.Values.eTypeData.etdDouble)
						Else
							ldblRest = mobjValues.StringToType(ldblRest, eFunctions.Values.eTypeData.etdDouble) - mobjValues.StringToType(lcolReinsurans.item(lintCountAUX).nCapital, eFunctions.Values.eTypeData.etdDouble)
						End If
					Next 
                        'Response.Write("<SCRIPT>alert('Rest: " & ldblRest & "');</" & "Script>")
                     
					If ldblRest = eRemoteDB.Constants.intNull Then
						.Columns("tcnAmount").DefValue = CStr(0)
					Else
						.Columns("tcnAmount").DefValue = ldblRest
					End If
					
					ldblRest2 = mobjValues.StringToType(ldblRest2, eFunctions.Values.eTypeData.etdDouble) + mobjValues.StringToType(ldblRest, eFunctions.Values.eTypeData.etdDouble)
				End If
				
				mobjGridCov.sEditRecordParam = "sIsFACOB=3" & "&nChange=" & .Columns("cbeChange").DefValue & "&sHeap_Code=" & .Columns("tctHeapCode").DefValue & "&nCapital_cov=" & lcolBranchrs.item(lintCount).nCapital_cov
				lstrCover = lstrCover & .DoRow()
			End With
		Next 
		
		'+ Carga los datos de la cobertura seleccionada
		If Request.QueryString.Item("nBranchRei") = vbNullString Then
			lclsBranchr = lcolBranchrs.item(1)
		Else
			lclsBranchr = lcolBranchrs.item("A" & Request.QueryString.Item("nModulec") & Request.QueryString.Item("nCover") & Request.QueryString.Item("sClient") & Request.QueryString.Item("nBranchRei"))
		End If
		
		'+ Se toma la descripción de la cobertura para hacer referencia en el grid de contratos.
		mobjCoverDesc = lclsBranchr.sCoverDesc
		mobjsCliename = lclsBranchr.sCliename
		
		With lclsBranchr
			ldlbCapitalRein = .nCapital
			
                If .Reinsurans.Count <= 0 Then
                    mobjGridF.AddButton = False
                    mobjValues.ActionQuery = True
                End If
			
                ldblRest = .nRest
			
                mobjGridC.sEditRecordParam = mobjGridC.sEditRecordParam & "&nCapitalRein=" & mobjValues.TypeToString(ldlbCapitalRein, eFunctions.Values.eTypeData.etdDouble, True, 6) & "&sBrancht=" & mclsProduct.sBrancht & "&nBranchRei=" & .nBranchRei & "&nCurrency=" & .nCurrency & "&sHeapCode=" & lstrCumulCode & "&nModulec=" & .nModulec & "&nCover=" & .nCover & "&sClient=" & .sClient & "&nRetention=123" & "&nRest=" & ldblRest & "&nCapital_cov=" & Request.QueryString.Item("nCapital_cov")
			
                '+Para edicion del Grid Facultativo
                mobjGridF.sEditRecordParam = mobjGridF.sEditRecordParam & "&nCapitalRein=" & mobjValues.TypeToString(ldlbCapitalRein, eFunctions.Values.eTypeData.etdDouble, True, 6) & "&sBrancht=" & mclsProduct.sBrancht & "&nBranchRei=" & .nBranchRei & "&nCurrency=" & .nCurrency & "&sHeapCode=" & lstrCumulCode & "&nModulec=" & .nModulec & "&nCover=" & .nCover & "&sClient=" & .sClient & "&nCapital_cov=" & .nCapital_cov ' Request.QueryString("nCapital_cov")
			
                '+Para eliminar del Grid Facultativo
                mobjGridF.sDelRecordParam = mobjGridF.sDelRecordParam & "&nBranchRei=" & CStr(.nBranchRei) & "&nModulec=" & .nModulec & "&nCover=" & .nCover & "&sClient=" & .sClient
            End With
		
		If ldblRest2 = 0 Then
			mobjGridC.Columns("cbeContract").EditRecord = False
			mobjGridF.AddButton = False
			mobjGridF.sReloadIndex = vbNullString
		End If
		
		lcolReinsurans = lclsBranchr.Reinsurans
		
		'+Se recorre el ramo de reaseguro, para mostrar los contratos
		lclsReinsuran = New ePolicy.Reinsuran
		
		lintCount = 1
		
		lstrFCOB = vbNullString
		lstrContract = vbNullString
		
		'+Se informa que no existen contratos asociados a la póliza.
		If lcolReinsurans.count = 0 Then
			lclsError = New eFunctions.Errors
			'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.42.04
			lclsError.sSessionID = Session.SessionID
			lclsError.nUsercode = Session("nUsercode")
			'~End Body Block VisualTimer Utility
			Response.Write(lclsError.ErrorMessage("CA021", 6142, 0,  ,  , True))
			lclsError = Nothing
			mobjValues.actionQuery = True
			lintError = 1
		End If
		Do While lintCount <= lcolReinsurans.count
			lclsReinsuran = lcolReinsurans.item(lintCount)
			
			If lclsReinsuran.nType <> 1 Then
				
				'+En el caso de facultativo, se utiliza la variable lstrFACOB
				If lclsReinsuran.nType = 4 Then
					With mobjGridF
						.Columns("cbeCompany").DefValue = CStr(lclsReinsuran.nCompany)
						.Columns("tcnClasific").DefValue = CStr(lclsReinsuran.nClasific)
						.Columns("tcnParticip").DefValue = CStr(lclsReinsuran.nCapital)
						.Columns("tcnPercentage").DefValue = CStr(lclsReinsuran.nShare)
						.Columns("tcnReser_rate").DefValue = CStr(lclsReinsuran.nReser_rate)
						.Columns("tcnInter_rate").DefValue = CStr(lclsReinsuran.nInter_rate)
						.Columns("tcnComission").DefValue = CStr(lclsReinsuran.nCommissi)
						.Columns("tcdAcceptDate").DefValue = CStr(lclsReinsuran.dAcceDate)
					End With
					
					lstrFCOB = lstrFCOB & mobjGridF.DoRow()
				Else
					With mobjGridC
						.Columns("cbeContract").DefValue = lclsReinsuran.nNumber & " - " & lclsReinsuran.sContraDes
						.Columns("tcnParticip").DefValue = CStr(lclsReinsuran.nCapital)
						.Columns("tcnRetention").DefValue = CStr(lclsReinsuran.nCapital)
						'	.Columns("tctCompany").DefValue   = lclsReinsuran.sCompany
						.Columns("tcnType").DefValue = CStr(lclsReinsuran.nType)
						
						'+Si el monto del contrato es mayor a cero muestra el % de participacion del contrato.
						If lclsReinsuran.nCapital > 0 Then
							.Columns("tcnPercentage").DefValue = CStr(lclsReinsuran.nShare)
						Else
							.Columns("tcnPercentage").DefValue = CStr(0)
						End If
						
						.Columns("tcnCapitalMax").DefValue = CStr(lclsReinsuran.nCapitalMax)
						.Columns("tctBrancht").DefValue = CStr(mclsProduct.sBrancht)
						.Columns("tcnAllowedChange").DefValue = "2"
						.Columns("tcnNumber").DefValue = CStr(lclsReinsuran.nNumber)
					End With
					lstrContract = lstrContract & mobjGridC.DoRow()
				End If
                Else
                    
                    Response.Write(mobjValues.HiddenControl("tctHeapCode", lclsReinsuran.sHeap_code))
			End If
			lintCount = lintCount + 1
		Loop 
	Else
		lclsError = New eFunctions.Errors
		Response.Write(lclsError.ErrorMessage("CA021", 6142, 0,  ,  , True))
		lclsError = Nothing
		mobjValues.actionQuery = True
		mobjGridF.AddButton = False
		lintError = 1
	End If
	
	
	lstrCover = lstrCover & mobjGridCov.closeTable()
	lstrContract = lstrContract & mobjGridC.closeTable()
	lstrFCOB = lstrFCOB & mobjGridF.closeTable()
	
	
Response.Write("" & vbCrLf)
Response.Write("    <TABLE WIDTH=""100%"" COLS=""5"">" & vbCrLf)
Response.Write("	<TR>" & vbCrLf)
Response.Write("	    <TD COLSPAN=""8"" CLASS=""HighLighted""><LABEL><A NAME=""Coberturas"">" & GetLocalResourceObject("AnchorCoberturasCaption") & "</A></LABEL></td>" & vbCrLf)
Response.Write("	</TR>" & vbCrLf)
Response.Write("	<TR>" & vbCrLf)
Response.Write("	    <TD WIDTH=""100%"" COLSPAN=""8"" CLASS=""HorLine""></TD>" & vbCrLf)
Response.Write("	</TR>" & vbCrLf)
Response.Write("  </TABLE>  ")

	
	Response.Write(lstrCover)
	
Response.Write("" & vbCrLf)
Response.Write("	<TABLE WIDTH=""100%"">	" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			")

	Response.Write("<BR>")
	If lintError <> 1 Then
Response.Write("" & vbCrLf)
Response.Write("			      <TD COLSPAN=""4"" CLASS=""HighLighted""><LABEL><A NAME=""Contratos"">Contratos para ")

		Response.Write(mobjCoverDesc)
Response.Write(" - ")

		Response.Write(mobjsCliename)
Response.Write("</A></LABEL></TD>" & vbCrLf)
Response.Write("			")

	Else
Response.Write("" & vbCrLf)
Response.Write("				  <TD COLSPAN=""4"" CLASS=""HighLighted""><LABEL><A NAME=""Contratos"">" & GetLocalResourceObject("AnchorContratosCaption") & "</A></LABEL></TD>" & vbCrLf)
Response.Write("			")

	End If
Response.Write("" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("		<TR><TD COLSPAN=""4"" CLASS=""HorLine""></TD></TR>" & vbCrLf)
Response.Write("	</TABLE>")

	
	Response.Write(lstrContract)
	
Response.Write("" & vbCrLf)
Response.Write("	<TABLE WIDTH=""100%"">" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			")

	Response.Write("<BR>")
Response.Write("" & vbCrLf)
Response.Write("  		    <TD CLASS=""HighLighted""><LABEL><A NAME=""Facultativo"">" & GetLocalResourceObject("AnchorFacultativoCaption") & "</A></LABEL></TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD CLASS=""HorLine""></TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("	</TABLE>")

	
	Response.Write(lstrFCOB)
	
	lcolBranchrs = Nothing
	
Response.Write("" & vbCrLf)
Response.Write("	<TABLE WIDTH=""100%"">" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("		    <TD>" & vbCrLf)
Response.Write("                ")

	If ldblRest2 = eRemoteDB.Constants.intNull Then ldblRest2 = 0
Response.Write("" & vbCrLf)
Response.Write("			    ")


Response.Write(mobjValues.HiddenControl("hddRest", mobjValues.StringToType(ldblRest2, eFunctions.Values.eTypeData.etdDouble)))


Response.Write("" & vbCrLf)
Response.Write("			</TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("	</TABLE>")
        
	'If Request.QueryString.Item("nBranchrei") = vbNullString Then
	'	Response.Write("<SCRIPT>insMakeURLCA021(" & lnBranchrei & ", " & lnModulec & ", " & lnCover & ", '" & lsClient & "')</" & "Script>")
	'End If
	
	mclsProduct = Nothing
	lclsCoinsuran = Nothing
End Sub

'------------------------------------------------------------------------------------------------------------------------------------------------------
Private Sub insPreCA021Upd()
	'------------------------------------------------------------------------------------------------------------------------------------------------------
	If Request.QueryString.Item("sIsFACOB") = "1" Then
		Call insPreCA021UpdF()
	ElseIf Request.QueryString.Item("sIsFACOB") = "2" Then 
		Call insPreCA021UpdC()
	ElseIf Request.QueryString.Item("sIsFACOB") = "3" Then 
		Call insPreCA021UpdCov()
	End If
End Sub

'------------------------------------------------------------------------------
'---------------------------- Tratamiento de las ventanas PopUps --------------
'------------------------------------------------------------------------------

'+Grid de Coberturas
'------------------------------------------------------------------------------------------------------------------------------------------------------
Private Sub insPreCA021UpdCov()
	'------------------------------------------------------------------------------------------------------------------------------------------------------
	If Request.QueryString.Item("Action") <> "Del" Then
		Response.Write("<SCRIPT>setTimeout('ChangeValue()', 100);</" & "Script>")
	End If
	With Request
		Response.Write(mobjValues.HiddenControl("blnContract", CStr(False)))
		Response.Write(mobjValues.HiddenControl("tctSetting", ""))
		Response.Write(mobjValues.HiddenControl("tctPopUpT", "Cov"))
		Response.Write(mobjValues.HiddenControl("hddTypeRel", ""))
		Response.Write("<SCRIPT>self.document.forms[0].hddTypeRel.value = 1;</" & "Script>")
		Response.Write(mobjGridCov.DoFormUpd(.QueryString.Item("Action"), "valPolicySeq.aspx", .QueryString.Item("sCodispl"), .QueryString.Item("nMainAction"), mobjValues.actionQuery, CShort(.QueryString.Item("Index"))))
	End With
End Sub

'+Grid de Contratos
'------------------------------------------------------------------------------------------------------------------------------------------------------
Private Sub insPreCA021UpdC()
	'------------------------------------------------------------------------------------------------------------------------------------------------------
	With Request
		Response.Write(mobjGridC.DoFormUpd(.QueryString.Item("Action"), "valPolicySeq.aspx", .QueryString.Item("sCodispl"), .QueryString.Item("nMainAction"), mobjValues.actionQuery, CShort(.QueryString.Item("Index"))))
		
		Response.Write(mobjValues.HiddenControl("blnContract", CStr(True)))
		Response.Write(mobjValues.HiddenControl("tctSetting", ""))
		Response.Write(mobjValues.HiddenControl("tctPopUpT", "C"))
		Response.Write(mobjValues.HiddenControl("tcnCapitalRein", .QueryString.Item("nCapitalRein")))
		Response.Write(mobjValues.HiddenControl("cbeBranchrei", .QueryString.Item("nBranchRei")))
		Response.Write(mobjValues.HiddenControl("tcnModulec", .QueryString.Item("nModulec")))
		Response.Write(mobjValues.HiddenControl("valCover", .QueryString.Item("nCover")))
		Response.Write(mobjValues.HiddenControl("valClient", .QueryString.Item("sClient")))
		Response.Write(mobjValues.HiddenControl("tcnCurrency", .QueryString.Item("nCurrency")))
		Response.Write(mobjValues.HiddenControl("tctHeap_code", .QueryString.Item("sHeapCode")))
	End With
End Sub

'+Grid de Facultativo
'------------------------------------------------------------------------------------------------------------------------------------------------------
Private Sub insPreCA021UpdF()
	'------------------------------------------------------------------------------------------------------------------------------------------------------
	Dim lclsReinsuran As ePolicy.Reinsuran
	
	With Request
		If Request.QueryString.Item("Action") = "Del" Then
			lclsReinsuran = New ePolicy.Reinsuran
			
			Response.Write(mobjValues.ShowWindowsName("CA021", Request.QueryString.Item("sWindowDescript")))
			Response.Write(mobjValues.ConfirmDelete())
			With lclsReinsuran
				.sCertype = Session("sCertype")
				.nBranch = Session("nBranch")
				.nProduct = Session("nProduct")
				.nPolicy = Session("nPolicy")
				.nCertif = Session("nCertif")
				.dEffecdate = Session("dEffecdate")
				.nBranch_rei = mobjValues.StringToType(Request.QueryString.Item("nBranchRei"), eFunctions.Values.eTypeData.etdDouble)
				.nModulec = mobjValues.StringToType(Request.QueryString.Item("nModulec"), eFunctions.Values.eTypeData.etdDouble)
				.nCover = mobjValues.StringToType(Request.QueryString.Item("nCover"), eFunctions.Values.eTypeData.etdDouble)
				.sClient = Request.QueryString.Item("sClient")
				.nType = mobjValues.StringToType(Request.QueryString.Item("nType"), eFunctions.Values.eTypeData.etdDouble)
				.nNumber = mobjValues.StringToType(Request.QueryString.Item("nNumber"), eFunctions.Values.eTypeData.etdDouble)
				.nCompany = mobjValues.StringToType(Request.QueryString.Item("nCompany"), eFunctions.Values.eTypeData.etdDouble)
				Call .Delete(False)
				Session("sPopupT") = "F"
			End With
			lclsReinsuran = Nothing
		Else
			Response.Write(mobjValues.HiddenControl("blnContract", CStr(False)))
			Response.Write(mobjValues.HiddenControl("tcnCapitalRein", .QueryString.Item("nCapitalRein")))
			Response.Write(mobjValues.HiddenControl("tctBrancht", .QueryString.Item("sBrancht")))
			Response.Write(mobjValues.HiddenControl("cbeBranchrei", .QueryString.Item("nBranchRei")))
			Response.Write(mobjValues.HiddenControl("tcnModulec", Request.QueryString.Item("nModulec")))
			Response.Write(mobjValues.HiddenControl("valCover", Request.QueryString.Item("nCover")))
			Response.Write(mobjValues.HiddenControl("valClient", Request.QueryString.Item("sClient")))
			Response.Write(mobjValues.HiddenControl("tcnCurrency", .QueryString.Item("nCurrency")))
			Response.Write(mobjValues.HiddenControl("tctHeap_code", Request.QueryString.Item("sHeapCode")))
			Response.Write(mobjValues.HiddenControl("tcnAmount", Request.QueryString.Item("nAmount")))
			Response.Write(mobjValues.HiddenControl("tctSetting", ""))
			Response.Write(mobjValues.HiddenControl("tctPopUpT", "F"))
			
			Response.Write(mobjValues.HiddenControl("tcnCapital_cov", Request.QueryString.Item("nCapital_cov")))
			
			Response.Write("<SCRIPT>")
			Response.Write("    with(document.forms[0]){")
			Response.Write("        tctSetting.value = top.opener.document.location.href.replace(/.*OnSeq=1/,'');")
			Response.Write("		tctSetting.value = tctSetting.value.replace(/sKeep=[12]/,'');")
			Response.Write("		tctSetting.value = tctSetting.value.replace(/&nBranchRei=[1234567890]*/,'');")
			Response.Write("		tctSetting.value = tctSetting.value.replace(/&nModulec=[1234567890]*/,'');")
			Response.Write("		tctSetting.value = tctSetting.value.replace(/&nCover=[1234567890]*/,'');")
			Response.Write("		tctSetting.value = tctSetting.value.replace(/&sClient=[1234567890]*/,'')}")
			Response.Write("</" & "Script>")
		End If
		Response.Write(mobjGridF.DoFormUpd(.QueryString.Item("Action"), "valPolicySeq.aspx", .QueryString.Item("sCodispl"), .QueryString.Item("nMainAction"), mobjValues.actionQuery, CShort(.QueryString.Item("Index"))))
	End With
End Sub

</script>
<%
Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("CA021")

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.42.04
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "CA021"

mobjValues.actionQuery = Session("bQuery")



%>
<SCRIPT LANGUAGE="JavaScript" SRC="../../Scripts/GenFunctions.js"></SCRIPT>
<HTML>
<HEAD>


    <META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
<%
With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjValues.WindowsTitle("CA021"))
	
	If Request.QueryString.Item("Type") <> "PopUp" Then
		mobjMenu = New eFunctions.Menues
		
		'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.42.04
		mobjMenu.sSessionID = Session.SessionID
		mobjMenu.nUsercode = Session("nUsercode")
		'~End Body Block VisualTimer Utility
		
		.Write(mobjMenu.setZone(2, Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), CShort(Request.QueryString.Item("nWindowTy"))))
		.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction;</SCRIPT>")
	End If
End With
primera = "S"
mobjMenu = Nothing
%>

<SCRIPT>

    //- Variable para el control de versiones
    document.VssVersion = "$$Revision: 18 $|$$Date: 8/09/04 16.12 $|$$Author: Nvaplat60 $"

    //%insShowShare.Para calcular el porcentaje de participación
    //-----------------------------------------------------------------------------
    function insShowShare(lintCapital) {
        //-----------------------------------------------------------------------------
        var lintValue

        if ((lintCapital != '') && (lintCapital != '0')) {
            //		lintValue = (insConvertNumber(lintCapital) * 100) / insConvertNumber(document.forms[0].tcnCapitalRein.value);
            lintValue = (insConvertNumber(lintCapital) * 100) / insConvertNumber(document.forms[0].tcnCapital_cov.value);
        } else {
            lintValue = 0;
        }

        if (lintValue > 100) {
            alert('El monto excede el 100% de participación del reaseguro');
            document.forms[0].tcnParticip.value = 0;
            document.forms[0].tcnPercentage.value = 0;
        } else {
            document.forms[0].tcnParticip.value = lintCapital;
            document.forms[0].tcnPercentage.value = VTFormat(lintValue, '', '', '', 6, true);
            $(document.forms[0].tcnPercentage).change();
        }
    }

    //ChangeValue: Re-calcula el monto "Por Distribuir" en el grid "mobjGridCov"
    //-----------------------------------------------------------------------------
    function ChangeValue() {
        //-----------------------------------------------------------------------------
        with (self.document.forms[0]) {
            ldblAmount = insConvertNumber(tcnReinCapital.value) - insConvertNumber(tcnRetention.value);
            tcnAmount.value = VTFormat(ldblAmount, '', '', '', 6, true);
            if (ldblAmount < 0)
                tcnAmount.value = VTFormat(0, '', '', '', 6, true);
            else
               // $(tcnAmount).change();
               tcnAmount.onblur();
        }
    }

    //%insMakeURLCA021: Funcion que es invocada desde el boton del grid de coberturas, para que recarge de la página
    //--------------------------------------------------------------------------------------------
    function insMakeURLCA021(nBranch_rei, nModulec, nCover, sClient) {
        //--------------------------------------------------------------------------------------------
        var lstrLocation = document.location.href.replace(/sOnSeq=1.*/, 'sOnSeq=1')

        lstrLocation = lstrLocation + "&nBranchRei=" + nBranch_rei + "&nModulec=" + nModulec + "&nCover=" + nCover + "&sClient=" + sClient + "&nMode=4";
        document.location.href = lstrLocation;
    }

</SCRIPT>

</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="frmCA021" ACTION="ValPolicyseq.aspx?blnMassive=True">
<%
If Request.QueryString.Item("Type") <> "PopUp" Then
	%>
	    <P ALIGN="CENTER">						
	        <LABEL><A HREF="#Coberturas"> <%= GetLocalResourceObject("AnchorCoberturas2Caption") %></A></LABEL><LABEL> | </LABEL>
	        <LABEL><A HREF="#Contratos"> <%= GetLocalResourceObject("AnchorContratos2Caption") %></A></LABEL><LABEL> | </LABEL>
	        <LABEL><A HREF="#Facultativo"> <%= GetLocalResourceObject("AnchorFacultativo2Caption") %></A></LABEL>
	    </P>
<%	
End If

If Request.QueryString.Item("Action") <> "Del" Or Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write(mobjValues.ShowWindowsName("CA021", Request.QueryString.Item("sWindowDescript")))
	Response.Write("<BR>")
End If

'+Se define el grid de coberturas
Call insDefineHeaderCov()

'+Se define el grid de contratos     
Call insDefineHeaderC()

'+Se define el grid de facultativo
Call insDefineHeaderF()

If Request.QueryString.Item("Type") <> "PopUp" Then
	Call insPreCA021()
Else
	Call insPreCA021Upd()
End If

mobjGridC = Nothing
mobjGridF = Nothing
mobjGridCov = Nothing
mobjValues = Nothing

%>
</FORM>
</BODY>
</HTML>

<%
'^Begin Footer Block VisualTimer Utility 1.1 31/3/03 19.42.04
Call mobjNetFrameWork.FinishPage("CA021")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer
%>




