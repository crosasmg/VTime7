<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eCollection" %>
<%@ Import namespace="ePolicy" %>
<%@ Import namespace="eFinance" %>
<%@ Import namespace="eCashBank" %>
<%@ Import namespace="eClient" %>
<%@ Import namespace="eGeneral" %>
<%@ Import namespace="eReports" %>
<script language="VB" runat="Server">
'Dim insUpdSelCO700() As Object

Dim mobjValues As eFunctions.Values


    
'% insUpd_print: actualiza la temporal
'--------------------------------------------------------------------------------------------
Private Sub insUpd_print()
	'--------------------------------------------------------------------------------------------
	Dim lclsPremium As eCollection.Premium
	
	lclsPremium = New eCollection.Premium
	
	Call lclsPremium.insUpd_sPrint(Request.QueryString.Item("sKey"), mobjValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nReceipt"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nDraft"), eFunctions.Values.eTypeData.etdLong), Request.QueryString.Item("sPrint"))
	
	lclsPremium = Nothing
End Sub

'% insShowPolicy: se muestran los datos asociados al número de póliza.
'%                Se utiliza para el campo Póliza de la página CA001_K.aspx
'--------------------------------------------------------------------------------------------
Sub insShowPolicy()
	'--------------------------------------------------------------------------------------------
	Dim lclsPolicy_po As ePolicy.Policy
	
	lclsPolicy_po = New ePolicy.Policy
	
	If lclsPolicy_po.FindPolicybyPolicy("2", mobjValues.StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble)) Then
		Response.Write("top.frames['fraHeader'].document.forms[0].cbeAgency.value=" & lclsPolicy_po.nAgency & ";")
		Response.Write("top.frames['fraHeader'].document.forms[0].cbeBranch.value=" & lclsPolicy_po.nBranch & ";")
		Response.Write("top.frames['fraHeader'].document.forms[0].valProduct.disabled=false;")
		Response.Write("top.frames['fraHeader'].document.forms[0].btnvalProduct.disabled=false;")
		Response.Write("top.frames['fraHeader'].document.forms[0].valProduct.Parameters.Param1.sValue=" & lclsPolicy_po.nBranch & ";")
		Response.Write("top.frames['fraHeader'].document.forms[0].valProduct.value=" & lclsPolicy_po.nProduct & ";")
		Response.Write("top.frames['fraHeader'].$('#valProduct').change();")
	Else
		Response.Write("top.frames['fraHeader'].document.forms[0].cbeAgency.value=0;")
	End If
	
	lclsPolicy_po = Nothing
	
End Sub

'% insShowDataCO003: Muestra los datos de la transacción CO003.
'--------------------------------------------------------------------------------------------
Private Sub insShowDataCO003()
	Dim mbjValues As Object
	'--------------------------------------------------------------------------------------------
	Dim lclsDocument As eCollection.Premium
	
	Select Case Request.QueryString.Item("sField")
		Case "getBalance"
			lclsDocument = New eCollection.Premium
			
			If lclsDocument.Find("2", CDbl(Request.QueryString.Item("nReceipt")), 0, 0, 0, 0) Then
				Response.Write("top.fraHeader.document.forms[0].tcnPremium.value = " & lclsDocument.nBalance & ";")
			Else
				Response.Write("top.fraHeader.document.forms[0].tcnPremium.value = '';")
			End If
			
		Case "calAmountRate"
			lclsDocument = New eCollection.Premium
			With lclsDocument
				If .calCO003(mbjValues.StringToType(Request.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.QueryString.Item("dPayDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.QueryString.Item("nIntammou"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nRate"), eFunctions.Values.eTypeData.etdDouble)) Then
					Response.Write("top.frames['fraFolder'].document.forms[0].tcnRate.value='" & .nRatePay & "';")
					Response.Write("top.frames['fraFolder'].document.forms[0].tcnPremium.value='" & .nIntAmmouPay & "';")
				Else
					Response.Write("top.frames['fraFolder'].document.forms[0].tcnRate.value='';")
					Response.Write("top.frames['fraFolder'].document.forms[0].tcnPremium.value='';")
				End If
			End With
		Case "getPayNumbe"
			lclsDocument = New eCollection.Premium
			Response.Write("top.frames['fraFolder'].document.forms[0].tcnPaynumbe.value='" & lclsDocument.getMaxPayNumbe(mobjValues.TypeToString(Request.QueryString.Item("nReceipt"), eFunctions.Values.eTypeData.etdDouble)) & "';")
	End Select
	
	lclsDocument = Nothing
	
End Sub

'% insShowDataCO675: 
'--------------------------------------------------------------------------------------------
Private Sub insShowDataCO675()
	'--------------------------------------------------------------------------------------------
	Dim lclsPremium As eCollection.Premium
	Dim lclsFinanc_dra As eFinance.FinanceDraft
	
	lclsPremium = New eCollection.Premium
	
	With lclsPremium
		If .FindPremiumExist("2", mobjValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nReceipt"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(0), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(0), eFunctions.Values.eTypeData.etdDouble), eCollection.Premium.TypeNumeratorPOL_REC.cstrSysNumeGeneral) Then
			Response.Write("top.frames['fraHeader'].document.forms[0].tcnBranch.value = " & .nBranch & ";")
			Response.Write("top.frames['fraHeader'].document.forms[0].tcnProduct.value=" & .nProduct & ";")
			Response.Write("top.frames['fraHeader'].document.forms[0].tctPremiumExist.value=""True"";")
			Response.Write("top.frames['fraHeader'].document.forms[0].tcnStatusPre.value='" & .nStatus_pre & "';")
			Response.Write("top.frames['fraHeader'].document.forms[0].tcnWayPay.value=" & mobjValues.TypeToString(.nWay_Pay, eFunctions.Values.eTypeData.etdDouble) & ";")
			Response.Write("top.frames['fraHeader'].document.forms[0].tctDescWayPay.value='" & .sDescWay_Pay & "';")
			Response.Write("top.frames['fraHeader'].document.forms[0].tcnDigit.value=" & .nDigit & ";")
			Response.Write("top.frames['fraHeader'].document.forms[0].tcnPaynumbe.value=" & .nPaynumbe & ";")
			Response.Write("top.frames['fraHeader'].document.forms[0].tcnBulletins.value=" & .nBulletins & ";")
			Response.Write("top.frames['fraHeader'].document.forms[0].tcdLimitDate.value='" & mobjValues.TypeToString(.dLimitdate, eFunctions.Values.eTypeData.etdDate) & "';")
			Response.Write("top.frames['fraHeader'].document.forms[0].btnShowSCO001.disabled=false;")
			Response.Write("top.frames['fraHeader'].document.forms[0].hddnType.value=" & .nType & ";")
			Response.Write("top.frames['fraHeader'].document.forms[0].tcnContrat.value=" & .nContrat & ";")
			Response.Write("top.frames['fraHeader'].document.forms[0].valDraft.Parameters.Param1.sValue=" & .nContrat & ";")
		Else
			Call insShowBlankCO675()
		End If
	End With
	
	If lclsPremium.nContrat > 0 Then
		lclsFinanc_dra = New eFinance.FinanceDraft
		If lclsFinanc_dra.Find_Financ_CO675(lclsPremium.nContrat) Then
			Response.Write("top.frames['fraHeader'].document.forms[0].valDraft.value='" & mobjValues.TypeToString(lclsFinanc_dra.nDraft, eFunctions.Values.eTypeData.etdDouble) & "';")
			Response.Write("top.frames['fraHeader'].document.forms[0].valDraft.disabled=false;")
			Response.Write("top.frames['fraHeader'].document.forms[0].btnvalDraft.disabled=false;")
		End If
		lclsFinanc_dra = Nothing
	Else
		Response.Write("top.frames['fraHeader'].document.forms[0].tcnContrat.value='';")
		Response.Write("top.frames['fraHeader'].document.forms[0].valDraft.value='';")
	End If
	
	lclsPremium = Nothing
End Sub

'% insShowDataCO009: 
'--------------------------------------------------------------------------------------------
Private Sub insShowDataCO009()
	'--------------------------------------------------------------------------------------------
	Dim lclsPremium As eCollection.Premium
	Dim lclsPremium_mo As eCollection.Premium_mo
	Dim lclsFinanc_dra As eFinance.FinanceDraft
	Dim lclsColFormRef As eCollection.ColformRef
    Dim lclsPolicy As ePolicy.Policy
    Dim lclsAgreement_al As eBranches.Agreement_al
	Dim lblnFind As Boolean
	Dim ldblBordereaux As Double
	Dim lblnRelation As Boolean
	Dim lintCod_Agree As String
	Dim ldblPremium As Double
	Dim ldblCurrency As Integer
	
	lblnFind = False
	lblnRelation = False
	
	If mobjValues.StringToType(Request.QueryString.Item("nBordereaux"), eFunctions.Values.eTypeData.etdDouble) > 0 Then
		ldblBordereaux = mobjValues.StringToType(Request.QueryString.Item("nBordereaux"), eFunctions.Values.eTypeData.etdDouble)
		lblnFind = True
		lblnRelation = True
	End If
	'+Si no es una relacion se busca los datos del recibo	
	If Not lblnRelation Then
		'+ Si el valor del campo contrato pasado por el querystring no tiene valor
		If mobjValues.StringToType(Request.QueryString.Item("nContrat"), eFunctions.Values.eTypeData.etdDouble) <= 0 Then
			
			'+ Si el campo recibo tiene valor se busca sino se borran los campos
			
			If mobjValues.StringToType(Request.QueryString.Item("nReceipt"), eFunctions.Values.eTypeData.etdDouble) > 0 Then
				lclsPremium = New eCollection.Premium
				
				Response.Write("top.frames['fraHeader'].document.forms[0].tcnBordereaux.disabled=true;")
				With lclsPremium
					If .Findco009("2", 0, 0, mobjValues.StringToType(Request.QueryString.Item("nReceipt"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(0), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(0), eFunctions.Values.eTypeData.etdDouble), Session("sReceiptNum")) Then
						lblnFind = True
						If mobjValues.StringToType(Request.QueryString.Item("nDraft"), eFunctions.Values.eTypeData.etdDouble) < 0 Then
							Response.Write("top.frames['fraHeader'].document.forms[0].tcnStatusPre.value='" & .nStatus_pre & "';")
							Response.Write("top.frames['fraHeader'].document.forms[0].cbeBranch.value='" & .nBranch & "';")
							Response.Write("top.frames['fraHeader'].document.forms[0].valProduct.value='" & .nProduct & "';")
							Response.Write("top.frames['fraHeader'].document.forms[0].tcnContrat.value='" & .nContrat & "';")
							Response.Write("top.frames['fraHeader'].document.forms[0].hddClient.value='" & .sClient & "';")
							Response.Write("top.frames['fraHeader'].document.forms[0].tcnCurrency.value='" & .nCurrency & "';")
							Response.Write("top.frames['fraHeader'].document.forms[0].hddOffice.value='" & .nOffice & "';")
							Response.Write("top.frames['fraHeader'].document.forms[0].hddRel_amoun.value='" & .nPremium & "';")
							Response.Write("top.frames['fraHeader'].document.forms[0].hddnBranch.value='" & .nBranch & "';")
							Response.Write("top.frames['fraHeader'].document.forms[0].hddnProduct.value='" & .nProduct & "';")
                                Response.Write("top.frames['fraHeader'].document.forms[0].hddnPolicy.value='" & .nPolicy & "';")
                                Response.Write("top.frames['fraHeader'].document.forms[0].hddnCertif.value='" & .nCertif & "';")
							
							'para saber si se toma el monto y la moneda del recibo o el de la relacion
							ldblPremium = .nPremium
							ldblCurrency = .nCurrency
							
							Response.Write("top.frames['fraHeader'].UpdateDiv('lblWay_pay','" & .sDescWay_Pay & "');")
							Response.Write("top.frames['fraHeader'].UpdateDiv('lblStatus','" & .sDesStatus_pre & "');")
							Response.Write("top.frames['fraHeader'].UpdateDiv('lblBranch','" & .sDesBranch & "');")
							Response.Write("top.frames['fraHeader'].UpdateDiv('lblProduct','" & .sDesProduct & "');")
							Response.Write("top.frames['fraHeader'].UpdateDiv('lblCurrency','" & .sDesCurrency & "');")
							Response.Write("top.frames['fraHeader'].UpdateDiv('lblOffice','" & .sDesOffice & "');")
							Response.Write("top.frames['fraHeader'].UpdateDiv('lblPolicy','" & .nPolicy & "');")
						End If
					End If
				End With
				lclsPremium = Nothing
			Else
				Response.Write("top.frames['fraHeader'].document.forms[0].tcnBordereaux.disabled=false;")
			End If
		Else
			
			'+ Si el valor del campo contrato pasado por el querystring tiene valor
			If mobjValues.StringToType(Request.QueryString.Item("nContrat"), eFunctions.Values.eTypeData.etdDouble) > 0 Then
				lclsFinanc_dra = New eFinance.FinanceDraft
				
				With lclsFinanc_dra
					If .Find(mobjValues.StringToType(Request.QueryString.Item("nContrat"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nDraft"), eFunctions.Values.eTypeData.etdDouble)) Then
						
						lblnFind = True
						Response.Write("top.frames['fraHeader'].document.forms[0].tcnStatusPre.value='" & .nStat_draft & "';")
						Response.Write("top.frames['fraHeader'].UpdateDiv('lblStatus','" & .sDesStatusDraft & "');")
						Response.Write("top.frames['fraHeader'].UpdateDiv('lblCurrency','" & .sDesCurrency & "');")
						Response.Write("top.frames['fraHeader'].UpdateDiv('lblOffice','" & .sDesOffice & "');")
					End If
				End With
				lclsFinanc_dra = Nothing
			End If
		End If
	End If
	
	If lblnFind Then
		
		'+se habilita el boton de verificacion del recibo
		Response.Write("top.frames['fraHeader'].document.forms[0].btnShowSCO001.disabled=false;")
		
		'+ Si el tratamineto corresponde a un documento (Recibo/cuota) y no a una relación
		If Not lblnRelation Then
			lclsPremium_mo = New eCollection.Premium_mo
			
			'+ Se obtiene el número de la última relación asociado al documento.
			If lclsPremium_mo.Find_LastnBordereaux(vbNullString, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, mobjValues.StringToType(Request.QueryString.Item("nReceipt"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.QueryString.Item("nContrat"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.QueryString.Item("nDraft"), eFunctions.Values.eTypeData.etdDouble, True), eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull) Then
				ldblBordereaux = mobjValues.StringToType(CStr(lclsPremium_mo.nBordereaux), eFunctions.Values.eTypeData.etdDouble, True)
			End If
		End If
		'+ Si existe una relación asociada al recibo.
		If ldblBordereaux > 0 Then
			lclsColFormRef = New eCollection.ColformRef
            lclsPremium_mo = New eCollection.Premium_mo
            lclsAgreement_al = New eBranches.Agreement_al
			
			Response.Write("top.frames['fraHeader'].document.forms[0].tcnBordereaux.value='" & ldblBordereaux & "';")
			With lclsColFormRef
				If .findColFormRef(ldblBordereaux) Then
					If mobjValues.StringToType(Request.QueryString.Item("nBordereaux"), eFunctions.Values.eTypeData.etdDouble) > 0 Then
						Response.Write("top.frames['fraHeader'].document.forms[0].tcnReceiptNum.disabled=true;")
						Response.Write("top.frames['fraHeader'].document.forms[0].btnShowSCO001.disabled=true;")
					End If
					    '********************************CONTROL DE OPCIONES DE REVERSO DE PAGO'********************************
                        If lclsPremium_mo.Find_Nmodulec(.nBranch, .nProduct, .nPolicy, .nCertif) Then
                            If lclsPremium_mo.sBrancht = "6" Then
                                lclsPolicy = New ePolicy.Policy
                                '+ Se hace el llamo al "Find" de policy para obtener la Sucursal, Agencia y Oficina de la póliza.
                                Call lclsPolicy.Find("2", .nBranch, .nProduct, .nPolicy, True)
                                If lclsAgreement_al.Find(lclsPolicy.nAgreement) Then
                                    If lclsAgreement_al.nAgree_Type = 1 Then '******** TIPO DE ACUERDO ES ELECTRóNICO '********
                                        If lclsPremium_mo.Find_nType_Mov(.nBordereaux) Then
                                            If lclsPremium_mo.nMov_Type_aux = 15 Or lclsPremium_mo.nMov_Type_aux = 3 Then '********CONCILIACIÓN, DEPOSITO BANCARIO'********
                                                Response.Write("top.frames['fraHeader'].document.forms[0].optTypOper[0].checked=true;")
                                                Response.Write("top.frames['fraHeader'].document.forms[0].optTypOper[0].disabled=false;")
                                                Response.Write("top.frames['fraHeader'].document.forms[0].optTypOper[1].checked=false;")
                                                Response.Write("top.frames['fraHeader'].document.forms[0].optTypOper[1].disabled=true;")
                                                Response.Write("top.frames['fraHeader'].document.forms[0].optTypOper[2].checked=false;")
                                                Response.Write("top.frames['fraHeader'].document.forms[0].optTypOper[2].disabled=true;")
                                                Response.Write("top.frames['fraHeader'].document.forms[0].optTypOper[3].checked=false;")
                                                Response.Write("top.frames['fraHeader'].document.forms[0].optTypOper[3].disabled=false;")
                                            Else '********OTROS RELACIONES'********
                                                If lclsPremium_mo.Find_nIntermedia(.nBranch, .nProduct, .nPolicy, mobjValues.StringToType(Request.QueryString.Item("nReceipt"), eFunctions.Values.eTypeData.etdDouble)) Then
                                                    If lclsPremium_mo.nIntermedia_aux = 0 Then
                                                        Response.Write("top.frames['fraHeader'].document.forms[0].optTypOper[0].checked=true;")
                                                        Response.Write("top.frames['fraHeader'].document.forms[0].optTypOper[0].disabled=false;")
                                                        Response.Write("top.frames['fraHeader'].document.forms[0].optTypOper[1].checked=false;")
                                                        Response.Write("top.frames['fraHeader'].document.forms[0].optTypOper[1].disabled=true;")
                                                        Response.Write("top.frames['fraHeader'].document.forms[0].optTypOper[2].checked=false;")
                                                        Response.Write("top.frames['fraHeader'].document.forms[0].optTypOper[2].disabled=true;")
                                                        Response.Write("top.frames['fraHeader'].document.forms[0].optTypOper[3].checked=false;")
                                                        Response.Write("top.frames['fraHeader'].document.forms[0].optTypOper[3].disabled=false;")
                                                    Else
                                                        Response.Write("top.frames['fraHeader'].document.forms[0].optTypOper[0].checked=true;")
                                                        Response.Write("top.frames['fraHeader'].document.forms[0].optTypOper[0].disabled=false;")
                                                        Response.Write("top.frames['fraHeader'].document.forms[0].optTypOper[1].checked=false;")
                                                        Response.Write("top.frames['fraHeader'].document.forms[0].optTypOper[1].disabled=true;")
                                                        Response.Write("top.frames['fraHeader'].document.forms[0].optTypOper[2].checked=false;")
                                                        Response.Write("top.frames['fraHeader'].document.forms[0].optTypOper[2].disabled=true;")
                                                        Response.Write("top.frames['fraHeader'].document.forms[0].optTypOper[3].checked=false;")
                                                        Response.Write("top.frames['fraHeader'].document.forms[0].optTypOper[3].disabled=false;")
                                                    End If
                                                End If
                                            End If
                                        End If
                                    ElseIf lclsAgreement_al.nAgree_Type = 2 Then '******** TIPO DE ACUERDO MANUAL '********
                                        Response.Write("top.frames['fraHeader'].document.forms[0].optTypOper[0].checked=false;")
                                        Response.Write("top.frames['fraHeader'].document.forms[0].optTypOper[0].disabled=false;")
                                        Response.Write("top.frames['fraHeader'].document.forms[0].optTypOper[1].checked=false;")
                                        Response.Write("top.frames['fraHeader'].document.forms[0].optTypOper[1].disabled=true;")
                                        Response.Write("top.frames['fraHeader'].document.forms[0].optTypOper[2].checked=true;")
                                        Response.Write("top.frames['fraHeader'].document.forms[0].optTypOper[2].disabled=false;")
                                        Response.Write("top.frames['fraHeader'].document.forms[0].optTypOper[3].checked=false;")
                                        Response.Write("top.frames['fraHeader'].document.forms[0].optTypOper[3].disabled=false;")
                                    End If
                                End If
                                
                            Else '********OTROS RAMOS'********
                                If lclsPremium_mo.Find_nIntermedia(.nBranch, .nProduct, .nPolicy, mobjValues.StringToType(Request.QueryString.Item("nReceipt"), eFunctions.Values.eTypeData.etdDouble)) = 0 Then
                                    Response.Write("top.frames['fraHeader'].document.forms[0].optTypOper[0].checked=false;")
                                    Response.Write("top.frames['fraHeader'].document.forms[0].optTypOper[0].disabled=false;")
                                    Response.Write("top.frames['fraHeader'].document.forms[0].optTypOper[1].checked=false;")
                                    Response.Write("top.frames['fraHeader'].document.forms[0].optTypOper[1].disabled=false;")
                                    Response.Write("top.frames['fraHeader'].document.forms[0].optTypOper[2].checked=false;")
                                    Response.Write("top.frames['fraHeader'].document.forms[0].optTypOper[2].disabled=true;")
                                Else
                                    Response.Write("top.frames['fraHeader'].document.forms[0].optTypOper[0].checked=false;")
                                    Response.Write("top.frames['fraHeader'].document.forms[0].optTypOper[0].disabled=false;")
                                    Response.Write("top.frames['fraHeader'].document.forms[0].optTypOper[1].checked=false;")
                                    Response.Write("top.frames['fraHeader'].document.forms[0].optTypOper[1].disabled=false;")
                                    Response.Write("top.frames['fraHeader'].document.forms[0].optTypOper[2].checked=false;")
                                    Response.Write("top.frames['fraHeader'].document.forms[0].optTypOper[2].disabled=false;")
                                End If
                            End If
                            End If
                            ' si es PAC/TRANSBANK o Descuento por planilla 	
					
                            If CDbl(.sRel_Type) = 6 Or CDbl(.sRel_Type) = 1 Then
                                Response.Write("top.frames['fraHeader'].document.forms[0].optTypOper[0].checked=false;")
                                Response.Write("top.frames['fraHeader'].document.forms[0].optTypOper[1].checked=true;")
                                Response.Write("top.frames['fraHeader'].document.forms[0].chkRelAll.disabled=false;")
                                Response.Write("top.frames['fraHeader'].document.forms[0].chkRelAll.checked=false;")
                                Response.Write("top.frames['fraHeader'].document.forms[0].tcdDateIncrease.disabled=false;")
                                Response.Write("top.frames['fraHeader'].document.forms[0].btn_tcdDateIncrease.disabled=false;")
                                Response.Write("top.frames['fraHeader'].document.forms[0].tcdDateIncrease.value='" & Today & "';")
                            Else
                                Response.Write("top.frames['fraHeader'].document.forms[0].optTypOper[0].checked=false;")
						
                                '+ si la relacion es Manual entonces se desabilita el boton de ingreso a cta. cte.
                                If CDbl(.sRel_Type) = 5 Then
                                    Response.Write("top.frames['fraHeader'].document.forms[0].optTypOper[1].checked=false;")
                                Else
                                    Response.Write("top.frames['fraHeader'].document.forms[0].optTypOper[1].checked=false;")
                                End If
                                Response.Write("top.frames['fraHeader'].document.forms[0].tcdDateIncrease.disabled=true;")
                                Response.Write("top.frames['fraHeader'].document.forms[0].btn_tcdDateIncrease.disabled=true;")
                                Response.Write("top.frames['fraHeader'].document.forms[0].tcdDateIncrease.value='';")
                            End If
					
                            '+Se asigna monto de la relacion si solo si no se esta reversando un solo recibo
                            If ldblPremium <= 0 Then
                                Response.Write("top.frames['fraHeader'].document.forms[0].hddRel_amoun.value='" & .nRel_amoun & "';")
                            End If
					
                            '+se muestra el convenio asociado a la relacion
                            lclsPremium_mo = New eCollection.Premium_mo
					
                            lintCod_Agree = lclsPremium_mo.ShowDefValuesCo09(ldblBordereaux)
					
                            If lintCod_Agree <> vbNullString Then
                                Response.Write("top.frames['fraHeader'].UpdateDiv('lblAgreement','" & lintCod_Agree & "');")
                            Else
                                Response.Write("top.frames['fraHeader'].UpdateDiv('lblAgreement','');")
                            End If
					
                            '+ Si el proceso es por relación entonces asignará como cliente el de la relación de lo contrario 
                            '+ tomará el del recibo
                            If lblnRelation Then
                                If .sClient <> vbNullString Then
                                    Response.Write("top.frames['fraHeader'].document.forms[0].hddClient.value='" & .sClient & "';")
                                Else
                                    '+ Si el cliente de la relación es nulo no se puede reversar a cuenta corriente (Transbank)                        
                                    Response.Write("top.frames['fraHeader'].document.forms[0].optTypOper[0].checked=true;")
                                    Response.Write("top.frames['fraHeader'].document.forms[0].tcdDateIncrease.disabled=false;")
                                    Response.Write("top.frames['fraHeader'].document.forms[0].btn_tcdDateIncrease.disabled=false;")
                                    Response.Write("top.frames['fraHeader'].document.forms[0].tcdDateIncrease.value='" & Today & "';")
                                End If
                                Response.Write("top.frames['fraHeader'].document.forms[0].chkRelAll.checked=true;")
                                Response.Write("top.frames['fraHeader'].document.forms[0].chkRelAll.disabled=true;")
                            End If
					
                            If .nCurrency > 0 And ldblCurrency <= 0 Then
                                Response.Write("top.frames['fraHeader'].document.forms[0].tcnCurrency.value='" & .nCurrency & "';")
                            End If
					
                            If .nBranch > 0 Then
                                Response.Write("top.frames['fraHeader'].document.forms[0].hddnBranch.value='" & .nBranch & "';")
                            Else
                                Response.Write("top.frames['fraHeader'].document.forms[0].hddnBranch.value='';")
                            End If
					
                            If .nProduct > 0 Then
                                Response.Write("top.frames['fraHeader'].document.forms[0].hddnProduct.value='" & .nProduct & "';")
                            Else
                                Response.Write("top.frames['fraHeader'].document.forms[0].hddnProduct.value='';")
                            End If
					
                            If .nPolicy > 0 Then
                                Response.Write("top.frames['fraHeader'].document.forms[0].hddnPolicy.value='" & .nPolicy & "';")
                            Else
                                Response.Write("top.frames['fraHeader'].document.forms[0].hddnPolicy.value='';")
                            End If
					
					
                            lclsPolicy = New ePolicy.Policy
                            '+ Se hace el llamo al "Find" de policy para obtener la Sucursal, Agencia y Oficina de la póliza.
                            Call lclsPolicy.Find("2", lclsColFormRef.nBranch, lclsColFormRef.nProduct, lclsColFormRef.nPolicy, True)
                            If lclsPolicy.nOffice > 0 Then
                                Response.Write("top.frames['fraHeader'].document.forms[0].hddOffice.value='" & lclsPolicy.nOffice & "';")
                            End If
                            If lclsPolicy.nOfficeAgen > 0 Then
                                Response.Write("top.frames['fraHeader'].document.forms[0].hddOfficeAgen.value='" & lclsPolicy.nOfficeAgen & "';")
                            End If
                            If lclsPolicy.nAgency > 0 Then
                                Response.Write("top.frames['fraHeader'].document.forms[0].hddAgency.value='" & lclsPolicy.nAgency & "';")
                            End If
                            lclsPolicy = Nothing
                        Else
                            Response.Write("top.frames['fraHeader'].document.forms[0].tcnBordereaux.value='';")
                        End If
			End With
			lclsColFormRef = Nothing
		Else
			
			Response.Write("top.frames['fraHeader'].document.forms[0].tcnReceiptNum.disabled=false;")
			Response.Write("top.frames['fraHeader'].document.forms[0].btnShowSCO001.disabled=false;")
			Response.Write("top.frames['fraHeader'].document.forms[0].tcnBordereaux.value='';")
		End If
		lclsPremium_mo = Nothing
	Else
		Response.Write("top.frames['fraHeader'].document.forms[0].btnShowSCO001.disabled=true;")
		Response.Write("top.frames['fraHeader'].document.forms[0].tcnReceiptNum.disabled=false;")
		Response.Write("top.frames['fraHeader'].document.forms[0].tcnBordereaux.disabled=false;")
		
		If mobjValues.StringToType(Request.QueryString.Item("nDraft"), eFunctions.Values.eTypeData.etdDouble) < 0 Then
			Response.Write("top.frames['fraHeader'].document.forms[0].tcnContrat.value='';")
			Response.Write("top.frames['fraHeader'].document.forms[0].cbeBranch.value='';")
			Response.Write("top.frames['fraHeader'].document.forms[0].valProduct.value='';")
			Response.Write("top.frames['fraHeader'].document.forms[0].tcnBordereaux.value='';")
			Response.Write("top.frames['fraHeader'].UpdateDiv('lblWay_pay','');")
			Response.Write("top.frames['fraHeader'].UpdateDiv('lblStatus','');")
			Response.Write("top.frames['fraHeader'].UpdateDiv('lblCurrency','');")
			Response.Write("top.frames['fraHeader'].UpdateDiv('lblOffice','');")
			Response.Write("top.frames['fraHeader'].UpdateDiv('lblPolicy','');")
		End If
	End If
	
	If mobjValues.StringToType(Session("nCashNum"), eFunctions.Values.eTypeData.etdDouble, True) > 0 Then
		Response.Write("top.frames['fraHeader'].document.forms[0].optTypOper[0].checked=false;")
	End If
End Sub

'% insShowBlank: 
'--------------------------------------------------------------------------------------------
Private Sub insShowBlankCO009()
	'--------------------------------------------------------------------------------------------
	Response.Write("opener.document.forms[0].tctPremiumExist.value=""False"";")
	Response.Write("opener.document.forms[0].tcnReceiptNum.value=""0"";")
	Response.Write("opener.document.forms[0].cbeBranch.value="""";")
	Response.Write("opener.document.forms[0].valProduct.value="""";")
	
	Response.Write("opener.document.forms[0].valProduct.Parameters.Param1.sValue=opener.document.forms[0].cbeBranch.value;")
	Response.Write("opener.document.forms[0].valProduct.disabled=false;")
	Response.Write("opener.document.forms[0].valProduct.focus();")
	Response.Write("opener.$('#valProduct').change();")
	Response.Write("opener.document.forms[0].valProduct.disabled=true;")
	
	Response.Write("opener.document.forms[0].tctDescWayPay.value="""";")
	Response.Write("opener.document.forms[0].tcnAgreement.value="""";")
	Response.Write("opener.document.forms[0].tcnWayPay.value="""";")
	Response.Write("opener.document.forms[0].tcnBordereaux.value="""";")
	Response.Write("opener.document.forms[0].tcnBordereaux.disabled=false;")
	Response.Write("UpdateDiv('lblCurrency','" & " " & "','PopUp');")
	Response.Write("UpdateDiv('lblOffice','" & " " & "','PopUp');")
End Sub

'% insShowBlankCO675: 
'--------------------------------------------------------------------------------------------
Private Sub insShowBlankCO675()
	'--------------------------------------------------------------------------------------------
	Response.Write("top.frames['fraHeader'].document.forms[0].tctPremiumExist.value=""False"";")
	Response.Write("top.frames['fraHeader'].document.forms[0].tcnBranch.value="""";")
	Response.Write("top.frames['fraHeader'].document.forms[0].tcnProduct.value="""";")
	Response.Write("top.frames['fraHeader'].document.forms[0].tctDescWayPay.value="""";")
	Response.Write("top.frames['fraHeader'].document.forms[0].tcnDigit.value="""";")
	Response.Write("top.frames['fraHeader'].document.forms[0].tcnWayPay.value="""";")
	Response.Write("top.frames['fraHeader'].document.forms[0].tcnPaynumbe.value="""";")
	Response.Write("top.frames['fraHeader'].document.forms[0].tcnBulletins.value="""";")
	Response.Write("top.frames['fraHeader'].document.forms[0].tcnGeneralNumerator.value="""";")
	Response.Write("top.frames['fraHeader'].document.forms[0].tcnStatusPre.value="""";")
	Response.Write("top.frames['fraHeader'].document.forms[0].tcdLimitDate.value='" & " " & "';")
End Sub

'% insShowDataCO634: Obtiene la información de los documentos de la transacción de Traspaso de pago. 
'-------------------------------------------------------------------------------------------- 
Private Sub insShowDataCO634()
	'-------------------------------------------------------------------------------------------- 
	Dim lobjValues As eFunctions.Values
	Dim lobjDocument As Object
	Dim lobjPremium_mo As eCollection.Premium_mo
	Dim lObjFinanc_dra As eFinance.FinanceDraft
	
	lobjValues = New eFunctions.Values
	
	Response.Write("with (top.frames['fraHeader'].document.forms[0]){")
	
	Select Case Request.QueryString.Item("sDocument")
		
		Case "Proponum"
			lobjDocument = New eCashBank.Move_acc
			With lobjDocument
				If Request.QueryString.Item("sTypDocument") = "Ori" Then
					If .Find_nProponum_o(lobjValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), lobjValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), lobjValues.StringToType(Request.QueryString.Item("nProponum"), eFunctions.Values.eTypeData.etdDouble)) Then
						Response.Write("cbeCurrencyOri.value='" & lobjValues.TypeToString(.nCurrency, eFunctions.Values.eTypeData.etdInteger) & "';")
						Response.Write("tcnAmountOri.value='" & lobjValues.TypeToString(.nAmount, eFunctions.Values.eTypeData.etdDouble, True, 6) & "';")
						Response.Write("tcnPolicyOri.value='" & lobjValues.TypeToString(.nPolicy, eFunctions.Values.eTypeData.etdDouble) & "';")
						Response.Write("tcnAmountTrasOri.value='" & lobjValues.TypeToString(.nAmount, eFunctions.Values.eTypeData.etdDouble, True, 6) & "';")
						Response.Write("cbeCurrencyDes.value='" & lobjValues.TypeToString(.nCurrency, eFunctions.Values.eTypeData.etdInteger) & "';")
						Response.Write("tcnAmountDes.value='" & lobjValues.TypeToString(.nAmount, eFunctions.Values.eTypeData.etdDouble, True, 6) & "';")
					Else
						Response.Write("cbeCurrencyOri.value='';")
						Response.Write("tcnAmountOri.value='';")
						Response.Write("tcnAmountTrasOri.value='';")
						Response.Write("cbeCurrencyDes.value='';")
						Response.Write("tcnAmountDes.value='';")
					End If
				End If
			End With
			
		Case "Receipt"
			lobjDocument = New eCollection.Premium
			With lobjDocument
				If .Find("2", lobjValues.StringToType(Request.QueryString.Item("nReceipt"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, 0, 0) Then
					If Request.QueryString.Item("sTypDocument") = "Ori" Then
						Response.Write("cbeBranchOri.value='" & lobjValues.TypeToString(.nBranch, eFunctions.Values.eTypeData.etdInteger) & "';")
						Response.Write("valProductOri.Parameters.Param1.sValue=cbeBranchOri.value;")
						Response.Write("valProductOri.value='" & lobjValues.TypeToString(.nProduct, eFunctions.Values.eTypeData.etdInteger) & "';")
						Response.Write("top.frames['fraHeader'].$('#valProductOri').change();")
						Response.Write("tcnPolicyOri.value='" & lobjValues.TypeToString(.nPolicy, eFunctions.Values.eTypeData.etdDouble) & "';")
						If mobjValues.StringToType(.nContrat, eFunctions.Values.eTypeData.etdDouble, True) <> eRemoteDB.Constants.intNull Then
							Response.Write("tcnAmountTrasOri.disabled = true;")
							'+ Si es Financiado se obtiene el monto y la moneda de la última cuota pagada	
							lObjFinanc_dra = New eFinance.FinanceDraft
							If lObjFinanc_dra.Find_Co634(lobjDocument.nContrat, 2) Then
								Response.Write("top.frames['fraHeader'].ShowDiv('DivlblDraftOri', 'show');")
								Response.Write("top.frames['fraHeader'].ShowDiv('DivDraftOri', 'show');")
								Response.Write("top.frames['fraHeader'].ShowDiv('DivlblDraftDes', 'show');")
								Response.Write("top.frames['fraHeader'].ShowDiv('DivDraftDes', 'show');")
								Response.Write("hddnContratOri.value='" & lobjValues.TypeToString(lobjDocument.nContrat, eFunctions.Values.eTypeData.etdDouble) & "';")
								Response.Write("cbeCurrencyOri.value='" & lobjValues.TypeToString(lObjFinanc_dra.nCurrency, eFunctions.Values.eTypeData.etdDouble) & "';")
								Response.Write("tcnDraftOri.value='" & lobjValues.TypeToString(lObjFinanc_dra.nDraft, eFunctions.Values.eTypeData.etdDouble) & "';")
								Response.Write("tcnAmountOri.value='" & lobjValues.TypeToString(lObjFinanc_dra.nAmount, eFunctions.Values.eTypeData.etdDouble, True, 6) & "';")
								Response.Write("tcnAmountTrasOri.value='" & lobjValues.TypeToString(lObjFinanc_dra.nAmount, eFunctions.Values.eTypeData.etdDouble, True, 6) & "';")
								Response.Write("tcnInt_moraTrasOri.value=0;")
							Else
								Response.Write("hddnContratOri.value='';")
								Response.Write("cbeCurrencyOri.value='';")
								Response.Write("tcnDraftOri.value='';")
								Response.Write("tcnAmountOri.value='';")
								Response.Write("tcnAmountTrasOri.value='';")
								Response.Write("tcnInt_moraTrasOri.value='';")
							End If
							lObjFinanc_dra = Nothing
						Else
							Response.Write("top.frames['fraHeader'].ShowDiv('DivlblDraftOri', 'hide');")
							Response.Write("top.frames['fraHeader'].ShowDiv('DivDraftOri', 'hide');")
							Response.Write("top.frames['fraHeader'].ShowDiv('DivlblDraftDes', 'hide');")
							Response.Write("top.frames['fraHeader'].ShowDiv('DivDraftDes', 'hide');")
							Response.Write("cbeCurrencyOri.value='" & lobjValues.TypeToString(.nCurrency, eFunctions.Values.eTypeData.etdInteger) & "';")
							'Response.Write "tcnAmountTrasOri.disabled = false;"							
							'+ Se obtiene la información del movimiento a traspasar del recibo origen
							lobjPremium_mo = New eCollection.Premium_mo
							With lobjPremium_mo
								If CBool(.insReaReceipt_LastMovPay(lobjDocument.sCertype, lobjDocument.nBranch, lobjDocument.nProduct, lobjDocument.nReceipt, lobjDocument.nDigit, lobjDocument.nPaynumbe).ToOADate) Then
									
									Response.Write("tcnAmountOri.value='" & lobjValues.TypeToString(.nAmount, eFunctions.Values.eTypeData.etdDouble, True, 6) & "';")
									Response.Write("tcnAmountTrasOri.value='" & lobjValues.TypeToString(.nAmount, eFunctions.Values.eTypeData.etdDouble, True, 6) & "';")
									Response.Write("tcnInt_moraTrasOri.value='" & lobjValues.TypeToString(.nInt_mora, eFunctions.Values.eTypeData.etdDouble, True, 6) & "';")
								Else
									Response.Write("tcnAmountOri.value='';")
									Response.Write("tcnAmountTrasOri.value='';")
									Response.Write("tcnInt_moraTrasOri.value='';")
								End If
							End With
							lobjPremium_mo = Nothing
						End If
					Else
						Response.Write("cbeBranchDes.value='" & lobjValues.TypeToString(.nBranch, eFunctions.Values.eTypeData.etdInteger) & "';")
						Response.Write("valProductDes.Parameters.Param1.sValue=cbeBranchDes.value;")
						Response.Write("valProductDes.value='" & lobjValues.TypeToString(.nProduct, eFunctions.Values.eTypeData.etdInteger) & "';")
						Response.Write("top.frames['fraHeader'].$('#valProductDes').change();")
						Response.Write("tcnPolicyDes.value='" & lobjValues.TypeToString(.nPolicy, eFunctions.Values.eTypeData.etdDouble) & "';")
						If mobjValues.StringToType(.nContrat, eFunctions.Values.eTypeData.etdDouble, True) <> eRemoteDB.Constants.intNull Then
							'+ Si es Financiado se obtiene el monto y la moneda de la última cuota pagada													
							lObjFinanc_dra = New eFinance.FinanceDraft
							If lObjFinanc_dra.Find_Co634(lobjDocument.nContrat, 1) Then
								Response.Write("hddnContratDes.value='" & lobjValues.TypeToString(.nContrat, eFunctions.Values.eTypeData.etdDouble) & "';")
								Response.Write("tcnDraftDes.value='" & lobjValues.TypeToString(lObjFinanc_dra.nDraft, eFunctions.Values.eTypeData.etdDouble) & "';")
								Response.Write("cbeCurrencyDes.value='" & lobjValues.TypeToString(lObjFinanc_dra.nCurrency, eFunctions.Values.eTypeData.etdDouble) & "';")
								Response.Write("tcnAmountDes.value='" & lobjValues.TypeToString(lObjFinanc_dra.nAmount, eFunctions.Values.eTypeData.etdDouble, True, 6) & "';")
							Else
								Response.Write("hddnContratDes.value='';")
								Response.Write("tcnDraftDes.value='';")
								Response.Write("cbeCurrencyDes.value=0;")
								Response.Write("tcnAmountDes.value='';")
							End If
							lObjFinanc_dra = Nothing
						Else
							Response.Write("cbeCurrencyDes.value='" & lobjValues.TypeToString(.nCurrency, eFunctions.Values.eTypeData.etdInteger) & "';")
							Response.Write("tcnAmountDes.value='" & lobjValues.TypeToString(.nBalance, eFunctions.Values.eTypeData.etdDouble, True, 6) & "';")
						End If
					End If
				Else
					If Request.QueryString.Item("sTypDocument") = "Ori" Then
						Response.Write("cbeBranchOri.value='';")
						Response.Write("valProductOri.value='';")
						Response.Write("top.frames['fraHeader'].$('#valProductOri').change();")
						Response.Write("tcnPolicyOri.value='';")
						Response.Write("cbeCurrencyOri.value='';")
						Response.Write("tcnAmountOri.value='';")
						Response.Write("tcnDraftOri.value='';")
						Response.Write("hddnContratOri.value='';")
						Response.Write("tcnAmountTrasOri.value='';")
						Response.Write("tcnInt_moraTrasOri.value='';")
						Response.Write("top.frames['fraHeader'].ShowDiv('DivlblDraftOri', 'hide');")
						Response.Write("top.frames['fraHeader'].ShowDiv('DivDraftOri', 'hide');")
						Response.Write("top.frames['fraHeader'].ShowDiv('DivlblDraftDes', 'hide');")
						Response.Write("top.frames['fraHeader'].ShowDiv('DivDraftDes', 'hide');")
					Else
						Response.Write("cbeBranchDes.value='';")
						Response.Write("valProductDes.value='';")
						Response.Write("top.frames['fraHeader'].$('#valProductDes').change();")
						Response.Write("tcnPolicyDes.value='';")
						Response.Write("cbeCurrencyDes.value='';")
						Response.Write("tcnAmountDes.value='';")
						Response.Write("hddnContratDes.value='';")
						Response.Write("tcnDraftDes.value='';")
					End If
				End If
			End With
	End Select
	Response.Write("}")
	lobjValues = Nothing
	lobjDocument = Nothing
End Sub

'% insShowDataCO722: Muestra el contratante y el número de mandato dado una póliza.
'--------------------------------------------------------------------------------------------
Private Sub insShowDataCO722()
	'--------------------------------------------------------------------------------------------
	Dim lclsDir_Debit As ePolicy.DirDebit
	Dim lclsPolicy As ePolicy.Policy
	Dim lclsClient As eClient.Client
	
	lclsDir_Debit = New ePolicy.DirDebit
	lclsPolicy = New ePolicy.Policy
	lclsClient = New eClient.Client
	
	With lclsPolicy
		If .Find("2", CInt(Request.QueryString.Item("nBranch")), CInt(Request.QueryString.Item("nProduct")), CDbl(Request.QueryString.Item("nPolicy"))) Then
			If .sPolitype = "1" Then
				Response.Write("top.frames['fraHeader'].document.forms[0].tcnCertif.disabled=true;")
			Else
				Response.Write("top.frames['fraHeader'].document.forms[0].tcnCertif.disabled=false;")
			End If
		End If
	End With
	
	If lclsDir_Debit.Find("2", CInt(Request.QueryString.Item("nBranch")), CInt(Request.QueryString.Item("nProduct")), CDbl(Request.QueryString.Item("nPolicy")), CDbl(Request.QueryString.Item("nCertif")), CDate(Request.QueryString.Item("dDate"))) Then
		If lclsClient.Find(lclsDir_Debit.sClient) Then
			Response.Write("top.frames['fraHeader'].document.forms[0].dtcClient.value='" & lclsDir_Debit.sClient & "';")
			Response.Write("top.frames['fraHeader'].document.forms[0].dtcClient_Digit.value='" & lclsClient.sDigit & "';")
			Response.Write("top.frames['fraHeader'].UpdateDiv('lblCliename','" & lclsClient.sCliename & "');")
			Response.Write("top.frames['fraHeader'].document.forms[0].tctBankAuthOld.value = '" & lclsDir_Debit.sBankauth & "';")
			Response.Write("top.frames['fraHeader'].document.forms[0].tctBankAuthNew.value ='';")
			Response.Write("top.frames['fraHeader'].$('#tctBankAuthNew').change();")
		End If
	Else
		Response.Write("top.frames['fraHeader'].document.forms[0].dtcClient.value='';")
		Response.Write("top.frames['fraHeader'].document.forms[0].dtcClient_Digit.value='';")
		Response.Write("top.frames['fraHeader'].UpdateDiv('lblCliename','');")
		Response.Write("top.frames['fraHeader'].document.forms[0].tctBankAuthOld.value ='';")
		Response.Write("top.frames['fraHeader'].document.forms[0].tctBankAuthNew.value ='';")
	End If
	
	lclsDir_Debit = Nothing
	lclsPolicy = Nothing
	lclsClient = Nothing
End Sub

'% insShowDesc: 
'--------------------------------------------------------------------------------------------
Private Sub insShowDesc()
	'--------------------------------------------------------------------------------------------
	Dim lclsBulletins_det As eCollection.Bulletins_det
	
	lclsBulletins_det = New eCollection.Bulletins_det
	
	With lclsBulletins_det
		If .insFindC0514_K("CO514", mobjValues.StringToType(Request.QueryString.Item("nBulletins"), eFunctions.Values.eTypeData.etdDouble)) Then
			Response.Write("opener.document.forms[0].tctClient.value = '" & .sCliename & "';")
			Response.Write("opener.document.forms[0].tctCurrency.value='" & .sCurrency & "';")
			Response.Write("opener.document.forms[0].tctWayPay.value='" & .sWay_Pay & "';")
			Response.Write("opener.document.forms[0].tctAmoun_pa.value='" & .sAmount_pa & "';")
			Response.Write("opener.document.forms[0].tctStatus.value='" & .sStatus & "';")
		End If
	End With
	lclsBulletins_det = Nothing
End Sub
'% insDisabledCertif: Habilita o deshabilita el campo nCertif dependiendo del tipo de póliza pasada como parámetro.
'--------------------------------------------------------------------------------------------
Private Sub insDisabledCertif()
	'--------------------------------------------------------------------------------------------
	Dim lclsPolicy As ePolicy.Policy
	
	lclsPolicy = New ePolicy.Policy
	
	With lclsPolicy
		If .Find(Request.QueryString.Item("sCertype"), CInt(Request.QueryString.Item("nBranch")), CInt(Request.QueryString.Item("nProduct")), CDbl(Request.QueryString.Item("nPolicy"))) Then
			If .sPolitype = "1" Then
				Response.Write("top.frames['fraHeader'].document.forms[0]).tcnCertif.disabled=true;")
				Response.Write("top.frames['fraHeader'].document.forms[0]).tcnCertif.values='0';")
			Else
				Response.Write("top.frames['fraHeader'].document.forms[0]).tcnCertif.disabled=true;")
			End If
		End If
	End With
	
	lclsPolicy = Nothing
End Sub

'% insFindDocumentCO633: Se buscan los datos de los diferentes documentos a procesar.
'-----------------------------------------------------------------------------------
Private Sub insFindDocumentCO633()
	'-----------------------------------------------------------------------------------
	Dim lobjDocument As Object
	Dim lobjCertificat As Object
	
	Dim mobjCertificat As ePolicy.Certificat
	Select Case Request.QueryString.Item("sDocument")
		Case "Policy"
			lobjDocument = New ePolicy.Policy
			With lobjDocument
				
				If .FindPolicybyPolicy(Request.QueryString.Item("sCertype"), mobjValues.StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble)) Then
					If .sPolitype = "1" Then
						Response.Write("top.frames['fraHeader'].document.forms[0].tcnCertif.disabled=true;")
						Response.Write("top.frames['fraHeader'].document.forms[0].tcnCertif.value='0';")
						Call insFindCertificat(Request.QueryString.Item("sCertype"), .nBranch, .nProduct, .nPolicy, 0)
					Else
						Response.Write("top.frames['fraHeader'].document.forms[0].tcnCertif.disabled=false;")
						Response.Write("top.frames['fraHeader'].document.forms[0].tcnCertif.value='';")
					End If
                        Response.Write("top.frames['fraHeader'].document.forms[0].cbeBranch.value=" & .nBranch & ";")
                        Response.Write("top.frames['fraHeader'].document.forms[0].valProduct.Parameters.Param1.sValue=" & .nBranch & ";")
                        Response.Write("top.frames['fraHeader'].document.forms[0].valProduct.value=" & .nProduct & ";")
					Response.Write("top.frames['fraHeader'].$('#valProduct').change();")
					If mobjValues.TypeToString(.dExpirdat, eFunctions.Values.eTypeData.etdDate) <> "" Then
						Response.Write("top.frames['fraHeader'].ldtmExpirDat='" & mobjValues.TypeToString(.dExpirdat, eFunctions.Values.eTypeData.etdDate) & "';")
					Else
						Response.Write("top.frames['fraHeader'].ldtmExpirDat='01/01/2200';")
					End If
					If Request.QueryString.Item("nOption") = "1" Then
                            Response.Write("top.frames['fraHeader'].document.forms[0].tcdCollSus_ini.value='" & DateSerial(Year(Today),Month(Today),Day(.dNextReceip)) & "';")
					Else
						mobjCertificat = New ePolicy.Certificat
						If mobjCertificat.Find(Request.QueryString.Item("sCertype"), .nBranch, .nProduct, .nPolicy, 0, True) Then
							Response.Write("top.frames['fraHeader'].document.forms[0].tcdCollSus_ini.value='" & mobjValues.TypeToString(mobjCertificat.dCollSus_ini, eFunctions.Values.eTypeData.etdDate) & "';")
							Response.Write("top.frames['fraHeader'].document.forms[0].tcdCollSus_end.value='" & mobjValues.TypeToString(mobjCertificat.dCollSus_end, eFunctions.Values.eTypeData.etdDate) & "';")
							mobjCertificat = Nothing
						End If
					End If
					'Response.Write "top.frames['fraHeader'].document.forms[0].tcdCollSus_end.value='" & mobjValues.TypeToString(.dExpirdat,eFunctions.Values.eTypeData.etdDate) & "';"
				Else
					Response.Write("top.frames['fraHeader'].document.forms[0].tcnPolicy.value='';")
					Response.Write("top.frames['fraHeader'].document.forms[0].tcnCertif.disabled=false;")
					Response.Write("top.frames['fraHeader'].document.forms[0].tcnCertif.value='';")
					Response.Write("top.frames['fraHeader'].document.forms[0].cbeBranch.value=0;")
					Response.Write("top.frames['fraHeader'].document.forms[0].valProduct.Parameters.Param1.sValue='';")
					Response.Write("top.frames['fraHeader'].document.forms[0].valProduct.value='';")
					Response.Write("top.frames['fraHeader'].$('#valProduct').change();")
					Response.Write("top.frames['fraHeader'].document.forms[0].tcdCollSus_end.value='';")
					Response.Write("top.frames['fraHeader'].document.forms[0].chkDef.checked=false;")
					Response.Write("top.frames['fraHeader'].insChekDef();")
					Response.Write("top.frames['fraHeader'].ldtmExpirDat='';")
					Response.Write("top.frames['fraHeader'].document.forms[0].tcdCollSus_ini.value='';")
					Response.Write("top.frames['fraHeader'].document.forms[0].tcdCollSus_end.value='';")
				End If
			End With
			
		Case "Certif"
			Call insFindCertificat(Request.QueryString.Item("sCertype"), mobjValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nCertif"), eFunctions.Values.eTypeData.etdDouble))
			
			
		Case "Receipt"
			lobjDocument = New eCollection.Premium
			With lobjDocument
				
				If .Find(Request.QueryString.Item("sCertype"), mobjValues.StringToType(Request.QueryString.Item("nReceipt"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, 0, 0) Then
					
					'+ Se verifica si dicho recibo esta financiado para mostrar el número de contrato y habilitar el campo cuota.
					If .nContrat > 0 Then
						Response.Write("top.frames['fraHeader'].document.forms[0].tcnContrat.value='" & mobjValues.TypeToString(.nContrat, eFunctions.Values.eTypeData.etdDouble) & "';")
						Response.Write("top.frames['fraHeader'].document.forms[0].tcnDraft.disabled=false;")
					Else
						Response.Write("top.frames['fraHeader'].document.forms[0].tcdCollSus_ini.value='" & mobjValues.TypeToString(.dCollSus_ini, eFunctions.Values.eTypeData.etdDate) & "';")
						Response.Write("top.frames['fraHeader'].document.forms[0].tcdCollSus_end.value='" & mobjValues.TypeToString(.dCollSus_end, eFunctions.Values.eTypeData.etdDate) & "';")
						Response.Write("top.frames['fraHeader'].document.forms[0].cbeSus_reason.value='" & mobjValues.TypeToString(.nSus_reason, eFunctions.Values.eTypeData.etdDouble) & "';")
						Response.Write("top.frames['fraHeader'].document.forms[0].tcnContrat.value='';")
						Response.Write("top.frames['fraHeader'].document.forms[0].tcnDraft.disabled=true;")
						Call setTypOperAut(mobjValues.StringToType(Request.QueryString.Item("nTypOper"), eFunctions.Values.eTypeData.etdDouble), .dCollSus_ini, .dCollSus_ini)
					End If
				Else
					Response.Write("top.frames['fraHeader'].document.forms[0].tcnContrat.value='';")
					Response.Write("top.frames['fraHeader'].document.forms[0].tcnDraft.value='';")
					Response.Write("top.frames['fraHeader'].document.forms[0].tcnDraft.disabled=true;")
				End If
			End With
			
		Case "Draft"
			lobjDocument = New eFinance.FinanceDraft
			With lobjDocument
				'+ Si el campo contrato tiene valor.	
				
				If CDbl(Request.QueryString.Item("nContrat")) > 0 Then
					If .Find(mobjValues.StringToType(Request.QueryString.Item("nContrat"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nDraft"), eFunctions.Values.eTypeData.etdDouble)) Then
						Response.Write("top.frames['fraHeader'].document.forms[0].tcdCollSus_ini.value='" & mobjValues.TypeToString(.dCollSus_ini, eFunctions.Values.eTypeData.etdDate) & "';")
						Response.Write("top.frames['fraHeader'].document.forms[0].tcdCollSus_end.value='" & mobjValues.TypeToString(.dCollSus_end, eFunctions.Values.eTypeData.etdDate) & "';")
						Response.Write("top.frames['fraHeader'].document.forms[0].cbeSus_reason.value='" & mobjValues.TypeToString(.nSus_reason, eFunctions.Values.eTypeData.etdDouble) & "';")
						Call setTypOperAut(mobjValues.StringToType(Request.QueryString.Item("nTypOper"), eFunctions.Values.eTypeData.etdDouble), .dCollSus_ini, .dCollSus_ini)
					Else
						Response.Write("top.frames['fraHeader'].document.forms[0].tcnDraft.value='';")
						Response.Write("alert(""" & "Err 7131: Cuota no existe para ese contrato" & """);")
					End If
				End If
			End With
	End Select
	lobjDocument = Nothing
	lobjCertificat = Nothing
End Sub

'% insFindCertificat: Se buscan los datos de un determinado certificado.
'-----------------------------------------------------------------------------------
Sub insFindCertificat(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double)
	'-----------------------------------------------------------------------------------
	Dim lobjCertificat As ePolicy.Certificat
	
	lobjCertificat = New ePolicy.Certificat
	
	With lobjCertificat
		If .Find(sCertype, nBranch, nProduct, nPolicy, nCertif) Then
			Response.Write("top.frames['fraHeader'].document.forms[0].tcdCollSus_ini.value='" & mobjValues.TypeToString(.dCollSus_ini, eFunctions.Values.eTypeData.etdDate) & "';")
			Response.Write("top.frames['fraHeader'].document.forms[0].tcdCollSus_end.value='" & mobjValues.TypeToString(.dCollSus_end, eFunctions.Values.eTypeData.etdDate) & "';")
			Response.Write("top.frames['fraHeader'].document.forms[0].cbeSus_reason.value='" & mobjValues.TypeToString(.nSus_reason, eFunctions.Values.eTypeData.etdDouble) & "';")
			
			'+ En caso de que tenga la cobranza suspendida y el tipo de operación sea Suspensión se cambia a reactivación automáticamente.
			Call setTypOperAut(mobjValues.StringToType(Request.QueryString.Item("nTypOper"), eFunctions.Values.eTypeData.etdDouble), .dCollSus_ini, .dCollSus_ini)
			
			'+ En caso de que el tipo de operación sea suspensión y el origen sea cartera se asigna como fecha de fins de la suspensión la fecha de vencimiento de certificado.									
			If mobjValues.StringToType(Request.QueryString.Item("nTypOper"), eFunctions.Values.eTypeData.etdDouble) = 1 And Request.QueryString.Item("sSus_origi") = "2" Then
				Response.Write("top.frames['fraHeader'].document.forms[0].tcdCollSus_end.value='" & mobjValues.TypeToString(.dExpirdat, eFunctions.Values.eTypeData.etdDate) & "';")
			End If
		Else
			Response.Write("top.frames['fraHeader'].document.forms[0].tcnCertif.value='';")
		End If
	End With
	lobjCertificat = Nothing
End Sub

'% setTypOperAut: Se setea automáticamente el campo tipo de operción de acuerdo al valor de las fechas de la suspensión.
'-----------------------------------------------------------------------------------
Sub setTypOperAut(ByVal nTypOper As Byte, ByVal dCollSus_ini As Object, ByVal dCollSus_end As Object)
	'-----------------------------------------------------------------------------------
	'+ En caso de que tenga la cobranza suspendida y el tipo de operación sea Suspensión se cambia a reactivación automáticamente.
	If nTypOper = 1 Then
		If dCollSus_ini <> eRemoteDB.Constants.dtmNull And dCollSus_end <> eRemoteDB.Constants.dtmNull Then
			Response.Write("top.frames['fraHeader'].document.forms[0].optTypOper[0].checked=true;")
			Response.Write("top.frames['fraHeader'].document.forms[0].tcdCollSus_ini.disabled=true;")
			Response.Write("top.frames['fraHeader'].document.forms[0].tcdCollSus_end.disabled=true;")
			Response.Write("top.frames['fraHeader'].document.forms[0].cbeSus_reason.disabled=true;")
		End If
	Else
		'+ Si el tipo de operación es reactivación y la fecha están sin valor se cambia automáticamente a suspensión.
		If dCollSus_ini = eRemoteDB.Constants.dtmNull And dCollSus_end = eRemoteDB.Constants.dtmNull Then
			Response.Write("top.frames['fraHeader'].document.forms[0].tcdCollSus_ini.disabled=false;")
			Response.Write("top.frames['fraHeader'].document.forms[0].tcdCollSus_end.disabled=false;")
			Response.Write("top.frames['fraHeader'].document.forms[0].cbeSus_reason.disabled=false;")
		End If
	End If
End Sub

'% insUpdSelCO501: Se encarga de actualizar el campo sel de la transacción CO501.
'--------------------------------------------------------------------------------------------
Private Sub insUpdSelCO501()
	'--------------------------------------------------------------------------------------------
	Dim lclsBulletin As eCollection.Bulletin
	
	lclsBulletin = New eCollection.Bulletin
	
	'+ Se actualiza el campo sSel de la tabla temporal t_doctyp para seleccionar o deseleccionar el registro.
	Call lclsBulletin.insPostCO501Upd(Request.QueryString.Item("sKey"), "Upddes", mobjValues.StringToType(Request.QueryString.Item("nBulletins"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Constants.intNull, Session("nUsercode"), eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, mobjValues.StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Constants.intNull, "", eRemoteDB.Constants.intNull, "", eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, "", eRemoteDB.Constants.intNull)
	
	Response.Write("top.frames['fraFolder'].insReload('" & Request.QueryString.Item("sKey") & "');")
	
	lclsBulletin = Nothing
End Sub

'% insUpdSelCO632: Se encarga de actualizar el campo sel en la tabla temporal t_bulletins_det.
'--------------------------------------------------------------------------------------------
Private Sub insUpdSelCO632()
	'--------------------------------------------------------------------------------------------
	Dim lclsT_bulletins_det As eCollection.T_bulletins_det
	Dim lintTypDoc As Object
	
	lclsT_bulletins_det = New eCollection.T_bulletins_det
	
	'+ Se actualiza el campo sSel de la tabla temporal t_doctyp para seleccionar o deseleccionar el registro.
	With lclsT_bulletins_det
		.nBulletins = mobjValues.StringToType(Request.QueryString.Item("nBulletins"), eFunctions.Values.eTypeData.etdDouble)
		.nId = mobjValues.StringToType(Request.QueryString.Item("nId"), eFunctions.Values.eTypeData.etdDouble)
		.sSel = Request.QueryString.Item("sSel")
		.insUpdT_bulletins_det(4)
	End With
	
	lintTypDoc = mobjValues.StringToType(Request.QueryString.Item("nCollecDocTyp"), eFunctions.Values.eTypeData.etdDouble)
	'+ Si el tipo de documento corresponde a un recibo o una cuota se recarga la ventana.	
	
	
	'+ Si no corresponde a recibo ni cuota se efectúa el recalculo.
	With lclsT_bulletins_det
		.nBulletins = mobjValues.StringToType(Session("nBulletins"), eFunctions.Values.eTypeData.etdDouble)
		.dCollectDate = mobjValues.StringToType(Session("dCollectDate"), eFunctions.Values.eTypeData.etdDate)
		.calTotalsBulletins()
		Response.Write("top.frames['fraHeader'].UpdateDiv('lblTotSaldo','" & mobjValues.TypeToString(.nTotalGeneral, eFunctions.Values.eTypeData.etdDouble, True, 6) & "');")
	End With
	
	
	lclsT_bulletins_det = Nothing
End Sub

'% insDelCO632: Se encarga de eliminar los registros de la tabla temporal t_bulletins_det según un número de boletín.
'--------------------------------------------------------------------------------------------
Private Sub insDelCO632()
	'--------------------------------------------------------------------------------------------
	Dim lclsT_bulletins_det As eCollection.T_bulletins_det
	
	lclsT_bulletins_det = New eCollection.T_bulletins_det
	
	'+ Se elimina la información según el número del boletín.
	lclsT_bulletins_det.Delete_all(Session("nBulletins"))
	
	lclsT_bulletins_det = Nothing
End Sub

'% insShowBulletinsCO632: Obtiene la información del boletín para su consulta o posterior modificación.
'--------------------------------------------------------------------------------------------
Private Sub insShowBulletinsCO632()
	'--------------------------------------------------------------------------------------------
	Dim lclsT_bulletins_det As eCollection.T_bulletins_det
	
	lclsT_bulletins_det = New eCollection.T_bulletins_det
	
	With lclsT_bulletins_det
		'+ Se verifica si existe el boletin para su consulta o modificación
		If .insReaExistsCO632_K(CInt(Request.QueryString.Item("nMainAction")), CDbl(Request.QueryString.Item("nBulletins"))) Then
			
			If .sStyle_bull = "1" Then
				Response.Write("top.frames['fraHeader'].document.forms[0].optStyle_bull[0].checked=true;")
				Response.Write("top.frames['fraHeader'].ShowDiv('divCurrency','show');")
			Else
				Response.Write("top.frames['fraHeader'].document.forms[0].optStyle_bull[1].checked=true;")
				Response.Write("top.frames['fraHeader'].ShowDiv('divCurrency','hide');")
			End If
			Response.Write("top.frames['fraHeader'].document.forms[0].cbeInsur_area.value='" & mobjValues.TypeToString(.nInsur_area, eFunctions.Values.eTypeData.etdDouble) & "';")
			Response.Write("top.frames['fraHeader'].document.forms[0].tctStatus.value='" & .sStatus & "';")
			Response.Write("top.frames['fraHeader'].UpdateDiv('lblStatus','" & .sStatus & "');")
			Response.Write("top.frames['fraHeader'].document.forms[0].cbeCurrencyBul.value='" & .nCurrency & "';")
		Else
			Response.Write("top.frames['fraHeader'].document.forms[0].tcnBulletins.value='';")
			Response.Write("top.frames['fraHeader'].document.forms[0].cbeInsur_area.value='';")
			Response.Write("top.frames['fraHeader'].UpdateDiv('lblStatus','');")
			Response.Write("top.frames['fraHeader'].document.forms[0].cbeCurrencyBul.value=0;")
		End If
	End With
	
	lclsT_bulletins_det = Nothing
End Sub

'% insShowDataCO632: Se buscan los datos de los diferentes documentos a procesar.
'-----------------------------------------------------------------------------------
Private Sub insShowDataCO632()
	'-----------------------------------------------------------------------------------
	Dim lobjGeneral As eGeneral.Exchange
	Dim lobjDocument As eCollection.Premium
	Dim lobjDraft As eFinance.FinanceDraft
	Dim ldblExchange As Double
	Dim lintCurrency As Integer
	Dim ldblAmount As Double
	Dim lblnFind As Boolean
	Dim lintTypDoc As Object
	Dim llngCollector As Double
	Dim lobjCollector As eCollection.Collector
	
	lblnFind = False
	lintCurrency = 1
	ldblExchange = 1
	ldblAmount = 0
	
	lintTypDoc = mobjValues.StringToType(Request.QueryString.Item("nCollecDocTyp"), eFunctions.Values.eTypeData.etdDouble, True)
	Select Case lintTypDoc
		'+Recibos
		Case 1
			lobjDocument = New eCollection.Premium
			With lobjDocument
				
				'+ Si el campo contrato no tiene valor; se trata como un recibo.				
				If .Find("2", mobjValues.StringToType(Request.QueryString.Item("nReceipt"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), 0, 0) Then
					Response.Write("top.frames['fraFolder'].document.forms[0].tcnDocument.value='" & mobjValues.TypeToString(.nReceipt, eFunctions.Values.eTypeData.etdDouble) & "';")
					Response.Write("top.frames['fraFolder'].document.forms[0].cbeBranch.value='" & mobjValues.TypeToString(.nBranch, eFunctions.Values.eTypeData.etdDouble) & "';")
					Response.Write("top.frames['fraFolder'].document.forms[0].tcdStatDate.value='" & mobjValues.StringToType(CStr(.dStatdate), eFunctions.Values.eTypeData.etdDate) & "';")
					Response.Write("top.frames['fraFolder'].document.forms[0].tcdStatDate.disabled= true;")
					Response.Write("top.frames['fraFolder'].document.forms[0].tcdExpirDat.value='" & mobjValues.StringToType(CStr(.dExpirdat), eFunctions.Values.eTypeData.etdDate) & "';")
					Response.Write("top.frames['fraFolder'].document.forms[0].tcdExpirDat.disabled= true;")
					Response.Write("top.frames['fraFolder'].document.forms[0].valProduct.value='" & mobjValues.TypeToString(.nProduct, eFunctions.Values.eTypeData.etdDouble) & "';")
					Response.Write("top.frames['fraFolder'].document.forms[0].tcnPolicy.value='" & mobjValues.TypeToString(.nPolicy, eFunctions.Values.eTypeData.etdDouble) & "';")
					Response.Write("top.frames['fraFolder'].document.forms[0].tcnCertif.value='" & mobjValues.TypeToString(.nCertif, eFunctions.Values.eTypeData.etdDouble) & "';")
					Response.Write("top.frames['fraFolder'].document.forms[0].tcnDigit.value='" & mobjValues.StringToType(CStr(.nDigit), eFunctions.Values.eTypeData.etdDouble) & "';")
					Response.Write("top.frames['fraFolder'].document.forms[0].tcnPaynumbe.value='" & mobjValues.StringToType(CStr(.nPaynumbe), eFunctions.Values.eTypeData.etdDouble) & "';")
					Response.Write("top.frames['fraFolder'].document.forms[0].tcnCod_agree.value='" & mobjValues.StringToType(CStr(.nCod_Agree), eFunctions.Values.eTypeData.etdDouble) & "';")
					Response.Write("top.frames['fraFolder'].document.forms[0].dtcClient.value='" & .sClient & "';")
					Response.Write("top.frames['fraFolder'].document.forms[0].tcnType.value='" & mobjValues.TypeToString(.nType, eFunctions.Values.eTypeData.etdDouble) & "';")
					Response.Write("top.frames['fraFolder'].document.forms[0].tcnTratypei.value='" & mobjValues.TypeToString(.nTratypei, eFunctions.Values.eTypeData.etdDouble) & "';")
					Response.Write("top.frames['fraFolder'].document.forms[0].tcnReceipt.value='" & mobjValues.TypeToString(.nReceipt, eFunctions.Values.eTypeData.etdDouble) & "';")
					Response.Write("top.frames['fraFolder'].document.forms[0].tcnInsurArea.value='" & mobjValues.TypeToString(.nInsur_area, eFunctions.Values.eTypeData.etdDouble) & "';")
					Response.Write("top.frames['fraFolder'].document.forms[0].tcnStatus_pre.value='" & mobjValues.TypeToString(.nStatus_pre, eFunctions.Values.eTypeData.etdDouble) & "';")
					
					llngCollector = .nCollector
					ldblAmount = .nBalance
					lintCurrency = .nCurrency
					lblnFind = True
				End If
			End With
			'Cuotas
		Case 2
			lobjDraft = New eFinance.FinanceDraft
			With lobjDraft
				If .Find(mobjValues.StringToType(Request.QueryString.Item("nContrat"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.QueryString.Item("nDraft"), eFunctions.Values.eTypeData.etdDouble)) Then
					lblnFind = True
					
					ldblAmount = .nAmount
					lintCurrency = .nCurrency
					llngCollector = .nCollector
					Response.Write("top.frames['fraFolder'].document.forms[0].tcnContrat.value='" & mobjValues.StringToType(Request.QueryString.Item("nContrat"), eFunctions.Values.eTypeData.etdDouble, True) & "';")
					Response.Write("top.frames['fraFolder'].document.forms[0].tcdStatDate.value='" & .dStat_date & "';")
					Response.Write("top.frames['fraFolder'].document.forms[0].tcdStatDate.disabled = true;")
					Response.Write("top.frames['fraFolder'].document.forms[0].tcdExpirDat.value='" & .dExpirdat & "';")
					Response.Write("top.frames['fraFolder'].document.forms[0].tcdExpirDat.disabled= true;")
					Response.Write("top.frames['fraFolder'].document.forms[0].tcdnStat_draft.value='" & mobjValues.StringToType(CStr(.nStat_draft), eFunctions.Values.eTypeData.etdDouble) & "';")
					Response.Write("top.frames['fraFolder'].document.forms[0].dtcClient.value='" & .sClient & "';")
				End If
			End With
			lobjDraft = Nothing
	End Select
	
	'+ Si se encontró información se procede a realizar el cálculo según el factor de cambio a la fecha de valorización.
	If lblnFind Then
		'+ Si el tipo de moneda es local
		If Request.QueryString.Item("sStyle_bull") = "1" Then
			lobjGeneral = New eGeneral.Exchange
			'+ Se calcula factor de cambio de acuerdo a fecha de cobranza.
			If lintCurrency <> 1 Then
				'+ Se aplica factor de cambio
				If lobjGeneral.Find(lintCurrency, Session("dCollectDate")) Then
					ldblExchange = lobjGeneral.nExchange
				Else
					ldblExchange = 1
				End If
				ldblAmount = ldblAmount * ldblExchange
			End If
		End If
		
		lobjCollector = New eCollection.Collector
		'+ Se obtiene el tipo de cobrador.		
		With lobjCollector
			If .Find(llngCollector, "") Then
				If .nCollectorType <> 1 Then
					Response.Write("top.frames['fraFolder'].document.forms[0].tctCollector.value='1';")
				End If
			End If
		End With
		lobjCollector = Nothing
		
		Response.Write("top.frames['fraFolder'].document.forms[0].tcnAmount.value='" & mobjValues.TypeToString(ldblAmount, eFunctions.Values.eTypeData.etdDouble, True, 2) & "';")
		Response.Write("top.frames['fraFolder'].document.forms[0].tcnAmount.disabled = true;")
		Response.Write("top.frames['fraFolder'].document.forms[0].cbeCurrency.value='" & lintCurrency & "';")
		
		lobjGeneral = Nothing
	Else
		Response.Write("top.frames['fraFolder'].document.forms[0].tcnDocument.value='';")
		Response.Write("top.frames['fraFolder'].document.forms[0].cbeBranch.value='';")
		Response.Write("top.frames['fraFolder'].document.forms[0].valProduct.value='';")
		Response.Write("top.frames['fraFolder'].document.forms[0].tcnPolicy.value='';")
		Response.Write("top.frames['fraFolder'].document.forms[0].tcnCertif.value='0';")
		
		Response.Write("top.frames['fraFolder'].document.forms[0].tcnContrat.value='';")
		Response.Write("top.frames['fraFolder'].document.forms[0].tcnDraft.value='';")
		Response.Write("top.frames['fraFolder'].document.forms[0].dtcClient.value='';")
		
		Response.Write("top.frames['fraFolder'].document.forms[0].tcnAmount.value='';")
		Response.Write("top.frames['fraFolder'].document.forms[0].cbeCurrency.value='';")
		Response.Write(" alert('Error 5092: Número del documento no se encuentra registrado en el sistema');")
		
	End If
	
	lobjDocument = Nothing
End Sub

'% insShowDataCO700: Muestra los datos de la transacción de generación de facturas.
'--------------------------------------------------------------------------------------------
Private Sub insShowDataCO700()
	Dim ldblBill As String
	'--------------------------------------------------------------------------------------------
	Dim lclsDocument As Object
	
	Select Case Request.QueryString.Item("sField")
		Case "FindBill"
			lclsDocument = New eCollection.Bills
			If mobjValues.StringToType(Request.QueryString.Item("nBill"), eFunctions.Values.eTypeData.etdDouble) > 0 Then
				If lclsDocument.Find(mobjValues.StringToType(Request.QueryString.Item("nInsur_area"), eFunctions.Values.eTypeData.etdDouble), Request.QueryString.Item("sBillType"), mobjValues.StringToType(Request.QueryString.Item("nBill"), eFunctions.Values.eTypeData.etdDouble), True) Then
					Response.Write("top.frames['fraHeader'].document.forms[0].dtcClient.value='" & lclsDocument.sClient & "';")
					Response.Write("top.frames['fraHeader'].$('#dtcClient').change();")
					'+ Si se trata de una nota de crédito; se despliega la fecha tanto inicial como final.
					If lclsDocument.sBillType = "3" Or Request.QueryString.Item("nMainAction") = "401" Then
						Response.Write("top.frames['fraHeader'].document.forms[0].tcdDateIni.value='" & mobjValues.TypeToString(lclsDocument.dStatdate, eFunctions.Values.eTypeData.etdDate) & "';")
						Response.Write("top.frames['fraHeader'].document.forms[0].tcdDateEnd.value='" & mobjValues.TypeToString(lclsDocument.dStatdate, eFunctions.Values.eTypeData.etdDate) & "';")
					End If
				Else
					Response.Write("top.frames['fraHeader'].document.forms[0].tcnBill.value='';")
					Response.Write("top.frames['fraHeader'].document.forms[0].dtcClient.value='';")
					Response.Write("top.frames['fraHeader'].$('#dtcClient').change();")
					Response.Write("top.frames['fraHeader'].document.forms[0].tcdDateIni.value='';")
					Response.Write("top.frames['fraHeader'].document.forms[0].tcdDateEnd.value='';")
				End If
			End If
			
		Case "FindClient"
			lclsDocument = New ePolicy.Roles
			With lclsDocument
				If .Find("2", mobjValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble), 0, 1, vbNullString, Today, True) Then
					Response.Write("top.frames['fraHeader'].document.forms[0].dtcClient.value='" & .sClient & "';")
					Response.Write("top.frames['fraHeader'].document.forms[0].dtcClient_Digit.value='" & .sDigit & "';")
					Response.Write("top.frames['fraHeader'].UpdateDiv('lblCliename','" & .sCliename & "');")
				Else
					Response.Write("top.frames['fraHeader'].document.forms[0].dtcClient.value='';")
					Response.Write("top.frames['fraHeader'].document.forms[0].dtcClient_Digit.value='';")
					Response.Write("top.frames['fraHeader'].UpdateDiv('lblCliename','');")
				End If
			End With
			
		Case "FindLastBill"
			
			lclsDocument = New eCollection.Bills_Num
			
			ldblBill = lclsDocument.getLastBill(mobjValues.StringToType(Request.QueryString.Item("nInsur_area"), eFunctions.Values.eTypeData.etdDouble), Request.QueryString.Item("sBillType"))
			Response.Write("top.frames['fraHeader'].document.forms[0].tcnLastBill.value='" & mobjValues.TypeToString(ldblBill, eFunctions.Values.eTypeData.etdInteger) & "';")
			
		Case "DelTmp_CO700"
			lclsDocument = New eCollection.Bills
			lclsDocument.Delete(Request.QueryString.Item("sKey"))
			
		Case "UpdTmp_CO700sSel"
			lclsDocument = New eCollection.Bills
			'+ Se actualiza el campo sSel de la tabla temporal t_doctyp para seleccionar o deseleccionar el registro.
			If lclsDocument.InsUpdTmp_CO700_sSel(Request.QueryString.Item("sKey"), mobjValues.StringToType(Request.QueryString.Item("nId"), eFunctions.Values.eTypeData.etdDouble), Request.QueryString.Item("sSel")) Then
			End If
			
	End Select
	
	lclsDocument = Nothing
	
End Sub

'% ShowDefValuesCO004: Se encarga de cambiar los valores de la página
'--------------------------------------------------------------------------------------------  
Private Sub ShowDefValuesCO004()
	'--------------------------------------------------------------------------------------------  
	Dim lclsPolicy As ePolicy.Policy
	Dim lclsRoles As ePolicy.Roles
	
	lclsPolicy = New ePolicy.Policy
	lclsRoles = New ePolicy.Roles
	
	If lclsPolicy.FindPolicybyPolicy("2", mobjValues.StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble)) Then
		
		Response.Write("top.frames['fraHeader'].document.forms[0].cbeBranch.value='" & lclsPolicy.nBranch & "';")
		Response.Write("top.frames['fraHeader'].document.forms[0].valProduct.value='" & lclsPolicy.nProduct & "';")
		
		If lclsRoles.Find("2", lclsPolicy.nBranch, lclsPolicy.nProduct, CDbl(Request.QueryString.Item("nPolicy")), lclsPolicy.nCertif, 1, vbNullString, Today) Then
			
			Response.Write("top.frames['fraHeader'].document.forms[0].dtcClient.value='" & lclsRoles.sClient & "';")
			Response.Write("top.fraHeader.document.forms[0].dtcClient_Digit.value='" & lclsRoles.sDigit & "';")
			Response.Write("top.fraHeader.$('#dtcClient_Digit').change();")
		End If
	End If
	
	lclsPolicy = Nothing
	lclsRoles = Nothing
End Sub

'% insShowDataCO004: Muestra los datos de la transacción de modificación de gestión de cobro.
'--------------------------------------------------------------------------------------------
Private Sub insShowDataCO004()
	'--------------------------------------------------------------------------------------------
	Dim lclsPremium As eCollection.Premium
	Dim lclsRoles As ePolicy.Roles
	
	lclsPremium = New eCollection.Premium
	lclsRoles = New ePolicy.Roles
	
	If mobjValues.StringToType(Request.QueryString.Item("nReceipt"), eFunctions.Values.eTypeData.etdDouble) <> eRemoteDB.Constants.intNull Then
		
		With lclsPremium
			If .Find("2", mobjValues.StringToType(Request.QueryString.Item("nReceipt"), eFunctions.Values.eTypeData.etdDouble), 0, 0, mobjValues.StringToType("0", eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType("0", eFunctions.Values.eTypeData.etdDouble), False) Then
				
				
				Response.Write("top.frames['fraHeader'].document.forms[0].tcnDraft.value = '';")
				Response.Write("top.frames['fraHeader'].document.forms[0].action=top.frames['fraHeader'].document.forms[0].action + '&nReceipt=" & Request.QueryString.Item("nReceipt") & "';")
				
				Response.Write("top.frames['fraHeader'].document.forms[0].cbeBranch.value='" & .nBranch & "';")
				Response.Write("top.frames['fraHeader'].document.forms[0].valProduct.value='" & .nProduct & "';")
				Response.Write("top.frames['fraHeader'].document.forms[0].tcnPolicy.value='" & .nPolicy & "';")
				
				Response.Write("top.frames['fraHeader'].document.forms[0].hddnBranch.value='" & .nBranch & "';")
				Response.Write("top.frames['fraHeader'].document.forms[0].hddnProduct.value='" & .nProduct & "';")
				Response.Write("top.frames['fraHeader'].document.forms[0].hddnPolicy.value='" & .nPolicy & "';")
				Response.Write("top.frames['fraHeader'].document.forms[0].hddsCertype.value='" & .sCertype & "';")
				Response.Write("top.frames['fraHeader'].document.forms[0].hddnCertif.value='" & .nCertif & "';")
				Response.Write("top.frames['fraHeader'].document.forms[0].hddnDigit.value='" & .nDigit & "';")
				Response.Write("top.frames['fraHeader'].document.forms[0].hddnPaynumbe.value='" & .nPaynumbe & "';")
				Response.Write("top.frames['fraHeader'].document.forms[0].btnShowSCO001.disabled=false;")
				Response.Write("top.frames['fraHeader'].document.forms[0].hddnStatus_pre.value ='" & .nStatus_pre & "';")
				If .nStatus_pre = 8 Then
					Response.Write("top.frames['fraHeader'].document.forms[0].tcnDraft.disabled = false;")
				Else
					Response.Write("top.frames['fraHeader'].document.forms[0].tcnDraft.disabled = true;")
				End If
				
				If lclsRoles.Find("2", .nBranch, .nProduct, .nPolicy, .nCertif, 1, vbNullString, Today) Then
					
					Response.Write("top.frames['fraHeader'].document.forms[0].dtcClient.value='" & lclsRoles.sClient & "';")
					Response.Write("top.fraHeader.document.forms[0].dtcClient_Digit.value='" & lclsRoles.sDigit & "';")
					Response.Write("top.fraHeader.$('#dtcClient_Digit').change();")
				End If
				
			Else
				Response.Write("top.frames['fraHeader'].UpdateDiv('divBranch','','Normal');")
				Response.Write("top.frames['fraHeader'].UpdateDiv('divProduct','','Normal');")
				Response.Write("top.frames['fraHeader'].UpdateDiv('divPolicyD','','Normal');")
				Response.Write("top.frames['fraHeader'].document.forms[0].hddnBranch.value='';")
				Response.Write("top.frames['fraHeader'].document.forms[0].hddnProduct.value='';")
				Response.Write("top.frames['fraHeader'].document.forms[0].hddnPolicy.value='';")
				Response.Write("top.frames['fraHeader'].document.forms[0].btnShowSCO001.disabled=true;")
				Response.Write("top.frames['fraHeader'].document.forms[0].tcnDraft.value = '';")
				Response.Write("top.frames['fraHeader'].document.forms[0].tcnDraft.disabled = true;")
				Response.Write("top.frames['fraHeader'].document.forms[0].hddnStatus_pre.value = '';")
			End If
		End With
	End If
	lclsPremium = Nothing
	lclsRoles = Nothing
End Sub
'% Contrat_CO004: Busca datos del contrato a partir del numero de contrato ingresado 
'-----------------------------------------------------------------------------------
Private Sub Contrat_CO004()
	Dim lclsFinance As eFinance.FinancePre
	Dim lclsRoles As ePolicy.Roles
	
	lclsFinance = New eFinance.FinancePre
	lclsRoles = New ePolicy.Roles
	
	If Not IsNothing(Request.QueryString.Item("nContrat")) Then
		If lclsFinance.Find(CDbl(Request.QueryString.Item("nContrat")), eRemoteDB.Constants.intNull) Then
			Response.Write("top.frames['fraHeader'].document.forms[0].tcnPolicy.value='" & lclsFinance.nPolicy & "';")
			Response.Write("top.frames['fraHeader'].document.forms[0].cbeBranch.value='" & lclsFinance.nBranch & "';")
			Response.Write("top.frames['fraHeader'].document.forms[0].valProduct.value='" & lclsFinance.nProduct & "';")
			
			
			If lclsRoles.Find("2", lclsFinance.nBranch, lclsFinance.nProduct, lclsFinance.nPolicy, 0, 1, vbNullString, Today) Then
				
				Response.Write("top.frames['fraHeader'].document.forms[0].dtcClient.value='" & lclsRoles.sClient & "';")
				Response.Write("top.fraHeader.document.forms[0].dtcClient_Digit.value='" & lclsRoles.sDigit & "';")
				Response.Write("top.fraHeader.$('#dtcClient_Digit').change();")
			End If
		End If
	End If
	
	lclsFinance = Nothing
	lclsRoles = Nothing
	
End Sub

'% insFindCollector: Busca datos del cobrador necesarios para el encabezado de CO635
'-------------------------------------------------------------------------------------------- 
Private Sub insFindCollector()
	'-------------------------------------------------------------------------------------------- 
	Dim lclsCollector As eCollection.Collector
	
	lclsCollector = New eCollection.Collector
	
	With lclsCollector
		
		If .Find(mobjValues.StringToType(Request.QueryString.Item("nCollector"), eFunctions.Values.eTypeData.etdDouble, True), Request.QueryString.Item("sClient")) Then
			Response.Write(" with(top.frames['fraHeader'].document.forms[0]){")
			Response.Write(" cbeCollectortype.value='" & mobjValues.TypeToString(.nCollectorType, eFunctions.Values.eTypeData.etdDouble) & "';")
			Response.Write(" cbeContype.value='" & mobjValues.TypeToString(.nConType, eFunctions.Values.eTypeData.etdDouble) & "';")
			Response.Write(" };")
		Else
			Response.Write(" with(top.frames['fraHeader'].document.forms[0]){")
			Response.Write("valCollectorPre.value='';")
			Response.Write("cbeCollectortype.value=0;")
			Response.Write("cbeContype.value=0;")
			Response.Write(" };")
			Response.Write("top.frames['fraHeader'].UpdateDiv('valCollectorPreDesc','','Normal');")
		End If
	End With
	
	lclsCollector = Nothing
End Sub


'% insPrintCollectionRep: Se encarga de generar el reporte correspondiente.  
'--------------------------------------------------------------------------------------------  
Private Sub insPrintCollection()
	'--------------------------------------------------------------------------------------------  
	Dim mobjDocuments As eReports.Report
	
	mobjDocuments = New eReports.Report
	With mobjDocuments
		.ReportFilename = "COL635.rpt"
		.sCodispl = "CO635"
		.SetStorProcParam(1, mobjValues.StringToType(Request.QueryString.Item("nCollector"), eFunctions.Values.eTypeData.etdDouble))
		
		Response.Write((.Command))
		
	End With
	
	mobjDocuments = Nothing
End Sub

'% ChangeDefValues_CO004: Se encarga de cambiar los valores de la página
'--------------------------------------------------------------------------------------------  
Private Sub ChangeDefValues_CO004()
	'--------------------------------------------------------------------------------------------  
	Dim lclsDir_Debit As eCollection.Dir_debit
	
	lclsDir_Debit = New eCollection.Dir_debit
	
	Call lclsDir_Debit.insReaPremiumDir_debit(Request.QueryString.Item("sCertype_CO004"), mobjValues.StringToType(Request.QueryString.Item("nReceipt_CO004"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nDigit_CO004"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nPayNumbe_CO004"), eFunctions.Values.eTypeData.etdDouble), Session("sReceiptnum"), mobjValues.StringToDate(Request.QueryString.Item("dDateProcess")), mobjValues.StringToType(Request.QueryString.Item("nContrat_CO004"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nDraft_CO004"), eFunctions.Values.eTypeData.etdDouble))
	
	With lclsDir_Debit
		'+Si es domiciliación bancaria
		Response.Write("top.frames['fraFolder'].document.forms[0].hddsTitular.value='" & .sClient & "';")
		If Request.QueryString.Item("optChange_CO004") = "1" Then
			Response.Write("top.frames['fraFolder'].document.forms[0].cbeBank.value='" & .nBankext & "';")
			Response.Write("top.frames['fraFolder'].document.forms[0].tctTitular.value='" & .sClient & "';")
			Response.Write("top.frames['fraFolder'].document.forms[0].tctTitular_Digit.value='" & .sDigit & "';")
			Response.Write("top.frames['fraFolder'].UpdateDiv('tctName','" & .sDesClient & "');")
			Response.Write("top.frames['fraFolder'].document.forms[0].tctAccount.value='" & .sAccount & "';")
			Response.Write("top.frames['fraFolder'].document.forms[0].tctBankAuth.value='" & .sBankauth & "';")
		End If
		
		'+Si es tarjeta de credito
		If Request.QueryString.Item("optChange_CO004") = "2" Then
			Response.Write("top.frames['fraFolder'].document.forms[0].cbeCardType.value='" & .nTyp_crecard & "';")
			Response.Write("top.frames['fraFolder'].document.forms[0].tctCardNumber.value='" & .sCredi_card & "';")
			Response.Write("top.frames['fraFolder'].document.forms[0].tcdCardExpir.value='" & .dCardExpir & "';")
		End If
		
		'+Si es intermediario
		If Request.QueryString.Item("optChange_CO004") = "3" Then
			Response.Write("top.frames['fraFolder'].document.forms[0].valIntermed.value='" & .nIntermed & "';")
			'Response.Write "top.frames['fraFolder'].UpdateDiv('valIntermedDesc','" & .sDesIntermed & "');"
			Response.Write("top.frames['fraFolder'].$('#valIntermed').change();")
		End If
	End With
	lclsDir_Debit = Nothing
End Sub

'% insShowDataCO501: Se encarga de buscar el registro a rechazar en la  transacción CO501.
'--------------------------------------------------------------------------------------------
Private Sub insShowDataCO501()
	'--------------------------------------------------------------------------------------------
	Dim lclsBulletin As eCollection.Bulletin
	
	lclsBulletin = New eCollection.Bulletin
	If lclsBulletin.FindCo501(CInt(Request.QueryString.Item("nWay_pay")), CDate(Request.QueryString.Item("dEffecDate")), mobjValues.StringToType(Request.QueryString.Item("nBank"), eFunctions.Values.eTypeData.etdLong), CDbl(Request.QueryString.Item("nPolicy"))) Then
		Response.Write("top.frames['fraFolder'].document.forms[0].tctClient.value='" & lclsBulletin.sClient & "';")
		Response.Write("top.frames['fraFolder'].document.forms[0].tctCliename.value='" & lclsBulletin.sCliename & "';")
		Response.Write("top.frames['fraFolder'].document.forms[0].tctnBank.value='" & lclsBulletin.nBank_code & "';")
		Response.Write("top.frames['fraFolder'].document.forms[0].tctDocument.value='" & lclsBulletin.sDocument & "';")
		Response.Write("top.frames['fraFolder'].document.forms[0].tctAccount.value='" & lclsBulletin.sAccount & "';")
		Response.Write("top.frames['fraFolder'].document.forms[0].tctbulletins.value='" & lclsBulletin.nBulletins & "';")
		Response.Write("top.frames['fraFolder'].document.forms[0].cbeBranch.value='" & lclsBulletin.nBranch & "';")
		Response.Write("top.frames['fraFolder'].document.forms[0].valProduct.value='" & lclsBulletin.nProduct & "';")
		Response.Write("top.frames['fraFolder'].document.forms[0].tcnReceipt.value='" & lclsBulletin.nReceipt & "';")
		Response.Write("top.frames['fraFolder'].document.forms[0].tcnDraft.value='" & mobjValues.TypeToString(lclsBulletin.nDraft, eFunctions.Values.eTypeData.etdLong) & "';")
		Response.Write("top.frames['fraFolder'].document.forms[0].tctAmount.value='" & lclsBulletin.nAmount & "';")
		Response.Write("top.frames['fraFolder'].document.forms[0].tctCause.value='" & lclsBulletin.nRejectCause & "';")
	Else
		Response.Write("top.frames['fraFolder'].document.forms[0].tctClient.value='';")
		Response.Write("top.frames['fraFolder'].document.forms[0].tctCliename.value='';")
		Response.Write("top.frames['fraFolder'].document.forms[0].tctnBank.value='';")
		Response.Write("top.frames['fraFolder'].document.forms[0].tctDocument.value='';")
		Response.Write("top.frames['fraFolder'].document.forms[0].tctAccount.value='';")
		Response.Write("top.frames['fraFolder'].document.forms[0].tctbulletins.value='';")
		Response.Write("top.frames['fraFolder'].document.forms[0].cbeBranch.value=0;")
		Response.Write("top.frames['fraFolder'].document.forms[0].valProduct.value='';")
		Response.Write("top.frames['fraFolder'].document.forms[0].tcnReceipt.value='';")
		Response.Write("top.frames['fraFolder'].document.forms[0].tcnDraft.value='';")
		Response.Write("top.frames['fraFolder'].document.forms[0].tctAmount.value='';")
		Response.Write("top.frames['fraFolder'].document.forms[0].tctCause.value=0;")
	End If
	
	lclsBulletin = Nothing
End Sub

'% insShowPolicyCO788: Muestra los datos de la Póliza para la transaccion CO788.
'--------------------------------------------------------------------------------------------
Private Sub insShowPolicyCO788()
	'--------------------------------------------------------------------------------------------
	Dim lclsPolicy As ePolicy.Policy
	lclsPolicy = New ePolicy.Policy
	
	If mobjValues.StringToType(Request.QueryString.Item("nCollecDocTyp"), eFunctions.Values.eTypeData.etdInteger) = 6 Then
		'+ Se busca la información de la póliza
		If lclsPolicy.FindPolicybyPolicy("2", mobjValues.StringToType(Request.QueryString.Item("nDocument"), eFunctions.Values.eTypeData.etdDouble)) Then
			Response.Write("top.frames['fraHeader'].document.forms[0].valLoans.Parameters.Param1.sValue=" & lclsPolicy.nBranch & ";")
			Response.Write("top.frames['fraHeader'].document.forms[0].valLoans.Parameters.Param2.sValue=" & lclsPolicy.nProduct & ";")
			Response.Write("top.frames['fraHeader'].document.forms[0].valLoans.Parameters.Param3.sValue=" & lclsPolicy.nPolicy & ";")
			Response.Write("top.frames['fraHeader'].insShowField('TD','tdlblvalLoans','show');")
			Response.Write("top.frames['fraHeader'].insShowField('TD','tdvalLoans','show');")
		Else
			Response.Write("top.frames['fraHeader'].document.forms[0].valLoans.Parameters.Param1.sValue=0;")
			Response.Write("top.frames['fraHeader'].document.forms[0].valLoans.Parameters.Param2.sValue=0;")
			Response.Write("top.frames['fraHeader'].document.forms[0].valLoans.Parameters.Param3.sValue=0;")
			Response.Write("top.frames['fraHeader'].insShowField('TD','tdlblvalLoans','noshow');")
			Response.Write("top.frames['fraHeader'].insShowField('TD','tdvalLoans','noshow');")
		End If
		Response.Write("top.frames['fraHeader'].UpdateDiv('valLoansDesc','');")
		Response.Write("top.frames['fraHeader'].document.forms[0].valLoans.value='';")
		Response.Write("top.frames['fraHeader'].insShowField('DIV','divDatRel','noshow');")
		Response.Write("top.frames['fraHeader'].insShowField('TD','tdlblAmount','noshow');")
		Response.Write("top.frames['fraHeader'].insShowField('TD','tdlbllblAmount','noshow');")
		Response.Write("top.frames['fraHeader'].document.forms[0].hddDoc_Amount.value =0;")
		Response.Write("top.frames['fraHeader'].UpdateDiv('lblAmount','');")
		Response.Write("top.frames['fraHeader'].UpdateDiv('lblBordereaux','');")
		Response.Write("top.frames['fraHeader'].UpdateDiv('lblTypRel','');")
		Response.Write("top.frames['fraHeader'].UpdateDiv('lblDateRel','');")
		Response.Write("top.frames['fraHeader'].UpdateDiv('lblAgree','');")
		Response.Write("top.frames['fraHeader'].UpdateDiv('lblBank','');")
		Response.Write("top.frames['fraHeader'].UpdateDiv('lblQDoc','');")
		Response.Write("top.frames['fraHeader'].UpdateDiv('lblAmountRel','');")
		Response.Write("top.frames['fraHeader'].document.forms[0].hddSequence.value=0;")
		Response.Write("top.frames['fraHeader'].document.forms[0].tcnBordereaux.value ='';")
	End If
	lclsPolicy = Nothing
End Sub

'% insShowDataCO788: Muestra los datos de la transacción de devolución de cobro.
'--------------------------------------------------------------------------------------------
Private Sub insShowDataCO788()
	'--------------------------------------------------------------------------------------------
	Dim lclsColFormRef As eCollection.ColformRef
	
	lclsColFormRef = New eCollection.ColformRef
	
	With lclsColFormRef
		If .FindColFormRefCO788(mobjValues.StringToType(Request.QueryString.Item("nBordereaux"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nCollecDocTyp"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(Request.QueryString.Item("nDocument"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nDraft"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(Request.QueryString.Item("nLoans"), eFunctions.Values.eTypeData.etdDouble)) Then
			Response.Write("top.frames['fraHeader'].insShowField('DIV','divDatRel','show');")
			If mobjValues.StringToType(Request.QueryString.Item("nDocument"), eFunctions.Values.eTypeData.etdDouble) <> eRemoteDB.Constants.intNull Then
				Response.Write("top.frames['fraHeader'].insShowField('TD','tdlblAmount','show');")
				Response.Write("top.frames['fraHeader'].insShowField('TD','tdlbllblAmount','show');")
				Response.Write("top.frames['fraHeader'].document.forms[0].hddDoc_Amount.value ='" & FormatNumber(.nAmountDoc, 0) & "';")
				Response.Write("top.frames['fraHeader'].UpdateDiv('lblAmount','" & mobjValues.TypeToString(.nAmountDoc, eFunctions.Values.eTypeData.etdDouble, True, 0) & "');")
			Else
				Response.Write("top.frames['fraHeader'].insShowField('TD','tdlblAmount','noshow');")
				Response.Write("top.frames['fraHeader'].insShowField('TD','tdlbllblAmount','noshow');")
				Response.Write("top.frames['fraHeader'].document.forms[0].hddDoc_Amount.value =0;")
				Response.Write("top.frames['fraHeader'].UpdateDiv('lblAmount','');")
			End If
			Response.Write("top.frames['fraHeader'].UpdateDiv('lblBordereaux','" & .nBordereaux & "');")
			Response.Write("top.frames['fraHeader'].UpdateDiv('lblTypRel','" & .sRel_Type & " - " & .sDesRel_Type & "');")
			Response.Write("top.frames['fraHeader'].UpdateDiv('lblDateRel','" & .dCollect & "');")
			Response.Write("top.frames['fraHeader'].document.forms[0].dtcClient.value ='" & .sClient & "';")
			Response.Write("top.frames['fraHeader'].document.forms[0].dtcClient_Digit.value ='" & .sDigit & "';")
			Response.Write("top.frames['fraHeader'].UpdateDiv('lblCliename','" & .sCliename & "');")
			Response.Write("top.frames['fraHeader'].document.forms[0].tcdDateIncrease.value ='" & .dValueDate & "';")
			Response.Write("top.frames['fraHeader'].document.forms[0].hddSequence.value ='" & .nSequence & "';")
			Response.Write("top.frames['fraHeader'].document.forms[0].tcnBordereaux.value ='" & .nBordereaux & "';")
			If .nAgreement = 0 Then
				Response.Write("top.frames['fraHeader'].insShowField('TD','tdlbllblAgree','noshow');")
				Response.Write("top.frames['fraHeader'].insShowField('TD','tdlblAgree','noshow');")
			Else
				Response.Write("top.frames['fraHeader'].insShowField('TD','tdlbllblAgree','show');")
				Response.Write("top.frames['fraHeader'].insShowField('TD','tdlblAgree','show');")
				Response.Write("top.frames['fraHeader'].UpdateDiv('lblAgree','" & mobjValues.TypeToString(.nAgreement, eFunctions.Values.eTypeData.etdDouble, True, 0) & "');")
			End If
			If .nBank = 0 Then
				Response.Write("top.frames['fraHeader'].insShowField('TD','tdlbllblBank','noshow');")
				Response.Write("top.frames['fraHeader'].insShowField('TD','tdlblBank','noshow');")
			Else
				Response.Write("top.frames['fraHeader'].insShowField('TD','tdlbllblBank','show');")
				Response.Write("top.frames['fraHeader'].insShowField('TD','tdlblBank','show');")
				Response.Write("top.frames['fraHeader'].UpdateDiv('lblBank','" & mobjValues.TypeToString(.nBank, eFunctions.Values.eTypeData.etdDouble, True, 0) & " - " & .sDesBank & "');")
			End If
			Response.Write("top.frames['fraHeader'].UpdateDiv('lblQDoc','" & .nqDocs & "');")
			Response.Write("top.frames['fraHeader'].UpdateDiv('lblAmountRel','" & mobjValues.TypeToString(.nRel_amoun, eFunctions.Values.eTypeData.etdDouble, True, 0) & "');")
		Else
			Response.Write("top.frames['fraHeader'].insShowField('DIV','divDatRel','noshow');")
			Response.Write("top.frames['fraHeader'].insShowField('TD','tdlblAmount','noshow');")
			Response.Write("top.frames['fraHeader'].insShowField('TD','tdlbllblAmount','noshow');")
			Response.Write("top.frames['fraHeader'].document.forms[0].hddDoc_Amount.value =0;")
			Response.Write("top.frames['fraHeader'].UpdateDiv('lblAmount','');")
			Response.Write("top.frames['fraHeader'].UpdateDiv('lblBordereaux','');")
			Response.Write("top.frames['fraHeader'].UpdateDiv('lblTypRel','');")
			Response.Write("top.frames['fraHeader'].UpdateDiv('lblDateRel','');")
			Response.Write("top.frames['fraHeader'].UpdateDiv('lblAgree','');")
			Response.Write("top.frames['fraHeader'].UpdateDiv('lblBank','');")
			Response.Write("top.frames['fraHeader'].UpdateDiv('lblQDoc','');")
			Response.Write("top.frames['fraHeader'].UpdateDiv('lblAmountRel','');")
			Response.Write("top.frames['fraHeader'].document.forms[0].dtcClient.value ='';")
			Response.Write("top.frames['fraHeader'].document.forms[0].dtcClient_Digit.value ='';")
			Response.Write("top.frames['fraHeader'].UpdateDiv('lblCliename','');")
			Response.Write("top.frames['fraHeader'].document.forms[0].tcdDateIncrease.value ='" & mobjValues.TypeToString(Today, eFunctions.Values.eTypeData.etdDate) & "';")
			Response.Write("top.frames['fraHeader'].document.forms[0].hddSequence.value=0;")
			Response.Write("top.frames['fraHeader'].document.forms[0].tcnBordereaux.value ='';")
		End If
	End With
	lclsColFormRef = Nothing
End Sub

'% insShowDataCO005: Muestra los datos de la transacción de anulación/reinstalación de recibos.
'--------------------------------------------------------------------------------------------
Private Sub insShowDataCO005()
	'--------------------------------------------------------------------------------------------
	Dim lclsPremium As eCollection.Premium
	
	lclsPremium = New eCollection.Premium
	If lclsPremium.FindPremiumExist("2", eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, mobjValues.StringToType(Request.QueryString.Item("nReceipt"), eFunctions.Values.eTypeData.etdDouble), 0, 0, 1) Then
		Response.Write("top.frames['fraHeader'].insShowField('DIV','divDatRec','show');")
		With lclsPremium
			Response.Write("top.frames['fraHeader'].document.forms[0].hddBranch.value='" & .nBranch & "';")
			Response.Write("top.frames['fraHeader'].UpdateDiv('lblBranch','" & .sDesBranch & "');")
			Response.Write("top.frames['fraHeader'].document.forms[0].hddProduct.value='" & .nProduct & "';")
			Response.Write("top.frames['fraHeader'].UpdateDiv('lblProduct','" & .sDesProduct & "');")
			Response.Write("top.frames['fraHeader'].UpdateDiv('lblPolicy','" & .nPolicy & "');")
			Response.Write("top.frames['fraHeader'].UpdateDiv('lblClient','" & .sClient & "-" & .sCliename & "');")
			Response.Write("top.frames['fraHeader'].UpdateDiv('lblOffice','" & .sDesOffice & "');")
			Response.Write("top.frames['fraHeader'].UpdateDiv('lblCurrency','" & .sDesCurrency & "');")
			Response.Write("top.frames['fraHeader'].UpdateDiv('lblStatus_pre','" & .sDesStatus_pre & "');")
			Response.Write("top.frames['fraHeader'].UpdateDiv('lblTratypei','" & .sDescTratypei & "');")
			Response.Write("top.frames['fraHeader'].document.forms[0].cbeCause.value ='" & .nNullCode & "';")
			If .nNullCode = 0 Then
				Response.Write("top.frames['fraHeader'].document.forms[0].cbeCause.disabled=false;")
				Response.Write("top.frames['fraHeader'].document.forms[0].optAnul[0].checked=true;")
			Else
				Response.Write("top.frames['fraHeader'].document.forms[0].cbeCause.disabled=true;")
				Response.Write("top.frames['fraHeader'].document.forms[0].optAnul[1].checked=true;")
			End If
		End With
	Else
		Response.Write("top.frames['fraHeader'].insShowField('DIV','divDatRec','noshow');")
		Response.Write("top.frames['fraHeader'].document.forms[0].hddBranch.value='';")
		Response.Write("top.frames['fraHeader'].UpdateDiv('lblBranch','');")
		Response.Write("top.frames['fraHeader'].document.forms[0].hddProduct.value='';")
		Response.Write("top.frames['fraHeader'].UpdateDiv('lblProduct','');")
		Response.Write("top.frames['fraHeader'].UpdateDiv('lblPolicy','');")
		Response.Write("top.frames['fraHeader'].UpdateDiv('lblClient','');")
		Response.Write("top.frames['fraHeader'].UpdateDiv('lblCurrency','');")
		Response.Write("top.frames['fraHeader'].UpdateDiv('lblOffice','');")
		Response.Write("top.frames['fraHeader'].UpdateDiv('lblStatus_pre','');")
		Response.Write("top.frames['fraHeader'].UpdateDiv('lblTratypei','');")
		Response.Write("top.frames['fraHeader'].document.forms[0].cbeCause.value=0;")
	End If
	
	lclsPremium = Nothing
End Sub

Sub GenDirect()
	
	Dim lobjRoles As ePolicy.Roles
	Dim sClientEmp As String
	Dim sClientPay As String
	
	
	lobjRoles = New ePolicy.Roles
	If lobjRoles.Find("2", mobjValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nCertif"), eFunctions.Values.eTypeData.etdDouble), 25, "", mobjValues.StringToType(Request.QueryString.Item("dDate"), eFunctions.Values.eTypeData.etdDate), True) Then
		sClientPay = lobjRoles.sClient
	End If
	If lobjRoles.Find("2", mobjValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nCertif"), eFunctions.Values.eTypeData.etdDouble), 85, "", mobjValues.StringToType(Request.QueryString.Item("dDate"), eFunctions.Values.eTypeData.etdDate), True) Then
		sClientEmp = lobjRoles.sClient
	End If
	
	
	If sClientEmp = sClientPay And sClientPay <> vbNullString Then
		Response.Write("top.frames['fraFolder'].document.forms[0].optDirect[0].checked=true;")
	Else
		Response.Write("top.frames['fraFolder'].document.forms[0].optDirect[1].checked=true;")
	End If
	lobjRoles = Nothing
End Sub


Sub GenAgreement()
	
	Dim lintCod_Agree As Object
	Dim nCod_Agree As Integer
	Dim lobjRoles As ePolicy.Roles
	Dim lobjAgreements As eCollection.Agreements
	Dim lobjAgreement As eCollection.Agreement
	nCod_Agree = 0
	lobjRoles = New ePolicy.Roles
	If lobjRoles.Find("2", mobjValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nCertif"), eFunctions.Values.eTypeData.etdDouble), 25, Request.QueryString.Item("dClientPay"), mobjValues.StringToType(Request.QueryString.Item("dDate"), eFunctions.Values.eTypeData.etdDate), True) Then
		If lobjRoles.sClient <> vbNullString Then
			lobjAgreements = New eCollection.Agreements
			Call lobjAgreements.Find_sClient(0, lobjRoles.sClient, True)
			For	Each lobjAgreement In lobjAgreements
				If lobjAgreement.nType_rec = 8 Then
					nCod_Agree = lobjAgreement.nCod_Agree
					Exit For
				End If
			Next lobjAgreement
			lobjAgreements = Nothing
			lobjAgreement = Nothing
			If nCod_Agree = 0 Then
				lobjAgreement = New eCollection.Agreement
				lobjAgreement.nTypeAgree = 1
				lobjAgreement.sClient = lobjRoles.sClient
				lobjAgreement.nUsercode = Session("nUsercode")
				lobjAgreement.nIntermed = eRemoteDB.Constants.intNull
				lobjAgreement.nType_rec = 8
				lobjAgreement.sStatregt = "1"
				lobjAgreement.dInit_date = Today
				lobjAgreement.nAgency = eRemoteDB.Constants.intNull
				lobjAgreement.sCliename = lobjRoles.sCliename
				lobjAgreement.sName_Agree = lobjRoles.sCliename
				lobjAgreement.Add()
				nCod_Agree = lobjAgreement.nCod_Agree
				lobjAgreement = Nothing
			End If
			If nCod_Agree <> 0 Then
				Response.Write("top.frames['fraFolder'].document.forms[0].valAgreementNew.value='" & nCod_Agree & "';")
				Response.Write("top.frames['fraFolder'].UpdateDiv(""valAgreementNewDesc"",'" & lobjRoles.sCliename & "','Normal');")
				Response.Write("top.frames['fraFolder'].document.forms[0].valAgreementNew.disabled = true;")
				Response.Write("top.frames['fraFolder'].document.forms[0].btnvalAgreementNew.disabled = true;")
			End If
		End If
	End If
	lobjRoles = Nothing
	
End Sub


Sub GenAgreement2()
	Dim nCod_Agree As Integer
	Dim lobjAgreements As eCollection.Agreements
	Dim lobjAgreement As eCollection.Agreement
	Dim lobjClient As eClient.Client
	nCod_Agree = 0
	
	
	lobjClient = New eClient.Client
	
	If lobjClient.Find(Request.QueryString.Item("dClientPay"), True) Then
		lobjAgreements = New eCollection.Agreements
		
		Call lobjAgreements.Find_sClient(0, Request.QueryString.Item("dClientPay"), True)
		For	Each lobjAgreement In lobjAgreements
			If lobjAgreement.nType_rec = 8 Then
				nCod_Agree = lobjAgreement.nCod_Agree
				Exit For
			End If
		Next lobjAgreement
		lobjAgreements = Nothing
		lobjAgreement = Nothing
		If nCod_Agree = 0 Then
			lobjAgreement = New eCollection.Agreement
			lobjAgreement.nTypeAgree = 1
			lobjAgreement.sClient = lobjClient.sClient
			lobjAgreement.nUsercode = Session("nUsercode")
			lobjAgreement.nIntermed = eRemoteDB.Constants.intNull
			lobjAgreement.nType_rec = 8
			lobjAgreement.sStatregt = "1"
			lobjAgreement.dInit_date = Today
			lobjAgreement.nAgency = eRemoteDB.Constants.intNull
			lobjAgreement.sCliename = lobjClient.sCliename
			lobjAgreement.sName_Agree = lobjClient.sCliename
			lobjAgreement.Add()
			nCod_Agree = lobjAgreement.nCod_Agree
			lobjAgreement = Nothing
		End If
		If nCod_Agree <> 0 Then
			Response.Write("top.frames['fraFolder'].document.forms[0].valAgreementNew.value='" & nCod_Agree & "';")
			Response.Write("top.frames['fraFolder'].UpdateDiv(""valAgreementNewDesc"",'" & lobjClient.sCliename & "','Normal');")
                Response.Write("top.frames['fraFolder'].document.forms[0].valAgreementNew.disabled = false;")
                Response.Write("top.frames['fraFolder'].document.forms[0].btnvalAgreementNew.disabled = false;")
		End If
	End If
End Sub

'% insUpdSelCO982: Se encarga de actualizar el campo sel de la transacción CO982.
'--------------------------------------------------------------------------------------------
Private Sub insUpdSelCO982()
	'--------------------------------------------------------------------------------------------
	Dim lclsReject_Cause As eCollection.Reject_cause
	
	lclsReject_Cause = New eCollection.Reject_cause
	
	'+ Se actualiza el campo sSel de la tabla temporal tmp_co982 para seleccionar o deseleccionar el registro.
	Call lclsReject_Cause.insPostCO982Upd(Request.QueryString.Item("sKey"), mobjValues.StringToType(Request.QueryString.Item("nBulletins"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble), Request.QueryString.Item("sSel"))
	
	lclsReject_Cause = Nothing
End Sub

'% insUpdSelCO982: Se encarga de actualizar el campo sel de la transacción CO982.
'--------------------------------------------------------------------------------------------
Private Sub InsUpdCO982check()
	'--------------------------------------------------------------------------------------------
	Dim lclsReject_Cause As eCollection.Reject_cause
	
	lclsReject_Cause = New eCollection.Reject_cause
	
	'+ Se actualiza el campo sSel de la tabla temporal tmp_co982 para marcar o desmarcar todos los registros
	Call lclsReject_Cause.insPostCO982UpdAll(Request.QueryString.Item("sKey"), Request.QueryString.Item("sSel"))
	'Response.Write "top.frames['fraFolder'].insReload('" & Request.QueryString("sKey") & "');"
	Response.Write("top.frames['fraFolder'].document.location.reload();")
	lclsReject_Cause = Nothing
End Sub

</script>
<%Response.Expires = -1
mobjValues = New eFunctions.Values
%>
<HTML>
<HEAD>
<%
With Response
	.Write(mobjValues.StyleSheet())
End With
%>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>




<SCRIPT>
//+ Variable para el control de versiones
	     document.VssVersion="$$Revision: 4 $|$$Date: 8/10/09 3:32p $|$$Author: Gletelier $"

//%insgetDV: Obtiene el dígito verificador de sClient
//--------------------------------------------------------------------------------------------
function insgetDV(sClient) {
//--------------------------------------------------------------------------------------------
	llngFactor = 2;
	llngSummary = 0;

	for (i = sClient.length-1;i>=0; i--){
	    if (llngFactor == 8){
	        llngFactor = 2;
	    };
	    llngSummary = llngSummary + sClient.substr(i,1)*llngFactor;
	    llngFactor++;
	};
		 
	llngRUT = llngSummary%11;
	llngRUT = 11 - llngRUT;
 
	switch (llngRUT){
	     case 11: 
				return "0";
	     case 10: 
				return "K";
	     default:
				return llngRUT.toString();
	};
}
//--------------------------------------------------------------------------------------------
</SCRIPT>

<%
If Request.QueryString.Item("Field") = "COL635_REP" Then
	Call insPrintCollection()
Else
	Response.Write("<SCRIPT>")
	
	Select Case Request.QueryString.Item("Field")
		Case "ShowDataCO003"
			Call insShowDataCO003()
		Case "ShowDataCO009"
			Call insShowDataCO009()
		Case "Blank"
			Call insShowBlankCO009()
		Case "Bulletins"
			Call insShowDesc()
		Case "UpdSelCO501"
			Call insUpdSelCO501()
		Case "ShowDataCO632"
			Call insShowDataCO632()
		Case "UpdSelCO632"
			Call insUpdSelCO632()
		Case "ShowBulletinsCO632"
			Call insShowBulletinsCO632()
		Case "delCO632"
			Call insDelCO632()
		Case "CO633"
			Call insFindDocumentCO633()
		Case "ShowDataCO634"
			Call insShowDataCO634()
		Case "CO635"
			Call insFindCollector()
		Case "Blank2"
			Call insShowBlankCO675()
		Case "Receipt_1"
			Call insShowDataCO004()
		Case "Receipt_2"
			Call insShowDataCO675()
		Case "ShowDataCO700"
			Call insShowDataCO700()
		Case "UpdSelCO700"
			'Call insUpdSelCO700() ' Desde la CO700 no se invoca
		Case "ShowDataCO722"
			Call insShowDataCO722()
		Case "ChangeDefValues_CO004"
			Call ChangeDefValues_CO004()
		Case "insShowPolicy"
			Call insShowPolicy()
		Case "InsPrint"
			Call insUpd_print()
		Case "ShowDataCO501"
			Call insShowDataCO501()
		Case "ShowDataCO788"
			Call insShowDataCO788()
		Case "ShowPolicyCO788"
			Call insShowPolicyCO788()
		Case "ShowDefValuesCO004"
			Call ShowDefValuesCO004()
		Case "Contrat_CO004"
			Call Contrat_CO004()
		Case "ShowDataCO005"
			Call insShowDataCO005()
		Case "GenDirect"
			Call GenDirect()
		Case "GenAgreement"
			Call GenAgreement()
		Case "GenAgreement2"
			Call GenAgreement2()
		Case "insUpdSelCO982"
			Call insUpdSelCO982()
		Case "InsUpdCO982check"
			Call InsUpdCO982check()
	End Select
	
	Response.Write(mobjValues.CloseShowDefValues(Request.QueryString.Item("sFrameCaller")))
	Response.Write("</SCRIPT>")
	mobjValues = Nothing
End If

%>
</HEAD>
<BODY>
	<FORM NAME="ShowValues">
	</FORM>
</BODY>
</HTML>





