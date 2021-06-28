<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="ePolicy" %>
<script language="VB" runat="Server">
    '^Begin Header Block VisualTimer Utility 1.1 31/3/03 19.42.02
    Dim mobjNetFrameWork As eNetFrameWork.Layout

    '- Objeto para el manejo de las funciones generales de carga de valores
    Dim mobjValues As eFunctions.Values


    '- Objeto para el manejo de las rutinas genéricas
    Dim mobjMenu As eFunctions.Menues
    '~End Body Block VisualTimer Utility

    '- Objeto para el manejo de la tabla Auto    
    Dim mclsAuto As ePolicy.Automobile

    '- Objeto para el manejo de la tabla Polizas     
    Dim mclsPolicy As ePolicy.Policy

    '- Objeto para el manejo de la tabla Certificados     
    Dim mclsCertificat As ePolicy.Certificat

    '- Objeto para el manejo de la tabla Grupos      
    Dim mclsGroups As ePolicy.Groups

    '- Objeto para el manejo de la tabla Situación       
    Dim mclsSituation As ePolicy.Situation

    '- Objeto para el manejo de la Errores        
    Dim mobjErrors As eFunctions.Errors
    '~End Body Block VisualTimer Utility

    Dim lobjOptionsInstallation As eGeneral.OptionsInstallation
    '- Variables de usu varios  
    Dim mblnGroups As Boolean
    Dim mblnSituation As Boolean
    Dim mblnPreCA004 As Boolean
    Dim lclsDepreciatedCapital As ePolicy.DepreciatedCapital
    Dim mblnLogico As Boolean
    Dim lstrAction As String
    Dim lnUse As Integer
    Dim msAutomovileWebService As String


    '% insPreAU001: hace la lectura de los campos a mostrar en pantalla
    '----------------------------------------------------------------------------------------------
    Function insPreAU001() As Boolean
        '----------------------------------------------------------------------------------------------
        insPreAU001 = True
        mblnPreCA004 = True
        mblnLogico = True
        mclsCertificat = New  ePolicy.Certificat
        lclsDepreciatedCapital = New ePolicy.DepreciatedCapital
        lobjOptionsInstallation = New eGeneral.OptionsInstallation
        Call lobjOptionsInstallation.FindOptPolicy

        lnUse = 0

        With mobjValues
            Call mclsPolicy.Find(Session("sCertype"), .StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), .StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), .StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble), True)
            Call mclsCertificat.Find(Session("sCertype"), .StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), .StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), .StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble),.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), True)
            If mclsCertificat.sClient = vbNullString Then
                mblnPreCA004 = False
            End If
            If mobjValues.StringToType(CStr(mclsPolicy.dNextReceip), eFunctions.Values.eTypeData.etdDate) <> eRemoteDB.Constants.dtmNull Then
                Session("dExpireDate") = mclsPolicy.dNextReceip
            Else
                Session("dExpireDate") = eRemoteDB.Constants.dtmNull
            End If

            Call mclsAuto.Find(Session("sCertype"), .StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), .StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), .StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble), .StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), .StringToDate(Session("dEffecdate")), True)
            Call lclsDepreciatedCapital.GetCapitalByRoutine(Session("sCertype"), .StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), .StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), .StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble), .StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), mclsAuto.nGroup, .StringToDate(Session("dEffecdate")), "AU_VEH_NOX2", True)
            lnUse = mclsAuto.nUse
            If mclsCertificat.sInd_Multiannual = "1" Then
                mclsAuto.nCapital = lclsDepreciatedCapital.nCapital
                mclsAuto.nVeh_valor = lclsDepreciatedCapital.nCapital
            End If
            If CStr(Session("nTransaction")) = "12" Or CStr(Session("nTransaction")) = "13" Or CStr(Session("nTransaction")) = "14" Or CStr(Session("nTransaction")) = "15" Then
                insPreAU001 = False
                mblnLogico = False
            End If

            mblnGroups = False
            If mclsPolicy.sPolitype = "2" Then
                mblnGroups = mclsGroups.valGroupExist(Session("sCertype"), _
                                                      .StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), _
                                                      .StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), _
                                                      .StringToType(Session("npolicy"), eFunctions.Values.eTypeData.etdDouble), _
                                                      .StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate))
            End If

            mblnSituation = mclsSituation.valExistsSituation(Session("sCertype"), .StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), .StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), .StringToType(Session("npolicy"), eFunctions.Values.eTypeData.etdDouble), .StringToType(CStr(eRemoteDB.Constants.intNull), eFunctions.Values.eTypeData.etdDouble))
        End With
    End Function

</script>
<%Response.Expires = -1441
    mobjNetFrameWork = New eNetFrameWork.Layout
    mobjNetFrameWork.sSessionID = Session.SessionID
    mobjNetFrameWork.nUsercode = Session("nUsercode")
    Call mobjNetFrameWork.BeginPage("AU001")
    '~End Header Block VisualTimer Utility
    Response.CacheControl = "private"
    mobjValues = New eFunctions.Values
    '^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.42.02
    mobjValues.sSessionID = Session.SessionID
    mobjValues.nUsercode = Session("nUsercode")
    '~End Body Block VisualTimer Utility

    mobjValues.sCodisplPage = Request.QueryString.Item("sCodispl")
    mobjMenu = New eFunctions.Menues

    '^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.42.02
    mobjMenu.sSessionID = Session.SessionID
    mobjMenu.nUsercode = Session("nUsercode")
    mclsAuto = New ePolicy.Automobile
    mclsPolicy = New ePolicy.Policy
    mclsGroups = New ePolicy.Groups
    mclsSituation = New ePolicy.Situation
    mobjErrors = New eFunctions.Errors

    '^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.42.02
    mobjErrors.sSessionID = Session.SessionID
    mobjErrors.nUsercode = Session("nUsercode")
    '-  Cuando se llama desde la secuencia de ordenes de servicio, se carga la pagina en modo consulta
    If CStr(Session("CallSequence")) = "Prof_ord" Then
        lstrAction = "/VTimeNet/Prof_ord/Prof_ordseq/valProf_ordseq.aspx?nMainAction=" & Request.QueryString.Item("nMainAction")
        mobjValues.ActionQuery = True
    Else
        lstrAction = "valPolicySeq.aspx?nMainAction=" & Request.QueryString.Item("nMainAction") & "&nHolder=1"
        mobjValues.ActionQuery = Session("bQuery")
    End If

    If Not insPreAU001() Then
        Response.Write(mobjErrors.ErrorMessage("AU001", 13989, , , , True))
    End If
%>
<script>

//% ShowChangeValues: Se cargan los valores de acuerdo al auto que se seleccione 
//-------------------------------------------------------------------------------------------
function ShowChangeValues(sField){
//-------------------------------------------------------------------------------------------
	var strParams; 
    if (typeof(self.document.forms[0].tcnYear)=='undefined') return 0;
    if (typeof(self.document.forms[0].tcnType)=='undefined') return 0;
    var lobjself_doc_form = self.document.forms[0];
	switch(sField){
		case "Auto":
    		strParams = "sRegist=" + lobjself_doc_form.tctRegister.value +  
    		            "&sDigit=" + lobjself_doc_form.tctDigit.value +  
			            "&sVehCode=" + lobjself_doc_form.valVehcode.value +
			            "&nYear=" + lobjself_doc_form.tcnYear.value +
			            "&sVehCode_ori=" + <%=mclsAuto.sVehcode%> +
			            "&nYear_ori=" + <%=mclsAuto.nYear%> +
			            "&sCapital_ori=" + <%="'" & mobjValues.TypeToString(mclsAuto.nVeh_valor, eFunctions.Values.eTypeData.etdDouble,True,6) & "'" %>;

			insDefValues(sField,strParams,'/VTimeNet/Policy/PolicySeq');
 			break;
		case "Auto1":
    		strParams = "Field1=Auto1" + 
			            "&sRegist=" + lobjself_doc_form.tctRegister.value + 
    		            "&sDigit=" + lobjself_doc_form.tctDigit.value +  			            
			            "&sVehCode=" + lobjself_doc_form.ValVehModel.value + 
			            "&nYear=" + lobjself_doc_form.tcnYear.value 
			insDefValues("Auto",strParams,'/VTimeNet/Policy/PolicySeq');
			break;
	}
	if (sField=="Auto")
	{
		with (lobjself_doc_form) {	
			if(valVehcode.value=='') {
				UpdateDiv("lblType",'','Normal')
				tcnType.value=''
				tcnCapital.value=''
				tcnVehPlace.value=''
				tcnVehPma.value=''
				ValVehMark.value=''
				ValVehModel.value = ''
				tcnYear.value = ''
				cbeGroup.value = ''
				ValVehMark.disabled=false
				ValVehModel.disabled=false
				btnValVehModel.disabled=false
				UpdateDiv("ValVehModelDesc",'','Normal')
			}
		}
	}
}   


//% ShowChangeValues: Se cargan los valores de acuerdo al auto que se seleccione 
//-------------------------------------------------------------------------------------------
function DelCoverOnchange(field){
//-------------------------------------------------------------------------------------------
	var strParams; 
  
        strParams = "Field1=Auto1" + 
			            "&nGroup=" + field.value ;
                  insDefValues("DelCoverOnchange",strParams,'/VTimeNet/Policy/PolicySeq');
 			
	
	
}   

//% getCompleteYear: Esta rutina se encarga de devolver el año completo (4 digitos) cuando se introduce incompleto (2 dígitos).
//----------------------------------------------------------------------------------------------------------------------------
function getCompleteYear(lstrValue){
//------------------------------------------------------------------------------------------------------------------------------
    var ldtmYear = new Date()
    var lintPos  
    var lstrYear
    var llngValue = 0
    do {
       lstrValue = lstrValue.replace(".","")
    }
    while (lstrValue != lstrValue.replace(".","")) 
    if (lstrValue == '') llngValue = 0 
    else llngValue = parseFloat(lstrValue) 
	if (llngValue<1000){ 
	    if (llngValue<=50) 
	        llngValue += 2000 
	    else 
	        if (llngValue<100) 
	            llngValue += 1900 
	        else 
	            llngValue += 2000 
	} 
	return "" + llngValue 
} 

//% ShowData: Se cargan los valores de acuerdo al número de placa, si ésta está previamente registrada en el sistema 
//--------------------------------------------------------------------------------------------------------------------
function ShowData(sField,sField_1){
//--------------------------------------------------------------------------------------------------------------------
    var lobjdocument_form = document.forms[0];
	switch(sField){
		case "Auto_Regist": 
		    if(sField_1.oldValue != sField_1.value){
		        sField_1.oldValue = sField_1.value;
			    insDefValues(sField,"sRegist=" + sField_1.value + "&Slicense_ty=" + lobjdocument_form.cbeLicense_ty.value)
			}
 			break;
		case "Slicense_ty": 
			lobjdocument_form.cbeNlic_special.disabled=true;
			lobjdocument_form.cbeNlic_special.value = "";
			lobjdocument_form.tctDigit.value = "";
			if (sField_1.value=='1'){
				lobjdocument_form.tctRegister.disabled=false;
				insDefValues("Auto_Regist","sRegist=" + lobjdocument_form.tctRegister.value + "&Slicense_ty=" + lobjdocument_form.cbeLicense_ty.value)
			}
			else{
				lobjdocument_form.tctRegister.disabled=false;
				lobjdocument_form.tctMotor.disabled=false;
				lobjdocument_form.tctChassis.disabled=false;
				if (sField_1.value=='3'){
					insDefValues(sField,"sRegist=" + lobjdocument_form.tctRegister.value + "&Slicense_ty=" + sField_1.value)
				}
				else{
					lobjdocument_form.cbeNlic_special.disabled=false;
				}
			}
			break;
    }
}

// ShowYear: Muestra el año completo (4 digitos)
//-------------------------------------------------------------------------------------------
function ShowYear(){
//-------------------------------------------------------------------------------------------
var d = new Date();
	with (self.document.forms[0]) {
	    tcnYear.value = getCompleteYear(tcnYear.value)
	    if (cbeLicense_ty.value == '3')
			if (tcnYear.value < d.getFullYear()) {
				tcnYear.value = '';	    
				alert("El año debe ser mayor o igual al año en curso, si la patente es provisional" )
			}
	}
}


//% DisabledField: Se inhabilitan los campos de la forma si la transacción es modificar
//------------------------------------------------------------------------------------------- 
function DisabledFields(){ 
//------------------------------------------------------------------------------------------- 
	var lobjself_doc_form = self.document.forms[0];
	lobjself_doc_form.cbovalSituation.disabled = true 
	self.document.btncbovalSituation.disabled = true 
    lobjself_doc_form.cbovalGroup.disabled = true 
    self.document.btncbovalGroup.disabled = true 
    lobjself_doc_form.cbeLicense_ty.disabled=true 
    lobjself_doc_form.tctRegister.disabled = true 
	lobjself_doc_form.tctMotor.disabled = true 
	lobjself_doc_form.tctChassis.disabled = true 
	lobjself_doc_form.tctColor.disabled = true 
	lobjself_doc_form.valVehcode.disabled = true 
	lobjself_doc_form.btnvalVehcode.disabled = true 
	lobjself_doc_form.tcnYear.disabled = true 
	lobjself_doc_form.ValVehModel.disabled = true 
} 

//% InsChangeValues: Se actualizan los parametros de las listas de valores 
//------------------------------------------------------------------------------------------- 
function InsChangeValues(Field){ 
//------------------------------------------------------------------------------------------- 
	self.document.forms[0].ValVehModel.Parameters.Param2.sValue = Field.value 
} 

//% InsClickField: Se cambia el valor del objeto checkbox al hacer click 
//------------------------------------------------------------------------------------------- 
function InsClickField(objField){	
//------------------------------------------------------------------------------------------- 
	if (objField.checked == true) 
		objField.value = "1" 
	else 
		objField.value = "2" 
} 

//% AllDisabled: Se inhabilitan todos los campos de la página  
//------------------------------------------------------------------------------------------- 
function AllDisabled(){	
//------------------------------------------------------------------------------------------- 
	var lobjself_doc_form = self.document.forms[0];
	var lintIndex = 0;
	var lintlength
	try{ 
		with (lobjself_doc_form){
			lintlength = length;
			for (lintIndex=0;lintIndex<lintlength;lintIndex++){
			    elements[lintIndex].disabled = true;
				if(self.document.images.length>0){
				    if(typeof(self.document.images["btn_" + elements[lintIndex].name])!='undefined')
				       self.document.images["btn_" + elements[lintIndex].name].disabled = elements[lintIndex].disabled 
				    if(typeof(self.document.images["btn" + elements[lintIndex].name])!='undefined')
				       self.document.images["btn" + elements[lintIndex].name].disabled = elements[lintIndex].disabled 
				}
			}
		}
	} catch(error){}
}

 
//% insChangeAgenDealer: Se fija el parametro concesionario al valores posible de Agencias por concesionario
//------------------------------------------------------------------------------------------- 
function insChangeAgenDealer(){	
//------------------------------------------------------------------------------------------- 
    with (self.document.forms[0]){
        valAgenDealer.Parameters.Param1.sValue=self.document.forms[0].tctClient_Dealer.value
        valAgenDealer.value=''
        UpdateDiv("valAgenDealerDesc",'','Normal')
    }
}


</SCRIPT>
<HTML>
<HEAD>
    <meta name="GENERATOR" content="Microsoft Visual Studio 6.0">
    <script type="text/javascript" src="/VTimeNet/Scripts/tmenu.js"></script>
    <script type="text/javascript" src="/VTimeNet/Scripts/GenFunctions.js"></script>
    <script type="text/javascript" src="/VTimeNet/Scripts/Constantes.js"></script>
    <%With Response
            .Write(mobjValues.StyleSheet())
            .Write(mobjMenu.setZone(2, "AU001", Request.QueryString.Item("sWindowDescript"), CShort(Request.QueryString.Item("nWindowTy"))))
        End With
        mobjMenu = Nothing
    %>
</head>
<BODY onunload="closeWindows();">
    <FORM method="POST" id="FORM" name="FRMAU001" action="<%=lstrAction%>">
    <p align="CENTER">
        <label id="40644">
            <a href="#Datos del colectivo">
                <%= GetLocalResourceObject("AnchorDatos del colectivoCaption") %></a></label><label
                    id="0">
                    |
                </label>
        <label id="40645">
            <a href="#Matrícula">
                <%= GetLocalResourceObject("AnchorMatrículaCaption") %></a></label><label id="0">
                    |
                </label>
        <label id="40646">
            <a href="#Datos del vehículo">
                <%= GetLocalResourceObject("AnchorDatos del vehículoCaption") %></a></label><label
                    id="0">
                    |
                </label>
    </p>
    <%=mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"))%>
    <td>
        &nbsp;
    </td>
    <TABLE width="100%">
        <TR>
            <td colspan="2" class="HighLighted">
                <label id="40647">
                    <a name="Datos del colectivo">
                        <%= GetLocalResourceObject("AnchorDatos del colectivo2Caption") %></a></label>
            </td>
            <td width="10%">
                &nbsp;
            </td>
            <td colspan="2" class="HighLighted">
                <label id="40648">
                    <a name="Matrícula">
                        <%= GetLocalResourceObject("AnchorMatrícula2Caption") %></a></label>
            </td>
        </TR>
        <TR>
            <td colspan="2" class="Horline">
            </td>
            <td>
            </td>
            <td colspan="2" class="Horline">
            </td>
        </TR>
        <TR>
            <td width="18%">
                <label id="12879">
                    <%= GetLocalResourceObject("cbovalGroupCaption") %></label>
            </td>
            <%
                With mobjValues.Parameters
                    .Add("sCertype", Session("sCertype"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Add("nProduct", Session("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Add("nPolicy", Session("nPolicy"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                End With
            %>
            <td>
                <%=mobjValues.PossiblesValues("cbovalGroup", "tabGroups", eFunctions.Values.eValuesType.clngWindowType, CStr(mclsAuto.nGroup), True, , , , , "DelCoverOnchange(this)", , , GetLocalResourceObject("cbovalGroupToolTip"))%>
            </td>
            <td>
            </td>
            <td>
                <label id="13531">
                    <%= GetLocalResourceObject("cbeLicense_tyCaption") %></label>
            </td>
            <td>
                <%mobjValues.BlankPosition = False
                    If mclsAuto.sLicense_ty = "" Or mclsAuto.sLicense_ty = CStr(eRemoteDB.Constants.strNull) Then
                        Response.Write(mobjValues.PossiblesValues("cbeLicense_ty", "table80", eFunctions.Values.eValuesType.clngComboType, "1", , , , , , "ShowData(""Slicense_ty"",this);", , , GetLocalResourceObject("cbeLicense_tyToolTip")))
                    Else
                        Response.Write(mobjValues.PossiblesValues("cbeLicense_ty", "table80", eFunctions.Values.eValuesType.clngComboType, mclsAuto.sLicense_ty, , , , , , "ShowData(""Slicense_ty"",this);", , , GetLocalResourceObject("cbeLicense_tyToolTip")))
                    End If
                %>
            </td>
        </TR>
            <TR>
                <td>
                    <label id="13531">
                        <%= GetLocalResourceObject("cbovalSituationCaption") %></label>
                </td>
                <%
                    With mobjValues.Parameters
                        .Add("sCertype", Session("sCertype"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                        .Add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                        .Add("nProduct", Session("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                        .Add("nPolicy", Session("nPolicy"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    End With
                %>
                <td>
                    <%=mobjValues.PossiblesValues("cbovalSituation", "tabSituation", 2, CStr(mclsAuto.nSituation), True,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbovalSituationToolTip"))%>
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    <label id="13531">
                        <%= GetLocalResourceObject("cbeNlic_specialCaption") %></label>
                </td>
                <td>
                    <%=mobjValues.PossiblesValues("cbeNlic_special", "table5594", eFunctions.Values.eValuesType.clngComboType, CStr(mclsAuto.nlic_special),  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeNlic_specialToolTip"))%>
                </td>
            </TR>
        <TR>
            <td>
            </td>
            <td>
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                <label id="12883">
                    <%= GetLocalResourceObject("tctRegisterCaption") %></label>
            </td>
             <td>
            <% 
                  Response.Write(mobjValues.TextControl("tctRegister", 10, mclsAuto.sRegist,  , GetLocalResourceObject("tctRegisterToolTip"),  ,  ,  , "ShowData(""Auto_Regist"",this);")) : Response.Write( "-" & mobjValues.TextControl("tctDigit", 1, mclsAuto.sDigit,  , "Dígito verificador de la placa",  ,  ,  ,  , True))  
                
                %>
            </td>
        </TR>
        <TR>
            <td width="8%">
                &nbsp;
            </td>
            <td colspan="5" class="HighLighted">
                <label id="40649">
                    <a name="Datos del vehículo">
                        <%= GetLocalResourceObject("AnchorDatos del vehículo2Caption") %></a></label>
            </td>
        </TR>
        <TR>
            <td colspan="5" class="Horline">
            </td>
        </TR>
        <TR>
            <TD>
                <label id="12881">
                    <%= GetLocalResourceObject("tctMotorCaption") %></label>
            </TD>
            <TD>
                <%=mobjValues.TextControl("tctMotor", 40, mclsAuto.sMotor,  , GetLocalResourceObject("tctMotorToolTip"))%>
            </TD>
            <td>
                &nbsp;
            </td>
            <td>
                <label id="12876">
                    <%= GetLocalResourceObject("tctChassisCaption") %></label>
            </td>
            <td>
                <%
                        Response.Write(mobjValues.TextControl("tctChassis", 40, mclsAuto.sChassis,  , GetLocalResourceObject("tctChassisToolTip")))
                 %>
            </td>
            <td>
            </td>
        </TR>
        <TR>
            <td>
                <label id="12877">
                    <%= GetLocalResourceObject("tctColorCaption") %></label>
            </td>
            <td>
                <%=mobjValues.TextControl("tctColor", 15, mclsAuto.sColor,  , GetLocalResourceObject("tctColorToolTip"))%>
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                <label id="12874">
                    <%= GetLocalResourceObject("valVehcodeCaption") %></label>
            </td>
            <%
                With mobjValues.Parameters
                    .Add("sStatregt", "1", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                End With
            %>
            <td>
                <%=mobjValues.PossiblesValues("valVehcode", "tabTab_au_veh", eFunctions.Values.eValuesType.clngWindowType, mclsAuto.sVehcode, True,  ,  ,  ,  , "ShowChangeValues(""Auto"")",  , 6, GetLocalResourceObject("valVehcodeToolTip"), eFunctions.Values.eTypeCode.eString)%>
            </td>
        </TR>
        <TR>
            <td>
                <label id="12886">
                    <%= GetLocalResourceObject("ValVehMarkCaption") %></label>
            </td>
            <td>
                <%=mobjValues.PossiblesValues("ValVehMark", "table7042", eFunctions.Values.eValuesType.clngComboType, CStr(mclsAuto.nVehBrand),  ,  ,  ,  ,  , "InsChangeValues(this);",  ,  , GetLocalResourceObject("ValVehMarkToolTip"))%>
            </td>
            <td>
            </td>
            <td>
                <label id="12887">
                    <%= GetLocalResourceObject("ValVehModelCaption") %></label>
            </td>
            <td>
                <%
                    With mobjValues.Parameters
                        .Add("sStatregt", "1", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                        .Add("nVehBrand", mclsAuto.nVehBrand, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    End With
                    Response.Write(mobjValues.PossiblesValues("ValVehModel", "tabTab_au_model", eFunctions.Values.eValuesType.clngWindowType, mclsAuto.sVehcode, True, , , , , "ShowChangeValues(""Auto1"")", , , GetLocalResourceObject("ValVehModelToolTip")))
                %>
            </td>
        </TR>
        <TR>
            <td>
                <label id="12885">
                    <%= GetLocalResourceObject("AnchorCaption") %></label>
            </td>
            <%=mobjValues.DIVControl("lblType", True, mclsAuto.sVehType)%>
            <TD>&nbsp;</TD>
            <td>
                <label id="12893">
                    <%= GetLocalResourceObject("tcnYearCaption") %></label>
            </td>
            <td>
                <%=mobjValues.NumericControl("tcnYear", 4, CStr(mclsAuto.nYear),  , GetLocalResourceObject("tcnYearToolTip"),  ,  ,  ,  ,  , "ShowYear();ShowChangeValues(""Auto"")", False)%>
            </td>
        </TR>
        <TR>
            <TD><LABEL ID=16248><%= GetLocalResourceObject("cbeGroupCaption") %></LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("cbeGroup", "table6028", eFunctions.Values.eValuesType.clngComboType, CStr(lnUse), , , , , , , , , GetLocalResourceObject("cbeGroupToolTip"))%></TD>           			
            <td colspan="2"></td>
        </TR>
        <TR>
            <td>
                <label id="12880">
                    <%= GetLocalResourceObject("Anchor2Caption") %></label>
            </td>
            <td>
                <%If mclsAuto.sRelapsing = "1" Then
                        Response.Write(mobjValues.CheckControl("chksrelapsing", "", "1", "1", "InsClickField(this)", True, , GetLocalResourceObject("chksrelapsingToolTip")))
                    Else
                        Response.Write(mobjValues.CheckControl("chksrelapsing", "", "2", "1", "InsClickField(this)", True, , GetLocalResourceObject("chksrelapsingToolTip")))
                    End If
                %>
            <td>
                &nbsp;
            </td>
            <td>
                <label id="12880">
                    <%= GetLocalResourceObject("Anchor3Caption") %></label>
            </td>
            <td>
                <%If mclsAuto.sN_infrac = "1" Then
                        Response.Write(mobjValues.CheckControl("chksn_infrac", "", "1", "1", "InsClickField(this)", , , GetLocalResourceObject("chksn_infracToolTip")))
                    Else
                        Response.Write(mobjValues.CheckControl("chksn_infrac", "", "2", "2", "InsClickField(this)", , , GetLocalResourceObject("chksn_infracToolTip")))
                    End If
                %>
            </td>
        </TR>
        <tr>
            <td>
                <label id="12875">
                    <%= GetLocalResourceObject("Anchor4Caption") %></label>
            </td>
            <td>
                <%If mclsAuto.sReturn = "1" Then
                        Response.Write(mobjValues.CheckControl("chksreturn", "", "1", "1", "InsClickField(this)", , , GetLocalResourceObject("chksreturnToolTip")))
                    Else
                        Response.Write(mobjValues.CheckControl("chksreturn", "", "2", "2", "InsClickField(this)", , , GetLocalResourceObject("chksreturnToolTip")))
                    End If
                %>
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                <label id="12875">
                    <%= GetLocalResourceObject("tcnCapitalCaption") %></label>
            </td>
            <td>
                <%= mobjValues.NumericControl("tcnCapital", 18, mobjValues.TypeToString(mclsAuto.nVeh_valor, eFunctions.Values.eTypeData.etdDouble, True, 6), , GetLocalResourceObject("tcnCapitalToolTip"), True, 6)%>
            </td>
        </tr>
        <tr>
            <td>
                <label id="12891">
                    <%= GetLocalResourceObject("tcnVehPlaceCaption") %></label>
            </td>
            <td>
                <%= mobjValues.NumericControl("tcnVehPlace", 3, mobjValues.TypeToString(mclsAuto.nVehplace, eFunctions.Values.eTypeData.etdDouble), , GetLocalResourceObject("tcnVehPlaceToolTip"))%>
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                <label id="12892">
                    <%= GetLocalResourceObject("tcnVehPmaCaption") %></label>
            </td>
            <td>
                <%=mobjValues.NumericControl("tcnVehPma", 5, mobjValues.TypeToString(mclsAuto.nVehpma, eFunctions.Values.eTypeData.etdDouble),  , GetLocalResourceObject("tcnVehPmaToolTip"))%>
            </td>
        </tr>
        <tr>
            <td>
                <label id="12882">
                    <%= GetLocalResourceObject("cbeProviCodCaption") %></label>
            </td>
            <td>
                <%=mobjValues.PossiblesValues("cbeProviCod", "Table224", 1, CStr(mclsAuto.nAutoZone),  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeProviCodToolTip"))%>
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                <label id="LABEL1">
                    <%= GetLocalResourceObject("tcnCollectedPremiumCaption")%></label>
            </td>
            <td>
                <%=mobjValues.NumericControl("tcnCollectedPremium", 18, mobjValues.TypeToString(mclsAuto.nCollectedPrem, eFunctions.Values.eTypeData.etdDouble), , GetLocalResourceObject("tcnCollectedPremiumToolTip"), True, 6)%>
            </td>
        </tr>
        <tr>
            <td>
                <label id="Label2">
                        <%= GetLocalResourceObject("tctEngineCaption") %></label>
                </td>
            <td>
                <%=mobjValues.TextControl("tctEngine", 20, mclsAuto.sEngine,  , GetLocalResourceObject("tctEngineToolTip"))%>
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                <label id="Label3">
                    <%= GetLocalResourceObject("Anchor5Caption") %></label>
            </td>
            <td>
                <%If mclsAuto.sHybridVehicle = "1" Then
                        Response.Write(mobjValues.CheckControl("chks_HybridVehicle", "", "1", "1", "InsClickField(this)", , , GetLocalResourceObject("chks_HybridVehicleToolTip")))
                    Else
                        Response.Write(mobjValues.CheckControl("chks_HybridVehicle", "", "2", "2", "InsClickField(this)", , , GetLocalResourceObject("chks_HybridVehicleToolTip")))
                    End If
                %>
            </td>
        </tr>
         <tr>
            <td width="20%">
                <label>
                    <%= GetLocalResourceObject("sClient_DealerCaption") %></label>
            </td>
            <td>
                <%= mobjValues.ClientControl("tctClient_Dealer", mclsAuto.sClient_Dealer, , GetLocalResourceObject("sClient_DealerCaption"), "insChangeAgenDealer()", , , , , , , , , , False)%>
            </td>
            <td>
                &nbsp;
            </td>
            <td width="20%">
                <label>
                    <%= GetLocalResourceObject("sClient_SellerCaption") %></label>
            </td>
            <td>
                <%= mobjValues.ClientControl("tctClient_Seller", mclsAuto.sClient_Seller, , GetLocalResourceObject("sClient_SellerCaption"),  ,  ,  ,  ,  ,  ,  ,  ,  ,  , False)%>
            </td>
        </tr>

         <tr>
            <td width="20%">
                <label>
                    <%= GetLocalResourceObject("valAgenDealerCaption")%></label>
            </td>
            <td>
                <%
                    With mobjValues.Parameters
                        .Add("sClient_Dealer", mclsAuto.sClient_Dealer, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    End With
                %>
                <%= mobjValues.PossiblesValues("valAgenDealer", "tabAgencies_Dealer", Values.eValuesType.clngWindowType, mclsAuto.nAgenDealer, True, , , , , , , , GetLocalResourceObject("valAgenDealerTooltip"))%>
            </td>
        </tr>


    </table>
    <%
        If Not mblnPreCA004 Then
            Response.Write("<script>AllDisabled();</script>")
            Response.Write(mobjErrors.ErrorMessage("AU001", 3926, , , , True))
        End If

        If Not mblnLogico Then
            Response.Write("<script>DisabledFields();</script>")
        End If
        With Response
            .Write(mobjValues.BeginPageButton)
            .Write(mobjValues.HiddenControl("tcnType", CStr(1)))
        End With
        If Not mobjValues.ActionQuery Then
            With Response
                .Write("<script>")
                .Write("self.document.forms[0].tcnVehPlace.disabled=true;")
                .Write("self.document.forms[0].tcnVehPma.disabled=true;")
                .Write("</script>")
            End With
	
            If Not mblnGroups Then
                With Response
                    .Write("<script>")
                    .Write("self.document.forms[0].cbovalGroup.disabled=true;")
                    .Write("self.document.btncbovalGroup.disabled=true;")
                    .Write("</script>")
                End With
            End If
	
            If Not mblnSituation Then
                With Response
                    .Write("<script>")
                    .Write("self.document.forms[0].cbovalSituation.disabled=true;")
                    .Write("self.document.btncbovalSituation.disabled=true;")
                    .Write("</script>")
                End With
            End If
	
            If mclsAuto.sRegist <> "" Or mclsAuto.sRegist <> CStr(eRemoteDB.Constants.strNull) Then
                With Response
                    .Write("<script>")
                    .Write("ShowChangeValues(""Auto"");")
                    .Write("</script>")
                End With
            End If
        End If

        mobjValues = Nothing
        mclsAuto = Nothing
        mclsPolicy = Nothing
        mclsGroups = Nothing
        mclsSituation = Nothing
        mobjErrors = Nothing
    %>
    </form>
</body>
</html>
<script type='text/javascript'>
<!--
    if (typeof (self.document.forms[0].tctRegister) != 'undefined')
        self.document.forms[0].tctRegister.oldValue = '';
//-->
</script>
<%'^Begin Footer Block VisualTimer Utility 1.1 31/3/03 19.42.02
    Call mobjNetFrameWork.FinishPage("AU001")
    mobjNetFrameWork = Nothing
    '^End Footer Block VisualTimer%>
