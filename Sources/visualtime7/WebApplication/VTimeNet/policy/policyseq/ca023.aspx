<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="ePolicy" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 31/3/03 19.42.04
Dim mobjNetFrameWork As eNetFrameWork.Layout

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo del grid de la página
Dim mobjGrid As eFunctions.Grid

'- Objeto para el manejo del menú
Dim mobjMenu As eFunctions.Menues

'- Objeto para el manejo particular de los datos de la página
Dim mcolBeneficiar As ePolicy.Beneficiars

'- Objeto para tomar información de los módulos
Dim mclsModules As ePolicy.Modules
Dim mblnExist As Boolean


'% insDefineHeader: se definen las propiedades del grid
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.42.04
	mobjGrid.sSessionID = Session.SessionID
	mobjGrid.nUsercode = Session("nUsercode")
	'~End Body Block VisualTimer Utility
	
	mobjGrid.sCodisplPage = Request.QueryString.Item("sCodispl")
	Call mobjGrid.SetWindowParameters(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), Request.QueryString.Item("nWindowTy"))
	
	'+ Se definen las columnas del grid    
	With mobjGrid.Columns
		Call .AddClientColumn(0, GetLocalResourceObject("dtcClientColumnCaption"), "dtcClient", vbNullString,  , GetLocalResourceObject("dtcClientColumnToolTip"),  , Request.QueryString.Item("Action") = "Update", "tctCliename",  ,  ,  ,  ,  , True)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnParticipColumnCaption"), "tcnParticip", 5, vbNullString,  , GetLocalResourceObject("tcnParticipColumnToolTip"), True, 2)
		Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeRelationColumnCaption"), "cbeRelation", "Table55", eFunctions.Values.eValuesType.clngComboType, CStr(0),  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeRelationColumnToolTip"))
		
		If mblnExist Then
			Call mobjGrid.Columns.AddPossiblesColumn(0, GetLocalResourceObject("valModulecColumnCaption"), "valModulec", "tabTabModul_CO_PG", eFunctions.Values.eValuesType.clngWindowType, vbNullString, True,  ,  ,  , "insChangeValues(this)", Request.QueryString.Item("Action") = "Update",  , GetLocalResourceObject("valModulecColumnToolTip"))
		End If
		
		Call .AddPossiblesColumn(0, GetLocalResourceObject("valCoverColumnCaption"), "valCover", "tabCoverPolicy", eFunctions.Values.eValuesType.clngWindowType, vbNullString, True,  ,  ,  ,  , Request.QueryString.Item("Action") = "Update",  , GetLocalResourceObject("valCoverColumnToolTip"))
		Call .AddDateColumn(0, GetLocalResourceObject("tcddatedeclaColumnCaption"), "tcddatedecla", "",  , GetLocalResourceObject("tcddatedeclaColumnToolTip"),  ,  ,  , False)
		Call .AddCheckColumn(0, GetLocalResourceObject("chkIrrevocColumnCaption"), "chkIrrevoc", "",  ,  ,  , Request.QueryString.Item("Type") <> "PopUp")
            Call .AddCheckColumn(0, GetLocalResourceObject("chkContiColumnCaption"), "chkConti", "", , , , Request.QueryString.Item("Type") <> "PopUp")
            Call .AddCheckColumn(0, GetLocalResourceObject("chkDesignColumnCaption"), "chkDesign", "", , , , Request.QueryString.Item("Type") <> "PopUp")
		
	End With
	
	If mblnExist Then
		With mobjGrid.Columns("valModulec").Parameters
			.Add("sCertype", Session("sCertype"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Add("nBranch", mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Add("nProduct", mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Add("nPolicy", mobjValues.StringToType(Session("npolicy"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Add("nCertif", mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Add("dEffecdate", mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Add("nGroup", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		End With
	End If
	
	'+ Se definen las propiedades generales del grid
	With mobjGrid
		.Codispl = "CA023"
		.ActionQuery = mobjValues.ActionQuery
		.Columns("dtcClient").EditRecord = True
		.Top = 100
		
		If mblnExist Then
			.Height = 380
		Else
			.Height = 350
		End If
		
		.Width = 470
		.UpdContent = True
		
		If mblnExist Then
			.sDelRecordParam = "sClient=' + marrArray[lintIndex].dtcClient + '&nModulec=' + marrArray[lintIndex].valModulec + '&nCover=' + marrArray[lintIndex].valCover + '"
		Else
			.sDelRecordParam = "sClient=' + marrArray[lintIndex].dtcClient + '&nModulec= 0 ' + '&nCover=' + marrArray[lintIndex].valCover + '"
		End If
		.nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
		.Columns("Sel").GridVisible = Not .ActionQuery
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
		With .Columns("valCover").Parameters
			.Add("sCertype", Session("sCertype"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Add("nBranch", mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Add("nProduct", mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Add("nPolicy", mobjValues.StringToType(Session("npolicy"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Add("nCertif", mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Add("dEffecdate", mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Add("nGroup", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Add("nModulec", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		End With
	End With
End Sub

'% insPreCA023: se realiza el manejo del grid
'--------------------------------------------------------------------------------------------
Private Sub insPreCA023()
	'--------------------------------------------------------------------------------------------
	Dim lclsBeneficiar As Object
	mcolBeneficiar = New ePolicy.Beneficiars
	
	If mcolBeneficiar.Find(Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("npolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate)) Then
		
		For	Each lclsBeneficiar In mcolBeneficiar
			With mobjGrid
				.Columns("dtcClient").DefValue = lclsBeneficiar.sClient
				.Columns("tcnParticip").DefValue = lclsBeneficiar.nParticip
				.Columns("cbeRelation").DefValue = lclsBeneficiar.nRelation
				
				If mblnExist Then
					.Columns("valModulec").DefValue = lclsBeneficiar.nModulec
				End If
				
				.Columns("valCover").Parameters.Add("nModulec", lclsBeneficiar.nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Columns("valCover").DefValue = lclsBeneficiar.nCover
				.Columns("tcddatedecla").DefValue = lclsBeneficiar.ddatedecla
				.Columns("chkIrrevoc").Checked = lclsBeneficiar.sirrevoc
				.Columns("chkIrrevoc").DefValue = lclsBeneficiar.sirrevoc
				.Columns("chkConti").DefValue = lclsBeneficiar.sConting
				.Columns("chkConti").Checked = lclsBeneficiar.sConting
                    .Columns("chkDesign").DefValue = lclsBeneficiar.sDesign
                    .Columns("chkDesign").Checked = lclsBeneficiar.sDesign
				
                
				Select Case Session("nTransaction")
					Case "12", "13", "14", "15", "24", "25", "26", "27"
						If lclsBeneficiar.sirrevoc = "1" Then
                                '    .Columns("Sel").Disabled = True
                                .Columns("Sel").Disabled = False
						Else
							.Columns("Sel").Disabled = False
						End If
				End Select
				Response.Write(.DoRow)
			End With
		Next lclsBeneficiar
	End If
	Response.Write(mobjValues.HiddenControl("hddnCount", CStr(mcolBeneficiar.Count)))
	Response.Write(mobjGrid.closeTable())
	lclsBeneficiar = Nothing
End Sub

'% insPreCA023Upd: Se realiza el manejo de la ventana PopUp asociada al grid
'--------------------------------------------------------------------------------------------
Private Sub insPreCA023Upd()
	'--------------------------------------------------------------------------------------------
	Dim lclsBeneficiar As ePolicy.Beneficiar
	Dim lstrContent As String
	lstrContent = vbNullString
	
	With Request
		If .QueryString.Item("Action") = "Del" Then
			lclsBeneficiar = New ePolicy.Beneficiar
			Call lclsBeneficiar.insPostCA023(.QueryString.Item("Action"), Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), .QueryString.Item("sClient"), eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.QueryString.Item("nModulec"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nCover"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Constants.dtmNull, vbNullString, vbNullString)
			lstrContent = lclsBeneficiar.sContent
			Response.Write(mobjValues.ConfirmDelete)
		End If
		Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "valPolicySeq.aspx", "CA023", .QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(.QueryString.Item("Index")), lstrContent))
		Response.Write(mobjValues.UpdContent("CA025", "3"))
	End With
	lclsBeneficiar = Nothing
End Sub

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("CA023")
'~End Header Block VisualTimer Utility
Response.CacheControl = "private"

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.42.04
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = Request.QueryString.Item("sCodispl")
mclsModules = New ePolicy.Modules
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.42.04
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.ActionQuery = Session("bQuery")

%>
<HTML>
<HEAD>
<SCRIPT>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 3 $|$$Date: 15/10/03 16:49 $|$$Author: Nvaplat61 $"
</SCRIPT>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>


	<%
Response.Write(mobjValues.StyleSheet())
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write(mobjMenu.setZone(2, "CA023", Request.QueryString.Item("sWindowDescript"), CShort(Request.QueryString.Item("nWindowTy"))))
	mobjMenu = Nothing
	Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
End If
%>
<SCRIPT>
//% insChangeValues: se controla el cambio de valor de los controles
//-------------------------------------------------------------------------------------------
function insChangeValues(Field){
//-------------------------------------------------------------------------------------------
	with(self.document.forms[0]){
		valCover.Parameters.Param8.sValue = Field.value;
<%If Request.QueryString.Item("Action") = "Add" Then%>
		valCover.disabled=(Field.value=='')?true:false;
		btnvalCover.disabled=valCover.disabled;
		valCover.value='';
		UpdateDiv('valCoverDesc','');
<%End If%>
	}
}
</SCRIPT>
</HEAD>

<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" NAME="CA023" ACTION="valPolicySeq.aspx?sTime=1">

    <%Response.Write(mobjValues.ShowWindowsName("CA023", Request.QueryString.Item("sWindowDescript")))
mblnExist = mclsModules.InsValModulPolicy(Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("npolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), vbNullString)
Call insDefineHeader()
If Request.QueryString.Item("Type") = "PopUp" Then
	Call insPreCA023Upd()
Else
	Call insPreCA023()
End If
mobjValues = Nothing
mclsModules = Nothing
mobjGrid = Nothing
mcolBeneficiar = Nothing
%>
</FORM> 
</BODY>
</HTML>

<%'^Begin Footer Block VisualTimer Utility 1.1 31/3/03 19.42.04
Call mobjNetFrameWork.FinishPage("CA023")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




