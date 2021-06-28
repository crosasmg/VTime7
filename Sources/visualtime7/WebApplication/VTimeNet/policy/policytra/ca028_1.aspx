<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="ePolicy" %>
<script language="VB" runat="Server">

Dim mobjGrid As eFunctions.Grid
Dim mobjValues As eFunctions.Values


'% insDefineHeader: se definen las Carac. del grid
'--------------------------------------------------------------------------------------------
Private Function insDefineHeader() As Object
	'--------------------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	mobjGrid.sCodisplPage = "CA028_1"
	
	With mobjGrid.Columns
		Call .AddClientColumn(0, GetLocalResourceObject("dtcClientColumnCaption"), "dtcClient", vbNullString,  , GetLocalResourceObject("dtcClientColumnToolTip"),  , True)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnCodeItem_vColumnCaption"), "tcnCodeItem_v", 5, vbNullString,  , GetLocalResourceObject("tcnCodeItem_vColumnToolTip"),  ,  ,  ,  ,  , True)
		Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeType_vColumnCaption"), "cbeType_v", "Table298", eFunctions.Values.eValuesType.clngComboType, CStr(0),  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeType_vColumnToolTip"))
		Call .AddTextColumn(0, GetLocalResourceObject("tctElementColumnCaption"), "tctElement", 20, vbNullString,  , GetLocalResourceObject("tctElementColumnToolTip"),  ,  ,  , True)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnPremiumAColumnCaption"), "tcnPremiumA", 18, vbNullString, False, GetLocalResourceObject("tcnPremiumAColumnToolTip"), True, 6,  ,  , "changevaluesField(""Premium"",this)", Request.QueryString.Item("sAddTax") <> "1")
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnPremiumEColumnCaption"), "tcnPremiumE", 18, vbNullString, False, GetLocalResourceObject("tcnPremiumEColumnToolTip"), True, 6,  ,  , "changevaluesField(""Premium"",this)", Request.QueryString.Item("sAddTax") = "1")
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnCommi_rateColumnCaption"), "tcnCommi_rate", 4, vbNullString,  , GetLocalResourceObject("tcnCommi_rateColumnToolTip"), True, 2)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnCommissionColumnCaption"), "tcnCommission", 18, vbNullString,  , GetLocalResourceObject("tcnCommissionColumnToolTip"), True, 6)
		Call .AddHiddenColumn("tcnCodeItem", vbNullString)
		Call .AddHiddenColumn("cbeType", vbNullString)
		Call .AddHiddenColumn("hddAddTax", vbNullString)
		Call .AddHiddenColumn("hddPremium", vbNullString)
		Call .AddHiddenColumn("hddType_detai", Request.QueryString.Item("nType"))
		Call .AddHiddenColumn("hddDisexprc", Request.QueryString.Item("nCodeItem"))
		Call .AddHiddenColumn("hddId_bill", vbNullString)
	End With
	With mobjGrid
		.Columns("Sel").GridVisible = False
		.Columns("cbeType_v").EditRecord = True
		.ActionQuery = CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401 Or CDbl(Request.QueryString.Item("nPrem_det")) = 1
		.AddButton = False
		.DeleteButton = False
		.Codispl = "CA028_1"
		.Width = 450
		.Height = 360
		.Top = 60
		.Left = 60
		Call .Splits_Renamed.AddSplit(0, vbNullString, 4)
		Call .Splits_Renamed.AddSplit(0, GetLocalResourceObject("4ColumnCaption"), 4)
	End With
End Function

'% inspreCA028_1: se cargan los valores de la ventana
'--------------------------------------------------------------------------------------------
Private Function inspreCA028_1() As Object
	'--------------------------------------------------------------------------------------------
	Dim lclsTDetail_pre As Object
	Dim lcolTDetail_pre As ePolicy.TDetail_pres
	Dim lintShowButton As Integer
	Dim ldblPremium As Object
	lcolTDetail_pre = New ePolicy.TDetail_pres
	
	Call lcolTDetail_pre.inspreCA028_1(Session("nBranch"), Session("nProduct"), mobjValues.StringToType(Request.QueryString.Item("nCodeItem"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(Request.QueryString.Item("dIssueDate"), eFunctions.Values.eTypeData.etdDate), Request.QueryString.Item("sPrem_det"), Session("SessionID"), Session("nUsercode"), mobjValues.StringToType(Request.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdInteger))
	
Response.Write("" & vbCrLf)
Response.Write("	<TABLE WIDTH=100%>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD WIDTH=""30%""><LABEL>" & GetLocalResourceObject("AnchorCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			<TD CLASS=""Field"">")


Response.Write(Request.QueryString.Item("nCodeItem"))


Response.Write("</TD>" & vbCrLf)
Response.Write("			<TD>&nbsp;</TD>" & vbCrLf)
Response.Write("			<TD><LABEL>" & GetLocalResourceObject("Anchor2Caption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			<TD CLASS=""Field"">")


Response.Write(mobjValues.getMessage(CShort(Request.QueryString.Item("nType")), "Table298"))


Response.Write("</TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD><LABEL>" & GetLocalResourceObject("Anchor3Caption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			<TD CLASS=""Field"">")


Response.Write(Request.QueryString.Item("sDescript"))


Response.Write("</TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("	</TABLE>")

	
	Response.Write("<DIV ID=""Scroll"" STYLE=""width:630;height:230;overflow:auto;outset gray"">")
	If lcolTDetail_pre.Count = 0 Then
		lintShowButton = eFunctions.Values.eButtonsToShow.OnlyCancel
	Else
		lintShowButton = eFunctions.Values.eButtonsToShow.OnlyAccept
	End If
	ldblPremium = 0
	For	Each lclsTDetail_pre In lcolTDetail_pre
		With mobjGrid
			.Columns("dtcClient").DefValue = lclsTDetail_pre.sClient
			.Columns("tcnCodeItem").DefValue = lclsTDetail_pre.nAplic_code
			.Columns("tcnCodeItem_v").DefValue = lclsTDetail_pre.nAplic_code
			.Columns("tcnCodeItem").DefValue = lclsTDetail_pre.nItem
			.Columns("cbeType").DefValue = lclsTDetail_pre.nType
			.Columns("cbeType_v").DefValue = lclsTDetail_pre.nAplication
			.Columns("tctElement").DefValue = lclsTDetail_pre.sShort_des
			.Columns("tcnPremiumA").DefValue = lclsTDetail_pre.nPremiumA
			.Columns("tcnPremiumE").DefValue = lclsTDetail_pre.nPremiumE
			.Columns("tcnCommi_rate").DefValue = lclsTDetail_pre.nCommi_rate
			.Columns("tcnCommission").DefValue = lclsTDetail_pre.nCommission
			.Columns("hddId_bill").DefValue = lclsTDetail_pre.nId_bill
			.Columns("hddAddTax").DefValue = lclsTDetail_pre.sAddtax
			ldblPremium = ldblPremium + lclsTDetail_pre.nPremium
			.sEditRecordParam = "dIssueDate=" & Request.QueryString.Item("dIssueDate") & "&sAddTax=" & lclsTDetail_pre.sAddtax
			Response.Write(.DoRow)
		End With
	Next lclsTDetail_pre
	With Response
		.Write(mobjGrid.closeTable)
		.Write(mobjValues.HiddenControl("hddTotPremium", ldblPremium))
		.Write("</DIV>")
	End With
	
Response.Write("  <BR>" & vbCrLf)
Response.Write("	<TABLE WIDTH=100%>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD COLSPAN=""2"">&nbsp;</TD>" & vbCrLf)
Response.Write("			<TD ALIGN=""RIGHT""><LABEL>" & GetLocalResourceObject("tcnTotalPremCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			<TD WIDTH=""20%"" ALIGN=""RIGHT"">")


Response.Write(mobjValues.NumericControl("tcnTotalPrem", 18, ldblPremium,  , GetLocalResourceObject("tcnTotalPremToolTip"), True, 6, True))


Response.Write("</TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("		<TR CLASS=""HeigthRow"">" & vbCrLf)
Response.Write("			<TD COLSPAN=""4""></TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD COLSPAN=""4"" CLASS=""HORLINE""></TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD WIDTH=3%>")


Response.Write(mobjValues.ButtonAbout("CA028_1"))


Response.Write("</TD>" & vbCrLf)
Response.Write("			<TD WIDTH=3%>")


Response.Write(mobjValues.ButtonHelp("CA028_1"))


Response.Write("</TD>" & vbCrLf)
Response.Write("			<TD COLSPAN=""2"" ALIGN=""RIGHT"">")


Response.Write(mobjValues.ButtonAcceptCancel("insAcceptGrid(3);", "insAcceptGrid(2)", False,  , lintShowButton))


Response.Write("</TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("	</TABLE>")

	
	lcolTDetail_pre = Nothing
	lclsTDetail_pre = Nothing
End Function

'% inspreCA028_1: se realiza el manejo de la ventana cuando ésta es PopUp
'--------------------------------------------------------------------------------------------
Private Function inspreCA028_1Upd() As Object
	'--------------------------------------------------------------------------------------------
	With Request
		Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "ValPolicyTra.aspx", .QueryString.Item("sCodispl"), .QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(.QueryString.Item("Index"))))
	End With
End Function

</script>
<%Response.Expires = -1
mobjValues = New eFunctions.Values
mobjValues.sCodisplPage = "CA028_1"
%>
<HTML>
<HEAD>
    <META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>


<%Response.Write(mobjValues.StyleSheet())
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write("<SCRIPT>var nMainAction=" & Request.QueryString.Item("nMainAction") & "</SCRIPT>")
End If
%>
<SCRIPT>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 2 $|$$Date: 2/12/03 20:13 $|$$Author: Nvaplat18 $"
//% insAcceptGrid: se realizan las acciones al aceptar el detalle del rec/des/imp
//-------------------------------------------------------------------------------------------
function insAcceptGrid(sPrem_det){
//-------------------------------------------------------------------------------------------
//+ Si el total de prima es igual a cero (0), se toma como no procesado
	if(self.document.forms[0].hddTotPremium.value==0)
		sPrem_det='2';
	opener.document.forms[0].hddPrem_det_proc.value=sPrem_det;
	top.close();
}

//% changevaluesField: se controla el cambio de valor de los campos de la ventana
//--------------------------------------------------------------------------------------------
function changevaluesField(Option, Field){
//--------------------------------------------------------------------------------------------
    switch(Option){
        case "Premium":
			with(self.document.forms[0]){
				if (Field.value != '' && 
				    Field.value != '0'){
					hddPremium.value = Field.value;
					if (Field.name == 'tcnPremiumA'){
						tcnPremiumE.value = '';
					}
					else{
						tcnPremiumA.value = '';
					}
				}
			}
            break;
   }
}
</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="CA028_1" ACTION="ValPolicyTra.aspx?sTime=1&dIssueDate=<%=Request.QueryString.Item("dIssueDate")%>">
<%
Response.Write(mobjValues.ShowWindowsName("CA028_1"))
Response.Write(mobjValues.WindowsTitle("CA028_1"))

Call insDefineHeader()
If Request.QueryString.Item("Type") = "PopUp" Then
	Call inspreCA028_1Upd()
Else
	Call inspreCA028_1()
End If

mobjValues = Nothing
mobjGrid = Nothing
%>
</FORM>
</BODY>
</HTML>





