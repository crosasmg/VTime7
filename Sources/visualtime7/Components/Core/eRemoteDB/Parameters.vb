Option Strict Off
Option Explicit On

Public Class Parameters
    Implements System.Collections.IEnumerable

#Region "Atributes"

    '**+Objective: Collection that supports the class: Parameters
    '**+Version: $$Revision: $
    '+Objetivo: Colección que le da soporte a la clase: Parameters
    '+Version: $$Revision: $

    '**-Objective:
    '-Objetivo:
    Private mcolParameters As Collection

#End Region

#Region "Constructors"

    '**%Objective: Controls the creation of an instance of the collection
    '%Objetivo: Controla la creación de una instancia de la colección
    Private Sub Class_Initialize_Renamed()
        ''On Error GoTo ErrorHandler

        mcolParameters = New Collection

        Exit Sub
ErrorHandler:
        ProcError("Parameters.Class_Initialize()")
    End Sub
    Public Sub New()
        MyBase.New()
        Class_Initialize_Renamed()
    End Sub

#End Region

#Region "Properties"

    '**%Objective: Returns an element of the collection (according to the index)
    '**%Parameters:
    '**%    vntIndexKey -
    '%Objetivo: Devuelve un elemento de la colección (segun índice)
    '%Parámetros:
    '%      vntIndexKey -
    Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Parameter
        Get
            ''On Error GoTo ErrorHandler
            Item = mcolParameters.Item(vntIndexKey)

            Exit Property
ErrorHandler:
            ProcError("Parameters.Item(vntIndexKey)", New Object() {vntIndexKey})
        End Get
    End Property

    '**%Objective: Returns the number of elements that the collection has
    '%Objetivo: Devuelve el número de elementos que posee la colección
    Public ReadOnly Property Count() As Integer
        Get
            ''On Error GoTo ErrorHandler
            Count = mcolParameters.Count()

            Exit Property
ErrorHandler:
            ProcError("Parameters.Count()")
        End Get
    End Property

    '**%Objective: Enumerates the collection for use in a For Each...Next loop
    '%Objetivo: Permite enumerar la colección para utilizarla en un ciclo For Each... Next

    'Public ReadOnly Property NewEnum() As stdole.IUnknown
    'Get
    ''On Error GoTo ErrorHandler
    'NewEnum = mcolParameters._NewEnum
    '
    'Exit Property
    'ErrorHandler: '
    'ProcError("Parameters.NewEnum()")
    'End Get
    'End Property

    Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
        GetEnumerator = mcolParameters.GetEnumerator
    End Function

    '**%Objective: This property returns the contents of the variable "Incomplete"
    '%Objetivo: Esta propiedad devuelve el contenido de la variable "Incomplete"
    Public ReadOnly Property Incomplete() As Boolean
        Get
            Dim lobjMember As Parameter

            ''On Error GoTo ErrorHandler

            Incomplete = False
            For Each lobjMember In mcolParameters
                If lobjMember.Incomplete Then
                    Incomplete = True
                    Exit Property
                End If
            Next lobjMember
            lobjMember = Nothing

            Exit Property
ErrorHandler:
            ProcError("Parameters.Incomplete()")
        End Get
    End Property

#End Region

#Region "Methods"

    '**%Objective: Controls the destruction of an instance of the collection
    '%Objetivo: Controla la destrucción de una instancia de la colección
    Private Sub Class_Terminate_Renamed()
        ''On Error GoTo ErrorHandler

        mcolParameters = Nothing

        Exit Sub
ErrorHandler:
        ProcError("Parameters.Class_Terminate()")
    End Sub
    Protected Overrides Sub Finalize()
        Class_Terminate_Renamed()
        MyBase.Finalize()
    End Sub


    '**%Objective: adds a new instance of the "Parameter" class to the collection
    '**%Parameters:
    '**%    Name         -
    '**%    Value        -
    '**%    Direction    -
    '**%    ParType      -
    '**%    Size         -
    '**%    NumericScale -
    '**%    Precision    -
    '**%    Attributes   -
    '**%    ParObject    -
    '%Objetivo: Añade una nueva instancia de la clase "Parameter" a la colección
    '%Parámetros:
    '%      Name         -
    '%      Value        -
    '%      Direction    -
    '%      ParType      -
    '%      Size         -
    '%      NumericScale -
    '%      Precision    -
    '%      Attributes   -
    '%      ParObject    -
    Public Function Add(ByVal Name As String, ByVal Value As Object, Optional ByVal Direction As Parameter.eRmtDataDir = Parameter.eRmtDataDir.rdbParamInput, Optional ByVal ParType As Parameter.eRmtDataType = Parameter.eRmtDataType.rdbEmpty, Optional ByVal Size As Integer = 0, Optional ByVal NumericScale As Byte = 0, Optional ByVal Precision As Byte = 0, Optional ByVal Attributes As Parameter.eRmtDataAttrib = 0, Optional ByVal ParObject As Object = Nothing, Optional ByVal DoDecrypt As Boolean = False) As Parameter
        Dim objNewMember As Parameter

        ''On Error GoTo ErrorHandler

        objNewMember = New Parameter
        With objNewMember
            .Name = Name
            If DoDecrypt Then
                .Value = CryptSupport.EncryptString(Value)
            Else
                .Value = Value
            End If
            .Direction = Direction
            .ParType = ParType
            .Size = Size
            .NumericScale = NumericScale
            .Precision = Precision
            .Attributes = Attributes
            If Not ParObject Is Nothing Then
                .ParObject = ParObject
            End If
        End With

        mcolParameters.Add(objNewMember, objNewMember.Name)
        Add = objNewMember
        objNewMember = Nothing

        Exit Function
ErrorHandler:
        ProcError("Parameters.Add(Name,Value,Direction,ParType,Size,NumericScale,Precision,Attributes,ParObject)", New Object() {Name, Value, Direction, ParType, Size, NumericScale, Precision, Attributes, ParObject})
    End Function
    '**%Objective: Deletes an element from the collection
    '**%Parameters:
    '**%    vntIndexKey -
    '%Objetivo: Elimina un elemento de la colección
    '%Parámetros:
    '%      vntIndexKey -
    Public Sub Remove(ByRef vntIndexKey As Object)
        ''On Error GoTo ErrorHandler
        mcolParameters.Remove(vntIndexKey)

        Exit Sub
ErrorHandler:
        ProcError("Parameters.Remove(vntIndexKey)", New Object() {vntIndexKey})
    End Sub

#End Region

End Class