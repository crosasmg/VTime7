Option Strict Off
Option Explicit On
Public Class FormFile
	'%-------------------------------------------------------%'
	'% $Workfile:: FormFile.cls                             $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:29p                                $%'
	'% $Revision:: 27                                       $%'
	'%-------------------------------------------------------%'
	Private mvarFormRecords As FormRecords
	Private mvariBoundary As String
	Private mStringBuffer As String
	Private mvariStreamBuffer() As Byte
	Private mSize As Double 
	Private mCurrPos As Integer
	Public ibOK As Boolean
	Public inLines As Integer

    '-La implementación de esta componente se basa en una lectura rápida de los documentos siguientes:
    '- RFC 1867, RFC 1521, RFC 1341


    Public Sub pTextFormFile(ByRef asFileName As String, ByRef asBoundary As String)

        Dim lsLinea As Object = New Object
        Dim lstrInputData As Object

        FileOpen(1, asFileName, OpenMode.Input) ' Abre el archivo recién creado.
        Do While Not EOF(1) ' Repite el bucle hasta el final del archivo.
            lstrInputData = LineInput(1)
            lsLinea = lsLinea & lstrInputData & CStr(vbCrLf) ' Lee el carácter en la variable.
        Loop
        FileClose(1) ' Cierra el archivo.


        Me.iBoundary = asBoundary
        Me.iStringBuffer = lsLinea

    End Sub
    Public Function pBinaryFormFile(ByRef asFileName As String, ByRef asBoundary As String) As Boolean
		
		Dim lbLinea() As Byte
		Dim lcurrPos As Integer
		
		FileOpen(1, asFileName, OpenMode.Binary) ' Abre el archivo recién creado.
		
		ReDim lbLinea(LOF(1))
		
		Do While lcurrPos < LOF(1) ' Repite el bucle hasta el final del archivo.
			Input(1, lbLinea(lcurrPos + 1))
			lcurrPos = Loc(1) ' Obtiene la posición actual en el archivo.
		Loop 
		
		FileClose(1) ' Cierra el archivo.
		
		Me.iBoundary = asBoundary
		'UPGRADE_TODO: Code was upgraded to use System.Text.UnicodeEncoding.Unicode.GetString() which may not have the same behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="93DD716C-10E3-41BE-A4A8-3BA40157905B"'
		Me.iStringBuffer = System.Text.UnicodeEncoding.Unicode.GetString(lbLinea)
		
		pBinaryFormFile = False
		
	End Function
	Public Function Request(ByRef asFieldName As Object) As String
		Dim lFormRecord As FormRecord
		
		On Error GoTo Request_err
		
		If iFormRecords.Count = 0 Then
			
			Request = String.Empty
			
		Else
			
			lFormRecord = iFormRecords(asFieldName)
			
			Request = lFormRecord.isValue
			
		End If
		
Request_err: 
		
		If Err.Number Then
			Request = String.Empty
		End If
		
		On Error GoTo 0
		
	End Function
	Private Function EOS() As Boolean
		
		If mCurrPos = mSize Then
			EOS = True
		Else
			EOS = False
		End If
		
	End Function
	Private Function lineToString(ByRef Linea() As Byte) As String
		
		Dim li as Double 
		Dim ls As String
		
		li = 0
		ls = ""

        If UBound(Linea) < mSize Then
            ReDim Preserve Linea(mSize)
        End If
		
		While li <= mSize
			If Linea(li) <> 0 Then
                If Linea(li) <> 13 Then
				    ls = ls & Chr(Linea(li))
                Else
                    Exit While 
                End If 
			End If
			li = li + 1
		End While
		
		lineToString = ls
		
	End Function
	Private Function getLine() As Byte()
		
		Dim iByte As Byte
		Dim Linea() As Byte
		Dim lInitialPos As Integer
		Dim eoln As Byte
		Dim lblneoln As Boolean
		
		eoln = CByte(10)
		lblneoln = False
		
		ReDim Linea(mSize - mCurrPos + 1)
		
		lInitialPos = mCurrPos
		
		Do While (mCurrPos <= mSize) And Not lblneoln
			
			iByte = mvariStreamBuffer(mCurrPos)
			
			If iByte <> eoln Then
				
				Linea(mCurrPos - lInitialPos + 1) = iByte
				
			Else
				
				lblneoln = True
				
			End If
			
			mCurrPos = mCurrPos + 1
			
		Loop 
		
		If mCurrPos > mSize Then
			
			ibOK = False
			
		End If
		
		inLines = inLines + 1
        getLine = Linea		
	End Function
	Private Function getStrLine() As String

        Dim Linea As String
		Dim lInitialPos As Integer
		Dim seoln As String
		Dim lblneoln As Boolean
		
		seoln = CStr(vbCrLf)
		
		lblneoln = False
		
		
		lInitialPos = InStr(mCurrPos + 1, mStringBuffer, seoln, CompareMethod.Text)
		
		
		If lInitialPos = 0 Then
			
			Linea = Mid(mStringBuffer, mCurrPos + 1)
			mCurrPos = mSize
			
		Else
			
			Linea = Mid(mStringBuffer, mCurrPos + 1, lInitialPos - mCurrPos - 1)
			mCurrPos = lInitialPos + 1
			
		End If
		
		
		If mCurrPos > mSize Then
			
			ibOK = False
			
		End If
		
		inLines = inLines + 1
		getStrLine = Linea
		
	End Function
	
	Public Function getRandomFilename(ByVal anUserCode As Integer, Optional ByRef asSeed As String = "TMP") As String
		
		Dim lstrDrive As String
		Dim lstrPath As String
		Dim lstrFileName As String
		Dim lobjGeneral As eGeneral.GeneralFunction
		
		On Error GoTo getRandomFilename_err
		
		lobjGeneral = New eGeneral.GeneralFunction
		
		
		lstrPath = lobjGeneral.GetLoadFile()
		
		lstrFileName = asSeed & lobjGeneral.getsKey(anUserCode)
		
		lstrFileName = lstrPath & lstrFileName
		
		getRandomFilename = lstrFileName
		
getRandomFilename_err: 
		
		If Err.Number Then
			getRandomFilename = "tmp"
		End If
		
		On Error GoTo 0
		
		
	End Function
	
	Public Property iStreamBuffer() As Byte()
		Get
            ''used when retrieving value of a property, on the right side of an assignment.
            ''Syntax: Debug.Print X.iStreamBuffer
            '    If IsObject(mvariStreamBuffer) Then
            '        Set iStreamBuffer = mvariStreamBuffer
            '    Else
            '        iStreamBuffer = mvariStreamBuffer
            '    End If
            Return Nothing
        End Get
		Set(ByVal Value() As Byte)

            Dim line() As Byte
            Dim lcurrLine As String
            Dim lsEOF As String
			Dim li As Integer
			Dim lFormRecord As FormRecord
			Dim lContentType As FormRecord.fieldType
			
			
			li = 1
			lsEOF = Me.iBoundary & "--"
            mvariStreamBuffer = Value
			mSize = UBound(Value)
			
			ReDim line(mSize)
			
			lcurrLine = lineToString(getLine)
			
			If StrComp(lcurrLine, Me.iBoundary, CompareMethod.Text) <> 0 Then
				
				Me.ibOK = False
				
				Err.Raise(vbObjectError + 101, "eCollection.FormFile iBoundary Error")
				
			End If
			
			
			Do While Not EOS() And Me.ibOK
				
				lFormRecord = New FormRecord
				
				lcurrLine = lineToString(getLine)
				
				lContentType = lFormRecord.getContentDisposition(lcurrLine)
				
				If lContentType = FormRecord.fieldType.invalid Then
					
					Me.ibOK = False
					Err.Raise(vbObjectError + 102, "eCollection.FormFile ContentDiposition Error")
					
				End If
				
				If lContentType = FormRecord.fieldType.file Then
					
					lcurrLine = lineToString(getLine)
					lFormRecord.getContentType(lcurrLine)
					lcurrLine = lineToString(getLine)
					
				End If
				
				lcurrLine = lineToString(getLine)
				
				Do While StrComp(lcurrLine, Me.iBoundary, CompareMethod.Text) <> 0 And StrComp(lcurrLine, lsEOF, CompareMethod.Text) <> 0 And Me.ibOK
					
					lFormRecord.getContentValue(lcurrLine)
					lcurrLine = lineToString(getLine)
					
				Loop 
				
				Me.iFormRecords.Add(lFormRecord, lFormRecord.isKey)
				
			Loop 
			
			
		End Set
	End Property
	Public Property iBoundary() As String
		Get
			'used when retrieving value of a property, on the right side of an assignment.
			'Syntax: Debug.Print X.iBoundary
			iBoundary = mvariBoundary
		End Get
		Set(ByVal Value As String)
			
			mvariBoundary = Value
			
		End Set
	End Property
	Public Property iFormRecords() As FormRecords
		Get
			If mvarFormRecords Is Nothing Then
				mvarFormRecords = New FormRecords
			End If
			iFormRecords = mvarFormRecords
		End Get
		Set(ByVal Value As FormRecords)
			mvarFormRecords = Value
		End Set
	End Property
	Public WriteOnly Property iStringBuffer() As String
		Set(ByVal Value As String)

            Dim line() As Byte
            Dim lcurrLine As String
            Dim lsEOF As String
			Dim li As Integer
			Dim lFormRecord As FormRecord
			
			li = 1
			lsEOF = Me.iBoundary & "--"
			mStringBuffer = Value
			mSize = Len(Value)
			
			ReDim line(mSize)
			
			lcurrLine = getStrLine()
			
			If StrComp(lcurrLine, Me.iBoundary, CompareMethod.Text) <> 0 Then
				
				ibOK = False
				
			End If
			
			
			Do While Not EOS() And Me.ibOK
				
				lFormRecord = New FormRecord
				
				lcurrLine = getStrLine()
				If lFormRecord.getContentDisposition(lcurrLine) = FormRecord.fieldType.file Then
					
					lcurrLine = getStrLine()
					lFormRecord.getContentType(lcurrLine)
					'+  Revisar estructura del RFC para determinar
					lcurrLine = getStrLine()
					
				End If
				
				lcurrLine = getStrLine()
				
				Do While StrComp(lcurrLine, Me.iBoundary, CompareMethod.Text) <> 0 And StrComp(lcurrLine, lsEOF, CompareMethod.Text) <> 0
					
					lFormRecord.getContentValue(lcurrLine)
					lcurrLine = getStrLine()
					
				Loop 
				
				Me.iFormRecords.Add(lFormRecord, lFormRecord.isKey)
				
			Loop 
			
		End Set
	End Property
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		
		'    Set mvariBoundary = New Boundary
		mSize = 0
		mCurrPos = 0
		ibOK = True
		inLines = 0
		
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	'UPGRADE_NOTE: Class_Terminate was upgraded to Class_Terminate_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Terminate_Renamed()
		'UPGRADE_NOTE: Object mvarFormRecords may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mvarFormRecords = Nothing
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
End Class






