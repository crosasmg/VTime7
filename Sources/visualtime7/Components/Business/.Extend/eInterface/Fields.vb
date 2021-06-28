Option Strict Off
Option Explicit On
Public Class Fields
	Implements System.Collections.IEnumerable
	
	Private mCol As Collection
	Public smessage As String
	
	'% Add: Añade una nueva instancia de la clase Field
	Public Function Add(ByVal objClass As Field) As Field
		
		If objClass Is Nothing Then
			objClass = New Field
		End If
		
		With objClass
			mCol.Add(objClass, .sKey & .nRow & .nColumn & .sTable & .sField)
		End With
		
		'return the object created
		Add = objClass
	End Function
	
	'% Add_Inconsist: Añade una nueva instancia de la clase MasiveCharge a la colección
	Public Function Add_Inconsist(ByVal objClass As Field) As Field
		
		If objClass Is Nothing Then
			objClass = New Field
		End If
		
		With objClass
			mCol.Add(objClass, .sKey & .sTable & .sField & .sValue)
		End With
		
		'return the object created
		Add_Inconsist = objClass
	End Function
	
	
	'* Item: se instancia un elemento de la colección
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Field
		Get
			'used when referencing an element in the collection
			'vntIndexKey contains either the Index or Key to the collection,
			'this is why it is declared as a Variant
			'Syntax: Set foo = x.Item(xyz) or Set foo = x.Item(5)
			Item = mCol.Item(vntIndexKey)
		End Get
	End Property
	
	'* Count: devuelve el Nro. de elementos que tiene la colección
	Public ReadOnly Property Count() As Integer
		Get
			'used when retrieving the number of elements in the
			'collection. Syntax: Debug.Print x.Count
			Count = mCol.Count()
		End Get
	End Property
	
	'* NewEnum: permite recorrer los elementos de la colección
	'UPGRADE_NOTE: NewEnum property was commented out. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="B3FC1610-34F3-43F5-86B7-16C984F0E88E"'
	'Public ReadOnly Property NewEnum() As stdole.IUnknown
		'Get
			'this property allows you to enumerate
			'this collection with the For...Each syntax
			'NewEnum = mCol._NewEnum
		'End Get
	'End Property
	
	Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
		'UPGRADE_TODO: Uncomment and change the following line to return the collection enumerator. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="95F9AAD0-1319-4921-95F0-B9D3C4FF7F1C"'
		GetEnumerator = mCol.GetEnumerator
	End Function
	
	'* Remove: elimina un elemento de la colección
	Public Sub Remove(ByRef vntIndexKey As Object)
		'used when removing an element from the collection
		'vntIndexKey contains either the Index or Key, which is why
		'it is declared as a Variant
		'Syntax: x.Remove(xyz)
		
		
		mCol.Remove(vntIndexKey)
	End Sub
	
	
	'* Class_Initialize: se controla la creación de la colección
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		'creates the collection when this class is created
		mCol = New Collection
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	
	'* Class_Terminate: se controla la destrucción de la colección
	'UPGRADE_NOTE: Class_Terminate was upgraded to Class_Terminate_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Terminate_Renamed()
		'destroys collection when this class is terminated
		'UPGRADE_NOTE: Object mCol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mCol = Nothing
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
	
	'% Find: se buscan los elementos asociados a una tabla temporal
	Public Function Find(ByVal sKey As String, ByVal nRow As Integer, Optional ByVal nType As Integer = 0) As Boolean
		Dim lrecReafield As Object
		Dim lrecReaInterface As eRemoteDB.Execute
		Dim lobjField As Field
		Dim nFieldType As Integer
		On Error GoTo Find_Err
		mCol = New Collection
		lrecReaInterface = New eRemoteDB.Execute
		
		nFieldType = nType
		If nFieldType <= 0 Then
			nFieldType = 2
		End If
		
		'Definición de parámetros para stored procedure 'insudb.ReaT_Interface'
		
		With lrecReaInterface
			.StoredProcedure = "ReaT_Interface"
			.Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRow", nRow, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nFieldType", nFieldType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Find = True
				Do While Not .EOF
					lobjField = New Field
					lobjField.sKey = .FieldToClass("sKey")
					lobjField.nRow = .FieldToClass("nRow")
					lobjField.nColumn = .FieldToClass("nColumn")
					lobjField.sField = .FieldToClass("sField")
					lobjField.sValue = .FieldToClass("sValue")
					lobjField.sTable = .FieldToClass("sTable")
					lobjField.sProcess = .FieldToClass("sProcess")
					Call Add(lobjField)
					.RNext()
				Loop 
				.RCloseRec()
			Else
				Find = False
			End If
			
		End With
		
		
		
Find_Err: 
		If Err.Number Then
			Find = False
			Me.smessage = "Error=" & lrecReaInterface.ErrorMsg
		End If
		'UPGRADE_NOTE: Object lrecReafield may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecReafield = Nothing
		'UPGRADE_NOTE: Object lrecReaInterface may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecReaInterface = Nothing
	End Function
	
	''% FindInconsist: Se buscan las Inconsistencias de la Tabla Temporal
	''-----------------------------------------------------------------------------------------------------------------------
	'Public Function FindInconsist(ByVal sKey As String, _
	''                              ByVal nUsercode As Long) As Boolean
	''-----------------------------------------------------------------------------------------------------------------------
	'
	'    Dim lrecInconsist As eRemotedb.Execute
	'    Dim lobjField     As Field
	'
	'    On Error GoTo FindInconsist_Err
	'
	'    Set mCol = New Collection
	'    Set lrecInconsist = New eRemotedb.Execute
	'
	''Definición de parámetros para stored procedure 'insudb.DelMasiveCharge' aqui
	'
	'    With lrecInconsist
	'        .StoredProcedure = "INSREAT_INCONSIST"
	'        .Parameters.Add "sKey", sKey, rdbParamInput, rdbVarChar, 20, 0, 0, rdbParamNullable
	'        .Parameters.Add "nUsercode", nUsercode, rdbParamInput, rdbInteger, 22, 0, 10, rdbParamNullable
	'        If .Run Then
	'            FindInconsist = True
	'            Do While Not .EOF
	'                Set lobjField = New Field
	'                lobjField.sKey = sKey
	'                lobjField.sField = .FieldToClass("sField")
	'                lobjField.sValue = .FieldToClass("sValue")
	'                lobjField.sTable = .FieldToClass("sTable")
	'                lobjField.sProcess = .FieldToClass("sProcess") aquiiiiiiii
	'                lobjField.sFieldName = .FieldToClass("sColumnname")
	'                lobjField.nUsercode = nUsercode
	'                Call Add_Inconsist(lobjField)
	'                .RNext
	'            Loop
	'            .RCloseRec
	'        Else
	'            FindInconsist = False
	'        End If
	'
	'    End With
	'
	'    Set lrecInconsist = Nothing
	'
	'FindInconsist_Err:
	'    If Err Then
	'        FindInconsist = False
	'    End If
	'End Function
End Class






