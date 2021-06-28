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
Call mobjNetFrameWork.BeginPage("CA088_k")

mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.31.19
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility
mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.31.19
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "CA088_k"
%>
<SCRIPT> 
//- Variable para el control de versiones 
    document.VssVersion="$$Revision: 1 $|$$Date: 2/09/03 19:03 $|$$Author: Iusr_llanquihue $"  

//% insCancel: Función que se ejecuta al ejecutar la transacción
//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
	return true;
}

//% insStateZone: se controla el estado de los campos de la página
//------------------------------------------------------------------------------------------
function insStateZone(){
//------------------------------------------------------------------------------------------      
}
</SCRIPT>
<HTML>
<HEAD>


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></SCRIPT>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">
	<%With Response
	.Write(mobjValues.StyleSheet() & vbCrLf)
	.Write(mobjMenu.MakeMenu("CA088", "CA088_K.aspx", 1, Request.QueryString.Item("sWindowDescript"), Session("sDesMultiCompany"), Session("sSche_code")))
End With
mobjMenu = Nothing
%>
<SCRIPT>
//% InsShowCertificat: Habilita o deshabilita el campo del número del certificado 
//-----------------------------------------------------------------------------
function InsShowCertificat(Field){
//-----------------------------------------------------------------------------
	with (self.document.forms[0]){
		if (Field.name=="tcnPolicy"){
			if (cbeBranch.value != "" && Field.value != ""){
				insDefValues("insValsPolitype","nBranch=" + self.document.forms[0].cbeBranch.value + "&nProduct=" + self.document.forms[0].valProduct.value + "&nPolicy=" + Field.value + "&sCodispl=" + "CA088_K" + "&sFrame=");
			}
			else{
				tcnCertif.disabled = false
				tcnCertif.value = ""
			}
		}
		else{
			if (cbeBranch.value != "" && tcnPolicy.value!="" && Field.value != "" && Field.value!="0"){
				insDefValues("Certificat","sCertype=2" + "&nBranch=" + cbeBranch.value + "&nProduct=" + valProduct.value + "&nPolicy=" + tcnPolicy.value + "&nCertif=" + Field.value + "&sCodispl=" + "CA088_K" + "&sFrame=");
			}
		}
	}
}
</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="frmManReceiptk" ACTION="ValPolicyTra.aspx?sMode=1">
    <BR><BR>
    <TABLE WIDTH="100%">
		<TR>
			<TD WIDTH="10%"><LABEL ID=13764><%= GetLocalResourceObject("cbeBranchCaption") %></LABEL></TD>
			<TD WIDTH="35%"><%=mobjValues.BranchControl("cbeBranch", GetLocalResourceObject("cbeBranchToolTip"), CStr(2), "valProduct")%></TD>
			<TD WIDTH="15%"><LABEL ID=13771><%= GetLocalResourceObject("valProductCaption") %></LABEL></TD>
			<TD WIDTH="40%"><%=mobjValues.ProductControl("valProduct", GetLocalResourceObject("valProductToolTip"), CStr(eRemoteDB.Constants.intNull), eFunctions.Values.eValuesType.clngWindowType, True, vbNullString)%></TD>
        </TR>
        <TR>
            <TD><LABEL ID=13769><%= GetLocalResourceObject("tcnPolicyCaption") %></LABEL></TD>
			<TD><%=mobjValues.NumericControl("tcnPolicy", 10, "",  , GetLocalResourceObject("tcnPolicyToolTip"),  ,  ,  ,  ,  , "InsShowCertificat(this);")%></TD>
			<TD><LABEL ID=13766><%= GetLocalResourceObject("tcnCertifCaption") %></LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnCertif", 10, "0",  , GetLocalResourceObject("tcnCertifToolTip"),  ,  ,  ,  ,  , "InsShowCertificat(this);", True)%></TD>
        </TR>
        <TR>
        <TD><LABEL ID=0><%= GetLocalResourceObject("cbeStatus_polCaption") %></LABEL></TD>
        <TD><%=mobjValues.PossiblesValues("cbeStatus_pol", "table181", eFunctions.Values.eValuesType.clngComboType,  , False,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeStatus_polToolTip"))%></TD>
        <TD><LABEL ID=0><%= GetLocalResourceObject("tcdDate_originCaption") %></LABEL></TD>
        <TD><%=mobjValues.DateControl("tcdDate_origin",  ,  , GetLocalResourceObject("tcdDate_originToolTip"),  ,  ,  ,  , True)%></TD>
        </TR>
	</TABLE>
<%
mobjValues = Nothing%>
</FORM>
</BODY>
</HTML>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 10.31.19
Call mobjNetFrameWork.FinishPage("CA088_k")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




