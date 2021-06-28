<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eTarif" %>
<script language="VB" runat="Server">

'----------------------------------------------------------------------------------------
'- Ventana Masiva.  Causas del estado pendiente de la poliza/certificado 
'----------------------------------------------------------------------------------------

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo del grid de la página
Dim mobjGrid As eFunctions.Grid

'- Objeto para el manejo del menú
Dim mobjMenu As eFunctions.Menues

Dim mcoltarif_columns As eTarif.tarif_columns


'% insDefineHeader: se definen las propiedades del grid
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	
	mobjGrid.sCodisplPage = "MDP8003_k"
	
	'+ Se definen las columnas del grid 
	With mobjGrid.Columns
		
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnId_columnColumnCaption"), "tcnId_column", 5, vbNullString,  , GetLocalResourceObject("tcnId_columnColumnToolTip"),  ,  ,  ,  ,  , True)
		If Request.QueryString.Item("Type") <> "PopUp" Then
			Call .AddTextColumn(0, GetLocalResourceObject("tctTableColumnCaption"), "tctTable", 30, vbNullString,  , GetLocalResourceObject("tctTableColumnToolTip"))
			Call .AddTextColumn(0, GetLocalResourceObject("tctColumnColumnCaption"), "tctColumn", 30, vbNullString,  , GetLocalResourceObject("tctColumnColumnToolTip"))
		Else
			If Request.QueryString.Item("Action") = "Update" Then
				.AddPossiblesColumn(0, GetLocalResourceObject("tctTableColumnCaption"), "tctTable", "TabSysTables_2", eFunctions.Values.eValuesType.clngWindowType, vbNullString, False,  ,  ,  ,  , True, 30, GetLocalResourceObject("tctTableColumnToolTip"), eFunctions.Values.eTypeCode.eString)
			Else
				.AddPossiblesColumn(0, GetLocalResourceObject("tctTableColumnCaption"), "tctTable", "TabSysTables_2", eFunctions.Values.eValuesType.clngWindowType, vbNullString, False,  ,  ,  , "ChangeValues(""File"", this);", False, 30, GetLocalResourceObject("tctTableColumnToolTip"), eFunctions.Values.eTypeCode.eString)
			End If
			Call .AddPossiblesColumn(0, GetLocalResourceObject("tctColumnColumnCaption"), "tctColumn", "TabSysColumns", eFunctions.Values.eValuesType.clngWindowType, vbNullString, True,  ,  ,  , "ChangeValues(""Column"", this,""" & Request.QueryString.Item("Action") & """);", Request.QueryString.Item("Action") = "Update", 15, GetLocalResourceObject("tctColumnColumnToolTip"), eFunctions.Values.eTypeCode.eString)
			mobjGrid.Columns("tctColumn").Parameters.Add("sFile", "", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 15, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		End If
		Call .AddTextColumn(0, GetLocalResourceObject("tctName_colColumnCaption"), "tctName_col", 30, vbNullString,  , GetLocalResourceObject("tctName_colColumnToolTip"))
		Call .AddTextColumn(0, GetLocalResourceObject("tctdata_typeColumnCaption"), "tctdata_type", 30, vbNullString,  , GetLocalResourceObject("tctdata_typeColumnToolTip"),  ,  ,  , True)
		Call .AddTextColumn(0, GetLocalResourceObject("tctsizeColumnCaption"), "tctsize", 30, vbNullString,  , GetLocalResourceObject("tctsizeColumnToolTip"),  ,  ,  , True)
		Call .AddTextColumn(0, GetLocalResourceObject("tctdecimalColumnCaption"), "tctdecimal", 30, vbNullString,  , GetLocalResourceObject("tctdecimalColumnToolTip"),  ,  ,  , True)
		Call .AddTextColumn(0, GetLocalResourceObject("tctTablefkColumnCaption"), "tctTablefk", 30, vbNullString,  , GetLocalResourceObject("tctTablefkColumnToolTip"),  ,  ,  , False)
		Call .AddHiddenColumn("hdddata_type", "")
	End With
	
	'+ Se definen las propiedades generales del grid 
	With mobjGrid
		.Codispl = "MDP8003"
		.Codisp = "MDP8003"
		If Request.QueryString.Item("nMainAction") = "401" Or Request.QueryString.Item("nMainAction") = vbNullString Then
			mobjGrid.ActionQuery = True
			mobjGrid.Columns("Sel").GridVisible = False
		Else
			.Columns("tctTable").EditRecord = True
		End If
		
		.Height = 350
		.Width = 500
		.Top = 100
		.WidthDelete = 500
		
		'+ parámetros para eliminación
		.sDelRecordParam = "nId_column='+ marrArray[lintIndex].tcnId_column + '"
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
End Sub

'% insPreMDP8003: se realiza el manejo del grid
'--------------------------------------------------------------------------------------------
Private Sub insPreMDP8003()
	'--------------------------------------------------------------------------------------------
	
	Dim lstrInd As Object
	Dim mclstarif_column As eTarif.tarif_column
	mclstarif_column = New eTarif.tarif_column
	mcoltarif_columns = New eTarif.tarif_columns
	
	If mcoltarif_columns.Find() Then
		For	Each mclstarif_column In mcoltarif_columns
			With mobjGrid
				.Columns("tcnId_column").DefValue = CStr(mclstarif_column.nId_column)
				.Columns("tctTable").DefValue = mclstarif_column.sTable
				.Columns("tctColumn").Parameters.Add("sFile", CStr(mclstarif_column.sTable), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 15, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Columns("tctColumn").DefValue = mclstarif_column.sColumn
				.Columns("tctName_col").DefValue = mclstarif_column.sName_col
				.Columns("tctdata_type").DefValue = mclstarif_column.sData_type
				.Columns("tctsize").DefValue = CStr(mclstarif_column.nSize)
				.Columns("tctdecimal").DefValue = mobjValues.TypeToString(mclstarif_column.nDecimal, eFunctions.Values.eTypeData.etdLong)
				.Columns("hdddata_type").DefValue = CStr(mclstarif_column.nData_type)
				.Columns("tctTablefk").DefValue = mclstarif_column.sTablefk
				Response.Write(.DoRow)
			End With
		Next mclstarif_column
	End If
	Response.Write(mobjGrid.closeTable())
	mclstarif_column = Nothing
	mcoltarif_columns = Nothing
End Sub

'% insPreMDP8003Upd: Se realiza el manejo de la ventana PopUp asociada al grid
'--------------------------------------------------------------------------------------------
Private Sub insPreMDP8003Upd()
	'--------------------------------------------------------------------------------------------
	'- Objeto para procesar eliminacion de registro
	Dim lobjtarif_column As eTarif.tarif_column
	
	lobjtarif_column = New eTarif.tarif_column
	
	With Request
		If .QueryString.Item("Action") = "Del" Then
			Response.Write(mobjValues.ConfirmDelete())
			If lobjtarif_column.insPostMDP8003(Request.QueryString.Item("Action"), mobjValues.StringToType(Request.QueryString.Item("nId_column"), eFunctions.Values.eTypeData.etdDouble)) Then
			End If
		End If
		Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "valmantproduct.aspx", "MDP8003", .QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(.QueryString.Item("Index"))))
	End With
End Sub

</script>
<%Response.Expires = -1
mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues

'mobjValues.sCodisplPage = "mdp8003_k"			
%>
<HTML>
<HEAD>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>




<SCRIPT LANGUAGE=JavaScript>

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
//% ChangeValues: Refresca los Valores de los parametros de los valores Posibles
//--------------------------------------------------------------------------------------------
function ChangeValues(Option, Field, Action){
//-------------------------------------------------------------------------------------------*/
	
	var strParams; 
	switch(Option){
		case "File":
			with(self.document.forms[0]){
				tctColumn.Parameters.Param1.sValue=Field.value;
				tctColumn.value="";
				UpdateDiv("tctColumnDesc","");
			}
			break;
			
		case "Column":
		    {
    		strParams = "sField=" + "getData" + "&tctTable=" +  self.document.forms[0].tctTable.value + "&tctColumn=" +  Field.value + "&Action=" + Action
			insDefValues('ShowDataMDP8003',strParams,'/VTimeNet/maintenance/mantproduct'); 
			}
			break;
	}
}

</SCRIPT>
<%
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write("<SCRIPT>var nMainAction = top.frames['fraSequence'].plngMainAction</SCRIPT>")
	Response.Write("<SCRIPT LANGUAGE=""JavaScript"" SRC=""/VTimeNet/Scripts/tmenu.js""></SCRIPT>" & vbCrLf)
End If
With Response
	.Write(mobjValues.StyleSheet() & vbCrLf)
	If Request.QueryString.Item("Type") <> "PopUp" Then
		.Write(mobjMenu.MakeMenu("MDP8003", "MDP8003_k.aspx", 1, ""))
		mobjMenu = Nothing
	End If
End With
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" NAME="MDP8003" ACTION="valmantproduct.aspx?sMode=2">
<%

Call insDefineHeader()
If Request.QueryString.Item("Type") = "PopUp" Then
	Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl")))
	Call insPreMDP8003Upd()
Else
	Response.Write("<BR></BR>")
	Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl")))
	Call insPreMDP8003()
End If
mobjValues = Nothing
mobjGrid = Nothing
%>
</FORM>
</BODY>
</HTML>





