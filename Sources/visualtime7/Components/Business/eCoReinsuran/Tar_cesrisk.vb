Option Strict Off
Option Explicit On
Public Class Tar_cesrisk
	'% $Workfile:: Tar_cesrisk.cls                          $%'
	'% $Author:: Vvera                                      $%'
	'% $Date:: 30/03/06 12:52                               $%'
	'% $Revision:: 1                                        $%'
	'+
	'+ Estructura de tabla tar_cesrisk
	'+     Property                Type         DBType   Size Scale  Prec  Null
	'+-------------------------------------------------------------------------
	Public nNumber As Integer ' NUMBER     22   0     5    N
	Public nBranch_rei As Integer ' NUMBER     22   0     5    N
	Public dStartdate As Date ' DATE       7    0     0    N
	Public nType As Integer
	Public nCovergen As Integer ' NUMBER     22   0     5    N
	Public dEffecdate As Date ' DATE       7    0     0    N
	Public dNulldate As Date ' DATE       7    0     0    S
	Public nRate As Double ' NUMBER     22   6     8    S
	Public nUsercode As Integer ' NUMBER     22   0     5    N
	Public nClass_risk As Integer
	
	'%insValCR780_k: Esta función se encarga de validar los datos introducidos en la forma CR780_k (Header).
	Public Function insValCR780_k(ByVal sCodispl As String, ByVal nBranch_rei As Integer, ByVal nNumber As Integer, ByVal nType As Integer, ByVal dEffecdate As Date) As String
		Dim lclsErrors As eFunctions.Errors
		Dim lclsContrproc As eCoReinsuran.Contrproc
		Dim lintReten As Integer
		Dim ldtmDate As Date
		Dim lblnFilled As Boolean
		
		On Error GoTo insValCR780_k_Err
		
		lclsErrors = New eFunctions.Errors
		lclsContrproc = New eCoReinsuran.Contrproc
		
		lblnFilled = False
		
		'+ Se valida el ramo del reaseguro
		If nBranch_rei = eRemoteDB.Constants.intNull Or nBranch_rei = 0 Then
			Call lclsErrors.ErrorMessage(sCodispl, 60314)
		Else
			lblnFilled = True
		End If
		
		'+Se valida que el código del contrato
		If nNumber = eRemoteDB.Constants.intNull Or nNumber = 0 Then
			Call lclsErrors.ErrorMessage(sCodispl, 3357)
		Else
			lblnFilled = True
		End If
		
		'+Se valida que el tipo de contrato
		If nType = eRemoteDB.Constants.intNull Or nType = 0 Then
			Call lclsErrors.ErrorMessage(sCodispl, 6018)
		Else
			lblnFilled = True
		End If
		
		'+Validacion de la fecha del contrato
		If dEffecdate = eRemoteDB.Constants.dtmNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 9068)
		Else
			lblnFilled = True
		End If
		
		'+ Se valida que el registro exista en la tabla CONTRPROC
		If lblnFilled Then
			If Not lclsContrproc.Find(nNumber, nType, nBranch_rei, dEffecdate) Then
				Call lclsErrors.ErrorMessage(sCodispl, 21002)
			End If
		End If
		
		insValCR780_k = lclsErrors.Confirm
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		
insValCR780_k_Err: 
		If Err.Number Then
			insValCR780_k = insValCR780_k & Err.Description
		End If
		
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		'UPGRADE_NOTE: Object lclsContrproc may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsContrproc = Nothing
		
		On Error GoTo 0
	End Function
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		nNumber = eRemoteDB.Constants.intNull
		nBranch_rei = eRemoteDB.Constants.intNull
		dStartdate = eRemoteDB.Constants.dtmNull
		dEffecdate = eRemoteDB.Constants.dtmNull
		dNulldate = eRemoteDB.Constants.dtmNull
		nRate = eRemoteDB.Constants.intNull
		nUsercode = eRemoteDB.Constants.intNull
		nClass_risk = eRemoteDB.Constants.intNull
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	'%insPostCR726: Esta función se encarga de almacenar los datos en la tabla Contr_rate_I
	Public Function insPostCR780(ByVal sCodispl As String, ByVal sMainAction As String, ByVal nBranch_rei As Integer, ByVal nNumber As Integer, ByVal nClass_risk As Integer, ByVal nCovergen As Integer, ByVal dEffecdate As Date, ByVal nRate As Double, ByVal nUsercode As Integer) As Boolean
		
		On Error GoTo insPostCR780_err
		
		Me.nBranch_rei = nBranch_rei
		Me.nNumber = nNumber
		Me.nClass_risk = nClass_risk
		Me.nCovergen = nCovergen
		Me.dEffecdate = dEffecdate
		Me.nRate = nRate
		Me.nUsercode = nUsercode
		
		Select Case sMainAction
			
			'+ Si la opción seleccionada es Registrar.
			
			Case "Add"
				insPostCR780 = InsupdTar_Cesrisk(1)
				'+ Si la opción seleccionada es Modificar.
			Case "Update"
				insPostCR780 = InsupdTar_Cesrisk(2)
				'+ Si la opción seleccionada es Eliminar.
			Case "Del"
				insPostCR780 = InsupdTar_Cesrisk(3)
		End Select
		
insPostCR780_err: 
		If Err.Number Then
			insPostCR780 = False
		End If
		
		On Error GoTo 0
	End Function
	Public Function insValCR780(ByVal sCodispl As String, ByVal sAction As String, ByVal nBranch_rei As Integer, ByVal nNumber As Integer, ByVal nType As Integer, ByVal nCovergen As Integer, ByVal nClass_risk As Integer, ByVal nRate As Double, ByVal dEffecdate As Date) As String
		
		Dim lclsErrors As eFunctions.Errors
		Dim lclsTar_cesrisks As eCoReinsuran.Tar_cesrisks
		
		On Error GoTo insValCR780_Err
		
		lclsErrors = New eFunctions.Errors
		lclsTar_cesrisks = New eCoReinsuran.Tar_cesrisks
		
		If nClass_risk = eRemoteDB.Constants.intNull Or nClass_risk = 0 Then
			Call lclsErrors.ErrorMessage(sCodispl, 3225)
		End If
		
		If nRate = eRemoteDB.Constants.intNull Or nRate = 0 Then
			Call lclsErrors.ErrorMessage(sCodispl, 10121)
		End If
		
		If nCovergen = eRemoteDB.Constants.intNull Or nCovergen = 0 Then
			Call lclsErrors.ErrorMessage(sCodispl, 60331)
		End If
		
		If sAction = "Add" And lclsTar_cesrisks.Find(nNumber, nBranch_rei, nType, dEffecdate, nCovergen) Then
			Call lclsErrors.ErrorMessage(sCodispl, 7148)
		End If
		
		insValCR780 = lclsErrors.Confirm
		
insValCR780_Err: 
		If Err.Number Then
			insValCR780 = insValCR780 & Err.Description
		End If
		
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		'UPGRADE_NOTE: Object lclsTar_cesrisks may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsTar_cesrisks = Nothing
		
		On Error GoTo 0
		
	End Function
	Public Function InsupdTar_Cesrisk(ByVal nAction As Integer) As Boolean
		
		Dim lreccreTar_cesrisk As eRemoteDB.Execute
		
		On Error GoTo InsupdTar_Cesrisk_Err
		
		lreccreTar_cesrisk = New eRemoteDB.Execute
		
		With lreccreTar_cesrisk
			.StoredProcedure = "insUpdtar_Cesrisk"
			.Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nNumber", nNumber, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch_rei", nBranch_rei, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nClass_risk", nClass_risk, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCovergen", nCovergen, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRate", nRate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			InsupdTar_Cesrisk = .Run(False)
		End With
		
InsupdTar_Cesrisk_Err: 
		If Err.Number Then
			InsupdTar_Cesrisk = False
		End If
		'UPGRADE_NOTE: Object lreccreTar_cesrisk may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lreccreTar_cesrisk = Nothing
		On Error GoTo 0
		
	End Function
End Class






