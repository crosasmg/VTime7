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
    document.VssVersion="$$Revision: 1 $|$$Date: 30/06/04 18:54 $"

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
Response.Write(mobjMenu.MakeMenu("AGL918", "AGL918_k.aspx", 1, ""))
mobjMenu = Nothing
%>    
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="AGL918" ACTION="valAgentRep.aspx?sMode=1">
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
            <TD><LABEL ID=0><%= GetLocalResourceObject("tcddatefromCaption") %></LABEL></TD>
<TD><% %>
<%=mobjValues.DateControl("tcddatefrom", CStr(Today), True, GetLocalResourceObject("tcddatefromToolTip"))%></TD>        
		<TR>
           <TD><LABEL ID=0><%= GetLocalResourceObject("tcddatetoCaption") %></LABEL></TD>
<TD><% %>
<%=mobjValues.DateControl("tcddateto", CStr(Today), True, GetLocalResourceObject("tcddatetoToolTip"))%></TD>        
		</TR>
        <TR>
		    <TD COLSPAN="2" CLASS="HighLighted"><LABEL ID=0><A NAME="Proceso"><%= GetLocalResourceObject("AnchorProceso2Caption") %></A></LABEL></TD>
		</TR>
		<TR>
		    <TD COLSPAN="2" CLASS="HorLine"></TD>
            <TD></TD> 
		</TR>
		<TR>
            <TD COLSPAN="2"><%=mobjValues.OptionControl(0, "optReport", GetLocalResourceObject("optReport_1Caption"), "1", "1")%> </TD>
        </TR>
		<TR>
		    <TD COLSPAN="2"><%=mobjValues.OptionControl(0, "optReport", GetLocalResourceObject("optReport_2Caption"),  , "2")%> </TD>
        </TR>  
    <TR>
      <TD WIDTH="15%">
      <TD WIDTH="15%">
    </TR>
  </TABLE>
<%
mobjValues = Nothing
%>    
</FORM>
</BODY>
</HTML>






