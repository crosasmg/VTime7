Option Strict Off
Option Explicit On
Public Class Prod_addcap
	'%-------------------------------------------------------%'
	'% $Workfile:: Prod_addcap.cls                          $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 12:36p                               $%'
	'% $Revision:: 20                                       $%'
	'%-------------------------------------------------------%'
	
	'+ Estructura de tabla prod_addcap al 22-08-2002
	'+     Property                Type         DBType   Size Scale  Prec  Null
	'+-------------------------------------------------------------------------
	Public nBranch As Integer ' NUMBER     22   0     5    N
	Public nProduct As Integer ' NUMBER     22   0     5    N
	Public nCovergen As Integer ' NUMBER     22   0     5    N
	Public nBranchadd As Integer ' NUMBER     22   0     5    N
	Public sBranchadd As String
	Public nProductadd As Integer ' NUMBER     22   0     5    N
	Public sProductadd As String	
	Public dEffecdate As Date ' DATE       7    0     0    N
	Public nId As Integer ' NUMBER     22   0     5    N
	Public nRole As Integer ' NUMBER     22   0     5    Y
	Public nCapital As Double ' NUMBER     22   6    18    Y
	Public nCoveradd As Integer ' NUMBER     22   0     5    Y
	Public sCoveradd As String
	Public nRoleadd As Integer ' NUMBER     22   0     5    Y
	Public sRoleadd As String
	Public nClusteradd As Integer
	Public nCapitalAdd As Integer
    Public nInverse As Integer ' NUMBER     22   0     5    N
	Private mlngUsercode As Integer ' NUMBER     22   0     5    N
	

	Public nTyp_cumul As Integer
	
	Public sBranch As String
	Public sProduct As String
	Public sCover As String
	Public sRole As String
	
	'% InsUpdProd_AddCap: Realiza la actualización de la tabla
	Private Function InsUpdProd_AddCap(ByVal nAction As Integer) As Boolean
		Dim lrecInsUpdProd_AddCap As eRemoteDB.Execute
		
		On Error GoTo InsUpdProd_AddCap_Err
		
		lrecInsUpdProd_AddCap = New eRemoteDB.Execute
		
		'+Definición de parámetros para stored procedure 'InsUpdProd_AddCap'
		'+Información leída el 23/08/2002
		With lrecInsUpdProd_AddCap
			.StoredProcedure = "InsUpdProd_AddCap"
			.Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCovergen", nCovergen, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranchadd", nBranchadd, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProductadd", nProductadd, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nId", nId, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRole", nRole, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCapital", nCapital, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCoveradd", nCoveradd, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRoleadd", nRoleadd, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", mlngUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTyp_cumul", nTyp_cumul, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			InsUpdProd_AddCap = .Run(False)
		End With
		
InsUpdProd_AddCap_Err: 
		If Err.Number Then
			InsUpdProd_AddCap = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecInsUpdProd_AddCap may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecInsUpdProd_AddCap = Nothing
	End Function
	
	'% Add: Crea un registro en la tabla
	Public Function Add() As Boolean
		Add = InsUpdProd_AddCap(1)
	End Function
	
	'% Delete: Borra los datos de la tabla
	Public Function Delete() As Boolean
		Delete = InsUpdProd_AddCap(3)
	End Function
	
	'% InsValDP770: Validaciones de la transacción
	Public Function InsValDP770(ByVal sAction As String, ByVal sCodispl As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nBranchadd As Integer, ByVal nProductadd As Integer, ByVal dEffecdate As Date, ByVal nCovergen As Integer, ByVal nCovergenadd As Integer, ByVal nRoleadd As Integer) As String
		Dim lclsErrors As eFunctions.Errors
		Dim lclsLife_cover As eProduct.Life_cover
		
		On Error GoTo InsValDP770_Err
		
		lclsErrors = New eFunctions.Errors
		With lclsErrors
			'+Se valida el campo Ramo
			If nBranchadd = eRemoteDB.Constants.intNull Then
				.ErrorMessage(sCodispl, 1022)
			End If
			
			'+Se valida el campo Producto
			If nProductadd = eRemoteDB.Constants.intNull Then
				.ErrorMessage(sCodispl, 1014)
			Else
				If nCovergenadd = eRemoteDB.Constants.intNull Then
					lclsLife_cover = New eProduct.Life_cover
					If lclsLife_cover.Count_a(nBranchadd, nProductadd, nCovergen, dEffecdate) = 0 Then
						.ErrorMessage(sCodispl, 55657)
					End If
				End If
			End If
			
			'+ Se valida registro único
			If sAction = "Add" Then
				If IsExist(nBranch, nProduct, dEffecdate, nCovergen, nBranchadd, nProductadd, nCovergenadd, nRoleadd) Then
					.ErrorMessage(sCodispl, 55658)
				End If
			End If
			
			InsValDP770 = .Confirm
		End With
		
InsValDP770_Err: 
		If Err.Number Then
			InsValDP770 = "InsValDP770: " & Err.Description
		End If
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		'UPGRADE_NOTE: Object lclsLife_cover may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsLife_cover = Nothing
		On Error GoTo 0
	End Function
	
    '	'%InsPostDP770: Actualizan los datos de la transacción
    '	Public Function InsPostDP770(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, ByVal nUsercode As Integer) As Boolean
    '		Dim lclsProd_win As eProduct.Prod_win
    '		Dim lstrContent As String

    '		On Error GoTo InsPostDP770_Err
    '		lclsProd_win = New eProduct.Prod_win

    '		If IsExist(nBranch, nProduct, dEffecdate) Then
    '			lstrContent = "2"
    '		Else
    '			lstrContent = "1"
    '		End If

    '		InsPostDP770 = lclsProd_win.Add_Prod_win(nBranch, nProduct, dEffecdate, "DP770", lstrContent, nUsercode)

    'InsPostDP770_Err: 
    '		If Err.Number Then
    '			InsPostDP770 = False
    '		End If
    '		'UPGRADE_NOTE: Object lclsProd_win may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
    '		lclsProd_win = Nothing
    '	End Function
	
	'% InsPostDP770Upd: Actualizan los datos de la transacción
	Public Function InsPostDP770Upd(ByVal sAction As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nCovergen As Integer, ByVal nBranchadd As Integer, ByVal nProductadd As Integer, ByVal dEffecdate As Date, ByVal nId As Integer, ByVal nRole As Integer, ByVal nCoveradd As Integer, ByVal nRoleadd As Integer, ByVal nCapital As Double, ByVal nUsercode As Integer, ByVal nTyp_cumul As Integer) As Boolean
		On Error GoTo InsPostDP770Upd_Err
		InsPostDP770Upd = True
		With Me
			.nBranch = nBranch
			.nProduct = nProduct
			.nBranchadd = nBranchadd
			.nProductadd = nProductadd
			.dEffecdate = dEffecdate
			.nCovergen = nCovergen
			.nId = nId
			.nRole = nRole
			.nCapital = nCapital
			.nCoveradd = nCoveradd
			.nRoleadd = nRoleadd
			.nTyp_cumul = nTyp_cumul
			mlngUsercode = nUsercode
			Select Case sAction
				Case "Add"
					InsPostDP770Upd = .Add
				Case "Del"
					InsPostDP770Upd = .Delete
			End Select
		End With
		
InsPostDP770Upd_Err: 
		If Err.Number Then
			InsPostDP770Upd = False
		End If
		On Error GoTo 0
	End Function
	
	'% IsExist: Verifica que no se duplique la llave
	Public Function IsExist(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, Optional ByVal nCovergen As Integer = eRemoteDB.Constants.intNull, Optional ByVal nBranchadd As Integer = eRemoteDB.Constants.intNull, Optional ByVal nProductadd As Integer = eRemoteDB.Constants.intNull, Optional ByVal nCoveradd As Integer = eRemoteDB.Constants.intNull, Optional ByVal nRoleadd As Integer = eRemoteDB.Constants.intNull) As Boolean
		Dim lrecReaProd_addcap As eRemoteDB.Execute
		
		On Error GoTo IsExist_Err
		IsExist = True
		lrecReaProd_addcap = New eRemoteDB.Execute
		With lrecReaProd_addcap
			.StoredProcedure = "ReaProd_addcap_v"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCovergen", nCovergen, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranchadd", nBranchadd, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProductadd", nProductadd, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCoveradd", nCoveradd, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRoleadd", nRoleadd, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nExists", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				IsExist = .Parameters("nExists").Value = 1
			End If
		End With
		
IsExist_Err: 
		If Err.Number Then
			IsExist = True
		End If
		'UPGRADE_NOTE: Object lrecReaProd_addcap may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecReaProd_addcap = Nothing
		On Error GoTo 0
	End Function
	
	'% UpdateCapital: se actualiza el capital asociado a la cobertura-rol
	Public Function UpdateCapital(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nCovergen As Integer, ByVal dEffecdate As Date, ByVal nRole As Integer, ByVal nCapital As Double, ByVal nUsercode As Integer) As Boolean
		Dim lclsRemote As eRemoteDB.Execute
		
		On Error GoTo UpdateCapital_Err
		
		lclsRemote = New eRemoteDB.Execute
		
		With lclsRemote
			.StoredProcedure = "updProd_AddCap_capital"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCovergen", nCovergen, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRole", nRole, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCapital", nCapital, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			UpdateCapital = .Run(False)
		End With
		
UpdateCapital_Err: 
		If Err.Number Then
			UpdateCapital = False
		End If
		On Error GoTo 0
		lclsRemote = Nothing
	End Function
	
	'% Class_Initialize: se controla la creación de la instancia de la clase
	Private Sub Class_Initialize_Renamed()
		nBranch = eRemoteDB.Constants.intNull
		nProduct = eRemoteDB.Constants.intNull
		nCovergen = eRemoteDB.Constants.intNull
		mlngUsercode = eRemoteDB.Constants.intNull
		nBranchadd = eRemoteDB.Constants.intNull
		nProductadd = eRemoteDB.Constants.intNull
		dEffecdate = eRemoteDB.Constants.dtmNull
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'%InsPostDP770: Se realiza la actualización de los datos en la ventana DP770
	Public Function InsPostDP770(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nCovergen As Integer, ByVal nRole As Integer, ByVal dEffecdate As Date, ByVal sBranchadd As String, ByVal sProductadd As String, ByVal sCoveradd As String, ByVal sRoleadd As String, ByVal sClusteradd As String, ByVal sCapitalAdd As String, ByVal sId As String, ByVal sInverse As String, ByVal nCapital As Double, ByVal nUsercode As Integer) As Boolean
		Dim lrecDP770 As eRemoteDB.Execute
		
		On Error GoTo InsPostDP770_Err
		
		lrecDP770 = New eRemoteDB.Execute
		With lrecDP770
			.StoredProcedure = "INSDP770PKG.INSPOSTDP770"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCovergen", nCovergen, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRole", nRole, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sBranchadd", sBranchadd, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 4000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sProductadd", sProductadd, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 4000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCoveradd", sCoveradd, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 4000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sRoleadd", sRoleadd, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 4000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClusteradd", sClusteradd, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 4000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCapitalAdd", sCapitalAdd, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 4000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sId", sId, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 4000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sInverse", sInverse, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 4000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCapital", nCapital, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			InsPostDP770 = .Run(False)
		End With
		lrecDP770 = Nothing
		
InsPostDP770_Err: 
		If Err.Number Then
			InsPostDP770 = False
		End If
		On Error GoTo 0
	End Function
End Class






