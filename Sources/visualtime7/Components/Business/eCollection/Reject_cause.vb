Option Strict Off
Option Explicit On
Public Class Reject_cause
	'%-------------------------------------------------------%'
	'% $Workfile:: Reject_cause.cls                         $%'
	'% $Author:: Gletelier                                  $%'
	'% $Date:: 8/10/09 3:29p                                $%'
	'% $Revision:: 3                                        $%'
	'%-------------------------------------------------------%'
	
	Public nRejectCause As Integer 'NUMBER(5) NOT NULL,
	Public nBank_code As Double 'NUMBER(10) NOT NULL,
	Public nWay_Pay As Integer 'NUMBER(5) NOT NULL,
	Public sDescript As String 'CHAR(30) NOT NULL,
	Public sShort_des As String 'CHAR(12) NULL,
	Public sStatregt As String 'CHAR(1) NULL,
	Public nUsercode As Integer 'NUMBER(5) NOT NULL
	Public sNO_Endeavour As String
	Public sDescbankcode As String
	Public nBulletins As Double
	Public nReceipt As Double
	Public nPremium As Double
	Public sDesc_Rejectcause As String
	Public nPolicy As Double
	Public nProduct As Integer
	Public sProduct As String
	Public sDocument As String
	Public sSel As String
	Public dNextreceip As Date
	
	'% insvalMCO827_K: se realizan las validaciones del encabezado de la transacción
	Public Function insvalMCO827_K(ByVal sCodispl As String, ByVal nWay_Pay As Integer, ByVal nBank_code As Double) As String
		Dim lclsErrors As eFunctions.Errors
		
		On Error GoTo insvalMCO827_K_err
		
		lclsErrors = New eFunctions.Errors
		
		With lclsErrors
			'+ La vía de pago debe estar llena
			If nWay_Pay = eRemoteDB.Constants.intNull Then
				Call .ErrorMessage(sCodispl, 38044)
			End If
			
			'+ El código del banco debe estar lleno
			If nBank_code = eRemoteDB.Constants.intNull And nWay_Pay = 1 Then
				Call .ErrorMessage(sCodispl, 55000)
			End If
			
			insvalMCO827_K = .Confirm
		End With
		
insvalMCO827_K_err: 
		If Err.Number Then
			insvalMCO827_K = "insvalMCO827_K: " & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
	End Function
	
	'% insvalMCO827: se realizan las validaciones de la zona masiva de la transacción
	Public Function insvalMCO827(ByVal sCodispl As String, ByVal sAction As String, ByVal nRejectCause As Integer, ByVal nBank_code As Double, ByVal nWay_Pay As Integer, ByVal sDescript As String, ByVal sStatregt As String) As String
		Dim lclsErrors As eFunctions.Errors
		
		On Error GoTo insvalMCO827_err
		
		lclsErrors = New eFunctions.Errors
		
		With lclsErrors
			'+ La causa del rechazo debe estar llena
			If nRejectCause = eRemoteDB.Constants.intNull Then
				Call .ErrorMessage(sCodispl, 10872)
			End If
			
			'+ La descripción debe estar llena
			If sDescript = String.Empty Then
				Call .ErrorMessage(sCodispl, 10071)
			End If
			
			'+ El estado debe estar lleno
			If sStatregt = "0" Then
				Call .ErrorMessage(sCodispl, 9089)
			End If
			
			If sAction = "Add" Then
				'+ La causa de rechazo debe ser única para la vía de pago/banco
				If insvalExists(nRejectCause, nBank_code, nWay_Pay) Then
					Call .ErrorMessage(sCodispl, 55035)
				End If
			End If
			
			insvalMCO827 = .Confirm
		End With
		
insvalMCO827_err: 
		If Err.Number Then
			insvalMCO827 = "insvalMCO827: " & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
	End Function
	
	'% insvalExists: se verifica la existencia de la causa de rechazo
	Private Function insvalExists(ByVal nRejectCause As Integer, ByVal nBank_code As Double, ByVal nWay_Pay As Integer) As Boolean
		Dim lclsRemote As eRemoteDB.Execute
		Dim lintExists As Short
		
		On Error GoTo insvalExists_Err
		
		lclsRemote = New eRemoteDB.Execute
		With lclsRemote
			.StoredProcedure = "insvalReject_cause"
			.Parameters.Add("nRejectCause", nRejectCause, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBank_code", nBank_code, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nWay_pay", nWay_Pay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nExists", lintExists, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				insvalExists = (.Parameters("nExists").Value = 1)
			End If
		End With
		
insvalExists_Err: 
		If Err.Number Then
			insvalExists = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsRemote may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsRemote = Nothing
	End Function
	
	'% inspostMCO827: se realizan las actualizaciones de la transacción
	Public Function inspostMCO827(ByVal sAction As String, ByVal nRejectCause As Integer, ByVal nBank_code As Double, ByVal nWay_Pay As Integer, ByVal nUsercode As Integer, Optional ByVal sDescript As String = "", Optional ByVal sShort_des As String = "", Optional ByVal sStatregt As String = "", Optional ByVal sEndeavour As String = "") As Boolean
		With Me
			.nRejectCause = nRejectCause
			.nBank_code = IIf(nBank_code = eRemoteDB.Constants.intNull, 0, nBank_code)
			.nWay_Pay = nWay_Pay
			.nUsercode = nUsercode
			.sDescript = sDescript
			.sShort_des = sShort_des
			.sStatregt = sStatregt
			.sNO_Endeavour = IIf(sEndeavour = CStr(eRemoteDB.Constants.strNull), "2", sEndeavour)
		End With
		Select Case sAction
			Case "Add"
				inspostMCO827 = Add()
			Case "Update"
				inspostMCO827 = Update(2)
			Case "Del"
				inspostMCO827 = Delete()
		End Select
	End Function
	
	'% Add: se agrega un registro a la tabla
	Private Function Add() As Boolean
		Add = Update(1)
	End Function
	
	'% Delete: se elimina un registro de la tabla
	Private Function Delete() As Boolean
		Delete = Update(3)
	End Function
	
	'% Update: se actualizan los datos en la tabla
	Private Function Update(ByVal nAction As Short) As Object
		Dim lclsRemote As eRemoteDB.Execute
		
		On Error GoTo Update_Err
		
		lclsRemote = New eRemoteDB.Execute
		
		With lclsRemote
			.StoredProcedure = "insupdReject_cause"
			.Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRejectcause", nRejectCause, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBank_code", nBank_code, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nWay_pay", nWay_Pay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDescript", sDescript, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 60, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sShort_des", sShort_des, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sStatregt", sStatregt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sNo_Endeavour", sNO_Endeavour, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Update = .Run(False)
		End With
		
Update_Err: 
		If Err.Number Then
			Update = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsRemote may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsRemote = Nothing
	End Function
	
	'% InsValCO982: Validacion de la transaccion Cambio de via de pago por rechazo de cobranza
	Public Function InsValCO982_k(ByVal sCodispl As String, ByVal nBank_code As Double, ByVal nYear As Short) As String
		Dim lclsErrors As eFunctions.Errors
		
		On Error GoTo InsValCO982_k_err
		
		lclsErrors = New eFunctions.Errors
		
		With lclsErrors
			'+ La entidad financiera debe estar llena
			If nBank_code = eRemoteDB.Constants.intNull Then
				Call .ErrorMessage(sCodispl, 70141)
			End If
			
			If nYear = eRemoteDB.Constants.intNull Then
				Call .ErrorMessage(sCodispl, 9060)
			End If
			
			InsValCO982_k = .Confirm
		End With
		
InsValCO982_k_err: 
		If Err.Number Then
			InsValCO982_k = "InsValCO982_k: " & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
	End Function
	
	'% insPostCO982Upd: Actualiza los registros correspondientes en la tabla tmp_co982
	Public Function insPostCO982Upd(ByVal sKey As String, ByVal nBulletins As Double, ByVal nPolicy As Double, ByVal sSel As String) As Boolean
		On Error GoTo insPostCO982Upd_Err
		
		Dim lrecinsCO982 As eRemoteDB.Execute
		lrecinsCO982 = New eRemoteDB.Execute
		
		With lrecinsCO982
			.StoredProcedure = "InsPostCO982Upd"
			.Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBulletins", nBulletins, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sSel", sSel, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			insPostCO982Upd = .Run(False)
		End With
		
insPostCO982Upd_Err: 
		If Err.Number Then
			insPostCO982Upd = False
		End If
		On Error GoTo 0
	End Function
	
	'% insPostCO982: Actualiza los registros correspondientes a la transacción CO982.
	Public Function insPostCO982(ByVal sKey As String) As Boolean
		Dim lrecinsUpdCO982 As eRemoteDB.Execute
		On Error GoTo insPostCO982_Err
		
		lrecinsUpdCO982 = New eRemoteDB.Execute
		With lrecinsUpdCO982
			.StoredProcedure = "InsPostCO982"
			.Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			insPostCO982 = .Run(False)
		End With
		
insPostCO982_Err: 
		If Err.Number Then
			insPostCO982 = False
		End If
		On Error GoTo 0
	End Function
	
	'% insPostCO982UpdAll: Actualiza los registros correspondientes en la tabla tmp_co982
	Public Function insPostCO982UpdAll(ByVal sKey As String, ByVal sSel As String) As Boolean
		On Error GoTo insPostCO982UpdAll_Err
		
		Dim lrecinsCO982All As eRemoteDB.Execute
		lrecinsCO982All = New eRemoteDB.Execute
		
		With lrecinsCO982All
			.StoredProcedure = "InsPostCO982UpdAll"
			.Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sSel", sSel, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			insPostCO982UpdAll = .Run(False)
		End With
		
insPostCO982UpdAll_Err: 
		If Err.Number Then
			insPostCO982UpdAll = False
		End If
		On Error GoTo 0
	End Function
End Class






