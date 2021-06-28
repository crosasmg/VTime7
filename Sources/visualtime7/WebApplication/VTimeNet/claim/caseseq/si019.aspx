<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eClaim" %>
<script language="VB" runat="Server">
    '^Begin Header Block VisualTimer Utility 1.1 7/4/03 11.33.47
    Dim mobjNetFrameWork As eNetFrameWork.Layout
    '~End Header Block VisualTimer Utility

    '- Objeto para el manejo de las funciones generales de carga de valores
    Dim mobjValues As eFunctions.Values
    '- Obsejo para el manejo de la secuencia de menués.    
    Dim mobjMenu As eFunctions.Menues

    '+Se busca la información del tercero asociado al siniestro
    Dim lobjClaim_thir As eClaim.Claim_thir

    Dim lobjOptionsInstallation As eGeneral.OptionsInstallation
    Dim msAutomovileWebService As String

</script>
<%  Response.Expires = -1441
    mobjNetFrameWork = New eNetFrameWork.Layout
    mobjNetFrameWork.sSessionID = Session.SessionID
    mobjNetFrameWork.nUsercode = Session("nUsercode")
    Call mobjNetFrameWork.BeginPage("si019")
    mobjValues = New eFunctions.Values
    '^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.33.47
    mobjValues.sSessionID = Session.SessionID
    mobjValues.nUsercode = Session("nUsercode")
    '~End Body Block VisualTimer Utility

    mobjValues.sCodisplPage = "si019"
    mobjMenu = New eFunctions.Menues
    '^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.33.47
    mobjMenu.sSessionID = Session.SessionID
    mobjMenu.nUsercode = Session("nUsercode")
    '~End Body Block VisualTimer Utility

    '- Se establece el estado del tipo de acción.
    mobjValues.ActionQuery = Session("bQuery")
%>
<script type="text/javascript" src="/VTimeNet/Scripts/Constantes.js"></script>
<script type="text/javascript" src="/VTimeNet/Scripts/GenFunctions.js"></script>
<script type="text/javascript">
    // UpdateDescriptionVeh: Actualiza los datos de la marca y modelo del vehículo
    //-------------------------------------------------------------------------------------------
    function UpdateDescriptionVeh(lobjOption) {
        var strParams
        if (lobjOption.value != '') {
            strParams = "sVehCode=" + self.document.all.valVehCode.value;
            insDefValues("Auto", strParams, '/VTimeNet/Claim/CaseSeq');
        }
        else {
            // Actualiza la marca del vehículo : Blanquea la marca.
            self.document.getElementById("lblMarkVeh").innerHTML = ''
            // Actualiza el modelo del vehículo : Blanquea el modelo.       
            self.document.getElementById("lblModelVeh").innerHTML = ''
        }
    }

    //% UpdateFields: Habilita o no los campos siniestro y póliza.
    //-------------------------------------------------------------------------------------------
    function UpdateFields(lobjName) {
        //-------------------------------------------------------------------------------------------
        if (lobjName.value == 0) {
            with (self.document.forms[0]) {
                tctThir_Claim.disabled = true;
                tctThir_Polic.disabled = true;
                tctThir_Claim.value = "";
                tctThir_Polic.value = "";
            }
        }
        else {
            with (self.document.forms[0]) {
                tctThir_Claim.disabled = false;
                tctThir_Polic.disabled = false;
            }
        }
    }

//% ShowChangeValues: Se obtienen los datos de un vehículo ya registrado según su matrícula.
//-------------------------------------------------------------------------------------------
function ShowChangeValues(sField){
//-------------------------------------------------------------------------------------------
	switch(sField){
		case "Auto_db":
			with(self.document.forms[0]){
				ShowPopUp("/VTimeNet/Claim/CaseSeq/ShowDefValues.aspx?Field=" + sField + "&nType=1" + "&sRegister=" + tctRegister.value + "&sLicense=" + (optLicence[0].checked?optLicence[0].value:optLicence[1].value), "ShowDefValuesClaimCase", 1, 1,"no","no",2000,2000);
			}
	}
	}


    //% ShowDigit: Muestra el digito verificador de la placa
    //-------------------------------------------------------------------------------------------
    function ShowDigit() {
        //-------------------------------------------------------------------------------------------
        var slicence_ty
        with (self.document.forms[0]) {
            if (optLicence[0].checked == true)
                slicence_ty = "1"
            else
                slicence_ty = "2"
            insDefValues("Digit", "sLicense_ty=" + slicence_ty + "&sRegist=" + tctRegister.value + "&sDigit=" + tctDigit.value)
        }
    }
    //% ClickRecov_Ind: Cambia el valor del campo sRecov_ind (Probabilidad de recupero).
    //-------------------------------------------------------------------------------------------
    function ClickRecov_Ind() {
        //-------------------------------------------------------------------------------------------
        with (self.document.forms[0]) {
            if (chkRecov_ind.checked == true) {
                chkRecov_ind.value = "1"
                tcnRecov_per.disabled = false;
            }
            else {
                chkRecov_ind.value = "2";
                tcnRecov_per.value = "";
                tcnRecov_per.disabled = true;
            }
        }
    }
    //% InsChangeValues: Se actualizan los parametros de las listas de valores 
    //------------------------------------------------------------------------------------------- 
    function InsChangeValues(Field) {
        //------------------------------------------------------------------------------------------- 
        self.document.forms[0].ValVehModel.Parameters.Param2.sValue = Field.value
    }


    //% ShowData: Se cargan los valores de acuerdo al número de placa, si ésta está previamente registrada en el sistema 
    //--------------------------------------------------------------------------------------------------------------------
    function ShowData(sField, sField_1) {
        //--------------------------------------------------------------------------------------------------------------------
        var lobjdocument_form = document.forms[0];
        switch (sField) {
            case "Auto_Regist":
                if (sField_1.oldValue != sField_1.value) {
                    sField_1.oldValue = sField_1.value;
                    insDefValues(sField, "sRegist=" + sField_1.value + "&Slicense_ty=" + lobjdocument_form.cbeLicense_ty.value)
                }
                break;
            case "Auto_WebService":
                //self.parent.top.frames['fraHeader'].UpdateDiv('lblWaitProcess', 'Procesando WEB SERVICES, por favor espere...'); 
                self.parent.top.frames['fraFolder'].resValues.marqueeMessage = 'Procesando WEB SERVICES, por favor espere...';

                if (sField_1.oldValue != sField_1.value) {
                    sField_1.oldValue = sField_1.value;
                    if (sField_1.value == lobjdocument_form.tctRegister.value)
                        insDefValues(sField, "sRegist=" + lobjdocument_form.tctRegister.value + "&Slicense_ty=" + lobjdocument_form.cbeLicense_ty.value)
                    else
                        if (sField_1.value == lobjdocument_form.tctChassis.value)
                            insDefValues(sField, "&sChassis=" + lobjdocument_form.tctChassis.value)
                }
                break;
            case "Slicense_ty":
                lobjdocument_form.cbeNlic_special.disabled = true;
                lobjdocument_form.cbeNlic_special.value = "";
                lobjdocument_form.tctDigit.value = "";
                if (sField_1.value == '1') {
                    lobjdocument_form.tctRegister.disabled = false;
                    insDefValues("Auto_Regist", "sRegist=" + lobjdocument_form.tctRegister.value + "&Slicense_ty=" + lobjdocument_form.cbeLicense_ty.value)
                }
                else {
                    lobjdocument_form.tctRegister.disabled = false;
                    lobjdocument_form.tctMotor.disabled = false;
                    lobjdocument_form.tctChassis.disabled = false;
                    if (sField_1.value == '3') {
                        insDefValues(sField, "sRegist=" + lobjdocument_form.tctRegister.value + "&Slicense_ty=" + sField_1.value)
                    }
                    else {
                        lobjdocument_form.cbeNlic_special.disabled = false;
                    }
                }
                break;
        }
    }

    // ShowYear: Muestra el año completo (4 digitos)
    //-------------------------------------------------------------------------------------------
    function ShowYear() {
        //-------------------------------------------------------------------------------------------
        var d = new Date();
        with (self.document.forms[0]) {
            tcnYear.value = getCompleteYear(tcnYear.value)
            if (cbeLicense_ty.value == '3')
                if (tcnYear.value < d.getFullYear()) {
                    //tcnYear.value = '';	    
                    //alert("El año debe ser mayor o igual al año en curso, si la placa es provisional" )
                }
        }
    }

    //% getCompleteYear: Esta rutina se encarga de devolver el año completo (4 digitos) cuando se introduce incompleto (2 dígitos).
    //----------------------------------------------------------------------------------------------------------------------------
    function getCompleteYear(lstrValue) {
        //------------------------------------------------------------------------------------------------------------------------------
        var ldtmYear = new Date()
        var lintPos
        var lstrYear
        var llngValue = 0
        do {
            lstrValue = lstrValue.replace(".", "")
        }
        while (lstrValue != lstrValue.replace(".", ""))
        if (lstrValue == '') llngValue = 0
        else llngValue = parseFloat(lstrValue)
        if (llngValue < 1000) {
            if (llngValue <= 50)
                llngValue += 2000
            else
                if (llngValue < 100)
                    llngValue += 1900
                else
                    llngValue += 2000
            }
            return "" + llngValue
        } 




</script>
<%  lobjClaim_thir = New eClaim.Claim_thir

    Call lobjClaim_thir.Find(CDbl(Session("nClaim")), CInt(Session("nCase_num")), CInt(Session("nDeman_type")))

%>
<html>
<head>
    <%  Response.Write(mobjMenu.setZone(2, Request.QueryString("sCodispl"), Request.QueryString("sWindowDescript"), Request.QueryString("nWindowTy")))
        'UPGRADE_NOTE: Object mobjMenu may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
        mobjMenu = Nothing
    %>
    <meta name="GENERATOR" content="Microsoft Visual Studio 6.0">
    <%=mobjValues.StyleSheet()%>
</head>
<body onunload="closeWindows();">
    <form method="POST" id="FORM" name="frmSI019" action="valCaseSeq.aspx?sMode=1">
    <%Response.Write(mobjValues.ShowWindowsName(Request.QueryString("sCodispl"), Request.QueryString("sWindowDescript")))%>
    <p align="Center">
        <label id="0">
            <a href="#Matricula">Placa</a></label><label id="0">
                |
            </label>
        <label id="0">
            <a href="#Tipo de vehículo">Tipo de vehiculo</a></label><label id="0">
                |
            </label>
        <label id="0">
            <a href="#Reparación del vehículo">Reparacion del vehiculo</a></label><label id="0">
                |
            </label>
        <label id="0">
            <a href="#Datos generales de terceros">Datos generales de terceros</a></label><label
                id="0">
                |
            </label>
        <label id="0">
            <a href="#Informacion de la compañía contraria">Información de la compañia contraria</a></label><label
                id="0">
                |
            </label>
        <label id="0">
            <a href="#Acuerdo establecido">Acuerdo establecido</a></label><label id="0">
                |
            </label>
        <label id="0">
            <a href="#Recupero">Recupero</a></label>
    </p>
    <table width="100%">
        <tr>
            <td width="50%" colspan="2" class="HighLighted">
                <label id="40232">
                    <a name="Matrícula">Placa</a></label>
            </td>
            <td colspan="3">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <hr>
            </td>
            <td width="100pcx">
                &nbsp;
            </td>
            <td>
                <label id="9589">
                    Motor</label>
            </td>
            <td>
                <%=mobjValues.TextControl("tctMotor", 40, lobjClaim_thir.sMotor, , "Número del motor del vehículo", , , , , , 3)%>
            </td>
        </tr>
         <%If lobjClaim_thir.sLicence_ty = "" Or lobjClaim_thir.sLicence_ty = "1" Then%>
            <td>
                 <%mobjValues.BlankPosition = False
                     If lobjClaim_thir.sLicence_ty = "" Or lobjClaim_thir.sLicence_ty = CStr(eRemoteDB.Constants.strNull) Then
                         Response.Write(mobjValues.PossiblesValues("cbeLicense_ty", "table80", eFunctions.Values.eValuesType.clngComboType, "1", , , , , , "ShowData(""Slicense_ty"",this);", , , GetLocalResourceObject("cbeLicense_tyToolTip")))
                     Else
                         Response.Write(mobjValues.PossiblesValues("cbeLicense_ty", "table80", eFunctions.Values.eValuesType.clngComboType, lobjClaim_thir.sLicence_ty, , , , , , "ShowData(""Slicense_ty"",this);", , , GetLocalResourceObject("cbeLicense_tyToolTip")))
                     End If
                %>
            </td>
            <td>
                &nbsp;
            </td>
            <%Else%>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
            <%End If%>
            <td>
                &nbsp;
            </td>    <td>
                <label id="9587">
                    Chasis</label>
            </td>
            <td>
               <%
                   If (msAutomovileWebService = "1") Then
                       Response.Write(mobjValues.TextControl("tctChassis", 40, lobjClaim_thir.sChassis, , GetLocalResourceObject("tctChassisToolTip"), , , , "ShowData(""Auto_WebService"",this);"))
                   Else
                       Response.Write(mobjValues.TextControl("tctChassis", 40, lobjClaim_thir.sChassis, , GetLocalResourceObject("tctChassisToolTip")))
                   End If
                 %>
            </td>
        </tr>
        <tr>
            <td>
                <label id="9590">
                    Placa</label>
            </td>
            <td>
                <%
                    lobjOptionsInstallation = New eGeneral.OptionsInstallation
                    Call lobjOptionsInstallation.FindOptPolicy()

                    'msAutomovileWebService = lobjOptionsInstallation.sAutomovileWebService

                    'If (msAutomovileWebService = "1") Then
                    '    Response.Write(mobjValues.TextControl("tctRegister", 10, lobjClaim_thir.sRegist, , GetLocalResourceObject("tctRegisterToolTip"), , , , "ShowData(""Auto_WebService"",this);")) : Response.Write("-" & mobjValues.TextControl("tctDigit", 1, lobjClaim_thir.sDigit, , "Dígito verificador de la placa", , , , , True))
                    'Else
                    '    Response.Write(mobjValues.TextControl("tctRegister", 10, lobjClaim_thir.sRegist, , GetLocalResourceObject("tctRegisterToolTip"), , , , "ShowData(""Auto_Regist"",this);")) : Response.Write("-" & mobjValues.TextControl("tctDigit", 1, lobjClaim_thir.sDigit, , "Dígito verificador de la placa", , , , , True))
                    'End If

                    'Colocado temporalmente a la espera de una respuesta por parte de Joel.
                    Response.Write(mobjValues.TextControl("tctRegister", 10, lobjClaim_thir.sRegist, , GetLocalResourceObject("tctRegisterToolTip"), , , , "ShowData(""Auto_Regist"",this);")) : Response.Write("-" & mobjValues.TextControl("tctDigit", 1, lobjClaim_thir.sDigit, , "Dígito verificador de la placa", , , , , True))

                %>
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                <label id="9588">
                    Color</label>
            </td>
            <td>
                <%=mobjValues.TextControl("tctColor", 15, lobjClaim_thir.sColor,  , "Color del vehículo siniestrado",  ,  ,  ,  , lobjClaim_thir.sColor <> "", 5)%>
            </td>
             <td>
                <label id="Label1">
                    Año</label>
            </td>
            <td>
            <%= mobjValues.NumericControl("tcnYear", 4, CStr(lobjClaim_thir.nYear), , GetLocalResourceObject("tcnYearToolTip"), , , , , , "ShowYear();ShowChangeValues(""Auto"")", False)%>
            </td>
        </tr>
    </table>
    <table width="100%">
        <tr>
            <td colspan="5" class="HighLighted">
                <label id="40233">
                    <a name="Tipo de vehículo">Tipo de vehículo</a></label>
            </td>
            <td>
                &nbsp;
            </td>
            <td colspan="2" class="HighLighted">
                <label id="40234">
                    <a name="Reparación del vehículo">Reparación del vehículo</a></label>
            </td>
        </tr>
        <tr>
            <td colspan="5">
                <hr>
            </td>
            <td>
                &nbsp;
            </td>
            <td colspan="2">
                <hr>
            </td>
        </tr>
        <tr>
            <td>
                <label id="9586">
                    Código</label>
            </td>
            <td colspan="2">
              <%
                With mobjValues.Parameters
                    .Add("sStatregt", "1", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                End With
            %>
             <%=mobjValues.PossiblesValues("valVehcode", "tabTab_au_veh", eFunctions.Values.eValuesType.clngWindowType, lobjClaim_thir.sVehcode, True,  ,  ,  ,  , "ShowChangeValues(""Auto"")",  , 6, GetLocalResourceObject("valVehcodeToolTip"), eFunctions.Values.eTypeCode.eString)%>
            </td>
            <td>
                <label id="9591">
                    Marca</label>
            </td>
            <td>

             <%=mobjValues.PossiblesValues("ValVehMark", "table7042", eFunctions.Values.eValuesType.clngComboType, CStr(lobjClaim_thir.nVehBrand),  ,  ,  ,  ,  , "InsChangeValues(this);",  ,  , GetLocalResourceObject("ValVehMarkToolTip"))%>
              
                  <%  
                     mobjValues.HiddenControl("lblMarkVeh", lobjClaim_thir.sDesMark)
                    %>
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                <label id="9593">
                    Taller asignado</label>
            </td>
            <td>
                <%
                    With mobjValues
                        .Parameters.Add("nClaim", Session("nClaim"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                        .Parameters.Add("nCase_num", Session("nCase_num"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                        .Parameters.Add("nDeman_type", Session("nDeman_type"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                        .Parameters.Add("nBene_type", eClaim.Claim_case.eClaimRole.clngClaimRWorkShop, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                        .Parameters.Add("nTypeProv", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                        .Parameters.Add("sBene_type", "", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 20, 0, , eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                        Response.Write(mobjValues.PossiblesValues("valProvider", "tabClaimBenef", eFunctions.Values.eValuesType.clngWindowType, CStr(lobjClaim_thir.nProvider), True, , , , , , False, , "Taller donde se encuentra en reparación el vehículo", , 7))
                    End With
                %>
            </td>
        </tr>
        <tr>
            <td colspan="3">
                &nbsp;
            </td>
            <td>
                <label id="9592">
                    Modelo</label>
            </td>
            <td>
           
            <%
             
                With mobjValues.Parameters
                    .Add("sStatregt", "1", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Add("nVehBrand", lobjClaim_thir.nVehBrand, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                End With
                Response.Write(mobjValues.PossiblesValues("ValVehModel", "tabTab_au_model", eFunctions.Values.eValuesType.clngWindowType, lobjClaim_thir.sVehcode, True, , , , , "ShowChangeValues(""Auto1"")", , , GetLocalResourceObject("ValVehModelToolTip")))
                %>
               
                <%
                    mobjValues.HiddenControl("lblModelVeh",  lobjClaim_thir.sVehModel)
               
                    %>
            </td>
        </tr>
    </table>
    <table width="100%">
        <tr>
            <td colspan="2" class="HighLighted">
                <label id="40245">
                    <a name="Datos generales de terceros">Datos generales de terceros</a></label>
            </td>
            <td>
            </td>
            <td colspan="2" class="HighLighted">
                <label id="40246">
                    <a name="Informacion de la compañía contraria">Información de la compañía contraria</a></label>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <hr>
            </td>
            <td>
            </td>
            <td colspan="2">
                <hr>
            </td>
        </tr>
        <tr>
            <td>
                <label id="9614">
                    Responsabilidad del tercero</label>
            </td>
            <td>
                <%
                    Response.Write(mobjValues.PossiblesValues("cbeBlame", "Table204", eFunctions.Values.eValuesType.clngComboType, CStr(lobjClaim_thir.nBlame), , , , , , , False, , "Indicador de responsabilidad del tercero", , 8))
                %>
            </td>
            <td>
            </td>
            <td>
                <label id="9616">
                    Nombre</label>
            </td>
            <td>
                <%
                    mobjValues.Parameters.Add("sType", vbNullString, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    Response.Write(mobjValues.PossiblesValues("cbeThir_Comp", "tabCompany_sType", 1, CStr(lobjClaim_thir.nThir_comp), True, , , , , "UpdateFields(this)", False, , "Compañia en la que se encuentra asegurado la persona, vehículo o material involucrado", , 9))%>
            </td>
        </tr>
        <tr>
            <td colspan="3">
                &nbsp;
            </td>
            <td>
                <label id="9615">
                    Siniestro</label>
            </td>
            <td>
                <%=mobjValues.TextControl("tctThir_Claim", 12, lobjClaim_thir.sThir_claim,  , "Número del siniestro de la compañia contraria",  ,  ,  ,  , lobjClaim_thir.nThir_comp = CDbl("0"), 10)%>
            </td>
        </tr>
        <tr>
            <td colspan="3">
                &nbsp;
            </td>
            <td>
                <label id="9617">
                    Póliza</label>
            </td>
            <td>
                <%=mobjValues.TextControl("tctThir_Polic", 12, lobjClaim_thir.sThir_polic,  , "Número de la póliza del siniestro de la compañia contraria",  ,  ,  ,  , lobjClaim_thir.nThir_comp = CDbl("0"), 11)%>
            </td>
        </tr>
        <tr>
            <td colspan="5">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="5" class="HighLighted">
                <label id="40247">
                    <a name="Acuerdo establecido">Acuerdo establecido</a></label>
            </td>
        </tr>
        <tr>
            <td colspan="5">
                <hr>
            </td>
        </tr>
        <tr>
            <td colspan="5" align="CENTER">
                <%
                    With mobjValues
                        Response.Write(.TextAreaControl("tctNote", 5, 60, lobjClaim_thir.sDescriptNote, , "Acuerdo establecido por las compañias para el siniestro en tratamiento", , True))
                        Response.Write(.ButtonNotes("SCA2-K", lobjClaim_thir.nNoteAgree, False, mobjValues.ActionQuery, , , , 12))
                    End With
                %>
            </td>
        </tr>
    </table>
    <table width="50%">
        <tr>
            <td colspan="2" class="HighLighted">
                <label id="0">
                    <a name="Recupero">Recupero</a></label>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <hr>
            </td>
        </tr>
        <td>
            <label id="0">
                Probabilidad de recupero</label>
        </td>
        <td>
            <%=mobjValues.CheckControl("chkRecov_ind", "", lobjClaim_thir.sRecov_Ind, CStr(1), "ClickRecov_Ind()",  , 13)%>
        </td>
        </TR>
        <tr>
            <td>
                <label id="0">
                    Porcentaje probable de recupero</label>
            </td>
            <td>
                <%=mobjValues.NumericControl("tcnRecov_per", 5, CStr(lobjClaim_thir.nRecov_Per), False, "Monto expresado en porcentaje de la probabilidad de recupero del siniestro", False, 2,  ,  ,  ,  , lobjClaim_thir.sRecov_Ind <> "1", 14)%>
            </td>
        </tr>
    </table>
    <%Response.Write(mobjValues.BeginPageButton)%>
    </form>
</body>
</html>
<%  'UPGRADE_NOTE: Object mobjValues may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
    mobjValues = Nothing
    'UPGRADE_NOTE: Object lobjClaim_thir may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
    lobjClaim_thir = Nothing%>
<%  '^Begin Footer Block VisualTimer Utility 1.1 7/4/03 11.33.47
    Call mobjNetFrameWork.FinishPage("si019")
    'UPGRADE_NOTE: Object mobjNetFrameWork may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
    mobjNetFrameWork = Nothing
    '^End Footer Block VisualTimer%>
