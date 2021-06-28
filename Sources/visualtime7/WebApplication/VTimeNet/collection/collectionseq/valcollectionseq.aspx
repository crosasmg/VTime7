<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eCollection" %>
<%@ Import namespace="eCashBank" %>
<%@ Import namespace="eProduct" %>
<%@ Import namespace="eRemoteDB" %>
<%@ Import namespace="eReports" %>
<%@ Import namespace="eBatch" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 3/4/03 12.00.01
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

Dim mobjValues As eFunctions.Values
Dim mobjCollection As Object
Dim mstrLocationCO001_k As String
Dim mstrErrors As String
Dim mstrKeyGenDoc As Object

'+ Se declara la variable para almacenar el String en donde se definen los controles HIDDEN
'+ de la página que la invoca. 
Dim mstrCommand As String

'+  Variable para usar el querystring
Dim mstrQueryString As String


'% insvalSequence: Se realizan las validaciones masivas de la forma
'--------------------------------------------------------------------------------------------
Function insvalSequence() As String
	Dim nOldAmount As Double
	Dim ldblDocument As Double
	Dim nBalanceTotal As Double
	Dim nAmount_Aux As Double
	Dim nBalance_Aux As Double
	'--------------------------------------------------------------------------------------------
	
	Dim nCompany As Integer
	Dim nIsur_area As Integer
	
	mobjNetFrameWork.BeforeValidate(Request.QueryString.Item("sCodispl"))
	Dim lobjCash_Stat As eCashBank.Cash_stat
	If Request.QueryString.Item("nAction") = CStr(eFunctions.Menues.TypeActions.clngAcceptDataFinish) Then
		If Session("bQuery") = False Then
			mobjCollection = New eCollection.ColformRef
			insvalSequence = mobjCollection.insValFolder(Session("nBordereaux"))
		End If
	Else
		
		Select Case Request.QueryString.Item("sCodispl")
			
			Case "CO001_K", "CO01_K"
				
				mobjCollection = New eCollection.ColformRef
				'+ inico 
				'+ en caso que no el usuario no tenga caja asociada 
				'+ abre la caja 9999 para este                 
				With Request
					If CStr(Session("nCashNum")) = "" Or Session("nCashNum") = 0 Then
						lobjCash_Stat = New eCashBank.Cash_stat
						If lobjCash_Stat.valCash_statClosed(CInt("9999"), mobjValues.StringToType(.Form.Item("tcdCollectDate"), eFunctions.Values.eTypeData.etdDate)) Then
							If lobjCash_Stat.insPostOPL719_K(9, mobjValues.StringToType(.Form.Item("tcdCollectDate"), eFunctions.Values.eTypeData.etdDate), 9999, mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble)) Then
								Session("nCashNum") = "9999"
							End If
						Else
							Session("nCashNum") = "9999"
						End If
						lobjCash_Stat = Nothing
					End If
					'+ fin				    
					insvalSequence = mobjCollection.insvalCO001_K(.QueryString("sCodispl"), mobjValues.StringToType(.Form.Item("cbeAction"), eFunctions.Values.eTypeData.etdDouble, True), Session("nInsur_Area"), mobjValues.StringToType(.Form.Item("cbeInputTyp"), eFunctions.Values.eTypeData.etdDouble, True), .Form.Item("cbeRel_Type"), mobjValues.StringToType(.Form.Item("cbeBank"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valBank_Agree"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valAgreement"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnCertif"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("dtcClient"), mobjValues.StringToType(.Form.Item("tcdCollectDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("hddnCurrency"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnRelaNum"), eFunctions.Values.eTypeData.etdDouble, True), Session("nUserCode"), Session("nCashNum"), mobjValues.StringToType(.Form.Item("valCollector"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcdCollect"), eFunctions.Values.eTypeData.etdDate))
				End With
				
			Case "CO001"

				mobjCollection = New eCollection.T_DocTyp
				
				With Request
					If mobjValues.StringToType(.Form.Item("nItems"), eFunctions.Values.eTypeData.etdDouble) <= 0 Then
						If .QueryString.Item("WindowType") <> "PopUp" Then
							'+ Se debe seleccionar por lo menos un registro de la ventana
							insvalSequence = mobjCollection.insValCO001(Session("nBordereaux"), mobjValues.StringToType(.Form.Item("nItems"), eFunctions.Values.eTypeData.etdDouble))
						End If
					End If
					
					'+ Si no hay validación.

					If insvalSequence = vbNullString Then
						If .QueryString.Item("WindowType") = "PopUp" Then
							If mobjValues.StringToType(.Form.Item("cbeCollecDocTyp"), eFunctions.Values.eTypeData.etdDouble, True) = 2 Then
								ldblDocument = mobjValues.StringToType(.Form.Item("hddnReceipt"), eFunctions.Values.eTypeData.etdDouble, True)
							Else
								ldblDocument = mobjValues.StringToType(.Form.Item("tcnDocument"), eFunctions.Values.eTypeData.etdDouble, True)
							End If
                            insvalSequence = mobjCollection.insValCO001Upd(.QueryString("sCodispl"), .QueryString("WindowType"), "1", .QueryString("Action"), Session("CO001_nAction"), Session("sReceiptNum"), Session("sPolicynum"), Session("sRel_Type"), Session("nBordereaux"), mobjValues.StringToType(.Form.Item("cbeCollecDocTyp"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("hddnType"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddnContrat"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnDraft"), eFunctions.Values.eTypeData.etdDouble), ldblDocument, mobjValues.StringToType(.Form.Item("tcnAmountCol"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnAmountPay"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnAmountLoc"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnInterest_rate"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeCurrency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnExchange"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("dtcClient"), mobjValues.StringToType(Session("nCod_agree"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Session("nInsur_area"), eFunctions.Values.eTypeData.etdDouble), Session("chkRentVital"), mobjValues.StringToType(.Form.Item("tcdValuedate"), eFunctions.Values.eTypeData.etdDate, True), eRemoteDB.Constants.dtmNull, mobjValues.StringToType(.Form.Item("tcnTax_discount"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnface_value"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcdIssuedate"), eFunctions.Values.eTypeData.etdDate, True), mobjValues.StringToType(.Form.Item("tcdExpirdate"), eFunctions.Values.eTypeData.etdDate, True), mobjValues.StringToType(.Form.Item("tcdValuedate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("valCode"), eFunctions.Values.eTypeData.etdDouble), Session("sValueDateAll"), mobjValues.StringToType(.Form.Item("hddnSequence"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valOrigin"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcdOriginDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("valInstitution"), eFunctions.Values.eTypeData.etdDouble, True), 1, mobjValues.StringToType(.Form.Item("cbeTyp_Profit"), eFunctions.Values.eTypeData.etdInteger, True), .Form.Item("chkNewReceipt"))
						Else
      						insvalSequence = mobjCollection.insValCO001Upd(.QueryString("sCodispl"), .QueryString("WindowType"), "1", .QueryString("Action"), Session("CO001_nAction"), Session("sReceiptNum"), Session("sPolicynum"), Session("sRel_Type"), Session("nBordereaux"), eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, vbNullString, mobjValues.StringToType(Session("nCod_agree"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Session("nInsur_area"), eFunctions.Values.eTypeData.etdDouble), Session("chkRentVital"), mobjValues.StringToType(.Form.Item("tcdValuedate"), eFunctions.Values.eTypeData.etdDate, True), eRemoteDB.Constants.dtmNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.dtmNull, eRemoteDB.Constants.dtmNull, eRemoteDB.Constants.dtmNull, eRemoteDB.Constants.intNull, Session("sValueDateAll"), eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.dtmNull, eRemoteDB.Constants.intNull, .Form.Item("Sel").Length, eRemoteDB.Constants.intNull, vbNullString)
						End If
					End If
				End With

			Case "CO823"
				mobjCollection = New eCollection.T_concepts
				With Request
					If .QueryString.Item("WindowType") = "PopUp" Then
						insvalSequence = mobjCollection.insValCO823Upd(mobjValues.StringToType(.Form.Item("valConcept"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeCurrency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnAmountOrig"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdValuedate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("valBank_agree"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valAccount_Agree"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valAgreement"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdCollect"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcnBulletins"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("dtcClient"), mobjValues.StringToType(.Form.Item("hddnProponum"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnClaim"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddCase_num"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valCurrAcc"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valIntermed"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valCompanyCR"), eFunctions.Values.eTypeData.etdDouble), Session("nCashNum"), Session("dCollectDate"), Session("nUsercode"), .QueryString("sCodispl"), mobjValues.StringToType(.Form.Item("valLoans"), eFunctions.Values.eTypeData.etdDouble), Session("nBordereaux"))
					Else
						'+ Se debe seleccionar por lo menos un registro de la ventana.
						If mobjValues.StringToType(.Form.Item("nItems"), eFunctions.Values.eTypeData.etdDouble) <= 0 Then
							insvalSequence = mobjCollection.insValCO823(Session("nBordereaux"), mobjValues.StringToType(.Form.Item("nItems"), eFunctions.Values.eTypeData.etdDouble))
						End If
					End If
				End With
				
			Case "CO008"
				mobjCollection = New eCollection.CashBankAccMov
				
				With Request
					If .QueryString.Item("WindowType") = "PopUp" Then
						
						insvalSequence = mobjCollection.insValCO008Upd(.QueryString("sCodispl"), Session("nBordereaux"), Session("dCollectDate"), mobjValues.StringToType(.Form.Item("nTypPay"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("dDoc_date"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("nBankAcc"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("nCurrency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("nAmount"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("nBank"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("sDocNumber"), mobjValues.StringToType(.Form.Item("nTypCreCard"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("nIntermed"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("sClient"), mobjValues.StringToType(.Form.Item("nLed_compan"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("sAccount"), Session("nUsercode"), Session("nCashNum"), mobjValues.StringToType(.Form.Item("nChequeLocat"), eFunctions.Values.eTypeData.etdDouble, True), Session("sRelorigi"), mobjValues.StringToType(.Form.Item("tcnCashId"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcdValDate"), eFunctions.Values.eTypeData.etdDate))
					Else
						'+ Se debe seleccionar por lo menos un registro de la ventana.
						If mobjValues.StringToType(.Form.Item("nItems"), eFunctions.Values.eTypeData.etdDouble) <= 0 Then
							insvalSequence = mobjCollection.insValCO008(Session("nBordereaux"), mobjValues.StringToType(.Form.Item("nItems"), eFunctions.Values.eTypeData.etdDouble))
						End If
					End If
				End With
				
			Case "CO010"
				mobjCollection = New eCollection.CashBankAccMov
				
				With Request
					
					If .QueryString.Item("WindowType") = "PopUp" Then
						insvalSequence = mobjCollection.insValCO010Upd(.QueryString("sCodispl"), Session("nBordereaux"), mobjValues.StringToType(Session("dCollectDate"), eFunctions.Values.eTypeData.etdDate), .Form.Item("sType"), mobjValues.StringToType(.Form.Item("nSequence"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("nTypDev"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("nAccBankO"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("nCurrency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("nAmount"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnExchange"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnAmountLoc"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("sClient"), mobjValues.StringToType(.Form.Item("nBankDes"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("nBk_agency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("nTypAcc"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("sAccBankD"))
					Else
						'+ Se debe seleccionar por lo menos un registro de la ventana.
						If mobjValues.StringToType(.Form.Item("nItems"), eFunctions.Values.eTypeData.etdDouble) <= 0 Then
							insvalSequence = mobjCollection.insValCO010(Session("nBordereaux"), mobjValues.StringToType(.Form.Item("nItems"), eFunctions.Values.eTypeData.etdDouble))
						End If
					End If
				End With
				
			Case "CO012"
				mobjCollection = New eCollection.T_Move_acc
				
				With Request
					
					
					If .QueryString.Item("WindowType") = "PopUp" Then
						
						If mobjValues.StringToType(.Form.Item("nAmount"), eFunctions.Values.eTypeData.etdDouble) = eRemoteDB.Constants.dblNull Then
							nAmount_Aux = 0
						Else
							nAmount_Aux = mobjValues.StringToType(.Form.Item("nAmount"), eFunctions.Values.eTypeData.etdDouble)
						End If
						
						If mobjValues.StringToType(.Form.Item("hddBalance"), eFunctions.Values.eTypeData.etdDouble) = eRemoteDB.Constants.dblNull Then
							nBalance_Aux = 0
						Else
							nBalance_Aux = mobjValues.StringToType(.Form.Item("hddBalance"), eFunctions.Values.eTypeData.etdDouble)
						End If
						
						If mobjValues.StringToType(.Form.Item("hddAmount"), eFunctions.Values.eTypeData.etdDouble) = eRemoteDB.Constants.dblNull Then
							nOldAmount = 0
						Else
							nOldAmount = mobjValues.StringToType(.Form.Item("hddAmount"), eFunctions.Values.eTypeData.etdDouble)
						End If
						
						
						nBalanceTotal = nOldAmount + nBalance_Aux
						
						insvalSequence = mobjCollection.insvalCO012Upd(Session("nBordereaux"), mobjValues.StringToType(.Form.Item("nAmount"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("sClient"), mobjValues.StringToType(.Form.Item("nCurrency"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnExchange"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("nOldAmountl"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("TotalDiff"), eFunctions.Values.eTypeData.etdDouble), nBalance_Aux, nBalanceTotal)
						
					Else
						insvalSequence = mobjCollection.insValCO012(Session("nBordereaux"), mobjValues.StringToType(.Form.Item("nItems"), eFunctions.Values.eTypeData.etdDouble))
					End If
				End With
				
			Case "GE101"
				insvalSequence = vbNullString
				
		End Select
	End If
	mobjNetFrameWork.AfterValidate(Request.QueryString.Item("sCodispl"))
End Function

'% insPostSequence: Se realizan las actualizaciones de las ventanas
'--------------------------------------------------------------------------------------------
Function insPostSequence() As Boolean
	Dim ldblDocument As Object
	'--------------------------------------------------------------------------------------------
	Dim lobjProduct As eProduct.Product
	Dim nCompany As Object
	Dim ldtmValueDate As Object
	
	mobjNetFrameWork.BeforePost(Request.QueryString.Item("sCodispl"))
	Select Case Request.Item("sCodispl")
		
		Case "CO001_K", "CO01_K"
			mobjCollection = New eCollection.ColformRef
			With Request
				
				'+ Indica si se valoriza con la misma fecha o se valoriza por cada documento
				If mobjValues.StringToType(.Form.Item("tcdValueDate"), eFunctions.Values.eTypeData.etdDate) = eRemoteDB.Constants.dtmNull Then
					ldtmValueDate = mobjValues.StringToType(.Form.Item("tcdCollectDate"), eFunctions.Values.eTypeData.etdDate)
					Session("sValueDateAll") = "2"
				Else
					ldtmValueDate = mobjValues.StringToType(.Form.Item("tcdValueDate"), eFunctions.Values.eTypeData.etdDate)
					Session("sValueDateAll") = "1"
				End If
				
				insPostSequence = mobjCollection.insPostCO001_K(mobjValues.StringToType(.Form.Item("cbeAction"), eFunctions.Values.eTypeData.etdDouble, True), Session("nInsur_Area"), mobjValues.StringToType(.Form.Item("cbeInputTyp"), eFunctions.Values.eTypeData.etdDouble, True), .Form.Item("cbeRel_Type"), mobjValues.StringToType(.Form.Item("cbeBank"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valAgreement"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valBank_Agree"), eFunctions.Values.eTypeData.etdDouble, True), "2", mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnCertif"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("dtcClient"), mobjValues.StringToType(.Form.Item("tcdCollectDate"), eFunctions.Values.eTypeData.etdDate), ldtmValueDate, mobjValues.StringToType(.Form.Item("hddnCurrency"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnRelaNum"), eFunctions.Values.eTypeData.etdDouble, True), Session("sStatus"), Session("nUserCode"), Session("nCashNum"), .Form.Item("chkRentVital"), mobjValues.StringToType(.Form.Item("valCollector"), eFunctions.Values.eTypeData.etdDouble, True), .Form.Item("optRelOrigi"), mobjValues.StringToType(.Form.Item("tcdCollect"), eFunctions.Values.eTypeData.etdDate))
				If insPostSequence Then
					Session("dCollectDate") = mobjValues.StringToType(.Form.Item("tcdCollectDate"), eFunctions.Values.eTypeData.etdDate)
					Session("dCollect") = mobjValues.StringToType(.Form.Item("tcdCollect"), eFunctions.Values.eTypeData.etdDate)
					Session("sClient") = .Form.Item("dtcClient")
					Session("nBordereaux") = mobjValues.StringToType(mobjCollection.nBordereaux, eFunctions.Values.eTypeData.etdDouble)
					
					Session("sRelorigi") = .Form.Item("optRelOrigi")
					Session("CO001_nAction") = mobjValues.StringToType(.Form.Item("cbeAction"), eFunctions.Values.eTypeData.etdDouble)
					Session("nCurrencyCollect") = mobjValues.StringToType(.Form.Item("hddnCurrency"), eFunctions.Values.eTypeData.etdDouble, True)
					Session("sRel_Type") = .Form.Item("cbeRel_Type")
					Session("dValuedate") = ldtmValueDate
					Session("sStatus") = mobjCollection.sStatus
					Session("sType") = mobjCollection.sType
					Session("nInputtyp") = mobjValues.StringToType(.Form.Item("cbeInputTyp"), eFunctions.Values.eTypeData.etdDouble)
					Session("nBank") = mobjValues.StringToType(.Form.Item("cbeBank"), eFunctions.Values.eTypeData.etdDouble)
					Session("nBranch") = mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble)
					Session("nProduct") = mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble)
					Session("nPolicy") = mobjValues.StringToType(.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble)
					Session("nCertif") = mobjValues.StringToType(.Form.Item("tcnCertif"), eFunctions.Values.eTypeData.etdDouble)
					
					If .Form.Item("chkRentVital") = "1" Then
						lobjProduct = New eProduct.Product
						Session("chkRentVital") = lobjProduct.getProdClas(Session("nBranch"), Session("nProduct"), Session("dValuedate"))
						If CStr(Session("chkRentVital")) = "0" Then
							Session("chkRentVital") = "9"
						End If
						Session("nProdClas") = Session("chkRentVital")
						lobjProduct = Nothing
					Else
						lobjProduct = New eProduct.Product
						Session("chkRentVital") = "0"
						Session("nProdClas") = lobjProduct.getProdClas(Session("nBranch"), Session("nProduct"), Session("dValuedate"))
						lobjProduct = Nothing
					End If
					If mobjValues.StringToType(.Form.Item("valBank_Agree"), eFunctions.Values.eTypeData.etdDouble) <= 0 Then
						Session("nAgreement") = mobjValues.StringToType(.Form.Item("valAgreement"), eFunctions.Values.eTypeData.etdDouble)
					Else
						Session("nAgreement") = mobjValues.StringToType(.Form.Item("valBank_Agree"), eFunctions.Values.eTypeData.etdDouble)
					End If
					
					Session("blnCol_Agree") = False
					Session("nBranch") = mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble)
					Session("nProduct") = mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble)
					Session("nPolicy") = mobjValues.StringToType(.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble)
					Session("nCertif") = mobjValues.StringToType(.Form.Item("tcnCertif"), eFunctions.Values.eTypeData.etdDouble)
					Session("nOneTime") = 1
					Session("valAccountAgree") = vbNullString
				End If
			End With
			
			mstrQueryString = "&SlinkSpecial=" & Request.QueryString.Item("sLinkSpecial")
			
			If Session("CO001_nAction") = eCollection.ColformRef.TypeActionsSeqColl.cstrCut Then
				With Response
					.Write("<SCRIPT>")
					.Write("insReloadTop(true, false);")
					.Write("</" & "Script>")
				End With
			End If
			
		Case "CO001"
			mobjCollection = New eCollection.T_DocTyp
			'+ Si existen registros 
			With Request
				If .QueryString.Item("WindowType") = "PopUp" Then
					If mobjValues.StringToType(.Form.Item("cbeCollecDocTyp"), eFunctions.Values.eTypeData.etdDouble, True) = 2 Then
						ldblDocument = mobjValues.StringToType(.Form.GetValues("hddnReceipt").GetValue(1 - 1), eFunctions.Values.eTypeData.etdDouble, True)
					Else
						ldblDocument = mobjValues.StringToType(.Form.Item("tcnDocument"), eFunctions.Values.eTypeData.etdDouble, True)
					End If
					
					insPostSequence = mobjCollection.insPostCO001(.QueryString("WindowType"), "1", .QueryString("Action"), Session("nBordereaux"), mobjValues.StringToType(.Form.GetValues("hddnSequence").GetValue(1 - 1), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeCollecDocTyp"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnCertif"), eFunctions.Values.eTypeData.etdDouble), ldblDocument, .Form.Item("dtcClient"), mobjValues.StringToType(.Form.GetValues("hddnProponum_aux").GetValue(1 - 1), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.GetValues("hddnBulletins_aux").GetValue(1 - 1), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("nTypMove"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcdValuedate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.GetValues("hddnContrat").GetValue(1 - 1), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnDraft"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.GetValues("hddnType").GetValue(1 - 1), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.GetValues("hddnTratypei").GetValue(1 - 1), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeCurrency"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnExchange"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnAmountCol"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnAmountPay"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnInterest_rate"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.GetValues("hdddExpirDat").GetValue(1 - 1), eFunctions.Values.eTypeData.etdDate), Session("nUserCode"), mobjValues.StringToType(.Form.Item("tcdValuedate"), eFunctions.Values.eTypeData.etdDate), Session("chkRentVital"), mobjValues.StringToType(.Form.Item("tcnTax_discount"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnface_value"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcdIssuedate"), eFunctions.Values.eTypeData.etdDate, True), mobjValues.StringToType(.Form.Item("tcdExpirdate"), eFunctions.Values.eTypeData.etdDate, True), mobjValues.StringToType(.Form.Item("valOrigin"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcdOriginDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("valInstitution"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valCode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeTyp_Profit"), eFunctions.Values.eTypeData.etdInteger, True), .Form.Item("chkNewReceipt"))
				Else
                ' Ver si hacen referencia al .Form.Item("Sel").Length y que valor le llega
					insPostSequence = mobjCollection.insPostCO001(.QueryString("WindowType"), "1", .QueryString("Action"), Session("nBordereaux"), eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, vbNullString, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.dtmNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.dtmNull, Session("nUserCode"), Session("dValuedate"), Session("chkRentVital"), eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.dtmNull, eRemoteDB.Constants.dtmNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.dtmNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, vbNullString)
					If insPostSequence Then
						Session("Finan_Interest") = mobjCollection.nExists_Finan_Interest
					End If
				End If
			End With
			
		Case "CO823"
			mobjCollection = New eCollection.T_concepts
			'+ Si existen registros 
			With Request
				If .QueryString.Item("WindowType") = "PopUp" Then
					
					insPostSequence = mobjCollection.insPostCO823Upd(.QueryString("Action"), mobjValues.StringToType(.Form.Item("valConcept"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeCurrency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnAmountOrig"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnAmountLoc"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdValuedate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("valBank_agree"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valAccount_Agree"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valAgreement"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdCollect"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcnBulletins"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("dtcClient"), mobjValues.StringToType(.Form.Item("hddnProponum"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnClaim"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddCase_num"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valCurrAcc"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valIntermed"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valCompanyCR"), eFunctions.Values.eTypeData.etdDouble, True), Session("nCashNum"), Session("dCollectDate"), Session("nUsercode"), Session("nBordereaux"), mobjValues.StringToType(.Form.Item("nTransac"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnExchange"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddDeman_type"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnNotenum"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valLoans"), eFunctions.Values.eTypeData.etdDouble))
					If insPostSequence Then
						Session("valAccountAgree") = mobjValues.StringToType(.Form.Item("valAccount_Agree"), eFunctions.Values.eTypeData.etdDouble)
					End If
				Else
					insPostSequence = mobjCollection.insPostCO823(Session("nBordereaux"), mobjValues.StringToType(.Form.Item("nItems"), eFunctions.Values.eTypeData.etdDouble), Session("nUsercode"))
					Session("Finan_Interest") = "0"
				End If
			End With
			
		Case "CO008"
			mobjCollection = New eCollection.CashBankAccMov
			
			With Request
				If .QueryString.Item("WindowType") = "PopUp" Then
					insPostSequence = mobjCollection.insPostCO008Upd(.QueryString("sCodispl"), Session("nBordereaux"), mobjValues.StringToType(Session("dCollectDate"), eFunctions.Values.eTypeData.etdDate), .Form.Item("sType"), mobjValues.StringToType(.Form.Item("nSequence"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("nTypPay"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("dDoc_date"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("nBankAcc"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("nCurrency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnExchange"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("nAmount"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("nAmountDec"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnAmountLoc"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("nBank"), eFunctions.Values.eTypeData.etdDouble, True), .Form.Item("sDocNumber"), mobjValues.StringToType(.Form.Item("nTypCreCard"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("nIntermed"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("sClient"), mobjValues.StringToType(.Form.Item("nLed_compan"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("sAccount"), .Form.Item("sAux_accoun"), eRemoteDB.Constants.intNull, mobjValues.StringToType(.Form.Item("nTransac"), eFunctions.Values.eTypeData.etdDouble), Session("nCashNum"), mobjValues.StringToType(.Form.Item("tcnCashId"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("nChequeLocat"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), Session("nOffice"))
				Else
					insPostSequence = mobjCollection.insPostCO008(Session("nBordereaux"), mobjValues.StringToType(.Form.Item("nItems"), eFunctions.Values.eTypeData.etdDouble), Session("nUsercode"))
					
				End If
			End With
			mobjCollection = Nothing
			
			'+ Devoluciones        
		Case "CO010"
			mobjCollection = New eCollection.CashBankAccMov
			With Request
				If .QueryString.Item("WindowType") = "PopUp" Then
					insPostSequence = mobjCollection.insPostCO010Upd(.QueryString("sCodispl"), Session("nBordereaux"), mobjValues.StringToType(Session("dCollectDate"), eFunctions.Values.eTypeData.etdDate), .Form.Item("sType"), mobjValues.StringToType(.Form.Item("nSequence"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("nTypDev"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("nAccBankO"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("nCurrency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("nAmount"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnExchange"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnAmountLoc"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("sClient"), vbNullString, mobjValues.StringToType(.Form.Item("nBankDes"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("nBk_agency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("nTypAcc"), eFunctions.Values.eTypeData.etdDouble, True), .Form.Item("sAccBankD"))
				Else
					insPostSequence = mobjCollection.insPostCO010(Session("nBordereaux"), mobjValues.StringToType(.Form.Item("nItems"), eFunctions.Values.eTypeData.etdDouble))
				End If
			End With
			
		Case "CO012"
			mobjCollection = New eCollection.T_Move_acc
			
			With Request
				If .QueryString.Item("WindowType") = "PopUp" Then
					insPostSequence = mobjCollection.insPostCO012Upd(Session("nBordereaux"), mobjValues.StringToType(.Form.Item("nAmount"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("sClient"), mobjValues.StringToType(.Form.Item("nCurrency"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnExchange"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dCollectDate"), eFunctions.Values.eTypeData.etdDate, True), mobjValues.StringToType(.Form.Item("nSequence"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeDiferenceTyp"), eFunctions.Values.eTypeData.etdDouble, True), Session("nCashNum"))
				Else
					insPostSequence = mobjCollection.insPostCO012(Session("nBordereaux"), mobjValues.StringToType(.Form.Item("nItems"), eFunctions.Values.eTypeData.etdDouble))
				End If
			End With
			
			
		Case "GE101"
			mobjCollection = New eCollection.ColformRef
			
			'+ Se elimina la relación del sistema.            
			If Request.Form.Item("optElim") = "Delete" Then
				mobjCollection.Delete(Session("nBordereaux"), "DELETE")
			Else
				'+ Se atualiza el campo nUser_amend con cero para que la relación quede habilitada para ser modificada por otro usuario.
				mobjCollection.nBordereaux = Session("nBordereaux")
				mobjCollection.UpdateUserAmend(Session("nUsercode"))
			End If
			
			With Response
				.Write("<SCRIPT>")
				.Write("insReloadTop(true, true);")
				.Write("</" & "Script>")
			End With
			
	End Select
	mobjNetFrameWork.AfterPost(Request.QueryString.Item("sCodispl"))
End Function

'% insFinish: Se activa cuando la acción es finalizar
'--------------------------------------------------------------------------------------------
Function insFinish() As Object
	'--------------------------------------------------------------------------------------------
	Dim lclsQuery As eRemoteDB.Query
	If mobjCollection Is Nothing Then
		mobjCollection = New eCollection.ColformRef
	End If
	
        If Session("CO001_nAction") = 2 Then
            insPrintDocuments()
            Response.Write("<SCRIPT>insReloadTop(true, false);</" & "Script>")
        Else
            If mobjCollection.insPostFolder(Session("nBordereaux"), Session("nCashNum"), Session("nUserCode"), Session("nBranchOrd"), Session("nProductOrd"), Session("nReceiptOrd"), Session("CO001_nAction")) Then
		
                If mobjCollection.sStatus = "1" Then
                    insPrintDocuments()
                
                Else
                    lclsQuery = New eRemoteDB.Query
                    If lclsQuery.OpenQuery("Message", "sMessaged", "nErrornum = 3302") Then
                        Response.Write("<SCRIPT>alert('" & lclsQuery.FieldToClass("sMessaged") & " (" & mobjCollection.nAmountDif & " )" & "');</" & "Script>")
                    End If
                    lclsQuery = Nothing
                    '	        Response.Write "<NOTSCRIPT>alert('" & mobjCollection.nAmountDif & "');</" & "Script>"
                End If
		
                Response.Write("<SCRIPT>insReloadTop(true, false);</" & "Script>")
            End If
        End If
        mobjCollection = Nothing
    End Function
'-----------------------------------------------------------------------------------
Private Sub insPrintDocuments()
	'-----------------------------------------------------------------------------------
	Dim mobjDocuments As eReports.Report
	mobjNetFrameWork.BeginProcess("insPrintDocuments")
	mobjDocuments = New eReports.Report
	
	With mobjDocuments
		.ReportFilename = "COL022.rpt"
		.sCodispl = "CO001"
		.setStorProcParam(1, Session("nBordereaux"))
		Response.Write((.Command))
		
	End With
	
	'+ en caso de exitir data despliega el reporte de exdente tributario
	Dim mobjBath As eBatch.ValBatch
	mobjBath = New eBatch.ValBatch
	If mobjBath.valRepCol742 Then
		With mobjDocuments
			.Reset()
			.ReportFilename = "col742_exedente.rpt"
			.sCodispl = "COL742"
			Response.Write((.Command))
		End With
	End If
	mobjBath = Nothing
	
	mobjDocuments = Nothing
	mobjNetFrameWork.FinishProcess("insPrintDocuments")
End Sub

</script>
<%Response.Expires = -1
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("valCollectionSeq")

mstrCommand = "&sModule=Collection&sProject=CollectionSeq&sCodisplReload=" & Request.QueryString.Item("sCodispl")

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 3/4/03 12.00.01
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "valCollectionSeq"
%>
<HTML>
<HEAD>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></SCRIPT>





	
    <%=mobjValues.StyleSheet()%>
<SCRIPT>
//+ Variable para el control de versiones 
    document.VssVersion="$$Revision: 20 $|$$Date: 13/09/04 20:34 $|$$Author: Nvaplat40 $"
    
//% CancelErrors: función que retorna a la pagina anterior
//---------------------------------------------------------------------------------------------------
function CancelErrors(){
//---------------------------------------------------------------------------------------------------
    self.history.go(-1)}

//% NewLocation: función que posiciona en una pagina determinada dependiendo del codigo de la transacción
//---------------------------------------------------------------------------------------------------    
function NewLocation(Source,Codisp){
//---------------------------------------------------------------------------------------------------
    var lstrLocation = "";
    lstrLocation += Source.location;
    lstrLocation = lstrLocation.replace(/&OPENER=.*/,"") + "&OPENER=" + Codisp;
    Source.location = lstrLocation;
}
</SCRIPT>

</HEAD>
<%

'+ Si no se han validado los campos de la página
If Request.Form.Item("sCodisplReload") = vbNullString Then
	mstrErrors = insvalSequence
	Session("sErrorTable") = mstrErrors
	Session("sForm") = Request.Form.ToString
Else
	Session("sErrorTable") = vbNullString
	Session("sForm") = vbNullString
End If

If mstrErrors > vbNullString Then
	With Response
		.Write(mstrErrors)
		.Write("<SCRIPT LANGUAGE=JAVASCRIPT>")
		.Write("ShowPopUp(""/VTimeNet/Common/Errors.aspx?sSource=" & Server.URLEncode(mstrCommand) & "&sQueryString=" & Server.URLEncode(Request.Params.Get("Query_String")) & """, ""CollectionSeqErrors"",660,330);document.location.href='/VTimeNet/common/blank.htm';")
		.Write(mobjValues.StatusControl(False, CShort(Request.QueryString.Item("nZone")), Request.QueryString.Item("WindowType")))
		.Write("</SCRIPT>")
	End With
Else
	If Request.QueryString.Item("nAction") <> CStr(eFunctions.Menues.TypeActions.clngAcceptDataFinish) Then
		If insPostSequence Then
			If Request.QueryString.Item("WindowType") <> "PopUp" Then
				'+Si el código logico es CO001_k, se verifica si la accion es eliminar, para no mostrar la secuencia de ventanas
				
				If Request.QueryString.Item("sCodispl") = "CO001_K" Or Request.QueryString.Item("sCodispl") = "CO01_K" Then
					If CStr(mobjCollection.nAction) = CStr(eCollection.ColformRef.TypeActionsSeqColl.cstrCut) Then
						mstrLocationCO001_k = "'/VTimeNet/Common/secWHeader.aspx?sCodispl=" & Request.QueryString.Item("sCodispl") & "&sProject=CollectionSeq&sModule=Collection&nAction=" & Request.QueryString.Item("nMainAction") & "&nHeight=170'"
					Else
						mstrLocationCO001_k = "'/VTimeNet/Common/secWHeader.aspx?sCodispl=" & Request.QueryString.Item("sCodispl") & "&sProject=CollectionSeq&sModule=Collection&sConfig=InSequence&nAction=" & Request.QueryString.Item("nMainAction") & "&nHeight=170" & mstrQueryString & "'"
					End If
				Else
					mstrLocationCO001_k = "'/VTimeNet/Common/secWHeader.aspx?sCodispl=" & Request.QueryString.Item("sCodispl") & "&sProject=CollectionSeq&sModule=Collection&sConfig=InSequence&nAction=" & Request.QueryString.Item("nMainAction") & "&nHeight=170'"
				End If
				
				'+ Si se está tratando con un frame y no con la ventana principal de la secuencia, 
				'+ se mueve automaticamente a la siguiente página
				If Request.QueryString.Item("nZone") = "1" Then
					
					'+ Se carga nuevamente la ventana principal de la secuencia
					If Request.Form.Item("sCodisplReload") = vbNullString Then
						Response.Write("<SCRIPT>top.document.location=" & mstrLocationCO001_k & ";</SCRIPT>")
					Else
						Response.Write("<SCRIPT>opener.top.document.location=" & mstrLocationCO001_k & ";window.close();</SCRIPT>")
					End If
				Else
					If Request.Form.Item("sCodisplReload") = vbNullString Then
						Response.Write("<SCRIPT>top.frames['fraSequence'].document.location='/VTimeNet/Collection/CollectionSeq/Sequence.aspx?nAction=" & Request.QueryString.Item("nMainAction") & "&sGoToNext=YES&nOpener=" & Request.QueryString.Item("sCodispl") & "';</SCRIPT>")
					Else
						Response.Write("<SCRIPT>window.close();opener.top.frames['fraSequence'].document.location='/VTimeNet/Collection/CollectionSeq/Sequence.aspx?nAction=" & Request.QueryString.Item("nMainAction") & "&sGoToNext=YES&nOpener=" & Request.QueryString.Item("sCodispl") & "';</SCRIPT>")
					End If
				End If
			Else
				If Request.Form.Item("sCodisplReload") = vbNullString Then
					Response.Write("<SCRIPT>window.close();top.opener.top.frames['fraSequence'].document.location='/VTimeNet/Collection/CollectionSeq/Sequence.aspx?nAction=" & Request.QueryString.Item("nMainAction") & "&sGoToNext=NO&nOpener=" & Request.QueryString.Item("sCodispl") & "';</SCRIPT>")
				Else
                    If Request.QueryString.Item("sCodispl") = "CO001" Then 
                        Response.Write("<SCRIPT>window.close();top.opener.top.opener.document.location.href='" & Request.QueryString.Item("sCodispl") & ".aspx?sCodispl=" & Request.QueryString.Item("sCodispl") & "&Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&nMainAction=" & Request.QueryString.Item("nMainAction") & "&Type=Normal" & "&Index=" & Request.QueryString.Item("Index") & "&lsWay=" & Request.QueryString.Item("lsWay") & "&lsFirstRecord=" & Request.QueryString.Item("lsFirstRecord") & "'</SCRIPT>")
					Else
                        Response.Write("<SCRIPT>window.close();opener.top.opener.top.frames['fraSequence'].document.location='/VTimeNet/Collection/CollectionSeq/Sequence.aspx?nAction=" & Request.QueryString.Item("nMainAction") & "&sGoToNext=NO&nOpener=" & Request.QueryString.Item("sCodispl") & "';</SCRIPT>")
				    End if
                End If
				If Request.QueryString.Item("lsWay") = vbNullString Then
					Select Case Request.QueryString.Item("sCodispl")
						Case "CO001", "CO823", "CO008", "CO010", "CO012"
							If Request.Form.Item("sCodisplReload") = vbNullString Then
								Response.Write("<SCRIPT>top.opener.document.location.href='" & Request.QueryString.Item("sCodispl") & ".aspx?sCodispl=" & Request.QueryString.Item("sCodispl") & "&Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&nMainAction=" & Request.QueryString.Item("nMainAction") & "&Type=Normal" & "&Index=" & Request.QueryString.Item("Index") & "'</SCRIPT>")
							Else
								Response.Write("<SCRIPT>top.opener.top.opener.document.location.href='" & Request.QueryString.Item("sCodispl") & ".aspx?sCodispl=" & Request.QueryString.Item("sCodispl") & "&Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&nMainAction=" & Request.QueryString.Item("nMainAction") & "&Type=Normal" & "&Index=" & Request.QueryString.Item("Index") & "'</SCRIPT>")
							End If
					End Select
				Else
					Select Case Request.QueryString.Item("sCodispl")
						Case "CO001", "CO823", "CO008", "CO010", "CO012"
							If Request.Form.Item("sCodisplReload") = vbNullString Then
								Response.Write("<SCRIPT>top.opener.document.location.href='" & Request.QueryString.Item("sCodispl") & ".aspx?sCodispl=" & Request.QueryString.Item("sCodispl") & "&Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&nMainAction=" & Request.QueryString.Item("nMainAction") & "&Type=Normal" & "&Index=" & Request.QueryString.Item("Index") & "&lsWay=" & Request.QueryString.Item("lsWay") & "&lsFirstRecord=" & Request.QueryString.Item("lsFirstRecord") & "'</SCRIPT>")
							Else
								Response.Write("<SCRIPT>top.opener.top.opener.document.location.href='" & Request.QueryString.Item("sCodispl") & ".aspx?sCodispl=" & Request.QueryString.Item("sCodispl") & "&Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&nMainAction=" & Request.QueryString.Item("nMainAction") & "&Type=Normal" & "&Index=" & Request.QueryString.Item("Index") & "&lsWay=" & Request.QueryString.Item("lsWay") & "&lsFirstRecord=" & Request.QueryString.Item("lsFirstRecord") & "'</SCRIPT>")
							End If
					End Select
				End If
			End If
		End If
	Else
            If Session("bQuery") = True Then
                insPrintDocuments()
                Response.Write("<SCRIPT>insReloadTop(true, false);</SCRIPT>")
                'Response.Write("<SCRIPT>top.frames['fraSequence'].document.location='/VTimeNet/Collection/CollectionSeq/Sequence.aspx?nAction=" & Request.QueryString.Item("nMainAction") & "&sGoToNext=YES&nOpener=" & Request.QueryString.Item("sCodispl") & "';</SCRIPT>")

                'Response.Write("<SCRIPT>if (top.fraHeader.sLinkSpecial == '1') top.close(); else insReloadTop(true, false);</SCRIPT>")
            Else
                insFinish()
            End If
	End If
End If

mobjValues = Nothing
mobjCollection = Nothing

%>
</BODY>
</HTML>
<%'^Begin Footer Block VisualTimer Utility 1.1 3/4/03 12.00.01
Call mobjNetFrameWork.FinishPage("valCollectionSeq")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




