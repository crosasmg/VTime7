Option Strict Off
Option Explicit On
Public Class tar_tr_mers
    Implements System.Collections.IEnumerable
    '**+Objective: Collection that supports the class 'tar_tr_mer'.
    '**+Version: $$Revision: 4 $
    '+Objetivo: Colección que le da soporte a la clase 'tar_tr_mer'.
    '+Version: $$Revision: 4 $

    '**+Objective: Local variable to hold collection.
    '+Objetivo: Variable Local para almacenar la colección.
    Private mcoltar_tr_mer As Collection



    '**%Objective: It adds an element to the collection.
    '**%Parameters:
    '**%    lclstar_tr_mer -
    '%Objetivo: Agrega un elemento a la colección.
    '%Parámetros:
    '%    lclstar_tr_mer -
    Public Function Add(ByRef lclstar_tr_mer As tar_tr_mer) As tar_tr_mer

        '**-set the properties passed into the method


        mcoltar_tr_mer.Add(lclstar_tr_mer)

        '**-return the object created
        Add = lclstar_tr_mer

        Exit Function
    End Function

    '**%Objective: Function that makes the search in the table 'tar_tr_mer'.
    '**%Parameters:
    '**%    nBranch     - Code of the commercial branch.
    '**%    nProduct    - Code of the product.
    '**%    nCurrency   - Code of the currency.
    '**%    dEffecdate  - Date which from the record is valid.
    '%Objetivo: Función que realiza la busqueda en la tabla 'tar_tr_mer'.
    '%Parámetros:
    '%    nBranch     - Codigo del ramo comercial.
    '%    nProduct    - Codigo del producto.
    '%    nCurrency   - Código de la moneda.
    '%    dEffecdate  - Fecha de efecto del registro.
    Public Function Find(ByVal nBranch As Short, ByVal nProduct As Short, ByVal nCurrency As Short, ByVal dEffecDate As Date) As Boolean
        Dim lclstar_tr_mer As eRemoteDB.Execute
        Dim lclstar_tr_merItem As tar_tr_mer


        lclstar_tr_mer = New eRemoteDB.Execute

        '+ Define all parameters for the stored procedures 'insudb.reatar_tr_mer'. Generated on 30/08/2004 02:28:11 p.m.
        With lclstar_tr_mer
            .StoredProcedure = "reatar_tr_mer_a"
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecDate", dEffecDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run(True) Then
                Do While Not .EOF
                    lclstar_tr_merItem = New tar_tr_mer
                    lclstar_tr_merItem.nBranch = .FieldToClass("nBranch")
                    lclstar_tr_merItem.nProduct = .FieldToClass("nProduct")
                    lclstar_tr_merItem.nCurrency = .FieldToClass("nCurrency")
                    lclstar_tr_merItem.dEffecDate = .FieldToClass("dEffecDate")
                    lclstar_tr_merItem.nClassMerch = .FieldToClass("nClassMerch")
                    lclstar_tr_merItem.nPacking = .FieldToClass("nPacking")
                    lclstar_tr_merItem.nRate = .FieldToClass("nRate")
                    Call Add(lclstar_tr_merItem)
                    lclstar_tr_merItem = Nothing
                    .RNext()
                Loop
                Find = True
                .RCloseRec()
            Else
                Find = False
            End If
        End With

        lclstar_tr_mer = Nothing
        lclstar_tr_merItem = Nothing

        Exit Function
    End Function

    '**%Objective: This property is used when reference to an element becomes of the collection.
    '**%Parameters:
    '**%    vIndexKey   -
    '%Objetivo: Esta propiedad es usada cuando se hace referencia a un elemento de la colección.
    '%Parámetros:
    '%    vIndexKey -
    Public ReadOnly Property Item(ByVal vIndexKey As Object) As tar_tr_mer
        Get


            Item = mcoltar_tr_mer.Item(vIndexKey)

            Exit Property
        End Get
    End Property

    '**%Objective: It returns the amount of existing elements in the collection.
    '%Objetivo: Retorna la contidad de elementos existentes en la colección.
    Public ReadOnly Property Count() As Integer
        Get


            Count = mcoltar_tr_mer.Count()

            Exit Property
        End Get
    End Property

    '**%Objective: This property allows you to enumerate this collection with the "For...Each".
    '%Objetivo: Esta propiedad permite enumerar los elementos de la colección por medio de un "For...Each".
    'UPGRADE_NOTE: NewEnum property was commented out. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="B3FC1610-34F3-43F5-86B7-16C984F0E88E"'
    'Public ReadOnly Property NewEnum() As stdole.IUnknown
    'Get

    '
    'NewEnum = mcoltar_tr_mer._NewEnum
    '
    'Exit Property
    'ErrorHandler: '
    'ProcError("tar_tr_mers.NewEnum()")
    'End Get
    'End Property

    Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
        GetEnumerator = mcoltar_tr_mer.GetEnumerator
    End Function

    '**%Objective: It allows to remove an element of the collection.
    '**%Parameters:
    '**%    vIndexKey   -
    '%Objetivo: Permite eliminar un elemento de la colección.
    '%Parámetros:
    '%    vIndexKey -
    Public Sub Remove(ByRef vIndexKey As Object)


        mcoltar_tr_mer.Remove(vIndexKey)

        Exit Sub
    End Sub

    '**%Objective: Creates the collection when this class is created.
    '%Objetivo: Crea la colección cuando se crea esta clase.
    Private Sub Class_Initialize_Renamed()



        mcoltar_tr_mer = New Collection

        Exit Sub
    End Sub
    Public Sub New()
        MyBase.New()
        Class_Initialize_Renamed()
    End Sub

    '**%Objective: Destroys collection when this class is terminated.
    '%Objetivo: Destruye la colección cuando se termina esta clase.
    Private Sub Class_Terminate_Renamed()


        Exit Sub
    End Sub
    Protected Overrides Sub Finalize()
        Class_Terminate_Renamed()
        MyBase.Finalize()
    End Sub
End Class











