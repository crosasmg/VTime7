<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eCashBank" %>
<%@ Import namespace="eAgent" %>
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
	
	mobjGrid.sCodisplPage = "opc013"
	
	'+ Se definen las columnas del grid  
	
	With mobjGrid.Columns
		Call .AddDateColumn(40180, GetLocalResourceObject("tcdOperdateColumnCaption"), "tcdOperdate", CStr(eRemoteDB.Constants.dtmNull),  , GetLocalResourceObject("tcdOperdateColumnToolTip"))
		Call .AddTextColumn(40178, GetLocalResourceObject("tctDescriptColumnCaption"), "tctDescript", 30, CStr(eRemoteDB.Constants.strNull),  , GetLocalResourceObject("tctDescriptColumnToolTip"))
		Call .AddPossiblesColumn(40173, GetLocalResourceObject("tctind_credebColumnCaption"), "tctind_credeb", "Table287", eFunctions.Values.eValuesType.clngComboType, CStr(eRemoteDB.Constants.intNull),  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("tctind_credebColumnToolTip"))
		Call .AddPossiblesColumn(40174, GetLocalResourceObject("tcnType_MoveColumnCaption"), "tcnType_Move", "Table401", eFunctions.Values.eValuesType.clngComboType, CStr(eRemoteDB.Constants.intNull),  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("tcnType_MoveColumnToolTip"))
		Call .AddNumericColumn(40175, GetLocalResourceObject("tcnamountColumnCaption"), "tcnamount", 30, CStr(eRemoteDB.Constants.intNull),  , GetLocalResourceObject("tcnamountColumnToolTip"), True, 6)
		Call .AddPossiblesColumn(0, GetLocalResourceObject("tctnBranchColumnCaption"), "tctnBranch", "Table10", eFunctions.Values.eValuesType.clngComboType, CStr(eRemoteDB.Constants.intNull),  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("tctnBranchColumnToolTip"))
		Call .AddTextColumn(40179, GetLocalResourceObject("tctproductdesColumnCaption"), "tctproductdes", 30, CStr(eRemoteDB.Constants.strNull),  , GetLocalResourceObject("tctproductdesColumnToolTip"))
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnPolicyColumnCaption"), "tcnPolicy", 30, CStr(eRemoteDB.Constants.intNull),  , GetLocalResourceObject("tcnPolicyColumnToolTip"))
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnCertifColumnCaption"), "tcnCertif", 30, CStr(eRemoteDB.Constants.intNull),  , GetLocalResourceObject("tcnCertifColumnToolTip"))
		Call .AddNumericColumn(40177, GetLocalResourceObject("tcnReceiptColumnCaption"), "tcnReceipt", 30, CStr(eRemoteDB.Constants.intNull),  , GetLocalResourceObject("tcnReceiptColumnToolTip"))
	End With
	
	'+ Se definen las propiedades generales del grid
	
	With mobjGrid
		.Codispl = "OPC013_k"
		.DeleteButton = False
		.AddButton = False
		.Columns("Sel").GridVisible = False
	End With
End Sub


'% insPreOPC013: Se cargan los controles de la página
'--------------------------------------------------------------------------------------------
Private Sub insPreOPC013()
	'--------------------------------------------------------------------------------------------
	Dim lclsMove_acc As Object
	Dim lcolMove_accs As eCashBank.Move_accs
	Dim lclsAgent As eAgent.Agents
	Dim lstrClient As String
	
	lcolMove_accs = New eCashBank.Move_accs
	lclsAgent = New eAgent.Agents
	
	Response.Write(mobjValues.ShowWindowsName("OPC013"))
	
	'+ Se ejecuta la condicion de busqueda para cargar el grid
	
	If lclsAgent.Find(mobjValues.StringToType(Request.QueryString.Item("nIntermed"), eFunctions.Values.eTypeData.etdDouble)) Then
		lstrClient = lclsAgent.sClient
	Else
		lstrClient = Request.QueryString.Item("sClient")
	End If
	
	If lcolMove_accs.Find_OPC013(CInt(Request.QueryString.Item("sTypeAcco")), "0", lstrClient, CInt(Request.QueryString.Item("nCurrency")), CDate(Request.QueryString.Item("dEffecdate"))) Then
		
		For	Each lclsMove_acc In lcolMove_accs
			With lclsMove_acc
				mobjGrid.Columns("tcdOperdate").DefValue = lclsMove_acc.dOperdate
				mobjGrid.Columns("tctDescript").DefValue = lclsMove_acc.sDescript
				If lclsMove_acc.namount < 0 Then
					mobjGrid.Columns("tctind_credeb").DefValue = "1"
				Else
					mobjGrid.Columns("tctind_credeb").DefValue = "2"
				End If
				mobjGrid.Columns("tcnType_Move").DefValue = lclsMove_acc.nType_Move
				mobjGrid.Columns("tcnamount").DefValue = lclsMove_acc.namount
				mobjGrid.Columns("tctnBranch").DefValue = lclsMove_acc.nbranch
				mobjGrid.Columns("tctproductdes").DefValue = lclsMove_acc.sproductdes
				mobjGrid.Columns("tcnPolicy").DefValue = lclsMove_acc.nPolicy
				mobjGrid.Columns("tcnCertif").DefValue = lclsMove_acc.nCertif
				mobjGrid.Columns("tcnReceipt").DefValue = lclsMove_acc.nReceipt
				Response.Write(mobjGrid.DoRow())
			End With
		Next lclsMove_acc
	End If
	
	Response.Write(mobjGrid.closeTable())
	Response.Write(mobjValues.BeginPageButton)
	lcolMove_accs = Nothing
	lclsAgent = Nothing
End Sub

</script>
<%Response.Expires = 0

mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues

mobjValues.sCodisplPage = "opc013"
%>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">


    <%With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjMenu.setZone(2, "OPC010", "OPC010.aspx"))
End With
mobjMenu = Nothing%>    
	  
<SCRIPT>

 //+Variable para el control de versiones
    document.VssVersion="$$Revision: 2 $|$$Date: 19/03/04 13:36 $|$$Author: Nvaplat53 $"
    
</SCRIPT>	  
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="fraContent" ACTION="valCashBank.aspx?sMode=2">

<%
Call insDefineHeader()
Call insPreOPC013()
%>
</FORM>
</BODY>
</HTML>






