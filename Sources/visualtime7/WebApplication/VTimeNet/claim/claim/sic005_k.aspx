<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 11.34.48
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo de las zonas de la página    
Dim mobjMenu As eFunctions.Menues


</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("sic005_k")

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.34.48
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "sic005_k"
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.34.48
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility
%>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">
	<%'UPGRADE_NOTE: Language element 'SCRIPT' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file '/VTimeNet/Scripts/GenFunctions.js' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
	<%'UPGRADE_NOTE: Language element 'SCRIPT' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file '/VTimeNet/Scripts/tmenu.js' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></script>
    <%'UPGRADE_NOTE: Language element '#INCLUDE' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file 'Vtime/Includes/Constantes.aspx' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<!-- #INCLUDE VIRTUAL="~/VTimeNet/Includes/Constantes.aspx" -->

    <%
With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjMenu.MakeMenu("SIC005", "SIC005_k.aspx", 1, Request.QueryString("sWindowDescript"), CStr(Session("sDesMultiCompany")), CStr(Session("sSche_code"))))
End With

'UPGRADE_NOTE: Object mobjMenu may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjMenu = Nothing
%>
<SCRIPT>
//+ Variable para el control de versiones
    document.VssVersion="$$Revision: 1 $|$$Date: 2/09/03 19:01 $|&&Author: &"
//% insStateZone: se manejan los campos de la página
//------------------------------------------------------------------------------------------
function  insStateZone()
//------------------------------------------------------------------------------------------
{
	self.document.forms[0].tcdInitdate.disabled=false;
	self.document.forms[0].btn_tcdInitdate.disabled=false;
	self.document.forms[0].cbeOffice.disabled=false;
	self.document.forms[0].cbeBranch.disabled=false;
	self.document.forms[0].valMoveType.disabled=false;
	self.document.forms[0].btnvalMoveType.disabled=false;
	self.document.forms[0].valCurrency.disabled=false;
	self.document.forms[0].btnvalCurrency.disabled=false;
	self.document.forms[0].tcdInitdate.focus();
}

//% insCancel: Ejecuta rutinas necesarias en el momento de cancelar la página
//------------------------------------------------------------------------------------------
function insCancel()
//------------------------------------------------------------------------------------------
{		    
	return true;
}

//%ShowChangeValues: Evento OnChange de CbeBranch
//-----------------------------------------------------------------------------
function ShowChangeValues(Control)
//-----------------------------------------------------------------------------
{
	if(Control=="0")
	    self.document.forms[0].valProduct.disabled=true
	else{
	    self.document.forms[0].cbeBranch.disabled=false
	    self.document.forms[0].valProduct.disabled=false
   }     
}
</SCRIPT>
</HEAD>

<BODY ONUNLOAD="closeWindows();">

<FORM METHOD="POST" ID="FORM" NAME="SIC005" ACTION="ValClaim.ASPX?sMode=1">
	<BR><BR>
    <TABLE WIDTH="100%">
		<TR>
			<TD WIDTH=20%><LABEL ID=0>Fecha</LABEL></TD>
			<TD><%'UPGRADE_WARNING: Date was upgraded to Today and has a new behavior. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1041.aspx'%>
<%=mobjValues.DateControl("tcdInitdate", CStr(Today),  , "Indica la fecha a partir de la cual se han realizado las operaciones a consultar",  ,  ,  ,  , True, 1)%></TD>
			<TD><LABEL ID=0>Sucursal</LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("cbeOffice", "table9", eFunctions.Values.eValuesType.clngComboType, "",  ,  ,  ,  ,  ,  , True,  , "Sucursal a la que pertenecen los siniestros a consultar. Limita la consulta a movimientos de la sucursal seleccionada.",  , 2)%></TD>
        </TR>
		<TR>
            <TD WIDTH=5%><LABEL ID=0>Ramo</LABEL></TD>
            <TD WIDTH=28%><%=mobjValues.BranchControl("cbeBranch", "Ramo al que pertenecen las pólizas de los siniestros a consultar. Limita la consulta a movimientos del ramo seleccionado",  ,  ,  ,  ,  , "ShowChangeValues(this.value);", True, 3)%></TD>
            <TD WIDTH=5%><LABEL ID=0>Producto</LABEL></TD>
            <TD><%=mobjValues.ProductControl("valProduct", "Producto al que pertenecen las pólizas de los siniestros a consultar. Limita la consulta a movimientos del producto seleccionado",  , eFunctions.Values.eValuesType.clngWindowType,  ,  ,  ,  ,  ,  , 4, True)%></TD>
        </TR>
        <TR>
			<TD WIDTH=20%><LABEL ID=0>Tipo de operación</LABEL></TD>
			<TD><%=mobjValues.PossiblesValues("valMoveType", "Table140", eFunctions.Values.eValuesType.clngWindowType,  ,  ,  ,  ,  ,  ,  , True,  , "Tipo de operación a consultar. Limita la consulta a movimientos del tipo seleccionado",  , 5)%></TD>
			<TD WIDTH=20%><LABEL ID=0>Moneda</LABEL></TD>
			<TD><%=mobjValues.PossiblesValues("valCurrency", "Table11", eFunctions.Values.eValuesType.clngWindowType, "",  ,  ,  ,  ,  ,  , True,  , "Moneda en la que se han realizado las operaciones a consultar. Limita la consulta a movimientos en la moneda indicada",  , 6)%></TD>
        </TR>
	</TABLE>
	<P>&nbsp;</P>
	<P>&nbsp;</P>
</FORM>
</BODY>
</HTML>

<%'UPGRADE_NOTE: Object mobjValues may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjValues = Nothing%>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 11.34.48
Call mobjNetFrameWork.FinishPage("sic005_k")
'UPGRADE_NOTE: Object mobjNetFrameWork may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




