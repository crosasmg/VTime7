Option Strict Off
Option Explicit On
Public Class Tab_svs
	'%-------------------------------------------------------%'
	'% $Workfile:: Tab_svs.cls                              $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:13p                                $%'
	'% $Revision:: 10                                       $%'
	'%-------------------------------------------------------%'
	
	'- Variables segun campos en la tabla al 22/05/2003
	
	'+ Nombre              Tipo                     ¿Nulo?
	'+ ------------------- ------------------------ ------
	Public nInsur_area As Integer 'NUMBER(5)   No
	Public nFactor As Short 'NUMBER(1)   No
	Public nSVSClass As Integer 'NUMBER(5)   No
	Public dEffecdate As Date 'DATE        No
	Public nValue As Double 'NUMBER(8,5) No
	Public nUsercode As Integer 'NUMBER(5)   No
	
	'+ Variables auxiliares
	
	'- Indica si se puede eliminar o no el registro.  Depende de la fecha en que fué generado
	'- el cálculo de margen de solvencia (Ctrol_date, nType_proce=41), y la vigencia del registro
	Public sDelete As String
	
	'% insvalMMGS001_K: se realizan las validaciones del encabezado de la página
	Public Function insvalMMGS001_K(ByVal sCodispl As String, ByVal nInsur_area As Integer, ByVal nFactor As Integer) As String
		Dim lobjErrors As eFunctions.Errors
		
		On Error GoTo insvalMMGS001_K_err
		
		lobjErrors = New eFunctions.Errors
		
		With lobjErrors
			'+ El área del seguro debe estar lleno
			If nInsur_area = eRemoteDB.Constants.intNull Then
				Call .ErrorMessage(sCodispl, 55031)
			End If
			
			'+ El factor o coeficiente debe estar lleno
			If nFactor = eRemoteDB.Constants.intNull Then
				Call .ErrorMessage(sCodispl, 55910)
			End If
			
			insvalMMGS001_K = .Confirm
		End With
		
insvalMMGS001_K_err: 
		If Err.Number Then
			insvalMMGS001_K = "insvalMMGS001_K: " & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjErrors = Nothing
	End Function
	
	'% insvalMMGS001: se realizan las validaciones de la zona masiva de la página
	Public Function insvalMMGS001(ByVal sCodispl As String, ByVal sAction As String, ByVal nSVSClass As Integer, ByVal dEffecdate As Date, ByVal nValue As Double, ByVal nInsur_area As Integer, ByVal nFactor As Short) As String
		Dim lobjErrors As eFunctions.Errors
		Dim lclsCtrol_date As eGeneral.Ctrol_date
		Dim ldblMax As Double
		Dim ldblMin As Double
		
		On Error GoTo insvalMMGS001_err
		
		lobjErrors = New eFunctions.Errors
		
		With lobjErrors
			'+ La clasificación SVS debe estar llena
			If nSVSClass = eRemoteDB.Constants.intNull Then
				Call .ErrorMessage(sCodispl, 11320)
			End If
			
			'+ La fecha de inicio de vigencia debe estar llena
			If dEffecdate = eRemoteDB.Constants.dtmNull Then
				Call .ErrorMessage(sCodispl, 2056)
			Else
				If sAction = "Add" Then
					'+ Si la clasificación ya existe en la tabla, debe ser posterior a la última fecha
					'+ para la misma
					If insvalExist(nInsur_area, nFactor, nSVSClass, dEffecdate) Then
						Call .ErrorMessage(sCodispl, 55908)
					End If
				Else
					lclsCtrol_date = New eGeneral.Ctrol_date
					If lclsCtrol_date.Find(41) Then
						'+ Si se han realizado cálculos del margen de solvencia para la fecha
						If lclsCtrol_date.dEffecdate >= dEffecdate Then
							Call .ErrorMessage(sCodispl, 55909)
						End If
					End If
				End If
			End If
			
			'+ El porcentaje o valor debe estar lleno
			If nValue = eRemoteDB.Constants.intNull Then
				Call .ErrorMessage(sCodispl, 55911)
			Else
				ldblMax = 999.99999
				ldblMin = 0.00001
				If nValue > ldblMax Or nValue < ldblMin Then
					Call .ErrorMessage(sCodispl, 1935,  , eFunctions.Errors.TextAlign.RigthAling, "(" & ldblMin & " - " & ldblMax & ")")
				End If
			End If
			
			insvalMMGS001 = .Confirm
		End With
		
insvalMMGS001_err: 
		If Err.Number Then
			insvalMMGS001 = "insvalMMGS001: " & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjErrors = Nothing
		'UPGRADE_NOTE: Object lclsCtrol_date may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsCtrol_date = Nothing
	End Function
	
	'% inspostMMGS001: se actualizan los campos en la tabla de valores de la S.V.S
	'%                 (Superintendencia de Valores y Seguros) para el cálculo del margen de
	'%                solvencia
	Public Function inspostMMGS001(ByVal sAction As String, ByVal nInsur_area As Integer, ByVal nFactor As Short, ByVal nSVSClass As Integer, ByVal dEffecdate As Date, ByVal nUsercode As Integer, Optional ByVal nValue As Double = 0) As Boolean
		On Error GoTo inspostMMGS001_err
		
		With Me
			.nInsur_area = nInsur_area
			.nFactor = nFactor
			.nSVSClass = nSVSClass
			.dEffecdate = dEffecdate
			.nValue = nValue
			.nUsercode = nUsercode
			'+ Se asigna valor a la acción para ser tomada en el SP
			Select Case sAction
				Case "Add"
					inspostMMGS001 = .Add()
				Case "Update"
					inspostMMGS001 = .Update(2)
				Case "Del"
					inspostMMGS001 = .Delete()
			End Select
		End With
		
inspostMMGS001_err: 
		If Err.Number Then
			inspostMMGS001 = False
		End If
		On Error GoTo 0
	End Function
	
	'% Add: se crean los registros en la tabla
	Public Function Add() As Boolean
		Add = Update(1)
	End Function
	
	'% Delete: se eliminan los registros en la tabla
	Public Function Delete() As Boolean
		Delete = Update(3)
	End Function
	
	'% Update: actualiza los campos de la tabla
	Public Function Update(ByVal nAction As Integer) As Boolean
		Dim lclsRemote As eRemoteDB.Execute
		
		On Error GoTo Update_err
		
		lclsRemote = New eRemoteDB.Execute
		
		With lclsRemote
			.StoredProcedure = "insupdTab_SVS"
			.Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInsur_area", nInsur_area, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nFactor", nFactor, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nSVSClass", nSVSClass, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nValue", nValue, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 5, 8, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Update = .Run(False)
		End With
		
Update_err: 
		If Err.Number Then
			Update = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsRemote may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsRemote = Nothing
	End Function
	
	'% insvalExist: verifica la existencia de la clasificación para una fecha
	Private Function insvalExist(ByVal nInsur_area As Integer, ByVal nFactor As Short, ByVal nSVSClass As Integer, ByVal dEffecdate As Date) As Boolean
		Dim lclsRemote As eRemoteDB.Execute
		
		On Error GoTo insvalExist_err
		
		lclsRemote = New eRemoteDB.Execute
		
		With lclsRemote
			.StoredProcedure = "valExist_Tab_SVS"
			.Parameters.Add("nInsur_area", nInsur_area, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nFactor", nFactor, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nSVSClass", nSVSClass, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nExists", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				insvalExist = .Parameters("nExists").Value = 1
			End If
		End With
		
insvalExist_err: 
		If Err.Number Then
			insvalExist = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsRemote may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsRemote = Nothing
	End Function
End Class






