<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eCashBank" %>
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
			.AddPossiblesColumn(0, GetLocalResourceObject("valConceptColumnCaption"), "valConcept", "table22", eFunctions.Values.eValuesType.clngWindowType,  ,  ,  ,  ,  ,  , False,  , GetLocalResourceObject("valConceptColumnToolTip"))
			.AddPossiblesColumn(0, GetLocalResourceObject("cbeBranchColumnCaption"), "cbeBranch", "Table10", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  , False,  , GetLocalResourceObject("cbeBranchColumnToolTip"))
			.AddPossiblesColumn(0, GetLocalResourceObject("valDocTypColumnCaption"), "valDocTyp", "table5587", eFunctions.Values.eValuesType.clngWindowType, "",  ,  ,  ,  ,  , False,  , GetLocalResourceObject("valDocTypColumnToolTip"))
			.AddPossiblesColumn(0, GetLocalResourceObject("valDefaultDatColumnCaption"), "valDefaultDat", "table5640", eFunctions.Values.eValuesType.clngWindowType, "",  ,  ,  ,  ,  , False,  , GetLocalResourceObject("valDefaultDatColumnToolTip"))
			.AddPossiblesColumn(0, GetLocalResourceObject("valChangesDatColumnCaption"), "valChangesDat", "table5642", eFunctions.Values.eValuesType.clngWindowType, "",  ,  ,  ,  ,  , False,  , GetLocalResourceObject("valChangesDatColumnToolTip"))
			.AddHiddenColumn("hddId", "0")
			
		End With
		
		.Height = 290
		.Width = 420
		.Codispl = "MOP822"
		.Codisp = "MOP822_K"
		.sCodisplPage = "MOP822"
		.ActionQuery = mobjValues.ActionQuery
		.Columns("Sel").GridVisible = Not .ActionQuery
		.Columns("valDefaultDat").EditRecord = Not .ActionQuery
		
		.sDelRecordParam = "nId='+ marrArray[lintIndex].hddId + '"
		
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
End Sub

'% insPreMOP822: Carga los datos en el grid de la forma 
'------------------------------------------------------------------------------------------
Private Sub insPreMOP822()
	'------------------------------------------------------------------------------------------
	Dim lcolValdatconditionss As eCashBank.Valdatconditionss
	Dim lclsValdatconditions As eCashBank.Valdatconditions
	
	lcolValdatconditionss = New eCashBank.Valdatconditionss
	lclsValdatconditions = New eCashBank.Valdatconditions
	
	If lcolValdatconditionss.Find() Then
		With mobjGrid
			For	Each lclsValdatconditions In lcolValdatconditionss
				
				.Columns("valConcept").DefValue = CStr(lclsValdatconditions.nConcept)
				.Columns("cbeBranch").DefValue = CStr(lclsValdatconditions.nBranch)
				.Columns("valDocTyp").DefValue = CStr(lclsValdatconditions.nDocTyp)
				.Columns("valDefaultDat").DefValue = CStr(lclsValdatconditions.nDefaultDat)
				.Columns("valChangesDat").DefValue = CStr(lclsValdatconditions.nChangesDat)
				.Columns("hddId").DefValue = CStr(lclsValdatconditions.nId)
				
				.Columns("Sel").OnClick = "InsChangeSel(this," & lclsValdatconditions.nDefaultDat & ",this.value)"
				
				.sEditRecordParam = "nId=" & lclsValdatconditions.nId
				
				Response.Write(.DoRow)
			Next lclsValdatconditions
		End With
	End If
	Response.Write(mobjGrid.CloseTable)
	Response.Write(mobjValues.BeginPageButton)
	
	lcolValdatconditionss = Nothing
	lclsValdatconditions = Nothing
End Sub

'% insPreMOP822Upd: Actualiza un registro en el grid
'------------------------------------------------------------------------------------------
Private Sub insPreMOP822Upd()
	'------------------------------------------------------------------------------------------
	Dim lobjError As Object
	Dim lclsValdatconditions As eCashBank.Valdatconditions
	
	With Request
		If .QueryString.Item("Action") = "Del" Then
			lclsValdatconditions = New eCashBank.Valdatconditions
			
			If lclsValdatconditions.Delete(CInt(.QueryString.Item("nId"))) Then
				Response.Write(mobjValues.ConfirmDelete())
			End If
			
			lclsValdatconditions = Nothing
		End If
		
		Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "valMantcashbank.aspx", .QueryString.Item("sCodispl"), .QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(.QueryString.Item("Index"))))
	End With
	
End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjValues.sCodisplPage = "MOP822"
%>

<HTML>
<HEAD>
<SCRIPT>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 1 $|$$Date: 2/09/03 19:02 $|$$Author: Iusr_llanquihue $"
</SCRIPT>
<META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></SCRIPT>




		
<%
If Request.QueryString.Item("Type") <> "PopUp" Then
	%>
		<%	'$$EWI_1012:D:\VisualTIMEChile\Result\VTimeStep1\maintenance\mantcashbank\Vtime\Scripts\tMenu.js#%>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tMenu.js"></SCRIPT>
<%	
End If
Response.Write(mobjValues.StyleSheet())

If Request.QueryString.Item("Type") <> "PopUp" Then
	mobjMenu = New eFunctions.Menues
	With Response
		.Write(mobjMenu.MakeMenu("MOP822", "MOP822_K.aspx", 1, ""))
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
function InsChangeSel(Field,valDefaultDat,index){
//------------------------------------------------------------------------------
	if (Field.checked){
		insDefValues("Delete_MOP822","valDefaultDat=" + valDefaultDat + "&nindex=" + index);
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
<FORM METHOD="post" ID="FORM" NAME="MOP822_K" ACTION="valMantcashbank.aspx?mode=1">
<%
Response.Write("<SCRIPT>var	nMainAction	= 0" & Request.QueryString.Item("nMainAction") & "</SCRIPT>")

Call insDefineHeader()

If Request.QueryString.Item("Type") <> "PopUp" Then
	Call insPreMOP822()
Else
	Call insPreMOP822Upd()
End If

mobjGrid = Nothing
mobjValues = Nothing
%>
</FORM>
</BODY>
</HTML>





