Option Strict Off
Option Explicit On
Public Class Folios_comps

    Implements System.Collections.IEnumerable

    Private mCol As Collection

    '% Add: Adds a new instance to the class Folios_comp to the collection.
    '% Add: Añade una nueva instancia de la clase Folios_comp a la colección
    Public Function Add(ByVal objElement As Object) As Folios_comp

        Dim objNewMember As Folios_comp
        objNewMember = objElement

        mCol.Add(objNewMember)

        '+ Returns the created object.
        '+ Retorna el objeto creado

        Add = objNewMember
        'UPGRADE_NOTE: Object objNewMember may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        objNewMember = Nothing
    End Function

    '% Find:Devuelve información de todas los registros 
    '%      de la tabla Folios asignados a la compañía (Folios_comp)
    Public Function Find() As Boolean

        Static lblnRead As Boolean
        Dim lrecreaFolios_comp_a As eRemoteDB.Execute
        Dim lclsFolios_comp As Folios_comp

        On Error GoTo Find_Err

        lrecreaFolios_comp_a = New eRemoteDB.Execute

        With lrecreaFolios_comp_a
            .StoredProcedure = "reaFolios_comp_a"

            If .Run Then
                Do While Not .EOF
                    lclsFolios_comp = New Folios_comp

                    lclsFolios_comp.nYear = .FieldToClass("nYear")
                    lclsFolios_comp.nStart = .FieldToClass("nStart")
                    lclsFolios_comp.nEnd = .FieldToClass("nEnd")
                    lclsFolios_comp.sStatregt = .FieldToClass("sStatregt")

                    Call Add(lclsFolios_comp)

                    lclsFolios_comp = Nothing

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
        lrecreaFolios_comp_a = Nothing
    End Function

    '% Item: restores an element from the collection (according to the index)
    '% Item: Devuelve un elemento de la colección (segun índice)
    Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Folios_comp
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
