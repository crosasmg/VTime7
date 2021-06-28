Option Strict Off
Option Explicit On
Public Class Instruction
	Public Key As String
	
	Private mnLast As Double
	
	Public oCommand As eRemoteDB.Execute
	Public oMacros As Macros
	Public oParameters As Collection
	Public nTotal As Double
	Public nAverage As Double
	Public nSample As Double
	Public oExpandMacros As Macros
	Public sCommand As String
	Public bRecordSet As Boolean
	Public blnError As Boolean
	
	'+ Datos del error detectado.
	Public nError As Integer
	Public sDescription As String
	
	'+ Contiene el tiempo de espera.
	Public nDelay As Double
	
	
	Public Property nLast() As Double
		Get
			nLast = mnLast
		End Get
		Set(ByVal Value As Double)
			mnLast = Value
		End Set
	End Property
	
	Public Sub AddDefParameter(ByVal sLine As String)
		oParameters.Add(sLine, CObj(sLine))
	End Sub
	
	Public Sub ReLoadParameters()
		Dim oMacro As Macro
		Dim sLine As String
		Dim intIndex As Short
		Dim intField As Short
		Dim vntDef As Object
        Dim strNam As String = ""
        Dim strVal As String = ""
        Dim strDir As String = ""
        Dim strTyp As String = ""
        Dim strSiz As String = ""
        Dim strSca As String = ""
        Dim strPre As String = ""
        Dim strAtt As String = ""
        Dim intPos As Short
		
		If oCommand Is Nothing Then
			oCommand = New eRemoteDB.Execute
		End If
		
		If InStr("INSERT!SELECT!UPDATE!DELETE", Left(UCase(sCommand), 6)) > 0 Then
			oCommand.SQL = sCommand
		Else
			oCommand.StoredProcedure = sCommand
		End If
		
		For	Each vntDef In oParameters
			
			sLine = CStr(vntDef)
			sLine = Mid(sLine, 3)
			
			intIndex = InStr(sLine, "#,")
			intField = 0
			Do While intIndex > 0
				Select Case intField
					Case 0 'Name
						If Left(sLine, 1) = "@" Then
							strNam = Mid(sLine, 2, intIndex - 2)
						Else
							strNam = Mid(sLine, 1, intIndex - 1)
						End If
						intField = 1
						
						
					Case 1 'Value
						strVal = Mid(sLine, 1, intIndex - 1)
						
						For	Each oMacro In oExpandMacros
							intPos = InStr(strVal, oMacro.Name)
							If intPos > 0 Then
								strVal = Mid(strVal, 1, intPos - 1) & oMacro.Value & Mid(strVal, intPos + Len(oMacro.Name))
								Exit For
							End If
						Next oMacro
						
						intField = 2
						
					Case 2 'Dir
						strDir = Mid(sLine, 1, intIndex - 1)
						intField = 3
						
					Case 3 'Type
						strTyp = Mid(sLine, 1, intIndex - 1)
						intField = 4
						
					Case 4 'Size
						strSiz = Mid(sLine, 1, intIndex - 1)
						intField = 5
						
					Case 5 'NumericSacle
						strSca = Mid(sLine, 1, intIndex - 1)
						intField = 6
						
					Case 6 'Precision
						strPre = Mid(sLine, 1, intIndex - 1)
						intField = 7
						
						strAtt = Mid(sLine, intIndex + 2)
						Exit Do
						
				End Select
				sLine = Mid(sLine, intIndex + 2)
				intIndex = InStr(sLine, "#,")
			Loop 
			
			If strNam <> "RETURN_VALUE" Then
				'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
				oCommand.Parameters.Add(strNam, IIf(strVal = "NULL", System.DBNull.Value, strVal), CShort(strDir), CShort(strTyp), CInt(strSiz), CByte(strSca), CByte(strPre), CShort(strAtt))
			End If
		Next vntDef
	End Sub
	
	
	Public Function Execute() As Boolean
		Dim oMacro As Macro
		
		On Error GoTo ErrorHandler
		
		ReLoadParameters()
		
		
		If oCommand.Run(bRecordSet) Then
			
			If bRecordSet Then
				For	Each oMacro In oMacros
					If oMacro.IsField Then
						oMacro.PutValue = oCommand.FieldToClass(oMacro.Field)
						oExpandMacros.AddObj(oMacro)
					ElseIf Not oMacro.IsParameter Then 
						oMacro.ReCalValue()
						oExpandMacros.AddObj(oMacro)
					End If
				Next oMacro
				
				oCommand.RCloseRec()
			End If
			
			For	Each oMacro In oMacros
				If oMacro.IsParameter Then
					oMacro.PutValue = oCommand.Parameters((oMacro.Field)).Value
					oExpandMacros.AddObj(oMacro)
				End If
			Next oMacro
			Execute = True
		Else
			Execute = (oCommand.ErrorNumber = eRemoteDB.Execute.ErrorDB.clngNotFound)
		End If
		'UPGRADE_NOTE: Object oCommand may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		oCommand = Nothing
		'UPGRADE_NOTE: Object oExpandMacros may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		oExpandMacros = Nothing
		'Exit Function
ErrorHandler: 
		If Err.Number > 0 Then
			Execute = True
            General.mTypeError.nError = Err.Number
            General.mTypeError.sDescription = Err.Description
		End If
	End Function
	
	Public Sub Options(ByVal sLine As String)
		sLine = UCase(sLine)
		Select Case sLine
			Case "@RSET"
				bRecordSet = True
			Case "@NOTRSET"
				bRecordSet = False
		End Select
	End Sub
	
	
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		oMacros = New Macros
		oParameters = New Collection
		'Set oExpandMacros = New Collection
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
End Class






