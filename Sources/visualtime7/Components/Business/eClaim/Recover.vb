Option Strict Off
Option Explicit On
Public Class Recover
	'%-------------------------------------------------------%'
	'% $Workfile:: Recover.cls                              $%'
	'% $Author:: Nvaplat22                                  $%'
	'% $Date:: 8/03/04 1:30p                                $%'
	'% $Revision:: 16                                       $%'
	'%-------------------------------------------------------%'
	
	'Column_name                            Type        Computed                            Length      Prec  Scale Nullable                            TrimTrailingBlanks                  FixedLenNullInSource                Collation
	'-------------------------------------- ----------- ----------------------------------- ----------- ----- ----- ----------------------------------- ----------------------------------- ----------------------------------- --------------------------------------------------------------------------------------------------------------------------------
	Public nClaim As Double '       int         no                                  4           10    0     no                                  (n/a)                               (n/a)                               NULL
	Public nCase_Num As Integer '       smallint    no                                  2           5     0     no                                  (n/a)                               (n/a)                               NULL
	Public nDeman_Type As Integer '       smallint    no                                  2           5     0     no                                  (n/a)                               (n/a)                               NULL
	Public nTransac As Integer '       smallint    no                                  2           5     0     no                                  (n/a)                               (n/a)                               NULL
	Public dCompdate As Date '       datetime    no                                  8                       yes                                 (n/a)                               (n/a)                               NULL
	Public nRecover_typ As Double '       int         no                                  4           10    0     yes                                 (n/a)                               (n/a)                               NULL
	Public nCost_recu As Double '       decimal     no                                  9           14    2     yes                                 (n/a)                               (n/a)                               NULL
	Public nCostl_recu As Double '       decimal     no                                  9           14    2     yes                                 (n/a)                               (n/a)                               NULL
	Public nCurrency As Integer '       smallint    no                                  2           5     0     yes                                 (n/a)                               (n/a)                               NULL
	Public nEs_cos_re As Double '       decimal     no                                  9           14    2     yes                                 (n/a)                               (n/a)                               NULL
	Public nEs_inc_re As Double '       decimal     no                                  9           14    2     yes                                 (n/a)                               (n/a)                               NULL
	Public nEsl_cos_re As Double '       decimal     no                                  9           14    2     yes                                 (n/a)                               (n/a)                               NULL
	Public nEsl_inc_re As Double '       decimal     no                                  9           14    2     yes                                 (n/a)                               (n/a)                               NULL
	Public dEstdate As Date '       datetime    no                                  8                       yes                                 (n/a)                               (n/a)                               NULL
	Public sClient As String '       char        no                                  14                      yes                                 no                                  yes                                 SQL_Latin1_General_CP1_CI_AS
	Public sNum_case As String '       char        no                                  10                      yes                                 no                                  yes                                 SQL_Latin1_General_CP1_CI_AS
	Public dPresdate As Date '       datetime    no                                  8                       yes                                 (n/a)                               (n/a)                               NULL
	Public nRec_amount As Double '       decimal     no                                  9           14    2     yes                                 (n/a)                               (n/a)                               NULL
	Public dRecdate As Date '       datetime    no                                  8                       yes                                 (n/a)                               (n/a)                               NULL
	Public nRecl_amoun As Double '       decimal     no                                  9           14    2     yes                                 (n/a)                               (n/a)                               NULL
	Public nProvider As Integer '       smallint    no                                  2           5     0     yes                                 (n/a)                               (n/a)                               NULL
	Public sStatregt As String '       char        no                                  1                       yes                                 no                                  yes                                 SQL_Latin1_General_CP1_CI_AS
	Public sTribunal As String '       char        no                                  40                      yes                                 no                                  yes                                 SQL_Latin1_General_CP1_CI_AS
	Public nUsercode As Integer '       smallint    no                                  2           5     0     yes                                 (n/a)                               (n/a)                               NULL
	Public nNotenum As Double '       int         no                                  4           10    0     yes                                 (n/a)                               (n/a)                               NULL
    Public nStatus As Integer
	'+ Definición de variables auxilares
	Public sCurrencyDescript As String
	Public nCover As Integer
	Public sCoverDescript As String
	Public sClienameRecover As String
	Public sClienameThird As String
	Public sCliename As String
	
	Public sEstdate As String
	Public sPresdate As String
	
	Public nBordereaux As Double
	Public nConcept As Integer
	
	Public nRecoverAmou As Double
	Public nExpensesAmou As Double
	
	Public nModulec As Integer
	Public nRecoverNumber As Integer
	
	Public nCost_recu_sum As Double
	Public nCostl_recu_sum As Double
	Public nRec_amount_sum As Double
    Public nRecl_amoun_sum As Double
    Public nRecoveramou_Sum As Double
    Public nCost_amou_Sum As Double

	Public ReadOnly Property sKey(ByVal nUsercode As Integer) As Object
		Get
			sKey = Format(Now, "yyyyMMddhhmmss") & CStr(nUsercode)
		End Get
	End Property
	
	'%FindRecover: Realiza la consulta de los ingresos por recobro que se han realizado
	Public Function FindRecover(ByVal nClaim As Double, ByVal nTransac As Integer) As Boolean
		Dim lrecClaim As eRemoteDB.Execute
		
		On Error GoTo FindRecover_err
		
		lrecClaim = New eRemoteDB.Execute
		
		FindRecover = True
		
		With lrecClaim
			.StoredProcedure = "reaRecoverAmount"
			.Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTransac", nTransac, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				sCurrencyDescript = .FieldToClass("sDescript")
				nCurrency = .FieldToClass("nCurrency")
				nRec_amount = .FieldToClass("nRec_Amount")
				nCost_recu = .FieldToClass("nCost_recu")
                nNotenum = .FieldToClass("nNotenum")
                nBordereaux = .FieldToClass("nBordereaux")

				.RCloseRec()
			Else
				FindRecover = False
			End If
		End With
		
FindRecover_err: 
		If Err.Number Then
			FindRecover = False
		End If
		On Error GoTo 0
		lrecClaim = Nothing
	End Function
	
	'%FindTCLRecover: Realiza la consulta de los ingresos por recobro que se han realizado sobre la tabla temporal
    Public Function FindTCLRecover(ByVal sKey As String, ByVal nClaim As Double, ByVal nCover As Double, ByVal sClient As String, Optional ByVal nTransac As Integer = eRemoteDB.Constants.intNull) As Boolean
        Dim lrecClaim As eRemoteDB.Execute

        On Error GoTo FindTCLRecover_err

        lrecClaim = New eRemoteDB.Execute

        FindTCLRecover = True

        With lrecClaim
            .StoredProcedure = "reaTCLRecover"
            .Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTransac", nTransac, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run Then
                nRecoverAmou = .FieldToClass("nRecoverAmou")
                nExpensesAmou = .FieldToClass("nExpensesAmou")
                .RCloseRec()
            Else
                FindTCLRecover = False
            End If
        End With

FindTCLRecover_err:
        If Err.Number Then
            FindTCLRecover = False
        End If
        On Error GoTo 0
        lrecClaim = Nothing
    End Function
	
	'%FindCashID: Realiza la consulta de los ingresos por recobro que se han realizado
	Public Function FindCashID(ByVal nClaim As Double, ByVal nCash_ID As Double) As Boolean
		Dim lrecClaim As eRemoteDB.Execute
		
		On Error GoTo FindCashID_err
		
		lrecClaim = New eRemoteDB.Execute
		
		FindCashID = True
		
		With lrecClaim
			.StoredProcedure = "reaClaimCashID"
			.Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCash_ID", nCash_ID, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				nClaim = .FieldToClass("nClaim")
				nCash_ID = .FieldToClass("nCash_ID")
				nConcept = .FieldToClass("nConcept")
				.RCloseRec()
			Else
				FindCashID = False
			End If
		End With
		
FindCashID_err: 
		If Err.Number Then
			FindCashID = False
		End If
		On Error GoTo 0
		lrecClaim = Nothing
	End Function
	
	'%FindRelConceptsClaim: Realiza la consulta de las relaciones por recobro que se han realizado
	Public Function FindRelConceptsClaim(ByVal nClaim As Double, ByVal nBordereaux As Double) As Boolean
		Dim lrecClaim As eRemoteDB.Execute
		
		On Error GoTo FindRelConceptsClaim_Err
		
		lrecClaim = New eRemoteDB.Execute
		
		FindRelConceptsClaim = True
		
		With lrecClaim
			.StoredProcedure = "ReaRelConceptsClaim"
			.Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBordereaux", nBordereaux, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				nClaim = .FieldToClass("nClaim")
				nBordereaux = .FieldToClass("nBordereaux")
				nConcept = .FieldToClass("nConcept")
				.RCloseRec()
			Else
				FindRelConceptsClaim = False
			End If
		End With
		
FindRelConceptsClaim_Err: 
		If Err.Number Then
			FindRelConceptsClaim = False
		End If
		On Error GoTo 0
		lrecClaim = Nothing
	End Function
	
	'*Find : Realiza la lectura de la tabla Recover
    Public Function Find(ByVal nClaim As Double, ByVal nCase_Num As Integer, ByVal nDeman_Type As Integer, ByVal nTransac As Integer, Optional ByVal nStatus As Integer = 2) As Boolean
        Dim lrecRecover As eRemoteDB.Execute

        On Error GoTo Find_Err

        lrecRecover = New eRemoteDB.Execute

        With lrecRecover
            .StoredProcedure = "reaRecover"
            .Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCase_num", nCase_Num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nDeman_type", nDeman_Type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTransac", nTransac, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nStatus", nStatus, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            If .Run Then
                Find = True
                Me.nRecover_typ = .FieldToClass("nRecover_typ")
                Me.nCurrency = .FieldToClass("nCurrency")
                Me.dEstdate = .FieldToClass("dEstDate")
                Me.dPresdate = .FieldToClass("dPresDate")
                Me.sEstdate = .FieldToClass("dEstDate")
                Me.sPresdate = .FieldToClass("dPresDate")
                Me.nEs_inc_re = .FieldToClass("nEs_inc_re")
                Me.nEs_cos_re = .FieldToClass("nEs_cos_re")
                Me.sNum_case = .FieldToClass("sNum_Case")
                Me.sTribunal = .FieldToClass("sTribunal")
                Me.sClient = .FieldToClass("sClient")
                Me.nProvider = .FieldToClass("nProvider")
                Me.nTransac = .FieldToClass("nTransac")
                Me.nCase_Num = .FieldToClass("nCase_num")
                Me.nStatus = .FieldToClass("nStatus")
                Me.nBordereaux = .FieldToClass("nBordereaux")
                .RCloseRec()

                .StoredProcedure = "reaTab_provider"
                .Parameters.Add("nProvider", Me.nProvider, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                If .Run Then
                    Me.sClienameRecover = .FieldToClass("sCliename")
                    .RCloseRec()
                End If

                .StoredProcedure = "reaClient"
                .Parameters.Add("sClient", Me.sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                If .Run Then
                    Me.sClienameThird = .FieldToClass("sCliename")
                    .RCloseRec()
                End If
            Else
                Find = False
            End If
        End With

Find_Err:
        If Err.Number Then
            Find = False
        End If
        On Error GoTo 0
        lrecRecover = Nothing
    End Function
	
	'%insPostSI012: Esta función se encarga de validar los datos introducidos en la zona de contenido para "frame" especifico.
    Public Function insPostSI012(ByVal sCodispl As String, ByVal nMainAction As Integer, ByVal nClaim As Double, ByVal nCase_Num As Integer, ByVal nTransac As Double, ByVal nDeman_Type As Integer, ByVal sNum_case As String, ByVal sTribunal As String, ByVal nCurrency As Integer, ByVal nUsercode As Integer, ByVal nExpenses As Double, ByVal nIncome As Double, ByVal dEstdate As Date, ByVal dPresdate As Date, ByVal sThird As String, ByVal nRecover_typ As Double, ByVal nProvider As Integer, ByVal nStatus As Integer) As Boolean
        Dim lrecRecover As eRemoteDB.Execute
        Dim lclsClaim As eClaim.Claim
        Dim lclsRecover As eClaim.Recover
        Dim lrecReaClaim_His_Max_Transac As New eRemoteDB.Execute
        Dim llngTransaction As Integer
        Dim nRecoverNumberAux As Integer
        Dim llngAction As Object

        lrecRecover = New eRemoteDB.Execute
        lclsClaim = New eClaim.Claim
        lclsRecover = New eClaim.Recover

        On Error GoTo insPostSI012_err

        llngAction = IIf(nTransac = eRemoteDB.Constants.intNull, 0, 1)

        '+ Obtiene los datos del siniestro
        If nTransac = eRemoteDB.Constants.intNull Then
            With lrecReaClaim_His_Max_Transac
                .StoredProcedure = "ReaClaim_His_Max_Transac"
                .Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nCase_num", nCase_Num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nDeman_type", nDeman_Type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nTransac", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                If .Run(False) Then
                    llngTransaction = .Parameters("nTransac").Value
                Else
                    llngTransaction = 1
                End If
            End With
            lrecReaClaim_His_Max_Transac = Nothing
        End If

        nRecoverNumberAux = IIf(nTransac = eRemoteDB.Constants.intNull, llngTransaction + 1, nTransac)

        insPostSI012 = True
        If nMainAction <> 401 Then
            With lrecRecover
                .StoredProcedure = "insRecover"
                .Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nCase_num", nCase_Num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nDeman_type", nDeman_Type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("sNum_case", sNum_case, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("sTribunal", sTribunal, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 40, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nEs_cos_re", nExpenses, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 12, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nes_inc_re", nIncome, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 12, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("dEstDate", dEstdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("dPresDate", dPresdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("sClient", sThird, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nRecover_typ", nRecover_typ, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nProvider", nProvider, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nTransac", nRecoverNumberAux, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nAction", llngAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nStatus", nStatus, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

                Me.nRecoverNumber = nRecoverNumberAux
                insPostSI012 = .Run(False)
            End With
        End If

insPostSI012_err:
        If Err.Number Then
            insPostSI012 = False
        End If
        On Error GoTo 0
        lrecRecover = Nothing
        lclsClaim = Nothing
        lclsRecover = Nothing
    End Function
	
	'%insValSI012: Se realiza todas la validaciones del folder o frame
    Public Function insValSI012(ByVal sCodispl As String, ByVal nCase_Num As Integer, ByVal nProvider As Integer, ByVal dPresdate As Date, ByVal dEstdate As Date, ByVal nIncome As Double, ByVal nExpenses As Double, ByVal nCurrency As Integer, ByVal sNum_case As String, ByVal sTribunal As String, ByVal nClaim As Double, ByVal nStatus As Integer) As String
        Dim lclsErrors As eFunctions.Errors
        Dim lclsClaim As eClaim.Claim

        On Error GoTo insValSI012_err

        lclsErrors = New eFunctions.Errors
        lclsClaim = New eClaim.Claim

        '+Validacion del CAMPO caso
        If nCase_Num <= 0 Or nCase_Num = eRemoteDB.Constants.intNull Then
            Call lclsErrors.ErrorMessage(sCodispl, 4289)
        End If

        '+Validacion del campo recobrador
        If nProvider > 0 Or nProvider = eRemoteDB.Constants.intNull Then
            If Not ReaTab_Provider(nProvider) Then
                Call lclsErrors.ErrorMessage(sCodispl, 4069)
            End If
        End If

        '+ Obtiene los datos básicos del sinisestro
        Call lclsClaim.Find(nClaim)

        '+Validacion del campo fecha de presentacion
        If (dPresdate = eRemoteDB.Constants.dtmNull) Then
            Call lclsErrors.ErrorMessage(sCodispl, 4070)
        Else
            If dPresdate < lclsClaim.dDecladat Then
                Call lclsErrors.ErrorMessage(sCodispl, 4347)
            Else
                If (dPresdate > Now) Then
                    Call lclsErrors.ErrorMessage(sCodispl, 4072)
                End If
            End If
        End If

        '+Se valida la fecha estimada de recobro
        If (dEstdate = eRemoteDB.Constants.dtmNull) Then
            Call lclsErrors.ErrorMessage(sCodispl, 4073)
        Else
            If dEstdate < lclsClaim.dDecladat Then
                Call lclsErrors.ErrorMessage(sCodispl, 4074)
            Else
                If (dEstdate < dPresdate) Then
                    Call lclsErrors.ErrorMessage(sCodispl, 4074)
                End If
            End If
        End If

        '+Se valida estimado - ingresos
        If nIncome = 0 Or nIncome = eRemoteDB.Constants.intNull Then
            Call lclsErrors.ErrorMessage(sCodispl, 4075)
        End If

        '+Se valida estimado - costos
        If nExpenses = 0 Or nExpenses = eRemoteDB.Constants.intNull Then
            Call lclsErrors.ErrorMessage(sCodispl, 4076)
        End If

        '+Se valida Importe estimado-Moneda
        If nCurrency <= 0 Or nCurrency = eRemoteDB.Constants.intNull Then
            Call lclsErrors.ErrorMessage(sCodispl, 1351)
        End If

        '+Se valida Tribunal-Caso juridico
        If Trim(sNum_case) = String.Empty And Trim(sTribunal) <> String.Empty Then
            Call lclsErrors.ErrorMessage(sCodispl, 4079)
        ElseIf Trim(sNum_case) <> String.Empty And Trim(sTribunal) = String.Empty Then
            Call lclsErrors.ErrorMessage(sCodispl, 4184)
        End If


        '+Se valida Valores preliminares
        If nStatus <= 0 Or nStatus = eRemoteDB.Constants.intNull Then
            Call lclsErrors.ErrorMessage(sCodispl, 900037, , , "Valores preliminares")
        End If


        insValSI012 = lclsErrors.Confirm

insValSI012_err:
        If Err.Number Then
            insValSI012 = "insValSI012: " & Err.Description
        End If
        On Error GoTo 0
        lclsErrors = New eFunctions.Errors
        lclsClaim = New eClaim.Claim
    End Function
	
	'insValSI013Upd: Se realiza todas la validaciones correspondientes a la transacción (UPD)
	Public Function insValSI013Upd(ByVal sCodispl As String, ByVal nRecoverAmou As Double) As String
		Dim lclsErrors As eFunctions.Errors
		
		On Error GoTo insValSI013Upd_err
		
		lclsErrors = New eFunctions.Errors
		
		With lclsErrors
			'+ Importe recobrado debe estar lleno
			If nRecoverAmou = 0 Or nRecoverAmou = eRemoteDB.Constants.intNull Then
				Call .ErrorMessage(sCodispl, 4314)
			End If
			
			insValSI013Upd = .Confirm
		End With
		
insValSI013Upd_err: 
		If Err.Number Then
			insValSI013Upd = "insValSI013Upd: " & Err.Description
		End If
		On Error GoTo 0
		lclsErrors = Nothing
	End Function
	
	'insValSI013: Se realiza todas la validaciones correspondientes a la transacción
	Public Function insValSI013(ByVal sCodispl As String, ByVal nTransac As Integer, ByVal nBordereaux As Double, ByVal sKey As String, ByVal nClaim As Double) As String
		Dim lclsErrors As eFunctions.Errors
		Dim lclsQuery As eRemoteDB.Query
		
		On Error GoTo insValSI013_Err
		
		lclsErrors = New eFunctions.Errors
		
		With lclsErrors
			'+Validacion del campo trámite de recobro
			If nTransac <= 0 Then
				Call .ErrorMessage(sCodispl, 4292)
			End If
			
            '+Validacion de que al menos una línea debe tener contenido en la columna de importe recobrado
			If Not valExistAmount(sKey, nClaim) Then
                Call .ErrorMessage(sCodispl, 4083)
            Else
                FindRecoverSumtot(nClaim, nCase_Num, nDeman_Type, nTransac, sKey)
                If nRec_amount_sum <> nRecoveramou_Sum Then
                    Call .ErrorMessage(sCodispl, 90000504, , 1, "Recupero")
                End If
                If nRecl_amoun_sum <> nCost_amou_Sum Then
                    Call .ErrorMessage(sCodispl, 90000504, , 1, "Costo")
                End If
            End If


            insValSI013 = .Confirm
        End With

insValSI013_Err:
        If Err.Number Then
            insValSI013 = "insValSI013: " & Err.Description
        End If
        On Error GoTo 0
        lclsErrors = Nothing
    End Function

    '%ReaTab_Provider: Lee la tabla Tab_provider
    Private Function ReaTab_Provider(ByVal nProvider As Integer) As Boolean
        Dim lrecRecover As eRemoteDB.Execute

        On Error GoTo ReaTab_Provider_err

        lrecRecover = New eRemoteDB.Execute

        ReaTab_Provider = True

        With lrecRecover
            .StoredProcedure = "reaTab_provider"
            .Parameters.Add("nProvider", nProvider, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run Then
                Me.sClienameRecover = .FieldToClass("sCliename")
                .RCloseRec()
            Else
                ReaTab_Provider = False
            End If
        End With

ReaTab_Provider_err:
        If Err.Number Then
            ReaTab_Provider = False
        End If
        On Error GoTo 0
        lrecRecover = Nothing
    End Function

    'insPostSI013Upd: Ejecuta las actualizaciones de la transacción/ventana de tipo PopUp
    Public Function insPostSI013Upd(ByVal sKey As String, ByVal nClaim As Double, ByVal nTransac As Integer, ByVal nCover As Integer, ByVal nModulec As Integer, ByVal nRecoverAmou As Double, ByVal nExpensesAmou As Double, ByVal sClient As String, ByVal nUsercode As Integer) As Boolean
        Dim lrecClaim As eRemoteDB.Execute

        On Error GoTo insPostFolder_Err

        lrecClaim = New eRemoteDB.Execute

        With lrecClaim
            .StoredProcedure = "creTclRecover"
            .Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTransac", nTransac, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nModuleC", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nRecoverAmou", nRecoverAmou, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 14, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nExpensesAmou", nExpensesAmou, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 14, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            insPostSI013Upd = .Run(False)
        End With

insPostFolder_Err:
        If Err.Number Then
            insPostSI013Upd = False
        End If
        On Error GoTo 0
        lrecClaim = Nothing
    End Function

    '%insPostSI013: Ejecuta las actualizaciones de la transacción/ventana principal
    Public Function insPostSI013(ByVal sKey As String, ByVal nClaim As Double, ByVal nTransac As Integer, ByVal nUsercode As Integer, ByVal nReference As Integer, ByVal nNotenum As Double, ByVal nBordereaux As Double) As Boolean
        Dim lrecClaim As eRemoteDB.Execute

        On Error GoTo insPostFolder_Err

        lrecClaim = New eRemoteDB.Execute

        With lrecClaim
            .StoredProcedure = "insRecover_SI013"
            .Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTransac", nTransac, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nReference", nReference, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nNotenum", nNotenum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBordereaux", nBordereaux, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            insPostSI013 = .Run(False)
        End With

insPostFolder_Err:
        If Err.Number Then
            insPostSI013 = False
        End If
        On Error GoTo 0
        lrecClaim = Nothing
    End Function

    '%valExistAmount: Verifica si se ha ingresado alguna línea en el grid; es decir, si existe algún monto dado
    Public Function valExistAmount(ByVal sKey As String, ByVal nClaim As Double) As Boolean
        Dim lrecClaim As eRemoteDB.Execute
        Dim ldblValue As Double

        On Error GoTo valExistAmount_Err

        lrecClaim = New eRemoteDB.Execute

        With lrecClaim
            .StoredProcedure = "GetSumTCLRECOVER"
            .Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nAmount_sum", ldblValue, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 14, 2, 14, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Run(False)
            valExistAmount = (.Parameters("nAmount_sum").Value > 0)
        End With

valExistAmount_Err:
        If Err.Number Then
            valExistAmount = False
        End If
        On Error GoTo 0
        lrecClaim = Nothing
    End Function

    '%FindTCLRecoverSum: Obtiene el monto a recobrar y gastos actuales.
    Public Function FindTCLRecoverSum(ByVal sKey As String, ByVal nClaim As Double) As Boolean
        Dim lrecClaim As eRemoteDB.Execute
        Dim ldblValues As Double

        On Error GoTo FindTCLRecoverSum_err

        lrecClaim = New eRemoteDB.Execute

        FindTCLRecoverSum = True

        With lrecClaim
            .StoredProcedure = "reaTCLRecoverSum"
            .Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nRecoverAmou_o", ldblValues, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 14, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nExpensesAmou_o", ldblValues, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 14, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Run(False)

            Me.nRec_amount = .Parameters("nRecoverAmou_o").Value
            Me.nExpensesAmou = .Parameters("nExpensesAmou_o").Value
        End With

FindTCLRecoverSum_err:
        If Err.Number Then
            FindTCLRecoverSum = False
        End If
        On Error GoTo 0
        lrecClaim = Nothing
    End Function

    '%FindRecoverSumE: Verifica si exsiten datos para la pagina SI013
    Public Function FindRecoverSumE(ByVal nClaim As Double, ByVal nCase_Num As Integer, ByVal nDeman_Type As Integer, ByVal nTransac As Integer) As Boolean
        Dim lrecClaim As eRemoteDB.Execute
        Dim ldblValues As Double

        On Error GoTo FindRecoverSum_Err

        lrecClaim = New eRemoteDB.Execute

        FindRecoverSumE = True

        With lrecClaim
            .StoredProcedure = "reaRecoverSumE"
            .Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCase_num", nCase_Num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nDeman_type", nDeman_Type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTransac", nTransac, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCost_recu_sum", ldblValues, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 14, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCostl_recu_sum", ldblValues, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 14, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nRec_amount_sum", ldblValues, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 14, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nRecl_amoun_sum", ldblValues, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 14, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Run(False)
            Me.nCost_recu_sum = .Parameters("nCost_recu_sum").Value
            Me.nCostl_recu_sum = .Parameters("nCostl_recu_sum").Value
            Me.nRec_amount_sum = .Parameters("nRec_amount_sum").Value
            Me.nRecl_amoun_sum = .Parameters("nRecl_amoun_sum").Value
        End With

FindRecoverSum_Err:
        If Err.Number Then
            FindRecoverSumE = False
        End If
        On Error GoTo 0
        lrecClaim = Nothing
    End Function


    '%FindRecoverSumtot: Verifica  que lo ingresado en la ventana corresponda a los totales
    Public Function FindRecoverSumtot(ByVal nClaim As Double, ByVal nCase_Num As Integer, ByVal nDeman_Type As Integer, ByVal nTransac As Integer, ByVal skey As String) As Boolean
        Dim lrecClaim As eRemoteDB.Execute
        Dim ldblValues As Double

        On Error GoTo FindRecoverSum_Err

        lrecClaim = New eRemoteDB.Execute

        FindRecoverSumtot = True

        With lrecClaim
            .StoredProcedure = "REARECOVERSUMTOT"
            .Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCase_num", nCase_Num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nDeman_type", nDeman_Type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTransac", nTransac, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sKey", skey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nRec_amount_sum", ldblValues, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 14, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nRecl_amoun_sum", ldblValues, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 14, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nRecoveramou_Sum", ldblValues, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 14, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCost_amou_Sum", ldblValues, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 14, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            .Run(False)
            Me.nRec_amount_sum = .Parameters("nRec_amount_sum").Value
            Me.nRecl_amoun_sum = .Parameters("nRecl_amoun_sum").Value
            Me.nRecoveramou_Sum = .Parameters("nRecoveramou_Sum").Value
            Me.nCost_amou_Sum = .Parameters("nCost_amou_Sum").Value


        End With

FindRecoverSum_Err:
        If Err.Number Then
            FindRecoverSumtot = False
        End If
        On Error GoTo 0
        lrecClaim = Nothing
    End Function
End Class






