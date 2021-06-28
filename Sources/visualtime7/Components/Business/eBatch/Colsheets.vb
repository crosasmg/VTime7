Option Strict Off
Option Explicit On
Public Class Colsheets
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: Colsheets.cls                            $%'
	'% $Author:: Nvaplat41                                  $%'
	'% $Date:: 10/10/03 17.34                               $%'
	'% $Revision:: 9                                        $%'
	'%-------------------------------------------------------%'
	
	'-Variable local de la coleccion
	Private mCol As Collection
	
	'% Add: Añade una nueva instancia de la clase Colsheet a la colección
	Public Function Add(ByVal objClass As Colsheet) As Colsheet
		
		If objClass Is Nothing Then
			objClass = New Colsheet
		End If
		
		With objClass
			mCol.Add(objClass, .nId & .nIdRec & .nSheet & .nOrder & .sColumnName)
		End With
		
		'+Retorna el elemento creado
		Add = objClass
		
	End Function
	
	'%Item: Devuelve un elemento de la colección (segun índice)
	'-----------------------------------------------------------
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Colsheet
		Get
			'-----------------------------------------------------------
			
			Item = mCol.Item(vntIndexKey)
			
		End Get
	End Property
	
	'%Count: Devuelve el número de elementos que posee la colección
	Public ReadOnly Property Count() As Integer
		Get
			Count = mCol.Count()
		End Get
	End Property
	
	'% NewEnum: Permite enumerar la colección para utilizarla en un ciclo For Each... Next
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
	
	'%FindCA051(). Esta funcion carga en la coleccion los registros a ser utilizados en la
	'%ventana CA051.
	Public Function FindCA051(ByVal nId As Integer) As Boolean
		Dim lrecreaColSheet As eRemoteDB.Execute
		Dim lclsColsheet As Colsheet
		
		On Error GoTo FindCa051_Err
		
		lrecreaColSheet = New eRemoteDB.Execute
		
		'+Definición de parámetros para stored procedure 'insudb.reaColSheet'
		'+Información leída el 31/01/2001 03:50:05 p.m.
		
		With lrecreaColSheet
			.StoredProcedure = "reaColSheet"
			.Parameters.Add("nId", nId, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				If Not .EOF Then
					FindCA051 = True
				End If
				Do While Not .EOF
					lclsColsheet = New Colsheet
					With lclsColsheet
						.sExists = lrecreaColSheet.FieldToClass("sExists", 0)
						.sSheet = lrecreaColSheet.FieldToClass("sSheet", "")
						.sSel = lrecreaColSheet.FieldToClass("sSel", 0)
						.sColumnName = lrecreaColSheet.FieldToClass("sColumnName", "")
                        .sDefaultValue = lrecreaColSheet.FieldToClass("sValue", "")
						.nOrder = lrecreaColSheet.FieldToClass("nOrder", 0)
						.sRequire = lrecreaColSheet.FieldToClass("sRequire", "")
						.sGroupRequire = lrecreaColSheet.FieldToClass("GroupRequire", "")
						.sValuesList = String.Empty
						.sPossibleValues = String.Empty
						.sComment = String.Empty
						.sField = lrecreaColSheet.FieldToClass("sField", String.Empty)
						.sTableName = String.Empty
						.sSelected = lrecreaColSheet.FieldToClass("sSelected", String.Empty)
						.nId = nId
						.nIdRec = lrecreaColSheet.FieldToClass("nIdRec", 0)
						.nBranch = eRemoteDB.Constants.intNull
						.nProduct = eRemoteDB.Constants.intNull
						.npolicy = eRemoteDB.Constants.intNull
						.nSheet = lrecreaColSheet.FieldToClass("nSheet", 0)
					End With
					Call Add(lclsColsheet)
					.RNext()
				Loop 
				.RCloseRec()
			End If
		End With
		
FindCa051_Err: 
		If Err.Number Then
			FindCA051 = False
		End If
		On Error GoTo 0
		
		'UPGRADE_NOTE: Object lrecreaColSheet may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaColSheet = Nothing
		
	End Function
	
	'%FindSheet. Esta funcion carga en la coleccion los registros de las columnas activas
	Public Function FindSheet(ByVal nId As Integer) As Boolean
		Dim lrecreaColSheet As eRemoteDB.Execute
		
		Dim lclsColsheet As Colsheet
		
		On Error GoTo FindSheet_Err
		
		lrecreaColSheet = New eRemoteDB.Execute
		
		'+Definición de parámetros para stored procedure 'insudb.reaColSheet'
		'+Información leída el 31/01/2001 03:50:05 p.m.
		With lrecreaColSheet
			.StoredProcedure = "reaColWorkSheet"
			.Parameters.Add("nId", nId, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				If Not .EOF Then
					FindSheet = True
				End If
				Do While Not .EOF
					lclsColsheet = New Colsheet
					With lclsColsheet
						.sExists = "0"
						.sSheet = lrecreaColSheet.FieldToClass("sSheet", "")
						.sSel = String.Empty
						.sColumnName = lrecreaColSheet.FieldToClass("sColumnName", "")
                        .sDefaultValue = lrecreaColSheet.FieldToClass("sValue", "")
						.nOrder = lrecreaColSheet.FieldToClass("nOrder", 0)
						.sRequire = lrecreaColSheet.FieldToClass("sRequire", "")
						.sGroupRequire = String.Empty
						.sValuesList = lrecreaColSheet.FieldToClass("sValuesList")
						.sPossibleValues = lrecreaColSheet.FieldToClass("sPossibleValues")
						.sComment = lrecreaColSheet.FieldToClass("sComment", String.Empty)
						.sField = lrecreaColSheet.FieldToClass("sField", String.Empty)
						.sTableName = lrecreaColSheet.FieldToClass("sTable", String.Empty)
						.sSelected = lrecreaColSheet.FieldToClass("sSelected", String.Empty)
						.nId = nId
						.nIdRec = lrecreaColSheet.FieldToClass("nIdRec", 0)
						.nBranch = lrecreaColSheet.FieldToClass("nBranch", eRemoteDB.Constants.intNull)
						.nProduct = lrecreaColSheet.FieldToClass("nProduct", eRemoteDB.Constants.intNull)
						.npolicy = lrecreaColSheet.FieldToClass("nPolicy", eRemoteDB.Constants.intNull)
						.nSheet = lrecreaColSheet.FieldToClass("nSheet", 0)
					End With
					Call Add(lclsColsheet)
					.RNext()
				Loop 
				.RCloseRec()
			End If
		End With
		
FindSheet_Err: 
		If Err.Number Then
			FindSheet = False
		End If
		On Error GoTo 0
		
		'UPGRADE_NOTE: Object lrecreaColSheet may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaColSheet = Nothing
		
	End Function
	
	'%Find. Esta funcion carga en la coleccion los registros de colsheet relacionados con group_columns
	Public Function Find(ByVal nId As Integer) As Boolean
		Dim lrecreaColSheet As eRemoteDB.Execute
		Dim lclsColsheet As Colsheet
		
		On Error GoTo Find_Err
		
		lrecreaColSheet = New eRemoteDB.Execute
		
		'Definición de parámetros para stored procedure 'insudb.reaColSheet'
		'Información leída el 31/01/2001 03:50:05 p.m.
		
		With lrecreaColSheet
			.StoredProcedure = "reaColSheet_1pkg.reaColSheet_1"
			.Parameters.Add("nId", nId, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				If Not .EOF Then
					Find = True
				End If
				Do While Not .EOF
					lclsColsheet = New Colsheet
					With lclsColsheet
						.sExists = String.Empty
						.sSheet = String.Empty
						.sSel = String.Empty
						.sColumnName = lrecreaColSheet.FieldToClass("sColumnName", "")
						.sDefaultValue = lrecreaColSheet.FieldToClass("sValue", "")
						.nOrder = lrecreaColSheet.FieldToClass("nOrder", 0)
						.sRequire = lrecreaColSheet.FieldToClass("sRequire", "")
						.sGroupRequire = String.Empty
						.sValuesList = lrecreaColSheet.FieldToClass("sValuesList")
						.sPossibleValues = String.Empty
						.sComment = String.Empty
						.sField = lrecreaColSheet.FieldToClass("sField", String.Empty)
						.sTableName = lrecreaColSheet.FieldToClass("sTable", String.Empty)
						.sSelected = lrecreaColSheet.FieldToClass("sSelected", String.Empty)
						.nId = nId
						.nIdRec = lrecreaColSheet.FieldToClass("nIdRec", 0)
						.nBranch = eRemoteDB.Constants.intNull
						.nProduct = eRemoteDB.Constants.intNull
						.npolicy = eRemoteDB.Constants.intNull
						.nSheet = lrecreaColSheet.FieldToClass("nSheet", 0)
						.sData_Type = lrecreaColSheet.FieldToClass("sData_Type", "CHAR")
						.nData_Length = lrecreaColSheet.FieldToClass("nData_Length", 0)
						.nData_Precision = lrecreaColSheet.FieldToClass("nData_Precision", 0)
						.nData_Scale = lrecreaColSheet.FieldToClass("nData_Scale", 0)
					End With
					Call Add(lclsColsheet)
					.RNext()
				Loop 
				.RCloseRec()
			End If
		End With
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecreaColSheet may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaColSheet = Nothing
	End Function
End Class






