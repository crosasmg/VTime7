Option Strict Off
Option Explicit On
Public Class Activelife
	'%-------------------------------------------------------%'
	'% $Workfile:: Activelife.cls                           $%'
	'% $Author:: Gletelier                                  $%'
	'% $Date:: 28/10/09 12:08a                              $%'
	'% $Revision:: 3                                        $%'
	'%-------------------------------------------------------%'
	
	'+ Definición de la tabla ACTIVELIFE tomada el 20/11/2001 17:45
	'+ Column_Name                                   Type      Length  Prec  Scale Nullable
	'------------------------------ --------------- - -------- ------- ----- ------ --------
	Public sCertype As String ' CHAR           1              No
	Public nBranch As Integer ' NUMBER        22     5      0 No
	Public nProduct As Integer ' NUMBER        22     5      0 No
	Public nPolicy As Double ' NUMBER        22    10      0 No
	Public nCertif As Double ' NUMBER        22    10      0 No
	Public dEffecdate As Date ' DATE           7              No
	Public nCapitaldeath As Double ' NUMBER        22    12      0 No
	Public sClient As String ' CHAR          14              No
	Public nTypDurins As Integer ' CHAR           1              No
	Public nInsurtime As Integer ' NUMBER        22     5      0 No
	Public nPremiumbas As Double ' NUMBER        22    10      2 No
	Public nSald_amount As Integer ' NUMBER        22    12      0 Yes
	Public sSald_prog As String ' CHAR           1              Yes
	Public nIntproject As Double ' NUMBER        22     4      2 No
	Public nWarminint As Double ' NUMBER        22     4      2 No
	Public dProg_date As Date ' DATE           7              Yes
	Public nModulec As Integer ' NUMBER        22     5      0 No
	Public nOption As IndemnityOptionType ' NUMBER        22     5      0 No
	Public nTypeinvest As Integer ' NUMBER        22     5      0 No
	Public nPremium As Double ' NUMBER        22    10      2 No
	Public nPremimin As Double ' NUMBER        22    10      2 Yes
	Public nCapital As Integer ' NUMBER        22    12      0 Yes
	Public nPremdeal As Double ' NUMBER        22    10      2 No
	Public dNulldate As Date ' DATE           7              Yes
	Public dStartdate As Date ' DATE           7              No
	Public dExpirdat As Date ' DATE           7              No
	Public dIssuedat As Date ' DATE           7              No
	Public nTransactio As Integer ' NUMBER        22     5      0 No
	Public nNullcode As Integer ' NUMBER        22     5      0 Yes
	Public nGroup As Integer ' NUMBER        22     5      0 Yes
	Public nSituation As Integer ' NUMBER        22     5      0 Yes
	Public nPrsugest As Double ' NUMBER        22    10      2 Yes
	Public nUsercode As Integer ' NUMBER        22     5      0 No
	Public nAgreement As Integer ' NUMBER        22     5      0 Yes
	Public nVPprsug As Double ' NUMBER        22    10      2 Yes
	Public nVPprdeal As Double ' NUMBER        22    10      2 No
	Public sInsCalPre As String ' CHAR           1              No
	
	'-Variables auxiliares
	Public nCurrency As Integer
	
	'- Prima mínima permitida según el producto
	Public nPremMin As Double
	
	'- Frecuencia de pago de la póliza/certificado
	Public nPayfreq As Integer
	
	'- Tasa según frecuencia de pago
	Public nRatepayf As Double
	
	'- Variables para la habilitación de controles.
	Public bnTypdurinsDisable As Boolean
	Public bnInsurtimeDisable As Boolean
	Public bnPremdealDisable As Boolean
	
	'- Costo del rescate
	Public nSurrCost As Double
	
	'- Retencion
	Public nRetention As Double
	
	'- Retencion %
	Public nRet_Pct As Double
	
	'- Descripcion del cliente
	Public sCliename As String
	
	'- Descripcion de la moneda
	Public sCurrDescript As String
	
	'- Fecha la ultima contribucion de prima
	Public dLastContrib As Date
	
	'- Fecha de ultima modificacion de poliza certificado
	Public dLastMove As Date
	
	'- Monto de los aportes
	Public nAmountcontr As Double
	
	'-Prima segun frecuencia de pago
	Public nPremfreq As Double
	
	'- Años poliza a la fecha de efecto de la transaccion
	Public nPolYears As Integer
	
	'- Tipos de duración
	Public Enum IduraindType
		nTypdurinsAge = 1
		nTypdurinsYear = 2
		nTypdurinsPolicy = 3
		nTypdurinsRou = 4
		nTypdurinsOpen = 5
		nTypdurinsFree = 6
	End Enum
	
	'- Tipo de Indemnizacion
	Public Enum IndemnityOptionType
		nIndemOptionA = 1
		nIndemOptionB = 2
	End Enum
	
	'- Opciones para ejecutar Calculo de Valor Poliza
	Public Enum eCalVPOptions
		eVPOptUpdateVP = 1 '+ Actualizar el VP
		eVPOptProjectVP = 2 '+ Proyectar el VP
		eVPOptRecalVP = 3 '+ Recalcular el VP
		eVPOptProjectVPNoPay = 4 '+ Proyectar el VP (sin pagos)
		eVPOptTempCalc = 5 '+ Calculo temporal
		eVPOptInverse = 6 '+ VP Inverso
	End Enum
	
	'-Variable que guarda la fecha de búsqueda
	Private mdtmEffecdate As Date
	
	'%InsValVAL630_K: Realiza la validación de la transaccion de Impresion de Cartolas de VidActiva
	Public Function InsValVAL630_K(ByVal sCodispl As String, ByVal nAction As Integer, ByVal nOption As Integer, ByVal dStartdate As Date, ByVal dEndDate As Date, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double) As String
		Dim lclsPolicy As ePolicy.Policy
		Dim lclsErrors As eFunctions.Errors
		Dim lclsCertificat As ePolicy.Certificat
		On Error GoTo InsValVAL630_K_Err
		lclsErrors = New eFunctions.Errors
		lclsPolicy = New ePolicy.Policy
		lclsCertificat = New ePolicy.Certificat
		
		With lclsErrors
			'+ Validacion de fechas
			If dStartdate = eRemoteDB.Constants.dtmNull Then
				.ErrorMessage(sCodispl, 3237)
			End If
			
			If dEndDate = eRemoteDB.Constants.dtmNull Then
				.ErrorMessage(sCodispl, 3239)
			End If
			
			If dEndDate < dStartdate Then
				.ErrorMessage(sCodispl, 3108)
			End If
			
			'+ Para busqueda puntual se valida poliza
			If nOption = 1 Then
				'+Validacion del ramo
				If nBranch = eRemoteDB.Constants.intNull Then
					.ErrorMessage(sCodispl, 60215)
				End If
				
				'+Validacion del producto
				If nProduct = eRemoteDB.Constants.intNull Then
					.ErrorMessage(sCodispl, 11009)
				End If
				
				If nPolicy = eRemoteDB.Constants.intNull Then
					.ErrorMessage(sCodispl, 21033)
				End If
				
				'+ La póliza debe corresponder con un registro válido
				If nPolicy <> eRemoteDB.Constants.intNull Then
					If Not lclsPolicy.Find("2", nBranch, nProduct, nPolicy) Then
						.ErrorMessage(sCodispl, 3001)
					End If
				End If
				
				If nCertif = eRemoteDB.Constants.intNull And nPolicy <> eRemoteDB.Constants.intNull Then
					.ErrorMessage(sCodispl, 3006)
				End If
				
				'+ si el certificado tienen valor debe existir en el sistema
				If nCertif <> eRemoteDB.Constants.intNull Then
					If Not lclsCertificat.Find("2", nBranch, nProduct, nPolicy, nCertif) Then
						.ErrorMessage(sCodispl, 8215)
					End If
				End If
				
			End If
			
			InsValVAL630_K = .Confirm
		End With
		
InsValVAL630_K_Err: 
		If Err.Number Then
			InsValVAL630_K = "InsValVAL630_K: " & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsCertificat may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsCertificat = Nothing
		'UPGRADE_NOTE: Object lclsPolicy may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsPolicy = Nothing
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
	End Function
	
	
	'%InsValVAL696_K: Realiza la validación de la transaccion Control de Caducidad
	Public Function InsValVAL696_K(ByVal sCodispl As String, ByVal nAction As Integer, ByVal nOption As Integer, ByVal dEffecdate As Date, ByVal nBranch As Integer) As String
		Dim lclsErrors As eFunctions.Errors
		Dim lclsCtrol_Date As eGeneral.Ctrol_date
		
		On Error GoTo InsValVAL696_K_Err
		lclsErrors = New eFunctions.Errors
		With lclsErrors
			'+ Validacion de fechas
			If dEffecdate = eRemoteDB.Constants.dtmNull Then
				.ErrorMessage(sCodispl, 2056)
			Else
				lclsCtrol_Date = New eGeneral.Ctrol_date
				If Not lclsCtrol_Date.InsValdLedgerdat(1, dEffecdate) Then
					.ErrorMessage(sCodispl, 1006)
				End If
			End If
			
			If nBranch = eRemoteDB.Constants.intNull Then
				.ErrorMessage(sCodispl, 11135)
			End If
			
			InsValVAL696_K = .Confirm
		End With
		
InsValVAL696_K_Err: 
		If Err.Number Then
			InsValVAL696_K = "InsValVAL696_K: " & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsCtrol_Date may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsCtrol_Date = Nothing
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
	End Function
	
	'% InsPostVAL696: Esta función permite realizar el llamado al procedimiento que crea la temporal (VAL696).
	Public Function InsPostVAL696(ByVal nOption As Integer, ByVal dEffecdate As Date, ByVal nUsercode As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer) As String
		Dim lrecInsPostVAL696 As eRemoteDB.Execute
		Dim lstrKey As String
		
		lrecInsPostVAL696 = New eRemoteDB.Execute
		
		With lrecInsPostVAL696
			lstrKey = "t" & Today.ToString("yyyyMMdd") & TimeOfDay.ToString("hhmmss") & nUsercode
			
			.StoredProcedure = "INSPOSTVAL696"
			.Parameters.Add("nOption", nOption, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecDate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUserCode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sKey", lstrKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(False) Then
				InsPostVAL696 = lstrKey
			End If
		End With
		
		'UPGRADE_NOTE: Object lrecInsPostVAL696 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecInsPostVAL696 = Nothing
	End Function
	
	'%InsValVAL709_K: Realiza la validación de la transaccion Control de Caducidad
	Public Function InsValVAL709_K(ByVal sCodispl As String, ByVal nAction As Integer, ByVal dEffecdate As Date, ByVal nBranch As Integer, ByVal nProduct As Integer) As String
		Dim lclsErrors As eFunctions.Errors
		
		On Error GoTo InsValVAL709_K_Err
		
		lclsErrors = New eFunctions.Errors
		
		'+ Validacion de fechas
		If dEffecdate = eRemoteDB.Constants.dtmNull Then
			lclsErrors.ErrorMessage(sCodispl, 55581)
		End If
		
		InsValVAL709_K = lclsErrors.Confirm
		
InsValVAL709_K_Err: 
		If Err.Number Then
			InsValVAL709_K = "InsValVAL709_K: " & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
	End Function
	
	'% ClearFields: se inicializa el valor de las variables de la clase
	Private Sub ClearFields()
		Me.sCertype = String.Empty
		Me.nBranch = eRemoteDB.Constants.intNull
		Me.nProduct = eRemoteDB.Constants.intNull
		Me.nPolicy = eRemoteDB.Constants.intNull
		Me.nCertif = eRemoteDB.Constants.intNull
		Me.dEffecdate = eRemoteDB.Constants.dtmNull
		Me.nCapitaldeath = eRemoteDB.Constants.intNull
		Me.sClient = String.Empty
		Me.nTypDurins = eRemoteDB.Constants.intNull
		Me.nInsurtime = eRemoteDB.Constants.intNull
		Me.nPremiumbas = eRemoteDB.Constants.intNull
		Me.nSald_amount = eRemoteDB.Constants.intNull
		Me.sSald_prog = String.Empty
		Me.nIntproject = eRemoteDB.Constants.intNull
		Me.nWarminint = eRemoteDB.Constants.intNull
		Me.dProg_date = eRemoteDB.Constants.dtmNull
		Me.nModulec = eRemoteDB.Constants.intNull
		Me.nOption = eRemoteDB.Constants.intNull
		Me.nTypeinvest = eRemoteDB.Constants.intNull
		Me.nPremium = eRemoteDB.Constants.intNull
		Me.nPremimin = eRemoteDB.Constants.intNull
		Me.nCapital = eRemoteDB.Constants.intNull
		Me.nPremdeal = eRemoteDB.Constants.intNull
		Me.dNulldate = eRemoteDB.Constants.dtmNull
		Me.dStartdate = eRemoteDB.Constants.dtmNull
		Me.dExpirdat = eRemoteDB.Constants.dtmNull
		Me.dIssuedat = eRemoteDB.Constants.dtmNull
		Me.nTransactio = eRemoteDB.Constants.intNull
		Me.nNullcode = eRemoteDB.Constants.intNull
		Me.nGroup = eRemoteDB.Constants.intNull
		Me.nSituation = eRemoteDB.Constants.intNull
		Me.nPrsugest = eRemoteDB.Constants.intNull
		Me.nUsercode = eRemoteDB.Constants.intNull
		Me.nAgreement = eRemoteDB.Constants.intNull
		Me.nVPprsug = eRemoteDB.Constants.intNull
		Me.nVPprdeal = eRemoteDB.Constants.intNull
		Me.sInsCalPre = String.Empty
		mdtmEffecdate = eRemoteDB.Constants.dtmNull
	End Sub
	
	'%Find: Lee la información de los datos particulares de vida activa(Activelife)
	Public Function Find(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, Optional ByVal bFind As Boolean = False) As Boolean
		Dim lrereaActivelife As eRemoteDB.Execute
		
		On Error GoTo Find_Err
		If Me.sCertype <> sCertype Or Me.nBranch <> nBranch Or Me.nProduct <> nProduct Or Me.nPolicy <> nPolicy Or Me.nCertif <> nCertif Or mdtmEffecdate <> dEffecdate Or bFind Then
			
			'+Definición de parámetros para stored procedure 'insudb.reaActivelife'
			lrereaActivelife = New eRemoteDB.Execute
			With lrereaActivelife
				.StoredProcedure = "reaActivelife_o"
				.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If .Run Then
					Me.sCertype = sCertype
					Me.nBranch = nBranch
					Me.nProduct = nProduct
					Me.nPolicy = nPolicy
					Me.nCertif = nCertif
					Me.dEffecdate = .FieldToClass("dEffecdate")
					mdtmEffecdate = Me.dEffecdate
					Me.nCapitaldeath = .FieldToClass("nCapitaldeath")
					Me.sClient = .FieldToClass("sClient")
					Me.nTypDurins = .FieldToClass("nTypdurins")
					Me.nInsurtime = .FieldToClass("nInsurtime")
					Me.nPremiumbas = .FieldToClass("nPremiumbas")
					Me.nSald_amount = .FieldToClass("nSald_amount")
					Me.sSald_prog = .FieldToClass("sSald_prog")
					Me.nIntproject = .FieldToClass("nIntproject")
					Me.nWarminint = .FieldToClass("nWarminint")
					Me.dProg_date = .FieldToClass("dProg_date")
					Me.nModulec = .FieldToClass("nModulec")
					Me.nOption = .FieldToClass("nOption")
					Me.nTypeinvest = .FieldToClass("nTypeinvest")
					Me.nPremium = .FieldToClass("nPremium")
					Me.nPremimin = .FieldToClass("nPremimin")
					Me.nCapital = .FieldToClass("nCapital")
					Me.nPremdeal = .FieldToClass("nPremdeal")
					Me.dNulldate = .FieldToClass("dNulldate")
					Me.dStartdate = .FieldToClass("dStartdate")
					Me.dExpirdat = .FieldToClass("dExpirdat")
					Me.dIssuedat = .FieldToClass("dIssuedat")
					Me.nTransactio = .FieldToClass("nTransactio")
					Me.nNullcode = .FieldToClass("nNullcode")
					Me.nGroup = .FieldToClass("nGroup")
					Me.nSituation = .FieldToClass("nSituation")
					Me.nPrsugest = .FieldToClass("nPrsugest")
					Me.nAgreement = .FieldToClass("nAgreement")
					Me.nVPprsug = .FieldToClass("nVpprsug")
					Me.nVPprdeal = .FieldToClass("nVpprdeal")
					Me.sInsCalPre = .FieldToClass("sInscalpre")
					.RCloseRec()
					Find = True
				End If
			End With
		Else
			Find = True
		End If
		'UPGRADE_NOTE: Object lrereaActivelife may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrereaActivelife = Nothing
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
	End Function
	
	'%InsCalIndemVA: Calcula el importe de indemnizacion de vida activa
	Public Function InsCalIndemVA(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date) As Double
		Dim lrecInsCalIndemVA As eRemoteDB.Execute
		
		On Error GoTo InsCalIndemVA_Err
		'+ Definición de store procedure InsCalIndemVA al 36-03-2002
		lrecInsCalIndemVA = New eRemoteDB.Execute
		With lrecInsCalIndemVA
			.StoredProcedure = "InsCalIndemVA"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nIndemamount", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				InsCalIndemVA = .Parameters("nIndemamount").Value
			End If
		End With
		
InsCalIndemVA_Err: 
		If Err.Number Then
			InsCalIndemVA = -1
		End If
		'UPGRADE_NOTE: Object lrecInsCalIndemVA may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecInsCalIndemVA = Nothing
		On Error GoTo 0
	End Function
	
	'%InsCalSuggestPrem: Obtiene los valores de prima proyectada sugerida y
	'%                   valor poliza segun prima sugerida
	Public Function InsCalSuggestPrem(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nTargetPrem As Double, ByVal nTargetVP As Integer, ByVal nUsercode As Integer) As Double
		Dim lrecInsCalSuggestPrem As eRemoteDB.Execute
		
		On Error GoTo InsCalSuggestPrem_Err
		'+ Definición de store procedure InsCalSuggestPrem al 11-04-2002
		lrecInsCalSuggestPrem = New eRemoteDB.Execute
		With lrecInsCalSuggestPrem
			.StoredProcedure = "InsCalSuggestPrem"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPpo", nTargetPrem, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nVpo", nTargetVP, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProjrent", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nModulec", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nSuggestprem", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nVpprsug", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				nPrsugest = .Parameters("nSuggestprem").Value
				nVPprsug = .Parameters("nVpprsug").Value
				InsCalSuggestPrem = nPrsugest
			End If
		End With
		
InsCalSuggestPrem_Err: 
		If Err.Number Then
			InsCalSuggestPrem = -1
		End If
		'UPGRADE_NOTE: Object lrecInsCalSuggestPrem may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecInsCalSuggestPrem = Nothing
		On Error GoTo 0
	End Function
	
	'%InsRoutineSurrender: Calcula el valor y costo de rescate de la poliza
    Public Function InsRoutineSurrender(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal sRoutine As String, Optional ByVal sSurrType As String = "1", Optional ByVal nSurr_reason As Integer = 0, Optional ByVal nCurrency As Integer = 0, Optional ByVal sCodispl As String = "", Optional ByVal nValuePol As Double = 0) As Double
        Dim lrecinsCalvalresva As eRemoteDB.Execute

        On Error GoTo InsRoutineSurrender_Err
        '+ Definición de store procedure insCalvalresva al 03-22-2002 12:25:04
        lrecinsCalvalresva = New eRemoteDB.Execute
        With lrecinsCalvalresva
            .StoredProcedure = "InsRoutineSurrender"
            .Parameters.Add("sRoutine", sRoutine, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sSurrtype", sSurrType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nSurr_reason", nSurr_reason, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sCodispl", sCodispl, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nValuepol", nValuePol, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nType", 1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nSurramount", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 20, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nSurrcost", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 20, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nRetention", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 20, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nRet_pct", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("NGROSS_BALANCE", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            If .Run(False) Then
                InsRoutineSurrender = .Parameters.Item("nSurrAmount").Value
                Me.nSurrCost = .Parameters.Item("nSurrcost").Value
                Me.nRetention = .Parameters.Item("nRetention").Value
                Me.nRet_Pct = .Parameters.Item("nRet_Pct").Value
            Else
                InsRoutineSurrender = 0
                Me.nSurrCost = 0
                Me.nRetention = 0
                Me.nRet_Pct = 0
            End If
        End With

InsRoutineSurrender_Err:
        If Err.Number Then
            InsRoutineSurrender = 0
            Me.nSurrCost = 0
            Me.nRetention = 0
            Me.nRet_Pct = 0
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lrecinsCalvalresva may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecinsCalvalresva = Nothing
    End Function
	
	'%InsCalVP: Retorna el Valor Poliza calculado
	Public Function InsCalVP(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nYear As Integer, ByVal nMonth As Integer, ByVal nOption As eCalVPOptions, ByVal nUsercode As Integer, Optional ByVal nLastvp As Double = 0, Optional ByVal nAddpremium As Double = 0, Optional ByVal nPremdeal As Double = 0, Optional ByVal nRentint As Double = 0, Optional ByVal nSuramount As Double = 0, Optional ByVal sKey As String = "") As Double
		Dim lrecInsCalVP As eRemoteDB.Execute
		
		On Error GoTo InsCalVP_Err
		'+ Definición de store procedure InsCalVP al 03-04-2002
		lrecInsCalVP = New eRemoteDB.Execute
		With lrecInsCalVP
			.StoredProcedure = "InsCalVPPkg.InsCalVP"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nMonth", nMonth, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nYear", nYear, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOption", nOption, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nLastvp", nLastvp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAddpremium", nAddpremium, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPremdeal", nPremdeal, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRentint", nRentint, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nSuramount", nSuramount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPremAcu", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAmountvp", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAmountvpw", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				InsCalVP = .Parameters("nAmountvp").Value
			Else
				InsCalVP = 0
			End If
		End With
		
InsCalVP_Err: 
		If Err.Number Then
			InsCalVP = -1
		End If
		'UPGRADE_NOTE: Object lrecInsCalVP may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecInsCalVP = Nothing
		On Error GoTo 0
	End Function
	
	'%InsGetDataVA669: Obtiene los valores iniciales de la transaccion VA669
	Public Function InsGetDataVA669(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date) As Boolean
		Dim lrecInsGetDataVA669 As eRemoteDB.Execute
		Dim lclsGeneral As eGeneral.GeneralFunction
		Dim lintMonth As Integer
		
		On Error GoTo InsGetDataVA669_Err
		'+ Definición de store procedure InsGetDataVA669 al 11-04-2002
		lrecInsGetDataVA669 = New eRemoteDB.Execute
		With lrecInsGetDataVA669
			.StoredProcedure = "InsGetDataVA669"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				InsGetDataVA669 = True
				sClient = .FieldToClass("sClient")
				sCliename = .FieldToClass("sCliename")
				nCurrency = .FieldToClass("nCurrency")
				sCurrDescript = .FieldToClass("sCurrdescript")
				dStartdate = .FieldToClass("dStartdate")
				dExpirdat = .FieldToClass("dExpirdat")
				nPremiumbas = .FieldToClass("nPremiumbas")
				nPremimin = .FieldToClass("nPremimin")
				nPremdeal = .FieldToClass("nPremdeal")
				nPremfreq = .FieldToClass("nPremdealfreq")
				nIntproject = .FieldToClass("nIntproject")
				nWarminint = .FieldToClass("nWarminint")
				nAmountcontr = .FieldToClass("nAmountcontr")
				dLastContrib = .FieldToClass("dLastContrib")
				dLastMove = .FieldToClass("dLastMove")
				lclsGeneral = New eGeneral.GeneralFunction
				Call lclsGeneral.getYearMonthDiff(dStartdate, dEffecdate, nPolYears, lintMonth)
				'UPGRADE_NOTE: Object lclsGeneral may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
				lclsGeneral = Nothing
			End If
		End With
		
InsGetDataVA669_Err: 
		If Err.Number Then
			InsGetDataVA669 = False
		End If
		'UPGRADE_NOTE: Object lrecInsGetDataVA669 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecInsGetDataVA669 = Nothing
		On Error GoTo 0
	End Function
	
	'%InsurK_AfterSurr: Entrega el capital asegurado tras realizar rescate
	Public Function InsurK_AfterSurr(ByVal nVP As Double, ByVal nDeathK As Double, ByVal nIndemnityOpt As IndemnityOptionType, ByVal nMinimalK As Double, ByVal nRescAmount As Double) As Double
		'- Valor temporal para evaluar monto con respecto a Zona Corredor
		Dim ldblZone As Double
		'- Nuevo capital tras rescate
		Dim ldblNewK As Double
		Dim lintRest As Integer
		
		If nIndemnityOpt = IndemnityOptionType.nIndemOptionA Then
			ldblZone = nVP + 0.1 * nDeathK
			'+ Si Valor Poliza (VP) no entra en Zona Corredor...
			If ldblZone < nDeathK Then
				ldblNewK = nDeathK - nRescAmount
				
				'+ Si VP esta Zona Corredor y permanece tras rescate ...
			ElseIf ldblZone <= (nDeathK - nRescAmount) Then 
				ldblNewK = nDeathK
				
				'+ Si VP esta Zona Corredor, pero sale de Zona tras rescate...
			ElseIf ldblZone > (nDeathK - nRescAmount) Then 
				ldblNewK = ldblZone - nRescAmount
			End If
			
			'+ Debe ser al menos el minimo indicado
			If ldblNewK < nMinimalK Then
				ldblNewK = nMinimalK
			End If
			
		ElseIf nIndemnityOpt = IndemnityOptionType.nIndemOptionB Then 
			ldblNewK = nDeathK
		End If
		
		'+ Capital calculado se aproxima a multiplo de 100 superior
		'UPGRADE_WARNING: Mod has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
		lintRest = ldblNewK Mod 100
		If lintRest > 0 Then
			ldblNewK = ldblNewK + (100 - lintRest)
		End If
		InsurK_AfterSurr = ldblNewK
		
	End Function
	
	'%InsValVA669: Validaciones de detalle de transaccion VA669
	Public Function InsValVA669(ByVal sCodispl As String, ByVal nAction As Integer, ByVal dEffecdate As Date, ByVal nIllustType As Tmp_val669s.eIllustType, ByVal nProjRent As Double, ByVal nAddPrem As Double, ByVal nSurrYear As Integer, ByVal nSurrMonth As Integer, ByVal nSurrAmount As Double, ByVal nTargetVP As Double) As String
		Dim lclsErrors As eFunctions.Errors
		
		On Error GoTo InsValVA669_Err
		lclsErrors = New eFunctions.Errors
		With lclsErrors
			If nProjRent = eRemoteDB.Constants.intNull Then
				.ErrorMessage(sCodispl, 60162)
			End If
			
			If nIllustType = Tmp_val669s.eIllustType.eIllustAddPrem Then
				If nAddPrem = eRemoteDB.Constants.intNull Then
					.ErrorMessage(sCodispl, 60294)
				End If
				
			ElseIf nIllustType = Tmp_val669s.eIllustType.eIllustProjPrem Then 
				If nTargetVP = eRemoteDB.Constants.intNull Then
					.ErrorMessage(sCodispl, 60016)
				End If
			End If
			
			If nSurrYear <> eRemoteDB.Constants.intNull Then
				If nSurrYear < Year(dEffecdate) Then
					.ErrorMessage(sCodispl, 60011)
				End If
				
				If nSurrMonth = eRemoteDB.Constants.intNull Then
					.ErrorMessage(sCodispl, 60012)
				Else
					If nSurrMonth < Month(dEffecdate) Then
						.ErrorMessage(sCodispl, 60013)
					End If
					
					If nSurrMonth < 1 Then
						.ErrorMessage(sCodispl, 60014)
						
					ElseIf nSurrMonth > 12 Then 
						.ErrorMessage(sCodispl, 60014)
					End If
					
					If nSurrAmount = eRemoteDB.Constants.intNull Then
						.ErrorMessage(sCodispl, 60015)
					End If
				End If
			End If
			InsValVA669 = .Confirm
		End With
		
		
InsValVA669_Err: 
		If Err.Number Then
			InsValVA669 = "InsValVA669: " & Err.Description
		End If
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		On Error GoTo 0
	End Function
	
	'%InsValVA669_K: Validaciones de cabecera de transaccion VA669
	Public Function InsValVA669_K(ByVal sCodispl As String, ByVal nAction As Integer, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date) As String
		Dim lclsErrors As eFunctions.Errors
		Dim lclsPolicy As Policy
		Dim lclsCertificat As Certificat
		Dim lblnError As Boolean
		
		On Error GoTo InsValVA669_Err
		lclsErrors = New eFunctions.Errors
		With lclsErrors
			If nBranch = eRemoteDB.Constants.intNull Then
				.ErrorMessage(sCodispl, 1022)
			End If
			
			If nProduct = eRemoteDB.Constants.intNull Then
				.ErrorMessage(sCodispl, 1014)
			End If
			
			If nPolicy = eRemoteDB.Constants.intNull Then
				.ErrorMessage(sCodispl, 3003)
			End If
			
			If dEffecdate = eRemoteDB.Constants.dtmNull Then
				.ErrorMessage(sCodispl, 3404)
			End If
		End With
		
		If nBranch <> eRemoteDB.Constants.intNull And nProduct <> eRemoteDB.Constants.intNull And nPolicy <> eRemoteDB.Constants.intNull Then
			lclsPolicy = New Policy
			With lclsPolicy
				If Not .Find(sCertype, nBranch, nProduct, nPolicy) Then
					lblnError = True
					lclsErrors.ErrorMessage(sCodispl, 3001)
				Else
					If .sStatus_pol <> CStr(Policy.TypeStatus_Pol.cstrIncomplete) And .sStatus_pol <> CStr(Policy.TypeStatus_Pol.cstrInvalid) And .sStatus_pol <> "8" Then
						If .sStatus_pol = CStr(Policy.TypeStatus_Pol.cstrAnnuled) Then
							lblnError = True
							lclsErrors.ErrorMessage(sCodispl, 3098)
						End If
					Else
						lblnError = True
						lclsErrors.ErrorMessage(sCodispl, 3882)
					End If
				End If
			End With
			
			If Not lblnError Then
				If nCertif = eRemoteDB.Constants.intNull Then
					If lclsPolicy.sPolitype <> "1" Then
						lclsErrors.ErrorMessage(sCodispl, 3006)
					End If
				Else
					lclsCertificat = New Certificat
					With lclsCertificat
						If nCertif > 0 Then
							If Not .Find(sCertype, nBranch, nProduct, nPolicy, nCertif) Then
								lclsErrors.ErrorMessage(sCodispl, 3010)
							Else
								'+ Se válida que el certificado sea válido
								If .sStatusva = CStr(Policy.TypeStatus_Pol.cstrIncomplete) Or .sStatusva = CStr(Policy.TypeStatus_Pol.cstrInvalid) Or .sStatusva = "8" Then
									lclsErrors.ErrorMessage(sCodispl, 3883)
								Else
									If .sStatusva = CStr(Policy.TypeStatus_Pol.cstrAnnuled) Then
										lclsErrors.ErrorMessage(sCodispl, 3099)
									End If
								End If
							End If
						End If
					End With
					'UPGRADE_NOTE: Object lclsCertificat may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					lclsCertificat = Nothing
				End If
			End If
			'UPGRADE_NOTE: Object lclsPolicy may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			lclsPolicy = Nothing
		End If
		InsValVA669_K = lclsErrors.Confirm
		
InsValVA669_Err: 
		If Err.Number Then
			InsValVA669_K = "InsValVA669_K: " & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		'UPGRADE_NOTE: Object lclsPolicy may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsPolicy = Nothing
		'UPGRADE_NOTE: Object lclsCertificat may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsCertificat = Nothing
	End Function
	
	'% Class_Initialize: Se inicializan las variables de la clase
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		Call ClearFields()
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'%InsPreVA589: Inicializa los valores de la página VA589
	Public Function InsPreVA589(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nTransaction As Integer, ByVal dStartdate As Date) As Boolean
		Dim lcolModule As Modules
		Dim lclsProduct_li As eProduct.Product
		Dim lclsTab_Activelife As eProduct.Tab_ActiveLife
		Dim lclsCertificat As Certificat
		Dim lclsPer_deposit As Per_deposit
		Dim lclsPay_fracti As Pay_Fracti
		Dim lclsCurren_pol As Curren_pol
		Dim lintCount As Integer
		
		On Error GoTo InsPreVA589_Err
		lcolModule = New ePolicy.Modules
		lclsProduct_li = New eProduct.Product
		lclsCertificat = New Certificat
		lclsPer_deposit = New Per_deposit
		lclsPay_fracti = New Pay_Fracti
		lclsCurren_pol = New Curren_pol
		
		'+ Se obtiene la información de la póliza/certificado
		Call lclsCertificat.Find(sCertype, nBranch, nProduct, nPolicy, nCertif)
		
		'+ Se obtiene la información del producto relacionado con la póliza.
		Call lclsProduct_li.FindProduct_li(nBranch, nProduct, dEffecdate)
		
		'+ Se obtiene los módulos que están asociados a la póliza/certificado
		Call lcolModule.Find(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate)
		
		'+ Se obtiene el factor asociado a frecuencia de pago de la póliza
		Call lclsPay_fracti.Find(nBranch, nProduct, lclsCertificat.nPayfreq, 0, dEffecdate)
		
		'+ Se obtiene la moneda de la póliza
		Call lclsCurren_pol.FindOneOrLocal(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate)
		
		'+ Se lee la información de los datos particulares de vida activa
		Call Find(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate)
		
		With Me
			'+ Valores de los campo siempre fijos.
			.nPremimin = lclsProduct_li.nPremMin
			.nPayfreq = lclsCertificat.nPayfreq
			.nCurrency = lclsCurren_pol.nCurrency
			.nTransactio = nTransaction
			.nRatepayf = lclsPay_fracti.nRatepayf
			.dIssuedat = lclsCertificat.dIssuedat
			.nPremium = lclsCertificat.nPremium
			.dExpirdat = lclsCertificat.dExpirdat
			
			'+ Variables para desactivar controles, deshabilitados por defecto
			.bnTypdurinsDisable = True
			.bnInsurtimeDisable = True
			.bnPremdealDisable = True
			
			.nTypeinvest = .nTypeinvest
			
			If .nIntproject = eRemoteDB.Constants.intNull Then
				.nIntproject = .nIntproject
			End If
			
			If .nWarminint = eRemoteDB.Constants.intNull Then
				.nWarminint = .nWarminint
			End If
			
			If .nTypDurins = eRemoteDB.Constants.intNull Then
				.nTypDurins = lclsProduct_li.nTypDurins
			End If
			
			If .nModulec = eRemoteDB.Constants.intNull Or .nModulec <> lcolModule.nModulec Or (.nModulec = lcolModule.nModulec And (nTransaction = Constantes.PolTransac.clngPolicyAmendment Or nTransaction = Constantes.PolTransac.clngTempPolicyAmendment Or nTransaction = Constantes.PolTransac.clngCertifAmendment Or nTransaction = Constantes.PolTransac.clngTempCertifAmendment Or nTransaction = Constantes.PolTransac.clngPolicyQuotAmendent Or nTransaction = Constantes.PolTransac.clngCertifQuotAmendent) Or nTransaction = Constantes.PolTransac.clngPolicyPropAmendent Or nTransaction = Constantes.PolTransac.clngCertifPropAmendent) Then
				.nModulec = IIf(lcolModule.nModulec = eRemoteDB.Constants.intNull, 0, lcolModule.nModulec)
				.nCapitaldeath = eRemoteDB.Constants.intNull
			End If
			
			.nPremiumbas = IIf(.nPremiumbas = eRemoteDB.Constants.intNull, 0, .nPremiumbas)
			.nPremium = IIf(.nPremium = eRemoteDB.Constants.intNull, 0, .nPremium)
			.nVPprdeal = IIf(.nVPprdeal = eRemoteDB.Constants.intNull, 0, .nVPprdeal)
			
			'+Se obtiene el capital asegurado en caso de muerte
			If .nCapitaldeath = eRemoteDB.Constants.intNull Then
				lclsTab_Activelife = New eProduct.Tab_ActiveLife
				If lclsTab_Activelife.Find(nBranch, nProduct, .nModulec, 0, dEffecdate) Then
					'+ Se obtiene el tipo de indemnización asociado al módule en tratamiento
					.nOption = lclsTab_Activelife.nOption
				End If
				
				Select Case nTransaction
					Case Constantes.PolTransac.clngPolicyIssue, Constantes.PolTransac.clngCertifIssue, Constantes.PolTransac.clngRecuperation, Constantes.PolTransac.clngPolicyQuotation, Constantes.PolTransac.clngCertifQuotation, Constantes.PolTransac.clngPolicyReissue, Constantes.PolTransac.clngCertifReissue, Constantes.PolTransac.clngPolicyProposal, Constantes.PolTransac.clngCertifProposal
						.nCapitaldeath = lclsTab_Activelife.nCapmin
						
					Case Constantes.PolTransac.clngPolicyAmendment, Constantes.PolTransac.clngTempPolicyAmendment, Constantes.PolTransac.clngCertifAmendment, Constantes.PolTransac.clngTempCertifAmendment, Constantes.PolTransac.clngPolicyQuotAmendent, Constantes.PolTransac.clngCertifQuotAmendent, Constantes.PolTransac.clngPolicyPropAmendent, Constantes.PolTransac.clngCertifPropAmendent
						.nCapitaldeath = InsCalfallVA(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nTransaction)
				End Select
			Else
				Select Case nTransaction
					Case Constantes.PolTransac.clngPolicyQuery, Constantes.PolTransac.clngCertifQuery, Constantes.PolTransac.clngQuotationQuery, Constantes.PolTransac.clngProposalQuery
						.nPremfreq = .nPremdeal * .nRatepayf
				End Select
			End If
			
			'+ Si el tipo de duracion es fija.
			If lclsProduct_li.sIdurvari = "2" Then
				If .nInsurtime = eRemoteDB.Constants.intNull Then
					.nInsurtime = lclsProduct_li.nIdurafix
				End If
			Else
				'+ Duración variable
				Select Case .nTypDurins
					'+ Si el tipo de duracion es abierta.
					Case IduraindType.nTypdurinsOpen
						.nInsurtime = eRemoteDB.Constants.intNull
						.dExpirdat = eRemoteDB.Constants.dtmNull
						
						'+ Si el tipo de duracion es libre.
					Case IduraindType.nTypdurinsFree
						.bnTypdurinsDisable = False
						.bnInsurtimeDisable = False
						
					Case Else
						.bnInsurtimeDisable = False
				End Select
			End If
			
			'+ Se verifica cuantos registros hay en a tabla de Plan de pago para los aportes (Per_deposit)
			lintCount = lclsPer_deposit.Count(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate)
			.bnPremdealDisable = Not (lintCount = 0 Or lintCount = 1)
			
		End With
		
InsPreVA589_Err: 
		If Err.Number Then
			InsPreVA589 = False
		End If
		'UPGRADE_NOTE: Object lcolModule may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lcolModule = Nothing
		'UPGRADE_NOTE: Object lclsProduct_li may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsProduct_li = Nothing
		'UPGRADE_NOTE: Object lclsTab_Activelife may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsTab_Activelife = Nothing
		'UPGRADE_NOTE: Object lclsCertificat may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsCertificat = Nothing
		'UPGRADE_NOTE: Object lclsPer_deposit may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsPer_deposit = Nothing
		'UPGRADE_NOTE: Object lclsPay_fracti may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsPay_fracti = Nothing
		'UPGRADE_NOTE: Object lclsCurren_pol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsCurren_pol = Nothing
		On Error GoTo 0
	End Function
	
	'%InsCalfallVA: Calcular el monto de la suma asegurada por fallecimiento
	Public Function InsCalfallVA(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nTransaction As Integer) As Integer
		Dim lrereaActivelife As eRemoteDB.Execute
		
		On Error GoTo InsCalfallVA_Err
		'+Definición de parámetros para stored procedure 'InsCalfallVA'
		lrereaActivelife = New eRemoteDB.Execute
		With lrereaActivelife
			.StoredProcedure = "InsCalfallVA"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTransactio", nTransaction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCapitaldeath", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				InsCalfallVA = .Parameters("nCapitaldeath").Value
			End If
		End With
		
InsCalfallVA_Err: 
		If Err.Number Then
			InsCalfallVA = -1
		End If
		'UPGRADE_NOTE: Object lrereaActivelife may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrereaActivelife = Nothing
		On Error GoTo 0
	End Function
	
	'%InsValVA589: Valida los campo de la transaccion "VA589"
	Public Function InsValVA589(ByVal sCodispl As String, ByVal nTransactio As Integer, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nTypDurins As Integer, ByVal nInsurtime As Integer, ByVal nCapitaldeath As Double, ByVal nPremdeal As Double, ByVal nCalPrem As Double, ByVal nTypeinvest As Integer, ByVal nIntproject As Double, ByVal nWarminint As Double, ByVal nAgreement As Integer, ByVal nModulec As Integer, ByVal nPremMin As Double, ByVal sLevelint As String) As String
		Dim lobjErrors As eFunctions.Errors
		Dim lclsTab_Activelife As eProduct.Tab_ActiveLife
		
		On Error GoTo InsValVA589_Err
		lobjErrors = New eFunctions.Errors
		lclsTab_Activelife = New eProduct.Tab_ActiveLife
		nModulec = IIf(nModulec = eRemoteDB.Constants.intNull, 0, nModulec)
		
		'+ Se obtiene el plan asociado a la póliza/certificado.
		Call lclsTab_Activelife.Find(nBranch, nProduct, nModulec, 0, dEffecdate)
		
		'+ Tipo de duración: Debe estar lleno
		With lobjErrors
			If nTypDurins = eRemoteDB.Constants.intNull Then
				.ErrorMessage(sCodispl, 60167)
			End If
			
			'+ Duración del seguro: Si el tipo de duración es diferente a "abierta", debe se mayor a cero
			If nTypDurins <> IduraindType.nTypdurinsOpen And nInsurtime <= 0 Then
				.ErrorMessage(sCodispl, 60168)
			End If
			
			'+ Suma asegurada: Debe ser mayor a cero
			If nCapitaldeath <= 0 Then
				.ErrorMessage(sCodispl, 60169)
			End If
			
			'+ Suma asegurada: Debe ser mayor o igual al capital mínimo definido para el plan/módulo asociado a la póliza
			If nCapitaldeath < lclsTab_Activelife.nCapmin Then
				.ErrorMessage(sCodispl, 60170)
			End If
			
			'+ Prima según frecuencia de pago: Si este campo está lleno, debe ser mayor o igual a la
			'+ prima mínima definida para el producto
			If nPremdeal > 0 Then
				If nCalPrem > 0 And nCalPrem < nPremMin Then
					.ErrorMessage(sCodispl, 60172)
				End If
			End If
			'+ Modalidad de inversión: Debe estar lleno
			If nTypeinvest = eRemoteDB.Constants.intNull Then
				.ErrorMessage(sCodispl, 60173)
			End If
			
			'+ Modalidad de inversión: Si la transacción en ejecución es modificación,
			'+ o cotización/propuesta de modificación, la cantidad de cambios de modalidad
			'+ de inversión no puede ser mayor a la definida para el plan asociado a la póliza.
			If nTransactio = Constantes.PolTransac.clngPolicyAmendment Or nTransactio = Constantes.PolTransac.clngTempPolicyAmendment Or nTransactio = Constantes.PolTransac.clngCertifAmendment Or nTransactio = Constantes.PolTransac.clngTempCertifAmendment Or nTransactio = Constantes.PolTransac.clngPolicyQuotAmendent Or nTransactio = Constantes.PolTransac.clngCertifQuotAmendent Or nTransactio = Constantes.PolTransac.clngPolicyPropAmendent Or nTransactio = Constantes.PolTransac.clngCertifPropAmendent Then
				If lclsTab_Activelife.nMchainves < ReaActivelife_count(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate) Then
					.ErrorMessage(sCodispl, 60174)
				End If
			End If
			
			'+ % rentabilidad proyectada: Debe estar lleno
			If nIntproject = eRemoteDB.Constants.intNull Then
				.ErrorMessage(sCodispl, 60162)
			End If
			
			'+ % rentabilidad garantizada: Debe estar lleno
			If nTypeinvest <> 2 Then
				If nWarminint = eRemoteDB.Constants.intNull Then
					.ErrorMessage(sCodispl, 60163)
				End If
			End If
			
			'+ Convenio: Si esta lleno, debe estar registrado en la tabla de convenios de VidActiva
			If nAgreement <> eRemoteDB.Constants.intNull Then
				'+ Verifica el Indicador de nivel de asociados del convenio para los intermediarios
				Select Case sLevelint
                    Case CStr(eBranches.Agreement_al.LevelIntType.levelIntByIntermType)
                        '+ Por tipo de intermediario: debe estar asociado al convenio
                        If Not InsValCommission_Int_typ_agre(sCertype, nBranch, nProduct, nPolicy, dEffecdate, nAgreement) Then
                            .ErrorMessage(sCodispl, 60176)
                        End If
                    Case CStr(eBranches.Agreement_al.LevelIntType.levelIntByInterm)
                        '+ Por intermediario: debe estar asociado al convenio
                        If Not InsValCommission_Interm_agre(sCertype, nBranch, nProduct, nPolicy, dEffecdate, nAgreement) Then
                            .ErrorMessage(sCodispl, 60177)
                        End If
                End Select
			End If
			
			InsValVA589 = .Confirm
		End With
		
InsValVA589_Err: 
		If Err.Number Then
			InsValVA589 = "InsValVA589: " & Err.Description
		End If
		'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjErrors = Nothing
		'UPGRADE_NOTE: Object lclsTab_Activelife may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsTab_Activelife = Nothing
		On Error GoTo 0
	End Function
	
	'%ReaActivelife_count: Retorna la cantidad de endosos realizados sobre la tabla "activelife".
	Public Function ReaActivelife_count(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date) As Integer
		Dim lrereaActivelife As eRemoteDB.Execute
		
		On Error GoTo Find_Err
		'+Definición de parámetros para stored procedure 'ReaActivelife_count'
		lrereaActivelife = New eRemoteDB.Execute
		With lrereaActivelife
			.StoredProcedure = "ReaActivelife_count"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCount", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 12, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				ReaActivelife_count = .Parameters("nCount").Value
			End If
		End With
		
		'UPGRADE_NOTE: Object lrereaActivelife may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrereaActivelife = Nothing
		
Find_Err: 
		If Err.Number Then
			ReaActivelife_count = -1
		End If
		On Error GoTo 0
	End Function
	
	'%InsValCommission_Interm_agre: Valida que los intermediarios asociados a la póliza, estén
	'%                              permitidos en el convenio
	Public Function InsValCommission_Interm_agre(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal dEffecdate As Date, ByVal nAgreement As Integer) As Boolean
		Dim lrereaActivelife As eRemoteDB.Execute
		
		On Error GoTo Find_Err
		'+Definición de parámetros para stored procedure 'InsValCommission_Interm_agre'
		lrereaActivelife = New eRemoteDB.Execute
		With lrereaActivelife
			.StoredProcedure = "InsValCommission_Interm_agre"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAgreement", nAgreement, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCount", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 1, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				InsValCommission_Interm_agre = .Parameters("nCount").Value = 0
			End If
		End With
		
Find_Err: 
		If Err.Number Then
			InsValCommission_Interm_agre = False
		End If
		'UPGRADE_NOTE: Object lrereaActivelife may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrereaActivelife = Nothing
		On Error GoTo 0
	End Function
	
	'% InsValCommission_Int_typ_agre: Valida que los tipos de intermediarios asociados a la póliza,
	'%                               estén permitidos en el convenio
	Public Function InsValCommission_Int_typ_agre(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal dEffecdate As Date, ByVal nAgreement As Integer) As Boolean
		Dim lrereaActivelife As eRemoteDB.Execute
		
		On Error GoTo InsValCommission_Int_typ_agre_Err
		
		'+Definición de parámetros para stored procedure 'InsValCommission_Int_typ_agre'
		lrereaActivelife = New eRemoteDB.Execute
		With lrereaActivelife
			.StoredProcedure = "InsValCommission_Int_typ_agre"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAgreement", nAgreement, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCount", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 1, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				InsValCommission_Int_typ_agre = .Parameters("nCount").Value = 0
			End If
		End With
		
InsValCommission_Int_typ_agre_Err: 
		If Err.Number Then
			InsValCommission_Int_typ_agre = False
		End If
		'UPGRADE_NOTE: Object lrereaActivelife may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrereaActivelife = Nothing
		On Error GoTo 0
	End Function
	
	'% InsPostVA589: cambios necesarios para finalizar la transaccion VA589
	Public Function InsPostVA589(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nCapitaldeath As Double, ByVal nTypDurins As Integer, ByVal nInsurtime As Integer, ByVal nIntproject As Double, ByVal nWarminint As Double, ByVal nOption As Integer, ByVal nTypeinvest As Integer, ByVal nPremdeal As Double, ByVal nTransactio As Integer, ByVal nUsercode As Integer, ByVal nAgreement As Integer, ByVal nModulec As Integer, ByVal nPremiumbas As Double, ByVal nPremium As Double, ByVal nVPprdeal As Double, ByVal dStartdate As Date, ByVal dExpirdat As Date, ByVal sPolitype As String, ByVal sBrancht As String, ByVal dIssuedat As Date, ByVal nCapitaldeath_old As Double, ByVal nPremimin As Double, ByVal nCapital As Double, ByVal nPrsugest As Double, ByVal nVPprsug As Double) As Boolean
		Dim lclsPolicy_Win As Policy_Win
		Dim lclsRoles As Roles
		Dim lblnLife As Boolean
		
		On Error GoTo InsPostVA589_Err
		lclsRoles = New Roles
		'+ verifica que el campo suma asegura haya cambiado
		With Me
			Call lclsRoles.InsGetClientHolder(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate)
			
			.sClient = lclsRoles.sClient
			.sCertype = sCertype
			.nBranch = nBranch
			.nProduct = nProduct
			.nPolicy = nPolicy
			.nCertif = nCertif
			.dEffecdate = dEffecdate
			.nCapitaldeath = nCapitaldeath
			.nTypDurins = nTypDurins
			.nInsurtime = nInsurtime
			.nIntproject = nIntproject
			.nWarminint = nWarminint
			.nOption = nOption
			.nTypeinvest = nTypeinvest
			.nPremdeal = nPremdeal
			.nTransactio = nTransactio
			.nUsercode = nUsercode
			.nAgreement = nAgreement
			.nPremiumbas = nPremiumbas
			.nPremium = nPremium
			.nVPprdeal = nVPprdeal
			.dStartdate = dStartdate
			.dExpirdat = dExpirdat
			.dIssuedat = dIssuedat
			.nModulec = nModulec
			.nPremimin = nPremimin
			.nCapital = nCapital
			.nPrsugest = nPrsugest
			.nVPprsug = nVPprsug
			
		End With
		
		If Update Then
			InsPostVA589 = True
			lclsPolicy_Win = New Policy_Win
			With lclsPolicy_Win
				
				'+ Deja estado de transacción con contenido.
				.Add_PolicyWin(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nUsercode, "VA589", "2")
				
				'+ Deja estado de otras transacciones como "Requerida y sin información"
				'+ Solo cuando cambia la suma asegurada
				If nCapitaldeath <> nCapitaldeath_old Then
					If (sBrancht = CStr(eProduct.Product.pmBrancht.pmlife) Or sBrancht = CStr(eProduct.Product.pmBrancht.pmNotTraditionalLife)) And (sPolitype = "1" Or (sPolitype = "2" And nCertif > 0)) Then
						lblnLife = True
					Else
						lblnLife = False
					End If
					.Add_PolicyWin(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nUsercode, "CA014", "3",  ,  , lblnLife, False)
					
					.Add_PolicyWin(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nUsercode, "VI021", "3",  ,  ,  , False)
				End If
			End With
		End If
		
InsPostVA589_Err: 
		If Err.Number Then
			InsPostVA589 = False
		End If
		'UPGRADE_NOTE: Object lclsPolicy_Win may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsPolicy_Win = Nothing
		'UPGRADE_NOTE: Object lclsRoles may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsRoles = Nothing
		On Error GoTo 0
	End Function
	
	'% Update: actualiza la tabla "activelife"
	Public Function Update() As Boolean
		Dim lrecinsupdactivelife As eRemoteDB.Execute
		
		On Error GoTo Update_Err
		'+Definición de parámetros para stored procedure 'InsUpdActivelife'
		'+Información leída el 29/11/2001
		lrecinsupdactivelife = New eRemoteDB.Execute
		With lrecinsupdactivelife
			.StoredProcedure = "InsUpdActivelife"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCapitaldeath", nCapitaldeath, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTypdurins", nTypDurins, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInsurtime", nInsurtime, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPremiumbas", nPremiumbas, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nSald_amount", nSald_amount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sSald_prog", sSald_prog, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nIntproject", nIntproject, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nWarminint", nWarminint, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dProg_date", dProg_date, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOption", nOption, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTypeinvest", nTypeinvest, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPremium", nPremium, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPremimin", nPremimin, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCapital", nCapital, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPremdeal", nPremdeal, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dStartdate", dStartdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dExpirdat", dExpirdat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dIssuedat", dIssuedat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTransactio", nTransactio, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nNullcode", nNullcode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nGroup", nGroup, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nSituation", nSituation, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPrsugest", nPrsugest, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAgreement", nAgreement, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nVpprsug", nVPprsug, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nVpprdeal", nVPprdeal, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sInscalpre", sInsCalPre, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Update = .Run(False)
		End With
		
Update_Err: 
		If Err.Number Then
			Update = False
		End If
		'UPGRADE_NOTE: Object lrecinsupdactivelife may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsupdactivelife = Nothing
		On Error GoTo 0
	End Function
	
	'% Update_Premin: Actualiza la prima mínima según la suma de las primas mínimas de cada
	'%                asegurado/cobertura/capa
	Public Function Update_Premin(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nUsercode As Integer, Optional ByVal nPremimin As Double = eRemoteDB.Constants.intNull) As Boolean
		Dim lrecActivelife As eRemoteDB.Execute
		
		On Error GoTo Update_Premin_Err
		'+Definición de parámetros para stored procedure 'InsUpdpremin_Activelife'
		'+Información leída el 15/01/2002
		lrecActivelife = New eRemoteDB.Execute
		With lrecActivelife
			.StoredProcedure = "InsUpdpremin_Activelife"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPremimin", nPremimin, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCommit", 1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Update_Premin = .Run(False)
		End With
		
Update_Premin_Err: 
		If Err.Number Then
			Update_Premin = False
		End If
		'UPGRADE_NOTE: Object lrecActivelife may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecActivelife = Nothing
		On Error GoTo 0
	End Function
	
	'%InsPreVA595: Obtiene los datos de la VA595
	Public Function InsPreVA595(ByVal sReload As String, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nPpo As Double, ByVal nUsercode As Integer, Optional ByVal nCurrency As Integer = 0, Optional ByVal nPremiumbas As Double = 0, Optional ByVal nPremimin As Double = 0, Optional ByVal nVPprdeal As Double = 0, Optional ByVal nPremdealfreq As Double = 0, Optional ByVal nPremdeal As Double = 0, Optional ByVal nPrsugest As Double = 0, Optional ByVal nVPprsug As Double = 0, Optional ByVal nAmountcontr As Double = 0, Optional ByVal nIntproject As Double = 0, Optional ByVal nWarminint As Double = 0, Optional ByVal sInsCalPre As String = "", Optional ByVal nRatepayf As Double = 0, Optional ByVal nInsurtime As Integer = 0) As Boolean
		On Error GoTo InsPreVA595_Err
		If sReload = String.Empty Then
			InsPreVA595 = InsGetDataVA595(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nPpo, nUsercode)
		Else
			InsPreVA595 = True
			Me.nCurrency = nCurrency
			Me.nPremiumbas = nPremiumbas
			Me.nPremimin = nPremimin
			Me.nVPprdeal = nVPprdeal
			Me.nPremfreq = nPremdealfreq
			Me.nPremdeal = nPremdeal
			Me.nPrsugest = nPrsugest
			Me.nVPprsug = nVPprsug
			Me.nAmountcontr = nAmountcontr
			Me.nIntproject = nIntproject
			Me.nWarminint = nWarminint
			Me.sInsCalPre = sInsCalPre
			Me.nRatepayf = nRatepayf
			Me.nInsurtime = nInsurtime
		End If
		
InsPreVA595_Err: 
		If Err.Number Then
			InsPreVA595 = False
		End If
		On Error GoTo 0
	End Function
	
	'%InsGetDataVA595: Obtiene los datos de la VA595
	Private Function InsGetDataVA595(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nPpo As Double, ByVal nUsercode As Integer) As Boolean
		Dim lrecInsGetactivedata As eRemoteDB.Execute
		
		On Error GoTo InsGetDataVA595_Err
		'+ Definición de store procedure InsGetactivedata al 04-08-2002 16:09:32
		lrecInsGetactivedata = New eRemoteDB.Execute
		With lrecInsGetactivedata
			.StoredProcedure = "InsGetDataVA595"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPpo", nPpo, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				InsGetDataVA595 = True
				Me.nCurrency = .FieldToClass("nCurrency")
				Me.nPremiumbas = .FieldToClass("nPremiumbas")
				Me.nPremimin = .FieldToClass("nPremimin")
				Me.nVPprdeal = .FieldToClass("nVpprdeal")
				Me.nPremfreq = .FieldToClass("nPremdealfreq")
				Me.nPremdeal = .FieldToClass("nPremdeal")
				Me.nPrsugest = .FieldToClass("nPrsugest")
				Me.nVPprsug = .FieldToClass("nVpprsug")
				Me.nAmountcontr = .FieldToClass("nAmountcontr")
				Me.nIntproject = .FieldToClass("nIntproject")
				Me.nWarminint = .FieldToClass("nWarminint")
				Me.sInsCalPre = .FieldToClass("sInscalpre")
				Me.nRatepayf = .FieldToClass("nRatepayf")
				Me.nInsurtime = .FieldToClass("nInsurtime")
			End If
		End With
		
InsGetDataVA595_Err: 
		If Err.Number Then
			InsGetDataVA595 = False
		End If
		'UPGRADE_NOTE: Object lrecInsGetactivedata may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecInsGetactivedata = Nothing
		On Error GoTo 0
	End Function
End Class






