Option Strict Off
Option Explicit On
Public Class FormRecord
	Public isKey As String
	Public isValue As String
	Public ibFile As Boolean
	Public Enum fieldType
		file = 1
		notfile = 2
		invalid = 3
	End Enum
	Public iFieldType As fieldType
	Public Function getContentDisposition(ByRef aLinea As String) As fieldType
		
		Dim lstr As String
		Dim lpos As Integer
		Dim lpossemicolon As Integer
		Dim lblnFlag As Boolean
		Dim llen As Integer
		
		On Error GoTo getContentDisposition_err
		
		llen = Len(aLinea)
		aLinea = aLinea & ";"
		
		lpos = InStr(1, aLinea, "Content-Disposition:", CompareMethod.Text)
		lpossemicolon = InStr(1, aLinea, ";", CompareMethod.Text)
		
		If lpos = 0 Then
			
			lblnFlag = False
			
		Else
			
			lstr = Mid(aLinea, lpos, lpossemicolon - lpos)
			
		End If
		
		lpos = InStr(1, aLinea, "name=", CompareMethod.Text)
		
		lpossemicolon = InStr(lpos, aLinea, ";", CompareMethod.Text)
		
		If lpos = 0 Then
			
			lblnFlag = False
			
		Else
			
			If lpossemicolon = 0 Then
				
				lpossemicolon = llen + 2
				
			End If
			
			lpos = lpos + Len("name=") + 1
			lstr = Mid(aLinea, lpos, lpossemicolon - lpos - 1)
			isKey = lstr
		End If
		
		
		lpos = InStr(lpossemicolon, aLinea, "filename=", CompareMethod.Text)
		
		If lpos = 0 Then
			
			getContentDisposition = fieldType.notfile
			Me.ibFile = False
			Me.iFieldType = fieldType.notfile
			
			
		Else
			
			getContentDisposition = fieldType.file
			Me.ibFile = True
			Me.iFieldType = fieldType.file
			
		End If
		
getContentDisposition_err: 
		
		
		If Err.Number Then
			
			getContentDisposition = fieldType.invalid
			Err.Raise(vbObjectError + 101, "eCollection.FormRecord")
			
		End If
		
		On Error GoTo 0
		
		
		
	End Function
	'Content-Type: text/plain
	
	Public Function getContentType(ByRef aLinea As String) As Integer
		
		Dim lstr As String
		Dim lpos As Integer
		Dim lposcolon As Integer
		Dim lblnFlag As Boolean
		Dim llen As Integer
		
		
		llen = Len(aLinea)
		
		lpos = InStr(1, aLinea, "Content-Type:", CompareMethod.Text)
		lposcolon = InStr(1, aLinea, ":", CompareMethod.Text)
		
		If lpos = 0 Then
			
			lblnFlag = False
			
		Else
			
			lstr = Mid(aLinea, lpos, lposcolon - lpos)
			
		End If
		
		
		getContentType = lpos
		
	End Function
	Public Function getContentValue(ByRef asLinea As String) As Integer
		
		If Me.iFieldType = fieldType.file Then
			
			isValue = isValue & asLinea & CStr(vbCrLf)
			
		Else
			
			isValue = isValue & asLinea
			
		End If
		
		getContentValue = 0
		
	End Function
	
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		
		Me.isValue = ""
		Me.iFieldType = fieldType.invalid
		Me.ibFile = False
		
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
End Class






