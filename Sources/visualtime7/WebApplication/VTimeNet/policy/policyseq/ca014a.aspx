<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eGeneral" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="ePolicy" %>
<script language="VB" runat="Server">
    '^Begin Header Block VisualTimer Utility 1.1 31/3/03 19.42.03
    Dim mobjNetFrameWork As eNetFrameWork.Layout

    Dim mobjValues As eFunctions.Values
    Dim mobjMenu As eFunctions.Menues
    Dim mobjGrid As eFunctions.Grid
    Dim mclsCover As ePolicy.Cover
    Dim mclsTCover As ePolicy.TCover
    Dim mobjGeneral As eGeneral.GeneralFunction

    Dim mblnFound As Boolean
    Dim mblnTransaction As Boolean
    Dim mintGroup As Object
    Dim mintCurrency As Object
    Dim mdblLegAmount As Object

    Dim mstrError As String


    '%insDefineHeader: Este procedimiento se encarga de definir las líneas del encabezado del grid.
    '---------------------------------------------------------------------------------------
    Private Sub insDefineHeader()
	'---------------------------------------------------------------------------------------
	Dim lobjColumn As eFunctions.Column
	Dim lintLength As Short
	Dim lstrVar As String

	'+ Sólo tiene sentido para póliza matriz
	lintLength = 30
	If Request.QueryString.Item("Type") <> "PopUp" Then
		lintLength = 120

		If Request.QueryString.Item("reloadaction") = "Update" Then
			lstrVar = "2"
		Else
			If Request.QueryString.Item("sDelTCover") <> "1" And Request.QueryString.Item("sDelTCover") <> "2" Then
				lstrVar = "1"
			Else
				lstrVar = Request.QueryString.Item("sDelTCover")
			End If
		End If

		If mclsCover.insPreCA014A(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), Session("dEffecdate"), mobjValues.StringToType(Request.QueryString.Item("nGroup"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nCurrency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToDate(Session("dNulldate")), Session("nTransaction"), Session("sBrancht"), Request.QueryString.Item("sKey"), Session("nUsercode"), Session("SessionId"), lstrVar, False, mobjValues.StringToType(Session("nType_amend"), eFunctions.Values.eTypeData.etdLong)) Then
			mblnFound = True
		End If
		mintGroup = mclsCover.nGroup
		mintCurrency = mclsCover.nCurrency
		mdblLegAmount = mclsCover.nLegAmount
		mblnTransaction = mclsCover.bTransaction
	End If

	'+ Variable para controlar la actualización de la información de manera puntual (desde el botón de la ventana)
	Response.Write(mobjValues.HiddenControl("hddbPuntual", CStr(False)))
	Response.Write(mobjValues.HiddenControl("hddbCopiar", CStr(False)))

	mobjGrid = New eFunctions.Grid
	'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.42.03
	mobjGrid.sSessionID = Session.SessionID
	mobjGrid.nUsercode = Session("nUsercode")
	'~End Body Block VisualTimer Utility

	mobjGrid.sCodisplPage = Request.QueryString.Item("sCodispl")
	Call mobjGrid.SetWindowParameters(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), Request.QueryString.Item("nWindowTy"))

	'+Se definen todas las columnas del Grid
	With mobjGrid.Columns
		Call .AddHiddenColumn("hddnExist", CStr(2))
		Call .AddCheckColumn(0, GetLocalResourceObject("chkRequiredColumnCaption"), "chkRequired", "",  ,  , "insChangeRequired()", Request.QueryString.Item("Type") <> "PopUp" Or (Request.QueryString.Item("Type") = "PopUp" And Request.QueryString.Item("sRequire") = "1"), GetLocalResourceObject("chkRequiredColumnToolTip"))
		Call .AddCheckColumn(0, GetLocalResourceObject("chkDefaultiColumnCaption"), "chkDefaulti", "",  ,  ,  , Request.QueryString.Item("Type") <> "PopUp" Or (Request.QueryString.Item("Type") = "PopUp" And Request.QueryString.Item("sRequire") = "1"), GetLocalResourceObject("chkDefaultiColumnToolTip"))
		lobjColumn = .AddNumericColumn(40805, GetLocalResourceObject("tcnModulecColumnCaption"), "tcnModulec", 5, CStr(0), True, GetLocalResourceObject("tcnModulecColumnToolTip"), True, 0,  ,  ,  , True)
		lobjColumn.EditRecord = True
		lobjColumn = .AddPossiblesColumn(0, GetLocalResourceObject("cbeRoleColumnCaption"), "cbeRole", "Table12", eFunctions.Values.eValuesType.clngComboType, "",  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeRoleColumnToolTip"))
		lobjColumn.EditRecord = True
		lobjColumn = .AddNumericColumn(40806, GetLocalResourceObject("tcnCoverColumnCaption"), "tcnCover", 5, CStr(0), True, GetLocalResourceObject("tcnCoverColumnToolTip"), True, 0,  ,  ,  , True)
		lobjColumn.EditRecord = True
		lobjColumn = .AddTextColumn(40785, GetLocalResourceObject("tctCoverColumnCaption"), "tctCover", lintLength, vbNullString,  , GetLocalResourceObject("tctCoverColumnToolTip"),  ,  ,  , True)
		lobjColumn.EditRecord = True
		Call .AddNumericColumn(40807, GetLocalResourceObject("tcnCapitalColumnCaption"), "tcnCapital", 18, CStr(0), True, GetLocalResourceObject("tcnCapitalColumnToolTip"), True, 6)
		lobjColumn = .AddNumericColumn(0, GetLocalResourceObject("tcnRateColumnCaption"), "tcnRate", 9, CStr(0), False, GetLocalResourceObject("tcnRateColumnToolTip"), True, 6,  ,  ,  , True)
		lobjColumn.GridVisible = False
		Call .AddNumericColumn(40792, GetLocalResourceObject("tcnRatecoveColumnCaption"), "tcnRatecove", 9, CStr(0), True, GetLocalResourceObject("tcnRatecoveColumnToolTip"), True, 6)
		Call .AddNumericColumn(40793, GetLocalResourceObject("tcnPremiumColumnCaption"), "tcnPremium", 18, CStr(0), True, GetLocalResourceObject("tcnPremiumColumnToolTip"), True, 6)

		lobjColumn = .AddPossiblesColumn(0, GetLocalResourceObject("cbeRetarifColumnCaption"), "cbeRetarif", "Table5559", eFunctions.Values.eValuesType.clngComboType, CStr(8),  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeRetarifColumnToolTip"))
		lobjColumn.BlankPosition = False
		lobjColumn.GridVisible = False

		lobjColumn = .AddPossiblesColumn(0, GetLocalResourceObject("valBranch_reiColumnCaption"), "valBranch_rei", "Table5000", eFunctions.Values.eValuesType.clngWindowType,  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("valBranch_reiColumnToolTip"))
		lobjColumn.BlankPosition = False
		lobjColumn.GridVisible = False

		'+Columnas que aplican solamente para Vida
		If Session("sBrancht") = 1 Then
			lobjColumn = .AddPossiblesColumn(0, GetLocalResourceObject("cbenTypAgeMinMColumnCaption"), "cbenTypAgeMinM", "Table5589", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbenTypAgeMinMColumnToolTip"))
			lobjColumn.BlankPosition = False
			lobjColumn.GridVisible = False
			lobjColumn.TypeList = CShort("1")
			lobjColumn.List = "2,9"

			lobjColumn = .AddPossiblesColumn(0, GetLocalResourceObject("cbenTypAgeMinFColumnCaption"), "cbenTypAgeMinF", "Table5589", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbenTypAgeMinFColumnToolTip"))
			lobjColumn.BlankPosition = False
			lobjColumn.GridVisible = False
			lobjColumn.TypeList = CShort("1")
			lobjColumn.List = "2,9"

			lobjColumn = .AddNumericColumn(0, GetLocalResourceObject("tcnAgemininsColumnCaption"), "tcnAgeminins", 3, "",  , GetLocalResourceObject("tcnAgemininsColumnToolTip"))
			lobjColumn.GridVisible = False
			lobjColumn = .AddNumericColumn(0, GetLocalResourceObject("tcnAgemininsfColumnCaption"), "tcnAgemininsf", 3, "",  , GetLocalResourceObject("tcnAgemininsfColumnToolTip"))
			lobjColumn.GridVisible = False
			lobjColumn = .AddNumericColumn(0, GetLocalResourceObject("tcnAgemaxinsColumnCaption"), "tcnAgemaxins", 2, "",  , GetLocalResourceObject("tcnAgemaxinsColumnToolTip"))
			lobjColumn.GridVisible = False
			lobjColumn = .AddNumericColumn(0, GetLocalResourceObject("tcnAgemaxinsfColumnCaption"), "tcnAgemaxinsf", 2, "",  , GetLocalResourceObject("tcnAgemaxinsfColumnToolTip"))
			lobjColumn.GridVisible = False
			lobjColumn = .AddNumericColumn(0, GetLocalResourceObject("tcnAgemaxperColumnCaption"), "tcnAgemaxper", 2, "",  , GetLocalResourceObject("tcnAgemaxperColumnToolTip"))
			lobjColumn.GridVisible = False
			lobjColumn = .AddNumericColumn(0, GetLocalResourceObject("tcnAgemaxperfColumnCaption"), "tcnAgemaxperf", 2, "",  , GetLocalResourceObject("tcnAgemaxperfColumnToolTip"))
			lobjColumn.GridVisible = False
		End If


		Call .AddHiddenColumn("cbeFrandedi", vbNullString)
		Call .AddHiddenColumn("tcnFraRate", vbNullString)
		Call .AddHiddenColumn("cbeFrancApl", vbNullString)
		Call .AddHiddenColumn("tcnFixamount", vbNullString)
		Call .AddHiddenColumn("tcnMinAmount", vbNullString)
		Call .AddHiddenColumn("tcnMaxAmount", vbNullString)
		Call .AddHiddenColumn("tcnDiscount", vbNullString)
		Call .AddHiddenColumn("tcnDisc_amoun", vbNullString)

		lobjColumn = .AddPossiblesColumn(40804, GetLocalResourceObject("cbeWait_typeColumnCaption"), "cbeWait_type", "Table52", 1, CStr(0),  ,  ,  ,  , "insEnableControls(this)", True,  , GetLocalResourceObject("cbeWait_typeColumnCaption"))
		lobjColumn.BlankPosition = False
		lobjColumn.GridVisible = False
		lobjColumn = .AddNumericColumn(40814, GetLocalResourceObject("tcnWaitQColumnCaption"), "tcnWaitQ", 5, CStr(0), False, GetLocalResourceObject("tcnWaitQColumnToolTip"), True, 0,  ,  ,  , True)
		lobjColumn.GridVisible = False
		lobjColumn = .AddNumericColumn(40815, GetLocalResourceObject("tcnPremimaxColumnCaption"), "tcnPremimax", 18, CStr(0), True, GetLocalResourceObject("tcnPremimaxColumnToolTip"), True, 6)
		lobjColumn.GridVisible = False
		lobjColumn = .AddNumericColumn(40816, GetLocalResourceObject("tcnPremiminColumnCaption"), "tcnPremimin", 18, CStr(0), True, GetLocalResourceObject("tcnPremiminColumnToolTip"), True, 6)
		lobjColumn.GridVisible = False
		lobjColumn = .AddNumericColumn(40817, GetLocalResourceObject("tcnCacalmaxColumnCaption"), "tcnCacalmax", 18, CStr(0), True, GetLocalResourceObject("tcnCacalmaxColumnToolTip"), True, 6)
		lobjColumn.GridVisible = False
		lobjColumn = .AddNumericColumn(40818, GetLocalResourceObject("tcnCacalminColumnCaption"), "tcnCacalmin", 18, CStr(0), True, GetLocalResourceObject("tcnCacalminColumnToolTip"), True, 6)
		lobjColumn.GridVisible = False


		Call .AddHiddenColumn("hddsChange", CStr(1))
		Call .AddHiddenColumn("hddnPremifix", vbNullString)
		Call .AddHiddenColumn("hddnPremiRat", vbNullString)
		Call .AddHiddenColumn("hddnCoverApl", vbNullString)
		Call .AddHiddenColumn("hddnCover_in", vbNullString)
		Call .AddHiddenColumn("hddnPremimin", vbNullString)
		Call .AddHiddenColumn("hddnPremiMax", vbNullString)
		Call .AddHiddenColumn("hddnGenCurrency", vbNullString)
		Call .AddHiddenColumn("hddnCapital_o", vbNullString)
		Call .AddHiddenColumn("hddnPremium_o", vbNullString)
		Call .AddHiddenColumn("hddnRatecove_o", vbNullString)
		Call .AddHiddenColumn("hddsKeyGrid", vbNullString)
		Call .AddHiddenColumn("hddsParam", vbNullString)
		Call .AddHiddenColumn("hddsRequire", "2")
		Call .AddHiddenColumn("hddnRole", vbNullString)
		Call .AddHiddenColumn("hddsRoupremi", vbNullString)
		Call .AddHiddenColumn("hddSeekTar", vbNullString)
		Call .AddHiddenColumn("hddnApply_perc", vbNullString)

		'+ Se agregan las columnas ocultas para el manejo de creación sin POPUP
		Call .AddHiddenColumn("hddnCapital", vbNullString)
		Call .AddHiddenColumn("hddnRateCove", vbNullString)
		Call .AddHiddenColumn("hddnPremium", vbNullString)
		Call .AddHiddenColumn("hddnCover", vbNullString)
		Call .AddHiddenColumn("hddnModulec", vbNullString)
		Call .AddHiddenColumn("hddsFrandedi", vbNullString)
		Call .AddHiddenColumn("hddsWait_type", vbNullString)
		Call .AddHiddenColumn("hddsFrancApl", vbNullString)
		Call .AddHiddenColumn("hddnDisc_amoun", vbNullString)
		Call .AddHiddenColumn("hddnFraRate", vbNullString)
		Call .AddHiddenColumn("hddnDiscount", vbNullString)
		Call .AddHiddenColumn("hddnFixAmount", vbNullString)
		Call .AddHiddenColumn("hddnMaxAmount", vbNullString)
		Call .AddHiddenColumn("hddnMinAmount", vbNullString)
		Call .AddHiddenColumn("hddnWaitQ", vbNullString)
		Call .AddHiddenColumn("hddnCapital_Wait", vbNullString)

		Call .AddHiddenColumn("hddnTypAgeMinM", vbNullString)
		Call .AddHiddenColumn("hddnTypAgeMinF", vbNullString)

		Call .AddHiddenColumn("hddnAgeminins", vbNullString)
		Call .AddHiddenColumn("hddnAgemaxins", vbNullString)
		Call .AddHiddenColumn("hddnAgemaxper", vbNullString)
		Call .AddHiddenColumn("hddnAgemininsf", vbNullString)
		Call .AddHiddenColumn("hddnAgemaxinsf", vbNullString)
		Call .AddHiddenColumn("hddnAgemaxperf", vbNullString)
		Call .AddHiddenColumn("hddnTypdurins", vbNullString)
		Call .AddHiddenColumn("hddnDurinsur", vbNullString)
		Call .AddHiddenColumn("hddnTypdurpay", vbNullString)
		Call .AddHiddenColumn("hddnDurpay", vbNullString)
		Call .AddHiddenColumn("hddnBranch_Rei", vbNullString)
		Call .AddHiddenColumn("hddnRetarif", vbNullString)
		Call .AddHiddenColumn("hddnCauseupd", vbNullString)
		Call .AddHiddenColumn("hdddfer", vbNullString)
		Call .AddHiddenColumn("hddsExist", vbNullString)
		Call .AddHiddenColumn("hddnAgeIns", vbNullString)
		Call .AddHiddenColumn("hddnCapital_req", vbNullString)
		Call .AddHiddenColumn("tcnCapital_req", vbNullString)
		'INI UGVT7
		Call .AddHiddenColumn("hddFraRateClaim", String.Empty)
		Call .AddHiddenColumn("hddFixamountClaim", String.Empty)
		Call .AddHiddenColumn("hddMinAmountClaim", String.Empty)
		Call .AddHiddenColumn("hddMaxAmountClaim", String.Empty)
		Call .AddHiddenColumn("hddDiscountClaim", String.Empty)
		Call .AddHiddenColumn("hddDisc_amounClaim", String.Empty)
		Call .AddHiddenColumn("hddFrancdays", String.Empty)
		'FIN UGVT7
	End With

	With mobjGrid
		.ActionQuery = Session("bQuery")
		.Codispl = Request.QueryString.Item("sCodispl")
		.FieldsByRow = 2
		'+Si se trata de un producto de vida
		If Session("sBrancht") = 1 Then
			.Top = 5
			.Left = 10
			.Width = 770
			.Height = 460
		Else
			.Top = 55
			.Left = 10
			.Width = 770
			.Height = 420
		End If

		.Splits_Renamed.AddSplit(0, "", 4)
		.Splits_Renamed.AddSplit(0, GetLocalResourceObject("2ColumnCaption"), 2)

		.DeleteButton = False
		.AddButton = False
		.nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
		.sDelRecordParam = "' + marrArray[lintCount].hddsParam + '"

		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
		.EditRecordQuery = mobjValues.ActionQuery
	End With
    End Sub

    '% insPreCA014A: Obtiene la información de las coberturas de una póliza matriz
    '-------------------------------------------------------------------------------------------
    Private Sub insPreCA014A()
	'-------------------------------------------------------------------------------------------
	Dim lintIndex As Short
	Dim lblnOk As Boolean
	Dim lblnLeg As Boolean
	Dim lclsPolicy As ePolicy.Policy
	Dim lstrDataFound As String

	lblnLeg = True
	If CStr(Session("sBrancht")) = "1" Then
		lclsPolicy = New ePolicy.Policy
		Call lclsPolicy.Find(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), True)
		If lclsPolicy.sLeg = "1" Then
			'+ El campo tope de capital por evaluaciòn sólo se habilita si se trata de: Cotización con nómina temporal, Cotización de renovación.
			If Session("nTransaction") = 1 Or Session("nTransaction") = 3 Or Session("nTransaction") = 4 Or Session("nTransaction") = 6 Or Session("nTransaction") = 18 Or Session("nTransaction") = 28 Or Session("nTransaction") = 30 Or Session("nTransaction") = 12 Or Session("nTransaction") = 13 Or Session("nTransaction") = 24 Or Session("nTransaction") = 26 Then
				lblnLeg = False
			End If
		End If
		lclsPolicy = Nothing
	End If

        Response.Write("" & vbCrLf)
        Response.Write("    <TABLE WIDTH=""100%"" COLS=4>" & vbCrLf)
        Response.Write("		<TR>")


	'+ Si las especificaciones son por grupo
	If mclsCover.sTyp_module = "3" Then

            Response.Write("" & vbCrLf)
            Response.Write("		    <TD WIDTH=""22%""><LABEL ID=""13043"">" & GetLocalResourceObject("valGroupCaption") & "</LABEL></TD>" & vbCrLf)
            Response.Write("		    <TD WIDTH=""30%"">")


		mobjValues.ActionQuery = False
		With mobjValues.Parameters
			.Add("sCertype", Session("sCertype"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Add("nProduct", Session("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Add("nPolicy", Session("nPolicy"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Add("nCertif", Session("nCertif"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Add("dEffecdate", Session("dEffecdate"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		End With
		Response.Write(mobjValues.PossiblesValues("valGroup", "tabgroups_coll", eFunctions.Values.eValuesType.clngWindowType, mintGroup, True,  ,  ,  ,  , "insReload(this)", False,  , GetLocalResourceObject("valGroupToolTip")))
		Response.Write(mobjValues.HiddenControl("hddnGroup", mintGroup))
		Response.Write("<SCRIPT> mintGroupChange = '" & mintGroup & "'; </" & "Script>")

            Response.Write("" & vbCrLf)
            Response.Write("			</TD>")


	End If
	'+ Se escriben los Script de JavaScript para la forma masiva

        Response.Write("" & vbCrLf)
        Response.Write("            <TD WIDTH=""15%""><LABEL ID=13050>" & GetLocalResourceObject("cbeCurrencDesCaption") & "</LABEL></TD>" & vbCrLf)
        Response.Write("            <TD COLSPAN=""2"">" & vbCrLf)
        Response.Write("            ")

	mobjValues.TypeList = 1
	If mblnFound Then
		mobjValues.List = mclsCover.mclsCurren_pol.Charge_Combo
	Else
		mobjValues.List = "1"
	End If
	mobjValues.BlankPosition = False
	Response.Write(mobjValues.PossiblesValues("cbeCurrencDes", "Table11", eFunctions.Values.eValuesType.clngComboType, mintCurrency,  ,  ,  ,  ,  , "insReload(this)", mclsCover.nCountCurrency <= 1 Or Not mblnFound,  , GetLocalResourceObject("cbeCurrencDesToolTip")))
	Response.Write("<SCRIPT> mintCurrencyChange = '" & mintCurrency & "'; </" & "Script>")
	Response.Write(mobjValues.HiddenControl("hddnProdclas", CStr(mclsCover.nProdClas)))

        Response.Write("" & vbCrLf)
        Response.Write("			</TD>" & vbCrLf)
        Response.Write("        </TR>" & vbCrLf)
        Response.Write("	")

	If CStr(Session("sBrancht")) = "1" Then
            Response.Write("" & vbCrLf)
            Response.Write("        <TR>" & vbCrLf)
            Response.Write("            <TD><LABEL ID=0>" & GetLocalResourceObject("tcnLegCaption") & "</LABEL></TD>" & vbCrLf)
            Response.Write("            <TD>")


            Response.Write(mobjValues.NumericControl("tcnLeg", 18, mdblLegAmount,  , GetLocalResourceObject("tcnLegToolTip"), True, 6,  ,  ,  ,  , lblnLeg))


            Response.Write("</TD>" & vbCrLf)
            Response.Write("        </TR>" & vbCrLf)
            Response.Write("	")

	End If
        Response.Write("" & vbCrLf)
        Response.Write("")


	lblnOk = False
	If mblnFound Then
		'+ Si no se trata de consulta	
		If Not mobjValues.ActionQuery Then
			'+ Si el tratamiento es de un certificado
			If Session("nCerif") > 0 Then
				'+ Si existen más de una moneda a tratar
				If (mclsCover.mclsCurren_pol.CountCurrenPol + 1) > 1 Then
					lblnOk = True
				End If
			Else
				'+ Si se trata de una póliza matriz o individual
				'+ Si existen más de una moneda a tratar o si tiene más de un grupo de colectivo
				If (mclsCover.mclsCurren_pol.CountCurrenPol + 1) > 1 Or (mclsCover.sTyp_module = "3" And mclsCover.bFindGroup And mclsCover.nCountGroup > 1) Then
					lblnOk = True
				End If
			End If
		End If
	End If
	If mblnTransaction Then
		If lblnOk Then
			Response.Write("<TR><TD><LABEL ID=""0"">" & GetLocalResourceObject("btn_ApplyCaption") & "</LABEL></TD>")
			Response.Write("<TD> " & mobjValues.AnimatedButtonControl("btn_Apply", "/VTimeNet/images/FindPolicyOff.png", GetLocalResourceObject("btn_ApplyToolTip"),  , "insCopy()") & "</TD></TR>")
		End If
	End If
	If lblnOk Then
		Response.Write("<TD COLSPAN=""5"">" & "</TD>")
		Response.Write("<TD WIDTH=""5%"">" & mobjValues.AnimatedButtonControl("btn_Apply", "/VTimeNet/images/btnAcceptOff.png", GetLocalResourceObject("btn_ApplyToolTip"),  , "insAccept()",  , 10) & "</TD>")
	End If

        Response.Write("" & vbCrLf)
        Response.Write("    </TABLE>")


	Response.Write(mobjValues.HiddenControl("hddsKey", mclsCover.sKey))
	mobjGrid.Columns("hddsKeyGrid").DefValue = mclsCover.sKey

	'+Se recorren las coberturas encontradas, para mostrarlas en el Grid
	Dim lobjError As eFunctions.Errors
	If mblnFound Then
		lstrDataFound = "2"
		If mclsCover.mcolTCovers.bDataFound Then
			lstrDataFound = "1"
		End If
		Response.Write(mobjValues.HiddenControl("hddnDataFound", lstrDataFound))
		lintIndex = 0
		For	Each mclsTCover In mclsCover.mcolTCovers
			With mobjGrid
				.Columns("Sel").Checked = mclsTCover.nSel(mclsCover.mcolTCovers.bDataFound)
				.Columns("Sel").OnClick = "insCheckSelClick(this," & CStr(lintIndex) & ")"
				.Columns("tcnModulec").DefValue = CStr(mclsTCover.nModulec)
				.Columns("hddnModulec").DefValue = CStr(mclsTCover.nModulec)
				.Columns("tcnRate").DefValue = CStr(mclsTCover.nRatecove)
				.Columns("tcnRatecove").DefValue = CStr(mclsTCover.nRatecove)
				.Columns("hddnRatecove_o").DefValue = CStr(mclsTCover.nRateCove_o)
				.Columns("tcnPremium").DefValue = CStr(mclsTCover.nPremium)
				.Columns("hddnPremium_o").DefValue = CStr(mclsTCover.nPremium_o)
				.Columns("cbeRole").DefValue = CStr(mclsTCover.nRole)
				.Columns("tcnCover").DefValue = CStr(mclsTCover.nCover)
				.Columns("tctCover").DefValue = mclsTCover.sDescript
				.Columns("tcnCapital").DefValue = CStr(mclsTCover.nCapital)
				.Columns("cbeFrandedi").DefValue = mclsTCover.sFrandedi
				.Columns("tcnFixamount").DefValue = CStr(mclsTCover.nFixamount)
				.Columns("tcnFraRate").DefValue = CStr(mclsTCover.nRate)
				.Columns("tcnMinAmount").DefValue = CStr(mclsTCover.nMinamount)
				.Columns("tcnMaxAmount").DefValue = CStr(mclsTCover.nMaxamount)
				.Columns("cbeFrancapl").DefValue = mclsTCover.sFrancapl
				.Columns("tcnDiscount").DefValue = CStr(mclsTCover.nDiscount)
				.Columns("tcnDisc_amoun").DefValue = CStr(mclsTCover.nDisc_Amoun)

				.Columns("cbeWait_type").DefValue = mclsTCover.sWait_type
				.Columns("tcnWaitQ").DefValue = CStr(mclsTCover.nWait_quan)

				.Columns("hddnPremifix").DefValue = CStr(mclsTCover.nPremifix)
				.Columns("hddnPremiRat").DefValue = CStr(mclsTCover.nPremiRat)
				.Columns("hddnCoverApl").DefValue = CStr(mclsTCover.nCoverApl)
				.Columns("hddnCover_in").DefValue = CStr(mclsTCover.nCover_in)
				.Columns("hddnPremimin").DefValue = CStr(mclsTCover.nPremimin)
				.Columns("hddnPremiMax").DefValue = CStr(mclsTCover.nPremimax)
				.Columns("hddnGenCurrency").DefValue = CStr(mclsTCover.nTarifCurr)
				.Columns("hddnCapital_o").DefValue = CStr(mclsTCover.nCapital_o)
				.Columns("hddsChange").DefValue = mclsTCover.sChange
				.Columns("hddsRequire").DefValue = mclsTCover.sRequired
				.Columns("hddsExist").DefValue = mclsTCover.sExist
				.Columns("tcnPremimax").DefValue = CStr(mclsTCover.nPremimax)
				.Columns("tcnPremimin").DefValue = CStr(mclsTCover.nPremimin)
				.Columns("tcnCacalmax").DefValue = CStr(mclsTCover.nCacalmax)
				.Columns("tcnCacalmin").DefValue = CStr(mclsTCover.nCacalmin)

				.Columns("hddsParam").DefValue = "sKey=" & mclsCover.sKey & "&nCover=" & mclsTCover.nCover & "&nModulec=" & mclsTCover.nModulec & "&nGroup=" & mclsTCover.nGroup & "&nCurrency=" & mclsTCover.nCurrency & "&nRole=" & mclsTCover.nRole & "&sRequire=" & mclsTCover.sRequired

				.sEditRecordParam = "sRequire=' + marrArray[" & CStr(lintIndex) & "].hddsRequire + '"


				.Columns("hddnRole").DefValue = CStr(mclsTCover.nRole)
				If Session("sBrancht") = 1 Then
					.Columns("cbenTypAgeMinM").DefValue = CStr(mclsTCover.nTyp_AgeMinM)
					.Columns("cbenTypAgeMinF").DefValue = CStr(mclsTCover.nTyp_AgeMinF)
					.Columns("tcnAgeminins").DefValue = CStr(mclsTCover.nAgeminins)
					.Columns("tcnAgemaxins").DefValue = CStr(mclsTCover.nAgemaxins)
					.Columns("tcnAgemaxper").DefValue = CStr(mclsTCover.nAgemaxper)
					.Columns("tcnAgemininsf").DefValue = CStr(mclsTCover.nAgemininsf)
					.Columns("tcnAgemaxinsf").DefValue = CStr(mclsTCover.nAgemaxinsf)
					.Columns("tcnAgemaxperf").DefValue = CStr(mclsTCover.nAgemaxperf)
				End If
				.Columns("cbeRetarif").DefValue = CStr(mclsTCover.nRetarif)

				If mclsTCover.sRequirec = "1" Then
					.Columns("chkRequired").Checked = CShort("1")
					.Columns("chkRequired").Disabled = CBool("1")
					.Columns("chkDefaulti").Disabled = CBool("1")
				Else
					.Columns("chkRequired").Checked = CShort("2")
					.Columns("chkRequired").Disabled = CBool("2")
					.Columns("chkDefaulti").Disabled = CBool("2")
				End If

				If mclsTCover.sDefaultic = "1" Then
					.Columns("chkDefaulti").Checked = CShort("1")
				Else
					.Columns("chkDefaulti").Checked = CShort("2")
				End If

				.Columns("valBranch_rei").DefValue = CStr(mclsTCover.nBranch_rei)
				.Columns("hddsRoupremi").DefValue = mclsTCover.sRoupremi
				.Columns("hddSeekTar").DefValue = CStr(mclsTCover.dSeekTar)
				.Columns("hddnApply_perc").DefValue = CStr(mclsTCover.nApply_perc)

				'+ Se agregan las columnas ocultas para el manejo de creación sin POPUP
				.Columns("hddnCapital").DefValue = CStr(mclsTCover.nCapital)
				.Columns("hddnRateCove").DefValue = CStr(mclsTCover.nRatecove)
				.Columns("hddnPremium").DefValue = CStr(mclsTCover.nPremium)
				.Columns("hddnCover").DefValue = CStr(mclsTCover.nCover)
				.Columns("hddnModulec").DefValue = CStr(mclsTCover.nModulec)
				.Columns("hddsFrandedi").DefValue = mclsTCover.sFrandedi
				.Columns("hddsWait_type").DefValue = mclsTCover.sWait_type
				.Columns("hddsFrancApl").DefValue = mclsTCover.sFrancapl
				.Columns("hddnDisc_amoun").DefValue = CStr(mclsTCover.nDisc_Amoun)
				.Columns("hddnFraRate").DefValue = CStr(mclsTCover.nRate)
				.Columns("hddnDiscount").DefValue = CStr(mclsTCover.nDiscount)
				.Columns("hddnFixAmount").DefValue = CStr(mclsTCover.nFixamount)
				.Columns("hddnMaxAmount").DefValue = CStr(mclsTCover.nMaxamount)
				.Columns("hddnMinAmount").DefValue = CStr(mclsTCover.nMinamount)
				.Columns("hddnWaitQ").DefValue = CStr(mclsTCover.nWait_quan)
				.Columns("hddnCapital_Wait").DefValue = CStr(mclsTCover.nCapital_wait)
				.Columns("hddnTypAgeMinM").DefValue = CStr(mclsTCover.nTyp_AgeMinM)
				.Columns("hddnTypAgeMinF").DefValue = CStr(mclsTCover.nTyp_AgeMinF)
				.Columns("hddnAgeminins").DefValue = CStr(mclsTCover.nAgeminins)
				.Columns("hddnAgemaxins").DefValue = CStr(mclsTCover.nAgemaxins)
				.Columns("hddnAgemaxper").DefValue = CStr(mclsTCover.nAgemaxper)
				.Columns("hddnAgemininsf").DefValue = CStr(mclsTCover.nAgemininsf)
				.Columns("hddnAgemaxinsf").DefValue = CStr(mclsTCover.nAgemaxinsf)
				.Columns("hddnAgemaxperf").DefValue = CStr(mclsTCover.nAgemaxperf)
				.Columns("hddnTypdurins").DefValue = CStr(mclsTCover.nTypdurins)
				.Columns("hddnDurinsur").DefValue = CStr(mclsTCover.nDurinsur)
				.Columns("hddnTypdurpay").DefValue = CStr(mclsTCover.nDurpay)
				.Columns("hddnDurpay").DefValue = CStr(mclsTCover.nDurpay)
				.Columns("hddnBranch_Rei").DefValue = CStr(mclsTCover.nBranch_rei)
				.Columns("hddnRetarif").DefValue = CStr(mclsTCover.nRetarif)
				.Columns("hddnCauseupd").DefValue = CStr(mclsTCover.nCauseupd)
				.Columns("hdddfer").DefValue = CStr(mclsTCover.dFer)
				.Columns("hddsExist").DefValue = mclsTCover.sExist
				Response.Write(.DoRow)
			End With
			lintIndex = lintIndex + 1
		Next mclsTCover
	Else
		If mclsCover.nError > 0 Then
			lobjError = New eFunctions.Errors
			'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.42.03
			lobjError.sSessionID = Session.SessionID
			lobjError.nUsercode = Session("nUsercode")
			'~End Body Block VisualTimer Utility
			Response.Write(lobjError.ErrorMessage("CA014A", mclsCover.nError,  ,  ,  , True))
			lobjError = Nothing
		End If
	End If
	'+Se cierra el recorrido de la tabla
	Response.Write(mobjGrid.CloseTable())
	Response.Write(mobjValues.BeginPageButton)
    End Sub

    '%insPreCA014AUpd. Esta ventana se encarga de mostrar el código correspondiente a la
    '%actualización de las coberturas.
	'---------------------------------------------------------------------------------------
    Private Sub insPreCA014AUpd()
        '---------------------------------------------------------------------------------------

	With mobjGrid
		.Columns("tcnCapital").OnChange = "insCalPremium(""1"")"
		.Columns("tcnRatecove").OnChange = "insCalPremium(""2"")"
		.Columns("cbeRetarif").OnChange = "insCalPremium(""1"")"
	End With

	With Response
		.Write(mobjGrid.DoFormUpd(Request.QueryString.Item("Action"), "ValPolicySeq.aspx", Request.QueryString.Item("sCodispl"), Request.QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(Request.QueryString.Item("Index"))))
		.Write(mobjValues.HiddenControl("hddnCurrency", mintCurrency))
		.Write(mobjValues.HiddenControl("hddnGroup", mintGroup))
		.Write(mobjValues.HiddenControl("hddnLegAmount", CStr(0)))
	End With

	If Not mobjValues.ActionQuery Then

            Response.Write("" & vbCrLf)
            Response.Write("<SCRIPT>" & vbCrLf)
            Response.Write("		if(typeof(top.opener.document.forms[0].valGroup)!='undefined')" & vbCrLf)
            Response.Write("			top.frames['fraFolder'].document.forms[0].hddnGroup.value = top.opener.document.forms[0].valGroup.value;" & vbCrLf)
            Response.Write("		else" & vbCrLf)
            Response.Write("			top.frames['fraFolder'].document.forms[0].hddnGroup.value = 0;" & vbCrLf)
            Response.Write("    " & vbCrLf)
            Response.Write("		top.frames['fraFolder'].document.forms[0].hddnCurrency.value = top.opener.document.forms[0].cbeCurrencDes.value;" & vbCrLf)
            Response.Write("		insEnableControls("""");" & vbCrLf)
            Response.Write("</" & "SCRIPT>")


		If Session("sBrancht") = 1 Then
			Response.Write("<SCRIPT>top.frames['fraFolder'].document.forms[0].hddnLegAmount.value = top.opener.document.forms[0].tcnLeg.value;</" & "Script>")
		End If
	End If
    End Sub

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("CA014A")
'~End Header Block VisualTimer Utility
Response.CacheControl = "private"

mobjGeneral = New eGeneral.GeneralFunction
mstrError = mobjGeneral.insLoadMessage(55963)
mobjGeneral = Nothing

With Server
	mobjValues = New eFunctions.Values
	'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.42.03
	mobjValues.sSessionID = Session.SessionID
	mobjValues.nUsercode = Session("nUsercode")
	'~End Body Block VisualTimer Utility
	
	mobjValues.sCodisplPage = Request.QueryString.Item("sCodispl")
	mobjMenu = New eFunctions.Menues
	'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.42.03
	mobjMenu.sSessionID = Session.SessionID
	mobjMenu.nUsercode = Session("nUsercode")
	'~End Body Block VisualTimer Utility
End With

mobjValues.ActionQuery = Session("bQuery")
%>

<HTML>
<HEAD>
    <META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>


<SCRIPT>
//+ Variable para el control de versiones
    document.VssVersion="$$Revision: 6 $|$$Date: 21/10/03 9:42 $"

	var mintGroupChange = 0; mintCurrencyChange = 0; mlngCapital = 0; mlngRatecove = 0; mintRetarif = 0;

//% insAccept: Se acpta la secuencia en tratamiento 
//------------------------------------------------------------------------------------------
function insAccept(){
//------------------------------------------------------------------------------------------
    with (self.document.forms[0]) {
		self.document.forms[0].hddbPuntual.value = true;
		self.document.forms[0].hddbCopiar.value = false;
	}
	top.frames['fraHeader'].ClientRequest(390,2);
}
//% insCopy: Se copian las coberturas en todos los grupos
//------------------------------------------------------------------------------------------
function insCopy(){
//------------------------------------------------------------------------------------------
    with (self.document.forms[0]) {
		self.document.forms[0].hddbCopiar.value = true;
		self.document.forms[0].hddbPuntual.value = true;
	}
	top.frames['fraHeader'].ClientRequest(390,2);
}

//% insCheckSelClick: controla la columna Sel, para mostrar la ventana PopUp    
//-------------------------------------------------------------------------------------------
function insCheckSelClick(Field,lintIndex){
//-------------------------------------------------------------------------------------------
    var lstrQueryString
    var lstrString
    var lstrString_Val
    var lintAge = ''
    var lstrGroup = ''

	if (typeof(mstrDoSubmit) == 'undefined') mstrDoSubmit = '1';
	if (mstrDoSubmit == '1'){
		if (typeof(self.document.forms[0].hddnAge) != 'undefined'){
			lintAge = self.document.forms[0].hddnAge.value;
		}
		lstrQueryString = '&nAge=' + lintAge;

		if(typeof(self.document.forms[0].valGroup)!='undefined')
		    lstrGroup = self.document.forms[0].valGroup.value;
		else
		    lstrGroup = '0';

		if (!Field.checked){
			if (marrArray[lintIndex].hddsRequire=="1"){
			    alert("Error 55963:" + "<%=mstrError%>");
			    Field.checked = !Field.checked;
			}
			else{
				lstrString = 'sKey=' + marrArray[lintIndex].hddsKeyGrid;
				lstrString = lstrString + '&nModulec=' + marrArray[lintIndex].tcnModulec;
				lstrString = lstrString + '&nCover=' + marrArray[lintIndex].tcnCover;
				lstrString = lstrString + '&nRole=' + marrArray[lintIndex].cbeRole;
			    lstrString = lstrString + '&nGroup=' + lstrGroup;
				setPointer('wait');
			    insDefValues("DelTCover", lstrString, '/VTimeNet/Policy/PolicySeq');
			}
		}
		else {
		    lstrString_Val = '&nGroup=' + lstrGroup;
		    lstrString_Val = lstrString_Val + '&nModulec=' + marrArray[lintIndex].tcnModulec;
		    lstrString_Val = lstrString_Val + '&nCover=' + marrArray[lintIndex].tcnCover;
		    lstrString_Val = lstrString_Val + '&nCurrency=' + self.document.forms[0].cbeCurrencDes.value;
		    lstrString_Val = lstrString_Val + '&nProdclas=' + self.document.forms[0].hddnProdclas.value;
		    lintIndex+=1;

			mstrDoSubmit = "2";
		    document.forms[0].action ="ValPolicySeq.aspx?nZone=2&sCodispl=CA014A&Action=Update&WindowType=PopUp&nMainAction=304&ActionType=Check&nIndex=" + lintIndex + lstrString_Val
		    top.frames["fraFolder"].document.forms[0].target="fraGeneric";
		    self.document.forms[0].cbeCurrencDes.disabled = false;
		    setPointer('wait');
		    self.document.forms[0].submit(); 
		    self.document.forms[0].cbeCurrencDes.disabled = true;
		}
	}
	else{
	    Field.checked = !Field.checked;
		alert('Por favor espere');
	}
}

//% insChangeRequired: Si se selecciona el campo requerido automáticamente se selecciona el campo pre-selección.
//-------------------------------------------------------------------------------------------
function insChangeRequired(){
//-------------------------------------------------------------------------------------------
    with (self.document.forms[0]) {
		if (chkRequired.checked)
			chkDefaulti.checked = chkRequired.checked;
	}
}

//% insReload: Se encarga de recargar la página al seleccionar cualquier valor de los campos del encabezado del grid.
//-------------------------------------------------------------------------------------------
function insReload(Field){
//-------------------------------------------------------------------------------------------
    var lstrQuery
    var lblnChange
    var lstrDelTCover

//+ Si se cambia la moneda entonces no se borra la tabla temporal tcover de lo contrario si (cuando se cambia el grupo)    
    with (self.document.forms[0]) {
		 lstrQuery = "&sKey=" + hddsKey.value;
//+ Caso en que el grupo esté visible
		if(typeof(valGroup)!='undefined'){
			if (mintGroupChange!=valGroup.value)
				lblnChange = true
			    mintGroupChange = valGroup.value;
			lstrQuery = lstrQuery + "&nGroup=" + valGroup.value
		} else
			lstrQuery = lstrQuery + "&nGroup=0"
		
		if (mintCurrencyChange!=cbeCurrencDes.value) {
			mintCurrencyChange = cbeCurrencDes.value;
			lblnChange = true;
		}

//+ Si hubo algún cambio en cuanto al grupo (si corresponde) o la moneda; se recarga la ventana.
		if (lblnChange==true) {

			lstrQuery = lstrQuery + "&nCurrency=" + cbeCurrencDes.value + "&sDelTCover=";
			document.location.href = document.location.href.replace(/&sKey=.*/,'') + lstrQuery
		}
    }
} 

//% insEnableControls: se habilitan/deshabilitan los campos de la página
//---------------------------------------------------------------------------------
function insEnableControls(Field){
//---------------------------------------------------------------------------------
    with (document.forms[0]){		
        tcnFraRate.disabled = (cbeFrandedi.value == 1)
        cbeFrancApl.disabled = (cbeFrandedi.value == 1)
        cbeFrancApl.value = (cbeFrandedi.value==1?1:cbeFrancApl.value)
        tcnFixamount.disabled = (cbeFrandedi.value == 1)
        tcnMinAmount.disabled = (cbeFrandedi.value == 1)
        tcnMaxAmount.disabled = (cbeFrandedi.value == 1)
        tcnDiscount.disabled = (cbeFrandedi.value == 1)
        tcnDisc_amoun.disabled = (cbeFrandedi.value == 1)
        cbeWait_type.disabled = false
        tcnWaitQ.disabled = (cbeWait_type.value== 1)
        tcnWaitQ.value = (cbeWait_type.value==1?'':tcnWaitQ.value)
        
        if (cbeFrandedi.value==1) {
			tcnFixamount.value = '0';
			tcnFraRate.value = '0,00';
			tcnMaxAmount.value = '0';
			tcnMinAmount.value = '0';
			tcnDiscount.value = '0';
			tcnDisc_amoun.value = '0,00';
        }
        
//		if (typeof(valBranch_rei) != 'undefined')
//+ Si el campo porcentaje de f/d tiene valor se inicializa el monto fijo de f/d
		if (Field.name=='tcnFraRate')
			if (tcnFraRate.value>0)
				tcnFixamount.value = '0'
			
//+ Si el campo monto fijo de f/d tiene valor se inicializa el porcentaje de f/d
		if (Field.name=='tcnFixamount')
			if (tcnFixamount.value>0)
				tcnFraRate.value = '0,00'
        
//+ Si el campo porcentaje de descuento de f/d tiene valor se inicializa el monto de descuento de f/d
        if (Field.name=='tcnDiscount')
			if (tcnDiscount.value>0)
				tcnDisc_amoun.value = '0,00'
        
//+ Si el campo monto de descuento de f/d tiene valor se inicializa el porcentaje de descuento de f/d
		if (Field.name=='tcnDisc_amoun')
			if (tcnDisc_amoun.value>0)
				tcnDiscount.value = '0,00'
    }
}

//% insCalPremium: se recarga la página en caso que se modifique la prima o tasa,
//%                   para calcular los valores de manera autómatica
//% sOrigen: Es para verificar si el procedimiento se llama de tasa, Capital o Retarifica
//----------------------------------------------------------------------------------------
function insCalPremium(sOrigen){
//----------------------------------------------------------------------------------------
    var lstrQueryString
    var llngCapital
    var llngCapital_o
    var llngRatecove
    var llngRatecove_o

    with (self.document.forms[0]){
        llngCapital    = insConvertNumber(tcnCapital.value);
        llngRatecove   = insConvertNumber(tcnRatecove.value);
//+ Si existe modificación en los campos Suma Asegurada y prima (sólo si se ha cambiado)
        llngCapital_o  = insConvertNumber(hddnCapital.value);
        llngRatecove_o = insConvertNumber(hddnRateCove.value);

        if (llngCapital != llngCapital_o || 
            llngRatecove != llngRatecove_o ||
            cbeRetarif.value != hddnRetarif.value){
            lstrQueryString = 'nCover=' + tcnCover.value + 
                              '&nModulec=' + tcnModulec.value +
                              '&nGroup=' + hddnGroup.value +
                              '&nRetarif=8' +
                              '&nCover_in=' + hddnCover_in.value +
                              '&sRoupremi=' + hddsRoupremi.value +
                              '&nCurrencyOri=' + hddnCurrency.value +
                              '&nCurrencyDes=' + hddnGenCurrency.value +
                              '&sKey=' + hddsKeyGrid.value +
                              '&nPremifix=' + hddnPremifix.value +
                              '&nPremirat=' + hddnPremiRat.value +
                              '&nCoverapl=' + hddnCoverApl.value +
                              '&dSeektar=' + hddSeekTar.value +
                              '&sBrancht=' + '<%=Session("sBrancht")%>' +
                              '&nApply_perc=' + hddnApply_perc.value +
                              '&nPremimin=' + hddnPremimin.value +
                              '&nPremimax=' + hddnPremiMax.value +
                              '&nCapital=' + tcnCapital.value +
                              '&nRatecove=' + tcnRatecove.value +
                              '&nRatecove_o=' + hddnRatecove_o.value +
                              '&nPremium=' + tcnPremium.value +
                              '&sOrigen=' + sOrigen +
                              '&nRole=' + hddnRole.value +
                              '&sExist=' + hddsExist.value

            insDefValues("Premium", lstrQueryString, '/VTimeNet/Policy/PolicySeq');
        }

/* Se inhabilita el campo de prima si el valor de la tasa es ingresado. */
        if (llngRatecove==0 || llngRatecove=='')
			tcnPremium.disabled = false;
		else
			tcnPremium.disabled = true;        
    }
}


</SCRIPT>
<%
With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjValues.WindowsTitle(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript")))
End With

If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write(mobjMenu.setZone(2, Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), CShort(Request.QueryString.Item("nWindowTy"))))
	mobjMenu = Nothing
	Response.Write("<SCRIPT>var nMainAction=" & Request.QueryString.Item("nMainAction") & ";</SCRIPT>")
End If
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<%=mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"))%>
<FORM METHOD="POST" ID="FORM" NAME="CA014A" ACTION="ValPolicySeq.aspx?mode=1">
<%
mclsCover = New ePolicy.Cover
Call insDefineHeader()
If Request.QueryString.Item("Type") = "PopUp" Then
	Call insPreCA014AUpd()
Else
	Call insPreCA014A()
End If
mclsCover = Nothing
mobjValues = Nothing
mobjGrid = Nothing
%>
</FORM>
</BODY>
</HTML>
<%'^Begin Footer Block VisualTimer Utility 1.1 31/3/03 19.42.03
Call mobjNetFrameWork.FinishPage("CA014A")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




