<%@ Page Language="VB" explicit="true" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eSaapv" %>
<script language="VB" runat="Server">

'**- Object for the handling of the general functions of load of values.
'- Objeto para el manejo de las funciones generales de carga de valores.

Dim mobjValues As eFunctions.Values
Dim mobjGrid As eFunctions.Grid
Dim mobjMenu As eFunctions.Menues


'**% insDefineHeader: This function defined the GRID fields.
'% insDefineHeader: Configura los datos del grid.
'--------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------
	
	'**+ The columns of the GRID are defined
	'+ Se definen las columnas del grid    
	
	With mobjGrid.Columns
		.AddPossiblesColumn(0, "Tipo de SAAPV", "cbeType_saapv", "Table5742", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  ,  , "Tipo de SAAPV: Prop/Pol, Deco, Tegr, Ting, Modp.")
		.AddPossiblesColumn(0, "Estado desde", "cbeType_state_origi", "Table5741", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  ,  , "Estado origen de un SAAPV.")
		.AddPossiblesColumn(0, "Estado hasta", "cbeType_state_end", "Table5741", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  ,  , "Estado al cual puede pasar el registro anterior(Estado desde).")
	End With
	
	'**+ The properties of the GRID are defined
	'+ Se definen las propiedades generales del grid
	
	With mobjGrid
		.Codispl = "MVI7500"
		.Codisp = "MVI7500_K"
		.sCodisplPage = "MVI7500"
		If Request.QueryString("nMainAction") = "401" Or Request.QueryString("nMainAction") = vbNullString Then
			mobjGrid.ActionQuery = True
			mobjGrid.Columns("Sel").GridVisible = False
		Else
			.Columns("cbeType_saapv").EditRecord = True
		End If
		.Top = 200
		.Left = 300
		.Height = 250
		.Width = 350
		.sDelRecordParam = "nType_saapv='+ marrArray[lintIndex].cbeType_saapv + '"
		If Request.QueryString("Reload") = "1" Then
			.sReloadIndex = Request.QueryString("ReloadIndex")
		End If
		If Request.QueryString("Reload") = "1" Then
			.sReloadIndex = Request.QueryString("ReloadIndex")
		End If
	End With
End Sub

'**% insPreMVI7500: Get the information of the investment funds
'% insPreMVI7500: Obtiene los datos de los fondos de inversión
'--------------------------------------------------------------------------------------
Private Sub insPreMVI7500()
	'--------------------------------------------------------------------------------------
	Dim lclsTab_state_saapv As eSaapv.Tab_state_saapv
	Dim lcolTab_state_saapvs As eSaapv.Tab_state_saapvs
	
	With Server
		lclsTab_state_saapv = New eSaapv.Tab_state_saapv
		lcolTab_state_saapvs = New eSaapv.Tab_state_saapvs
	End With
	
	If lcolTab_state_saapvs.Find() Then
		With mobjGrid
			For	Each lclsTab_state_saapv In lcolTab_state_saapvs
				.Columns("cbeType_saapv").DefValue = CStr(lclsTab_state_saapv.nType_saapv)
				.Columns("cbeType_state_origi").DefValue = CStr(lclsTab_state_saapv.nType_state_origi)
				.Columns("cbeType_state_end").DefValue = CStr(lclsTab_state_saapv.nType_state_end)
				
				Response.write(.DoRow)
			Next lclsTab_state_saapv
		End With
	End If
	
	Response.write(mobjGrid.closeTable)
	
	lcolTab_state_saapvs = Nothing
	lclsTab_state_saapv = Nothing
End Sub

'**% insPreMVI7500Upd: This function allows to make the reading of the table.
'% insPreMVI7500Upd: Esta función permite realizar la lectura de la tabla.
'------------------------------------------------------------------------------
Private Sub insPreMVI7500Upd()
	'------------------------------------------------------------------------------
	Dim lclsTab_state_saapv As eSaapv.Tab_state_saapv
	lclsTab_state_saapv = New eSaapv.Tab_state_saapv
	
	With Request
		If Request.QueryString("Action") = "Del" Then
			Response.write(mobjValues.ConfirmDelete)
			
			Call lclsTab_state_saapv.insPostMVI7500(Request.QueryString("Action"), mobjValues.StringToType(Request.QueryString("nType_saapv"), eFunctions.Values.eTypeData.etdLong), 0, 0, mobjValues.StringToType(CStr(Session("nUsercode")), eFunctions.Values.eTypeData.etdLong))
			
		End If
	End With
	
	Response.write(mobjGrid.DoFormUpd(Request.QueryString("Action"), "valMantNoTraLife.aspx", "MVI7500", Request.QueryString("nMainAction"), mobjValues.ActionQuery, Request.QueryString("Index")))
	
	'UPGRADE_NOTE: Object lclsTab_state_saapv may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
	lclsTab_state_saapv = Nothing
End Sub

</script>
<%Response.Expires = -1

With Server
	mobjValues = New eFunctions.Values
	mobjGrid = New eFunctions.Grid
	mobjMenu = New eFunctions.Menues
End With

mobjValues.sCodisplPage = "MVI7500"
%>
<HTML>
<HEAD>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0">
    <SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<!-- #INCLUDE VIRTUAL="~/VTimeNet/Includes/Constantes.aspx" -->

<%
If Request.QueryString("Type") <> "PopUp" Then
%>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></SCRIPT>
<%	
End If
%>
<%
Response.write(mobjValues.StyleSheet())
If Request.QueryString("Type") <> "PopUp" Then
	Response.write(mobjMenu.MakeMenu(Request.QueryString("sCodispl"), "MVI7500_K.aspx", 1, ""))
	Response.write("<BR></BR>")
	mobjMenu = Nothing
	Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
End If
%>
<SCRIPT LANGUAGE=JavaScript>
//+ Variable para el control de versiones
    document.VssVersion="$$Revision: 2 $|$$Date: 31/10/03 11:38 $"
    
//% insStateZone: se controla el estado de los campos de la página
//--------------------------------------------------------------------------------------------
function insStateZone(){
//--------------------------------------------------------------------------------------------
}
//% insCancel: se controla la acción Cancelar de la página
//--------------------------------------------------------------------------------------------
function insCancel(){
//--------------------------------------------------------------------------------------------
	return true;
}
//% insPreZone: Define ubicacion de documento
//-------------------------------------------------------------------------------------------------------------------
function insPreZone(llngAction){
//-------------------------------------------------------------------------------------------------------------------
	switch (llngAction){
	    case 301:
	    case 302:
	    case 401:
	        document.location.href = document.location.href.replace(/&nMainAction.*/,'') + '&nMainAction=' + llngAction
	        break;
	}
 }
//% insFinish: se controla la acción Cancelar de la página
//--------------------------------------------------------------------------------------------
function insFinish(){
//--------------------------------------------------------------------------------------------
    return true;
}
</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="frmDBVehicle" ACTION="valMantNoTraLife.aspx?mode=1&nMainAction=<%=Request.QueryString("nMainAction")%>">
<%Response.write(mobjValues.ShowWindowsName("MVI7500"))

mobjGrid = New eFunctions.Grid
Call insDefineHeader()

If Request.QueryString("Type") = "PopUp" Then
	Call insPreMVI7500Upd()
Else
	Call insPreMVI7500()
End If
mobjValues = Nothing
mobjGrid = Nothing
%>
</FORM> 
</BODY>
</HTML>






