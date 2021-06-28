Option Strict Off
Option Explicit On
Public Class Reject_causes
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: Reject_causes.cls                        $%'
	'% $Author:: Gletelier                                  $%'
	'% $Date:: 8/10/09 3:31p                                $%'
	'% $Revision:: 3                                        $%'
	'%-------------------------------------------------------%'
	Private mCol As Collection
	
	'% Find: se buscan los datos asociados a la via de pago/banco
	Public Function Find(ByVal nBank_Code As Double, ByVal nWay_Pay As Integer) As Boolean
		Dim lclsRemote As eRemoteDB.Execute
		Dim lclsReject_cause As eCollection.Reject_cause
		
		On Error GoTo Find_Err
		
		lclsRemote = New eRemoteDB.Execute
		
		With lclsRemote
			.StoredProcedure = "reaReject_cause_all"
			.Parameters.Add("nBank_code", nBank_Code, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nWay_pay", nWay_Pay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Do While Not .EOF
					lclsReject_cause = New eCollection.Reject_cause
					lclsReject_cause.nRejectcause = .FieldToClass("nRejectCause")
					lclsReject_cause.sDescript = .FieldToClass("sDescript")
					lclsReject_cause.sShort_des = .FieldToClass("sShort_des")
					lclsReject_cause.sStatregt = .FieldToClass("sStatregt")
					lclsReject_cause.sNO_Endeavour = .FieldToClass("sNo_Endeavour")
					Call Add(lclsReject_cause)
					'UPGRADE_NOTE: Object lclsReject_cause may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					lclsReject_cause = Nothing
					.RNext()
				Loop 
				.RCloseRec()
				Find = True
			End If
		End With
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsRemote may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsRemote = Nothing
		'UPGRADE_NOTE: Object lclsReject_cause may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsReject_cause = Nothing
	End Function
	
	'% Add: añade un nuevo elemento a la colección
	Public Function Add(ByRef oReject_cause As eCollection.Reject_cause) As Reject_cause
		mCol.Add(oReject_cause)
		
		Add = oReject_cause
		'UPGRADE_NOTE: Object oReject_cause may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		oReject_cause = Nothing
	End Function
	
	'* Item:
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Reject_cause
		Get
			Item = mCol.Item(vntIndexKey)
		End Get
	End Property
	
	'* Count:
	Public ReadOnly Property Count() As Integer
		Get
			Count = mCol.Count()
		End Get
	End Property
	
	'* NewEnum:
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
	
	'% Remove:
	Public Sub Remove(ByRef vntIndexKey As Object)
		mCol.Remove(vntIndexKey)
	End Sub
	
	'* Class_Initialize:
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		mCol = New Collection
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'* Class_Terminate:
	'UPGRADE_NOTE: Class_Terminate was upgraded to Class_Terminate_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Terminate_Renamed()
		'UPGRADE_NOTE: Object mCol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mCol = Nothing
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
	
	'insPreCO982: Busca las polizas con boletines rechazados
	Public Function FindCO982(ByVal sKey As String) As Boolean
		Dim lrecreaReject_causes As eRemoteDB.Execute
		Dim lclsReject_cause As eCollection.Reject_cause
		
		On Error GoTo insFindCO982_Err
		
		lrecreaReject_causes = New eRemoteDB.Execute
		
		With lrecreaReject_causes
			.StoredProcedure = "insFindCO982"
			.Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				FindCO982 = True
				Do While Not .EOF
					lclsReject_cause = New eCollection.Reject_cause
					lclsReject_cause.sSel = .FieldToClass("sSel")
					lclsReject_cause.nBank_Code = .FieldToClass("nBank_code")
					lclsReject_cause.sDescbankcode = .FieldToClass("sDescbankcode")
					lclsReject_cause.sDocument = .FieldToClass("sDocument")
					lclsReject_cause.nBulletins = .FieldToClass("nBulletins")
					lclsReject_cause.nReceipt = .FieldToClass("nReceipt")
					lclsReject_cause.nPolicy = .FieldToClass("nPolicy")
					lclsReject_cause.nProduct = .FieldToClass("nProduct")
					lclsReject_cause.sProduct = .FieldToClass("sProduct")
					lclsReject_cause.nRejectcause = .FieldToClass("nRejectcause")
					lclsReject_cause.sDesc_Rejectcause = .FieldToClass("sDesc_Rejectcause")
					lclsReject_cause.nPremium = .FieldToClass("nPremium")
					lclsReject_cause.dNextreceip = .FieldToClass("dNextreceip")
					Call Add_CO982(lclsReject_cause)
					'UPGRADE_NOTE: Object lclsReject_cause may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					lclsReject_cause = Nothing
					.RNext()
				Loop 
				.RCloseRec()
			Else
				FindCO982 = False
			End If
		End With
		
insFindCO982_Err: 
		If Err.Number Then
			FindCO982 = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecreaReject_causes may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaReject_causes = Nothing
	End Function
	
	'insPreCO982: Busca las polizas con boletines rechazados
	Public Function insPreCO982(ByVal sKey As String, ByVal nUsercode As Integer, ByVal nBank_Code As Double, ByVal nYear As Short, ByVal nMonth As Short, Optional ByVal nRejectcause As Integer = 0) As Boolean
		Dim lrecreaReject_causes As eRemoteDB.Execute
		Dim lclsReject_cause As eCollection.Reject_cause
		
		On Error GoTo insPreCO982_Err
		
		lrecreaReject_causes = New eRemoteDB.Execute
		
		With lrecreaReject_causes
			.StoredProcedure = "insPreCO982"
			.Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBank_Code", nBank_Code, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nYear", nYear, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nMonth", nMonth, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRejectcause", nRejectcause, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				insPreCO982 = True
			Else
				insPreCO982 = False
			End If
		End With
		
insPreCO982_Err: 
		If Err.Number Then
			insPreCO982 = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecreaReject_causes may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaReject_causes = Nothing
	End Function
	
	Public Function Add_CO982(ByRef lclsReject_cause As eCollection.Reject_cause) As Reject_cause
		With lclsReject_cause
			mCol.Add(lclsReject_cause)
		End With
		
		'return the object created
		Add_CO982 = lclsReject_cause
	End Function
End Class






