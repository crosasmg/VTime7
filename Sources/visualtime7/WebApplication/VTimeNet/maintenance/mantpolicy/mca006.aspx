<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eBatch" %>
<%@ Import namespace="ePolicy" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores	
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo del grid
Dim mobjGrid As eFunctions.Grid

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjMenues As eFunctions.Menues

'- Objeto que hace referencia a la colección.
Dim mcolGroup_columnss As eBatch.Group_columnss

'- Objeto que hace referencia a la clase.
Dim mclsGroup_columns As eBatch.Group_columns


'% insDefineHeader:Este procedimiento se encarga de definir las columnas del grid y de habilitar
'% o inhabilitar los botones de añadir y eliminar.
'-------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'-------------------------------------------------------------------------------------------
	Dim lstrFile As String
	Dim lclsPolicy As ePolicy.Policy
	mobjGrid = New eFunctions.Grid
	
	mobjGrid.sCodisplPage = "mca006"
	
	mobjValues.ActionQuery = Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngactionquery)
	
	Response.Write(mobjValues.ShowWindowsName("MCA006") & "<BR>")
	
	With mobjGrid
		With .Columns
			If Request.QueryString.Item("Type") <> "PopUp" Then
				.AddTextColumn(0, GetLocalResourceObject("cbeTableColumnCaption"), "cbeTable", 30, vbNullString,  , GetLocalResourceObject("cbeTableColumnToolTip"))
				.AddTextColumn(0, GetLocalResourceObject("ValFieldColumnCaption"), "ValField", 15, vbNullString,  , GetLocalResourceObject("ValFieldColumnToolTip"))
			Else
				If mobjValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble) <= 0 Then
					If Request.QueryString.Item("Action") = "Update" Then
						.AddPossiblesColumn(0, GetLocalResourceObject("cbeTableColumnCaption"), "cbeTable", "TabSysTables", eFunctions.Values.eValuesType.clngWindowType, vbNullString, False,  ,  ,  ,  , True, 30, GetLocalResourceObject("cbeTableColumnToolTip"), eFunctions.Values.eTypeCode.eString)
					Else
						.AddPossiblesColumn(0, GetLocalResourceObject("cbeTableColumnCaption"), "cbeTable", "TabSysTables", eFunctions.Values.eValuesType.clngWindowType, vbNullString, False,  ,  ,  , "ChangeValues(""File"", this);", False, 30, GetLocalResourceObject("cbeTableColumnToolTip"), eFunctions.Values.eTypeCode.eString)
					End If
					lstrFile = vbNullString
				Else
					lclsPolicy = New ePolicy.Policy
					Call lclsPolicy.Find_TabNameB(CInt(Request.QueryString.Item("nBranch")))
					lstrFile = RTrim(UCase(CStr(lclsPolicy.sTabname)))
					.AddPossiblesColumn(0, GetLocalResourceObject("cbeTableColumnCaption"), "cbeTable", "TabSysTables", eFunctions.Values.eValuesType.clngWindowType, lstrFile, False,  ,  ,  ,  , True, 30, GetLocalResourceObject("cbeTableColumnToolTip"), eFunctions.Values.eTypeCode.eString)
				End If
				
				.AddPossiblesColumn(101919, GetLocalResourceObject("ValFieldColumnCaption"), "ValField", "TabSysColumns", eFunctions.Values.eValuesType.clngWindowType, vbNullString, True,  ,  ,  , "ChangeValues(""Column"", this, ""False"");", Request.QueryString.Item("Action") = "Update", 15, GetLocalResourceObject("ValFieldColumnToolTip"), eFunctions.Values.eTypeCode.eString)
				mobjGrid.Columns("ValField").Parameters.Add("sFile", lstrFile, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 15, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			End If
			
			.AddTextColumn(101922, GetLocalResourceObject("sColumnNameColumnCaption"), "sColumnName", 30, vbNullString,  , GetLocalResourceObject("sColumnNameColumnCaption"))
			.AddTextColumn(101923, GetLocalResourceObject("sCommentColumnCaption"), "sComment", 30, vbNullString,  , GetLocalResourceObject("sCommentColumnToolTip"))
			.AddNumericColumn(101921, GetLocalResourceObject("nOrderColumnCaption"), "nOrder", 5, CStr(0),  , GetLocalResourceObject("nOrderColumnToolTip"))
			.AddCheckColumn(101924, GetLocalResourceObject("sRequireColumnCaption"), "sRequire", "",  ,  ,  , Not Request.QueryString.Item("Type") = "PopUp", GetLocalResourceObject("sRequireColumnToolTip"))
			If Request.QueryString.Item("Type") <> "PopUp" Then
				.AddTextColumn(0, GetLocalResourceObject("ValListColumnCaption"), "ValList", 30, vbNullString,  , GetLocalResourceObject("ValListColumnToolTip"))
			Else
				.AddPossiblesColumn(0, GetLocalResourceObject("ValListColumnCaption"), "ValList", "TabSysTables_1", eFunctions.Values.eValuesType.clngWindowType, Request.QueryString.Item("sValList"), True,  ,  ,  ,  , True, 30, GetLocalResourceObject("ValListColumnToolTip"), eFunctions.Values.eTypeCode.eString)
				mobjGrid.Columns("ValList").Parameters.Add("sTable", "", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				mobjGrid.Columns("ValList").Parameters.Add("sColumn", "", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			End If
			.AddHiddenColumn("nIdRec", "0")
		End With
		
		.Height = 350
		.Width = 650
		.WidthDelete = 600
		.Codispl = "MCA006"
		.ActionQuery = mobjValues.ActionQuery
		.Columns("Sel").GridVisible = Not .ActionQuery
		.Columns("cbeTable").EditRecord = Not .ActionQuery
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
		.sEditRecordParam = "nSheet=" & Request.QueryString.Item("nSheet")
		
		.sDelRecordParam = "nBranch=" & Request.QueryString.Item("nBranch") & "&nSheet=" & Request.QueryString.Item("nSheet") & "&sField=' + marrArray[lintIndex].ValField + '" & "&nIdRec=' + marrArray[lintIndex].nIdRec + '"
	End With
End Sub

'%inspreMCA006upd: Esta función permite Actualizar los registros de la tabla Group_column
'-------------------------------------------------------------------------------------------
Private Sub inspreMCA006upd()
	'-------------------------------------------------------------------------------------------
	Dim lobjError As Object
	
	With Request
		If .QueryString.Item("Action") = "Del" Then
			mclsGroup_columns.nSheet = mobjValues.StringToType(Request.QueryString.Item("nSheet"), eFunctions.Values.eTypeData.etdDouble, True)
			mclsGroup_columns.sField = .QueryString.Item("sField")
			mclsGroup_columns.nIdrec = mobjValues.StringToType(.QueryString.Item("nIdRec"), eFunctions.Values.eTypeData.etdDouble, True)
			If mclsGroup_columns.Delete Then
				Response.Write(mobjValues.ConfirmDelete())
			End If
			lobjError = Nothing
		End If
	End With
	
	Response.Write(mobjGrid.DoFormUpd(Request.QueryString.Item("Action"), "valMantPolicy.aspx", "MCA006", Request.QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(Request.QueryString.Item("Index"))))
	
	'+ Según la acción se actualizan los valores de la página luego de diseñada.
	If LCase(Request.QueryString.Item("Action")) = "add" Then
		Response.Write("<SCRIPT>insDefAdd();</" & "Script>")
	End If
	
End Sub

'%inspreMCA006: Esta función permite realizar la lectura de la tabla Group_column de la transacción.
'-------------------------------------------------------------------------------------------
Private Sub inspreMCA006()
	'-------------------------------------------------------------------------------------------
	Dim lintIndex As Short
	lintIndex = 0
	
	If mcolGroup_columnss.FindMCA006(mobjValues.StringToType(Request.QueryString.Item("nSheet"), eFunctions.Values.eTypeData.etdDouble, True)) Then
		With mobjGrid
			For	Each mclsGroup_columns In mcolGroup_columnss
				
				.Columns("cbeTable").DefValue = mclsGroup_columns.sTable
				.Columns("ValField").Parameters.Add("sFile", CStr(mclsGroup_columns.sTable), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 15, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Columns("ValField").DefValue = mclsGroup_columns.sField
				
				.Columns("sColumnName").DefValue = mclsGroup_columns.sColumnName
				
				.Columns("sComment").DefValue = mclsGroup_columns.sComment
				.Columns("nOrder").DefValue = CStr(mclsGroup_columns.nOrder)
				.Columns("sRequire").Checked = CShort(mclsGroup_columns.sRequire)
				
				.Columns("ValList").Parameters.Add("sTable", mclsGroup_columns.sTable, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 15, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Columns("ValList").Parameters.Add("sColumn", mclsGroup_columns.sField, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 15, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Columns("ValList").DefValue = mclsGroup_columns.sValuesList
				
				.Columns("nIdRec").DefValue = CStr(mclsGroup_columns.nIdrec)
				.sEditRecordParam = "nBranch=" & Request.QueryString.Item("nBranch") & "&nSheet=" & Request.QueryString.Item("nSheet") & "&sField=' + marrArray[" & CStr(lintIndex) & "].ValField + '" & "&sTable=' + marrArray[" & CStr(lintIndex) & "].cbeTable + '" & "&sValList=' + marrArray[" & CStr(lintIndex) & "].ValList + '"
				Response.Write(.DoRow)
				lintIndex = lintIndex + 1
			Next mclsGroup_columns
		End With
	End If
	Response.Write(mobjGrid.CloseTable)
	Response.Write(mobjValues.BeginPageButton)
End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjMenues = New eFunctions.Menues

mobjValues.sCodisplPage = "mca006"
%>
<HTML>
<HEAD>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></SCRIPT>


			
<SCRIPT LANGUAGE="JavaScript">
	var nMainAction = 304;
	
//- Variable para el control de versiones
	document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 16:15 $|$$Author: Nvaplat61 $"

//% ChangeValues: Refresca los Valores de los parametros de los valores Posibles
//--------------------------------------------------------------------------------------------
function ChangeValues(Option, Field){
//-------------------------------------------------------------------------------------------*/
	switch(Option){
		case "File":
			with(self.document.forms[0]){
				ValField.Parameters.Param1.sValue=Field.value;
				ValField.value="";
				UpdateDiv("ValFieldDesc","");
				ValList.Parameters.Param1.sValue=Field.value;
			}
			break;
			
		case "Column":	
			with(self.document.forms[0]){
				ValList.Parameters.Param1.sValue=cbeTable.value;
				ValList.Parameters.Param2.sValue=ValField.value;
				if (Field.value != ''){
					ValList.disabled = false;
					btnValList.disabled = false;
				 }
				else {
					ValList.disabled = true;
					btnValList.disabled = true;
				}
			}
			break;

	}
}

//% insDefAdd: Establece el valor automatico del Orden
//--------------------------------------------------------------------------------------------
function insDefAdd(){
//--------------------------------------------------------------------------------------------
//- Se define la variable para almacenar el Orden más alto existente en el grid
    var llngMax = 0
    
//+ Se genera el número consecutivo del orden (el Nº de Orden más alto +1)   
	for(var llngIndex = 0;llngIndex<top.opener.marrArray.length;llngIndex++)
	    if(eval(top.opener.marrArray[llngIndex].nOrder)>llngMax)
	        llngMax = top.opener.marrArray[llngIndex].nOrder

//+ Se asignan los valores a los campos de la página	
	self.document.forms[0].nOrder.value = ++llngMax;
}
</SCRIPT>
	<%
Response.Write(mobjValues.StyleSheet())
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write(mobjMenues.setZone(2, "MCA006", "MCA006.aspx"))
End If
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="MCA006" ACTION="valMantPolicy.aspx?Time=1">
	<%
mcolGroup_columnss = New eBatch.Group_columnss
mclsGroup_columns = New eBatch.Group_columns

Call insDefineHeader()
If Request.QueryString.Item("Type") <> "PopUp" Then
	inspreMCA006()
Else
	inspreMCA006upd()
End If

mobjGrid = Nothing
mcolGroup_columnss = Nothing
mclsGroup_columns = Nothing
%>
</FORM>
</BODY>
</HTML>




