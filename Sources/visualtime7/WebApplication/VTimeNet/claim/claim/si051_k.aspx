<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 11.34.12
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
Dim mobjGrid As eFunctions.Grid
Dim mobjMenu As eFunctions.Menues
Dim lclsClaim As Object
Dim lcolClaims As Object


</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("si051_k")

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.34.12
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "si051_k"
mobjGrid = New eFunctions.Grid
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.34.12
mobjGrid.sSessionID = Session.SessionID
mobjGrid.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjGrid.sCodisplPage = "si051_k"
Call mobjGrid.SetWindowParameters(Request.QueryString("sCodispl"), Request.QueryString("sWindowDescript"), Request.QueryString("nWindowTy"))
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.34.12
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

%>
<SCRIPT>
//+Variable para el control de versiones
    document.VssVersion="$$Revision: 2 $|$$Date: 11/06/04 12:46 $"
</SCRIPT>

<HTML>
<HEAD>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">
    <%'UPGRADE_NOTE: Language element 'SCRIPT' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file '/VTimeNet/Scripts/GenFunctions.js' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
	<%'UPGRADE_NOTE: Language element 'SCRIPT' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file '/VTimeNet/Scripts/Constantes.js' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></SCRIPT>
    <%'UPGRADE_NOTE: Language element 'SCRIPT' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file '/VTimeNet/Scripts/tMenu.js' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tMenu.js"></SCRIPT>
    <%'UPGRADE_NOTE: Language element '#INCLUDE' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file 'VTime/Includes/Constantes.aspx' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<!-- #INCLUDE VIRTUAL="~/VTimeNet/Includes/Constantes.aspx" -->

<SCRIPT>
    document.VssVersion="$$Revision: 2 $|$$Date: 11/06/04 12:46 $"    

//% insStateZone: habilita los campos de la forma
//------------------------------------------------------------------------------------------
function insStateZone(){
//------------------------------------------------------------------------------------------
}

//% insCancel: Ejecuta rutinas necesarias en el momento de cancelar la página
//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
   return true
}

//% insFinish: Ejecuta rutinas necesarias en el momento de Finalizar la página
//------------------------------------------------------------------------------------------
function insFinish(){
//------------------------------------------------------------------------------------------
   return true
}
//% ClaimField: Limpia campo ramo-producto, si existe siniestro, si se ingresa ramo limpia siniestro
//------------------------------------------------------------------------------------------
function ClaimField(Field){
//------------------------------------------------------------------------------------------
var lstrQString

with (self.document.forms[0]) {

    if (Field.value != ''){
		lstrQString = 'nClaim=' + Field.value  
        insDefValues('ShowClaim',lstrQString,'/VTimeNet/Claim/Claim');
    }
 }
}
//% BranchField: Limpia campo ramo-producto, si existe siniestro, si se ingresa ramo limpia siniestro
//------------------------------------------------------------------------------------------
function BranchField(){
//------------------------------------------------------------------------------------------

with (self.document.forms[0]){
       if (cbeBranch.value != ''){
			valProduct.disabled = false
			tcnPolicy.disabled = false
			tcnClaim.value = ''
      }
      
 }
}


</SCRIPT>

    <%Response.Write(mobjValues.StyleSheet())
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.34.48
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility
Response.Write(mobjMenu.MakeMenu("SI051", "SI051_k.aspx", 1, Request.QueryString("sWindowDescript"), CStr(Session("sDesMultiCompany")), CStr(Session("sSche_code"))))
'UPGRADE_NOTE: Object mobjMenu may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjMenu = Nothing

%>    
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="frmClaimPaymentControl" ACTION="valClaim.aspx?sMode=1">
<BR><BR>
    <TABLE WIDTH="100%">
        <TR>
            
            <TR>
				<TD WIDTH="15%"><LABEL>Ramo</LABEL></TD>				
				<TD WIDTH="40%"><%=mobjValues.BranchControl("cbeBranch", "Ramo al que pertenecen los siniestros a mostrar", CStr(eRemoteDB.Constants.strNull),  ,  ,  ,  , "BranchField()")%></TD>
				<TD WIDTH="15%"><LABEL>Producto</LABEL></TD>
				<TD WIDTH="40%"><%=mobjValues.ProductControl("valProduct", "Producto al que pertenecen los siniestros a mostrar", CStr(eRemoteDB.Constants.strNull))%></TD>
			</TR>
			<TR>	
				<TD WIDTH="15%"><LABEL ID=0>Siniestro</LABEL></TD>
  				<TD WIDTH="40%"><%=mobjValues.NumericControl("tcnClaim", 10, CStr(eRemoteDB.Constants.strNull),  , "Número del siniestro que se desea procesar",  ,  ,  ,  ,  , "ClaimField(this);")%></TD>				    
  		    	<TD WIDTH="15%"><LABEL ID=0>Póliza</LABEL></TD>
				<TD WIDTH="40%"><%=mobjValues.NumericControl("tcnPolicy", 10,  ,  , "Número de póliza siniestrada")%></TD>
            </TR>	
            <TR>
				<TD WIDTH="15%"><LABEL>Fecha Desde</LABEL></TD>
				<TD WIDTH="20%"><%=mobjValues.DateControl("tcdInitial_date",  ,  , "Fecha desde de los movimientos de pago a mostrar")%></TD>
				<TD WIDTH="25%"><LABEL>Fecha Hasta</LABEL></TD>
				<TD WIDTH="25%"><%=mobjValues.DateControl("tcdFinal_date",  ,  , "Fecha hasta de los movimientos de pago a mostrar")%></TD>
			</TR>
		</TR>
    </TABLE>
<%
'UPGRADE_NOTE: Object mobjValues may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjValues = Nothing
%>
</FORM>
</BODY>
</HTML>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 11.34.48
Call mobjNetFrameWork.FinishPage("si051_k")
'UPGRADE_NOTE: Object mobjNetFrameWork may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




