<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eRemoteDB" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 11.11.56
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo de consultas
Dim mobjQuery As eRemoteDB.Query

'- Variables para el el manejo de los datos
Dim ldtmCtrol_date As Object

'- Objeto para el manejo de las zonas de la página    
Dim mobjMenu As eFunctions.Menues


</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("agl583_k")

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.11.56
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "agl583_k"
mobjQuery = New eRemoteDB.Query

If mobjQuery.OpenQuery("ctrol_date", "dEffecdate", "nType_Proce=51") Then
	'ldtmCtrol_date = mobjValues.StringToType(mobjQuery.FieldToClass("dEffecdate") + 1, eFunctions.Values.eTypeData.etdDate)
    ldtmCtrol_date = mobjValues.StringToType(mobjQuery.FieldToClass("dEffecdate").AddDays(1), eFunctions.Values.eTypeData.etdDate)
Else
	ldtmCtrol_date = Today
End If
mobjQuery = Nothing

%>
<HTML>
<HEAD>
    <META NAME="GENERATOR" CONTENT="eTransaction Designer for Visual TIME">
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tMenu.js"></SCRIPT>


<%
With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjValues.WindowsTitle("AGL583", Request.QueryString.Item("sWindowDescript")))
	mobjMenu = New eFunctions.Menues
	'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.11.56
	mobjMenu.sSessionID = Session.SessionID
	mobjMenu.nUsercode = Session("nUsercode")
	'~End Body Block VisualTimer Utility
	.Write(mobjMenu.MakeMenu("AGL583", "AGL583_k.aspx", 1, Request.QueryString.Item("sWindowDescript"), Session("sDesMultiCompany"), Session("sSche_code")))
End With
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
<FORM METHOD="POST" ID="FORM" NAME="AGL583" ACTION="ValAgentRep.aspx?sMode=1">
	<BR><BR>
    	<%Response.Write(mobjValues.ShowWindowsName("AGL583", Request.QueryString.Item("sWindowDescript")))%>
	<TABLE WIDTH="100%"> 
		<BR>
		<TR>
			<TD WIDTH="30%"><LABEL ID=0><%= GetLocalResourceObject("cbeInterTypCaption") %></LABEL></TD>
			<TD>
				<%
With mobjValues
	.TypeList = 1
	.List = "5,11,50,51,52,60,61,62"
	Response.Write(.PossiblesValues("cbeInterTyp", "tabinter_typ", eFunctions.Values.eValuesType.clngComboType, CStr(5),  ,  ,  ,  ,  ,  , False,  , GetLocalResourceObject("cbeInterTypToolTip")))
End With
%>
			</TD>      
		</TR>   
		<TR>
			<TD><LABEL ID=0><%= GetLocalResourceObject("tcdStartDateCaption") %></LABEL></TD>
			<TD><%=mobjValues.DateControl("tcdStartDate", ldtmCtrol_date, True, GetLocalResourceObject("tcdStartDateToolTip"),  ,  ,  ,  , False)%></TD></TR>
		</TR>  
		<TR>
			<TD><LABEL ID=0><%= GetLocalResourceObject("tcdEndDateCaption") %></LABEL></TD>
			<TD><%=mobjValues.DateControl("tcdEndDate", "", True, GetLocalResourceObject("tcdEndDateToolTip"),  ,  ,  ,  , False)%></TD>
		</TR>
	</TABLE>
<%
mobjValues = Nothing
%>  
</FORM>
</BODY>
</HTML>

<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 11.11.56
Call mobjNetFrameWork.FinishPage("agl583_k")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




