Option Strict Off
Option Explicit On
Public Class Intermed_his
	'%-------------------------------------------------------%'
	'% $Workfile:: Intermed_his.cls                         $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:41p                                $%'
	'% $Revision:: 9                                        $%'
	'%-------------------------------------------------------%'
	
	'+ Propiedades según la tabla en el sistema el 13/12/2001
	'+ El campo llave corresponde a los campos nIntermed y dEffecdate.
	
	'+       Column name             Type
	'+  ----------------------- ----------------
	
	Public nIntermed As Integer
	Public dEffecdate As Date
	Public nNullcode As Integer
	Public nInterTyp As Integer
	Public nOffice As Integer
	Public nSupervis As Integer
	Public nInt_status As Integer
	Public nUsercode As Integer
	Public dCompdate As Date
	
	'- Propiedades auxiliares
	
	Public dEffecdate_Old As Date
	
	'%ADD: Este método se encarga de agregar nuevos registros a la tabla "Intermed_his". Devolviendo verdadero o
	'%falso dependiendo de si el Stored procedure se ejecutó correctamente.
	Public Function Add() As Boolean
		Dim lreccreIntermed_his As eRemoteDB.Execute
		
		On Error GoTo Add_Err
		lreccreIntermed_his = New eRemoteDB.Execute
		
		'**+ Parameter definitin for stored procedure 'insudb.creIntermed_his'
		'+ Definición de parámetros para stored procedure 'insudb.creIntermed_his'
		'**+ Data of October 20, 2000  14.28.08
		'+ Información leída el 20/10/2000 14.28.08
		
		With lreccreIntermed_his
			.StoredProcedure = "creIntermed_his"
			.Parameters.Add("nIntermed", nIntermed, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nNullcode", nNullcode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nIntertyp", nInterTyp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOffice", nOffice, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nSupervis", nSupervis, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInt_status", nInt_status, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate_old", dEffecdate_Old, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Add = .Run(False)
		End With
		'UPGRADE_NOTE: Object lreccreIntermed_his may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lreccreIntermed_his = Nothing
		
Add_Err: 
		If Err.Number Then
			Add = False
		End If
		On Error GoTo 0
	End Function
	
	'% ReaLastDateIntermed_his : Valida que la fecha sea posterior a la última fecha de modificación
	'% del intermediario (Intermed_his).
	Public Function ReaLastDateIntermed_his() As Boolean
		Dim lrecIntermed_his As eRemoteDB.Execute
		lrecIntermed_his = New eRemoteDB.Execute
		Dim ldEffecdate As Date
		
		ldEffecdate = Me.dEffecdate
		On Error GoTo ReaLastDateIntermed_his_Err
		With lrecIntermed_his
			.StoredProcedure = "reaLastDateIntermed_his"
			.Parameters.Add("nIntermed", nIntermed, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(True) Then
				If lrecIntermed_his.FieldToClass("dEffecdate") <> dtmNull Then
					Me.dEffecdate = lrecIntermed_his.FieldToClass("dEffecdate")
					ReaLastDateIntermed_his = True
				Else
					ReaLastDateIntermed_his = False
				End If
			Else
				ReaLastDateIntermed_his = False
			End If
			.RCloseRec()
		End With
		
ReaLastDateIntermed_his_Err: 
		If Err.Number Then
			ReaLastDateIntermed_his = False
		End If
		On Error GoTo 0
		
		'UPGRADE_NOTE: Object lrecIntermed_his may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecIntermed_his = Nothing
		
	End Function
	
	Public Function UpdateIntermedia_His_Status() As Boolean
		
		Dim lrecupdIntermediaStatus As eRemoteDB.Execute
		
		On Error GoTo UpdateIntermedia_His_Status_err
		
		lrecupdIntermediaStatus = New eRemoteDB.Execute
		'Definición de parámetros para stored procedure 'insudb.updIntermediaStatus'
		'Información leída el 06/02/2001 10.04.52
		With lrecupdIntermediaStatus
			.StoredProcedure = "updIntermedia_His_Status"
			.Parameters.Add("nIntermed", nIntermed, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInt_status", nInt_status, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			UpdateIntermedia_His_Status = .Run(False)
		End With
		
UpdateIntermedia_His_Status_err: 
		If Err.Number Then
			UpdateIntermedia_His_Status = False
		End If
		
		'UPGRADE_NOTE: Object lrecupdIntermediaStatus may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecupdIntermediaStatus = Nothing
		
		On Error GoTo 0
		
	End Function
	
	'% ReaIntermed_his: Lee de la tabla Intermed_his y retorna algunos valores - ACM - 14/05/2002
	Public Function ReaIntermed_his(ByVal nIntermed As Integer, ByVal dEffecdate As Date) As Boolean
		Dim lrecReaIntermed_his As eRemoteDB.Execute
		
		On Error GoTo ReaIntermed_his_err
		
		lrecReaIntermed_his = New eRemoteDB.Execute
		
		With lrecReaIntermed_his
			.StoredProcedure = "ReaIntermed_his"
			.Parameters.Add("nIntermed", nIntermed, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			ReaIntermed_his = .Run(True)
			If ReaIntermed_his Then
				Me.nInterTyp = .FieldToClass("nInterTyp")
				Me.nOffice = .FieldToClass("nOffice")
				Me.nSupervis = .FieldToClass("nSupervis")
			End If
			
		End With
		
ReaIntermed_his_err: 
		If Err.Number Then
			ReaIntermed_his = False
		End If
		
		On Error GoTo 0
	End Function
End Class






