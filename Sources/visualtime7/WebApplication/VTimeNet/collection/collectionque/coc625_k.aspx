<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 10.44.07
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
Call mobjNetFrameWork.BeginPage("coc625_k")

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.44.07
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "coc625_k"
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.44.07
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
	document.VssVersion="$$Revision: 1 $|$$Date: 2/09/03 19:01 $|$$Author: Iusr_llanquihue $"

//% ChangeValues: se controla el cambio de valor de los campos de la página
//--------------------------------------------------------------------------------------------
function ChangeValues(Field){
//--------------------------------------------------------------------------------------------
	if(Field.value!=0 &&
	   Field.value!='')
		insDefValues('Client_Agreement', 'nCod_Agree=' + Field.value)
}
//% insStateZone: se controla el estado de los campos de la página
//--------------------------------------------------------------------------------------------
function insStateZone(){
//--------------------------------------------------------------------------------------------
	with(self.document.forms[0]){
		tcnCod_agree.disabled = false;
        tcdInit_date.disabled = false;
        btn_tcdInit_date.disabled = false;
		tcdEnd_date.disabled = false;
        btn_tcdEnd_date.disabled = false;
        optReceipt[0].disabled = false;
        optReceipt[1].disabled = false;
        optReceipt[2].disabled = false;
        cbeBranch.disabled = false;
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
</SCRIPT>
<%
With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjMenu.MakeMenu("COC625", "COC625_k.aspx", 1, Request.QueryString.Item("sWindowDescript"), Session("sDesMultiCompany"), Session("sSche_code")))
End With
mobjMenu = Nothing
Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" NAME="COC625" ACTION="valCollectionQue.aspx?sMode=2">
    <BR><BR>
    <TABLE WIDTH="100%">
        <TR>
			<TD WIDTH="15%"><LABEL ID=0><%= GetLocalResourceObject("tcnCod_agreeCaption") %></LABEL></TD>
			<TD WIDTH="10%"><%=mobjValues.NumericControl("tcnCod_agree", 5, vbNullString,  , GetLocalResourceObject("tcnCod_agreeToolTip"), False,  ,  ,  ,  , "ChangeValues(this)", True, 1)%></TD>
			<TD><LABEL><DIV ID="sClient"></DIV></LABEL></TD>
		</TR>
	</TABLE>
	<TABLE WIDTH="100%">
        <TR>
		    <TD COLSPAN="2" CLASS="HighLighted" WIDTH="50%"><LABEL ID=0><%= GetLocalResourceObject("AnchorCaption") %></LABEL></TD>
		    <TD WIDTH="10%">&nbsp;</TD>
		    <TD COLSPAN="2" CLASS="HighLighted"><LABEL ID=0><%= GetLocalResourceObject("Anchor2Caption") %></LABEL></TD>
		</TR>
		<TR>
		    <TD COLSPAN="2" CLASS="HorLine"></TD>
		    <TD></TD>
		    <TD COLSPAN="2" CLASS="HorLine"></TD>
		</TR>
		<TR>
			<TD WIDTH="15%"><LABEL ID=0><%= GetLocalResourceObject("tcdInit_dateCaption") %></LABEL></TD>
            <TD><%=mobjValues.DateControl("tcdInit_date", vbNullString,  , GetLocalResourceObject("tcdInit_dateToolTip"),  ,  ,  ,  , True, 2)%></TD>
			<TD>&nbsp;</TD>
			<TD COLSPAN="2"><%=mobjValues.OptionControl(0, "optReceipt", GetLocalResourceObject("optReceipt_1Caption"), "1", "1",  , True, 4, GetLocalResourceObject("optReceipt_1ToolTip"))%> </TD>
		</TR>
		<TR>
			<TD><LABEL ID=0><%= GetLocalResourceObject("tcdEnd_dateCaption") %></LABEL></TD>
            <TD><%=mobjValues.DateControl("tcdEnd_date", vbNullString,  , GetLocalResourceObject("tcdEnd_dateToolTip"),  ,  ,  ,  , True, 3)%></TD>
			<TD>&nbsp;</TD>
			<TD COLSPAN="2"><%=mobjValues.OptionControl(0, "optReceipt", GetLocalResourceObject("optReceipt_2Caption"),  , "2",  , True, 5, GetLocalResourceObject("optReceipt_2ToolTip"))%> </TD>
        </TR>
		<TR>
			<TD COLSPAN="3">&nbsp;</TD>
			<TD COLSPAN="2"><%=mobjValues.OptionControl(0, "optReceipt", GetLocalResourceObject("optReceipt_3Caption"), "2", "3",  , True, 6, GetLocalResourceObject("optReceipt_3ToolTip"))%> </TD>
        </TR>
        <TR>
			<TD><LABEL ID=0><%= GetLocalResourceObject("cbeBranchCaption") %></LABEL></TD>
            <TD COLSPAN="4"><%=mobjValues.BranchControl("cbeBranch", GetLocalResourceObject("cbeBranchToolTip"), vbNullString, "valProduct",  ,  ,  ,  , True, 7)%></TD>
        </TR>
        <TR>
			<TD><LABEL ID=0><%= GetLocalResourceObject("valProductCaption") %></LABEL></TD>
            <TD COLSPAN="4"><%=mobjValues.ProductControl("valProduct", GetLocalResourceObject("valProductToolTip"), vbNullString, eFunctions.Values.eValuesType.clngWindowType,  , vbNullString,  ,  ,  ,  , 8)%></TD>
        </TR>
    </TABLE>
</FORM> 
</BODY>
</HTML>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 10.44.07
Call mobjNetFrameWork.FinishPage("coc625_k")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




