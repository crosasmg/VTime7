Option Strict Off
Option Explicit On
Public Class Auto_damage
	'%-------------------------------------------------------%'
	'% $Workfile:: Auto_damage.cls                          $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:34p                                $%'
	'% $Revision:: 13                                       $%'
	'%-------------------------------------------------------%'
	
	'+ Estructura de tabla clobos.auto_damage al 04-22-2002 13:02:20
	'+  Property                    Type         DBType   Size Scale  Prec  Null
	'+-------------------------------------------------------------------------
	Public nServ_Order As Double ' NUMBER     22   0    10   N
	Public nPart_auto As Integer ' NUMBER     22   0     5    N
	Public nDamag_auto As Integer ' NUMBER     22   0     5    S
	Public nDamage_magnif As Integer ' NUMBER     22   0     5    S
	Public nDeduc As Double ' NUMBER     22   2     4    S
	Public dCompdate As Date ' DATE       7    0     0    N
	Public nUsercode As Integer ' NUMBER     22   0     5    N
	'%InsUpdauto_damage: Se encarga de actualizar la tabla auto_damage
	Private Function InsUpdauto_damage(ByVal nAction As Integer) As Boolean
		
		Dim lrecinsUpdauto_damage As eRemoteDB.Execute
		On Error GoTo insUpdauto_damage_Err
		
		lrecinsUpdauto_damage = New eRemoteDB.Execute
		
		'+
		'+ Definición de store procedure insUpdauto_damage al 04-22-2002 13:21:32
		'+
		With lrecinsUpdauto_damage
			.StoredProcedure = "insUpdauto_damage"
			.Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nServ_order", nServ_Order, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPart_auto", nPart_auto, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDamag_auto", nDamag_auto, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDamage_magnif", nDamage_magnif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDeduc", nDeduc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			InsUpdauto_damage = .Run(False)
		End With
		
insUpdauto_damage_Err: 
		If Err.Number Then
			InsUpdauto_damage = False
		End If
		lrecinsUpdauto_damage = Nothing
		On Error GoTo 0
	End Function
	
	'% valExistsAuto_Damage: Valida si existen grupos asociados a una póliza
	Public Function valExistsAuto_Damage(ByVal nServ_Order As Double, ByVal nPart_auto As Integer, ByVal nDamag_auto As Integer) As Boolean
		Dim lrecAuto_Damage As eRemoteDB.Execute
		Dim lintExists As Integer
		On Error GoTo valExistsAuto_Damage_Err
		lrecAuto_Damage = New eRemoteDB.Execute
		
		With lrecAuto_Damage
			.StoredProcedure = "valExistsAuto_Damage"
			.Parameters.Add("nServ_order", nServ_Order, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPart_auto", nPart_auto, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDamag_auto", nDamag_auto, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nExists", lintExists, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Run(False)
			If .Parameters("nExists").Value = 1 Then
				valExistsAuto_Damage = True
			End If
		End With
		
valExistsAuto_Damage_Err: 
		If Err.Number Then
			valExistsAuto_Damage = False
		End If
		On Error GoTo 0
		lrecAuto_Damage = Nothing
	End Function
	'% insValOS591: se realizan las validaciones a la OS591
	Public Function insValOS591(ByVal sCodispl As String, ByVal sAction As String, ByVal nServ_Order As Double, ByVal nPart_auto As Integer, ByVal nDamag_auto As Integer) As String
		Dim lclsErrors As eFunctions.Errors
		On Error GoTo insValOS591_Err
		lclsErrors = New eFunctions.Errors
		
		If sAction = "Add" Then
			If valExistsAuto_Damage(nServ_Order, nPart_auto, nDamag_auto) Then
				Call lclsErrors.ErrorMessage(sCodispl, 8307)
			End If
		End If
		
		insValOS591 = lclsErrors.Confirm
		
insValOS591_Err: 
		If Err.Number Then
			insValOS591 = insValOS591 & Err.Description
		End If
		On Error GoTo 0
	End Function
	
	
	'%Add: Crea un registro en la tabla
	Public Function Add() As Boolean
		Add = InsUpdauto_damage(1)
	End Function
	
	'%Update: Actualiza un registro en la tabla
	Public Function Update() As Boolean
		Update = InsUpdauto_damage(2)
	End Function
	
	'%Delete: Borra un registro en la tabla
	Public Function Delete() As Boolean
		Delete = InsUpdauto_damage(3)
	End Function
	'%InsPostOS591: Ejecuta el post de la transacción
	'%               Tabla de control de prima mínima(OS591)
	Public Function InsPostOS591(ByVal sAction As String, ByVal nServ_Order As Double, ByVal nPart_auto As Integer, ByVal nDamag_auto As Integer, ByVal nDamage_magnif As Integer, ByVal nDeduc As Double, ByVal nUsercode As Integer) As Boolean
		
		On Error GoTo InsPostOS591_Err
		
		With Me
			.nServ_Order = nServ_Order
			.nPart_auto = nPart_auto
			.nDamag_auto = nDamag_auto
			.nDamage_magnif = nDamage_magnif
			.nDeduc = nDeduc
			.nUsercode = nUsercode
		End With
		
		Select Case sAction
			Case "Add"
				InsPostOS591 = Add
			Case "Update"
				InsPostOS591 = Update
			Case "Del"
				InsPostOS591 = Delete
		End Select
		
InsPostOS591_Err: 
		If Err.Number Then
			InsPostOS591 = False
		End If
		On Error GoTo 0
	End Function
	
	'%Class_Initialize: Inicializa las propiedades cuando se instancia la clase
	Private Sub Class_Initialize_Renamed()
		nServ_Order = eRemoteDB.Constants.intNull
		nPart_auto = eRemoteDB.Constants.intNull
		nDamag_auto = eRemoteDB.Constants.intNull
		nDamage_magnif = eRemoteDB.Constants.intNull
		nDeduc = eRemoteDB.Constants.intNull
		dCompdate = eRemoteDB.Constants.dtmNull
		nUsercode = eRemoteDB.Constants.intNull
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
End Class






