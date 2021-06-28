Option Strict Off
Option Explicit On
Public Class Ratingss
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: Ratingss.cls                             $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:38p                                $%'
	'% $Revision:: 8                                        $%'
	'%-------------------------------------------------------%'
	
	'- Local variable to hold collection
	Private mCol As Collection
	
	'- Variables que guardan la llave de busqueda
	Private mintBranch As Integer
	Private mlngProduct As Integer
	Private mdtmEffecdate As Date
	
	'%Add:
	Public Function Add(ByRef objClass As Ratings) As Ratings
		
		If objClass Is Nothing Then
			objClass = New Ratings
		End If
		
		With objClass
			mCol.Add(objClass, "R" & .nBranch & .nProduct & .nAge_ini & .dEffecdate.ToString("yyyyMMdd"))
		End With
		
		Add = objClass
		
	End Function
	
	'% Item: Se usa para referenciar un elemento de la colección
	'------------------------------------------------------------
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Ratings
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
	
	'%Find: Lee los datos de la tabla
	Public Function Find(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, Optional ByVal bFind As Boolean = False) As Boolean
		Dim lrecReaRatings As eRemoteDB.Execute
		Dim lclsRatings As Ratings
		
		On Error GoTo Find_Err
		Find = True
		
		If mintBranch <> nBranch Or mlngProduct <> nProduct Or mdtmEffecdate <> dEffecdate Or bFind Then
			
			lrecReaRatings = New eRemoteDB.Execute
			
			mintBranch = nBranch
			mlngProduct = nProduct
			mdtmEffecdate = dEffecdate
			
			With lrecReaRatings
				.StoredProcedure = "ReaRatings"
				.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If .Run Then
					Do While Not .EOF
						lclsRatings = New Ratings
						lclsRatings.nBranch = nBranch
						lclsRatings.nProduct = nProduct
						lclsRatings.dEffecdate = dEffecdate
						lclsRatings.nAge_ini = .FieldToClass("nAge_ini")
						lclsRatings.nAge_end = .FieldToClass("nAge_end")
						lclsRatings.nRating = .FieldToClass("nRating")
						lclsRatings.dNulldate = .FieldToClass("dNulldate")
						Call Add(lclsRatings)
						.RNext()
						'UPGRADE_NOTE: Object lclsRatings may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
						lclsRatings = Nothing
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
		'UPGRADE_NOTE: Object lrecReaRatings may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecReaRatings = Nothing
		On Error GoTo 0
	End Function
End Class






