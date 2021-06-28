<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eAgent" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo de las rutinas genéricas
Dim mobjGrid As eFunctions.Grid

'- Objeto para el manejo de las zonas de la pantalla
Dim mobjMenu As eFunctions.Menues

'-Objeto para recuperar la información de la página
Dim mclsContrat_Pay As eAgent.Contrat_Pay


'% insDefineHeader:Este procedimiento se encarga de definir las columnas del grid
'-------------------------------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'-------------------------------------------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	
	'+ Se definen las columnas del Grid
	'mobjGrid.nMainAction = Request.QueryString("nMainAction")
	mobjGrid.sArrayName = "marrAG954"
	With mobjGrid.Columns
		.AddHiddenColumn("nSeq", CStr(0))
		.AddPossiblesColumn(0, GetLocalResourceObject("nCodeColumnCaption"), "nCode", "TABTAB_GOALS2", eFunctions.Values.eValuesType.clngWindowType,  , True,  ,  ,  ,  ,  ,  , GetLocalResourceObject("nCodeColumnToolTip"))
		.AddNumericColumn(0, GetLocalResourceObject("nInit_DurColumnCaption"), "nInit_Dur", 5, vbNullString,  , GetLocalResourceObject("nInit_DurColumnToolTip"),  ,  ,  ,  ,  , Request.QueryString.Item("Action") = "Update")
		.AddNumericColumn(0, GetLocalResourceObject("nEnd_DurColumnCaption"), "nEnd_Dur", 5, vbNullString,  , GetLocalResourceObject("nEnd_DurColumnToolTip"),  ,  ,  ,  ,  , Request.QueryString.Item("Action") = "Update")
		.AddNumericColumn(0, GetLocalResourceObject("nPercent_detailColumnCaption"), "nPercent_detail", 9, vbNullString,  , GetLocalResourceObject("nPercent_detailColumnToolTip"), True, 6)
	End With
	
	'+ Se asignan las caracteristicas del Grid
	With mobjGrid
		.Codispl = "AG954"
		.Codisp = "AG954"
		.sCodisplPage = "AG954"
		.Columns("nInit_Dur").EditRecord = True
		mobjGrid.ActionQuery = mobjValues.ActionQuery
		'+ Pase de parametros necesarios para la eliminación de registros
		.sDelRecordParam = "nContrat_Pay=" & Request.QueryString.Item("nContrat_Pay") & "&nSeq='+marrAG954[lintIndex].nSeq + '"
		.Height = 250
		.Width = 400
		.sEditRecordParam = "nContrat_Pay=" & Request.QueryString.Item("nContrat_Pay") & "&sClient=' + self.document.forms[0].sClient.value + '" & "&sDescript=' + self.document.forms[0].sDescript.value + '" & "&dStartDate=' + self.document.forms[0].dStartDate.value + '" & "&nType_Calc=' + self.document.forms[0].nType_Calc.value + '" & "&nPercent=' + self.document.forms[0].nPercent.value + '" & "&nAmount=' + self.document.forms[0].nAmount.value + '" & "&nCurrency=' + self.document.forms[0].nCurrency.value + '" & "&nAply=' + self.document.forms[0].nAply.value + '" & "&sTaxin=' + self.document.forms[0].sTaxin.value + '" & "&sStatregt=' + self.document.forms[0].sStatregt.value + '"
		
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
		
		If Request.QueryString.Item("Type") = "PopUp" Then
			Response.Write(mobjValues.HiddenControl("hddClient", Request.QueryString.Item("sClient")))
			Response.Write(mobjValues.HiddenControl("hddDescript", Request.QueryString.Item("sDescript")))
			Response.Write(mobjValues.HiddenControl("hddStartDate", Request.QueryString.Item("dStartDate")))
			Response.Write(mobjValues.HiddenControl("hddType_Calc", Request.QueryString.Item("nType_Calc")))
			Response.Write(mobjValues.HiddenControl("hddPercent", Request.QueryString.Item("nPercent")))
			Response.Write(mobjValues.HiddenControl("hddAmount", Request.QueryString.Item("nAmount")))
			Response.Write(mobjValues.HiddenControl("hddCurrency", Request.QueryString.Item("nCurrency")))
			Response.Write(mobjValues.HiddenControl("hddAply", Request.QueryString.Item("nAply")))
			Response.Write(mobjValues.HiddenControl("hddTaxin", Request.QueryString.Item("sTaxin")))
			Response.Write(mobjValues.HiddenControl("hddStatregt", Request.QueryString.Item("sStatregt")))
		End If
		
	End With
End Sub

'%insPreAG954: Se cargan los datos iniciales de la página de la parte repetitiva 1
'-------------------------------------------------------------------------------------------------------------------
Private Sub insPreAG954Grid()
	'-------------------------------------------------------------------------------------------------------------------
	Dim lclsContrat_Pay_Detail As eAgent.contrat_pay_detail
	
	If mclsContrat_Pay.blnValues Then
		With mobjGrid
			If mclsContrat_Pay.mcolContrat_Pay_Detail.Count > 0 Then
				For	Each lclsContrat_Pay_Detail In mclsContrat_Pay.mcolContrat_Pay_Detail
					.Columns("nSeq").DefValue = CStr(lclsContrat_Pay_Detail.nSeq)
					.Columns("nCode").DefValue = CStr(lclsContrat_Pay_Detail.nCode)
					If lclsContrat_Pay_Detail.nCode > 0 Then
						.Columns("nCode").Descript = CStr(lclsContrat_Pay_Detail.nCode)
					Else
						.Columns("nCode").Descript = vbNullString
					End If
					
					.Columns("nInit_Dur").DefValue = CStr(lclsContrat_Pay_Detail.nInit_Dur)
					.Columns("nEnd_Dur").DefValue = CStr(lclsContrat_Pay_Detail.nEnd_Dur)
					.Columns("nPercent_detail").DefValue = CStr(lclsContrat_Pay_Detail.nPercent_detail)
					Response.Write(.DoRow)
				Next lclsContrat_Pay_Detail
			End If
			Response.Write("<SCRIPT>document.forms[0].action=document.forms[0].action + '&nContrat_Pay=" & mclsContrat_Pay.mcolContrat_Pay_Detail.Count & "'</" & "Script>")
		End With
	Else
		Response.Write("<SCRIPT>document.forms[0].action=document.forms[0].action + '&nContrat_Pay=0'</" & "Script>")
	End If
	Response.Write(mobjGrid.closeTable())
End Sub

'% insPreAG954Upd: Se realiza el manejo de la ventana PopUp asociada al grid
'------------------------------------------------------------------------------------------------------------------------------
Private Sub insPreAG954Upd()
	'------------------------------------------------------------------------------------------------------------------------------
	With Request
		If .QueryString.Item("Action") = "Del" Then
			Response.Write(mobjValues.ConfirmDelete())
			Call mclsContrat_Pay.InsPostAG954Upd(.QueryString.Item("Action"), CInt(.QueryString.Item("nContrat_Pay")), vbNullString, vbNullString, System.Date.FromOADate(eRemoteDB.Constants.intNull), eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, CStr(eRemoteDB.Constants.intNull), vbNullString, mobjValues.StringToType(.QueryString.Item("nSeq"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, Session("nUsercode"))
		End If
		Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "ValAgent.aspx", .QueryString.Item("sCodispl"), .QueryString.Item("nMainAction"), mobjGrid.ActionQuery, CShort(.QueryString.Item("Index"))))
	End With
	
Response.Write("" & vbCrLf)
Response.Write("<SCRIPT LANGUAGE=javascript>" & vbCrLf)
Response.Write("    var sClient" & vbCrLf)
Response.Write("    var sDescript" & vbCrLf)
Response.Write("    var dStartDate" & vbCrLf)
Response.Write("    var nType_Calc" & vbCrLf)
Response.Write("    var nPercent" & vbCrLf)
Response.Write("    var nAmount" & vbCrLf)
Response.Write("    var nCurrency" & vbCrLf)
Response.Write("    var nAply" & vbCrLf)
Response.Write("    var sTaxin" & vbCrLf)
Response.Write("    var sStatregt" & vbCrLf)
Response.Write("            " & vbCrLf)
Response.Write("    if (typeof(top.opener.document.forms[0].sClient)!=""undefined""){" & vbCrLf)
Response.Write("        top.opener.top.sClient = top.opener.document.forms[0].sClient.value" & vbCrLf)
Response.Write("        top.opener.top.bClient = true" & vbCrLf)
Response.Write("    }" & vbCrLf)
Response.Write("    if (typeof(top.opener.document.forms[0].sDescript)!=""undefined""){" & vbCrLf)
Response.Write("        top.opener.top.sDescript = top.opener.document.forms[0].sDescript.value" & vbCrLf)
Response.Write("        top.opener.top.bDescript = true" & vbCrLf)
Response.Write("    }" & vbCrLf)
Response.Write("    if (typeof(top.opener.document.forms[0].dStartDate)!=""undefined""){" & vbCrLf)
Response.Write("        top.opener.top.dStartDate = top.opener.document.forms[0].dStartDate.value" & vbCrLf)
Response.Write("        top.opener.top.bStartDate = true" & vbCrLf)
Response.Write("    }" & vbCrLf)
Response.Write("    if (typeof(top.opener.document.forms[0].nType_Calc)!=""undefined""){" & vbCrLf)
Response.Write("        top.opener.top.nType_Calc = top.opener.document.forms[0].nType_Calc.value" & vbCrLf)
Response.Write("        top.opener.top.bType_Calc = true" & vbCrLf)
Response.Write("    }" & vbCrLf)
Response.Write("    if (typeof(top.opener.document.forms[0].nPercent)!=""undefined""){" & vbCrLf)
Response.Write("        top.opener.top.nPercent = top.opener.document.forms[0].nPercent.value" & vbCrLf)
Response.Write("        top.opener.top.bPercent = true" & vbCrLf)
Response.Write("    }" & vbCrLf)
Response.Write("    if (typeof(top.opener.document.forms[0].nAmount)!=""undefined""){" & vbCrLf)
Response.Write("        top.opener.top.nAmount = top.opener.document.forms[0].nAmount.value" & vbCrLf)
Response.Write("        top.opener.top.bAmount = true" & vbCrLf)
Response.Write("    }" & vbCrLf)
Response.Write("    if (typeof(top.opener.document.forms[0].nCurrency)!=""undefined""){" & vbCrLf)
Response.Write("        top.opener.top.nCurrency = top.opener.document.forms[0].nCurrency.value" & vbCrLf)
Response.Write("        top.opener.top.bCurrency = true" & vbCrLf)
Response.Write("    }" & vbCrLf)
Response.Write("    if (typeof(top.opener.document.forms[0].nAply)!=""undefined""){" & vbCrLf)
Response.Write("        top.opener.top.nAply = top.opener.document.forms[0].nAply.value" & vbCrLf)
Response.Write("        top.opener.top.bAply = true" & vbCrLf)
Response.Write("    }" & vbCrLf)
Response.Write("    if (typeof(top.opener.document.forms[0].sTaxin)!=""undefined""){    " & vbCrLf)
Response.Write("        top.opener.top.sTaxin = top.opener.document.forms[0].sTaxin.checked" & vbCrLf)
Response.Write("        top.opener.top.bTaxin = true" & vbCrLf)
Response.Write("    }" & vbCrLf)
Response.Write("    if (typeof(top.opener.document.forms[0].sStatregt)!=""undefined""){" & vbCrLf)
Response.Write("        top.opener.top.sStatregt = top.opener.document.forms[0].sStatregt.value" & vbCrLf)
Response.Write("        top.opener.top.bStatregt = true" & vbCrLf)
Response.Write("    }" & vbCrLf)
Response.Write("" & vbCrLf)
Response.Write("</" & "SCRIPT>    ")

	
End Sub

'% InsPreAG954: Esta función permite realizar la lectura de la tabla principal de la transacción. 
'---------------------------------------------------------------------------------------------------
Private Sub InsPreAG954()
	'---------------------------------------------------------------------------------------------------
	Call mclsContrat_Pay.InsPreAG954(CInt(Request.QueryString.Item("nContrat_Pay")))
	
Response.Write("" & vbCrLf)
Response.Write("<TABLE WIDTH=""100%"">" & vbCrLf)
        'Response.Write("    <TR>" & vbCrLf)
        'Response.Write("        <TD><LABEL ID=0>" & GetLocalResourceObject("sClientCaption") & "</LABEL></TD>" & vbCrLf)
        'Response.Write("        <TD COLSPAN=""4"">")


        'Response.Write(mobjValues.ClientControl("sClient", mclsContrat_Pay.sClient,  , GetLocalResourceObject("sClientToolTip"),  ,  , "lblCliename", False,  ,  ,  ,  ,  , True))


        'Response.Write("</TD>" & vbCrLf)
        'Response.Write("    </TR>" & vbCrLf)
Response.Write(mobjValues.HiddenControl("sClient", mclsContrat_Pay.sClient))
Response.Write("    <TR>" & vbCrLf)
Response.Write("        <TD><LABEL ID=0>" & GetLocalResourceObject("sDescriptCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("        <TD>")


Response.Write(mobjValues.TextControl("sDescript", 60, mclsContrat_Pay.sDescript,  , GetLocalResourceObject("sDescriptToolTip")))


Response.Write("</TD>        " & vbCrLf)
Response.Write("        <TD><LABEL ID=0>" & GetLocalResourceObject("dStartDateCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("	    <TD> ")


Response.Write(mobjValues.DateControl("dStartDate", CStr(mclsContrat_Pay.dStartDate),  , GetLocalResourceObject("dStartDateToolTip")))


Response.Write(" </TD>" & vbCrLf)
Response.Write("    </TR>" & vbCrLf)
Response.Write("    <TR>" & vbCrLf)
Response.Write("        <TD><LABEL ID=0>" & GetLocalResourceObject("nType_CalcCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("        <TD>")


Response.Write(mobjValues.PossiblesValues("nType_Calc", "Table8100", eFunctions.Values.eValuesType.clngComboType, CStr(mclsContrat_Pay.nType_Calc),  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("nType_CalcToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("        <TD><LABEL ID=0>" & GetLocalResourceObject("nPercentCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("        <TD>")


Response.Write(mobjValues.NumericControl("nPercent", 9, CStr(mclsContrat_Pay.nPercent),  , GetLocalResourceObject("nPercentToolTip"),  , 6))


Response.Write("</TD>" & vbCrLf)
Response.Write("    </TR>" & vbCrLf)
Response.Write("    <TR>" & vbCrLf)
Response.Write("        <TD><LABEL ID=0>" & GetLocalResourceObject("nCurrencyCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("        <TD>")


Response.Write(mobjValues.PossiblesValues("nCurrency", "Table11", eFunctions.Values.eValuesType.clngComboType, CStr(mclsContrat_Pay.nCurrency),  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("nCurrencyToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("        <TD><LABEL ID=0>" & GetLocalResourceObject("nAmountCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("        <TD>")


Response.Write(mobjValues.NumericControl("nAmount",  18,  mclsContrat_Pay.nAmount,   , GetLocalResourceObject("nAmountToolTip"),  , 6))


Response.Write("</TD>" & vbCrLf)
Response.Write("    </TR>" & vbCrLf)
Response.Write("    <TR>" & vbCrLf)
Response.Write("        <TD><LABEL ID=0>" & GetLocalResourceObject("nAplyCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("        <TD>")


Response.Write(mobjValues.PossiblesValues("nAply", "Table8101", eFunctions.Values.eValuesType.clngComboType, CStr(mclsContrat_Pay.nAply),  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("nAplyToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("        <TD>")


Response.Write(mobjValues.CheckControl("sTaxin", GetLocalResourceObject("sTaxinCaption"), mclsContrat_Pay.sTaxin, "1",  ,  ,  , GetLocalResourceObject("sTaxinToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("        <TD>")


Response.Write(mobjValues.HiddenControl("nContrat_Pay", Request.QueryString.Item("nContrat_Pay")))


Response.Write("</TD>" & vbCrLf)
Response.Write("    </TR>" & vbCrLf)
        Response.Write("    <TR>" & vbCrLf)
        Response.Write("        <TD><LABEL ID=0>" & GetLocalResourceObject("nType_accoCaption") & "</LABEL></TD>" & vbCrLf)
        Response.Write("        <TD>" & vbCrLf)
        Response.Write("        ")

	
        mobjValues.BlankPosition = False
        mobjValues.TypeList = CShort("2")
        mobjValues.List = "2"
        Response.Write(mobjValues.PossiblesValues("nTyp_acco", "Table400", eFunctions.Values.eValuesType.clngComboType, mclsContrat_Pay.nTyp_acco, , , , , , , , , GetLocalResourceObject("nType_accoToolTip")))
	
        Response.Write("" & vbCrLf)
        Response.Write("        </TD>" & vbCrLf)
Response.Write("        <TD><LABEL ID=0>" & GetLocalResourceObject("sStatregtCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("        <TD>" & vbCrLf)
Response.Write("        ")

	
	mobjValues.BlankPosition = False
	mobjValues.TypeList = CShort("2")
	mobjValues.List = "2"
	Response.Write(mobjValues.PossiblesValues("sStatregt", "Table26", eFunctions.Values.eValuesType.clngComboType, mclsContrat_Pay.sStatregt,  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("sStatregtToolTip")))
	
Response.Write("" & vbCrLf)
Response.Write("        </TD>" & vbCrLf)
Response.Write("    </TR>" & vbCrLf)
Response.Write("    <TR> </TR>" & vbCrLf)
Response.Write("</TABLE>" & vbCrLf)
Response.Write("<TABLE WIDTH=""100%"">" & vbCrLf)
Response.Write("    <TR>            " & vbCrLf)
Response.Write("        <TD CLASS=""HighLighted""><LABEL ID=0>" & GetLocalResourceObject("AnchorCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("    </TR>" & vbCrLf)
Response.Write("    <TR>" & vbCrLf)
Response.Write("        <TD CLASS=""Horline""></TD>" & vbCrLf)
Response.Write("    </TR>  " & vbCrLf)
Response.Write("    <TR>" & vbCrLf)
Response.Write("        <TD>" & vbCrLf)
Response.Write("            ")

	
	Call insPreAG954Grid()
Response.Write("" & vbCrLf)
Response.Write("        </TD>" & vbCrLf)
Response.Write("    </TR>" & vbCrLf)
Response.Write("</TABLE>")

	
	mclsContrat_Pay = Nothing
	Response.Write("<SCRIPT>")
	Response.Write("with (document.forms[0]){")
	Response.Write("if (top.bClient) sClient.value = top.sClient;")
	Response.Write("if (top.bDescript) sDescript.value = top.sDescript;")
	Response.Write("if (top.dStartDate) dStartDate.value = top.dStartDate;")
	Response.Write("if (top.bType_Calc) nType_Calc.value = top.nType_Calc;")
	Response.Write("if (top.bPercent) nPercent.value = top.nPercent;")
	Response.Write("if (top.bAmount) nAmount.value = top.nAmount;")
	Response.Write("if (top.bCurrency) nCurrency.value = top.nCurrency;")
	Response.Write("if (top.bAply) nAply.value = top.nAply;")
	Response.Write("if (top.bTaxin) sTaxin.checked = top.sTaxin;")
	Response.Write("if (top.bStatregt) sStatregt.value = top.sStatregt;")
	Response.Write("}")
	Response.Write("</" & "Script>")
End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mclsContrat_Pay = New eAgent.Contrat_Pay
mobjValues.ActionQuery = Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionQuery) Or Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionCut)
mobjValues.sCodisplPage = "AG954"
%>
<HTML>
<HEAD>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>




<%
Response.Write(mobjValues.StyleSheet())
If Request.QueryString.Item("Type") <> "PopUp" Then
	mobjMenu = New eFunctions.Menues
	Response.Write("<SCRIPT>var nMainAction=" & Request.QueryString.Item("nMainAction") & "</SCRIPT>")
	Response.Write(mobjMenu.setZone(2, "AG954", ""))
	mobjMenu = Nothing
End If
%>
<SCRIPT>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 3 $|$$Date: 14/11/03 12:57 $|$$Author: Nvaplat18 $"
</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="frmAG954" ACTION="ValAgent.aspx?nMainAction=<%=Request.QueryString.Item("nMainAction")%>&nContrat_Pay=<%=Request.QueryString.Item("nContrat_Pay")%>">
<%
Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl")))
Call insDefineHeader()
If Request.QueryString.Item("Type") <> "PopUp" Then
	Call InsPreAG954()
Else
	Call insPreAG954Upd()
End If
mobjValues = Nothing
%>
</FORM>
</BODY>
</HTML>




