Option Strict Off
Option Explicit On
Public Class Folios_Agents

    Implements System.Collections.IEnumerable

    Private mCol As Collection

    '% Add: Adds a new instance to the class Folios_agent to the collection.
    '% Add: Añade una nueva instancia de la clase Folios_agent a la colección
    Public Function Add(ByVal objElement As Object) As Folios_Agent

        Dim objNewMember As Folios_Agent
        objNewMember = objElement

        mCol.Add(objNewMember)

        '+ Returns the created object.
        '+ Retorna el objeto creado

        Add = objNewMember
        objNewMember = Nothing
    End Function

    '% Find:Devuelve información de todas los registros 
    '%      de la tabla Folios asignados a la compañía (Folios_agent)
    Public Function Find(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nIntermed As Long, ByVal dAssign_date As Date) As Boolean

        Static lblnRead As Boolean
        Dim lrecreaFolios_agent_a As eRemoteDB.Execute
        Dim lclsFolios_agent As Folios_Agent

        On Error GoTo Find_Err

        lrecreaFolios_agent_a = New eRemoteDB.Execute

        With lrecreaFolios_agent_a
            .StoredProcedure = "reaFolios_agent_a"
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nIntermed", nIntermed, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dAssign_date", dAssign_date, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            If .Run Then
                Do While Not .EOF

                    lclsFolios_agent = New Folios_Agent
                    lclsFolios_agent.nBranch = .FieldToClass("nBranch")
                    lclsFolios_agent.nProduct = .FieldToClass("nProduct")
                    lclsFolios_agent.nIntermed = .FieldToClass("nIntermed")
                    lclsFolios_agent.dAssign_date = .FieldToClass("dAssign_date")
                    lclsFolios_agent.sPolitype = .FieldToClass("sPolitype")
                    lclsFolios_agent.nStart = .FieldToClass("nStart")
                    lclsFolios_agent.nEnd = .FieldToClass("nEnd")
                    lclsFolios_agent.sProcessInd = .FieldToClass("sProcessInd")
                    lclsFolios_agent.nStartPolNumber = .FieldToClass("nStartPolNumber")
                    lclsFolios_agent.nEndPolNumber = .FieldToClass("nEndPolNumber")

                    Call Add(lclsFolios_agent)

                    lclsFolios_agent = Nothing

                    .RNext()
                Loop

                .RCloseRec()
                Find = True
            Else
                Find = False
            End If
        End With

Find_Err:
        If Err.Number Then
            Find = False
        End If
        On Error GoTo 0
        lrecreaFolios_agent_a = Nothing
    End Function

    '% Find:Devuelve información de todas los registros 
    '%      de la tabla Folios asignados a la compañía (Folios_agent)
    Public Function PreSO002(ByVal nIntermedSource As Long, ByVal nFolioI As Long, ByVal nFolioE As Long, ByVal nIntermedDest As Long, _
                             ByVal nUsercode As Integer) As Boolean

        Static lblnRead As Boolean
        Dim lrecreaFolios_agent_a As eRemoteDB.Execute
        Dim lclsFolios_agent As Folios_Agent

        On Error GoTo Find_Err

        lrecreaFolios_agent_a = New eRemoteDB.Execute
        With lrecreaFolios_agent_a
            .StoredProcedure = "INSSO002PKG.INSPRESO002"
            .Parameters.Add("nIntermedSource", nIntermedSource, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nFolioI", nFolioI, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nFolioE", nFolioE, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nIntermedDest", nIntermedDest, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            If .Run Then
                Do While Not .EOF

                    lclsFolios_agent = New Folios_Agent
                    lclsFolios_agent.sCause = .FieldToClass(.FieldName(0))

                    Call Add(lclsFolios_agent)

                    lclsFolios_agent = Nothing

                    .RNext()
                Loop

                .RCloseRec()
                PreSO002 = True
            Else
                PreSO002 = False
            End If
        End With

Find_Err:
        If Err.Number Then
            PreSO002 = False
        End If
        On Error GoTo 0
        lrecreaFolios_agent_a = Nothing
    End Function

    '% FindYear:Devuelve información de todas los registros 
    '%          de la tabla Folios asignados a la compañía (Folios_agent) en un año especifico
    Public Function FindYear(ByVal nYear As Integer) As Boolean

        Static lblnRead As Boolean
        Dim lrecreaFolios_agent_a As eRemoteDB.Execute
        Dim lclsFolios_agent As Folios_Agent

        On Error GoTo Find_Err

        lrecreaFolios_agent_a = New eRemoteDB.Execute

        With lrecreaFolios_agent_a
            .StoredProcedure = "reaFolios_year"
            .Parameters.Add("nYear", nYear, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            If .Run Then
                Do While Not .EOF

                    lclsFolios_agent = New Folios_Agent
                    lclsFolios_agent.sIntermedia = .FieldToClass("sIntermedia")
                    lclsFolios_agent.nStart = .FieldToClass("nStart")
                    lclsFolios_agent.nEnd = .FieldToClass("nEnd")
                    lclsFolios_agent.sPolitype = .FieldToClass("sPolitype")
                    lclsFolios_agent.sDesBranch = .FieldToClass("sDesBranch")
                    lclsFolios_agent.sDesProd = .FieldToClass("sDesProd")
                    lclsFolios_agent.nSold = .FieldToClass("nSold")

                    Call Add(lclsFolios_agent)

                    lclsFolios_agent = Nothing

                    .RNext()
                Loop

                .RCloseRec()
                FindYear = True
            Else
                FindYear = False
            End If
        End With

Find_Err:
        If Err.Number Then
            FindYear = False
        End If
        On Error GoTo 0
        lrecreaFolios_agent_a = Nothing
    End Function


    '% Item: restores an element from the collection (according to the index)
    '% Item: Devuelve un elemento de la colección (segun índice)
    Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Folios_Agent
        Get
            Item = mCol.Item(vntIndexKey)
        End Get
    End Property

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
