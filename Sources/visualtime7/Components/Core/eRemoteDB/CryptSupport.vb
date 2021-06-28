Option Strict Off
Option Explicit On

Public Class CryptSupport
    '**+Objective:
    '**+Version: $$Revision: $
    '+Objetivo:
    '+Version: $$Revision: $

    '**%Objective:
    '**%Parameters:
    '**%    sText    -
    '**%    Password -
    '%Objetivo:
    '%Parámetros:
    '%      sText    -
    '%      Password -
    Public Shared Function EncryptString(ByVal sText As String, Optional ByVal Password As String = "") As String
        ''On Error GoTo ErrorHandler

        EncryptString = HexEncryptString(sText)

        Exit Function
ErrorHandler:
        ProcError("CryptSupport.EncryptString(sText)", New Object() {sText})
    End Function

    '**%Objective:
    '**%Parameters:
    '**%    sText    -
    '**%    Password -
    '%Objetivo:
    '%Parámetros:
    '%      sText    -
    '%      Password -
    Public Shared Function DecryptString(ByVal sText As String, Optional ByVal Password As String = "") As String
        ''On Error GoTo ErrorHandler

        DecryptString = HexDecryptString(sText)

        Exit Function
ErrorHandler:
        ProcError("CryptSupport.DecryptString(sText)", New Object() {sText})
    End Function

    '**%Objective: Password encryption routine
    '**%Parameters:
    '**%    s -
    '%Objetivo: Rutina de encriptamiento de password
    '%Parámetros:
    '%      s -
    Public Shared Function ASCIIEncryptString(ByVal strS As String) As String
        Dim rnd As New Random
        Dim Key As Integer
        Dim salt As Boolean
        Dim n As Integer
        Dim lngI As Integer
        Dim ss As String
        Dim k1 As Integer
        Dim k2 As Integer
        Dim k3 As Integer
        Dim k4 As Integer
        Dim t As Integer

        ASCIIEncryptString = String.Empty

        Static saltvalue As String

        If Trim(strS) <> String.Empty Then


            Key = 1234567890
            salt = False

            If salt Then
                For lngI = 1 To 4
                    t = 100 * (1 + Asc(Mid(saltvalue, lngI, 1))) * rnd.Next(1, Integer.MaxValue)
                    Mid(saltvalue, lngI, 1) = Chr(t Mod 256)
                Next
                strS = Mid(saltvalue, 1, 2) & strS & Mid(saltvalue, 3, 2)
            End If

            n = Len(strS)
            ss = Space(n)
            Dim sn(n) As Integer

            k1 = 11 + (Key Mod 233)
            k2 = 7 + (Key Mod 239)
            k3 = 5 + (Key Mod 241)
            k4 = 3 + (Key Mod 251)

            For lngI = 1 To n
                sn(lngI) = Asc(Mid(strS, lngI, 1))
            Next lngI
            For lngI = 2 To n
                sn(lngI) = sn(lngI) Xor sn(lngI - 1) Xor ((k1 * sn(lngI - 1)) Mod 256)
            Next lngI
            For lngI = n - 1 To 1 Step -1
                sn(lngI) = sn(lngI) Xor sn(lngI + 1) Xor (k2 * sn(lngI + 1)) Mod 256
            Next lngI
            For lngI = 3 To n
                sn(lngI) = sn(lngI) Xor sn(lngI - 2) Xor (k3 * sn(lngI - 1)) Mod 256
            Next lngI
            For lngI = n - 2 To 1 Step -1
                sn(lngI) = sn(lngI) Xor sn(lngI + 2) Xor (k4 * sn(lngI + 1)) Mod 256
            Next lngI
            For lngI = 1 To n
                Mid(ss, lngI, 1) = Chr(sn(lngI))
            Next lngI
            ASCIIEncryptString = ss
        End If
    End Function

    '**%Objective: Password de-encryptment routine
    '**%Parameters:
    '**%    s -
    '%Objetivo: Rutina de des-encriptamiento de password
    '%Parámetros:
    '%      s -
    Public Shared Function ASCIIDecryptString(ByVal strS As String) As String
        Dim Key As Integer
        Dim salt As Boolean
        Dim n As Integer
        Dim lngI As Integer
        Dim ss As String
        Dim k1 As Integer
        Dim k2 As Integer
        Dim k3 As Integer
        Dim k4 As Integer

        ASCIIDecryptString = String.Empty
        If Trim(strS) <> String.Empty Then

            Key = 1234567890
            salt = False

            n = Len(strS)
            ss = Space(n)
            Dim sn(n) As Integer

            k1 = 11 + (Key Mod 233)
            k2 = 7 + (Key Mod 239)
            k3 = 5 + (Key Mod 241)
            k4 = 3 + (Key Mod 251)

            For lngI = 1 To n
                sn(lngI) = Asc(Mid(strS, lngI, 1))
            Next lngI

            For lngI = 1 To n - 2
                sn(lngI) = sn(lngI) Xor sn(lngI + 2) Xor (k4 * sn(lngI + 1)) Mod 256
            Next lngI
            For lngI = n To 3 Step -1
                sn(lngI) = sn(lngI) Xor sn(lngI - 2) Xor (k3 * sn(lngI - 1)) Mod 256
            Next lngI
            For lngI = 1 To n - 1
                sn(lngI) = sn(lngI) Xor sn(lngI + 1) Xor (k2 * sn(lngI + 1)) Mod 256
            Next lngI
            For lngI = n To 2 Step -1
                sn(lngI) = sn(lngI) Xor sn(lngI - 1) Xor (k1 * sn(lngI - 1)) Mod 256
            Next lngI

            For lngI = 1 To n
                Mid(ss, lngI, 1) = Chr(sn(lngI))
            Next lngI

            If salt Then
                ASCIIDecryptString = Mid(ss, 3, Len(ss) - 4)
            Else
                ASCIIDecryptString = ss
            End If
        End If
    End Function

    '%Objetivo: .
    '%Parámetros:
    '%    Text     - .
    '%    Password - .
    Public Shared Function HexEncryptString(ByRef Text As String) As String
        Dim strBuffer As String = String.Empty
        Dim strOutput As String
        Dim intIndex As Short
        Dim intCount As Short

        ''On Error GoTo ErrorHandler

        strBuffer = ASCIIEncryptString(Text)
        intCount = Len(strBuffer)
        strOutput = String.Empty
        For intIndex = 1 To intCount
            strOutput = strOutput & Right("00" & Hex(Asc(Mid(strBuffer, intIndex, 1))), 2)
        Next
        HexEncryptString = strOutput

        Exit Function
ErrorHandler:
        ProcError("CryptSupport.HexEncryptString(Text)", New Object() {Text})
    End Function

    '%Objetivo: .
    '%Parámetros:
    '%    Text     - .
    '%    Password - .
    Public Shared Function HexDecryptString(ByRef Text As String) As String
        Dim strOutput As String
        Dim intIndex As Short
        Dim intCount As Short

        ''On Error GoTo ErrorHandler

        intCount = Len(Text)
        strOutput = String.Empty
        For intIndex = 1 To intCount Step 2
            strOutput = strOutput & Chr(CInt(Hex2Int(Mid(Text, intIndex, 2))))
        Next
        HexDecryptString = ASCIIDecryptString(strOutput)

        Exit Function
ErrorHandler:
        ProcError("CryptSupport.HexDecryptString(Text)", New Object() {Text})
    End Function

    '%Objetivo: .
    '%Parámetros:
    '%    sHex - .
    Private Shared Function Hex2Int(ByVal sHex As String) As Short
        Dim Tmp As String
        Dim lo1 As Short
        Dim lo2 As Short
        Dim hi1 As Integer
        Dim hi2 As Integer

        Const Hx As String = "&H"
        Const BigShift As Integer = 65536
        Const LilShift As Short = 256
        Const Two As Short = 2

        ''On Error GoTo ErrorHandler

        Tmp = sHex
        If UCase(Left(sHex, 2)) = "&H" Then Tmp = Mid(sHex, 3)
        Tmp = Right("0000000" & Tmp, 8)
        If IsNumeric(Hx & Tmp) Then
            lo1 = CShort(Hx & Right(Tmp, Two))
            hi1 = CInt(Hx & Mid(Tmp, 5, Two))
            lo2 = CShort(Hx & Mid(Tmp, 3, Two))
            hi2 = CInt(Hx & Left(Tmp, Two))
            Hex2Int = CDec(hi2 * LilShift + lo2) * BigShift + (hi1 * LilShift) + lo1
        End If

        Exit Function
ErrorHandler:
        ProcError("CryptSupport.Hex2Int(sHex)", New Object() {sHex})
    End Function
End Class






