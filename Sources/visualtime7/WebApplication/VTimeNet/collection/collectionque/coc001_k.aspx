<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 10.44.07
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility
'- Objeto para el manejo de las funciones generales de carga de valores

Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues


</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("coc001_k")
With Server
	mobjValues = New eFunctions.Values
	'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.44.07
	mobjValues.sSessionID = Session.SessionID
	mobjValues.nUsercode = Session("nUsercode")
	'~End Body Block VisualTimer Utility
	
	mobjValues.sCodisplPage = "coc001_k"
	mobjMenu = New eFunctions.Menues
	'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.44.07
	mobjMenu.sSessionID = Session.SessionID
	mobjMenu.nUsercode = Session("nUsercode")
	'~End Body Block VisualTimer Utility
End With
%>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></script>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">
    <SCRIPT>
//+ Variable para el control de versiones
	     document.VssVersion="$$Revision: 1 $|$$Date: 2/09/03 19:01 $|$$Author: Iusr_llanquihue $"
    </SCRIPT>


    <%With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjMenu.MakeMenu("COC001", "COC001_k.aspx", 1, Request.QueryString.Item("sWindowDescript"), Session("sDesMultiCompany"), Session("sSche_code")))
End With
mobjMenu = Nothing%>
<SCRIPT>
//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
	return true;
}
//------------------------------------------------------------------------------------------
function  insStateZone(){
//------------------------------------------------------------------------------------------
    var lintIndex = 0;
    for (lintIndex=0;lintIndex<document.forms[0].length;lintIndex++)
        document.forms[0].elements[lintIndex].disabled = false
    document.images["btn_tcdInitDate"].disabled = false
    document.images["btn_tcdEndDate"].disabled = false
}
//% insFinish: se controla la acción Cancelar de la página
//--------------------------------------------------------------------------------------------
function insFinish(){
//--------------------------------------------------------------------------------------------
	insReloadTop(false);
}
</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="frmCollecOper" ACTION="valCollectionQue.aspx?mode=1">
<BR></BR>
    <TABLE WIDTH="100%">
		<TR>
            <TD COLSPAN="2" CLASS="HighLighted"><LABEL ID=40421><A NAME="Período a consultar"><%= GetLocalResourceObject("AnchorPeríodo a consultarCaption") %></A></LABEL></TD>
        </TR>
        <TR>
			<TD COLSPAN="2"><HR></TD>
        </TR>
        <TR>
            <TD><LABEL ID=9909><%= GetLocalResourceObject("tcdInitDateCaption") %></LABEL></TD>
            <TD><%=mobjValues.DateControl("tcdInitDate", CStr(Today),  , GetLocalResourceObject("tcdInitDateToolTip"),  ,  ,  ,  , True)%></TD>
            <TD>&nbsp;</TD>
            <TD><LABEL ID=9906><%= GetLocalResourceObject("tcnCashnumCaption") %></LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnCashnum", 5, vbNullString,  , GetLocalResourceObject("tcnCashnumToolTip"),  ,  ,  ,  ,  ,  , True)%></TD>
        </TR>
        <TR>
            <TD><LABEL ID=9908><%= GetLocalResourceObject("tcdEndDateCaption") %></LABEL></TD>
            <TD><%=mobjValues.DateControl("tcdEndDate", CStr(Today),  , GetLocalResourceObject("tcdEndDateToolTip"),  ,  ,  ,  , True)%></TD>
            <TD>&nbsp;</TD>
            <TD><LABEL ID=9906><%= GetLocalResourceObject("cbeOfficeCaption") %></LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("cbeOffice", "Table9", 1,  ,  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeOfficeToolTip"))%></TD>
        </TR>
        <TR>
			<TD COLSPAN="3">&nbsp;</TD>
            <TD><LABEL ID=9907><%= GetLocalResourceObject("cbeCurrencyCaption") %></LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("cbeCurrency", "Table11", 1,  ,  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeCurrencyToolTip"))%></TD>
        </TR>
    </TABLE>
</FORM>
</BODY>
</HTML>
<%mobjValues = Nothing%>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 10.44.07
Call mobjNetFrameWork.FinishPage("coc001_k")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




