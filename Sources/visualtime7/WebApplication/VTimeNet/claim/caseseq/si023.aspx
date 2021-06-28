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
'- Obsejo para el manejo de la secuencia de menués.    
Dim mobjMenu As eFunctions.Menues
'+ Objeto para el manejo de terceros    
Dim mobjClaimThird As eClaim.Claim_thir


</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("si023")

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.33.47
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "si023"
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.33.47
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility
mobjClaimThird = New eClaim.Claim_thir

'- Se establece el estado del tipo de acción.
mobjValues.ActionQuery = Session("bQuery")

'Call mobjClaimThird.insPreSI023(Session("nClaim"), Session("nCase_num"), Session("nDeman_type"))

%>
<!-- #INCLUDE VIRTUAL="~/VTimeNet/Includes/Constantes.aspx" -->
<!-- #INCLUDE VIRTUAL="~/VTimeNet/Includes/Claim.aspx" -->

<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript">
// UpdateFields: Habilita o no los campos siniestro y póliza.
//-------------------------------------------------------------------------------------------
function UpdateFields(lobjName){
    if (lobjName.value==0){
		with(self.document.forms[0]){
			gmtThirCLaim.disabled = true;
			gmtThirPolicy.disabled = true;
			gmtThirCLaim.value = "";
			gmtThirPolicy.value = "";
		}
    }
    else{
		with(self.document.forms[0]){
			gmtThirCLaim.disabled = false;
			gmtThirPolicy.disabled = false;
        }
    }
}


</SCRIPT>

<HTML>
<HEAD>
    <%Response.Write(mobjMenu.setZone(2, Request.QueryString("sCodispl"), Request.QueryString("sWindowDescript"), Request.QueryString("nWindowTy")))%>
mobjMenu = Nothing
<META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">
<%=mobjValues.StyleSheet()%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
    <FORM METHOD="post" ID="FORM" NAME="frmSI023" ACTION="valCaseSeq.aspx?sMode=1">
        <A NAME="BeginPage"></A>
    	<P ALIGN="Center">
    		<LABEL ID=40240><A HREF="#Datos generales de terceros II"> Datos generales de terceros II</A></LABEL><LABEL ID=0> | </LABEL>
    		<LABEL ID=40242><A HREF="#Información de la compañía contraria"> Información de la compañía contraria</A></LABEL><LABEL ID=0> | </LABEL>
    		<LABEL ID=40244><A HREF="#Acuerdo establecido"> Acuerdo establecido</A></LABEL>
        </P>
<%Response.Write(mobjValues.ShowWindowsName(Request.QueryString("sCodispl"), Request.QueryString("sWindowDescript")))%>
        <TABLE WIDTH="100%">
            <TR>
                <TD COLSPAN="2" CLASS="HighLighted"><LABEL ID=40245><A NAME="Datos generales de terceros II">Datos generales de terceros II</A></LABEL></TD>
                <TD></TD>
                <TD COLSPAN="2" CLASS="HighLighted"><LABEL ID=40246><A NAME="Información de la compañía contraria">Información de la compañía contraria</A></LABEL></TD>
            </TR>
            <TR>
                <TD COLSPAN="2"><HR></TD>
                <TD></TD>
                <TD COLSPAN="2"><HR></TD>
            </TR>
            <TR>
    			<TD><LABEL ID=9614>Culpabilidad del tercero</LABEL></TD>
                <TD><%
Response.Write(mobjValues.PossiblesValues("cboBlame", "Table204", 1))
%>
                </TD>
                <TD></TD>
                <TD><LABEL ID=9616>Nombre</LABEL></TD>
    			<TD><%
'UPGRADE_WARNING: Use of Null/IsNull() detected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1049.aspx'
mobjValues.Parameters.Add("sType", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
Response.Write(mobjValues.PossiblesValues("cboComp", "tabcompany_stype", 1, CStr(mobjClaimThird.nThir_comp), True,  ,  ,  ,  , "UpdateFields(this)"))%>
    			</TD>
            </TR>
            <TR>
                <TD COLSPAN="3">&nbsp;</TD>
                <TD><LABEL ID=9615>Siniestro</LABEL></TD>
    			<TD><%=mobjValues.TextControl("gmtThirCLaim", 12, mobjClaimThird.sThir_claim,  , "",  ,  ,  ,  , True)%></TD>
            </TR>
            <TR>
                <TD COLSPAN="3">&nbsp;</TD>
                <TD><LABEL ID=9617>Póliza</LABEL></TD>
                <TD><%=mobjValues.TextControl("gmtThirPolicy", 12, mobjClaimThird.sThir_polic,  , "",  ,  ,  ,  , True)%></TD>
            </TR>
            <TR>
                <TD COLSPAN="5">&nbsp;</TD>
            </TR>
            <TR>
                <TD COLSPAN="5" CLASS="HighLighted"><LABEL ID=40247><A NAME="Acuerdo establecido">Acuerdo establecido</A></LABEL></TD>
            </TR>
            <TR>
                <TD COLSPAN="5"><HR></TD>
            </TR>
            <TR>
                <TD COLSPAN=5 ALIGN=CENTER><%
With mobjValues
	Response.Write(.TextAreaControl("txtNoteAgree", 5, 60, mobjClaimThird.sDescriptNote,  ,  ,  , True))
	Response.Write(.ButtonNotes("SCA2-K", mobjClaimThird.nNoteAgree, False, mobjValues.ActionQuery))
End With
%></TD>
            </TR>
        </TABLE>
<%Response.Write(mobjValues.BeginPageButton)
'UPGRADE_NOTE: Object mobjClaimThird may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjClaimThird = Nothing
'UPGRADE_NOTE: Object mobjValues may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjValues = Nothing
%>
</FORM>
</BODY>
</HTML>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 11.33.47
Call mobjNetFrameWork.FinishPage("si023")
'UPGRADE_NOTE: Object mobjNetFrameWork may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




