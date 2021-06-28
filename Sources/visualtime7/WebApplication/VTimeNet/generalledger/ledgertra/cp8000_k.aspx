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
Dim mcolCtrol_Dates As eGeneral.Ctrol_dates


'% insDefineHeader: se definen las propiedades del grid
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	mobjGrid.sCodisplPage = "cp8000_k"
	
	'+ Se definen las columnas del grid 
	With mobjGrid.Columns
		If Request.QueryString.Item("Type") <> "PopUp" Then
			Call .AddNumericColumn(0, GetLocalResourceObject("tcnType_proceColumnCaption"), "tcnType_proce", 5, vbNullString,  , GetLocalResourceObject("tcnType_proceColumnToolTip"),  ,  ,  ,  ,  , False)
			Call .AddTextColumn(0, GetLocalResourceObject("tctDescriptColumnCaption"), "tctDescript", 30, vbNullString,  , GetLocalResourceObject("tctDescriptColumnToolTip"))
			Call .AddDateColumn(0, GetLocalResourceObject("tcndate_closeColumnCaption"), "tcndate_close",  ,  , GetLocalResourceObject("tcndate_closeColumnToolTip"))
		Else
			Call .AddPossiblesColumn(0, GetLocalResourceObject("tcnType_proceColumnCaption"), "tcnType_proce", "Table526", 2,  , True,  ,  ,  ,  , False,  , GetLocalResourceObject("tcnType_proceColumnToolTip"))
			Call .AddHiddenColumn("tctDescript", "")
			Call .AddDateColumn(0, GetLocalResourceObject("tcndate_closeColumnCaption"), "tcndate_close",  ,  , GetLocalResourceObject("tcndate_closeColumnToolTip"))
		End If
	End With
	
	'+ Se definen las propiedades generales del grid 
	With mobjGrid
		.Codispl = "CP8000"
		.Codisp = "CP8000"
		If Request.QueryString.Item("nMainAction") = "401" Or Request.QueryString.Item("nMainAction") = vbNullString Then
			mobjGrid.ActionQuery = True
			mobjGrid.Columns("Sel").GridVisible = False
		Else
			.Columns("tctDescript").EditRecord = True
		End If
		
		.Height = 200
		.Width = 500
		.Top = 100
		
		'+ parámetros para eliminación
		.sDelRecordParam = "nType_proce='+ marrArray[lintIndex].tcnType_proce + '"
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
End Sub

'% insPreCP8000: se realiza el manejo del grid
'--------------------------------------------------------------------------------------------
Private Sub insPreCP8000()
	'--------------------------------------------------------------------------------------------
	Dim mclsCtrol_Date As eGeneral.Ctrol_date
	mclsCtrol_Date = New eGeneral.Ctrol_date
	mcolCtrol_Dates = New eGeneral.Ctrol_dates
	If mcolCtrol_Dates.insPreCP8000() Then
		For	Each mclsCtrol_Date In mcolCtrol_Dates
			With mobjGrid
				.Columns("tcnType_proce").DefValue = CStr(mclsCtrol_Date.nType_proce)
				.Columns("tctDescript").DefValue = mclsCtrol_Date.sDescript
				.Columns("tcndate_close").DefValue = CStr(mclsCtrol_Date.dEffecdate)
				Response.Write(.DoRow)
			End With
		Next mclsCtrol_Date
	End If
	Response.Write(mobjGrid.closeTable())
End Sub

'% insPreCP8000Upd: Se realiza el manejo de la ventana PopUp asociada al grid
'--------------------------------------------------------------------------------------------
Private Sub insPreCP8000Upd()
	'--------------------------------------------------------------------------------------------
	'- Objeto para procesar eliminacion de registro
	Dim lobjCtrol_date As eGeneral.Ctrol_date
	
	lobjCtrol_date = New eGeneral.Ctrol_date
	
	With Request
		If .QueryString.Item("Action") = "Del" Then
			Response.Write(mobjValues.ConfirmDelete())
			If lobjCtrol_date.insPostCP8000(Request.QueryString.Item("Action"), mobjValues.StringToType(Request.QueryString.Item("nType_proce"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("tcndate_close"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble)) Then
			End If
		End If
		Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "valledgertra.aspx", "CP8000", .QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(.QueryString.Item("Index"))))
	End With
End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values

mobjValues.sCodisplPage = "CP8000"
%>
<HTML>
<HEAD>
    <meta NAME="GENERATOR" Content="eTransaction Designer for Visual TIME">
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
		.Write(mobjMenu.MakeMenu(Request.QueryString.Item("sCodispl"), "cp8000_k.aspx", 1, ""))
		mobjMenu = Nothing
	End If
End With
%>
<SCRIPT>

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
</SCRIPT> 
</HEAD>

<BODY ONUNLOAD="closeWindows();">
<%
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write("<BR><BR>")
End If
Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl")))
%>
<FORM METHOD="post" ID="FORM" NAME="CP8000" ACTION="valledgertra.aspx?sTime=1">
<%
Call insDefineHeader()
If Request.QueryString.Item("Type") <> "PopUp" Then
	Call insPreCP8000()
Else
	Call insPreCP8000Upd()
End If
mobjValues = Nothing
mobjGrid = Nothing
%>
</FORM>
</BODY>
</HTML>




