Option Strict Off
Option Explicit On
Public Class Comm_mod
	'%-------------------------------------------------------%'
	'% $Workfile:: Comm_mod.cls                             $%'
	'% $Author:: Nvaplat37                                  $%'
	'% $Date:: 22/08/03 7:27p                               $%'
	'% $Revision:: 2                                        $%'
	'%-------------------------------------------------------%'
	
	'- Estructura de tabla cliallopro
	'-         Property                Type         DBType   Size Scale  Prec  Null
	Public nBranch As Integer ' NUMBER     22   0     5    N
	Public nProduct As Integer ' NUMBER     22   0     5    N
	Public nModulec_ex As Integer ' NUMBER     22   0     5    N
	Public nCover_ex As Integer ' NUMBER     22   0     5    N
	Public nRole_ex As Integer ' NUMBER     22   0     5    N
	Public nModulec_ad As Integer ' NUMBER     22   0     5    N
	Public nCover_ad As Integer ' NUMBER     22   0     5    N
	Public nRole_ad As Integer ' NUMBER     22   0     5    N
	Public nType_comm As Integer ' NUMBER     22   0     5    N
	Public dEffecdate As Date ' DATE        0   0     0    N
	Public nUsercode As Integer ' NUMBER     22   0     5    N
	
	'- Variables auxiliares
	Public sDescModulec_ex As String
	Public sDescCover_ex As String
	Public sDescRole_ex As String
	Public sDescModulec_ad As String
	Public sDescCover_ad As String
	Public sDescRole_ad As String
	Public sDescType_comm As String
	
	'%InsUpdComm_mod: Se encarga de actualizar la tabla Comm_mod
	Private Function InsUpdComm_mod(ByVal nAction As Integer) As Boolean
		Dim lrecInsUpdComm_mod As eRemoteDB.Execute
		
		On Error GoTo InsUpdComm_mod_Err
		
		lrecInsUpdComm_mod = New eRemoteDB.Execute
		
		'+ Definición de store procedure InsUpdComm_mod al 21-08-2003
		With lrecInsUpdComm_mod
			.StoredProcedure = "InsUpdComm_mod"
			.Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nModulec_ex", nModulec_ex, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCover_ex", nCover_ex, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRole_ex", nRole_ex, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nModulec_ad", nModulec_ad, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCover_ad", nCover_ad, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRole_ad", nRole_ad, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nType_comm", nType_comm, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			InsUpdComm_mod = .Run(False)
		End With
		
InsUpdComm_mod_Err: 
		If Err.Number Then
			InsUpdComm_mod = False
		End If
		'UPGRADE_NOTE: Object lrecInsUpdComm_mod may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecInsUpdComm_mod = Nothing
		On Error GoTo 0
	End Function
	
	'%Add: Crea un registro en la tabla
	Public Function Add() As Boolean
		Add = InsUpdComm_mod(1)
	End Function
	
	'%Update: Actualiza un registro en la tabla
	Public Function Update() As Boolean
		Update = InsUpdComm_mod(2)
	End Function
	
	'%Delete: Borra un registro en la tabla
	Public Function Delete() As Boolean
		Delete = InsUpdComm_mod(3)
	End Function
	
	'%InsValExistComm_mod: Valida que no exista el registro
	Public Function InsValExistComm_mod(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nModulec_ex As Integer, ByVal nCover_ex As Integer, ByVal nRole_ex As Integer, ByVal nModulec_ad As Integer, ByVal nCover_ad As Integer, ByVal nRole_ad As Integer, ByVal dEffecdate As Date) As Boolean
		Dim lrecreacom_mod As eRemoteDB.Execute
		
		On Error GoTo reacom_mod_Err
		
		lrecreacom_mod = New eRemoteDB.Execute
		
		'+
		'+ Definición de store procedure reaAdd_risk al 04-25-2002 16:02:43
		'+
		With lrecreacom_mod
			.StoredProcedure = "InsValExistComm_mod"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nModulec_ex", nModulec_ex, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCover_ex", nCover_ex, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRole_ex", nRole_ex, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nModulec_ad", nModulec_ad, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCover_ad", nCover_ad, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRole_ad", nRole_ad, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nExist", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(False) Then
				InsValExistComm_mod = .Parameters("nExist").Value = 1
			Else
				InsValExistComm_mod = False
			End If
		End With
		
reacom_mod_Err: 
		If Err.Number Then
			InsValExistComm_mod = False
		End If
		'UPGRADE_NOTE: Object lrecreacom_mod may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreacom_mod = Nothing
		On Error GoTo 0
	End Function
	
	
	'%InsPostDP828_Win: Actualizan la carpeta de contenido en la secuencia
	Public Function InsPostDP828_Win(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, ByVal nUsercode As Integer) As Boolean
		Dim lclsProd_win As eProduct.Prod_win
		Dim lstrContent As String
		
		On Error GoTo InsPostDP828_Win_Err
		lclsProd_win = New eProduct.Prod_win
		
		If InsValExistComm_mod(nBranch, nProduct, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, dEffecdate) Then
			lstrContent = "2"
		Else
			lstrContent = "1"
		End If
		
		InsPostDP828_Win = lclsProd_win.Add_Prod_win(nBranch, nProduct, dEffecdate, "DP828", lstrContent, nUsercode)
		
InsPostDP828_Win_Err: 
		If Err.Number Then
			InsPostDP828_Win = False
		End If
		'UPGRADE_NOTE: Object lclsProd_win may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsProd_win = Nothing
	End Function
	
	'%InsPostDP828: Ejecuta el post de la transacción
	'%              Tabla de Condiciones para el cálculo de comisión en endosos (DP828)
	Public Function InsPostDP828(ByVal sAction As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nModulec_ex As Integer, ByVal nCover_ex As Integer, ByVal nRole_ex As Integer, ByVal nModulec_ad As Integer, ByVal nCover_ad As Integer, ByVal nRole_ad As Integer, ByVal nType_comm As Integer, ByVal dEffecdate As Date, ByVal nUsercode As Integer) As Boolean
		
		On Error GoTo InsPostDP828_Err
		
		With Me
			.nBranch = nBranch
			.nProduct = nProduct
			.nModulec_ex = nModulec_ex
			.nCover_ex = nCover_ex
			.nRole_ex = nRole_ex
			.nModulec_ad = nModulec_ad
			.nCover_ad = nCover_ad
			.nRole_ad = nRole_ad
			.nType_comm = nType_comm
			.dEffecdate = dEffecdate
			.nUsercode = nUsercode
		End With
		
		Select Case sAction
			Case "Add"
				InsPostDP828 = Add
			Case "Update"
				InsPostDP828 = Update
			Case "Del"
				InsPostDP828 = Delete
		End Select
		
InsPostDP828_Err: 
		If Err.Number Then
			InsPostDP828 = False
		End If
		On Error GoTo 0
	End Function
	
	'%InsValDP828: Ejecuta las validaciones de la transacción
	'%             Tabla de Condiciones para el cálculo de comisión en endosos (DP828)
	Public Function InsValDP828(ByVal sAction As String, ByVal sCodispl As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nModulec_ex As Integer, ByVal nCover_ex As Integer, ByVal nRole_ex As Integer, ByVal nModulec_ad As Integer, ByVal nCover_ad As Integer, ByVal nRole_ad As Integer, ByVal nType_comm As Integer, ByVal dEffecdate As Date) As String
		Dim lclsErrors As eFunctions.Errors
		Dim lclsProduct As Product
		
		On Error GoTo InsValDP828_Err
		
		lclsErrors = New eFunctions.Errors
		lclsProduct = New eProduct.Product
		If lclsProduct.IsModule(nBranch, nProduct, dEffecdate) Then
			If nModulec_ex = eRemoteDB.Constants.intNull Or nModulec_ex = 0 Then
				Call lclsErrors.ErrorMessage(sCodispl, 55537,  , eFunctions.Errors.TextAlign.LeftAling, "Módulo cob. incluida ")
			End If
			
			If nModulec_ad = eRemoteDB.Constants.intNull Or nModulec_ad = 0 Then
				Call lclsErrors.ErrorMessage(sCodispl, 55537,  , eFunctions.Errors.TextAlign.LeftAling, "Módulo cob. excluida ")
			End If
		End If
		
		If sAction = "Add" Then
			If nType_comm = eRemoteDB.Constants.intNull Or nType_comm = 0 Then
				Call lclsErrors.ErrorMessage(sCodispl, 55537,  , eFunctions.Errors.TextAlign.LeftAling, "Forma de cálculo de comisión ")
			End If
			
			If nCover_ex = eRemoteDB.Constants.intNull Or nCover_ex = 0 Then
				Call lclsErrors.ErrorMessage(sCodispl, 55537,  , eFunctions.Errors.TextAlign.LeftAling, "Cobertura excluida ")
			End If
			
			If nCover_ad = eRemoteDB.Constants.intNull Or nCover_ad = 0 Then
				Call lclsErrors.ErrorMessage(sCodispl, 55537,  , eFunctions.Errors.TextAlign.LeftAling, "Cobertura incluida ")
			End If
			
			If nRole_ex = eRemoteDB.Constants.intNull Or nRole_ex = 0 Then
				Call lclsErrors.ErrorMessage(sCodispl, 55537,  , eFunctions.Errors.TextAlign.LeftAling, "Rol de la cobertura excluida ")
			End If
			
			If nRole_ad = eRemoteDB.Constants.intNull Or nRole_ad = 0 Then
				Call lclsErrors.ErrorMessage(sCodispl, 55537,  , eFunctions.Errors.TextAlign.LeftAling, "Rol de la cobertura incluida ")
			End If
			If InsValExistComm_mod(nBranch, nProduct, nModulec_ex, nCover_ex, nRole_ex, nModulec_ad, nCover_ad, nRole_ad, dEffecdate) Then
				Call lclsErrors.ErrorMessage(sCodispl, 10284)
			End If
		End If
		InsValDP828 = lclsErrors.Confirm
		
InsValDP828_Err: 
		If Err.Number Then
			InsValDP828 = CStr(False)
		End If
		On Error GoTo 0
	End Function
	
	
	'%Class_Initialize: Inicializa las propiedades cuando se instancia la clase
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		nBranch = eRemoteDB.Constants.intNull
		nProduct = eRemoteDB.Constants.intNull
		nModulec_ex = eRemoteDB.Constants.intNull
		nCover_ex = eRemoteDB.Constants.intNull
		nRole_ex = eRemoteDB.Constants.intNull
		nModulec_ad = eRemoteDB.Constants.intNull
		nCover_ad = eRemoteDB.Constants.intNull
		nRole_ad = eRemoteDB.Constants.intNull
		nType_comm = eRemoteDB.Constants.intNull
		dEffecdate = eRemoteDB.Constants.dtmNull
		nUsercode = eRemoteDB.Constants.intNull
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
End Class






