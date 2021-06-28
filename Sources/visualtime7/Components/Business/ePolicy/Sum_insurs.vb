Option Strict Off
Option Explicit On
Public Class Sum_insurs
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: Sum_insurs.cls                           $%'
	'% $Author:: Nvaplat41                                  $%'
	'% $Date:: 9/10/03 19.02                                $%'
	'% $Revision:: 16                                       $%'
	'%-------------------------------------------------------%'
	
	Private mCol As Collection
	
	Public nCurrency As Integer
	Public sCurrency As String
	
	'% InsPreCA009: Función que obtiene los valores de la CA009
	Public Function InsPreCA009(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nCurrency As Integer, ByVal sCurrency As String, ByVal nAction As Integer) As Boolean
		Dim lclsCurren_pol As Curren_pol
		
		On Error GoTo InsPreCA009_Err
		If nCurrency <= 0 Then
			lclsCurren_pol = New Curren_pol
			If lclsCurren_pol.FindOneOrLocal(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate) Then
				sCurrency = lclsCurren_pol.Charge_Combo
				nCurrency = lclsCurren_pol.nCurrency
				InsPreCA009 = True
			End If
		Else
			InsPreCA009 = True
		End If
		
		If InsPreCA009 Then
			Me.sCurrency = sCurrency
			Me.nCurrency = nCurrency
			
			InsPreCA009 = Find(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nCurrency)
		End If
		
InsPreCA009_Err: 
		If Err.Number Then
			InsPreCA009 = False
		End If
		'UPGRADE_NOTE: Object lclsCurren_pol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsCurren_pol = Nothing
		On Error GoTo 0
	End Function
	
	'% Find: Función que carga la información de de la tabla Sum_insur...
	Public Function Find(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nCurrency As Integer) As Boolean
		Dim lrecreaSum_insur As eRemoteDB.Execute
		
		lrecreaSum_insur = New eRemoteDB.Execute
		
		On Error GoTo Find_Err
		
		'+ Definición de parámetros para stored procedure 'insudb.reaSum_insur'
		'+ Información leída el 20/11/2000 11:01:59 a.m.
		With lrecreaSum_insur
			.StoredProcedure = "reaSum_insur"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Do While Not .EOF
					Call Add(.FieldToClass("sCertype"), .FieldToClass("nBranch"), .FieldToClass("nProduct"), .FieldToClass("nPolicy"), .FieldToClass("nCertif"), .FieldToClass("nSumins_cod"), .FieldToClass("dEffecdate"), .FieldToClass("nSumins_real"), .FieldToClass("nSum_insur"), .FieldToClass("nCoinsuran"), .FieldToClass("dCompdate"), .FieldToClass("nCurrency"), .FieldToClass("nUsercode"), .FieldToClass("nTransactio"), .FieldToClass("dNulldate"), .FieldToClass("nCode"), .FieldToClass("sDescript"))
					.RNext()
				Loop 
				.RCloseRec()
				Find = True
			Else
				Find = False
			End If
		End With
		'UPGRADE_NOTE: Object lrecreaSum_insur may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaSum_insur = Nothing
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
	End Function
	'% Find: Función que carga la información de de la tabla Sum_insur...
	Public Function Find_Cal963(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double) As Boolean
		Dim lrecreaSum_insur As eRemoteDB.Execute
		
		lrecreaSum_insur = New eRemoteDB.Execute
		
		On Error GoTo Find_Err
		
		'+ Definición de parámetros para stored procedure 'insudb.reaSum_insur'
		'+ Información leída el 20/11/2000 11:01:59 a.m.
		With lrecreaSum_insur
			.StoredProcedure = "REA_CAL963"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Do While Not .EOF
					Call Add_Cal963(.FieldToClass("dStartdate"), .FieldToClass("dExpirdat"), .FieldToClass("nPremium_tmp"), .FieldToClass("nPremium_real"), .FieldToClass("nPremium_ajust"))
					.RNext()
				Loop 
				.RCloseRec()
				Find_Cal963 = True
			Else
				Find_Cal963 = False
			End If
		End With
		'UPGRADE_NOTE: Object lrecreaSum_insur may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaSum_insur = Nothing
		
Find_Err: 
		If Err.Number Then
			Find_Cal963 = False
		End If
		On Error GoTo 0
	End Function
	
	'% Add: Añade un nuevo elemento a la colección
	Public Function Add(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nSumins_cod As Integer, ByVal dEffecdate As Date, ByVal nSumins_real As Double, ByVal nSum_insur As Double, ByVal nCoinsuran As Double, ByVal dCompdate As Date, ByVal nCurrency As Integer, ByVal nUsercode As Integer, ByVal nTransaction As String, ByVal dNulldate As Date, ByVal nCode As Integer, ByVal sDescript As String) As Sum_insur
		Dim objNewMember As Sum_insur
		
		objNewMember = New Sum_insur
		
		With objNewMember
			.sCertype = sCertype
			.nBranch = nBranch
			.nProduct = nProduct
			.nPolicy = nPolicy
			.nCertif = nCertif
			.nSumins_cod = nSumins_cod
			.dEffecdate = dEffecdate
			.nSumins_real = nSumins_real
			.nSum_insur = nSum_insur
			.nCoinsuran = nCoinsuran
			.nCurrency = nCurrency
			.dNulldate = dNulldate
			.nTransactio = CInt(nTransaction)
			.nUsercode = nUsercode
			.nCode = nCode
			.sDescript = sDescript
			
			mCol.Add(objNewMember, "FC" & sCertype & nBranch & nProduct & nPolicy & nCertif & nSumins_cod & dEffecdate & nCode & sDescript)
		End With
		
		Add = objNewMember
		'UPGRADE_NOTE: Object objNewMember may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		objNewMember = Nothing
		
	End Function
	'% Add: Añade un nuevo elemento a la colección
	Public Function Add_Cal963(ByVal dStartdate As Date, ByVal dExpirdate As Date, ByVal nPremium_tmp As Double, ByVal nPremium_Real As Double, ByVal nPremium_ajust As Double) As Sum_insur
		Dim objNewMember As Sum_insur
		
		objNewMember = New Sum_insur
		
		With objNewMember
			.dStartdate = dStartdate
			.dExpirdate = dExpirdate
			.nPremium_tmp = nPremium_tmp
			.nPremium_Real = nPremium_Real
			.nPremium_ajust = nPremium_ajust
		End With
		
		mCol.Add(objNewMember)
		
		Add_Cal963 = objNewMember
		'UPGRADE_NOTE: Object objNewMember may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		objNewMember = Nothing
		
	End Function
	
	'% Item: Toma un elemento de la colección
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Sum_insur
		Get
			Item = mCol.Item(vntIndexKey)
		End Get
	End Property
	
	'% Count: Cuenta el número de elementos dentro de la colección
	Public ReadOnly Property Count() As Integer
		Get
			Count = mCol.Count()
		End Get
	End Property
	
	'% NewEnum: Enumera los elementos dentro de la colección
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
	
	'% Remove: Elimina un elemento dentro de la colección
	Public Sub Remove(ByRef vntIndexKey As Object)
		mCol.Remove(vntIndexKey)
	End Sub
	
	'% Class_Initialize: Controla la apertura de cada instancia de la colección
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		mCol = New Collection
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'% Class_Terminate: Elimina la colección
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






