Option Strict Off
Option Explicit On
Public Class Tab_reqexcs
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: Tab_reqexcs.cls                          $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 12:36p                               $%'
	'% $Revision:: 12                                       $%'
	'%-------------------------------------------------------%'
	
	Private mCol As Collection
	
	'- Variables auxiliares
	'- Se definen las variables que se van a utilizar para la búsqueda
	Private mlngBranch As Integer
	Private mlngProduct As Integer
	Private mdtmEffecdate As Date
	Private mlngReqexc1 As Integer
	
	'% Add: Añade una nueva instancia de la clase Tab_reqexc a la colección
	Public Function Add(ByRef objClass As Tab_reqexc) As Tab_reqexc
		If objClass Is Nothing Then
			objClass = New Tab_reqexc
		End If
		
		With objClass
			mCol.Add(objClass, .sType1 & "/" & .sType2 & "/" & .nBranch & "/" & .nProduct & "/" & .nCode1 & "/" & .nCode2 & "/" & .nRole1 & "/" & .nRole2 & "/" & .nDefReq & "/" & .dEffecdate.ToString("yyyyMMdd"))
		End With
		Add = objClass
	End Function
	
	'% Find: Carga la coleccion con los elementos de la tabla "Tab_reqexc"
	Public Function Find(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, Optional ByRef bFind As Boolean = False, Optional ByVal nDefReq As Integer = 1) As Boolean
		Dim lrecreaTab_reqexc As eRemoteDB.Execute
		Dim lclsTab_reqexc As Tab_reqexc
		
		On Error GoTo reaTab_reqexc_Err
		If mlngBranch <> nBranch Or mlngProduct <> nProduct Or mdtmEffecdate <> dEffecdate Or bFind Then
			
			'+ Definición de parámetros para stored procedure 'insudb.reaTab_reqexc'
			'+ Información leída el 07/12/2000 13:40:19
			lrecreaTab_reqexc = New eRemoteDB.Execute
			With lrecreaTab_reqexc
				.StoredProcedure = "reaTab_reqexc"
				.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nDefReq", nDefReq, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If .Run Then
					Do While Not .EOF
						lclsTab_reqexc = New Tab_reqexc
						lclsTab_reqexc.sType1 = .FieldToClass("nReqExc1")
						lclsTab_reqexc.nCode1 = .FieldToClass("nCode1")
						lclsTab_reqexc.nRole1 = .FieldToClass("nRole1")
						lclsTab_reqexc.sType2 = .FieldToClass("nReqExc2")
						lclsTab_reqexc.nCode2 = .FieldToClass("nCode2")
						lclsTab_reqexc.nRole2 = .FieldToClass("nRole2")
						lclsTab_reqexc.sRelation = .FieldToClass("nRelReqExc")
						lclsTab_reqexc.sInvrel = .FieldToClass("sInvrel")
						Call Add(lclsTab_reqexc)
						'UPGRADE_NOTE: Object lclsTab_reqexc may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
						lclsTab_reqexc = Nothing
						.RNext()
					Loop 
					mlngBranch = nBranch
					mlngProduct = nProduct
					mdtmEffecdate = dEffecdate
					.RCloseRec()
					Find = True
				End If
			End With
		Else
			Find = True
		End If
		
reaTab_reqexc_Err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecreaTab_reqexc may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaTab_reqexc = Nothing
	End Function
	
	'% Find_by_type: Carga la coleccion con los elementos de la tabla "Tab_reqexc" dado el tipo
	Public Function Find_by_type(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, ByVal nReqexc1 As Integer, Optional ByRef bFind As Boolean = False) As Boolean
		Dim lrecreaTab_reqexc As eRemoteDB.Execute
		Dim lclsTab_reqexc As Tab_reqexc
		
		On Error GoTo Find_by_type_Err
		If mlngBranch <> nBranch Or mlngProduct <> nProduct Or mdtmEffecdate <> dEffecdate Or mlngReqexc1 <> nReqexc1 Or bFind Then
			
			'+ Definición de parámetros para stored procedure 'reaTab_reqexc_by_type'
			lrecreaTab_reqexc = New eRemoteDB.Execute
			With lrecreaTab_reqexc
				.StoredProcedure = "reaTab_reqexc_by_type"
				.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nReqexc1", nReqexc1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If .Run Then
					Do While Not .EOF
						lclsTab_reqexc = New Tab_reqexc
						lclsTab_reqexc.sType1 = .FieldToClass("nReqExc1")
						lclsTab_reqexc.nCode1 = .FieldToClass("nCode1")
						lclsTab_reqexc.nRole1 = .FieldToClass("nRole1")
						lclsTab_reqexc.sType2 = .FieldToClass("nReqExc2")
						lclsTab_reqexc.nCode2 = .FieldToClass("nCode2")
						lclsTab_reqexc.nRole2 = .FieldToClass("nRole2")
						lclsTab_reqexc.sRelation = .FieldToClass("nRelReqExc")
						lclsTab_reqexc.sInvrel = .FieldToClass("sInvrel")
						Call Add(lclsTab_reqexc)
						'UPGRADE_NOTE: Object lclsTab_reqexc may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
						lclsTab_reqexc = Nothing
						.RNext()
					Loop 
					
					mlngBranch = nBranch
					mlngProduct = nProduct
					mdtmEffecdate = dEffecdate
					mlngReqexc1 = nReqexc1
					.RCloseRec()
					Find_by_type = True
				End If
			End With
		Else
			Find_by_type = True
		End If
		
		
Find_by_type_Err: 
		If Err.Number Then
			Find_by_type = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecreaTab_reqexc may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaTab_reqexc = Nothing
	End Function
	
	'% FindDP038: Carga la coleccion con los elementos de la tabla "Tab_reqexc"
	Public Function FindDP038(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, Optional ByVal sBrancht As String = "", Optional ByVal nDefReq As Integer = 0, Optional ByRef bFind As Boolean = False) As Boolean
		Dim lrecreaTab_reqexc As eRemoteDB.Execute
		Dim lclsTab_reqexc As Tab_reqexc
		
		On Error GoTo reaTab_reqexc_Err
		If mlngBranch <> nBranch Or mlngProduct <> nProduct Or mdtmEffecdate <> dEffecdate Or bFind Then
			
			'+ Definición de parámetros para stored procedure 'insudb.reaTab_reqexc'
			'+ Información leída el 07/12/2000 13:40:19
			lrecreaTab_reqexc = New eRemoteDB.Execute
			With lrecreaTab_reqexc
				.StoredProcedure = "reaTab_reqexc_DP038"
				.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("sBrancht", sBrancht, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nDefReq", nDefReq, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				
				If .Run Then
					Do While Not .EOF
						lclsTab_reqexc = New Tab_reqexc
						lclsTab_reqexc.sType1 = .FieldToClass("nReqExc1")
						lclsTab_reqexc.nCode1 = .FieldToClass("nCode1")
						lclsTab_reqexc.sDesReqExc1 = .FieldToClass("sDesReqExc1")
						lclsTab_reqexc.nModulec1 = .FieldToClass("nModulec1")
						lclsTab_reqexc.nRole1 = .FieldToClass("nRole1")
						lclsTab_reqexc.sType2 = .FieldToClass("nReqExc2")
						lclsTab_reqexc.nCode2 = .FieldToClass("nCode2")
						lclsTab_reqexc.sDesReqExc2 = .FieldToClass("sDesReqExc2")
						lclsTab_reqexc.nModulec2 = .FieldToClass("nModulec2")
						lclsTab_reqexc.nRole2 = .FieldToClass("nRole2")
						lclsTab_reqexc.sRelation = .FieldToClass("nRelReqExc")
						lclsTab_reqexc.sInvrel = .FieldToClass("sInvrel")
						'                    lclsTab_reqexc.nDefReq = .FieldToClass("nDefReq")
						Call Add(lclsTab_reqexc)
						'UPGRADE_NOTE: Object lclsTab_reqexc may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
						lclsTab_reqexc = Nothing
						.RNext()
					Loop 
					mlngBranch = nBranch
					mlngProduct = nProduct
					mdtmEffecdate = dEffecdate
					.RCloseRec()
					FindDP038 = True
				End If
			End With
		Else
			FindDP038 = True
		End If
		
reaTab_reqexc_Err: 
		If Err.Number Then
			FindDP038 = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecreaTab_reqexc may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaTab_reqexc = Nothing
	End Function
	
	'* Item: Devuelve un elemento de la colección (segun índice)
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Tab_reqexc
		Get
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
	Public Sub Remove(ByRef vntIndexKey As Object)
		mCol.Remove(vntIndexKey)
	End Sub
	
	'% Class_Initialize: Controla la creación de una instancia de la colección
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		mCol = New Collection
		mlngBranch = eRemoteDB.Constants.intNull
		mlngProduct = eRemoteDB.Constants.intNull
		mdtmEffecdate = eRemoteDB.Constants.dtmNull
		mlngReqexc1 = eRemoteDB.Constants.intNull
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






