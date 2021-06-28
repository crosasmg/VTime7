Option Strict Off
Option Explicit On


Public Class Claim_cases
    Implements System.Collections.IEnumerable
    '%-------------------------------------------------------%'
    '% $Workfile:: Claim_cases.cls                          $%'
    '% $Author:: Jrengifo                                   $%'
    '% $Date:: 14-01-13 6:01                                $%'
    '% $Revision:: 3                                        $%'
    '%-------------------------------------------------------%'

    Public sRenewal As String

    Private mCol As Collection

    Private mOldClaim As Double

    '**-Defined the used variable for the OnlyDemadant
    '- Se define la variable utilizada por OnlyDemandant

    Private mblnDemandant As Boolean

    '**% OnlyDemandant: indicates that the collection is going to be in charge  just when the
    '**%                client is the claimant
    '% OnlyDemandant: indica si la colección cargará sólo los casos cuyo cliente
    '%               sea el reclamante
    Public WriteOnly Property OnlyDemandant() As Boolean
        Set(ByVal Value As Boolean)
            mblnDemandant = Value
        End Set
    End Property


    Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Claim_case
        Get
            'used when referencing an element in the collection
            'vntIndexKey contains either the Index or Key to the collection,
            'this is why it is declared as a Variant
            'Syntax: Set foo = x.Item(xyz) or Set foo = x.Item(5)
            Item = mCol.Item(vntIndexKey)
        End Get
    End Property

    Public ReadOnly Property Count() As Integer
        Get
            'used when retrieving the number of elements in the
            'collection. Syntax: Debug.Print x.Count
            Count = mCol.Count()
        End Get
    End Property

    'UPGRADE_NOTE: NewEnum property was commented out. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="B3FC1610-34F3-43F5-86B7-16C984F0E88E"'
    'Public ReadOnly Property NewEnum() As stdole.IUnknown
    'Get
    'this property allows you to enumerate
    'this collection with the For...Each syntax
    'NewEnum = mCol._NewEnum
    'End Get
    'End Property

    Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
        GetEnumerator = mCol.GetEnumerator
    End Function

    '**% Add: add a new element to the collection
    '% Add: añade un nuevo elemento a la colección
    Private Function Add(ByRef oClaim_case As Claim_case) As Claim_case
        mCol.Add(oClaim_case, "CC" & oClaim_case.nClaim & oClaim_case.nCase_num & oClaim_case.nDeman_type & oClaim_case.sClient & oClaim_case.nId)
        Add = oClaim_case
        oClaim_case = Nothing
    End Function

    '%Update: realiza las actualizaciones en la tabla "Claim_Case"
    Public Function Update() As Boolean

        Dim lclsClaim_case As Claim_case

        On Error GoTo Claim_casesUpdate_Err

        '+ Valores posibles para nStatusInstance
        '+ 0: El registro es nuevo
        '+ 1: El registro ya existe en la tabla
        '+ 2: El registro ya existe, hay que actualizarlo
        '+ 3: El registro ya existe, hay que eliminarlo
        Update = True

        For Each lclsClaim_case In mCol
            With lclsClaim_case
                If mOldClaim = VariantType.Null Then
                    mOldClaim = .nClaim
                End If

                Update = .Update_ClaimCaseGeneric
                Select Case .nStatusInstance
                    Case 0
                        .nStatusInstance = 1
                    Case 3
                        mCol.Remove(("CC" & .nClaim & .nCase_num & .nDeman_type))
                End Select
            End With
        Next lclsClaim_case

Claim_casesUpdate_Err:
        If Err.Number Then
            Update = False
        End If

    End Function

    '**% Find: find the associates claim cases
    '% Find: busca los casos asociados al siniestro
    Public Function Find(ByVal nClaim As Double, Optional ByVal lstrTypeProcess As String = "") As Boolean
        Dim lblnCharge As Boolean
        Dim lrecreaClaim_Case As eRemoteDB.Execute
        Dim lclsClaim_case As Claim_case

        lrecreaClaim_Case = New eRemoteDB.Execute

        '**+Parameters definition for the stored procedure 'insudb.reaClaim_Case'
        '+ Definición de parámetros para stored procedure 'insudb.reaClaim_Case'
        '**+ Data read on 20/08/2001 02:44:32 PM
        '+ Información leída el 08/02/2001 02:44:32 PM

        Find = False
        If nClaim <> mOldClaim Then
            With lrecreaClaim_Case
                .StoredProcedure = "reaClaim_Case"
                .Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
                .Parameters.Add("nCase_num", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
                .Parameters.Add("nDeman_type", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                If .Run Then
                    Do While Not .EOF
                        lblnCharge = True

                        If mblnDemandant And .FieldToClass("sDemandant") <> "1" Then
                            lblnCharge = False
                        End If

                        If lblnCharge Then
                            lclsClaim_case = New eClaim.Claim_case
                            lclsClaim_case.nStatusInstance = 1
                            lclsClaim_case.nClaim = nClaim
                            lclsClaim_case.nCase_num = .FieldToClass("nCase_num")
                            lclsClaim_case.nDeman_type = .FieldToClass("nDeman_type")
                            lclsClaim_case.sStaReserve = .FieldToClass("sStaReserve")
                            lclsClaim_case.nNoteDama = .FieldToClass("nNoteDama")
                            lclsClaim_case.sClaim_affe = .FieldToClass("sClaim_affe")
                            lclsClaim_case.sClient = .FieldToClass("sClient")
                            lclsClaim_case.sDemandant = .FieldToClass("sDemandant")
                            lclsClaim_case.sCliename = .FieldToClass("sCliename")
                            lclsClaim_case.nBene_type = .FieldToClass("nBene_type")
                            lclsClaim_case.sTypProcess = lstrTypeProcess
                            lclsClaim_case.sDescript = .FieldToClass("sDeman_type")
                            lclsClaim_case.sDeman_type = lclsClaim_case.sDescript
                            lclsClaim_case.nRelation = .FieldToClass("nRelation")
                            lclsClaim_case.nId = .FieldToClass("nId")
                            lclsClaim_case.sRelation = .FieldToClass("sRelation")
                            lclsClaim_case.sBene_type = .FieldToClass("sBene_type")
                            lclsClaim_case.sStacase = .FieldToClass("sStacase")
                            lclsClaim_case.sDigit = .FieldToClass("sDigit")
                            lclsClaim_case.sFirstName = .FieldToClass("sFirstName")
                            lclsClaim_case.sLastName = .FieldToClass("sLastName")
                            lclsClaim_case.sLastName2 = .FieldToClass("sLastName2")
                            lclsClaim_case.sConting = .FieldToClass("sConting")
                            lclsClaim_case.nGrowth_RateI = .FieldToClass("nGrowth_RateI")
                            lclsClaim_case.nGrowth_RateE = .FieldToClass("nGrowth_RateE")
                            lclsClaim_case.sHas_Surv_Pension_Benefs = .FieldToClass("sHas_Surv_Pension_Benefs")
                            lclsClaim_case.dSummon = .FieldToClass("dSummon")
                            lclsClaim_case.dSummon_Limit = .FieldToClass("dSummon_Limit")
                            Call Add(lclsClaim_case)
                            lclsClaim_case = Nothing
                        End If
                        .RNext()
                    Loop
                    .RCloseRec()
                    Find = True
                    mOldClaim = nClaim
                End If
            End With
        Else
            Find = True
        End If

Find_Err:
        If Err.Number Then
            Find = False
        End If
        On Error GoTo 0
        lrecreaClaim_Case = Nothing
        lclsClaim_case = Nothing
    End Function

    '% insPostSI016: se actualizan los datos asociados a la transacción
    Public Function insPostSI016(ByVal nClaim As Double, ByVal sCase_num As String, ByVal sDeman_type As String, ByVal sClaim_affe As String, ByVal nUsercode As Integer, ByVal sStatusCod As String) As Boolean
        Dim lclsRemote As eRemoteDB.Execute


        lclsRemote = New eRemoteDB.Execute

        With lclsRemote
            .StoredProcedure = "inspostSI016"
            .Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sCase_num", sCase_num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 4000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sDeman_type", sDeman_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 4000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sClaim_affe", sClaim_affe, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 4000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sTypProcess", "4", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUserCode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sStatusCod", sStatusCod, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 4000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            insPostSI016 = .Run(False)
        End With


    End Function

    '**% insValSI099: verify that all the required windows of the subsequence have a content
    '% insValSI099: se verifica que todas las ventanas requeridas de la subsecuencia tengan contenido
    Public Function InsValSI099(ByVal nClaim As Double, ByVal nCase_num As Integer, ByVal nDeman_type As Integer) As String
        Dim lclsErrors As eFunctions.Errors
        Dim lclsClaim_case As Claim_case

        On Error GoTo InsValSI099_Err

        lclsErrors = New eFunctions.Errors
        lclsClaim_case = New Claim_case

        With lclsClaim_case
            .nClaim = nClaim
            .nCase_num = nCase_num
            .nDeman_type = nDeman_type
        End With

        InsValSI099 = lclsErrors.Confirm
        lclsErrors = Nothing

InsValSI099_Err:
        If Err.Number Then
            InsValSI099 = "insvalSI099: " & Err.Description
        End If
        On Error GoTo 0
    End Function

    '**% insVerifyConten: This function verifies if all the windows have any content
    '% insVerifyConten: Esta funcion verifica si todas las ventanas tienen contenido
    Private Function insVerifyConten(ByVal nClaim As Double) As Boolean
        Dim lclsClaim_case As Claim_case
        insVerifyConten = True
        If Find(nClaim) Then
            For Each lclsClaim_case In mCol
                With lclsClaim_case
                    If Not .bFullCase Then
                        insVerifyConten = False
                        Exit For
                    End If
                End With
            Next lclsClaim_case
        Else
            insVerifyConten = False
        End If
    End Function

    Public Sub Remove(ByRef vntIndexKey As Object)
        'used when removing an element from the collection
        'vntIndexKey contains either the Index or Key, which is why
        'it is declared as a Variant
        'Syntax: x.Remove(xyz)

        mCol.Remove(vntIndexKey)
    End Sub

    Private Sub Class_Initialize_Renamed()
        'creates the collection when this class is created
        mCol = New Collection
    End Sub
    Public Sub New()
        MyBase.New()
        Class_Initialize_Renamed()
    End Sub

    Private Sub Class_Terminate_Renamed()
        'destroys collection when this class is terminated
        mCol = Nothing
    End Sub
    Protected Overrides Sub Finalize()
        Class_Terminate_Renamed()
        MyBase.Finalize()
    End Sub
End Class






