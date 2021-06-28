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
Dim mclsLeg As eBranches.Leg


'% insDefineHeader: se definen las propiedades del grid
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	'+ Se definen las columnas del grid    
	With mobjGrid.Columns
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnCapitalIColumnCaption"), "tcnCapitalI", 18, vbNullString,  , GetLocalResourceObject("tcnCapitalIColumnToolTip"),  , 6,  ,  ,  , Request.QueryString.Item("Action") = "Update")
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnCapitalFColumnCaption"), "tcnCapitalF", 18, vbNullString,  , GetLocalResourceObject("tcnCapitalFColumnToolTip"),  , 6,  ,  ,  , Request.QueryString.Item("Action") = "Update")
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnAmountbasColumnCaption"), "tcnAmountbas", 18, vbNullString,  , GetLocalResourceObject("tcnAmountbasColumnToolTip"), True, 6)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnFactColumnCaption"), "tcnFact", 6, vbNullString,  , GetLocalResourceObject("tcnFactColumnCaption"), True, 3)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnAmountmaxColumnCaption"), "tcnAmountmax", 18, vbNullString,  , GetLocalResourceObject("tcnAmountmaxColumnCaption"), True, 6)
		Call .AddHiddenColumn("cbeAuxCurrency", vbNullString)
	End With
	'+ Se definen las propiedades generales del grid
	With mobjGrid
		.Codispl = "MVI706"
		.sCodisplPage = "MVI706"
		.ActionQuery = mobjValues.ActionQuery
		.Height = 290
		.Width = 300
		.nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
		.Columns("Sel").GridVisible = Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionUpdate)
		.sDelRecordParam = "nBranch=" & Request.QueryString.Item("nBranch") & "&nProduct=" & Request.QueryString.Item("nProduct") & "&dEffecdate=" & Request.QueryString.Item("dEffecdate") & "&nCapitalI=' + marrArray[lintIndex].tcnCapitalI + '"
		.sEditRecordParam = "nBranch=" & Request.QueryString.Item("nBranch") & "&nProduct=" & Request.QueryString.Item("nProduct") & "&dEffecdate=" & Request.QueryString.Item("dEffecdate")
		.Columns("tcnCapitalI").EditRecord = True
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
End Sub
'% insPreMVI706: se realiza el manejo del grid
'--------------------------------------------------------------------------------------------
Private Sub insPreMVI706()
	'--------------------------------------------------------------------------------------------
	Dim lintIndex As Integer
	Dim lblnFind As Boolean
	lblnFind = mclsLeg.Find_All(mobjValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate))
	Call mclsLeg.Item(0)
	
Response.Write("" & vbCrLf)
Response.Write("    <TABLE WIDTH=""100%"">" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("			<TD WIDTH=15%><LABEL ID=0>" & GetLocalResourceObject("cbeCurrencyCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.PossiblesValues("cbeCurrency", "Table11", eFunctions.Values.eValuesType.clngComboType, CStr(mclsLeg.nCurrency),  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeCurrencyToolTip")))


Response.Write(" </TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("    </TABLE>" & vbCrLf)
Response.Write("    <BR>")

	
	If lblnFind Then
		For lintIndex = 0 To mclsLeg.CountItem
			Call mclsLeg.Item(lintIndex)
			With mobjGrid
				.Columns("tcnCapitalI").DefValue = CStr(mclsLeg.nCapitalI)
				.Columns("tcnCapitalF").DefValue = CStr(mclsLeg.nCapitalF)
				.Columns("tcnAmountbas").DefValue = CStr(mclsLeg.nAmountbas)
				.Columns("tcnFact").DefValue = CStr(mclsLeg.nFact)
				.Columns("tcnAmountmax").DefValue = CStr(mclsLeg.nAmountmax)
				Response.Write(.DoRow)
			End With
		Next 
		'+ Si para la fecha en la cual se realiza la operación existen registros en la tabla,
		'+ el campo moneda no se habilita.		
		If Request.QueryString.Item("nMainAction") <> CStr(eFunctions.Menues.TypeActions.clngActionQuery) Then
			Response.Write("<SCRIPT>self.document.forms[0].cbeCurrency.disabled=true;</" & "Script>")
		End If
	End If
	Response.Write(mobjGrid.closeTable())
End Sub
'% insPreMVI706Upd: Se realiza el manejo de la ventana PopUp asociada al grid
'--------------------------------------------------------------------------------------------
Private Sub insPreMVI706Upd()
	'--------------------------------------------------------------------------------------------
	With Request
		If Request.QueryString.Item("Action") = "Del" Then
			Response.Write(mobjValues.ConfirmDelete())
			Call mclsLeg.insPostMVI706(Request.QueryString.Item("Action"), mobjValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.QueryString.Item("nCapitalI"), eFunctions.Values.eTypeData.etdDouble),  ,  ,  ,  ,  , mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
		End If
		Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "valMantLife.aspx", "MVI706", .QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(.QueryString.Item("Index"))))
		If Request.QueryString.Item("Action") <> "Del" Then
			Response.Write("<SCRIPT>self.document.forms[0].cbeAuxCurrency.value=top.opener.document.forms[0].cbeCurrency.value</" & "Script>")
		End If
	End With
End Sub

</script>
<%Response.Expires = -1
mobjValues = New eFunctions.Values
mclsLeg = New eBranches.Leg
mobjMenu = New eFunctions.Menues
mobjValues.ActionQuery = CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401
mobjValues.sCodisplPage = "MVI706"
%>
<HTML>
<HEAD>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>



    
<SCRIPT>
//- Variable para el control de versiones
	document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 16:06 $|$$Author: Nvaplat61 $"
</SCRIPT>
	<%
Response.Write(mobjValues.StyleSheet())
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write(mobjMenu.setZone(2, "MVI706", "MVI706.aspx"))
	mobjMenu = Nothing
	Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
End If
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" NAME="MVI706" ACTION="valMantLife.aspx?sMode=2">
<%Response.Write(mobjValues.ShowWindowsName("MVI706"))
Call insDefineHeader()
If Request.QueryString.Item("Type") = "PopUp" Then
	Call insPreMVI706Upd()
Else
	Call insPreMVI706()
End If
%>
</FORM> 
</BODY>
</HTML>




