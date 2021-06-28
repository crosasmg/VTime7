<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="ePolicy" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 31/3/03 19.44.14
Dim mobjNetFrameWork As eNetFrameWork.Layout
'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
'- Objeto para el manejo del grid de la página
Dim mobjGrid As eFunctions.Grid
'- Objeto para el manejo del menú
Dim mobjMenu As eFunctions.Menues
'- Objeto para el manejo de los datos de la página
Dim mblnError As Boolean


'% insDefineHeader: se definen las propiedades del grid
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.44.14
	mobjGrid.sSessionID = Session.SessionID
	mobjGrid.nUsercode = Session("nUsercode")
	'~End Body Block VisualTimer Utility
	
	mobjGrid.sCodisplPage = Request.QueryString.Item("sCodispl")
	Call mobjGrid.SetWindowParameters(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), Request.QueryString.Item("nWindowTy"))
	
	'+ Se definen las columnas del grid
	With mobjGrid.Columns
		Call .AddNumericColumn(40790, GetLocalResourceObject("tcnCoverColumnCaption"), "tcnCover", 5, CStr(eRemoteDB.Constants.intNull), True, GetLocalResourceObject("tcnCoverColumnToolTip"), True, 0,  ,  ,  , True)
		Call .AddPossiblesColumn(40785, GetLocalResourceObject("cbeCoverColumnCaption"), "cbeCover", "TabGen_cover3", eFunctions.Values.eValuesType.clngComboType, CStr(0), True,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeCoverColumnToolTip"))
		Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeRoleColumnCaption"), "cbeRole", "Table12", eFunctions.Values.eValuesType.clngComboType, CStr(0),  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeRoleColumnToolTip"))
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnInsuredColumnCaption"), "tcnInsured", 10, CStr(eRemoteDB.Constants.intNull),  , GetLocalResourceObject("tcnInsuredColumnToolTip"), False,  ,  ,  ,  , True)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnExInsuredColumnCaption"), "tcnExInsured", 5, CStr(eRemoteDB.Constants.intNull),  , GetLocalResourceObject("tcnExInsuredColumnToolTip"), False,  ,  ,  ,  , True)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnCapitalColumnCaption"), "tcnCapital", 18, CStr(eRemoteDB.Constants.intNull),  , GetLocalResourceObject("tcnCapitalColumnToolTip"), True, 6,  ,  ,  , True)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnIVAColumnCaption"), "tcnIVA", 5, CStr(eRemoteDB.Constants.intNull),  , GetLocalResourceObject("tcnIVAColumnToolTip"), True, 2,  ,  ,  , True)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnRateColumnCaption"), "tcnRate", 9, CStr(eRemoteDB.Constants.intNull),  , GetLocalResourceObject("tcnRateColumnToolTip"), True, 6,  ,  , "ChangeValues('Rate',this)")
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnUtilMarColumnCaption"), "tcnUtilMar", 5, CStr(eRemoteDB.Constants.intNull),  , GetLocalResourceObject("tcnUtilMarColumnToolTip"), True, 2,  ,  ,  , True)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnPremiumColumnCaption"), "tcnPremium", 18, CStr(eRemoteDB.Constants.intNull),  , GetLocalResourceObject("tcnPremiumColumnToolTip"), True, 6,  ,  , "ChangeValues('Premium',this)")
		Call .AddHiddenColumn("hddOldRate", CStr(0))
		Call .AddHiddenColumn("hddnPremiumOrig", CStr(0))
		Call .AddHiddenColumn("hddUtilMarOrig", CStr(0))
		Call .AddHiddenColumn("hddOldPremium", CStr(0))
	End With
	'+ Se definen las propiedades generales del grid
	With mobjGrid
		.Codispl = "VI666"
		.ActionQuery = mobjValues.ActionQuery
		.Height = 400
		.Width = 380
		.Top = 100
		.nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
		.Columns("Sel").GridVisible = False
		.AddButton = False
		.DeleteButton = False
		.Columns("cbeCover").EditRecord = True
		.sEditRecordParam = "nGroup='+ self.document.forms[0].valGroup.value +'"
		With .Columns("cbeCover").Parameters
			.Add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Add("nProduct", Session("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Add("nModulec", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Add("dEffecdate", Session("dEffecdate"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Add("sCovergen", "2", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		End With
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
End Sub
'% insPreVI666: se realiza el manejo del grid
'--------------------------------------------------------------------------------------------
Private Sub insPreVI666()
	'--------------------------------------------------------------------------------------------
	Dim lclsCover_quota As ePolicy.Cover_quota
	Dim mclsCover_quota As ePolicy.Cover_quota
	
	mclsCover_quota = New ePolicy.Cover_quota
	
	Call mclsCover_quota.insPreVI666(Session("bQuery"), Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("dEffecdate"), Session("nTransaction"), mobjValues.StringToType(Request.QueryString.Item("nGroup"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.QueryString.Item("nModulec"), eFunctions.Values.eTypeData.etdDouble, True), Session("SessionId"), Session("nUsercode"), Request.QueryString.Item("sReloadPage"))
	
	If mclsCover_quota.nGroup_Initial <> eRemoteDB.Constants.intNull And IsNothing(Request.QueryString.Item("nGroup")) Then
		Call mclsCover_quota.insPreVI666(Session("bQuery"), Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("dEffecdate"), Session("nTransaction"), mobjValues.StringToType(CStr(mclsCover_quota.nGroup_Initial), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.QueryString.Item("nModulec"), eFunctions.Values.eTypeData.etdDouble, True), Session("SessionId"), Session("nUsercode"), Request.QueryString.Item("sReloadPage"))
	End If
	
	mblnError = mclsCover_quota.sErrors <> vbNullString
	mobjValues.ActionQuery = Session("bQuery") Or mblnError
	mobjGrid.ActionQuery = mobjValues.ActionQuery
	
Response.Write("" & vbCrLf)
Response.Write("	<TABLE WIDTH=100%>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("        	<TD><LABEL ID=13052>" & GetLocalResourceObject("valGroupCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")

	
	With mobjValues.Parameters
		.Add("sCertype", Session("sCertype"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Add("nProduct", Session("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Add("nPolicy", Session("nPolicy"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	End With
	Response.Write(mobjValues.PossiblesValues("valGroup", "tabGroups", eFunctions.Values.eValuesType.clngWindowType, Request.QueryString.Item("nGroup"), True,  ,  ,  ,  , "ChangeValues(""Group"",this)", Not mclsCover_quota.bExistGroups,  , GetLocalResourceObject("valGroupToolTip")))
	
Response.Write("" & vbCrLf)
Response.Write("            </TD>" & vbCrLf)
Response.Write("			<TD>&nbsp;</TD>" & vbCrLf)
Response.Write("			<TD><LABEL ID=0>" & GetLocalResourceObject("valModuleCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")

	
	With mobjValues.Parameters
		.Add("sCertype", Session("sCertype"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Add("nProduct", Session("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Add("nPolicy", Session("nPolicy"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Add("nCertif", Session("nCertif"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Add("dEffecdate", Session("dEffecdate"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		If IsNothing(Request.QueryString.Item("nGroup")) Then
			.Add("nGroup", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		Else
			.Add("nGroup", mobjValues.StringToType(Request.QueryString.Item("nGroup"), eFunctions.Values.eTypeData.etdDouble, True), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		End If
	End With
	Response.Write(mobjValues.PossiblesValues("valModule", "tabTabModul_CO_PG", eFunctions.Values.eValuesType.clngWindowType, Request.QueryString.Item("nModulec"), True,  ,  ,  ,  , "ChangeValues(""Modulec"",this)", Not mclsCover_quota.bExistModulec,  , GetLocalResourceObject("valModuleToolTip")))
	
Response.Write("" & vbCrLf)
Response.Write("            </TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("	</TABLE>")

	
	For	Each lclsCover_quota In mclsCover_quota.mcolCover_quotas
		With mobjGrid
			
			If lclsCover_quota.nTaxMar = eRemoteDB.Constants.intNull Then
				lclsCover_quota.nTaxMar = 0
			End If
			If lclsCover_quota.nPremium = eRemoteDB.Constants.intNull Then
				lclsCover_quota.nPremium = 0
			End If
			If lclsCover_quota.nTaxIVA = eRemoteDB.Constants.intNull Then
				lclsCover_quota.nTaxIVA = 0
			End If
			If lclsCover_quota.nTax = eRemoteDB.Constants.intNull Then
				lclsCover_quota.nTax = 0
			End If
			If Request.QueryString.Item("sReloadPage") <> "1" Then
				Session("nPremiumOrig") = lclsCover_quota.nPremiumOrig
				Session("nUtilMarOrig") = lclsCover_quota.nTaxOrig
			End If
			.Columns("tcnCover").DefValue = CStr(lclsCover_quota.nCover)
			.Columns("cbeCover").Parameters.Add("nModulec", mobjValues.StringToType(Request.QueryString.Item("nModulec"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Columns("cbeCover").DefValue = CStr(lclsCover_quota.nCover)
			.Columns("cbeRole").DefValue = CStr(lclsCover_quota.nRole)
			.Columns("tcnInsured").DefValue = CStr(lclsCover_quota.nInsucount)
			.Columns("tcnExInsured").DefValue = CStr(lclsCover_quota.nExcInsured)
			.Columns("tcnCapital").DefValue = CStr(lclsCover_quota.nCapital)
			.Columns("tcnIVA").DefValue = CStr(lclsCover_quota.nTaxIVA)
			.Columns("tcnRate").DefValue = CStr(lclsCover_quota.nTax)
			.Columns("tcnUtilMar").DefValue = CStr(lclsCover_quota.nTaxMar)
			.Columns("tcnPremium").DefValue = CStr(lclsCover_quota.nPremium)
			.Columns("hddOldRate").DefValue = CStr(lclsCover_quota.nTax)
			.Columns("hddOldPremium").DefValue = CStr(lclsCover_quota.nPremium)
			.Columns("hddnPremiumOrig").DefValue = CStr(lclsCover_quota.nPremium)
			.Columns("hddUtilMarOrig").DefValue = CStr(lclsCover_quota.nTaxMar)
			Response.Write(mobjGrid.DoRow())
		End With
	Next lclsCover_quota
	Response.Write(mobjGrid.closeTable())
	
Response.Write("" & vbCrLf)
Response.Write("	<BR>" & vbCrLf)
Response.Write("	<TABLE WIDTH=100%>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("        	<TD><LABEL ID=0>" & GetLocalResourceObject("tcnIVACaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.NumericControl("tcnIVA", 18, CStr(mclsCover_quota.mcolCover_quotas.nTotalIVA),  , GetLocalResourceObject("tcnIVAToolTip"), True, 6, True))


Response.Write("</TD>" & vbCrLf)
Response.Write("			<TD>&nbsp;</TD>" & vbCrLf)
Response.Write("			<TD><LABEL ID=0>" & GetLocalResourceObject("tcnPremiumCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.NumericControl("tcnPremium", 18, CStr(mclsCover_quota.mcolCover_quotas.nTotalPremium),  , GetLocalResourceObject("tcnPremiumToolTip"), True, 6, True))


Response.Write("</TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("	</TABLE>")

	
	If mblnError Then
		Response.Write(mclsCover_quota.sErrors)
	End If
	lclsCover_quota = Nothing
	mclsCover_quota = Nothing
End Sub

'% insPreVI666Upd: Se realiza el manejo de la ventana PopUp asociada al grid
'--------------------------------------------------------------------------------------------
Private Sub insPreVI666Upd()
	'--------------------------------------------------------------------------------------------
	With Request
		Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "valPolicySeq.aspx", "VI666", .QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(.QueryString.Item("Index"))))
	End With
End Sub

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("VI666")
'~End Header Block VisualTimer Utility
Response.CacheControl = "private"
mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.44.14
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = Request.QueryString.Item("sCodispl")
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.44.14
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
	Response.Write(mobjMenu.setZone(2, "VI666", Request.QueryString.Item("sWindowDescript"), CShort(Request.QueryString.Item("nWindowTy"))))
	mobjMenu = Nothing
	Response.Write("<SCRIPT>var nMainAction=304;</SCRIPT>")
End If
%>
<SCRIPT>


//% ChangeValues: se controla el cambio de valor de los campos
//-------------------------------------------------------------------------------------------
function ChangeValues(Option, Field){
//-------------------------------------------------------------------------------------------
	var ldblUtilMar
	with(self.document.forms[0]){
		switch(Option){
			case "Premium":
//+ Se recalcula el margen de utilidad de la cobertura basado en la nueva prima
				ldblUtilMar = tcnUtilMar.value;
				hddOldPremium.value = VTFormat(insConvertNumber(hddOldPremium.value), "", "", "", 2, true);
				if (tcnUtilMar.value == ''){
					tcnUtilMar.value = 0;
				}
				if (tcnRate.value == ''){
					tcnRate.value = 0;
				}
				if(hddOldPremium.value!=Field.value)
					if(insConvertNumber(Field.value)==0 ||
						Field.value=='' ||
						insConvertNumber(hddOldPremium.value)==0 ||
						hddOldPremium.value==''){
						if (insConvertNumber(hddOldPremium.value)!=0 &&
							hddOldPremium.value!='' && 
							insConvertNumber(Field.value)!=0 &&
							Field.value!=''){
							tcnUtilMar.value = 0;
							tcnRate.value = 0;
						}
					}
					else{
						tcnUtilMar.value = (insConvertNumber(tcnUtilMar.value)==0)?0:VTFormat(insConvertNumber(Field.value) * insConvertNumber(tcnUtilMar.value) / insConvertNumber(hddOldPremium.value), "", "", "", 2, true);
						tcnRate.value = (insConvertNumber(tcnRate.value)==0)?0:VTFormat(insConvertNumber(Field.value) * insConvertNumber(tcnRate.value) / insConvertNumber(hddOldPremium.value), "", "", "", 6, true);
						
						if (insConvertNumber(tcnUtilMar.value) >= insConvertNumber(1000)){
							tcnUtilMar.value = ldblUtilMar;
							tcnRate.value = hddOldRate.value;
							Field.value = hddOldPremium.value;
							alert('El campo porcentaje de margen de utilidad definida para la póliza no debe ser mayor que 999,99');
						}
						if (insConvertNumber(tcnRate.value) >= insConvertNumber(10000)){
							tcnUtilMar.value = ldblUtilMar;
							tcnRate.value = hddOldRate.value;
							Field.value = hddOldPremium.value;
							alert('El campo monto de tasa comercial calculada para la cobertura no debe ser mayor que 999,999999');
						}
						hddOldRate.value = tcnRate.value;
						hddOldPremium.value = VTFormat(insConvertNumber(Field.value), "", "", "", 2, true);
				}
				break;
			case "Rate":
//+ Se recalcula el margen de utilidad de la cobertura basado en la nueva tasa
				ldblUtilMar = tcnUtilMar.value;
				if (tcnUtilMar.value == ''){
					tcnUtilMar.value = 0;
				}
				if (tcnPremium.value == ''){
					tcnPremium.value = 0;
				}

				if(hddOldRate.value!=Field.value)
					if (insConvertNumber(Field.value)==0 ||
						 Field.value=='' ||
						 insConvertNumber(hddOldRate.value)==0 ||
						 hddOldRate.value==''){
						if (insConvertNumber(hddOldRate.value)!=0 &&
							hddOldRate.value!='' && 
							insConvertNumber(Field.value)!=0 &&
							Field.value!=''){
							tcnUtilMar.value = 0;
							tcnPremium.value = 0;
							Field.value = 0;
						}
					}
					else{
						tcnUtilMar.value = (insConvertNumber(tcnUtilMar.value)==0)?0:VTFormat(insConvertNumber(Field.value) * insConvertNumber(tcnUtilMar.value) / insConvertNumber(hddOldRate.value), "", "", "", 2, true);
						tcnPremium.value = (insConvertNumber(tcnPremium.value)==0)?0:VTFormat(insConvertNumber(Field.value) * insConvertNumber(tcnPremium.value) / insConvertNumber(hddOldRate.value), "", "", "", 2, true);
						
						if (insConvertNumber(tcnUtilMar.value) >= insConvertNumber(1000)){
							tcnUtilMar.value = ldblUtilMar;
							tcnPremium.value = hddOldPremium.value;
							Field.value = hddOldRate.value;
							alert('El campo porcentaje de margen de utilidad definida para la póliza no debe ser mayor que 999,99');
						}
						if (insConvertNumber(tcnPremium.value) >= insConvertNumber(100000000)){
							tcnUtilMar.value = ldblUtilMar;
							tcnPremium.value = hddOldPremium.value;
							Field.value = hddOldRate.value;
							alert('El campo monto de prima para la cobertura no debe ser mayor que 999,999999');
						}

						hddOldPremium.value = tcnPremium.value;
						hddOldRate.value = VTFormat(insConvertNumber(Field.value), "", "", "", 6, true);
					}
				break;
			case "Group":
				self.document.location.href = self.document.location.href.replace(/&nGroup=.*/,'') + "&nGroup=" + valGroup.value  + "&nModulec=" + valModule.value
			case "Modulec":
				self.document.location.href = self.document.location.href.replace(/&nGroup=.*/,'') + "&nGroup=" + valGroup.value  + "&nModulec=" + valModule.value
				break;
		}
	}
}
</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" NAME="VI666" ACTION="valPolicySeq.aspx?sMode=1&nGroup=<%=Request.QueryString.Item("nGroup")%>">
    <%Response.Write(mobjValues.ShowWindowsName("VI666", Request.QueryString.Item("sWindowDescript")))
Call insDefineHeader()
If Request.QueryString.Item("Type") = "PopUp" Then
	Call insPreVI666Upd()
Else
	Call insPreVI666()
End If
mobjValues = Nothing
mobjMenu = Nothing
mobjGrid = Nothing
%>
</FORM>
</BODY>
</HTML>
<%'^Begin Footer Block VisualTimer Utility 1.1 31/3/03 19.44.14
Call mobjNetFrameWork.FinishPage("VI666")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




