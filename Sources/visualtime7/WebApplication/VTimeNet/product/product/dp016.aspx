<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eProduct" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores.
Dim mobjValues As eFunctions.Values
Dim mobjGrid As eFunctions.Grid
Dim mobjMenu As eFunctions.Menues


'% insDefineHeader:Permite definir las columnas del grid, así como habilitar o inhabilitar el 
'% botón de eliminar y registrar.
'-----------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'-----------------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	
	mobjGrid.sCodisplPage = "dp016"
	
	'+ Se definen las columnas del Grid.
	With mobjGrid
		.Codispl = Request.QueryString.Item("sCodispl")
		.Codisp = "DP016"
	End With
	
	With mobjGrid.Columns
		Call .AddNumericColumn(100337, GetLocalResourceObject("tcnAgeColumnCaption"), "tcnAge", 5, vbNullString, False, GetLocalResourceObject("tcnAgeColumnToolTip"))
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnMonthColumnCaption"), "tcnMonth", 5, vbNullString, False, GetLocalResourceObject("tcnMonthColumnToolTip"))
		Call .AddNumericColumn(100337, GetLocalResourceObject("tcnLive_lxColumnCaption"), "tcnLive_lx", 16, vbNullString, False, GetLocalResourceObject("tcnLive_lxColumnToolTip"),  , 4)
		Call .AddNumericColumn(100338, GetLocalResourceObject("tcnDeath_dxColumnCaption"), "tcnDeath_dx", 12, vbNullString, False, GetLocalResourceObject("tcnDeath_dxColumnToolTip"),  , 0)
		Call .AddNumericColumn(100339, GetLocalResourceObject("tcnDeath_qxColumnCaption"), "tcnDeath_qx", 9, vbNullString, False, GetLocalResourceObject("tcnDeath_qxColumnToolTip"),  , 8)
		Call .AddNumericColumn(100340, GetLocalResourceObject("tcnLiver_pxColumnCaption"), "tcnLiver_px", 9, vbNullString, False, GetLocalResourceObject("tcnLiver_pxColumnToolTip"),  , 8)
		Call .AddNumericColumn(100341, GetLocalResourceObject("tcnConmu_dxColumnCaption"), "tcnConmu_dx", 20, vbNullString, False, GetLocalResourceObject("tcnConmu_dxColumnToolTip"),  , 5)
		Call .AddNumericColumn(100342, GetLocalResourceObject("tcnConmu_cxColumnCaption"), "tcnConmu_cx", 20, vbNullString, False, GetLocalResourceObject("tcnConmu_cxColumnToolTip"),  , 5)
		Call .AddNumericColumn(100343, GetLocalResourceObject("tcnConmu_nxColumnCaption"), "tcnConmu_nx", 20, vbNullString, False, GetLocalResourceObject("tcnConmu_nxColumnToolTip"),  , 5)
		Call .AddNumericColumn(100344, GetLocalResourceObject("tcnConmu_mxColumnCaption"), "tcnConmu_mx", 20, vbNullString, False, GetLocalResourceObject("tcnConmu_mxColumnToolTip"),  , 5)
		Call .AddNumericColumn(100345, GetLocalResourceObject("tcnConmu_sxColumnCaption"), "tcnConmu_sx", 20, vbNullString, False, GetLocalResourceObject("tcnConmu_sxColumnToolTip"),  , 5)
		Call .AddNumericColumn(100346, GetLocalResourceObject("tcnConmu_rxColumnCaption"), "tcnConmu_rx", 20, vbNullString, False, GetLocalResourceObject("tcnConmu_rxColumnToolTip"),  , 5)
		Call .AddNumericColumn(100347, GetLocalResourceObject("tcnConmu_txColumnCaption"), "tcnConmu_tx", 20, vbNullString, False, GetLocalResourceObject("tcnConmu_txColumnToolTip"),  , 5)
		Call .AddNumericColumn(100347, GetLocalResourceObject("tcnConmu_vxColumnCaption"), "tcnConmu_vx", 20, vbNullString, False, GetLocalResourceObject("tcnConmu_vxColumnToolTip"),  , 5)
		Call .AddNumericColumn(100347, GetLocalResourceObject("tcnConmu_exColumnCaption"), "tcnConmu_ex", 20, vbNullString, False, GetLocalResourceObject("tcnConmu_exColumnToolTip"),  , 5)
	End With
	
	With mobjGrid
		.Columns("tcnAge").Disabled = (Request.QueryString.Item("Action") = "Update")
		.Columns("tcnMonth").Disabled = (Request.QueryString.Item("Action") = "Update")
		.Columns("tcnLive_lx").Disabled = (Request.QueryString.Item("Action") = "Update")
		.Columns("tcnDeath_qx").Disabled = (Request.QueryString.Item("Action") = "Update")
		
		If Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionQuery) Or Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionUpdate) Or Request.QueryString.Item("nMainAction") = vbNullString Then
			.Columns("Sel").GridVisible = False
			
			If Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionQuery) Then
				.ActionQuery = True
			End If
		End If
		
		If Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionUpdate) Then
			.Columns("tcnDeath_dx").EditRecord = True
			.AddButton = False
			.DeleteButton = False
			.Height = 550
			.Width = 400
			.Top = 50
			
			If Request.QueryString.Item("Reload") = "1" Then
				.sReloadIndex = Request.QueryString.Item("ReloadIndex")
			End If
		End If
	End With
End Sub

'% insPreDP016: Se definen los objetos a ser utilizados.
'-----------------------------------------------------------------------------------------
Private Sub insPreDP016()
	'-----------------------------------------------------------------------------------------
	Dim lintIndex As Object
	Dim lcolConmutativs As eProduct.Conmutativs
	Dim lclsConmutativ As Object
	
	
Response.Write("" & vbCrLf)
Response.Write("<SCRIPT>" & vbCrLf)
Response.Write("" & vbCrLf)
Response.Write("//% insPreZone: Se definen las acciones a utilizar." & vbCrLf)
Response.Write("//-------------------------------------------------------------------------------------------------------------------" & vbCrLf)
Response.Write("function insPreZone(llngAction){" & vbCrLf)
Response.Write("//-------------------------------------------------------------------------------------------------------------------" & vbCrLf)
Response.Write("	switch (llngAction){" & vbCrLf)
Response.Write("	    case 302:" & vbCrLf)
Response.Write("	    case 401:" & vbCrLf)
Response.Write("	        document.location.href = document.location.href.replace(/&nMainAction.*/,'') + '&nMainAction=' + llngAction" & vbCrLf)
Response.Write("	        break;" & vbCrLf)
Response.Write("	}" & vbCrLf)
Response.Write("}" & vbCrLf)
Response.Write("" & vbCrLf)
Response.Write("</" & "SCRIPT>" & vbCrLf)
Response.Write("")

	
	'+ Se setea el objeto y se realiza la lectura del o los registros a ser mostrados
	'+ en las columnas del grid.
	lcolConmutativs = New eProduct.Conmutativs
	
	If lcolConmutativs.FindConmutativ(CStr(session("sMortalco")), CDbl(session("nInterest")), True) Then
		For	Each lclsConmutativ In lcolConmutativs
			With lclsConmutativ
				mobjGrid.Columns("tcnAge").DefValue = .nAge
				mobjGrid.Columns("tcnMonth").DefValue = .nMonth
				mobjGrid.Columns("tcnLive_lx").DefValue = .nLive_lx
				mobjGrid.Columns("tcnDeath_dx").DefValue = .nDeath_dx
				mobjGrid.Columns("tcnDeath_qx").DefValue = .nDeath_qx
				mobjGrid.Columns("tcnLiver_px").DefValue = .nLiver_px
				mobjGrid.Columns("tcnConmu_dx").DefValue = .nConmu_dx
				mobjGrid.Columns("tcnConmu_cx").DefValue = .nConmu_cx
				mobjGrid.Columns("tcnConmu_nx").DefValue = .nConmu_nx
				mobjGrid.Columns("tcnConmu_mx").DefValue = .nConmu_mx
				mobjGrid.Columns("tcnConmu_sx").DefValue = .nConmu_sx
				mobjGrid.Columns("tcnConmu_rx").DefValue = .nConmu_rx
				mobjGrid.Columns("tcnConmu_tx").DefValue = .nConmu_tx
				mobjGrid.Columns("tcnConmu_vx").DefValue = .nConmu_vx
				mobjGrid.Columns("tcnConmu_ex").DefValue = .nConmu_ex
				
				Response.Write(mobjGrid.DoRow())
			End With
		Next lclsConmutativ
	End If
	
	Response.Write(mobjGrid.closeTable())
	Response.Write(mobjValues.BeginPageButton)
	
	lcolConmutativs = Nothing
	lclsConmutativ = Nothing
End Sub

'% insPreDP016Upd: Permite realizar el llamado a la ventana PopUp.
'-----------------------------------------------------------------------------------------
Private Sub insPreDP016Upd()
	'-----------------------------------------------------------------------------------------
	Response.Write(mobjGrid.DoFormUpd(Request.QueryString.Item("Action"), "ValProduct.aspx", Request.QueryString.Item("sCodispl"), Request.QueryString.Item("nMainAction"),  , CShort(Request.QueryString.Item("Index"))))
End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values

mobjValues.sCodisplPage = "dp016"
%>
<SCRIPT LANGUAGE="JavaScript">
//- Variable para el control de versiones
document.VssVersion="$$Revision: 1 $|$$Date: 2/09/03 19:04 $|$$Author: Iusr_llanquihue $"

//% insCancel: Permite cancelar la página.
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
'+ Se realiza el llamado a las rutinas generales para cargar la página invocada.
With Response
	.Write(mobjValues.StyleSheet())
	
	If Request.QueryString.Item("Type") <> "PopUp" Then
		mobjMenu = New eFunctions.Menues
		.Write("<SCRIPT>var	nMainAction	= 0" & Request.QueryString.Item("nMainAction") & "</SCRIPT>")
		.Write(mobjMenu.setZone(2, "DP016", "DP016.aspx"))
		mobjMenu = Nothing
	End If
	
	If Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionQuery) Then
		mobjValues.ActionQuery = True
	End If
End With
%>
</SCRIPT>
</HEAD>

<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="DP016" ACTION="valProduct.aspx?sZone=2">
<%
Call insDefineHeader()

If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write(mobjValues.ShowWindowsName("DP016"))
	
	Call insPreDP016()
Else
	Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl")))
	
	Call insPreDP016Upd()
End If

mobjValues = Nothing
mobjGrid = Nothing
%>
</FORM>
</BODY>
</HTML>





