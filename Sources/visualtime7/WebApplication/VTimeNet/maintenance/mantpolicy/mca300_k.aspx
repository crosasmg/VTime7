<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">
'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues

</script>
<%
mobjValues = New eFunctions.Values
Response.Expires = -1
%>
<HTML>
<HEAD>
<SCRIPT>		
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 1 $|$$Date: 2/09/03 19:04 $"
//%insCancel:
//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
	return true;
}
//%insStateZone:
//------------------------------------------------------------------------------------------
function insStateZone(){
//------------------------------------------------------------------------------------------
}
</SCRIPT>
	<META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">
	<%=mobjValues.StyleSheet()%>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></script>


    <%
mobjMenu = New eFunctions.Menues
With Response
            .Write(mobjMenu.MakeMenu("MCA300", "MCA300_K.aspx", 1, ""))
End With
mobjMenu = Nothing
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="MCA300_k" ACTION="ValMantPolicy.aspx?Zone=1">
<BR></BR>
    <TABLE WIDTH="100%">

            <LABEL><%= GetLocalResourceObject("tcdAssign_dateCaption") %></LABEL>
			<%Response.Write(mobjValues.DateControl("tcdAssign_date", , , GetLocalResourceObject("tcdAssign_dateToolTip"), , , , , False))%>

    </TABLE>
</FORM>
</BODY>
</HTML>
<%mobjValues = Nothing%>





