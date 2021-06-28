Option Strict Off
Option Explicit On
Public Class Load_surr
	'%-------------------------------------------------------%'
	'% $Workfile:: Load_surr.cls                            $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 12:35p                               $%'
	'% $Revision:: 9                                        $%'
	'%-------------------------------------------------------%'
	
	'- Estructura de tabla insudb.load_surr al 11-21-2001 15:24:48
	'-     Property                    Type         DBType   Size Scale  Prec  Null
	Public nBranch As Integer ' NUMBER     22   0     5    N
	Public nProduct As Integer ' NUMBER     22   0     5    N
	Public nModulec As Integer ' NUMBER     22   0     5    N
	Public nQMonthIni As Integer ' NUMBER     22   0     5    N
	Public dEffecdate As Date ' DATE       7    0     0    N
	Public nQMonthEnd As Integer ' NUMBER     22   0     5    N
	Public nPercent As Double ' NUMBER     22   2     5    S
	Public nUsercode As Integer ' NUMBER     22   0     5    N
	Public nPerTotSurr As Double ' NUMBER     22   2     5    S
	Public nPerParSurr As Double ' NUMBER     22   2     5    S
	Public nChargTSurr As Double ' NUMBER     22   2     5    S
	Public nChargPSurr As Double ' NUMBER     22   2     5    S
	Public nQFree_Surr As Integer ' NUMBER     22   0     5    N
	
	'%InsUpdLoad_surr: Se encarga de actualizar la tabla Load_surr
	Private Function InsUpdLoad_surr(ByVal nAction As Integer) As Boolean
		Dim lrecinsUpdload_surr As eRemoteDB.Execute
		
		On Error GoTo insUpdload_surr_Err
		
		InsUpdLoad_surr = False
		
		lrecinsUpdload_surr = New eRemoteDB.Execute
		
		'+
		'+ Definición de store procedure insUpdload_surr al 11-21-2001 15:36:39
		'+
		With lrecinsUpdload_surr
			.StoredProcedure = "insUpdload_surr"
			.Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nQmonthini", nQMonthIni, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nQmonthend", nQMonthEnd, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPercent", nPercent, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPerTotSurr", nPerTotSurr, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPerParSurr", nPerParSurr, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nChargTSurr", nChargTSurr, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nChargPSurr", nChargPSurr, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nQFree_Surr", nQFree_Surr, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			InsUpdLoad_surr = .Run(False)
			
		End With
		
insUpdload_surr_Err: 
		If Err.Number Then
			InsUpdLoad_surr = False
		End If
		
		'UPGRADE_NOTE: Object lrecinsUpdload_surr may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsUpdload_surr = Nothing
		On Error GoTo 0
	End Function
	
	'%Add: Crea un registro en la tabla
	Public Function Add() As Boolean
		Add = InsUpdLoad_surr(1)
	End Function
	
	'%Update: Actualiza un registro en la tabla
	Public Function Update() As Boolean
		Update = InsUpdLoad_surr(2)
	End Function
	
	'%Delete: Borra un registro en la tabla
	Public Function Delete() As Boolean
		Delete = InsUpdLoad_surr(3)
	End Function
	
	'%Find: Lee los datos de la tabla
	Public Function Find(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nModulec As Integer, ByVal nQMonthIni As Integer, ByVal dEffecdate As Date, Optional ByVal lblnFind As Boolean = False) As Boolean
		Dim lrecreaLoad_surr As eRemoteDB.Execute
		On Error GoTo reaLoad_surr_Err
		
		Find = False
		
		lrecreaLoad_surr = New eRemoteDB.Execute
		
		'+
		'+ Definición de store procedure reaLoad_surr al 11-21-2001 15:29:42
		'+
		With lrecreaLoad_surr
			.StoredProcedure = "reaLoad_surr"
			With .Parameters
				.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Add("nQmonthini", nQMonthIni, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			End With
			
			If .Run(True) Then
				Find = True
				Me.nBranch = nBranch
				Me.nProduct = nProduct
				Me.nModulec = nModulec
				Me.nQMonthIni = nQMonthIni
				Me.nQMonthEnd = .FieldToClass("nQmonthend")
				Me.dEffecdate = dEffecdate
				Me.nPercent = .FieldToClass("nPercent")
				Me.nUsercode = .FieldToClass("nUsercode")
				Me.nPerTotSurr = .FieldToClass("nPerTotSurr")
				Me.nPerParSurr = .FieldToClass("nPerParSurr")
				Me.nChargTSurr = .FieldToClass("nChargTSurr")
				Me.nChargPSurr = .FieldToClass("nChargPSurr")
				Me.nQFree_Surr = .FieldToClass("nQFree_Surr")
				.RCloseRec()
			End If
		End With
		
reaLoad_surr_Err: 
		If Err.Number Then
			Find = False
		End If
		
		'UPGRADE_NOTE: Object lrecreaLoad_surr may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaLoad_surr = Nothing
		On Error GoTo 0
	End Function
	
	'%InsValRange: Valida que no exista rango antes de crearlo
	Public Function InsValRange(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nModulec As Integer, ByVal nQMonthIni As Integer, ByVal dEffecdate As Date, ByVal nQMonthEnd As Integer) As Boolean
		
		Dim lrecinsValrange_load_surr As eRemoteDB.Execute
		On Error GoTo insValrange_load_surr_Err
		
		InsValRange = False
		
		lrecinsValrange_load_surr = New eRemoteDB.Execute
		
		'+
		'+ Definición de store procedure insValrange_load_surr al 11-21-2001 15:44:12
		'+
		With lrecinsValrange_load_surr
			.StoredProcedure = "insValrange_load_surr"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nQmonthini", nQMonthIni, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nQmonthend", nQMonthEnd, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			InsValRange = Not .Run(True)
			.RCloseRec()
		End With
		
insValrange_load_surr_Err: 
		If Err.Number Then
			InsValRange = False
		End If
		
		'UPGRADE_NOTE: Object lrecinsValrange_load_surr may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsValrange_load_surr = Nothing
		On Error GoTo 0
	End Function
	'%Class_Initialize: Inicializa las propiedades cuando se instancia la clase
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		
		nBranch = eRemoteDB.Constants.intNull
		nProduct = eRemoteDB.Constants.intNull
		nModulec = eRemoteDB.Constants.intNull
		nQMonthIni = eRemoteDB.Constants.intNull
		dEffecdate = eRemoteDB.Constants.dtmNull
		nQMonthEnd = eRemoteDB.Constants.intNull
		nPercent = eRemoteDB.Constants.intNull
		nUsercode = eRemoteDB.Constants.intNull
		nPerTotSurr = eRemoteDB.Constants.intNull
		nPerParSurr = eRemoteDB.Constants.intNull
		nChargTSurr = eRemoteDB.Constants.intNull
		nChargPSurr = eRemoteDB.Constants.intNull
		nQFree_Surr = eRemoteDB.Constants.intNull
		
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
End Class






