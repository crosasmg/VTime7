Option Strict Off
Option Explicit On
Public Class MasiveCharges
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: MasiveCharges.cls                        $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 28/08/03 6:25p                               $%'
	'% $Revision:: 14                                       $%'
	'%-------------------------------------------------------%'
	
	Private mCol As Collection
	Public nError As Short
	
	'% Add: Añade una nueva instancia de la clase MasiveCharge
	Public Function Add(ByVal objClass As MasiveCharge) As MasiveCharge
		
		If objClass Is Nothing Then
			objClass = New MasiveCharge
		End If
		
		With objClass
            mCol.Add(objClass)
		End With
		
		'return the object created
		Add = objClass
	End Function
	
	'% Add_Inconsist: Añade una nueva instancia de la clase MasiveCharge a la colección
	Public Function Add_Inconsist(ByVal objClass As MasiveCharge) As MasiveCharge
		
		If objClass Is Nothing Then
			objClass = New MasiveCharge
		End If
		
		With objClass
			mCol.Add(objClass, .sKey & .sTable & .sField & .sValue)
		End With
		
		'return the object created
		Add_Inconsist = objClass
	End Function
	
	
	'* Item: se instancia un elemento de la colección
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As MasiveCharge
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
	Public Function Find(ByVal sKey As String, ByVal nRow As Integer) As Boolean
		Dim lrecReaMasiveCharge As eRemoteDB.Execute
		Dim lobjMasiveCharge As MasiveCharge
		On Error GoTo Find_Err
		mCol = New Collection
		lrecReaMasiveCharge = New eRemoteDB.Execute
		
		'Definición de parámetros para stored procedure 'insudb.DelMasiveCharge'
		
		With lrecReaMasiveCharge
			.StoredProcedure = "ReaT_MasiveCharge"
			.Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRow", nRow, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Find = True
				Do While Not .EOF
					lobjMasiveCharge = New MasiveCharge
					lobjMasiveCharge.sKey = .FieldToClass("sKey")
					lobjMasiveCharge.nRows = .FieldToClass("nRows")
					lobjMasiveCharge.nColumns = .FieldToClass("nColumns")
					lobjMasiveCharge.sField = .FieldToClass("sField")
					lobjMasiveCharge.sValue = .FieldToClass("sValue")
					lobjMasiveCharge.nSearch = .FieldToClass("nSearch")
					lobjMasiveCharge.sTable = .FieldToClass("sTable")
					lobjMasiveCharge.sValuesList = .FieldToClass("sValuesList")
					Call Add(lobjMasiveCharge)
					.RNext()
				Loop 
				.RCloseRec()
			Else
				Find = False
			End If
			
		End With
		
		'UPGRADE_NOTE: Object lrecReaMasiveCharge may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecReaMasiveCharge = Nothing
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
	End Function
	
	'% FindInconsist: Se buscan las Inconsistencias de la Tabla Temporal
	Public Function FindInconsist(ByVal sKey As String, ByVal nUsercode As Integer, ByVal nSheet As Short) As Boolean
		
		Dim lrecInconsist As eRemoteDB.Execute
		Dim lobjMasiveCharge As MasiveCharge
		
		On Error GoTo FindInconsist_Err
		
		mCol = New Collection
		lrecInconsist = New eRemoteDB.Execute
		
		'Definición de parámetros para stored procedure 'insudb.DelMasiveCharge'
		
		With lrecInconsist
			.StoredProcedure = "INSREAT_INCONSIST"
			.Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nSheet", nSheet, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				FindInconsist = True
				Do While Not .EOF
					lobjMasiveCharge = New MasiveCharge
					lobjMasiveCharge.sKey = sKey
					lobjMasiveCharge.sField = .FieldToClass("sField")
					lobjMasiveCharge.sValue = .FieldToClass("sValue")
					lobjMasiveCharge.sTable = .FieldToClass("sTable")
					lobjMasiveCharge.sValuesList = .FieldToClass("sValueList")
					lobjMasiveCharge.sFieldName = .FieldToClass("sColumnname")
					lobjMasiveCharge.nUsercode = nUsercode
					Call Add_Inconsist(lobjMasiveCharge)
					nError = .FieldToClass("nErrornum")
					.RNext()
				Loop 
				.RCloseRec()
			Else
				FindInconsist = False
			End If
			
		End With
		
		'UPGRADE_NOTE: Object lrecInconsist may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecInconsist = Nothing
		
FindInconsist_Err: 
		If Err.Number Then
			FindInconsist = False
		End If
	End Function
End Class






