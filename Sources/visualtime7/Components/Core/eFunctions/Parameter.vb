Option Strict Off
Option Explicit On
Public Class Parameter
	
	Public Enum eRmtDataDir
		rdbParamUnknown = 0
		rdbParamInput = 1
		rdbParamOutput = 2
		rdbParamInputOutput = 3
		rdbParamReturnValue = 4
	End Enum

    Public Enum eRmtDataType
        rdbEmpty = 0
        rdbBoolean = 2
        rdbChar = 3
        rdbDate = 4
        rdbDBTime = 4
        rdbNumeric = 5 'No existe equivalente en ADO.NET. Se iguala al valor para rdbDecimal
        rdbDecimal = 5
        rdbDouble = 6 'No existe equivalente en ADO.NET. Se utiliza el valor de SqlDbType.Float
        rdbImage = 7
        rdbInteger = 8
        rdbSmallInt = 16
        rdbDBTimeStamp = 19
        rdbVarchar = 22
    End Enum

	Public Name As String
	Public Value As Object
	Public Direction As eRmtDataDir
	Public ParType As eRmtDataType
	Public Size As Integer
	Public NumericScale As Byte
	Public Precision As Byte
	Public Attributes As Integer
	Public ParObject As Object
	
	Public VisibleColumn As Boolean
	Public CreateColumn As Boolean
	Public TitleColumn As String
	
	'-Variable que guarda el número de sesión
	Public sSessionID As String
	
	'-Código del usuario
	Public nUsercode As Integer
	
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		Name = String.Empty
		'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
		Value = System.DBNull.Value
		Direction = eRmtDataDir.rdbParamOutput
		ParType = eRmtDataType.rdbEmpty
		Size = 0
		NumericScale = 0
		Precision = 0
		Attributes = 0
		VisibleColumn = False
		CreateColumn = False
		TitleColumn = String.Empty
		'UPGRADE_NOTE: Object ParObject may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		ParObject = Nothing
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	'**%Incomplete: This property is used to determine if the parameter information is complete
	'**%            to execute the refresh button of the parameters
	'%Incomplete: Esta propiedad permite determinar si la información del parametro esta completa
	'%            o no, para saber si se realiza el refresh de los parametros.
	ReadOnly Property Incomplete() As Boolean
		Get
			Incomplete = (Direction = eRmtDataDir.rdbParamUnknown Or ParType = eRmtDataType.rdbEmpty)
		End Get
	End Property
End Class






