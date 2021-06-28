Option Strict Off
Option Explicit On
Public Class T_concepts
	'%-------------------------------------------------------%'
	'% $Workfile:: T_concepts.cls                           $%'
	'% $Author:: Nvaplat40                                  $%'
	'% $Date:: 13/09/04 8:30p                               $%'
	'% $Revision:: 15                                       $%'
	'%-------------------------------------------------------%'
	'   Name                                    Type
	'   -----------------------------------------------------
	Public sSel As String 'Char(1)
	Public nBordereaux As Double 'Number(10)
	Public nTransac As Integer 'Number(5)
	Public nSequence As Integer 'Number(5)
	Public nConcept As Integer 'Number(5)
	Public nBranch As Integer 'Number(5)
	Public nProduct As Integer 'Number(5)
	Public nProponum As Double 'Number(10)
	Public nCertif As Double 'Number(10)
	Public sClient As String 'Char(14)
	Public nOricurr As Integer 'Number(5)
	Public nOriAmount As Double 'Number(18,6)
	Public nCurrency As Integer 'Number(5)
	Public nAmount As Double 'Number(18,6)
	Public nExchange As Double 'Number(11,6)
	Public dValDate As Date 'Date
	Public nChangeDat As Integer 'Number(5)
	Public nClaim As Double 'Number(10)
	Public nCase_num As Integer 'Number(5)
	Public nDeman_Type As Integer 'Number(5)
	Public nBank_code As Double 'Number(10)
	Public nBank_Agree As Integer 'Number(5)
	Public nAgreement As Integer 'Number(5)
	Public nNoteNum As Integer 'Number(5)
	Public Nsuport_Id As Double 'Number(10)
	Public NtypeSupport As Integer 'Number(5)
	Public Dcollection As Date 'Date
	Public nCash_Id As Double 'Number(10)
	Public nMov_Type As Integer 'Number(5)
	Public nTyp_acco As Integer 'Number(5)
	Public nIntermed As Double 'Number(10)
	Public nCompany As Integer 'Number(5)
	Public nAccount As Integer 'Number(5)
	Public sType_BankAgree As String
	Public nBulletins As Double
	Public sCaseNum As String
	Public nLoan As Integer 'Number(5)
	
	Public sConcept As String
	Public sCliename As String
	Public sBank_agree As String
	Public sCurrency As String
	Public sAgreement As String
	Public sIntermed As String
	Public sAccount As String
	Public sTyp_acco As String
	Public sLoan As String
	
	'% insValCO823Upd: Se efectuan las validaciones de la ventana CO823.
	Public Function insValCO823Upd(ByVal nConcept As Integer, ByVal nCurrency As Integer, ByVal nAmountOrig As Double, ByVal dValueDate As Date, ByVal nBank_Agree As Integer, ByVal nCod_Agree As Integer, ByVal nAgreement As Integer, ByVal dCollect As Date, ByVal nBulletins As Double, ByVal sClient As String, ByVal nProponum As Double, ByVal nClaim As Double, ByVal nCase_num As Double, ByVal nCurrAcc As Integer, ByVal nIntermed As Integer, ByVal nCompanyCR As Integer, ByVal nCashnum As Integer, ByVal dDate_collect As Date, ByVal nUsercode As Integer, ByVal sCodispl As String, ByVal nLoan As Integer, ByVal nBordereaux As Double) As String
		Dim lobjErrors As eFunctions.Errors
		Dim lclsUser_CashNum As Object
		Dim lobjcash_bank As Object
		Dim lstrErrors As String
		Dim lclsClaim As Object
		
		On Error GoTo insValCO823Upd_Err
		
		lobjErrors = New eFunctions.Errors
		lclsUser_CashNum = eRemoteDB.NetHelper.CreateClassInstance("eCashBank.User_cashnum")
		lobjcash_bank = eRemoteDB.NetHelper.CreateClassInstance("eCashBank.Cash_stat")
		lclsClaim = eRemoteDB.NetHelper.CreateClassInstance("eClaim.Claim")
		
		With lobjErrors
			'+Validación sobre el concepto
			If nConcept <= 0 Then
				.ErrorMessage(sCodispl, 7005)
			End If
			
			'+Validación sobre la moneda origen
			If nCurrency <= 0 Then
				.ErrorMessage(sCodispl, 60122)
			End If
			
			'+Validación sobre el monto en moneda origen
			If nAmountOrig <= 0 Then
				.ErrorMessage(sCodispl, 5036)
			End If
			
			'+Validación sobre el banco-convenio
			If nConcept = 29 Or nConcept = 36 Then
				If nBank_Agree <= 0 Then
					.ErrorMessage(sCodispl, 60132)
				End If
			End If
			
			'+Validación sobre el convenio-descuento por planilla
			If nConcept = 38 Then
				If nAgreement <= 0 Then
					.ErrorMessage(sCodispl, 60117)
				End If
			End If
			
			'+Validación sobre la fecha de cobranza
			'If nConcept = 29 Or nConcept = 38 Then
			'	If dCollect = eRemoteDB.Constants.dtmNull Then
			'		.ErrorMessage(sCodispl, 60133)
			'	End If
			'End If
			
			'+Validación sobre el aviso
			If nConcept = 35 Then
				If nBulletins <= 0 Then
					.ErrorMessage(sCodispl, 55019)
				End If
			End If

            If nConcept < 200 Or nConcept > 500 Then
                '+Validación sobre el cliente

                If sClient = String.Empty Then
                    .ErrorMessage(sCodispl, 21118)
                End If
            End If

            '+Validación sobre la propuesta
            If nConcept = 26 Then
                If nProponum <= 0 Then
                    .ErrorMessage(sCodispl, 3789)
                End If
            End If

            '+Validación sobre el siniestro
            If nConcept = 30 Or nConcept = 31 Or nConcept = 32 Or nConcept = 114 Then
                If nClaim <= 0 Then
                    .ErrorMessage(sCodispl, 7022)
                Else

                    '+ Se valida que el siniestro sea un siniestro válido en la BD

                    If Not lclsClaim.Find(nClaim) Then
                        .ErrorMessage(sCodispl, 7023)
                    Else

                        '+ Se valida que se haya introducido un caso asociado al siniestro. Si se introdujo algún caso, se tiene la certeza
                        '+ que está asociado al siniestro pues es un valores posibles cuyo parámetro es el número del siniestro introducido previamente

                        If nCase_num <= 0 Then
                            .ErrorMessage(sCodispl, 4289)
                        End If
                    End If
                End If
            End If

            '+Validación sobre la Cta. Cte.

            If nConcept = 10 Then
                If nCurrAcc <= 0 Then
                    .ErrorMessage(sCodispl, 7107)
                End If
            End If

            '+Validación sobre el intermediario cuando se trata de: 2)remesa de agente y/o 46)Abono de anticipo de comisión.
            If nConcept = 2 Or nConcept = 46 Then
                If nIntermed <= 0 Then
                    .ErrorMessage(sCodispl, 7020)
                Else
                    '+ Si se trata de 46)Abono de anticipo de comisión el número de anticipo debe estar lleno
                    If nConcept = 46 Then
                        If nLoan <= 0 Then
                            .ErrorMessage(sCodispl, 3963)
                        End If
                    End If
                End If
            End If

            '+Validación sobre la Co/Reaseguro
            If nConcept = 3 Then
                If nCompanyCR <= 0 Then
                    .ErrorMessage(sCodispl, 7024)
                End If
            End If


            '+Validaciones que se realizan el la BD
            lstrErrors = InsValCO823DB(nConcept, nCashnum, nClaim, nCase_num, dDate_collect, nIntermed, dValueDate, nBranch, nUsercode, nBordereaux)

            Call lobjErrors.ErrorMessage(sCodispl, , , , , , lstrErrors)

            insValCO823Upd = .Confirm
        End With
		
		'UPGRADE_NOTE: Object lclsUser_CashNum may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsUser_CashNum = Nothing
		'UPGRADE_NOTE: Object lobjcash_bank may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjcash_bank = Nothing
		'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjErrors = Nothing
		'UPGRADE_NOTE: Object lclsClaim may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsClaim = Nothing
		
insValCO823Upd_Err: 
		If Err.Number Then
			'UPGRADE_NOTE: Object lclsUser_CashNum may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			lclsUser_CashNum = Nothing
			'UPGRADE_NOTE: Object lobjcash_bank may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			lobjcash_bank = Nothing
			'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			lobjErrors = Nothing
			'UPGRADE_NOTE: Object lclsClaim may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			lclsClaim = Nothing
			insValCO823Upd = insValCO823Upd & Err.Description
		End If
		On Error GoTo 0
		
		'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjErrors = Nothing
	End Function
	
	'% insValCO823: Se efectuan las validaciones de la ventana CO823.
	Public Function insValCO823(ByVal nBordereaux As Double, ByVal nItems As Integer) As String
		Dim lclsErrors As eFunctions.Errors
		Dim lclsColformRef As ColformRef
		Dim lstrConWin As String
		
		lclsErrors = New eFunctions.Errors
		lclsColformRef = New ColformRef
		
		On Error GoTo insValCO823_Err
		
		With lclsErrors
			'+ Si no existen registros
			If nItems <= 0 Then
				.ErrorMessage("CO823", 750055)
				lstrConWin = lclsColformRef.getConWinRel(nBordereaux)
				If Mid(lstrConWin, 1, 1) <> "3" Then
					lclsColformRef.sConwin = "3" & Mid(lstrConWin, 2)
					lclsColformRef.UpdateConWin()
				End If
			End If
			
			insValCO823 = .Confirm
		End With
		
insValCO823_Err: 
		If Err.Number Then
			insValCO823 = insValCO823 & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		'UPGRADE_NOTE: Object lclsColformRef may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsColformRef = Nothing
	End Function
	
	'%InsValco823: Este metodo se encarga de realizar las validaciones que son accesando la BD
	'%             descritas en el funcional de la ventana "CO823"
	Private Function InsValCO823DB(ByVal nConcept As Integer, ByVal nCashnum As Integer, ByVal nClaim As Double, ByVal nCase_num As Double, ByVal dEffecdate As Date, ByVal nIntermed As Double, ByVal dValueDate As Date, ByVal nBranch As Integer, ByVal nUsercode As Integer, ByVal nBordereaux As Double) As String
		Dim lrecInsValCO823DB As eRemoteDB.Execute
		
		'+Definición de parámetros para stored procedure 'InsValOP001'
		'+Información leída el 10/04/2003
		
		On Error GoTo InsValCO823DB_Err
		lrecInsValCO823DB = New eRemoteDB.Execute
		
		With lrecInsValCO823DB
			.StoredProcedure = "InsValCO823"
			.Parameters.Add("nConcept", nConcept, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCashnum", nCashnum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCase_Num", nCase_num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nIntermed", nIntermed, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dValueDate", dValueDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBordereaux", nBordereaux, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("Arrayerrors", String.Empty, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 4000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				InsValCO823DB = .Parameters("Arrayerrors").Value
			End If
		End With
		
InsValCO823DB_Err: 
		If Err.Number Then
			InsValCO823DB = "InsValCO823DB: " & Err.Description
		End If
		'UPGRADE_NOTE: Object lrecInsValCO823DB may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecInsValCO823DB = Nothing
		On Error GoTo 0
	End Function
	
	'%insPostCO823Upd: Este método se encarga de actualizar registros en la tabla "T_Concepts". Devolviendo verdadero o
	'%falso dependiendo de si el Stored procedure se ejecutó correctamente.
	Public Function insPostCO823Upd(ByVal sAction As String, ByVal nConcept As Integer, ByVal nCurrency As Integer, ByVal nAmountOrig As Double, ByVal nAmount As Double, ByVal dValueDate As Date, ByVal nBank_Agree As Integer, ByVal nCod_Agree As Integer, ByVal nAgreement As Integer, ByVal dCollect As Date, ByVal nBulletins As Double, ByVal sClient As String, ByVal nProponum As Double, ByVal nClaim As Double, ByVal nCase_num As Double, ByVal nTyp_acco As Integer, ByVal nIntermed As Double, ByVal nCompanyCR As Integer, ByVal nCashnum As Integer, ByVal dDate_collect As Date, ByVal nUsercode As Integer, ByVal nBordereaux As Double, ByVal nTransac As Integer, ByVal nExchange As Double, ByVal nDeman_Type As Integer, ByVal nNoteNum As Integer, ByVal nLoan As Integer) As Boolean
		Dim lclsT_Concepts As T_concepts
		lclsT_Concepts = New T_concepts
		
		On Error GoTo insPostCO823Upd_Err
		
		insPostCO823Upd = True
		
		With lclsT_Concepts
			.nBordereaux = nBordereaux
			.nConcept = nConcept
			.nCurrency = nCurrency
			.nOriAmount = nAmountOrig
			.nAmount = nAmount
			.dValDate = dValueDate
			.nBank_Agree = nBank_Agree
			If nConcept = 29 Or nConcept = 36 Then
				.sType_BankAgree = IIf(nConcept = 29, 1, 2)
			Else
				.sType_BankAgree = String.Empty
			End If
			.nAccount = nCod_Agree
			.nAgreement = nAgreement
			.Dcollection = dCollect
			.sClient = sClient
			.nProponum = nProponum
			.nClaim = nClaim
			.nCase_num = nCase_num
			.nDeman_Type = nDeman_Type
			.nTyp_acco = nTyp_acco
			.nIntermed = nIntermed
			.nCompany = nCompanyCR
			.nTransac = nTransac
			.nExchange = nExchange
			.nBulletins = nBulletins
			.nNoteNum = nNoteNum
			.nLoan = nLoan
			
			If sAction = "Add" Then
				insPostCO823Upd = .Add
			ElseIf sAction = "Update" Then 
				insPostCO823Upd = .Update
			End If
			
		End With
		
insPostCO823Upd_Err: 
		If Err.Number Then
			insPostCO823Upd = False
		End If
		
		'UPGRADE_NOTE: Object lclsT_Concepts may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsT_Concepts = Nothing
		On Error GoTo 0
		
	End Function
	
	'% insPostCO823: Se ejecuta la actualización de las tablas de la CO823.
	Public Function insPostCO823(ByRef nBordereaux As Double, ByVal nItems As Integer, ByVal nUsercode As Integer) As Boolean
		On Error GoTo insPostCO823_Err
		
		insPostCO823 = UpdColformRefCO823(nBordereaux, nUsercode)
		
insPostCO823_Err: 
		If Err.Number Then
			insPostCO823 = False
		End If
		On Error GoTo 0
	End Function
	
	
	'%Del_T_Concepts: Este método se encarga de eliminar los registros en T_Concepts
	Public Function Del_T_Concepts(ByVal nBordereaux As Double, ByVal nTransac As Integer) As Boolean
		Dim lclsT_Concepts As T_concepts
		lclsT_Concepts = New T_concepts
		
		On Error GoTo Del_T_Concepts_Err
		
		Del_T_Concepts = True
		
		With lclsT_Concepts
			.nBordereaux = nBordereaux
			.nTransac = nTransac
			
			Del_T_Concepts = .Delete
			
		End With
		
Del_T_Concepts_Err: 
		If Err.Number Then
			Del_T_Concepts = CBool("Del_T_Concepts: " & Err.Description)
		End If
		
		'UPGRADE_NOTE: Object lclsT_Concepts may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsT_Concepts = Nothing
		On Error GoTo 0
		
	End Function
	
	
	'%Add: Este método se encarga de crear los registros en T_Concepts
	Public Function Add() As Boolean
		Dim lrecT_Concepts As eRemoteDB.Execute
		
		lrecT_Concepts = New eRemoteDB.Execute
		
		With lrecT_Concepts
			.StoredProcedure = "CreT_Concepts"
			.Parameters.Add("nBordereaux", nBordereaux, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nConcept", nConcept, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOriAmount", nOriAmount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAmount", nAmount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nExchange", nExchange, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dValDate", dValDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBank_Agree", nBank_Agree, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAgreement", nAgreement, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("Dcollection", Dcollection, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProponum", nProponum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCase_Num", nCase_num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDeman_type", nDeman_Type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTyp_acco", nTyp_acco, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nIntermed", nIntermed, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCompany", nCompany, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAccount", nAccount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sType_BankAgree", sType_BankAgree, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBulletins", nBulletins, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nNotenum", nNoteNum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nLoan", nLoan, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			Add = .Run(False)
		End With
		
		'UPGRADE_NOTE: Object lrecT_Concepts may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecT_Concepts = Nothing
	End Function
	
	'%Update: Este método se encarga de Actualizar los registros en T_Concepts
	Public Function Update() As Boolean
		Dim lrecT_Concepts As eRemoteDB.Execute
		
		lrecT_Concepts = New eRemoteDB.Execute
		
		With lrecT_Concepts
			.StoredProcedure = "UpdT_Concepts"
			.Parameters.Add("nBordereaux", nBordereaux, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nConcept", nConcept, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTransac", nTransac, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOriAmount", nOriAmount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAmount", nAmount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nExchange", nExchange, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dValDate", dValDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBank_Agree", nBank_Agree, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAgreement", nAgreement, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("Dcollection", Dcollection, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProponum", nProponum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCase_Num", nCase_num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDeman_type", nDeman_Type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTyp_acco", nTyp_acco, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nIntermed", nIntermed, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCompany", nCompany, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAccount", nAccount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sType_BankAgree", sType_BankAgree, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBulletins", nBulletins, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nNotenum", nNoteNum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			Update = .Run(False)
		End With
		
		'UPGRADE_NOTE: Object lrecT_Concepts may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecT_Concepts = Nothing
	End Function
	
	'%Delete: Este método se encarga de eliminar los registros en T_Concepts
	Public Function Delete() As Boolean
		Dim lrecT_Concepts As eRemoteDB.Execute
		
		lrecT_Concepts = New eRemoteDB.Execute
		
		With lrecT_Concepts
			.StoredProcedure = "DelT_Concepts"
			.Parameters.Add("nBordereaux", nBordereaux, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTransac", nTransac, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			Delete = .Run(False)
		End With
		
		'UPGRADE_NOTE: Object lrecT_Concepts may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecT_Concepts = Nothing
	End Function
	
	'%UpdColformRefCO823: Permite actualizar el campo sType de una relación.
	Public Function UpdColformRefCO823(ByVal nBordereaux As Double, ByVal nUsercode As Integer) As Boolean
		Dim lrecColFormRef As eRemoteDB.Execute
		
		On Error GoTo Err_UpdColformRefCO823
		
		lrecColFormRef = New eRemoteDB.Execute
		
		With lrecColFormRef
			.StoredProcedure = "insUpdColFormRef_CO823"
			.Parameters.Add("nBordereaux", nBordereaux, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			UpdColformRefCO823 = .Run(False)
		End With
		
Err_UpdColformRefCO823: 
		If Err.Number Then
			UpdColformRefCO823 = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecColFormRef may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecColFormRef = Nothing
	End Function
End Class






