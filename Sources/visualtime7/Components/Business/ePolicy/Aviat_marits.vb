Option Strict Off
Option Explicit On
Public Class Aviat_marits
    Implements System.Collections.IEnumerable

    Public Function Find(ByVal sCertype As String, ByVal nBranch As Short, ByVal nProduct As Short, ByVal nPolicy As Integer, ByVal nCertif As Integer, ByVal dEffecdate As Date) As Boolean
        Dim lclsAviat_marit As eRemoteDB.Execute
        Dim lclsAviat_maritItem As Aviat_marit

        lclsAviat_marit = New eRemoteDB.Execute

        '+ Define all parameters for the stored procedures 'insudb.reaCredit'. Generated on 21/07/2004 03:31:12 p.m.
        With lclsAviat_marit
            .StoredProcedure = "reaviat_marit"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run(True) Then
                Do While Not .EOF
                    lclsAviat_maritItem = New Aviat_marit
                    lclsAviat_maritItem.scertype = sCertype
                    lclsAviat_maritItem.nbranch = nBranch
                    lclsAviat_maritItem.nproduct = nProduct
                    lclsAviat_maritItem.npolicy = nPolicy
                    lclsAviat_maritItem.ncertif = nCertif
                    lclsAviat_maritItem.ngroup = .FieldToClass("ngroup")
                    lclsAviat_maritItem.nsituation = .FieldToClass("nsituation")
                    lclsAviat_maritItem.nparticularclas = .FieldToClass("nparticularclas")
                    lclsAviat_maritItem.sname = .FieldToClass("sname")
                    lclsAviat_maritItem.sbrand = .FieldToClass("sbrand")
                    lclsAviat_maritItem.smodel = .FieldToClass("smodel")
                    lclsAviat_maritItem.sseries = .FieldToClass("sseries")
                    lclsAviat_maritItem.nyear = .FieldToClass("nyear")
                    lclsAviat_maritItem.sorigin = .FieldToClass("sorigin")
                    lclsAviat_maritItem.sregistrationnumber = .FieldToClass("sregistrationnumber")
                    lclsAviat_maritItem.scapacity = .FieldToClass("scapacity")
                    lclsAviat_maritItem.ntakeoff_maxwei = .FieldToClass("ntakeoff_maxwei")
                    lclsAviat_maritItem.sairportbase = .FieldToClass("sairportbase")
                    lclsAviat_maritItem.sgeographical = .FieldToClass("sgeographical")
                    lclsAviat_maritItem.nuse = .FieldToClass("nuse")
                    lclsAviat_maritItem.snavigationcertificate = .FieldToClass("snavigationcertificate")
                    lclsAviat_maritItem.nqualificationship = .FieldToClass("nqualificationship")
                    lclsAviat_maritItem.sportdeparture = .FieldToClass("sportdeparture")
                    lclsAviat_maritItem.sportarrival = .FieldToClass("sportarrival")
                    lclsAviat_maritItem.sdimensions = .FieldToClass("sdimensions")
                    lclsAviat_maritItem.saddicionaltext = .FieldToClass("saddicionaltext")
                    lclsAviat_maritItem.nseatnumber = .FieldToClass("nseatnumber")
                    lclsAviat_maritItem.ncrewnumber = .FieldToClass("ncrewnumber")
                    lclsAviat_maritItem.npassengersnumber = .FieldToClass("npassengersnumber")
                    lclsAviat_maritItem.nnibranumber = .FieldToClass("nnibranumber")
                    lclsAviat_maritItem.ncapital = .FieldToClass("ncapital")
                    Call Add(lclsAviat_maritItem)
                    lclsAviat_maritItem = Nothing
                    .RNext()
                Loop

                Find = True
                .RCloseRec()

            Else
                Find = False
            End If
        End With

        lclsAviat_marit = Nothing
        lclsAviat_maritItem = Nothing

        Exit Function
    End Function

    '**+Objective: Local variable to hold collection.
    '+Objetivo: Variable Local para almacenar la colección.
    Private mcolAviat_marit As Collection

    '**%Objective: It adds an element to the collection.
    '**%Parameters:
    '**%    lclsAviat_marit -
    '%Objetivo: Agrega un elemento a la colección.
    '%Parámetros:
    '%    lclsAviat_marit -
    Public Function Add(ByRef lclsAviat_marit As Aviat_marit) As Aviat_marit
        mcolAviat_marit.Add(lclsAviat_marit)
        '**-return the object created
        Add = lclsAviat_marit
        Exit Function
    End Function

    '**%Objective: This property is used when reference to an element becomes of the collection.
    '**%Parameters:
    '**%    vIndexKey   -
    '%Objetivo: Esta propiedad es usada cuando se hace referencia a un elemento de la colección.
    '%Parámetros:
    '%    vIndexKey -
    Public ReadOnly Property Item(ByVal vIndexKey As Object) As Aviat_marit
        Get

            Item = mcolAviat_marit.Item(vIndexKey)

            Exit Property
        End Get
    End Property

    '**%Objective: It returns the amount of existing elements in the collection.
    '%Objetivo: Retorna la contidad de elementos existentes en la colección.
    Public ReadOnly Property Count() As Integer
        Get

            Count = mcolAviat_marit.Count()

            Exit Property
        End Get
    End Property


    Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
        GetEnumerator = mcolAviat_marit.GetEnumerator
    End Function

    '**%Objective: It allows to remove an element of the collection.
    '**%Parameters:
    '**%    vIndexKey   -
    '%Objetivo: Permite eliminar un elemento de la colección.
    '%Parámetros:
    '%    vIndexKey -
    Public Sub Remove(ByRef vIndexKey As Object)

        mcolAviat_marit.Remove(vIndexKey)

        Exit Sub
    End Sub

    '**%Objective: Creates the collection when this class is created.
    '%Objetivo: Crea la colección cuando se crea esta clase.
    Private Sub Class_Initialize_Renamed()

        mcolAviat_marit = New Collection

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
