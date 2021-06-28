<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values


</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjValues.sCodisplPage = "MAG006"
%>

<HTML>
<HEAD>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>


 	<%With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjValues.ShowWindowsName("MAG006"))
	.Write(mobjValues.WindowsTitle("MAG006"))
End With
%>
	
<SCRIPT>
//% insCloseWindows: Permite cerrar la ventana PopUp invocada. Este evento es llamado desde el botón 
//% ButtonAcceptCancel.
//------------------------------------------------------------------------------------------------
function insCloseWindows(){
//------------------------------------------------------------------------------------------------
	window.close()
}
</SCRIPT>    
</HEAD>

<FORM METHOD="POST" ID="FORM" NAME="frmTabCommission" ACTION="valMantAgent.aspx?sCodispl=MAG006&mode=1&WindowType=PopUp">
<TABLE WIDTH="100%">
		<TR>
            <TD><LABEL><A><%= GetLocalResourceObject("nTable_codCaption") %></A></LABEL></TD>
            <TD><%=mobjValues.NumericControl("nTable_cod", 5, CStr(eRemoteDB.Constants.intNull),  , GetLocalResourceObject("nTable_codToolTip"),  ,  ,  ,  ,  ,  , False)%></TD>
 		</TR>           
		<TR>
            <TD><LABEL><A><%= GetLocalResourceObject("tctDescCaption") %></A></LABEL></TD>
            <TD><%=mobjValues.TextControl("tctDesc", 30, Request.QueryString.Item("tctDesc"),  , GetLocalResourceObject("tctDescToolTip"),  ,  ,  ,  , False)%></TD>
 		</TR>            
		<TR>		
            <TD> <LABEL><A NAME="Fecha"><%= GetLocalResourceObject("tcdEffecdateCaption") %></A></LABEL></TD>
<TD><% %>
<%=mobjValues.DateControl("tcdEffecdate", CStr(Today), True, GetLocalResourceObject("tcdEffecdateToolTip"),  ,  ,  ,  , False)%></TD>
         </TR>
    </TABLE>
    <P ALIGN=RIGHT> 
             <%=mobjValues.ButtonAcceptCancel( , "insCloseWindows()", True,  , eFunctions.Values.eButtonsToShow.All)%> </P>
             <%=mobjValues.HiddenControl("hddAction", Request.QueryString.Item("nAction"))%>
             <%=mobjValues.HiddenControl("hddnTableDup", Request.QueryString.Item("nTabdup"))%>    
<%
mobjValues = Nothing
%>
</FORM>
</BODY>
</HTML>






