<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eClaim" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores

Dim mobjValues As eFunctions.Values

'- Objeto para el manejo de las rutinas genéricas

Dim mobjGrid As eFunctions.Grid

'- Objeto para el manejo de las zonas de la pantalla
Dim mobjMenues As eFunctions.Menues



'+ Definición de las Columnas del Grid
'------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	
	'+Se definen las columnas del Grid
	
	With mobjGrid.Columns
		Call .AddNumericColumn(101782, GetLocalResourceObject("tcnCausecodColumnCaption"), "tcnCausecod", 4, CStr(eRemoteDB.Constants.strNull), True, GetLocalResourceObject("tcnCausecodColumnToolTip"),  ,  ,  ,  ,  , Request.QueryString.Item("Action") = "Update")
		Call .AddTextColumn(101783, GetLocalResourceObject("tctDescriptColumnCaption"), "tctDescript", 30, CStr(eRemoteDB.Constants.strNull),  , GetLocalResourceObject("tctDescriptColumnToolTip"))
		Call .AddTextColumn(101784, GetLocalResourceObject("tctShort_desColumnCaption"), "tctShort_des", 12, CStr(eRemoteDB.Constants.strNull),  , GetLocalResourceObject("tctShort_desColumnToolTip"))
		Call .AddCheckColumn(101785, GetLocalResourceObject("chkPart_lossColumnCaption"), "chkPart_loss", "",  , "1")
		Call .AddCheckColumn(101786, GetLocalResourceObject("chkTotal_lossColumnCaption"), "chkTotal_loss", "",  , "1")
		Call .AddPossiblesColumn(101781, GetLocalResourceObject("cbeStatregtColumnCaption"), "cbeStatregt", "Table26", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeStatregtColumnToolTip"))
	End With
	
	'+Se asignan las caracteristicas del Grid
	
	With mobjGrid
		'+Se crean los parametros para las listas de valores posibles
		.Columns("tctDescript").EditRecord = True
		.Columns("cbeStatregt").TypeList = 2
		.Columns("cbeStatregt").List = "2"
		.Codispl = "MSI010"
		.Codisp = "MSI010"
		.sCodisplPage = "MSI010"
		If Request.QueryString.Item("nMainAction") = "401" Or Request.QueryString.Item("nMainAction") = vbNullString Then
			mobjGrid.ActionQuery = True
			mobjGrid.Columns("Sel").GridVisible = False
		End If
		If Request.QueryString.Item("Action") <> "Add" And Request.QueryString.Item("Action") <> "Update" Then
			.Columns("chkPart_loss").Disabled = True
			.Columns("chkTotal_loss").Disabled = True
		End If
		'+Pase de parametros necesarios para la eliminación de registros
		.sDelRecordParam = "nBranch=" & Session("nBranch") & "&nProduct=" & Session("nProduct") & "&nCausecod='+marrArray[lintIndex].tcnCausecod + '"
		.Height = 300
		.Width = 350
		
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
	
End Sub

'+ Proceso que carga los datos del Grid
'------------------------------------------------------------------------------
Private Sub insPreMSI010()
	'------------------------------------------------------------------------------
	Dim lcolClaim_causs As eClaim.Claim_causs
	Dim lclsClaim_caus As Object
	
	lcolClaim_causs = New eClaim.Claim_causs
	
	If CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 306 Then
		Call lcolClaim_causs.Find(mobjValues.StringToType(Session("nLastBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nLastProduct"), eFunctions.Values.eTypeData.etdDouble))
	Else
		Call lcolClaim_causs.Find(mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble))
	End If
	For	Each lclsClaim_caus In lcolClaim_causs
		With mobjGrid
			.Columns("tcnCauseCod").DefValue = lclsClaim_caus.nCausecod
			.Columns("tctDescript").DefValue = lclsClaim_caus.sDescript
			.Columns("tctShort_des").DefValue = lclsClaim_caus.sShort_des
			.Columns("chkPart_loss").Checked = lclsClaim_caus.sPartial_loss
			.Columns("chkTotal_loss").Checked = lclsClaim_caus.sTotal_loss
			.Columns("cbeStatregt").DefValue = lclsClaim_caus.sStatregt
		End With
		
		'+Se ejecuta el metodo DoRow, que se encarga de mostrar los elementos de grid
		Response.Write(mobjGrid.DoRow())
	Next lclsClaim_caus
	
	Response.Write(mobjGrid.closeTable())
	lcolClaim_causs = Nothing
	
End Sub

'+ Proceso de Actualizacion del Registro del Grid
'------------------------------------------------------------------------------
Private Sub insPreMSI010Upd()
	'------------------------------------------------------------------------------
	Dim lclsClaim_caus As eClaim.Claim_caus
	Dim lstrErrors As String
	Dim mstrCommand As String
	
	mstrCommand = "&sModule=Maintenance&sProject=MantClaim&sCodisplReload=" & Request.QueryString.Item("sCodispl")
	
	If Request.QueryString.Item("Action") = "Del" Then
		lclsClaim_caus = New eClaim.Claim_caus
		lstrErrors = lclsClaim_caus.insValMSI010("MSI010", "Del", mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), CInt(Request.QueryString.Item("nCausecod")))
		If lstrErrors = vbNullString Then
			Response.Write(mobjValues.ConfirmDelete())
			With lclsClaim_caus
				.nBranch = mobjValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble)
				.nProduct = mobjValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble)
				.nCausecod = mobjValues.StringToType(Request.QueryString.Item("nCausecod"), eFunctions.Values.eTypeData.etdDouble)
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
		lclsClaim_caus = Nothing
	End If
	With Response
		.Write(mobjGrid.DoFormUpd(Request.QueryString.Item("Action"), "valMantClaim.aspx", Request.QueryString.Item("sCodispl"), Request.QueryString.Item("MainAction"),  , CShort(Request.QueryString.Item("Index"))))
		.Write(mobjValues.HiddenControl("sAction", Request.QueryString.Item("Action")))
	End With
End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjValues.sCodisplPage = "MSI010"

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
	Response.Write(mobjMenues.setZone(2, "MSI010", "MSI010"))
	mobjMenues = Nothing
End If
%>

<SCRIPT>

//% insStateZone: se controla el estado de los campos de la página
//-------------------------------------------------------------------------------------------------------------------
function insStateZone(){
//-------------------------------------------------------------------------------------------------------------------
	return true;
}
//% insPreZone: Modifica el comportamiento de la página dependiendo de la acción
//% que proviene del menú principal
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
//-Variable para el control de Versiones
document.VssVersion="$$Revision: 3 $|$$Date: 15/10/03 15:53 $|$$Author: Nvaplat61 $"

</SCRIPT>
</HEAD>

<BODY ONUNLOAD="closeWindows();">

<FORM METHOD="POST" ID="FORM" NAME="frmClaimCauses" ACTION="ValMantClaim.aspx?mode=1">
<%
Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl")))
Call insDefineHeader()
If Request.QueryString.Item("Type") <> "PopUp" Then
	Call insPreMSI010()
Else
	Call insPreMSI010Upd()
End If
mobjGrid = Nothing
mobjValues = Nothing
%>
</FORM>
</BODY>
</HTML>




