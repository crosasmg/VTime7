Option Strict Off
Option Explicit On
Public Class Claim_his
	'%-------------------------------------------------------%'
	'% $Workfile:: Claim_his.cls                            $%'
	'% $Author:: Nvapla10                                   $%'
	'% $Date:: 31/08/04 6:06p                               $%'
	'% $Revision:: 50                                       $%'
	'%-------------------------------------------------------%'
	
	'**-Define the principals proerties of the correspondent class to the Claim_his table (01/10/2001)
	'-Se definen las propiedades principales de la clase correspondientes a la tabla Claim_his (10/01/2001)
	
	'   Column_name                            Type                                                                                                                             Computed                            Length      Prec  Scale Nullable                            TrimTrailingBlanks                  FixedLenNullInSource
	Public nClaim As Double 'int                                                                                                                              no                                  4           10    0     no                                  (n/a)                               (n/a)
	Public nCase_num As Integer 'smallint                                                                                                                         no                                  2           5     0     no                                  (n/a)                               (n/a)
	Public nDeman_type As Integer 'smallint                                                                                                                         no                                  2           5     0     no                                  (n/a)                               (n/a)
	Public nTransac As Integer 'smallint                                                                                                                         no                                  2           5     0     no                                  (n/a)                               (n/a)
	Public nAmount As Double 'decimal                                                                                                                          no                                  9           14    2     yes                                 (n/a)                               (n/a)
	Public nOper_type As Integer 'smallint                                                                                                                         no                                  2           5     0     no                                  (n/a)                               (n/a)
	Public sCessiCoi As String 'char                                                                                                                             no                                  1                       yes                                 no                                  yes
	Public nCurrency As Integer 'smallint                                                                                                                         no                                  2           5     0     yes                                 (n/a)                               (n/a)
	Public nExchange As Double 'decimal                                                                                                                          no                                  9           10    6     yes                                 (n/a)                               (n/a)
	Public sExecuted As String 'char                                                                                                                             no                                  1                       yes                                 no                                  yes
	Public nInc_amount As Double 'decimal                                                                                                                          no                                  9           10    2     yes                                 (n/a)                               (n/a)
	Public nIncometax As Double 'decimal                                                                                                                          no                                  5           4     2     yes                                 (n/a)                               (n/a)
	Public sInd_aut As String 'char                                                                                                                             no                                  1                       yes                                 no                                  yes
	Public sInd_order As String 'char                                                                                                                             no                                  1                       yes                                 no                                  yes
	Public sInd_rev As String 'char                                                                                                                             no                                  1                       yes                                 no                                  yes
	Public nLoc_amount As Double 'decimal                                                                                                                          no                                  9           14    2     yes                                 (n/a)                               (n/a)
	Public dOperdate As Date 'datetime                                                                                                                         no                                  8                       yes                                 (n/a)                               (n/a)
	Public sOrder_num As String 'char                                                                                                                             no                                  10                      yes                                 no                                  yes
	Public nPay_type As Integer
	Public dPosted As Date 'datetime                                                                                                                         no                                  8                       yes                                 (n/a)                               (n/a)
	Public nServ_Order As Double 'int                                                                                                                              no                                  4           10    0     yes                                 (n/a)                               (n/a)
	Public sClient As String 'char                                                                                                                             no                                  14                      yes                                 no                                  yes
	Public sDest_Cheque As String 'char                                                                                                                             no                                  14                      yes                                 no                                  yes
	Public nBordereaux As Integer 'int                                                                                                                              no                                  4           10    0     yes                                 (n/a)                               (n/a)
	Public nPay_form As Integer
	Public nUserCode As Integer 'smallint                                                                                                                         no                                  2           5     0     yes                                 (n/a)                               (n/a)
	Public sKey As String
	Public sIndCl_Pay As String
	Public nNotenum As Integer 'smallint
	Public nAmountPay_Orig As Double
	Public nCurrency_Orig As Integer

    Public nOffice_Pay As Integer 
    Public nOfficeAgen_Pay As Integer 
    Public nAgency_Pay As Integer 

	
	Public sConcept As String
	
	'**-Defined the variable that contains the status of each instance of the class
	'- Se define la variable que contiene el estado de la cada instancia de la clase
	Public nStatusInstance As Integer
	
	'**-Defined the variable that indicates if the reverse record generated transactions
	'- Se define la variable que indica si el registro de reverso genero movimientos
	Public nAso As Integer
	
	Public dDecladat As Date
	Public nOffice As Integer
	Public nPolicy As Double
	Public nCertif As Double
	Public nRequest_nu As Integer
	Public sCheque As String
	Public nConsec As Integer
	Public nConcept As Integer
	Public nAmountPay As Double
	Public nAmountCov As Double
	Public nSta_cheque As Integer
	Public sMovement As String
	Public sOper_type As String
	'Public nAmountPay_Orig As Double
	
	'- Tipo registro
	Private Structure udtClaim_PayOrders
		Dim nClaim As Double
		Dim dDecladat As Date
		Dim nOffice As Integer
		Dim nPolicy As Double
		Dim nCertif As Double
		Dim nRequest_nu As Integer
		Dim sCheque As String
		Dim nConsec As Integer
		Dim nConcept As Integer
		Dim nAmountPay As Double
		Dim sClient As String
		Dim sDest_Cheque As String
		Dim nSta_cheque As Integer
		Dim sMovement As String
		Dim nTransac As Integer
		Dim dOperdate As Date
		Dim nCurrency As Integer
		Dim nExchange As Double
		Dim nAmountPay_Orig As Double
		Dim nCurrency_Orig As Integer
        Dim nOffice_Pay As Integer 
        Dim nOfficeAgen_Pay As Integer 
        Dim nAgency_Pay As Integer 
	End Structure
	
	'- Arreglo
	Private arrClaim_PayOrders() As udtClaim_PayOrders
	'
	
	'**%FindTransac: Find the claim data into the Claim_his table from the claim number and movement number
	'% FindTransac: Busca los datos del siniestro en la tabla Claim_his a partir del número de siniestro dado y número de movimiento
	Public Function FindTransac(ByVal nClaim As Double, ByVal nTransac As Integer, Optional ByVal lblnFind As Boolean = False) As Boolean
		Dim lrecClaim_his As eRemoteDB.Execute
		Static lblnRead As Boolean
		Static llngOldClaim As Double
		Static lintOldTransac As Integer
		
		On Error GoTo FindTransac_err
		
		If llngOldClaim <> nClaim Or lintOldTransac <> nTransac Or lblnFind Then
			
			llngOldClaim = nClaim
			lintOldTransac = nTransac
			
			lrecClaim_his = New eRemoteDB.Execute
			With lrecClaim_his
				.StoredProcedure = "reaClaim_his_v"
				.Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nTransac", nTransac, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				
				If .Run Then
					nClaim = .FieldToClass("nClaim")
					nCase_num = .FieldToClass("nCase_num")
					nDeman_type = .FieldToClass("nDeman_type")
					nTransac = .FieldToClass("nTransac")
					nAmount = .FieldToClass("nAmount")
					nOper_type = .FieldToClass("nOper_type")
					sCessiCoi = .FieldToClass("sCessicoi")
					nCurrency = .FieldToClass("nCurrency")
					nExchange = .FieldToClass("nExchange")
					sExecuted = .FieldToClass("sExecuted")
					nInc_amount = .FieldToClass("nInc_amount")
					nIncometax = .FieldToClass("nIncometax")
					sInd_aut = .FieldToClass("sInd_aut")
					sInd_order = .FieldToClass("sInd_order")
					sInd_rev = .FieldToClass("sInd_rev")
					nLoc_amount = .FieldToClass("nLoc_amount")
					dOperdate = .FieldToClass("dOperdate")
					sOrder_num = .FieldToClass("sOrder_num")
					nPay_type = .FieldToClass("nPay_type")
					dPosted = .FieldToClass("dPosted")
					nNotenum = .FieldToClass("nNotenum")
					nServ_Order = .FieldToClass("nServ_order")
					sClient = .FieldToClass("sClient")
					nBordereaux = .FieldToClass("nBordereaux")
					lblnRead = True
				Else
					lblnRead = False
				End If
				.RCloseRec()
			End With
		End If
		
		FindTransac = lblnRead
		
FindTransac_err: 
		If Err.Number Then
			FindTransac = False
		End If
		On Error GoTo 0
		lrecClaim_his = Nothing
	End Function
	'% FindMovReserv: Busca los datos del siniestro en la tabla Claim_his a partir del número de siniestro dado y número de movimiento
	Public Function FindMovReserv(ByVal nClaim As Double) As Boolean
		Dim lrecClaim_his As eRemoteDB.Execute
		
		On Error GoTo FindMovReserv_err
		
		lrecClaim_his = New eRemoteDB.Execute
		With lrecClaim_his
			.StoredProcedure = "reaClaim_hisMovReserv"
			.Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nExist", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Run(False)
			
			If .Run(False) Then
				FindMovReserv = (.Parameters("nExist").Value = 1)
			Else
				FindMovReserv = False
			End If
			
		End With
		
FindMovReserv_err: 
		If Err.Number Then
			FindMovReserv = False
		End If
		On Error GoTo 0
		lrecClaim_his = Nothing
	End Function
	'% FindMovReservCaus: Busca los datos del siniestro en la tabla Claim_his a partir del número de siniestro dado y número de movimiento
	Public Function FindMovReservCase(ByVal nClaim As Double, ByVal nCase_num As Integer) As Boolean
		Dim lrecClaim_his As eRemoteDB.Execute
		
		On Error GoTo FindMovReservCase_err
		
		lrecClaim_his = New eRemoteDB.Execute
		With lrecClaim_his
			.StoredProcedure = "reaClaim_hisMovReservCase"
			.Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCase_Num", nCase_num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nExist", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Run(False)
			
			If .Run(False) Then
				FindMovReservCase = (.Parameters("nExist").Value = 1)
			Else
				FindMovReservCase = False
			End If
		End With
		
FindMovReservCase_err: 
		If Err.Number Then
			FindMovReservCase = False
		End If
		On Error GoTo 0
		lrecClaim_his = Nothing
	End Function
	
	'**% FindTransac: Find the claim data in the Claim_his table from the claim number and movement number
	'% FindTransac: Busca los datos del siniestro en la tabla Claim_his a partir del número de siniestro dado y número de movimiento
	Public Function ValClaimHisOper(ByVal nClaim As Double, ByVal nOpertype1 As Integer, ByVal nOpertype2 As Integer, ByVal nCompare As Integer, Optional ByVal lblnFind As Boolean = False) As Boolean
		Dim lrecClaim_his As eRemoteDB.Execute
		Static llngOldClaim As Integer
		Static lintOldOpertype1 As Integer
		Static lintOldOpertype2 As Integer
		Static lintOldCompare As Integer
		
		On Error GoTo ValClaimHisOper_err
		
		lrecClaim_his = New eRemoteDB.Execute
		
		If llngOldClaim <> nClaim Or lintOldOpertype1 <> nOpertype1 Or lintOldOpertype2 <> nOpertype2 Or lintOldCompare <> nCompare Or lblnFind Then
			
			llngOldClaim = nClaim
			lintOldOpertype1 = nOpertype1
			lintOldOpertype2 = nOpertype2
			lintOldCompare = nCompare
			
			'+ Definición de parámetros para stored procedure 'insudb.reaClaimHisOper'
			With lrecClaim_his
				.StoredProcedure = "reaClaimHisOper"
				.Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nOpertype1", nOpertype1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nOpertype2", nOpertype2, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nCompare", nCompare, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If .Run Then
					If .FieldToClass("exist") > 0 Then
						ValClaimHisOper = True
					Else
						ValClaimHisOper = False
					End If
					.RCloseRec()
				Else
					ValClaimHisOper = False
				End If
			End With
		Else
			ValClaimHisOper = True
		End If
		
ValClaimHisOper_err: 
		If Err.Number Then
			ValClaimHisOper = False
		End If
		On Error GoTo 0
		lrecClaim_his = Nothing
	End Function
	
	'**% Update: Find the claim data in the Claim_his table from the claim number and the movements number
	'% Update: Busca los datos del siniestro en la tabla Claim_his a partir del número de siniestro dado y número de movimiento
	Public Function Update() As Boolean
		Dim lrecinsClaim_his As eRemoteDB.Execute
		
		lrecinsClaim_his = New eRemoteDB.Execute
		
		On Error GoTo Update_Err
		
		'+ Definición de parámetros para stored procedure 'insudb.insClaim_his'
		With lrecinsClaim_his
			.StoredProcedure = "insClaim_his"
			.Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sInd_aut", sInd_aut, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTransac", nTransac, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOper_type", nOper_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dOperdate", dOperdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 22, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUserCode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCase_num", nCase_num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDeman_type", nDeman_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAmount", nAmount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCessicoi", sCessiCoi, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nExchange", nExchange, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sExecuted", sExecuted, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInc_amount", nInc_amount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nIncometax", nIncometax, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sInd_order", sInd_order, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sInd_rev", sInd_rev, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nLoc_amount", nLoc_amount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sOrder_num", sOrder_num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPay_type", nPay_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dPosted", dPosted, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nServ_order", nServ_Order, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBordereaux", nBordereaux, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Update = .Run(False)
		End With
		
Update_Err: 
		If Err.Number Then
			Update = False
		End If
		On Error GoTo 0
		lrecinsClaim_his = Nothing
	End Function
	
	'% Update_dates:
	Public Function Update_dates() As Boolean
		Dim lrecinsClaim_his As eRemoteDB.Execute
		
		lrecinsClaim_his = New eRemoteDB.Execute
		
		On Error GoTo Update_dates_err
		
		With lrecinsClaim_his
			'Definición de parámetros para stored procedure 'insudb.updClaim_his'
			.StoredProcedure = "updClaim_his"
			.Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dOperdate", dOperdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dPosted", dPosted, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUserCode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Update_dates = .Run(False)
		End With
		
Update_dates_err: 
		If Err.Number Then
			Update_dates = False
		End If
		On Error GoTo 0
		lrecinsClaim_his = Nothing
	End Function
	'% Update_Notes:
	Public Function Update_Notes() As Boolean
		Dim lrecinsClaim_his As eRemoteDB.Execute
		
		lrecinsClaim_his = New eRemoteDB.Execute
		
		On Error GoTo Update_Notes_err
		
		With lrecinsClaim_his
			'Definición de parámetros para stored procedure 'insudb.updClaim_his'
			.StoredProcedure = "updClaim_hisNotes"
			.Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTransac", nTransac, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 20, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nnotenum", nNotenum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUserCode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Update_Notes = .Run(False)
		End With
		
Update_Notes_err: 
		If Err.Number Then
			Update_Notes = False
		End If
		On Error GoTo 0
		lrecinsClaim_his = Nothing
	End Function
	
	'**% valClaimHisOper_in: Find the claim data into the Claim_his table from the claim number and the movement number
	'% ValClaimHisOper_in: Busca los datos del siniestro en la tabla Claim_his a partir del número de siniestro dado y número de movimiento
	Public Function ValClaimHisOper_in(ByVal nClaim As Double, ByVal sValue As String, ByVal nCompare As Integer, Optional ByVal lblnFind As Boolean = False) As Boolean
		Dim lrecClaim_his As eRemoteDB.Execute
		Dim lintExists As Short
		Static llngOldClaim As Integer
		Static lstrOldValue As String
		Static lintOldCompare As Integer
		
		
		lrecClaim_his = New eRemoteDB.Execute
		
		On Error GoTo ValClaimHisOper_in_err
		
		If llngOldClaim <> nClaim Or lstrOldValue <> sValue Or lintOldCompare <> nCompare Or lblnFind Then
			
			llngOldClaim = nClaim
			lstrOldValue = sValue
			lintOldCompare = nCompare
			
			With lrecClaim_his
				.StoredProcedure = "valExistClaimHisOper_in"
				.Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("sValues", sValue, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 80, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nCompare", nCompare, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nExists", lintExists, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Run(False)
				ValClaimHisOper_in = .Parameters("nExists").Value = 1
			End With
		Else
			ValClaimHisOper_in = True
		End If
		
ValClaimHisOper_in_err: 
		If Err.Number Then
			ValClaimHisOper_in = False
		End If
		On Error GoTo 0
		lrecClaim_his = Nothing
	End Function
	
	Public Function insValSI010(ByVal lintSelection As Integer, ByVal nTransaction As Integer, ByVal lblnSelected As Boolean, ByVal nClaim As Double, ByVal nCase_num As Integer, ByVal nDemand_type As Integer, ByVal nOper_type As Integer) As String
		Dim lrecInsValSI010 As eRemoteDB.Execute
		Dim lclsErrors As New eFunctions.Errors
		Dim lstrError As String
		Dim nlblnSelected As Short
		
		On Error GoTo insValSI010_Err
		
		If lblnSelected = False Then
			nlblnSelected = 0
		Else
			nlblnSelected = 1
		End If
		
		lrecInsValSI010 = New eRemoteDB.Execute
		With lrecInsValSI010
			.StoredProcedure = "insSi010pkg.insvalsi010"
			.Parameters.Add("lintSelection", lintSelection, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTransac", nTransaction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 20, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("lblnSelected", nlblnSelected, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 20, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCase_num", nCase_num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDemand_type", nDemand_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOper_type", nOper_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("Arrayerrors", String.Empty, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 4000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Run(False)
			lstrError = .Parameters("Arrayerrors").Value
			
			If lstrError <> String.Empty Then
				lclsErrors = New eFunctions.Errors
				With lclsErrors
					.ErrorMessage("SI010",  ,  ,  ,  ,  , lstrError)
					insValSI010 = lclsErrors.Confirm
				End With
			End If
			
		End With
		
insValSI010_Err:
        If Err.Number Then
            insValSI010 = ""
            insValSI010 = insValSI010 & " " & Err.Description
        End If
        On Error GoTo 0
		lclsErrors = Nothing
		lrecInsValSI010 = Nothing
	End Function
	
	'% insPostSI010: Ejecuta las actualizaciones sobre la base de datos
	Public Function insPostSI010(ByVal nClaim As Double, ByVal nCase_num As Integer, ByVal nDeman_type As Integer, ByVal nMovement As Integer, ByVal nUserCode As Integer) As Boolean
		Dim lclsClaim As eClaim.Claim = New eClaim.Claim
		Dim lintCount As Integer
		
		On Error GoTo insPostSI010_Err
		
		With lclsClaim
			.nClaim = nClaim
			.nCase_num = nCase_num
			.nDeman_type = nDeman_type
			.nMovement = nMovement
			.nUserCode = nUserCode
			insPostSI010 = .Update_SI010
		End With
		
insPostSI010_Err: 
		If Err.Number Then
			insPostSI010 = False
		End If
		On Error GoTo 0
		lclsClaim = Nothing
	End Function
	
	'%insValAdjust:
	Public Function insValAdjust(ByVal nTransaction As Integer, ByVal nClaim As Double, ByVal nCase_num As Integer, ByVal nDemand_type As Integer) As Boolean
		Dim lrecClaim_his As eRemoteDB.Execute
		
		lrecClaim_his = New eRemoteDB.Execute
		
		insValAdjust = True
		
		With lrecClaim_his
			.StoredProcedure = "ReaOperType_ClaimHis"
			.Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCase_num", nCase_num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDeman_type", nDemand_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTransac", nTransaction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				If .FieldToClass("nCount") > 0 Then
					insValAdjust = False
				End If
			End If
			
		End With
		lrecClaim_his = Nothing
	End Function
	
	'**% insValSI010_k: header validations of the transactions reverse
	'% insValSI010_k: Validaciones del encabezado de reverso de movimientos
	Public Function insValSI010_k(ByVal dEffecdate As Date, ByVal nClaim As Double, ByVal nCase_num As Integer, ByVal nDemand_type As Integer) As String
		Dim lcolClaim_his As eClaim.Claim_hiss
		Dim lclsErrors As eFunctions.Errors
		Dim lclsClaim As eClaim.Claim
		Dim lstrSep As String
        Dim lstrError As String = ""
        On Error GoTo insValSI010_k_Err
		
		lstrSep = "||"
		
		lcolClaim_his = New eClaim.Claim_hiss
		lclsErrors = New eFunctions.Errors
		lclsClaim = New eClaim.Claim
		
		'+ "Fecha" Debe estar llena
		If dEffecdate = eRemoteDB.Constants.dtmNull Then
			lstrError = lstrError & lstrSep & "4015"
		End If
		
		'+ "Siniestro" Debe estar lleno
		If nClaim = eRemoteDB.Constants.intNull Then
			lstrError = lstrError & lstrSep & "4006"
		End If
		
		If lclsErrors.Confirm = String.Empty Then
			'+ "Siniestro" Si este campo está lleno, debe estar registrado en el archivo de siniestros
			If Not lclsClaim.Find(nClaim) Then
				lstrError = lstrError & lstrSep & "4005"
			Else
				'+ "Siniestro" Si este campo está lleno y el siniestro está registrado en el archivo de siniestros,
				'+             el siniestro no puede estar pendiente de información
				If CStr(lclsClaim.sStaclaim) = "6" Then
					lstrError = lstrError & lstrSep & "4305"
				Else
					'**+ It Validates if the claim is CANCELLED OR REJECTED
					'+ Se valida si el siniestro se encuentra ANULADO o RECHAZADO
					If CStr(lclsClaim.sStaclaim) = "1" Or CStr(lclsClaim.sStaclaim) = "7" Then
						lstrError = lstrError & lstrSep & "4099"
					End If
				End If
			End If
		End If
		
		If lclsErrors.Confirm = String.Empty Then
			If Not lcolClaim_his.Find_SI010(nClaim, nCase_num, IIf(nDeman_type = 0, eRemoteDB.Constants.intNull, nDemand_type), dEffecdate) Then
				lstrError = lstrError & lstrSep & "4322"
			End If
		End If
		
		If lstrError <> String.Empty Then
			lstrError = Mid(lstrError, 3)
			With lclsErrors
				.ErrorMessage("SI010",  ,  ,  ,  ,  , lstrError)
				insValSI010_k = .Confirm()
			End With
		End If
		
insValSI010_k_Err: 
		If Err.Number Then
			insValSI010_k = "insValSI010_k: " & Err.Description
		End If
		On Error GoTo 0
		lcolClaim_his = Nothing
		lclsErrors = Nothing
		lclsClaim = Nothing
	End Function
	
	'% insValSI777_K: Valida que los datos introducidos en el encabezado de la transacción
	'%                estén correctos
	Public Function insValSI777_K(ByVal sCodispl As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dInitial_date As Date, ByVal dFinal_date As Date, ByVal nPolicy As Integer, ByVal nAmount_min As Double) As Object
		'                              ByVal sChek_rel As String, _
		''-----------------------------------------------------------------------------------
		Dim lclsError As New eFunctions.Errors
		Dim lclsInsValSi777_k As eRemoteDB.Execute
		Dim lclsValues As New eFunctions.Values
		Dim lstrSep As String
        Dim lstrError As String = ""

        On Error GoTo insValSI777_K_err
		
		lstrSep = "||"
		
		If nBranch <= 0 Then
			lstrError = lstrError & lstrSep & "1022"
		End If
		
		If nProduct <= 0 Then
			lstrError = lstrError & lstrSep & "1014"
		End If
		
		If dFinal_date <> eRemoteDB.Constants.dtmNull Then
			If dInitial_date > dFinal_date Or dInitial_date = eRemoteDB.Constants.dtmNull Then
				lstrError = lstrError & lstrSep & "4158"
			End If
		End If
		
		If nPolicy <= 0 And nPolicy <> eRemoteDB.Constants.intNull Then
			lstrError = lstrError & lstrSep & "100112"
		End If
		
		If nProduct >= 0 Then
			
			lclsInsValSi777_k = New eRemoteDB.Execute
			With lclsInsValSi777_k
				.StoredProcedure = "Reaproduct_li_amount_min"
				.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If .Run(True) Then
					sIndCl_Pay = .FieldToClass("sIndCl_Pay")
					If sIndCl_Pay = "1" Then
						If lclsValues.StringToType(CStr(nAmount_min), eFunctions.Values.eTypeData.etdLong) < 0 Then
							lstrError = lstrError & lstrSep & "100113"
						End If
					End If
				End If
			End With
		End If
		
		
		If lstrError <> String.Empty Then
			lstrError = Mid(lstrError, 3)
			lclsError.ErrorMessage("SI777",  ,  ,  ,  ,  , lstrError)
			insValSI777_K = lclsError.Confirm
		End If
		
insValSI777_K_err: 
		If Err.Number Then
			insValSI777_K = "insValSI777_K: " & Err.Description
		End If
		On Error GoTo 0
		lclsError = Nothing
		lclsInsValSi777_k = Nothing
	End Function
	
	'% Find_SI777: Retorna en un arreglo los registros solicitados desde la transacción SI777  - ACM - 26/06/2002
	Public Function Find_SI777(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Integer, ByVal nStatus_PayOrder As Integer, Optional ByVal dInitial_date As Date = eRemoteDB.Constants.dtmNull, Optional ByVal dFinal_date As Object = eRemoteDB.Constants.dtmNull, Optional ByVal sClient As String = "") As Boolean
		Dim lrecReaCheques_Per_Claim As New eRemoteDB.Execute
		Dim lclsValues As New eFunctions.Values
		Dim lintCounter As Integer
		Dim lintArray As Integer
		Dim lintMaxArray As Integer
		
		On Error GoTo Find_SI777_err
		
		lintCounter = 0
        lintMaxArray = 10000
		
		Me.nClaim = 0
		Me.nRequest_nu = 0
		
		With lrecReaCheques_Per_Claim
			.StoredProcedure = "ReaCheques_Per_Claim"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nStatus_Order", nStatus_PayOrder, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dInitial_date", dInitial_date, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dFinal_date", dFinal_date, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(True) Then
				ReDim arrClaim_PayOrders(lintMaxArray)
				Do While Not .EOF And lintCounter <= lintMaxArray
					
					If nStatus_PayOrder <> 3 Then
                        If Not (Me.nClaim = .FieldToClass("nClaim") And Me.nRequest_nu = .FieldToClass("nRequest_nu")) Then

                            lintCounter = lintCounter + 1
                            arrClaim_PayOrders(lintCounter).nClaim = .FieldToClass("nClaim")
                            arrClaim_PayOrders(lintCounter).dDecladat = .FieldToClass("dDecladat")
                            arrClaim_PayOrders(lintCounter).nOffice = .FieldToClass("nOffice")
                            arrClaim_PayOrders(lintCounter).nPolicy = .FieldToClass("nPolicy")
                            arrClaim_PayOrders(lintCounter).nCertif = .FieldToClass("nCertif")
                            arrClaim_PayOrders(lintCounter).nRequest_nu = .FieldToClass("nRequest_nu")
                            arrClaim_PayOrders(lintCounter).sCheque = .FieldToClass("sCheque")
                            arrClaim_PayOrders(lintCounter).nConsec = .FieldToClass("nConsec")
                            arrClaim_PayOrders(lintCounter).nConcept = .FieldToClass("nConcept")
                            arrClaim_PayOrders(lintCounter).nAmountPay = .FieldToClass("nAmountPay")
                            arrClaim_PayOrders(lintCounter).sClient = .FieldToClass("sClient")
                            arrClaim_PayOrders(lintCounter).sDest_Cheque = .FieldToClass("sDest_Cheque")
                            arrClaim_PayOrders(lintCounter).nSta_cheque = .FieldToClass("nSta_cheque")
                            arrClaim_PayOrders(lintCounter).dOperdate = .FieldToClass("dOperdate")
                            arrClaim_PayOrders(lintCounter).nCurrency = .FieldToClass("nCurrencyPay")
                            arrClaim_PayOrders(lintCounter).nExchange = .FieldToClass("nExchange")
                            arrClaim_PayOrders(lintCounter).nAmountPay_Orig = .FieldToClass("nAmountPay_Orig")
                            arrClaim_PayOrders(lintCounter).nCurrency_Orig = .FieldToClass("nCurrency_Orig")
                            arrClaim_PayOrders(lintCounter).nOffice_Pay = .FieldToClass("nOffice_Pay")
                            arrClaim_PayOrders(lintCounter).nOfficeAgen_Pay = .FieldToClass("nOfficeAgen_Pay")
                            arrClaim_PayOrders(lintCounter).nAgency_Pay = .FieldToClass("nAgency_Pay")

                            Me.nClaim = .FieldToClass("nClaim")
                            Me.nRequest_nu = .FieldToClass("nRequest_nu")

                            If lclsValues.StringToType(.FieldToClass("nTransac"), eFunctions.Values.eTypeData.etdLong) < 0 Then
                                arrClaim_PayOrders(lintCounter).nTransac = 0
                                arrClaim_PayOrders(lintCounter).sMovement = .FieldToClass("sDescript")
                            Else
                                arrClaim_PayOrders(lintCounter).nTransac = .FieldToClass("nTransac")
                                arrClaim_PayOrders(lintCounter).sMovement = CStr(.FieldToClass("nTransac")) & " - " & .FieldToClass("sDescript")
                            End If
                        End If
					Else
						lintCounter = lintCounter + 1
						arrClaim_PayOrders(lintCounter).nClaim = .FieldToClass("nClaim")
						arrClaim_PayOrders(lintCounter).dDecladat = .FieldToClass("dDecladat")
						arrClaim_PayOrders(lintCounter).nOffice = .FieldToClass("nOffice")
						arrClaim_PayOrders(lintCounter).nPolicy = .FieldToClass("nPolicy")
						arrClaim_PayOrders(lintCounter).nCertif = .FieldToClass("nCertif")
						arrClaim_PayOrders(lintCounter).nRequest_nu = .FieldToClass("nRequest_nu")
						arrClaim_PayOrders(lintCounter).sCheque = .FieldToClass("sCheque")
						arrClaim_PayOrders(lintCounter).nConsec = .FieldToClass("nConsec")
						arrClaim_PayOrders(lintCounter).nConcept = .FieldToClass("nConcept")
						arrClaim_PayOrders(lintCounter).nAmountPay = .FieldToClass("nAmountPay")
						arrClaim_PayOrders(lintCounter).sClient = .FieldToClass("sClient")
						arrClaim_PayOrders(lintCounter).sDest_Cheque = .FieldToClass("sDest_Cheque")
						arrClaim_PayOrders(lintCounter).nSta_cheque = .FieldToClass("nSta_cheque")
						arrClaim_PayOrders(lintCounter).dOperdate = .FieldToClass("dOperdate")
						arrClaim_PayOrders(lintCounter).nCurrency = .FieldToClass("nCurrencyPay")
						arrClaim_PayOrders(lintCounter).nExchange = .FieldToClass("nExchange")
						arrClaim_PayOrders(lintCounter).nAmountPay_Orig = .FieldToClass("nAmountPay_Orig")
						arrClaim_PayOrders(lintCounter).nCurrency_Orig = .FieldToClass("nCurrency_Orig")
                        arrClaim_PayOrders(lintCounter).nOffice_Pay = .FieldToClass("nOffice_Pay")
                        arrClaim_PayOrders(lintCounter).nOfficeAgen_Pay = .FieldToClass("nOfficeAgen_Pay")
                        arrClaim_PayOrders(lintCounter).nAgency_Pay = .FieldToClass("nAgency_Pay")
						
						Me.nClaim = .FieldToClass("nClaim")
						Me.nRequest_nu = .FieldToClass("nRequest_nu")
						
						If lclsValues.StringToType(.FieldToClass("nTransac"), eFunctions.Values.eTypeData.etdLong) < 0 Then
							arrClaim_PayOrders(lintCounter).nTransac = 0
							arrClaim_PayOrders(lintCounter).sMovement = .FieldToClass("sDescript")
						Else
							arrClaim_PayOrders(lintCounter).nTransac = .FieldToClass("nTransac")
							arrClaim_PayOrders(lintCounter).sMovement = CStr(.FieldToClass("nTransac")) & " - " & .FieldToClass("sDescript")
						End If
					End If
					.RNext()
				Loop 
				ReDim Preserve arrClaim_PayOrders(lintCounter)
				.RCloseRec()
				Find_SI777 = True
			Else
				Find_SI777 = False
			End If
		End With
		
Find_SI777_err: 
		If Err.Number Then
			Find_SI777 = False
		End If
		On Error GoTo 0
		lrecReaCheques_Per_Claim = Nothing
		lclsValues = Nothing
	End Function
	
	'% CountSI777: Retorna la cantidad de registros almacenados en el arreglo - ACM - 26/06/2002
	Public Function CountSI777() As Integer
		CountSI777 = UBound(arrClaim_PayOrders)
	End Function
	
	'% ItemSI777: Asigna a las propiedades de la clase los valores contenidos en la posición "nIndex"
	'%            del arreglo - ACM - 26/06/2002
	Public Function ItemSI777(ByVal nIndex As Integer) As Boolean
		If nIndex <= UBound(arrClaim_PayOrders) Then
			With arrClaim_PayOrders(nIndex)
				Me.nClaim = .nClaim
				Me.dDecladat = .dDecladat
				Me.nOffice = .nOffice
				Me.nPolicy = .nPolicy
				Me.nCertif = .nCertif
				Me.nRequest_nu = .nRequest_nu
				Me.sCheque = .sCheque
				Me.nConsec = .nConsec
				Me.nConcept = .nConcept
				Me.nAmountPay = .nAmountPay
				Me.sClient = .sClient
				Me.sDest_Cheque = .sDest_Cheque
				Me.nSta_cheque = .nSta_cheque
				Me.sMovement = .sMovement
				Me.nTransac = .nTransac
				Me.dOperdate = .dOperdate
				Me.nCurrency = .nCurrency
				Me.nExchange = .nExchange
				Me.nAmountPay_Orig = .nAmountPay_Orig
				Me.nCurrency_Orig = .nCurrency_Orig
                Me.nOffice_Pay = .nOffice_Pay
                Me.nOfficeAgen_Pay = .nOfficeAgen_Pay
                Me.nAgency_Pay = .nAgency_Pay

			End With
			ItemSI777 = True
		Else
			ItemSI777 = False
		End If
		
	End Function
	
	'% insvalSI777: Se valida que el usuario que autoriza el pago del siniestro esté debidamente autorizado - ACM - 28/06/2002
	Public Function insValSI777(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nCurrency As Integer, ByVal nAmountPay As Double, ByVal sSecurity_Schema As String) As String
		Dim lclsSecurity As New eSecurity.Secur_sche
		
		Dim lclsErrors As New eFunctions.Errors
		
		On Error GoTo insValSI777_err
		
		If lclsSecurity.Find(sSecurity_Schema, True) Then
			If lclsSecurity.valLimits(3, sSecurity_Schema, nBranch, nCurrency, CDec(nAmountPay), nProduct) Then
				If lclsSecurity.ItemLimits(sSecurity_Schema, nCurrency, nBranch, nProduct) Then
                    If lclsSecurity.nClaim_pay >= 0 And nAmountPay > lclsSecurity.nClaim_pay Then
                        Call lclsErrors.ErrorMessage("SI777", 60266)
                    End If
                End If
            End If
        End If

        insValSI777 = lclsErrors.Confirm

insValSI777_err:
        If Err.Number Then
            insValSI777 = "insValSI777: " & Err.Description
        End If
        On Error GoTo 0
        lclsErrors = Nothing
        lclsSecurity = Nothing
	End Function
	
	'% insPostSI777: Se procesa transacción de Control de órdenes para pagos de stros
	Public Function insPostSI777(ByVal nClaimNumber As Double, ByVal nAproval As Integer, ByVal nUserCode As Integer, ByVal nMovement As Integer, ByVal nConsecutive As Integer, ByVal sCheque As String, ByVal nTransac As Integer, ByVal nPolicy As Integer, ByVal sChek_rel As String, ByVal sKey As String) As Boolean
		Dim lrecinsPostSI777 As eRemoteDB.Execute
        Dim nStatus As Integer 
		
		On Error GoTo insPostSI777_Err
		
		lrecinsPostSI777 = New eRemoteDB.Execute
		
        nStatus = 0
		With lrecinsPostSI777
			.StoredProcedure = "InsSi777pkg.insPostSi777"
			.Parameters.Add("nClaimNumber", nClaimNumber, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUserCode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nMovement", nMovement, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nConsecutive", nConsecutive, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCheque", sCheque, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTransac", nTransac, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nStatus", nStatus, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sChek_rel", sChek_rel, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nRequesNu", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sUpdate", "1", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(False) Then
				insPostSI777 = (.Parameters("nStatus").Value = 1)
			Else
				insPostSI777 = False
			End If
		End With
insPostSI777_Err: 
		If Err.Number Then
			insPostSI777 = False
		End If
		lrecinsPostSI777 = Nothing
		On Error GoTo 0
	End Function
	
	'% insPostSI777_Old: Ejecuta las actualizaciones sobre la base de datos - ACM - 28/06/2002
	Public Function insPostSI777_Old(ByVal nClaimNumber As Double, ByVal nAproval As Integer, ByVal nUserCode As Integer, ByVal nMovement As Integer, ByVal nConsecutive As Integer, ByVal sCheque As String, ByVal nTransac As Integer) As Boolean
		Dim lclsClaim As Claim
		Dim lclsClaim_case As Claim_case
		Dim lclsCheque As eCashBank.Cheque
		Dim lclsCl_cover As Cl_Cover
		Dim lstrAproval As String
        Dim sStaclaim As String = ""

        On Error GoTo insPostSI777_Old_err
		
		lclsClaim = New Claim
		lclsClaim_case = New Claim_case
		lclsCheque = New eCashBank.Cheque
		lclsCl_cover = New eClaim.Cl_Cover
		
		'+ Si se trata de órdenes de pago "Por Autorizar" (1) o "Autorizadas" (8)
		If nAproval = 1 Or nAproval = 8 Then
			lstrAproval = "1"
		Else
			'+ Si se trata de órdenes de pago "Sin emisión de orden de pago" (3)
			lstrAproval = "2"
		End If
		
		'+ Se localizan los datos del siniestro en tratamiento
		If lclsClaim.Find(nClaimNumber) Then
			If FindTransac(nClaimNumber, nTransac) Then
				'+ Dependiendo del valor del campo "nPay_type" se actualiza el estado del siniestro
				Select Case nPay_type
					Case 1, 3, 5, 6
						sStaclaim = "4"
						lclsClaim.sStaclaim = CShort("4")
					Case 2, 4, 7
						sStaclaim = "5"
						lclsClaim.sStaclaim = CShort("5")
				End Select
				'+ Se actualiza el registro en Claim
				lclsClaim.sStaclaim = CShort(sStaclaim)
				insPostSI777_Old = lclsClaim.Update()
				lclsClaim_case.nUserCode = nUserCode
				insPostSI777_Old = lclsClaim_case.UpdatesStareserve(nClaimNumber, nDeman_type, nCase_num, sStaclaim)
				If CDbl(sStaclaim) = 5 Then
					insPostSI777_Old = lclsCl_cover.Update_sReservstat_Case(nClaimNumber, nCase_num, nDeman_type, "4", nUserCode)
				End If
			End If
			
			lclsCheque.sCheque = sCheque
			lclsCheque.nRequest_nu = nMovement
			lclsCheque.nConsec = nConsecutive
			lclsCheque.nNullcode = eRemoteDB.Constants.intNull
			lclsCheque.dNulldate = eRemoteDB.Constants.dtmNull
			lclsCheque.nSta_cheque = 8 'Aprobada
			lclsCheque.nUserCode = nUserCode
			
			'+ Se actualizan los registros en la tabla Cheques
			insPostSI777_Old = lclsCheque.UpdChequeStat()
			
		Else
			insPostSI777_Old = False
		End If
		
insPostSI777_Old_err: 
		If Err.Number Then
			insPostSI777_Old = False
		End If
		
		On Error GoTo 0
		
		lclsClaim = Nothing
		lclsClaim_case = Nothing
		lclsCheque = Nothing
		lclsCl_cover = Nothing
	End Function
	
	'%Claim_hisnLoc_Amount: Obtiene la sumatoria de los montos de los movimientos de tipo Pago parcial, pago total y
	'                       anulación de pagos, en moneda local
	Public Function Claim_hisnLoc_Amount(ByVal nClaim As Double, ByVal nCase_num As Integer, ByVal nDeman_type As Integer, ByVal sClient As String, Optional ByVal nCover As Double = 0) As Double
		
		Dim lrecClaim_hisAmount As eRemoteDB.Execute
		
		On Error GoTo Claim_hisnLoc_Amount_Err
		
		lrecClaim_hisAmount = New eRemoteDB.Execute
		
		With lrecClaim_hisAmount
			.StoredProcedure = "ReaClaim_his_nLoc_amount"
			.Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCase_num", nCase_num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDeman_Type", nDeman_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nLoc_amount", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAmount", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				Claim_hisnLoc_Amount = .Parameters("nLoc_Amount").Value
				Me.nAmountPay = .Parameters("nLoc_Amount").Value
				Me.nAmountCov = .Parameters("nAmount").Value
			End If
		End With
		
		lrecClaim_hisAmount = Nothing
		
Claim_hisnLoc_Amount_Err: 
		If Err.Number Then
			Claim_hisnLoc_Amount = 0
		End If
		On Error GoTo 0
	End Function
	
	Public Function inpostreaop(ByVal sClaim As String) As Boolean
		
		Dim lrecClaim_op06 As eRemoteDB.Execute
		
		On Error GoTo lrecClaim_op06_Err
		
		lrecClaim_op06 = New eRemoteDB.Execute
		
		With lrecClaim_op06
			.StoredProcedure = "InPostreaOp"
			.Parameters.Add("sClaim", sClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nConcept", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sConcept", String.Empty, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAmount", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				Me.nAmountPay = .Parameters("nAmount").Value
				Me.sConcept = .Parameters("sConcept").Value
				Me.nConcept = .Parameters("nConcept").Value
			End If
		End With
		
		lrecClaim_op06 = Nothing
		
lrecClaim_op06_Err: 
		If Err.Number Then
			inpostreaop = False
		End If
		On Error GoTo 0
	End Function
	
	Public Function reaCall_InsPostSI777(ByVal sClaimNumber As String, ByVal nAproval As Integer, ByVal nUserCode As Integer, ByVal sMovement As String, ByVal sConsecutive As String, ByVal sCheque As String, ByVal sTransac As String, ByVal nPolicy As Integer, ByVal sChek_rel As String, ByVal sKey As String, ByVal nRequesNu As Integer) As Boolean
		
		'ByVal sClient As String, _
		'
		Dim lrecreaCall_InsPostSI777 As eRemoteDB.Execute
		
		On Error GoTo reaCall_InsPostSI777_Err
		
		lrecreaCall_InsPostSI777 = New eRemoteDB.Execute
		
		With lrecreaCall_InsPostSI777
			.StoredProcedure = "InsSi777pkg.reaCall_InsPostSI777"
            .Parameters.Add("sClaimNumber", sClaimNumber, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 32000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUserCode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sMovement", sMovement, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 32000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sConsecutive", sConsecutive, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 32000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCheque", sCheque, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sTransac", sTransac, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 32000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sChek_rel", sChek_rel, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRequesNu", nRequesNu, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(False) Then
				reaCall_InsPostSI777 = True
			Else
				reaCall_InsPostSI777 = False
			End If
		End With
reaCall_InsPostSI777_Err: 
		If Err.Number Then
			reaCall_InsPostSI777 = False
		End If
		lrecreaCall_InsPostSI777 = Nothing
		On Error GoTo 0
	End Function
End Class






