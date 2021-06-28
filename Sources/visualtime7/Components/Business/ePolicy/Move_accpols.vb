Option Strict Off
Option Explicit On
Public Class Move_Accpols
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: Move_Accpols.cls                         $%'
	'% $Author:: Pgarin                                     $%'
	'% $Date:: 24/08/06 10:56                               $%'
	'% $Revision:: 3                                        $%'
	'%-------------------------------------------------------%'
	
	'- Llave de proceso
	Public sKey As String
	
	'- Local variable to hold collection
	Private mCol As Collection
	
	'% Add: Agrega un elemento a la colección
	Public Function Add(ByRef objClass As Move_Accpol) As Move_Accpol
		If objClass Is Nothing Then
			objClass = New Move_Accpol
		End If
		
		mCol.Add(objClass)
		
		'    With objClass
		'        mCol.Add objClass, .sCertype & _
		''                           .nBranch & _
		''                           .nProduct & _
		''                           .nPolicy & _
		''                           .nCertif & _
		''                           .nIdmov
		'    End With
		
		'+ Entrega objeto creado
		Add = objClass
	End Function
	
	'% FindByDate: Busca movimientos por fecha
	Public Function FindByDate(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, Optional ByVal dMovDate As Date = eRemoteDB.Constants.dtmNull) As Boolean
		Dim lrecreaMove_Accpol_dmovdate As eRemoteDB.Execute
		Dim lclsMove_Accpol As Move_Accpol
		On Error GoTo reaMove_Accpol_dmovdate_Err
		lrecreaMove_Accpol_dmovdate = New eRemoteDB.Execute
		
		With lrecreaMove_Accpol_dmovdate
			.StoredProcedure = "reaMove_Accpol_dmovdate"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dMovdate", dMovDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				FindByDate = True
				Do While Not .EOF
					lclsMove_Accpol = New Move_Accpol
					lclsMove_Accpol.nIdmov = .FieldToClass("nIdmov")
					lclsMove_Accpol.nTypemove = .FieldToClass("nTypemove")
					lclsMove_Accpol.sTypemove = .FieldToClass("sTypemove")
					lclsMove_Accpol.nAmount = .FieldToClass("nAmount")
					lclsMove_Accpol.nCredit = .FieldToClass("nCredit")
					lclsMove_Accpol.nDebit = .FieldToClass("nDebit")
					lclsMove_Accpol.dMovDate = .FieldToClass("dMovdate")
					lclsMove_Accpol.sInddetail = .FieldToClass("sInddetail")
					lclsMove_Accpol.sUse = .FieldToClass("sUse")
					lclsMove_Accpol.nIdsurr = .FieldToClass("nIdsurr")
					lclsMove_Accpol.nCashnum = .FieldToClass("nCashnum")
					lclsMove_Accpol.nBordereaux = .FieldToClass("nBordereaux")
					lclsMove_Accpol.nReceipt = .FieldToClass("nReceipt")
					lclsMove_Accpol.nYear = .FieldToClass("nYear")
					lclsMove_Accpol.nMonth = .FieldToClass("nMonth")
					lclsMove_Accpol.sAdjustment = .FieldToClass("sAdjustment")
					lclsMove_Accpol.dPosted = .FieldToClass("dPosted")
					lclsMove_Accpol.nInterest = .FieldToClass("nInterest")
					
					Call Add(lclsMove_Accpol)
					'UPGRADE_NOTE: Object lclsMove_Accpol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					lclsMove_Accpol = Nothing
					.RNext()
				Loop 
				.RCloseRec()
			Else
				FindByDate = False
			End If
		End With
		
reaMove_Accpol_dmovdate_Err: 
		If Err.Number Then
			FindByDate = False
		End If
		'UPGRADE_NOTE: Object lrecreaMove_Accpol_dmovdate may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaMove_Accpol_dmovdate = Nothing
		On Error GoTo 0
	End Function
	'% FindByBorderaux: Busca movimientos por propuesta
	Public Function FindByBorderaux(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double) As Boolean
		Dim lrecreaMove_Acc_Borderaux As eRemoteDB.Execute
		Dim lclsMove_Accpol As Move_Accpol
		On Error GoTo reaMove_Acc_Borderaux_Err
		lrecreaMove_Acc_Borderaux = New eRemoteDB.Execute
		
		With lrecreaMove_Acc_Borderaux
			.StoredProcedure = "reaMove_Acc_Borderaux"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				FindByBorderaux = True
				Do While Not .EOF
					lclsMove_Accpol = New Move_Accpol
					lclsMove_Accpol.nBordereaux = .FieldToClass("nBordereaux")
					lclsMove_Accpol.nAmount = .FieldToClass("nAmount")
					lclsMove_Accpol.sDescript = .FieldToClass("sDescript")
					lclsMove_Accpol.sClient = .FieldToClass("sClient")
					
					Call Add(lclsMove_Accpol)
					'UPGRADE_NOTE: Object lclsMove_Accpol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					lclsMove_Accpol = Nothing
					.RNext()
				Loop 
				.RCloseRec()
			Else
				FindByBorderaux = False
			End If
		End With
		
reaMove_Acc_Borderaux_Err: 
		If Err.Number Then
			FindByBorderaux = False
		End If
		'UPGRADE_NOTE: Object lrecreaMove_Acc_Borderaux may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaMove_Acc_Borderaux = Nothing
		On Error GoTo 0
	End Function
	
	'%InsCalMoveVP: Realiza los movimientos de ajustes y de prima de inyeccion de las polizas
	'%              de vida activa
	Public Function InsCalMoveVP(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dMovedate As Date, ByVal nTypemove As Integer, ByVal nUsercode As Integer, ByVal sKey As String) As Boolean
		'- Variable para conectarse a la base de datos
		Dim lrecInsCalMoveVP As eRemoteDB.Execute
		'- Objeto para agregar un registro a la coleccion
		Dim lclsMove_Accpol As Move_Accpol
		'- Correlativo de movimiento
		Dim lintIdMov As Integer
		
		On Error GoTo InsCalMoveVP_Err
		lrecInsCalMoveVP = New eRemoteDB.Execute
		
		With lrecInsCalMoveVP
			.StoredProcedure = "InsCalMoveVP"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dMovdate", dMovedate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTypemove", nTypemove, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				InsCalMoveVP = True
				Do While Not .EOF
					lintIdMov = lintIdMov + 1
					lclsMove_Accpol = New Move_Accpol
					lclsMove_Accpol.nTypemove = .FieldToClass("nTypemove")
					lclsMove_Accpol.nYear = .FieldToClass("nYear")
					lclsMove_Accpol.nAmount = .FieldToClass("nAmount")
					lclsMove_Accpol.nMonth = .FieldToClass("nMonth")
					lclsMove_Accpol.nIdmov = lintIdMov
					Me.sKey = .FieldToClass("sKey")
					Call Add(lclsMove_Accpol)
					'UPGRADE_NOTE: Object lclsMove_Accpol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					lclsMove_Accpol = Nothing
					.RNext()
				Loop 
				.RCloseRec()
			End If
		End With
		
InsCalMoveVP_Err: 
		If Err.Number Then
			InsCalMoveVP = False
		End If
		'UPGRADE_NOTE: Object lrecInsCalMoveVP may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecInsCalMoveVP = Nothing
		On Error GoTo 0
	End Function
	
	'% Item: Devuelve un elemento de la colección (segun índice)
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Move_Accpol
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
	
	'% Remove: Elimina un elemento de la colección
	Public Sub Remove(ByRef vntIndexKey As Object)
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






