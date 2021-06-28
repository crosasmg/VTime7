<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo de las rutinas genéricas y menues
Dim mobjMenu As eFunctions.Menues


</script>
<%Response.Expires = -1
mobjValues = New eFunctions.Values

mobjValues.sCodisplPage = "MCO827_K"
%>
<HTML>
<HEAD>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/valFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></SCRIPT>

<SCRIPT>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 1 $|$$Date: 2/09/03 19:03 $|$$Author: Iusr_llanquihue $"

</SCRIPT>
<SCRIPT LANGUAGE="JavaScript">
//% insCancel: se controla la acción cancelar de la transacción
//------------------------------------------------------------------------------------------
function insChangeWay_pay(Field){
//------------------------------------------------------------------------------------------
	var lblnDisabled = Field.value!=1
	
	with(self.document.forms[0]){
		valBank_code.disabled=lblnDisabled;
		btnvalBank_code.disabled=lblnDisabled;

		if(lblnDisabled){
			UpdateDiv('valBank_codeDesc','')
			valBank_code.value='';
		}
	}
}
//% insCancel: se controla la acción cancelar de la transacción
//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
	return true;
}
//% insStateZone: se controla el estado de los campos de la transacción
//------------------------------------------------------------------------------------------
function  insStateZone(){
//------------------------------------------------------------------------------------------
	self.document.forms[0].cbeWay_pay.disabled=false;
}
</SCRIPT>
<%
mobjMenu = New eFunctions.Menues
With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjMenu.MakeMenu("MCO827", "MCO827_K.aspx", 1, Request.QueryString.Item("sWindowDescript"), Session("sDesMultiCompany"), Session("sSche_code")))
End With
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="MCO827" ACTION="valMantCollection.aspx?sMode=1">
	<BR><BR>
	<TABLE WIDTH="100%">
        <TR>
			<TD WIDTH="15%"><LABEL ID=0><%= GetLocalResourceObject("cbeWay_payCaption") %></LABEL></TD>
			<TD><%
With mobjValues
	.TypeList = 1
	.List = "1,2"
	Response.Write(mobjValues.PossiblesValues("cbeWay_pay", "Table5002", eFunctions.Values.eValuesType.clngComboType, vbNullString,  ,  ,  ,  ,  , "insChangeWay_pay(this)", True,  , GetLocalResourceObject("cbeWay_payToolTip")))
End With
%>
			</TD>
        </TR>
        <TR>
			<TD><LABEL ID=0><%= GetLocalResourceObject("valBank_codeCaption") %></LABEL></TD>
			<TD><%=mobjValues.PossiblesValues("valBank_code", "tabBank_Agree_Pac", eFunctions.Values.eValuesType.clngWindowType, vbNullString,  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("valBank_codeToolTip"))%></TD> 
        <TR>
    </TABLE>		
</BODY>
</FORM>
</HTML>
<%
mobjValues = Nothing
mobjMenu = Nothing
%>






