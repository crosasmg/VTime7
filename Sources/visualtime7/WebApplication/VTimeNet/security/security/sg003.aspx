<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eSecurity" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores.

Dim mobjValues As eFunctions.Values
Dim mobjGrid As eFunctions.Grid
Dim mobjMenues As eFunctions.Menues

'- Variable de parametro para el control de ramo
Dim mintBranch As Object


'%insDefineHeader:Permite definir las columnas del grid, así como habilitar o inhabilitar el 
'%botón de eliminar y registrar.
'-----------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'-----------------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	
	'+ Se definen las columnas del Grid.
	
	With mobjGrid
		.sCodisplPage = "SG003"
		.Codispl = Request.QueryString.Item("sCodispl")
		.Codisp = "SG003"
		.Height = 310
		.Width = 400
		.AddButton = True
		.DeleteButton = True
	End With
	
	With mobjGrid.Columns
		Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeCurrencyColumnCaption"), "cbeCurrency", "table11", eFunctions.Values.eValuesType.clngComboType,  ,  False,  ,  ,  , "ChangeFields(this)",  ,  , GetLocalResourceObject("cbeCurrencyColumnCaption"))
		Call .AddBranchColumn(0, GetLocalResourceObject("cbeBranchColumnCaption"), "cbeBranch", GetLocalResourceObject("cbeBranchColumnToolTip"))
		Call .AddProductColumn(0, GetLocalResourceObject("valProductColumnCaption"), "valProduct", GetLocalResourceObject("valProductColumnToolTip"))
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnIssuelimColumnCaption"), "tcnIssuelim", 12, CStr(0), False, GetLocalResourceObject("tcnIssuelimColumnToolTip"), True)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnClaim_dColumnCaption"), "tcnClaim_d", 14, CStr(0), False, GetLocalResourceObject("tcnClaim_dColumnToolTip"), True, 2)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnClaim_pColumnCaption"), "tcnClaim_p", 14, CStr(0), False, GetLocalResourceObject("tcnClaim_pColumnToolTip"), True, 2)
	End With
	
	With mobjGrid
		.Columns("cbeCurrency").Parameters.Add("sSche_code", Session("sSche_codeWin"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 6, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Columns("cbeBranch").Parameters.Add("nBranch", mintBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		
		If Request.QueryString.Item("Action") = "Update" Then
			.Columns("cbeCurrency").disabled = True
			.Columns("cbeBranch").disabled = True
			.Columns("valProduct").disabled = True
		End If
		
		If Request.QueryString.Item("Action") = "Add" Then
			Call UpdateFields(Request.QueryString.Item("Action"))
		End If
		
		'+ Si la acción que viaja a través del QueryString es Consulta (401), Elimiación (303) o el
		'+ parámetro nMainAction tiene valor NULO (vbNUllString o ""), la propiedad ActionQuery se setea en TRUE,
		'+ de lo contrario se setea en FALSE
		If Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionQuery) Or Request.QueryString.Item("nMainAction") = vbNullString Or CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 303 Then
			.Columns("Sel").GridVisible = False
			.ActionQuery = True
		Else
			.Columns("Sel").GridVisible = True
			.ActionQuery = False
		End If
		.Columns("cbeCurrency").EditRecord = True
		
		.sDelRecordParam = "nCurrency=' + marrArray[lintIndex].cbeCurrency + '&nBranch=' + marrArray[lintIndex].cbeBranch + '&nProduct=' + marrArray[lintIndex].valProduct + '"
		
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
End Sub

'%insPreSG003: Se definen los objetos a ser utilizados.
'-----------------------------------------------------------------------------------------
Private Sub insPreSG003()
	'-----------------------------------------------------------------------------------------
	Dim lintCount As Short
	Dim lintIndex As Object
	Dim lcolSecur_sches As eSecurity.Secur_sches
	Dim lclsSecur_sche As Object
	
	'+ Se setea el objeto y se realiza la lectura del o los registros a ser mostrados
	'+ en las columnas del grid.
	
	lcolSecur_sches = New eSecurity.Secur_sches
	If lcolSecur_sches.FindLimits(Session("sSche_codeWin"), True) Then
		lintCount = 0
		
		For	Each lclsSecur_sche In lcolSecur_sches
			With lclsSecur_sche
				mobjGrid.Columns("cbeCurrency").DefValue = .nCurrency
				mobjGrid.Columns("cbeBranch").DefValue = .nBranch
				mobjGrid.Columns("valProduct").DefValue = .nProduct
				mobjGrid.Columns("tcnIssuelim").DefValue = .nIssuelimit
				mobjGrid.Columns("tcnClaim_d").DefValue = .nClaim_dec
				mobjGrid.Columns("tcnClaim_p").DefValue = .nClaim_pay
				
				'+ Se ejecuta el metodo DoRow, que se encarga de mostrar los elementos en el grid
				Response.Write(mobjGrid.DoRow())
			End With
			lintCount = lintCount + 1
			If lintCount = 200 Then
				Exit For
			End If
		Next lclsSecur_sche
	End If
	
	Response.Write(mobjGrid.closeTable())
	
	lcolSecur_sches = Nothing
	lclsSecur_sche = Nothing
End Sub

'%insPreSG003Upd: Permite realizar el llamado a la ventana PopUp.
'-----------------------------------------------------------------------------------------
Private Sub insPreSG003Upd()
	'-----------------------------------------------------------------------------------------
	Dim lclsSecur_sche As eSecurity.Secur_sche
	If Request.QueryString.Item("Action") = "Del" Then
		Response.Write(mobjValues.ConfirmDelete())
		
		
		lclsSecur_sche = New eSecurity.Secur_sche
		
		Call lclsSecur_sche.insDelLimits_2(Session("sSche_codeWin"), CInt(Request.QueryString.Item("nCurrency")), CInt(Request.QueryString.Item("nBranch")), CInt(Request.QueryString.Item("nProduct")))
		lclsSecur_sche = Nothing
	End If
	Response.Write(mobjGrid.DoFormUpd(Request.QueryString.Item("Action"), "ValSecuritySeqSchema.aspx", Request.QueryString.Item("sCodispl"), Request.QueryString.Item("nMainAction"),  , CShort(Request.QueryString.Item("Index"))))
End Sub

'%UpdateFields: Permite desabilitar los campos de la ventana Popup.
'-----------------------------------------------------------------------------------------
Private Sub UpdateFields(ByVal Action As String)
	'-----------------------------------------------------------------------------------------
	With mobjGrid
		If Action = "Add" Then
			.Columns("cbeBranch").disabled = True
			.Columns("valProduct").disabled = True
			.Columns("tcnIssuelim").disabled = True
			.Columns("tcnClaim_d").disabled = True
			.Columns("tcnClaim_p").disabled = True
		End If
	End With
End Sub

</script>
<%
Response.Expires = -1

mobjValues = New eFunctions.Values
mobjValues.sCodisplPage = "SG003"
%>
<SCRIPT LANGUAGE="JavaScript">

//%insCancel: Permite cancelar la página.
//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
	return true;
}

//%ChangeFields: Habilita o deshabilita los campos de la ventana
//------------------------------------------------------------------------------------------
function ChangeFields(Field){
//------------------------------------------------------------------------------------------
	if (Field.value!=0)
	{
		with(self.document.forms[0])
		{
			cbeBranch.value = "";
			valProduct.value = "";
		    cbeBranch.disabled = false;
			valProduct.disabled = false;
			btnvalProduct.disabled = false;
			tcnIssuelim.disabled = false;
			tcnClaim_d.disabled = false;
			tcnClaim_p.disabled = false;
		}
	}
	else
	{
		with(self.document.forms[0])
		{
			cbeBranch.value = "";
			valProduct.value = "";
		    cbeBranch.disabled = true;
			valProduct.disabled = true;
			btnvalProduct.disabled = true;
			tcnIssuelim.disabled = true;
			tcnClaim_d.disabled = true;
			tcnClaim_p.disabled = true;
		}
	}    
}
//- Variable apra el control de versiones
	document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 17.43 $|$$Author: Nvaplat60 $"

</SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>




<HTML>
<HEAD>
    <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">
<%
'+ Se realiza el llamado a las rutinas generales para cargar la página invocada.

mobjMenues = New eFunctions.Menues

If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write("<SCRIPT>var nMainAction=304</SCRIPT>")
	Response.Write(mobjMenues.setZone(2, "SG003", "SG003.aspx"))
End If

With Response
	.Write(mobjValues.WindowsTitle("SG003"))
	.Write(mobjValues.StyleSheet())
End With
%>

</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="SG003" ACTION="valSecuritySeqSchema.aspx?sTime=1&nMainAction=<%=Request.QueryString.Item("nMainAction")%>">
<%
Call insDefineHeader()

If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write(mobjValues.ShowWindowsName("SG003"))
	Call insPreSG003()
Else
	Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl")))
	Call insPreSG003Upd()
End If

mobjValues = Nothing
mobjGrid = Nothing
%>
</FORM>
</BODY>
</HTML>




