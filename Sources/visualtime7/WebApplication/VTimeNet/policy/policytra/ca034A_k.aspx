<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="ePolicy" %>
<script language="VB" runat="Server">
'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 10.31.20
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility
Dim mobjNull_condi As ePolicy.Null_condi
Dim mstrCodisplOri As String
Dim mstrCertype As String


'% insPreCA034A: 
'---------------------------------------------------------------------------
Sub insPreCA034A()
	'---------------------------------------------------------------------------	
	With Request
		If IsNothing(.QueryString("sCodisplOri")) Then
			mstrCodisplOri = "CA034A"
		Else
			mstrCodisplOri = .QueryString.Item("sCodisplOri")
		End If
		
		Session("sCodispl") = mstrCodisplOri
		
		Call mobjNull_condi.insPreCA033_k(mstrCodisplOri, Session("nOperat"), mobjValues.StringToType(.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate))
	End With
End Sub

</script>
<%mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.31.20
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "CA034A"
Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("ca034A_k")
mobjNull_condi = New ePolicy.Null_condi
'+ Se hace carga inicial de datos
Call insPreCA034A()

%>
<HTML>
<HEAD>
<SCRIPT>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 12 $|$$Date: 13/10/04 12:12 $|$$Author: Nvaplat28 $"
</SCRIPT>
	<META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">
	<%=mobjValues.StyleSheet()%>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></script>


    <%
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.31.20
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility
Response.Write(mobjMenu.MakeMenu("CA034A", "CA034A_k.aspx", 1, Request.QueryString.Item("sWindowDescript"), Session("sDesMultiCompany"), Session("sSche_code")))
mobjMenu = Nothing
%>
<SCRIPT>

//- Variable para el control de versiones
	document.VssVersion="$$Revision: 12 $|$$Date: 13/10/04 12:12 $"
	
//% insCancel: se controla la acción Cancelar de la página
//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
	return true;
}
//%insStateZone: se controla el estado de los campos de la página
//------------------------------------------------------------------------------------------
function insStateZone(){
//------------------------------------------------------------------------------------------
}
//% insChangeField: Se recargan los valores cuando cambia el campo
//-------------------------------------------------------------------------------------------
function insChangeField(Field){
//-------------------------------------------------------------------------------------------    
	with (self.document.forms[0]){
		switch(Field.name){
			case "tcnPolicy":
				insDefValues("Policy_CA099","sCodispl=CA034A&nBranch=" + cbeBranch.value + "&nProduct=" + valProduct.value + "&nPolicy= " + tcnPolicy.value)
				break;
		}
	}
}

//% ShowChangeValues: Se habilitan/deshabilitan los controles de acuerdo a lo definido para
//%	producto, póliza o certificado
//-------------------------------------------------------------------------------------------
function ShowChangeValues(sField){
//-------------------------------------------------------------------------------------------
	with(self.document.forms[0]){
		switch(sField){
			case "Agency":
				if(cbeAgency.value!="")
				    insDefValues(sField, "nAgency=" + cbeAgency.value + "&nOfficeAgen=" + cbeOfficeAgen.value +"&nOffice=" + cbeOffice.value,'/VTimeNet/Policy/PolicySeq')
				break;
		}
	}
}
</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="frmReahPolicy_K" ACTION="ValPolicyTra.aspx?x=1&nProponum=<%=Request.QueryString.Item("npolicy")%>">
	<BR></BR>
	<TABLE WIDTH=100%>
		<TR>
			<TD WIDTH=25%><LABEL ID=13901><%= GetLocalResourceObject("cbeBranchCaption") %></LABEL></TD>
			<TD>
				<%If Request.QueryString.Item("sCertype") = vbNullString Then
	mstrCertype = "2"
Else
	mstrCertype = Request.QueryString.Item("sCertype")
End If
Response.Write(mobjValues.HiddenControl("tctCertype", mstrCertype))
Response.Write(mobjValues.BranchControl("cbeBranch", GetLocalResourceObject("cbeBranchToolTip"), Request.QueryString.Item("nBranch"), "valProduct"))%>
			</TD>
			<TD>&nbsp;</TD>
			<TD COLSPAN="2" CLASS="HighLighted"><LABEL ID=0><%= GetLocalResourceObject("AnchorCaption") %></LABEL></TD>
		</TR>
		<TR>
			<TD COLSPAN="3"></TD>
			<TD COLSPAN="2" CLASS="HorLine"></TD>
		</TR>
		<TR>
			<TD><LABEL ID=13909><%= GetLocalResourceObject("valProductCaption") %></LABEL></TD>
			<TD><%Response.Write(mobjValues.ProductControl("valProduct", GetLocalResourceObject("valProductToolTip"), Request.QueryString.Item("nBranch"), eFunctions.Values.eValuesType.clngWindowType,  , Request.QueryString.Item("nProduct")))%></TD>
			<TD>&nbsp;</TD>
			<TD COLSPAN="2"><%=mobjValues.OptionControl(0, "optProcess", GetLocalResourceObject("optProcess_1Caption"), CStr(1), "1")%></TD>
		</TR>
		<TR>
		    <TD><LABEL ID=13803><%= GetLocalResourceObject("tcnPolicyCaption") %></LABEL></TD>
			<TD><%Response.Write(mobjValues.NumericControl("tcnPolicy", 10, Request.QueryString.Item("nPolicy"),  , GetLocalResourceObject("tcnPolicyToolTip"),  , 0,  ,  ,  , "insChangeField(tcnPolicy);"))%></TD>
			<TD COLSPAN="3" CLASS="HighLighted"><LABEL ID=0><%= GetLocalResourceObject("Anchor2Caption") %></LABEL></TD>
		</TR>
		<TR>
			<TD COLSPAN="3"></TD>
			<TD COLSPAN="2" CLASS="HorLine"></TD>
		</TR>
		<TR>
			<TD><LABEL ID=0><%= GetLocalResourceObject("tcnCertifCaption") %></LABEL></TD>
			<TD><%Response.Write(mobjValues.NumericControl("tcnCertif", 10, CStr(0),  , GetLocalResourceObject("tcnCertifToolTip")))%></TD>
			<TD>&nbsp;</TD>
			<TD COLSPAN="2"><%=mobjValues.OptionControl(0, "optExecute", GetLocalResourceObject("optExecute_1Caption"), CStr(1), "1")%></TD>
		</TR>
		<TR>
		    <TD><LABEL ID=13803><%= GetLocalResourceObject("tcdNullDateCaption") %></LABEL></TD>
<TD COLSPAN="3"><% %>
<%=mobjValues.DateControl("tcdNullDate", CStr(Today),  , GetLocalResourceObject("tcdNullDateToolTip"))%></TD>
			<TD COLSPAN="2" CLASS="HighLighted"><LABEL ID=0></LABEL></TD>
		</TR>
		<TR>
			<TD COLSPAN="3"></TD>
			<TD COLSPAN="2"></TD>
		</TR>
		<TR>
		    <TD><LABEL ID=13378></LABEL></TD>
			<TD COLSPAN="2"></TD>
			<TD COLSPAN="2"></TD>
			<TD COLSPAN="2"></TD>	
		</TR>
		<TR>
		    <TD><LABEL ID=0></LABEL></TD>
			<TD COLSPAN="2"></TD>
			<TD COLSPAN="2"></TD>
			<TD COLSPAN="2"></TD>
		</TR>
		<TR>
		    <TD><LABEL ID=0></LABEL></TD>
			<TD COLSPAN="4"></TD>
		</TR>
	</TABLE>
<%
Response.Write(mobjValues.HiddenControl("hddCodisplOri", Request.QueryString.Item("sCodisplOri")))
%>
</FORM>
</BODY>
</HTML>
<%
mobjNull_condi = Nothing
mobjValues = Nothing
%>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 10.31.20
Call mobjNetFrameWork.FinishPage("ca034_k")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




