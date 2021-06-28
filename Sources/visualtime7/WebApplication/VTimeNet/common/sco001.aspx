<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eCollection" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 10.53.46
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
Dim mobjCollection As eCollection.Premium


</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("SCO001")

mobjValues = New eFunctions.Values
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("SCO001")

mobjCollection = New eCollection.Premium

mobjValues.sCodisplPage = "sco001"
%>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>



<HTML>
<HEAD>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">
    <%'<TITLE>Datos de verificación del recibo</TITLE>
Response.Write(mobjValues.StyleSheet())
Response.Write(mobjValues.WindowsTitle(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript")))
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="frmSCO001" ACTION="ValGeneralForm.aspx?sZone=2">
<%
Call mobjCollection.insAcceptDataVerifyReceipt(CDbl(Request.QueryString.Item("nReceipt")), Request.QueryString.Item("sCertype"), mobjValues.StringToType(Request.QueryString.Item("nDigit"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nPayNumber"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nGeneralNumerator"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble))
%>
	<TABLE WIDTH="100%">
        <TD	WIDTH="15%"><LABEL><%= GetLocalResourceObject("lblNumReceiptCaption") %></LABEL></TD>
        <TD><%=mobjValues.TextControl("lblNumReceipt", 30, Request.QueryString.Item("nReceipt"),  ,  , True)%></TD>
		<TD WIDTH="50%" COLSPAN="2" CLASS="HighLighted"><LABEL><A NAME="Datos de la compañía de seguro"><%= GetLocalResourceObject("AnchorDatos de la compañía de seguroCaption") %></A></LABEL><hr></TD>
        </TR>
        <TD><LABEL ><%= GetLocalResourceObject("lblDesBranchCaption") %></LABEL></TD>
        <TD><%=mobjValues.TextControl("lblDesBranch", 30, mobjCollection.sDesBranch,  ,  , True)%></TD>
        <TD><LABEL ><%= GetLocalResourceObject("lblOriginPolCaption") %></LABEL></TD>
        <TD><%=mobjValues.TextControl("lblOriginPol", 30, mobjCollection.sOriginal,  ,  , True)%></TD>
        </TR>
        <TD><LABEL ><%= GetLocalResourceObject("lblDesProductCaption") %></LABEL></TD>
        <TD><%=mobjValues.TextControl("lblDesProduct", 30, mobjCollection.sDescProd,  ,  , True)%></TD>
        <TD><LABEL ><%= GetLocalResourceObject("lblOriginRecCaption") %></LABEL></TD>
        <TD><%=mobjValues.TextControl("lblOriginRec", 30, mobjCollection.sOrigReceipt,  ,  , True)%></TD>
        </TR>
        <TD><LABEL ><%= GetLocalResourceObject("lblNumPolicyCaption") %></LABEL></TD>
        <TD><%=mobjValues.TextControl("lblNumPolicy", 30, CStr(mobjCollection.nPolicy),  ,  , True)%></TD>
        <TD><LABEL ><%= GetLocalResourceObject("lblInsuranceComCaption") %></LABEL></TD>
        <TD><%=mobjValues.TextControl("lblInsuranceCom", 30, mobjCollection.sCompany,  ,  , True)%></TD>
        </TR>
        <TD><LABEL ><%= GetLocalResourceObject("lblDesZoneCaption") %></LABEL></TD>
        <TD><%=mobjValues.TextControl("lblDesZone", 30, mobjCollection.sDesOffice,  ,  , True)%></TD>
        <TD><LABEL ><%= GetLocalResourceObject("lblDesZoneCaption") %></LABEL></TD>
        <TD><%=mobjValues.TextControl("lblOfficeCom", 30, mobjCollection.sOfficeIns,  ,  , True)%></TD>
	</TABLE>

	</TR>
	<TABLE WIDTH="100%">
		<TD WIDTH="45%" COLSPAN="4" CLASS="HighLighted"><LABEL ><A NAME="Vigencia del recibo"><%= GetLocalResourceObject("AnchorVigencia del reciboCaption") %></A></LABEL><hr></TD>
		<TD WIDTH="55%" COLSPAN="4" CLASS="HighLighted"><LABEL ><A NAME="Datos del estado"><%= GetLocalResourceObject("AnchorDatos del estadoCaption") %></A></LABEL><hr></TD>
		</TR>
		<TR>
			<TD WIDTH="14%"><LABEL ><%= GetLocalResourceObject("lblInitDateCaption") %></LABEL></TD>
			<TD><%=mobjValues.TextControl("lblInitDate", 30, CStr(mobjCollection.dEffecdate),  ,  , True)%></TD>
			<TD></TD>
			<TD></TD>
			<TD WIDTH="16%"><LABEL ><%= GetLocalResourceObject("lblDesStatusCaption") %></LABEL></TD>
			<TD	width="25%"><%=mobjValues.TextControl("lblDesStatus", 30, mobjCollection.sDesStatus_pre,  ,  , True)%></TD>
		</TR>
		<TR>
			<TD WIDTH="14%"><LABEL ><%= GetLocalResourceObject("lblEndDateCaption") %></LABEL></TD>
			<TD><%=mobjValues.TextControl("lblEndDate", 30, CStr(mobjCollection.dExpirdat),  ,  , True)%></TD>
			<TD></TD>
			<TD></TD>
			<TD WIDTH="16%"><LABEL ><%= GetLocalResourceObject("lblStatusDateCaption") %></LABEL></TD>
			<TD><%=mobjValues.TextControl("lblStatusDate", 30, CStr(mobjCollection.dStatDate),  ,  , True)%></TD>
		</TR>
	</TABLE>
		
	<TABLE WIDTH="100%">
		<TR>		
		    <TD WIDTH="16%"><LABEL ><%= GetLocalResourceObject("lblClientCodeCaption") %></LABEL></TD>
		    <TD><%=mobjValues.TextControl("lblClientCode", 30, mobjCollection.sClient & " " & mobjCollection.sCliename,  ,  , True)%></TD>
		</TR>		
		<TR>            
		    <TD WIDTH="16%"><LABEL ><%= GetLocalResourceObject("lblAgentCodeCaption") %></LABEL></TD>
		    <TD><%=mobjValues.TextControl("lblAgentCode", 30, mobjCollection.nIntermed & "-" & mobjCollection.sClienameProductor,  ,  , True)%></TD>
		</TR>
		<TR>
		    <TD WIDTH="16%"><LABEL ><%= GetLocalResourceObject("lblSupCodeCaption") %></LABEL></TD>
		    <TD><%=mobjValues.TextControl("lblSupCode", 30, mobjCollection.nSupervis & "-" & mobjCollection.sClienameSupervis,  ,  , True)%></TD>
		</TR>
		<TR>
		    <TD WIDTH="16%"><LABEL ><%= GetLocalResourceObject("lblDesTratypeiCaption") %></LABEL></TD>
		    <TD><%=mobjValues.TextControl("lblDesTratypei", 30, mobjCollection.sDesTratypei,  ,  , True)%></TD>
		</TR>
	</TABLE>

	<TABLE WIDTH="100%">
		<TD WIDTH="50%" COLSPAN="2" CLASS="HighLighted"><LABEL><A NAME="Comisiones"><%= GetLocalResourceObject("AnchorComisionesCaption") %></A></LABEL><HR></TD>
        <TR>
		    <TD><LABEL ><%= GetLocalResourceObject("lblAgentCodeCaption") %></LABEL></TD>
			<TD><%=mobjValues.TextControl("lblAgentCommiss", 30, CStr(mobjCollection.nAmountP),  ,  , True)%></TD>
			<TD></TD>
		    <TD><LABEL ><%= GetLocalResourceObject("lblPremiumCaption") %></LABEL></TD>
            <TD><%=mobjValues.TextControl("lblPremium", 30, CStr(mobjCollection.nPremium),  ,  , True)%></TD>
        </TR>
            
        <TR>
			<TD><LABEL ><%= GetLocalResourceObject("lblSupCodeCaption") %></LABEL></TD>
			<TD><%=mobjValues.TextControl("lblSupCommiss", 30, CStr(mobjCollection.nAmountS),  ,  , True)%></TD>
			<TD></TD>
			<TD><LABEL ><%= GetLocalResourceObject("lblCurrencyCaption") %></LABEL></TD>
			<TD><%=mobjValues.TextControl("lblCurrency", 30, mobjCollection.sDesCurrency,  ,  , True)%></TD></TD>
			<TD></TD>
			<TD ALIGN="RIGTH"><%=mobjValues.ButtonAcceptCancel("closeWindows();",  ,  ,  , 2)%></TD>
        </TR>
	</TABLE>
<%
mobjValues = Nothing
mobjCollection = Nothing
'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 10.53.46
Call mobjNetFrameWork.FinishPage("SCO001")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer
%>
</FORM>
</BODY>
</HTML>





