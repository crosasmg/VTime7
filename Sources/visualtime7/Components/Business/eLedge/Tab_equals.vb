Option Strict Off
Option Explicit On
Public Class Tab_equals
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: Tab_equals.cls                           $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:18p                                $%'
	'% $Revision:: 5                                        $%'
	'%-------------------------------------------------------%'
	
	'*+Local variable to hold collection.
	'+ Variable Local para almacenar la colección.
	
	Private mcolTab_equal As Collection
	
	'*%Add: It adds an element to the collection.
	'% Add: Agrega un elemento a la colección.
	Public Function Add(ByRef lclsTab_equal As Tab_equal) As Tab_equal
		
		'set the properties passed into the method
		mcolTab_equal.Add(lclsTab_equal)
		
		'return the object created
		Add = lclsTab_equal
		'UPGRADE_NOTE: Object lclsTab_equal may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsTab_equal = Nothing
	End Function
	
	'*%Find: Function that makes the search in the table 'Tab_equal'.
	'% Find: Función que realiza la busqueda en la tabla 'Tab_equal'.
	Public Function Find(ByVal nLed_compan As Integer, ByVal nTypecode As Integer) As Boolean
		Dim lclsTab_equal As eRemoteDB.Execute
		Dim lclsTab_equalItem As Tab_equal
		
		On Error GoTo Find_Err
		
		lclsTab_equal = New eRemoteDB.Execute
		
		'+ Define all parameters for the stored procedures 'insudb.reaTab_equal'. Generated on 08/10/2002 09:45:23 a.m.
		With lclsTab_equal
			.StoredProcedure = "reaTab_equal_a"
			.Parameters.Add("nLed_compan", nLed_compan, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTypecode", nTypecode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(True) Then
				Do While Not .EOF
					lclsTab_equalItem = New Tab_equal
					lclsTab_equalItem.nLed_compan = .FieldToClass("nLed_compan")
					lclsTab_equalItem.nTypecode = .FieldToClass("nTypecode")
					lclsTab_equalItem.sCodeVisual = .FieldToClass("sCodeVisual")
					lclsTab_equalItem.sCodeAsi = .FieldToClass("sCodeAsi")
					lclsTab_equalItem.sDescript = .FieldToClass("sDescript")
					Call Add(lclsTab_equalItem)
					'UPGRADE_NOTE: Object lclsTab_equalItem may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					lclsTab_equalItem = Nothing
					.RNext()
				Loop 
				Find = True
				.RCloseRec()
				
			Else
				Find = False
			End If
		End With
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		
		'UPGRADE_NOTE: Object lclsTab_equal may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsTab_equal = Nothing
		
		On Error GoTo 0
		
	End Function
	
	'*%Item: This property is used when reference to an element becomes of the collection.
	'% Item: Esta propiedad es usada cuando se hace referencia a un elemento de la colección.
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Tab_equal
		Get
			Item = mcolTab_equal.Item(vntIndexKey)
		End Get
	End Property
	
	'*%Count: It returns the amount of existing elements in the collection.
	'% Count: Retorna la contidad de elementos existentes en la colección.
	Public ReadOnly Property Count() As Integer
		Get
			Count = mcolTab_equal.Count()
		End Get
	End Property
	
	'*%NewEnum: This property allows you to enumerate this collection with the "For...Each".
	'% NewEnum: Esta propiedad permite enumerar los elementos de la colección por medio de un "For...Each".
	'UPGRADE_NOTE: NewEnum property was commented out. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="B3FC1610-34F3-43F5-86B7-16C984F0E88E"'
	'Public ReadOnly Property NewEnum() As stdole.IUnknown
		'Get
			'NewEnum = mcolTab_equal._NewEnum
		'End Get
	'End Property
	
	Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
		'UPGRADE_TODO: Uncomment and change the following line to return the collection enumerator. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="95F9AAD0-1319-4921-95F0-B9D3C4FF7F1C"'
		GetEnumerator = mcolTab_equal.GetEnumerator
	End Function
	
	'*%Remove: It allows to remove an element of the collection.
	'% Remove: Permite eliminar un elemento de la colección.
	Public Sub Remove(ByRef vntIndexKey As Object)
		mcolTab_equal.Remove(vntIndexKey)
	End Sub
	
	'*%Class_Initialize: Creates the collection when this class is created.
	'% Class_Initialize: Crea la colección cuando se crea esta clase.
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		mcolTab_equal = New Collection
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'*%Class_Terminate: Destroys collection when this class is terminated.
	'% Class_Terminate: Destruye la colección cuando se termina esta clase.
	'UPGRADE_NOTE: Class_Terminate was upgraded to Class_Terminate_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Terminate_Renamed()
		'UPGRADE_NOTE: Object mcolTab_equal may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mcolTab_equal = Nothing
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
End Class






