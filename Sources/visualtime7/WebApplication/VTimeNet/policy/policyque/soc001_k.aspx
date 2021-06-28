<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores

Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues


</script>
<%Response.Expires = 0

mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues
%>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></script>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/ValFunctions.js"></script>
<HEAD>
    <%With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjMenu.MakeMenu("SOC001", "SOC001_k.aspx", 1, ""))
End With

mobjMenu = Nothing%>
<SCRIPT>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 1 $|$$Date: 2/09/03 19:03 $$Author: Iusr_llanquihue $"
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
}
</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="frmSOC001" ACTION="valPolicyQue.aspx?mode=1">
<BR></BR>
    <TABLE WIDTH="100%">
	<TR>        
			<TD WIDTH=25%> </TD>
			<TD WIDTH=25%>
				<LABEL ID=0><%= GetLocalResourceObject("tcnYearCaption") %></LABEL>
			</TD>
			<TD WIDTH=25%>
				<%=mobjValues.NumericControl("tcnYear",  4,  , True, GetLocalResourceObject("tcnYearToolTip"),  ,  ,  ,  ,  ,  ,  True)%>
			</TD>
			<TD WIDTH=25%> </TD>
        </TR>
    </TABLE>
</FORM>
</BODY>
</HTML>
<%   
    mobjValues = Nothing
%>
