<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eGeneral" %>
<script language="VB" runat="Server">

Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues
Dim mobjOptionInstall As eGeneral.OptionsInstallation


'**********************************************************************************************************
'% insPreMAG978 : define la estructura de la página "pintando" los campos puntuales 
'--------------------------------------------------------------------------------------------------
Private Function insPreMAG978() As Object
	'--------------------------------------------------------------------------------------------------
	
Response.Write("" & vbCrLf)
Response.Write("    <TABLE WIDTH=""100%"">" & vbCrLf)
Response.Write("	    <TR>" & vbCrLf)
Response.Write("			<TD COLSPAN=""6"" CLASS=""HIGHLIGHTED""><LABEL ID=0>" & GetLocalResourceObject("AnchorCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("	    </TR>" & vbCrLf)
Response.Write("	    <TR>" & vbCrLf)
Response.Write("			<TD COLSPAN=""6"" CLASS=""HORLINE""></TD>" & vbCrLf)
Response.Write("	    </TR>" & vbCrLf)
Response.Write("" & vbCrLf)
Response.Write("	    <TR>" & vbCrLf)
Response.Write("	       <TD width=20%><LABEL ID=0></LABEL></TD>" & vbCrLf)
Response.Write("	    	<TD width=20%><LABEL ID=0>" & GetLocalResourceObject("tcnQM_MinDuratCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("	    	<TD width=20%>")


Response.Write(mobjValues.NumericControl("tcnQM_MinDurat", 18, CStr(mobjOptionInstall.nQM_MinDurat),  , GetLocalResourceObject("tcnQM_MinDuratToolTip"), True, 0,  ,  ,  ,  , CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401))


Response.Write("</TD>" & vbCrLf)
Response.Write("	    	<TD colspan =2>" & vbCrLf)
Response.Write("	    </TR>" & vbCrLf)
Response.Write("	    <TR>" & vbCrLf)
Response.Write("	    </TR>" & vbCrLf)
Response.Write("	    <TR>" & vbCrLf)
Response.Write("	       <TD><LABEL ID=0></LABEL></TD>" & vbCrLf)
Response.Write("	    	<TD><LABEL ID=0>" & GetLocalResourceObject("tcnMonth_ExpiryCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("	    	<TD>")


Response.Write(mobjValues.NumericControl("tcnMonth_Expiry", 18, CStr(mobjOptionInstall.nMonth_Expiry),  , GetLocalResourceObject("tcnMonth_ExpiryToolTip"), True, 0,  ,  ,  ,  , CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401))


Response.Write("</TD>" & vbCrLf)
Response.Write("	    	<TD colspan =2>" & vbCrLf)
Response.Write("	    </TR>" & vbCrLf)
Response.Write("	    <TR>" & vbCrLf)
Response.Write("	       <TD><LABEL ID=0></LABEL></TD>" & vbCrLf)
Response.Write("	    	<TD><LABEL ID=0>" & GetLocalResourceObject("tcnMonth_PunishCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("	    	<TD >")


Response.Write(mobjValues.NumericControl("tcnMonth_Punish", 18, CStr(mobjOptionInstall.nMonth_Punish),  , GetLocalResourceObject("tcnMonth_PunishToolTip"), True, 0,  ,  ,  ,  , CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401))


Response.Write("</TD>" & vbCrLf)
Response.Write("	    	<TD colspan =2>" & vbCrLf)
Response.Write("	    </TR>" & vbCrLf)
Response.Write("	</TABLE>")

End Function

</script>
<%Response.Expires = -1

'+ Se instancian los objetos necesarios para trabajr las particularidades de creación de la forma por rutinas genéricas

mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues
mobjOptionInstall = New eGeneral.OptionsInstallation

mobjValues.ActionQuery = CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401
mobjValues.sCodisplPage = "MAG978"
%> 
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<%="<SCRIPT LANGUAGE=""JavaScript"">"%>
var nMainAction = <%=Request.QueryString.Item("nMainAction")%>;
</SCRIPT>
<HTML> 
<HEAD>
	<META NAME = "GENERATOR" Content="Microsoft Visual Studio 6.0">
<SCRIPT LANGUAGE = "JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></SCRIPT>
<SCRIPT LANGUAGE = "JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
	<SCRIPT>
//- Variable para el control de versiones
	     document.VssVersion="$$Revision: 3 $|$$Date: 19/04/04 16:14 $|$$Author: Nvaplat40 $"
	</SCRIPT>


	<%=mobjValues.StyleSheet()%>
<TITLE>Generalidades de las opciones de instalación</TITLE>
</HEAD>
	
<BODY ONUNLOAD="closeWindows();">
<%
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write(mobjMenu.setZone(2, "MAG978", "MAG978.aspx"))
End If
mobjMenu = Nothing
Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl")))
%>
<FORM METHOD="POST" ACTION="valMantGeneral.aspx?nMainAction=<%=Request.QueryString.Item("nMainAction")%>" id=form1 name=form1>
<%

'+ Se realiza la lectura de los valores cargados en la tabla de la opciones de instalación cobranza
mobjOptionInstall.FindOptIntermed()
Call insPreMAG978()
%>
</BODY>
</HTML>






