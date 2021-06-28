<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eClient" %>
<%@ Import namespace="eCashBank" %>
<%@ Import namespace="eGeneral" %>
<%@ Import namespace="eAgent" %>
<script language="VB" runat="Server">
'- Objeto para el manejo de las funciones generales de carga de valores	
Dim mobjValues As eFunctions.Values

'- Variable auxiliar para manejo de moneda
    Dim mintCurrency As Integer

'% insShowMovAcc: se utiliza para rescatar el correlativo del movimiento a generar
'--------------------------------------------------------------------------------------------
Sub insShowMovAcc()
	'--------------------------------------------------------------------------------------------
	Dim lobjMove_acc As eCashBank.Move_acc
	Dim lobjClient As eClient.Client
	Dim sClient As String
	
	lobjClient = New eClient.Client
	lobjMove_acc = New eCashBank.Move_acc
	
	mintCurrency = 0
	sClient = lobjClient.ExpandCode(Request.QueryString.Item("sClient"))
	
	If lobjMove_acc.FindLastMove2(mobjValues.StringToType(Request.QueryString.Item("nTypeAcco"), eFunctions.Values.eTypeData.etdDouble), Request.QueryString.Item("sType_acc"), sClient, mobjValues.StringToType(Request.QueryString.Item("nCurrency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("dOperdate"), eFunctions.Values.eTypeData.etdDate)) Then
		Response.Write("top.opener.document.forms[0].gmnTransact.value=" & lobjMove_acc.nIDConsec + 1 & ";")
		Response.Write("top.opener.document.forms[0].gmnTransact.disabled=true;")
	Else
		Response.Write("top.opener.document.forms[0].gmnTransact.value=" & 1 & ";")
	End If
	lobjMove_acc = Nothing
	lobjClient = Nothing
	
End Sub


'% insShowCashnum: se utiliza para rescatar el correlativo del movimiento a generar
'--------------------------------------------------------------------------------------------
Sub insShowCashnum()
	'--------------------------------------------------------------------------------------------  
	
	Dim lobjUser_Cashnum As eCashBank.User_cashnum
	lobjUser_Cashnum = New eCashBank.User_cashnum
	
	If lobjUser_Cashnum.Find(mobjValues.StringToType(Request.QueryString.Item("nCashnum"), eFunctions.Values.eTypeData.etdDouble)) Then
		Response.Write("UpdateDiv('Usercashnum' ,'" & lobjUser_Cashnum.sCliename & "');")
	Else
		Response.Write("UpdateDiv('Usercashnum',' ');")
	End If
	
	lobjUser_Cashnum = Nothing
	
End Sub


'% insShowCashnum_by_Client: se utiliza para rescatar el numero de caja dado un cliente
'--------------------------------------------------------------------------------------------
Sub insShowCashnum_by_Client()
	'--------------------------------------------------------------------------------------------  
	
	Dim lobjUser_Cashnum As eCashBank.User_cashnum
	lobjUser_Cashnum = New eCashBank.User_cashnum
	
	If lobjUser_Cashnum.Find_cashnum_by_Client(Request.QueryString.Item("sClient")) Then
		Response.Write("top.fraFolder.document.forms[0].tcnCashnum.value=" & lobjUser_Cashnum.nCashnum & ";")
	Else
		Response.Write("top.fraFolder.document.forms[0].tcnCashnum.value=" & 0 & ";")
	End If
	
	lobjUser_Cashnum = Nothing
End Sub

'% insShowClient_by_Cashnum: se utiliza para rescatar el cliente dado un numero de caja
'--------------------------------------------------------------------------------------------
Sub insShowClient_by_Cashnum()
	'--------------------------------------------------------------------------------------------  
	Dim lobjUser_Cashnum As eCashBank.User_cashnum
	Dim llngCashnum As Object
	
	lobjUser_Cashnum = New eCashBank.User_cashnum
	
	If lobjUser_Cashnum.FindClient_by_cashnum(mobjValues.StringToType(Request.QueryString.Item("nCashnum"), eFunctions.Values.eTypeData.etdDouble)) Then
		Response.Write("top.fraFolder.document.forms[0].tctClientCode.value='" & lobjUser_Cashnum.sClient & "';")
		Response.Write("top.fraFolder.document.forms[0].tctClientCode_Digit.value='" & lobjUser_Cashnum.sDigit & "';")
		Response.Write("top.fraFolder.UpdateDiv('tctClientCode_Name' ,'" & lobjUser_Cashnum.sCliename & "');")
	Else
		Response.Write("top.fraFolder.document.forms[0].tctClientCode.value='';")
		Response.Write("top.fraFolder.document.forms[0].tctClientCode_Digit.value='';")
		Response.Write("top.fraFolder.UpdateDiv('tctClientCode_Name' ,' ');")
	End If
	
	If lobjUser_Cashnum.Find(mobjValues.StringToType(Request.QueryString.Item("nCashnum"), eFunctions.Values.eTypeData.etdDouble)) Then
		Response.Write("top.fraFolder.document.forms[0].cbeOfficeAgen.value='" & lobjUser_Cashnum.nOfficeAgen & "';")
		Response.Write("top.fraFolder.document.forms[0].cbeOfficeAgen.disabled=true;")
	Else
		Response.Write("top.fraFolder.document.forms[0].cbeOfficeAgen.value='';")
		Response.Write("top.fraFolder.document.forms[0].cbeOfficeAgen.disabled=false;")
	End If
	
	lobjUser_Cashnum = Nothing
End Sub


'% insShowCurrAcc: se muestran los datos asociados al número de póliza.
'%                Se utiliza para el campo Tipo de negocio de la página OP092_K.aspx
'--------------------------------------------------------------------------------------------
Sub insShowCurrAcc()
	'--------------------------------------------------------------------------------------------
	Dim lobjCurr_acc As eCashBank.Curr_acc
	Dim lobjClient As eClient.Client
	Dim sClient As String
	Dim lobjMove_acc As eCashBank.Move_acc
	Dim lstrTransactvalue As String
	
	lstrTransactvalue = "top.opener.document.forms[0].gmnTransact.value="
	
	lobjClient = New eClient.Client
	lobjCurr_acc = New eCashBank.Curr_acc
	lobjMove_acc = New eCashBank.Move_acc
	
	mintCurrency = 0
	sClient = lobjClient.ExpandCode(Request.QueryString.Item("sClient"))
	
	If lobjCurr_acc.AccCount(mobjValues.StringToType(Request.QueryString.Item("nTypeAccount"), eFunctions.Values.eTypeData.etdDouble), "0", sClient) Then
		
		If lobjCurr_acc.nCount = 1 Then
			'+ Si la cuenta corriente está definida en una sola moneda y se está trabajando con la transacción
			'+ OPC015 (Reporte Mov. APgo recibidos por la cia de seg.), inmediatamente se busca el 
			'+ saldo de la cuenta en la moneda en la que está definida la cuenta
			mintCurrency = lobjCurr_acc.nCurrency
			If Request.QueryString.Item("sCodispl") <> vbNullString And Request.QueryString.Item("sCodispl") = "OPC015" Then
				Response.Write("top.opener.document.forms[0].cbeCurrency.value=" & lobjCurr_acc.nCurrency & ";" & vbCrLf)
			Else
                    Response.Write("top." & Request.QueryString.Item("sZone") & ".document.forms[0].cbeCurrency.value=" & lobjCurr_acc.nCurrency & ";" & vbCrLf)
                End If
			
			Response.Write("try{" & vbCrLf)
			Response.Write("top.opener.document.forms[0].cbeCurrency.value=" & lobjCurr_acc.nCurrency & ";" & vbCrLf)
                
                
			If CDbl(Request.QueryString.Item("nAction")) = 301 Then
				If IsNothing(Request.QueryString.Item("sBussiType")) Or Request.QueryString.Item("sBussiType") = "0" Then
					If lobjMove_acc.FindLastMove2(mobjValues.StringToType(Request.QueryString.Item("nTypeAccount"), eFunctions.Values.eTypeData.etdDouble), "0", sClient, lobjCurr_acc.nCurrency, mobjValues.StringToType(Request.QueryString.Item("dOperdate"), eFunctions.Values.eTypeData.etdDate)) Then
						lstrTransactvalue = lstrTransactvalue & lobjMove_acc.nIDConsec + 1 & ";" & vbCrLf
						Response.Write(lstrTransactvalue)
					Else
						lstrTransactvalue = lstrTransactvalue & 1 & ";" & vbCrLf
						Response.Write(lstrTransactvalue)
					End If
				Else
					If lobjMove_acc.FindLastMove2(mobjValues.StringToType(Request.QueryString.Item("nTypeAccount"), eFunctions.Values.eTypeData.etdDouble), Request.QueryString.Item("sBussiType"), sClient, lobjCurr_acc.nCurrency, mobjValues.StringToType(Request.QueryString.Item("dOperdate"), eFunctions.Values.eTypeData.etdDate)) Then
						lstrTransactvalue = lstrTransactvalue & lobjMove_acc.nIDConsec + 1 & ";" & vbCrLf
						Response.Write(lstrTransactvalue)
					Else
						lstrTransactvalue = lstrTransactvalue & 1 & ";" & vbCrLf
						Response.Write(lstrTransactvalue)
					End If
				End If
			End If
            
			Response.Write("}catch(x){")
			Response.Write("top." & Request.QueryString.Item("sZone") & ".document.forms[0].cbeCurrency.value=" & lobjCurr_acc.nCurrency & ";" & vbCrLf)
			Response.Write("}")
		
                '+ Si la cuenta corriente está definida en una sola moneda y se está trabajando con la transacción
                '+ OP091 (Remesa de pago), inmediatamente se busca el saldo de la cuenta en la moneda en la que está
                '+ definida la cuenta
                If Request.QueryString.Item("sCodispl") <> vbNullString And Request.QueryString.Item("sCodispl") = "OP091" Then
                    Response.Write("top." & Request.QueryString.Item("sZone") & ".$('#cbeCurrency').change();" & vbCrLf)
                    Call insShowBalance()
                End If
            
            
            End If
		
		
		'+Asignación del campo Moneda
	Else
		Response.Write("top.opener.document.forms[0].cbeCurrency.disabled=false;")
	End If
	
	lobjCurr_acc = Nothing
	lobjClient = Nothing
	
End Sub


'% insShowCurrAccParam: Muestra las monedas para un cliente y tipo de cuenta
'--------------------------------------------------------------------------------------------
Sub insShowCurrAccParam()
	'--------------------------------------------------------------------------------------------
	Dim lobjCurr_acc As eCashBank.Curr_acc
	Dim lobjClient As eClient.Client
	Dim sClient As String
	
	lobjClient = New eClient.Client
	lobjCurr_acc = New eCashBank.Curr_acc
	
	mintCurrency = 0
	sClient = lobjClient.ExpandCode(Request.QueryString.Item("sClient"))
	
	If lobjCurr_acc.AccCount(mobjValues.StringToType(Request.QueryString.Item("nTypeAccount"), eFunctions.Values.eTypeData.etdDouble), "0", sClient) Then
		
		If lobjCurr_acc.nCount = 1 Then
			mintCurrency = lobjCurr_acc.nCurrency
			Response.Write("top.fraHeader.document.forms[0].cbeCurrency.value=" & lobjCurr_acc.nCurrency & ";")
			Response.Write("top.fraHeader.document.forms[0].cbeCurrency.disabled=true;")
			Response.Write("top.fraHeader.$('#cbeCurrency').change();")
			Response.Write("top.fraHeader.document.forms[0].btncbeCurrency.disabled = true;")
		Else
			Response.Write("top.fraHeader.document.forms[0].cbeCurrency.disabled=false;")
		End If
		
		If lobjCurr_acc.findClientCurr_acc(CInt(Request.QueryString.Item("nTypeAccount")), Request.QueryString.Item("sBussiType"), Request.QueryString.Item("sClient"), eRemoteDB.Constants.intNull) Then
			
			If lobjCurr_acc.nBranch <> eRemoteDB.Constants.intNull Then
				Response.Write("top.fraHeader.document.forms[0].cbeBranch.value=" & mobjValues.StringToType(CStr(lobjCurr_acc.nBranch), eFunctions.Values.eTypeData.etdDouble) & ";")
			Else
				Response.Write("top.fraHeader.document.forms[0].cbeBranch.value=0;")
			End If
			
			If lobjCurr_acc.nProduct <> eRemoteDB.Constants.intNull Then
				Response.Write("top.fraHeader.document.forms[0].valProduct.value=" & lobjCurr_acc.nProduct & ";")
			Else
				Response.Write("top.fraHeader.document.forms[0].valProduct.value=0;")
			End If
			
			If lobjCurr_acc.nPolicy <> eRemoteDB.Constants.intNull Then
				Response.Write("top.fraHeader.document.forms[0].tcnPolicy.value=" & lobjCurr_acc.nPolicy & ";")
			Else
				Response.Write("top.fraHeader.document.forms[0].tcnPolicy.value=0;")
			End If
			
			If lobjCurr_acc.nCertIf <> eRemoteDB.Constants.intNull Then
				Response.Write("top.fraHeader.document.forms[0].tcnCertif.value=" & lobjCurr_acc.nCertIf & ";")
			Else
				Response.Write("top.fraHeader.document.forms[0].tcnCertif.value=0;")
			End If
			
			If lobjCurr_acc.nBalance <> eRemoteDB.Constants.intNull Then
				Response.Write("top.fraHeader.document.forms[0].lblBalance.value='" & mobjValues.TypeToString(lobjCurr_acc.nBalance, eFunctions.Values.eTypeData.etdDouble, True, 2) & "'")
			Else
				Response.Write("top.fraHeader.document.forms[0].lblBalance.value=0;")
			End If
			
			If lobjCurr_acc.nDebit > lobjCurr_acc.nCredit Then
				Response.Write("top.fraHeader.document.forms[0].optTypeAmou[0].checked=true;")
				Response.Write("top.fraHeader.document.forms[0].optTypeAmou[1].checked=false;")
			Else
				If lobjCurr_acc.nDebit < lobjCurr_acc.nCredit Then
					Response.Write("top.fraHeader.document.forms[0].optTypeAmou[0].checked=false;")
					Response.Write("top.fraHeader.document.forms[0].optTypeAmou[1].checked=true;")
				End If
			End If
		End If
		
		'+Asignación del campo Moneda
	Else
		Response.Write("top.fraHeader.document.forms[0].cbeCurrency.disabled=false;")
	End If
	
	lobjCurr_acc = Nothing
	lobjClient = Nothing
	
End Sub

'% insShowValuesAccBankCash: se muestran los datos asociados a la cuenta de caja o bancaria
'%                Se utiliza para el campo Cuenta de la página OP004_K.aspx
'--------------------------------------------------------------------------------------------
Sub insShowValuesAccBankCash()
	'-------------------------------------------------------------------------------------------
	
	Dim lobjCash_acc As eCashBank.Cash_acc
	Dim lobjBank_acc As eCashBank.Bank_acc
	
	lobjCash_acc = New eCashBank.Cash_acc
	lobjBank_acc = New eCashBank.Bank_acc
	
	
	If mobjValues.StringToType(Request.QueryString.Item("nAccBankCash"), eFunctions.Values.eTypeData.etdDouble) = 9998 Or mobjValues.StringToType(Request.QueryString.Item("nAccBankCash"), eFunctions.Values.eTypeData.etdDouble) = 9999 Or mobjValues.StringToType(Request.QueryString.Item("nAccBankCash"), eFunctions.Values.eTypeData.etdDouble) = 9996 Or mobjValues.StringToType(Request.QueryString.Item("nAccBankCash"), eFunctions.Values.eTypeData.etdDouble) = 9997 Then
		
		If lobjCash_acc.Find(mobjValues.StringToType(Request.QueryString.Item("nAccBankCash"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nOffice"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nCurrency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nCashNum"), eFunctions.Values.eTypeData.etdDouble), 1) Then
			
                Response.Write("top.fraHeader.document.forms[0].cbeCurrency.value=" & lobjCash_acc.nCurrency & ";")
                Response.Write("top.fraHeader.document.forms[0].cbeStatregt.value='" & lobjCash_acc.sStatregt & "';")
                Response.Write("top.fraHeader.document.forms[0].tcdEffecdate.value='" & mobjValues.DateToString(lobjCash_acc.dEffecdate) & "';")
                Response.Write("top.fraHeader.document.forms[0].cbeOffice.value='" & mobjValues.TypeToString(lobjCash_acc.nOffice, eFunctions.Values.eTypeData.etdDouble) & "';")
                Response.Write("top.fraHeader.document.forms[0].tcnAvailable.value='" & mobjValues.TypeToString(lobjCash_acc.nAvailable, eFunctions.Values.eTypeData.etdDouble, True, 2) & "';")
                Response.Write("top.fraHeader.document.forms[0].valLedCompan.value='" & mobjValues.TypeToString(lobjCash_acc.nLed_compan, eFunctions.Values.eTypeData.etdDouble) & "';")
                Response.Write("top.fraHeader.document.forms[0].valAccLedger.value='" & lobjCash_acc.sAccount & "';")
                Response.Write("top.fraHeader.document.forms[0].valAuxAccount.value='" & lobjCash_acc.sAux_accoun & "';")
                Response.Write("top.fraHeader.document.forms[0].tcnAmountMin.value='" & mobjValues.TypeToString(lobjCash_acc.nMin_Amount, eFunctions.Values.eTypeData.etdDouble, True, 2) & "';")
		End If
	Else
            If lobjBank_acc.Find_O(mobjValues.StringToType(Request.QueryString.Item("nAccBankCash"), eFunctions.Values.eTypeData.etdDouble), True) Then
                Response.Write("top.fraHeader.document.forms[0].cbeCurrency.value=" & lobjBank_acc.nCurrency & ";")
                Response.Write("top.fraHeader.document.forms[0].cbeStatregt.value='" & lobjBank_acc.sStatregt & "';")
                Response.Write("top.fraHeader.document.forms[0].tcdEffecdate.value='" & mobjValues.DateToString(lobjBank_acc.dEffecdate) & "';")
                Response.Write("top.fraHeader.document.forms[0].cbeAccType.value='" & mobjValues.TypeToString(lobjBank_acc.nAcc_type, eFunctions.Values.eTypeData.etdDouble) & "';")
                Response.Write("top.fraHeader.document.forms[0].cbeOffice.value='" & mobjValues.TypeToString(lobjBank_acc.nOffice, eFunctions.Values.eTypeData.etdDouble) & "';")
                Response.Write("top.fraHeader.document.forms[0].tcnAvailable.value=" & mobjValues.TypeToString(lobjBank_acc.nAvailable, eFunctions.Values.eTypeData.etdDouble) & ";")
                Response.Write("top.fraHeader.document.forms[0].tctAccNumber.value='" & lobjBank_acc.sAcc_number & "';")
                Response.Write("top.fraHeader.document.forms[0].cbeBank.value=" & lobjBank_acc.nBank_code & ";")
                Response.Write("top.fraHeader.document.forms[0].cbeCompany.value=" & lobjBank_acc.nCompany & ";")
                '+ Parámetro del campo AGENCIA - ACM - 20/12/2001
                Response.Write("top.fraHeader.document.forms[0].valBk_agency.Parameters.Param1.sValue=" & lobjBank_acc.nBank_code & ";")
                If lobjBank_acc.nBk_agency <> eRemoteDB.Constants.intNull Then
                    Response.Write("top.fraHeader.document.forms[0].valBk_agency.value=" & lobjBank_acc.nBk_agency & ";")
                End If
                Response.Write("top.fraHeader.document.forms[0].cbeAvailType.value='" & mobjValues.TypeToString(lobjBank_acc.nAvail_type, eFunctions.Values.eTypeData.etdDouble) & "';")
                Response.Write("top.fraHeader.document.forms[0].tcnTransit1.value='" & mobjValues.TypeToString(lobjBank_acc.nTransit_1, eFunctions.Values.eTypeData.etdDouble, True, 2) & "';")
                Response.Write("top.fraHeader.document.forms[0].tcnTransit2.value='" & mobjValues.TypeToString(lobjBank_acc.nTransit_2, eFunctions.Values.eTypeData.etdDouble, True, 2) & "';")
                Response.Write("top.fraHeader.document.forms[0].tcnTransit3.value='" & mobjValues.TypeToString(lobjBank_acc.nTransit_3, eFunctions.Values.eTypeData.etdDouble, True, 2) & "';")
                Response.Write("top.fraHeader.document.forms[0].tcnTransit4.value='" & mobjValues.TypeToString(lobjBank_acc.nTransit_4, eFunctions.Values.eTypeData.etdDouble, True, 2) & "';")
                Response.Write("top.fraHeader.document.forms[0].tcnTransit5.value='" & mobjValues.TypeToString(lobjBank_acc.nTransit_5, eFunctions.Values.eTypeData.etdDouble, True, 2) & "';")
                Response.Write("top.fraHeader.document.forms[0].valLedCompan.value='" & mobjValues.TypeToString(lobjBank_acc.nLed_compan, eFunctions.Values.eTypeData.etdDouble) & "';")
                '+ Parámetro del campo CUENTA CONTABLE - ACM - 20/12/2001
                Response.Write("top.fraHeader.document.forms[0].valAccLedger.Parameters.Param1.sValue='" & mobjValues.TypeToString(lobjBank_acc.nLed_compan, eFunctions.Values.eTypeData.etdDouble) & "';")
                Response.Write("top.fraHeader.document.forms[0].valAccLedger.value='" & lobjBank_acc.sAcc_ledger & "';")
                '+ Parámetro del campo AUXILIAR CONTABLE - ACM - 20/12/2001
                Response.Write("top.fraHeader.document.forms[0].valAuxAccount.Parameters.Param1.sValue='" & lobjBank_acc.sAcc_ledger & "';")
                Response.Write("top.fraHeader.document.forms[0].valAuxAccount.Parameters.Param2.sValue='" & mobjValues.TypeToString(lobjBank_acc.nLed_compan, eFunctions.Values.eTypeData.etdDouble) & "';")
                Response.Write("top.fraHeader.document.forms[0].valAuxAccount.value='" & lobjBank_acc.sAux_accoun & "';")
                Response.Write("top.fraHeader.$('#valAuxAccount').change();")
                Session("nBankCode") = lobjBank_acc.nBank_code
                Response.Write("top.fraHeader.$('#valBk_agency').change();")
                Response.Write("top.fraHeader.$('#valLedCompan').change();")
                Response.Write("top.fraHeader.$('#valAccLedger').change();")
            End If
	End If
	
	lobjCash_acc = Nothing
	lobjBank_acc = Nothing
	
End Sub

'%insCalCheqInitEnd: En este evento se recalcula la cantidad de cheques emitidos,cancelados y pendientes
'%si el usuario modIficó el rango de cheques asociado a la chequera o la cantidad de cheques dañados
'--------------------------------------------------------------------------------------------
Private Sub insCalCheqInitEnd()
	'--------------------------------------------------------------------------------------------
	Dim ldblCheqEmited As Integer
	Dim ldblCheqCancel As Integer
	Dim ldblCheqrange As Double
	Dim lclsCheq_book As eCashBank.Cheq_book
	Dim lobjValues As eFunctions.Values
	
	With Server
		lobjValues = New eFunctions.Values
		lclsCheq_book = New eCashBank.Cheq_book
	End With
	
	Response.Write("opener.document.forms[0].tcnCheqRangeChange.value=""1"";")
	With Request
		If .QueryString.Item("nCheqEnd") <> vbNullString And .QueryString.Item("nCheqInit") <> vbNullString Then
			ldblCheqEmited = lclsCheq_book.insReaCheqIssue(lobjValues.StringToType(Session("nAcc_bank"), eFunctions.Values.eTypeData.etdDouble), .QueryString.Item("nCheqInit"), .QueryString.Item("nCheqEnd"))
			
			ldblCheqCancel = lclsCheq_book.insReaCheqCancel(lobjValues.StringToType(Session("nAcc_bank"), eFunctions.Values.eTypeData.etdDouble), .QueryString.Item("nCheqInit"), .QueryString.Item("nCheqEnd"))
			If ldblCheqEmited <> -1 And ldblCheqCancel <> -1 Then
				Response.Write("UpdateDiv('lblCheqIssue','" & ldblCheqEmited & "','PopUp');")
				Response.Write("UpdateDiv('lblCheqCancel','" & ldblCheqCancel & "','PopUp');")
				Response.Write("opener.document.forms[0].tcnCheqCancel.value = " & ldblCheqCancel & ";")
				ldblCheqrange = CDbl(.QueryString.Item("nCheqEnd")) - CDbl(.QueryString.Item("nCheqInit"))
				If ldblCheqrange > 0 Then
					If (ldblCheqrange - (ldblCheqEmited + CDbl(.QueryString.Item("nCheqCancel")) + CDbl(.QueryString.Item("nCheqDan")))) > 0 Then
						Response.Write("UpdateDiv('lblCheqOutstand','" & ldblCheqrange - (ldblCheqEmited + CDbl(.QueryString.Item("nCheqCancel")) + CDbl(.QueryString.Item("nCheqDan"))) & "','PopUp');")
					Else
						Response.Write("UpdateDiv('lblCheqOutstand','" & 0 & "','PopUp');")
					End If
				Else
					Response.Write("UpdateDiv('lblCheqIssue','" & 0 & "','PopUp');")
					Response.Write("UpdateDiv('lblCheqCancel','" & 0 & "','PopUp');")
					Response.Write("opener.document.forms[0].tcnCheqCancel.value = " & 0 & ";")
					Response.Write("UpdateDiv('lblCheqOutstand','" & 0 & "','PopUp');")
				End If
			Else
				Response.Write("UpdateDiv('lblCheqIssue','" & 0 & "','PopUp');")
				Response.Write("UpdateDiv('lblCheqCancel','" & 0 & "','PopUp');")
				Response.Write("opener.document.forms[0].tcnCheqCancel.value = " & 0 & ";")
				Response.Write("UpdateDiv('lblCheqOutstand','" & 0 & "','PopUp');")
			End If
		Else
			Response.Write("UpdateDiv('lblCheqIssue','" & 0 & "','PopUp');")
			Response.Write("UpdateDiv('lblCheqCancel','" & 0 & "','PopUp');")
			Response.Write("opener.document.forms[0].tcnCheqCancel.value = " & 0 & ";")
			Response.Write("UpdateDiv('lblCheqOutstand','" & 0 & "','PopUp');")
		End If
	End With
	
	lclsCheq_book = Nothing
	lobjValues = Nothing
End Sub

'%insCalCheqDan: En este evento se recalcula la cantidad de cheques emitidos,cancelados y pendientes
'%si el usuario modIficó el rango de cheques asociado a la chequera o la cantidad de cheques dañados
'--------------------------------------------------------------------------------------------
Private Sub insCalCheqDan()
	'--------------------------------------------------------------------------------------------
	Dim lclsCheq_book As eCashBank.Cheq_book
	Dim lobjValues As eFunctions.Values
	Dim lintCheqIssue As Integer
	Dim ldblCheqrange As Double
	
	With Server
		lclsCheq_book = New eCashBank.Cheq_book
		lobjValues = New eFunctions.Values
	End With
	
	With Request
		lintCheqIssue = lclsCheq_book.insReaCheqIssue(lobjValues.StringToType(Session("nAcc_bank"), eFunctions.Values.eTypeData.etdDouble), .QueryString.Item("nCheqInit"), .QueryString.Item("nCheqEnd"))
		If .QueryString.Item("nCheqEnd") <> vbNullString And .QueryString.Item("nCheqInit") <> vbNullString Then
			ldblCheqrange = CDbl(.QueryString.Item("nCheqEnd")) - CDbl(.QueryString.Item("nCheqInit"))
			If ldblCheqrange > 0 Then
				If (ldblCheqrange - (lintCheqIssue + CDbl(.QueryString.Item("nCheqCancel")) + CDbl(.QueryString.Item("nCheqDan")))) > 0 Then
					Response.Write("UpdateDiv('lblCheqOutstand','" & ldblCheqrange - (lintCheqIssue + CDbl(.QueryString.Item("nCheqCancel")) + CDbl(.QueryString.Item("nCheqDan"))) & "','PopUp');")
				Else
					Response.Write("UpdateDiv('lblCheqOutstand','" & 0 & "','PopUp');")
				End If
			End If
		End If
	End With
	
	lclsCheq_book = Nothing
	lobjValues = Nothing
End Sub

'%insShowCheqIssueOutstand
'--------------------------------------------------------------------------------------------
Private Function insShowCheqIssueOutstand() As Object
	'--------------------------------------------------------------------------------------------
	Dim lclsCheq_book As eCashBank.Cheq_book
	Dim lobjValues As eFunctions.Values
	Dim lintCheqIssue As Integer
	
	With Server
		lclsCheq_book = New eCashBank.Cheq_book
		lobjValues = New eFunctions.Values
	End With
	
	With Request
		lintCheqIssue = lclsCheq_book.insReaCheqIssue(lobjValues.StringToType(Session("nAcc_bank"), eFunctions.Values.eTypeData.etdDouble), .QueryString.Item("nCheqInit"), .QueryString.Item("nCheqEnd"))
		
		Response.Write("UpdateDiv('lblCheqIssue','" & lintCheqIssue & "','PopUp');")
		
		If .QueryString.Item("nCheqEnd") <> vbNullString And .QueryString.Item("nCheqInit") <> vbNullString Then
			If (CDbl(.QueryString.Item("nCheqEnd")) - CDbl(.QueryString.Item("nCheqInit"))) - (lintCheqIssue + CDbl(.QueryString.Item("nCheqCancel")) + CDbl(.QueryString.Item("nCheqDan"))) > 0 Then
				Response.Write("UpdateDiv('lblCheqOutstand','" & (CDbl(.QueryString.Item("nCheqEnd")) - CDbl(.QueryString.Item("nCheqInit"))) - (lintCheqIssue + CDbl(.QueryString.Item("nCheqCancel")) + CDbl(.QueryString.Item("nCheqDan"))) & "','PopUp');")
			Else
				Response.Write("UpdateDiv('lblCheqOutstand','" & 0 & "','PopUp');")
			End If
		Else
			Response.Write("UpdateDiv('lblCheqOutstand','" & 0 & "','PopUp');")
		End If
	End With
	
	lclsCheq_book = Nothing
	lobjValues = Nothing
End Function

'% insShowRemNum: se muestran el número de remesa
'--------------------------------------------------------------------------------------------
Sub insShowRemNum()
	'--------------------------------------------------------------------------------------------
	Dim lobjNumerator As eGeneral.GeneralFunction
	Dim llngRemNum As Double
	lobjNumerator = New eGeneral.GeneralFunction
	llngRemNum = lobjNumerator.Find_Numerator(33, 0, Session("nUsercode"))
	Response.Write("try{opener.document.forms[0].gmnRemNum.value=" & llngRemNum & ";")
	Response.Write("opener.document.forms[0].gmnRemNum.disabled=true;}")
	Response.Write("catch(x){}")
	lobjNumerator = Nothing
End Sub

'% insShowBalance: se muestra el saldo de la cuenta corriente
'--------------------------------------------------------------------------------------------
Sub insShowBalance()
	'--------------------------------------------------------------------------------------------
	Dim lobjCurr_acc As eCashBank.Curr_acc
	Dim lobjClient As eClient.Client
        Dim ldblAmount As Double
	Dim loptDeb As Byte
	Dim loptCre As Byte
	Dim lstrClient As String
	
	lobjClient = New eClient.Client
	
	lstrClient = lobjClient.ExpandCode(Request.QueryString.Item("sClient"))
	
	'+ Si se está mostrando el saldo de la cuenta corriente y para la clave (tipo de cuenta-
	'+ -tipo de negocio-titular) existe más de una moneda, se toma el valor de la moneda dada por 
	'+ el usuario
	
	If Request.QueryString.Item("Field") = "Balance" Then
		mintCurrency = mobjValues.StringToType(Request.QueryString.Item("nCurrency"), eFunctions.Values.eTypeData.etdDouble)
	End If
	
	lobjCurr_acc = New eCashBank.Curr_acc
	
	Response.Write("try{")
	If lobjCurr_acc.findClientCurr_acc(mobjValues.StringToType(Request.QueryString.Item("nTypeAccount"), eFunctions.Values.eTypeData.etdDouble), Request.QueryString.Item("sBussiType"), lstrClient, mintCurrency) Then
		If mobjValues.StringToType(CStr(lobjCurr_acc.nBalance), eFunctions.Values.eTypeData.etdDouble) < 0 Then
			ldblAmount = System.Math.Abs(lobjCurr_acc.nBalance)
			loptDeb = 1
			loptCre = 2
			Session("optCreDebAux") = 1
			Response.Write("top.opener.document.forms[0].optCreDeb[0].checked=true;")
			Response.Write("top.opener.document.forms[0].optCreDeb[1].checked=false;")
			Response.Write("top.opener.document.forms[0].optCreDeb[0].value=1;")
			Response.Write("top.opener.document.forms[0].optCreDeb[1].value=0;")
		Else
			loptDeb = 2
			loptCre = 1
			Session("optCreDebAux") = 2
			ldblAmount = System.Math.Abs(lobjCurr_acc.nBalance * -1)
			Response.Write("top.opener.document.forms[0].optCreDeb[0].checked=false;")
			Response.Write("top.opener.document.forms[0].optCreDeb[1].checked=true;")
			Response.Write("top.opener.document.forms[0].optCreDeb[0].value=0;")
			Response.Write("top.opener.document.forms[0].optCreDeb[1].value=1;")
		End If
		Response.Write("top.opener.document.forms[0].gmnAmount.value='" & mobjValues.TypeToString(ldblAmount, eFunctions.Values.eTypeData.etdDouble, True, 6) & "';")
		Response.Write("top.opener.document.forms[0].gmnPayAmount.value='" & mobjValues.TypeToString(ldblAmount, eFunctions.Values.eTypeData.etdDouble, True, 6) & "';")
	End If
	Response.Write("}catch(x){};")
	
	lobjCurr_acc = Nothing
	lobjClient = Nothing
	
End Sub


'% insShowCurren: Muestra la cantidad de cuentas/monedas asociadas.
'--------------------------------------------------------------------------------------------
Private Sub insShowCurren()
	'--------------------------------------------------------------------------------------------
	Dim lobjCurr_acc As eCashBank.Curr_acc
	Dim lobjIntermedia As eAgent.Intermedia
	Dim sClient As String
	Dim lobjClient As eClient.Client
	
	lobjCurr_acc = New eCashBank.Curr_acc
	lobjIntermedia = New eAgent.Intermedia
	lobjClient = New eClient.Client
	
	If Request.QueryString.Item("sClient") <> "OPC012" Then
		If CDbl(Request.QueryString.Item("nIntermed")) > 0 Then
			Call lobjIntermedia.Find(mobjValues.StringToType(Request.Item("nIntermed"), eFunctions.Values.eTypeData.etdDouble))
			sClient = lobjIntermedia.sClient
		Else
			sClient = lobjClient.ExpandCode(Request.QueryString.Item("sClient"))
		End If
	Else
		sClient = lobjClient.ExpandCode(Request.QueryString.Item("sClient"))
	End If
	
	'Response.Write "top.fraFolder.document.forms[0].cboCurrency.Parameters.Param1.sValue=" & Request.QueryString("nType_acc") & ";"
	'Response.Write "top.fraFolder.document.forms[0].cboCurrency.Parameters.Param4.sValue='" &  sclient & "';"
	
	If lobjCurr_acc.AccCount(CInt(Request.QueryString.Item("nType_acc")), "0", sClient) Then
		
		If lobjCurr_acc.nCount = 1 Then
			Response.Write("top." & Request.QueryString.Item("sZone") & ".document.forms[0].cboCurrency.value=" & lobjCurr_acc.nCurrency & ";")
			Response.Write("top." & Request.QueryString.Item("sZone") & ".document.forms[0].cboCurrency.disabled=true;")
			Response.Write("top." & Request.QueryString.Item("sZone") & ".$('#cboCurrency').change();")
			Response.Write("top." & Request.QueryString.Item("sZone") & ".document.forms[0].btncboCurrency.disabled = true;")
		Else
			Response.Write("top." & Request.QueryString.Item("sZone") & ".document.forms[0].cboCurrency.disabled=false;")
		End If
	Else
		
	End If
	lobjCurr_acc = Nothing
	lobjIntermedia = Nothing
	lobjClient = Nothing
	
End Sub

'%insShowRequeNum: Se muestra el número de orden asociado a la solicitud a registrar
'--------------------------------------------------------------------------------------------
Private Function insShowRequeNum() As Object
	'--------------------------------------------------------------------------------------------
	Dim lclsGeneralFunction As eGeneral.GeneralFunction
	lclsGeneralFunction = New eGeneral.GeneralFunction
	Response.Write("opener.document.forms[0].tcnRequeNum.value = " & lclsGeneralFunction.Find_Numerator(10, CShort(0), mobjValues.StringToType(Request.Item("nUserCode"), eFunctions.Values.eTypeData.etdInteger)) & ";")
	lclsGeneralFunction = Nothing
End Function

'---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
Private Sub InsShowAccount()
	'---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
	
	Dim lclsBank_acc As eCashBank.Bank_acc
	lclsBank_acc = New eCashBank.Bank_acc
	With lclsBank_acc
		If .Find_O(mobjValues.StringToType(Request.QueryString.Item("sAccount"), eFunctions.Values.eTypeData.etdDouble)) Then
			Response.Write("opener.document.forms[0].tcnAmountTransf.value='" & mobjValues.TypeToString(.nAvailable, eFunctions.Values.eTypeData.etdDouble, True, 2) & "';")
			Response.Write("opener.document.forms[0].cboCurrency.value = " & .nCurrency & ";")
			Response.Write("opener.UpdateDiv('lblCurrency" & "','" & mobjValues.getMessage(.nCurrency, "table11") & "','Normal');")
		End If
	End With
	lclsBank_acc = Nothing
End Sub

'---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
Private Sub InsShowExchange()
	'---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
	
	Dim ldblExchange As Object
	Dim ldblExchangeOld As Object
	Dim lclsExchange As eGeneral.Exchange
	Dim lintCurrency As String
	
	lclsExchange = New eGeneral.Exchange
	lintCurrency = Request.QueryString.Item("nOriCurrency")
	If lintCurrency = "1" Then
		lintCurrency = Request.QueryString.Item("nDesCurrency")
	End If
	lclsExchange.Find(mobjValues.StringToType(lintCurrency, eFunctions.Values.eTypeData.etdDouble), Session("dTransDate"))
	ldblExchangeOld = lclsExchange.nExchange
	If Request.QueryString.Item("nOriCurrency") = "1" Then
		ldblExchange = Request.QueryString.Item("nExchangeFromLocal")
		If ldblExchangeOld <> 0 Then
			ldblExchangeOld = 1 / ldblExchangeOld
		End If
	Else
		ldblExchange = Request.QueryString.Item("nExchangeToLocal")
	End If
	If ldblExchangeOld <> ldblExchange Then
		Response.Write("opener.document.forms[0].tcnExchange.value='" & mobjValues.TypeToString(ldblExchange, eFunctions.Values.eTypeData.etdDouble, True, 2) & "';")
		Response.Write("opener.document.forms[0].tcnAmountNew.value='" & mobjValues.TypeToString(Session("nAmountTransf") * ldblExchange, eFunctions.Values.eTypeData.etdDouble, True, 2) & "';")
	End If
	lclsExchange = Nothing
End Sub

'---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
Private Sub ShowCurrencyValue()
	'---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
	Dim lclsBank_account As eCashBank.Bank_acc
	Dim lintAccount As Integer
	lclsBank_account = New eCashBank.Bank_acc
	
	If Request.QueryString.Item("Account_number") <> "0" And Request.QueryString.Item("Account_number") <> vbNullString Then
		
		lintAccount = mobjValues.StringToType(Request.QueryString.Item("Account_number"), eFunctions.Values.eTypeData.etdDouble)
		If lclsBank_account.FindCurrency(lintAccount) Then
			Response.Write("opener.UpdateDiv('lblCurrency','" & lclsBank_account.sCurrDescript & "','Normal');")
		End If
	End If
	lclsBank_account = Nothing
	
End Sub

'---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
Private Sub CalculateMovementNumber()
	'---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
	Dim lclsCurr_Acc As eCashBank.Curr_acc
	Dim lclsClient As eClient.Client
	Dim lstrClient As String
	
	lclsCurr_Acc = New eCashBank.Curr_acc
	lclsClient = New eClient.Client
	If Len(Trim(Request.QueryString.Item("sClient"))) < 14 Then
		lstrClient = lclsClient.ExpandCode(Request.QueryString.Item("sClient"))
	Else
		lstrClient = Request.QueryString.Item("sClient")
	End If
	
	If lclsCurr_Acc.insvalLastMovement(CInt(Request.QueryString.Item("nTypeAccount")), CInt(Request.QueryString.Item("sBussiType")), lstrClient, CInt(Request.QueryString.Item("nCurrency")), CDate(Request.QueryString.Item("dOperDate"))) Then
		Response.Write("top.fraHeader.document.forms[0].elements['gmnTransact'].value = " & lclsCurr_Acc.nIDConsec & ";")
		Response.Write("top.fraHeader.$('#valClient').change();")
	End If
	
	lclsCurr_Acc = Nothing
	lclsClient = Nothing
	lstrClient = Nothing
End Sub

'% insShowIntAmount: Calcula y muestra el porcentaje de interés financiero.
'--------------------------------------------------------------------------------------------
Sub insShowIntAmount()
	'--------------------------------------------------------------------------------------------
	
	Dim ldblPercent As Object
	Dim lclsCash_mov As eCashBank.Cash_mov
	lclsCash_mov = New eCashBank.Cash_mov
	
	ldblPercent = lclsCash_mov.GetFinanInt(mobjValues.StringToType(Request.QueryString.Item("nAmount"), eFunctions.Values.eTypeData.etdDouble), CDate(Request.QueryString.Item("dDatePro")), CDate(Request.QueryString.Item("dDateDoc")))
	
	Response.Write("top.fraFolder.document.forms[0].tcnIntAmount.value='" & mobjValues.TypeToString(ldblPercent, eFunctions.Values.eTypeData.etdDouble, True, 2) & "';")
	
	lclsCash_mov = Nothing
	
End Sub

'% insClientName:  Carga el nombre del cliente en el detalle
'----------------------------------------------------------------
Sub insClientName()
	'----------------------------------------------------------------
	Dim lobjClient As eClient.Client
	lobjClient = New eClient.Client
	
	If Request.QueryString.Item("sClient") <> vbNullString Then
		If lobjClient.FindClientName(Request.QueryString.Item("sClient")) Then
			Response.Write("top.frames['fraFolder'].UpdateDiv('lblCliename2','" & lobjClient.sCliename & "');")
			
		End If
	End If
	lobjClient = Nothing
End Sub

'% insShowPayOrdBord: se muestran el número de relación
'--------------------------------------------------------------------------------------------
Sub insShowPayOrdBord()
	'--------------------------------------------------------------------------------------------
	Dim lobjNumerator As eGeneral.GeneralFunction
	Dim llngPayOrdBord As Double
	
	lobjNumerator = New eGeneral.GeneralFunction
	llngPayOrdBord = lobjNumerator.Find_Numerator(64, 0, Session("nUsercode"))
	Response.Write("top.fraHeader.document.forms[0].tcnPayOrdBord.value=" & llngPayOrdBord & ";")
	Response.Write("top.fraHeader.document.forms[0].tcnPayOrdBord.disabled=true;")
	lobjNumerator = Nothing
	
End Sub

'% insShowIntermed: Habilita o inhabilita el campo intermediario
'--------------------------------------------------------------------------------------------
Sub insShowIntermed()
	'--------------------------------------------------------------------------------------------
	Dim lobjIntermedia As eAgent.Interm_typ
	Dim lobjIntermedia_aux As eAgent.Intermedia                
	Response.Write("top.fraHeader.document.forms[0].valIntermedia.value='';")
	Dim lclsClientInter As eAgent.Intermedia
    Dim sClient As String
    Dim nIntermed As Integer    
    Dim lobjClient As eClient.Client    

	lobjIntermedia = New eAgent.Interm_typ
    lclsClientInter = New eAgent.Intermedia
    lobjClient = New eClient.Client
	sClient = lobjClient.ExpandCode(Request.QueryString.Item("sClient"))
    If Request.QueryString.Item("nIntermed") <> "" then
        nIntermed = Request.QueryString.Item("nIntermed")
    Else
        nIntermed = 0            
    End if
        
	If lobjIntermedia.Find_Typ_acco(CInt(Request.QueryString.Item("nTypeAccount"))) Then
		Response.Write("top.fraHeader.document.forms[0].valIntermedia.disabled=false;")
		Response.Write("top.fraHeader.document.forms[0].btnvalIntermedia.disabled=false;")
        If lclsClientInter.Find(nIntermed) Then
            If sClient <> lclsClientInter.sClient Then    
                Response.Write("top.fraHeader.document.forms[0].valIntermedia.value='';")
                Response.Write("top.fraHeader.UpdateDiv('valIntermediaDesc','');")
            End if
        End If
	Else
        Response.Write("top.fraHeader.document.forms[0].valIntermedia.value='';")
        Response.Write("top.fraHeader.UpdateDiv('valIntermediaDesc','');")            
		Response.Write("top.fraHeader.document.forms[0].valIntermedia.disabled=true;")
		Response.Write("top.fraHeader.document.forms[0].btnvalIntermedia.disabled=true;")
            Response.Write("top.fraHeader.document.forms[0].valIntermedia.value='';")
            Response.Write("top.fraHeader.$('#valIntermedia').change();")
        End If
	lobjIntermedia = Nothing
	
    
        If CStr(Request.QueryString.Item("sClient")) <> vbNullString Then
            Response.Write("top.fraHeader.document.forms[0].valIntermedia.value='';")
            lobjIntermedia_aux = New eAgent.Intermedia	
            If lobjIntermedia_aux.Find_ClientInter(CStr(Request.QueryString.Item("sClient"))) Then
                Response.Write("top.fraHeader.document.forms[0].valIntermedia.value='" & lobjIntermedia_aux.nIntermed & "';")                
                Response.Write("top.fraHeader.$('#valIntermedia').change();")                
                lobjIntermedia = Nothing
            End If
        Else
            Response.Write("top.fraHeader.document.forms[0].valIntermedia.value='';")
            Response.Write("top.fraHeader.$('#valIntermedia').change();")
        End If
    End Sub
    '% insShowIntermed: Habilita o inhabilita el campo intermediario
    '--------------------------------------------------------------------------------------------
    Sub insShowIntermed2()
        '--------------------------------------------------------------------------------------------
        Dim lobjIntermedia As eAgent.Interm_typ
        Dim lobjIntermedia_aux As eAgent.Intermedia
                
        Response.Write("top.fraFolder.document.forms[0].valIntermedia.value='';")
        lobjIntermedia = New eAgent.Interm_typ
	
        If lobjIntermedia.Find_Typ_acco(CInt(Request.QueryString.Item("nTypeAccount"))) Then
            Response.Write("top.fraFolder.document.forms[0].valIntermedia.disabled=false;")
            Response.Write("top.fraFolder.document.forms[0].btnvalIntermedia.disabled=false;")
        Else
            Response.Write("top.fraFolder.document.forms[0].valIntermedia.disabled=true;")
            Response.Write("top.fraFolder.document.forms[0].btnvalIntermedia.disabled=true;")
            Response.Write("top.fraFolder.document.forms[0].valIntermedia.value='';")
            Response.Write("top.fraFolder.$('#valIntermedia.change();")
        End If
        lobjIntermedia = Nothing
	
    
        If CStr(Request.QueryString.Item("sClient")) <> vbNullString Then
            Response.Write("top.fraFolder.document.forms[0].valIntermedia.value='';")
            lobjIntermedia_aux = New eAgent.Intermedia
            If lobjIntermedia_aux.Find_ClientInter(CStr(Request.QueryString.Item("sClient"))) Then
                Response.Write("top.fraFolder.document.forms[0].valIntermedia.value='" & lobjIntermedia_aux.nIntermed & "';")
                Response.Write("top.fraFolder.$('#valIntermedia').change();")
                lobjIntermedia = Nothing
            End If
        Else
            Response.Write("top.fraFolder.document.forms[0].valIntermedia.value='';")
            Response.Write("top.fraFolder.$('#valIntermedia').change();")
        End If
    End Sub
    '% insShowIntermed: Habilita o inhabilita el campo intermediario
    '--------------------------------------------------------------------------------------------
    Sub insShowIntermedbysClient()
        '--------------------------------------------------------------------------------------------
        Dim lobjIntermedia As eAgent.Intermedia
	
        Response.Write("top.fraHeader.document.forms[0].valIntermedia.value='';")
        lobjIntermedia = New eAgent.Intermedia
	
        lobjIntermedia.Find_ClientInter(CInt(Request.QueryString.Item("sClient")))
        Response.Write("top.fraHeader.document.forms[0].valIntermedia.value='" & lobjIntermedia.nIntermed & "';")
        Response.Write("top.fraHeader.$('#valIntermedia').change();")        
        lobjIntermedia = Nothing
	
    End Sub
    '--------------------------------------------------------------------------------------------
    Sub insShowValuesAccAvaliable()
        '-------------------------------------------------------------------------------------------	        
        Dim lobjBank_acc As eCashBank.Bank_acc	
        
        lobjBank_acc = New eCashBank.Bank_acc
	
        If lobjBank_acc.Find(mobjValues.StringToType(Request.QueryString.Item("nAccBank"), eFunctions.Values.eTypeData.etdDouble), True) Then
            'Response.Write("top.fraHeader.document.forms[0].Available.value ='" & lobjBank_acc.nAvailable & "';")
            Response.Write("top.fraHeader.UpdateDiv('Available','" & lobjBank_acc.nAvailable & "');")
        End If
	
        lobjBank_acc = Nothing
	
    End Sub    
</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.31.22
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "showdefvalues"
%>
<HTML>
<HEAD>
    <%=mobjValues.StyleSheet()%>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>




<SCRIPT>
//+ Variable para el control de versiones
	document.VssVersion="$$Revision: 7 $|$$Date: 27/07/04 9:50 $|$$Author: Nvaplat28 $"
</SCRIPT>
</HEAD>
<BODY>
	<FORM NAME="ShowValues">
	</FORM>
</BODY>
</HTML>
<%
Response.Write("<SCRIPT>")

Select Case Request.QueryString.Item("Field")
	Case "BussiType"
		Call insShowCurrAcc()
	Case "BussiTypeParam"
		Call insShowCurrAccParam()
	Case "AccBankCash"
		Call insShowValuesAccBankCash()
	Case "CheqInitEnd"
		Call insCalCheqInitEnd()
	Case "CheqDan"
		Call insCalCheqDan()
	Case "CheqIssueOutstand"
		Call insShowCheqIssueOutstand()
	Case "RemNum"
		Call insShowRemNum()
	Case "Balance"
		Call insShowBalance()
	Case "RequeNum"
		Call insShowRequeNum()
	Case "Curren"
		Call insShowCurren()
	Case "Account"
		Call InsShowAccount()
	Case "Exchange"
		Call InsShowExchange()
	Case "CurrencyAccount"
		Call ShowCurrencyValue()
	Case "MovementNumber"
		Call CalculateMovementNumber()
	Case "IntAmount"
		Call insShowIntAmount()
	Case "MovAcc"
		Call insShowMovAcc()
	Case "ClientName"
		Call insClientName()
	Case "PayOrdBord"
		Call insShowPayOrdBord()
	Case "Cashnum"
		Call insShowCashnum()
	Case "Client_OPC720"
		Call insShowCashnum_by_Client()
	Case "Cashnum_OPC720"
		Call insShowClient_by_Cashnum()
	Case "Intermed"
		insShowIntermed()
    Case "Avaliable"
            insShowValuesAccAvaliable()
        Case "Intermed2"
            insShowIntermed2()
    End Select

Response.Write(mobjValues.CloseShowDefValues(Request.QueryString.Item("sFrameCaller")))
Response.Write("</SCRIPT>")

mobjValues = Nothing

mobjValues = Nothing
%>




