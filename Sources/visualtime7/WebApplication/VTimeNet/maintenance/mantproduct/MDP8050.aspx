<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eProduct" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Se define la variable mobjGrid para el manejo del Grid de la ventana
Dim mobjGrid As eFunctions.Grid

'- Objeto para el manejo de las zonas de la página
Dim mobjMenu As eFunctions.Menues


'% insDefineHeader. Definición de columnas del GRID
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------	    
	
	'+Se definen todas las columnas del Grid
	
	With mobjGrid.Columns
		
		Call .AddPossiblesColumn(0, GetLocalResourceObject("cbenMonthColumnCaption"), "cbenMonth", "Table7013", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbenMonthColumnToolTip"),  , 0)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnRateColumnCaption"), "tcnRate", 9,  ,  , GetLocalResourceObject("tcnRateColumnToolTip"),  , 6,  ,  ,  , Request.QueryString.Item("Type") <> "PopUp")
		
		'+Tasa fija		
		If Session("nTypeInvest") = 1 Then
			Call .AddNumericColumn(0, GetLocalResourceObject("tcnRate_secColumnCaption"), "tcnRate_sec", 9,  ,  , GetLocalResourceObject("tcnRate_secColumnToolTip"),  , 6,  ,  ,  , True)
		Else
			'+Tasa Mixta (Doble garantía)		
			If Session("nTypeInvest") = 2 Then
				Call .AddNumericColumn(0, GetLocalResourceObject("tcnRate_secColumnCaption"), "tcnRate_sec", 9,  ,  , GetLocalResourceObject("tcnRate_secColumnToolTip"),  , 6,  ,  ,  , Request.QueryString.Item("Type") <> "PopUp")
			Else
				'+Tasa Paralela			
				If Session("nTypeInvest") = 3 Then
					Call .AddNumericColumn(0, GetLocalResourceObject("tcnRate_secColumnCaption"), "tcnRate_sec", 9,  ,  , GetLocalResourceObject("tcnRate_secColumnToolTip"),  , 6,  ,  ,  , Request.QueryString.Item("Type") <> "PopUp")
				End If
			End If
		End If
		
		
	End With
	
	With mobjGrid
		.Codispl = "MDP8050"
		.Top = 200
		.Left = 140
		.Width = 350
		.Height = 215
		.DeleteButton = True
		.AddButton = True
		.sDelRecordParam = "nMonth=' + marrArray[lintIndex].cbenMonth + '"
		.sEditRecordParam = "nTable=" & Request.QueryString.Item("nTable")
		.WidthDelete = 600
		If Session("bQuery") Then
			.ActionQuery = True
			.DeleteButton = False
			.AddButton = False
			.Columns("Sel").GridVisible = False
			.bOnlyForQuery = True
		Else
			.Columns("cbenMonth").EditRecord = True
		End If
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
		.nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
	End With
End Sub

'% insPreMDP8050: Esta función se encarga de cargar los datos en la forma "Folder" 
'--------------------------------------------------------------------------------------------
Private Sub insPreMDP8050()
	'--------------------------------------------------------------------------------------------
	
	Dim lclsPlan_Intwar_Month As Object
	Dim lcolPlan_Intwar_Month As eProduct.Plan_Intwar_Month
	
	lcolPlan_Intwar_Month = New eProduct.Plan_Intwar_Month
	
	
	If lcolPlan_Intwar_Month.Find_Plan_Intwar_Month(Session("nYear"), Session("nTypeInvest")) Then
		
		For	Each lclsPlan_Intwar_Month In lcolPlan_Intwar_Month
			With mobjGrid
				.Columns("tcnRate").DefValue = lclsPlan_Intwar_Month.nRate
				.Columns("tcnRate_sec").DefValue = lclsPlan_Intwar_Month.nRate_sec
				.Columns("cbenMonth").DefValue = lclsPlan_Intwar_Month.nMonth
			End With
			Response.Write(mobjGrid.DoRow())
		Next lclsPlan_Intwar_Month
	End If
	
	lcolPlan_Intwar_Month = Nothing
	
	Response.Write(mobjGrid.closeTable())
	Response.Write(mobjValues.BeginPageButton)
	
End Sub

'% insPreMDP8050Upd. Se define esta funcion para contruir el contenido de la ventana UPD de la tabla de corto plazo
'------------------------------------------------------------------------------------------------------------------
Private Sub insPreMDP8050Upd()
	'------------------------------------------------------------------------------------------------------------------		
	Dim lblnPost As Boolean
	Dim lobjError As Object
	
	Dim lclsPlan_Intwar_Month As eProduct.Plan_Intwar_Month
	If Request.QueryString.Item("Action") = "Del" Then
		
		lclsPlan_Intwar_Month = New eProduct.Plan_Intwar_Month
		
		Response.Write(mobjValues.ConfirmDelete())
		
		With Request
			lblnPost = lclsPlan_Intwar_Month.insPostMDP8050(.QueryString.Item("Action"), Session("nYear"), Session("nTypeInvest"), CInt(Request.QueryString.Item("nMonth")), CDbl(Request.QueryString.Item("nRate")), CDbl(Request.QueryString.Item("nRate_sec")), Session("nUsercode"))
		End With
		lclsPlan_Intwar_Month = Nothing
	End If
	
	Response.Write(mobjGrid.DoFormUpd(Request.QueryString.Item("Action"), "valMantProduct.aspx", "MDP8050", Request.QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(Request.QueryString.Item("Index"))))
End Sub

</script>
<%Response.Expires = -1
mobjValues = New eFunctions.Values
mobjGrid = New eFunctions.Grid
mobjMenu = New eFunctions.Menues

mobjValues.ActionQuery = Session("bQuery")
%>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>


<%
With Response
	If Request.QueryString.Item("Type") <> "PopUp" Then
		.Write(mobjMenu.setZone(2, "MDP8050", "MDP8050.aspx"))
		.Write("<SCRIPT>var nMainAction = top.frames['fraSequence'].plngMainAction</SCRIPT>")
	End If
	.Write(mobjValues.StyleSheet())
End With
%>
<SCRIPT>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 3 $|$$Date: 20-10-09 15:18 $|$$Author: Ljimenez $"
</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="MDP8050" ACTION="valMantProduct.aspx?Validate=1">
<%
Response.Write(mobjValues.ShowWindowsName("MDP8050"))
Call insDefineHeader()
If Request.QueryString.Item("Type") <> "PopUp" Then
	Call insPreMDP8050()
Else
	Call insPreMDP8050Upd()
End If

mobjValues = Nothing
mobjGrid = Nothing
mobjMenu = Nothing
%>
</FORM>
</BODY>
</HTML>




