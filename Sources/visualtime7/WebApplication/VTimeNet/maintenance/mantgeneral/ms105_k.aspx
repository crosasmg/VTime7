<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eGeneralForm" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo de las rutinas genéricas

Dim mobjMenu As eFunctions.Menues
Dim mobjGrid As eFunctions.Grid


'% insDefineHeader: Define las columnas del Grid
'-----------------------------------------------
Private Sub insDefineHeader()
	'-----------------------------------------------
	
	mobjGrid = New eFunctions.Grid
	
	'+ Se definen las columnas del grid
	
	With mobjGrid.Columns
		Call .AddNumericColumn(100023, GetLocalResourceObject("tcnZip_codeColumnCaption"), "tcnZip_code", 4, "", True, GetLocalResourceObject("tcnZip_codeColumnToolTip"),  ,  ,  ,  ,  , Request.QueryString.Item("Action") = "Update")
		Call .AddPossiblesColumn(100022, GetLocalResourceObject("valLocalColumnCaption"), "valLocal", "tab_locat_a", eFunctions.Values.eValuesType.clngWindowType, "",  ,  ,  ,  ,  , Request.QueryString.Item("Action") = "Update",  , GetLocalResourceObject("valLocalColumnToolTip"))
		Call .AddPossiblesColumn(100022, GetLocalResourceObject("cbeOfficeColumnCaption"), "cbeOffice", "tabOffice", eFunctions.Values.eValuesType.clngComboType, "",  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeOfficeColumnToolTip"))
		Call .AddNumericColumn(100024, GetLocalResourceObject("tcnOrderColumnCaption"), "tcnOrder", 2, "", True, GetLocalResourceObject("tcnOrderColumnToolTip"))
		Call .AddNumericColumn(100025, GetLocalResourceObject("tcnAuto_zoneColumnCaption"), "tcnAuto_zone", 2, "", True, GetLocalResourceObject("tcnAuto_zoneColumnToolTip"))
	End With
	
	'+ Se asignan las caracteristicas del Grid
	
	With mobjGrid
		.Codisp = "MS105_K"
		.Codispl = "MS105"
		.sCodisplPage = "MS105"
		.Width = 350
		.Height = 300
		
		If Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionQuery) Or Request.QueryString.Item("nMainAction") = vbNullString Then
			.ActionQuery = True
		End If
		
		.nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
		.Columns("Sel").GridVisible = Not .ActionQuery
		.Columns("valLocal").EditRecord = True
		.sDelRecordParam = "nZip_code='+ marrArray[lintIndex].tcnZip_code + '" & "&nLocal='+ marrArray[lintIndex].valLocal + '"
		
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
End Sub

'% insPreMS105: Carga los datos en el grid de la forma "Folder"
'--------------------------------------------------------------
Private Sub insPreMS105()
	'--------------------------------------------------------------
	
	Dim lcolZip_codes As eGeneralForm.Zip_codes
	Dim lclsZip_code As Object
	
	lcolZip_codes = New eGeneralForm.Zip_codes
	
	If lcolZip_codes.Find() Then
		For	Each lclsZip_code In lcolZip_codes
			With mobjGrid
				.Columns("tcnZip_code").DefValue = lclsZip_code.nZip_code
				.Columns("valLocal").DefValue = lclsZip_code.nLocal
				.Columns("cbeOffice").DefValue = lclsZip_code.nOffice
				.Columns("tcnOrder").DefValue = lclsZip_code.nOrder
				.Columns("tcnAuto_zone").DefValue = lclsZip_code.nAuto_zone
			End With
			
			'+ Se ejecuta el metodo DoRow, que se encarga de mostrar los elementos de grid
			
			Response.Write(mobjGrid.DoRow())
		Next lclsZip_code
	End If
	Response.Write(mobjGrid.closeTable())
End Sub

'% insPreMS105Upd: Gestiona			 lo relacionado a la actualización de un registro del Grid
'------------------------------------------------------------------------------------
Private Sub insPreMS105Upd()
	'------------------------------------------------------------------------------------
	
	Dim lclsZip_code As eGeneralForm.Zip_code
	Dim lclsErrors As eFunctions.Errors
	
	
	If Request.QueryString.Item("Action") = "Del" Then
		lclsZip_code = New eGeneralForm.Zip_code
		lclsErrors = New eFunctions.Errors
		
		lclsErrors.Highlighted = True
		
		If lclsZip_code.Find_Address_ZipLocal_a(CInt(Request.QueryString.Item("nZip_code")), CInt(Request.QueryString.Item("nLocal"))) Then
			Response.Write(lclsErrors.ErrorMessage(Request.QueryString.Item("sCodispl"), 10834,  ,  ,  , True))
		Else
			Response.Write(mobjValues.ConfirmDelete())
			With lclsZip_code
				.nZip_code = mobjValues.StringToType(Request.QueryString.Item("nZip_code"), eFunctions.Values.eTypeData.etdDouble)
				.nLocal = mobjValues.StringToType(Request.QueryString.Item("nLocal"), eFunctions.Values.eTypeData.etdDouble)
				Call .Delete()
			End With
		End If
		
		lclsZip_code = Nothing
		lclsErrors = Nothing
	End If
	Response.Write(mobjGrid.DoFormUpd(Request.QueryString.Item("Action"), "valMantGeneral.aspx", Request.QueryString.Item("sCodispl"), Request.QueryString.Item("MainAction"),  , CShort(Request.QueryString.Item("Index"))))
End Sub

</script>
<%
Response.Expires = -1

mobjValues = New eFunctions.Values
mobjValues.sCodisplPage = "MS105"
%>

<HTML>
<HEAD>
    <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>




<%
With Response
	.Write(mobjValues.StyleSheet())
	
	If Request.QueryString.Item("Type") <> "PopUp" Then
		.Write("<SCRIPT LANGUAGE=""JavaScript"" SRC=""/VTimeNet/Scripts/tmenu.js""></SCRIPT>")
		
		mobjMenu = New eFunctions.Menues
		.Write(mobjMenu.MakeMenu("MS105", "MS105_K.aspx", 1, ""))
		.Write("<SCRIPT>var nMainAction = top.frames['fraSequence'].plngMainAction</SCRIPT>")
		mobjMenu = Nothing
	End If
End With
%>

<SCRIPT>
//-------------------------------------------------------------------------------------------------------------------
function insStateZone(){}
//-------------------------------------------------------------------------------------------------------------------

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
<%
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write("<BR><BR>")
End If
Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl")))
%>
	<FORM METHOD="POST" ID="FORM" NAME="MS105_K" ACTION="valMantGeneral.aspx?mode=1">
<%
Call insDefineHeader()

If Request.QueryString.Item("Type") <> "PopUp" Then
	Call insPreMS105()
Else
	Call insPreMS105Upd()
End If
%>
	</FORM>
</BODY>
</HTML>

<%
mobjGrid = Nothing
mobjValues = Nothing
%>




