Option Strict Off
Option Explicit On
Public Class Detailsallowedses
    Implements System.Collections.IEnumerable
    '%-------------------------------------------------------%'
    '% $Workfile:: Detailsallowedses.cls                          $%'
    '% $Author:: Nvaplat7                                   $%'
    '% $Date:: 9/08/03 1:06p                                $%'
    '% $Revision:: 5                                        $%'
    '%-------------------------------------------------------%'

    Private mCol As Collection

    '**- auxiliary variables
    '- Variables auxiliares
    '**- define the variables that will be used for the search.
    '- Se definen las variables que se van a utilizar para la busqueda

    Private mintBranch As Integer
    Private mintProduct As Integer

    '**% Add. Adds a new instance of the Tab_goods to the collection.
    '% Add: Añade una nueva instancia de la clase Tab_goods a la colección
    Public Function Add(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nType As Integer, ByVal nCode_good As Integer, ByVal sDescript_Good As String, ByVal sDescript_Type As String, ByVal sAddCapital As String, ByVal nUserCode As Integer, ByVal dEffecdate As Date) As Detailsallowed

        '**- Define the variable that will contain the instance to be added.
        '- Se define la variable que contendra la instancia a añadir

        Dim objNewMember As Detailsallowed
        objNewMember = New Detailsallowed

        With objNewMember
            .nBranch = nBranch
            .nProduct = nProduct
            .nCode_good = nCode_good
            .sDescript_Good = sDescript_Good
            .nType = nType
            .sDescript_Type = sDescript_Type
            .dEffecdate = dEffecdate
            .sAddCapital = sAddCapital
            .nUsercode = nUserCode
        End With

        mCol.Add(objNewMember)

        '**+ Returns the created objetc
        '+ Retorna el objeto creado

        Add = objNewMember
        'UPGRADE_NOTE: Object objNewMember may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        objNewMember = Nothing
    End Function

    '%Find: Este metodo carga la coleccion de elementos de la tabla "Tab_goods" devolviendo Verdadero o
    '%falso, dependiendo de la existencia de los registros.
    '------------------------------------------------------------
    Public Function Find(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, Optional ByVal lblnFind As Boolean = False) As Boolean
        '------------------------------------------------------------
        Dim lrecreaTab_goods As eRemoteDB.Execute
        On Error GoTo Find_Err
        lrecreaTab_goods = New eRemoteDB.Execute

        Static lblnRead As Boolean

        '**+ By default the function always return True
        '+ Por defecto la función siempre devuelve True
        lblnRead = True
        If mintBranch <> nBranch Or mintProduct <> nProduct Or lblnFind Then

            mintBranch = nBranch
            mintProduct = nProduct

            '**+ Parameter definition for stored procedure 'insudb.reaTab_goods'
            '+ Definición de parámetros para stored procedure 'insudb.reaTab_goods'
            '**+ Information read on Novemeber 08,2000 11:26:16 a.m.
            '+ Información leída el 08/11/2000 11:26:16 AM

            With lrecreaTab_goods
                .StoredProcedure = "reaDetailsAllowed"
                .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

                If .Run Then
                    Do While Not .EOF
                        Call Add(.FieldToClass("nBranch"), .FieldToClass("nProduct"), .FieldToClass("nType"), .FieldToClass("nCode_good"), .FieldToClass("sDescript_good"), .FieldToClass("sDescript_type"), .FieldToClass("sAddCapital"), .FieldToClass("nUsercode"), .FieldToClass("dEffecdate"))
                        .RNext()
                    Loop

                    .RCloseRec()

                Else
                    lblnRead = False
                End If
            End With
        End If

        Find = lblnRead

Find_Err:
        If Err.Number Then
            Find = CShort(Find) + CDbl(Err.Description)
        End If

        'UPGRADE_NOTE: Object lrecreaTab_goods may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecreaTab_goods = Nothing

        On Error GoTo 0

    End Function

    '%Find: Este metodo carga la coleccion de elementos de la tabla "Tab_goods" devolviendo Verdadero o
    '%falso, dependiendo de la existencia de los registros.
    '------------------------------------------------------------
    Public Function Find_Dup(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nCode_good As Integer, ByVal nType As Integer, ByVal dEffecdate As Date, Optional ByVal lblnFind As Boolean = False) As Boolean
        '------------------------------------------------------------

        Dim lrecreaTab_goods As eRemoteDB.Execute
        On Error GoTo Find_Dup_Err
        lrecreaTab_goods = New eRemoteDB.Execute

        Static lblnRead As Boolean

        lblnRead = True
        With lrecreaTab_goods
            .StoredProcedure = "reaDetailsAllowed_dup"
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCode_good", nCode_good, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nType", nType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)


            If .Run Then
                Do While Not .EOF
                    Call Add(.FieldToClass("nBranch"), .FieldToClass("nProduct"), .FieldToClass("nType"), .FieldToClass("nCode_good"), .FieldToClass("sDescript_good"), .FieldToClass("sDescript_type"), .FieldToClass("sAddCapital"), .FieldToClass("nUsercode"), .FieldToClass("dEffecdate"))
                    .RNext()
                Loop
                .RCloseRec()
            Else
                lblnRead = False
            End If
        End With

        Find_Dup = lblnRead

Find_Dup_Err:
        If Err.Number Then
            Find_Dup = CShort(Find_Dup) + CDbl(Err.Description)
        End If

        'UPGRADE_NOTE: Object lrecreaTab_goods may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecreaTab_goods = Nothing

        On Error GoTo 0
    End Function


    '*** Item: Restores an element of the collection (according to index)
    '* Item: Devuelve un elemento de la colección (segun índice)
    Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Detailsallowed
        Get

            Item = mCol.Item(vntIndexKey)
        End Get
    End Property

    '*** Count: Restores the number of elements that the collection owns
    '* Count: Devuelve el número de elementos que posee la colección
    Public ReadOnly Property Count() As Integer
        Get

            Count = mCol.Count()
        End Get
    End Property

    '*** NewEnum: Allows to enumerate the collection for using it in a cycle For Each...Next
    '* NewEnum: Permite enumerar la colección para utilizarla en un ciclo For Each... Next
    'UPGRADE_NOTE: NewEnum property was commented out. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="B3FC1610-34F3-43F5-86B7-16C984F0E88E"'
    'Public ReadOnly Property NewEnum() As stdole.IUnknown
    'Get
    '
    'NewEnum = mCol._NewEnum
    'End Get
    'End Property

    Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
        'UPGRADE_TODO: Uncomment and change the following line to return the collection enumerator. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="95F9AAD0-1319-4921-95F0-B9D3C4FF7F1C"'
        GetEnumerator = mCol.GetEnumerator
    End Function

    '**% Remove: removes an element from the collection.
    '% Remove: Elimina un elemento de la colección
    '---------------------------------------------
    Public Sub Remove(ByRef vntIndexKey As Object)
        '---------------------------------------------

        mCol.Remove(vntIndexKey)
    End Sub

    '**% Class_Initialize: controls the creation of an instace of the collection.
    '% Class_Initialize: Controla la creación de una instancia de la colección
    'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
    Private Sub Class_Initialize_Renamed()

        mCol = New Collection
    End Sub
    Public Sub New()
        MyBase.New()
        Class_Initialize_Renamed()
    End Sub

    '**% Class_Terminate: controls the delete of an instance of the collection
    '% Class_Terminate: Controla la destrucción de una instancia de la colección
    'UPGRADE_NOTE: Class_Terminate was upgraded to Class_Terminate_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
    Private Sub Class_Terminate_Renamed()

        'UPGRADE_NOTE: Object mCol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        mCol = Nothing
    End Sub
    Protected Overrides Sub Finalize()
        Class_Terminate_Renamed()
        MyBase.Finalize()
    End Sub
End Class






