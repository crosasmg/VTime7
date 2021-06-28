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

'- Variables para almacenar parametros
Dim mstrBranch As String
Dim mstrProduct As String
Dim mstrPolicy As String
Dim mstrCertif As String
Dim mstrStartdate As String
Dim mhddPolicy As String
Dim mstrTransaction As String


</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("cac011_k")

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.27.21
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "cac011_k"
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.27.21
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility
mobjValues.ActionQuery = CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401

'+ Se cargan datos de parametros
With Request
	If .QueryString.Item("sCodisplOrig") = "CAC001" Then
		mstrPolicy = .QueryString.Item("nProponum")
		If Request.QueryString.Item("sCertype") <> "1" And Request.QueryString.Item("sCertype") <> "3" Then
			mhddPolicy = .QueryString.Item("nPolicy")
		End If
	Else
		mstrPolicy = .QueryString.Item("nPolicy")
		mhddPolicy = .QueryString.Item("nPolicy")
	End If
	mstrTransaction = .QueryString.Item("nTransaction")
	mstrBranch = .QueryString.Item("nBranch")
	mstrProduct = .QueryString.Item("nProduct")
	mstrCertif = .QueryString.Item("nCertif")
	mstrStartdate = .QueryString.Item("dStartdate")
End With
%>
<HTML>
<HEAD>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></SCRIPT>
<SCRIPT LANGUAGE=JavaScript>
//- Variable para el control de versiones
       document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 16:37 $"

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
//%InsChangeLabel: Cambia el label de poliza/certificado a propuesta/certificado
//--------------------------------------------------------------------------------------------
function InsChangeLabel(Value){
//--------------------------------------------------------------------------------------------
	if (Value==2 || Value==3 || Value==4 || Value==5){
		UpdateDiv('lblPolizaPropuesta','Póliza/Certificado');
	}
	else{
		UpdateDiv('lblPolizaPropuesta','Propuesta/Certificado');
	}
	document.forms[0].hddTransaction.value = 8;
}
//%InsChangeField: Asigna los valores de los parametros del valor posible de Tipo de endoso
//--------------------------------------------------------------------------------------------
function InsChangeField(){
//--------------------------------------------------------------------------------------------
	with(self.document.forms[0]){
		valTypeAmend.Parameters.Param1.sValue = cbeBranch.value;
		valTypeAmend.Parameters.Param2.sValue = valProduct.value;
		if (tcdEffecdate.value == "")
			valTypeAmend.Parameters.Param3.sValue = hddeffecdate.value;
		else
			valTypeAmend.Parameters.Param3.sValue = tcdEffecdate.value;
		valTypeAmend.disabled = (valTypeAmend.Parameters.Param1.sValue == '' ||
		                         valTypeAmend.Parameters.Param2.sValue == '0')
		btnvalTypeAmend.disabled = valTypeAmend.disabled;
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
	Response.Write(mobjMenu.MakeMenu(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sCodispl") & ".aspx", 1, Request.QueryString.Item("sWindowDescript"), Session("sDesMultiCompany"), Session("sSche_code")))
	mobjMenu = Nothing
	Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
End If
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<BR><BR>
<FORM METHOD="POST" NAME="frmPolicyHisQ" ACTION="ValPolicyQue.aspx?sMode=2">
    <TABLE WIDTH="100%">
        <TR>
			<TD><LABEL ID=0><%= GetLocalResourceObject("cbeCertypeCaption") %></LABEL></TD>
			<TD><%=mobjValues.ComboControl("cbeCertype", "1|Propuesta,2|Póliza,3|Cotización,4|Cotización de modificación,5|Cotización de renovación,6|Propuesta de modificación,7|Propuesta de renovación,8|Propuestas especiales", Request.QueryString.Item("sCertype"),  ,  ,  , "InsChangeLabel(this.value);")%></TD>
			<TD>&nbsp;</TD>
			<TD><LABEL ID=0><%= GetLocalResourceObject("tcdEffecdateCaption") %></LABEL></TD>
            <TD><%=mobjValues.DateControl("tcdEffecdate", mstrStartdate,  , GetLocalResourceObject("tcdEffecdateToolTip"),  ,  ,  , "InsChangeField();")%></TD>
        </TR>
        <TR>
			<TD><LABEL ID=0><%= GetLocalResourceObject("cbeBranchCaption") %></LABEL></TD>
            <TD><%=mobjValues.BranchControl("cbeBranch", GetLocalResourceObject("cbeBranchToolTip"), mstrBranch,  ,  ,  ,  , "InsChangeField();")%></TD>
			<TD>&nbsp;</TD>
			<TD><LABEL ID=0><%= GetLocalResourceObject("valProductCaption") %></LABEL></TD>
            <TD><%=mobjValues.ProductControl("valProduct", GetLocalResourceObject("valProductToolTip"), mstrBranch,  ,  , mstrProduct,  ,  ,  , "InsChangeField();")%></TD>
        </TR>
		<TR>
			<TD><LABEL><DIV ID="lblPolizaPropuesta"><%= GetLocalResourceObject("tcnPolicyCaption") %></DIV></LABEL></TD>
			<TD><%=mobjValues.NumericControl("tcnPolicy", 9, mstrPolicy,  , GetLocalResourceObject("tcnPolicyToolTip"), False, 0,  ,  ,  , "ShowPoliza()")%> / <%=mobjValues.NumericControl("tcnCertif", 9, mstrCertif,  , "Número de certificado  que se desea consultar", False, 0)%></TD>
			<TD>&nbsp;</TD>
			<TD><LABEL ID=0><%= GetLocalResourceObject("valTypeAmendCaption") %></LABEL></TD>
			<TD><%
With mobjValues
	.Parameters.Add("nBranch", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Parameters.Add("nProduct", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Parameters.Add("dEffecdate", Today, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	Response.Write(mobjValues.PossiblesValues("valTypeAmend", "tabtype_amend", eFunctions.Values.eValuesType.clngWindowType,  , True,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("valTypeAmendToolTip")))
End With
%>
			</TD>
		</TR>
    </TABLE>
<%=mobjValues.HiddenControl("hddeffecdate", CStr(Today))%>
    <%=mobjValues.HiddenControl("hddPolicy", mhddPolicy)%>
    <%=mobjValues.HiddenControl("hddTransaction", mstrTransaction)%>
</FORM> 
</BODY>
</HTML>
<%
'Se agrega validación para identificar si es llamada por navegacion o 
'desde el menú principal.
If Request.QueryString.Item("nPolicy") <> vbNullString And Request.QueryString.Item("nProponum") <> vbNullString Then
	Response.Write("<SCRIPT> ClientRequest(390,6); </script>")
End If

mobjValues = Nothing
%>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 10.27.21
Call mobjNetFrameWork.FinishPage("cac011_k")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




