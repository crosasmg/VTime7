<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">
'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues


</script>
<%Response.Expires = 0

mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues


mobjValues.ActionQuery = Session("bQuery")

mobjValues.sCodisplPage = "fi014_k"
%>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<HTML>
<HEAD>
<SCRIPT>
//% insStateZone: 
//--------------------------------------------------------------------------------------------
function insStateZone(){
//--------------------------------------------------------------------------------------------
}
</SCRIPT>
        <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></SCRIPT>
        
    <%
With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjMenu.MakeMenu("FI014", "FI014_k.aspx", 1, ""))
End With
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="frmCollectReverse" ACTION="valFinancing.aspx?nMainAction=<%=Request.QueryString.Item("nMainAction")%>">
<%With Response
	.Write("<BR><BR>")
End With
%>    
    <TABLE WIDTH="100%">
        <TR>
            <TD><LABEL ID=11128><%= GetLocalResourceObject("tcnContratCaption") %></LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnContrat", 8, "",  ,  ,  , 0)%></TD>
            <TD><LABEL ID=11133><%= GetLocalResourceObject("tcnDraftCaption") %></LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnDraft", 5, "")%></TD>
	    </TR>
	    <TR>        
            <TD><LABEL ID=11129><%= GetLocalResourceObject("cbeCauseCaption") %></LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("cbeCause", "table259", 1,  ,  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeCauseToolTip"))%></TD>
            <TD><LABEL ID=11136><%= GetLocalResourceObject("tcdOpe_dateCaption") %></LABEL></TD>
            <TD><%=mobjValues.DateControl("tcdOpe_date", "",  , GetLocalResourceObject("tcdOpe_dateToolTip"))%></TD>
        </TR>    
    </TABLE>
</FORM>
</BODY>
</HTML>





