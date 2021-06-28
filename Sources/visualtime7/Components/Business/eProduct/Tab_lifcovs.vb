Option Strict Off
Option Explicit On
Public Class Tab_lifcovs
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: Tab_lifcovs.cls                          $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 12:36p                               $%'
	'% $Revision:: 7                                        $%'
	'%-------------------------------------------------------%'
	
	'**- Local variable to contein the collection
	'- Variable local para contener colección
	
	Private mCol As Collection
	
	'**-Define the auxiliary properties of the transaction DP039 - Generic coverages consult.
	'- Se definen las propiedades auxiliares de la transacción DP039 - Consulta de coberturas genéricas.
	
	Private mintCurrency As Integer
	Private mstrCondition As String
	
	'**% Add: add a new element to the collection.
	'% Add: Añade un nuevo elemento a la colección.
	Public Function Add(ByRef nCovergen As Integer, ByRef sDescript As String) As Tab_lifcov
		'**+ create a new object
		Dim objNewMember As eProduct.Tab_lifcov
		If mCol Is Nothing Then
			mCol = New Collection
		End If
		
		'**+ Set the properties passed into the method
		objNewMember = New eProduct.Tab_lifcov
		With objNewMember
			.nCovergen = nCovergen
			.sDescript = sDescript
		End With
		
		mCol.Add(objNewMember)
		
		'**+ Return the created object
		Add = objNewMember
		'UPGRADE_NOTE: Object objNewMember may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		objNewMember = Nothing
	End Function
	
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Tab_lifcov
		Get
			'**+ Is use to make reference to one element of the collection
			'**+vntIndexKey that contein the index or the collection key
			'**+ so is declared as a Variant Syntax: Set foo = x.Item(xyz) or Set foo = x.Item (5).
			'+ Se usa al hacer referencia a un elemento de la colección
			'+ vntIndexKey contiene el índice o la clave de la colección,
			'+ por lo que se declara como un Variant Syntax: Set foo = x.Item(xyz) or Set foo = x.Item(5).
			
			Item = mCol.Item(vntIndexKey)
		End Get
	End Property
	
	Public ReadOnly Property Count() As Integer
		Get
			'**+ Is use when is obteined the elements number of the collection. Sintaxis: Depug.Print x.Count.
			'+ Se usa al obtener el número de elementos de la colección. Sintaxis: Debug.Print x.Count.
			Count = mCol.Count()
		End Get
	End Property
	
	'UPGRADE_NOTE: NewEnum property was commented out. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="B3FC1610-34F3-43F5-86B7-16C984F0E88E"'
	'Public ReadOnly Property NewEnum() As stdole.IUnknown
		'Get
			'**+ This property permit to enumerate this collection
			'**+with the sintaxis For...Each
			'+ esta propiedad permite enumerar
			'+ esta colección con la sintaxis For...Each
			'
			'NewEnum = mCol._NewEnum
		'End Get
	'End Property
	
	Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
		'UPGRADE_TODO: Uncomment and change the following line to return the collection enumerator. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="95F9AAD0-1319-4921-95F0-B9D3C4FF7F1C"'
		GetEnumerator = mCol.GetEnumerator
	End Function
	
	Public Sub Remove(ByRef vntIndexKey As Object)
		'**+ Is use to remove one element from the collection
		'**+vntIndexKey contein the index or the key so is declare
		'**+ as a Variant Sintaxis: x.Remove(xyz).
		'+ se usa al quitar un elemento de la colección
		'+ vntIndexKey contiene el índice o la clave, por lo que se
		'+ declara como un Variant Sintaxis: x.Remove(xyz).
		
		mCol.Remove(vntIndexKey)
	End Sub
	
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		'**+ Create the collection when the class is created.
		'+ Crea la colección cuando se crea la clase.
		
		mCol = New Collection
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'UPGRADE_NOTE: Class_Terminate was upgraded to Class_Terminate_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Terminate_Renamed()
		'**+ Destroy the collection when the class is finished
		'+ Destruye la colección cuando se termina la clase.
		
		'UPGRADE_NOTE: Object mCol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mCol = Nothing
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
	
	'**% Find: Permit to make the reading of the generic coverages when the option
	'**%is "Life" in the DP039 window - Generic coverages consult
	'% Find: Permite realizar la lectura de las coberturas genéricas cuando la opción
	'% es "Vida" en la ventana DP039 - COnsulta de coberturas genéricas.
	Public Function Find(ByVal nCurrency As Integer, ByVal sCondition As String, Optional ByVal lblnFind As Boolean = True) As Boolean
		Dim lrecTab_lifcov As eRemoteDB.Execute
		lrecTab_lifcov = New eRemoteDB.Execute
		On Error GoTo Find_Err
		Find = True
		If nCurrency <> mintCurrency Or sCondition <> mstrCondition Or lblnFind Then
			'UPGRADE_NOTE: Object mCol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			mCol = Nothing
			mCol = New Collection
			'**+ Parameters definition for the stored procedure  'insudb.reaAllCovergen'.
			'+ Definición de parámetros para stored procedure 'insudb.reaAllCovergen'.
			With lrecTab_lifcov
				.StoredProcedure = "reaTab_lifcov_2"
				.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("sCondition", sCondition, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If .Run Then
					mintCurrency = nCurrency
					mstrCondition = sCondition
					Do While Not .EOF
						Call Add(.FieldToClass("nCovergen"), .FieldToClass("sDescript"))
						.RNext()
					Loop 
					.RCloseRec()
				Else
					Find = False
					mintCurrency = 0
					mstrCondition = CStr(Nothing)
				End If
			End With
		End If
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		'UPGRADE_NOTE: Object lrecTab_lifcov may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecTab_lifcov = Nothing
		On Error GoTo 0
	End Function
End Class






