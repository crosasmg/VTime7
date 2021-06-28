<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eClaim" %>
<script language="VB" runat="Server">
'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo de las rutinas genéricas    
Dim mobjMenu As eFunctions.Menues
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 10.31.20

Dim mobjNetFrameWork As eNetFrameWork.Layout

</script>
<%
Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("NC001_K ")
'~End Header Block VisualTimer Utility
Response.CacheControl = "private"
  Dim mobjMenu As Object

mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.31.39
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility
mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.31.39
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

%>
<HTML>
<HEAD>
<SCRIPT>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 15 $|$$Date: 14/06/05 13:01 $|$$Author: Nvaplat28 $"
</SCRIPT>
	<META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">
	<%=mobjValues.StyleSheet()%>
	<%'UPGRADE_NOTE: Language element 'SCRIPT' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file '/VTimeNet/Scripts/GenFunctions.js' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
	<%'UPGRADE_NOTE: Language element 'SCRIPT' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file '/VTimeNet/Scripts/tmenu.js' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></script>
	<%'UPGRADE_NOTE: Language element '#INCLUDE' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file 'VTime/Includes/Constantes.aspx' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<!-- #INCLUDE VIRTUAL="~/VTimeNet/Includes/Constantes.aspx" -->

    <%
'UPGRADE_NOTE: The 'eFunctions.Menues' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
'Dim mobjMenu as   eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.31.20
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility
Response.Write(mobjValues.StyleSheet())
Response.Write(mobjMenu.MakeMenu("NC001_K", "NC001_K.aspx", 1, vbNullString))
'UPGRADE_NOTE: Object mobjMenu may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjMenu = Nothing
%>
<SCRIPT>

//- Variable para el control de versiones
	document.VssVersion="$$Revision: 15 $|$$Date: 14/06/05 13:01 $"
		
//% insCancel: se controla la acción Cancelar de la página
//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
	return true;
}
//%insStateZone: se controla el estado de los campos de la página
//------------------------------------------------------------------------------------------
function insStateZone()
//------------------------------------------------------------------------------------------
{
	self.document.forms[0].elements[0].disabled = false;
	self.document.forms[0].elements[1].disabled = false;
	self.document.btncbeClient_Provider.disabled = false;
	//self.document.cbeCod_Provider.disabled = false;
	self.document.forms[0].elements[3].disabled = false;
	self.document.forms[0].elements[4].disabled = false;
	self.document.forms[0].elements[5].disabled = false;
	self.document.btn_tcdDate_Document.disabled = false;
	self.document.forms[0].elements[6].disabled = false;
	
}

//%insStateZone: se controla el estado de los campos de la página
//------------------------------------------------------------------------------------------
function ChangeValues(Field)
//------------------------------------------------------------------------------------------
{
	cliente = InsValuesCero(Field);
	
	self.document.forms[0].cbeClient_Provider.value = cliente;
	
		if(cliente != 0)		
		{
		    
			insDefValues("Provider", "sClient=" + cliente,'/VTimeNet/Document/DocumentTra','showdefnc001')
			self.document.forms[0].cbeCod_Provider.disabled = true;
		}else{
				self.document.forms[0].cbeCod_Provider.value = "";
				self.document.forms[0].cbeCod_Provider.disabled = true;
			 }

	
}



</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="NC001" ACTION="valNC001tra.aspx?mode=1">
    <BR><BR>
	<%=mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl"))%>
    <BR><BR>
    <TABLE WIDTH="100%">
    <TR>
    
        <TD><LABEL ID=9380>Tipo de documento</LABEL></TD>
        <TD><%mobjValues.TypeList = 1
mobjValues.List = "1,2,3,8"
Response.Write(mobjValues.PossiblesValues("cbeTypesupport", "Table5570", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  , True,  , "Tipo de documento"))%></TD>
		<TD><LABEL ID=9380>Proveedor</LABEL></TD>
		<TD><%'mobjValues.Parameters.Add "sclient", 1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable 
mobjValues.Parameters.ReturnValue("nProvider", True, "Codigo Prov.", True)
Response.Write(mobjValues.PossiblesValues("cbeClient_Provider", "TABPROVIDER_DOC_PAY", 2,  , False,  ,  ,  ,  , "ChangeValues(this);", True, 15, "Tabla de Proveedores"))%></TD>
	</TR>
    <TR>    
        <TD><LABEL ID=9380>Código proveedor</LABEL></TD> 
        <TD><%=mobjValues.NumericControl("cbeCod_Provider", 10,  ,  , "Codigo del proveedor",  ,  ,  ,  ,  ,  , True)%></TD> 
		<TD><LABEL ID=0>Número</LABEL></TD>
		<TD><%=mobjValues.NumericControl("tcnN_Document", 10,  ,  ,  ,  , 0,  ,  ,  ,  , True)%></TD> 
    </TR>
    <TR>    
	<TD><LABEL ID=0>Monto</LABEL></TD>
	<TD><%=mobjValues.NumericControl("tcnMount_Document", 12,  ,  , "Monto Documento", 1, 0,  ,  ,  ,  , True,  ,  , False)%></TD> 			
    <TD><LABEL ID=0>Fecha documento</LABEL></TD>
    <TD><%=mobjValues.DateControl("tcdDate_Document", "",  ,  ,  ,  ,  ,  , True)%></TD> 			
    </TR>
    <TR>
	<TD><LABEL ID="9123">Moneda</LABEL></TD>
	<TD><%mobjValues.BlankPosition = False
mobjValues.TypeList = 1
mobjValues.List = "1"
Response.Write(mobjValues.PossiblesValues("cbeCurrency", "Table11", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  , True,  , "Moneda en la que se realiza el pago"))%></TD>
	</TR>
    
    </TABLE> 
</FORM>
</BODY>
</HTML>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 10.31.20
Call mobjNetFrameWork.FinishPage("NC001_k")
'UPGRADE_NOTE: Object mobjNetFrameWork may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




