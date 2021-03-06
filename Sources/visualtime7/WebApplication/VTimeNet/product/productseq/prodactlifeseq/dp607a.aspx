<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eProduct" %>
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
	
	mobjGrid.sCodisplPage = "dp607a"
	
	'+ Se definen las columnas del grid    
	With mobjGrid.Columns
		Call .AddPossiblesColumn(0, GetLocalResourceObject("valModulecColumnCaption"), "valModulec", "tabtab_modul", eFunctions.Values.eValuesType.clngWindowType, Request.QueryString.Item("nModulec"), True,  ,  ,  ,  , True, 5, GetLocalResourceObject("valModulecColumnToolTip"))
		With mobjGrid.Columns("valModulec").Parameters
			.Add("nBranch", mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Add("nProduct", mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Add("dEffecdate", mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		End With
		Call .AddHiddenColumn("tctModulec", Request.QueryString.Item("sModulecDesc"))
		Call .AddHiddenColumn("cbeModulec", Request.QueryString.Item("nModulec"))
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnCapMinColumnCaption"), "tcnCapMin", 18, vbNullString,  , GetLocalResourceObject("tcnCapMinColumnToolTip"), True, 6)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnMChanInvesColumnCaption"), "tcnMChanInves", 5, vbNullString,  , GetLocalResourceObject("tcnMChanInvesColumnToolTip"), True)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnErrRangeColumnCaption"), "tcnErrRange", 18, vbNullString,  , GetLocalResourceObject("tcnErrRangeColumnToolTip"), True, 6)
		Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeOptionColumnCaption"), "cbeOption", "Table5519", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeOptionColumnToolTip"))
		Call .AddHiddenColumn("hddOption", vbNullString)
	End With
	
	Response.Write(mobjValues.HiddenControl("tcnCurrency", Session("nCurrency")))
	
	'+ Se definen las propiedades generales del grid
	With mobjGrid
		.Codispl = "DP607A"
		.Codisp = "DP607A"
		.Height = 310
		.Width = 350
		.AddButton = False
		.DeleteButton = False
		.ActionQuery = mobjValues.ActionQuery
		.Columns("Sel").GridVisible = True
		.Columns("valModulec").EditRecord = True
		
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
End Sub


'% insPreDP607A: se realiza el manejo del grid
'--------------------------------------------------------------------------------------------
Private Sub insPreDP607A()
	'--------------------------------------------------------------------------------------------
	
Response.Write("" & vbCrLf)
Response.Write("<SCRIPT>" & vbCrLf)
Response.Write("//% Se verifica si el campo ha sido seleccionado" & vbCrLf)
Response.Write("//-------------------------------------------------------------------------------------------" & vbCrLf)
Response.Write("function insCheckSelClick(Field,lintIndex){" & vbCrLf)
Response.Write("//-------------------------------------------------------------------------------------------" & vbCrLf)
Response.Write("    var lstrParam=''" & vbCrLf)
Response.Write("    " & vbCrLf)
Response.Write("    if (!Field.checked){" & vbCrLf)
Response.Write("//+ cuando el campo se desmarca, se elimina registro" & vbCrLf)
Response.Write("		lstrParam = ""nModulec="" + marrArray[lintIndex].cbeModulec" & vbCrLf)
Response.Write("        EditRecord(lintIndex,nMainAction,""Del"",lstrParam)" & vbCrLf)
Response.Write("    }" & vbCrLf)
Response.Write("    else{" & vbCrLf)
Response.Write("//+ cuando el campo se marca, se agrega registro" & vbCrLf)
Response.Write("		lstrParam = ""sModulecDesc="" + marrArray[lintIndex].tctModulec " & vbCrLf)
Response.Write("				  + ""&nModulec="" + marrArray[lintIndex].cbeModulec" & vbCrLf)
Response.Write("        EditRecord(lintIndex,nMainAction,""Add"",lstrParam);" & vbCrLf)
Response.Write("    }" & vbCrLf)
Response.Write("    Field.checked = !Field.checked" & vbCrLf)
Response.Write("}" & vbCrLf)
Response.Write("</" & "SCRIPT>" & vbCrLf)
Response.Write("")

	
	Dim lclsTab_ActiveLife As eProduct.Tab_ActiveLife
	Dim lcolTab_ActiveLife As eProduct.Tab_ActiveLifes
	Dim lintIndex As Short
	
	lclsTab_ActiveLife = New eProduct.Tab_ActiveLife
	lcolTab_ActiveLife = New eProduct.Tab_ActiveLifes
	
	lintIndex = 0
	
	If lcolTab_ActiveLife.Find(mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate)) Then
		
		For	Each lclsTab_ActiveLife In lcolTab_ActiveLife
			With mobjGrid
				.Columns("cbeModulec").DefValue = CStr(lclsTab_ActiveLife.nModulec)
				.Columns("valModulec").DefValue = CStr(lclsTab_ActiveLife.nModulec)
				.Columns("tctModulec").DefValue = lclsTab_ActiveLife.sModulecDesc
				.Columns("tcnCapMin").DefValue = CStr(lclsTab_ActiveLife.nCapmin)
				.Columns("tcnMChanInves").DefValue = CStr(lclsTab_ActiveLife.nMchainves)
				.Columns("tcnErrRange").DefValue = CStr(lclsTab_ActiveLife.nErrrange)
				.Columns("Sel").Checked = lclsTab_ActiveLife.nExists
				.Columns("cbeOption").DefValue = CStr(lclsTab_ActiveLife.nOption)
				.Columns("hddOption").DefValue = CStr(lclsTab_ActiveLife.nOption)
				
				.Columns("Sel").OnClick = "insCheckSelClick(this," & CStr(lintIndex) & ")"
				Response.Write(.DoRow)
			End With
			lintIndex = lintIndex + 1
		Next lclsTab_ActiveLife
	End If
	
	Response.Write(mobjGrid.closeTable())
End Sub

'% insPreDP607AUpd: Se realiza el manejo de la ventana PopUp asociada al grid
'--------------------------------------------------------------------------------------------
Private Sub insPreDP607AUpd()
	'--------------------------------------------------------------------------------------------
	Dim mclsTab_ActiveLife As eProduct.Tab_ActiveLife
	
	mclsTab_ActiveLife = New eProduct.Tab_ActiveLife
	
	
	With Request
		If Request.QueryString.Item("Action") = "Del" Then
			Response.Write(mobjValues.ConfirmDelete())
			
                If mclsTab_ActiveLife.InsPostDP607A("Del", mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nModulec"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), 0, 0, mobjValues.StringToType(Session("nUserCode"), eFunctions.Values.eTypeData.etdDouble), 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0) Then
                    Response.Write("<SCRIPT>top.opener.top.frames['fraSequence'].document.location='/VTimeNet/Product/ProductSeq/ProdActLifeSeq/Sequence.aspx?nAction=" & Request.QueryString.Item("nMainAction") & "&nOpener=" & Request.QueryString.Item("sCodispl") & "&sGoToNext=NO" & "';</" & "Script>")
                End If
		End If
		
		Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "valProdActLifeSeq.aspx", "DP607A", .QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(.QueryString.Item("Index"))))
	End With
	
	mclsTab_ActiveLife = Nothing
	
End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues

mobjValues.ActionQuery = CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401

mobjValues.sCodisplPage = "dp607a"
%>
<HTML>
<HEAD>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>

<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>    
<%
Response.Write(mobjValues.StyleSheet())
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write(mobjMenu.setZone(2, "DP607A", "DP607A.aspx"))
	
	mobjMenu = Nothing
	Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
End If
%>
</HEAD>
<SCRIPT>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 17:06 $|$$Author: Nvaplat61 $"
</SCRIPT>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" NAME="DP607A" ACTION="valProdActLifeSeq.aspx?sMode=2">
<%Response.Write(mobjValues.ShowWindowsName("DP607A"))

Call insDefineHeader()

If Request.QueryString.Item("Type") = "PopUp" Then
	Call insPreDP607AUpd()
Else
	Call insPreDP607A()
End If
%>
</FORM> 
</BODY>
</HTML>





