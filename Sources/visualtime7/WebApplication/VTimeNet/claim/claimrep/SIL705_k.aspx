<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eGeneral" %>
<script language="VB" runat="Server">

    Dim sCodispl As String
    Dim sCodisplPage As String
    '^Begin Header Block VisualTimer Utility 1.1 7/4/03 11.35.14
    Dim mobjNetFrameWork As eNetFrameWork.Layout
    '~End Header Block VisualTimer Utility

    '- Objeto para el manejo de las funciones generales de carga de valores
    Dim mobjValues As eFunctions.Values
    Dim mobjMenu As eFunctions.Menues

    '- Objeto para el manejo de la Fecha
    Dim mobjDate As eGeneral.GeneralFunction

    '- Objeto para el manejo de periodos
    Dim lclsCtrol_date As eGeneral.Ctrol_date
    
    '- Constante para determinar codigo de acción para obtener ultimo periodo de CTROL_DATE
    Const clngGenBookCollection As Short = 203
    
    'Variables para determinación de fechas
    Dim mdEffecdate As String
    Dim FirstDay As Date
    Dim LastDay As Date


</script>
<%
    sCodispl = Trim(Request.QueryString("sCodispl"))
    sCodisplPage = LCase(sCodispl) & "_k"

    Response.Expires = -1441
    mobjNetFrameWork = New eNetFrameWork.Layout
    mobjNetFrameWork.sSessionID = Session.SessionID
    mobjNetFrameWork.nUsercode = Session("nUsercode")
    Call mobjNetFrameWork.BeginPage(sCodisplPage)
    
    lclsCtrol_date = New eGeneral.Ctrol_date

    mobjValues = New eFunctions.Values
    '^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.35.14
    mobjValues.sSessionID = Session.SessionID
    mobjValues.nUsercode = Session("nUsercode")
    mobjValues.sCodisplPage = sCodisplPage
    '~End Body Block VisualTimer Utility

    mobjMenu = New eFunctions.Menues
    '^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.35.14
    mobjMenu.sSessionID = Session.SessionID
    mobjMenu.nUsercode = Session("nUsercode")
    '~End Body Block VisualTimer Utility

%>

<%  
    'Se obtiene la variable de fecha de CTROL_DATE para asignar valores a la pagina
    If lclsCtrol_date.Find(clngGenBookCollection) Then
        
        'Se determina la fecha de inicio del periodo
        FirstDay = lclsCtrol_date.dEffecdate.AddDays(1)
    
        'Se determina la fecha de fin del periodo
        LastDay = lclsCtrol_date.dEffecdate.AddMonths(1)
        LastDay = LastDay.AddDays(1)

    End If
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
//+ Variable para el control de versiones
    document.VssVersion="$$Revision: 1 $|$$Date: 2/09/03 19:01 $"

//% insStateZone: se manejan los campos de la página
//------------------------------------------------------------------------------------------
function insStateZone()
//------------------------------------------------------------------------------------------
{
}

//% insCancel: Ejecuta rutinas necesarias en el momento de cancelar la página
//------------------------------------------------------------------------------------------
function insCancel()
//------------------------------------------------------------------------------------------
{		    
	return true;
}

//%insDefValue:Permite asignarle "0,00" al control en caso de no haber indicado
//%valor numerico al campo
//------------------------------------------------------------------------------------------
function insDefValue(Field){
//------------------------------------------------------------------------------------------
    if(Field.value=='')
        self.document.forms[0].tcnExcess.value='0'
}

</SCRIPT>
    <META NAME="GENERATOR" Content="Microsoft Visual Studio 6.0">
	<%
Response.Write(mobjValues.StyleSheet())
Response.Write(mobjMenu.MakeMenu(sCodispl, sCodispl & "_k.aspx", 1, Request.QueryString("sWindowDescript"), CStr(Session("sDesMultiCompany")), CStr(Session("sSche_code"))))
	    Response.Write(mobjValues.WindowsTitle("sil705", Request.QueryString("sWindowDescript")))
	    'UPGRADE_NOTE: Object mobjMenu may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjMenu = Nothing
%>
</HEAD>

<BODY ONUNLOAD="closeWindows();">
<FORM method="post" id="FORM" name="frmClaim" action="valClaimRep.aspx?mode=1">
    <BR></BR>
    <%=mobjValues.ShowWindowsName(Request.QueryString("sCodispl"), Request.QueryString("sWindowDescript"))%>
	<table width="100%" border="0">
    <BR></BR>
		<tr>
            <td><LABEL ID="8704">Fecha Desde: </LABEL></td>
            <td><%=mobjValues.DateControl("tcdIniDate", FirstDay, , "Fecha desde la cual se desea listar los siniestros", , , , , False)%></td>

		    <TD>&nbsp</TD>
		    <TD>&nbsp</TD>
            <TD><LABEL ID="0">Tipo Ejecucion</LABEL></TD>
            <TD><%=mobjValues.OptionControl(0, "optOption", GetLocalResourceObject("optOption_1Caption"), "1", "1")%></TD>
        
        </tr>
        <tr>
            <td><LABEL ID="8703">Fecha Hasta:</LABEL></td>
            <td><%=mobjValues.DateControl("tcdEndDate", LastDay, , "Fecha hasta la cual se desea listar los siniestros", , , , , False)%></td>        
		    <TD>&nbsp</TD>
		    <TD>&nbsp</TD>
		    <TD>&nbsp</TD>
            <TD><%=mobjValues.OptionControl(0, "optOption", GetLocalResourceObject("optOption_2Caption"), "2", "2")%> </TD>
        </tr>
	</table>
</FORM>
</BODY>
</HTML>
<%'UPGRADE_NOTE: Object mobjValues may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjValues = Nothing%>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 11.35.14
Call mobjNetFrameWork.FinishPage(sCodisplPage)
'UPGRADE_NOTE: Object mobjNetFrameWork may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




