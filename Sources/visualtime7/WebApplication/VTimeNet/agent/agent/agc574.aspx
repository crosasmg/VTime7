<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eAgent" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 11.11.56
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility
'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
Dim mobjGrid As eFunctions.Grid
Dim llngAction As Object
Dim mobjMenu As eFunctions.Menues


'% insDefineHeader: se definen las propiedades del grid 
'----------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'----------------------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.11.56
	mobjGrid.sSessionID = SESSION.SessionID
	mobjGrid.nUsercode = SESSION("nUsercode")
	'~End Body Block VisualTimer Utility
	
	mobjGrid.sCodisplPage = "agc574"
	Call mobjGrid.SetWindowParameters(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), Request.QueryString.Item("nWindowTy"))
	
	With mobjGrid.Columns
		.AddPossiblesColumn(40010, GetLocalResourceObject("cbeBranchColumnCaption"), "cbeBranch", "Table10", eFunctions.Values.eValuesType.clngComboType, CStr(0),  ,  ,  ,  , "document.forms[0].valProduct.Parameters.Param1.sValue=this.value", Request.QueryString.Item("Action") = "Update",  , GetLocalResourceObject("cbeBranchColumnCaption"))
		.AddPossiblesColumn(40011, GetLocalResourceObject("valProductColumnCaption"), "valProduct", "tabProdmaster1", eFunctions.Values.eValuesType.clngWindowType, Request.Form.Item("valProduct"), True,  ,  ,  ,  , Request.QueryString.Item("Action") = "Update", 4, GetLocalResourceObject("valProductColumnToolTip"))
		.AddNumericColumn(40012, GetLocalResourceObject("tcnBudColumnCaption"), "tcnBud", 14, CStr(0),  , GetLocalResourceObject("tcnBudColumnCaption"), True, 2)
		If llngAction = eFunctions.Menues.TypeActions.clngActionQuery Then
			.AddNumericColumn(40013, GetLocalResourceObject("tcnRealColumnCaption"), "tcnReal", 14, CStr(0),  , GetLocalResourceObject("tcnRealColumnCaption"), True, 2)
			.AddNumericColumn(40014, GetLocalResourceObject("tcnPercentColumnCaption"), "tcnPercent", 5, CStr(0),  , GetLocalResourceObject("tcnPercentColumnCaption"), True, 2)
		End If
	End With
	
	With mobjGrid
		.Codispl = "AGC574"
		.Width = 300
		.Height = 210
		.AddButton = llngAction = eFunctions.Menues.TypeActions.clngActionadd
		.DeleteButton = llngAction <> eFunctions.Menues.TypeActions.clngActionQuery
		.Columns("Sel").GridVisible = llngAction <> eFunctions.Menues.TypeActions.clngActionQuery
		.Columns("cbeBranch").EditRecord = llngAction <> eFunctions.Menues.TypeActions.clngActionQuery
		.Columns("valProduct").Parameters.Add("nBranch", 0)
		.nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
	End With
End Sub
'---------------------------------------------------------------------------------------------
Private Sub insPreAGC574()
	'----------------------------------------------------------------------------------------------
	Dim lintIntermed As Integer
	Dim lintCurrency As Integer
	Dim lstrType_infor As String
	Dim lstrPeriodtyp As String
	Dim lintPeriodnum As Integer
	Dim ldtmEffecdate As Date
	Dim lobjInterm_bud As eAgent.Interm_buds
	Dim lintCount As Integer
	Dim ldblBud_total As Object
	Dim ldblReal_total As Object
	Dim ldblPerc_total As Object
	
	lobjInterm_bud = New eAgent.Interm_buds
	ldblBud_total = 0
	ldblReal_total = 0
	
	lintIntermed = mobjValues.StringToType(SESSION("nIntermed"), eFunctions.Values.eTypeData.etdDouble)
	lintCurrency = mobjValues.StringToType(SESSION("nCurrency"), eFunctions.Values.eTypeData.etdDouble)
	lstrType_infor = SESSION("sType_infor")
	lstrPeriodtyp = SESSION("sPeriodtyp")
	lintPeriodnum = mobjValues.StringToType(SESSION("nPeriodnum"), eFunctions.Values.eTypeData.etdDouble)
	ldtmEffecdate = mobjValues.StringToType(SESSION("dEffecdate"), eFunctions.Values.eTypeData.etdDate)
	
	Call lobjInterm_bud.Find(lintIntermed, lintCurrency, lstrType_infor, lstrPeriodtyp, lintPeriodnum, ldtmEffecdate)
	
	mobjGrid.sDelRecordParam = "nBranch=' + marrArray[lintIndex].cbeBranch + '&nProduct=' + marrArray[lintIndex].valProduct + '&nBud=' + marrArray[lintIndex].tcnBud + '"
	For lintCount = 1 To lobjInterm_bud.Count
		With mobjGrid
			.Columns("cbeBranch").DefValue = CStr(lobjInterm_bud.Item(lintCount).nBranch)
			.Columns("valProduct").Parameters.Add("nBranch", lobjInterm_bud.Item(lintCount).nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
            .Columns("valProduct").DefValue = CStr(lobjInterm_bud.Item(lintCount).nProduct)
			.Columns("tcnBud").DefValue = CStr(lobjInterm_bud.Item(lintCount).nBud_total)
			If llngAction = eFunctions.Menues.TypeActions.clngActionQuery Then
				.Columns("tcnReal").DefValue = CStr(lobjInterm_bud.Item(lintCount).nReal_total)
				If lobjInterm_bud.Item(lintCount).nBud_total <> 0 Then
					.Columns("tcnPercent").DefValue = CStr(lobjInterm_bud.Item(lintCount).nReal_total * 100 / lobjInterm_bud.Item(lintCount).nBud_total)
				Else
					.Columns("tcnPercent").DefValue = CStr(0)
				End If
				ldblReal_total = ldblReal_total + lobjInterm_bud.Item(lintCount).nReal_total
			End If
			ldblBud_total = ldblBud_total + lobjInterm_bud.Item(lintCount).nBud_total
		End With
		Response.Write(mobjGrid.DoRow())
	Next 
	
	If ldblBud_total <> 0 Then
		ldblPerc_total = ldblReal_total * 100 / ldblBud_total
	Else
		ldblPerc_total = 0
	End If
	
	Response.Write(mobjGrid.CloseTable())
	
Response.Write("" & vbCrLf)
Response.Write("    <TABLE WIDTH=100%>" & vbCrLf)
Response.Write("		")

	If llngAction = eFunctions.Menues.TypeActions.clngActionQuery Then
Response.Write("" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD COLSPAN=""5"" CLASS=""HIGHLIGHTED""><LABEL ID=40009><A NAME=""Total"">" & GetLocalResourceObject("AnchorTotalCaption") & "</A></LABEL></TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD COLSPAN=""5""><HR></TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD WIDTH=80pcx><LABEL ID=8038>" & GetLocalResourceObject("tcnBudTotalCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			<TD>&nbsp;</TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.NumericControl("tcnBudTotal", 18, ldblBud_total,  , "", True, 6, True))


Response.Write("</TD>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("            <TD><LABEL ID=8037>" & GetLocalResourceObject("tcnRealTotalCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>&nbsp;</TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.NumericControl("tcnRealTotal", 18, ldblReal_total,  , "", True, 6, True))


Response.Write("</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("			<TD><LABEL ID=8039>" & GetLocalResourceObject("tcnPercentTotalCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>&nbsp;</TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.NumericControl("tcnPercentTotal", 18, ldblPerc_total,  , "", True, 6, True))


Response.Write("</TD>" & vbCrLf)
Response.Write("            <TD COLSPAN=""2"">&nbsp;</TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("		")

	End If
	
	If Request.QueryString.Item("Reload") = "1" Then
		'+ Se recarga la ventana PopUp, en caso que el check de "Continuar" se encuentre marcado
		Select Case Request.QueryString.Item("ReloadAction")
			Case "Add"
				Response.Write("<SCRIPT>EditRecord(-1,nMainAction,'Add')</" & "Script>")
			Case "Update"
				Response.Write("<SCRIPT>EditRecord(" & Request.QueryString.Item("ReloadIndex") & ",nMainAction,'Update')</" & "Script>")
		End Select
	End If
	
	mobjGrid = Nothing
	lobjInterm_bud = Nothing
	mobjValues = Nothing
	
End Sub

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = SESSION.SessionID
mobjNetFrameWork.nUsercode = SESSION("nUsercode")
Call mobjNetFrameWork.BeginPage("agc574")

llngAction = Request.QueryString.Item("nMainAction")
mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.11.56
mobjValues.sSessionID = SESSION.SessionID
mobjValues.nUsercode = SESSION("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "agc574"
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.11.56
mobjMenu.sSessionID = SESSION.SessionID
mobjMenu.nUsercode = SESSION("nUsercode")
'~End Body Block VisualTimer Utility
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write(mobjMenu.setZone(2, "AGC574", Request.QueryString.Item("sWindowDescript"), CShort(Request.QueryString.Item("nWindowTy"))))
End If
mobjMenu = Nothing
%>
<HTML>
    <%="<SCRIPT>nMainAction='" & Request.QueryString.Item("nMainAction") & "'</SCRIPT>"%>
<HEAD>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>    
    <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">

<SCRIPT>
//+ Variable para el control de versiones
    document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 13.15 $"        
</SCRIPT>        





	<%=mobjValues.StyleSheet()%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="frmIntermBud" ACTION="valAgent.aspx?nMainAction=<%=Request.QueryString.Item("nMainAction")%>">
<%
Response.Write(mobjValues.ShowWindowsName("AGC574", Request.QueryString.Item("sWindowDescript")))
Call insDefineHeader()
Call insPreAGC574()

%>
</FORM>
</BODY>
</HTML>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 11.11.56
Call mobjNetFrameWork.FinishPage("agc574")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




