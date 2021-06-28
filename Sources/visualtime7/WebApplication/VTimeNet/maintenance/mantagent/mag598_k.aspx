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
		Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeCurrencyColumnCaption"), "cbeCurrency", "Table11", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeCurrencyColumnToolTip"),  , 1)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnAmo_IniColumnCaption"), "tcnAmo_Ini", 18, "", True, GetLocalResourceObject("tcnAmo_IniColumnToolTip"), True, 6,  ,  ,  , False, 2)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnAmo_EndColumnCaption"), "tcnAmo_End", 18, "", True, GetLocalResourceObject("tcnAmo_EndColumnToolTip"), True, 6,  ,  ,  , False, 3)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnFactorColumnCaption"), "tcnFactor", 5, "", True, GetLocalResourceObject("tcnFactorColumnToolTip"),  , 2,  ,  ,  , False, 4)
	End With
	
	With mobjGrid
		.Codispl = Request.QueryString.Item("sCodispl")
		.Codisp = "MAG598_k"
		mobjValues.sCodisplPage = "MAG598"
		.Top = 100
		.Height = 250
		.Width = 300
		.bOnlyForQuery = Request.QueryString.Item("nMainAction") = "401"
		.ActionQuery = (Request.QueryString.Item("nMainAction") = "401" Or IsNothing(Request.QueryString.Item("nMainAction")))
		.Columns("Sel").GridVisible = Not .ActionQuery
		.Columns("cbeCurrency").EditRecord = True
		.Columns("cbeCurrency").Disabled = Request.QueryString.Item("Action") = "Update"
		.Columns("tcnAmo_Ini").Disabled = Request.QueryString.Item("Action") = "Update"
		.Columns("tcnAmo_End").Disabled = Request.QueryString.Item("Action") = "Update"
		.sDelRecordParam = "pnAmo_Ini='+ marrArray[lintIndex].tcnAmo_Ini + '" & "&pnCurrency='+ marrArray[lintIndex].cbeCurrency + '"
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
End Sub

'%insPreMAG598: Esta función se encarga de cargar los datos en la forma "Folder" 
'--------------------------------------------------------------------------------------------
Private Sub insPreMAG598()
	'--------------------------------------------------------------------------------------------
	Dim lcolaccomp_factor As eAgent.accomp_factors
	Dim lclsaccomp_factor As Object
	
	lcolaccomp_factor = New eAgent.accomp_factors
	
	With mobjGrid
		If lcolaccomp_factor.Find() Then
			For	Each lclsaccomp_factor In lcolaccomp_factor
				.Columns("cbeCurrency").DefValue = lclsaccomp_factor.nCurrency
				.Columns("tcnAmo_Ini").DefValue = lclsaccomp_factor.nAmo_Ini
				.Columns("tcnAmo_End").DefValue = lclsaccomp_factor.nAmo_End
				.Columns("tcnFactor").DefValue = lclsaccomp_factor.nFactor
				Response.Write(mobjGrid.DoRow())
			Next lclsaccomp_factor
		End If
	End With
	
	
	Response.Write(mobjGrid.CloseTable())
	Response.Write(mobjValues.BeginPageButton)
	
	lcolaccomp_factor = Nothing
End Sub

'% insPreMAG598Upd. Se define esta funcion para contruir el contenido de la ventana UPD de los archivos de datos particulares
'----------------------------------------------------------------------------------------------------------------------------
Private Sub insPreMAG598Upd()
	'----------------------------------------------------------------------------------------------------------------------------
	Dim lclsaccomp_factor As eAgent.accomp_factor
	
	With Request
		If .QueryString.Item("Action") = "Del" Then
			Response.Write(mobjValues.ConfirmDelete())
			lclsaccomp_factor = New eAgent.accomp_factor
			Call lclsaccomp_factor.insPostMAG598_K(False, .QueryString.Item("sCodispl"), CInt(.QueryString.Item("nMainAction")), .QueryString.Item("Action"), Session("nUsercode"), mobjValues.StringToType(.QueryString.Item("pnCurrency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("pnAmo_Ini"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("pnAmo_End"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("tcnFactor"), eFunctions.Values.eTypeData.etdDouble))
			lclsaccomp_factor = Nothing
		End If
		Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "valMantAgent.aspx", .QueryString.Item("sCodispl"), .QueryString.Item("nMainAction"),  , CShort(.QueryString.Item("Index"))))
	End With
	lclsaccomp_factor = Nothing
End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjValues.sCodisplPage = "MAG598"
%>
<HTML>
<HEAD>
	<META NAME="GENERATOR" CONTENT="eTransaction Designer for Visual TIME"> 
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>



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
		.Write(mobjMenu.MakeMenu(Request.QueryString.Item("sCodispl"), "MAG598_K.aspx", 1, ""))
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
<FORM METHOD="post" ID="FORM" NAME="frmMAG598" ACTION="valMantAgent.aspx?sTime=1">
<%
Call insDefineHeader()
If Request.QueryString.Item("Type") <> "PopUp" Then
	Call insPreMAG598()
Else
	Call insPreMAG598Upd()
End If
mobjValues = Nothing
mobjGrid = Nothing
%>
</FORM>
</BODY>
</HTML>




