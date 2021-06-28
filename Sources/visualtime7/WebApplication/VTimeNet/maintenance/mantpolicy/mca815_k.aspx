<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eGeneral" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="ePolicy" %>
<script language="VB" runat="Server">

'----------------------------------------------------------------------------------------
'- Ventana Masiva.  Causas del estado pendiente de la poliza/certificado 
'----------------------------------------------------------------------------------------

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo del grid de la página
Dim mobjGrid As eFunctions.Grid

'- Objeto para el manejo del menú
Dim mobjMenu As eFunctions.Menues

'- Objeto i variables para el error cuando se intenta eliminar un código
'  que está a una póliza/certificado
Dim lobjErrors As eGeneral.GeneralFunction
Dim mstrAlert As String

'- Objeto para el manejo particular de los datos de la página
Dim mcolNoconverss As ePolicy.Noconverss


'% insDefineHeader: se definen las propiedades del grid
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	
	mobjGrid.sCodisplPage = "mca815_k"
	
	'+ Se definen las columnas del grid 
	With mobjGrid.Columns
		If Request.QueryString.Item("Action") = "Add" Then
			Call .AddNumericColumn(0, GetLocalResourceObject("tcnNo_conversColumnCaption"), "tcnNo_convers", 5, vbNullString,  , GetLocalResourceObject("tcnNo_conversColumnToolTip"),  ,  ,  ,  ,  , False)
		Else
			Call .AddNumericColumn(0, GetLocalResourceObject("tcnNo_conversColumnCaption"), "tcnNo_convers", 5, vbNullString,  , GetLocalResourceObject("tcnNo_conversColumnToolTip"),  ,  ,  ,  ,  , True)
		End If
		Call .AddTextColumn(0, GetLocalResourceObject("tctDescriptColumnCaption"), "tctDescript", 30, vbNullString,  , GetLocalResourceObject("tctDescriptColumnToolTip"))
		Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeAreaWaitColumnCaption"), "cbeAreaWait", "Table5603", eFunctions.Values.eValuesType.clngComboType, CStr(0),  ,  ,  ,  ,  ,  , 2, GetLocalResourceObject("cbeAreaWaitColumnToolTip"))
		Call .AddCheckColumn(0, GetLocalResourceObject("chksDevoColumnCaption"), "chksDevo", vbNullString, CShort("1"),  ,  , Request.QueryString.Item("Type") <> "PopUp")
		Call .AddCheckColumn(0, GetLocalResourceObject("chksDiscColumnCaption"), "chksDisc", vbNullString, CShort("1"),  ,  , Request.QueryString.Item("Type") <> "PopUp")
		Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeStatregtColumnCaption"), "cbeStatregt", "Table26", eFunctions.Values.eValuesType.clngComboType, CStr(1),  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeStatregtColumnToolTip"))
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnGastAdmColumnCaption"), "tcnGastAdm", 18, vbNullString,  , GetLocalResourceObject("tcnGastAdmColumnToolTip"),  , 6,  ,  ,  , False)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnGastMedColumnCaption"), "tcnGastMed", 18, vbNullString,  , GetLocalResourceObject("tcnGastMedColumnToolTip"),  , 6,  ,  ,  , False)
		Call .AddTextColumn(0, GetLocalResourceObject("tctRutineColumnCaption"), "tctRutine", 12, vbNullString,  , GetLocalResourceObject("tctRutineColumnToolTip"))
	End With
	
	'+ Se definen las propiedades generales del grid 
	With mobjGrid
		.Codispl = "MCA815"
		.Codisp = "MCA815"
		If Request.QueryString.Item("nMainAction") = "401" Or Request.QueryString.Item("nMainAction") = vbNullString Then
			mobjGrid.ActionQuery = True
			mobjGrid.Columns("Sel").GridVisible = False
		Else
			.Columns("tctDescript").EditRecord = True
		End If
		
		.Height = 350
		.Width = 500
		.Top = 100
		.WidthDelete = 500
		
		'+ parámetros para eliminación
		.sDelRecordParam = "nNo_convers='+ marrArray[lintIndex].tcnNo_convers + '"
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
End Sub

'% insPreMCA815: se realiza el manejo del grid
'--------------------------------------------------------------------------------------------
Private Sub insPreMCA815()
	'--------------------------------------------------------------------------------------------
	
	Dim lstrInd As String
	Dim mclsNoconvers As ePolicy.Noconvers
	mclsNoconvers = New ePolicy.Noconvers
	mcolNoconverss = New ePolicy.Noconverss
	
	If mcolNoconverss.Find(mobjValues.StringToType(vbNullString, eFunctions.Values.eTypeData.etdDouble)) Then
		For	Each mclsNoconvers In mcolNoconverss
			With mobjGrid
				.Columns("tcnNo_convers").DefValue = CStr(mclsNoconvers.nNo_convers)
				lstrInd = "0"
				If mclsNoconvers.Find_Noconvers(mclsNoconvers.nNo_convers) Then
					lstrInd = "1"
				End If
				.Columns("Sel").OnClick = "InsChangeSel(this," & lstrInd & ");"
				.Columns("tctDescript").DefValue = mclsNoconvers.sDescript
				.Columns("cbeAreaWait").DefValue = CStr(mclsNoconvers.nAreaWait)
				
				If mclsNoconvers.sDevo = "1" Then
					.Columns("chksDevo").Checked = CShort("1")
				Else
					.Columns("chksDevo").Checked = False
				End If
				
				If mclsNoconvers.sDisc = "1" Then
					.Columns("chksDisc").Checked = CShort("1")
				Else
					.Columns("chksDisc").Checked = False
				End If
				
				.Columns("cbeStatregt").DefValue = mclsNoconvers.sStatregt
				.Columns("tcnGastAdm").DefValue = CStr(mclsNoconvers.nExpenses)
				.Columns("tcnGastMed").DefValue = CStr(mclsNoconvers.nHealthexp)
				.Columns("tctRutine").DefValue = mclsNoconvers.sRoutine
				Response.Write(.DoRow)
			End With
		Next mclsNoconvers
	End If
	Response.Write(mobjGrid.closeTable())
End Sub

'% insPreMCA815Upd: Se realiza el manejo de la ventana PopUp asociada al grid
'--------------------------------------------------------------------------------------------
Private Sub insPreMCA815Upd()
	'--------------------------------------------------------------------------------------------
	'- Objeto para procesar eliminacion de registro
	Dim lobjNoconvers As ePolicy.Noconvers
	
	lobjNoconvers = New ePolicy.Noconvers
	
	With Request
		If .QueryString.Item("Action") = "Del" Then
			Response.Write(mobjValues.ConfirmDelete())
			If lobjNoconvers.insPostMCA815(Request.QueryString.Item("Action"), mobjValues.StringToType(Request.QueryString.Item("nNo_convers"), eFunctions.Values.eTypeData.etdDouble), Request.Form.Item("tctDescript"), mobjValues.StringToType(Request.Form.Item("cbeAreaWait"), eFunctions.Values.eTypeData.etdDouble), Request.Form.Item("chksDevo"), Request.Form.Item("chksDisc"), Request.Form.Item("cbeStatregt"), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble)) Then
			End If
		End If
		Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "valMantPolicy.aspx", "MCA815", .QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(.QueryString.Item("Index"))))
	End With
End Sub

</script>
<%Response.Expires = -1
lobjErrors = New eGeneral.GeneralFunction
mstrAlert = "Err. 55873 " & lobjErrors.insLoadMessage(55873)
lobjErrors = Nothing

mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues

mobjValues.sCodisplPage = "mca815_k"
%>
<HTML>
<HEAD>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>


<SCRIPT LANGUAGE=JavaScript>
//- Variable para el control de versiones
	document.VssVersion="$$Revision: 2 $|$$Date: 16/09/09 5:32p $|$$Author: Gletelier $"

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
		.Write(mobjMenu.MakeMenu(Request.QueryString.Item("sCodispl"), "MCA815_K.aspx", 1, ""))
		Response.Write("<BR></BR>")
		mobjMenu = Nothing
	End If
End With
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" NAME="MCA815" ACTION="valMantPolicy.aspx?sMode=2">
<%
Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl")))
Call insDefineHeader()
If Request.QueryString.Item("Type") = "PopUp" Then
	Call insPreMCA815Upd()
Else
	Call insPreMCA815()
End If
mobjValues = Nothing
mobjGrid = Nothing
%>
</FORM>
<%="<SCRIPT>"%>
//InsChangeSel: Función encargada de enviar mensaje de validación para cuando no se pueda eliminar un registro del grid
//------------------------------------------------------------------------
function InsChangeSel(Field, sIndNoconvers){
//------------------------------------------------------------------------
	if (Field.checked && sIndNoconvers == "1") {
		alert('<%=mstrAlert%>');
		Field.checked = false
	}
}
<%="</SCRIPT>"%>
</BODY>
</HTML>






