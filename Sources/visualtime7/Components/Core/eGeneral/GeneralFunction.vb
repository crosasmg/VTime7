Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Public Class GeneralFunction
	'%-------------------------------------------------------%'
	'% $Workfile:: GeneralFunction.cls                      $%'
	'% $Author:: Clobos                                     $%'
	'% $Date:: 23-03-06 15:51                               $%'
	'% $Revision:: 2                                        $%'
	'%-------------------------------------------------------%'
	
	
	'**- Enumerate type for the types of data
	'- Tipo enumerado para los tipos de datos
	Public Enum eTypeData
		TypNumInt = 0
		TypNumLng = 1
		TypNumSng = 2
		TypNumDbl = 3
		TypNumCur = 4
		TypDate = 5
		TypBoolean = 6 'No usado en los LET
		TypString = 7
		TypStrCheked = 8
		TypHour = 9
		TypOption = 10
	End Enum
	
	
	'**% Function to obtain and increase the counter in the numerator table,
	'**% if it's unsuccesful it returns -1.
	'% Función para obtener e incrementar los contadores en la tabla numerator
	'% si no tine éxito devuelve -1
	Public Function Find_Numerator(ByVal nTypenum As Integer, ByVal nOrd_num As Integer, ByVal nUsercode As Integer, Optional ByVal sCertype As String = "", Optional ByVal nBranch As Integer = 0, Optional ByVal nProduct As Integer = 0, Optional ByVal nDigit As Integer = 0, Optional ByVal nPaynumbe As Integer = 0, Optional ByVal nPolicy As Integer = 0, Optional ByVal nCertif As Integer = 0, Optional ByVal sCheque As String = "", Optional ByVal nConsec As Integer = 0, Optional ByVal nYear As Integer = 0) As Double
		Dim lrecinsNumerator As eRemoteDB.Execute
		
		On Error GoTo Find_Numerator_Err
		'**+ Parameter definition for the stored procedure 'insud.insNumerator'
		'+ Definición de parámetros para stored procedure 'insudb.insNumerator'
		'**+ Information read on October 21, 1999  08:44:27 a.m.
		'+ Información leída el 21/10/1999 08:44:27 AM
		lrecinsNumerator = New eRemoteDB.Execute
		With lrecinsNumerator
			.StoredProcedure = "InsReaUpdNumerator_sp"
			.Parameters.Add("nTypenum", nTypenum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOrd_num", nOrd_num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRequest_nu", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDigit", nDigit, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPaynumbe", nPaynumbe, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCheque", sCheque, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nConsec", nConsec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nYear", nYear, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Run(False)
			Find_Numerator = .Parameters("nRequest_nu").Value
		End With
		
Find_Numerator_Err: 
		If Err.Number Then
			Find_Numerator = -1
		End If
		'UPGRADE_NOTE: Object lrecinsNumerator may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsNumerator = Nothing
		On Error GoTo 0
	End Function
	
	'**% Find_Officeins: Verifies that a company has branch offices or not
	'% Find_Officeins: Verifica si una compañía tiene o no sucursales
	Public Function Find_Officeins(ByVal lintCompany As Integer) As Boolean
		Dim lrecreaOfficeins As eRemoteDB.Execute
		
		lrecreaOfficeins = New eRemoteDB.Execute
		
		'**+ Parameter information for stored procedure 'insudb.reaOfficeins'
		'+ Definición de parámetros para stored procedure 'insudb.reaOfficeins'
		'**+ Information read on October 21,1999  11:23:38 a.m.
		'+ Información leída el 21/10/1999 11:23:38 AM
		
		With lrecreaOfficeins
			.StoredProcedure = "reaOfficeins"
			.Parameters.Add("nCompany", lintCompany, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Find_Officeins = True
				.RCloseRec()
			Else
				Find_Officeins = False
			End If
		End With
		'UPGRADE_NOTE: Object lrecreaOfficeins may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaOfficeins = Nothing
	End Function
	
	'**% LetValue: restores the associated value to the type of data, or the default value.
	'% LetValue: devuelve el valor asociado al tipo de dato, o al valor por defecto
	Public Function GetValue(ByVal Value As Object, ByVal DatType As eTypeData, Optional ByRef DefValue As Object = Nothing) As Object
        'UPGRADE_WARNING: IsEmpty was upgraded to IsNothing and has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
        'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
        Dim caseAux As Object = New Object
        If Not IsDBNull(Value) And Not IsNothing(Value) Then
            Select Case DatType
                Case eTypeData.TypNumInt, eTypeData.TypNumSng, eTypeData.TypNumLng, eTypeData.TypNumDbl, eTypeData.TypNumCur
                    If IsNumeric(Value) Then
                        Select Case DatType
                            Case eTypeData.TypNumInt
                                caseAux = CShort(Value)
                            Case eTypeData.TypNumLng
                                caseAux = CInt(Value)
                            Case eTypeData.TypNumSng
                                caseAux = CSng(Value)
                            Case eTypeData.TypNumDbl
                                caseAux = CDbl(Value)
                            Case eTypeData.TypNumCur
                                caseAux = CDec(Value)
                        End Select
                    Else
                        'UPGRADE_NOTE: IsMissing() was changed to IsNothing(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="8AE1CB93-37AB-439A-A4FF-BE3B6760BB23"'
                        If Not (IsNothing(DefValue)) Then
                            caseAux = DefValue
                        Else
                            caseAux = 0
                        End If
                    End If
                Case eTypeData.TypString
                    'UPGRADE_NOTE: IsMissing() was changed to IsNothing(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="8AE1CB93-37AB-439A-A4FF-BE3B6760BB23"'
                    If (Value = String.Empty And Not (IsNothing(DefValue))) Then
                        caseAux = DefValue
                    Else
                        caseAux = Trim(Value)
                    End If
                Case eTypeData.TypStrCheked
                    caseAux = IIf(Value = "1", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
                Case eTypeData.TypDate
                    If Trim(Value) = "Dec 30 1899 12:00AM" Or Trim(Value) = "12:00:00 AM" Then
                        'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
                        caseAux = System.DBNull.Value
                    Else
                        caseAux = CDate(Format(Value, "Short date"))
                    End If
                Case eTypeData.TypHour
                    caseAux = Format(Value, "Short Time")
                Case eTypeData.TypOption
                    caseAux = (Value = "1")
                Case Else
                    caseAux = Value
            End Select
            'UPGRADE_NOTE: IsMissing() was changed to IsNothing(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="8AE1CB93-37AB-439A-A4FF-BE3B6760BB23"'
        ElseIf Not (IsNothing(DefValue)) Then
            caseAux = DefValue
        Else
            Select Case DatType
                Case eTypeData.TypNumInt, eTypeData.TypNumSng, eTypeData.TypNumLng, eTypeData.TypNumDbl, eTypeData.TypNumCur
                    caseAux = 0
                Case eTypeData.TypDate
                    'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
                    caseAux = System.DBNull.Value
                Case eTypeData.TypBoolean
                    caseAux = False
                Case eTypeData.TypString
                    caseAux = String.Empty
                Case eTypeData.TypStrCheked
                    caseAux = System.Windows.Forms.CheckState.Unchecked
                Case eTypeData.TypHour
                    caseAux = String.Empty
                Case eTypeData.TypOption
                    caseAux = False
            End Select
        End If
        Return caseAux
    End Function
	
	'**% Obtains the corresponding message to the error number content in lintError.
	'% Obtiene el mensaje correspondiente al número de error contenido en lintError.
	Public Function insLoadMessage(ByVal lintError As Integer) As String
		
		Dim lobjQuery As eRemoteDB.Query
		
		lobjQuery = New eRemoteDB.Query
		
		insLoadMessage = String.Empty
		
		With lobjQuery
			If .OpenQuery("Message", "sMessaged", "nErrornum=" & lintError) Then
				insLoadMessage = .FieldToClass("sMessaged")
				.CloseQuery()
			End If
		End With
		'UPGRADE_NOTE: Object lobjQuery may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjQuery = Nothing
		
	End Function
	
	'% getYearMonthDiff: Retorna la diferencia de años-meses entre dos fechas
	Public Sub getYearMonthDiff(ByVal dDateStart As Date, ByVal dDateEnd As Date, ByRef nYear As Integer, ByRef nMonth As Integer)
		
		'+  Se aplica division entera a diferencia de meses
		If DatePart(Microsoft.VisualBasic.DateInterval.Day, dDateStart) > DatePart(Microsoft.VisualBasic.DateInterval.Day, dDateEnd) Then
			nYear = (DateDiff(Microsoft.VisualBasic.DateInterval.Month, dDateStart, dDateEnd) - 1) \ 12
		Else
			nYear = DateDiff(Microsoft.VisualBasic.DateInterval.Month, dDateStart, dDateEnd) \ 12
		End If
		'+ A diferencia total de meses se restan los contemplados en años
		nMonth = DateDiff(Microsoft.VisualBasic.DateInterval.Month, dDateStart, dDateEnd) - 12 * nYear
		
	End Sub
	'**% LetValue: restores the associated value to the type of data.
	'% LetValue: devuelve el valor asociado al tipo de dato
	Public Function LetValue(ByVal Value As Object, ByVal DatType As eTypeData, Optional ByRef DefValue As Object = Nothing) As Object
        Dim lblnError As Boolean
        Dim caseAux As Object = New Object

        Select Case DatType
			Case eTypeData.TypNumInt, eTypeData.TypNumSng, eTypeData.TypNumLng, eTypeData.TypNumDbl, eTypeData.TypNumCur
				If IsNumeric(Value) Then
					Select Case DatType
						Case eTypeData.TypNumInt
                            caseAux = CShort(Value)
                        Case eTypeData.TypNumLng
                            caseAux = CInt(Value)
                        Case eTypeData.TypNumSng
                            caseAux = CSng(Value)
                        Case eTypeData.TypNumDbl
                            caseAux = CDbl(Value)
                        Case eTypeData.TypNumCur
                            caseAux = CDec(Value)
                    End Select
					lblnError = False
				Else
					lblnError = True
				End If
			Case eTypeData.TypDate
				If IsDate(Value) Then
                    caseAux = CDate(Value)
                    lblnError = False
				Else
					lblnError = True
				End If
			Case eTypeData.TypString
				'UPGRADE_WARNING: VarType has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
				If VarType(Value) = VariantType.Null Then
					lblnError = True
				Else
					If (Trim(Value) <> String.Empty) Then
                        caseAux = Trim(Value)
                        lblnError = False
					Else
						lblnError = True
					End If
				End If
			Case eTypeData.TypStrCheked
				If Value = System.Windows.Forms.CheckState.Checked Then
                    caseAux = "1"
                Else
                    caseAux = "2"
                End If
			Case eTypeData.TypHour
				If IsDate(Value) Then
                    caseAux = Format(Value, "Short Time")
                    lblnError = False
				Else
					lblnError = True
				End If
			Case eTypeData.TypOption
				If Value Then
                    caseAux = "1"
                Else
                    caseAux = "2"
                End If
			Case Else
                caseAux = Value
        End Select
        If lblnError Then
            'UPGRADE_NOTE: IsMissing() was changed to IsNothing(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="8AE1CB93-37AB-439A-A4FF-BE3B6760BB23"'
            If Not IsNothing(DefValue) Then
                caseAux = DefValue
            Else
                'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
                caseAux = System.DBNull.Value
            End If
        End If
        Return caseAux
    End Function
	
	'**% Find_Table10: Verifies if the Line of business exists.
	'% Find_Table10: Verifica si existe el Ramo
	Public Function Find_Table10(ByVal nBranch As Integer) As Boolean
		Dim lrecBranch As eRemoteDB.Execute
		
		On Error GoTo Find_Table10_err
		
		lrecBranch = New eRemoteDB.Execute
		
		Find_Table10 = True
		
		With lrecBranch
			.StoredProcedure = "reaTable10_v"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If Not .Run Then
				Find_Table10 = False
			End If
		End With
		
		'UPGRADE_NOTE: Object lrecBranch may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecBranch = Nothing
		
Find_Table10_err: 
		If Err.Number Then
			Find_Table10 = False
		End If
		On Error GoTo 0
		
	End Function
	
	'**% Find_Table9. Verifies if the Office exists.
	'% Find_Table9: Verifica si existe la Oficina
	Public Function Find_Table9(ByVal nOffice As Integer) As Boolean
		Dim lrecOffice As eRemoteDB.Execute
		
		On Error GoTo Find_Table9_err
		
		lrecOffice = New eRemoteDB.Execute
		
		Find_Table9 = True
		
		With lrecOffice
			.StoredProcedure = "reaTable9"
			.Parameters.Add("nOffice", nOffice, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If Not .Run Then
				Find_Table9 = False
			End If
		End With
		
		'UPGRADE_NOTE: Object lrecOffice may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecOffice = Nothing
		
Find_Table9_err: 
		If Err.Number Then
			Find_Table9 = False
		End If
		On Error GoTo 0
		
	End Function
	
	'------------------------- Funciones utilizadas para Seguridad   ----------------------------
	
	'**% StrEncode: Password Encryptment routine
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

    '**% StrDecode: Password de-encryptment routine
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

    '%getsKey: Rutina para obtener un valor único basado en mes-día-hora-segundos-usuario para el manejo de campo llave.
    Public Function getsKey(ByVal nUsercode As Integer) As String
		Dim lstrKey As String
		
		lstrKey = "T" & Today.ToString("yyyyMMdd") & TimeOfDay.ToString("hhmmss")
		If Len(Trim(Str(nUsercode))) > 5 Then
			getsKey = lstrKey & New String("0", 10 - Len(Trim(Str(nUsercode)))) & nUsercode
		Else
			getsKey = lstrKey & New String("0", 5 - Len(Trim(Str(nUsercode)))) & nUsercode
		End If
		getsKey = Mid(getsKey, 1, 20)
		
	End Function
	
	'%getWordsAmount: Rutina para obtener un valor único basado en mes-día-hora-segundos-usuario para el manejo de campo llave.
	Public Function getWordsAmount(ByVal sAmount As String, Optional ByVal sCurrency As String = "", Optional ByVal nCurrency As Integer = 0, Optional ByVal nDigitDecimal As Integer = 2) As String
		Dim lclsFunctions As eFunctions.Values
		Dim lrecinsWordsAmount As eRemoteDB.Execute
		Dim lstrWord As String
		
		On Error GoTo insWordsAmount_Err
		
		lclsFunctions = New eFunctions.Values
		lrecinsWordsAmount = New eRemoteDB.Execute
		
		lstrWord = String.Empty
		
		If sCurrency = String.Empty Then
			If nCurrency > 0 Then
				sCurrency = lclsFunctions.getMessage(nCurrency, "Table11")
			End If
		End If
		'+
		'+ Definición de store procedure insWordsamount al 05-08-2002 11:17:28
		'+
		With lrecinsWordsAmount
			.StoredProcedure = "insWordsAmount"
			.Parameters.Add("sAmount", sAmount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCurrency", sCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDigitDecimal", nDigitDecimal, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sWordsAmount1", lstrWord, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 128, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sWordsAmount2", lstrWord, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 128, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sWordsAmount3", lstrWord, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 128, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sWordsAmount4", lstrWord, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 128, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(False) Then
				getWordsAmount = .Parameters("sWordsAmount1").Value & .Parameters("sWordsAmount2").Value & .Parameters("sWordsAmount3").Value & .Parameters("sWordsAmount4").Value
			End If
		End With
		
insWordsAmount_Err: 
		If Err.Number Then
			getWordsAmount = String.Empty
		End If
		'UPGRADE_NOTE: Object lrecinsWordsAmount may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsWordsAmount = Nothing
		'UPGRADE_NOTE: Object lclsFunctions may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsFunctions = Nothing
		On Error GoTo 0
	End Function
	
	'%insPrecision: Devuelve la longitud de un campo de una tabla
	Public Function insPrecision(ByVal sTablename As String, ByVal sField As String) As String
		Dim lrecRecordset As eRemoteDB.Execute
		
		lrecRecordset = New eRemoteDB.Execute
		
		On Error GoTo insPrecision_err
		
		With lrecRecordset
			.SQL = "SELECT " & sField & " FROM " & sTablename & " WHERE 1 = 2"
			.Special = True
			
			If .Run Then
                If .FieldDatatype(sField) = "2" Then
                    insPrecision = CStr(.FieldPrecision(sField))
                Else
                    insPrecision = CStr(.FieldMaxsize(sField))
                End If
				.RCloseRec()
			Else
				insPrecision = String.Empty
			End If
		End With
		
		'UPGRADE_NOTE: Object lrecRecordset may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecRecordset = Nothing
		
insPrecision_err: 
		If Err.Number Then
			insPrecision = CStr(False)
		End If
		On Error GoTo 0
	End Function

    '%GetRegister: Obtiene la ruta del servidor donde se van a insertar los archivos
    Public Function GetLoadFile(Optional ByVal nOrigin As Boolean = False) As String
        Dim lclsvalue As eFunctions.Values
        Dim lstrName As String
        Dim lstrFileName As String
        Dim strResult As String = ""

        Try

            lclsvalue = New eFunctions.Values
            lstrFileName = Trim(UCase(lclsvalue.insGetSetting("LoadFile", String.Empty, "Paths")))
            If nOrigin Then
                Do While lstrFileName <> String.Empty
                    lstrName = Mid(lstrFileName, 1, 1)
                    strResult = strResult & IIf(lstrName = "\", "\\", lstrName)
                    lstrFileName = Mid(lstrFileName, 2)
                Loop
            Else
                strResult = lstrFileName
            End If
            Return strResult
        Catch ex As Exception
            strResult = CStr(False)
            Return strResult
        Finally
            'UPGRADE_NOTE: Object lclsvalue may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
            lclsvalue = Nothing
        End Try
    End Function
    '% GetLastFistDay: obtiene el primer o último día dem mes en curso
    Public Function GetLastFistDay(ByRef sDay As String) As Date
		Dim lrecGetLastFistDay As eRemoteDB.Execute
		
		On Error GoTo GetLastFistDay_err
		
		lrecGetLastFistDay = New eRemoteDB.Execute
		
		With lrecGetLastFistDay
			.StoredProcedure = "REA_LAST_FIRST_DAY"
			.Parameters.Add("sDay", sDay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dDate", Today, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(False) Then
				GetLastFistDay = .Parameters("dDate").Value
			Else
				GetLastFistDay = eRemoteDB.Constants.dtmNull
			End If
		End With
GetLastFistDay_err: 
		If Err.Number Then
			GetLastFistDay = eRemoteDB.Constants.dtmNull
		End If
		'UPGRADE_NOTE: Object lrecGetLastFistDay may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecGetLastFistDay = Nothing
		On Error GoTo 0
	End Function
	
	'% GetLastFistDay: Obtiene la fecha con el ultimo de dia del mes
	Public Function GetLastDay(ByRef dDate As Date) As Date
		
		On Error GoTo GetLastDay_err
		
		GetLastDay = System.Date.FromOADate(CDate("01/" & Month(DateAdd(Microsoft.VisualBasic.DateInterval.Month, 1, dDate)) & "/" & Year(DateAdd(Microsoft.VisualBasic.DateInterval.Month, 1, dDate))).ToOADate - 1)
		
GetLastDay_err: 
		If Err.Number Then
			GetLastDay = eRemoteDB.Constants.dtmNull
		End If
		On Error GoTo 0
    End Function


    '**%Objective: It verifies the existence of a registry in table "CliDocuments" using the key of this table.
    '**%Parameters:
    '**%    nClassTypDoc - Class of associated document al document that wants to be validated
    '**%    nTypClientDoc - Type of document to validate
    '**%    nDocument - Number or value of the document that itself this validating
    '%Objetivo: Verifica la existencia de un registro en la tabla "CliDocuments" usando la clave de dicha tabla.
    '%Parámetros:
    '%    nClassTypDoc  - Clase de documento asociada al documento que se quiere validar
    '%    nTypClientDoc - Tipo de documento a validar
    '%    nDocument - Número o valor del documento que se esta validando
    Public Function InsFormatValue(ByVal nClassTypDoc As Short, ByVal nTypClientDoc As Short, ByVal sDocument As String) As Boolean
        Dim lclsGeneralFunction As eRemoteDB.Execute
        Dim sValida As String

        lclsGeneralFunction = New eRemoteDB.Execute
        sValida = "0"

        '+ Define all parameters for the stored procedures 'insudb.valCliDocumentsExist'. Generated on 11/19/2004 3:04:01 PM
        With lclsGeneralFunction
            .StoredProcedure = "ValFormatValue"
            .Parameters.Add("nClassTypDoc", nClassTypDoc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTypClientDoc", nTypClientDoc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sDocument", sDocument, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 25, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sValida", sValida, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run(False) Then
                InsFormatValue = (.Parameters("sValida").Value = 1)
            Else
                InsFormatValue = False
            End If
        End With

        lclsGeneralFunction = Nothing

        Exit Function
    End Function
    '--------------------------------------------------------------------------------------------
    Public Function ValCurrency(ByVal nCurrency As Long, ByVal dEffecdate As Date) As Boolean
        '--------------------------------------------------------------------------------------------
        Dim lValcurrency As eRemoteDB.Execute

        On Error GoTo ValCurrency_err

        lValcurrency = New eRemoteDB.Execute

        With lValcurrency
            .StoredProcedure = "INSVALCURRENCY"
            .Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nExist", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            If .Run(False) Then
                ValCurrency = .Parameters("nExist").Value = 1
            End If

        End With

ValCurrency_err:
        If Err.Number Then
            ValCurrency = False
        End If
        On Error GoTo 0
    End Function
End Class






