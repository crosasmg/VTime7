Option Strict Off
Option Explicit On
Public Class User_jobs
	'%-------------------------------------------------------%'
	'% $Workfile:: User_jobs.cls                            $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:19p                                $%'
	'% $Revision:: 5                                        $%'
	'%-------------------------------------------------------%'
	
	'*-Propiedades según la tabla en el sistema el 27/12/2000
	'Column_Name                   Type          Length  Prec    Scale   Nullable
	'-------------------------   --------------- ------ -------- ------- ---------
	Public nJob As Double
	Public dNext_date As Date
	Public sWhat As String
	
	'%InsUpdUser_jobs: Realiza la actualización de la tabla
	Private Function InsUpdUser_jobs(ByVal nAction As Integer) As Boolean
		Dim lrecInsUpdDbms_job As eRemoteDB.Execute
		
		On Error GoTo InsUpdDbms_job_Err
		
		lrecInsUpdDbms_job = New eRemoteDB.Execute
		
		'+Definición de parámetros para stored procedure 'InsUpdDbms_job'
		'+Información leída el 8/3/02
		With lrecInsUpdDbms_job
			.StoredProcedure = "InsUpdDbms_job"
			.Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 38, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nJob", nJob, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sWhat", sWhat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 4000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dNext_date", dNext_date, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			InsUpdUser_jobs = .Run(False)
		End With
InsUpdDbms_job_Err: 
		If Err.Number Then
			InsUpdUser_jobs = False
		End If
		'UPGRADE_NOTE: Object lrecInsUpdDbms_job may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecInsUpdDbms_job = Nothing
		On Error GoTo 0
	End Function
	
	'%Add: Crea un registro en la tabla
	Public Function Add() As Boolean
		Add = InsUpdUser_jobs(1)
	End Function
	
	'%Delete: Borra los datos de la tabla
	Public Function Delete() As Boolean
		Delete = InsUpdUser_jobs(3)
	End Function
	
	'%Update: Actualiza los datos de la tabla
	Public Function Update() As Boolean
		Update = InsUpdUser_jobs(2)
	End Function
	
	'% InsPostMA5000: Actualiza las tareas asociadas al usuario
	Public Function InsPostMA5000(ByVal sAction As String, ByVal nJob As Double, ByVal dNext_date As Date, ByVal sWhat As String) As Boolean
		On Error GoTo InsPostMA5000_Error
		
		With Me
			.nJob = nJob
			.dNext_date = dNext_date
			.sWhat = sWhat
			If sAction = "Add" Then
				InsPostMA5000 = .Add
			ElseIf sAction = "Update" Then 
				InsPostMA5000 = .Update
			ElseIf sAction = "Del" Then 
				InsPostMA5000 = .Delete
			End If
		End With
InsPostMA5000_Error: 
		If Err.Number Then
			InsPostMA5000 = False
		End If
		On Error GoTo 0
	End Function
End Class






