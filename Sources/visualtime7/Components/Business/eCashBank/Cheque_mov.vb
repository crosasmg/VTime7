Option Strict Off
Option Explicit On
Public Class Cheque_mov
	'%-------------------------------------------------------%'
	'% $Workfile:: Cheque_mov.cls                           $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:35p                                $%'
	'% $Revision:: 4                                        $%'
	'%-------------------------------------------------------%'
	
	'+ Propiedades según la tabla en el sistema al 17/07/2002.
	
	'        Name                          Type            Null?
	'--------------------------------- ------------ ----------------------------
	Public nCompany As Integer ' NOT NULL NUMBER(5)
	Public nBank_code As Integer ' NOT NULL NUMBER(10)
	Public sChequenu As String ' NOT NULL CHAR(10)
	Public dEffecdate As Date ' NOT NULL DATE
	Public nCheopertyp As Integer ' NOT NULL NUMBER(5)
	Public nCheoper_Reason As Integer '
	Public nBank_inter As Integer '
	Public sChequeinter As String '
	Public dNew_Date As Date '
	Public sDepnumber As String '
	Public nUsercode As Integer ' NOT NULL NUMBER(5)
	Public dCompdate As Date ' NOT NULL DATE
	Public nFin_Int As Double '
	
	'%FindChequeByBank: Devuelve un número de cheque de la tabla "Cheque_mov" asociado a un banco
	Public Function FindChequeByBank(ByVal sChequenu As String, ByVal nBank_code As Integer) As Boolean
		
		'-Se define la variable lrecreaChequeByBank
		
		Dim lrecreaChequeByBank As eRemoteDB.Execute
		
		On Error GoTo FindChequeByBank_Err
		
		lrecreaChequeByBank = New eRemoteDB.Execute
		
		'+Definición de parámetros para stored procedure 'insudb.reaChequeByBank'
		'+Información leída el 07/02/2001 8:47:47
		
		With lrecreaChequeByBank
			.StoredProcedure = "reaChequeByBank"
			.Parameters.Add("sChequenu", sChequenu, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBank_Code", nBank_code, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				nCompany = .FieldToClass("nCompany")
				nBank_code = .FieldToClass("nBank_Code")
				sChequenu = .FieldToClass("sChequenu")
				dEffecdate = .FieldToClass("dEffecdate")
				nCheopertyp = .FieldToClass("nCheopertyp")
				nCheoper_Reason = .FieldToClass("nCheoper_Reason")
				nBank_inter = .FieldToClass("nBank_inter")
				sChequeinter = .FieldToClass("sChequeinter")
				dNew_Date = .FieldToClass("dNew_Date")
				sDepnumber = .FieldToClass("sDepnumber")
				nUsercode = .FieldToClass("nUsercode")
				dCompdate = .FieldToClass("dCompdate")
				nFin_Int = .FieldToClass("nFin_Int")
				FindChequeByBank = True
				.RCloseRec()
			Else
				FindChequeByBank = False
			End If
		End With
		
FindChequeByBank_Err: 
		If Err.Number Then
			FindChequeByBank = False
		End If
		'UPGRADE_NOTE: Object lrecreaChequeByBank may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaChequeByBank = Nothing
		On Error GoTo 0
	End Function
End Class






