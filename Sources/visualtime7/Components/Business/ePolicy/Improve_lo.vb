Option Strict Off
Option Explicit On
Public Class Improve_lo
	'%-------------------------------------------------------%'
	'% $Workfile:: Improve_lo.cls                           $%'
	'% $Author:: Nvaplat41                                  $%'
	'% $Date:: 9/10/03 19.01                                $%'
	'% $Revision:: 11                                       $%'
	'%-------------------------------------------------------%'
	
	'**+ Properties according the table in the system on 04/04/2001
	'+ Propiedades según la tabla en el sistema el 04/04/2001
	
	'   Column_name                    Type                  Computed    Length      Prec  Scale Nullable     TrimTrailingBlanks     FixedLenNullInSource
	Public nBranch As Integer 'smallint         no         2           5     0     no              (n/a)                    (n/a)
	Public nPolicy As Double 'int              no         4           10    0     no              (n/a)                    (n/a)
	Public nProduct As Integer 'smallint         no         2           5     0     no              (n/a)                    (n/a)
	Public nCertif As Double 'int              no         4           10    0     no              (n/a)                    (n/a)
	Public nCode As Integer 'smallint         no         2           5     0     no              (n/a)                    (n/a)
	Public nConsec As Integer 'smallint         no         2           5     0     no              (n/a)                    (n/a)
	Public dImprov_dat As Date 'datetime         no         8                       no              (n/a)                    (n/a)
	Public nAmount As Double 'decimal          no         9           12    0     yes             (n/a)                    (n/a)
	Public nUsercode As Integer 'smallint         no         2           5     0     no              (n/a)                    (n/a)
	Public nBordereaux As Integer 'int              no         4           10    0     no              (n/a)                    (n/a)
	
	'**% Add: This function returns TRUE when adds a record in the table "Improve_lo"
	'% Add: Función que retorna VERDADERO al insertar un registro en la tabla 'Improve_lo'
	Public Function Add() As Boolean
		Dim lrecinsCreUpdImprove_loans As eRemoteDB.Execute
		
		On Error GoTo Add_err
		'**+Stored procedure parameters definition 'insudb.insCreUpdImprove_loans'
		'**+Data of 04/04/2001 11:08:04 a.m.
		'+ Definición de parámetros para stored procedure 'insudb.insCreUpdImprove_loans'
		'+ Información leída el 04/04/2001 11:08:04 a.m.
		lrecinsCreUpdImprove_loans = New eRemoteDB.Execute
		With lrecinsCreUpdImprove_loans
			.StoredProcedure = "insCreUpdImprove_loans"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCode", nCode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dImprov_dat", dImprov_dat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAmount", nAmount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBordereaux", nBordereaux, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCommit", 1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Add = .Run(False)
		End With
		
Add_err: 
		If Err.Number Then
			Add = False
		End If
		'UPGRADE_NOTE: Object lrecinsCreUpdImprove_loans may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsCreUpdImprove_loans = Nothing
		On Error GoTo 0
	End Function
	
	'**%InsCalImprove_lo:This routine calculates the sum of the payments of an amount in advance
	'%InsCalImprove_lo:Esta función permite calcular la sumatoria de todos los abonos
	'%de un anticipo.
	Public Function InsCalImprove_lo(ByVal nBranch As Integer, ByVal nPolicy As Double, ByVal nProduct As Integer, ByVal nCertif As Double, ByVal nCode As Integer) As Double
		Dim lrecInsCalImprove_lo As eRemoteDB.Execute
		
		'**+Stored procedure parameters definition 'InsCalImprove_lo'
		'**+Data of 02/28/2000 04:35:05 PM
		'+ Definición de parámetros para stored procedure 'InsCalImprove_lo'
		'+ Información leída el 28/02/2000 04:35:05 PM
		On Error GoTo InsCalImprove_lo_Err
		lrecInsCalImprove_lo = New eRemoteDB.Execute
		With lrecInsCalImprove_lo
			.StoredProcedure = "InsCalImprove_lo"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCode", nCode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nSumAmount", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				InsCalImprove_lo = lrecInsCalImprove_lo.Parameters("nSumAmount").Value
			End If
		End With
		
InsCalImprove_lo_Err: 
		If Err.Number Then
			InsCalImprove_lo = 0
		End If
		'UPGRADE_NOTE: Object lrecInsCalImprove_lo may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecInsCalImprove_lo = Nothing
		On Error GoTo 0
	End Function
	
	'**%insMaxDatemprove_lo: This routine calculates the date of the last payment
	'**%of the amount in advance
	'%insMaxDatemprove_lo: Esta rutina permite calcular la máxima fecha de realización del último pago
	'%realizado al anticipo.
	Public Function insMaxDateImprove_lo(ByVal nBranch As Integer, ByVal nPolicy As Double, ByVal nProduct As Integer, ByVal nCertif As Double, ByVal nCode As Integer) As Date
		Dim lrecreaMaxDateImprove_lo As eRemoteDB.Execute
		
		On Error GoTo insMaxDateImprove_lo_Err
		lrecreaMaxDateImprove_lo = New eRemoteDB.Execute
		insMaxDateImprove_lo = eRemoteDB.Constants.dtmNull
		
		'**+Stored procedure parameters definition 'insudb.reaMaxDateImprove_lo'
		'**+Data of 02/29/2000 09:23:32 AM
		'+ Definición de parámetros para stored procedure 'insudb.reaMaxDateImprove_lo'
		'+ Información leída el 29/02/2000 09:23:32 AM
		With lrecreaMaxDateImprove_lo
			.StoredProcedure = "reaMaxDateImprove_lo"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCode", nCode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dImprov_dat", eRemoteDB.Constants.dtmNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				insMaxDateImprove_lo = .Parameters("dImprov_dat").Value
			End If
		End With
		
insMaxDateImprove_lo_Err: 
		If Err.Number Then
			insMaxDateImprove_lo = eRemoteDB.Constants.dtmNull
		End If
		'UPGRADE_NOTE: Object lrecreaMaxDateImprove_lo may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaMaxDateImprove_lo = Nothing
		On Error GoTo 0
	End Function
	
	'%InsValVI012_K: Validaciones según especificaciones funcionales VI012(Encabezado)
	Public Function InsValVI012_K(ByVal sCodispl As String, ByVal nAction As Integer, Optional ByVal sCertype As String = "", Optional ByVal nBranch As Integer = 0, Optional ByVal nProduct As Integer = 0, Optional ByVal nPolicy As Double = 0, Optional ByVal nCertif As Double = 0, Optional ByVal dEffecdate As Date = #12:00:00 AM#, Optional ByVal sCompanyType As String = "") As String
		Dim lclsErrors As eFunctions.Errors
		Dim lclsProduct As eProduct.Product
        Dim lclsPolicy As ePolicy.Policy = New ePolicy.Policy
        Dim lclsCertificat As ePolicy.Certificat
		Dim lblnError As Boolean
		
		On Error GoTo InsValVI012_K_Err
		
		lclsErrors = New eFunctions.Errors
		
		With lclsErrors
			'+Se valida el campo Ramo.
			If nBranch = eRemoteDB.Constants.intNull Then
				.ErrorMessage(sCodispl, 1022)
				lblnError = True
			End If
			
			'+ Se valida el campo Producto
			If nProduct = eRemoteDB.Constants.intNull Then
				.ErrorMessage(sCodispl, 1014)
				lblnError = True
			Else
				lclsProduct = New eProduct.Product
				'+ Se valida que el producto corresponda a vida o combinado
				lclsProduct.FindProdMaster(nBranch, nProduct)
				If CStr(lclsProduct.sBrancht) <> "1" And CStr(lclsProduct.sBrancht) <> "5" Then
					.ErrorMessage(sCodispl, 3987)
				End If
			End If
			
			'+Se valida que el campo póliza
			If nPolicy = eRemoteDB.Constants.intNull Then
				.ErrorMessage(sCodispl, 3003)
				lblnError = True
			Else
				'+ Se valida que sea una póliza válida
				If Not lblnError Then
					lclsPolicy = New ePolicy.Policy
					If Not lclsPolicy.FindPolicyOfficeName(sCertype, nBranch, nProduct, nPolicy, sCompanyType) Then
						.ErrorMessage(sCodispl, 3001)
						lblnError = True
					End If
				End If
			End If
			
			'+Se valida el campo certificado
			If Not lblnError Then
				If nCertif = eRemoteDB.Constants.intNull Then
					If lclsPolicy.sPolitype <> "1" Then
						.ErrorMessage(sCodispl, 3006)
					End If
				Else
					lclsCertificat = New ePolicy.Certificat
					If Not lclsCertificat.Find(sCertype, nBranch, nProduct, nPolicy, nCertif) Then
						.ErrorMessage(sCodispl, 3010)
					End If
				End If
			End If
			
			InsValVI012_K = .Confirm
		End With
		
InsValVI012_K_Err: 
		If Err.Number Then
			InsValVI012_K = "InsValVI012_K: " & Err.Description
		End If
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		'UPGRADE_NOTE: Object lclsProduct may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsProduct = Nothing
		'UPGRADE_NOTE: Object lclsPolicy may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsPolicy = Nothing
		'UPGRADE_NOTE: Object lclsCertificat may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsCertificat = Nothing
		On Error GoTo 0
	End Function
	
	'%InsValVI012Upd: Validaciones según especificaciones funcionales VI012(Folder)
	Public Function InsValVI012Upd(ByVal sCodispl As String, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nImprov_Amount As Double, ByVal dPay_Date As Date, ByVal nCode As Integer, ByVal nSald_init As Double) As String
		Dim lclsErrors As eFunctions.Errors
		Dim lclsImprove_lo As Improve_lo
		
		On Error GoTo InsValVI012Upd_Err
		lclsErrors = New eFunctions.Errors
		With lclsErrors
			If nImprov_Amount <> eRemoteDB.Constants.intNull Then
				'+Validaciòn del campo Fecha de pago, debe estar lleno.
				If dPay_Date = eRemoteDB.Constants.dtmNull Then
					.ErrorMessage(sCodispl, 3587)
				End If
				
				'+Valida si que el monto no sea mayor al saldo inicial
				If nImprov_Amount > nSald_init Then
					.ErrorMessage(sCodispl, 3669)
				End If
			Else
				'+Validaciòn del campo abono, debe estar lleno.
				If dPay_Date <> eRemoteDB.Constants.dtmNull Then
					.ErrorMessage(sCodispl, 3418)
				End If
			End If
			
			'+Validaciòn del campo Fecha de pago.
			If dPay_Date <> eRemoteDB.Constants.dtmNull Then
				lclsImprove_lo = New Improve_lo
				If dPay_Date < lclsImprove_lo.insMaxDateImprove_lo(nBranch, nPolicy, nProduct, nCertif, nCode) Then
					.ErrorMessage(sCodispl, 3417)
				End If
			End If
			
			'+Validación del campo Abono.
			InsValVI012Upd = .Confirm
		End With
		
InsValVI012Upd_Err: 
		If Err.Number Then
			InsValVI012Upd = "InsValVI012Upd: " & Err.Description
		End If
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		'UPGRADE_NOTE: Object lclsImprove_lo may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsImprove_lo = Nothing
		On Error GoTo 0
	End Function
	
	'%InsPostVI012: Se realiza la actualización de los datos en la ventana VI012 (Folder)
	Public Function InsPostVI012Upd(ByVal sCodispl As String, ByVal nAction As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nCode As Integer, ByVal nAmount As Double, ByVal dImprov_dat As Date, ByVal nUsercode As Integer) As Boolean
		''--------------------------------------------------------------------------------------------
		Dim lclsImprove_lo As ePolicy.Improve_lo
		lclsImprove_lo = New ePolicy.Improve_lo
		
		On Error GoTo InsPostVI012Upd_Err
		
		InsPostVI012Upd = True
		
		With lclsImprove_lo
			.nBranch = nBranch
			.nProduct = nProduct
			.nPolicy = nPolicy
			.nCertif = nCertif
			.nAmount = nAmount
			.nCode = nCode
			.nUsercode = nUsercode
			.dImprov_dat = dImprov_dat
			If nAction = eFunctions.Menues.TypeActions.clngActionInput Then
				InsPostVI012Upd = .Add
			End If
		End With
		
InsPostVI012Upd_Err: 
		If Err.Number Then
			InsPostVI012Upd = False
		End If
		'UPGRADE_NOTE: Object lclsImprove_lo may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsImprove_lo = Nothing
	End Function
	
	'%Class_Initialize: Se encarga de inicializar los valores de la clase.
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		nPolicy = eRemoteDB.Constants.intNull
		nProduct = eRemoteDB.Constants.intNull
		nCertif = eRemoteDB.Constants.intNull
		nCode = eRemoteDB.Constants.intNull
		nConsec = eRemoteDB.Constants.intNull
		dImprov_dat = eRemoteDB.Constants.dtmNull
		nAmount = eRemoteDB.Constants.intNull
		nUsercode = eRemoteDB.Constants.intNull
		nBordereaux = eRemoteDB.Constants.intNull
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
End Class






