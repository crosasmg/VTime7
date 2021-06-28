Option Strict Off
Option Explicit On
Public Class Contr_rate_I
	'%-------------------------------------------------------%'
	'% $Workfile:: Contr_rate_I.cls                         $%'
	'% $Author:: Nvaplat40                                  $%'
	'% $Date:: 13/10/03 6:15p                               $%'
	'% $Revision:: 25                                       $%'
	'%-------------------------------------------------------%'
	'+
	'+ Estructura de tabla contr_rate_I al 03-26-2002 17:44:12
	'+     Property                Type         DBType   Size Scale  Prec  Null
	'+-------------------------------------------------------------------------
	Public nNumber As Integer ' NUMBER     22   0     5    N
	Public nBranch_rei As Integer ' NUMBER     22   0     5    N
	Public nType As Integer ' NUMBER     22   0     5    N
	Public dStartdate As Date ' DATE       7    0     0    N
	Public nCovergen As Integer ' NUMBER     22   0     5    N
	Public nAge_ini As Integer ' NUMBER     22   0     5    N
	Public nAge_reinsu As Integer ' NUMBER     22   0     5    N
	Public dEffecdate As Date ' DATE       7    0     0    N
	Public dNulldate As Date ' DATE       7    0     0    S
	Public nRatewomen As Double ' NUMBER     22   6     8    S
	Public nPremwomen As Double ' NUMBER     22   2     10   S
	Public nRatemen As Double ' NUMBER     22   6     8    S
	Public nPremmen As Double ' NUMBER     22   2     10   S
	Public dCompdate As Date ' DATE       7    0     0    N
	Public nUsercode As Integer ' NUMBER     22   0     5    N
	
	
	
	'%insValCR726_k: Esta función se encarga de validar los datos introducidos en la forma CR726_k (Header).
	Public Function insValCR726_k(ByVal sCodispl As String, ByVal nAction As Integer, ByVal nBranch_rei As Integer, ByVal nNumber As Integer, ByVal nType As Integer, ByVal nCovergen As Integer, ByVal dEffecdate As Date, ByVal nDuplicate As Integer) As String
		Dim lclsErrors As eFunctions.Errors
		Dim lclsContrproc As eCoReinsuran.Contrproc
		Dim lclsContr_rate_is As eCoReinsuran.Contr_rate_Is
		Dim lintReten As Integer
		Dim ldtmDate As Date
		Dim lblnFilled As Boolean
		
		On Error GoTo insValCR726_k_Err
		
		lclsErrors = New eFunctions.Errors
		lclsContrproc = New eCoReinsuran.Contrproc
		lclsContr_rate_is = New eCoReinsuran.Contr_rate_Is
		
		lblnFilled = False
		
		'+ Valida que el registro a duplicar no exista en Contr_rate_i
		If nDuplicate = 1 And lclsContr_rate_is.Find(sCodispl, nAction, nBranch_rei, nNumber, nType, nCovergen, dEffecdate) Then
			Call lclsErrors.ErrorMessage(sCodispl, 55858)
		End If
		
		
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
		
		'+Se valida la cobertura genérica
		If nCovergen = eRemoteDB.Constants.intNull Or nCovergen = 0 Then
			Call lclsErrors.ErrorMessage(sCodispl, 60315)
		Else
			lblnFilled = True
		End If
		
		'+Validacion de la fecha del contrato
		If dEffecdate = eRemoteDB.Constants.dtmNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 9068)
		Else
			lblnFilled = True
		End If
		
		'+Si es modificar no debe existir una modificación posterior a la fecha
		If nAction = 302 Then
			If dEffecdate <> eRemoteDB.Constants.dtmNull Then
				ldtmDate = getMaxEffecdate(nBranch_rei, nNumber, nType, dEffecdate, nCovergen)
				If ldtmDate <> eRemoteDB.Constants.dtmNull Then
					If dEffecdate < ldtmDate Then
						Call lclsErrors.ErrorMessage(sCodispl, 10868)
						lblnFilled = False
					Else
						lblnFilled = True
					End If
				End If
			End If
		End If
		
		'+ Se valida que el registro exista en la tabla CONTRPROC
		If lblnFilled Then
			If Not lclsContrproc.Find(nNumber, nType, nBranch_rei, dEffecdate) Then
				Call lclsErrors.ErrorMessage(sCodispl, 21002)
			End If
		End If
		
		insValCR726_k = lclsErrors.Confirm
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		
insValCR726_k_Err: 
		If Err.Number Then
			insValCR726_k = insValCR726_k & Err.Description
		End If
		
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		'UPGRADE_NOTE: Object lclsContrproc may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsContrproc = Nothing
		'UPGRADE_NOTE: Object lclsContr_rate_is may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsContr_rate_is = Nothing
		
		On Error GoTo 0
	End Function
	
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		nNumber = eRemoteDB.Constants.intNull
		nBranch_rei = eRemoteDB.Constants.intNull
		nType = eRemoteDB.Constants.intNull
		dStartdate = eRemoteDB.Constants.dtmNull
		nCovergen = eRemoteDB.Constants.intNull
		nAge_ini = eRemoteDB.Constants.intNull
		nAge_reinsu = eRemoteDB.Constants.intNull
		dEffecdate = eRemoteDB.Constants.dtmNull
		dNulldate = eRemoteDB.Constants.dtmNull
		nRatewomen = eRemoteDB.Constants.intNull
		nPremwomen = eRemoteDB.Constants.intNull
		nRatemen = eRemoteDB.Constants.intNull
		nPremmen = eRemoteDB.Constants.intNull
		dCompdate = eRemoteDB.Constants.dtmNull
		nUsercode = eRemoteDB.Constants.intNull
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	Public Function getMaxEffecdate(ByVal nBranch_rei As Integer, ByVal nNumber As Integer, ByVal nType As Integer, ByVal dEffecdate As Date, ByVal nCovergen As Integer) As Date
		
		Dim lrecmaxDeffecdate_contr_rate_i As eRemoteDB.Execute
		
		On Error GoTo maxDeffecdate_contr_rate_i_Err
		
		lrecmaxDeffecdate_contr_rate_i = New eRemoteDB.Execute
		
		'+ Definición de store procedure maxDeffecdate_contr_rate_i al 03-27-2002 11:23:37
		
		With lrecmaxDeffecdate_contr_rate_i
			.StoredProcedure = "maxDeffecdate_contr_rate_i"
			.Parameters.Add("nNumber", nNumber, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch_rei", nBranch_rei, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nType", nType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dStartdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCovergen", nCovergen, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(True) Then
				getMaxEffecdate = .FieldToClass("dEffecdate")
			End If
		End With
		
maxDeffecdate_contr_rate_i_Err: 
		If Err.Number Then
			getMaxEffecdate = eRemoteDB.Constants.dtmNull
		End If
		'UPGRADE_NOTE: Object lrecmaxDeffecdate_contr_rate_i may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecmaxDeffecdate_contr_rate_i = Nothing
		On Error GoTo 0
	End Function
	
	'%insPostCR726: Esta función se encarga de almacenar los datos en la tabla Contr_rate_I
	Public Function insPostCR726(ByVal sCodispl As String, ByVal sMainAction As String, ByVal nBranch_rei As Integer, ByVal nNumber As Integer, ByVal nType As Integer, ByVal nCovergen As Integer, ByVal dEffecdate As Date, ByVal nAge_ini As Integer, ByVal nAge_reinsu As Integer, ByVal nRatewomen As Double, ByVal nPremwomen As Double, ByVal nRatemen As Double, ByVal nPremmen As Double, ByVal nUsercode As Integer) As Boolean
		
		On Error GoTo insPostCR726_err
		
		Me.nBranch_rei = nBranch_rei
		Me.nNumber = nNumber
		Me.nType = nType
		Me.nCovergen = nCovergen
		Me.dEffecdate = dEffecdate
		Me.nAge_ini = nAge_ini
		Me.nAge_reinsu = nAge_reinsu
		Me.nRatewomen = nRatewomen
		Me.nPremwomen = nPremwomen
		Me.nRatemen = nRatemen
		Me.nPremmen = nPremmen
		Me.nUsercode = nUsercode
		
		Select Case sMainAction
			
			'**+ If the selected option is To register.
			'+ Si la opción seleccionada es Registrar.
			
			Case "Add"
				insPostCR726 = Add()
				
				'**+ If the selected option is to modify.
				'+ Si la opción seleccionada es Modificar.
				
			Case "Update"
				insPostCR726 = Upd()
				
				'**+ If the selected option es To eliminate.
				'+ Si la opción seleccionada es Eliminar.
				
			Case "Del"
				insPostCR726 = Del()
		End Select
		
insPostCR726_err: 
		If Err.Number Then
			insPostCR726 = False
		End If
		
		On Error GoTo 0
	End Function
	
	'**% Add: It adds the corresponding data for an agreement of payment by client.
	'% Add: Agrega los datos correspondientes para un convenio de pago por cliente
	Public Function Add() As Boolean
		Dim lreccreContr_rate_i As eRemoteDB.Execute
		
		On Error GoTo Add_Err
		
		lreccreContr_rate_i = New eRemoteDB.Execute
		
		'+
		'+ Definición de store procedure creContr_rate_i al 03-28-2002 15:33:42
		'+
		With lreccreContr_rate_i
			.StoredProcedure = "creContr_rate_i"
			.Parameters.Add("nNumber", nNumber, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch_rei", nBranch_rei, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nType", nType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCovergen", nCovergen, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAge_ini", nAge_ini, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAge_reinsu", nAge_reinsu, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRatewomen", nRatewomen, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 9, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPremwomen", nPremwomen, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRatemen", nRatemen, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 9, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPremmen", nPremmen, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			Add = .Run(False)
		End With
		
Add_Err: 
		If Err.Number Then
			Add = False
		End If
		'UPGRADE_NOTE: Object lreccreContr_rate_i may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lreccreContr_rate_i = Nothing
		On Error GoTo 0
		
	End Function
	Public Function Del() As Boolean
		
		Dim lrecdelContr_rate_i As eRemoteDB.Execute
		
		On Error GoTo delContr_rate_i_Err
		
		lrecdelContr_rate_i = New eRemoteDB.Execute
		
		'+
		'+ Definición de store procedure delContr_rate_i al 04-01-2002 16:09:54
		'+
		With lrecdelContr_rate_i
			.StoredProcedure = "delContr_rate_i"
			.Parameters.Add("nNumber", nNumber, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch_rei", nBranch_rei, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nType", nType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCovergen", nCovergen, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAge_ini", nAge_ini, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAge_reinsu", nAge_reinsu, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			Del = .Run(False)
		End With
		
delContr_rate_i_Err: 
		If Err.Number Then
			Del = False
		End If
		'UPGRADE_NOTE: Object lrecdelContr_rate_i may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecdelContr_rate_i = Nothing
		On Error GoTo 0
		
	End Function
	
	Public Function Upd() As Boolean
		
		Dim lrecupdContr_rate_i As eRemoteDB.Execute
		
		On Error GoTo updContr_rate_i_Err
		
		lrecupdContr_rate_i = New eRemoteDB.Execute
		
		'+
		'+ Definición de store procedure updContr_rate_i al 04-02-2002 09:36:14
		'+
		With lrecupdContr_rate_i
			.StoredProcedure = "updContr_rate_i"
			.Parameters.Add("nNumber", nNumber, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch_rei", nBranch_rei, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nType", nType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCovergen", nCovergen, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAge_ini", nAge_ini, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAge_reinsu", nAge_reinsu, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRatewomen", nRatewomen, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 9, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPremwomen", nPremwomen, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRatemen", nRatemen, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 9, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPremmen", nPremmen, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			Upd = .Run(False)
		End With
		
updContr_rate_i_Err: 
		If Err.Number Then
			Upd = False
		End If
		'UPGRADE_NOTE: Object lrecupdContr_rate_i may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecupdContr_rate_i = Nothing
		On Error GoTo 0
		
	End Function
	
	'%insValCR726: Esta función se encarga de validar los datos introducidos en la forma CR726_k (Details).
	Public Function insValCR726(ByVal sCodispl As String, ByVal sAction As String, ByVal nBranch_rei As Integer, ByVal nNumber As Integer, ByVal nType As Integer, ByVal nCovergen As Integer, ByVal dEffecdate As Date, ByVal nAge_ini As Integer, ByVal nAge_reinsu As Integer, ByVal nRatewomen As Integer, ByVal nPremwomen As Integer, ByVal nRatemen As Integer, ByVal nPremmen As Integer) As String
		Dim lclsErrors As eFunctions.Errors
		Dim lclsContr_rate_is As eCoReinsuran.Contr_rate_Is
		
		On Error GoTo insValCR726_Err
		
		lclsErrors = New eFunctions.Errors
		lclsContr_rate_is = New eCoReinsuran.Contr_rate_Is
		
		'+ Se valida que el registro no exista en la tabla CONTR_RATE_I
		If sAction = "Add" And lclsContr_rate_is.FindCR726(nBranch_rei, nNumber, nType, nCovergen, nAge_ini, nAge_reinsu, dEffecdate) Then
			Call lclsErrors.ErrorMessage(sCodispl, 55034)
		End If
		'+ Se valida que exista al menos un valor en el campo mujeres
		If nRatewomen = eRemoteDB.Constants.intNull And nPremwomen = eRemoteDB.Constants.intNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 60317)
		End If
		
		'+ Se valida que exista al menos un valor en el campo hombres
		If nRatemen = eRemoteDB.Constants.intNull And nPremmen = eRemoteDB.Constants.intNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 60316)
		End If
		
		'+ Se valida la edad inicial
		If nAge_ini = eRemoteDB.Constants.intNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 11109)
		End If
		
		'+ Se valida la edad actuarial
		If nAge_reinsu = eRemoteDB.Constants.intNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 3954)
		End If
		
		insValCR726 = lclsErrors.Confirm
		
insValCR726_Err: 
		If Err.Number Then
			insValCR726 = insValCR726 & Err.Description
		End If
		
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		'UPGRADE_NOTE: Object lclsContr_rate_is may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsContr_rate_is = Nothing
		
		On Error GoTo 0
		
	End Function
	'% InsDupContr_rate_I: Invoca al procedimiento que duplica, la información
	'%                     de la tabla para un nueva llave
	Public Function InsDupContr_rate_I(ByVal nNumber As Integer, ByVal nBranch_rei As Integer, ByVal nType As Integer, ByVal nCovergen As Integer, ByVal dEffecdate As Date, ByVal nNumber_new As Integer, ByVal nBranch_rei_new As Integer, ByVal nType_new As Integer, ByVal nCovergen_new As Integer, ByVal dEffecdate_new As Date, ByVal nUsercode As Integer) As Boolean
		Dim lrecinsDupcontr_rate_i As eRemoteDB.Execute
		
		On Error GoTo insDupcontr_rate_i_Err
		
		lrecinsDupcontr_rate_i = New eRemoteDB.Execute
		
		'+
		'+ Definición de store procedure insDupcontr_rate_i al 04-24-2002 15:52:02
		'+
		With lrecinsDupcontr_rate_i
			.StoredProcedure = "insDupcontr_rate_i"
			.Parameters.Add("nNumber", nNumber, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch_rei", nBranch_rei, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nType", nType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCovergen", nCovergen, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nNumber_new", nNumber_new, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch_rei_new", nBranch_rei_new, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nType_new", nType_new, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCovergen_new", nCovergen_new, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate_new", dEffecdate_new, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(False) Then
				InsDupContr_rate_I = True
			Else
				InsDupContr_rate_I = False
			End If
		End With
		
insDupcontr_rate_i_Err: 
		If Err.Number Then
			InsDupContr_rate_I = False
		End If
		'UPGRADE_NOTE: Object lrecinsDupcontr_rate_i may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsDupcontr_rate_i = Nothing
		On Error GoTo 0
	End Function
End Class






