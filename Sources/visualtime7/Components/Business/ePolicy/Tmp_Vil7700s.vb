Option Strict Off
Option Explicit On
Public Class Tmp_Vil7700s
    Implements System.Collections.IEnumerable
    '**+Objective: Collection that supports the class 'tran_way'.
    '**+Version: $$Revision: 2 $
    '+Objetivo: Colección que le da soporte a la clase 'tran_way'.
    '+Version: $$Revision: 2 $
    'variable local para contener colección
    Private mCol As Collection

    Public Function Add(ByVal objNewMember As Tmp_Vil7700, Optional ByVal sKey As String = "") As Tmp_Vil7700
        'crear un nuevo objeto
        'Set objNewMember = New Tmp_VIL7700
        'establecer las propiedades que se transfieren al método
        If Len(sKey) = 0 Then
            mCol.Add(objNewMember)
        Else
            mCol.Add(objNewMember, sKey)
        End If

        'devolver el objeto creado
        Add = objNewMember
        objNewMember = Nothing
    End Function

    '*** Item: takes one element from the collection
    '* Item: toma un elemento de la colección
    Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Tmp_Vil7700
        Get
            Item = mCol.Item(vntIndexKey)
        End Get
    End Property

    '***Count: Returns the number of elements that the collection has
    '*Count: Devuelve el número de elementos que posee la colección
    Public ReadOnly Property Count() As Integer
        Get
            '**+used when retrieving the number of elements in the
            '**+collection. Syntax: Debug.Print x.Count
            Count = mCol.Count()
        End Get
    End Property

    Public Sub Remove(ByVal vntIndexKey As Object)
        'se usa al quitar un elemento de la colección
        'vntIndexKey contiene el índice o la clave, por lo que se
        'declara como un Variant
        'Sintaxis: x.Remove(xyz)
        mCol.Remove(vntIndexKey)
    End Sub

    Private Sub Class_Initialize_Renamed()
        On Error GoTo ErrorHandler
        mCol = New Collection

        Exit Sub
ErrorHandler:
        'UPGRADE_NOTE: Object mCol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        mCol = Nothing
    End Sub

    Public Sub New()
        MyBase.New()
        Class_Initialize_Renamed()
    End Sub

    '%Class_Terminate: Controla la destrucción de una instancia de la colección'**%Class_Terminate: Controls the destruction of an instance of the collection
    '%Class_Terminate: Controla la destrucción de una instancia de la colección
    'UPGRADE_NOTE: Class_Terminate was upgraded to Class_Terminate_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
    Private Sub Class_Terminate_Renamed()
        'destroys collection when this class is terminated
        'UPGRADE_NOTE: Object mCol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        mCol = Nothing
    End Sub

    Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
        'UPGRADE_TODO: Uncomment and change the following line to return the collection enumerator. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="95F9AAD0-1319-4921-95F0-B9D3C4FF7F1C"'
        GetEnumerator = mCol.GetEnumerator
    End Function
    'actlual
    '%Find: Lee los datos de la tabla Tmp_VIL7700 la transacción VIL7700
    '------------------------------------------------------------------
    Public Function Find(ByVal sKey_aux As String, Optional ByVal lblnAll As Boolean = True) As Boolean
        '------------------------------------------------------------------------------
        Dim lrecreaTmp_VIL7700 As eRemoteDB.Execute
        Dim lclsTmp_VIL7700 As Tmp_Vil7700

        On Error GoTo Find_Err
        lrecreaTmp_VIL7700 = New eRemoteDB.Execute
        With lrecreaTmp_VIL7700
            .StoredProcedure = "reaTmp_VIL7700"
            .Parameters.Add("SKEY", sKey_aux, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run Then
                Find = True
                Do While Not .EOF
                    lclsTmp_VIL7700 = New Tmp_Vil7700
                    lclsTmp_VIL7700.nNumcart = .FieldToClass("nNumcart")
                    lclsTmp_VIL7700.NCARTPOL = .FieldToClass("NCARTPOL")
                    lclsTmp_VIL7700.nBranch = .FieldToClass("NBRANCH")
                    lclsTmp_VIL7700.nProduct = .FieldToClass("NPRODUCT")
                    lclsTmp_VIL7700.nPolicy = .FieldToClass("NPOLICY")
                    lclsTmp_VIL7700.sFileName = .FieldToClass("sFileName")
                    Call Add(lclsTmp_VIL7700)
                    .RNext()
                    lclsTmp_VIL7700 = Nothing
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
        lrecreaTmp_VIL7700 = Nothing
    End Function


    '    'OHIO
    '    '%Find: Lee los datos de la tabla Tmp_VIL7700 la transacción VIL7700
    '    '------------------------------------------------------------------
    '    Public Function Find(Optional ByVal lblnAll As Boolean) As Boolean
    '        '------------------------------------------------------------------------------
    '        Dim lrecreaTmp_VIL7700 As eRemoteDB.Execute
    '        Dim lclsTmp_VIL7700 As Tmp_Vil7700

    '        On Error GoTo Find_Err
    '        lrecreaTmp_VIL7700 = New eRemoteDB.Execute
    '        With lrecreaTmp_VIL7700
    '            .StoredProcedure = "reaTmp_VIL7700"
    '            If .Run Then
    '                Find = True
    '                Do While Not .EOF
    '                    lclsTmp_VIL7700 = New Tmp_Vil7700
    '                    lclsTmp_VIL7700.nNumcart = .FieldToClass("nNumcart")
    '                    lclsTmp_VIL7700.NCARTPOL = .FieldToClass("NCARTPOL")
    '                    lclsTmp_VIL7700.nBranch = .FieldToClass("NBRANCH")
    '                    lclsTmp_VIL7700.nProduct = .FieldToClass("NPRODUCT")
    '                    lclsTmp_VIL7700.nPolicy = .FieldToClass("NPOLICY")
    '                    lclsTmp_VIL7700.sFileName = .FieldToClass("sFileName")
    '                    Call Add(lclsTmp_VIL7700)
    '                    .RNext()
    '                    lclsTmp_VIL7700 = Nothing
    '                Loop
    '                .RCloseRec()
    '            Else
    '                Find = False
    '            End If
    '        End With

    'Find_Err:
    '        If Err() Then
    '            Find = False
    '        End If
    '        On Error GoTo 0
    '        lrecreaTmp_VIL7700 = Nothing
    '    End Function

    '%Find: Lee los datos de la tabla Tmp_VIL7700 la transacción VIL7700
    '------------------------------------------------------------------
    Public Function UpdateProcess(ByVal sKey As String) As Boolean
        '------------------------------------------------------------------------------
        Dim lrecreaTmp_VIL7700 As eRemoteDB.Execute
        Dim lclsTmp_VIL7700 As Tmp_Vil7700

        On Error GoTo Find_Err
        lrecreaTmp_VIL7700 = New eRemoteDB.Execute
        With lrecreaTmp_VIL7700
            .StoredProcedure = "UpdTmp_VIL7700"
            .Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            UpdateProcess = .Run(False)
        End With

Find_Err:
        If Err.Number Then
            UpdateProcess = False
        End If
        On Error GoTo 0
        lrecreaTmp_VIL7700 = Nothing
    End Function
End Class