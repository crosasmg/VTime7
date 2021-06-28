Option Strict Off
Option Explicit On
<System.Runtime.InteropServices.ProgId("UL_Curr_Acc_Pol_NET.UL_Curr_Acc_Pol")> Public Class UL_Curr_Acc_Pol
	'%-------------------------------------------------------%'
	'% $Workfile:: ul_curr_acc_pol.cls                      $%'
	'% $Author:: Nvaplat41                                  $%'
	'% $Date:: 9/10/03 19.02                                $%'
	'% $Revision:: 10                                       $%'
	'%-------------------------------------------------------%'
	
	'- Propiedades de la clase ul_curr_acc_pol basado en la tabla ul_curr_acc_pol
	
	'+ Column_name                     Type           Nullable
	'+ ------------------------------  -------------- --------
	Public sCertype As String 'CHAR (1)      NOT NULL
	Public nBranch As Integer 'NUMBER (5)    NOT NULL
	Public nProduct As Integer 'NUMBER (5)    NOT NULL
	Public nPolicy As Double 'NUMBER (10)   NOT NULL
	Public nCertif As Double 'NUMBER (10)   NOT NULL
	Public nCurrency As Integer 'NUMBER (5)    NOT NULL
	Public nBalance As Double 'NUMBER (14,6) NOT NULL
	Public nCredit As Double 'NUMBER (14,6) NOT NULL
	Public nDebit As Double 'NUMBER (14,6) NOT NULL
	Public nLed_Compan As Integer 'NUMBER (5)
	Public sAccount As String 'CHAR (20)
	Public sAux_Accoun As String 'CHAR (20)
	Public dCompdate As Date 'DATE          NOT NULL
	Public nUsercode As Integer 'NUMBER (5)    NOT NULL
	
	'% Add: Esta función crea un registro en la tabla de cuenta de ahorros de productos Unit Link
	Public Function Add() As Boolean
		Dim lrecUl_Curr_acc_pol As eRemoteDB.Execute
		
		On Error GoTo ErrorHandler
		
		lrecUl_Curr_acc_pol = New eRemoteDB.Execute
		
		Add = False
		
		With lrecUl_Curr_acc_pol
			.StoredProcedure = "creUL_Curr_Acc_Pol"
			
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBalance", nBalance, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDebit", nDebit, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCredit", nCredit, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nLed_Compan", nLed_Compan, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If sAccount = String.Empty Then
				'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
				.Parameters.Add("sAccount", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Else
				.Parameters.Add("sAccount", sAccount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			End If
			
			If sAccount = sAux_Accoun Then
				'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
				.Parameters.Add("sAux_Accoun", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Else
				.Parameters.Add("sAux_Accoun", sAux_Accoun, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			End If
			
			Add = .Run(False)
		End With
		
		'UPGRADE_NOTE: Object lrecUl_Curr_acc_pol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecUl_Curr_acc_pol = Nothing
		
		Exit Function
ErrorHandler: 
		'UPGRADE_NOTE: Object lrecUl_Curr_acc_pol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecUl_Curr_acc_pol = Nothing
		
		Add = False
	End Function
End Class






