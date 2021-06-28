Option Strict Off
Option Explicit On
Public Class average_prods
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: average_prods.cls                        $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:41p                                $%'
	'% $Revision:: 3                                        $%'
	'%-------------------------------------------------------%'
	
	Private mcolaverage_prod As Collection
	
	'%Add: Añade una nueva instancia de la clase "average_prod" a la colección
	Public Function Add(ByVal nInit_Range As Double, ByVal nEnd_Range As Double, ByVal nFactor As Double, Optional ByRef sKey As String = "") As average_prod
		Dim lclsaverage_prod As average_prod
		
		lclsaverage_prod = New average_prod
		
		With lclsaverage_prod
			.nInit_Range = nInit_Range
			.nEnd_Range = nEnd_Range
			.nFactor = nFactor
		End With
		
		'set the properties passed into the method
		If sKey = String.Empty Then
			mcolaverage_prod.Add(lclsaverage_prod)
		Else
			mcolaverage_prod.Add(lclsaverage_prod, sKey)
		End If
		
		'return the object created
		Add = lclsaverage_prod
		'UPGRADE_NOTE: Object lclsaverage_prod may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsaverage_prod = Nothing
	End Function
	
	'Find: Función que realiza la busqueda en la tabla 'average_prod'
	Public Function Find() As Boolean
		Dim lclsaverage_prod As eRemoteDB.Execute
		
		lclsaverage_prod = New eRemoteDB.Execute
		
		'+ Define all parameters for the stored procedures 'insudb.reaaverage_prod'. Generated on 14/12/2001 03:08:41 p.m.
		With lclsaverage_prod
			.StoredProcedure = "ReaAverage_Prod_a"
			
			If .Run(True) Then
				Do While Not .EOF
					Call Add(.FieldToClass("nInit_Range"), .FieldToClass("nEnd_Range"), .FieldToClass("nFactor"))
					.RNext()
				Loop 
				Find = True
				.RCloseRec()
			Else
				Find = False
			End If
		End With
		'UPGRADE_NOTE: Object lclsaverage_prod may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsaverage_prod = Nothing
	End Function
	
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As average_prod
		Get
			Item = mcolaverage_prod.Item(vntIndexKey)
		End Get
	End Property
	
	Public ReadOnly Property Count() As Integer
		Get
			Count = mcolaverage_prod.Count()
		End Get
	End Property
	
	'UPGRADE_NOTE: NewEnum property was commented out. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="B3FC1610-34F3-43F5-86B7-16C984F0E88E"'
	'Public ReadOnly Property NewEnum() As stdole.IUnknown
		'Get
			'NewEnum = mcolaverage_prod._NewEnum
		'End Get
	'End Property
	
	Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
		'UPGRADE_TODO: Uncomment and change the following line to return the collection enumerator. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="95F9AAD0-1319-4921-95F0-B9D3C4FF7F1C"'
		GetEnumerator = mcolaverage_prod.GetEnumerator
	End Function
	
	Public Sub Remove(ByRef vntIndexKey As Object)
		mcolaverage_prod.Remove(vntIndexKey)
	End Sub
	
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		mcolaverage_prod = New Collection
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'UPGRADE_NOTE: Class_Terminate was upgraded to Class_Terminate_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Terminate_Renamed()
		'UPGRADE_NOTE: Object mcolaverage_prod may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mcolaverage_prod = Nothing
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
End Class






