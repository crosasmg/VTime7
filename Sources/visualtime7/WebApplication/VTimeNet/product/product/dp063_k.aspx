<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">

Dim mobjValues As eFunctions.Values


</script>
<%Response.Expires = -1
mobjValues = New eFunctions.Values

mobjValues.sCodisplPage = "dp063_k"
%>
<HTML>
<HEAD>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>


	<%With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjValues.ShowWindowsName("DP063"))
	.Write(mobjValues.WindowsTitle("DP063"))
End With
%>
	
<SCRIPT>
//% insCloseWindows: Permite cerrar la ventana PopUp invocada. Este evento es llamado desde el botón 
//% ButtonAcceptCancel.
//------------------------------------------------------------------------------------------------
function insCloseWindows(){
//------------------------------------------------------------------------------------------------
	window.close()
}
</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="DP063" ACTION="valProduct.aspx?scodispl=DP063&mode=1&WindowType=PopUp&nProduct=<%=Request.QueryString.Item("nProduct")%>&dEffecdate=<%=Request.QueryString.Item("dEffecdate")%>">
    <TABLE WIDTH="100%">
		<TR>
            <TD WIDTH=10%><LABEL ID=14238><A NAME="Ramo"><%= GetLocalResourceObject("cbeBranchCaption") %></A></LABEL></TD>
            <TD> <%=mobjValues.PossiblesValues("cbeBranch", "Table10", eFunctions.Values.eValuesType.clngComboType, session("nBranch"),  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeBranchToolTip"),  , 1)%></TD>
		</TR>           
		<TR>
            <TD WIDTH=10%><LABEL ID=14240><A NAME="Product"><%= GetLocalResourceObject("tcnProductNewCaption") %></A></LABEL></TD>
            <TD> <%=mobjValues.NumericControl("tcnProductNew", 5, vbNullString, False, GetLocalResourceObject("tcnProductNewToolTip"),  ,  ,  ,  ,  ,  , False, 2)%></TD>
		</TR>            
		<TR>		
            <TD WIDTH=12%=> <LABEL ID=14239><A NAME="Fecha"><%= GetLocalResourceObject("tcdDateNewCaption") %></A></LABEL></TD>
            <TD> <%=mobjValues.DateControl("tcdDateNew", CStr(Now),  , GetLocalResourceObject("tcdDateNewToolTip"),  ,  ,  ,  , False, 2)%></TD>
        </TR>
    </TABLE>
    <P ALIGN=RIGHT> <%=mobjValues.ButtonAcceptCancel( , "insCloseWindows()", True,  , eFunctions.Values.eButtonsToShow.All)%> </P>
</FORM>
</BODY>
</HTML>
<%
mobjValues = Nothing
%>





