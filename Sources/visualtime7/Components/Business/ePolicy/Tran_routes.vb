Option Strict Off
Option Explicit On
Public Class tran_routes
    Implements System.Collections.IEnumerable
    '**+Objective: Collection that supports the class 'tran_route'.
    '**+Version: $$Revision: 2 $
    '+Objetivo: Colección que le da soporte a la clase 'tran_route'.
    '+Version: $$Revision: 2 $

    '**+Objective: Local variable to hold collection.
    '+Objetivo: Variable Local para almacenar la colección.
    Private mcoltran_route As Collection



    '**%Objective: It adds an element to the collection.
    '**%Parameters:
    '**%    lclstran_route -
    '%Objetivo: Agrega un elemento a la colección.
    '%Parámetros:
    '%    lclstran_route -
    Public Function Add(ByRef lclstran_route As tran_route) As tran_route
        '**-set the properties passed into the method


        mcoltran_route.Add(lclstran_route)

        '**-return the object created
        Add = lclstran_route

        Exit Function
    End Function

    '**%Objective: Function that makes the search in the table 'tran_route'.
    '**%Parameters:
    '**%    sCertype   - Type of record
    '**%    nBranch    - Code of the line of business
    '**%    nProduct   - Code of the product
    '**%    nPolicy    - Policy number
    '**%    nCertif    - Number identifying the certificate
    '**%    dEffecdate - Effective date
    '%Objetivo: Función que realiza la busqueda en la tabla 'tran_route'.
    '%Parámetros:
    '%    sCertype   - Tipo de registro
    '%    nBranch    - Código de la línea del negocio
    '%    nProduct   - Código del producto
    '%    nPolicy    - Número de la póliza
    '%    nCertif    - Número que identifica el certificado
    '%    dEffecdate - Fecha de efecto
    Public Function Find(ByVal sCertype As String, ByVal nBranch As Short, ByVal nProduct As Short, ByVal nPolicy As Integer, ByVal nCertif As Integer, ByVal dEffecdate As Date) As Boolean
        Dim lclstran_route As eRemoteDB.Execute
        Dim lclstran_routeItem As tran_route


        lclstran_route = New eRemoteDB.Execute

        '+ Define all parameters for the stored procedures 'insudb.reatran_route'. Generated on 30/06/2004 11:43:55 a.m.
        With lclstran_route
            .StoredProcedure = "reatran_route_a"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecDate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run(True) Then
                Do While Not .EOF
                    lclstran_routeItem = New tran_route
                    lclstran_routeItem.sCertype = sCertype
                    lclstran_routeItem.nBranch = nBranch
                    lclstran_routeItem.nProduct = nProduct
                    lclstran_routeItem.nPolicy = nPolicy
                    lclstran_routeItem.nCertif = nCertif
                    lclstran_routeItem.nRoute = .FieldToClass("nRoute")
                    lclstran_routeItem.nTypRoute = .FieldToClass("nTypRoute")
                    lclstran_routeItem.nNotenum = .FieldToClass("nNoteNum")
                    lclstran_routeItem.nTranspType = .FieldToClass("nTranspType")
                    Call Add(lclstran_routeItem)
                    lclstran_routeItem = Nothing
                    .RNext()
                Loop
                Find = True
                .RCloseRec()
            Else
                Find = False
            End If
        End With

        lclstran_route = Nothing
        lclstran_routeItem = Nothing

        Exit Function
    End Function

    '**%Objective: This property is used when reference to an element becomes of the collection.
    '**%Parameters:
    '**%    vIndexKey   -
    '%Objetivo: Esta propiedad es usada cuando se hace referencia a un elemento de la colección.
    '%Parámetros:
    '%    vIndexKey -
    Default Public ReadOnly Property Item(ByVal vIndexKey As Object) As tran_route
        Get


            Item = mcoltran_route.Item(vIndexKey)

            Exit Property
        End Get
    End Property

    '**%Objective: It returns the amount of existing elements in the collection.
    '%Objetivo: Retorna la contidad de elementos existentes en la colección.
    Public ReadOnly Property Count() As Integer
        Get


            Count = mcoltran_route.Count()

            Exit Property
        End Get
    End Property

    '**%Objective: This property allows you to enumerate this collection with the "For...Each".
    '%Objetivo: Esta propiedad permite enumerar los elementos de la colección por medio de un "For...Each".
    'UPGRADE_NOTE: NewEnum property was commented out. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="B3FC1610-34F3-43F5-86B7-16C984F0E88E"'
    'Public ReadOnly Property NewEnum() As stdole.IUnknown
    'Get

    '
    'NewEnum = mcoltran_route._NewEnum
    '
    'Exit Property
    'ErrorHandler: '
    'ProcError("tran_routes.NewEnum()")
    'End Get
    'End Property

    Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
        GetEnumerator = mcoltran_route.GetEnumerator
    End Function

    '**%Objective: It allows to remove an element of the collection.
    '**%Parameters:
    '**%    vIndexKey   -
    '%Objetivo: Permite eliminar un elemento de la colección.
    '%Parámetros:
    '%    vIndexKey -
    Public Sub Remove(ByRef vIndexKey As Object)


        mcoltran_route.Remove(vIndexKey)

        Exit Sub
    End Sub

    '**%Objective: Creates the collection when this class is created.
    '%Objetivo: Crea la colección cuando se crea esta clase.
    Private Sub Class_Initialize_Renamed()


        mcoltran_route = New Collection

        Exit Sub
    End Sub
    Public Sub New()
        MyBase.New()
        Class_Initialize_Renamed()
    End Sub

    '**%Objective: Destroys collection when this class is terminated.
    '%Objetivo: Destruye la colección cuando se termina esta clase.
    Private Sub Class_Terminate_Renamed()


        mcoltran_route = Nothing

        Exit Sub
    End Sub
    Protected Overrides Sub Finalize()
        Class_Terminate_Renamed()
        MyBase.Finalize()
    End Sub
End Class











