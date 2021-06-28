Option Strict Off
Option Explicit On
Public Class CoReinsuran_win
	'%-------------------------------------------------------%'
	'% $Workfile:: CoReinsuran_win.cls                      $%'
	'% $Author:: Vvera                                      $%'
	'% $Date:: 27/03/06 19:29                               $%'
	'% $Revision:: 3                                        $%'
	'%-------------------------------------------------------%'
	
	'- Constantes para el número posible de frames en la secuencia de CoReaseguros
	
	'- Contratos Proporcionales
	Private Const CN_FRAMESNUMCO_REINSURANSEQ_P As Integer = 11
	
	'- Contratos No Proporcionales
    Private Const CN_FRAMESNUMCO_REINSURANSEQ_NP As Integer = 4
	
	'- Se define la variable que contiene la imagen a asociar a la página en la secuencia
	Private mintPageImage As eFunctions.Sequence.etypeImageSequence
	
	'- Se define la constante para los codispl en la secuencia de CoReaseguros
	
	'- Contratos Proporcionales
	Private Const CN_WINDOWSCO_REINSURANSEQ_P As String = "CR301   CR572   CR760   CR724   CR302   CR307   CR725   CR731   CR303   CR758   CR020"
	'- Contratos No Proporcionales
    Private Const CN_WINDOWSCO_REINSURANSEQ_NP As String = "CR304   CR305   CR307   CR309"

    '% LoadTabsContrproc: Esta función se encarga de cargar la información necesaria para cada
    '%                    pestaña que será mostrada para CoReaseguro - Contratos Proporcionales
	Private Function LoadTabsContrproc(ByVal nAction As Integer, ByVal nContraType As Integer, ByVal sCodispl_CR As String, ByVal nNumber As Integer, ByVal nBranch As Integer, ByVal dEffecdate As Date) As String
		Dim lclsQuery As eRemoteDB.Query
		Dim lclsContrproc As eCoReinsuran.Contrproc
		Dim lclsPart_contr As eCoReinsuran.Part_contr
		Dim lclsRetention As eCoReinsuran.Retention
		Dim lclsSequence As eFunctions.Sequence
		Dim lclsContr_cumul As eCoReinsuran.Contr_Cumul
		Dim lobjContr_Cumuls As eCoReinsuran.Contr_Cumuls
		Dim lobjContr_Cescovs As eCoReinsuran.contr_cescovs
		Dim lobjContr_limcov As eCoReinsuran.Contr_LimCov
		Dim lclsContr_comm As eCoReinsuran.Contr_comm
		Dim lcolContr_comms As eCoReinsuran.Contr_comms
		Dim lobjRetentionzones As eCoReinsuran.Retentionzones
		Dim lclsRetentioncov As eCoReinsuran.Retentioncov = New eCoReinsuran.Retentioncov
		Dim lcolretentioncovs As eCoReinsuran.Retentioncovs = New eCoReinsuran.Retentioncovs
		
		Dim llngCount As Integer
		Dim llngAux As Integer
        Dim lvntRequireField As Object = Nothing
        Dim lvntAux As Object = Nothing
		Dim lstrHTMLCode As String
		Dim lblnValid As Boolean
		
		On Error GoTo LoadTabsContrproc_err
		
		lclsQuery = New eRemoteDB.Query
		lclsContrproc = New eCoReinsuran.Contrproc
		lclsPart_contr = New eCoReinsuran.Part_contr
		lclsRetention = New eCoReinsuran.Retention
		lclsSequence = New eFunctions.Sequence
		lclsContr_cumul = New eCoReinsuran.Contr_Cumul
		lobjContr_Cumuls = New eCoReinsuran.Contr_Cumuls
		lobjContr_Cescovs = New eCoReinsuran.contr_cescovs
		lobjContr_limcov = New eCoReinsuran.Contr_LimCov
		lclsContr_comm = New eCoReinsuran.Contr_comm
		lcolContr_comms = New eCoReinsuran.Contr_comms
		lobjRetentionzones = New eCoReinsuran.Retentionzones
		
		'-Se define la variable lstrCodispl en la cual se almacena el código de la ventana
		'-extraído de la constante cstrWindows
		
        Dim lstrCodispl As String
        Dim nTypeConst As Integer = 0
		
		Call lclsContrproc.Find(nNumber, nContraType, nBranch, dEffecdate, True)
		
		lstrHTMLCode = lclsSequence.makeTable
		
		
		llngAux = 1
		
		Dim lintCount As Integer
		Dim lintTotalPercent As Integer
		For llngCount = 1 To CN_FRAMESNUMCO_REINSURANSEQ_P
			
			'+ Se extrae el código de la ventana
			
			lstrCodispl = Trim(Mid(CN_WINDOWSCO_REINSURANSEQ_P, llngAux, 8))
			llngAux = llngAux + 8
			
			lblnValid = True
			If (nContraType = 1) And (lstrCodispl <> "CR301" And lstrCodispl <> "CR020" And lstrCodispl <> "CR572" And lstrCodispl <> "CR760" And lstrCodispl <> "CR758") Then
				lblnValid = False
			End If
			If (nContraType <> 1) And (lstrCodispl = "CR572" Or lstrCodispl = "CR760") Then
				lblnValid = False
			End If
			If (nContraType = 1) And (lstrCodispl = "CR572") And (lclsContrproc.sRetcover) <> "1" Then
				lblnValid = False
			End If
			If (lstrCodispl = "CR758") And (lclsContrproc.sCumulpol) <> "3" Then
				lblnValid = False
			End If
			If (nContraType = 1) And (lstrCodispl = "CR760") And (lclsContrproc.sRetzone) <> "1" Then
				lblnValid = False
			End If
			If (nContraType <> 1) And (lstrCodispl = "CR725") And (lclsContrproc.sCessprcov) <> "1" Then
				lblnValid = False
			Else
				If (nContraType = 1) And (lstrCodispl = "CR725") Then
					lblnValid = False
				End If
			End If
			
			If (nContraType = 1) And (lstrCodispl = "CR724") And (lclsContrproc.sLimitCov) <> "1" Then
				lblnValid = False
			End If
			
			If (nContraType = 1) And (lstrCodispl = "CR731") And (lclsContrproc.sCommCov) <> "1" Then
				lblnValid = False
			End If
			
			
			If lblnValid Then
				Call lclsQuery.OpenQuery("Windows", "sCodisp, sCodispl, sShort_des", "sCodispl='" & lstrCodispl & "'")
				
				Select Case Trim(lstrCodispl)
					
					'+ Se asigna la imagen "Required" sólo si la actión el "Registrar" y los codispl son estos casos
					
					Case "CR301"
						If lclsContrproc.sLimitCov = "1" Then
                            lvntRequireField = lclsContrproc.sLimitCov
                            nTypeConst = 3
                        Else
                            Select Case nContraType
                                Case 1
                                    lvntRequireField = lclsContrproc.nCurrency
                                    nTypeConst = 1
                                Case 2, 3
                                    lvntRequireField = lclsContrproc.nQuota_sha
                                    nTypeConst = 4
                                Case 5, 6, 7, 8
                                    lvntRequireField = lclsContrproc.nLines
                                    nTypeConst = 4
                                Case 9, 10
                                    lvntRequireField = lclsContrproc.nMax_even
                                    nTypeConst = 1
                            End Select
						End If
						
					Case "CR302"
						If lclsContrproc.nFreqpay > 0 And lclsContrproc.nFqcy_acc > 0 And lclsContrproc.nCurrency > 0 Then
							lvntRequireField = lclsContrproc.nFreqpay
						Else
							lvntRequireField = eRemoteDB.Constants.intNull
                        End If
                        nTypeConst = 1
						
					Case "CR303"
						If lclsContrproc.nProfit_sh <> 0 And lclsContrproc.nProfit_sh <> eRemoteDB.Constants.intNull Then
                            lvntRequireField = lclsContrproc.nProfit_sh
                            nTypeConst = 4
						Else
							If (lclsContrproc.nRate_claim <> 0 And lclsContrproc.nRate_claim <> eRemoteDB.Constants.intNull) Then
                                lvntRequireField = lclsContrproc.nRate_claim
                                nTypeConst = 4
							Else
								If lclsContrproc.nExcess <> 0 And lclsContrproc.nExcess <> eRemoteDB.Constants.intNull Then
									lvntRequireField = lclsContrproc.nExcess
								Else
                                    lvntRequireField = System.DBNull.Value
                                End If
                                lvntAux = 0
                                nTypeConst = 0
							End If
						End If
						
					Case "CR020"
						
						If lclsRetention.Find(nNumber, nContraType, nBranch, dEffecdate) Then
							Call lclsRetention.ItemRetention(0)
						End If
						
						If lclsRetention.nConsec <> 0 And lclsRetention.nConsec <> eRemoteDB.Constants.intNull Then
                            lvntRequireField = lclsRetention.nConsec
                            nTypeConst = 1
						Else
                            lvntRequireField = System.DBNull.Value
                            nTypeConst = 0
                            lvntAux = 0
                        End If
						
					Case "CR307"
						If lclsPart_contr.Find(sCodispl_CR, nNumber, nContraType, nBranch, dEffecdate) Then
							
							For lintCount = 0 To lclsPart_contr.Count - 1
								If lclsPart_contr.ItemCR307(lintCount) Then
									lintTotalPercent = lintTotalPercent + lclsPart_contr.nShare
								End If
							Next 
							If lintTotalPercent <> 100 Then
								lvntRequireField = eRemoteDB.Constants.intNull
							Else
								lvntRequireField = lintTotalPercent
								'Verificar si hubo ingreso por cálculo solo de cesión por cobertura, se verifica los valores en Contr_Cescov
                                If lclsContrproc.sCesscia = String.Empty And (lclsContrproc.sCessprcov <> String.Empty And lclsContrproc.sCessprcov <> "0") Then
                                    If lobjContr_Cescovs.Find(nNumber, nBranch, nContraType, dEffecdate) Then
                                        lvntRequireField = "1"
                                    Else
                                        lvntRequireField = eRemoteDB.Constants.intNull
                                    End If
                                End If
							End If
						Else
							lvntRequireField = eRemoteDB.Constants.intNull
                        End If
                        nTypeConst = 1
						
					Case "CR572"
						If lcolretentioncovs.Find(nNumber, nBranch, nContraType, dEffecdate) Then
							For	Each lclsRetentioncov In lcolretentioncovs
								If lclsRetentioncov.nCovergen > 0 Then
									lvntRequireField = "1"
									Exit For
								End If
							Next lclsRetentioncov
						Else
							lvntRequireField = String.Empty
                        End If
                        nTypeConst = 3
						
					Case "CR758"
						If lobjContr_Cumuls.Find(nNumber, nBranch, nContraType, dEffecdate) Then
							For	Each lclsContr_cumul In lobjContr_Cumuls
								lvntRequireField = lclsContr_cumul.nBranch
								Exit For
                            Next lclsContr_cumul
                            nTypeConst = 1
						Else
							If lclsContrproc.sCumulpol = "3" Then
                                lvntRequireField = System.DBNull.Value
								If lclsContrproc.sCumulpol = "3" Then
									lvntRequireField = eRemoteDB.Constants.intNull
                                End If
                                nTypeConst = 1
							Else
                                lvntRequireField = lclsContrproc.sCumulpol
                                nTypeConst = 3
							End If
						End If
						
					Case "CR760"
						If lclsContrproc.sRetzone = "1" Then
							lvntRequireField = eRemoteDB.Constants.intNull
							If lobjRetentionzones.Find(nNumber, nBranch, nContraType, dEffecdate) Then
								lvntRequireField = lobjRetentionzones.Count
                            End If
                            nTypeConst = 1
						Else
                            lvntRequireField = lclsContrproc.sRetzone
                            nTypeConst = 3
						End If
						
					Case "CR724"
						If lobjContr_limcov.ReaContr_LimCov(nNumber, nBranch, nContraType, dEffecdate) Then
                            lvntRequireField = "1"
                            nTypeConst = 3
						Else
							If lclsContrproc.sLimitCov = "1" Then
                                lvntRequireField = eRemoteDB.Constants.intNull
                                nTypeConst = 1
							Else
                                lvntRequireField = System.DBNull.Value
                                nTypeConst = 0
                                lvntAux = 0
							End If
						End If
						
					Case "CR725"
						If (lobjContr_Cescovs.Find(nNumber, nBranch, nContraType, dEffecdate)) And (lclsPart_contr.Find(sCodispl_CR, nNumber, nContraType, nBranch, dEffecdate)) Then
                            lvntRequireField = "1"
                            nTypeConst = 1
						Else
							If lclsContrproc.sCessprcov = "1" Then
                                lvntRequireField = eRemoteDB.Constants.intNull
                                nTypeConst = 1
							Else
                                lvntRequireField = System.DBNull.Value
                                nTypeConst = 0
							End If
						End If
						
					Case "CR731"
						If lcolContr_comms.Find(nNumber, nBranch, nContraType, dEffecdate) Then
                            lvntRequireField = "1"
                            nTypeConst = 3
						Else
							If lclsContrproc.sCommCov = "1" Then
                                lvntRequireField = eRemoteDB.Constants.intNull
                                nTypeConst = 1
							Else
                                lvntRequireField = System.DBNull.Value
                                nTypeConst = 0
                                lvntAux = 0
							End If
						End If
						
					Case Else
                        lvntRequireField = System.DBNull.Value
                        nTypeConst = 0
                        lvntAux = 0
				End Select
				
				
                mintPageImage = eFunctions.Sequence.etypeImageSequence.eEmpty
                mintPageImage = ValCodispl_LoadTabsContrproc(lvntRequireField, nTypeConst, lvntAux)
				
				'+ Se asigna la imagen asociada a la página asociada al Codispl
                'If lvntRequireField = eRemoteDB.Constants.intNull Or lvntRequireField = eRemoteDB.Constants.dtmNull Or lvntRequireField = String.Empty Or IsDBNull(lvntAux) Then
                'If IsDBNull(lvntRequireField) Then
                '    mintPageImage = eFunctions.Sequence.etypeImageSequence.eEmpty
                'ElseIf (IsNumeric(lvntRequireField) AndAlso lvntRequireField <= 0) OrElse lvntRequireField.ToString() = String.Empty Then
                '    mintPageImage = eFunctions.Sequence.etypeImageSequence.eRequired
                'ElseIf (IsNumeric(lvntRequireField) AndAlso lvntRequireField <= 0) OrElse lvntRequireField.ToString() <> String.Empty Or lvntRequireField Is System.DBNull.Value Then
                '    '+ Ventanas con contenido
                '    mintPageImage = eFunctions.Sequence.etypeImageSequence.eOK
                'End If
                If (nAction = eFunctions.Menues.TypeActions.clngActionQuery And mintPageImage = eFunctions.Sequence.etypeImageSequence.eOK) Or nAction <> eFunctions.Menues.TypeActions.clngActionQuery Then
                    lstrHTMLCode = lstrHTMLCode & lclsSequence.makeRow(lclsQuery.FieldToClass("sCodisp"), lstrCodispl, nAction, lclsQuery.FieldToClass("sShort_des"), mintPageImage)
                End If
            End If
		Next llngCount
		
		
		LoadTabsContrproc = lstrHTMLCode & lclsSequence.closeTable()
		
		
		
LoadTabsContrproc_err: 
		If Err.Number Then
			LoadTabsContrproc = "LoadTabsContrproc: " & Err.Description
		End If
		
        lclsQuery = Nothing
        lclsContrproc = Nothing
        lclsPart_contr = Nothing
        lclsRetention = Nothing
        lclsSequence = Nothing
        lclsContr_cumul = Nothing
        lobjContr_Cumuls = Nothing
        lobjContr_Cescovs = Nothing
        lobjContr_limcov = Nothing
        lclsContr_comm = Nothing
        lcolContr_comms = Nothing
        lobjRetentionzones = Nothing
        lclsRetentioncov = Nothing
        lcolretentioncovs = Nothing
		
		On Error GoTo 0
	End Function
	
	'% LoadTabsContrnproc: Esta función se encarga de cargar la información necesaria para cada
	'%                    pestaña que será mostrada para CoReaseguro - Contratos No Proporcionales
	Private Function LoadTabsContrnproc(ByVal nAction As Integer, ByVal nContraType As Integer, ByVal sCodispl_CR As String, ByVal nNumber As Integer, ByVal nBranch As Integer, ByVal dEffecdate As Date) As String
		Dim lclsQuery As eRemoteDB.Query
		Dim lclsContrnpro As eCoReinsuran.Contrnpro
		Dim lcolContrnpros As eCoReinsuran.Contrnpros
		Dim lclsPart_contr As eCoReinsuran.Part_contr
		Dim lclsSequence As eFunctions.Sequence
        Dim lcolContrnp_Riskss As eCoReinsuran.Contrnp_Riskss

		Dim llngCount As Integer
		Dim llngAux As Integer
        Dim lvntRequireField As Object = New Object
        Dim lvntAux As Object = Nothing
		Dim lstrHTMLCode As String
		Dim lblnValid As Boolean
		Dim lstrCodispl As String
		
		On Error GoTo LoadTabsContrnproc_err
		
		lclsQuery = New eRemoteDB.Query
		lclsContrnpro = New eCoReinsuran.Contrnpro
		lcolContrnpros = New eCoReinsuran.Contrnpros
		lclsPart_contr = New eCoReinsuran.Part_contr
		lclsSequence = New eFunctions.Sequence
        lcolContrnp_Riskss = New eCoReinsuran.Contrnp_Riskss

		'-Se define la variable lstrCodispl en la cual se almacena el código de la ventana
		'-extraído de la constante cstrWindows
		
		Call lclsContrnpro.Find(nNumber, nContraType, nBranch, dEffecdate, True)
		
		lstrHTMLCode = lclsSequence.makeTable
		
		llngAux = 1
		lblnValid = True
		
		
		Dim lintCount As Integer
		Dim lintTotalPercent As Integer
		For llngCount = 1 To CN_FRAMESNUMCO_REINSURANSEQ_NP
			
			'+ Se extrae el código de la ventana
			lstrCodispl = Mid(CN_WINDOWSCO_REINSURANSEQ_NP, llngAux, 8)
			llngAux = llngAux + 8
			
			If lblnValid Then
				Call lclsQuery.OpenQuery("Windows", "sCodisp, sCodispl, sShort_des", "sCodispl='" & lstrCodispl & "'")
				
				Select Case Trim(lstrCodispl)
					
					'+ Se asigna la imagen "Required" sólo si la actión el "Registrar" y los codispl son estos casos'
					
					Case "CR304"
						If lclsContrnpro.Find(nNumber, nContraType, nBranch, dEffecdate) Then
							lvntRequireField = lclsContrnpro.nRetention
							If lvntRequireField <= 0 Then
								Call lcolContrnpros.Find(nNumber, nContraType, nBranch, dEffecdate)
								If lcolContrnpros.Count > 0 Then
									lvntRequireField = lcolContrnpros.Count
								End If
							End If
						Else
							lvntRequireField = String.Empty
						End If
						
					Case "CR305"
						If lclsContrnpro.Find(nNumber, nContraType, nBranch, dEffecdate) Then
							lvntRequireField = lclsContrnpro.sReinsuran
							If lvntRequireField = String.Empty Then
								Call lcolContrnpros.Find(nNumber, nContraType, nBranch, dEffecdate)
								If lcolContrnpros.Count > 0 Then
									lvntRequireField = lcolContrnpros.Count
								End If
							End If
						Else
							lvntRequireField = String.Empty
						End If
						
					Case "CR307"
						If lclsPart_contr.Find(sCodispl_CR, nNumber, nContraType, nBranch, dEffecdate) Then
							
							For lintCount = 0 To lclsPart_contr.Count - 1
								If lclsPart_contr.ItemCR307(lintCount) Then
									lintTotalPercent = lintTotalPercent + lclsPart_contr.nShare
								End If
							Next 
							If lintTotalPercent <> 100 Then
								lvntRequireField = eRemoteDB.Constants.intNull
							Else
								lvntRequireField = lintTotalPercent
							End If
						Else
							lvntRequireField = eRemoteDB.Constants.intNull
						End If

                    Case "CR309"
                        If lcolContrnp_Riskss.Find(nNumber, nBranch, dEffecdate, nContraType) Then

                            If lcolContrnp_Riskss.Count > 0 Then
                                lvntRequireField = lcolContrnp_Riskss.Count
                            Else
                                lvntRequireField = eRemoteDB.Constants.intNull
                            End If
                        End If
                    Case Else
                        lvntRequireField = System.DBNull.Value
                End Select


                mintPageImage = eFunctions.Sequence.etypeImageSequence.eEmpty

                '+ Se asigna la imagen asociada a la página asociada al Codispl

                If (IsNumeric(lvntRequireField) AndAlso lvntRequireField = eRemoteDB.Constants.intNull) Or _
                   (IsDate(lvntRequireField) AndAlso lvntRequireField = eRemoteDB.Constants.dtmNull) Or _
                   (TypeName(lvntRequireField) = "String" AndAlso lvntRequireField = String.Empty) Or _
                   IsDBNull(lvntAux) Then
                    mintPageImage = eFunctions.Sequence.etypeImageSequence.eRequired
                Else
                    '+Ventanas con contenido
                    mintPageImage = eFunctions.Sequence.etypeImageSequence.eOK
                End If

                If (nAction = eFunctions.Menues.TypeActions.clngActionQuery And mintPageImage = eFunctions.Sequence.etypeImageSequence.eOK) Or nAction <> eFunctions.Menues.TypeActions.clngActionQuery Then

                    lstrHTMLCode = lstrHTMLCode & lclsSequence.makeRow(lclsQuery.FieldToClass("sCodisp"), lstrCodispl, nAction, lclsQuery.FieldToClass("sShort_des"), mintPageImage)
                End If
                lclsQuery.CloseQuery()
            End If
        Next llngCount

        LoadTabsContrnproc = lstrHTMLCode & lclsSequence.closeTable()


        lclsQuery = Nothing
        lclsContrnpro = Nothing
        lclsPart_contr = Nothing
        lclsSequence = Nothing

LoadTabsContrnproc_err:
        If Err.Number Then
            LoadTabsContrnproc = "LoadTabsContrnproc: " & Err.Description
        End If
        On Error GoTo 0
	End Function
	
	'% LoadTabs: Esta función se encarga de cargar la información necesaria para cada
	'%           pestaña que será mostrada para CoReaseguro - Contratos Proporcionales y
	'%           Contratos No Proporcionales
    Public Function LoadTabs(ByVal nAction As Integer, ByVal nContraType As Integer, ByVal sCodispl_CR As String, ByVal nNumber As Integer, ByVal nBranch As Integer, ByVal dEffecdate As Date) As String

        LoadTabs = String.Empty

        If nContraType = 1 Or nContraType = 2 Or nContraType = 3 Or nContraType = 5 Or nContraType = 6 Or nContraType = 7 Or nContraType = 8 Or nContraType = 9 Or nContraType = 10 Or sCodispl_CR = "CR301_K" Then

            LoadTabs = LoadTabsContrproc(nAction, nContraType, sCodispl_CR, nNumber, nBranch, dEffecdate)


        ElseIf nContraType = 680 Or nContraType = 681 Or nContraType = 682 Or nContraType = 683 Or nContraType = 685 Or nContraType = 686 Or nContraType = 687 Or nContraType = 689 Or nContraType = 690 Or nContraType = 691 Or nContraType = 692 Then

            LoadTabs = LoadTabsContrnproc(nAction, nContraType, sCodispl_CR, nNumber, nBranch, dEffecdate)

        End If

    End Function
	
	'%UpdContrMasterState: Esta funcion se actualiza el estado del contrato dependiendo de las transacciones
	Public Function UpdContrMasterState(ByVal nAction As Integer, ByVal nContraType As Integer, ByVal sCodispl_CR As String, ByVal nNumber As Integer, ByVal nBranch As Integer, ByVal dEffecdate As Date) As Boolean
		Dim lclsContrmaster As Object
        Dim lstrCodisp As String = ""

        lclsContrmaster = New eCoReinsuran.Contrmaster
		
		If nContraType = 1 Or nContraType = 2 Or nContraType = 3 Or nContraType = 5 Or nContraType = 6 Or nContraType = 7 Or nContraType = 8 Or nContraType = 9 Or nContraType = 10 Or sCodispl_CR = "CR301_K" Then
			
			UpdContrMasterState = CheckTabsContrproc(nAction, nContraType, sCodispl_CR, nNumber, nBranch, dEffecdate)
			lstrCodisp = "CR301_k"
			
        ElseIf nContraType = 680 Or nContraType = 681 Or nContraType = 682 Or nContraType = 683 Or nContraType = 685 Or nContraType = 686 Or nContraType = 687 Or nContraType = 689 Or nContraType = 690 Or nContraType = 691 Or nContraType = 692 Then

            UpdContrMasterState = CheckTabsContrnproc(nAction, nContraType, sCodispl_CR, nNumber, nBranch, dEffecdate)
            lstrCodisp = "CR304_k"
		End If
		
		If UpdContrMasterState Then
			If lclsContrmaster.updContrMasterStatregt(lstrCodisp, nNumber, nContraType, nBranch, "1") Then
				UpdContrMasterState = True
			Else
				UpdContrMasterState = False
			End If
		End If
		'UPGRADE_NOTE: Object lclsContrmaster may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsContrmaster = Nothing
	End Function
	
	'% CheckTabsContrproc: Esta función se encarga de verificar que la información requerida de las transacciones
	'%                     para CoReaseguro - Contratos Proporcionales ha sido incluida
	Private Function CheckTabsContrproc(ByVal nAction As Integer, ByVal nContraType As Integer, ByVal sCodispl_CR As String, ByVal nNumber As Integer, ByVal nBranch As Integer, ByVal dEffecdate As Date) As Boolean
		Dim lclsQuery As eRemoteDB.Query
		Dim lclsContrproc As eCoReinsuran.Contrproc
		Dim lclsPart_contr As eCoReinsuran.Part_contr
		Dim lclsRetention As eCoReinsuran.Retention
		Dim lclsSequence As eFunctions.Sequence
		Dim lclsContr_cumul As eCoReinsuran.Contr_Cumul
		Dim lobjContr_Cumuls As eCoReinsuran.Contr_Cumuls
		Dim lobjContr_Cescovs As eCoReinsuran.contr_cescovs
		Dim lobjContr_limcov As eCoReinsuran.Contr_LimCov
		Dim lclsContr_comm As eCoReinsuran.Contr_comm
		Dim lcolContr_comms As eCoReinsuran.Contr_comms
		Dim lobjRetentionzones As eCoReinsuran.Retentionzones
		Dim lclsRetentioncov As eCoReinsuran.Retentioncov = New eCoReinsuran.Retentioncov
        Dim lcolretentioncovs As eCoReinsuran.Retentioncovs = New eCoReinsuran.Retentioncovs
        Dim lclsContrMaster As eCoReinsuran.Contrmaster = Nothing
		
		Dim llngCount As Integer
		Dim llngAux As Integer
		Dim lvntRequireField As Object
        Dim lvntAux As Object = New Object
        Dim lblnOk As Boolean
		Dim lblnValid As Boolean
		
		On Error GoTo CheckTabsContrproc_err
		
		lblnOk = True
		
		lclsQuery = New eRemoteDB.Query
		lclsContrproc = New eCoReinsuran.Contrproc
		lclsPart_contr = New eCoReinsuran.Part_contr
		lclsRetention = New eCoReinsuran.Retention
		lclsContr_cumul = New eCoReinsuran.Contr_Cumul
		lobjContr_Cumuls = New eCoReinsuran.Contr_Cumuls
		lobjContr_Cescovs = New eCoReinsuran.contr_cescovs
		lobjContr_limcov = New eCoReinsuran.Contr_LimCov
        lobjRetentionzones = New eCoReinsuran.Retentionzones
        lclsContrMaster = New eCoReinsuran.Contrmaster
		
		'-Se define la variable lstrCodispl en la cual se almacena el código de la ventana
		'-extraído de la constante cstrWindows
		
        Dim lstrCodispl As String
        Dim lintTypeConst As Integer = 0
		
        Call lclsContrproc.Find(nNumber, nContraType, nBranch, dEffecdate, True)

        Call lclsContrMaster.Find(1, nNumber, nContraType, nBranch, dEffecdate)
		
		llngAux = 1
		
		Dim lintCount As Integer
		Dim lintTotalPercent As Integer
		For llngCount = 1 To CN_FRAMESNUMCO_REINSURANSEQ_P
			
			lstrCodispl = Trim(Mid(CN_WINDOWSCO_REINSURANSEQ_P, llngAux, 8))
			llngAux = llngAux + 8
			
			lblnValid = True
			If (nContraType = 1) And (lstrCodispl <> "CR301" And lstrCodispl <> "CR020" And lstrCodispl <> "CR572" And lstrCodispl <> "CR760") Then
				lblnValid = False
			End If
			If (nContraType <> 1) And (lstrCodispl = "CR572" Or lstrCodispl = "CR760") Then
				lblnValid = False
			End If
			If (nContraType = 1) And (lstrCodispl = "CR572") And (lclsContrproc.sRetcover) <> "1" Then
				lblnValid = False
			End If
			If (lstrCodispl = "CR758") And (lclsContrproc.sCumulpol) <> "3" Then
				lblnValid = False
			End If
			If (nContraType = 1) And (lstrCodispl = "CR760") And (lclsContrproc.sRetzone) <> "1" Then
				lblnValid = False
			End If
			If (nContraType <> 1) And (lstrCodispl = "CR725") And (lclsContrproc.sCessprcov) <> "1" Then
				lblnValid = False
			Else
				If (nContraType = 1) And (lstrCodispl = "CR725") Then
					lblnValid = False
				End If
			End If
			
			If (nContraType = 1) And (lstrCodispl = "CR724") And (lclsContrproc.sLimitCov) <> "1" Then
				lblnValid = False
			End If
			
			If lblnValid Then
				Call lclsQuery.OpenQuery("Windows", "sCodisp, sCodispl, sShort_des", "sCodispl='" & lstrCodispl & "'")
                lvntRequireField = 0
				Select Case Trim(lstrCodispl)
					
					Case "CR301"
						If lclsContrproc.sLimitCov = "1" Then
                            lvntRequireField = lclsContrproc.sLimitCov
                            lintTypeConst = 3
						Else
                            lintTypeConst = 4
                            Select Case nContraType
                                Case 1
                                    '  lvntRequireField = lclsContrproc.nAmount
                                    lvntRequireField = lclsContrproc.nCurrency
                                    lintTypeConst = 1
                                    ' If lvntRequireField = NumNull Then
                                    '     lvntRequireField = IIf(lclsContrproc.sRetcover = "2", NumNull, lclsContrproc.sRetcover)
                                    '     If lvntRequireField = NumNull Then
                                    '         lvntRequireField = IIf(lclsContrproc.sRetzone = "2", NumNull, lclsContrproc.sRetzone)
                                    '     End If
                                    ' End If
                                Case 2, 3
                                    lvntRequireField = lclsContrproc.nQuota_sha
                                Case 5, 6, 7, 8
                                    lvntRequireField = lclsContrproc.nLines
                                Case 9, 10
                                    lvntRequireField = lclsContrproc.nMax_even
                                    lintTypeConst = 1
                            End Select
						End If
						
                    Case "CR302"
                        lintTypeConst = 1
                        If lclsContrproc.nFreqpay > 0 Then
                            lvntRequireField = lclsContrproc.nFreqpay
                        Else
                            lvntRequireField = eRemoteDB.Constants.intNull
                        End If
						
                    Case "CR303"
                        lintTypeConst = 4
                        If lclsContrproc.nProfit_sh <> 0 And lclsContrproc.nProfit_sh <> eRemoteDB.Constants.intNull Then
                            lvntRequireField = lclsContrproc.nProfit_sh
                        Else
                            If (lclsContrproc.nRate_claim <> 0 And lclsContrproc.nRate_claim <> eRemoteDB.Constants.intNull) Then
                                lvntRequireField = lclsContrproc.nRate_claim
                            Else
                                If lclsContrproc.nExcess <> 0 And lclsContrproc.nExcess <> eRemoteDB.Constants.intNull Then
                                    lvntRequireField = lclsContrproc.nExcess
                                Else
                                    lvntRequireField = System.DBNull.Value
                                    lvntAux = 0
                                    lintTypeConst = 0
                                End If
                            End If
                        End If
						
					Case "CR020"
						If lclsRetention.Find(nNumber, nContraType, nBranch, dEffecdate) Then
							Call lclsRetention.ItemRetention(0)
						End If
						
						If lclsRetention.nConsec <> 0 And lclsRetention.nConsec <> eRemoteDB.Constants.intNull Then
                            lvntRequireField = lclsRetention.nConsec
                            lintTypeConst = 1
						Else
                            lvntRequireField = System.DBNull.Value
                            lvntAux = 0
                            lintTypeConst = 0
						End If
						
                    Case "CR307"
                        lintTypeConst = 1
                        If lclsPart_contr.Find(sCodispl_CR, nNumber, nContraType, nBranch, dEffecdate) Then

                            For lintCount = 0 To lclsPart_contr.Count - 1
                                If lclsPart_contr.ItemCR307(lintCount) Then
                                    lintTotalPercent = lintTotalPercent + lclsPart_contr.nShare
                                End If
                            Next
                            If lintTotalPercent <> 100 Then
                                lvntRequireField = eRemoteDB.Constants.intNull
                            Else
                                lvntRequireField = lintTotalPercent
                            End If
                        Else
                            lvntRequireField = eRemoteDB.Constants.intNull
                        End If
						
                    Case "CR572"
                        lintTypeConst = 3
                        If lcolretentioncovs.Find(nNumber, nBranch, nContraType, dEffecdate) Then
                            For Each lclsRetentioncov In lcolretentioncovs
                                If lclsRetentioncov.nCovergen > 0 Then
                                    lvntRequireField = "1"
                                    Exit For
                                End If
                            Next lclsRetentioncov
                        Else
                            lvntRequireField = String.Empty
                        End If
						
					Case "CR758"
						If lobjContr_Cumuls.Find(nNumber, nBranch, nContraType, dEffecdate) Then
							For	Each lclsContr_cumul In lobjContr_Cumuls
                                lvntRequireField = lclsContr_cumul.nBranch
                                lintTypeConst = 1
								Exit For
							Next lclsContr_cumul
						Else
							If lclsContrproc.sCumulpol = "3" Then
                                lvntRequireField = System.DBNull.Value
                                lvntAux = 0
                                lintTypeConst = 0
								If lclsContrproc.sCumulpol = "3" Then
                                    lvntRequireField = eRemoteDB.Constants.intNull
                                    lintTypeConst = 1
								End If
							Else
                                lvntRequireField = lclsContrproc.sCumulpol
                                lintTypeConst = 3
							End If
						End If
						
					Case "CR760"
						If lclsContrproc.sRetzone = "1" Then
                            lvntRequireField = eRemoteDB.Constants.intNull
                            lintTypeConst = 1
							If lobjRetentionzones.Find(nNumber, nBranch, nContraType, dEffecdate) Then
								lvntRequireField = lobjRetentionzones.Count
							End If
						Else
                            lvntRequireField = lclsContrproc.sRetzone
                            lintTypeConst = 3
						End If
						
					Case "CR724"
						If lobjContr_limcov.ReaContr_LimCov(nNumber, nBranch, nContraType, dEffecdate) Then
                            lvntRequireField = "1"
                            lintTypeConst = 3
						Else
							If lclsContrproc.sLimitCov = "1" Then
                                lvntRequireField = eRemoteDB.Constants.intNull
                                lintTypeConst = 1
							Else
                                lvntRequireField = System.DBNull.Value
                                lvntAux = 0
                                lintTypeConst = 0
							End If
						End If
						
					Case "CR725"
						If lobjContr_Cescovs.Find(nNumber, nBranch, nContraType, dEffecdate) Then
                            lvntRequireField = "1"
                            lintTypeConst = 3
						Else
							If lclsContrproc.sCessprcov = "1" Then
                                lvntRequireField = eRemoteDB.Constants.intNull
                                lintTypeConst = 1
							Else
                                lvntRequireField = System.DBNull.Value
                                lvntAux = 0
                                lintTypeConst = 0
							End If
						End If
						
					Case Else
                        lvntRequireField = System.DBNull.Value
                        lvntAux = 0
                        lintTypeConst = 0
				End Select
				
                If IsNullValue(lvntRequireField, lintTypeConst, lvntAux) Then
                    lblnOk = False
                    Exit For
                End If
                lclsQuery.CloseQuery()
			End If
		Next llngCount
		
		CheckTabsContrproc = lblnOk
		
		
CheckTabsContrproc_err: 
		If Err.Number Then
			CheckTabsContrproc = False
		End If
		
        lclsQuery = Nothing
        lclsContrproc = Nothing
        lclsPart_contr = Nothing
        lclsRetention = Nothing
        lclsSequence = Nothing
        lclsContr_cumul = Nothing
        lobjContr_Cumuls = Nothing
        lobjContr_Cescovs = Nothing
        lobjContr_limcov = Nothing
        lclsContr_comm = Nothing
        lcolContr_comms = Nothing
        lobjRetentionzones = Nothing
        lclsRetentioncov = Nothing
        lcolretentioncovs = Nothing
        lclsContrMaster = Nothing
		
		On Error GoTo 0
	End Function
	'% CheckTabsContrnproc: Esta función se encarga de verificar que la información requerida de las transacciones
	'%                     para CoReaseguro - Contratos No Proporcionales ha sido incluida
	Private Function CheckTabsContrnproc(ByVal nAction As Integer, ByVal nContraType As Integer, ByVal sCodispl_CR As String, ByVal nNumber As Integer, ByVal nBranch As Integer, ByVal dEffecdate As Date) As Boolean
		Dim lclsQuery As New eRemoteDB.Query
		Dim lclsContrnpro As eCoReinsuran.Contrnpro = New eCoReinsuran.Contrnpro
		Dim lcolContrnpros As eCoReinsuran.Contrnpros = New eCoReinsuran.Contrnpros
		Dim lclsPart_contr As eCoReinsuran.Part_contr = New eCoReinsuran.Part_contr
        Dim lcolContrnp_Riskss As eCoReinsuran.Contrnp_Riskss = New eCoReinsuran.Contrnp_Riskss

		Dim llngCount As Integer
		Dim llngAux As Integer
        Dim lvntRequireField As Object = New Object
        Dim lvntAux As Object
		Dim lblnValid As Boolean
		Dim lblnOk As Boolean
		Dim lstrCodispl As String
		
		On Error GoTo CheckTabsContrnproc_err
		
		lblnOk = True
		
		'-Se define la variable lstrCodispl en la cual se almacena el código de la ventana
		'-extraído de la constante cstrWindows
		
		Call lclsContrnpro.Find(nNumber, nContraType, nBranch, dEffecdate, True)
		
		llngAux = 1
		lblnValid = True
		
		
		Dim lintCount As Integer
		Dim lintTotalPercent As Integer
		For llngCount = 1 To CN_FRAMESNUMCO_REINSURANSEQ_NP
			
			'+ Se extrae el código de la ventana
			lstrCodispl = Mid(CN_WINDOWSCO_REINSURANSEQ_NP, llngAux, 8)
			llngAux = llngAux + 8
			
			Call lclsQuery.OpenQuery("Windows", "sCodisp, sCodispl, sShort_des", "sCodispl='" & lstrCodispl & "'")
			
			Select Case Trim(lstrCodispl)
				
				'+ Se asigna la imagen "Required" sólo si la actión el "Registrar" y los codispl son estos casos'
				
				Case "CR304"
					lvntRequireField = lclsContrnpro.sDescript
					
				Case "CR305"
					lvntRequireField = lclsContrnpro.sReinsuran
					If lvntRequireField = String.Empty Then
						Call lcolContrnpros.Find(nNumber, nContraType, nBranch, dEffecdate)
						If lcolContrnpros.Count > 0 Then
							lvntRequireField = lcolContrnpros.Count
						End If
					End If
					
				Case "CR307"
					If lclsPart_contr.Find(sCodispl_CR, nNumber, nContraType, nBranch, dEffecdate) Then
						
						For lintCount = 0 To lclsPart_contr.Count - 1
							If lclsPart_contr.ItemCR307(lintCount) Then
								lintTotalPercent = lintTotalPercent + lclsPart_contr.nShare
							End If
						Next 
						If lintTotalPercent <> 100 Then
							lvntRequireField = eRemoteDB.Constants.intNull
						Else
							lvntRequireField = lintTotalPercent
						End If
					Else
						lvntRequireField = eRemoteDB.Constants.intNull
					End If

                Case "CR309"
                    If lcolContrnp_Riskss.Find(nNumber, nBranch, dEffecdate, nContraType) Then

                        If lcolContrnp_Riskss.Count > 0 Then
                            lvntRequireField = lcolContrnp_Riskss.Count

                        Else
                            lvntRequireField = eRemoteDB.Constants.intNull
                        End If
                    End If
                Case Else
                    lvntRequireField = System.DBNull.Value
            End Select
            lclsQuery.CloseQuery()
            '+ Se asigna la imagen asociada a la página asociada al Codispl
            If (IsNumeric(lvntRequireField) AndAlso lvntRequireField = eRemoteDB.Constants.intNull) Or _
               (IsDate(lvntRequireField) AndAlso lvntRequireField = eRemoteDB.Constants.dtmNull) Or _
               (TypeName(lvntRequireField) = "String" AndAlso lvntRequireField = String.Empty) Then
                lblnOk = False
                Exit For
            End If
        Next llngCount



        CheckTabsContrnproc = lblnOk

CheckTabsContrnproc_err:
        If Err.Number Then
            CheckTabsContrnproc = False
        End If

        lclsQuery = Nothing
        lclsContrnpro = Nothing
        lclsPart_contr = Nothing
        lcolContrnpros = Nothing

        On Error GoTo 0
    End Function


    '% IsNullValue: Verifica si el valor pasado como parámetro es Nulo dependiendo de su Tipo
    '% La función dara como resultado un valor booleano
    Private Function IsNullValue(ByVal lvntRequireField As Object, ByVal nTypeConst As Integer, Optional ByVal lvntAux As Object = Nothing) As Boolean
        Select Case nTypeConst
            Case 0  '+ Para valores tipo DBNULL
                If IsDBNull(lvntRequireField) And IsDBNull(lvntAux) Then
                    IsNullValue = True
                Else
                    IsNullValue = False
                End If
            Case 1  '+ Para valores tipo integer
                If lvntRequireField = eRemoteDB.Constants.intNull Or IsDBNull(lvntAux) Then
                    IsNullValue = True
                Else
                    IsNullValue = False
                End If
            Case 2  '+ Para valores tipo date
                If lvntRequireField = eRemoteDB.Constants.dtmNull Or IsDBNull(lvntAux) Then
                    IsNullValue = True
                Else
                    IsNullValue = False
                End If
            Case 3  '+ Para valores tipo String
                If lvntRequireField Is String.Empty Or IsDBNull(lvntAux) Then
                    IsNullValue = True
                Else
                    IsNullValue = False
                End If
            Case 4  '+ Para valores tipo Double
                If lvntRequireField = eRemoteDB.Constants.dblNull Or IsDBNull(lvntAux) Then
                    IsNullValue = True
                Else
                    IsNullValue = False
                End If
        End Select
    End Function

    '% ValCodispl_LoadTabsContrproc: Se valida y asigna la imagen asociada a la página asociada al Codispl
    '% La función dara como resultado el tipo enumerado correspondiente según etypeImageSequence
    Private Function ValCodispl_LoadTabsContrproc(ByVal lvntRequireField As Object, ByVal nTypeConst As Integer, Optional ByVal lvntAux As Object = Nothing) As eFunctions.Sequence.etypeImageSequence
        Select Case nTypeConst
            Case 0  '+ Para valores tipo DBNULL
                If IsDBNull(lvntRequireField) And IsDBNull(lvntAux) Then
                    ValCodispl_LoadTabsContrproc = eFunctions.Sequence.etypeImageSequence.eRequired
                ElseIf Not IsDBNull(lvntRequireField) Then
                    ValCodispl_LoadTabsContrproc = eFunctions.Sequence.etypeImageSequence.eOK
                Else
                    ValCodispl_LoadTabsContrproc = eFunctions.Sequence.etypeImageSequence.eEmpty
                End If
            Case 1  '+ Para valores tipo integer
                If lvntRequireField = eRemoteDB.Constants.intNull Or IsDBNull(lvntAux) Then
                    ValCodispl_LoadTabsContrproc = eFunctions.Sequence.etypeImageSequence.eRequired
                ElseIf lvntRequireField <> eRemoteDB.Constants.intNull Then
                    ValCodispl_LoadTabsContrproc = eFunctions.Sequence.etypeImageSequence.eOK
                Else
                    ValCodispl_LoadTabsContrproc = eFunctions.Sequence.etypeImageSequence.eEmpty
                End If
            Case 2  '+ Para valores tipo date
                If lvntRequireField = eRemoteDB.Constants.dtmNull Or IsDBNull(lvntAux) Then
                    ValCodispl_LoadTabsContrproc = eFunctions.Sequence.etypeImageSequence.eRequired
                ElseIf lvntRequireField <> eRemoteDB.Constants.intNull Then
                    ValCodispl_LoadTabsContrproc = eFunctions.Sequence.etypeImageSequence.eOK
                Else
                    ValCodispl_LoadTabsContrproc = eFunctions.Sequence.etypeImageSequence.eEmpty
                End If
            Case 3  '+ Para valores tipo String
                If lvntRequireField Is String.Empty Or IsDBNull(lvntAux) Then
                    ValCodispl_LoadTabsContrproc = eFunctions.Sequence.etypeImageSequence.eRequired
                ElseIf lvntRequireField <> String.Empty Then
                    ValCodispl_LoadTabsContrproc = eFunctions.Sequence.etypeImageSequence.eOK
                Else
                    ValCodispl_LoadTabsContrproc = eFunctions.Sequence.etypeImageSequence.eEmpty
                End If
            Case 4  '+ Para valores tipo Double
                If lvntRequireField = eRemoteDB.Constants.dblNull Or IsDBNull(lvntAux) Then
                    ValCodispl_LoadTabsContrproc = eFunctions.Sequence.etypeImageSequence.eRequired
                ElseIf lvntRequireField <> eRemoteDB.Constants.dblNull Then
                    ValCodispl_LoadTabsContrproc = eFunctions.Sequence.etypeImageSequence.eOK
                Else
                    ValCodispl_LoadTabsContrproc = eFunctions.Sequence.etypeImageSequence.eEmpty
                End If
        End Select
    End Function


End Class






