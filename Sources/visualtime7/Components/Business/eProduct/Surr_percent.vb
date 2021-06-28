Option Strict Off
Option Explicit On
Public Class Surr_percent
	'%-------------------------------------------------------%'
	'% $Workfile:: Surr_percent.cls                         $%'
	'% $Author:: MVazquez                                   $%'
	'% $Date:: 9/08/03 12:35p                               $%'
	'% $Revision:: 9                                        $%'
	'%-------------------------------------------------------%'
	
	'- Estructura de tabla insudb.Surr_percent al 11-21-2001 15:24:48
	'-     Property                    Type         DBType   Size Scale  Prec  Null
	Public nBranch As Integer ' NUMBER     22   0     5    N
	Public nProduct As Integer ' NUMBER     22   0     5    N
	Public nQSurrIni As Integer ' NUMBER     22   0     5    N
	Public dEffecdate As Date ' DATE       7    0     0    N
	Public nQSurrEnd As Integer ' NUMBER     22   0     5    N
	Public nPercent As Double ' NUMBER     22   2     5    S
	Public nUsercode As Integer ' NUMBER     22   0     5    N
	
	'%InsUpdSurr_percent: Se encarga de actualizar la tabla Surr_percent
	Private Function InsUpdSurr_percent(ByVal nAction As Integer) As Boolean
		Dim lrecinsUpdSurr_percent As eRemoteDB.Execute
		
		On Error GoTo insUpdSurr_percent_Err
		
		InsUpdSurr_percent = False
		
		lrecinsUpdSurr_percent = New eRemoteDB.Execute
		
		'+
		'+ Definición de store procedure insUpdSurr_percent al 11-21-2001 15:36:39
		'+
		With lrecinsUpdSurr_percent
			.StoredProcedure = "insUpdSurr_percent"
			.Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nQSurrIni", nQSurrIni, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nQSurrEnd", nQSurrEnd, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPercent", nPercent, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			InsUpdSurr_percent = .Run(False)
			
		End With
		
insUpdSurr_percent_Err: 
		If Err.Number Then
			InsUpdSurr_percent = False
		End If
		
		'UPGRADE_NOTE: Object lrecinsUpdSurr_percent may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsUpdSurr_percent = Nothing
		On Error GoTo 0
	End Function
	
	'%Add: Crea un registro en la tabla
	Public Function Add() As Boolean
		Add = InsUpdSurr_percent(1)
	End Function
	
	'%Update: Actualiza un registro en la tabla
	Public Function Update() As Boolean
		Update = InsUpdSurr_percent(2)
	End Function
	
	'%Delete: Borra un registro en la tabla
	Public Function Delete() As Boolean
		Delete = InsUpdSurr_percent(3)
	End Function
	
	'%Find: Lee los datos de la tabla
	Public Function Find(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nQSurrIni As Integer, ByVal dEffecdate As Date, Optional ByVal lblnFind As Boolean = False) As Boolean
		Dim lrecreaSurr_percent As eRemoteDB.Execute
		On Error GoTo reaSurr_percent_Err
		
		Find = False
		
		lrecreaSurr_percent = New eRemoteDB.Execute
		
		'+
		'+ Definición de store procedure reaSurr_percent al 11-21-2001 15:29:42
		'+
		With lrecreaSurr_percent
			.StoredProcedure = "reaSurr_percent"
			With .Parameters
				.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Add("nQSurrIni", nQSurrIni, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			End With
			
			If .Run(True) Then
				Find = True
				Me.nBranch = nBranch
				Me.nProduct = nProduct
				Me.nQSurrIni = nQSurrIni
				Me.nQSurrEnd = .FieldToClass("nQSurrEnd")
				Me.dEffecdate = dEffecdate
				Me.nPercent = .FieldToClass("nPercent")
				Me.nUsercode = .FieldToClass("nUsercode")
				.RCloseRec()
			End If
		End With
		
reaSurr_percent_Err: 
		If Err.Number Then
			Find = False
		End If
		
		'UPGRADE_NOTE: Object lrecreaSurr_percent may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaSurr_percent = Nothing
		On Error GoTo 0
	End Function
	
	'%InsValRange: Valida que no exista rango antes de crearlo
	Public Function InsValRange(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nQSurrIni As Integer, ByVal dEffecdate As Date, ByVal nQSurrEnd As Integer) As Boolean
		
		Dim lrecinsValrange_Surr_percent As eRemoteDB.Execute
		On Error GoTo insValrange_Surr_percent_Err
		
		InsValRange = False
		
		lrecinsValrange_Surr_percent = New eRemoteDB.Execute
		
		'+
		'+ Definición de store procedure insValrange_Surr_percent al 11-21-2001 15:44:12
		'+
		With lrecinsValrange_Surr_percent
			.StoredProcedure = "insValrange_Surr_percent"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nQSurrIni", nQSurrIni, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nQSurrEnd", nQSurrEnd, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			InsValRange = Not .Run(True)
			.RCloseRec()
		End With
		
insValrange_Surr_percent_Err: 
		If Err.Number Then
			InsValRange = False
		End If
		
		'UPGRADE_NOTE: Object lrecinsValrange_Surr_percent may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsValrange_Surr_percent = Nothing
		On Error GoTo 0
	End Function
	
	
	'%InsValDP8006: Validaciones de la transacción(Folder)
	Public Function InsValDP8006(ByVal sCodispl As String, ByVal sAction As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nQSurrIni As Integer, ByVal dEffecdate As Date, ByVal nQSurrEnd As Integer, ByVal nPercent As Double) As String
		Dim lclsErrors As eFunctions.Errors
		
		On Error GoTo InsValDP8006_Err
		lclsErrors = New eFunctions.Errors
		
		With lclsErrors
			If nQSurrIni <= 0 Then
				.ErrorMessage(sCodispl, 80092)
			End If
			
			'+ Si se está agregando un rango
			If sAction = "Add" Then
				'+ Se valida que el nuevo rango no contenga a los existentes
				If Not InsValRange(nBranch, nProduct, nQSurrIni, dEffecdate, IIf(nQSurrEnd < 0, 0, nQSurrEnd)) Then
					.ErrorMessage(sCodispl, 80093)
				End If
			End If
			
			'+ Si se actualiza o agrega se requiere validar final de rango
			If sAction <> "Del" Then
				If nQSurrIni > nQSurrEnd And nQSurrEnd <> 0 Then
					.ErrorMessage(sCodispl, 80094)
				End If
				
				If nPercent = eRemoteDB.Constants.intNull Then
					.ErrorMessage(sCodispl, 80095)
				End If
				
			End If
			
			InsValDP8006 = .Confirm
		End With
		
InsValDP8006_Err: 
		If Err.Number Then
			InsValDP8006 = "InsValDP8006: " & Err.Description
		End If
		
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		On Error GoTo 0
	End Function
	
	
	'%InsPostDP8006: Ejecuta el post de la transacción DP8006
	Public Function InsPostDP8006(ByVal sAction As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nQSurrIni As Integer, ByVal dEffecdate As Date, ByVal nQSurrEnd As Integer, ByVal nPercent As Double, ByVal nUsercode As Integer) As Boolean
		
		On Error GoTo InsPostDP8006_Err
		
		Me.nBranch = nBranch
		Me.nProduct = nProduct
		Me.nQSurrIni = nQSurrIni
		Me.dEffecdate = dEffecdate
		Me.nQSurrEnd = nQSurrEnd
		Me.nPercent = nPercent
		Me.nUsercode = nUsercode
		
		Select Case sAction
			Case "Add"
				InsPostDP8006 = Add
			Case "Update"
				InsPostDP8006 = Update
			Case "Del"
				InsPostDP8006 = Delete
		End Select
		
InsPostDP8006_Err: 
		If Err.Number Then
			InsPostDP8006 = False
		End If
		On Error GoTo 0
	End Function
	
	
	'%Class_Initialize: Inicializa las propiedades cuando se instancia la clase
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		
		nBranch = eRemoteDB.Constants.intNull
		nProduct = eRemoteDB.Constants.intNull
		nQSurrIni = eRemoteDB.Constants.intNull
		dEffecdate = eRemoteDB.Constants.dtmNull
		nQSurrEnd = eRemoteDB.Constants.intNull
		nPercent = eRemoteDB.Constants.intNull
		nUsercode = eRemoteDB.Constants.intNull
		
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
End Class






