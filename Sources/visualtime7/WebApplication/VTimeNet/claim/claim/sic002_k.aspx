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
Call mobjNetFrameWork.BeginPage("sic002_k")

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.34.48
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "sic002_k"
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
	.Write(mobjMenu.MakeMenu("SIC002", "SIC002_k.aspx", 1, Request.QueryString("sWindowDescript"), CStr(Session("sDesMultiCompany")), CStr(Session("sSche_code"))))
End With

'UPGRADE_NOTE: Object mobjMenu may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjMenu = Nothing
%>
<SCRIPT>
//+ Variable para el control de versiones
    document.VssVersion="$$Revision: 4 $|$$Date: 11/11/03 17:08 $"
//% insStateZone: se manejan los campos de la página
//------------------------------------------------------------------------------------------
function  insStateZone()
//------------------------------------------------------------------------------------------
{
    self.document.forms[0].cbeBranch.disabled=false
    self.document.forms[0].valProduct.disabled=false
	self.document.forms[0].tcnPolicy.disabled=false
	self.document.forms[0].tcdOccurdate.disabled=false
}

//% insCancel: Ejecuta rutinas necesarias en el momento de cancelar la página
//------------------------------------------------------------------------------------------
function insCancel()
//------------------------------------------------------------------------------------------
{		    
	return true;
}

//% insPreZone: Se maneja la Acción para la Busqueda por Condición
//------------------------------------------------------------------------------------------
function insPreZone(llngAction)
//------------------------------------------------------------------------------------------
{
    return true;
}
//%ShowChangeValues: Evento OnChange de CbeBranch
//-----------------------------------------------------------------------------
function ShowChangeValues(Control)
//-----------------------------------------------------------------------------
{
	if(Control=="0"){
	    self.document.forms[0].valProduct.disabled=true
	    self.document.forms[0].tcnPolicy.disabled=true
	}
	else{
	    self.document.forms[0].cbeBranch.disabled=false
	    self.document.forms[0].valProduct.disabled=false
	    self.document.forms[0].tcnPolicy.disabled=false
	    self.document.forms[0].tcdOccurdate.disabled=false
   }     
}

//% insShowValues:Habilita o deshabilita el campo Certificado dependiendo del tipo de póliza pasada como parámetro.
//-------------------------------------------------------------------------------------------
function insShowValues(sField){
//-------------------------------------------------------------------------------------------

	with(self.document.forms[0]){
			if(tcnPolicy.value!="")
			    insDefValues("ShowCertif", "nPolicy=" + tcnPolicy.value)
	}
}

//%ShowPolicy: Busca el tipo de la póliza para habilitar y desabilitar el campo certificado.
//-----------------------------------------------------------------------------------------
function ShowPolicy(nBranch, nProduct, nPolicy)
//-----------------------------------------------------------------------------------------
{	
	if (nBranch.value!="" && nProduct.value!="" && nPolicy.value!="")	
		insDefValues('SIC001', 'sCertype=2' + '&nBranch=' + nBranch.value + '&nProduct=' + nProduct.value + '&nPolicy=' + nPolicy.value,'/VTimeNet/Claim/Claim')
}

//-----------------------------------------------------------------------------------------
function insDefValues(sKey,sParameters,sPath){
//-----------------------------------------------------------------------------------------
    if (typeof(top)!='undefined')
        if (typeof(top.frames)!='undefined')
            if (typeof(top.frames["fraGeneric"])!='undefined'){
                sPath = (typeof(sPath)=='undefined'?'':sPath + '/')
                sParameters = (typeof(sParameters)=='undefined'?'':'&' + sParameters)
                top.frames["fraGeneric"].location.href = sPath + 'ShowDefValues.aspx?Field=' + sKey  + sParameters;
            }
}
</SCRIPT>
</HEAD>

<BODY ONUNLOAD="closeWindows();">

<FORM METHOD="POST" ID="FORM" NAME="SIC002" ACTION="ValClaim.ASPX?sMode=1">
	<BR><BR>
    <TABLE WIDTH="100%" border=0>
		<TR>
			<TD WIDTH=10%></TD>
            <TD WIDTH=5%><LABEL ID=0>Ramo</LABEL></TD>
            <TD WIDTH=28%><%=mobjValues.BranchControl("cbeBranch", "Ramo al que pertenece la póliza a consultar. Valores según ramos comerciales definidos",  ,  ,  ,  ,  ,  , True, 1)%></TD>
            <TD WIDTH=5%><LABEL ID=0>Producto</LABEL></TD>
            <TD><%=mobjValues.ProductControl("valProduct", "Producto al que pertenece la póliza a consultar",  , eFunctions.Values.eValuesType.clngWindowType, True,  ,  ,  ,  ,  , 2, True)%></TD>            
        </TR>
		<TR>
			<TD WIDTH=10%></TD>
            <TD WIDTH=5%><LABEL ID=0>Póliza</LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnPolicy", 10, vbNullString,  , "Número identificativo de la póliza a consultar",  ,  ,  ,  ,  , "insShowValues(this);", True, 3)%></TD>
            <TD WIDTH=5%><LABEL ID=0>Certificado</LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnCertif", 10, "0",  , "Certificado a consultar",  , 0,  ,  ,  ,  , False, 4)%></TD>
        </TR>
		<TR>
			<TD WIDTH=10%></TD>
			<TD WIDTH=20%><LABEL ID=0>Ocurridos después de</LABEL></TD>
			<TD><%=mobjValues.DateControl("tcdOccurdate", CStr(System.Date.FromOADate(Today.ToOADate - 365)),  , "Limita la consulta a los siniestros ocurridos a partir de esta fecha",  ,  ,  ,  , True, 3)%></TD>
			<TD WIDTH=20%><LABEL ID=0>Fecha de origen póliza</LABEL></TD>
			<TD><%=mobjValues.DateControl("tcdDate_origi",  ,  , "Fecha de origen de la póliza",  ,  ,  ,  , True, 3)%></TD>
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
Call mobjNetFrameWork.FinishPage("sic002_k")
'UPGRADE_NOTE: Object mobjNetFrameWork may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




