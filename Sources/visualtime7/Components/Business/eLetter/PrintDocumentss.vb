Option Strict Off
Option Explicit On
Public Class PrintDocumentss
    Implements System.Collections.IEnumerable
    '**-Objective: Local variable to hold collection.
    '-Objetivo: Variable Local para almacenar la colección.
    Private mcolPrintDocuments As Collection

    '**%Objective: Used when referencing an element in the collection vntIndexKey contains either the Index or Key to the collection, this is why it is declared as a Variant Syntax: Set foo = x.Item(xyz) or Set foo = x.Item(5)
    '%Objetivo: Es usada para refenciar un elemento de la colección. La sintaxis: Set foo = x.Item(xyz) or Set foo = x.Item(5)
    Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As PrintDocuments
        Get

            If Not IsIDEMode() Then
            End If

            Item = mcolPrintDocuments.Item(vntIndexKey)

            Exit Property
        End Get
    End Property

    '**%Objective: Restores the number of elements that the collection owns.
    '%Objetivo: Devuelve el número de elementos que posee la colección
    Public ReadOnly Property Count() As Integer
        Get

            If Not IsIDEMode() Then
            End If

            Count = mcolPrintDocuments.Count()

            Exit Property
        End Get
    End Property

    '**%Objective: Allows to enumerate the collection for using it in a cycle For Each...Next
    '%Objetivo: Permite enumerar la colección para utilizarla en un ciclo For Each... Next
    Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
        GetEnumerator = mcolPrintDocuments.GetEnumerator
    End Function

    '**%Objective: Adds an element to the collection.
    '**%Parameters:
    '**%    lclsPrintDocuments -
    '%Objetivo: Este método permite agregar un elemento a la colección.
    '%Parámetros:
    '%    lclsPrintDocuments -
    '------------------------------------------------------------------------------------------------------------------------
    Public Function Add(ByRef lclsPrintDocuments As PrintDocuments) As PrintDocuments
        '------------------------------------------------------------------------------------------------------------------------
        If Not IsIDEMode() Then
        End If

        '**+ The properties passed to the method are assigned to the collection.
        '+ Las propiedades pasadas al método son asignadas a la colección.

        mcolPrintDocuments.Add(lclsPrintDocuments)

        '**+Returns the object created.
        '+ Retorna el objeto creado.

        Add = lclsPrintDocuments
        lclsPrintDocuments = Nothing

        Exit Function
    End Function

    '**%Objective: Removes an element from the collection.
    '**%Parameters:
    '**%    vIndexKey - An expression that specifies the position of an element from the collection
    '%Objetivo: Permite eliminar un elemento de la colección.
    '%Parámetros:
    '%    vIndexKey - Una expresión que especifica la posición de un elemento de la colección.
    '------------------------------------------------------------------------------------------------------------------------
    Public Sub Remove(ByRef vIndexKey As Object)
        '------------------------------------------------------------------------------------------------------------------------
        If Not IsIDEMode() Then
        End If

        mcolPrintDocuments.Remove(vIndexKey)

        Exit Sub
    End Sub

    Public Function Find(ByVal nShipmentType As Integer, _
                     ByVal sTypeDocument As String, _
                     ByVal nOfficeAgen As Integer, _
                     ByVal nAgency As Integer, _
                     ByVal nIntermed As Integer, _
                     ByVal sClient As String, _
                     ByVal sCertype As String, _
                     ByVal nBranch As Integer, _
                     ByVal nProduct As Integer, _
                     ByVal nPolicy As Integer, _
                     ByVal nCertif As Integer, _
                     ByVal sStatusDocument As Integer) As Boolean
        '------------------------------------------------------------------------------------------------------------------------
        Dim lclsPrintDocuments As eRemoteDB.Execute
        Dim lclsPrintDocumentsItem As PrintDocuments

        If Not IsIDEMode() Then
        End If

        lclsPrintDocuments = New eRemoteDB.Execute

        With lclsPrintDocuments
            .StoredProcedure = "reaPrintDocuments_a"

            .Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", IIf(nProduct = intNull, 0, nProduct), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", IIf(nPolicy = intNull, 0, nPolicy), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", IIf(nCertif = intNull, 0, nCertif), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nOfficeAgen", IIf(nOfficeAgen = intNull, 0, nOfficeAgen), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nAgency", IIf(nAgency = intNull, 0, nAgency), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nIntermed", IIf(nIntermed = intNull, 0, nIntermed), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nShipmentType", IIf(nShipmentType = intNull, 0, nShipmentType), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sTypeDocument", sTypeDocument, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sStatusDocument", sStatusDocument, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            If .Run(True) Then
                Do While Not .EOF
                    lclsPrintDocumentsItem = New PrintDocuments

                    lclsPrintDocumentsItem.nAgency = .FieldToClass("nAgency")
                    lclsPrintDocumentsItem.nOfficeAgen = .FieldToClass("nOfficeAgen")
                    lclsPrintDocumentsItem.nIntermed = .FieldToClass("nIntermed")
                    lclsPrintDocumentsItem.sClient = .FieldToClass("sClient")
                    lclsPrintDocumentsItem.sTypeDocument = .FieldToClass("sTypeDocument")
                    lclsPrintDocumentsItem.sCertype = .FieldToClass("sCerType")
                    lclsPrintDocumentsItem.nBranch = .FieldToClass("nBranch")
                    lclsPrintDocumentsItem.nProduct = .FieldToClass("nProduct")
                    lclsPrintDocumentsItem.nPolicy = .FieldToClass("nPolicy")
                    lclsPrintDocumentsItem.nCertif = .FieldToClass("nCertif")
                    lclsPrintDocumentsItem.nReceipt = .FieldToClass("nReceipt")
                    lclsPrintDocumentsItem.nLettRequest = .FieldToClass("nLettRequest")
                    lclsPrintDocumentsItem.nLetterNum = .FieldToClass("nLetterNum")
                    lclsPrintDocumentsItem.sOfficialCer = .FieldToClass("sOfficialCer")
                    lclsPrintDocumentsItem.sAddress = .FieldToClass("sAddress")
                    lclsPrintDocumentsItem.sPrintStatus = .FieldToClass("sPrintStatus")
                    lclsPrintDocumentsItem.nShipmentType = .FieldToClass("nShipmentType")
                    lclsPrintDocumentsItem.nType = .FieldToClass("nType")
                    lclsPrintDocumentsItem.nCodForm = .FieldToClass("nCodForm")
                    lclsPrintDocumentsItem.nConsecutive = .FieldToClass("nConsecutive")
                    'lclsPrintDocumentsItem.sDitribution = .FieldToClass("sDitribution")
                    lclsPrintDocumentsItem.nSituation = .FieldToClass("nSituation")

                    Call Add(lclsPrintDocumentsItem)
                    lclsPrintDocumentsItem = Nothing
                    .RNext()
                Loop

                Find = True
                .RCloseRec()
            Else
                Find = False
            End If
        End With

        lclsPrintDocuments = Nothing

        Exit Function
        ObjectRelease = lclsPrintDocuments
    End Function

    '**%Objective: Searches for records in the table 'PrintDocuments'.
    '**%Parameters:
    '**%    <__PARAMETER_LIST_DESC__>
    '%Objetivo: Esta función permite realizar la búsqueda de la información en la tabla 'PrintDocuments'.
    '%Parámetros:
    '%    <__PARAMETER_LIST_DESC__>
    '------------------------------------------------------------------------------------------------------------------------
    Public Function Find_Client(ByVal nShipmentType As Integer, _
                                ByVal sTypeDocument As String, _
                                ByVal nOfficeAgen As Integer, _
                                ByVal nAgency As Integer, _
                                ByVal nIntermed As Integer, _
                                ByVal sClient As String, _
                                ByVal sCertype As String, _
                                ByVal nBranch As Integer, _
                                ByVal nProduct As Integer, _
                                ByVal nPolicy As Integer, _
                                ByVal nCertif As Integer) As Boolean
        '------------------------------------------------------------------------------------------------------------------------
        Dim lclsPrintDocuments As eRemoteDB.Execute
        Dim lclsPrintDocumentsItem As PrintDocuments

        If Not IsIDEMode() Then
        End If

        lclsPrintDocuments = New eRemoteDB.Execute

        With lclsPrintDocuments
            .StoredProcedure = "reaPrintDocuments_Client"

            .Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", IIf(nProduct = intNull, 0, nProduct), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", IIf(nPolicy = intNull, 0, nPolicy), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", IIf(nCertif = intNull, 0, nCertif), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nOfficeAgen", IIf(nOfficeAgen = intNull, 0, nOfficeAgen), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nAgency", IIf(nAgency = intNull, 0, nAgency), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nIntermed", IIf(nIntermed = intNull, 0, nIntermed), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nShipmentType", IIf(nShipmentType = intNull, 0, nShipmentType), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sTypeDocument", sTypeDocument, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            If .Run(True) Then
                Do While Not .EOF
                    lclsPrintDocumentsItem = New PrintDocuments
                    lclsPrintDocumentsItem.sClient = .FieldToClass("sClient")
                    lclsPrintDocumentsItem.sAddress = .FieldToClass("sAddress")

                    Call Add(lclsPrintDocumentsItem)
                    lclsPrintDocumentsItem = Nothing
                    .RNext()
                Loop

                Find_Client = True
                .RCloseRec()
            Else
                Find_Client = False
            End If
        End With

        lclsPrintDocuments = Nothing

        Exit Function
        ObjectRelease = lclsPrintDocuments
    End Function

    '**%Objective: Creates the collection when this class is created
    '%Objetivo: Crea la colección cunado la clase es creada.
    Private Sub Class_Initialize_Renamed()
        If Not IsIDEMode Then
        End If

        mcolPrintDocuments = New Collection

        Exit Sub
    End Sub

    Public Sub New()
        MyBase.New()
        Class_Initialize_Renamed()
    End Sub

    '**%Objective: Destroys collection when this class is terminated
    '%Objetivo: Elimina la colección cuando la clase finaliza.
    Private Sub Class_Terminate_Renamed()
        If Not IsIDEMode Then
        End If

        mcolPrintDocuments = Nothing

        Exit Sub
    End Sub

    Protected Overrides Sub Finalize()
        Class_Terminate_Renamed()
        MyBase.Finalize()
    End Sub

    Public Function Find_Policy() As Boolean
        '------------------------------------------------------------------------------------------------------------------------
        Dim lclsPrintDocuments As eRemoteDB.Execute
        Dim lclsPrintDocumentsItem As PrintDocuments

        If Not IsIDEMode() Then
        End If

        lclsPrintDocuments = New eRemoteDB.Execute

        With lclsPrintDocuments
            .StoredProcedure = "reaPrintDocuments_Policy"

            If .Run(True) Then
                Do While Not .EOF
                    lclsPrintDocumentsItem = New PrintDocuments
                    lclsPrintDocumentsItem.sCertype = .FieldToClass("sCertype")
                    lclsPrintDocumentsItem.nBranch = .FieldToClass("nBranch")
                    lclsPrintDocumentsItem.nProduct = .FieldToClass("nProduct")
                    lclsPrintDocumentsItem.nPolicy = .FieldToClass("nPolicy")
                    lclsPrintDocumentsItem.nCertif = .FieldToClass("nCertif")
                    lclsPrintDocumentsItem.sClient = .FieldToClass("sClient")
                    lclsPrintDocumentsItem.sAddress = .FieldToClass("sAddress")
                    lclsPrintDocumentsItem.sDitribution = .FieldToClass("sDitribution")
                    lclsPrintDocumentsItem.nSituation = .FieldToClass("nSituation")

                    Call Add(lclsPrintDocumentsItem)
                    lclsPrintDocumentsItem = Nothing
                    .RNext()
                Loop

                Find_Policy = True
                .RCloseRec()
            Else
                Find_Policy = False
            End If
        End With

        lclsPrintDocuments = Nothing

        Exit Function
        ObjectRelease = lclsPrintDocuments
    End Function
End Class





