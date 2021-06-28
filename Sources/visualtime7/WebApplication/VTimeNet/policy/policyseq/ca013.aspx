<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="ePolicy" %>
<%@ Import namespace="eGeneral" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo del grid de la página
Dim mobjGrid As eFunctions.Grid

'- Objeto para el manejo del menú
Dim mobjMenu As eFunctions.Menues
Dim mlngGroup As Object
Dim mlngCurrency As Object


   
'% insDefineHeader: se definen las propiedades del grid
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	'+ Se definen las columnas del grid    
	With mobjGrid.Columns
		If Request.QueryString.Item("Type") <> "PopUp" Then
			.AddNumericColumn(0, GetLocalResourceObject("valModulecColumnCaption"), "valModulec", 9, vbNullString,  , GetLocalResourceObject("valModulecColumnToolTip"),  ,  ,  ,  ,  , True)
			.AddTextColumn(0, GetLocalResourceObject("tctModuleColumnCaption"), "tctModule", 30, vbNullString,  , GetLocalResourceObject("tctModuleColumnToolTip"))
		Else
			If Session("nTransaction") = 1 Or Session("nTransaction") = 3 Or Session("nTransaction") = 4 Or Session("nTransaction") = 6 Or Session("nTransaction") = 18 Or Session("nTransaction") = 23 Then
				.AddPossiblesColumn(0, GetLocalResourceObject("valModulecColumnCaption"), "valModulec", "TabTabModul_Exc", eFunctions.Values.eValuesType.clngWindowType,  , True,  ,  ,  , "insHabilitate(this);", Request.QueryString.Item("Action") = "Update",  , GetLocalResourceObject("valModulecColumnToolTip"))
			Else
				.AddPossiblesColumn(0, GetLocalResourceObject("valModulecColumnCaption"), "valModulec", "TabTabModul", eFunctions.Values.eValuesType.clngWindowType,  , True,  ,  ,  , "insHabilitate(this);", Request.QueryString.Item("Action") = "Update",  , GetLocalResourceObject("valModulecColumnToolTip"))
			End If
			mobjGrid.Columns("valModulec").Parameters.Add("sCertype", Session("sCertype"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			mobjGrid.Columns("valModulec").Parameters.Add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			mobjGrid.Columns("valModulec").Parameters.Add("nProduct", Session("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			mobjGrid.Columns("valModulec").Parameters.Add("nPolicy", Session("nPolicy"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			mobjGrid.Columns("valModulec").Parameters.Add("nCertif", Session("nCertif"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			mobjGrid.Columns("valModulec").Parameters.Add("dEffecdate", Session("dEffecdate"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			mobjGrid.Columns("valModulec").Parameters.Add("nGroup", mlngGroup, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		End If
		
		.AddCheckColumn(0, GetLocalResourceObject("chkChangeColumnCaption"), "chkChange", vbNullString,  ,  ,  , Request.QueryString.Item("Type") <> "PopUp", GetLocalResourceObject("chkChangeColumnToolTip"))
		.AddNumericColumn(0, GetLocalResourceObject("tcnPremiratColumnCaption"), "tcnPremirat", 9, CStr(eRemoteDB.Constants.intNull),  , GetLocalResourceObject("tcnPremiratColumnToolTip"),  , 6)
		If Request.QueryString.Item("Type") = "PopUp" Then
			.AddCheckColumn(0, GetLocalResourceObject("chkInheritColumnCaption"), "chkInherit", vbNullString,  ,  ,  , Request.QueryString.Item("Type") <> "PopUp", GetLocalResourceObject("chkInheritColumnToolTip"))
		End If
		.AddHiddenColumn("hddstyp_rat", vbNullString)
		.AddHiddenColumn("hddnPremirat", vbNullString)
		.AddHiddenColumn("hddnModulec", vbNullString)
		.AddHiddenColumn("hddsChecked", "1")
		.AddHiddenColumn("hddsChange", vbNullString)
	End With
	
	'+ Se definen las propiedades generales del grid
	With mobjGrid
		.Codispl = Request.QueryString.Item("sCodispl")
        .Columns("valModulec").EditRecord = True
		.Height = 250
		.Width = 400
        .nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") = "", 0, Request.QueryString.Item("nMainAction")))
            .Columns("Sel").GridVisible = Not .ActionQuery
            .Columns("Sel").OnClick = "valDelModule(this);"
		.sEditRecordParam = "nCurrency=' + (typeof(self.document.forms[0].cbeCurrency)!='undefined'?self.document.forms[0].cbeCurrency.value:'') + '" & "&nGroup=' + self.document.forms[0].hddnGroup.value + '" & "&sTyp_module=' + self.document.forms[0].hddsTyp_module.value + '"
		
		.sDelRecordParam = .sEditRecordParam & "&nModulec=' + marrArray[lintIndex].valModulec + '"
		
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
End Sub

'% insPreCA013: se realiza el manejo del grid
'--------------------------------------------------------------------------------------------
Private Sub insPreCA013()
	'--------------------------------------------------------------------------------------------
	Dim lclsModules As ePolicy.Modules
	Dim lintIndex As Integer
	Dim lblnFound As Boolean
	
	lclsModules = New ePolicy.Modules
	lblnFound = lclsModules.insPreCA013(Request.QueryString.Item("sCodispl"), Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), Session("dEffecdate"), mlngGroup, mobjValues.StringToDate(Session("dNulldate")), Session("nUsercode"), mobjValues.StringToType(Request.QueryString.Item("Reload"), eFunctions.Values.eTypeData.etdDouble), mlngCurrency, Session("nTransaction"))
	
	mlngGroup = lclsModules.nGroup_insu
	If lclsModules.nCurrency = eRemoteDB.Constants.intNull Then
		mlngCurrency = 4
	Else
		mlngCurrency = lclsModules.nCurrency
	End If
	Response.Write(mobjValues.HiddenControl("hddnGroup", mlngGroup))
	Response.Write(mobjValues.HiddenControl("hddsTyp_module", lclsModules.sTyp_module))
	
Response.Write("" & vbCrLf)
Response.Write("<TABLE WIDTH=""100%"">")

	
	Session("sTyp_module") = lclsModules.sTyp_module
	If lclsModules.sTyp_module = "3" Then
		If CStr(Session("sPolitype")) = "1" Then
			
Response.Write(" " & vbCrLf)
Response.Write("    <TR> " & vbCrLf)
Response.Write("        <TD WIDTH=""25%""><LABEL ID=""13043"">" & GetLocalResourceObject("valGroupCaption") & "</LABEL></TD> " & vbCrLf)
Response.Write("        <TD> " & vbCrLf)
Response.Write("        ")

                With mobjValues.Parameters
                    .Add("sCertype", Session("sCertype"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Add("nProduct", Session("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Add("nPolicy", Session("nPolicy"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                End With
                mobjValues.BlankPosition = False
                Response.Write(mobjValues.PossiblesValues("valGroup", "tabGroups", eFunctions.Values.eValuesType.clngComboType, mlngGroup, True, , , , , "ReloadPage()", CStr(Session("nCertif")) > "0", , GetLocalResourceObject("valGroupToolTip")))
			
			
Response.Write("</TD>")

			
			' Si es pro grupo y colectiva 
		Else
			
Response.Write("  <TR> " & vbCrLf)
Response.Write("                <TD WIDTH=""25%""><LABEL ID=""13043"">" & GetLocalResourceObject("valGroupCaption") & "</LABEL></TD> " & vbCrLf)
Response.Write("                <TD> " & vbCrLf)
Response.Write("                ")

                With mobjValues.Parameters
                    .Add("sCertype", Session("sCertype"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Add("nProduct", Session("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Add("nPolicy", Session("nPolicy"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Add("nCertif", Session("nCertif"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Add("dEffecdate", Session("dEffecdate"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				
                End With
			mobjValues.BlankPosition = False
			
                Response.Write(mobjValues.PossiblesValues("valGroup", "tabgroups_coll", eFunctions.Values.eValuesType.clngComboType, mlngGroup, True, , , , , "ReloadPage()", CStr(Session("nCertif")) > "0", , GetLocalResourceObject("valGroupToolTip")))
			
			'Response.Write mobjvalues.PossiblesValues("valGroup","tabGroups", eFunctions.Values.eValuesType.clngComboType, mlngGroup, True,,,,,"ReloadPage()",Session("nCertif") > "0",, GetLocalResourceObject("valGroupToolTip"))
			
			
Response.Write("</TD>")

			
		End If
	End If
	
	'+ Si las especificaciones son por grupo
	If CStr(Session("sPolitype")) = "1" Or Session("nCertif") > 0 Then
		
Response.Write("" & vbCrLf)
Response.Write("        <TD WIDTH=""10%""><LABEL ID=13038>" & GetLocalResourceObject("cbeCurrencyCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("        <TD>" & vbCrLf)
Response.Write("        ")

		
		mobjValues.TypeList = 1
		mobjValues.List = lclsModules.mclsCurren_pol.Charge_Combo
		mobjValues.BlankPosition = False
		Response.Write(mobjValues.PossiblesValues("cbeCurrency", "table11", eFunctions.Values.eValuesType.clngComboType, mlngCurrency,  ,  ,  ,  ,  , "ReloadPage()", lclsModules.nCountCurrency <= 1))
		
Response.Write("</TD>")

	Else
		Response.Write(mobjValues.HiddenControl("cbeCurrency", mlngCurrency))
	End If
Response.Write("" & vbCrLf)
Response.Write("</TABLE>")

	
    mobjValues.ActionQuery = Session("bQuery")
        
	'+ Si existe información pata procesar
	Dim lclsGeneral As eGeneral.GeneralFunction
	If lblnFound Then
		For lintIndex = 0 To lclsModules.CountModules
			If lclsModules.ModuleItem(lintIndex) Then
				With mobjGrid
					.Columns("valModulec").DefValue = CStr(lclsModules.nModulec)
					.Columns("hddnModulec").DefValue = CStr(lclsModules.nModulec)
					.Columns("tctModule").DefValue = lclsModules.sDescript
					.Columns("chkChange").Checked = CShort(lclsModules.sChangei)
					.Columns("hddsChange").DefValue = lclsModules.sChangei
					.Columns("tcnPremirat").DefValue = CStr(lclsModules.nPremirat)
					.Columns("hddnPremirat").DefValue = CStr(lclsModules.nPremirat)
					.Columns("hddstyp_rat").DefValue = lclsModules.styp_rat
					If lclsModules.styp_rat = "1" Then
						.Columns("tcnPremirat").disabled = False
					Else
						.Columns("tcnPremirat").disabled = True
					End If
				End With
				Response.Write(mobjGrid.DoRow())
			End If
		Next 
	Else
		If lclsModules.nError > 0 Then
			lclsGeneral = New eGeneral.GeneralFunction
			Response.Write("<SCRIPT>")
			Response.Write("alert(""Err. " & lclsModules.nError & ": " & lclsGeneral.insLoadMessage(lclsModules.nError) & """);")
			Response.Write("</" & "Script>")
			lclsGeneral = Nothing
			mobjGrid.AddButton = False
		End If
	End If
	Response.Write(mobjGrid.closeTable())
	'+ Se liberan de memoria las instancias creadas de los objetos utilizados en esta ventana - ACM - 15/12/2000    
	lclsModules = Nothing
End Sub

'% insPreCA013Upd: Se realiza el manejo de la ventana PopUp asociada al grid
'--------------------------------------------------------------------------------------------
Private Sub insPreCA013Upd()
	'--------------------------------------------------------------------------------------------
	Dim lclsModules As ePolicy.Modules
	
	Dim lclsRefresh As ePolicy.ValPolicySeq
	With Request
		If .QueryString.Item("Action") = "Del" Then
			Response.Write(mobjValues.ConfirmDelete())
			lclsModules = New ePolicy.Modules
			If lclsModules.InsPostCA013Upd(.QueryString.Item("sCodispl"), Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), mobjValues.StringToType(.QueryString.Item("nModulec"), eFunctions.Values.eTypeData.etdDouble), Session("dEffecdate"), vbNullString, vbNullString, Session("nTransaction"), mlngCurrency, Session("nUsercode"), Session("sPoliType"), Session("sBrancht"), Session("SessionId"), .QueryString.Item("Action"), .QueryString.Item("sTyp_module"), mobjValues.StringToType(.QueryString.Item("nGroup"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Constants.intNull, vbNullString, "2") Then
				
				'+Se se actualizó la ventana de coberturas se refresca la secuencia
				If lclsModules.bUpdCover Then
					lclsRefresh = New ePolicy.ValPolicySeq
					Response.Write(lclsRefresh.RefreshSequence(.QueryString.Item("sCodispl"), Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), Session("dEffecdate"), Session("sBrancht"), Session("sPolitype"), "No"))
					lclsRefresh = Nothing
				End If
			End If
			lclsModules = Nothing
		End If
		Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "valPolicySeq.aspx", .QueryString.Item("sCodispl"), .QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(.QueryString.Item("Index"))))
	End With
	Response.Write("<SCRIPT>insHabilitate2(); </" & "Script>")
End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues
mlngGroup = mobjValues.StringToType(Request.QueryString.Item("nGroup"), eFunctions.Values.eTypeData.etdDouble)
mlngCurrency = mobjValues.StringToType(Request.QueryString.Item("nCurrency"), eFunctions.Values.eTypeData.etdDouble)
%>
<HTML>
<HEAD>
<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE=JavaScript>
//% ReloadPage: se recarga la página cuando se cambia la moneda
//-------------------------------------------------------------------------------------------
function ReloadPage(){
//-------------------------------------------------------------------------------------------
    var nGroup='';
    
    with(self.document.forms[0]){        
        nGroup = (typeof(valGroup)=='undefined')?"0":valGroup.value
        self.document.location.href = "CA013.aspx?sCodispl=<%=Request.QueryString.Item("sCodispl")%>&sCodisp=<%=Request.QueryString.Item("sCodisp")%>&nMainAction=" + nMainAction +
                                      "&sOnSeq=1&nCurrency=" + (typeof(cbeCurrency) != 'undefined'?cbeCurrency.value:'') +
                                      "&nGroup=" + nGroup
    }                                                                 
}

//----------------------------------------------------------------------------------------
function insHabilitate(Field){
//----------------------------------------------------------------------------------------
    var Action = '<%=Request.QueryString.Item("Action")%>'
    insDefValues("Modulec", "nModulec=" + Field.value + "&nGroup=" + <%=mlngGroup%> + "&Action=" + Action, '/VTimeNet/Policy/PolicySeq');
}
//----------------------------------------------------------------------------------------
function insHabilitate2(){
//----------------------------------------------------------------------------------------
    with(self.document.forms[0]){
        if (typeof(hddstyp_rat)!='undefined' && typeof(tcnPremirat)!='undefined') {
            if (hddstyp_rat.value == '1')
                tcnPremirat.disabled=false;
            else
                tcnPremirat.disabled=true;
        }
    }
}

//%valModule: Se verifica si se puede borrar o no el Módulo
//--------------------------------------------------------------------------------------------------
function valDelModule(Field){
//--------------------------------------------------------------------------------------------------
    var nGroup='';
    with(self.document.forms[0]){
	    if(Field.checked){
            nGroup = (typeof(valGroup)=='undefined')?"0":valGroup.value
		    insDefValues('DeleteCA013', 'nGroup=' + nGroup + '&nModulec=' + marrArray[Field.value].valModulec + '&nIndex=' + Field.value)
	    }
    }
}

</SCRIPT>
<%

Response.Write(mobjValues.StyleSheet())
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write(mobjMenu.setZone(2, Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sCodispl") & ".aspx"))
	mobjMenu = Nothing
	Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
End If
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" NAME="frmCA013" ACTION="ValPolicySeq.aspx?x=1">
<%  Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl")))
    
    Call insDefineHeader()
    mobjGrid.ActionQuery = Session("bQuery")
    
    If Request.QueryString.Item("Type") = "PopUp" Then
        Call insPreCA013Upd()
    Else
        Call insPreCA013()
    End If
    
    mobjGrid = Nothing
    mobjValues = Nothing
%>
</FORM> 
</BODY>
</HTML>




                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                     