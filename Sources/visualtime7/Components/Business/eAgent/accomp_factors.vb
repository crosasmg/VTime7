Option Strict Off
Option Explicit On
Public Class accomp_factors
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: accomp_factors.cls                       $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:41p                                $%'
	'% $Revision:: 4                                        $%'
	'%-------------------------------------------------------%'
	
	Private mcolaccomp_factor As Collection
	
	'% Add: añade un nuevo elemento a la colección
	Public Function Add(ByRef nCurrency As Object, ByRef nAmo_Ini As Object, ByRef nAmo_End As Object, ByRef nFactor As Object, Optional ByRef sKey As String = "") As accomp_factor
		Dim lclsaccomp_factor As accomp_factor
		
		lclsaccomp_factor = New accomp_factor
		
		With lclsaccomp_factor
			.nCurrency = nCurrency
			.nAmo_Ini = nAmo_Ini
			.nAmo_End = nAmo_End
			.nFactor = nFactor
		End With
		'+ Set de la Propiedad pasada en este metodo
		If sKey = String.Empty Then
			mcolaccomp_factor.Add(lclsaccomp_factor)
		Else
			mcolaccomp_factor.Add(lclsaccomp_factor, sKey)
		End If
		
		'+ Retorna el Objeto creado
		Add = lclsaccomp_factor
		'UPGRADE_NOTE: Object lclsaccomp_factor may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsaccomp_factor = Nothing
	End Function
	
	'% Find: Función que realiza la busqueda en la tabla 'accomp_factor'
	Public Function Find() As Boolean
		Dim lclsaccomp_factor As eRemoteDB.Execute
		
		lclsaccomp_factor = New eRemoteDB.Execute
		
		'+ Define todos los parametros para el stored procedures 'reaacomp_factor_a'.
		With lclsaccomp_factor
			.StoredProcedure = "reaaccomp_factor_a"
			
			If .Run(True) Then
				Do While Not .EOF
					Call Add(.FieldToClass("nCurrency"), .FieldToClass("nAmo_Ini"), .FieldToClass("nAmo_End"), .FieldToClass("nFactor"))
					.RNext()
				Loop 
				Find = True
				.RCloseRec()
				
			Else
				Find = False
			End If
		End With
		'UPGRADE_NOTE: Object lclsaccomp_factor may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsaccomp_factor = Nothing
	End Function
	
	'% Item: Devuelve un elemento de la colección (segun índice)
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As accomp_factor
		Get
			Item = mcolaccomp_factor.Item(vntIndexKey)
		End Get
	End Property
	
	'% Count: Devuelve el número de elementos que posee la colección
	Public ReadOnly Property Count() As Integer
		Get
			Count = mcolaccomp_factor.Count()
		End Get
	End Property
	
	'% NewEnum: Permite enumerar la colección para utilizarla en un ciclo For Each... Next
	'UPGRADE_NOTE: NewEnum property was commented out. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="B3FC1610-34F3-43F5-86B7-16C984F0E88E"'
	'Public ReadOnly Property NewEnum() As stdole.IUnknown
		'Get
			'NewEnum = mcolaccomp_factor._NewEnum
		'End Get
	'End Property
	
	Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
		'UPGRADE_TODO: Uncomment and change the following line to return the collection enumerator. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="95F9AAD0-1319-4921-95F0-B9D3C4FF7F1C"'
		GetEnumerator = mcolaccomp_factor.GetEnumerator
	End Function
	
	'% Remove: Elimina un elemento de la colección
	Public Sub Remove(ByRef vntIndexKey As Object)
		mcolaccomp_factor.Remove(vntIndexKey)
	End Sub
	
	'% Class_Initialize: Controla la creación de una instancia de la colección
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		mcolaccomp_factor = New Collection
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'% Class_Terminate: Controla la destrucción de una instancia de la colección
	'UPGRADE_NOTE: Class_Terminate was upgraded to Class_Terminate_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Terminate_Renamed()
		'UPGRADE_NOTE: Object mcolaccomp_factor may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mcolaccomp_factor = Nothing
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
End Class






