Option Strict Off
Option Explicit On
Public Class Tab_Catevents
    Implements System.Collections.IEnumerable
    '%-------------------------------------------------------%'
    '% $Workfile:: Tab_Catevents.cls                        $%'
    '% $Author:: Nvaplat7                                   $%'
    '% $Date:: 9/08/03 1:28p                                $%'
    '% $Revision:: 13                                       $%'
    '%-------------------------------------------------------%'

    'local variable to hold collection
    Private mCol As Collection

    Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Tab_Catevent
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

    'UPGRADE_NOTE: NewEnum property was commented out. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="B3FC1610-34F3-43F5-86B7-16C984F0E88E"'
    'Public ReadOnly Property NewEnum() As stdole.IUnknown
    'Get
    'this property allows you to enumerate
    'this collection with the For...Each syntax
    'NewEnum = mCol._NewEnum
    'End Get
    'End Property

    Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
        'UPGRADE_TODO: Uncomment and change the following line to return the collection enumerator. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="95F9AAD0-1319-4921-95F0-B9D3C4FF7F1C"'
        GetEnumerator = mCol.GetEnumerator
    End Function

    Public Sub Remove(ByRef vntIndexKey As Object)
        'used when removing an element from the collection
        'vntIndexKey contains either the Index or Key, which is why
        'it is declared as a Variant
        'Syntax: x.Remove(xyz)
        mCol.Remove(vntIndexKey)
    End Sub

    'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
    Private Sub Class_Initialize_Renamed()
        'creates the collection when this class is created
        mCol = New Collection
    End Sub
    Public Sub New()
        MyBase.New()
        Class_Initialize_Renamed()
    End Sub

    'UPGRADE_NOTE: Class_Terminate was upgraded to Class_Terminate_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
    Private Sub Class_Terminate_Renamed()
        'destroys collection when this class is terminated
        'UPGRADE_NOTE: Object mCol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        mCol = Nothing
    End Sub
    Protected Overrides Sub Finalize()
        Class_Terminate_Renamed()
        MyBase.Finalize()
    End Sub
    '%Add: Agrega un nuevo registro a la colección
    Public Function Add(ByVal objClass As Tab_Catevent) As Tab_Catevent
        If objClass Is Nothing Then
            objClass = New Tab_Catevent
        End If

        With objClass
            mCol.Add(objClass, "CP" & .nIdcatas)
        End With

        'Return the object created
        Add = objClass

    End Function
    'Find: Valida que el registro a duplicar no exista en Tab_Catevent
    Public Function Find() As Object

        Dim lrecreaTab_Catevent As eRemoteDB.Execute
        Dim lclsTab_Catevent As Tab_Catevent

        On Error GoTo Find_Err

        lrecreaTab_Catevent = New eRemoteDB.Execute

        '+
        '+ Definición de store procedure reaTab_Catevent al 04-04-2002 11:58:22
        '+
        With lrecreaTab_Catevent
            .StoredProcedure = "reaTab_Catevent"
            If .Run Then
                Find = True
                Do While Not .EOF
                    lclsTab_Catevent = New Tab_Catevent
                    With lclsTab_Catevent
                        .nIdcatas = lrecreaTab_Catevent.FieldToClass("nIdcatas")
                        .nNumber = lrecreaTab_Catevent.FieldToClass("nNumber")
                        .nType = lrecreaTab_Catevent.FieldToClass("nType")
                        .nType_Rel = lrecreaTab_Catevent.FieldToClass("nType_Rel")
                        .nBranch = lrecreaTab_Catevent.FieldToClass("nBranch")

                        .sDescript = lrecreaTab_Catevent.FieldToClass("sDescript")
                        .sShort_Des = lrecreaTab_Catevent.FieldToClass("sShort_Des")
                        .sStatregt = lrecreaTab_Catevent.FieldToClass("sStatregt")


                    End With
                    Call Add(lclsTab_Catevent)
                    'UPGRADE_NOTE: Object lclsTab_Catevent may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
                    lclsTab_Catevent = Nothing
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
        'UPGRADE_NOTE: Object lrecreaTab_Catevent may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecreaTab_Catevent = Nothing
        On Error GoTo 0

    End Function

End Class
