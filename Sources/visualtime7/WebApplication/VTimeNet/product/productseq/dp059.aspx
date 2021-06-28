<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eBranches" %>
<script language="VB" runat="Server">
'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
Dim mobjGrid As eFunctions.Grid
Dim mobjMenu As eFunctions.Menues
Dim mstrTypeFind As String
Dim mblnVisible As Boolean
Dim mblnDisabled As Object


'% insDefineHeader: Se definen los campos del grid
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	'+ Se definen las columnas del grid
	
	With mobjGrid.Columns
		Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeHospitalColumnCaption"), "cbeHospital", "tabtab_provider", eFunctions.Values.eValuesType.clngWindowType,  , True,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeHospitalColumnToolTip"))
		Call .AddHiddenColumn("tcnAuxHopital", CStr(0))
		Call .AddHiddenColumn("tcdEffecdate_reg", CStr(0))
		Call .AddHiddenColumn("sAuxSel", CStr(2))
	End With
	
	'+ Se definen las propiedades generales del grid
	With mobjGrid
		.Codispl = "DP059"
		.Width = 400
		.Height = 150
		.Columns("cbeHospital").Parameters.Add("nBranch", mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Columns("cbeHospital").Parameters.Add("nTypeProv", 1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.bOnlyForQuery = Session("bQuery")
		.Columns("Sel").GridVisible = Not Session("bQuery")
		If request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = request.QueryString.Item("ReloadIndex")
		End If
		.Columns("Sel").OnClick = "if(document.forms[0].sAuxSel.length>0)document.forms[0].sAuxSel[this.value].value =(this.checked?1:2); else document.forms[0].sAuxSel.value =(this.checked?1:2);"
		.sDelRecordParam = "dEffecdate=" + mobjValues.TypeToString(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate) & "&nHospital=' + marrArray[lintIndex].cbeHospital  + '" & "&dEffecdate_reg='+ marrArray[lintIndex].tcdEffecdate_reg + '"
	End With
End Sub

'% insPreDP02: Se cargan los controles de la página
'--------------------------------------------------------------------------------------------
Private Sub insPreDP059()
	'--------------------------------------------------------------------------------------------
	Dim lclsTab_am_cli As eBranches.Tab_am_cli
	Dim lcolTab_am_clis As eBranches.Tab_am_clis
	Dim lintIndex As Object
	
	lclsTab_am_cli = New eBranches.Tab_am_cli
	lcolTab_am_clis = New eBranches.Tab_am_clis
	
	mobjGrid.AddButton = True
	If lcolTab_am_clis.Find(mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate)) Then
		For	Each lclsTab_am_cli In lcolTab_am_clis
			With mobjGrid
				.Columns("cbeHospital").DefValue = CStr(lclsTab_am_cli.nHospital)
				.Columns("tcnAuxHopital").DefValue = CStr(lclsTab_am_cli.nHospital)
				.Columns("tcdEffecdate_reg").DefValue = CStr(lclsTab_am_cli.dEffecdate_Reg)
				Response.Write(.DoRow)
			End With
		Next lclsTab_am_cli
	Else
		mblnVisible = True
	End If
	Response.Write(mobjGrid.closeTable())
	
	lclsTab_am_cli = Nothing
	lcolTab_am_clis = Nothing
End Sub

'% insPreDP059Upd: Se muetra la ventana Popup para efecto de actualización del Gird
'--------------------------------------------------------------------------------------------
Private Sub insPreDP059Upd()
	'--------------------------------------------------------------------------------------------
	Dim lclsTab_am_cli1 As eBranches.Tab_am_cli
	
	lclsTab_am_cli1 = New eBranches.Tab_am_cli
	
	If request.QueryString.Item("Action") = "Del" Then
		Response.Write(mobjValues.ConfirmDelete)
		Call lclsTab_am_cli1.insPostDP059("DP059", request.QueryString.Item("Action"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(request.QueryString.Item("nHospital"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(request.QueryString.Item("dEffecdate_reg"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
		Response.Write("<SCRIPT>top.opener.top.frames['fraSequence'].document.location='/VTimeNet/Product/ProductSeq/Sequence.aspx?nAction=" & request.QueryString.Item("nMainAction") & "&nOpener=" & request.QueryString.Item("sCodispl") & "&sGoToNext=NO" & "';</" & "Script>")
	End If
	With request
		Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "valProductSeq.aspx", "DP059", .QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(.QueryString.Item("Index"))))
	End With
	lclsTab_am_cli1 = Nothing
End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjGrid = New eFunctions.Grid
mobjMenu = New eFunctions.Menues

mobjGrid.sCodisplPage = "DP059"
mobjValues.sCodisplPage = "DP059"

If IsNothing(request.QueryString.Item("sTypeFind")) Then
	mstrTypeFind = "1"
Else
	mstrTypeFind = "2"
End If
mobjValues.ActionQuery = Session("bQuery")
%>
<HTML>
<HEAD>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/ValFunctions.js"></SCRIPT>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">



<%
With Response
	.Write(mobjValues.StyleSheet())
	If request.QueryString.Item("Type") <> "PopUp" Then
		.Write("<SCRIPT>var nMainAction=304</SCRIPT>")
		.Write(mobjMenu.setZone(2, "DP059", "DP059.aspx"))
		mobjMenu = Nothing
	End If
End With
%>
   <SCRIPT LANGUAGE="JavaScript">
//+ Variable para el control de versiones
       document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 17:02 $|$$Author: Nvaplat61 $"
   </SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="frmDP059" ACTION="valProductSeq.aspx?sMode=2">
<%
Response.Write(mobjValues.ShowWindowsName("DP059"))
Call insDefineHeader()
If request.QueryString.Item("Type") = "PopUp" Then
	Call insPreDP059Upd()
Else
	Call insPreDP059()
End If
%>
</FORM>
</BODY>
</HTML>
<%
mobjValues = Nothing
mobjGrid = Nothing
%>




