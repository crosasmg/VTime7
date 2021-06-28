<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eProduct" %>
<script language="VB" runat="Server">

'^Begin Header Block VisualTimer Utility 1.1 31/3/03 19.42.04
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

'- Objeto para el manejo de las funciones generales de carga de valores
    Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues
Dim mclsProduct As eProduct.Product


'% Se cargan los campos de página	
'----------------------------------------------------------------------------------------------------------------------	
Public Sub insPreDP005()
	'----------------------------------------------------------------------------------------------------------------------	
	
	Dim lintTyp_cli As String
	Dim lintTyp_pol As String
	
	Call mclsProduct.Find(mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate))
	With mclsProduct
		
		'+	Si el tipo de domiciliacion es por cliente
		If .sTyp_dom = "1" Then
			lintTyp_cli = "1"
			lintTyp_pol = "2"
			'+	Si el tipo de domiciliacion es por poliza			
		Else
			If .sTyp_dom = "2" Then
				lintTyp_pol = "1"
				lintTyp_cli = "2"
			End If
		End If
		
Response.Write("    " & vbCrLf)
Response.Write("	<TABLE WIDTH=""100%"">" & vbCrLf)
Response.Write("		<TR>		     " & vbCrLf)
Response.Write("			<TD WIDTH=23%><LABEL ID=0>" & GetLocalResourceObject("tcnChUserLevCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("		    <TD WIDTH=23%>")


Response.Write(mobjValues.NumericControl("tcnChUserLev", 2, CStr(.nChUserLev),  , GetLocalResourceObject("tcnChUserLevToolTip"),  , 0,  ,  ,  ,  ,  , 1))


Response.Write("</TD>" & vbCrLf)
Response.Write("		    <TD>&nbsp;</TD>" & vbCrLf)
Response.Write("			<TD>&nbsp;</TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("		<TR>		     " & vbCrLf)
Response.Write("			<TD WIDTH=23%><LABEL ID=14301>" & GetLocalResourceObject("tcnCopyCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("		    <TD WIDTH=23%>")


Response.Write(mobjValues.NumericControl("tcnCopy", 4, CStr(.nCopies),  , GetLocalResourceObject("tcnCopyToolTip"),  , 0,  ,  ,  ,  ,  , 1))


Response.Write("</TD>" & vbCrLf)
Response.Write("		    <TD>&nbsp;</TD>" & vbCrLf)
Response.Write("			<TD COLSPAN=""3"" CLASS=""HighLighted""><LABEL ID=41251><A NAME=""Renovación de la póliza"">" & GetLocalResourceObject("AnchorRenovación de la pólizaCaption") & "</A></LABEL><hr></TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("		<TR>    " & vbCrLf)
Response.Write("		    <TD><LABEL ID=14312>" & GetLocalResourceObject("tcnTimeCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("		    <TD>")


            Response.Write(mobjValues.NumericControl("tcnTime", 4, CStr(.nDuration), , GetLocalResourceObject("tcnTimeToolTip"), , 0, , , , , , 2))


Response.Write("</TD>" & vbCrLf)
Response.Write("			<TD>&nbsp;</TD>" & vbCrLf)
Response.Write("		    <TD COLSPAN=""3"">")


            Response.Write(mobjValues.CheckControl("chkAutoreneaw", GetLocalResourceObject("chkAutoreneawCaption"), .sRenewal, "1", "InsChangeAutoreneaw(this);", , 19, GetLocalResourceObject("chkAutoreneawToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("		    <TD><LABEL ID=14299>" & GetLocalResourceObject("tcnCancelnoticeCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("		    <TD>")


Response.Write(mobjValues.NumericControl("tcnCancelnotice", 4, CStr(.nCancnoti),  , GetLocalResourceObject("tcnCancelnoticeToolTip"),  , 0,  ,  ,  ,  ,  , 3))


Response.Write("</TD>" & vbCrLf)
Response.Write("			<TD>&nbsp;</TD>" & vbCrLf)
Response.Write("		    <TD><LABEL ID=14302>" & GetLocalResourceObject("cbeGrouprenewCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			")

		If .sGroupind = "1" Or .sMultiind = "1" Then
		Else
			.sTimeren = "3"
		End If
		
Response.Write("" & vbCrLf)
Response.Write("			<TD COLSPAN=""2"">")


Response.Write(mobjValues.PossiblesValues("cbeGrouprenew", "Table25", eFunctions.Values.eValuesType.clngComboType, .sTimeren,  ,  ,  ,  ,  ,  , .sTimeren = "3",  , GetLocalResourceObject("cbeGrouprenewToolTip"),  , 20))


Response.Write("</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("		<TR>    " & vbCrLf)
Response.Write("		    <TD><LABEL ID=0>" & GetLocalResourceObject("tcnMonth_surrCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("		    <TD>")


Response.Write(mobjValues.NumericControl("tcnMonth_surr", 4, CStr(.nMonth_surr),  , GetLocalResourceObject("tcnMonth_surrToolTip"),  ,  ,  ,  ,  ,  ,  , 4))


Response.Write("</TD> " & vbCrLf)
Response.Write("			<TD>&nbsp;</TD>" & vbCrLf)
Response.Write("		    <TD CLASS=""HighLighted"" COLSPAN=""3""><LABEL ID=41252><A NAME=""Datos para la cobranza"">" & GetLocalResourceObject("AnchorDatos para la cobranzaCaption") & "</A></LABEL><HR></TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("		<TR>    " & vbCrLf)
Response.Write("		    <TD COLSPAN=""3"">")


Response.Write(mobjValues.CheckControl("chkDeclapolicy", GetLocalResourceObject("chkDeclapolicyCaption"), .sDeclaaut, "1",  ,  , 5, GetLocalResourceObject("chkDeclapolicyToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("		    <TD><LABEL ID=14304>" & GetLocalResourceObject("cbePayfrequenCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("		    <TD COLSPAN=""2"">")

		mobjValues.TypeList = 2
		mobjValues.List = "7"
		mobjValues.BlankPosition = False
		Response.Write(mobjValues.PossiblesValues("cbePayfrequen", "Table36", eFunctions.Values.eValuesType.clngComboType, CStr(.nPayfreq),  ,  ,  ,  ,  , "SetQuotas()",  , 5, GetLocalResourceObject("cbePayfrequenToolTip"),  , 21))
Response.Write("</TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("		    <TD WIDTH=23%><LABEL ID=14313>" & GetLocalResourceObject("cbeTypepolicyCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("		    <TD>")


            Response.Write(mobjValues.PossiblesValues("cbeTypepolicy", "Table17", eFunctions.Values.eValuesType.clngComboType, .sPolitype, , , , , , "SelectPolicyType(this.value);", , , GetLocalResourceObject("cbeTypepolicyToolTip"), , 6))


Response.Write("</TD>" & vbCrLf)
Response.Write("			<TD>&nbsp;</TD>" & vbCrLf)
Response.Write("		    <TD><LABEL ID=14306>" & GetLocalResourceObject("tcnQuotasCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("		    <TD COLSPAN=""2"">")


Response.Write(mobjValues.NumericControl("tcnQuotas", 4, CStr(.nQuota),  , GetLocalResourceObject("tcnQuotasToolTip"),  ,  ,  ,  ,  ,  ,  , 22))


Response.Write("</TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("		    <TD COLSPAN=""3"">")


Response.Write(mobjValues.CheckControl("chksLeg", GetLocalResourceObject("chksLegCaption"), .sLeg, "1",  , (.sGroupind = "2") Or (CStr(Session("sBrancht")) <> "1"), 7, GetLocalResourceObject("chksLegToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("			<TD><LABEL ID=0>" & GetLocalResourceObject("cbeway_payCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			<TD COLSPAN=""2"">")


Response.Write(mobjValues.PossiblesValues("cbeway_pay", "table5002", eFunctions.Values.eValuesType.clngComboType, CStr(.nway_pay),  ,  ,  ,  ,  ,  ,  , 5, GetLocalResourceObject("cbeway_payToolTip"),  , 23))


Response.Write("</TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("		    <TD><LABEL ID=0>" & GetLocalResourceObject("tcnNotCancelDayCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("		    <TD>")


Response.Write(mobjValues.NumericControl("tcnNotCancelDay", 4, CStr(mclsProduct.nNotCancelDay),  , GetLocalResourceObject("tcnNotCancelDayToolTip"),  ,  ,  ,  ,  ,  ,  , 8))


Response.Write("</TD> " & vbCrLf)
Response.Write("			<TD>&nbsp;</TD>" & vbCrLf)
Response.Write("			<TD><LABEL ID=0>" & GetLocalResourceObject("tcnbill_dayCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			<TD COLSPAN=""2"">")


Response.Write(mobjValues.NumericControl("tcnbill_day", 4, CStr(.nBill_day),  , GetLocalResourceObject("tcnbill_dayToolTip"),  ,  ,  ,  ,  ,  ,  , 24))


Response.Write("</TD>                " & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD COLSPAN=""3"">&nbsp;</TD>" & vbCrLf)
Response.Write("			<TD COLSPAN=""2""><LABEL ID=0>" & GetLocalResourceObject("tcnQDays_DifQuoCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			<TD>")


Response.Write(mobjValues.NumericControl("tcnQDays_DifQuo", 2, CStr(.nQDays_DifQuo),  , GetLocalResourceObject("tcnQDays_DifQuoToolTip"),  ,  ,  ,  ,  ,  ,  , 24))


Response.Write("</TD>                " & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("		    <TD COLSPAN = ""3"">&nbsp;</TD>" & vbCrLf)
Response.Write("			<TD><LABEL ID=0>" & GetLocalResourceObject("AnchorCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			<TD>")

		
		If .sTyp_dom = vbNullString Then
			Response.Write(mobjValues.OptionControl(0, "optTyp_dom", GetLocalResourceObject("optTyp_dom_1Caption"), "1", "1",  ,  , 25, GetLocalResourceObject("optTyp_dom_1ToolTip")))
		Else
			Response.Write(mobjValues.OptionControl(0, "optTyp_dom", GetLocalResourceObject("optTyp_dom_1Caption"), lintTyp_cli, "1",  ,  , 25, GetLocalResourceObject("optTyp_dom_1ToolTip")))
		End If
		
Response.Write("</TD>" & vbCrLf)
Response.Write("			<TD>")


Response.Write(mobjValues.OptionControl(0, "optTyp_dom", GetLocalResourceObject("optTyp_dom_2Caption"), lintTyp_pol, "2",  ,  , 26, GetLocalResourceObject("optTyp_dom_2ToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD COLSPAN=""2"">")


Response.Write(mobjValues.CheckControl("chksFirst_pay", GetLocalResourceObject("chksFirst_payCaption"), .sFirst_pay, "1",  ,  , 11, GetLocalResourceObject("chksFirst_payToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("		    <TD>&nbsp;</TD>" & vbCrLf)
Response.Write("		    <TD><LABEL ID=14303>" & GetLocalResourceObject("cbeInvoceholdCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("		    <TD COLSPAN=""2"">")


Response.Write(mobjValues.PossiblesValues("cbeInvocehold", "Table45", eFunctions.Values.eValuesType.clngComboType, .sHolder,  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeInvoceholdToolTip"),  , 27))


Response.Write("</TD>" & vbCrLf)
Response.Write("		    " & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD COLSPAN=""2"">")


Response.Write(mobjValues.CheckControl("chksDatecoll", GetLocalResourceObject("chksDatecollCaption"), .sDatecoll, "1",  ,  , 12, GetLocalResourceObject("chksDatecollToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("            <TD>&nbsp;</TD>" & vbCrLf)
Response.Write("			<TD COLSPAN=""3"">&nbsp;</TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD><LABEL ID=0>" & GetLocalResourceObject("cbeReinsuranCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("		    <TD>")


Response.Write(mobjValues.PossiblesValues("cbeReinsuran", "Table8", eFunctions.Values.eValuesType.clngComboType, .sReintype,  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeReinsuranToolTip"),  , 13))


Response.Write("</TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD COLSPAN=""2"">")


Response.Write(mobjValues.CheckControl("chksTarQuo_Ind", GetLocalResourceObject("chksTarQuo_IndCaption"), .sTarQuo_Ind, "1",  ,  ,  , GetLocalResourceObject("chksTarQuo_IndToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("		    <TD>&nbsp;</TD>" & vbCrLf)
Response.Write("            <TD CLASS=""HighLighted"" COLSPAN=""3""><LABEL ID=0><A NAME=""Moneda del recibo"">" & GetLocalResourceObject("AnchorMoneda del reciboCaption") & "</A></LABEL><HR></TD>   			" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD><LABEL ID=0>" & GetLocalResourceObject("tcnQdays_quoCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("		    <TD>")


Response.Write(mobjValues.NumericControl("tcnQdays_quo", 4, CStr(.nQdays_quo),  , GetLocalResourceObject("tcnQdays_quoToolTip"),  ,  ,  ,  ,  ,  ,  , 14))


Response.Write("</TD> " & vbCrLf)
Response.Write("		    <TD>&nbsp;</TD>" & vbCrLf)
Response.Write("			<TD><LABEL ID=14305>" & GetLocalResourceObject("cbePremiumCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			<TD COLSPAN=""2"">")


Response.Write(mobjValues.PossiblesValues("cbePremium", "Table161", eFunctions.Values.eValuesType.clngComboType, .sStyle_prem,  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbePremiumToolTip"),  , 28))


Response.Write("</TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD><LABEL ID=0>" & GetLocalResourceObject("tcnQdays_proCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("		    <TD>")


Response.Write(mobjValues.NumericControl("tcnQdays_pro", 5, CStr(.nQdays_pro),  , GetLocalResourceObject("tcnQdays_proToolTip"),  ,  ,  ,  ,  ,  ,  , 15))


Response.Write("</TD> " & vbCrLf)
Response.Write("		    <TD>&nbsp;</TD>		    " & vbCrLf)
Response.Write("			<TD><LABEL ID=41255>" & GetLocalResourceObject("cbeTaxesCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			<TD COLSPAN=""2"">")


Response.Write(mobjValues.PossiblesValues("cbeTaxes", "Table161", eFunctions.Values.eValuesType.clngComboType, .sStyle_tax,  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeTaxesToolTip"),  , 29))


Response.Write("</TD>" & vbCrLf)
Response.Write("		</TR>	" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD COLSPAN=""2"">")


Response.Write(mobjValues.CheckControl("chkSetprem", GetLocalResourceObject("chkSetpremCaption"), .sSetprem, "2", "InsClickField(this)",  , 16, GetLocalResourceObject("chkSetpremToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("			<TD>&nbsp;</TD>" & vbCrLf)
Response.Write("			<TD><LABEL ID=41258>" & GetLocalResourceObject("cbeCommissionCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			<TD COLSPAN=""2"">")


Response.Write(mobjValues.PossiblesValues("cbeCommission", "Table161", eFunctions.Values.eValuesType.clngComboType, .sStyle_comm,  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeCommissionToolTip"),  , 30))


Response.Write("</TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD><LABEL ID=41257>" & GetLocalResourceObject("tcnMonth_SetprCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("		    <TD>")


Response.Write(mobjValues.NumericControl("tcnMonth_Setpr", 2, CStr(.nMonth_Setpr),  , GetLocalResourceObject("tcnMonth_SetprToolTip"),  , 0,  ,  ,  ,  , .sSetprem <> "1", 17))


Response.Write("</TD>" & vbCrLf)
Response.Write("			<TD>&nbsp;</TD>" & vbCrLf)
Response.Write("			<TD COLSPAN=""3"">")


Response.Write(mobjValues.CheckControl("chkFracReceip", GetLocalResourceObject("chkFracReceipCaption"), .sFracReceip, "1",  ,  , 31, GetLocalResourceObject("chkFracReceipToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD WIDTH=""35%"">")


Response.Write(mobjValues.CheckControl("chkRecSec", GetLocalResourceObject("chkRecSecCaption"), .sRecSec, "1",  ,  , 18, GetLocalResourceObject("chkRecSecToolTip")))


            Response.Write("</TD>" & vbCrLf)
            Response.Write("			<TD>&nbsp;</TD>" & vbCrLf)
            Response.Write("			<TD>")
            Response.Write("		    <TD><LABEL ID=0>" & GetLocalResourceObject("tctRou_warning_chargCaption") & "</LABEL></TD>" & vbCrLf)
            Response.Write("		    <TD>")


            Response.Write(mobjValues.TextControl("tctRou_warning_charg", 12, .sRou_warning_charg, , GetLocalResourceObject("tctRou_warning_chargToolTip")))


            Response.Write("</TD>" & vbCrLf)

            Response.Write("		</TR>" & vbCrLf)

            mobjValues.BlankPosition = True
            Response.Write("		<TR>" & vbCrLf)
            Response.Write("		    <TD><LABEL ID=0>" & GetLocalResourceObject("tctCurrencyCaption") & "</LABEL></TD>" & vbCrLf)
            Response.Write("		    <TD>")

            Response.Write(mobjValues.PossiblesValues("cbeCurr_receipt", "Table11", eFunctions.Values.eValuesType.clngComboType, .nCurr_receipt, , , , , , , , , GetLocalResourceObject("cbeCurrencyToolTip"), , 35))
            
            Response.Write("</TD>" & vbCrLf)
            Response.Write("			<TD>")
            Response.Write("		    <TD><LABEL ID=0>" & GetLocalResourceObject("tctRou_coverCaption") & "</LABEL></TD>" & vbCrLf)
            Response.Write("		    <TD>")


            Response.Write(mobjValues.TextControl("tctRou_cover", 12, .sRou_cover, , GetLocalResourceObject("tctRou_coverCaptionToolTip")))


            Response.Write("</TD>" & vbCrLf)
            
            'Response.Write("		<TR>" & vbCrLf)
            'Response.Write("		    <TD><LABEL ID=0>" & GetLocalResourceObject("tcnPayableCaption") & "</LABEL></TD>" & vbCrLf)
            'Response.Write("		    <TD>")


            'Response.Write(mobjValues.NumericControl("tcnPayable", 4, CStr(mclsProduct.nPayable),  , GetLocalResourceObject("tcnPayableToolTip"),  ,  ,  ,  ,  ,  ,  , 22))
            Response.Write(mobjValues.HiddenControl("tcnPayable", mclsProduct.nPayable))

            'Response.Write("</TD> 		" & vbCrLf)
            'Response.Write("			<TD>&nbsp;</TD>		    " & vbCrLf)
            'Response.Write("		    <TD><LABEL ID=0>" & GetLocalResourceObject("tcnAdvanceCaption") & "</LABEL></TD>" & vbCrLf)
            'Response.Write("		    <TD>")


            'Response.Write(mobjValues.NumericControl("tcnAdvance", 4, CStr(mclsProduct.nAdvance),  , GetLocalResourceObject("tcnAdvanceToolTip"),  ,  ,  ,  ,  ,  ,  , 32))
            Response.Write(mobjValues.HiddenControl("tcnAdvance", mclsProduct.nAdvance))

            'Response.Write("</TD> 					" & vbCrLf)
            'Response.Write("		</TR>		" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("		    <TD CLASS=""HighLighted"" COLSPAN=""2""><LABEL ID=41253><A NAME=""Revalorización de la póliza"">" & GetLocalResourceObject("AnchorRevalorización de la pólizaCaption") & "</A></LABEL><HR></TD>" & vbCrLf)
Response.Write("			<TD>&nbsp;</TD>" & vbCrLf)
Response.Write("			")

		If CStr(Session("sBrancht")) <> "1" Then
Response.Write("" & vbCrLf)
Response.Write("			<TD CLASS=""HighLighted"" COLSPAN=""3""><LABEL>" & GetLocalResourceObject("Anchor2Caption") & "</LABEL><HR></TD>" & vbCrLf)
Response.Write("			")

		End If
Response.Write("" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("		    <TD><LABEL ID=41254>" & GetLocalResourceObject("cbeRevalformCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("		    <TD>")


Response.Write(mobjValues.PossiblesValues("cbeRevalform", "Table154", eFunctions.Values.eValuesType.clngComboType, .sRevalapl,  ,  ,  ,  ,  , "SetRevalForm()",  ,  , GetLocalResourceObject("cbeRevalformToolTip"),  , 23))


Response.Write("</TD>" & vbCrLf)
Response.Write("			<TD>&nbsp;</TD>" & vbCrLf)
Response.Write("			")

		If CStr(Session("sBrancht")) <> "1" Then
Response.Write("" & vbCrLf)
Response.Write("			<TD COLSPAN=""3"">")


Response.Write(mobjValues.CheckControl("chkRetarif", GetLocalResourceObject("chkRetarifCaption"), .sRetarif, "1",  ,  , 23, GetLocalResourceObject("chkRetarifToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("			")

		Else
Response.Write("" & vbCrLf)
Response.Write("			<TD COLSPAN=""3"">")


Response.Write(mobjValues.HiddenControl("chkRetarif", "2"))


Response.Write("</TD>" & vbCrLf)
Response.Write("			")

		End If
Response.Write("" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD><LABEL ID=41256>" & GetLocalResourceObject("cbeRevalTypeCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			<TD>")


Response.Write(mobjValues.PossiblesValues("cbeRevalType", "Table46", eFunctions.Values.eValuesType.clngComboType, .sRevaltyp,  ,  ,  ,  ,  , "SetRevalType()", True,  , GetLocalResourceObject("cbeRevalTypeToolTip"),  , 24))


Response.Write("</TD>" & vbCrLf)
Response.Write("			<TD>&nbsp;</TD>" & vbCrLf)
Response.Write("			<TD COLSPAN=""3"">&nbsp;</TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD><LABEL ID=41257>" & GetLocalResourceObject("tcnrevalfactorCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			<TD>")


Response.Write(mobjValues.NumericControl("tcnrevalfactor", 4, CStr(.nRevalrat),  , GetLocalResourceObject("tcnrevalfactorToolTip"),  , 2,  ,  ,  ,  , True, 25))


Response.Write("</TD>" & vbCrLf)
Response.Write("			<TD>&nbsp;</TD>" & vbCrLf)
Response.Write("			<TD COLSPAN=""3"">&nbsp;</TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("		    <TD CLASS=""HighLighted"" COLSPAN=""2""><LABEL ID=0><A NAME=""Rehabilitación"">" & GetLocalResourceObject("AnchorRehabilitaciónCaption") & "</A></LABEL><HR></TD>" & vbCrLf)
Response.Write("			<TD>&nbsp;</TD>" & vbCrLf)
Response.Write("			<TD CLASS=""HighLighted"" COLSPAN=""2""><LABEL ID=0><A NAME=""Reactivación"">" & GetLocalResourceObject("AnchorReactivaciónCaption") & "</A></LABEL><HR></TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD COLSPAN=""2"">" & vbCrLf)
Response.Write("				")

		If .sReinst = vbNullString Then
			Response.Write(mobjValues.CheckControl("chksReinst", GetLocalResourceObject("chksReinstCaption"), CStr(1), "1", "SetReinst()",  , 9, GetLocalResourceObject("chksReinstToolTip")))
		Else
			Response.Write(mobjValues.CheckControl("chksReinst", GetLocalResourceObject("chksReinstCaption"), .sReinst, "1", "SetReinst()",  , 9, GetLocalResourceObject("chksReinstToolTip")))
		End If
		
Response.Write("" & vbCrLf)
Response.Write("			</TD>" & vbCrLf)
Response.Write("			<TD>&nbsp;</TD>" & vbCrLf)
Response.Write("			<TD COLSPAN=""2"">" & vbCrLf)
Response.Write("				")

		If .sReactivation = vbNullString Then
			Response.Write(mobjValues.CheckControl("chkReactivation", GetLocalResourceObject("chkReactivationCaption"),  , "1", "SetReinst()",  , 9, GetLocalResourceObject("chkReactivationToolTip")))
		Else
			Response.Write(mobjValues.CheckControl("chkReactivation", GetLocalResourceObject("chkReactivationCaption"), .sReactivation, "1", "SetReinst()",  , 9, GetLocalResourceObject("chkReactivationToolTip")))
		End If
		
Response.Write("" & vbCrLf)
Response.Write("			</TD>  " & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD><LABEL ID=0>" & GetLocalResourceObject("tcnRehabperiodCaption") & " </LABEL></TD>" & vbCrLf)
Response.Write("		    <TD>")


Response.Write(mobjValues.NumericControl("tcnRehabperiod", 4, CStr(.nRehabperiod),  , GetLocalResourceObject("tcnRehabperiodToolTip"),  ,  ,  ,  ,  ,  ,  , 10))


Response.Write("</TD>  " & vbCrLf)
Response.Write("		    <TD>&nbsp;</TD>" & vbCrLf)
Response.Write("		    <TD><LABEL ID=0>" & GetLocalResourceObject("tcnReactPeriodCaption") & " </LABEL></TD>" & vbCrLf)
Response.Write("		    <TD>")


Response.Write(mobjValues.NumericControl("tcnReactPeriod", 4, CStr(.nReactPeriod),  , GetLocalResourceObject("tcnReactPeriodToolTip"),  ,  ,  ,  ,  ,  ,  , 10))


Response.Write("</TD>  " & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD><LABEL ID=0>" & GetLocalResourceObject("tcnRehabperiod_autCaption") & " </LABEL></TD>" & vbCrLf)
Response.Write("		    <TD>")


Response.Write(mobjValues.NumericControl("tcnRehabperiod_aut", 4, CStr(.nRehabperiod_aut),  , GetLocalResourceObject("tcnRehabperiod_autToolTip"),  ,  ,  ,  ,  ,  ,  , 10))


Response.Write("</TD>  " & vbCrLf)
Response.Write("		    <TD>&nbsp;</TD>" & vbCrLf)
Response.Write("		    <TD><LABEL ID=0>" & GetLocalResourceObject("tcnReactPeriod_AutCaption") & " </LABEL></TD>" & vbCrLf)
Response.Write("		    <TD>")


Response.Write(mobjValues.NumericControl("tcnReactPeriod_Aut", 4, CStr(.nReactPeriod_Aut),  , GetLocalResourceObject("tcnReactPeriod_AutToolTip"),  ,  ,  ,  ,  ,  ,  , 10))


Response.Write("</TD>  " & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("			<TD><LABEL ID=0>" & GetLocalResourceObject("tctRehaut_rCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("		    <TD>")


Response.Write(mobjValues.TextControl("tctRehaut_r", 12, .sRoutaut_r,  , GetLocalResourceObject("tctRehaut_rToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("		    <TD>&nbsp;</TD>" & vbCrLf)
Response.Write("		    <TD><LABEL ID=0>" & GetLocalResourceObject("tctRoutReactCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("		    <TD>")


Response.Write(mobjValues.TextControl("tctRoutReact", 12, .sRoutReact,  , GetLocalResourceObject("tctRoutReactToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("	</TABLE>")

	End With
	
	With Response
		If Not Session("bQuery") Then
			.Write("<SCRIPT>")
			.Write("SetQuotas();")
			.Write("SetRevalForm();")
			.Write("SetRevalType();")
			.Write("SetReinst();")
			.Write("</" & "Script>")
		End If
		
		.Write(mobjValues.HiddenControl("tctIndivind", mclsProduct.sIndivind))
		.Write(mobjValues.HiddenControl("tctGroupind", mclsProduct.sGroupind))
		.Write(mobjValues.HiddenControl("tctMultiind", mclsProduct.sMultiind))
		
		'+ Se evalúa la propiedad "nCopies" de la clase Product.
		'+ Si el valor de "nCopies" es igual a cero (0) quiere decir que no existen valores y 
		'+ Se deben registrar Acción 301 (clngActionadd), en caso contrario, se efectúa una modificación
		'+ Acción 302 (clngActionUpdate) - ACM - 04/04/2001
		If mclsProduct.nCopies = 0 Then
			.Write(mobjValues.HiddenControl("tcnAction", CStr(301)))
		Else
			.Write(mobjValues.HiddenControl("tcnAction", CStr(302)))
		End If
	End With
	
	mclsProduct = Nothing
End Sub

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("DP005")

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.42.04
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "DP005"

mclsProduct = New eProduct.Product

mobjValues.ActionQuery = Session("bQuery")
%>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT>

//- Variable para el control de versiones
    document.VssVersion="$$Revision: 8 $|$$Date: 13/10/04 12:12 $|$$Author: Nvaplat28 $"

//% Asigna el valor de las variables para el tipo de póliza
//----------------------------------------------------------------------------------------
function SelectPolicyType(valor){
//----------------------------------------------------------------------------------------
	
	if(typeof(valor)!='undefined')
	{
		switch (valor)
		{
			case "1": // Pólizas Individuales
				self.document.forms[0].elements["tctIndivind"].value = "1"
			case "2": // Pólizas Colectivas
				self.document.forms[0].elements["tctGroupind"].value = "1"
			case "3": // Pólizas de Multilocalidad
				self.document.forms[0].elements["tctMultiind"].value = "1"
		}
	}
	else
	{
		switch (valor)
		{
			case 1: // Pólizas Individuales
				self.document.forms[0].elements["tctIndivind"].value = "1"
			case 2: // Pólizas Colectivas
				self.document.forms[0].elements["tctGroupind"].value = "1"
			case 3: // Pólizas de Multilocalidad
				self.document.forms[0].elements["tctMultiind"].value = "1"
		}
			
	}
}

//% SetQuotas : Habilita el campo de cuotas y actualiza su valor sólo cuando la frecuencia
//%             de pago sea por cuotas
//----------------------------------------------------------------------------------------
function SetQuotas() {
//----------------------------------------------------------------------------------------

    with (self.document.forms[0]){
        if (cbePayfrequen.value==8){ //cuotas
            tcnQuotas.disabled = false;
            tcnQDays_DifQuo.disabled = false;
        }            
        else{
            tcnQuotas.disabled = true;
            tcnQuotas.value = 0;

            tcnQDays_DifQuo.disabled = true;
            tcnQDays_DifQuo.value = 0;
        }
    }
}

//% SetRevalForm : Establece el estado del tipo de revalorización de la póliza cuando
//%                cambia la forma de revalorización.
//----------------------------------------------------------------------------------------
function SetRevalForm(){
//----------------------------------------------------------------------------------------
    with (self.document.forms[0]){
        if (cbeRevalform.value==3){
            cbeRevalType.value=4;
            cbeRevalType.disabled=true;
            tcnrevalfactor.disabled=true;
            tcnrevalfactor.value=0;
            }
        else{
            cbeRevalType.disabled=false;
        }
    }
}
	
//% SetRevalPol : Establece el estado del factor de revalorización de la póliza cuando
//%               cambia el tipo de revalorización.
//----------------------------------------------------------------------------------------
function SetRevalType(){
//----------------------------------------------------------------------------------------
    with (self.document.forms[0]){    
        if(cbeRevalType.value==3)
            tcnrevalfactor.disabled=false;
        else{
            tcnrevalfactor.disabled=true;
            tcnrevalfactor.value=0;
        }
    }
}

//% SetReinst : Establece el estado del 'Período de rehabilitación' cuando
//%             cambia el estado de 'Permite rehabilitar'
//----------------------------------------------------------------------------------------
function SetReinst(){
//----------------------------------------------------------------------------------------
    
    with (self.document.forms[0]){    
        if(chksReinst.checked== true){
            tcnRehabperiod.disabled=false;
            tcnRehabperiod_aut.disabled=false;
        }
        else{
            tcnRehabperiod.value= "";
            tcnRehabperiod_aut.value= "";
            tcnRehabperiod.disabled=true;
            tcnRehabperiod_aut.disabled=true;
        }    
    }
}
//% InsClickField: Se cambia el valor del objeto checkbox al hacer click 
//------------------------------------------------------------------------------------------- 
function InsClickField(objField){	
//------------------------------------------------------------------------------------------- 
    with (self.document.forms[0]){    
		if (objField.checked == true) 
			objField.value = "1" 
		else 
			objField.value = "2" 
		if (objField.name == "chkSetprem")
			if (objField.value == "1")  
		        tcnMonth_Setpr.disabled=false;
		    else{
		        tcnMonth_Setpr.disabled=true;
		        tcnMonth_Setpr.value= "";
			} 
	} 
}

//% InsChangeAutoreneaw: Si se marca la opcion de renovación Automática y la selección del tipo de poiza es colectiva
//------------------------------------------------------------------------------------------- 
function InsChangeAutoreneaw(objField) {
    //------------------------------------------------------------------------------------------- 
    with (self.document.forms[0]) {
        if (objField.checked == true)
            cbeGrouprenew.disabled = false;
        else
            cbeGrouprenew.disabled = true;
        
    }
}


</SCRIPT>
<HTML>
<HEAD>


    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">
<%
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.42.04
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility
With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjValues.WindowsTitle("DP005"))
	.Write(mobjMenu.setZone(2, "DP005", "DP005.aspx"))
End With
mobjMenu = Nothing
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<%=mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl"))%> 	
<FORM METHOD="POST" ID="FORM" NAME="frmDP005" ACTION="valProductSeq.aspx?nMainAction=<%=Request.QueryString.Item("nMainAction")%>">
    <A NAME="BeginPage"></A>
    <P ALIGN="Center">
		<LABEL ID=41246><A HREF="#Renovación de la póliza"> <%= GetLocalResourceObject("AnchorRenovación de la póliza2Caption") %></A></LABEL><LABEL ID=0> | </LABEL>
		<LABEL ID=41248><A HREF="#Moneda del recibo"> <%= GetLocalResourceObject("AnchorMoneda del recibo2Caption") %></A></LABEL><LABEL ID=0> | </LABEL>
		<LABEL ID=41250><A HREF="#Revalorización de la póliza"> <%= GetLocalResourceObject("AnchorRevalorización de la póliza2Caption") %></A></LABEL><LABEL ID=0> | </LABEL>
		<LABEL ID=0><A HREF="#Datos para la cobranza"> <%= GetLocalResourceObject("AnchorDatos para la cobranza2Caption") %></A></LABEL>
    </P>
   
<%
Call insPreDP005()
%>		
    <P ALIGN="Center">
		<A HREF="#BeginPage">
		<%
Response.Write(mobjValues.BeginPageButton)
mobjValues = Nothing
%>
		</A>
    </P>
<%'^Begin Footer Block VisualTimer Utility 1.1 31/3/03 19.42.04
Call mobjNetFrameWork.FinishPage("DP005")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer
%>
</FORM>
</BODY>
</HTML>





