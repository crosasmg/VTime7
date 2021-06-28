Option Strict Off
Option Explicit On
Public Class Tab_rescosts
    Implements System.Collections.IEnumerable
    '%-------------------------------------------------------%'
    '% $Workfile:: Tab_rescosts                             $%'
    '% $Author:: HMendez                                    $%'
    '% $Date:: 14/08/03 16:38p                              $%'
    '% $Revision:: 4                                        $%'
    '%-------------------------------------------------------%'

    'local variable to hold collection
    Private mCol As Collection

    '%Add: Crea un registro en la tabla
    Public Function Add(ByRef objClass As Tab_rescost) As Tab_rescost
        With objClass
            mCol.Add(objClass)
        End With

        Add = objClass
        'UPGRADE_NOTE: Object objClass may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        objClass = Nothing
    End Function

    '%Find: Lee los datos de la tabla para la transacción MCA300
    Public Function Find(ByVal dEffecdate As Date) As Boolean
        Dim lrecReaTab_rescost_a As eRemoteDB.Execute
        Dim lclsTab_rescost As Tab_rescost

        On Error GoTo Find_Err

        lrecReaTab_rescost_a = New eRemoteDB.Execute

        With lrecReaTab_rescost_a
            .StoredProcedure = "ReaTab_rescost_a"
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run Then
                Find = True
                Do While Not .EOF
                    lclsTab_rescost = New Tab_rescost
                    lclsTab_rescost.nCodcost = .FieldToClass("nCodcost")
                    lclsTab_rescost.dEffecdate = .FieldToClass("dEffecdate")
                    lclsTab_rescost.sDescript = .FieldToClass("sDescript")
                    lclsTab_rescost.nRate = .FieldToClass("nRate")
                    lclsTab_rescost.nCurrency = .FieldToClass("nCurrency")
                    lclsTab_rescost.nAmount = .FieldToClass("nAmount")
                    lclsTab_rescost.nMinimum = .FieldToClass("nMinimum")
                    lclsTab_rescost.nMaximum = .FieldToClass("nMaximum")
                    Call Add(lclsTab_rescost)
                    .RNext()
                    'UPGRADE_NOTE: Object lclsTab_rescost may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
                    lclsTab_rescost = Nothing
                Loop
                .RCloseRec()
            End If
        End With

Find_Err:
        If Err.Number Then
            Find = False
        End If
        'UPGRADE_NOTE: Object lrecReaTab_rescost_a may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecReaTab_rescost_a = Nothing
        On Error GoTo 0
    End Function

    '* Item: Devuelve un elemento de la colección (segun índice)
    Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Tab_rescost
        Get
            Item = mCol.Item(vntIndexKey)
        End Get
    End Property

    '* Count: Devuelve el número de elementos que posee la colección
    Public ReadOnly Property Count() As Integer
        Get
            Count = mCol.Count()
        End Get
    End Property

    '* NewEnum: Permite enumerar la colección para utilizarla en un ciclo For Each... Next
    'UPGRADE_NOTE: NewEnum property was commented out. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="B3FC1610-34F3-43F5-86B7-16C984F0E88E"'
    'Public ReadOnly Property NewEnum() As stdole.IUnknown
    'Get
    'NewEnum = mCol._NewEnum
    'End Get
    'End Property

    Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
        'UPGRADE_TODO: Uncomment and change the following line to return the collection enumerator. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="95F9AAD0-1319-4921-95F0-B9D3C4FF7F1C"'
        GetEnumerator = mCol.GetEnumerator
    End Function

    '* Remove: elimina un elemento de la colección
    Public Sub Remove(ByRef vntIndexKey As Object)
        mCol.Remove(vntIndexKey)
    End Sub

    '* Class_Initialize: se controla la creación de la instancia del objeto
    'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
    Private Sub Class_Initialize_Renamed()
        mCol = New Collection
    End Sub
    Public Sub New()
        MyBase.New()
        Class_Initialize_Renamed()
    End Sub

    '* Class_Terminate: se controla la destrucción de la instancia del objeto
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






