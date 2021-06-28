Option Strict Off
Option Explicit On
Public Class Agreement_pols
    Implements System.Collections.IEnumerable
    '%-------------------------------------------------------%'
    '% $Workfile:: Agreement_pols.cls                          $%'
    '% $Author:: Nvaplat41                                  $%'
    '% $Date:: 9/10/03 19.01                                $%'
    '% $Revision:: 13                                       $%'
    '%-------------------------------------------------------%'

    '-Local variable to hold collection
    Private mCol As Collection



    '**%Add: Add a new instance of the benefit class to the collection
    '%Add: Añade una nueva instancia de la clase Agreement_pol a la colección
    Public Function Add(ByVal lclsAgreement_pol As Agreement_pol) As Agreement_pol
        With lclsAgreement_pol
            mCol.Add(lclsAgreement_pol, .sCertype & .nBranch & .nProduct & .nPolicy & .nCertif & .sClient & .dEffecdate.ToString("yyyyMMdd") & .nCod_Agree & .sCliename & .sDigit)
        End With

        Add = lclsAgreement_pol
        'UPGRADE_NOTE: Object lclsAgreement_pol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsAgreement_pol = Nothing
    End Function

    '**%Find: This method fills the collection with records from the table "Agreement_pol" returning TRUE or FALSE
    '**%depending on the existence of the records
    '%Find: Este metodo carga la coleccion de elementos de la tabla "Agreement_pol" devolviendo Verdadero o
    '%falso, dependiendo de la existencia de los registros.
    Public Function Find(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, Optional ByVal nAction As Integer = vbNull, Optional ByVal nUsercode As Double = vbNull, Optional ByVal lblnFind As Boolean = False) As Boolean
        Dim lrecReaAgreement_pol As eRemoteDB.Execute
        Dim lclsAgreement_pol As Agreement_pol
        Dim llngIndex As Integer

        On Error GoTo Find_Err

        lrecReaAgreement_pol = New eRemoteDB.Execute
        With lrecReaAgreement_pol
            .StoredProcedure = "insAgreement_polPKG.reaAgreement_pol"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sClient", vbNullString, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCod_Agree", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run Then
                llngIndex = 0
                Do While Not .EOF
                    lclsAgreement_pol = New Agreement_pol
                    lclsAgreement_pol.sClient = .FieldToClass("sClient")
                    lclsAgreement_pol.nCod_Agree = .FieldToClass("nCod_Agree")
                    lclsAgreement_pol.sCliename = .FieldToClass("sCliename")
                    lclsAgreement_pol.sDigit = .FieldToClass("sDigit")
                    Call Add(lclsAgreement_pol)
                    lclsAgreement_pol = Nothing
                    llngIndex = llngIndex + 1
                    .RNext()
                Loop
                .RCloseRec()
                Find = True
            End If
        End With

Find_Err:
        If Err.Number Then
            Find = False
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lrecReaAgreement_pol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecReaAgreement_pol = Nothing
        'UPGRADE_NOTE: Object lclsAgreement_pol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsAgreement_pol = Nothing
    End Function

    '***Item: Returns a element of the collection (according Index)
    '*Item: Devuelve un elemento de la colección (segun índice)
    Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Agreement_pol
        Get
            Item = mCol.Item(vntIndexKey)
        End Get
    End Property

    '***Count: Returns the number of the element the collection has
    '*Count: Devuelve el número de elementos que posee la colección
    Public ReadOnly Property Count() As Integer
        Get
            Count = mCol.Count()
        End Get
    End Property

    '***NewEnum: Enumerates the collection for use in a For Each...Next loop
    '*NewEnum: Permite enumerar la colección para utilizarla en un ciclo For Each... Next
    'UPGRADE_NOTE: NewEnum property was commented out. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="B3FC1610-34F3-43F5-86B7-16C984F0E88E"'
    'Public ReadOnly Property NewEnum() As stdole.IUnknown
    'Get
    'NewEnum = mCol._NewEnum
    'End Get
    'End Property

    Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
        'UPGRADE_TODO: Uncomment and change the following line to return the collection enumerator. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="95F9AAD0-1319-4921-95F0-B9D3C4FF7F1C"'
        GetEnumerator = mCol.GetEnumerator
    End Function

    '**%Remove: Delete the element of the collection
    '%Remove: Elimina un elemento de la colección
    Public Sub Remove(ByRef vntIndexKey As Object)
        mCol.Remove(vntIndexKey)
    End Sub

    '**% Class_Initialize: Control the creation of a collection instance
    '% Class_Initialize: Controla la creación de una instancia de la colección
    'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
    Private Sub Class_Initialize_Renamed()
        mCol = New Collection
    End Sub
    Public Sub New()
        MyBase.New()
        Class_Initialize_Renamed()
    End Sub

    '**% Class_Terminate: Control the destruction of the collection instance
    '% Class_Terminate: Controla la destrucción de una instancia de la colección
    'UPGRADE_NOTE: Class_Terminate was upgraded to Class_Terminate_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
    Private Sub Class_Terminate_Renamed()
        'UPGRADE_NOTE: Object mCol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        mCol = Nothing
    End Sub
    Protected Overrides Sub Finalize()
        Class_Terminate_Renamed()
        MyBase.Finalize()
    End Sub
End Class






