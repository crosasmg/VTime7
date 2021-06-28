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

mobjValues.sCodisplPage = "fi005_k"
%>
	
<HTML>
<HEAD>
<Script>
//% insStateZone: 
//--------------------------------------------------------------------------------------------
function insStateZone(){
//--------------------------------------------------------------------------------------------
}
</Script>
    <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></SCRIPT>
        
    <%
With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjMenu.MakeMenu("FI005", "FI005_k.aspx", 1, ""))
End With
%>

</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="FI005_K" ACTION="valFinancing.aspx?nMainAction=<%=Request.QueryString.Item("nMainAction")%>">
<%With Response
	'.Write   mobjValues.ShowWindowsName("FI005") 
	.Write("<BR><BR>")
End With
%>    
    <TABLE WIDTH="100%">
        <TR>
            <TD width = "10%"><LABEL ID=11190><%= GetLocalResourceObject("tcnContratCaption") %></LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnContrat", 8, "",  ,  ,  , 0)%></TD>
            
            <TD width = "20%"><LABEL ID=11194><%= GetLocalResourceObject("tcdEffecdateCaption") %></LABEL></TD>
<TD><% %>
<%=mobjValues.DateControl("tcdEffecdate", CStr(Today),  , GetLocalResourceObject("tcdEffecdateToolTip"))%></TD>
        </TR>
    </TABLE>
</FORM>
</BODY>
</HTML>





