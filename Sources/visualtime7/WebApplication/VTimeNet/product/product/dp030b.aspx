<%@ Page Language="VB" Explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon"
    EnableViewState="false" %>

<%@ Import Namespace="eFunctions" %>
<%@ Import Namespace="eProduct" %>
<script language="VB" runat="Server">
    '- Objeto para el manejo de la página.
    Dim mobjValues As eFunctions.Values
    Dim mobjMenu As eFunctions.Menues
    Dim lstrAction As Object
    Dim lclsTab_GenCov As eProduct.Tab_gencov


</script>
<%Response.Expires = -1

    mobjValues = New eFunctions.Values
    lstrAction = Request.QueryString.Item("nMainAction")
    mobjValues.ActionQuery = lstrAction = eFunctions.Menues.TypeActions.clngActionQuery Or lstrAction = eFunctions.Menues.TypeActions.clngActionDuplicate Or lstrAction = eFunctions.Menues.TypeActions.clngActioncut
    Response.Write(mobjValues.StyleSheet())
    mobjMenu = New eFunctions.Menues

    mobjValues.sCodisplPage = "dp030b"
%>
<html>
<head>
    <meta name="GENERATOR" content="Microsoft Visual Studio 6.0">
    <script type="text/javascript" src="/VTimeNet/Scripts/GenFunctions.js"></script>
    <script type="text/javascript" src="/VTimeNet/Scripts/Constantes.js"></script>
    <%Response.Write(mobjMenu.setZone(2, "DP030B", "DP030B.aspx"))
        mobjMenu = Nothing
    %>
    <script>        
        //% InsChangeOptPay : se controla la activación de la sección "Tipo"
        //------------------------------------------------------------------------------------------
        function InsChangeoptType(nValue) {
            //------------------------------------------------------------------------------------------
            with (self.document.forms[0]) {
                if (nValue == '1') {
                    optAplied[0].checked = true;
                    optAplied[0].disabled = true;
                    optAplied[1].disabled = true;
                    optAplied[2].disabled = true;
                    optAplied[3].disabled = true;

                    tctFranchiseRou.value = '';
                    tctFranchiseRou.disabled = true;
                    tcnFranchiseRate.value = '';
                    tcnFranchiseRate.disabled = true;
                    tcnFranchiseFix.value = '';
                    tcnFranchiseFix.disabled = true;
                    tcnFranchiseMin.value = '';
                    tcnFranchiseMin.disabled = true;
                    tcnFranchiseMax.value = '';
                    tcnFranchiseMax.disabled = true;

                    tctFranchiseRouClaim.value = '';
                    tctFranchiseRouClaim.disabled = true;
                    tcnFranchiseRateClaim.value = '';
                    tcnFranchiseRateClaim.disabled = true;
                    tcnFranchiseFixClaim.value = '';
                    tcnFranchiseFixClaim.disabled = true;
                    tcnFranchiseMinClaim.value = '';
                    tcnFranchiseMinClaim.disabled = true;
                    tcnFranchiseMaxClaim.value = '';
                    tcnFranchiseMaxClaim.disabled = true;
                }
                else {
                    optAplied[0].disabled = false;
                    optAplied[1].disabled = false;
                    optAplied[2].disabled = false;
                    optAplied[3].disabled = false;
                    tctFranchiseRou.disabled = false;
                    tcnFranchiseRate.disabled = false;
                    tcnFranchiseFix.disabled = false;
                    tcnFranchiseMin.disabled = false;
                    tcnFranchiseMax.disabled = false;
                    tctFranchiseRouClaim.disabled = false;
                    tcnFranchiseRateClaim.disabled = false;
                    tcnFranchiseFixClaim.disabled = false;
                    tcnFranchiseMinClaim.disabled = false;
                    tcnFranchiseMaxClaim.disabled = false;
                }
            }
        }

        //% InsChangeoptAplied : se controla la activación de la sección "Aplica sobre"
        //------------------------------------------------------------------------------------------
        function InsChangeoptAplied(nValue) {
            //------------------------------------------------------------------------------------------
            with (self.document.forms[0]) {
                if (nValue == '1') {
                    tctFranchiseRou.disabled = true;
                    tcnFranchiseRate.disabled = true;
                    tcnFranchiseFix.disabled = true;
                    tcnFranchiseMin.disabled = true;
                    tcnFranchiseMax.disabled = true;
                    tctFranchiseRouClaim.disabled = true;
                    tcnFranchiseRateClaim.disabled = true;
                    tcnFranchiseFixClaim.disabled = true;
                    tcnFranchiseMinClaim.disabled = true;
                    tcnFranchiseMaxClaim.disabled = true;
                }
                else if ((nValue == '2')) {
                    tctFranchiseRou.disabled = false;
                    tcnFranchiseRate.disabled = false;
                    tcnFranchiseFix.disabled = false;
                    tcnFranchiseMin.disabled = false;
                    tcnFranchiseMax.disabled = false;
                    tctFranchiseRouClaim.disabled = true;
                    tcnFranchiseRateClaim.disabled = true;
                    tcnFranchiseFixClaim.disabled = true;
                    tcnFranchiseMinClaim.disabled = true;
                    tcnFranchiseMaxClaim.disabled = true;
                }
                else if ((nValue == '3')) {
                    tctFranchiseRou.disabled = true;
                    tcnFranchiseRate.disabled = true;
                    tcnFranchiseFix.disabled = true;
                    tcnFranchiseMin.disabled = true;
                    tcnFranchiseMax.disabled = true;
                    tctFranchiseRouClaim.disabled = false;
                    tcnFranchiseRateClaim.disabled = false;
                    tcnFranchiseFixClaim.disabled = false;
                    tcnFranchiseMinClaim.disabled = false;
                    tcnFranchiseMaxClaim.disabled = false;
                }
                else {
                    tctFranchiseRou.disabled = false;
                    tcnFranchiseRate.disabled = false;
                    tcnFranchiseFix.disabled = false;
                    tcnFranchiseMin.disabled = false;
                    tcnFranchiseMax.disabled = false;
                    tctFranchiseRouClaim.disabled = false;
                    tcnFranchiseRateClaim.disabled = false;
                    tcnFranchiseFixClaim.disabled = false;
                    tcnFranchiseMinClaim.disabled = false;
                    tcnFranchiseMaxClaim.disabled = false;                                  
                }
            }
        }
    </script>
</head>
<body onunload="closeWindows();">
    <%  Response.Write(mobjValues.ShowWindowsName("DP030B"))
        lclsTab_GenCov = New eProduct.Tab_gencov
        lclsTab_GenCov.Find(mobjValues.StringToType(Session("nCover"), eFunctions.Values.eTypeData.etdDouble))
        Dim lintFrantype As Integer
        Dim lintFrancApl As Integer
    
        If String.IsNullOrEmpty(lclsTab_GenCov.sFrantype) Then
            lintFrantype = 0
        Else
            lintFrantype = CShort(lclsTab_GenCov.sFrantype)
        End If
    
        If String.IsNullOrEmpty(lclsTab_GenCov.sFrancApl) Then
            lintFrancApl = 0
        Else
            lintFrancApl = CShort(lclsTab_GenCov.sFrancApl)
        End If

    %>
    <form method="post" id="FORM" name="frmDP030B" action="valCoverSeq.aspx?nMainAction=<%=Request.QueryString.Item("nMainAction")%>">
    <table width="100%">
        <tr>
            <td>
                <label id="14171">
                    <%= GetLocalResourceObject("tctPremiumRouCaption") %></label>
            </td>
            <td>
                <%=mobjValues.TextControl("tctPremiumRou", 12, lclsTab_GenCov.sRoupremi,  , GetLocalResourceObject("tctPremiumRouToolTip"))%>
            </td>
            <td>
                <label id="14167">
                    <%= GetLocalResourceObject("valCoverInCaption") %></label>
            </td>
            <td>
                <%
                    mobjValues.Parameters.Add("sStatregt", "1", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    mobjValues.Parameters.Add("nCoverGen", Session("nCover"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    Response.Write(mobjValues.PossiblesValues("valCoverIn", "tabTabGenCov", eFunctions.Values.eValuesType.clngWindowType, CStr(lclsTab_GenCov.nCover_in), True, , , , , , , , GetLocalResourceObject("valCoverInToolTip")))
                %>
            </td>
        </tr>
        <tr>
            <td>
                <label id="14168">
                    <%= GetLocalResourceObject("tcnPremiumFixCaption") %></label>
            </td>
            <td>
                <%=mobjValues.NumericControl("tcnPremiumFix", 18, CStr(lclsTab_GenCov.nPremifix), , GetLocalResourceObject("tcnPremiumFixToolTip"), True, 6)%>
            </td>
            <td>
                <label id="14172">
                    <%= GetLocalResourceObject("tcnRateCaption") %></label>
            </td>
            <td>
                <%=mobjValues.NumericControl("tcnRate", 5, CStr(lclsTab_GenCov.nPremirat),  , GetLocalResourceObject("tcnRateToolTip"), True , 2)%>
            </td>
        </tr>
        <tr>
            <td>
                <label id="14170">
                    <%= GetLocalResourceObject("tcnPremiumMinCaption") %></label>
            </td>
            <td>
                <%=mobjValues.NumericControl("tcnPremiumMin", 18, CStr(lclsTab_GenCov.nPremimin), , GetLocalResourceObject("tcnPremiumMinToolTip"), True, 6)%>
            </td>
            <td>
                <label id="14169">
                    <%= GetLocalResourceObject("tcnPremiumMaxCaption") %></label>
            </td>
            <td>
                <%=mobjValues.NumericControl("tcnPremiumMax", 18, CStr(lclsTab_GenCov.nPremimax),  , GetLocalResourceObject("tcnPremiumMaxToolTip"), true , 6)%>
            </td>
        </tr>
        <tr>
            <td width="100%" colspan="4">
                &nbsp;
            </td>
        </tr>
    </table>
    <table width="100%">
        <tr>
            <td colspan="8" class="HighLighted">
                <label id="100375">
                    <%= GetLocalResourceObject("AnchorCaption") %></label>
            </td>
        </tr>
        <tr>
            <td colspan="8" class="HorLine">
            </td>
        </tr>
        <tr>
            <td class="HighLighted" width="15%">
                <label id="100376">
                    <%= GetLocalResourceObject("Anchor2Caption") %></label>
            </td>
            <td class="HighLighted" width="15%">
                <label id="100377">
                    <%= GetLocalResourceObject("Anchor3Caption") %></label>
            </td>
            <td>
            </td>
            <td colspan="5" class="HighLighted" width="60%">
                <label id="100378">
                    <%= GetLocalResourceObject("Anchor4Caption")%></label>
            </td>
        </tr>
        <tr>
            <td class="HorLine">
            </td>
            <td class="HorLine">
            </td>
            <td>
            </td>
            <td colspan="5" class="HorLine">
            </td>
        </tr>
        <tr>
            <td>
                <%= mobjValues.OptionControl(100378, "optType", GetLocalResourceObject("optType_1Caption"), lintFrantype, "1", "InsChangeoptType(this.value);", , , GetLocalResourceObject("optType_1ToolTip"))%>
            </td>
            <td>
                <%= mobjValues.OptionControl(100379, "optAplied", GetLocalResourceObject("optAplied_1Caption"), lintFrancApl, "1", "InsChangeoptAplied(this.value)", , , GetLocalResourceObject("optAplied_1ToolTip"))%>
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                <label id="Label1">
                    <%= GetLocalResourceObject("tcnFranchiseRateCaption") %></label>
            </td>
            <td>
                <%=mobjValues.NumericControl("tcnFranchiseRate", 4, CStr(lclsTab_GenCov.nFrancrat),  , GetLocalResourceObject("tcnFranchiseRateToolTip"),true  , 2)%>
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                <label id="0">
                    <%= GetLocalResourceObject("tcnFranchiseFixCaption") %></label>
            </td>
            <td>
                <%=mobjValues.NumericControl("tcnFranchiseFix", 18, CStr(lclsTab_GenCov.nFrancFix), , GetLocalResourceObject("tcnFranchiseFixToolTip"), True, 6)%>
            </td>
        </tr>
        <tr>
            <td>
                <%= mobjValues.OptionControl(0, "optType", GetLocalResourceObject("optType_2Caption"), CStr(3 - lintFrantype), "2", "InsChangeoptType(this.value);", , , GetLocalResourceObject("optType_2ToolTip"))%>
            </td>
            <td>
                <%= mobjValues.OptionControl(100380, "optAplied", GetLocalResourceObject("optAplied_2Caption"), CStr(3 - lintFrancApl), "2", "InsChangeoptAplied(this.value)", , , GetLocalResourceObject("optAplied_2ToolTip"))%>
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                <label id="Label3">
                    <%= GetLocalResourceObject("tcnFranchiseMinCaption") %></label>
            </td>
            <td>
                <%=mobjValues.NumericControl("tcnFranchiseMin", 18, CStr(lclsTab_GenCov.nFrancMin), , GetLocalResourceObject("tcnFranchiseMinToolTip"), True, 6)%>
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                <label id="Label4">
                    <%= GetLocalResourceObject("tcnFranchiseMaxCaption") %></label>
            </td>
            <td>
                <%=mobjValues.NumericControl("tcnFranchiseMax", 18, CStr(lclsTab_GenCov.nFrancMax),  , GetLocalResourceObject("tcnFranchiseMaxToolTip"),  true, 6)%>
            </td>
        </tr>
        <tr>
            <td>
                <%= mobjValues.OptionControl(100382, "optType", GetLocalResourceObject("optType_3Caption"), CStr(4 - lintFrantype), "3", "InsChangeoptType(this.value);", , , GetLocalResourceObject("optType_3ToolTip"))%>
            </td>
            <td>
                <%= mobjValues.OptionControl(100381, "optAplied", GetLocalResourceObject("optAplied_3Caption"), CStr(4 - lintFrancApl), "3", "InsChangeoptAplied(this.value)", , , GetLocalResourceObject("optAplied_3ToolTip"))%>
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                <label id="Label5">
                    <%= GetLocalResourceObject("tctPremiumRouCaption") %></label>
            </td>
            <td>
                <%=mobjValues.TextControl("tctFranchiseRou", 12, lclsTab_GenCov.sRoufranc,  , "")%>
            </td>
        </tr>
        <tr>
            <td> 
                &nbsp;
            </td>
            <td>
                <%= mobjValues.OptionControl(100381, "optAplied", GetLocalResourceObject("optAplied_4Caption"), CStr(5 - lintFrancApl), "4", "InsChangeoptAplied(this.value)", , , GetLocalResourceObject("optAplied_4ToolTip"))%>
            </td>
            <td> 
                &nbsp;
            </td>
            <td colspan="5" class="HighLighted" width="60%">
                <label id="Label2">
                    <%= GetLocalResourceObject("Anchor5Caption")%></label>
            </td>
        </tr>
        <tr>
            <td></td>
            <td></td>
            <td> 
            </td>
            <td colspan="5" class="HorLine">
            </td>
        </tr>
        <tr>
            <td></td>
            <td></td>
            <td></td>
            <td>
                <label id="Label6">
                    <%= GetLocalResourceObject("tcnFranchiseRateCaption") %></label>
            </td>
            <td>
                <%= mobjValues.NumericControl("tcnFranchiseRateClaim", 4, CStr(lclsTab_GenCov.nFrancRatCla), , GetLocalResourceObject("tcnFranchiseRateClaimToolTip"), True, 2)%>
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                <label id="Label7">
                    <%= GetLocalResourceObject("tcnFranchiseFixCaption") %></label>
            </td>
            <td>
                <%= mobjValues.NumericControl("tcnFranchiseFixClaim", 18, CStr(lclsTab_GenCov.nFrancFixCla), , GetLocalResourceObject("tcnFranchiseFixClaimToolTip"), True, 6)%>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;            
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                <label id="Label8">
                    <%= GetLocalResourceObject("tcnFranchiseMinCaption") %></label>
            </td>
            <td>
                <%= mobjValues.NumericControl("tcnFranchiseMinClaim", 18, CStr(lclsTab_GenCov.nFranxMinCla), , GetLocalResourceObject("tcnFranchiseMinClaimToolTip"), True, 6)%>
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                <label id="Label9">
                    <%= GetLocalResourceObject("tcnFranchiseMaxCaption") %></label>
            </td>
            <td>
                <%= mobjValues.NumericControl("tcnFranchiseMaxClaim", 18, CStr(lclsTab_GenCov.nFranxMaxCla), , GetLocalResourceObject("tcnFranchiseMaxClaimToolTip"), True, 6)%>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;            
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                <label id="Label10">
                    <%= GetLocalResourceObject("tctPremiumRouCaption") %></label>
            </td>
            <td>
                <%= mobjValues.TextControl("tctFranchiseRouClaim", 12, lclsTab_GenCov.sRouFrancCla, , "")%>
            </td>
        </tr>
    </table>
    </form>
    <script>InsChangeoptType(<%=lclsTab_GenCov.sFrantype%>)</script>
    <%
        lclsTab_GenCov = Nothing%>
</body>
</html>
