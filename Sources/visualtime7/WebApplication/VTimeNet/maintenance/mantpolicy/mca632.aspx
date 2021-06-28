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

'- Objeto para el manejo particular de los datos de la página
Dim mcolType_amends As ePolicy.Type_amends


'% insDefineHeader: se definen las propiedades del grid
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	Dim Heigh As Object
	
	mobjGrid = New eFunctions.Grid
	
	mobjGrid.sCodisplPage = "mca632"
	
	'+ Se definen las columnas del grid    
	With mobjGrid.Columns
		Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeType_amendColumnCaption"), "cbeType_amend", "table6059", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  , Request.QueryString.Item("Action") = "Update",  , GetLocalResourceObject("cbeType_amendColumnCaption"))
		Call .AddCheckColumn(0, GetLocalResourceObject("chksInd_order_servColumnCaption"), "chksInd_order_serv", vbNullString,  ,  ,  , Request.QueryString.Item("Type") <> "PopUp")
		Call .AddPossiblesColumn(0, GetLocalResourceObject("cbenTypeIssueColumnCaption"), "cbenTypeIssue", "Table5569", eFunctions.Values.eValuesType.clngComboType, CStr(2),  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbenTypeIssueColumnToolTip"))
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnLevelColumnCaption"), "tcnLevel", 5, vbNullString,  , GetLocalResourceObject("tcnLevelColumnCaption"))
		Call .AddCheckColumn(0, GetLocalResourceObject("chkRetarifColumnCaption"), "chkRetarif", "", CShort("1"),  ,  , Request.QueryString.Item("Type") <> "PopUp", GetLocalResourceObject("chkRetarifColumnToolTip"))
	End With
	
	'+ Se definen las propiedades generales del grid
	With mobjGrid
		
		.Codispl = "MCA632"
		.Codisp = "MCA632"
		.ActionQuery = mobjValues.ActionQuery
		.Height = 320
		.Width = 360
		
		.sEditRecordParam = "nBranch=" & Request.QueryString.Item("nBranch") & "&nProduct=" & Request.QueryString.Item("nProduct") & "&dEffecdate=" & Request.QueryString.Item("dEffecdate")
		
		'+  parámetros para eliminación 
		.sDelRecordParam = "nBranch=" & Request.QueryString.Item("nBranch") & "&nProduct=" & Request.QueryString.Item("nProduct") & "&dEffecdate=" & Request.QueryString.Item("dEffecdate") & "&nType_amend='+marrArray[lintIndex].cbeType_amend + '"
		
		.nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
		.Columns("Sel").GridVisible = Not .ActionQuery
		.Columns("cbeType_amend").EditRecord = True
		
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
		
	End With
End Sub

'% insPreMCA632: se realiza el manejo del grid
'--------------------------------------------------------------------------------------------
Private Sub insPreMCA632()
	'--------------------------------------------------------------------------------------------
	Dim lclsType_amend As Object
	Dim lcolType_amends As Object
	
	mcolType_amends = New ePolicy.Type_amends
	
	If mcolType_amends.Find(mobjValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate)) Then
		
		For	Each lclsType_amend In mcolType_amends
			With mobjGrid
				.Columns("cbeType_amend").DefValue = lclsType_amend.nType_amend
				.Columns("cbeType_amend").Descript = lclsType_amend.sDescript
				If lclsType_amend.sInd_order_serv = "1" Then
					.Columns("chksInd_order_serv").Checked = CShort("1")
				Else
					.Columns("chksInd_order_serv").Checked = False
				End If
				.Columns("cbenTypeIssue").DefValue = lclsType_amend.nTypeIssue
				.Columns("tcnLevel").DefValue = lclsType_amend.nLevel
				.Columns("chkRetarif").Checked = lclsType_amend.sRetarif
				Response.Write(.DoRow)
			End With
		Next lclsType_amend
	End If
	
	Response.Write(mobjGrid.closeTable())
End Sub

'% insPreMCA632Upd: Se realiza el manejo de la ventana PopUp asociada al grid
'--------------------------------------------------------------------------------------------
Private Sub insPreMCA632Upd()
	'--------------------------------------------------------------------------------------------
	Dim lobjType_amend As ePolicy.Type_amend
	
	lobjType_amend = New ePolicy.Type_amend
	
	With Request
		If .QueryString.Item("Action") = "Del" Then
			
			Response.Write(mobjValues.ConfirmDelete())
			If lobjType_amend.insPostMCA632(.QueryString.Item("Action"), mobjValues.StringToType(.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.QueryString.Item("nType_amend"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble)) Then
				
			End If
		End If
		Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "valMantPolicy.aspx", "MCA632", .QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(.QueryString.Item("Index"))))
	End With
End Sub

</script>
<%Response.Expires = 0

mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues

mobjValues.ActionQuery = CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401

mobjValues.sCodisplPage = "mca632"
%>
<HTML>
<HEAD>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>



<SCRIPT LANGUAGE=JavaScript>
//+ Variable para el control de versiones
	document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 16:15 $"
</SCRIPT>
	<%
Response.Write(mobjValues.StyleSheet())
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write(mobjMenu.setZone(2, "MCA632", "MCA632.aspx"))
	mobjMenu = Nothing
	Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
End If
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" NAME="MCA632" ACTION="valMantPolicy.aspx?sMode=1&<%=Request.Params.Get("Query_String")%>">
<%Response.Write(mobjValues.ShowWindowsName("MCA632"))
Call insDefineHeader()
If Request.QueryString.Item("Type") = "PopUp" Then
	Call insPreMCA632Upd()
Else
	Call insPreMCA632()
End If
%>
</FORM> 
</BODY>
</HTML>




