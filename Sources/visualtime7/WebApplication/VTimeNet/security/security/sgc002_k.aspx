<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores

Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues


</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjValues.sCodisplPage = "SGC002_K"

mobjMenu = New eFunctions.Menues
%>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></script>


    <%
With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjMenu.MakeMenu("SGC002", "SGC002_k.aspx", 1, ""))
End With

mobjMenu = Nothing%>
<SCRIPT>
//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
	return true;
}
//------------------------------------------------------------------------------------------
function  insStateZone(){
//------------------------------------------------------------------------------------------
    var lintIndex = 0;
    
    for (lintIndex=0; lintIndex < document.forms[0].length; lintIndex++)
         document.forms[0].elements[lintIndex].disabled = false
			       
    for (lintIndex=0; lintIndex < document.images.length; lintIndex++){
        if (document.images[lintIndex].belongtoolbar!=true)
         document.images[lintIndex].disabled = false
    }         
}

//% UpperCase: Permite colocar en mayúscula los campos Lógico, Físico y Pseudónimo. 
//--------------------------------------------------------------------------------------------
function UpperCase(){
//--------------------------------------------------------------------------------------------
	var lstrvalues
    document.forms[0].tctCodispl.value = document.forms[0].tctCodispl.value.toUpperCase();
    document.forms[0].tctCodisp.value = document.forms[0].tctCodisp.value.toUpperCase();
    document.forms[0].tctPseudo.value = document.forms[0].tctPseudo.value.toUpperCase();
    lstrvalues = document.forms[0].tctCodispl.value
    if (lstrvalues.indexOf("'")>-1){
		alert("el campo no acepta el caracter '");
		document.forms[0].tctCodispl.value = "";
	}
    lstrvalues = document.forms[0].tctCodisp.value
    if (lstrvalues.indexOf("'")>-1){
		alert("el campo no acepta el caracter '");
		document.forms[0].tctCodisp.value = "";
	}
	lstrvalues = document.forms[0].tctPseudo.value
    if (lstrvalues.indexOf("'")>-1){
		alert("el campo no acepta el caracter '");
		document.forms[0].tctPseudo.value = "";
	}
}

//+ Variable para el control de versiones
    document.VssVersion="$$Revision: 1 $|$$Date: 2/09/03 19:05 $"
</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="SGC002" ACTION="valSecurityQue.aspx?Mode=1">
	<BR><BR>
    <TABLE WIDTH="100%">
		<TR>
            <TD><LABEL ID=14969><%= GetLocalResourceObject("cbeModulesCaption") %></LABEL></TD>
            <TD WIDTH=89%><%=mobjValues.PossiblesValues("cbeModules", "Table87", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeModulesToolTip"),  , 1)%></TD>
        </TR>
    </TABLE>        
    <TABLE WIDTH="100%">        
        <TR>
            <TD><LABEL ID=14968><%= GetLocalResourceObject("tctCodisplCaption") %></LABEL></TD>
            <TD><%Response.Write(mobjValues.TextControl("tctCodispl", 8, vbNullString, False, GetLocalResourceObject("tctCodisplToolTip"),  ,  ,  , "UpperCase()", True, 2))%></TD>
            <TD><LABEL ID=14967><%= GetLocalResourceObject("tctCodispCaption") %></LABEL></TD>
            <TD><%Response.Write(mobjValues.TextControl("tctCodisp", 8, vbNullString, False, GetLocalResourceObject("tctCodispToolTip"),  ,  ,  , "UpperCase()", True, 3))%></TD>
            <TD><LABEL ID=14970><%= GetLocalResourceObject("tctPseudoCaption") %></LABEL></TD>
            <TD><%Response.Write(mobjValues.TextControl("tctPseudo", 12, vbNullString, False, GetLocalResourceObject("tctPseudoToolTip"),  ,  ,  , "UpperCase()", True, 4))%></TD>
		</TR>
    </TABLE>
</FORM>
</BODY>
</HTML>

<%
mobjValues = Nothing%>





