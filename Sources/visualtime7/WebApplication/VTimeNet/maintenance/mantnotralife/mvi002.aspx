<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="ePolicy" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 31/3/03 19.42.04
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues
Dim mobjGrid As eFunctions.Grid



'% insDefineHeader: Configura los datos del grid.
'%--------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'%--------------------------------------------------------------------------------------
	
	mobjGrid = New eFunctions.Grid
	
	'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.42.04
	mobjGrid.sSessionID = Session.SessionID
	mobjGrid.nUsercode = Session("nUsercode")
	'~End Body Block VisualTimer Utility
	
	mobjGrid.sCodisplPage = "MVI002"
	
	'+ Se definen las columnas del grid.
	
	With mobjGrid.Columns
		Call .AddPossiblesColumn(0, GetLocalResourceObject("tcnFundColumnCaption"), "tcnFund", "tabFund_inv", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  , "insUpdateUnits(this)",  ,  , GetLocalResourceObject("tcnFundColumnToolTip"), True, 6,  ,  ,  , True)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnAmountColumnCaption"), "tcnAmount", 18, "",  , GetLocalResourceObject("tcnAmountColumnToolTip"), True, 6)
		Call .AddPossiblesColumn(0, GetLocalResourceObject("tcnCurrencyColumnCaption"), "tcnCurrency", "Table11", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("tcnCurrencyColumnToolTip"))
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnQuan_availColumnCaption"), "tcnQuan_avail", 14, "",  , GetLocalResourceObject("tcnQuan_availColumnToolTip"), True, 5,  ,  ,  , True)
		Call .AddDateColumn(0, GetLocalResourceObject("tcdFundDateColumnCaption"), "tcdFundDate",  ,  , GetLocalResourceObject("tcdFundDateColumnToolTip"),  ,  ,  , True)
	End With
	
	'+ Se definen las propiedades generales del grid.
	With mobjGrid
		.Codispl = "MVI002"
		.sCodisplPage = "MVI002"
		.Columns("Sel").GridVisible = False
		.Height = 300
		.Width = 350
		.nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
		.Columns("tcnFund").EditRecord = True
		.Columns("tcnFund").disabled = True
		.AddButton = False
		.DeleteButton = False
		
		'+ Permite continuar si el check está marcado.
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
End Sub

'% insPreMVI002: Obtiene los datos de los fondos de inversión.
'%--------------------------------------------------------------------------------------
Private Sub insPreMVI002()
	'%--------------------------------------------------------------------------------------
	Dim mobjFund_value As ePolicy.Fund_value
	Dim mobjFund_values As ePolicy.Fund_values
	
	mobjFund_value = New ePolicy.Fund_value
	mobjFund_values = New ePolicy.Fund_values
	
	'+ Se buscan los fondos de inversión asociados al plan siempre y cuando la acción sea
	'+ diferente a una inserción.
	
	If mobjFund_values.FindFounds(mobjValues.StringToDate(Session("dEffecdate"))) Then
		With mobjGrid
			For	Each mobjFund_value In mobjFund_values
				'+ Descripción del fondo.
				.Columns("tcnFund").DefValue = CStr(mobjFund_value.nFunds)
				'+ Valor.
				.Columns("tcnAmount").DefValue = CStr(mobjFund_value.nAmount)
				'+ Moneda.
				.Columns("tcnCurrency").DefValue = CStr(mobjFund_value.nCurrency)
				'+ Unidades.
				.Columns("tcnQuan_avail").DefValue = CStr(mobjFund_value.nQuan_avail)
				'+ Fecha del Fondo               
				.Columns("tcdFundDate").DefValue = CStr(mobjFund_value.dEffecDate)
				
				Response.Write(.DoRow)
			Next mobjFund_value
		End With
	End If
	
	Response.Write(mobjGrid.closeTable)
	
	mobjFund_value = Nothing
	mobjFund_values = Nothing
End Sub

'% insPreMVI002Upd: Muestra la ventana Popup para las actualizaciones.
'%--------------------------------------------------------------------------------------
Private Function insPreMVI002Upd() As Object
	'%------------------------------------------------------------------------
	Response.Write(mobjGrid.DoFormUpd(Request.QueryString.Item("Action"), "valMantNoTraLife.aspx", "MVI002", Request.QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(Request.QueryString.Item("Index"))))
End Function

</script>
<%Response.Expires = -1

Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("CA021")

mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues
mobjGrid = New eFunctions.Grid

If CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401 Then
	mobjGrid.ActionQuery = True
	mobjValues.ActionQuery = True
End If
mobjValues.sCodisplPage = "MVI002"
%>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>



<SCRIPT LANGUAGE="JavaScript">
    var nMainAction = <%=Request.QueryString.Item("nMainAction")%>;

//+ Para Control de Versiones "NO REMOVER"
//------------------------------------------------------------------------------
	document.VssVersion="$$Revision: 1 $|$$Date: 2/09/03 19:03 $"
//------------------------------------------------------------------------------

//% insCancel: Esta función ejecuta la acción Cancelar de la página.
//------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------
	return true
}
//% insUpdateUnits: Obtiene la cantidad de unidades según el tipo de fondo.
//--------------------------------------------------------------------------------------
function insUpdateUnits(lobj){
//--------------------------------------------------------------------------------------
//   lstrQueryString = "/VTimeNet/Maintenance/MantNoTraLife/ShowDefValues.aspx?Field=Funds";
//   lstrQueryString = ;
//   ShowPopUp(lstrQueryString,"Values",1,1,"no","no", 2000, 2000);

   InsDefValues("Funds","nFunds=" + lobj.value);
}
//% insCalculate: Función utilizada para calcular los importes de la ventana.
//--------------------------------------------------------------------------------------------
function insCalculate(){
//--------------------------------------------------------------------------------------------
	var nAmount=0;

    nAmount = insConvertNumber(self.document.forms[0].tcnAmount.value);
	self.document.forms[0].tcnAmount.value = VTFormat((nAmount * 1), "", "", "", 6, true) 
}
</SCRIPT>    
        <%With Response
	.Write(mobjValues.StyleSheet() & vbCrLf)
	
	If Request.QueryString.Item("Type") <> "PopUp" Then
		.Write(mobjMenu.setZone(2, "MVI002", "MVI002.aspx"))
	End If
End With

mobjMenu = Nothing%>
    </HEAD>
    <BODY ONUNLOAD="closeWindows();">
        <FORM METHOD="post" ID="FORM" NAME="frmDBVehicle" ACTION="valMantNoTraLife.aspx?mode=1&nMainAction=<%=Request.QueryString.Item("nMainAction")%>">
            <%=mobjValues.ShowWindowsName("MVI002")%>
            <BR>
            <%
Call insDefineHeader()

If Request.QueryString.Item("Type") <> "PopUp" Then
	Call insPreMVI002()
Else
	Call insPreMVI002Upd()
End If

mobjValues = Nothing
mobjGrid = Nothing
%>
        </FORM>
    </BODY>
<%If Request.QueryString.Item("Type") = "PopUp" Then%>
    <SCRIPT>insCalculate();</SCRIPT>
<%End If%>    
</HTML>
<%
'^Begin Footer Block VisualTimer Utility 1.1 31/3/03 19.42.04
Call mobjNetFrameWork.FinishPage("CA021")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer

%>





