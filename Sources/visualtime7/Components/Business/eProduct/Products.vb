Option Strict Off
Option Explicit On
Public Class Products
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: Products.cls                             $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 12:36p                               $%'
	'% $Revision:: 8                                        $%'
	'%-------------------------------------------------------%'
	
	'**- Local variable to contein the collection
	'- Variable local para contener colección
	
	Private mCol As Collection
	
	'**- Auxiliary properties of the transaction DP002 - Product of a commercial branch
	'- Propiedades auxiliares de la transacción DP002 - Productos de un ramo comercial.
	
	Private mintBranch As Integer
	Private mdtmEffecdate As Date
	
	'**%Add: adds a new instance of the "ProdMaster_a" class to the collection
	'%Add: Añade una nueva instancia de la clase "ProdMaster_a" a la colección
	Public Function Add(ByRef nStatusInstance As Integer, ByRef dEffecdate As Date, ByRef nBranch As Integer, ByRef nProduct As Integer, ByRef sDescript As String, ByRef sShort_des As String, ByRef sStatregt As String, ByRef nUsercode As Integer, ByRef sBrancht As String) As Product
		Dim objNewMember As Product
		
		objNewMember = New Product
		
		If mCol Is Nothing Then
			mCol = New Collection
		End If
		
		With objNewMember
			.nStatusInstance = nStatusInstance
			.dEffecdate = dEffecdate
			.nBranch = nBranch
			.nProduct = nProduct
			.sDescript = sDescript
			.sShort_des = sShort_des
			.sStatregt = CShort(sStatregt)
			.nUsercode = nUsercode
			.sBrancht = CShort(sBrancht)
		End With
		
		mCol.Add(objNewMember, "A" & nBranch & nProduct)
		
		Add = objNewMember
		'UPGRADE_NOTE: Object objNewMember may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		objNewMember = Nothing
	End Function
	
	'**% Find: read the commercial branch products
	'% Find: Permite realizar la lectura de los productos de un ramo comercial.
	Public Function Find(ByVal nBranch As Integer, ByVal dEffecdate As Date, Optional ByVal lblnFind As Boolean = True) As Boolean
		Dim lrecProdmaster As eRemoteDB.Execute
		
		lrecProdmaster = New eRemoteDB.Execute
		
		On Error GoTo Find_Err
		
		Find = True
		
		If nBranch <> mintBranch Or dEffecdate <> mdtmEffecdate Or lblnFind Then
			'UPGRADE_NOTE: Object mCol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			mCol = Nothing
			mCol = New Collection
			
			'**+ Parameters definition for the stored procedure 'insudb.reaProdMaster_a'.
			'+ Definición de parámetros para stored procedure 'insudb.reaProdMaster_a'.
			With lrecProdmaster
				.StoredProcedure = "reaProdMaster_a"
				.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				
				If .Run Then
					mintBranch = nBranch
					mdtmEffecdate = dEffecdate
					
					Do While Not .EOF
						Call Add(0, .FieldToClass("dEffecdate"), .FieldToClass("nBranch", 0), .FieldToClass("nProduct", 0), .FieldToClass("sDescript"), .FieldToClass("sShort_des"), .FieldToClass("sStatregt"), .FieldToClass("nUsercode", 0), .FieldToClass("sBrancht"))
						.RNext()
					Loop 
					
					.RCloseRec()
				Else
					Find = False
					
					mintBranch = 0
					mdtmEffecdate = CDate(Nothing)
				End If
			End With
			
			'UPGRADE_NOTE: Object lrecProdmaster may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			lrecProdmaster = Nothing
		End If
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
	End Function
	
	'**% Item: restores one element of the collection (accourding to the index)
	'% Item: Devuelve un elemento de la colección (segun índice)
	Default Public ReadOnly Property Item(ByVal nBranch As Integer, ByVal nProduct As Integer) As Product
		Get
			Item = mCol.Item("A" & nBranch & nProduct)
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
	
	'**% Update: makes the treatment of each instance of the class in the collection
	'% Update: Realiza el tratamiento de cada instancia de la clase en la colección.
	Public Function Update() As Boolean
		Dim lclsProduct As eProduct.Product
		Dim lclsProduct_ge As eProduct.Product_ge
		Dim lcolAux As Collection
		
		On Error GoTo Update_Err
		
		Update = True
		
		lcolAux = New Collection
		
		For	Each lclsProduct In mCol
			With lclsProduct
				Select Case .nStatusInstance
					
					'**+ If the action is Add
					'+ Si la acción es Agregar.
					Case 1
						Update = .UpdateProduct()
						
						'**+ If is not Life
						'+ Si no es vida.
						If (CStr(.sBrancht) <> "1") Then
							lclsProduct_ge = New eProduct.Product_ge
							
							With lclsProduct_ge
								.nBranch = lclsProduct.nBranch
								.nProduct = lclsProduct.nProduct
								.dEffecdate = lclsProduct.dEffecdate
								
								Update = .Update()
							End With
							
							'UPGRADE_NOTE: Object lclsProduct_ge may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
							lclsProduct_ge = Nothing
						End If
						
						'**+ If is Life or is one combinated
						'+ Si es vida o es un combinado
						If (.sBrancht = 1 Or .sBrancht = 5 Or .sBrancht = 6) Then
							If Not .FindProduct_li(.nBranch, .nProduct, .dEffecdate, True) Then
								Update = .AddProduct_li
							ElseIf .dEffecdate = .dEffecdateProduct_li Then 
								Update = .UpdateProduct_Li
								
								'**+ If the modification is a posterior date then is null the existance record and create a new one.
								'+ Si la modificación es una fecha posterior, se anula el registro existente y se crea un nuevo.
							Else
								Update = .UpdProduct_liDPost
							End If
						End If
						
						'**+ If the action is Update
						'+ Si la acción es Actualizar.
					Case 2
						Update = .UpdateProdmaster()
				End Select
				
				If .nStatusInstance <> 3 Then
					If Update Then
						.nStatusInstance = 0
					End If
					
					lcolAux.Add(lclsProduct)
				End If
			End With
		Next lclsProduct
		
		mCol = lcolAux
		
Update_Err: 
		If Err.Number Then
			Update = False
		End If
		'UPGRADE_NOTE: Object lclsProduct may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsProduct = Nothing
		'UPGRADE_NOTE: Object lcolAux may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lcolAux = Nothing
		On Error GoTo 0
	End Function
End Class






