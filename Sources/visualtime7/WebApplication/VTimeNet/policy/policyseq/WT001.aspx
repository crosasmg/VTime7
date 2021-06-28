<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="ePolicy" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 31/3/03 19.44.14
Dim mobjNetFrameWork As eNetFrameWork.Layout
'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues
'- Objeto para el manejo de la tabla Life
Dim mclsCertificat As ePolicy.Warranty


'% insPreWT001: Realiza la lectura de los campos a mostrar en pantalla
'---------------------------------------------------------------------
Private Sub insPreWT001()
	'---------------------------------------------------------------------
	Dim mclsRoleses As Object
	Dim lintAction As Short
	
	lintAction = 1
	If Session("bQuery") Then
		lintAction = 0
	End If
	
	With mobjValues
		Call mclsCertificat.Find_WT001(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), Session("dEffecdate"), lintAction)
		
	End With
End Sub

</script>
<%Response.Expires = -1
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("WT001")
'~End Header Block VisualTimer Utility
Response.CacheControl = "private"
mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.44.14
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = Request.QueryString.Item("sCodispl")
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.44.14
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility
mclsCertificat = New ePolicy.Warranty
mobjValues.ActionQuery = Session("bQuery")
Call insPreWT001()
%>
<SCRIPT>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 6 $|$$Date: 3/12/09 7:15 $|$$Author: Cidler $"
</SCRIPT>


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">
    <LINK REL="StyleSheet" TYPE="text/css" HREF="/VTimeNet/Common/Custom.css">
    <%Response.Write(mobjMenu.setZone(2, Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), CShort(Request.QueryString.Item("nWindowTy"))))
mobjMenu = Nothing%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="frmWT001" ACTION="valPolicySeq.aspx?nMainAction=301&nHolder=1">
	<%=mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"))%>
    <TABLE WIDTH="100%">
        <TR>
			<TD><LABEL COLSPAN="2" ID=13518><%= GetLocalResourceObject("tctProjectNameCaption") %></LABEL></TD>
			<TD COLSPAN="2" ><%=mobjValues.TextControl("tctProjectName", 60, mclsCertificat.sProjectName,  , GetLocalResourceObject("tctProjectNameToolTip"),  ,  ,  , CStr(Session("nCertif") <> 0))%></TD>
		</TR>
        <TR>
			<TD><LABEL COLSPAN="2" ID=13518><%= GetLocalResourceObject("tctIndentifyCaption") %></LABEL></TD>
			<TD COLSPAN="2" ><%
If Session("nCertif") = 0 And CStr(Session("sPolitype")) = "2" Then
	Response.Write(mobjValues.TextAreaControl("tctIndentify", 4, 50, mclsCertificat.sIdentify,  , GetLocalResourceObject("tctIndentifyToolTip"),  , True))
Else
	Response.Write(mobjValues.TextAreaControl("tctIndentify", 4, 50, mclsCertificat.sIdentify,  , GetLocalResourceObject("tctIndentifyToolTip"),  , False))
End If
%></TD>
		</TR>
    </TABLE>
<%
Response.Write(mobjValues.BeginPageButton)
mobjValues = Nothing
mobjMenu = Nothing
mclsCertificat = Nothing
%>
</FORM>
</BODY>
</HTML>
<%'^Begin Footer Block VisualTimer Utility 1.1 31/3/03 19.44.14
Call mobjNetFrameWork.FinishPage("WT001")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




