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



'% insDefineHeader : Configura las columnas del grid
'------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'------------------------------------------------------------------------------
	
	mobjGrid = New eFunctions.Grid
	
	'+Se definen las columnas del Grid
	
	With mobjGrid.Columns
		Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeActivityCatColumnCaption"), "cbeActivityCat", "Table7044", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeActivityCatColumnToolTip"),  , 0)
		Call .AddPossiblesColumn(1, GetLocalResourceObject("cbeConstCatColumnCaption"), "cbeConstCat", "Table233", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeConstCatColumnToolTip"),  , 1)
		Call .AddNumericColumn(2, GetLocalResourceObject("ntctRateBuildColumnCaption"), "ntctRateBuild", 8, CStr(0),  , GetLocalResourceObject("ntctRateBuildColumnToolTip"),  , 5,  ,  ,  ,  , 2)
		Call .AddNumericColumn(3, GetLocalResourceObject("ntctRateContColumnCaption"), "ntctRateCont", 8, CStr(0),  , GetLocalResourceObject("ntctRateContColumnToolTip"),  , 5,  ,  ,  ,  , 3)
		Call .AddNumericColumn(4, GetLocalResourceObject("ntctRateRCColumnCaption"), "ntctRateRC", 8, CStr(0),  , GetLocalResourceObject("ntctRateRCColumnToolTip"),  , 5,  ,  ,  ,  , 4)
		Call .AddHiddenColumn("dtctEffecDate", CStr(eRemoteDB.Constants.dtmNull))
		Call .AddHiddenColumn("dtctNullDate", CStr(eRemoteDB.Constants.dtmNull))
	End With
	
	'+Se asignan las caracteristicas del Grid
	
	With mobjGrid
		'+Se crean los parametros para las listas de valores posibles
		.Columns("cbeActivityCat").EditRecord = True
		.Columns("cbeConstCat").EditRecord = True
		.Codispl = "MIN003"
		.Codisp = "MIN003"
		.sCodisplPage = "MIN003"
		If Request.QueryString.Item("nMainAction") = "401" Or Request.QueryString.Item("nMainAction") = vbNullString Then
			mobjGrid.ActionQuery = True
			mobjGrid.Columns("Sel").GridVisible = False
		End If
		
		If Request.QueryString.Item("Action") = "Update" Then
			.Columns("cbeActivityCat").Disabled = True
			.Columns("cbeConstCat").Disabled = True
		Else
			.Columns("cbeActivityCat").Disabled = False
			.Columns("cbeConstCat").Disabled = False
		End If
		
		'+Pase de parametros necesarios para la eliminación de registros
		.sDelRecordParam = "nActivityCat='+marrArray[lintIndex].cbeActivityCat + '" & "&nConstCat='+marrArray[lintIndex].cbeConstCat + '" & "&dNullDate='+marrArray[lintIndex].dtctNullDate + '" & "&dEffecDate='+marrArray[lintIndex].dtctEffecDate + '"
		.Height = 300
		.Width = 350
		
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
	
End Sub

'%insPreMIN003: Llena el Grid con información
'------------------------------------------------------------------------------
Private Sub insPreMIN003()
	'------------------------------------------------------------------------------
	Dim lcolTar_firecats As eBranches.Tar_firecats
	Dim lclsTar_Firecat As eBranches.Tar_firecat
	
	lclsTar_Firecat = New eBranches.Tar_firecat
	lcolTar_firecats = New eBranches.Tar_firecats
	
	
	If lcolTar_firecats.Find(mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate)) Then 'Request.QueryString("cbedate") ,eFunctions.Values.eTypeData.etdDate )
		For	Each lclsTar_Firecat In lcolTar_firecats
			With mobjGrid
				.Columns("cbeActivityCat").DefValue = CStr(lclsTar_Firecat.nActivityCat)
				.Columns("cbeConstCat").DefValue = CStr(lclsTar_Firecat.nConstCat)
				.Columns("ntctRateBuild").DefValue = CStr(lclsTar_Firecat.nRateBuild)
				.Columns("ntctRateCont").DefValue = CStr(lclsTar_Firecat.nRateCont)
				.Columns("ntctRateRC").DefValue = CStr(lclsTar_Firecat.nRateRC)
				.Columns("dtctEffecDate").DefValue = CStr(lclsTar_Firecat.dEffecdate)
				.Columns("dtctNullDate").DefValue = CStr(lclsTar_Firecat.dNulldate)
			End With
			
			'+Se ejecuta el metodo DoRow, que se encarga de mostrar los elementos de grid
			
			Response.Write(mobjGrid.DoRow())
		Next lclsTar_Firecat
	End If
	Response.Write(mobjGrid.closeTable())
	lclsTar_Firecat = Nothing
	lcolTar_firecats = Nothing
End Sub

'%insPreMIN003Upd: Actualiza un Registro del Grid
'------------------------------------------------------------------------------
Private Sub insPreMIN003Upd()
	'------------------------------------------------------------------------------
	Dim lclsTar_Firecat As eBranches.Tar_firecat
	
	Dim lobjErrors As eFunctions.Errors
	If Request.QueryString.Item("Action") = "Del" Then
		
		lclsTar_Firecat = New eBranches.Tar_firecat
		
		
		lobjErrors = New eFunctions.Errors
		
		If mobjValues.StringToDate(Request.QueryString.Item("dNullDate")) <> eRemoteDB.Constants.dtmNull Then
			lobjErrors.Highlighted = True
			Response.Write(lobjErrors.ErrorMessage("MIN003", 700018,  ,  ,  , True))
		Else
			Response.Write(mobjValues.ConfirmDelete())
			Call lclsTar_Firecat.insPostMIN003(Request.QueryString.Item("Action"), CInt(Request.QueryString.Item("nActivityCat")), CInt(Request.QueryString.Item("nConstCat")), mobjValues.StringToDate(Session("dEffecdate")), CDbl(Request.QueryString.Item("ntctRateBuild")), CDbl(Request.QueryString.Item("ntctRateCont")), CDbl(Request.QueryString.Item("ntctRateRC")), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
		End If
		
		lobjErrors = Nothing
		lclsTar_Firecat = Nothing
	End If
	
	With Response
		.Write(mobjGrid.DoFormUpd(Request.QueryString.Item("Action"), "valMantFire.aspx", Request.QueryString.Item("sCodispl"), Request.QueryString.Item("MainAction"),  , CShort(Request.QueryString.Item("Index"))))
		.Write(mobjValues.HiddenControl("sAction", Request.QueryString.Item("Action")))
	End With
End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjValues.sCodisplPage = "MIN003"
%>

<HTML>
<HEAD>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
    <META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>


    <%=mobjValues.StyleSheet()%>
    <%="<script>var nMainAction=" & Request.QueryString.Item("nMainAction") & "</script>"%>
    <%
If Request.QueryString.Item("Type") <> "PopUp" Then
	mobjMenues = New eFunctions.Menues
	Response.Write(mobjMenues.setZone(2, "MIN003", "MIN003"))
	mobjMenues = Nothing
End If
%>

<SCRIPT>

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
</SCRIPT>

</HEAD>
<BODY ONUNLOAD="closeWindows();">

<FORM METHOD="POST" ID="FORM" NAME="frmMantFire" ACTION="ValMantFire.aspx?mode=1">
<%
Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl")))
Call insDefineHeader()
If Request.QueryString.Item("Type") <> "PopUp" Then
	Call insPreMIN003()
	
	'+ Reasignando valores a los campos del encabezado luego de su recarga
	
Else
	Call insPreMIN003Upd()
End If
mobjValues = Nothing
mobjGrid = Nothing
%>
</FORM>
</BODY>
</HTML>





