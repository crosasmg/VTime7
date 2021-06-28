<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eAgent" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Se define la variable mobjGrid para el manejo del Grid de la ventana
Dim mobjGrid As eFunctions.Grid

'- Objeto para el manejo de las zonas de la página
Dim mobjMenu As eFunctions.Menues


'%insDefineHeader. Definición de columnas del GRID
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------        
	mobjGrid = New eFunctions.Grid
	
	'+Se definen todas las columnas del Grid
	With mobjGrid.Columns
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnYear_IniColumnCaption"), "tcnYear_Ini", 5, "", True, GetLocalResourceObject("tcnYear_IniColumnToolTip"),  , 2,  ,  ,  , True, 1)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnYear_EndColumnCaption"), "tcnYear_End", 5, "", True, GetLocalResourceObject("tcnYear_EndColumnToolTip"),  , 2,  ,  ,  , True, 2)
		Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeCurrencyColumnCaption"), "cbeCurrency", "Table11", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeCurrencyColumnToolTip"),  , 3)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnMinAmountColumnCaption"), "tcnMinAmount", 18, "", False, GetLocalResourceObject("tcnMinAmountColumnToolTip"), True, 6,  ,  ,  , False, 4)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnPersistColumnCaption"), "tcnPersist", 5, "", True, GetLocalResourceObject("tcnPersistColumnToolTip"),  , 2,  ,  ,  , False, 5)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnReal_GoalColumnCaption"), "tcnReal_Goal", 18, "", True, GetLocalResourceObject("tcnReal_GoalColumnToolTip"), True, 6,  ,  ,  , False, 6)
	End With
	
	With mobjGrid
		.Codispl = Request.QueryString.Item("sCodispl")
		.Codisp = "MAG597_k"
		.sCodisplPage = "MAG597"
		.Top = 100
		.Height = 320
		.Width = 370
		.bOnlyForQuery = Request.QueryString.Item("nMainAction") = "401"
		.ActionQuery = (Request.QueryString.Item("nMainAction") = "401" Or IsNothing(Request.QueryString.Item("nMainAction")))
		.Columns("Sel").GridVisible = Not .ActionQuery
		.Columns("tcnYear_Ini").EditRecord = True
		.Columns("tcnYear_Ini").Disabled = Request.QueryString.Item("Action") = "Update"
		.Columns("tcnYear_End").Disabled = Request.QueryString.Item("Action") = "Update"
		.sDelRecordParam = "pnYear_Ini='+ marrArray[lintIndex].tcnYear_Ini + '" & "&pnYear_End='+ marrArray[lintIndex].tcnYear_End + '"
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
End Sub

'%insPreMAG597: Esta función se encarga de cargar los datos en la forma "Folder" 
'--------------------------------------------------------------------------------------------
Private Sub insPreMAG597()
	'--------------------------------------------------------------------------------------------
	Dim lcolbonus_gen As eAgent.bonus_gens
	Dim lclsbonus_gen As Object
	lcolbonus_gen = New eAgent.bonus_gens
	
	With mobjGrid
		If lcolbonus_gen.Find() Then
			For	Each lclsbonus_gen In lcolbonus_gen
				.Columns("tcnYear_Ini").DefValue = lclsbonus_gen.nYear_Ini
				.Columns("tcnYear_End").DefValue = lclsbonus_gen.nYear_End
				.Columns("cbeCurrency").DefValue = lclsbonus_gen.nCurrency
				.Columns("tcnMinAmount").DefValue = lclsbonus_gen.nMinAmount
				.Columns("tcnPersist").DefValue = lclsbonus_gen.nPersist
				.Columns("tcnReal_Goal").DefValue = lclsbonus_gen.nReal_Goal
				Response.Write(mobjGrid.DoRow())
			Next lclsbonus_gen
		End If
	End With
	
	
	Response.Write(mobjGrid.CloseTable())
	Response.Write(mobjValues.BeginPageButton)
	
	lcolbonus_gen = Nothing
End Sub

'% insPreMAG597Upd. Se define esta funcion para contruir el contenido de la ventana UPD de los archivos de datos particulares
'----------------------------------------------------------------------------------------------------------------------------
Private Sub insPreMAG597Upd()
	'----------------------------------------------------------------------------------------------------------------------------
	Dim lclsbonus_gen As eAgent.bonus_gen
	
	With Request
		If .QueryString.Item("Action") = "Del" Then
			Response.Write(mobjValues.ConfirmDelete())
			lclsbonus_gen = New eAgent.bonus_gen
			Call lclsbonus_gen.insPostMAG597_K(False, .QueryString.Item("sCodispl"), CInt(.QueryString.Item("nMainAction")), "Del", Session("nUsercode"), mobjValues.StringToType(.QueryString.Item("pnYear_Ini"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("pnYear_End"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("cbeCurrency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("tcnMinAmount"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("tcnPersist"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("tcnReal_Goal"), eFunctions.Values.eTypeData.etdDouble))
			lclsbonus_gen = Nothing
		End If
		Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "valMantAgent.aspx", .QueryString.Item("sCodispl"), .QueryString.Item("nMainAction"),  , CShort(.QueryString.Item("Index"))))
	End With
	lclsbonus_gen = Nothing
End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjValues.sCodisplPage = "MAG597"
%>


<HTML>
<HEAD>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
	<META NAME="GENERATOR" Content="eTransaction Designer for Visual TIME">
<SCRIPT>
//+ Variable para el control de versiones
    document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 15:35 $"

//* Funcion que cancela las las acciones de la Pagina
//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
    return true;
}

//+ Controla el estado de los campos de la página
//------------------------------------------------------------------------------------------
function insStateZone(){
//------------------------------------------------------------------------------------------      
    return true;
}

//+ Controla las acciones a ejecutar sobre la ventana
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
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write("<SCRIPT>var nMainAction = " & CShort("0" & Request.QueryString.Item("nMainAction")) & "</SCRIPT>")
End If
With Response
	.Write(mobjValues.StyleSheet())
	If Request.QueryString.Item("Type") <> "PopUp" Then
		.Write("<SCRIPT LANGUAGE=""JavaScript"" SRC=""/VTimeNet/Scripts/tmenu.js""></SCRIPT>")
		mobjMenu = New eFunctions.Menues
		.Write(mobjMenu.MakeMenu(Request.QueryString.Item("sCodispl"), "MAG597_K.aspx", 1, ""))
		mobjMenu = Nothing
	End If
End With
%>    
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<%
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write("<BR><BR>")
End If
Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl")))
%>
<FORM METHOD="post" ID="FORM" NAME="frmMAG750" ACTION="valMantAgent.aspx?sTime=1">
<%
Call insDefineHeader()
If Request.QueryString.Item("Type") <> "PopUp" Then
	Call insPreMAG597()
Else
	Call insPreMAG597Upd()
End If
mobjValues = Nothing
mobjGrid = Nothing
%>
</FORM>
</BODY>
</HTML>






