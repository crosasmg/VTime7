<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eGeneral" %>
<script language="VB" runat="Server">
'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
'- Objeto para el manejo de las rutinas genéricas
Dim mobjMenu As eFunctions.Menues
Dim mobjGrid As eFunctions.Grid


'------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	
	mobjGrid.sCodisplPage = "ms011_k"
	
	'+ Se definen las columns del Grid
	With mobjGrid.Columns
		If Request.QueryString.Item("Action") = "Update" Then
			Call .AddPossiblesColumn(41683, GetLocalResourceObject("cbenTypenumColumnCaption"), "cbenTypenum", "Table297", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  , "ChangeTables()", True,  , GetLocalResourceObject("cbenTypenumColumnToolTip"))
		Else
			Call .AddPossiblesColumn(41683, GetLocalResourceObject("cbenTypenumColumnCaption"), "cbenTypenum", "Table297", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  , "ChangeTables()",  ,  , GetLocalResourceObject("cbenTypenumColumnToolTip"))
		End If
		If Request.QueryString.Item("Action") = "Update" Then
			Call .AddPossiblesColumn(41685, GetLocalResourceObject("cbenOrd_numColumnCaption"), "cbenOrd_num", "Table10", eFunctions.Values.eValuesType.clngWindowType,  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbenOrd_numColumnToolTip"))
		Else
			Call .AddPossiblesColumn(41685, GetLocalResourceObject("cbenOrd_numColumnCaption"), "cbenOrd_num", "Table10", eFunctions.Values.eValuesType.clngWindowType,  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbenOrd_numColumnToolTip"))
		End If
		Call .AddNumericColumn(41686, GetLocalResourceObject("tcnInitialColumnCaption"), "tcnInitial", 10, CStr(eRemoteDB.Constants.strNull), True, GetLocalResourceObject("tcnInitialColumnToolTip"), True)
		Call .AddNumericColumn(41687, GetLocalResourceObject("tcnEnd_numColumnCaption"), "tcnEnd_num", 10, CStr(eRemoteDB.Constants.strNull), True, GetLocalResourceObject("tcnEnd_numColumnToolTip"), True)
		Call .AddNumericColumn(41688, GetLocalResourceObject("tcnLastnumbColumnCaption"), "tcnLastnumb", 10, CStr(eRemoteDB.Constants.strNull), True, GetLocalResourceObject("tcnLastnumbColumnToolTip"), True)
	End With
	'+ Se asignan las caracteristicas del Grid
	With mobjGrid
		.Columns("cbenTypenum").EditRecord = True
		.Columns("cbenTypenum").BlankPosition = False
		.Codispl = Request.QueryString.Item("sCodispl")
		.ActionQuery = Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionQuery) Or Request.QueryString.Item("nMainAction") = vbNullString
		.Codisp = "MS011_K"
		.sDelRecordParam = "sCodisp=" & Session("sCodispl") & "&nTypenum='+ marrArray[lintIndex].cbenTypenum + '" & "&nOrd_num='+ marrArray[lintIndex].cbenOrd_num + '" & "&nInitial='+ marrArray[lintIndex].tcnInitial + '" & "&nEnd_num='+ marrArray[lintIndex].tcnEnd_num + '" & "&nLastnumb='+ marrArray[lintIndex].tcnLastnumb + '"
		.Height = 280
		.Width = 350
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
End Sub
'------------------------------------------------------------------------------
Private Sub insPreMS011_K()
	'------------------------------------------------------------------------------
	Dim lcolNumerators As eGeneral.Numerators
	Dim lclsNumerator As eGeneral.Numerator
	lclsNumerator = New eGeneral.Numerator
	lcolNumerators = New eGeneral.Numerators
	If lcolNumerators.Find(vbNullString, 1) Then
		For	Each lclsNumerator In lcolNumerators
			With mobjGrid
				.Columns("cbenTypenum").DefValue = CStr(lclsNumerator.nTypenum)
				Select Case .Columns("cbenTypenum").DefValue
					Case "16"
						.Columns("cbenOrd_num").TableName = "Table9"
					Case Else
                            .Columns("cbenOrd_num").TableName = "Table10"
				End Select
                    .Columns("cbenOrd_num").DefValue = CStr(lclsNumerator.nOrd_num)
                    .Columns("cbenOrd_num").Descript= lclsNumerator.sShort_des2
				.Columns("tcnInitial").DefValue = CStr(lclsNumerator.nInitial)
				.Columns("tcnEnd_num").DefValue = CStr(lclsNumerator.nEnd_num)
				.Columns("tcnLastnumb").DefValue = CStr(lclsNumerator.nLastnumb)
			End With
			'+Se ejecuta el metodo DoRow, que se encarga de mostrar los elementos de grid
			Response.Write(mobjGrid.DoRow())
		Next lclsNumerator
	End If
	Response.Write(mobjGrid.closeTable())
	lclsNumerator = Nothing
	lcolNumerators = Nothing
End Sub
'------------------------------------------------------------------------------
Private Sub insPreMS011_K_Upd()
	'------------------------------------------------------------------------------
	Dim lclsNumerator As eGeneral.Numerator
	Dim lstrErrors As String
	With Request
		If .QueryString.Item("Action") = "Del" Then
			lclsNumerator = New eGeneral.Numerator
			lstrErrors = lclsNumerator.insValMS011_K(.QueryString.Item("sCodispl"), .QueryString.Item("Action"), mobjValues.StringToType(.QueryString.Item("nTypenum"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nOrd_num"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nInitial"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nEnd_num"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nLastnumb"), eFunctions.Values.eTypeData.etdDouble), Session("nUsercode"))
			If lstrErrors = vbNullString Then
				Response.Write(mobjValues.ConfirmDelete())
				With lclsNumerator
					.nTypenum = mobjValues.StringToType(Request.QueryString.Item("nTypenum"), eFunctions.Values.eTypeData.etdDouble)
					.nOrd_num = mobjValues.StringToType(Request.QueryString.Item("nOrd_num"), eFunctions.Values.eTypeData.etdDouble)
					.Delete()
				End With
			Else
				Response.Write(lstrErrors)
			End If
			lclsNumerator = Nothing
		End If
	End With
	Response.Write(mobjGrid.DoFormUpd(Request.QueryString.Item("Action"), "valMantsys.aspx", Request.QueryString.Item("sCodispl"), Request.QueryString.Item("MainAction"),  , CShort(Request.QueryString.Item("Index"))))
	Response.Write(mobjValues.HiddenControl("sAction", Request.QueryString.Item("Action")))
End Sub

</script>
<%Response.Expires = -1
mobjValues = New eFunctions.Values

mobjValues.sCodisplPage = "ms011_k"
%>




<HTML>
<HEAD>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">
<%
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write("<SCRIPT>var nMainAction=0</SCRIPT>")
	Response.Write("<SCRIPT LANGUAGE=""JavaScript"" SRC=""/VTimeNet/Scripts/tmenu.js""></SCRIPT>" & vbCrLf)
End If
With Response
	.Write(mobjValues.StyleSheet() & vbCrLf)
	If Request.QueryString.Item("Type") <> "PopUp" Then
		mobjMenu = New eFunctions.Menues
		.Write(mobjMenu.MakeMenu(Request.QueryString.Item("sCodispl"), "MS011_k.aspx", 1, ""))
		mobjMenu = Nothing
	End If
End With
%>
<SCRIPT>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 16:29 $"
//-------------------------------------------------------------------------------------------------------------------
function insStateZone(){}
//-------------------------------------------------------------------------------------------------------------------
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
function insCancel(){
//------------------------------------------------------------------------------------------
	return true;
}
//------------------------------------------------------------------------------------------
function insFinish(){
//------------------------------------------------------------------------------------------
	return true;
}
//------------------------------------------------------------------------------------------
function ChangeTables(){
/*-------------------------------------------------------------------------------------------*/
	with(self.document.forms[0]){
		if(cbenTypenum.value=="16"){
			cbenOrd_num.sTabName = "Table9"
			cbenOrd_num.value = ""
			UpdateDiv("cbenOrd_numDesc","" ,'Normal')
		}
		else{
			cbenOrd_num.sTabName = "Table10"
			cbenOrd_num.value = ""
			UpdateDiv("cbenOrd_numDesc","" ,'Normal')
		}
	}
}
</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<%
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write("<BR><BR>")
End If
%>
<FORM METHOD="POST" ID="FORM" NAME="frmNumerator" ACTION="valmantsys.aspx?mode=1">
<%
Response.Write("<SCRIPT>var	nMainAction	= 0" & Request.QueryString.Item("nMainAction") & "</SCRIPT>")
Response.Write(mobjValues.ShowWindowsName("MS011"))
Call insDefineHeader()
If Request.QueryString.Item("Type") <> "PopUp" Then
	Call insPreMS011_K()
Else
	Call insPreMS011_K_Upd()
End If
mobjValues = Nothing
%>
</FORM>
</BODY>
</HTML>




