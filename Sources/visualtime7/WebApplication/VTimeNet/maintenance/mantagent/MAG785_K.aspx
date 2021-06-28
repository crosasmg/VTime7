<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eGeneral" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo de las rutinas genéricas y menues
Dim mobjMenu As eFunctions.Menues

'- Objeto para el manejo de las rutinas genéricas y menues
Dim mobjGrid As eFunctions.Grid


'% insDefineHeader: se definen las características del grid
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	With mobjGrid.Columns
		Call .AddTextColumn(0, GetLocalResourceObject("tctType_ProceColumnCaption"), "tctType_Proce", 30, vbNullString,  , GetLocalResourceObject("tctType_ProceColumnToolTip"))
		Call .AddTextColumn(0, GetLocalResourceObject("tctIntertypColumnCaption"), "tctIntertyp", 30, vbNullString,  , GetLocalResourceObject("tctIntertypColumnToolTip"))
		Call .AddDateColumn(0, GetLocalResourceObject("tcdEffecdateColumnCaption"), "tcdEffecdate", CStr(eRemoteDB.Constants.dtmNull),  , GetLocalResourceObject("tcdEffecdateColumnToolTip"))
	End With
	
	'+ Se definen las columns del Grid
	With mobjGrid
		.Codispl = "MAG785"
		.Codisp = "MAG785"
		.sCodisplPage = "MAG785"
		.ActionQuery = True
		.Columns("Sel").GridVisible = False
	End With
End Sub

'% insPreMAG785: se realiza el manejo de los campos de la zona masiva de la transacción
'--------------------------------------------------------------------------------------------
Private Sub insPreMAG785()
	'--------------------------------------------------------------------------------------------
	Dim lcolCtrol_dateag As eGeneral.Ctrol_dateags
	Dim lclsCtrol_dateag As Object
	
	lcolCtrol_dateag = New eGeneral.Ctrol_dateags
	
	If lcolCtrol_dateag.Find() Then
		For	Each lclsCtrol_dateag In lcolCtrol_dateag
			With mobjGrid
				.Columns("tctType_Proce").DefValue = lclsCtrol_dateag.sType_proce
				.Columns("tctIntertyp").DefValue = lclsCtrol_dateag.sIntertyp
				.Columns("tcdEffecdate").DefValue = lclsCtrol_dateag.dEffecdate
			End With
			Response.Write(mobjGrid.DoRow())
		Next lclsCtrol_dateag
	End If
	Response.Write(mobjGrid.closeTable())
	
	lcolCtrol_dateag = Nothing
	lclsCtrol_dateag = Nothing
End Sub

</script>
<%Response.Expires = -1
mobjValues = New eFunctions.Values

mobjValues.sCodisplPage = "MAG785"
%>
<HTML>
<HEAD>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/valFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></SCRIPT>

<SCRIPT LANGUAGE="JavaScript">
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 1 $|$$Date: 2/09/03 19:02 $|$$Author: Iusr_llanquihue $"

//% insCancel: se controla la acción cancelar de la transacción
//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
	return true;
}
//% insFinish: se controla la acción finalizar de la transacción
//------------------------------------------------------------------------------------------
function insFinish(){
//------------------------------------------------------------------------------------------
	insReloadTop(false);
}
//% insStateZone: se controla el estado de los campos de la transacción
//------------------------------------------------------------------------------------------
function  insStateZone(){
//------------------------------------------------------------------------------------------
}
</SCRIPT>
<%
mobjMenu = New eFunctions.Menues
With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjMenu.MakeMenu("MAG785", "MAG785_K.aspx", 1, Request.QueryString.Item("sWindowDescript"), Session("sDesMultiCompany"), Session("sSche_code")))
End With
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="MAG785" ACTION="valMantAgent.aspx?sMode=1">
	<BR><BR>
<%
Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl")) & "<BR>")
Call insDefineHeader()
Call insPreMAG785()

mobjValues = Nothing
mobjMenu = Nothing
mobjGrid = Nothing
%>
</FORM>
</BODY>
</HTML>
<SCRIPT>
	self.document.A401.disabled=true;
</SCRIPT>




