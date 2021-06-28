<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eProduct" %>
<script language="VB" runat="Server">

Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues
Dim mobjGrid As eFunctions.Grid


'% insDefineHeader: Se definen los campos del grid.
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	'+ Se definen las columnas del grid.
	
	With mobjGrid.Columns
		Call .AddPossiblesColumn(0, GetLocalResourceObject("tcnlimit_typeColumnCaption"), "tcnlimit_type", "Table5647", eFunctions.Values.eValuesType.clngComboType, "",  ,  ,  ,  ,  ,  , 5, GetLocalResourceObject("tcnlimit_typeColumnCaption"))
		Call .AddPossiblesColumn(0, GetLocalResourceObject("tcnlimit_codeColumnCaption"), "tcnlimit_code", "Table5639", eFunctions.Values.eValuesType.clngComboType, "",  ,  ,  ,  ,  ,  , 5, GetLocalResourceObject("tcnlimit_codeColumnToolTip"))
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnvalminColumnCaption"), "tcnvalmin", 18, CStr(0),  , GetLocalResourceObject("tcnvalminColumnToolTip"),  , 6)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnvalmaxColumnCaption"), "tcnvalmax", 18, CStr(0),  , GetLocalResourceObject("tcnvalmaxColumnToolTip"),  , 6)
	End With
	'+ Se definen las propiedades generales del grid.
	With mobjGrid
		.AddButton = True
		.DeleteButton = True
		.Height = 250
		.Width = 450
		.Codispl = "DP066"
		'.nMainAction = Request.QueryString("nMainAction")
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
		.Columns("Sel").GridVisible = Not Session("bQuery")
		.sDelRecordParam = "nlimit_typ=' + marrArray[lintIndex].tcnlimit_type  + '" & "&nlimit_code=' + marrArray[lintIndex].tcnlimit_code  + '" & "&nvalmin=' + marrArray[lintIndex].tcnvalmin  + '" & "&nvalmax='+ marrArray[lintIndex].tcnvalmax + '"
	End With
End Sub
'% insPreDP066: Se cargan los controles de la página.
'--------------------------------------------------------------------------------------------
Private Sub insPreDP066()
	'--------------------------------------------------------------------------------------------
	Dim lblnDataFound As Object
	Dim lindexnModule As Object
	Dim lindexnCover As Object
	
	Dim lclsProduct_limits As Object
	Dim lcolProduct_limits As eProduct.Product_limitss
	
	lcolProduct_limits = New eProduct.Product_limitss
	
	If lcolProduct_limits.Find(mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate)) Then
		
		For	Each lclsProduct_limits In lcolProduct_limits
			With mobjGrid
				.Columns("tcnlimit_type").DefValue = lclsProduct_limits.nlimit_type
				.Columns("tcnlimit_code").DefValue = lclsProduct_limits.nlimit_code
				.Columns("tcnvalmin").DefValue = lclsProduct_limits.nvalmin
				.Columns("tcnvalmax").DefValue = lclsProduct_limits.nvalmax
				Response.Write(.DoRow)
			End With
		Next lclsProduct_limits
	End If
	Response.Write(mobjGrid.closeTable)
	lcolProduct_limits = Nothing
	lclsProduct_limits = Nothing
End Sub

'% insPreDP017Upd: Permite realizar el llamado a la ventana PopUp, cuando se está eliminando
'% un registro. 
'-----------------------------------------------------------------------------------------
Private Sub insPreDP066Upd()
	'-----------------------------------------------------------------------------------------
	Dim lclsProduct_limits As eProduct.Product_limits
	lclsProduct_limits = New eProduct.Product_limits
	If Request.QueryString.Item("Action") = "Del" Then
		Response.Write(mobjValues.ConfirmDelete())
		If lclsProduct_limits.InsPostDP066("Del", mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(Request.QueryString.Item("nlimit_typ"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(Request.QueryString.Item("nlimit_code"), eFunctions.Values.eTypeData.etdLong), 0, 0, mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), 0) Then
			Response.Write("<SCRIPT>top.opener.top.frames['fraSequence'].document.location='/VTimeNet/Product/ProductSeq/Sequence.aspx?nAction=" & Request.QueryString.Item("nMainAction") & "&nOpener=" & Request.QueryString.Item("sCodispl") & "&sGoToNext=NO" & "';</" & "Script>")
		End If
	End If
	Response.Write(mobjGrid.DoFormUpd(Request.QueryString.Item("Action"), "ValProductSeq.aspx", "DP066", Request.QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(Request.QueryString.Item("Index"))))
	lclsProduct_limits = Nothing
End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjValues.sCodisplPage = "DP066"

mobjMenu = New eFunctions.Menues

mobjGrid = New eFunctions.Grid
mobjGrid.sCodisplPage = "DP066"

mobjGrid.ActionQuery = Session("bQuery")
mobjValues.ActionQuery = Session("bQuery")
%>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>


<%
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write(mobjMenu.setZone(2, "DP066", "DP066.aspx"))
	mobjMenu = Nothing
	Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
End If
Response.Write(mobjValues.StyleSheet())
%>
<SCRIPT>
//- Variable para el control de versiones
       document.VssVersion="$$Revision: 2 $|$$Date: 29/06/06 5:41p $"
</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="DP066" ACTION="valProductSeq.aspx?nMainAction=<%=Request.QueryString.Item("nMainAction")%>">
<%
Response.Write(mobjValues.ShowWindowsName("DP066"))
Call insDefineHeader()
If Request.QueryString.Item("Type") = "PopUp" Then
	Call insPreDP066Upd()
Else
	Call insPreDP066()
End If
mobjValues = Nothing
%>
</FORM>
</BODY>
</HTML>





