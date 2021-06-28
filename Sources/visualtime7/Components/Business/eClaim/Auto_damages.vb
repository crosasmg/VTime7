Option Strict Off
Option Explicit On
Public Class Auto_damages
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: Auto_damages.cls                         $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:34p                                $%'
	'% $Revision:: 7                                        $%'
	'%-------------------------------------------------------%'
	
	'local variable to hold collection
	Private mCol As Collection
	
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Auto_damage
		Get
			'used when referencing an element in the collection
			'vntIndexKey contains either the Index or Key to the collection,
			'this is why it is declared as a Variant
			'Syntax: Set foo = x.Item(xyz) or Set foo = x.Item(5)
			Item = mCol.Item(vntIndexKey)
		End Get
	End Property
	
	Public ReadOnly Property Count() As Integer
		Get
			'used when retrieving the number of elements in the
			'collection. Syntax: Debug.Print x.Count
			Count = mCol.Count()
		End Get
	End Property
	
	'UPGRADE_NOTE: NewEnum property was commented out. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="B3FC1610-34F3-43F5-86B7-16C984F0E88E"'
	'Public ReadOnly Property NewEnum() As stdole.IUnknown
		'Get
			'this property allows you to enumerate
			'this collection with the For...Each syntax
			'NewEnum = mCol._NewEnum
		'End Get
	'End Property
	
	Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
				GetEnumerator = mCol.GetEnumerator
	End Function
	
	Public Sub Remove(ByRef vntIndexKey As Object)
		'used when removing an element from the collection
		'vntIndexKey contains either the Index or Key, which is why
		'it is declared as a Variant
		'Syntax: x.Remove(xyz)
		mCol.Remove(vntIndexKey)
	End Sub
	
	Private Sub Class_Initialize_Renamed()
		'creates the collection when this class is created
		mCol = New Collection
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	Private Sub Class_Terminate_Renamed()
		'destroys collection when this class is terminated
		mCol = Nothing
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
	
	Public Function Add(ByVal lclsauto_damage As Auto_damage) As Auto_damage
		mCol.Add(lclsauto_damage)
		
		'+Devolver el objeto creado
		Add = lclsauto_damage
	End Function
	'%Find : Esta función se encarga de de buscar la colección de datos de acuerdo
	'%a el ramo, producto, modulo, cobertura y fecha
	Public Function Find(ByVal nServ_Order As Double) As Boolean
		Dim lrecreaauto_damage As eRemoteDB.Execute
		Dim lclsauto_damage As Auto_damage
		
		On Error GoTo reaauto_damage_Err
		
		lrecreaauto_damage = New eRemoteDB.Execute
		
		'+
		'+ Definición de store procedure reaTar_fire_fh al 04-04-2002 13:14:42
		'+
		With lrecreaauto_damage
			.StoredProcedure = "reaauto_damage"
			.Parameters.Add("nServ_order", nServ_Order, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				Find = True
				Do While Not .EOF
					lclsauto_damage = New Auto_damage
					lclsauto_damage.nServ_Order = nServ_Order
					lclsauto_damage.nPart_auto = .FieldToClass("nPart_auto")
					lclsauto_damage.nDamag_auto = .FieldToClass("nDamag_auto")
					lclsauto_damage.nDamage_magnif = .FieldToClass("nDamage_magnif")
					lclsauto_damage.nDeduc = .FieldToClass("nDeduc")
					lclsauto_damage.dCompdate = .FieldToClass("dCompdate")
					lclsauto_damage.nUsercode = .FieldToClass("nUsercode")
					Call Add(lclsauto_damage)
					lclsauto_damage = Nothing
					.RNext()
				Loop 
				
			Else
				Find = False
			End If
		End With
		
reaauto_damage_Err: 
		If Err.Number Then
			Find = False
		End If
		lrecreaauto_damage = Nothing
		On Error GoTo 0
	End Function
End Class






