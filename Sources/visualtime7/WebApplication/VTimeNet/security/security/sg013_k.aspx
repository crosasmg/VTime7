<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores

Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues
Dim mstrQuote As String


</script>
<%Response.Expires = -1
mobjValues = New eFunctions.Values
mobjValues.sCodisplPage = "SG013_k"

mstrQuote = """"
%>

<HTML>
<HEAD>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tMenu.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></SCRIPT>


    
    <%mobjMenu = New eFunctions.Menues
With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjMenu.MakeMenu("SG013_k", "SG013_k.aspx", 1, ""))
End With

mobjMenu = Nothing
%>

<SCRIPT>
//% insStateZone: 
//--------------------------------------------------------------------------------------------
function insStateZone(){
//--------------------------------------------------------------------------------------------
    var lintIndex = 0;
    
    for (lintIndex=0; lintIndex < document.forms[0].length; lintIndex++)
         document.forms[0].elements[lintIndex].disabled = false;
			       
    for (lintIndex=0; lintIndex < document.images.length; lintIndex++)
         document.images[lintIndex].disabled = false;
         
}
//% insCancel: 
//--------------------------------------------------------------------------------------------
function insCancel(){
//--------------------------------------------------------------------------------------------
	if(top.frames['fraSequence'].pintZone==1)
		return true
	else
	{
		if(top.frames['fraSequence'].plngMainAction==301)
			ShowPopUp("/VTimeNet/Common/GE101.aspx?sCodispl=SG013_k", "EndProcess", 300, 150)
		else
			top.location.reload();
	}
}

//% insFinish: Ejecuta la acción de Finalizar de la página.
//--------------------------------------------------------------------------------------------
function insFinish(){
//--------------------------------------------------------------------------------------------
    return true;
}

//- Esta línea guarda la versión procedente de VSS 
    document.VssVersion="$$Revision: 1 $|$$Date: 2/09/03 19:05 $|$$Author: Iusr_llanquihue $"

</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="frmSchemaDef" ACTION="valSecuritySeqSchema.aspx?sMode=1">

<BR> </BR>
    <TABLE WIDTH="100%">
        <TR>
            <TD WIDTH="15%"><LABEL ID=14981><%= GetLocalResourceObject("valScheCodeCaption") %></LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("valScheCode", "Tab_Schema", eFunctions.Values.eValuesType.clngWindowType, vbNullString,  ,  ,  ,  ,  ,  , True, 6, GetLocalResourceObject("valScheCodeToolTip"), eFunctions.Values.eTypeCode.eString, 1,  , True)%></TD>
        </TR>
    </TABLE>
</FORM>
</BODY>
</HTML>

<%
mobjValues = Nothing
%>





