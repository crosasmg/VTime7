<%@ Page Language="VB" explicit="true"  Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false" %>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="ePolicy" %>
<%@ Import namespace="eProduct" %>
<script language="VB" runat="Server">
    '^Begin Header Block VisualTimer Utility 1.1 31/3/03 19.42.05
    Dim mobjNetFrameWork As eNetFrameWork.Layout

    '- Objeto para el manejo de las funciones generales de carga de valores
    Dim mobjValues As eFunctions.Values

    '- Objeto para el manejo de las rutinas genéricas
    Dim mobjMenu As eFunctions.Menues
    '~End Body Block VisualTimer Utility

    Dim mclsPolicy As ePolicy.Policy

    Dim mclsProduct As eProduct.Product

    Dim mclsTransport As ePolicy.transport


    '% insPreTR001: Realiza la lectura de los campos a mostrar en la forma TR001
    '----------------------------------------------------------------------------------------------
    Private Sub insPreTR001()
        '----------------------------------------------------------------------------------------------
        Call mclsPolicy.Find(Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(Session("npolicy"), eFunctions.Values.eTypeData.etdDouble))
	
	
        Call mclsProduct.Find(mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate))
	
        Call mclsTransport.Find(Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(Session("npolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate))
	
        With mclsTransport
            If mclsPolicy.sDeclari = "1" Then
                If .nDep_rate = eRemoteDB.Constants.intNull And .nDecla_freq = eRemoteDB.Constants.intNull Then
                    .nDep_rate = 100
				
                    If .nEstAmount <> eRemoteDB.Constants.intNull Then
                        .nDep_prem = .nEstAmount * .nDep_rate / 100
                    End If
                End If
            Else
                .nDecla_freq = eRemoteDB.Constants.intNull
                .nDep_prem = eRemoteDB.Constants.intNull
                .nDep_rate = eRemoteDB.Constants.intNull
            End If
        End With
	
    End Sub

</script>
<%Response.Expires = -1441
    mobjNetFrameWork = New eNetFrameWork.Layout
    mobjNetFrameWork.sSessionID = Session.SessionID
    mobjNetFrameWork.nUsercode = Session("nUsercode")
    Call mobjNetFrameWork.BeginPage("TR001")
    '~End Header Block VisualTimer Utility
    Response.CacheControl = "private"
    mobjValues = New eFunctions.Values
    '^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.42.05
    mobjValues.sSessionID = Session.SessionID
    mobjValues.nUsercode = Session("nUsercode")
    '~End Body Block VisualTimer Utility

    mobjValues.sCodisplPage = Request.QueryString.Item("sCodispl")
    mobjMenu = New eFunctions.Menues
    '^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.42.05
    mobjMenu.sSessionID = Session.SessionID
    mobjMenu.nUsercode = Session("nUsercode")
    mclsPolicy = New ePolicy.Policy
    mclsProduct = New eProduct.Product
    mclsTransport = New ePolicy.transport

    mobjValues.ActionQuery = Session("bQuery")
    Call insPreTR001()
%>

<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<%With Response
        .Write(mobjValues.StyleSheet())
        .Write(mobjMenu.setZone(2, "TR001", Request.QueryString.Item("sWindowDescript"), CShort(Request.QueryString.Item("nWindowTy"))))
    End With
    mobjMenu = Nothing
%>
<HTML>
<HEAD>
<SCRIPT>

        //insChangeValue: Actualiza el % de prima de depósito y el monto de prima estimado
        //--------------------------------------------------------------------------------
        function insChangeValue(Field) {
            //--------------------------------------------------------------------------------
            var ldblField
            with (self.document.forms[0]) {
                if (Field.name == 'tcnDep_rate') {
                    if ((insConvertNumber(tcnEstAmount.value) > 0) && (insConvertNumber(Field.value) > 0) && (tcnRate_Apply.value > 0)) {
                        ldblField = (insConvertNumber(Field.value) * insConvertNumber(tcnEstAmount.value));
                        ldblField = (insConvertNumber(ldblField) / 100) * (insConvertNumber(tcnRate_Apply.value));
                        ldblField = ldblField / 1000
                        tcnDep_prem.value = VTFormat(ldblField, '', '<%=Session("Session_DecimalSeparator")%>', '<%=Session("Session_ThousandSeparator")%>', tcnDep_prem.DecimalPlace);
                    }
                    else
                        tcnDep_prem.value = VTFormat(0, '', '<%=Session("Session_DecimalSeparator")%>', '<%=Session("Session_ThousandSeparator")%>', tcnDep_prem.DecimalPlace);
                }
                if (Field.name == 'tcnDep_prem') {
                    if ((insConvertNumber(tcnEstAmount.value) > 0) && (insConvertNumber(Field.value) > 0)) {
                        ldblField = ((insConvertNumber(Field.value) * 100) / (insConvertNumber(tcnEstAmount.value))) * (insConvertNumber(tcnRate_Apply.value))    ;
                        tcnDep_rate.value = VTFormat(ldblField, '', '<%=Session("Session_DecimalSeparator")%>', '<%=Session("Session_ThousandSeparator")%>', tcnDep_rate.DecimalPlace);
                    }
                }
                if (Field.name == 'tcnEstAmount') {
                    if (insConvertNumber(Field.value) > 0) { 
                        if ((insConvertNumber(tcnDep_rate.value) > 0) && (insConvertNumber(tcnDep_prem.value) > 0)) {
                            ldblField = (insConvertNumber(Field.value) * insConvertNumber(tcnDep_rate.value));
                            ldblField = (ldblField / 100) * (insConvertNumber(tcnRate_Apply.value));
                            ldblField = ldblField /1000
                            tcnDep_prem.value = VTFormat(ldblField, '', '<%=Session("Session_DecimalSeparator")%>', '<%=Session("Session_ThousandSeparator")%>', tcnDep_prem.DecimalPlace);
                        }
                        if ((insConvertNumber(tcnDep_rate.value) > 0) && (tcnDep_prem.value == '') && (tcnRate_Apply.value > 0 )) {
                            ldblField = ((insConvertNumber(Field.value)) * (insConvertNumber(tcnDep_rate.value)));
                            ldblField = (insConvertNumber(ldblField) / 100) * (insConvertNumber(tcnRate_Apply.value) );
                            ldblField = ldblField / 1000
                            tcnDep_prem.value = VTFormat(ldblField, '', '<%=Session("Session_DecimalSeparator")%>', '<%=Session("Session_ThousandSeparator")%>', tcnDep_prem.DecimalPlace);
                        }

                        if ((tcnDep_rate.value == '') && (tcnDep_prem.value > 0)) {
                            ldblField = ((insConvertNumber(tcnDep_prem.value) * 100) / insConvertNumber(Field.value)) * (insConvertNumber(tcnRate_Apply.value));
                            ldblField = ldblField / 1000
                            tcnDep_rate.value = VTFormat(ldblField, '', '<%=Session("Session_DecimalSeparator")%>', '<%=Session("Session_ThousandSeparator")%>', tcnDep_rate.DecimalPlace);
                        }
                      }


                    else
                        tcnDep_prem.value = VTFormat(0, '', '<%=Session("Session_DecimalSeparator")%>', '<%=Session("Session_ThousandSeparator")%>', tcnDep_prem.DecimalPlace);
                }

                if (Field.name == 'tcnRate_Apply') {
                    if ((insConvertNumber(tcnEstAmount.value) > 0) && (insConvertNumber(Field.value) > 0) && (tcnDep_rate.value > 0)) {
                        ldblField = (insConvertNumber(tcnEstAmount.value) * insConvertNumber(tcnDep_rate.value));
                        ldblField = (insConvertNumber(ldblField) / 100) * (insConvertNumber(tcnRate_Apply.value));
                        ldblField = ldblField / 1000
                        tcnDep_prem.value = VTFormat(ldblField, '', '<%=Session("Session_DecimalSeparator")%>', '<%=Session("Session_ThousandSeparator")%>', tcnDep_rate.DecimalPlace);
                    }
                }
//- Variable para el control de versiones 
    document.VssVersion="$$Revision: 3 $|$$Date: 17/09/04 10:21a $|$$Author: Fbonilla $"
</SCRIPT>
    <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">    
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="frmTransport" ACTION="ValPolicySeq.aspx?x=1">
    
    <%
        Response.Write(mobjValues.ShowWindowsName("TR001", Request.QueryString.Item("sWindowDescript")))

    %>
    <td>
        &nbsp;
    </td>
    <table width="100%">
        <tr>
            <td width="35%">
                &nbsp;
            </td>
            <td width="10%">
                <label id="2557">
                    <%= GetLocalResourceObject("cbeCurrencyCaption")%></label>
            </td>
            <%
                With mobjValues.Parameters
                    .Add("sCertype", Session("sCertype"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Add("nProduct", Session("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Add("nPolicy", Session("nPolicy"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Add("nCertif", Session("nCertif"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Add("dEffecdate", Session("dEffecdate"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                End With
            %>
            <td>
                <%mobjValues.BlankPosition = False
                    Response.Write(mobjValues.PossiblesValues("cbeCurrency", "TabCurren_pol", 1, CStr(0), True, False, , , , , False, , GetLocalResourceObject("cbeCurrencyToolTip")))%>
            </td>
        </tr>
    </table>
    <td>
        &nbsp;
    </td>
    <table width="100%">
        <td colspan="8" width="100%" class="HighLighted">
            <label id="14767">
                <%= GetLocalResourceObject("AnchorDeclaracionesCaption")%></label>
        </td>
        <tr>
            <td colspan="8" class="Horline">
            </td>
        </tr>
        <tr>
            <td>
                <label id="2558">
                    <%= GetLocalResourceObject("tcnMaxLimTripCaption")%></label>
            </td>
            <td>
                <%= mobjValues.NumericControl("tcnMaxLimTrip", 12, mobjValues.StringToType(CStr(mclsTransport.nMaxLimTrip), eFunctions.Values.eTypeData.etdDouble, True), False, GetLocalResourceObject("tcnMaxLimTripToolTip"), True, , , , , , False)%>
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                <label id="14768">
                    <%= GetLocalResourceObject("cbeModalitySuminsCaption")%></label>
            </td>
            <td>
                <%= mobjValues.PossiblesValues("cbeModalitySumins", "Table6034", eFunctions.Values.eValuesType.clngComboType, mobjValues.StringToType(CStr(mclsTransport.nModalitySumins), eFunctions.Values.eTypeData.etdInteger, True), , , , , , , False, , GetLocalResourceObject("cbeModalitySuminsToolTip"))%>
            </td>
        </tr>
        <tr>
                <td>
                    <label id="Label1">
                            <%= GetLocalResourceObject("tcnRate_ApplyCaption")%></label>
                    </td>
                    <td>
                        <%= mobjValues.NumericControl("tcnRate_Apply", 5, mobjValues.StringToType(CStr(mclsTransport.nRate_Apply), eFunctions.Values.eTypeData.etdDouble, True), False, GetLocalResourceObject("tcnRate_ApplyToolTip"), , 2, , , , "insChangeValue(this);")%>
                    </td>
                    <td>
                        &nbsp;
                </td>
             <td>
                <label id="2559">
                    <%= GetLocalResourceObject("tcnDep_rateCaption")%></label>
            </td>
            <td>
                <%= mobjValues.NumericControl("tcnDep_rate", 5, mobjValues.StringToType(CStr(mclsTransport.nDep_rate), eFunctions.Values.eTypeData.etdDouble, True), False, GetLocalResourceObject("tcnDep_rateToolTip"), , 2,  , , , "insChangeValue(this);")%>
            </td>
            
        </tr>
          <tr>
    
            <td>
                <label id="2561">
                    <%= GetLocalResourceObject("tcnEstAmountCaption")%></label>
            </td>
            <td>
                <%= mobjValues.NumericControl("tcnEstAmount", 12, mobjValues.StringToType(CStr(mclsTransport.nEstAmount), eFunctions.Values.eTypeData.etdDouble, True), False, GetLocalResourceObject("tcnEstAmountToolTip"), True, , , , , "insChangeValue(this);")%>
            </td>
        </tr>
        <tr>
            <td width="30%">
                <label id="2560">
                    <%= GetLocalResourceObject("cbenDecla_freqCaption")%></label>
            </td>
            <td>
                <%  mobjValues.TypeList = 1
                    If CStr(Session("sPolitype")) = "1" Then
                        mobjValues.List = "6"
                    Else
                        mobjValues.List = "1,2,3,4,5"
                    End If
                    Response.Write(mobjValues.PossiblesValues("cbenDecla_freq", "Table36", eFunctions.Values.eValuesType.clngComboType, CStr(mclsTransport.nDecla_freq), , , , , , , , 2, GetLocalResourceObject("cbenDecla_freqToolTip"), eFunctions.Values.eTypeCode.eNumeric))%>
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                <label id="14769">
                    <%= GetLocalResourceObject("tcnDep_premCaption")%></label>
            </td>
            <td>
                <%= mobjValues.NumericControl("tcnDep_prem", 18, mobjValues.StringToType(CStr(mclsTransport.nDep_prem), eFunctions.Values.eTypeData.etdDouble, True), , GetLocalResourceObject("tcnDep_premToolTip"), True, 6, , , , "insChangeValue(this);")%>
            </td>
        </tr>
        <tr>
            <td>
                <label id="2562">
                    <%= GetLocalResourceObject("tcnOverLineCaption")%></label>
            </td>
            <td>
                <%=mobjValues.NumericControl("tcnOverLine", 5, mobjValues.StringToType(CStr(mclsTransport.nOverLine), eFunctions.Values.eTypeData.etdDouble, True), False,GetLocalResourceObject("tcnOverLineToolTip"),  ,  ,  ,  ,  ,  , False)%>
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
    </table>
    <%
        If CStr(Session("sPolitype")) = "1" Then
            With Response
                .Write("<script>")
                .Write("self.document.forms[0].cbenDecla_freq.value=6;")
                .Write("self.document.forms[0].cbenDecla_freq.disabled=true;")
                .Write("self.document.forms[0].tcnEstAmount.disabled=true;")
                .Write("self.document.forms[0].tcnMaxLimTrip.disabled=true;")
                .Write("self.document.forms[0].tcnDep_rate.disabled=true;")
                .Write("self.document.forms[0].tcnOverLine.disabled=true;")
                .Write("self.document.forms[0].tcnDep_prem.disabled=true;")
                .Write("self.document.forms[0].cbeCurrency.disabled=true;")
                .Write("self.document.forms[0].tcnDep_rate.value='';")
                .Write("</script>")
            End With
        End If

        If mclsProduct.nDuration = 0 Then
            With Response
                .Write("<script>")
                .Write("self.document.forms[0].tcnEstAmount.disabled=true;")
                .Write("</script>")
            End With
        End If

        mobjValues = Nothing
        mclsPolicy = Nothing
        mclsProduct = Nothing
    %>
    </form>
</body>
</html>
<%'^Begin Footer Block VisualTimer Utility 1.1 31/3/03 19.42.05
    Call mobjNetFrameWork.FinishPage("TR001")
    mobjNetFrameWork = Nothing
    mclsTransport = Nothing
    '^End Footer Block VisualTimer%>
