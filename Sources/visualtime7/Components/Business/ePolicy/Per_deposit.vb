Option Strict Off
Option Explicit On
Public Class Per_deposit
	'%-------------------------------------------------------%'
	'% $Workfile:: Per_deposit.cls                          $%'
	'% $Author:: Clobos                                     $%'
	'% $Date:: 10-05-06 13:56                               $%'
	'% $Revision:: 5                                        $%'
	'%-------------------------------------------------------%'
	
	'*-Propiedades según la tabla en el sistema el 27/12/2000
	'Column_Name                   Type          Length  Prec    Scale   Nullable
	'-------------------------   --------------- ------ -------- ------- ---------
	Public sCertype As String ' CHAR       1    0     0    N
	Public nBranch As Integer ' NUMBER     22   0     5    N
	Public nProduct As Integer ' NUMBER     22   0     5    N
	Public nPolicy As Double ' NUMBER     22   0     10   N
	Public nCertif As Double ' NUMBER     22   0     10   N
	Public nYear_ini As Integer ' NUMBER     22   0     5    N
	Public dEffecdate As Date ' DATE       7    0     0    N
	Public nYear_end As Integer ' NUMBER     22   0     5    N
	Public nAmountdep As Double ' NUMBER     22   2     10   N
	Public nAmountdep_aux As Double ' NUMBER     22   2     10   N
	Public dNulldate As Date ' DATE       7    0     0    S
	Private mlngUsercode As Integer ' NUMBER     22   0     5    N
	Public nBasicPrem As Double ' NUMBER     22   2     10   N
	Public nSavingPrem As Double ' NUMBER     22   2     10   N
	Public nRecamount As Double ' NUMBER     22   2     10   N
	Public nPayfreq As Integer
	Public nExtPrem As Double
	Public nSurrender As Double
	
	
	'-Variables auxiliares
	'-Variable que guarda la acción póliza que se esta ejecutando
	Private mlngTransaction As Integer
	
	'%InsUpdPer_deposit: Realiza la actualización de la tabla
	Private Function InsUpdPer_deposit(ByVal nAction As Integer) As Boolean
		Dim lrecInsUpdPer_deposit As eRemoteDB.Execute
		
		On Error GoTo InsUpdPer_deposit_Err
		'+ Definición de Stored Procedure InsUpdPer_deposit al 04-03-2002
		lrecInsUpdPer_deposit = New eRemoteDB.Execute
		With lrecInsUpdPer_deposit
			.StoredProcedure = "InsUpdPer_deposit"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nYear_ini", nYear_ini, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nYear_end", nYear_end, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAmountdep", nAmountdep, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dNulldate", dNulldate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", mlngUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 1, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTransaction", mlngTransaction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			InsUpdPer_deposit = .Run(False)
		End With
		
InsUpdPer_deposit_Err: 
		If Err.Number Then
			InsUpdPer_deposit = False
		End If
		'UPGRADE_NOTE: Object lrecInsUpdPer_deposit may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecInsUpdPer_deposit = Nothing
		On Error GoTo 0
	End Function
	
	'%Add: Crea un registro en la tabla
	Public Function Add() As Boolean
		Add = InsUpdPer_deposit(1)
	End Function
	
	'%Update: Actualiza un registro en la tabla
	Public Function Update() As Boolean
		Update = InsUpdPer_deposit(2)
	End Function
	
	'%Delete: Borra los datos de la tabla
	Public Function Delete() As Boolean
		Delete = InsUpdPer_deposit(3)
	End Function
	
	'% Count: Retorna la contidad de registros asociados a la poliza.
	Public Function Count(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date) As Integer
		Dim reaPer_deposit_count As eRemoteDB.Execute
		
		On Error GoTo Count_Err
		'+Definición de parámetros para stored procedure 'insudb.reaActivelife'
		reaPer_deposit_count = New eRemoteDB.Execute
		With reaPer_deposit_count
			.StoredProcedure = "reaPer_deposit_count"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCount", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 12, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				Count = .Parameters("nCount").Value
			End If
		End With
		
Count_Err: 
		If Err.Number Then
			Count = -1
		End If
		'UPGRADE_NOTE: Object reaPer_deposit_count may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		reaPer_deposit_count = Nothing
		On Error GoTo 0
	End Function
	
	
	
	'%InsValVA595Upd: Validaciones de la transacción
	Public Function InsValVA595Upd(ByVal sCodispl As String, ByVal sAction As String, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nYear_ini As Integer, ByVal nYear_end As Integer, ByVal nAmountdep As Double, Optional ByVal nPremDeal As Double = 0, Optional ByVal nTransactio As Short = 0) As String
        Dim lstrErrorAll As String = String.Empty
		Dim lclsErrors As eFunctions.Errors
		Dim lrecinsvalVA595 As eRemoteDB.Execute
		
		On Error GoTo insvalVA595Upd_Err
		
		lrecinsvalVA595 = New eRemoteDB.Execute
		
		'+ Se invoca el SP para validar los campos de la transacción
		With lrecinsvalVA595
			.StoredProcedure = "insVA595PKG.insvalVA595Upd"
			.Parameters.Add("sAction", sAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nYear_ini", nYear_ini, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nYear_end", nYear_end, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAmountdep", nAmountdep, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sArrayerrors", " ", eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 4000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPremDeal", nPremDeal, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCodispl", sCodispl, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTransactio", nTransactio, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(False) Then
				lstrErrorAll = .Parameters("sArrayerrors").Value
			End If
		End With
		
		lclsErrors = New eFunctions.Errors
		
		With lclsErrors
			If Len(lstrErrorAll) > 0 Then
				Call .ErrorMessage(sCodispl,  ,  ,  ,  ,  , lstrErrorAll)
			End If
			InsValVA595Upd = .Confirm
		End With
		
insvalVA595Upd_Err: 
		If Err.Number Then
			InsValVA595Upd = "insvalVA595Upd: " & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		'UPGRADE_NOTE: Object lrecinsvalVA595 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsvalVA595 = Nothing
	End Function
	'%InsValRange: Valida que el rango indicado no este dentro de otro rango
	Public Function InsValRange(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nYear_ini As Integer, ByVal nYear_end As Integer) As Boolean
		Dim lrecInsValRange As eRemoteDB.Execute
		
		On Error GoTo InsValRange_Err
		'+ Definición de store procedure InsValRangeinper_deposit al 04-03-2002 13:07:16
		lrecInsValRange = New eRemoteDB.Execute
		With lrecInsValRange
			.StoredProcedure = "InsValRangeinPer_deposit"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nYear_ini", nYear_ini, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nYear_end", nYear_end, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCount", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				InsValRange = .Parameters("nCount").Value = 0
			End If
		End With
		
InsValRange_Err: 
		If Err.Number Then
			InsValRange = False
		End If
		'UPGRADE_NOTE: Object lrecInsValRange may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecInsValRange = Nothing
		On Error GoTo 0
	End Function
	
	'%InsValVA669Upd: Validaciones de la transacción
	Public Function InsValVA669Upd(ByVal sCodispl As String, ByVal sAction As String, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nYear_ini As Integer, ByVal nYear_end As Integer, ByVal nAmountdep As Double, Optional ByVal nPolYears As Integer = eRemoteDB.Constants.intNull, Optional ByVal nIllustype As Tmp_val669s.eIllustType = 0) As String
		Dim lclsErrors As eFunctions.Errors
		Dim lclsActivelife As Activelife
		Dim lclsGeneral As eGeneral.GeneralFunction
		Dim lintMonth As Integer
		Dim lblnError As Boolean
		
		On Error GoTo InsValVA669Upd_Err
		lclsErrors = New eFunctions.Errors
		
		With lclsErrors
			'+Se valida el año inicial
			If nYear_ini = eRemoteDB.Constants.intNull Then
				.ErrorMessage(sCodispl, 60283)
				lblnError = True
			Else
				
				'+ Se valida que año sea superior a la cantidad de años de la poliza
				'+ Si no viene por parametros se calcula
				If nPolYears = eRemoteDB.Constants.intNull Then
					lclsActivelife = New Activelife
					lclsGeneral = New eGeneral.GeneralFunction
					lclsActivelife.Find(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate)
					
					lclsGeneral.getYearMonthDiff(lclsActivelife.dStartdate, dEffecdate, nPolYears, lintMonth)
					'UPGRADE_NOTE: Object lclsGeneral may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					lclsGeneral = Nothing
					'UPGRADE_NOTE: Object lclsActivelife may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					lclsActivelife = Nothing
				End If
				If nYear_ini < nPolYears Then
					.ErrorMessage(sCodispl, 60010,  , eFunctions.Errors.TextAlign.RigthAling, "(" & nPolYears & ")")
				End If
			End If
			
			'+Se valida el año final
			If nYear_end = eRemoteDB.Constants.intNull Then
				.ErrorMessage(sCodispl, 60284)
				lblnError = True
			ElseIf nYear_end <= nYear_ini Then 
				.ErrorMessage(sCodispl, 60285)
				lblnError = True
			End If
			
			'+Se valida que no se repita el rango
			If Not lblnError Then
				If Not InsValRange(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, CShort(nYear_ini), CShort(nYear_end)) Then
					.ErrorMessage(sCodispl, 60286)
				End If
			End If
			
			'+Se valida el monto de prima anual a pagar
			If nAmountdep = eRemoteDB.Constants.intNull Then
				.ErrorMessage(sCodispl, 60287)
			End If
			
			InsValVA669Upd = .Confirm
		End With
		
InsValVA669Upd_Err: 
		If Err.Number Then
			InsValVA669Upd = "InsValVA669Upd: " & Err.Description
		End If
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		'UPGRADE_NOTE: Object lclsActivelife may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsActivelife = Nothing
		'UPGRADE_NOTE: Object lclsGeneral may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsGeneral = Nothing
		On Error GoTo 0
	End Function
	
	'%InsPostVA595Upd: Ejecuta el post de la transacción Planes de pago(VA595)
	Public Function InsPostVA595Upd(ByVal sAction As String, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nYear_ini As Integer, ByVal dEffecdate As Date, ByVal nYear_end As Integer, ByVal nAmountdep As Double, ByVal dNulldate As Date, ByVal nUsercode As Integer, ByVal nTransaction As Integer, ByVal sCodispl As String, Optional ByVal nAmountdep_aux As Double = 0, Optional ByVal nPayfreq As Integer = 0, Optional ByVal nExtPrem As Integer = 0, Optional ByVal nSurrender As Integer = 0) As Boolean
		Dim lclsRemoteVA595 As eRemoteDB.Execute
		
		On Error GoTo InsPostVA595Upd_Err
		
		lclsRemoteVA595 = New eRemoteDB.Execute
		
		With lclsRemoteVA595
			.StoredProcedure = "insVA595PKG.InsPostVA595Upd"
			.Parameters.Add("sAction", sAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dNulldate", dNulldate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nYear_ini", nYear_ini, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nYear_end", nYear_end, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAmountdep", nAmountdep, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTransactio", nTransaction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCodispl", sCodispl, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nValid", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAmountdep_aux", nAmountdep_aux, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPayfreq", nPayfreq, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nExtPrem", nExtPrem, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nSurrender", nSurrender, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				InsPostVA595Upd = .Parameters("nValid").Value = 1
			End If
		End With
		
InsPostVA595Upd_Err: 
		If Err.Number Then
			InsPostVA595Upd = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsRemoteVA595 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsRemoteVA595 = Nothing
	End Function
	'%InitValues: Inicializa los valores de las variables publicas de la clase
	Private Sub InitValues()
		sCertype = String.Empty
		nBranch = eRemoteDB.Constants.intNull
		nProduct = eRemoteDB.Constants.intNull
		nPolicy = eRemoteDB.Constants.intNull
		nCertif = eRemoteDB.Constants.intNull
		nYear_ini = eRemoteDB.Constants.intNull
		dEffecdate = eRemoteDB.Constants.dtmNull
		nYear_end = eRemoteDB.Constants.intNull
		nAmountdep = eRemoteDB.Constants.intNull
		dNulldate = eRemoteDB.Constants.dtmNull
		mlngUsercode = eRemoteDB.Constants.intNull
		mlngTransaction = eRemoteDB.Constants.intNull
	End Sub
	
	'%Class_Initialize: Se ejecuta cuando se instancia la clase
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		Call InitValues()
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'%InsValVI7003Upd: Validaciones de la transacción
	Public Function InsValVI7003Upd(ByVal sCodispl As String, ByVal sAction As String, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nYear_ini As Integer, ByVal nYear_end As Integer, ByVal nAmountdep As Double) As String
		Dim lclsErrors As eFunctions.Errors
		Dim lblnError As Boolean
		
		On Error GoTo InsValVI7003Upd_Err
		
		lclsErrors = New eFunctions.Errors
		
		With lclsErrors
			
			'+Se valida el año inicial - Desde (Año póliza)
			
			If nYear_ini = eRemoteDB.Constants.intNull Or nYear_ini = 0 Then
				.ErrorMessage(sCodispl, 70099)
				lblnError = True
			ElseIf nYear_ini >= nYear_end Then 
				.ErrorMessage(sCodispl, 60285)
				lblnError = True
			End If
			
			'+Se valida el año final - Hasta (Año póliza)
			
			If nYear_end = eRemoteDB.Constants.intNull Or nYear_end = 0 Then
				.ErrorMessage(sCodispl, 70100)
				lblnError = True
			End If
			
			'+Se valida el monto de prima anual a pagar - Contribución anual
			
			If nAmountdep = eRemoteDB.Constants.intNull Or nAmountdep = 0 Then
				.ErrorMessage(sCodispl, 70101)
				lblnError = True
			End If
			
			'+Se valida que no se repita el rango
			
			If sAction = "Add" Then
				If Not lblnError Then
					If Not InsValRange(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nYear_ini, nYear_end) Then
						.ErrorMessage(sCodispl, 70102)
					End If
				End If
			End If
			InsValVI7003Upd = .Confirm
		End With
		
InsValVI7003Upd_Err: 
		If Err.Number Then
			InsValVI7003Upd = "InsValVI7003Upd: " & Err.Description
		End If
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		On Error GoTo 0
	End Function
	
	'%insPostVI7003Upd: Ejecuta el post de la transacción Planes de Ahorros(VI7003)
	Public Function insPostVI7003Upd(ByVal sAction As String, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nYear_ini As Integer, ByVal dEffecdate As Date, ByVal nYear_end As Integer, ByVal nAmountdep As Double, ByVal dNulldate As Date, ByVal nUsercode As Integer, ByVal nTransaction As Integer) As Boolean
		
		Dim lclsPolicy_Win As Policy_Win
		Dim lcolPer_Deposits As ePolicy.Per_deposits
		
		On Error GoTo insPostVI7003Upd_Err
		
		With Me
			.sCertype = sCertype
			.nBranch = nBranch
			.nProduct = nProduct
			.nPolicy = nPolicy
			.nCertif = nCertif
			.nYear_ini = nYear_ini
			.dEffecdate = dEffecdate
			.nYear_end = nYear_end
			.nAmountdep = nAmountdep
			.dNulldate = dNulldate
			mlngTransaction = nTransaction
			mlngUsercode = nUsercode
		End With
		
		Select Case sAction
			Case "Add"
				insPostVI7003Upd = Add
			Case "Update"
				insPostVI7003Upd = Update
			Case "Del"
				insPostVI7003Upd = Delete
		End Select
		
		If insPostVI7003Upd Then
			lclsPolicy_Win = New Policy_Win
			lcolPer_Deposits = New ePolicy.Per_deposits
			
			'+ Se actualiza la CA017 sin información si se modifica el plan de pago (detalle)
			
			lclsPolicy_Win.Add_PolicyWin(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nUsercode, "CA017", "1")
			
			'+ Se actualiza la tabla Policy_Win
			
			If lcolPer_Deposits.Find(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, True) Then
				lclsPolicy_Win.Add_PolicyWin(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nUsercode, "VI7003", "2")
			Else
				lclsPolicy_Win.Add_PolicyWin(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nUsercode, "VI7003", "1")
			End If
		End If
		
insPostVI7003Upd_Err: 
		If Err.Number Then
			insPostVI7003Upd = False
		End If
		
		On Error GoTo 0
		
		'UPGRADE_NOTE: Object lclsPolicy_Win may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsPolicy_Win = Nothing
		'UPGRADE_NOTE: Object lcolPer_Deposits may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lcolPer_Deposits = Nothing
	End Function
End Class






