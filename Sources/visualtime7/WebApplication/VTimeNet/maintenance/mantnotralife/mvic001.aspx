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
	
	With mobjGrid.Columns
		.AddDateColumn(0, GetLocalResourceObject("tcdEffecdateColumnCaption"), "tcdEffecdate",  ,  , GetLocalResourceObject("tcdEffecdateColumnToolTip"))
		.AddNumericColumn(0, GetLocalResourceObject("tcnAmountColumnCaption"), "tcnAmount", 12, CStr(0),  , GetLocalResourceObject("tcnAmountColumnToolTip"), True, 6)
	End With
	
	'**+ The general properties of the grid are defined.
	'+ Se definen las propiedades generales del grid.
	
	With mobjGrid
		.Codispl = "MVIC001"
		.sCodisplPage = "MVIC001"
		.DeleteButton = False
		.AddButton = False
		.Columns("Sel").GridVisible = False
		.ActionQuery = True
	End With
End Sub

'**% insPreMVIC001: This function allows to show in the grid the read values.
'% insPreMVIC001: Esta función permite mostrar en el grid los valores leídos.
'--------------------------------------------------------------------------------------------
Private Sub insPreMVIC001()
	'--------------------------------------------------------------------------------------------
	Dim lintCount As Short
	Dim lcolFund_values As ePolicy.Fund_values
	Dim lclsFund_value As Object
	
	lcolFund_values = New ePolicy.Fund_values
	
	If lcolFund_values.Find(Session("nFund"), Session("nCurrency")) Then
		lintCount = 0
		
		For	Each lclsFund_value In lcolFund_values
			With lclsFund_value
				mobjGrid.Columns("tcdEffecdate").DefValue = .dEffecdate
				mobjGrid.Columns("tcnAmount").DefValue = .nAmount
				
				Response.Write(mobjGrid.DoRow())
			End With
			
			lintCount = lintCount + 1
			
			If lintCount = 200 Then
				Exit For
			End If
		Next lclsFund_value
	End If
	
	Response.Write(mobjGrid.closeTable())
	
	lcolFund_values = Nothing
	lclsFund_value = Nothing
End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues
mobjValues.sCodisplPage = "MVIC001"
%>
<HTML>
<HEAD>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>




    <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">

<SCRIPT LANGUAGE="JavaScript">
//**+ Source Safe control of version
//+ Para Control de Versiones de Source Safe
    document.VssVersion="$$Revision: 1 $|$$Date: 2/09/03 19:03 $|$$Author: Iusr_llanquihue $"

//**% insCancel: This function executes the cancel action of the page.
//% insCancel: Ejecuta la acción cancelar de la página.
//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
	return true;
}

</SCRIPT>
    <%
Response.Write(mobjValues.StyleSheet())
Response.Write(mobjMenu.setZone(2, "MVIC001", "MVIC001.aspx"))

If Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionQuery) Then
	mobjValues.ActionQuery = True
End If
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="MVIC001" ACTION="ValMantNoTraLife.aspx?Zone=2">
<%
Response.Write(mobjValues.ShowWindowsName("MVIC001"))

Call insDefineHeader()
Call insPreMVIC001()

mobjGrid = Nothing
mobjValues = Nothing
mobjMenu = Nothing
%>     
</FORM>
</BODY>
</HTML>




