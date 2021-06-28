Option Strict Off
Option Explicit On
<System.Runtime.InteropServices.ProgId("Lend_Agree_Prods_NET.Lend_Agree_Prods")> Public Class Lend_Agree_Prods
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: Lend_Agree_Prods.cls                          $%'
	'% $Author:: lsereno                                   $%'
	'% $Date:: 03/05/07 12:35p                               $%'
	'% $Revision:: 01                                       $%'
	'%-------------------------------------------------------%'
	
	'+ Variable local para contener colección.
	
	Private mCol As Collection
	'% AddLend_Agree_Prod: Este método permite añadir registros a la colección.
	Public Function AddLend_Agree_Prod(ByRef ncod_agree As Integer) As Lend_Agree_Prod
		'+ Crear un nuevo objeto.
		Dim objNewMember As Lend_Agree_Prod
		
		'+ Establecer las propiedades que se transfieren al método.
		objNewMember = New Lend_Agree_Prod
		With objNewMember
			.ncod_agree = ncod_agree
		End With
		
		mCol.Add(objNewMember, "A" & CStr(ncod_agree))
		
		'+ Return the object created.
		
		AddLend_Agree_Prod = objNewMember
		'UPGRADE_NOTE: Object objNewMember may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		objNewMember = Nothing
	End Function
	
	'%Item: Devuelve un elemento de la colección (segun índice)
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Prod_Am_Bil
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
	
	'% FindLend_agree_Prod: Verifica que exista información por cobertura.
	Public Function FindLend_agree_Prod(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, Optional ByVal lblnFind As Boolean = True) As Boolean
		Dim lrecReaLend_agree_Prod As eRemoteDB.Execute
		
		lrecReaLend_agree_Prod = New eRemoteDB.Execute
		
		On Error GoTo FindLend_agree_Prod_Err
		
		FindLend_agree_Prod = True
		'UPGRADE_NOTE: Object mCol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mCol = Nothing
		mCol = New Collection
		
		'+ Definición de parámetros para stored procedure
		With lrecReaLend_agree_Prod
			.StoredProcedure = "Fnd_Lend_Agree_prod"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("deffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				Do While Not .EOF
					Call AddLend_Agree_Prod(.FieldToClass("NCOD_AGREE"))
					.RNext()
				Loop 
				.RCloseRec()
			End If
		End With
		'UPGRADE_NOTE: Object lrecReaLend_agree_Prod may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecReaLend_agree_Prod = Nothing
		
FindLend_agree_Prod_Err: 
		If Err.Number Then
			FindLend_agree_Prod = False
		End If
		On Error GoTo 0
	End Function
End Class






