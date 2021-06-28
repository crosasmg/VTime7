Option Strict Off
Option Explicit On
Public Class gen_bonsups
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: gen_bonsups.cls                          $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:41p                                $%'
	'% $Revision:: 3                                        $%'
	'%-------------------------------------------------------%'
	
	Private mcolgen_bonsup As Collection
	
	'%Add: Añade una nueva instancia de la clase "gen_bonsup" a la colección
	Public Function Add(ByVal nInit_Range As Double, ByVal nEnd_Range As Double, ByVal nFactor As Double, Optional ByRef sKey As String = "") As gen_bonsup
		Dim lclsgen_bonsup As gen_bonsup
		
		lclsgen_bonsup = New gen_bonsup
		
		With lclsgen_bonsup
			.nInit_Range = nInit_Range
			.nEnd_Range = nEnd_Range
			.nFactor = nFactor
		End With
		
		'set the properties passed into the method
		If sKey = String.Empty Then
			mcolgen_bonsup.Add(lclsgen_bonsup)
		Else
			mcolgen_bonsup.Add(lclsgen_bonsup, sKey)
		End If
		
		'return the object created
		Add = lclsgen_bonsup
		'UPGRADE_NOTE: Object lclsgen_bonsup may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsgen_bonsup = Nothing
	End Function
	
	'Find: Función que realiza la busqueda en la tabla 'gen_bonsup'
	Public Function Find() As Boolean
		Dim lclsgen_bonsup As eRemoteDB.Execute
		
		lclsgen_bonsup = New eRemoteDB.Execute
		
		'+ Define all parameters for the stored procedures 'insudb.reagen_bonsup'. Generated on 14/12/2001 11:47:11 a.m.
		With lclsgen_bonsup
			.StoredProcedure = "reagen_bonsup_a"
			
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
		'UPGRADE_NOTE: Object lclsgen_bonsup may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsgen_bonsup = Nothing
	End Function
	
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As gen_bonsup
		Get
			Item = mcolgen_bonsup.Item(vntIndexKey)
		End Get
	End Property
	
	Public ReadOnly Property Count() As Integer
		Get
			Count = mcolgen_bonsup.Count()
		End Get
	End Property
	
	'UPGRADE_NOTE: NewEnum property was commented out. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="B3FC1610-34F3-43F5-86B7-16C984F0E88E"'
	'Public ReadOnly Property NewEnum() As stdole.IUnknown
		'Get
			'NewEnum = mcolgen_bonsup._NewEnum
		'End Get
	'End Property
	
	Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
		'UPGRADE_TODO: Uncomment and change the following line to return the collection enumerator. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="95F9AAD0-1319-4921-95F0-B9D3C4FF7F1C"'
		GetEnumerator = mcolgen_bonsup.GetEnumerator
	End Function
	
	Public Sub Remove(ByRef vntIndexKey As Object)
		mcolgen_bonsup.Remove(vntIndexKey)
	End Sub
	
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		mcolgen_bonsup = New Collection
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'UPGRADE_NOTE: Class_Terminate was upgraded to Class_Terminate_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Terminate_Renamed()
		'UPGRADE_NOTE: Object mcolgen_bonsup may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mcolgen_bonsup = Nothing
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
End Class






