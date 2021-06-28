<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eClaim" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjMantClaim As eClaim.Tab_docu
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo de las rutinas genéricas
Dim mobjGrid As eFunctions.Grid

'- Objeto para el manejo de las zonas de la página
Dim mobjMenu As eFunctions.Menues


'%insDefineHeader: Definición de columnas del GRID
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	
	With mobjGrid.Columns
		Call .AddNumericColumn(0, GetLocalResourceObject("tctnDoc_codeColumnCaption"), "tctnDoc_code", 5, "",  , GetLocalResourceObject("tctnDoc_codeColumnToolTip"),  ,  ,  ,  ,  , Request.QueryString.Item("Action") = "Update")
		Call .AddTextColumn(0, GetLocalResourceObject("sDescriptColumnCaption"), "sDescript", 30, "",  , GetLocalResourceObject("sDescriptColumnToolTip"))
		Call .AddTextColumn(0, GetLocalResourceObject("tctsShort_desColumnCaption"), "tctsShort_des", 12, "",  , GetLocalResourceObject("tctsShort_desColumnToolTip"))
		
		If Request.QueryString.Item("Type") <> "PopUp" Then
			Call .AddCheckColumn(0, GetLocalResourceObject("chkClaimPayColumnCaption"), "chkClaimPay", "",  , "0",  , True)
		Else
			Call .AddCheckColumn(0, GetLocalResourceObject("chkClaimPayColumnCaption"), "chkClaimPay", "",  , "0")
		End If
		
		Call .AddPossiblesColumn(0, GetLocalResourceObject("tctsStratregtColumnCaption"), "tctsStratregt", "table26", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("tctsStratregtColumnToolTip"))
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnDays_prescColumnCaption"), "tcnDays_presc", 5, "",  , GetLocalResourceObject("tcnDays_prescColumnToolTip"))
		Call .AddHiddenColumn("sParam", vbNullString)
	End With
	
	'+Se definen todas las columnas del Grid
	
	With mobjGrid
		.Codispl = "MSI015"
		.sCodisplPage = "MSI015"
		.Top = 120
		.Left = 100
		.Width = 450
		.Height = 280
		.Columns("tctsStratregt").TypeList = 2
		.Columns("tctsStratregt").List = "2"
		.Columns("sDescript").EditRecord = True
		.DeleteButton = True
		.AddButton = True
		.nMainAction = mobjValues.StringToType(Request.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble)
		.sDelRecordParam = "' + marrArray[lintIndex].sParam + '"
		
		If Request.QueryString.Item("nMainAction") = "401" Then
			.bOnlyForQuery = True
			.ActionQuery = True
			.Columns("Sel").GridVisible = False
		End If
		
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
End Sub

'% insPreMSI015: Esta función se encarga de cargar los datos en la forma  
'-------------------------------------------------------------------------------------------------------
Private Sub insPreMSI015()
	'-------------------------------------------------------------------------------------------------------
	Dim lcolClaim As eClaim.Tab_docus
	Dim lclsClaim As eClaim.Tab_docu
	
	lcolClaim = New eClaim.Tab_docus
	lclsClaim = New eClaim.Tab_docu
	
	If CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 306 Then
		With mobjValues
			Call lcolClaim.Find(.StringToType(Session("nLastBranch"), eFunctions.Values.eTypeData.etdDouble), .StringToType(Session("nLastProduct"), eFunctions.Values.eTypeData.etdDouble), .StringToType(Session("nLastModulec"), eFunctions.Values.eTypeData.etdDouble), .StringToType(Session("nLastCover"), eFunctions.Values.eTypeData.etdDouble), .StringToType(Session("nLastCauscodcl"), eFunctions.Values.eTypeData.etdDouble))
		End With
	Else
		With mobjValues
			Call lcolClaim.Find(.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), .StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), .StringToType(Session("nModulec"), eFunctions.Values.eTypeData.etdDouble), .StringToType(Session("nCover"), eFunctions.Values.eTypeData.etdDouble), .StringToType(Session("nCauscodcl"), eFunctions.Values.eTypeData.etdDouble))
		End With
	End If
	For	Each lclsClaim In lcolClaim
		With mobjGrid
			.Columns("tctnDoc_code").DefValue = CStr(lclsClaim.nDoc_code)
			.Columns("sDescript").DefValue = lclsClaim.sDescript
			.Columns("tctsShort_des").DefValue = lclsClaim.sShort_des
			.Columns("chkClaimPay").Checked = CShort(lclsClaim.sClaimpay)
			.Columns("tctsStratregt").DefValue = lclsClaim.sStatregt
			.Columns("tcnDays_presc").DefValue = CStr(lclsClaim.nDays_presc)
			.sDelRecordParam = "' + marrArray[lintIndex].sParam + '"
			'+ Se "arma" un QueryString en la columna oculta sParam. Estos valores serán pasados a la 
			.Columns("sParam").DefValue = "&nBranch=" & Session("nBranch") & "&nProduct=" & Session("nProduct") & "&nModulec=" & Session("nModulec") & "&nCover=" & Session("nCover") & "&sCodispl=" & Request.QueryString.Item("scodispl") & "&tctnDoc_code=" & lclsClaim.nDoc_code & "&sDescript=" & lclsClaim.sDescript & "&tctsShort_des=" & lclsClaim.sShort_des & "&chkClaimPay=" & lclsClaim.sClaimpay & "&tctsStratregt=" & lclsClaim.sStatregt & "&tcnDays_presc=" & lclsClaim.nDays_presc
			
		End With
		Response.Write(mobjGrid.DoRow())
	Next lclsClaim
	Response.Write(mobjGrid.closeTable())
	lcolClaim = Nothing
	lclsClaim = Nothing
End Sub

'% insPreMSI015Upd: Esta función se encarga de Actualizar el Registro seleccionado en el Grid
'-------------------------------------------------------------------------------------------------------
Private Sub insPreMSI015Upd()
	'-------------------------------------------------------------------------------------------------------
	Dim lblnPost As Boolean
	Dim lstrValMSI015 As String
	Dim mstrCommand As String
	
	mstrCommand = "&sModule=Maintenance&sProject=MantClaim&sCodisplReload=" & Request.QueryString.Item("sCodispl")
	
	If Request.QueryString.Item("Action") = "Del" Then
		mobjMantClaim = New eClaim.Tab_docu
		With Request
			lstrValMSI015 = mobjMantClaim.insValMSI015(.QueryString.Item("sCodispl"), .QueryString.Item("Action"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nModulec"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCover"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCauscodcl"), eFunctions.Values.eTypeData.etdDouble), CInt(.QueryString.Item("tctnDoc_code")), .QueryString.Item("sDescript"), .QueryString.Item("tctsShort_des"), .QueryString.Item("tctsStratregt"), CInt(.QueryString.Item("tcnDays_presc")))
		End With
		If lstrValMSI015 > vbNullString Then
			Session("sErrorTable") = lstrValMSI015
			With Response
				.Write("<SCRIPT LANGUAGE=JAVASCRIPT>")
				.Write("ShowPopUp(""/VTimeNet/Common/Errors.aspx?sSource=" & Server.URLEncode(mstrCommand) & "&sQueryString=" & Server.URLEncode(Request.Params.Get("Query_String")) & """, ""MantClaimError"",660,330);self.document.location.href='/VTimeNet/Common/Blank.htm';top.window.close();")
				.Write("</" & "Script>")
			End With
		Else
			With Request
				lblnPost = mobjMantClaim.insPostMSI015(.QueryString.Item("sCodispl"), .QueryString.Item("Action"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nModulec"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCover"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCauscodcl"), eFunctions.Values.eTypeData.etdDouble), CInt(.QueryString.Item("tctnDoc_code")), .QueryString.Item("chkClaimPay"), .QueryString.Item("sDescript"), .QueryString.Item("tctsShort_des"), .QueryString.Item("tctsStratregt"), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), CInt(.QueryString.Item("tcnDays_presc")))
			End With
			Response.Write(mobjValues.ConfirmDelete())
			Response.Write(mobjGrid.DoFormUpd(Request.QueryString.Item("Action"), "valMantClaim.aspx", Request.QueryString.Item("sCodispl"), Request.QueryString.Item("nMainAction"),  , CShort(Request.QueryString.Item("Index"))))
		End If
		mobjMantClaim = Nothing
	Else
		Response.Write(mobjGrid.DoFormUpd(Request.QueryString.Item("Action"), "valMantClaim.aspx", Request.QueryString.Item("sCodispl"), Request.QueryString.Item("nMainAction"),  , CShort(Request.QueryString.Item("Index"))))
	End If
End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues

mobjValues.sCodisplPage = "MSI015"
%>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>


	<%=mobjValues.StyleSheet()%>
<SCRIPT>
//% insStateZone: se controla el estado de los campos de la página
//-------------------------------------------------------------------------------------------------------------------
function insStateZone(){
//-------------------------------------------------------------------------------------------------------------------
	return true;
}
//-Variable para el control de Versiones
	document.VssVersion="$$Revision: 3 $|$$Date: 15/10/03 15:53 $|$$Author: Nvaplat61 $"
</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="MSI015" ACTION="ValMantClaim.aspx?sMode=1">
<%
Response.Write(mobjValues.ShowWindowsName("MSI015"))
Call insDefineHeader()

If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write(mobjMenu.setZone(2, "MSI015", "MSI015.aspx"))
	Response.Write("<SCRIPT>var nMainAction =" & Request.QueryString.Item("nMainAction") & "</SCRIPT>")
	Call insPreMSI015()
Else
	Call insPreMSI015Upd()
End If

mobjGrid = Nothing
mobjValues = Nothing
mobjMenu = Nothing
%>
</FORM>
</BODY>
</HTML>





