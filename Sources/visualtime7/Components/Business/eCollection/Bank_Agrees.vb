Option Strict Off
Option Explicit On
Public Class Bank_Agrees
	Implements System.Collections.IEnumerable
	'local variable to hold collection
	Private mCol As Collection
	
	'% Find: Busca los datos correspondiente a un convenio de banco.
	Public Function Find(ByVal Type_BankAgree As String, Optional ByVal lblnFind As Boolean = False) As Boolean
        Dim sType_BankAgree As Object = String.Empty
		Dim lrecreaBank_Agree As eRemoteDB.Execute
		
		Dim lclsAgreement As Bank_Agree
		
		lrecreaBank_Agree = New eRemoteDB.Execute
		
		On Error GoTo Find_Err
		
		If (Type_BankAgree = sType_BankAgree) Or lblnFind Then
			Find = True
		Else
			
			'+ Definición de parámetros para stored procedure 'insudb.reaBank_Agreeall'
			'+ Información leída el 10/10/2001
			
			With lrecreaBank_Agree
				.StoredProcedure = "reaBank_Agreeall"
				.Parameters.Add("sType_BankAgree", Type_BankAgree, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If .Run Then
					Do While Not .EOF
						lclsAgreement = New Bank_Agree
						
						lclsAgreement.sType_BankAgree = .FieldToClass("sType_BankAgree")
						lclsAgreement.nBank = .FieldToClass("nBank")
						lclsAgreement.nAccount = .FieldToClass("nAccount")
						lclsAgreement.sAcc_Number = .FieldToClass("sAcc_Number")
						lclsAgreement.sClient = .FieldToClass("sClient")
						
						Call Add(lclsAgreement)
						
						'UPGRADE_NOTE: Object lclsAgreement may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
						lclsAgreement = Nothing
						
						.RNext()
					Loop 
					.RCloseRec()
					Find = True
				Else
					Find = False
				End If
			End With
		End If
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecreaBank_Agree may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaBank_Agree = Nothing
	End Function
	
	Public Function FindMultipac(ByVal nBank_Lider As Double, Optional ByVal nBank As Double = 0, Optional ByVal nType As Integer = 0, Optional ByVal lblnFind As Boolean = False) As Boolean
		Dim lreaMultipac As eRemoteDB.Execute
		Dim lclsMultipac As eCollection.Bank_Agree
		
		On Error GoTo FindMultipac_Err
		lreaMultipac = New eRemoteDB.Execute
		
		
		With lreaMultipac
			.StoredProcedure = "reaMultipac"
			.Parameters.Add("nBank_Lider", nBank_Lider, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBank", nBank, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nType", nType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				Do While Not .EOF
					lclsMultipac = New eCollection.Bank_Agree
					
					lclsMultipac.nBank = .FieldToClass("nBank")
					lclsMultipac.dAgree_Date = .FieldToClass("dAgree_Date")
					
					Call AddM(lclsMultipac)
					
					'UPGRADE_NOTE: Object lclsMultipac may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					lclsMultipac = Nothing
					
					.RNext()
				Loop 
				FindMultipac = True
				.RCloseRec()
			Else
				FindMultipac = False
			End If
		End With
		
FindMultipac_Err: 
		If Err.Number Then
			FindMultipac = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lreaMultipac may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lreaMultipac = Nothing
	End Function
	Public Function AddM(ByVal objClass As Bank_Agree) As Bank_Agree
		
		If objClass Is Nothing Then
			objClass = New Bank_Agree
		End If
		
		mCol.Add(objClass, "AG" & objClass.nBank & objClass.dAgree_Date)
		
		AddM = objClass
	End Function
	Public Function Add(ByVal objClass As Bank_Agree) As Bank_Agree
		
		If objClass Is Nothing Then
			objClass = New Bank_Agree
		End If
		
		mCol.Add(objClass, "AG" & objClass.sType_BankAgree & objClass.nBank & objClass.nAccount)
		
		Add = objClass
	End Function
	
	'* Item: toma un elemento de la colección
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Bank_Agree
		Get
			
			Item = mCol.Item(vntIndexKey)
			
		End Get
	End Property
	
	'* Count: cuenta los elementos de la colección
	Public ReadOnly Property Count() As Integer
		Get
			
			Count = mCol.Count()
			
		End Get
	End Property
	
	'* NewEnum: enumera los elementos de la colección
	'UPGRADE_NOTE: NewEnum property was commented out. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="B3FC1610-34F3-43F5-86B7-16C984F0E88E"'
	'Public ReadOnly Property NewEnum() As stdole.IUnknown
		'Get
			'
			'NewEnum = mCol._NewEnum
			'
		'End Get
	'End Property
	
	Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
		'UPGRADE_TODO: Uncomment and change the following line to return the collection enumerator. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="95F9AAD0-1319-4921-95F0-B9D3C4FF7F1C"'
		GetEnumerator = mCol.GetEnumerator
	End Function
	
	'* Remove: elimina un elemento de la colección
	Public Sub Remove(ByRef vntIndexKey As Object)
		
		mCol.Remove(vntIndexKey)
		
	End Sub
	
	'* Class_Initialize: controla la apertura de la colección
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		
		mCol = New Collection
		
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'* Class_Terminate: controla el fin de la colección
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






