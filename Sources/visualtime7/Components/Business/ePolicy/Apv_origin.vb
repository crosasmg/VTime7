Option Strict Off
Option Explicit On
Public Class Apv_origin
	
	Public sCertype As String 'CHAR(1 BYTE)                 NOT NULL,
    Public nProduct As Long 'NUMBER(5)                    NOT NULL,
    Public nBranch As Long 'NUMBER(5)                    NOT NULL,
	Public nPolicy As Double 'NUMBER(10)                   NOT NULL,
	Public nCertif As Double 'NUMBER(10)                   NOT NULL,
    Public nOrigin As Long 'NUMBER(5)                    NOT NULL,
	Public dEffecdate As Date
	Public nPercent As Double 'DECIMAL(5,2) ,
	Public nPremDeal_anu As Double 'DECIMAL(18,6),
	Public nPremDeal As Double 'DECIMAL(18,6),
	Public dNulldate As Date
    Public nUsercode As Long
	
	
	'-Campos de la forma VI8002
	Public dDependant As Date
	Public dIndependant As Date
	Public nAFP As Integer
	Public sClient As String
	Public nOption As Integer
	Public nTaxregime As Integer
	Public nCapital As Double
	Public nYearMonth_fPay As Integer
	Public sClientBos As String
	Public sFolionumber As String
	Public nDirect As Short
	Public nIndirect As Short
	
	Private mvarApv_origins As Apv_origins
	

	
	Public Property Apv_origins() As Apv_origins
		Get
			If mvarApv_origins Is Nothing Then
				mvarApv_origins = New Apv_origins
			End If
			
			Apv_origins = mvarApv_origins
		End Get
		Set(ByVal Value As Apv_origins)
			mvarApv_origins = Value
		End Set
	End Property
	'UPGRADE_NOTE: Class_Terminate was upgraded to Class_Terminate_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Terminate_Renamed()
		'UPGRADE_NOTE: Object mvarApv_origins may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mvarApv_origins = Nothing
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
	Public Function Add() As Boolean
		Add = InsUpdApv_Origin(1)
	End Function
	Public Function Delete() As Boolean
		Delete = InsUpdApv_Origin(3)
	End Function
	'%InitValues: Inicializa los valores de las variables publicas de la clase
	Private Sub InitValues()
		sCertype = String.Empty
		nBranch = eRemoteDB.Constants.intNull
		nProduct = eRemoteDB.Constants.intNull
		nPolicy = eRemoteDB.Constants.intNull
		nCertif = eRemoteDB.Constants.intNull
		dEffecdate = eRemoteDB.Constants.dtmNull
		nOrigin = eRemoteDB.Constants.intNull
		dNulldate = eRemoteDB.Constants.dtmNull
		nPremDeal_anu = eRemoteDB.Constants.intNull
		nPremDeal = eRemoteDB.Constants.intNull
		nPercent = eRemoteDB.Constants.intNull
		nUsercode = eRemoteDB.Constants.intNull
	End Sub
	'%insPostVI8002Upd: Ejecuta el post de la transacción Planes de Ahorros(VI8002)
	Public Function insPostVI8002Upd(ByVal sAction As String, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nOrigin As Integer, ByVal nPercent As Double, ByVal nPremDeal_anu As Double, ByVal nPremDeal As Double, ByVal dNulldate As Date, ByVal nUsercode As Integer) As Boolean
		Dim lclsPolicy_Win As Policy_Win
		Dim lcolAPV_origins As ePolicy.Apv_origins
		
		On Error GoTo insPostVI8002Upd_Err
		
		With Me
			.sCertype = sCertype
			.nBranch = nBranch
			.nProduct = nProduct
			.nPolicy = nPolicy
			.nCertif = nCertif
			.dEffecdate = dEffecdate
			.nOrigin = nOrigin
			.nPercent = nPercent
			.nPremDeal_anu = nPremDeal_anu
			.nPremDeal = nPremDeal
			.dNulldate = dNulldate
			.nUsercode = nUsercode
		End With
		
		Select Case sAction
			Case "Add"
				insPostVI8002Upd = Add
			Case "Update"
				insPostVI8002Upd = Update
			Case "Del"
				insPostVI8002Upd = Delete
		End Select
		
insPostVI8002Upd_Err: 
		If Err.Number Then
			insPostVI8002Upd = False
		End If
		
		On Error GoTo 0
		
		'UPGRADE_NOTE: Object lclsPolicy_Win may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsPolicy_Win = Nothing
		'UPGRADE_NOTE: Object lcolAPV_origins may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lcolAPV_origins = Nothing
	End Function
	'%insPostVI8002: Ejecuta el post de la transacción Planes de Ahorros(VI8002)
	Public Function insPostVI8002(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nOption As Integer, ByVal nTaxregime As Double, ByVal nCapital As Double, ByVal nYearMonth_fPay As Double, ByVal sClient As String, ByVal sAct_date As String, ByVal dDateWork As Date, ByVal nAFP As Short, ByVal nUsercode As Integer, ByVal sFolionumber As String) As Boolean
		Dim lclsPolicy_Win As Policy_Win
		Dim lcolAPV_origins As ePolicy.Apv_origins
		
		On Error GoTo insPostVI8002_Err
		
		With Me
			.sCertype = sCertype
			.nBranch = nBranch
			.nProduct = nProduct
			.nPolicy = nPolicy
			.nCertif = nCertif
			.dEffecdate = dEffecdate
			.nUsercode = nUsercode
		End With
		insPostVI8002 = updLifeVI8002(nOption, nTaxregime, nCapital, nYearMonth_fPay, sClient, sAct_date, dDateWork, nAFP, sFolionumber)
		
		
		If insPostVI8002 Then
			lclsPolicy_Win = New Policy_Win
			lcolAPV_origins = New ePolicy.Apv_origins
			
			'+ Se actualiza la tabla Policy_Win
			
			If lcolAPV_origins.Find(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, 0) Then
				lclsPolicy_Win.Add_PolicyWin(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nUsercode, "VI8002", "2")
			Else
				lclsPolicy_Win.Add_PolicyWin(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nUsercode, "VI8002", "1")
			End If
		End If
		
insPostVI8002_Err: 
		If Err.Number Then
			insPostVI8002 = False
		End If
		
		On Error GoTo 0
		
		'UPGRADE_NOTE: Object lclsPolicy_Win may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsPolicy_Win = Nothing
		'UPGRADE_NOTE: Object lcolAPV_origins may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lcolAPV_origins = Nothing
	End Function
	
	
	'% InsUpdApv_Origin: Realiza la actualización de la tabla
	Private Function InsUpdApv_Origin(ByVal nAction As Integer) As Boolean
		Dim lrecInsUpdApv_Origin As eRemoteDB.Execute
		
		On Error GoTo InsUpdApv_Origin_Err
		
		lrecInsUpdApv_Origin = New eRemoteDB.Execute
		
		With lrecInsUpdApv_Origin
			.StoredProcedure = "InsUpdApv_Origin"
			
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOrigin", nOrigin, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPercent", nPercent, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 5, 0, 2, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPremDeal_anu", nPremDeal_anu, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPremDeal", nPremDeal, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dNulldate", dNulldate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 1, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			InsUpdApv_Origin = .Run(False)
		End With
		
InsUpdApv_Origin_Err: 
		If Err.Number Then
			InsUpdApv_Origin = False
		End If
		
		'UPGRADE_NOTE: Object lrecInsUpdApv_Origin may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecInsUpdApv_Origin = Nothing
		
		On Error GoTo 0
	End Function
	
	'%InsValVI8002Upd: Validaciones de la transacción
    Public Function InsValVI8002Upd(ByVal sCodispl As String, ByVal sAction As String, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nOrigin As Integer, ByVal nPercent As Double, ByVal nPremDeal_anu As Double, ByVal nPayfreq As Integer, ByVal nWayPay As Integer, Optional ByVal dEffecdate As Date = #12:00:00 AM#, Optional ByVal nOptdirect As Object = Nothing) As String
        Dim lclsErrors As eFunctions.Errors
        Dim lcolAPV_origins As ePolicy.Apv_origins
        Dim lblnError As Boolean

        Dim lrecreaLifes As eRemoteDB.Execute
        Dim lstrDes As String = ""
        On Error GoTo InsValVI8002Upd_Err
        lclsErrors = New eFunctions.Errors
        lcolAPV_origins = New ePolicy.Apv_origins
        lrecreaLifes = New eRemoteDB.Execute

        With lclsErrors
            If sAction = "Add" Then
                If nOrigin <= 0 Then
                    .ErrorMessage(sCodispl, 80073)
                Else
                    If nPercent <= 0 And nPremDeal_anu <= 0 Then
                        .ErrorMessage(sCodispl, 80141)
                    End If
                    If lcolAPV_origins.Find(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nOrigin) Then
                        .ErrorMessage(sCodispl, 80140)
                    End If
                    If nPercent > 100 Then
                        .ErrorMessage(sCodispl, 1938)
                    End If
                End If
            End If

            With lrecreaLifes
                .StoredProcedure = "insvalvi8002bd2"
                .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nPremDeal_anu", nPremDeal_anu, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nOrigin", nOrigin, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("ArrayErrors", lstrDes, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 100, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Run(False)
                lstrDes = .Parameters("ArrayErrors").Value
            End With

            If Len(lstrDes) > 0 Then
                Call .ErrorMessage(sCodispl, , , , , , lstrDes)
            End If
            ' PAC (1), TRANSBANK (2), AVISO (4): DIRECTO
            If nWayPay = 1 Or nWayPay = 2 Or nWayPay = 4 Then

                ' COTIZACION VOLUNTARIA / DEPOSITO CONVENIDO
                If nOrigin = 1 Or nOrigin = 3 Then
                    .ErrorMessage(sCodispl, 80161)
                End If

                ' Descuento por planilla (3): Indirecto / Directo Empleador
            ElseIf nWayPay = 3 Then
                If nOptdirect <> 1 Then
                    ' COTIZACION VOLUNTARIA -- DEPOSITO CONVENIDO
                    If nOrigin = 1 Or nPayfreq = 1 Or nPayfreq = 2 Or nPayfreq = 3 Or nPayfreq = 6 Then
                        .ErrorMessage(sCodispl, 80161)
                    End If
                End If
            End If

            InsValVI8002Upd = .Confirm
        End With

InsValVI8002Upd_Err:
        If Err.Number Then
            InsValVI8002Upd = "InsValVI8002Upd: " & Err.Description
        End If

        'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsErrors = Nothing
        'UPGRADE_NOTE: Object lcolAPV_origins may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lcolAPV_origins = Nothing

        On Error GoTo 0
    End Function
	'%InsValVI8002: Validaciones de la transacción
    Public Function InsValVI8002(ByVal sCodispl As String, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nOption As Integer, ByVal nTaxregime As Double, ByVal nCapital As Double, ByVal nYearMonth_fPay As Integer, ByVal nYear As Integer, ByVal nMonth As Integer, ByVal dEffecdate As Date, ByVal sFolionumber As String, ByVal nDepend As Integer, ByVal nIndep As Integer, Optional ByVal nTransaction As Integer = 0, Optional ByVal nAFP As Double = 0, Optional ByVal dDate_work As Date = #12:00:00 AM#, Optional ByVal nPayfreq As Integer = 0, Optional ByVal nDirect As Short = 0, Optional ByVal nIndirect As Short = 0) As String
        Dim lclsErrors As eFunctions.Errors
        Dim lblnError As Boolean
        Dim lcolAPV_origins As Apv_origins
        Dim lstrValReq As String
        Dim lclsCapital_age As eProduct.Capital_age
        Dim lclsTab_Activelife As eProduct.Tab_ActiveLife
        Dim nModulec As Integer
        Dim nTotalDeposits As Double
        Dim nCounter As Short

        On Error GoTo InsValVI8002_Err

        lclsErrors = New eFunctions.Errors
        lcolAPV_origins = New ePolicy.Apv_origins
        lclsCapital_age = New eProduct.Capital_age
        lclsTab_Activelife = New eProduct.Tab_ActiveLife

        nModulec = IIf(nModulec = eRemoteDB.Constants.intNull, 0, nModulec)

        Call lclsTab_Activelife.Find(nBranch, nProduct, nModulec, eRemoteDB.Constants.intNull, dEffecdate)

        With lclsErrors
            ' + Si el código de GRUPO está vacío o es NULO y existe infomación en los demás campos - error # 1084
            If sCertype <> "3" Then
                If dDate_work = eRemoteDB.Constants.dtmNull Then
                    Call lclsErrors.ErrorMessage(sCodispl, 978005)
                End If
                If nAFP <= 0 Then
                    Call .ErrorMessage(sCodispl, 60213, , eFunctions.Errors.TextAlign.RigthAling, "(AFP)")
                End If
            End If
            If nOption <= 0 Then
                Call .ErrorMessage(sCodispl, 56006)
            End If
            If nTaxregime <= 0 Then
                Call .ErrorMessage(sCodispl, 60377)
            End If
            '+ Suma asegurada: Debe ser mayor a cero
            If nCapital <= 0 Then
                Call .ErrorMessage(sCodispl, 60169)
            Else
                If lclsCapital_age.insValCapital(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nCapital, nTransaction) Then
                    If (lclsCapital_age.nCapmini > 0 And nCapital < lclsCapital_age.nCapmini) Then
                        Call .ErrorMessage(sCodispl, 800032, , eFunctions.Errors.TextAlign.RigthAling, "-" & lclsCapital_age.nCapmini & "-")
                    ElseIf (lclsCapital_age.nCapmaxim > 0 And nCapital > lclsCapital_age.nCapmaxim) Then
                        Call .ErrorMessage(sCodispl, 800033, , eFunctions.Errors.TextAlign.RigthAling, "-" & lclsCapital_age.nCapmaxim & "-")
                    End If
                End If

                If nCapital < lclsTab_Activelife.nCapmin Then
                    Call .ErrorMessage(sCodispl, 60170)
                End If
            End If

            If nYear <= 0 Then
                If nTransaction <> 4 Then
                    Call .ErrorMessage(sCodispl, 60338)
                End If
            ElseIf nYear < 2000 Then
                Call .ErrorMessage(sCodispl, 80136)
            End If

            If nMonth <= 0 Then
                If nTransaction <> 4 Then
                    Call .ErrorMessage(sCodispl, 1137)
                End If
            ElseIf nMonth > 12 Then
                Call .ErrorMessage(sCodispl, 80137)
            End If
            If Not lcolAPV_origins.Find(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, 0) And nPayfreq <> 6 Then
                Call .ErrorMessage(sCodispl, 80139)
            Else
                For nCounter = 1 To lcolAPV_origins.Count
                    If lcolAPV_origins(nCounter).nPremDeal_anu > 0 Then
                        nTotalDeposits = nTotalDeposits + lcolAPV_origins(nCounter).nPremDeal_anu
                    End If
                Next nCounter
                If nTotalDeposits = 0 And nPayfreq <> 6 Then
                    Call .ErrorMessage(sCodispl, 80139)
                End If
            End If

            '+ Se valida el numero de Folio
            If nOption <> eRemoteDB.Constants.intNull Or nTaxregime <> eRemoteDB.Constants.intNull Or lcolAPV_origins.Find(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, 0) Then
                If sFolionumber = String.Empty And nTransaction <> 4 Then
                    Call .ErrorMessage(sCodispl, 80145)
                End If
            End If
            If nIndirect = 1 And nPayfreq <> 5 Then
                Call .ErrorMessage(sCodispl, 56165, , eFunctions.Errors.TextAlign.RigthAling, ", para tipo Indirecto solo es permitido frecuencia de pago mensual")
            End If

            '+ Se realizan validaciones que requieren de varias lecturas de la BD
            lstrValReq = insvalvi8002bd(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, sFolionumber, nDepend, nIndep, nTransaction)

            If lstrValReq <> String.Empty Then
                Call .ErrorMessage(sCodispl, , , , , , lstrValReq)
            End If

            InsValVI8002 = .Confirm
        End With

InsValVI8002_Err:
        If Err.Number Then
            InsValVI8002 = "InsValVI8002: " & Err.Description
        End If

        'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsErrors = Nothing
        'UPGRADE_NOTE: Object lcolAPV_origins may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lcolAPV_origins = Nothing

        On Error GoTo 0
    End Function
	Public Function Update() As Boolean
		Update = InsUpdApv_Origin(2)
	End Function
	Public Function updLifeVI8002(ByVal nOption As Integer, ByVal nTaxregime As Double, ByVal nCapital As Double, ByVal nYearMonth_fPay As Double, ByVal sClient As String, ByVal sAct_date As String, ByVal dDateWork As Date, ByVal nAFP As Short, ByVal sFolionumber As String) As Boolean
		Dim lrecupdLifeVI8002 As eRemoteDB.Execute
		
		On Error GoTo updLifeVI8002_Err
		
		lrecupdLifeVI8002 = New eRemoteDB.Execute
		
		With lrecupdLifeVI8002
			.StoredProcedure = "InsPostVI8002"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCapital", nCapital, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOption", nOption, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTaxRegime", nTaxregime, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 5, 0, 2, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nYearMonth_fpay", nYearMonth_fPay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAct_date", sAct_date, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dDateWork", dDateWork, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAfp", nAFP, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sFolionumber", sFolionumber, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			updLifeVI8002 = .Run(False)
		End With
		
updLifeVI8002_Err: 
		If Err.Number Then
			updLifeVI8002 = False
		End If
		
		'UPGRADE_NOTE: Object lrecupdLifeVI8002 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecupdLifeVI8002 = Nothing
		
		On Error GoTo 0
	End Function
	
	
	'% FindVI8002: Lee los registros de la tabla APV_Origin
	Public Function FindVI8002(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date) As Boolean
		Dim lrecreaApv_Origin_a As eRemoteDB.Execute
		
		lrecreaApv_Origin_a = New eRemoteDB.Execute
		
		On Error GoTo FindVI8002_Err
		
		With lrecreaApv_Origin_a
			.StoredProcedure = "reaVI8002"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				FindVI8002 = True
				dDependant = .FieldToClass("dDependant")
				dIndependant = .FieldToClass("dIndependant")
				nAFP = .FieldToClass("nAFP")
				sClient = .FieldToClass("sClient")
				nOption = .FieldToClass("nOption")
				nTaxregime = .FieldToClass("nTaxregime")
				nCapital = .FieldToClass("nCapital")
				nYearMonth_fPay = .FieldToClass("nYearMonth_fPay")
				sClientBos = .FieldToClass("sClientBos")
				sFolionumber = .FieldToClass("sFolionumber")
				nDirect = .FieldToClass("nDirect")
				nIndirect = .FieldToClass("nIndirect")
				.RCloseRec()
			End If
		End With
		
FindVI8002_Err: 
		If Err.Number Then
			FindVI8002 = False
		End If
		
		'UPGRADE_NOTE: Object lrecreaApv_Origin_a may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaApv_Origin_a = Nothing
		
		On Error GoTo 0
		
	End Function
	
	'% insvalvi8002bd: Se valida informacion de la BD
	Public Function insvalvi8002bd(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal sFolionumber As String, ByVal nDepend As Integer, ByVal nIndep As Integer, Optional ByVal nTransaction As Integer = 0) As String
		Dim lrecreaLifes As eRemoteDB.Execute
        Dim lstrDes As String = ""

        On Error GoTo insvalvi8002bd_Err
		
		lrecreaLifes = New eRemoteDB.Execute
		
		With lrecreaLifes
			.StoredProcedure = "insvalvi8002bd"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sFolionumber", sFolionumber, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTransaction", nTransaction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDepend", nDepend, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nIndep", nIndep, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("ArrayErrors", lstrDes, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 100, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Run(False)
			insvalvi8002bd = .Parameters("ArrayErrors").Value
		End With
		
insvalvi8002bd_Err: 
		If Err.Number Then
			insvalvi8002bd = String.Empty
		End If
		'UPGRADE_NOTE: Object lrecreaLifes may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaLifes = Nothing
		On Error GoTo 0
	End Function
End Class






