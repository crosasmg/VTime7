<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eBranches" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo del grid de la página
Dim mobjGrid As eFunctions.Grid

'- Objeto para el manejo del menú
Dim mobjMenu As eFunctions.Menues

'- Objeto para el manejo particular de los datos de la página
Dim mcolCrit_sorts As eBranches.Crit_sorts


'% insDefineHeader: se definen las propiedades del grid
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	
	'+ Se definen las columnas del grid 
	With mobjGrid.Columns
		
		If Request.QueryString.Item("Action") = "Add" Then
			If Request.QueryString.Item("Type") = "PopUp" Then
				Call .AddNumericColumn(0, GetLocalResourceObject("tcnCrthecniColumnCaption"), "tcnCrthecni", 5, vbNullString,  , GetLocalResourceObject("tcnCrthecniColumnToolTip"),  ,  ,  ,  ,  , True)
				Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeCrthecniColumnCaption"), "cbeCrthecni", "Table32", eFunctions.Values.eValuesType.clngComboType, CStr(1),  ,  ,  ,  ,  , False,  , GetLocalResourceObject("cbeCrthecniColumnToolTip"))
			Else
				Call .AddNumericColumn(0, GetLocalResourceObject("tcnCrthecniColumnCaption"), "tcnCrthecni", 5, vbNullString,  , GetLocalResourceObject("tcnCrthecniColumnToolTip"),  ,  ,  ,  ,  , True)
				Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeCrthecniColumnCaption"), "cbeCrthecni", "Table32", eFunctions.Values.eValuesType.clngComboType, CStr(1),  ,  ,  ,  ,  , False,  , GetLocalResourceObject("cbeCrthecniColumnToolTip"))
			End If
		Else
			If Request.QueryString.Item("Type") = "PopUp" Then
				Call .AddNumericColumn(0, GetLocalResourceObject("tcnCrthecniColumnCaption"), "tcnCrthecni", 5, vbNullString,  , GetLocalResourceObject("tcnCrthecniColumnToolTip"),  ,  ,  ,  ,  , True)
				Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeCrthecniColumnCaption"), "cbeCrthecni", "Table32", eFunctions.Values.eValuesType.clngComboType, CStr(1),  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeCrthecniColumnToolTip"))
			Else
				Call .AddNumericColumn(0, GetLocalResourceObject("tcnCrthecniColumnCaption"), "tcnCrthecni", 5, vbNullString,  , GetLocalResourceObject("tcnCrthecniColumnToolTip"),  ,  ,  ,  ,  , True)
				Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeCrthecniColumnCaption"), "cbeCrthecni", "Table32", eFunctions.Values.eValuesType.clngComboType, CStr(1),  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeCrthecniColumnToolTip"))
			End If
		End If
		
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnRandomColumnCaption"), "tcnRandom", 5, CStr(eRemoteDB.Constants.intNull),  , GetLocalResourceObject("tcnRandomColumnToolTip"),  ,  ,  ,  ,  , False)
		Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeStatregtColumnCaption"), "cbeStatregt", "Table26", eFunctions.Values.eValuesType.clngComboType, CStr(1),  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeStatregtColumnToolTip"))
		Call .AddHiddenColumn("hddsSolic", "2")
		Call .AddHiddenColumn("hddnCount", "1")
	End With
	
	'+ Se definen las propiedades generales del Grid 
	With mobjGrid
		.Codispl = "MVI816"
		.Codisp = "MVI816"
		.sCodisplPage = "MVI816"
		If Request.QueryString.Item("nMainAction") = "401" Or Request.QueryString.Item("nMainAction") = vbNullString Then
			mobjGrid.ActionQuery = True
			mobjGrid.Columns("Sel").GridVisible = False
		Else
			.Columns("cbeCrthecni").EditRecord = True
		End If
		mobjGrid.Columns("tcnCrthecni").PopUpVisible = False
		.Height = 240
		.Width = 450
		.WidthDelete = 500
		
		'+ Parámetros para eliminación
		.sDelRecordParam = "nCrthecni='+ marrArray[lintIndex].cbeCrthecni + '"
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
End Sub

'% insPreMVI816: se realiza el manejo del Grid
'--------------------------------------------------------------------------------------------
Private Sub insPreMVI816()
	'--------------------------------------------------------------------------------------------
	
	Dim lstrInd As String
	Dim mclsCrit_sort As eBranches.Crit_sort
	mclsCrit_sort = New eBranches.Crit_sort
	mcolCrit_sorts = New eBranches.Crit_sorts
	
	If mcolCrit_sorts.Find(mobjValues.StringToType(vbNullString, eFunctions.Values.eTypeData.etdDouble)) Then
		For	Each mclsCrit_sort In mcolCrit_sorts
			With mobjGrid
				.Columns("tcnCrthecni").DefValue = CStr(mclsCrit_sort.nCrthecni)
				.Columns("cbeCrthecni").DefValue = CStr(mclsCrit_sort.nCrthecni)
				
				lstrInd = "0"
				'			    .Columns("Sel").OnClick = "InsChangeSel(this," & lstrInd & ");"
				.Columns("tcnRandom").DefValue = CStr(mclsCrit_sort.nRandom)
				.Columns("hddsSolic").DefValue = mclsCrit_sort.sSolic
				.Columns("hddnCount").DefValue = CStr(mclsCrit_sort.nCount)
				.Columns("cbeStatregt").DefValue = mclsCrit_sort.sStatregt
				Response.Write(.DoRow)
			End With
		Next mclsCrit_sort
	End If
	Response.Write(mobjGrid.closeTable())
	mcolCrit_sorts = Nothing
	mclsCrit_sort = Nothing
	
End Sub

'% insPreMVI816Upd: Se realiza el manejo de la ventana PopUp asociada al grid
'--------------------------------------------------------------------------------------------
Private Sub insPreMVI816Upd()
	'--------------------------------------------------------------------------------------------
	'- Objeto para procesar eliminacion de registro
	Dim lobjCrit_sort As eBranches.Crit_sort
	
	lobjCrit_sort = New eBranches.Crit_sort
	
	With Request
		If .QueryString.Item("Action") = "Del" Then
			Response.Write(mobjValues.ConfirmDelete())
			If lobjCrit_sort.insPostMVI816Upd(Request.QueryString.Item("Action"), mobjValues.StringToType(Request.QueryString.Item("nCrthecni"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("nRandom"), eFunctions.Values.eTypeData.etdDouble), Request.Form.Item("hddsSolic"), mobjValues.StringToType(Request.Form.Item("hddnCount"), eFunctions.Values.eTypeData.etdDouble), Request.Form.Item("cbeStatregt"), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble)) Then
			End If
		End If
		Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "valMantLife.aspx", "MVI816", .QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(.QueryString.Item("Index"))))
	End With
	lobjCrit_sort = Nothing
End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues

mobjValues.sCodisplPage = "MVI816"
%>
<HTML>
<HEAD>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>


<SCRIPT LANGUAGE=JavaScript>
//- Variable para el control de versiones
	document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 16:06 $|$$Author: Nvaplat61 $"

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
<%
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write("<SCRIPT>var nMainAction=0</SCRIPT>")
	Response.Write("<SCRIPT LANGUAGE=""JavaScript"" SRC=""/VTimeNet/Scripts/tmenu.js""></SCRIPT>" & vbCrLf)
End If
With Response
	.Write(mobjValues.StyleSheet() & vbCrLf)
	If Request.QueryString.Item("Type") <> "PopUp" Then
		mobjMenu = New eFunctions.Menues
		.Write(mobjMenu.MakeMenu(Request.QueryString.Item("sCodispl"), "MVI816_K.aspx", 1, ""))
		Response.Write("<BR></BR>")
		mobjMenu = Nothing
	End If
End With
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" NAME="MVI816" ACTION="valMantLife.aspx?sMode=2">
<%
Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl")))
Call insDefineHeader()
If Request.QueryString.Item("Type") = "PopUp" Then
	Call insPreMVI816Upd()
Else
	Call insPreMVI816()
End If
mobjValues = Nothing
mobjGrid = Nothing
%>
</FORM>
</BODY>
</HTML>





