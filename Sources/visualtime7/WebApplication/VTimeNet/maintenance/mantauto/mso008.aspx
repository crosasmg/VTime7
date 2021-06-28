<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eBranches" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Se define la variable mobjGrid para el manejo del Grid de la ventana
Dim mobjGrid As eFunctions.Grid

'- Objeto para el manejo de las zonas de la página
Dim mobjMenu As eFunctions.Menues

Dim mstrMarca As String


'% insDefineHeader: Se definen las columnas del grid
'------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	
	'+ Se definen todas las columnas del Grid
	With mobjGrid.Columns
		
		.AddPossiblesColumn(0, GetLocalResourceObject("cbeVehTypeColumnCaption"), "cbeVehType", "Table78109", eFunctions.Values.eValuesType.clngWindowType, CStr(0),  ,  ,  ,  ,  ,  ,  5, GetLocalResourceObject("cbeVehTypeColumnToolTip"))
		.AddNumericColumn(0, GetLocalResourceObject("tcnPremiumColumnCaption"), "tcnPremium", 18, "",  , GetLocalResourceObject("tcnPremiumColumnToolTip"),  , 6)
		
		.AddHiddenColumn("hdddEffecdate", Request.QueryString.Item("dEffecdate"))
		.AddHiddenColumn("sParam", vbNullString)
		
	End With
	
	With mobjGrid
		.Codispl = "MSO008"
		.Codisp = "MSO008"
		.sCodisplPage = "MSO008"
		.Top = 100
		.Height = 288
		.Width = 390
		.ActionQuery = mobjValues.ActionQuery
		.nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
		.bOnlyForQuery = Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionQuery)
		.Columns("Sel").GridVisible = Not .ActionQuery 
		.Columns("cbeVehType").EditRecord = True
		.Columns("cbeVehType").Disabled = Request.QueryString.Item("Action") = "Update"
		.sEditRecordParam = "dEffecdate=" & Request.QueryString.Item("dEffecdate")
		.sDelRecordParam = "' + marrArray[lintIndex].sParam + '"
		
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
End Sub

'% insPreMSO008. Se crea la ventana madre (Principal)
'------------------------------------------------------------------------------
Private Sub insPreMSO008()
	'------------------------------------------------------------------------------
	Dim lcolTar_prem_SOAPs As eBranches.Tar_prem_SOAPs
	Dim lclsTar_prem_SOAP As Object
	
	With Request
		lcolTar_prem_SOAPs = New eBranches.Tar_prem_SOAPs
		With mobjGrid
			If lcolTar_prem_SOAPs.Find(mobjValues.StringToType(Request.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate)) Then
				
				For	Each lclsTar_prem_SOAP In lcolTar_prem_SOAPs
					.Columns("cbeVehType").DefValue = lclsTar_prem_SOAP.nVehType
					.Columns("tcnPremium").DefValue = lclsTar_prem_SOAP.nPremium
					
					'+ Se "Construye" un QueryString en la columna oculta sParam. Estos valores serán pasados a la 
					'+ función insPostMSO008Upd cuando se eliminen los registros seleccionados 
					.Columns("sParam").DefValue = "dEffecdate=" & Request.QueryString.Item("dEffecdate") & "&nVehType=" & lclsTar_prem_SOAP.nVehType & "&nUserCode=" & Session("nUsercode")
					
					Response.Write(mobjGrid.DoRow())
				Next lclsTar_prem_SOAP
			End If
		End With
		
	End With
	Response.Write(mobjGrid.CloseTable())
	
	lclsTar_prem_SOAP = Nothing
	lcolTar_prem_SOAPs = Nothing
	
End Sub

'% insPreMSO008Upd. Se define esta funcion para contruir el contenido de la 
'% ventana UPD de tarifa de automóvil
'------------------------------------------------------------------------------
Private Sub insPreMSO008Upd()
	'------------------------------------------------------------------------------
	Dim lclsTar_prem_SOAP As eBranches.Tar_prem_SOAP
	
	With Request
		If .QueryString.Item("Action") = "Del" Then
			Response.Write(mobjValues.ConfirmDelete())
			lclsTar_prem_SOAP = New eBranches.Tar_prem_SOAP
			Call lclsTar_prem_SOAP.insPostMSO008Upd("Del", mobjValues.StringToType(.QueryString.Item("deffecDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.QueryString.Item("nVehType"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(0), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
		End If
		Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "ValMantAuto.aspx", .QueryString.Item("sCodispl"), .QueryString.Item("nMainAction"), mobjGrid.ActionQuery, CShort(.QueryString.Item("Index"))))
	End With
	lclsTar_prem_SOAP = Nothing
End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues
mobjValues.ActionQuery = CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401
mobjValues.sCodisplPage = "MSO008"
%>

<HTML>
<HEAD>
<SCRIPT	LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<%
With Response
	.Write(mobjValues.StyleSheet())
	If Request.QueryString.Item("Type") <> "PopUp" Then
		mobjMenu = New eFunctions.Menues
		.Write(mobjMenu.setZone(2, "MSO008", "MSO008.aspx"))
		Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
		mobjMenu = Nothing
	End If
End With
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST"	ID="FORM" NAME="frmMSO008" ACTION="valMantAuto.aspx?sZone=2">
<SCRIPT LANGUAGE=JavaScript>
//- Variable para el control de versiones
document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 15:45 $|$$Author: Nvaplat61 $"

</SCRIPT>
<%
Response.Write(mobjValues.ShowWindowsName("MSO008"))
Call insDefineHeader()
If Request.QueryString.Item("Type") <> "PopUp" Then
	Call insPreMSO008()
Else
	Call insPreMSO008Upd()
End If

mobjValues = Nothing
mobjGrid = Nothing
%>	  
</FORM>
</BODY>
</HTML>
