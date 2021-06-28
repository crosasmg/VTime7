Option Strict Off
Option Explicit On
Public Class Parameters
	Implements System.Collections.IEnumerable
	
	Private mCol As Collection
	
	'- Variable para manejar los valores que devolverá el Stored Procedure
	Private mcolReturnValue As Collection
	
	'-Variable que guarda el número de sesión
	Public sSessionID As String
	
	'-Código del usuario
	Public nUsercode As Integer
	
	'* Class_Initialize: se controla la creación de la instancia de la clase
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		mCol = New Collection
		mcolReturnValue = New Collection
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'* Class_Terminate: se controla la destrucción de la instancia de la clase
	'UPGRADE_NOTE: Class_Terminate was upgraded to Class_Terminate_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Terminate_Renamed()
		'UPGRADE_NOTE: Object mCol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mCol = Nothing
		'UPGRADE_NOTE: Object mcolReturnValue may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mcolReturnValue = Nothing
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
	
	'% Add: se agrega un elemento a la colección de parámetros del Stored Procedure
	Public Function Add(ByVal Name As String, ByVal Value As Object, Optional ByVal Direction As Parameter.eRmtDataDir = Parameter.eRmtDataDir.rdbParamInput, Optional ByVal ParType As Parameter.eRmtDataType = Parameter.eRmtDataType.rdbEmpty, Optional ByVal Size As Integer = 0, Optional ByVal NumericScale As Byte = 0, Optional ByVal Precision As Byte = 0, Optional ByVal Attributes As Integer = 0, Optional ByVal ParObject As Object = Nothing) As Parameter
		Dim strName As String
		Dim objNewMember As Parameter
		
		strName = Name
		On Error Resume Next
		Err.Clear()
        objNewMember = mCol.Item(strName)
        If Err.Number Then
			'UPGRADE_NOTE: Object objNewMember may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			objNewMember = Nothing
		End If
		On Error GoTo 0
		If objNewMember Is Nothing Then
			objNewMember = New Parameter
			objNewMember.Name = strName
			objNewMember.Value = Value
			
			Select Case ParType
                Case Parameter.eRmtDataType.rdbDouble, Parameter.eRmtDataType.rdbInteger, Parameter.eRmtDataType.rdbNumeric, Parameter.eRmtDataType.rdbSmallInt
                    If IsDBNull(Value) OrElse Value = eRemoteDB.Constants.intNull Then
                        'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
                        Value = System.DBNull.Value
                    End If
                Case Parameter.eRmtDataType.rdbVarchar, Parameter.eRmtDataType.rdbChar
                    If IsDBNull(Value) OrElse String.IsNullOrEmpty(Value) Then
                        'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
                        Value = System.DBNull.Value
                    End If
                Case IsDBNull(Value) OrElse Parameter.eRmtDataType.rdbDBTimeStamp, Parameter.eRmtDataType.rdbDBTime, Parameter.eRmtDataType.rdbDBTimeStamp
                    'UPGRADE_WARNING: IsEmpty was upgraded to IsNothing and has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
                    If IsNothing(Value) Then
                        'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
                        Value = System.DBNull.Value
                    End If

            End Select

            objNewMember.Direction = Direction
            'Se realiza conversión de SmallINT a Integer en possiblesValues para que sistema reconozca enteros mas grandes
            objNewMember.ParType = IIf(ParType = Parameter.eRmtDataType.rdbSmallInt, Parameter.eRmtDataType.rdbInteger, ParType)
            objNewMember.Size = Size
			objNewMember.NumericScale = NumericScale
			objNewMember.Precision = Precision
			objNewMember.Attributes = Attributes
			objNewMember.ParObject = ParObject
			mCol.Add(objNewMember, objNewMember.Name)
		Else
			objNewMember.Value = Value
		End If
		Add = objNewMember
		'UPGRADE_NOTE: Object objNewMember may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		objNewMember = Nothing
	End Function
	
	'% ReturnValue: se agrega un elemento a la colección de parámetros que devuelve el recorset
	Public Function ReturnValue(ByVal sName As String, Optional ByVal bVisible As Boolean = False, Optional ByVal sTitle As String = "", Optional ByVal bCreate As Boolean = False) As Parameter
		Dim objNewMember As Parameter
		objNewMember = New Parameter
		
		With objNewMember
			.Name = sName
			.VisibleColumn = bVisible
			.CreateColumn = bCreate
			.TitleColumn = sTitle
		End With
		
		mcolReturnValue.Add(objNewMember, objNewMember.Name)
		
		ReturnValue = objNewMember
		'UPGRADE_NOTE: Object objNewMember may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		objNewMember = Nothing
	End Function
	
	'* Item: toma un elemento de la colección
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Parameter
		Get
			On Error Resume Next
			Item = mCol.Item(vntIndexKey)
		End Get
	End Property
	
	'* Count: cuenta los elementos de la colección
	Public ReadOnly Property Count() As Integer
		Get
			Count = mCol.Count()
		End Get
	End Property
	
	'* NewEnum: enumera los elementos de la colección
	'UPGRADE_NOTE: NewEnum property was commented out. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="B3FC1610-34F3-43F5-86B7-16C984F0E88E"'
	'Public ReadOnly Property NewEnum() As stdole.IUnknown
		'Get
			'NewEnum = mCol._NewEnum
		'End Get
	'End Property
	
	Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
		'UPGRADE_TODO: Uncomment and change the following line to return the collection enumerator. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="95F9AAD0-1319-4921-95F0-B9D3C4FF7F1C"'
		GetEnumerator = mCol.GetEnumerator
	End Function
	
	'* Incomplete: Valida si el parámetro está incompleto
	ReadOnly Property Incomplete() As Boolean
		Get
			Dim objMember As Parameter
			
			Incomplete = False
			For	Each objMember In mCol
				If objMember.Incomplete Then
					Incomplete = True
					Exit Property
				End If
			Next objMember
			'UPGRADE_NOTE: Object objMember may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			objMember = Nothing
		End Get
	End Property
	
	'*Item_ReturnValue: Retorna un elemento de la colección de parametros que retorna un valor posible
	Public ReadOnly Property Item_ReturnValue(ByVal nIndexKey As Object) As Parameter
		Get
			Item_ReturnValue = mcolReturnValue.Item(nIndexKey)
		End Get
	End Property
	
	'*Count_ReturnValue: Retorna la cantidad de parametros que retorna un valor posible
	Public ReadOnly Property Count_ReturnValue() As Integer
		Get
			Count_ReturnValue = mcolReturnValue.Count()
		End Get
	End Property
	
	'* Remove: elimina un elemento de la colección
	Public Sub Remove(ByRef vntIndexKey As Object)
		mCol.Remove(vntIndexKey)
	End Sub
End Class






