Option Strict Off
Option Explicit On
Public Class OptFinance
	'%-------------------------------------------------------%'
	'% $Workfile:: OptFinance.cls                           $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 2/06/04 1:17p                                $%'
	'% $Revision:: 7                                        $%'
	'%-------------------------------------------------------%'
	'-
	'- Estructura de tabla Opt_financ al 09-18-2002 11:44:54
	'-  Property                       Type         DBType   Size Scale  Prec  Null
	Public nOpt_draft As Integer ' NUMBER     22   0     5    S
	Public sCh_opt_dra As String ' CHAR       1    0     0    S
	Public nLevel_dra As Integer ' NUMBER     22   0     5    S
	Public nOpt_null As Integer ' NUMBER     22   0     5    S
	Public sCh_opt_nul As String ' CHAR       1    0     0    S
	Public nLevel_nul As Integer ' NUMBER     22   0     5    S
	Public nDefaulti As Double ' NUMBER     22   2     4    S
	Public sCh_up As String ' CHAR       1    0     0    S
	Public nInt_up As Double ' NUMBER     22   2     6    S
	Public sCh_down As String ' CHAR       1    0     0    S
	Public nInt_down As Double ' NUMBER     22   2     6    S
	Public nLevel_fin As Integer ' NUMBER     22   0     5    S
	Public sOpt_intere As String ' CHAR       1    0     0    S
	Public sCh_opt_int As String ' CHAR       1    0     0    S
	Public nLevel_initial As Integer ' NUMBER     22   0     5    S
	Public sInterest_e As String ' CHAR       1    0     0    S
	Public sTime_exa As String ' CHAR       1    0     0    S
	Public nIntdelay As Double ' NUMBER     22   2     4    S
	Public sCh_del_up As String ' CHAR       1    0     0    S
	Public nInt_del_up As Double ' NUMBER     22   2     6    S
	Public sCh_del_down As String ' CHAR       1    0     0    S
	Public nInt_del_down As Double ' NUMBER     22   2     6    S
	Public nLevel_delay As Integer ' NUMBER     22   0     5    S
	Public nOpt_comm As Integer ' NUMBER     22   0     5    S
	Public sCh_opt_com As String ' CHAR       1    0     0    S
	Public nLevel_comm As Integer ' NUMBER     22   0     5    S
	Public nDscto_pag As Double ' NUMBER     22   2     4    S
	Public nDscto_amo As Double ' NUMBER     22   2     10   S
	Public nCurrency As Integer ' NUMBER     22   0     5    S
	Public sCh_pay_up As String ' CHAR       1    0     0    S
	Public nPay_up As Double ' NUMBER     22   2     6    S
	Public sCh_pay_down As String ' CHAR       1    0     0    S
	Public nPay_down As Double ' NUMBER     22   2     6    S
	Public nLevel_pay As Integer ' NUMBER     22   0     5    S
	Public dCompdate As Date ' DATE       7    0     0    N
	Public nUsercode As Integer ' NUMBER     22   0     5    N
	
	'-Se define el tipo enumerado que indica el tiempo a utilizar "Exacto" ó "Aproximado".
	
	Public Enum eTime_exa
		etExact = 1
		etApproximate = 2
	End Enum
	
	'-Se define la variable para el estado de los permisos
	
	Public Enum PermissionState
		Affirmative = 1
		Negative = 2
	End Enum
	
	'%Find: Este metodo carga la coleccion de elementos de la tabla "Opt_financ" devolviendo Verdadero o
	'%falso, dependiendo de la existencia de los registros.
	Public Function Find() As Boolean
		Dim lrecreaOpt_financ As eRemoteDB.Execute
		
		lrecreaOpt_financ = New eRemoteDB.Execute
		
		'+ Definición de parámetros para stored procedure 'insudb.reaClient'
		'+ Información leída el 11/08/1999 09:10:00 PM
		
		With lrecreaOpt_financ
			.StoredProcedure = "reaOpt_financ"
			If .Run Then
				nOpt_draft = .FieldToClass("nOpt_draft")
				sCh_opt_dra = .FieldToClass("sCh_opt_dra")
				nLevel_dra = .FieldToClass("nLevel_dra")
				nOpt_null = .FieldToClass("nOpt_null")
				sCh_opt_nul = .FieldToClass("sCh_opt_nul")
				nLevel_nul = .FieldToClass("nLevel_nul")
				nDefaulti = .FieldToClass("nDefaulti")
				sCh_up = .FieldToClass("sCh_up")
				nInt_up = .FieldToClass("nInt_up")
				sCh_down = .FieldToClass("sCh_down")
				nInt_down = .FieldToClass("nInt_down")
				nLevel_fin = .FieldToClass("nLevel_fin")
				sOpt_intere = .FieldToClass("sOpt_intere")
				sCh_opt_int = .FieldToClass("sCh_opt_int")
				nLevel_initial = .FieldToClass("nLevel_Initial")
				sInterest_e = .FieldToClass("sInterest_e")
				sTime_exa = .FieldToClass("sTime_exa")
				nIntdelay = .FieldToClass("nIntDelay")
				sCh_del_up = .FieldToClass("sCh_del_up")
				nInt_del_up = .FieldToClass("nInt_del_up")
				sCh_del_down = .FieldToClass("sCh_del_down")
				nInt_del_down = .FieldToClass("nInt_del_down")
				nLevel_delay = .FieldToClass("nLevel_delay")
				nOpt_comm = .FieldToClass("nOpt_comm")
				sCh_opt_com = .FieldToClass("sCh_opt_com")
				nLevel_comm = .FieldToClass("nLevel_comm")
				nDscto_pag = .FieldToClass("nDscto_pag")
				If (nDscto_pag = eRemoteDB.Constants.intNull) Then nDscto_pag = 0
				nDscto_amo = .FieldToClass("nDscto_amo")
				If (nDscto_amo = eRemoteDB.Constants.intNull) Then nDscto_amo = 0
				nCurrency = .FieldToClass("nCurrency")
				sCh_pay_up = .FieldToClass("sCh_pay_up")
				nPay_up = .FieldToClass("nPay_up")
				If nPay_up = eRemoteDB.Constants.intNull Then nPay_up = 0
				sCh_pay_down = .FieldToClass("sCh_pay_down")
				nPay_down = .FieldToClass("nPay_down")
				If nPay_down = eRemoteDB.Constants.intNull Then nPay_down = 0
				nLevel_pay = .FieldToClass("nLevel_pay")
				
				.RCloseRec()
				Find = True
			Else
				Find = False
			End If
		End With
		
		'UPGRADE_NOTE: Object lrecreaOpt_financ may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaOpt_financ = Nothing
	End Function
End Class






