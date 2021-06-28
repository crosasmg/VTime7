<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo de las rutinas genéricas
Dim mobjMenu As eFunctions.Menues


</script>
<%
Response.Expires = -1

mobjMenu = New eFunctions.Menues
mobjValues = New eFunctions.Values
mobjValues.sCodisplPage = "MAU101"
%>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></SCRIPT>


<HTML>
<HEAD>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">
<%
With Response
	.Write(mobjValues.StyleSheet() & vbCrLf)
	.Write(mobjMenu.MakeMenu("MAU101", "MAU101_K.aspx", 1, ""))
	.Write("<BR>")
End With
mobjMenu = Nothing
%>
<SCRIPT>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 1 $|$$Date: 2/09/03 19:02 $|$$Author: Iusr_llanquihue $"

//% insCancel: Ejecuta rutinas necesarias en el momento de cancelar la página
//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
	return true;
}
//% insStateZone: se manejan los campos de la página
//------------------------------------------------------------------------------------------
 function insStateZone(){
//------------------------------------------------------------------------------------------      
	with(self.document.forms[0]){
		tcdEffecDate.disabled=false;
		btn_tcdEffecDate.disabled=false;
		cbeType.disabled=false;
	}
}
</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="frmDeduc_Auto" ACTION="valMantAuto.aspx?sTime=1">
    <BR><BR>
    <TABLE WIDTH="100%">            
        <TR>
            <TD><LABEL ID=11781><%= GetLocalResourceObject("cbeTypeCaption") %></LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("cbeType", "Table226", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeTypeToolTip"))%></TD>
            <TD>&nbsp;</TD>
            <TD><LABEL ID=11780><%= GetLocalResourceObject("tcdEffecDateCaption") %></LABEL></TD>
            <TD><%=mobjValues.DateControl("tcdEffecDate", "",  , GetLocalResourceObject("tcdEffecDateToolTip"),  ,  ,  ,  , True)%></TD>
        </TR>
    </TABLE>
</FORM>
</BODY>
</HTML>





