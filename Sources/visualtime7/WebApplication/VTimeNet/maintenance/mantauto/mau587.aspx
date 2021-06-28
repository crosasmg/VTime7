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


'%insDefineHeader: Se definen las columnas del grid
'------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	
	'+Se definen todas las columnas del Grid
	With mobjGrid.Columns
		
		'+ Módulo
		Call .AddNumericColumn(0, GetLocalResourceObject("nModulecColumnCaption"), "nModulec", 5, CStr(0),  ,  ,  ,  ,  ,  ,  , True)
		mobjGrid.Columns("nModulec").PopUpVisible = False
		.AddPossiblesColumn(0, GetLocalResourceObject("cbeModulecColumnCaption"), "cbeModulec", "TabTab_Modul", eFunctions.Values.eValuesType.clngWindowType, CStr(0), True,  ,  ,  , "insChangeField(this);",  , 5, GetLocalResourceObject("cbeModulecColumnToolTip"))
		With mobjGrid.Columns("cbeModulec").Parameters
			.Add("nBranch", mobjValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Add("nProduct", mobjValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Add("dEffecdate", mobjValues.StringToType(Request.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		End With
		.AddNumericColumn(0, GetLocalResourceObject("tcnInimonthColumnCaption"), "tcnInimonth", 5, "",  , GetLocalResourceObject("tcnInimonthColumnToolTip"))
		.AddNumericColumn(0, GetLocalResourceObject("tcnEndmonthColumnCaption"), "tcnEndmonth", 5, "",  , GetLocalResourceObject("tcnEndmonthColumnToolTip"))
		mobjGrid.Columns.AddNumericColumn(0, GetLocalResourceObject("tcnAmount_claimColumnCaption"), "tcnAmount_claim", 18, "",  , GetLocalResourceObject("tcnAmount_claimColumnToolTip"), True, 6)
		.AddHiddenColumn("hddnId", CStr(0))
		.AddHiddenColumn("hddnBranch", Request.QueryString.Item("nBranch"))
		.AddHiddenColumn("hddnProduct", Request.QueryString.Item("nProduct"))
		.AddHiddenColumn("hdddEffecdate", Request.QueryString.Item("dEffecdate"))
		.AddHiddenColumn("hddnCurrency", Request.QueryString.Item("nCurrency"))
		.AddHiddenColumn("sParam", vbNullString)
	End With
	
	With mobjGrid
		.Codispl = "MAU587"
		.Codisp = "MAU587"
		.sCodisplPage = "MAU587"
		.Top = 100
		.Height = 250
		.Width = 390
		.ActionQuery = mobjValues.ActionQuery
		.nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
		.bOnlyForQuery = Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionQuery)
		.Columns("Sel").GridVisible = Not .ActionQuery
		.Columns("cbeModulec").EditRecord = True
		.Columns("cbeModulec").Disabled = Request.QueryString.Item("Action") = "Update"
		.sEditRecordParam = "nBranch=" & Request.QueryString.Item("nBranch") & "&nProduct=" & Request.QueryString.Item("nProduct") & "&dEffecdate=" & Request.QueryString.Item("dEffecdate") & "&nCurrency=" & Request.QueryString.Item("nCurrency")
		.sDelRecordParam = "' + marrArray[lintIndex].sParam + '&dEffecdate_Del=' + marrArray[lintIndex].hdddEffecdate + '"
		
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
End Sub

'%insPreMAU587. Se crea la ventana madre (Principal)
'------------------------------------------------------------------------------
Private Sub insPreMAU587()
	'------------------------------------------------------------------------------
	Dim lcolAu_bon_mods As eBranches.Au_bon_mods
	Dim lclsAu_bon_mod As Object
	
	With Request
		lcolAu_bon_mods = New eBranches.Au_bon_mods
		With mobjGrid
			If lcolAu_bon_mods.Find(mobjValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.QueryString.Item("nCurrency"), eFunctions.Values.eTypeData.etdDouble)) Then
				
				For	Each lclsAu_bon_mod In lcolAu_bon_mods
					.Columns("hddnId").DefValue = lclsAu_bon_mod.nId
					.Columns("nModulec").DefValue = lclsAu_bon_mod.nModulec
					.Columns("cbeModulec").DefValue = lclsAu_bon_mod.nModulec
					.Columns("tcnInimonth").DefValue = lclsAu_bon_mod.nInimonth
					.Columns("tcnEndmonth").DefValue = lclsAu_bon_mod.nEndmonth
					.Columns("tcnAmount_claim").DefValue = lclsAu_bon_mod.nAmount_claim
					
					'+ Se "Construye" un QueryString en la columna oculta sParam. Estos valores serán pasados a la 
					'+ función insPostMAU587Upd cuando se eliminen los registros seleccionados - NVAPLAT9 - 11/03/2002
					
					.Columns("sParam").DefValue = "nBranch=" & lclsAu_bon_mod.nBranch & "&nProduct=" & lclsAu_bon_mod.nProduct & "&dEffecdate=" & mobjValues.TypeToString(lclsAu_bon_mod.dEffecdate, eFunctions.Values.eTypeData.etdDate) & "&nCurrency=" & lclsAu_bon_mod.nCurrency & "&nModulec=" & lclsAu_bon_mod.nModulec & "&nId=" & lclsAu_bon_mod.nId & "&nUserCode=" & Session("nUsercode")
					Response.Write(mobjGrid.DoRow())
				Next lclsAu_bon_mod
			End If
		End With
		
	End With
	Response.Write(mobjGrid.CloseTable())
	
	lclsAu_bon_mod = Nothing
	lcolAu_bon_mods = Nothing
	
End Sub

'% insPreMAU587Upd. Se define esta funcion para contruir el contenido de la 
'%                  ventana de actualización de la Tabla para aplicación del
'%                  descuento por no siniestralidad
'------------------------------------------------------------------------------
Private Sub insPreMAU587Upd()
	'------------------------------------------------------------------------------
	Dim lclsAu_bon_mod As eBranches.Au_bon_mod
	
	With Request
		If .QueryString.Item("Action") = "Del" Then
			Response.Write(mobjValues.ConfirmDelete())
			lclsAu_bon_mod = New eBranches.Au_bon_mod
			
			Call lclsAu_bon_mod.insPostMAU587Upd("Del", mobjValues.StringToType(.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("dEffecdate_Del"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.QueryString.Item("nCurrency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nModulec"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nId"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(0), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(0), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(0), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
		End If
		Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "ValMantAuto.aspx", .QueryString.Item("sCodispl"), .QueryString.Item("nMainAction"), mobjGrid.ActionQuery, CShort(.QueryString.Item("Index"))))
	End With
	lclsAu_bon_mod = Nothing
End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues
mobjValues.ActionQuery = CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401
mobjValues.sCodisplPage = "MAU587"
%>

<HTML>
  <HEAD>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>




<SCRIPT	LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<%
With Response
	.Write(mobjValues.StyleSheet())
	If Request.QueryString.Item("Type") <> "PopUp" Then
		mobjMenu = New eFunctions.Menues
		.Write(mobjMenu.setZone(2, "MAU587", "MAU587.aspx"))
		Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
		mobjMenu = Nothing
	End If
End With
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST"	ID="FORM" NAME="frmMAU587" ACTION="valMantAuto.aspx?sZone=2">
<SCRIPT LANGUAGE=JavaScript>
//% insChangeField: Se recargan los valores cuando cambia el campo
//-------------------------------------------------------------------------------------------
function insChangeField(Field){
//-------------------------------------------------------------------------------------------    
	with (self.document.forms[0]){
		switch(Field.name){
            case "cbex":
                cbeCover.Parameters.Param5.sValue=cbeModulec.value;
                break;
		}
	}
}
</SCRIPT>
<%
Response.Write(mobjValues.ShowWindowsName("MAU587"))
Call insDefineHeader()
If Request.QueryString.Item("Type") <> "PopUp" Then
	Call insPreMAU587()
Else
	Call insPreMAU587Upd()
End If

mobjValues = Nothing
mobjGrid = Nothing
%>	  
</FORM>
</BODY>
</HTML>





