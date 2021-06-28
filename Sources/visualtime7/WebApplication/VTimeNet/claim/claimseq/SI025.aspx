<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eClaim" %>
<%@ Import namespace="eGeneral" %>
<%@ Import namespace="eProduct" %>
<%@ Import namespace="eBranches" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 11.35.15
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo de las rutinas genéricas	
Dim mobjMenu As eFunctions.Menues

'- Se define la variable mobjGrid para el manejo del Grid de la ventana
Dim mobjGrid As eFunctions.Grid

'- Se define las variables para la carga de datos del Grid de la ventana
Dim mclsCL_Cover As eClaim.Cl_Cover
Dim mcolCL_Covers As eClaim.CL_Covers
Dim mclsProdmaster As eProduct.Product

'+ Variable para almacenar la moneda de la última cobertura del grid
Dim mintOldCurrency As Object


'%insDefineHeader. Definición de columnas del GRID
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------	    
	With mobjGrid
		.Codispl = "SI025"
		.Top = 50
		.Left = 100
		.Width = 650
		.Height = 480
	End With
	
	'+Se definen todas las columnas del Grid
	
	With mobjGrid.Columns
		mobjGrid.Splits_Renamed.addsplit(0, "", 4)
		mobjGrid.Splits_Renamed.addsplit(0, "Limite", 2)
		mobjGrid.Splits_Renamed.addsplit(0, "", 1)
		mobjGrid.Splits_Renamed.addsplit(0, "Copago", 2)
		
		Call .AddClientColumn(CInt("0"), "RUT", "txtClient", "",  , "Código del cliente asociado al intermediario",  , True)
		Call .AddTextColumn(0, "Cobertura", "tctDescover", 60, "0",  , "Cobertura asociada a la poliza en tratamiento",  ,  ,  , True)
		Call .AddPossiblesColumn(40307, "Estado", "cbeReservstat", "Table141", eFunctions.Values.eValuesType.clngComboType)
		Call .AddPossiblesColumn(0, "Prestación", "tcnPrestac", "table160", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  , True,  , "Conceptos de prestaciones de pago permitidos para la cobertura indicada")
		'Limites
		Call .AddNumericColumn(40311, "Cantidad", "tcnQ_used", 18, CStr(0),  , "Franquicia/Deducible", True, 0,  ,  ,  , True)
		Call .AddNumericColumn(40311, "Importe", "tcnlimit", 18, CStr(0),  , "Franquicia/Deducible", True, 6,  ,  ,  , True)
		Call .AddNumericColumn(40311, "Exceso", "tcnPay_amount", 18, CStr(0),  , "Franquicia/Deducible", True, 6,  ,  ,  , True)
		'Gatos
		Call .AddNumericColumn(40311, "Cantidad", "tcnAmount", 18, CStr(0),  , "Franquicia/Deducible", True, 0,  ,  , "InsChangeAmo(this)", False)
		Call .AddNumericColumn(40311, "Importe", "tcnImport", 18, CStr(0),  , "Franquicia/Deducible", True, 6,  ,  , "InsChangeImp(this)", False)
		Call .AddNumericColumn(0, "% Deducible", "tcnDed_Percen", 4, vbNullString,  , "Indica el porcentaje a aplicar sobre el monto a indemnizar para obtener el monto de deducible correspondiente",  , 2)
		Call .AddNumericColumn(40311, "% Indemnización", "tcnIndem_Rate", 18, CStr(0),  , "Franquicia/Deducible", True, 6,  ,  ,  , True)
		Call .AddNumericColumn(40311, "Reserva", "tcnReserve", 18, CStr(0),  , "Franquicia/Deducible", True, 6,  ,  , "InsChangeRes()", False)
		
		Call .AddHiddenColumn("hhnReserve", "0")
		Call .AddHiddenColumn("tctClient", "")
		Call .AddHiddenColumn("tcnReserveAnt", "0")
		Call .AddHiddenColumn("nReserve_o", "0")
		Call .AddHiddenColumn("nExchange_o", "0")
		Call .AddHiddenColumn("sAutomRep", "0")
		Call .AddHiddenColumn("nFixAmount", "0")
		Call .AddHiddenColumn("nMaxAmount", "0")
		Call .AddHiddenColumn("nMinAmount", "0")
		Call .AddHiddenColumn("nRate", "0")
		Call .AddHiddenColumn("nModulec", "0")
		Call .AddHiddenColumn("nCover", "0")
		Call .AddHiddenColumn("tcnCapital", "0")
		Call .AddHiddenColumn("tcnBranch_rei", "0")
		Call .AddHiddenColumn("tcnBranch_led", "0")
		Call .AddHiddenColumn("tcnBranch_est", "0")
		Call .AddHiddenColumn("tcnFrandeda", "0")
		Call .AddHiddenColumn("tcnPayAmount", "0")
		Call .AddHiddenColumn("tcnGroup", "0")
		Call .AddHiddenColumn("tctFran_Ind", "")
		Call .AddHiddenColumn("tctRoureser", "")
		Call .AddHiddenColumn("tctInsurini", "")
		Call .AddHiddenColumn("tcnCase_num", "0")
		Call .AddHiddenColumn("tcnDeman_type", "0")
		Call .AddHiddenColumn("nCurrency_o", "0")
		Call .AddHiddenColumn("cbeCurrency", "0")
		Call .AddHiddenColumn("tctsIndCapIliCover", "")
		Call .AddHiddenColumn("tctsSchema", "")
		Call .AddHiddenColumn("tctCaren_type", "")
		Call .AddHiddenColumn("tcnCaren_quan", "0")
		Call .AddHiddenColumn("hddBill_ind", "1")
		Call .AddHiddenColumn("hddnMonto", CStr(0))
		Call .AddHiddenColumn("hddsFrancapl", "")
		Call .AddHiddenColumn("hddnFranAmount", CStr(0))
		Call .AddHiddenColumn("tcnExchange", "0")
		Call .AddHiddenColumn("tcnDamages", "0")
		Call .AddHiddenColumn("nPrestac", "0")
		
	End With
	
	With mobjGrid
		.Columns("tctDescover").EditRecord = True
		.Columns("Sel").OnClick = "insCheckSelClick(this)"
		.DeleteButton = False
		.AddButton = False
		.Columns("cbeReservstat").BlankPosition = False
		'.Columns("cbeReservstat").NotCache = true
		If Request.QueryString("Reload") = "1" Then
			.sReloadIndex = Request.QueryString("ReloadIndex")
		End If
		
		
	End With
End Sub

'%insPreSI025: Esta función se encarga de cargar los datos en la forma
'--------------------------------------------------------------------------------------------
Private Sub insPreSI025()
	Dim mclsCL_Coverma As eClaim.cl_coverma
	'--------------------------------------------------------------------------------------------    
	Dim lblnFind As Boolean
	Dim lstrAlert As String
	Dim lclsClaimCases As eClaim.ClaimBenefs
	Dim lstrFirstCase As String
	Dim lintCase_num As Integer
	Dim lintDeman_type As Integer
	Dim lstrClient As String
	Dim lclsClaim As eClaim.Claim
	
	Dim lobjErrors As eGeneral.GeneralFunction
	lobjErrors = New eGeneral.GeneralFunction
	
	lblnFind = False
	
	If Request.QueryString("nCase_num") = vbNullString Or Request.QueryString("nDeman_type") = vbNullString Or Request.QueryString("sClient") = vbNullString Then
		lclsClaimCases = New eClaim.ClaimBenefs
		If lclsClaimCases.Find_BenefByClaim(CDbl(Session("nClaim")), 0, 1) Then
			lstrFirstCase = CStr(lclsClaimCases.Item(1).nCase_num) & "/" & CStr(lclsClaimCases.Item(1).nDeman_type) & "/" & lclsClaimCases.Item(1).sClient
			lintCase_num = lclsClaimCases.Item(1).nCase_num
			lintDeman_type = lclsClaimCases.Item(1).nDeman_type
			lstrClient = lclsClaimCases.Item(1).sClient
			'UPGRADE_NOTE: Object lclsClaimCases may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
			lclsClaimCases = Nothing
			lblnFind = True
		End If
	Else
		
		lstrFirstCase = Request.QueryString("nCase_num") & "/" & Request.QueryString("nDeman_type") & "/" & Request.QueryString("sClient")
		lintCase_num = mobjValues.StringToType(Request.QueryString("nCase_num"), eFunctions.Values.eTypeData.etdDouble)
		lintDeman_type = mobjValues.StringToType(Request.QueryString("nDeman_type"), eFunctions.Values.eTypeData.etdDouble)
		lstrClient = Request.QueryString("sClient")
		lblnFind = True
	End If
	
	'+ Si la fecha de ocurrencia del siniestro está fuera de la vigencia de la póliza, no se realiza 
	'+ la lectura de las coberturas. VVERA 25/03/2003.	
	If CBool(Session("bPolicyVigency")) = True Then
		lblnFind = False
		lstrAlert = "Err. 4019 " & lobjErrors.insLoadMessage(4019)
		Response.Write("<SCRIPT>alert('" & lstrAlert & "')</" & "Script>")
	End If
	'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
	lobjErrors = Nothing
	
	
	Response.Write("<SCRIPT> mintCase_num='" & lintCase_num & "';</" & "Script>")
	Response.Write("<SCRIPT> mintDeman_type='" & lintDeman_type & "';</" & "Script>")
	Response.Write("<SCRIPT> mstrClient='" & lstrClient & "';</" & "Script>")
	
	'+Se verifica que el proceso no venga desde SI021	
	If CStr(Session("SI025_Codispl")) = vbNullString Then
		Session("sProcess_SI021") = 0
	End If
	
Response.Write("" & vbCrLf)
Response.Write("<TABLE WIDTH=""100%"" BORDER = 0>" & vbCrLf)
Response.Write("    <TR>" & vbCrLf)
Response.Write("        <TD><LABEL ID=9501>Caso</LABEL></TD>" & vbCrLf)
Response.Write("        <TD COLSPAN=""1"">")

	
	With mobjValues
		.BlankPosition = False
		.Parameters.Add("nClaim", Session("nClaim"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		Response.Write(mobjValues.PossiblesValues("cbeCase", "TabClaim_cases", eFunctions.Values.eValuesType.clngComboType, "" & lstrFirstCase, True,  ,  ,  ,  , "insChangeCase(this)",  ,  , "", eFunctions.Values.eTypeCode.eString))
		Response.Write(mobjValues.HiddenControl("tctclient", lstrClient))
		
		Response.Write(mobjValues.HiddenControl("tcnDeman_type", CStr(lintDeman_type)))
		Response.Write(mobjValues.HiddenControl("cbeCases", CStr(lintCase_num)))
	End With
	
	
Response.Write("" & vbCrLf)
Response.Write("		</TD>" & vbCrLf)
Response.Write("		<TD><LABEL ID=9501>Cobertura</LABEL></TD>" & vbCrLf)
Response.Write("		<TD>")

	
	' mobjValues.BlankPosition = False
	mobjValues.Parameters.Add("scertype", "2", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	mobjValues.Parameters.Add("nbranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	mobjValues.Parameters.Add("nproduct", Session("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	mobjValues.Parameters.Add("npolicy", Session("nPolicy"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	mobjValues.Parameters.Add("ncertif", Session("nCertif"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	mobjValues.Parameters.Add("dEffecdate", Session("dEffecdate"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	mobjValues.Parameters.Add("nmodulec", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	mobjValues.Parameters.Add("ngroup", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	mobjValues.Parameters.Add("sClient", lstrClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	Response.Write(mobjValues.PossiblesValues("nCover", "tabcoverpolicy_sc", eFunctions.Values.eValuesType.clngComboType, Request.QueryString("nCover"), True,  ,  ,  ,  , "insChangecover(this)"))
	
	
	
Response.Write("" & vbCrLf)
Response.Write("		</TD>		" & vbCrLf)
Response.Write("    </TR>" & vbCrLf)
Response.Write("    <TR>" & vbCrLf)
Response.Write("        <TD COLSPAN=""4"">&nbsp;</TD>" & vbCrLf)
Response.Write("    </TR>                " & vbCrLf)
Response.Write("</TABLE> " & vbCrLf)
Response.Write("")

	
	
	Dim lcolTab_Am_Bil As eBranches.Tab_am_Bils
	Dim lclsTab_Am_Bil As eBranches.Tab_Am_Bil
	Dim mcolCL_Coverma As eClaim.cl_covermas
	If lblnFind Then
		lclsClaim = New eClaim.Claim
		
		Call lclsClaim.Find(CDbl(Session("nClaim")))
		If CStr(Session("sBrancht")) = "" Then
			mclsProdmaster = New eProduct.Product
			Call mclsProdmaster.FindProdMaster(CInt(Session("nBranch")), CInt(Session("nProduct")))
			mclsProdmaster = New eProduct.Product
			Session("sBrancht") = mclsProdmaster.sBrancht
			'UPGRADE_NOTE: Object mclsProdmaster may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
			mclsProdmaster = Nothing
		End If
		If mobjValues.StringToType(CStr(Session("dOccurdate_l")), eFunctions.Values.eTypeData.etdDate) = eRemoteDB.Constants.dtmnull Then
			Session("dOccurdate_l") = Session("dEffecdate")
		End If
		
		lcolTab_Am_Bil = New eBranches.Tab_am_Bils
		lclsTab_Am_Bil = New eBranches.Tab_Am_Bil
		
		mcolCL_Coverma = New eClaim.cl_covermas
		If mcolCL_Coverma.insreacover_si025(CStr(Session("sCertype")), CInt(Session("nBranch")), CInt(Session("nProduct")), CDbl(Session("nPolicy")), CDbl(Session("nCertif")), CDate(Session("dEffecdate")), CInt(Session("nClaim")), lintCase_num, lintDeman_type, Request.QueryString("nOpt_claityp"), Request.QueryString("nCover"), "1") Then
			For	Each mclsCL_Coverma In mcolCL_Coverma
				With mobjGrid
					.Columns("txtClient").DefValue = mclsCL_Coverma.sClient
					.Columns("tctDescover").DefValue = mclsCL_Coverma.sDescover
					.Columns("nCover").DefValue = Request.QueryString("nCover")
					'If mclsCL_Coverma.nDed_percen = intNull Then
					'    .Columns("tcnDed_Percen").DefValue = 0
					'Else
					.Columns("tcnDed_Percen").DefValue = CStr(mclsCL_Coverma.nDed_percen)
					'End IF
					If (mclsCL_Coverma.nCaren_quan <> eRemoteDB.Constants.intNull And mclsCL_Coverma.nCaren_quan <> 0) Or (mclsCL_Coverma.nQuant_used <> eRemoteDB.Constants.intNull And mclsCL_Coverma.nQuant_used <> 0) Then
						'se setean sin decimales cuando son cantidades					     
						.Columns("tcnPay_amount").DecimalPlaces = 0
						.Columns("tcnLimit").DecimalPlaces = 0
						.Columns("tcnReserve").DecimalPlaces = 0
						.Columns("tcnImport").DecimalPlaces = 0
						.Columns("tcnDed_Percen").DecimalPlaces = 0
						.Columns("tcnIndem_rate").DecimalPlaces = 0
						
						.Columns("tcnQ_used").DefValue = CStr(mclsCL_Coverma.nCaren_quan - mclsCL_Coverma.nReserve)
						.Columns("tcnLimit").DefValue = CStr(0)
						.Columns("tcnImport").Disabled = True
						.Columns("tcnAmount").DefValue = CStr(mclsCL_Coverma.nQuant_used)
						If mclsCL_Coverma.nQuant_used > mclsCL_Coverma.nCaren_quan Then
							.Columns("tcnPay_amount").DefValue = CStr(mclsCL_Coverma.nQuant_used - mclsCL_Coverma.nCaren_quan)
						Else
							.Columns("tcnPay_amount").DefValue = CStr(0)
						End If
					Else
						' se setean con 6 decimales cuando es importe			        
						.Columns("tcnLimit").DecimalPlaces = 6
						.Columns("tcnPay_amount").DecimalPlaces = 6
						.Columns("tcnImport").DecimalPlaces = 6
						.Columns("tcnReserve").DecimalPlaces = 6
						.Columns("tcnDed_Percen").DecimalPlaces = 6
						.Columns("tcnIndem_rate").DecimalPlaces = 6
						
						If (lclsTab_Am_Bil.nlimit <> eRemoteDB.Constants.intNull) Then
							.Columns("tcnLimit").DefValue = CStr(mclsCL_Coverma.nlimit)
							.Columns("tcnQ_used").DefValue = CStr(0)
							.Columns("tcnAmount").Disabled = True
							.Columns("tcnAmount").DefValue = CStr(0)
						Else
							.Columns("tcnLimit").DefValue = CStr(mclsCL_Coverma.nLimit_h)
							.Columns("tcnQ_used").DefValue = CStr(0)
							.Columns("tcnAmount").Disabled = True
							.Columns("tcnAmount").DefValue = CStr(0)
						End If
						If mclsCL_Coverma.nAmount > mclsCL_Coverma.nlimit Then
							.Columns("tcnPay_amount").DefValue = CStr(mclsCL_Coverma.nAmount - mclsCL_Coverma.nlimit)
						Else
							.Columns("tcnPay_amount").DefValue = CStr(0)
						End If
					End If
					.Columns("tcnPrestac").DefValue = CStr(mclsCL_Coverma.Npay_Concep)
					.Columns("nPrestac").DefValue = CStr(mclsCL_Coverma.Npay_Concep)
					.Columns("tcnImport").DefValue = CStr(mclsCL_Coverma.nAmount)
					.Columns("tcnCase_num").DefValue = CStr(lintCase_num)
					.Columns("nCurrency_o").DefValue = CStr(mclsCL_Cover.nCurrency)
					.Columns("tcnReserve").DefValue = CStr(mclsCL_Coverma.nReserve)
					.Columns("hhnReserve").DefValue = CStr(mclsCL_Coverma.nReserve)
					If mclsCL_Coverma.nReserve <> 0 Then
						.Columns("Sel").Checked = 1
					Else
						.Columns("Sel").Checked = 0
					End If
					
					
					
					.Columns("cbeReservstat").DefValue = mclsCL_Coverma.sReservstat
					'.Columns("cbeReservstat").Descript  = mclsCL_Coverma.sReservstat   				
					.Columns("tcnDamages").DefValue = CStr(mclsCL_Coverma.nDamages)
					.Columns("tcnReserve").DefValue = CStr(mclsCL_Coverma.nReserve)
					.Columns("tcnReserveAnt").DefValue = CStr(mclsCL_Coverma.nReserve)
					.Columns("cbeCurrency").DefValue = CStr(mclsCL_Coverma.nCurrency)
					.Columns("tcnExchange").DefValue = CStr(mclsCL_Coverma.nExchange)
					.Columns("nCurrency_o").DefValue = CStr(mclsCL_Coverma.nCurrency)
					.Columns("sAutomRep").DefValue = mclsCL_Coverma.sAutomRep
					.Columns("nFixAmount").DefValue = CStr(mclsCL_Coverma.nFixAmount)
					.Columns("nMaxAmount").DefValue = CStr(mclsCL_Coverma.nMaxAmount)
					.Columns("nMinAmount").DefValue = CStr(mclsCL_Coverma.nMinAmount)
					.Columns("nRate").DefValue = CStr(mclsCL_Coverma.nRate)
					.Columns("nModulec").DefValue = CStr(mclsCL_Coverma.nModulec)
					.Columns("nCover").DefValue = CStr(mclsCL_Coverma.nCover)
					.Columns("tcnCapital").DefValue = CStr(mclsCL_Coverma.nCapital)
					.Columns("tcnBranch_rei").DefValue = CStr(mclsCL_Coverma.nBranch_rei)
					.Columns("tcnBranch_led").DefValue = CStr(mclsCL_Coverma.nBranch_led)
					.Columns("tcnBranch_est").DefValue = CStr(mclsCL_Coverma.nBranch_est)
					.Columns("tcnPayAmount").DefValue = CStr(mclsCL_Coverma.nPay_amount)
					.Columns("tcnGroup").DefValue = CStr(mclsCL_Coverma.nGroup)
					.Columns("tctRoureser").DefValue = mclsCL_Coverma.sRoureser
					.Columns("tctInsurini").DefValue = mclsCL_Coverma.sInsurini
					.Columns("tcnCase_num").DefValue = CStr(lintCase_num)
					.Columns("tcnDeman_type").DefValue = CStr(lintDeman_type)
					.Columns("tctCaren_type").DefValue = mclsCL_Coverma.sCaren_type
					.Columns("tcnCaren_quan").DefValue = CStr(mclsCL_Coverma.nCaren_quan)
					.Columns("hddsFrancapl").DefValue = mclsCL_Cover.sFrancapl
					
					Session("nTotal") = CDbl(Session("nTotal")) + mclsCL_Coverma.nReserve
					Response.Write(mobjGrid.DoRow)
				End With
			Next mclsCL_Coverma
			
			'		else
			'			If lcolTab_Am_bil.reatab_am_bil_si025(Session("sCertype"), '												   Session("nBranch"), '												   Session("nProduct"), '												   Session("nPolicy"), '												   Session("nCertif") , '												   0, '												   0, '												   Request.QueryString("nCover") , '												   2, '												   lstrClient, '												   Session("dEffecdate"))  then   
			'			Session("nTotal") = 0
			'			For Each lclsTab_Am_bil In lcolTab_Am_bil
			'				    mobjGrid.Columns("txtClient").DefValue    = lclsTab_Am_bil.sClient
			'					mobjGrid.Columns("tctDescover").DefValue   = lclsTab_Am_bil.nCover
			'					mobjGrid.Columns("cbeReservstat").DefValue = 1
			'					If (lclsTab_Am_bil.nLimit <> intNull) Then
			'					    mobjGrid.Columns("tcnLimit").DefValue   = lclsTab_Am_bil.nLimit
			'					Else
			'					   mobjGrid.Columns("tcnLimit").DefValue   = lclsTab_Am_bil.nLimit_h
			'					End IF
			'					
			'					With mobjGrid
			'						.Columns("tcnPrestac").DefValue = lclsTab_Am_bil.nPrestac
			'					if (lclsTab_Am_bil.nPrestac = 0) Then
			'						.Columns("tcnPrestac").Disabled = False
			'					End IF
			'						.Columns("tcnDed_Percen").DefValue  = lclsTab_Am_bil.nDed_percen
			'						.Columns("tcnIndem_Rate").DefValue = lclsTab_Am_bil.nIndem_rate			
			'						.Columns("tcnCase_num").DefValue = lintCase_num
			'						.Columns("cbeReservstat").DefValue = lclsTab_Am_bil.sReservstat
			'						.Columns("cbeReservstat").Descript  = lclsTab_Am_bil.sReservstat   				
			'						.Columns("tcnDamages").DefValue = lclsTab_Am_bil.nDamages
			'						.Columns("tcnReserve").DefValue = lclsTab_Am_bil.nReserve 
			'						.Columns("tcnReserveAnt").DefValue = lclsTab_Am_bil.nReserve 
			'						.Columns("cbeCurrency").DefValue = lclsTab_Am_bil.nCurrency 
			'						.Columns("tcnExchange").DefValue = lclsTab_Am_bil.nExchange
			'						.Columns("nCurrency_o").DefValue = lclsTab_Am_bil.nCurrency 
			'						.Columns("sAutomRep").DefValue = lclsTab_Am_bil.sAutomRep 
			'						.Columns("nFixAmount").DefValue = lclsTab_Am_bil.nFixAmount 
			'						.Columns("nMaxAmount").DefValue = lclsTab_Am_bil.nMaxAmount 
			'						.Columns("nMinAmount").DefValue = lclsTab_Am_bil.nMinAmount				
			'						.Columns("nRate").DefValue = lclsTab_Am_bil.nRate 
			'						.Columns("nModulec").DefValue = lclsTab_Am_bil.nModulec
			'						.Columns("nCover").DefValue = lclsTab_Am_bil.nCover 
			'						.Columns("tcnCapital").DefValue = lclsTab_Am_bil.nCapital
			'						.Columns("tcnBranch_rei").DefValue = lclsTab_Am_bil.nBranch_rei 
			'						.Columns("tcnBranch_led").DefValue = lclsTab_Am_bil.nBranch_led 
			'						.Columns("tcnBranch_est").DefValue = lclsTab_Am_bil.nBranch_est 
			'						.Columns("tcnPayAmount").DefValue = lclsTab_Am_bil.nPay_amount 
			'						.Columns("tcnGroup").DefValue = lclsTab_Am_bil.nGroup 
			'						.Columns("tctRoureser").DefValue = lclsTab_Am_bil.sRoureser 
			'						.Columns("tctInsurini").DefValue = lclsTab_Am_bil.sInsurini 
			'						.Columns("tcnDeman_type").DefValue = lintDeman_type
			'						.Columns("tctCaren_type").DefValue = lclsTab_Am_bil.sCaren_type
			'						.Columns("tcnCaren_quan").DefValue = lclsTab_Am_bil.nCaren_quan
			'						.Columns("hddsFrancapl").DefValue = lclsTab_Am_bil.sFrancapl
			'					End With
			'					    
			'					Response.Write mobjGrid.DoRow
			'
			'					'lintIndex = lintIndex + 1
			'			    Next		
			'			End If
		End If
	End If
	Response.Write(mobjGrid.CloseTable())
	Response.Write(mobjValues.HiddenControl("tcnOldCurrency", mintOldCurrency))
	'------------------------------------------------------------------------------------------------------------------
	
	
	
End Sub

'% insPreSI025Upd. Se define esta funcion para contruir el contenido de la ventana UPD de las reservas del sinietro
'------------------------------------------------------------------------------------------------------------------
Private Sub insPreSI025Upd()
	'------------------------------------------------------------------------------------------------------------------
	Dim lstrBoolean As Object
	Dim lcolTab_Am_Bil As Object
	Dim lclsTab_Am_Bil As Object
	Dim lintIndex As Object
	Dim lblnExist As Object
	Dim lblnHeader As Object
	
	Response.Write(mobjValues.HiddenControl("htcnDeman_typ", ""))
	Response.Write(mobjValues.HiddenControl("hcbeCase", ""))
	Response.Write(mobjValues.HiddenControl("hddClient", ""))
	
Response.Write("" & vbCrLf)
Response.Write("<SCRIPT>" & vbCrLf)
Response.Write("" & vbCrLf)
Response.Write("var mblnDo=false;" & vbCrLf)
Response.Write("//%FormatField: formatea el valor introducido en el campo ""Estimación según profesional""" & vbCrLf)
Response.Write("//---------------------------------------------------------------------------------" & vbCrLf)
Response.Write("function FormatField(Field){" & vbCrLf)
Response.Write("//---------------------------------------------------------------------------------" & vbCrLf)
Response.Write("	with (self.document.forms[0]){" & vbCrLf)
Response.Write("		if(elements[""tcnDamProf""].value!=''){" & vbCrLf)
Response.Write("			elements[""tcnDamProf""].value = VTFormat(insConvertNumber(Field),"""","""","""",6, true);" & vbCrLf)
Response.Write("		}else{" & vbCrLf)
Response.Write("			elements[""tcnDamProf""].value = VTFormat(insConvertNumber(0),"""","""","""",6, true);" & vbCrLf)
Response.Write("		}" & vbCrLf)
Response.Write("	}" & vbCrLf)
Response.Write("}" & vbCrLf)
Response.Write("" & vbCrLf)
Response.Write("//%AssignValues: Asigna los valores de la forma madre a la PopUp para luego pasarlos como Querystring" & vbCrLf)
Response.Write("//---------------------------------------------------------------------------------" & vbCrLf)
Response.Write("function AssignValues()" & vbCrLf)
Response.Write("//---------------------------------------------------------------------------------" & vbCrLf)
Response.Write("{	with (self.document.forms[0])" & vbCrLf)
Response.Write("	{" & vbCrLf)
Response.Write("		htcnDeman_typ.value = top.opener.document.forms[0].tcnDeman_type.value;" & vbCrLf)
Response.Write("		hcbeCase.value = top.opener.document.forms[0].cbeCases.value;" & vbCrLf)
Response.Write("		hddClient.value = top.opener.document.forms[0].tctclient.value;" & vbCrLf)
Response.Write("	}" & vbCrLf)
Response.Write("}" & vbCrLf)
Response.Write("//---------------------------------------------------------------------------------" & vbCrLf)
Response.Write("function insUpdValues(ldblDamages, ldblFrandeda, ldblReserve){" & vbCrLf)
Response.Write("//---------------------------------------------------------------------------------" & vbCrLf)
Response.Write("//    mdblFrandeda = ldblFrandeda;" & vbCrLf)
Response.Write("//    with (document.forms[0])" & vbCrLf)
Response.Write("//    {" & vbCrLf)
Response.Write("//       elements[""tcnDamages""].value = VTFormat(ldblDamages,"""","""","""",6, true);" & vbCrLf)
Response.Write("//        elements[""tcnFrandeda""].value = VTFormat(ldblFrandeda,"""","""","""",6, true);" & vbCrLf)
Response.Write("//        elements[""tcnReserve""].value = VTFormat(ldblReserve,"""","""","""",6, true);" & vbCrLf)
Response.Write("//    }" & vbCrLf)
Response.Write("}" & vbCrLf)
Response.Write("//---------------------------------------------------------------------------------" & vbCrLf)
Response.Write("function insCalReserve(){" & vbCrLf)
Response.Write("//---------------------------------------------------------------------------------" & vbCrLf)
Response.Write("    var ldblDamages=0;" & vbCrLf)
Response.Write("    var ldblDamages2=0;" & vbCrLf)
Response.Write("    var ldblFrandeda=0;" & vbCrLf)
Response.Write("    var ldblReserve=0;" & vbCrLf)
Response.Write("    var ldblCapital=0;" & vbCrLf)
Response.Write("    var ldblCapital_Cover=0;" & vbCrLf)
Response.Write("    var ldblFra_amount=0;" & vbCrLf)
Response.Write("    var ldblFranAmouPerc=0;" & vbCrLf)
Response.Write("" & vbCrLf)
Response.Write("    if (!mblnDo) return;" & vbCrLf)
Response.Write("	" & vbCrLf)
Response.Write("	with (document.forms[0]){" & vbCrLf)
Response.Write("" & vbCrLf)
Response.Write("//		if(tcnDamages.value==''){" & vbCrLf)
Response.Write("//			ldblDamages=0;" & vbCrLf)
Response.Write("//		}else{" & vbCrLf)
Response.Write("//			ldblDamages=tcnDamages.value;" & vbCrLf)
Response.Write("//		}" & vbCrLf)
Response.Write("" & vbCrLf)
Response.Write("//Estimacion de daños" & vbCrLf)
Response.Write("	    ldblDamages = ldblDamages;" & vbCrLf)
Response.Write("//Monto fijo." & vbCrLf)
Response.Write("		ldblFrandeda = tcnFrandeda.value;" & vbCrLf)
Response.Write("//Capital de la cobertura aplicada al porcentaje" & vbCrLf)
Response.Write("		ldblCapital = hddnMonto.value;" & vbCrLf)
Response.Write("//Capital de la cobertura" & vbCrLf)
Response.Write("		ldblCapital_Cover = tcnCapital.value;" & vbCrLf)
Response.Write("		" & vbCrLf)
Response.Write("//Monto Franquicia / Deducible en Porcentaje o Monto Fijo" & vbCrLf)
Response.Write("		ldblFra_amount = tcnFra_amount.value;" & vbCrLf)
Response.Write("//Monto pagado " & vbCrLf)
Response.Write("		lblPayAmount = tcnPayAmount.value;" & vbCrLf)
Response.Write("	" & vbCrLf)
Response.Write("//%tipo de aplicacion de la franquicia/deducible" & vbCrLf)
Response.Write("//%aplica sobre capital" & vbCrLf)
Response.Write("		if (hddsFrancapl.value=='2'){" & vbCrLf)
Response.Write("	//+Cuando es deducible" & vbCrLf)
Response.Write("			if (cbeFrantype.value==3){" & vbCrLf)
Response.Write("	//+ si es un porcentaje    " & vbCrLf)
Response.Write("			    if (cbeFrantype_aux.value==1){" & vbCrLf)
Response.Write("			    " & vbCrLf)
Response.Write("			 " & vbCrLf)
Response.Write("	//+ Provision es igual al monto del capital de la cobertura " & vbCrLf)
Response.Write("					if (insConvertNumber(ldblDamages) < insConvertNumber(ldblCapital)){" & vbCrLf)
Response.Write("						ldblReserve = VTFormat(insConvertNumber(0),'', '', '', 6, true);" & vbCrLf)
Response.Write("					}else{" & vbCrLf)
Response.Write("						ldblReserve = VTFormat(insConvertNumber(ldblDamages) - insConvertNumber(ldblCapital) ,'', '', '', 6, true);" & vbCrLf)
Response.Write("						ldblFranAmouPerc = insConvertNumber(ldblCapital); " & vbCrLf)
Response.Write("					}" & vbCrLf)
Response.Write("	//+Monto fijo" & vbCrLf)
Response.Write("				}else{" & vbCrLf)
Response.Write("					if (insConvertNumber(ldblDamages) < insConvertNumber(ldblFrandeda)){" & vbCrLf)
Response.Write("						ldblReserve = VTFormat(insConvertNumber(0),'', '', '', 6, true);" & vbCrLf)
Response.Write("					}else{" & vbCrLf)
Response.Write("						ldblReserve = VTFormat(insConvertNumber(ldblDamages) - insConvertNumber(ldblFrandeda) ,'', '', '', 6, true);" & vbCrLf)
Response.Write("					}" & vbCrLf)
Response.Write("				}" & vbCrLf)
Response.Write("	//+si es una franquicia" & vbCrLf)
Response.Write("			}else{" & vbCrLf)
Response.Write("				if (cbeFrantype.value==2){" & vbCrLf)
Response.Write("	//+ si es un porcentaje    " & vbCrLf)
Response.Write("	                 	      " & vbCrLf)
Response.Write("					if (cbeFrantype_aux.value==1){" & vbCrLf)
Response.Write("					" & vbCrLf)
Response.Write("					 " & vbCrLf)
Response.Write("						if (insConvertNumber(ldblDamages) > insConvertNumber(ldblCapital)){" & vbCrLf)
Response.Write("							ldblReserve = VTFormat(insConvertNumber(ldblDamages),'', '', '', 6, true);" & vbCrLf)
Response.Write("						}else{" & vbCrLf)
Response.Write("							ldblReserve = VTFormat(insConvertNumber(0),'', '', '', 6, true);" & vbCrLf)
Response.Write("						}" & vbCrLf)
Response.Write("	//+Monto fijo" & vbCrLf)
Response.Write("					}else{" & vbCrLf)
Response.Write("						if (insConvertNumber(ldblDamages) > insConvertNumber(ldblFrandeda)){" & vbCrLf)
Response.Write("							ldblReserve = VTFormat(insConvertNumber(ldblFrandeda),'', '', '', 6, true);" & vbCrLf)
Response.Write("						}else{" & vbCrLf)
Response.Write("							ldblReserve = VTFormat(insConvertNumber(0),'', '', '', 6, true);" & vbCrLf)
Response.Write("						}" & vbCrLf)
Response.Write("					}" & vbCrLf)
Response.Write("	//+No aplica" & vbCrLf)
Response.Write("				}else{" & vbCrLf)
Response.Write("					ldblReserve = VTFormat(insConvertNumber(ldblDamages),'', '', '', 6, true);" & vbCrLf)
Response.Write("				}" & vbCrLf)
Response.Write("			}" & vbCrLf)
Response.Write("		}else{" & vbCrLf)
Response.Write("//%aplica sobre siniestro" & vbCrLf)
Response.Write("			if (hddsFrancapl.value=='3'){ " & vbCrLf)
Response.Write("		//+Cuando es deducible" & vbCrLf)
Response.Write("				if (cbeFrantype.value==3){" & vbCrLf)
Response.Write("		//+ si es un porcentaje    " & vbCrLf)
Response.Write("		                 	   " & vbCrLf)
Response.Write("				    if (cbeFrantype_aux.value==1){" & vbCrLf)
Response.Write("						//+se consigue el monto del deducible" & vbCrLf)
Response.Write("						ldblDamages2 = insConvertNumber(ldblDamages) * insConvertNumber(ldblFra_amount) / 100;" & vbCrLf)
Response.Write("						" & vbCrLf)
Response.Write("					" & vbCrLf)
Response.Write("						ldblReserve = VTFormat(insConvertNumber(ldblDamages) - ldblDamages2 ,'', '', '', 6, true);" & vbCrLf)
Response.Write("						ldblFranAmouPerc= VTFormat(ldblDamages2,'', '', '', 6, true);" & vbCrLf)
Response.Write("					" & vbCrLf)
Response.Write("		//+Monto fijo" & vbCrLf)
Response.Write("					}else{" & vbCrLf)
Response.Write("						if (insConvertNumber(ldblDamages) < insConvertNumber(ldblFra_amount)){" & vbCrLf)
Response.Write("							ldblReserve = VTFormat(insConvertNumber(0),'', '', '', 6, true);" & vbCrLf)
Response.Write("						}else{" & vbCrLf)
Response.Write("							ldblReserve = VTFormat(insConvertNumber(ldblDamages) - insConvertNumber(ldblFra_amount) ,'', '', '', 6, true);" & vbCrLf)
Response.Write("						}" & vbCrLf)
Response.Write("					}" & vbCrLf)
Response.Write("		//+si es una franquicia" & vbCrLf)
Response.Write("				}else{" & vbCrLf)
Response.Write("" & vbCrLf)
Response.Write("//revisar este proceso" & vbCrLf)
Response.Write("					if (cbeFrantype.value==2){" & vbCrLf)
Response.Write("		//+ si es un porcentaje    " & vbCrLf)
Response.Write("						if (cbeFrantype_aux.value==1){" & vbCrLf)
Response.Write("						//	if (insConvertNumber(ldblDamages) > insConvertNumber(ldblCapital)){" & vbCrLf)
Response.Write("								ldblReserve = VTFormat(insConvertNumber(ldblDamages),'', '', '', 6, true);" & vbCrLf)
Response.Write("						//	}else{" & vbCrLf)
Response.Write("						//		ldblReserve = VTFormat(insConvertNumber(0),'', '', '', 6, true);" & vbCrLf)
Response.Write("						//	}" & vbCrLf)
Response.Write("		//+Monto fijo" & vbCrLf)
Response.Write("						}else{" & vbCrLf)
Response.Write("							if (insConvertNumber(ldblDamages) > insConvertNumber(ldblFra_amount)){" & vbCrLf)
Response.Write("								ldblReserve = VTFormat(insConvertNumber(ldblDamages),'', '', '', 6, true);" & vbCrLf)
Response.Write("							}else{" & vbCrLf)
Response.Write("								ldblReserve = VTFormat(insConvertNumber(0),'', '', '', 6, true);" & vbCrLf)
Response.Write("							}" & vbCrLf)
Response.Write("						}" & vbCrLf)
Response.Write("		//+No aplica" & vbCrLf)
Response.Write("					}else{" & vbCrLf)
Response.Write("						ldblReserve = VTFormat(insConvertNumber(ldblDamages),'', '', '', 6, true);" & vbCrLf)
Response.Write("					}" & vbCrLf)
Response.Write("				}" & vbCrLf)
Response.Write("" & vbCrLf)
Response.Write("			}else{" & vbCrLf)
Response.Write("//No aplica" & vbCrLf)
Response.Write("				ldblReserve = ldblDamages;" & vbCrLf)
Response.Write("			}" & vbCrLf)
Response.Write("		}" & vbCrLf)
Response.Write("" & vbCrLf)
Response.Write("//+Se actualizan los datos de la página." & vbCrLf)
Response.Write("		mdblFrandeda = ldblReserve; " & vbCrLf)
Response.Write("		elements[""tcnDamages""].value = VTFormat(insConvertNumber(ldblDamages),'', '', '', 6, true); " & vbCrLf)
Response.Write("        elements[""tcnFrandeda""].value = ldblFrandeda;" & vbCrLf)
Response.Write("         elements[""hddnFranAmount""].value = ldblFranAmouPerc;" & vbCrLf)
Response.Write("        ldblReserve = insConvertNumber(ldblReserve) - insConvertNumber(lblPayAmount); " & vbCrLf)
Response.Write("        elements[""tcnReserve""].value = VTFormat(ldblReserve,'', '', '', 6, true); " & vbCrLf)
Response.Write("	}" & vbCrLf)
Response.Write("}" & vbCrLf)
Response.Write("//---------------------------------------------------------------------------------" & vbCrLf)
Response.Write("function InsActive()" & vbCrLf)
Response.Write("//---------------------------------------------------------------------------------" & vbCrLf)
Response.Write("{" & vbCrLf)
Response.Write("	" & vbCrLf)
Response.Write("	self.document.forms[0].tcnImport.disabled = ( insConvertNumber(self.document.forms[0].tcnQ_used.value) != 0)" & vbCrLf)
Response.Write("	self.document.forms[0].tcnAmount.disabled = ( insConvertNumber(self.document.forms[0].tcnQ_used.value) == 0)" & vbCrLf)
Response.Write("	" & vbCrLf)
Response.Write("}	       " & vbCrLf)
Response.Write("</" & "SCRIPT>")

	
	Response.Write("<SCRIPT>AssignValues();</" & "Script>")
	
	With Request
		Response.Write(mobjGrid.DoFormUpd(.QueryString("Action"), "ValClaimSeq.aspx", "SI025", .QueryString("nMainAction"), mobjValues.ActionQuery, .QueryString("Index")))
	End With
	
	If Request.QueryString("CalPremium") = "1" Then
		Response.Write("<SCRIPT>var mdblDamages ='0" & Request.Form("tcnDamages") & "'; var mdblFrandeda='0" & Request.Form("tcnFrandeda") & "'; var mdblReserve ='0" & Request.Form("tcnReserve") & "';</" & "Script>")
		
		With mclsCL_Cover
			.sCertype = "2"
			.nBranch = mobjValues.StringToType(CStr(Session("nBranch")), eFunctions.Values.eTypeData.etdDouble)
			.nProduct = mobjValues.StringToType(CStr(Session("nProduct")), eFunctions.Values.eTypeData.etdDouble)
			.nPolicy = mobjValues.StringToType(CStr(Session("nPolicy")), eFunctions.Values.eTypeData.etdDouble)
			.dEffecdate = mobjValues.StringToType(CStr(Session("dOccurdate_l")), eFunctions.Values.eTypeData.etdDate)
			.nModulec = mobjValues.StringToType(Request.Form("nModulec"), eFunctions.Values.eTypeData.etdDouble)
			.nCover = mobjValues.StringToType(Request.Form("tcnCover"), eFunctions.Values.eTypeData.etdDouble)
			.nGroup_insu = mclsCL_Cover.nGroup
			.nClaim = mobjValues.StringToType(CStr(Session("nClaim")), eFunctions.Values.eTypeData.etdDouble)
			.nCase_num = mobjValues.StringToType(Request.Form("tcnCase_num"), eFunctions.Values.eTypeData.etdDouble)
			.nDeman_type = mobjValues.StringToType(Request.Form("tcnDeman_type"), eFunctions.Values.eTypeData.etdDouble)
			.nFixAmount = mclsCL_Cover.nFixAmount
			.nMaxAmount = mclsCL_Cover.nMaxAmount
			.nMinAmount = mclsCL_Cover.nMinAmount
			.nRate = mobjValues.StringToType(Request.Form("nRate"), eFunctions.Values.eTypeData.etdDouble)
			.nCapital = mobjValues.StringToType(Request.Form("tcnCapital"), eFunctions.Values.eTypeData.etdDouble)
			.nDamages = mobjValues.StringToType(Request.Form("tcnDamages"), eFunctions.Values.eTypeData.etdDouble)
			If Request.Form("cbeFrantype") = eRemoteDB.Constants.intNull Then
				.sFrantype = ""
			Else
				.sFrantype = Request.Form("cbeFrantype")
			End If
			.sFrancapl = mclsCL_Cover.sFrancapl
			.nPay_amount = mobjValues.StringToType(Request.Form("tcnPayAmount"), eFunctions.Values.eTypeData.etdDouble)
			.nFra_amount = 0
			.sAutomRep = mclsCL_Cover.sAutomRep
			.sRoureser = mclsCL_Cover.sRoureser
			.nOpt_claityp = mobjValues.StringToType(CStr(Session("nTransaccion")), eFunctions.Values.eTypeData.etdDouble)
			.sRec_fra = "1"
			.sBrancht = Session("sBrancht")
			.sShowInd = "1"
			.nFrandeda = mobjValues.StringToType(Request.Form("tcnFrandeda"), eFunctions.Values.eTypeData.etdDouble)
			.nReserve = mobjValues.StringToType(Request.Form("tcnReserve"), eFunctions.Values.eTypeData.etdDouble)
			.nSel = mobjValues.StringToType(CStr(mclsCL_Cover.nSel), eFunctions.Values.eTypeData.etdDouble)
			.sClient = mclsCL_Cover.sClient
			.sBill_ind = mclsCL_Cover.sBill_ind
			Call .CalReserve()
			Response.Write("<SCRIPT>insUpdValues('" & CStr(.nDamages) & "', '" & CStr(.nFrandeda) & "', '" & CStr(.nReserve) & "');")
			Response.Write("document.forms[0]." & Request.QueryString("Field") & ".focus();</" & "Script>")
			
		End With
	Else
		Response.Write("<SCRIPT>var mdblDamages ='0'; var mdblFrandeda='0'; var mdblReserve='0'</" & "Script>")
	End If
	
	
Response.Write("" & vbCrLf)
Response.Write("	<SCRIPT>" & vbCrLf)
Response.Write("		mblnDo=true;" & vbCrLf)
Response.Write("		InsActive();" & vbCrLf)
Response.Write("	</" & "SCRIPT>")

	
	'	Response.Write "<NOTSCRIPT>insCalReserve();</" & "Script>"
End Sub

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("SI025")
mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.35.15
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "SI025"

'-Se crean las instancias de las variables modulares

mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.35.15
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility
mobjGrid = New eFunctions.Grid
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.35.15
mobjGrid.sSessionID = Session.SessionID
mobjGrid.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjGrid.sCodisplPage = "SI025"
Call mobjGrid.SetWindowParameters(Request.QueryString("sCodispl"), Request.QueryString("sWindowDescript"), Request.QueryString("nWindowTy"))


mclsCL_Cover = New eClaim.Cl_Cover
mcolCL_Covers = New eClaim.CL_Covers

'- Variable en JScript que indica la acción que seleccionó el usuario
Response.Write("<SCRIPT>var mstrTransaction='" & Session("nTransaction") & "'</SCRIPT>")

If Request.QueryString("Type") <> "PopUp" Then
	With Response
		.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction;</SCRIPT>")
	End With
        If Session("nTransaction") = eClaim.Claim_win.eClaimTransac.clngClaimQuery Then
            mobjGrid.ActionQuery = Session("bQuery")
        Else
            mobjGrid.ActionQuery = False
            Session("bQuery") = False
        End If
End If
%>
<HTML>
<HEAD>
<META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">	
<%'UPGRADE_NOTE: Language element 'SCRIPT' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file '/VTimeNet/Scripts/GenFunctions.js' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<%'UPGRADE_NOTE: Language element 'SCRIPT' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file '/VTimeNet/Scripts/valFunctions.js' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/valFunctions.js"></SCRIPT>
<%'UPGRADE_NOTE: Language element 'SCRIPT' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file '/VTimeNet/Scripts/Claim.js' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Claim.js"></SCRIPT>
<%'UPGRADE_NOTE: Language element '#INCLUDE' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file 'vtime/Includes/Constantes.aspx' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<!-- #INCLUDE VIRTUAL="~/VTimeNet/Includes/Constantes.aspx" -->

<%
With Response
	If Request.QueryString("Type") <> "PopUp" Then
		Response.Write(mobjMenu.setZone(2, Request.QueryString("sCodispl"), Request.QueryString("sWindowDescript"), Request.QueryString("nWindowTy")))
	End If
	.Write(mobjValues.StyleSheet() & vbCrLf)
End With
'UPGRADE_NOTE: Object mobjMenu may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjMenu = Nothing
%>      
<SCRIPT>
 //+Variable para el control de versiones
    document.VssVersion="$$Revision: 19 $|$$Date: 14/10/04 12:31 $|$$Author: Nvapla10 $"
</SCRIPT>

</HEAD>
<BODY ONUNLOAD="closeWindows();">
<%=mobjValues.ShowWindowsName(Request.QueryString("sCodispl"), Request.QueryString("sWindowDescript"))%>
<FORM METHOD="post" ID="FORM" NAME="frmSI025" ACTION="valClaimSeq.aspx?Mode=1">
<%
Call insDefineHeader()

If Request.QueryString("Type") <> "PopUp" Then
	Session("nTotal") = Request.QueryString("nTotal")
	Call insPreSI025()
	'+ Se inhabilita el combo de casos, si solo está registrado un caso                
	%>
		<SCRIPT>if (self.document.forms[0].cbeCase.length == 1) self.document.forms[0].cbeCase.disabled = true;</SCRIPT>
<%	
Else
	Call insPreSI025Upd()
End If
'UPGRADE_NOTE: Object mobjValues may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjValues = Nothing
'UPGRADE_NOTE: Object mobjGrid may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjGrid = Nothing
'UPGRADE_NOTE: Object mclsCL_Cover may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mclsCL_Cover = Nothing
'UPGRADE_NOTE: Object mcolCL_Covers may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mcolCL_Covers = Nothing
%>
</FORM>
</BODY>
<%
'If Request.QueryString("nState") <> vbNullString Then
'    Response.Write "<NOTSCRIPT>self.document.forms[0].cbeReservstat.value = " & Request.QueryString("nState") & " ;</SCRIPT>" 
'End if
%>

</HTML>
<SCRIPT>
var mintCase_num 
var mintDeman_type 
var mstrClient 
//------------------------------------------------------------------------------------
function insChangeCase(Field) {
//------------------------------------------------------------------------------------   
   var lstrCase_num = '';
   var lstrDeman_type = '';
   var lstrClient = '';
   var lstrString = '';
   var lstrLocation = '';

                                                 
    lstrString += Field.value
    lstrCase_num = lstrString.substring(0,(lstrString.indexOf("/")))
    lstrDeman_type = lstrString.substr(lstrString.indexOf("/")+1,1)
    lstrClient += lstrString.replace(/.*\//,"")   
    lstrLocation += document.location.href
    lstrLocation = lstrLocation.replace(/&nCase_num.*/,"")
    lstrLocation = lstrLocation + "&nCase_num=" + lstrCase_num + "&nDeman_type=" + lstrDeman_type + "&sClient=" + lstrClient
    lstrString = "nCase_num=" + mintCase_num + "&nDeman_type=" + mintDeman_type + "&sClient=" + mstrClient + '&nOldCurrency=' + self.document.forms[0].tcnOldCurrency.value
    insDefValues('Reser_Total',lstrString,'/VTimeNet/Claim/ClaimSeq');
	mintCase_num=lstrCase_num;
	mintDeman_type=lstrDeman_type;
	mstrClient=lstrClient;
    document.location.href = lstrLocation;
   
}
//------------------------------------------------------------------------------------
function insChangecover(Field) {
//------------------------------------------------------------------------------------   
   var lstrCover = '';
   var lstrDeman_type = '';
   var lstrClient = '';
   var lstrString = '';
   var lstrLocation = '';
     
    lstrString+= Field.value
    lstrLocation = document.location.href
    lstrLocation = lstrLocation.replace(/&nCover.*/,"")
    lstrLocation  = lstrLocation  + "&nCover=" + Field.value; 
    document.location.href = lstrLocation

}
//-------------------------------------------------------------------------------------------
function insChangeValue(Field){
//-------------------------------------------------------------------------------------------
//alert(Field.value);
//alert(self.document.forms[0].tcnlimit.value);
    //if (Field.value > self.document.forms[0].tcnlimit.value)
     //  Field.value = 0;
     //  alert('uno');
    if (Field.checked)
    {
        self.document.forms[0].hddBill_ind.value= 1;
    }
    else
    {
        self.document.forms[0].hddBill_ind.value= 2;    
    }

}
//-------------------------------------------------------------------------------------------
function InsChangeImp(Field){
//-------------------------------------------------------------------------------------------
    var l_nAmount = insConvertNumber(self.document.forms[0].tcnlimit.value)+insConvertNumber(self.document.forms[0].hhnReserve.value);
     l_nAmount = insConvertNumber(self.document.forms[0].tcnImport.value) - l_nAmount;
     if (l_nAmount >=0)
    self.document.forms[0].tcnPay_amount.value = l_nAmount;
}
//-------------------------------------------------------------------------------------------
function InsChangeAmo(Field){
//-------------------------------------------------------------------------------------------
    var l_nAmount = insConvertNumber(self.document.forms[0].tcnQ_used.value)+insConvertNumber(self.document.forms[0].hhnReserve.value);
    l_nAmount = insConvertNumber(self.document.forms[0].tcnAmount.value) - l_nAmount; 
     if (l_nAmount >=0)
         self.document.forms[0].tcnPay_amount.value = l_nAmount;
}
//-------------------------------------------------------------------------------------------
function InsChangeRes(){
//-------------------------------------------------------------------------------------------
var l_nAmountRes = insConvertNumber(self.document.forms[0].tcnReserve.value);
var l_nAmount_Limit = insConvertNumber(self.document.forms[0].tcnlimit.value);
var l_nAmount_Qused = insConvertNumber(self.document.forms[0].tcnQ_used.value);
var l_nAMount_ResO  = insConvertNumber(self.document.forms[0].hhnReserve.value);

    if (l_nAmount_Limit!=0){
        if (((l_nAmount_Limit+l_nAMount_ResO) - l_nAmountRes) < 0){
            alert('Err: 4355 La Reserva no puede ser superior al limite');
            self.document.forms[0].tcnReserve.value =0;
        }
    }
    else
        if (l_nAmount_Qused !=0)	
        if (((l_nAmount_Qused+l_nAMount_ResO) - l_nAmountRes) < 0){
            alert('Err: 4355 La Reserva no puede ser superior al limite');
            self.document.forms[0].tcnReserve.value =0;
        }
}
//-------------------------------------------------------------------------------------------
function insCheckSelClick(Field){
//-------------------------------------------------------------------------------------------
//+ Se levanta la ventana PopUp para actualizar el registro

    if(Field.checked){
        EditRecord(Field.value,nMainAction)
        Field.checked = !Field.checked
    }
    else
    {
    
    
           
        if (marrArray[Field.value].tcnPayAmount > 0)
        {
			alert('No puede eliminar cobertura con pagos realizados')
			Field.checked = !Field.checked
        }
        else
        {
		    insDefValues('CoverDel','sClient='+ marrArray[Field.value].tctClient + 
		                            '&nCurrency=' +  marrArray[Field.value].cbeCurrency +
									'&nCurrency_o=' + marrArray[Field.value].nCurrency_o + 
									'&nDeman_type=' + marrArray[Field.value].tcnDeman_type +
									'&nCase_num=' + marrArray[Field.value].tcnCase_num +
									'&nExchange=' + marrArray[Field.value].tcnExchange +
									'&nDamages=0&nReserve=0'+ 
									'&nPayAmount=' + marrArray[Field.value].tcnPayAmount +
									'&nFra_amount=' + marrArray[Field.value].tcnFra_amount +
									'&nFrandeda=' + marrArray[Field.value].tcnFrandeda +
									'&nDamProf=' + marrArray[Field.value].tcnDamProf +
									'&nBranch_est=' + marrArray[Field.value].tcnBranch_est +
									'&nBranch_rei=' + marrArray[Field.value].tcnBranch_rei +
									'&nBranch_led=' + marrArray[Field.value].tcnBranch_led +
									'&nModulec=' + marrArray[Field.value].nModulec +
									'&nCover=' + marrArray[Field.value].nCover +
									'&nGroup=' + marrArray[Field.value].tcnGroup +
									'&nReservstat=' + marrArray[Field.value].cbeReservstat +
									'&nFrantype=' + marrArray[Field.value].cbeFrantype +
									'&sAutomRep=' + marrArray[Field.value].sAutomRep +
									'&nReserveAnt=' + marrArray[Field.value].tcnReserveAnt +
									'&nBill_ind=' + marrArray[Field.value].hddBill_ind,'/VTimeNet/Claim/ClaimSeq')		
        }
    }
}
</SCRIPT>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 11.35.15
Call mobjNetFrameWork.FinishPage("SI025")
'UPGRADE_NOTE: Object mobjNetFrameWork may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




