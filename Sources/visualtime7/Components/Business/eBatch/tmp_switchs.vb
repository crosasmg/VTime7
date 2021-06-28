Option Strict Off
Option Explicit On
'UPGRADE_WARNING: Class instancing was changed to public. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="ED41034B-3890-49FC-8076-BD6FC2F42A85"'
Public Class tmp_switchs

    Implements System.Collections.IEnumerable
    'variable local para contener colección
    Private mCol As Collection
    Public nCountOrigin As Integer

    Public Function Add(ByVal objClass As Tmp_switch) As Tmp_switch
        'crear un nuevo objeto
        If objClass Is Nothing Then
            objClass = New tmp_switch
        End If

        With objClass
            mCol.Add(objClass, CStr(.nId))
        End With

        'return the object created
        Add = objClass
    End Function

    Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As tmp_switch
        Get
            'se usa al hacer referencia a un elemento de la colección
            'vntIndexKey contiene el índice o la clave de la colección,
            'por lo que se declara como un Variant
            'Syntax: Set foo = x.Item(xyz) or Set foo = x.Item(5)
            Item = mCol.Item(vntIndexKey)
        End Get
    End Property



    Public ReadOnly Property Count() As Integer
        Get
            'se usa al obtener el número de elementos de la
            'colección. Sintaxis: Debug.Print x.Count
            Count = mCol.Count
        End Get
    End Property


    Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
        'UPGRADE_TODO: Uncomment and change the following line to return the collection enumerator. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="95F9AAD0-1319-4921-95F0-B9D3C4FF7F1C"'
        GetEnumerator = mCol.GetEnumerator
    End Function

    Public Sub Remove(ByVal vntIndexKey As Object)
        'se usa al quitar un elemento de la colección
        'vntIndexKey contiene el índice o la clave, por lo que se
        'declara como un Variant
        'Sintaxis: x.Remove(xyz)


        mCol.Remove(vntIndexKey)
    End Sub

    '% Class_Initialize: controla la creación de la instancia del objeto de la clase
    'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
    Private Sub Class_Initialize_Renamed()
        mCol = New Collection
    End Sub
    Public Sub New()
        MyBase.New()
        Class_Initialize_Renamed()
    End Sub

    '% Class_Terminate: controla la destrucción de la instancia del objeto de la clase
    'UPGRADE_NOTE: Class_Terminate was upgraded to Class_Terminate_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
    Private Sub Class_Terminate_Renamed()

        'UPGRADE_NOTE: Object mCol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        mCol = Nothing
    End Sub
    Protected Overrides Sub Finalize()
        Class_Terminate_Renamed()
        MyBase.Finalize()
    End Sub



    '% Find: se buscan los elementos asociados a una tabla temporal
    '------------------------------------------------------------------------------------------
    Public Function Find(ByVal sKey As String, _
                         ByVal nOrigin As Long) As Boolean
        '------------------------------------------------------------------------------------------
        Dim lrecReatmp_switch As eRemoteDB.Execute
        Dim lobjtmp_switch As tmp_switch
        On Error GoTo Find_Err
        mCol = New Collection
        lrecReatmp_switch = New eRemoteDB.Execute

        'Definición de parámetros para stored procedure 'insudb.Deltmp_switch'

        With lrecReatmp_switch
            .StoredProcedure = "insVI017pkg.reavi017"
            .Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nOrigin", nOrigin, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            If .Run Then
                Find = True
                Do While Not .EOF
                    lobjtmp_switch = New tmp_switch
                    lobjtmp_switch.nId = .FieldToClass("nId")
                    lobjtmp_switch.nFunds_sell = .FieldToClass("nFunds_sell")
                    lobjtmp_switch.nCount_Sell = .FieldToClass("nCount_Sell")
                    lobjtmp_switch.nTyp_profitworker_sell = .FieldToClass("nTyp_profitworker_sell")
                    lobjtmp_switch.sTyp_profitworker_sell = .FieldToClass("sTyp_profitworker_sell")
                    lobjtmp_switch.sFund_sell = .FieldToClass("sFund_sell")
                    lobjtmp_switch.nQuan_avail_sell = .FieldToClass("nQuan_avail_sell")
                    lobjtmp_switch.nQuan_avail_sell_uf = .FieldToClass("nQuan_avail_sell_uf")
                    lobjtmp_switch.nPercent_sell = .FieldToClass("nPercent_sell")
                    lobjtmp_switch.nAmount_mov = .FieldToClass("nAmount_mov_sell")
                    Call Add(lobjtmp_switch)
                    .RNext()
                Loop
                .RCloseRec()
            Else
                Find = False
            End If

        End With

        lrecReatmp_switch = Nothing

Find_Err:
        If Err.Number Then
            Find = False
        End If
    End Function

    '% Find: se buscan los elementos asociados a una tabla temporal
    '------------------------------------------------------------------------------------------
    Public Function Find_1(ByVal sKey As String, _
                           ByVal nFund_sell As Long, _
                           ByVal nTyp_profitworker_sell As Long, _
                           ByVal nOrigin As Long) As Boolean
        '------------------------------------------------------------------------------------------
        Dim lrecReatmp_switch As eRemoteDB.Execute
        Dim lobjtmp_switch As tmp_switch
        On Error GoTo Find_Err
        mCol = New Collection
        lrecReatmp_switch = New eRemoteDB.Execute

        'Definición de parámetros para stored procedure 'insudb.Deltmp_switch'

        With lrecReatmp_switch
            .StoredProcedure = "insVI017pkg.reavi017_1"
            .Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nFund_sell", nFund_sell, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTyp_profitworker_sell", nTyp_profitworker_sell, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nOrigin", nOrigin, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run Then
                Find_1 = True
                Do While Not .EOF
                    lobjtmp_switch = New tmp_switch
                    lobjtmp_switch.nId = .FieldToClass("nId")
                    lobjtmp_switch.nPercent_buy = .FieldToClass("nPercent_buy")
                    lobjtmp_switch.sFund_buy = .FieldToClass("sFund_buy")
                    lobjtmp_switch.sTyp_profitworker_buy = .FieldToClass("sTyp_profitworker_buy")
                    Call Add(lobjtmp_switch)
                    .RNext()
                Loop
                .RCloseRec()
            Else
                Find_1 = False
            End If

        End With

        lrecReatmp_switch = Nothing

Find_Err:
        If Err.Number Then
            Find_1 = False
        End If
    End Function


    '% Find_2: se buscan los elementos asociados a una tabla temporal
    '------------------------------------------------------------------------------------------
    Public Function Find_2(ByVal sKey As String, _
                           ByVal nOrigin As Long) As Boolean
        '------------------------------------------------------------------------------------------
        Dim lrecReatmp_switch As eRemoteDB.Execute
        Dim lobjtmp_switch As tmp_switch
        On Error GoTo Find_Err
        mCol = New Collection
        lrecReatmp_switch = New eRemoteDB.Execute

        'Definición de parámetros para stored procedure 'insudb.Deltmp_switch'

        With lrecReatmp_switch
            .StoredProcedure = "insVI017pkg.reavi017_2"
            .Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nOrigin", nOrigin, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run Then
                Find_2 = True
                Do While Not .EOF
                    lobjtmp_switch = New tmp_switch
                    lobjtmp_switch.nId = .FieldToClass("nId")
                    lobjtmp_switch.nPercent_buy = .FieldToClass("nPercent_buy")
                    lobjtmp_switch.sFund_buy = .FieldToClass("sFund_buy")
                    lobjtmp_switch.sTyp_profitworker_buy = .FieldToClass("sTyp_profitworker_buy")
                    lobjtmp_switch.nQuan_avail_buy_switch = .FieldToClass("nQuan_avail_buy_switch")
                    lobjtmp_switch.nAmount_mov = .FieldToClass("nAmount_mov_buy")
                    lobjtmp_switch.nQuot_Value_Buy = .FieldToClass("nQuot_Value_Buy")
                    Call Add(lobjtmp_switch)
                    .RNext()
                Loop
                .RCloseRec()
            Else
                Find_2 = False
            End If

        End With

        lrecReatmp_switch = Nothing

Find_Err:
        If Err.Number Then
            Find_2 = False
        End If
    End Function

    '% Find_3: se buscan los elementos asociados a una tabla temporal
    '------------------------------------------------------------------------------------------
    Public Function Find_3(ByVal sKey As String, _
                           ByVal nOrigin As Long) As Boolean
        '------------------------------------------------------------------------------------------
        Dim lrecReatmp_switch As eRemoteDB.Execute
        Dim lobjtmp_switch As tmp_switch
        On Error GoTo Find_Err
        mCol = New Collection
        lrecReatmp_switch = New eRemoteDB.Execute

        'Definición de parámetros para stored procedure 'insudb.Deltmp_switch'

        With lrecReatmp_switch
            .StoredProcedure = "insVI017pkg.reavi017_3"
            .Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nOrigin", nOrigin, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run Then
                Find_3 = True
                Do While Not .EOF
                    lobjtmp_switch = New tmp_switch
                    lobjtmp_switch.nId = .FieldToClass("nId")
                    lobjtmp_switch.nPercent_buy = .FieldToClass("nPercent_buy")
                    lobjtmp_switch.sFund_buy = .FieldToClass("sFund_buy")
                    lobjtmp_switch.nQuan_avail_buy_switch = .FieldToClass("nQuan_avail_buy_switch")
                    lobjtmp_switch.nAmount_mov = .FieldToClass("nAmount_mov_buy")
                    lobjtmp_switch.nQuot_Value_Buy = .FieldToClass("nQuot_Value_Buy")
                    Call Add(lobjtmp_switch)
                    .RNext()
                Loop
                .RCloseRec()
            Else
                Find_3 = False
            End If

        End With

        lrecReatmp_switch = Nothing

Find_Err:
        If Err.Number Then
            Find_3 = False
        End If
    End Function

    '% Find_Origin: se buscan las cuentas origen disponibles 
    '------------------------------------------------------------------------------------------
    Public Function Find_TabOriginPol(ByVal sShowNum As String, _
                                      ByVal sCondition As String, _
                                      ByVal nBranch As Long, _
                                      ByVal nProduct As Long, _
                                      ByVal nPolicy As Double) As Boolean
        '------------------------------------------------------------------------------------------
        Dim lrecReatmp_switch As eRemoteDB.Execute
        Dim lobjtmp_switch As tmp_switch
        On Error GoTo Find_Err
        'mCol = New Collection
        lrecReatmp_switch = New eRemoteDB.Execute

        'Definición de parámetros para stored procedure 'insudb.Deltmp_switch'
        nCountOrigin = 0

        With lrecReatmp_switch
            .StoredProcedure = "TAB_ORIGINPOLPKG.TAB_ORIGINPOL"
            .Parameters.Add("sShowNum", sShowNum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sCondition", sCondition, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 255, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run Then
                Find_TabOriginPol = True
                Do While Not .EOF
                    nCountOrigin = nCountOrigin + 1
                    lobjtmp_switch = New tmp_switch
                    lobjtmp_switch.nOrigin = .FieldToClass("nOrigin")
                    lobjtmp_switch.sOrigin = .FieldToClass("sDescript")
                    Call Add(lobjtmp_switch)
                    .RNext()
                Loop
                .RCloseRec()
            Else
                Find_TabOriginPol = False
            End If

        End With

        lrecReatmp_switch = Nothing

Find_Err:
        If Err.Number Then
            Find_TabOriginPol = False
        End If
    End Function

End Class


