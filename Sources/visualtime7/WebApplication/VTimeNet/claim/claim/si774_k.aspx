<%@ Page Language="VB" Explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon"
    EnableViewState="false" %>

<%@ Import Namespace="eNetFrameWork" %>
<%@ Import Namespace="eFunctions" %>
<script language="VB" runat="Server">
    '^Begin Header Block VisualTimer Utility 1.1 7/4/03 11.34.24
    Dim mobjNetFrameWork As eNetFrameWork.Layout

    '- Objeto para el manejo de las funciones generales de carga de valores
    Dim mobjValues As eFunctions.Values
    Dim mobjMenu As eFunctions.Menues


</script>
<%Response.Expires = -1441
    mobjNetFrameWork = New eNetFrameWork.Layout
    mobjNetFrameWork.sSessionID = Session.SessionID
    mobjNetFrameWork.nUsercode = Session("nUsercode")
    Call mobjNetFrameWork.BeginPage("si774_k")
    '~End Header Block VisualTimer Utility
    Response.CacheControl = "private"

    mobjValues = New eFunctions.Values
    '^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.34.24
    mobjValues.sSessionID = Session.SessionID
    mobjValues.nUsercode = Session("nUsercode")
    '~End Body Block VisualTimer Utility

    mobjValues.sCodisplPage = "si774_k"

    Response.Write("<script>var mlngClaim</script>")
%>
<html>
<head>
    <meta name="GENERATOR" content="Microsoft Visual Studio 6.0">
    <script type="text/javascript" src="/VTimeNet/Scripts/GenFunctions.js"></script>
    <script type="text/javascript" src="/VTimeNet/Scripts/tMenu.js"></script>
    <script>

        //% insStateZone: Dependiendo de la acción seleccionada en el menú, se habilitan o deshabilitan los
        //%               RadioButtons de las acciones propias de esta transacción
        //------------------------------------------------------------------------------------------
        function insStateZone() {
            //------------------------------------------------------------------------------------------
            switch (top.frames['fraSequence'].plngMainAction) {
                case 301:
                    {
                       // self.document.forms[0].elements['tcnAction'][0].disabled = true;
                        // self.document.forms[0].elements['tcnAction'][1].disabled = true;
                        self.document.forms[0].elements['tcnAction'][0].disabled = false;
                        self.document.forms[0].elements['tcnAction'][1].disabled = false;
                        self.document.forms[0].elements['tcnAction'][0].checked = true;
                        DisableFields("");
                        self.document.forms[0].elements['tcnActionAUX'].value = 1;
                        self.document.forms[0].elements["tcnServiceOrder"].Parameters.Param4.sValue = '1,2,7';
                        break;
                    }

                case 302:
                    {
                        self.document.forms[0].elements['tcnAction'][0].disabled = false;
                        self.document.forms[0].elements['tcnAction'][1].disabled = false;
                        self.document.forms[0].elements['tcnAction'][0].checked = true;
                        DisableFields("");
                        SetFieldValue(self.document.forms[0].elements['tcnAction'][0].value);
                        self.document.forms[0].elements["tcnServiceOrder"].Parameters.Param4.sValue = '3';
                        break;
                    }

                case 401:
                    {
                        if (typeof (self.document.forms[0].elements['tcnAction']) != 'undefined') {
                            if (self.document.forms[0].elements['tcnAction'].length > 0) {
                                self.document.forms[0].elements['tcnAction'][0].disabled = true;
                                self.document.forms[0].elements['tcnAction'][1].disabled = true;
                            }
                            else {
                                self.document.forms[0].elements['tcnAction'].disabled = false;
                                self.document.forms[0].elements['tcnAction'].checked = true;
                            }
                            DisableFields("");
                            self.document.forms[0].elements['tcnActionAUX'].value = 4;
                        }
                        self.document.forms[0].elements["tcnServiceOrder"].Parameters.Param4.sValue = '3,4';
                        break;
                    }
            }
        }

        //% insCancel: Ejecuta rutinas necesarias en el momento de cancelar la página
        //------------------------------------------------------------------------------------------
        function insCancel() {
            //------------------------------------------------------------------------------------------
            return true;
        }

        //% insFinish: Ejecuta rutinas necesarias en el momento de Finalizar la página
        //------------------------------------------------------------------------------------------
        function insFinish() {
            //------------------------------------------------------------------------------------------
            return true;
        }

        //% GetClaimData: Obtiene la data relacionada con un siniestro en particular - ACM - 17/06/2002
        //---------------------------------------------------------------------------------------------
        function GetClaimData(nClaimNumber) {
            //---------------------------------------------------------------------------------------------
            if (nClaimNumber != "")
                insDefValues('Claim_SI774', 'nClaim=' + nClaimNumber, '/VTimeNet/Claim/Claim');
        }

        //%ChangeCaseNumber: Descompone el string "Caso-tipo de demandante"
        //---------------------------------------------------------------------------------------------
        function ChangeCaseNumber(Field) {
            //---------------------------------------------------------------------------------------------
            var lstrCase_num = '';
            var lstrDeman_type = '';
            var lstrClient = '';
            var lstrString = '';
            var lstrLocation = '';

            lstrString += Field.value;
            lstrCase_num = lstrString.substring(0, (lstrString.indexOf("/")));
            lstrDeman_type = lstrString.substr(lstrString.indexOf("/") + 1, 1);
            lstrClient += lstrString.replace(/.*\//, "");
            lstrLocation += document.location.href;
            lstrLocation = lstrLocation.replace(/&nCase_num.*/, "");
            lstrLocation = lstrLocation + "&nCase_num=" + lstrCase_num + "&nDeman_type=" + lstrDeman_type + "&sClient=" + lstrClient + "&nCaseNumber=" + Field.value;
            document.location.href = lstrLocation;
        }

        //%ReloadPage: Dado el nro. de siniestro, se recarga la página con los valores necesarios para 
        //             obtener el caso-tipo de demandante
        //---------------------------------------------------------------------------------------------
        function ReloadPage(nValue) {
            //---------------------------------------------------------------------------------------------
            var lstrLocation = '';

            lstrLocation += document.location.href;
            lstrLocation = lstrLocation.replace(/&nClaim.*/, "");
            lstrLocation = lstrLocation + "&nClaim=" + self.document.forms[0].elements["tcnClaimNumber"].value;
            lstrLocation = lstrLocation + "&nMainAction=" + top.frames['fraSequence'].plngMainAction;
            lstrLocation = lstrLocation + "&nPolicy=" + self.document.forms[0].elements["tcnPolicy"].value;
            lstrLocation = lstrLocation + "&nBranch=" + self.document.forms[0].elements["tcnBranch"].value;
            lstrLocation = lstrLocation + "&nProduct=" + self.document.forms[0].elements["tcnProduct"].value;
            lstrLocation = lstrLocation + "&nCertif=" + self.document.forms[0].elements["tcnCertif"].value;
            lstrLocation = lstrLocation + "&sCertype=" + self.document.forms[0].elements["tctCertype"].value;

            document.location.href = lstrLocation;
        }

        //%ShowOrderData: Invoca el método para obtener los datos de la orden de servicio.
        //---------------------------------------------------------------------------------------------
        function ShowOrderData(nValue, sForm) {
            //---------------------------------------------------------------------------------------------
            if (nValue != "" && nValue > 0)
                insDefValues('ServiceOrder', 'nServiceOrder=' + nValue + '&nCaseNumber=' + self.document.forms[0].cbeCaseNumber.value +
                                   '&nClaim=' + self.document.forms[0].elements["tcnClaimNumber"].value +
                                   '&sCertype=' + self.document.forms[0].elements["tctCertype"].value +
                                   '&nBranch=' + self.document.forms[0].elements["tcnBranch"].value +
                                   '&nProduct=' + self.document.forms[0].elements["tcnProduct"].value +
                                   '&nPolicy=' + self.document.forms[0].elements["tcnPolicy"].value +
                                   '&nCertif=' + self.document.forms[0].elements["tcnCertif"].value +
                                   '&sForm=' + sForm, '/VTimeNet/Claim/Claim');
        }

        //%DisableFields: Habilita y/o deshabilita los campos de la ventana.
        //---------------------------------------------------------------------------------------------
        function DisableFields(sValue) {
            //---------------------------------------------------------------------------------------------
            if (sValue != "" && sValue == "SI011") {
                self.document.forms[0].elements["tcdEffecdate"].disabled = true;
                self.document.forms[0].elements["tcnClaimNumber"].disabled = true;
                self.document.forms[0].elements["cbeCaseNumber"].disabled = true;
                //		self.document.forms[0].elements["tcnServiceOrder"].disabled = true;
                //		self.document.forms[0].elements["btntcnServiceOrder"].disabled = true;
            }
            else {
                self.document.forms[0].elements["tcdEffecdate"].disabled = false;
                self.document.forms[0].elements["tcnClaimNumber"].disabled = false;
                self.document.forms[0].elements["cbeCaseNumber"].disabled = false;
                //		self.document.forms[0].elements["tcnServiceOrder"].disabled = false;
                //		self.document.forms[0].elements["btntcnServiceOrder"].disabled = false;
            }
        }

        //%SetFieldValue: Obtiene y asigna el valor del tipo de Acción "Aprobar", "Rechazar"
        //--------------------------------------------------------------------------------------------
        function SetFieldValue(nValue) {
            //--------------------------------------------------------------------------------------------
            self.document.forms[0].elements['tcnActionAUX'].value = nValue;
        }
    </script>
    <%Response.Write(mobjValues.StyleSheet())
        mobjMenu = New eFunctions.Menues
        '^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.34.24
        mobjMenu.sSessionID = Session.SessionID
        mobjMenu.nUsercode = Session("nUsercode")
        '~End Body Block VisualTimer Utility
        Response.Write(mobjMenu.MakeMenu("SI774", "SI774_k.aspx", 1, Request.QueryString("sWindowDescript"), CStr(Session("sDesMultiCompany")), CStr(Session("sSche_code"))))
        'UPGRADE_NOTE: Object mobjMenu may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
        mobjMenu = Nothing
    %>
</head>
<body onunload="closeWindows();">
    <form method="POST" id="FORM" name="frmClaimPayment" action="valClaim.aspx?sOriginalForm=<%=Request.QueryString("sOriginalForm")%>">
    <br>
    <br>
    <table width="100%">
        <tr>
            <td width="15%">
                <label><%= GetLocalResourceObject("tcdEffecdateCaption")%>
                    </label>
            </td>
            <td>
                <%'UPGRADE_WARNING: Date was upgraded to Today and has a new behavior. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1041.aspx'%>
                <%=mobjValues.DateControl("tcdEffecdate", CStr(Today), , GetLocalResourceObject("tcdEffecdateToolTip"))%>
            </td>
            <td width="15%">
                <label><%= GetLocalResourceObject("tcnClaimNumberCaption")%>
                   </label>
            </td>
            <td>
                <%=mobjValues.NumericControl("tcnClaimNumber", 10, Request.QueryString("nClaim"), , GetLocalResourceObject("tcnClaimNumberToolTip"), False, 0, , , , "GetClaimData(this.value); ReloadPage(this.value)")%>
            </td>
        </tr>
        <tr>
            <td width="15%">
                <label><%= GetLocalResourceObject("cbeCaseNumberCaption")%>
                    </label>
            </td>
            <td>
                <%
                    With mobjValues
                        .Parameters.Add("nClaim", mobjValues.StringToType(Request.QueryString("nClaim"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                        mobjValues.BlankPosition = False
                        If Request.QueryString("sOriginalForm") = "SI011" Then
                            Response.Write(mobjValues.PossiblesValues("cbeCaseNumber", "TabClaim_cases", eFunctions.Values.eValuesType.clngComboType, Request.QueryString("nCaseNumber"), True, , , , , "ChangeCaseNumber(this)", Request.QueryString("nClaim") = vbNullString, , GetLocalResourceObject("cbeCaseNumberToolTip"), eFunctions.Values.eTypeCode.eString))
                        Else
                            Response.Write(mobjValues.PossiblesValues("cbeCaseNumber", "TabClaim_cases", eFunctions.Values.eValuesType.clngComboType, Request.QueryString("nCaseNumber"), True, , , , , "ChangeCaseNumber(this)", Request.QueryString("nClaim") = vbNullString, , GetLocalResourceObject("cbeCaseNumberToolTip"), eFunctions.Values.eTypeCode.eString))
                        End If
                        If Request.QueryString("nCase_Num") = vbNullString Then
                            Response.Write("<script>if(self.document.forms[0].elements['cbeCaseNumber'].value!='') ChangeCaseNumber(self.document.forms[0].elements['cbeCaseNumber']);</script>")
                        End If
                        Response.Write(mobjValues.HiddenControl("cbeCaseNumber_AUX", Request.QueryString("nCase_Num")))
                        Response.Write(mobjValues.HiddenControl("cbeDemantype_AUX", Request.QueryString("nDeman_type")))
                        Session("nDemandantType_SI774") = Request.QueryString("nDeman_Type")
                    End With
                %>
            </td>
            <td width="10%">
                <label><%= GetLocalResourceObject("tcnServiceOrderCaption")%>
                    </label>
            </td>
            <td>
                <%
                    With mobjValues.Parameters
                        .Add("nClaim", Request.QueryString("nClaim"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                        .Add("nCase_Num", Request.QueryString("nCase_Num"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                        .Add("nDeman_Type", Request.QueryString("nDeman_Type"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                        .Add("sStatus_ord", "1,2,7", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 22, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                        .Add("sOrdertype", "4", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 22, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    End With
                    If Request.QueryString("sOriginalForm") = "SI011" Then
                        Response.Write(mobjValues.PossiblesValues("tcnServiceOrder", "Tab_Prof_OrdBudget", eFunctions.Values.eValuesType.clngWindowType, Request.QueryString("nService_Order"), True, , , , , "ShowOrderData('+this.value+', 'SI011')", Request.QueryString("nClaim") = vbNullString, 10, GetLocalResourceObject("tcnServiceOrderToolTip")))
                    Else
                        Response.Write(mobjValues.PossiblesValues("tcnServiceOrder", "Tab_Prof_OrdBudget", eFunctions.Values.eValuesType.clngWindowType, , True, , , , , "ShowOrderData(this.value)", Request.QueryString("nClaim") = vbNullString, 10, GetLocalResourceObject("tcnServiceOrderToolTip")))
                    End If
                    
                    Response.Write(" <script> ")
                    If Request.QueryString("nMainAction") = 301 Then
                        Response.Write(" document.forms[0].tcnServiceOrder.Parameters.Param4.sValue = '1,2,7'; ")
                    End If
                        
                    If Request.QueryString("nMainAction") = 401 Then
                        Response.Write(" document.forms[0].tcnServiceOrder.Parameters.Param4.sValue = '3,4'; ")
                    End If

                    If Request.QueryString("nMainAction") = 302 Then
                        Response.Write(" document.forms[0].tcnServiceOrder.Parameters.Param4.sValue = '3'; ")
                    End If
                    Response.Write(" </script> ")
                    %>
            </td>
        </tr>
        <td width="15%">
            <label><%= GetLocalResourceObject("AnchorCaption")%>
                </label>
        </td>
        <td>
            <%=mobjValues.DIVControl("cbeMark",  , "")%>
        </td>
        <td width="15%">
            <label><%= GetLocalResourceObject("Anchor2Caption")%>
                </label>
        </td>
        <td>
            <%=mobjValues.DIVControl("cbeModel",  , "")%>
        </td>
        </TR>
        <tr>
            <td width="15%">
                <label><%= GetLocalResourceObject("Anchor3Caption")%>
                    </label>
            </td>
            <td>
                <%=mobjValues.DIVControl("tcnYear",  , "")%>
            </td>
            <td width="15%">
                <label><%= GetLocalResourceObject("Anchor4Caption")%>
                    </label>
            </td>
            <td>
                <%=mobjValues.DIVControl("tctChasisCode",  , "")%>
            </td>
        </tr>
        <%
            If Request.QueryString("sOriginalForm") <> "SI011" Then
        %>
        <tr>
            <td width="15%">
                <label><%= GetLocalResourceObject("Anchor5Caption")%>
                   </label>
            </td>
            <td>
                <%=mobjValues.OptionControl(3, "tcnAction", GetLocalResourceObject("tcnAction_CStr2Caption"), , CStr(2), "SetFieldValue(this.value);", True)%>
            </td>
            <td>
                <%=mobjValues.OptionControl(4, "tcnAction", GetLocalResourceObject("tcnAction_CStr3Caption"), , CStr(3), "SetFieldValue(this.value);", True)%>
            </td>
            <td>
            </td>
        </tr>
        <%	
        End If
        %>
    </table>
    <%
        If Request.QueryString("sOriginalForm") <> vbNullString And Request.QueryString("sOriginalForm") = "SI011" Then
            Response.Write(mobjValues.HiddenControl("tcnTypeOrder", Request.QueryString("nTypeOrder")))
            Response.Write(mobjValues.HiddenControl("tctStateOrder", Request.QueryString("sStateOrder")))
            Response.Write(mobjValues.HiddenControl("tcnDemandantType", Request.QueryString("nDemandantType")))
            Response.Write(mobjValues.HiddenControl("tctOriginalForm", Request.QueryString("sOriginalForm")))
            Session("sOriginalForm") = Request.QueryString("sOriginalForm")
            Response.Write(mobjValues.HiddenControl("tcnTransaction", ""))
        Else
            Response.Write(mobjValues.HiddenControl("tcnTypeOrder", ""))
            Response.Write(mobjValues.HiddenControl("tctStateOrder", ""))
            Response.Write(mobjValues.HiddenControl("tcnDemandantType", ""))
            Response.Write(mobjValues.HiddenControl("tcnTransaction", ""))
            Response.Write(mobjValues.HiddenControl("tctOriginalForm", Request.QueryString("sOriginalForm")))
            Session("sOriginalForm") = Request.QueryString("sOriginalForm")
        End If

        Response.Write(mobjValues.HiddenControl("tctCertype", CStr(Session("tctCertype"))))
        Response.Write(mobjValues.HiddenControl("tcnBranch", CStr(Session("tcnBranch"))))
        Response.Write(mobjValues.HiddenControl("tcnProduct", CStr(Session("tcnProduct"))))
        Response.Write(mobjValues.HiddenControl("tcnPolicy", CStr(Session("tcnPolicy"))))
        Response.Write(mobjValues.HiddenControl("tcnCertif", CStr(Session("tcnCertif"))))
        Response.Write(mobjValues.HiddenControl("tcnActionAUX", CStr(0)))

        'UPGRADE_NOTE: Object mobjValues may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
        mobjValues = Nothing
    %>
    </form>
</body>
</html>
<%
    Response.Write("<script>DisableFields('SI011');</script>")

    If Request.QueryString("sOriginalForm") <> vbNullString And Request.QueryString("sOriginalForm") = "SI011" Then
        Response.Write("<script>if(insDisabledButton(document.A401)) ClientRequest(401,2);</script>")
        Response.Write("<script>if(insDisabledButton(document.A390)) ClientRequest(390,6);</script>")
    Else
        Response.Write("<script>ShowOrderData('" & Request.QueryString("nService_Order") & "');</script>")
        Response.Write("<script>ClientRequest(top.frames['fraSequence'].plngMainAction, 6);</script>")
    End If

    If Request.QueryString("sOriginalForm") <> vbNullString And Request.QueryString("sOriginalForm") = "SI011" Then
        Response.Write("<script>GetClaimData(self.document.forms[0].elements['tcnClaimNumber'].value);</script>")
        Response.Write("<script>ShowOrderData(self.document.forms[0].elements['tcnServiceOrder'].value, 'SI011');</script>")
    End If

%>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 11.34.24
    Call mobjNetFrameWork.FinishPage("si774_k")
    'UPGRADE_NOTE: Object mobjNetFrameWork may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
    mobjNetFrameWork = Nothing
    '^End Footer Block VisualTimer%>
