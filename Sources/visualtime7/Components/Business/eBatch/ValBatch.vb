Option Strict Off
Option Explicit On
Option Compare Text
Public Class ValBatch
	'%-------------------------------------------------------%'
	'% $Workfile:: ValBatch.cls                             $%'
	'% $Author:: Nvaplat15                                  $%'
	'% $Date:: 16/11/04 15.55                               $%'
	'% $Revision:: 35                                       $%'
	'%-------------------------------------------------------%'
	
	Public nCount As Integer
	Public sKey As String
	
	'- Constantes para el número posible de frames en la secuencia de Carga Masiva.
	Private Const CN_FRAMESCHARGE As Integer = 2
	
	'- Se define la constante para los codispl en la secuencia de Carga Masiva.
	Private Const CN_WINDOWSCHARGE As String = "CAL659  CAL660  "
	
	'-Mensaje de error del proceso
	Public sError As String
	
	Public Enum MassiveChargeActions
		MassChargCertificat = 1 '+Carga masiva de certificados
		MassChargExclutions = 2 '+Exclusión de asegurado/certificado
		MassChargChange = 3 '+Reemplazo de nómina
		MassChargClient = 4 '+Carga masiva de clientes
		MassChargTemp = 5 '+Carga de nomina temporal
		MassCalcTempList = 6 '+Calculo Nómina Temporal Retroactiva
		MassDelTempList = 7 '+Elimna Nómina Temporal Retroactiva
		MassPrintList = 8 '+Impresión de Nómina
        MassChargePolicy = 11 '+Carga masiva de pólizas
        MassChargExclutionsPolicy = 12 '+Anulación de pólizas
        MassChargePolicyCollect = 20 '+Carga masiva de polizas colectivas
        MassChargePolicyMulti = 25 '+Carga masiva multipolizas
	End Enum
	' Propiedades que se eliminaran posteriormente
	
	
	
	'%insValCA051_K: Realiza la validación del header de la ventana CA051 'Hojas para la carga
	'% de póliza/certificado
	Public Function insValCA051_K(ByVal nAction As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal sFile As String, ByVal sDescript As String, ByVal sList As String, ByVal nId As Integer) As String
		Dim lerrTime As eFunctions.Errors
		Dim lclsProduct As eProduct.Product
		Dim lclsPolicy As ePolicy.Policy
		Dim lblnValprod As Boolean
		Dim lblnValpolexist As Boolean
		Dim lclsWorksheet As Worksheet
		
		On Error GoTo insValCA051_k_Err
		
		lerrTime = New eFunctions.Errors
		lclsWorksheet = New Worksheet
		
		If nAction = eFunctions.Menues.TypeActions.clngActionQuery Then
			If Not lclsWorksheet.FindWorksheet(nId) Then
				lerrTime.ErrorMessage("CA051", 1073)
			End If
		Else
			If lclsWorksheet.FindWorksheet(nId) Then
				lerrTime.ErrorMessage("CA051", 1074)
			End If
			
			
			'+ Validación del producto
			
			If nProduct <> eRemoteDB.Constants.intNull Then
				If nBranch = eRemoteDB.Constants.intNull Then
					lerrTime.ErrorMessage("CA051", 9064)
				Else
					'+ Validación del ramo
					lclsProduct = New eProduct.Product
					If Not lclsProduct.Find(nBranch, nProduct, Today) Then
						lerrTime.ErrorMessage("CA051", 9066)
					Else
						lblnValprod = True
					End If
				End If
			End If
			
			'+ Validación de la póliza
			
			If nPolicy <> eRemoteDB.Constants.intNull Then
				lclsPolicy = New ePolicy.Policy
				
				'+ Debe existir
				
				If Not lclsPolicy.Find("2", nBranch, nProduct, nPolicy) Then
					lerrTime.ErrorMessage("CA051", 3001)
				Else
					
					'+ Debe ser de tipo colectiva
					
					If lclsPolicy.sPolitype <> "2" Then
						lerrTime.ErrorMessage("CA051", 38016)
					Else
						
						'+  No debe estar anulada
						
						If lclsPolicy.dNulldate <> eRemoteDB.Constants.dtmNull Then
							lerrTime.ErrorMessage("CA051", 3098)
						Else
							
							'+ Debe corresponder a una póliza válida
							
							If lclsPolicy.sStatus_pol = CStr(ePolicy.Policy.TypeStatus_Pol.cstrIncomplete) Or lclsPolicy.sStatus_pol = CStr(ePolicy.Policy.TypeStatus_Pol.cstrInvalid) Then
								lerrTime.ErrorMessage("CA051", 3720)
							Else
								lblnValpolexist = True
							End If
						End If
					End If
				End If
			End If
			
			'+ Validación del Archivo
			
			If sFile = String.Empty And sList = "1" Then
				lerrTime.ErrorMessage("CA051", 38023)
			End If
			
			'+ Validación de la Descripción
			
			If sDescript = String.Empty Then
				lerrTime.ErrorMessage("CA051", 10010)
			End If
		End If
		
		insValCA051_K = lerrTime.Confirm
		
insValCA051_k_Err: 
		If Err.Number Then insValCA051_K = insValCA051_K & Err.Description
		On Error GoTo 0
		'UPGRADE_NOTE: Object lerrTime may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lerrTime = Nothing
		'UPGRADE_NOTE: Object lclsProduct may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsProduct = Nothing
		'UPGRADE_NOTE: Object lclsPolicy may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsPolicy = Nothing
		'UPGRADE_NOTE: Object lclsWorksheet may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsWorksheet = Nothing
	End Function
	
	'%insPostCA051_K: Crea en la tabla Worksheet
	Public Function insPostCA051_K(ByVal nAction As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal sDescript As String, ByVal nUsercode As Integer, ByVal sValuesList As String, ByVal nId As Integer) As Boolean
		Dim lclsWorksheet As Worksheet
		
		On Error GoTo insPostCA051_k_Err
		
		If nAction <> eFunctions.Menues.TypeActions.clngActionQuery Then
			
			lclsWorksheet = New Worksheet
			
			With lclsWorksheet
				If .FindWorksheet(nId) Then
					.nBranch = nBranch
					.nProduct = nProduct
					.nPolicy = IIf(nPolicy = eRemoteDB.Constants.intNull, 0, nPolicy)
					.nId = nId
					.sDescript = sDescript
					.nUsercode = nUsercode
					.sValuesList = sValuesList
					insPostCA051_K = .Update
				Else
					.nBranch = nBranch
					.nProduct = nProduct
					.nPolicy = IIf(nPolicy = eRemoteDB.Constants.intNull, 0, nPolicy)
					.nId = nId
					.sDescript = sDescript
					.nUsercode = nUsercode
					.sValuesList = sValuesList
					insPostCA051_K = .Add
				End If
				
			End With
		End If
		
insPostCA051_k_Err: 
		If Err.Number Then insPostCA051_K = False
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsWorksheet may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsWorksheet = Nothing
	End Function
	
	'%insValCA051Upd: Realiza la validación del Registro seleccionado en el Grid de la ventana CA051 'Hojas para la carga
	'% de póliza/certificado
	Public Function insValCA051Upd(ByVal nAction As Integer, ByVal nId As Integer, ByVal nIdRec As Integer, ByVal sColumnName As String, ByVal nOrder As Integer, ByVal sRequire As String, ByVal sSelected As String, ByVal nUsercode As Integer, ByVal nSheet As Integer, ByVal sField As String, ByVal sFieldValidate As String) As String
		
		Dim lclssheets As Colsheet
		Dim lerrTime As eFunctions.Errors
		Dim lcolsheets As Colsheets
		Dim lintCritery As Integer
		Dim lblnError As Boolean
		
		On Error GoTo insValCA051Upd_Err
		
		lerrTime = New eFunctions.Errors
		lcolsheets = New Colsheets
		lclssheets = New Colsheet
		
		lblnError = False
		
		'+ Validación del Orden
		
		If nOrder = eRemoteDB.Constants.intNull Or nOrder = 0 Then
			
			lerrTime.ErrorMessage("CA051", 10900)
		Else
			If lcolsheets.FindCA051(nId) Then
				lintCritery = 0
				For	Each lclssheets In lcolsheets
					If sField = "NITEM" And sFieldValidate = "1" Then
						lblnError = True
					End If
					If (lclssheets.nOrder = nOrder And lclssheets.nSheet = nSheet And lclssheets.sSel = "1" And lclssheets.nIdRec <> nIdRec) Then
						lerrTime.ErrorMessage("CA051", 10902)
					End If
					If lclssheets.nIdRec <> nIdRec And lclssheets.sColumnName = sColumnName Then
						lerrTime.ErrorMessage("CA051", 10931)
					End If
					If lclssheets.sSelected = "1" Then
						lintCritery = lintCritery + 1
					End If
				Next lclssheets
				If sSelected = "1" And lintCritery > 2 Then
					lerrTime.ErrorMessage("CA051", 55711)
				End If
			End If
		End If
		
		If lblnError Then
			lerrTime.ErrorMessage("CA051", 60462)
		End If
		
		insValCA051Upd = lerrTime.Confirm
		
insValCA051Upd_Err: 
		If Err.Number Then insValCA051Upd = insValCA051Upd & Err.Description
		On Error GoTo 0
		'UPGRADE_NOTE: Object lerrTime may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lerrTime = Nothing
		'UPGRADE_NOTE: Object lcolsheets may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lcolsheets = Nothing
		'UPGRADE_NOTE: Object lclssheets may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclssheets = Nothing
	End Function
	
	
	'%insValCA051: Realiza la validación de Seleccionado algun Registro en el Grid de la ventana CA051 'Hojas para la carga
	'% de póliza/certificado
	Public Function insValCA051(ByVal nCount As Integer, ByVal nCountCritery As Integer, Optional ByVal nId As Integer = 0) As String
		
		Dim lerrTime As eFunctions.Errors
		Dim lintCount As Integer
		On Error GoTo insValCA051_Err
		
		lerrTime = New eFunctions.Errors
		
		If nCount = 0 Or nCount = eRemoteDB.Constants.intNull Then
			lerrTime.ErrorMessage("CA051", 10903)
		End If
		
		If nCountCritery = 0 Or nCountCritery = eRemoteDB.Constants.intNull Then
			lerrTime.ErrorMessage("CA051", 55820)
		Else
			lintCount = FindCount_critery(nId)
			If lintCount > 0 Then
				If lintCount = 1 Then
					lerrTime.ErrorMessage("CA051", 60571)
				Else
					lerrTime.ErrorMessage("CA051", 60572)
				End If
			End If
		End If
		
		insValCA051 = lerrTime.Confirm
		
		'UPGRADE_NOTE: Object lerrTime may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lerrTime = Nothing
		
insValCA051_Err: 
		If Err.Number Then insValCA051 = insValCA051 & Err.Description
		On Error GoTo 0
	End Function

    '%insPostCA051: Actualiza los registros en Colsheet
    Public Function insPostCA051(ByVal sSel As String, ByVal nId As Integer, ByVal nIdRec As Integer, ByVal sWindowType As String, ByVal sColumnName As String, ByVal nOrder As Integer, ByVal sRequire As String, ByVal nUsercode As Integer, ByVal sSelected As String, ByVal sDefaultValue As String) As Boolean

        Dim lclsColsheet As Colsheet

        On Error GoTo insPostCA051_k_Err

        lclsColsheet = New Colsheet

        With lclsColsheet
            .nId = nId
            .nIdRec = nIdRec
            .sColumnName = sColumnName
            .nOrder = nOrder
            .sRequire = sRequire
            .nUsercode = nUsercode
            .sSelected = sSelected
            .sDefaultValue = sDefaultValue

            If sWindowType = "PopUp" Then
                insPostCA051 = .Add
            Else
                If sSel = "1" Then
                    insPostCA051 = .Add
                Else
                    insPostCA051 = .Delete
                End If
            End If

        End With

insPostCA051_k_Err:
        If Err.Number Then
            insPostCA051 = False
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lclsColsheet may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsColsheet = Nothing
    End Function

    Public Function insPostCAL659(ByVal sKey As String, ByVal sAction As String, Optional ByVal sRowValues As String = "", Optional ByVal nId_Table As Integer = 0, Optional ByVal nRow As Integer = 0) As Boolean

        Dim lrecInsPostCAL659 As eRemoteDB.Execute

        On Error GoTo insPostCAL659_Err

        lrecInsPostCAL659 = New eRemoteDB.Execute

        With lrecInsPostCAL659
            .StoredProcedure = "insPostCAL659"
            .Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sAction", sAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sRowsValue", sRowValues, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 4000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nId_Table", nId_Table, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nRow", nRow, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            insPostCAL659 = .Run(False)
            If insPostCAL659 Then
                sKey = .Parameters("sKey").Value
            End If
        End With

insPostCAL659_Err:
        If Err.Number Then
            insPostCAL659 = False
        End If
        'UPGRADE_NOTE: Object lrecInsPostCAL659 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecInsPostCAL659 = Nothing
        On Error GoTo 0
    End Function

    '% insValCAL013_k: Realiza la validación de los campos a actualizar en la ventana CAL013.
    '  (Carga Masiva de Colectivos)
    Public Function insValCAL013_k(ByVal sCodispl As String,
                                   ByVal sCertype As String,
                                   ByVal nBranch As Integer,
                                   ByVal nProduct As Integer,
                                   ByVal nPolicy As Double,
                                   ByVal nWorksheet As Integer,
                                   ByVal sFile As String,
                                   ByVal nAction As Integer,
                                   ByVal dEffecdate As Date,
                                   ByVal sProc_massive As String,
                                   ByVal sReinsuran As String,
                                   ByVal sManual As String,
                                   ByVal sUseFile As String) As String

        Dim lobjValues As eFunctions.Values
        Dim lobjErrors As eFunctions.Errors
        Dim lclsPolicy As ePolicy.Policy
        Dim lcolColSheet As Colsheets
        Dim lclsColsheet As Colsheet
        Dim lobjProduct As eProduct.Product
        Dim lstrFieldSearch As String
        Dim lstrCritery As String = ""
        Dim lintCount As Integer
        Dim lintCount_critery As Integer

        lobjValues = New eFunctions.Values
        lobjErrors = New eFunctions.Errors
        lclsPolicy = New ePolicy.Policy
        lcolColSheet = New Colsheets
        lobjProduct = New eProduct.Product

        On Error GoTo insValCAL013_k_Err

        '+ Se valida el campo Ramo

        If nAction <> MassiveChargeActions.MassChargClient And
           nAction <> MassiveChargeActions.MassChargExclutionsPolicy And
           nAction <> MassiveChargeActions.MassChargePolicyCollect And
           nAction <> MassiveChargeActions.MassChargePolicyMulti Then
            If nBranch = eRemoteDB.Constants.intNull Then
                Call lobjErrors.ErrorMessage(sCodispl, 9064)
            Else
                '+ Se va a validar el campo producto

                If nProduct = eRemoteDB.Constants.intNull Then
                    Call lobjErrors.ErrorMessage(sCodispl, 11009)
                Else
                    lobjValues.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    If Not lobjValues.IsValid("tabProdMaster1", CStr(nProduct), True) Then
                        Call lobjErrors.ErrorMessage(sCodispl, 9066)
                    End If
                End If
            End If

            If nAction <> MassiveChargeActions.MassChargePolicy And
               nAction <> MassiveChargeActions.MassChargExclutionsPolicy Then
                If dEffecdate = eRemoteDB.Constants.dtmNull Then
                    Call lobjErrors.ErrorMessage(sCodispl, 70122)
                End If
            End If

            '+ Se va a validar el Campo de poliza
            If nAction <> MassiveChargeActions.MassDelTempList And nAction <> MassiveChargeActions.MassPrintList And nAction <> MassiveChargeActions.MassChargePolicy And nAction <> MassiveChargeActions.MassChargExclutionsPolicy Then
                If Not (nBranch = eRemoteDB.Constants.intNull) And Not (nProduct = eRemoteDB.Constants.intNull) Then
                    If nPolicy = eRemoteDB.Constants.intNull Then
                        '+ Se valida que el número de póliza se haya indicado solo si se trata de un producto colectivo
                        If lobjProduct.Find(nBranch, nProduct, Today) Then
                            If Trim(lobjProduct.sPolitype) = "2" Then
                                Call lobjErrors.ErrorMessage(sCodispl, 3003)
                            End If
                        End If
                    Else
                        If nAction <> MassiveChargeActions.MassChargTemp Then
                            With lclsPolicy
                                If Not .FindPolicyOfficeName(sCertype, nBranch, nProduct, nPolicy, "1") Then
                                    Call lobjErrors.ErrorMessage(sCodispl, 3001)
                                Else
                                    If Trim(.sPolitype) <> "2" Then
                                        Call lobjErrors.ErrorMessage(sCodispl, 38016)
                                    End If
                                    If (sCertype = "2") Then
                                        If (Trim(.sStatus_pol) <> "3") And (Trim(.sStatus_pol) <> "2") Then

                                            If (Not (.nNullcode = eRemoteDB.Constants.intNull)) And Trim(CStr(.nNullcode)) <> "0" Then
                                                Call lobjErrors.ErrorMessage(sCodispl, 3098)
                                            End If
                                        Else
                                            Call lobjErrors.ErrorMessage(sCodispl, 3882)
                                        End If
                                    End If
                                    If dEffecdate <> eRemoteDB.Constants.dtmNull Then
                                        If nAction = MassiveChargeActions.MassChargCertificat Then
                                            If dEffecdate <> .dStartdate And .dStartdate > .dChangdat Then
                                                Call lobjErrors.ErrorMessage(sCodispl, 60589)
                                            End If
                                        Else
                                            If dEffecdate < .dDate_Origi Or dEffecdate > .DEXPIRDAT Then
                                                Call lobjErrors.ErrorMessage(sCodispl, 60569)
                                            End If
                                            If dEffecdate > .dNextReceip Then
                                                Call lobjErrors.ErrorMessage(sCodispl, 38049)
                                            End If
                                        End If
                                        If .dChangdat > dEffecdate And nAction <> MassiveChargeActions.MassCalcTempList Then
                                            Call lobjErrors.ErrorMessage(sCodispl, 3090)
                                        End If
                                    End If
                                End If
                            End With
                        End If
                    End If
                End If
            End If
        End If

        If sReinsuran <> "1" And nAction <> MassiveChargeActions.MassDelTempList And (nAction <> MassiveChargeActions.MassCalcTempList And sUseFile <> "1") And nAction <> MassiveChargeActions.MassPrintList Then
            If nWorksheet = eRemoteDB.Constants.intNull Then
                Call lobjErrors.ErrorMessage(sCodispl, 55715)
            Else
                If nAction = eRemoteDB.Constants.intNull Then
                    Call lobjErrors.ErrorMessage(sCodispl, 10896)
                Else
                    If lcolColSheet.Find(nWorksheet) Then
                        lintCount = 0
                        If nAction <> MassiveChargeActions.MassChargClient Then
                            lstrFieldSearch = "NROLE"
                        Else
                            lstrFieldSearch = "SCLIENT"
                        End If
                        For Each lclsColsheet In lcolColSheet
                            If lclsColsheet.sSelected = "1" Then
                                lstrCritery = lclsColsheet.sField
                            End If
                            If lclsColsheet.sField = lstrFieldSearch Then
                                lintCount = lintCount + 1
                            End If

                            '+Si se cumplen las condiciones que dan por valida la transaccion se abandona búsqueda
                            If lstrCritery <> String.Empty And lintCount > 0 Then
                                Exit For
                            End If
                        Next lclsColsheet

                        If lintCount = 0 Then
                            If nAction = MassiveChargeActions.MassChargClient Then
                                Call lobjErrors.ErrorMessage(sCodispl, 55718)
                            End If
                        End If
                        If nAction = MassiveChargeActions.MassChargClient Then
                            If Trim(lstrCritery) <> "SCLIENT" Then
                                Call lobjErrors.ErrorMessage(sCodispl, 55719)
                            End If
                        End If
                    End If
                End If
            End If

            If sManual = "2" Then
                If sFile = String.Empty Then
                    Call lobjErrors.ErrorMessage(sCodispl, 55026)
                End If
            End If
        End If

        If sProc_massive = "1" Then
            Call lobjErrors.ErrorMessage(sCodispl, 100141)
        End If

        insValCAL013_k = lobjErrors.Confirm

insValCAL013_k_Err:
        If Err.Number Then
            insValCAL013_k = insValCAL013_k & Err.Description
        End If
        'UPGRADE_NOTE: Object lclsPolicy may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsPolicy = Nothing
        'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lobjErrors = Nothing
        'UPGRADE_NOTE: Object lobjValues may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lobjValues = Nothing
        'UPGRADE_NOTE: Object lcolColSheet may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lcolColSheet = Nothing
        lobjProduct = Nothing
        On Error GoTo 0
    End Function

    '% insPostCAL013_k: Realiza la validación de los campos a actualizar en la ventana CAL013.
    '  (Carga Masiva de Colectivos)
    Public Function insPostCAL013_k(ByVal nId As Integer, ByVal sFile As String, ByVal sKey As String, Optional ByVal sSeparate As String = "", Optional ByVal nRepinsured As Integer = 0, Optional ByVal nUsercode As Integer = 0, Optional ByVal nPreview As Integer = 1) As Boolean
        Dim lclsColsheet As Colsheet

        On Error GoTo insPostCAL013_k_Err

        lclsColsheet = New Colsheet


        insPostCAL013_k = lclsColsheet.insQueryInportExcel(nId, sFile, sKey, sError, sSeparate, nRepinsured, nUsercode, nPreview)


insPostCAL013_k_Err:
        If Err.Number Then
            sError = lclsColsheet.sMessage
            insPostCAL013_k = False
        End If
        '		'UPGRADE_NOTE: Object lclsColsheet may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsColsheet = Nothing
        '		On Error GoTo 0
    End Function
	
	
	'% LoadTabs: carga la secuencia de ventanas para el módulo de Carga Masiva
	Public Function LoadTabs(ByVal bQuery As Boolean, ByVal nContent As Integer) As String
		
		Dim lclsQuery As eRemoteDB.Query
		Dim lclsSequence As eFunctions.Sequence
		
		Dim lintCount As Integer
		Dim lintAux As Integer
		Dim lstrHTMLCode As String
		Dim lintAction As Integer
		Dim lintPageImage As eFunctions.Sequence.etypeImageSequence
		
		On Error GoTo LoadTabs_err
		
		lclsQuery = New eRemoteDB.Query
		lclsSequence = New eFunctions.Sequence
		
		'-Se define la variable lstrCodispl en la cual se almacena el código de la ventana
		'-extraído de la constante cstrWindows
		
		Dim lstrCodispl As String
		
		lstrHTMLCode = lclsSequence.makeTable
		
		lintAux = 1
		lintAction = IIf(bQuery, eFunctions.Menues.TypeActions.clngActionQuery, eFunctions.Menues.TypeActions.clngActionUpdate)
		
		For lintCount = 1 To CN_FRAMESCHARGE
			
			'+ Se extrae el código de la ventana
			lstrCodispl = Mid(CN_WINDOWSCHARGE, lintAux, 8)
			lintAux = lintAux + 8
			
			Call lclsQuery.OpenQuery("Windows", "sCodisp, sCodispl, sShort_des", "sCodispl='" & lstrCodispl & "'")
			
			Select Case Trim(lstrCodispl)
				'+ Se obtiene por cada transacción un campo (requerido) de la misma para identificar
				'+ si tiene o no contenido
				Case "CAL659"
					If nContent <> 1 Then
						lintPageImage = eFunctions.Sequence.etypeImageSequence.eRequired
					Else
						lintPageImage = eFunctions.Sequence.etypeImageSequence.eOK
					End If
					
				Case "CAL660"
					If nContent <> 1 Then
						lintPageImage = eFunctions.Sequence.etypeImageSequence.eRequired
					Else
						lintPageImage = eFunctions.Sequence.etypeImageSequence.eOK
					End If
					
			End Select
			
			lstrHTMLCode = lstrHTMLCode & lclsSequence.makeRow(lclsQuery.FieldToClass("sCodisp"), lstrCodispl, lintAction, lclsQuery.FieldToClass("sShort_des"), lintPageImage)
			
		Next lintCount
		
		LoadTabs = lstrHTMLCode & lclsSequence.closeTable()
		
		
LoadTabs_err: 
		If Err.Number Then
			LoadTabs = "LoadTabs: " & Err.Description
		End If
		'UPGRADE_NOTE: Object lclsQuery may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsQuery = Nothing
		'UPGRADE_NOTE: Object lclsSequence may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsSequence = Nothing
		On Error GoTo 0
	End Function
	
	'% DefaultValueCAL013: se maneja el estado de los campos de la página
	Public Function DefaultValueCAL013(ByVal sField As String, ByVal sValue As String, ByVal sLinkSpecial As String) As Integer
		Select Case sField
			'+ Propuesta
			
			Case "opt_Proposal"
				DefaultValueCAL013 = IIf(sValue = "1" And sLinkSpecial <> String.Empty, 1, 2)
				'+ Póliza
			Case "opt_Policy"
				If sLinkSpecial = String.Empty Then
					DefaultValueCAL013 = 1
				Else
					DefaultValueCAL013 = IIf(sValue = "2", 1, 2)
				End If
				'+ Cotización
			Case "opt_Quotation"
				DefaultValueCAL013 = IIf(sValue = "3" And sLinkSpecial <> String.Empty, 1, 2)
		End Select
	End Function
	
	'%insValCAL013: Esta función se encarga de validar si existen inconsistencias para la tabla de carga masiva.
	Public Function insValCAL013(ByVal sContent As String) As String
		Dim lerrTime As eFunctions.Errors
		
		On Error GoTo insValCAL013_Err
		
		lerrTime = New eFunctions.Errors
		
		With lerrTime
			If sContent <> "1" Then
				Call .ErrorMessage("CAL013", 55717)
			End If
			insValCAL013 = .Confirm
		End With
		
insValCAL013_Err: 
		If Err.Number Then
			insValCAL013 = insValCAL013 & Err.Description
		End If
		'UPGRADE_NOTE: Object lerrTime may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lerrTime = Nothing
		On Error GoTo 0
	End Function
	
	'%Valt_inconsist(). Esta funcion devuelve la cantidad de registros sin actualizar
	Public Function Valt_inconsist(ByVal sKey As String) As Boolean
		
		Dim lrecT_inconsist As eRemoteDB.Execute
		
		On Error GoTo Valt_inconsist_Err
		
		lrecT_inconsist = New eRemoteDB.Execute
		
		'Definición de parámetros para stored procedure 'insudb.valt_inconsist'
		
		With lrecT_inconsist
			.StoredProcedure = "valt_inconsist"
			.Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Me.nCount = .FieldToClass("lCount")
				.RCloseRec()
			End If
		End With
		
		
Valt_inconsist_Err: 
		If Err.Number Then
			Valt_inconsist = False
		End If
		'UPGRADE_NOTE: Object lrecT_inconsist may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecT_inconsist = Nothing
		On Error GoTo 0
	End Function
	
	
	'%insValCAL660: Esta función se encarga de validar que se seleccione registros en los valores permitidos
	Public Function insValCAL660(ByVal sValue As String) As String
		Dim lerrTime As eFunctions.Errors
		
		On Error GoTo insValCAL660_Err
		
		lerrTime = New eFunctions.Errors
		
		With lerrTime
			If sValue = String.Empty Or sValue = "0" Then
				Call .ErrorMessage("CAL660", 55537,  , eFunctions.Errors.TextAlign.LeftAling, "Valor")
			End If
			insValCAL660 = .Confirm
		End With
		
insValCAL660_Err: 
		If Err.Number Then
			insValCAL660 = insValCAL660 & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lerrTime may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lerrTime = Nothing
	End Function
	
	'%FindTable5571. Esta funcion se encarga de Buscar el Criterio de busqueda en Table5571
	Public Function Countcritery(ByVal nId As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double) As Integer
		Dim lrecCountcritery As eRemoteDB.Execute
		
		On Error GoTo Countcritery_Err
		
		lrecCountcritery = New eRemoteDB.Execute
		
		'+Definición de parámetros para stored procedure 'insudb.UpdColSheet'
		'+Información leída el 05/02/2001 10:58:38 a.m.
		With lrecCountcritery
			.StoredProcedure = "Reacount_worksheet_critery"
			.Parameters.Add("nId", nId, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Countcritery = .FieldToClass("nCount", 0)
				.RCloseRec()
			Else
				Countcritery = 0
			End If
		End With
		
		
Countcritery_Err: 
		If Err.Number Then
			Countcritery = 0
		End If
		On Error GoTo 0
	End Function
	
	'%FindCount_critery(). Esta funcion devuelve si los campos estan completos ára el criterio de busqueda
	Public Function FindCount_critery(ByVal nId As Integer) As Integer
		
		Dim lrecReaCountColSheet As eRemoteDB.Execute
		
		lrecReaCountColSheet = New eRemoteDB.Execute
		
		'Definición de parámetros para stored procedure 'insudb.CreColSheet'
		'Información leída el 05/02/2001 10:58:38 a.m.
		
		With lrecReaCountColSheet
			.StoredProcedure = "REACOUNT_CRITERY"
			.Parameters.Add("nId", nId, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				FindCount_critery = .FieldToClass("nCount", 0)
				.RCloseRec()
			Else
				FindCount_critery = 0
			End If
		End With
		
		'UPGRADE_NOTE: Object lrecReaCountColSheet may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecReaCountColSheet = Nothing
		
	End Function
	
	'% insValCAL784_k: Validaciones proceso de generación automática de propuestas de renovación
	Public Function insValCAL784_K(ByVal dStart As Date, ByVal dEnd As Date) As String
		Dim lobjErrors As eFunctions.Errors
		
		lobjErrors = New eFunctions.Errors
		
		On Error GoTo insValCAL784_K_Err
		
		'+ Se valida el campo fecha de inicio
		If dStart = eRemoteDB.Constants.dtmNull Then
			Call lobjErrors.ErrorMessage("CAL784", 3237)
		End If
		
		'+ Se validar el campo fecha final
		If dEnd = eRemoteDB.Constants.dtmNull Then
			Call lobjErrors.ErrorMessage("CAL784", 3239)
		End If
		
		If dStart <> eRemoteDB.Constants.dtmNull And dEnd <> eRemoteDB.Constants.dtmNull And dStart > dEnd Then
			Call lobjErrors.ErrorMessage("CAL784", 3108)
		End If
		
		insValCAL784_K = lobjErrors.Confirm
		
insValCAL784_K_Err: 
		If Err.Number Then
			insValCAL784_K = insValCAL784_K & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjErrors = Nothing
	End Function
	
	'% insPostCAL784_K: Se realiza proceso de generación automática de propuestas de renovación
	Public Function insPostCAL784_K(ByVal dStart As Date, ByVal dEnd As Date, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nOffice As Integer, ByVal nOfficeagen As Integer, ByVal nIntermed As Double, ByVal nUsercode As Integer, ByVal sOptions As String) As Boolean
		Dim lrecinsPostCAL784_K As eRemoteDB.Execute
		
		On Error GoTo insPostCAL784_K_Err
		
		lrecinsPostCAL784_K = New eRemoteDB.Execute
		
		With lrecinsPostCAL784_K
			.StoredProcedure = "insPostCAL784_K"
			.Parameters.Add("dStart", dStart, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEnd", dEnd, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOffice", nOffice, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOfficeagen", nOfficeagen, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nIntermed", nIntermed, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sOptions", sOptions, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sKey", "", eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			insPostCAL784_K = .Run(False)
			If insPostCAL784_K Then
				sKey = .Parameters("sKey").Value
			End If
		End With
		
insPostCAL784_K_Err: 
		If Err.Number Then
			insPostCAL784_K = False
		End If
		'UPGRADE_NOTE: Object lrecinsPostCAL784_K may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsPostCAL784_K = Nothing
		On Error GoTo 0
	End Function
	
	'% insPostCAL854_K: Se realiza proceso de generación automática de propuestas de renovación
	Public Function insPostCAL854_K(ByVal sOptions As String, ByVal nOrigin As Integer, ByVal dEndDate As Date, ByVal nUsercode As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer) As Boolean
		Dim lrecinsPostCAL854_K As eRemoteDB.Execute
		
		On Error GoTo insPostCAL854_K_Err
		
		lrecinsPostCAL854_K = New eRemoteDB.Execute
		
		With lrecinsPostCAL854_K
			.StoredProcedure = "insPostCAL854_K"
			.Parameters.Add("sOptions", sOptions, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOrigin", nOrigin, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEndDate", dEndDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sKey", "", eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			insPostCAL854_K = .Run(False)
			If insPostCAL854_K Then
				sKey = .Parameters("sKey").Value
			End If
		End With
		
insPostCAL854_K_Err: 
		If Err.Number Then
			insPostCAL854_K = False
		End If
		'UPGRADE_NOTE: Object lrecinsPostCAL854_K may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsPostCAL854_K = Nothing
		On Error GoTo 0
	End Function
	
	
	
	Public Function valRepCol742() As Boolean
		Dim lrecvalRepCol742 As eRemoteDB.Execute
		
		On Error GoTo valRepCol742_Err
		
		lrecvalRepCol742 = New eRemoteDB.Execute
		'+
		'+ Definición de store procedure valExistsCO700 al 02-09-2002 14:31:52
		'+
		With lrecvalRepCol742
			.StoredProcedure = "REAT_CHEQUESPKG.REA_REPORT_EXIST"
			.Parameters.Add("nExists", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				valRepCol742 = (.Parameters("nExists").Value > 0)
			End If
		End With
		
valRepCol742_Err: 
		If Err.Number Then
			valRepCol742 = False
		End If
		'UPGRADE_NOTE: Object lrecvalRepCol742 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecvalRepCol742 = Nothing
		On Error GoTo 0
	End Function
End Class






