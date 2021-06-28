Option Strict Off
Option Explicit On
Public Class Tab_Ul_Costss
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: Tab_Ul_Costss.cls                        $%'
	'% $Author:: Admin                                      $%'
	'% $Date:: 9/09/03 16:20                                $%'
	'% $Revision:: 6                                        $%'
	'%-------------------------------------------------------%'
	
	'+ Variable local para mantener la colección.
	
	Private mCol As Collection
	
	'**% Add: Adds a new instance of the Tab_Ul_Costs class to the collection.
	'% Add: Añade una nueva instancia de la clase Tab_Ul_Costs a la colección.
	Public Function Add(ByRef objClass As Tab_Ul_Costs) As Tab_Ul_Costs
		'+ Crea un nuevo proyecto.
		
		If objClass Is Nothing Then
			objClass = New Tab_Ul_Costs
		End If
		
		With objClass
			mCol.Add(objClass, .nBranch & .nProduct & .nMonth_from & .nMonth_until & .nCost_amount & .nCurrency & .nType_cost & .nRate & .nMax_amou)
		End With
		
		'+ Retorna el objeto creado.
		
		Add = objClass
		'UPGRADE_NOTE: Object objClass may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		objClass = Nothing
	End Function
	
	'*** Item: Returns an element of the collection (acording to the index)
	'* Item: Devuelve un elemento de la colección (segun índice)
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Tab_Ul_Costs
		Get
			'**+ Used when referencing an element in the collection vntIndexKey contains either the Index
			'**+ or Key to the collection, this is why it is declared as a Variant Syntax: Set foo = x.Item(xyz)
			'**+ or Set foo = x.Item(5).
			
			Item = mCol.Item(vntIndexKey)
		End Get
	End Property
	
	'*** Count: Returns the number of elements that the collection has
	'* Count: Devuelve el número de elementos que posee la colección
	Public ReadOnly Property Count() As Integer
		Get
			'**+ Used when retrieving the number of elements in the collection. Syntax: Debug.Print x.Count.
			
			Count = mCol.Count()
		End Get
	End Property
	
	'*** NewEnum: Enumerates the collection for use in a For Each...Next loop
	'* NewEnum: Permite enumerar la colección para utilizarla en un ciclo For Each... Next
	'UPGRADE_NOTE: NewEnum property was commented out. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="B3FC1610-34F3-43F5-86B7-16C984F0E88E"'
	'Public ReadOnly Property NewEnum() As stdole.IUnknown
		'Get
			'**+ This property allows you to enumerate this collection with the For...Each syntax.
			'
			'NewEnum = mCol._NewEnum
		'End Get
	'End Property
	
	Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
		'UPGRADE_TODO: Uncomment and change the following line to return the collection enumerator. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="95F9AAD0-1319-4921-95F0-B9D3C4FF7F1C"'
		GetEnumerator = mCol.GetEnumerator
	End Function
	
	'**% Remove: Deletes an element from the collection
	'% Remove: Elimina un elemento de la colección.
	Public Sub Remove(ByRef vntIndexKey As Object)
		'**+ Used when removing an element from the collection vntIndexKey contains either the Index or
		'**+ Key, which is why it is declared as a Variant Syntax: x.Remove(xyz).
		
		mCol.Remove(vntIndexKey)
	End Sub
	
	'**% Class_Initialize: Controls the creation of an instance of the collection
	'% Class_Initialize: Controla la creación de una instancia de la colección
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		'**+ Creates the collection when this class is created.
		
		mCol = New Collection
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'**%Class_Terminate: Controls the destruction of an instance of the collection
	'%Class_Terminate: Controla la destrucción de una instancia de la colección
	'UPGRADE_NOTE: Class_Terminate was upgraded to Class_Terminate_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Terminate_Renamed()
		'**+ Destroys collection when this class is terminated.
		
		'UPGRADE_NOTE: Object mCol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mCol = Nothing
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
	
	'% Find: Devuelve una colección de objetos de tipo Tab_Ul_Costs.
	Public Function Find(ByVal nBranch As Integer, ByVal nProduct As Integer) As Boolean
		'- Se define la variable lrecReaTab_Ul_Costs que se utilizará como cursor.
		
		Dim lrecReaTab_Ul_Costs As eRemoteDB.Execute
		Dim lclsTab_Ul_Costs As Tab_Ul_Costs
		
		On Error GoTo Find_Err
		
		lrecReaTab_Ul_Costs = New eRemoteDB.Execute
		
		'+ Se ejecuta el Store Procedure que busca los movimientos de la tabla de costos fijos de APV.
		'+ Definición de parámetros para stored procedure 'insudb.reaTab_Ul_Costs'
		'+ Información leída el 16/12/2002 02:32:15 pm.
		
		With lrecReaTab_Ul_Costs
			.StoredProcedure = "reaTab_Ul_Costs"
			
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				Find = True
				
				Do While Not .EOF
					lclsTab_Ul_Costs = New Tab_Ul_Costs
					
					lclsTab_Ul_Costs.nBranch = .FieldToClass("nBranch")
					lclsTab_Ul_Costs.nProduct = .FieldToClass("nProduct")
					lclsTab_Ul_Costs.nMonth_from = .FieldToClass("nMonth_from")
					lclsTab_Ul_Costs.nMonth_until = .FieldToClass("nMonth_until")
					lclsTab_Ul_Costs.nCost_amount = .FieldToClass("nCost_amount")
					lclsTab_Ul_Costs.nCurrency = .FieldToClass("nCurrency")
					lclsTab_Ul_Costs.sCreDeb = .FieldToClass("sCreDeb")
					lclsTab_Ul_Costs.nType_cost = .FieldToClass("nType_cost")
					lclsTab_Ul_Costs.nMax_amou = .FieldToClass("nMax_amou")
					lclsTab_Ul_Costs.nRate = .FieldToClass("nRate")
					
					Call Add(lclsTab_Ul_Costs)
					
					'UPGRADE_NOTE: Object lclsTab_Ul_Costs may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					lclsTab_Ul_Costs = Nothing
					
					.RNext()
				Loop 
			Else
				Find = False
			End If
		End With
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		
		'UPGRADE_NOTE: Object lrecReaTab_Ul_Costs may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecReaTab_Ul_Costs = Nothing
		
		On Error GoTo 0
	End Function
End Class






