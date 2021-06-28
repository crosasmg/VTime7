<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues


</script>
<%Response.Expires = 0
mobjValues = New eFunctions.Values

%>
<HTML>
<HEAD>
    <%=mobjValues.StyleSheet()%>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></SCRIPT>
<SCRIPT> 
//+ Esta línea guarda la versión procedente de VSS 
    document.VssVersion="$$Revision: 1 $|$$Date: 2/09/03 19:01 $" 
   
//% insStateZone: se manejan los campos de la página
//-------------------------------------------------------------------------------------------
function insStateZone(){
//-------------------------------------------------------------------------------------------
}
    </SCRIPT>
    <%
Session("ButtomAdd") = True
mobjMenu = New eFunctions.Menues
Response.Write(mobjMenu.MakeMenu("BC005", "BC005_k.aspx", 1, ""))
mobjMenu = Nothing
%>
<SCRIPT>

//% insCancel: se ejecuta la acción Cancelar de la página
//-------------------------------------------------------------------------------------------
function insCancel(){
//-------------------------------------------------------------------------------------------
	return(true);
}

//% insStateZone1
//-------------------------------------------------------------------------------------------
function insStateZone1(blnEnabled){
//-------------------------------------------------------------------------------------------
    if (typeof(blnEnabled)=='undefined'){
        top.fraSequence.insLetCurrZone(1)
        blnEnabled = true
    }
    if ((blnEnabled) &&
       (top.fraSequence.insGetCurrZone() == 1)){
	    self.document.forms[0].elements["dtcClient"].disabled = false
	}
}
</SCRIPT>
</HEAD>
<BODY  ONUNLOAD="closeWindows();">
<BR>
<FORM METHOD="POST" ACTION="valclient.aspx?time=1?nZone=1">
	<TABLE WIDTH=100%>
		<TR>
            <TD COLSPAN="1" CLASS="HighLighted"><LABEL ID=41078><A NAME="Acción"><%= GetLocalResourceObject("AnchorAcciónCaption") %></A></LABEL></TD> 
        </TR>
        <TR>
		    <TD COLSPAN="1" CLASS="Horline"></TD>		  
        </TR>        	
        <TR>
            <TD><%=mobjValues.OptionControl(0, "optAct", GetLocalResourceObject("optAct_1Caption"), "1", "1")%></TD>
        </TR>		
        <TR>
            <TD><%=mobjValues.OptionControl(0, "optAct", GetLocalResourceObject("optAct_2Caption"),  , "2")%></TD>
            <TD>&nbsp;</TD>
            <TD WIDTH=10%><LABEL ID=9689><%= GetLocalResourceObject("dtcClientCaption") %></LABEL></td>
            <TD><%=mobjValues.ClientControl("dtcClient", vbNullString, True, GetLocalResourceObject("dtcClientToolTip"),  , False, "lblCliename")%></TD>
            <TD><%=mobjValues.TextControl("lblCliename", 30, "",  ,  , True,  ,  ,  , True)%></TD>
        </TR>
	</TABLE>
</FORM>
</BODY>
</HTML>
<SCRIPT>insStateZone1(false)</SCRIPT>
<%
mobjValues = Nothing%>





