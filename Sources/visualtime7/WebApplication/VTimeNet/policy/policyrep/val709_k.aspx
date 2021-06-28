<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 10.28.03
Dim mobjNetFrameWork As eNetFrameWork.Layout

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues
Dim mintBranch As String
Dim mintProduct As String
Dim mlngPolicy As Object


</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("val709_k")
'~End Header Block VisualTimer Utility
Response.Cache.SetCacheability(HttpCacheability.NoCache)

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.28.03
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "val709_k"
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
    document.VssVersion="$$Revision: 6 $|$$Date: 1/10/04 18:43 $|$$Author: Jfrugero $"

//% insStateZone: se controla el estado de los campos de la página
//--------------------------------------------------------------------------------------------
function insStateZone(){
//--------------------------------------------------------------------------------------------
    
    with(self.document.forms[0]){
        tcdEffecdate.disabled=false;
        btn_tcdEffecdate.disabled=false;
		cbeBranch.disabled=false;
    }
}    

//% insCancel: se controla la acción Cancelar de la página
//--------------------------------------------------------------------------------------------
function insCancel(){
//--------------------------------------------------------------------------------------------
    return true;
}

//--------------------------------------------------------------------------------------------
function EnabledField(sName, Value){
//--------------------------------------------------------------------------------------------
	if (sName=='valProduct'){
		if (Value>0){
			self.document.forms[0].nPolicy.disabled=false;
		}else{
			self.document.forms[0].nPolicy.disabled=true;
			self.document.forms[0].nReceipt.disabled=true;
			self.document.forms[0].nPolicy.value='';
			self.document.forms[0].nReceipt.value='';
		}
	}else{
		if (Value>0){
			self.document.forms[0].nReceipt.disabled=false;
		}else{
			self.document.forms[0].nReceipt.disabled=true;
			self.document.forms[0].nReceipt.value='';
		}
	}
}

//% insFinish: se controla la acción Cancelar de la página
//--------------------------------------------------------------------------------------------
function insFinish(){
//--------------------------------------------------------------------------------------------
    return true;
}
</SCRIPT>
<%
Response.Write(mobjValues.StyleSheet())
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write(mobjMenu.MakeMenu("VAL709", "VAL709_K.aspx", 1, vbNullString))
	mobjMenu = Nothing
	Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
End If

%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" NAME="VAL709" ACTION="valPolicyRep.aspx?sMode=2">
    <BR><BR>
    <%=mobjValues.ShowWindowsName("VAL709", Request.QueryString.Item("sWindowDescript"))%>
    <TABLE WIDTH="100%">
    <TR>
		<TD WIDTH="25%"><LABEL ID=0><%= GetLocalResourceObject("tcdEffecdateCaption") %></LABEL></TD>
<TD><% %>
<%=mobjValues.DateControl("tcdEffecdate", CStr(Today), CBool("1"), GetLocalResourceObject("tcdEffecdateToolTip"),  ,  ,  ,  , True, CShort("1"))%></TD>
		<TD ><LABEL><%= GetLocalResourceObject("AnchorCaption") %></LABEL></TD>
    </TR>
    <TR>
		<TD><LABEL><%= GetLocalResourceObject("cbeBranchCaption") %></LABEL></TD>
		<TD><%=mobjValues.BranchControl("cbeBranch", GetLocalResourceObject("cbeBranchToolTip"), mintBranch, "valProduct",  ,  ,  ,  , True, CShort("2"))%></TD>
        <TD><%=mobjValues.OptionControl(0, "optType", GetLocalResourceObject("optType_1Caption"), "1", "1")%></TD>
    </TR>
    <TR>
		<TD><LABEL><%= GetLocalResourceObject("valProductCaption") %></LABEL></TD>
		<TD><%=mobjValues.ProductControl("valProduct", GetLocalResourceObject("valProductToolTip"), mobjValues.StringToType(mintBranch, eFunctions.Values.eTypeData.etdDouble), eFunctions.Values.eValuesType.clngWindowType,  , mobjValues.StringToType(mintProduct, eFunctions.Values.eTypeData.etdDouble),  ,  ,  , "EnabledField(this.name, this.value);", CShort("3"),  ,  , eFunctions.Values.eProdClass.clngActiveLife)%></TD>
        <TD><%=mobjValues.OptionControl(0, "optType", GetLocalResourceObject("optType_2Caption"), "2", "2")%></TD>
    </TR>
    <TR>
		<TD><LABEL><%= GetLocalResourceObject("nPolicyCaption") %></LABEL></TD>
		<TD><%=mobjValues.NumericControl("nPolicy", 10,  ,  , GetLocalResourceObject("nPolicyToolTip"),  , 0,  ,  ,  , "EnabledField(this.name, this.value);", True, CShort("4"))%></TD>
    </TR>
    <TR>
		<TD><LABEL><%= GetLocalResourceObject("nReceiptCaption") %></LABEL></TD>
		<TD><%=mobjValues.NumericControl("nReceipt", 10,  ,  , GetLocalResourceObject("nReceiptToolTip"),  , 0,  ,  ,  ,  , True, CShort("5"))%></TD>
    </TR>
</TABLE>
</FORM> 
</BODY>
</HTML>
<%
mobjValues = Nothing%>

<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 10.28.03
Call mobjNetFrameWork.FinishPage("val709_k")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




