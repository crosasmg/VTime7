Option Strict Off
Option Explicit On
Public Class cot_stand_alones
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: cot_stand_alones.cls                       $%'
	'% $Author:: Pmanzur                                    $%'
	'% $Date:: 21/02/06 17:37                               $%'
	'% $Revision:: 2                                        $%'
	'%-------------------------------------------------------%'
	
	'-Variable de la coleccion
	Private mCol As Collection
	
	'%Add: Agrega un elemento a la colecci?n
	Public Function Add(ByVal lclscot_stand_alone As cot_stand_alone) As cot_stand_alone
		With lclscot_stand_alone
			mCol.Add(lclscot_stand_alone, "CT" & .nId_object)
		End With
		'+ Devuelve el objeto creado.
		Add = lclscot_stand_alone
		'UPGRADE_NOTE: Object lclscot_stand_alone may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclscot_stand_alone = Nothing
	End Function
	
	'% Find: busca los datos correspondientes a las columnas asociadas a una hoja
	Public Function Find() As Boolean
		Dim lrecTime As eRemoteDB.Execute
		Dim lclscot_stand_alone As cot_stand_alone
		
		On Error GoTo Find_Err
		
		lrecTime = New eRemoteDB.Execute
		
		With lrecTime
			.StoredProcedure = "Reacot_stand_alone"
			If .Run Then
				Find = True
				Do While Not .EOF
					lclscot_stand_alone = New cot_stand_alone
					With lclscot_stand_alone
						.nId_object = lrecTime.FieldToClass("nId_object", 0)
						.sName = lrecTime.FieldToClass("sName", "")
						.nType_object = lrecTime.FieldToClass("nType_object", 0)
						.nLevel = lrecTime.FieldToClass("nLevel", 0)
						.nOrder = lrecTime.FieldToClass("nOrder", 0)
						.sPath = lrecTime.FieldToClass("sPath", "")
					End With
					Call Add(lclscot_stand_alone)
					'UPGRADE_NOTE: Object lclscot_stand_alone may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					lclscot_stand_alone = Nothing
					.RNext()
				Loop 
			End If
		End With
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecTime may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecTime = Nothing
		
	End Function
	
	'%Item: devuelve un elemento de la colecci?n (seg?n ?ndice, o llave)
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As cot_stand_alone
		Get
			Item = mCol.Item(vntIndexKey)
		End Get
	End Property
	
	'%Count: devuelve el n?mero de elementos que posee la colecci?n
	Public ReadOnly Property Count() As Integer
		Get
			Count = mCol.Count()
		End Get
	End Property
	
	'% NewEnum: permite enumerar la colecci?n para utilizarla en un ciclo For Each... Next
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
	
	'%Remove: elimina un elemento de la colecci?n
	Public Sub Remove(ByRef vntIndexKey As Object)
		mCol.Remove(vntIndexKey)
	End Sub
	
	'% Class_Initialize: controla la creaci?n de la instancia del objeto de la clase
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		mCol = New Collection
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'% Class_Terminate: controla la destrucci?n de la instancia del objeto de la clase
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






