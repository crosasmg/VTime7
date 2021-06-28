<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eGeneral" %>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores

Dim mobjValues As eFunctions.Values

'- Objeto para el manejo del menú de la página

Dim MobjMenu As eFunctions.Menues

'- Objeto para manejar las opciones de instalación

Dim mobjOptionInstall As eGeneral.OptionsInstallation


</script>
<%Response.Expires = -1

mobjOptionInstall = New eGeneral.OptionsInstallation
mobjValues = New eFunctions.Values
mobjValues.sCodisplPage = "MFI023"
mobjValues.ActionQuery = True
%>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></SCRIPT>


	<%
MobjMenu = New eFunctions.Menues
Response.Write(MobjMenu.MakeMenu("MFI023", "MFI023_K.aspx", 1, ""))
MobjMenu = Nothing
With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjValues.WindowsTitle("MFI023"))
End With
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="MFI023" ACTION="ValOptFinanceSeq.aspx?mode=1">
    <P>&nbsp;</P>
    <TABLE WIDTH="100%">
        <TR>
            <TD COLSPAN = "4" CLASS = "HIGHLIGHTED"><LABEL ID=0><%= GetLocalResourceObject("AnchorCaption") %></LABEL></TD>
        </TR>
        <TR>
            <TD COLSPAN = "4" CLASS = "HORLINE"></TD>
        </TR>
        <TR>
        </TR>
        <TR>
            <TD><LABEL ID=0><%= GetLocalResourceObject("cbeRefinDraCaption") %></LABEL></TD>
            <TD><%Response.Write(mobjValues.PossiblesValues("cbeRefinDra", "Table252", eFunctions.Values.eValuesType.clngComboType, CStr(mobjOptionInstall.nCurrencyPol),  ,  ,  ,  ,  ,  , CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401,  , GetLocalResourceObject("cbeRefinDraToolTip")))%></TD>
            <TD><%Response.Write(mobjValues.CheckControl("chkRefinDraChg", GetLocalResourceObject("chkRefinDraChgCaption"), mobjOptionInstall.sClauseImpPol, CStr(1),  , CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401))%></TD>
        </TR>
        <TR>
            <TD COLSPAN = "2"></TD>
            <TD><LABEL ID=0><%= GetLocalResourceObject("tcnRefinDraLevCaption") %></LABEL></TD>
            <TD><%Response.Write(mobjValues.NumericControl("tcnRefinDraLev", 2, CStr(mobjOptionInstall.nUpper_limPrem),  , GetLocalResourceObject("tcnRefinDraLevToolTip"), False, False,  ,  ,  ,  , CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401))%></TD>
        </TR>
        <TR>
        </TR>
        <TR>
            <TD><LABEL ID=0><%= GetLocalResourceObject("cboNullOptCaption") %></LABEL></TD>
            <TD><%Response.Write(mobjValues.PossiblesValues("cboNullOpt", "Table254", eFunctions.Values.eValuesType.clngComboType, CStr(mobjOptionInstall.nCurrencyPol),  ,  ,  ,  ,  ,  , CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401,  , GetLocalResourceObject("cboNullOptToolTip")))%></TD>
            <TD><%Response.Write(mobjValues.CheckControl("chkNullOptChg", GetLocalResourceObject("chkNullOptChgCaption"), mobjOptionInstall.sClauseImpPol, CStr(1),  , CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401))%></TD>
        </TR>
        <TR>
            <TD COLSPAN = "2"></TD>
            <TD><LABEL ID=0><%= GetLocalResourceObject("tcnRefinDraLevCaption") %></LABEL></TD>
            <TD><%Response.Write(mobjValues.NumericControl("tcnRefinDraLev", 2, CStr(mobjOptionInstall.nUpper_limPrem),  , GetLocalResourceObject("tcnRefinDraLevToolTip"), False, False,  ,  ,  ,  , CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401))%></TD>
        </TR>
	</TABLE>
    <%="<SCRIPT>top.frames['fraSequence'].location.href='Sequence.aspx?nBene_type=" & Request.QueryString.Item("nBene_type") & "&sGoToNext=Yes'</SCRIPT>"%>
</FORM>
</BODY>
</HTML>
<SCRIPT>

//% insStateZone: 
//--------------------------------------------------------------------------------------------
function insStateZone()
//--------------------------------------------------------------------------------------------
{
}

//% insStateZone: 
//--------------------------------------------------------------------------------------------
function insCancel()
//--------------------------------------------------------------------------------------------
{	
	top.close()
}

//% insStateZone: 
//--------------------------------------------------------------------------------------------
function insFinish()
//--------------------------------------------------------------------------------------------
{
	return true
}
</SCRIPT>




