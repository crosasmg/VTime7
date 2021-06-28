<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eClaim" %>
<%@ Import namespace="eProduct" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 11.33.47
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues
Private mclsLifeClaim As eClaim.Life_claim
Private mobjGrid As eFunctions.Grid


'% insDefineHeader: se definen los campos del grid
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.35.39
	mobjGrid.sSessionID = Session.SessionID
	mobjGrid.nUsercode = Session("nUsercode")
	'~End Body Block VisualTimer Utility
	
	mobjGrid.sCodisplPage = "SI024"
	Call mobjGrid.SetWindowParameters(Request.QueryString("sCodispl"), Request.QueryString("sWindowDescript"), Request.QueryString("nWindowTy"))
	
	'+ Se definen las columnas del grid    
	With mobjGrid.Columns
		
		Call .AddHiddenColumn("hddOrigin", "")
		Call .AddHiddenColumn("hddOriginAttributes", "")
		Call .AddPossiblesColumn(0, "Cuenta Origen", "cboOrigin", "Table5633", eFunctions.Values.eValuesType.clngComboType, vbNullString)
		Call .AddHiddenColumn("hddTaxBenefit", "")
		Call .AddPossiblesColumn(0, "Beneficio tributario", "cboTaxBenefit", "Table950", eFunctions.Values.eValuesType.clngComboType, vbNullString)
		Call .AddDateColumn(0, "Fecha de saldo", "tcdValueDate", "")
		Call .AddHiddenColumn("hddVP", "")
		Call .AddNumericColumn(40310, "Valor Póliza", "tcnVP", 18, CStr(0), False, "", True, 6,  ,  , "")
		Call .AddHTMLColumn(0, "%Traspaso", "tcnTransPercent")
		Call .AddHTMLColumn(0, "Total traspaso", "tcnTransfAmount")
		Call .AddHTMLColumn(0, "Impuesto UF", "tcnTax_Amount")
		Call .AddHTMLColumn(0, "Saldo UF", "tcnBalance")
		Call .AddNumericColumn(40310, "Valor UF", "tcnExchange", 18, CStr(0), False, "", True, 6,  ,  , "")
	End With
	
	'+ Se definen las propiedades generales del grid
	With mobjGrid
		.Codispl = "SI024"
		.Codisp = "SI024"
		.DeleteButton = False
		.AddButton = False
		.Columns("Sel").GridVisible = False
		.bOnlyForQuery = True
	End With
End Sub

'% insPreSI016: se cargan los controles de la página
'--------------------------------------------------------------------------------------------
Private Sub insPreSI024()
	'--------------------------------------------------------------------------------------------
	Dim lintIndex As Short
	Dim lcolClaim_cases As Object
	Dim lclsClaim_case As Object
	Dim lstrReloadBySeqCase As Object
	Dim lobjTables As Object
	Dim lstrStatusDesc As Object
	
	Dim lclsOrigin As eClaim.Claim_origin
	
	lintIndex = 0
	lclsOrigin = New eClaim.Claim_origin
	
	If mclsLifeClaim.FindOrigins(CDbl(Session("nClaim")), CInt(Session("nCase_num")), CInt(Session("nDeman_type")), "") Then
		For	Each lclsOrigin In mclsLifeClaim.Origins
			With mobjGrid
				
				mobjGrid.Columns("hddOrigin").DefValue = CStr(lclsOrigin.nOrigin)
				mobjGrid.Columns("cboOrigin").DefValue = CStr(lclsOrigin.nOrigin)
				mobjGrid.Columns("hddTaxBenefit").DefValue = CStr(lclsOrigin.nTax_benefit)
				mobjGrid.Columns("cboTaxBenefit").DefValue = CStr(lclsOrigin.nTax_benefit)
				mobjGrid.Columns("tcdValueDate").DefValue = CStr(lclsOrigin.dValuedate)
				mobjGrid.Columns("hddVP").DefValue = CStr(lclsOrigin.nVP)
				mobjGrid.Columns("tcnVP").DefValue = CStr(lclsOrigin.nVP)
				mobjGrid.Columns("tcnTransPercent").DefValue = mobjValues.NumericControl("tcnTransPercent", 5, CStr(lclsOrigin.nTransf_percent),  , "Porcentaje de traspaso",  , 2,  ,  ,  ,  , lclsOrigin.nVP <= 0,  ,  , False)
				mobjGrid.Columns("tcnTransfAmount").DefValue = mobjValues.NumericControl("tcnTransfAmount", 18, CStr(lclsOrigin.nTransf_amount),  , "Total traspaso",  , 6,  ,  ,  ,  , lclsOrigin.nVP <= 0,  ,  , False)
				mobjGrid.Columns("tcnTax_Amount").DefValue = mobjValues.NumericControl("tcnTax_Amount", 18, CStr(lclsOrigin.nTax_Amount),  , "Total Impuestos",  , 6,  ,  ,  ,  , True)
				mobjGrid.Columns("tcnBalance").DefValue = mobjValues.NumericControl("tcnBalance", 18, CStr(lclsOrigin.nBalance),  , "Saldo restante",  , 6,  ,  ,  ,  , True)
				mobjGrid.Columns("tcnExchange").DefValue = CStr(lclsOrigin.nExchange)
				mobjGrid.Columns("hddOriginAttributes").DefValue = lclsOrigin.sOriginAttributes
				
				Response.Write(.DoRow)
			End With
			
			lintIndex = lintIndex + 1
		Next lclsOrigin
	End If
	Response.Write(mobjGrid.closeTable())
	
	
	'UPGRADE_NOTE: Object lclsClaim_case may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
	lclsClaim_case = Nothing
	'UPGRADE_NOTE: Object lcolClaim_cases may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
	lcolClaim_cases = Nothing
End Sub
'+ insShowValuesSI024: Muestra los valores de un siniestro de vida e invoca a la función insCalcSI024
'+ la cual realiza una serie de cálculos automáticos - ACM - 05/02/2001
'----------------------------------------------------------------------------------------------------------------------------------------------------
Function insShowValuesSI024() As Object
	'----------------------------------------------------------------------------------------------------------------------------------------------------
	'- Definición de variables y objetos locales usados en esta transacción
	Dim lblnFound As Boolean
	
	Dim lclsClaimDisability As eClaim.ClaimDisability
	Dim nPercent As Double
	
	mclsLifeClaim = New eClaim.Life_claim
	
	Call mclsLifeClaim.insPreSI024(CStr(Session("sCertype")), CInt(Session("nBranch")), CInt(Session("nProduct")), CInt(Session("nPolicy")), CInt(Session("nCertif")), CDate(Session("dEffecdate")), CDbl(Session("nClaim")), CInt(Session("nCase_num")), CInt(Session("nDeman_type")))
	
	lblnFound = mclsLifeClaim.bFound
	Response.Write(mobjValues.HiddenControl("tcnBranchT", mclsLifeClaim.sBrancht))
	
	lclsClaimDisability = New eClaim.ClaimDisability
	
	nPercent = lclsClaimDisability.insCalPercentDisability(CInt(Session("nClaim")), CInt(Session("nCase_num")), CInt(Session("nDeman_Type")))
	
	
Response.Write("" & vbCrLf)
Response.Write("    <A NAME=""BeginPage""></A>" & vbCrLf)
Response.Write("    <P ALIGN=""Center"">" & vbCrLf)
Response.Write("		<LABEL ID=40248><A HREF=""#Datos de siniestros de vida""> Datos de siniestros de vida</A></LABEL><LABEL ID=0> | </LABEL>" & vbCrLf)
Response.Write("		<LABEL ID=40250><A HREF=""#Pago de rentas""> Pago de rentas</A></LABEL><LABEL ID=0> | </LABEL>" & vbCrLf)
Response.Write("        <LABEL ID=40252><A HREF=""#Datos de verificación""> Datos de verificación</A></LABEL>" & vbCrLf)
Response.Write("    </P>" & vbCrLf)
Response.Write("    <TABLE WIDTH=""100%"">" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD COLSPAN=""4"" CLASS=""HighLighted""><LABEL ID=40253><A NAME=""Datos de siniestros de vida"">Datos de siniestros de vida</A></LABEL></TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD WIDTH=""100%"" COLSPAN=""4"" CLASS=""Horline""></TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("               " & vbCrLf)
Response.Write("            <TD><LABEL ID=9634>Tipo de siniestro</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")

	'If Session("nProdClas") = 13 Then
	Dim lclsProduct_li As eProduct.Product
	lclsProduct_li = New eProduct.Product
	'UPGRADE_WARNING: Date was upgraded to Today and has a new behavior. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1041.aspx'
	Call lclsProduct_li.FindProduct_li(CInt(Session("nBranch")), CInt(Session("nProduct")), Today, True)
	If lclsProduct_li.sClannpei <> vbNullString Then
		With mobjValues
			.List = "1,6" '"Muerte,Supervivencia,Invalidez"
			.TypeList = 1 'Incluir
			.BlankPosition = False
		End With
		Response.Write(mobjValues.PossiblesValues("cbeClaimType", "Table210", eFunctions.Values.eValuesType.clngComboType, CStr(mclsLifeClaim.nCla_li_typ),  ,  ,  ,  ,  , "Indemnity(this);",  ,  , "Tipo de siniestro"))
	Else
		Response.Write(mobjValues.PossiblesValues("cbeClaimType", "Table210", eFunctions.Values.eValuesType.clngComboType, CStr(mclsLifeClaim.nCla_li_typ),  ,  ,  ,  ,  , "EndDateEnabled(this.value);",  ,  , "Tipo de siniestro"))
	End If
Response.Write("</TD>" & vbCrLf)
Response.Write("            <TD><LABEL ID=9636>Indemnización</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")

	'If Session("nProdClas") = 13 Then
	If lclsProduct_li.sClannpei <> vbNullString Then
		With mobjValues
			.List = "1,2" '"Normal,Pension"
			.TypeList = 1 'Incluir
			.BlankPosition = False
		End With
		Response.Write(mobjValues.PossiblesValues("cbeIndemnity", "Table211", eFunctions.Values.eValuesType.clngComboType, CStr(mclsLifeClaim.nIn_lif_typ),  ,  ,  ,  ,  , "Indemnity(this);",  ,  , "Tipo de indemnización"))
	Else
		Response.Write(mobjValues.PossiblesValues("cbeIndemnity", "Table211", eFunctions.Values.eValuesType.clngComboType, CStr(mclsLifeClaim.nIn_lif_typ),  ,  ,  ,  ,  , "IndemnityEnabled(this.value);",  ,  , "Tipo de indemnización"))
	End If
	
Response.Write("</TD>" & vbCrLf)
Response.Write("	    <TR>" & vbCrLf)
Response.Write("	        <TD COLSPAN=""4"" CLASS=""HighLighted""><LABEL ID=0><A NAME=""Invalidez"">Invalidez</A></LABEL></TD>" & vbCrLf)
Response.Write("	    </TR>" & vbCrLf)
Response.Write("	    <TR>" & vbCrLf)
Response.Write("	        <TD WIDTH=""100%"" COLSPAN=""4"" CLASS=""Horline""></TD>" & vbCrLf)
Response.Write("	    </TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD><LABEL ID=0>% Indemnización</LABEL></TD>" & vbCrLf)
Response.Write("			<TD>")


Response.Write(mobjValues.NumericControl("gmnDisabilityRate", 5, mobjValues.TypeToString(nPercent, eFunctions.Values.eTypeData.etdDouble, True, 2),  , "Porcentaje de indemnización de invalidez",  , 2,  ,  ,  , "EndDateEnabled(this);", True))


Response.Write("</TD>" & vbCrLf)
Response.Write("			<TD>")


Response.Write(mobjValues.AnimatedButtonControl("btnbigwnotes", "/VTimeNet/Images/menu_transaction.png", "Porcentajes de indemnización de invalidez",  , "ShowSI024D();"))


Response.Write("</TD>" & vbCrLf)
Response.Write("	    </TR>" & vbCrLf)
Response.Write("	    <TR>" & vbCrLf)
Response.Write("	        <TD COLSPAN=""4"" CLASS=""HighLighted""><LABEL ID=40254><A NAME=""Pago de rentas"">Pago de rentas</A></LABEL></TD>" & vbCrLf)
Response.Write("	    </TR>" & vbCrLf)
Response.Write("	    <TR>" & vbCrLf)
Response.Write("	        <TD WIDTH=""100%"" COLSPAN=""4"" CLASS=""Horline""></TD>" & vbCrLf)
Response.Write("	    </TR>" & vbCrLf)
Response.Write("	    <TR>" & vbCrLf)
Response.Write("			<TABLE WIDTH=""100%"">" & vbCrLf)
Response.Write("				<TR>" & vbCrLf)
Response.Write("				<TD WIDTH=""15%""><LABEL ID=9638>Desde</LABEL></TD>" & vbCrLf)
Response.Write("					<TD>")


Response.Write(mobjValues.DateControl("gmdInit_date", mobjValues.TypeToString(mclsLifeClaim.dInit_date, eFunctions.Values.eTypeData.etdDate),  , "Fecha a partir de la cual el beneficiario comienza a recibir la renta",  ,  ,  ,  , True))


Response.Write("</TD>" & vbCrLf)
Response.Write("					<TD COLSPAN=""4""></TD>" & vbCrLf)
Response.Write("					<TD><LABEL ID=9635>Hasta</LABEL></TD>" & vbCrLf)
Response.Write("					<TD>")


Response.Write(mobjValues.DateControl("gmdEnd_date", mobjValues.TypeToString(mclsLifeClaim.dEnd_date, eFunctions.Values.eTypeData.etdDate),  , "Fecha hasta la cual el beneficiario recibe la renta",  ,  ,  ,  , True))


Response.Write("</TD>" & vbCrLf)
Response.Write("				</TR>" & vbCrLf)
Response.Write("				<TR>" & vbCrLf)
Response.Write("					<TD><LABEL ID=9639>Interés</LABEL></TD>" & vbCrLf)
Response.Write("					<TD>")


Response.Write(mobjValues.NumericControl("gmnInterest", 4, CStr(mclsLifeClaim.nInterest),  , "Porcentaje de interes a aplicar para el cálculo de la renta",  , 2,  ,  ,  ,  , True))


Response.Write("</TD>" & vbCrLf)
Response.Write("					<TD WIDTH=""10%"">&nbsp;</TD>" & vbCrLf)
Response.Write("					<TD><LABEL ID=9640>Monto</LABEL></TD>" & vbCrLf)
Response.Write("					<TD>")


Response.Write(mobjValues.NumericControl("gmnMonth_amo", 18, CStr(mclsLifeClaim.nMonth_amou),  , "Monto a pagar por concepto de pago de renta", True, 6,  ,  ,  ,  , True))


Response.Write("</TD>" & vbCrLf)
Response.Write("					<TD WIDTH=""10%"">&nbsp;</TD>" & vbCrLf)
Response.Write("					<TD><LABEL ID=9640>Frequencia de pago</LABEL></TD>" & vbCrLf)
Response.Write("					<TD>")


Response.Write(mobjValues.PossiblesValues("cbePayFreq", "Table36", eFunctions.Values.eValuesType.clngComboType, CStr(mclsLifeClaim.nPayFreq),  ,  ,  ,  ,  ,  , True,  , "Frequencia de pago en la cual se pagará la renta"))


Response.Write("</TD>" & vbCrLf)
Response.Write("				</TR>" & vbCrLf)
Response.Write("			</TABLE>" & vbCrLf)
Response.Write("	    </TR>" & vbCrLf)
Response.Write("		<TABLE WIDTH=""100%"">" & vbCrLf)
Response.Write("			<TR>" & vbCrLf)
Response.Write("			    <TD COLSPAN=""4"" CLASS=""HighLighted""><LABEL ID=40255><A NAME=""Datos de verificación"">Datos de verificación</A></LABEL></TD>" & vbCrLf)
Response.Write("			</TR>" & vbCrLf)
Response.Write("	    <TR>" & vbCrLf)
Response.Write("	        <TD WIDTH=""100%"" COLSPAN=""4"" CLASS=""Horline""></TD>" & vbCrLf)
Response.Write("	    </TR>" & vbCrLf)
Response.Write("	    <TR>" & vbCrLf)
Response.Write("	        <TD><LABEL ID=0>Tasa de crecimiento inicial</LABEL></TD>" & vbCrLf)
Response.Write("			<TD>")


Response.Write(mobjValues.NumericControl("gmnGrowth_RateI", 18, CStr(mclsLifeClaim.nGrowth_RateI),  , "Corresponde a la tasa de crecimiento inicial del año póliza en curso", True, 6,  ,  ,  ,  , False))


Response.Write("</TD>" & vbCrLf)
Response.Write("			<TD><LABEL ID=0>Tasa de crecimiento final</LABEL></TD>" & vbCrLf)
Response.Write("			<TD>")


Response.Write(mobjValues.NumericControl("gmnGrowth_RateE", 18, CStr(mclsLifeClaim.nGrowth_RateE),  , "Corresponde a la tasa de crecimiento final del año póliza en curso", True, 6,  ,  ,  ,  , False))


Response.Write("</TD>			" & vbCrLf)
Response.Write("	    </TR>		    " & vbCrLf)
Response.Write("	    <TR>" & vbCrLf)
Response.Write("	        <TD><LABEL ID=9632>Préstamos</LABEL></TD>" & vbCrLf)
Response.Write("	        <TD>")


Response.Write(mobjValues.NumericControl("gmnAdv_paymen", 18, CStr(mclsLifeClaim.nAdv_paymen),  , "Monto total de préstamos", True, 6,  ,  ,  ,  , True))


Response.Write("</TD>" & vbCrLf)
Response.Write("	        <TD><LABEL ID=9641>Rescate</LABEL></TD>" & vbCrLf)
Response.Write("	        <TD>")


Response.Write(mobjValues.NumericControl("gmnSalvage", 18, CStr(mclsLifeClaim.nSalvage),  , "Monto del valor de rescate", True, 6,  ,  ,  ,  , True))


Response.Write("</TD>" & vbCrLf)
Response.Write("	    </TR>" & vbCrLf)
Response.Write("	    <TR>" & vbCrLf)
Response.Write("	        <TD><LABEL ID=9633>Capital</LABEL></TD>" & vbCrLf)
Response.Write("	        <TD>")


Response.Write(mobjValues.NumericControl("gmnCapital", 18, CStr(mclsLifeClaim.nCapital),  , "Capital total de las coberturas del siniestro", True, 6,  ,  ,  ,  , True))


Response.Write("</TD>" & vbCrLf)
Response.Write("	        <TD><LABEL ID=9637>Indemnización</LABEL></TD>" & vbCrLf)
Response.Write("	        <TD>")


Response.Write(mobjValues.NumericControl("gmnIndemn", 18, CStr(mclsLifeClaim.nIndemnity),  , "Monto total máximo de indemnización", True, 6,  ,  ,  ,  , True))


Response.Write("</TD>" & vbCrLf)
Response.Write("	        " & vbCrLf)
Response.Write("	    </TR>" & vbCrLf)
Response.Write("	    <TR>" & vbCrLf)
Response.Write("	        <TD><LABEL ID=0>Opción de indemnización</LABEL></TD>" & vbCrLf)
Response.Write("	        <TD>")


Response.Write(mobjValues.PossiblesValues("cbeOption", "Table5519", eFunctions.Values.eValuesType.clngComboType, CStr(mclsLifeClaim.nOption),  ,  ,  ,  ,  ,  , True,  , "Opción de Inmdemnización seleccionada para la póliza"))


Response.Write("</TD>" & vbCrLf)
Response.Write("	        <TD ROWSPAN=2 COLSPAN=2>" & vbCrLf)
Response.Write("	            <TABLE ALIGN =LEFT>" & vbCrLf)
Response.Write("	              <TR>" & vbCrLf)
Response.Write("	        		  <TD>")


Response.Write(mobjValues.OptionControl(0, "optTransType", "Total", "1", "1", "insChangeTransType(this.value);", False, 7, "Indica que el traspaso se hará por el monto total"))


Response.Write("</TD>" & vbCrLf)
Response.Write("                      <TD></TD>" & vbCrLf)
Response.Write("                  </TR>" & vbCrLf)
Response.Write("	              <TR>" & vbCrLf)
Response.Write("			          <TD>")


Response.Write(mobjValues.OptionControl(0, "optTransType", "Parcial", "9", "2", "insChangeTransType(this.value);", False, 8, "Indica que el traspaso se hará por el monto o porcentaje que indique el usuario"))


Response.Write("</TD>" & vbCrLf)
Response.Write("					<TD>%")


Response.Write(mobjValues.NumericControl("tcnUniqueTransPercent", 5, "", False, "Porcentaje de traspaso", False, 2,  ,  ,  , "insChangeTransPercent(this.value);", True))


Response.Write("</TD>" & vbCrLf)
Response.Write("                  </TR>" & vbCrLf)
Response.Write("                </TABLE>   " & vbCrLf)
Response.Write("	        </TD>" & vbCrLf)
Response.Write("	        <!--TD></TD-->" & vbCrLf)
Response.Write("	    </TR>" & vbCrLf)
Response.Write("	    <TR>" & vbCrLf)
Response.Write("	        <TD><LABEL ID=0>AFP</LABEL></TD>" & vbCrLf)
Response.Write("	        <TD>")


Response.Write(mobjValues.PossiblesValues("cbeAFP", "Table5524", eFunctions.Values.eValuesType.clngComboType, CStr(mclsLifeClaim.nAFP),  ,  ,  ,  ,  ,  , False,  , "Administradora de fondos de pension (AFP) a la que se encuentra afiliado el cliente"))


Response.Write("</TD>" & vbCrLf)
Response.Write("	        " & vbCrLf)
Response.Write("	        <!--TD></TD>" & vbCrLf)
Response.Write("	        <TD></TD-->" & vbCrLf)
Response.Write("	    </TR>" & vbCrLf)
Response.Write("	    <TR>" & vbCrLf)
Response.Write("	        <TD><LABEL ID=9637>Moneda</LABEL></TD>" & vbCrLf)
Response.Write("			<TD>")


Response.Write(mobjValues.PossiblesValues("cbeCurrency", "Table11", eFunctions.Values.eValuesType.clngComboType, CStr(mclsLifeClaim.nCurrency),  ,  ,  ,  ,  ,  , True,  , "Moneda de pago"))


Response.Write("</TD>" & vbCrLf)
Response.Write("			<TD><LABEL ID=0>Bono de Permanencia</LABEL></TD>" & vbCrLf)
Response.Write("			<TD>")


Response.Write(mobjValues.NumericControl("tcnStayBonus", 18, CStr(mclsLifeClaim.nStay_Bonus),  , "Bono de permanencia asociado a la póliza", True, 6,  ,  ,  ,  , True))


Response.Write("</TD>			" & vbCrLf)
Response.Write("	    </TR>		    " & vbCrLf)
Response.Write("	    <TR>" & vbCrLf)
Response.Write("			<TD COLSPAN=4>	" & vbCrLf)
Response.Write("			")

	insDefineHeader()
	insPreSI024()
	
Response.Write("" & vbCrLf)
Response.Write("			</TD>" & vbCrLf)
Response.Write("	    </TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD COLSPAN=4>	" & vbCrLf)
Response.Write("				<TABLE ALIGN=RIGHT>" & vbCrLf)
Response.Write("				    <TR>" & vbCrLf)
Response.Write("				        <TD><LABEL ID=0>Capital</LABEL></TD>" & vbCrLf)
Response.Write("						<TD>")


Response.Write(mobjValues.NumericControl("tcnCapitalAPV", 18, CStr(mclsLifeClaim.nApv_capital),  , "", True, 6,  ,  ,  ,  , True))


Response.Write("</TD>" & vbCrLf)
Response.Write("				    </TR>		    " & vbCrLf)
Response.Write("				    <TR>" & vbCrLf)
Response.Write("				        <TD><LABEL ID=0>Saldo antes C. 2052</LABEL></TD>" & vbCrLf)
Response.Write("						<TD>")


Response.Write(mobjValues.NumericControl("tcnApv_balance_ac2052", 18, CStr(mclsLifeClaim.nApv_balance_ac2052),  , "", True, 6,  ,  ,  ,  , True))


Response.Write("</TD>" & vbCrLf)
Response.Write("				    </TR>		    " & vbCrLf)
Response.Write("				    <TR>" & vbCrLf)
Response.Write("				        <TD><LABEL ID=0>Saldo según D. 2052</LABEL></TD>" & vbCrLf)
Response.Write("						<TD>")


Response.Write(mobjValues.NumericControl("tcnApv_balance_bc2052", 18, CStr(mclsLifeClaim.nApv_balance_bc2052),  , "", True, 6,  ,  ,  ,  , True))


Response.Write("</TD>" & vbCrLf)
Response.Write("				    </TR>		    " & vbCrLf)
Response.Write("				    <TR>" & vbCrLf)
Response.Write("				        <TD><LABEL ID=0>Traspaso AFP</LABEL></TD>" & vbCrLf)
Response.Write("						<TD>")


Response.Write(mobjValues.NumericControl("tcnTransf_amount", 18, CStr(mclsLifeClaim.nTransf_amount),  , "", True, 6,  ,  ,  ,  , True))


Response.Write("</TD>" & vbCrLf)
Response.Write("				    </TR>		    " & vbCrLf)
Response.Write("				    <TR>" & vbCrLf)
Response.Write("				        <TD><LABEL ID=0>Impuesto D. 2052</LABEL></TD>" & vbCrLf)
Response.Write("						<TD>")


Response.Write(mobjValues.NumericControl("tcnApv_tax", 18, CStr(mclsLifeClaim.nApv_tax),  , "", True, 6,  ,  ,  ,  , True))


Response.Write("</TD>" & vbCrLf)
Response.Write("				    </TR>		    " & vbCrLf)
Response.Write("				    <TR>" & vbCrLf)
Response.Write("				        <TD><LABEL ID=0>Saldo a pagar / Beneficiarios </LABEL></TD>" & vbCrLf)
Response.Write("						<TD>")


Response.Write(mobjValues.NumericControl("tcnApv_benef_balance", 18, CStr(mclsLifeClaim.nApv_benef_balance),  , "", True, 6,  ,  ,  ,  , True))


Response.Write("</TD>" & vbCrLf)
Response.Write("				    </TR>		    " & vbCrLf)
Response.Write("				</TABLE>" & vbCrLf)
Response.Write("			</TD>" & vbCrLf)
Response.Write("	    </TR>	    " & vbCrLf)
Response.Write("	    </TABLE>" & vbCrLf)
Response.Write("	</TABLE>" & vbCrLf)
Response.Write("	<P ALIGN=""Center"">        " & vbCrLf)
Response.Write("	    ")


Response.Write(mobjValues.AnimatedButtonControl("btnBack", "/VTimeNet/Images/btnBack.gif", "Ir al inicio de la ventana", "#BeginPage"))


Response.Write("" & vbCrLf)
Response.Write("	</P>")

	
	
	Response.Write("<SCRIPT>EndDateEnabled('0');</" & "Script>")
	Response.Write("<SCRIPT>IndemnityEnabled(" & mclsLifeClaim.nIn_lif_typ & ");</" & "Script>")
	'UPGRADE_NOTE: Object mclsLifeClaim may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
	mclsLifeClaim = Nothing
	'UPGRADE_NOTE: Object mobjValues may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
	mobjValues = Nothing
	
End Function

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("si024")

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.33.47
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "si024"



mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.33.47
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility
    If Session("nTransaction") = eClaim.Claim_win.eClaimTransac.clngClaimQuery Then
        mobjGrid.ActionQuery = Session("bQuery")
    Else
        mobjGrid.ActionQuery = False
        Session("bQuery") = False
    End If
%>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">
    <%'UPGRADE_NOTE: Language element 'SCRIPT' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file '/VTimeNet/Scripts/GenFunctions.js' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
    <%'UPGRADE_NOTE: Language element '#INCLUDE' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file 'vtime/Includes/Constantes.aspx' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<!-- #INCLUDE VIRTUAL="~/VTimeNet/Includes/Constantes.aspx" -->

    <%'UPGRADE_NOTE: Language element '#INCLUDE' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file 'vtime/Includes/Claim.aspx' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<!-- #INCLUDE VIRTUAL="~/VTimeNet/Includes/Claim.aspx" -->

<%
Response.Write(mobjValues.StyleSheet())
If Request.QueryString("Type") <> "PopUp" Then
	Response.Write(mobjMenu.setZone(2, Request.QueryString("sCodispl"), ""))
	'UPGRADE_NOTE: Object mobjMenu may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
	mobjMenu = Nothing
	Response.Write("<SCRIPT>var nMainAction=304" & Request.QueryString("nMainAction") & "</SCRIPT>")
End If


%>
<SCRIPT LANGUAGE="JavaScript">
//+Variable para el control de versiones
    document.VssVersion="$$Revision: 7 $|$$Date: 21-01-13 11:46 $|$$Author: Jrengifo $"

//- Variable para controlar el cambio de valor en el combo
	var mintPrevvalue
    var sTotalLoss
	var sCauseCod
	
	
	sTotalLoss = <%="'" & Session("sTotalLoss") & "'"%> 
	sCauseCod  = <%="'" & Session("sCause") & "'"%> 

//* Habilita/Deshabilita los campos del frame "Datos de verificación"
//-----------------------------------------------------------------------------------------------------------------------------------
function EndDateEnabled(sField){
//-----------------------------------------------------------------------------------------------------------------------------------
	with(self.document.forms[0]){
//		if((sField!=0 && sTotalLoss=='2')||(sField!=0 && sCauseCod=='21'))
		   	
		if(sField!=0)	
		 {				    
			gmnIndemn.disabled = true
			insDefValues('IndemAmount','gmnIndemn='+ gmnIndemn.value + "&Cla_li_typ=" + cbeClaimType.value ,'/VTimeNet/Claim/CaseSeq');
	    }
		else{
				gmnIndemn.disabled = false
		    }
	}
}
// Llena los campos para el producto Universitario
//-----------------------------------------------------------------------------------------------------------------------------------
function Indemnity(Field){
//-----------------------------------------------------------------------------------------------------------------------------------
	with (self.document.forms[0]){
		if (Field.name == 'cbeIndemnity')
			insDefValues("CalIndemnity","sCodispl=SI024&nClaimType=" + cbeClaimType.value + "&nIndemnity=" + Field.value)
		else
			insDefValues("CalIndemnity","sCodispl=SI024&nClaimType=" + Field.value + "&nIndemnity=" + cbeIndemnity.value)		
	}
}

//* Habilita/Deshabilita los campos del frame "Pago de Rentas"
//-----------------------------------------------------------------------------------------------------------------------------------
function IndemnityEnabled(Value){
//-----------------------------------------------------------------------------------------------------------------------------------
     if(mintPrevvalue!=Value){
      mintPrevvalue = Value
      if(mintPrevvalue==3 || mintPrevvalue==2|| mintPrevvalue==5)
      {
        with(self.document.forms[0])
        {
            if (mintPrevvalue==3)
            {
               gmdEnd_date.disabled =true
               btn_gmdEnd_date.disabled =true
	 	       gmdEnd_date.value = ""
            }
            else
            {
              if (mintPrevvalue!=5)
               {
				  if (gmdEnd_date.value==''){
					insDefValues('DateIndem','nIn_lif_typ='+ Value ,'/VTimeNet/Claim/CaseSeq');
				  }
				     gmdEnd_date.disabled =false
				     btn_gmdEnd_date.disabled =false	
			   }						
            }
            
            if (mintPrevvalue==5)
            {
                gmdInit_date.disabled =true
                btn_gmdInit_date.disabled = true
	 	        cbePayFreq.disabled =true
	 	        gmnInterest.disabled =true
	 	        gmnMonth_amo.disabled =true
                gmnIndemn.disabled =true	 	       
            }
            else
            {
            gmdInit_date.disabled =false
            btn_gmdInit_date.disabled = false
	 	    cbePayFreq.disabled =false
	 	    gmnInterest.disabled =false
	 	    gmnMonth_amo.disabled =false
	 	    }
	    }	
	  }  
	  else
	  {
	    with(self.document.forms[0])
	    {
	        gmdInit_date.disabled =true
	        btn_gmdInit_date.disabled =true
	        gmdInit_date.value= ""
	        btn_gmdEnd_date.disabled =true
		    gmdEnd_date.disabled =true
	 	    gmdEnd_date.value = ""
	 	    cbePayFreq.disabled =true
	 	    cbePayFreq.value = ""
	 	    gmnInterest.disabled =true
	 	    gmnInterest.value = "0,00"
	 	    gmnMonth_amo.disabled =true
	 	    gmnMonth_amo.value = "0,000000"     
        } 
      }
    }  
}

//% ShowSI024D: Muestra la ventana de porcentaje de indemnización de invalidez
//-------------------------------------------------------------------------------------------
function ShowSI024D(){
//-------------------------------------------------------------------------------------------
	with (self.document.forms[0]){
		if (cbeClaimType.value==6){
			ShowPopUp("SI024DFrame.aspx","SI024DFrame",700,400,true,false,20,20)
		}
	}
}

//-------------------------------------------------------------------------------------------
function insRCalLine(nPos, sInput) {
//-------------------------------------------------------------------------------------------
   var percents=document.getElementsByName("tcnTransPercent");
   var vps=document.getElementsByName("hddVP");
   var totals=document.getElementsByName("tcnTransfAmount");
   var attrs=document.getElementsByName("hddOriginAttributes");
   var benefits=document.getElementsByName("hddTaxBenefit");
   var taxes =document.getElementsByName("tcnTax_Amount");
   var balances =document.getElementsByName("tcnBalance");
   
    nVP = insConvertNumber(vps[nPos].value,".",",", false); 
    if (sInput=="percent")
    {
		nPercent = insConvertNumber(percents[nPos].value,".",",", false);
	    nTransF =  nVP * nPercent  /100
		totals[nPos].value=VTFormat(nTransF , ".", ",", ".", 6, true);
	}
	else
	{
		nTransF = insConvertNumber(totals[nPos].value,".",",", false);
		nPercent = nTransF*100/nVP;
		percents[nPos].value=VTFormat(nPercent , ".", ",", ".", 2, true);
	}	
		
	nTax=0
		
	if (attrs[nPos].value=="2052" && benefits[nPos].value=="2")
	{
		nTax=(nVP-nTransF)*.15;
		//taxes[nPos].value=nVP-nTransF;
	}
	taxes[nPos].value=VTFormat(nTax , ".", ",", ".", 6, true);
	nBalance= nVP-nTransF-nTax;
	balances[nPos].value=VTFormat(nBalance, ".", ",", ".", 6, true);

} 

//-------------------------------------------------------------------------------------------
function insSummarize() {
//-------------------------------------------------------------------------------------------
   var vps=document.getElementsByName("hddVP");
   var totals=document.getElementsByName("tcnTransfAmount");
   var attrs=document.getElementsByName("hddOriginAttributes");
   var benefits=document.getElementsByName("hddTaxBenefit");
   var taxes =document.getElementsByName("tcnTax_Amount");
   var balances =document.getElementsByName("tcnBalance");
   var nTotalA2052=0;	   
   var nTotalB2052=0;
   var nTotalBalance=0;
   var nTotalTransf=0;
   var nTotalTaxes=0;
   
   for (var nPos=0;nPos<vps.length;nPos++)
   {	
        nVP = insConvertNumber(vps[nPos].value,".",",", false); 
        nTransF = insConvertNumber(totals[nPos].value,".",",", false); 
		nTax= insConvertNumber(taxes[nPos].value,".",",", false); 
		nBalance= nVP-nTransF-nTax;
		nTotalBalance+=nBalance;
		nTotalTransf+=nTransF;
		nTotalTaxes+=nTax;
		if (attrs[nPos].value=="2052")
			nTotalB2052+=nBalance;
		else
			nTotalA2052+=nBalance;
   } 

   
   var nCapital=insConvertNumber("0"+document.getElementsByName("tcnCapitalAPV")[0].value,".",",", false);
   nTotalBalance=nTotalBalance+nCapital;
   document.getElementsByName("tcnApv_balance_ac2052")[0].value=VTFormat(nTotalA2052, ".", ",", ".", 6, true);
   document.getElementsByName("tcnApv_balance_bc2052")[0].value=VTFormat(nTotalB2052, ".", ",", ".", 6, true);
   document.getElementsByName("tcnTransf_amount")[0].value=VTFormat(nTotalTransf, ".", ",", ".", 6, true);
   document.getElementsByName("tcnApv_tax")[0].value=VTFormat(nTotalTaxes, ".", ",", ".", 6, true);
   document.getElementsByName("tcnApv_benef_balance")[0].value=VTFormat(nTotalBalance, ".", ",", ".", 6, true);
}
//-------------------------------------------------------------------------------------------
function insChangeTransPercent(sValue){
//-------------------------------------------------------------------------------------------
	var percents=document.getElementsByName("tcnTransPercent");
   
	if (window.confirm("Se recalcularán los valores de traspaso del cuadro resumen."))
	{
		for (var nPos=0;nPos<percents.length;nPos++)
		{	
			percents[nPos].value = sValue;
			insRCalLine(nPos,"percent");
		} 
		insSummarize();  
	}	
}


//-------------------------------------------------------------------------------------------
function insChangeTransType(sOption)
//-------------------------------------------------------------------------------------------
{
   var oPercent=document.getElementsByName("tcnUniqueTransPercent")[0]; 
    if (sOption=='1') //total
    {
		oPercent.disabled=true;
		oPercent.value = "100,00";
		if (window.confirm("Se recalcularán los valores de traspaso del cuadro resumen."))
			insChangeTransPercent("100,00");
    }
    else //Parcial
    {
		oPercent.disabled=false;
		oPercent.value = "";
    }
}
//------------------------------------------
function insRedefineEvents(){
//------------------------------------------
   var percents=document.getElementsByName("tcnTransPercent");
   var amounts=document.getElementsByName("tcnTransfAmount");   
   
   for (var nPos=0;nPos<percents.length;nPos++)
   {	percents[nPos].nItemOrder=nPos;
		$(percents[nPos]).change(function z_z_z(){if(ValNumber(this,".",",","false",2)){insRCalLine(this.nItemOrder,"percent");insSummarize();}});
		amounts[nPos].nItemOrder=nPos;
		$(amounts[nPos]).change( function y_y_y(){if(ValNumber(this,".",",","false",2)){insRCalLine(this.nItemOrder,"transf");insSummarize();}});
   } 
}

function insCalAPVCapital(nCapital)
{
	var nApvCap=0;
    if (document.getElementsByName("cbeOption")[0].value=="1") //Opcion A
    {
        nApvCap=nCapital*.1;
    }
    else //Opcion B
    {
        nApvCap=nCapital;
	}
    document.getElementsByName("tcnCapitalAPV")[0].value=VTFormat(nApvCap, ".", ",", ".", 6, true);
    insSummarize();
}
</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();" ONLOAD="insRedefineEvents();insSummarize();">
<FORM METHOD="post" ID="FORM" NAME="frmSI004" ACTION="valCaseSeq.aspx?nMainAction=304">
<%

Call insShowValuesSI024()
%>
</FORM>
</BODY>
</HTML>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 11.33.47
Call mobjNetFrameWork.FinishPage("si024")
'UPGRADE_NOTE: Object mobjNetFrameWork may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




