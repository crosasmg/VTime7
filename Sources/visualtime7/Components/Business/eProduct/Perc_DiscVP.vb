Option Strict Off
Option Explicit On
Public Class Perc_DiscVP
	
	'+
	'+ Estructura de tabla Perc_DiscVP al 10-09-2008 18:06:23
	'+     Property                Type         DBType   Size Scale  Prec  Null
	'+-------------------------------------------------------------------------
	Public nBranch As Integer ' NUMBER     22   0     5    N
	Public nProduct As Integer ' NUMBER     22   0     5    N
	Public dEffecdate As Date ' DATE       7    0     0    N
	Public nVp_ini As Double ' NUMBER     22   0     5    N
	Public nVp_end As Double ' NUMBER     22   0     5    N
	Public nDisc_perc_vp As Double ' NUMBER     22   6     9    S
	Public dNulldate As Date ' DATE       7    0     0    S
	Public nusercode As Integer ' NUMBER     22   0     5    N
	
	'%InsUpdPerc_DiscVP: Se encarga de actualizar la tabla Perc_DiscVP
	Private Function InsUpdPerc_DiscVP(ByVal nAction As Short) As Boolean
		Dim lrecinsUpdPerc_DiscVP As eRemoteDB.Execute
		On Error GoTo insUpdPerc_DiscVP_Err
		lrecinsUpdPerc_DiscVP = New eRemoteDB.Execute
		
		'+
		'+ Definición de store procedure insUpdPerc_DiscVP al 09-26-2008 18:31:56
		'+
		With lrecinsUpdPerc_DiscVP
			.StoredProcedure = "InsPerc_DiscVPpkg.InsUpdPerc_DiscVP"
			.Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 38, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nVp_ini", nVp_ini, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nVp_end", nVp_end, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDisc_perc_vp", nDisc_perc_vp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 9, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nusercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			InsUpdPerc_DiscVP = .Run(False)
		End With
		
insUpdPerc_DiscVP_Err: 
		If Err.Number Then
			InsUpdPerc_DiscVP = False
		End If
		'UPGRADE_NOTE: Object lrecinsUpdPerc_DiscVP may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsUpdPerc_DiscVP = Nothing
		On Error GoTo 0
	End Function
	
	'%Add: Crea un registro en la tabla
	Public Function Add() As Boolean
		Add = InsUpdPerc_DiscVP(1)
	End Function
	
	'%Update: Actualiza un registro en la tabla
	Public Function Update() As Boolean
		Update = InsUpdPerc_DiscVP(2)
	End Function
	
	'%Delete: Borra un registro en la tabla
	Public Function Delete() As Boolean
		Delete = InsUpdPerc_DiscVP(3)
	End Function
	
	'%Find: Lee los datos de la tabla
	Public Function Find(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, ByVal nVp_ini As Double, ByVal nVp_end As Double, Optional ByVal lblnFind As Boolean = False) As Boolean
		Dim lrecPerc_DiscVPo As eRemoteDB.Execute
		
		On Error GoTo Find_Err
		lrecPerc_DiscVPo = New eRemoteDB.Execute
		'+
		'+ Definición de store procedure Perc_DiscVPo al 09-26-2008 18:48:08
		'+
		With lrecPerc_DiscVPo
			.StoredProcedure = "InsPerc_DiscVPpkg.ReaPerc_DiscVP_o"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nVp_ini", nVp_ini, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nVp_end", nVp_end, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(True) Then
				Find = True
				Me.nBranch = .FieldToClass("nBranch")
				Me.nProduct = .FieldToClass("nProduct")
				Me.dEffecdate = .FieldToClass("dEffecdate")
				Me.dNulldate = .FieldToClass("dNulldate")
				Me.nVp_ini = .FieldToClass("nVp_ini")
				Me.nVp_end = .FieldToClass("nVp_end")
				Me.nDisc_perc_vp = .FieldToClass("nDisc_perc_vp")
			End If
		End With
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		'UPGRADE_NOTE: Object lrecPerc_DiscVPo may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecPerc_DiscVPo = Nothing
		On Error GoTo 0
	End Function
	
	'%InsValEffecdate: Valida la fecha de efecto de la transacción
	Public Function insValEffecdate(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date) As Boolean
		Dim lrecvalDeffecdate As eRemoteDB.Execute
		Dim ldMaxEffecdate As Date
		On Error GoTo insValEffecdate_err
		
		lrecvalDeffecdate = New eRemoteDB.Execute
		'+
		'+ Definición de store procedure valDeffecdate al 09-26-2008 19:52:32
		'+
		With lrecvalDeffecdate
			.StoredProcedure = "InsPerc_DiscVPpkg.valdEffecdate"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", ldMaxEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run() Then
				ldMaxEffecdate = .FieldToClass("dEffecdate")
				insValEffecdate = ldMaxEffecdate = eRemoteDB.Constants.dtmNull Or ldMaxEffecdate <= dEffecdate
				Me.dEffecdate = ldMaxEffecdate
			End If
		End With
		
insValEffecdate_err: 
		If Err.Number Then
			insValEffecdate = False
		End If
		'UPGRADE_NOTE: Object lrecvalDeffecdate may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecvalDeffecdate = Nothing
		On Error GoTo 0
		
	End Function
	
	'%InsValMVI8015_K: Validaciones de la transacción(Header)
	Public Function InsValMVI8015_K(ByVal sCodispl As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date) As String
		Dim lclsErrors As eFunctions.Errors
		Dim lblnValDate As Boolean
		
		On Error GoTo InsValMVI8015_K_Err
		lclsErrors = New eFunctions.Errors
		lblnValDate = True
		
		With lclsErrors
			'+ Se valida el Campo Ramo
			If nBranch = eRemoteDB.Constants.intNull Then
				.ErrorMessage(sCodispl, 1022)
				lblnValDate = False
			End If
			
			'+ Se valida el Campo Producto
			If nProduct = eRemoteDB.Constants.intNull Then
				.ErrorMessage(sCodispl, 1014)
				lblnValDate = False
			End If
			
			'+ Se valida el Campo Fecha
			If dEffecdate = eRemoteDB.Constants.dtmNull Then
				.ErrorMessage(sCodispl, 2056)
			Else
				If lblnValDate Then
					If Not insValEffecdate(nBranch, nProduct, dEffecdate) Then
						.ErrorMessage(sCodispl, 1943,  , eFunctions.Errors.TextAlign.RigthAling, "(" & Me.dEffecdate & ")")
					End If
				End If
			End If
			
			InsValMVI8015_K = .Confirm
		End With
		
InsValMVI8015_K_Err: 
		If Err.Number Then
			InsValMVI8015_K = "InsValMVI8015_K: " & Err.Description
		End If
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		On Error GoTo 0
	End Function
	
	'%InsValMVI8015: Validaciones de la transacción(Folder)
	Public Function InsValMVI8015(ByVal sCodispl As String, ByVal sAction As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, ByVal nVp_ini As Double, ByVal nVp_end As Double, ByVal nDisc_perc_vp As Double) As String
		Dim lclsErrors As eFunctions.Errors
		Dim lblnFind As Boolean
		On Error GoTo InsValMVI8015_Err
		lclsErrors = New eFunctions.Errors
		
		With lclsErrors
			lblnFind = True
			'+Valor inicial debe estar lleno
			If nVp_ini = eRemoteDB.Constants.intNull Then
				.ErrorMessage(sCodispl, 80075)
				lblnFind = False
			End If
			
			'+Valor final debe estar lleno
			If nVp_end = eRemoteDB.Constants.intNull Then
				.ErrorMessage(sCodispl, 80076)
				lblnFind = False
			Else
				'+Valor final debe ser mayor o igual a valor inicial
				If nVp_ini <> eRemoteDB.Constants.intNull And nVp_end < nVp_ini Then
					.ErrorMessage(sCodispl, 80079)
					lblnFind = False
				End If
			End If
			
			'+Validaciones gasto porcentual
			If nDisc_perc_vp = eRemoteDB.Constants.intNull Then
				.ErrorMessage(sCodispl, 80077)
			Else
				If nDisc_perc_vp >= 100 Then
					.ErrorMessage(sCodispl, 80078)
				End If
			End If
			
			'+Validar que no se dupliquen registros
			If sAction = "Add" And lblnFind Then
				If Find(nBranch, nProduct, dEffecdate, nVp_ini, nVp_end) Then
					.ErrorMessage(sCodispl, 10284)
				End If
			End If
			
			InsValMVI8015 = .Confirm
		End With
		
InsValMVI8015_Err: 
		If Err.Number Then
			InsValMVI8015 = "InsValMVI8015: " & Err.Description
		End If
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		On Error GoTo 0
	End Function
	
	'%InsPostMVI8015: Ejecuta el post de la transacción
	Public Function InsPostMVI8015(ByVal sAction As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, ByVal nVp_ini As Double, ByVal nVp_end As Double, ByVal nusercode As Integer, Optional ByVal nDisc_perc_vp As Double = 0) As Boolean
		
		On Error GoTo InsPostMVI8015_Err
		
		With Me
			.nBranch = nBranch
			.nProduct = nProduct
			.dEffecdate = dEffecdate
			.nVp_ini = nVp_ini
			.nVp_end = nVp_end
			.nusercode = nusercode
			.nDisc_perc_vp = nDisc_perc_vp
		End With
		
		Select Case sAction
			Case "Add"
				InsPostMVI8015 = Add
			Case "Update"
				InsPostMVI8015 = Update
			Case "Del"
				InsPostMVI8015 = Delete
		End Select
		
InsPostMVI8015_Err: 
		If Err.Number Then
			InsPostMVI8015 = False
		End If
		On Error GoTo 0
	End Function
	
	'%Class_Initialize: Inicializa las propiedades cuando se instancia la clase
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		nBranch = eRemoteDB.Constants.intNull
		nProduct = eRemoteDB.Constants.intNull
		dEffecdate = eRemoteDB.Constants.dtmNull
		nusercode = eRemoteDB.Constants.intNull
		nVp_ini = eRemoteDB.Constants.intNull
		nVp_end = eRemoteDB.Constants.intNull
		nDisc_perc_vp = eRemoteDB.Constants.intNull
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
End Class






