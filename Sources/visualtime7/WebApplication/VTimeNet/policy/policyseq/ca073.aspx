<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false" %>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eGeneral" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="ePolicy" %>
<script language="VB" runat="Server">
    Dim mobjNetFrameWork As eNetFrameWork.Layout

    Dim mobjValues As eFunctions.Values
    Dim mobjMenu As eFunctions.Menues
    Dim mobjGeneral As eGeneral.GeneralFunction
    Dim mclsRecIng As ePolicy.Reconocimiento_ingresos
    Dim mobjCertificat As ePolicy.Certificat


    Dim lstrQCertif As Object
    Dim mintCurrency As Object

    Dim mstrError As String

    Private Sub insPreCA073()
        mclsRecIng = New ePolicy.Reconocimiento_ingresos
        mobjCertificat = New ePolicy.Certificat
        If mclsRecIng.insPreCA073(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), Session("dEffecdate")) Then
            mintCurrency = mclsRecIng.nCurrency
        End If
        Call mobjCertificat.insPreCA004(Session("sCertype"),
                                mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdInteger),
                                mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdInteger),
                                mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdLong),
                                mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdLong),
                                mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate),
                                mobjValues.StringToType(Session("nTransaction"), eFunctions.Values.eTypeData.etdInteger),
                                Session("sSche_code"))
    End Sub

    Private Sub RecalAmounts()
        Response.Write("<SCRIPT>RecalAmounts(null);</" & "Script>")
    End Sub

</script>
<%Response.Expires = -1441
    mobjNetFrameWork = New eNetFrameWork.Layout
    mobjNetFrameWork.sSessionID = Session.SessionID
    mobjNetFrameWork.nUsercode = Session("nUsercode")
    Call mobjNetFrameWork.BeginPage("CA073")
    '~End Header Block VisualTimer Utility
    Response.CacheControl = "private"

    mobjGeneral = New eGeneral.GeneralFunction
    mstrError = mobjGeneral.insLoadMessage(55963)
    mobjGeneral = Nothing

    With Server
        mobjValues = New eFunctions.Values
        '^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.42.03
        mobjValues.sSessionID = Session.SessionID
        mobjValues.nUsercode = Session("nUsercode")
        '~End Body Block VisualTimer Utility

        mobjValues.sCodisplPage = Request.QueryString.Item("sCodispl")
        mobjMenu = New eFunctions.Menues
        '^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.42.03
        mobjMenu.sSessionID = Session.SessionID
        mobjMenu.nUsercode = Session("nUsercode")
        '~End Body Block VisualTimer Utility
    End With

    mobjValues.ActionQuery = Session("bQuery")
%>
<SCRIPT type="text/javascript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<script type='text/javascript'>
    function RecalAmounts(Field) {
        var nInPrimNetaFP = 0;
        var nInDE = 0;
        var nInIGV = 0;
        var nPrimNetaDC = 0;
        var nMeses = 0;
        var nValueFP = 0;
        var nFinalValueFP = 0;
        var nInDE = 0;
        var nDEFP = 0;
        var nInIGV = 0;
        var nIGVFP = 0;
        var nPrimaComFP = 0;
        var nPrimaTotFP = 0;
        var nMultFP = 0;

        nMeses = parseInt(hcnDuration.value);
        nValueFP = hcbePayFreq.value;

        switch (nValueFP) {
            case "1" : nFinalValueFP = 12 ; break;
            case "2" : nFinalValueFP = 6 ; break;
            case "3" : nFinalValueFP = 3 ; break;
            case "4" : nFinalValueFP = 2 ; break;
            case "5" : nFinalValueFP = 1 ; break;
            default:  nFinalValueFP = nMeses ; break;        
        }
        nMultFP = (nMeses / nFinalValueFP);

        try {
            nInPrimNetaFP = parseFloat(tcnInPrimNetaFP.value);
        } catch (e) {
            nInPrimNetaFP = parseFloat(hcnInPrimNetaFP.value);
        }
        
        /*try {
            nInDE = parseFloat(tcnInDE.value);
        } catch (e) {
            nInDE = parseFloat(hcnInDE.value);
        }*/
        nInDE = 0;
        
        if (!isNaN(nInDE)) {
            if (nInDE > 0) {
                nDEFP = nInPrimNetaFP * (nInDE / 100);                
            }
        } else {
            nInDE = 0;
        }
        //tcnDEFP.value = nDEFP;

        nPrimaComFP = nInPrimNetaFP + nDEFP;

        /*try {
            nInIGV = parseFloat(tcnInIGV.value);
        } catch (e) {
            nInIGV = parseFloat(hcnInIGV.value);
        }*/

        nInIGV = 0;
        
        if (!isNaN(nInIGV)) {
            if (nInIGV > 0) {
                nIGVFP = nPrimaComFP * (nInIGV / 100);
            }
        } else {
            nInIGV = 0;
        }
        //tcnIGVFP.value = Round2(nIGVFP);
        
        nPrimaTotFP = nPrimaComFP + nIGVFP;

        tcnPrimNetaFP.value = Round2(nInPrimNetaFP); //seteando campo
        //tcnPrimaComerFP.value = Round2(nPrimaComFP);
        //tcnPrimaTotalFP.value = Round2(nPrimaTotFP);

        nPrimNetaDC = Round2(nInPrimNetaFP * nMultFP); //(meses / fp) * primaneta 
        
        tcnPrimNetaDC.value = Round2(nPrimNetaDC);
        //tcnPrimaComerDc.value = Round2(nPrimaComFP * nMultFP);
        //tcnPrimaTotalDC.value = Round2(nPrimaTotFP * nMultFP);
        //tcnDEDC.value = Round2(nDEFP * nMultFP);
        //tcnIGVDc.value = Round2(nIGVFP * nMultFP);
    }
    window.onload = function () {
        RecalAmounts(null);
    };
    function Round2(x) {
        return Math.round((x + Number.EPSILON) * 100) / 100;
    }
</script>
<html>
<head>
    <META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
    <%
        Response.Write(mobjMenu.setZone(2, Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), CShort(Request.QueryString.Item("nWindowTy"))))
        mobjMenu = Nothing
        Response.Write(mobjValues.StyleSheet())
    %>
</head>
<body ONUNLOAD="closeWindows();">
    <%=mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"))%>
    <FORM METHOD="POST" ID="FORM" NAME="CA073" ACTION="ValPolicySeq.aspx?mode=1">
        <% Call insPreCA073() %>
        <table width="100%"> 
            <tr>
                <td width="50%">
                    <table width="97%">
                        <tr>
                            <td colspan="4" CLASS="HighLighted">
                                <LABEL ID=99100><a NAME="lblVigencia"><%= GetLocalResourceObject("lblVigCaption") %></a></LABEL>                        
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <hr />
                            </td>
                        </tr>
                        <tr>
                            <td><label id="99102"><%= GetLocalResourceObject("lblSegFrecCaption") %></label></td>
                            <td>
                                <%  With mobjValues
                                        '+ Se carga el valor por defecto del campo Facturación-Según frecuencia (COMBO)
                                        .BlankPosition = False
                                        .Parameters.Add("nBranch", mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdInteger), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                                        .Parameters.Add("nProduct", mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdInteger), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                                        .Parameters.Add("nQuota", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                                        .Parameters.Add("dEffecdate", mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                                        Response.Write(.PossiblesValues("cbePayFreq", "tabPay_fracti", eFunctions.Values.eValuesType.clngComboType, CStr(mobjCertificat.nPayfreq), True, , , , , "ChangeFreq(this)", True, , GetLocalResourceObject("cbePayFreqToolTip"), , 10))
                                    End With
                                %>
                                <input type="hidden" name="hcbePayFreq" id="hcbePayFreq" value="<%=CStr(mobjCertificat.nPayfreq) %>" />
                            </td>
                            <td><label id="99103"><%= GetLocalResourceObject("lblInicioCaption") %></label></td>
                            <td><%=mobjValues.TextControl("tcdStartDate", 14, CStr(mobjCertificat.dStartdate),  , GetLocalResourceObject("tcdStartDateToolTip"),  ,  ,  ,  , True)%></td>
                        </tr>
                        <tr>
                            <td><label id="99104">Meses de duración</label></td>
                            <td>
                                <%=mobjValues.NumericControl("tcnDuration", 4, CStr(mobjCertificat.nDuration),  , GetLocalResourceObject("tcnDurationToolTip"),  ,  ,  ,  ,  , , True, 3)%>
                                <input type="hidden" name="hcnDuration" id="hcnDuration" value="<%=CStr(mobjCertificat.nDuration) %>" />
                            </td>
                            <td><label id="99105">Vencimiento</label></td>
                            <td>
                                <%=mobjValues.TextControl("tcdExpirDate", 14, CStr(mobjCertificat.dExpirdat),  , GetLocalResourceObject("tcdExpirDateToolTip"),  ,  ,  ,  , True)%>
                                <input type="hidden" name="hcdExpirDate" id="hcdExpirDate" value="<%=CStr(mobjCertificat.dExpirdat) %>" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4" CLASS="HighLighted">
                                <LABEL ID=99106><a ><%= GetLocalResourceObject("lblMontoEstimadoCaption") %></a></LABEL>                        
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <hr />
                            </td>
                        </tr>
                        <tr>
                            <td><label id="99107"><%= GetLocalResourceObject("lblIngPrimaNetaCaption") %>:</label></td>
                            <td>
                                <%=mobjValues.NumericControl("tcnInPrimNetaFP", 14, mclsRecIng.nInPrimNetaFP, , , , 6, , , , "RecalAmounts(this);", False, 4)%>
                                <input type="hidden" name="hcnInPrimNetaFP" id="hcnInPrimNetaFP" value="<%= CStr(mclsRecIng.nInPrimNetaFP) %>" />
                            </td>
                            <!--<td><label id="99108"><%= GetLocalResourceObject("lblDerechoEmisionCaption") %></label></td>
                            <td>
                                <%=mobjValues.NumericControl("tcnInDE", 2, mclsRecIng.nInDE, , , , , , , , "RecalAmounts(this);", , 4)%>
                                <input type="hidden" name="hcnInDE" id="hcnInDE" value="<%= CStr(mclsRecIng.nInDE) %>" />
                            </td>-->
                        </tr>
                        <tr>
                            <td></td>
                            <td></td>
                            <!--<td><label id="99109"><%= GetLocalResourceObject("lblIGVCaption") %></label></td>
                            <td><%=mobjValues.NumericControl("tcnInIGV", 2, mclsRecIng.nInIGV, , , , , , , , "RecalAmounts(this);", , 4)%>
                                <input type="hidden" name="hcnInIGV" id="hcnInIGV" value="<%= CStr(mclsRecIng.nInIGV) %>" />
                            </td>-->
                        </tr>
                        <tr>
                            <td>
                                <br />
                            </td>
                        </tr>                        
                    </table>
                </td>
                <td width="50%" style="vertical-align: top;">
                    <table width="100%">
                        <tr>
                             <td colspan="2" CLASS="HighLighted">
                                <LABEL ID=99101><a NAME="lblMoneda"><%= GetLocalResourceObject("lblMonCaption") %></a></LABEL>                        
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2"><hr /></td>
                        </tr>
                        <tr>
                            <td><label id="99113"><%= GetLocalResourceObject("lblMonedaCaption") %></label></td>
                            <td><%Response.Write(mobjValues.PossiblesValues("cbeCurrencDes", "Table11", eFunctions.Values.eValuesType.clngComboType, mintCurrency,  ,  ,  ,  ,  , "insReload(this)", True,  , GetLocalResourceObject("cbeCurrencDesToolTip"))) %></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <table width="100%">
            <tr>
                <td width="50%">
                    <table width="97%">
                        <tr>
                            <td colspan="4" class="HighLighted">
                                <label id="99114"><a><%= GetLocalResourceObject("lblMontoEstimadoFPCaption") %></a></label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <hr />
                            </td>
                        </tr>
                        <tr>
                            <%If Session("NTRANSACTION") = 8 Then %>
                            <td width="50%">
                                <table>
                                    <tr>
                                        <td>
                                            <label id="99110"><%= GetLocalResourceObject("lblPrimaNetaFPCaption") %>:</label></td>                                        
                                        <td><input type="text" disabled name="tcnPrimNetaFP" id="tcnPrimNetaFP" size="10" maxlength="10" tabindex="4" title="" style="text-align:right"/></td>
                                    </tr>
                                    <!--<tr>
                                        <td>
                                            <label id="99111"><%= GetLocalResourceObject("lblPrimaComercialFPCaption") %>:</label></td>
                                        <td><input type="text" disabled name="tcnPrimaComerFP" id="tcnPrimaComerFP" size="10" maxlength="10" tabindex="4" title="" style="text-align:right"/></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label id="99112"><%= GetLocalResourceObject("lblPrimaFPCaption") %>:</label></td>
                                        <td><input type="text" disabled name="tcnPrimaTotalFP" id="tcnPrimaTotalFP" size="10" maxlength="10" tabindex="4" title="" style="text-align:right"/></td>
                                    </tr>-->
                                </table>
                            </td>
                            <td width="50%" style="vertical-align: top;">
                                <!--<table>
                                    <tr>
                                        <td>
                                            <label id="99110"><%= GetLocalResourceObject("lblDEFPCaption") %>:</label></td>
                                        <td><input type="text" disabled name="tcnDEFP" id="tcnDEFP" size="10" maxlength="10" tabindex="4" title="" style="text-align:right"/></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label id="99121"><%= GetLocalResourceObject("lblIgvFPCaption") %>:</label></td>
                                        <td><input type="text" disabled name="tcnIGVFP" id="tcnIGVFP" size="10" maxlength="10" tabindex="4" title="" style="text-align:right"/></td>
                                    </tr>
                                </table>-->
                            </td>
                            <% Else  %>                            
                            <td width="50%">
                                <table>
                                    <tr>
                                        <td>
                                            <label id="99110"><%= GetLocalResourceObject("lblPrimaNetaFPCaption") %>:</label></td>
                                        <td><%=mobjValues.NumericControl("tcnPrimNetaFP", 10, , , , , , , , , "", True, 4)%></td>
                                    </tr>
                                    <!--<tr>
                                        <td>
                                            <label id="99111"><%= GetLocalResourceObject("lblPrimaComercialFPCaption") %>:</label></td>
                                        <td><%=mobjValues.NumericControl("tcnPrimaComerFP", 10, , , , , , , , , "", True, 4)%></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label id="99112"><%= GetLocalResourceObject("lblPrimaFPCaption") %>:</label></td>
                                        <td><%=mobjValues.NumericControl("tcnPrimaTotalFP", 10, , , , , , , , , "", True, 4)%></td>
                                    </tr>-->
                                </table>
                            </td>
                            <td width="50%" style="vertical-align: top;">
                                <!--<table>
                                    <tr>
                                        <td>
                                            <label id="99110"><%= GetLocalResourceObject("lblDEFPCaption") %>:</label></td>
                                        <td><%=mobjValues.NumericControl("tcnDEFP", 10, , , , , , , , , "", True, 4)%></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label id="99121"><%= GetLocalResourceObject("lblIgvFPCaption") %>:</label></td>
                                        <td><%=mobjValues.NumericControl("tcnIGVFP", 10, , , , , , , , , "", True, 4)%></td>
                                    </tr>
                                </table>-->
                            </td>
                            <% End If  %>
                        </tr>
                    </table>

                </td>
                <td width="50%">
                    <table width="97%">
                        <tr>
                            <td colspan="4" class="HighLighted">
                                <label id="99115"><a><%= GetLocalResourceObject("lblMovEstimCaption") %></a></label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <hr />
                            </td>
                        </tr>
                        <tr>
                            <%If Session("NTRANSACTION") = 8 Then %>
                            <td width="50%">
                                <table>
                                    <tr>
                                        <td>
                                            <label id="99116"><%= GetLocalResourceObject("lblPrimaNetaDCCaption") %>:</label></td>
                                        <td><input type="text" disabled name="tcnPrimNetaDC" id="tcnPrimNetaDC" size="10" maxlength="10" tabindex="4" title="" style="text-align:right" /></td>
                                    </tr>
                                    <!--<tr>
                                        <td>
                                            <label id="99117"><%= GetLocalResourceObject("lblPrimaComerDCCaption") %>:</label></td>
                                        <td><input type="text" disabled name="tcnPrimaComerDc" id="tcnPrimaComerDc" size="10" maxlength="10" tabindex="4" title="" style="text-align:right" /></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label id="99118"><%= GetLocalResourceObject("lblPrimaTotalDCCaption") %>:</label></td>
                                        <td><input type="text" disabled name="tcnPrimaTotalDC" id="tcnPrimaTotalDC" size="10" maxlength="10" tabindex="4" title="" style="text-align:right"/></td>
                                    </tr>-->
                                </table>
                            </td>
                            <td width="50%" style="vertical-align: top;">
                                <!--<table>
                                    <tr>
                                        <td>
                                            <label id="99119"><%= GetLocalResourceObject("lblDEDCCaption") %>:</label></td>
                                        <td><input type="text" disabled name="tcnDEDC" id="tcnDEDC" size="10" maxlength="10" tabindex="4" title="" style="text-align:right"/></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label id="99120"><%= GetLocalResourceObject("lblIGVDCCaption") %>:</label></td>
                                        <td><input type="text" disabled name="tcnIGVDc" id="tcnIGVDc" size="10" maxlength="10" tabindex="4" title="" style="text-align:right"/></td>
                                    </tr>
                                </table>-->
                            </td>
                            <% Else  %>
                            <td width="50%">
                                <table>
                                    <tr>
                                        <td>
                                            <label id="99116"><%= GetLocalResourceObject("lblPrimaNetaDCCaption") %>:</label></td>
                                        <td><%=mobjValues.NumericControl("tcnPrimNetaDC", 10, , , , , , , , , "", True, 4)%></td>
                                    </tr>
                                    <!--<tr>
                                        <td>
                                            <label id="99117"><%= GetLocalResourceObject("lblPrimaComerDCCaption") %>:</label></td>
                                        <td><%=mobjValues.NumericControl("tcnPrimaComerDc", 10, , , , , , , , , "", True, 4)%></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label id="99118"><%= GetLocalResourceObject("lblPrimaTotalDCCaption") %>:</label></td>
                                        <td><%=mobjValues.NumericControl("tcnPrimaTotalDC", 10, , , , , , , , , "", True, 4)%></td>
                                    </tr>-->
                                </table>
                            </td>
                            <td width="50%" style="vertical-align: top;">
                                <!--<table>
                                    <tr>
                                        <td>
                                            <label id="99119"><%= GetLocalResourceObject("lblDEDCCaption") %>:</label></td>
                                        <td><%=mobjValues.NumericControl("tcnDEDC", 10, , , , , , , , , "", True, 4)%></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label id="99120"><%= GetLocalResourceObject("lblIGVDCCaption") %>:</label></td>
                                        <td><%=mobjValues.NumericControl("tcnIGVDc", 10, , , , , , , , , "", True, 4)%></td>
                                    </tr>
                                </table>-->
                            </td>
                            <% End If  %>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    <div>
    
    </div>
    </form>
</body>
</html>
<%
    '^Begin Footer Block VisualTimer Utility 1.1 31/3/03 19.42.03
    Call mobjNetFrameWork.FinishPage("CA073")
    mobjNetFrameWork = Nothing
    '^End Footer Block VisualTimer
%>