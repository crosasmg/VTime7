<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eBatch" %>
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

'- Objeto para el manejo particular de los datos de la página
Dim mcoltab_waitpos As Object


'% insDefineHeader: se definen las propiedades del grid
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	
	mobjGrid.sCodisplPage = "MCA816_k"
	
	'+ Se definen las columnas del grid 
	With mobjGrid.Columns
		Call .AddTextColumn(0, GetLocalResourceObject("tctNameColumnCaption"), "tctName", 70, vbNullString,  , GetLocalResourceObject("tctNameColumnToolTip"))
		Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeType_objectColumnCaption"), "cbeType_object", "Table5646", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeType_objectColumnToolTip"))
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnLevelColumnCaption"), "tcnLevel", 5, "1",  , GetLocalResourceObject("tcnLevelColumnToolTip"))
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnOrderColumnCaption"), "tcnOrder", 5, vbNullString,  , GetLocalResourceObject("tcnOrderColumnToolTip"))
		Call .AddTextColumn(0, GetLocalResourceObject("tctPathColumnCaption"), "tctPath", 70, vbNullString,  , GetLocalResourceObject("tctPathColumnToolTip"))
		Call .AddHiddenColumn("hddnId_object", "")
	End With
	
	'+ Se definen las propiedades generales del grid 
	With mobjGrid
		.Codispl = "MCA816"
		.Codisp = "MCA816_k"
		If Request.QueryString.Item("nMainAction") = "401" Or Request.QueryString.Item("nMainAction") = vbNullString Then
			mobjGrid.ActionQuery = True
			mobjGrid.Columns("Sel").GridVisible = False
		End If
		
		.Height = 350
		.Width = 600
		.Top = 100
		.WidthDelete = 500
		
		' parámetros para eliminación
		.sDelRecordParam = "nId_object='+ marrArray[lintIndex].hddnId_object + '"
		
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
End Sub

'% insPremca816: se realiza el manejo del grid
'--------------------------------------------------------------------------------------------
Private Sub insPremca816()
	'--------------------------------------------------------------------------------------------
	Dim mclscot_stand_alone As eBatch.cot_stand_alone
	Dim mcolcot_stand_alones As eBatch.cot_stand_alones
	mclscot_stand_alone = New eBatch.cot_stand_alone
	mcolcot_stand_alones = New eBatch.cot_stand_alones
	
	If mcolcot_stand_alones.Find() Then
		For	Each mclscot_stand_alone In mcolcot_stand_alones
			With mobjGrid
				.Columns("tctName").DefValue = mclscot_stand_alone.sName
				.Columns("cbeType_object").DefValue = CStr(mclscot_stand_alone.nType_object)
				.Columns("tcnLevel").DefValue = CStr(mclscot_stand_alone.nLevel)
				.Columns("tcnOrder").DefValue = CStr(mclscot_stand_alone.nOrder)
				.Columns("tctPath").DefValue = mclscot_stand_alone.sPath
				.Columns("hddnId_object").DefValue = CStr(mclscot_stand_alone.nId_object)
				Response.Write(.DoRow)
				
			End With
		Next mclscot_stand_alone
	End If
	Response.Write(mobjGrid.closeTable())
End Sub

'*++ Modificar nombre de la función. Modificar "mca816" por el código lógico de la transacción
'% insPremca816Upd: Se realiza el manejo de la ventana PopUp asociada al grid
'--------------------------------------------------------------------------------------------
Private Sub insPremca816Upd()
	'--------------------------------------------------------------------------------------------
	'- Objeto para procesar eliminacion de registro
	Dim lobjcot_stand_alone As eBatch.cot_stand_alone
	
	lobjcot_stand_alone = New eBatch.cot_stand_alone
	
	With Request
		If .QueryString.Item("Action") = "Del" Then
			Response.Write(mobjValues.ConfirmDelete())
			If lobjcot_stand_alone.insPostMCA816_K("MCA816", Request.QueryString.Item("Action"), mobjValues.StringToType(Request.QueryString.Item("nId_object"), eFunctions.Values.eTypeData.etdDouble)) Then
			End If
		End If
		Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "valMantPolicy.aspx", "MCA816", .QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(.QueryString.Item("Index"))))
	End With
End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues

mobjValues.sCodisplPage = "MCA816_k"
%>
<HTML>
<HEAD>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>


<SCRIPT LANGUAGE=JavaScript>
//- Variable para el control de versiones
	document.VssVersion="$$Revision: 1 $|$$Date: 21/02/06 17:36 $|$$Author: Pmanzur $"

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
		.Write(mobjMenu.MakeMenu(Request.QueryString.Item("sCodispl"), "MCA816_K.aspx", 1, ""))
		Response.Write("<BR></BR>")
		mobjMenu = Nothing
	End If
End With
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" NAME="MCA816" ACTION="valMantPolicy.aspx?sMode=2">
<%
Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl")))
Call insDefineHeader()
If Request.QueryString.Item("Type") = "PopUp" Then
	Call insPremca816Upd()
Else
	Call insPremca816()
End If
mobjValues = Nothing
mobjGrid = Nothing
%>
</FORM> 
</BODY>
</HTML>





