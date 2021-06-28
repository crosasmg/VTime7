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
Dim mcolTab_comm_al As eBranches.tab_comm_als


'% insDefineHeader: se definen las propiedades del grid
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	Dim blnActionUpd As Boolean
	
	blnActionUpd = Request.QueryString.Item("Action") = "Update"
	
	mobjGrid = New eFunctions.Grid
	
	'+ Se definen las columnas del grid    
	With mobjGrid.Columns
		Call .AddPossiblesColumn(0, GetLocalResourceObject("valAgreementColumnCaption"), "valAgreement", "tabAgreement_al", eFunctions.Values.eValuesType.clngWindowType, CStr(0), True,  ,  ,  ,  , CBool(blnActionUpd),  , GetLocalResourceObject("valAgreementColumnToolTip"))
		mobjGrid.Columns("valAgreement").Parameters.Add("sStatregt", "1", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnQPBColumnCaption"), "tcnQPB", 5, vbNullString,  , GetLocalResourceObject("tcnQPBColumnCaption"), True,  ,  ,  ,  , blnActionUpd)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnPercentColumnCaption"), "tcnPercent", 5, vbNullString,  , GetLocalResourceObject("tcnPercentColumnToolTip"), True, 2)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnAmountColumnCaption"), "tcnAmount", 18, "",  , GetLocalResourceObject("tcnAmountColumnToolTip"), True, 6)
	End With
	
	'+ Se definen las propiedades generales del grid
	With mobjGrid
		.Codispl = "MVA645"
		.sCodisplPage = "MVA645"
		.ActionQuery = mobjValues.ActionQuery
		.Columns("tcnQPB").EditRecord = True
		.Height = 250
		.Width = 350
		.nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
		.Columns("Sel").GridVisible = Not .ActionQuery
		.sDelRecordParam = "nAgreement=' + marrArray[lintIndex].valAgreement + '" & "&nQPB=' + marrArray[lintIndex].tcnQPB + '"
		
		
		'+Duplicacion de la tabla
		If Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionDuplicate) Then
			.Columns("valAgreement").EditRecord = False
			.Columns("Sel").GridVisible = False
			.Columns("Sel").Disabled = False
			.AddButton = False
			.DeleteButton = False
		End If
		
		
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
End Sub

'% insPreMVA645: se realiza el manejo del grid
'--------------------------------------------------------------------------------------------
Private Sub insPreMVA645()
	'--------------------------------------------------------------------------------------------
	Dim lclsTab_comm_al As eBranches.tab_comm_al
	
	lclsTab_comm_al = New eBranches.tab_comm_al
	mcolTab_comm_al = New eBranches.tab_comm_als
	
	If mcolTab_comm_al.Find_Agreement(mobjValues.StringToType(Session("nComtabli"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nIntertyp"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nSellChannel"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nWay_pay"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nModulec"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCover"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate)) Then
		With mobjGrid
                For Each lclsTab_comm_al In mcolTab_comm_al
                    
                    If lclsTab_comm_al.nAgreement = 999 Then
                        lclsTab_comm_al.nAgreement = eRemoteDB.Constants.intNull
                    End If
                    
                    .Columns("valAgreement").DefValue = CStr(lclsTab_comm_al.nAgreement)
                    .Columns("tcnQPB").DefValue = CStr(lclsTab_comm_al.nQPB)
                    .Columns("tcnPercent").DefValue = CStr(lclsTab_comm_al.nPercent)
                    .Columns("tcnAmount").DefValue = CStr(lclsTab_comm_al.nAmount)
				
                    Response.Write(.DoRow)
                Next lclsTab_comm_al
		End With
	End If
	Response.Write(mobjGrid.closeTable())
	lclsTab_comm_al = Nothing
	mcolTab_comm_al = Nothing
End Sub

'% insPreMVA645Upd: Se realiza el manejo de la ventana PopUp asociada al grid
'--------------------------------------------------------------------------------------------
Private Sub insPreMVA645Upd()
	'--------------------------------------------------------------------------------------------
	Dim lobjTab_comm_al As eBranches.tab_comm_al
	
	lobjTab_comm_al = New eBranches.tab_comm_al
	
	With Request
		If Request.QueryString.Item("Action") = "Del" Then
			Response.Write(mobjValues.ConfirmDelete())
			If lobjTab_comm_al.insPostMVA645(.QueryString.Item("Action"), mobjValues.StringToType(Session("nComtabli"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nIntertyp"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nSellChannel"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nWay_pay"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nModulec"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCover"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nAgreement"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nQPB"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate),  ,  ,  , mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble)) Then
			End If
		End If
		
		Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "valMantLife.aspx", "MVA645", .QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(.QueryString.Item("Index"))))
	End With
	lobjTab_comm_al = Nothing
End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues
mobjValues.sCodisplPage = "MVA645"
%>
<HTML>
<HEAD>
<SCRIPT>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 16:06 $|$$Author: Nvaplat61 $"
</SCRIPT>
<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>




<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<%
Response.Write(mobjValues.StyleSheet())
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write(mobjMenu.setZone(2, "MVA645", "MVA645.aspx"))
	mobjMenu = Nothing
	Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
End If
%>
<SCRIPT LANGUAGE=JavaScript>
//% insStateZone: se controla el estado de los campos de la página
//--------------------------------------------------------------------------------------------
function insStateZone(){
//--------------------------------------------------------------------------------------------
    with (self.document.forms[0]){
        btnvalModulec.disabled = valModulec.disabled;
    }
}
//% insChangeField: se controla la modificación de los campos de parametros
//--------------------------------------------------------------------------------------------
function insChangeField(vObj){
//--------------------------------------------------------------------------------------------
	var sValue, bNullValue;
	
	sValue = vObj.value;
	bNullValue = (sValue == '');
	
	with (self.document.forms[0]){
		switch (vObj.name){
			case 'cbeBranch':
				valProduct.Parameters.Param1.sValue=sValue;
				valModulec.Parameters.Param1.sValue=sValue;
				valCover.Parameters.Param2.sValue=sValue;
				valProduct.disabled = btnvalProduct.disabled = bNullValue;
				valCover.value = valModulec.value = '0';
				UpdateDiv('valModulecDesc','','Normal');
				UpdateDiv('valCoverDesc','','Normal');
				break;

			case 'valProduct':
				valModulec.Parameters.Param2.sValue=sValue;
				valCover.Parameters.Param3.sValue=sValue;
				valCover.Parameters.Param4.sValue=0;
				valModulec.disabled = btnvalModulec.disabled = bNullValue;
				valCover.disabled = btnvalCover.disabled = bNullValue;
				break;

			case 'tcdEffecdate':
				valModulec.Parameters.Param3.sValue=sValue;
				valCover.Parameters.Param4.sValue=sValue;
				break;

			case 'valModulec':
				if (sValue == '') sValue = '0';
				valCover.Parameters.Param4.sValue=sValue;
				break;
		}
		if (bNullValue)	valModulec.value = valCover.value = '0';
	}
}
</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" NAME="MVA645.aspx" ACTION="valMantLife.aspx?sMode=2">
<%Response.Write(mobjValues.ShowWindowsName("MVA645"))

Call insDefineHeader()
If Request.QueryString.Item("Type") = "PopUp" Then
	Call insPreMVA645Upd()
Else
	Call insPreMVA645()
End If
%>
</FORM> 
</BODY>
</HTML>





