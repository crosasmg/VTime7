<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 10.31.19
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues


</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("ca032_k")

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.31.19
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "ca032_k"
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.31.19
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility
%>
<HTML>
<HEAD>
<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></SCRIPT>
<%
With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjMenu.MakeMenu("CA032", "CA032_K.aspx", 1, Request.QueryString.Item("sWindowDescript"), Session("sDesMultiCompany"), Session("sSche_code")))
End With
mobjMenu = Nothing
%>
<SCRIPT>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 1 $|$$Date: 2/09/03 19:03 $|$$Author: Iusr_llanquihue $"
   
function insStateZone(){
//------------------------------------------------------------------------------------------
}

//% insCancel: Ejecuta rutinas necesarias en el momento de cancelar la página
//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
   return (true);
}

//%insChangePolicy : Valida si la póliza es individual para deshabilitar el certificado
//------------------------------------------------------------------------------------------
function insChangePolicy(){
//------------------------------------------------------------------------------------------
	with (self.document.forms[0]){
		if (tcnPolicy.value != '' &&
		    tcnPolicy.value != hddnPolicy.value){
		    insDefValues('insValsPolitype', 'nBranch=' + cbeBranch.value +
		                                    '&nProduct=' + valProduct.value +
		                                    '&nPolicy=' + tcnPolicy.value + 
		                                    '&sFindCliename=2');
			hddnPolicy.value = tcnPolicy.value;
		}
	}
} 
</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="frmReverseAmeRen" ACTION="ValPolicyTra.aspx?sTime=1">
	<BR>
    <TABLE WIDTH="100%">
        <TR>
            <TD WIDTH="100%" COLSPAN="4">&nbsp;</TD>
        </TR>
        <TR>
            <TD><LABEL ID=13791><%= GetLocalResourceObject("cbeBranchCaption") %></LABEL></TD>
            <TD><%=mobjValues.BranchControl("cbeBranch", GetLocalResourceObject("cbeBranchToolTip"))%></TD>
			<TD><LABEL ID=13804><%= GetLocalResourceObject("valProductCaption") %></LABEL></TD>
            <TD><%=mobjValues.ProductControl("valProduct", GetLocalResourceObject("valProductToolTip"))%></TD>
		</TR>            
		<TR>
            <TD><LABEL ID=13803><%= GetLocalResourceObject("tcnPolicyCaption") %></LABEL></TD>
            <TD>
            <%
Response.Write(mobjValues.PolicyControl("tcnPolicy", GetLocalResourceObject("tcnPolicyToolTip"), "cbeBranch", CStr(0), "valProduct", CStr(0),  ,  ,  ,  ,  ,  ,  , "insChangePolicy();",  ,  , False))
'                Response.Write mobjvalues.NumericControl("tcnPolicy", 10, vbNullString,, GetLocalResourceObject("tcnPolicyToolTip"),,,,,, "insChangePolicy();")
Response.Write(mobjValues.HiddenControl("hddnPolicy", vbNullString))
%>
            </TD>
			<TD><LABEL ID=13792><%= GetLocalResourceObject("tcnCertifCaption") %></LABEL></TD>
			<TD><%=mobjValues.NumericControl("tcnCertif", 10, vbNullString,  , GetLocalResourceObject("tcnCertifToolTip"),  , 0)%></TD>
        </TR>
    </TABLE>
</FORM>
</BODY>
</HTML>
<%
mobjValues = Nothing%> 
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 10.31.19
Call mobjNetFrameWork.FinishPage("ca032_k")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




