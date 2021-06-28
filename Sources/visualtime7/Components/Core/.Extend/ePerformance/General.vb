Option Strict Off
Option Explicit On
Public Class General

    Const F_MACRO As Short = 0
    Const F_FIELD As Short = 1
    Const F_LENGTH As Short = 2
    Const F_JUST As Short = 3
    Const F_FILL As Short = 4

    Private mMacroDef As String
    Private mstrMacro As String
    Private mintLength As Short
    Private mstrJust As String
    Private mstrFill As String

    Public Field As String
    Public Name As String
    Public Shared Value As Object
    Public IsField As Boolean
    Public IsParameter As Boolean

    Public Shared oExpandMacros As Macros
    Public gintStep As Integer

    Public Shared gstrMacroDef As String
    Public gstrScriptFile As String
    Public Shared gblnMuteMode As Boolean
    Public glngClient As Integer
    Public gblnError As Boolean

    Public gdblMax As Double
    Public gdblMaxOld As Double

    Public mclsInstructions As Instructions
    Public Shared mclsInstruction As Instruction

    Structure errType
        Dim nError As Integer
        Dim sDescription As String
    End Structure
    Public Shared mTypeError As errType
    Public Function DiffTime(ByRef sTimeIni As String, ByRef sTimeEnd As String) As Single
        DiffTime = ((CShort(Left(sTimeEnd, 2)) * 3600.0!) + (CShort(Mid(sTimeEnd, 4, 2)) * 60) + Val(Right(sTimeEnd, 7))) - ((CShort(Left(sTimeIni, 2)) * 3600.0!) + (CShort(Mid(sTimeIni, 4, 2)) * 60) + Val(Right(sTimeIni, 7)))
    End Function


    Public WriteOnly Property MacroDef() As String
        Set(ByVal Value As String)
            Dim intIndex As Short
            Dim intField As Short

            mMacroDef = Value

            Value = Mid(Value, 2, Len(Value) - 2)

            intIndex = InStr(Value, ",")
            intField = F_MACRO
            Do While intIndex > 0
                Select Case intField
                    Case F_MACRO
                        mstrMacro = Mid(Value, 1, intIndex - 1)
                        Name = "%" & UCase(mstrMacro) & "%"
                        intField = F_FIELD

                    Case F_FIELD
                        Field = Mid(Value, 1, intIndex - 1)
                        IsField = ValField(Field)
                        If IsField Then
                            If ValParameter(Field) Then
                                IsParameter = True
                                Field = Mid(Field, 2)
                                IsField = False
                            Else
                                IsParameter = False
                            End If
                        End If
                        intField = F_LENGTH

                    Case F_LENGTH
                        If intIndex > 1 Then mintLength = CShort(Mid(Value, 1, intIndex - 1))
                        intField = F_JUST


                    Case F_JUST
                        If intIndex > 1 Then mstrJust = Mid(Value, 1, intIndex - 1)
                        intField = F_FILL

                        If intIndex > 1 Then mstrFill = Mid(Value, intIndex + 1)
                        Exit Do

                End Select
                Value = Mid(Value, intIndex + 1)
                intIndex = InStr(Value, ",")
            Loop
        End Set
    End Property

    Public WriteOnly Property PutValue() As Object
        Set(ByVal Value As Object)
            Dim strValue As String = ""

            If Asc(mstrFill) <> 0 And mstrFill <> String.Empty Then
                Select Case mstrJust
                    Case "L"
                        strValue = Left(CStr(Value) & New String(mstrFill, mintLength), mintLength)
                    Case "R"
                        strValue = Right(New String(mstrFill, mintLength) & CStr(Value), mintLength)
                End Select
                Me.Value = strValue
            Else
                Me.Value = CStr(Value)
            End If
            'Add_Log "Usuario " & CStr(glngClient) & ":" & Name & " = " & Value & vbCrLf
        End Set
    End Property


    Public Sub ReCalValue()
        Select Case UCase(Field)
            Case "VALINPUT"
                Value = Value + 1
        End Select
    End Sub


    Private Function ValField(ByRef sField As String) As Boolean
        ValField = False
        Dim intIndex As Short
        Dim intLastIndex As Short
        Select Case UCase(sField)
            Case "VALINPUT"
                If gblnMuteMode Then
                    intIndex = InStr(gstrMacroDef, Name)
                    If intIndex > 0 Then
                        intIndex = Len(Name) + 1
                        intLastIndex = InStr(intIndex, gstrMacroDef, ":")
                        Value = CInt(Mid(gstrMacroDef, intIndex, intLastIndex - intIndex))
                    End If
                Else
                    Value = CInt(InputBox("Ingrese el valor inicial", "Valor inicial"))
                End If

            Case Else
                ValField = True
        End Select
    End Function

    Private Function ValParameter(ByRef sField As String) As Boolean
        ValParameter = (Left(sField, 1) = "&")
    End Function
End Class






