<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores

Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues


</script>
<%Response.Expires = -1

With Server
	mobjMenu = New eFunctions.Menues
	mobjValues = New eFunctions.Values
End With
mobjValues.sCodisplPage = "SG001_k"
%>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></script>
<HTML>
<HEAD>
<META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">


<%
With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjMenu.MakeMenu("SG001", "SG001_k.aspx", 1, ""))
End With
mobjMenu = Nothing
%>
<SCRIPT>
//+ Variable para el control de versiones
    document.VssVersion="$$Revision: 1 $|$$Date: 2/09/03 19:05 $"

//%insCancel: Esta función finaliza la transacción al presionar cancelar.
//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
    return true;
}

//%insStateZone: Permite habilitar los objetos y las imagenes en la ventana.
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
</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="SG001" ACTION="valSecurity.aspx?mode=1">
<BR><BR>
<TABLE WIDTH="100%">
    <TR>
        <TD WIDTH=200pcx ALIGN=RIGHT><LABEL ID=15071><%= GetLocalResourceObject("valUsercodCaption") %></LABEL></TD>
        <TD><%=mobjValues.PossiblesValues("valUsercod", "tabUsers", eFunctions.Values.eValuesType.clngWindowType, vbNullString,  ,  ,  ,  ,  ,  , True, 5, GetLocalResourceObject("valUsercodToolTip"),  ,  ,  , True)%></TD>
    </TR>
</TABLE>
</FORM>
</BODY>
</HTML>
<%
mobjValues = Nothing%>





