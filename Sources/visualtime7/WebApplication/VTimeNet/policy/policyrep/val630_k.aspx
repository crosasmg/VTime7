<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 10.28.03
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
Call mobjNetFrameWork.BeginPage("val630_k")
'- Objeto para el manejo particular de los datos de la página
mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.28.03
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "val630_k"
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.28.03
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility
mobjValues.ActionQuery = CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401
%>
<HTML>
<HEAD>
<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></SCRIPT>
<SCRIPT LANGUAGE=JavaScript>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 3 $|$$Date: 5/08/04 17:12 $"
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
//% insEnabledPolicy(): Deshabilita los datos para la poliza cuando busqueda es masiva
//------------------------------------------------------------------------------------------
function insEnabledPolicy(Field){
//------------------------------------------------------------------------------------------
    var lblnMasive;
    lblnMasive=(Field.value==2);
	with(self.document.forms[0]){
		tcnPolicy.disabled=lblnMasive;
		tcnCertif.disabled=lblnMasive;
		cbeBranch.disabled=lblnMasive;
		valProduct.disabled=lblnMasive;
		btnvalProduct.disabled=lblnMasive;
		if(lblnMasive){
		    tcnPolicy.value='';
		    tcnCertif.value='';
		    cbeBranch.value='';
		    valProduct.value='';
		    UpdateDiv('valProductDesc', '');
		}
		else{
		   cbeBranch.value=6;
		   cbeBranch.disabled=true;
		}
    }
}
//% ShowPoliza: Se encarga de validar el tipo de Póliza
//--------------------------------------------------------------------------------------------
function ShowPoliza(){
//--------------------------------------------------------------------------------------------
	insDefValues('ValPolitype', "nBranch=" + self.document.forms[0].cbeBranch.value + "&nProduct=" + self.document.forms[0].valProduct.value + "&nPolicy=" + self.document.forms[0].tcnPolicy.value);
 }
</SCRIPT>
<%
Response.Write(mobjValues.StyleSheet())
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write(mobjMenu.MakeMenu("VAL630", "VAL630_K.aspx", 1, vbNullString))
	mobjMenu = Nothing
	Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
End If
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<BR>
<FORM METHOD="POST" NAME="VAL630" ACTION="valPolicyRep.aspx?sMode=2">
    <TABLE WIDTH="100%">
	<BR><BR>
	    <TR>
		    <TD WIDTH="5%"></TD>
			<TD COLSPAN=2 CLASS="HighLighted"><LABEL ID=0><A NAME="Proceso"><%= GetLocalResourceObject("AnchorProcesoCaption") %></A></LABEL></TD>
			<TD></TD>
			<TD></TD>
			<TD></TD>
	    </TR>
		<TR>
		    <TD></TD>
            <TD COLSPAN=2 CLASS="HORLINE"></TD>
			<TD></TD>
			<TD></TD>
			<TD></TD>
        </TR>
		<TR>
		    <TD></TD>
			<TD><%=mobjValues.OptionControl(0, "optType", GetLocalResourceObject("optType_CStr1Caption"), CStr(1), CStr(1), "insEnabledPolicy(this);")%></TD>
			<TD></TD>
			<TD><LABEL ID=41208><%= GetLocalResourceObject("cbeBranchCaption") %></LABEL></TD>
			<TD><%=mobjValues.BranchControl("cbeBranch", GetLocalResourceObject("cbeBranchToolTip"), CStr(6), vbNullString,  ,  ,  ,  , True)%></TD>
        </TR>
		<TR>
		    <TD></TD>
            <TD><%=mobjValues.OptionControl(0, "optType", GetLocalResourceObject("optType_CStr2Caption"), CStr(0), CStr(2), "insEnabledPolicy(this);")%></TD>
            <TD></TD>
			<TD><LABEL ID=40011><%= GetLocalResourceObject("valProductCaption") %></LABEL></TD>
			<TD><%=mobjValues.ProductControl("valProduct", GetLocalResourceObject("valProductToolTip"), CStr(6),  , False,  ,  ,  ,  ,  ,  ,  ,  , eFunctions.Values.eProdClass.clngActiveLife)%></TD>
		</TR>
		<TR>
		    <TD></TD>
            <TD></TD>
            <TD></TD>
			<TD><LABEL ID=40281><%= GetLocalResourceObject("tcnPolicyCaption") %></LABEL> </TD>
            <TD><%=mobjValues.NumericControl("tcnPolicy", 8, Request.QueryString.Item("nPolicy"),  , GetLocalResourceObject("tcnPolicyToolTip"),  , 0,  ,  ,  , "ShowPoliza()")%></TD>
		</TR>
		<TR>
			<TD></TD>
			<TD></TD>
			<TD></TD>
		    <TD><LABEL ID=41370><%= GetLocalResourceObject("tcnCertifCaption") %></LABEL> </TD>
            <TD> <%=mobjValues.NumericControl("tcnCertif", 4, Request.QueryString.Item("nCertif"),  , GetLocalResourceObject("tcnCertifToolTip"),  , 0)%></TD>
		</TR>
		<TR>
		    <TD></TD>
			<TD></TD>
			<TD></TD>
			<TD><LABEL ID=41370><%= GetLocalResourceObject("tcdStartDateCaption") %></LABEL></TD>
<TD><% %>
<%=mobjValues.DateControl("tcdStartDate", CStr(Today),  , GetLocalResourceObject("tcdStartDateToolTip"))%>
		</TR>
		<TR>
		    <TD></TD>
			<TD></TD>
			<TD></TD>
			<TD><LABEL ID=41370><%= GetLocalResourceObject("tcdEndDateCaption") %></LABEL></TD>
<TD><% %>
<%=mobjValues.DateControl("tcdEndDate", CStr(Today),  , GetLocalResourceObject("tcdEndDateToolTip"))%>
		</TR>
	</TABLE>
</FORM>
</BODY>
</HTML>
<%
mobjValues = Nothing%> 

<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 10.28.03
Call mobjNetFrameWork.FinishPage("val630_k")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




