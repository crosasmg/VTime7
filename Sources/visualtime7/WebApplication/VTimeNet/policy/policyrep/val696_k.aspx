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
<%
Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("val696_k")

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.28.03
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "val696_k"
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
    document.VssVersion="$$Revision: 2 $|$$Date: 23/08/09 9:14p $|$$Author: Gletelier $"
//% insStateZone: se controla el estado de los campos de la página
//--------------------------------------------------------------------------------------------
function insStateZone(){
//--------------------------------------------------------------------------------------------
    
    with(self.document.forms[0]){
        optType[0].disabled=false;
        optType[1].disabled=false;
        tcdEffecdate.disabled=false;
        btn_tcdEffecdate.disabled=false;
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
Response.Write(mobjValues.StyleSheet())
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write(mobjMenu.MakeMenu("VAL696", "VAL696_K.aspx", 1, vbNullString))
	mobjMenu = Nothing
	Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
End If

%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<BR><BR>
<FORM METHOD="POST" NAME="VAL696" ACTION="valPolicyRep.aspx?sMode=2">
    <%=mobjValues.ShowWindowsName("VAL696", Request.QueryString.Item("sWindowDescript"))%>
<TABLE WIDTH="100%">
    <TR>
        <TD CLASS="HighLighted"><LABEL ID=0><%= GetLocalResourceObject("AnchorCaption") %></LABEL></TD>
        <TD>&nbsp;</TD>
        <TD CLASS="HighLighted"><LABEL ID=0><%= GetLocalResourceObject("Anchor2Caption") %></LABEL></TD>
    </TR>
    <TR>
        <TD></TD>
        <TD></TD>
        <TD></TD>
        <TD></TD>
    </TR>
    <TR>
        <TD><%=mobjValues.OptionControl(0, "optType", GetLocalResourceObject("optType_1Caption"), "1", "1",  , True)%></TD>
        <TD><LABEL ID=13901><%= GetLocalResourceObject("tcdEffecdateCaption") %></LABEL></TD>
<TD><% %>
<%=mobjValues.DateControl("tcdEffecdate", CStr(Today), CBool("1"), GetLocalResourceObject("tcdEffecdateToolTip"),  ,  ,  ,  , True)%></TD>
    </TR>
    <TR>    
        <TD><%=mobjValues.OptionControl(0, "optType", GetLocalResourceObject("optType_2Caption"), "2", "2",  , True)%></TD>
        <TD><LABEL ID=13901><%= GetLocalResourceObject("cbeBranchCaption") %></LABEL></TD>
        <TD><%=mobjValues.BranchControl("cbeBranch", GetLocalResourceObject("cbeBranchToolTip"), Request.QueryString.Item("nBranch"), "valProduct")%></TD>
    </TR>
    <TR>
        <TD></TD>
        <TD></TD>
        <TD></TD>
    </TR>
    <TR>
        <TD></TD>
        <TD><LABEL ID=13901><%= GetLocalResourceObject("Anchor3Caption") %></LABEL></TD>
        <TD><%=mobjValues.ProductControl("valProduct", GetLocalResourceObject("valProductToolTip"),  , eFunctions.Values.eValuesType.clngWindowType, True)%></TD>        
        <TD><LABEL ID=13901><%= GetLocalResourceObject("Anchor4Caption") %></LABEL></TD>
    </TR>
</TABLE>
</FORM> 
</BODY>
</HTML>
<%
mobjValues = Nothing%>

<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 10.28.03
Call mobjNetFrameWork.FinishPage("val696_k")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>





