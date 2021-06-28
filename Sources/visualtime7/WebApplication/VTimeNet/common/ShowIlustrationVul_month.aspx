<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eGeneral" %>
<%@ Import namespace="ePolicy" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo     
Dim lclsNull_condi As Object
Dim mclsPolicy As Object
Dim nGuaranty As Object
Dim nIntwarr2 As Object
Dim nIntwarrSav2 As Object
Dim lclsProduct As Object

'- Objeto para el manejo del grid de la página
Dim mobjGrid As Object



'% InsPreVI1410: se realiza el manejo del grid
'--------------------------------------------------------------------------------------------
Private Sub InsShowVul()
	'--------------------------------------------------------------------------------------------
	'- Objeto para el manejo particular de los datos de la página
	Dim lcolProjectvul As ePolicy.Projectvuls
	Dim lclsProjectvul As Object
	Dim llngCount As Object
	Dim lblnQuery As Object
	Dim nPeriod As Object
	Dim lclsGeneral As eGeneral.GeneralFunction
	lclsGeneral = New eGeneral.GeneralFunction
	Session("sKey") = lclsGeneral.getsKey(Session("nUsercode"))
	lclsGeneral = Nothing
	lcolProjectvul = New ePolicy.Projectvuls
	Response.Write(lcolProjectvul.MakeVI1410(Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), Session("sKey"), mobjValues.StringToType(Request.QueryString.Item("nYear"), eFunctions.Values.eTypeData.etdDouble)))
	lcolProjectvul = Nothing
	lclsProjectvul = Nothing
	
End Sub

</script>
<%Response.Expires = -1

Response.CacheControl = "private"

Response.Buffer = True

mobjValues = New eFunctions.Values

mobjValues.sCodisplPage = Request.QueryString.Item("sCodispl")

mobjValues.ActionQuery = True
%>
<HTML>
<HEAD>
    <META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>


    
<SCRIPT>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 10 $|$$Date: 10-05-06 13:01 $|$$Author: Clobos $"

//%InsProcessed: Actualiza el indicador de procesado
//-----------------------------------------------------------------------------------------
function InsProcessed(){
//-----------------------------------------------------------------------------------------
	if (typeof(top.opener.document.forms[0].hddsProcessed) != 'undefined'){
		top.opener.document.forms[0].hddsProcessed.value = '1';
	}
}
</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" NAME="VI1410">
<%
Response.Write(mobjValues.StyleSheet())
Response.Write(mobjValues.ShowWindowsName("VI1410", "Ilustración mensual del año " & Request.QueryString.Item("nYear")))
Response.Write("<BR>")
Call InsShowVul()
mobjValues = Nothing
mobjGrid = Nothing
lclsNull_condi = Nothing
%>
</FORM> 
</BODY>
</HTML>




