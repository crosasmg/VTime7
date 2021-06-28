Option Strict Off
Option Explicit On
Public Class Ctrol_premins
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: Ctrol_premins.cls                        $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:38p                                $%'
	'% $Revision:: 7                                        $%'
	'%-------------------------------------------------------%'
	
	'local variable to hold collection
	Private mCol As Collection
	
	'- Variables que guardan la llave de busqueda
	Private mintBranch As Integer
	Private mlngProduct As Integer
	Private mdtmEffecdate As Date
	
	'%Add: Agrega un nuevo registro a la colección
	Public Function Add(ByRef objClass As Ctrol_premin) As Ctrol_premin
		If objClass Is Nothing Then
			objClass = New Ctrol_premin
		End If
		
		With objClass
			mCol.Add(objClass, "CP" & .nBranch & .nProduct & .nMonth & .dEffecdate.ToString("yyyyMMdd"))
		End With
		
		Add = objClass
		
	End Function
	
	'% Item: Se usa para referenciar un elemento de la colección
	'------------------------------------------------------------
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Ctrol_premin
		Get
			'------------------------------------------------------------
			Item = mCol.Item(vntIndexKey)
		End Get
	End Property
	
	'% Count: Se usa para obtener el numero de elementos de la colección
	'------------------------------------------------------------
	Public ReadOnly Property Count() As Integer
		Get
			'------------------------------------------------------------
			Count = mCol.Count()
		End Get
	End Property
	
	'% NewEnum: Obtiene un item de la colección
	'------------------------------------------------------------
	'UPGRADE_NOTE: NewEnum property was commented out. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="B3FC1610-34F3-43F5-86B7-16C984F0E88E"'
	'Public ReadOnly Property NewEnum() As stdole.IUnknown
		'Get
			'------------------------------------------------------------
			'NewEnum = mCol._NewEnum
		'End Get
	'End Property
	
	Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
		'UPGRADE_TODO: Uncomment and change the following line to return the collection enumerator. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="95F9AAD0-1319-4921-95F0-B9D3C4FF7F1C"'
		GetEnumerator = mCol.GetEnumerator
	End Function
	
	'% Remove: Se usa para remover elementos de la colección
	'------------------------------------------------------------
	Public Sub Remove(ByRef vntIndexKey As Object)
		'------------------------------------------------------------
		mCol.Remove(vntIndexKey)
	End Sub
	
	'%Find: Lee los datos de la tabla
	Public Function Find(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, Optional ByVal bFind As Boolean = False) As Boolean
		Dim lrecReaCtrol_premin As eRemoteDB.Execute
		Dim lclsCtrol_premin As Ctrol_premin
		
		On Error GoTo Find_Err
		Find = True
		
		If mintBranch <> nBranch Or mlngProduct <> nProduct Or mdtmEffecdate <> dEffecdate Or bFind Then
			
			lrecReaCtrol_premin = New eRemoteDB.Execute
			
			With lrecReaCtrol_premin
				.StoredProcedure = "ReaCtrol_premin_a"
				.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If .Run Then
					Find = True
					mintBranch = nBranch
					mlngProduct = nProduct
					mdtmEffecdate = dEffecdate
					Do While Not .EOF
						lclsCtrol_premin = New Ctrol_premin
						lclsCtrol_premin.nBranch = nBranch
						lclsCtrol_premin.nProduct = nProduct
						lclsCtrol_premin.nMonth = .FieldToClass("nMonth")
						lclsCtrol_premin.dEffecdate = .FieldToClass("dEffecdate")
						lclsCtrol_premin.nRate = .FieldToClass("nRate")
						lclsCtrol_premin.nAmount = .FieldToClass("nAmount")
						lclsCtrol_premin.dNulldate = .FieldToClass("dNulldate")
						Call Add(lclsCtrol_premin)
						.RNext()
						'UPGRADE_NOTE: Object lclsCtrol_premin may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
						lclsCtrol_premin = Nothing
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
		'UPGRADE_NOTE: Object lrecReaCtrol_premin may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecReaCtrol_premin = Nothing
		On Error GoTo 0
	End Function
	
	'% Class_Initialize: Crea la colección
	'------------------------------------------------------------
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		'------------------------------------------------------------
		mCol = New Collection
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'% Class_Terminate: Destruye la colección
	'------------------------------------------------------------
	'UPGRADE_NOTE: Class_Terminate was upgraded to Class_Terminate_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Terminate_Renamed()
		'------------------------------------------------------------
		'UPGRADE_NOTE: Object mCol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mCol = Nothing
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
End Class






