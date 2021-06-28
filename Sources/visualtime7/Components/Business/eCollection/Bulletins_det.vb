Option Strict Off
Option Explicit On
Public Class Bulletins_det
	'%-------------------------------------------------------%'
	'% $Workfile:: Bulletins_det.cls                        $%'
	'% $Author:: Nvaplat41                                  $%'
	'% $Date:: 10/10/03 17.35                               $%'
	'% $Revision:: 23                                       $%'
	'%-------------------------------------------------------%'
	'+ Propiedades según la tabla Bulletins_det 26/10/2000
	'+ Los campos llaves corresponden a nCod_agree
	'+ Name Column              Type            Null            Longitud
	'+ ======================== =============== =============== =============
	Public nBulletins As Double 'NOT NULL       NUMBER(10)
	Public nCollecDocTyp As String 'NOT NULL       CHAR(1)
	Public sDocument As String 'NOT NULL       CHAR(15)
	Public nContrat As Double '               Number(10)
	Public nDraft As Integer '               Number(5)
	Public sCertype As String '               Char(1)
	Public nBranch As Integer '               Number(5)
	Public nProduct As Integer '               Number(5)
	Public nReceipt As Double '               Number(10)
	Public nAmountpay As Double '               Number(10, 2)
	Public nUsercode As Integer 'NOT NULL       NUMBER(5)
	Public sTypdoc As String
	
	'+ Se define la variable que contiene el estado de la cada instancia de la clase
	Public nStatusInstance As Integer
	Public nPremium As Double
	Public nExchange As Double
	
	'+ Se define las variables para la CO514
	Public sClient As String
	Public sCliename As String
	Public nCurrency As Integer
	Public sCurrency As String
	Public nCancel_Cod As Integer
	Public sCancel_Cod As String
	Public nWay_Pay As Integer
	Public sWay_Pay As String
	Public nAmount_pa As Double
	Public sAmount_pa As String
	Public nStatus As Integer
	Public sStatus As String
	Public nNull_Cod As Integer
	Public nPolicy As Double
	
	Private mvarBulletins_dets As Bulletins_dets
	
	
	
	Public Property Bulletins_dets() As Bulletins_dets
		Get
			If mvarBulletins_dets Is Nothing Then
				mvarBulletins_dets = New Bulletins_dets
			End If
			
			Bulletins_dets = mvarBulletins_dets
		End Get
		Set(ByVal Value As Bulletins_dets)
			mvarBulletins_dets = Value
		End Set
	End Property
	
	'UPGRADE_NOTE: Class_Terminate was upgraded to Class_Terminate_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Terminate_Renamed()
		'UPGRADE_NOTE: Object mvarBulletins_dets may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mvarBulletins_dets = Nothing
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
	
	'% Find: busca los datos correspondientes para un cliente, año y concepto específico
	Public Function Find(ByVal Bulletins As Double, Optional ByVal Typdoc As String = "", Optional ByVal Document As String = "", Optional ByVal lblnFind As Boolean = False) As Boolean
		Dim lreaBulletins_det As eRemoteDB.Execute
		
		lreaBulletins_det = New eRemoteDB.Execute
		
		If Bulletins = nBulletins And Typdoc = nCollecDocTyp And Not lblnFind Then
			Find = True
		Else
			With lreaBulletins_det
				.StoredProcedure = "reaBulletins_det"
				.Parameters.Add("nBulletins", Bulletins, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nCollecDocTyp", Typdoc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				'.Parameters.Add "sDocument", Document, rdbParamInput, rdbVarChar, 15, 0, 0, rdbParamNullable
				If .Run Then
					nBulletins = .FieldToClass("nBulletins")
					nCollecDocTyp = .FieldToClass("nCollecdoctyp")
					'sDocument = .FieldToClass("sDocument")
					nContrat = .FieldToClass("nContrat")
					nDraft = .FieldToClass("nDraft")
					sCertype = .FieldToClass("sCertype")
					nBranch = .FieldToClass("nBranch")
					nProduct = .FieldToClass("nProduct")
					nReceipt = .FieldToClass("nReceipt")
					nAmountpay = .FieldToClass("nAmountpay")
					nUsercode = .FieldToClass("nUsercode")
					nPremium = .FieldToClass("nPremium")
					nExchange = .FieldToClass("nExchange")
					Find = True
					.RCloseRec()
				Else
					Find = False
				End If
			End With
			'UPGRADE_NOTE: Object lreaBulletins_det may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			lreaBulletins_det = Nothing
		End If
	End Function
	
	'% Add: Agrega los datos correspondientes para un convenio de pago por cliente
	Public Function Add() As Boolean
		Dim lreaBulletins_det As eRemoteDB.Execute
		
		lreaBulletins_det = New eRemoteDB.Execute
		
		'+ Definición de parámetros para stored procedure 'insudb.creFinanc_cli'
		'+ Información leída el 11/01/2000 14:33:46
		
		With lreaBulletins_det
			.StoredProcedure = "creBulletins_det"
			.Parameters.Add("nBulletins", nBulletins, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCollecDocTyp", nCollecDocTyp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			'.Parameters.Add "sDocument", sDocument, rdbParamInput, rdbVarChar, 15, 0, 0, rdbParamNullable
			.Parameters.Add("nContrat", nContrat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDraft", nDraft, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nReceipt", nReceipt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAmountpay", nAmountpay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Add = .Run(False)
		End With
		'UPGRADE_NOTE: Object lreaBulletins_det may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lreaBulletins_det = Nothing
	End Function
	
	'% Update: Actualiza los datos correspondientes para un cliente, año y concepto específico
	Public Function Update() As Boolean
		Dim lupdBulletins_det As eRemoteDB.Execute
		
		lupdBulletins_det = New eRemoteDB.Execute
		
		'+ Definición de parámetros para stored procedure 'insudb.updFinanc_cli'
		'+ Información leída el 11/01/2000 14:51:58
		
		With lupdBulletins_det
			.StoredProcedure = "updBulletins_det"
			.Parameters.Add("nBulletins", nBulletins, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCollecDocTyp", nCollecDocTyp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			' .Parameters.Add "sDocument", sDocument, rdbParamInput, rdbVarChar, 15, 0, 0, rdbParamNullable
			.Parameters.Add("nContrat", nContrat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDraft", nDraft, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nReceipt", nReceipt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAmountpay", nAmountpay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Update = .Run(False)
		End With
		'UPGRADE_NOTE: Object lupdBulletins_det may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lupdBulletins_det = Nothing
	End Function
	
	'% Delete: Elimina los datos correspondientes para un cliente, año y concepto específico
	Public Function Delete() As Boolean
		Dim ldelBulletins_det As eRemoteDB.Execute
		
		ldelBulletins_det = New eRemoteDB.Execute
		
		'+ Definición de parámetros para stored procedure 'insudb.delFinanc_cli'
		'+ Información leída el 11/01/2000 14:50:44
		
		With ldelBulletins_det
			.StoredProcedure = "delBulletins_det"
			.Parameters.Add("nBulletins", nBulletins, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCollecDocTyp", nCollecDocTyp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			'.Parameters.Add "sDocument", sDocument, rdbParamInput, rdbVarChar, 15, 0, 0, rdbParamNullable
			Delete = .Run(False)
		End With
		'UPGRADE_NOTE: Object ldelBulletins_det may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		ldelBulletins_det = Nothing
	End Function
	
	'% Find: busca los datos correspondientes para un cliente, año y concepto específico
	Public Function Count_Bulletins_det(Optional ByVal Bulletins As Double = 0, Optional ByVal Typdoc As String = "", Optional ByVal Document As String = "", Optional ByVal lblnFind As Boolean = False) As Boolean
		Dim lreaBulletins_det As eRemoteDB.Execute
		
		lreaBulletins_det = New eRemoteDB.Execute
		
		If Bulletins = nBulletins And Typdoc = nCollecDocTyp And Not lblnFind Then
			Count_Bulletins_det = True
		Else
			
			'+ Definición de parámetros para stored procedure 'insudb.reaFinanc_cli'
			'+ Información leída el 11/01/2000 14:09:20
			
			With lreaBulletins_det
				.StoredProcedure = "valBulletins_det"
				'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
				.Parameters.Add("nBulletins", IIf(Bulletins = 0, System.DBNull.Value, Bulletins), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
				.Parameters.Add("nCollecDocTyp", IIf(Typdoc = String.Empty, System.DBNull.Value, Typdoc), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				'.Parameters.Add "sDocument", IIf(Document = String.Empty, Null, Document), rdbParamInput, rdbVarChar, 15, 0, 0, rdbParamNullable
				
				If .Run Then
					If .FieldToClass("lCount") = 1 Then
						Count_Bulletins_det = True
					End If
					.RCloseRec()
				Else
					Count_Bulletins_det = False
				End If
			End With
			'UPGRADE_NOTE: Object lreaBulletins_det may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			lreaBulletins_det = Nothing
		End If
	End Function
	'% insValCO514: Esta función se encarga de validar los datos introducidos en la cabecera de la
	'% forma.
	Public Function insValCO514_K(ByVal sCodispl As String, ByVal nBulletins As Double, ByVal nAction As Integer) As String
		Dim lerrTime As eFunctions.Errors
		Dim ldllBulletins As eCollection.Bulletin
		
		lerrTime = New eFunctions.Errors
		
		On Error GoTo insValVal514_K_Err
		
		ldllBulletins = New eCollection.Bulletin
		
		
		'+ Se valida si el codigo es valido (por el caso en que sea juridico)
		If nBulletins <= 0 Then
			Call lerrTime.ErrorMessage(sCodispl, 55019)
		Else
			With ldllBulletins
				If Not .Find(nBulletins) Then
					Call lerrTime.ErrorMessage(sCodispl, 55016)
				Else
					If nAction <> 401 Then
						If .nStatus <> 1 And .nStatus <> 4 Then
							Call lerrTime.ErrorMessage(sCodispl, 55020)
						End If
					End If
				End If
			End With
		End If
		
		insValCO514_K = lerrTime.Confirm
		
		'UPGRADE_NOTE: Object lerrTime may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lerrTime = Nothing
		'UPGRADE_NOTE: Object ldllBulletins may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		ldllBulletins = Nothing
		
insValVal514_K_Err: 
		If Err.Number Then
			insValCO514_K = "insValCO514_K: " & Err.Description
		End If
		
		On Error GoTo 0
		
	End Function
	
	'%insValCO514: Se efectuan las validaciones del la CO514
	Public Function insValCO514(ByVal sCodispl As String, ByVal nCause As Integer, ByVal nAction As Integer) As String
		Dim lerrTime As eFunctions.Errors '+ eFunctions.Errors
		On Error GoTo insValCO514_Err
		lerrTime = New eFunctions.Errors
		insValCO514 = String.Empty
		On Error GoTo insValCO514_Err
		
		insValCO514 = String.Empty
		
		'+ Validación del campo "Causa de anulación" si la accion es distinta de consultar
		
		If nAction <> 401 Then
			If nCause <= 0 Then
				Call lerrTime.ErrorMessage(sCodispl, 10895)
			End If
		End If
		
		insValCO514 = lerrTime.Confirm
		
		'UPGRADE_NOTE: Object lerrTime may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lerrTime = Nothing
		
insValCO514_Err: 
		If Err.Number Then
			insValCO514 = insValCO514 & Err.Description
		End If
	End Function
	
	'% insPostC0514: Crea/actualiza los registros correspondientes en la tabla
	Public Function insPostC0514(ByVal sAction As String, ByVal nNullCode As Integer, ByVal nBulletin As Double, ByVal nUsercode As Integer) As Boolean
		With Me
			.nBulletins = nBulletin
			.nCancel_Cod = nNullCode
			.nUsercode = nUsercode
		End With
		
		insPostC0514 = UpdateCancel_code(nBulletin)
		
		
	End Function
	
	'% insFindC0514: Crea/actualiza los registros correspondientes en la tabla
	Public Function insFindC0514_K(ByVal sCodispl As String, ByVal nBulletins As Double) As Boolean
		Dim ldllBulletins As eCollection.Bulletin
		Dim lremQuery As eRemoteDB.Query
		Dim lvalField As eFunctions.valField
		
		lremQuery = New eRemoteDB.Query
		ldllBulletins = New eCollection.Bulletin
		lvalField = New eFunctions.valField
		
		
		If ldllBulletins.Find(nBulletins) Then
			insFindC0514_K = True
			
			If lremQuery.OpenQuery("table11", "sDescript", "nCodigint = " & ldllBulletins.nCurrency) Then
				nCurrency = ldllBulletins.nCurrency
				sCurrency = lremQuery.FieldToClass("sDescript")
				lremQuery.CloseQuery()
			End If
			
			If lremQuery.OpenQuery("table5002", "sDescript", "nWay_pay = " & ldllBulletins.nWay_Pay) Then
				sWay_Pay = lremQuery.FieldToClass("sDescript")
				nWay_Pay = ldllBulletins.nWay_Pay
				lremQuery.CloseQuery()
			End If
			lvalField.ValFormat = "###,###,###,##0.00"
			If lvalField.ValNumber(ldllBulletins.nAmount) Then
				nAmount_pa = lvalField.Value
				sAmount_pa = lvalField.Value
			End If
			
			nStatus = ldllBulletins.nStatus
			If lremQuery.OpenQuery("table5004", "sDescript", "nStatus = " & ldllBulletins.nStatus) Then
				sStatus = lremQuery.FieldToClass("sDescript")
				lremQuery.CloseQuery()
			End If
		End If
		
		If ldllBulletins.nCancel_Cod <> eRemoteDB.Constants.intNull Then
			nNull_Cod = ldllBulletins.nCancel_Cod
		Else
			nNull_Cod = eRemoteDB.Constants.intNull
		End If
		
		sClient = ldllBulletins.sClient
		sCliename = ldllBulletins.sCliename
		nCancel_Cod = ldllBulletins.nCancel_Cod
		nCurrency = ldllBulletins.nCurrency
		nWay_Pay = ldllBulletins.nWay_Pay
		nAmount_pa = lvalField.Value
		nStatus = ldllBulletins.nStatus
		
		'UPGRADE_NOTE: Object lremQuery may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lremQuery = Nothing
		'UPGRADE_NOTE: Object ldllBulletins may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		ldllBulletins = Nothing
		'UPGRADE_NOTE: Object lvalField may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lvalField = Nothing
		
	End Function
	
	'% UpdateStatBulletin: Esta función modifica el Estado del Boletin.
	Public Function UpdateCancel_code(ByVal nBulletin As Double) As Boolean
		Dim lrecupdBulletin_Cancel As eRemoteDB.Execute
		
		On Error GoTo Err_UpdateCancel_code
		
		lrecupdBulletin_Cancel = New eRemoteDB.Execute
		
		With lrecupdBulletin_Cancel
			.StoredProcedure = "updBulletin_CancelCode"
			
			.Parameters.Add("nBulletin", nBulletin, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCancel_code", nCancel_Cod, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			UpdateCancel_code = .Run(False)
		End With
		
		'UPGRADE_NOTE: Object lrecupdBulletin_Cancel may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecupdBulletin_Cancel = Nothing
		
Err_UpdateCancel_code: 
		If Err.Number Then
			UpdateCancel_code = False
		End If
		
		On Error GoTo 0
	End Function
	
	'% valMoreDocBulletins_det: Valida si existen más documentos asociados al boletín pasado como parámetro (Los documentos procesados son 1-Recibos, 2-Cuotas y 10-Intereses por préstamo).
	'% Devuelve: True -> El boletín tiene asociado más de un documento; False -> No tiene más documentos asociados.
	Public Function valMoreDocBulletins_det(ByVal nBulletins As Double) As Boolean
		Dim lrecBulletins_det As eRemoteDB.Execute
		
		lrecBulletins_det = New eRemoteDB.Execute
		
		On Error GoTo valMoreDocBulletins_det_Err
		
		With lrecBulletins_det
			.StoredProcedure = "reaCountDocBulletins_det"
			.Parameters.Add("nBulletins", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCount", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				If .Parameters.Item("nCount").Value > 1 Then
					valMoreDocBulletins_det = True
				End If
			End If
		End With
		
valMoreDocBulletins_det_Err: 
		If Err.Number Then
			valMoreDocBulletins_det = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecBulletins_det may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecBulletins_det = Nothing
	End Function
End Class






