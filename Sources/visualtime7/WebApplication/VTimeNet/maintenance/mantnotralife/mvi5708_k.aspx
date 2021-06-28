<%@ Page Language="VB" explicit="true" %>
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
Dim mcolTable5708s As ePolicy.Table5708s


'% insDefineHeader: se definen las propiedades del grid
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	
	mobjGrid.sCodisplPage = "MVI5708_k"
	
	'+ Se definen las columnas del grid 
	With mobjGrid.Columns
		If Request.QueryString("Action") = "Add" Then
			Call .AddNumericColumn(0, "Código", "tcnType_Move", 5, vbNullString,  , "Código del tipo de movimiento de cuenta corriente",  ,  ,  ,  ,  , False)
		Else
			Call .AddNumericColumn(0, "Código", "tcnType_Move", 5, vbNullString,  , "Código del tipo de movimiento de cuenta corriente",  ,  ,  ,  ,  , True)
		End If
		Call .AddTextColumn(0, "Descripción", "tctDescript", 30, vbNullString,  , "Descripción Completa")
		Call .AddTextColumn(0, "Descripción corta", "tctShort_Des", 12, vbNullString,  , "Descripción Completa")
		Call .AddPossiblesColumn(0, "Tipo", "cbeType", "Table5713", eFunctions.Values.eValuesType.clngComboType, CStr(0),  ,  ,  ,  ,  ,  , 2, "Tipo de movimiento")
		Call .AddCheckColumn(0, "Afecta prima básica", "chkPb_Bmg", vbNullString, CShort("1"),  ,  , Request.QueryString("Type") <> "PopUp")
		Call .AddPossiblesColumn(0, "Estado", "cbeStatregt", "Table26", eFunctions.Values.eValuesType.clngComboType, CStr(1),  ,  ,  ,  ,  ,  ,  , "Estado del registro")
	End With
	
	'+ Se definen las propiedades generales del grid 
	With mobjGrid
		.Codispl = "MVI5708"
		.Codisp = "MVI5708"
		If Request.QueryString("nMainAction") = "401" Or Request.QueryString("nMainAction") = vbNullString Then
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
		.sDelRecordParam = "nType_Move='+ marrArray[lintIndex].tcnType_Move + '"
		If Request.QueryString("Reload") = "1" Then
			.sReloadIndex = Request.QueryString("ReloadIndex")
		End If
	End With
End Sub

'% insPreMVI5708: se realiza el manejo del grid
'--------------------------------------------------------------------------------------------
Private Sub insPreMVI5708()
	'--------------------------------------------------------------------------------------------
	
	Dim lstrInd As String
	Dim mclsTable5708 As ePolicy.Table5708
	mclsTable5708 = New ePolicy.Table5708
	mcolTable5708s = New ePolicy.Table5708s
	
	If mcolTable5708s.Find(mobjValues.StringToType(vbNullString, eFunctions.Values.eTypeData.etdDouble)) Then
		For	Each mclsTable5708 In mcolTable5708s
			With mobjGrid
				.Columns("tcnType_Move").DefValue = CStr(mclsTable5708.nType_Move)
				lstrInd = "0"
				
				If mclsTable5708.Find_Type_Move(mclsTable5708.nType_Move) Then
					lstrInd = "1"
				End If
				
				'.Columns("Sel").OnClick = "InsChangeSel(this," & lstrInd & ");"
				.Columns("tctDescript").DefValue = mclsTable5708.sDescript
				.Columns("tctShort_Des").DefValue = mclsTable5708.sShort_des
				.Columns("cbeType").DefValue = CStr(mclsTable5708.nType)
				
				If mclsTable5708.sPb_Bmg = "1" Then
					.Columns("chkPb_Bmg").Checked = CShort("1")
				Else
					.Columns("chkPb_Bmg").Checked = False
				End If
				
				.Columns("cbeStatregt").DefValue = mclsTable5708.sStatregt
				Response.Write(.DoRow)
			End With
		Next mclsTable5708
	End If
	Response.Write(mobjGrid.closeTable())
End Sub

'% insPreMVI5708Upd: Se realiza el manejo de la ventana PopUp asociada al grid
'--------------------------------------------------------------------------------------------
Private Sub insPreMVI5708Upd()
	'--------------------------------------------------------------------------------------------
	'- Objeto para procesar eliminacion de registro
	Dim lobjTable5708 As ePolicy.Table5708
	
	lobjTable5708 = New ePolicy.Table5708
	
	With Request
		If .QueryString("Action") = "Del" Then
			Response.Write(mobjValues.ConfirmDelete())
			If lobjTable5708.insPostMVI5708(Request.QueryString("Action"), mobjValues.StringToType(Request.QueryString("nType_Move"), eFunctions.Values.eTypeData.etdDouble), Request.Form("tctDescript"), "", mobjValues.StringToType(Request.Form("cbeType"), eFunctions.Values.eTypeData.etdDouble), Request.Form("chkPb_Bmg"), Request.Form("cbeStatregt"), mobjValues.StringToType(CStr(Session("nUsercode")), eFunctions.Values.eTypeData.etdDouble)) Then
			End If
		End If
		Response.Write(mobjGrid.DoFormUpd(.QueryString("Action"), "valMantNoTraLife.aspx", "MVI5708", .QueryString("nMainAction"), mobjValues.ActionQuery, .QueryString("Index")))
	End With
End Sub

</script>
<%Response.Expires = -1
lobjErrors = New eGeneral.GeneralFunction
mstrAlert = "Err. 55873 " & lobjErrors.insLoadMessage(55873)
'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
lobjErrors = Nothing

mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues

mobjValues.sCodisplPage = "MVI5708_k"
%>
<HTML>
<HEAD>
	<META NAME="GENERATOR" Content="Microsoft Visual Studio 6.0">
    <%'UPGRADE_NOTE: Language element 'SCRIPT' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file '/VTimeNet/Scripts/GenFunctions.js' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
    <%'UPGRADE_NOTE: Language element '#INCLUDE' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file 'VTime/Includes/Constantes.aspx' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<!-- #INCLUDE VIRTUAL="~/VTimeNet/Includes/Constantes.aspx" -->

<SCRIPT LANGUAGE=JavaScript>
//- Variable para el control de versiones
	document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 16:15 $|$$Author: Nvaplat61 $"

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
If Request.QueryString("Type") <> "PopUp" Then
	Response.Write("<SCRIPT>var nMainAction=0</SCRIPT>")
	Response.Write("<SCRIPT LANGUAGE=""JavaScript"" SRC=""/VTimeNet/Scripts/tmenu.js""></SCRIPT>" & vbCrLf)
End If
With Response
	.Write(mobjValues.StyleSheet() & vbCrLf)
	If Request.QueryString("Type") <> "PopUp" Then
		mobjMenu = New eFunctions.Menues
		.Write(mobjMenu.MakeMenu(Request.QueryString("sCodispl"), "MVI5708_K.aspx", 1, ""))
		Response.Write("<BR></BR>")
		'UPGRADE_NOTE: Object mobjMenu may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
		mobjMenu = Nothing
	End If
End With
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" NAME="MVI5708" ACTION="valMantNoTraLife.aspx?sMode=2">
<%
Response.Write(mobjValues.ShowWindowsName(Request.QueryString("sCodispl")))
Call insDefineHeader()
If Request.QueryString("Type") = "PopUp" Then
	Call insPreMVI5708Upd()
Else
	Call insPreMVI5708()
End If
'UPGRADE_NOTE: Object mobjValues may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjValues = Nothing
'UPGRADE_NOTE: Object mobjGrid may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
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





