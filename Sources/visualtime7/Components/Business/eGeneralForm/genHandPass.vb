Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Friend Class genHandPass
	
	'**% StrEncode: Password encryptment routine
	'%StrEncode: Rutina de encriptamiento de password
	Public Function StrEncode(ByVal s As String) As String
		Dim key As Integer
		Dim salt As Boolean
		Dim n As Integer
		Dim i As Integer
		Dim ss As String
		Dim k1 As Integer
		Dim k2 As Integer
		Dim k3 As Integer
		Dim k4 As Integer
        Dim t As Integer
        Dim varAux As String = ""

        Static saltvalue As String
        If Trim(s) <> String.Empty Then


            key = 1234567890
            salt = False

            If salt Then
                For i = 1 To 4
                    t = 100 * (1 + Asc(Mid(saltvalue, i, 1))) * Rnd() * (VB.Timer() + 1)
                    Mid(saltvalue, i, 1) = Chr(t Mod 256)
                Next
                s = Mid(saltvalue, 1, 2) & s & Mid(saltvalue, 3, 2)
            End If

            n = Len(s)
            ss = Space(n)
            Dim sn(n) As Integer

            k1 = 11 + (key Mod 233) : k2 = 7 + (key Mod 239)
            k3 = 5 + (key Mod 241) : k4 = 3 + (key Mod 251)

            For i = 1 To n : sn(i) = Asc(Mid(s, i, 1)) : Next

            For i = 2 To n : sn(i) = sn(i) Xor sn(i - 1) Xor ((k1 * sn(i - 1)) Mod 256) : Next
            For i = n - 1 To 1 Step -1 : sn(i) = sn(i) Xor sn(i + 1) Xor (k2 * sn(i + 1)) Mod 256 : Next
            For i = 3 To n : sn(i) = sn(i) Xor sn(i - 2) Xor (k3 * sn(i - 1)) Mod 256 : Next
            For i = n - 2 To 1 Step -1 : sn(i) = sn(i) Xor sn(i + 2) Xor (k4 * sn(i + 1)) Mod 256 : Next

            For i = 1 To n : Mid(ss, i, 1) = Chr(sn(i)) : Next

            varAux = ss

            'saltvalue = Mid(ss, Len(ss) / 2, 4)
        End If
        Return varAux
    End Function

    '**% StrDecode: Password des-encryptment routine
    '%StrDecode: Rutina de des-encriptamiento de password
    Public Function StrDecode(ByVal s As String) As String
        Dim key As Integer
        Dim salt As Boolean
        Dim n As Integer
        Dim i As Integer
        Dim ss As String
        Dim k1 As Integer
        Dim k2 As Integer
        Dim k3 As Integer
        Dim k4 As Integer
        Dim varAux As String = ""

        If Trim(s) <> String.Empty Then

            key = 1234567890
            salt = False

            n = Len(s)
            ss = Space(n)
            Dim sn(n) As Integer

            k1 = 11 + (key Mod 233) : k2 = 7 + (key Mod 239)
            k3 = 5 + (key Mod 241) : k4 = 3 + (key Mod 251)

            For i = 1 To n : sn(i) = Asc(Mid(s, i, 1)) : Next

            For i = 1 To n - 2 : sn(i) = sn(i) Xor sn(i + 2) Xor (k4 * sn(i + 1)) Mod 256 : Next
            For i = n To 3 Step -1 : sn(i) = sn(i) Xor sn(i - 2) Xor (k3 * sn(i - 1)) Mod 256 : Next
            For i = 1 To n - 1 : sn(i) = sn(i) Xor sn(i + 1) Xor (k2 * sn(i + 1)) Mod 256 : Next
            For i = n To 2 Step -1 : sn(i) = sn(i) Xor sn(i - 1) Xor (k1 * sn(i - 1)) Mod 256 : Next

            For i = 1 To n : Mid(ss, i, 1) = Chr(sn(i)) : Next i

            If salt Then varAux = Mid(ss, 3, Len(ss) - 4) Else varAux = ss
        End If
        Return varAux
    End Function
End Class






