Option Strict Off
Option Explicit On
Public Class Soap_Sell_Periods

    Implements System.Collections.IEnumerable

    Private mCol As Collection

    '% Add: Adds a new instance to the class Soap_Sell_Period to the collection.
    '% Add: Añade una nueva instancia de la clase Soap_Sell_Period a la colección
    Public Function Add(ByVal objElement As Object) As Soap_Sell_Period

        Dim objNewMember As Soap_Sell_Period
        objNewMember = objElement

        mCol.Add(objNewMember)

        '+ Returns the created object.
        '+ Retorna el objeto creado

        Add = objNewMember
        objNewMember = Nothing
    End Function

    '% Find:Devuelve información de todas los registros 
    '%      de la tabla Soap_Sell_Period
    Public Function Find(ByVal nVehType As Integer) As Boolean

        Static lblnRead As Boolean
        Dim lrecreaSoap_Sell_Period_a As eRemoteDB.Execute
        Dim lclsSoap_Sell_Period As Soap_Sell_Period

        On Error GoTo Find_Err

        lrecreaSoap_Sell_Period_a = New eRemoteDB.Execute

        With lrecreaSoap_Sell_Period_a
            .StoredProcedure = "REASOAP_SELL_PERIOD_A"
            .Parameters.Add("nVehType", nVehType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            If .Run Then
                Do While Not .EOF
                    lclsSoap_Sell_Period = New Soap_Sell_Period
                    'NTYPEVEH, DSTARTPERIOD, DEXPIREPERIOD, DSTARTDATEPOL, DEXPIRDATEPOL, SSTATUS, DNULLDATE, NUSERCODE
                    lclsSoap_Sell_Period.nVehType = .FieldToClass("NTYPEVEH")
                    lclsSoap_Sell_Period.dStartPeriod = .FieldToClass("DSTARTPERIOD")
                    lclsSoap_Sell_Period.dExpirePeriod = .FieldToClass("DEXPIREPERIOD")
                    lclsSoap_Sell_Period.dStartDatepol = .FieldToClass("DSTARTDATEPOL")
                    lclsSoap_Sell_Period.dExpireDatepol = .FieldToClass("DEXPIRDATEPOL")
                    lclsSoap_Sell_Period.sStatus = .FieldToClass("SSTATUS")
                    lclsSoap_Sell_Period.dNullDate = .FieldToClass("DNULLDATE")
                    lclsSoap_Sell_Period.nUserCode = .FieldToClass("NUSERCODE")
                    lclsSoap_Sell_Period.nYear = .FieldToClass("NYEAR")

                    Call Add(lclsSoap_Sell_Period)

                    lclsSoap_Sell_Period = Nothing

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
        lrecreaSoap_Sell_Period_a = Nothing
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
