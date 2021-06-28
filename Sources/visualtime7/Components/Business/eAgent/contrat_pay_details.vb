Option Strict Off
Option Explicit On
<System.Runtime.InteropServices.ProgId("Contrat_Pay_Details_NET.Contrat_Pay_Details")> Public Class Contrat_Pay_Details
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: Contrat_Pay_Details.cls                          $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:38p                                $%'
	'% $Revision:: 5                                        $%'
	'%-------------------------------------------------------%'
	
	'- Variables locales
	Private mCol As Collection
	
	'% Add: Añade una nueva instancia de la clase Contrat_Pay_Detail a la colección
	Public Function Add(ByRef objClass As contrat_pay_detail) As contrat_pay_detail
		'create a new object
		If objClass Is Nothing Then
			objClass = New contrat_pay_detail
		End If
		
		With objClass
			mCol.Add(objClass, .nContrat_Pay & .nSeq & .nCode & .nInit_Dur & .nEnd_Dur & .nPercent_detail)
		End With
		
		'return the object created
		Add = objClass
		'UPGRADE_NOTE: Object objClass may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		objClass = Nothing
	End Function
	
	'% Find: Devuelve una coleccion de objetos de tipo Contrat_Pay_Detail
	Public Function Find(ByVal nContrat_Pay As String) As Boolean
		'- Se define la variable lrecContrat_Pay_Detail que se utilizará como cursor.
		Dim lrecContrat_Pay_Detail As eRemoteDB.Execute
		Dim lclsContrat_Pay_Detail As contrat_pay_detail
		
		On Error GoTo Find_Err
		lrecContrat_Pay_Detail = New eRemoteDB.Execute
		'+ Se ejecuta el store procedure que busca los vehículos
		With lrecContrat_Pay_Detail
			.StoredProcedure = "reaContrat_Pay_Detail_a"
			.Parameters.Add("nContrat_Pay", nContrat_Pay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Find = True
				Do While Not .EOF
					lclsContrat_Pay_Detail = New contrat_pay_detail
					lclsContrat_Pay_Detail.nContrat_Pay = CInt(nContrat_Pay)
					lclsContrat_Pay_Detail.nSeq = .FieldToClass("nSeq")
					lclsContrat_Pay_Detail.nCode = .FieldToClass("nCode")
					lclsContrat_Pay_Detail.nInit_Dur = .FieldToClass("nInit_Dur")
					lclsContrat_Pay_Detail.nEnd_Dur = .FieldToClass("nEnd_Dur")
					lclsContrat_Pay_Detail.nPercent_detail = .FieldToClass("nPercent")
					Call Add(lclsContrat_Pay_Detail)
					'UPGRADE_NOTE: Object lclsContrat_Pay_Detail may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					lclsContrat_Pay_Detail = Nothing
					.RNext()
				Loop 
			End If
		End With
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		'UPGRADE_NOTE: Object lrecContrat_Pay_Detail may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecContrat_Pay_Detail = Nothing
		'UPGRADE_NOTE: Object lclsContrat_Pay_Detail may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsContrat_Pay_Detail = Nothing
		On Error GoTo 0
		
	End Function
	
	'%Item: Devuelve un elemento de la colección (segun índice)
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As contrat_pay_detail
		Get
			Item = mCol.Item(vntIndexKey)
		End Get
	End Property
	
	'%Count: Devuelve el número de elementos que posee la colección
	Public ReadOnly Property Count() As Integer
		Get
			Count = mCol.Count()
		End Get
	End Property
	
	'%NewEnum: Permite enumerar la colección para utilizarla en un ciclo For Each... Next
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
	
	'%Remove: Elimina un elemento de la colección
	Public Sub Remove(ByRef vntIndexKey As Object)
		mCol.Remove(vntIndexKey)
	End Sub
	
	'%Class_Initialize: Controla la creación de una instancia de la colección
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		mCol = New Collection
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'%Class_Terminate: Controla la destrucción de una instancia de la colección
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






