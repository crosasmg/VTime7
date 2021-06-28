<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eProduct" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de la página.
Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues

Dim mclsTab_LifCov As eProduct.Tab_lifcov


</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mclsTab_LifCov = New eProduct.Tab_lifcov

mobjValues.ActionQuery = Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionQuery) Or Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionDuplicate) Or Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionCut)

Call mclsTab_LifCov.Find(mobjValues.StringToType(Session("nCover"), eFunctions.Values.eTypeData.etdDouble))

mobjValues.sCodisplPage = "dp050g"
%>
<HTML>
<HEAD>
	<META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></SCRIPT>




<%
mobjMenu = New eFunctions.Menues
With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjMenu.setZone(2, "DP050G", "DP050G.aspx"))
End With
mobjMenu = Nothing
%>
<SCRIPT>    
//- Variable para el control de versiones
	document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 16:56 $|$$Author: Nvaplat61 $"

//% InsChangeOptSecure: se maneja el cambio de valor para las opciones del frame "Seguro"
//-------------------------------------------------------------------------------------------
function InsChangeOptSecure(Field){
//-------------------------------------------------------------------------------------------
	with (self.document.forms[0]){
		if (Field.value == '1' ) {
			tcnQuantity.value = '';
			tcnQuantity.disabled = true;
		}
		else
			tcnQuantity.disabled = false;
	}
}

//% InsChangeOptPay: se maneja el cambio de valor para las opciones del frame "Seguro"
//-------------------------------------------------------------------------------------------
function InsChangeOptPay(Field){
//-------------------------------------------------------------------------------------------
	with (self.document.forms[0]){
		if (Field.value == '1' ) {
			tcnQuantityPays.value = '';
			tcnQuantityPays.disabled = true;
		}
		else
			tcnQuantityPays.disabled = false;
	}
}
</SCRIPT>    
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="frmDP050G" ACTION="valCoverSeq.aspx?nMainAction=<%=Request.QueryString.Item("nMainAction")%>">
	<%=mobjValues.ShowWindowsName("DP050G")%>
    <TABLE WIDTH="100%">
        <TR>
            <TD COLSPAN="2" CLASS="HighLighted"><LABEL ID=100384><%= GetLocalResourceObject("AnchorCaption") %></LABEL></TD>
            <TD WIDTH="5%">&nbsp;</TD>
            <TD COLSPAN="2" CLASS="HighLighted"><LABEL ID=100385><%= GetLocalResourceObject("Anchor2Caption") %></LABEL></TD>
        </TR>
		<TR>
			<TD COLSPAN="2" CLASS="HorLine"></TD>
            <TD></TD>
			<TD COLSPAN="2" CLASS="HorLine"></TD>
		</TR>
		<TR>
            <TD><%=mobjValues.OptionControl(100133, "optSecure", GetLocalResourceObject("optSecure_1Caption"), mclsTab_LifCov.sIduropei, "1", "InsChangeOptSecure(this);",  , 1, GetLocalResourceObject("optSecure_1ToolTip"))%></TD>
            <TD><%=mobjValues.OptionControl(100136, "optSecure", GetLocalResourceObject("optSecure_2Caption"), mclsTab_LifCov.sIdurayear, "2", "InsChangeOptSecure(this);",  , 2, GetLocalResourceObject("optSecure_2ToolTip"))%></TD>
			<TD>&nbsp;</TD>
            <TD><%=mobjValues.OptionControl(100133, "optPay", GetLocalResourceObject("optPay_1Caption"), mclsTab_LifCov.sPduropei, "1", "InsChangeOptPay(this);",  , 5, GetLocalResourceObject("optPay_1ToolTip"))%></TD>
            <TD><%=mobjValues.OptionControl(100136, "optPay", GetLocalResourceObject("optPay_2Caption"), mclsTab_LifCov.sPduryear, "2", "InsChangeOptPay(this);",  , 6, GetLocalResourceObject("optPay_2ToolTip"))%></TD>
		</TR>
        <TR>
            <TD COLSPAN="2"><%=mobjValues.OptionControl(100138, "optSecure", GetLocalResourceObject("optSecure_3Caption"), mclsTab_LifCov.sIduraage, "3", "InsChangeOptSecure(this);",  , 3, GetLocalResourceObject("optSecure_3ToolTip"))%></TD>
			<TD>&nbsp;</TD>
            <TD COLSPAN="2"><%=mobjValues.OptionControl(100138, "optPay", GetLocalResourceObject("optPay_3Caption"), mclsTab_LifCov.sPduraage, "3", "InsChangeOptPay(this);",  , 7, GetLocalResourceObject("optPay_3ToolTip"))%></TD>
        </TR>
		<TR>
            <TD><LABEL ID=14193><%= GetLocalResourceObject("tcnQuantityCaption") %></LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnQuantity", 3, CStr(mclsTab_LifCov.nDuratInd),  , GetLocalResourceObject("tcnQuantityToolTip"),  , 0,  ,  ,  ,  , mclsTab_LifCov.sIduropei = "1", 4)%></TD>
			<TD>&nbsp;</TD>	
            <TD><LABEL ID=14194><%= GetLocalResourceObject("tcnQuantityPaysCaption") %></LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnQuantityPays", 3, CStr(mclsTab_LifCov.nDuratPay),  , GetLocalResourceObject("tcnQuantityPaysToolTip"),  , 0,  ,  ,  ,  , mclsTab_LifCov.sPduropei = "1", 8)%></TD>
		</TR>
        <TR>
			<TD COLSPAN="5">&nbsp;</TD>
        </TR>
        <TR>
            <TD COLSPAN="5" CLASS="HighLighted"><LABEL ID=100386><%= GetLocalResourceObject("Anchor3Caption") %></LABEL></TD>
        </TR>
		<TR>
			<TD COLSPAN="5" CLASS="HorLine"></TD>
		</TR>
        <TR>
            <TD><%=mobjValues.CheckControl("chkRenew", GetLocalResourceObject("chkRenewCaption"), mclsTab_LifCov.sRenewali,  ,  ,  , 9, GetLocalResourceObject("chkRenewToolTip"))%></TD>
            <TD><%=mobjValues.CheckControl("chkRevalue", GetLocalResourceObject("chkRevalueCaption"), mclsTab_LifCov.sRevIndex,  ,  ,  , 10, GetLocalResourceObject("chkRevalueToolTip"))%></TD>
			<TD>&nbsp;</TD>
            <TD><LABEL ID=14197><%= GetLocalResourceObject("tcnAgeCaption") %></LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnAge", 3, CStr(mclsTab_LifCov.nAgemaxi),  , GetLocalResourceObject("tcnAgeToolTip"),  , 0,  ,  ,  ,  ,  , 11)%></TD>
        </TR>
        <TR>
			<TD COLSPAN="5">&nbsp;</TD>
        </TR>
        <TR>
            <TD COLSPAN="2" CLASS="HighLighted"><LABEL ID=100387><%= GetLocalResourceObject("Anchor4Caption") %></LABEL></TD>
            <TD>&nbsp;</TD>
            <TD COLSPAN="2" CLASS="HighLighted"><LABEL ID=100388><%= GetLocalResourceObject("Anchor5Caption") %></LABEL></TD>
        </TR>
		<TR>
			<TD COLSPAN="2" CLASS="HorLine"></TD>
            <TD></TD>
			<TD COLSPAN="2" CLASS="HorLine"></TD>
		</TR>
        <TR>
            <TD><LABEL ID=14195><%= GetLocalResourceObject("tctRoutineCaption") %></LABEL></TD>
            <TD><%=mobjValues.TextControl("tctRoutine", 12, mclsTab_LifCov.sRouchaca,  , GetLocalResourceObject("tctRoutineToolTip"),  ,  ,  ,  ,  , 12)%></TD>
			<TD>&nbsp;</TD>
            <TD COLSPAN="2"><%=mobjValues.CheckControl("chkAgeReach", GetLocalResourceObject("chkAgeReachCaption"), mclsTab_LifCov.sRechapri, "1",  ,  , 13, GetLocalResourceObject("chkAgeReachToolTip"))%></TD>
        </TR>
        <TR>
			<TD COLSPAN="3">&nbsp;</TD>
            <TD><LABEL ID=14196><%= GetLocalResourceObject("tctRouPremiumCaption") %></LABEL></TD>
            <TD><%=mobjValues.TextControl("tctRouPremium", 12, mclsTab_LifCov.sRouchapr,  , GetLocalResourceObject("tctRouPremiumToolTip"),  ,  ,  ,  ,  , 14)%></TD>
        </TR>
    </TABLE>
</FORM>
</BODY>
</HTML>
<%
mobjValues = Nothing
mclsTab_LifCov = Nothing
%>




