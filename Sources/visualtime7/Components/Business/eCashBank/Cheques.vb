Option Strict Off
Option Explicit On
Public Class Cheques
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: Cheques.cls                              $%'
	'% $Author:: Nvaplat11                                  $%'
	'% $Date:: 3/11/03 7:19p                                $%'
	'% $Revision:: 33                                       $%'
	'%-------------------------------------------------------%'
	
	Private mCol As Collection
	
	'**- Auxiliary Variables
	'- Variables auxiliares
	
	Private mdtmStat_date As Date
	Private mintSta_cheque As Integer
	
	'**% Add: Adds a new instance in the Cash_mov class to the collection.
	'% Add: Añade una nueva instancia de la clase Cash_mov a la colección
    Public Function Add(ByVal nRequest_nu As Double, ByVal sCheque As String, ByVal nConsec As Integer, _
                        ByVal nAmount As Double, ByVal nConcept As Integer, ByVal sClient As String, _
                        ByVal nBranch_Led As Integer, ByVal nClaim As Double, ByVal nVoucher_le As Integer, _
                        ByVal nVoucher As Integer, ByVal dDat_propos As Date, ByVal sDescript As String, _
                        ByVal dIssue_Dat As Date, ByVal dLedger_dat As Date, ByVal nNullcode As Integer, _
                        ByVal dNulldate As Date, ByVal sPay_freq As String, ByVal nQ_pays As Integer, _
                        ByVal nReceipt As Integer, ByVal sRequest_ty As String, ByVal nSta_cheque As Integer, _
                        ByVal dStat_date As Date, ByVal nTransac As Integer, ByVal nUser_sol As Integer, _
                        ByVal nUsercode As Integer, ByVal nYear_month As Integer, ByVal nAcc_bank As Integer, _
                        ByVal nBordereaux As Double, ByVal sInter_pay As String, ByVal nNoteNum As Integer, _
                        ByVal nAcc_type As Integer, ByVal sAcco_num As String, ByVal nBank_code As Integer, _
                        ByVal nBk_agency As Integer, ByVal sN_Aba As String, ByVal sBenef_name As String, _
                        ByVal sInter_name As String, ByVal sUser_name As String, ByVal sBank_name As String, _
                        ByVal nBank_curr As Integer, ByVal sAcc_number As String, ByVal nCompany As Integer, _
                        ByVal nCurrencyPay As Integer, ByVal nAmountPay As Double, ByVal nTypesupport As Integer, _
                        ByVal nDocSupport As Double, ByVal nTax_code As Integer, ByVal nTax_Percent As Double, _
                        ByVal nTax_Amount As Double, ByVal nAfect As Double, ByVal nExcent As Double, _
                        ByVal nOfficePay As Integer) As Cheque
        '**- Variable definition that will have the instance to be added
        '- Se define la variable que contendra la instancia a añadir

        Dim objNewMember As Cheque
        objNewMember = New Cheque

        With objNewMember
            .nRequest_nu = nRequest_nu
            .sCheque = sCheque
            .nConsec = nConsec
            .nAmount = nAmount
            .nConcept = nConcept
            .sClient = sClient
            .nBranch_Led = nBranch_Led
            .nClaim = nClaim
            .nVoucher_le = nVoucher_le
            .nVoucher = nVoucher
            .dDat_propos = dDat_propos
            .sDescript = sDescript
            .dIssue_Dat = dIssue_Dat
            .dLedger_dat = dLedger_dat
            .nNullcode = nNullcode
            .dNulldate = dNulldate
            .sPay_freq = sPay_freq
            .nQ_pays = nQ_pays
            .nReceipt = nReceipt
            .sRequest_ty = sRequest_ty
            .nSta_cheque = nSta_cheque
            .dStat_date = dStat_date
            .nTransac = nTransac
            .nUser_sol = nUser_sol
            .nUsercode = nUsercode
            .nYear_month = nYear_month
            .nAcc_bank = nAcc_bank
            .nBordereaux = nBordereaux
            .sInter_pay = sInter_pay
            .nNoteNum = nNoteNum
            .nAcc_type = nAcc_type
            .sAcco_num = sAcco_num
            .nBank_code = nBank_code
            .nBk_agency = nBk_agency
            .sN_Aba = sN_Aba
            .sBenef_name = sBenef_name
            .sInter_name = sInter_name
            .sUser_name = sUser_name
            .sBank_name = sBank_name
            .nBank_curr = nBank_curr
            .sAcc_number = sAcc_number

            .nCompany = nCompany
            .nCurrencyPay = nCurrencyPay
            .nAmountPay = nAmountPay
            .nTypesupport = nTypesupport
            .nDocSupport = nDocSupport
            .nTax_code = nTax_code
            .nTax_Percent = nTax_Percent
            .nTax_Amount = nTax_Amount
            .nAfect = nAfect
            .nExcent = nExcent





        End With

        mCol.Add(objNewMember)

        '**+ Returns the created object
        '+ Retorna el objeto creado

        Add = objNewMember
        'UPGRADE_NOTE: Object objNewMember may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        objNewMember = Nothing
    End Function
	
	'**% Find: Restores the values of the check's applications
	'** (called from the Check's control form)
	'% Find: Devuelve los valores de las solicitudes de cheques
	'  (llamado desde la forma de Control de Cheques)
	'----------------------------------------------------------
	Public Function Find(ByVal dStartDate As Date, ByVal dEndDate As Date, ByVal nSta_cheque As Integer, ByVal nConcept As Integer, ByVal sClient As String, Optional ByVal lblnFind As Boolean = False) As Boolean
		'----------------------------------------------------------
		
		'**- Declares the the variable that determines the result of the function (True/False)
		'- Se declara la variable que determina el resultado de la funcion (True/False)
		
		Static lblnRead As Boolean
		
		'**-Variable definition lrecreaChequesOP009
		'- Se define la variable lrecreaChequesOP009
		
		Dim lrecreaChequesOP009 As eRemoteDB.Execute
		lrecreaChequesOP009 = New eRemoteDB.Execute
		
		On Error GoTo Find_Err
		
		With lrecreaChequesOP009
			.StoredProcedure = "reaChequesOP009"
			.Parameters.Add("dStarDate", dStartDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEndDate", IIf(dEndDate = dtmNull, System.DBNull.Value, dEndDate), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nSta_cheque", nSta_cheque, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nConcept", IIf(nConcept = eRemoteDB.Constants.intNull, System.DBNull.Value, nConcept), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sClient", IIf(sClient = strNull, System.DBNull.Value, sClient), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
                Do While Not .EOF

                    'Call Add(.FieldToClass("nRequest_nu"), .FieldToClass("sCheque"), .FieldToClass("nConsec"), .FieldToClass("nAmount"), eRemoteDB.Constants.intNull, .FieldToClass("sClient") & " " & .FieldToClass("sDes_Client"), eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, dtmNull, .FieldToClass("sDes_Concept"), .FieldToClass("dIssue_dat"), dtmNull, eRemoteDB.Constants.intNull, dtmNull, String.Empty, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, String.Empty, eRemoteDB.Constants.intNull, dtmNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, .FieldToClass("nAcc_bank"), eRemoteDB.Constants.intNull, String.Empty, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, String.Empty, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, String.Empty, String.Empty, String.Empty, String.Empty, .FieldToClass("sDescript") & " " & .FieldToClass("sAcc_Number"), .FieldToClass("nCurrency"), String.Empty, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull)

                    Call Add(.FieldToClass("nRequest_nu"), .FieldToClass("sCheque"), .FieldToClass("nConsec"), _
                             .FieldToClass("nAmount"), eRemoteDB.Constants.intNull, _
                             sClient, eRemoteDB.Constants.intNull, _
                             eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, dtmNull, _
                             String.Empty, .FieldToClass("dIssue_dat"), _
                             dtmNull, eRemoteDB.Constants.intNull, dtmNull, String.Empty, eRemoteDB.Constants.intNull, _
                             eRemoteDB.Constants.intNull, String.Empty, eRemoteDB.Constants.intNull, dtmNull, _
                             eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, _
                             eRemoteDB.Constants.intNull, .FieldToClass("nAcc_bank"), eRemoteDB.Constants.intNull, _
                             String.Empty, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, _
                             String.Empty, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull,
                             String.Empty, String.Empty, String.Empty, String.Empty, _
                             .FieldToClass("SBANKNAME"), _
                             .FieldToClass("nCurrency"), String.Empty, eRemoteDB.Constants.intNull, _
                             eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, _
                             eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, _
                             eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, _
                             eRemoteDB.Constants.intNull)
                    .RNext()
                Loop
				
				.RCloseRec()
				lblnRead = True
			Else
				lblnRead = False
			End If
		End With
		
		Find = lblnRead
		'UPGRADE_NOTE: Object lrecreaChequesOP009 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaChequesOP009 = Nothing
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
	End Function
	
	'**% FindOP008: Restores the values of the check's applications
	'** (called from the Check's control form)
	'% FindOP008: Devuelve los valores de las solicitudes de cheques
	'  (llamado desde la forma de Control de Cheques)
	'----------------------------------------------------------
	Public Function FindOP008(ByVal nRequest_nu As Double, ByVal sCheque As String, ByVal nConsec As Integer, ByVal nBordereaux As Double, Optional ByVal lblnFind As Boolean = False) As Boolean
		'----------------------------------------------------------
		
		'**- Declares the the variable that determines the result of the function (True/False)
		'- Se declara la variable que determina el resultado de la funcion (True/False)
		
		Static lblnRead As Boolean
		
		'**-Variable definition lrecreaChequesOP009
		'- Se define la variable lrecreaChequesOP009
		
		Dim lrecreaChequesOP008 As eRemoteDB.Execute
		lrecreaChequesOP008 = New eRemoteDB.Execute
		
		On Error GoTo FindOP008_Err
		
		With lrecreaChequesOP008
			.StoredProcedure = "reaChequesOP006"
			.Parameters.Add("nRequest_nu", nRequest_nu, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCheque", sCheque, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nConsec", nConsec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBordereaux", nBordereaux, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Do While Not .EOF
					Call Add_OP008(.FieldToClass("nRequest_nu"), .FieldToClass("sCheque"), .FieldToClass("nConsec"), .FieldToClass("nAmount", 0), .FieldToClass("nConcept"), .FieldToClass("sClient"), .FieldToClass("dDat_propos"), .FieldToClass("sDescript"), .FieldToClass("dIssue_dat"), .FieldToClass("dLedger_dat"), .FieldToClass("nAcc_bank"), .FieldToClass("sInter_pay"), .FieldToClass("nUser_sol"), .FieldToClass("nBranch_led"), .FieldToClass("nClaim"), .FieldToClass("nVoucher_le"), .FieldToClass("nVoucher"), .FieldToClass("nNullcode"), .FieldToClass("sRequest_ty"), .FieldToClass("dNulldate"), .FieldToClass("sPay_freq"), .FieldToClass("nQ_pays", 0), .FieldToClass("nReceipt"), .FieldToClass("nSta_cheque"), .FieldToClass("dStat_date"), .FieldToClass("nTransac"), .FieldToClass("nYear_month"), .FieldToClass("nBordereaux"), .FieldToClass("sBenefName"), .FieldToClass("sInterName"), .FieldToClass("sUserName"), .FieldToClass("sBankName"), .FieldToClass("nCurrency"), .FieldToClass("nNotenum"), .FieldToClass("sAcc_number"), .FieldToClass("nCompany"), .FieldToClass("nBank_code"), .FieldToClass("sClientInter"), .FieldToClass("sClientUser"))
					.RNext()
				Loop 
				
				.RCloseRec()
				lblnRead = True
			Else
				lblnRead = False
			End If
		End With
		
		FindOP008 = lblnRead
		'UPGRADE_NOTE: Object lrecreaChequesOP008 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaChequesOP008 = Nothing
		
FindOP008_Err: 
		If Err.Number Then
			FindOP008 = False
		End If
		On Error GoTo 0
	End Function
	
	'**%Find: This method fills the collection with records from the table "Cheques" returning TRUE or FALSE
	'**%depending on the existence of the records
	'%Find: Este metodo carga la coleccion de elementos de la tabla "Cheques" devolviendo Verdadero o
	'%falso, dependiendo de la existencia de los registros.
	Public Function FindOP006(ByVal lclsCheque As Cheque) As Boolean
		Dim lclsConstruct As eRemoteDB.ConstructSelect
		Dim lclsExeTime As eRemoteDB.Execute
		
		On Error GoTo FindOP006_Err
		FindOP006 = False
		lclsConstruct = New eRemoteDB.ConstructSelect
		lclsExeTime = New eRemoteDB.Execute
		
		With lclsCheque
			.nConsec = 0
			lclsConstruct.WhereClause("cheques.nConsec", eRemoteDB.ConstructSelect.eTypeValue.TypCNumeric, "=" & .nConsec, eRemoteDB.ConstructSelect.eWordConnection.eAnd)
			
			If .nRequest_nu <> eRemoteDB.Constants.intNull And .nRequest_nu <> 0 Then
				lclsConstruct.WhereClause("cheques.nRequest_nu", eRemoteDB.ConstructSelect.eTypeValue.TypCNumeric, "=" & .nRequest_nu, eRemoteDB.ConstructSelect.eWordConnection.eAnd)
			End If
			If .sCheque <> String.Empty Then
				lclsConstruct.WhereClause("cheques.sCheque", eRemoteDB.ConstructSelect.eTypeValue.TypCString, "=" & .sCheque, eRemoteDB.ConstructSelect.eWordConnection.eAnd)
			End If
			If .nCompany <> eRemoteDB.Constants.intNull And .nCompany <> 0 Then
				lclsConstruct.WhereClause("cheques.nCompany", eRemoteDB.ConstructSelect.eTypeValue.TypCNumeric, "=" & .nCompany, eRemoteDB.ConstructSelect.eWordConnection.eAnd)
			End If
			If .nConcept <> eRemoteDB.Constants.intNull And .nConcept <> 0 Then
				lclsConstruct.WhereClause("cheques.nConcept", eRemoteDB.ConstructSelect.eTypeValue.TypCNumeric, "=" & .nConcept, eRemoteDB.ConstructSelect.eWordConnection.eAnd)
			End If
			If .sDescript <> String.Empty Then
				lclsConstruct.WhereClause("cheques.sDescript", eRemoteDB.ConstructSelect.eTypeValue.TypCString, "=" & .sDescript, eRemoteDB.ConstructSelect.eWordConnection.eAnd)
			End If
			If .nCurrencyOri <> eRemoteDB.Constants.intNull And .nCurrencyOri <> 0 Then
				lclsConstruct.WhereClause("cheques.nCurrencyOri", eRemoteDB.ConstructSelect.eTypeValue.TypCNumeric, "=" & .nCurrencyOri, eRemoteDB.ConstructSelect.eWordConnection.eAnd)
			End If
			If .nAmount <> eRemoteDB.Constants.intNull And .nAmount <> 0 Then
				lclsConstruct.WhereClause("cheques.nAmount", eRemoteDB.ConstructSelect.eTypeValue.TypCNumeric, "=" & .nAmount, eRemoteDB.ConstructSelect.eWordConnection.eAnd)
			End If
			If .nOffice <> eRemoteDB.Constants.intNull And .nOffice <> 0 Then
				lclsConstruct.WhereClause("cheques.nOffice", eRemoteDB.ConstructSelect.eTypeValue.TypCNumeric, "=" & .nOffice, eRemoteDB.ConstructSelect.eWordConnection.eAnd)
			End If
			If .nCurrencyPay <> eRemoteDB.Constants.intNull And .nCurrencyPay <> 0 Then
				lclsConstruct.WhereClause("cheques.nCurrencyPay", eRemoteDB.ConstructSelect.eTypeValue.TypCNumeric, "=" & .nCurrencyPay, eRemoteDB.ConstructSelect.eWordConnection.eAnd)
			End If
			If .nAmountPay <> eRemoteDB.Constants.intNull And .nAmountPay <> 0 Then
				lclsConstruct.WhereClause("cheques.nAmountpay", eRemoteDB.ConstructSelect.eTypeValue.TypCNumeric, "=" & .nAmountPay, eRemoteDB.ConstructSelect.eWordConnection.eAnd)
			End If
			If .nTypesupport <> eRemoteDB.Constants.intNull And .nTypesupport <> 0 Then
				lclsConstruct.WhereClause("cheques.nTypesupport", eRemoteDB.ConstructSelect.eTypeValue.TypCNumeric, "=" & .nTypesupport, eRemoteDB.ConstructSelect.eWordConnection.eAnd)
			End If
			If .nDocSupport <> eRemoteDB.Constants.intNull And .nDocSupport <> 0 Then
				lclsConstruct.WhereClause("cheques.nDocSupport", eRemoteDB.ConstructSelect.eTypeValue.TypCNumeric, "=" & .nDocSupport, eRemoteDB.ConstructSelect.eWordConnection.eAnd)
			End If
			If .nTax_code <> eRemoteDB.Constants.intNull And .nTax_code <> 0 Then
				lclsConstruct.WhereClause("cheques.nTaxCode", eRemoteDB.ConstructSelect.eTypeValue.TypCNumeric, "=" & .nTax_code, eRemoteDB.ConstructSelect.eWordConnection.eAnd)
			End If
			If .nTax_Percent <> eRemoteDB.Constants.intNull And .nTax_Percent <> 0 Then
				lclsConstruct.WhereClause("cheques.nTax_percent", eRemoteDB.ConstructSelect.eTypeValue.TypCNumeric, "=" & .nTax_Percent, eRemoteDB.ConstructSelect.eWordConnection.eAnd)
			End If
			If .nTax_Amount <> eRemoteDB.Constants.intNull And .nTax_Amount <> 0 Then
				lclsConstruct.WhereClause("cheques.nTax_amount", eRemoteDB.ConstructSelect.eTypeValue.TypCNumeric, "=" & .nTax_Amount, eRemoteDB.ConstructSelect.eWordConnection.eAnd)
			End If
			If .nAfect <> eRemoteDB.Constants.intNull And .nAfect <> 0 Then
				lclsConstruct.WhereClause("cheques.nAfect", eRemoteDB.ConstructSelect.eTypeValue.TypCNumeric, "=" & .nAfect, eRemoteDB.ConstructSelect.eWordConnection.eAnd)
			End If
			If .nExcent <> eRemoteDB.Constants.intNull And .nExcent <> 0 Then
				lclsConstruct.WhereClause("cheques.nExent", eRemoteDB.ConstructSelect.eTypeValue.TypCNumeric, "=" & .nExcent, eRemoteDB.ConstructSelect.eWordConnection.eAnd)
			End If
			If .sClient <> String.Empty Then
				lclsConstruct.WhereClause("cheques.sClient", eRemoteDB.ConstructSelect.eTypeValue.TypCString, "=" & .sClient, eRemoteDB.ConstructSelect.eWordConnection.eAnd)
			End If
			
			If .dDat_propos <> dtmNull Then
				lclsConstruct.WhereClause("cheques.dDat_propos", eRemoteDB.ConstructSelect.eTypeValue.TypCDate, "=" & .dDat_propos, eRemoteDB.ConstructSelect.eWordConnection.eAnd)
			End If
			If .dLedger_dat <> dtmNull Then
				lclsConstruct.WhereClause("cheques.dLedger_dat", eRemoteDB.ConstructSelect.eTypeValue.TypCDate, "=" & .dLedger_dat, eRemoteDB.ConstructSelect.eWordConnection.eAnd)
			End If
			If .dIssue_Dat <> dtmNull Then
				lclsConstruct.WhereClause("cheques.dIssue_dat", eRemoteDB.ConstructSelect.eTypeValue.TypCDate, "=" & .dIssue_Dat, eRemoteDB.ConstructSelect.eWordConnection.eAnd)
			End If
			If .nUser_sol <> eRemoteDB.Constants.intNull And .nUser_sol <> 0 Then
				lclsConstruct.WhereClause("cheques.nUser_sol", eRemoteDB.ConstructSelect.eTypeValue.TypCNumeric, "=" & .nUser_sol, eRemoteDB.ConstructSelect.eWordConnection.eAnd)
			End If
			If .sRequest_ty <> String.Empty And .sRequest_ty <> "0" Then
				lclsConstruct.WhereClause("cheques.sRequest_ty", eRemoteDB.ConstructSelect.eTypeValue.TypCString, "=" & .sRequest_ty, eRemoteDB.ConstructSelect.eWordConnection.eAnd)
			End If
			If .nBranch <> eRemoteDB.Constants.intNull And .nBranch <> 0 Then
				lclsConstruct.WhereClause("cheques.nBranch", eRemoteDB.ConstructSelect.eTypeValue.TypCNumeric, "=" & .nBranch, eRemoteDB.ConstructSelect.eWordConnection.eAnd)
			End If
			If .nBranch_Led <> eRemoteDB.Constants.intNull And .nBranch_Led <> 0 Then
				lclsConstruct.WhereClause("cheques.nBranch_Led", eRemoteDB.ConstructSelect.eTypeValue.TypCNumeric, "=" & .nBranch_Led, eRemoteDB.ConstructSelect.eWordConnection.eAnd)
			End If
			If .nProduct <> eRemoteDB.Constants.intNull And .nProduct <> 0 Then
				lclsConstruct.WhereClause("cheques.nProduct", eRemoteDB.ConstructSelect.eTypeValue.TypCNumeric, "=" & .nProduct, eRemoteDB.ConstructSelect.eWordConnection.eAnd)
			End If
			If .nPolicy <> eRemoteDB.Constants.intNull And .nPolicy <> 0 Then
				lclsConstruct.WhereClause("cheques.nPolicy", eRemoteDB.ConstructSelect.eTypeValue.TypCNumeric, "=" & .nPolicy, eRemoteDB.ConstructSelect.eWordConnection.eAnd)
			End If
			
			If lclsExeTime.Server = eFunctions.Tables.sTypeServer.sOracle Then
				lclsConstruct.SelectClause(("nRequest_nu,sCheque,nCompany," & "users.nOffice nUserOffice,nConcept," & "Cheques.sDescript,nCurrencyOri,nAmount," & "cheques.nOffice,cheques.nOfficeAgen,cheques.nAgency," & "nCurrencyPay,nAmountPay," & "nTypeSupport,nDocSupport,nTaxCode," & "nTax_Percent,nTax_amount,nAfect," & "nExent,((nAmountPay * (nTax_Percent/100)) + nAfect + nExent) nTotalPay, cheques.sClient," & "dDat_propos,dLedger_dat,nUser_sol," & "sRequest_ty,dIssue_dat,nAmount_Local,nConsec," & "nBranch,nBranch_Led,nProduct,nPolicy,sDigit," & "T9.sDescript sDesOffice,T5556.sDescript sDesOfficeAgen,T5555.sDescript sDesAgency, " & "cheques.sAccountHolder, cheques.nBankExt, cheques.nAcc_Type , cheques.sBankAccount"))
				
				lclsConstruct.NameFatherTable("cheques", "cheques")
				
				lclsConstruct.RelationsTables(eRemoteDB.ConstructSelect.eRelationTables.RelInner, "Users ", "Users ", "cheques.nUsercode = Users.nUserCode")
				lclsConstruct.RelationsTables(eRemoteDB.ConstructSelect.eRelationTables.RelInner, "Client", "Client", "cheques.sClient = Client.sClient")
				
				lclsConstruct.RelationsTables(eRemoteDB.ConstructSelect.eRelationTables.RelInner, "Table9", "T9", "Cheques.nOffice = T9.nOffice")
				lclsConstruct.RelationsTables(eRemoteDB.ConstructSelect.eRelationTables.RelInner, "Table5556", "T5556", "Cheques.nOfficeAgen = T5556.nOfficeAgen")
				lclsConstruct.RelationsTables(eRemoteDB.ConstructSelect.eRelationTables.RelInner, "Table5555", "T5555", "Cheques.nAgency = T5555.nAgency")
				
			Else
				lclsConstruct.SelectClause(("nRequest_nu,sCheque,nCompany," & "users.nOffice nUserOffice,nConcept," & "sDescript,nCurrencyOri,nAmount," & "cheques.nOffice,cheques.nOfficeAgen,cheques.nAgency," & "nCurrencyPay,nAmountPay," & "nTypeSupport,nDocSupport,nTaxCode," & "nTax_Percent,nTax_amount,nAfect," & "nExent,((nAmountPay * (nTax_Percent/100)) + nAfect + nExent) nTotalPay, cheques.sClient," & "sRequest_ty,dIssue_dat,nAmount_Local,nConsec," & "nBranch,nBranch_Led,nProduct,nPolicy "))
				
				
				lclsConstruct.NameFatherTable("cheques", "cheques")
				
				lclsConstruct.RelationsTables(eRemoteDB.ConstructSelect.eRelationTables.RelInner, "Users", "Users", "cheques.nUsercode = Users.nUserCode")
				lclsConstruct.RelationsTables(eRemoteDB.ConstructSelect.eRelationTables.RelInner, "Client", "Client", "cheques.sClient = Client.sClient")
			End If
			
			lclsExeTime.Sql = lclsConstruct.Answer
			
		End With
		
		
		Dim lrecreaExecute As eRemoteDB.Execute
		With lclsExeTime
			If .Run Then
				FindOP006 = True
				Do While Not (.EOF)
					
					'+ Se instancia la variable para hacer referencia al método "insCalDigit" y obtener el Dígito verificador
					lrecreaExecute = New eRemoteDB.Execute
					lrecreaExecute.StoredProcedure = "insCalDigit"
					lrecreaExecute.Parameters.Add("sClient", .FieldToClass("sClient"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					lrecreaExecute.Parameters.Add("sDigit", String.Empty, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					If lrecreaExecute.Run(False) Then
						lclsCheque.sClient_Digit = lrecreaExecute.Parameters.Item("sDigit").Value
					End If
					
					Call Add_OP006(.FieldToClass("nRequest_nu"), .FieldToClass("sCheque"), .FieldToClass("nCompany"), .FieldToClass("nConcept"), .FieldToClass("sDescript"), .FieldToClass("nCurrencyOri"), .FieldToClass("nAmount"), .FieldToClass("nUserOffice"), .FieldToClass("nCurrencyPay"), .FieldToClass("nAmountpay"), .FieldToClass("nTypesupport"), .FieldToClass("nDocSupport"), .FieldToClass("nTaxCode"), .FieldToClass("nTax_percent"), .FieldToClass("nTax_amount"), .FieldToClass("nAfect"), .FieldToClass("nExent"), .FieldToClass("sClient"), .FieldToClass("dDat_propos"), .FieldToClass("dLedger_dat"), .FieldToClass("nUser_sol"), .FieldToClass("sRequest_ty"), .FieldToClass("dIssue_dat"), .FieldToClass("nOffice"), .FieldToClass("nOfficeAgen"), .FieldToClass("nAgency"), .FieldToClass("nTotalPay"), .FieldToClass("nAmount_Local"), .FieldToClass("nConsec"), .FieldToClass("sDesOffice"), .FieldToClass("sDesOfficeAgen"), .FieldToClass("sDesAgency"), .FieldToClass("nBranch"), .FieldToClass("nBranch_Led"), .FieldToClass("nProduct"), .FieldToClass("nPolicy"), .FieldToClass("sDigit"))
					.RNext()
				Loop 
			End If
			.RCloseRec()
		End With
		
FindOP006_Err: 
		If Err.Number Then
			FindOP006 = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsConstruct may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsConstruct = Nothing
		'UPGRADE_NOTE: Object lclsExeTime may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsExeTime = Nothing
		'UPGRADE_NOTE: Object lrecreaExecute may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaExecute = Nothing
	End Function
	
	'*** Item: Restores an element to the collection (according to the index)
	'* Item: Devuelve un elemento de la colección (segun índice)
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Cheque
		Get
			
			Item = mCol.Item(vntIndexKey)
		End Get
	End Property
	
	'*** Count: Restores the number of elements that the collection owns
	'* Count: Devuelve el número de elementos que posee la colección
	Public ReadOnly Property Count() As Integer
		Get
			
			Count = mCol.Count()
		End Get
	End Property
	
	'***NewEnum: Allows to enumerate the ollection for using it in a cycle For Each...Next
	'* NewEnum: Permite enumerar la colección para utilizarla en un ciclo For Each... Next
	'UPGRADE_NOTE: NewEnum property was commented out. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="B3FC1610-34F3-43F5-86B7-16C984F0E88E"'
	'Public ReadOnly Property NewEnum() As stdole.IUnknown
		'Get
			'
			'NewEnum = mCol._NewEnum
		'End Get
	'End Property
	
	Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
		'UPGRADE_TODO: Uncomment and change the following line to return the collection enumerator. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="95F9AAD0-1319-4921-95F0-B9D3C4FF7F1C"'
		GetEnumerator = mCol.GetEnumerator
	End Function
	
	'**% Remove: Deletes an element from the collection
	'% Remove: Elimina un elemento de la colección
	'---------------------------------------------
	Public Sub Remove(ByRef vntIndexKey As Object)
		'---------------------------------------------
		
		mCol.Remove(vntIndexKey)
	End Sub
	
	'**% Class_Initialize: Controls the creation of an instance of the collection
	'% Class_Initialize: Controla la creación de una instancia de la colección
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		
		mCol = New Collection
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'**% Class_Terminate: controls the delete of an instance of the collection
	'% Class_Terminate: Controla la destrucción de una instancia de la colección
	'UPGRADE_NOTE: Class_Terminate was upgraded to Class_Terminate_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Terminate_Renamed()
		
		'UPGRADE_NOTE: Object mCol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mCol = Nothing
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
	
	'% Add_OP006: Añade una nueva instancia de la Cheque a la colección
	Public Function Add_OP006(ByVal nRequest_nu As Double, ByVal sCheque As String, ByVal nCompany As Integer, ByVal nConcept As Integer, ByVal sDescript As String, ByVal nCurrencyOri As Integer, ByVal nAmount As Double, ByVal nUserOffice As Integer, ByVal nCurrencyPay As Integer, ByVal nAmountPay As Double, ByVal nTypesupport As Integer, ByVal nDocSupport As Double, ByVal nTax_code As Integer, ByVal nTax_Percent As Double, ByVal nTax_Amount As Double, ByVal nAfect As Double, ByVal nExcent As Double, ByVal sClient As String, ByVal dDat_propos As Date, ByVal dLedger_dat As Date, ByVal nUser_sol As Integer, ByVal sRequest_ty As String, ByVal dIssue_Dat As Date, ByVal nOffice As Integer, ByVal nOfficeAgen As Integer, ByVal nAgency As Integer, ByVal nTotalPay As Double, ByVal nAmount_Local As Double, ByVal nConsec As Integer, ByVal sDesOffice As String, ByVal sDesOfficeAgen As String, ByVal sDesAgency As String, Optional ByVal nBranch As Integer = 0, Optional ByVal nBranch_Led As Integer = 0, Optional ByVal nProduct As Integer = 0, Optional ByVal nPolicy As Double = 0, Optional ByVal sClient_Digit As String = "", Optional ByVal nAcc_Type As Integer = 0) As Cheque
		'**- Variable definition that will have the instance to be added
		'- Se define la variable que contendra la instancia a añadir
		
		Dim objNewMember As Cheque
		objNewMember = New Cheque
		
		With objNewMember
			.nRequest_nu = nRequest_nu
			.sCheque = sCheque
			.nCompany = nCompany
			.nConcept = nConcept
			.sDescript = sDescript
			.nCurrencyOri = nCurrencyOri
			.nAmount = nAmount
			.nCurrencyPay = nCurrencyPay
			.nAmountPay = nAmountPay
			.nTypesupport = nTypesupport
			.nDocSupport = nDocSupport
			.nTax_code = nTax_code
			.nTax_Percent = nTax_Percent
			.nTax_Amount = nTax_Amount
			.nAfect = nAfect
			.nExcent = nExcent
			.sClient = sClient
			.dDat_propos = dDat_propos
			.dLedger_dat = dLedger_dat
			.nUser_sol = nUser_sol
			.sRequest_ty = sRequest_ty
			.dIssue_Dat = dIssue_Dat
			.nTotalPay = nTotalPay
			.nOffice = nOffice
			.nOfficeAgen = nOfficeAgen
			.nAgency = nAgency
			.nAmount_Local = nAmount_Local
			.nConsec = nConsec
			.nBranch = nBranch
			.nBranch_Led = nBranch_Led
			.nProduct = nProduct
			.nPolicy = nPolicy
			.sClient_Digit = sClient_Digit
			.sDesOffice = sDesOffice
			.sDesOfficeAgen = sDesOfficeAgen
			.sDesAgency = sDesAgency
            .nAcc_type = nAcc_Type
		End With
		
		mCol.Add(objNewMember)
		
		'**+ Returns the created object
		'+ Retorna el objeto creado
		
		Add_OP006 = objNewMember
		'UPGRADE_NOTE: Object objNewMember may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		objNewMember = Nothing
	End Function
	
	'% Add_OP008: Añade una nueva instancia de la Cheque a la colección
	Public Function Add_OP008(ByVal nRequest_nu As Double, ByVal sCheque As String, ByVal nConsec As Integer, ByVal nAmount As Double, ByVal nConcept As Integer, ByVal sClient As String, ByVal dDat_propos As Date, ByVal sDescript As String, ByVal dIssue_Dat As Date, ByVal dLedger_dat As Date, ByVal nAcc_bank As Integer, ByVal sInter_pay As String, ByVal nUser_sol As Integer, ByVal nBranch_Led As Integer, ByVal nClaim As Double, ByVal nVoucher_le As Integer, ByVal nVoucher As Integer, ByVal nNullcode As Integer, ByVal sRequest_ty As String, ByVal dNulldate As Date, ByVal sPay_freq As String, ByVal nQ_pays As Integer, ByVal nReceipt As Double, ByVal nSta_cheque As Integer, ByVal dStat_date As Date, ByVal nTransac As Integer, ByVal nYear_month As Integer, ByVal nBordereaux As Double, ByVal sBenef_name As String, ByVal sInter_name As String, ByVal sUser_name As String, ByVal sBank_name As String, ByVal nBank_curr As Integer, ByVal nNoteNum As Integer, ByVal sAcc_number As String, ByVal nCompany As Integer, ByVal nBank_code As Integer, ByVal sClientInter As String, ByVal sClientUser As String) As Cheque
		'**- Variable definition that will have the instance to be added
		'- Se define la variable que contendra la instancia a añadir
		
		Dim objNewMember As Cheque
		objNewMember = New Cheque
		
		With objNewMember
			.nRequest_nu = nRequest_nu
			.sCheque = sCheque
			.nConsec = nConsec
			.nAmount = nAmount
			.nConcept = nConcept
			.sClient = sClient
			.dDat_propos = dDat_propos
			.sDescript = sDescript
			.dIssue_Dat = dIssue_Dat
			.dLedger_dat = dLedger_dat
			.nAcc_bank = nAcc_bank
			.sInter_pay = sInter_pay
			.nUser_sol = nUser_sol
			.nBranch_Led = nBranch_Led
			.nClaim = nClaim
			.nVoucher_le = nVoucher_le
			.nVoucher = nVoucher
			.nNullcode = nNullcode
			.sRequest_ty = sRequest_ty
			.dNulldate = dNulldate
			.sPay_freq = sPay_freq
			.nQ_pays = nQ_pays
			.nReceipt = nReceipt
			.nSta_cheque = nSta_cheque
			.dStat_date = dStat_date
			.nTransac = nTransac
			.nYear_month = nYear_month
			.nBordereaux = nBordereaux
			.sBenef_name = sBenef_name
			.sInter_name = sInter_name
			.sUser_name = sUser_name
			.sBank_name = sBank_name
			.nBank_curr = nBank_curr
			.nNoteNum = nNoteNum
			.sAcc_number = sAcc_number
			.nCompany = nCompany
			.nBank_code = nBank_code
			.sClientInter = sClientInter
			.sClientUser = sClientUser
		End With
		
		mCol.Add(objNewMember)
		
		'**+ Returns the created object
		'+ Retorna el objeto creado
		
		Add_OP008 = objNewMember
		'UPGRADE_NOTE: Object objNewMember may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		objNewMember = Nothing
	End Function
	'% Add_OP716: Añade una nueva instancia de la Cheque a la colección
    Public Function Add_OP716(ByVal nRequest_nu As Double, ByVal nConcept As Integer, ByVal nAmount As Double,
                              ByVal sClient As String, ByVal sDigit As String, ByVal sCliename As String,
                              ByVal sCheque As String, ByVal nConsec As Integer, ByVal dDat_propos As Date,
                              ByVal sDescript As String, ByVal sRequest_ty As String, ByVal dStat_date As Date,
                              ByVal nUser_sol As Integer, ByVal nAcc_bank As Integer, ByVal nAmount_Local As Double,
                              ByVal sAccountHolder As String, ByVal nBankExt As Integer, ByVal sBankAccount As String,
                              ByVal nAcc_type As Integer, ByVal nExternal_Concept As Integer, ByVal nOffice As Integer,
                              ByVal nCurrencyPay As Integer, ByVal nAmountPay As Double, ByVal nId_ExternalSystem As Long,
                              ByVal nBranch As Integer, ByVal nProduct As Integer) As Cheque
        '**- Variable definition that will have the instance to be added
        '- Se define la variable que contendra la instancia a añadir

        Dim objNewMember As Cheque
        objNewMember = New Cheque

        With objNewMember
            .nRequest_nu = nRequest_nu
            .nConcept = nConcept
            .nAmount = nAmount
            .sClient = sClient
            .sDigit = sDigit
            .sCliename = sCliename
            .sCheque = sCheque
            .nConsec = nConsec
            .dDat_propos = dDat_propos
            .sDescript = sDescript
            .sRequest_ty = sRequest_ty
            .dStat_date = dStat_date
            .nUser_sol = nUser_sol
            .nAcc_bank = nAcc_bank
            .nAmount_Local = nAmount_Local
            .sAccountHolder = sAccountHolder
            .nBankExt = nBankExt
            .sBankAccount = sBankAccount
            .nAcc_type = nAcc_type
            .nExternal_Concept = nExternal_Concept
            .nOffice = nOffice
            .nCurrencyPay = nCurrencyPay
            .nAmountPay = nAmountPay
            .nId_ExternalSystem = nId_ExternalSystem
            .nBranch = nBranch
            .nProduct = nProduct 
        End With

        mCol.Add(objNewMember)

        '**+ Returns the created object
        '+ Retorna el objeto creado

        Add_OP716 = objNewMember
        'UPGRADE_NOTE: Object objNewMember may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        objNewMember = Nothing
    End Function

	
	'**%InsPreOP714: Restores an object collection of type Cheque
	'%InsPreOP714: Devuelve una coleccion de objetos de tipo Cheque
	'------------------------------------------------------------
	Public Function InsPreOP714(ByVal nCompany As Integer, ByVal nConcept As Integer, ByVal dStartDate As Date, ByVal dEndDate As Date) As Boolean
		'------------------------------------------------------------
		Dim lrecreaChequesOP714 As eRemoteDB.Execute
		
		On Error GoTo InsPreOP714_Err
		
		lrecreaChequesOP714 = New eRemoteDB.Execute
		
		With lrecreaChequesOP714
			
			.StoredProcedure = "reaChequesOP714"
			'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
			.Parameters.Add("nCompany", IIf(nCompany = eRemoteDB.Constants.intNull, System.DBNull.Value, nCompany), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
			.Parameters.Add("nConcept", IIf(nConcept = eRemoteDB.Constants.intNull, System.DBNull.Value, nConcept), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dStartDate", dStartDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
			.Parameters.Add("dEndDate", IIf(dEndDate = dtmNull, System.DBNull.Value, dEndDate), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Do While Not .EOF
					Call Add_OP006(.FieldToClass("nRequest_nu"), .FieldToClass("sCheque"), .FieldToClass("nCompany"), .FieldToClass("nConcept"), strNull, .FieldToClass("nCurrencyOri"), .FieldToClass("nAmount"), .FieldToClass("nOffice"), eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, .FieldToClass("sClient"), .FieldToClass("dDat_Propos"), dtmNull, eRemoteDB.Constants.intNull, strNull, dtmNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, .FieldToClass("nAmount_Local"), .FieldToClass("nConsec"), .FieldToClass("nOffice"), .FieldToClass("nOfficeAgen"), .FieldToClass("nAgency"))
					.RNext()
				Loop 
				.RCloseRec()
				InsPreOP714 = True
			Else
				InsPreOP714 = False
			End If
		End With
		
		'UPGRADE_NOTE: Object lrecreaChequesOP714 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaChequesOP714 = Nothing
		
InsPreOP714_Err: 
		If Err.Number Then
			InsPreOP714 = False
		End If
		On Error GoTo 0
	End Function
	
	'**%InsPreOP715: Restores an object collection of type Cheque
	'%InsPreOP715: Devuelve una coleccion de objetos de tipo Cheque
	'------------------------------------------------------------
	Public Function InsPreOP715(ByVal nAction As Integer, ByVal nPayOrdBord As Integer, ByVal nCompany As Integer, ByVal nConcept As Integer, ByVal dStartDate As Date, ByVal dEndDate As Date) As Boolean
		'------------------------------------------------------------
		Dim lrecreaChequesOP715 As eRemoteDB.Execute
		
		On Error GoTo InsPreOP715_Err
		
		lrecreaChequesOP715 = New eRemoteDB.Execute
		
		With lrecreaChequesOP715
			.StoredProcedure = "reaChequesOP715"
			.Parameters.Add("nPayOrdBord", nPayOrdBord, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
			.Parameters.Add("nCompany", IIf(nCompany = eRemoteDB.Constants.intNull, System.DBNull.Value, nCompany), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
			.Parameters.Add("nConcept", IIf(nConcept = eRemoteDB.Constants.intNull, System.DBNull.Value, nConcept), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
			.Parameters.Add("dStartDate", IIf(dStartDate = System.Date.FromOADate(eRemoteDB.Constants.intNull), System.DBNull.Value, dStartDate), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
			.Parameters.Add("dEndDate", IIf(dEndDate = dtmNull, System.DBNull.Value, dEndDate), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAction", IIf(nAction = 302, 1, 2), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Do While Not .EOF
					Call Add_OP006(.FieldToClass("nRequest_nu"), .FieldToClass("sCheque"), .FieldToClass("nCompany"), .FieldToClass("nConcept"), strNull, .FieldToClass("nCurrencyOri"), .FieldToClass("nAmount"), .FieldToClass("nOffice"), eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, .FieldToClass("sClient"), .FieldToClass("dDat_Propos"), dtmNull, eRemoteDB.Constants.intNull, strNull, dtmNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, .FieldToClass("nAmount_Local"), .FieldToClass("nConsec"), strNull, strNull, strNull)
					.RNext()
				Loop 
				.RCloseRec()
				InsPreOP715 = True
			Else
				InsPreOP715 = False
			End If
		End With
		
		'UPGRADE_NOTE: Object lrecreaChequesOP715 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaChequesOP715 = Nothing
		
InsPreOP715_Err: 
		If Err.Number Then
			InsPreOP715 = False
		End If
		On Error GoTo 0
	End Function
	'**%InsPreOP716: Restores an object collection of type Cheque
	'%InsPreOP716: Devuelve una coleccion de objetos de tipo Cheque
	'------------------------------------------------------------
    Public Function InsPreOP716(ByVal dStartDate As Date, ByVal dEndDate As Date, ByVal nUsercode As Long) As Boolean
        '------------------------------------------------------------
        Dim lrecreaChequesOP716 As eRemoteDB.Execute

        On Error GoTo InsPreOP716_Err

        lrecreaChequesOP716 = New eRemoteDB.Execute

        With lrecreaChequesOP716

            .StoredProcedure = "reaChequesOP716"
            .Parameters.Add("dStartDate", dStartDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEndDate", IIf(dEndDate = dtmNull, System.DBNull.Value, dEndDate), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run Then
                Do While Not .EOF
                    Call Add_OP716(.FieldToClass("nRequest_nu"), .FieldToClass("nConcept"), .FieldToClass("nAmount"),
                                   .FieldToClass("sClient"), .FieldToClass("sDigit"), .FieldToClass("sCliename"),
                                   .FieldToClass("sCheque"), .FieldToClass("nConsec"), .FieldToClass("dDat_propos"),
                                   .FieldToClass("sDescript"), .FieldToClass("sRequest_ty"), .FieldToClass("dStat_date"),
                                   .FieldToClass("nUser_sol"), .FieldToClass("nAcc_bank"), .FieldToClass("nAmount_Local"),
                                   .FieldToClass("sAccountHolder"), .FieldToClass("nBankExt"), .FieldToClass("sBankAccount"),
                                   .FieldToClass("nAcc_type"), .FieldToClass("nExternal_Concept"), .FieldToClass("nOffice"),
                                   .FieldToClass("nCurrencyPay"), .FieldToClass("nAmountPay"), .FieldToClass("nId_ExternalSystem"), 
                                   .FieldToClass("nBranch"), .FieldToClass("nProduct"))
                    .RNext()
                Loop
                .RCloseRec()
                InsPreOP716 = True
            Else
                InsPreOP716 = False
            End If
        End With

        lrecreaChequesOP716 = Nothing

InsPreOP716_Err:
        If Err.Number Then
            InsPreOP716 = False
        End If
        On Error GoTo 0
    End Function
	
	'% Find_Cheques: Lee los datos de las tablas de cheques
	Public Function Find_Cheques(ByVal nAcc_bank As Double, ByVal sCheque As String, ByVal nRequest_nu As Double, ByVal nSta_cheque As Integer, ByVal nAmount As Double, ByVal dDat_propos As Date, ByVal dIssue_Dat As Date, ByVal nConcept As Integer, ByVal sClient As String, ByVal optInfType As Integer) As Boolean
		Dim lclsCheque As eCashBank.Cheque
		Dim lrecCheque As eRemoteDB.Execute
		
		On Error GoTo Find_Cheques_Err
		
		lclsCheque = New eCashBank.Cheque
		lrecCheque = New eRemoteDB.Execute
		
		Find_Cheques = True
		
		With lrecCheque
			.StoredProcedure = "REACHEQUESQUERYPKG.REACHEQUESQUERY"
			
			.Parameters.Add("nAcc_bank", nAcc_bank, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCheque", sCheque, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRequest_nu", nRequest_nu, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nSta_cheque", nSta_cheque, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAmount", nAmount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dDat_propos", dDat_propos, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dIssue_Dat", dIssue_Dat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nConcept", nConcept, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("optInfType", optInfType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				Find_Cheques = True
				Do While Not .EOF
					lclsCheque = New Cheque
					
					lclsCheque.sCheque = .FieldToClass("sCheque")
					lclsCheque.nRequest_nu = .FieldToClass("nRequest_nu")
					lclsCheque.nConsec = .FieldToClass("nConsec")
					lclsCheque.nSta_cheque = .FieldToClass("nSta_cheque")
					lclsCheque.nAmount = .FieldToClass("nAmount")
					lclsCheque.dDat_propos = .FieldToClass("dDat_propos")
					lclsCheque.dIssue_Dat = .FieldToClass("dIssue_dat")
					lclsCheque.nConcept = .FieldToClass("nConcept")
					lclsCheque.sClient = .FieldToClass("sClient")
					lclsCheque.nAvailable = .FieldToClass("nAvailable")
					lclsCheque.nTransit_1 = .FieldToClass("nTransit_1")
					lclsCheque.nTransit_2 = .FieldToClass("nTransit_2")
					lclsCheque.nTransit_3 = .FieldToClass("nTransit_3")
					lclsCheque.nTransit_4 = .FieldToClass("nTransit_4")
					lclsCheque.nTransit_5 = .FieldToClass("nTransit_5")
					lclsCheque.sCurrency = .FieldToClass("sCurrency")
					
					Call Add_Opc002(lclsCheque)
					.RNext()
					'UPGRADE_NOTE: Object lclsCheque may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					lclsCheque = Nothing
				Loop 
				.RCloseRec()
			Else
				Find_Cheques = False
			End If
		End With
		
Find_Cheques_Err: 
		If Err.Number Then
			Find_Cheques = False
		End If
		
		'UPGRADE_NOTE: Object lclsCheque may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsCheque = Nothing
		'UPGRADE_NOTE: Object lrecCheque may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecCheque = Nothing
		
		On Error GoTo 0
	End Function
	
	'% Add_Opc002: Agrega un nuevo registro a la colección
	Public Function Add_Opc002(ByRef objClass As Cheque) As Cheque
		If objClass Is Nothing Then
			objClass = New Cheque
		End If
		
		With objClass
			mCol.Add(objClass, "CH" & .sCheque & .nRequest_nu & .nConsec)
		End With
		
		'Return the object created
		Add_Opc002 = objClass
	End Function
End Class






