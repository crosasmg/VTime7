Option Strict Off
Option Explicit On
Public Class Tab_au_veh
	'%-------------------------------------------------------%'
	'% $Workfile:: Tab_au_veh.cls                           $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:38p                                $%'
	'% $Revision:: 12                                       $%'
	'%-------------------------------------------------------%'
	
	'-
	'- Estructura de tabla tab_au_veh al 09-19-2002 14:02:13
	'-     Property                Type         DBType   Size Scale  Prec  Null
	Public sVehCode As String ' CHAR       6    0     0    N
	Public sDescript As String ' CHAR       30   0     0    S
	Public nNational As Integer ' NUMBER     22   0     5    S
	Public nVehBrand As Integer ' NUMBER     22   0     5    S
	Public sStatregt As String ' CHAR       1    0     0    S
	Public sVehmodel As String ' CHAR       20   0     0    S
	Public nVehType As Integer ' NUMBER     22   0     5    N
	Public nVehplace As Integer ' NUMBER     22   0     5    S
	Public nVehpma As Integer ' NUMBER     22   0     5    S
	
	Private mlngUsercode As Integer ' NUMBER     22   0     5    S
	
    Public mcolTab_au_val As New tab_au_vals
    Public mcolVeh_allow As New Veh_allows
	
	'%Find: Este metodo retorna VERDADERO o FALSO dependiendo de la existencia o no de registros en la
	'%tabla "Tab_au_veh"
	Public Function Find(ByVal sVehCode As String) As Boolean
		Dim lrecreaTab_au_veh As eRemoteDB.Execute
		
		On Error GoTo Find_Err
		
		'+Definición de parámetros para stored procedure 'insudb.reaTab_au_veh'
		'+Información leída el 22/01/2001 2:59:05 PM
		lrecreaTab_au_veh = New eRemoteDB.Execute
		With lrecreaTab_au_veh
			.StoredProcedure = "reaTab_au_veh"
			.Parameters.Add("sVehcode", sVehCode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 6, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Me.sVehCode = sVehCode
				nNational = .FieldToClass("nNational")
				nVehBrand = .FieldToClass("nVehBrand")
				sStatregt = .FieldToClass("sStatregt")
				sVehmodel = .FieldToClass("sVehModel")
				nVehType = .FieldToClass("nVehType")
				nVehplace = .FieldToClass("nVehplace")
				nVehpma = .FieldToClass("nVehpma")
				sDescript = .FieldToClass("sDescript")
				.RCloseRec()
				Find = True
			End If
		End With
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		'UPGRADE_NOTE: Object lrecreaTab_au_veh may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaTab_au_veh = Nothing
		On Error GoTo 0
	End Function
	
	'%InsUpdTab_au_veh: Actualiza la informacion de la tabla de vehiculos
	Private Function InsUpdTab_au_veh(ByVal nAction As Integer) As Boolean
		Dim lrecinsUpdtab_au_veh As eRemoteDB.Execute
		
		On Error GoTo insUpdtab_au_veh_Err
		lrecinsUpdtab_au_veh = New eRemoteDB.Execute
		'+ Definición de store procedure insUpdtab_au_veh al 10-03-2002 15:57:37
		With lrecinsUpdtab_au_veh
			.StoredProcedure = "insUpdtab_au_veh"
			.Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sVehcode", sVehCode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 6, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDescript", sDescript, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 60, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nNational", nNational, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nVehbrand", nVehBrand, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sStatregt", sStatregt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sVehmodel", sVehmodel, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 60, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nVehtype", nVehType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", mlngUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nVehplace", nVehplace, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nVehpma", nVehpma, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("SVEHCODE_NEW", String.Empty, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 6, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            InsUpdTab_au_veh = .Run(False)
            If InsUpdTab_au_veh Then
                sVehCode = .Parameters.Item("SVEHCODE_NEW").Value
            End If
		End With
		
insUpdtab_au_veh_Err: 
		If Err.Number Then
			InsUpdTab_au_veh = False
		End If
		'UPGRADE_NOTE: Object lrecinsUpdtab_au_veh may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsUpdtab_au_veh = Nothing
		On Error GoTo 0
	End Function
	
	'%Add: Esta función agrega registros a la tabla TAB_AU_VEH
	Public Function Add() As Boolean
		Add = InsUpdTab_au_veh(1)
	End Function
	
	'%Update: Esta función actualiza registros en la tabla TAB_AU_VEH
	Public Function Update() As Boolean
		Update = InsUpdTab_au_veh(2)
	End Function
	
	'%Delete: Esta función elimina registros de la tabla TAB_AU_VEH
	Public Function Delete() As Boolean
		Delete = InsUpdTab_au_veh(3)
	End Function
	
	'%IsExist: Valida la existencia de un código.
	Public Function IsExist(ByVal sVehCode As String) As Boolean
		Dim lrecTab_au_veh As eRemoteDB.Execute
		
		On Error GoTo IsExist_Err
		lrecTab_au_veh = New eRemoteDB.Execute
		With lrecTab_au_veh
			.StoredProcedure = "valTab_au_veh"
			.Parameters.Add("sVehcode", sVehCode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 6, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCount", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Run(False)
			IsExist = .Parameters("nCount").Value > 0
		End With
		
IsExist_Err: 
		If Err.Number Then
			IsExist = False
		End If
		'UPGRADE_NOTE: Object lrecTab_au_veh may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecTab_au_veh = Nothing
		On Error GoTo 0
	End Function
	
	'%Find_Auto: Valida que en caso de eliminar que no exista información relacionada en la tabla
	'%            AUTO.
	Private Function Find_Auto(ByVal sVehCode As String) As Boolean
		Dim lrecreaAuto_v As eRemoteDB.Execute
		
		On Error GoTo Find_Auto_Err
		lrecreaAuto_v = New eRemoteDB.Execute
		
		'+ Definición de store procedure reaAuto_v al 10-07-2002 13:22:02
		With lrecreaAuto_v
			.StoredProcedure = "reaAuto_v"
			.Parameters.Add("sVehcode", sVehCode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 6, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nExist", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Call .Run(False)
			Find_Auto = .Parameters("nExist").Value = 1
		End With
		
Find_Auto_Err: 
		If Err.Number Then
			Find_Auto = False
		End If
		'UPGRADE_NOTE: Object lrecreaAuto_v may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaAuto_v = Nothing
		On Error GoTo 0
	End Function
	
	'%InsValMAU001_K: Esta función se encarga de validar los datos introducidos en la cabecera de
	'%la forma.
	Public Function InsValMAU001_K(ByVal sCodispl As String, ByVal nAction As Integer, ByVal sVehCode As String) As String
		Dim lclsErrors As eFunctions.Errors
		Dim lblnExist As Boolean
		
		On Error GoTo InsValMAU001_K_Err
		lclsErrors = New eFunctions.Errors
		
		With lclsErrors
			'+Validación del campo: Código.
            If sVehCode = String.Empty And nAction <> eFunctions.Menues.TypeActions.clngActionadd Then
                .ErrorMessage(sCodispl, 10046)
            Else
                '+ Si la acción es registrar no debe existir información en la tabla TAB_AU_VEH.
                lblnExist = IsExist(sVehCode)
                If nAction = eFunctions.Menues.TypeActions.clngActionadd Then
                    If lblnExist Then
                        .ErrorMessage(sCodispl, 10004)
                    End If

                    '+ Si la acción no es registrar se verifica que exista información en la tabla.
                Else
                    If Not lblnExist Then
                        .ErrorMessage(sCodispl, 10012)
                    Else
                        If nAction = eFunctions.Menues.TypeActions.clngActioncut Then
                            If Find_Auto(sVehCode) Then
                                .ErrorMessage(sCodispl, 3950)
                            End If
                        End If
                    End If
                End If
            End If
			InsValMAU001_K = .Confirm
		End With
		
InsValMAU001_K_Err: 
		If Err.Number Then
			InsValMAU001_K = "InsValMAU001_K: " & Err.Description
		End If
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		On Error GoTo 0
	End Function
	
	'%InsPreMAU001: Esta función se encarga de validar los datos introducidos en la zona de detalle
	Public Function InsPreMAU001(ByVal sVehCode As String) As Boolean
		On Error GoTo InsPreMAU001_Err
		
		'+Se busca los datos generales del vehículo
		If Find(sVehCode) Then
			InsPreMAU001 = True
			'+Se la información de la tabla de valores asegurados de vehículos.
			mcolTab_au_val = New tab_au_vals
			Call mcolTab_au_val.Find(sVehCode)
			
			'+Se la información de la tabla de Ramos-Productos permitidos por vehículo
			mcolVeh_allow = New Veh_allows
			Call mcolVeh_allow.Find(sVehCode)
		End If
		
InsPreMAU001_Err: 
		If Err.Number Then
			InsPreMAU001 = False
		End If
		On Error GoTo 0
	End Function
	
	'%InsValMAU001: Esta función se encarga de validar los datos introducidos en la zona de detalle
	Public Function InsValMAU001(ByVal sCodispl As String, ByVal nVehType As Integer, ByVal nVehBrand As Integer, ByVal nVehplace As Integer, ByVal nVehpma As Integer, ByVal nCountTab_au_val As Integer, ByVal nAction As Integer) As String
		Dim lclsErrors As eFunctions.Errors
		
		On Error GoTo InsValMAU001_Err
		lclsErrors = New eFunctions.Errors
		
		If nAction <> eFunctions.Menues.TypeActions.clngActioncut Then
			With lclsErrors
				'+ Se valida la columna: nVehbrand
				If nVehBrand = eRemoteDB.Constants.intNull Then
					.ErrorMessage(sCodispl, 10013)
				End If
				
				'+ Se valida la columna: nVehtype
				If nVehType = eRemoteDB.Constants.intNull Then
					.ErrorMessage(sCodispl, 13988)
				End If
				
				'+ Se valida la columna: nVehplace
				If nVehplace = eRemoteDB.Constants.intNull Then
					.ErrorMessage(sCodispl, 3647)
				End If
				
				'+ Se valida la columna: nVehpma
				If nVehpma = eRemoteDB.Constants.intNull Then
					.ErrorMessage(sCodispl, 3437)
				End If
				
				'+ Se valida: nCountTab_au_val
				If nCountTab_au_val = eRemoteDB.Constants.intNull Then
					.ErrorMessage(sCodispl, 1928)
				End If
				
				InsValMAU001 = .Confirm
			End With
		End If
		
InsValMAU001_Err: 
		If Err.Number Then
			InsValMAU001 = "InsValMAU001: " & Err.Description
		End If
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		On Error GoTo 0
	End Function
	
	'%InsPostMAU001: Esta función se encarga de crear/actualizar los registros
	'%               correspondientes en la tabla Tab_au_veh
	Public Function InsPostMAU001(ByVal nAction As Integer, ByVal sVehCode As String, ByVal sStatregt As String, ByVal sDescript As String, ByVal sVehmodel As String, ByVal nVehType As Integer, ByVal nVehBrand As Integer, ByVal nVehplace As Integer, ByVal nVehpma As Integer, ByVal nNational As Integer, ByVal nUsercode As Integer) As Boolean
		
		On Error GoTo InsPostMAU001_err
		
		With Me
			InsPostMAU001 = True
			If nAction <> eFunctions.Menues.TypeActions.clngActionQuery Then
				.sVehCode = sVehCode
				.sStatregt = sStatregt
				.sDescript = sDescript
				.sVehmodel = sVehmodel
				.nVehType = nVehType
				.nVehBrand = nVehBrand
				.nVehplace = nVehplace
				.nVehpma = nVehpma
				.nNational = nNational
				mlngUsercode = nUsercode
				Select Case nAction
					'+Si la opción seleccionada es Registrar
					Case eFunctions.Menues.TypeActions.clngActionadd
						InsPostMAU001 = .Add()
						
						'+Si la opción seleccionada es Modificar
					Case eFunctions.Menues.TypeActions.clngActionUpdate
						InsPostMAU001 = .Update()
						
						'+Si la opción seleccionada es Eliminar
					Case eFunctions.Menues.TypeActions.clngActioncut
						InsPostMAU001 = .Delete()
				End Select
			End If
		End With
		
InsPostMAU001_err: 
		If Err.Number Then
			InsPostMAU001 = False
		End If
		On Error GoTo 0
	End Function
	
	'%InsValMAU001Upd: Valida las partes repetitivas de la transacción
	Public Function InsValMAU001Upd(ByVal sCodispl As String, ByVal sAction As String, ByVal sGrid As String, ByVal sVehCode As String, ByVal nYear As Integer, ByVal nCapital As Double, ByVal nBranch As Integer, ByVal nProduct As Integer) As String
		Dim lclsObject As Object
		On Error GoTo InsValMAU001Upd_Err
		
		If sGrid = "1" Then
			lclsObject = New Tab_au_val
			InsValMAU001Upd = lclsObject.InsValMAU001Upd(sCodispl, sAction, sVehCode, nYear, nCapital)
		Else
			lclsObject = New Veh_allow
			InsValMAU001Upd = lclsObject.InsValMAU001Upd(sCodispl, sAction, sVehCode, nBranch, nProduct)
		End If
		
InsValMAU001Upd_Err: 
		If Err.Number Then
			InsValMAU001Upd = "InsValMAU001Upd: " & Err.Description
		End If
		'UPGRADE_NOTE: Object lclsObject may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsObject = Nothing
		On Error GoTo 0
	End Function
	
	'%InsPostMAU001Upd: Valida las partes repetitivas de la transacción
	Public Function InsPostMAU001Upd(ByVal sAction As String, ByVal sGrid As String, ByVal sVehCode As String, ByVal nYear As Integer, ByVal nCapital As Double, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nUsercode As Object) As Boolean
		Dim lclsObject As Object
		On Error GoTo InsPostMAU001Upd_Err
		
		If sGrid = "1" Then
			lclsObject = New Tab_au_val
			InsPostMAU001Upd = lclsObject.InsPostMAU001Upd(sAction, sVehCode, nYear, nCapital, nUsercode)
		Else
			lclsObject = New Veh_allow
			InsPostMAU001Upd = lclsObject.InsPostMAU001Upd(sAction, sVehCode, nBranch, nProduct, nUsercode)
		End If
		
InsPostMAU001Upd_Err: 
		If Err.Number Then
			InsPostMAU001Upd = False
		End If
		'UPGRADE_NOTE: Object lclsObject may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsObject = Nothing
		On Error GoTo 0
	End Function
	
	'%Class_Initialize: Se ejecuta cuando se instancia la clase
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		sVehCode = String.Empty
		sDescript = String.Empty
		nNational = eRemoteDB.Constants.intNull
		nVehBrand = eRemoteDB.Constants.intNull
		sStatregt = String.Empty
		sVehmodel = String.Empty
		nVehType = eRemoteDB.Constants.intNull
		mlngUsercode = eRemoteDB.Constants.intNull
		nVehplace = eRemoteDB.Constants.intNull
		nVehpma = eRemoteDB.Constants.intNull
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'%Class_Terminate: Se ejecuta cuando se destruye la clase
	'UPGRADE_NOTE: Class_Terminate was upgraded to Class_Terminate_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Terminate_Renamed()
		'UPGRADE_NOTE: Object mcolTab_au_val may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mcolTab_au_val = Nothing
		'UPGRADE_NOTE: Object mcolVeh_allow may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mcolVeh_allow = Nothing
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
End Class






