Option Strict Off
Option Explicit On
Public Class Tab_Ord_Origins
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: Tab_Ord_Origins.cls                      $%'
	'% $Author:: Clobos                                     $%'
	'% $Date:: 7/02/06 11:11                                $%'
	'% $Revision:: 2                                        $%'
	'%-------------------------------------------------------%'
	
	'- Variable local para mantener la colección.
	
	Private mCol As Collection
	
	'% Add: Añade una nueva instancia de la clase Tab_Ord_Origin a la colección.
	Public Function Add(ByRef objClass As Tab_Ord_Origin) As Tab_Ord_Origin
		'+ Se crea el objeto.
		
		If objClass Is Nothing Then
			objClass = New Tab_Ord_Origin
		End If
		
		'+[APV2]  HAD 1021. Tabla de orden de aplicacion de las cuentas origen. DBLANCO 05-09-2003
		
		With objClass
			mCol.Add(objClass)
			
		End With
		
		'+ Retorna el objeto creado.
		
		Add = objClass
		'UPGRADE_NOTE: Object objClass may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		objClass = Nothing
	End Function
	
	'* Item: Devuelve un elemento de la colección (segun índice).
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Tab_Ord_Origin
		Get
			Item = mCol.Item(vntIndexKey)
		End Get
	End Property
	
	'* Count: Devuelve el número de elementos que posee la colección
	Public ReadOnly Property Count() As Integer
		Get
			Count = mCol.Count()
		End Get
	End Property
	
	'* NewEnum: Permite enumerar la colección para utilizarla en un ciclo For Each... Next
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
	
	'* Remove: Elimina un elemento de la colección
	Public Sub Remove(ByRef vntIndexKey As Object)
		mCol.Remove(vntIndexKey)
	End Sub
	
	'* Class_Initialize: Controla la creación de una instancia de la colección
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		mCol = New Collection
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'* Class_Terminate: Controla la destrucción de una instancia de la colección
	'UPGRADE_NOTE: Class_Terminate was upgraded to Class_Terminate_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Terminate_Renamed()
		'UPGRADE_NOTE: Object mCol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mCol = Nothing
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
	
	'% Find: Devuelve una coleccion de objetos de tipo Tab_Ord_Origin.
	'+[APV2]  HAD 1021. Tabla de orden de aplicacion de las cuentas origen. DBLANCO 05-09-2003
	Public Function Find(ByVal nBranch As Integer, ByVal nProduct As Integer) As Boolean
		'- Se define la variable lrecTab_Ord_Origin que se utilizará como cursor.
		
		Dim lrecTab_Ord_Origin As eRemoteDB.Execute
		Dim lclsTab_Ord_Origin As eBranches.Tab_Ord_Origin
		
		On Error GoTo Find_Err
		
		lrecTab_Ord_Origin = New eRemoteDB.Execute
		
		'+ Se ejecuta el store procedure que busca la información relacionada con la tabla de orden
		'+ de uso de las cuentas origen para pagar cargos (APV).
		
		With lrecTab_Ord_Origin
			.StoredProcedure = "reaTab_Ord_Origin"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Find = True
				
				Do While Not .EOF
					lclsTab_Ord_Origin = New eBranches.Tab_Ord_Origin
					lclsTab_Ord_Origin.nBranch = nBranch
					lclsTab_Ord_Origin.nProduct = nProduct
					lclsTab_Ord_Origin.nOrigin = .FieldToClass("nOrigin")
					lclsTab_Ord_Origin.nOrder = .FieldToClass("nOrder")
					lclsTab_Ord_Origin.sDescript = .FieldToClass("sDescript")
					lclsTab_Ord_Origin.sPrimary = .FieldToClass("sPrimary")
                    lclsTab_Ord_Origin.nPerc_collect = .FieldToClass("nPerc_Collect")
                    lclsTab_Ord_Origin.sSell_cost = .FieldToClass("sSell_cost")
                    lclsTab_Ord_Origin.dExpirdat = .FieldToClass("dExpirdat")
                    lclsTab_Ord_Origin.nOrigen_dep = .FieldToClass("nOrigen_dep")

                    Call Add(lclsTab_Ord_Origin)
					
					'UPGRADE_NOTE: Object lclsTab_Ord_Origin may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					lclsTab_Ord_Origin = Nothing
					
					.RNext()
				Loop 
			End If
		End With
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		
		'UPGRADE_NOTE: Object lrecTab_Ord_Origin may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecTab_Ord_Origin = Nothing
		'UPGRADE_NOTE: Object lclsTab_Ord_Origin may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsTab_Ord_Origin = Nothing
		
		On Error GoTo 0
	End Function
End Class






