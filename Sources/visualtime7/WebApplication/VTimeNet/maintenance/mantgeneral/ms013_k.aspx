<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eGeneral" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Se define la variable mobjGrid para el manejo del Grid de la ventana
Dim mobjGrid As eFunctions.Grid

'- Objeto para el manejo de las zonas de la página
Dim mobjMenu As eFunctions.Menues


'%insDefineheader. Definición de columnas del GRID
'--------------------------------------------------------------------------------------------
Private Sub insDefineheader()
	'--------------------------------------------------------------------------------------------        
	mobjGrid = New eFunctions.Grid
	
	'+Se definen todas las columnas del Grid
	With mobjGrid.Columns
		Call .AddTextColumn(0, GetLocalResourceObject("tcsTab_codeColumnCaption"), "tcsTab_code", 20, "", True, GetLocalResourceObject("tcsTab_codeColumnToolTip"),  ,  ,  , False)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnCount_itemColumnCaption"), "tcnCount_item", 4, CStr(0), True, GetLocalResourceObject("tcnCount_itemColumnToolTip"), False)
		Call .AddTextColumn(0, GetLocalResourceObject("tcsCode_itemColumnCaption"), "tcsCode_item", 20, "", True, GetLocalResourceObject("tcsCode_itemColumnToolTip"),  ,  ,  , False)
		Call .AddTextColumn(0, GetLocalResourceObject("tcsDesc_itemColumnCaption"), "tcsDesc_item", 12, "", True, GetLocalResourceObject("tcsDesc_itemColumnToolTip"),  ,  ,  , False)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnCount_tablColumnCaption"), "tcnCount_tabl", 4, CStr(0), True, GetLocalResourceObject("tcnCount_tablColumnToolTip"), False)
		Call .AddTextColumn(0, GetLocalResourceObject("tcsDeSCRIPTColumnCaption"), "tcsDeSCRIPT", 30, "", True, GetLocalResourceObject("tcsDeSCRIPTColumnToolTip"),  ,  ,  , False)
		Call .AddTextAreaColumn(0, GetLocalResourceObject("tcsDs_selectColumnCaption"), "tcsDs_select", "", 4, 45, True, GetLocalResourceObject("tcsDs_selectColumnToolTip"),  , False)
		Call .AddTextColumn(0, GetLocalResourceObject("tcsQ_valueColumnCaption"), "tcsQ_value", 1, "", True, GetLocalResourceObject("tcsQ_valueColumnToolTip"),  ,  ,  , False)
		Call .AddCheckColumn(0, GetLocalResourceObject("tcsShowNumColumnCaption"), "tcsShowNum", "")
		Call .AddTextColumn(0, GetLocalResourceObject("tcsInitQueryColumnCaption"), "tcsInitQuery", 1, "", True, GetLocalResourceObject("tcsInitQueryColumnToolTip"),  ,  ,  , False)
		Call .AddCheckColumn(0, GetLocalResourceObject("tcsIndSpColumnCaption"), "tcsIndSp", "")
		Call .AddTextColumn(0, GetLocalResourceObject("tcsKeyColumnCaption"), "tcsKey", 20, "", True, GetLocalResourceObject("tcsKeyColumnToolTip"),  ,  ,  , False)
		
	End With
	
	With mobjGrid
		.Codispl = "MS013"
		.Codisp = "MS013"
		.sCodisplPage = "MS013"
		.Top = 100
		.Height = 500
		.Width = 750
		.bOnlyForQuery = Request.QueryString.Item("nMainAction") = "401"
		.ActionQuery = (Request.QueryString.Item("nMainAction") = "401" Or IsNothing(Request.QueryString.Item("nMainAction")))
		.Columns("Sel").GridVisible = Not .ActionQuery
		.Columns("tcsTab_code").EditRecord = True
		.Columns("tcsTab_code").Disabled = Request.QueryString.Item("Action") = "Update"
		.Columns("tcnCount_item").GridVisible = False
		.Columns("tcsCode_item").GridVisible = False
		.Columns("tcnCount_tabl").GridVisible = False
		.Columns("tcsDs_select").GridVisible = False
		.Columns("tcsQ_value").GridVisible = False
		.Columns("tcsShowNum").GridVisible = False
		.Columns("tcsInitQuery").GridVisible = False
		.Columns("tcsIndSp").GridVisible = False
		.Columns("tcsKey").GridVisible = False
		.sDelRecordParam = "p_nAction='+ marrArray[lintIndex].tcsTab_code + '"
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
End Sub

'%insPreMS013: Esta función se encarga de cargar los datos en la forma "Folder" 
'--------------------------------------------------------------------------------------------
Private Sub insPreMS013()
	'--------------------------------------------------------------------------------------------
	Dim lcolTab_tabless As eGeneral.Tab_tabless
	Dim lclsTab_tables As eGeneral.Tab_tables
	lclsTab_tables = New eGeneral.Tab_tables
	lcolTab_tabless = New eGeneral.Tab_tabless
	If lcolTab_tabless.Find("") Then
		For	Each lclsTab_tables In lcolTab_tabless
			With mobjGrid
				.Columns("tcsTab_code").DefValue = lclsTab_tables.stab_code
				
				.Columns("tcnCount_item").DefValue = CStr(lclsTab_tables.nCount_item)
				.Columns("tcsCode_item").DefValue = lclsTab_tables.sCode_item
				
				.Columns("tcsDesc_item").DefValue = lclsTab_tables.sDesc_item
				
				.Columns("tcnCount_tabl").DefValue = CStr(lclsTab_tables.nCount_tabl)
				.Columns("tcsDeSCRIPT").DefValue = lclsTab_tables.sDeSCRIPT
				.Columns("tcsDs_select").DefValue = lclsTab_tables.sDs_select
				.Columns("tcsQ_value").DefValue = lclsTab_tables.sQ_value
				.Columns("tcsShowNum").Checked = CShort(lclsTab_tables.sShowNum)
				
				.Columns("tcsInitQuery").DefValue = lclsTab_tables.sInitQuery
				.Columns("tcsIndSp").Checked = CShort(lclsTab_tables.sIndSp)
				.Columns("tcsKey").DefValue = lclsTab_tables.sKey
			End With
			'+Se ejecuta el metodo DoRow, que se encarga de mostrar los elementos de grid
			Response.Write(mobjGrid.DoRow())
		Next lclsTab_tables
		Response.Write(mobjGrid.closeTable())
	End If
	lclsTab_tables = Nothing
	lcolTab_tabless = Nothing
	
End Sub

'% insPreMS013Upd. Se define esta funcion para contruir el contenido de la ventana UPD de los archivos de datos particulares
'----------------------------------------------------------------------------------------------------------------------------
Private Sub insPreMS013Upd()
	'----------------------------------------------------------------------------------------------------------------------------
	Dim lclsTab_tables As eGeneral.Tab_tables
	Dim lstrErrors As String
	With Request
		If .QueryString.Item("Action") = "Del" Then
			lclsTab_tables = New eGeneral.Tab_tables
			lstrErrors = lclsTab_tables.insValMS013_K(.QueryString.Item("Action"), .QueryString.Item("sCodispl"), .QueryString.Item("p_nAction"), 1, "sCode_item", "sDesc_item", 1, "sDeSCRIPT", "sDs_select", "sQ_value", Session("nUsercode"), "sShowNum", "sInitQuery", "sIndSp", "sKey")
			
			If lstrErrors = vbNullString Then
				Response.Write(mobjValues.ConfirmDelete())
				lclsTab_tables.stab_code = .QueryString.Item("p_nAction")
				lclsTab_tables.Delete()
			Else
				Response.Write(lstrErrors)
			End If
			lclsTab_tables = Nothing
		End If
	End With
	Response.Write(mobjGrid.DoFormUpd(Request.QueryString.Item("Action"), "valMantGeneral.aspx", Request.QueryString.Item("sCodispl"), Request.QueryString.Item("MainAction"),  , CShort(Request.QueryString.Item("Index"))))
	Response.Write(mobjValues.HiddenControl("sAction", Request.QueryString.Item("Action")))
End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values

mobjValues.sCodisplPage = "MS013"
%>
<HTML>
<HEAD>
    <meta NAME="GENERATOR" Content="eTransaction Designer for Visual TIME">
<SCRIPT LANGUAGE="JavaSCRIPT" SRC="/VTimeNet/SCRIPTs/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaSCRIPT" SRC="/VTimeNet/SCRIPTs/Constantes.js"></SCRIPT>


<%
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write("<SCRIPT>var nMainAction = 0" & Request.QueryString.Item("nMainAction") & "</SCRIPT>")
	Response.Write("<SCRIPT LANGUAGE=""JavaSCRIPT"" SRC=""/VTimeNet/SCRIPTs/tmenu.js""></SCRIPT>" & vbCrLf)
End If
With Response
	.Write(mobjValues.StyleSheet() & vbCrLf)
	If Request.QueryString.Item("Type") <> "PopUp" Then
		mobjMenu = New eFunctions.Menues
		.Write(mobjMenu.MakeMenu(Request.QueryString.Item("sCodispl"), "MS013_k.aspx", 1, ""))
		mobjMenu = Nothing
	End If
End With
%>
<SCRIPT>

//% insCancel: Ejecuta rutinas necesarias en el momento de cancelar la página
//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
    return true;
}
//% insStateZone: se manejan los campos de la página
//------------------------------------------------------------------------------------------
function insStateZone(){
//------------------------------------------------------------------------------------------      
    return true;
}
//% insPreZone: Se maneja la Acción para la Busqueda por Condición
//-------------------------------------------------------------------------------------------------------------------
function insPreZone(llngAction){
//-------------------------------------------------------------------------------------------------------------------
    switch (llngAction){
        case 302:
        case 305:
        case 401:
	        document.location.href = document.location.href.replace(/&nMainAction.*/,'') + '&nMainAction=' + llngAction            
            break;
    }
}
</SCRIPT> 
</HEAD>

<BODY ONUNLOAD="closeWindows();">
<%
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write("<BR><BR>")
End If
Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl")))
%>
<FORM METHOD="post" ID="FORM" NAME="frmTab_tablesSys" ACTION="valMantGeneral.aspx?sTime=1">
<%
Call insDefineheader()
If Request.QueryString.Item("Type") <> "PopUp" Then
	Call insPreMS013()
Else
	Call insPreMS013Upd()
End If
mobjValues = Nothing
mobjGrid = Nothing
%>
</FORM>
</BODY>
</HTML>






