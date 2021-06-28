<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%Response.Expires = -1441
Session("sLinkBatch") = "1"
%>
<HTML>
<HEAD>
	<LINK REL="SHORTCUT ICON" HREF="/VTimeNet/images/favicon.ico">
	<TITLE>Menu principal</TITLE>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
<SCRIPT>
//% insClose: se controla la descarga de la página
//-------------------------------------------------------------------------------------------
function insClose(){
//-------------------------------------------------------------------------------------------
}
</SCRIPT>
</HEAD>
<FRAMESET COLS="153,*" BORDER=0> 
    <FRAME SRC="/VTimeNet/Visualtime/Modules.aspx" NAME="FraModules" TARGET="FraHeader" NORESIZE SCROLLING="no">
    <FRAMESET ROWS="60,*" >
        <FRAME SRC="/VTimeNet/common/Blank.htm" NAME="FraHeader" SCROLLING="no" NORESIZE SCROLLING="no">
        <FRAME SRC="/VTimeNet/Visualtime/Login.aspx?<%=Request.Params.Get("Query_String")%>" NAME="treeFrame" NORESIZE> 
    </FRAMESET>
    <NOFRAMES>
</FRAMESET> 
</HTML>





