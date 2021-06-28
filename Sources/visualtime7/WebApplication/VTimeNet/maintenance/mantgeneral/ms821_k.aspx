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
			.AddPossiblesColumn(100022, GetLocalResourceObject("valMonthColumnCaption"), "valMonth", "table7013", eFunctions.Values.eValuesType.clngWindowType, "",  ,  ,  ,  ,  , Request.QueryString.Item("Action") = "Update",  , GetLocalResourceObject("valMonthColumnToolTip"))
			.AddNumericColumn(101895, GetLocalResourceObject("tcnDayColumnCaption"), "tcnDay", 5, vbNullString, True, GetLocalResourceObject("tcnDayColumnCaption"), False,  ,  ,  ,  , Request.QueryString.Item("Action") = "Update")
			.AddTextColumn(101896, GetLocalResourceObject("tctDescriptColumnCaption"), "tctDescript", 60, vbNullString, True, GetLocalResourceObject("tctDescriptColumnToolTip"))
			.AddPossiblesColumn(0, GetLocalResourceObject("valCountryColumnCaption"), "valCountry", "table66", eFunctions.Values.eValuesType.clngWindowType, "",  ,  ,  ,  ,  , Request.QueryString.Item("Action") = "Update",  , GetLocalResourceObject("valCountryColumnToolTip"))
		End With
		
		.Height = 300
		.Width = 400
		.Codispl = "MS821"
		.Codisp = "MS821_K"
		.sCodisplPage = "MS821"
		.ActionQuery = mobjValues.ActionQuery
		.Columns("Sel").GridVisible = Not .ActionQuery
		.Columns("tctDescript").EditRecord = Not .ActionQuery
		.sDelRecordParam = "valMonth='+ marrArray[lintIndex].valMonth + '" & "&tcnDay='+ marrArray[lintIndex].tcnDay + '" & "&valCountry='+ marrArray[lintIndex].valCountry + '"
		
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
End Sub

'% insPreMS821: Carga los datos en el grid de la forma 
'------------------------------------------------------------------------------------------
Private Sub insPreMS821()
	'------------------------------------------------------------------------------------------
	Dim lcolHollidays As eGeneralForm.Hollidayss
	Dim lclsHollidays As eGeneralForm.Hollidays
	
	lcolHollidays = New eGeneralForm.Hollidayss
	lclsHollidays = New eGeneralForm.Hollidays
	
	If lcolHollidays.Find() Then
		With mobjGrid
			For	Each lclsHollidays In lcolHollidays
				.Columns("valMonth").DefValue = CStr(lclsHollidays.nMonth)
				.Columns("tcnDay").DefValue = CStr(lclsHollidays.nDay)
				.Columns("tctDescript").DefValue = lclsHollidays.sDescript
				.Columns("Sel").OnClick = "InsChangeSel(this," & lclsHollidays.nMonth & ",this.value)"
				.Columns("valCountry").DefValue = CStr(lclsHollidays.nCountry)
				Response.Write(.DoRow)
			Next lclsHollidays
		End With
	End If
	Response.Write(mobjGrid.CloseTable)
	Response.Write(mobjValues.BeginPageButton)
	
	lcolHollidays = Nothing
	lclsHollidays = Nothing
End Sub

'% insPreMS821Upd: Actualiza un registro en el grid
'------------------------------------------------------------------------------------------
Private Sub insPreMS821Upd()
	'------------------------------------------------------------------------------------------
	Dim lobjError As Object
	Dim lclsHollidays As eGeneralForm.Hollidays
	
	With Request
		If .QueryString.Item("Action") = "Del" Then
			lclsHollidays = New eGeneralForm.Hollidays
			If lclsHollidays.Delete(CInt(.QueryString.Item("valMonth")), CInt(.QueryString.Item("tcnDay")), CInt(.QueryString.Item("valCountry"))) Then
				Response.Write(mobjValues.ConfirmDelete())
			End If
			
			lclsHollidays = Nothing
		End If
		Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "valMantGeneral.aspx", .QueryString.Item("sCodispl"), .QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(.QueryString.Item("Index"))))
	End With
	
End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjValues.sCodisplPage = "MS821"
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
		.Write(mobjMenu.MakeMenu("MS821", "MS821_K.aspx", 1, ""))
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
function InsChangeSel(Field,nMonth,index){
//------------------------------------------------------------------------------
	if (Field.checked){
		insDefValues("Delete_MS821","nMonth=" + nMonth + "&nindex=" + index);
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
<FORM METHOD="post" ID="FORM" NAME="MS821_K" ACTION="valMantGeneral.aspx?mode=1">
<%
Response.Write("<SCRIPT>var	nMainAction	= 0" & Request.QueryString.Item("nMainAction") & "</SCRIPT>")

Call insDefineHeader()

If Request.QueryString.Item("Type") <> "PopUp" Then
	Call insPreMS821()
Else
	Call insPreMS821Upd()
End If

mobjGrid = Nothing
mobjValues = Nothing
%>
</FORM>
</BODY>
</HTML>





