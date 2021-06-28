<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eProduct" %>
<%@ Import namespace="ePolicy" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objetos para el manejo del grid de la página
Dim mobjGrid As eFunctions.Grid
Dim mobjAnnual As eFunctions.Grid
Dim mobjProrrated As eFunctions.Grid
Dim mintPayfreq As Integer
Dim mblnRateProy As Boolean

Dim mclslife As ePolicy.Life
Dim lcolTab_intproy As ePolicy.Tab_intproys
Dim lclsTab_intproy As ePolicy.Tab_intproy
Dim lintIndex2 As Integer

'- Objeto para el manejo del menú
Dim mobjMenu As eFunctions.Menues

'- Objeto para validar si el primer aporte es modificable
Dim mclsproduct As eProduct.Product
Dim mclscertificat As ePolicy.Certificat
Dim mclsobject As Object

Dim mintError As Integer

Dim lintPayiniti As Double
Dim lintnType_rateproy As Object

Dim mclsnPayiniti As Boolean
Dim mstrApv As String


'% InsInitial: Crea los campos de la parte puntual de la página
'--------------------------------------------------------------------------------------------
Private Sub InsInitial()
	'--------------------------------------------------------------------------------------------
	Dim nRate_proy As Object
	mclslife = New ePolicy.Life
	Dim mclsErrors As eFunctions.Errors
	With mclslife
		.InsPreVI1410("", CStr(Session("sCertype")), CInt(Session("nBranch")), CInt(Session("nProduct")), CDbl(Session("nPolicy")), CDbl(Session("nCertif")), CDate(Session("dEffecdate")), CInt(Session("nUsercode")), CInt(Session("nTransaction")), mobjValues.StringToType(Request.QueryString.Item("nCurrency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nPremiumbas"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nPremimin"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nVpprdeal"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nPremfreq"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nPremdeal"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nAmountcontr"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nIntwarr"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nRatepayf"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nInsurtime"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nVpi"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("dEffecdate_to"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.QueryString.Item("dBirthdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.QueryString.Item("nOption"), eFunctions.Values.eTypeData.etdDouble), Request.QueryString.Item("sOption"), Request.QueryString.Item("sPayfreq"))
		
		mintError = .nError
		If mintError <> eRemoteDB.Constants.intNull Then
			mclsErrors = New eFunctions.Errors
			Response.Write(mclsErrors.ErrorMessage("VI1410", mintError,  ,  ,  , True))
			mclsErrors = Nothing
		End If
		Response.Write(mobjValues.HiddenControl("hddnVPprdeal", CStr(.nVpprdeal)))
		Response.Write(mobjValues.HiddenControl("hddnPremimin", CStr(.nPremmin)))
		Response.Write(mobjValues.HiddenControl("hddnPremdep", CStr(.nAmountcontr)))
		Response.Write(mobjValues.HiddenControl("hddnCurrency", CStr(.nCurrency)))
		Response.Write(mobjValues.HiddenControl("hddnIntwarr", CStr(.nIntwarr)))
		Response.Write(mobjValues.HiddenControl("hddnRatepayf", CStr(.nRatepayf)))
		Response.Write(mobjValues.HiddenControl("hddnInsurtime", CStr(.nInsur_time)))
		Response.Write(mobjValues.HiddenControl("hddnPremfreq", CStr(.nPremdeal)))
		Response.Write(mobjValues.HiddenControl("hddPremdeal_anu", CStr(.nPremdeal_anu)))
		Response.Write(mobjValues.HiddenControl("hddBirthdate", mobjValues.TypeToString(.dBirthdate, eFunctions.Values.eTypeData.etdDate)))
		Response.Write(mobjValues.HiddenControl("hddEffecdate_to", mobjValues.TypeToString(.dEffecdate_to, eFunctions.Values.eTypeData.etdDate)))
		Response.Write(mobjValues.HiddenControl("hddVp_initial", mobjValues.TypeToString(.nVpi, eFunctions.Values.eTypeData.etdDouble)))
		Response.Write(mobjValues.HiddenControl("hddnYear_end", CStr(eRemoteDB.Constants.intNull)))
		Response.Write(mobjValues.HiddenControl("hddsPremdeal_Chan", ""))
		Response.Write(mobjValues.HiddenControl("hddsProcessed", ""))
		Response.Write(mobjValues.HiddenControl("hddnPremdeal_old", CStr(.nPremdeal_anu)))
		Response.Write(mobjValues.HiddenControl("hddnPremiumbas", CStr(.nPremiumbas)))
		Response.Write(mobjValues.HiddenControl("hddnOption", CStr(.nOption)))
		Response.Write(mobjValues.HiddenControl("hddsOption", .sOption))
		Response.Write(mobjValues.HiddenControl("hddsPayfreq", .sPayfreq))
		Response.Write(mobjValues.HiddenControl("tcnIntwarr2", CStr(.nIntwarrVar)))
		Response.Write(mobjValues.HiddenControl("tcnIntwarr3", CStr(.nIntwarrexc)))
		Response.Write(mobjValues.HiddenControl("tcnIntwarr4", CStr(.nIntwarrExcVar)))
		Response.Write(mobjValues.HiddenControl("hddnDivide", CStr(.nDivide)))
		Response.Write(mobjValues.HiddenControl("hddnMultiply", CStr(.nMultiply)))
		
		
Response.Write("" & vbCrLf)
Response.Write("<TABLE WIDTH=""100%"">" & vbCrLf)
Response.Write("    <TR>" & vbCrLf)
Response.Write("        <TD><LABEL ID=0>" & GetLocalResourceObject("cbeCurrencyCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("        <TD>")


Response.Write(mobjValues.PossiblesValues("cbeCurrency", "Table11", eFunctions.Values.eValuesType.clngComboType, CStr(.nCurrency),  , True,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeCurrencyToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("        <TD>&nbsp;</TD>" & vbCrLf)
Response.Write("         <TD><LABEL ID=0>" & GetLocalResourceObject("tcnPayfreqCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("        <TD>")


Response.Write(mobjValues.TextControl("tcnPayfreq", 30, .sPayfreq,  , GetLocalResourceObject("tcnPayfreqToolTip"), True,  ,  ,  , True))


Response.Write("</TD>        " & vbCrLf)
Response.Write("    </TR>" & vbCrLf)
Response.Write("    <TR>" & vbCrLf)
Response.Write("    <TD COLSPAN=2 CLASS=""HighLighted""><LABEL ID=0><A NAME=""Varios"">" & GetLocalResourceObject("AnchorVariosCaption") & "</A></LABEL></TD>" & vbCrLf)
Response.Write("    <TD></TD>" & vbCrLf)
Response.Write("    <TD COLSPAN=2 CLASS=""HighLighted""><LABEL ID=0><A NAME=""Varios"">" & GetLocalResourceObject("AnchorVarios2Caption") & "</A></LABEL></TD>" & vbCrLf)
Response.Write("    </TR>" & vbCrLf)
Response.Write("	<TR>" & vbCrLf)
Response.Write("			<TD COLSPAN=""2"" CLASS=""Horline""></TD>" & vbCrLf)
Response.Write("            <TD></TD> " & vbCrLf)
Response.Write("			<TD COLSPAN=""2"" CLASS=""Horline""></TD>" & vbCrLf)
Response.Write("    </TR>" & vbCrLf)
Response.Write("    <TR>" & vbCrLf)
Response.Write("    <TD COLSPAN=2>")

		insDrawAnnualValues()
Response.Write("</TD>" & vbCrLf)
Response.Write("    <TD></TD>" & vbCrLf)
Response.Write("    <TD COLSPAN=2>")

		insDrawProrratedValues()
Response.Write("</TD>" & vbCrLf)
Response.Write("	</TR>" & vbCrLf)
Response.Write("" & vbCrLf)
Response.Write("" & vbCrLf)
Response.Write("    <TR STYLE='display:none'>" & vbCrLf)
Response.Write("        <TD><LABEL ID=0>" & GetLocalResourceObject("tcnPremAnuCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("        <TD>")


Response.Write(mobjValues.NumericControl("tcnPremAnu", 18, CStr(.nPremiumbas),  , GetLocalResourceObject("tcnPremAnuToolTip"), True, 6, True))


Response.Write("</TD>" & vbCrLf)
Response.Write("        <TD>&nbsp;</TD>" & vbCrLf)
Response.Write("        <TD><LABEL ID=0>" & GetLocalResourceObject("tcnPremiminCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("        <TD>")


Response.Write(mobjValues.NumericControl("tcnPremimin", 18, CStr(.nPremmin),  , GetLocalResourceObject("tcnPremiminToolTip"), True, 6, True))


Response.Write("</TD>" & vbCrLf)
Response.Write("    </TR>" & vbCrLf)
Response.Write("    " & vbCrLf)
Response.Write("    <TR>" & vbCrLf)
Response.Write("        <TD><LABEL ID=0>" & GetLocalResourceObject("tcnPremdealCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("        <TD>")


Response.Write(mobjValues.NumericControl("tcnPremdeal", 18, CStr(.nPremdeal_anu),  , GetLocalResourceObject("tcnPremdealToolTip"), True, 6,  ,  ,  , "InsDisPremdel(1, this.value, '')", True))


Response.Write("</TD>" & vbCrLf)
Response.Write("        <TD>&nbsp;</TD>" & vbCrLf)
Response.Write("        <TD><LABEL ID=0>" & GetLocalResourceObject("tcnPremfreqCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("        <TD>")


Response.Write(mobjValues.NumericControl("tcnPremfreq", 18, CStr(.nPremdeal),  , GetLocalResourceObject("tcnPremfreqToolTip"), True, 6,  ,  ,  ,  , True))


Response.Write("</TD> " & vbCrLf)
Response.Write("    </TR>" & vbCrLf)
Response.Write("" & vbCrLf)
Response.Write("	")

		If lintPayiniti = 0 Then
Response.Write("" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("		    <TD><LABEL ID=0>" & GetLocalResourceObject("tcnVPprdealCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("		    <TD>")


Response.Write(mobjValues.NumericControl("tcnVPprdeal", 18, CStr(.nVpprdeal),  , GetLocalResourceObject("tcnVPprdealToolTip"), True, 6, True))


Response.Write("</TD>" & vbCrLf)
Response.Write("		    <TD>&nbsp;</TD>" & vbCrLf)
Response.Write("		    <TD><LABEL ID=0>" & GetLocalResourceObject("tcnPremdepCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("		    <TD>")


Response.Write(mobjValues.NumericControl("tcnPremdep", 18, CStr(.nAmountcontr),  , GetLocalResourceObject("tcnPremdepToolTip"), True, 6, True))


Response.Write("</TD>" & vbCrLf)
Response.Write("" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("		")

			Response.Write(mobjValues.HiddenControl("tcnPayiniti", lintPayiniti))
Response.Write("" & vbCrLf)
Response.Write("    ")

		Else
Response.Write("" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("		    <TD><LABEL ID=0>" & GetLocalResourceObject("tcnPayinitiCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("		    <TD>")


Response.Write(mobjValues.NumericControl("tcnPayiniti", 18, CStr(.nInitialPayment),  , GetLocalResourceObject("tcnPayinitiToolTip"), True, 6,  ,  ,  ,  , True))


Response.Write("</TD>" & vbCrLf)
Response.Write("		    " & vbCrLf)
Response.Write("		    <TD>&nbsp;</TD>" & vbCrLf)
Response.Write("		    <TD><LABEL ID=0>" & GetLocalResourceObject("tcnVPprdealCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("		    <TD>")


Response.Write(mobjValues.NumericControl("tcnVPprdeal", 18, CStr(.nVpprdeal),  , GetLocalResourceObject("tcnVPprdealToolTip"), True, 6, True))


Response.Write("</TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("		    <TD><LABEL ID=0>" & GetLocalResourceObject("tcnPremdepCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("		    <TD>")


Response.Write(mobjValues.NumericControl("tcnPremdep", 18, CStr(.nAmountcontr),  , GetLocalResourceObject("tcnPremdepToolTip"), True, 6, True))


Response.Write("</TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("            " & vbCrLf)
Response.Write("    ")

		End If
Response.Write("" & vbCrLf)
Response.Write("	<TR>" & vbCrLf)
Response.Write("	    <TD><LABEL>" & GetLocalResourceObject("tcnIntwarrCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("        ")

            nRate_proy = mclslife.nIntwarr
            'Si calculo de rentbilidad es CUI(2)
            If CDbl(Session("nType_rateproy")) = 2 Then
                'Si nIntwarr < min SVS toma min SVS
                If CDbl(Session("nSvsMin")) <> 0 And mclslife.nIntwarr < CDbl(Session("nSvsMin")) Then
                    nRate_proy = Session("nSvsMin")
                    mclslife.nIntwarr = nRate_proy
                    Response.Write("<SCRIPT>alert('Rentabilidad calculada (" & mclslife.nIntwarr & "%) es menor al permitido por la SVS, se proyectará con el mínimo permitido (" & nRate_proy & "%)');</" & "Script>")
                End If
                'Si es nIntwarr > max SVS toma max SVS
                If CDbl(Session("nSvsMax")) <> 0 And mclslife.nIntwarr > CDbl(Session("nSvsMax")) Then
                    nRate_proy = Session("nSvsMax")
                    mclslife.nIntwarr = nRate_proy
                    Response.Write("<SCRIPT>alert('Rentabilidad calculada (" & mclslife.nIntwarr & "%) es mayor al permitido por la SVS, se proyectará con el maximo permitido (" & nRate_proy & "%)');</" & "Script>")
                End If
            End If
            'Calculo rentabilidad de producto es fijo            
            If CDbl(Session("nType_rateproy")) = 5 Then
                If CDbl(Session("nIntProy")) <> 0 And mclslife.nIntwarr > CDbl(Session("nIntProy")) Then
                    nRate_proy = Session("nIntProy")
                    mclslife.nIntwarr = nRate_proy
                End If
                If  CDbl(Session("nIntProy")) <> 0 And mclslife.nIntwarr <  CDbl(Session("nIntProy")) Then
                    mclslife.nIntwarr = nRate_proy
                End If
            End If

		
Response.Write("" & vbCrLf)
Response.Write("		<TD>")


Response.Write(mobjValues.NumericControl("tcnIntwarr", 10, CStr(mclslife.nIntwarr),  , GetLocalResourceObject("tcnIntwarrToolTip"), True, 2,  ,  ,  , "InsChangeInt_Warr(this.value)", mblnRateProy,  ,  , True))


Response.Write("</TD></TD>" & vbCrLf)
Response.Write("		<TD>&nbsp;</TD>" & vbCrLf)
Response.Write("        <TD ><LABEL ID=0>" & GetLocalResourceObject("btnCalcCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("        <TD>")


Response.Write(mobjValues.AnimatedButtonControl("btnCalc", "..\..\images\batchStat06.png", GetLocalResourceObject("btnCalcToolTip"),  , "InsShowIlustration(" & mintError & ");"))


Response.Write("<TD>" & vbCrLf)
Response.Write("     </TR>" & vbCrLf)
Response.Write("        " & vbCrLf)
Response.Write("</TABLE>" & vbCrLf)
Response.Write("    <TABLE WIDTH=""100%"">				" & vbCrLf)
Response.Write("		" & vbCrLf)
Response.Write("    	    <TD COLSPAN=""6"" CLASS=""HighLighted""><LABEL ID=0><A NAME=""Varios"">" & GetLocalResourceObject("AnchorVarios3Caption") & "</A></LABEL></TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD COLSPAN=""6"" CLASS=""Horline""></TD>" & vbCrLf)
Response.Write("			 			" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("    </TABLE>" & vbCrLf)
Response.Write("<BR>")

		
	End With
End Sub

'% InsDefineProrratedHeader: se definen las propiedades del grid
'--------------------------------------------------------------------------------------------
Private Sub InsDefineProrratedHeader()
	'--------------------------------------------------------------------------------------------
	mobjProrrated = New eFunctions.Grid
	
	mobjGrid.sCodisplPage = Request.QueryString.Item("sCodispl")
	Call mobjProrrated.SetWindowParameters(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), Request.QueryString.Item("nWindowTy"))
	mobjProrrated.bOnlyForQuery = True
	'+ Se definen las columnas del grid
	With mobjProrrated.Columns
		Call .AddTextColumn(0, GetLocalResourceObject("tctConceptColumnCaption"), "tctConcept", 30, vbNullString,  , GetLocalResourceObject("tctConceptColumnToolTip"),  ,  ,  , True)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnPremiumColumnCaption"), "tcnPremium", 24, vbNullString,  , GetLocalResourceObject("tcnPremiumColumnToolTip"),  , 6)
		
	End With
	mobjProrrated.Columns("Sel").GridVisible = False
	
	'+ Se definen las propiedades generales del grid
	With mobjProrrated
		.Codispl = "VI1410"
		.ActionQuery = True
	End With
End Sub

'% InsDefineAnnualHeader: se definen las propiedades del grid
'--------------------------------------------------------------------------------------------
Private Sub InsDefineAnnualHeader()
	'--------------------------------------------------------------------------------------------
	mobjAnnual = New eFunctions.Grid
	
	mobjAnnual.sCodisplPage = Request.QueryString.Item("sCodispl")
	Call mobjAnnual.SetWindowParameters(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), Request.QueryString.Item("nWindowTy"))
	mobjAnnual.bOnlyForQuery = True
	
	'+ Se definen las columnas del grid
	With mobjAnnual.Columns
		Call .AddTextColumn(0, GetLocalResourceObject("tctConceptColumnCaption"), "tctConcept", 30, vbNullString,  , GetLocalResourceObject("tctConceptColumnToolTip"),  ,  ,  , True)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnPremiumColumnCaption"), "tcnPremium", 24, vbNullString,  , GetLocalResourceObject("tcnPremiumColumnToolTip"),  , 6)
	End With
	mobjAnnual.Columns("Sel").GridVisible = False
	
	'+ Se definen las propiedades generales del grid
	With mobjAnnual
		.Codispl = "VI1410"
		.ActionQuery = True
	End With
End Sub

'% InsDefineHeader: se definen las propiedades del grid
'--------------------------------------------------------------------------------------------
Private Sub InsDefineHeader()
	'--------------------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	
	mobjGrid.sCodisplPage = Request.QueryString.Item("sCodispl")
	Call mobjGrid.SetWindowParameters(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), Request.QueryString.Item("nWindowTy"))
	
	'+ Se definen las columnas del grid
	With mobjGrid.Columns
		.AddNumericColumn(0, GetLocalResourceObject("tcnYear_iniColumnCaption"), "tcnYear_ini", 5, vbNullString,  , GetLocalResourceObject("tcnYear_iniColumnToolTip"),  ,  ,  ,  ,  , Request.QueryString.Item("Action") = "Update")
		.AddNumericColumn(0, GetLocalResourceObject("tcnYear_endColumnCaption"), "tcnYear_end", 5, vbNullString,  , GetLocalResourceObject("tcnYear_endColumnToolTip"),  ,  ,  ,  ,  , Request.QueryString.Item("Action") = "Update")
		.AddNumericColumn(0, GetLocalResourceObject("tcnAmountdepColumnCaption"), "tcnAmountdep", 18, vbNullString,  , GetLocalResourceObject("tcnAmountdepColumnToolTip"), True, 6)
		If Request.QueryString.Item("Type") <> "PopUp" Then
			.AddAnimatedColumn(0, GetLocalResourceObject("sLinkColumnCaption"), "sLink", "/VTimeNet/Images/clfolder.png", GetLocalResourceObject("sLinkColumnCaption"))
		End If
		
	End With
	
	'+ Se definen las propiedades generales del grid
	With mobjGrid
		.Codispl = "VI1410"
		.ActionQuery = mobjValues.ActionQuery
		.Columns("tcnYear_ini").EditRecord = True
		'.Columns("tcnYear_end").EditRecord = True
		.Height = 200
		.Width = 380
		.nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
		.Columns("Sel").GridVisible = Not .ActionQuery
		.sDelRecordParam = "nYear_ini=' + marrArray[lintIndex].tcnYear_ini +  '"
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
End Sub

'% InsPreVI1410: se realiza el manejo del grid
'--------------------------------------------------------------------------------------------
Private Sub InsPreVI1410()
	'--------------------------------------------------------------------------------------------
	
Response.Write("" & vbCrLf)
Response.Write("<SCRIPT>" & vbCrLf)
Response.Write("//%InsDisPremdel: Valida la cantidad de registros en per_deposit para habilitar o no la prima" & vbCrLf)
Response.Write("//                convenida anual" & vbCrLf)
Response.Write("//-------------------------------------------------------------------------------------------" & vbCrLf)
Response.Write("function InsDisPremdel(nCount, nPremdeal, nYear_end){" & vbCrLf)
Response.Write("//-------------------------------------------------------------------------------------------" & vbCrLf)
Response.Write("    var sThouSep = '")


Response.Write(mobjValues.msUserThousandSeparator)


Response.Write("';" & vbCrLf)
Response.Write("    var sDecSep  = '")


Response.Write(mobjValues.msUserDecimalSeparator)


Response.Write("';" & vbCrLf)
Response.Write("	if (nPremdeal == ''){" & vbCrLf)
Response.Write("		nPremdeal = 0;" & vbCrLf)
Response.Write("	}" & vbCrLf)
Response.Write("	if(typeof(self.document.forms[0].tcnPremdeal)!='undefined'){" & vbCrLf)
Response.Write("" & vbCrLf)
Response.Write("		with (self.document.forms[0]){" & vbCrLf)
Response.Write("			tcnPremdeal.value = VTFormatT(nPremdeal, '', '', '', 6);" & vbCrLf)
Response.Write("			InsChangePremdeal(tcnPremdeal.value);" & vbCrLf)
Response.Write("			if (nYear_end!='')" & vbCrLf)
Response.Write("				hddnYear_end.value = nYear_end;" & vbCrLf)
Response.Write("		}" & vbCrLf)
Response.Write("	}" & vbCrLf)
Response.Write("}" & vbCrLf)
Response.Write("</" & "SCRIPT>")

	
	'- Objetos para el manejo de los datos repetitivos de la página
	Dim lcolPer_deposit As ePolicy.Per_deposits
	Dim lclsPer_deposit As ePolicy.Per_deposit
	Dim ldblPrem_deal As Double
	Dim llngYear_end As Integer
	Dim lintIndex As Short
	
	
	mobjGrid.AddButton = Not mintError <> eRemoteDB.Constants.intNull
	
	lcolPer_deposit = New ePolicy.Per_deposits
	If lcolPer_deposit.Find(CStr(Session("sCertype")), CInt(Session("nBranch")), CInt(Session("nProduct")), CDbl(Session("nPolicy")), CDbl(Session("nCertif")), CDate(Session("dEffecdate"))) Then
		lintIndex = 0
		
		For	Each lclsPer_deposit In lcolPer_deposit
			lintIndex = lintIndex + 1
			With mobjGrid
				.Columns("Sel").Disabled = False
				If mintPayfreq = 6 And mstrApv = "1" Then
					.Columns("Sel").Disabled = True
				Else
					.Columns("Sel").Disabled = False
				End If
				'				if (lintPayiniti > 1 And lclsPer_deposit.nYear_ini = 1 and lclsPer_deposit.nYear_end = 1) Or '				   (mclslife.sApv = "1" And  lclsPer_deposit.nYear_ini = 1) Then
				'						.Columns("Sel").Disabled=true
				'				end if
				.Columns("tcnYear_ini").DefValue = CStr(lclsPer_deposit.nYear_ini)
				.Columns("tcnYear_ini").EditRecord = Not (mclslife.sApv = "1" And lclsPer_deposit.nYear_ini = 1)
				If lintIndex = 1 Then
					ldblPrem_deal = ldblPrem_deal + lclsPer_deposit.nAmountdep
				End If
				llngYear_end = lclsPer_deposit.nYear_end
				.Columns("tcnYear_end").DefValue = CStr(llngYear_end)
				.Columns("tcnAmountdep").DefValue = CStr(lclsPer_deposit.nAmountdep)
				.Columns("sLink").HRefScript = "ShowMonth(" & lintIndex - 1 & ");"
				.Columns("sLink").Disabled = False
				If lclsPer_deposit.nYear_ini = lclsPer_deposit.nYear_end Then
					.Columns("sLink").HRefScript = "ShowMonth(" & lintIndex - 1 & "," & lclsPer_deposit.nYear_ini & ");"
				Else
					.Columns("sLink").HRefScript = "ShowMonth(" & lintIndex - 1 & "," & lclsPer_deposit.nYear_ini & ");"
				End If
				Response.Write(.DoRow)
				
			End With
		Next lclsPer_deposit
		'+Si hay más de un registro en Per_deposit se deshabilita el campo prima convenida anual}
		If Not mobjValues.ActionQuery Then
			If lintIndex > 0 Then
				ldblPrem_deal = ldblPrem_deal
			End If
			Response.Write("<SCRIPT>InsDisPremdel(" & lcolPer_deposit.Count & ",'" & ldblPrem_deal & "','" & llngYear_end & "');</" & "Script>")
		End If
	Else
		Response.Write("<SCRIPT>InsChangePremdeal(document.forms[0].tcnPremdeal.value);</" & "Script>")
	End If
	
	Response.Write(mobjGrid.closeTable())
	lcolPer_deposit = Nothing
	lclsPer_deposit = Nothing
End Sub

'% insDrawAnnualvalues: Se realiza el manejo de la ventana PopUp asociada al grid
'--------------------------------------------------------------------------------------------
Private Sub insDrawAnnualValues()
	'--------------------------------------------------------------------------------------------
	Dim lintIndex As Integer
        Dim ldblTotal As Double
        Dim ldblExceso As Double
        ldblTotal = 0
        ldblExceso = 0
	If Not mclslife.oBillingItems Is Nothing Then
		For lintIndex = 1 To mclslife.oBillingItems.Count
			
			With mobjAnnual
                    If mclslife.oBillingItems.Item(lintIndex).sTypDetai = "8" Then
                        ldblExceso = mclslife.oBillingItems.Item(lintIndex).nAnnualPremium
                    Else
                        ldblTotal = ldblTotal + mclslife.oBillingItems.Item(lintIndex).nAnnualPremium
                        .Columns("tctConcept").DefValue = mclslife.oBillingItems.Item(lintIndex).sConcept
                        .Columns("tcnPremium").DefValue = CStr(mclslife.oBillingItems.Item(lintIndex).nAnnualPremium)
                        Response.Write(.DoRow)
                    End If
                End With
		Next 
	End If
	mobjAnnual.Columns("tctConcept").DefValue = "Total prima mínima"
        mobjAnnual.Columns("tcnPremium").DefValue = CStr(ldblTotal)
        Response.Write(mobjAnnual.DoRow())
        If ldblExceso > 0 Then
            mobjAnnual.Columns("tctConcept").DefValue = "Prima en exceso"
            mobjAnnual.Columns("tcnPremium").DefValue = CStr(ldblExceso)
            Response.Write(mobjAnnual.DoRow())
        End If
	
        Response.Write(mobjAnnual.closeTable())
        mobjAnnual = Nothing
	
    End Sub


'% insDrawAnnualvalues: Se realiza el manejo de la ventana PopUp asociada al grid
'--------------------------------------------------------------------------------------------
Private Sub insDrawProrratedValues()
	'--------------------------------------------------------------------------------------------
	Dim lintIndex As Integer
	Dim ldblTotal As Double
        Dim ldblExceso As Double
        ldblTotal = 0
        ldblExceso = 0

	If Not mclslife.oBillingItems Is Nothing Then
		For lintIndex = 1 To mclslife.oBillingItems.Count
			
			With mobjProrrated
                    If mclslife.oBillingItems.Item(lintIndex).sTypDetai = "8" Then
                        ldblExceso = mclslife.oBillingItems.Item(lintIndex).nProrratedPremium
                    Else
                        ldblTotal = ldblTotal + mclslife.oBillingItems.Item(lintIndex).nProrratedPremium
                        .Columns("tctConcept").DefValue = mclslife.oBillingItems.Item(lintIndex).sConcept
                        .Columns("tcnPremium").DefValue = CStr(mclslife.oBillingItems.Item(lintIndex).nProrratedPremium)
                        Response.Write(.DoRow)
                    End If
			End With
		Next 
	End If
	mobjProrrated.Columns("tctConcept").DefValue = "Total prima mínima"
	mobjProrrated.Columns("tcnPremium").DefValue = CStr(ldblTotal)
	Response.Write(mobjProrrated.DoRow())
        If ldblExceso > 0 Then
            mobjProrrated.Columns("tctConcept").DefValue = "Prima en exceso"
            mobjProrrated.Columns("tcnPremium").DefValue = CStr(ldblExceso)
            Response.Write(mobjProrrated.DoRow())
        End If
	
	Response.Write(mobjProrrated.closeTable())
	mobjProrrated = Nothing
	
End Sub


'% InsPreVI1410Upd: Se realiza el manejo de la ventana PopUp asociada al grid
'--------------------------------------------------------------------------------------------
Private Sub InsPreVI1410Upd()
	'--------------------------------------------------------------------------------------------
	
Response.Write("" & vbCrLf)
Response.Write("<SCRIPT>" & vbCrLf)
Response.Write("//% UpdateFields: actualiza los campos ocultos con los campos puntuales de la BC001J" & vbCrLf)
Response.Write("//-------------------------------------------------------------------------------------------" & vbCrLf)
Response.Write("function UpdateFields(){" & vbCrLf)
Response.Write("//-------------------------------------------------------------------------------------------" & vbCrLf)
Response.Write("    with(self.document.forms[0]){" & vbCrLf)
Response.Write("        hddnPremdeal.value = top.opener.document.forms[0].tcnPremdeal.value;" & vbCrLf)
Response.Write("        hddnVPprdeal.value = top.opener.document.forms[0].hddnVPprdeal.value;" & vbCrLf)
Response.Write("        hddnPremimin.value = top.opener.document.forms[0].hddnPremimin.value;" & vbCrLf)
Response.Write("        hddnCurrency.value = top.opener.document.forms[0].hddnCurrency.value;" & vbCrLf)
Response.Write("        hddnPremdep.value = top.opener.document.forms[0].hddnPremdep.value;" & vbCrLf)
Response.Write("        hddnIntwarr.value = top.opener.document.forms[0].hddnIntwarr.value;" & vbCrLf)
Response.Write("        hddnRatepayf.value = top.opener.document.forms[0].hddnRatepayf.value;" & vbCrLf)
Response.Write("        hddPremdeal_anu.value = top.opener.document.forms[0].hddPremdeal_anu.value;" & vbCrLf)
Response.Write("        hddnPremfreq.value = top.opener.document.forms[0].hddnPremfreq.value;" & vbCrLf)
Response.Write("        hddBirthdate.value = top.opener.document.forms[0].hddBirthdate.value;" & vbCrLf)
Response.Write("		hddEffecdate_to.value = top.opener.document.forms[0].hddEffecdate_to.value;" & vbCrLf)
Response.Write("		hddVp_initial.value = top.opener.document.forms[0].hddVp_initial.value;" & vbCrLf)
Response.Write("		hddsPremdeal_Chan.value = top.opener.document.forms[0].hddsPremdeal_Chan.value;" & vbCrLf)
Response.Write("		hddsProcessed.value = top.opener.document.forms[0].hddsProcessed.value;" & vbCrLf)
Response.Write("		hddnPremdeal_old.value = top.opener.document.forms[0].hddnPremdeal_old.value;" & vbCrLf)
Response.Write("		hddnPremiumbas.value = top.opener.document.forms[0].hddnPremiumbas.value;" & vbCrLf)
Response.Write("		hddnOption.value = top.opener.document.forms[0].hddnOption.value;" & vbCrLf)
Response.Write("		hddsOption.value = top.opener.document.forms[0].hddsOption.value;" & vbCrLf)
Response.Write("		hddsPayfreq.value = top.opener.document.forms[0].hddsPayfreq.value;" & vbCrLf)
Response.Write("    }													 " & vbCrLf)
Response.Write("}" & vbCrLf)
Response.Write("</" & "SCRIPT>")

	
	Dim lclsPer_deposit As ePolicy.Per_deposit
	
	lclsPer_deposit = New ePolicy.Per_deposit
	
	With Request
		If .QueryString.Item("Action") = "Del" Then
			Response.Write(mobjValues.ConfirmDelete())
			If lclsPer_deposit.InsPostVA595Upd(.QueryString.Item("Action"), Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), mobjValues.StringToType(.QueryString.Item("nYear_ini"), eFunctions.Values.eTypeData.etdDouble), Session("dEffecdate"), 0, 0, Session("dNulldate"), Session("nUsercode"), Session("nTransaction"), "VI1410") Then
			End If
		End If
		Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "ValPolicySeq.aspx", "VI1410", .QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(.QueryString.Item("Index"))))
	End With
	With Response
		.Write(mobjValues.HiddenControl("hddnVPprdeal", vbNullString))
		.Write(mobjValues.HiddenControl("hddnPremimin", vbNullString))
		.Write(mobjValues.HiddenControl("hddnPremdep", vbNullString))
		.Write(mobjValues.HiddenControl("hddnIntwarr", vbNullString))
		.Write(mobjValues.HiddenControl("hddnPremdeal", vbNullString))
		.Write(mobjValues.HiddenControl("hddnRatepayf", vbNullString))
		.Write(mobjValues.HiddenControl("hddnPremfreq", vbNullString))
		.Write(mobjValues.HiddenControl("hddPremdeal_anu", vbNullString))
		.Write(mobjValues.HiddenControl("hddBirthdate", vbNullString))
		.Write(mobjValues.HiddenControl("hddEffecdate_to", vbNullString))
		.Write(mobjValues.HiddenControl("hddVp_initial", vbNullString))
		.Write(mobjValues.HiddenControl("hddsPremdeal_Chan", vbNullString))
		.Write(mobjValues.HiddenControl("hddsProcessed", vbNullString))
		.Write(mobjValues.HiddenControl("hddnPremdeal_old", vbNullString))
		.Write(mobjValues.HiddenControl("hddnCurrency", vbNullString))
		.Write(mobjValues.HiddenControl("hddnPremiumbas", vbNullString))
		.Write(mobjValues.HiddenControl("hddnOption", vbNullString))
		.Write(mobjValues.HiddenControl("hddsOption", vbNullString))
		.Write(mobjValues.HiddenControl("hddsPayfreq", vbNullString))
		
		.Write("<SCRIPT>UpdateFields()</" & "Script>")
	End With
	lclsPer_deposit = Nothing
End Sub

</script>
<%Response.Expires = -1

Response.CacheControl = "private"


mobjValues = New eFunctions.Values
    
Response.Write("" & vbCrLf)
Response.Write("<SCRIPT>" & vbCrLf)
    Response.Write("    var mstrPeriodoutofrangeMessage = '")
    Response.Write(GetLocalResourceObject("PeriodoutofrangeMessage"))
    Response.Write("';" & vbCrLf)
Response.Write("" & vbCrLf)
Response.Write("</" & "SCRIPT>")
    

mobjValues.sCodisplPage = Request.QueryString.Item("sCodispl")
mobjMenu = New eFunctions.Menues
mobjValues.ActionQuery = Session("bQuery")


mclsproduct = New eProduct.Product

Call mclsproduct.FindProduct_li(CInt(Session("nBranch")), CInt(Session("nProduct")), mobjValues.StringToType(CStr(Session("dEffecdate")), eFunctions.Values.eTypeData.etdDate))
Session("nType_rateproy") = mclsproduct.nType_rateproy
If mclsproduct.nType_rateproy = 2 Then
	mblnRateProy = True
Else
	mblnRateProy = False
End If

If mclsproduct.nPayiniti > 1 Then
	lintPayiniti = mclsproduct.nPayiniti
Else
	lintPayiniti = 0
End If

mstrApv = mclsproduct.sApv

'- verifica si en aporte incial se debe modificar
mclsnPayiniti = False
If mclsproduct.sApv = "1" And mclsproduct.nPayiniti > 0 Then
	mclscertificat = New ePolicy.Certificat
	Call mclscertificat.Find(CStr(Session("sCertype")), mobjValues.StringToType(CStr(Session("nBranch")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(Session("nProduct")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(Session("nPolicy")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(Session("nCertif")), eFunctions.Values.eTypeData.etdDouble), True)
	If mclscertificat.nPayfreq = 6 Then
		mclsnPayiniti = True
	End If
	mclscertificat = Nothing
Else
	mclscertificat = New ePolicy.Certificat
	Call mclscertificat.Find(CStr(Session("sCertype")), mobjValues.StringToType(CStr(Session("nBranch")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(Session("nProduct")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(Session("nPolicy")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(Session("nCertif")), eFunctions.Values.eTypeData.etdDouble), True)
	mintPayfreq = mclscertificat.nPayfreq
	mclscertificat = Nothing
	
	
End If

lintIndex2 = 1
lcolTab_intproy = New ePolicy.Tab_intproys
Call lcolTab_intproy.Find(CDate(Session("dEffecdate")))
lclsTab_intproy = lcolTab_intproy.Item(lintIndex2)
Session("nSvsMax") = lclsTab_intproy.nSvsproy_max
Session("nSvsMin") = lclsTab_intproy.nSvsproy_min
'UPGRADE_NOTE: Object mclsproduct may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mclsproduct = Nothing
'UPGRADE_NOTE: Object lcolTab_intproy may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
lcolTab_intproy = Nothing
'UPGRADE_NOTE: Object lclsTab_intproy may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
lclsTab_intproy = Nothing

%>
<HTML>
<HEAD>
    <META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 4 $|$$Date: 25/08/09 3:58p $|$$Author: Gazuaje $"

	
	
//% UpdateMonthlySchedule: 
//--------------------------------------------------------------------------------------------
function SetMinDeposit(nNewValue){
//--------------------------------------------------------------------------------------------
 <%If lintPayiniti > 1 Then%> 
    document.forms[0].tcnPayiniti.value = nNewValue;
<%End If%> 
 
}
//% UpdateMonthlySchedule: 
//--------------------------------------------------------------------------------------------
function UpdateMonthlySchedule(nNewValue, nMin){
//--------------------------------------------------------------------------------------------


	if (insConvertNumber(nNewValue) < insConvertNumber(nMin))
	{ 
	    // alert('El aporte inicial debe ser mayor o igual al definido en el producto');		
	     nNewValue = nMin;
         document.forms[0].tcnPayiniti.value =  insConvertNumber(nNewValue);
    }
    insDefValues('UpdateInitialPayment', "nNewValue=" + nNewValue, '/VTimeNet/Policy/PolicySeq');
}


//% ShowPayFreq: muestra las frecuencias de pago para la vía de pago en tratamiento
//--------------------------------------------------------------------------------------------
function ShowMonth(Index,nMes){
//--------------------------------------------------------------------------------------------
	var nPay
	nPay = '<%=lintPayiniti%>'
	if (nMes == 1)
		ShowPopUp('VI1410A.aspx?sCodispl=VI1410A&nYear_ini=' + marrArray[Index].tcnYear_ini + '&nPay=' + nPay + '&nMainAction=' + nMainAction ,'VI1410',600,350,'no','no',50,50);
	else
		alert(mstrPeriodoutofrangeMessage);		

}

//%InsShowIlustration: Muestra la ventana ValuePolIlustration, para mostrar la Ilustración
//------------------------------------------------------------------------------------------------
function InsShowIlustration(nError){
//------------------------------------------------------------------------------------------------
    var lstrQueryString
    var lstrQuery
    lstrQuery = '<%=Session("bQuery")%>'
	lstrQuery = lstrQuery.toLowerCase()

	if (nError<=0){
	    if(typeof(self.document.forms[0].tcnPremdeal)!='undefined'){
			with (self.document.forms[0]){
				lstrQueryString = '&sCertype=<%=Session("sCertype")%>' + '&nBranch=<%=Session("nBranch")%>' + 
				                  '&nProduct=<%=Session("nProduct")%>' + '&nPolicy=<%=Session("nPolicy")%>' + 
				                  '&nCertif=<%=Session("nCertif")%>'   + '&dEffecdate=<%=Session("dEffecdate")%>' + 
				                  '&nVp_initial=' + hddVp_initial.value + 
				                  '&dBirthdate=' + hddBirthdate.value + 
				                  '&dEffecdate_to=' + hddEffecdate_to.value +
				                  '&nOption='	+	hddnOption.value +
				                  '&sOption='	+	hddsOption.value +  
				                  '&bQuery='+ lstrQuery;
				if (lstrQuery=='true' || lstrQuery=='verdadero'){
					lstrQueryString = lstrQueryString +
									  '&nPremdeal_anu=' + hddPremdeal_anu.value +
					                  '&nPremfreq=' + hddnPremfreq.value +
					                  '&nIntwarr=' + tcnIntwarr.value+
					                  '&nIntwarrsav=' + tcnIntwarr3.value+
					                  '&nIntwarr2=' + tcnIntwarr2.value+
					                  '&nIntwarrsav2=' + tcnIntwarr4.value;

				}
				else{
					lstrQueryString = lstrQueryString +
									  '&nPremdeal_anu=' + tcnPremdeal.value +  
					                  '&nPremfreq=' + tcnPremfreq.value +
					                  '&nIntwarr=' + tcnIntwarr.value +
					                  '&nIntwarrsav=' + tcnIntwarr3.value+
					                  '&nIntwarr2=' + tcnIntwarr2.value+
					                  '&nIntwarrsav2=' + tcnIntwarr4.value;
				}
			}
		}
		ShowPopUp("../../Common/ShowIlustrationVul.aspx?sCodispl=VI1410" + lstrQueryString, "ValuePolIlustration", 750, 500, 'yes', 'yes', 10, 10) 
	}
} 

//%InsChangePremdeal: Calcula la prima proy. según frecuencia de pago
//------------------------------------------------------------------------------------------------
function InsChangePremdeal(nPremdeal){
//------------------------------------------------------------------------------------------------
	if(typeof(self.document.forms[0].tcnPremdeal)!='undefined'){

		with (self.document.forms[0]){
		    if (hddnPremdeal_old.value != insConvertNumber(tcnPremdeal.value)) {
		        hddsPremdeal_Chan.value='2';
		        hddPremdeal_anu.value = tcnPremdeal.value
		    }
		    else{
		        hddsPremdeal_Chan.value='1';
		    }

		    if (nPremdeal != '' && hddnRatepayf.value != -32768){
   
		        insDefValues('Ins_ConvertNumber', "nValue=" + nPremdeal + "&nFrequence=" + hddnRatepayf.value +"&nDivide=" + hddnDivide.value + "&nMultiply=" + hddnMultiply.value  , '/VTimeNet/Policy/PolicySeq')
		                   
		    }
		    else{
				tcnPremfreq.value = 0;
		    }

		}
	}   
}

function vtRound(val, dec)
{
   return Math.round(val *  Math.pow(10,dec))/Math.pow(10,dec) 
    
}

//%InsChangeInt_Warr: Si se modifica % de rentabilidad proyectada
//------------------------------------------------------------------------------------------------
function InsChangeInt_Warr(nInt_Warr){
//------------------------------------------------------------------------------------------------
	if(typeof(self.document.forms[0].tcnIntwarr)!='undefined'){
		with (self.document.forms[0]){
    
		    if (insConvertNumber(nInt_Warr) > 10 || insConvertNumber(nInt_Warr) < 0 || nInt_Warr == '' ){
		        alert ('La rentabilidad neta proyectada debe estar entre 0% y 10%');
 		        tcnIntwarr.value=hddnIntwarr.value
		    }
		    else
		    {
				if (hddnIntwarr.value != tcnIntwarr.value) {
				    hddsProcessed.value='1';
				    hddnIntwarr.value=nInt_Warr
				}
				else{
				    hddsProcessed.value='2';
				}
			}		
		}
	}
 }
</SCRIPT>
<%
Response.Write(mobjValues.StyleSheet())
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write(mobjMenu.setZone(2, "VI1410", Request.QueryString.Item("sWindowDescript"), CShort(Request.QueryString.Item("nWindowTy"))))
	mobjMenu = Nothing
	Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
End If
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" NAME="VI1410" ACTION="ValPolicySeq.aspx?sMode=2">
<%
Response.Write(mobjValues.ShowWindowsName("VI1410", Request.QueryString.Item("sWindowDescript")))
Call InsDefineHeader()
Call InsDefineAnnualHeader()
Call InsDefineProrratedHeader()

If Request.QueryString.Item("Type") <> "PopUp" Then
	Call InsInitial()
End If

If Request.QueryString.Item("Type") = "PopUp" Then
	Call InsPreVI1410Upd()
Else
	Call InsPreVI1410()
End If

mobjValues = Nothing
mobjGrid = Nothing
mclslife = Nothing

%>

</FORM> 
</BODY>
</HTML>






