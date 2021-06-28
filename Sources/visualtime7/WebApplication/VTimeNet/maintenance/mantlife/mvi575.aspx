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
Dim mcolCap_educind As eBranches.Cap_educinds


'% insDefineHeader: se definen las propiedades del grid
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	
	'+ Se definen las columnas del grid    
	With mobjGrid.Columns
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnAgeColumnCaption"), "tcnAge", 5, vbNullString,  , GetLocalResourceObject("tcnAgeColumnCaption"),  , 0,  ,  ,  , Request.QueryString.Item("Action") = "Update")
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnCapschoolColumnCaption"), "tcnCapschool", 18, vbNullString,  , GetLocalResourceObject("tcnCapschoolColumnToolTip"), True, 6)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnCaphschoColumnCaption"), "tcnCaphscho", 18, vbNullString,  , GetLocalResourceObject("tcnCaphschoColumnToolTip"), True, 6)
		Call .AddPossiblesColumn(0, GetLocalResourceObject("valCurrencyColumnCaption"), "valCurrency", "Table11", eFunctions.Values.eValuesType.clngWindowType, CStr(0),  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("valCurrencyColumnToolTip"))
	End With
	
	'+ Se definen las propiedades generales del grid
	With mobjGrid
		.Codispl = "MVI575"
		.sCodisplPage = "MVI575"
		.ActionQuery = mobjValues.ActionQuery
		.Columns("tcnAge").EditRecord = True
		.Height = 280
		.Width = 320
		.nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
		.Columns("Sel").GridVisible = Not .ActionQuery
		.sDelRecordParam = "nAge=' + marrArray[lintIndex].tcnAge + '" & "&nBranch=" & Request.QueryString.Item("nBranch") & "&nProduct=" & Request.QueryString.Item("nProduct") & "&dEffecdate=" & Request.QueryString.Item("dEffecdate")
		.sEditRecordParam = "nBranch=" & Request.QueryString.Item("nBranch") & "&nProduct=" & Request.QueryString.Item("nProduct") & "&dEffecdate=" & Request.QueryString.Item("dEffecdate")
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
End Sub

'% insPreMVI575: se realiza el manejo del grid
'--------------------------------------------------------------------------------------------
Private Sub insPreMVI575()
	'--------------------------------------------------------------------------------------------
	Dim lclsCap_educind As Object
	
	mcolCap_educind = New eBranches.Cap_educinds
	
	If mcolCap_educind.Find(mobjValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate)) Then
		For	Each lclsCap_educind In mcolCap_educind
			With mobjGrid
				.Columns("tcnAge").DefValue = lclsCap_educind.nAge
				.Columns("tcnCapschool").DefValue = lclsCap_educind.nCapschool
				.Columns("tcnCaphscho").DefValue = lclsCap_educind.nCaphscho
				.Columns("valCurrency").DefValue = lclsCap_educind.nCurrency
				
				Response.Write(.DoRow)
			End With
		Next lclsCap_educind
	End If
	
	Response.Write(mobjGrid.closeTable())
End Sub

'% insPreMVI575Upd: Se realiza el manejo de la ventana PopUp asociada al grid
'--------------------------------------------------------------------------------------------
Private Sub insPreMVI575Upd()
	'--------------------------------------------------------------------------------------------
	Dim lobjCap_educind As eBranches.Cap_educind
	
	lobjCap_educind = New eBranches.Cap_educind
	
	With Request
		If Request.QueryString.Item("Action") = "Del" Then
			Response.Write(mobjValues.ConfirmDelete())
			If lobjCap_educind.InsPostMVI575(.QueryString.Item("Action"), mobjValues.StringToType(.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nAge"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valCurrency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcnCapschool"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCaphscho"), eFunctions.Values.eTypeData.etdDouble), Session("dNulldate"), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble)) Then
			End If
		End If
		
		Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "valMantLife.aspx", "MVI575", .QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(.QueryString.Item("Index"))))
	End With
End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues

mobjValues.ActionQuery = CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401
mobjValues.sCodisplPage = "MVI575"
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
	Response.Write(mobjMenu.setZone(2, "MVI575", "MVI575.aspx"))
	mobjMenu = Nothing
	Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
End If
%>
</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" NAME="MVI575.aspx" ACTION="valMantLife.aspx?nBranch=<%=Request.QueryString.Item("nBranch")%>&nProduct=<%=Request.QueryString.Item("nProduct")%>&dEffecdate=<%=Request.QueryString.Item("dEffecdate")%>">
<%Response.Write(mobjValues.ShowWindowsName("MVI575"))
Call insDefineHeader()

If Request.QueryString.Item("Type") = "PopUp" Then
	Call insPreMVI575Upd()
Else
	Call insPreMVI575()
End If
%>
</FORM> 
</BODY>
</HTML>




