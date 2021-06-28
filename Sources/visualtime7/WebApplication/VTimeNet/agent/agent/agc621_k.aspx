<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 11.11.56
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo de las zonas de la página    
Dim mobjMenu As eFunctions.Menues


</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("agc621_k")

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.11.56
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "agc621_k"
%>
<HTML>
<HEAD>
    <META NAME="GENERATOR" Content="eTransaction Designer for Visual TIME">
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tMenu.js"></SCRIPT>


<SCRIPT> 
//+ Variable para el control de versiones
    document.VssVersion="$$Revision: 1 $|$$Date: 2/09/03 19:01 $|$$Author: Iusr_llanquihue $"

//-----------------------------------------------------------------------------
function insStateZone()
//-----------------------------------------------------------------------------
{
	with(self.document.forms[0])
	{
		valIntermed.disabled = false;
		tcdEffecdateIni.disabled = false;
		tcdEffecdateEnd.disabled = false;
		tcnPay_comm.disabled = false;
		btnvalIntermed.disabled = false;
		btn_tcdEffecdateIni.disabled = false;
		btn_tcdEffecdateEnd.disabled = false;
	}
}

//-----------------------------------------------------------------------------
function insPreZone(llngAction){
//-----------------------------------------------------------------------------
}

//-----------------------------------------------------------------------------
function insCancel(){
//-----------------------------------------------------------------------------
	return true;
}
</SCRIPT>
<%
Response.Write(mobjValues.StyleSheet())
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.11.56
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility
Response.Write(mobjMenu.MakeMenu("AGC621", "AGC621_k.aspx", 1, Request.QueryString.Item("sWindowDescript"), Session("sDesMultiCompany"), Session("sSche_code")))
mobjMenu = Nothing
%> 
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="AGC621" ACTION="ValAgent.aspx?sMode=1">
	<BR><BR>
	<TABLE WIDTH="100%">
		<TR>
			<TD><LABEL ID=0><%= GetLocalResourceObject("valIntermedCaption") %></LABEL></TD>
			<%mobjValues.Parameters.Add("nUserCode", Session("nUserCode"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)%>
					<TD COLSPAN="2" ><%=mobjValues.PossiblesValues("valIntermed", "tabIntermedia3", eFunctions.Values.eValuesType.clngWindowType,  , True,  ,  ,  ,  ,  , True, 10, GetLocalResourceObject("valIntermedToolTip"),  , 9)%></TD>		
	
		</TR>
		<TR>
			<TD><LABEL ID=0><%= GetLocalResourceObject("tcdEffecdateIniCaption") %></LABEL></TD>
			<TD><%=mobjValues.DateControl("tcdEffecdateIni", "",  , GetLocalResourceObject("tcdEffecdateIniToolTip"),  ,  ,  ,  , True)%></TD>
			<TD><LABEL ID=0><%= GetLocalResourceObject("tcdEffecdateEndCaption") %></LABEL></TD>
			<TD><%=mobjValues.DateControl("tcdEffecdateEnd", "",  , GetLocalResourceObject("tcdEffecdateEndToolTip"),  ,  ,  ,  , True)%></TD>
		</TR>
		<TR>
			<TD><LABEL ID=0><%= GetLocalResourceObject("tcnPay_commCaption") %></LABEL></TD>
			<TD><%=mobjValues.NumericControl("tcnPay_comm", 10, "",  , GetLocalResourceObject("tcnPay_commToolTip"),  ,  ,  ,  ,  ,  , True)%></TD>
		</TR>
	</TABLE>
</FORM>
</BODY>
</HTML>
<%
mobjValues = Nothing
%>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 11.11.56
Call mobjNetFrameWork.FinishPage("agc621_k")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




