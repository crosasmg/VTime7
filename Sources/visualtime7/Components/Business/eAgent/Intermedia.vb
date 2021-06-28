Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Public Class Intermedia
	'%-------------------------------------------------------%'
	'% $Workfile:: Intermedia.cls                           $%'
	'% $Author:: Gletelier                                  $%'
	'% $Date:: 6/10/09 4:56p                                $%'
	'% $Revision:: 4                                        $%'
	'%-------------------------------------------------------%'
	
	'+ Propiedades según la tabla en el sistema el 28/01/2000
	'+ El campo llave corresponde a nIntermed.
	
	'+
	'+ Estructura de tabla vgajardo.intermedia al 03-22-2003 15:48:01
	'+         Property                Type         DBType   Size Scale  Prec  Null
	'+-----------------------------------------------------------------------------
	Public nIntermed As Integer ' NUMBER     22   0     10   N
	Public sClient As String ' CHAR       14   0     0    S
	Public dCompdate As Date ' DATE       7    0     0    S
	Public nComtabge As Integer ' NUMBER     22   0     5    S
	Public nComtabli As Integer ' NUMBER     22   0     5    S
	Public dInpdate As Date ' DATE       7    0     0    S
	Public nInt_status As Integer ' NUMBER     22   0     5    S
	Public nIntertyp As Integer ' NUMBER     22   0     5    S
	Public nNullcode As Integer ' NUMBER     22   0     5    S
	Public dNulldate As Date ' DATE       7    0     0    S
	Public nOffice As Integer ' NUMBER     22   0     5    S
	Public nSupervis As Integer ' NUMBER     22   0     10   S
	Public nTable_cod As Integer ' NUMBER     22   0     5    S
	Public nTax As Double ' NUMBER     22   2     4    S
	Public nUsercode As Integer ' NUMBER     22   0     5    S
	Public sCol_agree As String ' CHAR       1    0     0    S
	Public nNotenum As Integer ' NUMBER     22   0     10   S
	Public nEco_sche As Integer ' NUMBER     22   0     5    S
	Public sInter_id As String ' CHAR       10   0     0    S
	Public sAgreeInt As String ' CHAR       1    0     0    S
	Public sLife As String ' CHAR       1    0     0    S
	Public sNonlife As String ' CHAR       1    0     0    S
	Public nLife_sche As Integer ' NUMBER     22   0     5    S
	Public nGen_sche As Integer ' NUMBER     22   0     5    S
	Public nLegal_sch As Integer ' NUMBER     22   0     5    S
	Public nSup_gen As Integer ' NUMBER     22   0     10   S
	Public nInsu_Assist As Integer ' NUMBER     22   0     10   S
	Public nGoal_gen As Integer ' NUMBER     22   0     5    S
	Public nGoal_life As Integer ' NUMBER     22   0     5    S
	Public nInsu_assistlif As Integer ' NUMBER     22   0     10   S
	Public nAgency As Integer ' NUMBER     22   0     5    S
	Public nOfficeAgen As Integer ' NUMBER     22   0     5    S
	Public nSlc_Tab_nr As Integer ' NUMBER     22   0     5    S
	Public sValid As String
	Public dCommidate As Date
	
	'+ Variables auxiliares
	Public blnCol_Agree As Boolean
	Public sCliename As String
	Public sParticin As String
	Public sIntertyp As String
	Public sOfficeDes As String
	Public sOrgName As String
	Public nCircular_doc As Integer
	Public sClientDig As String
	
	Public WithInformation As String
	Public sKey As String
	
	'+ Variables utilizadas para la transacción AGL008.
	Public sCertype As String
	Public nBranch As Integer
	Public nProduct As Integer
	Public nPolicy As Double
	Public dStartdate As Date
	Public dExpirdat As Date
	Public nIntermedPol As Integer
	Public nPremanual As Double
	Public nComanual As Double
	
	Public Enum eStatusAgent
		NotExist
		Exist
		NotValid
		Suspend
		Active
	End Enum
	Public nPay_Comm As Double
	
	
	
	'%insValAGL620_K: Función que realiza la validacion de los datos introducidos en la sección de Encabezado
	Public Function insValAGL620_K(ByVal sCodispl As String, ByVal nTyp_Proc As Integer, ByVal nPay_Comm As Double, ByVal dEffecdate As Date, ByVal dEffecdateEnd As Date, ByVal dEffeclastProc As Date, ByVal sOptTyp As String) As String
		
		'+ dEffeclastProc : Fecha del último proceso
		Dim lclsErrors As eFunctions.Errors
		
		On Error GoTo insValAGL620_K_Err
		
		lclsErrors = New eFunctions.Errors
		
		'+ Se debe haber ejecutado la liquidación de comisiones para sus respectivos vended
		If sOptTyp = "2" Then
			Call lclsErrors.ErrorMessage(sCodispl, 55130)
		End If
		
		'+ Si el tipo de proceso corresponde a Procesar e imprimir
		If nTyp_Proc = 1 Then
			
			'+ Si el proceso fue ejecutado para el rango de fechas se envía la advertencia
			If dEffecdate <> dtmNull And dEffeclastProc <> dtmNull Then
				If dEffecdate > dEffeclastProc Then
					Call lclsErrors.ErrorMessage(sCodispl, 60152)
				End If
			End If
			
			'+ Debe indicar la fecha de valorización
			If dEffeclastProc = dtmNull Then
				Call lclsErrors.ErrorMessage(sCodispl, 55527)
			End If
		Else
			
			'+ Si el tipo de proceso corresponde a Reimpresión
			'+ Si no se ha indicado código del intermediario, se debe indicar el número de liquidación.
			If nPay_Comm <= 0 Then
				Call lclsErrors.ErrorMessage(sCodispl, 60134)
			End If
		End If
		
		'+ La fecha hasta debe ser posterior a la fecha desde
		If dEffecdateEnd <> dtmNull Then
			If dEffecdate > dEffecdateEnd Then
				Call lclsErrors.ErrorMessage(sCodispl, 36006)
			End If
		End If
		
		insValAGL620_K = lclsErrors.Confirm
		
insValAGL620_K_Err: 
		If Err.Number Then
			insValAGL620_K = "insValAGL620_K: " & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
	End Function
	
	'%insPostAGL620_K: Función que realiza los cálculos y actualizaciones relacionadas al proceso
	'%                 de liquidación de comisiones.
	Public Function insPostAGL620_K(ByVal sCodispl As String, ByVal nTyp_Proc As Integer, ByVal nPay_Comm As Double, ByVal dEffecdate As Date, ByVal dEffecdateEnd As Date, ByVal dEffeclastProc As Date, ByVal nIntertyp As Integer, ByVal nIntermed As Double, ByVal nUsercode As Integer, ByVal sOptTyp As String, ByVal sOptprocess As String) As Boolean
		Dim lclsRemote As eRemoteDB.Execute
		
		On Error GoTo insPostAGL620_K_Err
		
		lclsRemote = New eRemoteDB.Execute
		
		With lclsRemote
			.StoredProcedure = "insLiquidation"
			.Parameters.Add("nTyp_Proc", nTyp_Proc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate_ini", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate_end", dEffecdateEnd, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInterTyp", IIf(nIntertyp = eRemoteDB.Constants.intNull, 0, nIntertyp), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nIntermed", IIf(nIntermed = eRemoteDB.Constants.intNull, 0, nIntermed), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPay_Comm", IIf(nPay_Comm = eRemoteDB.Constants.intNull, 0, nPay_Comm), eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dVal_Date", dEffeclastProc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUserCode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sOptTyp", sOptTyp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sOptProcess", sOptprocess, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				insPostAGL620_K = True
				Me.nPay_Comm = .Parameters("nPay_Comm").Value
			End If
		End With
		
insPostAGL620_K_Err: 
		If Err.Number Then
			insPostAGL620_K = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsRemote may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsRemote = Nothing
		
	End Function
	
	'%insValAGL605_K: Función que realiza la validacion de los datos introducidos en la sección de Encabezado
	Public Function insValAGL605_K(ByVal sCodispl As String, ByVal nInterm_Typ As Integer, ByVal dEffecdate As Date, ByVal dEffeclastProc As Date, ByVal sOptTyp As String) As String
		Dim lclsErrors As eFunctions.Errors
		On Error GoTo insValAGL605_K_Err
		lclsErrors = New eFunctions.Errors
		
		'+ Se debe haber ejecutado la liquidación de comisiones para sus respectivos vended
		If sOptTyp = "2" Then
			Call lclsErrors.ErrorMessage(sCodispl, 55130)
		End If
		
		'+ Tipo de intermediario debe estar lleno
		'If nInterm_Typ = 0 Or nInterm_Typ = eRemoteDB.Constants.intNull Then
        '	Call lclsErrors.ErrorMessage(sCodispl, 10095)
        'End If
		
		'+ Fecha debe estar llena
		If dEffecdate = dtmNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 2056)
		Else
			If dEffecdate <= dEffeclastProc Then
				Call lclsErrors.ErrorMessage(sCodispl, 3090)
			End If
		End If
		
		insValAGL605_K = lclsErrors.Confirm
		
insValAGL605_K_Err: 
		If Err.Number Then
			insValAGL605_K = "insValAGL605_K: " & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
	End Function
	'%insValAGL605_K:Función que realiza la validacion de los datos introducidos en la sección de Encabezado
	Public Function insPostAGL605_K(ByVal nInsur_area As Integer, ByVal sOptprocess As String, ByVal dEffecdate As Date, ByVal nIntertyp As Integer, ByVal nUsercode As Integer, ByVal sOptTyp As Integer) As Boolean
		Dim lclsExecute As eRemoteDB.Execute
		On Error GoTo insPostAGL605_Err
		lclsExecute = New eRemoteDB.Execute
		
		insPostAGL605_K = False
		With lclsExecute
			.StoredProcedure = "insCtasCtes"
			.Parameters.Add("nInsur_Area", nInsur_area, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", CDate(dEffecdate), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nIntertyp", nIntertyp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sOptTyp", sOptTyp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sOptProcess", sOptprocess, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				insPostAGL605_K = True
			End If
		End With
		
insPostAGL605_Err: 
		If Err.Number Then
			insPostAGL605_K = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsExecute may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsExecute = Nothing
	End Function
	'%insValAGL618_K: Método que valida los valores introducidos en la forma AGL618_K
	Public Function insValAGL618_K(ByVal sCodispl As String, ByVal nInterm_Typ As Integer, ByVal dEffecdate As Date, ByVal dEffeclastProc As Date) As String
		
		'+ dEffeclastProc : Fecha del último proceso
		
		Dim lclsErrors As eFunctions.Errors
		
		On Error GoTo insValAGL618_K_Err
		
		lclsErrors = New eFunctions.Errors
		
		'+ Tipo de intermediario debe estar lleno
		If nInterm_Typ = 0 Or nInterm_Typ = eRemoteDB.Constants.intNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 10095)
		End If
		
		'+ Fecha inicial debe estar llena
		If dEffecdate = dtmNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 9071)
		End If
		
		'+ Fecha final debe estar llena
		If dEffeclastProc = dtmNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 9072)
		Else
			
			'+ No debe ser menor ni igual a la fecha inicial
			If dEffeclastProc <= dEffecdate Then
				Call lclsErrors.ErrorMessage(sCodispl, 3240)
			End If
		End If
		
		insValAGL618_K = lclsErrors.Confirm
		
insValAGL618_K_Err: 
		If Err.Number Then
			insValAGL618_K = "insValAGL618_K: " & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
	End Function
	
	'%insPostAGL618: Método que realiza el proceso de cálculo de incentivos de agentes de mantención
	Public Function insPostAGL618(ByVal nIntertyp As Integer, ByVal dIni_date As Date, ByVal dEnd_Date As Date, ByVal nUsercode As Integer) As Boolean
		Dim lexeinsAGL618 As eRemoteDB.Execute
		
		lexeinsAGL618 = New eRemoteDB.Execute
		
		On Error GoTo insPostAGL618_Err
		
		With lexeinsAGL618
			.StoredProcedure = "insAGL618"
			.Parameters.Add("nIntertyp", nIntertyp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dIni_date", dIni_date, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEnd_date", dEnd_Date, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				insPostAGL618 = True
			Else
				insPostAGL618 = False
			End If
		End With
		
insPostAGL618_Err: 
		If Err.Number Then
			insPostAGL618 = False
		End If
		'UPGRADE_NOTE: Object lexeinsAGL618 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lexeinsAGL618 = Nothing
		On Error GoTo 0
	End Function
	
	'% Find: Busca la información de un determinado intermediario
	Public Function Find(ByVal IntermediaCode As Integer, Optional ByVal lblnFind As Boolean = False) As Boolean
		Dim lrecreaIntermedia As eRemoteDB.Execute
		
		On Error GoTo Find_Err
		
		lrecreaIntermedia = New eRemoteDB.Execute
		
		'+ Definición de parámetros para stored procedure 'insudb.reaClient'
		'+ Información leída el 01/07/1999 03:20:55 PM
		
		With lrecreaIntermedia
			.StoredProcedure = "reaIntermedia"
			.Parameters.Add("nIntermed", IntermediaCode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(True) Then
				nIntermed = IntermediaCode
				Me.sClient = .FieldToClass("sClient")
				Me.nComtabge = .FieldToClass("nComtabge")
				Me.nComtabli = .FieldToClass("nComtabli")
				Me.dInpdate = .FieldToClass("dInpdate")
				Me.nInt_status = .FieldToClass("nInt_status")
				Me.nIntertyp = .FieldToClass("nIntertyp")
				Me.nNullcode = .FieldToClass("nNullcode")
				Me.dNulldate = .FieldToClass("dNulldate")
				Me.nOffice = .FieldToClass("nOffice")
				Me.nSupervis = .FieldToClass("nSupervis")
				Me.nTable_cod = .FieldToClass("nTable_cod")
				Me.nTax = .FieldToClass("nTax")
				Me.nUsercode = .FieldToClass("nUsercode")
				Me.sCol_agree = .FieldToClass("sCol_agree")
				Me.nNotenum = .FieldToClass("nNotenum")
				Me.nEco_sche = .FieldToClass("nEco_sche")
				Me.sInter_id = .FieldToClass("sInter_id")
				Me.sAgreeInt = .FieldToClass("sAgreeInt")
				Me.sCliename = .FieldToClass("sCliename")
				Me.sParticin = .FieldToClass("sParticin")
				Me.nGen_sche = .FieldToClass("nGen_sche")
				Me.nLife_sche = .FieldToClass("nLife_sche")
				Me.sLife = .FieldToClass("sLife")
				Me.sNonlife = .FieldToClass("sNonlife")
				Me.nLegal_sch = .FieldToClass("nLegal_sch")
				Me.nSup_gen = .FieldToClass("nSup_Gen")
				Me.nInsu_Assist = .FieldToClass("nInsu_Assist")
				Me.nInsu_assistlif = .FieldToClass("nInsu_AssistLif")
				Me.nGoal_gen = .FieldToClass("nGoal_Gen")
				Me.nGoal_life = .FieldToClass("nGoal_Life")
				Me.nAgency = .FieldToClass("nAgency")
				Me.nOfficeAgen = .FieldToClass("nOfficeAgen")
				Me.nSlc_Tab_nr = .FieldToClass("nSlc_Tab_nr")
				Me.sValid = .FieldToClass("sValid")
				.RCloseRec()
				Find = True
			Else
				Find = False
			End If
		End With
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		
		'UPGRADE_NOTE: Object lrecreaIntermedia may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaIntermedia = Nothing
		
		On Error GoTo 0
	End Function
	
	'% Remove: Elimina la información de un determinado intermediario
	Public Function Remove(ByVal IntermediaCode As String) As Boolean
		Dim lrecdelIntermedia As eRemoteDB.Execute
		Dim lobjValues As New eFunctions.Values
		Dim lintIntermediaCode As Integer
		'Definición de parámetros para stored procedure 'insudb.delIntermedia'
		'Información leída el 06/02/2001 9.40.18
		
		On Error GoTo Remove_Err
		
		lrecdelIntermedia = New eRemoteDB.Execute
		
		lintIntermediaCode = lobjValues.StringToType(IntermediaCode, eFunctions.Values.eTypeData.etdInteger)
		
		With lrecdelIntermedia
			.StoredProcedure = "delIntermedia"
			.Parameters.Add("nIntermed", lintIntermediaCode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Remove = .Run(False)
		End With
		
Remove_Err: 
		If Err.Number Then
			Remove = False
		End If
		
		'UPGRADE_NOTE: Object lrecdelIntermedia may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecdelIntermedia = Nothing
		'UPGRADE_NOTE: Object lobjValues may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjValues = Nothing
		
		On Error GoTo 0
	End Function
	
	'% Find_InterID: verifica la existencia del código oficial.
	Public Function Find_Inter_id(ByVal Inter_id As String) As Boolean
		Dim lrecreaIntermedia_Inter_id As eRemoteDB.Execute
		
		lrecreaIntermedia_Inter_id = New eRemoteDB.Execute
		
		On Error GoTo Find_Inter_id_Err
		
		'+ Definición de parámetros para stored procedure 'insudb.reaIntermedia_Inter_id'
		'+ Información leída el 25/02/2000 08:43:19 AM
		
		With lrecreaIntermedia_Inter_id
			.StoredProcedure = "reaIntermedia_Inter_id"
			.Parameters.Add("sInter_id", Inter_id, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(True) Then
				nIntermed = .FieldToClass("nIntermed")
				.RCloseRec()
				Find_Inter_id = True
			Else
				Find_Inter_id = False
			End If
		End With
		'UPGRADE_NOTE: Object lrecreaIntermedia_Inter_id may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaIntermedia_Inter_id = Nothing
		
Find_Inter_id_Err: 
		If Err.Number Then
			Find_Inter_id = False
		End If
		On Error GoTo 0
	End Function
	'%Find_ClientInter(). Esta funcion busca en la tabla de intermediarios
	'%segun el cliente indicado, si el mismo se encuentra previamente
	'%registrado bajo otro código.
	Public Function Find_ClientInter(ByRef lstrClient As String) As Boolean
		Dim lrecIntermed As eRemoteDB.Execute
		lrecIntermed = New eRemoteDB.Execute
		On Error GoTo Find_ClientInter_Err
		
		With lrecIntermed
			.StoredProcedure = "reaIntermed_v3"
			.Parameters.Add("sClient", lstrClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(True) Then
				'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
				If Not IsDbNull(.FieldToClass("nInt_status")) And .FieldToClass("nInt_status") = 1 Then
					Find_ClientInter = True
					nIntermed = .FieldToClass("nIntermed")
				End If
				nIntermed = .FieldToClass("nIntermed")
				.RCloseRec()
			Else
				Find_ClientInter = False
			End If
		End With
		
		'UPGRADE_NOTE: Object lrecIntermed may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecIntermed = Nothing
		
Find_ClientInter_Err: 
		If Err.Number Then
			Find_ClientInter = False
		End If
		On Error GoTo 0
	End Function
	'% FindTypeInterm_Client: verifica que el tipo de intermediario para un cliente exista
	Public Function FindTypeInterm_Client(ByVal Client As String, ByVal InterTyp As Integer) As Boolean
		Dim lrecinter As eRemoteDB.Execute
		On Error GoTo FindTypeInterm_Client_Err
		
		lrecinter = New eRemoteDB.Execute
		With lrecinter
			.StoredProcedure = "reaIntermed_vG"
			.Parameters.Add("sClient", Client, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nIntertyp", InterTyp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(True) Then
				FindTypeInterm_Client = True
				nIntermed = .FieldToClass("nIntermed")
				sInter_id = .FieldToClass("sInter_id")
				dInpdate = .FieldToClass("dInpdate")
				nSupervis = .FieldToClass("nSupervis")
				nOffice = .FieldToClass("nOffice")
				nIntertyp = .FieldToClass("nIntertyp")
				nInt_status = .FieldToClass("nInt_status")
				sClient = .FieldToClass("sClient")
				nComtabge = .FieldToClass("nComtabge")
				nComtabli = .FieldToClass("nComtabli")
				nNullcode = .FieldToClass("nNullcode")
				dNulldate = .FieldToClass("dNulldate")
				nTable_cod = .FieldToClass("nTable_cod")
				nTax = .FieldToClass("nTax")
				sCol_agree = .FieldToClass("sCol_agree")
				nNotenum = .FieldToClass("nNotenum")
				nEco_sche = .FieldToClass("nEco_sche")
				sLife = .FieldToClass("sLife")
				sNonlife = .FieldToClass("sNonLife")
			Else
				FindTypeInterm_Client = False
			End If
			.RCloseRec()
		End With
		
FindTypeInterm_Client_Err: 
		If Err.Number Then
			FindTypeInterm_Client = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecinter may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinter = Nothing
	End Function
	
	'%findIntermediaClient:
	Public Function findIntermediaClient(ByVal nIntermed As Integer, ByVal nIntertyp As Integer, ByVal dEffecdate As Date) As Boolean
		'-Se define la variable lrec_Intermed que se utilizará como cursor.
		Dim lrec_Intermed As eRemoteDB.Execute
		
		'-Se define el arreglo de parámetro a pasar al store procedure.
		lrec_Intermed = New eRemoteDB.Execute
		
		On Error GoTo findIntermediaClient_err
		'Definición de parámetros para stored procedure 'insudb.reaIntermediaClient'
		'Información leída el 15/11/2000 04:49:59 a.m.
		
		With lrec_Intermed
			.StoredProcedure = "reaIntermediaClient"
			.Parameters.Add("nIntermed", nIntermed, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nIntertyp", nIntertyp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dInpdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(True) Then
				findIntermediaClient = True
				Me.nIntermed = .FieldToClass("nIntermed")
				Me.nIntertyp = .FieldToClass("nIntertyp")
				'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
				If Not IsDbNull(.FieldToClass("sCol_agree")) Then
					Me.blnCol_Agree = True '"1"
				Else
					Me.blnCol_Agree = False
				End If
				Me.nSupervis = .FieldToClass("nSupervis", 0)
				Me.sClient = .FieldToClass("sClient")
				Me.sCliename = .FieldToClass("sCliename")
				Me.nInt_status = .FieldToClass("nInt_status")
				Me.dInpdate = .FieldToClass("dInpDate")
				.RCloseRec()
			End If
		End With
		
findIntermediaClient_err: 
		If Err.Number Then
			findIntermediaClient = False
		End If
		On Error GoTo 0
	End Function
	
	'%FindTrasPol:
	Public Function FindTrasPol(ByVal nIntermed As Integer, ByVal dEffecdate As Date) As Boolean
		'-Se define la variable lrec_Intermed que se utilizará como cursor.
		Dim lrec_Intermed As eRemoteDB.Execute
		
		'-Se define el arreglo de parámetro a pasar al store procedure.
		lrec_Intermed = New eRemoteDB.Execute
		
		On Error GoTo FindTrasPol_err
		'Definición de parámetros para stored procedure 'insudb.ReaTrasPolCount'
		'Información leída el 15/11/2000 04:49:59 a.m.
		
		With lrec_Intermed
			.StoredProcedure = "ReaTrasPolCount"
			.Parameters.Add("nIntermed", nIntermed, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dInpdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(True) Then
				If .FieldToClass("Count") <> 0 Then
					FindTrasPol = True
				Else
					FindTrasPol = False
				End If
				.RCloseRec()
			End If
		End With
		
FindTrasPol_err: 
		If Err.Number Then
			FindTrasPol = False
		End If
		On Error GoTo 0
	End Function
	
	
	'%funcion ValIntermediaCli. Este función devuelve verdadero o falso dependiento de la extistencia
	'%del código de cliente (pasado como parametro) en la tabla de intermediarios (Intermedia).
	Public Function ValIntermediaCli(ByVal sClient As String, ByVal dEffecdate As Object, Optional ByVal lblnOnlyValid As Boolean = True, Optional ByVal lblnFind As Boolean = False) As Boolean
		
		Dim lrecreaIntermediaCli As eRemoteDB.Execute
		Static lblnRead As Boolean
		Static lstrOldClient As String
		
		On Error GoTo ValIntermediaCli_Err
		
		If lstrOldClient <> sClient Or lblnFind Then
			
			lstrOldClient = sClient
			lrecreaIntermediaCli = New eRemoteDB.Execute
			
			With lrecreaIntermediaCli
				
				.StoredProcedure = "reaIntermed_v3" 'Listo
				.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If .Run(True) Then 'Listo
					If Not lblnOnlyValid Then
						lblnRead = True
						.RCloseRec()
					Else
						Do While Not .EOF And Not lblnRead
							
							'+Se valida que sea un intermediario valido
							
							If .FieldToClass("nInt_status") = 1 Then
								lblnRead = True
							Else
								
								'+Se valida la fecha de anulación del intermediario contra la fecha del siniestro
								
								If .FieldToClass("nInt_status") = 2 Then
									If .FieldToClass("dNulldate") > dEffecdate Then
										lblnRead = True
									Else
										lblnRead = False
									End If
								Else
									lblnRead = False
								End If
							End If
							.RNext()
						Loop 
						.RCloseRec()
					End If
				End If
			End With
			'UPGRADE_NOTE: Object lrecreaIntermediaCli may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			lrecreaIntermediaCli = Nothing
		End If
		
		ValIntermediaCli = lblnRead
		
ValIntermediaCli_Err: 
		If Err.Number Then
			ValIntermediaCli = False
		End If
		On Error GoTo 0
	End Function
	'----------------------------------------------------
	'-----------------------------------------------------
	'*****************************************************
	'----------------------------------------------------
	'-----------------------------------------------------
	
	'% UpdIntermedia
	Public Function UpdIntermedia() As Boolean
		Dim lrecupdIntermedia As eRemoteDB.Execute
		lrecupdIntermedia = New eRemoteDB.Execute
		
		On Error GoTo UpdIntermedia_Err
		With lrecupdIntermedia
			.StoredProcedure = "updIntermedia"
			.Parameters.Add("nIntermed", nIntermed, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nComtabge", nComtabge, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nComtabli", nComtabli, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTable_cod", nTable_cod, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nEco_sche", nEco_sche, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCol_agree", sCol_agree, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAgreeInt", sAgreeInt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nLife_Sche", nLife_sche, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nGen_Sche", nGen_sche, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nGoal_Life", nGoal_life, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nGoal_Gen", nGoal_gen, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nSlc_Tab_nr", nSlc_Tab_nr, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			UpdIntermedia = .Run(False)
		End With
		'UPGRADE_NOTE: Object lrecupdIntermedia may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecupdIntermedia = Nothing
		
UpdIntermedia_Err: 
		If Err.Number Then
			UpdIntermedia = False
		End If
		On Error GoTo 0
	End Function
	
	'%insValAGC001_K: Esta función se encarga de validar los datos introducidos en la cabecera de la
	'%forma.
	Public Function insValAGC001_K(ByVal sCodispl As String, ByVal nIntermed As Integer, ByVal dEffecdate As Date) As String
		
		
		Dim lclsErrors As eFunctions.Errors
		
		On Error GoTo insValAGC001_K_Err
		
		lclsErrors = New eFunctions.Errors
		
		'+Validación del Intermediario
		
		If nIntermed = eRemoteDB.Constants.intNull Or nIntermed = 0 Then
			Call lclsErrors.ErrorMessage(sCodispl, 3272)
		Else
			If Not Find(nIntermed) Then
				Call lclsErrors.ErrorMessage(sCodispl, 9053)
			Else
			End If
		End If
		
		If dEffecdate = dtmNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 2056)
		Else
		End If
		
		insValAGC001_K = lclsErrors.Confirm
		
insValAGC001_K_Err: 
		If Err.Number Then
			insValAGC001_K = "insValAGC001_K: " & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
	End Function
	
	'%insPostAGC001_K: Esta función se encarga de validar los datos introducidos en la zona de
	'%cabecera.
	Public Function insPostAGC001_K() As Boolean
		insPostAGC001_K = True
	End Function
	'%insPostAGC001: Esta función se encaga de validar todos los datos introducidos en la forma
	Public Function insPostAGC001() As Boolean
		insPostAGC001 = True
	End Function
	
	'%insValAGC002_K: Esta función se encarga de validar los datos introducidos en la cabecera de la
	'%forma.
	Public Function insValAGC002_K(ByVal sCodispl As String, ByVal nIntermed As Integer, ByVal dStardate As Date, ByVal dEnddate As Date, ByVal sStatLoan As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nLoan As Integer) As String
		
		Dim lclsErrors As eFunctions.Errors
		
		On Error GoTo insValAGC002_K_Err
		
		lclsErrors = New eFunctions.Errors
		
		insValAGC002_K = ""
		
		'+Validación del Intermediario
		
		If nIntermed <> 0 And nIntermed <> eRemoteDB.Constants.intNull Then
			If Not Find(nIntermed) Then
				Call lclsErrors.ErrorMessage(sCodispl, 9053)
			End If
		End If
		
		'+Validación de la fecha Inicial
		
		If dEnddate <> dtmNull Then
			If dStardate <> dtmNull Then
				If dEnddate < dStardate Then
					Call lclsErrors.ErrorMessage(sCodispl, 3240)
				End If
			End If
		End If
		
		'+Validación de Ramo
		
		If nPolicy <> eRemoteDB.Constants.intNull Then
			If nBranch = eRemoteDB.Constants.intNull Or nBranch = 0 Then
				Call lclsErrors.ErrorMessage(sCodispl, 9064)
			End If
		End If
		
		'+Validación de Producto
		
		If nPolicy <> eRemoteDB.Constants.intNull Then
			If nProduct = eRemoteDB.Constants.intNull Or nProduct = 0 Then
				Call lclsErrors.ErrorMessage(sCodispl, 11009)
			End If
		End If
		
		'+Validación de Póliza
		
		If nBranch <> eRemoteDB.Constants.intNull And nBranch <> 0 And nPolicy <> eRemoteDB.Constants.intNull And nPolicy <> 0 Then
			If nPolicy = eRemoteDB.Constants.intNull Or nPolicy = 0 Then
				Call lclsErrors.ErrorMessage(sCodispl, 3003)
			End If
		End If
		
		insValAGC002_K = lclsErrors.Confirm
		
insValAGC002_K_Err: 
		If Err.Number Then
			insValAGC002_K = "insValAGC002_K: " & Err.Description
		End If
		On Error GoTo 0
		
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
	End Function
	
	'%insValAGC002: Esta función se encarga de validar los datos introducidos en la zona de detalle para
	'%forma.
	Public Function insValAGC002() As Boolean
		insValAGC002 = True
	End Function
	
	
	'%insValAGC001: Esta función se encarga de validar los datos introducidos en la zona de detalle para
	'%forma.
	Public Function insValAGC001() As Boolean
		insValAGC001 = True
	End Function
	
	'%insPostAGC002_K: Esta función se encarga de validar los datos introducidos en la zona de
	'%cabecera.
	Public Function insPostAGC002_K() As Boolean
		insPostAGC002_K = True
	End Function
	
	'**%insPostAGL001: It allows to carry out the process corresponding to the preparation of current account of intermediary
	'%  insPostAGL001: Permite llevar a cabo el proceso correspondiente a la preparación de cuenta corriente de intermediario
	Public Function insPostAGL001(ByVal dInit_Date As Date, ByVal dEnd_Date As Date, ByVal sUpd_Ind As String, ByVal nUsercode As Integer) As Boolean
		Dim lrecinsAGL001 As eRemoteDB.Execute
		
		On Error GoTo insPostAGL001_err
		
		lrecinsAGL001 = New eRemoteDB.Execute
		
		sKey = Trim(CStr(nUsercode)) & Trim(CStr(VB.Day(Today))) & Trim(CStr(Month(Today))) & Trim(CStr(Year(Today))) & Trim(CStr(Hour(TimeOfDay))) & Trim(CStr(Minute(TimeOfDay))) & Trim(CStr(Second(TimeOfDay)))
		
		'+ Definición de parámetros para stored procedure 'insudb.insAGL001'
		'+ Información leída el 05/09/2001 11:31:38
		
		With lrecinsAGL001
			.StoredProcedure = "insAGL001"
			'        .Parameters.Add "sKey", sKey, rdbParamInput, rdbVarChar, 30, 0, 0, rdbParamNullable
			.Parameters.Add("Init_date", dInit_Date, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("End_Date", dEnd_Date, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			'        .Parameters.Add "sUpd_Ind", sUpd_Ind, rdbParamInput, rdbVarChar, 1, 0, 0, rdbParamNullable
			insPostAGL001 = .Run(True)
		End With
		'UPGRADE_NOTE: Object lrecinsAGL001 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsAGL001 = Nothing
		
insPostAGL001_err: 
		If Err.Number Then
			insPostAGL001 = False
		End If
	End Function
	
	'%insPostAGC002: Esta función se encaga de validar todos los datos introducidos en la forma
	Public Function insPostAGC002() As Boolean
		insPostAGC002 = True
	End Function
	
	'% reaIntermed_v2
	Public Function reaIntermed_v2() As Boolean
		Dim lrecinter As eRemoteDB.Execute
		On Error GoTo reaIntermed_v2_Err
		lrecinter = New eRemoteDB.Execute
		
		reaIntermed_v2 = False
		With lrecinter
			.StoredProcedure = "reaIntermed_v2"
			.Parameters.Add("sInter_id", sInter_id, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nIntermed", nIntermed, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(True) Then
				reaIntermed_v2 = True
				dInpdate = lrecinter.FieldToClass("dInpdate")
				nSupervis = lrecinter.FieldToClass("nSupervis")
				nOffice = lrecinter.FieldToClass("nOffice")
				nIntertyp = lrecinter.FieldToClass("nIntertyp")
				nInt_status = lrecinter.FieldToClass("nInt_status")
				sClient = lrecinter.FieldToClass("sClient")
			End If
			.RCloseRec()
		End With
		
reaIntermed_v2_Err: 
		If Err.Number Then
			reaIntermed_v2 = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecinter may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinter = Nothing
	End Function
	
	'% reaIntermed_v1
	Public Function reaIntermed_v1() As Boolean
		Dim lrecinter As eRemoteDB.Execute
		lrecinter = New eRemoteDB.Execute
		On Error GoTo reaIntermed_v1_Err
		With lrecinter
			.StoredProcedure = "reaIntermed_v1"
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sInter_id", sInter_id, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nIntermed", nIntermed, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			reaIntermed_v1 = .Run
			dInpdate = lrecinter.FieldToClass("dInpdate")
			nSupervis = lrecinter.FieldToClass("nSupervis")
			nOffice = lrecinter.FieldToClass("nOffice")
			nIntertyp = lrecinter.FieldToClass("nIntertyp")
			nInt_status = lrecinter.FieldToClass("nInt_status")
			sClient = lrecinter.FieldToClass("sClient")
			.RCloseRec()
		End With
		'UPGRADE_NOTE: Object lrecinter may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinter = Nothing
		
reaIntermed_v1_Err: 
		If Err.Number Then
			reaIntermed_v1 = False
		End If
		On Error GoTo 0
	End Function
	
	'% add
	Public Function Add() As Boolean
		Dim lrecIntermedia As eRemoteDB.Execute
		lrecIntermedia = New eRemoteDB.Execute
		On Error GoTo Add_Err
		
		With lrecIntermedia
			.StoredProcedure = "creIntermedia"
			.Parameters.Add("nIntermed", nIntermed, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dInpdate", dInpdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInt_status", nInt_status, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nIntertyp", nIntertyp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nNullcode", nNullcode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dNulldate", dNulldate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOffice", nOffice, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nSupervis", nSupervis, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sInter_id", sInter_id, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sLife", sLife, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sNonLife", sNonlife, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nLegal_sch", nLegal_sch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nSup_Gen", nSup_gen, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInsu_Assist", nInsu_Assist, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInsu_assistLif", nInsu_assistlif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOfficeAgen", IIf(nOfficeAgen = 0, eRemoteDB.Constants.intNull, nOfficeAgen), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAgency", IIf(nAgency = 0, eRemoteDB.Constants.intNull, nAgency), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sValid", IIf(sValid = String.Empty, "2", sValid), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Add = .Run(False)
		End With
		'UPGRADE_NOTE: Object lrecIntermedia may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecIntermedia = Nothing
		
Add_Err: 
		If Err.Number Then
			Add = False
		End If
		On Error GoTo 0
	End Function
	
	'% Deletetablename
	Public Function Deletetablename(ByRef lstrIntermed As String, ByRef lstrtablename As String) As Boolean
		Dim lregTable As eRemoteDB.Execute
		lregTable = New eRemoteDB.Execute
		On Error GoTo Deletetablename_Err
		With lregTable
			.StoredProcedure = "delTablename"
			.Parameters.Add("sTableName", lstrtablename, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sIntermed", lstrIntermed, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 6, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Deletetablename = .Run(False)
		End With
		'UPGRADE_NOTE: Object lregTable may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lregTable = Nothing
		
		
Deletetablename_Err: 
		If Err.Number Then
			Deletetablename = False
		End If
		On Error GoTo 0
	End Function
	
	'% UpdIntermedia_nInterm_id
	Public Function UpdIntermedia_nInterm_id() As Boolean
		Dim lrecIntermedia As eRemoteDB.Execute
		lrecIntermedia = New eRemoteDB.Execute
		On Error GoTo UpdIntermedia_nInterm_id_Err
		With lrecIntermedia
			.StoredProcedure = "updIntermedia_nInter_id"
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sInter_id", sInter_id, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			UpdIntermedia_nInterm_id = .Run(False)
		End With
		'UPGRADE_NOTE: Object lrecIntermedia may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecIntermedia = Nothing
		
		
UpdIntermedia_nInterm_id_Err: 
		If Err.Number Then
			UpdIntermedia_nInterm_id = False
		End If
		On Error GoTo 0
	End Function
	
	'%findValIntermed_Receipt: Rutina que verificar si el cliente es titular de alguno de los recibos
	'%previamente introducidos
	Public Function findValIntermed_Receipt(ByVal sClient As String, ByVal nreceipt As Integer) As Boolean
		Dim lrecreaIntermedia_Receipt As eRemoteDB.Execute
		
		On Error GoTo findValIntermed_Receipt_Err
		findValIntermed_Receipt = False
		
		lrecreaIntermedia_Receipt = New eRemoteDB.Execute
		
		'Definición de parámetros para stored procedure 'insudb.reaIntermedia_Receipt'
		'Información leída el 11/10/2000 15:37:01
		
		With lrecreaIntermedia_Receipt
			.StoredProcedure = "reaIntermedia_Receipt"
			.Parameters.Add("nReceipt", nreceipt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(True) Then
				findValIntermed_Receipt = True
			End If
			.RCloseRec()
		End With
		'UPGRADE_NOTE: Object lrecreaIntermedia_Receipt may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaIntermedia_Receipt = Nothing
		
findValIntermed_Receipt_Err: 
		If Err.Number Then
			findValIntermed_Receipt = False
		End If
		On Error GoTo 0
	End Function
	
	Public Function Update_statusNull() As Boolean
		Dim lrecupdIntermediaNull As eRemoteDB.Execute
		Dim lclsIntermedia As eAgent.Intermedia
		
		lrecupdIntermediaNull = New eRemoteDB.Execute
		lclsIntermedia = New eAgent.Intermedia
		
		On Error GoTo Update_statusNull_Err
		
		'Definición de parámetros para stored procedure 'insudb.updIntermediaNull'
		'Información leída el 05/02/2001 9.57.40
		With lrecupdIntermediaNull
			.StoredProcedure = "updIntermediaNull"
			.Parameters.Add("nIntermed", nIntermed, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dNulldate", dNulldate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInt_status", nInt_status, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nNullcode", nNullcode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Update_statusNull = .Run(False)
		End With
		
		If nCircular_doc > 0 Then
			With lrecupdIntermediaNull
				.StoredProcedure = "updIntermed_partic"
				.Parameters.Add("nIntermed", nIntermed, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nCircular_doc", nCircular_doc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				Update_statusNull = .Run(False)
			End With
		End If
		
		If lclsIntermedia.Find(nIntermed) Then
			With lrecupdIntermediaNull
				.StoredProcedure = "creIntermed_his"
				.Parameters.Add("nIntermed", nIntermed, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("dEffecdate", dNulldate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nNullcode", nNullcode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nIntertyp", lclsIntermedia.nIntertyp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nOffice", lclsIntermedia.nOffice, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nSupervis", lclsIntermedia.nSupervis, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nInt_Status", nInt_status, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("dNulldate", dNulldate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				Update_statusNull = .Run(False)
			End With
		End If
		
		'UPGRADE_NOTE: Object lrecupdIntermediaNull may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecupdIntermediaNull = Nothing
		'UPGRADE_NOTE: Object lclsIntermedia may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsIntermedia = Nothing
		
Update_statusNull_Err: 
		If Err.Number Then
			Update_statusNull = False
		End If
		On Error GoTo 0
	End Function
	Public Function Update_status() As Boolean
		Dim lrecupdIntermediaStatus As eRemoteDB.Execute
		lrecupdIntermediaStatus = New eRemoteDB.Execute
		On Error GoTo Update_status_Err
		'Definición de parámetros para stored procedure 'insudb.updIntermediaStatus'
		'Información leída el 06/02/2001 10.04.52
		With lrecupdIntermediaStatus
			.StoredProcedure = "updIntermediaStatus"
			.Parameters.Add("nIntermed", nIntermed, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInt_status", nInt_status, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Update_status = .Run(False)
		End With
		'UPGRADE_NOTE: Object lrecupdIntermediaStatus may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecupdIntermediaStatus = Nothing
		
		
Update_status_Err: 
		If Err.Number Then
			Update_status = False
		End If
		On Error GoTo 0
	End Function
	Public Function ValRequired(ByVal Intermed As Integer) As Boolean
		Dim lrecinsValRequired_Interm As eRemoteDB.Execute
		lrecinsValRequired_Interm = New eRemoteDB.Execute
		
		On Error GoTo ValRequired_Err
		'Definición de parámetros para stored procedure 'insudb.insValRequired_Interm'
		'Información leída el 05/02/2001 16.21.47
		
		With lrecinsValRequired_Interm
			.StoredProcedure = "insValRequired_Interm"
			.Parameters.Add("nIntermed", Intermed, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(True) Then
				Me.WithInformation = .FieldToClass("WithInformation")
				ValRequired = True
				.RCloseRec()
			Else
				ValRequired = False
			End If
		End With
		'UPGRADE_NOTE: Object lrecinsValRequired_Interm may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsValRequired_Interm = Nothing
		
		
ValRequired_Err: 
		If Err.Number Then
			ValRequired = False
		End If
		On Error GoTo 0
	End Function
	
	'%LoadTabs: arma la secuencia en código HTML
	Public Function LoadTabs(ByVal nIntermedia As Integer, ByVal nAction As Integer, ByVal sUserSchema As String, ByVal nUsercode As Integer) As String
		Const CN_WINDOWS As String = "AG001   AG003   AG550   AG553   "
		Dim lrecWindows As eRemoteDB.Query
		Dim lclsSecurSche As eSecurity.Secur_sche
		Dim mintPageImage As eFunctions.Sequence.etypeImageSequence
		Dim lintCountWindows As Integer

        Dim lstrCodisp As String = ""
        Dim lstrCodispl As String
        Dim lstrShort_desc As String = ""
        Dim lblnContent As Boolean
		Dim lblnRequired As Boolean
		
		Dim lstrHTMLCode As String
		
		'Dim lclsValues      As eFunctions.Values
		Dim lclsSequence As eFunctions.Sequence
		Dim lclsIntermedia As eAgent.Intermedia
		
		On Error GoTo LoadTabs_Err
		
		lclsSecurSche = New eSecurity.Secur_sche
		lclsSequence = New eFunctions.Sequence
		lrecWindows = New eRemoteDB.Query
		lclsIntermedia = New eAgent.Intermedia
		'Set lclsValues = New eFunctions.Values
		
		lstrHTMLCode = String.Empty
		
		Call ValRequired(nIntermedia)
		
		lstrHTMLCode = lclsSequence.makeTable
		lintCountWindows = 1
		lstrCodispl = Mid(CN_WINDOWS, lintCountWindows, 8)
		Do While Trim(lstrCodispl) <> String.Empty
			
			'+ Se asignan los valores a las variables de requerido
			If Trim(lstrCodispl) = "AG001" Or Trim(lstrCodispl) = "AG003" Then
				lblnRequired = True
			Else
				lblnRequired = False
			End If
			
			If Trim(lstrCodispl) = "AG550" Then
				Call lclsIntermedia.Find(nIntermedia)
				If lclsIntermedia.nIntertyp = 3 Then '+ Tipo de intermediario "CORREDOR"
					lblnRequired = True
				Else
					lblnRequired = False
				End If
			End If
			
			'+ Se asignan los valores a las variables de contenido
			If InStr(1, WithInformation, Trim(lstrCodispl)) <> 0 Then
				lblnContent = True
			Else
				lblnContent = False
			End If
			'+ Se asignan los valores a las variables de descripcion
			
			If lrecWindows.OpenQuery("windows", "sCodisp, sShort_des", "scodispl='" & Trim(lstrCodispl) & "'") Then
				lstrCodisp = lrecWindows.FieldToClass("sCodisp")
				lstrShort_desc = lrecWindows.FieldToClass("sShort_des")
				lrecWindows.CloseQuery()
			End If
			
			
			'+ Se busca la imagen a colocar en los links
			With lclsSecurSche
				If Not .valTransAccess(sUserSchema, lstrCodisp, "1") Then
					If lblnContent Then
						mintPageImage = eFunctions.Sequence.etypeImageSequence.eDeniedOK
					Else
						If lblnRequired Then
							mintPageImage = eFunctions.Sequence.etypeImageSequence.eDeniedReq
						Else
							mintPageImage = eFunctions.Sequence.etypeImageSequence.eDeniedS
						End If
					End If
				Else
					If Not lblnContent Then
						If lblnRequired Then
							mintPageImage = eFunctions.Sequence.etypeImageSequence.eRequired
						Else
							mintPageImage = eFunctions.Sequence.etypeImageSequence.eEmpty
						End If
					Else
						mintPageImage = eFunctions.Sequence.etypeImageSequence.eOK
					End If
				End If
			End With
			
			lstrHTMLCode = lstrHTMLCode & lclsSequence.makeRow(lstrCodisp, lstrCodispl, nAction, lstrShort_desc, mintPageImage)
			'+ Se mueve al siguiente registro encontrado
			lintCountWindows = lintCountWindows + 8
			lstrCodispl = Mid(CN_WINDOWS, lintCountWindows, 8)
		Loop 
		lstrHTMLCode = lstrHTMLCode & lclsSequence.closeTable()
		
		LoadTabs = lstrHTMLCode
		
		'UPGRADE_NOTE: Object lclsSecurSche may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsSecurSche = Nothing
		'UPGRADE_NOTE: Object lrecWindows may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecWindows = Nothing
		'UPGRADE_NOTE: Object lclsIntermedia may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsIntermedia = Nothing
		
		Exit Function
LoadTabs_Err: 
		LoadTabs = "LoadTabs: " & Err.Description
		On Error GoTo 0
	End Function
	
	'%insValAG001_k:Validacion del Encabezado de la secuencia
	Public Function insValAG001_k(ByVal lintAction As Integer, ByVal nIntermed As Integer) As String
		Dim lclsErrors As eFunctions.Errors
		Dim lvalTime As eFunctions.valField
		Dim lclsIntermedia As Intermedia
		Dim lblnCodeError As Boolean
		
		On Error GoTo insValAG001_k_Err
		lclsErrors = New eFunctions.Errors
		lclsIntermedia = New Intermedia
		
		lblnCodeError = False
		
		If nIntermed = 0 Or nIntermed = eRemoteDB.Constants.intNull Then
			'+Se debe introducir el código del intermediario.
			Call lclsErrors.ErrorMessage("AG001_K", 9004)
		Else
			'+Si el campo tiene valor:
			If Not lclsIntermedia.Find(nIntermed) Then
				'+Si la accion no es registrar, el valor debe existir en la tabla de intermediarios.
				If lintAction <> eFunctions.Menues.TypeActions.clngActionadd And lintAction <> eFunctions.Menues.TypeActions.clngActionDuplicate Then
					Call lclsErrors.ErrorMessage("AG001_K", 9002)
				End If
			Else
				'+Si la accion es agregar el codigo no debe existir en la tabla de intermediarios
				If lintAction = eFunctions.Menues.TypeActions.clngActionadd Or lintAction = eFunctions.Menues.TypeActions.clngActionDuplicate Then
					Call lclsErrors.ErrorMessage("AG001_K", 10004)
				End If
			End If
		End If
		
		insValAG001_k = lclsErrors.Confirm
		
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		'UPGRADE_NOTE: Object lclsIntermedia may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsIntermedia = Nothing
		
insValAG001_k_Err: 
		If Err.Number Then
			insValAG001_k = "insValAG001_k: " & Err.Description
		End If
		On Error GoTo 0
	End Function
	
	'% insValAG001:Validacion de la Ventana Ag001
	Public Function insValAG001(ByVal sCodispl As String, ByVal lintAction As Integer, ByVal nIntermed As Integer, ByVal dEffecdate As Date, ByVal nLegal_sch As Integer, ByVal sAll As String, ByVal nInsu_area As Integer, ByVal nAgency As Integer, ByVal nOffice As Integer, ByVal nOfficeAgen As Integer, Optional ByVal sInter_id As String = "", Optional ByVal sClient As String = "", Optional ByVal nIntertyp As Integer = 0, Optional ByVal nSupervis As Integer = 0, Optional ByVal nSup_gen As Integer = 0, Optional ByVal nInt_status As Integer = 0, Optional ByVal nInsu_Assist As Integer = 0, Optional ByVal nInsu_assistlif As Integer = 0, Optional ByVal dInpdate As Date = #12:00:00 AM#, Optional ByVal sValid As String = "") As String
		Dim lclsErrors As eFunctions.Errors
		Dim lvalTime As eFunctions.valField
        Dim lvalValues As eFunctions.Values
        Dim lvalClient As eClient.ValClient
		Dim lclsClient As eClient.Client
		Dim lclsIntermedia As Intermedia
		Dim lclsIntermed_his As Intermed_his
		Dim lclsInterm_typ As Intermedia
		Dim lclsQuery As eRemoteDB.Query
		Dim lclsIntermed_partic As eAgent.Intermed_partic
        Dim lstrMessage As String = ""
        Dim lstrDescInt_typ As String = ""
        Dim lblnSupervis As Boolean
		'- lblnUpdateDate: Esta variable indica si se puede (true) o no (false) modificar algún campo que se encuentre
		'-                 dentro del histórico de intermediarios - ACM - 13/05/2002
		Dim lblnUpdateDate As Boolean
		Dim lblnUpdateDate1 As Boolean
		
		Dim lintIntermediaType As Integer
		Dim lintOffice As Integer
		Dim lintSupervisor As Integer
		Dim lintOldStatus As Integer
		Dim nIntertyp_aux As Integer
		
		On Error GoTo insValAG001_Err
		
		lclsErrors = New eFunctions.Errors
		lvalTime = New eFunctions.valField
		lvalValues = New eFunctions.Values
		lvalClient = New eClient.ValClient
		lclsIntermedia = New Intermedia
		lclsIntermed_his = New Intermed_his
		lclsClient = New eClient.Client
		lclsQuery = New eRemoteDB.Query
		lclsIntermed_partic = New Intermed_partic
		
		lblnSupervis = True
		lblnUpdateDate = True
		lblnUpdateDate1 = True
		
		'+ Validacion del campo sValid.
		'+ Si se coloca valido se verifica que no tenga
		'+ otro tipo de intermediario valido para le mismo RUT
		If sValid = "1" And sClient <> strNull And nIntertyp <> eRemoteDB.Constants.intNull And nIntermed <> eRemoteDB.Constants.intNull Then
			If FindValidIntermediario(nIntermed, sClient, nIntertyp) > 0 Then
				Call lclsErrors.ErrorMessage(sCodispl, 71101)
			End If
		End If
		
		
		'+Validacion del campo Fecha de efecto
		If dEffecdate = dtmNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 21006)
		Else
			If IsDate(dEffecdate) Then
				With lclsIntermed_his
					.nIntermed = nIntermed
					If .ReaLastDateIntermed_his Then
						If dEffecdate <= .dEffecdate And lintAction = 302 Then
							Call lclsErrors.ErrorMessage(sCodispl, 10869,  , eFunctions.Errors.TextAlign.LeftAling, "Fecha de efecto - ")
							lblnUpdateDate = False
						End If
					End If
					If dEffecdate <= Today And lintAction = 302 Then
						Call lclsErrors.ErrorMessage(sCodispl, 10868,  , eFunctions.Errors.TextAlign.LeftAling, "Fecha de efecto - ")
						lblnUpdateDate1 = False
					End If
				End With
			Else
				Call lclsErrors.ErrorMessage(sCodispl, 1001)
			End If
		End If
		
		If Not lblnUpdateDate Then
			If lclsIntermed_his.ReaIntermed_his(nIntermed, dEffecdate) Then
				lintIntermediaType = lclsIntermed_his.nIntertyp
				lintOffice = lclsIntermed_his.nOffice
				lintSupervisor = lclsIntermed_his.nSupervis
			End If
		End If
		
		'+ Área de seguros debe estar lleno
		If (nInsu_area = 0 Or nInsu_area = eRemoteDB.Constants.intNull) And sAll <> "1" Then
			Call lclsErrors.ErrorMessage(sCodispl, 55031)
		End If
		
		'+Validación del campo Cód. Oficial
		If Not sInter_id = String.Empty Then
			If lclsIntermedia.Find_Inter_id(sInter_id) Then
				If lclsIntermedia.nIntermed <> nIntermed Then
					Call lclsErrors.ErrorMessage(sCodispl, 9130)
				End If
			End If
		End If
		
		'+Validación del campo RUT.
		If sClient <> strNull Then
			If lvalClient.Validate(sClient, lintAction) Then
				'+El cliente debe estar registrado dentro de la tabla de clientes.
				If lclsClient.Find(sClient) Then
					lclsInterm_typ = New Intermedia
					
					'+Si el cliente ya se encuentra asociado a otro intermediario se envía la advertencia.
					If lclsInterm_typ.Find_ClientInter(sClient) Then
						If lclsInterm_typ.nIntermed <> nIntermed Then
							Call lclsErrors.ErrorMessage(sCodispl, 60381)
						End If
					End If
					
					'+Si el cliente ya se encuentra asociado a otro intermediario mas de una vez para la misma compañía, se envía la advertencia.
					
					If lclsQuery.OpenQuery("Interm_typ", "sDescript", "nInterTyp =" & nIntertyp) Then
						lstrDescInt_typ = lclsQuery.FieldToClass("sDescript")
                        lstrMessage = eFunctions.Values.GetMessage(816)
                    End If
					
					If lclsInterm_typ.FindTypeInterm_Client(sClient, nIntertyp) Then
						If lclsInterm_typ.nIntermed <> nIntermed Then
							
							'+ Se valida que intermediario trabaja para la compañía de Vida
							If nInsu_area = CDbl("1") And lclsInterm_typ.sLife = "1" And (lclsInterm_typ.nInt_status = CDbl("2") Or lclsInterm_typ.nInt_status = CDbl("4")) Then
								Call lclsErrors.ErrorMessage(sCodispl, 60831,  , eFunctions.Errors.TextAlign.RigthAling, lstrDescInt_typ & " " & lstrMessage & " " & sClient)
							End If
							'+ Se valida que intermediario trabaja para la compañía de Generales
							If nInsu_area = CDbl("2") And lclsInterm_typ.sNonlife = "1" And (lclsInterm_typ.nInt_status = CDbl("2") Or lclsInterm_typ.nInt_status = CDbl("4")) Then
								Call lclsErrors.ErrorMessage(sCodispl, 60831,  , eFunctions.Errors.TextAlign.RigthAling, lstrDescInt_typ & " " & lstrMessage & " " & sClient)
							End If
						End If
					End If
					
					
					'UPGRADE_NOTE: Object lclsInterm_typ may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					lclsInterm_typ = Nothing
					If lclsClient.dDeathdat <> dtmNull Then
						Call lclsErrors.ErrorMessage(sCodispl, 2051)
					End If
				Else
					Call lclsErrors.ErrorMessage(sCodispl, 1007)
				End If
			Else
				'+Si el código del intermediario no cumple con el formato indicado para el código de cliente
				Select Case lvalClient.Status
					Case eClient.ValClient.eTypeValClientErr.StructInvalid
						Call lclsErrors.ErrorMessage(sCodispl, 2012)
					Case eClient.ValClient.eTypeValClientErr.TypeNotFound
						Call lclsErrors.ErrorMessage(sCodispl, 2013)
					Case eClient.ValClient.eTypeValClientErr.FieldEmpty
						Call lclsErrors.ErrorMessage(sCodispl, 2228)
				End Select
			End If
			
		Else
			'+El código del cliente no puede estar sin contenido.
			Call lclsErrors.ErrorMessage(sCodispl, 2001)
		End If
		
		'+Validación del campo Agencia.
		If nAgency = eRemoteDB.Constants.intNull Or nAgency = 0 Then
			Call lclsErrors.ErrorMessage(sCodispl, 55522)
		End If
		
		'+Validación del campo Sucursal.
		If nOffice = eRemoteDB.Constants.intNull Or nOffice = 0 Then
			Call lclsErrors.ErrorMessage(sCodispl, 1040)
		Else
			If Not lblnUpdateDate And lintAction = 302 And nOffice <> lintOffice Then
				Call lclsErrors.ErrorMessage(sCodispl, 10869,  , eFunctions.Errors.TextAlign.LeftAling, "Sucursal - ")
			End If
			If Not lblnUpdateDate1 And lintAction = 302 And nOffice <> lintOffice Then
				Call lclsErrors.ErrorMessage(sCodispl, 10868,  , eFunctions.Errors.TextAlign.LeftAling, "Sucursal - ")
			End If
			
		End If
		
		'+Validación del campo Sucursal.
		If nOfficeAgen = eRemoteDB.Constants.intNull Or nOfficeAgen = 0 Then
			Call lclsErrors.ErrorMessage(sCodispl, 55523)
		End If
		
		'+Validación del campo Tipo de intermediario
		If nIntertyp = eRemoteDB.Constants.intNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 10095)
		Else
			If Not lblnUpdateDate And lintAction = 302 And nIntertyp <> lintIntermediaType Then
				Call lclsErrors.ErrorMessage(sCodispl, 10869,  , eFunctions.Errors.TextAlign.LeftAling, "Tipo - ")
			End If
			If Not lblnUpdateDate1 And lintAction = 302 And nIntertyp <> lintIntermediaType Then
				Call lclsErrors.ErrorMessage(sCodispl, 10868,  , eFunctions.Errors.TextAlign.LeftAling, "Tipo - ")
			End If
			
			'+ Validación de Intermediaro con personal a su Cargo
			If Find(nSupervis) Then
				nIntertyp_aux = Me.nIntertyp
				If lintAction = 302 And nIntertyp_aux <> 5 And nIntertyp_aux <> 11 And nIntertyp_aux < 50 Then
					If Find_Supervis_v(nSupervis) Then
						Call lclsErrors.ErrorMessage(sCodispl, 55870)
					End If
				End If
			End If
		End If
		
		'+Validación del campo Supervisor de Generales.
		If (nSup_gen = eRemoteDB.Constants.intNull Or nSup_gen = 0) And (sAll = "1" Or nInsu_area = CDbl("1")) Then
			Call lclsErrors.ErrorMessage(sCodispl, 9016,  ,  , "- Área de Generales")
		Else
			'+Este valor debe existir en la tabla de intermediarios.
			If nSup_gen <> eRemoteDB.Constants.intNull And nSup_gen <> 0 Then
				If Not lclsIntermedia.findIntermediaClient(nSup_gen, 50, dInpdate) Then
					lblnSupervis = False
				Else
					lblnSupervis = True
				End If
				If Not lblnSupervis Then
					If Not lclsIntermedia.findIntermediaClient(nSup_gen, 51, dInpdate) Then
						lblnSupervis = False
					Else
						lblnSupervis = True
					End If
				End If
				If Not lblnSupervis Then
					If Not lclsIntermedia.findIntermediaClient(nSup_gen, 52, dInpdate) Then
						lblnSupervis = False
					Else
						lblnSupervis = True
					End If
				End If
				
				If Not lblnSupervis Then
					If Not lclsIntermedia.findIntermediaClient(nSup_gen, 5, dInpdate) Then
						lblnSupervis = False
					Else
						lblnSupervis = True
					End If
				End If
				If Not lblnSupervis Then
					If Not lclsIntermedia.findIntermediaClient(nSup_gen, 11, dInpdate) Then
						lblnSupervis = False
					Else
						lblnSupervis = True
					End If
				End If
				
				If Not lblnSupervis Then
					Call lclsErrors.ErrorMessage(sCodispl, 9017)
				Else
					If lclsIntermedia.nInt_status <> 1 Then
						Call lclsErrors.ErrorMessage(sCodispl, 9114,  , eFunctions.Errors.TextAlign.LeftAling, "Supervisor - ")
					End If
				End If
			End If
		End If
		
		'+Validación del campo Supervisor de Vida.
		
		If (nSupervis = eRemoteDB.Constants.intNull Or nSupervis = 0) And (sAll = "1" Or nInsu_area = CDbl("2")) Then
			Call lclsErrors.ErrorMessage(sCodispl, 9016,  ,  , "- Área de Vida")
		Else
			
			'+Este valor debe existir en la tabla de intermediarios.
			
			If nSupervis <> eRemoteDB.Constants.intNull And nSupervis <> 0 Then
				If Not lclsIntermedia.findIntermediaClient(nSupervis, 50, dInpdate) Then
					lblnSupervis = False
				Else
					lblnSupervis = True
				End If
				If Not lblnSupervis Then
					If Not lclsIntermedia.findIntermediaClient(nSupervis, 51, dInpdate) Then
						lblnSupervis = False
					Else
						lblnSupervis = True
					End If
				End If
				If Not lblnSupervis Then
					If Not lclsIntermedia.findIntermediaClient(nSupervis, 52, dInpdate) Then
						lblnSupervis = False
					Else
						lblnSupervis = True
					End If
				End If
				
				If Not lblnSupervis Then
					If Not lclsIntermedia.findIntermediaClient(nSupervis, 5, dInpdate) Then
						lblnSupervis = False
					Else
						lblnSupervis = True
					End If
				End If
				If Not lblnSupervis Then
					If Not lclsIntermedia.findIntermediaClient(nSupervis, 11, dInpdate) Then
						lblnSupervis = False
					Else
						lblnSupervis = True
					End If
				End If
				
				
				If Not lblnSupervis Then
					Call lclsErrors.ErrorMessage(sCodispl, 9017)
				Else
					If lclsIntermedia.nInt_status <> 1 Then
						Call lclsErrors.ErrorMessage(sCodispl, 9114,  , eFunctions.Errors.TextAlign.LeftAling, "Supervisor - ")
					End If
				End If
			End If
		End If
		
		'+ Superisor para vida no puede ser igual al Supervisor para generales.
		
		If (nSupervis <> eRemoteDB.Constants.intNull And nSupervis <> 0) And (nSup_gen <> eRemoteDB.Constants.intNull And nSup_gen <> 0) Then
			If nSupervis = nSup_gen Then
				Call lclsErrors.ErrorMessage(sCodispl, 55934)
			End If
		End If
		
		'+Validación del campo Asistente de seguros de vida.
		If nInsu_assistlif <> eRemoteDB.Constants.intNull And nInsu_assistlif <> 0 Then
			'+Este valor debe existir en la tabla de intermediarios.
			If Not lclsIntermedia.findIntermediaClient(nInsu_assistlif, 9, dInpdate) Then
				Call lclsErrors.ErrorMessage(sCodispl, 55515,  ,  , "- Área de Vida")
			Else
				If lclsIntermedia.nInt_status <> 1 Then
					Call lclsErrors.ErrorMessage(sCodispl, 9114,  , eFunctions.Errors.TextAlign.LeftAling, "Asistente - ")
				End If
			End If
		End If
		
		'+Validación del campo Asistente de seguros generales.
		If nInsu_Assist <> eRemoteDB.Constants.intNull And nInsu_Assist <> 0 Then
			'+Este valor debe existir en la tabla de intermediarios.
			If Not lclsIntermedia.findIntermediaClient(nInsu_Assist, 9, dInpdate) Then
				Call lclsErrors.ErrorMessage(sCodispl, 55515,  ,  , "- Área de Generales")
			Else
				If lclsIntermedia.nInt_status <> 1 Then
					Call lclsErrors.ErrorMessage(sCodispl, 9114,  , eFunctions.Errors.TextAlign.LeftAling, "Asistente - ")
				End If
			End If
		End If
		
		
		'+Validación del campo Estado.
		If nInt_status = eRemoteDB.Constants.intNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 9022)
		Else
			'+ Se envía el mensaje de advertencia que indica que el cliente ya existe como intermediario activo
			If nInt_status = 1 Then
				If lclsIntermedia.Find(nIntermed) Then
					
					If nInt_status <> lclsIntermedia.nInt_status Then
						Call lclsErrors.ErrorMessage(sCodispl, 55516)
					End If
					
					If (lclsIntermedia.nInt_status = 4 And lclsIntermedia.nNullcode = 1) Then
						If Not lclsIntermed_partic.Find(nIntermed) Then
							Call lclsErrors.ErrorMessage(sCodispl, 55517)
						End If
					End If
					
				End If
			End If
		End If
		
		If dInpdate = dtmNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 9013)
		End If
		
		'+ Validación del campo Régimen Tributario
		
		If nLegal_sch = 0 Or nLegal_sch = eRemoteDB.Constants.intNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 60377)
		End If
		
		Dim lobjCommision As Object
		If lclsIntermedia.Find(nIntermed) Then
			'+ Se valida que si se esta cambiando el tipo de intermediario de "Agente"(directo o libre) a "Supervisor",
			'+ se haya realizado el traspaso de cartera.
			If (lclsIntermedia.nIntertyp = 1 Or lclsIntermedia.nIntertyp = 10) And (nIntertyp = 50 Or nIntertyp = 51 Or nIntertyp = 52 Or nIntertyp = 5 Or nIntertyp = 11) Then
				If FindTrasPol(nIntermed, dEffecdate) Then
					Call lclsErrors.ErrorMessage(sCodispl, 60382)
				End If
			End If
			'+ Se valida que no se cambie el tipo de intermediario si tiene pólizas asociadas al tipo anterior
			If lclsIntermedia.nIntertyp <> nIntertyp Then
				lobjCommision = eRemoteDB.NetHelper.CreateClassInstance("ePolicy.Commission")
				If lobjCommision.Find_Commintermedia(nIntermed, dEffecdate) Then
					Call lclsErrors.ErrorMessage(sCodispl, 55124)
				End If
				'UPGRADE_NOTE: Object lobjCommision may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
				lobjCommision = Nothing
			End If
		End If
		
		'+ Se valida que si se esta cambiando el tipo de intermediario de Supervisor a
		'+ "Agente"(directo o libre).
		
		If lclsIntermedia.Find(nIntermed) Then
			If (lclsIntermedia.nIntertyp = 50 Or lclsIntermedia.nIntertyp = 51 Or lclsIntermedia.nIntertyp = 52 Or lclsIntermedia.nIntertyp = 5 Or lclsIntermedia.nIntertyp = 11) And (nIntertyp = 1 Or nIntertyp = 10) Then
				
				If FindTrasPol(nIntermed, dEffecdate) Then
					Call lclsErrors.ErrorMessage(sCodispl, 60382)
				End If
			End If
		End If
		
		
		'+ Se valida que NO se cambie el estado actual del intermediario a
		'+ "EN PROCESO DE INSTALACIÓN" - ACM - 19/06/2001
		If nInt_status <> 0 And nInt_status <> eRemoteDB.Constants.intNull And nInt_status = 3 Then
			If lclsIntermedia.Find(nIntermed) Then
				If nInt_status <> lclsIntermedia.nInt_status Then
					Call lclsErrors.ErrorMessage(sCodispl, 9023)
				End If
			End If
		End If
		
		'+ Se valida que la fecha de efecto sea mayor o igual a la fecha de ingreso del
		'+ intermediario sólo cuando se está registrando un intermediario nuevo
		'+ Validación #60430 - ACM - 21/05/2002
		If dEffecdate < dInpdate And lintAction = 301 Then
			Call lclsErrors.ErrorMessage(sCodispl, 60430)
		End If
		
		If nAgency <> eRemoteDB.Constants.intNull And nAgency > 0 Then
			If Not lclsQuery.OpenQuery("Agencies", "nAgency", "nAgency=" & nAgency) Then
				Call lclsErrors.ErrorMessage(sCodispl, 60433)
			End If
		End If
		
		If nOfficeAgen <> eRemoteDB.Constants.intNull And nOfficeAgen > 0 Then
			If Not lclsQuery.OpenQuery("Agencies", "nofficeAgen", "nOfficeAgen=" & nOfficeAgen) Then
				Call lclsErrors.ErrorMessage(sCodispl, 60432)
			End If
		End If
		
		
		insValAG001 = lclsErrors.Confirm
		
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		'UPGRADE_NOTE: Object lvalTime may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lvalTime = Nothing
		'UPGRADE_NOTE: Object lvalClient may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lvalClient = Nothing
		'UPGRADE_NOTE: Object lclsIntermedia may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsIntermedia = Nothing
		'UPGRADE_NOTE: Object lclsClient may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsClient = Nothing
		'UPGRADE_NOTE: Object lclsIntermed_his may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsIntermed_his = Nothing
		'UPGRADE_NOTE: Object lvalValues may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lvalValues = Nothing
		'UPGRADE_NOTE: Object lclsQuery may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsQuery = Nothing
		
insValAG001_Err: 
		If Err.Number Then
			insValAG001 = "insValAG001: " & Err.Description
		End If
		On Error GoTo 0
	End Function
	
	'%insPostHeader: Esta función se encarga de validar los datos introducidos en la zona de
	'%cabecera.
	Public Function insPostAG001_k(ByVal lintAction As Integer, ByVal nIntermed As Integer, ByVal nUsercode As Integer) As Boolean
		insPostAG001_k = True
		
		On Error GoTo insPostAG001_k_Err
		
		'+Si la opción seleccionada es Registrar o Duplicar
		If lintAction = eFunctions.Menues.TypeActions.clngActionDuplicate Or lintAction = eFunctions.Menues.TypeActions.clngActionadd Then
			Me.nIntermed = nIntermed
			Me.nUsercode = nUsercode
			Me.sValid = "1"
			Me.nInt_status = 3
			insPostAG001_k = Add
		End If
insPostAG001_k_Err: 
		If Err.Number Then
			insPostAG001_k = CBool("insPostAG001_k: " & Err.Description)
		End If
		On Error GoTo 0
	End Function
	
	'*InsPostAG001: Esta funcion se encarga de crear/actualizar los registros
	'*correspondientes en la tabla de Intermediarios
	Public Function InsPostAG001(ByVal nIntermed As Integer, ByVal sClient As String, ByVal dEffecdate As Date, ByVal dEffecdate_Old As Date, ByVal dInpdate As Date, ByVal nInt_status As Integer, ByVal nIntertyp As Integer, ByVal nNullcode As Integer, ByVal dNulldate As Date, ByVal nOffice As Integer, ByVal nOfficeAgen As Integer, ByVal nAgency As Integer, ByVal nSupervis As Integer, ByVal nUsercode As Integer, ByVal sInter_id As String, ByVal sAll As String, ByVal nInsu_area As Integer, ByVal nLegal_sch As Integer, ByVal nSup_gen As Integer, ByVal nInsu_Assist As Integer, ByVal nInsu_assistlif As Integer, ByVal sValid As String) As Boolean
		Dim lclsIntermedia As eAgent.Intermedia
		Dim lclsIntermed_his As eAgent.Intermed_his
		Dim lclsIntermed_partic As eAgent.Intermed_partic
		
		lclsIntermedia = New eAgent.Intermedia
		lclsIntermed_his = New eAgent.Intermed_his
		
		On Error GoTo InsPostAG001_err
		
		With lclsIntermedia
			.nIntermed = nIntermed
			.sClient = sClient
			.dInpdate = dInpdate
			.nInt_status = nInt_status
			.nNullcode = nNullcode
			.dNulldate = dNulldate
			.nOffice = nOffice
			.nSupervis = nSupervis
			.nUsercode = nUsercode
			.sInter_id = sInter_id
			
			If sAll = "1" Then
				.sLife = "1"
				.sNonlife = "1"
			Else
				Select Case nInsu_area
					'+ Si se trata de ramos de VIDA
					Case Is = 1
						.sLife = "2"
						.sNonlife = "1"
						'+ Si se trata de ramos GENERALES
					Case Is = 2
						.sLife = "1"
						.sNonlife = "2"
				End Select
			End If
			
			.nLegal_sch = nLegal_sch
			.nSup_gen = nSup_gen
			.nInsu_Assist = nInsu_Assist
			.nInsu_assistlif = nInsu_assistlif
			.nOfficeAgen = nOfficeAgen
			.nAgency = nAgency
			.sValid = sValid
			
			'+ Se valida que si el tipo actual es "Corredor" y se cambia
			'+ Se debe borrar la información de la tabla "intermed_partic"
			If Me.Find(nIntermed) Then
				If nIntertyp <> Me.nIntertyp Then
					lclsIntermed_partic = New eAgent.Intermed_partic
					lclsIntermed_partic.nIntermed = nIntermed
					Call lclsIntermed_partic.Delete()
				End If
			End If
			.nIntertyp = nIntertyp
			InsPostAG001 = .Add
		End With
		
		With lclsIntermed_his
			.nIntermed = nIntermed
			.dEffecdate = dEffecdate
			.dEffecdate_Old = dEffecdate_Old
			.nIntertyp = nIntertyp
			.nNullcode = nNullcode
			.nIntertyp = nIntertyp
			.nOffice = nOffice
			.nSupervis = nSupervis
			.nUsercode = nUsercode
			.nInt_status = nInt_status
			InsPostAG001 = .Add
		End With
		
		
		'    If pdmtInputdate <> tcdInputDate.Value Then
		'        Call DuplicateRegister
		'        pdmtInputdate = tcdInputDate.Value
		'    End If
		
		'UPGRADE_NOTE: Object lclsIntermedia may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsIntermedia = Nothing
InsPostAG001_err: 
		If Err.Number Then
			InsPostAG001 = False
		End If
		On Error GoTo 0
	End Function
	
	'% GetNewIntermediaCode: obtiene el nuevo número del intermediario.
	Public Function GetNewIntermediaCode(ByVal nUsercode As Integer) As Integer
		Dim lclsGeneral As eGeneral.GeneralFunction
		
		On Error GoTo GetNewIntermediaCode_Err
		lclsGeneral = New eGeneral.GeneralFunction
		GetNewIntermediaCode = lclsGeneral.Find_Numerator(30, 0, nUsercode)
		
GetNewIntermediaCode_Err: 
		If Err.Number Then
			GetNewIntermediaCode = 0
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsGeneral may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsGeneral = Nothing
	End Function
	
	'%insValAG003: Realiza las validaciones de rigor de la AG003
	Public Function insValAG003(ByVal sCodispl As String, ByVal nAction As Integer, ByVal nIntermed As Integer, Optional ByVal dEffecdate As Date = #12:00:00 AM#) As String
		Dim lclsErrors As eFunctions.Errors
		Dim lclsCommis_his As eAgent.commis_his
		Dim lclsIntermedia As eAgent.Intermedia
		
		lclsIntermedia = New eAgent.Intermedia
		lclsErrors = New eFunctions.Errors
		lclsCommis_his = New eAgent.commis_his
		
		On Error GoTo insValAG003_Err
		
		If dEffecdate = dtmNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 21006)
		Else
			'+Validacion del campo Fecha de efecto
			If nAction = eFunctions.Menues.TypeActions.clngActionUpdate Then
				If IsDate(dEffecdate) Then
					With lclsCommis_his
						.nIntermed = nIntermed
						.dEffecdate = dEffecdate
						If lclsCommis_his.ReaLastDateCommis_his Then
							If dEffecdate <= .dEffecdate Then
                                Call lclsErrors.ErrorMessage(sCodispl, 4210, , eFunctions.Errors.TextAlign.RigthAling, "(" & .dEffecdate.ToString() & ")")
							End If
						Else
							With lclsIntermedia
								If .Find(nIntermed) Then
									If dEffecdate < .dInpdate Then
										Call lclsErrors.ErrorMessage(sCodispl, 7150)
									End If
								End If
							End With
						End If
					End With
				Else
					Call lclsErrors.ErrorMessage(sCodispl, 1001)
				End If
			End If
		End If
		
		insValAG003 = lclsErrors.Confirm
		
insValAG003_Err: 
		If Err.Number Then
			insValAG003 = "insValAG003: " & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsIntermedia may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsIntermedia = Nothing
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		'UPGRADE_NOTE: Object lclsCommis_his may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsCommis_his = Nothing
		
	End Function
	
	'% insValCer_Intermed: Verifica que existan certificados vigentes para un intermediario.
	Function insValCer_Intermed(ByVal nIntermed As Integer, ByVal dNulldate As Date) As Boolean
		Dim lrecreaIntermedia As eRemoteDB.Execute
		
		On Error GoTo insValCer_Intermed_Err
		
		lrecreaIntermedia = New eRemoteDB.Execute
		
		With lrecreaIntermedia
			.StoredProcedure = "ValCer_Intermed"
			.Parameters.Add("nIntermed", nIntermed, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dnulldate", CDate(dNulldate), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				If .FieldToClass("LCOUNT") > 0 Then
					insValCer_Intermed = True
				Else
					insValCer_Intermed = False
				End If
				.RCloseRec()
			Else
				insValCer_Intermed = False
			End If
		End With
		'UPGRADE_NOTE: Object lrecreaIntermedia may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaIntermedia = Nothing
		
insValCer_Intermed_Err: 
		If Err.Number Then
			insValCer_Intermed = False
		End If
		On Error GoTo 0
	End Function
	
	'%insValAG011_K: Esta función se encarga de validar los datos introducidos en la cabecera de la
	'%forma.
	Public Function insValAG011_K(ByVal nIntermed As Integer, ByVal nOptStatus As Integer) As String
		Dim lclsErrors As eFunctions.Errors
		Dim lclsIntermedia As eAgent.Intermedia
		
		lclsIntermedia = New eAgent.Intermedia
		lclsErrors = New eFunctions.Errors
		
		On Error GoTo insValAG011_K_Err
		
		insValAG011_K = CStr(True)
		
		'+Validacion del código del intermediario.
		
		If nIntermed = eRemoteDB.Constants.intNull Or nIntermed = 0 Then
			'+Se debe introducir el código del intermediario.
			Call lclsErrors.ErrorMessage("AG0011", 9004)
		Else
			'+Si el campo tiene valor:
			With lclsIntermedia
				If .Find(nIntermed) Then
					If nOptStatus = 2 Then '+ Anulación
						If ((.nInt_status = 2) Or (.nInt_status = 3)) Then '+ Anulado o En proceso de instalación
							Call lclsErrors.ErrorMessage("AG0011", 9115)
						End If
					ElseIf nOptStatus = 4 Then  '+ Suspensión
						If ((.nInt_status = 4) Or (.nInt_status = 3) Or (.nInt_status = 2)) Then '+ Suspendido, En proceso de instalación o Anulado
							Call lclsErrors.ErrorMessage("AG0011", 9115)
						End If
					End If
				Else
					Call lclsErrors.ErrorMessage("AG0011", 9002)
				End If
			End With
		End If
		
		insValAG011_K = lclsErrors.Confirm
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		'UPGRADE_NOTE: Object lclsIntermedia may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsIntermedia = Nothing
		
insValAG011_K_Err: 
		If Err.Number Then
			insValAG011_K = "insValAG011_K: " & Err.Description
		End If
		On Error GoTo 0
	End Function
	'*insValAG011: Esta función valida los valores de los campos de la forma
	Public Function insValAG011(ByVal nIntermed As Integer, ByVal dNulldate As Date, ByVal nNullcode As Integer, ByVal nInt_status As Integer, ByVal dInpdate As Date, ByVal nCircular_doc As Integer) As String
		Dim lclsErrors As eFunctions.Errors
		Dim lvaldate As eFunctions.valField
		Dim lclsIntermedia As eAgent.Intermedia
		Dim lclsIntermed_his As eAgent.Intermed_his
		
		lclsErrors = New eFunctions.Errors
		lvaldate = New eFunctions.valField
		lclsIntermedia = New eAgent.Intermedia
		lclsIntermed_his = New eAgent.Intermed_his
		
		On Error GoTo insValAG011_Err
		
		insValAG011 = CStr(True)
		
		'+Se realiza la validacion del campo Fecha de Egreso
		lvaldate.objErr = lclsErrors
		
		lvaldate.ErrEmpty = 9013
		If lvaldate.ValDate(dNulldate,  , eFunctions.valField.eTypeValField.onlyvalid) Then
			
			If dNulldate <= dInpdate Then
				Call lclsErrors.ErrorMessage("AG011", 9006)
			End If
			
			With lclsIntermed_his
				.nIntermed = nIntermed
				If .ReaLastDateIntermed_his Then
					If dNulldate <= .dEffecdate Then
						Call lclsErrors.ErrorMessage("AG011", 10868)
					End If
					If .dEffecdate <= Today Then
						If dNulldate <= Today Then
							Call lclsErrors.ErrorMessage("AG011", 1964)
						End If
					End If
				Else
					If dNulldate <= Today Then
						Call lclsErrors.ErrorMessage("AG011", 1964)
					End If
				End If
			End With
			
			If (nInt_status <> 0 And nInt_status <> eRemoteDB.Constants.intNull And nInt_status = 2) Then 'Anulación
				With lclsIntermedia
					If .insValCer_Intermed(nIntermed, dNulldate) Then
						Call lclsErrors.ErrorMessage("AG011", 55594)
					End If
				End With
			End If
		End If
		
		'+Se realiza la validacion del campo  Causa de egreso.
		'+El codigo de anulación es obligatorio.
		If nNullcode = eRemoteDB.Constants.intNull Or nNullcode = 0 Then
			Call lclsErrors.ErrorMessage("AG011", 9101)
		End If
		
		'+Si el intermediario es corredor, se valida que el campo Número de circular este lleno.
		With lclsIntermedia
			If .Find(nIntermed) Then
				
				'+Si el intermediario es "Corredor"
				If .nIntertyp = 3 Then
					If nCircular_doc = eRemoteDB.Constants.intNull Or nCircular_doc = 0 Then
						Call lclsErrors.ErrorMessage("AG011", 55563)
					End If
				End If
			End If
		End With
		
		insValAG011 = lclsErrors.Confirm
		
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		'UPGRADE_NOTE: Object lvaldate may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lvaldate = Nothing
		'UPGRADE_NOTE: Object lclsIntermedia may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsIntermedia = Nothing
		'UPGRADE_NOTE: Object lclsIntermed_his may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsIntermed_his = Nothing
		
insValAG011_Err: 
		If Err.Number Then
			insValAG011 = "insValAG011: " & Err.Description
		End If
		On Error GoTo 0
	End Function
	'%insValAGC006: Esta función se encarga de validar los datos introducidos en la zona de detalle para
	'%forma.
	Public Function insValAGC006(ByVal sCodispl As String, ByVal nType As Integer, ByVal sClient As String, ByVal sAgent As String, ByVal sNameAgent As String, ByVal sOrgAgent As String, ByVal sOrgAgentName As String, ByVal nOffice As Integer, ByVal nState As Integer, ByVal tcdDateAnull As Date, ByVal tcdCommidate As Date) As String
        Dim lclsErrors As eFunctions.Errors = New eFunctions.Errors
        Dim lblError As Boolean
		
		insValAGC006 = String.Empty
		lblError = False
		
		'+ Verifica que exista por lo menos una condición de búsqueda
		If nType <= 0 And Trim(sAgent) = String.Empty And Trim(sClient) = String.Empty And Trim(sOrgAgent) = String.Empty And nOffice <= 0 And nState <= 0 And tcdDateAnull = dtmNull And tcdCommidate = dtmNull Then
			
			lclsErrors = New eFunctions.Errors
			
			Call lclsErrors.ErrorMessage(sCodispl, 99022)
			
			lblError = True
		End If
		
		If lblError Then
			insValAGC006 = lclsErrors.Confirm
			'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			lclsErrors = Nothing
		End If
		
insValAGC006_Err: 
		If Err.Number Then
			insValAGC006 = "insValAGC006: " & Err.Description
		End If
		On Error GoTo 0
	End Function
	
	'**% insValAGL001_K: validate the header section of the page AGL001 as described in the
	'**% functional specifications
	'% InsValAGL001_K: Este metodo se encarga de realizar las validaciones del encabezado (Header)
	'% descritas en el funcional de la ventana AGL001
	Public Function insValAGL001_k(ByVal sCodispl As String, ByVal dInitDate As Date, ByVal dEnddate As Date) As String
		Dim lclsErrors As eFunctions.Errors
		Dim lclsValField As eFunctions.valField
		lclsErrors = New eFunctions.Errors
		lclsValField = New eFunctions.valField
		lclsValField.objErr = lclsErrors
		
		On Error GoTo insValAGL001_k_Err
		
		'**+ Validation of the Final Date
		'+Validación de la Fecha Final
		
		lclsValField.ErrEmpty = 9072
		If lclsValField.ValDate(dEnddate,  , eFunctions.valField.eTypeValField.ValAll) Then
			If dEnddate < dInitDate Then
				lclsErrors.ErrorMessage(sCodispl, 3240)
			End If
		End If
		
		insValAGL001_k = lclsErrors.Confirm
		
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		'UPGRADE_NOTE: Object lclsValField may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsValField = Nothing
		
insValAGL001_k_Err: 
		If Err.Number Then
			insValAGL001_k = "insValAGL001_k: " & Err.Description
		End If
		On Error GoTo 0
	End Function
	Public Function insValAG781(ByVal sCodispl As String, ByVal nIntermedOld As Integer, ByVal nInsur_area As Integer, ByVal nIntermedNew As Integer) As String
		Dim lclsErrors As eFunctions.Errors
		lclsErrors = New eFunctions.Errors

        Dim sLifeOld As Object = New Object
        Dim sNonLifeOld As Object = New Object
        Dim sLifeNew As Object = New Object
        Dim sNonLifeNew As Object = New Object


        On Error GoTo insValAG781_Err
		
		If nInsur_area = eRemoteDB.Constants.intNull Then
			lclsErrors.ErrorMessage(sCodispl, 55031)
		End If
		
		If nIntermedOld = eRemoteDB.Constants.intNull Then
			lclsErrors.ErrorMessage(sCodispl, 55665,  , eFunctions.Errors.TextAlign.RigthAling, "(Supervisor/Asistente a sustituir)")
		Else
			'+Verifica que Intermediario tenga al menos un supervisado/asistido
			If Not Find_Supervis_v(nIntermedOld) Then
				lclsErrors.ErrorMessage(sCodispl, 60480)
			Else
				'+Verifica que Intermediario, corresponda al área especidicada
				If Find(nIntermedOld) Then
					If nInsur_area = 2 Then
						sLifeOld = Me.sLife
						If Me.sLife <> "1" Then
							lclsErrors.ErrorMessage(sCodispl, 60575,  , eFunctions.Errors.TextAlign.RigthAling, "(a sustituir)")
						End If
					End If
					
					If nInsur_area = 1 Then
						sNonLifeOld = Me.sNonlife
						If Me.sNonlife <> "1" Then
							lclsErrors.ErrorMessage(sCodispl, 60575,  , eFunctions.Errors.TextAlign.RigthAling, "(a sustituir)")
						End If
					End If
					
					If nIntermedNew = eRemoteDB.Constants.intNull Then
						lclsErrors.ErrorMessage(sCodispl, 55665,  , eFunctions.Errors.TextAlign.RigthAling, "(Supervisor/Asistente nuevo)")
					Else
						If Find(nIntermedNew) Then
							'+Se verifica que el status del area sea activo
							If Me.nInt_status <> 1 Then
								lclsErrors.ErrorMessage(sCodispl, 60489)
							End If
							'+Se verifica que supervisor/asistente, pertenezca al area ingresada (Vida)
							If nInsur_area = 2 Then
								sLifeNew = Me.sLife
								If Me.sLife <> "1" Then
									lclsErrors.ErrorMessage(sCodispl, 60575,  , eFunctions.Errors.TextAlign.RigthAling, "(nuevo)")
								End If
							End If
							'+Se verifica que supervisor/asistente, pertenezca al area ingresada (Generales)
							If nInsur_area = 1 Then
								sNonLifeNew = Me.sNonlife
								If Me.sNonlife <> "1" Then
									lclsErrors.ErrorMessage(sCodispl, 60575,  , eFunctions.Errors.TextAlign.RigthAling, "(nuevo)")
								End If
							End If
							
							'+ Se compara si los intermediarios pertenecen a la misma área
							If nInsur_area = 1 Then
								If sNonLifeNew <> sNonLifeOld Then
									lclsErrors.ErrorMessage(sCodispl, 60574)
								End If
							Else
								If sLifeNew <> sLifeOld Then
									lclsErrors.ErrorMessage(sCodispl, 60574)
								End If
							End If
							
						End If
						
					End If
				End If
				
			End If
		End If
		
		insValAG781 = lclsErrors.Confirm
		
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		
insValAG781_Err: 
		If Err.Number Then
			insValAG781 = "insValAG781: " & Err.Description
		End If
		On Error GoTo 0
	End Function
	'%insPostAGL618: Método que realiza el proceso de cálculo de incentivos de agentes de mantención
	Public Function insPostAG781(ByVal nIntertyp As Integer, ByVal nIntermedOld As Integer, ByVal nInsur_area As Integer, ByVal nIntermedNew As Integer, ByVal nUsercode As Integer) As Boolean
		Dim lintAGL618 As eRemoteDB.Execute
		
		lintAGL618 = New eRemoteDB.Execute
		
		On Error GoTo insPostAG781_Err
		
		With lintAGL618
			.StoredProcedure = "UpdSupIntermed"
			.Parameters.Add("nInsur_area", nInsur_area, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nSupIntnew", nIntermedNew, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nSupIntold", nIntermedOld, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInterTyp", nIntertyp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(False) Then
				insPostAG781 = True
			Else
				insPostAG781 = False
			End If
		End With
		
insPostAG781_Err: 
		If Err.Number Then
			insPostAG781 = False
		End If
		'UPGRADE_NOTE: Object lintAGL618 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lintAGL618 = Nothing
		On Error GoTo 0
	End Function
	'*InsPostAG003(): Función que hace el llamado a los métodos de inserción y actualización  sobre las tablas afectadas
	Public Function InsPostAG003(ByVal nIntermed As Integer, ByVal sCol_agree As String, ByVal sAgreeInt As String, ByVal nComtab_Lif As Integer, ByVal nComtab_gen As Integer, ByVal nComtab_ExComm As Integer, ByVal nComtab_EcoSche As Integer, ByVal nSlc_Tab_nr As Integer, ByVal nLife_sche As Integer, ByVal nGen_sche As Integer, ByVal nGoal_life As Integer, ByVal nGoal_gen As Integer, ByVal dEffecdate As Date, ByVal dEffecdate_Old As Date, ByVal nUsercode As Integer) As Boolean
		Dim lclsCommis_his As commis_his
		Dim lclsGoals As Goals
		
		lclsCommis_his = New commis_his
		lclsGoals = New Goals
		
		On Error GoTo InsPostAG003_err
		
		If nComtab_Lif <> eRemoteDB.Constants.intNull Then
			With lclsCommis_his
				.nIntermed = nIntermed
				.sTyp_comiss = CStr(commis_his.commissTables.Lifecommiss)
				.nComtab = nComtab_Lif
				.dEffecdate = dEffecdate
				.dEffecdate_Old = dEffecdate_Old
				InsPostAG003 = .Add
			End With
		End If
		
		If nComtab_gen <> eRemoteDB.Constants.intNull Then
			With lclsCommis_his
				.nIntermed = nIntermed
				.sTyp_comiss = CStr(commis_his.commissTables.GralCommiss)
				.nComtab = nComtab_gen
				.dEffecdate = dEffecdate
				.dEffecdate_Old = dEffecdate_Old
				InsPostAG003 = .Add
			End With
		End If
		
		
		If nComtab_ExComm <> eRemoteDB.Constants.intNull Then
			With lclsCommis_his
				.nIntermed = nIntermed
				.sTyp_comiss = CStr(commis_his.commissTables.ExCommiss)
				.nComtab = nComtab_ExComm
				.dEffecdate = dEffecdate
				.dEffecdate_Old = dEffecdate_Old
				InsPostAG003 = .Add
			End With
		End If
		
		If nComtab_EcoSche <> eRemoteDB.Constants.intNull Then
			With lclsCommis_his
				.nIntermed = nIntermed
				.sTyp_comiss = CStr(commis_his.commissTables.EscheCommiss)
				.nComtab = nComtab_EcoSche
				.dEffecdate = dEffecdate
				.dEffecdate_Old = dEffecdate_Old
				InsPostAG003 = .Add
			End With
		End If
		
		If nGoal_life <> eRemoteDB.Constants.intNull Then
			With lclsCommis_his
				.nIntermed = nIntermed
				.sTyp_comiss = CStr(commis_his.commissTables.LifeGoals)
				.nComtab = nGoal_life
				.dEffecdate = dEffecdate
				.dEffecdate_Old = dEffecdate_Old
				InsPostAG003 = .Add
			End With
		End If
		
		If nGoal_gen <> eRemoteDB.Constants.intNull Then
			With lclsCommis_his
				.nIntermed = nIntermed
				.sTyp_comiss = CStr(commis_his.commissTables.GralGoals)
				.nComtab = nGoal_gen
				.dEffecdate = dEffecdate
				.dEffecdate_Old = dEffecdate_Old
				InsPostAG003 = .Add
			End With
		End If
		
		If nSlc_Tab_nr <> eRemoteDB.Constants.intNull Then
			With lclsCommis_his
				.nIntermed = nIntermed
				.sTyp_comiss = CStr(commis_his.commissTables.SpeLifeCommi)
				.nComtab = nSlc_Tab_nr
				.dEffecdate = dEffecdate
				.dEffecdate_Old = dEffecdate_Old
				InsPostAG003 = .Add
			End With
		End If
		
		If nGoal_gen <> eRemoteDB.Constants.intNull Then
			Call lclsGoals.Addtableinterm_bud(nGoal_gen, nIntermed, dEffecdate, nUsercode)
		End If
		
		If nGoal_life <> eRemoteDB.Constants.intNull Then
			Call lclsGoals.Addtableinterm_bud(nGoal_life, nIntermed, dEffecdate, nUsercode)
		End If
		
		
		Me.nIntermed = nIntermed
		Me.nComtabge = nComtab_gen '    Public nComtabge          As long  'smallint 2      5     0     yes      (n/a)              (n/a)
		Me.nComtabli = nComtab_Lif '    Public nComtabli          As long  'smallint 2      5     0     yes      (n/a)              (n/a)
		Me.nTable_cod = nComtab_ExComm '    Public nTable_cod         As long  'smallint 2      5     0     yes      (n/a)              (n/a)
		Me.nEco_sche = nComtab_EcoSche '    Public nEco_sche          As long  'smallint 2      5     0     yes      (n/a)              (n/a)
		Me.nLife_sche = nLife_sche
		Me.nGen_sche = nGen_sche
		Me.nUsercode = nUsercode '   Public nUsercode          As long  'smallint 2      5     0     yes      (n/a)              (n/a)
		Me.nGoal_gen = nGoal_gen
		Me.nGoal_life = nGoal_life
		Me.sCol_agree = sCol_agree '    Public sCol_agree         As String   'char     1                  yes      yes                yes
		Me.sAgreeInt = sAgreeInt '    Public sAgreeInt          As String   'char     1                  yes      yes                yes
		Me.nSlc_Tab_nr = nSlc_Tab_nr
		
		InsPostAG003 = Me.UpdIntermedia
		
InsPostAG003_err: 
		If Err.Number Then
			InsPostAG003 = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsCommis_his may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsCommis_his = Nothing
		'UPGRADE_NOTE: Object lclsGoals may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsGoals = Nothing
	End Function
	'% insPostAG011. Actualiza el estado del intermediario.
	Public Function insPostAG011(ByVal nIntermed As Integer, ByVal dNulldate As Date, ByVal nNullcode As Integer, ByVal nInt_status As Integer, ByVal nUsercode As Integer, ByVal nCircular_doc As Integer) As Boolean
		Dim lclsAgent As eAgent.Intermedia
		lclsAgent = New eAgent.Intermedia
		
		On Error GoTo insPostAG011_Err
		With lclsAgent
			.nIntermed = nIntermed
			.dNulldate = dNulldate
			.nInt_status = nInt_status
			.nUsercode = nUsercode
			.nNullcode = nNullcode
			.nCircular_doc = nCircular_doc
			insPostAG011 = .Update_statusNull
		End With
		'UPGRADE_NOTE: Object lclsAgent may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsAgent = Nothing
		
insPostAG011_Err: 
		If Err.Number Then
			insPostAG011 = False
		End If
		On Error GoTo 0
	End Function
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		nIntermed = eRemoteDB.Constants.intNull
		sClient = String.Empty
		dInpdate = dtmNull
		nInt_status = eRemoteDB.Constants.intNull
		nIntertyp = eRemoteDB.Constants.intNull
		nNullcode = eRemoteDB.Constants.intNull
		dNulldate = dtmNull
		nOffice = eRemoteDB.Constants.intNull
		nSupervis = eRemoteDB.Constants.intNull
		nUsercode = eRemoteDB.Constants.intNull
		sInter_id = String.Empty
		nLegal_sch = eRemoteDB.Constants.intNull
		sCertype = String.Empty
		nBranch = eRemoteDB.Constants.intNull
		nProduct = eRemoteDB.Constants.intNull
		nPolicy = eRemoteDB.Constants.intNull
		dStartdate = dtmNull
		dExpirdat = dtmNull
		nIntermedPol = eRemoteDB.Constants.intNull
		nPremanual = eRemoteDB.Constants.intNull
		nComanual = eRemoteDB.Constants.intNull
		nSup_gen = eRemoteDB.Constants.intNull
		nInsu_Assist = eRemoteDB.Constants.intNull
		nInsu_assistlif = eRemoteDB.Constants.intNull
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	
	'%insPostAGL583: Método que realiza el proceso de cálculo de incentivo de supervisores generales
	Public Function insPostAGL583(ByVal nIntertyp As Integer, ByVal dEffecdateIni As Date, ByVal dEffecdateEnd As Date, ByVal nUsercode As Integer) As Boolean
		
		Dim lclsExecute As eRemoteDB.Execute
		
		On Error GoTo insPostAGL583_Err
		
		lclsExecute = New eRemoteDB.Execute
		
		With lclsExecute
			.StoredProcedure = "INSAGL583"
			.Parameters.Add("nIntertyp", nIntertyp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdateIni", dEffecdateIni, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdateEnd", dEffecdateEnd, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(False) Then
				insPostAGL583 = True
			Else
				insPostAGL583 = False
			End If
		End With
		
insPostAGL583_Err: 
		If Err.Number Then
			insPostAGL583 = False
		End If
		'UPGRADE_NOTE: Object lclsExecute may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsExecute = Nothing
		On Error GoTo 0
		
	End Function
	
	'%insValAGL583: Función que realiza la validacion de los datos introducidos en la sección de Encabezado
	Public Function insValAGL583_K(ByVal sCodispl As String, ByVal nIntertyp As Integer, ByVal dEffecdateIni As Date, ByVal dEffecdateEnd As Date) As String
		
		Dim lclsErrors As eFunctions.Errors
		Dim lclsCtrol_Date As eGeneral.Ctrol_date
		
		On Error GoTo insValAGL583_K_Err
		
		lclsErrors = New eFunctions.Errors
		lclsCtrol_Date = New eGeneral.Ctrol_date
		
		'+ Tipo de intermediario debe estar lleno
		If nIntertyp = 0 Or nIntertyp = eRemoteDB.Constants.intNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 10095)
		Else
			If nIntertyp <> 5 Then
				Call lclsErrors.ErrorMessage(sCodispl, 60102)
			End If
		End If
		
		'+ Fecha debe estar llena
		If dEffecdateIni = dtmNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 9071)
		Else
			If lclsCtrol_Date.Find(51) Then
				If dEffecdateIni <= lclsCtrol_Date.dEffecdate Then
					Call lclsErrors.ErrorMessage(sCodispl, 9122)
				End If
			End If
		End If
		
		
		If dEffecdateEnd = dtmNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 9072)
		Else
			If dEffecdateIni > dEffecdateEnd Then
				Call lclsErrors.ErrorMessage(sCodispl, 3240)
			End If
		End If
		
		insValAGL583_K = lclsErrors.Confirm
		
insValAGL583_K_Err: 
		If Err.Number Then
			insValAGL583_K = "insValAGL583_K: " & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		
	End Function
	'%insPostAGL596: Método que realiza el proceso de cálculo de bonos para agentes generales
	Public Function insPostAGL596(ByVal nIntertyp As Integer, ByVal dEffecdateIni As Date, ByVal dEffecdateEnd As Date, ByVal nUsercode As Integer) As Boolean
		
		Dim lclsExecute As eRemoteDB.Execute
		
		On Error GoTo insPostAGL596_Err
		
		lclsExecute = New eRemoteDB.Execute
		
		With lclsExecute
			.StoredProcedure = "INSAGL596"
			.Parameters.Add("nIntertyp", nIntertyp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdateIni", dEffecdateIni, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdateEnd", dEffecdateEnd, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(False) Then
				insPostAGL596 = True
			Else
				insPostAGL596 = False
			End If
		End With
		
insPostAGL596_Err: 
		If Err.Number Then
			insPostAGL596 = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsExecute may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsExecute = Nothing
	End Function
	
	'%insValAGL596: Función que realiza la validacion de los datos introducidos en la sección de Encabezado
	Public Function insValAGL596_K(ByVal sCodispl As String, ByVal nIntertyp As Integer, ByVal dEffecdateIni As Date, ByVal dEffecdateEnd As Date) As String
		
		Dim lclsErrors As eFunctions.Errors
		Dim lclsCtrol_Date As eGeneral.Ctrol_date
		
		On Error GoTo insValAGL596_K_err
		
		lclsErrors = New eFunctions.Errors
		lclsCtrol_Date = New eGeneral.Ctrol_date
		
		'+ Tipo de intermediario debe estar lleno
		If nIntertyp = 0 Or nIntertyp = eRemoteDB.Constants.intNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 10095)
		End If
		
		'+ Fecha debe estar llena
		If dEffecdateIni = dtmNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 9071)
		Else
			If lclsCtrol_Date.Find(50) Then
				If dEffecdateIni <= lclsCtrol_Date.dEffecdate Then
					Call lclsErrors.ErrorMessage(sCodispl, 9122)
				End If
			End If
		End If
		
		
		If dEffecdateEnd = dtmNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 9072)
		Else
			If dEffecdateEnd <= dEffecdateIni Then
				Call lclsErrors.ErrorMessage(sCodispl, 3240)
			End If
		End If
		
		insValAGL596_K = lclsErrors.Confirm
		
insValAGL596_K_err: 
		If Err.Number Then
			insValAGL596_K = "insValAGL596_K: " & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		'UPGRADE_NOTE: Object lclsCtrol_Date may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsCtrol_Date = Nothing
	End Function
	
	'%insValAGL603: Función que realiza la validacion de los datos introducidos en la sección de Encabezado
	Public Function insValAGL603(ByVal nIntertyp As Integer, ByVal dEffecdateIni As Date, ByVal dEffecdateEnd As Date) As String
		
		Dim lclsErrors As eFunctions.Errors
		Dim sCodispl As String
		
		'+ dEffeclastProc : Fecha del último proceso
		
		On Error GoTo insValAGL603_Err
		
		lclsErrors = New eFunctions.Errors
		
		sCodispl = "AGL603"
		
		
		'+ Tipo de intermediario debe estar lleno
		If nIntertyp = 0 Or nIntertyp = eRemoteDB.Constants.intNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 10095)
		End If
		
		'+ Fecha inicial: Debe estar llena
		If dEffecdateIni = dtmNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 9071)
		End If
		
		'+ Fecha Final: Debe estar llena
		If dEffecdateEnd = dtmNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 9072)
		Else
			'+ Fecha final debe ser mayor que la Fecha inicial
			If dEffecdateIni >= dEffecdateEnd Then
				Call lclsErrors.ErrorMessage(sCodispl, 3240)
			End If
		End If
		
		insValAGL603 = lclsErrors.Confirm
		
insValAGL603_Err: 
		If Err.Number Then
			insValAGL603 = "insValAGL603: " & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
	End Function
	
	'%insPostAGL603: Método que realiza el proceso de cálculo de bonos de cumplimientos generales
	Public Function insPostAGL603(ByVal nIntertyp As Integer, ByVal dEffecdateIni As Date, ByVal dEffecdateEnd As Date, ByVal nUsercode As Integer) As Boolean
		
		Dim lexeTime As eRemoteDB.Execute
		
		On Error GoTo insPostAGL603_Err
		
		lexeTime = New eRemoteDB.Execute
		
		
		With lexeTime
			.StoredProcedure = "INSAGL603"
			.Parameters.Add("nIntertyp", nIntertyp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdateIni", dEffecdateIni, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdateEnd", dEffecdateEnd, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(False) Then
				insPostAGL603 = True
			Else
				insPostAGL603 = False
			End If
		End With
		
insPostAGL603_Err: 
		If Err.Number Then
			insPostAGL603 = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lexeTime may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lexeTime = Nothing
	End Function
	
	'insValAGL728_K: Función que realiza la validacion de los datos introducidos en la sección de Encabezado
	Public Function insValAGL728_K(ByVal pstrCodispl As String, ByVal dProcess_date As Date) As String
		Dim lclsErrors As eFunctions.Errors
		
		On Error GoTo insValAGL728_K_Err
		
		lclsErrors = New eFunctions.Errors
		
		'+ Fecha de proceso de información debe estar llena
		If dProcess_date = dtmNull Then
			Call lclsErrors.ErrorMessage(pstrCodispl, 2056)
		End If
		
		insValAGL728_K = lclsErrors.Confirm
		
insValAGL728_K_Err: 
		If Err.Number Then
			insValAGL728_K = "insValAGL728_K: " & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
	End Function
	
	'insPostAGL728_K: Método que realiza el proceso de carga de comisiones por pólizas
	Public Function insPostAGL728_K(ByVal sOptprocess As String, ByVal dProcess_date As Date, ByVal nUsercode As Integer) As Boolean
		
		Dim lexeinsAGL728 As eRemoteDB.Execute
		
		lexeinsAGL728 = New eRemoteDB.Execute
		
		On Error GoTo insPostAGL728_K_Err
		
		With lexeinsAGL728
			.StoredProcedure = "insCharge_Comm_Pol"
			.Parameters.Add("sOptProcess", sOptprocess, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dProcess_date", dProcess_date, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				insPostAGL728_K = True
			Else
				insPostAGL728_K = False
			End If
		End With
		
insPostAGL728_K_Err: 
		If Err.Number Then
			insPostAGL728_K = False
		End If
		'UPGRADE_NOTE: Object lexeinsAGL728 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lexeinsAGL728 = Nothing
		On Error GoTo 0
	End Function
	
	'insValAGL009_K: Función que realiza la validacion de los datos introducidor en la sección
	'              del encabezado
    Public Function insValAGL009_K(ByVal sCodispl As String, ByVal nInsur_area As Integer, ByVal nIntermed As Integer, ByVal dValor_date As Date, ByVal nPay_comm As Integer, nDocSupport As Integer) As String
        Dim lclsErrors As eFunctions.Errors
        Dim lclsCashBank As Object
        Dim lstrAccDesc As String

        On Error GoTo insValAGL009_K_Err

        lclsErrors = New eFunctions.Errors
        lclsCashBank = eRemoteDB.NetHelper.CreateClassInstance("eCashBank.Cheq_book")

        '+ Área de seguro debe estar llena

        If nInsur_area = 0 Or nInsur_area = eRemoteDB.Constants.intNull Then
            Call lclsErrors.ErrorMessage(sCodispl, 55031)
        End If

        '+ Fecha de Valorización debe estar llena

        If dValor_date = dtmNull Then
            Call lclsErrors.ErrorMessage(sCodispl, 55527)
        End If

        If nIntermed <> 0 And nIntermed <> eRemoteDB.Constants.intNull Then
            If nPay_comm = 0 Or nPay_comm = eRemoteDB.Constants.intNull Then
                Call lclsErrors.ErrorMessage(sCodispl, 90000042)
            End If
            If nDocSupport = 0 Or nDocSupport = eRemoteDB.Constants.intNull Then
                Call lclsErrors.ErrorMessage(sCodispl, 90000043)
            End If
        End If

        insValAGL009_K = lclsErrors.Confirm

insValAGL009_K_Err:
        If Err.Number Then
            insValAGL009_K = "insValAGL009_K: " & Err.Description
        End If
        'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsErrors = Nothing
        'UPGRADE_NOTE: Object lclsCashBank may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsCashBank = Nothing

        On Error GoTo 0
    End Function
	
	'insPostAGL009_K: Método que realiza el proceso de solicitud de pago de comisiones
    Public Function insPostAGL009_K(ByVal nIntertyp As Integer, ByVal nInsur_area As Integer, ByVal nIntermed As Integer, ByVal dProcess_date As Date, ByVal dValue_date As Date, ByVal nUsercode As Integer, ByVal sOptprocess As String, ByVal nPay_comm As Double, ByVal nTypeSupport As Double, ByVal nDocSupport As Double) As Boolean

        Dim lexeinsAGL009 As eRemoteDB.Execute

        lexeinsAGL009 = New eRemoteDB.Execute

        On Error GoTo insPostAGL009_K_Err

        With lexeinsAGL009
            .StoredProcedure = "insComm_Pay"
            .Parameters.Add("nIntertyp", nIntertyp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nInsur_Area", nInsur_area, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nIntermed", nIntermed, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dProcess_date", dProcess_date, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dValue_date", dValue_date, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sInd", IIf(nIntermed <> 0 And nIntermed <> eRemoteDB.Constants.intNull, "1", "2"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sKey_Aux", "1", eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sOptProcess", sOptprocess, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPay_comm", nPay_comm, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTypeSupport", nTypeSupport, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nDocSupport", nDocSupport, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)


            If .Run(False) Then
                sKey = .Parameters("sKey_Aux").Value
                insPostAGL009_K = True
            Else
                insPostAGL009_K = False
            End If
        End With

insPostAGL009_K_Err:
        If Err.Number Then
            insPostAGL009_K = False
        End If
        'UPGRADE_NOTE: Object lexeinsAGL009 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lexeinsAGL009 = Nothing
        On Error GoTo 0
    End Function
	
	Public Function insValAGL014_k(ByVal sCodispl As String, ByVal nOption As Integer, ByVal nIntermed As Integer, ByVal sClientCode As String, ByVal dStardate As Date, ByVal dEnddate As Date, ByVal nPolicy As Double) As String
		Dim lclsIntermedia As eAgent.Intermedia = New eAgent.Intermedia
		Dim lclsClient As New eClient.Client
		Dim lclsErrors As New eFunctions.Errors
		
		On Error GoTo insvalAGL014_k_err
		
		Select Case nOption
			Case 1
				'+ Se seleccionó listar préstamos de intermediarios:
				If nIntermed = 0 Or nIntermed = intNull Then
					Call lclsErrors.ErrorMessage(sCodispl, 21038)
				Else
					If Not lclsIntermedia.Find(nIntermed) Then
						Call lclsErrors.ErrorMessage(sCodispl, 3634)
					End If
				End If
				
			Case 2
				'+ Se seleccionó listar préstamos de un cliente:
				If sClientCode = String.Empty Then
					Call lclsErrors.ErrorMessage(sCodispl, 4122)
				Else
					If Not lclsClient.Find(sClientCode) Then
						Call lclsErrors.ErrorMessage(sCodispl, 7050)
					End If
					
					If Not lclsIntermedia.Find_ClientInterAGL014(sClientCode) Then
						Call lclsErrors.ErrorMessage(sCodispl, 9121)
					End If
				End If
		End Select
		
		If nPolicy = eRemoteDB.Constants.intNull Or nPolicy = 0 Then
			If dStardate = dtmNull Then
				'+ No se indicó número de póliza ni fecha de inicio
				Call lclsErrors.ErrorMessage(sCodispl, 9071)
			End If
			
			If dEnddate = dtmNull Then
				'+ No se indicó número de póliza ni fecha de fin
				Call lclsErrors.ErrorMessage(sCodispl, 9072)
			Else
				If dStardate <> dtmNull Then
					If dStardate > dEnddate Then
						'+ Fecha de inicio mayor a fecha fin
						Call lclsErrors.ErrorMessage(sCodispl, 3240)
					End If
				End If
			End If
		End If
		
		insValAGL014_k = lclsErrors.Confirm
		
insvalAGL014_k_err: 
		If Err.Number Then
			insValAGL014_k = "insValAGL014_k: " & Err.Description
		End If
		
		'UPGRADE_NOTE: Object lclsIntermedia may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsIntermedia = Nothing
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		'UPGRADE_NOTE: Object lclsClient may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsClient = Nothing
		
		On Error GoTo 0
		
	End Function
	
	'%Find_ClientInterAGL014(). Esta funcion valida que el cliente tenga al menos un
	'%intermediario asociado en la tabla de intermediarios segun el cliente indicado.
	Public Function Find_ClientInterAGL014(ByRef lstrClient As String) As Boolean
		Dim lrecIntermed As eRemoteDB.Execute
		lrecIntermed = New eRemoteDB.Execute
		On Error GoTo Find_ClientInterAGL014_Err
		
		With lrecIntermed
			.StoredProcedure = "reaIntermed_v4"
			.Parameters.Add("sClient", lstrClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(True) Then
				Find_ClientInterAGL014 = .FieldToClass("nExist") = 1
				.RCloseRec()
			End If
		End With
		
		'UPGRADE_NOTE: Object lrecIntermed may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecIntermed = Nothing
		
Find_ClientInterAGL014_Err: 
		If Err.Number Then
			Find_ClientInterAGL014 = False
		End If
		On Error GoTo 0
	End Function
	
	'**%insValAG008_K: Basic fields Validation function. These validations are for the report execution - ACM - Jan-16-2002
	'%  insValAG008_K: Función de validación de campos básicos para la emisión del reporte - ACM - 16/01/2002
	Public Function insValAGL008_K(ByVal sCodispl As String, ByVal dDateProcess As Date, ByVal nInsur_area As Integer, ByVal nIntermediaOld As Double, ByVal nIntermediaNew As Double) As String
		Dim lclsErrors As New eFunctions.Errors
		
		On Error GoTo insValAGL008_K_err
		
		'**+ Validation #55031: It must be filled
		'+ Validación #55031: Debe estar lleno
		If nInsur_area = 0 Or nInsur_area = eRemoteDB.Constants.intNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 55031)
		End If
		
		'**+ Validation #9068: It must be filled
		'+ Validación #9068: Debe estar lleno
		If dDateProcess = dtmNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 9068)
		End If
		
		'**+ Validation #9073: It must be filled
		'+ Validación #9073: Debe estar lleno
		If nIntermediaOld = 0 Or nIntermediaOld = eRemoteDB.Constants.intNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 9073)
		Else
			
			'**+ Validation #8068: If this field is filled, it must be registered in the intermedaries file
			'+ Validation #8068: Si este campo está lleno, debe estar registrado en el archivo de intermediarios
			If Not Find(nIntermediaOld) Then
				Call lclsErrors.ErrorMessage(sCodispl, 8068)
			End If
		End If
		
		'Si este campo está lleno, debe ser diferente al indicado como intermediario anterior  09005
		
		'**+ Validation #9074: It must be filled
		'+ Validación #9074: Debe estar lleno
		If nIntermediaNew = 0 Or nIntermediaNew = eRemoteDB.Constants.intNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 9074)
		Else
			
			'**+ Validation #8068: If this field is filled, it must be registered in the intermedaries file
			'+ Validation #8068: Si este campo está lleno, debe estar registrado en el archivo de intermediarios
			If Not Find(nIntermediaNew) Then
				Call lclsErrors.ErrorMessage(sCodispl, 8068)
			End If
			
			If Me.nInt_status = 2 Then
				Call lclsErrors.ErrorMessage(sCodispl, 750098)
			End If
			
			'**+ Validation #9005: If the parameters "nIntermediaOld" and "nIntermediaNew" are filled,
			'**+                   these parameters can not have the same value
			'+ Validación #9005: Si los parámetros "nIntermediaOld" y "nIntermediaNew" están llenos, éstos
			'+                   no pueden ser iguales
			If nIntermediaOld = nIntermediaNew Then
				Call lclsErrors.ErrorMessage(sCodispl, 9005)
			End If
		End If
		
		insValAGL008_K = lclsErrors.Confirm
		
insValAGL008_K_err: 
		If Err.Number Then
			insValAGL008_K = "insValAGL008_K: " & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
	End Function
	
	'% insPostAGL008: Función que realiza el llamado al proceso de traspaso de cartera de intermediarios.
	Public Function insPostAGL008(ByVal dDateProcess As Date, ByVal nInsur_area As Integer, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal dStartdate As Date, ByVal dExpirdat As Date, ByVal nInterBefore As Integer, ByVal nInterNew As Integer, ByVal nInterPol As Integer, ByVal sKey_Aux As String, ByVal nUsercode As Integer, ByVal sOptProcTyp As String) As Boolean
		Dim lclsRemote As New eRemoteDB.Execute
		Dim sFormat As String
		
		On Error GoTo insPostAGL008_err
		
		lclsRemote = New eRemoteDB.Execute
		sFormat = "yyyyMMdd"
		
		With lclsRemote
			.StoredProcedure = "Reacommipol_2"
			.Parameters.Add("dDateProcess", Format(dDateProcess, sFormat), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sOptProcTyp", sOptProcTyp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInsur_Area", nInsur_area, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dStartDate", dStartdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dExpirdat", dExpirdat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInterBefore", nInterBefore, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInterNew", nInterNew, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInterPol", nInterPol, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sKey", sKey_Aux, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(False) Then
				sKey = .Parameters("skey").Value
				insPostAGL008 = True
			Else
				insPostAGL008 = False
			End If
		End With
		
insPostAGL008_err: 
		If Err.Number Then
			insPostAGL008 = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsRemote may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsRemote = Nothing
	End Function
	
	'**% insValAG002_K: Validation function.
	'% insValAG002_K: Función de validación.
	Public Function insValAGL002_k(ByVal sCodispl As String, ByVal dProcessDate As Date, ByVal nInsur_area As Integer) As String
		
		Dim lclsErrors As eFunctions.Errors
		Dim lclsCtrolDate As eGeneral.Ctrol_date
		Dim dCtrolDate As Date
		
		lclsCtrolDate = New eGeneral.Ctrol_date
		lclsErrors = New eFunctions.Errors
		
		On Error GoTo insValAGL002_k_Err
		
		'**+ Validation #36203: It must be filled
		'+   Validación #36203: Debe estar lleno
		
		If dProcessDate = dtmNull Then
			lclsErrors.ErrorMessage(sCodispl, 36203)
		Else
			
			'+ (10) Valor correspondiente al proceso de Act. de Ctas. Ctes. por Préstamo (Según Table178)
			Call lclsCtrolDate.Find(10)
			
			dCtrolDate = lclsCtrolDate.dEffecdate
			'+ Valida que el proceso se ejecute al menos con 28 días de diferencia
			dCtrolDate = System.Date.FromOADate(dCtrolDate.ToOADate + 28)
			If dProcessDate < dCtrolDate Then
				lclsErrors.ErrorMessage(sCodispl, 9126)
			End If
		End If
		
		
		'+ Valida el campo área de seguros
		If nInsur_area = eRemoteDB.Constants.intNull Or nInsur_area = 0 Then
			lclsErrors.ErrorMessage(sCodispl, 55031)
		End If
		
		insValAGL002_k = lclsErrors.Confirm
		
insValAGL002_k_Err: 
		If Err.Number Then
			insValAGL002_k = "insValAGL002_k: " & Err.Description
		End If
		
		'UPGRADE_NOTE: Object lclsCtrolDate may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsCtrolDate = Nothing
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		
		On Error GoTo 0
	End Function
	
	'%Find: Este metodo retorna VERDADERO o FALSO dependiendo de la existencia de Intermediarios asociados
	'% a un supervisor
	Public Function Find_Supervis_v(ByVal nSupervis As Integer) As Boolean
		Dim lrecinter As eRemoteDB.Execute
		
		On Error GoTo Find_Supervis_v_Err
		
		lrecinter = New eRemoteDB.Execute
		
		With lrecinter
			.StoredProcedure = "reaIntermed_sup"
			.Parameters.Add("nSupervis", nSupervis, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Find_Supervis_v = True
				.RCloseRec()
			Else
				Find_Supervis_v = False
			End If
		End With
		
Find_Supervis_v_Err: 
		If Err.Number Then
			Find_Supervis_v = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecinter may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinter = Nothing
	End Function
	
	'%findIntermedsClient:Valida que el rut ingresado pertenezca a un intermediario
	Public Function findIntermedsClient(ByVal sClient As String) As Boolean
		'-Se define la variable lrec_Intermed que se utilizará como cursor.
		Dim lrec_Intermed As eRemoteDB.Execute
		
		'-Se define el arreglo de parámetro a pasar al store procedure.
		lrec_Intermed = New eRemoteDB.Execute
		
		On Error GoTo findIntermedsClient_err
		'Definición de parámetros para stored procedure 'insudb.reaintermedsclient'
		'Información leída el 15/11/2000 04:49:59 a.m.
		
		With lrec_Intermed
			.StoredProcedure = "reaintermedsclient"
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(True) Then
				findIntermedsClient = True
				Me.sClient = .FieldToClass("sClient")
				.RCloseRec()
			End If
		End With
		
findIntermedsClient_err: 
		If Err.Number Then
			findIntermedsClient = False
		End If
		On Error GoTo 0
	End Function
	
	'%findIntermSup:Valida que el rut ingresado pertenezca a un intermediario
	Public Function FindIntermSup(ByVal sClient As String, ByVal nIntertyp As Integer) As Boolean
		'-Se define la variable lrec_Intermed que se utilizará como cursor.
		Dim lrec_Intermed As eRemoteDB.Execute
		
		'-Se define el arreglo de parámetro a pasar al store procedure.
		lrec_Intermed = New eRemoteDB.Execute
		
		On Error GoTo FindIntermSup_err
		'Definición de parámetros para stored procedure 'insudb.reaintermsup'
		'Información leída el 15/11/2000 04:49:59 a.m.
		
		With lrec_Intermed
			.StoredProcedure = "reaintermsup"
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nIntertyp", nIntertyp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(True) Then
				FindIntermSup = True
				Me.sClient = .FieldToClass("sClient")
				.RCloseRec()
			End If
		End With
		
FindIntermSup_err: 
		If Err.Number Then
			FindIntermSup = False
		End If
		On Error GoTo 0
	End Function
	
	'%findIntermSup:Valida que el rut ingresado pertenezca a un intermediario
	Public Function FindsClienInterm(ByVal sClient As String, ByVal nIntermed As Integer) As Boolean
		'-Se define la variable lrec_Intermed que se utilizará como cursor.
		Dim lrec_Intermed As eRemoteDB.Execute
		
		'-Se define el arreglo de parámetro a pasar al store procedure.
		lrec_Intermed = New eRemoteDB.Execute
		
		On Error GoTo FindsClienInterm_err
		'Definición de parámetros para stored procedure 'insudb.reaintermsup'
		'Información leída el 15/11/2000 04:49:59 a.m.
		
		With lrec_Intermed
			.StoredProcedure = "reasclienintermedia"
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nIntermed", nIntermed, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCount", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				If .Parameters("nCount").Value > 0 Then
					FindsClienInterm = True
				Else
					FindsClienInterm = False
				End If
			End If
		End With
		
FindsClienInterm_err: 
		If Err.Number Then
			FindsClienInterm = False
		End If
		On Error GoTo 0
	End Function
	
	'%+ FindValidIntermediario: verifica que no exista un codigo de intermediario vigente
	'%+ para el mismo RUT y tipo de intermediario
	Public Function FindValidIntermediario(ByVal nIntermed As Integer, ByVal sClient As String, ByVal nIntertyp As Integer) As Integer
		Dim lrecinter As eRemoteDB.Execute
		
		On Error GoTo FindValidIntermediario_Err
		
		lrecinter = New eRemoteDB.Execute
		With lrecinter
			.StoredProcedure = "reaIntermedValid"
			.Parameters.Add("nIntermed", nIntermed, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nIntertyp", nIntertyp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCount", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(False) Then
				FindValidIntermediario = .Parameters("nCount").Value
			Else
				FindValidIntermediario = 0
			End If
			.RCloseRec()
		End With
		
FindValidIntermediario_Err: 
		If Err.Number Then
			FindValidIntermediario = 0
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecinter may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinter = Nothing
	End Function
	
	'**% insValAGL001_K: validate the header section of the page AGL001 as described in the
	'**% functional specifications
	'% InsValAGL001_K: Este metodo se encarga de realizar las validaciones del encabezado (Header)
	'% descritas en el funcional de la ventana AGL001
	Public Function insValAGL703_k(ByVal dInitDate As Date, ByVal dEnddate As Date) As String
		Dim lclsErrors As eFunctions.Errors
		Dim lclsValField As eFunctions.valField
		lclsErrors = New eFunctions.Errors
		lclsValField = New eFunctions.valField
		lclsValField.objErr = lclsErrors
		
		On Error GoTo insValAGL703_k_Err
		
		'**+ Validation of the Final Date
		'+Validación de la Fecha Final
		
		lclsValField.ErrEmpty = 9071
		If lclsValField.ValDate(dInitDate,  , eFunctions.valField.eTypeValField.ValAll) Then
			
		Else
			lclsValField.ErrEmpty = 9072
			If lclsValField.ValDate(dEnddate,  , eFunctions.valField.eTypeValField.ValAll) Then
				If dEnddate < dInitDate Then
					lclsErrors.ErrorMessage("AGL703", 3240)
				End If
			End If
		End If
		
		insValAGL703_k = lclsErrors.Confirm
		
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		'UPGRADE_NOTE: Object lclsValField may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsValField = Nothing
		
insValAGL703_k_Err: 
		If Err.Number Then
			insValAGL703_k = "insValAGL703_k: " & Err.Description
		End If
		On Error GoTo 0
	End Function
End Class






