<%@ Page Language="VB" Explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon"
    EnableViewState="false" %>

<%@ Import Namespace="eNetFrameWork" %>
<%@ Import Namespace="eFunctions" %>
<script language="VB" runat="Server">
    '^Begin Header Block VisualTimer Utility 1.1 7/4/03 11.35.42
    Dim mobjNetFrameWork As eNetFrameWork.Layout
    '~End Header Block VisualTimer Utility

    '- Objeto para el manejo de las funciones generales de carga de valores
    Dim mobjValues As eFunctions.Values
    Dim mobjMenu As eFunctions.Menues

    '- Variables para establecer el número de siniestro a trabajar en la página.
    '- La primera para el combo de casos.  La segunda, para el campo de siniestros.
    Dim mlngClaim As Long
    Dim mstrClaim As String
    Dim mstrCase_num As String

    '- Variables para establecer el tipo de pago.
    Dim mstrPayType As String

    '- Variables para establecer la fecha de pago.    
    Dim mstrPayDate As Date

    Dim mblnDisabled As Boolean
    Dim mblnDisabled_date As Boolean
    
    Dim mblnDisabledCase As Boolean


</script>
<%Response.Expires = -1441
    mobjNetFrameWork = New eNetFrameWork.Layout
    mobjNetFrameWork.sSessionID = Session.SessionID
    mobjNetFrameWork.nUsercode = Session("nUsercode")
    Call mobjNetFrameWork.BeginPage("si008_k")
    '- Se limpa la variabvle de sesión    
    Session("stypeTax") = ""

    mlngClaim = 0
    mstrClaim = ""
    mstrCase_num = 0
    mstrPayType = ""
    'UPGRADE_WARNING: Date was upgraded to Today and has a new behavior. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1041.aspx'
    mstrPayDate = Today
    mblnDisabled = True
    mblnDisabled_date = True
    mblnDisabledCase = True

    mobjValues = New eFunctions.Values
    '^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.35.42
    mobjValues.sSessionID = Session.SessionID
    mobjValues.nUsercode = Session("nUsercode")
    '~End Body Block VisualTimer Utility

    mobjValues.sCodisplPage = "si008_k"

    Response.Write("<script>var mlngClaim</script>")

    If Request.QueryString("nClaim") <> vbNullString Then
        mlngClaim = Request.QueryString("nClaim")
        mstrClaim = Request.QueryString("nClaim")
        Response.Write("<script>mlngClaim=" & Request.QueryString("nClaim") & "</script>")
        If Request.QueryString("sOriginalForm") = "SI738" Then
            mblnDisabled = True
            mblnDisabled_date = True
            mblnDisabledCase = True
        Else
            mblnDisabled = False
            mblnDisabled_date = False
            mblnDisabledCase = False
        End If
    End If

    If Request.QueryString("nPayType") <> vbNullString Then
        mstrPayType = Request.QueryString("nPayType")
        mblnDisabled = True
        mblnDisabled_date = True
        
        
    End If

    If Request.QueryString("nCaseNum") <> vbNullString Then
        mstrCase_num = Request.QueryString("nCaseNum") & "/" & Request.QueryString("nDeman_type") & "/" & Request.QueryString("sClient")
        mblnDisabled = True
        mblnDisabled_date = True
    End If

    If Request.QueryString("dPayDate") <> vbNullString Then
        mstrPayDate = Request.QueryString("dPayDate")
        mblnDisabled = True
        mblnDisabled_date = True
        If Request.QueryString("sOriginalForm") = "SI021" Then
            mblnDisabled_date = False 
        End If
    Else
        'UPGRADE_WARNING: Date was upgraded to Today and has a new behavior. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1041.aspx'
        mstrPayDate = Today
        mblnDisabled = False
        mblnDisabled_date = False
    End If

%>
<html>
<head>
    <%=mobjValues.StyleSheet()%>
    <script type="text/javascript" src="/VTimeNet/Scripts/GenFunctions.js"></script>
    <script type="text/javascript" src="/VTimeNet/Scripts/Constantes.js"></script>
    <script type="text/javascript" src="/VTimeNet/Scripts/tMenu.js"></script>
    <meta name="GENERATOR" content="Microsoft Visual Studio 6.0">
    <script type="text/javascript">
        //% insStateZone: controla el estado de los campos de la página
        //--------------------------------------------------------------------------------------------
        function insStateZone() {
            //--------------------------------------------------------------------------------------------
        }

        //% insFinish: ejecuta la acción de Finalizar de la página.
        //--------------------------------------------------------------------------------------------
        function insFinish() {
            //--------------------------------------------------------------------------------------------
            return true;
        }
        //% insCancel: ejecuta la acción de Cancelar de la página.
        //--------------------------------------------------------------------------------------------
        function insCancel() {
            //--------------------------------------------------------------------------------------------
            return true
        }       
    </script>
    <%
        mobjMenu = New eFunctions.Menues
        '^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.35.42
        mobjMenu.sSessionID = Session.SessionID
        mobjMenu.nUsercode = Session("nUsercode")
        '~End Body Block VisualTimer Utility
        Response.Write(mobjMenu.MakeMenu("SI008_K", "SI008_k.aspx", 1, Request.QueryString("sWindowDescript"), CStr(Session("sDesMultiCompany")), CStr(Session("sSche_code"))))
        'UPGRADE_NOTE: Object mobjMenu may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
        mobjMenu = Nothing
        If Request.QueryString("nClaim") <> vbNullString Then
            Response.Write("<script>insDefValues('Claim','nClaim= " & Request.QueryString("nClaim") & "&nCase_num=" & Request.QueryString("nCaseNum") & "','/VTimeNet/Claim/PaySeq/')</script>")
        End If
    %>
</head>
<body onunload="closeWindows()">
    <form method="POST" id="FORM" name="frmClaimPayment" action="valPaySeq.aspx?sMode=1">
    <br>
    <br>
    <table width="100%">
        <tr>
            <td width="100px">
                <label id="9124">
                    <%= GetLocalResourceObject("tcdEffecdateCaption") %></label>
            </td>
            <td width="110px">
                <%=mobjValues.DateControl("tcdEffecdate", mstrPayDate,  , GetLocalResourceObject("tcdEffecdateToolTip"),  ,  ,  ,  , CBool(mblnDisabled_date))%>
            </td>
            <td>
                &nbsp;
            </td>
            <td width="60">
                <label id="9121">
                    Siniestro</label>
            </td>
            <td>
                <%=mobjValues.NumericControl("tcnClaim", 10, mstrClaim,  , "Número que identifica al siniestro al que se le realiza el pago",  , 0,  ,  ,  , "ReloadPage(this)", CBool(mblnDisabled))%>
            </td>
        </tr>
        <tr>
            <td>
                <label id="9120">
                    Caso</label>
            </td>
            <td>
                <%
                    With mobjValues
                        .BlankPosition = False
                        .Parameters.Add("nClaim", mlngClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                        Response.Write(mobjValues.PossiblesValues("cbeCase", "tabClaim_cases", eFunctions.Values.eValuesType.clngComboType, mstrCase_num, True, , , , , , CBool(mblnDisabledCase), , "Número del caso involucrado en el pago de siniestro"))
                    End With
                %>
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                <label id="0">
                    Ramo</label>
            </td>
            <td>
                <%Response.Write(mobjValues.DIVControl("cbeBranch", , ""))%>
            </td>
            <%=mobjValues.HiddenControl("hddBranch", "")%>
        </tr>
        <tr>
            <td>
                <label id="0">
                    Producto</label>
            </td>
            <td>
                <%Response.Write(mobjValues.DIVControl("valProduct", , ""))%>
            </td>
            <%=mobjValues.HiddenControl("hddProduct", "")%>
            <td>
                &nbsp;
            </td>
            <td>
                <label id="0">
                    Póliza</label>
            </td>
            <td>
                <%Response.Write(mobjValues.DIVControl("tcnPolicy", , ""))%>
            </td>
            <%=mobjValues.HiddenControl("hddPolicy", "")%>
            <%=mobjValues.HiddenControl("hddCertif", "")%>
        <tr>
            <td>
                <label id="0">
                    Tipo de pago</label>
            </td>
            <td>
                <%=mobjValues.PossiblesValues("cbePay_Type", "Table199", eFunctions.Values.eValuesType.clngComboType, mstrPayType, False,  ,  ,  ,  ,  , CBool(mblnDisabled),  , "Tipo de pago a realizar sobre el siniestro", eFunctions.Values.eTypeCode.eNumeric, 7, False)%>
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                <label id="0">
                    Total prima a descontar (Moneda de pago)</label>
            </td>
            <td>
                <%=mobjValues.NumericControl("tcnPremium", 18, CStr(0),  , "Total del monto de prima en modeda de pago, de los recibos pendientes seleccionados por el usuario", True, 6,  ,  ,  ,  , True, 8)%>
            </td>
        </tr>

    </table>
    <%
        '+ Campos ocultos para guardar datos globales 
        Response.Write(mobjValues.HiddenControl("hddCurrency", ""))

        'UPGRADE_NOTE: Object mobjValues may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
        mobjValues = Nothing

        If Request.QueryString("sOriginalForm") = "SI738" Then
            Response.Write("<script>ClientRequest(301,5);</script>")
        End If
    %>
    </form>
</body>
</html>
<script>
    //% ReloadPage: se recarga la página para asignar valor al combo de Casos
    //-------------------------------------------------------------------------------------------
    function ReloadPage(Claim) {
        //-------------------------------------------------------------------------------------------
        with (self.document.forms[0]) {
            if (tcnClaim.value == 0) {
                cbeCase.value = 0;
                cbeBranch.value = 0;
                valProduct.value = 0;
                UpdateDiv("valProductDesc", "");
            } else {
                if (mlngClaim != tcnClaim.value)
                    self.document.location.href = "SI008_K.aspx?sCodispl=SI008_K" +
											             "&dEffecdate=" + tcdEffecdate.value +
														 "&nClaim=" + tcnClaim.value + "&sConfig=InSequence" +
														 "&nHeight=200"
            }
        }
    }
</script>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 11.35.42
    Call mobjNetFrameWork.FinishPage("si008_k")
    'UPGRADE_NOTE: Object mobjNetFrameWork may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
    mobjNetFrameWork = Nothing
    '^End Footer Block VisualTimer%>
