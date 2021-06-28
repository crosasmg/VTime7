Option Strict Off
Option Explicit On
Public Class Request
	'%-------------------------------------------------------%'
	'% $Workfile:: Request.cls                              $%'
	'% $Author:: Nvaplat22                                  $%'
	'% $Date:: 23/07/04 9:12p                               $%'
	'% $Revision:: 36                                       $%'
	'%-------------------------------------------------------%'
	
	'+
	'+ Estructura de tabla request al 03-17-2003 15:40:13
	'+         Property                Type               DBType   Size Scale  Prec  Null
	'+------------------------------------------------------------------------------------
	Public sCertype As String ' CHAR       1    0     0    N
	Public nOrigin As eRequestOrigin ' NUMBER     22   0     5    N
	Public nBranch As Integer ' NUMBER     22   0     5    N
	Public nProduct As Integer ' NUMBER     22   0     5    N
	Public nPolicy As Double ' NUMBER     22   0     10   N
	Public nProponum As Double ' NUMBER     22   0     10   N
	Public nCertif As Double ' NUMBER     22   0     10   N
	Public dEffecdate As Date ' DATE       7    0     0    N
	Public dNulldate As Date ' DATE       7    0     0    S
	Public sTyp_surr As String ' CHAR       1    0     0    S
	Public sPayorder As String ' CHAR       1    0     0    S
	Public sNull_rec As String ' CHAR       1    0     0    S
	Public nAmount As Double ' NUMBER     22   2     12   S
	Public sDescript As String ' CHAR       30   0     0    S
	Public nNotenum As Double ' NUMBER     22   0     10   S
	Public dCompdate As Date ' DATE       7    0     0    N
	Public nUsercode As Integer ' NUMBER     22   0     5    N
	Public nNullcode As Integer ' NUMBER     22   0     5    S
	Public nTyp_rec As Integer ' NUMBER     22   0     5    S
	Public sReh_lrec As String ' CHAR       1    0     0    S
	Public nInterestrate As Double ' NUMBER     22   2     4    S
	Public nTypepay As Integer ' NUMBER     22   0     5    S
	Public dMovDate As Date ' DATE       7    0     0    S
	Public nRequest_nu As Integer ' NUMBER     22   0     10   S
	Public sClipaysurr As String ' CHAR       14   0     0    S
	Public sCertpaysurr As String ' CHAR       1    0     0    S
	Public nBrapaysurr As Integer ' NUMBER     22   0     5    S
	Public nPropaysurr As Integer ' NUMBER     22   0     5    S
	Public nPolpaysurr As Integer ' NUMBER     22   0     10   S
	Public nCerpaysurr As Integer ' NUMBER     22   0     10   S
	Public nAgency As Integer ' NUMBER     22   0     5    S
	Public nSurr_reason As Integer ' NUMBER     22   0     5    S
	Public nType_payment As Integer ' NUMBER     22   0     5    S
	Public nInstitution As Integer ' NUMBER     22   0     5    S
	Public sClientinstitution As String ' CHAR       14   0     0    S
	Public sReturn_ind As String
	Public nReturn_Rat As Integer
	Public nOrigin_apv As Double
	Public sInd_Insur As Object
	Public nCurrency As Integer
	Public nSwitchOrigin As Integer
	
	'-Estado de propuesta en certificat
	Public nStatquota As Integer
	'-Clase de producto de la cotizacion/propuesta asociada a la solicitud
	Public nProdClass As Integer
	
	
	
	'+ Tipo de Cotizacion/Propuesta
	Public Enum eRequestOrigin
		reqOrigIssue = 1 ' Emision
		reqOrigModified = 2 ' Modificacion
		reqOrigRenewal = 3 ' Renovacion
		reqOrigCancelation = 4 ' Anulacion
		reqOrigRehab = 5 ' Rehablitacion
		reqOrigSettled = 6 ' Saldado
		reqOrigExtended = 7 ' Prorrogado
		reqOrigSurrender = 8 ' Rescate
		reqOrigLoan = 9 ' Prestamo
	End Enum
	
	'% insPostCA767: Se realiza la actualización de los datos en la ventana CA767
	Public Function insPostCA767(ByVal sCodispl As String, ByVal nAction As Integer, ByVal sCertype As String, ByVal nOrigin As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal sTyp_surr As String, ByVal sPayorder As String, ByVal sNull_rec As String, ByVal nAmount As Double, ByVal sDescript As String, ByVal nNotenum As Double, ByVal nNullcode As Integer, ByVal nTyp_rec As Integer, ByVal sReh_lrec As String, ByVal nUsercode As Integer, ByVal nOperat As Integer, ByVal nStatquota As Integer, ByVal nNo_convers As Integer, ByVal nProponum As Double) As Boolean
		Dim lclsPolicy_his As Policy_his
		Dim lclsCertificat As Certificat
		
		On Error GoTo insPostCA767_Err
		
		lclsPolicy_his = New Policy_his
		lclsCertificat = New Certificat
		
		insPostCA767 = True
		With Me
			.sCertype = sCertype
			.nOrigin = nOrigin
			.nBranch = nBranch
			.nProduct = nProduct
			.nPolicy = nProponum
			.nCertif = nCertif
			.dEffecdate = dEffecdate
			.dNulldate = dNulldate
			.sTyp_surr = sTyp_surr
			.sPayorder = IIf(sPayorder = "0", String.Empty, sPayorder)
			.sNull_rec = sNull_rec
			.nAmount = nAmount
			.sDescript = sDescript
			.nNotenum = nNotenum
			.nNullcode = nNullcode
			.nTyp_rec = nTyp_rec
			.sReh_lrec = sReh_lrec
			.nUsercode = nUsercode
			
			Select Case nAction
				'+Si la opción seleccionada es Modificar
				Case eFunctions.Menues.TypeActions.clngActionUpdate
					insPostCA767 = Update
			End Select
		End With
		
		With lclsPolicy_his
			.sCertype = sCertype
			.nBranch = nBranch
			.nProduct = nProduct
			.nPolicy = nProponum
			.nCertif = nCertif
			.nUsercode = nUsercode
			.nAgency = eRemoteDB.Constants.intNull
			.nProponum = nPolicy
			.dEffecdate = dEffecdate
			Select Case nOperat
				Case 2
					nStatquota = 1 '+ Pendiente
				Case 3
					.nType = 63 '+ Rechazo de Cotiz./Prop.
					nStatquota = 3 '+ Rechazado
				Case 4
					.nType = 62 '+ Anulación de Cotiz./Prop.
					nStatquota = 4 '+ Anulado
				Case 5
					nStatquota = 1 '+ Pendiente
				Case 6
					.nType = 64 '+ Regularizar Cotiz./Prop.
					nStatquota = 6 '+ Regularizar
				Case 7
					.nType = 76 '+ Reverso de estado.
					nStatquota = 1 '+ Regularizar
			End Select
			
			Select Case nAction
				'+Si la opción seleccionada es Modificar
				Case eFunctions.Menues.TypeActions.clngActionUpdate
					If Not .insCrePolicy_his() Then
						insPostCA767 = False
					End If
					If lclsCertificat.Find(sCertype, nBranch, nProduct, nProponum, nCertif, True) Then
						If nOperat = 4 Then
							lclsCertificat.dDat_no_con = Today
							lclsCertificat.nNo_convers = nNo_convers
						End If
						lclsCertificat.nStatquota = nStatquota
						
						If Not lclsCertificat.Update() Then
							insPostCA767 = False
						End If
					End If
			End Select
		End With
        
        With lclsPolicy_his
			.sCertype = "2"
			.nBranch = nBranch
			.nProduct = nProduct
			.nPolicy = nPolicy
			.nCertif = nCertif
			.nUsercode = nUsercode
			.nAgency = eRemoteDB.Constants.intNull
			.nProponum = nProponum
			.dEffecdate = dEffecdate
			Select Case nOperat
				Case 2
					nStatquota = 1 '+ Pendiente
				Case 3
					.nType = 63 '+ Rechazo de Cotiz./Prop.
					nStatquota = 3 '+ Rechazado
				Case 4
					.nType = 62 '+ Anulación de Cotiz./Prop.
					nStatquota = 4 '+ Anulado
				Case 5
					nStatquota = 1 '+ Pendiente
				Case 6
					.nType = 64 '+ Regularizar Cotiz./Prop.
					nStatquota = 6 '+ Regularizar
				Case 7
					.nType = 76 '+ Reverso de estado.
					nStatquota = 1 '+ Regularizar
			End Select
			
			Select Case nAction
				'+Si la opción seleccionada es Modificar
				Case eFunctions.Menues.TypeActions.clngActionUpdate
					If Not .insCrePolicy_his() Then
						insPostCA767 = False
					End If					
			End Select
		End With		
insPostCA767_Err: 
		If Err.Number Then
			insPostCA767 = False
		End If
		'UPGRADE_NOTE: Object lclsPolicy_his may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsPolicy_his = Nothing
		'UPGRADE_NOTE: Object lclsCertificat may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsCertificat = Nothing
		On Error GoTo 0
	End Function
	
	'%Add: Crea un registro en la tabla
	Public Function Add() As Boolean
		Add = InsUpdRequest(1)
	End Function
	
	'%Update: Actualiza un registro en la tabla
	Public Function Update() As Boolean
		Update = InsUpdRequest(2)
	End Function
	
	'%Delete: Borra un registro en la tabla
	Public Function Delete() As Boolean
		Delete = InsUpdRequest(3)
	End Function
	
	'%Find: Lee los datos de la tabla
	Public Function Find(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, Optional ByVal lblnFind As Boolean = False) As Boolean
		Dim lrecrearequest As eRemoteDB.Execute
		
		On Error GoTo Find_Err
		
		If Me.sCertype <> sCertype Or Me.nBranch <> nBranch Or Me.nProduct <> nProduct Or Me.nPolicy <> nPolicy Or Me.nCertif <> nCertif Or Me.dEffecdate <> dEffecdate Or lblnFind Then
			
			lrecrearequest = New eRemoteDB.Execute
			'+Definición de parámetros para stored procedure 'reaRequest'
			With lrecrearequest
				.StoredProcedure = "reaRequest"
				.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If .Run Then
					Find = True
					Me.sCertype = sCertype
					Me.nBranch = nBranch
					Me.nProduct = nProduct
					Me.nPolicy = nPolicy
					Me.nCertif = nCertif
					Me.dEffecdate = .FieldToClass("dEffecdate")
					nOrigin = .FieldToClass("nOrigin")
					sTyp_surr = .FieldToClass("sTyp_surr")
					sPayorder = .FieldToClass("sPayOrder")
					sNull_rec = .FieldToClass("sNull_rec")
					nAmount = .FieldToClass("nAmount")
					sDescript = .FieldToClass("sDescript")
					nNotenum = .FieldToClass("nNoteNum")
					nTyp_rec = .FieldToClass("nTyp_rec")
					sReh_lrec = .FieldToClass("sReh_lrec")
					nInterestrate = .FieldToClass("nInterestRate")
					nTypepay = .FieldToClass("nTypepay")
					sClipaysurr = .FieldToClass("sCliPaySurr")
					sCertpaysurr = .FieldToClass("sCertPaySurr")
					nBrapaysurr = .FieldToClass("nBraPaySurr")
					nPropaysurr = .FieldToClass("nProPaySurr")
					nPolpaysurr = .FieldToClass("nPolPaySurr")
					nCerpaysurr = .FieldToClass("nCerPaySurr")
					nAgency = .FieldToClass("nAgency")
					nSurr_reason = .FieldToClass("nSurr_reason")
					nType_payment = .FieldToClass("nType_payment")
					nInstitution = .FieldToClass("nInstitution")
					sClientinstitution = .FieldToClass("sClientinstitution")
					nNullcode = .FieldToClass("nNullcode")
					sReturn_ind = .FieldToClass("sReturn_ind")
					nReturn_Rat = .FieldToClass("nReturn_Rat")
					nOrigin_apv = .FieldToClass("nOrigin_apv")
					dNulldate = .FieldToClass("dNulldate")
					nRequest_nu = .FieldToClass("nRequest_nu")
					sInd_Insur = .FieldToClass("sInd_Insur")
					nCurrency = .FieldToClass("nCurrency")
					nSwitchOrigin = .FieldToClass("nSwitchOrigin")
					
					.RCloseRec()
				End If
			End With
		Else
			Find = True
		End If
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecrearequest may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecrearequest = Nothing
	End Function
	
	'% Find_nProponum: Busca el ultimo numero de propuesta
	Public Function Find_nProponum(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double) As Boolean
		Dim lrecreaFind_nProponum As eRemoteDB.Execute
		
		On Error GoTo Find_nProponum_Err
		lrecreaFind_nProponum = New eRemoteDB.Execute
		
		'+ Definición de store procedure reanProponumal 14-08-2003
		With lrecreaFind_nProponum
			.StoredProcedure = "reanProponum"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				Find_nProponum = True
				Me.sCertype = sCertype
				Me.nBranch = nBranch
				Me.nProduct = nProduct
				Me.nPolicy = nPolicy
				Me.nCertif = nCertif
				Me.dEffecdate = .FieldToClass("dEffecdate")
				nOrigin = .FieldToClass("nOrigin")
				sTyp_surr = .FieldToClass("sTyp_surr")
				sPayorder = .FieldToClass("sPayOrder")
				sNull_rec = .FieldToClass("sNull_rec")
				nAmount = .FieldToClass("nAmount")
				sDescript = .FieldToClass("sDescript")
				nNotenum = .FieldToClass("nNoteNum")
				nTyp_rec = .FieldToClass("nTyp_rec")
				sReh_lrec = .FieldToClass("sReh_lrec")
				nInterestrate = .FieldToClass("nInterestRate")
				nTypepay = .FieldToClass("nTypepay")
				sClipaysurr = .FieldToClass("sCliPaySurr")
				sCertpaysurr = .FieldToClass("sCertPaySurr")
				nBrapaysurr = .FieldToClass("nBraPaySurr")
				nPropaysurr = .FieldToClass("nProPaySurr")
				nPolpaysurr = .FieldToClass("nPolPaySurr")
				nCerpaysurr = .FieldToClass("nCerPaySurr")
				nAgency = .FieldToClass("nAgency")
				nSurr_reason = .FieldToClass("nSurr_reason")
				nType_payment = .FieldToClass("nType_payment")
				nInstitution = .FieldToClass("nInstitution")
				sClientinstitution = .FieldToClass("sClientinstitution")
				nNullcode = .FieldToClass("nNullcode")
				sReturn_ind = .FieldToClass("sReturn_ind")
				nReturn_Rat = .FieldToClass("nReturn_Rat")
				
				.RCloseRec()
			End If
		End With
		
Find_nProponum_Err: 
		If Err.Number Then
			Find_nProponum = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecreaFind_nProponum may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaFind_nProponum = Nothing
	End Function
	
	'%InsUpdRequest: Crea un registro en la tabla
	Private Function InsUpdRequest(ByVal nAction As Integer) As Boolean
		Dim lrecinsupdrequest As eRemoteDB.Execute
		
		On Error GoTo insupdrequest_Err
		
		lrecinsupdrequest = New eRemoteDB.Execute
		
		'+Definición de parámetros para stored procedure 'insupdrequest'
		'+Información leída el 26/11/2001
		With lrecinsupdrequest
			.StoredProcedure = "InsUpdRequest"
			.Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOrigin", nOrigin, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dNulldate", dNulldate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sTyp_surr", sTyp_surr, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sPayorder", sPayorder, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sNull_rec", sNull_rec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAmount", nAmount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDescript", sDescript, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nNotenum", nNotenum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nNullcode", nNullcode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTyp_rec", nTyp_rec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sReh_lrec", sReh_lrec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInterestrate", nInterestrate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 0, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRequest_nu", nRequest_nu, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTypepay", nTypepay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dMovdate", dMovDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCliPaySurr", sClipaysurr, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCertPaySurr", sCertpaysurr, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBraPaySurr", nBrapaysurr, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProPaySurr", nPropaysurr, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolPaySurr", nPolpaysurr, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCerPaySurr", nCerpaysurr, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAgency", nAgency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nSurr_reason", nSurr_reason, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nType_payment", nType_payment, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInstitution", nInstitution, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClientinstitution", sClientinstitution, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCommit", 1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 1, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sReturn_ind", sReturn_ind, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nReturn_Rat", nReturn_Rat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOrigin_apv", nOrigin_apv, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			InsUpdRequest = .Run(False)
		End With
		
insupdrequest_Err: 
		If Err.Number Then
			InsUpdRequest = False
		End If
		'UPGRADE_NOTE: Object lrecinsupdrequest may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsupdrequest = Nothing
		On Error GoTo 0
	End Function
	
	'%insPreCA767: Esta función lee los datos iniciales de la transacción "CA767"
	Public Function insPreCA767(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nOperat As Integer) As Boolean
		Dim lclsProduct_li As eProduct.Product
		
		On Error GoTo insPreCA767_Err
		
		
		insPreCA767 = True
		
		With Me
			If .Find(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, True) Then
				Select Case nOperat
					Case 2
						nStatquota = 1 '+ Pendiente
					Case 5
						nStatquota = 1 '+ Pendiente
					Case 3
						nStatquota = 3 '+ Rechazado
					Case 4
						nStatquota = 4 '+ Anulado
					Case 6
						nStatquota = 6 '+ Regularizar
				End Select
				
				lclsProduct_li = New eProduct.Product
				Call lclsProduct_li.FindProduct_li(nBranch, nProduct, dEffecdate)
				Me.nProdClass = lclsProduct_li.nProdClas
				'UPGRADE_NOTE: Object lclsProduct_li may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
				lclsProduct_li = Nothing
			End If
		End With
		
insPreCA767_Err: 
		If Err.Number Then
			insPreCA767 = False
		End If
		On Error GoTo 0
	End Function
	
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		sCertype = String.Empty
		nOrigin = eRemoteDB.Constants.intNull
		nBranch = eRemoteDB.Constants.intNull
		nProduct = eRemoteDB.Constants.intNull
		nPolicy = eRemoteDB.Constants.intNull
		nCertif = eRemoteDB.Constants.intNull
		dEffecdate = eRemoteDB.Constants.dtmNull
		dNulldate = eRemoteDB.Constants.dtmNull
		sTyp_surr = String.Empty
		sPayorder = String.Empty
		sNull_rec = String.Empty
		nAmount = eRemoteDB.Constants.intNull
		sDescript = String.Empty
		nNotenum = eRemoteDB.Constants.intNull
		nNullcode = eRemoteDB.Constants.intNull
		nTyp_rec = eRemoteDB.Constants.intNull
		sReh_lrec = String.Empty
		nUsercode = eRemoteDB.Constants.intNull
		nInterestrate = eRemoteDB.Constants.intNull
		nStatquota = eRemoteDB.Constants.intNull
		nRequest_nu = eRemoteDB.Constants.intNull
		nTypepay = eRemoteDB.Constants.intNull
		dMovDate = eRemoteDB.Constants.dtmNull
		sClipaysurr = String.Empty
		sCertpaysurr = String.Empty
		nBrapaysurr = eRemoteDB.Constants.intNull
		nPropaysurr = eRemoteDB.Constants.intNull
		nPolpaysurr = eRemoteDB.Constants.intNull
		nCerpaysurr = eRemoteDB.Constants.intNull
		nAgency = eRemoteDB.Constants.intNull
		nSurr_reason = eRemoteDB.Constants.intNull
		nType_payment = eRemoteDB.Constants.intNull
		nInstitution = eRemoteDB.Constants.intNull
		nProdClass = eRemoteDB.Constants.intNull
		nOrigin_apv = eRemoteDB.Constants.intNull
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
End Class






