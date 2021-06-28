<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo de las rutinas genéricas

Dim mobjMenu As eFunctions.Menues


</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values

mobjValues.sCodisplPage = "MSI015"
%>
<HTML>
<HEAD>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/ValFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></SCRIPT>    


    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">
    <%=mobjValues.StyleSheet()%>
<SCRIPT>
//% insCancel: se controla la acción Cancelar de la página
//---------------------------------------------------------------------------------------------------
function insCancel(){
//---------------------------------------------------------------------------------------------------
    return true;
}

//% insStateZone: se controla el estado de los campos de la página
//---------------------------------------------------------------------------------------------------
function insStateZone(){
//---------------------------------------------------------------------------------------------------
	self.document.forms[0].cbeBranch.disabled=false;
}                  

//% InsParamValue: Asigna Cobertura, Causa, Modulo
//---------------------------------------------------------------------------------------------------
function InsParamValue(){
//---------------------------------------------------------------------------------------------------
	with(self.document.forms[0]){
		cbeCover.Parameters.Param1.sValue = cbeBranch.value;
		cbeCover.Parameters.Param2.sValue = valProduct.value;
		
		cbeCauscodcl.Parameters.Param1.sValue = cbeBranch.value;
		cbeCauscodcl.Parameters.Param2.sValue = valProduct.value;
		
		cbeModulec.Parameters.Param1.sValue = cbeBranch.value;
		cbeModulec.Parameters.Param2.sValue = valProduct.value;

		cbeCover.disabled = (valProduct.value=='')?true:false;
		btncbeCover.disabled = (valProduct.value=='')?true:false;
		cbeCauscodcl.disabled = (valProduct.value=='')?true:false;
		btncbeCauscodcl.disabled = (valProduct.value=='')?true:false;
		cbeModulec.disabled = (valProduct.value=='')?true:false;
		btncbeModulec.disabled = (valProduct.value=='')?true:false;
	}
} 
//% InsParamValue_Modul: Asigna Modulo
//---------------------------------------------------------------------------------------------------
function InsParamValue_Modul(lobj){
//---------------------------------------------------------------------------------------------------
	self.document.forms[0].cbeCover.Parameters.Param3.sValue = lobj.value;
}

//% InsValueInit: Limpia valiables de llave de acceso
//---------------------------------------------------------------------------------------------------
function InsValueInit(){
//---------------------------------------------------------------------------------------------------
	with(self.document.forms[0]){
		cbeModulec.value = "";
		UpdateDiv('cbeModulecDesc',"")	
		cbeCover.value = "";
		UpdateDiv('cbeCoverDesc',"")	
		cbeCauscodcl.value = "";		
		UpdateDiv('cbeCauscodclDesc',"")	
	}
} 
//% InsValueInit_Modulo: Limpia valiables de llave de acceso
//---------------------------------------------------------------------------------------------------
function InsValueInit_Modulo(){
//---------------------------------------------------------------------------------------------------
	with(self.document.forms[0]){
		cbeCover.value = "";
		UpdateDiv('cbeCoverDesc',"")	
		cbeCauscodcl.value = "";		
		UpdateDiv('cbeCauscodclDesc',"")	
	}
} 
//% InsValueInit_Cobertura: Limpia valiables de llave de acceso
//---------------------------------------------------------------------------------------------------
function InsValueInit_Cobertura(){
//---------------------------------------------------------------------------------------------------
	with(self.document.forms[0]){
		cbeCauscodcl.value = "";		
		UpdateDiv('cbeCauscodclDesc',"")	
	}
} 
//% insFinish: Se controla la acción de Finalizar la página
//------------------------------------------------------------------------------------------
function insFinish(){
//------------------------------------------------------------------------------------------
	return true;
}

//% ReaModules: 
//------------------------------------------------------------------------------------------
function ReaModules()
//------------------------------------------------------------------------------------------
{
	ShowPopUp("/VTimeNet/Maintenance/MantClaim/ShowDefValues.aspx?Field=Modules&nBranch=" + self.document.forms[0].elements['cbeBranch'].value + 
	                                                         "&nProduct=" + self.document.forms[0].elements['valProduct'].value, "ShowDefValuesMantClaim", 1, 1,"no","no",2000,2000);
}
//-Variable para el control de Versiones
document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 15:53 $|$$Author: Nvaplat61 $"

</SCRIPT>
<%
With Response
	mobjMenu = New eFunctions.Menues
	.Write(mobjMenu.MakeMenu("MSI015", "MSI015_K.aspx", 1, ""))
	mobjMenu = Nothing
End With
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="frmTabExtComm" ACTION="ValMantClaim.aspx?mode=1">
	<BR></BR>
    <TABLE BORDER= 0 WIDTH="100%">
		<TR>
			<TD><LABEL ID=13372><%= GetLocalResourceObject("cbeBranchCaption") %></LABEL></TD>
			<TD><%Response.Write(mobjValues.BranchControl("cbeBranch", GetLocalResourceObject("cbeBranchToolTip"), CStr(0), "valProduct",  ,  ,  , "InsValueInit();", True))%></TD>
			<TD><LABEL ID=13382><%= GetLocalResourceObject("valProductCaption") %></LABEL></TD>
			<TD><%=mobjValues.ProductControl("valProduct", GetLocalResourceObject("valProductToolTip"), CStr(0),  , True,  ,  ,  ,  , "InsParamValue();InsValueInit();ReaModules();")%></TD>
		</TR>
		<TR>
            <TD WIDTH ="15%"><LABEL><%= GetLocalResourceObject("cbeModulecCaption") %></LABEL></TD>
            <TD>
              <%
With mobjValues
	Call .Parameters.Add("nBranch", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	Call .Parameters.Add("nProduct", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	Call .Parameters.Add("dEffecdate", Today, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	Response.Write(mobjValues.PossiblesValues("cbeModulec", "tabtab_modul", eFunctions.Values.eValuesType.clngWindowType, vbNullString, True,  ,  ,  ,  , "InsParamValue_Modul(this);InsValueInit_Modulo();", True,  , GetLocalResourceObject("cbeModulecToolTip")))
End With
%>
            </TD>

			<TD><LABEL ID=11737><%= GetLocalResourceObject("cbeCoverCaption") %></LABEL></TD>
			<TD><%With mobjValues
	.Parameters.Add("nBranch", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Parameters.Add("nProduct", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Parameters.Add("nModulec", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Parameters.Add("dEffecdate", Today, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	Response.Write(mobjValues.PossiblesValues("cbeCover", "tab_cover", eFunctions.Values.eValuesType.clngWindowType,  , True,  ,  ,  ,  , "InsValueInit_Cobertura()", True,  , GetLocalResourceObject("cbeCoverToolTip")))
End With
%>
			</TD>
		</TR>
		<TR>
   			<TD><LABEL ID=0><%= GetLocalResourceObject("cbeCauscodclCaption") %></LABEL></TD>
			<TD><%With mobjValues
	.Parameters.Add("nBranch", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Parameters.Add("nProduct", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	Response.Write(mobjValues.PossiblesValues("cbeCauscodcl", "tabclaim_caus", eFunctions.Values.eValuesType.clngWindowType,  , True,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeCauscodclToolTip")))
End With
%>
			</TD>
		</TR> 
	</TABLE>
</FORM>
</BODY>
</HTML> 

<%
mobjValues = Nothing%>





