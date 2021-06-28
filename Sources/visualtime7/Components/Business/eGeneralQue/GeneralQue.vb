Option Strict Off
Option Explicit On
Public Class GeneralQue
	'%-------------------------------------------------------%'
	'% $Workfile:: GeneralQue.cls                           $%'
	'% $Author:: Pgarin                                     $%'
	'% $Date:: 5/04/06 16:12                                $%'
	'% $Revision:: 6                                        $%'
	'%-------------------------------------------------------%'
	
	Private Enum eArea
		arPolicy = 0
		arClient = 1
		arClaim = 2
		arCheque = 3
	End Enum
	
	'**% insvalHeaderGE099: MISSING
    Public Function insvalHeaderGE099(ByVal sQueryType As GenFunct.eQueryType, ByVal dEffecdate As Date, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Integer, ByVal sOriginalP As Object, ByVal sClient As String, ByVal nClaim As Double, ByVal nContrat As Integer, ByVal sCheque As String, ByVal nProvider As Integer, ByVal nIntermed As Integer, ByVal nCompany As Integer, ByVal nUserCode As Integer) As String
        'Static lstrValField As String
        Dim lobjError As eFunctions.Errors
        Dim lobjPolicy As Object
        Dim lobjCertificat As Object
        Dim lobjProduct As Object
        'Dim llngRow As Integer
        'Dim llngIndex As Integer
        Dim lblnError As Integer
        Dim lClaTime As Object
        Dim lfinTime As Object
        Dim lobjValues As eFunctions.valField
        Dim lobjClient As Object
        Dim lobjPremium As Object
        Dim lobjCheque As Object
        Dim lobjProvider As Object
        Dim sMessajeRet As String

        insvalHeaderGE099 = CStr(True)

        If nBranch = eRemoteDB.Constants.intNull Then nBranch = 0
        If nProduct = eRemoteDB.Constants.intNull Then nProduct = 0
        If nPolicy = eRemoteDB.Constants.intNull Then nPolicy = 0
        If nCertif = eRemoteDB.Constants.intNull Then nCertif = 0
        If nClaim = eRemoteDB.Constants.intNull Then nClaim = 0
        If nContrat = eRemoteDB.Constants.intNull Then nContrat = 0
        If nProvider = eRemoteDB.Constants.intNull Then nProvider = 0
        'UPGRADE_WARNING: IsEmpty was upgraded to IsNothing and has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
        If IsNothing(dEffecdate) Then dEffecdate = Today
        If nCompany = eRemoteDB.Constants.intNull Then nCompany = 0

        lobjError = New eFunctions.Errors
        lobjPolicy = eRemoteDB.NetHelper.CreateClassInstance("ePolicy.Policy")

        lobjCertificat = eRemoteDB.NetHelper.CreateClassInstance("ePolicy.Certificat")

        Select Case sQueryType
            Case GenFunct.eQueryType.qtPolicy, GenFunct.eQueryType.qtQuotation, GenFunct.eQueryType.qtProposal, GenFunct.eQueryType.qtcertificat
                Select Case sQueryType
                    Case GenFunct.eQueryType.qtPolicy, GenFunct.eQueryType.qtcertificat
                        sCertype = "2"
                    Case GenFunct.eQueryType.qtQuotation
                        sCertype = "3"
                    Case GenFunct.eQueryType.qtProposal
                        sCertype = "1"
                End Select

                '**+ Validation of the policy branch
                '+ Validación del ramo de la póliza
                If nBranch = 0 Then
                    lobjError.ErrorMessage("GE099", 1022)
                    lblnError = True
                End If
                '**+ Validate the product field.
                '+ Se valida el campo producto
                If Not lblnError Then
                    If nProduct = 0 Then
                        lobjError.ErrorMessage("GE099", 1014)
                        lblnError = True
                    Else
                        lobjProduct = eRemoteDB.NetHelper.CreateClassInstance("eProduct.Product")
                        If Not lobjProduct.Find(nBranch, nProduct, dEffecdate) Then
                            lobjError.ErrorMessage("GE099", 1011)
                        End If
                        'UPGRADE_NOTE: Object lobjProduct may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
                        lobjProduct = Nothing
                    End If
                End If
                '**+ Make the concerning validations to the policy number.
                '+Se efectua las validaciones concernientes al número de la póliza
                If Not lblnError Then
                    If nPolicy = 0 Then
                        lobjError.ErrorMessage("GE099", IIf(sCertype = "2", 3003, IIf(sCertype = "1", 7044, 3976)))
                        lblnError = True
                    Else
                        If Not lobjPolicy.Find(sCertype, nBranch, nProduct, nPolicy) Then
                            lobjError.ErrorMessage("GE099", IIf(sCertype = "2", 3001, IIf(sCertype = "1", 3667, 4000)))
                            lblnError = True
                        End If
                    End If
                End If

                If sQueryType = GenFunct.eQueryType.qtcertificat Then
                    If Not lblnError Then
                        If nCertif = 0 Then
                            lobjError.ErrorMessage("GE099", 3200)
                            lblnError = True
                        Else
                            If Not lobjCertificat.Find(sCertype, nBranch, nProduct, nPolicy, nCertif) Then
                                lobjError.ErrorMessage("GE099", 3713)
                                lblnError = True
                            End If
                        End If
                    End If
                End If

                '**+ Validate the date.
                '+ Se valida la fecha.
                If dEffecdate <> eRemoteDB.Constants.dtmNull Then
                    lobjValues = New eFunctions.valField
                    With lobjValues
                        .objErr = lobjError
                        .ValDate(dEffecdate)
                    End With
                    'UPGRADE_NOTE: Object lobjValues may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
                    lobjValues = Nothing
                End If
                If Not lblnError Then
                    If Not insValpolicyintermedia(sCertype, nBranch, nProduct, nPolicy, nUserCode) Then
                        lobjError.ErrorMessage("GE099", 1102, , eFunctions.Errors.TextAlign.RigthAling, "Esta cotización,propuesta,poliza pertenece a otro intermediario")
                    End If
                End If



            Case GenFunct.eQueryType.qtClient

                '**+ Make the validations of the client field.
                '+Se efectua la validación del campo cliente
                If sClient = String.Empty Then
                    lobjError.ErrorMessage("GE099", 12043)
                Else
                    '**+ Validate that the client number and the structure is entered
                    '+Se valida que se ingrese el número de cliente y la estructura
                    lobjClient = eRemoteDB.NetHelper.CreateClassInstance("eClient.Client")
                    sClient = UCase(sClient)
                    If lobjClient.ValidateClientStruc(sClient) Then
                        sClient = lobjClient.ExpandCode(sClient)
                        If Not lobjClient.Find(sClient, True) Then
                            lobjError.ErrorMessage("GE099", 2044)
                        End If
                    Else
                        lobjError.ErrorMessage("GE099", 2012)
                    End If
                    ' Valida si el cliente pertenece al Usuario Intermediario o si es cliente sin polizas
                    sMessajeRet = ""
                    If lobjClient.Validate_Client(sClient, 401, nUserCode, dEffecdate, sMessajeRet) Then
                        If sMessajeRet <> "OK" Then
                            lobjError.ErrorMessage("GE099", 60459, , eFunctions.Errors.TextAlign.LeftAling, sMessajeRet)
                        End If
                    End If
                    'UPGRADE_NOTE: Object lobjClient may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
                    lobjClient = Nothing
                End If

                If dEffecdate <> eRemoteDB.Constants.dtmNull Then
                    lobjValues = New eFunctions.valField
                    With lobjValues
                        .objErr = lobjError
                        .ErrInvalid = 1001
                        .ValDate(dEffecdate)
                    End With
                    'UPGRADE_NOTE: Object lobjValues may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
                    lobjValues = Nothing
                End If



            Case GenFunct.eQueryType.qtcertificat
                If nBranch = 0 Then
                    lobjError.ErrorMessage("GE009", 1022)
                    lblnError = True
                End If
            Case GenFunct.eQueryType.qtClaim
                lClaTime = eRemoteDB.NetHelper.CreateClassInstance("eClaim.Claim")
                If nClaim = 0 Then
                    lobjError.ErrorMessage("GE099", 4006)
                Else
                    If Not lClaTime.Find(nClaim) Then
                        lobjError.ErrorMessage("GE099", 4005)
                        lblnError = True
                    End If
                End If
                'UPGRADE_NOTE: Object lClaTime may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
                lClaTime = Nothing
            Case GenFunct.eQueryType.qtContract
                If nContrat = 0 Then
                    lobjError.ErrorMessage("GE099", 3357)
                    lblnError = True
                Else
                    lfinTime = eRemoteDB.NetHelper.CreateClassInstance("eFinance.financeCo")
                    If Not lfinTime.Find_Contrat(nContrat) Then
                        lobjError.ErrorMessage("GE009", 21002)
                        lblnError = True
                    End If
                End If
            Case GenFunct.eQueryType.qtReceipt
                If nClaim = 0 Then
                    lobjError.ErrorMessage("GE099", 5053)
                    lblnError = True
                Else
                    lobjPremium = eRemoteDB.NetHelper.CreateClassInstance("eCollection.Premium")
                    If Not lobjPremium.Find("2", nClaim, 0, 0, 0, 0) Then
                        lobjError.ErrorMessage("GE099", 5004)
                        lblnError = True
                    End If
                    'UPGRADE_NOTE: Object lobjPremium may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
                    lobjPremium = Nothing
                End If
            Case GenFunct.eQueryType.qtcheque
                If sCheque = String.Empty Then
                    lobjError.ErrorMessage("GE099", 7040)
                    lblnError = True
                Else
                    lobjCheque = eRemoteDB.NetHelper.CreateClassInstance("ecashbank.cheque")
                    If Not lobjCheque.valChequeExists(sCheque) Then
                        lobjError.ErrorMessage("GE099", 7065)
                        lblnError = True
                    End If
                    'UPGRADE_NOTE: Object lobjCheque may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
                    lobjCheque = Nothing
                End If
            Case GenFunct.eQueryType.qtLoanLease
                If sCheque = String.Empty Then
                    lobjError.ErrorMessage("GE099", 80000)
                Else
                    If Not insValLoanExists(sCheque, dEffecdate) Then
                        lobjError.ErrorMessage("GE099", 80001)
                    End If
                End If

                '        Case qtOriginalPolicy
                '            If sCheque = String.Empty Then
                '                lobjError.ObjErrors.ErrorMessage "GE099", 21033
                '                lblnError = True
                '            Else
                '                Set lobjPolicy = eRemoteDB.NetHelper.CreateClassInstance("epolicy.policy")
                '                If Not lobjPolicy.Find_OriginalPolicy(1, sCheque) Then
                '                    lobjError.ErrorMessage "GE099", 3001
                '                    lblnError = True
                '                End If
                '                Set lobjPolicy = Nothing
                '            End If
                '        Case qtOriginalReceipt
                '            If sCheque = String.Empty Then
                '                lobjError.ErrorMessage "GE099", 5560
                '                lblnError = True
                '            Else
                '                If Not insValPremiumOrigExists(tctCheque.Value) Then
                '                    If pmnuTime.ObjErrors.ErrorMessage(gstrCodispl, 5004) Then
                '                        insValHeader = False
                '                        lblnError = True
                '                    End If
                '                End If
                '            End If
            Case GenFunct.eQueryType.qtprovider
                If nProvider = 0 Then
                    lobjError.ErrorMessage("GE099", 10908)
                    lblnError = True
                Else
                    lobjProvider = eRemoteDB.NetHelper.CreateClassInstance("eClaim.tab_Provider")
                    If Not lobjProvider.FindProvider(nProvider) Then
                        lobjError.ErrorMessage("GE099", 38031)
                        lblnError = True
                    End If
                    'UPGRADE_NOTE: Object lobjProvider may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
                    lobjProvider = Nothing
                End If
            Case GenFunct.eQueryType.qtIntermed
                If nIntermed = 0 Then
                    lobjError.ErrorMessage("GE099", 750200)
                End If
            Case GenFunct.eQueryType.qtnCompany
                If nCompany = 0 Then
                    lobjError.ErrorMessage("GE099", 750201)
                End If
            Case GenFunct.eQueryType.qtnPrimaCedida
                If nPolicy = 0 Then
                    lobjError.ErrorMessage("GE099", 750201)
                End If
            Case GenFunct.eQueryType.qtnSiniestroCedido
                If nPolicy = 0 Then
                    lobjError.ErrorMessage("GE099", 750201)
                End If
            Case GenFunct.eQueryType.qtnDistribCapital
                If nPolicy = 0 Then
                    lobjError.ErrorMessage("GE099", 750201)
                End If
            Case GenFunct.eQueryType.qtnDistReaseg_Poliza
                If nPolicy = 0 Then
                    lobjError.ErrorMessage("GE099", 750201)
                End If
            Case GenFunct.eQueryType.qtnDistReaseg_Siniestro
                If nClaim = 0 Then
                    lobjError.ErrorMessage("GE099", 4006)
                End If
                '            If dEffecdate = dtmNull Then
                '                lobjError.ErrorMessage "GE099", 1094
                '            End If
            Case Else
                lobjError.ErrorMessage("GE099", 750110)
        End Select
        'UPGRADE_NOTE: Object lobjCertificat may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lobjCertificat = Nothing
        'UPGRADE_NOTE: Object lobjPolicy may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lobjPolicy = Nothing
        insvalHeaderGE099 = lobjError.Confirm
    End Function
	
	'**% Function insValLoanExist. This function verifies the existenece of a Loan in the data base.
	'%Funcion insValLoanExist. Esta funcion se encarga de verificar la existencia de un Prestamo
	'%en la base de datos.
	Private Function insValLoanExists(ByRef lstrLoan As String, ByRef dEffecdate As Date) As Boolean
		Dim lrecqueDatLoan As eRemoteDB.Execute
		
		lrecqueDatLoan = New eRemoteDB.Execute
		
		'**+ Parameter definition for stored procedure 'insudb.queDatPolicyOrig'
		'+ Definición de parámetros para stored procedure 'insudb.queDatPolicyOrig'
		'**+ Information read on June 08,2001
		'+ Información leída el 08/06/2001
		
		With lrecqueDatLoan
			.StoredProcedure = "queDatLoan"
			.Parameters.Add("sLoan", lstrLoan, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 25, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			insValLoanExists = .Run
			
			If insValLoanExists Then
				.RCloseRec()
			End If
		End With
		'UPGRADE_NOTE: Object lrecqueDatLoan may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecqueDatLoan = Nothing
	End Function
	
	'%Funcion insValPolicy . Esta funcion se encarga de verificar si una poliza esta relacionado con un
	'intermediaro en ecaso que correponda verdadero en caso que este relacionado
	Public Function insValpolicyintermedia(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nUserCode As Integer) As Boolean
		Dim lrecquedatpolint As eRemoteDB.Execute
		lrecquedatpolint = New eRemoteDB.Execute
		
		'**+ Parameter definition for stored procedure 'insudb.queDatPolicyOrig'
		'+ Definición de parámetros para stored procedure 'insudb.queDatPolicyOrig'
		'**+ Information read on June 08,2001
		'+ Información leída el 08/06/2001
		
		With lrecquedatpolint
			.StoredProcedure = "QUEDATPOLINT"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUserCode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nExist", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				insValpolicyintermedia = (.Parameters("nExist").Value = 1)
			End If
		End With
		'UPGRADE_NOTE: Object lrecquedatpolint may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecquedatpolint = Nothing
	End Function
End Class






