Option Strict Off
Option Explicit On
Public Class Contrproc_cover_co_gs
    Implements System.Collections.IEnumerable
    '%-------------------------------------------------------%'
    '% $Workfile:: Contrproc_Cover_co_gs.cls                $%'
    '% $Author:: Inmotion                                   $%'
    '% $Date:: 12/12/07 18.01                               $%'
    '%-------------------------------------------------------%'

    Private mCol As Collection

    '- La variable sManualMovType contiene el indicador de tipo de movimiento manual. Valores Posibles 0.-No tiene, 1.-Contratos, 3.-Facultativos
    Public sManualMovType As String


    '**% Add: Add a new element to the collection
    '% Add: añade un nuevo elemento a la colección
    Public Function Add(ByVal nCover As Integer, ByVal nBranchRei As Integer, ByVal nNumber As Integer, ByVal nType As Integer, ByVal sCoverDesc As String, ByVal sBranch_Reides As String, ByVal sDesc_Contrato As String, ByVal dDate_Contrato As Date, ByVal nQuota_Sha As Double, ByVal nCapital As Double) As Contrproc_Cover_co_g
        'Create a new object
        Dim objNewMember As ePolicy.Contrproc_Cover_co_g
        Try

            If nType = eRemoteDB.Constants.intNull Then

            End If

            objNewMember = New ePolicy.Contrproc_Cover_co_g

            'Set the properties passed into the method
            With objNewMember
                .nCover = nCover
                .sCoverDesc = sCoverDesc
                .nBranchRei = nBranchRei
                .sBranch_Reides = sBranch_Reides
                .nType = nType
                .sDesc_Contrato = sDesc_Contrato
                .dDate_Contrato = dDate_Contrato
                .nShare = nQuota_Sha
                .nCapital = nCapital

                If nNumber <> 0 And nNumber <> eRemoteDB.Constants.intNull Then
                    .nNumber = nNumber
                End If
            End With

            mCol.Add(objNewMember, "A" & nCover & nBranchRei & nType)

            'Return the object created
            Return objNewMember
        Catch ex As Exception

        Finally
            objNewMember = Nothing
        End Try
    End Function


    '**% Add: Add a new element to the collection
    '% Add: añade un nuevo elemento a la colección
    Public Function Add_Facult(ByVal nCover As Integer, ByVal nType As Integer, ByVal nCompany As Integer, ByVal sCliename As String, ByVal nClasific As Integer, ByVal sDesc_Clasif As String, ByVal nCapital As Double, ByVal nShare As Double, ByVal nCommissi As Double, ByVal nReser_rate As Double, ByVal nInter_rate As Double, ByVal dAcceDate As Date) As Contrproc_Cover_co_g

        'Create a new object

        Dim objNewMember As ePolicy.Contrproc_Cover_co_g

        objNewMember = New ePolicy.Contrproc_Cover_co_g

        'Set the properties passed into the method
        With objNewMember
            .nCover = nCover
            .nType = nType
            .nCompany = nCompany
            .sCliename = sCliename
            .nClasific = nClasific
            .sDesc_Clasif = sDesc_Clasif
            .nCapital = nCapital
            .nShare = nShare
            .nCommissi = nCommissi
            .nReser_rate = nReser_rate
            .nInter_rate = nInter_rate
            .dAcceDate = dAcceDate
        End With

        mCol.Add(objNewMember, "A" & nCover & nType & nCompany)

        'Return the object created
        Add_Facult = objNewMember
        'UPGRADE_NOTE: Object objNewMember may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        objNewMember = Nothing
    End Function


    '%Find: Lee los contratos de reaseguro asociados a las coberturas de la poliza
    Public Function Find(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCover As Integer, ByVal dEffecdate As Date) As Boolean
        Dim lreaContrproc_Cover_co_g As eRemoteDB.Execute
        Dim lclsContrproc_Cover_co_g As Contrproc_Cover_co_g

        On Error GoTo Find_Err

        lreaContrproc_Cover_co_g = New eRemoteDB.Execute

        With lreaContrproc_Cover_co_g
            .StoredProcedure = "reacontrproc_cover_co_g"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run Then
                Find = True
                Do While Not .EOF

                    lclsContrproc_Cover_co_g = New Contrproc_Cover_co_g
                    Call Add(.FieldToClass("ncover"), .FieldToClass("nBranchrei"), .FieldToClass("nNumber"), .FieldToClass("nType"), .FieldToClass("sCoverDesc"), .FieldToClass("sBranch_reiDes"), .FieldToClass("sDesc_Contrato"), .FieldToClass("dDate_Contrato"), .FieldToClass("nQuota_Sha"), .FieldToClass("nCapital"))

                    'UPGRADE_NOTE: Object lclsContrproc_Cover_co_g may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
                    lclsContrproc_Cover_co_g = Nothing
                    .RNext()
                Loop
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
        'UPGRADE_NOTE: Object lreaContrproc_Cover_co_g may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lreaContrproc_Cover_co_g = Nothing
    End Function

    '%Find: Lee los contratos de reaseguro asociados a las coberturas de la poliza
    Public Function Find_Facultativo(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nCover As Integer, ByVal dEffecdate As Date) As Boolean
        Dim lreaContrproc_Cover_co_g As eRemoteDB.Execute
        Dim lclsContrproc_Cover_co_g As Contrproc_Cover_co_g

        On Error GoTo Find_Err

        lreaContrproc_Cover_co_g = New eRemoteDB.Execute

        With lreaContrproc_Cover_co_g
            .StoredProcedure = "reafacultativos"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run Then
                Find_Facultativo = True
                Do While Not .EOF

                    lclsContrproc_Cover_co_g = New Contrproc_Cover_co_g
                    Call Add_Facult(.FieldToClass("nCover"), .FieldToClass("nType"), .FieldToClass("nCompany"), .FieldToClass("sCliename"), .FieldToClass("nClasific"), .FieldToClass("sDesc_Clasif"), .FieldToClass("nCapital"), .FieldToClass("nShare"), .FieldToClass("nCommissi"), .FieldToClass("nReser_rate"), .FieldToClass("nInter_rate"), .FieldToClass("dAcceDate"))

                    'UPGRADE_NOTE: Object lclsContrproc_Cover_co_g may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
                    lclsContrproc_Cover_co_g = Nothing
                    .RNext()
                Loop
                .RCloseRec()
            Else
                Find_Facultativo = False
            End If
        End With

Find_Err:
        If Err.Number Then
            Find_Facultativo = False
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lreaContrproc_Cover_co_g may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lreaContrproc_Cover_co_g = Nothing
    End Function


    '***Item: Returns an element of the collection (according to the index)
    '*Item: Devuelve un elemento de la colección (segun índice)
    Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Contrproc_Cover_co_g
        Get
            'Used when referencing an element in the collection.
            'vntIndexKey contains either the Index or Key to the collection,
            'this is why it is declared as a Variant
            'Syntax: Set foo = x.Item(xyz) or Set foo = x.Item(5)
            Item = mCol.Item(vntIndexKey)
        End Get
    End Property

    '***Count: Returns the number of elements that the collection has
    '*Count: Devuelve el número de elementos que posee la colección
    Public ReadOnly Property Count() As Integer
        Get
            'Used when retrieving the number of elements in the collection.
            'Syntax: Debug.Print x.Count
            Count = mCol.Count()
        End Get
    End Property

    '***NewEnum: Enumerates the collection for use in a For Each...Next loop
    '*NewEnum: Permite enumerar la colección para utilizarla en un ciclo For Each...
    'UPGRADE_NOTE: NewEnum property was commented out. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="B3FC1610-34F3-43F5-86B7-16C984F0E88E"'
    'Public ReadOnly Property NewEnum() As stdole.IUnknown
    'Get
    'This property allows you to enumerate this collection with the For...Each syntax
    'NewEnum = mCol._NewEnum
    'End Get
    'End Property

    Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
        'UPGRADE_TODO: Uncomment and change the following line to return the collection enumerator. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="95F9AAD0-1319-4921-95F0-B9D3C4FF7F1C"'
        GetEnumerator = mCol.GetEnumerator
    End Function

    '**%Remove: Deletes an element from the collection
    '%Remove: Elimina un elemento de la colección
    Public Sub Remove(ByRef vntIndexKey As Object)
        'Used when removing an element from the collection.
        'vntIndexKey contains either the Index or Key, which is why
        'it is declared as a Variant
        'Syntax: x.Remove(xyz)
        mCol.Remove(vntIndexKey)
    End Sub

    '**%Class_Initialize: Controls the creation of an instance of the collection
    '%Class_Initialize: Controla la creación de una instancia de la colección
    'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
    Private Sub Class_Initialize_Renamed()
        'Creates the collection when this class is created
        mCol = New Collection
        sManualMovType = "1"
    End Sub
    Public Sub New()
        MyBase.New()
        Class_Initialize_Renamed()
    End Sub

    '**%Class_Terminate: Controls the destruction of an instance of the collection
    '%Class_Terminate: Controla la destrucción de una instancia de la colección
    'UPGRADE_NOTE: Class_Terminate was upgraded to Class_Terminate_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
    Private Sub Class_Terminate_Renamed()
        'Destroys collection when this class is terminated
        'UPGRADE_NOTE: Object mCol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        mCol = Nothing
    End Sub
    Protected Overrides Sub Finalize()
        Class_Terminate_Renamed()
        MyBase.Finalize()
    End Sub
End Class






