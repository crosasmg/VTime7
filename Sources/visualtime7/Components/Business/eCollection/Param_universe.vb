Option Strict Off
Option Explicit On
Public Class Param_universe
	'%-------------------------------------------------------%'
	'% $Workfile:: Param_universe.cls                       $%'
	'% $Author:: Nvaplat19                                  $%'
	'% $Date:: 25/08/03 6:46p                               $%'
	'% $Revision:: 6                                        $%'
	'%-------------------------------------------------------%'
	'+ Propiedades según la tabla en el sistema el 25/10/2000.
	'+ Los campos llaves corresponden a nBank_Code.
	'Name                                                  Null?    Type
	'----------------------------------------------------- -------- ------------------------------------
	Public nBank_code As Integer
	Public dUniEffect As Date
	Public nPosIniReg As Integer
	Public nPosEndReg As Integer
	Public sIndTypReg As String
	Public sIndTypTot As String
	Public nPosIniCli As Integer
	Public nPosEndCli As Integer
	Public nPosIniStat As Integer
	Public nPosEndStat As Integer
	Public sIndStat As String
	Public dCompdate As Date
	Public nUsercode As Integer
	
	'% Find: Busca los datos correspondiente a un recibo en la tabla Premium.
	Public Function Find(ByVal Bank_Code As Double, Optional ByVal lblnFind As Boolean = False) As Boolean
		Dim lRecReaParam_Universe As eRemoteDB.Execute
		
		lRecReaParam_Universe = New eRemoteDB.Execute
		
		If Bank_Code = nBank_code Or lblnFind Then
			Find = True
		Else
			
			'Definición de parámetros para stored procedure 'insudb.reaPremiumF_Receipt'
			'Información leída el 23/09/1999 1:02:48 PM
			
			With lRecReaParam_Universe
				'.StoredProcedure = "ReaParam_Universepkg.ReaParam_Universe"
				.StoredProcedure = "ReaParam_Universe"
				.Parameters.Add("nBank_Code", Bank_Code, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If .Run Then
					dUniEffect = .FieldToClass("dUniEffect", eRemoteDB.Constants.dtmNull)
					nPosIniReg = .FieldToClass("nPosIniReg", eRemoteDB.Constants.intNull)
					nPosEndReg = .FieldToClass("nPosEndReg", eRemoteDB.Constants.intNull)
					sIndTypReg = .FieldToClass("sIndTypReg", eRemoteDB.Constants.strNull)
					sIndTypTot = .FieldToClass("sIndTypTot", eRemoteDB.Constants.strNull)
					nPosIniCli = .FieldToClass("nPosIniCli", eRemoteDB.Constants.intNull)
					nPosEndCli = .FieldToClass("nPosEndCli", eRemoteDB.Constants.intNull)
					nPosIniStat = .FieldToClass("nPosIniStat", eRemoteDB.Constants.intNull)
					nPosEndStat = .FieldToClass("nPosEndStat", eRemoteDB.Constants.intNull)
					sIndStat = .FieldToClass("sIndStat", eRemoteDB.Constants.strNull)
					nUsercode = .FieldToClass("nUsercode", eRemoteDB.Constants.intNull)
					Find = True
					.RCloseRec()
				Else
					Find = False
				End If
			End With
			'UPGRADE_NOTE: Object lRecReaParam_Universe may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			lRecReaParam_Universe = Nothing
		End If
	End Function
	
	'Update: Funcion que realiza la actualización de los campos de la tabla client dependiendo del código de cliente
	Public Function Update() As Boolean
		Dim lRecReaParam_Universe As eRemoteDB.Execute
		
		lRecReaParam_Universe = New eRemoteDB.Execute
		
		'Definición de parámetros para stored procedure 'insudb.updClientBC001J'
		'Información leída el 01/03/2000 09:57:19 AM
		Update = False
		With lRecReaParam_Universe
			.StoredProcedure = "updParam_Universe"
			.Parameters.Add("sBank_Code", nBank_code, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dUniEffect", dUniEffect, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPosIniReg", nPosIniReg, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPosEndReg", nPosEndReg, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sIndTypReg", sIndTypReg, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sIndTypTot", sIndTypTot, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPosIniCli", nPosIniCli, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPosEndCli", nPosEndCli, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPosIniStat", nPosIniStat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPosEndStat", nPosEndStat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sIndStat", sIndStat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 13, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Update = .Run(False)
		End With
		'UPGRADE_NOTE: Object lRecReaParam_Universe may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lRecReaParam_Universe = Nothing
	End Function
	
	
	Public Function Add() As Boolean
		Dim lRecReaParam_Universe As eRemoteDB.Execute
		
		lRecReaParam_Universe = New eRemoteDB.Execute
		'Definición de parámetros para stored procedure 'insudb.creClientCode'
		'Información leída el 26/11/99 14:07:10
		Add = False
		With lRecReaParam_Universe
			.StoredProcedure = "creParam_Universe"
			.Parameters.Add("sBank_Code", nBank_code, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dUniEffect", dUniEffect, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPosIniReg", nPosIniReg, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPosEndReg", nPosEndReg, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sIndTypReg", sIndTypReg, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sIndTypTot", sIndTypTot, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPosIniCli", nPosIniCli, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPosEndCli", nPosEndCli, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPosIniStat", nPosIniStat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPosEndStat", nPosEndStat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sIndStat", sIndStat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 13, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Add = .Run(False)
		End With
		'UPGRADE_NOTE: Object lRecReaParam_Universe may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lRecReaParam_Universe = Nothing
	End Function
	
	
	Public Function Delete() As Boolean
		Dim lRecReaParam_Universe As eRemoteDB.Execute
		
		lRecReaParam_Universe = New eRemoteDB.Execute
		'Definición de parámetros para stored procedure 'insudb.creClientCode'
		'Información leída el 26/11/99 14:07:10
		Delete = False
		With lRecReaParam_Universe
			.StoredProcedure = "delParam_Universe"
			
			.Parameters.Add("sBank_Code", nBank_code, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Delete = .Run(False)
		End With
		'UPGRADE_NOTE: Object lRecReaParam_Universe may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lRecReaParam_Universe = Nothing
	End Function
End Class






