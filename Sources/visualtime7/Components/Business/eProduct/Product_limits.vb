Option Strict Off
Option Explicit On
Public Class Product_limits
	'%-------------------------------------------------------%'
	'% $Workfile:: Product_limits.cls                       $%'
	'% $Author:: Fmendoza                                   $%'
	'% $Date:: 29/06/06 5:34p                               $%'
	'% $Revision:: 3                                        $%'
	'%-------------------------------------------------------%'
	
	'+
	'+ Estructura de tabla Product_Limits al 06-27-2006 16:53:14
	'+     Property                Type         DBType   Size Scale  Prec  Null
	'+-------------------------------------------------------------------------
	Public nBranch As Integer ' NUMBER     22   0     5    N
	Public nProduct As Integer ' NUMBER     22   0     5    N
	Public nLimit_type As Integer ' NUMBER     22   0     5    N
	Public nLimit_code As Integer ' NUMBER     22   0     5    N
	Public nValmax As Double ' NUMBER     22   6     18   S
	Public nValmin As Double ' NUMBER     22   6     18   S
	Public dCompdate As Date ' DATE       7    0     0    S
	Public nUsercode As Integer ' NUMBER     22   0     5    S
	Public dEffecdate As Date
	
	
	'%InsUpdProduct_limits: Se encarga de actualizar la tabla Product_limits
	Private Function InsUpdProduct_limits(ByVal nAction As Short) As Boolean
		Dim lrecins_row As eRemoteDB.Execute
		On Error GoTo ins_row_Err
		
		lrecins_row = New eRemoteDB.Execute
		
		'+
		'+ Definición de store procedure ins_row al 06-27-2006 16:58:47
		'+
		With lrecins_row
			.StoredProcedure = "Product_limits_sqlpkg.ins_row"
			.Parameters.Add("p_naction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 38, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("p_nbranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("p_nproduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("p_nlimit_type", nLimit_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("p_nlimit_code", nLimit_code, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("p_nvalmax", nValmax, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("p_nvalmin", nValmin, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("p_nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			InsUpdProduct_limits = .Run(False)
		End With
		
ins_row_Err: 
		If Err.Number Then
			InsUpdProduct_limits = False
		End If
		'UPGRADE_NOTE: Object lrecins_row may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecins_row = Nothing
		On Error GoTo 0
		
	End Function
	
	'%Add: Crea un registro en la tabla
	Public Function Add() As Boolean
		Add = InsUpdProduct_limits(1)
	End Function
	
	'%Update: Actualiza un registro en la tabla
	Public Function Update() As Boolean
		Update = InsUpdProduct_limits(2)
	End Function
	
	'%Delete: Borra un registro en la tabla
	Public Function Delete() As Boolean
		Delete = InsUpdProduct_limits(3)
	End Function
	
	'%Find: Lee los datos de la tabla
	Public Function Find(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nLimit_type As Integer, ByVal nLimit_code As Integer, ByVal dEffecdate As Date) As Boolean
		Dim lrecins_row As eRemoteDB.Execute
		On Error GoTo ins_row_Err
		
		lrecins_row = New eRemoteDB.Execute
		
		'+
		'+ Definición de store procedure ins_row al 06-27-2006 16:58:47
		'+
		With lrecins_row
			.StoredProcedure = "Product_limits_sqlpkg.REA_CUR_PK"
			.Parameters.Add("p_nbranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("p_nproduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("p_nlimit_type", nLimit_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("p_nlimit_code", nLimit_code, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			Find = .Run()
		End With
		
ins_row_Err: 
		If Err.Number Then
			Find = False
		End If
		'UPGRADE_NOTE: Object lrecins_row may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecins_row = Nothing
		On Error GoTo 0
		
	End Function
	
	'%InsValDP066_K: Validaciones de la transacción(Header)
	Public Function InsValDP066_K(ByVal nLimit_type As Integer, ByVal nLimit_code As Integer, ByVal nValmax As Double, ByVal nValmin As Double, ByVal sAction As String, ByVal dEffecdate As Date, ByVal nBranch As Integer, ByVal nProduct As Integer) As String
		Dim lclsErrors As eFunctions.Errors
		Dim sCodispl As Object
		
		sCodispl = "DP066"
		
		On Error GoTo InsValDP066_K_Err
		lclsErrors = New eFunctions.Errors
		
		With lclsErrors
			If nLimit_type = 0 Or nLimit_type = eRemoteDB.Constants.intNull Then
				.ErrorMessage(sCodispl, 80060)
			End If
			
			If (nLimit_code = 0 Or nLimit_code = eRemoteDB.Constants.intNull) And nLimit_type = 1 Then
				.ErrorMessage(sCodispl, 80056)
			End If
			
			If nValmin = 0 Or nValmin = eRemoteDB.Constants.intNull Then
				.ErrorMessage(sCodispl, 80058)
			End If
			If nValmax = 0 Or nValmax = eRemoteDB.Constants.intNull Then
				.ErrorMessage(sCodispl, 80057)
			End If
			If nValmax < nValmin Then
				.ErrorMessage(sCodispl, 80059)
			End If
			
			'+Validar que no se dupliquen registros
			If sAction = "Add" Then
				If Find(nBranch, nProduct, nLimit_type, nLimit_code, dEffecdate) Then
					.ErrorMessage(sCodispl, 55858)
				End If
			End If
			
			InsValDP066_K = .Confirm
		End With
		
InsValDP066_K_Err: 
		If Err.Number Then
			InsValDP066_K = "InsValDP066_K: " & Err.Description
		End If
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		On Error GoTo 0
	End Function
	
	
	'%InsPostDP066: Ejecuta el post de la transacción
	'%               Tabla de control de prima mínima(DP066)
	Public Function InsPostDP066(ByVal sAction As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nLimit_type As Integer, ByVal nLimit_code As Integer, ByVal nValmax As Double, ByVal nValmin As Double, ByVal dEffecdate As Date, ByVal nUsercode As Integer) As Boolean
		
		On Error GoTo InsPostDP066_Err
		Dim lcolProduct_limitss As Product_limitss
		Dim lclsProd_win As Prod_win
		
		
		With Me
			.nBranch = nBranch
			.nProduct = nProduct
			.nLimit_type = nLimit_type
			.nLimit_code = nLimit_code
			.nValmax = nValmax
			.nValmin = nValmin
			.nUsercode = nUsercode
			.dEffecdate = dEffecdate
		End With
		
		Select Case sAction
			Case "Add"
				InsPostDP066 = Add
			Case "Update"
				InsPostDP066 = Update
			Case "Del"
				InsPostDP066 = Delete
		End Select
		
		If InsPostDP066 Then
			lcolProduct_limitss = New Product_limitss
			lclsProd_win = New Prod_win
			If lcolProduct_limitss.Find(nBranch, nProduct, dEffecdate) Then
				Call lclsProd_win.Add_Prod_win(nBranch, nProduct, dEffecdate, "DP066", "2", nUsercode)
			Else
				Call lclsProd_win.Add_Prod_win(nBranch, nProduct, dEffecdate, "DP066", "1", nUsercode)
			End If
			'UPGRADE_NOTE: Object lcolProduct_limitss may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			lcolProduct_limitss = Nothing
			'UPGRADE_NOTE: Object lclsProd_win may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			lclsProd_win = Nothing
		End If
		
		
InsPostDP066_Err: 
		If Err.Number Then
			InsPostDP066 = False
		End If
		On Error GoTo 0
	End Function
	
	'%Class_Initialize: Inicializa las propiedades cuando se instancia la clase
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		nBranch = eRemoteDB.Constants.intNull
		nProduct = eRemoteDB.Constants.intNull
		nLimit_type = eRemoteDB.Constants.intNull
		nLimit_code = eRemoteDB.Constants.intNull
		nValmax = eRemoteDB.Constants.intNull
		nValmin = eRemoteDB.Constants.intNull
		dCompdate = eRemoteDB.Constants.dtmNull
		nUsercode = eRemoteDB.Constants.intNull
		dEffecdate = eRemoteDB.Constants.dtmNull
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
End Class






