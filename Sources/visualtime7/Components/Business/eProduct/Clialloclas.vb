Option Strict Off
Option Explicit On
Public Class Clialloclas
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: Clialloclas.cls                          $%'
	'% $Author:: Nvaplat18                                  $%'
	'% $Date:: 8/09/03 18.30                                $%'
	'% $Revision:: 8                                        $%'
	'%-------------------------------------------------------%'
	
	Private mCol As Collection
	
	'**-Auxiliary variables
	'-Variables auxiliares
	
	Private mintBranch As Integer
	Private mintProduct As Integer
	
	'**%Add: Adds a new instance of the Cliallocla class to the collection.
	'%Add: Añade una nueva instancia de la clase Cliallocla a la colección
	Public Function Add(ByRef objElement As Object) As Cliallocla
		Dim objNewMember As Cliallocla
		objNewMember = objElement
		
		mCol.Add(objNewMember)
		
		'**+Returns the created object
		'+Retorna el objeto creado
		Add = objNewMember
		'UPGRADE_NOTE: Object objNewMember may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		objNewMember = Nothing
	End Function
	
	'**%Find: This method fills the collection with records from the table "Cliallocla" returning TRUE or FALSE
	'**%depending on the existence of the records
	'%Find: Este metodo carga la coleccion de elementos de la tabla "Cliallocla" devolviendo Verdadero o
	'%falso, dependiendo de la existencia de los registros.
	Public Function Find(ByVal nBranch As Integer, ByVal nProduct As Integer, Optional ByVal lblnFind As Boolean = False) As Boolean
		
		'**-Declare the variable that determines the result of the function (True/False)
		'-Se declara la variable que determina el resultado de la funcion (True/False)
		
		Static lblnRead As Boolean
		Dim lrecreaCliallocla As eRemoteDB.Execute
		Dim lclsCliallocla As eProduct.Cliallocla
		
		lrecreaCliallocla = New eRemoteDB.Execute
		
		If mintBranch <> nBranch Or mintProduct <> nProduct Or lblnFind Then
			
			mintBranch = nBranch
			mintProduct = nProduct
			
			'**+Parameter definition for stored procedure 'insudb.reaCliallocla'
			'**+Information read on April 09,2001  09:17:21 a.m.
			'+Definición de parámetros para stored procedure 'insudb.reaCliallocla'
			'+Información leída el 09/04/2001 09:17:21 a.m.
			
			With lrecreaCliallocla
				.StoredProcedure = "reaCliallocla"
				.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				
				If .Run Then
					Do While Not .EOF
						lclsCliallocla = New eProduct.Cliallocla
						lclsCliallocla.nBranch = .FieldToClass("nBranch")
						lclsCliallocla.nProduct = .FieldToClass("nProduct")
						lclsCliallocla.nRole = .FieldToClass("nRole")
						lclsCliallocla.sRequire = .FieldToClass("sRequire")
						lclsCliallocla.nUsercode = .FieldToClass("nUsercode")
                        lclsCliallocla.nMaxnum_rol = .FieldToClass("nMaxnum_rol")
                        lclsCliallocla.SDEFAULT_CLA_IND = .FieldToClass("SDEFAULT_CLA_IND")
						
						Call Add(lclsCliallocla)
						
						'UPGRADE_NOTE: Object lclsCliallocla may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
						lclsCliallocla = Nothing
						.RNext()
					Loop 
					
					.RCloseRec()
					lblnRead = True
				Else
					lblnRead = False
				End If
			End With
		End If
		
		Find = lblnRead
		'UPGRADE_NOTE: Object lrecreaCliallocla may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaCliallocla = Nothing
	End Function
	
	'**%Find: Returns the information of the permitted clients in a claim for a product.
	'%Find: Devuelve la información de los clientes permitidos en un siniestro para un producto
	Public Function FindDP056(ByVal nBranch As Integer, ByVal nProduct As Integer, Optional ByVal lblnFind As Boolean = False) As Boolean
		
		'**-Declare the variable that determines the result of the function (True/False)
		'-Se declara la variable que determina el resultado de la funcion (True/False)
		
		Static lblnRead As Boolean
		Dim lrecreaCliallocla As eRemoteDB.Execute
		Dim lclsCliallocla As eProduct.Cliallocla
		
		lrecreaCliallocla = New eRemoteDB.Execute
		
		If mintBranch <> nBranch Or mintProduct <> nProduct Or lblnFind Then
			
			mintBranch = nBranch
			mintProduct = nProduct
			
			'**+Parameter definition for stroed procedure 'insudb.reaCliallocla'
			'**+Information read in April 23,2001  09:17:21 a.m.
			'+Definición de parámetros para stored procedure 'insudb.reaCliallocla'
			'+Información leída el 23/04/2001 09:17:21 a.m.
			
			With lrecreaCliallocla
				.StoredProcedure = "reaClialloclaTable184"
				.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				
				If .Run Then
					Do While Not .EOF
						lclsCliallocla = New eProduct.Cliallocla
						lclsCliallocla.nBranch = .FieldToClass("nBranch")
						lclsCliallocla.nProduct = .FieldToClass("nProduct")
						lclsCliallocla.nRole = .FieldToClass("nRole")
						lclsCliallocla.sRequire = .FieldToClass("sRequire")
						lclsCliallocla.nUsercode = .FieldToClass("nUsercode")
						lclsCliallocla.nMaxnum_rol = .FieldToClass("nMaxnum_rol")
						lclsCliallocla.sDescript = .FieldToClass("sDescript")
                        lclsCliallocla.SDEFAULT_CLA_IND = .FieldToClass("SDEFAULT_CLA_IND")

						Call Add(lclsCliallocla)
						
						'UPGRADE_NOTE: Object lclsCliallocla may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
						lclsCliallocla = Nothing
						.RNext()
					Loop 
					
					.RCloseRec()
					lblnRead = True
				Else
					lblnRead = False
				End If
			End With
		End If
		
		FindDP056 = lblnRead
		'UPGRADE_NOTE: Object lrecreaCliallocla may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaCliallocla = Nothing
	End Function
	
	'***Item: Returns an element of the collection (according to the index)
	'*Item: Devuelve un elemento de la colección (segun el índice)
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Cliallocla
		Get
			
			Item = mCol.Item(vntIndexKey)
		End Get
	End Property
	
	'***Count: Returns the number of elements that the collection has
	'*Count: Devuelve el número de elementos que posee la colección
	Public ReadOnly Property Count() As Integer
		Get
			
			Count = mCol.Count()
		End Get
	End Property
	
	'***NewEnum: Enumerates the collection for use in a For Each...Next loop
	'*NewEnum: Permite enumerar la colección para utilizarla en un ciclo For Each... Next
	'UPGRADE_NOTE: NewEnum property was commented out. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="B3FC1610-34F3-43F5-86B7-16C984F0E88E"'
	'Public ReadOnly Property NewEnum() As stdole.IUnknown
		'Get
			'
			'NewEnum = mCol._NewEnum
		'End Get
	'End Property
	
	Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
		'UPGRADE_TODO: Uncomment and change the following line to return the collection enumerator. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="95F9AAD0-1319-4921-95F0-B9D3C4FF7F1C"'
		GetEnumerator = mCol.GetEnumerator
	End Function
	
	'**%Remove: Deletes an element from the collection
	'%Remove: Elimina un elemento de la colección
	'---------------------------------------------
	Public Sub Remove(ByRef vntIndexKey As Object)
		'---------------------------------------------
		
		mCol.Remove(vntIndexKey)
	End Sub
	
	'**%Class_Initialize: Controls the creation of an instance of the collection
	'%Class_Initialize: Controla la creación de una instancia de la colección
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		
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
		
		'UPGRADE_NOTE: Object mCol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mCol = Nothing
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
End Class






