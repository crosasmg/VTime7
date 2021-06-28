<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">
'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
'- Objeto para el manejo de las rutinas genéricas
Dim mobjMenu As eFunctions.Menues


</script>
<%
Response.Expires = -1
mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues
%>


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></SCRIPT>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">
<%
With Response
	.Write(mobjValues.StyleSheet() & vbCrLf)
	.Write(mobjMenu.MakeMenu("BC006", "BC006_k.aspx", 1, ""))
End With
mobjMenu = Nothing
%>
<SCRIPT>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 2 $|$$Date: 7/11/03 17:08 $|$$Author: Nvaplat26 $"
</SCRIPT>
<SCRIPT>
//% insStateZone: se controla el estado de los controles de la página
//-----------------------------------------------------------------------------
function insStateZone(){
//-----------------------------------------------------------------------------
var lintIndex;
var error;
var nActions = new TypeActions();
var nMainAction = top.frames["fraSequence"].plngMainAction;
	with (self.document.forms[0]){
		cbeBranch.disabled=false
		tcnPolicy.disabled=false
		tcnCertif.disabled = false
		valProduct.disabled = false
	}
}
//% insStateZone: se controla el estado de los controles de la página
//-----------------------------------------------------------------------------
function insCancel(){
//-----------------------------------------------------------------------------
return true;
}
function insFinish(){
    return true;
}
//% InsChangeField: se controla el cambio de valor de los campos de la página
//--------------------------------------------------------------------------------------------
function InsChangeField(sField, sValue){
//--------------------------------------------------------------------------------------------
	with (self.document.forms[0]){
		switch (sField){
			case 'Branch':
			    tcnPolicy.value="";
			    tcnCertif.value="";
				break;
			case 'Product':
			    tcnPolicy.value="";
			    tcnCertif.value="";
				break;
		}
	}
}
//%insChangePolicy : Valida si la póliza es individual para deshabilitar el certificado
//------------------------------------------------------------------------------------------
function insChangePolicy(Form, sCodispl, sFrame){
//------------------------------------------------------------------------------------------
	if (typeof(sCodispl) == 'undefined' ) sCodispl = '';
	if (typeof(sFrame) == 'undefined' ) sFrame = 'fraHeader';
	with (Form){
		if (tcnPolicy.value != '' &&
		    tcnPolicy.value != hddnPolicy.value){
		    insDefValues('insValsPolitype', 'nBranch=' + cbeBranch.value +
		                                    '&nProduct=' + valProduct.value +
		                                    '&nPolicy=' + tcnPolicy.value +
		                                    '&sCodispl=' + sCodispl +
		                                    '&sFrame=' + sFrame);
			hddnPolicy.value = tcnPolicy.value;
		}
	}
}
</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<TD><BR></TD>
<TD><BR></TD>
<FORM METHOD="POST" ID="FORM" NAME="BC006" ACTION="valClient.aspx?x=1">
    <TABLE WIDTH="100%" BORDER=0>
        <TR>
            <TD WIDTH="11%"><LABEL ID=13848><%= GetLocalResourceObject("cbeBranchCaption") %></LABEL></TD>
            <TD WIDTH="34%"><%=mobjValues.BranchControl("cbeBranch", GetLocalResourceObject("cbeBranchToolTip"),  , "valProduct",  ,  ,  , "InsChangeField(""Branch"",this.value)", True)%></TD>
            <TD WIDTH="15%"><LABEL ID=13852><%= GetLocalResourceObject("valProductCaption") %></LABEL></TD>
            <TD WIDTH="40%"><%=mobjValues.ProductControl("valProduct", GetLocalResourceObject("valProductToolTip"),  ,  , True,  ,  ,  ,  , "InsChangeField(""Branch"",this.value)")%></TD>
        </TR>
        <TR>
            <TD><LABEL ID=13851><%= GetLocalResourceObject("tcnPolicyCaption") %></LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnPolicy", 10, CStr(eRemoteDB.Constants.intNull),  , GetLocalResourceObject("tcnPolicyToolTip"),  , 0,  ,  ,  , "insChangePolicy(self.document.forms[0], 'VI806_K', 'fraHeader');", True)%></TD>
    	                    <%Response.Write(mobjValues.HiddenControl("hddnPolicy", vbNullString))%>
            <TD><LABEL ID=13849><%= GetLocalResourceObject("tcnCertifCaption") %></LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnCertif", 10, Session("nCertif"),  , GetLocalResourceObject("tcnCertifToolTip"),  , 0,  ,  ,  ,  , True)%></TD>
        </TR>
    </TABLE>
</FORM>
</BODY>
</HTML>





