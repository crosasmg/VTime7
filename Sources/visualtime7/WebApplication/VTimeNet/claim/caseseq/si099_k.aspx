<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eClaim" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 11.33.47
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
'- Objeto para el manejo del menú de la página
Dim MobjMenu As eFunctions.Menues


'% reaCase: busca la descripción del caso en tratamiento
'--------------------------------------------------------------------------------------------
Private Function reaCase() As String
	'--------------------------------------------------------------------------------------------
	Dim lclsClaim_case As eClaim.Claim_case
	lclsClaim_case = New eClaim.Claim_case
	
	If lclsClaim_case.ValClaim_case(mobjValues.StringToType(CStr(Session("nClaim")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(Session("nCase_num")), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(CStr(Session("nDeman_type")), eFunctions.Values.eTypeData.etdLong)) Then
		reaCase = Session("nCase_num") & " - " & lclsClaim_case.sDeman_type & " - " & lclsClaim_case.sCliename
	End If
	'UPGRADE_NOTE: Object lclsClaim_case may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
	lclsClaim_case = Nothing
End Function

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("si099_k")
mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.33.47
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "si099_k"

mobjValues.ActionQuery = True

Session("nCase_num") = Request.QueryString("nCase_num")
Session("nDeman_type") = Request.QueryString("nDeman_type")
Session("nBene_type") = Request.QueryString("nBene_type")
Session("nId") = Request.QueryString("nId")
%>
<SCRIPT>
//% insStateZone: 
//--------------------------------------------------------------------------------------------
function insStateZone(){
//--------------------------------------------------------------------------------------------
}
</SCRIPT>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">
    <%'UPGRADE_NOTE: Language element 'SCRIPT' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file '/VTimeNet/Scripts/GenFunctions.js' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
    <%'UPGRADE_NOTE: Language element 'SCRIPT' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file '/VTimeNet/Scripts/tmenu.js' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></SCRIPT>
    <%'UPGRADE_NOTE: Language element '#INCLUDE' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file '/VTimeNet/Includes/Constantes.aspx' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<!-- #INCLUDE VIRTUAL="~/VTimeNet/Includes/Constantes.aspx" -->

	<%
MobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.33.47
MobjMenu.sSessionID = Session.SessionID
MobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility
Response.Write(MobjMenu.MakeMenu("SI099", "SI099_K.aspx", 1, Request.QueryString("sWindowDescript"), CStr(Session("sDesMultiCompany")), CStr(Session("sSche_code"))))
'UPGRADE_NOTE: Object MobjMenu may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
MobjMenu = Nothing
With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjValues.WindowsTitle("SI099", Request.QueryString("sWindowDescript")))
End With
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="SI099" ACTION="valCaseSeq.aspx?mode=1">
    <P>&nbsp;</P>
    <TABLE WIDTH="100%">
        <TR>
            <TD WIDTH=25%>&nbsp;</TD>
            <TD><LABEL ID=9368>Caso</LABEL></TD>
            <TD><%=mobjValues.TextControl("CaseDescript", 30, reaCase())%></TD>
        </TR>
    </TABLE>
    
<%="<SCRIPT>top.frames['fraSequence'].location.href='Sequence.aspx?nBene_type=" & Request.QueryString("nBene_type") & "&sGoToNext=Yes'</SCRIPT>"%>
	
<%'UPGRADE_NOTE: Object mobjValues may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjValues = Nothing%>
</FORM>
</BODY>
</HTML>


<SCRIPT>
//% insStateZone: 
//--------------------------------------------------------------------------------------------
function insCancel(){
//--------------------------------------------------------------------------------------------
	top.close()
}

//% insStateZone: 
//--------------------------------------------------------------------------------------------
function insFinish(){
//--------------------------------------------------------------------------------------------
	return true
}
</SCRIPT>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 11.33.47
Call mobjNetFrameWork.FinishPage("si099_k")
'UPGRADE_NOTE: Object mobjNetFrameWork may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




