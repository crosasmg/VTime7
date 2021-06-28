Option Strict Off
Option Explicit On

Imports System.Data.Common
Imports System.Reflection
Imports eRemoteDB

Public Class Execute

#Region "Enumerated"
    '**+Objective: Class that supports the table Execute
    '**+           it's content is:
    '**+Version: $$Revision: 2 $
    '+Objetivo: Clase que le da soporte a la tabla Execute
    '+          cuyo contenido es:
    '+Version: $$Revision: 2 $

    '**-Objective:
    '-Objetivo:
    Public Enum eStateOO
        rooClosed = 0
        rooOpen = 1
        rooConnecting = 2
        rooExecuting = 4
        rooFetching = 8
    End Enum

    '**-Objective: Define the enumerated list to handle the error number to return for the
    '**-           execution of routines for the "Stored Procedure"
    '-Objetivo: Se define la lista enumerada para manajador los numero de error a dovolver por las
    '-          rutinas de ejecución para los "Stored Procedure".
    Public Enum ErrorDB
        clngOK = 0
        clngNotFound = 100
    End Enum

    '**-Objective:
    '-Objetivo:
    Public Enum eTypeControl
        ecCheckBox
    End Enum

    '**-Objective:
    '-Objetivo:
    Public Enum eState
        rdbClosed = 0
        rdbOpen = 1
        rdbConnecting = 2
        rdbExecuting = 4
        rdbFetching = 8
    End Enum

    '**-Objective: Enumerated list to get or establish values for the type of fields BINARY or TEXT
    '-Objetivo: Lista enumerada para obtener o establecer valores para campo del tipo BINARIO o TEXT
    Public Enum eFieldObject
        efoPicture
        efoOLE
    End Enum
#End Region

#Region "Atributes"
    '**-Objective:
    '-Objetivo:
    Private mstrSessionID As String

    '**-Objective:
    '-Objetivo:
    Private mblnSpecial As Boolean

    '**-Objective:
    '-Objetivo:
    Private mstrSQL As String

    '**-Objective:
    '-Objetivo:
    Private mlngReturnValue As Integer

    '**-Objective:
    '-Objetivo:
    Private mlngErrorNumber As Integer

    '**-Objective:
    '-Objetivo:
    Private mstrErrorMsg As String

    '**-Objective:
    '-Objetivo:
    Private mblnHideErrorMsg As Boolean

    '**-Objective:
    '-Objetivo:
    Private mlngWarningNumber As Integer

    '**-Objective:
    '-Objetivo:
    Private mstrWarningMsg As String

    '**-Objective:
    '-Objetivo:
    Private mblnShowWarnings As Boolean

    '**-Objective:
    '-Objetivo:
    Private mblnPageBehavior As Boolean

    '**-Objective: Variable that indicate application is in the erros module
    '-Objetivo: Variable que indica que se encuentra en el módulo de errores
    Public bErr_Module As Boolean

    '**-Objective:
    '-Objetivo:
    Private mclsConnection As eRemoteDB.Connection

    '**-Objective:
    '-Objetivo:
    'Private mvarcmd As SqlClient.SqlCommand
    Private mvarcmd As System.Data.Common.DbCommand

    '**-Objective:
    '-Objetivo:
    'Private mrecRecord As SqlClient.SqlDataReader
    Private mrecRecord As System.Data.Common.DbDataReader

    '**-Objective:
    '-Objetivo:
    Private mcolParameters As Parameters

    '**-Objective:
    '-Objetivo:
    Private mvarStoredProcedure As String

    '**-Objective:
    '-Objetivo:
    Private mvarOwner As String

    '**-Objective:
    '-Objetivo:
    Private mstrName As String

    '**-Objective:
    '-Objetivo:
    Private menuState As eStateOO

    '**-Objective:
    '-Objetivo:
    Private mlngPageSize As Integer

    '**-Objective:
    '-Objetivo:
    Private mstrDecSeparator As String

    '**-Objective:
    '-Objetivo:
    Private mblnGetRecordset As Boolean

    '**-Objective:
    '-Objetivo:
    Private mblnRefreshParameters As Boolean

    '**-Objective:
    '-Objetivo:
    Private mlngTimeout As Integer

    '**-Objective:
    '-Objetivo:
    Private mblnDebugActive As Boolean

    '**-Objective:
    '-Objetivo:
    Private mblnLogActive As Boolean

    '**-Objective:
    '-Objetivo:
    Private mTypeServer As Connection.sTypeServer

    Private mblnEOF As Boolean

    Private mblnPersistance As Boolean

    Private _isTabTablesSP As Boolean

#End Region

#Region "Properties"
    '**%Objective: Indicates how long to wait while executing a command before terminating the attempt and generating an error.
    '**%Parameters:
    '**%    vNewValue - Sets or returns a Long value that indicates, in seconds, how long to wait for a command to execute. Default is 30
    '%Objetivo:
    '%Parámetros:
    '%    vNewValue -
    Public Property Timeout() As Integer
        Get
            If Not IsIDEMode() Then
                'On Error GoTo ErrorHandler
            End If

            Timeout = mlngTimeout

            Exit Property
ErrorHandler:
            ProcError("Execute.Timeout()")
        End Get
        Set(ByVal Value As Integer)
            If Not IsIDEMode() Then
                'On Error GoTo ErrorHandler
            End If

            mlngTimeout = Value

            Exit Property
ErrorHandler:
            ProcError("Execute.Timeout(vNewValue)", New Object() {Value})
        End Set
    End Property


    '**%Objective:
    '%Objetivo:
    Public ReadOnly Property FieldsCount() As Integer
        Get
            If Not IsIDEMode() Then
                'On Error GoTo ErrorHandler
            End If
            FieldsCount = mrecRecord.FieldCount

            Exit Property
ErrorHandler:
            ProcError("Execute.FieldsCount()")
        End Get
    End Property

    Public Property IsTabTablesSP As Boolean
        Get
            Return _isTabTablesSP
        End Get

        Set(ByVal value As Boolean)
            _isTabTablesSP = value
        End Set
    End Property


    '**%Objective:
    '**%Parameters:
    '**%    nValue -
    '%Objetivo:
    '%Parámetros:
    '%      nValue -
    '%OBSERVACION: Validar con las paginas, antes de ser eliminado

    '**%Objective:
    '%Objetivo:
    Public Property AbsolutePage() As Integer
        Get
            If Not IsIDEMode() Then
                'On Error GoTo ErrorHandler
            End If
            'NS AbsolutePage = mrecRecord.AbsolutePosition / mlngPageSize + 1

            Exit Property
ErrorHandler:
            ProcError("Execute.AbsolutePage()")
        End Get
        Set(ByVal Value As Integer)
            Dim llngBookMark As Integer

            If Not IsIDEMode() Then
                'On Error GoTo ErrorHandler
            End If
            'NS llngBookMark = (Value - 1) * mlngPageSize + 1 - mrecRecord.AbsolutePosition
            If llngBookMark > 0 Then
                'NS mrecRecord.Move(System.Math.Abs(llngBookMark))
            End If

            Exit Property
ErrorHandler:
            ProcError("Execute.AbsolutePage(nValue)", New Object() {Value})
        End Set
    End Property

    '**%Objective:
    '**%Parameters:
    '**%    nValue -
    '%Objetivo:
    '%Parámetros:
    '%      nValue -
    '%OBSERVACION: Validar con las paginas, antes de ser eliminado

    '**%Objective:
    '%Objetivo:
    Public Property PageSize() As Integer
        Get
            If Not IsIDEMode() Then
                'On Error GoTo ErrorHandler
            End If

            PageSize = mlngPageSize

            Exit Property
ErrorHandler:
            ProcError("Execute.PageSize()")
        End Get
        Set(ByVal Value As Integer)
            If Not IsIDEMode() Then
                'On Error GoTo ErrorHandler
            End If

            mlngPageSize = Value

            Exit Property
ErrorHandler:
            ProcError("Execute.PageSize(nValue)", New Object() {Value})
        End Set
    End Property

    '**%Objective:
    '%Objetivo:
    '%OBSERVACION: Validar con las paginas, antes de ser eliminado
    Public ReadOnly Property PageCount() As Integer
        Get
            If Not IsIDEMode() Then
                'On Error GoTo ErrorHandler
            End If

            'NS PageCount = mrecRecord.RecordCount / mlngPageSize

            Exit Property
ErrorHandler:
            ProcError("Execute.PageCount()")
        End Get
    End Property

    '**%Objective:
    '%Objetivo:
    Public Property Owner() As String
        Get
            Dim lclsVisualTimeConfig As eRemoteDB.VisualTimeConfig

            If Not IsIDEMode() Then
                'On Error GoTo ErrorHandler
            End If

            lclsVisualTimeConfig = New eRemoteDB.VisualTimeConfig
            With lclsVisualTimeConfig
                mvarOwner = lclsVisualTimeConfig.LoadSetting("Owner", "", "database")
                If lclsVisualTimeConfig.LoadSetting("Server", "Oracle", "Database").ToUpper <> "ORACLE" Then
                    Owner = mvarOwner & "."
                Else
                    Owner = mvarOwner
                End If
            End With
            lclsVisualTimeConfig = Nothing
            Exit Property
ErrorHandler:
            ProcError("Execute.Owner()")
        End Get
        Set(ByVal Value As String)
            If Not IsIDEMode() Then
                'On Error GoTo ErrorHandler
            End If

            mvarOwner = Value

            Exit Property
ErrorHandler:
            ProcError("Execute.Owner(vData)", New Object() {Value})
        End Set
    End Property

    '**%Objective:
    '%Objetivo:
    Public ReadOnly Property EOF() As Boolean
        Get
            If Not IsIDEMode() Then
                'On Error GoTo ErrorHandler
            End If

            EOF = mblnEOF
            If PageBehavior And Not EOF Then
                'NS EOF = mrecRecord.AbsolutePosition Mod PageSize = 0
            End If

            Exit Property
ErrorHandler:
            ProcError("Execute.EOF()")
        End Get
    End Property

    '**%Objective:
    '%Objetivo:

    '**%Objective: Function that sets Paramaters to the parameter collection
    '**%Parameters:
    '**%    vData -
    '%Objetivo:
    '%Parámetros:
    '%      vData -
    Public Property Parameters() As Parameters
        Get
            If Not IsIDEMode() Then
                'On Error GoTo ErrorHandler
            End If

            If mcolParameters Is Nothing Then
                mcolParameters = New Parameters
            End If
            Parameters = mcolParameters

            Exit Property
ErrorHandler:
            ProcError("Execute.Parameters()")
        End Get
        Set(ByVal Value As Parameters)
            If Not IsIDEMode() Then
                'On Error GoTo ErrorHandler
            End If

            mcolParameters = Value

            Exit Property
ErrorHandler:
            ProcError("Execute.Parameters(vData)", New Object() {Parameters})
        End Set
    End Property

    '**%Objective: This property updates the contents of the variable "StoredProcedure"
    '**%Parameters:
    '**%    vData -
    '%Objetivo: Esta propiedad actualiza el contenido el variable "StoredProcedure"
    '%Parámetros:
    '%      vData -

    '**%Objective:
    '%Objetivo:
    Public Property StoredProcedure() As String
        Get
            If Not IsIDEMode() Then
                'On Error GoTo ErrorHandler
            End If

            StoredProcedure = mvarStoredProcedure

            Exit Property
ErrorHandler:
            ProcError("Execute.StoredProcedure()")
        End Get
        Set(ByVal Value As String)
            Dim lintPos As Short

            If Not IsIDEMode() Then
                'On Error GoTo ErrorHandler
            End If

            '**+If the owner is specified, this is separate from the name of the Stored Procedure
            '+Si se espesifica el propietario este es separado del nombre del Stored Procedure
            lintPos = InStr(Value, ".")
            mvarStoredProcedure = Value

            '+Siempre que se active este comando para un comando diferente, se eliminarán todos
            '+los parametros asociados
            mcolParameters = Nothing

            Exit Property
ErrorHandler:
            ProcError("Execute.StoredProcedure(vData)", New Object() {Value})
        End Set
    End Property

    '**%Objective:
    '%Objetivo:
    Public ReadOnly Property State() As eStateOO
        Get
            If Not IsIDEMode() Then
                'On Error GoTo ErrorHandler
            End If

            State = menuState

            Exit Property
ErrorHandler:
            ProcError("Execute.State()")
        End Get
    End Property

    '**%Objective:
    '%Objetivo:
    Public ReadOnly Property Server() As Connection.sTypeServer
        Get
            If Not IsIDEMode() Then
                'On Error GoTo ErrorHandler
            End If

            Server = mTypeServer

            Exit Property
ErrorHandler:
            ProcError("Execute.Server()")
        End Get
    End Property

    '**%Objective:
    '%Objetivo: Obtiene la conección activa
    Public ReadOnly Property Recordset() As DbDataReader
        'Public ReadOnly Property Recordset() As SqlClient.SqlDataReader
        Get
            If Not IsIDEMode() Then
                'On Error GoTo ErrorHandler
            End If

            Recordset = mrecRecord
            mblnGetRecordset = True

            Exit Property
ErrorHandler:
            ProcError("Execute.Recordset()")
        End Get
    End Property

    '**%Objective:
    '%Objetivo:

    '**%Objective:
    '**%Parameters:
    '**%    vNewValue -
    '%Objetivo:
    '%Parámetros:
    '%    vNewValue -
    Public Property sSessionID() As String
        Get
            If Not IsIDEMode() Then
                'On Error GoTo ErrorHandler
            End If

            sSessionID = mstrSessionID

            Exit Property
ErrorHandler:
            ProcError("Execute.sSessionID()")
        End Get
        Set(ByVal Value As String)
            If Not IsIDEMode() Then
                'On Error GoTo ErrorHandler
            End If

            mstrSessionID = Value

            Exit Property
ErrorHandler:
            ProcError("Execute.sSessionID(vNewValue)", New Object() {Value})
        End Set
    End Property

    '**%Objective:
    '%Objetivo:

    '**%Objective:
    '**%Parameters:
    '**%    vNewValue -
    '%Objetivo:
    '%Parámetros:
    '%    vNewValue -
    Public Property Special() As Boolean
        Get
            If Not IsIDEMode() Then
                'On Error GoTo ErrorHandler
            End If

            Special = mblnSpecial

            Exit Property
ErrorHandler:
            ProcError("Execute.Special()")
        End Get
        Set(ByVal Value As Boolean)
            If Not IsIDEMode() Then
                'On Error GoTo ErrorHandler
            End If

            mblnSpecial = Value

            Exit Property
ErrorHandler:
            ProcError("Execute.Special(vNewValue)", New Object() {Value})
        End Set
    End Property

    '**%Objective:
    '%Objetivo:

    '**%Objective:
    '**%Parameters:
    '**%    vNewValue -
    '%Objetivo:
    '%Parámetros:
    '%    vNewValue -
    Public Property SQL() As String
        Get
            If Not IsIDEMode() Then
                'On Error GoTo ErrorHandler
            End If

            SQL = mstrSQL

            Exit Property
ErrorHandler:
            ProcError("Execute.SQL()")
        End Get
        Set(ByVal Value As String)
            If Not IsIDEMode() Then
                'On Error GoTo ErrorHandler
            End If

            mstrSQL = Value

            Exit Property
ErrorHandler:
            ProcError("Execute.SQL(vNewValue)", New Object() {Value})
        End Set
    End Property

    '**%Objective:
    '%Objetivo:

    '**%Objective:
    '**%Parameters:
    '**%    vNewValue -
    '%Objetivo:
    '%Parámetros:
    '%    vNewValue -
    Public Property ReturnValue() As Integer
        Get
            If Not IsIDEMode() Then
                'On Error GoTo ErrorHandler
            End If

            ReturnValue = mlngReturnValue

            Exit Property
ErrorHandler:
            ProcError("Execute.ReturnValue()")
        End Get
        Set(ByVal Value As Integer)
            If Not IsIDEMode() Then
                'On Error GoTo ErrorHandler
            End If

            mlngReturnValue = Value

            Exit Property
ErrorHandler:
            ProcError("Execute.ReturnValue(vNewValue)", New Object() {Value})
        End Set
    End Property

    '**%Objective:
    '%Objetivo:

    '**%Objective:
    '**%Parameters:
    '**%    vNewValue -
    '%Objetivo:
    '%Parámetros:
    '%    vNewValue -
    Public Property ErrorNumber() As Integer
        Get
            If Not IsIDEMode() Then
                'On Error GoTo ErrorHandler
            End If

            ErrorNumber = mlngErrorNumber

            Exit Property
ErrorHandler:
            ProcError("Execute.ErrorNumber()")
        End Get
        Set(ByVal Value As Integer)
            If Not IsIDEMode() Then
                'On Error GoTo ErrorHandler
            End If

            mlngErrorNumber = Value

            Exit Property
ErrorHandler:
            ProcError("Execute.ErrorNumber(vNewValue)", New Object() {Value})
        End Set
    End Property

    '**%Objective:
    '%Objetivo:

    '**%Objective:
    '**%Parameters:
    '**%    vNewValue -
    '%Objetivo:
    '%Parámetros:
    '%    vNewValue -
    Public Property ErrorMsg() As String
        Get
            If Not IsIDEMode() Then
                'On Error GoTo ErrorHandler
            End If

            ErrorMsg = mstrErrorMsg

            Exit Property
ErrorHandler:
            ProcError("Execute.ErrorMsg()")
        End Get
        Set(ByVal Value As String)
            If Not IsIDEMode() Then
                'On Error GoTo ErrorHandler
            End If

            mstrErrorMsg = Value

            Exit Property
ErrorHandler:
            ProcError("Execute.ErrorMsg(vNewValue)", New Object() {Value})
        End Set
    End Property

    '**%Objective:
    '%Objetivo:

    '**%Objective:
    '**%Parameters:
    '**%    vNewValue -
    '%Objetivo:
    '%Parámetros:
    '%    vNewValue -
    Public Property HideErrorMsg() As Boolean
        Get
            If Not IsIDEMode() Then
                'On Error GoTo ErrorHandler
            End If

            HideErrorMsg = mblnHideErrorMsg

            Exit Property
ErrorHandler:
            ProcError("Execute.HideErrorMsg()")
        End Get
        Set(ByVal Value As Boolean)
            If Not IsIDEMode() Then
                'On Error GoTo ErrorHandler
            End If

            mblnHideErrorMsg = Value

            Exit Property
ErrorHandler:
            ProcError("Execute.HideErrorMsg(vNewValue)", New Object() {Value})
        End Set
    End Property

    '**%Objective:
    '%Objetivo:

    '**%Objective:
    '**%Parameters:
    '**%    vNewValue -
    '%Objetivo:
    '%Parámetros:
    '%    vNewValue -
    Public Property WarningNumber() As Integer
        Get
            If Not IsIDEMode() Then
                'On Error GoTo ErrorHandler
            End If

            WarningNumber = mlngWarningNumber

            Exit Property
ErrorHandler:
            ProcError("Execute.WarningNumber()")
        End Get
        Set(ByVal Value As Integer)
            If Not IsIDEMode() Then
                'On Error GoTo ErrorHandler
            End If

            mlngWarningNumber = Value

            Exit Property
ErrorHandler:
            ProcError("Execute.WarningNumber(vNewValue)", New Object() {Value})
        End Set
    End Property

    '**%Objective:
    '%Objetivo:

    '**%Objective:
    '**%Parameters:
    '**%    vNewValue -
    '%Objetivo:
    '%Parámetros:
    '%    vNewValue -
    Public Property WarningMsg() As String
        Get
            If Not IsIDEMode() Then
                'On Error GoTo ErrorHandler
            End If

            WarningMsg = mstrWarningMsg

            Exit Property
ErrorHandler:
            ProcError("Execute.WarningMsg()")
        End Get
        Set(ByVal Value As String)
            If Not IsIDEMode() Then
                'On Error GoTo ErrorHandler
            End If

            mstrWarningMsg = Value

            Exit Property
ErrorHandler:
            ProcError("Execute.WarningMsg(vNewValue)", New Object() {Value})
        End Set
    End Property

    '**%Objective:
    '%Objetivo:

    '**%Objective:
    '**%Parameters:
    '**%    vNewValue -
    '%Objetivo:
    '%Parámetros:
    '%    vNewValue -
    Public Property ShowWarnings() As Boolean
        Get
            If Not IsIDEMode() Then
                'On Error GoTo ErrorHandler
            End If

            ShowWarnings = mblnShowWarnings

            Exit Property
ErrorHandler:
            ProcError("Execute.ShowWarnings()")
        End Get
        Set(ByVal Value As Boolean)
            If Not IsIDEMode() Then
                'On Error GoTo ErrorHandler
            End If

            mblnShowWarnings = Value

            Exit Property
ErrorHandler:
            ProcError("Execute.ShowWarnings(vNewValue)", New Object() {Value})
        End Set
    End Property

    '**%Objective:
    '**%Parameters:
    '**%    vNewValue -
    '%Objetivo:
    '%Parámetros:
    '%    vNewValue -
    Public Property PageBehavior() As Boolean
        Get
            If Not IsIDEMode() Then
                'On Error GoTo ErrorHandler
            End If

            PageBehavior = mblnPageBehavior

            Exit Property
ErrorHandler:
            ProcError("Execute.ShowWarnings()")
        End Get
        Set(ByVal Value As Boolean)
            If Not IsIDEMode() Then
                'On Error GoTo ErrorHandler
            End If

            mblnPageBehavior = Value

            Exit Property
ErrorHandler:
            ProcError("Execute.PageBehavior(vNewValue)", New Object() {Value})
        End Set
    End Property

    '**%Objective:
    '%Objetivo:

    '**%Objective:
    '**%Parameters:
    '**%    oConnnection -
    '%Objetivo:
    '%Parámetros:
    '%      oConnnection -
    Public Property Connection() As eRemoteDB.Connection
        Get
            If Not IsIDEMode() Then
                'On Error GoTo ErrorHandler
            End If

            Connection = mclsConnection


            Exit Property
ErrorHandler:
            ProcError("Execute.Connection()")
        End Get
        Set(ByVal Value As eRemoteDB.Connection)
            If Not IsIDEMode() Then
                'On Error GoTo ErrorHandler
            End If

            mclsConnection = Value

            Exit Property
ErrorHandler:
            ProcError("Execute.Connection(oConnnection)", New Object() {Value})
        End Set
    End Property

#End Region

#Region "Constructors"
    '**%Objective: Controls the creation of an instance of the class
    '%Objetivo: Controla la creación de una instancia de la clase
    Private Sub Init()
        Dim lclsRegistry As eRemoteDB.VisualTimeConfig
        Dim lstrServer As String

        If Not IsIDEMode() Then
            'On Error GoTo ErrorHandler
        End If

        bErr_Module = False

        _isTabTablesSP = False
        mlngPageSize = 10
        mstrDecSeparator = Replace(CStr(1.1), "1", String.Empty)
        sSessionID = "000000000"

        lclsRegistry = New eRemoteDB.VisualTimeConfig
        mlngTimeout = lclsRegistry.LoadSetting("CommandTimeOut", -1, "Database")
        mblnDebugActive = (lclsRegistry.LoadSetting("Active", "No", "Debug") = "Yes")
        mblnLogActive = (lclsRegistry.LoadSetting("Log", "No", "Debug") = "Yes")

        lstrServer = UCase(lclsRegistry.LoadSetting("Server", "Oracle", "Database"))
        Select Case lstrServer
            Case "ORACLE"
                mTypeServer = eRemoteDB.Connection.sTypeServer.sOracle
            Case "SQL SERVER", "SQLSERVER7"
                mTypeServer = eRemoteDB.Connection.sTypeServer.sSQLServer7
            Case "DB2"
                mTypeServer = eRemoteDB.Connection.sTypeServer.sDB2
            Case "INFORMIX"
                mTypeServer = eRemoteDB.Connection.sTypeServer.sInformix
        End Select
        lclsRegistry = Nothing
        mblnPersistance = False

        Exit Sub
ErrorHandler:
        ProcError("Execute.Init()")
    End Sub

    Public Sub New()
        MyBase.New()
        Init()
    End Sub

    Public Sub New(ByVal persistance As Boolean)
        MyBase.New()
        Init()
        mblnPersistance = persistance
    End Sub

    Protected Overrides Sub Finalize()
        If Not IsNothing(mrecRecord) Then
            If Not IsNothing(mstrSQL) Then
                If mstrSQL.Length > 0 Then
                    NetHelper.WriteToEventLog("VisualTIME.Net: Error SQL: " + mstrSQL)
                End If
            End If
            If Not IsNothing(mvarStoredProcedure) Then
                If mvarStoredProcedure.Length > 0 Then
                    NetHelper.WriteToEventLog("VisualTIME.Net: Error SP: " + mvarStoredProcedure)
                End If
            End If
            Try
                If Not IsNothing(mrecRecord) Then
                    If Not mrecRecord.IsClosed Then
                        mrecRecord.Close()
                    End If
                    mrecRecord = Nothing
                End If

                If Not IsNothing(mclsConnection) Then
                    mclsConnection.CloseConnection()
                    mclsConnection = Nothing
                End If
            Catch ex As Exception
                NetHelper.WriteToEventLog("VisualTIME.Net: Error Exception: " + ex.Message)
            End Try
        End If
        mcolParameters = Nothing
        MyBase.Finalize()
    End Sub

#End Region

#Region "Functions"

#Region "Private Functions"

    Private Function DataReaderTypeToRemoteDB(ByVal sDataReaderType As String) As Parameter.eRmtDataType
        Select Case sDataReaderType.ToLower
            Case "char"
                DataReaderTypeToRemoteDB = Parameter.eRmtDataType.rdbChar
            Case "varchar", "varchar2"
                DataReaderTypeToRemoteDB = Parameter.eRmtDataType.rdbVarchar
            Case "datetime"
                DataReaderTypeToRemoteDB = Parameter.eRmtDataType.rdbDBTimeStamp
            Case "smallint"
                DataReaderTypeToRemoteDB = Parameter.eRmtDataType.rdbSmallInt
            Case "number"
                DataReaderTypeToRemoteDB = Parameter.eRmtDataType.rdbNumeric

            Case Else
                DataReaderTypeToRemoteDB = Parameter.eRmtDataType.rdbEmpty
        End Select
    End Function

    '%insFixChaParam. This function change some special characters like vblf or vbcr for vbcrlf.
    '%insFixChaParam. Esta función cambia algunos caracteres especiales como vbLf o vbCr para vbcrlf.
    '----------------------------------------------------------------------------------------------------
    Private Function insFixChaParam(ByVal sValue As String) As String
        '----------------------------------------------------------------------------------------------------
        Dim lblnReplace As Boolean
        If InStr(1, sValue, Chr(160)) > 0 Then
            sValue = Replace(sValue, Chr(160), Chr(32))
        End If
        If InStr(1, sValue, vbCrLf) > 0 Then
            sValue = Replace(sValue, vbCrLf, "~vbcrlVisualtimevbcrlf~")
            lblnReplace = True
        End If
        If InStr(1, sValue, Chr(10)) > 0 Then
            sValue = Replace(sValue, Chr(10), "~vbcrlVisualtimevbcrlf~")
            lblnReplace = True
        End If
        If InStr(1, sValue, Chr(13)) > 0 Then
            sValue = Replace(sValue, Chr(13), Chr(10))
        End If
        If lblnReplace Then
            sValue = Replace(sValue, "~vbcrlVisualtimevbcrlf~", Chr(10))
        End If
        insFixChaParam = sValue
    End Function

#End Region

#Region "Public Functions"

    '**%Objective:
    '**%Parameters:
    '**%    bChange -
    '%Objetivo: '%ChangeToErrModule. Este metodo cambia en módulo al módulo de errores
    '%Parámetros:
    '%      bChange -
    Public Function ChangeToErrModule(ByVal bChange As Boolean) As Boolean
        If Not IsIDEMode() Then
            'On Error GoTo ErrorHandler
        End If

        bErr_Module = bChange

        Exit Function
ErrorHandler:
        ProcError("Execute.ChangeToErrModule(bChange)", New Object() {bChange})
    End Function

    '**%Objective: Function that sets the mapping Field-to-Class
    '**%Parameters:
    '**%    FieldName -
    '**%    DefValue  -
    '%Objetivo:
    '%Parámetros:
    '%      FieldName -
    '%      DefValue  -
    Public Function FieldToClass(ByVal FieldName As String, Optional ByVal DefValue As Object = Nothing, Optional ByVal DoDecrypt As Boolean = False) As Object
        Dim intFieldIndex As Integer
        Dim intDataType As Integer

        If Not IsIDEMode() Then
            'On Error GoTo ErrorHandler
        End If

        Try
            If (Not IsNothing(mrecRecord)) Then
                intFieldIndex = mrecRecord.GetOrdinal(FieldName)
            End If
        Catch ex As Exception When mblnHideErrorMsg
            mlngErrorNumber = 666
            intFieldIndex = -1
        End Try

        If intFieldIndex > -1 And Not IsNothing(mrecRecord) Then
            intDataType = DataReaderTypeToRemoteDB(mrecRecord.GetDataTypeName(intFieldIndex))
            With mrecRecord
                FieldToClass = RmtFieldToClass(FieldName, intDataType, .GetValue(intFieldIndex), DefValue, mblnHideErrorMsg, mlngErrorNumber, mstrErrorMsg, DoDecrypt)
            End With
        Else
            FieldToClass = Nothing
        End If

        Exit Function
ErrorHandler:
        ProcError("Execute.FieldToClass(FieldName,DefValue)", New Object() {FieldName, DefValue}, , , "StoredProcedure: " & Trim(mvarStoredProcedure) & Trim(SQL), mblnHideErrorMsg, mlngErrorNumber, mstrErrorMsg)
    End Function

    '**%Objective: Function that sets the reverse mapping Class-to-Field
    '**%Parameters:
    '**%    vValue -
    '**%    nType  -
    '%Objetivo:
    '%Parámetros:
    '%      vValue -
    '%      nType  -
    '%OBSERVACION: Este metodo solo se usa en la clase Address.cls y Phone.cls
    Public Function ClassToField(ByVal vValue As Object, ByVal nType As Parameter.eRmtDataType) As Object
        Dim nValue As Integer = 0
        If Not IsIDEMode() Then
            'On Error GoTo ErrorHandler
        End If

        ClassToField = vValue
        Select Case nType
            Case Parameter.eRmtDataType.rdbSmallInt, Parameter.eRmtDataType.rdbInteger
                If IsDBNull(vValue) OrElse String.IsNullOrEmpty(vValue) OrElse vValue = intNull OrElse vValue = dblNull Then
                    ClassToField = System.DBNull.Value
                End If

            Case Parameter.eRmtDataType.rdbNumeric, Parameter.eRmtDataType.rdbDouble
                If IsNothing(vValue) OrElse IsDBNull(vValue) OrElse String.IsNullOrEmpty(vValue) OrElse vValue = dblNull OrElse vValue = intNull Then
                    ClassToField = System.DBNull.Value
                End If

            Case Parameter.eRmtDataType.rdbVarchar, Parameter.eRmtDataType.rdbChar, Parameter.eRmtDataType.rdbCharFixedLength
                If IsNothing(vValue) OrElse IsDBNull(vValue) OrElse Convert.ToString(vValue) = String.Empty Then
                    ClassToField = System.DBNull.Value
                ElseIf vValue.GetType.BaseType.Name = "Enum" Then
                    nValue = vValue
                    'ClassToField = nValue.ToString
                    ClassToField = insFixChaParam(nValue.ToString)
                End If

            Case Parameter.eRmtDataType.rdbDBTimeStamp
                If IsNothing(vValue) OrElse IsDBNull(vValue) OrElse String.IsNullOrEmpty(vValue) OrElse IsDate(vValue) AndAlso vValue = DateTime.MinValue Then
                    ClassToField = System.DBNull.Value
                ElseIf vValue.GetType.Name = "String" Then
                    ClassToField = CDate(vValue)
                End If
            Case Parameter.eRmtDataType.rdbDate
                If IsNothing(vValue) OrElse IsDBNull(vValue) OrElse String.IsNullOrEmpty(vValue) OrElse IsDate(vValue) AndAlso vValue = DateTime.MinValue Then
                    ClassToField = System.DBNull.Value
                End If

            Case Else
                ClassToField = System.DBNull.Value
                'Err.Raise(666, "eRemoteDB.Execute.ClassToField()", "Type unknow")
        End Select

        Exit Function
ErrorHandler:
        ProcError("Execute.ClassToField(vValue,nType)", New Object() {vValue, nType})
    End Function

    '**%Objective:
    '**%Parameters:
    '**%    Index -
    '%Objetivo:
    '%Parámetros:
    '%      Index -
    Public Function Item(ByVal Index As Integer) As String
        If Not IsIDEMode() Then
            'On Error GoTo ErrorHandler
        End If

        Item = mrecRecord.GetName(Index)

        Exit Function
ErrorHandler:
        ProcError("Execute.Item(Index)", New Object() {Index})
    End Function

    '**%Objective:
    '**%Parameters:
    '**%    strField -
    '%Objetivo:
    '%Parámetros:
    '%      strField -
    Public Function FieldType(ByVal FieldName As String) As Integer
        If Not IsIDEMode() Then
            'On Error GoTo ErrorHandler
        End If

        FieldType = DataReaderTypeToRemoteDB(mrecRecord.GetDataTypeName(mrecRecord.GetOrdinal(FieldName)))

        Exit Function
ErrorHandler:
        ProcError("Execute.FieldType(strField)", New Object() {FieldName})
    End Function


    Public Function FieldName(ByVal ordinal As Integer) As String

        FieldName = mrecRecord.GetName(ordinal)

        Exit Function
ErrorHandler:
        ProcError("Execute.FieldType(strField)", New Object() {FieldName})
    End Function



    '**%Objective: Function that executes a query against a database, returning true or false
    '**%Parameters:
    '**%    IsReturnRecordSet -
    '%Objetivo:
    '%Parámetros:
    '%      IsReturnRecordSet -
    'Public Function Run(ByVal IsReturnRecordSet As Boolean) As Boolean
    Public Function Run(Optional ByVal IsReturnRecordSet As Boolean = True) As Boolean
        Dim clsASPSupport As eRemoteDB.ASPSupport
        Dim sParamList As String

        'If Not IsIDEMode() Then
        On Error GoTo ErrorHandler
        'End If


        If mblnDebugActive Then
            DebugLog("Push", "Execute.Run(IsReturnRecordSet, StoredProcedure, SQL )", New Object() {IsReturnRecordSet, mvarStoredProcedure, SQL})
        End If

        If mblnLogActive Then
            clsASPSupport = New eRemoteDB.ASPSupport
            sSessionID = clsASPSupport.SessionID
            clsASPSupport = Nothing

            Call eRemoteDB.FileSupport.AddBufferToFile(sSessionID & "|Begin|Procedure|Run|" & Trim$(mvarStoredProcedure) & Trim$(SQL), sSessionID)
        End If

        Run = True
        ErrorNumber = 0
        ErrorMsg = String.Empty
        WarningNumber = 0
        WarningMsg = String.Empty
        mblnEOF = False

        If mclsConnection Is Nothing Then
            mclsConnection = New Connection
            If bErr_Module Then
                mclsConnection.RegPathConfig = "ErrorSystem"
            End If
            Call mclsConnection.OpenConnection(, , , sSessionID)
        End If
        '+Se carga la conexión y el valor de "timeout" por default
        If mvarStoredProcedure = String.Empty Then
            mstrName = SQL
        Else
            If mTypeServer = eRemoteDB.Connection.sTypeServer.sOracle Then
                mstrName = mvarStoredProcedure
            Else
                mstrName = Owner & mvarStoredProcedure
            End If
        End If

        mblnRefreshParameters = False
        mvarcmd = mclsConnection.Provider.CreateCommand()
        With mvarcmd
            If mlngTimeout <> -1 Then
                .CommandTimeout = mlngTimeout
            End If
            If mvarStoredProcedure = String.Empty Then
                .CommandText = mstrName
                .CommandType = CommandType.Text
                If Not mcolParameters Is Nothing AndAlso mcolParameters.Count > 0 Then
                    LoadParameters(IsTabTablesSP)
                End If
            Else
                .CommandType = CommandType.Text
                sParamList = LoadParameters(IsReturnRecordSet)
                .CommandText = "begin " & mstrName & "(" & sParamList & "); end;"
            End If
        End With

        If mblnDebugActive Then
            BluidEntryLog(mstrName, mvarcmd, IsReturnRecordSet)
        End If

        If IsReturnRecordSet Then
            If Not mrecRecord Is Nothing Then
                If Not mrecRecord.IsClosed AndAlso mblnPersistance Then
                    mrecRecord.Close()
                    mrecRecord = Nothing
                End If
            End If
            mvarcmd.Connection = mclsConnection.Connection_Renamed
            If Not mblnPersistance Then
                mrecRecord = mvarcmd.ExecuteReader(CommandBehavior.CloseConnection)
            Else
                mrecRecord = mvarcmd.ExecuteReader()
            End If
            If mrecRecord.HasRows Then
                mblnEOF = Not mrecRecord.Read()
            Else
                mblnEOF = True
                ErrorNumber = ErrorDB.clngNotFound
                If Not Special Then
                    mrecRecord.Close()
                    mrecRecord = Nothing

                    menuState = eStateOO.rooClosed
                    If Not mblnPersistance Then
                        mclsConnection.CloseConnection()
                        mclsConnection = Nothing
                    End If

                End If

            End If



            '**+ In case that don't restored any record, assing to the NumErro the constant
            '+ En caso de no devuelve ningun registro se le asigna al NumErro la constante de no encontrado

        Else
            mvarcmd.Connection = mclsConnection.Connection_Renamed
            mvarcmd.ExecuteNonQuery()
            If mblnDebugActive Then
                BluidEntryLog(mstrName, mvarcmd, IsReturnRecordSet)
            End If
        End If

        If mblnRefreshParameters Then
            RefreshParameters()
        End If
        mvarcmd.Dispose()

        If Not IsReturnRecordSet And _
           Not mblnPersistance Then
            mclsConnection.CloseConnection()
            mclsConnection = Nothing
        End If

        Run = (ErrorNumber = 0 Or Special)

        If mblnDebugActive Then
            DebugLog("Pop", "Execute.Run(Return)", New Object() {Run})
        End If

        If mblnLogActive Then
            Call eRemoteDB.FileSupport.AddBufferToFile(sSessionID & "|Finish|Procedure|Run|" & Trim$(mvarStoredProcedure) & Trim$(SQL), sSessionID)
        End If

        Exit Function
ErrorHandler:
        Run = False
        ProcError("Execute.Run(IsReturnRecordSet)", New Object() {IsReturnRecordSet}, , mclsConnection.Connection_Renamed, "StoredProcedure: " & Trim(mvarStoredProcedure) & Trim(SQL), mblnHideErrorMsg, mlngErrorNumber, mstrErrorMsg)
    End Function

    Public Function Table(ByVal sOption As String, ByVal sTable As String, Optional ByVal sFields As String = "") As String
        If Not IsIDEMode() Then
            'On Error GoTo ErrorHandler
        End If

        If sOption = "SearchKeyField" Then
            Table = "SELECT * FROM " & sTable & " WHERE 1 = 2"
        Else
            If sFields <> String.Empty Then
                Table = "SELECT * FROM " & sTable
            Else
                Table = "SELECT " & sFields & " FROM " & sTable
            End If

        End If

        Exit Function
ErrorHandler:
        ProcError("Execute.Table(sOption,sTable)", New Object() {sOption, sTable})
    End Function

    Public Function Condition(ByVal sTable As String, ByVal vCode As Object, ByVal sKeyField As String, ByVal sDesField As String, ByVal sCondition As String, ByVal sTypeOrder As String) As String
        If Not IsIDEMode() Then
            'On Error GoTo ErrorHandler
        End If

        If vCode & String.Empty <> String.Empty Then
            If IsNumeric(vCode) Then
                Condition = " WHERE " & sTable & "." & Trim(sKeyField) & " = " & CStr(vCode) & " AND " & sTable & ".SSTATREGT  = '1'"
            Else
                Condition = " WHERE " & sTable & "." & Trim(sKeyField) & " = " & "'" & vCode & "'" & " AND " & sTable & ".SSTATREGT = '1'"
            End If

        ElseIf sCondition = String.Empty Then
            Condition = " WHERE " & sTable & ".SSTATREGT = '1'" & " ORDER BY " & sTypeOrder

        Else
            Condition = " WHERE " & sTable & ".SSTATREGT = '1'" & " AND " & sTable & "." & sDesField & " LIKE '" & sCondition & " ORDER BY " & sTypeOrder
        End If

        Exit Function
ErrorHandler:
        ProcError("Execute.Condition(sTable,vCode,sKeyField,sDesField,sCondition,sTypeOrder)", New Object() {sTable, vCode, sKeyField, sDesField, sCondition, sTypeOrder})
    End Function

    Public Function ResultSet(ByVal sSource As String, Optional ByVal aParameters As Object = Nothing, Optional ByVal bReturnRecordSet As Boolean = True, Optional ByVal bSQLStatement As Boolean = False, Optional ByVal sInitials As String = "", Optional ByVal sPassword As String = "", Optional ByVal sConnectionString As String = "") As DbDataReader
        'Public Function ResultSet(ByVal sSource As String, Optional ByVal aParameters As Object = Nothing, Optional ByVal bReturnRecordSet As Boolean = True, Optional ByVal bSQLStatement As Boolean = False, Optional ByVal sInitials As String = "", Optional ByVal sPassword As String = "", Optional ByVal sConnectionString As String = "") As SqlClient.SqlDataReader
        If Not IsIDEMode() Then
            'On Error GoTo ErrorHandler
        End If

        If Run(bReturnRecordSet) Then
            ResultSet = mrecRecord
            mblnGetRecordset = True
            Call RCloseRec()
        Else
            ResultSet = Nothing
        End If
        Exit Function
ErrorHandler:
        ProcError("Execute.ResultSet(sSource,aParameters,bReturnRecordSet,bSQLStatement,sInitials,sPassword,sConnectionString)", New Object() {sSource, aParameters, bReturnRecordSet, bSQLStatement, sInitials, sPassword, sConnectionString})
    End Function

    Public Function ArraySet(ByVal sSource As String, Optional ByVal aParameters As Object = Nothing, Optional ByVal bReturnRecordSet As Boolean = True, Optional ByVal bSQLStatement As Boolean = False, Optional ByVal sInitials As String = "", Optional ByVal sPassword As String = "", Optional ByVal sConnectionString As String = "") As Object
        If Not IsIDEMode() Then
            'On Error GoTo ErrorHandler
        End If

        ArraySet = Nothing
        If bSQLStatement Then
            SQL = sSource
        Else
            StoredProcedure = sSource
        End If
        If Run(bReturnRecordSet) Then
            'NS ArraySet = mrecRecord.
            Call RCloseRec()
        Else
            ArraySet = New Object() {}
        End If
        Exit Function
ErrorHandler:
        ProcError("Execute.ArraySet(sSource,aParameters,bReturnRecordSet,bSQLStatement,sInitials,sPassword,sConnectionString)", New Object() {sSource, aParameters, bReturnRecordSet, bSQLStatement, sInitials, sPassword, sConnectionString})
    End Function

    '**%Objective:
    '**%Parámetros:
    '**%      sTablename -
    '**%      nSize      -
    '%Objetivo: Obtiene información del campo clave para una table del tipo valores posibles (TABLEXXX)
    '%Parámetros:
    '%      sTablename - Nombre de la tabla
    '%      nSize      - Tamaño permitido para la edición del campo clave
    Public Function GetTablePrimaryKeyInfo(ByRef sTablename As String, ByRef nSize As Short, ByRef nSizeDesc As Long, ByRef nSizeShortDesc As Long) As String
        Dim lstrField As String = String.Empty
        Dim lclsVTConfig As New eRemoteDB.VisualTimeConfig
        Dim npass As Integer = 0

        If Not IsIDEMode() Then
            'On Error GoTo ErrorHandler
        End If

        GetTablePrimaryKeyInfo = String.Empty
        If Server = eRemoteDB.Connection.sTypeServer.sOracle Then
            SQL = "SELECT ALL_TAB_COLUMNS.COLUMN_NAME, ALL_TAB_COLUMNS.DATA_TYPE AS TYPE_NAME, ALL_TAB_COLUMNS.DATA_LENGTH AS LENGTH, ALL_TAB_COLUMNS.DATA_PRECISION AS PRECISION" & vbCr & _
                  "  FROM (ALL_TAB_COLUMNS)" & vbCr & _
                  " WHERE ALL_TAB_COLUMNS.OWNER      = '" & lclsVTConfig.LoadSetting("Owner", String.Empty, "Database").ToUpper() & "'" & vbCr & _
                  "   AND ALL_TAB_COLUMNS.TABLE_NAME = '" & sTablename.ToUpper() & "'"
        Else
            SQL = "SP_COLUMNS " & sTablename.ToUpper() & ", " & lclsVTConfig.LoadSetting("Owner", String.Empty, "Database").ToUpper()
        End If

        Special = True

        If Run(True) Then
            Do While Not EOF
                lstrField = FieldToClass("COLUMN_NAME")
                If "sdescript|sshort_des|dcompdate|nusercode|sstatregt".IndexOf(lstrField.ToLower) < 0 Then
                    Select Case FieldToClass("TYPE_NAME").ToString.ToUpper()
                        Case "NUMBER"
                            nSize = FieldToClass("PRECISION")
                        Case "SMALLINT"
                            nSize = 4
                        Case Else
                            nSize = FieldToClass("LENGTH")
                    End Select

                    If GetTablePrimaryKeyInfo <> lstrField And npass = 0 Then
                        GetTablePrimaryKeyInfo = lstrField
                        npass = 1
                    End If
                    '                    Exit Do
                Else
                    If lstrField.ToUpper = "SDESCRIPT" Then
                        nSizeDesc = FieldToClass("LENGTH")
                    End If
                    If lstrField.ToUpper = "SSHORT_DES" Then
                        nSizeShortDesc = FieldToClass("LENGTH")
                    End If

                End If
                RNext()
            Loop
            RCloseRec()
        End If

        Special = False

        Exit Function
ErrorHandler:
        ProcError("Execute.GetTablePrimaryKeyInfo(sTablename,nSize)", New Object() {sTablename, nSize})
    End Function

    '**%Objective:
    '**%Parámetros:
    '**%      sRootElement    -
    '**%      sTemplate       -
    '**%      sPathFilename -
    '%Objetivo: Permite transformar un "recordset" a texto según una plantilla
    '%Parámetros:
    '%      sRootElement    - Define el nombre del elemento Raiz para un template del tipo XML
    '%      sTemplate       - Plantilla a ser usada para la transformación
    '%      sPathFilename - Nombre del archivo de salida
    Public Function ProcTemplate(ByRef sRootElement As String, ByRef sTemplate As String, Optional ByRef sPathFilename As String = "", Optional ByRef sFilename As String = "", Optional ByVal bGenerateFile As Boolean = False, Optional ByVal sCodispl As String = "") As String
        Dim lclsGetsettings As eRemoteDB.VisualTimeConfig
        Dim lclsAspSupport As ASPSupport
        Dim lclsQuery As Query
        Dim lstrPathTemplate As String = String.Empty
        Dim strStream As String
        Dim lngFieldCount As Integer
        Dim lngIndex As Integer
        Dim strValue As String = String.Empty
        Dim lintModules As Short
        Dim strRow As String

        If Not IsIDEMode() Then
            'On Error GoTo ErrorHandler
        End If

        ProcTemplate = String.Empty
        lclsAspSupport = New ASPSupport
        lclsGetsettings = New eRemoteDB.VisualTimeConfig


        If bGenerateFile Then
            If sFilename = String.Empty Then
                sFilename = Today.ToString("MMddyyyy") & Now.ToString("HHMMSS") & sCodispl.Trim() & lclsAspSupport.SessionID & ".xml"
            End If

            If sPathFilename = String.Empty Then
                sPathFilename = lclsGetsettings.LoadSetting("CDoc_Path", "/CDoc_Path/", "Paths") & "\" & sFilename
            End If
        End If

        If sTemplate = String.Empty Then
            lclsQuery = New eRemoteDB.Query

            With lclsQuery
                If .OpenQuery("windows", "nModules", "sCodispl='" & sCodispl & "'") Then
                    lintModules = .FieldToClass("nModules")
                    .CloseQuery()
                    If .OpenQuery("tab_sys_exe", , "nExe_code=" & lintModules) Then
                        lstrPathTemplate = lclsGetsettings.LoadSetting("VirtualDirectory", "/VirtualDirectory/", "Paths") & "/" & .FieldToClass("sFolderName") & "/" & .FieldToClass("sExe_name") & lclsGetsettings.LoadSetting("XMLTemplate", "/XMLTemplate/", "Paths") & Trim(sCodispl) & ".txt"

                        lstrPathTemplate = Replace(lstrPathTemplate, "\", "/")
                        .CloseQuery()
                    End If
                Else
                    lstrPathTemplate = String.Empty
                End If

                If lstrPathTemplate <> String.Empty Then
                    sTemplate = eRemoteDB.FileSupport.LoadFileToText(lstrPathTemplate)
                    sTemplate = Mid(sTemplate, 2, Len(sTemplate) - 2)
                End If
            End With
            lclsQuery = Nothing
        End If

        lclsAspSupport = Nothing
        lclsGetsettings = Nothing

        strStream = String.Empty
        lngFieldCount = mrecRecord.FieldCount - 1
        Do While mrecRecord.Read
            strRow = sTemplate
            For lngIndex = 0 To lngFieldCount
                If sRootElement.Trim.Length > 0 Then
                    Select Case DataReaderTypeToRemoteDB(mrecRecord.GetDataTypeName(lngIndex))
                        Case Parameter.eRmtDataType.rdbSmallInt
                            strValue = BuildXMLElement(String.Empty, mrecRecord.GetValue(lngIndex), XMLSupport.eXMLGetValueType.exvInteger, , True, False)
                        Case Parameter.eRmtDataType.rdbInteger
                            strValue = BuildXMLElement(String.Empty, mrecRecord.GetValue(lngIndex), XMLSupport.eXMLGetValueType.exvLong, , True, False)
                        Case Parameter.eRmtDataType.rdbNumeric
                            strValue = BuildXMLElement(String.Empty, mrecRecord.GetValue(lngIndex), XMLSupport.eXMLGetValueType.exvDecimal, , True, False)
                        Case Parameter.eRmtDataType.rdbDBTimeStamp
                            strValue = BuildXMLElement(String.Empty, mrecRecord.GetValue(lngIndex), XMLSupport.eXMLGetValueType.exvDate, , True, False)
                        Case Else
                            strValue = BuildXMLElement(String.Empty, mrecRecord.GetValue(lngIndex), XMLSupport.eXMLGetValueType.exvString, , True, False)
                    End Select
                Else
                    strValue = mrecRecord.GetValue(lngIndex)
                End If
                strRow = Replace(strRow, "##" & mrecRecord.GetName(lngIndex) & "##", Trim(String.Empty & strValue))
            Next lngIndex
            strStream = strStream & strRow
        Loop
        RCloseRec()

        If sRootElement.Length > 0 Then
            strStream = "<?xml version='1.0' encoding='ISO-8859-1'?><" & sRootElement & ">" & strStream & "</" & sRootElement & ">"
        End If

        If sPathFilename.Length > 0 Then
            eRemoteDB.FileSupport.SaveBufferToFile(sPathFilename, strStream, False, False)
        End If

        ProcTemplate = strStream
        Exit Function
ErrorHandler:
        ProcError("Execute.ProcTemplate(sRootElement,sTemplate,sPathFilename)", New Object() {sRootElement, sTemplate, sPathFilename})
    End Function

    Public Function Login() As Object
        Login = Nothing
    End Function

    Public Function GetTablePrimaryKey() As String
        Dim lstrField As String = String.Empty

        GetTablePrimaryKey = String.Empty
        With mrecRecord
            For lintIndex As Integer = 0 To .FieldCount - 1
                lstrField = .GetName(lintIndex).ToLower
                If "sdescript|sshort_des|dcompdate|nusercode|sstatregt".IndexOf(lstrField) < 0 Then
                    GetTablePrimaryKey = .GetName(lintIndex)
                    Exit For
                End If
            Next lintIndex
        End With
    End Function

    Public Function FieldDatatype(ByVal FieldName As String) As String
        Dim intFieldIndex As Integer = mrecRecord.GetOrdinal(FieldName)
        FieldDatatype = mrecRecord.GetDataTypeName(intFieldIndex)
    End Function

    Public Function FieldPrecision(ByVal FieldName As String) As Integer
        Dim nRV As Object
        nRV = Nothing
        Dim Schema As DataTable = mrecRecord.GetSchemaTable()
        Dim row As DataRow
        For Each row In Schema.Rows
            If row("ColumnName").ToString().ToUpper() = FieldName.ToUpper() Then
                nRV = row("ColumnSize")
                Exit For
            End If
        Next
        FieldPrecision = nRV
    End Function

    Public Function FieldMaxsize(ByVal FieldName As String) As Object
        Dim valor As Object
        valor = Nothing
        Dim Schema As DataTable = mrecRecord.GetSchemaTable()
        Dim row As DataRow
        For Each row In Schema.Rows
            If row("ColumnName").ToString().ToUpper() = FieldName.ToUpper() Then
                valor = row("ColumnSize")
                Exit For
            End If
        Next
        FieldMaxsize = valor
        'FieldMaxsize = mrecRecord.GetSchemaTable.Columns(FieldName).MaxLength
    End Function

    Public Function RecordCount() As Object
        RecordCount = Nothing
    End Function

#End Region

#End Region

#Region "Methods"

#Region "Private Methods"

    '**%Objective:
    '%Objetivo:
    Private Function LoadParameters(ByVal IsReturnRecordSet As Boolean) As String
        'Dim parAdo As SqlClient.SqlParameter
        Dim parAdo As Object
        Dim parRmt As eRemoteDB.Parameter
        Dim sRV As String = String.Empty

        If Not IsIDEMode() Then
            'On Error GoTo ErrorHandler
        End If

        If Not mcolParameters Is Nothing Then
            If mcolParameters.Count > 0 Then
                For Each parRmt In mcolParameters

                    If Not mblnRefreshParameters Then
                        If parRmt.Direction = Parameter.eRmtDataDir.rdbParamOutput Or _
                           parRmt.Direction = Parameter.eRmtDataDir.rdbParamInputOutput Or _
                           parRmt.Direction = Parameter.eRmtDataDir.rdbParamReturnValue Then
                            mblnRefreshParameters = True
                        End If
                    End If

                    parAdo = mclsConnection.Provider.CreateParameter()

                    With parAdo
                        Select Case parRmt.ParType
                            Case Parameter.eRmtDataType.rdbBoolean
                                .DbType = DbType.Boolean
                            Case Parameter.eRmtDataType.rdbChar
                                .DbType = DbType.AnsiString
                            Case Parameter.eRmtDataType.rdbDate, Parameter.eRmtDataType.rdbDBTime, Parameter.eRmtDataType.rdbDBTimeStamp
                                .DbType = DbType.DateTime
                            Case Parameter.eRmtDataType.rdbNumeric, Parameter.eRmtDataType.rdbDecimal
                                .DbType = DbType.Decimal
                                If parRmt.Precision = 0 Then
                                    parRmt.Precision = parRmt.Size
                                End If
                            Case Parameter.eRmtDataType.rdbDouble
                                .DbType = DbType.Decimal
                                If parRmt.Precision = 0 Then
                                    parRmt.Precision = parRmt.Size
                                End If
                            Case Parameter.eRmtDataType.rdbImage
                                .DbType = DbType.Binary
                            Case Parameter.eRmtDataType.rdbInteger
                                .DbType = DbType.Int32
                            Case Parameter.eRmtDataType.rdbSmallInt
                                .DbType = DbType.Int16
                            Case Parameter.eRmtDataType.rdbVarchar
                                .DbType = DbType.String
                            Case Parameter.eRmtDataType.rdbCharFixedLength
                                .DbType = DbType.StringFixedLength
                            Case Else
                                .DbType = DbType.String
                        End Select
                    End With
                    With parAdo

                        .ParameterName = parRmt.Name
                        .Direction = parRmt.Direction
                        .Precision = parRmt.Precision
                        .Scale = parRmt.NumericScale
                        .Size = parRmt.Size
                        .Value = ClassToField(parRmt.Value, parRmt.ParType)
                        .IsNullable = (parRmt.Attributes = eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                        .SourceColumnNullMapping = True
                        If sRV <> String.Empty Then
                            sRV &= ","
                        End If
                        sRV &= ":" & .ParameterName
                    End With
                    mvarcmd.Parameters.Add(parAdo)

                Next parRmt
            End If
        End If

        If mTypeServer = eRemoteDB.Connection.sTypeServer.sOracle And _
                         IsReturnRecordSet Then
            If sRV <> String.Empty Then
                sRV &= ","
            End If
            sRV &= ":RC1"
            If mclsConnection.ProviderName = "oracle.dataaccess.client" Then
                mvarcmd.Parameters.Add(CreateRefCursorParameter(mvarcmd.CreateParameter()))
            Else
                parAdo = New OracleClient.OracleParameter("RC1", OracleClient.OracleType.Cursor)
                parAdo.Direction = ParameterDirection.Output

                mvarcmd.Parameters.Add(parAdo)
            End If
        End If

        parRmt = Nothing
        parAdo = Nothing

        Return sRV
ErrorHandler:
        ProcError("Execute.LoadParameters()", , , , "StoredProcedure: " & Trim(mvarStoredProcedure) & Trim(SQL) & vbCrLf & "     Parameter Name: " & parRmt.Name & vbCrLf & "                     Type: " & CStr(parRmt.ParType) & " Direction: " & CStr(parRmt.Direction) & " Size: " & CStr(parRmt.Size) & " Value: '" & CStr(parRmt.Value) & "' (Len " & Len(CStr(parRmt.Value)) & ")" & vbCrLf & "                     Scale: " & CStr(parRmt.NumericScale) & " Precision: " & CStr(parRmt.Precision) & " Attributes: " & CStr(parRmt.Attributes))

    End Function

    Private Shared Function CreateRefCursorParameter(ByVal parameterInstance As DbParameter) As DbParameter
        Dim parameterType As Type = parameterInstance.GetType
        Dim oracleDbType As Type = parameterType.Assembly.GetType("Oracle.DataAccess.Client.OracleDbType")
        Dim refCursorParameter As Object = Activator.CreateInstance(parameterType, New Object() {"RC1", [Enum].Parse(oracleDbType, "RefCursor")})
        refCursorParameter.Direction = ParameterDirection.Output

        Return refCursorParameter
    End Function

    '**%Objective:
    '%Objetivo:
    Private Sub RefreshParameters()
        'Dim parAdo As SqlClient.SqlParameter
        Dim parAdo As DbParameter
        Dim parRmt As eRemoteDB.Parameter

        If Not IsIDEMode() Then
            'On Error GoTo ErrorHandler
        End If

        If mvarStoredProcedure <> String.Empty And Not Special Then
            If Not (mcolParameters Is Nothing) Then
                If mcolParameters.Count > 0 Then

                    For Each parAdo In mvarcmd.Parameters
                        If (parAdo.Direction = ParameterDirection.Output Or _
                            parAdo.Direction = ParameterDirection.InputOutput) And _
                            parAdo.DbType <> DbType.Object Then
                            parRmt = mcolParameters(parAdo.ParameterName)
                            parRmt.Value = RmtFieldToClass(parAdo.ParameterName, CInt(parRmt.ParType), parAdo.Value)
                        ElseIf parAdo.Direction = ParameterDirection.ReturnValue Then
                            ReturnValue = parAdo.Value
                        End If
                    Next parAdo
                End If
            End If
        End If

        parAdo = Nothing
        parRmt = Nothing

        Exit Sub
ErrorHandler:
        ProcError("Execute.RefreshParameters()")
    End Sub

#End Region

#Region "Public Methods"

    '**%Objective:
    '%Objetivo:
    Public Sub RNext()
        If Not IsIDEMode() Then
            'On Error GoTo ErrorHandler
        End If

        If Not IsNothing(mrecRecord) Then
            mblnEOF = Not mrecRecord.Read()
        End If

        Exit Sub
ErrorHandler:
        ProcError("Execute.RNext()")
    End Sub

    '**%Objective: Sub routine to close a recordset or table
    '%Objetivo:
    Public Sub RCloseRec()
        If Not IsIDEMode() Then
            'On Error GoTo ErrorHandler
        End If

        Try
            If Not IsNothing(mrecRecord) Then
                If Not mrecRecord.IsClosed Then
                    mrecRecord.Close()
                End If
                mrecRecord = Nothing
            End If

            If mblnRefreshParameters Then
                RefreshParameters()
                mvarcmd.Dispose()
            End If

            If Not mblnPersistance Then
                If Not IsNothing(mclsConnection) Then
                    mclsConnection.CloseConnection()
                    mclsConnection = Nothing
                End If
            End If
            menuState = eStateOO.rooClosed
        Catch ex As Exception
            If Not IsNothing(mstrSQL) Then
                If mstrSQL.Length > 0 Then
                    NetHelper.WriteToEventLog("VisualTIME.Net: Error SQL: " + mstrSQL + " - " + ex.Message)
                End If
            End If
            If Not IsNothing(mvarStoredProcedure) Then
                If mvarStoredProcedure.Length > 0 Then
                    NetHelper.WriteToEventLog("VisualTIME.Net: Error SP: " + mvarStoredProcedure + " - " + ex.Message)
                End If
            End If
        End Try
        Exit Sub
ErrorHandler:
        ProcError("Execute.RCloseRec()")
    End Sub

    '**%Objective:
    '**%Parameters:
    '**%    sCommand          -
    '**%    cmdSQL            -
    '**%    IsReturnRecordSet -
    '**%    bOracle           -
    '%Objetivo:
    '%Parámetros:
    '%      sCommand          -
    '%      cmdSQL            -
    '%      IsReturnRecordSet -
    '%      bOracle           -
    Public Sub BluidEntryLog(ByVal sCommand As String, ByRef cmdSQL As DbCommand, ByVal IsReturnRecordSet As Boolean, Optional ByVal bOracle As Boolean = False)
        'Public Sub BluidEntryLog(ByVal sCommand As String, ByRef cmdSQL As SqlClient.SqlCommand, ByVal IsReturnRecordSet As Boolean, Optional ByVal bOracle As Boolean = False)
        Dim lclsConfig As eRemoteDB.VisualTimeConfig
        'Dim oParm As SqlClient.SqlParameter
        Dim oParm As DbParameter
        Dim strDefParam As String = String.Empty
        Dim strRem As String = String.Empty
        Dim lstsDecParam As String = String.Empty
        Dim lstrBuffer As String = String.Empty

        If Not IsIDEMode() Then
            'On Error GoTo ErrorHandler
        End If

        lclsConfig = New eRemoteDB.VisualTimeConfig

        strDefParam = String.Empty


        If cmdSQL.CommandType = CommandType.Text Then
            lstrBuffer = sCommand
        Else
            If bOracle Then
                If Not cmdSQL.Parameters Is Nothing Then
                    strDefParam = "DECLARE" & vbCrLf
                    If IsReturnRecordSet Then
                        strDefParam = strDefParam & "    TYPE RCT1 IS REF CURSOR;" & vbCrLf
                    End If
                    For Each oParm In cmdSQL.Parameters
                        Select Case oParm.DbType
                            Case Parameter.eRmtDataType.rdbVarchar, Parameter.eRmtDataType.rdbChar
                                If IsDBNull(oParm.Value) Then
                                    If oParm.Size > 0 Then
                                        strDefParam = strDefParam & "    " & oParm.ParameterName & " VARCHAR2(" & oParm.Size & ") := NULL;" & vbCrLf '& " /*(" & oParm.Size & ")*/" & vbCrLf
                                    Else
                                        strDefParam = strDefParam & "    " & oParm.ParameterName & " VARCHAR2(2000) := NULL;" & vbCrLf
                                    End If
                                Else
                                    If oParm.Size > 0 Then
                                        strDefParam = strDefParam & "    " & oParm.ParameterName & " VARCHAR2(" & oParm.Size & ") := " & "'" & IIf(IsDBNull(oParm.Value), "NULL", oParm.Value) & "';" & vbCrLf '& " /*(" & oParm.Size & ")*/" & vbCrLf
                                    Else
                                        strDefParam = strDefParam & "    " & oParm.ParameterName & " VARCHAR2(2000) := " & "'" & IIf(IsDBNull(oParm.Value), "NULL", oParm.Value) & "';" & vbCrLf '& " /*(" & oParm.Size & ")*/" & vbCrLf
                                    End If
                                End If
                            Case Parameter.eRmtDataType.rdbDBTimeStamp, Parameter.eRmtDataType.rdbDate
                                If IsDBNull(oParm.Value) Then
                                    strDefParam = strDefParam & "    " & oParm.ParameterName & " DATE := NULL;" & vbCrLf '& " /*(" & oParm.Size & ")*/" & vbCrLf
                                Else
                                    strDefParam = strDefParam & "    " & oParm.ParameterName & " DATE := TO_DATE('" & oParm.Value & "', 'yyyy/MM/dd');" & vbCrLf '& " /*(" & oParm.Size & ")*/" & vbCrLf
                                End If
                            Case Else
                                strDefParam = strDefParam & "    " & oParm.ParameterName & " NUMBER := " & IIf(IsDBNull(oParm.Value), "NULL", oParm.Value) & ";" & vbCrLf '& " /*(" & oParm.Precision & "," & oParm.NumericScale & ")*/" & vbCrLf
                        End Select
                        lstsDecParam = lstsDecParam & ", '" & oParm.Value & "'"
                    Next oParm
                End If

                lstsDecParam = "(" & Mid(lstsDecParam, 3, Len(lstsDecParam)) & ")"

                lstrBuffer = lstrBuffer & strRem

                If strDefParam > String.Empty Then
                    lstrBuffer = lstrBuffer & strDefParam
                End If
                lstrBuffer = lstrBuffer & "BEGIN" & vbCrLf & vbTab & sCommand & lstsDecParam & ";" & vbCrLf & "END;"
            Else
                If Not cmdSQL.Parameters Is Nothing Then
                    strDefParam = String.Empty
                    For Each oParm In cmdSQL.Parameters
                        Select Case oParm.DbType
                            Case Parameter.eRmtDataType.rdbVarchar, Parameter.eRmtDataType.rdbChar

                                If IsDBNull(oParm.Value) Then

                                    If oParm.Size > 0 Then
                                        strDefParam = strDefParam & " DECLARE @" & oParm.ParameterName & " VARCHAR2(" & oParm.Size & ") := NULL;" & vbCrLf '& " /*(" & oParm.Size & ")*/" & vbCrLf
                                    Else
                                        strDefParam = strDefParam & " DECLARE @" & oParm.ParameterName & " VARCHAR2(2000) := NULL;" & vbCrLf
                                    End If
                                Else
                                    If oParm.Size > 0 Then
                                        strDefParam = strDefParam & " DECLARE @" & oParm.ParameterName & " VARCHAR2(" & oParm.Size & ") := " & "'" & IIf(IsDBNull(oParm.Value), "NULL", oParm.Value) & "';" & vbCrLf '& " /*(" & oParm.Size & ")*/" & vbCrLf
                                    Else
                                        strDefParam = strDefParam & " DECLARE @" & oParm.ParameterName & " VARCHAR2(2000) := " & "'" & IIf(IsDBNull(oParm.Value), "NULL", oParm.Value) & "';" & vbCrLf '& " /*(" & oParm.Size & ")*/" & vbCrLf
                                    End If
                                End If
                            Case Parameter.eRmtDataType.rdbDBTimeStamp, Parameter.eRmtDataType.rdbDate
                                If IsDBNull(oParm.Value) Then
                                    strDefParam = strDefParam & " DECLARE @" & oParm.ParameterName & " DATE := NULL;" & vbCrLf '& " /*(" & oParm.Size & ")*/" & vbCrLf
                                Else
                                    strDefParam = strDefParam & " DECLARE @" & oParm.ParameterName & " DATE := TO_DATE('" & oParm.Value & "', 'yyyy/MM/dd');" & vbCrLf '& " /*(" & oParm.Size & ")*/" & vbCrLf
                                End If
                            Case Else
                                strDefParam = strDefParam & " DECLARE @" & oParm.ParameterName & " NUMBER := " & IIf(IsDBNull(oParm.Value), "NULL", oParm.Value) & ";" & vbCrLf '& " /*(" & oParm.Precision & "," & oParm.NumericScale & ")*/" & vbCrLf
                        End Select
                        lstsDecParam = lstsDecParam & ", '" & oParm.Value & "'"
                    Next oParm
                End If

                lstsDecParam = "(" & Mid(lstsDecParam, 3, Len(lstsDecParam)) & ")"

                lstrBuffer = lstrBuffer & strRem

                If strDefParam > String.Empty Then
                    lstrBuffer = lstrBuffer & strDefParam
                End If
                lstrBuffer = lstrBuffer & "EXEC " & sCommand & lstsDecParam & vbCrLf
            End If
        End If

#If VTDEBUG Then

		DebugLog "SPCall", _
		                 lstrBuffer
#End If

        Exit Sub
ErrorHandler:
        ProcError("Execute.BluidEntryLog(sCommand,cmdSQL,IsReturnRecordSet,bOracle)", New Object() {sCommand, cmdSQL, IsReturnRecordSet, bOracle})
    End Sub

    '**%Objective: Sub routine to reinitialize the type of database server
    '%Objetivo:
    Public Sub ReQuery()
        If Not IsIDEMode() Then
            'On Error GoTo ErrorHandler
        End If

        'NS mrecRecord.ReQuery()

        Exit Sub
ErrorHandler:
        ProcError("Execute.ReQuery()")
    End Sub

    Public Shared Widening Operator CType(v As Execute) As String
        Throw New NotImplementedException()
    End Operator

#End Region

#End Region

End Class
