Option Strict Off
Option Explicit On
Public Class Tab_waitPos
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: Tab_waitPos.cls                          $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:06p                                $%'
	'% $Revision:: 10                                       $%'
	'%-------------------------------------------------------%'
	
	'Variables de uso local de la collección
	Private mCol As Collection
	
	'% Add: Agrega un nuevo registro a la colección
	Public Function Add(ByRef objClass As Tab_waitPo) As Tab_waitPo
		If objClass Is Nothing Then
            objClass = New Tab_waitPo
		End If
		
		With objClass
			mCol.Add(objClass, "CP" & .nWait_code & .nOrder)
		End With
		
		'Return the object created
		Add = objClass
	End Function
	
	'% Item: toma un elemento de la colección
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Tab_waitPo
		Get
			Item = mCol.Item(vntIndexKey)
		End Get
	End Property
	'% Count: cuenta el número de elementos dentro de la colección
	Public ReadOnly Property Count() As Integer
		Get
			Count = mCol.Count()
		End Get
	End Property
	'% NewEnum: enumera los elementos dentro de la colección
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
	'% Remove: elimina un elemento dentro de la colección
	Public Sub Remove(ByRef vntIndexKey As Object)
		mCol.Remove(vntIndexKey)
	End Sub
	'% Class_Initialize: controla la apertura de cada instancia de la colección
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		mCol = New Collection
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	'% Class_Terminate: elimina la colección
	'UPGRADE_NOTE: Class_Terminate was upgraded to Class_Terminate_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Terminate_Renamed()
		'UPGRADE_NOTE: Object mCol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mCol = Nothing
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
	
	'%Find: Lee los datos de la tabla tab_waitpo la transacción mca005
	Public Function Find(Optional ByVal lblnAll As Boolean = False) As Boolean
		Dim lrecreaTab_waitPo As eRemoteDB.Execute
		Dim lclsTab_waitPo As Tab_waitPo
		
		On Error GoTo Find_Err
		lrecreaTab_waitPo = New eRemoteDB.Execute
		With lrecreaTab_waitPo
			.StoredProcedure = "reaTab_waitPo"
			'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
			.Parameters.Add("nWait_code", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
			.Parameters.Add("nOrder", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Find = True
				Do While Not .EOF
					lclsTab_waitPo = New Tab_waitPo
					lclsTab_waitPo.nOrder = .FieldToClass("nOrder")
					lclsTab_waitPo.nWait_code = .FieldToClass("nWait_code")
					lclsTab_waitPo.sDescript = .FieldToClass("sDescript")
					lclsTab_waitPo.sShort_des = .FieldToClass("sShort_des")
					lclsTab_waitPo.nAreaWait = .FieldToClass("nAreaWait")
					lclsTab_waitPo.sStatregt = .FieldToClass("sStatregt")
					lclsTab_waitPo.nUsercode = .FieldToClass("nUsercode")
					lclsTab_waitPo.sConvert = .FieldToClass("sConvert")
					Call Add(lclsTab_waitPo)
					.RNext()
					'UPGRADE_NOTE: Object lclsTab_waitPo may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					lclsTab_waitPo = Nothing
				Loop 
				.RCloseRec()
			Else
				Find = False
			End If
		End With
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecreaTab_waitPo may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaTab_waitPo = Nothing
	End Function
End Class






