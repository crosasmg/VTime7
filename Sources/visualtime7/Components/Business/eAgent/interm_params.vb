Option Strict Off
Option Explicit On
Public Class interm_params
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: interm_params.cls                        $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:41p                                $%'
	'% $Revision:: 5                                        $%'
	'%-------------------------------------------------------%'
	
	Private mcolinterm_param As Collection
	
	'%Add: Añade una nueva instancia de la clase "interm_param" a la colección
	Public Function Add(ByVal nInsu_Assist As Double, ByVal nBonus_Curr As Integer, ByVal nMax_Bonus As Double, ByVal nMax_Accomp As Double, ByVal nMinAmount As Double, ByVal nDay_Discloan As Integer, Optional ByRef sKey As String = "") As interm_param
		Dim lclsinterm_param As interm_param
		
		lclsinterm_param = New interm_param
		
		With lclsinterm_param
			.nInsu_Assist = nInsu_Assist
			.nBonus_Curr = nBonus_Curr
			.nMax_Bonus = nMax_Bonus
			.nMax_Accomp = nMax_Accomp
			.nMinAmount = nMinAmount
			.nDay_Discloan = nDay_Discloan
		End With
		
		'+ Set de las Propiedades pasadas en el metodo
		If sKey = String.Empty Then
			mcolinterm_param.Add(lclsinterm_param)
		Else
			mcolinterm_param.Add(lclsinterm_param, sKey)
		End If
		
		'+ Retorna el Objeto creado
		Add = lclsinterm_param
		'UPGRADE_NOTE: Object lclsinterm_param may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsinterm_param = Nothing
	End Function
	
	'Find: Función que realiza la busqueda en la tabla 'interm_param'
	Public Function Find(ByVal plngUsercode As Integer) As Boolean
		Dim lclsinterm_param As eRemoteDB.Execute
		
		lclsinterm_param = New eRemoteDB.Execute
		
		'+ Define todos los parametros para el stored procedures 'insudb.reainterm_param'
		With lclsinterm_param
			.StoredProcedure = "reainterm_param_a"
			
			If .Run(True) Then
				Do While Not .EOF
					Call Add(.FieldToClass("nInsu_Assist"), .FieldToClass("nBonus_Curr"), .FieldToClass("nMax_Bonus"), .FieldToClass("nMax_Accomp"), .FieldToClass("nMinAmount"), .FieldToClass("nDay_Discloan"))
					.RNext()
				Loop 
				Find = True
				.RCloseRec()
				
			Else
				Find = False
			End If
		End With
		'UPGRADE_NOTE: Object lclsinterm_param may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsinterm_param = Nothing
	End Function
	
	'% Item: Devuelve un elemento de la colección (segun índice)
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As interm_param
		Get
			Item = mcolinterm_param.Item(vntIndexKey)
		End Get
	End Property
	
	'% Count: Devuelve el número de elementos que posee la colección
	Public ReadOnly Property Count() As Integer
		Get
			Count = mcolinterm_param.Count()
		End Get
	End Property
	
	'% NewEnum: Permite enumerar la colección para utilizarla en un ciclo For Each... Next
	'UPGRADE_NOTE: NewEnum property was commented out. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="B3FC1610-34F3-43F5-86B7-16C984F0E88E"'
	'Public ReadOnly Property NewEnum() As stdole.IUnknown
		'Get
			'NewEnum = mcolinterm_param._NewEnum
		'End Get
	'End Property
	
	Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
		'UPGRADE_TODO: Uncomment and change the following line to return the collection enumerator. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="95F9AAD0-1319-4921-95F0-B9D3C4FF7F1C"'
		GetEnumerator = mcolinterm_param.GetEnumerator
	End Function
	
	'% Remove: Elimina un elemento de la colección
	Public Sub Remove(ByRef vntIndexKey As Object)
		mcolinterm_param.Remove(vntIndexKey)
	End Sub
	
	'% Class_Initialize: Controla la creación de una instancia de la colección
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		mcolinterm_param = New Collection
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'% Class_Terminate: Controla la destrucción de una instancia de la colección
	'UPGRADE_NOTE: Class_Terminate was upgraded to Class_Terminate_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Terminate_Renamed()
		'UPGRADE_NOTE: Object mcolinterm_param may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mcolinterm_param = Nothing
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
End Class






