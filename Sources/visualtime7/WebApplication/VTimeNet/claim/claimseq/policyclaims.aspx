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
	
	mobjGrid.sCodisplPage = "policyclaims"
	Call mobjGrid.SetWindowParameters(Request.QueryString("sCodispl"), Request.QueryString("sWindowDescript"), Request.QueryString("nWindowTy"))
	With mobjGrid.Columns
		Call .AddNumericColumn(100418, "Siniestro", "nClaim", 10, "")
		Call .AddDateColumn(100421, "Fecha de ocurrencia", "dOccurdat", "")
		Call .AddTextColumn(100418, "Estado", "sStatus", 30, "")
		Call .AddTextColumn(100419, "Causa", "sCause", 30, "")
		Call .AddTextColumn(100420, "Tipo", "sType", 30, "")
	End With
	With mobjGrid
		.AddButton = False
		.DeleteButton = False
		.Columns("Sel").GridVisible = False
	End With
End Sub

'% insPrePolicyClaims: se cargan los siniestros del asegurado
'--------------------------------------------------------------------------------------------
Private Sub insPrePolicyClaims()
	'--------------------------------------------------------------------------------------------
	Dim lclsClaim As eClaim.Claim
	Dim lintIndex As Integer
	lclsClaim = New eClaim.Claim
	
	If lclsClaim.FindClaimIns(Request.QueryString("sClient"), mobjValues.StringToType(CStr(Session("nClaim")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString("nCase_num"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString("nDeman_type"), eFunctions.Values.eTypeData.etdDouble)) Then
		For lintIndex = 0 To lclsClaim.CountItemClaimIns
			If lclsClaim.ItemClaimIns(lintIndex) Then
				With mobjGrid
					.Columns("nClaim").DefValue = CStr(lclsClaim.nClaim)
					.Columns("dOccurdat").DefValue = CStr(lclsClaim.dOccurdat)
					.Columns("sStatus").DefValue = lclsClaim.sStaClaimDes
					.Columns("sCause").DefValue = lclsClaim.sDesClaimCause
					.Columns("sType").DefValue = lclsClaim.sClaimTypDes
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
Call mobjNetFrameWork.BeginPage("policyclaims")

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.35.15
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "policyclaims"
%>

<HTML>
<HEAD>
    <%'UPGRADE_NOTE: Language element 'SCRIPT' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file '/VTimeNet/Scripts/GenFunctions.js' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
    <%'UPGRADE_NOTE: Language element '#INCLUDE' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file 'vtime/Includes/Constantes.aspx' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<!-- #INCLUDE VIRTUAL="~/VTimeNet/Includes/Constantes.aspx" -->

    <%=mobjValues.StyleSheet()%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="frmPolicyClaims" ACTION="valClaimSeq.aspx?sTime=1">
	<DIV ID="Scroll" style="width:500;height:230;overflow:auto;outset gray">
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
<%
'UPGRADE_NOTE: Object mobjValues may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjValues = Nothing
'UPGRADE_NOTE: Object mobjGrid may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjGrid = Nothing
%>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 11.35.15
Call mobjNetFrameWork.FinishPage("policyclaims")
'UPGRADE_NOTE: Object mobjNetFrameWork may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




