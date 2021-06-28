<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eGeneral" %>
<script language="VB" runat="Server">
'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
'- Se define la variable mobjGrid para el manejo del Grid de la ventana
Dim mobjGrid As eFunctions.Grid
'- Objeto para el manejo de las zonas de la página
Dim mobjMenu As eFunctions.Menues
Dim mstrAlert As String
Dim lobjErrors As eGeneral.GeneralFunction


'%insDefineHeader. Definición de columnas del GRID
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------        
	mobjGrid = New eFunctions.Grid
	
	mobjGrid.sCodisplPage = "ms006_k"
	
	'+Se definen todas las columnas del Grid
	With mobjGrid.Columns
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnActionColumnCaption"), "tcnAction", 4, "", True, GetLocalResourceObject("tcnActionColumnToolTip"), False, 0,  ,  ,  , False)
		Call .AddTextColumn(0, GetLocalResourceObject("tcsDescriptColumnCaption"), "tcsDescript", 20, "", True, GetLocalResourceObject("tcsDescriptColumnToolTip"),  ,  ,  , False)
		Call .AddTextColumn(0, GetLocalResourceObject("tcsHel_actioColumnCaption"), "tcsHel_actio", 70, "", True, GetLocalResourceObject("tcsHel_actioColumnToolTip"),  ,  ,  , False)
		Call .AddPossiblesColumn(0, GetLocalResourceObject("tcsStatregtColumnCaption"), "tcsStatregt", "Table26", 1,  ,  ,  ,  ,  ,  ,  , 1, GetLocalResourceObject("tcsStatregtColumnToolTip"), 2)
		Call .AddTextColumn(0, GetLocalResourceObject("tcsPathImageColumnCaption"), "tcsPathImage", 50, "", False, GetLocalResourceObject("tcsPathImageColumnToolTip"),  ,  ,  , False)
	End With
	
	With mobjGrid
		.Codispl = "MS006"
		.Codisp = "MS006"
		.Top = 150
		.Left = 100
		.Height = 288
		.Width = 600
		.bOnlyForQuery = Request.QueryString.Item("nMainAction") = "401"
		.ActionQuery = (Request.QueryString.Item("nMainAction") = "401" Or IsNothing(Request.QueryString.Item("nMainAction")))
		.Columns("Sel").GridVisible = Not .ActionQuery
		.Columns("tcnAction").EditRecord = True
		.Columns("tcnAction").Disabled = Request.QueryString.Item("Action") = "Update"
		.sDelRecordParam = "p_nAction='+ marrArray[lintIndex].tcnAction + '"
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
End Sub

'%insPreMS006: Esta función se encarga de cargar los datos en la forma "Folder" 
'--------------------------------------------------------------------------------------------
Private Sub insPreMS006()
	'--------------------------------------------------------------------------------------------
	Dim lcolActionss As eGeneral.Actionss
	Dim lclsActions As eGeneral.Actions
	Dim lstrinderr As Byte
	lclsActions = New eGeneral.Actions
	lcolActionss = New eGeneral.Actionss
	If lcolActionss.Find(0) Then
		For	Each lclsActions In lcolActionss
			With mobjGrid
				.Columns("tcnAction").DefValue = CStr(lclsActions.nAction)
				.Columns("tcsDescript").DefValue = lclsActions.sDescript
				.Columns("tcsHel_actio").DefValue = lclsActions.sHel_actio
				.Columns("tcsStatregt").DefValue = lclsActions.sStatregt
				.Columns("tcsPathImage").DefValue = lclsActions.sPathImage
				If lclsActions.sExist = "1" Then
					lstrinderr = 1
				Else
					lstrinderr = 2
				End If
				
				.Columns("Sel").OnClick = "InsChangeSel(this," & lstrinderr & ")"
				
			End With
			'+Se ejecuta el metodo DoRow, que se encarga de mostrar los elementos de grid
			Response.Write(mobjGrid.DoRow())
		Next lclsActions
		Response.Write(mobjGrid.closeTable())
	End If
	lclsActions = Nothing
	lcolActionss = Nothing
End Sub
'% insPreMS006Upd. Se define esta funcion para contruir el contenido de la ventana UPD de los archivos de datos particulares
'----------------------------------------------------------------------------------------------------------------------------
Private Sub insPreMS006Upd()
	'----------------------------------------------------------------------------------------------------------------------------
	Dim lclsActions As eGeneral.Actions
	Dim lstrErrors As Object
	With Request
		If .QueryString.Item("Action") = "Del" Then
			lclsActions = New eGeneral.Actions
			lstrErrors = lclsActions.insPostMS006_K(.QueryString.Item("sCodispl"), .QueryString.Item("Action"), mobjValues.StringToType(.QueryString.Item("p_nAction"), eFunctions.Values.eTypeData.etdDouble), "Descripcion", "sHel_actio", "1", Session("nUsercode"), "sPathImage")
			Response.Write(mobjValues.ConfirmDelete())
			If lstrErrors = vbNullString Then
				
				lclsActions.nAction = mobjValues.StringToType(.QueryString.Item("p_nAction"), eFunctions.Values.eTypeData.etdDouble)
				lclsActions.Delete()
			End If
			lclsActions = Nothing
		End If
	End With
	Response.Write(mobjGrid.DoFormUpd(Request.QueryString.Item("Action"), "valMantsys.aspx", Request.QueryString.Item("sCodispl"), Request.QueryString.Item("MainAction"),  , CShort(Request.QueryString.Item("Index"))))
	Response.Write(mobjValues.HiddenControl("sAction", Request.QueryString.Item("Action")))
End Sub

</script>
<%Response.Expires = -1
mobjValues = New eFunctions.Values
lobjErrors = New eGeneral.GeneralFunction
mstrAlert = "Err. 100011 " & lobjErrors.insLoadMessage(100011)
lobjErrors = Nothing

mobjValues.sCodisplPage = "ms006_k"
%>
<HTML>
<HEAD>
    <META NAME="GENERATOR" Content="eTransaction Designer for Visual TIME">
<SCRIPT LANGUAGE="JavaSCRIPT" SRC="/VTimeNet/SCRIPTs/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaSCRIPT" SRC="/VTimeNet/SCRIPTs/Constantes.js"></SCRIPT>


<%
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write("<SCRIPT>var nMainAction = 0" & Request.QueryString.Item("nMainAction") & "</SCRIPT>")
	Response.Write("<SCRIPT LANGUAGE=""JavaSCRIPT"" SRC=""/VTimeNet/SCRIPTs/tmenu.js""></SCRIPT>" & vbCrLf)
End If
With Response
	.Write(mobjValues.StyleSheet() & vbCrLf)
	If Request.QueryString.Item("Type") <> "PopUp" Then
		mobjMenu = New eFunctions.Menues
		.Write(mobjMenu.MakeMenu(Request.QueryString.Item("sCodispl"), "MS006_k.aspx", 1, ""))
		mobjMenu = Nothing
	End If
End With
%>
<SCRIPT>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 16:29 $"

//% insCancel: Ejecuta rutinas necesarias en el momento de cancelar la página
//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
    return true;
}
//% insStateZone: se manejan los campos de la página
//------------------------------------------------------------------------------------------
function insStateZone(){
//------------------------------------------------------------------------------------------      
    return true;
}
//% insPreZone: Se maneja la Acción para la Busqueda por Condición
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
//%InsChangeSel: Se envía mensaje de validación al no poder eliminar un registro
//------------------------------------------------------------------------------
function InsChangeSel(Field, sInd){
//------------------------------------------------------------------------------
	if (Field.checked && sInd == "1") {
		alert('<%=mstrAlert%>');
		Field.checked = false
	}
}
</SCRIPT> 
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<%
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write("<BR><BR>")
End If
Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl")))
%>
<FORM METHOD="post" ID="FORM" NAME="frmActionsSys" ACTION="valMantsys.aspx?sTime=1">
<%
Call insDefineHeader()
If Request.QueryString.Item("Type") <> "PopUp" Then
	Call insPreMS006()
Else
	Call insPreMS006Upd()
End If
mobjValues = Nothing
mobjGrid = Nothing
%>
</FORM>
</BODY>
</HTML>





