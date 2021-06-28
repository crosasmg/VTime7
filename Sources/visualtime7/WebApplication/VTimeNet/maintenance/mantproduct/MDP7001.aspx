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
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnRateColumnCaption"), "tcnRate", 10,  ,  , GetLocalResourceObject("tcnRateColumnToolTip"),  , 2)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnMin_yearColumnCaption"), "tcnMin_year", 5,  ,  , GetLocalResourceObject("tcnMin_yearColumnToolTip"),  ,  ,  ,  ,  , Request.QueryString.Item("Type") = "PopUp" And Request.QueryString.Item("Action") = "Update")
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnMax_yearColumnCaption"), "tcnMax_year", 5,  ,  , GetLocalResourceObject("tcnMax_yearColumnToolTip"))
		Call .AddHiddenColumn("sParam", vbNullString)
		Call .AddHiddenColumn("nTable", vbNullString)
		Call .AddHiddenColumn("nMax_year_Aux", vbNullString)
	End With
	
	With mobjGrid
		.Codispl = "MDP7001"
		.Top = 200
		.Left = 140
		.Width = 350
		.Height = 215
		.DeleteButton = True
		.AddButton = True
		.sDelRecordParam = "' + marrArray[lintIndex].sParam + '"
		.sEditRecordParam = "nTable=" & Request.QueryString.Item("nTable")
		.WidthDelete = 600
		If Session("bQuery") Then
			.ActionQuery = True
			.DeleteButton = False
			.AddButton = False
			.Columns("Sel").GridVisible = False
			.bOnlyForQuery = True
		Else
			.Columns("tcnRate").EditRecord = True
		End If
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
		.nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
	End With
End Sub

'% insPreMDP7001: Esta función se encarga de cargar los datos en la forma "Folder" 
'--------------------------------------------------------------------------------------------
Private Sub insPreMDP7001()
	'--------------------------------------------------------------------------------------------
	
	Dim lclsTab_apv_warran As Object
	Dim lcolTab_apv_warrans As eProduct.Tab_apv_warrans
	lcolTab_apv_warrans = New eProduct.Tab_apv_warrans
	
	If lcolTab_apv_warrans.Find(Session("nBranch"), Session("nProduct"), CInt(Request.QueryString.Item("nTable"))) Then
		
		For	Each lclsTab_apv_warran In lcolTab_apv_warrans
			With mobjGrid
				.Columns("tcnRate").DefValue = lclsTab_apv_warran.nRate
				.Columns("tcnMin_year").DefValue = lclsTab_apv_warran.nMin_year
				.Columns("tcnMax_year").DefValue = lclsTab_apv_warran.nMax_year
				.Columns("nMax_year_Aux").DefValue = lclsTab_apv_warran.nMax_year
				.Columns("sParam").DefValue = "nMin_year=" & lclsTab_apv_warran.nMin_year & "&nTable=" & lclsTab_apv_warran.nTable & "&nMax_year=" & lclsTab_apv_warran.nMax_year
			End With
			Response.Write(mobjGrid.DoRow())
		Next lclsTab_apv_warran
	End If
	
	lcolTab_apv_warrans = Nothing
	
	Response.Write(mobjGrid.closeTable())
	Response.Write(mobjValues.BeginPageButton)
	
End Sub

'% insPreMDP7001Upd. Se define esta funcion para contruir el contenido de la ventana UPD de la tabla de corto plazo
'------------------------------------------------------------------------------------------------------------------
Private Sub insPreMDP7001Upd()
	'------------------------------------------------------------------------------------------------------------------		
	Dim lblnPost As Object
	Dim lobjError As Object
	
	Dim lclsTab_apv_warran As eProduct.Tab_apv_warran
	If Request.QueryString.Item("Action") = "Del" Then
		
		lclsTab_apv_warran = New eProduct.Tab_apv_warran
		
		
		With Request
			If lclsTab_apv_warran.insPostMDP7001(Session("nBranch"), Session("nProduct"), CInt(.QueryString.Item("nTable")), CInt(.QueryString.Item("nMin_year")), 0, 0, Session("nUsercode"), .QueryString.Item("Action")) Then
				Response.Write(mobjValues.ConfirmDelete())
			End If
		End With
		lclsTab_apv_warran = Nothing
	End If
	
	Response.Write(mobjGrid.DoFormUpd(Request.QueryString.Item("Action"), "valMantProduct.aspx", "MDP7001", Request.QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(Request.QueryString.Item("Index"))))
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
		.Write(mobjMenu.setZone(2, "MDP7001", "MDP7001.aspx"))
		.Write("<SCRIPT>var nMainAction = top.frames['fraSequence'].plngMainAction</SCRIPT>")
	End If
	.Write(mobjValues.StyleSheet())
End With
%>
<SCRIPT>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 2 $|$$Date: 12-08-09 14:46 $|$$Author: Mgonzalez $"
</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="MDP7001" ACTION="valMantProduct.aspx?Validate=1">
<%
Response.Write(mobjValues.ShowWindowsName("MDP7001"))
Call insDefineHeader()
If Request.QueryString.Item("Type") <> "PopUp" Then
	Call insPreMDP7001()
Else
	Call insPreMDP7001Upd()
End If

mobjValues = Nothing
mobjGrid = Nothing
mobjMenu = Nothing
%>
</FORM>
</BODY>
</HTML>




