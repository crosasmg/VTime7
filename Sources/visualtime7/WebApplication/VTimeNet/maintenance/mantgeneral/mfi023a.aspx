<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eGeneral" %>
<script language="VB" runat="Server">

Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues
Dim mobjOptionInstall As eGeneral.OptionsInstallation



'**% insDefineFields : defines the structure of the page "painting" the precise fields and the grid
'%   insDefineFields : define la estructura de la página "pintando" los campos puntuales y el grid
'--------------------------------------------------------------------------------------------------
Private Function insPreMCR002() As Object
	'--------------------------------------------------------------------------------------------------
	Dim lintMarked As Byte
	
	If mobjOptionInstall.nCoaCessCoRe = 1 Then
		lintMarked = 2
	Else
		lintMarked = 1
	End If
	
Response.Write("" & vbCrLf)
Response.Write("    <TABLE WIDTH=""100%"">" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD CLASS = ""HIGHLIGHTED""><LABEL ID=0>" & GetLocalResourceObject("AnchorCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD WIDTH = ""10%"">" & vbCrLf)
Response.Write("        	</TD>" & vbCrLf)
Response.Write("            <TD WIDTH = ""40%"">" & vbCrLf)
Response.Write("        	</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD CLASS = ""HORLINE""></TD>" & vbCrLf)
Response.Write("            <TD WIDTH = ""10%"">" & vbCrLf)
Response.Write("            <TD WIDTH = ""40%"">" & vbCrLf)
Response.Write("        	</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD>" & vbCrLf)
Response.Write("                <LABEL ID=0>" & GetLocalResourceObject("tcnInterestCaption") & "</LABEL>" & vbCrLf)
Response.Write("                ")

	Response.Write(mobjValues.NumericControl("tcnInterest", 5, CStr(mobjOptionInstall.nUpperIntPrem),  , GetLocalResourceObject("tcnInterestToolTip"), False, False,  ,  ,  ,  , CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401))
Response.Write("" & vbCrLf)
Response.Write("            </TD>                " & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("        	<TD WIDTH = ""40%"">" & vbCrLf)
Response.Write("        		")

	Response.Write(mobjValues.CheckControl("chkInterestAdd", GetLocalResourceObject("chkInterestAddCaption"), mobjOptionInstall.sMod_upLimPrem, CStr(1),  , CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401))
Response.Write("" & vbCrLf)
Response.Write("        	</TD>" & vbCrLf)
Response.Write("        	<TD WIDTH = ""10%"">" & vbCrLf)
Response.Write("        	</TD>        	        	" & vbCrLf)
Response.Write("        	<TD WIDTH = ""40%"">" & vbCrLf)
Response.Write("        	    <LABEL ID=0>" & GetLocalResourceObject("tcnInterestCaption") & "</LABEL>" & vbCrLf)
Response.Write("        		")

	Response.Write(mobjValues.NumericControl("tcnInterestAdd", 5, CStr(mobjOptionInstall.nUpperIntPrem),  , GetLocalResourceObject("tcnInterestAddToolTip"), False, False,  ,  ,  ,  , CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401))
Response.Write("" & vbCrLf)
Response.Write("        	</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("        	<TD>" & vbCrLf)
Response.Write("        		")

	Response.Write(mobjValues.CheckControl("chkInterestSub", GetLocalResourceObject("chkInterestSubCaption"), mobjOptionInstall.sMod_loLimPrem, CStr(1),  , CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401))
Response.Write("" & vbCrLf)
Response.Write("        	</TD>" & vbCrLf)
Response.Write("        	<TD WIDTH = ""10%"">" & vbCrLf)
Response.Write("        	</TD>        	        	" & vbCrLf)
Response.Write("            <TD>" & vbCrLf)
Response.Write("                <LABEL ID=0>" & GetLocalResourceObject("tcnInterestCaption") & "</LABEL>" & vbCrLf)
Response.Write("        		")

	Response.Write(mobjValues.NumericControl("tcnInterestSub", 5, CStr(mobjOptionInstall.nLowerIntPrem),  , GetLocalResourceObject("tcnInterestSubToolTip"), False, False,  ,  ,  ,  , CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401))
Response.Write("" & vbCrLf)
Response.Write("        	</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD>" & vbCrLf)
Response.Write("                <LABEL ID=0>" & GetLocalResourceObject("tcnLevelCaption") & "</LABEL>" & vbCrLf)
Response.Write("        		")

	Response.Write(mobjValues.NumericControl("tcnLevel", 2, CStr(mobjOptionInstall.nUpperIntPrem),  , GetLocalResourceObject("tcnLevelToolTip"), False, False,  ,  ,  ,  , CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401))
Response.Write("" & vbCrLf)
Response.Write("        	</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD CLASS = ""HIGHLIGHTED""><LABEL ID=0>" & GetLocalResourceObject("Anchor2Caption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD WIDTH = ""10%"">" & vbCrLf)
Response.Write("        	</TD>" & vbCrLf)
Response.Write("            <TD CLASS = ""HIGHLIGHTED""><LABEL ID=0>" & GetLocalResourceObject("Anchor3Caption") & "</LABEL></TD>            " & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD CLASS = ""HORLINE""></TD>" & vbCrLf)
Response.Write("            <TD WIDTH = ""10%"">" & vbCrLf)
Response.Write("        	</TD>" & vbCrLf)
Response.Write("            <TD CLASS = ""HORLINE""></TD>            " & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("        	<TD>" & vbCrLf)
Response.Write("        		")

	Response.Write(mobjValues.OptionControl(0, "optInterestExa", GetLocalResourceObject("optInterestExa_CStr1Caption"), CStr(mobjOptionInstall.nUpperIntPrem), CStr(1),  , CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401))
Response.Write("" & vbCrLf)
Response.Write("                ")

	Response.Write(mobjValues.OptionControl(0, "optInterestExa", GetLocalResourceObject("optInterestExa_CStr1Caption"), CStr(mobjOptionInstall.nUpperIntPrem), CStr(1),  , CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401))
Response.Write("" & vbCrLf)
Response.Write("            </TD>" & vbCrLf)
Response.Write("        	<TD WIDTH = ""10%"">" & vbCrLf)
Response.Write("        	</TD>        	        	" & vbCrLf)
Response.Write("            <TD>            " & vbCrLf)
Response.Write("        		")

	Response.Write(mobjValues.OptionControl(0, "optTimeExa", GetLocalResourceObject("optTimeExa_CStr1Caption"), CStr(mobjOptionInstall.nUpperIntPrem), CStr(1),  , CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401))
Response.Write("" & vbCrLf)
Response.Write("        		")

	Response.Write(mobjValues.OptionControl(0, "optTimeExa", GetLocalResourceObject("optTimeExa_CStr1Caption"), CStr(mobjOptionInstall.nUpperIntPrem), CStr(1),  , CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401))
Response.Write("" & vbCrLf)
Response.Write("        	</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("    </TABLE>        ")

	
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
mobjValues.sCodisplPage = "MFI023A"
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
		<FORM METHOD="POST" ACTION="valMantGeneral.aspx?nMainAction=<%=Request.QueryString.Item("nMainAction")%>" id=form1 name=form1>
		<%

'**+ The reading of the inserted values is made in the table of the options of installation of finance
'+ Se realiza la lectura de los valores caragados en la tabla de la opciones de instalación de financiamiento

mobjOptionInstall.insPreMCR002()

'**+ The fields of the page are defined to capture the data
'+ Se definen los campos de la página para capturar los datos

insPreMCR002()
%>
    </BODY>
</HTML>






