<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eClaim" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo de las rutinas genéricas

Dim mobjMenu As eFunctions.Menues
Dim mobjGrid As eFunctions.Grid



'% insDefineHeader: Definición del Grid
'------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	
	'+ Se definen las columns del Grid
	With mobjGrid.Columns
		Call .AddPossiblesColumn(101801, GetLocalResourceObject("cbeOper_typeColumnCaption"), "cbeOper_type", "Table140", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  , Request.QueryString.Item("Action") = "Update")
		If Request.QueryString.Item("Type") <> "PopUp" Then
			Call .AddCheckColumn(101807, GetLocalResourceObject("chkInd_revColumnCaption"), "chkInd_rev", "",  ,  ,  , True)
		Else
			Call .AddCheckColumn(101808, GetLocalResourceObject("chkInd_revColumnCaption"), "chkInd_rev", "")
		End If
		Call .AddHiddenColumn("tcnChecked", CStr(0))
		Call .AddHiddenColumn("tcnRChecked", "")
		Call .AddPossiblesColumn(101890, GetLocalResourceObject("cbeGen_operaColumnCaption"), "cbeGen_opera", "Table140", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  , Request.QueryString.Item("Action") = "Add")
		Call .AddPossiblesColumn(101803, GetLocalResourceObject("cbeReserve_inColumnCaption"), "cbeReserve_in", "Table294", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  , Request.QueryString.Item("Action") = "Add")
		Call .AddPossiblesColumn(101804, GetLocalResourceObject("cbePay_indColumnCaption"), "cbePay_ind", "Table294", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  , Request.QueryString.Item("Action") = "Add")
		Call .AddPossiblesColumn(101805, GetLocalResourceObject("cbeRec_esp_inColumnCaption"), "cbeRec_esp_in", "Table294", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  , Request.QueryString.Item("Action") = "Add")
		Call .AddPossiblesColumn(101806, GetLocalResourceObject("cbeRecover_inColumnCaption"), "cbeRecover_in", "Table294", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  , Request.QueryString.Item("Action") = "Add")
		
	End With
	
	'+ Se asignan las caracteristicas del Grid
	
	With mobjGrid
		.Codispl = Request.QueryString.Item("sCodispl")
		.Codisp = "MSI016_K"
		.sCodisplPage = "MSI016"
		.Columns("chkInd_rev").OnClick = "insCheckClick(this)"
		If Request.QueryString.Item("nMainAction") = "401" Or Request.QueryString.Item("nMainAction") = vbNullString Then
			mobjGrid.ActionQuery = True
			mobjGrid.Columns("Sel").GridVisible = False
		Else
			.Columns("cbeOper_type").EditRecord = True
			.Columns("Sel").Title = "Sel"
		End If
		.sDelRecordParam = "nOper_type='+ marrArray[lintIndex].cbeOper_type + '"
		.Height = 320
		.Width = 400
		.Top = 100
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
	
End Sub

'% insPreMSI016: Función que carga todos los valores en el Grid
'------------------------------------------------------------------------------
Private Sub insPreMSI016()
	'------------------------------------------------------------------------------
	Dim lcolTab_ClaRevconds As eClaim.Tab_ClaRevconds
	Dim lclsTab_ClaRevcond As eClaim.Tab_ClaRevcond
	Dim lintRecord_item As Short
	
	lcolTab_ClaRevconds = New eClaim.Tab_ClaRevconds
	lclsTab_ClaRevcond = New eClaim.Tab_ClaRevcond
	
	If lcolTab_ClaRevconds.Find Then
		lintRecord_item = 0
		For	Each lclsTab_ClaRevcond In lcolTab_ClaRevconds
			With mobjGrid
				.Columns("tcnChecked").DefValue = CStr(1)
				.Columns("cbeOper_type").DefValue = CStr(lclsTab_ClaRevcond.nOper_type)
				.Columns("chkInd_rev").Checked = lclsTab_ClaRevcond.nInd_rev
				.Columns("tcnRChecked").DefValue = CStr(lclsTab_ClaRevcond.nInd_rev)
				.Columns("cbeGen_opera").DefValue = CStr(lclsTab_ClaRevcond.nGen_opera)
				.Columns("cbeReserve_in").DefValue = CStr(lclsTab_ClaRevcond.nReserve_in)
				.Columns("cbePay_ind").DefValue = CStr(lclsTab_ClaRevcond.npay_ind)
				.Columns("cbeRec_esp_in").DefValue = CStr(lclsTab_ClaRevcond.nRec_esp_in)
				.Columns("cbeRecover_in").DefValue = CStr(lclsTab_ClaRevcond.nRecover_in)
			End With
			
			'+Se ejecuta el metodo DoRow, que se encarga de mostrar los elementos de grid
			mobjGrid.Columns("chkInd_rev").OnClick = "insCheckClick(this," & lintRecord_item & ")"
			lintRecord_item = lintRecord_item + 1
			Response.Write(mobjGrid.DoRow())
		Next lclsTab_ClaRevcond
	End If
	Response.Write(mobjGrid.closeTable())
	
	Response.Write(mobjValues.BeginPageButton)
	
	lcolTab_ClaRevconds = Nothing
	lclsTab_ClaRevcond = Nothing
End Sub

'% insPreMSI016Upd: Función que Actualiza un Registro del Grid
'------------------------------------------------------------------------------
Private Sub insPreMSI016Upd()
	'------------------------------------------------------------------------------
	Dim lclsTab_ClaRevcond As eClaim.Tab_ClaRevcond
	lclsTab_ClaRevcond = New eClaim.Tab_ClaRevcond
	If Request.QueryString.Item("Action") = "Del" Then
		Response.Write(mobjValues.ConfirmDelete())
		
		With lclsTab_ClaRevcond
			.nOper_type = CInt(Request.QueryString.Item("nOper_type"))
			.Delete()
		End With
	End If
	lclsTab_ClaRevcond = Nothing
	Response.Write(mobjGrid.DoFormUpd(Request.QueryString.Item("Action"), "valMantClaim.aspx", Request.QueryString.Item("sCodispl"), Request.QueryString.Item("nMainAction"),  , CShort(Request.QueryString.Item("Index"))))
End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjValues.sCodisplPage = "MSI016"
%>
<HTML>
<HEAD>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/ValFunctions.js"></SCRIPT>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">


<%
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write("<SCRIPT>var nMainAction=0</SCRIPT>")
	Response.Write("<SCRIPT LANGUAGE=""JavaScript"" SRC=""/VTimeNet/Scripts/tmenu.js""></SCRIPT>" & vbCrLf)
End If
With Response
	.Write(mobjValues.StyleSheet() & vbCrLf)
	If Request.QueryString.Item("Type") <> "PopUp" Then
		mobjMenu = New eFunctions.Menues
		.Write(mobjMenu.MakeMenu(Request.QueryString.Item("sCodispl"), "MSI016_K.aspx", 1, ""))
		mobjMenu = Nothing
	End If
End With
%>
<SCRIPT>
//% insCheckClick : Controla el Check del Campo tcnRChecked
//-------------------------------------------------------------------------------------------
function insCheckClick(Field, nIndex){
//-------------------------------------------------------------------------------------------
	with(self.document.forms[0]){
		if(typeof(nIndex)!='undefined')
			tcnRChecked(nIndex).value = (Field.checked?1:2);
		cbeGen_opera.disabled = (Field.checked?false:true);
		cbeReserve_in.disabled = (Field.checked?false:true);
		cbePay_ind.disabled = (Field.checked?false:true);
		cbeRec_esp_in.disabled = (Field.checked?false:true);
		cbeRecover_in.disabled = (Field.checked?false:true);
		
		if(!Field.checked){
			cbeGen_opera.value = 0
			cbeReserve_in.value = 0
			cbePay_ind.value = 0
			cbeRec_esp_in.value = 0
			cbeRecover_in.value = 0
		}
    }
}
//% insCancel: se controla la acción Cancelar de la página
//-------------------------------------------------------------------------------------------
function insCancel(){
//-------------------------------------------------------------------------------------------
    return true
}

//% insStateZone: se controla el estado de los campos de la página
//-------------------------------------------------------------------------------------------
function insStateZone(){}
//-------------------------------------------------------------------------------------------

//% insPreZone: Modifica el comportamiento de la página dependiendo de la acción
//% que proviene del menú principal
//-------------------------------------------------------------------------------------------
function insPreZone(nAction){
//-------------------------------------------------------------------------------------------
	switch (nAction){
	    case 301:
	    case 302:
	    case 401:
	        document.location.href = document.location.href.replace(/&nMainAction.*/,'') + '&nMainAction=' + nAction
	        break;
	}
}
//+ Esta línea guarda la versión procedente de VSS 
   document.VssVersion="$$Revision: 1 $|$$Date: 2/09/03 19:03 $" 
</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<%
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write("<BR></BR>")
End If
Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl")))
%>
<FORM METHOD="post" ID="FORM" NAME="frmTabClaRevCond" ACTION="valMantClaim.aspx?sMode=1">
<%
Call insDefineHeader()
If Request.QueryString.Item("Type") <> "PopUp" Then
	Call insPreMSI016()
Else
	Call insPreMSI016Upd()
End If
mobjValues = Nothing
mobjGrid = Nothing
%>
</FORM>
</BODY>
</HTML>




