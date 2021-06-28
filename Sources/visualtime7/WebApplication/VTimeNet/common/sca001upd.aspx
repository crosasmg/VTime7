<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eGeneralForm" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eGeneral" %>
<script language="VB" runat="Server">

'- Objeto para creacion de menu 
Dim mobjMenu As eFunctions.Menues
'- Objeto para uso de valores    
Dim mobjValues As eFunctions.Values
'- Objeto para uso de direcciones    
Dim mobjAddress As eGeneralForm.Address
'- Objeto para manejo de grid
Dim mobjGrid As Object
'- Objeto para usos generales
Dim mobjGeneral As eGeneral.OptionsInstallation

'- Variables para almacenar parametros de pagina y propiedades de objetos
Dim mblnActionQuery As Object
Dim mstrKeyAddress As String
Dim mintRecowner As Object
Dim mstrRecType As String
Dim mstrQuote As String
Dim mstrAmp As String
Dim mstrOnSeq As Object
Dim mstrClient As String
Dim mstrcodagree As String


'- Variable para activar o desactivar boton Agregar en la grilla de teléfonos
Dim mblnExistAddress As Boolean
Dim mblnExistPhones As Object

'- Variables para almacenar la fecha de ingreso del cliente
Dim mdatClient As Object
Dim mdatClientPhone As Object


'- Variables para manejo temporal del cliente y la acción que se está ejecutando.
Dim mstrLastClient As Object
Dim mblnLastAction As Object
Dim mclsCertificat As Object
Dim dEffecDate As Date
Dim bAccept As Object


'%insPreSCA001: Despliega la ventana de la transaccion
'------------------------------------------------------------------------------
Private Sub insPreSCA001()
	'------------------------------------------------------------------------------
	
Response.Write("" & vbCrLf)
Response.Write("<script>" & vbCrLf)
Response.Write("	var nMainAction ='")


Response.Write(Request.QueryString.Item("nMainAction"))


Response.Write("'" & vbCrLf)
Response.Write("	 " & vbCrLf)
Response.Write("" & vbCrLf)
Response.Write("//%ShowAddress: Refresca la página cuando se cambia la opcion de tipo de dirección" & vbCrLf)
Response.Write("//-------------------------------------------------------------------------------------------" & vbCrLf)
Response.Write("function ShowAddress(Value) {" & vbCrLf)
Response.Write("//-------------------------------------------------------------------------------------------" & vbCrLf)
Response.Write("    var lstrLocation" & vbCrLf)
Response.Write("    lstrLocation = self.document.location" & vbCrLf)
Response.Write("" & vbCrLf)
Response.Write("    lstrLocation = ")


Response.Write(mstrQuote)




Response.Write(mstrQuote)


Response.Write(" + lstrLocation" & vbCrLf)
Response.Write("    lstrLocation = lstrLocation.replace(")


Response.Write(mstrQuote)




Response.Write(mstrAmp)


Response.Write("Reload=0")


Response.Write(mstrQuote)


Response.Write(",")


Response.Write(mstrQuote)




Response.Write(mstrQuote)


Response.Write(")" & vbCrLf)
Response.Write("    lstrLocation = lstrLocation.replace(")


Response.Write(mstrQuote)




Response.Write(mstrAmp)


Response.Write("sRecType=1")


Response.Write(mstrQuote)


Response.Write(",")


Response.Write(mstrQuote)




Response.Write(mstrQuote)


Response.Write(")" & vbCrLf)
Response.Write("    lstrLocation = lstrLocation.replace(")


Response.Write(mstrQuote)




Response.Write(mstrAmp)


Response.Write("sRecType=2")


Response.Write(mstrQuote)


Response.Write(",")


Response.Write(mstrQuote)




Response.Write(mstrQuote)


Response.Write(")" & vbCrLf)
Response.Write("    lstrLocation = lstrLocation.replace(")


Response.Write(mstrQuote)




Response.Write(mstrAmp)


Response.Write("sRecType=3")


Response.Write(mstrQuote)


Response.Write(",")


Response.Write(mstrQuote)




Response.Write(mstrQuote)


Response.Write(")" & vbCrLf)
Response.Write("    lstrLocation = lstrLocation.replace(")


Response.Write(mstrQuote)




Response.Write(mstrAmp)


Response.Write("sRecType=4")


Response.Write(mstrQuote)


Response.Write(",")


Response.Write(mstrQuote)




Response.Write(mstrQuote)


Response.Write(")" & vbCrLf)
Response.Write("    lstrLocation = lstrLocation.replace(")


Response.Write(mstrQuote)




Response.Write(mstrAmp)


Response.Write("nSendAddr=1")


Response.Write(mstrQuote)


Response.Write(",")


Response.Write(mstrQuote)




Response.Write(mstrQuote)


Response.Write(")" & vbCrLf)
Response.Write("    lstrLocation = lstrLocation.replace(")


Response.Write(mstrQuote)




Response.Write(mstrAmp)


Response.Write("nSendAddr=2")


Response.Write(mstrQuote)


Response.Write(",")


Response.Write(mstrQuote)




Response.Write(mstrQuote)


Response.Write(")" & vbCrLf)
Response.Write("    lstrLocation = lstrLocation.replace(")


Response.Write(mstrQuote)




Response.Write(mstrAmp)


Response.Write("nSendAddr=3")


Response.Write(mstrQuote)


Response.Write(",")


Response.Write(mstrQuote)




Response.Write(mstrQuote)


Response.Write(")" & vbCrLf)
Response.Write("    lstrLocation = lstrLocation.replace(")


Response.Write(mstrQuote)




Response.Write(mstrAmp)


Response.Write("nSendAddr=4")


Response.Write(mstrQuote)


Response.Write(",")


Response.Write(mstrQuote)




Response.Write(mstrQuote)


Response.Write(")" & vbCrLf)
Response.Write("    lstrLocation = lstrLocation.replace(/&RecTypeReload.*/, """");" & vbCrLf)
Response.Write("    lstrLocation = lstrLocation.replace(/&txtAddress.*/, """");" & vbCrLf)
Response.Write("    " & vbCrLf)
Response.Write("    self.document.location.href = lstrLocation + ")


Response.Write(mstrQuote)




Response.Write(mstrAmp)


Response.Write("RecTypeReload=1")


Response.Write(mstrAmp)


Response.Write("Reload=0")


Response.Write(mstrAmp)


Response.Write("sRecType=")


Response.Write(mstrQuote)


Response.Write(" + Value + ")


Response.Write(mstrQuote)




Response.Write(mstrAmp)


Response.Write("nSendAddr=")


Response.Write(mstrQuote)


Response.Write(" + Value" & vbCrLf)
Response.Write("}" & vbCrLf)
Response.Write("" & vbCrLf)
Response.Write("//%InsChangeMunicipality: Busca la ciudad y la región dada la comuna" & vbCrLf)
Response.Write("//-------------------------------------------------------------------------------------------	" & vbCrLf)
Response.Write("function InsChangeMunicipality(nMunicipality){" & vbCrLf)
Response.Write("//-------------------------------------------------------------------------------------------	" & vbCrLf)
Response.Write("    insDefValues('Municipality', 'nMunicipality=' + nMunicipality)" & vbCrLf)
Response.Write("    " & vbCrLf)
Response.Write("}" & vbCrLf)
Response.Write("//%UpdValues: Actualiza el contenido de los campos de la region" & vbCrLf)
Response.Write("//-------------------------------------------------------------------------------------------	" & vbCrLf)
Response.Write("function UpdValues(lintProvince,lstrProvince){" & vbCrLf)
Response.Write("//-------------------------------------------------------------------------------------------	" & vbCrLf)
Response.Write("    with (self.document.forms[0]){" & vbCrLf)
Response.Write("        elements[")


Response.Write(mstrQuote)


Response.Write("tcnProvince")


Response.Write(mstrQuote)


Response.Write("].value = lintProvince;" & vbCrLf)
Response.Write("        elements[")


Response.Write(mstrQuote)


Response.Write("tctProvince")


Response.Write(mstrQuote)


Response.Write("].value = lstrProvince;" & vbCrLf)
Response.Write("    }" & vbCrLf)
Response.Write("}" & vbCrLf)
Response.Write("" & vbCrLf)
Response.Write("//%InsChangeZipCode: Actualiza los parametros del codigo de zona" & vbCrLf)
Response.Write("//-------------------------------------------------------------------------------------------	" & vbCrLf)
Response.Write("function InsChangeZipCode(Field){" & vbCrLf)
Response.Write("//-------------------------------------------------------------------------------------------	" & vbCrLf)
Response.Write("    with (self.document.forms[0]){" & vbCrLf)
Response.Write("		valLocal.Parameters.Param1.sValue=Field.value;" & vbCrLf)
Response.Write("		valLocal.disabled = Field.value == 0;" & vbCrLf)
Response.Write("		btnvalLocal.disabled = valLocal.disabled;" & vbCrLf)
Response.Write("	    ShowPopUp('ShowDefValues.aspx?sField=ZipCode&amp;nZipCode=' + Field.value,'ShowDefValues',100,100,'No','No',3000,3000)" & vbCrLf)
Response.Write("    }" & vbCrLf)
Response.Write("}" & vbCrLf)
Response.Write("" & vbCrLf)
Response.Write("//%InsChangeChkDel: actualiza el estado de un checkbox" & vbCrLf)
Response.Write("//-------------------------------------------------------------------------------------------	" & vbCrLf)
Response.Write("function InsChangeChkDel(Field){" & vbCrLf)
Response.Write("//-------------------------------------------------------------------------------------------	" & vbCrLf)
Response.Write("    Field.value=(Field.checked?'1':'0');" & vbCrLf)
Response.Write("}" & vbCrLf)
Response.Write("" & vbCrLf)
Response.Write("//% insEnabledFields: Inhabilita los campos de la ventana que estén llenos si la variable" & vbCrLf)
Response.Write("//%					  de sesión ""sOriginalForm"" es diferente de blanco " & vbCrLf)
Response.Write("//---------------------------------------------------------------------------------------------------" & vbCrLf)
Response.Write("function insEnabledFields(){" & vbCrLf)
Response.Write("//---------------------------------------------------------------------------------------------------" & vbCrLf)
Response.Write("	with(self.document.forms[0]){" & vbCrLf)
Response.Write("	    elements[""optAdr[]""].disabled = !(elements[""optAdr[]""].checked);" & vbCrLf)
Response.Write("	    txtAddress.disabled = !(txtAddress.value== """");" & vbCrLf)
Response.Write("	    valZipCode.disabled = !(valZipCode.value=="""" || valZipCode.value==0);" & vbCrLf)
Response.Write("	    valLocal.disabled = !(valLocal.value=="""" || valLocal.value==0);" & vbCrLf)
Response.Write("	    cbeCountry.disabled = !(cbeCountry.value=="""" || cbeCountry.value==0);" & vbCrLf)
Response.Write("	    tctE_mail.disabled = !(tctE_mail.value=="""");" & vbCrLf)
Response.Write("	    tcnLatCardinG.disabled = !(tcnLatCardinG.value=="""" || tcnLatCardinG.value==0);" & vbCrLf)
Response.Write("	    tcnLatCardinM.disabled = !(tcnLatCardinM.value=="""" || tcnLatCardinM.value==0);" & vbCrLf)
Response.Write("        tcnLatCardinS.disabled = !(tcnLatCardinS.value=="""" || tcnLatCardinS.value==0);" & vbCrLf)
Response.Write("        tcnLonCardinG.disabled = !(tcnLonCardinG.value=="""" || tcnLonCardinG.value==0);" & vbCrLf)
Response.Write("	    tcnLonCardinM.disabled = !(tcnLonCardinM.value=="""" || tcnLonCardinM.value==0);	" & vbCrLf)
Response.Write("		tcnLonCardinS.disabled = !(tcnLonCardinS.value=="""" || tcnLonCardinS.value==0);" & vbCrLf)
Response.Write("	}" & vbCrLf)
Response.Write("}" & vbCrLf)
Response.Write("" & vbCrLf)
Response.Write("//% insEnabledFieldsPolicy: Inhabilita los campos de la ventana que estén llenos si la variable" & vbCrLf)
Response.Write("//%      				    de sesión ""sOriginalForm"" es diferente de blanco " & vbCrLf)
Response.Write("//---------------------------------------------------------------------------------------------------" & vbCrLf)
Response.Write("function insEnabledFieldsPolicy(){" & vbCrLf)
Response.Write("//---------------------------------------------------------------------------------------------------" & vbCrLf)
Response.Write("	with(self.document.forms[0]){" & vbCrLf)
Response.Write("		tctBuild.disabled = true;" & vbCrLf)
Response.Write("		valMunicipality.disabled = true;" & vbCrLf)
Response.Write("		btnvalMunicipality.disabled = true;" & vbCrLf)
Response.Write("		valLocal.disabled     = true;" & vbCrLf)
Response.Write("		btnvalLocal.disabled = true;" & vbCrLf)
Response.Write("		cbeProvince.disabled = true;" & vbCrLf)
Response.Write("		cbeCountry.disabled = true;" & vbCrLf)
Response.Write("		if(tctRecType.value==3)" & vbCrLf)
Response.Write("			tctPobox.disabled = true;" & vbCrLf)
Response.Write("		else{" & vbCrLf)
Response.Write("			txtAddress.disabled = true;" & vbCrLf)
Response.Write("			tcnFloor.disabled = true;" & vbCrLf)
Response.Write("			tctDepartment.disabled = true;" & vbCrLf)
Response.Write("			tctPopulation.disabled = true;" & vbCrLf)
Response.Write("			tctDescadd.disabled = true;" & vbCrLf)
Response.Write("			tcnZipCode.disabled = true;" & vbCrLf)
Response.Write("			tctE_mail.disabled = true;" & vbCrLf)
Response.Write("			tctE_mail.disabled = true;" & vbCrLf)
Response.Write("			tcnLatCardinG.disabled = true;" & vbCrLf)
Response.Write("			tcnLatCardinM.disabled = true;" & vbCrLf)
Response.Write("			tcnLatCardinS.disabled = true;" & vbCrLf)
Response.Write("			tcnLonCardinG.disabled = true;" & vbCrLf)
Response.Write("			tcnLonCardinM.disabled = true;	" & vbCrLf)
Response.Write("			tcnLonCardinS.disabled = true;" & vbCrLf)
Response.Write("			cmdAdd.disabled = true;" & vbCrLf)
Response.Write("			if(typeof(cmdDelete)!='undefined')" & vbCrLf)
Response.Write("				cmdDelete.disabled = true;" & vbCrLf)
Response.Write("		}" & vbCrLf)
Response.Write("	}" & vbCrLf)
Response.Write("}" & vbCrLf)
Response.Write("//% insClickAccept: Valores setados al ejecutar el botón aceptar   " & vbCrLf)
Response.Write("//-------------------------------------------------------------------------------------------" & vbCrLf)
Response.Write("function InsReaAccept(){" & vbCrLf)
Response.Write("//-------------------------------------------------------------------------------------------" & vbCrLf)
Response.Write("    top.close();" & vbCrLf)
Response.Write("}" & vbCrLf)
Response.Write("" & vbCrLf)
Response.Write("</" & "script>" & vbCrLf)
Response.Write("")

	
	Call insreaAddress(CStr(Request.QueryString.Item("sCodispl")))
	
	If mobjAddress.nCountry > 0 Then
		Session("nCountry") = mobjAddress.nCountry
	Else
		mobjGeneral = New eGeneral.OptionsInstallation
		If mobjGeneral.FindOptGeneral() Then
			Session("nCountry") = mobjGeneral.nCountry
		End If
		mobjGeneral = Nothing
	End If
	mobjValues.ActionQuery = mblnActionQuery
	
	
Response.Write("" & vbCrLf)
Response.Write("<TABLE WIDTH=""100%"" >    " & vbCrLf)
Response.Write("    <BR>" & vbCrLf)
Response.Write("" & vbCrLf)
Response.Write("    <tr>" & vbCrLf)
Response.Write("		<td WIDTH=""25%""><label>" & GetLocalResourceObject("txtAddressCaption") & "</label></td>" & vbCrLf)
Response.Write("		<td COLSPAN=""3"">")


Response.Write(mobjValues.TextAreaControl("txtAddress", 2, 40, mobjAddress.sStreet & " " & mobjAddress.sStreet1, True, GetLocalResourceObject("txtAddressToolTip"),  , Session("bQuery"),  , "insChangeAddress();"))


Response.Write("</td>" & vbCrLf)
Response.Write("    </tr>" & vbCrLf)
Response.Write("    <tr>" & vbCrLf)
Response.Write("        <td><label>" & GetLocalResourceObject("tctBuildCaption") & "</label></td>" & vbCrLf)
Response.Write("        <td>")


Response.Write(mobjValues.TextControl("tctBuild", 10, mobjAddress.sBuild,  , GetLocalResourceObject("tctBuildToolTip"),  ,  ,  , "insChangeAddress();", Session("bQuery")))


Response.Write("</td>" & vbCrLf)
Response.Write("        <td><label>" & GetLocalResourceObject("tcnFloorCaption") & "</label></td>" & vbCrLf)
Response.Write("        <td>")


Response.Write(mobjValues.NumericControl("tcnFloor", 5, CStr(mobjAddress.nFloor),  , GetLocalResourceObject("tcnFloorToolTip"),  ,  ,  ,  ,  , "insChangeAddress();", Session("bQuery")))


Response.Write("</td>" & vbCrLf)
Response.Write("    </tr>    " & vbCrLf)
Response.Write("    <tr>" & vbCrLf)
Response.Write("        <td><label>" & GetLocalResourceObject("tctDepartmentCaption") & "</label></td>" & vbCrLf)
Response.Write("        <td>")


Response.Write(mobjValues.TextControl("tctDepartment", 10, mobjAddress.sDepartment,  , GetLocalResourceObject("tctDepartmentToolTip"),  ,  ,  , "insChangeAddress();", Session("bQuery")))


Response.Write("</td>" & vbCrLf)
Response.Write("        <td><label>" & GetLocalResourceObject("tctPopulationCaption") & "</label></td>" & vbCrLf)
Response.Write("        <td>")


Response.Write(mobjValues.TextControl("tctPopulation", 40, mobjAddress.sPopulation,  , GetLocalResourceObject("tctPopulationToolTip"),  ,  ,  , "insChangeAddress();", Session("bQuery")))


Response.Write("</td>" & vbCrLf)
Response.Write("    </tr>    " & vbCrLf)
Response.Write("    <tr>" & vbCrLf)
Response.Write("        <td><label>" & GetLocalResourceObject("tctDescaddCaption") & "</label></td>" & vbCrLf)
Response.Write("        <td COLSPAN=""3"">")


Response.Write(mobjValues.TextAreaControl("tctDescadd", 2, 50, mobjAddress.sDescadd,  , GetLocalResourceObject("tctDescaddToolTip"),  , True))


Response.Write("</td>" & vbCrLf)
Response.Write("    </tr>" & vbCrLf)
Response.Write("    <tr>" & vbCrLf)
Response.Write("        <td><label>" & GetLocalResourceObject("tcnZipCodeCaption") & "</label></td>" & vbCrLf)
Response.Write("        <td>")


Response.Write(mobjValues.NumericControl("tcnZipCode", 10, CStr(mobjAddress.nZip_code),  , GetLocalResourceObject("tcnZipCodeToolTip"),  ,  ,  ,  ,  ,  , Session("bQuery")))


Response.Write("</td>" & vbCrLf)
Response.Write("        <td><label>" & GetLocalResourceObject("cbeCountryCaption") & "</label></td>" & vbCrLf)
Response.Write("        <td>")


Response.Write(mobjValues.PossiblesValues("cbeCountry", "Table66", eFunctions.Values.eValuesType.clngComboType, Session("nCountry"),  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeCountryToolTip")))


Response.Write("</td>" & vbCrLf)
Response.Write("    </tr>" & vbCrLf)
Response.Write("    <tr>" & vbCrLf)
Response.Write("        <td><label>" & GetLocalResourceObject("cbeProvinceCaption") & "</label></td>" & vbCrLf)
Response.Write("        <td>")


Response.Write(mobjValues.PossiblesValues("cbeProvince", "Tab_Province", eFunctions.Values.eValuesType.clngComboType, CStr(mobjAddress.nProvince),  ,  ,  ,  ,  , "insParameterLocat(this)", Session("bQuery"),  , GetLocalResourceObject("cbeProvinceToolTip")))


Response.Write("</td>" & vbCrLf)
Response.Write("        <td><label>" & GetLocalResourceObject("valLocalCaption") & "</label></td>" & vbCrLf)
Response.Write("        <td>")

	mobjValues.Parameters.Add("nProvince", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	Response.Write(mobjValues.PossiblesValues("valLocal", "tabTab_locat_a", eFunctions.Values.eValuesType.clngWindowType, CStr(mobjAddress.nLocal), True,  ,  ,  ,  , "insParameterMunicipality(this)", Session("bQuery"),  , GetLocalResourceObject("valLocalToolTip")))
Response.Write("</td>" & vbCrLf)
Response.Write("    </tr>" & vbCrLf)
Response.Write("    <tr>" & vbCrLf)
Response.Write("        <td><label>" & GetLocalResourceObject("valMunicipalityCaption") & "</label></td>" & vbCrLf)
Response.Write("        <td>")

	mobjValues.Parameters.Add("nLocat", mobjAddress.nLocal, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	mobjValues.Parameters.Add("nProvince", mobjAddress.nProvince, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	mobjValues.Parameters.ReturnValue("nLocal", False, vbNullString, True)
	Response.Write(mobjValues.PossiblesValues("valMunicipality", "tab_municipality_a", eFunctions.Values.eValuesType.clngWindowType, CStr(mobjAddress.nMunicipality), True,  ,  ,  ,  ,  , Session("bQuery"),  , GetLocalResourceObject("valMunicipalityToolTip")))
Response.Write("</td>" & vbCrLf)
Response.Write("    </tr>" & vbCrLf)
Response.Write("    </TABLE>" & vbCrLf)
Response.Write("    <TABLE>" & vbCrLf)
Response.Write("    <tr >    " & vbCrLf)
Response.Write("		<td WIDTH=""40%"">&nbsp</td>" & vbCrLf)
Response.Write("		<td> &nbsp</td>" & vbCrLf)
Response.Write("		<td WIDTH=""50%"">&nbsp</td>" & vbCrLf)
Response.Write("        <td  ALIGN=RIGTH>")


Response.Write(mobjValues.ButtonAcceptCancel("", "InsReaAccept()",  ,  , eFunctions.Values.eButtonsToShow.All))


Response.Write("</td>" & vbCrLf)
Response.Write("    </tr>  " & vbCrLf)
Response.Write("    </TABLE>")

	
	
	
End Sub

'%insreaAddress: Lee las direcciones asociadas de la tabla Address.
'--------------------------------------------------------------------------------------------
Private Sub insreaAddress(ByRef sCodispl As Object)
	'--------------------------------------------------------------------------------------------
	'- Objeto de direcciones
	Dim lcolAddresss As eGeneralForm.Addresss
	
	'- Variables para almacenar propiedades de objetos y variables de session
	Dim lstrClient As String
	Dim lblnFind As Boolean
	Dim lintnCod_Agree As Object
	
	'+ Se crean objetos a usar en proceso
	lcolAddresss = New eGeneralForm.Addresss
	
	'+ Se inicializan variables de trabajo    
	lstrClient = ""
	
	Select Case sCodispl
		'+ Dirección del Convenio
		Case "MCO505"
			mintRecowner = 14
			mstrRecType = "1"
			lintnCod_Agree = Session("ncod_agree")
	End Select
	'+ Se construye sKeyAddress    
	mstrKeyAddress = lcolAddresss.ConstructKeyAddress(mobjValues.StringToType(mintRecowner, eFunctions.Values.eTypeData.etdDouble), CShort(CStr(mstrRecType)), "", 0, 0, 0, 0, 0, "", 0, 0, 0, 0, mobjValues.StringToType(lintnCod_Agree, eFunctions.Values.eTypeData.etdLong)) ', 														  
	
	'+ Se recupera la dirección (de cualquier tipo), si existe	
	lblnFind = mobjAddress.Find(mstrKeyAddress, mobjValues.StringToType(mintRecowner, eFunctions.Values.eTypeData.etdDouble), dEffecDate, False, False)
	mblnExistAddress = lblnFind
	
	
	Session("Find_address") = 1
	lcolAddresss = Nothing
End Sub

</script>
<%Response.Expires = -1

mobjAddress = New eGeneralForm.Address
mobjValues = New eFunctions.Values


mobjValues.sCodisplPage = "sca001"

mstrLastClient = Session("sClient")
mblnLastAction = Session("bQuery")

mobjValues.ActionQuery = Session("bQuery") Or CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401
mstrQuote = """"
mstrAmp = "&"

Session("sClient") = Request.QueryString.Item("sClient_Contact")
mstrClient = Request.QueryString.Item("sClient_Contact")
Session("ncod_agree") = Request.QueryString.Item("ncod_agree")
mstrcodagree = Request.QueryString.Item("ncod_agree")

mstrRecType = Request.QueryString.Item("sRectype")
dEffecDate = mobjValues.TypeToString(Today, eFunctions.Values.eTypeData.etdDate)
%>
<html>
<head>
    <META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>  
<script>
//+ Variable para el control de versiones
	document.VssVersion="$$Revision: 3 $|$$Date: 31/08/04 18:35 $"
</script>
<script LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></script>


    <%=mobjValues.StyleSheet()%>
    <%
mobjMenu = New eFunctions.Menues
With Response
	.Write(mobjValues.WindowsTitle(Request.QueryString.Item("sCodispl")))
End With
mobjMenu = Nothing
%>
<script>
//%insChangeAddress: Actualiza los campos de las direcciones
//---------------------------------------------------------------------------
function insChangeAddress(){
//---------------------------------------------------------------------------
	with(document.forms[0]){
		tctDescadd.value= txtAddress.value + ' ' + tctBuild.value;
		if((tctDepartment.value!=0)&&
		   (tctDepartment.value!=''))
			tctDescadd.value += ', Dpto. ' + tctDepartment.value 
		if((tcnFloor.value!=0)&&
		   (tcnFloor.value!='')) 
			tctDescadd.value += ', Piso ' + tcnFloor.value;
		if((tctPopulation.value!=0)&&
		   (tctPopulation.value!='')) 
			tctDescadd.value += ', ' + tctPopulation.value;
	}
}	
//%insParameterLocat: Actualiza parametros de la region
//---------------------------------------------------------------------------
function insParameterLocat(Field){
//---------------------------------------------------------------------------
	with(self.document.forms[0]){
		valLocal.Parameters.Param1.sValue=Field.value;
		valMunicipality.Parameters.Param1.sValue=0;
		valMunicipality.Parameters.Param2.sValue=Field.value;		
		valLocal.disabled=(Field.value=='')?true:false;
		valLocal.value='';
		UpdateDiv('valLocalDesc','')
		valMunicipality.value='';
		UpdateDiv('valMunicipalityDesc','')
	}
	
}	
//%insParameterMunicipality: Actualiza parametros de la comuna
//---------------------------------------------------------------------------
function insParameterMunicipality(Field){
//---------------------------------------------------------------------------
	with(self.document.forms[0]){
		valMunicipality.Parameters.Param1.sValue=Field.value;
		valMunicipality.Parameters.Param2.sValue=cbeProvince.value;
		
		if (Field.value == '')
			valMunicipality.Parameters.Param1.sValue=0;
		
		valMunicipality.disabled=(Field.value=='')?true:false;
		if(valMunicipality_nLocal.value!=Field.value){
			valMunicipality.value='';
			UpdateDiv('valMunicipalityDesc','')
		}
	}
}	
</script>
</head>
<BODY ONUNLOAD="closeWindows();">
<%=mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl"))%>
<FORM METHOD="POST" ID="FORM" NAME="frmSCA001upd" ACTION="valGeneralForm.aspx?WindowType=PopUp<%=mstrAmp%>sCodisplReload=1<%=mstrAmp%>Action=Add<%=mstrAmp%>nRecOwner=<%=mintRecowner%><%=mstrAmp%>sKeyAddress=<%=mstrKeyAddress%><%=mstrAmp%>sCodispl=<%=Request.QueryString.Item("sCodispl")%><%=mstrAmp%>nMainAction=<%=Request.QueryString.Item("nMainAction")%><%=mstrAmp%>sOnSeq=<%=mstrOnSeq%><%=mstrAmp%>sRecType=<%=mstrRecType%><%=mstrAmp%>sClient_Contact=<%=mstrClient%><%=mstrAmp%>ncod_agree=<%=mstrcodagree%>">
<table WIDTH="100%" border="0">
<%
'Call insDefineHeader()
Call insPreSCA001()

Session("sClient") = mstrLastClient
Session("bQuery") = mblnLastAction

mobjAddress = Nothing
mobjGrid = Nothing

%>
</table>
</form>
</body>
</html>
<%
'+ Cuando es invocada de otra transaccion se deshabilitan los campos que viene con datos
If CStr(Session("sOriginalForm")) <> vbNullString Then
	Response.Write("<SCRIPT>insEnabledFields();</SCRIPT>")
End If

If Request.QueryString.Item("sCodispl") = "SCA102" And Request.QueryString.Item("Type") <> "PopUp" Then
	'+ Si la direccion de envío de poliza es distinta a "Por Póliza", 
	'+ entonces no se puede permitir incluir información  de dirección
	Response.Write("<SCRIPT>")
	If mclsCertificat.nSendAddr <> 4 Then
		If (Session("nTransaction") <> 12 And Session("nTransaction") <> 14) Then
			Response.Write("alert(""No puede incluir dirección de envío de póliza (Datos para la facturación)"");")
		End If
		If Request.QueryString.Item("Type") <> "PopUp" Then
			Response.Write("insEnabledFieldsPolicy();")
		End If
	End If
	Response.Write("</SCRIPT>")
End If

mclsCertificat = Nothing

%>





