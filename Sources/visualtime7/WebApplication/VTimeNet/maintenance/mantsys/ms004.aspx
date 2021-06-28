<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eGeneral" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo de las rutinas genéricas
Dim mobjMenu As eFunctions.Menues

'- Objeto para el manejo de las rutinas genéricas
Dim mobjGrid As eFunctions.Grid

Dim mlngRow As Integer
Dim mlngTotalRow As Integer


'% insDefineHeader:Este procedimiento se encarga de definir las columnas del grid y de habilitar
'% o inhabilitar los botones de añadir y eliminar.
'-----------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'-----------------------------------------------------------------------------------------
	
	mobjGrid = New eFunctions.Grid
	
	mobjGrid.sCodisplPage = "ms004"
	
	'+ Se definen las columns del Grid
	With mobjGrid.Columns
		Call .AddDateColumn(102115, GetLocalResourceObject("tcdEffecdateColumnCaption"), "tcdEffecdate",  , True, GetLocalResourceObject("tcdEffecdateColumnToolTip"),  ,  ,  , Request.QueryString.Item("Action") = "Update")
		Call .AddNumericColumn(102114, GetLocalResourceObject("tcnExchangeColumnCaption"), "tcnExchange", 11, CStr(0), True, GetLocalResourceObject("tcnExchangeColumnToolTip"), True, 6)
		Call .AddHiddenColumn("hddExchange", "")
	End With
	
	'+ Se asignan las caracteristicas del Grid
	
	With mobjGrid
		'+ Se crean los parametros para las listas de valores posibles
		.Columns("Sel").GridVisible = False
		If Request.QueryString.Item("nMainAction") = "401" Or Request.QueryString.Item("nMainAction") = vbNullString Then
			.ActionQuery = True
		End If
		
		.Codispl = "MS004"
		.Codisp = "MS004"
		.DeleteButton = False
		.Height = 200
		.Width = 350
		
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
End Sub

'% insPreSIC004: Esta función permite realizar la lectura de la tabla principal de la transacción.
'----------------------------------------------------------------------------------------------------
Private Sub insPreMS004()
	'----------------------------------------------------------------------------------------------------
	Dim lclsExchange As eGeneral.Exchange
	Dim lcolExchanges As eGeneral.Exchanges
	Dim bcond As Boolean
	
	lclsExchange = New eGeneral.Exchange
	lcolExchanges = New eGeneral.Exchanges
	
	If mobjValues.StringToType(Request.QueryString.Item("nRow"), eFunctions.Values.eTypeData.etdDouble) = eRemoteDB.Constants.intNull Then
		mlngRow = 1
	Else
		mlngRow = mobjValues.StringToType(Request.QueryString.Item("nRow"), eFunctions.Values.eTypeData.etdDouble)
	End If
	If lcolExchanges.Find(mobjValues.StringToType(Session("nCurrency"), eFunctions.Values.eTypeData.etdDouble), mlngRow) Then
		mlngTotalRow = lcolExchanges.Count
		bcond = True
		For	Each lclsExchange In lcolExchanges
			With mobjGrid
				.mblnLoadValue = True
				If lclsExchange.dEffecdate = Today Then
					.Columns("tcdEffecdate").EditRecord = True
					bcond = False
				Else
					.Columns("tcdEffecdate").EditRecord = False
				End If
				.Columns("tcdEffecdate").DefValue = CStr(lclsExchange.dEffecdate)
				.Columns("tcnExchange").DefValue = CStr(lclsExchange.nExchange)
				.Columns("hddExchange").DefValue = CStr(lclsExchange.nExchange)
			End With
			
			'+ Se ejecuta el metodo DoRow, que se encarga de mostrar los elementos de grid
			Response.Write(mobjGrid.DoRow())
			
		Next lclsExchange
	End If
	Response.Write(mobjGrid.closeTable())
	
	If Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionQuery) Then
		mobjValues.ActionQuery = True
	Else
		mobjValues.ActionQuery = False
	End If
	
	lclsExchange = Nothing
	lcolExchanges = Nothing
End Sub

'% insPreSIC004upd: Esta función permite Actualizar un registro del Grid
'----------------------------------------------------------------------------------------------------
Private Sub insPreMS004Upd()
	'----------------------------------------------------------------------------------------------------
	With Response
		.Write(mobjGrid.DoFormUpd(Request.QueryString.Item("Action"), "ValMantSys.aspx", Request.QueryString.Item("sCodispl"), Request.QueryString.Item("nMainAction"),  , CShort(Request.QueryString.Item("Index"))))
		.Write(mobjValues.HiddenControl("sAction", Request.QueryString.Item("Action")))
	End With
	Response.Write("<SCRIPT>disabledbackNext()</" & "Script>")
End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues

mobjValues.sCodisplPage = "ms004"
%>

<HTML>
<HEAD>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></SCRIPT>





    <%With Response
	.Write(mobjValues.StyleSheet())
	If Request.QueryString.Item("Type") <> "PopUp" Then
		.Write("<SCRIPT>var nMainAction=304</SCRIPT>")
		.Write(mobjMenu.setZone(2, "MS004", "MS004.aspx"))
		mobjMenu = Nothing
	End If
End With%>

<SCRIPT>
//% disabledbackNext: desabilita los vonones para moverse entre registros
//-------------------------------------------------------------------------------------------------------------------
function disabledbackNext(){
//-------------------------------------------------------------------------------------------------------------------
    self.document.forms[0].cmdBack.disabled=true;
    self.document.forms[0].cmdNext.disabled=true;
}
//% insStateZone: se manejan los campos de la página
//-------------------------------------------------------------------------------------------------------------------
function insStateZone(){}
//-------------------------------------------------------------------------------------------------------------------

//% insPreZone: Se maneja la Acción para la Busqueda por Condición
//-------------------------------------------------------------------------------------------------------------------
function insPreZone(llngAction){
//-------------------------------------------------------------------------------------------------------------------
	
	switch (llngAction){
	    case 301:
	    case 302:
	    case 401:
	        document.location.href = document.location.href.replace(/&nMainAction.*/,'') + '&nMainAction=' + llngAction
	        break;
	}
}

//% insShowHeader: Refresh del Header
//-------------------------------------------------------------------------------------------------------------------
function insShowHeader(){
//-------------------------------------------------------------------------------------------------------------------
    var lblnContinue=true
    if (typeof(top.fraHeader.document)!='undefined') {
	    if (typeof(top.fraHeader.document.forms[0])!='undefined') {
			if (typeof(top.fraHeader.document.forms[0].valCurrency)!='undefined'){
				top.fraHeader.document.forms[0].valCurrency.value= '<%=Session("nCurrency")%>'
				lblnContinue = false
			}
		}
	}
    if (lblnContinue)
		setTimeout("insShowHeader()",50);
}
//% ControlNextBack: Se encarga de amumentar o disminuir la consulta de los registros
//-------------------------------------------------------------------------------------------
function ControlNextBack(Option){
//-------------------------------------------------------------------------------------------
    var lstrURL = self.document.location.href
    var llngRow = lstrURL.substr(lstrURL.indexOf("&nRow=") + 6)
    lstrURL = lstrURL.replace(/&nRow=.*/,'')
	switch(Option){
		case "Next":
			if(isNaN(llngRow))
				lstrURL = lstrURL + "&nRow=51"
			else{
				llngRow = insConvertNumber(llngRow) + 50;
				lstrURL = lstrURL + "&nRow=" + llngRow
			}
			break;

		case "Back":
			if(!isNaN(llngRow)){
				llngRow = insConvertNumber(llngRow) - 50;
				lstrURL = lstrURL + "&nRow=" + llngRow
			}
	}
	self.document.location.href = lstrURL;
}
</SCRIPT>

</HEAD>

<BODY ONUNLOAD="closeWindows();">

	<FORM METHOD="post" ID="FORM" NAME="MS004" ACTION="valMantSys.aspx?Mode=2&nMainAction=<%=Request.QueryString.Item("nMainAction")%>">
<%
Response.Write(mobjValues.ShowWindowsName("MS004"))
Call insDefineHeader()
If Request.QueryString.Item("Type") <> "PopUp" Then
	Call insPreMS004()
	Response.Write(mobjValues.AnimatedButtonControl("cmdBack", "/VTimeNet/Images/btnLargeBackOff.png", GetLocalResourceObject("cmdBackToolTip"),  , "ControlNextBack('Back')", CDbl(Request.QueryString.Item("nRow")) <= 1 Or IsNothing(Request.QueryString.Item("nRow"))))
	Response.Write(mobjValues.AnimatedButtonControl("cmdNext", "/VTimeNet/Images/btnLargeNextOff.png", GetLocalResourceObject("cmdNextToolTip"),  , "ControlNextBack('Next')", mlngTotalRow <> 50))
Else
	Call insPreMS004Upd()
End If
mobjGrid = Nothing
%>
	</FORM>
</BODY>
</HTML>
<%
mobjValues = Nothing
%>





