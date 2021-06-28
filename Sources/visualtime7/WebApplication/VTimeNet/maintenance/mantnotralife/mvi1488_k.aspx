<%@ Page Language="VB" explicit="true" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eSaapv" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo del grid de la página
Dim mobjGrid As eFunctions.Grid

'- Objeto para el manejo del menú
Dim mobjMenu As eFunctions.Menues

'- Objeto para el manejo particular de los datos de la página
Dim mcolTab_req_docs As Object


'**% insDefineHeader: This function defined the GRID fields.
'% insDefineHeader: Configura los datos del grid.
'--------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------
	
	'**+ The columns of the GRID are defined
	'+ Se definen las columnas del grid    
	
	With mobjGrid.Columns
		.AddPossiblesColumn(0, "Tipo de Movimiento", "cbeType_move", "Table5708", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  , Request.QueryString("Action") = "Update",  , "Tipo de movimiento de cuenta corriente.")
		.AddPossiblesColumn(0, "Cuenta origen", "cbeOrigin", "Table5633", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  , Request.QueryString("Action") = "Update",  , "Cuenta de origen de los depósitos.")
		.AddPossiblesColumn(0, "Régimen tributario", "cbeTyp_profitworker", "Table950", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  , Request.QueryString("Action") = "Update",  , "Tipo de beneficio tributario.")
		.AddNumericColumn(0, "Tipo de movimiento RH", "tcnTransac", 5, "",  , "Tipo de movimiento para el registro histórico (Previred).", False)
	End With
	
	'**+ The properties of the GRID are defined
	'+ Se definen las propiedades generales del grid
	
	With mobjGrid
		.Codispl = "MVI1488"
		.Codisp = "MVI1488_K"
		.sCodisplPage = "MVI1488"
		If Request.QueryString("nMainAction") = "401" Or Request.QueryString("nMainAction") = vbNullString Then
			mobjGrid.ActionQuery = True
			mobjGrid.Columns("Sel").GridVisible = False
		Else
			.Columns("cbeType_move").EditRecord = True
		End If
		.Top = 200
		.Left = 300
		.Height = 230
		.Width = 470
		.WidthDelete = 420
		.sDelRecordParam = "nType_move='+ marrArray[lintIndex].cbeType_move + '" & "&nOrigin='+ marrArray[lintIndex].cbeOrigin + '" & "&nTyp_profitworker='+ marrArray[lintIndex].cbeTyp_profitworker + '"
		If Request.QueryString("Reload") = "1" Then
			.sReloadIndex = Request.QueryString("ReloadIndex")
		End If
		If Request.QueryString("Reload") = "1" Then
			.sReloadIndex = Request.QueryString("ReloadIndex")
		End If
	End With
End Sub

'% insPreMVI1488: Obtiene los datos de la matriz de transacciones RH Previred
'--------------------------------------------------------------------------------------
Private Sub insPreMVI1488()
	'--------------------------------------------------------------------------------------
	Dim lclsTab_matrix_rh As eSaapv.Tab_matrix_rh
	Dim lcolTab_matrix_rhs As eSaapv.Tab_matrix_rhs
	
	With Server
		lclsTab_matrix_rh = New eSaapv.Tab_matrix_rh
		lcolTab_matrix_rhs = New eSaapv.Tab_matrix_rhs
	End With
	
	If lcolTab_matrix_rhs.Find() Then
		With mobjGrid
			For	Each lclsTab_matrix_rh In lcolTab_matrix_rhs
				.Columns("cbeType_move").DefValue = CStr(lclsTab_matrix_rh.nType_move)
				.Columns("cbeOrigin").DefValue = CStr(lclsTab_matrix_rh.nOrigin)
				.Columns("cbeTyp_profitworker").DefValue = CStr(lclsTab_matrix_rh.nTyp_profitworker)
				.Columns("tcnTransac").DefValue = CStr(lclsTab_matrix_rh.nTransac)
				
				Response.write(.DoRow)
			Next lclsTab_matrix_rh
		End With
	End If
	
	Response.write(mobjGrid.closeTable)
	
	'UPGRADE_NOTE: Object lcolTab_matrix_rhs may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
	lcolTab_matrix_rhs = Nothing
	'UPGRADE_NOTE: Object lclsTab_matrix_rh may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
	lclsTab_matrix_rh = Nothing
End Sub

'**% insPreMVI1488Upd: This function allows to make the reading of the table.
'% insPreMVI1488Upd: Esta función permite realizar la lectura de la tabla.
'------------------------------------------------------------------------------
Private Sub insPreMVI1488Upd()
	'------------------------------------------------------------------------------
	Dim lclsTab_matrix_rh As eSaapv.Tab_matrix_rh
	lclsTab_matrix_rh = New eSaapv.Tab_matrix_rh
	
	With Request
		If Request.QueryString("Action") = "Del" Then
			Response.write(mobjValues.ConfirmDelete)
			
			Call lclsTab_matrix_rh.insPostMVI1488(Request.QueryString("Action"), mobjValues.StringToType(Request.QueryString("nType_move"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(Request.QueryString("nOrigin"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(Request.QueryString("nTyp_profitworker"), eFunctions.Values.eTypeData.etdLong), 0, mobjValues.StringToType(CStr(Session("nUsercode")), eFunctions.Values.eTypeData.etdLong))
			
		End If
	End With
	
	Response.write(mobjGrid.DoFormUpd(Request.QueryString("Action"), "valMantNoTraLife.aspx", "MVI1488", Request.QueryString("nMainAction"), mobjValues.ActionQuery, Request.QueryString("Index")))
	
	'UPGRADE_NOTE: Object lclsTab_matrix_rh may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
	lclsTab_matrix_rh = Nothing
End Sub

</script>
<%Response.Expires = -1

With Server
	mobjValues = New eFunctions.Values
	mobjMenu = New eFunctions.Menues
End With
mobjValues.sCodisplPage = "MVI1488"
%>
<HTML>
<HEAD>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0">
	<%'UPGRADE_NOTE: Language element 'SCRIPT' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file '/VTimeNet/Scripts/GenFunctions.js' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
    <%'UPGRADE_NOTE: Language element '#INCLUDE' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file 'VTime/Includes/Constantes.aspx' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<!-- #INCLUDE VIRTUAL="~/VTimeNet/Includes/Constantes.aspx" -->

<%
If Request.QueryString("Type") <> "PopUp" Then
	%>
	   <%	'$$EWI_1012:C:\NetMigra\Result\App\VTimeStep1\maintenance\mantnotralife\VTime\Scripts\tmenu.js#%>
<%	'UPGRADE_NOTE: Language element 'SCRIPT' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%	'UPGRADE_NOTE: The file '/VTimeNet/Scripts/tmenu.js' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></SCRIPT>
<%	
End If
%>
<%
Response.write(mobjValues.StyleSheet())
If Request.QueryString("Type") <> "PopUp" Then
	Response.write(mobjMenu.MakeMenu(Request.QueryString("sCodispl"), "MVI1488_K.aspx", 1, ""))
	Response.write("<BR></BR>")
	'UPGRADE_NOTE: Object mobjMenu may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
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
<%Response.write(mobjValues.ShowWindowsName("MVI1488"))

mobjGrid = New eFunctions.Grid
Call insDefineHeader()

If Request.QueryString("Type") = "PopUp" Then
	Call insPreMVI1488Upd()
Else
	Call insPreMVI1488()
End If
'UPGRADE_NOTE: Object mobjValues may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjValues = Nothing
'UPGRADE_NOTE: Object mobjGrid may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjGrid = Nothing
%>
</FORM> 
</BODY>
</HTML>






