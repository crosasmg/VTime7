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
//+ Variable para el control de versiones
    document.VssVersion="$$Revision: 2 $|$$Date: 24/05/04 19:33 $"

//-----------------------------------------------------------------------------
function insStateZone(){
//-----------------------------------------------------------------------------

}
//-----------------------------------------------------------------------------
function insPreZone(llngAction){
//-----------------------------------------------------------------------------

}
//-----------------------------------------------------------------------------
function insCancel(){
//-----------------------------------------------------------------------------
   return true
}
</SCRIPT>
<HTML>
<HEAD>
    <META NAME="GENERATOR" CONTENT="eTransaction Designer for Visual TIME">
<%
Response.Write(mobjValues.StyleSheet())
mobjMenu = New eFunctions.Menues
Response.Write(mobjMenu.MakeMenu("AGL728", "AGL728_k.aspx", 1, ""))
mobjMenu = Nothing
%>    
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="AGL728" ACTION="valAgentRep.aspx?sMode=1">
<BR><BR>
<BR>
	<%=mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl"))%>
<BR>
  <TABLE WIDTH="100%">  
          <TR>
		    <TD COLSPAN="2" CLASS="HighLighted"><LABEL ID=0><A NAME="Proceso"><%= GetLocalResourceObject("AnchorProcesoCaption") %></A></LABEL></TD>
		</TR>
		<TR>
		    <TD COLSPAN="2" CLASS="HorLine"></TD>
            <TD></TD> 
		</TR>
		<TR>
            <TD COLSPAN="2"><%=mobjValues.OptionControl(0, "optProcess", GetLocalResourceObject("optProcess_1Caption"), "1", "1")%> </TD>
            <TD>&nbsp;</TD> 
        <TD WIDTH="10%"><LABEL ID=0><%= GetLocalResourceObject("tcdProcess_dateCaption") %></LABEL></TD>
<TD WIDTH="25%"><% %>
<%=mobjValues.DateControl("tcdProcess_date", CStr(Today), True, GetLocalResourceObject("tcdProcess_dateToolTip"))%></TD>        
        </TR>
		<TR>
		    <TD COLSPAN="2"><%=mobjValues.OptionControl(0, "optProcess", GetLocalResourceObject("optProcess_2Caption"),  , "2")%> </TD>
        </TR>  
    <TR>
      <TD WIDTH="25%">
      <TD WIDTH="25%">
    </TR>
  </TABLE>
<%
mobjValues = Nothing
%>    
</FORM>
</BODY>
</HTML>






