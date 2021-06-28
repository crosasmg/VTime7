Option Strict Off
Option Explicit On
Public Class Tab_Interests
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: Tab_Interests.cls                        $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:38p                                $%'
	'% $Revision:: 8                                        $%'
	'%-------------------------------------------------------%'
	
	'local variable to hold collection
	Private mCol As Collection
	
	'- Variables que guardan la llave de busqueda
	Private mlngBranch As Integer
	Private mlngProduct As Integer
	Private mlngModulec As Integer
	Private mlngTypeinvest As Integer
	
	'%Add: Agrega un nuevo registro a la colección
	Public Function Add(ByRef objClass As Tab_Interest) As Tab_Interest
		If objClass Is Nothing Then
			objClass = New Tab_Interest
		End If
		
		With objClass
			mCol.Add(objClass, "CP" & Format(.nBranch) & Format(.nProduct) & Format(.nModulec) & Format(.nTypeinvest) & .dEffecdate.ToString("yyyyMMdd"))
		End With
		
		'Return the object created
		Add = objClass
	End Function
	
	'%Find: Lee los datos de la tabla
	Public Function Find(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nModulec As Integer, ByVal nTypeinvest As Integer, Optional ByVal bFind As Boolean = False) As Boolean
		Dim lrecReaTab_Interest_A As eRemoteDB.Execute
		Dim lclsTab_Interest As Tab_Interest
		
		On Error GoTo Find_Err
		If nBranch <> mlngBranch Or nProduct <> mlngProduct Or nModulec <> mlngModulec Or nTypeinvest <> mlngTypeinvest Or bFind Then
			
			'+ Definición de store procedure ReaTab_Interest_A al 11-09-2001 18:17:01
			lrecReaTab_Interest_A = New eRemoteDB.Execute
			With lrecReaTab_Interest_A
				.StoredProcedure = "ReaTab_Interest_A"
				.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nTypeinvest", nTypeinvest, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If .Run Then
					Do While Not .EOF
						lclsTab_Interest = New Tab_Interest
						lclsTab_Interest.nBranch = nBranch
						lclsTab_Interest.nProduct = nProduct
						lclsTab_Interest.nModulec = nModulec
						lclsTab_Interest.nTypeinvest = nTypeinvest
						lclsTab_Interest.dEffecdate = .FieldToClass("dEffecdate")
						lclsTab_Interest.nWarint = .FieldToClass("nWarint")
						Call Add(lclsTab_Interest)
						'UPGRADE_NOTE: Object lclsTab_Interest may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
						lclsTab_Interest = Nothing
						.RNext()
					Loop 
					.RCloseRec()
					mlngBranch = nBranch
					mlngProduct = nProduct
					mlngModulec = nModulec
					mlngTypeinvest = nTypeinvest
					Find = True
				End If
			End With
		Else
			Find = True
		End If
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		'UPGRADE_NOTE: Object lrecReaTab_Interest_A may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecReaTab_Interest_A = Nothing
		'UPGRADE_NOTE: Object lclsTab_Interest may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsTab_Interest = Nothing
		On Error GoTo 0
	End Function
	
	'% Item: Permite recuperar un elemento de la coleccion
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Tab_Interest
		Get
			Item = mCol.Item(vntIndexKey)
		End Get
	End Property
	
	'% Count: Propiedad que retorna la cantidad de elementos en la colección
	Public ReadOnly Property Count() As Integer
		Get
			Count = mCol.Count()
		End Get
	End Property
	
	'% NewEnum: Invocado para realizar For Each... Next
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
	
	'% Remove: Elimina un elemento de la colección
	Public Sub Remove(ByRef vntIndexKey As Object)
		mCol.Remove(vntIndexKey)
	End Sub
	
	'% Class_Initialize: se controla la creación de la colección
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		mCol = New Collection
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'% Class_Terminate: se controla la destrucción de la colección
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






