Option Strict Off
Option Explicit On
Public Class Bank_Universe
	'%-------------------------------------------------------%'
	'% $Workfile:: Bank_Universe.cls                        $%'
	'% $Author:: Nvaplat19                                  $%'
	'% $Date:: 25/08/03 6:46p                               $%'
	'% $Revision:: 4                                        $%'
	'%-------------------------------------------------------%'
	'+ Propiedades según la tabla en el sistema el 25/10/2000.
	'+ Los campos llaves corresponden a nBank_Code.
	'Name                                                  Null?    Type
	'----------------------------------------------------- -------- ------------------------------------
	Public nBank_code As Double
	Public sClient As String
	Public dCompdate As Date
	Public nUsercode As Integer
	
	'% Find: Busca los datos correspondiente a un recibo en la tabla Premium.
	Public Function Find(ByVal Bank_Code As Double, ByVal Client As String, Optional ByVal lblnFind As Boolean = False) As Boolean
		Dim lRecReaBank_Universe As eRemoteDB.Execute
		
		lRecReaBank_Universe = New eRemoteDB.Execute
		
		If (Bank_Code = nBank_code And Client = sClient) And Not lblnFind Then
			Find = True
		Else
			
			'Definición de parámetros para stored procedure 'insudb.reaPremiumF_Receipt'
			'Información leída el 23/09/1999 1:02:48 PM
			
			With lRecReaBank_Universe
				'.StoredProcedure = "ReaBank_Universepkg.ReaBank_Universe"
				.StoredProcedure = "ReaBank_Universe"
				.Parameters.Add("nBank_Code", Bank_Code, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("sClient", Client, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If .Run Then
					dCompdate = .FieldToClass("dCompdate")
					nUsercode = .FieldToClass("nUsercode")
					Find = True
					.RCloseRec()
				Else
					Find = False
				End If
			End With
			'UPGRADE_NOTE: Object lRecReaBank_Universe may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			lRecReaBank_Universe = Nothing
		End If
	End Function
	
	'Update: Funcion que realiza la actualización de los campos de la tabla client dependiendo del código de cliente
	Public Function Update() As Boolean
		Dim lRecReaBank_Universe As eRemoteDB.Execute
		
		lRecReaBank_Universe = New eRemoteDB.Execute
		
		'Definición de parámetros para stored procedure 'insudb.updClientBC001J'
		'Información leída el 01/03/2000 09:57:19 AM
		Update = False
		With lRecReaBank_Universe
			.StoredProcedure = "updBank_Universe"
			.Parameters.Add("nBank_Code", nBank_code, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Update = .Run(False)
		End With
		'UPGRADE_NOTE: Object lRecReaBank_Universe may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lRecReaBank_Universe = Nothing
	End Function
	
	
	Public Function Add() As Boolean
		Dim lRecReaBank_Universe As eRemoteDB.Execute
		
		lRecReaBank_Universe = New eRemoteDB.Execute
		'Definición de parámetros para stored procedure 'insudb.creClientCode'
		'Información leída el 26/11/99 14:07:10
		Add = False
		With lRecReaBank_Universe
			.StoredProcedure = "creBank_Universe"
			.Parameters.Add("sBank_Code", nBank_code, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Add = .Run(False)
		End With
		'UPGRADE_NOTE: Object lRecReaBank_Universe may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lRecReaBank_Universe = Nothing
	End Function
	
	
	Public Function Delete() As Boolean
		Dim lRecReaBank_Universe As eRemoteDB.Execute
		
		lRecReaBank_Universe = New eRemoteDB.Execute
		'Definición de parámetros para stored procedure 'insudb.creClientCode'
		'Información leída el 26/11/99 14:07:10
		Delete = False
		With lRecReaBank_Universe
			.StoredProcedure = "delBank_Universe"
			.Parameters.Add("sBank_Code", nBank_code, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Delete = .Run(False)
		End With
		'UPGRADE_NOTE: Object lRecReaBank_Universe may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lRecReaBank_Universe = Nothing
	End Function
End Class






