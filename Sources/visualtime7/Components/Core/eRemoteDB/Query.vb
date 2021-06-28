Option Strict Off
Option Explicit On
Public Class Query
	'**+Objective: Class that supports the table Query
	'**+Version: $$Revision: $
	'+Objetivo: Clase que le da soporte a la tabla Query
	'+Version: $$Revision: $

#Region "Atributes"

    '**-Objective:
    '-Objetivo:
    Private mstrKeyField As String

    '**-Objective:
    '-Objetivo:
    Private mblnError As Boolean

    '**-Objective:
    '-Objetivo:
    Private mstrErrorMessage As String

    '**-Objective:
    '-Objetivo:
    Private mrecQueryRmt As eRemoteDB.Execute

    '**-Objective:
    '-Objetivo:
    Private mvarOwner As String

#End Region

#Region "Methods"

    '**%Objective: Function that returns true or false for a valid query
    '**%Parameters:
    '**%    sTable     -
    '**%    sFields    -
    '**%    sCondition -
    '**%    sOrder     -
    '%Objetivo: Metodo que retorna verdadero o falso para un query valido
    '%Parámetros:
    '%      sTable     -
    '%      sFields    -
    '%      sCondition -
    '%      sOrder     -
    Public Function OpenQuery(ByVal sTable As String, Optional ByVal sFields As String = "*", Optional ByVal sCondition As String = "", Optional ByVal sOrder As String = "") As Boolean
        Dim strSQL As String = String.Empty
        Dim lstrTables As String
        Dim lstrCadena As String
        Dim lintPosition As Short
        Dim lintPositionA As Short
        Dim lintLong As Short

        If mrecQueryRmt Is Nothing Then
            mrecQueryRmt = New eRemoteDB.Execute
        End If

        lstrCadena = sTable
        lintLong = Len(lstrCadena)
        lintPosition = InStr(1, lstrCadena, ",")
        lintPositionA = 1
        lstrTables = String.Empty
        If lintPosition > 0 Then
            Do While lintPosition > 0
                lstrTables = lstrTables & Owner & LTrim(RTrim(Mid(lstrCadena, lintPositionA, lintPosition - lintPositionA + 1)))
                lintPositionA = lintPosition + 1
                lintPosition = InStr(lintPosition + 1, lstrCadena, ",")
                If lintPosition = 0 Then
                    lstrTables = lstrTables & Owner & LTrim(RTrim(Mid(lstrCadena, lintPositionA, Len(lstrCadena))))
                End If
            Loop
        Else
            If mrecQueryRmt.Server = Connection.sTypeServer.sOracle Then
                lstrTables = sTable
            Else
                lstrTables = Owner & sTable
            End If
        End If
        strSQL = "SELECT " & sFields & " FROM " & lstrTables

        If sCondition > String.Empty Then
            strSQL = strSQL & " WHERE " & sCondition
        End If
        If sOrder > String.Empty Then
            strSQL = strSQL & " ORDER BY " & sOrder
        End If
        mrecQueryRmt.SQL = strSQL
        If mrecQueryRmt.Run(True) Then
            OpenQuery = True
        Else
            OpenQuery = False
            mrecQueryRmt = Nothing
        End If

        Exit Function
ErrorHandler:
        ProcError("Query.OpenQuery(sTable,sFields,sCondition,sOrder)", New Object() {sTable, sFields, sCondition, sOrder})
    End Function

    '**%Objective: Function that gets specific a field from a table
    '**%Parameters:
    '**%    sField -
    '%Objetivo: Metodo que obtiene un campo specifico de una tabla
    '%Parámetros:
    '%      sField -
    Public Function FieldToClass(ByVal sField As String) As Object
        ''On Error GoTo ErrorHandler

        FieldToClass = mrecQueryRmt.FieldToClass(sField)

        Exit Function
ErrorHandler:
        ProcError("Query.FieldToClass(sField)", New Object() {sField})
    End Function

    '**%Objective: Function that returns true or false to indicate the End of a Query
    '%Objetivo: Metodo que retorna verdadero o falso para indicar el fin de una consulta
    Public Function EndQuery() As Boolean
        ''On Error GoTo ErrorHandler

        EndQuery = mrecQueryRmt.EOF

        Exit Function
ErrorHandler:
        ProcError("Query.EndQuery()")
    End Function

    '**%Objective: Sub Routine to navigate to the Next Record
    '%Objetivo: Rutina para navegar al proximo registro
    Public Sub NextRecord()
        ''On Error GoTo ErrorHandler

        mrecQueryRmt.RNext()

        Exit Sub
ErrorHandler:
        ProcError("Query.NextRecord()")
    End Sub

    '**%Objective: Sub Routine to close all open queries
    '%Objetivo: Rutina para cerrar todas consulta abiertas
    Public Sub CloseQuery()
        ''On Error GoTo ErrorHandler

        If Not (mrecQueryRmt Is Nothing) Then
            mrecQueryRmt.RCloseRec()
            mrecQueryRmt = Nothing
        End If
        Exit Sub
ErrorHandler:
        ProcError("Query.CloseQuery()")
    End Sub

    Protected Overrides Sub Finalize()
        If Not IsNothing(mrecQueryRmt) Then
            mrecQueryRmt.RCloseRec()
            mrecQueryRmt = Nothing
        End If
        MyBase.Finalize()
    End Sub

#End Region

#Region "Properties"

    '**%Objective:
    '**%Parameters:
    '**%    vData -
    '%Objetivo:
    '%Parámetros:
    '%      vData -

    '**%Objective:
    '%Objetivo:
    Public Property Owner() As String
        Get
            Dim lclsVisualTimeConfig As eRemoteDB.VisualTimeConfig

            ''On Error GoTo ErrorHandler

            If mvarOwner = String.Empty Then
                lclsVisualTimeConfig = New eRemoteDB.VisualTimeConfig
                mvarOwner = lclsVisualTimeConfig.LoadSetting("Owner", String.Empty, "database")
                lclsVisualTimeConfig = Nothing
            End If
            Owner = mvarOwner & "."

            Exit Property
ErrorHandler:
            ProcError("Query.Owner()")
        End Get
        Set(ByVal Value As String)
            ''On Error GoTo ErrorHandler

            mvarOwner = Value

            Exit Property
ErrorHandler:
            ProcError("Query.Owner(vData)", New Object() {Value})
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
    Public Property sKeyField() As String
        Get
            ''On Error GoTo ErrorHandler

            sKeyField = mstrKeyField

            Exit Property
ErrorHandler:
            ProcError("Query.sKeyField()")
        End Get
        Set(ByVal Value As String)
            ''On Error GoTo ErrorHandler

            mstrKeyField = Value

            Exit Property
ErrorHandler:
            ProcError("Query.sKeyField(vNewValue)", New Object() {Value})
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
    Public Property bError() As Boolean
        Get
            ''On Error GoTo ErrorHandler

            bError = mblnError

            Exit Property
ErrorHandler:
            ProcError("Query.bError()")
        End Get
        Set(ByVal Value As Boolean)
            ''On Error GoTo ErrorHandler

            mblnError = Value

            Exit Property
ErrorHandler:
            ProcError("Query.bError(vNewValue)", New Object() {Value})
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
    Public Property sErrorMessage() As String
        Get
            ''On Error GoTo ErrorHandler

            sErrorMessage = mstrErrorMessage

            Exit Property
ErrorHandler:
            ProcError("Query.sErrorMessage()")
        End Get
        Set(ByVal Value As String)
            ''On Error GoTo ErrorHandler

            mstrErrorMessage = Value

            Exit Property
ErrorHandler:
            ProcError("Query.sErrorMessage(vNewValue)", New Object() {Value})
        End Set
    End Property

#End Region

End Class






