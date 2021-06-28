Option Strict Off
Option Explicit On
Public Class Plan_Loads
	'%-------------------------------------------------------%'
	'% $Workfile:: Plan_Loads.cls                           $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 12:35p                               $%'
	'% $Revision:: 16                                       $%'
	'%-------------------------------------------------------%'
	
	'- Estructura de tabla insudb.plan_loads al 11-20-2001 10:35:21
	'-        Property                Type         DBType   Size Scale  Prec  Null
	Public nBranch As Integer ' NUMBER     22   0     5    N
	Public nProduct As Integer ' NUMBER     22   0     5    N
	Public nModulec As Integer ' NUMBER     22   0     5    N
	Public nTypeLoad As Integer ' NUMBER     22   0     5    N
	Public nInitMonth As Integer ' NUMBER     22   0     5    N
	Public nEndMonth As Integer ' NUMBER     22   0     5    N
	Public nCapStart As Double ' NUMBER     22   0     12   N
	Public nCapEnd As Double ' NUMBER     22   0     12   N
	Public dEffecdate As Date ' DATE       7    0     0    N
	Public nPercent As Double ' NUMBER     22   2     5    S
	Public nAmount As Double ' NUMBER     22   2     10   S
	Public nCurrency As Integer ' NUMBER     22   0     5    N
	Public nUsercode As Integer ' NUMBER     22   0     5    N
	Public nMonths As Integer
	
	
	'%InsUpdPlan_Loads: Se encarga de actualizar la tabla Plan_Loads
	Private Function InsUpdPlan_Loads(ByVal nAction As Integer) As Boolean
		Dim lrecinsUpdplan_loads As eRemoteDB.Execute
		Dim lclsPlan_loadss As Plan_Loadss
		Dim lclsProd_win As eProduct.Prod_win
		
		On Error GoTo insUpdplan_loads_Err
		
		InsUpdPlan_Loads = False
		
		lrecinsUpdplan_loads = New eRemoteDB.Execute
		lclsPlan_loadss = New Plan_Loadss
		lclsProd_win = New eProduct.Prod_win
		
		'+
		'+ Definición de store procedure insUpdplan_loads al 11-20-2001 10:44:45
		'+
		With lrecinsUpdplan_loads
			.StoredProcedure = "insUpdplan_loads"
			With .Parameters
				.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Add("nTypeload", nTypeLoad, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Add("nInitMonth", nInitMonth, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Add("nEndMonth", nEndMonth, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Add("nCapStart", nCapStart, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Add("nCapEnd", nCapEnd, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Add("nPercent", nPercent, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 9, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Add("nAmount", nAmount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 3, 9, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Add("nMonths", nMonths, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				
				
			End With
			
			InsUpdPlan_Loads = .Run(False)
			
		End With
		
		If InsUpdPlan_Loads Then
			If lclsPlan_loadss.Find_Product(nBranch, nProduct, dEffecdate) Then
				'+ Se actualiza la secuencia de ventana del producto con la transacción enviada como parámetro
				Call lclsProd_win.Add_Prod_win(nBranch, nProduct, dEffecdate, "DP607B", "2", nUsercode)
			Else
				Call lclsProd_win.Add_Prod_win(nBranch, nProduct, dEffecdate, "DP607B", "1", nUsercode)
			End If
		End If
		
insUpdplan_loads_Err: 
		If Err.Number Then
			InsUpdPlan_Loads = False
		End If
		
		'UPGRADE_NOTE: Object lrecinsUpdplan_loads may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsUpdplan_loads = Nothing
		'UPGRADE_NOTE: Object lclsPlan_loadss may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsPlan_loadss = Nothing
		'UPGRADE_NOTE: Object lclsProd_win may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsProd_win = Nothing
		On Error GoTo 0
		
	End Function
	
	'%Add: Crea un registro en la tabla
	Public Function Add() As Boolean
		Add = InsUpdPlan_Loads(1)
	End Function
	
	'%Update: Actualiza un registro en la tabla
	Public Function Update() As Boolean
		Update = InsUpdPlan_Loads(2)
	End Function
	
	'%Delete: Borra un registro en la tabla
	Public Function Delete() As Boolean
		Delete = InsUpdPlan_Loads(3)
	End Function
	
	'%Find: Lee los datos de la tabla
	Public Function Find(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nModulec As Integer, ByVal nTypeLoad As Integer, ByVal nDurIni As Integer, ByVal dEffecdate As Date, Optional ByVal lblnFind As Boolean = False) As Boolean
		
		Dim lrecreaPlan_loads As eRemoteDB.Execute
		On Error GoTo reaPlan_loads_Err
		
		lrecreaPlan_loads = New eRemoteDB.Execute
		
		Find = False
		
		'+
		'+ Definición de store procedure reaPlan_loads al 11-20-2001 10:37:41
		'+
		With lrecreaPlan_loads
			.StoredProcedure = "reaPlan_loads"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTypeload", nTypeLoad, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInitMonth", nInitMonth, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(True) Then
				Find = True
				Me.nBranch = nBranch
				Me.nProduct = nProduct
				Me.nModulec = nModulec
				Me.nTypeLoad = nTypeLoad
				Me.nInitMonth = nInitMonth
				Me.dEffecdate = dEffecdate
				Me.nEndMonth = .FieldToClass("nEndMonth")
				Me.nCapStart = .FieldToClass("nCapStart")
				Me.nCapEnd = .FieldToClass("nCapEnd")
				Me.nPercent = .FieldToClass("nPercent")
				Me.nAmount = .FieldToClass("nAmount")
				Me.nCurrency = .FieldToClass("nCurrency")
				Me.nUsercode = .FieldToClass("nUsercode")
				.RCloseRec()
			End If
		End With
		
reaPlan_loads_Err: 
		If Err.Number Then
		End If
		'UPGRADE_NOTE: Object lrecreaPlan_loads may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaPlan_loads = Nothing
		On Error GoTo 0
	End Function
	
	'%InsValRange: Valida que no exista rango antes de crearlo
	Public Function InsValRange(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nModulec As Integer, ByVal nTypeLoad As Integer, ByVal nInitMonth As Integer, ByVal nEndMonth As Integer, ByVal nCapStart As Double, ByVal nCapEnd As Double, ByVal dEffecdate As Date) As Boolean
		
		Dim nExist As Short
		Dim lrecinsValrange_plan_loads As eRemoteDB.Execute
		On Error GoTo insValrange_plan_loads_Err
		
		InsValRange = False
		
		lrecinsValrange_plan_loads = New eRemoteDB.Execute
		
		nExist = 0
		
		'+
		'+ Definición de store procedure insValrange_plan_loads al 11-20-2001 10:47:36
		'+
		With lrecinsValrange_plan_loads
			.StoredProcedure = "insValrange_plan_loads"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTypeload", nTypeLoad, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInitMonth", nInitMonth, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nEndMonth", nEndMonth, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCapStart", nCapStart, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCapEnd", nCapEnd, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nExist", nExist, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(False) Then
				InsValRange = Not .Parameters("nExist").Value = 1
			Else
				InsValRange = True
			End If
			
		End With
		
insValrange_plan_loads_Err: 
		If Err.Number Then
		End If
		'UPGRADE_NOTE: Object lrecinsValrange_plan_loads may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsValrange_plan_loads = Nothing
		On Error GoTo 0
	End Function
	
	
	'%Class_Initialize: Inicializa las propiedades cuando se instancia la clase
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		nBranch = eRemoteDB.Constants.intNull
		nProduct = eRemoteDB.Constants.intNull
		nModulec = eRemoteDB.Constants.intNull
		nTypeLoad = eRemoteDB.Constants.intNull
		nInitMonth = eRemoteDB.Constants.intNull
		nEndMonth = eRemoteDB.Constants.intNull
		nCapStart = eRemoteDB.Constants.intNull
		nCapEnd = eRemoteDB.Constants.intNull
		dEffecdate = eRemoteDB.Constants.dtmNull
		nPercent = eRemoteDB.Constants.intNull
		nAmount = eRemoteDB.Constants.intNull
		nCurrency = eRemoteDB.Constants.intNull
		nUsercode = eRemoteDB.Constants.intNull
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
End Class






