<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 09/05/2003 10:49:58 a.m.
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility
'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
'- Objeto para el manejo de las rutinas genéricas
Dim mobjMenu As eFunctions.Menues


</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
Call mobjNetFrameWork.BeginPage("LT004_K")
mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 09/05/2003 10:49:58 a.m.
mobjValues.sSessionID = Session.SessionID
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "LT004_K"
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 09/05/2003 10:49:58 a.m.
mobjMenu.sSessionID = Session.SessionID
'~End Body Block VisualTimer Utility

%>
<!-- #INCLUDE VIRTUAL="~/VTimeNet/Includes/Constantes.aspx" -->

<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></SCRIPT>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">
    <%With Response
	.Write(mobjValues.StyleSheet() & vbCrLf)
	.Write(mobjMenu.MakeMenu("LT004", "LT004_k.aspx", 1, ""))
End With
mobjMenu = Nothing%>
</HEAD>

<SCRIPT> 
//----------------------------------------------------------------------------------------------------------------------
function insStateZone(){
//----------------------------------------------------------------------------------------------------------------------
}
//-----------------------------------------------------------------------------
function insPreZone(llngAction){
//-----------------------------------------------------------------------------

}
//-----------------------------------------------------------------------------
function insCancel(){
//-----------------------------------------------------------------------------
   return true
}
</SCRIPT>

<BODY ONUNLOAD="closeWindows();">
<TD><BR></TD>
<TD><BR></TD>
<FORM METHOD="post" ID="FORM" NAME="LT004" ACTION="valLetter.aspx?x=1">
    <TABLE WIDTH="100%">
        <TR>
            <TD WIDTH="15%"><LABEL ID=7273>Solicitud</LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnLettRequest", 5, "",  ,"Número que identifica la solicitud de envío")%></TD>
        </TR>
        <TR>
            <TD><LABEL ID=7274>Cliente</LABEL></TD>
            <TD><%=mobjValues.ClientControl("tctClient", vbNullString,  ,"Código único de identificación al destinatario como cliente")%></TD>
      </TR>
	</TABLE>
<%mobjValues = Nothing%>
</FORM>
</BODY>
</HTML>
<%'^Begin Footer Block VisualTimer Utility 1.1 09/05/2003 10:49:58 a.m.
Call mobjNetFrameWork.FinishPage("LT004_K")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>








