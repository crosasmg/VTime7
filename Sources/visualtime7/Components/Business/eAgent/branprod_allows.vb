Option Strict Off
Option Explicit On
Public Class branprod_allows
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: branprod_allows.cls                      $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:41p                                $%'
	'% $Revision:: 11                                       $%'
	'%-------------------------------------------------------%'
	
	Private mcolbranprod_allow As Collection
	
	'%Add: Añade una nueva instancia de la clase "branprod_allow" a la colección
	Public Function Add(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nModulec As Integer, ByVal nInstallments As Integer, ByVal nStartMonth As Integer, ByVal nEndMonth As Integer, Optional ByRef sKey As String = "") As branprod_allow
		Dim lobjbranprod_allow As branprod_allow
		
		lobjbranprod_allow = New branprod_allow
		
		With lobjbranprod_allow
			.nBranch = nBranch
			.nProduct = nProduct
			.nModulec = nModulec
			.nInstallments = nInstallments
			.nStartMonth = nStartMonth
			.nEndMonth = nEndMonth
		End With
		
		'+ Setea las propiedades pasadas al método
		If sKey = String.Empty Then
			mcolbranprod_allow.Add(lobjbranprod_allow)
		Else
			mcolbranprod_allow.Add(lobjbranprod_allow, sKey)
		End If
		
		'+ Retorna el objeto creado
		
		Add = lobjbranprod_allow
		'UPGRADE_NOTE: Object lobjbranprod_allow may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjbranprod_allow = Nothing
	End Function
	
	'Find: Función que realiza la busqueda en la tabla 'branprod_allow'
	Public Function Find(ByVal nIntermed As Integer, Optional ByVal nBranch As Integer = 0, Optional ByVal nProduct As Integer = 0) As Boolean
		Dim lrecbranprod_allow As eRemoteDB.Execute
		
		lrecbranprod_allow = New eRemoteDB.Execute
		
		On Error GoTo Find_Err
		
		'+ Define dynamic parameters for Stored Procedures. Read on 05/22/01
		With lrecbranprod_allow
			.StoredProcedure = "reabranprod_allow"
			.Parameters.Add("PnIntermed", nIntermed, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", IIf(nBranch = 0, eRemoteDB.Constants.intNull, nBranch), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", IIf(nProduct = 0, eRemoteDB.Constants.intNull, nProduct), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDuration", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(True) Then
				Do While Not .EOF
					Call Add(.FieldToClass("nBranch"), .FieldToClass("nProduct"), .FieldToClass("nModulec"), .FieldToClass("nInstallments"), .FieldToClass("nStartMonth"), .FieldToClass("nEndMonth"))
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
		'UPGRADE_NOTE: Object lrecbranprod_allow may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecbranprod_allow = Nothing
	End Function
	
	'***Item: Returns an element of the collection (acording to the index)
	'*Item: Devuelve un elemento de la colección (segun índice)
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As branprod_allow
		Get
			Item = mcolbranprod_allow.Item(vntIndexKey)
		End Get
	End Property
	
	'***Count: Returns the number of elements that the collection has
	'*Count: Devuelve el número de elementos que posee la colección
	Public ReadOnly Property Count() As Integer
		Get
			Count = mcolbranprod_allow.Count()
		End Get
	End Property
	
	'***NewEnum: Enumerates the collection for use in a For Each...Next loop
	'*NewEnum: Permite enumerar la colección para utilizarla en un ciclo For Each... Next
	'UPGRADE_NOTE: NewEnum property was commented out. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="B3FC1610-34F3-43F5-86B7-16C984F0E88E"'
	'Public ReadOnly Property NewEnum() As stdole.IUnknown
		'Get
			'NewEnum = mcolbranprod_allow._NewEnum
		'End Get
	'End Property
	
	Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
		'UPGRADE_TODO: Uncomment and change the following line to return the collection enumerator. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="95F9AAD0-1319-4921-95F0-B9D3C4FF7F1C"'
		GetEnumerator = mcolbranprod_allow.GetEnumerator
	End Function
	
	'**%Remove: Deletes an element from the collection
	'%Remove: Elimina un elemento de la colección
	Public Sub Remove(ByRef vntIndexKey As Object)
		mcolbranprod_allow.Remove(vntIndexKey)
	End Sub
	
	'**%Class_Initialize: Controls the creation of an instance of the collection
	'%Class_Initialize: Controla la creación de una instancia de la colección
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		mcolbranprod_allow = New Collection
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	'**%Class_Terminate: Controls the destruction of an instance of the collection
	'%Class_Terminate: Controla la destrucción de una instancia de la colección
	'UPGRADE_NOTE: Class_Terminate was upgraded to Class_Terminate_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Terminate_Renamed()
		'UPGRADE_NOTE: Object mcolbranprod_allow may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mcolbranprod_allow = Nothing
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
End Class






