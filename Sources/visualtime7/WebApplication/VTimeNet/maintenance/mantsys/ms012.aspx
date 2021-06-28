<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eGeneral" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Se define la variable mobjGrid para el manejo del Grid de la ventana
Dim mobjGrid As eFunctions.Grid

'- Objeto para el manejo de las zonas de la página
Dim mobjMenu As eFunctions.Menues



'%insDefineHeader: Se definen las columnas del grid
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	
	mobjGrid.sCodisplPage = "ms012"
	
	'+ All of columns of the grid are defined	
	'+Se definen todas las columnas del Grid
	
	With mobjGrid.Columns
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnYearColumnCaption"), "tcnYear", 5, "", True, GetLocalResourceObject("tcnYearColumnToolTip"), False, 0,  ,  ,  , False)
		Call .AddPossiblesColumn(0, GetLocalResourceObject("tcnMonthColumnCaption"), "tcnMonth", "Table7013", 1,  ,  ,  ,  ,  ,  ,  , 1, GetLocalResourceObject("tcnMonthColumnToolTip"), 2)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnIndexfacColumnCaption"), "tcnIndexfac", 5, "", False, GetLocalResourceObject("tcnIndexfacColumnToolTip"), False, 2,  ,  ,  , False,  , True)
	End With
	
	With mobjGrid
		.nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
		.Codispl = "MS012"
		.Codisp = "MS012"
		.Top = 200
		.Left = 300
		.Height = 224
		.Width = 250
		.ActionQuery = mobjValues.ActionQuery
		.bOnlyForQuery = Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionQuery)
		.Columns("Sel").GridVisible = Not .ActionQuery
		.Columns("tcnYear").EditRecord = True
		.Columns("tcnYear").Disabled = Request.QueryString.Item("Action") = "Update"
		.Columns("tcnMonth").Disabled = Request.QueryString.Item("Action") = "Update"
		.sDelRecordParam = "nEcon_area=" & Request.QueryString.Item("nEcon_area") & "&nYear='+ marrArray[lintIndex].tcnYear + '" & "&nMonth='+ marrArray[lintIndex].tcnMonth + '"
		.sEditRecordParam = "nEcon_area=" & Request.QueryString.Item("nEcon_area")
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
End Sub

'%insPreMS012. Se crea la ventana madre (Principal)
'--------------------------------------------------------------------------------------------
Private Sub insPreMS012()
	'--------------------------------------------------------------------------------------------
	Dim lcolReval_facts As eGeneral.Reval_facts
	Dim lclsReval_fact As Object
	
	With Request
		lcolReval_facts = New eGeneral.Reval_facts
		With mobjGrid
			If lcolReval_facts.Find(mobjValues.StringToType(Request.QueryString.Item("nEcon_area"), eFunctions.Values.eTypeData.etdDouble)) Then
				For	Each lclsReval_fact In lcolReval_facts
					.Columns("tcnYear").DefValue = lclsReval_fact.nYear
					.Columns("tcnMonth").DefValue = lclsReval_fact.nMonth
					.Columns("tcnIndexfac").DefValue = lclsReval_fact.nIndexfac
					Response.Write(mobjGrid.DoRow())
				Next lclsReval_fact
			End If
		End With
		
	End With
	Response.Write(mobjGrid.CloseTable())
	
	lclsReval_fact = Nothing
	lcolReval_facts = Nothing
End Sub

'% insPreMS012Upd. Se actualiza la tabla Reval_fact desde la Pop up
'--------------------------------------------------------------------------------------------
Private Sub insPreMS012Upd()
	'--------------------------------------------------------------------------------------------
	Dim lclsReval_fact As eGeneral.Reval_fact
	
	With Request
		If .QueryString.Item("Action") = "Del" Then
			Response.Write(mobjValues.ConfirmDelete())
			lclsReval_fact = New eGeneral.Reval_fact
			Call lclsReval_fact.InsPostMS012(False, .QueryString.Item("Action"), Session("nUsercode"), mobjValues.StringToType(.QueryString.Item("nEcon_area"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nYear"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nMonth"), eFunctions.Values.eTypeData.etdDouble), 0)
		End If
		Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "ValMantSys.aspx", .QueryString.Item("sCodispl"), .QueryString.Item("nMainAction"), mobjGrid.ActionQuery, CShort(.QueryString.Item("Index"))))
	End With
	lclsReval_fact = Nothing
End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values

mobjValues.sCodisplPage = "ms012"
%>

<SCRIPT	LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>





<HTML>
<HEAD>
	<META NAME="GENERATOR" Content="eTransaction Designer for Visual TIME">
<%
mobjValues.ActionQuery = (Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionQuery))
With Response
	.Write(mobjValues.StyleSheet())
	.Write("<SCRIPT>var	nMainAction	= " & CShort("0" & Request.QueryString.Item("nMainAction")) & "</SCRIPT>")
	If Request.QueryString.Item("Type") <> "PopUp" Then
		mobjMenu = New eFunctions.Menues
		.Write(mobjMenu.setZone(2, "MS012", "MS012.aspx"))
		mobjMenu = Nothing
	End If
End With
%>
	</HEAD>
	<BODY ONUNLOAD="closeWindows();">
		<FORM METHOD="POST"	ID="FORM" NAME="frmMS012" ACTION="ValMantSys.aspx?sZone=2">
<%
Response.Write(mobjValues.ShowWindowsName("MS012"))
Call insDefineHeader()
If Request.QueryString.Item("Type") <> "PopUp" Then
	Call insPreMS012()
Else
	Call insPreMS012Upd()
End If
mobjValues = Nothing
mobjGrid = Nothing
%>	  
		</FORM>
	</BODY>
</HTML>




