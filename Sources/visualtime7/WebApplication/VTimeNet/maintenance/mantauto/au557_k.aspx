<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo del menú
Dim mobjMenu As eFunctions.Menues


</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues
mobjValues.sCodisplPage = "AU557"
%>
<HTML>
<HEAD>
<SCRIPT>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 1 $|$$Date: 2/09/03 19:02 $|$$Author: Iusr_llanquihue $"
</SCRIPT>
<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></SCRIPT>



<SCRIPT LANGUAGE="JavaScript">

//% insStateZone: se controla el estado de los campos de la página
//--------------------------------------------------------------------------------------------
function insStateZone(){
//--------------------------------------------------------------------------------------------
	var lintIndex = 0;
    for (lintIndex=0;lintIndex<document.forms[0].length;lintIndex++)
        document.forms[0].elements[lintIndex].disabled = false;
}
//% insCancel: se controla la acción Cancelar de la página
//--------------------------------------------------------------------------------------------
function insCancel(){
//--------------------------------------------------------------------------------------------
	return true;
}
//% ShowData: Se muestra el dígito verificador de la patente
//-------------------------------------------------------------------------------------------
function ShowData(sField){
//-------------------------------------------------------------------------------------------
	switch(sField){
		case "Regist":
			if(self.document.forms[0].tctRegister.value!=''){
				insDefValues(sField,"sRegist=" + self.document.forms[0].tctRegister.value + "&sLicense_ty=" + self.document.forms[0].cbeLicense_ty.value)
				break;
			}
	}
}
//% insFinish: se controla la acción Cancelar de la página
//--------------------------------------------------------------------------------------------
function insFinish(){
//--------------------------------------------------------------------------------------------
    return true;
}
</SCRIPT>
	<%
With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjMenu.MakeMenu("AU557", "AU557_K.aspx", 1, ""))
End With
mobjMenu = Nothing
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" NAME="AU557_K" ACTION="valMantAuto.aspx?sMode=1">
<BR><BR>
    <TABLE WIDTH="100%">
		<TR>
			<TD><LABEL><%= GetLocalResourceObject("cbeLicense_tyCaption") %></LABEL></TD>
			<TD><%mobjValues.BlankPosition = False
Response.Write(mobjValues.PossiblesValues("cbeLicense_ty", "table80", eFunctions.Values.eValuesType.clngComboType, "1",  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeLicense_tyToolTip")))%></TD>
			<TD><LABEL><%= GetLocalResourceObject("tctRegisterCaption") %></LABEL></TD>
             <TD><%=mobjValues.TextControl("tctRegister", 10,  ,  , GetLocalResourceObject("tctRegisterToolTip"),  ,  ,  , "ShowData(""Regist"")", True)%>-<%=mobjValues.TextControl("tctDigit", 1,  ,  , "Dígito verificador de la patente",  ,  ,  ,  , True)%></TD>
        </TR>
    </TABLE>
</FORM> 
</BODY>
</HTML>




