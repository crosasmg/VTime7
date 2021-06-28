Option Strict Off
Option Explicit On
<System.Runtime.InteropServices.ProgId("Contrat_Pays_NET.Contrat_Pays")> Public Class Contrat_Pays
    Implements System.Collections.IEnumerable
    '%-------------------------------------------------------%'
    '% $Workfile:: Contrat_Pays.cls                          $%'
    '% $Author:: Malllendes                                  $%'
    '% $Date:: 9/08/03 1:38p                                $%'
    '% $Revision:: 5                                        $%'
    '%-------------------------------------------------------%'

    '- Variables locales
    Private mCol As Collection

    '% Add: Añade una nueva instancia de la clase Contrat_Pay a la colección
    Public Function Add(ByRef objClass As Contrat_Pay) As Contrat_Pay
        'create a new object
        If objClass Is Nothing Then
            objClass = New Contrat_Pay
        End If

        With objClass
            mCol.Add(objClass) ', .nContrat_Pay & .nSeq & .nCode & .nInit_Dur & .nEnd_Dur & .nPercent_detail)
        End With

        'return the object created
        Add = objClass
        'UPGRADE_NOTE: Object objClass may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        objClass = Nothing
    End Function

    '% Find: Devuelve una coleccion de objetos de tipo Contrat_Pay_Detail
    Public Function Find(ByVal nContrat_Pay As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer) As Boolean
        '- Se define la variable lrecContrat_Pay_Detail que se utilizará como cursor.
        Dim lrecContrat_Pay_Prod As eRemoteDB.Execute
        Dim lclsContrat_Pay_Prod As Contrat_Pay

        On Error GoTo Find_Err
        lrecContrat_Pay_Prod = New eRemoteDB.Execute
        '+ Se ejecuta el store procedure que busca los vehículos
        With lrecContrat_Pay_Prod
            .StoredProcedure = "REACONTRAT_PAY_PROD_DETAIL"
            .Parameters.Add("nContrat_Pay", nContrat_Pay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run Then
                Find = True
                Do While Not .EOF
                    lclsContrat_Pay_Prod = New Contrat_Pay
                    lclsContrat_Pay_Prod.nBranch = CInt(nBranch)
                    lclsContrat_Pay_Prod.nProduct = CInt(nProduct)
                    lclsContrat_Pay_Prod.nContrat_Pay = CInt(nContrat_Pay)
                    lclsContrat_Pay_Prod.dEffecdate = .FieldToClass("dEffecdate")
                    lclsContrat_Pay_Prod.dNulldate = .FieldToClass("dNulldate")
                    lclsContrat_Pay_Prod.sClient = .FieldToClass("sClient")
                    lclsContrat_Pay_Prod.sDescript = .FieldToClass("sDescript")
                    lclsContrat_Pay_Prod.dStartdate = .FieldToClass("dStartDate")
                    lclsContrat_Pay_Prod.nType_Calc = .FieldToClass("nType_Calc")
                    lclsContrat_Pay_Prod.nPercent = .FieldToClass("nPercent")
                    lclsContrat_Pay_Prod.nAmount = .FieldToClass("nAmount")
                    lclsContrat_Pay_Prod.nCurrency = .FieldToClass("nCurrency")
                    lclsContrat_Pay_Prod.nAply = .FieldToClass("nAply")
                    lclsContrat_Pay_Prod.sTaxin = .FieldToClass("sTaxin")
                    lclsContrat_Pay_Prod.sStatregt = .FieldToClass("sStatregt")
                    lclsContrat_Pay_Prod.dCompdate = .FieldToClass("dCompdate")
                    lclsContrat_Pay_Prod.nUsercode = .FieldToClass("nUsercode")
                    lclsContrat_Pay_Prod.nTyp_acco = .FieldToClass("nTyp_acco")
                    lclsContrat_Pay_Prod.NAMOUNT_INI = .FieldToClass("nAmount_Ini")
                    lclsContrat_Pay_Prod.SROUTINE = .FieldToClass("sRoutine")
                    lclsContrat_Pay_Prod.NTYPE_CONTRAT = .FieldToClass("nType_Contrat")
                    lclsContrat_Pay_Prod.NMODULEC = .FieldToClass("nModulec")
                    lclsContrat_Pay_Prod.NPOLICY_DUR = .FieldToClass("nPolicy_Dur")
                    lclsContrat_Pay_Prod.NAGE_INIT = .FieldToClass("nAge_Init")
                    lclsContrat_Pay_Prod.NAGE_END = .FieldToClass("nAge_End")

                    Call Add(lclsContrat_Pay_Prod)
                    'UPGRADE_NOTE: Object lclsContrat_Pay_Detail may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
                    lclsContrat_Pay_Prod = Nothing
                    .RNext()
                Loop
            End If
        End With

Find_Err:
        If Err.Number Then
            Find = False
        End If
        'UPGRADE_NOTE: Object lrecContrat_Pay_Detail may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecContrat_Pay_Prod = Nothing
        'UPGRADE_NOTE: Object lclsContrat_Pay_Detail may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsContrat_Pay_Prod = Nothing
        On Error GoTo 0

    End Function

    '%Item: Devuelve un elemento de la colección (segun índice)
    Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Contrat_Pays
        Get
            Item = mCol.Item(vntIndexKey)
        End Get
    End Property

    '%Count: Devuelve el número de elementos que posee la colección
    Public ReadOnly Property Count() As Integer
        Get
            Count = mCol.Count()
        End Get
    End Property

    '%NewEnum: Permite enumerar la colección para utilizarla en un ciclo For Each... Next
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

    '%Remove: Elimina un elemento de la colección
    Public Sub Remove(ByRef vntIndexKey As Object)
        mCol.Remove(vntIndexKey)
    End Sub

    '%Class_Initialize: Controla la creación de una instancia de la colección
    'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
    Private Sub Class_Initialize_Renamed()
        mCol = New Collection
    End Sub
    Public Sub New()
        MyBase.New()
        Class_Initialize_Renamed()
    End Sub

    '%Class_Terminate: Controla la destrucción de una instancia de la colección
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

