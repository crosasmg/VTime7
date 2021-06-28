<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues


</script>
<%Response.Expires = -1
With Server
	mobjValues = New eFunctions.Values
	mobjMenu = New eFunctions.Menues
End With

mobjValues.sCodisplPage = "op007_k"
%>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></script>
<%With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjMenu.MakeMenu("OP007", "OP007_k.aspx", 1, ""))
End With
mobjMenu = Nothing
%>
<SCRIPT>
//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
	return true;
}
//------------------------------------------------------------------------------------------
function  insStateZone(){
//------------------------------------------------------------------------------------------
	if (top.fraSequence.plngMainAction != 301)
	    document.forms[0].tcnRequeNum.disabled=false
    else
        ShowPopUp("/VTimeNet/CashBank/CashBank/ShowDefValues.aspx?Field=RequeNum","ShowDefValuesCheques",1, 1,"no","no",2000,2000);
}
</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="frmChequeReque" ACTION="valCashBank.aspx?mode=1">
<BR></BR>
    <TABLE WIDTH="100%">
        <TR>
            <TD WIDTH="25%"><LABEL ID=8650><%= GetLocalResourceObject("tcnRequeNumCaption") %></LABEL></TD>
            <TD WIDTH="25%"><%=mobjValues.NumericControl("tcnRequeNum", 4, CStr(0),  , "",  ,  ,  ,  ,  ,  , True)%></TD>
            <TD WIDTH="25%"></TD>
            <TD></TD>
        </TR>
    </TABLE>
</FORM>
</BODY>
</HTML>
<%
mobjValues = Nothing%>




