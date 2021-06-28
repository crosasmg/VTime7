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
Dim mobjMenu As eFunctions.Menues
Dim mclsClaim_peop As eClaim.Claim_peop

'- Campos que obtienen los valores por defecto
Dim lstrClient As String
Dim lstrCliename As String
Dim lstrDigit As String
Dim lintDemageTy As Integer
Dim llngNote As Integer
Dim lstrTds_Text As String


'----------------------------------------------------------------------------
Private Sub insPreSI070()
	'----------------------------------------------------------------------------
	Dim mclsClaimBenef As eClaim.ClaimBenef
	mclsClaim_peop = New eClaim.Claim_peop
	
	If mclsClaim_peop.find(mobjValues.StringToType(CStr(Session("nClaim")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(Session("nCase_num")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(Session("nDeman_type")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(Session("nId")), eFunctions.Values.eTypeData.etdDouble)) Then
		lstrClient = mclsClaim_peop.sClient
		lstrCliename = mclsClaim_peop.sCliename
		lstrDigit = mclsClaim_peop.sDigit
		lintDemageTy = mclsClaim_peop.nDamage_typ
		llngNote = mclsClaim_peop.nNoteNum
		lstrTds_Text = mclsClaim_peop.tDs_Text
	Else
		mclsClaimBenef = New eClaim.ClaimBenef
		If mclsClaimBenef.Find_Demandant(mobjValues.StringToType(CStr(Session("nClaim")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(Session("nCase_num")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(Session("nDeman_type")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(Session("nId")), eFunctions.Values.eTypeData.etdDouble)) Then
			
			lstrClient = mclsClaimBenef.sClient
			lstrCliename = mclsClaimBenef.sCliename
			lstrDigit = mclsClaimBenef.sDigit
		End If
		'UPGRADE_NOTE: Object mclsClaimBenef may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
		mclsClaimBenef = Nothing
	End If
	'UPGRADE_NOTE: Object mclsClaim_peop may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
	mclsClaim_peop = Nothing
End Sub

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("si070")

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.33.47
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "si070"
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.33.47
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.ActionQuery = Session("bQuery")
%>
<%'UPGRADE_NOTE: Language element '#INCLUDE' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file 'vtime/Includes/Constantes.aspx' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<!-- #INCLUDE VIRTUAL="~/VTimeNet/Includes/Constantes.aspx" -->

<SCRIPT>
//+ Variable para el control de versiones
    document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 12.24 $|$$Author: Nvaplat60 $"
</SCRIPT>    
<%'UPGRADE_NOTE: Language element 'SCRIPT' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file '/VTimeNet/Scripts/GenFunctions.js' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js">
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
<%'UPGRADE_NOTE: Language element 'SCRIPT' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file '/VTimeNet/Scripts/Constantes.js' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></SCRIPT>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">
<%
Response.Write(mobjValues.StyleSheet() & vbCrLf)
Response.Write(mobjMenu.setZone(2, "SI070", Request.QueryString("sWindowDescript"), Request.QueryString("nWindowTy")))
'UPGRADE_NOTE: Object mobjMenu may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjMenu = Nothing
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<%=mobjValues.ShowWindowsName("SI070", Request.QueryString("sWindowDescript"))%>
<FORM METHOD="post" ID="FORM" NAME="SI070" ACTION="ValCaseSeq.aspx?x=1">
    <A NAME="BeginPage"></A>
    <P ALIGN="Center">		
		<LABEL ID=40219><A HREF="#Datos de persona siniestrada"> Datos de persona siniestrada</A></LABEL><LABEL ID=0> | </LABEL>
		<LABEL ID=40219><A HREF="#Detalle de lesiones sufridas"> Detalle de lesiones sufridas</A></LABEL>        
    </P>
    <%Call insPreSI070()%>
    <TABLE WIDTH="100%">
		<TR>
            <TD WIDTH="100%" COLSPAN="4">&nbsp;</TD>
        </TR>
        <TR>
            <TD COLSPAN="4" CLASS="HighLighted"><LABEL ID=40257><A NAME="Datos de persona siniestrada">Datos de persona siniestrada</A></LABEL></TD>
        </TR>
        <TR>
            <TD WIDTH="100%" COLSPAN="4" CLASS="Horline"></TD>
        </TR>
        <TR>
            <TD WIDTH=20%><LABEL ID=9668>Código</LABEL></TD>
            <TD COLSPAN="3"><%=mobjValues.ClientControl("gmtClient", lstrClient,  , "Código del cliente asociado a la persona siniestrada",  , True, "lblClieName",  ,  ,  ,  ,  ,  ,  ,  ,  ,  , lstrCliename, lstrDigit)%></TD>
        </TR>
        <TR>
            <TD><LABEL ID=9669>Tipo de lesión</LABEL></TD>
            <TD COLSPAN="3"><%=mobjValues.PossiblesValues("cboDamagesTy", "Table7508", eFunctions.Values.eValuesType.clngComboType, CStr(CShort(lintDemageTy)))%></TD>
        </TR>
        <TR>
            <TD WIDTH="100%" COLSPAN="4">&nbsp;</TD>
        </TR>
        <TR>
            <TD COLSPAN="4" CLASS="HighLighted"><LABEL ID=40258><A NAME="Detalle de lesiones sufridas">Detalle de lesiones sufridas</A></LABEL></TD>
        </TR>
        <TR>
            <TD WIDTH="100%" COLSPAN="4" CLASS="Horline"></TD>
        </TR>
        </TABLE>
        <TABLE WIDTH="100%" COLS=2>
        <TR>
            <TD><%=mobjValues.TextAreaControl("tctNote", 2, 60, lstrTds_Text,  ,  ,  , True)%></TD>
            <TD><%=mobjValues.ButtonNotes("SCA2-K", llngNote, False, mobjValues.ActionQuery)%> </TD>
			<TD></TD>
            <TD></TD>
            <TD></TD>
            <TD></TD>
        </TR>
		</TABLE>
    <P ALIGN="Center">        
        <%=mobjValues.AnimatedButtonControl("btnBack", "/VTimeNet/Images/btnBack.gif", "Ir al inicio de la ventana", "#BeginPage")%>
    </P>
</FORM>
</BODY>
</HTML>
<%'UPGRADE_NOTE: Object mobjValues may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjValues = Nothing
%>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 11.33.47
Call mobjNetFrameWork.FinishPage("si070")
'UPGRADE_NOTE: Object mobjNetFrameWork may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




