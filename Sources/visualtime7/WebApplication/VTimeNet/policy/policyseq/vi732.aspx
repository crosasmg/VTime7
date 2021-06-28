<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="ePolicy" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 31/3/03 19.44.16
Dim mobjNetFrameWork As eNetFrameWork.Layout

Dim mobjGrid As eFunctions.Grid
Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues
'~End Body Block VisualTimer Utility
Dim mclsGuarant_val As ePolicy.Guarant_val


'% insDefineHeader: se definen las propiedades del grid
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.44.16
	mobjGrid.sSessionID = Session.SessionID
	mobjGrid.nUsercode = Session("nUsercode")
	'~End Body Block VisualTimer Utility
	mobjGrid.sCodisplPage = Request.QueryString.Item("sCodispl")
	Call mobjGrid.SetWindowParameters(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), Request.QueryString.Item("nWindowTy"))
	'+ Se definen las columnas del grid    
	With mobjGrid.Columns
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnYearColumnCaption"), "tcnYear", 4, vbNullString,  , GetLocalResourceObject("tcnYearColumnToolTip"),  ,  ,  ,  ,  , Request.QueryString.Item("Action") = "Update")
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnMonthColumnCaption"), "tcnMonth", 2, vbNullString,  , GetLocalResourceObject("tcnMonthColumnToolTip"),  ,  ,  ,  ,  , Request.QueryString.Item("Action") = "Update")
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnAgeColumnCaption"), "tcnAge", 3, vbNullString,  , GetLocalResourceObject("tcnAgeColumnToolTip"),  ,  ,  ,  ,  , Request.QueryString.Item("Action") = "Update")
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnSaldvalkmColumnCaption"), "tcnSaldvalkm", 18, vbNullString,  , GetLocalResourceObject("tcnSaldvalkmColumnToolTip"), True, 6)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnResc_valColumnCaption"), "tcnResc_val", 18, vbNullString,  , GetLocalResourceObject("tcnResc_valColumnToolTip"), True, 6)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnSald_valColumnCaption"), "tcnSald_val", 18, vbNullString,  , GetLocalResourceObject("tcnSald_valColumnToolTip"), True, 6)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnPro_yearColumnCaption"), "tcnPro_year", 3, vbNullString,  , GetLocalResourceObject("tcnPro_yearColumnToolTip"))
		Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeDeferredColumnCaption"), "cbeDeferred", "Table5586", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeDeferredColumnToolTip"))
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnDefamountColumnCaption"), "tcnDefamount", 18, vbNullString,  , GetLocalResourceObject("tcnDefamountColumnToolTip"), True, 6)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnSal_taxColumnCaption"), "tcnSal_tax", 6, vbNullString,  , GetLocalResourceObject("tcnSal_taxColumnCaption"),  , 4)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnPeriod_covColumnCaption"), "tcnPeriod_cov", 3, vbNullString,  , GetLocalResourceObject("tcnPeriod_covColumnToolTip"))
		Call .AddHiddenColumn("cbeCurrency", vbNullString)
	End With
	'+ Se definen las propiedades generales del grid
	With mobjGrid
		.Codispl = "VI732"
		.ActionQuery = mobjValues.ActionQuery
		.Columns("tcnYear").EditRecord = mclsGuarant_val.sAut_guarval = "2"
		.DeleteButton = mclsGuarant_val.sAut_guarval = "2"
		.AddButton = mclsGuarant_val.sAut_guarval = "2"
		.Columns("Sel").GridVisible = Not .ActionQuery And mclsGuarant_val.sAut_guarval = "2"
		.sDelRecordParam = "nYear='+ marrArray[lintIndex].tcnYear + '" & "&nMonth='+ marrArray[lintIndex].tcnMonth + '" & "&nAge='+ marrArray[lintIndex].tcnAge + '"
		.Top = 50
		.Height = 420
		.Width = 400
		.nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
	Response.Write(mobjValues.HiddenControl("hddAut_guarval", mclsGuarant_val.sAut_guarval))
End Sub

'% insPreVi732: se realiza el manejo del grid
'--------------------------------------------------------------------------------------------
Private Sub insPreVi732()
	'--------------------------------------------------------------------------------------------
	Dim lclsErrors As eFunctions.Errors
	Dim lclsGuarant_val As Object
	
Response.Write("" & vbCrLf)
Response.Write("    <TABLE WIDTH=""100%"">" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("			<TD CLASS=""HIGHLIGHTED"" COLSPAN=""2""><LABEL ID=""0"">" & GetLocalResourceObject("AnchorCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			<TD COLSPAN=""3"">&nbsp;</TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("			<TD COLSPAN=""2"" CLASS=""HORLINE""></TD>" & vbCrLf)
Response.Write("			<TD COLSPAN=""3""></TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("		    <TD COLSPAN=""2"">")


Response.Write(mobjValues.OptionControl(10, "optAut_guarval", GetLocalResourceObject("optAut_guarval_CStr2Caption"), CStr(CDbl(mclsGuarant_val.sAut_guarval) - 1), CStr(2), "ChangeValues()",  , 1, GetLocalResourceObject("optAut_guarval_CStr2ToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("		    <TD WIDTH=10%>&nbsp;</TD>" & vbCrLf)
Response.Write("		    <TD><LABEL>" & GetLocalResourceObject("cbeCurrency_ACaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")

	
	mobjValues.BlankPosition = False
	With mobjValues.Parameters
		.Add("sCertype", Session("sCertype"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Add("nProduct", Session("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Add("nPolicy", Session("nPolicy"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Add("nCertif", Session("nCertif"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Add("dEffecdate", Session("dEffecdate"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	End With
	Response.Write(mobjValues.PossiblesValues("cbeCurrency_A", "tabCurren_pol", eFunctions.Values.eValuesType.clngComboType, CStr(mclsGuarant_val.nCurrency), True,  ,  ,  ,  , "ChangeValues()",  ,  , GetLocalResourceObject("cbeCurrency_AToolTip"),  , 3))
	
Response.Write("" & vbCrLf)
Response.Write("			</TD>" & vbCrLf)
Response.Write("			" & vbCrLf)
Response.Write("	    </TR>" & vbCrLf)
Response.Write("	    <TR>" & vbCrLf)
Response.Write("			<TD COLSPAN=""2"">")


Response.Write(mobjValues.OptionControl(10, "optAut_guarval", GetLocalResourceObject("optAut_guarval_CStr1Caption"), mclsGuarant_val.sAut_guarval, CStr(1), "ChangeValues()",  , 2, GetLocalResourceObject("optAut_guarval_CStr1ToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("			<TD COLSPAN=""3"">&nbsp;</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("    </TABLE>" & vbCrLf)
Response.Write("	<BR>")

	
	For	Each lclsGuarant_val In mclsGuarant_val.mcolGuarant_val
		With mobjGrid
			.Columns("tcnYear").DefValue = lclsGuarant_val.nYear
			.Columns("tcnMonth").DefValue = lclsGuarant_val.nMonth
			.Columns("tcnAge").DefValue = lclsGuarant_val.nAge
			.Columns("tcnSaldvalkm").DefValue = lclsGuarant_val.nSaldvalkm
			.Columns("tcnResc_val").DefValue = lclsGuarant_val.nResc_val
			.Columns("tcnSald_val").DefValue = lclsGuarant_val.nSald_val
			.Columns("tcnPro_year").DefValue = lclsGuarant_val.nPro_year
			.Columns("cbeDeferred").DefValue = lclsGuarant_val.nDeferred
			.Columns("cbeDeferred").Descript = lclsGuarant_val.sDeferred_Desc
			.Columns("tcnDefamount").DefValue = lclsGuarant_val.nDefamount
			.Columns("tcnSal_tax").DefValue = lclsGuarant_val.nSal_tax
			.Columns("tcnPeriod_cov").DefValue = lclsGuarant_val.nPeriod_cov
			Response.Write(.DoRow)
		End With
	Next lclsGuarant_val
	Response.Write(mobjGrid.closeTable())
	If mclsGuarant_val.bError Then
		lclsErrors = New eFunctions.Errors
		'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.44.16
		lclsErrors.sSessionID = Session.SessionID
		lclsErrors.nUsercode = Session("nUsercode")
		'~End Body Block VisualTimer Utility
		Response.Write(lclsErrors.ErrorMessage("VI732", mclsGuarant_val.nError,  ,  ,  , True))
	End If
	lclsErrors = Nothing
End Sub

'% insPreVi732Upd: Se realiza el manejo de la ventana PopUp asociada al grid
'--------------------------------------------------------------------------------------------
Private Sub insPreVi732Upd()
	'--------------------------------------------------------------------------------------------
	Dim lclsGuarant_val As ePolicy.Guarant_val
	lclsGuarant_val = New ePolicy.Guarant_val
	With Request
		If .QueryString.Item("Action") = "Del" Then
			Response.Write(mobjValues.ConfirmDelete())
			If lclsGuarant_val.insPostVI732(.QueryString.Item("Action"), Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), Session("dEffecdate"), vbNullString,  , mobjValues.StringTotype(.QueryString.Item("nYear"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringTotype(.QueryString.Item("nAge"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringTotype(.QueryString.Item("nMonth"), eFunctions.Values.eTypeData.etdDouble),  ,  ,  ,  , Session("nUsercode")) Then
			End If
		End If
		Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "valPolicySeq.aspx", "VI732", .QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(.QueryString.Item("Index"))))
	End With
	If Request.QueryString.Item("Action") <> "Del" Then
		With Response
			.Write("<SCRIPT>")
			.Write("self.document.forms[0].cbeCurrency.value=top.opener.document.forms[0].cbeCurrency_A.value;")
			.Write("self.document.forms[0].hddAut_guarval.value=(top.opener.document.forms[0].optAut_guarval[0].checked)?2:1;")
			.Write("</" & "Script>")
		End With
	End If
	lclsGuarant_val = Nothing
End Sub

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("VI732")
'~End Header Block VisualTimer Utility
Response.CacheControl = "private"

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.44.16
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = Request.QueryString.Item("sCodispl")
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.44.16
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")

mclsGuarant_val = New ePolicy.Guarant_val

If Request.QueryString.Item("Type") <> "PopUp" Then
	Call mclsGuarant_val.insPreVi732(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), Session("dEffecdate"), Request.QueryString.Item("sAut_guarval"), mobjValues.StringTotype(Request.QueryString.Item("nCurrency"), eFunctions.Values.eTypeData.etdDouble), Session("nUsercode"), Request.QueryString.Item("sDelete"), Session("bQuery"))
End If

mobjValues.ActionQuery = mclsGuarant_val.bError Or Session("bQuery")

%>
<HTML>
<HEAD>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<%
With Response
	.Write(mobjValues.StyleSheet())
	If Request.QueryString.Item("Type") <> "PopUp" Then
		.Write(mobjMenu.setZone(2, "VI732", Request.QueryString.Item("sWindowDescript"), CShort(Request.QueryString.Item("nWindowTy"))))
		.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
	End If
	mobjMenu = Nothing
End With
%>
<SCRIPT LANGUAGE="JAVASCRIPT">
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 6 $|$$Date: 6/08/04 12:46 $|$$Author: Nvaplat22 $"
    
//% ChangeValues: se controla el cambio de valor de los campos puntuales de la página
/*---------------------------------------------------------------------------------------------------------*/
function ChangeValues(){
/*---------------------------------------------------------------------------------------------------------*/
    var lstrURL = "";
    lstrURL += document.location;
    
	lstrURL = lstrURL.replace(/&nCurrency=.*/, "");
	lstrURL = lstrURL.replace(/&sAut_guarval=.*/, "");
	
	with(self.document.forms[0]){
		if (optAut_guarval[0].checked) 
		    lstrURL = lstrURL + "&sAut_guarval=" + optAut_guarval[0].value;
		else
			lstrURL = lstrURL + "&sAut_guarval=" + optAut_guarval[1].value;
		
		lstrURL = lstrURL + "&nCurrency="+ cbeCurrency_A.value + "&sDelete=1";
	}
	self.document.location = lstrURL;
}
</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" NAME="VI732" ACTION="valPolicySeq.aspx?sMode=2">
<%
Response.Write(mobjValues.ShowWindowsName("VI732", Request.QueryString.Item("sWindowDescript")))
Call insDefineHeader()
If Request.QueryString.Item("Type") = "PopUp" Then
	Call insPreVi732Upd()
Else
	Call insPreVi732()
End If

mobjGrid = Nothing
mobjValues = Nothing
mclsGuarant_val = Nothing
%>	
</FORM> 
</BODY>
</HTML>
<%'^Begin Footer Block VisualTimer Utility 1.1 31/3/03 19.44.16
Call mobjNetFrameWork.FinishPage("VI732")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




