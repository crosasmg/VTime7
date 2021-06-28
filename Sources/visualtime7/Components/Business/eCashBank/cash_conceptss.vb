Option Strict Off
Option Explicit On
Public Class cash_conceptss
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: cash_conceptss.cls                       $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:35p                                $%'
	'% $Revision:: 3                                        $%'
	'%-------------------------------------------------------%'
	
	Private mcolcash_concepts As Collection
	
	Public Function Add(ByVal nCompany As String, ByVal nConcept As Integer, ByVal sStatregt As String, Optional ByRef pstrKey As String = "") As cash_concepts
		Dim lclscash_concepts As cash_concepts
		
		lclscash_concepts = New cash_concepts
		
		With lclscash_concepts
			.nCompany = CInt(nCompany)
			.nConcept = nConcept
			.sStatregt = sStatregt
		End With
		
		'set the properties passed into the method
		If pstrKey = String.Empty Then
			mcolcash_concepts.Add(lclscash_concepts)
		Else
			mcolcash_concepts.Add(lclscash_concepts, pstrKey)
		End If
		
		'return the object created
		Add = lclscash_concepts
		'UPGRADE_NOTE: Object lclscash_concepts may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclscash_concepts = Nothing
	End Function
	
	'Find: Función que realiza la busqueda en la tabla 'cash_concepts'
	Public Function Find(ByVal nUsercode As Integer, ByVal nCompany As String) As Boolean
		Dim lclscash_concepts As eRemoteDB.Execute
		
		lclscash_concepts = New eRemoteDB.Execute
		
		'+ Define all parameters for the stored procedures 'insudb.reacash_concepts'. Generated on 20/12/2001 10:43:14 a.m.
		With lclscash_concepts
			.StoredProcedure = "reacash_concepts_a"
			.Parameters.Add("nCompany", nCompany, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(True) Then
				Do While Not .EOF
					Call Add(.FieldToClass("nCompany"), .FieldToClass("nConcept"), .FieldToClass("sStatregt"))
					.RNext()
				Loop 
				Find = True
				.RCloseRec()
			Else
				Find = False
			End If
		End With
		'UPGRADE_NOTE: Object lclscash_concepts may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclscash_concepts = Nothing
	End Function
	
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As cash_concepts
		Get
			Item = mcolcash_concepts.Item(vntIndexKey)
		End Get
	End Property
	
	Public ReadOnly Property Count() As Integer
		Get
			Count = mcolcash_concepts.Count()
		End Get
	End Property
	
	'UPGRADE_NOTE: NewEnum property was commented out. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="B3FC1610-34F3-43F5-86B7-16C984F0E88E"'
	'Public ReadOnly Property NewEnum() As stdole.IUnknown
		'Get
			'NewEnum = mcolcash_concepts._NewEnum
		'End Get
	'End Property
	
	Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
		'UPGRADE_TODO: Uncomment and change the following line to return the collection enumerator. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="95F9AAD0-1319-4921-95F0-B9D3C4FF7F1C"'
		GetEnumerator = mcolcash_concepts.GetEnumerator
	End Function
	
	Public Sub Remove(ByRef vntIndexKey As Object)
		mcolcash_concepts.Remove(vntIndexKey)
	End Sub
	
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		mcolcash_concepts = New Collection
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'UPGRADE_NOTE: Class_Terminate was upgraded to Class_Terminate_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Terminate_Renamed()
		'UPGRADE_NOTE: Object mcolcash_concepts may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mcolcash_concepts = Nothing
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
End Class






