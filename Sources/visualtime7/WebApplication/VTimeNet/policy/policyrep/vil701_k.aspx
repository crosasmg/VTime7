<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 10.28.05
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
Call mobjNetFrameWork.BeginPage("vil701_k")

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.28.05
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "vil701_k"
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.28.05
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
    document.VssVersion="$$Revision: 2 $|$$Date: 20/04/04 13:08 $|$$Author: Nvaplat53 $"

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

//% insChargeProduct: Se cargan los parámetros del campo producto.
//------------------------------------------------------------------------------------------
function insChargeProduct(lobject){
//------------------------------------------------------------------------------------------
	if (lobject.value!=0) {
		with(self.document.forms[0]){
			valProduct.disabled=false;
			btnvalProduct.disabled=false;
			valProduct.value="";
			UpdateDiv("valProductDesc", "")
			tcnPolicy.value = "";
			tcnCertif.value = "";
		}
    }
}
//% insEnabledPolicy(): Permite habilitar e inhabilitar el campo Póliza.
//------------------------------------------------------------------------------------------
function insEnabledPolicy(lobject){
//------------------------------------------------------------------------------------------
	if (lobject.value) 
		self.document.forms[0].tcnPolicy.disabled=false;
    else{
        with(self.document.forms[0]){
			tcnPolicy.disabled=true;
			tcnPolicy.value="";
        }
    }
}
//% Llama a la página ShowValues con el valor "ShowCertif" para habilitar o inhabilitar el campo nCertif
//-------------------------------------------------------------------------------------------
function insShowValues(sField){
//-------------------------------------------------------------------------------------------
	var ltype;
	with(self.document.forms[0]){
		switch(sField){
			case "Policy":
				if (cbeBranch.value!="0" &&
				    valProduct.value!="" &&
				    tcnPolicy.value!=""){
//+ Si es una cotización				    
				    if (optType[0].checked) ltype= '3'
//+ Si es una propuesta				    
				    if (optType[1].checked) ltype= '1'
//+ Si es una póliza
				    if (optType[2].checked) ltype= '2'
				    insDefValues("ShowCertif", "sCertype=" + ltype + "&nBranch=" + cbeBranch.value + "&nProduct=" + valProduct.value + "&nPolicy=" + tcnPolicy.value)
				}
				break;
		}
	}
}
</SCRIPT>
	<%
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write(mobjValues.StyleSheet())
	Response.Write(mobjMenu.MakeMenu("VIL701", "VIL701_K.aspx", 1, Request.QueryString.Item("sWindowDescript"), Session("sDesMultiCompany"), Session("sSche_code")))
	Response.Write(mobjMenu.setZone(1, "VIL701", Request.QueryString.Item("sWindowDescript"), CShort(Request.QueryString.Item("nWindowTy"))))
	
	mobjMenu = Nothing
	Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
End If
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<BR><BR>
<%Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript")))%>
<FORM METHOD="POST" NAME="VIL701" ACTION="valPolicyRep.aspx?sMode=2">
    <TABLE WIDTH="100%">
	    <TR>
		    <TD COLSPAN="2" CLASS="HIGHLIGHTED"><LABEL ID=41007><%= GetLocalResourceObject("AnchorCaption") %></LABEL></TD>
			<TD WIDTH=5%>&nbsp;</TD>
			<TD> <LABEL ID=41208><%= GetLocalResourceObject("cbeBranchCaption") %></LABEL> </TD>
			<TD> <%=mobjValues.BranchControl("cbeBranch", GetLocalResourceObject("cbeBranchToolTip"), vbNullString, "valProduct",  ,  ,  , "insChargeProduct(this)",  , 3)%></TD>
	    </TR>
		<TR>
            <TD COLSPAN="2" CLASS="HORLINE"></TD>
			<TD COLSPAN="3"></TD>
        </TR>
		<TR>
			<TD COLSPAN="2"><%=mobjValues.OptionControl(0, "optType", GetLocalResourceObject("optType_CStr3Caption"), CStr(0), CStr(3),  ,  , 0, GetLocalResourceObject("optType_CStr3ToolTip"))%></TD>
			<TD>&nbsp;</TD>
			<TD><LABEL ID=40011><%= GetLocalResourceObject("valProductCaption") %></LABEL></TD>
			<TD><%=mobjValues.ProductControl("valProduct", GetLocalResourceObject("valProductToolTip"), CStr(0), eFunctions.Values.eValuesType.clngWindowType, True, vbNullString,  ,  ,  , "insEnabledPolicy(this)", 4)%></TD>
        </TR>
		<TR>
            <TD COLSPAN="2"><%=mobjValues.OptionControl(0, "optType", GetLocalResourceObject("optType_CStr1Caption"), CStr(1), CStr(1),  ,  , 1, GetLocalResourceObject("optType_CStr1ToolTip"))%></TD>
            <TD>&nbsp;</TD>
            <TD><LABEL ID=0><%= GetLocalResourceObject("cbeOfficeCaption") %></LABEL></TD>
			<TD> <%=mobjValues.PossiblesValues("cbeOffice", "Table9", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeOfficeToolTip"))%></TD>
		</TR>
		<TR>
            <TD COLSPAN="2"><%=mobjValues.OptionControl(0, "optType", GetLocalResourceObject("optType_CStr2Caption"), CStr(1), CStr(2),  ,  , 2, GetLocalResourceObject("optType_CStr2ToolTip"))%></TD>
            <TD>&nbsp;</TD>
			<TD> <LABEL ID=40281><%= GetLocalResourceObject("tcnPolicyCaption") %></LABEL> </TD>
			<TD> <%=mobjValues.NumericControl("tcnPolicy", 10, vbNullString,  , GetLocalResourceObject("tcnPolicyToolTip"),  , 0,  ,  ,  , "insShowValues(""Policy"")", True, 5)%></TD>
		</TR>
		<TR>
            <TD COLSPAN="2"></TD>
            <TD>&nbsp;</TD>
		    <TD> <LABEL ID=41370><%= GetLocalResourceObject("tcnCertifCaption") %></LABEL> </TD>
			<TD> <%=mobjValues.NumericControl("tcnCertif", 10, vbNullString,  , GetLocalResourceObject("tcnCertifToolTip"),  , 0,  ,  ,  ,  , True, 6)%>
		</TR>
		<TR>
		    <TD COLSPAN="3"> </TD>
			<TD><LABEL ID=41370><%= GetLocalResourceObject("tcdEffecdatestartCaption") %></LABEL> </TD>
			<TD><%=mobjValues.DateControl("tcdEffecdatestart",  ,  , GetLocalResourceObject("tcdEffecdatestartToolTip"),  ,  ,  ,  ,  , 7)%>
		</TR>
		<TR>
		    <TD COLSPAN="3"> </TD>
			<TD><LABEL ID=41371><%= GetLocalResourceObject("tcdEffecdateendCaption") %></LABEL> </TD>
<TD><% %>
<%=mobjValues.DateControl("tcdEffecdateend", CStr(Today),  , GetLocalResourceObject("tcdEffecdateendToolTip"),  ,  ,  ,  ,  , 7)%>
		</TR>
		<TR>
		    <TD COLSPAN="3"> </TD>		
            <TD><LABEL ID=0><%= GetLocalResourceObject("cbnStatus_valCaption") %></LABEL></TD>
			<TD> <%=mobjValues.PossiblesValues("cbnStatus_val", "Table5572", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbnStatus_valToolTip"))%></TD>
		</TR>
	</TABLE>
</FORM>
</BODY>
</HTML>
<%
mobjValues = Nothing%>

<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 10.28.05
Call mobjNetFrameWork.FinishPage("vil701_k")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




