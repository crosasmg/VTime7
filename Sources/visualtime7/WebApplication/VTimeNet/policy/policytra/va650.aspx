<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="ePolicy" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 10.31.23
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility
'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
'- Objeto para el manejo del grid de la página
Dim mobjGrid As eFunctions.Grid
'- Objeto para el manejo del menú
Dim mobjMenu As eFunctions.Menues
'- Objeto para el manejo particular de los datos de la página
Dim mclsAccount_Pol As ePolicy.Account_Pol
Dim mstrKey As String


'% ReaInitial: Lee la información a mostrar en la ventana
'--------------------------------------------------------------------------------------------
Private Sub ReaInitial()
	'--------------------------------------------------------------------------------------------
	With Request
		mclsAccount_Pol.insPreVA650("2", mobjValues.StringToType(.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.QueryString.Item("nTypemove"), eFunctions.Values.eTypeData.etdDouble), Session("nUsercode"), .QueryString.Item("sKey"))
	End With
End Sub
'% insInitial: Pinta los controles de la parte puntual de la página
'--------------------------------------------------------------------------------------------
Private Sub insInitial()
	'--------------------------------------------------------------------------------------------
	Response.Write(mobjValues.HiddenControl("tctKey", mclsAccount_Pol.mcolMove_accpols.sKey))
	
Response.Write("" & vbCrLf)
Response.Write("<BR>" & vbCrLf)
Response.Write("<TABLE WIDTH=""100%"">" & vbCrLf)
Response.Write("    <TR>" & vbCrLf)
Response.Write("        <TD><LABEL ID=0>" & GetLocalResourceObject("txtClientCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("        <TD>")


Response.Write(mobjValues.TextControl("txtClient", 14, mclsAccount_Pol.sClient,  , GetLocalResourceObject("txtClientToolTip"), True) & "-" & mobjValues.TextControl("txtCliename", 40, mclsAccount_Pol.sCliename,  , "Nombre del contratante", True))


Response.Write("</TD>" & vbCrLf)
Response.Write("    </TR>" & vbCrLf)
Response.Write("    <TR>" & vbCrLf)
Response.Write("        <TD COLSPAN=""5"" CLASS=""HighLighted""><LABEL ID=0>" & GetLocalResourceObject("AnchorCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("    </TR>" & vbCrLf)
Response.Write("    <TR>" & vbCrLf)
Response.Write("        <TD COLSPAN=""5"" CLASS=""HorLine""></TD>" & vbCrLf)
Response.Write("    </TR>" & vbCrLf)
Response.Write("</TABLE>" & vbCrLf)
Response.Write("<TABLE WIDTH=""100%"">" & vbCrLf)
Response.Write("    <TR>" & vbCrLf)
Response.Write("        <TD><LABEL ID=0>" & GetLocalResourceObject("cbeCurrencyCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("        <TD>")


Response.Write(mobjValues.PossiblesValues("cbeCurrency", "Table11", eFunctions.Values.eValuesType.clngComboType, CStr(mclsAccount_Pol.nCurrency),  , True,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeCurrencyToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("        <TD>&nbsp;</TD>" & vbCrLf)
Response.Write("        <TD><LABEL ID=0>" & GetLocalResourceObject("tcnValuepolCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("        <TD>")


Response.Write(mobjValues.NumericControl("tcnValuepol", 18, CStr(mclsAccount_Pol.nValuepol),  , GetLocalResourceObject("tcnValuepolToolTip"), True, 6, True))


Response.Write("</TD>" & vbCrLf)
Response.Write("    </TR>" & vbCrLf)
Response.Write("    <TR>" & vbCrLf)
Response.Write("        <TD><LABEL ID=0>" & GetLocalResourceObject("tcnAmosurrenCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("        <TD>")


Response.Write(mobjValues.NumericControl("tcnAmosurren", 18, CStr(mclsAccount_Pol.nAmosurren),  , GetLocalResourceObject("tcnAmosurrenToolTip"), True, 6, True))


Response.Write("</TD>" & vbCrLf)
Response.Write("        <TD>&nbsp;</TD>" & vbCrLf)
Response.Write("        <TD><LABEL ID=0>" & GetLocalResourceObject("tcnFixchargeCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("        <TD>")


Response.Write(mobjValues.NumericControl("tcnFixcharge", 18, CStr(mclsAccount_Pol.nFixcharge),  , GetLocalResourceObject("tcnFixchargeToolTip"), True, 6, True))


Response.Write("</TD>" & vbCrLf)
Response.Write("    </TR>" & vbCrLf)
Response.Write("    <TR>" & vbCrLf)
Response.Write("        <TD><LABEL ID=0>" & GetLocalResourceObject("tcnCovercostCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("        <TD>")


Response.Write(mobjValues.NumericControl("tcnCovercost", 18, CStr(mclsAccount_Pol.nCovercost),  , GetLocalResourceObject("tcnCovercostToolTip"), True, 6, True))


Response.Write("</TD>" & vbCrLf)
Response.Write("        <TD>&nbsp;</TD>" & vbCrLf)
Response.Write("        <TD><LABEL ID=0>" & GetLocalResourceObject("tcnProfitCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("        <TD>")


Response.Write(mobjValues.NumericControl("tcnProfit", 18, CStr(mclsAccount_Pol.nProfit),  , GetLocalResourceObject("tcnProfitToolTip"), True, 6, True))


Response.Write("</TD>" & vbCrLf)
Response.Write("    </TR>" & vbCrLf)
Response.Write("    <TR>" & vbCrLf)
Response.Write("        <TD><LABEL ID=0>" & GetLocalResourceObject("tcnNetpaysCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("        <TD>")


Response.Write(mobjValues.NumericControl("tcnNetpays", 18, CStr(mclsAccount_Pol.nNetpays),  , GetLocalResourceObject("tcnNetpaysToolTip"), True, 6, True))


Response.Write("</TD>" & vbCrLf)
Response.Write("        <TD>&nbsp;</TD>" & vbCrLf)
Response.Write("        <TD><LABEL ID=0>" & GetLocalResourceObject("tcnPaysCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("        <TD>")


Response.Write(mobjValues.NumericControl("tcnPays", 18, CStr(mclsAccount_Pol.nPays),  , GetLocalResourceObject("tcnPaysToolTip"), True, 6, True))


Response.Write("</TD>" & vbCrLf)
Response.Write("    </TR>" & vbCrLf)
Response.Write("    <TR>" & vbCrLf)
Response.Write("        <TD><LABEL ID=0>" & GetLocalResourceObject("tcdLastpayCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("        <TD>")


Response.Write(mobjValues.DateControl("tcdLastpay", CStr(mclsAccount_Pol.dLastpay),  , GetLocalResourceObject("tcdLastpayToolTip"), True))


Response.Write("</TD>" & vbCrLf)
Response.Write("        <TD>&nbsp;</TD>" & vbCrLf)
Response.Write("        <TD><LABEL ID=0>" & GetLocalResourceObject("tcdLastdateCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("        <TD>")


Response.Write(mobjValues.DateControl("tcdLastdate", CStr(mclsAccount_Pol.dLastdate),  , GetLocalResourceObject("tcdLastdateToolTip"), True))


Response.Write("</TD>" & vbCrLf)
Response.Write("    </TR>" & vbCrLf)
Response.Write("    <TR>" & vbCrLf)
Response.Write("        <TD><LABEL ID=0>" & GetLocalResourceObject("tcdVP_negCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("        <TD>")


Response.Write(mobjValues.DateControl("tcdVP_neg", CStr(mclsAccount_Pol.dVp_neg),  , GetLocalResourceObject("tcdVP_negToolTip"), True))


Response.Write("</TD>" & vbCrLf)
Response.Write("    </TR>" & vbCrLf)
Response.Write("</TABLE>" & vbCrLf)
Response.Write("<BR>")

End Sub
'% insDefineHeader: se definen las propiedades del grid
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.31.23
	mobjGrid.sSessionID = Session.SessionID
	mobjGrid.nUsercode = Session("nUsercode")
	'~End Body Block VisualTimer Utility
	
	mobjGrid.sCodisplPage = "va650"
	Call mobjGrid.SetWindowParameters(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), Request.QueryString.Item("nWindowTy"))
	
	'+ Se definen las columnas del grid
	With mobjGrid.Columns
		.AddNumericColumn(0, GetLocalResourceObject("tcnYearColumnCaption"), "tcnYear", 4, vbNullString,  , GetLocalResourceObject("tcnYearColumnToolTip"),  ,  ,  ,  ,  , True)
		.AddNumericColumn(0, GetLocalResourceObject("tcnMonthColumnCaption"), "tcnMonth", 2, vbNullString,  , GetLocalResourceObject("tcnMonthColumnToolTip"),  ,  ,  ,  ,  , True)
		.AddPossiblesColumn(0, GetLocalResourceObject("tcnTypemoveColumnCaption"), "tcnTypemove", "Table5525", eFunctions.Values.eValuesType.clngComboType, vbNullString,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("tcnTypemoveColumnToolTip"))
		.AddNumericColumn(0, GetLocalResourceObject("tcnAmountColumnCaption"), "tcnAmount", 18, vbNullString,  , GetLocalResourceObject("tcnAmountColumnToolTip"), True, 6,  ,  , "if (typeof(self.document.forms[0].hddnAmount)!='undefined') self.document.forms[0].hddnAmount.value = this.value")
		.AddHiddenColumn("tctKeyG", "")
		If Request.QueryString.Item("nTypemove") = "1" Then
			.AddHiddenColumn("hddnAmount", "")
		End If
	End With
	'+ Se definen las propiedades generales del grid
	With mobjGrid
		.Codispl = "VA650"
		.ActionQuery = mobjValues.ActionQuery
		.AddButton = False
		.DeleteButton = False
		.bCheckVisible = False
		.Height = 250
		.Width = 280
		.nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
		.Columns("Sel").GridVisible = False
		If Request.QueryString.Item("sReload") <> "Yes" Then
			.sEditRecordParam = "nBranch=" & Request.QueryString.Item("nBranch") & "&nProduct=" & Request.QueryString.Item("nProduct") & "&nPolicy=" & Request.QueryString.Item("nPolicy") & "&nCertif=" & Request.QueryString.Item("nCertif") & "&dEffecdate=" & Request.QueryString.Item("dEffecdate") & "&nTypemove=" & Request.QueryString.Item("nTypemove") & "&sKey=" & mstrKey
		Else
			.sEditRecordParam = "nBranch=" & Request.QueryString.Item("nBranch") & "&nProduct=" & Request.QueryString.Item("nProduct") & "&nPolicy=" & Request.QueryString.Item("nPolicy") & "&nCertif=" & Request.QueryString.Item("nCertif") & "&dEffecdate=" & Request.QueryString.Item("dEffecdate") & "&nTypemove=" & Request.QueryString.Item("nTypemove") & "&sKey=" & Request.QueryString.Item("sKey")
		End If
		If Request.QueryString.Item("nTypemove") = "1" Then
			.Columns("tcnTypemove").EditRecord = True
		End If
	End With
End Sub
'% insPreVA650: se realiza el manejo del grid
'--------------------------------------------------------------------------------------------
Private Sub insPreVA650()
	'--------------------------------------------------------------------------------------------
	Dim lclsMove_accpol As ePolicy.Move_accpol
	If Request.QueryString.Item("sReload") = "Yes" Then
		lclsMove_accpol = New ePolicy.Move_accpol
		If lclsMove_accpol.Find_Tmp_move_accpol(Request.QueryString.Item("sKey"), "2", mobjValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nCertif"), eFunctions.Values.eTypeData.etdDouble)) Then
			With mobjGrid
				.Columns("tctKeyG").DefValue = Request.QueryString.Item("Skey")
				.Columns("tcnYear").DefValue = CStr(lclsMove_accpol.nYear)
				.Columns("tcnMonth").DefValue = CStr(lclsMove_accpol.nMonth)
				.Columns("tcnTypemove").DefValue = CStr(lclsMove_accpol.Typemove(mobjValues.StringToType(Request.QueryString.Item("nTypemove"), eFunctions.Values.eTypeData.etdDouble)))
				.Columns("tcnAmount").DefValue = CStr(lclsMove_accpol.nAmount)
				If Request.QueryString.Item("nTypemove") = "1" Then
					.Columns("hddnAmount").DefValue = CStr(lclsMove_accpol.nAmount)
				End If
				Response.Write(.DoRow)
			End With
		End If
		lclsMove_accpol = Nothing
	Else
		'    Dim lclsMove_accpol
		For	Each lclsMove_accpol In mclsAccount_Pol.mcolMove_accpols
			With mobjGrid
				.Columns("tctKeyG").DefValue = mclsAccount_Pol.mcolMove_accpols.sKey
				.Columns("tcnYear").DefValue = CStr(lclsMove_accpol.nYear)
				.Columns("tcnMonth").DefValue = CStr(lclsMove_accpol.nMonth)
				.Columns("tcnTypemove").DefValue = CStr(lclsMove_accpol.Typemove(mobjValues.StringToType(Request.QueryString.Item("nTypemove"), eFunctions.Values.eTypeData.etdDouble)))
				.Columns("tcnAmount").DefValue = CStr(lclsMove_accpol.nAmount)
				If Request.QueryString.Item("nTypemove") = "1" Then
					.Columns("hddnAmount").DefValue = CStr(lclsMove_accpol.nAmount)
				End If
				Response.Write(.DoRow)
			End With
		Next lclsMove_accpol
	End If
	Response.Write(mobjGrid.closeTable())
End Sub
'% insPreVA650Upd: Se realiza el manejo de la ventana PopUp asociada al grid
'--------------------------------------------------------------------------------------------
Private Sub insPreVA650Upd()
	'--------------------------------------------------------------------------------------------
	With Request
		Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "valPolicyTra.aspx", "VA650", .QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(.QueryString.Item("Index"))))
	End With
End Sub

</script>
<%
Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("va650")
With Server
	mobjMenu = New eFunctions.Menues
	'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.31.23
	mobjMenu.sSessionID = Session.SessionID
	mobjMenu.nUsercode = Session("nUsercode")
	'~End Body Block VisualTimer Utility
	mobjValues = New eFunctions.Values
	'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.31.23
	mobjValues.sSessionID = Session.SessionID
	mobjValues.nUsercode = Session("nUsercode")
	'~End Body Block VisualTimer Utility
	
	mobjValues.sCodisplPage = "va650"
	mclsAccount_Pol = New ePolicy.Account_Pol
End With
mobjValues.ActionQuery = CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401
%>
<HTML>
<HEAD>
<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 3 $|$$Date: 15/10/03 16:53 $|$$Author: Nvaplat61 $"
</SCRIPT>
<%
Response.Write(mobjValues.StyleSheet())
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write(mobjMenu.setZone(2, "VA650", Request.QueryString.Item("sWindowDescript"), CShort(Request.QueryString.Item("nWindowTy"))))
	mobjMenu = Nothing
	Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
End If
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" NAME="VA650" ACTION="valPolicyTra.aspx?nBranch=<%=Request.QueryString.Item("nBranch")%>&nProduct=<%=Request.QueryString.Item("nProduct")%>&nPolicy=<%=Request.QueryString.Item("nPolicy")%>&nCertif=<%=Request.QueryString.Item("nCertif")%>&dEffecdate=<%=Request.QueryString.Item("dEffecdate")%>&nTypemove=<%=Request.QueryString.Item("nTypemove")%>">
<%
Response.Write(mobjValues.ShowWindowsName("VA650", Request.QueryString.Item("sWindowDescript")))
If Request.QueryString.Item("Type") <> "PopUp" Then
	Call ReaInitial()
	Call insInitial()
	mstrKey = mclsAccount_Pol.mcolMove_accpols.sKey
Else
	mstrKey = Request.QueryString.Item("sKey")
End If
Call insDefineHeader()
If Request.QueryString.Item("Type") = "PopUp" Then
	Call insPreVA650Upd()
Else
	Call insPreVA650()
End If

mclsAccount_Pol = Nothing
mobjGrid = Nothing
mobjValues = Nothing
%>
</FORM>
</BODY>
</HTML>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 10.31.23
Call mobjNetFrameWork.FinishPage("va650")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




