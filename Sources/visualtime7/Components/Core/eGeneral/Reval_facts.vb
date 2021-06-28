Option Strict Off
Option Explicit On
Public Class Reval_facts
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: Reval_facts.cls                          $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:24p                                $%'
	'% $Revision:: 5                                        $%'
	'%-------------------------------------------------------%'
	
	Private mCol As Collection
	
	'% Add: Agrega un elemento a la colección.
	Public Function Add(ByRef lclsReval_fact As Reval_fact) As Reval_fact
		'set the properties passed into the method
		mCol.Add(lclsReval_fact)
		
		'return the object created
		Add = lclsReval_fact
		'UPGRADE_NOTE: Object lclsReval_fact may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsReval_fact = Nothing
		
	End Function
	
	'% Find: Función que realiza la busqueda en la tabla 'Reval_fact'.
	Public Function Find(ByVal nEcon_area As Integer) As Boolean
		Dim lclsReval_fact As eRemoteDB.Execute
		Dim lclsReval_factItem As Reval_fact
		
		lclsReval_fact = New eRemoteDB.Execute
		
		'+ Define all parameters for the stored procedures 'insudb.reaReval_fact'. Generated on 21/08/2002 04:25:17 p.m.
		With lclsReval_fact
			.StoredProcedure = "REAREVAL_FACT"
			.Parameters.Add("nEcon_area", nEcon_area, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(True) Then
				Do While Not .EOF
					lclsReval_factItem = New Reval_fact
					lclsReval_factItem.nEcon_area = .FieldToClass("nEcon_area")
					lclsReval_factItem.nYear = .FieldToClass("nYear")
					lclsReval_factItem.nMonth = .FieldToClass("nMonth")
					lclsReval_factItem.nIndexfac = .FieldToClass("nIndexfac")
					Call Add(lclsReval_factItem)
					'UPGRADE_NOTE: Object lclsReval_factItem may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					lclsReval_factItem = Nothing
					.RNext()
				Loop 
				Find = True
				.RCloseRec()
				
			Else
				Find = False
			End If
		End With
		'UPGRADE_NOTE: Object lclsReval_fact may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsReval_fact = Nothing
	End Function
	'* Item: Devuelve un elemento de la colección (segun índice)
	'-----------------------------------------------------------
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Reval_fact
		Get
			'-----------------------------------------------------------
			Item = mCol.Item(vntIndexKey)
		End Get
	End Property
	
	'* Count: Devuelve el número de elementos que posee la colección
	Public ReadOnly Property Count() As Integer
		Get
			Count = mCol.Count()
		End Get
	End Property
	
	'* NewEnum: Permite enumerar la colección para utilizarla en un ciclo For Each... Next
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
	
	'% Remove: Elimina un elemento de la colección
	'---------------------------------------------
	Public Sub Remove(ByRef vntIndexKey As Object)
		'---------------------------------------------
		mCol.Remove(vntIndexKey)
	End Sub
	
	'% Class_Initialize: Controla la creación de una instancia de la colección
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		mCol = New Collection
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'% Class_Terminate: Controla la destrucción de una instancia de la colección
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






