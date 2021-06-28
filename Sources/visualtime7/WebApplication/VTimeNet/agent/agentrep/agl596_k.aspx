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
Dim dtmCtrol_date As Object

'- Objeto para el manejo de las zonas de la página    
Dim mobjMenu As eFunctions.Menues
Dim mclsGeneral As eGeneral.Ctrol_date


</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("agl596_k")

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.11.56
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "agl596_k"
mclsGeneral = New eGeneral.Ctrol_date

%>



<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tMenu.js"></SCRIPT>
<SCRIPT> 
//+ Variable para el control de versiones
    document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 13.22 $"
    
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
<HTML>
<HEAD>
    <META NAME="GENERATOR" CONTENT="eTransaction Designer for Visual TIME">
<%
Response.Write(mobjValues.StyleSheet())
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.11.56
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility
Response.Write(mobjMenu.MakeMenu("AGL596", "AGL596_k.aspx", 1, Request.QueryString.Item("sWindowDescript"), Session("sDesMultiCompany"), Session("sSche_code")))
mobjMenu = Nothing
%>    
</HEAD>

<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="AGL596" ACTION="ValAgentRep.aspx?sMode=1">
<BR><BR>
        <%=mobjValues.ShowWindowsName("AGL596", Request.QueryString.Item("sWindowDescript"))%>
    	<%Response.Write(mobjValues.WindowsTitle("AGL596", Request.QueryString.Item("sWindowDescript")))%>
	
	<TABLE WIDTH="100%"> 
		<%If mclsGeneral.Find(50) Then
'UPGRADE_NOTE: Date operands have a different behavior in arithmetical operations. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1023.htm
	dtmCtrol_date = System.Date.FromOADate(mclsGeneral.dEffecdate.ToOADate + 1)
End If%>
		<BR>
		<TR>
			<TD WIDTH=30%>&nbsp</TD>
			<TD WIDTH="15%"><LABEL ID=0><%= GetLocalResourceObject("cboINTERTYPCaption") %></LABEL></TD>
			<TD>
				<%
With mobjValues
	.Parameters.Add("nInterTyp", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.List = "1"
	.TypeList = 1 'Incluir
	.BlankPosition = True
	Response.Write(mobjValues.PossiblesValues("cboINTERTYP", "Interm_typ", eFunctions.Values.eValuesType.clngComboType, "", True,  ,  ,  ,  ,  , False,  , GetLocalResourceObject("cboINTERTYPToolTip")))
End With
%>
			<TD WIDTH=30%>&nbsp</TD>	
			</TD>      
	    </TR>   
		<TR>
		    <TD WIDTH=30%>&nbsp</TD>
			<TD><LABEL ID=0><%= GetLocalResourceObject("tcdfinicialCaption") %></LABEL></TD>
			<TD><%=mobjValues.DateControl("tcdfinicial", dtmCtrol_date, True, GetLocalResourceObject("tcdfinicialToolTip"),  ,  ,  ,  , False)%></TD></TR>
			<TD WIDTH=30%>&nbsp</TD>
		</TR>  
	    <TR>
	        <TD WIDTH=30%>&nbsp</TD>
			<TD><LABEL ID=0><%= GetLocalResourceObject("tcdffinalCaption") %></LABEL></TD>
			<TD><%=mobjValues.DateControl("tcdffinal", "", True, GetLocalResourceObject("tcdffinalToolTip"),  ,  ,  ,  , False)%></TD>
			<TD WIDTH=30%>&nbsp</TD>
	    </TR>    
	</TABLE>
<%
mobjValues = Nothing
mclsGeneral = Nothing
%>    
</FORM>
</BODY>
</HTML>

<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 11.11.56
Call mobjNetFrameWork.FinishPage("agl596_k")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




