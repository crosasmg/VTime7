<%@ Page Language="VB" Explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon"
    EnableViewState="false" %>

<%@ Import Namespace="eNetFrameWork" %>
<%@ Import Namespace="eFunctions" %>
<%@ Import Namespace="eClaim" %>
<script language="VB" runat="Server">
    '^Begin Header Block VisualTimer Utility 1.1 7/4/03 11.35.39
    Dim mobjNetFrameWork As eNetFrameWork.Layout
    '~End Header Block VisualTimer Utility

    '- Objeto para el manejo de las funciones generales de carga de valores
    Dim mobjValues As eFunctions.Values
    Dim mobjMenu As eFunctions.Menues
    Dim mclsClaim_win As eClaim.Claim_win


</script>
<%Response.Expires = -1441
    mobjNetFrameWork = New eNetFrameWork.Layout
    mobjNetFrameWork.sSessionID = Session.SessionID
    mobjNetFrameWork.nUsercode = Session("nUsercode")
    Call mobjNetFrameWork.BeginPage("si012")
    
    mobjValues = New eFunctions.Values
    '^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.35.39
    mobjValues.sSessionID = Session.SessionID
    mobjValues.nUsercode = Session("nUsercode")
    '~End Body Block VisualTimer Utility

    mobjValues.sCodisplPage = "si012"
    mobjMenu = New eFunctions.Menues
    '^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.35.39
    mobjMenu.sSessionID = Session.SessionID
    mobjMenu.nUsercode = Session("nUsercode")
    '~End Body Block VisualTimer Utility

    mclsClaim_win = New eClaim.Claim_win

    mobjValues.ActionQuery = Session("bQuery")

%>
<script>
    //% Obtiene los valores por defecto asociados al caso-siniestro
    //%--------------------------------------------------------------------------------
    function insReaRecover(nDemantype) {
        //%--------------------------------------------------------------------------------
        var nItem, nCasenum;

        //+ Obtiene el texto asociado al combo.
        nItem = self.document.forms[0].cbeRecoverCase.selectedIndex;
        nCasenum = self.document.forms[0].cbeRecoverCase.options[nItem].text;
        //+ Separa el número del caso del resto de la cadena
        nCasenum = nCasenum.substr(0, (nCasenum.indexOf('/')));
        self.document.forms[0].elements['cbeTransac'].Parameters.Param2.sValue = nCasenum;
        //+ Actualiza el número del 		
        self.document.forms[0].tcnCase.value = nCasenum;        
        //+ Obtiene los valores por defecto asociados al caso-siniestro		
        insDefValues('FindRecover', 'nDemantype=' + nDemantype + '&nCasenum=' + nCasenum +
	             '&nTransaction=' + self.document.forms[0].cbeTransac.value, '/VTimeNet/Claim/ClaimSeq');
    }

    //% ClearFields: Blanquea los campos de la ventana
    //---------------------------------------------------------------------------------------------
    function ClearFields()
    //---------------------------------------------------------------------------------------------
    {
        with (self.document.forms[0]) {

            if (cbeTransac.value != "" && cbeTransac.value > 0) {

                tcnCase.value = "";
                cbeTransac.value = "";
                cbeTransac.onblur();
                cbeProvider.value = "";
                cbeProvider.onblur();
                cbeRecoverTy.value = "";
                dEstDate.value = "";
                tctClient.value = "";
                cbeCurrency.value = "";
                tcnIncome.value = "";
                tcnExpense.value = "";
                tctThird.value = "";
                tctCourtCase.value = "";
                cbeStatus = "";
                cbeStatus.onblur();
            }
            if (cbeRecoverCase.value != "0") {
                cbeTransac.disabled = false;

                btncbeTransac.disabled = false;
            }
            else {
                cbeTransac.disabled = true;
                btncbeTransac.disabled = true;
            }
        }
        

        
    }
</script>
<html>
<head>
    <script type="text/javascript" src="/VTimeNet/Scripts/GenFunctions.js"></script>
    <meta name="GENERATOR" content="Microsoft Visual Studio 6.0">
    <%
        With Response
            .Write(mobjValues.StyleSheet())
            .Write(mobjValues.WindowsTitle("SI012", Request.QueryString("sWindowDescript")))
            If Request.QueryString("Type") <> "PopUp" Then
                .Write(mobjMenu.setZone(2, "SI012", Request.QueryString("sWindowDescript"), Request.QueryString("nWindowTy")))
                'UPGRADE_NOTE: Object mobjMenu may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
                mobjMenu = Nothing
                .Write("<script>var nMainAction=top.frames['fraSequence'].plngMainAction;</script>")
            End If
        End With
    %>
</head>
<body onunload="closeWindows();">
    <form method="POST" id="FORM" name="frmSI012" action="ValClaimSeq.aspx?sMode=1&nMainAction=<% =Request.QueryString("nMainAction")%>">
    <a name="BeginPage"></a>
    <%Response.Write(mobjValues.ShowWindowsName("SI012", Request.QueryString("sWindowDescript")))

    %>
    <table width="100%">
        <tr>
            <td colspan="5">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="5" class="HighLighted" align="RIGHT">
                <label id="0">
                    <a name="Trámite de recobro"><%= GetLocalResourceObject("AnchorTrámite de recobroCaption")%></a></label>
            </td>
        </tr>
        <tr>
            <td colspan="5" class="HorLine">
            </td>
        </tr>
        <tr>
            <td>
                <label id="0"><%=GetLocalResourceObject("cbeRecoverCaseCaption")%></label>
            </td>
            <td colspan="4">
                <%
                    mobjValues.Parameters.Add("nClaim", mobjValues.StringToType(CStr(Session("nClaim")), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    Response.Write(mobjValues.PossiblesValues("cbeRecoverCase", "tabBuildingCase", eFunctions.Values.eValuesType.clngComboType, Session("nCasenum"), True, , , , , "ClearFields(); insReaRecover(this.value);", , , GetLocalResourceObject("cbeRecoverCaseToolTip")))
                    Response.Write(mobjValues.HiddenControl("tcnCase", ""))
                %>
            </td>
        </tr>
        <tr>
            <td>
                <label id="0"><%=GetLocalResourceObject("cbeTransacCaption")%></label>
            </td>
            <td>
                <label id="0">
                    <%
                        mobjValues.Parameters.Add("nClaim", mobjValues.StringToType(CStr(Session("nClaim")), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                        mobjValues.Parameters.Add("nCase_num", Session("nCaseNum"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                        Response.Write(mobjValues.PossiblesValues("cbeTransac", "tabrecover", eFunctions.Values.eValuesType.clngWindowType, "", True, , , , , "insReaRecover(self.document.forms[0].elements['cbeRecoverCase'].value);", True, , GetLocalResourceObject("cbeTransacToolTip")))
                    %>
                </label>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
                <label id="0"><%=GetLocalResourceObject("cbeProviderCaption")%></label>
            </td>
            <td colspan="4">
                <% =mobjValues.PossiblesValues("cbeProvider", "TabTab_providerSI012", eFunctions.Values.eValuesType.clngWindowType, "", True, , , , , , , , GetLocalResourceObject("cbeProviderToolTip"))%>
            </td>
        </tr>
        <tr>
            <td>
                <label id="0"><%=GetLocalResourceObject("cbeRecoverTyCaption")%></label>
            </td>
            <td colspan="4">
                <% =mobjValues.PossiblesValues("cbeRecoverTy", "Table216", eFunctions.Values.eValuesType.clngComboType, "", , , , , , , , , GetLocalResourceObject("cbeRecoverTyToolTip"))%>
            </td>
        </tr>
        <tr>
            <td>
                <label id="0"><%=GetLocalResourceObject("dPresDateCaption")%></label>
            </td>
            <td>
                <%'UPGRADE_WARNING: Date was upgraded to Today and has a new behavior. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1041.aspx'%>
                <% =mobjValues.DateControl("dPresDate", CStr(Today), , GetLocalResourceObject("dPresDateToolTip"))%>
            </td>
            <td>
            </td>
            <td>
                <label id="0"><%=GetLocalResourceObject("dEstDateCaption")%></label>
            </td>
            <td>
                <% =mobjValues.DateControl("dEstDate", "", , GetLocalResourceObject("dEstDateToolTip"))%>
            </td>
        </tr>
        <tr>
            <td>
                <label id="0"><%=GetLocalResourceObject("tctClientCaption")%>
                    </label>
            </td>
            <td colspan="4">
                <% =mobjValues.ClientControl("tctClient", "", , GetLocalResourceObject("tctClientToolTip"), , , "lblCliename", False, , , , , , True)%>
            </td>
        </tr>
        <tr>
            <td colspan="5" class="HighLighted" align="RIGHT">
                <label id="0">
                    <a name="Monto estimado"><%=GetLocalResourceObject("AnchorMonto estimadoCaption")%></a></label>
            </td>
        </tr>
        <tr>
            <td colspan="5" class="HorLine">
            </td>
        </tr>
        <tr>
            <td>
                <label id="0"><%=GetLocalResourceObject("cbeCurrencyCaption")%>
                    </label>
            </td>
            <td>
                <% =mobjValues.PossiblesValues("cbeCurrency", "Table11", eFunctions.Values.eValuesType.clngComboType, "", , , , , , , , , GetLocalResourceObject("cbeCurrencyToolTip"))%>
            </td>
            <td>
            </td>
            <td>
                <label id="0"><%=GetLocalResourceObject("tcnIncomeCaption")%>
                    </label>
            </td>
            <td>
                <% =mobjValues.NumericControl("tcnIncome", 18, CStr(0), , GetLocalResourceObject("tcnIncomeToolTip"), True, 6)%>
            </td>
        </tr>
        <tr>
           
              <td>
                <label id="Label1"><%=GetLocalResourceObject("cbeStatusCaption")%> 
                    </label>
            </td>
           
            <td>
                    <% =mobjValues.PossiblesValues("cbeStatus", "Table23", eFunctions.Values.eValuesType.clngComboType,  "", , , , , , , , , GetLocalResourceObject("cbeStatusToolTip"))%>
            </td>

              <td colspan="1">
            </td>   
           
         
            <td>
           
                    </label>
            </td>
            <td>
                <% =mobjValues.HiddenControl ("tcnExpense", 18)%>
            </td>
        </tr>
        <tr>
            <td colspan="5">
            </td>
        </tr>
        <tr>
            <td colspan="5" class="HighLighted" align="RIGHT">
                <label id="0">
                    <a name="Juzgado"><%=GetLocalResourceObject("AnchorJuzgadoCaption")%> </a></label>
            </td>
        </tr>
        <tr>
            <td colspan="5" class="HorLine">
            </td>
        </tr>
        <tr>
            <td>
                <label id="0"><%=GetLocalResourceObject("tctThirdCaption")%>
                    </label>
            </td>
            <td>
                <% =mobjValues.TextControl("tctThird", 40, "", , GetLocalResourceObject("tctThirdToolTip"))%>
            </td>
            <td>
            </td>
            <td>
                <label id="0"><%=GetLocalResourceObject("tctCourtCaseCaption")%>
                    </label>
            </td>
            <td>
                <% =mobjValues.TextControl("tctCourtCase", 10, "", , GetLocalResourceObject("tctCourtCaseToolTip"))%>
            </td>
        </tr>
    </table>
    </form>
    <%

       
        '	Call mclsClaim_win.Add_Claim_win(Session("nClaim"), "SI012", "1", Session("nUserCode"))    
        '	Response.Write "<NOTSCRIPT>top.frames['fraSequence'].document.location='/VTimeNet/Claim/ClaimSeq/Sequence.aspx?nAction=" & Request.QueryString("nMainAction") & "&sGoToNext=NO" & "';</script>"

        'UPGRADE_NOTE: Object mobjValues may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
        mobjValues = Nothing
        'UPGRADE_NOTE: Object mobjMenu may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
        mobjMenu = Nothing
        'UPGRADE_NOTE: Object mclsClaim_win may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
        mclsClaim_win = Nothing

    %>
</body>
</html>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 11.35.39
    Call mobjNetFrameWork.FinishPage("si012")
    'UPGRADE_NOTE: Object mobjNetFrameWork may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
    mobjNetFrameWork = Nothing
    '^End Footer Block VisualTimer%>
