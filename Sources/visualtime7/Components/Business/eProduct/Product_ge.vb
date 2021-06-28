Option Strict Off
Option Explicit On
Public Class Product_ge
	'%-------------------------------------------------------%'
	'% $Workfile:: Product_ge.cls                           $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 12:36p                               $%'
	'% $Revision:: 16                                       $%'
	'%-------------------------------------------------------%'
	
	'+ Propiedades según la tabla en el sistema al 02/01/2001.
	'+ Los campos llave de la tabla corresponden a: nBranch, nProduct y dEffecdate.
	
	
	'   Column_name                   Type       Computed  Length      Prec  Scale Nullable TrimTrailingBlanks  FixedLenNullInSource
	Public nBranch As Integer 'smallint      no       2           5     0     no          (n/a)                (n/a)
	Public nProduct As Integer 'smallint      no       2           5     0     no          (n/a)                (n/a)
	Public dEffecdate As Date 'datetime      no       8                       no          (n/a)                (n/a)
	Public nCurrency As Integer 'smallint      no       2           5     0     yes         (n/a)                (n/a)
	Public sFrancApl As String 'char          no       1                       yes         no                   yes
	Public nFrancMax As Double 'decimal       no       9           10    0     yes         (n/a)                (n/a)
	Public nFrancMin As Double 'decimal       no       9           10    0     yes         (n/a)                (n/a)
	Public nFrancrat As Double 'decimal       no       5           4     2     yes         (n/a)                (n/a)
	Public sFrantype As String 'char          no       1                       yes         no                   yes
	Public nLevelPay As Integer 'smallint      no       2           5     0     yes         (n/a)                (n/a)
	Public dNulldate As Date 'datetime      no       8                       yes         (n/a)                (n/a)
	Public sPayconre As String 'char          no       1                       yes         no                   yes
	Public nPre_amend As Double 'decimal       no       9           10    2     yes         (n/a)                (n/a)
	Public nPre_issue As Double 'decimal       no       9           10    2     yes         (n/a)                (n/a)
	Public sResemedi As String 'char          no       1                       yes         no                   yes
	Public sResmaypa As String 'char          no       1                       yes         no                   yes
	Public sSuspendi As String 'char          no       1                       yes         no                   yes
	Public nUsercode As Integer 'smallint      no       2           5     0     yes         (n/a)                (n/a)
	Public nFrancFix As Double 'decimal       no       9           10    0     yes         (n/a)                (n/a)
    Public nComerLine As Integer
    Public nDuplicatedType As Integer
	
	'% Find: Devuelve información de un registro de la tabla Product_ge
	Public Function Find(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, Optional ByVal lblnFind As Boolean = False) As Boolean
		
		Static lblnRead As Boolean
		
		'- Se define la variable lrecreaProduct_ge
		
		Dim lrecreaProduct_ge As eRemoteDB.Execute
		lrecreaProduct_ge = New eRemoteDB.Execute
		
		On Error GoTo Find_Err
		
		If Me.nBranch <> nBranch Or Me.nProduct <> nProduct Or Me.dEffecdate <> dEffecdate Or lblnFind Then
			
			Me.nBranch = nBranch
			Me.nProduct = nProduct
			Me.dEffecdate = dEffecdate
			
			'+ Definición de parámetros para stored procedure 'insudb.reaProduct_ge'
			'+ Información leída el 02/01/2001 13:51:45
			
			With lrecreaProduct_ge
				.StoredProcedure = "reaProduct_ge"
				.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("deffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				
				If .Run Then
					nBranch = .FieldToClass("nBranch")
					nProduct = .FieldToClass("nProduct")
					dEffecdate = .FieldToClass("dEffecdate")
					nCurrency = .FieldToClass("nCurrency")
					sFrancApl = .FieldToClass("sFrancApl")
					nFrancMax = .FieldToClass("nFrancmax")
					nFrancMin = .FieldToClass("nFrancmin")
					nFrancrat = .FieldToClass("nFrancrat")
					sFrantype = .FieldToClass("sFrantype")
					nLevelPay = .FieldToClass("nLevelPay")
					dNulldate = .FieldToClass("dNulldate")
					sPayconre = IIf(.FieldToClass("nPayconre") = eRemoteDB.Constants.intNull, String.Empty, CStr(.FieldToClass("nPayconre")))
					nPre_amend = .FieldToClass("nPre_amend")
					nPre_issue = .FieldToClass("nPre_issue")
					sResemedi = .FieldToClass("sResemedi")
					sResmaypa = .FieldToClass("sResmaypa")
					sSuspendi = .FieldToClass("sSuspendi")
					nUsercode = .FieldToClass("nUsercode")
					nFrancFix = .FieldToClass("nFrancFix")
                    nComerLine = .FieldToClass("nComerLine", eRemoteDB.Constants.intNull)
                    nDuplicatedType = .FieldToClass("nDuplicatedType")
					.RCloseRec()
					lblnRead = True
				Else
					lblnRead = False
				End If
			End With
		End If
		
		Find = lblnRead
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecreaProduct_ge may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaProduct_ge = Nothing
	End Function
	
	'% Update: Actualiza en la tabla Product_ge los valores introducidos
	Public Function Update() As Boolean
		
		'- Se define la variable lrecinsProduct_ge
		
		Dim lrecinsProduct_ge As eRemoteDB.Execute
		
		On Error GoTo Update_Err
		
		lrecinsProduct_ge = New eRemoteDB.Execute
		
		'+ Definición de parámetros para stored procedure 'insudb.insProduct_ge'
		'+ Información leída el 23/03/2001 01:52:02 p.m.
		
		With lrecinsProduct_ge
			.StoredProcedure = "insProduct_ge"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sFrancapl", sFrancApl, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nFrancmax", nFrancMax, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nFrancmin", nFrancMin, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nFrancrat", nFrancrat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sFrantype", sFrantype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nlevelpay", nLevelPay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If Trim(sPayconre) = String.Empty Then
				'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
				.Parameters.Add("nPayconre", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Else
				.Parameters.Add("nPayconre", CShort(sPayconre), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			End If
			.Parameters.Add("nPre_amend", nPre_amend, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPre_issue", nPre_issue, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sResemedi", sResemedi, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sResmaypa", sResmaypa, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sSuspendi", sSuspendi, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nFrancFix", nFrancFix, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nComerLine", nComerLine, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nDuplicatedType", nDuplicatedType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            Update = .Run(False)
		End With
		'UPGRADE_NOTE: Object lrecinsProduct_ge may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsProduct_ge = Nothing
		
Update_Err: 
		If Err.Number Then
			Update = False
		End If
		On Error GoTo 0
	End Function
	
	
	'%insValDP031: Esta rutina tiene la finalidad de validar masivamente todos los campos
	'%de la forma
	Public Function insValDP031(ByVal sCodispl As String, ByVal nResmaypa As Integer, ByVal nLevelPay As Integer, ByVal nDamage As Integer, ByVal nPayconre As Integer, ByVal nClaim_pres As Integer, ByVal nCurrency As Integer, ByVal nNoAplied As Integer, ByVal nPreissue As Double, ByVal nFrancrat As Double, ByVal nPreamend As Double, ByVal nFix As Double, ByVal nCapApl As Integer, ByVal nFrancMin As Double, ByVal nFrancMax As Double, ByVal nSuspended As Integer, ByVal nClaim_Notice As Integer, ByVal nClaim_Pay As Integer) As String
		Dim lobjErrors As eFunctions.Errors
		
		On Error GoTo insValDP031_err
		
		lobjErrors = New eFunctions.Errors
		
		insValDP031 = String.Empty
		
		If nPayconre = 0 Then
			lobjErrors.ErrorMessage(sCodispl, 11306)
		End If
		
		'+ Validación de dias de denuncio
		
		If nClaim_Notice = eRemoteDB.Constants.intNull Then
			Call lobjErrors.ErrorMessage("DP031", 55781)
		End If
		
		If nClaim_Notice <= 0 Then
			Call lobjErrors.ErrorMessage("DP031", 55726)
		End If
		
		'+ Validación de dias de plazo para liquidar
		If nClaim_Pay = eRemoteDB.Constants.intNull Then
			Call lobjErrors.ErrorMessage("DP031", 55782)
		End If
		
		If nClaim_Pay <= 0 Then
			Call lobjErrors.ErrorMessage("DP031", 55844)
		End If
		
		If nCurrency = 0 And ((nPreissue <> 0 And nPreissue <> eRemoteDB.Constants.intNull) Or (nPreamend <> 0 And nPreamend <> eRemoteDB.Constants.intNull) Or (nFix <> 0 And nFix <> eRemoteDB.Constants.intNull) Or (nFrancMin <> 0 And nFrancMin <> eRemoteDB.Constants.intNull) Or (nFrancMax <> 0 And nFrancMax <> eRemoteDB.Constants.intNull)) Then
			'+ Se debe indicar la moneda, si los campos de Franquicia/Deducible
			'+ y Prima mínima para la póliza tienen valor
			lobjErrors.ErrorMessage(sCodispl, 1351)
		End If
		
        '+ Se evalúan los campos de Franquicia/Deducible SOLO si aplica
        If nNoAplied = 2 Or nNoAplied = 3 Then
            If (nFrancMin <> 0 And nFrancMin <> eRemoteDB.Constants.intNull) And (nFrancrat = 0 Or nFrancrat = eRemoteDB.Constants.intNull) Then
                lobjErrors.ErrorMessage(sCodispl, 11077)
            End If

            If (nFrancMax <> 0 And nFrancMax <> eRemoteDB.Constants.intNull) And (nFrancrat = 0 Or nFrancrat = eRemoteDB.Constants.intNull) Then
                lobjErrors.ErrorMessage(sCodispl, 11358)
            End If

            If nFix <> 0 And nFix <> eRemoteDB.Constants.intNull And nFrancrat <> 0 And nFrancrat <> eRemoteDB.Constants.intNull Then
                lobjErrors.ErrorMessage(sCodispl, 11075)
            End If

            If (nFix <> 0 And nFrancMin <> 0 And nFrancMax <> 0) And (nFix <> eRemoteDB.Constants.intNull And nFrancMin <> eRemoteDB.Constants.intNull And nFrancMax <> eRemoteDB.Constants.intNull) Then
                lobjErrors.ErrorMessage(sCodispl, 11354)
            End If

            '+ Se valida que la franquicia mínima sea menos que la franquicia máxima, en caso de que el Monto Fijo no haya sido indicado y el procentaje sí.
            If (nFix = 0 Or nFix = eRemoteDB.Constants.intNull) And (nFrancrat <> 0 And nFrancrat <> eRemoteDB.Constants.intNull) And nFrancMin <> 0 And nFrancMin <> eRemoteDB.Constants.intNull And nFrancMax <= nFrancMin Then
                lobjErrors.ErrorMessage(sCodispl, 11048)
            End If

            If (nFrancrat = 0 Or nFrancrat = eRemoteDB.Constants.intNull) And (nFix = 0 Or nFix = eRemoteDB.Constants.intNull) Then
                lobjErrors.ErrorMessage(sCodispl, 11316)
            End If
        End If

        insValDP031 = lobjErrors.Confirm

        'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lobjErrors = Nothing

insValDP031_err:
        If Err.Number Then
            insValDP031 = "insValDP031: " & Err.Description
        End If
        On Error GoTo 0
    End Function
	
	'% insPostDP031: Esta rutina tiene la finalidad de actualizar en la clase los valores con los
	'% cuales se hará la actualización en las tablas respectivas
    Public Function insPostDP031(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, ByVal sResmaypa As String, ByVal nLevelPay As Integer, ByVal sDamage As String, ByVal sPayconre As String, ByVal nClaim_pres As Integer, ByVal nCurrency As Integer, ByVal sNoAplied As String, ByVal nPreissue As Double, ByVal nFrancrat As Double, ByVal nPreamend As Double, ByVal nFix As Double, ByVal sCapApl As String, ByVal nFrancMin As Double, ByVal nFrancMax As Double, ByVal sSuspended As String, ByVal nComerLine As Integer, ByVal nUsercode As Integer, ByVal nClaim_Notice As Integer, ByVal nClaim_Pay As Integer, ByVal nDuplicatedType As Integer) As Boolean
        Dim lclsProd_win As eProduct.Prod_win
        Dim lclsProduct As eProduct.Product

        On Error GoTo insPostDP031_err

        lclsProduct = New eProduct.Product
        lclsProd_win = New eProduct.Prod_win

        insPostDP031 = True

        With Me
            .sResmaypa = sResmaypa
            .nLevelPay = nLevelPay
            .nBranch = nBranch
            .nProduct = nProduct
            .dEffecdate = dEffecdate
            .nCurrency = nCurrency
            .sPayconre = sPayconre
            .nPre_amend = nPreamend
            .nPre_issue = nPreissue
            .sSuspendi = IIf(sSuspended = "1", sSuspended, "2")
            .nUsercode = nUsercode
            .nFrancFix = nFix
            .nFrancrat = nFrancrat
            .nFrancMax = nFrancMax
            .nFrancMin = nFrancMin
            .sFrancApl = sCapApl
            .sResemedi = sDamage
            .sFrantype = sNoAplied
            .nBranch = nBranch
            .nProduct = nProduct
            .dEffecdate = dEffecdate
            .nComerLine = nComerLine
            .nDuplicatedType = nDuplicatedType



            If .Update Then
                If lclsProduct.Find(nBranch, nProduct, dEffecdate) Then
                    lclsProduct.nClaim_pres = nClaim_pres
                    lclsProduct.nClaim_Notice = nClaim_Notice
                    lclsProduct.nClaim_Pay = nClaim_Pay
                    insPostDP031 = lclsProduct.UpdateProduct
                    If insPostDP031 Then
                        '+ Se actualiza la secuencia de ventana del producto con la transacción enviada como parámetro
                        lclsProd_win.Add_Prod_win(.nBranch, .nProduct, .dEffecdate, "DP031", "2", .nUsercode)
                    End If
                End If
            End If
        End With

        'UPGRADE_NOTE: Object lclsProd_win may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsProd_win = Nothing
        'UPGRADE_NOTE: Object lclsProduct may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsProduct = Nothing

insPostDP031_err:
        If Err.Number Then
            insPostDP031 = False
        End If
        On Error GoTo 0

    End Function
	
	'%insLoadProductDP031: Esta rutina tiene la finalidad de devolver el valor del campo nClaim_pres
	'%la tabla Product necesario en la transacción DP031
	Public Function insReaDP031(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date) As Double
		Dim lrecProduct As eRemoteDB.Execute
		
		On Error GoTo insReaDP031_Err
		lrecProduct = New eRemoteDB.Execute
		insReaDP031 = eRemoteDB.Constants.intNull
		
		With lrecProduct
			.StoredProcedure = "reaProduct"
			.Parameters.Add("nBranch", nBranch)
			.Parameters.Add("nProduct", nProduct)
			.Parameters.Add("deffecdate", dEffecdate)
			If .Run Then
				insReaDP031 = lrecProduct.FieldToClass("nClaim_pres")
				insReaDP031 = lrecProduct.FieldToClass("nClaim_pres")
				insReaDP031 = lrecProduct.FieldToClass("nClaim_noticy")
				insReaDP031 = lrecProduct.FieldToClass("nClaim_pay")
			End If
		End With
		
insReaDP031_Err: 
		If Err.Number Then
			insReaDP031 = eRemoteDB.Constants.intNull
		End If
		'UPGRADE_NOTE: Object lrecProduct may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecProduct = Nothing
	End Function
	
	'% DefaultValueDO031: Esta función se encarga de realizar la habilitación o des-habilitación de los
	'%                    campos de la ventana DP031.
	Public Function DefaultValueDP031(ByRef sField As Object) As String
        Dim lstrReturnValue As String = ""
        Select Case sField
			'+ Suspensión de garantías
			Case "Suspendi"
				lstrReturnValue = IIf(sSuspendi = "1", "1", "2")
				
				'+  Métodos de apertura
			Case "optDamage"
				lstrReturnValue = IIf(sResemedi = "1" Or sResemedi = String.Empty, "1", "2")
				
			Case "optCost"
				lstrReturnValue = IIf(sResemedi = "2", "1", "2")
				
				'+ Tipo de Franquicia/Deducible
			Case "optNoAplied_Not"
				lstrReturnValue = IIf(sFrancApl = "1" Or sFrancApl = String.Empty, "1", "2")
				
			Case "optNoAplied_F"
				lstrReturnValue = IIf(sFrancApl = "2", "1", "2")
				
			Case "optNoAplied_D"
				lstrReturnValue = IIf(sFrancApl = "3", "1", "2")
				
				'+ Franquicia/Deducible: Aplica sobre
			Case "optCapApl_Cap"
				lstrReturnValue = IIf(sFrantype = "2", "1", "2")
				
			Case "optCapApl_Cla"
				lstrReturnValue = IIf(sFrantype = "3", "1", "2")
				
			Case "optCapApl_Not"
				lstrReturnValue = IIf(sFrantype = "1" Or sFrantype = String.Empty, "1", "2")
		End Select
		DefaultValueDP031 = lstrReturnValue
		
	End Function
	
	'%Class_Initialize: Se ejecuta al instanciar la clase
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		nBranch = eRemoteDB.Constants.intNull
		nProduct = eRemoteDB.Constants.intNull
		dEffecdate = eRemoteDB.Constants.dtmNull
		nCurrency = eRemoteDB.Constants.intNull
		sFrancApl = String.Empty
		nFrancMax = eRemoteDB.Constants.intNull
		nFrancMin = eRemoteDB.Constants.intNull
		nFrancrat = eRemoteDB.Constants.intNull
		sFrantype = String.Empty
		nLevelPay = eRemoteDB.Constants.intNull
		dNulldate = eRemoteDB.Constants.dtmNull
		sPayconre = String.Empty
		nPre_amend = eRemoteDB.Constants.intNull
		nPre_issue = eRemoteDB.Constants.intNull
		sResemedi = String.Empty
		sResmaypa = String.Empty
		sSuspendi = String.Empty
		nUsercode = eRemoteDB.Constants.intNull
		nFrancFix = eRemoteDB.Constants.intNull
		nComerLine = eRemoteDB.Constants.intNull
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
End Class






