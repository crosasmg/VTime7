<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="ePolicy" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 31/3/03 19.44.14
Dim mobjNetFrameWork As eNetFrameWork.Layout

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo del menú
Dim mobjMenu As eFunctions.Menues

Dim lclsLife As ePolicy.Life


</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("VI002")
'~End Header Block VisualTimer Utility
Response.CacheControl = "private"
mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.44.14
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = Request.QueryString.Item("sCodispl")
lclsLife = New ePolicy.Life

Call lclsLife.Find(Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("npolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate))

If Request.QueryString.Item("sSource") = "SI091" Then
	mobjValues.ActionQuery = False
Else
	mobjValues.ActionQuery = Session("bQuery")
End If

%>

<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">


	<%mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.44.14
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility
Response.Write(mobjMenu.setZone(2, "VI002", Request.QueryString.Item("sWindowDescript"), CShort(Request.QueryString.Item("nWindowTy"))))
	    mobjMenu = Nothing
Response.Write(mobjValues.StyleSheet())
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="frmVI002" ACTION="valPolicySeq.aspx?sTime=1&sSource=<%=Request.QueryString.Item("sSource")%>">
	<P ALIGN="Center">
		<%If Request.QueryString.Item("sSource") = "SI091" Then%>
			<LABEL ID=100720><a HREF="#Clientes"><%= GetLocalResourceObject("AnchorClientesCaption") %></a></LABEL><LABEL ID=0> | </LABEL>
		<%End If%>
		<LABEL ID=40654><a HREF="#Montos"><%= GetLocalResourceObject("AnchorMontosCaption") %></a></LABEL><LABEL ID=0> | </LABEL>
		<LABEL ID=40656><a HREF="#Fechas"><%= GetLocalResourceObject("AnchorFechasCaption") %></a></LABEL><LABEL ID=0> | </LABEL>
		<LABEL ID=40658><a HREF="#Pagos"><%= GetLocalResourceObject("AnchorPagosCaption") %></a></LABEL><LABEL ID=0> | </LABEL>
		<LABEL ID=40660><a HREF="#Situación actual"><%= GetLocalResourceObject("AnchorSituación actualCaption") %></a></LABEL>
	</P>	    
    <% Response.Write(mobjValues.ShowWindowsName("VI002", Request.QueryString.Item("sWindowDescript"))) %>
    <P ALIGN="CENTER">
        <TABLE>
			<TR>
				<TD><% Response.Write(mobjValues.OptionControl(100740, "OptLoanType", GetLocalResourceObject("OptLoanType_1Caption"), , "1"))%></TD>
                <%--lclsLife.DefaultValueVI002("optLoan")--%>
				<TD WIDTH=50pcx>&nbsp;</TD>
				<TD><% Response.Write(mobjValues.OptionControl(100741, "OptLoanType", GetLocalResourceObject("OptLoanType_2Caption"), , "2"))%></TD>
                 <%--lclsLife.DefaultValueVI002("optLease")--%>
			</TR>
		</TABLE>
		<TABLE>
			<TR>
				<TD><LABEL ID=100720><%= GetLocalResourceObject("tctLoanCaption") %></LABEL></TD>
				<TD WIDTH=20pcx>&nbsp;</TD>
				<TD><%=mobjValues.TextControl("tctLoan", 20, lclsLife.sLoan_numbe)%></TD>
			</TR>
		</TABLE>
    </P>
    <TABLE WIDTH="100%" COLS=5>
    <%If Request.QueryString.Item("sSource") = "SI091" Then%>
		<TR>
			<TD COLSPAN="5" CLASS="HighLighted"><LABEL ID=100721><A NAME="Clientes"><%= GetLocalResourceObject("AnchorClientes2Caption") %></A></LABEL></TD>
		</TR>
		<TR>
			<TD COLSPAN="5"><HR></TD>
		</TR>		
		<TR>
			<TD><LABEL ID=100722><%= GetLocalResourceObject("valBorrowerCaption") %></LABEL>
			<TD COLSPAN = "4"><%=mobjValues.ClientControl("valBorrower", "",  , "",  ,  , "lblBorrower", False,  ,  ,  , eFunctions.Values.eTypeClient.SearchClient)%></TD>
		</TR> 
		<TR>
			<TD><LABEL ID=100724><%= GetLocalResourceObject("valCoBorrowerCaption") %></LABEL>
			<TD COLSPAN = "4"><%=mobjValues.ClientControl("valCoBorrower", "",  , "",  ,  , "lblCoBorrower", False,  ,  ,  , eFunctions.Values.eTypeClient.SearchClient)%></TD>
		</TR>
			<TD><LABEL ID=0><%= GetLocalResourceObject("valCarDealerCaption") %></LABEL>
			<TD COLSPAN = "4"><%	mobjValues.ClientRole = CStr(51)
	'mobjValues.nCertif = 0
	Response.Write(mobjValues.ClientControl("valCarDealer", "",  , GetLocalResourceObject("valCarDealerToolTip"),  ,  , "lblCliename", False,  ,  ,  , eFunctions.Values.eTypeClient.SearchClientPolicy))
	%>
			</TD>		
		</TR> 
	<%End If%>
        <TR>
			<TD COLSPAN="2" CLASS="HighLighted"><LABEL ID=100726><A NAME="Montos"><%= GetLocalResourceObject("AnchorMontos2Caption") %></A></LABEL></TD>
			<TD width=15%>&nbsp;</TD>
			<TD COLSPAN="2" CLASS="HighLighted"><LABEL ID=100727><A NAME="Fechas"><%= GetLocalResourceObject("AnchorFechas2Caption") %></A></LABEL></TD>
		</TR>
		<TR>
			<TD COLSPAN="2"><HR></TD>
			<TD>&nbsp;</TD>
			<TD COLSPAN="2"><HR></TD>
		</TR>
		<TR>
            <TD><LABEL ID=100728><%= GetLocalResourceObject("tcnTotalLoanCaption") %></LABEL></TD>
            <TD><%= mobjValues.NumericControl("tcnTotalLoan", 9, , , "", True)%></TD>
            <%--lclsLife.nTotalLoan--%>
            <TD>&nbsp;</TD>
            <TD><LABEL ID=100729><%= GetLocalResourceObject("tcdApplicationCaption") %></LABEL></TD>
            <TD><%= mobjValues.DateControl("tcdApplication", )%></TD>
            <%--lclsLife.dApplication--%>
        </TR>
        <TR>
            <TD><LABEL ID=100730><%= GetLocalResourceObject("tcnInstalmentCaption") %></LABEL></TD>
            <TD><%= mobjValues.NumericControl("tcnInstalment", 9, , , "", True)%></TD>
            <%--lclsLife.nInstallments--%>
            <TD>&nbsp;</TD>
            <TD><LABEL ID=100731><%= GetLocalResourceObject("tcdSigningCaption") %></LABEL></TD>
            <TD><%= mobjValues.DateControl("tcdSigning", )%></TD>
            <%--lclsLife.dSigning--%>
        </TR>
        <TR>
			<TD COLSPAN="2" CLASS="HighLighted"><LABEL ID=100732><A NAME="Pagos"><%= GetLocalResourceObject("AnchorPagos2Caption") %></A></LABEL></TD>
			<TD>&nbsp;</TD>
			<TD><LABEL ID=100733><%= GetLocalResourceObject("tcdDisbursementCaption") %></LABEL></TD>
			<TD><%= mobjValues.DateControl("tcdDisbursement", )%></TD>
            <%--lclsLife.dDisbursement--%>
        </TR>
        <TR>
    		<TD COLSPAN="2"><HR></TD>
			<TD>&nbsp;</TD>
			<TD><LABEL ID=100734><%= GetLocalResourceObject("tcdMaturityCaption") %></LABEL></TD>
			<TD><%= mobjValues.DateControl("tcdMaturity", )%></TD>
            <%--lclsLife.dMaturity--%>
        </TR>
        <TR>
			<TD><LABEL ID=100760><%= GetLocalResourceObject("tctPay_amountCaption") %></LABEL></TD>
			<TD><%= mobjValues.NumericControl("tctPay_amount", 10, , , , True, 2)%></TD>            
            <%--lclsLife.nPay_amount--%>
            <TD>&nbsp;</TD>
            <TD COLSPAN="2" CLASS="HighLighted"><LABEL ID=100736><A NAME="Situación actual"><%= GetLocalResourceObject("AnchorSituación actual2Caption") %></A></LABEL></TD>
        </TR>
        <TR>
            <TD><LABEL ID=100735><%= GetLocalResourceObject("cbeAmortize_wayCaption") %></LABEL></TD>
            <TD><%= mobjValues.PossiblesValues("cbeAmortize_way", "Table9004", eFunctions.Values.eValuesType.clngComboType, )%></TD>
            <%--lclsLife.nAmortize_way--%>
			<TD>&nbsp;</TD>
    		<TD COLSPAN="2"><HR></TD>
		</TR>
		<TR>
            <TD><LABEL ID=100737><%= GetLocalResourceObject("AnchorCaption") %></LABEL></TD>
            <TD><%= mobjValues.OptionControl(100742, "optPayWay", GetLocalResourceObject("optPayWay_1Caption"), , "1")%></TD>
            <%--lclsLife.DefaultValueVI002("EFTMethod")--%>
		    <TD>&nbsp;</TD>
		    <TD><LABEL ID=100738><%= GetLocalResourceObject("tcnBalanceCaption") %></LABEL></TD>
		    <TD><%= mobjValues.NumericControl("tcnBalance", 9, , , "", True)%></TD>
            <%--lclsLife.nBalance--%>
		</TR>
		<TR>
		    <TD>&nbsp;</TD>
		    <TD><%= mobjValues.OptionControl(100743, "optPayWay", GetLocalResourceObject("optPayWay_2Caption"), , "2")%></TD>
            <%--lclsLife.DefaultValueVI002("ChMethod")--%>
			<TD>&nbsp;</TD>
		    <TD><LABEL ID=100739><%= GetLocalResourceObject("tcdNextPaymentCaption") %></LABEL></TD>
		    <TD><%= mobjValues.DateControl("tcdNextPayment", , , "")%></TD>
            <%--lclsLife.dNextPayment--%>
        </TR>
    </TABLE>
    <%=mobjValues.BeginPageButton%>
</FORM>
</BODY>
</HTML>
<%
mobjValues = Nothing
lclsLife = Nothing
%> 
<%'^Begin Footer Block VisualTimer Utility 1.1 31/3/03 19.44.14
Call mobjNetFrameWork.FinishPage("VI002")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>





