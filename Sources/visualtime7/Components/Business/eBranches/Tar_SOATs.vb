Option Strict Off
Option Explicit On
Public Class Tar_Soats
    Implements System.Collections.IEnumerable
    '**+Objective: Collection that supports the class 'Tar_SOAT'.
    '**+Version: $$Revision: 4 $
    '+Objetivo: Colección que le da soporte a la clase 'Tar_SOAT'.
    '+Version: $$Revision: 4 $

    '**+Objective: Local variable to hold collection.
    '+Objetivo: Variable Local para almacenar la colección.
    Private mcolTar_SOAT As Collection

    '**%Objective: It adds an element to the collection.
    '**%Parameters:
    '**%    lclsTar_SOAT -
    '%Objetivo: Agrega un elemento a la colección.
    '%Parámetros:
    '%    lclsTar_SOAT -
    Public Function Add(ByRef lclsTar_SOAT As Tar_SOAT) As Tar_SOAT
        '**-set the properties passed into the method
        mcolTar_SOAT.Add(lclsTar_SOAT)

        '**-return the object created
        Add = lclsTar_SOAT
        lclsTar_SOAT = Nothing

        Exit Function
    End Function

    '**%Objective: Function that makes the search in the table 'Tar_SOAT'.
    '**%Parameters:
    '**%    Pending   -
    '%Objetivo: Función que realiza la busqueda en la tabla 'Tar_SOAT'.
    '%Parámetros:
    '%    Pendiente -
    Public Function Find(ByVal ncurrency As Short, ByVal dEffecDate As Date, ByVal nBranch As Short, ByVal nProduct As Short) As Boolean
        Dim lclsTar_SOAT As eRemoteDB.Execute
        Dim lclsTar_SOATItem As Tar_SOAT

        On Error GoTo ErrorHandler

        lclsTar_SOAT = New eRemoteDB.Execute

        '+ Define all parameters for the stored procedures 'insudb.reaTar_SOAT'. Generated on 05/01/2005 11:41:59 AM
        With lclsTar_SOAT
            .StoredProcedure = "reaTar_SOAT_a"
            .Parameters.Add("ncurrency", ncurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecDate", dEffecDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run(True) Then
                Do While Not .EOF
                    lclsTar_SOATItem = New Tar_SOAT
                    lclsTar_SOATItem.nCurrency = .FieldToClass("ncurrency")
                    lclsTar_SOATItem.dEffecdate = .FieldToClass("dEffecDate")
                    lclsTar_SOATItem.nBranch = .FieldToClass("nBranch")
                    lclsTar_SOATItem.nProduct = .FieldToClass("nProduct")
                    lclsTar_SOATItem.nTypeCalculate = .FieldToClass("nTypeCalculate")
                    lclsTar_SOATItem.nGroupVeh = .FieldToClass("nGroupVeh")
                    lclsTar_SOATItem.nTariff = .FieldToClass("nTariff")
                    lclsTar_SOATItem.nVehType = .FieldToClass("nVehType")
                    lclsTar_SOATItem.nLocat_Type = .FieldToClass("nLocat_Type")
                    lclsTar_SOATItem.nPremiumn = .FieldToClass("npremiumn")
                    lclsTar_SOATItem.nPremiumTar = .FieldToClass("nPremiumTar")
                    lclsTar_SOATItem.dNullDate = .FieldToClass("dNullDate")
                    lclsTar_SOATItem.bEditRecord = IIf(.FieldToClass("dNullDate") = dtmNull, True, False)
                    Call Add(lclsTar_SOATItem)
                    lclsTar_SOATItem = Nothing
                    .RNext()
                Loop
                Find = True
                .RCloseRec()
            Else
                Find = False
            End If
        End With
        lclsTar_SOAT = Nothing

        Exit Function
ErrorHandler:
        If Err.Number Then
            Find = False
        End If
    End Function

    Public Function FindMSO8500(ByVal ncurrency As Short, ByVal dEffecDate As Date, ByVal nBranch As Short, ByVal nProduct As Short) As Boolean
        Dim lclsTar_SOAT As eRemoteDB.Execute
        Dim lclsTar_SOATItem As Tar_SOAT

        On Error GoTo ErrorHandler

        lclsTar_SOAT = New eRemoteDB.Execute

        '+ Define all parameters for the stored procedures 'insudb.reaTar_SOAT'. Generated on 05/01/2005 11:41:59 AM
        With lclsTar_SOAT
            .StoredProcedure = "REATARIF_SOAT"
            .Parameters.Add("ncurrency", ncurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecDate", dEffecDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run(True) Then
                Do While Not .EOF
                    lclsTar_SOATItem = New Tar_SOAT
                    lclsTar_SOATItem.nCurrency = .FieldToClass("ncurrency")
                    lclsTar_SOATItem.dEffecdate = .FieldToClass("dEffecDate")
                    lclsTar_SOATItem.nBranch = .FieldToClass("nBranch")
                    lclsTar_SOATItem.nProduct = .FieldToClass("nProduct")
                    lclsTar_SOATItem.nTypeCalculate = .FieldToClass("nTypeCalculatePremsoat")
                    lclsTar_SOATItem.nGroupVeh = .FieldToClass("nGroupVeh")
                    lclsTar_SOATItem.nVehType = .FieldToClass("nVehtype")
                    lclsTar_SOATItem.nLocat_Type = .FieldToClass("nAutozone")
                    lclsTar_SOATItem.nPremiumn = .FieldToClass("nPremium")
                    lclsTar_SOATItem.nVehBrand = .FieldToClass("nVehBrand")
                    lclsTar_SOATItem.sVehModel = .FieldToClass("sVehModel")
                    lclsTar_SOATItem.nPlace = .FieldToClass("nPlace")
                    lclsTar_SOATItem.nPersontyp = .FieldToClass("nPersontyp")
                    lclsTar_SOATItem.nTypePremium = .FieldToClass("nTypePremium")
                    lclsTar_SOATItem.dNullDate = .FieldToClass("dNullDate")
                    lclsTar_SOATItem.bEditRecord = IIf(.FieldToClass("dNullDate") = dtmNull, True, False)
                    Call Add(lclsTar_SOATItem)
                    lclsTar_SOATItem = Nothing
                    .RNext()
                Loop
                FindMSO8500 = True
                .RCloseRec()
            Else
                FindMSO8500 = False
            End If
        End With
        lclsTar_SOAT = Nothing

        Exit Function
ErrorHandler:
        If Err.Number Then
            FindMSO8500 = False
        End If
    End Function

    Public Function FindMSO009(ByVal ncurrency As Short, ByVal dEffecDate As Date, ByVal nBranch As Short, ByVal nProduct As Short) As Boolean
        Dim lclsTar_SOAT As eRemoteDB.Execute
        Dim lclsTar_SOATItem As Tar_SOAT

        On Error GoTo ErrorHandler

        lclsTar_SOAT = New eRemoteDB.Execute

        '+ Define all parameters for the stored procedures 'insudb.reaTar_SOAT'. Generated on 05/01/2005 11:41:59 AM
        With lclsTar_SOAT
            .StoredProcedure = "REATARIF_SOAT"
            .Parameters.Add("ncurrency", ncurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecDate", dEffecDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run(True) Then
                Do While Not .EOF
                    lclsTar_SOATItem = New Tar_SOAT
                    lclsTar_SOATItem.nCurrency = .FieldToClass("ncurrency")
                    lclsTar_SOATItem.dEffecdate = .FieldToClass("dEffecDate")
                    lclsTar_SOATItem.nBranch = .FieldToClass("nBranch")
                    lclsTar_SOATItem.nProduct = .FieldToClass("nProduct")
                    lclsTar_SOATItem.nTypeCalculate = .FieldToClass("nTypeCalculatePremsoat")
                    lclsTar_SOATItem.nGroupVeh = .FieldToClass("nGroupVeh")
                    lclsTar_SOATItem.nVehType = .FieldToClass("nVehtype")
                    lclsTar_SOATItem.nLocat_Type = .FieldToClass("nAutozone")
                    lclsTar_SOATItem.nPremiumn = .FieldToClass("nPremium")
                    lclsTar_SOATItem.nVehBrand = .FieldToClass("nVehBrand")
                    lclsTar_SOATItem.sVehModel = .FieldToClass("sVehModel")
                    lclsTar_SOATItem.nPlace = .FieldToClass("nPlace")
                    lclsTar_SOATItem.nPersontyp = .FieldToClass("nPersontyp")
                    lclsTar_SOATItem.nTypePremium = .FieldToClass("nTypePremium")
                    lclsTar_SOATItem.dNullDate = .FieldToClass("dNullDate")
                    lclsTar_SOATItem.bEditRecord = IIf(.FieldToClass("dNullDate") = dtmNull, True, False)
                    Call Add(lclsTar_SOATItem)
                    lclsTar_SOATItem = Nothing
                    .RNext()
                Loop
                FindMSO009 = True
                .RCloseRec()
            Else
                FindMSO009 = False
            End If
        End With
        lclsTar_SOAT = Nothing

        Exit Function
ErrorHandler:
        If Err.Number Then
            FindMSO009 = False
        End If
    End Function


    '**%Objective: This property is used when reference to an element becomes of the collection.
    '**%Parameters:
    '**%    vIndexKey   -
    '%Objetivo: Esta propiedad es usada cuando se hace referencia a un elemento de la colección.
    '%Parámetros:
    '%    vIndexKey -
    Public ReadOnly Property Item(ByVal vIndexKey As Object) As Tar_SOAT
        Get

            Item = mcolTar_SOAT.Item(vIndexKey)

            Exit Property

        End Get
    End Property

    '**%Objective: It returns the amount of existing elements in the collection.
    '%Objetivo: Retorna la contidad de elementos existentes en la colección.
    Public ReadOnly Property Count() As Integer
        Get

            Count = mcolTar_SOAT.Count()
            Exit Property

        End Get
    End Property

    Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
        GetEnumerator = mcolTar_SOAT.GetEnumerator
    End Function

    '**%Objective: It allows to remove an element of the collection.
    '**%Parameters:
    '**%    vIndexKey   -
    '%Objetivo: Permite eliminar un elemento de la colección.
    '%Parámetros:
    '%    vIndexKey -
    Public Sub Remove(ByRef vIndexKey As Object)

        mcolTar_SOAT.Remove(vIndexKey)

        Exit Sub

    End Sub

    '**%Objective: Creates the collection when this class is created.
    '%Objetivo: Crea la colección cuando se crea esta clase.
    Private Sub Class_Initialize_Renamed()

        mcolTar_SOAT = New Collection

        Exit Sub

    End Sub
    Public Sub New()
        MyBase.New()
        Class_Initialize_Renamed()
    End Sub

    '**%Objective: Destroys collection when this class is terminated.
    '%Objetivo: Destruye la colección cuando se termina esta clase.
    Private Sub Class_Terminate_Renamed()

        mcolTar_SOAT = Nothing

        Exit Sub

    End Sub
    Protected Overrides Sub Finalize()
        Class_Terminate_Renamed()
        MyBase.Finalize()
    End Sub
End Class






