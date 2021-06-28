<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eGeneral" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 11.11.56
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo de las zonas de la página    
Dim mobjMenu As eFunctions.Menues

Dim mdtmDate_ini As Object
Dim mlblnDate As Boolean

'- Objeto para el manejo de la fecha Inicial
Dim mobjCtrol_date As eGeneral.Ctrol_date


</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("agl618_k")

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.11.56
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "agl618_k"

mobjCtrol_date = New eGeneral.Ctrol_date

If mobjCtrol_date.Find(52) Then
	mdtmDate_ini = mobjCtrol_date.dEffecdate
	mlblnDate = True
Else
	mdtmDate_ini = Today
	mlblnDate = False
End If
mobjCtrol_date = Nothing


%>



<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>

<SCRIPT> 
//+ Variable para el control de versiones
    document.VssVersion="$$Revision: 2 $|$$Date: 15/09/03 19:00 $"

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
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></SCRIPT>
<HTML>
<HEAD>
    <META NAME="GENERATOR" Content="eTransaction Designer for Visual TIME">	
<%
Response.Write(mobjValues.StyleSheet())
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.11.56
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility
Response.Write(mobjMenu.MakeMenu("AGL618", "AGL618_k.aspx", 1, Request.QueryString.Item("sWindowDescript"), Session("sDesMultiCompany"), Session("sSche_code")))
mobjMenu = Nothing
%>    
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="AGL618" ACTION="ValAgentRep.aspx?sMode=1">
    <BR><BR>
    <TABLE WIDTH="100%">
  	    <BR>
	    <%=mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"))%>
    	<BR>
        <TR>
			<TD></TD>
			<TD WIDTH=30%><LABEL ID=0><%= GetLocalResourceObject("cboIntertypCaption") %></LABEL></TD>
			<TD>
			    <%With mobjValues
	.List = "4,9,11" '"Agente de mantención/Asistente de seguros/Supervisor de Mantención"
	.TypeList = 1 'Incluir
	.BlankPosition = True
	Response.Write(mobjValues.PossiblesValues("cboIntertyp", "Interm_typ", 1, Session("nIntertyp"), False,  ,  ,  ,  ,  , False, 2, GetLocalResourceObject("cboIntertypToolTip"), 1))
End With%>
			</TD>
			<TD></TD>  
		</TR>
		<TR>
			<TD></TD>
			<TD><LABEL ID=0><%= GetLocalResourceObject("tcdInitialDateCaption") %></LABEL></TD>
			<TD><%=mobjValues.DateControl("tcdInitialDate", mdtmDate_ini, True, GetLocalResourceObject("tcdInitialDateToolTip"),  ,  ,  ,  , mlblnDate)%></TD>
			<TD></TD>
		</TR>  
		<TR>
		  <TD></TD>
		  <TD><LABEL ID=0><%= GetLocalResourceObject("tcdFinalDateCaption") %></LABEL></TD>
		  <TD><%=mobjValues.DateControl("tcdFinalDate", "", True, GetLocalResourceObject("tcdFinalDateToolTip"))%></TD>
		  <TD></TD>
		</TR>
	</TABLE>
	<%
mobjValues = Nothing
%>    
</FORM>
</BODY>
</HTML>

<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 11.11.56
Call mobjNetFrameWork.FinishPage("agl618_k")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




