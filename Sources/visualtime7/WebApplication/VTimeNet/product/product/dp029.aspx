<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eGeneral" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eProduct" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de la página.
Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues
Dim lstrAction As Object
Dim mstrError As String

Dim mobjGeneral As eGeneral.GeneralFunction


'% InsPreDP029: se controla la carga de la página
'--------------------------------------------------------------------------------------------
Sub InsPreDP029()
	'--------------------------------------------------------------------------------------------
	Dim lclsTab_GenCov As eProduct.Tab_gencov
	Dim lclsGeneral As eGeneral.GeneralFunction
	Dim nFields As String
	
	Response.Write(mobjValues.ShowWindowsName("DP018G"))
	
	lclsTab_GenCov = New eProduct.Tab_gencov
	lclsGeneral = New eGeneral.GeneralFunction
	
	lclsTab_GenCov.Find(mobjValues.StringToType(Session("nCover"), eFunctions.Values.eTypeData.etdDouble))
	nFields = lclsGeneral.insPrecision("tab_gencov", "sdescript")
	
Response.Write("" & vbCrLf)
Response.Write("<FORM METHOD=""post"" ID=""FORM"" NAME=""frmDP029"" ACTION=""valCoverSeq.aspx?nMainAction=")


Response.Write(Request.QueryString.Item("nMainAction"))


Response.Write(""">" & vbCrLf)
Response.Write("    <P ALIGN=""Center"">" & vbCrLf)
Response.Write("        <LABEL ID=100369><A HREF=""#Ramos"">" & GetLocalResourceObject("AnchorRamosCaption") & "</A></LABEL>" & vbCrLf)
Response.Write("	</P>" & vbCrLf)
Response.Write("    <TABLE WIDTH=""100%"">" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD><LABEL ID=14073>" & GetLocalResourceObject("tctDescriptCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD COLSPAN=""4"">")


Response.Write(mobjValues.TextAreaControl("tctDescript", 2, 60, lclsTab_GenCov.sDescript,  , GetLocalResourceObject("tctDescriptToolTip"),  ,  ,  , "ValidateLength(this," & nFields & ");"))


Response.Write("</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD><LABEL ID=14074>" & GetLocalResourceObject("tctShortDesCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD COLSPAN=""4"">")


Response.Write(mobjValues.TextControl("tctShortDes", 30, lclsTab_GenCov.sShort_des,  , GetLocalResourceObject("tctShortDesToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD><LABEL ID=14076>" & GetLocalResourceObject("cbeCurrencyCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.PossiblesValues("cbeCurrency", "table11", eFunctions.Values.eValuesType.clngComboType, CStr(lclsTab_GenCov.nCurrency),  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeCurrencyToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("            <TD>&nbsp;</TD>" & vbCrLf)
Response.Write("            <TD><LABEL ID=14074>" & GetLocalResourceObject("tctsCondSVSCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.TextControl("tctsCondSVS", 30, lclsTab_GenCov.sCondSVS,  , GetLocalResourceObject("tctsCondSVSToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD COLSPAN=""5"">")


Response.Write(mobjValues.CheckControl("chksInforProv", GetLocalResourceObject("chksInforProvCaption"), lclsTab_GenCov.sInforProv, "1", "InsChangeField()",  ,  , GetLocalResourceObject("chksInforProvToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD><LABEL ID=14074>" & GetLocalResourceObject("tctsProviderCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD COLSPAN=""4"">")


Response.Write(mobjValues.ClientControl("tctsProvider", lclsTab_GenCov.sProvider, True, GetLocalResourceObject("tctsProviderToolTip"),  , True, "lblCliename",  ,  ,  ,  ,  , 4, True))


Response.Write("</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD COLSPAN=""5"" CLASS=""HighLighted""><LABEL ID=100370>" & GetLocalResourceObject("AnchorCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD COLSPAN=""5"" CLASS=""HorLine""></TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD COLSPAN=""3"">")


Response.Write(mobjValues.CheckControl("chkAutomaticRep", GetLocalResourceObject("chkAutomaticRepCaption"), lclsTab_GenCov.sAutomrep,  ,  ,  ,  , GetLocalResourceObject("chkAutomaticRepToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("            <TD><LABEL ID=14135>" & GetLocalResourceObject("tcnMediumValueCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.NumericControl("tcnMediumValue", 18, CStr(lclsTab_GenCov.nMedreser),  , GetLocalResourceObject("tcnMediumValueToolTip"), True, 6))


Response.Write("</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD COLSPAN=""3"">&nbsp;</TD>" & vbCrLf)
Response.Write("            <TD><LABEL ID=14136>" & GetLocalResourceObject("tctReserveRouCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.TextControl("tctReserveRou", 12, lclsTab_GenCov.sRoureser,  , GetLocalResourceObject("tctReserveRouToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD COLSPAN=""5"">&nbsp;</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD COLSPAN=""5"" CLASS=""HighLighted""><LABEL ID=100371><A NAME=""Ramos"">" & GetLocalResourceObject("AnchorRamos2Caption") & "</A></LABEL></TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD COLSPAN=""5"" CLASS=""HorLine""></TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD><LABEL ID=14077>" & GetLocalResourceObject("cbeBranch_ledCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.PossiblesValues("cbeBranch_led", "Table75", eFunctions.Values.eValuesType.clngComboType, CStr(lclsTab_GenCov.nBranch_led),  ,  ,  ,  ,  ,  ,  ,  , ""))


Response.Write("</TD>" & vbCrLf)
Response.Write("            <TD>&nbsp;</TD>" & vbCrLf)
Response.Write("            <TD><LABEL ID=14078>" & GetLocalResourceObject("cbeBranch_reiCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.PossiblesValues("cbeBranch_rei", "table5000", eFunctions.Values.eValuesType.clngComboType, CStr(lclsTab_GenCov.nBranch_rei),  ,  ,  ,  ,  ,  ,  ,  , ""))


Response.Write("</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD><LABEL ID=0>" & GetLocalResourceObject("cbeBranch_estCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.PossiblesValues("cbeBranch_est", "table71", eFunctions.Values.eValuesType.clngComboType, CStr(lclsTab_GenCov.nBranch_est),  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeBranch_estToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("            <TD>&nbsp;</TD>" & vbCrLf)
Response.Write("            <TD><LABEL ID=14080>" & GetLocalResourceObject("cbeBranch_genCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.PossiblesValues("cbeBranch_gen", "table634", eFunctions.Values.eValuesType.clngComboType, CStr(lclsTab_GenCov.nBranch_gen),  ,  ,  ,  ,  ,  ,  ,  , ""))


Response.Write("</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD COLSPAN=""5"">")


Response.Write(mobjValues.CheckControl("chkRisk", GetLocalResourceObject("chkRiskCaption"), lclsTab_GenCov.sRisk, "1",  ,  ,  , GetLocalResourceObject("chkRiskToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("    </TABLE>" & vbCrLf)
Response.Write("	")


Response.Write(mobjValues.BeginPageButton)


Response.Write("" & vbCrLf)
Response.Write("</FORM>")

	
	lclsTab_GenCov = Nothing
End Sub

</script>
<%Response.Expires = -1
mobjGeneral = New eGeneral.GeneralFunction

mobjValues = New eFunctions.Values
lstrAction = Request.QueryString.Item("nMainAction")
mobjValues.ActionQuery = lstrAction = eFunctions.Menues.TypeActions.clngActionQuery Or lstrAction = eFunctions.Menues.TypeActions.clngActionDuplicate Or lstrAction = eFunctions.Menues.TypeActions.clngActionCut
Response.Write(mobjValues.StyleSheet())
mobjMenu = New eFunctions.Menues

mstrError = mobjGeneral.insLoadMessage(55892)

Call InsPreDP029()

mobjValues.sCodisplPage = "dp029"
%>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/ValFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></SCRIPT>




<%
Response.Write(mobjMenu.setZone(2, "DP029", "DP029.aspx"))
mobjMenu = Nothing
%>
<SCRIPT>
//- Variable para el control de versiones
	document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 16:56 $|$$Author: Nvaplat61 $"
       
//% insShowHeader: Recarga los campos del encabezado       
//-------------------------------------------------------------------------------------------
function insShowHeader(){
//-------------------------------------------------------------------------------------------
    var lblnAgain = true
    if (typeof(top.fraHeader.document)!='undefined')
        if (typeof(top.fraHeader.document.forms[0])!='undefined')
            if (typeof(top.fraHeader.document.forms[0].valCover)!='undefined'){
		        top.fraHeader.document.forms[0].valCover.value='<%=Session("nCover")%>';
		        top.fraHeader.$('#valCover').change();
                lblnAgain = false         
            }
   if (lblnAgain)
      setTimeout("insShowHeader",50)
}

//% ValidateLength: Se encarga de enviar un error en caso de que se ingrese una cantidad mayor
//% de caracteres para el campo descripción de la que está definida en la B.D.
//--------------------------------------------------------------------------------------------
function ValidateLength(Field, nMaxAllowed){
//--------------------------------------------------------------------------------------------
	if (self.document.forms[0].tctDescript.value.length > nMaxAllowed) {
		alert("55892: " + "<%=mstrError%>")
		Field.focus();
	}
}

//% InsChangeField: se controla los parámetros del campo producto.
//--------------------------------------------------------------------------------------------
function InsChangeField(){
//--------------------------------------------------------------------------------------------
	with (self.document.forms[0]){
		tctsProvider.disabled = !chksInforProv.checked;
		tctsProvider_Digit.disabled = tctsProvider.disabled;
		if (tctsProvider.disabled==true) {
	       tctsProvider.value = '';
	       tctsProvider_Digit.value = '';
	       UpdateDiv('lblCliename','');
		}
	}
}
	insShowHeader();
</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<%
'+ Si la acción es diferente a Consulta o Duplicar	
If CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) <> 401 And CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) <> 306 Then
	Response.Write("<SCRIPT>InsChangeField();</SCRIPT>")
End If
%>
</BODY>
</HTML>




