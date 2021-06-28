Option Strict Off
Option Explicit On
Public Class Life_p_speci
	'%-------------------------------------------------------%'
	'% $Workfile:: Life_p_speci.cls                         $%'
	'% $Author:: Nvaplat41                                  $%'
	'% $Date:: 9/10/03 19.01                                $%'
	'% $Revision:: 18                                       $%'
	'%-------------------------------------------------------%'
	
	'- Estructura de tabla insudb.Life_p_speci al 07-03-2002 15:18:10
	'-     Property                Type         DBType   Size Scale  Prec  Null
	Public sCertype As String ' CHAR       1    0     0    N
	Public nBranch As Integer ' NUMBER     22   0     5    N
	Public nProduct As Integer ' NUMBER     22   0     5    N
	Public nPolicy As Double ' NUMBER     22   0     10   N
	Public nCertif As Double ' NUMBER     22   0     10   N
	Public nModulec As Integer ' NUMBER     22   0     5    N
	Public nCover As Integer ' NUMBER     22   0     5    N
	Public dEffecdate As Date ' DATE       7    0     0    N
	Public nConsec As Integer ' NUMBER     22   0     5    N
	Public nAgeStart As Integer ' NUMBER     22   0     5    S
	Public nAgeEnd As Integer ' NUMBER     22   0     5    S
	Public nCapEnd As Double ' NUMBER     22   0     12   S
	Public nCapStart As Double ' NUMBER     22   0     12   S
	Public nCurrency As Integer ' NUMBER     22   0     5    S
	Public nCrthecni As Integer ' NUMBER     22   0     5    S
	Public dNulldate As Date ' DATE       7    0     0    S
	Public sSexclien As String ' CHAR       1    0     0    S
    Public nRole As Integer

    Private mlngUsercode As Integer ' NUMBER     22   0     5    N

	
	'-Variable que contiene la transaccion de poliza que se ejecuta
	Private mlngTransaction As Integer
	
	'%InsUpdlife_p_speci: Realiza la actualización de la tabla
	Private Function InsUpdlife_p_speci(ByVal nAction As Integer) As Boolean
		Dim lrecInsUpdlife_p_speci As eRemoteDB.Execute
		
		On Error GoTo InsUpdlife_p_speci_Err
		'+ Definición de store procedure InsUpdlife_p_speci al 03-07-2002
		lrecInsUpdlife_p_speci = New eRemoteDB.Execute
		With lrecInsUpdlife_p_speci
			.StoredProcedure = "insUpdlife_p_speci"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nConsec", nConsec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAgestart", nAgeStart, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAgeend", nAgeEnd, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCapend", nCapEnd, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCapstart", nCapStart, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCrthecni", nCrthecni, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dNulldate", dNulldate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sSexclien", sSexclien, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", mlngUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTransaction", mlngTransaction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCommit", 1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nRole", nRole, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

			InsUpdlife_p_speci = .Run(False)
		End With
		
		
InsUpdlife_p_speci_Err: 
		If Err.Number Then
			InsUpdlife_p_speci = False
		End If
		'UPGRADE_NOTE: Object lrecInsUpdlife_p_speci may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecInsUpdlife_p_speci = Nothing
		On Error GoTo 0
	End Function
	
	'%Add: Agrega datos de la tabla
	Public Function Add() As Boolean
		Add = InsUpdlife_p_speci(1)
	End Function
	
	'%Update: Actualiza los datos de la tabla
	Public Function Update() As Boolean
		Update = InsUpdlife_p_speci(2)
	End Function
	
	'%Delete: Borra los datos de la tabla
	Public Function Delete() As Boolean
		Delete = InsUpdlife_p_speci(3)
	End Function
	
	'%InsValVI641Upd: Validaciones de la transacción VI641, según especificaciones funcionales
    Public Function InsValVI641Upd(ByVal sCodispl As String, ByVal sCertype As String, ByVal nBranch As Integer,
                                   ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double,
                                   ByVal nModulec As Integer, ByVal nCover As Integer, ByVal dEffecdate As Date,
                                   ByVal nConsec As Integer, ByVal sSexclien As String, ByVal nAgeStart As Integer,
                                   ByVal nAgeEnd As Integer, ByVal nCapStart As Double, ByVal nCapEnd As Double,
                                   ByVal nCrthecni As Integer, ByVal nRole As Integer) As String
        Dim lclsErrors As eFunctions.Errors
        Dim lblnError As Boolean

        On Error GoTo InsValVI641Upd_Err
        lclsErrors = New eFunctions.Errors
        With lclsErrors

            'se valida el campo figura
            If nRole = 0 Or nRole = eRemoteDB.Constants.intNull Then
                .ErrorMessage(sCodispl, 2007)
                lblnError = True
            End If

            '+Se valida el campo sexo
            If sSexclien = "0" Or sSexclien = String.Empty Then
                .ErrorMessage(sCodispl, 2007)
                lblnError = True
            End If

            '+Se valida el campo criterio
            If nCrthecni = eRemoteDB.Constants.intNull Then
                .ErrorMessage(sCodispl, 11408)
                lblnError = True
            End If

            '+Se valida la edad inicial
            If nAgeStart = eRemoteDB.Constants.intNull Then
                .ErrorMessage(sCodispl, 11109)
                lblnError = True
            End If

            '+Se valida la edad final
            If nAgeEnd = eRemoteDB.Constants.intNull Then
                .ErrorMessage(sCodispl, 11110)
                lblnError = True
            ElseIf nAgeStart <> eRemoteDB.Constants.intNull Then
                If nAgeEnd < nAgeStart Then
                    .ErrorMessage(sCodispl, 11036)
                    lblnError = True
                End If
            End If

            '+Se valida el capital inicial
            If nCapStart = eRemoteDB.Constants.intNull Then
                .ErrorMessage(sCodispl, 11111)
                lblnError = True
            End If

            '+Se valida el capital final
            If nCapEnd = eRemoteDB.Constants.intNull Then
                .ErrorMessage(sCodispl, 11112)
                lblnError = True
            ElseIf nCapStart <> eRemoteDB.Constants.intNull Then
                If nCapEnd < nCapStart Then
                    .ErrorMessage(sCodispl, 11113)
                    lblnError = True
                End If
            End If

            '+Se valida que no se duplique el rango de edades
            If Not lblnError Then
                If Not InsValRange(sCertype, nBranch, nProduct,
                                   nPolicy, nCertif, nModulec,
                                   nCover, dEffecdate, nCrthecni,
                                   nConsec, sSexclien, nAgeStart,
                                   nAgeEnd, nCapStart, nCapEnd,
                                   nRole) Then
                    .ErrorMessage(sCodispl, 11138)
                End If
            End If

            InsValVI641Upd = .Confirm
        End With

InsValVI641Upd_Err:
        If Err.Number Then
            InsValVI641Upd = "InsValVI641Upd: " & Err.Description
        End If
        'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsErrors = Nothing
        On Error GoTo 0
    End Function
	
	'%InsPostVI641Upd: Actualizaciones de la transacción VI641, según especificaciones funcionales
    Public Function InsPostVI641Upd(ByVal sAction As String, ByVal sCertype As String, ByVal nBranch As Integer,
                                    ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double,
                                    ByVal nModulec As Integer, ByVal nCover As Integer, ByVal dEffecdate As Date,
                                    ByVal nConsec As Integer, ByVal dNulldate As Date, ByVal nUsercode As Integer,
                                    ByVal nTransaction As Integer, Optional ByVal nCurrency As Integer = 0,
                                    Optional ByVal sSexclien As String = "", Optional ByVal nAgeStart As Integer = 0,
                                    Optional ByVal nAgeEnd As Integer = 0, Optional ByVal nCapStart As Double = 0,
                                    Optional ByVal nCapEnd As Double = 0, Optional ByVal nCrthecni As Integer = 0,
                                    Optional ByVal nRole As Integer = 0) As Boolean
        On Error GoTo InsPostVI641Upd_Err

        With Me
            .sCertype = sCertype
            .nBranch = nBranch
            .nProduct = nProduct
            .nPolicy = nPolicy
            .nCertif = nCertif
            .nModulec = IIf(nModulec = eRemoteDB.Constants.intNull, 0, nModulec)
            .nCover = nCover
            .dEffecdate = dEffecdate
            .nConsec = nConsec
            .sSexclien = sSexclien
            .nAgeStart = nAgeStart
            .nAgeEnd = nAgeEnd
            .nCapStart = nCapStart
            .nCapEnd = nCapEnd
            .nCurrency = nCurrency
            .nCrthecni = nCrthecni
            .dNulldate = dNulldate
            .nRole = nRole
            mlngUsercode = nUsercode
            mlngTransaction = nTransaction

            Select Case sAction
                Case "Add"
                    InsPostVI641Upd = .Add
                Case "Update"
                    InsPostVI641Upd = .Update
                Case "Del"
                    InsPostVI641Upd = .Delete
            End Select

        End With
InsPostVI641Upd_Err:
        If Err.Number Then
            InsPostVI641Upd = False
        End If
        On Error GoTo 0
    End Function
	
	'%InsPostVI641: Actualizaciones de la transacción VI641, según especificaciones funcionales
    Public Function InsPostVI641(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer,
                                 ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date,
                                 ByVal nUsercode As Integer) As Boolean
        Dim lclsPolicy As ePolicy.Policy
        Dim lclsPolicy_Win As Policy_Win
        Dim lstrContent As String
        Dim ldouLegAmount As Double

        lclsPolicy = New ePolicy.Policy

        On Error GoTo InsPostVI641_Err
        lstrContent = "1"
        If Count(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate) > 0 Then
            lstrContent = "2"
        End If
        lclsPolicy_Win = New Policy_Win
        InsPostVI641 = lclsPolicy_Win.Add_PolicyWin(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nUsercode, "VI641", lstrContent)

        '+ Si se han modificado los límites de criterios para seleción del riesgo
        If InsPostVI641 And lstrContent = "2" And lclsPolicy.Find(sCertype, nBranch, nProduct, nPolicy) Then

            If lclsPolicy.sLeg = "1" Then
                ldouLegAmount = lclsPolicy.nLegAmount
                If lclsPolicy.InsCalLegAmount(sCertype, nBranch, nProduct, nPolicy, lclsPolicy.sTypenom, String.Empty, dEffecdate, eRemoteDB.Constants.intNull, String.Empty) Then

                    '+ Si el nuevo monto de Led es diferente al registrado y las coberturas son por póliza
                    '+ o grupo, se borra el Led existente y se coloca la venta de coberturas (CA014A) sin contenido.
                    If ldouLegAmount <> lclsPolicy.nLegAmount And (lclsPolicy.sTyp_module = "2" Or lclsPolicy.sTyp_module = "3") Then

                        Call UpdPolicy_nLegAmount(sCertype, nBranch, nProduct, nPolicy, 0, nUsercode)

                        InsPostVI641 = lclsPolicy_Win.Add_PolicyWin(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nUsercode, "CA014A", "1")
                    End If
                End If
            End If
        End If

InsPostVI641_Err:
        If Err.Number Then
            InsPostVI641 = False
        End If
        'UPGRADE_NOTE: Object lclsPolicy_Win may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsPolicy_Win = Nothing
        On Error GoTo 0
    End Function
	
	'%InsValRange: Valida que no se dupliquen los rangos de edad y capital
    Private Function InsValRange(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer,
                                 ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nModulec As Integer,
                                 ByVal nCover As Integer, ByVal dEffecdate As Date, ByVal nCrthecni As Integer,
                                 ByVal nConsec As Integer, ByVal sSexclien As String, ByVal nAgeStart As Integer,
                                 ByVal nAgeEnd As Integer, ByVal nCapStart As Double, ByVal nCapEnd As Double,
                                 ByVal nRole As Integer) As Boolean

        Dim lrecInsValRangeInLife_p_speci As eRemoteDB.Execute

        On Error GoTo InsValRange_Err
        '+ Definición de store procedure InsValRangeInLife_p_speci al 03-07-2002
        lrecInsValRangeInLife_p_speci = New eRemoteDB.Execute
        With lrecInsValRangeInLife_p_speci
            .StoredProcedure = "InsValRangeInLife_p_speci"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nAgestart", nAgeStart, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nAgeend", nAgeEnd, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCapstart", nCapStart, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCapend", nCapEnd, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCrthecni", nCrthecni, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nConsec", nConsec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sSexclien", sSexclien, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nExist", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nRole", nRole, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            If .Run(False) Then
                InsValRange = .Parameters("nExist").Value = 0
            End If
        End With

InsValRange_Err:
        If Err.Number Then
            InsValRange = False
        End If
        'UPGRADE_NOTE: Object lrecInsValRangeInLife_p_speci may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecInsValRangeInLife_p_speci = Nothing
        On Error GoTo 0
    End Function
	
	'%Count: Obtiene la cantidad de criterios de riesgos registrados para la póliza/certificado
    Private Function Count(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer,
                           ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date) As Integer
        Dim lrecReaLife_p_speci_count As eRemoteDB.Execute

        On Error GoTo ReaLife_p_speci_count_Err
        '+ Definición de store procedure ReaLife_p_speci_count al 07-04-2002 17:51:02
        lrecReaLife_p_speci_count = New eRemoteDB.Execute
        With lrecReaLife_p_speci_count
            .StoredProcedure = "ReaLife_p_speci_count"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCount", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run(False) Then
                Count = .Parameters("nCount").Value
            End If
        End With

ReaLife_p_speci_count_Err:
        If Err.Number Then
            Count = 0
        End If
        'UPGRADE_NOTE: Object lrecReaLife_p_speci_count may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecReaLife_p_speci_count = Nothing
        On Error GoTo 0
    End Function
	
	
	' UpdPolicy_nLegAmount : Procedimiento que realiza la actualización del Límite de Emisión Garantizada de
	'                        la póliza, actualizando nLegAmount,
    Public Sub UpdPolicy_nLegAmount(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer,
                                    ByVal nPolicy As Double, ByVal nLegAmount As Double, ByVal nUsercode As Integer)
        Dim lrecupdPolicy_nLegAmount As eRemoteDB.Execute

        On Error GoTo UpdPolicy_nLegAmount_Err

        lrecupdPolicy_nLegAmount = New eRemoteDB.Execute

        With lrecupdPolicy_nLegAmount
            .StoredProcedure = "updPolicy_nLegAmount"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nLegAmount", nLegAmount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Run(False)
        End With

UpdPolicy_nLegAmount_Err:
        'UPGRADE_NOTE: Object lrecupdPolicy_nLegAmount may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecupdPolicy_nLegAmount = Nothing
        On Error GoTo 0
    End Sub
	
	'%InitValues: Inicializa los valores de las variables publicas de la clase
	Private Sub InitValues()
		sCertype = String.Empty
		nBranch = eRemoteDB.Constants.intNull
		nProduct = eRemoteDB.Constants.intNull
		nPolicy = eRemoteDB.Constants.intNull
		nCertif = eRemoteDB.Constants.intNull
		nModulec = eRemoteDB.Constants.intNull
		nCover = eRemoteDB.Constants.intNull
		dEffecdate = eRemoteDB.Constants.dtmNull
		nConsec = eRemoteDB.Constants.intNull
		nAgeStart = eRemoteDB.Constants.intNull
		nAgeEnd = eRemoteDB.Constants.intNull
		nCapEnd = eRemoteDB.Constants.intNull
		nCapStart = eRemoteDB.Constants.intNull
		nCurrency = eRemoteDB.Constants.intNull
		nCrthecni = eRemoteDB.Constants.intNull
		dNulldate = eRemoteDB.Constants.dtmNull
		sSexclien = String.Empty
        mlngUsercode = eRemoteDB.Constants.intNull
        nRole = eRemoteDB.Constants.intNull

	End Sub
	
	'%Class_Initialize: Se ejecuta cuando se instancia la clase
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		Call InitValues()
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
End Class






