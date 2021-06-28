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
Private Function insPreMCA000() As Object
	'--------------------------------------------------------------------------------------------------
	
Response.Write("" & vbCrLf)
Response.Write("    <TABLE WIDTH=""100%"">" & vbCrLf)
Response.Write("    <TR>" & vbCrLf)
Response.Write("        <TD><LABEL ID=0>" & GetLocalResourceObject("cbeSalePolCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("        <TD>")

	Response.Write(mobjValues.PossiblesValues("cbeSalePol", "Table696", eFunctions.Values.eValuesType.clngComboType, CStr(mobjOptionInstall.nPolicySalePol),  ,  ,  ,  ,  ,  , CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401,  , GetLocalResourceObject("cbeSalePolToolTip")))
Response.Write("</TD>" & vbCrLf)
Response.Write("	</TR>" & vbCrLf)
Response.Write("	<TR>" & vbCrLf)
Response.Write("        <TD><LABEL ID=0>" & GetLocalResourceObject("valIntermediaCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("        <TD>")

	Response.Write(mobjValues.PossiblesValues("valIntermedia", "TabIntermedia", eFunctions.Values.eValuesType.clngWindowType, CStr(mobjOptionInstall.nIntermedPol), False,  ,  ,  ,  ,  , CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401, 5, GetLocalResourceObject("valIntermediaToolTip"), eFunctions.Values.eTypeCode.eNumeric))
Response.Write("</TD>" & vbCrLf)
Response.Write("	</TR>" & vbCrLf)
Response.Write("	<TR>" & vbCrLf)
Response.Write("        <TD><LABEL ID=0>" & GetLocalResourceObject("cbeCurrencyPolCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("        <TD>")

	Response.Write(mobjValues.PossiblesValues("cbeCurrencyPol", "Table11", eFunctions.Values.eValuesType.clngComboType, CStr(mobjOptionInstall.nCurrencyPol),  ,  ,  ,  ,  ,  , CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401,  , GetLocalResourceObject("cbeCurrencyPolToolTip")))
Response.Write("</TD>" & vbCrLf)
Response.Write("	</TR>" & vbCrLf)
Response.Write("	<TR>" & vbCrLf)
Response.Write("        <TD>")

	Response.Write(mobjValues.CheckControl("chkPrintClause", GetLocalResourceObject("chkPrintClauseCaption"), mobjOptionInstall.sClauseImpPol, CStr(1),  , CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401))
Response.Write("</TD>" & vbCrLf)
Response.Write("    </TR>    " & vbCrLf)
Response.Write("    <TR>" & vbCrLf)
Response.Write("        <TD>")

	Response.Write(mobjValues.CheckControl("chkSTock_ind", GetLocalResourceObject("chkSTock_indCaption"), mobjOptionInstall.sSTock_indPol, CStr(1),  , CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401))
Response.Write("</TD>" & vbCrLf)
Response.Write("    </TR>" & vbCrLf)
Response.Write("	</TABLE>")

	
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
mobjValues.sCodisplPage = "MCA000"
%> 
<HTML>
<HEAD>
	<META NAME		 = "GENERATOR" CONTENT="Microsoft Visual Studio 6.0">
<SCRIPT LANGUAGE = "JavaScript" SRC="/VTimeNet/Scripts/Constantes.js">		</SCRIPT>
<SCRIPT LANGUAGE = "JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js">	</SCRIPT>


<SCRIPT>
    var nMainAction = <%=Request.QueryString.Item("nMainAction")%>;
    
//- Variable para el control de versiones
	document.VssVersion="$$Revision: 1 $|$$Date: 2/09/03 19:03 $|$$Author: Iusr_llanquihue $"
</SCRIPT>
	<%
Response.Write(mobjValues.StyleSheet())
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write(mobjMenu.setZone(2, "MCA000", "MCA000.aspx"))
End If
mobjMenu = Nothing
Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl")))
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ACTION="valMantGeneral.aspx?nMainAction=<%=Request.QueryString.Item("nMainAction")%>" id=form1 name=form1>
	<%
'**+ The reading of the inserted values is made in the table of the options of installation of finance
'+ Se realiza la lectura de los valores caragados en la tabla de la opciones de instalación de Póliza	
mobjOptionInstall.insPreMCA000()

'**+ The fields of the page are defined to capture the data
'+ Se definen los campos de la página para capturar los datos
insPreMCA000()
%>
</BODY>
</HTML>





