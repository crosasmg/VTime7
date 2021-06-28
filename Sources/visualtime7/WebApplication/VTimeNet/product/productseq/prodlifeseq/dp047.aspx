<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eProduct" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
Dim mobjGrid As eFunctions.Grid
Dim mobjMenu As eFunctions.Menues


'% insDefineHeader: Se definen los campos del grid
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	'+ Se definen las columnas del grid
	With mobjGrid.Columns
		Call .AddNumericColumn(100198, GetLocalResourceObject("tcnDaynuminColumnCaption"), "tcnDaynumin", 2, CStr(0),  , GetLocalResourceObject("tcnDaynuminColumnToolTip"))
		Call .AddNumericColumn(100199, GetLocalResourceObject("tcnDaynumenColumnCaption"), "tcnDaynumen", 2, CStr(0),  , GetLocalResourceObject("tcnDaynumenColumnToolTip"))
		Call .AddPossiblesColumn(100197, GetLocalResourceObject("cbeValuestyColumnCaption"), "cbeValuesty", "Table125", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeValuestyColumnToolTip"))
		Call .AddNumericColumn(100200, GetLocalResourceObject("tcnDayaddColumnCaption"), "tcnDayadd", 2, CStr(0),  , GetLocalResourceObject("tcnDayaddColumnToolTip"))
		Call .AddPossiblesColumn(100197, GetLocalResourceObject("cbeValuesmoColumnCaption"), "cbeValuesmo", "Table126", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeValuesmoColumnToolTip"))
	End With
	
	'+ Se definen las propiedades generales del grid
	With mobjGrid
		.Codispl = "DP047"
		.bOnlyForQuery = Session("bQuery")
		.ActionQuery = mobjValues.ActionQuery
		.Height = 250
		.Columns("cbeValuesty").EditRecord = True
		.Columns("cbeValuesty").BlankPosition = False
		.Columns("tcnDaynumin").Disabled = Request.QueryString.Item("Action") = "Update"
		.Columns("tcnDaynumen").Disabled = Request.QueryString.Item("Action") = "Update"
		Call .Splits_Renamed.AddSplit(0, GetLocalResourceObject("2ColumnCaption"), 2)
		Call .Splits_Renamed.AddSplit(0, vbNullString, 1)
		Call .Splits_Renamed.AddSplit(0, GetLocalResourceObject("2ColumnCaption"), 2)
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
End Sub

'% insPreDP047: Se cargan los controles de la página
'--------------------------------------------------------------------------------------------
Private Sub insPreDP047()
	'--------------------------------------------------------------------------------------------
	Dim lclsEffect_dat As eProduct.Effect_dat
	Dim lcolEffect_dats As eProduct.Effect_dats
	
	lclsEffect_dat = New eProduct.Effect_dat
	lcolEffect_dats = New eProduct.Effect_dats
	
	If lcolEffect_dats.Find(Session("nBranch"), Session("nProduct"), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate)) Then
		For	Each lclsEffect_dat In lcolEffect_dats
			With mobjGrid
				.Columns("tcnDaynumin").DefValue = CStr(lclsEffect_dat.nDaynumin)
				.Columns("tcnDaynumen").DefValue = CStr(lclsEffect_dat.nDaynumen)
				.Columns("cbeValuesty").DefValue = CStr(lclsEffect_dat.nValuesty)
				.Columns("tcnDayadd").DefValue = CStr(lclsEffect_dat.nDayadd)
				.Columns("cbeValuesmo").DefValue = CStr(lclsEffect_dat.nValuesmo)
				.sDelRecordParam = "nDaynumin=' + marrArray[lintIndex].tcnDaynumin + '&nDaynumen=' + marrArray[lintIndex].tcnDaynumen + '&nValuesty=' + marrArray[lintIndex].cbeValuesty + '&nDayadd=' + marrArray[lintIndex].tcnDayadd + '&nValuesmo=' + marrArray[lintIndex].cbeValuesmo + '"
				Response.Write(.DoRow)
			End With
		Next lclsEffect_dat
	End If
	Response.Write(mobjGrid.closeTable())
	lclsEffect_dat = Nothing
	lcolEffect_dats = Nothing
End Sub

'% insPreDP047Upd: Se muetra la ventana Popup para efecto de actualización del Gird
'--------------------------------------------------------------------------------------------
Private Sub insPreDP047Upd()
	'--------------------------------------------------------------------------------------------
	Dim lclsEffect_dat As eProduct.Effect_dat
	
	With Request
		If .QueryString.Item("Action") = "Del" Then
			Response.Write(mobjValues.ConfirmDelete())
			lclsEffect_dat = New eProduct.Effect_dat
			If lclsEffect_dat.insPostDP047(.QueryString.Item("sCodispl"), .QueryString.Item("Action"), Session("nBranch"), Session("nProduct"), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), CInt(.QueryString.Item("nValuesty")), CInt(.QueryString.Item("nValuesmo")), CInt(.QueryString.Item("nDaynumin")), CInt(.QueryString.Item("nDaynumen")), CInt(.QueryString.Item("nDayadd")), Session("nUsercode")) Then
				Response.Write("<SCRIPT>top.opener.top.frames['fraSequence'].document.location='/VTimeNet/Product/ProductSeq/ProdLifeSeq/Sequence.aspx?nAction=" & Request.QueryString.Item("nMainAction") & "&nOpener=" & Request.QueryString.Item("sCodispl") & "&sGoToNext=NO" & "';</" & "Script>")
			End If
		End If
		Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "valProdLifeSeq.aspx", Request.QueryString.Item("sCodispl"), .QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(.QueryString.Item("Index"))))
	End With
	lclsEffect_dat = Nothing
End Sub

</script>
<%Response.Expires = -1

With Server
	mobjValues = New eFunctions.Values
	mobjGrid = New eFunctions.Grid
	mobjMenu = New eFunctions.Menues
End With
mobjValues.ActionQuery = Session("bQuery")

mobjValues.sCodisplPage = "dp047"
mobjGrid.sCodisplPage = "dp047"
%>
<HTML>
<HEAD>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">




<%
With Response
	.Write(mobjValues.StyleSheet())
	If Request.QueryString.Item("Type") <> "PopUp" Then
		.Write("<SCRIPT>var nMainAction=304</SCRIPT>")
		.Write(mobjMenu.setZone(2, Request.QueryString.Item("sCodispl"), "DP047.aspx"))
	End If
End With
%>
<SCRIPT>
//- Variable para el control de versiones
	document.VssVersion="$$Revision: 1 $|$$Date: 2/09/03 19:04 $|$$Author: Iusr_llanquihue $"
</SCRIPT>    
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="frmDP047" ACTION="valProdLifeSeq.aspx?sMode=2">
<%
Response.Write(mobjValues.ShowWindowsName("DP047"))
Call insDefineHeader()
If Request.QueryString.Item("Type") <> "PopUp" Then
	Call insPreDP047()
Else
	Call insPreDP047Upd()
End If
mobjValues = Nothing
mobjGrid = Nothing
mobjMenu = Nothing
%>
</FORM>
</HTML>




