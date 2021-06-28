Option Strict Off
Option Explicit On
Public Class Conm_master
	'%-------------------------------------------------------%'
	'% $Workfile:: Conm_master.cls                          $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 12:35p                               $%'
	'% $Revision:: 6                                        $%'
	'%-------------------------------------------------------%'
	
	'+ Column_name                    Type
	'-------------------------------- ---------
	Public sMortalco As String 'char
	Public nInterest As Double 'decimal
	Public dCompdate As Date 'datetime
	Public nUsercode As Integer 'smallint
	
	'**%FindConm_master: Verifies that there is information in the table "conm_Master"
	'%FindConm_master: Verifica que exista informacion en la tabla conm_Master
	Public Function FindConm_master(ByVal sMortalco As String, ByVal nInterest As Double) As Boolean
		
		Dim lrecreaConm_master As eRemoteDB.Execute
		
		On Error GoTo FindConm_master_err
		
		lrecreaConm_master = New eRemoteDB.Execute
		
		'**+Parameter definition for stored procedure 'insudb.reaConm_master'
		'**+Information read on May 17,2001 02:07:53 p.m.
		'+Definición de parámetros para stored procedure 'insudb.reaConm_master'
		'+Información leída el 17/05/2001 02:07:53 p.m.
		
		With lrecreaConm_master
			.StoredProcedure = "reaConm_master"
			.Parameters.Add("sMortalco", sMortalco, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 6, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInterest", nInterest, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				FindConm_master = True
				.RCloseRec()
			Else
				FindConm_master = False
			End If
		End With
		
		'UPGRADE_NOTE: Object lrecreaConm_master may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaConm_master = Nothing
		
FindConm_master_err: 
		If Err.Number Then
			FindConm_master = False
		End If
		On Error GoTo 0
	End Function
End Class






