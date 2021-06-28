Option Strict Off
Option Explicit On
Public Class Veh_allow
	'%-------------------------------------------------------%'
	'% $Workfile:: Veh_allow.cls                            $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:38p                                $%'
	'% $Revision:: 16                                       $%'
	'%-------------------------------------------------------%'
	'+ Definición de la tabla VEH_ALLOW tomada el 14/03/2002 13:44
	
	'+ Column_Name                                   Type      Length  Prec  Scale Nullable
	' ------------------------------ --------------- - -------- ------- ----- ------ --------
	Public sVehCode As String ' VARCHAR2       6              No
	Public nBranch As Integer ' NUMBER        22     5      0 No
	Public nProduct As Integer ' NUMBER        22     5      0 No
	Private mlngUsercode As Integer ' NUMBER        22     5      0 No
	
	'%InsUpdVeh_allow: Realiza la actualización de la tabla
	Private Function InsUpdVeh_allow(ByVal nAction As Integer) As Boolean
		Dim lrecInsUpdVeh_allow As eRemoteDB.Execute
		
		On Error GoTo InsUpdVeh_allow_Err
		lrecInsUpdVeh_allow = New eRemoteDB.Execute
		'+ Definición de parámetros para stored procedure 'InsUpdVeh_allow'
		'+ Información leída el 15/03/2002
		With lrecInsUpdVeh_allow
			.StoredProcedure = "InsUpdVeh_allow"
			.Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 38, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sVehCode", sVehCode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 6, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", mlngUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			InsUpdVeh_allow = .Run(False)
		End With
		
InsUpdVeh_allow_Err: 
		If Err.Number Then
			InsUpdVeh_allow = False
		End If
		'UPGRADE_NOTE: Object lrecInsUpdVeh_allow may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecInsUpdVeh_allow = Nothing
		On Error GoTo 0
	End Function
	
	'%Add: Crea un registro en la tabla
	Public Function Add() As Boolean
		Add = InsUpdVeh_allow(1)
	End Function
	
	'%Update: Actualiza los datos de la tabla
	Public Function Update() As Boolean
		Update = InsUpdVeh_allow(2)
	End Function
	
	'%Delete: Borra los datos de la tabla
	Public Function Delete() As Boolean
		Delete = InsUpdVeh_allow(3)
	End Function
	
	'%IsExist: Lee los datos de la tabla
	Public Function IsExist(ByVal sVehCode As String, ByVal nBranch As Integer, ByVal nProduct As Integer) As Boolean
		Dim lrecreaVeh_allow_v As eRemoteDB.Execute
		
		On Error GoTo reaVeh_allow_v_Err
		lrecreaVeh_allow_v = New eRemoteDB.Execute
		'+ Definición de store procedure reaVeh_allow_val 03-01-2002 17:14:26
		With lrecreaVeh_allow_v
			.StoredProcedure = "reaVeh_allow_v"
			.Parameters.Add("sVehcode", sVehCode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 6, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nExist", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Run(False)
			IsExist = .Parameters("nExist").Value > 0
		End With
		
reaVeh_allow_v_Err: 
		If Err.Number Then
			IsExist = False
		End If
		'UPGRADE_NOTE: Object lrecreaVeh_allow_v may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaVeh_allow_v = Nothing
		On Error GoTo 0
	End Function
	
	'%InsValMAU001Upd: Esta función se encarga de validar los datos del Form
	'%Tarifa de automóvil
	Public Function InsValMAU001Upd(ByVal sCodispl As String, ByVal sAction As String, ByVal sVehCode As String, ByVal nBranch As Integer, ByVal nProduct As Integer) As String
		Dim lclsErrors As eFunctions.Errors
		
		On Error GoTo InsValMAU001Upd_Err
		lclsErrors = New eFunctions.Errors
		'+ Validación del ramo
		With lclsErrors
			If nBranch = eRemoteDB.Constants.intNull Then
				.ErrorMessage(sCodispl, 1022)
				
				'+ Validación de duplicidad Vehículo/Ramo/Producto
			Else
				If sAction = "Add" Then
					If IsExist(sVehCode, nBranch, nProduct) Then
						.ErrorMessage(sCodispl, 60337)
					End If
				End If
			End If
			InsValMAU001Upd = .Confirm
		End With
		
InsValMAU001Upd_Err: 
		If Err.Number Then
			InsValMAU001Upd = "InsValMAU001Upd: " & Err.Description
		End If
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		On Error GoTo 0
	End Function
	
	'%InsPostMAU001Upd: Esta función realiza los cambios de BD según especificaciones funcionales
	'%                 de la transacción (MAU001)
	Public Function InsPostMAU001Upd(ByVal sAction As String, ByVal sVehCode As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nUsercode As Integer) As Boolean
		
		On Error GoTo InsPostMAU001Upd_Err
		With Me
			.sVehCode = sVehCode
			.nBranch = nBranch
			.nProduct = IIf(nProduct = eRemoteDB.Constants.intNull, 0, nProduct)
			mlngUsercode = nUsercode
			
			Select Case sAction
				Case "Add"
					'+ Se crea el registro
					InsPostMAU001Upd = .Add
					
					'+ Se modifica el registro
				Case "Update"
					InsPostMAU001Upd = .Update
					
					'+ Se elimina el registro
				Case "Del"
					InsPostMAU001Upd = .Delete
					
			End Select
		End With
		
InsPostMAU001Upd_Err: 
		If Err.Number Then
			InsPostMAU001Upd = False
		End If
		On Error GoTo 0
	End Function
	
	'% Class_Initialize: se controla la apertura de la clase
	'---------------------------------------------------------
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		'---------------------------------------------------------
		sVehCode = String.Empty
		nBranch = eRemoteDB.Constants.intNull
		nProduct = eRemoteDB.Constants.intNull
		mlngUsercode = eRemoteDB.Constants.intNull
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
End Class






