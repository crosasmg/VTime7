<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eMargin" %>
<script language="VB" runat="Server">
'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
'- Objeto para el manejo del grid de la página
Dim mobjGrid As eFunctions.Grid
'- Objeto para el manejo del menú
Dim mobjMenu As eFunctions.Menues


'% insDefineHeader: se definen las propiedades del grid
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	'+ Se definen las columnas del grid    
	With mobjGrid.Columns
		Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeSVSClassColumnCaption"), "cbeSVSClass", "Table71", eFunctions.Values.eValuesType.clngComboType, vbNullString,  ,  ,  ,  ,  , Request.QueryString.Item("Action") = "Update",  , GetLocalResourceObject("cbeSVSClassColumnToolTip"))
		Call .AddDateColumn(0, GetLocalResourceObject("tcdInitDateColumnCaption"), "tcdInitDate", vbNullString,  , GetLocalResourceObject("tcdInitDateColumnToolTip"),  ,  ,  , Request.QueryString.Item("Action") = "Update")
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnValueColumnCaption"), "tcnValue", 8, vbNullString,  , GetLocalResourceObject("tcnValueColumnToolTip"),  , 5)
		Call .AddHiddenColumn("hddDelete", vbNullString)
	End With
	'+ Se definen las propiedades generales del grid
	With mobjGrid
		.Codispl = "MMGS001"
		.ActionQuery = mobjValues.ActionQuery
		.Height = 210
		.Width = 420
		.WidthDelete = 450
		.nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
		.Columns("Sel").GridVisible = Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionUpdate)
		.sDelRecordParam = "nInsur_area=" & Request.QueryString.Item("nInsur_area") & "&nFactor=" & Request.QueryString.Item("nFactor") & "&nSVSClass=' + marrArray[lintIndex].cbeSVSClass + '" & "&dInitDate=' + marrArray[lintIndex].tcdInitDate + '"
		.sEditRecordParam = "nInsur_area=" & Request.QueryString.Item("nInsur_area") & "&nFactor=" & Request.QueryString.Item("nFactor")
		.Columns("cbeSVSClass").EditRecord = True
		If Session("nInsur_area") = 1 Then
			.Columns("cbeSVSClass").List = "1,2,3,4,5"
			.Columns("cbeSVSClass").TypeList = 1 'Incluir 
		Else
			.Columns("cbeSVSClass").List = "1,2,3,4,5"
			.Columns("cbeSVSClass").TypeList = 2 'Excluir
		End If
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
	Response.Write(mobjValues.HiddenControl("hddInsur_area", Request.QueryString.Item("nInsur_area")))
	Response.Write(mobjValues.HiddenControl("hddFactor", Request.QueryString.Item("nFactor")))
End Sub

'% insPreMMGS001: se realiza el manejo del grid
'--------------------------------------------------------------------------------------------
Private Sub insPreMMGS001()
	'--------------------------------------------------------------------------------------------
	Dim lintIndex As Short
	Dim lcolTab_svs As eMargin.Tab_svss
	Dim lclsTab_svs As Object
	lcolTab_svs = New eMargin.Tab_svss
	
	If lcolTab_svs.Find(mobjValues.StringToType(Request.QueryString.Item("nInsur_area"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nFactor"), eFunctions.Values.eTypeData.etdDouble)) Then
		lintIndex = 0
		For	Each lclsTab_svs In lcolTab_svs
			With mobjGrid
				.Columns("cbeSVSClass").DefValue = lclsTab_svs.nSVSClass
				.Columns("tcdInitDate").DefValue = lclsTab_svs.dEffecdate
				.Columns("tcnValue").DefValue = lclsTab_svs.nValue
				.Columns("hddDelete").DefValue = lclsTab_svs.sDelete
				.Columns("Sel").OnClick = "insSel(" & lintIndex & ")"
				Response.Write(.DoRow)
				If Session("nInsur_area") = 1 Then
					.Columns("cbeSVSClass").List = "1,2,3,4,5"
					.Columns("cbeSVSClass").TypeList = 1 'Incluir 
				Else
					.Columns("cbeSVSClass").List = "1,2,3,4,5"
					.Columns("cbeSVSClass").TypeList = 2 'Excluir
				End If
			End With
			lintIndex = lintIndex + 1
		Next lclsTab_svs
	End If
	Response.Write(mobjGrid.closeTable())
	Response.Write(mobjValues.BeginPageButton)
	lcolTab_svs = Nothing
End Sub

'% insPreMMGS001Upd: Se realiza el manejo de la ventana PopUp asociada al grid
'--------------------------------------------------------------------------------------------
Private Sub insPreMMGS001Upd()
	'--------------------------------------------------------------------------------------------
	Dim lclsTab_svs As eMargin.Tab_svs
	With Request
		If Request.QueryString.Item("Action") = "Del" Then
			lclsTab_svs = New eMargin.Tab_svs
			If lclsTab_svs.inspostMMGS001(.QueryString.Item("Action"), mobjValues.StringToType(.QueryString.Item("nInsur_area"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nFactor"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nSVSClass"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.QueryString.Item("dInitDate"), eFunctions.Values.eTypeData.etdDate), Session("nUsercode"), eRemoteDB.Constants.intNull) Then
				Response.Write(mobjValues.ConfirmDelete())
			End If
		End If
		Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "valMantMargin.aspx", "MMGS001", .QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(.QueryString.Item("Index"))))
	End With
End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues
mobjValues.ActionQuery = CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401
mobjValues.sCodisplPage = "MMGS001"
%>
<HTML>
<HEAD>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT>
//- Variable para el control de versiones
	document.VssVersion="$$Revision: 4 $|$$Date: 27/11/03 17:35 $|$$Author: Nvaplat15 $"

//% insSel: se valida si se puede eliminar o no el registro
//--------------------------------------------------------------------------------------------
function insSel(nIndex){
//--------------------------------------------------------------------------------------------
	var lblnError = false;
	if(marrArray[nIndex].hddDelete=='1'){
		alert('Err. 55909: <%=eFunctions.Values.GetMessage(55909)%>');
		lblnError = true;
	}
	else
		if(marrArray[nIndex].hddDelete=='2'){
			lblnError = true;
			alert('Err. 55908: <%=eFunctions.Values.GetMessage(55908)%>');
		}
	
	if(lblnError){
		marrArray[nIndex].Sel=false;
		if(marrArray.length>1)
			self.document.forms[0].Sel[nIndex].checked=false;
		else
			self.document.forms[0].Sel.checked=false;
	}
}
</SCRIPT>
<%
Response.Write(mobjValues.StyleSheet())
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write(mobjMenu.setZone(2, "MMGS001", "MMGS001.aspx"))
	Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
End If
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" NAME="MMGS001" ACTION="valMantMargin.aspx?sMode=2">
<%Response.Write(mobjValues.ShowWindowsName("MMGS001"))
Call insDefineHeader()
If Request.QueryString.Item("Type") = "PopUp" Then
	Call insPreMMGS001Upd()
Else
	Call insPreMMGS001()
End If
mobjMenu = Nothing
mobjValues = Nothing
%>
</FORM> 
</BODY>
</HTML>




