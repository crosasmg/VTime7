Option Strict Off
Option Explicit On
Public Class Plan_Loadss
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: Plan_Loadss.cls                          $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 12:35p                               $%'
	'% $Revision:: 12                                       $%'
	'%-------------------------------------------------------%'
	
	Private mCol As Collection
	
	'- Variables que guardan la llave de busqueda
	Private mlngBranch As Integer
	Private mlngProduct As Integer
	Private mlngModulec As Integer
	Private mlngTypeLoad As Integer
	Private mlngDurIni As Integer
	Private mlngDurEnd As Integer
	Private mdtmEffecdate As Date
	
	'%Add: Agrega un nuevo registro a la colección
	Public Function Add(ByRef objClass As Plan_Loads) As Plan_Loads
		If objClass Is Nothing Then
			objClass = New Plan_Loads
		End If
		
		With objClass
			mCol.Add(objClass, "CP" & Format(.nBranch) & Format(.nProduct) & Format(.nModulec) & Format(.nTypeLoad) & Format(.nInitMonth) & Format(.nEndMonth) & Format(.nCapStart) & Format(.nCapEnd) & Format(.nMonths) & .dEffecdate.ToString("yyyyMMdd"))
		End With
		
		'+ Retorna el objeto creado
		Add = objClass
		
	End Function
	
	'%Item: Devuelve un elemento de la colección (segun índice)
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Plan_Loads
		Get
			Item = mCol.Item(vntIndexKey)
		End Get
	End Property
	
	'%Count: Devuelve el número de elementos que posee la colección
	Public ReadOnly Property Count() As Integer
		Get
			Count = mCol.Count()
		End Get
	End Property
	
	'%NewEnum: Permite enumerar la colección para utilizarla en un ciclo For Each... Next
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
	
	'%Remove: Elimina un elemento de la colección
	Public Sub Remove(ByRef vntIndexKey As Object)
		mCol.Remove(vntIndexKey)
	End Sub
	
	'%Find: Lee los datos de la tabla
	Public Function Find(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nModulec As Integer, ByVal nTypeLoad As Integer, ByVal dEffecdate As Date, Optional ByVal lblnFind As Boolean = False) As Boolean
		Dim lrecreaPlan_loads_durini As eRemoteDB.Execute
		Dim lclsPlan_Loads As Plan_Loads
		
		On Error GoTo Find_Err
		
		Find = True
		
		If mlngBranch <> nBranch Or mlngProduct <> nProduct Or mlngModulec <> nModulec Or mlngTypeLoad <> nTypeLoad Or mdtmEffecdate <> dEffecdate Or lblnFind Then
			
			lrecreaPlan_loads_durini = New eRemoteDB.Execute
			
			'+
			'+ Definición de store procedure reaPlan_loads_durini al 11-20-2001 11:01:44
			'+
			With lrecreaPlan_loads_durini
				.StoredProcedure = "reaPlan_loads_durini"
				.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nTypeload", nTypeLoad, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				
				If .Run(True) Then
					Find = True
					Do While Not .EOF
						lclsPlan_Loads = New Plan_Loads
						
						lclsPlan_Loads.nBranch = nBranch
						lclsPlan_Loads.nProduct = nProduct
						lclsPlan_Loads.nModulec = nModulec
						lclsPlan_Loads.nTypeLoad = nTypeLoad
						lclsPlan_Loads.dEffecdate = dEffecdate
						lclsPlan_Loads.nInitMonth = .FieldToClass("nInitMonth")
						lclsPlan_Loads.nEndMonth = .FieldToClass("nEndMonth")
						lclsPlan_Loads.nCapStart = .FieldToClass("nCapStart")
						lclsPlan_Loads.nCapEnd = .FieldToClass("nCapEnd")
						lclsPlan_Loads.nPercent = .FieldToClass("nPercent")
						lclsPlan_Loads.nAmount = .FieldToClass("nAmount")
						lclsPlan_Loads.nMonths = .FieldToClass("nMonths")
						
						Call Add(lclsPlan_Loads)
						
						'UPGRADE_NOTE: Object lclsPlan_Loads may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
						lclsPlan_Loads = Nothing
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
		'UPGRADE_NOTE: Object lrecreaPlan_loads_durini may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaPlan_loads_durini = Nothing
		On Error GoTo 0
	End Function
	
	'%Find_Product: Lee los datos de la tabla para un ramo-producto
	Public Function Find_Product(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, Optional ByVal lblnFind As Boolean = False) As Boolean
		Dim lrecreaPlan_loads_durini As eRemoteDB.Execute
		Dim lclsPlan_Loads As Plan_Loads
		
		On Error GoTo Find_Err
		
		Find_Product = True
		
		If mlngBranch <> nBranch Or mlngProduct <> nProduct Or mdtmEffecdate <> dEffecdate Or lblnFind Then
			
			lrecreaPlan_loads_durini = New eRemoteDB.Execute
			
			'+
			'+ Definición de store procedure reaPlan_loads_durini al 11-20-2001 11:01:44
			'+
			With lrecreaPlan_loads_durini
				.StoredProcedure = "reaPlan_loads_modulec"
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
		'UPGRADE_NOTE: Object lrecreaPlan_loads_durini may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaPlan_loads_durini = Nothing
		On Error GoTo 0
	End Function
	
	'%Class_Initialize: Controla la creación de una instancia de la colección
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		mCol = New Collection
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
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






