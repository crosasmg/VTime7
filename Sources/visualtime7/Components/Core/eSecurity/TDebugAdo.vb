Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Module TDebugAdo
	'**+Objective: Class that supports the TDebugAdo
	'**+Version: $$Revision: 3 $
	'+Objetivo: Clase que le da soporte a TDebugAdo
	'+Version: $$Revision: 3 $
	
	'**-Objective:
	'-Objetivo:
	Public strFileName As String
	
	'**-Objective:
	'-Objetivo:
	Public gblnPerformanceDebug As Boolean
	
	'**%Objective:
	'**%Parameters:
	'**%    sCommand -
	'%Objetivo:
	'%Parámetros:
	'%      sCommand -
	Public Sub Add_Log(ByVal sCommand As String)
		Dim lngFile As Integer
		
		On Error GoTo ErrorHandler
		If strFileName = String.Empty Then
			strFileName = GetSetting("TIME", "PerformanceDebug", "LogFile", "C:\PerformanceDebug.Log")
		End If
		
		lngFile = FreeFile
		FileOpen(lngFile, strFileName, OpenMode.Append)
		PrintLine(lngFile, sCommand)
		FileClose(lngFile)
		
		Exit Sub
ErrorHandler: 
		'UPGRADE_WARNING: Array has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
		ProcError("TDebugAdo.Add_Log(sCommand)", New Object(){sCommand})
	End Sub
	
	'**%Objective:
	'%Objetivo:
	Public Function TimeMilliSec() As String
		
		Dim sngTimer As Single
		Dim intValue As Integer
		Dim strTime As String
		
		On Error GoTo ErrorHandler
		sngTimer = VB.Timer()
		
		If sngTimer >= 3600 Then
			intValue = Int(sngTimer / 3600!)
			sngTimer = sngTimer - (3600! * intValue)
		Else
			intValue = 0
		End If
		strTime = Format(intValue, "00") & ":"
		
		If sngTimer >= 60 Then
			intValue = Int(sngTimer / 60)
			sngTimer = sngTimer - (60 * intValue)
		Else
			intValue = 0
		End If
		strTime = strTime & Format(intValue, "00") & ":" & Format(sngTimer, "00.0000")
		Mid(strTime, InStr(strTime, ","), 1) = "."
		TimeMilliSec = strTime
		
		Exit Function
ErrorHandler: 
		ProcError("TDebugAdo.TimeMilliSec()")
	End Function
End Module






