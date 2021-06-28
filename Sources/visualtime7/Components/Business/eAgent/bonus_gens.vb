Option Strict Off
Option Explicit On
Public Class bonus_gens
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: bonus_gens.cls                           $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:41p                                $%'
	'% $Revision:: 3                                        $%'
	'%-------------------------------------------------------%'
	
	Private mcolbonus_gen As Collection
	
	'%Add: Añade una nueva instancia de la clase "bonus_gen" a la colección
	Public Function Add(ByRef nYear_Ini As Object, ByRef nYear_End As Object, ByRef nCurrency As Object, ByRef nMinAmount As Object, ByRef nPersist As Object, ByRef nReal_Goal As Object, Optional ByRef sKey As String = "") As bonus_gen
		Dim lclsbonus_gen As bonus_gen
		
		lclsbonus_gen = New bonus_gen
		
		With lclsbonus_gen
			.nYear_Ini = nYear_Ini
			.nYear_End = nYear_End
			.nCurrency = nCurrency
			.nMinAmount = nMinAmount
			.nPersist = nPersist
			.nReal_Goal = nReal_Goal
		End With
		
		'set the properties passed into the method
		If sKey = String.Empty Then
			mcolbonus_gen.Add(lclsbonus_gen)
		Else
			mcolbonus_gen.Add(lclsbonus_gen, sKey)
		End If
		
		'return the object created
		Add = lclsbonus_gen
		'UPGRADE_NOTE: Object lclsbonus_gen may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsbonus_gen = Nothing
	End Function
	
	'Find: Función que realiza la busqueda en la tabla 'bonus_gen'
	Public Function Find() As Boolean
		Dim lclsbonus_gen As eRemoteDB.Execute
		
		lclsbonus_gen = New eRemoteDB.Execute
		
		'+ Define all parameters for the stored procedures 'insudb.reabonus_gen_a'. Generated on 14/12/2001 11:47:11 a.m.
		With lclsbonus_gen
			.StoredProcedure = "reabonus_gen_a"
			
			If .Run(True) Then
				Do While Not .EOF
					Call Add(.FieldToClass("nYear_Ini"), .FieldToClass("nYear_End"), .FieldToClass("nCurrency"), .FieldToClass("nMinAmount"), .FieldToClass("nPersist"), .FieldToClass("nReal_Goal"))
					.RNext()
				Loop 
				Find = True
				.RCloseRec()
				
			Else
				Find = False
			End If
		End With
		'UPGRADE_NOTE: Object lclsbonus_gen may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsbonus_gen = Nothing
	End Function
	
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As bonus_gen
		Get
			Item = mcolbonus_gen.Item(vntIndexKey)
		End Get
	End Property
	
	Public ReadOnly Property Count() As Integer
		Get
			Count = mcolbonus_gen.Count()
		End Get
	End Property
	
	'UPGRADE_NOTE: NewEnum property was commented out. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="B3FC1610-34F3-43F5-86B7-16C984F0E88E"'
	'Public ReadOnly Property NewEnum() As stdole.IUnknown
		'Get
			'NewEnum = mcolbonus_gen._NewEnum
		'End Get
	'End Property
	
	Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
		'UPGRADE_TODO: Uncomment and change the following line to return the collection enumerator. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="95F9AAD0-1319-4921-95F0-B9D3C4FF7F1C"'
		GetEnumerator = mcolbonus_gen.GetEnumerator
	End Function
	
	Public Sub Remove(ByRef vntIndexKey As Object)
		mcolbonus_gen.Remove(vntIndexKey)
	End Sub
	
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		mcolbonus_gen = New Collection
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'UPGRADE_NOTE: Class_Terminate was upgraded to Class_Terminate_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Terminate_Renamed()
		'UPGRADE_NOTE: Object mcolbonus_gen may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mcolbonus_gen = Nothing
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
End Class






