Option Strict Off
Option Explicit On
Public Class valField
	
	Public Enum eTypeValField
		ValAll
		onlyvalid
	End Enum
	
	Public Enum eError
		veEmpty
		veInvalid
		veOutRange
	End Enum
	
	Public Enum eTypeVal
		veDate
		veNumber
		veHour
	End Enum
	
	Private mvarTypeVal As eTypeVal
    Private mvarobjErr As Errors
	Private mvarMin As Object
	Private mvarMax As Object
	Private mvarDescript As String
	Private mvarValFormat As String
	Private mvarValue As Object
	Private mblnEqualMax As Boolean
	Private mblnEqualMin As Boolean
	Private mstrCodispl As String
	Private mvarvfError As eError
	
	Private mvarErrInvalid As Integer
	Private mvarErrRange As Integer
	Private mvarErrEmpty As Integer

	'-Variable que guarda el número de sesión
	Public sSessionID As String
	
	'-Código del usuario
	Public nUsercode As Integer
	
	Public Property ErrInvalid() As Integer
		Get
			If (mvarErrInvalid = 0) Then
				Select Case mvarTypeVal
					Case eTypeVal.veDate
						mvarErrInvalid = 1001
					Case eTypeVal.veNumber
						mvarErrInvalid = 1937
					Case eTypeVal.veHour
						mvarErrInvalid = 12140
				End Select
			End If
			ErrInvalid = mvarErrInvalid
		End Get
		Set(ByVal Value As Integer)
			mvarErrInvalid = Value
		End Set
	End Property
	
	
	Public Property ErrRange() As Integer
		Get
			If (mvarErrRange = 0) Then
				Select Case mvarTypeVal
					Case eTypeVal.veDate
						mvarErrRange = 1935
					Case eTypeVal.veNumber
						mvarErrRange = 1935
					Case eTypeVal.veHour
						mvarErrRange = 1935
				End Select
			End If
			ErrRange = mvarErrRange
		End Get
		Set(ByVal Value As Integer)
			mvarErrRange = Value
		End Set
	End Property

	Public Property ErrEmpty() As Integer
		Get
			If (mvarErrEmpty = 0) Then
				Select Case mvarTypeVal
					Case eTypeVal.veDate
						mvarErrEmpty = 1012
					Case eTypeVal.veNumber
						mvarErrEmpty = 9004
					Case eTypeVal.veHour
						mvarErrEmpty = 1012
				End Select
			End If
			ErrEmpty = mvarErrEmpty
		End Get
		Set(ByVal Value As Integer)
			mvarErrEmpty = Value
		End Set
	End Property
	
	
	Public Property vfError() As eError
		Get
			vfError = mvarvfError
		End Get
		Set(ByVal Value As eError)
			mvarvfError = Value
		End Set
	End Property
	
	
	Public Property objErr() As Object
		Get
			objErr = mvarobjErr
		End Get
		Set(ByVal Value As Object)
			mvarobjErr = Value
		End Set
	End Property
	
	Public ReadOnly Property Value() As Object
		Get
			Value = mvarValue
		End Get
	End Property
	
	
	Public Property ValFormat() As String
		Get
			ValFormat = mvarValFormat
		End Get
		Set(ByVal Value As String)
			mvarValFormat = Value
		End Set
	End Property
	
	
	Public Property Descript() As String
		Get
			Descript = mvarDescript
		End Get
		Set(ByVal Value As String)
			mvarDescript = Value
		End Set
	End Property
	
	
	Public Property Min() As Object
		Get
			Min = mvarMin
		End Get
		Set(ByVal Value As Object)
			mvarMin = Value
		End Set
	End Property
	
	
	Public Property Max() As Object
		Get
			Max = mvarMax
		End Get
		Set(ByVal Value As Object)
			mvarMax = Value
		End Set
	End Property
	
	
	Public Property EqualMax() As Boolean
		Get
			EqualMax = mblnEqualMax
		End Get
		Set(ByVal Value As Boolean)
			mblnEqualMax = Value
		End Set
	End Property
	
	
	Public Property EqualMin() As Boolean
		Get
			EqualMin = mblnEqualMin
		End Get
		Set(ByVal Value As Boolean)
			mblnEqualMin = Value
		End Set
	End Property
	
	
	Public Property Codispl() As String
		Get
			Codispl = mstrCodispl
		End Get
		Set(ByVal Value As String)
			mstrCodispl = Value
		End Set
    End Property

	'**% MISSING
    Public Function ValNumber(ByVal FieldNumber As Object, Optional ByVal PosRow As Integer = 0, Optional ByVal TypeVal As eTypeValField = eTypeValField.ValAll) As Boolean
        Dim lclsValues As New Values
        Dim lblnError As Boolean
        Dim lstrMessage As String = ""
        Dim lvarFieldNumber As Object

        mvarTypeVal = eTypeVal.veNumber
        lvarFieldNumber = Replace(Replace(FieldNumber, lclsValues.msUserThousandSeparator, String.Empty), lclsValues.msUserDecimalSeparator, lclsValues.msServerDecimalSeparator)

        If lvarFieldNumber = CStr(eRemoteDB.Constants.intNull) Then lvarFieldNumber = Nothing
        '**+ Validate that the format is a number value.
        '+ Se valida que el formato sea un valor numérico.
        If IsNumeric(lvarFieldNumber) And Not IsNothing(lvarFieldNumber) Then

            '**+ Verify the minimum and maximum values permitted
            '+Se verifican los valores minimos y maximos permitidos
            lblnError = False
            lvarFieldNumber = CDbl(lvarFieldNumber)
            If IsNumeric(mvarMin) AndAlso CDbl(lvarFieldNumber) < mvarMin Then
                lblnError = True
            End If
            If IsNumeric(mvarMax) > 0 AndAlso CDbl(lvarFieldNumber) > mvarMax Then
                lblnError = True
            End If
            If lblnError Then
                mvarvfError = eError.veOutRange
                If (mvarDescript <> String.Empty) Then
                    lstrMessage = Trim(mvarDescript)
                End If
                '            mvarobjErr.EText = lstrMessage & " : [" & CStr(mvarMin) & "-" & CStr(mvarMax) & "]"

                If mvarobjErr.ErrorMessage(Codispl, ErrRange, PosRow, Errors.TextAlign.LeftAling, lstrMessage & ": [" & CStr(mvarMin) & "-" & CStr(mvarMax) & "]") = String.Empty Then
                    ValNumber = False
                End If
            End If

            '**+ In case where there is no error and some format is specified.
            '+En caso no haber tenido ningun error y de ser especificado algún formato
            If (Not lblnError And ValFormat <> String.Empty) Then
                mvarValue = Format(lvarFieldNumber, ValFormat)
                ValNumber = True
            End If
            If Not lblnError Then
                ValNumber = True
            End If
        Else
            If IsNothing(lvarFieldNumber) Or Trim(lvarFieldNumber) = String.Empty Then
                mvarvfError = eError.veEmpty
                If TypeVal = eTypeValField.ValAll Then
                    If mvarobjErr.ErrorMessage(Codispl, ErrEmpty, PosRow, Errors.TextAlign.LeftAling, IIf(mvarDescript <> String.Empty, Trim(mvarDescript), String.Empty)) = String.Empty Then
                        ValNumber = False
                    End If
                End If
            Else
                mvarvfError = eError.veInvalid
                If mvarobjErr.ErrorMessage(Codispl, ErrInvalid, PosRow, Errors.TextAlign.LeftAling, IIf(mvarDescript <> String.Empty, Trim(mvarDescript), String.Empty)) = String.Empty Then
                    ValNumber = False
                End If
            End If
        End If
        InitNumErr()
    End Function
	
	'**% ValDate: Function to validate date field
	'% MISSING
	Public Function ValDate(ByVal FieldDate As Object, Optional ByVal PosRow As Integer = 0, Optional ByVal TypeVal As eTypeValField = eTypeValField.ValAll) As Boolean
		Dim lblnError As Boolean
		Dim lobjValues As Values
		
		lobjValues = New Values


        If FieldDate.ToString() = String.Empty Then
            FieldDate = eRemoteDB.Constants.dtmNull
        End If
		
		'**+ The variable FieldDate is first converted to the server date format using
		'**+ the SysDateFormat method in the Values class
		FieldDate = IIf(FieldDate <> eRemoteDB.Constants.dtmNull, lobjValues.SysDateFormat(FieldDate), FieldDate)
		
		mvarTypeVal = eTypeVal.veDate
		
		ValDate = True
		If IsDate(FieldDate) Then
			If FieldDate = eRemoteDB.Constants.dtmNull Then
				mvarvfError = eError.veEmpty
				Call mvarobjErr.ErrorMessage(Codispl, ErrEmpty, PosRow,  , IIf(mvarDescript <> String.Empty, Trim(mvarDescript), String.Empty))
				ValDate = False
			Else
				If (FieldDate <= CDate("01/01/1800")) Then
					mvarvfError = eError.veInvalid
					Call mvarobjErr.ErrorMessage(Codispl, ErrInvalid, PosRow,  , IIf(mvarDescript <> String.Empty, Trim(mvarDescript), String.Empty))
					ValDate = False
				Else
					mvarValue = CDate(FieldDate)
					
					'+ Verifica el valor mínimo
					If IsDate(mvarMin) Then
						If mblnEqualMin Then
							lblnError = CDate(mvarValue) < CDate(mvarMin)
						Else
							lblnError = CDate(mvarValue) <= CDate(mvarMin)
						End If
					End If
					
					'+ Verifica el valor máximo
					If IsDate(mvarMax) And Not lblnError Then
						If mblnEqualMax Then
							lblnError = CDate(mvarValue) > CDate(mvarMax)
						Else
							lblnError = CDate(mvarValue) >= CDate(mvarMax)
						End If
					End If
					
					If lblnError Then
						mvarvfError = eError.veOutRange
						Call mvarobjErr.ErrorMessage(Codispl, ErrRange, PosRow,  , IIf(mvarDescript <> String.Empty, Trim(mvarDescript), String.Empty))
						ValDate = False
					End If
				End If
			End If
        ElseIf (IsDBNull(FieldDate) Or IsNothing(FieldDate)) And TypeVal = eTypeValField.ValAll Then
            mvarvfError = eError.veEmpty
            Call mvarobjErr.ErrorMessage(Codispl, ErrEmpty, PosRow, , IIf(mvarDescript <> String.Empty, Trim(mvarDescript), String.Empty))
            ValDate = False
		ElseIf (Trim(FieldDate) = "/  /" Or Trim(FieldDate) = ":" Or Trim(FieldDate) = String.Empty) Then 
			mvarvfError = eError.veEmpty
			ValDate = False
			If TypeVal = eTypeValField.ValAll Then
				Call mvarobjErr.ErrorMessage(Codispl, ErrEmpty, PosRow,  , IIf(mvarDescript <> String.Empty, Trim(mvarDescript), String.Empty))
			End If
		ElseIf (TypeVal = eTypeValField.ValAll Or TypeVal = eTypeValField.onlyvalid) Then 
			mvarvfError = eError.veInvalid
			Call mvarobjErr.ErrorMessage(Codispl, ErrInvalid, PosRow,  , IIf(mvarDescript <> String.Empty, Trim(mvarDescript), String.Empty))
			ValDate = False
		Else
			ValDate = False
		End If
		InitNumErr()
        lobjValues = Nothing
    End Function

	'**% MISSING
	'% MISSING
	Public Function ValHour(ByRef FieldHour As Object, Optional ByVal PosRow As Integer = 0, Optional ByVal TypeVal As eTypeValField = eTypeValField.ValAll) As Boolean
		Dim lblnError As Boolean
		
		mvarTypeVal = eTypeVal.veHour
		
		ValHour = True
		
		If IsDate(FieldHour) Then
			mvarValue = CDate(FieldHour)
			
			'**+ Verifies that is in the specified rage.
			'+ Se verifica que la esté dentro del rango especificado
			'**+ Verify the minimum value
			'+ Verifica el valor mínimo
			If IsDate(mvarMin) Then
				If mblnEqualMin Then
					lblnError = CDate(mvarValue) < CDate(mvarMin)
				Else
					lblnError = CDate(mvarValue) <= CDate(mvarMin)
				End If
			End If
			
			'**+ Verifies the maximum value
			'+ Verifica el valor máximo
			If IsDate(mvarMax) And Not lblnError Then
				If mblnEqualMax Then
					lblnError = CDate(mvarValue) > CDate(mvarMax)
				Else
					lblnError = CDate(mvarValue) >= CDate(mvarMax)
				End If
			End If
			
			If lblnError Then
				mvarvfError = eError.veOutRange
				Call mvarobjErr.ErrorMessage(Codispl, ErrRange, PosRow,  , IIf(mvarDescript <> String.Empty, Trim(mvarDescript), String.Empty))
				ValHour = False
			End If
			
        ElseIf (IsDBNull(FieldHour) Or IsNothing(FieldHour) Or Trim(FieldHour) = String.Empty) And TypeVal = eTypeValField.ValAll Then
            mvarvfError = eError.veEmpty
            Call mvarobjErr.ErrorMessage(Codispl, ErrEmpty, PosRow, , IIf(mvarDescript <> String.Empty, Trim(mvarDescript), String.Empty))
            ValHour = False
			
		ElseIf (Trim(FieldHour) = ":" Or Trim(FieldHour) = String.Empty) Then 
			mvarvfError = eError.veEmpty
			If TypeVal = eTypeValField.ValAll Then
				Call mvarobjErr.ErrorMessage(Codispl, ErrEmpty, PosRow,  , IIf(mvarDescript <> String.Empty, Trim(mvarDescript), String.Empty))
				ValHour = False
			Else
				ValHour = False
			End If
		ElseIf (TypeVal = eTypeValField.ValAll Or TypeVal = eTypeValField.onlyvalid) Then 
			mvarvfError = eError.veInvalid
			Call mvarobjErr.ErrorMessage(Codispl, ErrInvalid, PosRow,  , IIf(mvarDescript <> String.Empty, Trim(mvarDescript), String.Empty))
			ValHour = False
		Else
			ValHour = False
		End If
		
		InitNumErr()
	End Function
	
	Private Sub InitNumErr()
		mvarErrInvalid = 0
		mvarErrEmpty = 0
		mvarErrRange = 0
        mvarMin = System.DBNull.Value
        mvarMax = System.DBNull.Value
		mblnEqualMax = False
		mblnEqualMin = False
	End Sub
	
	Public Sub New()
		MyBase.New()
    End Sub
	
End Class






