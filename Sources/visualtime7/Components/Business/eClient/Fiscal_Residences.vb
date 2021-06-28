Option Strict Off
Option Explicit On
Public Class Fiscal_Residences
    Implements System.Collections.IEnumerable

    Private mCol As Collection

    '-Se definen las variables auxiliares para evitar una búsqueda innecesaria
    Private lauxsClient As String

    '**%Add: adds a new instance of the "Phone" class to the collection
    '%Add: Añade una nueva instancia de la clase "Phone" a la colección
    Public Function Add(ByVal sClient As String, ByVal nCountry As Integer, ByVal dEffecdate As Date, ByVal sUs_Itinnum As String, ByVal nMotive_itin As Integer, ByVal sJurisdiction As String, ByVal dNulldate As Date, ByVal nUsercode As Integer, ByVal dCompdate As Date) As Fiscal_Residence

        '+ Create a new object

        Dim objNewMember As Fiscal_Residence
        objNewMember = New Fiscal_Residence

        '+ Set the properties passed into the method
        With objNewMember
            .sClient = sClient
            .nCountry = nCountry
            .dEffecdate = dEffecdate
            .sUs_Itinnum = sUs_Itinnum
            .nMotive_Itin = nMotive_itin
            .sJurisdiction = sJurisdiction
            .dNulldate = dNulldate
            .nUsercode = nUsercode
            .dCompdate = dCompdate
        End With

        mCol.Add(objNewMember, sClient & nCountry)

        'Return the object created

        Add = objNewMember

        'UPGRADE_NOTE: Object objNewMember may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        objNewMember = Nothing

    End Function

    '% Find: Esta función es la encarga de buscar si existe informacion en las tablas.
    '%                FISCAL_RESIDENCE
    Public Function Find(ByVal sClient As String, ByVal dEffecdate As Date) As Object

        Dim lrecreaFiscal_Residence As eRemoteDB.Execute
        If lauxsClient = sClient Then
            Find = True
        Else
            lrecreaFiscal_Residence = New eRemoteDB.Execute

            '+ Definición de parámetros para stored procedure 'insudb.reaPhones'
            '+ Información leída el 12/07/2000 15:03:59
            With lrecreaFiscal_Residence
                .StoredProcedure = "Reafiscal_Residence"
                .Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                Find = .Run
                If Find Then
                    Do While Not .EOF
                        Call Add(.FieldToClass("sClient"), CInt(.FieldToClass("nCountry")), .FieldToClass("dEffecdate"), .FieldToClass("sUs_Itinnum"), CInt(.FieldToClass("nMotive_Itin")), .FieldToClass("sJurisdiction"), .FieldToClass("dNulldate"), CInt(.FieldToClass("nUsercode")), .FieldToClass("dCompdate"))
                        .RNext()
                    Loop
                    .RCloseRec()

                    '+ Se asignan los valores a las variables auxiliares, para futuras búsquedas
                    lauxsClient = sClient
                End If
            End With
            'UPGRADE_NOTE: Object lrecreaPhones_All may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
            lrecreaFiscal_Residence = Nothing
        End If
    End Function
    '% Find: Este metodo retorna VERDADERO o FALSO dependiendo de la existencia o no de registros en la
    '% tabla "Phones"
    Public Function GetFromAddress(ByVal nRecowner As Integer, ByVal sKeyAddress As String, ByVal dEffecdate As Date) As Boolean
        Dim lrecreaPhones_All As eRemoteDB.Execute

        lrecreaPhones_All = New eRemoteDB.Execute

        '+ Definición de parámetros para stored procedure 'insudb.creaTmp_Phones'
        With lrecreaPhones_All
            .StoredProcedure = "creaTmp_Phones"
            .Parameters.Add("nRecowner", nRecowner, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sKeyAddress", sKeyAddress, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 50, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            GetFromAddress = .Run()
            If GetFromAddress Then
                Do While Not .EOF
                    Call Add(.FieldToClass("sClient"), CInt(.FieldToClass("nCountry")), .FieldToClass("dEffecdate"), .FieldToClass("sUs_Itinnum"), CInt(.FieldToClass("nMotive_Itin")), .FieldToClass("sJurisdiction"), .FieldToClass("dNulldate"), CInt(.FieldToClass("nUsercode")), .FieldToClass("dCompdate"))
                    .RNext()
                Loop
                .RCloseRec()
            End If
        End With
        'UPGRADE_NOTE: Object lrecreaPhones_All may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecreaPhones_All = Nothing
    End Function

    '% Update: Este método se encarga de actualizar registros en la tabla "Phones". Devolviendo verdadero o
    '% falso dependiendo de si el Stored procedure se ejecutó correctamente.
    '    Public Function Update() As Boolean
    '    Dim lclsPhones As Fiscal_Residence
    '    Dim lcolAux As Collection
    '       Update = True
    '       lcolAux = New Collection
    '       For Each lclsPhones In mCol
    '          With lclsPhones
    '
    '             If lAuxKeyPhones = 0 Then
    '                lAuxEffecdate = .dEffecdate
    '               lAuxKeyPhones = .nKeyPhones
    '               lAuxRecowner = .nRecowner
    '           End If
    '
    '               Select Case .nStatusInstance

    '+ Si la acción es Agregar
    '                Case 1
    '                   Update = .Add()

    '+ Si la acción es Actualizar
    '                Case 2
    '                   Update = .Update()

    '+ Si la acción es Eliminar
    '                Case 3
    '                   Update = .Delete()
    '          End Select
    '         If .nStatusInstance <> 3 Then
    '            If Update Then
    '              .nStatusInstance = 0
    '           End If
    '         lcolAux.Add(lclsPhones, "A" & .nKeyPhones)
    '    End If
    '            End With
    '       Next lclsPhones
    '      mCol = lcolAux
    ' End Function
    '
    '%insMaxPhone: Esta función se encarga de buscar el maximo valor encontrado en los teléfonos
    '   Public Function insMaxPhone(ByVal Recowner As Address.eTypeRecOwner, ByVal rectype As Addresss.eTypeRecType, ByVal KeyAddress As String, Optional ByVal Effecdate As Date = dtmNull) As Integer
    '  Dim lrecreaMaxPhone As eRemoteDB.Execute
    '
    '    If Effecdate = dtmNull Then
    '        Effecdate = Today
    '    End If

    '        lrecreaMaxPhone = New eRemoteDB.Execute

    '+Definición de parámetros para stored procedure 'insudb.reaMaxPhone'
    '+Información leída el 28/08/2000 16:27:46
    '    With lrecreaMaxPhone
    '       .StoredProcedure = "reaMaxPhone"
    '      .Parameters.Add("nRecOwner", Recowner, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
    '     .Parameters.Add("sRecType", rectype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
    '    .Parameters.Add("sKeyAddress", KeyAddress, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 50, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
    '           If .Run Then
    '               insMaxPhone = .FieldToClass("MaxPhone", 0) + 1
    '               .RCloseRec()
    '           Else
    '               insMaxPhone = 1
    '           End If
    '       End With
    '   'UPGRADE_NOTE: Object lrecreaMaxPhone may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
    '      lrecreaMaxPhone = Nothing
    '  End Function

    '*Item: Devuelve un elemento de la colección (segun índice)
    Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Fiscal_Residences
        Get
            ' Used when referencing an element in the collection.
            'vntIndexKey contains either the Index or Key to the collection,
            'this is why it is declared as a Variant
            'Syntax: Set foo = x.Item(xyz) or Set foo = x.Item(5)

            Item = mCol.Item(vntIndexKey)
        End Get
    End Property

    '*Count: Devuelve el número de elementos que posee la colección
    Public ReadOnly Property Count() As Integer
        Get
            'Used when retrieving the number of elements in the collection.
            'Syntax: Debug.Print x.Count

            Count = mCol.Count()
        End Get
    End Property

    '***NewEnum: Enumerates the collection for use in a For Each...Next loop
    '*NewEnum: Permite enumerar la colección para utilizarla en un ciclo For Each... Next
    'UPGRADE_NOTE: NewEnum property was commented out. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="B3FC1610-34F3-43F5-86B7-16C984F0E88E"'
    'Public ReadOnly Property NewEnum() As stdole.IUnknown
    '   Get
    'This property allows you to enumerate this collection with the For...Each syntax
    'NewEnum = mCol._NewEnum
    'End Get
    'End Property

    Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
        'UPGRADE_TODO: Uncomment and change the following line to return the collection enumerator. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="95F9AAD0-1319-4921-95F0-B9D3C4FF7F1C"'
        GetEnumerator = mCol.GetEnumerator
    End Function

    '**%Remove: Deletes an element from the collection
    '%Remove: Elimina un elemento de la colección
    Public Sub Remove(ByRef vntIndexKey As Object)
        'Used when removing an element from the collection.
        'vntIndexKey contains either the Index or Key, which is why
        'it is declared as a Variant
        'Syntax: x.Remove(xyz)
        mCol.Remove(vntIndexKey)
    End Sub

    '**%Class_Initialize: Controls the creation of an instance of the collection
    '%Class_Initialize: Controla la creación de una instancia de la colección
    'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
    Private Sub Class_Initialize_Renamed()
        'Creates the collection when this class is created
        mCol = New Collection
    End Sub
    Public Sub New()
        MyBase.New()
        Class_Initialize_Renamed()
    End Sub

    '**%Class_Terminate: Controls the destruction of an instance of the collection
    '%Class_Terminate: Controla la destrucción de una instancia de la colección
    'UPGRADE_NOTE: Class_Terminate was upgraded to Class_Terminate_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
    Private Sub Class_Terminate_Renamed()
        'Destroys collection when this class is terminated
        'UPGRADE_NOTE: Object mCol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        mCol = Nothing
    End Sub
    Protected Overrides Sub Finalize()
        Class_Terminate_Renamed()
        MyBase.Finalize()
    End Sub
End Class






