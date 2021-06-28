<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eCollection" %>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 3/4/03 12.00.00
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

Dim mcolt_move_acc As eCollection.t_move_accs
Dim mobjt_move_acc As Object
    Dim mobjValues As New eFunctions.Values
Dim mobjGrid As eFunctions.Grid
Dim mobjMenues As eFunctions.Menues
Dim mdblTotals As Double

Dim mlngCount As Integer
Dim mdblPaidAmount As Double
Dim mdblTotalAmount As Double
Dim mdblTotalAmountGen As Double
Dim mdblExchangeUF As Object
Dim mstrTable5008 As String


'% insPrevInf: Se encarga de obtener la información inicial de la carga de la transacción.
'---------------------------------------------------------------------------------------------------------
Private Sub insPrevInf()
	'---------------------------------------------------------------------------------------------------------      
	Call mcolt_move_acc.findCO012(Session("CO001_nAction"), _
                                  Request.QueryString.Item("Type"), _
                                  mobjValues.StringToType(Session("nBordereaux"), eFunctions.Values.eTypeData.etdDouble), _
                                  Session("sStatus"), _
                                  mobjValues.StringToType(Session("dCollectDate"), eFunctions.Values.eTypeData.etdDate), _
                                  Session("sRel_Type"), _
                                  mobjValues.StringToType(Session("nAgreement"), eFunctions.Values.eTypeData.etdInteger), _
                                  mobjValues.StringToType(Session("dValueDate"), eFunctions.Values.eTypeData.etdDate), _
                                  Session("sRelOrigi"))

                                  

	
	mlngCount = mcolt_move_acc.nCount
	mdblPaidAmount = System.Math.Round(mcolt_move_acc.nPaidAmount)
	mdblTotalAmount = System.Math.Round(mcolt_move_acc.nTotalAmount)
	mdblTotalAmountGen = System.Math.Round(mcolt_move_acc.nTotalAmountGen)
	mstrTable5008 = mcolt_move_acc.sTable5008
	
End Sub

'% insPreCO012: Se encarga de cargar los datos de la ventana.
'---------------------------------------------------------------------------------------------------------
Private Sub insPreCO012()
	'---------------------------------------------------------------------------------------------------------   
	mdblTotals = System.Math.Abs(mdblTotalAmountGen)
	
	If mlngCount > 0 Then
		Response.Write(mobjValues.HiddenControl("nItems", CStr(mcolt_move_acc.nCount)))
		
		With mobjGrid
			For	Each mobjt_move_acc In mcolt_move_acc
				.Columns("Sel").Checked = CShort("0")
				
				If mobjt_move_acc.nCredit = 0 Then
					.Columns("Sel").disabled = True
				Else
					.Columns("Sel").disabled = False
				End If
				
				.Columns("nSequence").Defvalue = mobjt_move_acc.nSequence
				.Columns("sClient").Defvalue = mobjt_move_acc.sClient
				.Columns("sClient").Descript = mobjt_move_acc.sCliename
				
				.Columns("sClient").Digit = mobjt_move_acc.sDigit
				
				.Columns("nCurrency").Defvalue = mobjt_move_acc.nCurrency
				.Columns("nCurrency").Descript = mobjt_move_acc.sCurrency
				.Columns("nAmount").Defvalue = mobjt_move_acc.nCredit
				.Columns("hddAmount").Defvalue = mobjt_move_acc.nCredit
				.Columns("nOldAmountl").Defvalue = CStr(mobjt_move_acc.nCredit * mobjt_move_acc.nExchange)
				.Columns("tcnAmountLoc").Defvalue = CStr(System.Math.Round(mobjt_move_acc.nCredit * mobjt_move_acc.nExchange, 0))
				.Columns("hddBalance").Defvalue = CStr(mdblTotals)
				
				If mobjt_move_acc.nType_Move > 0 Then
					.Columns("cbeDiferenceTyp").Defvalue = mobjt_move_acc.nType_Move
				End If
				
				mobjGrid.sEditRecordParam = "nTotalRel=" & mobjValues.TypeToString(mdblTotals, eFunctions.Values.eTypeData.etdDouble, True, 2) & "&nAmount=" & mobjt_move_acc.nCredit
				
				Response.Write(.DoRow)
			Next mobjt_move_acc
		End With
	Else
		mobjGrid.sEditRecordParam = "nTotalRel=" & mobjValues.TypeToString(mdblTotals, eFunctions.Values.eTypeData.etdDouble, True, 6) & "&nAmount=" & mobjValues.TypeToString(mdblTotals, eFunctions.Values.eTypeData.etdDouble, True, 6)
	End If
	
	Response.Write(mobjGrid.closeTable)
	
	Response.Write("<SCRIPT>")
        Response.Write("top.frames['fraHeader'].UpdateDiv('lblTotCobDev','" & mobjValues.TypeToString(mdblTotalAmount, eFunctions.Values.eTypeData.etdDouble, True, 0) & "');")
        Response.Write("top.frames['fraHeader'].UpdateDiv('lblTotIn','" & mobjValues.TypeToString(mdblPaidAmount, eFunctions.Values.eTypeData.etdDouble, True, 0) & "');")
        Response.Write("top.frames['fraHeader'].UpdateDiv('lblTotSaldo','" & mobjValues.TypeToString(mdblTotalAmountGen, eFunctions.Values.eTypeData.etdDouble, True, 0) & "');")
	Response.Write("</" & "Script>")
	
	mcolt_move_acc = Nothing
	
End Sub

'% insPreCO012Upd: Carga los datos de la PopUp
'---------------------------------------------------------------------------------------------------------
Private Sub insPreCO012Upd()
	'---------------------------------------------------------------------------------------------------------
	If Request.QueryString.Item("Action") = "Del" Then
		insDelItem()
	End If
	If Request.QueryString.Item("Action") = "Update" Then
		If mobjValues.StringToType(Request.QueryString.Item("nAmount"), eFunctions.Values.eTypeData.etdDouble) <= 0 Then
			Response.Write("<SCRIPT> insExchange('" & Request.QueryString.Item("nTotalRel") & "') </" & "Script>")
		End If
	End If
	Response.Write(mobjGrid.DoFormUpd(Request.QueryString.Item("Action"), "ValCollectionSeq.aspx", "CO012", Request.QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(Request.QueryString.Item("Index"))))
End Sub

'% insDelItem: Elimina la información seleccionada.
'------------------------------------------------------------------------
Private Sub insDelItem()
	'------------------------------------------------------------------------    
	Dim lobjt_Premium As eCollection.T_Move_acc
	lobjt_Premium = New eCollection.T_Move_acc
	
	lobjt_Premium.Del(Session("nBordereaux"), mobjValues.StringToType(Request.QueryString.Item("nSequence"), eFunctions.Values.eTypeData.etdInteger))
	Response.Write(mobjValues.ConfirmDelete())
	
	lobjt_Premium = Nothing
End Sub

'% insDefineHeader: Se define la estructura del grid de la transacción.
'------------------------------------------------------------------------
Private Sub insDefineHeader()
	'------------------------------------------------------------------------
	Dim lobjColumn As eFunctions.Column
	mobjGrid = New eFunctions.Grid
	'^Begin Body Block VisualTimer Utility 1.1 3/4/03 12.00.00
	mobjGrid.sSessionID = Session.SessionID
	mobjGrid.nUsercode = Session("nUsercode")
	'~End Body Block VisualTimer Utility
	
	mobjGrid.sCodisplPage = "CO012"
	Call mobjGrid.SetWindowParameters(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), Request.QueryString.Item("nWindowTy"))
	
	'+ Se definen las propiedades generales del grid
	mobjGrid.ActionQuery = CStr(Session("CO001_nAction")) = CStr(eCollection.ColformRef.TypeActionsSeqColl.cstrQuery)
	
	With mobjGrid.Columns
		.AddHiddenColumn("nSequence", vbNullString)
		.AddHiddenColumn("nOldAmountl", vbNullString)
		.AddHiddenColumn("hddBalance", CStr(0))
		.AddHiddenColumn("hddAmount", CStr(0))
		
		If CStr(Session("sRelorigi")) = "1" Then
			.AddClientColumn(9999, GetLocalResourceObject("sClientColumnCaption"), "sClient", vbNullString,  , GetLocalResourceObject("sClientColumnToolTip"),  ,  ,  ,  ,  , GetLocalResourceObject("sClientColumnToolTip"))
		Else
			.AddClientColumn(9999, GetLocalResourceObject("sClientColumnCaption"), "sClient", vbNullString,  , GetLocalResourceObject("sClientColumnToolTip"),  ,  ,  ,  ,  , CStr(True))
		End If
		.AddNumericColumn(9999, GetLocalResourceObject("nAmountColumnCaption"), "nAmount", 18, mobjValues.StringToType(Request.QueryString.Item("nTotalRel"), eFunctions.Values.eTypeData.etdDouble),  ,  , True, 6,  ,  , "insExchange(0);")
		lobjColumn = .AddPossiblesColumn(9999, GetLocalResourceObject("nCurrencyColumnCaption"), "nCurrency", "table11", eFunctions.Values.eValuesType.clngComboType, CStr(1),  ,  ,  ,  , "insExchange(0);", True)
		lobjColumn.TypeList = 2
		lobjColumn.List = mstrTable5008
		.AddNumericColumn(9999, GetLocalResourceObject("tcnExchangeColumnCaption"), "tcnExchange", 6, CStr(1),  ,  , True, 2,  ,  ,  , True)
		.AddNumericColumn(9999, GetLocalResourceObject("tcnAmountLocColumnCaption"), "tcnAmountLoc", 18, CStr(0),  ,  , True, 6,  ,  ,  , True)
		
		If (Session("nCashNum") = 0 Or CStr(Session("nCashNum")) = "") And CStr(Session("CO001_nAction")) = CStr(eCollection.ColformRef.TypeActionsSeqColl.cstrModify) Then
			lobjColumn = .AddPossiblesColumn(0, GetLocalResourceObject("cbeDiferenceTypColumnCaption"), "cbeDiferenceTyp", "table401", 1, CStr(65),  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeDiferenceTypColumnCaption"))
		Else
			lobjColumn = .AddPossiblesColumn(0, GetLocalResourceObject("cbeDiferenceTypColumnCaption"), "cbeDiferenceTyp", "table401", 1, CStr(18),  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeDiferenceTypColumnCaption"))
		End If
		lobjColumn.TypeList = CShort("1")
		lobjColumn.List = "18,65"
		
	End With
	
	With mobjGrid
		.sDelRecordParam = "nSequence=' + marrArray[lintIndex].nSequence + '"
		.Height = 350
		.Width = 350
		.Top = 180
		.Codispl = "CO012"
		
		If CStr(Session("sRelorigi")) = "1" Then
			.AddButton = False
			.DeleteButton = True
		Else
			.AddButton = True
			.DeleteButton = True
		End If
		
		.Columns("sClient").EditRecord = True
		
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
	
	If CStr(Session("CO001_nAction")) = CStr(eCollection.ColformRef.TypeActionsSeqColl.cstrQuery) Then
		mobjGrid.Columns("Sel").GridVisible = False
	Else
		mobjGrid.Columns("Sel").GridVisible = True
	End If
End Sub

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("CO012")

mcolt_move_acc = New eCollection.t_move_accs

%>
<HTML>
    <META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Claim.js"></SCRIPT>



	


<%
mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 3/4/03 12.00.00
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "CO012"

Response.Write(mobjValues.StyleSheet())
Response.Write(mobjValues.ShowWindowsName("CO012", Request.QueryString.Item("sWindowDescript")))

mobjMenues = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 3/4/03 12.00.00
mobjMenues.sSessionID = Session.SessionID
mobjMenues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write(mobjMenues.setZone(2, "CO012", Request.QueryString.Item("sWindowDescript"), CShort(Request.QueryString.Item("nWindowTy"))))
End If
%>    
<SCRIPT>
	var nMainAction = 304;
	//var nIndex = <%=Request.QueryString.Item("Index")%>;
	var sDate  = '<%=mobjValues.TypeToString(Session("dCollectDate"), eFunctions.Values.eTypeData.etdDate)%>';
	
//+ Variable para el control de versiones
		document.VssVersion="$$Revision: 4 $|$$Date: 29/03/04 20:21 $|$$Author: Nvaplat7 $"
		
//+ Variable para el control de versiones
//---------------------------------------------------------------------------------------------------------
function insExchange(values){
//---------------------------------------------------------------------------------------------------------z
	if (values==0)
		insDefValues('LocalAmount','nCurrency=' + document.forms[0].nCurrency.value + '&dValDate=' + sDate + '&sType=Normal&nAmount=' + document.forms[0].nAmount.value);
	else {	
		insDefValues('LocalAmount','nCurrency=1&sType=Normal&dValDate=' + sDate + '&nAmount=' + values);
	}
}
</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD=post ACTION="valCollectionSeq.aspx?Time=1" ID=form1 NAME=form1>
<%
insPrevInf()
insDefineHeader()

If Request.QueryString.Item("Type") <> "PopUp" Then
	insPreCO012()
Else
	insPreCO012Upd()
End If

mobjGrid = Nothing
%>
</FORM>
</BODY>
</HTML>
<%'^Begin Footer Block VisualTimer Utility 1.1 3/4/03 12.00.00
Call mobjNetFrameWork.FinishPage("CO012")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




