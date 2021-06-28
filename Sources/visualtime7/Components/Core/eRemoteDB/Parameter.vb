Option Strict Off
Option Explicit On
Public Class Parameter

#Region "Constants"

    '**+Objective: Class that supports the table Parameter
    '**+           it's content is:
    '**+Version: $$Revision: $
    '+Objetivo: Clase que le da soporte a la tabla Parameter
    '+          cuyo contenido es:
    '+Version: $$Revision: $

    '**-Objective:
    '-Objetivo:
    Public Enum eRmtDataDir
        rdbParamUnknown = 0
        rdbParamInput = 1
        rdbParamOutput = 2
        rdbParamInputOutput = 3
        rdbParamReturnValue = 4
    End Enum

    '**-Objective:
    '-Objetivo:
    Public Enum eRmtDataType
        rdbEmpty = 0
        rdbBoolean = 2
        rdbChar = 3
        rdbDate = 4
        rdbDBTime = 4
        rdbNumeric = 5 'No existe equivalente en ADO.NET. Se iguala al valor para rdbDecimal
        rdbDecimal = 5
        rdbDouble = 6 'No existe equivalente en ADO.NET. Se utiliza el valor de SqlDbType.Float
        rdbImage = 7
        rdbInteger = 8
        rdbSmallInt = 16
        rdbDBTimeStamp = 19
        rdbVarchar = 22
        rdbCharFixedLength
    End Enum

    '**-Objective:
    '-Objetivo:
    Public Enum eRmtDataAttrib
        rdbParamSigned = 16
        rdbParamNullable = 64
        rdbParamLong = 128
    End Enum

#End Region

#Region "Private Atributes"
    '**-Objective:
    '-Objetivo:
    Private mstrName As String

    '**-Objective:
    '-Objetivo:
    Private mvntValue As Object

    '**-Objective:
    '-Objetivo:
    Private mlngDirection As eRmtDataDir

    '**-Objective:
    '-Objetivo:
    Private mlngParType As eRmtDataType

    '**-Objective:
    '-Objetivo:
    Private mlngSize As Integer

    '**-Objective:
    '-Objetivo:
    Private mbytNumericScale As Byte

    '**-Objective:
    '-Objetivo:
    Private mbytPrecision As Byte

    '**-Objective:
    '-Objetivo:
    Private mlngAttributes As eRmtDataAttrib

    '**-Objective:
    '-Objetivo:
    Private mobjParObject As Object

#End Region

#Region "Constructors"

    '**%Objective: Controls the creation of an instance of the class
    '%Objetivo: Controla la creación de una instancia de la clase
    Private Sub Class_Initialize_Renamed()
        ''On Error GoTo ErrorHandler
        Name = String.Empty

        Value = System.DBNull.Value
        Direction = eRmtDataDir.rdbParamInput
        ParType = eRmtDataType.rdbEmpty
        Size = 0
        NumericScale = 0
        Precision = 0
        Attributes = 0
        ParObject = Nothing

        Exit Sub
ErrorHandler:
        ProcError("Parameter.Class_Initialize()")
    End Sub
    Public Sub New()
        MyBase.New()
        Class_Initialize_Renamed()
    End Sub

#End Region

#Region "Properties"

    '**%Objective: This property determines if the parameter information is complete
    '**%           or not, to know if the parameters should be refreshed
    '%Objetivo: Esta propiedad permite determinar si la información del parametro esta completa
    '%          o no, para saber si se realiza el refresh de los parametros.
    Public ReadOnly Property Incomplete() As Boolean
        Get
            ''On Error GoTo ErrorHandler

            Incomplete = (Direction = eRmtDataDir.rdbParamUnknown Or ParType = eRmtDataType.rdbEmpty)

            Exit Property
ErrorHandler:
            ProcError("Parameter.Incomplete()")
        End Get
    End Property

    '**%Objective:
    '%Objetivo:

    '**%Objective:
    '**%Parameters:
    '**%    vNewValue -
    '%Objetivo:
    '%Parámetros:
    '%      vNewValue -
    Public Property Name() As String
        Get
            ''On Error GoTo ErrorHandler

            Name = mstrName

            Exit Property
ErrorHandler:
            ProcError("Parameter.Name()")
        End Get
        Set(ByVal Value As String)
            ''On Error GoTo ErrorHandler

            mstrName = Value

            Exit Property
ErrorHandler:
            ProcError("Parameter.Name(vNewValue)", New Object() {Value})
        End Set
    End Property

    '**%Objective:
    '%Objetivo:

    '**%Objective:
    '**%Parameters:
    '**%    vNewValue -
    '%Objetivo:
    '%Parámetros:
    '%      vNewValue -
    Public Property Value() As Object
        Get
            ''On Error GoTo ErrorHandler

            Value = mvntValue

            Exit Property
ErrorHandler:
            ProcError("Parameter.Value()")
        End Get
        Set(ByVal Value As Object)
            ''On Error GoTo ErrorHandler

            mvntValue = Value

            Exit Property
ErrorHandler:
            ProcError("Parameter.Value(vNewValue)", New Object() {Value})
        End Set
    End Property

    '**%Objective:
    '%Objetivo:

    '**%Objective:
    '**%Parameters:
    '**%    vNewValue -
    '%Objetivo:
    '%Parámetros:
    '%      vNewValue -
    Public Property Direction() As eRmtDataDir
        Get
            ''On Error GoTo ErrorHandler

            Direction = mlngDirection

            Exit Property
ErrorHandler:
            ProcError("Parameter.Direction()")
        End Get
        Set(ByVal Value As eRmtDataDir)
            ''On Error GoTo ErrorHandler

            mlngDirection = Value

            Exit Property
ErrorHandler:
            ProcError("Parameter.Direction(vNewValue)", New Object() {Value})
        End Set
    End Property

    '**%Objective:
    '%Objetivo:

    '**%Objective:
    '**%Parameters:
    '**%    vNewValue -
    '%Objetivo:
    '%Parámetros:
    '%      vNewValue -
    Public Property ParType() As eRmtDataType
        Get
            ''On Error GoTo ErrorHandler

            ParType = mlngParType

            Exit Property
ErrorHandler:
            ProcError("Parameter.ParType()")
        End Get
        Set(ByVal Value As eRmtDataType)
            ''On Error GoTo ErrorHandler

            mlngParType = Value

            Exit Property
ErrorHandler:
            ProcError("Parameter.ParType(vNewValue)", New Object() {Value})
        End Set
    End Property

    '**%Objective:
    '%Objetivo:

    '**%Objective:
    '**%Parameters:
    '**%    vNewValue -
    '%Objetivo:
    '%Parámetros:
    '%      vNewValue -
    Public Property Size() As Integer
        Get
            ''On Error GoTo ErrorHandler

            Size = mlngSize

            Exit Property
ErrorHandler:
            ProcError("Parameter.Size()")
        End Get
        Set(ByVal Value As Integer)
            ''On Error GoTo ErrorHandler

            mlngSize = Value

            Exit Property
ErrorHandler:
            ProcError("Parameter.Size(vNewValue)", New Object() {Value})
        End Set
    End Property

    '**%Objective:
    '%Objetivo:

    '**%Objective:
    '**%Parameters:
    '**%    vNewValue -
    '%Objetivo:
    '%Parámetros:
    '%      vNewValue -
    Public Property NumericScale() As Byte
        Get
            ''On Error GoTo ErrorHandler

            NumericScale = mbytNumericScale

            Exit Property
ErrorHandler:
            ProcError("Parameter.NumericScale()")
        End Get
        Set(ByVal Value As Byte)
            ''On Error GoTo ErrorHandler

            mbytNumericScale = Value

            Exit Property
ErrorHandler:
            ProcError("Parameter.NumericScale(vNewValue)", New Object() {Value})
        End Set
    End Property

    '**%Objective:
    '%Objetivo:

    '**%Objective:
    '**%Parameters:
    '**%    vNewValue -
    '%Objetivo:
    '%Parámetros:
    '%      vNewValue -
    Public Property Precision() As Byte
        Get
            ''On Error GoTo ErrorHandler

            Precision = mbytPrecision

            Exit Property
ErrorHandler:
            ProcError("Parameter.Precision()")
        End Get
        Set(ByVal Value As Byte)
            ''On Error GoTo ErrorHandler

            mbytPrecision = Value

            Exit Property
ErrorHandler:
            ProcError("Parameter.Precision(vNewValue)", New Object() {Value})
        End Set
    End Property

    '**%Objective:
    '%Objetivo:

    '**%Objective:
    '**%Parameters:
    '**%    vNewValue -
    '%Objetivo:
    '%Parámetros:
    '%      vNewValue -
    Public Property Attributes() As eRmtDataAttrib
        Get
            ''On Error GoTo ErrorHandler

            Attributes = mlngAttributes

            Exit Property
ErrorHandler:
            ProcError("Parameter.Attributes()")
        End Get
        Set(ByVal Value As eRmtDataAttrib)
            ''On Error GoTo ErrorHandler

            mlngAttributes = Value

            Exit Property
ErrorHandler:
            ProcError("Parameter.Attributes(vNewValue)", New Object() {Value})
        End Set
    End Property

    '**%Objective:
    '%Objetivo:

    '**%Objective:
    '**%Parameters:
    '**%    vNewValue -
    '%Objetivo:
    '%Parámetros:
    '%      vNewValue -
    Public Property ParObject() As Object
        Get
            ''On Error GoTo ErrorHandler

            ParObject = mobjParObject

            Exit Property
ErrorHandler:
            ProcError("Parameter.ParObject()")
        End Get
        Set(ByVal Value As Object)
            ''On Error GoTo ErrorHandler

            mobjParObject = Value

            Exit Property
ErrorHandler:
            ProcError("Parameter.ParObject(vNewValue)", New Object() {Value})
        End Set
    End Property

#End Region

End Class