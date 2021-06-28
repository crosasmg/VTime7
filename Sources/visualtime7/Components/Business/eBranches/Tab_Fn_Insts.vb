Option Strict Off
Option Explicit On
Public Class Tab_Fn_Insts
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: Tab_Fn_Insts.cls                         $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:38p                                $%'
	'% $Revision:: 3                                        $%'
	'%-------------------------------------------------------%'
	
	'- Variables locales
	Private mCol As Collection
	
	'% Add: A�ade una nueva instancia de la clase Tab_Fn_Inst a la colecci�n
	Public Function Add(ByRef objClass As Tab_Fn_Inst) As Tab_Fn_Inst
		'+ se crea el objeto
		
		If objClass Is Nothing Then
			objClass = New Tab_Fn_Inst
		End If
		
		With objClass
            mCol.Add(objClass, .nInstitution & .nTypeInstitu & .sName & .sStatregt & .sClient & .sDigit & .sInstitution)
		End With

		'+ retorna el objeto creado
		
		Add = objClass
		'UPGRADE_NOTE: Object objClass may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		objClass = Nothing
	End Function
	
	'% Find: Devuelve una coleccion de objetos de tipo Tab_Fn_Inst
	Public Function Find() As Boolean
		
		'- Se define la variable lrecTab_Fn_Inst que se utilizar� como cursor.
		
		Dim lrecTab_Fn_Inst As eRemoteDB.Execute
		Dim lclsTab_Fn_Inst As Tab_Fn_Inst
		
		On Error GoTo Find_Err
		
		lrecTab_Fn_Inst = New eRemoteDB.Execute
		
		'+ Se ejecuta el store procedure que busca los veh�culos
		
		With lrecTab_Fn_Inst
			.StoredProcedure = "reaTab_Fn_Inst_1"
			
			If .Run Then
				Find = True
				
				Do While Not .EOF
					lclsTab_Fn_Inst = New Tab_Fn_Inst
					
					lclsTab_Fn_Inst.nInstitution = .FieldToClass("nInstitution")
					lclsTab_Fn_Inst.nTypeInstitu = .FieldToClass("nTypeInstitu")
					lclsTab_Fn_Inst.sName = .FieldToClass("sName")
					lclsTab_Fn_Inst.sStatregt = .FieldToClass("sStatregt")
                    lclsTab_Fn_Inst.sClient = .FieldToClass("sClient")
                    lclsTab_Fn_Inst.sDigit = .FieldToClass("sDigit")
                    lclsTab_Fn_Inst.sInstitution = .FieldToClass("sInstitution")

					Call Add(lclsTab_Fn_Inst)
					
					'UPGRADE_NOTE: Object lclsTab_Fn_Inst may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					lclsTab_Fn_Inst = Nothing
					
					.RNext()
				Loop 
			End If
		End With
		
		'UPGRADE_NOTE: Object lrecTab_Fn_Inst may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecTab_Fn_Inst = Nothing
		'UPGRADE_NOTE: Object lclsTab_Fn_Inst may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsTab_Fn_Inst = Nothing
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		
		'UPGRADE_NOTE: Object lrecTab_Fn_Inst may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecTab_Fn_Inst = Nothing
		'UPGRADE_NOTE: Object lclsTab_Fn_Inst may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsTab_Fn_Inst = Nothing
		
		On Error GoTo 0
	End Function
	
	'* Item: Devuelve un elemento de la colecci�n (segun �ndice)
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Tab_Fn_Inst
		Get
			Item = mCol.Item(vntIndexKey)
		End Get
	End Property
	
	'* Count: Devuelve el n�mero de elementos que posee la colecci�n
	Public ReadOnly Property Count() As Integer
		Get
			Count = mCol.Count()
		End Get
	End Property
	
	'* NewEnum: Permite enumerar la colecci�n para utilizarla en un ciclo For Each... Next
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
	
	'* Remove: Elimina un elemento de la colecci�n
	Public Sub Remove(ByRef vntIndexKey As Object)
		mCol.Remove(vntIndexKey)
	End Sub
	
	'* Class_Initialize: Controla la creaci�n de una instancia de la colecci�n
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		mCol = New Collection
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'* Class_Terminate: Controla la destrucci�n de una instancia de la colecci�n
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






