Option Strict Off
Option Explicit On
Public Class Agreements
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: Agreements.cls                            $%'
	'% $Author:: Nvapla10                                   $%'
	'% $Date:: 20/10/04 3:35p                               $%'
	'% $Revision:: 15                                       $%'
	'%-------------------------------------------------------%'
	
	'- Local variable to hold collection
	Private mCol As Collection
	
	'% Find: Muestra todos los datos correspondiente a un convenio
	Public Function Find(ByVal nCod_Agree As Integer) As Boolean
        Dim mlngCod_agree As Object = 0
		Dim lreaAgreement As eRemoteDB.Execute
		Dim lclsAgreement As Agreement
		Dim varrClient() As String
		
		On Error GoTo Find_Err
		lreaAgreement = New eRemoteDB.Execute
		
		If nCod_Agree = mlngCod_agree Then
			Find = True
		Else
			
			'+ Definición de parámetros para stored procedure 'insudb.reaFinanc_cli'
			'+ Información leída el 11/01/2000 14:54:21
			
			With lreaAgreement
				.StoredProcedure = "reaAgreementCli"
				.Parameters.Add("nCod_agree", nCod_Agree, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
				.Parameters.Add("sClient", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If .Run Then
					Do While Not .EOF
						lclsAgreement = New Agreement
						lclsAgreement.nStatusInstance = 1
						lclsAgreement.nCod_Agree = .FieldToClass("nCod_agree")
						lclsAgreement.sClient = .FieldToClass("sClient")
						varrClient = Microsoft.VisualBasic.Split(.FieldToClass("sClient_desc"), "|")
						lclsAgreement.sDigit = varrClient(1)
						lclsAgreement.sCliename = varrClient(2)
						lclsAgreement.nQ_draft = .FieldToClass("nQ_draft")
						lclsAgreement.nMax_perc_dcto = .FieldToClass("nMax_perc_dcto")
						lclsAgreement.dInit_date = .FieldToClass("dInit_date")
						lclsAgreement.dEnd_date = .FieldToClass("dEnd_date")
						lclsAgreement.sStatregt = .FieldToClass("sStatregt")
						lclsAgreement.sStatregt_desc = .FieldToClass("sStatregt_Desc")
						lclsAgreement.nTypeAgree = .FieldToClass("nTypeagree")
						lclsAgreement.sTypeAgree_desc = .FieldToClass("sTypeAgree_desc")
						lclsAgreement.nIntermed = .FieldToClass("nIntermed")
						lclsAgreement.sIntermed_desc = .FieldToClass("sIntermed_desc")
						lclsAgreement.nAgency = .FieldToClass("nAgency")
						lclsAgreement.sAgency_desc = .FieldToClass("sAgency_desc")
						lclsAgreement.nType_rec = .FieldToClass("nType_rec")
						lclsAgreement.sType_Rec_desc = .FieldToClass("sType_Rec_desc")
						lclsAgreement.sFirstName = .FieldToClass("sFirstName")
						lclsAgreement.sLastName = .FieldToClass("sLastName")
						lclsAgreement.sCliename = .FieldToClass("sClienName")
						lclsAgreement.nposition = .FieldToClass("nPosition")
						lclsAgreement.sEmail_Contact = .FieldToClass("sEmail_Contact")
						lclsAgreement.sPhone_Contact = .FieldToClass("sPhone_Contact")
						lclsAgreement.sName_Agree = .FieldToClass("sName_Agree")
						
						Call Add(lclsAgreement)
						'UPGRADE_NOTE: Object lclsAgreement may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
						lclsAgreement = Nothing
						.RNext()
					Loop 
					.RCloseRec()
					Find = True
					mlngCod_agree = nCod_Agree
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
		'UPGRADE_NOTE: Object lreaAgreement may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lreaAgreement = Nothing
	End Function
	
	'% Find: busca los datos correspondientes a un cliente
	Public Function Find_sClient(Optional ByVal nCod_Agree As Integer = 0, Optional ByVal sClient As String = "", Optional ByVal lblnFind As Boolean = False) As Boolean
        Dim mstrClient As String = String.Empty
        Dim Client As Object = String.Empty
        Dim gintUser As Integer = eRemoteDB.Constants.intNull
		Dim lreaAgreement As eRemoteDB.Execute
		Dim lclsAgreement As Agreement
		Dim varrClient() As String
		
		On Error GoTo Find_sClient_Err
		lreaAgreement = New eRemoteDB.Execute
		
		If sClient = mstrClient Then
			Find_sClient = True
		Else
			
			'+ Definición de parámetros para stored procedure 'insudb.reaFinanc_cli'
			'+ Información leída el 11/01/2000 14:54:21
			
			With lreaAgreement
				.StoredProcedure = "reaAgreementCli"
                .Parameters.Add("nCod_Agree", IIf(nCod_Agree = 0, System.DBNull.Value, nCod_Agree), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("sClient", IIf(sClient = String.Empty, System.DBNull.Value, sClient), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If .Run Then
					Do While Not .EOF
						lclsAgreement = New Agreement
						lclsAgreement.nStatusInstance = 1
						lclsAgreement.nCod_Agree = .FieldToClass("nCod_agree")
						lclsAgreement.sClient = sClient
						varrClient = Microsoft.VisualBasic.Split(.FieldToClass("sClient_desc"), "|")
						lclsAgreement.sDigit = varrClient(1)
						lclsAgreement.sCliename = varrClient(2)
						lclsAgreement.nQ_draft = .FieldToClass("nQ_draft")
						lclsAgreement.nMax_perc_dcto = .FieldToClass("nMax_perc_dcto")
						lclsAgreement.dInit_date = .FieldToClass("dInit_date")
						lclsAgreement.dEnd_date = .FieldToClass("dEnd_date")
						lclsAgreement.sStatregt = .FieldToClass("sStatregt")
						lclsAgreement.sStatregt_desc = .FieldToClass("sStatregt_Desc")
						lclsAgreement.nUsercode = gintUser
						lclsAgreement.nTypeAgree = .FieldToClass("nTypeagree")
						lclsAgreement.sTypeAgree_desc = .FieldToClass("sTypeAgree_desc")
						lclsAgreement.nIntermed = .FieldToClass("nIntermed")
						lclsAgreement.sIntermed_desc = .FieldToClass("sIntermed_desc")
						lclsAgreement.nAgency = .FieldToClass("nAgency")
						lclsAgreement.sAgency_desc = .FieldToClass("sAgency_desc")
						lclsAgreement.nType_rec = .FieldToClass("nType_rec")
						lclsAgreement.sType_Rec_desc = .FieldToClass("sType_Rec_desc")
						lclsAgreement.sFirstName = .FieldToClass("sFirstName")
						lclsAgreement.sLastName = .FieldToClass("sLastName")
						lclsAgreement.sCliename = .FieldToClass("sClienName")
						lclsAgreement.nposition = .FieldToClass("nPosition")
						lclsAgreement.sEmail_Contact = .FieldToClass("sEmail_Contact")
						lclsAgreement.sPhone_Contact = .FieldToClass("sPhone_Contact")
						lclsAgreement.sName_Agree = .FieldToClass("sName_Agree")
                        lclsAgreement.snocollection = .FieldToClass("snocollection")

						Call Add(lclsAgreement)
						'UPGRADE_NOTE: Object lclsAgreement may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
						lclsAgreement = Nothing
						.RNext()
					Loop 
					.RCloseRec()
					Find_sClient = True
					mstrClient = Client
				Else
					Find_sClient = False
				End If
			End With
		End If
Find_sClient_Err: 
		If Err.Number Then
			Find_sClient = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lreaAgreement may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lreaAgreement = Nothing
	End Function
	
	'% Add: Añade una nueva instancia de la Agreement
	Public Function Add(ByVal objClass As Agreement) As Agreement
		If objClass Is Nothing Then
			objClass = New Agreement
		End If
		mCol.Add(objClass, "AG" & objClass.nCod_Agree)
		Add = objClass
	End Function
	
	'% AddCOC625: Añade una nueva instancia de la Agreement
	Public Function AddCOC625(ByVal objClass As Agreement) As Agreement
		If objClass Is Nothing Then
			objClass = New Agreement
		End If
		mCol.Add(objClass)
		AddCOC625 = objClass
	End Function
	
	'% Update: Actualizacion de Agrrement
	Public Function Update() As Boolean
        Dim mlngCod_agree As Object = 0
		Dim lclsAgreement As Agreement
		
		'+ Valores posibles para nStatusInstance
		'+ 0: El registro es nuevo
		'+ 1: El registro ya existe en la tabla
		'+ 2: El registro ya existe, hay que actualizarlo
		'+ 3: El registro ya existe, hay que eliminarlo
		Update = True
		For	Each lclsAgreement In mCol
			With lclsAgreement
				If mlngCod_agree = 0 Then
					mlngCod_agree = .nCod_Agree
				End If
				Select Case .nStatusInstance
					Case 0
						Update = .Add
						.nStatusInstance = 1
					Case 2
						Update = .Update
					Case 3
						Update = .Delete
						mCol.Remove(("AG" & .nCod_Agree))
				End Select
			End With
		Next lclsAgreement
		
	End Function
	
	'% Find_COC625: busca los datos para la transacción
	Public Function Find_COC625(ByVal nCod_Agree As Integer, ByVal dEffecdate As Date, ByVal dExpirdate As Date, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nStatus_pre As Integer) As Boolean
		Dim lclsRemote As eRemoteDB.Execute
		Dim lclsAgreement As Agreement
		
		On Error GoTo Find_COC625_Err
		
		lclsRemote = New eRemoteDB.Execute
		
		'+ Definición de parámetros para stored procedure 'reaPremium_COC625'
		'+ Información leída el 11/01/2000 14:09:20
		
		With lclsRemote
			.StoredProcedure = "reaPremium_COC625"
			.Parameters.Add("nCod_Agree", nCod_Agree, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dExpirdat", dExpirdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nStatus_pre", nStatus_pre, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run() Then
				Do While Not .EOF
					lclsAgreement = New Agreement
					lclsAgreement.nBranch = .FieldToClass("nBranch")
					lclsAgreement.nProduct = .FieldToClass("nProduct")
					lclsAgreement.nPolicy = .FieldToClass("nPolicy")
					lclsAgreement.nReceipt = .FieldToClass("nReceipt")
					lclsAgreement.nCurrency = .FieldToClass("nCurrency")
					lclsAgreement.nPremium = .FieldToClass("nPremium")
					lclsAgreement.dLimitdate = .FieldToClass("dLimitdate")
					lclsAgreement.nStatus_pre = .FieldToClass("nStatus_pre")
					lclsAgreement.sClient = .FieldToClass("sClient")
					lclsAgreement.nPremium = .FieldToClass("nPremium")
					lclsAgreement.nContrat = .FieldToClass("nContrat")
					lclsAgreement.nDraft = .FieldToClass("nDraft")
					'+ Si tiene un contrato de financiamiento asociado, se asigna el monto de la cuota,
					'+ sino, el monto del recibo
					lclsAgreement.nAmount = IIf(.FieldToClass("nContrat") = 0 Or .FieldToClass("nContrat") = eRemoteDB.Constants.intNull, lclsAgreement.nPremium, .FieldToClass("nAmount"))
					Call AddCOC625(lclsAgreement)
					'UPGRADE_NOTE: Object lclsAgreement may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					lclsAgreement = Nothing
					.RNext()
				Loop 
				Find_COC625 = True
			End If
		End With
		
Find_COC625_Err: 
		If Err.Number Then
			Find_COC625 = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsRemote may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsRemote = Nothing
		'UPGRADE_NOTE: Object lclsAgreement may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsAgreement = Nothing
	End Function
	
	'* Item: toma un elemento de la colección
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Agreement
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
			'NewEnum = mCol._NewEnum
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






