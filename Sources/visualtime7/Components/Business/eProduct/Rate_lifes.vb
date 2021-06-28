Option Strict Off
Option Explicit On
Public Class Rate_lifes
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: Rate_lifes.cls                           $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 12:36p                               $%'
	'% $Revision:: 8                                        $%'
	'%-------------------------------------------------------%'
	
	'**- Local variable to hold collection
	Private mCol As Collection
	
	'**- Defined the auxiliary properties
	'- Se definen las propiedades auxiliares.
	Private mlngBranch As Integer
	Private mlngProduct As Integer
	Private mlngCover As Integer
	Private mdtmEffecdate As Date
	
	'**% Add: adds a new instance of the "Rate_life" class to the collection
	'% Add: Añade una nueva instancia de la clase "Rate_life" a la colección
	Public Function Add(ByRef nStatusInstance As Integer, ByRef nBranch As Integer, ByRef nProduct As Integer, ByRef nCover As Integer, ByRef dEffecdate As Date, ByRef nAgeStart As Integer, ByRef nAgeEnd As Integer, ByRef nRatepure As Double, ByRef nRatenoni As Double, ByRef nRatenive As Double, ByRef nUsercode As Integer) As Rate_life
		'+ Create a new object.
		Dim objNewMember As eProduct.Rate_life
		objNewMember = New eProduct.Rate_life
		
		If mCol Is Nothing Then
			mCol = New Collection
		End If
		
		'+ Set the properties passed into the method.
		With objNewMember
			.nStatusInstance = nStatusInstance
			.nBranch = nBranch
			.nProduct = nProduct
			.nCover = nCover
			.dEffecdate = dEffecdate
			.nAgeStart = nAgeStart
			.nAgeEnd = nAgeEnd
			.nRatepure = nRatepure
			.nRatenoni = nRatenoni
			.nRatenive = nRatenive
			.nUsercode = nUsercode
		End With
		
		mCol.Add(objNewMember, "A" & CStr(nBranch) & CStr(nProduct) & CStr(nCover) & CStr(dEffecdate) & CStr(nAgeStart) & CStr(nAgeEnd))
		
		'+ Return the object created.
		Add = objNewMember
		'UPGRADE_NOTE: Object objNewMember may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		objNewMember = Nothing
	End Function
	
	'**% Item: restores one element of the collection (accourding to the index)
	'% Item: Devuelve un elemento de la colección (segun índice)
	Public ReadOnly Property Item(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nCover As Integer, ByVal dEffecdate As Date, ByVal nAgeStart As Integer, ByVal nAgeEnd As Integer) As Rate_life
		Get
			Item = mCol.Item("A" & CStr(nBranch) & CStr(nProduct) & CStr(nCover) & CStr(dEffecdate) & CStr(nAgeStart) & CStr(nAgeEnd))
		End Get
	End Property
	
	'**% Count: reatores the number of elements that the collection owns
	'% Count: Devuelve el número de elementos que posee la colección
	Public ReadOnly Property Count() As Integer
		Get
			Count = mCol.Count()
		End Get
	End Property
	
	'**% NewEnum: Allows to enumerate the collection for using it in a cycle For Each... Next
	'% NewEnum: Permite enumerar la colección para utilizarla en un ciclo For Each... Next
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
	
	'**% Remove: deletes one element of the collection
	'% Remove: Elimina un elemento de la colección
	Public Sub Remove(ByRef vntIndexKey As Object)
		mCol.Remove(vntIndexKey)
	End Sub
	
	'**% Class_Initialize: Controls the creation of an instance of the collection
	'% Class_Initialize: Controla la creación de una instancia de la colección
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		mCol = New Collection
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'**% Class_Terminate: Controls the delete of one instance of the collection
	'% Class_Terminate: Controla la destrucción de una instancia de la colección
	'UPGRADE_NOTE: Class_Terminate was upgraded to Class_Terminate_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Terminate_Renamed()
		'UPGRADE_NOTE: Object mCol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mCol = Nothing
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
	
	'**% Find: This method fills the collection with records from the table "Rate_life" returning TRUE or FALSE
	'**% depending on the existence of the records
	'% Find: Este metodo carga la coleccion de elementos de la tabla "Rate_life" devolviendo Verdadero o
	'% falso, dependiendo de la existencia de los registros.
	Public Function Find(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nCover As Integer, ByVal dEffecdate As Date, Optional ByVal lblnFind As Boolean = True) As Boolean
		Dim lrecRate_life As eRemoteDB.Execute
		
		lrecRate_life = New eRemoteDB.Execute
		
		On Error GoTo Find_Err
		
		Find = True
		
		If nBranch <> mlngBranch Or nProduct <> mlngProduct Or nCover <> mlngCover Or dEffecdate <> mdtmEffecdate Or lblnFind Then
			'UPGRADE_NOTE: Object mCol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			mCol = Nothing
			mCol = New Collection
			
			'**+ Parameters definition for the stored procedure insudb.reaRate_life'.
			'+ Definición de parámetros para stored procedure 'insudb.reaRate_life'.
			With lrecRate_life
				.StoredProcedure = "reaRate_lifePKG.reaRate_life"
				
				.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
				.Parameters.Add("nAgeStart", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
				.Parameters.Add("nAgeEnd", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				
				If .Run Then
					mlngBranch = nBranch
					mlngProduct = nProduct
					mlngCover = nCover
					mdtmEffecdate = dEffecdate
					
					Do While Not .EOF
						Call Add(0, .FieldToClass("nBranch"), .FieldToClass("nProduct"), .FieldToClass("nCover"), .FieldToClass("dEffecdate"), .FieldToClass("nAgeStart"), .FieldToClass("nAgeEnd"), .FieldToClass("nRatepure"), .FieldToClass("nRatenoni"), .FieldToClass("nRatenive"), .FieldToClass("nUsercode"))
						.RNext()
					Loop 
					
					.RCloseRec()
				Else
					Find = False
					
					mlngBranch = 0
					mlngProduct = 0
					mlngCover = 0
					mdtmEffecdate = CDate(Nothing)
				End If
			End With
			
			'UPGRADE_NOTE: Object lrecRate_life may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			lrecRate_life = Nothing
		End If
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
	End Function
End Class






