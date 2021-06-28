Option Strict Off
Option Explicit On
Public Class Tran_rates
    Implements System.Collections.IEnumerable
    '**+Objective: Collection that supports the class 'tran_rate'.
    '**+Version: $$Revision: 2 $
    '+Objetivo: Colección que le da soporte a la clase 'tran_rate'.
    '+Version: $$Revision: 2 $

    '**+Objective: Local variable to hold collection.
    '+Objetivo: Variable Local para almacenar la colección.
    Private mcoltran_rate As Collection

    '**%Objective: It adds an element to the collection.
    '**%Parameters:
    '**%    lclstran_rate - Record from tran_rate table
    '%Objetivo: Agrega un elemento a la colección.
    '%Parámetros:
    '%    lclstran_rate - registro de la tabla Tran_rate
    Public Function Add(ByRef lclstran_rate As Tran_rate) As Tran_rate
        '**-set the properties passed into the method

        mcoltran_rate.Add(lclstran_rate)

        '**-return the object created
        Add = lclstran_rate

        Exit Function
    End Function

    '**%Objective: Function that makes the search in the table 'tran_rate'.
    '**%Parameters:
    '**%    sCertype   - Type of record
    '**%    nBranch    - Code of the line of business
    '**%    nProduct   - Code of the product
    '**%    nPolicy    - Policy number
    '**%    nCertif    - Number identifying the certificate
    '**%    dEffecdate - Effective date
    '%Objetivo: Función que realiza la busqueda en la tabla 'tran_rate'.
    '%Parámetros:
    '%    sCertype   - Tipo de registro
    '%    nBranch    - Código de la línea del negocio
    '%    nProduct   - Código del producto
    '%    nPolicy    - Número de la póliza
    '%    nCertif    - Número que identifica el certificado
    '%    dEffecdate - Fecha de efecto
    Public Function Find(ByVal sCertype As String, ByVal nBranch As Short, ByVal nProduct As Short, ByVal nPolicy As Integer, ByVal nCertif As Integer, ByVal dEffecdate As Date) As Boolean
        Dim lclstran_rate As eRemoteDB.Execute
        Dim lclstran_rateItem As Tran_rate

        lclstran_rate = New eRemoteDB.Execute

        '+ Define all parameters for the stored procedures 'insudb.reatran_rate'. Generated on 30/06/2004 11:43:55 a.m.
        With lclstran_rate
            .StoredProcedure = "reatran_rate_a"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run(True) Then
                Do While Not .EOF
                    lclstran_rateItem = New Tran_rate
                    lclstran_rateItem.sCertype = sCertype
                    lclstran_rateItem.nBranch = nBranch
                    lclstran_rateItem.nProduct = nProduct
                    lclstran_rateItem.nPolicy = nPolicy
                    lclstran_rateItem.nCertif = nCertif
                    lclstran_rateItem.nClassmerch = .FieldToClass("nClassmerch")
                    lclstran_rateItem.nPacking = .FieldToClass("nPacking")
                    lclstran_rateItem.nAmo_deduc = .FieldToClass("nAmo_deduc")
                    lclstran_rateItem.nDeductible = .FieldToClass("nDeductible")
                    lclstran_rateItem.nLimitcapital = .FieldToClass("nLimitcapital")
                    lclstran_rateItem.nMaxamount = .FieldToClass("nMaxamount")
                    lclstran_rateItem.nMinamount = .FieldToClass("nMinamount")
                    lclstran_rateItem.nRate = .FieldToClass("nRate")
                    lclstran_rateItem.sFrancapl = .FieldToClass("sFrancapl")
                    Call Add(lclstran_rateItem)
                    lclstran_rateItem = Nothing
                    .RNext()
                Loop
                Find = True
                .RCloseRec()
            Else
                Find = False
            End If
        End With

        lclstran_rate = Nothing
        lclstran_rateItem = Nothing

        Exit Function
    End Function


    Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Tran_rate
        Get
            'used when referencing an element in the collection
            'vntIndexKey contains either the Index or Key to the collection,
            'this is why it is declared as a Variant
            'Syntax: Set foo = x.Item(xyz) or Set foo = x.Item(5)
            Item = mcoltran_rate.Item(vntIndexKey)
        End Get
    End Property



    Public ReadOnly Property Count() As Integer
        Get
            'used when retrieving the number of elements in the
            'collection. Syntax: Debug.Print x.Count
            Count = mcoltran_rate.Count()
        End Get
    End Property


    'UPGRADE_NOTE: NewEnum property was commented out. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="B3FC1610-34F3-43F5-86B7-16C984F0E88E"'
    'Public ReadOnly Property NewEnum() As stdole.IUnknown
    'Get
    'this property allows you to enumerate
    'this collection with the For...Each syntax
    'NewEnum = mcoltran_rate._NewEnum
    'End Get
    'End Property

    Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
        GetEnumerator = mcoltran_rate.GetEnumerator
    End Function


    Public Sub Remove(ByRef vntIndexKey As Object)
        'used when removing an element from the collection
        'vntIndexKey contains either the Index or Key, which is why
        'it is declared as a Variant
        'Syntax: x.Remove(xyz)


        mcoltran_rate.Remove(vntIndexKey)
    End Sub


    Private Sub Class_Initialize_Renamed()
        'creates the collection when this class is created
        mcoltran_rate = New Collection
    End Sub
    Public Sub New()
        MyBase.New()
        Class_Initialize_Renamed()
    End Sub


    Private Sub Class_Terminate_Renamed()
        'destroys collection when this class is terminated
        mcoltran_rate = Nothing
    End Sub
    Protected Overrides Sub Finalize()
        Class_Terminate_Renamed()
        MyBase.Finalize()
    End Sub
End Class











