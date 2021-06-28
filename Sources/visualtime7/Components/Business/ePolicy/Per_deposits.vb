Option Strict Off
Option Explicit On
Public Class Per_deposits
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: Per_deposits.cls                         $%'
	'% $Author:: Clobos                                     $%'
	'% $Date:: 10-05-06 13:56                               $%'
	'% $Revision:: 3                                        $%'
	'%-------------------------------------------------------%'
	
	'local variable to hold collection
	Private mCol As Collection
	
	'- Variables que guardan la llave de busqueda
	Private mstrCertype As String
	Private mlngBranch As Integer
	Private mlngProduct As Integer
	Private mlngPolicy As Double
	Private mlngCertif As Double
	Private mdtmEffecdate As Date
	
	'%Add: Agrega un nuevo registro a la colección
	Public Function Add(ByRef objClass As Per_deposit) As Per_deposit
		If objClass Is Nothing Then
			objClass = New Per_deposit
		End If
		
		With objClass
			mCol.Add(objClass, .sCertype & .nBranch & .nProduct & .nPolicy & .nCertif & .nYear_ini & .dEffecdate.ToString("yyyyMMdd"))
		End With
		
		'Return the object created
		Add = objClass
	End Function
	
	'%Find: Lee los planes de pago para los aportes de la póliza
	Public Function Find(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, Optional ByVal lblnFind As Boolean = False) As Boolean
		Dim lrecreaPer_deposit_a As eRemoteDB.Execute
		Dim lclsPer_deposit As Per_deposit
		
		On Error GoTo Find_Err
		
		If sCertype <> mstrCertype Or nBranch <> mlngBranch Or nProduct <> mlngProduct Or nPolicy <> mlngPolicy Or nCertif <> mlngCertif Or dEffecdate <> mdtmEffecdate Or lblnFind Then
			
			lrecreaPer_deposit_a = New eRemoteDB.Execute
			
			'+ Definición de store procedure reaPer_deposit_a al 04-03-2002 12:38:04
			With lrecreaPer_deposit_a
				.StoredProcedure = "reaPer_deposit_a"
				.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If .Run Then
					Find = True
					Do While Not .EOF
						lclsPer_deposit = New Per_deposit
						lclsPer_deposit.nYear_ini = .FieldToClass("nYear_ini")
						lclsPer_deposit.nYear_end = .FieldToClass("nYear_end")
						lclsPer_deposit.nAmountdep = .FieldToClass("nAmountdep")
						lclsPer_deposit.nAmountdep_aux = .FieldToClass("nAmountdep_aux")
						lclsPer_deposit.nPayfreq = .FieldToClass("nPayFreq")
						lclsPer_deposit.nExtPrem = .FieldToClass("nExtprem")
						lclsPer_deposit.nSurrender = .FieldToClass("nSurrender")
						
						Call Add(lclsPer_deposit)
						'UPGRADE_NOTE: Object lclsPer_deposit may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
						lclsPer_deposit = Nothing
						.RNext()
					Loop 
					.RCloseRec()
					mstrCertype = sCertype
					mlngBranch = nBranch
					mlngProduct = nProduct
					mlngPolicy = nPolicy
					mlngCertif = nCertif
					mdtmEffecdate = dEffecdate
				End If
			End With
		Else
			Find = True
		End If
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		'UPGRADE_NOTE: Object lrecreaPer_deposit_a may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaPer_deposit_a = Nothing
		On Error GoTo 0
	End Function
	'%Find: Lee los planes de pago para los aportes de la póliza
	Public Function Find_premium_det(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, Optional ByVal lblnFind As Boolean = False) As Boolean
		Dim lrecreaPer_deposit_a As eRemoteDB.Execute
		Dim lclsPer_deposit As Per_deposit
		
		On Error GoTo Find_Err
		
		If sCertype <> mstrCertype Or nBranch <> mlngBranch Or nProduct <> mlngProduct Or nPolicy <> mlngPolicy Or nCertif <> mlngCertif Or dEffecdate <> mdtmEffecdate Or lblnFind Then
			
			lrecreaPer_deposit_a = New eRemoteDB.Execute
			
			'+ Definición de store procedure reaPer_deposit_a al 04-03-2002 12:38:04
			With lrecreaPer_deposit_a
				.StoredProcedure = "reaPer_deposit_a2"
				.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If .Run Then
					Find_premium_det = True
					Do While Not .EOF
						lclsPer_deposit = New Per_deposit
						lclsPer_deposit.nYear_ini = .FieldToClass("nYear_ini")
						lclsPer_deposit.nYear_end = .FieldToClass("nYear_end")
						lclsPer_deposit.nAmountdep = .FieldToClass("nAmountdep")
						lclsPer_deposit.nAmountdep_aux = .FieldToClass("nAmountdep_aux")
						lclsPer_deposit.nBasicPrem = .FieldToClass("nPremium_Bas")
						lclsPer_deposit.nSavingPrem = .FieldToClass("nPremium_Sav")
						lclsPer_deposit.nRecamount = .FieldToClass("nRecamount")
						
						Call Add(lclsPer_deposit)
						'UPGRADE_NOTE: Object lclsPer_deposit may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
						lclsPer_deposit = Nothing
						.RNext()
					Loop 
					.RCloseRec()
					mstrCertype = sCertype
					mlngBranch = nBranch
					mlngProduct = nProduct
					mlngPolicy = nPolicy
					mlngCertif = nCertif
					mdtmEffecdate = dEffecdate
				End If
			End With
		Else
			Find_premium_det = True
		End If
		
Find_Err: 
		If Err.Number Then
			Find_premium_det = False
		End If
		'UPGRADE_NOTE: Object lrecreaPer_deposit_a may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaPer_deposit_a = Nothing
		On Error GoTo 0
	End Function
	'* Item: Devuelve un elemento de la colección (segun índice)
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Per_deposit
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
		mstrCertype = String.Empty
		mlngBranch = eRemoteDB.Constants.intNull
		mlngProduct = eRemoteDB.Constants.intNull
		mlngPolicy = eRemoteDB.Constants.intNull
		mlngCertif = eRemoteDB.Constants.intNull
		mdtmEffecdate = eRemoteDB.Constants.dtmNull
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






