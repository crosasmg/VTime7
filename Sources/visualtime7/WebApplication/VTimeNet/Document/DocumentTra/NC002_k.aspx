<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eClaim" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
 Dim mobjValues As New eFunctions.Values
    Dim mobjGrid As New eFunctions.Grid
    Dim mobjMenues As New eFunctions.Menues
    
    Dim mobjNetFrameWork As New eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

Dim mstrCodisplOri As Object
Dim mstrCertype As Object
Dim mobjRequest As Object
Dim mstrRehab_notrec As Object


</script>
<%'UPGRADE_NOTE: The 'eFunctions.Values' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm

'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.31.20
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "NC002_K"
Response.Expires = -1441
'UPGRADE_NOTE: The 'eNetFrameWork.Layout' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm

mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("NC002_k")

%>
<HTML>
<HEAD>
<SCRIPT>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 15 $|$$Date: 14/06/05 13:01 $|$$Author: Nvaplat28 $"
</SCRIPT>
	<META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">
    <SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
	<%'UPGRADE_NOTE: Language element 'SCRIPT' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file '/VTimeNet/Scripts/tmenu.js' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></script>
	<%=mobjValues.StyleSheet()%>


    <%
'UPGRADE_NOTE: The 'eFunctions.Menues' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm

Dim mobjMenu  as new  eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.31.20
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility
Response.Write(mobjMenu.MakeMenu("NC002", "NC002_k.aspx", 1, vbNullString))
'UPGRADE_NOTE: Object mobjMenu may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjMenu = Nothing


Session("nTypesupport") = ""
Session("sClient") = ""
Session("nDocument") = ""
Session("nStatus") = ""
Session("dDate_dStatus1") = ""
Session("dDate_dStatus2") = ""
Session("nCodeuser") = ""
Session("chkReport") = ""

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
function insStateZone(){
//------------------------------------------------------------------------------------------

	self.document.forms[0].elements[0].disabled = false;
	self.document.forms[0].elements[1].disabled = false;
	self.document.btncbeClient_Provider.disabled = false;
	self.document.forms[0].elements[2].disabled = false;
	self.document.forms[0].elements[3].disabled = false;
	self.document.forms[0].elements[4].disabled = false;
	self.document.btn_tcdDate_dStatus1.disabled = false;
	self.document.forms[0].elements[5].disabled = false;
	self.document.btn_tcdDate_dStatus2.disabled = false;
	self.document.forms[0].elements[6].disabled = false;
	self.document.forms[0].elements[7].disabled = false;
	self.document.forms[0].elements[8].disabled = false;
	self.document.btntcnUsercode.disabled = false;

}

//%ChangeValues: se formatea el campo proveedor
//------------------------------------------------------------------------------------------
function ChangeValues(Field)
//------------------------------------------------------------------------------------------
{
	cliente = InsValuesCero(Field);
	
	self.document.forms[0].cbeClient_Provider.value = cliente;
	
}
</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="NC002" ACTION="valNC002tra.aspx?mode=1">
	<BR></BR>
<%
Response.Write(mobjValues.ShowWindowsName("NC002"))

%>
    <BR><BR>

	<TABLE WIDTH=100%>
    <TR>
        <TD><LABEL ID=9380>Tipo de documento</LABEL></TD> 
        <TD><%
        mobjValues.TypeList = 1
mobjValues.List = "1,2,3,8"
Response.Write(mobjValues.PossiblesValues("cbeTypesupport", "Table5570", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  , True,  , "Tipo de documento"))%></TD> 
		<TD><LABEL ID=9380>Proveedor</LABEL></TD> 
		<TD><%'mobjValues.Parameters.Add "sclient", 1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable 
mobjValues.Parameters.ReturnValue("nProvider", True, "Codigo Prov.", True)
Response.Write(mobjValues.PossiblesValues("cbeClient_Provider", "TABPROVIDER_DOC_PAY", 2,  , False,  ,  ,  ,  , "ChangeValues(this)", True, 15, "Tabla de Proveedores"))%></TD> 	
    </TR>
    <TR>    
		<TD><LABEL ID=0>Número</LABEL></TD>
		<TD><%=mobjValues.NumericControl("tcnN_Document", 10,  ,  , "Numero documento",  , 0,  ,  ,  ,  , True)%></TD> 
		<TD><LABEL ID=9380>Estado</LABEL></TD> 
		<TD><%mobjValues.TypeList = 1
mobjValues.List = "2,3,6"
Response.Write(mobjValues.PossiblesValues("cbeStatus", "Table334", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  , True,  , "Estado del documento"))%></TD> 
	</TR>
	
	<TR>    
		<TD><LABEL ID=0>Fecha Registro Desde</LABEL></TD>
		<TD><%=mobjValues.DateControl("tcdDate_dStatus1",  ,  , "Fecha consulta desde",  ,  ,  ,  , True)%>  
			<LABEL ID=0>Hasta </LABEL>
		    <%=mobjValues.DateControl("tcdDate_dStatus2",  ,  , "Fecha consulta hasta",  ,  ,  ,  , True)%>  
		</TD> 
		<TD><LABEL ID=9380>Codigo Usuario</LABEL></TD> 
		<TD><%=mobjValues.PossiblesValues("tcnUsercode", "tabUsers", eFunctions.Values.eValuesType.clngWindowType, vbNullString,  ,  ,  ,  ,  ,  , True, 5, "Código que identifica al usuario en el sistema",  ,  ,  , True)%></TD>
	</TR>
		
	<TR>    
	<TD><%Response.Write(mobjValues.CheckControl("chkReport", "Generar reporte",  ,  ,  , True))%></TD></TR>
	</TABLE>
</FORM>
</BODY>
</HTML>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 10.31.20
Call mobjNetFrameWork.FinishPage("NC002_k")
'UPGRADE_NOTE: Object mobjNetFrameWork may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
    mobjNetFrameWork = Nothing
    mobjValues = Nothing
    mobjMenu = Nothing
'^End Footer Block VisualTimer%>




