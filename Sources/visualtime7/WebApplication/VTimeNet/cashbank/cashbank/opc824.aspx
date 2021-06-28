<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eCashBank" %>
<script language="VB" runat="Server">
'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
Dim mobjGrid As eFunctions.Grid
Dim mobjMenu As eFunctions.Menues
Dim mlngRow As Integer


'% insDefineHeader: Se definen los campos del grid
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	'+ Se definen las columnas del grid
	With mobjGrid.Splits_Renamed
		Call .AddSplit(0, GetLocalResourceObject("2ColumnCaption"), 2)
		Call .AddSplit(0, GetLocalResourceObject("BordereauxColumnCaption"), 2)
		Call .AddSplit(0, GetLocalResourceObject("1ColumnCaption"), 1)
		Call .AddSplit(0, GetLocalResourceObject("BranchColumnCaption"), 2)
		Call .AddSplit(0, GetLocalResourceObject("ProductColumnCaption"), 2)
	End With
	
	With mobjGrid.Columns
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnCashNumColumnCaption"), "tcnCashNum", 5,  ,  , GetLocalResourceObject("tcnCashNumColumnToolTip"))
		Call .AddDateColumn(0, GetLocalResourceObject("tcdCollectColumnCaption"), "tcdCollect",  ,  , GetLocalResourceObject("tcdCollectColumnToolTip"))
		Call .AddTextColumn(0, GetLocalResourceObject("tctDescRel_TypeColumnCaption"), "tctDescRel_Type", 12, "",  , GetLocalResourceObject("tctDescRel_TypeColumnToolTip"))
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnBordereauxColumnCaption"), "tcnBordereaux", 10,  ,  , GetLocalResourceObject("tcnBordereauxColumnToolTip"))
		Call .AddTextColumn(0, GetLocalResourceObject("tctClientColumnCaption"), "tctClient", 16, "",  , GetLocalResourceObject("tctClientColumnToolTip"))
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnBranchColumnCaption"), "tcnBranch", 5,  ,  , GetLocalResourceObject("tcnBranchColumnToolTip"))
		Call .AddTextColumn(0, GetLocalResourceObject("tctDesc_BranchColumnCaption"), "tctDesc_Branch", 12, "",  , GetLocalResourceObject("tctDesc_BranchColumnToolTip"))
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnProductColumnCaption"), "tcnProduct", 5,  ,  , GetLocalResourceObject("tcnProductColumnToolTip"))
		Call .AddTextColumn(0, GetLocalResourceObject("tctDesc_ProductColumnCaption"), "tctDesc_Product", 12, "",  , GetLocalResourceObject("tctDesc_ProductColumnToolTip"))
		Call .AddTextColumn(0, GetLocalResourceObject("tctPolicyColumnCaption"), "tctPolicy", 10, "",  , GetLocalResourceObject("tctPolicyColumnToolTip"))
		Call .AddTextColumn(0, GetLocalResourceObject("tctProponumColumnCaption"), "tctProponum", 10, "",  , GetLocalResourceObject("tctProponumColumnToolTip"))
		Call .AddTextColumn(0, GetLocalResourceObject("tctStatusColumnCaption"), "tctStatus", 30, "",  , GetLocalResourceObject("tctStatusColumnToolTip"))
		Call .AddTextColumn(0, GetLocalResourceObject("tctReceiptColumnCaption"), "tctReceipt", 16, "",  , GetLocalResourceObject("tctReceiptColumnToolTip"))
		Call .AddTextColumn(0, GetLocalResourceObject("tctBulletinsColumnCaption"), "tctBulletins", 10, "",  , GetLocalResourceObject("tctBulletinsColumnToolTip"))
		Call .AddDateColumn(0, GetLocalResourceObject("tcdValue_dateColumnCaption"), "tcdValue_date",  ,  , GetLocalResourceObject("tcdValue_dateColumnToolTip"))
	End With
	
	'+ Se definen las propiedades generales del grid
	With mobjGrid
		.Codispl = "OPC824"
		.Codisp = "OPC824"
		.DeleteButton = False
		.AddButton = False
		.Columns("Sel").GridVisible = False
	End With
End Sub

'% insPreOPC824: Se cargan los controles de la página
'--------------------------------------------------------------------------------------------
Private Sub insPreOPC824()
	'--------------------------------------------------------------------------------------------
	Dim lclsUser_cashnum As eCashBank.User_cashnum
	Dim lcolUser_cashnums As eCashBank.User_cashnums
	Dim lIndex As Integer
	
	lcolUser_cashnums = New eCashBank.User_cashnums
	lclsUser_cashnum = New eCashBank.User_cashnum
	
	If mobjValues.StringToType(Request.QueryString.Item("nRow"), eFunctions.Values.eTypeData.etdDouble) = eRemoteDB.Constants.intNull Then
		mlngRow = 1
	Else
		mlngRow = mobjValues.StringToType(Request.QueryString.Item("nRow"), eFunctions.Values.eTypeData.etdDouble)
	End If
	If lcolUser_cashnums.Find_OPC824(mobjValues.StringToDate(Request.QueryString.Item("dCollect")), mobjValues.StringToType(Request.QueryString.Item("nCashnum"), eFunctions.Values.eTypeData.etdDouble), Request.QueryString.Item("sStatus"), mlngRow) Then
		For lIndex = 1 To lcolUser_cashnums.Count
			lclsUser_cashnum = lcolUser_cashnums.Item(lIndex)
			With lclsUser_cashnum
				mobjGrid.Columns("tcnCashNum").DefValue = CStr(.nCashNum)
				mobjGrid.Columns("tcdCollect").DefValue = CStr(.dCollect)
				mobjGrid.Columns("tctDescRel_Type").DefValue = .sDesc_Reltype
				mobjGrid.Columns("tcnBordereaux").DefValue = CStr(.nBordereaux)
				If .sClient <> "" Then
					mobjGrid.Columns("tctClient").DefValue = .sClient & "-" & Trim(.sDigit)
				Else
					mobjGrid.Columns("tctClient").DefValue = ""
				End If
				mobjGrid.Columns("tcnBranch").DefValue = CStr(.nBranch)
				mobjGrid.Columns("tctDesc_Branch").DefValue = .sDesc_Branch
				mobjGrid.Columns("tcnProduct").DefValue = CStr(.nProduct)
				mobjGrid.Columns("tctDesc_Product").DefValue = .sDesc_Product
				If .nPolicy < 0 Then
					mobjGrid.Columns("tctPolicy").DefValue = ""
				Else
					mobjGrid.Columns("tctPolicy").DefValue = mobjValues.TypeToString(.nPolicy, eFunctions.Values.eTypeData.etdDouble)
				End If
				mobjGrid.Columns("tctProponum").DefValue = mobjValues.TypeToString(.nProponum, eFunctions.Values.eTypeData.etdDouble)
				mobjGrid.Columns("tctStatus").DefValue = .sStatus
				If .nDraft > 0 Then
					mobjGrid.Columns("tctReceipt").DefValue = mobjValues.TypeToString(.nDraft, eFunctions.Values.eTypeData.etdDouble) & " / " & mobjValues.TypeToString(.nReceipt, eFunctions.Values.eTypeData.etdDouble)
				Else
					mobjGrid.Columns("tctReceipt").DefValue = mobjValues.TypeToString(.nReceipt, eFunctions.Values.eTypeData.etdDouble)
				End If
				mobjGrid.Columns("tctBulletins").DefValue = mobjValues.TypeToString(.nBulletins, eFunctions.Values.eTypeData.etdDouble)
				mobjGrid.Columns("tcdValue_date").DefValue = CStr(.dValueDate)
				Response.Write(mobjGrid.DoRow())
			End With
		Next 
	End If
	Response.Write(mobjGrid.closeTable())
	lcolUser_cashnums = Nothing
	lclsUser_cashnum = Nothing
End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjGrid = New eFunctions.Grid
mobjMenu = New eFunctions.Menues

%>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">


<SCRIPT>
//%Variable para el control de Versiones
    document.VssVersion="$$Revision: 1 $|$$Date: 11/02/04 17:25 $"
</SCRIPT>        
<SCRIPT>
//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
	return true;
}
//------------------------------------------------------------------------------------------
function  insStateZone(){
//------------------------------------------------------------------------------------------
}
//% ControlNextBack: Se encarga de amumentar o disminuir la consulta de los registros
//-------------------------------------------------------------------------------------------
function ControlNextBack(Option){
//-------------------------------------------------------------------------------------------
    var lstrURL = self.document.location.href
    var llngRow = lstrURL.substr(lstrURL.indexOf("&nRow=") + 6)
    lstrURL = lstrURL.replace(/&nRow=.*/,'')
	switch(Option){
		case "Next":
			if(isNaN(llngRow))
				lstrURL = lstrURL + "&nRow=51"
			else{
				llngRow = insConvertNumber(llngRow) + 50;
				lstrURL = lstrURL + "&nRow=" + llngRow
			}
			break;

		case "Back":
			if(!isNaN(llngRow)){
				llngRow = insConvertNumber(llngRow) - 50;
				lstrURL = lstrURL + "&nRow=" + llngRow
			}
	}
	self.document.location.href = lstrURL;
}	
</SCRIPT>
<%
With Response
	.Write(mobjValues.StyleSheet() & vbCrLf)
	.Write(mobjValues.WindowsTitle("OPC001"))
	.Write(mobjMenu.setZone(2, "OPC824", "OPC824.aspx"))
End With
mobjMenu = Nothing
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="fraContent" ACTION="ValCashBank.aspx?Zone=1">
<%Response.Write(mobjValues.ShowWindowsName("OPC824"))%>
</FORM>
</BODY>
</HTML>

<%
Call insDefineHeader()
Call insPreOPC824()
Response.Write(mobjValues.AnimatedButtonControl("cmdBack", "/VTimeNet/Images/btnLargeBackOff.png", GetLocalResourceObject("cmdBackToolTip"),  , "ControlNextBack('Back')", CDbl(Request.QueryString.Item("nRow")) <= 1 Or IsNothing(Request.QueryString.Item("nRow"))))
Response.Write(mobjValues.AnimatedButtonControl("cmdNext", "/VTimeNet/Images/btnLargeNextOff.png", GetLocalResourceObject("cmdNextToolTip"),  , "ControlNextBack('Next')"))
mobjValues = Nothing
mobjGrid = Nothing
%>







