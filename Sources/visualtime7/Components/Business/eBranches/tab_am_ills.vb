Option Strict Off
Option Explicit On
Public Class tab_am_ills
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: tab_am_ills.cls                          $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:38p                                $%'
	'% $Revision:: 5                                        $%'
	'%-------------------------------------------------------%'
	
	Private mcoltab_am_ill As Collection
	
	'Add: Agrega un elemento a la colección.
	Public Function Add(ByRef lclsTab_am_ill As tab_am_ill) As tab_am_ill
		With lclsTab_am_ill
			mcoltab_am_ill.Add(lclsTab_am_ill, "MAM" & .sIllness & .sDescript & .sIll_OMS & .sStatregt)
		End With
		
		'+ Retorna el elemento a la colección
		Add = lclsTab_am_ill
		'UPGRADE_NOTE: Object lclsTab_am_ill may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsTab_am_ill = Nothing
	End Function
	
	'Find: Función que realiza la busqueda en la tabla 'tab_am_ill'
	Public Function Find() As Boolean
		Dim lrectab_am_ill As eRemoteDB.Execute
		Dim lclsTab_am_ill As tab_am_ill
		
		On Error GoTo Find_Err
		
		lrectab_am_ill = New eRemoteDB.Execute
		
		With lrectab_am_ill
			.StoredProcedure = "reatab_am_ill"
			.Parameters.Add("sIllness", "", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				Do While Not .EOF
					lclsTab_am_ill = New tab_am_ill
					
					lclsTab_am_ill.sIllness = .FieldToClass("sIllness")
					lclsTab_am_ill.sDescript = .FieldToClass("sDescript")
					lclsTab_am_ill.sIll_OMS = .FieldToClass("sIll_OMS")
					lclsTab_am_ill.sStatregt = .FieldToClass("sStatregt")
					
					Call Add(lclsTab_am_ill)
					
					'UPGRADE_NOTE: Object lclsTab_am_ill may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					lclsTab_am_ill = Nothing
					.RNext()
				Loop 
				Find = True
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
		'UPGRADE_NOTE: Object lrectab_am_ill may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrectab_am_ill = Nothing
	End Function
	
	'* Item: devuelve un elemento de la colección (según índice, o llave)
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As tab_am_ill
		Get
			Item = mcoltab_am_ill.Item(vntIndexKey)
		End Get
	End Property
	
	'* Count: devuelve el número de elementos que posee la colección
	Public ReadOnly Property Count() As Integer
		Get
			Count = mcoltab_am_ill.Count()
		End Get
	End Property
	
	'* NewEnum: permite enumerar la colección para utilizarla en un ciclo For Each... Next
	'UPGRADE_NOTE: NewEnum property was commented out. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="B3FC1610-34F3-43F5-86B7-16C984F0E88E"'
	'Public ReadOnly Property NewEnum() As stdole.IUnknown
		'Get
			'NewEnum = mcoltab_am_ill._NewEnum
		'End Get
	'End Property
	
	Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
		'UPGRADE_TODO: Uncomment and change the following line to return the collection enumerator. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="95F9AAD0-1319-4921-95F0-B9D3C4FF7F1C"'
		GetEnumerator = mcoltab_am_ill.GetEnumerator
	End Function
	
	'* Remove: elimina un elemento de la colección
	Public Sub Remove(ByRef vntIndexKey As Object)
		mcoltab_am_ill.Remove(vntIndexKey)
	End Sub
	
	'* Class_Initialize: controla la creación de la instancia del objeto de la colección
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		mcoltab_am_ill = New Collection
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'* Class_Terminate: controla la destrucción de la instancia del objeto de la colección
	'UPGRADE_NOTE: Class_Terminate was upgraded to Class_Terminate_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Terminate_Renamed()
		'UPGRADE_NOTE: Object mcoltab_am_ill may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mcoltab_am_ill = Nothing
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
End Class






