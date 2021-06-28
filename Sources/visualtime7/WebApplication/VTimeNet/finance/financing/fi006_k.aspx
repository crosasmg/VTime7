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

mobjValues.sCodisplPage = "fi006_k"
%>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<HTML>
<HEAD>
<Script>
//% insStateZone: 
//--------------------------------------------------------------------------------------------
function insStateZone(){
//--------------------------------------------------------------------------------------------
    var nMainAction = 304; 
}

</Script>
    <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></SCRIPT>
        
    <%
With Response
            .Write(mobjValues.StyleSheet())
            .Write(mobjMenu.MakeMenu("FI006", "FI006_k.aspx", 1, ""))
End With
%></HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="frmAnulContrat" ACTION="valFinancing.aspx?Zone=1&sCodisplReload=FI006 ">


<%With Response
	.Write("<BR><BR>")
End With
%>    

<TABLE WIDTH="100%">
            
    <TR>
        <TD width = "10%"><LABEL ID=11078><%= GetLocalResourceObject("tcnContratCaption") %></LABEL></TD>
        <TD><%=mobjValues.NumericControl("tcnContrat", 8, "",  ,  ,  , 0)%></TD>
        <TD width = "20%"><LABEL ID=11091><%= GetLocalResourceObject("tcdNulldateCaption") %></LABEL></TD>
<TD><% %>
<%=mobjValues.DateControl("tcdNulldate", CStr(Today),  , GetLocalResourceObject("tcdNulldateToolTip"))%></TD>
    </TR>
</TABLE>

</FORM>
</BODY>
</HTML>





