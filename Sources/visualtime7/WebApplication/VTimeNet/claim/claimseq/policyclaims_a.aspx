<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eClaim" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 11.35.15
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
Dim mobjGrid As eFunctions.Grid


'% insDefineHeader: se definen los campos del grid
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.35.15
	mobjGrid.sSessionID = Session.SessionID
	mobjGrid.nUsercode = Session("nUsercode")
	'~End Body Block VisualTimer Utility
	
	mobjGrid.sCodisplPage = "policyclaims_a"
	Call mobjGrid.SetWindowParameters(Request.QueryString("sCodispl"), Request.QueryString("sWindowDescript"), Request.QueryString("nWindowTy"))
	With mobjGrid.Columns
		Call .AddNumericColumn(0, "Siniestro", "tcnClaim", 10, "")
		Call .AddDateColumn(0, "Fecha siniestro", "tcdOccurdat", "")
		'Call .AddTextColumn (0,"Causa","tctCause",30,"")
		Call .AddBranchColumn(0, "Ramo", "cbeBranch", "Código del ramo",  ,  ,  ,  ,  , True)
		Call .AddProductColumn(0, "Producto", "tcnProduct", "Código del producto")
		Call .AddNumericColumn(0, "Póliza", "tcnPolicy", 10, "")
		Call .AddNumericColumn(0, "Certificado", "tcnCertif", 10, "")
	End With
	With mobjGrid
		.AddButton = False
		.DeleteButton = False
		.Columns("Sel").OnClick = "insCheckSelClick(this)"
	End With
	
End Sub

'% insPrePolicyClaims: se cargan los siniestros del asegurado
'--------------------------------------------------------------------------------------------
Private Sub insPrePolicyClaims()
	'--------------------------------------------------------------------------------------------
	Dim lclsClaim As eClaim.Claim
	Dim lintIndex As Integer
	lclsClaim = New eClaim.Claim
	
	If lclsClaim.FindClaimCli(Request.QueryString("sClient"), mobjValues.StringToType(Request.QueryString("nBranch"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(Request.QueryString("nProduct"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(Request.QueryString("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString("nType"), eFunctions.Values.eTypeData.etdLong)) Then
		For lintIndex = 0 To lclsClaim.CountItemClaimCli
			If lclsClaim.ItemClaimCli(lintIndex) Then
				With mobjGrid
					.Columns("tcnClaim").DefValue = CStr(lclsClaim.nClaim)
					.Columns("tcdOccurdat").DefValue = CStr(lclsClaim.dOccurdat)
					'.Columns("tctCause").DefValue = lclsClaim.sDesClaimCause
					.Columns("cbeBranch").DefValue = CStr(lclsClaim.nBranch)
					.Columns("tcnProduct").DefValue = CStr(lclsClaim.nProduct)
					.Columns("tcnPolicy").DefValue = CStr(lclsClaim.nPolicy)
					.Columns("tcnCertif").DefValue = CStr(lclsClaim.nCertif)
					Response.Write(.DoRow)
				End With
			End If
		Next 
	End If
	Response.Write(mobjGrid.closeTable())
	'UPGRADE_NOTE: Object lclsClaim may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
	lclsClaim = Nothing
End Sub

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("policyclaims_a")

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.35.15
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "policyclaims_a"
%>
<HTML>
<HEAD>
<META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">
<%'UPGRADE_NOTE: Language element 'SCRIPT' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file '/VTimeNet/Scripts/GenFunctions.js' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<%'UPGRADE_NOTE: Language element 'SCRIPT' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file '/VTimeNet/Scripts/Claim.js' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Claim.js"></SCRIPT>
<%'UPGRADE_NOTE: Language element 'SCRIPT' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file '/VTimeNet/Scripts/valFunctions.js' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/valFunctions.js"></SCRIPT>
<%'UPGRADE_NOTE: Language element '#INCLUDE' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file 'vtime/Includes/Constantes.aspx' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<!-- #INCLUDE VIRTUAL="~/VTimeNet/Includes/Constantes.aspx" -->

<%'UPGRADE_NOTE: Language element '#INCLUDE' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file '/VTimeNet/Includes/Claim.aspx' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<!-- #INCLUDE VIRTUAL="~/VTimeNet/Includes/Claim.aspx" -->

    <%=mobjValues.StyleSheet()%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="frmPolicyClaims_a" ACTION="valClaimSeq.aspx?sTime=1">
	<DIV ID="Scroll" style="width:550;height:230;overflow:auto;outset gray">
<%
Call insDefineHeader()
If Request.QueryString("Type") <> "PopUp" Then
	Call insPrePolicyClaims()
End If
%>
	</DIV>
	<HR>
	<TABLE WIDTH=100%>
		<TR>
			<TD ALIGN="RIGHT"><%=mobjValues.ButtonAcceptCancel( ,  ,  ,  , eFunctions.Values.eButtonsToShow.OnlyCancel)%></TD>
		</TR>
	</TABLE>
</FORM>
</BODY>
</HTML>
<SCRIPT>
//- Variable que contiene el tipo enumerado para identificar la transacción a ejecutar
	var eClaimTransac = new eClaimTransac()
	
//-------------------------------------------------------------------------------------------
function insCheckSelClick(Field){
//-------------------------------------------------------------------------------------------
    var strParams;
    
    if(Field.checked){
    
        opener.top.frames['fraHeader'].document.forms[0].tcnClaim.value = marrArray[Field.value].tcnClaim
        opener.top.frames['fraHeader'].document.forms[0].cbeBranch.value = marrArray[Field.value].cbeBranch
		opener.top.frames['fraHeader'].document.forms[0].valProduct.value = marrArray[Field.value].tcnProduct
		opener.top.frames['fraHeader'].document.forms[0].tcnPolicy.value = marrArray[Field.value].tcnPolicy
		opener.top.frames['fraHeader'].document.forms[0].tcnCertificat.value = marrArray[Field.value].tcnCertif
		opener.top.frames['fraHeader'].$('#tcnClaim').change();
		window.close();
    }
}
</SCRIPT>
<%
'UPGRADE_NOTE: Object mobjValues may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjValues = Nothing
'UPGRADE_NOTE: Object mobjGrid may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjGrid = Nothing
%>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 11.35.15
Call mobjNetFrameWork.FinishPage("policyclaims_a")
'UPGRADE_NOTE: Object mobjNetFrameWork may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




