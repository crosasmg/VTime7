Option Strict Off
Option Explicit On
Public Class Surr_percents
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: Surr_percents.cls                        $%'
	'% $Author:: MVazquez                                   $%'
	'% $Date:: 9/08/03 12:35p                               $%'
	'% $Revision:: 8                                        $%'
	'%-------------------------------------------------------%'
	
	'- Variables locales para la colección
	Private mCol As Collection
	
	'- Variables que guardan la llave de busqueda
	Private mlngBranch As Integer
	Private mlngProduct As Integer
	Private mdtmEffecdate As Date
	
	'%Add: Agrega un nuevo registro a la colección
	Public Function Add(ByRef objClass As Surr_percent) As Surr_percent
		If objClass Is Nothing Then
			objClass = New Surr_percent
		End If
		
		With objClass
			mCol.Add(objClass, "CP" & Format(.nBranch) & Format(.nProduct) & Format(.nQSurrIni) & .dEffecdate.ToString("yyyyMMdd"))
		End With
		
		'+ Se retorna el objeto creado
		Add = objClass
	End Function
	
	'%Item: Devuelve un elemento de la colección (segun índice)
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Surr_percent
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
	Public Function Find(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, Optional ByVal lblnFind As Boolean = False) As Boolean
		Dim lrecreaSurr_percent_QSurrIni As eRemoteDB.Execute
		Dim lclsSurr_percent As Surr_percent
		
		On Error GoTo Find_Err
		
		Find = False
		
		If mlngBranch <> nBranch Or mlngProduct <> nProduct Or mdtmEffecdate <> dEffecdate Or lblnFind Then
			
			lrecreaSurr_percent_QSurrIni = New eRemoteDB.Execute
			'+
			'+ Definición de store procedure reaSurr_percent_QSurrIni al 11-21-2001 15:51:52
			'+
			With lrecreaSurr_percent_QSurrIni
				.StoredProcedure = "reaSurr_percent_QSurrIni"
				.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				
				If .Run(True) Then
					Find = True
					
					Do While Not .EOF
						lclsSurr_percent = New Surr_percent
						lclsSurr_percent.nBranch = nBranch
						lclsSurr_percent.nProduct = nProduct
						lclsSurr_percent.nQSurrIni = .FieldToClass("nQSurrIni")
						lclsSurr_percent.nQSurrEnd = .FieldToClass("nQSurrEnd")
						lclsSurr_percent.nPercent = .FieldToClass("nPercent")
						
						Call Add(lclsSurr_percent)
						'UPGRADE_NOTE: Object lclsSurr_percent may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
						lclsSurr_percent = Nothing
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
		
		'UPGRADE_NOTE: Object lrecreaSurr_percent_QSurrIni may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaSurr_percent_QSurrIni = Nothing
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






