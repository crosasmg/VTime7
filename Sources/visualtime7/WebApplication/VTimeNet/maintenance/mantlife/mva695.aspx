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
Dim mcolCtrol_premins As eBranches.Ctrol_premins


'% insDefineHeader: se definen las propiedades del grid
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	
	'+ Se definen las columnas del grid    
	With mobjGrid.Columns
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnMonthColumnCaption"), "tcnMonth", 2, vbNullString,  , GetLocalResourceObject("tcnMonthColumnToolTip"),  , 0,  ,  ,  , Request.QueryString.Item("Action") = "Update")
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnRateColumnCaption"), "tcnRate", 9, vbNullString,  , GetLocalResourceObject("tcnRateColumnToolTip"), False, 6)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnAmountColumnCaption"), "tcnAmount", 18, vbNullString,  , GetLocalResourceObject("tcnAmountColumnToolTip"), True, 6)
	End With
	
	'+ Se definen las propiedades generales del grid
	With mobjGrid
		.Codispl = "MVA695"
		.sCodisplPage = "MVA695"
		.ActionQuery = mobjValues.ActionQuery
		.Columns("tcnMonth").EditRecord = True
		.Height = 220
		.Width = 310
		.nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
		.Columns("Sel").GridVisible = Not .ActionQuery
		.sDelRecordParam = "nMonth=' + marrArray[lintIndex].tcnMonth + '"
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
End Sub

'% insPreMVA695: se realiza el manejo del grid
'--------------------------------------------------------------------------------------------
Private Sub insPreMVA695()
	'--------------------------------------------------------------------------------------------
	Dim lclsCtrol_premin As Object
	
	mcolCtrol_premins = New eBranches.Ctrol_premins
	
	If mcolCtrol_premins.Find(mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), Session("dEffecdate")) Then
		For	Each lclsCtrol_premin In mcolCtrol_premins
			With mobjGrid
				.Columns("tcnMonth").DefValue = lclsCtrol_premin.nMonth
				.Columns("tcnRate").DefValue = lclsCtrol_premin.nRate
				.Columns("tcnAmount").DefValue = lclsCtrol_premin.nAmount
				Response.Write(.DoRow)
			End With
		Next lclsCtrol_premin
	End If
	
	Response.Write(mobjGrid.closeTable())
End Sub

'% insPreMVA695Upd: Se realiza el manejo de la ventana PopUp asociada al grid
'--------------------------------------------------------------------------------------------
Private Sub insPreMVA695Upd()
	'--------------------------------------------------------------------------------------------
	Dim lclsCtrol_premin As eBranches.Ctrol_premin
	
	
	With Request
		If Request.QueryString.Item("Action") = "Del" Then
			lclsCtrol_premin = New eBranches.Ctrol_premin
			
			Response.Write(mobjValues.ConfirmDelete())
			Call lclsCtrol_premin.InsPostMVA695(.QueryString.Item("Action"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nMonth"), eFunctions.Values.eTypeData.etdDouble), Session("dEffecdate"), eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
			
			lclsCtrol_premin = Nothing
		End If
		
		Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "valMantLife.aspx", "MVA695", .QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(.QueryString.Item("Index"))))
	End With
End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues

mobjValues.ActionQuery = CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401
mobjValues.sCodisplPage = "MVA695"
%>
<HTML>
<HEAD>
<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE=JavaScript>
//- Variable para el control de versiones
       document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 16:06 $"
</SCRIPT>    
<%
Response.Write(mobjValues.StyleSheet())
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write(mobjMenu.setZone(2, "MVA695", "MVA695.aspx"))
	mobjMenu = Nothing
	Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
End If
%>
</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" NAME="MVA695.aspx" ACTION="valMantLife.aspx?sMode=2">
<%Response.Write(mobjValues.ShowWindowsName("MVA695"))
Call insDefineHeader()

If Request.QueryString.Item("Type") = "PopUp" Then
	Call insPreMVA695Upd()
Else
	Call insPreMVA695()
End If
%>
</FORM> 
</BODY>
</HTML>




