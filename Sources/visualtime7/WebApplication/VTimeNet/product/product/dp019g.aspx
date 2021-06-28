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

mobjValues.sCodisplPage = "dp019g"
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
	.Write(mobjMenu.setZone(2, "DP019G", "DP019G.aspx"))
End With
mobjMenu = Nothing
%>
<SCRIPT>
//- Variable para el control de versiones
	document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 16:56 $|$$Author: Nvaplat61 $"

//% InsChangeOptCapital: se maneja el cambio de valor de los datos asociados al capital
//-------------------------------------------------------------------------------------------
function InsChangeOptCapital(Field){
//-------------------------------------------------------------------------------------------
	with (self.document.forms[0]){
		if (Field.value == '1' ) {
			tcnPrice.value = '';
			tcnPrice.disabled = true;
		}
		else
			tcnPrice.disabled = false;
	}
}
//% InsChangeChkPremium: se maneja el cambio de valor de los datos asociados a la prima
//------------------------------------------------------------------------------------------
function InsChangeChkPremium(Field){
//------------------------------------------------------------------------------------------
	with (self.document.forms[0]){
		if (Field.checked) {
			valCover.value = '';
			UpdateDiv('valCoverDesc','')
			valCover.disabled = true;
			btnvalCover.disabled = valCover.disabled;
			tcnRate.disabled = false;
		}
		else {
			valCover.disabled = false;
			btnvalCover.disabled = valCover.disabled;
			tcnRate.disabled = true;
			tcnRate.value = '';
		}
	}
}
</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="frmDP019G" ACTION="valCoverSeq.aspx?nMainAction=<%=Request.QueryString.Item("nMainAction")%>">
	<%=mobjValues.ShowWindowsName("DP019G")%>
    <TABLE WIDTH="100%">
        <TR>
            <TD COLSPAN="5" CLASS="HighLighted"><LABEL ID=100363><%= GetLocalResourceObject("AnchorCaption") %></LABEL></TD>
        </TR>
		<TR>
			<TD COLSPAN="5" CLASS="HorLine"></TD>
		</TR>
		<TR>
            <TD COLSPAN="2"><%=mobjValues.CheckControl("ChkCapital", GetLocalResourceObject("ChkCapitalCaption"), mclsTab_LifCov.sCapiprem,  ,  ,  , 1, GetLocalResourceObject("ChkCapitalToolTip"))%></TD>
            <TD COLSPAN="3"><%=mobjValues.OptionControl(100102, "optCapital", GetLocalResourceObject("optCapital_1Caption"), mclsTab_LifCov.sCacalfri, "1", "InsChangeOptCapital(this);",  , 2, GetLocalResourceObject("optCapital_1ToolTip"))%></TD>
		</TR>
		<TR>
            <TD COLSPAN="2">&nbsp;</TD>
            <TD WIDTH=20%><%=mobjValues.OptionControl(100106, "optCapital", GetLocalResourceObject("optCapital_2Caption"), mclsTab_LifCov.DefaultValueDP019G("CapitalFix"), "2", "InsChangeOptCapital(this);",  , 3, GetLocalResourceObject("optCapital_2ToolTip"))%></TD>
            <TD><LABEL ID=14109><%= GetLocalResourceObject("tcnPriceCaption") %></LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnPrice", 18, CStr(mclsTab_LifCov.nCacalfix),  , GetLocalResourceObject("tcnPriceToolTip"), True, 2,  ,  ,  ,  , mclsTab_LifCov.DefaultValueDP019G("Amount_disabled"), 6)%></TD>
		</TR>
	</TABLE>
	<TABLE WIDTH="100%">
        <TR>
            <TD COLSPAN="5" CLASS="HighLighted"><LABEL ID=100364><%= GetLocalResourceObject("Anchor2Caption") %></LABEL></TD>
        </TR>
		<TR>
			<TD COLSPAN="5" CLASS="HorLine"></TD>
		</TR>
		<TR>
            <TD COLSPAN="3"><%=mobjValues.CheckControl("ChkPremium", GetLocalResourceObject("ChkPremiumCaption"), mclsTab_LifCov.sPremcapi,  , "InsChangeChkPremium(this);",  , 5, GetLocalResourceObject("ChkPremiumToolTip"))%></TD>
            <TD><LABEL ID=14103><%= GetLocalResourceObject("valCoverCaption") %></LABEL></TD>
			<TD><%mobjValues.Parameters.Add("sStatregt", "1", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
mobjValues.Parameters.Add("nCoverGen", mobjValues.StringToType(Session("nCover"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
Response.Write(mobjValues.PossiblesValues("valCover", "tabTab_LifCov", eFunctions.Values.eValuesType.clngWindowType, CStr(mclsTab_LifCov.nCover_in), True,  ,  ,  ,  ,  , mclsTab_LifCov.DefaultValueDP019G("Cover"), 4, GetLocalResourceObject("valCoverToolTip"),  , 6))
%>
			</TD>
		</TR>
		<TR>
            <TD><LABEL ID=14111><%= GetLocalResourceObject("tctRutinCaption") %></LABEL></TD>
            <TD><%=mobjValues.TextControl("tctRutin", 12, mclsTab_LifCov.sRouprcal,  , GetLocalResourceObject("tctRutinToolTip"),  ,  ,  ,  ,  , 7)%></TD>
            <TD>&nbsp;</TD>
            <TD><LABEL ID=14110><%= GetLocalResourceObject("tcnRateCaption") %></LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnRate", 9, CStr(mclsTab_LifCov.nPremirat),  , GetLocalResourceObject("tcnRateToolTip"),  , 6,  ,  ,  ,  , mclsTab_LifCov.DefaultValueDP019G("Rate"), 8)%></TD>
		</TR>
        <TR>
            <TD COLSPAN="5" CLASS="HighLighted"><LABEL ID=100365><%= GetLocalResourceObject("Anchor3Caption") %></LABEL></TD>
        </TR>
		<TR>
			<TD COLSPAN="5" CLASS="HorLine"></TD>
		</TR>
        </TR>
            <TD><LABEL ID=14108><%= GetLocalResourceObject("tctDeathCaption") %></LABEL></TD>
            <TD><%=mobjValues.TextControl("tctDeath", 12, mclsTab_LifCov.sCldeathi,  , GetLocalResourceObject("tctDeathToolTip"),  ,  ,  ,  ,  , 9)%></TD>
            <TD>&nbsp;</TD>
            <TD><LABEL ID=14107><%= GetLocalResourceObject("tctDoubleIndemCaption") %></LABEL></TD>
            <TD><%=mobjValues.TextControl("tctDoubleIndem", 12, mclsTab_LifCov.sClaccidi,  , GetLocalResourceObject("tctDoubleIndemToolTip"),  ,  ,  ,  ,  , 10)%></TD>
        </TR>
            <TD><LABEL ID=14106><%= GetLocalResourceObject("tctTriIndemCaption") %></LABEL></TD>
            <TD><%=mobjValues.TextControl("tctTriIndem", 12, mclsTab_LifCov.sClvehaci,  , GetLocalResourceObject("tctTriIndemToolTip"),  ,  ,  ,  ,  , 11)%></TD>
            <TD>&nbsp;</TD>
            <TD><LABEL ID=14105><%= GetLocalResourceObject("tctSurvivalCaption") %></LABEL></TD>
            <TD><%=mobjValues.TextControl("tctSurvival", 12, mclsTab_LifCov.sClsurvii,  , GetLocalResourceObject("tctSurvivalToolTip"),  ,  ,  ,  ,  , 12)%></TD>
        </TR>
            <TD><LABEL ID=14112><%= GetLocalResourceObject("tctInabilityCaption") %></LABEL></TD>
            <TD><%=mobjValues.TextControl("tctInability", 12, mclsTab_LifCov.sClincapi,  , GetLocalResourceObject("tctInabilityToolTip"),  ,  ,  ,  ,  , 13)%></TD>
            <TD>&nbsp;</TD>
            <TD><LABEL ID=14104><%= GetLocalResourceObject("tctInvalidCaption") %></LABEL></TD>
            <TD><%=mobjValues.TextControl("tctInvalid", 12, mclsTab_LifCov.sClinvali,  , GetLocalResourceObject("tctInvalidToolTip"),  ,  ,  ,  ,  , 14)%></TD>
        </TR>
            <TD><LABEL ID=14113><%= GetLocalResourceObject("tctClillnessCaption") %></LABEL></TD>
            <TD COLSPAN="4"><%=mobjValues.TextControl("tctClillness", 12, mclsTab_LifCov.sClillness,  , GetLocalResourceObject("tctClillnessToolTip"),  ,  ,  ,  ,  , 15)%></TD>
        </TR>
    </TABLE>
</FORM>
</BODY>
</HTML>
<%
mobjValues = Nothing
mclsTab_LifCov = Nothing
%>




