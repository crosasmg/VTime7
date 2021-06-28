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
Dim mobjMenu As eFunctions.Menues

Dim lintStaClaim As Integer
Dim mclsClaim As eClaim.Claim
Dim mintCancel As Byte
Dim mintReject As Byte
Dim mintDesistimiento As Byte
Dim mstrLetter As String
Dim mintCause As Integer
Dim mstrClaimType As String


'% insPreSI006: 
'--------------------------------------------------------------------------------------------
Private Sub insPreSI006()
        '--------------------------------------------------------------------------------------------
        
        If Session("nTransaction") = eClaim.Claim_win.eClaimTransac.clngClaimQuery Then
            mobjValues.ActionQuery = Session("bQuery")
        Else
            mobjValues.ActionQuery = False
            Session("bQuery") = False
        End If
        
	Call insReaClaim()
	' Rechazo
	If CDbl(Session("nTransaction")) = 15 Then
		mintReject = 1
		mintCancel = 0
		mintDesistimiento = 0
		' Anulación 
	ElseIf CDbl(Session("nTransaction")) = 7 Then 
		mintReject = 0
		mintCancel = 1
		mintDesistimiento = 0
		' Desistimiento
	ElseIf CDbl(Session("nTransaction")) = 17 Then 
		mintReject = 0
		mintCancel = 0
		mintDesistimiento = 1
	End If
End Sub

'%insReaClaim: Esta función realiza la lectura del siniestro para mostrar 
'%             los datos de anulación/rechazo/Desistimiento
'--------------------------------------------------------------------------------------------
Private Sub insReaClaim()
	'--------------------------------------------------------------------------------------------
	mclsClaim = New eClaim.Claim
	If mclsClaim.Find(CDbl(Session("nClaim"))) Then
		mstrClaimType = mclsClaim.sClaimtyp
		lintStaClaim = mclsClaim.sStaclaim
		If lintStaClaim = 1 Then
			mintCancel = 1
			mintCause = mclsClaim.nNullcode
		ElseIf lintStaClaim = 7 Then 
			mintReject = 1
			mintCause = mclsClaim.nUnaccode
		Else
			mintCancel = 0
			mintReject = 0
			mintCause = 0
		End If
		mstrLetter = mclsClaim.sMailnumb
	End If
	'UPGRADE_NOTE: Object mclsClaim may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
	mclsClaim = Nothing
End Sub

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("si006")

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.35.15
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "si006"
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.35.15
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility
%>

<HTML>
<HEAD>
    <%'UPGRADE_NOTE: Language element '#INCLUDE' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file 'vtime/Includes/Constantes.aspx' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<!-- #INCLUDE VIRTUAL="~/VTimeNet/Includes/Constantes.aspx" -->

    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">
	<%'UPGRADE_NOTE: Language element 'SCRIPT' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file '/VTimeNet/Scripts/Constantes.js' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></SCRIPT>    
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
	  
	<%With Response
	.Write(mobjValues.StyleSheet() & vbCrLf)
	.Write(mobjMenu.setZone(2, "SI006", Request.QueryString("sWindowDescript"), Request.QueryString("nWindowTy")))
End With
'UPGRADE_NOTE: Object mobjMenu may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjMenu = Nothing
%>
<SCRIPT>
	//+ Variable para el control de versiones
    document.VssVersion="$$Revision: 1 $|$$Date: 2/09/03 19:01 $"
</SCRIPT>	
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="frmSI006" ACTION="ValClaimSeq.aspx?Mode=1">
    <%=mobjValues.ShowWindowsName("SI006", Request.QueryString("sWindowDescript"))%>
    <%Call insPreSI006()%>
    <TABLE WIDTH="100%">
		<TR>
            <TD><%=mobjValues.OptionControl(0, "optRejected", "Rechazo", CStr(mintReject), "1",  , True, 1)%></TD>
            <TD><%=mobjValues.OptionControl(0, "optRejected", "Anulación", CStr(mintCancel), "2",  , True, 2)%></TD>
			<TD><%=mobjValues.OptionControl(0, "optRejected", "Desistimiento", CStr(mintDesistimiento), "3",  , True, 3)%></TD>
        </TR>
        <TR>
            <TD><LABEL ID=0>Causa</LABEL></TD>
            <%With mobjValues
	'+ Se excluye el cero ("No aplica")				
	.List = "0"
	.TypeList = 2
End With
If Not CDbl(Session("nTransaction")) = 17 Then
	If CDbl(Session("nTransaction")) = 15 Then%>  
			          <TD><%=mobjValues.PossiblesValues("cboNullClaim", "Table133", eFunctions.Values.eValuesType.clngComboType, CStr(CInt(mintCause)),  ,  ,  ,  ,  ,  ,  ,  ,  ,  , 4)%></TD> 
			  <%	Else%> 
			          <TD><%=mobjValues.PossiblesValues("cboNullClaim", "Table136", eFunctions.Values.eValuesType.clngComboType, CStr(CInt(mintCause)),  ,  ,  ,  ,  ,  ,  ,  ,  ,  , 4)%></TD> 
			  <%	End If
Else%> 
			      <TD><%=mobjValues.PossiblesValues("cboNullClaim", "Table136", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  , True,  ,  ,  , 4)%></TD> 
			  <%End If
%>
        </TR>
        <TR>
            <TD><LABEL ID=0>Carta</LABEL></TD> 
            <TD><%=mobjValues.TextControl("gmtLetter", 6, CStr(mstrLetter),  ,  ,  ,  ,  ,  ,  , 5)%></TD> 
        </TR> 
    </TABLE> 
    <%=mobjValues.HiddenControl("lblClaimType", CStr(mstrClaimType))%>
</FORM>
</BODY>
</HTML>

<%'UPGRADE_NOTE: Object mobjValues may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjValues = Nothing
%>

<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 11.35.15
Call mobjNetFrameWork.FinishPage("si006")
'UPGRADE_NOTE: Object mobjNetFrameWork may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




