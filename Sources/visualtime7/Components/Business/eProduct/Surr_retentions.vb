Option Strict Off
Option Explicit On
Public Class Surr_retentions
	Implements System.Collections.IEnumerable
	'- Variable local donde se almacena la colección
	
	Private mCol As Collection
	
	'%Add: Añade una nueva instancia de la clase "ul_Move_Acc_pol" a la colección
    Public Function Add(ByRef nSurr_ret As Double, ByRef nSurr_reason As Integer, ByRef dNulldate As Date, ByRef dEffecdate As Date, ByRef nProduct As Integer, ByRef nBranch As Integer, Optional ByRef nTyp_profitworker As Integer = 0, Optional ByRef nAmountfree As Integer = 0, Optional ByRef nCurrency As Integer = 0, Optional ByRef nOrigin As Integer = 0) As Surr_retention
        'Public Function Add(ByRef nSurr_ret As Double, ByRef nSurr_reason As Integer, ByRef dNulldate As Date, ByRef dEffecdate As Date, ByRef nProduct As Integer, ByRef nBranch As Integer, ByRef nTyp_profitworker As Integer, ByRef nAmountfree As Integer, ByRef nCurrency As Integer) As Surr_retention
        '- Se crea un objeto nuevo
        Dim objNewMember As Surr_retention

        '+ Se setean las propiedades pasado al metodo
        objNewMember = New Surr_retention
        With objNewMember

            .nSurr_ret = nSurr_ret
            .nSurr_reason = nSurr_reason
            .dNulldate = dNulldate
            .dEffecdate = dEffecdate
            .nProduct = nProduct
            .nBranch = nBranch
            .nTyp_profitworker = nTyp_profitworker
            .nAmountfree = nAmountfree
            .nCurrency = nCurrency
            .nOrigin = nOrigin

        End With

        mCol.Add(objNewMember, RTrim(CStr(nBranch)) & RTrim(CStr(nProduct)) & RTrim(CStr(dEffecdate)) & RTrim(CStr(nSurr_reason)))

        '+ Se retorna el objeto creado
        Add = objNewMember
        'UPGRADE_NOTE: Object objNewMember may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        objNewMember = Nothing
    End Function

    '% Item: Toma un elemento de la colección
    Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Surr_retention
        Get
            Item = mCol.Item(vntIndexKey)
        End Get
    End Property

    '% Count: Cuenta el número de elementos dentro de la colección
    Public ReadOnly Property Count() As Integer
        Get
            Count = mCol.Count()
        End Get
    End Property

    '% NewEnum: Enumera los elementos dentro de la colección
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

    '% Remove: Elimina un elemento dentro de la colección
    Public Sub Remove(ByRef vntIndexKey As Object)
        mCol.Remove(vntIndexKey)
    End Sub

    '% Class_Initialize: Controla la apertura de cada instancia de la colección
    'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
    Private Sub Class_Initialize_Renamed()
        mCol = New Collection
    End Sub
    Public Sub New()
        MyBase.New()
        Class_Initialize_Renamed()
    End Sub

    '% Class_Terminate: Elimina la colección
    'UPGRADE_NOTE: Class_Terminate was upgraded to Class_Terminate_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
    Private Sub Class_Terminate_Renamed()
        'UPGRADE_NOTE: Object mCol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        mCol = Nothing
    End Sub
    Protected Overrides Sub Finalize()
        Class_Terminate_Renamed()
        MyBase.Finalize()
    End Sub

    '% Find: Este metodo carga la coleccion de elementos de la tabla "Surr_retention" devolviendo
    '%       Verdadero o falso, dependiendo de la existencia de los registros.
    Public Function Find(ByRef nBranch As Integer, ByRef nProduct As Integer, ByRef dEffecdate As Date) As Boolean
        Find = False

        Dim lrecReaSurr_retention As eRemoteDB.Execute
        lrecReaSurr_retention = New eRemoteDB.Execute

        On Error GoTo Find_Err

        With lrecReaSurr_retention
            .StoredProcedure = "reaSurr_retention"

            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            If .Run Then
                Do While Not .EOF
                    Call Add(.FieldToClass("nSurr_ret"), .FieldToClass("nSurr_reason"), .FieldToClass("dNulldate"), .FieldToClass("dEffecdate"), .FieldToClass("nProduct"), .FieldToClass("nBranch"), .FieldToClass("nTyp_profitworker"), .FieldToClass("nAmountfree"), .FieldToClass("nCurrency"), .FieldToClass("nOrigin"))
                    .RNext()
                Loop

                .RCloseRec()
                Find = True
            End If
        End With

        lrecReaSurr_retention = Nothing

Find_Err:
        If Err.Number Then
            Find = False
        End If
    End Function
End Class






