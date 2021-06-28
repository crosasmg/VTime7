<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues


</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues

mobjValues.sCodisplPage = "dp016_k"
%>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></SCRIPT>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">


    <%
With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjMenu.MakeMenu("DP016", "DP016_k.aspx", 1, ""))
End With

mobjMenu = Nothing
%>
<SCRIPT>
//- Variable para el control de versiones
document.VssVersion="$$Revision: 1 $|$$Date: 2/09/03 19:04 $|$$Author: Iusr_llanquihue $"

//% insCancel: Se cancela la página invocada.
//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
	return true;
}
//% insStateZone: Permite habilitar los objetos e imágenes de la página.
//------------------------------------------------------------------------------------------
function  insStateZone(){
//------------------------------------------------------------------------------------------
	with(self.document.forms[0]){
		valMortalco.disabled=false;
		btnvalMortalco.disabled=false;
		tcnInterest.disabled=false;
	}
}
</SCRIPT>
</HEAD>

<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="DP016_k" ACTION="valProduct.aspx?mode=1">
<BR> <BR>
    <TABLE WIDTH="100%">
		<TR>
			<TD WIDTH=12%><LABEL ID=14049><%= GetLocalResourceObject("valMortalcoCaption") %></LABEL></TD>
			<TD><%
mobjValues.Parameters.Add("sStatregt", "1", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
Response.Write(mobjValues.PossiblesValues("valMortalco", "tabMort_master", eFunctions.Values.eValuesType.clngWindowType, vbNullString, True,  ,  ,  ,  ,  , True, 6, GetLocalResourceObject("valMortalcoToolTip"), eFunctions.Values.eTypeCode.eString, 1))
%>
            </TD>
            <TD WIDTH=12%><LABEL ID=14048><%= GetLocalResourceObject("tcnInterestCaption") %></LABEL></TD>
            <TD> <%=mobjValues.NumericControl("tcnInterest", 4, CStr(0), False, GetLocalResourceObject("tcnInterestToolTip"),  , 2,  ,  ,  ,  , True, 2)%> </TD>
        </TR>
    </TABLE>
</FORM>
</BODY>
</HTML>

<%
mobjValues = Nothing%>





