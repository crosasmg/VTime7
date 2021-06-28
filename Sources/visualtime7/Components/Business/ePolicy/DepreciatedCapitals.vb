Option Strict Off
Option Explicit On
Public Class DepreciatedCapitals
    Implements System.Collections.IEnumerable
    '%-------------------------------------------------------%'
    '% $Workfile:: DepreciatedCapitals.cls                          $%'
    '% $Author:: Nvaplat7                                   $%'
    '% $Date:: 9/08/03 1:06p                                $%'
    '% $Revision:: 7                                        $%'
    '%-------------------------------------------------------%'

    'local variable to hold collection
    Private mCol As Collection


    '%Add: Agrega un elemento a la colección
    Public Function Add(ByVal lclsDepreciatedCapital As DepreciatedCapital) As DepreciatedCapital
        With lclsDepreciatedCapital
            mCol.Add(lclsDepreciatedCapital, "CT" & .dEffecdate & .nProduct & .nBranch & .nCapital & .sCertype & .nPolicy & .nCertif & .dExpirdat & .dStartdate)
        End With
        '+ Devuelve el objeto creado.
        Add = lclsDepreciatedCapital
        'UPGRADE_NOTE: Object lclsDepreciatedCapital may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsDepreciatedCapital = Nothing
    End Function

    '% FindMCA709: Devuelve una coleccion de objetos de tipo DepreciatedCapital
    '------------------------------------------------------------
    Public Function Find(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nGroup_insu As Double, ByVal nModulec As Double, ByVal nCover As Double, ByVal dEffecdate As Date, Optional ByVal lblnFind As Boolean = False) As Boolean
        '------------------------------------------------------------
        Dim lclsDepreciatedCapital As DepreciatedCapital
        '- Se define la variable lrecDepreciatedCapital que se utilizará como cursor.
        Dim lrecDepreciatedCapital As eRemoteDB.Execute

        On Error GoTo Find_Err

        lrecDepreciatedCapital = New eRemoteDB.Execute

        Find = True

        '+ Se ejecuta el Store procedure que busca los movimientos de un Ramo/Producto
        With lrecDepreciatedCapital
            .StoredProcedure = "reaDepreciatedCapital_a"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nGroup_insu", nGroup_insu, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If Not .Run Then
                Find = False
            Else
                Find = True
                Do While Not .EOF
                    lclsDepreciatedCapital = New DepreciatedCapital

                    lclsDepreciatedCapital.sCertype = .FieldToClass("sCertype")
                    lclsDepreciatedCapital.nBranch = .FieldToClass("nBranch")
                    lclsDepreciatedCapital.nProduct = .FieldToClass("nProduct")
                    lclsDepreciatedCapital.nPolicy = .FieldToClass("nPolicy")
                    lclsDepreciatedCapital.nCertif = .FieldToClass("nCertif")
                    lclsDepreciatedCapital.nGroup_insu = .FieldToClass("nGroup_insu")
                    lclsDepreciatedCapital.nModulec = .FieldToClass("nModulec")
                    lclsDepreciatedCapital.nCover = .FieldToClass("nCover")
                    lclsDepreciatedCapital.dNulldate = .FieldToClass("dNulldate")
                    lclsDepreciatedCapital.dEffecdate = .FieldToClass("dEffecdate")
                    lclsDepreciatedCapital.dStartdate = .FieldToClass("dStartdate")
                    lclsDepreciatedCapital.nCapital = .FieldToClass("nCapital")
                    lclsDepreciatedCapital.dExpirdat = .FieldToClass("dExpirdat")
                    lclsDepreciatedCapital.nEndorsementValue = .FieldToClass("nEndorsementValue")

                    Call Add(lclsDepreciatedCapital)
                    'UPGRADE_NOTE: Object lclsDepreciatedCapital may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
                    lclsDepreciatedCapital = Nothing
                    .RNext()
                Loop
            End If
        End With

Find_Err:
        If Err.Number Then
            Find = False
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lrecDepreciatedCapital may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecDepreciatedCapital = Nothing
    End Function

    '* Item: devuelve un elemento de la colección (según índice, o llave)
    '------------------------------------------------------------
    Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As DepreciatedCapital
        Get
            '------------------------------------------------------------
            '+ used when referencing an element in the collection
            '+ vntIndexKey contains either the Index or Key to the collection,
            '+ this is why it is declared as a Variant
            '+ Syntax: Set foo = x.Item(xyz) or Set foo = x.Item(5)
            Item = mCol.Item(vntIndexKey)
        End Get
    End Property

    '* Count: devuelve el número de elementos que posee la colección
    '------------------------------------------------------------
    Public ReadOnly Property Count() As Integer
        Get
            '------------------------------------------------------------
            '+ used when retrieving the number of elements in the
            '+ collection. Syntax: Debug.Print x.Count
            Count = mCol.Count()
        End Get
    End Property

    '* NewEnum: permite enumerar la colección para utilizarla en un ciclo For Each... Next
    '------------------------------------------------------------
    'UPGRADE_NOTE: NewEnum property was commented out. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="B3FC1610-34F3-43F5-86B7-16C984F0E88E"'
    'Public ReadOnly Property NewEnum() As stdole.IUnknown
    'Get
    '------------------------------------------------------------
    '+ this property allows you to enumerate
    '+ this collection with the For...Each syntax
    'NewEnum = mCol._NewEnum
    'End Get
    'End Property

    Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
        'UPGRADE_TODO: Uncomment and change the following line to return the collection enumerator. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="95F9AAD0-1319-4921-95F0-B9D3C4FF7F1C"'
        GetEnumerator = mCol.GetEnumerator
    End Function

    '* Remove: elimina un elemento de la colección
    '------------------------------------------------------------
    Public Sub Remove(ByRef vntIndexKey As Object)
        '------------------------------------------------------------
        '+ used when removing an element from the collection
        '+ vntIndexKey contains either the Index or Key, which is why
        '+ it is declared as a Variant
        '+ Syntax: x.Remove(xyz)

        mCol.Remove(vntIndexKey)
    End Sub

    '* Class_Initialize: controla la creación de la instancia del objeto de la colección
    '------------------------------------------------------------
    'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
    Private Sub Class_Initialize_Renamed()
        '------------------------------------------------------------
        '+ creates the collection when this class is created
        mCol = New Collection
    End Sub
    Public Sub New()
        MyBase.New()
        Class_Initialize_Renamed()
    End Sub

    '* Class_Terminate: controla la destrucción de la instancia del objeto de la colección
    '------------------------------------------------------------
    'UPGRADE_NOTE: Class_Terminate was upgraded to Class_Terminate_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
    Private Sub Class_Terminate_Renamed()
        '------------------------------------------------------------
        '+ destroys collection when this class is terminated
        'UPGRADE_NOTE: Object mCol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        mCol = Nothing
    End Sub
    Protected Overrides Sub Finalize()
        Class_Terminate_Renamed()
        MyBase.Finalize()
    End Sub
End Class






