Option Strict Off
Option Explicit On
Public Class RegSetting
	
	Private Declare Function GetUserDefaultLCID Lib "kernel32" () As Integer
	Private Declare Function GetLocaleInfo Lib "kernel32"  Alias "GetLocaleInfoA"(ByVal Locale As Integer, ByVal LCType As Integer, ByVal lpLCData As String, ByVal cchData As Integer) As Integer
	Private Declare Function GetSystemDefaultLCID Lib "kernel32" () As Integer
	Private Declare Function SetLocaleInfo Lib "kernel32"  Alias "SetLocaleInfoA"(ByVal Locale As Integer, ByVal LCType As Integer, ByVal lpLCData As String) As Boolean
	Private Declare Function PostMessage Lib "user32"  Alias "PostMessageA"(ByVal hwnd As Integer, ByVal wMsg As Integer, ByVal wParam As Integer, ByVal lParam As Integer) As Integer
	Const LOCALE_SDECIMAL As Integer = &HE '  decimal separator
	Const LOCALE_STHOUSAND As Integer = &HF '  thousand separator
	Const LOCALE_SMONDECIMALSEP As Integer = &H16 '  monetary decimal separator
	Const LOCALE_SMONTHOUSANDSEP As Integer = &H17 '  monetary thousand separator
	Const WM_SETTINGCHANGE As Integer = &H1A
	Const HWND_BROADCAST As Integer = &HFFFF
	
	'-Variable que guarda el número de sesión
	Public sSessionID As String
	
	'-Código del usuario
	Public nUsercode As Integer
	
    Private Function GetLocateInfo(ByVal nInfo As Integer) As Object
        Dim lngLCID As Integer
        Dim lngRet1 As Integer
        Dim lngRet2 As Integer
        Dim strBuffer As String = ""

        lngLCID = GetUserDefaultLCID()
        lngRet1 = GetLocaleInfo(lngLCID, nInfo, strBuffer, 0)
        strBuffer = New String(Chr(0), lngRet1)
        lngRet2 = GetLocaleInfo(lngLCID, nInfo, strBuffer, lngRet1)
        lngRet1 = InStr(strBuffer, Chr(0))
        If lngRet1 > 0 Then
            strBuffer = Left(strBuffer, lngRet1 - 1)
        End If
        GetLocateInfo = strBuffer
    End Function
	
    Private Function SetLocateInfo(ByVal nInfo As Integer, ByVal sValue As String) As Boolean
        Dim lngLCID As Integer

        lngLCID = GetSystemDefaultLCID()
        If SetLocaleInfo(lngLCID, nInfo, sValue) Then
            SetLocateInfo = True
        End If
    End Function
	
	Private Sub BroadCastSettingChange()
		PostMessage(HWND_BROADCAST, WM_SETTINGCHANGE, 0, 0)
	End Sub
	
	Public Sub RegionalSettingValidate()
		Dim strDecimal As String
		Dim strThousand As String
		
		strDecimal = GetLocateInfo(LOCALE_SDECIMAL)
		
		If strDecimal <> "," Then
			SetLocateInfo(LOCALE_SDECIMAL, ",")
		End If
		
		strDecimal = GetLocateInfo(LOCALE_SMONDECIMALSEP)
		If strDecimal <> "," Then
			SetLocateInfo(LOCALE_SMONDECIMALSEP, ",")
		End If
		
		strThousand = GetLocateInfo(LOCALE_STHOUSAND)
		If strThousand <> "." Then
			SetLocateInfo(LOCALE_STHOUSAND, ".")
		End If
		
		strThousand = GetLocateInfo(LOCALE_SMONTHOUSANDSEP)
		If strThousand <> "." Then
			SetLocateInfo(LOCALE_SMONTHOUSANDSEP, ".")
		End If
		
		BroadCastSettingChange()
	End Sub
End Class






