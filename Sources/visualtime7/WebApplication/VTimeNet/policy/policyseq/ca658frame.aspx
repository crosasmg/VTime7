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

'- Objeto para el manejo particular de los datos de la página
Dim mcolClient_tmp As ePolicy.Client_tmps

Dim mclsClient_tmp As ePolicy.Client_tmp

'- Variable para indicar si la póliza tiene grupos colectivos asociados
Dim mblnGroups As Object


'% insDefineHeader: se definen las propiedades del grid
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	Dim lstrCompon As String
	mobjGrid = New eFunctions.Grid
	'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.42.04
	mobjGrid.sSessionID = Session.SessionID
	mobjGrid.nUsercode = Session("nUsercode")
	'~End Body Block VisualTimer Utility
	
	mobjGrid.sCodisplPage = Request.QueryString.Item("sCodispl")
	Call mobjGrid.SetWindowParameters(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), Request.QueryString.Item("nWindowTy"))
	
	mclsClient_tmp = New ePolicy.Client_tmp
	
	If Session("nCertif") = 0 Then
		lstrCompon = "1"
	Else
		lstrCompon = "2"
	End If
	
	Call mclsClient_tmp.insPreCA658(Request.QueryString.Item("Type"), Session("sPolitype"), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("npolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate))
	
	mblnGroups = mclsClient_tmp.bGroupsExist
	
	
	'+ Se definen las columnas del grid    
	With mobjGrid.Columns
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnIdColumnCaption"), "tcnId", 4, vbNullString,  , GetLocalResourceObject("tcnIdColumnToolTip"),  ,  ,  ,  ,  , True)
		If mblnGroups Then
			Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeGroupColumnCaption"), "cbeGroup", "tabGroups", eFunctions.Values.eValuesType.clngWindowType, vbNullString, True,  ,  ,  ,  , Request.QueryString.Item("Action") = "Update", 4, GetLocalResourceObject("cbeGroupColumnToolTip"), eFunctions.Values.eTypeCode.eNumeric)
			mobjGrid.Columns("cbeGroup").Parameters.Add("sCertype", Session("sCertype"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			mobjGrid.Columns("cbeGroup").Parameters.Add("nBranch", mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			mobjGrid.Columns("cbeGroup").Parameters.Add("nProduct", mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			mobjGrid.Columns("cbeGroup").Parameters.Add("nPolicy", mobjValues.StringToType(Session("npolicy"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		End If
		Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeRoleColumnCaption"), "cbeRole", "TabCliallopro", eFunctions.Values.eValuesType.clngComboType, CStr(0), True,  ,  ,  ,  , Request.QueryString.Item("Action") = "Update",  , GetLocalResourceObject("cbeRoleColumnToolTip"))
		Call .AddDateColumn(0, GetLocalResourceObject("tcdBirthDatColumnCaption"), "tcdBirthDat", vbNullString,  , GetLocalResourceObject("tcdBirthDatColumnToolTip"),  ,  ,  , mclsClient_tmp.DefaultValueCA658(Request.QueryString.Item("nOptAge"), "tcdBirthDat"))
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnAgeColumnCaption"), "tcnAge", 3, vbNullString,  , GetLocalResourceObject("tcnAgeColumnToolTip"),  ,  ,  ,  ,  , mclsClient_tmp.DefaultValueCA658(Request.QueryString.Item("nOptAge"), "tcnAge"))
		Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeTAgeColumnCaption"), "cbeTAge", "tabAge_Collect_CA658", eFunctions.Values.eValuesType.clngComboType, "2", True,  ,  ,  , "insChangeValues(""Age"")", mclsClient_tmp.DefaultValueCA658(Request.QueryString.Item("nOptAge"), "cbeTAge"),  , GetLocalResourceObject("cbeTAgeColumnToolTip"), eFunctions.Values.eTypeCode.eString)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnInitAgeColumnCaption"), "tcnInitAge", 3, vbNullString,  , GetLocalResourceObject("tcnInitAgeColumnToolTip"),  ,  ,  ,  ,  , True)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnEndAgeColumnCaption"), "tcnEndAge", 3, vbNullString,  , GetLocalResourceObject("tcnEndAgeColumnToolTip"),  ,  ,  ,  ,  , True)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnInsuredColumnCaption"), "tcnInsured", 10, "1",  , GetLocalResourceObject("tcnInsuredColumnToolTip"))
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnRentAmountColumnCaption"), "tcnRentAmount", 18, vbNullString,  , GetLocalResourceObject("tcnRentAmountColumnToolTip"), True, 6)
		Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeCurrencyColumnCaption"), "cbeCurrency", "Table11", eFunctions.Values.eValuesType.clngComboType, CStr(0),  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeCurrencyColumnToolTip"))
		Call .AddCheckColumn(0, GetLocalResourceObject("chkVIPColumnCaption"), "chkVIP", vbNullString,  , "1",  , Request.QueryString.Item("Type") <> "PopUp")
	End With
	
	'+ Se definen las propiedades generales del grid
	
	With mobjGrid
		.Codispl = "CA658"
		.ActionQuery = Session("bQuery")
		.bOnlyForQuery = .ActionQuery
		.Height = 430
		.Width = 400
		.Top = 80
		.nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
		.sEditRecordParam = "nOptAge=' + self.document.forms[0].OptAge.value + '"
		If mblnGroups Then
			.sDelRecordParam = "nGroup=' + marrArray[lintIndex].cbeGroup + '&nRole=' + marrArray[lintIndex].cbeRole + '&nId=' + marrArray[lintIndex].tcnId + '"
		Else
			.sDelRecordParam = "nGroup=0&nRole=' + marrArray[lintIndex].cbeRole + '&nId=' + marrArray[lintIndex].tcnId + '"
		End If
		.Columns("cbeRole").EditRecord = True
		.Columns("cbeTAge").Parameters.Add("nBranch", mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Columns("cbeTAge").Parameters.Add("nProduct", mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Columns("cbeTAge").Parameters.Add("dEffecDate", mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Columns("cbeTAge").Parameters.ReturnValue("nInitage", False,  , True)
		.Columns("cbeTAge").Parameters.ReturnValue("nEndage", False,  , True)
		.Columns("cbeRole").Parameters.Add("nBranch", mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Columns("cbeRole").Parameters.Add("nProduct", mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Columns("cbeRole").Parameters.Add("sPolitype", Session("sPolitype"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Columns("cbeRole").Parameters.Add("sCompon", "", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
	With Response
		'+ Campos para almacenar las opciones para el ingreso de edades, tipo de nómina y 
		'+ la existencia de grupos colectivos para la póliza
		.Write(mobjValues.HiddenControl("OptAge", Request.QueryString.Item("nOptAge")))
		.Write(mobjValues.HiddenControl("optType", "1"))
		.Write(mobjValues.HiddenControl("hddExistGroups", mblnGroups))
	End With
	mclsClient_tmp = Nothing
End Sub

'% insPreCA658: se realiza el manejo del grid
'--------------------------------------------------------------------------------------------
Private Sub insPreCA658()
	'--------------------------------------------------------------------------------------------
	mcolClient_tmp = New ePolicy.Client_tmps
	
	If mcolClient_tmp.Find(Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("npolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate)) Then
		For	Each mclsClient_tmp In mcolClient_tmp
			With mobjGrid
				.Columns("tcnId").DefValue = CStr(mclsClient_tmp.nId)
				If mblnGroups Then
					.Columns("cbeGroup").DefValue = CStr(mclsClient_tmp.nGroup)
				End If
				.Columns("cbeRole").DefValue = CStr(mclsClient_tmp.nRole)
				.Columns("tcdBirthDat").DefValue = CStr(mclsClient_tmp.dBirthdate)
				.Columns("tcnAge").DefValue = CStr(mclsClient_tmp.nAge)
				.Columns("cbeTAge").DefValue = CStr(mclsClient_tmp.nInitAge)
				.Columns("tcnInitAge").DefValue = CStr(mclsClient_tmp.nInitAge)
				.Columns("tcnEndAge").DefValue = CStr(mclsClient_tmp.nEndAge)
				.Columns("tcnInsured").DefValue = CStr(mclsClient_tmp.nInsured)
				.Columns("tcnRentAmount").DefValue = CStr(mclsClient_tmp.nRentAmount)
				.Columns("cbeCurrency").DefValue = CStr(mclsClient_tmp.nCurrency)
				.Columns("chkVIP").Checked = CShort(mclsClient_tmp.sVIP)
				Response.Write(.DoRow)
			End With
		Next mclsClient_tmp
		Response.Write("<SCRIPT>top.frames['fraFolder'].document.forms[0].optAge[" & CDbl(mcolClient_tmp(1).sTypeAge) - 1 & "].checked = true;</" & "Script>")
		Response.Write("<SCRIPT>self.document.forms[0].OptAge.value=" & mcolClient_tmp(1).sTypeAge & "</" & "Script>")
		Response.Write("<SCRIPT>top.frames['fraFolder'].document.forms[0].hddMessCtrl.value = " & mcolClient_tmp(1).sTypeAge & ";</" & "Script>")
	Else
		If Request.QueryString.Item("nOptAge") = "1" Then
			Response.Write("<SCRIPT>(top.frames['fraFolder'].document.forms[0].optAge[0].disabled)?top.frames['fraFolder'].document.forms[0].optAge[2].checked=true:top.frames['fraFolder'].document.forms[0].optAge[0].checked=true;</" & "Script>")
		Else
			Response.Write("<SCRIPT>top.frames['fraFolder'].document.forms[0].optAge[1].checked=true;</" & "Script>")
		End If
		Response.Write("<SCRIPT>top.frames['fraFolder'].document.forms[0].hddMessCtrl.value = 0;</" & "Script>")
	End If
	With Response
		.Write("<SCRIPT>setPointer('');</" & "Script>")
		.Write("<SCRIPT>top.frames['fraFolder'].document.forms[0].tcnInsured.value = " & mcolClient_tmp.nTotalInsured & ";</" & "Script>")
		.Write(mobjGrid.closeTable())
	End With
	mclsClient_tmp = Nothing
	mcolClient_tmp = Nothing
End Sub

'% insPreCA658Upd: Se realiza el manejo de la ventana PopUp asociada al grid
'--------------------------------------------------------------------------------------------
Private Sub insPreCA658Upd()
	'--------------------------------------------------------------------------------------------
	Response.Write(mobjValues.ShowWindowsName("CA658", Request.QueryString.Item("sWindowDescript")))
	
	Dim lclsClient_tmp As ePolicy.Client_tmp
	
	lclsClient_tmp = New ePolicy.Client_tmp
	
	With Request
		If Request.QueryString.Item("Action") = "Del" Then
			Call lclsClient_tmp.inspostCA658(Request.QueryString.Item("Type"), Request.QueryString.Item("Action"), Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("npolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nGroup"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nRole"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nId"), eFunctions.Values.eTypeData.etdDouble))
			Response.Write(mobjValues.ConfirmDelete())
		End If
		
		Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "valPolicySeq.aspx", "CA658", .QueryString.Item("nMainAction"), mobjGrid.ActionQuery, CShort(.QueryString.Item("Index"))))
		If Request.QueryString.Item("Action") = "Add" Then
			Response.Write("<SCRIPT>insChangeValues(""Id"")</" & "Script>")
		End If
	End With
	lclsClient_tmp = Nothing
End Sub

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("CA658Frame")
'~End Header Block VisualTimer Utility
Response.CacheControl = "private"

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.42.04
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "CA658Frame"



%>
<HTML>
<HEAD>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
	<%
Response.Write(mobjValues.StyleSheet())
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
End If
%>
<SCRIPT>
//% insChangeValues: se controla el valor de los controles
//-------------------------------------------------------------------------------------------
function insChangeValues(Option){
//-------------------------------------------------------------------------------------------
//+ Se define la variable para almacenar el consecutivo más alto existente en el grid
    var llngMax = 0
    switch(Option){
		case "Age":
				insDefValues("ReaAge_collectCA658", "nInitAge=" + self.document.forms[0].cbeTAge.value)
			break;

		case "Id":
			with(self.document.forms[0]){
				for(var llngIndex = 0;llngIndex<top.opener.marrArray.length;llngIndex++){
				    if(top.opener.marrArray[llngIndex].tcnId > llngMax)
				        llngMax = top.opener.marrArray[llngIndex].tcnId
				}
				tcnId.value = ++llngMax;
			}
	}
}

//lstrMessage = lclsGeneral.insLoadMessage(55893)
</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" NAME="CA658Frame" ACTION="valPolicySeq.aspx?scodispl=CA658">
<%Call insDefineHeader()
If Request.QueryString.Item("Type") = "PopUp" Then
	Call insPreCA658Upd()
Else
	Call insPreCA658()
End If
mobjGrid = Nothing
mobjValues = Nothing
mcolClient_tmp = Nothing
%>
</FORM> 
</BODY>
</HTML>
<%'^Begin Footer Block VisualTimer Utility 1.1 31/3/03 19.42.04
Call mobjNetFrameWork.FinishPage("CA658Frame")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




