Option Strict Off
Option Explicit On
Public Class pay_ord_conceptss
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: pay_ord_conceptss.cls                    $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:35p                                $%'
	'% $Revision:: 4                                        $%'
	'%-------------------------------------------------------%'
	
	Private mcolpay_ord_concepts As Collection
	
	Public Function Add(ByRef nCompany As Integer, ByRef nConcept As Integer, ByRef sStatregt As String, Optional ByRef pstrKey As String = "") As pay_ord_concepts
		Dim lclspay_ord_concepts As pay_ord_concepts
		
		lclspay_ord_concepts = New pay_ord_concepts
		
		With lclspay_ord_concepts
			.nCompany = nCompany
			.nConcept = nConcept
			.sStatregt = sStatregt
		End With
		
		'set the properties passed into the method
		If pstrKey = String.Empty Then
			mcolpay_ord_concepts.Add(lclspay_ord_concepts)
		Else
			mcolpay_ord_concepts.Add(lclspay_ord_concepts, pstrKey)
		End If
		
		'return the object created
		Add = lclspay_ord_concepts
		'UPGRADE_NOTE: Object lclspay_ord_concepts may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclspay_ord_concepts = Nothing
	End Function
	
	'Find: Función que realiza la busqueda en la tabla 'pay_ord_concepts'
	Public Function Find(ByRef nCompany As Integer) As Boolean
		Dim lclspay_ord_concepts As eRemoteDB.Execute
		
		lclspay_ord_concepts = New eRemoteDB.Execute
		
		'+ Define all parameters for the stored procedures 'insudb.reapay_ord_concepts_a'.
		With lclspay_ord_concepts
			.StoredProcedure = "reapay_ord_concepts_a"
			.Parameters.Add("nCompany", nCompany, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
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
		'UPGRADE_NOTE: Object lclspay_ord_concepts may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclspay_ord_concepts = Nothing
	End Function
	
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As pay_ord_concepts
		Get
			Item = mcolpay_ord_concepts.Item(vntIndexKey)
		End Get
	End Property
	
	Public ReadOnly Property Count() As Integer
		Get
			Count = mcolpay_ord_concepts.Count()
		End Get
	End Property
	
	'UPGRADE_NOTE: NewEnum property was commented out. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="B3FC1610-34F3-43F5-86B7-16C984F0E88E"'
	'Public ReadOnly Property NewEnum() As stdole.IUnknown
		'Get
			'NewEnum = mcolpay_ord_concepts._NewEnum
		'End Get
	'End Property
	
	Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
		'UPGRADE_TODO: Uncomment and change the following line to return the collection enumerator. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="95F9AAD0-1319-4921-95F0-B9D3C4FF7F1C"'
		GetEnumerator = mcolpay_ord_concepts.GetEnumerator
	End Function
	
	Public Sub Remove(ByRef vntIndexKey As Object)
		mcolpay_ord_concepts.Remove(vntIndexKey)
	End Sub
	
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		mcolpay_ord_concepts = New Collection
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'UPGRADE_NOTE: Class_Terminate was upgraded to Class_Terminate_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Terminate_Renamed()
		'UPGRADE_NOTE: Object mcolpay_ord_concepts may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mcolpay_ord_concepts = Nothing
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
End Class






