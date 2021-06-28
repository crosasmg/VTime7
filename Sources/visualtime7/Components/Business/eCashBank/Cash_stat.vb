Option Strict Off
Option Explicit On
Public Class Cash_stat
	'%-------------------------------------------------------%'
	'% $Workfile:: Cash_stat.cls                            $%'
	'% $Author:: Nvaplat9                                   $%'
	'% $Date:: 4/08/04 3:42p                                $%'
	'% $Revision:: 34                                       $%'
	'%-------------------------------------------------------%'
	
	'          Name                                                  Null?    Type
	'          ----------------------------------------------------- -------- ------------------------------------
	
	Public nCashNum As Integer 'NOT NULL NUMBER(5)
	Public nCash_opertyp As Integer 'NOT NULL NUMBER(5)
	Public dStatDate As Date 'NOT NULL DATE
	Public nStatus As Integer 'NOT NULL NUMBER(5)
	Public nCash_id As Double 'NOT NULL NUMBER(10)
	Public dCompdate As Date 'NOT NULL DATE
	Public nUsercode As Integer 'NOT NULL NUMBER(5)
	Public sDsp_Status As String
	Public nResult As Integer
	Private nStatus_Aux As Integer
	Private dStatDate_Aux As Date
	Public nOfficeAgen As Integer
	Public sDescript As String
	Public sCliename As String
	Public sClient As String
	Public sDigit As String
	Public dStartDate As Date
	Public dInitCloseCash As Date
	Public dEndCloseCash As Date
	Public dCloseOkCash As Date
	Public sClientSup As String
	Public sDigitSup As String
	Public sClienameSup As String
	Public sClientHeadSup As String
	Public sDigitHeadSup As String
	Public sClienameHeadSup As String
	
	'% UpdCash_stat
	Public Function UpdCash_stat() As Boolean
		Dim lclsCash_stat As eRemoteDB.Execute
		
		On Error GoTo UpdCash_stat_Err
		
		lclsCash_stat = New eRemoteDB.Execute
		
		'+ Define all parameters for the stored procedures 'insudb.UpdCash_stat'.
		With lclsCash_stat
			.StoredProcedure = "UpdCash_stat"
			.Parameters.Add("nCashNum", nCashNum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("DEFFECDATE", dStatDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCash_opertyp", nCash_opertyp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			UpdCash_stat = .Run(False)
		End With
		
		'UPGRADE_NOTE: Object lclsCash_stat may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsCash_stat = Nothing
		
UpdCash_stat_Err: 
		If Err.Number Then
			UpdCash_stat = False
		End If
		On Error GoTo 0
	End Function
	
	'%valCash_statClosed: Valida que una determinada caja no este cerrada para una fecha en particular.
	'%True -> Si existe cierre para la caja y fecha pasada como parámetro
	'%False -> No existe cierre para la caja y fecha pasada como parámetro
	Public Function valCash_statClosed(ByVal nCashNum As Integer, ByVal dStatDate As Date) As Boolean
		Dim lrecReaCash_stat As eRemoteDB.Execute
		
		On Error GoTo valCash_statClosed_Err
		
		lrecReaCash_stat = New eRemoteDB.Execute
		
		valCash_statClosed = True
		
		With lrecReaCash_stat
			.StoredProcedure = "valCash_statClosed"
			.Parameters.Add("nCashNum", nCashNum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dStatdate", dStatDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				If .FieldToClass("nStatus") = 4 Or .FieldToClass("nStatus") = 6 Or .FieldToClass("nStatus") = 7 Or .FieldToClass("nStatus") = 9 Then
					valCash_statClosed = False
				Else
					valCash_statClosed = True
				End If
				.RCloseRec()
			End If
		End With
		
valCash_statClosed_Err: 
		If Err.Number Then
			valCash_statClosed = True
		End If
		'UPGRADE_NOTE: Object lrecReaCash_stat may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecReaCash_stat = Nothing
		On Error GoTo 0
	End Function
	
	'FindCash_stat: Función que realiza la busqueda de los datos de una caja determinada
	Public Function FindCash_stat(ByVal dStatDate As Date, ByVal nCashNum As Integer) As Integer
		Dim lrecvalCash_statClosed As eRemoteDB.Execute
		
		On Error GoTo FindCash_stat_Err
		
		lrecvalCash_statClosed = New eRemoteDB.Execute
		
		FindCash_stat = 0
		
		With lrecvalCash_statClosed
			.StoredProcedure = "valCash_statClosed"
			.Parameters.Add("nCashNum", nCashNum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dStatDate", dStatDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				FindCash_stat = .FieldToClass("nStatus")
				.RCloseRec()
			End If
		End With
		'UPGRADE_NOTE: Object lrecvalCash_statClosed may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecvalCash_statClosed = Nothing
		
FindCash_stat_Err: 
		If Err.Number Then
			FindCash_stat = 0
		End If
		On Error GoTo 0
	End Function
	
	'FindDateCash_stat: Función que realiza la busqueda de la fecha del estado actual de la caja
	Public Function FindDateCash_stat(ByVal nCashNum As Integer) As Boolean
		Dim lrecvalCash_statClosed As eRemoteDB.Execute
		
		On Error GoTo FindDateCash_stat_Err
		
		lrecvalCash_statClosed = New eRemoteDB.Execute
		
		With lrecvalCash_statClosed
			.StoredProcedure = "ReaDateCash_Stat"
			.Parameters.Add("nCashNum", nCashNum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				FindDateCash_stat = True
				nStatus_Aux = .FieldToClass("nStatus")
				dStatDate_Aux = .FieldToClass("dStatDate")
				.RCloseRec()
			Else
				FindDateCash_stat = False
			End If
		End With
		'UPGRADE_NOTE: Object lrecvalCash_statClosed may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecvalCash_statClosed = Nothing
		
FindDateCash_stat_Err: 
		If Err.Number Then
			FindDateCash_stat = False
		End If
		On Error GoTo 0
	End Function
	
	
	
	'ValCashmov_square: Función que valida que la caja este cuadrada
	Public Function ValCashmov_square(ByVal dStatDate As Date, ByVal nCashNum As Integer) As Integer
		Dim lrecValcash_Mov_Square As eRemoteDB.Execute
		
		On Error GoTo ValCashmov_square_Err
		
		lrecValcash_Mov_Square = New eRemoteDB.Execute
		nResult = 0
		
		ValCashmov_square = 0
		
		With lrecValcash_Mov_Square
			.StoredProcedure = "Valcash_Mov_Square"
			.Parameters.Add("nCashNum", nCashNum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dStatDate", dStatDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nStatus", nResult, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				nResult = .Parameters("nStatus").Value
				ValCashmov_square = nResult
			End If
		End With
		'UPGRADE_NOTE: Object lrecValcash_Mov_Square may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecValcash_Mov_Square = Nothing
		
ValCashmov_square_Err: 
		If Err.Number Then
			ValCashmov_square = 0
		End If
		On Error GoTo 0
	End Function
	
	'insValOPL719_k: Función que realiza la validacion de los datos introducidos en la ventana
	Public Function insValOPL719_k(ByVal sCodispl As String, ByVal nCash_opertyp As Integer, ByVal dStatDate As Date, ByVal nCashNum As Integer, ByVal nUsercode As Integer, ByVal nCheckPrint As String) As String
		Dim lclsErrors As eFunctions.Errors
		Dim lclsUser_cashnum As eCashBank.User_cashnum
		Dim lclsCtrol_date As eGeneral.Ctrol_date
		Dim nStatus As Integer
		Dim nSquare As Integer
		
		On Error GoTo insValOPL719_k_Err
		
		lclsErrors = New eFunctions.Errors
		lclsUser_cashnum = New eCashBank.User_cashnum
		lclsCtrol_date = New eGeneral.Ctrol_date
		

		'+ Se valida la fecha
		If dStatDate = dtmNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 60108)
		End If

        If sCodispl = "OPL729" Then
        Else
            '+ Se valida el tipo de operación
            If nCash_opertyp = eRemoteDB.Constants.intNull And CDbl(nCheckPrint) <= 0 Then
                Call lclsErrors.ErrorMessage(sCodispl, 60804)
            End If

            '+ Se valida el número de la caja
            If nCashNum = eRemoteDB.Constants.intNull Then
                Call lclsErrors.ErrorMessage(sCodispl, 60007)
            Else
                If lclsUser_cashnum.Find(nCashNum) Then
                    If dStatDate <> dtmNull Then

                        '+ Listado.
                        If CDbl(nCheckPrint) > 0 And nCash_opertyp = eRemoteDB.Constants.intNull And Not Val_CashNum_Init(dStatDate, nCashNum) Then
                            Call lclsErrors.ErrorMessage(sCodispl, 60596)
                        End If

                        If nCash_opertyp <> eRemoteDB.Constants.intNull And nCash_opertyp <> 9 And nCash_opertyp <> 4 Then
                            nSquare = ValCashmov_square(dStatDate, nCashNum)
                            If nSquare = 0 Then
                                Call lclsErrors.ErrorMessage(sCodispl, 60120)
                            End If
                        End If
                        nStatus = FindCash_stat_1(nCashNum, dStatDate)

                        If nCash_opertyp <> eRemoteDB.Constants.intNull Then
                            Select Case nCash_opertyp
                                '+ Cierre Preliminar.
                                Case 1
                                    If nStatus <> 9 And nStatus <> 4 And nStatus <> 1 Then
                                        Call lclsErrors.ErrorMessage(sCodispl, 60460)
                                    End If

                                    If lclsUser_cashnum.nUser <> nUsercode Then
                                        Call lclsErrors.ErrorMessage(sCodispl, 60466)
                                    End If

                                    If Not ValRelIncomplete(nCashNum, dStatDate) Then
                                        Call lclsErrors.ErrorMessage(sCodispl, 55159)
                                    End If
                                    '+ Cierre Definitivo.
                                Case 2
                                    If nStatus <> 1 And nStatus <> 6 Then
                                        Call lclsErrors.ErrorMessage(sCodispl, 60114)
                                    End If
                                    If lclsUser_cashnum.nCashSup <> nUsercode Then
                                        Call lclsErrors.ErrorMessage(sCodispl, 60466)
                                    End If
                                    '+ Cierre Aprobación.
                                Case 3
                                    If nStatus <> 2 And nStatus <> 7 Then
                                        Call lclsErrors.ErrorMessage(sCodispl, 60115)
                                    End If
                                    If lclsUser_cashnum.nHeadSup <> nUsercode Then
                                        Call lclsErrors.ErrorMessage(sCodispl, 60466)
                                    End If
                                    '+ Reapertura de caja.
                                Case 4
                                    If Not ValDate_OpenCash(nCashNum, dStatDate) Then
                                        Call lclsErrors.ErrorMessage(sCodispl, 1006)
                                    End If
                                    If lclsCtrol_date.Find(5) Then
                                        If lclsCtrol_date.dEffecdate > dStatDate Then
                                            Call lclsErrors.ErrorMessage(sCodispl, 1008)
                                        End If
                                    Else
                                        Call lclsErrors.ErrorMessage(sCodispl, 1008)
                                    End If
                                    '+ Se verifica el estado de la caja
                                    If nStatus <> 1 And nStatus <> 6 Then
                                        Call lclsErrors.ErrorMessage(sCodispl, 60470)
                                    End If
                                    If lclsUser_cashnum.nUser <> nUsercode Then
                                        Call lclsErrors.ErrorMessage(sCodispl, 60466)
                                    End If

                                    '+ Reapertura de supervisor caja.
                                Case 6
                                    If nStatus <> 2 And nStatus <> 7 Then
                                        Call lclsErrors.ErrorMessage(sCodispl, 60469)
                                    End If
                                    If lclsUser_cashnum.nCashSup <> nUsercode Then
                                        Call lclsErrors.ErrorMessage(sCodispl, 60468)
                                    End If
                                    '+ Reapertura de supervisor jefe.
                                Case 7
                                    If nStatus <> 3 Then
                                        Call lclsErrors.ErrorMessage(sCodispl, 60467)
                                    End If
                                    If lclsUser_cashnum.nHeadSup <> nUsercode Then
                                        Call lclsErrors.ErrorMessage(sCodispl, 60468)
                                    End If
                                    '+ Inicio de Caja.
                                Case 9
                                    If Val_Hollidays(dStatDate) Then
                                        Call lclsErrors.ErrorMessage(sCodispl, 10081, , , ". Fecha ingresada no corresponde a un Día Hábil")
                                    End If

                                    If FindDateCash_stat(nCashNum) Then
                                        If dStatDate <= dStatDate_Aux Then
                                            Call lclsErrors.ErrorMessage(sCodispl, 60577)
                                        End If

                                        '+ Se validad si caja aun sigue abierta, para no volver a iniciar otra si no esta cerrada
                                        'If nStatus_Aux = nCash_opertyp Then
                                        '    Call lclsErrors.ErrorMessage(sCodispl, 60834)
                                        'End If
                                    End If

                                    If lclsUser_cashnum.nUser <> nUsercode Then
                                        Call lclsErrors.ErrorMessage(sCodispl, 1102)
                                    End If

                            End Select
                        End If
                    End If
                Else
                    Call lclsErrors.ErrorMessage(sCodispl, 60803)
                End If
            End If
        End If
        insValOPL719_k = lclsErrors.Confirm

insValOPL719_k_Err:
        If Err.Number Then
            insValOPL719_k = insValOPL719_k & Err.Description
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsErrors = Nothing
        'UPGRADE_NOTE: Object lclsUser_cashnum may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsUser_cashnum = Nothing
        'UPGRADE_NOTE: Object lclsCtrol_date may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsCtrol_date = Nothing
    End Function
	
	'insPostOPL719_K: Función que realiza la validación de los datos ingresados en la ventana
	Public Function insPostOPL719_K(ByVal nCash_opertyp As Integer, ByVal dStatDate As Date, ByVal nCashNum As Integer, ByVal nUsercode As Integer) As Boolean
		If nCash_opertyp > 0 Then
			If nCash_opertyp <> 9 Then
				'+Si la caja no esta cuadrada no se cambia el estado de la caja
				If ValCashmov_square(dStatDate, nCashNum) <> 0 Then
					Me.nCash_opertyp = nCash_opertyp
					Me.dStatDate = dStatDate
					Me.nCashNum = nCashNum
					Me.nUsercode = nUsercode
					If ValRelIncomplete(nCashNum, dStatDate) Then
						insPostOPL719_K = UpdCash_stat()
					Else
						insPostOPL719_K = True
					End If
				Else
					insPostOPL719_K = True
				End If
			Else
				Me.nCash_opertyp = nCash_opertyp
				Me.dStatDate = dStatDate
				Me.nCashNum = nCashNum
				Me.nUsercode = nUsercode
				insPostOPL719_K = UpdCash_stat()
			End If
		Else
			insPostOPL719_K = True
		End If
	End Function
	
	'%insValOPC720: Esta función se encarga de validar los datos introducidos en la zona de detalle
	Public Function insValOPC720(ByVal sCodispl As String, ByVal nCashNum As Integer, ByVal dStatDate As Date, ByVal nStatus As Integer, ByVal nCash_id As Double, ByVal nOfficeAgen As Integer, ByVal dInitDate As Date, ByVal dEndDate As Date, ByVal dCloseDate As Date) As String
		Dim lclsErrors As eFunctions.Errors
		
		On Error GoTo insValOPC720_Err
		
		lclsErrors = New eFunctions.Errors
		
		'+ Verifica que exista por lo menos una condición de búsqueda
		If nCashNum <= 0 And dStatDate = dtmNull And nStatus <= 0 And nCash_id <= 0 And nOfficeAgen <= 0 And dInitDate = dtmNull And dEndDate = dtmNull And dCloseDate = dtmNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 99022)
		End If
		
		insValOPC720 = lclsErrors.Confirm
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		
insValOPC720_Err: 
		If Err.Number Then
			insValOPC720 = "insValOPC720" & Err.Description
		End If
		On Error GoTo 0
	End Function
	
	'FindCash_stat_1: Función que realiza la busqueda del estado actual de una caja
	Public Function FindCash_stat_1(ByVal nCashNum As Integer, ByVal dStatDate As Date) As Integer
		Dim lrecReaCash_stat_1 As eRemoteDB.Execute
		On Error GoTo FindCash_stat_1_Err
		
		lrecReaCash_stat_1 = New eRemoteDB.Execute
		
		nResult = 0
		
		With lrecReaCash_stat_1
			.StoredProcedure = "ReaCash_stat_1"
			.Parameters.Add("nCashNum", nCashNum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dStatDate", dStatDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nStatus", nResult, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				nResult = .Parameters("nStatus").Value
			End If
			FindCash_stat_1 = nResult
		End With
		
FindCash_stat_1_Err: 
		If Err.Number Then
			FindCash_stat_1 = 0
		End If
		'UPGRADE_NOTE: Object lrecReaCash_stat_1 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecReaCash_stat_1 = Nothing
		On Error GoTo 0
	End Function
	
	'ValDate_OpenCash: Función que valida la fecha de reapertura de una caja
	Public Function ValDate_OpenCash(ByVal nCashNum As Integer, ByVal dDateOpen As Date) As Boolean
		Dim lrecValDate_OpenCash As eRemoteDB.Execute
		On Error GoTo ValDate_OpenCash_Err
		
		lrecValDate_OpenCash = New eRemoteDB.Execute
		
		With lrecValDate_OpenCash
			.StoredProcedure = "ValDate_OpenCash"
			.Parameters.Add("nCashNum", nCashNum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dDateOpen", dDateOpen, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nValidate", 2, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				ValDate_OpenCash = .Parameters("nValidate").Value = 1
			End If
		End With
		
ValDate_OpenCash_Err: 
		If Err.Number Then
			ValDate_OpenCash = False
		End If
		'UPGRADE_NOTE: Object lrecValDate_OpenCash may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecValDate_OpenCash = Nothing
		On Error GoTo 0
	End Function
	
	'ValRelIncomplete: Función que valida si una caja tiene relaciones incompletas a una fecha dada
	Public Function ValRelIncomplete(ByVal nCashNum As Integer, ByVal dCollect As Date) As Boolean
		Dim lrecValRelIncomplete As eRemoteDB.Execute
		On Error GoTo ValRelIncomplete_Err
		
		lrecValRelIncomplete = New eRemoteDB.Execute
		
		With lrecValRelIncomplete
			.StoredProcedure = "InsValRelIncomplete"
			.Parameters.Add("nCashNum", nCashNum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dDateOpen", dCollect, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nValid", 2, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				ValRelIncomplete = .Parameters("nValid").Value = 1
			End If
		End With
		
ValRelIncomplete_Err: 
		If Err.Number Then
			ValRelIncomplete = False
		End If
		'UPGRADE_NOTE: Object lrecValRelIncomplete may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecValRelIncomplete = Nothing
		On Error GoTo 0
	End Function
	
	'val_hollidays: Función que valida la fecha corresponda a un día habil
	Public Function Val_Hollidays(ByVal dDateOpen As Date) As Boolean
		Dim lrecVal_Hollidays As eRemoteDB.Execute
		On Error GoTo Val_Hollidays_Err
		
		lrecVal_Hollidays = New eRemoteDB.Execute
		
		With lrecVal_Hollidays
			.StoredProcedure = "Val_Hollidays"
			.Parameters.Add("dDateOpen", dDateOpen, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nValid", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				Val_Hollidays = .Parameters("nValid").Value = 0
				'Else
				'Val_Hollidays = False
			End If
		End With
		
Val_Hollidays_Err: 
		If Err.Number Then
			Val_Hollidays = False
		End If
		'UPGRADE_NOTE: Object lrecVal_Hollidays may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecVal_Hollidays = Nothing
		On Error GoTo 0
	End Function
	
	'Val_CashNum_Init: Función que valida Inicio de caja
	Public Function Val_CashNum_Init(ByVal dStatDate As Date, ByVal nCashNum As Integer) As Boolean
		Dim lrecVal_CashNum_Init As eRemoteDB.Execute
		On Error GoTo Val_CashNum_Init_Err
		
		lrecVal_CashNum_Init = New eRemoteDB.Execute
		
		With lrecVal_CashNum_Init
			.StoredProcedure = "Val_CashNum_Init"
			.Parameters.Add("dStatDate", dStatDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCashNum", nCashNum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nValid", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				Val_CashNum_Init = .Parameters("nValid").Value = 1
			End If
		End With
		
Val_CashNum_Init_Err: 
		If Err.Number Then
			Val_CashNum_Init = False
		End If
		'UPGRADE_NOTE: Object lrecVal_CashNum_Init may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecVal_CashNum_Init = Nothing
		On Error GoTo 0
	End Function
End Class






