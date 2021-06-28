<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 10.31.19
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility
'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
'- Objeto para el manejo de las rutinas genéricas
Dim mobjMenu As eFunctions.Menues


</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("VI7700_k")
mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.31.19
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "VI7700_k"
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.31.19
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility
'+ Se hace carga inicial de datos
%>
<SCRIPT>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 1 $|$$Date: 8/10/03 19:16 $"

//% insCancel: Ejecuta rutinas necesarias en el momento de cancelar la página
//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
   return (true);
}

//% EnabledField: Habilita - deshabilita el campo CERTIFICADO
//------------------------------------------------------------------------------------------
function EnabledField(nValue)
//------------------------------------------------------------------------------------------
{
	insDefValues("nPolicy_APV", "sCertype=2&nBranch=" + self.document.forms[0].elements['cbeBranch'].value +
	                            "&nProduct=" + self.document.forms[0].elements['valProduct'].value +
	                            "&nPolicy=" + nValue, '/VTimeNet/Policy/PolicyTra'); 
}

</SCRIPT>
<HTML>
<HEAD>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></SCRIPT>

<%
With Response
	.Write(mobjValues.StyleSheet() & vbCrLf)
	.Write(mobjMenu.MakeMenu("VI7700", "VI7700_k.aspx", 1, ""))
End With
mobjMenu = Nothing
%>
<SCRIPT>
//%insStateZone: se controla el estado de los campos de la página
//------------------------------------------------------------------------------------------
function insStateZone(){
//------------------------------------------------------------------------------------------

	self.document.forms[0].elements['cbeBranch'].disabled = false;
	self.document.forms[0].elements['valProduct'].disabled = false;
	self.document.forms[0].elements['tcnPolicy'].disabled = false;
	self.document.forms[0].elements['tcnCertif'].disabled = false;

}

</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="VI7700" ACTION="ValPolicyTra.aspx?">
	<BR><BR>
    <TABLE WIDTH="100%">
        <TR>
            <TD WIDTH=20%><LABEL ID=13791><%= GetLocalResourceObject("cbeBranchCaption") %></LABEL></TD>
            <TD WIDTH=30%><%Response.Write(mobjValues.BranchControl("cbeBranch", GetLocalResourceObject("cbeBranchToolTip"), Request.QueryString.Item("nBranch"), "valProduct",  ,  ,  ,  , True))%></TD>
            <TD WIDTH=10%>&nbsp;</TD>
			<TD><LABEL ID=13804><%= GetLocalResourceObject("valProductCaption") %></LABEL></TD>
			<TD><%Response.Write(mobjValues.ProductControl("valProduct", GetLocalResourceObject("valProductToolTip"), Request.QueryString.Item("nBranch"), eFunctions.Values.eValuesType.clngWindowType,  , Request.QueryString.Item("nProduct")))%></TD>
		</TR>
		<TR>
            <TD><LABEL ID=13803><%= GetLocalResourceObject("tcnPolicyCaption") %></LABEL></TD>
            <TD>
				<%=mobjValues.NumericControl("tcnPolicy", 8, Request.QueryString.Item("nPolicy"),  , GetLocalResourceObject("tcnPolicyToolTip"),  , 0,  ,  ,  , "EnabledField(this.value);", True)%>
            </TD>
            <TD WIDTH=10%>&nbsp;</TD>
            <TD><LABEL ID=13803><%= GetLocalResourceObject("tcnCertifCaption") %></LABEL></TD>
            <TD> <%=mobjValues.NumericControl("tcnCertif", 4, Request.QueryString.Item("nCertif"),  , GetLocalResourceObject("tcnCertifToolTip"),  , 0,  ,  ,  ,  , True)%></TD>
		</TR>
    </TABLE>
</BODY>
</FORM>
</HTML>

<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 10.31.19
Call mobjNetFrameWork.FinishPage("VI7700_k")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer
%>





