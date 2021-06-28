<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eGeneralForm" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 10.27.20
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility
'- Objeto para el manejo de las funciones generales de carga de valores.
Dim mobjValues As eFunctions.Values
Dim mobjGrid As eFunctions.Grid
Dim mobjMenu As eFunctions.Menues


'% insDefineHeader: Se definen los campos del grid.
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.27.20
	mobjGrid.sSessionID = Session.SessionID
	mobjGrid.nUsercode = Session("nUsercode")
	'~End Body Block VisualTimer Utility
	
	mobjGrid.sCodisplPage = "cac005"
	Call mobjGrid.SetWindowParameters(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), Request.QueryString.Item("nWindowTy"))
	'+ Se definen las columnas del grid
	With mobjGrid.Columns
		Call .AddAnimatedColumn(0, "", "imgValues", "/VTimeNet/Images/lupa.bmp")
		Call .AddTextColumn(40638, GetLocalResourceObject("tctStreetColumnCaption"), "tctStreet", 40, vbNullString,  , GetLocalResourceObject("tctStreetColumnCaption"))
		Call .AddNumericColumn(40637, GetLocalResourceObject("tcnZip_codeColumnCaption"), "tcnZip_code", 10, CStr(0))
		Call .AddTextColumn(40635, GetLocalResourceObject("tctBranchColumnCaption"), "tctBranch", 30, vbNullString,  , GetLocalResourceObject("tctBranchColumnCaption"))
		Call .AddNumericColumn(40636, GetLocalResourceObject("tcnPolicyColumnCaption"), "tcnPolicy", 15, CStr(0))
		Call .AddNumericColumn(40637, GetLocalResourceObject("tcnCertifColumnCaption"), "tcnCertif", 15, CStr(0))
		Call .AddTextColumn(40639, GetLocalResourceObject("tctCurrencyColumnCaption"), "tctCurrency", 30, vbNullString,  , GetLocalResourceObject("tctCurrencyColumnCaption"))
	End With
	'+ Se definen las propiedades generales del grid.
	With mobjGrid
		.Codispl = "CAC005"
		.DeleteButton = False
		.AddButton = False
		.Columns("Sel").GridVisible = False
		.Columns("imgValues").GridVisible = False
		.ActionQuery = True
	End With
End Sub

'% insPreCAC005: Se cargan los controles de la página.
'--------------------------------------------------------------------------------------------
Private Sub insPreCAC005()
	'--------------------------------------------------------------------------------------------
	Dim lintCount As Short
	Dim lcolAddress As eGeneralForm.Addresss
	Dim lclsAddress As Object
	
	lcolAddress = New eGeneralForm.Addresss
	If lcolAddress.FindCAC005(mobjValues.StringToType(Request.QueryString.Item("nProvince"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.QueryString.Item("nLocal"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.QueryString.Item("nMunicipality"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate), Request.QueryString.Item("sCondition")) Then
		
		lintCount = 0
		For	Each lclsAddress In lcolAddress
			With lclsAddress
				mobjGrid.Columns("tctStreet").DefValue = Replace(.sStreet, """", "'") & Replace(.sStreet1, """", "'")
				mobjGrid.Columns("tctBranch").DefValue = .sDescBranch
				mobjGrid.Columns("tcnPolicy").DefValue = .nPolicy
				mobjGrid.Columns("tcnCertif").DefValue = .nCertif
				mobjGrid.Columns("tctCurrency").DefValue = .sDescCurrency
				mobjGrid.Columns("tcnZip_code").DefValue = .nZip_code
				Response.Write(mobjGrid.DoRow())
			End With
			lintCount = lintCount + 1
			If lintCount = 200 Then
				Exit For
			End If
		Next lclsAddress
	End If
	Response.Write(mobjGrid.closeTable())
	Response.Write(mobjValues.BeginPageButton)
	lcolAddress = Nothing
	lclsAddress = Nothing
End Sub

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("cac005")
mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.27.20
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "cac005"
%>
<SCRIPT LANGUAGE="JavaScript">
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 16:37 $"

// insCancel : Cancelación de la acción
//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
	return true;
}
</SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>




<HTML>
<HEAD>
<META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">
<%mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.27.20
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility
Response.Write(mobjValues.StyleSheet())
Response.Write(mobjValues.ShowWindowsName("CAC005", Request.QueryString.Item("sWindowDescript")))
Response.Write(mobjMenu.setZone(2, "CAC005", Request.QueryString.Item("sWindowDescript"), CShort(Request.QueryString.Item("nWindowTy"))))
If Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionQuery) Then
	mobjValues.ActionQuery = True
End If
mobjMenu = Nothing
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="CAC005" ACTION="ValPolicyQue.aspx?Zone=2">
<%
Call insDefineHeader()
Call insPreCAC005()
mobjGrid = Nothing
mobjValues = Nothing
%>
</FORM>
</BODY>
</HTML>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 10.27.20
Call mobjNetFrameWork.FinishPage("cac005")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




