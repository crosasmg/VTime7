Option Strict Off
Option Explicit On
Public Class Tab_Clauses
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: Tab_Clauses.cls                          $%'
	'% $Author:: Nvaplat18                                  $%'
	'% $Date:: 26/08/03 16.30                               $%'
	'% $Revision:: 8                                        $%'
	'%-------------------------------------------------------%'
	
	'-Variable Local para la coleccion de registros
	Private mCol As Collection
	
	'* Add:Añade una nueva instancia de la clase Tab_Clause a la colección
	Public Function Add(ByRef oTab_clause As Tab_Clause) As Tab_Clause
		mCol.Add(oTab_clause)
		Add = oTab_clause
		'UPGRADE_NOTE: Object oTab_clause may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		oTab_clause = Nothing
	End Function
	
	'% Find: Permite cargar la colección con las cláusulas definidas en el producto
	'%       o validar si el producto tiene clausulas asociadas
	Public Function Find(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, Optional ByVal bOnlyToValidate As Boolean = False) As Boolean
		Dim lrecreaTab_Clause_a As eRemoteDB.Execute
		Dim lclsTab_clause As Tab_Clause
		
		On Error GoTo Find_Err
		
		lrecreaTab_Clause_a = New eRemoteDB.Execute
		lclsTab_clause = New Tab_Clause
		
		'+Definición de parámetros para stored procedure 'insudb.reaTab_Clause_a'
		'+Información leída el 11/04/2001 13:24:24
		
		With lrecreaTab_Clause_a
			.StoredProcedure = "reaTab_Clause_a"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				'+Si es solo para validar la existencia, no se cargan los registros
				If bOnlyToValidate Then
					Find = Not .EOF
				Else
					Do While Not .EOF
						lclsTab_clause = New Tab_Clause
						lclsTab_clause.nBranch = nBranch
						lclsTab_clause.nProduct = nProduct
						lclsTab_clause.nClause = .FieldToClass("nClause")
						lclsTab_clause.dEffecdate = .FieldToClass("dEffecdate")
						lclsTab_clause.sDefaulti = .FieldToClass("sDefaulti")
						lclsTab_clause.sDescript = .FieldToClass("sDescript")
						lclsTab_clause.nNotenum = .FieldToClass("nNoteNum")
						lclsTab_clause.dNulldate = .FieldToClass("dNulldate")
						lclsTab_clause.sShort_des = .FieldToClass("sShort_des")
						lclsTab_clause.nModulec = .FieldToClass("nModulec")
						lclsTab_clause.nCover = .FieldToClass("nCover")
						lclsTab_clause.nType = .FieldToClass("nType")
						lclsTab_clause.sType_clause = .FieldToClass("sType_clause")
						lclsTab_clause.sDoc_attach = .FieldToClass("sDoc_attach")
						lclsTab_clause.nOrden = .FieldToClass("nOrden")
						lclsTab_clause.sModified = .FieldToClass("sModified")
						
						Call Add(lclsTab_clause)
						.RNext()
					Loop 
					Find = True
				End If
				.RCloseRec()
			End If
		End With
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsTab_clause may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsTab_clause = Nothing
		'UPGRADE_NOTE: Object lrecreaTab_Clause_a may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaTab_Clause_a = Nothing
	End Function
	
	'% Item: Retorna un elemento de la coleccion
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Tab_Clause
		Get
			Item = mCol.Item(vntIndexKey)
		End Get
	End Property
	
	'% Count: Cantidad de registros en la coleccion
	Public ReadOnly Property Count() As Integer
		Get
			Count = mCol.Count()
		End Get
	End Property
	
	'%NewEnum: Permite recorrer la coleccion de registros
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
	
	'%Remove : Elimina un registro de la coleccion
	Public Sub Remove(ByRef vntIndexKey As Object)
		mCol.Remove(vntIndexKey)
	End Sub
	
	'% Class_Initialize: Creación de clase
	'-------------------------------------------------------------
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		'-------------------------------------------------------------
		mCol = New Collection
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'%Class_Terminate: Destrucción de clase
	'-------------------------------------------------------------
	'UPGRADE_NOTE: Class_Terminate was upgraded to Class_Terminate_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Terminate_Renamed()
		'-------------------------------------------------------------
		'UPGRADE_NOTE: Object mCol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mCol = Nothing
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
End Class






