<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eTarif" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo del grid de la página
Dim mobjGrid As eFunctions.Grid

'- Objeto para el manejo del menú
Dim mobjMenu As eFunctions.Menues

'- Objeto para el manejo particular de los datos de la página
Dim mcolTarif_tab_col As eTarif.Tarif_tab_cols


'% insDefineHeader: se definen las propiedades del grid
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	
	mobjGrid.sCodisplPage = "DP8001"
	
	'+ Se definen las columnas del grid  
	With mobjGrid.Columns
		Call .AddPossiblesColumn(0, GetLocalResourceObject("valid_ColumnColumnCaption"), "valid_Column", "TABTARIF_COLUMN", eFunctions.Values.eValuesType.clngWindowType, CStr(eRemoteDB.Constants.intNull),  ,  ,  ,  ,  , False,  , GetLocalResourceObject("valid_ColumnColumnToolTip"))
		Call .AddComboControl(0, "Operador", "cbeOperator", "=,<,>,<=,>=,IS NULL", "=", GetLocalResourceObject("0ToolTip"),  , "Operador de comparación")
		Call .AddPossiblesColumn(0, GetLocalResourceObject("cbetype_calcColumnCaption"), "cbetype_calc", "Table5801", eFunctions.Values.eValuesType.clngComboType, CStr(eRemoteDB.Constants.intNull),  ,  ,  ,  ,  , False,  , GetLocalResourceObject("cbetype_calcColumnToolTip"))
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnOrderColumnCaption"), "tcnOrder", 5, CStr(eRemoteDB.Constants.intNull),  , GetLocalResourceObject("tcnOrderColumnToolTip"))
	End With
	
	'+ Se definen las propiedades generales del grid
	With mobjGrid
		.Codispl = "DP8001"
		.ActionQuery = mobjValues.ActionQuery
		.Columns("valid_Column").EditRecord = True
		.Columns("valid_Column").Parameters.ReturnValue("sTable", True, "Tabla BD", True)
		.Columns("valid_Column").Parameters.ReturnValue("sColumn", True, "Columna BD", True)
		.Columns("valid_Column").Disabled = Request.QueryString.Item("Action") <> "Add"
		.DeleteButton = True
		.AddButton = True
		.Top = 200
		.Height = 250
		.Width = 400
		.WidthDelete = 400
		.nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
		.Columns("Sel").GridVisible = Not .ActionQuery
		.sDelRecordParam = "nid_column='+ marrArray[lintIndex].valid_Column + '"
		.Columns("Sel").OnClick = "InsCheckSel(" & Session("nId_Table") & ",this);"
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
End Sub

'% insPreDP8001: se realiza el manejo del grid
'--------------------------------------------------------------------------------------------
Private Sub insPreDP8001()
	'--------------------------------------------------------------------------------------------
	Dim lclsTarif_tab_col As Object
	mcolTarif_tab_col = New eTarif.Tarif_tab_cols
	
	If mcolTarif_tab_col.Find(Session("nId_Table")) Then
		For	Each lclsTarif_tab_col In mcolTarif_tab_col
			With mobjGrid
				.Columns("valid_Column").DefValue = lclsTarif_tab_col.nId_column
				.Columns("cbeOperator").DefValue = lclsTarif_tab_col.sOperator
				.Columns("cbetype_calc").DefValue = lclsTarif_tab_col.nType_calc
				.Columns("tcnorder").DefValue = lclsTarif_tab_col.nOrder
				
				Response.Write(.DoRow)
			End With
		Next lclsTarif_tab_col
	End If
	
	Response.Write(mobjGrid.closeTable())
End Sub

'% insPreDP8001Upd: Se realiza el manejo de la ventana PopUp asociada al grid
'-------------------------------------------------------------------------------------------- 
Private Sub insPreDP8001Upd()
	'--------------------------------------------------------------------------------------------
	Dim lclsTarif_tab_col As eTarif.Tarif_tab_col
	lclsTarif_tab_col = New eTarif.Tarif_tab_col
	
	With Request
		If .QueryString.Item("Action") = "Del" Then
			Response.Write(mobjValues.ConfirmDelete())
			If lclsTarif_tab_col.InsPostDP8001(.QueryString.Item("Action"), mobjValues.StringToType(Session("nId_Table"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nId_column"), eFunctions.Values.eTypeData.etdDouble), "", eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull) Then
			End If
			Response.Write("<SCRIPT>top.opener.top.frames['fraSequence'].document.location='/VTimeNet/product/producttarseq/Sequence.aspx?nAction=" & Request.QueryString.Item("nMainAction") & "&nOpener=DP8001" & "&nMainAction=" & Request.QueryString.Item("nMainAction") & "&sGoToNext=No" & "';</" & "Script>")
		End If
		Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "valProducttarseq.aspx", "DP8001", .QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(.QueryString.Item("Index"))))
	End With
End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues
mobjValues.ActionQuery = CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401

mobjValues.sCodisplPage = "DP8001"
%>
<SCRIPT>
//% InsCheckSel: Valida la eliminación de un registro
//-------------------------------------------------------------------------------------------
function InsCheckSel(Id_table,Field){
//-------------------------------------------------------------------------------------------
	var strParams;
	strParams = "nId_table=" + Id_table + "&nIndex=" + Field.value;
	insDefValues("InsValDelete",strParams,'/VTimeNet/Product/producttarseq');
}
</SCRIPT>
<HTML>
<HEAD>
	<SCRIPT>
	//+ Variable para el control de versiones
	        document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 18.00 $"
    </SCRIPT>	
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>


	<%
Response.Write(mobjValues.StyleSheet())
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write(mobjMenu.setZone(2, "DP8001", "DP8001.aspx"))
	mobjMenu = Nothing
	Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
End If
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" NAME="DP8001" ACTION="valProducttarseq.aspx?nMainAction=<%=Request.QueryString.Item("nMainAction")%>">
	<%Response.Write(mobjValues.ShowWindowsName("DP8001"))
Call insDefineHeader()
If Request.QueryString.Item("Type") = "PopUp" Then
	Call insPreDP8001Upd()
Else
	If Request.QueryString.Item("Action") = "Del" Then
		Call insPreDP8001Upd()
	Else
		Call insPreDP8001()
	End If
End If
%>
</FORM> 
</BODY>
</HTML>





