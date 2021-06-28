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
Dim mclsDisc_riskInsu As eBranches.Disc_riskInsu


'% insDefineHeader: se definen las propiedades del grid
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	'+ Se definen las columnas del grid    
	With mobjGrid.Columns
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnCapital_initColumnCaption"), "tcnCapital_init", 18, vbNullString,  , GetLocalResourceObject("tcnCapital_initColumnToolTip"), True, 6,  ,  ,  , Request.QueryString.Item("Action") = "Update")
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnCapital_endColumnCaption"), "tcnCapital_end", 18, vbNullString,  , GetLocalResourceObject("tcnCapital_endColumnToolTip"), True, 6,  ,  ,  , Request.QueryString.Item("Action") = "Update")
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnRateColumnCaption"), "tcnRate", 9, "",  , GetLocalResourceObject("tcnRateColumnToolTip"),  , 6)
	End With
	
	'+ Se definen las propiedades generales del grid
	With mobjGrid
		.Codispl = "MVI805"
		.sCodisplPage = "MVI805"
		.ActionQuery = mobjValues.ActionQuery
		.Height = 260
		.Width = 300
		.WidthDelete = 400
		.Top = 100
		.Columns("tcnCapital_init").EditRecord = True
		.nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
		.Columns("Sel").GridVisible = Not .ActionQuery
		.sDelRecordParam = "nCapital_init=' + marrArray[lintIndex].tcnCapital_init + '"
		
		If Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionUpdate) Then
			.Columns("Sel").GridVisible = True
			.Columns("tcnCapital_init").EditRecord = True
		End If
		
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
End Sub

'% insPreMVI771: se realiza el manejo del grid
'--------------------------------------------------------------------------------------------
Private Sub insPreMVI805()
	'--------------------------------------------------------------------------------------------
	Dim lintIndex As Object
	Dim lblnFind As Object
	Dim mobjDisc_riskInsus As eBranches.Disc_riskInsus
	mobjDisc_riskInsus = New eBranches.Disc_riskInsus
	If mobjDisc_riskInsus.Find(mobjValues.StringToType(session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(session("dEffecdate"), eFunctions.Values.eTypeData.etdDate)) Then
		For	Each mclsDisc_riskInsu In mobjDisc_riskInsus
			With mobjGrid
				.Columns("tcnCapital_init").DefValue = CStr(mclsDisc_riskInsu.nCapital_init)
				.Columns("tcnCapital_end").DefValue = CStr(mclsDisc_riskInsu.nCapital_end)
				.Columns("tcnRate").DefValue = CStr(mclsDisc_riskInsu.nRate)
				Response.Write(.DoRow)
			End With
		Next mclsDisc_riskInsu
	End If
	Response.Write(mobjGrid.closeTable())
	mobjDisc_riskInsus = Nothing
End Sub
'% insPreMVI771Upd: Se realiza el manejo de la ventana PopUp asociada al grid
'--------------------------------------------------------------------------------------------
Private Sub insPreMVI805Upd()
	'--------------------------------------------------------------------------------------------
	With Request
		If Request.QueryString.Item("Action") = "Del" Then
			Response.Write(mobjValues.ConfirmDelete())
			
			Call mclsDisc_riskInsu.insPostMVI805(.QueryString.Item("Action"), mobjValues.StringToType(session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.QueryString.Item("nCapital_Init"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCapital_End"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnRate"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
		End If
		'Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "valMantLife.aspx", "MVI805", .QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(.QueryString.Item("Index"))))
		Response.Write(mobjGrid.DoFormUpd(.QueryString("Action"), "valMantLife.aspx", "MVI805", .QueryString("nMainAction"), mobjValues.ActionQuery, .QueryString("Index")))
'UPGRADE_ISSUE: The preceding line couldn't be parsed. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1010.aspx'
	End With
End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mclsDisc_riskInsu = New eBranches.Disc_riskInsu
mobjMenu = New eFunctions.Menues
mobjValues.ActionQuery = CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401
mobjValues.sCodisplPage = "MVI805"
%>
<HTML>
<HEAD>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>




<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<%
Response.Write(mobjValues.StyleSheet())
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write(mobjMenu.setZone(2, "MVI805", "MVI805.aspx"))
	mobjMenu = Nothing
	Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
End If
%>
<SCRIPT>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 16:06 $|$$Author: Nvaplat61 $"
</SCRIPT>    
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" NAME="MVI805" ACTION="valMantLife.aspx?sMode=2">
<%Response.Write(mobjValues.ShowWindowsName("MVI805"))
Call insDefineHeader()
If Request.QueryString.Item("Type") = "PopUp" Then
	Call insPreMVI805Upd()
Else
	Call insPreMVI805()
End If

mobjValues = Nothing
mobjGrid = Nothing
%>
</FORM> 
</BODY>
</HTML>





