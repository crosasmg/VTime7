<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eCollection" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 10.44.07
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility
'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
Dim mobjGrid As eFunctions.Grid
Dim mobjMenu As eFunctions.Menues


'% insPreCOC003: Se cargan los valores de la página
'--------------------------------------------------------------------------------------------
Private Sub insPreCOC003()
	'--------------------------------------------------------------------------------------------
	Dim lclsPremium As eCollection.Premium
	lclsPremium = New eCollection.Premium
	
	If lclsPremium.Find_Receipt_COC003(mobjValues.StringToType(Request.QueryString.Item("nReceipt"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble)) Then
	End If
	
	
Response.Write("  " & vbCrLf)
Response.Write("<TABLE WIDTH=""100%"">         " & vbCrLf)
Response.Write("	<TR>" & vbCrLf)
Response.Write("	    <TD WIDTH=""32%"">" & vbCrLf)
Response.Write("			")


Response.Write(mobjValues.FIELDSET(999, "Vigencia"))


Response.Write("" & vbCrLf)
Response.Write("			&nbsp;&nbsp;<LABEL ID=10521>" & GetLocalResourceObject("tcdEffecdateCaption") & "</LABEL>&nbsp;&nbsp;" & vbCrLf)
Response.Write("			")


Response.Write(mobjValues.DateControl("tcdEffecdate", CStr(lclsPremium.dEffecdate),  , GetLocalResourceObject("tcdEffecdateToolTip"),  ,  ,  ,  , True))


Response.Write("		    " & vbCrLf)
Response.Write("			&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" & vbCrLf)
Response.Write("			&nbsp;&nbsp;<LABEL ID=10521>" & GetLocalResourceObject("tcdExpirDatCaption") & "</LABEL>&nbsp;&nbsp;&nbsp;&nbsp;" & vbCrLf)
Response.Write("			")


Response.Write(mobjValues.DateControl("tcdExpirDat", CStr(lclsPremium.dExpirDat),  , GetLocalResourceObject("tcdExpirDatToolTip"),  ,  ,  ,  , True))


Response.Write("		    " & vbCrLf)
Response.Write("			")


Response.Write(mobjValues.closeFIELDSET())


Response.Write("" & vbCrLf)
Response.Write("		</TD>        " & vbCrLf)
Response.Write("		<TD WIDTH=""2%"">&nbsp;</TD>" & vbCrLf)
Response.Write("		<TD WIDTH=""32%"">" & vbCrLf)
Response.Write("			")


Response.Write(mobjValues.FIELDSET(999, "Datos del estado"))


Response.Write("" & vbCrLf)
Response.Write("			&nbsp;&nbsp;<LABEL ID=10521>" & GetLocalResourceObject("cbeStatus_PreCaption") & "</LABEL>&nbsp;&nbsp;" & vbCrLf)
Response.Write("			")


Response.Write(mobjValues.PossiblesValues("cbeStatus_Pre", "table19", eFunctions.Values.eValuesType.clngComboType, CStr(lclsPremium.nStatus_Pre),  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeStatus_PreToolTip")))


Response.Write("		    " & vbCrLf)
Response.Write("			&nbsp;&nbsp;<LABEL ID=10521>" & GetLocalResourceObject("tcdStatdateCaption") & "</LABEL>&nbsp;&nbsp;&nbsp;&nbsp;" & vbCrLf)
Response.Write("			")


Response.Write(mobjValues.DateControl("tcdStatdate", CStr(lclsPremium.dStatdate),  , GetLocalResourceObject("tcdStatdateToolTip"),  ,  ,  ,  , True))


Response.Write("		    " & vbCrLf)
Response.Write("			")


Response.Write(mobjValues.closeFIELDSET())


Response.Write("" & vbCrLf)
Response.Write("		</TD>		" & vbCrLf)
Response.Write("		<TD WIDTH=""2%"">&nbsp;</TD>" & vbCrLf)
Response.Write("		<TD WIDTH=""32%"">" & vbCrLf)
Response.Write("			")


Response.Write(mobjValues.FIELDSET(999, "Vía de cobro"))


Response.Write("" & vbCrLf)
Response.Write("			&nbsp;&nbsp;<LABEL ID=10521>" & GetLocalResourceObject("cbeWay_PayCaption") & "</LABEL>&nbsp;&nbsp;" & vbCrLf)
Response.Write("			")


Response.Write(mobjValues.PossiblesValues("cbeWay_Pay", "table5002", eFunctions.Values.eValuesType.clngComboType, CStr(lclsPremium.nWay_Pay),  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeWay_PayToolTip")))


Response.Write("				    " & vbCrLf)
Response.Write("			&nbsp;&nbsp;&nbsp;&nbsp;" & vbCrLf)
Response.Write("			&nbsp;&nbsp;<LABEL ID=10521>" & GetLocalResourceObject("tcnBulletinsCaption") & "</LABEL>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" & vbCrLf)
Response.Write("			")


Response.Write(mobjValues.NumericControl("tcnBulletins", 12, CStr(lclsPremium.nBulletins),  , GetLocalResourceObject("tcnBulletinsToolTip"),  ,  ,  ,  ,  ,  , True))


Response.Write("		    " & vbCrLf)
Response.Write("			")


Response.Write(mobjValues.closeFIELDSET())


Response.Write("" & vbCrLf)
Response.Write("		</TD>		" & vbCrLf)
Response.Write("	</TR>        " & vbCrLf)
Response.Write("	" & vbCrLf)
Response.Write("</TABLE>" & vbCrLf)
Response.Write("<TABLE WIDTH=""100%"">	" & vbCrLf)
Response.Write("    <TR><TD WIDTH=""13%"">&nbsp;</TD>" & vbCrLf)
Response.Write("        <TD WIDTH=""20%"">&nbsp;</TD>" & vbCrLf)
Response.Write("        <TD WIDTH=""1%"">&nbsp;</TD>" & vbCrLf)
Response.Write("        <TD WIDTH=""13%"">&nbsp;</TD>" & vbCrLf)
Response.Write("        <TD WIDTH=""20%"">&nbsp;</TD>" & vbCrLf)
Response.Write("        <TD WIDTH=""1%"">&nbsp;</TD>" & vbCrLf)
Response.Write("        <TD WIDTH=""12%"">&nbsp;</TD>" & vbCrLf)
Response.Write("        <TD WIDTH=""20%"">&nbsp;</TD>" & vbCrLf)
Response.Write("    </TR>   " & vbCrLf)
Response.Write("    <TR> 	" & vbCrLf)
Response.Write("	    <TD><LABEL id=13771>" & GetLocalResourceObject("cbeTypeCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("	    <TD>")


Response.Write(mobjValues.PossiblesValues("cbeType", "tabType_Receipt", eFunctions.Values.eValuesType.clngComboType, CStr(lclsPremium.nType),  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeTypeToolTip")))


Response.Write(" </TD>            " & vbCrLf)
Response.Write("	    <TD>&nbsp;</TD>" & vbCrLf)
Response.Write("	    <TD><LABEL id=13771>" & GetLocalResourceObject("cbeTratypeiCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("	    <TD>")


Response.Write(mobjValues.PossiblesValues("cbeTratypei", "table24", eFunctions.Values.eValuesType.clngComboType, CStr(lclsPremium.nTratypei),  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeTratypeiToolTip")))


Response.Write(" </TD>            " & vbCrLf)
Response.Write("	    <TD>&nbsp;</TD>" & vbCrLf)
Response.Write("	    <TD><LABEL id=13771>" & GetLocalResourceObject("cbePayFreqCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("	    <TD>")


Response.Write(mobjValues.PossiblesValues("cbePayFreq", "table36", eFunctions.Values.eValuesType.clngComboType, CStr(lclsPremium.nPayFreq),  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbePayFreqToolTip")))


Response.Write(" </TD>            " & vbCrLf)
Response.Write("	</TR>" & vbCrLf)
Response.Write("	<TR> 	" & vbCrLf)
Response.Write("	    <TD COLSPAN=""8"">&nbsp;</TD>" & vbCrLf)
Response.Write("	</TR>" & vbCrLf)
Response.Write("	<TR> 	" & vbCrLf)
Response.Write("	    <TD><LABEL id=13771>" & GetLocalResourceObject("tcnBalanceCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("	    <TD>")


Response.Write(mobjValues.NumericControl("tcnBalance", 18, CStr(lclsPremium.nBalance),  , GetLocalResourceObject("tcnBalanceToolTip"), True, 6,  ,  ,  ,  , True))


Response.Write(" </TD>            " & vbCrLf)
Response.Write("	    <TD>&nbsp;</TD>" & vbCrLf)
Response.Write("	    <TD><LABEL id=13771>" & GetLocalResourceObject("cbeCurrency_BalCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("	    <TD>")


Response.Write(mobjValues.PossiblesValues("cbeCurrency_Bal", "table11", eFunctions.Values.eValuesType.clngComboType, CStr(lclsPremium.nCurrency),  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeCurrency_BalToolTip")))


Response.Write(" </TD>            	    " & vbCrLf)
Response.Write("	    <TD>&nbsp;</TD>" & vbCrLf)
Response.Write("	    <TD><LABEL id=13771>" & GetLocalResourceObject("cbeNullCodeCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("	    <TD>")


Response.Write(mobjValues.PossiblesValues("cbeNullCode", "table95", eFunctions.Values.eValuesType.clngComboType, CStr(lclsPremium.nNullCode),  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeNullCodeToolTip")))


Response.Write(" </TD>            	    " & vbCrLf)
Response.Write("	</TR>" & vbCrLf)
Response.Write("	<TR> 	" & vbCrLf)
Response.Write("	    <TD COLSPAN=""8"">&nbsp;</TD>" & vbCrLf)
Response.Write("	</TR>" & vbCrLf)
Response.Write("	<TR> 	" & vbCrLf)
Response.Write("	    <TD><LABEL id=13771>" & GetLocalResourceObject("tcnContratCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("	    <TD>")


Response.Write(mobjValues.NumericControl("tcnContrat", 14, CStr(lclsPremium.nContrat),  , GetLocalResourceObject("tcnContratToolTip"), True, 2,  ,  ,  ,  , True))


Response.Write(" </TD>            " & vbCrLf)
Response.Write("	    <TD>&nbsp;</TD>" & vbCrLf)
Response.Write("	    <TD><LABEL id=13771>" & GetLocalResourceObject("cbeOfficeCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("	    <TD>")


Response.Write(mobjValues.PossiblesValues("cbeOffice", "table9", eFunctions.Values.eValuesType.clngComboType, CStr(lclsPremium.nOffice),  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeOfficeToolTip")))


Response.Write(" </TD>            	    " & vbCrLf)
Response.Write("	    <TD>&nbsp;</TD>" & vbCrLf)
Response.Write("	    <TD><LABEL id=13771>" & GetLocalResourceObject("tcnPolizaCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("	    <TD>")


Response.Write(mobjValues.NumericControl("tcnPoliza", 14, CStr(lclsPremium.nPolicy),  , GetLocalResourceObject("tcnPolizaToolTip"),  ,  ,  ,  ,  ,  , True))


Response.Write(" </TD>            " & vbCrLf)
Response.Write("	</TR>" & vbCrLf)
Response.Write("	<TR> 	" & vbCrLf)
Response.Write("	    <TD COLSPAN=""8"">&nbsp;</TD>" & vbCrLf)
Response.Write("	</TR>   " & vbCrLf)
Response.Write("	<TR> 	" & vbCrLf)
Response.Write("	    <TD><LABEL id=13771>" & GetLocalResourceObject("tdcClientCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("	    <TD COLSPAN=""4"">")


Response.Write(mobjValues.ClientControl("tdcClient", lclsPremium.sClient,  , GetLocalResourceObject("tdcClientToolTip"),  , True, "lblClieName"))


Response.Write(" </TD>    " & vbCrLf)
Response.Write("	    <TD>&nbsp;</TD>" & vbCrLf)
Response.Write("	    <TD><LABEL id=13771>" & GetLocalResourceObject("tcnAmount_IntCaption") & "</LABEL></TD>    " & vbCrLf)
Response.Write("	    <TD>")


Response.Write(mobjValues.NumericControl("tcnAmount_Int", 18, CStr(lclsPremium.nAmount_Int),  , GetLocalResourceObject("tcnAmount_IntToolTip"), True, 6,  ,  ,  ,  , True))


Response.Write(" </TD>            " & vbCrLf)
Response.Write("	</TR>" & vbCrLf)
Response.Write("	<TR> 	" & vbCrLf)
Response.Write("	    <TD COLSPAN=""8"">&nbsp;</TD>" & vbCrLf)
Response.Write("	</TR>   " & vbCrLf)
Response.Write("	<TR> 	 	" & vbCrLf)
Response.Write("	    <TD><LABEL id=13771>" & GetLocalResourceObject("valIntermedCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("	    <TD COLSPAN=""4"">")


Response.Write(mobjValues.PossiblesValues("valIntermed", "tabIntermed_Client", eFunctions.Values.eValuesType.clngWindowType, CStr(lclsPremium.nIntermed),  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("valIntermedToolTip")))


Response.Write(" </TD>    " & vbCrLf)
Response.Write("	    <TD>&nbsp;</TD>" & vbCrLf)
Response.Write("	    <TD><LABEL id=13771>" & GetLocalResourceObject("tcnPremiumnCaption") & "</LABEL></TD>    " & vbCrLf)
Response.Write("	    <TD>")


Response.Write(mobjValues.NumericControl("tcnPremiumn", 18, CStr(lclsPremium.nPremiumn),  , GetLocalResourceObject("tcnPremiumnToolTip"), True, 6,  ,  ,  ,  , True))


Response.Write(" </TD>            " & vbCrLf)
Response.Write("	</TR>" & vbCrLf)
Response.Write("	<TR> 	" & vbCrLf)
Response.Write("	    <TD COLSPAN=""8"">&nbsp;</TD>" & vbCrLf)
Response.Write("	</TR>   " & vbCrLf)
Response.Write("	<TR>" & vbCrLf)
Response.Write("	    <TD><LABEL id=13771>" & GetLocalResourceObject("tcnPremiumCaption") & "</LABEL></TD>    " & vbCrLf)
Response.Write("	    <TD>")


Response.Write(mobjValues.NumericControl("tcnPremium", 18, CStr(lclsPremium.nPremium),  , GetLocalResourceObject("tcnPremiumToolTip"), True, 6,  ,  ,  ,  , True))


Response.Write(" </TD>            " & vbCrLf)
Response.Write("	    <TD>&nbsp;</TD>" & vbCrLf)
Response.Write("	    <TD><LABEL id=13771>" & GetLocalResourceObject("valCollectoCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("	    <TD COLSPAN=""4"">")


Response.Write(mobjValues.PossiblesValues("valCollecto", "tabCollector_Client", eFunctions.Values.eValuesType.clngWindowType, CStr(lclsPremium.nCollecto),  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("valCollectoToolTip")))


Response.Write(" </TD>    " & vbCrLf)
Response.Write("	</TR>" & vbCrLf)
Response.Write("	<TR> 	" & vbCrLf)
Response.Write("	    <TD COLSPAN=""8"">&nbsp;</TD>" & vbCrLf)
Response.Write("	</TR>   " & vbCrLf)
Response.Write("	<TR>" & vbCrLf)
Response.Write("	    <TD><LABEL id=13771>" & GetLocalResourceObject("tcnInt_MoraCaption") & "</LABEL></TD>    " & vbCrLf)
Response.Write("	    <TD COLSPAN=""7"">")


Response.Write(mobjValues.NumericControl("tcnInt_Mora", 18, CStr(lclsPremium.nInt_mora),  , GetLocalResourceObject("tcnInt_MoraToolTip"), True, 6,  ,  ,  ,  , True))


Response.Write(" </TD>            " & vbCrLf)
Response.Write("	</TR>       " & vbCrLf)
Response.Write("</TABLE>")


	lclsPremium = Nothing
End Sub

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("coc003")
With Server
	mobjValues = New eFunctions.Values
	'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.44.07
	mobjValues.sSessionID = Session.SessionID
	mobjValues.nUsercode = Session("nUsercode")
	'~End Body Block VisualTimer Utility
	
	mobjValues.sCodisplPage = "coc003"
	mobjGrid = New eFunctions.Grid
	'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.44.07
	mobjGrid.sSessionID = Session.SessionID
	mobjGrid.nUsercode = Session("nUsercode")
	'~End Body Block VisualTimer Utility
	
	mobjGrid.sCodisplPage = "coc003"
	Call mobjGrid.SetWindowParameters(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), Request.QueryString.Item("nWindowTy"))
	mobjMenu = New eFunctions.Menues
	'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.44.07
	mobjMenu.sSessionID = Session.SessionID
	mobjMenu.nUsercode = Session("nUsercode")
	'~End Body Block VisualTimer Utility
End With
%>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">



    <%With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjMenu.setZone(2, "COC003", Request.QueryString.Item("sWindowDescript"), CShort(Request.QueryString.Item("nWindowTy"))))
End With
mobjMenu = Nothing%>
<SCRIPT>
//+ Variable para el control de versiones
     document.VssVersion="$$Revision: 3 $|$$Date: 29/10/03 11:18 $|$$Author: Nvaplat41 $"
</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="fraContent" ACTION="valCollectionQue.aspx?mode=2">
    <%Response.Write(mobjValues.ShowWindowsName("COC003", Request.QueryString.Item("sWindowDescript")))%>
    <%Call insPreCOC003()%>     
</FORM>
</BODY>
</HTML>
<%mobjValues = Nothing
mobjGrid = Nothing
%>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 10.44.07
Call mobjNetFrameWork.FinishPage("coc003")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




