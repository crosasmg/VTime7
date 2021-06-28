Option Strict Off
Option Explicit On
Public Class Tab_Settlements
    Implements System.Collections.IEnumerable

    '+ Variable local para contener colección
    Private mCol As Collection
    Public nCount As Integer

    '% Find:Devuelve información de todos los registros de la tabla Tab_Settlement 
    Public Function Find_DP7002(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nModulec As Integer, ByVal nCover As Integer) As Boolean

        Static lblnRead As Boolean
        Dim lrecreaTab_Settlement As eRemoteDB.Execute
        Dim lclsTab_Settlement As Tab_Settlement

        On Error GoTo Find_Err

        lrecreaTab_Settlement = New eRemoteDB.Execute

        With lrecreaTab_Settlement
            .StoredProcedure = "REATAB_SETTLEMENT_DP7002"
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run Then
                Do While Not .EOF

                    lclsTab_Settlement = New Tab_Settlement
                    lclsTab_Settlement.nId_Settle = .FieldToClass("nId_Settle")                    
                    lclsTab_Settlement.sDescript = .FieldToClass("sDescript")
                    lclsTab_Settlement.sSel = .FieldToClass("sSel")                    

                    Call Add(lclsTab_Settlement)

                    lclsTab_Settlement = Nothing

                    .RNext()
                Loop

                .RCloseRec()
                Find_DP7002 = True
            Else
                Find_DP7002 = False
            End If
        End With

Find_Err:
        If Err.Number Then
            Find_DP7002 = False
        End If
        On Error GoTo 0
        lrecreaTab_Settlement = Nothing
    End Function

    '% Add: Adds a new instance to the class Tab_Settlement to the collection.
    '% Add: Añade una nueva instancia de la clase Tab_Settlement a la colección
    Public Function Add(ByVal objElement As Object) As Tab_Settlement

        Dim objNewMember As Tab_Settlement
        objNewMember = objElement

        mCol.Add(objNewMember)

        '+ Returns the created object.
        '+ Retorna el objeto creado

        Add = objNewMember
        objNewMember = Nothing
    End Function
    '%Add: Agrega un nuevo registro a la colección
    Public Function Add_MSI7000(ByVal lclsClaim_SetleMent As Tab_Settlement) As Tab_Settlement
        mCol.Add(lclsClaim_SetleMent)

        '+ Devolver el objeto creado
        Add_MSI7000 = lclsClaim_SetleMent
    End Function
    '% Funcion que devuelve Tipos de finiquitos
    Public Function MSI7000_Find(Optional ByVal nBranch As Integer = 0, Optional ByVal nProduct As Integer = 0, Optional ByVal sFormatname As String = "", Optional ByVal nType_settle As Integer = 0)

        Dim lrecClaim_SetleMent As eRemoteDB.Execute
        Dim lclsClaim_SetleMent As Tab_Settlement
        lrecClaim_SetleMent = New eRemoteDB.Execute

        With lrecClaim_SetleMent
            .StoredProcedure = "reaTab_Settlement"
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sFormatname", sFormatname, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 80, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nType_settle", nType_settle, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            If Not .Run Then
                MSI7000_Find = False
            Else
                MSI7000_Find = True
                Do While Not .EOF
                    lclsClaim_SetleMent = New Tab_Settlement
                    lclsClaim_SetleMent.nBranch = nBranch
                    lclsClaim_SetleMent.nProduct = .FieldToClass("nProduct") 'IIf(nCovergen = eRemoteDB.intNull, .FieldToClass("nSettlecode"), nCovergen)
                    lclsClaim_SetleMent.sDescript = .FieldToClass("sDescript")
                    lclsClaim_SetleMent.sFormatname = .FieldToClass("sFormatname")
                    lclsClaim_SetleMent.nId_Settle = .FieldToClass("nId_settle")
                    lclsClaim_SetleMent.nBranch = .FieldToClass("nBranch")

                    Call Add_MSI7000(lclsClaim_SetleMent)
                    .RNext()
                Loop
            End If
        End With

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
