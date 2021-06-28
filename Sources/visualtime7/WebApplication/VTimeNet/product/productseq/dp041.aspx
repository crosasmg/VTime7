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
	mobjGrid = New eFunctions.Grid
	mobjGrid.ActionQuery = Session("bQuery")
	
	'+ Se definen las columnas del grid
	With mobjGrid.Columns
		.AddNumericColumn(41358, GetLocalResourceObject("tcnYearColumnCaption"), "tcnYear", 4, CStr(0),  , GetLocalResourceObject("tcnYearColumnToolTip"),  ,  ,  ,  ,  , Request.QueryString.Item("Action") = "Update")
		.AddNumericColumn(41359, GetLocalResourceObject("tcnClaimratColumnCaption"), "tcnClaimrat", 5, CStr(0),  , GetLocalResourceObject("tcnClaimratColumnToolTip"), True, 2,  ,  ,  , Request.QueryString.Item("Action") = "Update")
		.AddNumericColumn(41361, GetLocalResourceObject("tcnDiscountColumnCaption"), "tcnDiscount", 4, CStr(0),  , GetLocalResourceObject("tcnDiscountColumnToolTip"), True, 2)
	End With
	
	'+ Se definen las propiedades generales del grid
	With mobjGrid
		If Request.QueryString.Item("Action") = "Update" Then
			.Columns("Sel").GridVisible = False
		End If
		.Codispl = "DP041"
		.Width = 280
		.Height = 200
		.Columns("Sel").GridVisible = Not Session("bQuery")
		.bOnlyForQuery = Session("bQuery")
		.sDelRecordParam = "nYear=' + marrArray[lintIndex].tcnYear + '&nClaimrat=' + marrArray[lintIndex].tcnClaimrat + '&nDiscount=' + marrArray[lintIndex].tcnDiscount + '"
		.Columns("tcnYear").EditRecord = True
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
End Sub

'% insPreDP041: Se cargan los controles de la página, tanto de la parte fija como del grid
'--------------------------------------------------------------------------------------------
Private Sub insPreDP041()
	'--------------------------------------------------------------------------------------------
	Dim lclsTar_au_bon As eProduct.Tar_au_bon
	Dim lcolTar_au_bons As eProduct.Tar_au_bons
	
	lclsTar_au_bon = New eProduct.Tar_au_bon
	lcolTar_au_bons = New eProduct.Tar_au_bons
	
	If lcolTar_au_bons.Find(mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToDate(Session("dEffecdate"))) Then
		For	Each lclsTar_au_bon In lcolTar_au_bons
			With mobjGrid
				.Columns("tcnYear").DefValue = mobjValues.StringToType(CStr(lclsTar_au_bon.nYear), eFunctions.Values.eTypeData.etdDouble)
				.Columns("tcnClaimrat").DefValue = mobjValues.StringToType(CStr(lclsTar_au_bon.nClaimrat), eFunctions.Values.eTypeData.etdDouble)
				.Columns("tcnDiscount").DefValue = mobjValues.StringToType(CStr(lclsTar_au_bon.nDiscount), eFunctions.Values.eTypeData.etdDouble)
				Response.Write(.DoRow)
			End With
		Next lclsTar_au_bon
	End If
	Response.Write(mobjGrid.closeTable())
	Response.Write(mobjValues.HiddenControl("hddCountRecord", CStr(lcolTar_au_bons.Count)))
	
	lclsTar_au_bon = Nothing
	lcolTar_au_bons = Nothing
End Sub

'% insPreDP032Upd: Se muetra la ventana Popup para efecto de actualización del Gird
'--------------------------------------------------------------------------------------------
Private Sub insPreDP041Upd()
	'--------------------------------------------------------------------------------------------
	Dim lclsTar_au_bon As eProduct.Tar_au_bon
	lclsTar_au_bon = New eProduct.Tar_au_bon
	
	With Request
		If .QueryString.Item("Action") = "Del" Then
			Response.Write(mobjValues.ConfirmDelete)
			If lclsTar_au_bon.insPostDP041(.QueryString.Item("Action"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.QueryString.Item("nYear"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nClaimrat"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nDiscount"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble)) Then
				Response.Write("<SCRIPT>top.opener.top.frames['fraSequence'].document.location='/VTimeNet/Product/ProductSeq/Sequence.aspx?nAction=" & Request.QueryString.Item("nMainAction") & "&nOpener=" & Request.QueryString.Item("sCodispl") & "&sGoToNext=NO" & "';</" & "Script>")
			End If
		End If
		
		Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "valProductSeq.aspx", "DP041", .QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(.QueryString.Item("Index"))))
	End With
End Sub

</script>
<%Response.Expires = -1

With Server
	mobjValues = New eFunctions.Values
	mobjGrid = New eFunctions.Grid
	mobjMenu = New eFunctions.Menues
End With

mobjGrid.sCodisplPage = "DP041"
mobjValues.sCodisplPage = "DP041"

mobjValues.ActionQuery = Session("bQuery")
%>
<HTML>
<HEAD>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">




<SCRIPT>
//- Variable para el control de versiones
	document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 17:21 $|$$Author: Nvaplat61 $"
</SCRIPT>
<%
With Response
	.Write(mobjValues.StyleSheet())
	If Request.QueryString.Item("Type") <> "PopUp" Then
		.Write("<SCRIPT>var nMainAction=304</SCRIPT>")
		.Write(mobjMenu.setZone(2, "DP041", "DP041.aspx"))
		mobjMenu = Nothing
	End If
End With
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="frmDP041" ACTION="valProductSeq.aspx?nMainAction=<%=Request.QueryString.Item("nMainAction")%>">
<%Response.Write(mobjValues.ShowWindowsName("DP041"))
Call insDefineHeader()
If Request.QueryString.Item("Type") = "PopUp" Then
	Call insPreDP041Upd()
Else
	Call insPreDP041()
End If
mobjValues = Nothing
mobjGrid = Nothing
%>
</FORM>
</BODY>
</HTML>




