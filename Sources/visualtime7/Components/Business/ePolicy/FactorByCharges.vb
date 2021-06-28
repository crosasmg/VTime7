Option Strict Off
Option Explicit On
Public Class FactorByCharges
    Implements System.Collections.IEnumerable

    Private mCol As Collection
    '**% Add: Adds a new instance of the Tab_comrat class to the collection
    '% Add: Añade una nueva instancia de la clase Tab_comrat a la colección
    Public Function Add(ByVal nPosition As Integer, ByVal sDescript As String, ByVal nFactor As Integer) As FactorByCharge

        '+ Se crea un nuevo objeto

        Dim objNewMember As FactorByCharge
        objNewMember = New FactorByCharge

        With objNewMember
            .nPosition = nPosition
            .sDescript = sDescript
            .nFactor = nFactor
        End With

        mCol.Add(objNewMember)

        '+ Retorna el objeto creado

        Add = objNewMember
        'UPGRADE_NOTE: Object objNewMember may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        objNewMember = Nothing

    End Function

    '% Find: Devuelve una coleccion de objetos de tipo Tab_comrat
    Public Function Find() As Boolean

        '**- Variable definition lrecTab_comrat that will be used as a cursor.
        '- Se define la variable lrecTab_comrat que se utilizará como cursor.
        Dim lrecFactorByCharge As eRemoteDB.Execute

        lrecFactorByCharge = New eRemoteDB.Execute

        On Error GoTo Find_Err

     
            '**+ Execute the store procedure that searches an intermediary's transactions.
            '+ Se ejecuta el store procedure que busca los movimientos de un intermediario

        With lrecFactorByCharge
            .StoredProcedure = "REA_FACTORBYCHARGE_ALL"
           If Not .Run Then
                Find = False
            Else
               Find = True
                Do While Not .EOF
                    Call Add(.FieldToClass("nPosition"), .FieldToClass("sDescript"), .FieldToClass("nFactor"))
                    .RNext()
                Loop
            End If
        End With

Find_Err:
        If Err.Number Then
            Find = False
        End If
        'UPGRADE_NOTE: Object lrecTab_comrat may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecFactorByCharge = Nothing
    End Function

    '***Item: Restores an element to the collection (according to the index)
    '* Item: Devuelve un elemento de la colección (segun índice)
    Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As FactorByCharge
        Get

            Item = mCol.Item(vntIndexKey)
        End Get
    End Property

    '*** Count: Restores the number of elements that the collection owns.
    '* Count: Devuelve el número de elementos que posee la colección
    Public ReadOnly Property Count() As Integer
        Get
            Count = mCol.Count()
        End Get
    End Property


    Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
        'UPGRADE_TODO: Uncomment and change the following line to return the collection enumerator. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="95F9AAD0-1319-4921-95F0-B9D3C4FF7F1C"'
        GetEnumerator = mCol.GetEnumerator
    End Function

    '**% Remove: Removes an element from the collection.
    '% Remove: Elimina un elemento de la colección
    Public Sub Remove(ByRef vntIndexKey As Object)
        mCol.Remove(vntIndexKey)
    End Sub

    '**% Class_Initialize: Controls the creation of an instance of the collection.
    '% Class_Initialize: Controla la creación de una instancia de la colección
    'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
    Private Sub Class_Initialize_Renamed()
        mCol = New Collection
    End Sub
    Public Sub New()
        MyBase.New()
        Class_Initialize_Renamed()
    End Sub

    '**% Class_Terminate: Controls the destruction of an instance of the collection.
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