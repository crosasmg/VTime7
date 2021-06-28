Option Strict Off
Option Explicit On
Public Class PercentAdvances
	Implements System.Collections.IEnumerable
	'variable local para contener colecci�n
	Private mCol As Collection
	
	'% Add: A�ade un elemento a la coleccion
	Public Function Add(ByVal objClass As PercentAdvanc) As PercentAdvanc
		If objClass Is Nothing Then
			objClass = New PercentAdvanc
		End If
		
		With objClass
			mCol.Add(objClass, .nIntermtyp & .nCodmodpay)
		End With
		Add = objClass
		
	End Function
	
	'% Find: Obtiene losvalores maximos y minimos para un tipo de intermediario
	Public Function Find(ByVal nIntermtyp As Integer) As Boolean
		Dim lrecReaPercentAdvanc As eRemoteDB.Execute
		Dim lclsPercentAdvanc As PercentAdvanc
		
		On Error GoTo Find_Err
		lrecReaPercentAdvanc = New eRemoteDB.Execute
		'+ Definici�n de los par�metros del procedimiento reaPercentAdvanc_a al 24-05-2002
		With lrecReaPercentAdvanc
			.StoredProcedure = "ReaPercentAdvanc"
			.Parameters.Add("nIntermtyp", nIntermtyp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Find = True
				Do While Not .EOF
					lclsPercentAdvanc = New PercentAdvanc
					lclsPercentAdvanc.nIntermtyp = nIntermtyp
					lclsPercentAdvanc.nCodmodpay = .FieldToClass("nCodModPay")
					lclsPercentAdvanc.nPercent_init = .FieldToClass("nPercent_init")
					lclsPercentAdvanc.nPercent_end = .FieldToClass("nPercent_end")
					Call Add(lclsPercentAdvanc)
					'UPGRADE_NOTE: Object lclsPercentAdvanc may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					lclsPercentAdvanc = Nothing
					.RNext()
				Loop 
				.RCloseRec()
			End If
		End With
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		'UPGRADE_NOTE: Object lrecReaPercentAdvanc may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecReaPercentAdvanc = Nothing
		On Error GoTo 0
	End Function
	
	'* Item: toma un elemento de la colecci�n
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As PercentAdvanc
		Get
			Item = mCol.Item(vntIndexKey)
		End Get
	End Property
	
	'* Count: cuenta los elementos de la colecci�n
	Public ReadOnly Property Count() As Integer
		Get
			Count = mCol.Count()
		End Get
	End Property
	
	'* NewEnum: enumera los elementos de la colecci�n
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
	
	'* Remove: elimina un elemento de la colecci�n
	Public Sub Remove(ByRef vntIndexKey As Object)
		mCol.Remove(vntIndexKey)
	End Sub
	
	'* Class_Initialize: controla la apertura de la colecci�n
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		mCol = New Collection
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'* Class_Terminate: controla el fin de la colecci�n
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






