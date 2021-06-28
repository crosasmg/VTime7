<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 10.27.21
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo del menú
Dim mobjMenu As eFunctions.Menues


</script>
<%
Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("vic732_k")

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.27.21
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "vic732_k"
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.27.21
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility
%>
<HTML>
<HEAD>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></SCRIPT>		

<SCRIPT LANGUAGE=JavaScript>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 1 $|$$Date: 2/09/03 19:03 $|$$Author: Iusr_llanquihue $"

//% insStateZone: se controla el estado de los campos de la página
//--------------------------------------------------------------------------------------------
function insStateZone(){
//--------------------------------------------------------------------------------------------
	with (self.document.forms[0]){
		cbeBranch.disabled = false;
		tcnPolicy.disabled = false;
		tcdEffecdate.disabled = false;
		btn_tcdEffecdate.disabled = false;
	}
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
//% insChange: se controla el cambio de valor de los campos de la página
//--------------------------------------------------------------------------------------------
function insChange(){
//--------------------------------------------------------------------------------------------
	with(self.document.forms[0]){
		if(cbeBranch.value!='' &&
		   valProduct.value!='' &&
		   tcnPolicy.value!='')
//+ Se busca el tipo de póliza para habilitar/deshabilitar el campo "Certificado"
			insDefValues('insValPolitype', 'sCertype=2&nBranch=' + cbeBranch.value + '&nProduct=' + valProduct.value + '&nPolicy=' + tcnPolicy.value, '/VTimeNet/Policy/PolicyQue')
	}
}
</SCRIPT>
<%
Response.Write(mobjValues.StyleSheet())
Response.Write(mobjMenu.MakeMenu("VIC732", "VIC732_K.aspx", 1, vbNullString))
mobjMenu = Nothing
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" NAME="VIC732_K" ACTION="valPolicyQue.aspx?sMode=2">
    <BR><BR>
    <TABLE WIDTH="100%">
        <TR>
			<TD WIDTH="20%"><LABEL ID=0><%= GetLocalResourceObject("cbeBranchCaption") %></LABEL></TD>
			<TD WIDTH="30%"><%=mobjValues.BranchControl("cbeBranch", GetLocalResourceObject("cbeBranchToolTip"), vbNullString,  ,  ,  ,  , "insChange()", True)%></TD>
			<TD WIDTH="15%"><LABEL ID=0><%= GetLocalResourceObject("valProductCaption") %></LABEL></TD>
            <TD><%=mobjValues.ProductControl("valProduct", GetLocalResourceObject("valProductToolTip"), CStr(eRemoteDB.Constants.intNull), eFunctions.Values.eValuesType.clngWindowType, True,  ,  ,  ,  , "insChange()")%></TD>
        </TR>
        <TR>
        	<TD><LABEL ID=0><%= GetLocalResourceObject("tcnPolicyCaption") %></LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnPolicy", 10, vbNullString,  , GetLocalResourceObject("tcnPolicyToolTip"), False,  ,  ,  ,  , "insChange()", True)%> </TD>
			<TD><LABEL ID=0><%= GetLocalResourceObject("tcnCertifCaption") %></LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnCertif", 10, CStr(0),  , GetLocalResourceObject("tcnCertifToolTip"), False,  ,  ,  ,  ,  , True)%> </TD>
        </TR>
		<TR>
			<TD><LABEL ID=0><%= GetLocalResourceObject("tcdEffecdateCaption") %></LABEL></TD>
<TD COLSPAN="3"><% %>
<%=mobjValues.DateControl("tcdEffecdate", CStr(Today),  , GetLocalResourceObject("tcdEffecdateToolTip"),  ,  ,  ,  , True)%></TD>
        </TR>
    </TABLE>
</FORM> 
</BODY>
</HTML>
<%
mobjValues = Nothing
%>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 10.27.21
Call mobjNetFrameWork.FinishPage("vic732_k")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




