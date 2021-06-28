Option Strict Off
Option Explicit On
Public Class Series
	'%-------------------------------------------------------%'
	'% $Workfile:: Series.cls                               $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:38p                                $%'
	'% $Revision:: 12                                       $%'
	'%-------------------------------------------------------%'
	'+
	'+ Estructura de tabla SERIES al 03-01-2002 17:10:58
	'+     Property                Type         DBType   Size Scale  Prec  Null
	'+-------------------------------------------------------------------------
	Public sSerie As String ' VARCHAR2   4    0     0    N
	Public nDigit7 As Integer ' NUMBER     22   0     1    N
	Public nDigit6 As Integer ' NUMBER     22   0     1    N
	Public nDigit5 As Integer ' NUMBER     22   0     1    N
	Public nUsercode As Integer ' NUMBER     22   0     5    N
	'%InsUpdSeries: Se encarga de actualizar la tabla Series
	Private Function InsUpdSeries(ByVal nAction As Integer) As Boolean
		Dim lrecinsUpdseries As eRemoteDB.Execute
		On Error GoTo insUpdseries_Err
		
		lrecinsUpdseries = New eRemoteDB.Execute
		
		'+ Definición de store procedure insUpdseries al 03-01-2002 17:17:23
		With lrecinsUpdseries
			.StoredProcedure = "insUpdseries"
			.Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sSerie", sSerie, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 4, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDigit7", nDigit7, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 1, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDigit6", nDigit6, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 1, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDigit5", nDigit5, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 1, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			InsUpdSeries = .Run(False)
		End With
		
insUpdseries_Err: 
		If Err.Number Then
			InsUpdSeries = False
		End If
		'UPGRADE_NOTE: Object lrecinsUpdseries may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsUpdseries = Nothing
		On Error GoTo 0
	End Function
	
	'%Add: Crea un registro en la tabla
	Public Function Add() As Boolean
		Add = InsUpdSeries(1)
	End Function
	
	'%Update: Actualiza un registro en la tabla
	Public Function Update() As Boolean
		Update = InsUpdSeries(2)
	End Function
	
	'%Delete: Borra un registro en la tabla
	Public Function Delete() As Boolean
		Delete = InsUpdSeries(3)
	End Function
	'%InsValSerie: Valida la fecha de efecto de la transacción
	Public Function InsValSerie(ByVal sSerie As String) As Boolean
		Dim lrecreaSeries_v As eRemoteDB.Execute
		Dim nExist As Integer
		
		On Error GoTo reaSeries_v_Err
		
		lrecreaSeries_v = New eRemoteDB.Execute
		
		'+ Definición de store procedure reaSeries_v al 03-01-2002 17:14:26
		With lrecreaSeries_v
			.StoredProcedure = "reaSeries_v"
			.Parameters.Add("sSerie", sSerie, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 4, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nExist", nExist, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(False) Then
				InsValSerie = .Parameters("nExist").Value = 1
			Else
				InsValSerie = False
			End If
		End With
		
reaSeries_v_Err: 
		If Err.Number Then
			InsValSerie = False
		End If
		'UPGRADE_NOTE: Object lrecreaSeries_v may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaSeries_v = Nothing
		On Error GoTo 0
	End Function
	
	'%InsValMAU551_K: Validaciones de la transacción(Header)
	Public Function InsValMAU551_K(ByVal sCodispl As String, ByVal sAction As String, ByVal sSerie As String, ByVal nDigit7 As Integer, ByVal nDigit6 As Integer, ByVal nDigit5 As Integer) As String
		Dim lclsErrors As eFunctions.Errors
		
		On Error GoTo InsValMAU551_K_Err
		lclsErrors = New eFunctions.Errors
		
		With lclsErrors
			
			'+ Se valida el código de Serie
			If sSerie = String.Empty Then
				.ErrorMessage(sCodispl, 55602)
			End If
			
			'+ Se valida D7
			If nDigit7 = eRemoteDB.Constants.intNull Then
				.ErrorMessage(sCodispl, 55604,  ,  , " (dígito 7 para validación de patentes)")
			End If
			
			'+ Se valida D6
			If nDigit6 = eRemoteDB.Constants.intNull Then
				.ErrorMessage(sCodispl, 55604,  ,  , " (dígito 6 para validación de patentes)")
			End If
			
			'+ Se valida D5
			If nDigit5 = eRemoteDB.Constants.intNull Then
				.ErrorMessage(sCodispl, 55604,  ,  , " (dígito 5 para validación de patentes)")
			End If
			
			'+ Se valida que no se duplique la Serie
			If sAction = "Add" Then
				If InsValSerie(sSerie) Then
					.ErrorMessage(sCodispl, 55603)
				End If
			End If
			
			'+ Se Valida que no se duplique el equivalente numerico
			If InsValSerieDigit(sSerie, nDigit7, nDigit6, nDigit5) Then
				.ErrorMessage(sCodispl, 55601)
			End If
			
			InsValMAU551_K = .Confirm
		End With
		
InsValMAU551_K_Err: 
		If Err.Number Then
			InsValMAU551_K = "InsValMAU551_K: " & Err.Description
		End If
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		On Error GoTo 0
	End Function
	
	'%InsPostMAU551: Ejecuta el post de la transacción
	'%               Tabla de control de prima mínima(MAU551)
	Public Function InsPostMAU551(ByVal sAction As String, ByVal sSerie As String, ByVal nDigit7 As Integer, ByVal nDigit6 As Integer, ByVal nDigit5 As Integer, ByVal nUsercode As Object) As Boolean
		
		
		On Error GoTo InsPostMAU551_Err
		
		With Me
			.sSerie = sSerie
			.nDigit7 = nDigit7
			.nDigit6 = nDigit6
			.nDigit5 = nDigit5
			.nUsercode = nUsercode
		End With
		
		Select Case sAction
			Case "Add"
				InsPostMAU551 = Add
			Case "Update"
				InsPostMAU551 = Update
			Case "Del"
				InsPostMAU551 = Delete
		End Select
		
InsPostMAU551_Err: 
		If Err.Number Then
			InsPostMAU551 = False
		End If
		On Error GoTo 0
	End Function
	'% INSVALSERIE : VALIDA QUE NO SE REPITA EL EQUIVALENTE NUMERICO
	Public Function InsValSerieDigit(ByVal sSerie As String, ByVal nDigit7 As Integer, ByVal nDigit6 As Integer, ByVal nDigit5 As Integer) As Boolean
		Dim lrecinsValseries As eRemoteDB.Execute
		Dim nExist As Integer
		
		On Error GoTo insValseries_Err
		
		lrecinsValseries = New eRemoteDB.Execute
		
		'+
		'+ Definición de store procedure insValseries al 11-21-2002 10:39:59
		'+
		With lrecinsValseries
			.StoredProcedure = "insValseries"
			.Parameters.Add("sSerie", sSerie, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 4, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDigit5", nDigit5, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDigit6", nDigit6, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDigit7", nDigit7, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nExist", nExist, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(False) Then
				InsValSerieDigit = .Parameters("nExist").Value > 0
			Else
				InsValSerieDigit = False
			End If
		End With
		
insValseries_Err: 
		If Err.Number Then
			InsValSerieDigit = False
		End If
		'UPGRADE_NOTE: Object lrecinsValseries may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsValseries = Nothing
		On Error GoTo 0
	End Function
	'%Class_Initialize: Inicializa las propiedades cuando se instancia la clase
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		sSerie = String.Empty
		nDigit7 = eRemoteDB.Constants.intNull
		nDigit6 = eRemoteDB.Constants.intNull
		nDigit5 = eRemoteDB.Constants.intNull
		nUsercode = eRemoteDB.Constants.intNull
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
End Class






