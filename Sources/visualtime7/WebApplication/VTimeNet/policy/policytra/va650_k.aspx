<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 10.31.23
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo del menú
Dim mobjMenu As eFunctions.Menues


</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("va650_k")

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.31.23
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "va650_k"
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.31.23
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility
%>
<HTML>
<HEAD>
<SCRIPT LANGUAGE=JavaScript>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 1 $|$$Date: 2/09/03 19:04 $|$$Author: Iusr_llanquihue $"

//% insStateZone: se controla el estado de los campos de la página
//--------------------------------------------------------------------------------------------
function insStateZone(){
//--------------------------------------------------------------------------------------------
}

//% insCancel: se controla la acción Cancelar de la página
//--------------------------------------------------------------------------------------------
function insCancel(){
//--------------------------------------------------------------------------------------------
    return true;
}

//% insFinish: se controla la acción Cancelar de la página
//--------------------------------------------------------------------------------------------
function insFinish(){
//--------------------------------------------------------------------------------------------
    return true;
}

//%InsChangePolicy: Valida si la póliza es individual para deshabilitar el campo certificado
//--------------------------------------------------------------------------------------------
function InsChangePolicy(){
//--------------------------------------------------------------------------------------------
	with (self.document.forms[0]){
		if (tcnPolicy.value != hddnPolicy.value){
			hddnPolicy.value = tcnPolicy.value;
			insDefValues('Policy_VA650', 'nBranch='  + cbeBranch.value  +
			                             '&nProduct=' + valProduct.value +
			                             '&nPolicy=' + tcnPolicy.value + 
			                             '&sCodispl=VA650_K');
		}
		else{
			insDefValues('Account_Pol', 'nBranch='  + cbeBranch.value  +
										'&nProduct=' + valProduct.value +
										'&nPolicy=' + tcnPolicy.value +
										'&nCertif=' + tcnCertif.value);
		}
	}
}
//%InsChangeCertif: Asigna fecha de última modificación de la cuenta de valor poliza
//--------------------------------------------------------------------------------------------
function InsChangeCertif(){
//--------------------------------------------------------------------------------------------
	with (self.document.forms[0]){
		if (tcnCertif.value != ""){
			insDefValues('Account_Pol', 'nBranch='  + cbeBranch.value  +
										'&nProduct=' + valProduct.value +
										'&nPolicy=' + tcnPolicy.value +
										'&nCertif=' + tcnCertif.value);
		}
	}
}
</SCRIPT>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></SCRIPT>        


<%
Response.Write(mobjValues.StyleSheet())
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write(mobjMenu.MakeMenu("VA650", "VA650_K.aspx", 1, vbNullString))
	mobjMenu = Nothing
	Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
End If
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<BR></BR>
<FORM METHOD="POST" NAME="VA650_K" ACTION="valPolicyTra.aspx?sMode=2">
    <TABLE WIDTH="100%">
        <TR>
            <TD><LABEL ID=0><%= GetLocalResourceObject("cbeBranchCaption") %></LABEL></TD>
            <TD><%=mobjValues.BranchControl("cbeBranch", GetLocalResourceObject("cbeBranchToolTip"))%></TD>
            <TD><LABEL ID=0><%= GetLocalResourceObject("valProductCaption") %></LABEL></TD>
            <TD><%=mobjValues.ProductControl("valProduct", GetLocalResourceObject("valProductToolTip"), vbNullString, eFunctions.Values.eValuesType.clngWindowType,  ,  ,  ,  ,  ,  ,  ,  ,  , eFunctions.Values.eProdClass.clngActiveLife)%></TD>
        </TR>
        <TR>
            <TD><LABEL ID=13976><%= GetLocalResourceObject("tcnPolicyCaption") %></LABEL></TD>
            <TD>
            <%
Response.Write(mobjValues.NumericControl("tcnPolicy", 10, vbNullString,  , GetLocalResourceObject("tcnPolicyToolTip"),  ,  ,  ,  ,  , "InsChangePolicy();"))
Response.Write(mobjValues.HiddenControl("hddnPolicy", "-1"))
%>
			</TD>
            <TD><LABEL ID=13970><%= GetLocalResourceObject("tcnCertifCaption") %></LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnCertif", 10, "0",  , GetLocalResourceObject("tcnCertifToolTip"),  ,  ,  ,  ,  , "InsChangeCertif();")%></TD>
        </TR>
        <TR>
            <TD><LABEL ID=0><%= GetLocalResourceObject("tcdEffecdateCaption") %></LABEL></TD>
<TD><% %>
<%=mobjValues.DateControl("tcdEffecdate", CStr(Today),  , GetLocalResourceObject("tcdEffecdateToolTip"))%></TD>
        </TR>
        <TR>
            <TD><LABEL ID=0><%= GetLocalResourceObject("AnchorCaption") %></LABEL></TD>
            <TD><%=mobjValues.OptionControl(0, "optMovType", GetLocalResourceObject("optMovType_1Caption"), CStr(1), "1",  ,  ,  , GetLocalResourceObject("optMovType_1ToolTip"))%></TD>
            <TD><%=mobjValues.OptionControl(0, "optMovType", GetLocalResourceObject("optMovType_2Caption"), CStr(2), "2",  ,  ,  , GetLocalResourceObject("optMovType_2ToolTip"))%></TD>
        </TR>
    </TABLE>
</FORM> 
</BODY>
</HTML>

<%
mobjValues = Nothing%> 

<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 10.31.23
Call mobjNetFrameWork.FinishPage("va650_k")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




