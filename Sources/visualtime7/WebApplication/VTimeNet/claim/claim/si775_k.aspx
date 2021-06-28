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
    Call mobjNetFrameWork.BeginPage("si775_k")
    '~End Header Block VisualTimer Utility
    Response.CacheControl = "private"

    mobjValues = New eFunctions.Values
    '^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.34.24
    mobjValues.sSessionID = Session.SessionID
    mobjValues.nUsercode = Session("nUsercode")
    '~End Body Block VisualTimer Utility

    mobjValues.sCodisplPage = "si775_k"

    Response.Write("<script>var mlngClaim</script>")
%>
<html>
<head>
    <meta name="GENERATOR" content="Microsoft Visual Studio 6.0">
    <script type="text/javascript" src="/VTimeNet/Scripts/GenFunctions.js"></script>
    <script type="text/javascript" src="/VTimeNet/Scripts/tMenu.js"></script>
    <script>

        //% insCancel: se controla la acción Cancelar de la página
        //--------------------------------------------------------------------------------------------
        function insCancel() {
            //--------------------------------------------------------------------------------------------
            return true;
        }

        //% insFinish: se controla la acción Cancelar de la página
        //--------------------------------------------------------------------------------------------
        function insFinish() {
            //--------------------------------------------------------------------------------------------
            return true;
        }

        //% SetValues: Asigna el parámetro para el "PossiblesValues" Número de orden, habilita y deshabilita según sea el caso.
        //---------------------------------------------------------------------------------------------------------------------
        function SetValues(Field) {
            //---------------------------------------------------------------------------------------------------------------------
            with (document.forms[0]) {
                if (typeof (valServ_Order) != 'undefined')
                    valServ_Order.Parameters.Param1.sValue = Field.value;

                if (Field.value != '') {
                    valServ_Order.disabled = false;
                    btnvalServ_Order.disabled = false;
                }
                else {
                    valServ_Order.disabled = true;
                    btnvalServ_Order.disabled = true;
                    valServ_Order.value = '';
                    UpdateDiv('valServ_OrderDesc', '', 'Normal');
                }
            }
        }
        //% insStateZone: Dependiendo de la acción seleccionada en el menú, se habilitan o deshabilitan los
        //%               RadioButtons de las acciones propias de esta transacción
        //------------------------------------------------------------------------------------------
        function insStateZone() {
            //------------------------------------------------------------------------------------------
            self.document.forms[0].tcnClaim.disabled = false;
            switch (top.frames['fraSequence'].plngMainAction) {
                case 301:
                    {

                        self.document.forms[0].elements["valServ_Order"].Parameters.Param4.sValue = '1,2,7';
                        break;
                    }

                case 302:
                    {

                        self.document.forms[0].elements["valServ_Order"].Parameters.Param4.sValue = '3';
                        break;
                    }

                case 401:
                    {

                        self.document.forms[0].elements["valServ_Order"].Parameters.Param4.sValue = '3,4';
                        break;
                    }
            }

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
            lstrLocation = lstrLocation + "&nClaim=" + self.document.forms[0].elements["tcnClaim"].value;
            lstrLocation = lstrLocation + "&nMainAction=" + top.frames['fraSequence'].plngMainAction;

            document.location.href = lstrLocation;
        }

    
    </script>
    <%Response.Write(mobjValues.StyleSheet())
        mobjMenu = New eFunctions.Menues
        '^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.34.24
        mobjMenu.sSessionID = Session.SessionID
        mobjMenu.nUsercode = Session("nUsercode")
        '~End Body Block VisualTimer Utility
        Response.Write(mobjMenu.MakeMenu("SI775", "SI775_k.aspx", 1, Request.QueryString("sWindowDescript"), CStr(Session("sDesMultiCompany")), CStr(Session("sSche_code"))))
        'UPGRADE_NOTE: Object mobjMenu may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
        mobjMenu = Nothing
    %>
</head>
<body onunload="closeWindows();">
    <form method="POST" name="EntryBudget" action="valClaim.aspx?sMode=2">
    <br>
    <br>
    <table width="100%">
        <tr>
            <td width="20%">
                <label id="0">
                    Número de siniestro</label>
            </td>
            <td>
                <%=mobjValues.NumericControl("tcnClaim", 10, Request.QueryString("nClaim"), , "Número del siniestro que genera la orden de servicio", , , , , , "GetClaimData(this.value); ReloadPage(this.value);", IIf( Request.QueryString("nMainAction") = Nothing , True , False)   )%>
            </td>
                  <td width="15%">
                        <label><%= GetLocalResourceObject("cbeCaseNumberCaption")%>
                        </label>
                 </td>
               <td>
                <%
                    With mobjValues
                        .Parameters.Add("nClaim", mobjValues.StringToType(Request.QueryString("nClaim"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                        mobjValues.BlankPosition = False
                        Response.Write(mobjValues.PossiblesValues("cbeCaseNumber", "TabClaim_cases", eFunctions.Values.eValuesType.clngComboType, Request.QueryString("nCaseNumber"), True, , , , , "ChangeCaseNumber(this)", Request.QueryString("nClaim") = vbNullString, , GetLocalResourceObject("cbeCaseNumberToolTip"), eFunctions.Values.eTypeCode.eString))
                        If Request.QueryString("nCase_Num") = vbNullString Then
                            Response.Write("<script>if(self.document.forms[0].elements['cbeCaseNumber'].value!='') ChangeCaseNumber(self.document.forms[0].elements['cbeCaseNumber']);</script>")
                        End If
                        Response.Write(mobjValues.HiddenControl("cbeCaseNumber_AUX", Request.QueryString("nCase_Num")))
                        Response.Write(mobjValues.HiddenControl("cbeDemantype_AUX", Request.QueryString("nDeman_type")))
                    End With
                %>
            </td>
        </tr>
        <tr>
            <td>
                <label id="0">
                    Número de orden</label>
            </td>
            <td>

                    <%
                        With mobjValues.Parameters
                            .Add("nClaim", mobjValues.StringToType(Request.QueryString("nClaim"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                            .Add("nCase_Num", Request.QueryString("nCase_Num"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                            .Add("nDeman_Type", Request.QueryString("nDeman_Type"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                            .Add("sStatus_ord", "1,2,7", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 22, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                            .Add("sOrdertype", "5", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 22, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                        End With

                        Response.Write(mobjValues.PossiblesValues("valServ_Order", "Tab_Prof_OrdBudget", eFunctions.Values.eValuesType.clngWindowType, , True, ,  ,   , , , IIf(Request.QueryString("nMainAction") = Nothing, True, False), 10, "Número de la orden de servicio asociada al presupuesto"))

                        Response.Write(" <script> ")
                        If Request.QueryString("nMainAction") = 301 Then
                            Response.Write(" document.forms[0].valServ_Order.Parameters.Param4.sValue = '1,2,7'; ")
                        End If
                        
                        If Request.QueryString("nMainAction") = 401 Then
                            Response.Write(" document.forms[0].valServ_Order.Parameters.Param4.sValue = '3,4'; ")
                        End If

                        If Request.QueryString("nMainAction") = 302 Then
                            Response.Write(" document.forms[0].valServ_Order.Parameters.Param4.sValue = '3'; ")
                        End If
                        Response.Write(" </script> ")
                    %>

                
            </td>
           <tr>
           
       
        </tr>
    </table>
    </form>
</body>
</html>
 <%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 11.34.24
    Call mobjNetFrameWork.FinishPage("si775_k")
    'UPGRADE_NOTE: Object mobjNetFrameWork may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
    mobjNetFrameWork = Nothing
    '^End Footer Block VisualTimer%>