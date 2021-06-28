<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="ePolicy" %>
<script language="VB" runat="Server">

'**- Object for the handling of the general functions of load of values.    
'- Objeto para el manejo de las funciones generales de carga de valores.

Dim mobjValues As eFunctions.Values
Dim mobjGrid As eFunctions.Grid
Dim mobjMenu As eFunctions.Menues


'**% insDefineHeader: The columns del grid are defined.
'% insDefineHeader: Se definen las columnas del grid.
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	
	mobjGrid.sCodisplPage = "vic012"
	
	With mobjGrid.Columns
		.AddTextColumn(0, GetLocalResourceObject("tctBranchColumnCaption"), "tctBranch", 30, "",  , GetLocalResourceObject("tctBranchColumnToolTip"),  ,  ,  , True)
		.AddTextColumn(0, GetLocalResourceObject("tctProductColumnCaption"), "tctProduct", 30, "",  , GetLocalResourceObject("tctProductColumnToolTip"),  ,  ,  , True)
		.AddNumericColumn(0, GetLocalResourceObject("tcnPolicyColumnCaption"), "tcnPolicy", 10, "",  , GetLocalResourceObject("tcnPolicyColumnToolTip"),  ,  ,  ,  ,  , True)
		.AddNumericColumn(0, GetLocalResourceObject("tcnCertificateColumnCaption"), "tcnCertificate", 10, "",  , GetLocalResourceObject("tcnCertificateColumnToolTip"),  ,  ,  ,  ,  , True)
		.AddTextColumn(0, GetLocalResourceObject("tctClientColumnCaption"), "tctClient", 57, "",  , GetLocalResourceObject("tctClientColumnToolTip"),  ,  ,  , True)
		.AdddateColumn(0, GetLocalResourceObject("tcdEffecdateColumnCaption"), "tcdEffecdate",  ,  , GetLocalResourceObject("tcdEffecdateColumnToolTip"),  ,  ,  , True)
		.AddTextColumn(0, GetLocalResourceObject("tctEntryColumnCaption"), "tctEntry", 30, "",  , GetLocalResourceObject("tctEntryColumnToolTip"),  ,  ,  , True)
		.AddNumericColumn(0, GetLocalResourceObject("tcnUnitsColumnCaption"), "tcnUnits", 18, "",  , GetLocalResourceObject("tcnUnitsColumnToolTip"), True, 6,  ,  ,  , True)
		.AddNumericColumn(0, GetLocalResourceObject("tcnUnits_balanceColumnCaption"), "tcnUnits_balance", 18, "",  , GetLocalResourceObject("tcnUnits_balanceColumnToolTip"), True, 6,  ,  ,  , True)
		.AddTextColumn(0, GetLocalResourceObject("tctInstitutionColumnCaption"), "tctInstitution", 40, "",  , GetLocalResourceObject("tctInstitutionColumnToolTip"),  ,  ,  , True)
		.AddTextColumn(0, GetLocalResourceObject("tctOriginColumnCaption"), "tctOrigin", 30, "",  , GetLocalResourceObject("tctOriginColumnToolTip"),  ,  ,  , True)
		.AdddateColumn(0, GetLocalResourceObject("tcdDate_originColumnCaption"), "tcdDate_origin",  ,  , GetLocalResourceObject("tcdDate_originColumnToolTip"),  ,  ,  , True)
	End With
	
	'**+ The general properties of the grid are defined.
	'+ Se definen las propiedades generales del grid.
	
	With mobjGrid
		.Codispl = "VIC012"
		.DeleteButton = False
		.AddButton = False
		.Columns("Sel").GridVisible = False
		.ActionQuery = True
	End With
End Sub

'**% insPreVIC012: This function allows to show in the grid the read values.
'% insPreVIC012: Esta función permite mostrar en el grid los valores leídos.
'--------------------------------------------------------------------------------------------
Private Sub insPreVIC012()
	'--------------------------------------------------------------------------------------------
	Dim lintCount As Short
	Dim lclsFund_move As Object
	Dim lcolFund_moves As ePolicy.Fund_moves
	
	lcolFund_moves = New ePolicy.Fund_moves
	
	If lcolFund_moves.Find_units(Session("nFund"), Session("dDate")) Then
		lintCount = 0
		
		For	Each lclsFund_move In lcolFund_moves
			With lclsFund_move
				mobjGrid.Columns("tctBranch").DefValue = .sBranch
				mobjGrid.Columns("tctProduct").DefValue = .sProduct
				mobjGrid.Columns("tcnPolicy").DefValue = .nPolicy
				mobjGrid.Columns("tcnCertificate").DefValue = .nCertif
				mobjGrid.Columns("tctClient").DefValue = .sClient & " - " & .sCliename
				mobjGrid.Columns("tcdEffecdate").DefValue = .dOperdate
				mobjGrid.Columns("tctEntry").DefValue = .sEntry
				mobjGrid.Columns("tcnUnits").DefValue = .nUnits
				mobjGrid.Columns("tcnUnits_balance").DefValue = .nUnit_balance
				mobjGrid.Columns("tctInstitution").DefValue = .sInstitution
				mobjGrid.Columns("tctOrigin").DefValue = .sOrigin
				mobjGrid.Columns("tcdDate_origin").DefValue = .ddate_origin
				
				Response.Write(mobjGrid.DoRow())
			End With
			
			lintCount = lintCount + 1
			
			If lintCount = 200 Then
				Exit For
			End If
		Next lclsFund_move
	End If
	
	Response.Write(mobjGrid.closeTable())
	
	lcolFund_moves = Nothing
	lclsFund_move = Nothing
End Sub

</script>
<%
Response.Expires = 0

mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues

mobjValues.sCodisplPage = "vic012"
%>
<SCRIPT LANGUAGE="JavaScript">

//**+ Source Safe control of version
//+ Para Control de Versiones de Source Safe

    document.VssVersion="$$Revision: 1 $|$$Date: 2/09/03 19:01 $"

//**% insCancel: This function executes the cancel action of the page.
//% insCancel: Ejecuta la acción cancelar de la página.
//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
	return true;
}

</SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>




<HTML>
<HEAD>
    <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">
    <%
Response.Write(mobjValues.StyleSheet())
Response.Write(mobjMenu.setZone(2, "VIC012", "VIC012.aspx"))

If Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionQuery) Then
	mobjValues.ActionQuery = True
End If
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="VIC012" ACTION="ValBranchQue.aspx?Zone=2">
<%
Response.Write(mobjValues.ShowWindowsName("VIC012"))

Call insDefineHeader()
Call insPreVIC012()

mobjGrid = Nothing
mobjValues = Nothing
mobjMenu = Nothing
%>     
</FORM>
</BODY>
</HTML>




