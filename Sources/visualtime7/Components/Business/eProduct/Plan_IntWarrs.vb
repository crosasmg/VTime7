Option Strict Off
Option Explicit On
Public Class Plan_IntWarrs
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: Plan_IntWarrs.cls                        $%'
	'% $Author:: Gazuaje                                    $%'
	'% $Date:: 3/07/06 7:52p                                $%'
	'% $Revision:: 2                                        $%'
	'%-------------------------------------------------------%'
	
	Private mCol As Collection
	
	'- Variables que guardan la llave de busqueda
	Private mlngBranch As Integer
	Private mlngProduct As Integer
	Private mlngModulec As Integer
	Private mdtmEffecdate As Date
	
	
	'%Add: Agrega un nuevo registro a la colección
	Public Function Add(ByRef objClass As Plan_IntWar) As Plan_IntWar
		If objClass Is Nothing Then
			objClass = New Plan_IntWar
		End If
		
		With objClass
			mCol.Add(objClass, "CP" & Format(.nBranch) & Format(.nProduct) & Format(.nModulec) & Format(.nTypeInvest) & Format(.nIntwarr) & Format(.nIntWarrMin) & .dEffecdate.ToString("yyyyMMdd"))
		End With
		
		'+ Retorna el objeto creado
		Add = objClass
		
	End Function
	
	'%Find: Lee los datos de la tabla
	Public Function Find(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nModulec As Integer, ByVal dEffecdate As Date, Optional ByVal lblnFind As Boolean = False) As Boolean
		Dim lrecreaPlan_IntWarr As eRemoteDB.Execute
		Dim lclsPlan_IntWarr As Plan_IntWar
		
		On Error GoTo Find_Err
		
		Find = True
		
		If mlngBranch <> nBranch Or mlngProduct <> nProduct Or mlngModulec <> nModulec Or mdtmEffecdate <> dEffecdate Or lblnFind Then
			
			lrecreaPlan_IntWarr = New eRemoteDB.Execute
			
			'+
			'+ Definición de store procedure reaPlan_loads_durini al 11-20-2001 11:01:44
			'+
			With lrecreaPlan_IntWarr
				.StoredProcedure = "reaPlan_IntWarr"
				.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				
				If .Run(True) Then
					Find = True
					Do While Not .EOF
						lclsPlan_IntWarr = New Plan_IntWar
						
						lclsPlan_IntWarr.nBranch = nBranch
						lclsPlan_IntWarr.nProduct = nProduct
						lclsPlan_IntWarr.nModulec = nModulec
						lclsPlan_IntWarr.dEffecdate = dEffecdate
						lclsPlan_IntWarr.nTypeInvest = .FieldToClass("nTypeInvest")
						lclsPlan_IntWarr.nIntwarr = .FieldToClass("nIntWarr")
						lclsPlan_IntWarr.nIntWarrMin = .FieldToClass("nIntWarrMin")
						lclsPlan_IntWarr.nIntWarrClear = .FieldToClass("nIntWarrClear")
						Call Add(lclsPlan_IntWarr)
						
						'UPGRADE_NOTE: Object lclsPlan_IntWarr may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
						lclsPlan_IntWarr = Nothing
						.RNext()
					Loop 
					.RCloseRec()
				Else
					Find = False
				End If
			End With
		End If
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		'UPGRADE_NOTE: Object lrecreaPlan_IntWarr may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaPlan_IntWarr = Nothing
		On Error GoTo 0
	End Function
	
	'%Find_Product: Lee los datos de la tabla para un ramo-producto
	Public Function Find_Product(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, Optional ByVal lblnFind As Boolean = False) As Boolean
		Dim lrecreaPlan_IntWarr As eRemoteDB.Execute
		
		On Error GoTo Find_Err
		
		Find_Product = True
		
		If mlngBranch <> nBranch Or mlngProduct <> nProduct Or mdtmEffecdate <> dEffecdate Or lblnFind Then
			
			lrecreaPlan_IntWarr = New eRemoteDB.Execute
			
			'+
			'+ Definición de store procedure reaPlan_loads_durini al 11-20-2001 11:01:44
			'+
			With lrecreaPlan_IntWarr
				.StoredProcedure = "reaPlan_IntWarr_All"
				.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				
				Find_Product = .Run(True)
				.RCloseRec()
			End With
		End If
		
Find_Err: 
		If Err.Number Then
			Find_Product = False
		End If
		'UPGRADE_NOTE: Object lrecreaPlan_IntWarr may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaPlan_IntWarr = Nothing
		On Error GoTo 0
	End Function
	
	
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Plan_IntWar
		Get
			'used when referencing an element in the collection
			'vntIndexKey contains either the Index or Key to the collection,
			'this is why it is declared as a Variant
			'Syntax: Set foo = x.Item(xyz) or Set foo = x.Item(5)
			Item = mCol.Item(vntIndexKey)
		End Get
	End Property
	
	
	
	Public ReadOnly Property Count() As Integer
		Get
			'used when retrieving the number of elements in the
			'collection. Syntax: Debug.Print x.Count
			Count = mCol.Count()
		End Get
	End Property
	
	
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
	
	
	Public Sub Remove(ByRef vntIndexKey As Object)
		'used when removing an element from the collection
		'vntIndexKey contains either the Index or Key, which is why
		'it is declared as a Variant
		'Syntax: x.Remove(xyz)
		
		
		mCol.Remove(vntIndexKey)
	End Sub
	
	
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		'creates the collection when this class is created
		mCol = New Collection
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	
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
End Class






