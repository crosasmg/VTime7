<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eCollection" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 10.44.07
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

'- Objeto para el manejo de las funciones generales de carga de valores

Dim mobjValues As eFunctions.Values
Dim mobjGrid As eFunctions.Grid
Dim mobjMenu As eFunctions.Menues


'% insDefineHeader: Se definen los campos del grid
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.44.07
	mobjGrid.sSessionID = Session.SessionID
	mobjGrid.nUsercode = Session("nUsercode")
	'~End Body Block VisualTimer Utility
	
	mobjGrid.sCodisplPage = "coc747"
	Call mobjGrid.SetWindowParameters(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), Request.QueryString.Item("nWindowTy"))
	'+ Se definen las columnas del grid  
	
	
	With mobjGrid.Columns
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnDraftColumnCaption"), "tcnDraft", 5, CStr(0))
		Call .AddDateColumn(0, GetLocalResourceObject("tcdLimitDateColumnCaption"), "tcdLimitDate")
		Call .AddPossiblesColumn(40423, GetLocalResourceObject("cbeStat_draftColumnCaption"), "cbeStat_draft", "tabStatus_COC747", eFunctions.Values.eValuesType.clngWindowType, vbNullString, True,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeStat_draftColumnToolTip"))
		mobjValues.Parameters.Add("nTypDoc", 1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		
		Call .AddNumericColumn(40629, GetLocalResourceObject("tcnAmountColumnCaption"), "tcnAmount", 19, CStr(0),  ,  , True, 6)
	End With
	
	'+ Se definen las propiedades generales del grid
	
	With mobjGrid
		
		.Codispl = "COC747"
		.DeleteButton = False
		.AddButton = False
		.Columns("Sel").GridVisible = False
	End With
End Sub

'% insPreCOC747: Se cargan los controles de la página
'--------------------------------------------------------------------------------------------
Private Sub insPreCOC747()
	'--------------------------------------------------------------------------------------------
	Dim lclsPremium As eCollection.Premium
	Dim lcolPremiums As eCollection.Premiums
	Dim lintCount As Short
	
	lclsPremium = New eCollection.Premium
	lcolPremiums = New eCollection.Premiums
	If lcolPremiums.FindCOC747(mobjValues.StringToType(Request.QueryString.Item("nInsur_area"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble, True), 0) Then
		lintCount = 0
		
		For	Each lclsPremium In lcolPremiums
			With mobjGrid
				
				If lclsPremium.nContrat <> eRemoteDB.Constants.intNull Then
					.Columns("cbeStat_draft").Parameters.Add("nTypDoc", 2, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				Else
					.Columns("cbeStat_draft").Parameters.Add("nTypDoc", 1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				End If
				
				.Columns("cbeStat_draft").DefValue = CStr(lclsPremium.nStat_draft)
				.Columns("tcnDraft").DefValue = CStr(lclsPremium.nDraft)
				.Columns("tcdLimitDate").DefValue = mobjValues.TypeToString(lclsPremium.dLimitDate, eFunctions.Values.eTypeData.etdDate)
				.Columns("tcnAmount").DefValue = CStr(lclsPremium.nAmount)
				
				Response.Write(.DoRow)
				
			End With
			lintCount = lintCount + 1
			
			If lintCount = 200 Then
				Exit For
			End If
		Next lclsPremium
		
	End If
	Response.Write(mobjGrid.closeTable())
	'+ Se reasignan los valores del ancabezado de la forma
	With Response
		.Write("<SCRIPT>top.fraHeader.document.forms[0].cbeInsur_area.value=" & Request.QueryString.Item("nInsur_area") & ";</" & "Script>")
		.Write("<SCRIPT>top.fraHeader.document.forms[0].cbeBranch.value='" & Request.QueryString.Item("nBranch") & "';</" & "Script>")
		.Write("<SCRIPT>top.fraHeader.document.forms[0].valProduct.value='" & Request.QueryString.Item("nProduct") & "';</" & "Script>")
		.Write("<SCRIPT>top.fraHeader.document.forms[0].tcnPolicy.value='" & Request.QueryString.Item("nPolicy") & "';</" & "Script>")
	End With
	lclsPremium = Nothing
	lcolPremiums = Nothing
End Sub

</script>
<%
Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("coc747")

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.44.07
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "coc747"
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.44.07
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility
%>
<SCRIPT LANGUAGE="JavaScript">
//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
	return true;
}
//------------------------------------------------------------------------------------------
function insPrintCollection(){
//------------------------------------------------------------------------------------------
	insDefValues("COL747_REP"," ");
}
</SCRIPT>


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<HTML>
<HEAD>
<SCRIPT>
//+ Variable para el control de versiones
     document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 16.13 $|$$Author: Nvaplat60 $"
</SCRIPT>
    <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">
    <%Response.Write(mobjValues.StyleSheet())
Response.Write(mobjMenu.setZone(2, "COC747", Request.QueryString.Item("sWindowDescript"), CShort(Request.QueryString.Item("nWindowTy"))))

If Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionQuery) Then
	mobjValues.ActionQuery = True
End If
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="COC747" ACTION="valCollectionQue.aspx?sMode=2">
<%
Response.Write(mobjValues.ShowWindowsName("COC747", Request.QueryString.Item("sWindowDescript")))
Response.Write(mobjValues.ButtonControl("btnShowReport", "Imprimir", "insPrintCollection()"))
Call insDefineHeader()
Call insPreCOC747()
mobjGrid = Nothing
mobjValues = Nothing
%>     
</FORM>
</BODY>
</HTML>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 10.44.07
Call mobjNetFrameWork.FinishPage("coc747")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




