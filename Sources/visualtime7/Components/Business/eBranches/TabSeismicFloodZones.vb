Option Strict Off
Option Explicit On
Public Class TabSeismicFloodZones
    Implements System.Collections.IEnumerable

    'local variable to hold collection
    Private mCol As Collection

    '**% Add: Adds a new instance of the TabSeismicFloodZone class to the collection
    '% Add: Añade una nueva instancia de la clase TabSeismicFloodZone a la colección

    Public Function Add(ByVal nZip_Code As Integer, ByVal nGeographicZone1 As Integer, ByVal nGeographicZone2 As Integer, _
                        ByVal nGeographicZone3 As Integer, ByVal nSeismicZone As Integer, ByVal nDeduSeismicZone As Double, _
                        ByVal nCoasSeismicZone As Double, ByVal nZoneType As Integer, ByVal nDeduZoneType As Double, _
                        ByVal nCoasZoneType As Double, ByVal sStatRegt As String) As TabSeismicFloodZone
        'create a new object
        Dim objNewMember As TabSeismicFloodZone


        objNewMember = New TabSeismicFloodZone

        With objNewMember
            .nZip_Code = nZip_Code
            .nGeographicZone1 = nGeographicZone1
            .nGeographicZone2 = nGeographicZone2
            .nGeographicZone3 = nGeographicZone3
            .nSeismicZone = nSeismicZone
            .nDeduSeismicZone = nDeduSeismicZone
            .nCoasSeismicZone = nCoasSeismicZone
            .nZoneType = nZoneType
            .nDeduZoneType = nDeduZoneType
            .nCoasZoneType = nCoasZoneType
            .sStatRegt = sStatRegt
        End With

        'set the properties passed into the method
        mCol.Add(objNewMember)

        'return the object created
        Add = objNewMember

        objNewMember = Nothing

        Exit Function
    End Function

    '**% Find: Restores a collection of objects of TabSeismicFloodZone type
    '% Find: Devuelve una coleccion de objetos de tipo TabSeismicFloodZone
    Public Function Find() As Boolean

        '**- Variable definition lrecTabSeismicFloodZone that will be used as a cursor
        '- Se define la variable lrecTabSeismicFloodZone que se utilizará como cursor.
        Dim lrecTabSeismicFloodZone As eRemoteDB.Execute


        lrecTabSeismicFloodZone = New eRemoteDB.Execute

        With lrecTabSeismicFloodZone
            .StoredProcedure = "reaTabSeismicFloodZone_a"

            If Not .Run Then
                Find = False
            Else
                Find = True
                Do While Not .EOF
                    Call Add(.FieldToClass("nZip_Code"), .FieldToClass("nGeographicZone1"), .FieldToClass("nGeographicZone2"), _
                             .FieldToClass("nGeographicZone3"), .FieldToClass("nSeismicZone"), .FieldToClass("nDeduSeismicZone"), _
                             .FieldToClass("nCoasSeismicZone"), .FieldToClass("nZoneType"), .FieldToClass("nDeduZoneType"), _
                             .FieldToClass("nCoasZoneType"), .FieldToClass("sStatRegt"))

                    .RNext()
                Loop
                .RCloseRec()
            End If
        End With

        lrecTabSeismicFloodZone = Nothing

        Exit Function
    End Function

    '***Item: Returns an element of the collection (acording to the index)
    '*Item: Devuelve un elemento de la colección (segun índice)
    Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As TabSeismicFloodZone
        Get
            'used when referencing an element in the collection
            'vntIndexKey contains either the Index or Key to the collection,
            'this is why it is declared as a Variant
            'Syntax: Set foo = x.Item(xyz) or Set foo = x.Item(5)

            Item = mCol.Item(vntIndexKey)

            Exit Property
        End Get
    End Property

    '***Count: Returns the number of elements that the collection has
    '*Count: Devuelve el número de elementos que posee la colección
    Public ReadOnly Property Count() As Integer
        Get
            'used when retrieving the number of elements in the
            'collection. Syntax: Debug.Print x.Count
            

            Count = mCol.Count()

            Exit Property
        End Get
    End Property

    '***NewEnum: Enumerates the collection for use in a For Each...Next loop
    '*NewEnum: Permite enumerar la colección para utilizarla en un ciclo For Each... Next
    'UPGRADE_NOTE: NewEnum property was commented out. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="B3FC1610-34F3-43F5-86B7-16C984F0E88E"'
    'Public ReadOnly Property NewEnum() As stdole.IUnknown
    'Get
    'this property allows you to enumerate
    'this collection with the For...Each syntax

    '
    'NewEnum = mCol._NewEnum
    '
    'Exit Property
    'ErrorHandler: '
    'ProcError("TabSeismicFloodZones.NewEnum()")
    'End Get
    'End Property

    Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
        GetEnumerator = mCol.GetEnumerator
    End Function

    '**%Remove: Deletes an element from the collection
    '%Remove: Elimina un elemento de la colección
    Public Sub Remove(ByRef vntIndexKey As Object)
        'used when removing an element from the collection
        'vntIndexKey contains either the Index or Key, which is why
        'it is declared as a Variant
        'Syntax: x.Remove(xyz)


        mCol.Remove(vntIndexKey)

        Exit Sub
    End Sub

    '**%Class_Initialize: Controls the creation of an instance of the collection
    '%Class_Initialize: Controla la creación de una instancia de la colección
    Private Sub Class_Initialize_Renamed()
        'creates the collection when this class is created


        mCol = New Collection

        Exit Sub
    End Sub
    Public Sub New()
        MyBase.New()
        Class_Initialize_Renamed()
    End Sub

    '**%Class_Terminate: Controls the destruction of an instance of the collection
    '%Class_Terminate: Controla la destrucción de una instancia de la colección
    Private Sub Class_Terminate_Renamed()
        'destroys collection when this class is terminated


        mCol = Nothing

        Exit Sub
    End Sub
    Protected Overrides Sub Finalize()
        Class_Terminate_Renamed()
        MyBase.Finalize()
    End Sub

End Class





