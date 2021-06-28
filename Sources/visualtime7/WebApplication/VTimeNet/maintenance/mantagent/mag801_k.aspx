<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eAgent" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo del grid de la página
Dim mobjGrid As eFunctions.Grid

'- Objeto para el manejo del menú
Dim mobjMenu As eFunctions.Menues

'- Objeto para el manejo particular de los datos de la página
Dim mcolAgent_rate As eAgent.Agent_rates


'% insDefineHeader: se definen las propiedades del grid
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	
	'+ Se definen las columnas del grid    
	With mobjGrid.Columns
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnInit_rateColumnCaption"), "tcnInit_rate", 5, vbNullString,  , GetLocalResourceObject("tcnInit_rateColumnToolTip"),  ,  ,  ,  ,  , Request.QueryString.Item("Action") = "Update")
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnEnd_rateColumnCaption"), "tcnEnd_rate", 5, vbNullString,  , GetLocalResourceObject("tcnEnd_rateColumnToolTip"),  ,  ,  ,  ,  , Request.QueryString.Item("Action") = "Update")
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnFactorColumnCaption"), "tcnFactor", 5, vbNullString,  , GetLocalResourceObject("tcnFactorColumnToolTip"), True, 2)
	End With
	
	'+ Se definen las propiedades generales del grid
	
	With mobjGrid
		.Codispl = "MAG801"
		.sCodisplPage = "MAG801"
		.ActionQuery = mobjValues.ActionQuery Or IsNothing(Request.QueryString.Item("nMainAction"))
		.Columns("tcnInit_rate").EditRecord = True
		.Height = 220
		.Width = 370
		.nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
		.Columns("Sel").GridVisible = Not .ActionQuery
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
		.sDelRecordParam = "nInit_rate='+ marrArray[lintIndex].tcnInit_rate + '" & "&nEnd_rate='+marrArray[lintIndex].tcnEnd_rate + '"
		
	End With
End Sub

'% insPreMAG801: se realiza el manejo del grid
'--------------------------------------------------------------------------------------------
Private Sub insPreMAG801()
	'--------------------------------------------------------------------------------------------
	Dim lclsAgent_rate As Object
	
	mcolAgent_rate = New eAgent.Agent_rates
	
	If mcolAgent_rate.Find() Then
		For	Each lclsAgent_rate In mcolAgent_rate
			With mobjGrid
				.Columns("tcnInit_rate").DefValue = lclsAgent_rate.nInit_rate
				.Columns("tcnEnd_rate").DefValue = lclsAgent_rate.nEnd_rate
				.Columns("tcnFactor").DefValue = lclsAgent_rate.nFactor
				Response.Write(.DoRow)
			End With
		Next lclsAgent_rate
	End If
	
	Response.Write(mobjGrid.closeTable())
End Sub

'% insPreMAG801Upd: Se realiza el manejo de la ventana PopUp asociada al grid
'--------------------------------------------------------------------------------------------
Private Sub insPreMAG801Upd()
	'--------------------------------------------------------------------------------------------
	
	Dim lobjAgent_rate As eAgent.Agent_rate
	
	lobjAgent_rate = New eAgent.Agent_rate
	
	With Request
		If Request.QueryString.Item("Action") = "Del" Then
			Response.Write(mobjValues.ConfirmDelete())
			With lobjAgent_rate
				.nInit_rate = CDbl(Request.QueryString.Item("nInit_rate"))
				.nEnd_rate = CDbl(Request.QueryString.Item("nEnd_rate"))
				.Delete()
			End With
		End If
		
		Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "valMantAgent.aspx", "MAG801", .QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(.QueryString.Item("Index"))))
	End With
	lobjAgent_rate = Nothing
End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues

mobjValues.ActionQuery = CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401
mobjValues.sCodisplPage = "MAG801"
%>
<HTML>
<HEAD>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>    
<SCRIPT LANGUAGE=JavaScript>
//% insStateZone: se controla el estado de los campos de la página
//--------------------------------------------------------------------------------------------
function insStateZone(){
//--------------------------------------------------------------------------------------------
}

//% insCancel: se controla la acción Cancelar de la página
//--------------------------------------------------------------------------------------------
function insCancel(){
//--------------------------------------------------------------------------------------------
	return true;
}

//% insFinish: se controla la acción Cancelar de la página
//--------------------------------------------------------------------------------------------
function insFinish(){
//--------------------------------------------------------------------------------------------
    return true;
}

//% insPreZone: Se activa al seleccionar alguna acción
//-------------------------------------------------------------------------------------------------------------------
function insPreZone(llngAction){
//-------------------------------------------------------------------------------------------------------------------
    switch (llngAction){
        case 302:
        case 305:
        case 401:
	        document.location.href = document.location.href.replace(/&nMainAction.*/,'') + '&nMainAction=' + llngAction            
            break;
    }
}
</SCRIPT>
	<%
Response.Write(mobjValues.StyleSheet())
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write("<SCRIPT LANGUAGE=""JavaScript"" SRC=""/VTimeNet/Scripts/tmenu.js""></SCRIPT>")
	Response.Write(mobjMenu.MakeMenu("MAG801", "MAG801_K.aspx", 1, vbNullString))
	mobjMenu = Nothing
	Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
End If
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" NAME="MAG801" ACTION="valMantAgent.aspx?sMode=1">

<%
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write("<BR><BR>")
End If

Response.Write(mobjValues.ShowWindowsName("MAG801"))

Call insDefineHeader()

If Request.QueryString.Item("Type") = "PopUp" Then
	Call insPreMAG801Upd()
Else
	Call insPreMAG801()
End If
%>
</FORM> 
</BODY>
</HTML>

<%
mobjGrid = Nothing
mobjValues = Nothing
mcolAgent_rate = Nothing

%>




