<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores

Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues


</script>
<%
Response.Expires = -1

mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues

mobjValues.sCodisplPage = "CPC001_K"
%>

<HTML>
<HEAD>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>	
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tMenu.js"></SCRIPT>


<%
With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjMenu.MakeMenu("CPC001", "CPC001_K.aspx", 1, ""))
End With
mobjMenu = Nothing
%>

<SCRIPT>
//% insStateZone: 
//-----------------------
function insStateZone(){
//-----------------------
	self.document.forms[0].btn_tcdEndDate.disabled = false;
	with(self.document.forms[0]){
		cbeBalDate.disabled = false;
		cbeLevels.disabled = false;
		chkIndent.disabled = false;
		tcdEndDate.disabled = false;
		LedCompan.disabled = false;
	}
}

//% insCancel: Ejecuta las rutinas necesarias para la cancelación de la transacción
//---------------------------------------------------------------------------------
function insCancel(){return true}
//---------------------------------------------------------------------------------

//+ Esta línea guarda la versión procedente de VSS 
    document.VssVersion="$$Revision: 1 $|$$Date: 2/09/03 19:02 $" 
</SCRIPT>
</HEAD>

<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="CPC001_K" ACTION="valLedgerQue.aspx?sTime=1">
	<BR>
   	<%=mobjValues.ButtonLedCompan("LedCompan", Session("nLedCompan"), GetLocalResourceObject("LedCompanToolTip"), True)%>
    <TABLE WIDTH="100%">
        <TR>
            <TD><LABEL ID=11276><%= GetLocalResourceObject("cbeBalDateCaption") %></LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("cbeBalDate", "table7006", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeBalDateToolTip"),  , 1)%></TD>
            <TD><LABEL ID=11275><%= GetLocalResourceObject("cbeLevelsCaption") %></LABEL></TD>
            <%mobjValues.TypeOrder = CShort("1")%>
            <TD><%=mobjValues.PossiblesValues("cbeLevels", "table7007", eFunctions.Values.eValuesType.clngComboType, CStr(7),  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeLevelsToolTip"),  , 2)%></TD>
        </TR>
        <TR>
            <TD><LABEL ID=11274><%= GetLocalResourceObject("tcdEndDateCaption") %></LABEL></TD>
            <TD><%=mobjValues.DateControl("tcdEndDate", "",  , GetLocalResourceObject("tcdEndDateToolTip"),  ,  ,  ,  , True, 3)%></TD>
            <TD><%=mobjValues.CheckControl("chkIndent", GetLocalResourceObject("chkIndentCaption"),  ,  ,  , True,  , GetLocalResourceObject("chkIndentToolTip"))%></TD>
        </TR>
    </TABLE>
</FORM>
</BODY>
</HTML>

<%
mobjValues = Nothing
%>




