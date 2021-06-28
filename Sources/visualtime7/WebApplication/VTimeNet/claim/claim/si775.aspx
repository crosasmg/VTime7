<%@ Page Language="VB" Explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon"
    EnableViewState="false" %>

<%@ Import Namespace="eNetFrameWork" %>
<%@ Import Namespace="eFunctions" %>
<%@ Import Namespace="eClaim" %>
<%@ Import Namespace="eAgent" %>
<script language="VB" runat="Server">

    '^Begin Header Block VisualTimer Utility 1.1 7/4/03 11.34.24
    Dim mobjNetFrameWork As eNetFrameWork.Layout
    '~End Header Block VisualTimer Utility

    '- Objeto para el manejo de las funciones generales de carga de valores
    Dim mobjValues As eFunctions.Values

    '- Objeto para el manejo del menú
    Dim mobjMenu As eFunctions.Menues

    '- Objeto para el manejo particular de los datos de la página
    Dim mclsAuto_Budget As eClaim.Auto_Budget


    Dim ldblIva As Byte
    Dim ldblAmount As Object
    Dim ldblAmountTotal As Object
    Dim ldtmBudget_date As Object
    Dim lclsTax_Fixval As eAgent.tax_fixval


</script>
<%Response.Expires = -1441
    mobjNetFrameWork = New eNetFrameWork.Layout
    mobjNetFrameWork.sSessionID = Session.SessionID
    mobjNetFrameWork.nUsercode = Session("nUsercode")
    Call mobjNetFrameWork.BeginPage("si775")

    mobjValues = New eFunctions.Values
    '^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.34.24
    mobjValues.sSessionID = Session.SessionID
    mobjValues.nUsercode = Session("nUsercode")
    '~End Body Block VisualTimer Utility

    mobjValues.sCodisplPage = "si775"
    mobjMenu = New eFunctions.Menues
    '^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.34.24
    mobjMenu.sSessionID = Session.SessionID
    mobjMenu.nUsercode = Session("nUsercode")
    '~End Body Block VisualTimer Utility
    mclsAuto_Budget = New eClaim.Auto_Budget
%>
<html>
<head>
    <meta name="GENERATOR" content="Microsoft Visual Studio 6.0">
    <!-- #INCLUDE VIRTUAL="~/VTimeNet/Includes/Constantes.aspx" -->
    <!-- #INCLUDE VIRTUAL="~/VTimeNet/Includes/General.aspx" -->
    <script type="text/javascript" src="/VTimeNet/Scripts/GenFunctions.js"></script>
    <%
        Response.Write(mobjValues.StyleSheet())
        If Request.QueryString("Type") <> "PopUp" Then
            Response.Write(mobjMenu.setZone(2, "SI775", Request.QueryString("sWindowDescript"), Request.QueryString("nWindowTy")))
            'UPGRADE_NOTE: Object mobjMenu may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
            mobjMenu = Nothing
        End If
    %>
    <script>

        //% insShowIVA: se busca el valor correspondiente al IVA para una fecha
        //---------------------------------------------------------------------------------------------
        function insShowIVA(Field) {
            //---------------------------------------------------------------------------------------------
            insDefValues('ShowIVA', 'dEffecdate=' + Field.value, '/VTimeNet/Claim/Claim');
        }

        //%Setvalue: Asigna el valor expandido del código de RUC del taller
        //---------------------------------------------------------------------------------------------
        function Setvalue(Field) {
            //---------------------------------------------------------------------------------------------
            if (Field != '')
                insDefValues('WorkShop', 'sWorkshClient=' + Field, '/VTimeNet/Claim/Claim');
        }
        //% CalculateTotal: Calcula el total una vez que se añaden los montos
        //------------------------------------------------------------------------------------- 
        function CalculateTotal() {
            //------------------------------------------------------------------------------------- 
            var ldblIVA = 0;
            var ldblAmount = 0;
            var ldblDeduc_Amount = 0;
            var ldblDeprec_Amount = 0;
            var ldblAmount_Labor = 0;
            var ldblAmount_Paint = 0;
            var ldblAmount_Mechan = 0;
            var ldblAmount_Part = 0;

            with (self.document.forms[0]) {
                if (tcnIVA.value != "")
                    ldblIVA = insConvertNumber(tcnIVA.value);
                if (tcnDeprec_amount.value != "")
                    ldblDeduc_Amount = insConvertNumber(tcnDeprec_amount.value);
                if (tcnDeduc_amount.value != "")
                    ldblDeprec_Amount = insConvertNumber(tcnDeduc_amount.value);
                if (tcnAmount_Labor.value != "")
                    ldblAmount_Labor = insConvertNumber(tcnAmount_Labor.value);
                if (tcnAmount_Paint.value != "")
                    ldblAmount_Paint = insConvertNumber(tcnAmount_Paint.value);
                if (tcnAmount_Mechan.value != "")
                    ldblAmount_Mechan = insConvertNumber(tcnAmount_Mechan.value);
                if (tcnAmount_Part.value != "")
                    ldblAmount_Part = insConvertNumber(tcnAmount_Part.value);

                if (ldblIVA > 0) {
                    ldblIVA = (ldblIVA / 100) + 1;
                    ldblAmount = (ldblAmount + ldblAmount_Labor + ldblAmount_Paint + ldblAmount_Mechan + ldblAmount_Part) - (ldblDeduc_Amount + ldblDeprec_Amount);
                    ldblTotal = (ldblAmount) * ldblIVA;
                    tcnTotal.value = VTFormat(ldblTotal, '', '', '', 6, true);
                    tcnAmount.value = VTFormat(ldblAmount, '', '', '', 6, true);
                }
                else {
                    ldblAmount = (ldblAmount + ldblAmount_Labor + ldblAmount_Paint + ldblAmount_Mechan + ldblAmount_Part) - (ldblDeduc_Amount + ldblDeprec_Amount);
                    ldblTotal = ldblAmount;
                    tcnTotal.value = VTFormat(ldblTotal, '', '', '', 6, true);
                    tcnAmount.value = VTFormat(ldblTotal, '', '', '', 6, true);
                }
            }
        }
    </script>
</head>
<body onunload="closeWindows();">
    <form method="POST" name="EntryBudget" action="valClaim.aspx?nMainAction=<%=Request.QueryString("nMainAction")%>">
    <%=mobjValues.ShowWindowsName("SI775", Request.QueryString("sWindowDescript"))%>
    <%
        '+ Se obtienen los datos del presupuesto.
        Call mclsAuto_Budget.Find(mobjValues.StringToType(Request.QueryString("nServ_Order"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString("nClaim"), eFunctions.Values.eTypeData.etdDouble) , Session("nUsercode") , Request.QueryString("nMainAction"))

        If mclsAuto_Budget.sWsdeduc <> "1" Then
            mclsAuto_Budget.nDeduc_amount = 0
        End If
        lclsTax_Fixval = New eAgent.tax_fixval
        '+ Se obtiene el porcentaje fijo de IVA (Tabla Tax_Fixval) 
        If lclsTax_Fixval.Find(1, mclsAuto_Budget.dBudget_Date) Then
            ldblIva = mobjValues.StringToType(CStr(lclsTax_Fixval.nPercent), eFunctions.Values.eTypeData.etdDouble, True)
        End If
        'UPGRADE_NOTE: Object lclsTax_Fixval may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
        lclsTax_Fixval = Nothing

        ldblAmount = mobjValues.StringToType(CStr(mclsAuto_Budget.nAmount), eFunctions.Values.eTypeData.etdDouble, True)

        '+ Se obtienen los datos del auto asegurado asociado al siniestro.	
        Call mclsAuto_Budget.FindDataAuto(Request.QueryString("nClaim"))
    %>
    <table width="100%">
        <%If Request.QueryString("nMainAction") = eFunctions.Menues.TypeActions.clngActionUpdate  and  1 = 2 Then%>
        <tr>
            <td colspan="5" class="HighLighted">
                <label id="0"><%=GetLocalResourceObject("AnchorCaption")%>
                   </label>
            </td>
        </tr>
        <tr>
            <td colspan="5" class="HorLine">
            </td>
        </tr>
        <tr>
            <td colspan="2">
            <%=mobjValues.OptionControl(1, "optAction", GetLocalResourceObject("optAction_CStr1Caption"), " ", CStr(1))%>
                     <%=mobjValues.OptionControl(1, "optAction", GetLocalResourceObject("optAction_CStr1Caption"), " ", CStr(1))%>
            </td>
            <td>
                &nbsp;
            </td>
            <td colspan="2">
                <%=mobjValues.OptionControl(2, "optAction", GetLocalResourceObject("optAction_CStr2Caption"),  , CStr(2))%>
   
            </td>
        </tr>
        <tr>
            <td colspan="5" class="HeightRow">
            </td>
        </tr>
        <%End If

            If Request.QueryString("nMainAction") = 302 Then
                'mobjValues.ActionQuery = True
                mobjValues.ActionQuery = False
            Else
                mobjValues.ActionQuery = Request.QueryString("nMainAction") = eFunctions.Menues.TypeActions.clngActionQuery
            End If
            If mobjValues.ActionQuery Then
                If ldblIva > 0 Then
                    ldblAmountTotal = mobjValues.StringToType(CStr(((mclsAuto_Budget.nAmount_Labor + mclsAuto_Budget.nAmount_Mechan + mclsAuto_Budget.nAmount_Paint + mclsAuto_Budget.nAmount_Part) - (mclsAuto_Budget.nDeduc_amount + mclsAuto_Budget.nDeprec_amount)) * ((ldblIva / 100) + 1)), eFunctions.Values.eTypeData.etdDouble, True)
                Else
                    ldblAmountTotal = mobjValues.StringToType(CStr((mclsAuto_Budget.nAmount_Labor + mclsAuto_Budget.nAmount_Mechan + mclsAuto_Budget.nAmount_Paint + mclsAuto_Budget.nAmount_Part) - (mclsAuto_Budget.nDeduc_amount + mclsAuto_Budget.nDeprec_amount)), eFunctions.Values.eTypeData.etdDouble, True)
                End If
            End If
        %>
        <tr>
            <td colspan="5" class="HighLighted">
                <label id="0"><%=GetLocalResourceObject("Anchor2Caption")%>
                    </label>
            </td>
        </tr>
        <tr>
            <td colspan="5" class="HorLine">
            </td>
        </tr>
        <tr>
            <td>
                <label id="0"><%=GetLocalResourceObject("tcdBudget_DateCaption")%>
                    </label>
            </td>
            <td>
                <%=mobjValues.DateControl("tcdBudget_Date", CStr(mclsAuto_Budget.dBudget_Date),  , GetLocalResourceObject("tcdBudget_DateToolTip"),  ,  ,  , "insShowIVA(this);")%>
            </td>
            <td width="13%">
                &nbsp;
            </td>
            <td>
                <label id="0"><%=GetLocalResourceObject("tcnNum_BudgetCaption")%>
                    </label>
            </td>
            <td>
                <%=mobjValues.NumericControl("tcnNum_Budget", 10, CStr(mclsAuto_Budget.nNum_Budget),  ,GetLocalResourceObject("tcnNum_BudgetToolTip"))%>
            </td>
        </tr>
    </table>
    <table width="100%">
        <tr>
            <td>
                <label id="0"><%=GetLocalResourceObject("cbeWorkshClientCaption")%>
                  </label>
            </td>
            <td width="88%">
                <%mobjValues.Parameters.Add("nServ_order", Request.QueryString("nServ_Order"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    mobjValues.Parameters.Add("nClaim", Request.QueryString("nClaim"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    Response.Write(mobjValues.PossiblesValues("cbeWorkshClient", "Tab_ProviderWorkShop", eFunctions.Values.eValuesType.clngWindowType, mclsAuto_Budget.nWorksh, True, , , , , "Setvalue(this.value)", , 14, GetLocalResourceObject("cbeWorkshClientToolTip"), eFunctions.Values.eTypeCode.eNumeric))
                %>
        </tr>
    </table>
    <table width="100%">
        <tr>
            <td colspan="5" class="HighLighted">
                <label id="0"><%=GetLocalResourceObject("Anchor3Caption")%>
                  </label>
            </td>
        </tr>
        <tr>
            <td colspan="5" class="HorLine">
            </td>
        </tr>
        <tr>
            <td>
                <label id="0"><%=GetLocalResourceObject("tctVehBrandCaption")%>
                    </label>
            </td>
            <td>
                <%=mobjValues.TextControl("tctVehBrand", 10, mclsAuto_Budget.sDesVehBrand,  , "", True)%>
            </td>
            <td width="10%">
                &nbsp;
            </td>
            <td>
                <label id="0"><%=GetLocalResourceObject("tctVehModelCaption")%>
                </label>
            </td>
            <td>
                <%=mobjValues.TextControl("tctVehModel", 20, mclsAuto_Budget.sDesVehModel,  , "", True)%>
            </td>
        </tr>
        <tr>
            <td>
                <label id="0"><%=GetLocalResourceObject("tcnYearCaption")%>
                    </label>
            </td>
            <td>
                <%=mobjValues.NumericControl("tcnYear", 4, CStr(mclsAuto_Budget.nYear),  , "",  ,  , True)%>
            </td>
            <td width="10%">
                &nbsp;
            </td>
            <td>
                <label id="0"><%=GetLocalResourceObject("tctChasisCaption")%>
                    </label>
            </td>
            <td>
                <%=mobjValues.TextControl("tctChasis", 40, mclsAuto_Budget.sChassis,  , "", True)%>
            </td>
        </tr>
        <tr>
            <td colspan="5">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="5" class="HighLighted">
                <label id="0"><%=GetLocalResourceObject("Anchor4Caption")%>
                </label>
            </td>
        </tr>
        <tr>
            <td colspan="5" class="HorLine">
            </td>
        </tr>
        <tr>
            <td>
                <label id="0"> <%=GetLocalResourceObject("tcnAmount_LaborCaption")%>
                    </label>
            </td>
            <td>
                <%=mobjValues.NumericControl("tcnAmount_Labor", 18, CStr(mclsAuto_Budget.nAmount_Labor),  , GetLocalResourceObject("tcnAmount_LaborToolTip"), True, 6,  ,  ,  , "CalculateTotal();")%>
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                <label id="0"> <%=GetLocalResourceObject("tcnAmount_PaintCaption")%>
                    </label>
            </td>
            <td>
                <%=mobjValues.NumericControl("tcnAmount_Paint", 18, CStr(mclsAuto_Budget.nAmount_Paint),  , GetLocalResourceObject("tcnAmount_PaintToolTip"), True, 6,  ,  ,  , "CalculateTotal();")%>
            </td>
        </tr>
        <tr>
            <td>
                <label id="0"><%=GetLocalResourceObject("tcnAmount_MechanCaption")%>
                   </label>
            </td>
            <td>
                <%=mobjValues.NumericControl("tcnAmount_Mechan", 18, CStr(mclsAuto_Budget.nAmount_Mechan),  , GetLocalResourceObject("tcnAmount_MechanToolTip"), True, 6,  ,  ,  , "CalculateTotal();")%>
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                <label id="0"><%=GetLocalResourceObject("tcnAmount_PartCaption")%>
                    </label>
            </td>
            <td>
                <%=mobjValues.NumericControl("tcnAmount_Part", 18, CStr(mclsAuto_Budget.nAmount_Part),  , GetLocalResourceObject("tcnAmount_PartToolTip"), True, 6,  ,  ,  , "CalculateTotal();")%>
            </td>
        </tr>
        <tr>
            <td>
                <label id="0"><%=GetLocalResourceObject("tcnDeduc_amountCaption")%>
                    </label>
            </td>
            <td>
                <%=mobjValues.NumericControl("tcnDeduc_amount", 18, CStr(mclsAuto_Budget.nDeduc_amount),  , GetLocalResourceObject("tcnDeduc_amountToolTip"), True, 6,  ,  ,  , "CalculateTotal();", mclsAuto_Budget.sWsDeduc <> "1")%>
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                <label><%=GetLocalResourceObject("tcnDeprec_amountCaption")%>
                    </label>
            </td>
            <td>
                <%=mobjValues.NumericControl("tcnDeprec_amount", 18, CStr(mclsAuto_Budget.nDeprec_amount),  , GetLocalResourceObject("tcnDeprec_amountToolTip"), False, 6,  ,  ,  , "CalculateTotal();")%>
            </td>
        </tr>
        <tr>
            <td>
                <label><%=GetLocalResourceObject("tcnAmountCaption")%>
                    </label>
            </td>
            <td>
                <%=mobjValues.NumericControl("tcnAmount", 18, ldblAmount,  , GetLocalResourceObject("tcnAmountToolTip"), False, 6,  , "", "",  , True)%>
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                <label><%=GetLocalResourceObject("tcnIVACaption")%>
                    </label>
            </td>
            <td>
                <%=mobjValues.NumericControl("tcnIVA", 5, CStr(ldblIva), False, GetLocalResourceObject("tcnIVAToolTip"), False, 2,  ,  ,  ,  , True)%>
            </td>
        </tr>
        <tr>
            <td>
                <label><%=GetLocalResourceObject("tcnTotalCaption")%>
                   </label>
            </td>
            <td>
                <%=mobjValues.NumericControl("tcnTotal", 18, ldblAmountTotal, False, GetLocalResourceObject("tcnTotalToolTip"),  , 6,  ,  ,  ,  , True)%>
            </td>
        </tr>
        <%
            If Not mobjValues.ActionQuery Then
                Response.Write("<script>CalculateTotal();</script>")
            End If
            Response.Write(mobjValues.HiddenControl("tcnServ_Order", Request.QueryString("nServ_Order")))
                  Response.Write(mobjValues.HiddenControl("optAction","0"))
            'UPGRADE_NOTE: Object mclsAuto_Budget may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
            mclsAuto_Budget = Nothing
            'UPGRADE_NOTE: Object mobjValues may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
            mobjValues = Nothing
        %>
    </table>
    </form>
</body>
</html>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 11.34.24
    Call mobjNetFrameWork.FinishPage("si775")
    'UPGRADE_NOTE: Object mobjNetFrameWork may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
    mobjNetFrameWork = Nothing
    '^End Footer Block VisualTimer%>
