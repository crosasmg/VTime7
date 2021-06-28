<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eGeneralForm" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 11.35.42
Dim mobjNetFrameWork As eNetFrameWork.Layout

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo de la ventana
Dim mclsCancelProcess As eGeneralForm.CancelProcess


</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("GE101")
mobjValues = New eFunctions.Values
mclsCancelProcess = New eGeneralForm.CancelProcess

mobjValues.sCodisplPage = "GE101"
%>
<HTML>
<HEAD>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">
    <%With Response
	.Write(mobjValues.WindowsTitle("GE101", Request.QueryString.Item("sWindowDescript")))
	.Write(mobjValues.StyleSheet())
End With
%>
<SCRIPT>
//- Variable para el control de versiones
	document.VssVersion="$$Revision: 1 $|$$Date: 2/09/03 19:02 $|$$Author: Iusr_llanquihue $"
</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="GE101" ACTION="valPage.aspx?sCodispl=GE101">
<%
With Response
	.Write(mobjValues.ShowWindowsName("GE101", Request.QueryString.Item("sWindowDescript")))
	.Write("<BR>")
	.Write(mclsCancelProcess.makeTable(Request.QueryString.Item("sCodispl")))
End With
mobjValues = Nothing
mclsCancelProcess = Nothing
%>
</FORM>
</BODY>
</HTML>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 11.35.42
Call mobjNetFrameWork.FinishPage("GE101")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




