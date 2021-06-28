<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="ePolicy" %>
<%@ Import namespace="eClaim" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 11.35.15
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
'- Obsejo para el manejo de la secuencia de menués.    
Dim mobjMenu As eFunctions.Menues
'+ Objeto para el manejo de la póliza    
Dim mobjPolicy As ePolicy.Policy
'+ Objeto para el manejo del siniestro    
Dim mobjClaim As eClaim.Claim
'+ Variable para el manejo del siniestro    
Dim lstrClaim As String


</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("si003")

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.35.15
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "si003"
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.35.15
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility
mobjPolicy = New ePolicy.Policy
mobjClaim = New eClaim.Claim

'- Se establece el estado del tipo de acción.
    If Session("nTransaction") = eClaim.Claim_win.eClaimTransac.clngClaimQuery Then
        mobjValues.ActionQuery = Session("bQuery")
    Else
        mobjValues.ActionQuery = False
        Session("bQuery") = False
    End If

Call mobjPolicy.Find("2", CInt(Session("nBranch")), CInt(Session("nProduct")), CDbl(Session("nPolicy")))

If mobjClaim.Find(CDbl(Session("nClaim"))) Then
	lstrClaim = mobjClaim.sLeadcial
Else
	lstrClaim = vbNullString
End If
%>
<!-- #INCLUDE VIRTUAL="~/VTimeNet/Includes/Constantes.aspx" -->
<!-- #INCLUDE VIRTUAL="~/VTimeNet/Includes/Claim.aspx" -->

<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT>
//+ Variable para el control de versiones
    document.VssVersion="$$Revision: 1 $|$$Date: 2/09/03 19:01 $"
</SCRIPT>
<HTML>
    <HEAD>
    <%Response.Write(mobjMenu.setZone(2, Request.QueryString("sCodispl"), Request.QueryString("sWindowDescript"), Request.QueryString("nWindowTy")))%>
mobjMenu = Nothing
<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0">
<%=mobjValues.StyleSheet()%>
    </HEAD>
    
    <BODY ONUNLOAD="closeWindows();">
        <FORM METHOD="post" ID="FORM" NAME="frmSI023" ACTION="valClaimSeq.aspx?sMode=1">
<%Response.Write(mobjValues.ShowWindowsName(Request.QueryString("sCodispl"), Request.QueryString("sWindowDescript")))%>
            <TABLE WIDTH="60%">
                <TR>
        			<TD><LABEL ID="0">Compañía</LABEL></TD>
                    <TD><%Response.Write(mobjValues.PossiblesValues("cbeCompany", "Company", eFunctions.Values.eValuesType.clngComboType, CStr(mobjPolicy.nLeadcomp),  ,  ,  ,  ,  ,  , True,  , "Compañia asociada abridora de la póliza correspondiente al siniestro"))%></TD>
                </TR>
                <TR>
        			<TD><LABEL ID="0">Tipo de Negocio</LABEL></TD>
                    <TD><%Response.Write(mobjValues.PossiblesValues("cbeBussiTyp", "Table20", eFunctions.Values.eValuesType.clngComboType, mobjPolicy.sBussityp,  ,  ,  ,  ,  ,  , True,  , "Tipo de negocio asociado a la póliza"))%></TD>
                </TR>
                <TR>
       				<TD><LABEL ID=0>Siniestro</LABEL></TD>
					<TD><%=mobjValues.TextControl("tctClaim", 12, lstrClaim,  , "Número identificativo del siniestro asignado por la compañia abridora")%></TD>
                </TR>
            </TABLE>
            <%=mobjValues.HiddenControl("cboWaitCode", CStr(0))%>
            <%=mobjValues.HiddenControl("lblnEnabledcboWaitCode", CStr(True))%>
            <%Response.Write("<SCRIPT>self.document.forms[0].tctClaim.focus();</SCRIPT>")

mobjPolicy = Nothing
mobjClaim = Nothing
mobjValues = Nothing%>
        </FORM>
    </BODY>
</HTML>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 11.35.15
Call mobjNetFrameWork.FinishPage("si003")
'UPGRADE_NOTE: Object mobjNetFrameWork may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




