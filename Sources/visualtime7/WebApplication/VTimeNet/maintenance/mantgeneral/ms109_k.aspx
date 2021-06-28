<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eGeneralForm" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo de las rutinas genéricas
Dim mobjMenu As eFunctions.Menues

'- Objeto para el manejo del Grid de la ventana
Dim mobjGrid As eFunctions.Grid


'% insDefineHeader: Define las columnas del Grid
'------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'------------------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	
	mobjValues.ActionQuery = Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionQuery) Or Request.QueryString.Item("nMainAction") = vbNullString
	
	With mobjGrid
		With .Columns
			.AddNumericColumn(101895, GetLocalResourceObject("tcnProvinceColumnCaption"), "tcnProvince", 5, vbNullString, True, GetLocalResourceObject("tcnProvinceColumnToolTip"), False)
			.AddTextColumn(101896, GetLocalResourceObject("tctDescriptColumnCaption"), "tctDescript", 30, vbNullString, True, GetLocalResourceObject("tctDescriptColumnToolTip"))
			.AddTextColumn(101897, GetLocalResourceObject("tctShort_desColumnCaption"), "tctShort_des", 12, vbNullString, True, GetLocalResourceObject("tctShort_desColumnToolTip"))
		End With
		
		.Height = 230
		.Width = 400
		.Codispl = "MS109"
		.sCodisplPage = "MS109"
		.Codisp = "MS109_K"
		.ActionQuery = mobjValues.ActionQuery
		.Columns("Sel").GridVisible = Not .ActionQuery
		.Columns("tctDescript").EditRecord = Not .ActionQuery
		.Columns("tcnProvince").Disabled = Request.QueryString.Item("Action") = "Update"
		.sDelRecordParam = "tcnProvince='+ marrArray[lintIndex].tcnProvince + '"
		
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
End Sub

'% insPreMS109: Carga los datos en el grid de la forma 
'------------------------------------------------------------------------------------------
Private Sub insPreMS109()
	'------------------------------------------------------------------------------------------
	Dim lcolProvinces As eGeneralForm.Provinces
	Dim lclsProvince As eGeneralForm.Province
	
	lcolProvinces = New eGeneralForm.Provinces
	lclsProvince = New eGeneralForm.Province
	
	If lcolProvinces.Find() Then
		With mobjGrid
			For	Each lclsProvince In lcolProvinces
				.Columns("tcnProvince").DefValue = CStr(lclsProvince.nProvince)
				.Columns("tctDescript").DefValue = lclsProvince.sDescript
				.Columns("tctShort_des").DefValue = lclsProvince.sShort_des
				.Columns("Sel").OnClick = "InsChangeSel(this," & lclsProvince.nProvince & ",this.value)"
				
				Response.Write(.DoRow)
			Next lclsProvince
		End With
	End If
	Response.Write(mobjGrid.CloseTable)
	Response.Write(mobjValues.BeginPageButton)
	
	lcolProvinces = Nothing
	lclsProvince = Nothing
End Sub

'% insPreMS109Upd: Actualiza un registro en el grid
'------------------------------------------------------------------------------------------
Private Sub insPreMS109Upd()
	'------------------------------------------------------------------------------------------
	Dim lobjError As Object
	Dim lclsProvince As eGeneralForm.Province
	
	With Request
		If .QueryString.Item("Action") = "Del" Then
			lclsProvince = New eGeneralForm.Province
			
			If lclsProvince.Delete(CInt(.QueryString.Item("tcnProvince"))) Then
				Response.Write(mobjValues.ConfirmDelete())
			End If
			
			lclsProvince = Nothing
		End If
		Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "valMantGeneral.aspx", .QueryString.Item("sCodispl"), .QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(.QueryString.Item("Index"))))
	End With
	
End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjValues.sCodisplPage = "MS109"
%>

<HTML>
<HEAD>
<SCRIPT>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 1 $|$$Date: 2/09/03 19:03 $|$$Author: Iusr_llanquihue $"
</SCRIPT>
<META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></SCRIPT>




		
<%
If Request.QueryString.Item("Type") <> "PopUp" Then
	%>
		<%	'$$EWI_1012:D:\VisualTIMEChile\Result\VTimeStep1\maintenance\mantgeneral\Vtime\Scripts\tMenu.js#%>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tMenu.js"></SCRIPT>
<%	
End If
Response.Write(mobjValues.StyleSheet())

If Request.QueryString.Item("Type") <> "PopUp" Then
	mobjMenu = New eFunctions.Menues
	With Response
		.Write(mobjMenu.MakeMenu("MS109", "MS109_K.aspx", 1, ""))
		.Write("<SCRIPT>var nMainAction = top.frames['fraSequence'].plngMainAction</SCRIPT>")
	End With
	mobjMenu = Nothing
End If
%>

<SCRIPT>
//% insStateZone: 
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
function insCancel(){
//------------------------------------------------------------------------------------------
	return true;
}
//------------------------------------------------------------------------------------------
function insFinish(){
//------------------------------------------------------------------------------------------
	return true;
}
//%InsChangeSel: Se envía mensaje de validación al no poder eliminar un registro
//------------------------------------------------------------------------------
function InsChangeSel(Field,nProvince,index){
//------------------------------------------------------------------------------
	if (Field.checked){
		insDefValues("Delete_MS109","nProvince=" + nProvince + "&nindex=" + index);
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
<FORM METHOD="post" ID="FORM" NAME="MS109_K" ACTION="valMantGeneral.aspx?mode=1">
<%
Response.Write("<SCRIPT>var	nMainAction	= 0" & Request.QueryString.Item("nMainAction") & "</SCRIPT>")

Call insDefineHeader()

If Request.QueryString.Item("Type") <> "PopUp" Then
	Call insPreMS109()
Else
	Call insPreMS109Upd()
End If

mobjGrid = Nothing
mobjValues = Nothing
%>
</FORM>
</BODY>
</HTML>





