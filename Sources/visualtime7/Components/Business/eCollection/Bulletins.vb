Option Strict Off
Option Explicit On
Public Class Bulletins
	Implements System.Collections.IEnumerable
	'- local variable to hold collection
	
	Private mCol As Collection
	'- Variable Auxiliar
	Public nTotRow As Integer
	
	
	Public Function Add(ByRef lclsBulletin As Bulletin) As Bulletin
		With lclsBulletin
			mCol.Add(lclsBulletin, "CO" & .sSel & .nBulletins)
			
		End With
		
		'return the object created
		Add = lclsBulletin
	End Function
	
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Bulletin
		Get
			
			'+ used when referencing an element in the collection
			'+ vntIndexKey contains either the Index or Key to the collection,
			'+ this is why it is declared as a Variant
			'+ Syntax: Set foo = x.Item(xyz) or Set foo = x.Item(5)
			
			Item = mCol.Item(vntIndexKey)
		End Get
	End Property
	
	Public ReadOnly Property Count() As Integer
		Get
			
			'+ used when retrieving the number of elements in the
			'+ collection. Syntax: Debug.Print x.Count
			
			Count = mCol.Count()
		End Get
	End Property
	
	'UPGRADE_NOTE: NewEnum property was commented out. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="B3FC1610-34F3-43F5-86B7-16C984F0E88E"'
	'Public ReadOnly Property NewEnum() As stdole.IUnknown
		'Get
			'
			'+ this property allows you to enumerate
			'+ this collection with the For...Each syntax
			'
			'NewEnum = mCol._NewEnum
		'End Get
	'End Property
	
	Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
		'UPGRADE_TODO: Uncomment and change the following line to return the collection enumerator. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="95F9AAD0-1319-4921-95F0-B9D3C4FF7F1C"'
		GetEnumerator = mCol.GetEnumerator
	End Function
	
	Public Sub Remove(ByRef vntIndexKey As Object)
		
		'+ used when removing an element from the collection
		'+ vntIndexKey contains either the Index or Key, which is why
		'+ it is declared as a Variant
		'+ Syntax: x.Remove(xyz)
		
		mCol.Remove(vntIndexKey)
	End Sub
	
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		
		'+ creates the collection when this class is created
		
		mCol = New Collection
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'UPGRADE_NOTE: Class_Terminate was upgraded to Class_Terminate_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Terminate_Renamed()
		
		'+ destroys collection when this class is terminated
		'+ Set mCol = Nothing
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
	
	'% Find: Esta función se encarga de buscar los registros en la tabla bulletins
	Public Function Find() As Boolean
		Find = True
	End Function
	
	'% Update: realiza la accion que indica nStatInstanc
	Public Function Update() As Boolean
		Dim lclsBulletin As Bulletin
		
		Update = True
		
		For	Each lclsBulletin In mCol
			With lclsBulletin
				Select Case .nStatInstanc
					Case Bulletin.eStatusInstance.eftUpDate
						Update = .Update(.nBulletins)
				End Select
			End With
		Next lclsBulletin
	End Function
	
	'%FindPayToReject: Busca los datos correspondiente a un boletin en la tabla Bulletins.
	Public Function FindPayToReject(ByVal sKey As String, ByVal nRow As Integer, ByVal sRead As String, ByVal ldtmEffecdate As Date, ByVal lintWay_pay As Integer, ByVal llngBank As Double, ByVal sProcess As String, Optional ByVal lblnFind As Boolean = False, Optional ByVal ncod_agree As Integer = 0) As Boolean
		Dim llngCount As Object
		Dim lrecreaBulletins As eRemoteDB.Execute
		Dim lclsBulletin As eCollection.Bulletin
		
		On Error GoTo FindPayToReject_Err
		
		lrecreaBulletins = New eRemoteDB.Execute
		
		With lrecreaBulletins
			.StoredProcedure = "reaBulletins"
			.Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRow", nRow, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sRead", sRead, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", ldtmEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nWay_pay", lintWay_pay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBank", llngBank, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sProcess", sProcess, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCod_Agree", ncod_agree, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				FindPayToReject = True
				llngCount = 1
				If Not .EOF Then
					nTotRow = .FieldToClass("nTotRow", 0)
				End If
				Do While Not .EOF
					lclsBulletin = New eCollection.Bulletin
					lclsBulletin.nBulletins = .FieldToClass("nBulletins", 0)
					lclsBulletin.sClient = .FieldToClass("sClient", String.Empty)
					lclsBulletin.sCliename = .FieldToClass("sCliename", String.Empty)
					lclsBulletin.nBank_code = .FieldToClass("nBank_code")
					
					If llngBank <= 0 Then '+Transbank
						lclsBulletin.sDocument = .FieldToClass("sDocument")
						lclsBulletin.sAccount = String.Empty
					Else '+ PAC
						lclsBulletin.sAccount = .FieldToClass("sDocument")
						lclsBulletin.sDocument = String.Empty
					End If
					
					lclsBulletin.nAmount = .FieldToClass("nAmount")
					lclsBulletin.nRejectCause = .FieldToClass("nRejectCause", eRemoteDB.Constants.intNull)
					lclsBulletin.nbranch = .FieldToClass("nBranch")
					lclsBulletin.sBranch = .FieldToClass("sBranch")
					lclsBulletin.nProduct = .FieldToClass("nProduct")
					lclsBulletin.sProduct = .FieldToClass("sProduct")
					lclsBulletin.nPolicy = .FieldToClass("nPolicy", eRemoteDB.Constants.intNull)
					lclsBulletin.nReceipt = .FieldToClass("nReceipt")
					lclsBulletin.nDraft = .FieldToClass("nDraft", eRemoteDB.Constants.intNull)
					
					If lclsBulletin.nRejectCause <= 0 Then
						lclsBulletin.sSel = "0"
					Else
						lclsBulletin.sSel = "1"
					End If
					
					Call Add_CO501(lclsBulletin)
					llngCount = llngCount + 1
					'UPGRADE_NOTE: Object lclsBulletin may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					lclsBulletin = Nothing
					.RNext()
				Loop 
			End If
		End With
		
FindPayToReject_Err: 
		If Err.Number Then
			FindPayToReject = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecreaBulletins may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaBulletins = Nothing
	End Function
	
	'%findBulletinsCli: Busca los datos correspondiente a un boletin en la tabla Bulletins.
	Public Function findBulletinsCli(ByVal nRelanum As Double, ByVal nRelanum_aux As Double, ByVal sClient As String, Optional ByRef lblnFind As Boolean = False) As Boolean
		Dim llngCount As Object
		Dim lrecreaBulletins As eRemoteDB.Execute
		Dim lclsBulletin As eCollection.Bulletin
		
		Static lstrClient As String
		
		On Error GoTo findBulletinsCli_Err
		
		lrecreaBulletins = New eRemoteDB.Execute
		
		sClient = IIf(sClient = String.Empty, "0", sClient)
		
		If sClient = lstrClient Or lblnFind Then
			findBulletinsCli = True
		Else
			With lrecreaBulletins
				.StoredProcedure = "REABULLETINSCLIENT"
				
				.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nBordereaux", nRelanum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nBordereaux_aux", nRelanum_aux, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				
				If .Run(True) Then
					findBulletinsCli = True
					
					llngCount = 1
					Do While Not (.EOF Or llngCount > 100)
						lclsBulletin = New eCollection.Bulletin
						
						lclsBulletin.nBulletins = .FieldToClass("nBulletins", 0)
						lclsBulletin.sClient = .FieldToClass("sClient", String.Empty)
						lclsBulletin.sCliename = .FieldToClass("sCliename", String.Empty)
						lclsBulletin.dLimit_pay = .FieldToClass("dLimit_pay")
						lclsBulletin.nCurrency = .FieldToClass("nCurrency")
						lclsBulletin.nExchange = .FieldToClass("nExchange")
						lclsBulletin.nAmount = .FieldToClass("nAmount")
						lclsBulletin.nRejectCause = .FieldToClass("nRejectCause", eRemoteDB.Constants.intNull)
						lclsBulletin.nLocalAmount = .FieldToClass("nLocalAmount")
						lclsBulletin.sSel = .FieldToClass("sSel")
						
						Call Add(lclsBulletin)
						llngCount = llngCount + 1
						'UPGRADE_NOTE: Object lclsBulletin may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
						lclsBulletin = Nothing
						.RNext()
					Loop 
				End If
			End With
		End If
		
		'UPGRADE_NOTE: Object lrecreaBulletins may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaBulletins = Nothing
		
findBulletinsCli_Err: 
		If Err.Number Then
			findBulletinsCli = False
		End If
		
		On Error GoTo 0
	End Function
	
	Public Function FindBulletinsMan(ByRef nBordereaux As Double, Optional ByVal lblnFind As Boolean = False) As Boolean
		Dim llngCount As Object
		Dim lrecT_Bulletin As New eRemoteDB.Execute
		Dim lclsBulletin As eCollection.Bulletin
		Static ldblBordereaux As Double
		
		On Error GoTo FindBulletinsMan_Err
		
		If nBordereaux = ldblBordereaux Or Not lblnFind Then
			FindBulletinsMan = True
		Else
			With lrecT_Bulletin
				.StoredProcedure = "REABULLETINS_MANUAL"
				
				.Parameters.Add("nBordereaux", nBordereaux, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				
				If .Run Then
					FindBulletinsMan = True
					
					llngCount = 1
					Do While Not (.EOF Or llngCount > 100)
						lclsBulletin = New eCollection.Bulletin
						
						lclsBulletin.nBulletins = .FieldToClass("nBulletins", 0)
						lclsBulletin.sClient = .FieldToClass("sClient", String.Empty)
						lclsBulletin.sCliename = .FieldToClass("sCliename", String.Empty)
						lclsBulletin.dLimit_pay = .FieldToClass("dLimit_pay")
						lclsBulletin.nCurrency = .FieldToClass("nCurrency")
						lclsBulletin.nExchange = .FieldToClass("nExchange")
						lclsBulletin.nAmount = .FieldToClass("nAmount")
						lclsBulletin.nRejectCause = .FieldToClass("nRejectCause", eRemoteDB.Constants.intNull)
						lclsBulletin.nLocalAmount = .FieldToClass("nLocalAmount")
						lclsBulletin.sSel = .FieldToClass("sSel")
						
						Call Add(lclsBulletin)
						llngCount = llngCount + 1
						'UPGRADE_NOTE: Object lclsBulletin may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
						lclsBulletin = Nothing
						.RNext()
					Loop 
				End If
			End With
		End If
		
FindBulletinsMan_Err: 
		If Err.Number Then
			FindBulletinsMan = False
		End If
		
		On Error GoTo 0
	End Function
	Public Function Add_CO501(ByRef lclsBulletin As Bulletin) As Bulletin
		With lclsBulletin
			mCol.Add(lclsBulletin)
		End With
		
		'return the object created
		Add_CO501 = lclsBulletin
	End Function
End Class






