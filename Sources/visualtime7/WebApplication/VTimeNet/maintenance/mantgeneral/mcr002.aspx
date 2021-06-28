<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eGeneral" %>
<script language="VB" runat="Server">

Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues
Dim mobjOptionInstall As eGeneral.OptionsInstallation



'**********************************************************************************************************
'*************************************** FUNCTIONS VBScript ***********************************************
'*************************************** FUNCIONES VBScript ***********************************************
'**********************************************************************************************************

'**% insDefineFields : defines the structure of the page "painting" the precise fields and the grid
'%   insDefineFields : define la estructura de la página "pintando" los campos puntuales y el grid
'--------------------------------------------------------------------------------------------------
Private Function insPreMCR002() As Object
	'--------------------------------------------------------------------------------------------------
	Dim lintMarked As Object
	
	If mobjOptionInstall.nCoaCessCoRe = 1 Then
		lintMarked = 2
	Else
		lintMarked = 1
	End If
	
Response.Write("" & vbCrLf)
Response.Write("    <TABLE WIDTH=""100%"">" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD WIDTH = 25% CLASS = ""HIGHLIGHTED""><LABEL ID=0>" & GetLocalResourceObject("AnchorCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD WIDTH = 5%>&nbsp;</TD>" & vbCrLf)
Response.Write("            <TD CLASS = ""HIGHLIGHTED""><LABEL ID=0>" & GetLocalResourceObject("Anchor2Caption") & "</LABEL></TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD CLASS = ""HORLINE""></TD>" & vbCrLf)
Response.Write("            <TD></TD>" & vbCrLf)
Response.Write("            <TD CLASS = ""HORLINE""></TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD>")

	Response.Write(mobjValues.OptionControl(0, "optNetPremium", GetLocalResourceObject("optNetPremium_CStr1Caption"), CStr(mobjOptionInstall.nCoaCessCoRe), CStr(1),  , CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401))
Response.Write("</TD>" & vbCrLf)
Response.Write("            <TD>&nbsp;</TD>" & vbCrLf)
Response.Write("            <TD>")

	Response.Write(mobjValues.CheckControl("chkCesPreCoa", GetLocalResourceObject("chkCesPreCoaCaption"), mobjOptionInstall.sCoinsuriCoRe, CStr(1),  , CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401))
Response.Write("</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD>")

	Response.Write(mobjValues.OptionControl(0, "optNetPremium", GetLocalResourceObject("optNetPremium_CStr2Caption"), lintMarked, CStr(2),  , CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401))
Response.Write("</TD>" & vbCrLf)
Response.Write("            <TD>&nbsp;</TD>" & vbCrLf)
Response.Write("            <TD>")

	Response.Write(mobjValues.CheckControl("chkCesPreReaFac", GetLocalResourceObject("chkCesPreReaFacCaption"), mobjOptionInstall.sReinsurfCoRe, CStr(1),  , CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401))
Response.Write("</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD COLSPAN=""2"">&nbsp;</TD>" & vbCrLf)
Response.Write("            <TD>")

	Response.Write(mobjValues.CheckControl("chkCesPreReaObl", GetLocalResourceObject("chkCesPreReaOblCaption"), mobjOptionInstall.sReinsuroCoRe, CStr(1),  , CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401))
Response.Write("</TD>" & vbCrLf)
Response.Write("        </TR>                        " & vbCrLf)
Response.Write("    </TABLE>")

	
End Function

</script>
<%
Response.Expires = -1

'**+ The objects necessary are instancian to work the particularitities of creation of the form by generic routines  
'+ Se instancian los objetos necesarios para trabajr las particularidades de creación de la forma por rutinas genéricas

mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues
mobjOptionInstall = New eGeneral.OptionsInstallation

mobjValues.ActionQuery = CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401
mobjValues.sCodisplPage = "MCR002"
%> 
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<%="<SCRIPT LANGUAGE=""JavaScript"">"%>
var nMainAction = <%=Request.QueryString.Item("nMainAction")%>;
</SCRIPT>
<HTML>
	<HEAD>
		<META NAME		 = "GENERATOR" Content="Microsoft Visual Studio 6.0">
<SCRIPT LANGUAGE = "JavaScript" SRC="/VTimeNet/Scripts/Constantes.js">		</SCRIPT>
<SCRIPT LANGUAGE = "JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js">	</SCRIPT>


		<%=mobjValues.StyleSheet()%>
		<TITLE>Generalidades de las opciones de instalación</TITLE>
	</HEAD>
	
	<BODY ONUNLOAD="closeWindows();">
		<%
If Request.QueryString.Item("Type") <> "PopUp" Then Response.Write(mobjMenu.setZone(2, "MCR002", "MCR002.aspx"))
mobjMenu = Nothing
Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl")))
%>
		<FORM METHOD="POST" ACTION="valMantGeneral.aspx?nMainAction=<%=Request.QueryString.Item("nMainAction")%>">
		<%

'**+ The reading of the inserted values is made in the table of the options of installation of finance
'+ Se realiza la lectura de los valores caragados en la tabla de la opciones de instalación de financiamiento

mobjOptionInstall.insPreMCR002()

'**+ The fields of the page are defined to capture the data
'+ Se definen los campos de la página para capturar los datos

Call insPreMCR002()
%>
    </BODY>
</HTML>






