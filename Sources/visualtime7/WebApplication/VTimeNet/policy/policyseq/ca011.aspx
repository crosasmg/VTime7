<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="ePolicy" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo del grid de la página
Dim mobjGrid As eFunctions.Grid

'- Objeto para el manejo del menú
Dim mobjMenu As eFunctions.Menues

'- Objeto para manejar la clase de Groups
Dim lclsGroups As ePolicy.Groups

'- Objeto para el manejo particular de los datos de la página
Dim mcolClass As Object


'% insDefineHeader: se definen las propiedades del grid
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	Dim mobjPolicy As ePolicy.Policy
	mobjGrid = New eFunctions.Grid
	mobjPolicy = New ePolicy.Policy
	
	lclsGroups = New ePolicy.Groups
	'+ Se definen las columnas del grid    
	With mobjGrid.Columns
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnGroupColumnCaption"), "tcnGroup", 5, vbNullString,  , GetLocalResourceObject("tcnGroupColumnToolTip"))
		Call .AddTextColumn(0, GetLocalResourceObject("tctDescriptColumnCaption"), "tctDescript", 30, vbNullString,  , GetLocalResourceObject("tctDescriptColumnToolTip"))
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnParticipColumnCaption"), "tcnParticip", 5, vbNullString,  , GetLocalResourceObject("tcnParticipColumnToolTip"),  , 2)
		Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeGroupStatColumnCaption"), "cbeGroupStat", "Table26", eFunctions.Values.eValuesType.clngComboType, vbNullString,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeGroupStatColumnToolTip"), eFunctions.Values.eTypeCode.eString)
	End With
	'+ Se definen las propiedades generales del grid
	With mobjGrid
		
		If mobjPolicy.Find(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy")) Then
			If mobjPolicy.sTyp_module <> "3" And
			   mobjPolicy.sTyp_Discxp <> "3" And 
			   mobjPolicy.sTyp_Clause <> "3" Then
				.AddButton = False
			End If
		End If
		.Codispl = "CA011"
		.ActionQuery = mobjValues.ActionQuery
		.Columns("tcnGroup").EditRecord = True
		.Height = 250
		.Width = 350
		.nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
		.Columns("cbeGroupStat").TypeList = 2
		.Columns("cbeGroupStat").List = "2"
		.Columns("Sel").GridVisible = Not .ActionQuery
		.Columns("Sel").OnClick = "valGroup(this);"
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
		.sDelRecordParam = "nGroup='+ marrArray[lintIndex].tcnGroup + '" & "&sDescript='+ marrArray[lintIndex].tctDescript + '" & "&nParticip='+ marrArray[lintIndex].tcnParticip + '" & "&sGroupStat='+ marrArray[lintIndex].cbeGroupStat + '"
	End With
End Sub

'% insPreCa011: se realiza el manejo del grid
'--------------------------------------------------------------------------------------------
Private Sub insPreCa011()
	'--------------------------------------------------------------------------------------------
	'*++ Modificar nombre del objeto. Modificar "Class" por el nombre de la clase con la cual se trabaja
	
	Dim mcolGroupss As ePolicy.Groupss
	
	mcolGroupss = New ePolicy.Groupss
	
	If mcolGroupss.Find(Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("npolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate)) Then
		For	Each lclsGroups In mcolGroupss
			With mobjGrid
				.Columns("tcnGroup").DefValue = CStr(lclsGroups.nGroup)
				.Columns("tctDescript").DefValue = lclsGroups.sDescript
				.Columns("tcnParticip").DefValue = CStr(lclsGroups.nParticip)
				.Columns("cbeGroupStat").DefValue = lclsGroups.sStatregt
				Response.Write(.DoRow)
			End With
		Next lclsGroups
	End If
	
	Response.Write(mobjGrid.closeTable())
End Sub

'% insPreCa011Upd: Se realiza el manejo de la ventana PopUp asociada al grid
'--------------------------------------------------------------------------------------------
Private Sub insPreCa011Upd()
	'--------------------------------------------------------------------------------------------
	Dim lobjVal As ePolicy.ValPolicySeq
	
	lobjVal = New ePolicy.ValPolicySeq
	
	With Request
		If .QueryString.Item("Action") = "Del" Then
			Response.Write(mobjValues.ConfirmDelete())
			If lobjVal.insPostCA011("Delete", Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("npolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.QueryString.Item("nGroup"), eFunctions.Values.eTypeData.etdDouble), .QueryString.Item("sDescript"), mobjValues.StringToType(.QueryString.Item("nParticip"), eFunctions.Values.eTypeData.etdDouble), .QueryString.Item("sGroupStat"), mobjValues.StringToType(Session("nUserCode"), eFunctions.Values.eTypeData.etdDouble), 0) Then
				Response.Write("<SCRIPT>top.opener.top.frames['fraSequence'].document.location='/VTimeNet/Policy/PolicySeq/Sequence.aspx?nAction=" & Request.QueryString.Item("nMainAction") & "&nOpener=" & Request.QueryString.Item("sCodispl") & "&sGoToNext=NO" & "';</" & "Script>")
			End If
		End If
		
		Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "ValPolicySeq.aspx", "CA011", .QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(.QueryString.Item("Index"))))
	End With
End Sub

</script>
<%Response.Expires = -1
Response.CacheControl = "private"

mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues

mobjValues.ActionQuery = CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401
%>
<HTML>
<HEAD>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE=JavaScript>

//- Variable para el control de versiones
    document.VssVersion="$$Revision: 3 $|$$Date: 13/06/06 17:41 $|$$Author: Pmanzur $"

//%valClause: Se verifica si se puede borrar o no la cláusula
//--------------------------------------------------------------------------------------------------
function valGroup(Field){
//--------------------------------------------------------------------------------------------------
	if(Field.checked){
		self.document.cmdDelete.disabled = true;
		insDefValues('DeleteCA011', 'nGroup=' + marrArray[Field.value].tcnGroup + '&nIndex=' + Field.value)
	}
}

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

//% insFinish: se controla la acción Cancelar de la página
//--------------------------------------------------------------------------------------------
function insFinish(){
//--------------------------------------------------------------------------------------------
    return true;
}
</SCRIPT>
	<%
Response.Write(mobjValues.StyleSheet())
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write(mobjMenu.setZone(2, "CA011", "CA011.aspx"))
	mobjMenu = Nothing
	Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
End If
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" NAME="Nombre_de_la_página" ACTION="ValpolicySeq.aspx?sMode=2">
<%Response.Write(mobjValues.ShowWindowsName("CA011"))

Call insDefineHeader()

If Request.QueryString.Item("Type") = "PopUp" Then
	Call insPreCa011Upd()
Else
	Call insPreCa011()
End If
%>
</FORM> 
</BODY>
</HTML>





