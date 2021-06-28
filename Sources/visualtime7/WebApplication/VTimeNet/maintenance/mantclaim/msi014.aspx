<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eBranches" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo de las rutinas genéricas
Dim mobjGrid As eFunctions.Grid

'- Objeto para el manejo de las zonas de la pantalla
Dim mobjMenues As eFunctions.Menues


'% insDefineHeader: Se definen las columnas del Grid
'------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	
	With mobjGrid.Columns
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnDamage_codColumnCaption"), "tcnDamage_cod", 4, vbNullString, True, GetLocalResourceObject("tcnDamage_codColumnToolTip"),  ,  ,  ,  ,  , CBool(Request.QueryString.Item("Action") = "Update"))
		Call .AddTextColumn(0, GetLocalResourceObject("tctDescriptColumnCaption"), "tctDescript", 30, vbNullString,  , GetLocalResourceObject("tctDescriptColumnToolTip"))
		Call .AddTextColumn(0, GetLocalResourceObject("tctShort_desColumnCaption"), "tctShort_des", 12, vbNullString,  , GetLocalResourceObject("tctShort_desColumnToolTip"))
		Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeStatregtColumnCaption"), "cbeStatregt", "Table26", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeStatregtColumnToolTip"))
	End With
	
	'+Se asignan las caracteristicas del Grid
	
	With mobjGrid
		'+Se crean los parametros para las listas de valores posibles
		.Columns("tctDescript").EditRecord = True
		.Columns("cbeStatregt").TypeList = 2
		.Columns("cbeStatregt").List = "2"
		.Codispl = "MSI014"
		.Codisp = "MSI014"
		.sCodisplPage = "MSI014"
		If Request.QueryString.Item("nMainAction") = "401" Or Request.QueryString.Item("nMainAction") = vbNullString Then
			.ActionQuery = True
			.Columns("Sel").GridVisible = False
		End If
		'+Pase de parametros necesarios para la eliminación de registros
		.sDelRecordParam = "nBranch=" & mobjValues.typeToString(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble) & "&nDamage_cod='+marrArray[lintIndex].tcnDamage_cod + '"
		.Height = 230
		.Width = 350
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
End Sub

'%insPreMSI014: Se llena los valores de las columnas del Grid
'------------------------------------------------------------------------------
Private Sub insPreMSI014()
	'------------------------------------------------------------------------------
	Dim lcolTab_damages As eBranches.Tab_damages
	Dim lclsTab_damage As Object
	
	lcolTab_damages = New eBranches.Tab_damages
	
	If CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 306 Then
		Call lcolTab_damages.Find(mobjValues.StringToType(Session("nLastBranch"), eFunctions.Values.eTypeData.etdDouble))
	Else
		Call lcolTab_damages.Find(mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble))
	End If
	For	Each lclsTab_damage In lcolTab_damages
		With mobjGrid
			.Columns("tcnDamage_cod").DefValue = lclsTab_damage.nDamage_cod
			.Columns("tctDescript").DefValue = lclsTab_damage.sDescript
			.Columns("tctShort_des").DefValue = lclsTab_damage.sShort_des
			.Columns("cbeStatregt").DefValue = lclsTab_damage.sStatregt
		End With
		
		'+Se ejecuta el metodo DoRow, que se encarga de mostrar los elementos de grid
		Response.Write(mobjGrid.DoRow())
	Next lclsTab_damage
	Response.Write(mobjGrid.closeTable())
	lcolTab_damages = Nothing
End Sub

'%insPreMSI014Upd: Se Actualiza el Registro seleccionado en el Grid
'------------------------------------------------------------------------------
Private Sub insPreMSI014Upd()
	'------------------------------------------------------------------------------
	Dim lclsTab_damage As eBranches.Tab_damage
	Dim lstrErrors As String
	Dim mstrCommand As String
	
	mstrCommand = "&sModule=Maintenance&sProject=MantClaim&sCodisplReload=" & Request.QueryString.Item("sCodispl")
	
	If Request.QueryString.Item("Action") = "Del" Then
		lclsTab_damage = New eBranches.Tab_damage
		lstrErrors = lclsTab_damage.insValClaim_dama(Request.QueryString.Item("sCodispl"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), CInt(Request.QueryString.Item("nDamage_cod")))
		If lstrErrors = vbNullString Then
			Response.Write(mobjValues.ConfirmDelete())
			With lclsTab_damage
				.nBranch = mobjValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble)
				.nDamage_cod = mobjValues.StringToType(Request.QueryString.Item("nDamage_cod"), eFunctions.Values.eTypeData.etdDouble)
				.Delete()
			End With
		Else
			Session("sErrorTable") = lstrErrors
			With Response
				.Write("<SCRIPT LANGUAGE=JAVASCRIPT>")
				.Write("ShowPopUp(""/VTimeNet/Common/Errors.aspx?sSource=" & Server.URLEncode(mstrCommand) & "&sQueryString=" & Server.URLEncode(Request.Params.Get("Query_String")) & """, ""MantClaimError"",660,330);self.document.location.href='/VTimeNet/Common/Blank.htm';top.window.close();")
				.Write("</" & "Script>")
			End With
		End If
		lclsTab_damage = Nothing
	End If
	Response.Write(mobjGrid.DoFormUpd(Request.QueryString.Item("Action"), "valMantClaim.aspx", Request.QueryString.Item("sCodispl"), Request.QueryString.Item("MainAction"),  , CShort(Request.QueryString.Item("Index"))))
	Response.Write(mobjValues.HiddenControl("sAction", Request.QueryString.Item("Action")))
End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjValues.sCodisplPage = "MSI014"
%>
<HTML>
<HEAD>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
    <META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>


<%
Response.Write(mobjValues.StyleSheet())
Response.Write("<SCRIPT>var nMainAction=" & Request.QueryString.Item("nMainAction") & "</SCRIPT>")
If Request.QueryString.Item("Type") <> "PopUp" Then
	mobjMenues = New eFunctions.Menues
	Response.Write(mobjMenues.setZone(2, "MSI014", "MSI014"))
	mobjMenues = Nothing
End If
%>

<SCRIPT>
//-Variable para el control de Versiones
document.VssVersion="$$Revision: 3 $|$$Date: 15/10/03 15:53 $|$$Author: Nvaplat61 $"

//% insStateZone: se controla el estado de los campos de la página
//------------------------------------------------------------------------------------------
function insStateZone(){
//------------------------------------------------------------------------------------------      
}

//% insPreZone: Modifica el comportamiento de la página dependiendo de la acción
//% que proviene del menú principal
//------------------------------------------------------------------------------------------
function insPreZone(llngAction){
//------------------------------------------------------------------------------------------
	switch (llngAction){
	    case 301:
	    case 302:
	    case 401:
	        document.location.href = document.location.href.replace(/&nMainAction.*/,'') + '&nMainAction=' + llngAction
	        break;
	}
}

</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="frmClaimDamages" ACTION="ValMantClaim.aspx?mode=1">
<%
Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl")))
Call insDefineHeader()
If Request.QueryString.Item("Type") <> "PopUp" Then
	Call insPreMSI014()
Else
	Call insPreMSI014Upd()
End If
mobjValues = Nothing
mobjGrid = Nothing
%>
</FORM>
</BODY>
</HTML>




