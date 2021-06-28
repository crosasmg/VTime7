Option Strict Off
Option Explicit On
Public Class Cl_Settlements
    Implements System.Collections.IEnumerable

    '+ Variable local para contener colección
    Private mCol As Collection
    Public nCount As Integer

    '% Find:Devuelve información de todos los registros de la tabla Tab_Settlement 
    Public Function Find_SI764(ByVal nClaim As Integer, ByVal nDeman_type As Integer, ByVal nCase_num As Integer, ByVal nUsercode As Integer) As Boolean

        Static lblnRead As Boolean
        Dim lrecreaTab_Settlement As eRemoteDB.Execute
        Dim lclsTab_Settlement As Cl_Settlement

        On Error GoTo Find_Err

        lrecreaTab_Settlement = New eRemoteDB.Execute

        With lrecreaTab_Settlement
            .StoredProcedure = "REACL_SETTLEMENTS"
            .Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nDeman_type", nDeman_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCase_num", nCase_num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run Then
                Do While Not .EOF

                    lclsTab_Settlement = New Cl_Settlement
                    lclsTab_Settlement.nId_Settle = .FieldToClass("nId_Settle")
                    lclsTab_Settlement.sDescript = .FieldToClass("sDescript")
                    lclsTab_Settlement.sSel = .FieldToClass("sSel")
                    lclsTab_Settlement.nCover = .FieldToClass("nCover")
                    lclsTab_Settlement.nModulec = .FieldToClass("nModulec")
                    lclsTab_Settlement.sCover = .FieldToClass("sCover")

                    Call Add(lclsTab_Settlement)

                    lclsTab_Settlement = Nothing

                    .RNext()
                Loop

                .RCloseRec()
                Find_SI764 = True
            Else
                Find_SI764 = False
            End If
        End With

Find_Err:
        If Err.Number Then
            Find_SI764 = False
        End If
        On Error GoTo 0
        lrecreaTab_Settlement = Nothing
    End Function

    '% Add: Adds a new instance to the class Tab_Settlement to the collection.
    '% Add: Añade una nueva instancia de la clase Tab_Settlement a la colección
    Public Function Add(ByVal objElement As Object) As Cl_Settlement

        Dim objNewMember As Cl_Settlement
        objNewMember = objElement

        mCol.Add(objNewMember)

        '+ Returns the created object.
        '+ Retorna el objeto creado

        Add = objNewMember
        objNewMember = Nothing
    End Function
    '% Count: Restores the number of elements that the collection owns.
    '% Count: Devuelve el numero de elementos que posee la coleccion
    Public ReadOnly Property Count() As Integer
        Get
            Count = mCol.Count()
        End Get
    End Property

    Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
        GetEnumerator = mCol.GetEnumerator
    End Function

    '% Remove: Removes an element from the collection.
    '% Remove: Elimina un elemento de la coleccion
    Public Sub Remove(ByRef vntIndexKey As Object)
        mCol.Remove(vntIndexKey)
    End Sub

    '% Class_Initialize: controls the creation of an instance of the collection.
    '% Class_Initialize: Controla la creacion de una instancia de la coleccion
    Private Sub Class_Initialize_Renamed()
        mCol = New Collection
    End Sub
    Public Sub New()
        MyBase.New()
        Class_Initialize_Renamed()
    End Sub

    '% Class_Terminate: controls the delete of an instance of the collection.
    '% Class_Terminate: Controla la destruccion de una instancia de la coleccion
    Private Sub Class_Terminate_Renamed()
        mCol = Nothing
    End Sub

    Protected Overrides Sub Finalize()
        Class_Terminate_Renamed()
        MyBase.Finalize()
    End Sub

End Class
