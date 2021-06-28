Option Strict Off
Option Explicit On
Public Class Cap_educinds
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: Cap_educinds.cls                         $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:38p                                $%'
	'% $Revision:: 8                                        $%'
	'%-------------------------------------------------------%'
	
	'- Local variable to hold collection
	Private mCol As Collection
	
	'- Variables que guardan la llave de busqueda
	Private mintBranch As Integer
	Private mlngProduct As Integer
	Private mintAge As Integer
	Private mdtmEffecdate As Date
	
	'%Add: Función que agrega una fila a la colección
	Public Function Add(ByRef objClass As Cap_educind) As Cap_educind
		If objClass Is Nothing Then
			objClass = New Cap_educind
		End If
		
		With objClass
			mCol.Add(objClass, "EI" & .nBranch & .nProduct & .nAge & .dEffecdate.ToString("yyyyMMdd") & .nCurrency & .nCapschool & .nCaphscho)
		End With
		
		'+ retorna el objecto creado
		Add = objClass
		
	End Function
	
	'%Item: Se usa para referenciar un elemento de la colección
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Cap_educind
		Get
			Item = mCol.Item(vntIndexKey)
		End Get
	End Property
	
	'%Count: Se usa para obtener el numero de elementos de la colección
	Public ReadOnly Property Count() As Integer
		Get
			Count = mCol.Count()
		End Get
	End Property
	
	'%NewEnum: Obtiene un item de la colección
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
	
	'%Remove: Se usa para remover elementos de la colección
	Public Sub Remove(ByRef vntIndexKey As Object)
		mCol.Remove(vntIndexKey)
	End Sub
	
	'%Class_Initialize: inicializa la colección
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		mCol = New Collection
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'%Class_Terminate: Destruye la colección
	'UPGRADE_NOTE: Class_Terminate was upgraded to Class_Terminate_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Terminate_Renamed()
		'UPGRADE_NOTE: Object mCol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mCol = Nothing
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
	
	'%Find: Lee los datos de la tabla
	Public Function Find(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, Optional ByVal lblnFind As Boolean = False) As Boolean
		Dim lrecReaCap_educind As eRemoteDB.Execute
		Dim lclsCap_educind As Cap_educind
		
		On Error GoTo Find_Err
		Find = True
		
		If mintBranch <> nBranch Or mlngProduct <> nProduct Or mdtmEffecdate <> dEffecdate Or lblnFind Then
			
			lrecReaCap_educind = New eRemoteDB.Execute
			
			mintBranch = nBranch
			mlngProduct = nProduct
			mdtmEffecdate = dEffecdate
			
			'+ Definición de parámetros para stored procedure 'ReaCap_educind_a'
			With lrecReaCap_educind
				.StoredProcedure = "ReaCap_educind_a"
				.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If .Run Then
					Do While Not .EOF
						lclsCap_educind = New Cap_educind
						lclsCap_educind.nBranch = nBranch
						lclsCap_educind.nProduct = nProduct
						lclsCap_educind.nAge = .FieldToClass("nAge")
						lclsCap_educind.dEffecdate = .FieldToClass("dEffecdate")
						lclsCap_educind.nCurrency = .FieldToClass("nCurrency")
						lclsCap_educind.nCapschool = .FieldToClass("nCapschool")
						lclsCap_educind.nCaphscho = .FieldToClass("nCaphscho")
						lclsCap_educind.dNulldate = .FieldToClass("dNulldate")
						Call Add(lclsCap_educind)
						.RNext()
						'UPGRADE_NOTE: Object lclsCap_educind may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
						lclsCap_educind = Nothing
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
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecReaCap_educind may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecReaCap_educind = Nothing
	End Function
End Class






