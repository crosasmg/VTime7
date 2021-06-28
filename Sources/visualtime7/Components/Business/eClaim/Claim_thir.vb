Option Strict Off
Option Explicit On

Public Class Claim_thir
    '%-------------------------------------------------------%'
    '% $Workfile:: Claim_thir.cls                           $%'
    '% $Author:: Nvaplat60                                  $%'
    '% $Date:: 14/10/03 17.02                               $%'
    '% $Revision:: 12                                       $%'
    '%-------------------------------------------------------%'

    '-Se definen las propiedades principales de la clase correspondientes a la tabla claim_thir (13/01/2001)
    Public nClaim As Double
    Public nCase_num As Integer
    Public nDeman_type As Integer
    Public nBlame As Integer
    Public sLicence_ty As String
    Public sRegist As String
    Public sThir_claim As String
    Public nThir_comp As Integer
    Public sThir_polic As String
    Public nProvider As Integer
    Public nNoteAgree As Double
    Public nNoteThir As Double
    Public sMotor As String
    Public sChassis As String
    Public sRecov_ind As String
    Public nRecov_Per As Double
    Public nUsercode As Integer

    '**-Auxiliaries variables
    '-Variables auxiliares
    Public sColor As String
    Public sVehCode As String
    Public sDesMark As String
    Public nVehBrand As String
    Public sVehModel As String
    Public sDescProvid As String
    Public sCodispl As String
    Public sDigit As String
    Public nYear As Integer


    Private Enum eComponent
        eNone = 0
        eregister = 1
        emotor = 2
        echassis = 3
    End Enum

    '**-Data from the note
    '- Datos de la nota.
    Public sDescriptNote As String


    '**%ValClientCLaim_thir: The objective of this function is to validate that a record exist into the Claim_thir table. If it loose the client it will search for it or verify
    '**%if the table has the relative info about the claim case in treatment.
    '%ValClientClaim_thir: El objetivo de esta función es validar si existe un registro en la tabla Claim_thir. Si se le pasa el Cliente busca ese determinado cliente, sino verifica
    '%si la tabla tiene información relacionada al caso del siniestro en tratamiento.
    Public Function ValClientClaim_thir(ByVal nClaim As Double, ByVal nCase_num As Integer, ByVal nDeman_type As Integer, Optional ByVal sClient As String = "") As Boolean
        '**Define the variable lrecClaimBenef to execute the stored procedure
        'Se define la variable lrecClaimBenef para ejecutar el stored procedure
        Dim lrecClaim_thir As eRemoteDB.Execute

        Static lblnRead As Boolean
        Static llngOldClaim As Double
        Static lintOldCase_num As Integer
        Static lintOldDeman_type As Integer
        Static lstrOldClient As String

        On Error GoTo ValClientClaim_thir_Err

        llngOldClaim = nClaim
        lintOldCase_num = nCase_num
        lintOldDeman_type = nDeman_type
        If sClient <> String.Empty Then
            lstrOldClient = sClient
        End If

        lrecClaim_thir = New eRemoteDB.Execute
        With lrecClaim_thir
            .StoredProcedure = "valClientClaim_thir"
            .Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCase_num", nCase_num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nDeman_type", nDeman_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            If sClient <> String.Empty Then
                .Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            Else
                'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
                .Parameters.Add("sClient", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            End If

            If .Run Then
                If .FieldToClass("lCount") > 0 Then
                    lblnRead = True
                Else
                    lblnRead = False
                End If
                .RCloseRec()
            End If

        End With

        ValClientClaim_thir = lblnRead

ValClientClaim_thir_Err:
        If Err.Number Then
            ValClientClaim_thir = False
        End If
        On Error GoTo 0
        lrecClaim_thir = Nothing
    End Function

    '**%Find: Find the data of the Claim_thir table associated with the claim
    '%Find: Busca los datos de la tabla Claim_thir asociados al siniestro
    Public Function Find(ByVal nClaim As Double, ByVal nCase_num As Integer, ByVal nDeman_type As Integer) As Boolean

        Dim lrecreaClaim_thir As eRemoteDB.Execute

        On Error GoTo Find_Err

        lrecreaClaim_thir = New eRemoteDB.Execute

        '**Parameters definition for the stored procedure 'reaClaim_thir'
        'Definición de parámetros para stored procedure 'reaClaim_thir'
        With lrecreaClaim_thir
            .StoredProcedure = "reaClaim_thir"
            .Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCase_num", nCase_num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nDeman_type", nDeman_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run Then
                nBlame = .FieldToClass("nBlame")
                sLicence_ty = .FieldToClass("sLicense_ty")
                sRegist = .FieldToClass("sRegist")
                sDigit = .FieldToClass("sDigit")
                sThir_claim = .FieldToClass("sThir_claim")
                nThir_comp = .FieldToClass("nThir_comp")
                sThir_polic = .FieldToClass("sThir_polic")
                nNoteAgree = .FieldToClass("nNoteAgree")
                nNoteThir = .FieldToClass("nNoteThir")
                sMotor = .FieldToClass("sMotor")
                sChassis = .FieldToClass("sChassis")
                sColor = .FieldToClass("sColor")
                nProvider = .FieldToClass("nProvider")
                sVehCode = .FieldToClass("sVehcode")
                sDesMark = .FieldToClass("sDesMark")
                sVehModel = .FieldToClass("sVehModel")
                sDescProvid = .FieldToClass("sDescProvid")
                sRecov_ind = .FieldToClass("sRecov_Ind")
                nRecov_Per = .FieldToClass("nRecov_Per")
                sDescriptNote = .FieldToClass("tDs_Text")
                nYear = .FieldToClass("nYear")

                Dim objTab_au_veh As New eBranches.Tab_au_veh
                If objTab_au_veh.Find(sVehCode) Then
                    nVehBrand = objTab_au_veh.nVehBrand
                End If


                Find = True
                .RCloseRec()
            Else
                Find = False
            End If
        End With

Find_Err:
        If Err.Number Then
            Find = False
        End If
        On Error GoTo 0
        lrecreaClaim_thir = Nothing
    End Function


    '**%Update: This routine is in charge to create or updated the records in the intermediaries table
    '**%        in a claim
    '%Update: Esta rutina se encarga de crear o actualizar los registros en la tabla de terceros
    '%        en un siniestro
    Public Function Update(ByVal nClaim As Double, ByVal nCase_num As Integer, ByVal nDeman_type As Integer, ByVal nBlame As Integer, ByVal sLicence_ty As Integer, ByVal sRegist As String, ByVal sThir_claim As String, ByVal nThir_comp As Integer, ByVal sThir_polic As String, ByVal nProvider As Integer, ByVal nNoteAgree As Double, ByVal sMotor As String, ByVal sChassis As String, ByVal sRecov_ind As String, ByVal nRecov_Per As Double, ByVal sColor As String, ByVal sVehCode As String, ByVal sDigit As String, ByVal nUsercode As Integer) As Boolean
        Dim lrecinsUpdClaim_thir As eRemoteDB.Execute

        On Error GoTo Update_Err

        lrecinsUpdClaim_thir = New eRemoteDB.Execute

        '**+ Parameters definition for the stored procedure 'insudb.insUpdClaim_thir'
        '+ Definición de parámetros para stored procedure 'insudb.insUpdClaim_thir'

        With lrecinsUpdClaim_thir
            .StoredProcedure = "insUpdClaim_thir"
            .Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCase_num", nCase_num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nDeman_type", nDeman_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sChassis", sChassis, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 40, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sMotor", sMotor, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 40, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBlame", nBlame, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sLicence_ty", sLicence_ty, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sRegist", sRegist, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sThir_claim", sThir_claim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nThir_comp", nThir_comp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sThir_polic", sThir_polic, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProvider", nProvider, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nNoteAgree", nNoteAgree, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sRecov_Ind", sRecov_ind, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nRecov_Per", nRecov_Per, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sColor", sColor, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 15, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sVehCode", IIf(sVehCode = "0", String.Empty, sVehCode), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 6, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sDigit", sDigit, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            Update = .Run(False)
        End With

Update_Err:
        If Err.Number Then
            Update = False
        End If
        On Error GoTo 0
        lrecinsUpdClaim_thir = Nothing
    End Function

    '**%insValSI019: This function makes the correpondent validations with the window fields
    '%insValSI019: En esta funcion se realizan las validaciones correspondientes a los campos
    '%de la ventana.
    Public Function insValSI019(ByVal sCodispl As String, 
                                ByVal sRegister As String, 
                                ByVal sChassis As String, 
                                ByVal sMotor As String, 
                                ByVal optLicense As Integer, 
                                ByVal sVehCode As String, 
                                ByVal nProvider As Integer, 
                                ByVal nClaim As Double, 
                                ByVal nCase_num As Integer, 
                                ByVal nDeman_type As Integer, 
                                ByVal nBlame As Integer, 
                                ByVal sRecov_ind As String, 
                                ByVal nRecov_Per As Double,
                                ByVal sBrancht As String,
                                ByVal sDigit As String) As String
        Dim lerrTime As eFunctions.Errors
        Dim lclsTab_Provider As Tab_Provider
        Dim lclsAuto_db As Object
        Dim lclsTab_au_veh As Object
        Dim lstrSep As String
        Dim lstrError As String = ""
        Dim lintExists As Integer
        Dim lclsAuto As Object
        Dim lclsClaim As New eClaim.Claim

        On Error GoTo insValSI019_Err

        lstrSep = "||"

        lintExists = 0

        lerrTime = New eFunctions.Errors
        lclsAuto_db = eRemoteDB.NetHelper.CreateClassInstance("eBranches.Auto_db")
        lclsAuto = eRemoteDB.NetHelper.CreateClassInstance("ePolicy.Automobile")

        '**+Validation of the sRegister field
        '+Validación del campo sRegister
        If sRegister = String.Empty Then
            'Call lerrTime.ErrorMessage(sCodispl, 3121)
            lstrError = lstrError & lstrSep & "3121"
        Else
            If Not lclsClaim.Find(nClaim) Then
                Throw New Exception("El siniestro " & nClaim & " no existe")
            End If

            If sBrancht <> "6" Then
                If lclsAuto.Find(lclsClaim.sCertype, lclsClaim.nBranch, lclsClaim.nProduct, lclsClaim.nPolicy, lclsClaim.nCertif, lclsClaim.dOccurdat) _
                    AndAlso lclsAuto.sRegist.ToUpper().Trim() = sRegister.ToUpper().Trim() Then
                    lstrError = lstrError & lstrSep & "90000078"
                End If
            End If

            If lclsAuto_db.Find_AutoDB_Exists(eComponent.eregister, optLicense, sRegister) Then
                If lclsAuto_db.nVestatus <> 1 And lintExists = 0 Then
                    'Call lerrTime.ErrorMessage(sCodispl, 3839)
                    lstrError = lstrError & lstrSep & "3839"
                    lintExists = 1
                End If
            End If
        End If

        ''**+Validation of the field sChassis
        ''+Validación del campo sChassis
        If sChassis <> String.Empty Then
            If lclsAuto_db.Find_AutoDB_Exists(eComponent.echassis, optLicense, sChassis) Then
                If lclsAuto_db.nVestatus <> 1 And lintExists = 0 Then
                    'Call lerrTime.ErrorMessage(sCodispl, 3839)
                    lstrError = lstrError & lstrSep & "3839"
                    lintExists = 1
                End If
                If sRegister <> lclsAuto_db.sRegist Then
                    'Call lerrTime.ErrorMessage(sCodispl, 60510)
                    lstrError = lstrError & lstrSep & "60510"
                End If
            End If
        End If

        ''**+Validation of the sMotor field
        ''+Validación del campo sMotor
        If sMotor <> String.Empty Then
            If lclsAuto_db.Find_AutoDB_Exists(eComponent.emotor, optLicense, sMotor) Then
                If lclsAuto_db.nVestatus <> 1 And lintExists = 0 Then
                    'Call lerrTime.ErrorMessage(sCodispl, 3839)
                    lstrError = lstrError & lstrSep & "3839"
                End If

                If sRegister <> lclsAuto_db.sRegist Then
                    'Call lerrTime.ErrorMessage(sCodispl, 60511)
                    lstrError = lstrError & lstrSep & "60511"
                End If

            End If
        End If

        '**+Validation of the field optLincense
        '+Validación del campo optLicence
        If optLicense = 2 Then
            If sChassis = String.Empty Then
                'Call lerrTime.ErrorMessage(sCodispl, 3116)
                lstrError = lstrError & lstrSep & "3116"
            End If

            If sMotor = String.Empty Then
                'Call lerrTime.ErrorMessage(sCodispl, 3850)
                lstrError = lstrError & lstrSep & "3850"
            End If
        End If

        '**+validation of the sVehCode field
        '+Validación del campo sVehCode
        If sVehCode = String.Empty Then
            'Call lerrTime.ErrorMessage(sCodispl, 3380)
            lstrError = lstrError & lstrSep & "3380"
        End If

        '**+Validation of the nProvider field
        '+Validación del campo nProvider

        If nProvider <> eRemoteDB.Constants.intNull Then
            lclsTab_Provider = New Tab_Provider
            If Not lclsTab_Provider.ValTab_provider(Tab_Provider.eProvider.clngWorksh, nProvider) Then
                'Call lerrTime.ErrorMessage(sCodispl, 4119)
                lstrError = lstrError & lstrSep & "4119"
            ElseIf Not lclsTab_Provider.ValProviderCase(nClaim, nCase_num, nDeman_type, Claim_case.eClaimRole.clngClaimRWorkShop, nProvider, Tab_Provider.eProvider.clngWorksh) Then
                'Call lerrTime.ErrorMessage(sCodispl, 4336)
                lstrError = lstrError & lstrSep & "4336"
            End If
            lclsTab_Provider = Nothing
        End If

        '+Validación del campo Responsabilidad del tercero
        If nBlame = eRemoteDB.Constants.intNull Then
            'Call lerrTime.ErrorMessage(sCodispl, 4126)
            lstrError = lstrError & lstrSep & "4126"
        End If

        '+Validación del campo probabilidad de recupero
        If sRecov_ind = "1" And nRecov_Per = eRemoteDB.Constants.intNull Then
            'Call lerrTime.ErrorMessage(sCodispl, 60451)
            lstrError = lstrError & lstrSep & "60451"
        End If

        If optLicense = "1" And String.IsNullOrEmpty(sDigit.Trim()) Then
            lstrError = lstrError & lstrSep & "7816"
        End If

        'insValSI019 = lerrTime.Confirm
        If lstrError <> String.Empty Then
            lstrError = Mid(lstrError, 3)
            lerrTime.ErrorMessage(sCodispl, , , , , , lstrError)
            insValSI019 = lerrTime.Confirm
        End If

insValSI019_Err:
        If Err.Number Then
            insValSI019 = ""
            insValSI019 = insValSI019 & Err.Description
        End If
        On Error GoTo 0
        lerrTime = Nothing
        lclsAuto_db = Nothing
        lclsAuto = Nothing
    End Function

    '%insPostSI019:Esta función se encarga de realizar el impacto de los datos en la tabla claim_thir
    Public Function insPostSI019(ByVal sCodispl As String, 
                                 ByVal nClaim As Double, 
                                 ByVal nCase_num As Integer, 
                                 ByVal nDeman_type As Integer, 
                                 ByVal nBlame As Integer, 
                                 ByVal sLicence_ty As Integer, 
                                 ByVal sRegist As String, 
                                 ByVal sThir_claim As String, 
                                 ByVal nThir_comp As Integer, 
                                 ByVal sThir_polic As String, 
                                 ByVal nProvider As Integer, 
                                 ByVal nNoteAgree As Double, 
                                 ByVal sMotor As String, 
                                 ByVal sChassis As String, 
                                 ByVal sRecov_ind As String, 
                                 ByVal nRecov_Per As Double, 
                                 ByVal sColor As String, 
                                 ByVal sVehCode As String, 
                                 ByVal sDigit As String, 
                                 ByVal nUsercode As Integer, 
                                 ByVal nVehBrand As Integer, 
                                 ByVal sVehModel As String, 
                                 ByVal nYear As Integer) As Boolean
        Dim lclsCases_win As eClaim.Cases_win
        Dim lclsAuto_db As ePolicy.Auto_db
        On Error GoTo insPostSI019_Err
        lclsAuto_db = New ePolicy.Auto_db
        insPostSI019 = False


        With lclsAuto_db
            .sLicense_ty = sLicence_ty
            .sRegist = sRegist
            .sChassis = sChassis
            .sMotor = sMotor
            '.sClient = lclsPolicy.SCLIENT
            .sColor = sColor
            '.sVeh_own = lclsPolicy.SCLIENT
            .sVehcode = sVehCode
            .nVestatus = 1
            '        .nNoteNum = nNoteNum
            .nUsercode = nUsercode
            .nYear = nYear
            .nVehBrand = nVehBrand
            ' .sVehModel = sVehModel
            .nGroupVeh = 99999
            .nVehType = 99999
            .nYear = nYear
            '.nVehType = nVehType
            '        .nAnualKm = nAnualKm
            '        .nActualKm = nActualKm
            '        .nKeepVeh = nKeepVeh
            '        .nRoadType = nRoadType
            '        .nIndLaw = nIndLaw
            '        .nFuelType = nFuelType
            '        .nIndAlarm = nIndAlarm
            .sDigit = sDigit
            .nLic_special = 1
            '.nGroupVeh = nGroupVeh

            If Not .Exist_db1(sLicence_ty, sRegist) Then
                Call .Add()
            End If
        End With



        If Update(nClaim, nCase_num, nDeman_type, nBlame, CInt(Trim(CStr(sLicence_ty))), Trim(sRegist), sThir_claim, nThir_comp, sThir_polic, nProvider, nNoteAgree, Trim(sMotor), Trim(sChassis), sRecov_ind, nRecov_Per, sColor, sVehCode, Trim(sDigit), nUsercode) Then
            lclsCases_win = New eClaim.Cases_win
            insPostSI019 = lclsCases_win.Add_Cases_win(nClaim, nCase_num, nDeman_type, sCodispl, "2", nUsercode)
            lclsCases_win = Nothing
        End If

insPostSI019_Err:
        If Err.Number Then
            insPostSI019 = False
        End If
        On Error GoTo 0
    End Function
End Class






