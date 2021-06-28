Option Strict Off
Option Explicit On
Public Class Tarif_tab_col
	'%-------------------------------------------------------%'
	'% $Workfile:: Tarif_tab_col.cls                            $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:34p                                $%'
	'% $Revision:: 9                                        $%'
	'%-------------------------------------------------------%'
	'+
	'+ Estructura de tabla insudb.Tarif_tab_col al 04-25-2002 17:52:20
	'+         Property                Type         DBType   Size Scale  Prec  Null
	'+-----------------------------------------------------------------------------
	Public nId_table As Integer ' NUMBER     22   0     5    N
	Public nId_column As Integer ' NUMBER     22   0     5    N
	Public sOperator As String ' CHAR       30   0     0    S
	Public nType_calc As Integer ' NUMBER     22   0     5    N
	Public nOrder As Integer ' NUMBER     22   0     5    N
	Public nUsercode As Integer ' NUMBER     22   0     5    N
	
	'%InsUpdTarif_tab_col: Se encarga de actualizar la tabla Tarif_tab_col
	Private Function InsUpdTarif_tab_col(ByVal nAction As Integer) As Boolean
		Dim lrecinsUpdTarif_tab_col As eRemoteDB.Execute
		
		On Error GoTo insUpdTarif_tab_col_Err
		
		lrecinsUpdTarif_tab_col = New eRemoteDB.Execute
		
		'+
		'+ Definición de store procedure insUpdTarif_tab_col al 04-25-2002 17:55:43
		'+
		With lrecinsUpdTarif_tab_col
			.StoredProcedure = "insUpdTarif_tab_col"
			.Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nId_table", nId_table, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nId_column", nId_column, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sOperator", sOperator, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nType_calc", nType_calc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOrder", nOrder, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			InsUpdTarif_tab_col = .Run(False)
		End With
		
insUpdTarif_tab_col_Err: 
		If Err.Number Then
			InsUpdTarif_tab_col = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecinsUpdTarif_tab_col may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsUpdTarif_tab_col = Nothing
		
	End Function
	
	'%Add: Crea un registro en la tabla
	Public Function Add() As Boolean
		Add = InsUpdTarif_tab_col(1)
	End Function
	
	'%Update: Actualiza un registro en la tabla
	Public Function Update() As Boolean
		Update = InsUpdTarif_tab_col(2)
	End Function
	
	'%Delete: Borra un registro en la tabla
	Public Function Delete() As Boolean
		Delete = InsUpdTarif_tab_col(3)
	End Function
	'%InsPostDP8001: Ejecuta el post de la transacción
	'%               Tabla de control de prima mínima(DP8001)
	Public Function InsPostDP8001(ByVal sAction As String, ByVal nId_table As Integer, ByVal nId_column As Integer, ByVal sOperator As String, ByVal nType_calc As Integer, ByVal nOrder As Integer, ByVal nUsercode As Integer) As Boolean
		
		On Error GoTo InsPostDP8001_Err
		
		With Me
			.nId_table = nId_table
			.nId_column = nId_column
			.sOperator = sOperator
			.nType_calc = nType_calc
			.nOrder = nOrder
			.nUsercode = nUsercode
		End With
		
		Select Case sAction
			Case "Add"
				InsPostDP8001 = Add
			Case "Update"
				InsPostDP8001 = Update
			Case "Del"
				InsPostDP8001 = Delete
		End Select
		
InsPostDP8001_Err: 
		If Err.Number Then
			InsPostDP8001 = False
		End If
		On Error GoTo 0
	End Function
	'% insValDP8001: Realiza la validación de los campos de la ventana DP8001
	Public Function insValDP8001(ByVal sCodispl As String, ByVal nId_table As Integer, ByVal nId_column As Integer, ByVal sOperator As String, ByVal nOrder As Integer, ByVal sWindowType As String, ByVal sAction As String) As String
		Dim lobjErrors As Object
		Dim lobjtarif_tab_col As Tarif_tab_cols
		
		lobjErrors = eRemoteDB.NetHelper.CreateClassInstance("eFunctions.Errors")
		
		On Error GoTo insValDP8001_Err
		
		If sWindowType = "PopUp" Then
			'+Validación del de la columna
			If nId_column = eRemoteDB.Constants.intNull Then
				Call lobjErrors.ErrorMessage(sCodispl, 55537,  , 2, "Código de la columna ")
			Else
				If sAction = "Add" Then
					If Find(nId_table, nId_column) Then
						Call lobjErrors.ErrorMessage(sCodispl, 10284)
					End If
				End If
			End If
			
			'+Validación del operador
			If sOperator = String.Empty Or sOperator = "0" Then
				Call lobjErrors.ErrorMessage(sCodispl, 55537,  , 2, "Operador ")
			End If
			
			'+Validación del numero de orden
			If nOrder = eRemoteDB.Constants.intNull Then
				Call lobjErrors.ErrorMessage(sCodispl, 55537,  , 2, "Orden ")
			End If
		Else
			lobjtarif_tab_col = New eTarif.Tarif_tab_cols
			If Not lobjtarif_tab_col.Find(nId_table) Then
				Call lobjErrors.ErrorMessage(sCodispl, 707009)
			End If
		End If
		
		insValDP8001 = lobjErrors.Confirm
		
insValDP8001_Err: 
		If Err.Number Then
			insValDP8001 = insValDP8001 & Err.Description
		End If
		'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjErrors = Nothing
		'UPGRADE_NOTE: Object lobjtarif_tab_col may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjtarif_tab_col = Nothing
		On Error GoTo 0
	End Function
	'%insExistsTarifValue: Indica si la tabla lógica de tarifas tiene valores asociados
	Public Function insExistsTarifValue(ByVal nId_table As Integer) As Boolean
		Dim lrecinsExistsTarifValue As eRemoteDB.Execute
		On Error GoTo insExistsTarifValue_Err
		
		lrecinsExistsTarifValue = New eRemoteDB.Execute
		
		With lrecinsExistsTarifValue
			.StoredProcedure = "InsDP8002pkg.insExistsTarifValue"
			.Parameters.Add("nId_table", nId_table, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nExists", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(False) Then
				insExistsTarifValue = .Parameters("nExists").Value > 0
			Else
				insExistsTarifValue = False
			End If
		End With
		
insExistsTarifValue_Err: 
		If Err.Number Then
			insExistsTarifValue = False
		End If
		'UPGRADE_NOTE: Object lrecinsExistsTarifValue may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsExistsTarifValue = Nothing
		On Error GoTo 0
	End Function
	
	'%Find: Lee los datos de la tabla
	Public Function Find(ByVal nId_table As Integer, ByVal nId_column As Integer) As Boolean
		Dim lrecreaTarif_tab_col As eRemoteDB.Execute
		Dim lclsreatarif_tab_col As Tarif_tab_col
		
		On Error GoTo reaTarif_tab_col_Err
		
		lrecreaTarif_tab_col = New eRemoteDB.Execute
		
		'+
		'+ Definición de store procedure reaAdjacence al 04-25-2002 17:54:39
		'+
		With lrecreaTarif_tab_col
			.StoredProcedure = "reatarif_tab_col"
			.Parameters.Add("nId_table", nId_table, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nId_column", nId_column, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(True) Then
				Find = True
				nId_table = .FieldToClass("nId_table")
				nId_column = .FieldToClass("nId_column")
				sOperator = .FieldToClass("sOperator")
				nType_calc = .FieldToClass("nType_calc")
				nOrder = .FieldToClass("nOrder")
			Else
				Find = False
			End If
		End With
		
reaTarif_tab_col_Err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecreaTarif_tab_col may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaTarif_tab_col = Nothing
		
	End Function
	
	'%Class_Initialize: Inicializa las propiedades cuando se instancia la clase
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		nId_table = eRemoteDB.Constants.intNull
		nId_column = eRemoteDB.Constants.intNull
		sOperator = ""
		nType_calc = eRemoteDB.Constants.intNull
		nOrder = eRemoteDB.Constants.intNull
		nUsercode = eRemoteDB.Constants.intNull
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
End Class






