Option Strict Off
Option Explicit On
Public Class Change_mod
	'%-------------------------------------------------------%'
	'% $Workfile:: Change_mod.cls                           $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:06p                                $%'
	'% $Revision:: 12                                       $%'
	'%-------------------------------------------------------%'
	
	'+
	'+ Estructura de tabla insudb.change_mod al 06-24-2002 17:12:31
	'+     Property                Type         DBType   Size Scale  Prec  Null
	'+-------------------------------------------------------------------------
	Public nBranch As Integer ' NUMBER     22   0     5    N
	Public nProduct As Integer ' NUMBER     22   0     5    N
	Public nModul_ori As Integer ' NUMBER     22   0     5    N
	Public dEffecdate As Date ' DATE       7    0     0    N
	Public nModul_end As Integer ' NUMBER     22   0     5    N
	Public dNulldate As Date ' DATE       7    0     0    S
	Public dCompdate As Date ' DATE       7    0     0    N
	Public nUsercode As Integer ' NUMBER     22   0     5    N
	Public sIdemcap As String ' CHAR       1    0     0    N
	Public sIdemprem As String ' CHAR       1    0     0    N
	Public sIdemdeduc As String ' CHAR       1    0     0    N
	
	'%InsUpdChange_mod: Se encarga de actualizar la tabla Change_mod
	Private Function InsUpdChange_mod(ByVal nAction As Integer) As Boolean
		Dim lrecinsUpdchange_mod As eRemoteDB.Execute
		On Error GoTo insUpdchange_mod_Err
		
		lrecinsUpdchange_mod = New eRemoteDB.Execute
		
		'+
		'+ Definición de store procedure insUpdchange_mod al 06-24-2002 18:30:40
		'+
		With lrecinsUpdchange_mod
			.StoredProcedure = "insUpdchange_mod"
			.Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nModul_ori", nModul_ori, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nModul_end", nModul_end, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sIdemcap", sIdemcap, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sIdemprem", sIdemprem, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sIdemdeduc", sIdemdeduc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			InsUpdChange_mod = .Run(False)
		End With
		
insUpdchange_mod_Err: 
		If Err.Number Then
			InsUpdChange_mod = False
		End If
		'UPGRADE_NOTE: Object lrecinsUpdchange_mod may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsUpdchange_mod = Nothing
		On Error GoTo 0
	End Function
	
	'%Add: Crea un registro en la tabla
	Public Function Add() As Boolean
		Add = InsUpdChange_mod(1)
	End Function
	
	'%Update: Actualiza un registro en la tabla
	Public Function Update() As Boolean
		Update = InsUpdChange_mod(2)
	End Function
	
	'%Delete: Borra un registro en la tabla
	Public Function Delete() As Boolean
		Delete = InsUpdChange_mod(3)
	End Function
	
	'%InsValEffecdate: Valida la fecha de efecto de la transacción
	Public Function InsValEffecdate(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date) As Boolean
        Dim dEffecdateout As String = ""
        Dim lrecinsValeffecdate_change_mod As eRemoteDB.Execute
		On Error GoTo insValeffecdate_change_mod_Err
		
		lrecinsValeffecdate_change_mod = New eRemoteDB.Execute
		
		'+
		'+ Definición de store procedure insValeffecdate_change_mod al 06-25-2002 13:34:37
		'+
		With lrecinsValeffecdate_change_mod
			.StoredProcedure = "insValeffecdate_change_mod"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdateout", dEffecdateout, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			.Run(False)
			If .Parameters("dEffecdateout").Value = String.Empty Then
				InsValEffecdate = True
			Else
				InsValEffecdate = False
			End If
		End With
		
insValeffecdate_change_mod_Err: 
		If Err.Number Then
			InsValEffecdate = False
		End If
		'UPGRADE_NOTE: Object lrecinsValeffecdate_change_mod may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsValeffecdate_change_mod = Nothing
		On Error GoTo 0
	End Function
	
	'End Function
	
	'%InsValMCA814_K: Validaciones de la transacción(Header)
	Public Function InsValMCA814_K(ByVal sCodispl As String, ByVal nAction As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date) As String
		Dim lclsErrors As eFunctions.Errors
		
		On Error GoTo InsValMCA814_K_Err
		lclsErrors = New eFunctions.Errors
		
		With lclsErrors
			'+ Se valida el Campo Fecha
			If dEffecdate = eRemoteDB.Constants.dtmNull Then
				.ErrorMessage(sCodispl, 4003)
			Else
				If nAction = eFunctions.Menues.TypeActions.clngActionUpdate Or nAction = eFunctions.Menues.TypeActions.clngActionadd Then
					If Not InsValEffecdate(nBranch, nProduct, dEffecdate) Then
						.ErrorMessage(sCodispl, 55611)
					End If
				End If
			End If
			
			'+ se valida el campo ramo
			If nBranch = eRemoteDB.Constants.intNull Then
				.ErrorMessage(sCodispl, 1022)
			End If
			
			'+ se valida el campo producto
			If nProduct = eRemoteDB.Constants.intNull Then
				.ErrorMessage(sCodispl, 1014)
			End If
			InsValMCA814_K = .Confirm
		End With
		
InsValMCA814_K_Err: 
		If Err.Number Then
			InsValMCA814_K = "InsValMCA814_K: " & Err.Description
		End If
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		On Error GoTo 0
	End Function
	
	'%InsValMCA814: Validaciones de la transacción(Folder)
	'%              Tabla de control de prima mínima(MCA814)
	Public Function InsValMCA814(ByVal sCodispl As String, ByVal sAction As String, ByVal nModul_ori As Integer, ByVal nModul_end As Integer) As String
		Dim lclsErrors As eFunctions.Errors
		
		On Error GoTo InsValMCA814_Err
		lclsErrors = New eFunctions.Errors
		
		With lclsErrors
			If nModul_ori = eRemoteDB.Constants.intNull Then
				.ErrorMessage(sCodispl, 55812)
			End If
			
			If nModul_end = eRemoteDB.Constants.intNull Then
				.ErrorMessage(sCodispl, 55813)
			End If
			
			If nModul_end = nModul_ori Then
				.ErrorMessage(sCodispl, 55814)
			End If
			
			InsValMCA814 = .Confirm
		End With
		
InsValMCA814_Err: 
		If Err.Number Then
			InsValMCA814 = "InsValMCA814: " & Err.Description
		End If
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		On Error GoTo 0
	End Function
	
	'%InsPostMCA814: Ejecuta el post de la transacción
	'%               Tabla de control de prima mínima(MCA814)
	Public Function InsPostMCA814(ByVal sAction As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nModul_ori As Integer, ByVal dEffecdate As Date, ByVal nModul_end As Integer, ByVal nUsercode As Integer, ByVal sIdemcap As String, ByVal sIdemprem As String, ByVal sIdemdeduc As String) As Boolean
		
		On Error GoTo InsPostMCA814_Err
		
		With Me
			.nBranch = nBranch
			.nProduct = nProduct
			.nModul_ori = nModul_ori
			.dEffecdate = dEffecdate
			.nModul_end = nModul_end
			.nUsercode = nUsercode
			.sIdemcap = sIdemcap
			.sIdemprem = sIdemprem
			.sIdemdeduc = sIdemdeduc
		End With
		
		Select Case sAction
			Case "Add"
				InsPostMCA814 = Add
			Case "Update"
				InsPostMCA814 = Update
			Case "Del"
				InsPostMCA814 = Delete
		End Select
		
InsPostMCA814_Err: 
		If Err.Number Then
			InsPostMCA814 = False
		End If
		On Error GoTo 0
	End Function
	
	'%Class_Initialize: Inicializa las propiedades cuando se instancia la clase
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		nBranch = eRemoteDB.Constants.intNull
		nProduct = eRemoteDB.Constants.intNull
		nModul_ori = eRemoteDB.Constants.intNull
		nModul_end = eRemoteDB.Constants.intNull
		dNulldate = eRemoteDB.Constants.dtmNull
		dCompdate = eRemoteDB.Constants.dtmNull
		sIdemcap = String.Empty
		sIdemprem = String.Empty
		sIdemdeduc = String.Empty
		dEffecdate = eRemoteDB.Constants.dtmNull
		nUsercode = eRemoteDB.Constants.intNull
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
End Class






