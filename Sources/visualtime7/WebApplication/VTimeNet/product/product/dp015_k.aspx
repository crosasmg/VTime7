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

mobjValues.sCodisplPage = "dp015_k"
%>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></SCRIPT>


<SCRIPT>
//- Variable para el control de versiones
	document.VssVersion="$$Revision: 1 $|$$Date: 2/09/03 19:04 $|$$Author: Iusr_llanquihue $"

//% insCancel: Esta función finaliza la transacción al presionar cancelar.
//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
	return true;
}
//% insStateZone: Permite habilitar los objetos y las imágenes en la ventana. En
//% esta página el tratamiento es diferente, ya que la acción Entrar es la única que 
//% posee por lo tanto debería entrar con los objetos ya habilitados.
//------------------------------------------------------------------------------------------
function  insStateZone(){
//------------------------------------------------------------------------------------------
}
</SCRIPT>
    <%
With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjMenu.MakeMenu("DP015", "DP015_K.aspx", 1, ""))
End With
mobjMenu = Nothing
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="DP015" ACTION="valProduct.aspx?Mode=1">
	<BR> <BR>
	<TABLE WIDTH="100%">
        <TR>	
		    <TD COLSPAN=4><%=mobjValues.ShowWindowsName("DP015")%></TD>	
        </TR>		    
		<TR>
			<TD WIDTH=10%><LABEL ID=14053><%= GetLocalResourceObject("valMortalcoCaption") %></LABEL></TD>
			<TD WIDTH=40%>
				<%
mobjValues.Parameters.Add("sStatregt", "1", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
Response.Write(mobjValues.PossiblesValues("valMortalco", "tabMort_master", eFunctions.Values.eValuesType.clngWindowType, vbNullString, True,  ,  ,  ,  ,  ,  , 6, GetLocalResourceObject("valMortalcoToolTip"), eFunctions.Values.eTypeCode.eString, 1))
%>
			</TD>
			<TD WIDTH=10%><LABEL ID=14052><%= GetLocalResourceObject("tcnInterestCaption") %></LABEL></TD>
            <TD> <%=mobjValues.NumericControl("tcnInterest", 4, CStr(0), False, GetLocalResourceObject("tcnInterestToolTip"),  , 2,  ,  ,  ,  ,  , 2)%> </TD>
        </TR>
    </TABLE>
</FORM>
</BODY>
</HTML>
<%
mobjValues = Nothing%>





