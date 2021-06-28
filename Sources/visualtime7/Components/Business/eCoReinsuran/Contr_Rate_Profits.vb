Option Strict Off
Option Explicit On
Public Class Contr_Rate_Profits
    Implements System.Collections.IEnumerable
    '%-------------------------------------------------------%'
    '% $Workfile:: Contr_Rate_Profits.cls                   $%'
    '% $Author:: Nvaplat7                                   $%'
    '% $Date:: 9/08/03 1:28p                                $%'
    '% $Revision:: 13                                       $%'
    '%-------------------------------------------------------%'

    'local variable to hold collection
    Private mCol As Collection

    Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Contr_Rate_Profit
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
    Public Function Add(ByVal objClass As Contr_Rate_Profit) As Contr_Rate_Profit
        If objClass Is Nothing Then
            objClass = New Contr_Rate_Profit
        End If

        With objClass
            mCol.Add(objClass, "CP" & .nNumber & .nBranch_Rei & .nType & .nIni_Policy & .nCompany & .dEffecdate)
        End With

        'Return the object created
        Add = objClass

    End Function
    'Find: Valida que el registro a duplicar no exista en Contr_Rate_Profit
    Public Function Find(ByVal dEffecdate As Date) As Object

        Dim lrecreaContr_Rate_Profit As eRemoteDB.Execute
        Dim lclsContr_Rate_Profit As eCoReinsuran.Contr_Rate_Profit

        On Error GoTo Find_Err

        lrecreaContr_Rate_Profit = New eRemoteDB.Execute

        '+
        '+ Definición de store procedure reaContr_Rate_Profit al 04-04-2002 11:58:22
        '+
        With lrecreaContr_Rate_Profit
            .StoredProcedure = "reaContr_Rate_Profit"
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            If .Run Then
                Find = True
                Do While Not .EOF
                    lclsContr_Rate_Profit = New eCoReinsuran.Contr_Rate_Profit
                    With lclsContr_Rate_Profit
                        .nNumber = lrecreaContr_Rate_Profit.FieldToClass("nNumber")
                        .nBranch_Rei = lrecreaContr_Rate_Profit.FieldToClass("nBranch_rei")
                        .nType = lrecreaContr_Rate_Profit.FieldToClass("nType")
                        .nCompany = lrecreaContr_Rate_Profit.FieldToClass("nCompany")
                        .nIni_Policy = lrecreaContr_Rate_Profit.FieldToClass("nIni_Policy")
                        .nEnd_Policy = lrecreaContr_Rate_Profit.FieldToClass("nEnd_Policy")
                        .nPercent = lrecreaContr_Rate_Profit.FieldToClass("nPercent")
                    End With
                    Call Add(lclsContr_Rate_Profit)
                    'UPGRADE_NOTE: Object lclsContr_Rate_Profit may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
                    lclsContr_Rate_Profit = Nothing
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
        'UPGRADE_NOTE: Object lrecreaContr_Rate_Profit may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecreaContr_Rate_Profit = Nothing
        On Error GoTo 0

    End Function

End Class
