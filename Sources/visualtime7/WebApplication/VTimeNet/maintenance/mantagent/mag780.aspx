<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eAgent" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores

Dim mobjValues As eFunctions.Values

'- Objeto para el manejo de las rutinas genéricas

Dim mobjGrid As eFunctions.Grid

'- Objeto para el manejo de las zonas de la pantalla
Dim mobjMenu As eFunctions.Menues


'% insDefineHeader: Se definen los campos del grid
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	
	'+Se definen las columns del Grid
	
	With mobjGrid.Columns
		Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeModalityColumnCaption"), "cbeModality", "Table5601", eFunctions.Values.eValuesType.clngComboType, CStr(eRemoteDB.Constants.strNull),  ,  ,  ,  ,  , Request.QueryString.Item("Action") = "Update", 5, GetLocalResourceObject("cbeModalityColumnToolTip"))
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnPercentminColumnCaption"), "tcnPercentmin", 5, CStr(eRemoteDB.Constants.strNull), True, GetLocalResourceObject("tcnPercentminColumnToolTip"), True, 2)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnPercentmaxColumnCaption"), "tcnPercentmax", 5, CStr(eRemoteDB.Constants.strNull), True, GetLocalResourceObject("tcnPercentmaxColumnToolTip"), True, 2)
	End With
	
	'+Se asignan las caracteristicas del Grid
	
	With mobjGrid
		'+Se crean los parametros para las listas de valores posibles
		
		.Columns("cbeModality").EditRecord = True
		.Codispl = "MAG780"
		.Codisp = "MAG780"
		.sCodisplPage = "MAG780"
		
		If Request.QueryString.Item("nMainAction") = "401" Or Request.QueryString.Item("nMainAction") = vbNullString Then
			mobjGrid.ActionQuery = True
			mobjGrid.Columns("Sel").GridVisible = False
		End If
		
		.sEditRecordParam = "nIntermtyp=" & Request.QueryString.Item("nIntermtyp")
		
		'+Pase de parametros necesarios para la eliminación de registros
		.sDelRecordParam = "nIntermtyp=" & Request.QueryString.Item("nIntermtyp") & "&nModality='+ marrArray[lintIndex].cbeModality + '"
		
		.Height = 200
		.Width = 420
		
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
	
End Sub

'+ insPreMAG780: Carga de los registros de la tabla interm_typ en el grid
'------------------------------------------------------------------------------
Private Sub insPreMAG780()
	'------------------------------------------------------------------------------
	Dim lcolPercentAdvances As eAgent.PercentAdvances
	Dim lclsPercentAdvanc As Object
	
	lcolPercentAdvances = New eAgent.PercentAdvances
	If lcolPercentAdvances.Find(mobjValues.StringToType(Request.QueryString.Item("nIntermtyp"), eFunctions.Values.eTypeData.etdDouble)) Then
		For	Each lclsPercentAdvanc In lcolPercentAdvances
			With mobjGrid
				.Columns("cbeModality").DefValue = lclsPercentAdvanc.nCodModPay
				.Columns("tcnPercentmin").DefValue = lclsPercentAdvanc.nPercent_init
				.Columns("tcnPercentmax").DefValue = lclsPercentAdvanc.nPercent_end
			End With
			
			'+Se ejecuta el metodo DoRow, que se encarga de mostrar los elementos de grid
			
			Response.Write(mobjGrid.DoRow())
		Next lclsPercentAdvanc
	End If
	Response.Write(mobjGrid.closeTable())
	lclsPercentAdvanc = Nothing
	lcolPercentAdvances = Nothing
End Sub

'+ insPreMAG780Upd: Actualización de la tabla interm_typ
'------------------------------------------------------------------------------
Private Sub insPreMAG780Upd()
	'------------------------------------------------------------------------------
	Dim lclsPercentAdvanc As eAgent.PercentAdvanc
	
	If Request.QueryString.Item("Action") = "Del" Then
		
		lclsPercentAdvanc = New eAgent.PercentAdvanc
		
		With lclsPercentAdvanc
			.nIntermtyp = mobjValues.StringToType(Request.QueryString.Item("nIntermtyp"), eFunctions.Values.eTypeData.etdDouble, True)
			.nCodModPay = mobjValues.StringToType(Request.QueryString.Item("nModality"), eFunctions.Values.eTypeData.etdDouble, True)
			If .insUpdPercentAdvanc(3) Then
				Response.Write(mobjValues.ConfirmDelete())
			End If
		End With
		
		lclsPercentAdvanc = Nothing
	End If
	
	With Response
		.Write(mobjGrid.DoFormUpd(Request.QueryString.Item("Action"), "valMantAgent.aspx", Request.QueryString.Item("sCodispl"), Request.QueryString.Item("MainAction"),  , CShort(Request.QueryString.Item("Index"))))
		.Write(mobjValues.HiddenControl("hddnIntermtyp", mobjValues.StringToType(Request.QueryString.Item("nIntermtyp"), eFunctions.Values.eTypeData.etdDouble)))
	End With
End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values

mobjValues.sCodisplPage = "MAG780"
%>
<HTML>
<HEAD>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>


<SCRIPT>
	var nMainAction = 304;
//+ Variable para el control de versiones
    document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 15:35 $|$$Author: Nvaplat61 $"
    
</SCRIPT>    
<%
With Response
	.Write(mobjValues.StyleSheet())
	If Request.QueryString.Item("Type") <> "PopUp" Then
		mobjMenu = New eFunctions.Menues
		.Write(mobjMenu.setZone(2, "MAG780", "MAG780"))
		mobjMenu = Nothing
	End If
End With
%>
<SCRIPT>

//% insCancel: Se activa al cancelar la transacción
//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
	return true;
}

//% insFinish: Se activa al finalizar la transacción
//------------------------------------------------------------------------------------------
function insFinish(){
//------------------------------------------------------------------------------------------
	return true;
}

</SCRIPT>    
</HEAD>
<BODY ONUNLOAD="closeWindows();">

<FORM METHOD="POST" ID="FORM" NAME="frmTabEcoSche" ACTION="valMantAgent.aspx?mode=1">
<%
Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl")))
Call insDefineHeader()
If Request.QueryString.Item("Type") <> "PopUp" Then
	Call insPreMAG780()
Else
	Call insPreMAG780Upd()
End If
mobjValues = Nothing
mobjGrid = Nothing
%>
</FORM>
</BODY>
</HTML>





