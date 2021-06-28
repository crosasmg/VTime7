Option Strict Off
Option Explicit On
Public Class QueryClients
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: QueryClients.cls                         $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:30p                                $%'
	'% $Revision:: 16                                       $%'
	'%-------------------------------------------------------%'
	
	Private mCol As Collection
	
	'**-Order type
	'- Tipo de ordenamiento
	Enum eTypeOrderBy
		Acss = 1
		Descs = 2
	End Enum
	
	'- Se define la variable para almacenar el nro. de registros que devuelve la consulta por condición
	Public nRecordCount As Double
	
	Const SQL_QUERY As String = "    Client.sClient ,Client.dBirthdat ,Client.sCliename, Client.sFirstname,Client.sLastname ," & "    Client.sSexclien , T18.sDescript DescSex "
	'**% Count: Counts the number of elements in the collection
	'% Count: cuenta el número de elementos dentro de la colección
	Public ReadOnly Property Count() As Integer
		Get
			Count = mCol.Count()
		End Get
	End Property
	
	'**% Item: Get an element from the collection
	'% Item: toma un elemento de la colección
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As QueryClient
		Get
			Item = mCol.Item(vntIndexKey)
		End Get
	End Property
	
	'**% NewEnum: Enumerates the elements in the collection
	'% NewEnum: enumera los elementos dentro de la colección
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
	
	'**% FindCondition: Search for the clients according to the searching condition
	'% FindCondition: Obtiene los clientes que cumplan con la condición de búsqueda
    Public Function FindCondition(ByVal sClient As String, ByVal sCliename As String, ByVal sLastname As String, ByVal sLastName2 As String, ByVal sBirthday As String, ByVal sSexClient As String, ByVal nPerson_typ As Integer, Optional ByVal sSpecialWhere As String = "", Optional ByVal nRecords As Integer = 0, Optional ByVal nFirstRecord As Integer = 0, Optional ByVal nLastRecord As Integer = 0, _
                                  Optional ByVal eSequenceOrder As eTypeOrderBy = 0, _
                                  Optional ByVal sTypeUser As String = "", _
                                  Optional ByVal sUserClient As String = "") As Boolean


        'Dim strSQL As String

        '**- lblnCondition : Indicates if the SQL clause have a searching condition
        '- lblnCondition : Indica si la cláusula SQL contiene o no condiciones
        '- de búsqueda.

        Dim lrecClient As eRemoteDB.Execute

        Dim lclsClient As eClient.Client
        lclsClient = New eClient.Client

        '**- lintRecordsAdd : Counts the loaded records in the collection
        '- lintRecordsAdd : Realiza el conteo de los registros cargados
        '- a la colección, sólo en caso de haber sido definidos.
        Dim lintRecordsAdd As Integer
        lintRecordsAdd = 0

        '**- lintTotalRecords : Counts the encountered records
        '- lintTotalRecords : Realiza el conteo de todos los registros encontrados.
        Dim lintTotalRecords As Integer
        lintTotalRecords = 0

        'UPGRADE_NOTE: Object mCol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        mCol = Nothing

        FindCondition = False

        lrecClient = New eRemoteDB.Execute
        With lrecClient
            .StoredProcedure = "REACLIENTQUERYPKG.REACLIENTQUERY"
            .Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sFirstName", sCliename, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 63, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sLastName", sLastname, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 19, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sLastName2", sLastName2, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 19, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sBirthday", sBirthday, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sSexClient", IIf(sSexClient = "0", String.Empty, sSexClient), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPerson_typ", nPerson_typ, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sUserClient", sUserClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sTypeUser", sTypeUser, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            If .Run Then
                nRecordCount = .RecordCount
                FindCondition = True

                '**+New object of the collection
                '+ Nuevo objeto de colección.
                mCol = New Collection

                Do While Not .EOF
                    lintTotalRecords = lintTotalRecords + 1


                    '**+ Determines if there is a range to load the range with all the components
                    '+ Determina si se especificó o no un rango con el objeto de
                    '+ cargar el rango o todos los componentes.
                    If (nFirstRecord > 0) And (nLastRecord > 0) And (nFirstRecord <= nLastRecord) Then

                        '**+ Loads only the data of the specified range to the collection
                        '+ Carga a la colección sólo los datos que corresponden
                        '+ a un rango.
                        If (lintTotalRecords >= nFirstRecord) And (lintTotalRecords <= nLastRecord) Then

                            '**+ Adds the object to the collection
                            '+ Se agrega el objeto a la colección.
                            Call Add(.FieldToClass("sClient"), .FieldToClass("sDigit"), .FieldToClass("sCliename"), .FieldToClass("sFirstName"), .FieldToClass("sLastName"), .FieldToClass("sLastName2"), .FieldToClass("dBirthdat"), .FieldToClass("sSexclien"))
                            lintRecordsAdd = lintRecordsAdd + 1

                            '+ Ends the loop when the maximum is reached
                            '+ Se termina el ciclo cuando se alcanza el tope.
                            If lintTotalRecords >= nLastRecord Then Exit Do
                        End If
                    Else
                        '**+ Adds the object to the collection
                        '+ Se agrega el objeto a la colección.
                        Call Add(.FieldToClass("sClient"), .FieldToClass("sDigit"), .FieldToClass("sCliename"), .FieldToClass("sFirstName"), .FieldToClass("sLastName"), .FieldToClass("sLastName2"), .FieldToClass("dBirthdat"), .FieldToClass("sSexclien"))
                        lintRecordsAdd = lintRecordsAdd + 1
                    End If
                    .RNext()

                    '**+ Ends the load of the elements to the collection when there is a maximum number of records defined
                    '+ Termina abruptamente la carga de elementos a la colección,
                    '+ cuando se definió un número máximo de registros.
                    If nRecords > 0 Then
                        If lintRecordsAdd >= nRecords Then Exit Do
                    End If

                Loop
                .RCloseRec()
            End If
        End With

        'UPGRADE_NOTE: Object lrecClient may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecClient = Nothing
        'UPGRADE_NOTE: Object lclsClient may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsClient = Nothing
    End Function

    '**% FindClientsByProducer: Search for the clients according to Producer
    '% FindClientsByProducer: Obtiene los clientes asociados a un productor
    Public Function FindClientsByProducer(ByVal sClient As String) As Boolean

        Dim lrecClient As eRemoteDB.Execute

        Dim lclsClient As eClient.Client
        lclsClient = New eClient.Client

        mCol = Nothing

        FindClientsByProducer = False

        lrecClient = New eRemoteDB.Execute
        With lrecClient
            .StoredProcedure = "REACLIENTSBYPRODUCER"
            .Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            If .Run Then
                FindClientsByProducer = True

                '**+New object of the collection
                '+ Nuevo objeto de colección.
                mCol = New Collection

                Do While Not .EOF

                    '**+ Adds the object to the collection
                    '+ Se agrega el objeto a la colección.
                    Call Add(.FieldToClass("sClient"), .FieldToClass("sDigit"), .FieldToClass("sCliename"), .FieldToClass("sFirstName"), .FieldToClass("sLastName"), .FieldToClass("sLastName2"), .FieldToClass("dBirthdat"), .FieldToClass("sSexclien"))

                    .RNext()

                Loop
                .RCloseRec()
            End If
        End With

        lrecClient = Nothing
        lclsClient = Nothing
    End Function


    '**% Add: Adds a new element to the collection
    '% Add: añade un nuevo elemento a la colección
    Private Function Add(ByVal sClient As String, ByVal sDigit As String, ByVal sCliename As String, ByVal sFirstname As String, ByVal sLastname As String, ByVal sLastName2 As String, ByVal sBirthdat As String, ByVal sSexclien As String) As QueryClient

        '- Se define variable que almacena las propiedades y metodos de la clase principal
        Dim objNewMember As QueryClient

        objNewMember = New QueryClient

        With objNewMember
            .sClient = sClient
            .sClieName = sCliename
            .dBirthdat = CDate(sBirthdat)
            .sSexclien = sSexclien
            .sFirstName = sFirstname
            .sLastName = sLastname
            .sLastName2 = sLastName2
            .sDigit = sDigit
        End With

        mCol.Add(objNewMember, "QC" & sClient)

        Add = objNewMember
        'UPGRADE_NOTE: Object objNewMember may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        objNewMember = Nothing
    End Function
    '**% Class_Initialize: Controls the opening of each instance of the collection
    '% Class_Initialize: controla la apertura de cada instancia de la colección
    'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
    Private Sub Class_Initialize_Renamed()
        mCol = New Collection
    End Sub
    Public Sub New()
        MyBase.New()
        Class_Initialize_Renamed()
    End Sub
    '**% Class_Terminate: Deletes the collection
    '% Class_Terminate: elimina la colección
    'UPGRADE_NOTE: Class_Terminate was upgraded to Class_Terminate_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
    Private Sub Class_Terminate_Renamed()
        'UPGRADE_NOTE: Object mCol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        mCol = Nothing
    End Sub
    Protected Overrides Sub Finalize()
        Class_Terminate_Renamed()
        MyBase.Finalize()
    End Sub

    '% ClientQuery: Consulta de cliente
    Public Function ClientQuery(ByVal sClient As String, ByVal sFirstname As String, ByVal sLastname As String, ByVal sLastName2 As String, ByVal sBirthday As String, ByVal sSexClient As String) As Boolean
        Dim lreccreClient As eRemoteDB.Execute

        lreccreClient = New eRemoteDB.Execute

        With lreccreClient
            .StoredProcedure = "REACLIENTQUERYPKG.REACLIENTQUERY"
            .Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sFirstName", sFirstname, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 19, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sLastName", sLastname, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 19, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sLastName2", sLastName2, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 19, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sBirthday", sBirthday, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sSexClient", sSexClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            ClientQuery = .Run
        End With

        'UPGRADE_NOTE: Object lreccreClient may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lreccreClient = Nothing
    End Function
End Class






