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
            .Write(mobjMenu.MakeMenu("SO002", "SO002_K.aspx", 1, ""))
End With
mobjMenu = Nothing
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="frmReahPolicy_K" ACTION="ValPolicyTra.aspx?Zone=1">
<BR></BR>
    <TABLE WIDTH="100%">
        <TR>
            <TD><LABEL ID=0>Intermediario origen</LABEL></TD>
			<TD colspan =4><%Response.Write(mobjValues.PossiblesValues("valIntermedSource", "tabintermedia_o", eFunctions.Values.eValuesType.clngWindowType, , , , , , , , False, 10, GetLocalResourceObject("valIntermedToolTip")))%></TD>
         </TR>
        <TR>
			<TD><LABEL ID=LABEL2>Folio inicial</LABEL></TD>
			<TD><%= mobjValues.NumericControl("tcnFolioI", 10, CStr(0), , GetLocalResourceObject("tcnFolioToolTip"), , , , , , "")%></TD>
            <TD>&nbsp;</TD>
			<TD><LABEL ID=LABEL4>Folio final</LABEL></TD>
			<TD><%= mobjValues.NumericControl("tcnFolioE", 10, CStr(0), , GetLocalResourceObject("tcnFolioToolTip"), , , , , , "")%></TD>
        </TR>
        <TR>
            <TD><LABEL ID=LABEL1>Intermediario destino</LABEL></TD>
			<TD  colspan =4><%Response.Write(mobjValues.PossiblesValues("valIntermedDest", "tabintermedia_o", eFunctions.Values.eValuesType.clngWindowType, , , , , , , , False, 10, GetLocalResourceObject("valIntermedToolTip")))%></TD>
        </TR>
    </TABLE>
</FORM>
</BODY>
</HTML>
<%mobjValues = Nothing%>





