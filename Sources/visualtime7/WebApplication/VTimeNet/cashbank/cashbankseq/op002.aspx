<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eGeneral" %>
<%@ Import namespace="eCashBank" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
Dim mobjGrid As eFunctions.Grid
Dim llngAction As Object
Dim mobjMenu As eFunctions.Menues
Dim mobjGeneral As eGeneral.GeneralFunction
Dim lstrError As String=String.Empty
Dim mstrQueryString As String


'%insDefineHeader. Definición de columnas del GRID
'----------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'----------------------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	
	mobjGrid.sCodisplPage = "OP002"
	
	With mobjGrid.Columns
		Select Case Request.QueryString.Item("nOptDeposit")
			Case "2", "4", "5"
				Call .AddTextColumn(40204, GetLocalResourceObject("sChequeColumnCaption"), GetLocalResourceObject("sChequeColumnToolTip"), 12, "",  , GetLocalResourceObject("sChequeColumnToolTip"),  ,  ,  , True)
				Call .AddPossiblesColumn(40205, GetLocalResourceObject("cbeBankColumnCaption"), "cbeBank", "Table7", eFunctions.Values.eValuesType.clngComboType)
				Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeChequeLocatColumnCaption"), "cbeChequeLocat", "Table5553", eFunctions.Values.eValuesType.clngComboType)
			Case "3"
				Call .AddTextColumn(40207, GetLocalResourceObject("sVoucherColumnCaption"), GetLocalResourceObject("sVoucherColumnToolTip"), 15, "",  , GetLocalResourceObject("sVoucherColumnToolTip"),  ,  ,  , True)
				Call .AddPossiblesColumn(40208, GetLocalResourceObject("cbeCardColumnCaption"), "cbeCard", "Table183", eFunctions.Values.eValuesType.clngComboType)
		End Select
		Call .AddDateColumn(40209, GetLocalResourceObject("dDateColumnCaption"), GetLocalResourceObject("dDateColumnToolTip"),  ,  , GetLocalResourceObject("dDateColumnToolTip"),  ,  ,  , True)
		Call .AddNumericColumn(40203, GetLocalResourceObject("nAmountColumnCaption"), GetLocalResourceObject("nAmountColumnToolTip"), 19, CStr(0),  , GetLocalResourceObject("nAmountColumnToolTip"), True, 6,  ,  ,  , True)

        If Request.QueryString.Item("nOptSelection") = 2 Then
            Call .AddNumericColumn(0, GetLocalResourceObject("tcnBranchColumnCaption"), "tcnBranch", 5, CStr(eRemoteDB.Constants.intNull), , GetLocalResourceObject("tcnBranchColumnToolTip"))
            Call .AddNumericColumn(0, GetLocalResourceObject("tcnProductColumnCaption"), "tcnProduct", 5, CStr(eRemoteDB.Constants.intNull), , GetLocalResourceObject("tcnProductColumnToolTip"))
            Call .AddNumericColumn(0, GetLocalResourceObject("tcnPolicyColumnCaption"), "tcnPolicy", 10, CStr(eRemoteDB.Constants.intNull), , GetLocalResourceObject("tcnPolicyColumnToolTip"))
        End If
            
        Call .AddHiddenColumn("tcnAmount", CStr(0))
		Call .AddHiddenColumn("nAcc_cash", CStr(0))
		Call .AddHiddenColumn("nTransac", CStr(0))
		Call .AddHiddenColumn("dEffecdate", "")
		Call .AddHiddenColumn("hddMov_type", vbNullString)
        Call .AddHiddenColumn("hddSel", "0")
        Call .AddHiddenColumn("hddCashnum", "0")
        Call .AddHiddenColumn("hddOffice", "0")
	End With
	With mobjGrid
		.Codispl = "OP002"
		.AddButton = False
		.DeleteButton = False
		
		If Request.QueryString.Item("nOptDeposit") = "5" Then
			.Columns("Sel").OnClick = "insValidate(this,marrArray[this.value].dEffecdate);"
		Else
			.Columns("Sel").OnClick = "insCalTotAmount(this)"
		End If
		.Columns("Sel").OnClick = .Columns("Sel").OnClick & ";insUpdateSelection(this);"
		.Columns("Sel").GridVisible = llngAction = eFunctions.Menues.TypeActions.clngActionAdd
	End With
End Sub

'%insDefineHeader_Effec. Definición de columnas del GRID para depositos en efectivo
'----------------------------------------------------------------------------------------------
Private Sub insDefineHeader_Effec()
	'----------------------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	
	mobjGrid.sCodisplPage = "op002"
	
	With mobjGrid.Columns
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnCash_IdColumnCaption"), "tcnCash_Id", 5, CStr(eRemoteDB.Constants.intNull),  , GetLocalResourceObject("tcnCash_IdColumnToolTip"))
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnAmountColumnCaption"), "tcnAmount", 18, CStr(eRemoteDB.Constants.intNull),  , GetLocalResourceObject("tcnAmountColumnToolTip"), True, 6)
		Call .AddDateColumn(0, GetLocalResourceObject("tcdEffecdateColumnCaption"), "tcdEffecdate",  ,  , GetLocalResourceObject("tcdEffecdateColumnToolTip"))
		Call .AddDateColumn(0, GetLocalResourceObject("tcdCompdateColumnCaption"), "tcdCompdate",  ,  , GetLocalResourceObject("tcdCompdateColumnToolTip"))
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnOri_AmountColumnCaption"), "tcnOri_Amount", 18, CStr(eRemoteDB.Constants.intNull),  , GetLocalResourceObject("tcnOri_AmountColumnToolTip"), True, 6)
		Call .AddTextColumn(0, GetLocalResourceObject("tctDes_Ori_CurrColumnCaption"), "tctDes_Ori_Curr", 30, "",  , GetLocalResourceObject("tctDes_Ori_CurrColumnToolTip"))
        
        'If Request.QueryString.Item("nOptSelection") = 2 Then
        Call .AddNumericColumn(0, GetLocalResourceObject("tcnBranchColumnCaption"), "tcnBranch", 5, CStr(eRemoteDB.Constants.intNull),  , GetLocalResourceObject("tcnBranchColumnToolTip"))
        Call .AddNumericColumn(0, GetLocalResourceObject("tcnProductColumnCaption"), "tcnProduct", 5, CStr(eRemoteDB.Constants.intNull),  , GetLocalResourceObject("tcnProductColumnToolTip"))
        Call .AddNumericColumn(0, GetLocalResourceObject("tcnPolicyColumnCaption"), "tcnPolicy", 10, CStr(eRemoteDB.Constants.intNull),  , GetLocalResourceObject("tcnPolicyColumnToolTip"))
        'End If
        
        Call .AddHiddenColumn("hddCashnum", "0")
        Call .AddHiddenColumn("hddSel", "0")
        Call .AddHiddenColumn("nTransac", CStr(0))
        Call .AddHiddenColumn("hddEffecdate", "")
        Call .AddHiddenColumn("tcnAmount2", CStr(0))
        Call .AddHiddenColumn("hddOffice", "0")
    End With
	
	With mobjGrid
		.Codispl = "OP002"
		.AddButton = False
		.DeleteButton = False

        .Columns("Sel").OnClick = "insCalTotAmount2(this)"
        .Columns("Sel").OnClick = .Columns("Sel").OnClick & ";insUpdateSelection(this);"
        .Columns("Sel").GridVisible = llngAction = eFunctions.Menues.TypeActions.clngActionadd
    End With
End Sub

'%insPreOP002: Esta función se encarga de cargar los datos en la forma "Folder" 
'----------------------------------------------------------------------------------------------
Private Sub insPreOP002()
	'----------------------------------------------------------------------------------------------
	Dim nOptDeposit As Object
	Dim dEffecDate As String
	Dim sDeposit As String
	Dim nAccCash As String
	Dim lobjCash_movs As eCashBank.Cash_movs
	Dim lintCount As Integer
	Dim ldblCheq_total As Object
	Dim ldblTotal As Object
	Dim ldblCash_total As Object
	Dim ldblMinAmount As Object
	Dim nCompany As String
	Dim nCash As String
	Dim nChequeLocat As String
    Dim sIntermed As String
	
	nOptDeposit = Request.QueryString.Item("nOptDeposit")
	dEffecDate = Request.QueryString.Item("dEffecDate")
	sDeposit = Request.QueryString.Item("sDeposit")
	nAccCash = Request.QueryString.Item("nAccCash")
	nCompany = Request.QueryString.Item("nCompany")
	nCash = Request.QueryString.Item("nCashNum")
	nChequeLocat = Request.QueryString.Item("nChequeLocat")
    sIntermed = Request.QueryString.Item("nIntermed")

	ldblCheq_total = 0
	ldblTotal = 0
	ldblCash_total = 0
	
	lobjCash_movs = New eCashBank.Cash_movs
	
	If lobjCash_movs.insPreOP002(mobjValues.StringToType(llngAction, eFunctions.Values.eTypeData.etdDouble), _
                                 mobjValues.StringToType(nOptDeposit, eFunctions.Values.eTypeData.etdDouble), _
                                 mobjValues.StringToType(nAccCash, eFunctions.Values.eTypeData.etdDouble), _
                                 mobjValues.StringToType(dEffecDate, eFunctions.Values.eTypeData.etdDate), _
                                 sDeposit, _
                                 mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), _
                                 mobjValues.StringToType(Session("nOffice"), eFunctions.Values.eTypeData.etdDouble), _
                                 mobjValues.StringToType(nCash, eFunctions.Values.eTypeData.etdDouble), _
                                 mobjValues.StringToType(nCompany, eFunctions.Values.eTypeData.etdDouble), _
                                 mobjValues.StringToType(nChequeLocat, eFunctions.Values.eTypeData.etdDouble), _
                                 mobjValues.StringToType(sIntermed, eFunctions.Values.eTypeData.etdDouble)) Then

		Select Case nOptDeposit
			Case "1"

                For lintCount = 1 To lobjCash_movs.Count
					If lintCount = 1 Then
Response.Write("" & vbCrLf)
Response.Write("							<TABLE>" & vbCrLf)
Response.Write("								<TR>" & vbCrLf)
Response.Write("					   ")

					End If
					
					With mobjGrid
                            .Columns("Sel").Checked = 1
						.Columns("tcnCash_Id").DefValue = CStr(lobjCash_movs.Item(lintCount).nCash_Id)
						.Columns("tcnAmount").DefValue = CStr(lobjCash_movs.Item(lintCount).nAmount)
						.Columns("tcdEffecdate").DefValue = CStr(lobjCash_movs.Item(lintCount).dEffecDate)
						.Columns("tcdCompdate").DefValue = CStr(lobjCash_movs.Item(lintCount).dCompdate)
						.Columns("tcnori_Amount").DefValue = CStr(lobjCash_movs.Item(lintCount).nori_Amount)
						.Columns("tctDes_Ori_Curr").DefValue = lobjCash_movs.Item(lintCount).sDes_Ori_Curr
                        .Columns("hddCashnum").DefValue = lobjCash_movs.Item(lintCount).nCashNum
                        .Columns("nTransac").DefValue = CStr(lobjCash_movs.Item(lintCount).nTransac)
                        .Columns("hddSel").DefValue = 1
                        .Columns("hddEffecdate").DefValue = lobjCash_movs.Item(lintCount).dEffecDate
                        .Columns("tcnAmount2").DefValue = CStr(lobjCash_movs.Item(lintCount).nAmount)
                        .Columns("hddOffice").DefValue = lobjCash_movs.Item(lintCount).nOffice
                            
                        'If Request.QueryString.Item("nOptSelection") = 2 Then
                        .Columns("tcnBranch").DefValue = lobjCash_movs.Item(lintCount).nBranch
                        .Columns("tcnProduct").DefValue = lobjCash_movs.Item(lintCount).nProduct
                        .Columns("tcnPolicy").DefValue = lobjCash_movs.Item(lintCount).nPolicy
                        'End If

                        ldblCash_total = ldblCash_total + lobjCash_movs.Item(lintCount).nAmount
                        ldblTotal = lobjCash_movs.Item(lintCount).nAmount + ldblTotal
                        Response.Write(mobjGrid.DoRow())
					End With
				Next 
				Response.Write("</DIV>")
				Response.Write("</TD>")
				Response.Write("</TR>")
				Response.Write("</TABLE>")
                
                ldblMinAmount = lobjCash_movs.nMin_Amount
				If lobjCash_movs.Count > 0 Then
Response.Write("" & vbCrLf)
Response.Write("						<TABLE ALIGN=""CENTER"">" & vbCrLf)
Response.Write("							<TR><TD ALIGN=""CENTER"" COLSPAN=2><P>" & vbCrLf)
Response.Write("							<TR>" & vbCrLf)
Response.Write("								<TD COLSPAN=""4"" CLASS=""HighLighted""><LABEL ID=40202><A NAME=""Total"">" & GetLocalResourceObject("AnchorTotalCaption") & "</A></LABEL></TD>" & vbCrLf)
Response.Write("							</TR>" & vbCrLf)
Response.Write("							<TR>" & vbCrLf)
Response.Write("								<TD WIDTH=""100%"" COLSPAN=""4"" CLASS=""Horline""></TD>" & vbCrLf)
Response.Write("							</TR>" & vbCrLf)
Response.Write("							<TR>" & vbCrLf)
Response.Write("								<TD>" & vbCrLf)
Response.Write("									<LABEL ID=8994>" & GetLocalResourceObject("lblTotDepositCaption") & "</LABEL>" & vbCrLf)
Response.Write("									")


Response.Write(mobjValues.NumericControl("lblTotDeposit", 19, ldblCash_total,  , "", True, 6,  ,  ,  ,  , True))


Response.Write("" & vbCrLf)
Response.Write("									")

					Response.Write(mobjValues.HiddenControl("nAvailable", ldblTotal))
Response.Write("" & vbCrLf)
Response.Write("									")

					Response.Write(mobjValues.HiddenControl("nMinAmount", ldblMinAmount))
Response.Write("" & vbCrLf)
Response.Write("								</TD>" & vbCrLf)
Response.Write("								<TD>")


                        Response.Write(mobjValues.CheckControl("chkPrint", GetLocalResourceObject("chkPrintCaption"), "1", , , False))


Response.Write("</TD>" & vbCrLf)
Response.Write("							</TR>" & vbCrLf)
Response.Write("					")

				End If
			Case "2", "4", "5"
				
				For lintCount = 1 To lobjCash_movs.Count
					If lintCount = 1 Then
Response.Write("" & vbCrLf)
Response.Write("							<TABLE>" & vbCrLf)
Response.Write("								<TR>" & vbCrLf)
Response.Write("					   ")

					End If
					With mobjGrid
						If ((llngAction = eFunctions.Menues.TypeActions.clngActionQuery Or llngAction = eFunctions.Menues.TypeActions.clngActioncut) And lobjCash_movs.Item(lintCount).nMov_type <> 7) Or llngAction = eFunctions.Menues.TypeActions.clngActionAdd Then
							.Columns("sCheque").DefValue = lobjCash_movs.Item(lintCount).sDocnumbe
							.Columns("cbeBank").DefValue = CStr(lobjCash_movs.Item(lintCount).nBank_code)
							.Columns("dDate").DefValue = CStr(lobjCash_movs.Item(lintCount).dDoc_date)
							.Columns("cbeChequeLocat").DefValue = CStr(lobjCash_movs.Item(lintCount).nChequeLocat)
							.Columns("nAmount").DefValue = CStr(lobjCash_movs.Item(lintCount).nAmount)
							.Columns("tcnAmount").DefValue = CStr(lobjCash_movs.Item(lintCount).nAmount)
							.Columns("nAcc_Cash").DefValue = CStr(lobjCash_movs.Item(lintCount).nAcc_cash)
							.Columns("nTransac").DefValue = CStr(lobjCash_movs.Item(lintCount).nTransac)
							.Columns("dEffecdate").DefValue = CStr(lobjCash_movs.Item(lintCount).dEffecDate)
                            .Columns("hddMov_type").DefValue = CStr(lobjCash_movs.Item(lintCount).nMov_type)
                            .Columns("hddCashnum").DefValue = lobjCash_movs.Item(lintCount).nCashNum
                            .Columns("Sel").Checked = 1
                            .Columns("hddSel").DefValue = 1
                            .Columns("hddOffice").DefValue = lobjCash_movs.Item(lintCount).nOffice
                                
                            If Request.QueryString.Item("nOptSelection") = 2 Then
                                .Columns("tcnBranch").DefValue = lobjCash_movs.Item(lintCount).nBranch
                                .Columns("tcnProduct").DefValue = lobjCash_movs.Item(lintCount).nProduct
                                .Columns("tcnPolicy").DefValue = lobjCash_movs.Item(lintCount).nPolicy
                            End If
                                
						End If

						ldblCheq_total = lobjCash_movs.Item(lintCount).nAmount + ldblCheq_total
						ldblTotal = lobjCash_movs.Item(lintCount).nAmount + ldblTotal
						
						Response.Write(mobjGrid.DoRow())
					End With
				Next 
				Response.Write("</DIV>")
				Response.Write("</TD>")
				Response.Write("</TR>")
				Response.Write("</TABLE>")
Response.Write("	" & vbCrLf)
Response.Write("					<TABLE ALIGN=""CENTER"">" & vbCrLf)
Response.Write("						<TR><td ALIGN=""CENTER"" COLSPAN=2>						  " & vbCrLf)
Response.Write("							<P>				" & vbCrLf)
Response.Write("					<TR>" & vbCrLf)
Response.Write("						<TD COLSPAN=""4"" CLASS=""HighLighted""><LABEL ID=40202><A NAME=""Total"">" & GetLocalResourceObject("AnchorTotalCaption") & "</A></LABEL></TD>" & vbCrLf)
Response.Write("					</TR>" & vbCrLf)
Response.Write("					<TR>" & vbCrLf)
Response.Write("						<TD WIDTH=""100%"" COLSPAN=""4"" CLASS=""Horline""></TD>" & vbCrLf)
Response.Write("					</TR>" & vbCrLf)
Response.Write("					<TR>						" & vbCrLf)
Response.Write("						<TD><LABEL ID=0>" & GetLocalResourceObject("lblTotDepositCaption") & " </LABEL>")


Response.Write(mobjValues.NumericControl("lblTotDeposit", 19, ldblCheq_total,  , "", True, 6,  ,  ,  ,  , True))


Response.Write("</TD>					" & vbCrLf)
Response.Write("						<TD>")


                    Response.Write(mobjValues.CheckControl("chkPrint", GetLocalResourceObject("chkPrintCaption"), "1", , , False))


Response.Write("</TD>																			" & vbCrLf)
Response.Write("					</TR>" & vbCrLf)
Response.Write("			  ")

			Case "3"
				
				For lintCount = 1 To lobjCash_movs.Count
					If lintCount = 1 Then
Response.Write("" & vbCrLf)
Response.Write("							<TABLE>" & vbCrLf)
Response.Write("								<TR>" & vbCrLf)
Response.Write("									<TD ALIGN=""CENTER"" COLSPAN=2><DIV ID=""Scroll"" style=""width:600;height:230;overflow:auto; outset gray"">" & vbCrLf)
Response.Write("					   ")

					End If
					With mobjGrid
						If ((llngAction = eFunctions.Menues.TypeActions.clngActionQuery Or llngAction = eFunctions.Menues.TypeActions.clngActioncut) And lobjCash_movs.Item(lintCount).nMov_type <> 7) Or llngAction = eFunctions.Menues.TypeActions.clngActionAdd Then
							.Columns("sVoucher").DefValue = lobjCash_movs.Item(lintCount).sDocnumbe
							.Columns("cbeCard").DefValue = CStr(lobjCash_movs.Item(lintCount).nCard_typ)
							.Columns("dDate").DefValue = CStr(lobjCash_movs.Item(lintCount).dDoc_date)
							.Columns("nAmount").DefValue = CStr(lobjCash_movs.Item(lintCount).nAmount)
							.Columns("tcnAmount").DefValue = CStr(lobjCash_movs.Item(lintCount).nAmount)
							.Columns("nAcc_Cash").DefValue = CStr(lobjCash_movs.Item(lintCount).nAcc_cash)
							.Columns("nTransac").DefValue = CStr(lobjCash_movs.Item(lintCount).nTransac)
                            .Columns("dEffecdate").DefValue = CStr(lobjCash_movs.Item(lintCount).dEffecdate)
                            .Columns("hddCashnum").DefValue = lobjCash_movs.Item(lintCount).nCashNum
                            .Columns("hddOffice").DefValue = lobjCash_movs.Item(lintCount).nOffice
						End If
						If llngAction = eFunctions.Menues.TypeActions.clngActionQuery Or llngAction = eFunctions.Menues.TypeActions.clngActioncut Then
							ldblCheq_total = lobjCash_movs.Item(lintCount).nAmount + ldblCheq_total
							ldblTotal = lobjCash_movs.Item(lintCount).nAmount + ldblTotal
						End If
						Response.Write(mobjGrid.DoRow())
					End With
				Next 
				Response.Write("</DIV>")
				Response.Write("</TD>")
				Response.Write("</TR>")
				Response.Write("</TABLE>")
Response.Write("" & vbCrLf)
Response.Write("					<TABLE>" & vbCrLf)
Response.Write("						<TR><td ALIGN=""CENTER"" COLSPAN=2>						  " & vbCrLf)
Response.Write("							<P>				" & vbCrLf)
Response.Write("					<TR>" & vbCrLf)
Response.Write("					<TR>" & vbCrLf)
Response.Write("						<TD COLSPAN=""4"" CLASS=""HighLighted""><LABEL ID=40202><A NAME=""Total"">" & GetLocalResourceObject("AnchorTotalCaption") & "</A></LABEL></TD>" & vbCrLf)
Response.Write("					</TR>" & vbCrLf)
Response.Write("					<TR>" & vbCrLf)
Response.Write("						<TD WIDTH=""100%"" COLSPAN=""4"" CLASS=""Horline""></TD>" & vbCrLf)
Response.Write("					</TR>" & vbCrLf)
Response.Write("					<TR>						" & vbCrLf)
Response.Write("						<TD>" & vbCrLf)
Response.Write("							<LABEL ID=0>" & GetLocalResourceObject("lblTotDepositCaption") & " </LABEL>")


Response.Write(mobjValues.NumericControl("lblTotDeposit", 19, ldblCheq_total,  , "", True, 6,  ,  ,  ,  , True))


Response.Write("</TD>													" & vbCrLf)
Response.Write("						</TD>" & vbCrLf)
Response.Write("					</TR>	" & vbCrLf)
Response.Write("	      ")

		End Select
		
		If Request.QueryString.Item("Reload") = "1" Then
			'+ Se recarga la ventana PopUp, en caso que el check de "Continuar" se encuentre marcado
			Select Case Request.QueryString.Item("ReloadAction")
				Case "Add"
					Response.Write("<SCRIPT>EditRecord(-1,nMainAction,'Add')</" & "Script>")
				Case "Update"
					Response.Write("<SCRIPT>EditRecord(" & Request.QueryString.Item("ReloadIndex") & ",nMainAction,'Update')</" & "Script>")
			End Select
		End If
	End If
	Response.Write(mobjGrid.CloseTable())
	Response.Write(mobjValues.BeginPageButton)
	lobjCash_movs = Nothing
End Sub

</script>
<%Response.Expires = -1

llngAction = Request.QueryString.Item("nMainAction")
mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues

mobjValues.sCodisplPage = "op002"

mobjGeneral = New eGeneral.GeneralFunction
lstrError = "Err. 60475  " & mobjGeneral.insLoadMessage(60475)
mobjGeneral = Nothing

If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write(mobjMenu.setZone(2, "OP002", "OP002.aspx", eFunctions.Menues.TypeForm.clngFraRepetitive))
End If

mstrQueryString = "nMainAction=" & Request.QueryString.Item("nMainAction") & "&nOptDeposit=" & Request.QueryString.Item("nOptDeposit") & "&dEffecDate=" & Request.QueryString.Item("dEffecDate") & "&dRealEffecDate=" & Request.QueryString.Item("dRealEffecDate") & "&sDeposit=" & Request.QueryString.Item("sDeposit") & "&nAccCash=" & Request.QueryString.Item("nAccCash") & "&nCompany=" & Request.QueryString.Item("nCompany") & "&nCashNum=" & Request.QueryString.Item("nCashNum") & "&sLinkSpecial=" & Request.QueryString.Item("sLinkSpecial") & "&nChequeLocat=" & Request.QueryString.Item("nChequeLocat")  & "&nIntermed=" & Request.QueryString.Item("nIntermed")


%>
<HTML>
    <%="<SCRIPT>nMainAction=" & Request.QueryString.Item("nMainAction") & "</SCRIPT>"%>
<HEAD>
<SCRIPT>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 7 $|$$Date: 15/01/04 19:38 $|$$Author: Nvaplat11 $"
</SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
	<SCRIPT>
	

//---------------------------------------------------------------------------------------------------------
function insUpdateSelection(lobj){
//---------------------------------------------------------------------------------------------------------

	if(mintArrayCount>0)
	{

	    if(lobj.checked==false)
		{
		    self.document.forms[0].hddSel[lobj.value].value = "0";
//		    self.document.forms[0].Sel[lobj.value].value = "0";		    
		      self.document.forms[0].Sel.checked = false;		    		    		    
		}    
		else
		{
		    self.document.forms[0].hddSel[lobj.value].value = "1";
//	        self.document.forms[0].Sel[lobj.value].value = "1";		
		      self.document.forms[0].Sel.checked = true;		    	            
	    }    
	}
	else
	{

		if(lobj.checked==false)
		{
		      self.document.forms[0].hddSel.value = "0";
		      self.document.forms[0].Sel.checked = false;		    		    
		}	    
		else
		{
		      self.document.forms[0].hddSel.value = "1";
		      self.document.forms[0].Sel.checked = true;		    
		}    
	}
}

//%insValidate: Si la operación es redepósito valida que no sea el mismo día
//---------------------------------------------------------------------------------------------------------------------
function insValidate(Field,Effecdate){
//---------------------------------------------------------------------------------------------------------------------
	if (Effecdate== top.fraHeader.document.forms[0].tcdEffecDate.value){
		alert('<%=lstrError%>');
		Field.checked = false;
		}
	else
		{			
		insCalTotAmount(Field);		
		}
	}
//%insCalTotAmount: Calcula el total del depósito	
//---------------------------------------------------------------------------------------------------------------------
function insCalTotAmount(Field){
//---------------------------------------------------------------------------------------------------------------------
		    with (self.document.forms[0]){
				if (tcnAmount.length > 1){
			        if (Field.checked){
						lblTotDeposit.value = VTFormat(insConvertNumber(lblTotDeposit.value) + insConvertNumber(tcnAmount[Field.value].value) ,'', '', '', 6, true)
						if (typeof(lblTotOther) != 'undefined'){
							lblTotOther.value = VTFormat(insConvertNumber(lblTotOther.value) + insConvertNumber(tcnAmount[Field.value].value) ,'', '', '', 6, true)
						}
					}
					else {
						lblTotDeposit.value = VTFormat(insConvertNumber(lblTotDeposit.value) - insConvertNumber(tcnAmount[Field.value].value) ,'', '', '', 6, true)
						if (typeof(lblTotOther) != 'undefined'){
							lblTotOther.value = VTFormat(insConvertNumber(lblTotOther.value) - insConvertNumber(tcnAmount[Field.value].value) ,'', '', '', 6, true)
						}
					}
				}
				else {
			        if (Field.checked){
						lblTotDeposit.value = VTFormat(insConvertNumber(lblTotDeposit.value) + insConvertNumber(tcnAmount.value) ,'', '', '', 6, true)
						if (typeof(lblTotOther) != 'undefined'){
							lblTotOther.value = VTFormat(insConvertNumber(lblTotOther.value) + insConvertNumber(tcnAmount.value) ,'', '', '', 6, true)
						}
					}
					else {
						lblTotDeposit.value = VTFormat(insConvertNumber(lblTotDeposit.value) - insConvertNumber(tcnAmount.value) ,'', '', '', 6, true)
						if (typeof(lblTotOther) != 'undefined'){
							lblTotOther.value = VTFormat(insConvertNumber(lblTotOther.value) - insConvertNumber(tcnAmount.value) ,'', '', '', 6, true)
						}
					}
				}
            }
    }

    //%insCalTotAmount2: Calcula el total del depósito en efectivo
    //---------------------------------------------------------------------------------------------------------------------
    function insCalTotAmount2(Field) {
    //---------------------------------------------------------------------------------------------------------------------
        with (self.document.forms[0]) {
            if (tcnAmount2.length > 1) {
                if (Field.checked) {
                    lblTotDeposit.value = VTFormat(insConvertNumber(lblTotDeposit.value) + insConvertNumber(tcnAmount2[Field.value].value), '', '', '', 6, true)
                    if (typeof (lblTotOther) != 'undefined') {
                        lblTotOther.value = VTFormat(insConvertNumber(lblTotOther.value) + insConvertNumber(tcnAmount2[Field.value].value), '', '', '', 6, true)
                    }
                }
                else {
                    lblTotDeposit.value = VTFormat(insConvertNumber(lblTotDeposit.value) - insConvertNumber(tcnAmount2[Field.value].value), '', '', '', 6, true)
                    if (typeof (lblTotOther) != 'undefined') {
                        lblTotOther.value = VTFormat(insConvertNumber(lblTotOther.value) - insConvertNumber(tcnAmount2[Field.value].value), '', '', '', 6, true)
                    }
                }
            }
            else {
                if (Field.checked) {
                    lblTotDeposit.value = VTFormat(insConvertNumber(lblTotDeposit.value) + insConvertNumber(tcnAmount2.value), '', '', '', 6, true)
                    if (typeof (lblTotOther) != 'undefined') {
                        lblTotOther.value = VTFormat(insConvertNumber(lblTotOther.value) + insConvertNumber(tcnAmount2.value), '', '', '', 6, true)
                    }
                }
                else {
                    lblTotDeposit.value = VTFormat(insConvertNumber(lblTotDeposit.value) - insConvertNumber(tcnAmount2.value), '', '', '', 6, true)
                    if (typeof (lblTotOther) != 'undefined') {
                        lblTotOther.value = VTFormat(insConvertNumber(lblTotOther.value) - insConvertNumber(tcnAmount2.value), '', '', '', 6, true)
                    }
                }
            }
        }
    }	
	</SCRIPT>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">




    <%With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjMenu.setZone(2, "OP002", "OP002.aspx"))
End With
mobjMenu = Nothing
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="frmDeposit" ACTION="ValCashBankSeq.aspx?<%=mstrQueryString%>">
<%
Response.Write(mobjValues.ShowWindowsName("OP002"))
If Request.QueryString.Item("nOptDeposit") = "2" Or Request.QueryString.Item("nOptDeposit") = "3" Or Request.QueryString.Item("nOptDeposit") = "4" Or Request.QueryString.Item("nOptDeposit") = "5" Then
	Call insDefineHeader()
Else
	If Request.QueryString.Item("nOptDeposit") = "1" Then
		Call insDefineHeader_Effec()
	End If
End If

Call insPreOP002()
mobjGrid = Nothing
mobjValues = Nothing
%>
</FORM>
</BODY>
</HTML>
<%If Request.QueryString.Item("sLinkSpecial") = "1" Then
	Response.Write("<SCRIPT>top.fraHeader.document.A391.disabled = true;</SCRIPT>")
End If

%>




