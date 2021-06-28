<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 10.47.59
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility
'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues


</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("col742_k")

With Server
	mobjValues = New eFunctions.Values
	'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.47.59
	mobjValues.sSessionID = Session.SessionID
	mobjValues.nUsercode = Session("nUsercode")
	'~End Body Block VisualTimer Utility
	
	mobjValues.sCodisplPage = "col742_k"
	mobjMenu = New eFunctions.Menues
	'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.47.59
	mobjMenu.sSessionID = Session.SessionID
	mobjMenu.nUsercode = Session("nUsercode")
	'~End Body Block VisualTimer Utility
End With
%>
<HTML>
<HEAD>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tMenu.js"></SCRIPT>
	<SCRIPT>
//+ Variable para el control de versiones
	     document.VssVersion="$$Revision: 1 $|$$Date: 2/09/03 19:01 $|$$Author: Iusr_llanquihue $"
    </SCRIPT>




    <META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
<SCRIPT>
//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
    return true;
}
//------------------------------------------------------------------------------------------
function  insStateZone(){
//------------------------------------------------------------------------------------------
}
</SCRIPT>
<%
With Response
	.Write(mobjValues.StyleSheet() & vbCrLf)
	.Write(mobjValues.WindowsTitle("COL742", Request.QueryString.Item("sWindowDescript")))
	.Write(mobjMenu.MakeMenu("COL742", "COL742_K.aspx", 1, Request.QueryString.Item("sWindowDescript"), Session("sDesMultiCompany"), Session("sSche_code")))
	.Write(mobjMenu.setZone(1, Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), CShort(Request.QueryString.Item("nWindowTy"))))
End With
mobjMenu = Nothing
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="frmModCollect" ACTION="valCollectionRep.aspx?mode=1">
<BR><BR><BR>
    <%Response.Write(mobjValues.ShowWindowsName("COL742", Request.QueryString.Item("sWindowDescript")))%>
    <TABLE WIDTH="100%">
    <BR>
        <TR><TD COLSPAN="4">&nbsp;</TD></TR>
        <TR>
            <TD WIDTH="50">&nbsp;</TD>
            <TD WIDTH="200"><LABEL ID="10295"><%= GetLocalResourceObject("cbeInsurAreaCaption") %></LABEL></TD>
            <%mobjValues.BlankPosition = False%>      			
            <TD><%=mobjValues.PossiblesValues("cbeInsurArea", "table5001", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  ,  ,  , "")%></TD>                
            <TD>&nbsp;</TD>
		</TR>		
		<TR><TD COLSPAN="4">&nbsp;</TD></TR>
		<TR>
		     <TD WIDTH="50">&nbsp;</TD>
		    <TD><LABEL ID="10528"><%= GetLocalResourceObject("tcdCollectDateCaption") %></LABEL></TD>		    
<TD><% %>
<%=mobjValues.DateControl("tcdCollectDate", CStr(Today),  , GetLocalResourceObject("tcdCollectDateToolTip"),  ,  ,  ,  , False)%></TD>
		    <TD>&nbsp;</TD> 
		</TR>
		<TR><TD COLSPAN="4">&nbsp;</TD></TR>
		<TR>
		    <TD WIDTH="50">&nbsp;</TD>
		    <TD><LABEL ID="10528"><%= GetLocalResourceObject("valAgreementCaption") %></LABEL></TD>		    
		    <TD><%=mobjValues.PossiblesValues("valAgreement", "tabAgreement_sClient", eFunctions.Values.eValuesType.clngWindowType, CStr(eRemoteDB.Constants.intNull),  ,  ,  ,  ,  ,  , False, 5, GetLocalResourceObject("valAgreementToolTip"))%></TD>		    
		    <TD>&nbsp;</TD> 
		</TR>
    </TABLE>    
</FORM>
</BODY>
</HTML>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 10.47.59
Call mobjNetFrameWork.FinishPage("col742_k")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




