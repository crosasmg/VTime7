<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eBranches" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo del grid de la página
Dim mobjGrid As eFunctions.Grid

'- Objeto para el manejo del menú
Dim mobjMenu As eFunctions.Menues

Dim mlngFromFunds As Object
Dim mlngOrigin As Object


'% insDefineHeader: se definen las propiedades del grid
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	
	'+ Se definen las columnas del grid 
	With mobjGrid.Columns
		.AddPossiblesColumn(0, GetLocalResourceObject("cbeOriginColumnCaption"), "cbeOrigin", "table5633", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  , (mobjValues.Stringtotype(Request.QueryString.Item("Update"), Values.eTypeData.etdBoolean)),  , GetLocalResourceObject("cbeOriginColumnCaption"))
		If Request.QueryString.Item("Type") = "PopUp" Then
			.AddPossiblesColumn(0, GetLocalResourceObject("valFromFundsColumnCaption"), "valFromFunds", "tabFund_inv", eFunctions.Values.eValuesType.clngWindowType,  ,  ,  ,  ,  ,  , (mobjValues.Stringtotype(Request.QueryString.Item("Update"), Values.eTypeData.etdBoolean)),  , GetLocalResourceObject("valFromFundsColumnToolTip"))
			.AddPossiblesColumn(0, GetLocalResourceObject("valToFundsColumnCaption"), "valToFunds", "tabFund_inv", eFunctions.Values.eValuesType.clngWindowType,  ,  ,  ,  ,  ,  , (mobjValues.Stringtotype(Request.QueryString.Item("Update"), Values.eTypeData.etdBoolean)),  , GetLocalResourceObject("valToFundsColumnToolTip"))
		Else
			.AddNumericColumn(0, GetLocalResourceObject("valFromFundsColumnCaption"), "valFromFunds", 5,  ,  , GetLocalResourceObject("valFromFundsColumnCaption"))
			.AddTextColumn(0, GetLocalResourceObject("tctFromFundsColumnCaption"), "tctFromFunds", 30, "",  , GetLocalResourceObject("tctFromFundsColumnToolTip"))
			.AddNumericColumn(0, GetLocalResourceObject("valToFundsColumnCaption"), "valToFunds", 5,  ,  , GetLocalResourceObject("valToFundsColumnCaption"))
			.AddTextColumn(0, GetLocalResourceObject("tctToFundsColumnCaption"), "tctToFunds", 30, "",  , GetLocalResourceObject("tctToFundsColumnToolTip"))
		End If
		.AddPossiblesColumn(0, GetLocalResourceObject("cbeStatregtColumnCaption"), "cbeStatregt", "Table26", eFunctions.Values.eValuesType.clngComboType, CStr(1),  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeStatregtColumnToolTip"))
	End With
	
	'+ Se definen las propiedades generales del Grid 
	With mobjGrid
		.Codispl = "MVI817"
		.Codisp = "MVI817"
		.sCodisplPage = "MVI817"
		If Request.QueryString.Item("nMainAction") = "401" Or Request.QueryString.Item("nMainAction") = vbNullString Then
			mobjGrid.ActionQuery = True
			mobjGrid.Columns("Sel").GridVisible = False
		Else
			If Request.QueryString.Item("Type") <> "PopUp" Then
				.Columns("cbeOrigin").EditRecord = True
				.Columns("valFromFunds").EditRecord = True
				.Columns("tctFromFunds").EditRecord = True
			End If
		End If
		mobjGrid.Columns("cbeStatregt").BlankPosition = False
		.Height = 240
		.Width = 450
		.WidthDelete = 500
		
		'+ Parámetros para eliminación
		.sDelRecordParam = "nOrigin='+ marrArray[lintIndex].cbeOrigin + '&nFromFunds='+ marrArray[lintIndex].valFromFunds + '&nToFunds='+ marrArray[lintIndex].valToFunds + '&cbeFromFunds=' + " & mlngFromFunds & " + '&cbeOriginH=' + " & mlngOrigin & " +'"
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
End Sub

'% insPreMVI817: se realiza el manejo del Grid
'--------------------------------------------------------------------------------------------
Private Sub insPreMVI817()
	'--------------------------------------------------------------------------------------------
	
Response.Write("" & vbCrLf)
Response.Write("<br>" & vbCrLf)
Response.Write("<table width=""50%"">" & vbCrLf)
Response.Write("	<tr>" & vbCrLf)
Response.Write("		<td><LABEL ID=0>" & GetLocalResourceObject("cbeOriginHCaption") & "</LABEL><td>" & vbCrLf)
Response.Write("		<td>" & vbCrLf)
Response.Write("		")

	
	mobjValues.BlankPosition = False
	Response.Write(mobjValues.PossiblesValues("cbeOriginH", "table5633", eFunctions.Values.eValuesType.clngComboType, mlngOrigin,  ,  ,  ,  ,  , "insChangeHeader();",  ,  , GetLocalResourceObject("cbeOriginHToolTip")))
	
Response.Write("" & vbCrLf)
Response.Write("		<td>" & vbCrLf)
Response.Write("		<td><LABEL ID=0>" & GetLocalResourceObject("cbeFromFundsCaption") & "</LABEL><td>" & vbCrLf)
Response.Write("		<td>" & vbCrLf)
Response.Write("		")

	
	mobjValues.BlankPosition = False
	Response.Write(mobjValues.PossiblesValues("cbeFromFunds", "tabFund_inv", eFunctions.Values.eValuesType.clngComboType, mlngFromFunds,  ,  ,  ,  ,  , "insChangeHeader();",  ,  , GetLocalResourceObject("cbeFromFundsToolTip")))
	
Response.Write("" & vbCrLf)
Response.Write("		<td>" & vbCrLf)
Response.Write("	</tr>" & vbCrLf)
Response.Write("<table>")

	
	Dim mclsFunds_Switch As eBranches.Funds_Switch
	Dim mcolFunds_Switchs As eBranches.Funds_Switchs
	
	mclsFunds_Switch = New eBranches.Funds_Switch
	mcolFunds_Switchs = New eBranches.Funds_Switchs
	If mcolFunds_Switchs.Find(mlngOrigin, mlngFromFunds) Then
		For	Each mclsFunds_Switch In mcolFunds_Switchs
			With mobjGrid
				.Columns("cbeOrigin").DefValue = CStr(mclsFunds_Switch.nOrigin)
				.Columns("valFromFunds").DefValue = CStr(mclsFunds_Switch.nFromFunds)
				.Columns("tctFromFunds").DefValue = mclsFunds_Switch.sFromFunds
				.Columns("valToFunds").DefValue = CStr(mclsFunds_Switch.nToFunds)
				.Columns("tctToFunds").DefValue = mclsFunds_Switch.sToFunds
				.Columns("cbeStatregt").DefValue = CStr(mclsFunds_Switch.nStatregt)
				Response.Write(.DoRow)
			End With
		Next mclsFunds_Switch
	End If
	Response.Write(mobjGrid.closeTable())
	mcolFunds_Switchs = Nothing
	mclsFunds_Switch = Nothing
	
End Sub

'% insPreMVI817Upd: Se realiza el manejo de la ventana PopUp asociada al grid
'--------------------------------------------------------------------------------------------
Private Sub insPreMVI817Upd()
	'--------------------------------------------------------------------------------------------
	'- Objeto para procesar eliminacion de registro
	Dim lobjFunds_Switch As eBranches.Funds_Switch
	
	lobjFunds_Switch = New eBranches.Funds_Switch
	
	With Request
		If .QueryString.Item("Action") = "Del" Then
			Response.Write(mobjValues.ConfirmDelete())
			If lobjFunds_Switch.insPostMVI817Upd(Request.QueryString.Item("Action"), mobjValues.StringToType(Request.QueryString.Item("nOrigin"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(Request.QueryString.Item("nFromFunds"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(Request.QueryString.Item("nToFunds"), eFunctions.Values.eTypeData.etdLong), eRemoteDB.Constants.intNull, mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdLong)) Then
			End If
		End If
		Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "valMantLife.aspx", "MVI817", .QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(.QueryString.Item("Index"))))
	End With
	lobjFunds_Switch = Nothing
End Sub

</script>
<%Response.Expires = -1

'- Objeto para el manejo particular de los datos de la página
mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues

mobjValues.sCodisplPage = "MVI817"

mlngOrigin = mobjValues.StringToType(Request.QueryString.Item("cbeOriginH"), eFunctions.Values.eTypeData.etdLong)
If mlngOrigin = eRemoteDB.Constants.intNull Then
	mlngOrigin = 1
End If

mlngFromFunds = mobjValues.StringToType(Request.QueryString.Item("cbeFromFunds"), eFunctions.Values.eTypeData.etdLong)
If mlngFromFunds = eRemoteDB.Constants.intNull Then
	mlngFromFunds = 1
End If
%>
<HTML>
<HEAD>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>


<SCRIPT LANGUAGE=JavaScript>
//- Variable para el control de versiones
	document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 16:06 $|$$Author: Nvaplat61 $"

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

//% insPreZone: Define ubicacion de documento
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

//% insFinish: se controla la acción Cancelar de la página
//--------------------------------------------------------------------------------------------
function insFinish(){
//--------------------------------------------------------------------------------------------
    return true;
}

//% insChangeHeader:
//--------------------------------------------------------------------------------------------
function insChangeHeader(){
//--------------------------------------------------------------------------------------------

	with (self.document.forms[0])
	{
		document.location.href = document.location.href.replace(/&cbeOriginH.*/,'') + '&cbeOriginH=' + cbeOriginH.value +
		                         '&cbeFromFunds=' + cbeFromFunds.value +
		                         '&nMainAction=' + '<%=Request.QueryString.Item("nMainAction")%>'
	}
}

</SCRIPT>
<%
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write("<SCRIPT>var nMainAction=0</SCRIPT>")
	Response.Write("<SCRIPT LANGUAGE=""JavaScript"" SRC=""/VTimeNet/Scripts/tmenu.js""></SCRIPT>" & vbCrLf)
End If
With Response
	.Write(mobjValues.StyleSheet() & vbCrLf)
	If Request.QueryString.Item("Type") <> "PopUp" Then
		mobjMenu = New eFunctions.Menues
		.Write(mobjMenu.MakeMenu(Request.QueryString.Item("sCodispl"), "MVI817_K.aspx", 1, ""))
		Response.Write("<BR></BR>")
		mobjMenu = Nothing
	End If
End With
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" NAME="MVI817" ACTION="valMantLife.aspx?sMode=2">
<%
Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl")))
Call insDefineHeader()
If Request.QueryString.Item("Type") = "PopUp" Then
	Call insPreMVI817Upd()
Else
	Call insPreMVI817()
End If
mobjValues = Nothing
mobjGrid = Nothing
%>
</FORM>
</BODY>
</HTML>





