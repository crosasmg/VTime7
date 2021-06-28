<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 11.35.14
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

'- Objeto para el manejo de las funciones generales de carga de valores

Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues


</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("sil002_k")

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.35.14
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "sil002_k"
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.35.14
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

%>
<%'UPGRADE_NOTE: Language element 'SCRIPT' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file '/VTimeNet/Scripts/GenFunctions.js' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
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
	<%'UPGRADE_NOTE: Language element 'SCRIPT' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file '/VTimeNet/Scripts/GenFunctions.js' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
	<%'UPGRADE_NOTE: Language element 'SCRIPT' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file '/VTimeNet/Scripts/tMenu.js' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tMenu.js"></SCRIPT>
	<%'UPGRADE_NOTE: Language element '#INCLUDE' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file 'VTime/Includes/Constantes.aspx' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<!-- #INCLUDE VIRTUAL="~/VTimeNet/Includes/Constantes.aspx" -->

	<%
Response.Write(mobjValues.StyleSheet())
Response.Write(mobjMenu.MakeMenu("SIL002", "SIL002_K.aspx", 1, Request.QueryString("sWindowDescript"), CStr(Session("sDesMultiCompany")), CStr(Session("sSche_code"))))
'UPGRADE_NOTE: Object mobjMenu may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjMenu = Nothing
%>
</HEAD>

<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="frmClaimExcessAmount" ACTION="VALCLAIMREP.ASPX?mode=1">
    <BR></BR>
    <TABLE WIDTH="100%">
		<TR>
            <TD COLSPAN="2" CLASS="HighLighted"><LABEL ID="101554"><a NAME="Fecha de declaración">Fecha de declaración</a></LABEL></TD>
			<TD COLSPAN="4"></TD>	
		</TR>
		<TR>
			<TD COLSPAN="2" CLASS="HorLine"></TD>
			<TD COLSPAN="4"></TD>	
		</TR>
		<TR>
            <TD><LABEL ID="8704">Inicial</LABEL></TD>
            <TD><%=mobjValues.DateControl("tcdIniDate",  ,  , "Fecha desde la cual se desea listar los siniestros")%></TD>
			<TD COLSPAN="4">&nbsp</TD>	
        </TR>
        <TR>
            <TD><LABEL ID="8703">Final</LABEL></TD>
            <TD><%=mobjValues.DateControl("tcdEndate",  ,  , "Fecha hasta la cual se desea listar los siniestros")%></TD>
			<TD COLSPAN="4">&nbsp</TD>	        
		</TR>
		<TR>
			<TD COLSPAN="6">&nbsp</TD>	        
		</TR>			
		<TR>
            <TD COLSPAN="2" CLASS="HighLighted"><LABEL ID="101554"><a NAME="Carta a Imprimir">Carta a Imprimir</a></LABEL></TD>
			<TD COLSPAN="4"></TD>	
		</TR>
		<TR>
			<TD COLSPAN="2" CLASS="HorLine"></TD>
			<TD COLSPAN="4"></TD>	
		</TR>
        <TR>
            <TD COLSPAN="2"><%=mobjValues.OptionControl(101589, "optTypeRep", "Aviso Monto en Exceso", CStr(1), CStr(1), "insStateChek(true, false);")%></TD>
        </TR>
        <TR>
            <TD><%=mobjValues.OptionControl(101590, "optTypeRep", "Pago Monto en Exceso",  , CStr(2), "insStateChek(false, false);")%></TD>
        </TR>
        <TR>
		<TR>
			<TD COLSPAN="6">&nbsp</TD>	        
		</TR>			
        <TR>
            <TD><LABEL ID="8594">Sucursal</LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("cbeOffice", "Table9", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  ,  ,  , "Sucursal a la que pertenece el exceso dado")%></TD>
            <TD><LABEL ID="9380">Ramo</LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("cbeBranch", "Table10", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  ,  ,  , "Tipo de ramo en que se muestra  el exceso dado")%></TD>
        </TR>
    </TABLE>
</FORM>
</BODY>
</HTML>
<%'UPGRADE_NOTE: Object mobjValues may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjValues = Nothing%>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 11.35.14
Call mobjNetFrameWork.FinishPage("sil002_k")
'UPGRADE_NOTE: Object mobjNetFrameWork may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




