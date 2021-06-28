Option Strict Off
Option Explicit On
Module LetterFunctions
	
	'% CleanLetter
	'------------------------------------------------------------
	Public Function CleanLetter(ByRef tletter As String) As String
		'------------------------------------------------------------
		CleanLetter = Replace(tletter, Chr(13), "")
		CleanLetter = Replace(CleanLetter, Chr(10), "")
	End Function
End Module










