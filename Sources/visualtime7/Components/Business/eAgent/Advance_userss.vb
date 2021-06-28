Option Strict Off
Option Explicit On
Public Class Advance_userss
	Implements System.Collections.IEnumerable
	'variable local para contener colección
	Private mCol As Collection
	
	'% Add: Añade un elemento a la coleccion
	Public Function Add(ByVal objClass As Advance_users) As Advance_users
		If objClass Is Nothing Then
			objClass = New Advance_users
		End If
		
		With objClass
			mCol.Add(objClass, .nUser & .nCodmodpay)
		End With
		Add = objClass
		
	End Function
	
	'% Find: Obtiene los registros de la tabla de anticipos a otorgar
	Public Function Find() As Boolean
		Dim lrecReaAdvance_users As eRemoteDB.Execute
		Dim lclsAdvance_users As Advance_users
		
		On Error GoTo Find_Err
		lrecReaAdvance_users = New eRemoteDB.Execute
		'+ Definición de los parámetros del procedimiento reaAdvance_users_a al 24-05-2002
		With lrecReaAdvance_users
			.StoredProcedure = "ReaAdvance_users"
			If .Run Then
				Find = True
				Do While Not .EOF
					lclsAdvance_users = New Advance_users
					lclsAdvance_users.nUser = .FieldToClass("nUser")
					lclsAdvance_users.nCodmodpay = .FieldToClass("nCodmodpay")
					lclsAdvance_users.sStatregt = .FieldToClass("sStatregt")
					Call Add(lclsAdvance_users)
					'UPGRADE_NOTE: Object lclsAdvance_users may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					lclsAdvance_users = Nothing
					.RNext()
				Loop 
				.RCloseRec()
			End If
		End With
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		'UPGRADE_NOTE: Object lrecReaAdvance_users may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecReaAdvance_users = Nothing
		On Error GoTo 0
	End Function
	
	'* Item: toma un elemento de la colección
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Advance_users
		Get
			Item = mCol.Item(vntIndexKey)
		End Get
	End Property
	
	'* Count: cuenta los elementos de la colección
	Public ReadOnly Property Count() As Integer
		Get
			Count = mCol.Count()
		End Get
	End Property
	
	'* NewEnum: enumera los elementos de la colección
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
	
	'* Class_Initialize: controla la apertura de la colección
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		mCol = New Collection
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'* Class_Terminate: controla el fin de la colección
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






