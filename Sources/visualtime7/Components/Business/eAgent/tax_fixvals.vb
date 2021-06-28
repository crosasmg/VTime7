Option Strict Off
Option Explicit On
<System.Runtime.InteropServices.ProgId("Tax_fixvals_NET.Tax_fixvals")> Public Class Tax_fixvals
	Implements System.Collections.IEnumerable
	'local variable to hold collection
	Private mCol As Collection
	
	'%Add: Añade una nueva instancia de la clase "tax_fixval" a la colección
	Public Function Add(ByVal objClass As tax_fixval) As tax_fixval
		
		If objClass Is Nothing Then
			objClass = New tax_fixval
		End If
		
		With objClass
			mCol.Add(objClass, .nCode & .sTypeTax & .nTypeSupport & .nPercent)
		End With
		Add = objClass
		
Add_Err: 
		If Err.Number Then
            Add = Nothing
		End If
		On Error GoTo 0
	End Function
	
	'* Item: toma un elemento de la colección
	Public ReadOnly Property Item(ByVal vntIndexKey As Object) As tax_fixval
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
	
	'Find: Función que realiza la busqueda en la tabla 'tax_fixval'
	Public Function Find(ByVal dEffecdate As Date) As Boolean
        Dim nCode As Object = New Object
        Dim lrecTax_fixval As eRemoteDB.Execute
		Dim lclstax_fixval As tax_fixval
		On Error GoTo Find_Err
		
		lrecTax_fixval = New eRemoteDB.Execute
		
		'UPGRADE_WARNING: IsEmpty was upgraded to IsNothing and has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
		If IsNothing(nCode) Or nCode = 0 Then
			nCode = eRemoteDB.Constants.intNull
		End If
		With lrecTax_fixval
			.StoredProcedure = "reatax_fixval"
			.Parameters.Add("nCode", nCode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Do While Not .EOF
					lclstax_fixval = New tax_fixval
					lclstax_fixval.sTypeTax = .FieldToClass("sTypeTax")
					lclstax_fixval.nCode = .FieldToClass("nCode")
					lclstax_fixval.nPercent = .FieldToClass("nPercent")
					lclstax_fixval.nTypeSupport = .FieldToClass("nTypeSupport")
					Call Add(lclstax_fixval)
					'UPGRADE_NOTE: Object lclstax_fixval may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					lclstax_fixval = Nothing
					.RNext()
				Loop 
				.RCloseRec()
				Find = True
			Else
				Find = False
			End If
		End With
		'UPGRADE_NOTE: Object lrecTax_fixval may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecTax_fixval = Nothing
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
		
	End Function
End Class






