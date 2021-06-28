Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Module FileSupport
	'**+Objective:
	'**+Version: $$Revision: $
	'+Objetivo:
	'+Version: $$Revision: $
	
	
	'%Objetivo: .
	'%Parámetros:
	'%    sFileName -
	'%    sBuffer -
	Public Sub AddBufferToFile(ByVal sBuffer As String, Optional ByVal sFileName As String = "c:\NetFrameWork.log")
		Dim clsConfig As eRemoteDB.VisualTimeConfig
		Dim strFilename As String
		Dim lngHan As Integer
		If Trim(sFileName) > String.Empty Then
			
			clsConfig = New eRemoteDB.VisualTimeConfig
			strFilename = clsConfig.LoadSetting("VisualTIMERLog", "C:\VisualTIMERLog", "Paths")
			clsConfig = Nothing
			strFilename = strFilename & "\" & sFileName & "_VisualTIMER.log"
			
			lngHan = FreeFile
			FileOpen(lngHan, strFilename, OpenMode.Append)
			sBuffer = TimeMilliSec & "|" & sBuffer
			PrintLine(lngHan, sBuffer)
			FileClose(lngHan)
		End If
		
		Exit Sub
	End Sub
	
	'%Objetivo: .
	'%Parámetros:
	'%    sFileName -
	'%    sBuffer -
	Public Sub SaveBufferToFile(ByVal sFileName As String, ByVal sBuffer As String, Optional ByVal IsAppend As Boolean = False, Optional ByVal AddTimer As Boolean = False, Optional ByRef Search_Drive As Boolean = False)
		Dim lngHan As Integer
		
		
		If Trim(sFileName) > String.Empty Then
			
			If Search_Drive Then
				sFileName = Drive & sFileName
			End If
			
			lngHan = FreeFile
			If IsAppend Then
				FileOpen(lngHan, sFileName, OpenMode.Append)
			Else
				FileOpen(lngHan, sFileName, OpenMode.Output)
			End If
			If AddTimer Then
				sBuffer = TimeMilliSec & " " & sBuffer
			End If
			PrintLine(lngHan, sBuffer)
			FileClose(lngHan)
		End If
		
		Exit Sub
	End Sub
	
	'%Objetivo: .
	'%Parámetros:
	'%    sFileName -
	Public Function LoadFileToBuffer(ByVal sFileName As String) As String
        Dim strBuffer As String = String.Empty
		Dim lngHan As Integer
		If Trim(sFileName) > String.Empty Then
			lngHan = FreeFile
			FileOpen(lngHan, sFileName, OpenMode.Input)
			strBuffer = InputString(lngHan, LOF(lngHan))
			FileClose(lngHan)
		End If
		LoadFileToBuffer = strBuffer
		
		Exit Function
	End Function
	
	'**%Objective:
	'%Objetivo:
	Public Function TimeMilliSec() As String
		Dim sngTimer As Single
		Dim intValue As Short
		Dim strTime As String
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
		strTime = strTime & Format(intValue, "00") & ":" & Format(sngTimer, "00.00000")
		If InStr(strTime, ",") > 0 Then
			Mid(strTime, InStr(strTime, ","), 1) = "."
		End If
		TimeMilliSec = strTime
		
		Exit Function
	End Function
	
	
	'%Objetivo: .
	'%Parámetros:
	'%    sFileName -
	Public Function LoadFileToText(ByVal sFileName As String) As String
		Dim lngHandle As Integer
		
		lngHandle = FreeFile
		FileOpen(lngHandle, sFileName, OpenMode.Binary)
		' read the string and close the file
		LoadFileToText = Space(LOF(lngHandle))
		'UPGRADE_WARNING: Get was upgraded to FileGet and has a new behavior. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
		FileGet(lngHandle, LoadFileToText)
		FileClose(lngHandle)
		
		Exit Function
	End Function
	
	'**%Objective:
	'**%Parameters:
	'%Objetivo:
	'%Parámetros:
	Public Function Drive() As String
		Dim strDrive As String
		strDrive = My.Application.Info.DirectoryPath
		If strDrive > String.Empty Then
			Drive = Left(strDrive, 2) & "\"
		Else
			Drive = "D:\"
		End If
		
		Exit Function
	End Function
	
#If PERFORMANCE Then
	'UPGRADE_NOTE: #If #EndIf block was not upgraded because the expression PERFORMANCE did not evaluate to True or was not evaluated. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="27EE2C3C-05AF-4C04-B2AF-657B4FB6B5FC"'
	'**%Objective:
	'%Objetivo:
	Public Function LoadFileToBufferXML() As String
	Dim strBuffer            As String
	Dim lngHan               As Long
	Dim sFileName            As String
	Dim lobjSession          As ASPSupport
	Dim lclsVisualTimeConfig As eRemoteDB.VisualTimeConfig
	
	Set lobjSession = New ASPSupport
	With lobjSession
	sFileName = .GetASPSessionValue("sXMLClaimFileName")
	End With
	Set lobjSession = Nothing
	
	If sFileName = String.Empty Then
	Exit Function
	End If
	
	Set lclsVisualTimeConfig = New eRemoteDB.VisualTimeConfig
	sFileName = lclsVisualTimeConfig.LoadSetting("Cache", String.Empty, "Paths") & "\Claims" & "\" & sFileName & ".xml"
	Set lclsVisualTimeConfig = Nothing
	
	If Trim$(sFileName) > String.Empty Then
	lngHan = FreeFile
	Open sFileName For Input As #lngHan
	strBuffer = Input(LOF(lngHan), #lngHan)
	Close #lngHan
	End If
	
	
	LoadFileToBufferXML = strBuffer
	
	Exit Function
	ProcError "FileSupport.LoadFileToBufferXML()", Array()
	End Function
#End If
End Module











