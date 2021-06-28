Option Strict Off
Option Explicit On
Public Class Client_reqs
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: Client_reqs.cls                          $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 12:35p                               $%'
	'% $Revision:: 8                                        $%'
	'%-------------------------------------------------------%'
	
	Private mCol As Collection
	
	'**-Auxiliary variables
	'-Variables auxiliares
	
	Private mintBranch As Integer
	Private mintProduct As Integer
	Private mdtmEffecdate As Date
	
	'**%Add: Adds a new instance of the Client_req class to the collection
	'%Add: A�ade una nueva instancia de la clase Client_req a la colecci�n
	Public Function Add(ByRef objElement As Client_req) As Client_req
		
		mCol.Add(objElement)
		
		'**+Returns the created object
		'+Retorna el objeto creado
		
		Add = objElement
	End Function
	
	'**%Find: This method fills the collection with records from the table "Client_req" returning TRUE or FALSE
	'**%depending on the existence of the records
	'%Find: Este metodo carga la coleccion de elementos de la tabla "Client_req" devolviendo Verdadero o
	'%falso, dependiendo de la existencia de los registros.
	Public Function Find(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nRole As Integer, ByVal nTratypep As Integer, ByVal dEffecdate As Date, Optional ByVal lblnFind As Boolean = False) As Boolean
		
		'**-Declare the variable that determines the result of the function (True/False)
		'-Se declara la variable que determina el resultado de la funcion (True/False)
		
		Static lblnRead As Boolean
		
		'**-Variable definition lrecreaClient_reqFieldReq
		'-Se define la variable lrecreaClient_reqFieldReq
		
		Dim lrecreaClient_reqFieldReq As eRemoteDB.Execute
		Dim lclsClient_req As eProduct.Client_req
		
		On Error GoTo Find_Err
		
		lrecreaClient_reqFieldReq = New eRemoteDB.Execute
		
		If mintBranch <> nBranch Or mintProduct <> nProduct Or mdtmEffecdate <> dEffecdate Or lblnFind Then
			
			mintBranch = nBranch
			mintProduct = nProduct
			mdtmEffecdate = dEffecdate
			
			'**+Parameter definition for stored procedure 'insudb.reaClient_reqFieldReq'
			'**+Information read on March 29, 2001 04:09:55 p.m.
			'+Definici�n de par�metros para stored procedure 'insudb.reaClient_reqFieldReq'
			'+Informaci�n le�da el 29/03/2001 04:09:55 p.m.
			
			With lrecreaClient_reqFieldReq
				.StoredProcedure = "reaClient_reqFieldReq"
				.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nRole", nRole, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nTratypeP", nTratypep, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("dProductDate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				
				If .Run Then
					Do While Not .EOF
						lclsClient_req = New eProduct.Client_req
						lclsClient_req.nFieldabe = .FieldToClass("nFieldabe")
						lclsClient_req.nBranch = .FieldToClass("nBranch")
						lclsClient_req.nProduct = .FieldToClass("nProduct")
						lclsClient_req.nRole = .FieldToClass("nRole")
						lclsClient_req.dEffecdate = .FieldToClass("dEffecdate")
						lclsClient_req.nTratypep = .FieldToClass("nTratypeP")
						lclsClient_req.nField = .FieldToClass("nField")
						lclsClient_req.sRequired = .FieldToClass("sRequired")
						lclsClient_req.nusercode = .FieldToClass("nUsercode")
						lclsClient_req.sDescript = .FieldToClass("sDescript")
						
						Call Add(lclsClient_req)
						
						'UPGRADE_NOTE: Object lclsClient_req may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
						lclsClient_req = Nothing
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
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		'UPGRADE_NOTE: Object lrecreaClient_reqFieldReq may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaClient_reqFieldReq = Nothing
		On Error GoTo 0
	End Function
	
	'*** Item: Returns an element of the collection (according to the index)
	'* Item: Devuelve un elemento de la colecci�n (segun �ndice)
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Client_req
		Get
			
			Item = mCol.Item(vntIndexKey)
		End Get
	End Property
	
	'*** Count: Returns the number of elements that the collection has
	'* Count: Devuelve el n�mero de elementos que posee la colecci�n
	Public ReadOnly Property Count() As Integer
		Get
			
			Count = mCol.Count()
		End Get
	End Property
	
	'***NewEnum: Enumerates the collection for use in a For Each...Next loop
	'*NewEnum: Permite enumerar la colecci�n para utilizarla en un ciclo For Each... Next
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
	'%Remove: Elimina un elemento de la colecci�n
	Public Sub Remove(ByRef vntIndexKey As Object)
		
		mCol.Remove(vntIndexKey)
	End Sub
	
	'**%Class_Initialize: Controls the creation of an instance of the collection
	'%Class_Initialize: Controla la creaci�n de una instancia de la colecci�n
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		
		mCol = New Collection
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'**%Class_Terminate: Controls the destruction of an instance of the collection
	'%Class_Terminate: Controla la destrucci�n de una instancia de la colecci�n
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






