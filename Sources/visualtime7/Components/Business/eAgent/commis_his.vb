Option Strict Off
Option Explicit On
Public Class commis_his
	'%-------------------------------------------------------%'
	'% $Workfile:: commis_his.cls                           $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:41p                                $%'
	'% $Revision:: 10                                       $%'
	'%-------------------------------------------------------%'
	
	'**+ Properties according to the table in the system on January 28, 2000
	'+ Propiedades según la tabla en el sistema el 28/01/2000
	'**+ The key field correspond to nIntermed.
	'+ El campo llave corresponde a nIntermed.
	
	'+  Column name               Type                  Length Prec  Scale Nullable TrimTrailingBlanks FixedLenNullInSource
	'+  ------------------------- --------------------- ------ ----- ----- -------- ------------------ ---------------------
	Public nIntermed As Integer 'int      no     4     10    no       (n/a)              (n/a)
	Public dEffecdate As Date 'datetime no     8           no       (n/a)              (n/a)
	Public sTyp_comiss As String 'char     no     1           no       yes                no
	Public nComtab As Integer 'smallint no     2     5     yes      (n/a)              (n/a)
	
	'**- Auxiliaries properties
	'- Propiedades auxiliares
	
	Public dEffecdate_Old As Object
	
	'**- Property definition sTabComDes, to contain the name of the assigned commissions table
	'-Se define la propiedad sTabComDes, para contener el nombre de la tabla de comisiones asignada
	
	Public sTabComDes As String
	
	'**- Possible global values to be used in the agents module for the  commissions tables.
	'- Posibles valores globales a usar en el módulo de agentes para las tablas de comisiones.
	
	Enum commissTables
		Lifecommiss = 1
		GralCommiss = 2
		ExCommiss = 3
		EscheCommiss = 4
        LifeGoals = 5
        GralGoals = 6
        SpeLifeCommi = 7
	End Enum
	
	'**%ADD: Add new records to the table "commiss_his".  It returns TRUE or FALSE
	'**%depending on whether the stored procedure executed correctly.
	'%ADD: Este método se encarga de agregar nuevos registros a la tabla "commis_his". Devolviendo verdadero o
	'%falso dependiendo de si el Stored procedure se ejecutó correctamente.
	Public Function Add() As Boolean
		Dim lreccreCommis_his As eRemoteDB.Execute
		
		On Error GoTo Add_Err
		lreccreCommis_his = New eRemoteDB.Execute
		
		'**+ Parameter definitin for stored procedure 'insudb.creCommis_his'
		'+ Definición de parámetros para stored procedure 'insudb.creCommis_his'
		'**+ Data of October 20, 2000  14.28.08
		'+ Información leída el 20/10/2000 14.28.08
		
		With lreccreCommis_his
			.StoredProcedure = "creCommis_his"
			.Parameters.Add("nIntermed", nIntermed, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sTyp_comiss", sTyp_comiss, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nComtab", nComtab, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate_old", dEffecdate_Old, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Add = .Run(False)
		End With
		'UPGRADE_NOTE: Object lreccreCommis_his may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lreccreCommis_his = Nothing
		
Add_Err: 
		If Err.Number Then
			Add = False
		End If
		On Error GoTo 0
	End Function
	
	'**%ReaLastDateCommis_his: Validates that the date is before to the last date of
	'**% associated commisions modification (commis_his)
	'% ReaLastDateCommis_his : Valida que la fecha sea posterior a la última fecha de modificación
	'% de comisiones asociadas (commis_his).
	Public Function ReaLastDateCommis_his() As Boolean
		Dim lrecCommis_his As eRemoteDB.Execute
		Dim ldEffecdate As Date
		
		lrecCommis_his = New eRemoteDB.Execute
		
		ldEffecdate = dEffecdate
		
		On Error GoTo ReaLastDateCommis_his_Err
		
		With lrecCommis_his
			.StoredProcedure = "reaLastDateCommis_his"
			.Parameters.Add("nIntermed", nIntermed, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(True) Then
				If lrecCommis_his.FieldToClass("dEffecdate") <> dtmNull Then
					Me.dEffecdate = lrecCommis_his.FieldToClass("dEffecdate")
					ReaLastDateCommis_his = True
				Else
					ReaLastDateCommis_his = False
				End If
			Else
				ReaLastDateCommis_his = False
			End If
			.RCloseRec()
		End With
		'UPGRADE_NOTE: Object lrecCommis_his may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecCommis_his = Nothing
		
ReaLastDateCommis_his_Err: 
		If Err.Number Then
			ReaLastDateCommis_his = False
		End If
		On Error GoTo 0
	End Function
End Class






