Option Strict Off
Option Explicit On
Public Class Agencies_Dealers
    Implements System.Collections.IEnumerable
    'variable local para contener colección
    Private mCol As Collection

    Public Function Add(ByVal nAgendealer As Integer, ByVal sAgendealerdesc As String, ByVal sClient_dealer As String, ByVal nUsercode As Integer) As Agencies_Dealer
       
        'crear un nuevo objeto

        Dim objNewMember As Agencies_Dealer
        objNewMember = New Agencies_Dealer


        With objNewMember
            .nAgendealer = nAgendealer
            .sAgendealerdesc = sAgendealerdesc
            .sClient_dealer = sClient_dealer
            .nUsercode = nUsercode
        End With


        'devolver el objeto creado
        mCol.Add(objNewMember)
        Add = objNewMember
        objNewMember = Nothing


    End Function

    '**% Find: Allows to charge to the collection the possible damage of a claim
    '% Find: Permite cargar en la colección --------------- terminar
    Public Function Find() As Boolean

        Dim lrecreaTab_cl_ope_MSI016 As eRemoteDB.Execute

        lrecreaTab_cl_ope_MSI016 = New eRemoteDB.Execute

        'Definición de parámetros para stored procedure 'insudb.reaTab_cl_ope_MSI016'
        'Información leída el 20/09/2001 11:11:15 a.m.

        With lrecreaTab_cl_ope_MSI016
            .StoredProcedure = "REAAGENCIES_DEALER"
            If .Run Then
                Do While Not .EOF
                    Call Add(.FieldToClass("nAgendealer"), .FieldToClass("sAgendealerdesc"), .FieldToClass("sclient_dealer"), .FieldToClass("nUsercode"))
                    .RNext()
                Loop
                Find = True
                .RCloseRec()
            End If
        End With
        lrecreaTab_cl_ope_MSI016 = Nothing

    End Function


    Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Agencies_Dealer
        Get
            'se usa al hacer referencia a un elemento de la colección
            'vntIndexKey contiene el índice o la clave de la colección,
            'por lo que se declara como un Variant
            'Syntax: Set foo = x.Item(xyz) or Set foo = x.Item(5)
            Item = mCol.Item(vntIndexKey)
        End Get
    End Property



    Public ReadOnly Property Count() As Integer
        Get
            'se usa al obtener el número de elementos de la
            'colección. Sintaxis: Debug.Print x.Count
            Count = mCol.Count()
        End Get
    End Property


    'UPGRADE_NOTE: NewEnum property was commented out. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="B3FC1610-34F3-43F5-86B7-16C984F0E88E"'
    'Public ReadOnly Property NewEnum() As stdole.IUnknown
    'Get
    'esta propiedad permite enumerar
    'esta colección con la sintaxis For...Each
    'NewEnum = mCol._NewEnum
    'End Get
    'End Property

    Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
        GetEnumerator = mCol.GetEnumerator
    End Function


    Public Sub Remove(ByRef vntIndexKey As Object)
        'se usa al quitar un elemento de la colección
        'vntIndexKey contiene el índice o la clave, por lo que se
        'declara como un Variant
        'Sintaxis: x.Remove(xyz)


        mCol.Remove(vntIndexKey)
    End Sub


    Private Sub Class_Initialize_Renamed()
        'crea la colección cuando se crea la clase
        mCol = New Collection
    End Sub
    Public Sub New()
        MyBase.New()
        Class_Initialize_Renamed()
    End Sub


    Private Sub Class_Terminate_Renamed()
        'destruye la colección cuando se termina la clase
        mCol = Nothing
    End Sub
    Protected Overrides Sub Finalize()
        Class_Terminate_Renamed()
        MyBase.Finalize()
    End Sub
End Class






