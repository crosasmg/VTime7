
<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eClaim" %>
<script language="VB" runat="Server">
    '^Begin Header Block VisualTimer Utility 1.1 7/4/03 11.35.15
    Dim mobjNetFrameWork As eNetFrameWork.Layout
    '~End Header Block VisualTimer Utility
    
    '- Objeto para el manejo de las funciones generales de carga de valores
    Dim mobjValues As eFunctions.Values
    
    '- Objeto para el manejo de las zonas de la página    
    Dim mobjMenu As eFunctions.Menues
    
    '- Objeto para el manejo de Siniestro   
    Dim mobjClaim As eClaim.Claim
</script>

<%  Response.Expires = -1441
    mobjNetFrameWork = New eNetFrameWork.Layout
    mobjNetFrameWork.sSessionID = Session.SessionID
    mobjNetFrameWork.nUsercode = Session("nUsercode")
    Call mobjNetFrameWork.BeginPage("SIL974_k")
    
    mobjValues = New eFunctions.Values
    '^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.35.15
    mobjValues.sSessionID = Session.SessionID
    mobjValues.nUsercode = Session("nUsercode")
    '~End Body Block VisualTimer Utility
    mobjValues.sCodisplPage = "SIL974_k"
%>

<%'UPGRADE_NOTE: Language element 'SCRIPT' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file '/VTimeNet/Scripts/GenFunctions.js' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
    <SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<%'UPGRADE_NOTE: Language element 'SCRIPT' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file '/VTimeNet/Scripts/tMenu.js' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
    <SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tMenu.js"></SCRIPT>
<%'UPGRADE_NOTE: Language element '#INCLUDE' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file 'VTime/Includes/Constantes.aspx' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<!-- #INCLUDE VIRTUAL="~/VTimeNet/Includes/Constantes.aspx" -->

<HTML>
    <HEAD>
    <SCRIPT>
        //% insCancel: Ejecuta rutinas necesarias en el momento de cancelar la página
        //-----------------------------------------------------------------------------
        function insCancel()
        //-----------------------------------------------------------------------------
        {
            return true
        }
    </SCRIPT>
    <META NAME="GENERATOR" Content="eTransaction Designer for Visual TIME">
<%
    Response.Write(mobjValues.StyleSheet())
    mobjMenu = New eFunctions.Menues
    '^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.35.15
    mobjMenu.sSessionID = Session.SessionID
    mobjMenu.nUsercode = Session("nUsercode")
    '~End Body Block VisualTimer Utility
    Response.Write(mobjMenu.MakeMenu("SIL974", "SIL974_k.aspx", 1, Request.QueryString("sWindowDescript"), CStr(Session("sDesMultiCompany")), CStr(Session("sSche_code"))))
    'UPGRADE_NOTE: Object mobjMenu may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
    mobjMenu = Nothing
%>
    <SCRIPT>
    //+ Variable para el control de versiones
        document.VssVersion = "$$Revision: 1 $|$$Date: 2/09/03 19:01 $"
    </SCRIPT>
    </HEAD>
    <BODY ONUNLOAD="closeWindows();">
        <FORM METHOD="POST" ID="FORM" NAME="SIL974" ACTION="valClaimRep.aspx?sMode=1">
	    <BR><BR>
		    <%=mobjValues.ShowWindowsName(Request.QueryString("sCodispl"), Request.QueryString("sWindowDescript"))%>
	    <BR><BR>
	    <TABLE WIDTH="100%">
	        <TR>
			    <TD WIDTH="25%"><LABEL ID="Solicitud"><%= GetLocalResourceObject("tcnChequeCaption")%></LABEL></TD>
			    <TD WIDTH="25%"><% = mobjValues.NumericControl("tcnCheque", 10, , , GetLocalResourceObject("tcnChequeToolTip"))%></TD>
			    <TD WIDTH="25%">&nbsp</TD>
			    <TD WIDTH="25%">&nbsp</TD>
	        </TR>
        </TABLE>
        </FORM>
    </BODY>
</HTML>
<%	
    mobjValues = Nothing
    '^Begin Footer Block VisualTimer Utility 1.1 7/4/03 11.35.15
	Call mobjNetFrameWork.FinishPage("SIL974_k")
    mobjNetFrameWork = Nothing
    '^End Footer Block VisualTimer%>
