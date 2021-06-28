<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eBranches" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo del grid de la página
Dim mobjGrid As eFunctions.Grid

'- Objeto para el manejo del menú
Dim mobjMenu As eFunctions.Menues

'- Objeto para el manejo particular de los datos de la página
Dim mcoltarcoverfh As eBranches.Tar_cover_fhs


'% insDefineHeader: se definen las propiedades del grid
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	
	mobjGrid = New eFunctions.Grid
	Dim sAccion As String
	sAccion = Request.QueryString.Item("Action")
	'+ Se definen las columnas del grid  
	With mobjGrid.Columns
            Call .AddNumericColumn(0, GetLocalResourceObject("tcnCap_initialColumnCaption"), "tcnCap_initial", 18, CStr(eRemoteDB.Constants.intNull), , GetLocalResourceObject("tcnCap_initialColumnToolTip"), True, 3)
            Call .AddNumericColumn(0, GetLocalResourceObject("tcnCap_endColumnCaption"), "tcnCap_end", 18, CStr(eRemoteDB.Constants.intNull), , GetLocalResourceObject("tcnCap_endColumnToolTip"), True, 3)
		Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeConstcatColumnCaption"), "cbeConstcat", "Table233", eFunctions.Values.eValuesType.clngComboType, CStr(eRemoteDB.Constants.intNull),  ,  ,  ,  ,  ,  , 5, GetLocalResourceObject("cbeConstcatColumnToolTip"))
		Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeProvinceColumnCaption"), "cbeProvince", "Tab_Province", eFunctions.Values.eValuesType.clngComboType, "0", False,  ,  ,  , "insChangeField(this);",  , 5, GetLocalResourceObject("cbeProvinceColumnToolTip"))
		If sAccion <> "Update" Then
			Call .AddPossiblesColumn(0, GetLocalResourceObject("valMunicipalityColumnCaption"), "valMunicipality", "Tab_municipality_a", eFunctions.Values.eValuesType.clngWindowType, "0", True,  ,  ,  ,  ,  , 5, GetLocalResourceObject("valMunicipalityColumnToolTip"))
		Else
			Call .AddPossiblesColumn(0, GetLocalResourceObject("valMunicipalityColumnCaption"), "valMunicipality", "tabmunicipality", eFunctions.Values.eValuesType.clngComboType, "0",  ,  ,  ,  ,  ,  , 5, GetLocalResourceObject("valMunicipalityColumnToolTip"))
		End If
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnRateColumnCaption"), "tcnRate", 5, CStr(eRemoteDB.Constants.intNull),  , GetLocalResourceObject("tcnRateColumnToolTip"),  , 2)
            Call .AddNumericColumn(0, GetLocalResourceObject("tcnPremiumColumnCaption"), "tcnPremium", 18, CStr(eRemoteDB.Constants.intNull), , GetLocalResourceObject("tcnPremiumColumnToolTip"), , 3)
		Call .AddHiddenColumn("nBranch", Request.QueryString.Item("nBranch"))
		Call .AddHiddenColumn("nProduct", Request.QueryString.Item("nProduct"))
		Call .AddHiddenColumn("nCover", Request.QueryString.Item("nCover"))
		Call .AddHiddenColumn("nModulec", Request.QueryString.Item("nModulec"))
		Call .AddHiddenColumn("nCurrency", Request.QueryString.Item("nCurrency"))
		Call .AddHiddenColumn("dEffecdate", Request.QueryString.Item("dEffecdate"))
	End With
	
	'+ Se definen las propiedades generales del grid
	
	With mobjGrid
		.Codispl = "MIN652"
		.sCodisplPage = "MIN652"
		.ActionQuery = mobjValues.ActionQuery
		.Columns("tcnCap_initial").EditRecord = True
		.Height = 360
		.Width = 370
		.WidthDelete = 440
		.nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
		If sAccion <> "Update" Then
			.Columns("valMunicipality").Parameters.Add("nLocal", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric)
			.Columns("valMunicipality").Parameters.Add("nProvince", "0", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric)
		Else
			.Columns("tcnCap_initial").Disabled = True
			.Columns("cbeConstcat").Disabled = True
			.Columns("cbeProvince").Disabled = True
			.Columns("valMunicipality").Disabled = True
		End If
		.Columns("Sel").GridVisible = Not .ActionQuery
		.sEditRecordParam = "nBranch=" & Request.QueryString.Item("nBranch") & "&nProduct=" & Request.QueryString.Item("nProduct") & "&nCover=" & Request.QueryString.Item("nCover") & "&nModulec=" & Request.QueryString.Item("nModulec") & "&nCurrency=" & Request.QueryString.Item("nCurrency") & "&dEffecdate=" & Request.QueryString.Item("dEffecdate")
		.sDelRecordParam = "nBranch=" & Request.QueryString.Item("nBranch") & "&nProduct=" & Request.QueryString.Item("nProduct") & "&nCover=" & Request.QueryString.Item("nCover") & "&nModulec=" & Request.QueryString.Item("nModulec") & "&nCurrency=" & Request.QueryString.Item("nCurrency") & "&dEffecdate=" & Request.QueryString.Item("dEffecdate") & "&nConstcat='+ marrArray[lintIndex].cbeConstcat + '" & "&nCap_initial='+ marrArray[lintIndex].tcnCap_initial + '" & "&nProvince='+ marrArray[lintIndex].cbeProvince + '" & "&nMunicipality='+ marrArray[lintIndex].valMunicipality + '"
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
End Sub

'% insPreMIN652: se realiza el manejo del grid
'--------------------------------------------------------------------------------------------
Private Sub insPreMIN652()
	'--------------------------------------------------------------------------------------------
	Dim lclstarcoverfh As Object
	
	mcoltarcoverfh = New eBranches.Tar_cover_fhs
	If mcoltarcoverfh.Find(mobjValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nModulec"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nCover"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nCurrency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate)) Then
		For	Each lclstarcoverfh In mcoltarcoverfh
			With mobjGrid
				.Columns("tcnCap_initial").DefValue = lclstarcoverfh.nCap_initial
				.Columns("tcnCap_end").DefValue = lclstarcoverfh.nCap_end
				.Columns("cbeConstcat").DefValue = lclstarcoverfh.nConstcat
				.Columns("cbeProvince").DefValue = lclstarcoverfh.nProvince
				.Columns("valMunicipality").Parameters.Add("nLocal", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric)
				.Columns("valMunicipality").Parameters.Add("nProvince", lclstarcoverfh.nProvince, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric)
				.Columns("valMunicipality").DefValue = lclstarcoverfh.nMunicipality
				.Columns("tcnRate").DefValue = lclstarcoverfh.nRate
				.Columns("tcnPremium").DefValue = lclstarcoverfh.nPremium
				Response.Write(.DoRow)
			End With
		Next lclstarcoverfh
	End If
	
	Response.Write(mobjGrid.closeTable())
End Sub

'% insPreMIN652Upd: Se realiza el manejo de la ventana PopUp asociada al grid
'--------------------------------------------------------------------------------------------
Private Sub insPreMIN652Upd()
	'--------------------------------------------------------------------------------------------
	'*++ Modificar nombre del objeto. Modificar "Class" por el nombre de la clase con la cual se trabaja
	Dim lobjClass As eBranches.Tar_cover_fh
	lobjClass = New eBranches.Tar_cover_fh
	With Request
		If .QueryString.Item("Action") = "Del" Then
			Response.Write(mobjValues.ConfirmDelete())
			If lobjClass.InsPostMIN652(.QueryString.Item("Action"), mobjValues.StringToType(.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nModulec"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nCover"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nCurrency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.QueryString.Item("nConstcat"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nProvince"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nMunicipality"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nCap_initial"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble)) Then
			End If
		End If
		Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "valMantFire.aspx", "MIN652", .QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(.QueryString.Item("Index"))))
	End With
End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues
mobjValues.ActionQuery = CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401
mobjValues.sCodisplPage = "MIN652"
%>
<HTML>
<HEAD>
<SCRIPT>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 15:59 $|$$Author: Nvaplat61 $"
</SCRIPT>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>


<SCRIPT LANGUAGE="JavaScript">

//% Se limpia el campo Comuna cuando cambia la región
//--------------------------------------------------------------------------------------------
function insChangeField(objField){
//--------------------------------------------------------------------------------------------
	with(document.forms[0]){
		if(objField.name=='cbeProvince')
		{
			valMunicipality.Parameters.Param2.sValue=objField.value;
			document.forms[0].valMunicipality.value = "";
			UpdateDiv("valMunicipalityDesc","");
		}
    }
}
</SCRIPT>    
<%
Response.Write(mobjValues.StyleSheet())
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write(mobjMenu.setZone(2, "MIN652", "MIN652.aspx"))
	
	mobjMenu = Nothing
	Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
End If
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" NAME="MIN652" ACTION="valMantFire.aspx?sMode=2">
<%
Response.Write(mobjValues.ShowWindowsName("MIN652"))
Call insDefineHeader()
If Request.QueryString.Item("Type") = "PopUp" Then
	Call insPreMIN652Upd()
Else
	Call insPreMIN652()
End If
%>
</FORM> 
</BODY>
</HTML>





