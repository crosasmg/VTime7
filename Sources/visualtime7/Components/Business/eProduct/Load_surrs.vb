Option Strict Off
Option Explicit On
Public Class Load_surrs
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: Load_surrs.cls                           $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 12:35p                               $%'
	'% $Revision:: 8                                        $%'
	'%-------------------------------------------------------%'
	
	'- Variables locales para la colección
	Private mCol As Collection
	
	'- Variables que guardan la llave de busqueda
	Private mlngBranch As Integer
	Private mlngProduct As Integer
	Private mlngModulec As Integer
	Private mlngQMonthIni As Integer
	Private mdtmEffecdate As Date
	
	'%Add: Agrega un nuevo registro a la colección
	Public Function Add(ByRef objClass As Load_surr) As Load_surr
		If objClass Is Nothing Then
			objClass = New Load_surr
		End If
		
		With objClass
			mCol.Add(objClass, "CP" & Format(.nBranch) & Format(.nProduct) & Format(.nModulec) & Format(.nQMonthIni) & .dEffecdate.ToString("yyyyMMdd"))
		End With
		
		'+ Se retorna el objeto creado
		Add = objClass
	End Function
	
	'%Item: Devuelve un elemento de la colección (segun índice)
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Load_surr
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
	Public Function Find(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nModulec As Integer, ByVal dEffecdate As Date, Optional ByVal lblnFind As Boolean = False) As Boolean
		Dim lrecreaLoad_surr_qmonthini As eRemoteDB.Execute
		Dim lclsLoad_Surr As Load_surr
		
		On Error GoTo Find_Err
		
		Find = False
		
		If mlngBranch <> nBranch Or mlngProduct <> nProduct Or mlngModulec <> nModulec Or mdtmEffecdate <> dEffecdate Or lblnFind Then
			
			lrecreaLoad_surr_qmonthini = New eRemoteDB.Execute
			'+
			'+ Definición de store procedure reaLoad_surr_qmonthini al 11-21-2001 15:51:52
			'+
			With lrecreaLoad_surr_qmonthini
				.StoredProcedure = "reaLoad_surr_qmonthini"
				.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				
				If .Run(True) Then
					Find = True
					
					Do While Not .EOF
						lclsLoad_Surr = New Load_surr
						lclsLoad_Surr.nBranch = nBranch
						lclsLoad_Surr.nProduct = nProduct
						lclsLoad_Surr.nModulec = nModulec
						lclsLoad_Surr.nQMonthIni = .FieldToClass("nQmonthini")
						lclsLoad_Surr.nQMonthEnd = .FieldToClass("nQmonthend")
						lclsLoad_Surr.nPercent = .FieldToClass("nPercent")
						lclsLoad_Surr.nPerTotSurr = .FieldToClass("nPerTotSurr")
						lclsLoad_Surr.nPerParSurr = .FieldToClass("nPerParSurr")
						lclsLoad_Surr.nChargTSurr = .FieldToClass("nChargTSurr")
						lclsLoad_Surr.nChargPSurr = .FieldToClass("nChargPSurr")
						lclsLoad_Surr.nQFree_Surr = .FieldToClass("nQFree_Surr")
						
						Call Add(lclsLoad_Surr)
						'UPGRADE_NOTE: Object lclsLoad_Surr may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
						lclsLoad_Surr = Nothing
						.RNext()
					Loop 
					.RCloseRec()
				End If
			End With
		End If
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		
		'UPGRADE_NOTE: Object lrecreaLoad_surr_qmonthini may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaLoad_surr_qmonthini = Nothing
		On Error GoTo 0
	End Function
	
	'%Find_Product: Lee los datos de la tabla para un ramo-producto
	Public Function Find_Product(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, Optional ByVal lblnFind As Boolean = False) As Boolean
		Dim lrecreaLoad_surr_modulec As eRemoteDB.Execute
		On Error GoTo reaLoad_surr_modulec_Err
		
		lrecreaLoad_surr_modulec = New eRemoteDB.Execute
		
		'+
		'+ Definición de store procedure reaLoad_surr_modulec al 11-21-2001 17:11:02
		'+
		With lrecreaLoad_surr_modulec
			.StoredProcedure = "reaLoad_surr_modulec"
			With .Parameters
				.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			End With
			Find_Product = .Run(True)
			.RCloseRec()
		End With
		
reaLoad_surr_modulec_Err: 
		If Err.Number Then
			Find_Product = False
		End If
		
		'UPGRADE_NOTE: Object lrecreaLoad_surr_modulec may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaLoad_surr_modulec = Nothing
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






