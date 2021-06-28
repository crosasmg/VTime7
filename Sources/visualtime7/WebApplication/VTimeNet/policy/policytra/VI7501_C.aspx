<%@ Page Language="VB" explicit="true" %>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eSaapv" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 31/3/03 19.44.14
Dim mobjNetFrameWork As eNetFrameWork.Layout

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues

Dim mclsSaapv As eSaapv.Saapv


'% insPreVI7501_C: Realiza la lectura de los campos a mostrar en pantalla
'---------------------------------------------------------------------
Private Sub insPreVI7501_C()
	'---------------------------------------------------------------------
	Call mclsSaapv.Find(mobjValues.TypeToString(Session("nCod_saapv"), eFunctions.Values.eTypeData.etdDouble), mobjValues.TypeToString(Session("nInstitution"), eFunctions.Values.eTypeData.etdLong))
End Sub

</script>
<%Response.Expires = -1
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("VI7501_C")
'~End Header Block VisualTimer Utility
Response.CacheControl = "private"

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.44.14
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = Request.QueryString("sCodispl")
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.44.14
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility
mclsSaapv = New eSaapv.Saapv
mobjValues.ActionQuery = Session("bQuery")
Call insPreVI7501_C()
%>
<SCRIPT>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 1 $|$$Date: 16/11/11 16:49 $|$$Author: ljimenez $"
</SCRIPT>
<%'UPGRADE_NOTE: Language element '#INCLUDE' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file 'vtime/Includes/Constantes.aspx' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<!-- #INCLUDE VIRTUAL="~/VTimeNet/Includes/Constantes.aspx" -->

<%'UPGRADE_NOTE: Language element 'SCRIPT' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file '/VTimeNet/Scripts/GenFunctions.js' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">
    <LINK REL="StyleSheet" TYPE="text/css" HREF="/VTimeNet/Common/Custom.css">
    <%Response.Write(mobjMenu.setZone(2, Request.QueryString("sCodispl"), Request.QueryString("sWindowDescript"), Request.QueryString("nWindowTy")))
'UPGRADE_NOTE: Object mobjMenu may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjMenu = Nothing%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="frmVI7501_C" ACTION="valVI7501tra.aspx?nMainAction=301&nHolder=1">
	<%=mobjValues.ShowWindowsName(Request.QueryString("sCodispl"), Request.QueryString("sWindowDescript"))%>
    <BR><BR>
    <TABLE WIDTH="100%">
        <TR>
            <TD>&nbsp;</TD>
            <TD><%=mobjValues.CheckControl("chkContributionAfp", "AFP", mclsSaapv.sIndContributios_Afp, "1",  , False, 1, "Indica que el cliente cotiza a través de AFP")%></TD>
			<TD>&nbsp;</TD>
			<TD COLSPAN="2" CLASS="HighLighted"><LABEL ID=0>Trabajador</LABEL></TD>
        </TR>        
        <TR>
            <TD></TD>
            <TD></TD>
            <TD></TD>
            <TD COLSPAN="2" CLASS="Horline"></TD>
        </TR>        
        <TR>
            <TD COLSPAN="3">&nbsp;</TD>
		    <TD><%=mobjValues.OptionControl(0, "optEmployee", "Dependiente", CStr(mclsSaapv.nType_Employee), "1",  ,  , 2, "Tipo de trabajador: Dependiente")%></TD>
		    <TD><%=mobjValues.OptionControl(0, "optEmployee", "Independiente", CStr(mclsSaapv.nType_Employee - 1), "2",  ,  , 2, "Tipo de trabajador: Independiente")%></TD>
        </TR>
        <TR>
            <TD>&nbsp;</TD>
            <TD><%=mobjValues.CheckControl("chkContributionIps", "IPS Ex Caja de Previsión", mclsSaapv.sIndContributios_Ips, "1",  , False, 3, "Indica que el cliente cotiza a través de IPS")%></TD>
			<TD COLSPAN="3">&nbsp;</TD>
        </TR>
        <TR>
            <TD COLSPAN="5">&nbsp;</TD>
        </TR>                
        <TR>
            <TD>&nbsp;</TD>
            <TD><%=mobjValues.CheckControl("chkHealth", "Cotizaciones de Salud", CStr(mclsSaapv.nInd_Health), "1",  , False, 4, "Indica que el cliente cotiza en el sistema de salud")%></TD>
			<TD COLSPAN="3">&nbsp;</TD>
        </TR>
    </TABLE>

<%
'Response.Write mobjValues.BeginPageButton 

'UPGRADE_NOTE: Object mobjValues may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjValues = Nothing
'UPGRADE_NOTE: Object mobjMenu may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjMenu = Nothing
'UPGRADE_NOTE: Object mclsSaapv may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mclsSaapv = Nothing
%>
</FORM>
</BODY>
</HTML>
<%'^Begin Footer Block VisualTimer Utility 1.1 31/3/03 19.44.14
Call mobjNetFrameWork.FinishPage("VI7501_C")
'UPGRADE_NOTE: Object mobjNetFrameWork may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




