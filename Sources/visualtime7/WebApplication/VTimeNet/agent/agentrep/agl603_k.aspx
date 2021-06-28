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
Call mobjNetFrameWork.BeginPage("agl603_k")

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.11.56
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "agl603_k"
%>
<HTML>
<HEAD>
    <META NAME="GENERATOR" Content="eTransaction Designer for Visual TIME">
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tMenu.js"></SCRIPT>


	<%
Response.Write(mobjValues.StyleSheet())
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.11.56
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility
Response.Write(mobjMenu.MakeMenu("AGL603", "AGL603_k.aspx", 1, Request.QueryString.Item("sWindowDescript"), Session("sDesMultiCompany"), Session("sSche_code")))
mobjMenu = Nothing
%>    
<SCRIPT> 
//+ Variable para el control de versiones
    document.VssVersion="$$Revision: 1 $|$$Date: 2/09/03 19:01 $"

function insCancel(){
//------------------------------------------------------------------------------------------
	return true;
}
//------------------------------------------------------------------------------------------
function insFinish(){
//------------------------------------------------------------------------------------------
	return true;
}

//------------------------------------------------------------------------------------------
function insStateZone(){
}

</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="AGL603" ACTION="ValAgentRep.aspx?sMode=1">
<BR><BR>
    <%Response.Write(mobjValues.ShowWindowsName("AGL603", Request.QueryString.Item("sWindowDescript")))%>
	<TABLE WIDTH="100%"> 
	<BR><BR>
		<TR>
			<TD width="30%"><LABEL ID=0><%= GetLocalResourceObject("cboINTERTYPCaption") %></LABEL></TD>
			<TD><%
With mobjValues
	.Parameters.Add("nInterTyp", 0)
	.List = "1"
	.TypeList = 1 'Incluir
	.BlankPosition = True
	Response.Write(mobjValues.PossiblesValues("cboINTERTYP", "Interm_typ", eFunctions.Values.eValuesType.clngComboType,  , False,  ,  ,  ,  ,  , False,  , GetLocalResourceObject("cboINTERTYPToolTip")))
End With
%>
			</TD>      
		</TR>   
		<TR>
			<TD><LABEL ID=0><%= GetLocalResourceObject("tcdfinicialCaption") %></LABEL></TD>
			<TD><%=mobjValues.DateControl("tcdfinicial", Session("dfinicial"), True, GetLocalResourceObject("tcdfinicialToolTip"),  ,  ,  ,  , False)%></TD></TR>
		</TR>  
		<TR>
			<TD><LABEL ID=0><%= GetLocalResourceObject("tcdffinalCaption") %></LABEL></TD>
			<TD><%=mobjValues.DateControl("tcdffinal", Session("dffinal"), True, GetLocalResourceObject("tcdffinalToolTip"),  ,  ,  ,  , False)%></TD>
		</TR>    
	</TABLE>
<%
mobjValues = Nothing
%>    
</FORM>
</BODY>
</HTML>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 11.11.56
Call mobjNetFrameWork.FinishPage("agl603_k")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




