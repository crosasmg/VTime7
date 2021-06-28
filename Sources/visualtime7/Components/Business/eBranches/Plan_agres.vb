Option Strict Off
Option Explicit On
Public Class Plan_agres
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: Plan_agres.cls                           $%'
	'% $Author:: Nvaplat18                                  $%'
	'% $Date:: 8/10/03 10.38                                $%'
	'% $Revision:: 8                                        $%'
	'%-------------------------------------------------------%'
	
	'local variable to hold collection
	Private mCol As Collection
	
	'- Variables que guardan la llave de busqueda
	Private mlngAgreement As Integer
	
	'%Add: Agrega un objeto a la colección
	Public Function Add(ByRef objClass As Plan_agre) As Plan_agre
		If objClass Is Nothing Then
			objClass = New Plan_agre
		End If
		
		mCol.Add(objClass)
		Add = objClass
	End Function
	
	'%Find: Lee los datos de la tabla
	Public Function Find(ByVal nAgreement As Integer, Optional ByVal bFind As Boolean = False) As Boolean
		Dim lrecReaPlan_agre As eRemoteDB.Execute
		Dim lclsPlan_agre As Plan_agre
		
		On Error GoTo Find_Err
		If mlngAgreement <> nAgreement Or bFind Then
			'+Definición de parámetros para stored procedure 'ReaPlan_agre_a'
			'+Información leída el 24/10/01
			lrecReaPlan_agre = New eRemoteDB.Execute
			With lrecReaPlan_agre
				.StoredProcedure = "ReaPlan_agre_a"
				.Parameters.Add("nAgreement", nAgreement, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If .Run Then
					Find = True
					Do While Not .EOF
						lclsPlan_agre = New Plan_agre
						lclsPlan_agre.sSel = .FieldToClass("sSel")
						lclsPlan_agre.nAgreement = .FieldToClass("nAgreement")
						lclsPlan_agre.nBranch = .FieldToClass("nBranch")
						lclsPlan_agre.sDesBranch = .FieldToClass("sDesBranch")
						lclsPlan_agre.nProduct = .FieldToClass("nProduct")
						lclsPlan_agre.sDesProduct = .FieldToClass("sDesProduct")
						lclsPlan_agre.nModulec = .FieldToClass("nModulec")
						lclsPlan_agre.sDesModulec = .FieldToClass("sDesModulec")
						Call Add(lclsPlan_agre)
						'UPGRADE_NOTE: Object lclsPlan_agre may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
						lclsPlan_agre = Nothing
						.RNext()
					Loop 
					mlngAgreement = nAgreement
					.RCloseRec()
				Else
					Find = False
				End If
			End With
		Else
			Find = True
		End If
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		'UPGRADE_NOTE: Object lrecReaPlan_agre may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecReaPlan_agre = Nothing
		On Error GoTo 0
	End Function
	
	'* Item: se instancia un elemento de la colección
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Plan_agre
		Get
			Item = mCol.Item(vntIndexKey)
		End Get
	End Property
	
	'* Count: devuelve el Nro. de elementos que tiene la colección
	Public ReadOnly Property Count() As Integer
		Get
			Count = mCol.Count()
		End Get
	End Property
	
	'* NewEnum: permite recorrer los elementos de la colección
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
	
	'* Remove: elimina un elemento de la colección
	Public Sub Remove(ByRef vntIndexKey As Object)
		mCol.Remove(vntIndexKey)
	End Sub
	
	'* Class_Initialize: se controla la creación de la colección
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		mlngAgreement = eRemoteDB.Constants.intNull
		mCol = New Collection
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'* Class_Terminate: se controla la destrucción de la colección
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






