<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eGeneral" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo de las rutinas genéricas

Dim mobjMenu As eFunctions.Menues
Dim mobjGrid As eFunctions.Grid



'% insDefineHeader:Este procedimiento se encarga de definir las columnas del grid y de habilitar
'%				   o inhabilitar los botones de añadir y eliminar.
'------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	
	mobjGrid.sCodisplPage = "ms100_k"
	
	'+ Se definen las columns del Grid
	With mobjGrid.Columns
		Call .AddPossiblesColumn(101515, GetLocalResourceObject("cbeType_proceColumnCaption"), "cbeType_proce", "Table526", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  , Request.QueryString.Item("Action") = "Update",  , GetLocalResourceObject("cbeType_proceColumnToolTip"))
		Call .AddDateColumn(101517, GetLocalResourceObject("tcdEffecdateColumnCaption"), "tcdEffecdate",  ,  , GetLocalResourceObject("tcdEffecdateColumnToolTip"))
	End With
	
	'+ Se asignan las caracteristicas del Grid
	With mobjGrid
		.Codispl = Request.QueryString.Item("sCodispl")
		.Codisp = "MS100_K"
		If Request.QueryString.Item("nMainAction") = "401" Or Request.QueryString.Item("nMainAction") = vbNullString Then
			mobjGrid.ActionQuery = True
			mobjGrid.Columns("Sel").GridVisible = False
		Else
			.Columns("cbeType_proce").EditRecord = True
			.Columns("Sel").Title = "Sel"
		End If
		.sDelRecordParam = "nType_proce='+ marrArray[lintIndex].cbeType_proce + '"
		.Height = 230
		.Width = 310
		.Top = 100
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
	
End Sub

' insPreMS100(): Función que carga todos los valores en el Grid
'------------------------------------------------------------------------------
Private Sub insPreMS100()
	'------------------------------------------------------------------------------
	Dim lcolCtrol_dates As eGeneral.Ctrol_dates
	Dim lclsCtrol_date As eGeneral.Ctrol_date
	
	lcolCtrol_dates = New eGeneral.Ctrol_dates
	lclsCtrol_date = New eGeneral.Ctrol_date
	
	If lcolCtrol_dates.find Then
		For	Each lclsCtrol_date In lcolCtrol_dates
			With mobjGrid
				.Columns("cbeType_proce").DefValue = CStr(lclsCtrol_date.nType_proce)
				.Columns("tcdEffecdate").DefValue = CStr(lclsCtrol_date.dEffecdate)
			End With
			
			'+Se ejecuta el metodo DoRow, que se encarga de mostrar los elementos de grid
			Response.Write(mobjGrid.DoRow())
		Next lclsCtrol_date
	End If
	Response.Write(mobjGrid.closeTable())
	lcolCtrol_dates = Nothing
	lclsCtrol_date = Nothing
End Sub

' insPreMS100(): Función que Actualiza un Registro en el Grid
'------------------------------------------------------------------------------
Private Sub insPreMS100Upd()
	'------------------------------------------------------------------------------
	Dim lclsCtrol_date As eGeneral.Ctrol_date
	Dim lclsErrors As eFunctions.Errors
	
	With Server
		lclsCtrol_date = New eGeneral.Ctrol_date
		lclsErrors = New eFunctions.Errors
	End With
	
	If Request.QueryString.Item("Action") = "Del" Then
		Response.Write(mobjValues.ConfirmDelete())
		With lclsCtrol_date
			.nType_proce = CInt(Request.QueryString.Item("nType_proce"))
			.Delete()
		End With
	End If
	
	lclsCtrol_date = Nothing
	lclsErrors = Nothing
	Response.Write(mobjGrid.DoFormUpd(Request.QueryString.Item("Action"), "valMantSys.aspx", Request.QueryString.Item("sCodispl"), Request.QueryString.Item("nMainAction"),  , CShort(Request.QueryString.Item("Index"))))
End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values

mobjValues.sCodisplPage = "ms100_k"
%>
<HTML>
<HEAD>
<SCRIPT>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 1 $|$$Date: 2/09/03 19:03 $|$$Author: Iusr_llanquihue $"
</SCRIPT>	
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">


<SCRIPT>
//% insStateZone: se manejan los campos de la página
//------------------------------------------------------------------------------------------
function  insStateZone()
//------------------------------------------------------------------------------------------
{
}

//% insCancel: Ejecuta rutinas necesarias en el momento de cancelar la página
//------------------------------------------------------------------------------------------
function insCancel()
//------------------------------------------------------------------------------------------
{		    
	return true;
}

//% insPreZone: Se maneja la Acción para la Busqueda por Condición
//------------------------------------------------------------------------------------------
function insPreZone(llngAction)
//------------------------------------------------------------------------------------------
{
	switch (llngAction)
	{
		case 302:
		case 301:
		case 401:
			document.location.href = document.location.href.replace(/&nMainAction.*/,'') + '&nMainAction=' + llngAction            
		break;
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
		.Write(mobjMenu.MakeMenu(Request.QueryString.Item("sCodispl"), "MS100_K.aspx", 1, ""))
		mobjMenu = Nothing
	End If
End With
%>
</HEAD>

<BODY ONUNLOAD="closeWindows();">
<%
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write("<BR></BR>")
End If
Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl")))
%>
<FORM METHOD="POST" ID="FORM" NAME="frmTabRelations" ACTION="valMantsys.aspx?mode=1">
 <%
Call insDefineHeader()
If Request.QueryString.Item("Type") <> "PopUp" Then
	Call insPreMS100()
Else
	Call insPreMS100Upd()
End If
mobjValues = Nothing
mobjGrid = Nothing
%>
</FORM>
</BODY>
</HTML>





