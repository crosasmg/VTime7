Option Strict Off
Option Explicit On
Public Class TConvertions
	'%-------------------------------------------------------%'
	'% $Workfile:: TConvertions.cls                         $%'
	'% $Author:: Mpalleres                                  $%'
	'% $Date:: 14-08-09 11:23                               $%'
	'% $Revision:: 7                                        $%'
	'%-------------------------------------------------------%'
	
	'+
	'+ Estructura de tabla tconvertions al 12-03-2001 13:01:43
	'+     Property                Type         DBType   Size Scale  Prec  Null
	'+-------------------------------------------------------------------------
	Public nProponum As Double ' NUMBER     22   0     10   S
	Public nPolicy As Double ' NUMBER     22   0     10   S
	Public nNum_doc As Integer ' NUMBER     22   0     10   S
	Public sPen_doc As String ' CHAR       1    0     0    S
	Public dDate_init As Date ' DATE       7    0     0    S
	Public nStatus As Integer ' NUMBER     22   0     5    S
	Public dStat_date As Date ' DATE       7    0     0    S
	Public nNullcode As Integer ' NUMBER     22   0     5    S
	Public nRole As Integer ' NUMBER     22   0     5    S
	Public dEffecdate As Date ' DATE       7    0     0    S
	Public dExpirdat As Date ' DATE       7    0     0    S
	Public dLimit_date As Date ' DATE       7    0     0    S
	Public sObserv As String ' CHAR       1    0     0    S
	Public nServ_order As Double ' NUMBER     22   0     5    S
	Public nStatus_ord As Integer ' NUMBER     22   0     5    S
	Public nBordereaux As Integer ' NUMBER     22   0     10   S
	Public nFirst_prem As Double ' NUMBER     22   2     10   S
	Public nPrem_curr As Integer ' NUMBER     22   0     5    S
	Public sPrem_che As String ' CHAR       1    0     0    S
	Public sPay_order As String ' CHAR       1    0     0    S
	Public nExpenses As Double ' NUMBER     22   2     10   S
	Public sDevolut As String ' CHAR       1    0     0    S
	Public sCertype As String ' CHAR       1    0     0    S
	Public nBranch As Integer ' NUMBER     22   0     5    S
	Public nOffice As Integer ' NUMBER     22   0     5    S
	Public nOfficeAgen As Integer ' NUMBER     22   0     5    S
	Public nAgency As Integer ' NUMBER     22   0     5    S
	Public nProduct As Integer ' NUMBER     22   0     5    S
	Public nOrigin As Integer ' NUMBER     22   0     5    S
	Public sClient As String ' CHAR       14   0     0    S
	Public nCertif As Double ' NUMBER     22   0     10   S
	Public nExchange As Double ' NUMBER     22   6     11   S
	Public nOrig_prem As Double ' NUMBER     22   6     18   S
	Public nOrig_curr As Integer ' NUMBER     22   0     5    S
	Public nRoutine As Double ' NUMBER     22   2     10   S
	Public nHealthexp As Double ' NUMBER     22   2     10   S
	
	Public ncount As Integer
	'- Se declara para los procesos de tratamiento de cotizacion/propuesta
	Public nUsercode As Integer
	Public sCliename As String
	Public nType_amend As Integer
	Public sType_amend As String
	Public sStatus As String
	Public sStatus_ord As String
	Public nPol_quot As Double
	Public dStartdate As Date
	Public sPolitype As String
	Public nNo_convers As Integer
	Public sCon_descript As String
	Public nWait_code As Integer
	Public sWai_descript As String
	Public nRequest_nu As Double
	Public nIndex As Integer
	Public sOrig_curr As String
	
	'- Se declara para los procesos de conversion
	Public nNewPolicy As Double
	Public sText As String
	
	'- Descripcion de moneda
	Public sPrem_currDesc As String
	'- Estado de la poliza
	Public spenstatus_pol As String
	
	'- Tipo de operacion sobre propuesta/cotizacion segun table7043
	Public Enum ePropQuotOperations
		eQuotOperQuery = 1 '+ Consulta
		eQuotOperApprove = 2 '+ Aprobar/Convertir
		eQuotOperRejected = 3 '+ Rechazar
		eQuotOperAnnul = 4 '+ Anular
		eQuotOperModernize = 5 '+ Actualizar
		eQuotOperRegulate = 6 '+ Regularizar
		eQuotOperReverse = 7 '+ Reverso
		eQuotOperReceive = 8 '+ Recepcionar propuestas
	End Enum
	
	'- Contiene la clave de los registros temporales creados por proceso
	Private mstrKey As String
	
	' CertypeByOrigin: Permite obtener el tipo de registro segun el origen de la propuesta o cotizacion
	Public ReadOnly Property CertypeByOrigin(ByVal nOrigin As Request.eRequestOrigin, ByVal sTypeDoc As String) As String
		Get
			
			Select Case nOrigin
				Case Request.eRequestOrigin.reqOrigIssue
					'+Cotizacion
					If sTypeDoc = "1" Then
						CertypeByOrigin = CStr(Constantes.ePolCertype.cstrQuotation)
						'+Propuesta
					Else
						CertypeByOrigin = CStr(eCollection.Premium.TypeRecord.cstrRequest)
					End If
				Case Request.eRequestOrigin.reqOrigModified
					If sTypeDoc = "1" Then
						CertypeByOrigin = CStr(Constantes.ePolCertype.cstrAmendQuot)
					Else
						CertypeByOrigin = CStr(Constantes.ePolCertype.cstrAmendProposal)
					End If
				Case Request.eRequestOrigin.reqOrigRenewal
					If sTypeDoc = "1" Then
						CertypeByOrigin = CStr(Constantes.ePolCertype.cstrRenewalQuot)
					Else
						CertypeByOrigin = CStr(Constantes.ePolCertype.cstrRenewalProposal)
					End If
				Case Else
					CertypeByOrigin = CStr(Constantes.ePolCertype.cstrSpecialProposal)
			End Select
			
		End Get
	End Property
	
	'%sGeneratedPol: Retorna la llave del proceso
	Public ReadOnly Property sKey() As String
		Get
			
			sKey = mstrKey
			
		End Get
	End Property
	
	'%InsUpdTConvertions: Se encarga de actualizar la tabla TConvertions
	Private Function InsUpdTConvertions(ByVal nAction As Integer) As Boolean
		Dim lrecInsUpdTConvertions As eRemoteDB.Execute
		
		On Error GoTo InsUpdTConvertionsErr
		
		lrecInsUpdTConvertions = New eRemoteDB.Execute
		
		'+ Definición de parámetros para stored procedure 'insudb.insUpdCapitalPremium'
		'+ Información leída el 30/11/1999 15:49:51
		
		With lrecInsUpdTConvertions
			.StoredProcedure = "InsUpdTConvertions"
			.Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProponum", nProponum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sPen_doc", sPen_doc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dDate_init", dDate_init, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nStatus", nStatus, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dStat_date", dStat_date, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nNullcode", nNullcode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dExpirdat", dExpirdat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dLimit_date", dLimit_date, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sObserv", sObserv, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nServ_order", nServ_order, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nStatus_ord", nStatus_ord, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBordereaux", nBordereaux, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nFirst_prem", nFirst_prem, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPrem_curr", nPrem_curr, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sPrem_che", sPrem_che, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sPay_order", sPay_order, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nExpenses", nExpenses, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDevolut", sDevolut, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOrigin", nOrigin, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nNum_doc", nNum_doc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nNo_convers", nNo_convers, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRoutine", nRoutine, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nHealthexp", nHealthexp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("spenstatus_pol", spenstatus_pol, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			InsUpdTConvertions = .Run(False)
		End With
		
InsUpdTConvertionsErr: 
		If Err.Number Then
			InsUpdTConvertions = False
		End If
		'UPGRADE_NOTE: Object lrecInsUpdTConvertions may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecInsUpdTConvertions = Nothing
	End Function
	
	'%InsUpdPayOrder: Actualizar el indicador de orden de pago de TConvertions
	Public Function InsUpdPayOrder(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal sPay_order As String, ByVal nConcept As Short, ByVal nUsercode As Double) As Boolean
		Dim lrecInsUpdPayOrder As eRemoteDB.Execute
		
		On Error GoTo InsUpdPayOrderErr
		
		lrecInsUpdPayOrder = New eRemoteDB.Execute
		
		'+ Definición de parámetros para stored procedure 'insudb.insUpdCapitalPremium'
		'+ Información leída el 30/11/1999 15:49:51
		
		With lrecInsUpdPayOrder
			.StoredProcedure = "updTConvertions_sPayorder"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sPay_order", sPay_order, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nConcept", nConcept, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			InsUpdPayOrder = .Run(False)
		End With
		
InsUpdPayOrderErr: 
		If Err.Number Then
			InsUpdPayOrder = False
		End If
		'UPGRADE_NOTE: Object lrecInsUpdPayOrder may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecInsUpdPayOrder = Nothing
	End Function
	
	'%Add: Crea un registro en la tabla
	Public Function Add() As Boolean
		Add = InsUpdTConvertions(1)
	End Function
	
	'%Update: Actualiza un registro en la tabla
	Public Function Update() As Boolean
		Update = InsUpdTConvertions(2)
	End Function
	
	
	'%Delete: Borra un registro en la tabla
	Public Function Delete() As Boolean
		Delete = InsUpdTConvertions(3)
	End Function
	
	'%InsValCA099_K: Validaciones de la transacción(Header)
	Public Function insValCA099_K(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nOrigin As Integer, ByVal sBrancht As String, ByVal nOperat As Integer, ByVal nNumCotPro As Integer, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nUsercode As Integer, ByVal nTypeDoc As Integer, ByVal nAccion As Integer, ByVal dStartdate As Date, ByVal sSche_Code As String, ByVal nPolicy As Double, ByVal nProponum As Double) As String
		Dim lclsErrors As eFunctions.Errors
		Dim lclsWindows As eSecurity.Windows
		Dim lclsMenues As eFunctions.Menues
		Dim lclsCertificat As Certificat
		Dim lclsFinance As Object
		Dim lclsRequest As Request
        Dim lclsTab_waitPo As Tab_waitPo
        Dim strResult As String = ""

        '- Tipo de registro a obtener segun origen y tipo (cotizacion o propuesta)
        Dim lstrCertype As String
		Dim ldtmEffecdate As Date
        Dim lblnError As Boolean
        Dim lstrError As String = String.Empty
		Dim lstrSep As String

        Try

            lstrSep = "||"

            ldtmEffecdate = IIf(dEffecdate = eRemoteDB.Constants.dtmNull, Today, dEffecdate)

            '+Validacion del campo Tipo de documento
            If nTypeDoc = 1 Then
                If nOperat = ePropQuotOperations.eQuotOperReceive Then
                    lstrError = lstrError & lstrSep & "800041"
                End If
            End If

            '+Validacion del campo Ramo
            If nBranch <= 0 Then
                lstrError = lstrError & lstrSep & "1022"
            End If

            '+Validacion del Producto
            If nBranch <= 0 And nProduct <= 0 Then
                lstrError = lstrError & lstrSep & "1014"
            End If


            '+Si ramo no es de vida, el origen no puede ser no saldada, prorrogada, rescate ni prestamo
            If nBranch > 0 And nProduct > 0 Then
                If ((sBrancht <> "1") And (nOrigin = Request.eRequestOrigin.reqOrigSettled Or nOrigin = Request.eRequestOrigin.reqOrigExtended Or nOrigin = Request.eRequestOrigin.reqOrigSurrender Or nOrigin = Request.eRequestOrigin.reqOrigLoan)) Then
                    lstrError = lstrError & lstrSep & "55946"
                End If
            End If
            '+Validacion de la operacion para la operacion actualizar
            If nAccion <> eFunctions.Menues.TypeActions.clngActionQuery And (nOperat = eRemoteDB.Constants.intNull Or nOperat = 0) Then
                lstrError = lstrError & lstrSep & "55742"
            End If

            '+Validacion de la operacion para la operacion actualizar
            If nAccion <> eFunctions.Menues.TypeActions.clngActionQuery And (nOrigin <= 0) Then
                lstrError = lstrError & lstrSep & "55943"
                lblnError = True
            End If

            '+Validacion nivel de actualizacion del usuario según operación
            If nOperat = ePropQuotOperations.eQuotOperApprove Or nOperat = ePropQuotOperations.eQuotOperRejected Or nOperat = ePropQuotOperations.eQuotOperAnnul Then
                lclsMenues = New eFunctions.Menues
                '+ Rescata el módulo al cual pertenece la transacción
                lclsWindows = New eSecurity.Windows
                If lclsWindows.reaWindows("CA099") Then
                    Call lclsMenues.ValActionLevel("CA099", CShort("2"), sSche_Code, lclsWindows.nInqlevel, lclsWindows.nAmelevel)
                    '+ Si el nivel no permite la conversión, se envia la validación correspondiente.
                    If Not lclsMenues.mblnAmeAcces Then
                        lstrError = lstrError & lstrSep & "55725"
                    End If
                End If
            End If

            '+Validacion numero de propuesta/cotizacion
            If ((nNumCotPro <= 0) And (nOperat <> 1) And (nOrigin = Request.eRequestOrigin.reqOrigCancelation Or nOrigin = Request.eRequestOrigin.reqOrigRehab Or nOrigin = Request.eRequestOrigin.reqOrigSettled Or nOrigin = Request.eRequestOrigin.reqOrigExtended Or nOrigin = Request.eRequestOrigin.reqOrigSurrender Or nOrigin = Request.eRequestOrigin.reqOrigLoan)) Then
                lstrError = lstrError & lstrSep & "55652"
            End If

            '+ Se obtiene tipo de registro asociado al origen y tipo (cotizacion / propuesta)

            If Not lblnError Then

                lstrCertype = CertypeByOrigin(nOrigin, CStr(nTypeDoc))

                '+Validacion que la cotizacion corresponda al origen correspondiente
                If nNumCotPro > 0 Then
                    If (nOrigin = Request.eRequestOrigin.reqOrigCancelation Or nOrigin = Request.eRequestOrigin.reqOrigRehab Or nOrigin = Request.eRequestOrigin.reqOrigSettled Or nOrigin = Request.eRequestOrigin.reqOrigExtended Or nOrigin = Request.eRequestOrigin.reqOrigSurrender Or nOrigin = Request.eRequestOrigin.reqOrigLoan) Then
                        lclsRequest = New Request
                        If lclsRequest.Find(lstrCertype, nBranch, nProduct, nNumCotPro, nCertif, dStartdate) Then
                            If lclsRequest.nOrigin <> nOrigin Then
                                lstrError = lstrError & lstrSep & "55743"
                            End If
                        End If
                    End If
                End If

                '+ Valida cuando la cotizacion esta llena
                If (nNumCotPro > 0) Then
                    If nCertif = eRemoteDB.Constants.intNull Then
                        nCertif = 0
                    End If
                    lclsCertificat = New Certificat
                    If Not lclsCertificat.Find(lstrCertype, nBranch, nProduct, nNumCotPro, nCertif) Then
                        lstrError = lstrError & lstrSep & "55651"
                    Else
                        If (nOperat = ePropQuotOperations.eQuotOperApprove And lclsCertificat.dMaximum_da <> eRemoteDB.Constants.dtmNull) Then
                            If ldtmEffecdate > lclsCertificat.dMaximum_da Then
                                lstrError = lstrError & lstrSep & "55654"
                            End If
                        End If

                        If nOperat = ePropQuotOperations.eQuotOperReverse Then
                            If lclsCertificat.nStatquota = 1 Or lclsCertificat.nStatquota = 2 Then
                                lstrError = lstrError & lstrSep & "55118"
                            End If

                            Call ValType_move(lstrCertype, nBranch, nProduct, nNumCotPro, nCertif)
                            If Me.nRequest_nu > 0 Then
                                lstrError = lstrError & lstrSep & "55119"
                            End If
                        End If

                        If nAccion <> eFunctions.Menues.TypeActions.clngActionQuery And nOperat <> 1 And nOperat <> 7 And lclsCertificat.nStatquota <> 1 Then
                            lblnError = True
                            lstrError = lstrError & lstrSep & "55741"
                        End If

                        If Not lblnError Then
                            '+ Sola para generales y cuando sea aprobar una propuesta de renovacion
                            If sBrancht <> "1" And nOperat = ePropQuotOperations.eQuotOperApprove And nOrigin = Request.eRequestOrigin.reqOrigRenewal Then

                                Call lclsCertificat.Find("2", nBranch, nProduct, nPolicy, nCertif)

                                If lclsCertificat.sRenewal <> "1" And lclsCertificat.nPayfreq = 8 Then
                                    lclsFinance = eRemoteDB.NetHelper.CreateClassInstance("eFinance.FinanceDraft")
                                    If CountDraft("2", nBranch, nProduct, nPolicy, nCertif, 1) Then
                                        lstrError = lstrError & lstrSep & "55727"
                                    End If
                                    'UPGRADE_NOTE: Object lclsFinance may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
                                    lclsFinance = Nothing
                                End If

                            End If
                        End If

                    End If
                End If
            End If

            '+ Se realizan las validaciones cuando se trata de una aprobación de propuesta de rehabilitación
            If nOrigin = Request.eRequestOrigin.reqOrigRehab And nOperat = ePropQuotOperations.eQuotOperApprove Then
                lclsCertificat = New Certificat
                If lclsCertificat.Find("8", nBranch, nProduct, nProponum, nCertif, True) Then
                    If lclsCertificat.nWait_code <> eRemoteDB.Constants.intNull Then
                        lclsTab_waitPo = New Tab_waitPo
                        Call lclsTab_waitPo.Find(lclsCertificat.nWait_code, eRemoteDB.Constants.intNull)
                        If lclsTab_waitPo.sConvert = "2" Then
                            lstrError = lstrError & lstrSep & "55871"
                        End If
                        'UPGRADE_NOTE: Object lclsTab_waitPo may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
                        lclsTab_waitPo = Nothing

                        '+ Se verifica que la póliza tiene fondos suficientes en la cuenta corriente para pagar los recibos de prima
                        '+ asociados a la propuesta de rehabilitación
                        If Not insValPremiumRehabilitate("8", nBranch, nProduct, nProponum, nCertif, nPolicy, nUsercode, dEffecdate) Then
                            lstrError = lstrError & lstrSep & "978004|0|1|" & Me.sText
                        End If
                    End If
                End If
            End If

            If lstrError <> String.Empty Then
                lstrError = Mid(lstrError, 3)
                lclsErrors = New eFunctions.Errors
                With lclsErrors
                    .ErrorMessage("CA099",  ,  ,  ,  ,  , lstrError)
                    strResult = .Confirm()
                End With
            End If
            Return strResult
        Catch ex As Exception
            Return strResult = strResult & Err.Description
        Finally
            'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
            lclsErrors = Nothing
            'UPGRADE_NOTE: Object lclsWindows may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
            lclsWindows = Nothing
            'UPGRADE_NOTE: Object lclsMenues may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
            lclsMenues = Nothing
            'UPGRADE_NOTE: Object lclsCertificat may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
            lclsCertificat = Nothing
            'UPGRADE_NOTE: Object lclsRequest may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
            lclsRequest = Nothing
        End Try
    End Function
	
	'%InsValCA099: Validaciones de la transacción(Folder)
    Public Function insValCA099(ByVal sOperat As String, ByVal nNullcode As Integer, ByVal nStatus As Integer, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal dStartdate As Date, ByVal nProponum As Double, Optional ByVal dStatdate As Date = #12:00:00 AM#, Optional ByVal dMaximum_da As Date = #12:00:00 AM#) As String
        '- Objeto de mensajes de error
        Dim lclsErrors As eFunctions.Errors
        Dim lcslCertificat As Certificat
        Dim lclsPolicy_his As Policy_his
        Dim lclsTab_waitPo As Tab_waitPo
        Dim lclsProduct As eProduct.Product
        Dim lclsWay_pay_prod As eProduct.Way_pay_prod
        Dim lstrMessage As String
        Dim strResult As String = ""

        Dim lintCertif As Integer
        Dim ldEffecdate As Date

        If nCertif = eRemoteDB.Constants.intNull Then
            lintCertif = 0
        Else
            lintCertif = nCertif
        End If

        Try

            lclsErrors = New eFunctions.Errors

            '+ Si la operación es rechazar o anular se debe incluir la causa.
            If sOperat = "3" Or sOperat = "4" Then
                If nNullcode = eRemoteDB.Constants.intNull Then
                    Call lclsErrors.ErrorMessage("CA099", 3940)
                End If
            Else
                If sOperat <> "7" And (sCertype = "1" Or sCertype = 3) Then
                    If dStatdate < dEffecdate Or (dStatdate > dMaximum_da And dMaximum_da > #12:00:00 AM#) Then
                        Call lclsErrors.ErrorMessage("CA099", 9026)
                    End If
                End If
            End If
            '+ Si pasa a aprobada
            If sOperat = "2" Then
                '+ Estaba pendiente
                If nStatus = Certificat.Stat_quot.esqPending Then
                    lcslCertificat = New Certificat
                    If lcslCertificat.Find(sCertype, nBranch, nProduct, nPolicy, lintCertif, True) Then

                        '+ Se verifica si se trata de una propuesta de rehabilitación, en cuyo caso, la validación se realiza
                        '+ con la fecha de efecto de la propuesta y no con la fecha de inicio
                        If sCertype = "8" And dEffecdate < lcslCertificat.dStartdate Then
                            Call lclsErrors.ErrorMessage("CA099", 3262)
                        Else
                            If sCertype <> "8" And dStartdate < lcslCertificat.dStartdate Then
                                Call lclsErrors.ErrorMessage("CA099", 3262)
                            End If
                        End If

                        If lcslCertificat.nWait_code <> eRemoteDB.Constants.intNull Then
                            lclsTab_waitPo = New Tab_waitPo
                            Call lclsTab_waitPo.Find(lcslCertificat.nWait_code, eRemoteDB.Constants.intNull)
                            If lclsTab_waitPo.sConvert = "2" Then
                                Call lclsErrors.ErrorMessage("CA099", 55871)
                            End If
                            'UPGRADE_NOTE: Object lclsTab_waitPo may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
                            lclsTab_waitPo = Nothing
                        End If

                        If sOperat = CStr(ePropQuotOperations.eQuotOperReverse) Then
                            If lcslCertificat.nStatquota = 1 Or lcslCertificat.nStatquota = 2 Then
                                Call lclsErrors.ErrorMessage("CA099", 55118)
                            End If

                            Call ValType_move(sCertype, nBranch, nProduct, nPolicy, lintCertif)
                            If Me.nRequest_nu > 0 Then
                                Call lclsErrors.ErrorMessage("CA099", 55119)
                            End If
                        End If
                    End If
                    ValReqFields(sCertype, nBranch, nProduct, nPolicy, lintCertif, dEffecdate, lclsErrors)

                End If
                '+ si es aprobacion de sol de endoso  verifica las fechas
                If sCertype = "6" Then
                    lcslCertificat = New Certificat
                    If lcslCertificat.Find("2", nBranch, nProduct, nProponum, lintCertif, True) Then
                        If lcslCertificat.sStatusva = "6" Then
                            Call lclsErrors.ErrorMessage("CA099", 3063)
                        End If
                    End If

                    lclsPolicy_his = New Policy_his
                    ldEffecdate = lclsPolicy_his.reapolicy_hisdate("2", nBranch, nProduct, nProponum, lintCertif, 57)

                    If (ldEffecdate <> eRemoteDB.Constants.dtmNull) And (dStartdate < ldEffecdate) Then

                        lstrMessage = InsValChangePremium(sCertype, nBranch, nProduct, nPolicy, lintCertif, dEffecdate)
                        If lstrMessage <> String.Empty Then
                            Call lclsErrors.ErrorMessage("CA099", 56185, , , lstrMessage)
                        End If
                    End If
                    ldEffecdate = eRemoteDB.Constants.dtmNull
                    ldEffecdate = lclsPolicy_his.reapolicy_hisdate("2", nBranch, nProduct, nProponum, lintCertif, 58)
                    If (ldEffecdate <> eRemoteDB.Constants.dtmNull) And (dStartdate < ldEffecdate) Then
                        lstrMessage = InsValChangePremium(sCertype, nBranch, nProduct, nPolicy, lintCertif, dEffecdate)
                        If lstrMessage <> String.Empty Then
                            Call lclsErrors.ErrorMessage("CA099", 56185, , , lstrMessage)
                        End If

                    End If
                    'UPGRADE_NOTE: Object lclsPolicy_his may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
                    lclsPolicy_his = Nothing
                End If
                '+ si es una aprobación de propuesta se verifica el pago de la primera prima
                If sCertype = "1" Then
                    lclsProduct = New eProduct.Product
                    If lclsProduct.Find(nBranch, nProduct, dEffecdate) Then
                        '+ Se verifica si se definió el pago de Primera Prima a nivel del Producto
                        If lclsProduct.sFirst_pay = "1" Then
                            If Not insValPremiumFirst(nPolicy, 1178) Then
                                Call lclsErrors.ErrorMessage("CA099", 60017, , , Me.sText)
                            End If
                        Else
                            '+ Si no se definió en Producto, se verifica Primera Prima a nivel de la Vía de pago
                            lclsWay_pay_prod = New eProduct.Way_pay_prod
                            If lclsWay_pay_prod.Find(nBranch, nProduct, lclsProduct.nWay_pay, dEffecdate) Then
                                '+ Si se indicó Primera Prima para la Vía de Pago, se valida el depósito de la misma
                                If lclsWay_pay_prod.sPrem_first = "1" Then
                                    If Not insValPremiumFirst(nPolicy, 1178) Then
                                        Call lclsErrors.ErrorMessage("CA099", 60017, , , Me.sText)
                                    End If
                                End If
                            End If
                            'UPGRADE_NOTE: Object lclsWay_pay_prod may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
                            lclsWay_pay_prod = Nothing
                        End If
                    End If
                    If insValPremiumMin(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate) Then
                        Call lclsErrors.ErrorMessage("CA099", 750142, , , Me.sText)
                    End If
                    'UPGRADE_NOTE: Object lclsProduct may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
                    lclsProduct = Nothing
                End If
            End If
            Return strResult = lclsErrors.Confirm
        Catch ex As Exception
            Return strResult = strResult & Err.Description
        Finally
            'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
            lclsErrors = Nothing
            'UPGRADE_NOTE: Object lcslCertificat may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
            lcslCertificat = Nothing
        End Try
    End Function
	
	'%InsPostCA099: Ejecuta el post de la transacción
    Public Function insPostCA099(ByVal WindowType As String, ByVal sAction As String, ByVal nProponum As Double, ByVal nPolicy As Double, ByVal sPen_doc As String, ByVal dDate_init As Date, ByVal nStatus As Integer, ByVal dStat_date As Date, ByVal nNoConvers As Integer, ByVal dEffecdate As Date, ByVal dExpirdat As Date, ByVal dLimit_date As Date, ByVal sObserv As String, ByVal nServ_order As Double, ByVal nStatus_ord As Integer, ByVal nBordereaux As Integer, ByVal nFirst_prem As Double, ByVal nPrem_curr As Integer, ByVal sPrem_che As String, ByVal sPay_order As String, ByVal nExpenses As Double, ByVal sDevolut As String, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nOrigin As Integer, ByVal sClient As String, ByVal nCertif As Double, ByVal nUsercode As Integer, ByVal nOperat As Integer, ByVal nNum_doc As Integer, ByVal nWait_code As Integer, Optional ByVal sKey As String = "", Optional ByVal nHealthexp As Double = 0, Optional ByVal nRoutine As Double = 0, Optional ByVal spenstatus_pol As String = "") As Boolean
        On Error GoTo insPostCA099_Err
        Dim lcslCertificat As Certificat

        insPostCA099 = True

        If WindowType <> "PopUp" Then
            If nOperat <> 1 And nOperat <> eRemoteDB.Constants.intNull Then
                If sAction = "Update" Then
                    '+Si la opción seleccionada es Actualizar se realiza modificacion masiva de los datos de tconvertions
                    insPostCA099 = insUpdPropQuot_Convertions(nOperat, sCertype, dEffecdate, nUsercode, sKey, nHealthexp, nExpenses, nRoutine, "CA099")
                End If
            End If
        Else
            '+Ventana popup
            With Me
                .sCertype = sCertype
                .nBranch = nBranch
                .nProduct = nProduct
                .nCertif = nCertif
                .nPolicy = nPolicy
                .nProponum = nProponum
                .nOrigin = nOrigin
                .sPen_doc = sPen_doc
                .dDate_init = dDate_init
                .nStatus = nStatus
                .dStat_date = dStat_date
                .nNullcode = nNoConvers
                .dEffecdate = dEffecdate
                .dExpirdat = dExpirdat
                .dLimit_date = dLimit_date
                .sObserv = sObserv
                .nServ_order = nServ_order
                .nStatus_ord = nStatus_ord
                .nBordereaux = nBordereaux
                .nFirst_prem = nFirst_prem
                .nPrem_curr = nPrem_curr
                .sPrem_che = sPrem_che
                .sPay_order = sPay_order
                .nExpenses = nExpenses
                .sDevolut = sDevolut
                .sClient = sClient
                .nNum_doc = nNum_doc
                .nWait_code = nWait_code
                .nNo_convers = nNoConvers
                .nRoutine = nRoutine
                .nHealthexp = nHealthexp
                .spenstatus_pol = spenstatus_pol
                Select Case sAction
                    Case "Update"
                        insPostCA099 = Update()
                        lcslCertificat = New Certificat
                        If lcslCertificat.Find(sCertype, nBranch, nProduct, nPolicy, nCertif) Then
                            lcslCertificat.nWait_code = nWait_code
                            lcslCertificat.Update()
                        End If
                        'UPGRADE_NOTE: Object lcslCertificat may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
                        lcslCertificat = Nothing
                    Case "Delete"
                        insPostCA099 = Delete()
                End Select
            End With
        End If
insPostCA099_Err:
        If Err.Number Then
            insPostCA099 = False
        End If
    End Function
	'% insUpdPropQuot_Convertions: Esta rutina se encarga de actualizar las tablas correspondientes segun la
	'%                             informacion de cotizaciones propuestas registrada en convertions
    Public Function insUpdPropQuot_Convertions(ByVal nOperat As ePropQuotOperations, ByVal sCertype As String, ByVal dEffecdate As Date, ByVal nUsercode As Integer, ByVal Skey_In As String, Optional ByVal nHealthexp As Double = 0, Optional ByVal nExpenses As Double = 0, Optional ByVal nRoutine As Double = 0, Optional ByVal sCodispl As String = "") As Boolean
        Dim lrecinsUpdpropquot_convertions As eRemoteDB.Execute
        On Error GoTo insUpdpropquot_convertions_Err

        lrecinsUpdpropquot_convertions = New eRemoteDB.Execute

        '+
        '+ Definición de store procedure insUpdpropquot_convertions al 08-05-2002 19:03:28
        '+
        With lrecinsUpdpropquot_convertions
            .StoredProcedure = "insUpdpropquot_convertions"
            .Parameters.Add("nOperat", nOperat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sKey_In", Skey_In, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nHealthexp", nHealthexp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nExpenses", nExpenses, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nRoutine", nRoutine, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sCodispl", sCodispl, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sKey", "", eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            insUpdPropQuot_Convertions = .Run(False)
            If insUpdPropQuot_Convertions Then
                'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
                If IsDBNull(.Parameters("sKey").Value) Then
                    mstrKey = ""
                Else
                    mstrKey = .Parameters("sKey").Value
                End If
            End If
        End With

insUpdpropquot_convertions_Err:
        If Err.Number Then
            insUpdPropQuot_Convertions = False
        End If
        'UPGRADE_NOTE: Object lrecinsUpdpropquot_convertions may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecinsUpdpropquot_convertions = Nothing
        On Error GoTo 0
    End Function
	
	
	
	'% Find_PropSpecial: Busca la informacion de la propuesta especial
	Public Function Find_PropSpecial(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nProponum As Double) As Boolean
		Dim lrecReaSpecial_Prop As eRemoteDB.Execute
		
		On Error GoTo ReaSpecial_Prop_Err
		
		lrecReaSpecial_Prop = New eRemoteDB.Execute
		
		'+
		'+ Definición de store procedure reaCertificat_ca099 al 11-23-2001 16:02:56
		'+
		With lrecReaSpecial_Prop
			.StoredProcedure = "ReaSpecial_Prop"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProponum", nProponum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(True) Then
				Me.nPolicy = .FieldToClass("nPolicy")
				Me.nCertif = .FieldToClass("nCertif")
				Me.dStartdate = .FieldToClass("dStartdate")
				Me.sPolitype = .FieldToClass("sPolitype")
				.RCloseRec()
				Find_PropSpecial = True
			Else
				Find_PropSpecial = False
			End If
		End With
		
ReaSpecial_Prop_Err: 
		If Err.Number Then
			Find_PropSpecial = False
		End If
		
		'UPGRADE_NOTE: Object lrecReaSpecial_Prop may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecReaSpecial_Prop = Nothing
		On Error GoTo 0
	End Function
	
	'% Find_PropSpecial: Busca la informacion de la propuesta especial
	Public Function Find_Prop_ren(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nProponum As Double) As Boolean
		Dim lrecReaProp_Ren As eRemoteDB.Execute
		
		On Error GoTo Find_Prop_ren_Err
		
		lrecReaProp_Ren = New eRemoteDB.Execute
		
		'+
		'+ Definición de store procedure reaCertificat_ca099 al 11-23-2001 16:02:56
		'+
		With lrecReaProp_Ren
			.StoredProcedure = "ReaPro_Ren"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProponum", nProponum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run() Then
				Me.nPolicy = .FieldToClass("nPolicy")
				Me.nCertif = .FieldToClass("nCertif")
				Me.dStartdate = .FieldToClass("dStartdate")
				Me.sPolitype = .FieldToClass("sPolitype")
				.RCloseRec()
				Find_Prop_ren = True
			Else
				Find_Prop_ren = False
			End If
		End With
		
Find_Prop_ren_Err: 
		If Err.Number Then
			Find_Prop_ren = False
		End If
		
		'UPGRADE_NOTE: Object lrecReaProp_Ren may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecReaProp_Ren = Nothing
		On Error GoTo 0
	End Function
	
	
	'%ValType_move: Valida si se genero order de pago de devolucion
	Public Function ValType_move(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double) As Boolean
		'- Objeto para busqueda de datos
		Dim lrecValType_move As eRemoteDB.Execute
		
		On Error GoTo ValType_move_Err
		
		lrecValType_move = New eRemoteDB.Execute
		
		With lrecValType_move
			.StoredProcedure = "REATYPE_MOVE"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRequest_nu", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(False) Then
				ValType_move = True
				Me.nRequest_nu = .Parameters("nRequest_nu").Value
			End If
		End With
		
ValType_move_Err: 
		If Err.Number Then
			ValType_move = False
		End If
		'UPGRADE_NOTE: Object lrecValType_move may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecValType_move = Nothing
		On Error GoTo 0
	End Function
	
	
	'%Class_Initialize: Inicializa las propiedades cuando se instancia la clase
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		nProponum = eRemoteDB.Constants.intNull
		nPolicy = eRemoteDB.Constants.intNull
		nNum_doc = eRemoteDB.Constants.intNull
		sPen_doc = String.Empty
		dDate_init = eRemoteDB.Constants.dtmNull
		nStatus = eRemoteDB.Constants.intNull
		sStatus = String.Empty
		dStat_date = eRemoteDB.Constants.dtmNull
		nNullcode = eRemoteDB.Constants.intNull
		nRole = eRemoteDB.Constants.intNull
		dEffecdate = eRemoteDB.Constants.dtmNull
		dExpirdat = eRemoteDB.Constants.dtmNull
		dLimit_date = eRemoteDB.Constants.dtmNull
		sObserv = String.Empty
		nServ_order = eRemoteDB.Constants.intNull
		nStatus_ord = eRemoteDB.Constants.intNull
		sStatus_ord = String.Empty
		nBordereaux = eRemoteDB.Constants.intNull
		nFirst_prem = eRemoteDB.Constants.intNull
		nPrem_curr = eRemoteDB.Constants.intNull
		sPrem_che = String.Empty
		sPay_order = String.Empty
		nExpenses = eRemoteDB.Constants.intNull
		sDevolut = String.Empty
		sCertype = String.Empty
		nBranch = eRemoteDB.Constants.intNull
		nProduct = eRemoteDB.Constants.intNull
		nOrigin = eRemoteDB.Constants.intNull
		sClient = String.Empty
		nCertif = eRemoteDB.Constants.intNull
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'%Find_proponum: Retorna la cantidad de polizas asociadas a una propuesta
	Public Function Find_Proponum(ByVal sCertype As String, ByVal nProponum As Double, ByVal sOperat As String) As Boolean
		Dim lrereaProponum As eRemoteDB.Execute
		If sOperat = CStr(ePropQuotOperations.eQuotOperApprove) Then
			
			On Error GoTo Find_Err
			'+Definición de parámetros para stored procedure 'ReaActivelife_count'
			lrereaProponum = New eRemoteDB.Execute
			With lrereaProponum
				.StoredProcedure = "ReaProponum"
				.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nProponum", nProponum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nCount", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 12, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If .Run(False) Then
					Find_Proponum = .Parameters("nCount").Value > 0
				End If
			End With
			
			'UPGRADE_NOTE: Object lrereaProponum may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			lrereaProponum = Nothing
		Else
			Find_Proponum = False
		End If
Find_Err: 
		If Err.Number Then
			Find_Proponum = -1
		End If
		On Error GoTo 0
	End Function
	
	'% CountDraft: Esta función se encarga de verificar si existe giros a partir de los datos dados por parametro.
	Public Function CountDraft(ByVal sCertype As String, ByVal nBranch As Double, ByVal nProduct As Double, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nStat_draft As Integer) As Boolean
		Dim lrecValFinanc_dra_Stat As eRemoteDB.Execute
		
		On Error GoTo CountDraft_Err
		lrecValFinanc_dra_Stat = New eRemoteDB.Execute
		CountDraft = False
		
		'+ Stored procedure parameters definition 'insudb.ValFinanc_dra_Stat'
		'+ Data of 09/10/1999 02:28:46 PM
		'+ Definición de parámetros para stored procedure 'insudb.ValFinanc_dra_Stat'
		'+ Información leída el 10/09/1999 02:28:46 PM
		With lrecValFinanc_dra_Stat
			.StoredProcedure = "ValCount_draft"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nStat_draft", nStat_draft, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCount", ncount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				CountDraft = .Parameters("nCount").Value > 0
			End If
		End With
		
CountDraft_Err: 
		If Err.Number Then
			CountDraft = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecValFinanc_dra_Stat may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecValFinanc_dra_Stat = Nothing
	End Function
	
	'% insValPremiumFirst: Esta función se encarga de verificar si se puede pagar la primera prima de la póliza.
	Public Function insValPremiumFirst(ByVal nProponum As Double, ByVal nUsercode As Integer) As Boolean
		Dim lrecinsValPremiumFirst As eRemoteDB.Execute
		
		On Error GoTo insValPremiumFirst_Err
		lrecinsValPremiumFirst = New eRemoteDB.Execute
		insValPremiumFirst = False
		
		With lrecinsValPremiumFirst
			.StoredProcedure = "InsQuotPropConvertionPKG.insValPremiumFirst"
			.Parameters.Add("nProponum", nProponum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOperation", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sText", "", eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 200, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				insValPremiumFirst = .Parameters("nOperation").Value <> 0
				Me.sText = .Parameters("sText").Value
			End If
		End With
		
insValPremiumFirst_Err: 
		If Err.Number Then
			insValPremiumFirst = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecinsValPremiumFirst may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsValPremiumFirst = Nothing
	End Function
	
	'% insValPremiumMin: Esta función se encarga de verificar si la Primera Prima supera la Prima Mínima establecida.
	Public Function insValPremiumMin(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date) As Boolean
		Dim lrecinsValPremiumMin As eRemoteDB.Execute
		
		On Error GoTo insValPremiumMin_Err
		lrecinsValPremiumMin = New eRemoteDB.Execute
		insValPremiumMin = False
		
		With lrecinsValPremiumMin
			.StoredProcedure = "insVal_Prem_Min"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nWaitCode", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sText", "", eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 200, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(False) Then
				insValPremiumMin = .Parameters("nWaitCode").Value <> 0
				Me.sText = .Parameters("sText").Value
			End If
		End With
		
insValPremiumMin_Err: 
		If Err.Number Then
			insValPremiumMin = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecinsValPremiumMin may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsValPremiumMin = Nothing
	End Function
	
	'% insValPremiumRehabilitate: Esta función se encarga de verificar si se pueden pagar los recibos de prima que
	'% se generan como consecuencia de la propuesta de rehabilitación
	Public Function insValPremiumRehabilitate(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nProponum As Double, ByVal nCertif As Double, ByVal nPolicy As Double, ByVal nUsercode As Integer, ByVal dEffecdate As Date) As Boolean
		Dim lclsErrors As eFunctions.Errors
		Dim lrecinsValPremiumRehabilitate As eRemoteDB.Execute
		
		On Error GoTo insValPremiumRehabilitate_Err
		
		lrecinsValPremiumRehabilitate = New eRemoteDB.Execute
		insValPremiumRehabilitate = False
		
		With lrecinsValPremiumRehabilitate
			.StoredProcedure = "InsQuotPropConvertionPKG.insValPremiumRehabilitate"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProponum", nProponum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sText", "", eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 200, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				insValPremiumRehabilitate = .Parameters("sText").Value = ""
				Me.sText = .Parameters("sText").Value
			End If
		End With
		
insValPremiumRehabilitate_Err: 
		If Err.Number Then
			insValPremiumRehabilitate = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecinsValPremiumRehabilitate may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsValPremiumRehabilitate = Nothing
	End Function
	
	'%InsValVI7000: Validaciones de la cabecera de forma VI7000
	Public Function InsValChangePremium(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date) As String
        Dim lstrErrorAll As String = String.Empty
        Dim lrecInsValChangePremium As eRemoteDB.Execute

        On Error GoTo insValChangePremium_Err

        lrecInsValChangePremium = New eRemoteDB.Execute

        '+ Definición de store procedure InsValVI7000
        With lrecInsValChangePremium
            .StoredProcedure = "insValChangePremium"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sArrayerrors", " ", eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 4000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            If .Run(False) Then
                lstrErrorAll = .Parameters("sArrayerrors").Value
            End If
        End With

        If Len(lstrErrorAll) > 0 Then
            InsValChangePremium = lstrErrorAll
        End If

insValChangePremium_Err:
        If Err.Number Then
            InsValChangePremium = "InsValChangePremium: " & Err.Description
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lrecInsValChangePremium may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecInsValChangePremium = Nothing
    End Function


    Private Function ValReqFields(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByRef lclsErrors As eFunctions.Errors) As Boolean

        Dim lstrErrorAll As String = String.Empty
        Dim lrecValReqFields As eRemoteDB.Execute

        On Error GoTo ValReqFields_Err

        lrecValReqFields = New eRemoteDB.Execute

        '+ Definición de store procedure InsValVI7000
        With lrecValReqFields
            .StoredProcedure = "VALREQUIREDFIELDS_MASSIVE"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTransaction", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sPolitype", "1", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sError", " ", eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 4000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            If .Run(False) Then
                lstrErrorAll = .Parameters("sError").Value
            End If
        End With

        If Len(lstrErrorAll) > 0 Then
            lclsErrors.ErrorMessage("CA099", , , , , , Mid(lstrErrorAll, 3))
        Else
            ValReqFields = True
        End If

ValReqFields_Err:
        If Err.Number Then
            ValReqFields = CBool("ValReqFields: " & Err.Description)
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lrecValReqFields may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecValReqFields = Nothing

    End Function
End Class






