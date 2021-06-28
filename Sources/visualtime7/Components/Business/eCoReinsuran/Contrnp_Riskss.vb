Option Strict Off
Option Explicit On
Public Class Contrnp_Riskss
    Implements System.Collections.IEnumerable

    'local variable to hold collection
    Private mCol As Collection

    Public Function Add(ByVal lclsContrnp_Risks As Contrnp_Risks) As Contrnp_Risks
        With lclsContrnp_Risks
            mCol.Add(lclsContrnp_Risks, "C" & .nNumber & .nBranch & .dEffecdate & .nType & .sClient)
        End With
        'return the object created
        Add = lclsContrnp_Risks
        lclsContrnp_Risks = Nothing
    End Function

    Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Contrnp_Risks
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

    Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
        GetEnumerator = mCol.GetEnumerator
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

    '+ FindContr_limCovNumber: recupera todos aquellos registros validos asociados
    '+ a un numero de contrato
    Function Find(ByVal nNumber As Integer, ByVal nBranch As Integer, ByVal dEffecdate As Date, ByVal nType As Integer) As Boolean
        Dim lrecFind As eRemoteDB.Execute
        Dim lclsContrnp_Risks As Contrnp_Risks

        lrecFind = New eRemoteDB.Execute
        On Error GoTo Find_Err
        Find = True
        With lrecFind
            .StoredProcedure = "reaContrnp_Risks"
            .Parameters.Add("nNumber", nNumber, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nType", nType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run() Then
                While Not .EOF
                    lclsContrnp_Risks = New Contrnp_Risks
                    With lclsContrnp_Risks
                        .nNumber = lrecFind.FieldToClass("nNumber")
                        .nBranch = lrecFind.FieldToClass("nBranch")
                        .dEffecdate = lrecFind.FieldToClass("dEffecdate")
                        .nType = lrecFind.FieldToClass("nType")
                        .sClient = lrecFind.FieldToClass("sClient")
                        .nSumInsured = lrecFind.FieldToClass("nSumInsured")
                        .sSpcApply = lrecFind.FieldToClass("sSpcApply")
                    End With
                    Add(lclsContrnp_Risks)
                    lclsContrnp_Risks = Nothing
                    .RNext()
                End While
            Else
                Find = False
            End If
        End With

        lrecFind = Nothing

Find_Err:
        If Err.Number Then
            Find = False
        End If
        On Error GoTo 0
    End Function
End Class