<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo de las zonas de la página    
Dim mobjMenu As eFunctions.Menues


</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
%>



<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tMenu.js"></SCRIPT>
<SCRIPT>

//%insStateZone: habilita los campos de la forma
//-----------------------------------------------------------------------------
function insStateZone(){
//-----------------------------------------------------------------------------
    if (typeof(document.forms[0])!='undefined'){
		document.forms[0].elements["tcdProcessDate"].disabled = false;
		document.images["btn_tcdProcessDate"].disabled = false
	}
}

//%insCancel: Ejecuta rutinas necesarias en el momento de cancelar la página
//-----------------------------------------------------------------------------
function insCancel(){
//-----------------------------------------------------------------------------
   return true
}

//%ProcessDate: Se obtiene la última fecha de ejecución del proceso más 28 días
//-----------------------------------------------------------------------------
function ProcessDate(){
//-----------------------------------------------------------------------------
	insDefValues('LastProcess_date', 'sValue=AGL002','/VTimeNet/Agent/AgentRep');
}

</SCRIPT>
<HTML>
<HEAD>
    <META NAME="GENERATOR" CONTENT="eTransaction Designer for Visual TIME">
<%
Response.Write(mobjValues.StyleSheet())
mobjMenu = New eFunctions.Menues
Response.Write(mobjMenu.MakeMenu("AGL002", "AGL002_k.aspx", 1, vbNullString))
mobjMenu = Nothing
%>
<SCRIPT>
//+ Esta línea guarda la versión procedente de VSS 
    document.VssVersion="$$Revision: 2 $|$$Date: 24/05/04 19:33 $|$$Author: Nvaplat9 $" 
</SCRIPT>     

</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="AGL002" ACTION="valAgentRep.aspx?sMode=1">
    <BR><BR>
		<%=mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl"))%>
	<BR><BR>
    <TABLE WIDTH="100%" BORDER = 0>
    
		 <TR>
		    <TD COLSPAN="1" CLASS="HighLighted"><LABEL ID=0><A NAME="Proceso"><%= GetLocalResourceObject("AnchorProcesoCaption") %></A></LABEL></TD>
		</TR>
		<TR>
		    <TD COLSPAN="1" CLASS="HorLine"></TD>
            <TD></TD> 
		</TR>
		<TR>
            <TD COLSPAN="2"><%=mobjValues.OptionControl(0, "optProcess", GetLocalResourceObject("optProcess_1Caption"), "1", "1")%> </TD>           
            <TD COLSPAN="1">&nbsp;</TD>            
            <TD COLSPAN="1">&nbsp;</TD>                        
            <TD COLSPAN="1"><LABEL ID=0><%= GetLocalResourceObject("tcdProcessDateCaption") %></LABEL></TD>
            <TD><%=mobjValues.DateControl("tcdProcessDate", "", False, GetLocalResourceObject("tcdProcessDateToolTip"),  ,  ,  ,  , True)%></TD> 
		</TR>
		
		<TR>
		    <TD COLSPAN="2"><%=mobjValues.OptionControl(0, "optProcess", GetLocalResourceObject("optProcess_2Caption"),  , "2")%> </TD>
            <TD COLSPAN="1">&nbsp;</TD>                        
            <TD COLSPAN="1">&nbsp;</TD>            			
			<TD COLSPAN="1" ><LABEL ID=0><%= GetLocalResourceObject("cbeInsur_AreaCaption") %></LABEL></TD>
			<TD><%=mobjValues.PossiblesValues("cbeInsur_Area", "Table5001", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeInsur_AreaToolTip"),  , 2)%> </TD>
        </TR>         
        <TR>
            <TD COLSPAN="4">&nbsp;</TD>
			<TD COLSPAN="1"><LABEL ID=0><%= GetLocalResourceObject("AnchorCaption") %></LABEL></TD>
			<TD><%=mobjValues.CheckControl("chkLoansDelay", "", CStr(False),  ,  ,  ,  , GetLocalResourceObject("chkLoansDelayToolTip"))%> </TD>        
        </TR>
		<TR>

		</TR>		
		<TR>
		</TR>		
    </TABLE>
    <SCRIPT>if(insDisabledButton(document.A304))ClientRequest(304,5);
            ProcessDate();
    </SCRIPT>
</FORM>
</BODY>
</HTML>




