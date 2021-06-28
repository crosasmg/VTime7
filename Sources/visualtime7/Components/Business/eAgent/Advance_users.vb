Option Strict Off
Option Explicit On
Public Class Advance_users
	'%-------------------------------------------------------%'
	'% $Workfile:: Advance_users.cls                        $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:41p                                $%'
	'% $Revision:: 9                                        $%'
	'%-------------------------------------------------------%'
	'+
	'+ Estructura de tabla Advance_users al 03-04-2002 10:40:24
	'+     Property                Type         DBType   Size Scale  Prec  Null
	'+-------------------------------------------------------------------------
	Public nCodmodpay As Integer ' NUMBER     22   0     5    N
	Public nUser As Integer ' NUMBER     22   0     5    N
	Public sStatregt As String
	Public nUsercode As Integer ' NUMBER     22   0     5    N
	
	
	'%InsUpdAdvance_users: Se encarga de actualizar la tabla Advance_users
	Private Function InsUpdAdvance_users(ByVal nAction As Integer) As Boolean
		
		Dim lrecinsUpdAdvance_users As eRemoteDB.Execute
		On Error GoTo insUpdAdvance_users_Err
		
		lrecinsUpdAdvance_users = New eRemoteDB.Execute
		'+
		'+ Definición de store procedure insUpdAdvance_users al 03-04-2002 11:34:19
		'+
		With lrecinsUpdAdvance_users
			.StoredProcedure = "insUpdAdvance_users"
			.Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUser", nUser, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCodmodpay", nCodmodpay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sStatregt", sStatregt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			InsUpdAdvance_users = .Run(False)
		End With
		
insUpdAdvance_users_Err: 
		If Err.Number Then
			InsUpdAdvance_users = False
		End If
		'UPGRADE_NOTE: Object lrecinsUpdAdvance_users may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsUpdAdvance_users = Nothing
		On Error GoTo 0
	End Function
	
	'%Add: Crea un registro en la tabla
	Public Function Add() As Boolean
		Add = InsUpdAdvance_users(1)
	End Function
	
	'%Update: Actualiza un registro en la tabla
	Public Function Update() As Boolean
		Update = InsUpdAdvance_users(2)
	End Function
	
	'%Delete: Borra un registro en la tabla
	Public Function Delete() As Boolean
		Delete = InsUpdAdvance_users(3)
	End Function
	
	'%InsValMAG770_K: Validaciones de la transacción(Header)
	Public Function InsValMAG770_K(ByVal sCodispl As String, ByVal sAction As String, ByVal nUser As Integer, ByVal nCodmodpay As Integer, ByVal sStatregt As String) As String
		Dim lclsErrors As eFunctions.Errors
		
		On Error GoTo InsValMAG770_K_Err
		lclsErrors = New eFunctions.Errors
		
		With lclsErrors
			
			'+ Código de usuario: Debe estar lleno
			
			If nUser = 0 Or nUser = eRemoteDB.Constants.intNull Then
				.ErrorMessage(sCodispl, 60008)
			End If
			
			
			'+ Modalidad: Debe estar lleno.
			
			If nCodmodpay = 0 Or nCodmodpay = eRemoteDB.Constants.intNull Then
				.ErrorMessage(sCodispl, 60422)
			End If
			
			'+ Modalidad: Debe estar lleno.
			
			If CDbl(sStatregt) = 0 Or sStatregt = CStr(eRemoteDB.Constants.intNull) Then
				.ErrorMessage(sCodispl, 55633)
			End If
			
			
			' Registro no debe estar repetido
			
			If sAction = "Add" Then
				If Find(nUser, nCodmodpay) Then
					.ErrorMessage(sCodispl, 60464)
				End If
			End If
			
			InsValMAG770_K = .Confirm
		End With
		
InsValMAG770_K_Err: 
		If Err.Number Then
			InsValMAG770_K = "InsValMAG770_K: " & Err.Description
		End If
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		On Error GoTo 0
		
	End Function
	
	'%InsPostMAG770_k: Ejecuta el post de la transacción
	'%               Tabla de presupuesto de dotación por agencia(MAG770)
	Public Function InsPostMAG770_k(ByVal sCodispl As String, ByVal sAction As String, ByVal nUser As Integer, ByVal nCodmodpay As Integer, ByVal sStatregt As String, ByVal nUsercode As Integer) As Boolean
		
		On Error GoTo InsPostMAG770_k_Err
		
		With Me
			.nCodmodpay = nCodmodpay
			.nUser = nUser
			.nUsercode = nUsercode
			.sStatregt = sStatregt
		End With
		
		Select Case sAction
			Case "Add"
				InsPostMAG770_k = Add
			Case "Update"
				InsPostMAG770_k = Update
			Case "Del"
				InsPostMAG770_k = Delete
		End Select
		
InsPostMAG770_k_Err: 
		If Err.Number Then
			InsPostMAG770_k = False
		End If
		On Error GoTo 0
	End Function
	
	'%Find: Metodo que devuelve los campos de un registro especifico
	Public Function Find(ByVal nUser As Integer, ByVal nCodmodpay As Integer) As Boolean
		
		Dim lrecReaAdvance_users As eRemoteDB.Execute
		
		On Error GoTo Find_Err
		
		lrecReaAdvance_users = New eRemoteDB.Execute
		
		With lrecReaAdvance_users
			.StoredProcedure = "ReaAdvance_users_v"
			.Parameters.Add("nUser", nUser, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCodmodpay", nCodmodpay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Me.sStatregt = CStr(CShort(.FieldToClass("sStatregt")))
				Find = True
			End If
		End With
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecReaAdvance_users may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecReaAdvance_users = Nothing
	End Function
	
	'%Class_Initialize: Inicializa las propiedades cuando se instancia la clase
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		nCodmodpay = eRemoteDB.Constants.intNull
		nUser = eRemoteDB.Constants.intNull
		nUsercode = eRemoteDB.Constants.intNull
		sStatregt = String.Empty
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
End Class






