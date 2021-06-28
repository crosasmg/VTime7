Option Strict Off
Option Explicit On
Public Class Cl_cov_bil
	'%-------------------------------------------------------%'
	'% $Workfile:: Cl_cov_bil.cls                           $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 12:35p                               $%'
	'% $Revision:: 11                                       $%'
	'%-------------------------------------------------------%'
	'-
	'- Estructura de tabla CL_COV_BIL al 07-10-2002 18:10:19
	'-     Property                    Type         DBType   Size Scale  Prec  Null
	Public nBranch As Integer ' NUMBER     22   0     5    N
	Public nProduct As Integer ' NUMBER     22   0     5    N
	Public nModulec As Integer ' NUMBER     22   0     5    N
	Public nCover As Integer ' NUMBER     22   0     5    N
	Public dEffecdate As Date ' DATE       7    0     0    N
	Public nRole As Integer ' NUMBER     22   0     5    N
	Public nPay_concep As Integer ' NUMBER     22   0     5    N
	Public dCompdate As Date ' DATE       7    0     0    S
	Public dNulldate As Date ' DATE       7    0     0    S
	Public nUsercode As Integer ' NUMBER     22   0     5    S
	Public sStatregt As String ' CHAR       1    0     0    S
	Public nPerclaim As Double ' NUMBER     22   2     5    S
	
	
	Public sDescript As String
	Public sShort_des As String
	Public nSelection As Integer
	
	'%Delete_cl_cov_bil: Este método se encarga de eliminar registros en la tabla "Cl_cov_bil". Devolviendo verdadero o
	'%falso dependiendo de si el Stored procedure se ejecutó correctamente.
	Public Function Delete_cl_cov_bil() As Boolean
		Dim lrecinsDelcl_cov_bil As eRemoteDB.Execute
		
		On Error GoTo insDelcl_cov_bil_Err
		
		lrecinsDelcl_cov_bil = New eRemoteDB.Execute
		
		'+Definición de parámetros para stored procedure 'insudb.insDelcl_cov_bil'
		'+Información leída el 07/05/2001 04:28:57 p.m.
		
		With lrecinsDelcl_cov_bil
			.StoredProcedure = "insDelcl_cov_bil"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPay_concep", nPay_concep, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Delete_cl_cov_bil = .Run(False)
		End With
		
insDelcl_cov_bil_Err: 
		If Err.Number Then
			Delete_cl_cov_bil = False
		End If
		
		'UPGRADE_NOTE: Object lrecinsDelcl_cov_bil may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsDelcl_cov_bil = Nothing
		On Error GoTo 0
		
	End Function
	
	'%Update: Este método se encarga de actualizar registros en la tabla "Cl_cov_bil". Devolviendo verdadero o
	'%falso dependiendo de si el Stored procedure se ejecutó correctamente.
	Public Function Update_cl_cov_bil() As Boolean
		Dim lrecinsCl_cov_bil As eRemoteDB.Execute
		
		On Error GoTo insCl_cov_bil_Err
		
		lrecinsCl_cov_bil = New eRemoteDB.Execute
		
		'+Definición de parámetros para stored procedure 'insudb.insCl_cov_bil'
		'+Información leída el 07/05/2001 04:02:28 p.m.
		
		With lrecinsCl_cov_bil
			.StoredProcedure = "insCl_cov_bil"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPay_concep", nPay_concep, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sStatregt", sStatregt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Update_cl_cov_bil = .Run(False)
		End With
		
insCl_cov_bil_Err: 
		If Err.Number Then
			Update_cl_cov_bil = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecinsCl_cov_bil may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsCl_cov_bil = Nothing
	End Function
	
	'%insValDP049: Esta rutina realiza la validación de los datos de conceptos de reserva de pago
	Public Function insValDP049(ByVal sCodispl As String, ByVal nCount_Reg As Integer, ByVal sStatregt As String) As String
		Dim lclsErrors As eFunctions.Errors
		
		On Error GoTo insValDP049_err
		lclsErrors = New eFunctions.Errors
		
		If nCount_Reg <= 0 Then
			Call lclsErrors.ErrorMessage("DP049", 1047)
		End If
		
		If sStatregt = String.Empty Then
			Call lclsErrors.ErrorMessage("DP049", 9089)
		End If
		
		insValDP049 = lclsErrors.Confirm
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		
insValDP049_err: 
		If Err.Number Then
			insValDP049 = insValDP049 & Err.Description
		End If
		
		On Error GoTo 0
	End Function
	
	'%insPostDP049: Este metodo se encarga de realizar el impacto en la base de datos (descrito en las
	'%especificaciones funcionales)de la ventana "DP049"
	Public Function insPostDP049(ByVal sAction As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nModulec As Integer, ByVal nCover As Integer, ByVal nPay_concep As Integer, ByVal dEffecdate As Date, ByVal sStatregt As String, ByVal nUsercode As Integer) As Boolean
		Dim lclsCl_cov_bil As Cl_cov_bil
		
		On Error GoTo insPostDP049_err
		
		lclsCl_cov_bil = New Cl_cov_bil
		
		With lclsCl_cov_bil
			.nBranch = nBranch
			.nProduct = nProduct
			.nModulec = nModulec
			.nCover = nCover
			.nPay_concep = nPay_concep
			.dEffecdate = dEffecdate
			.sStatregt = sStatregt
			.nUsercode = nUsercode
		End With
		If sAction = "Add" Or sAction = "Update" Then
			insPostDP049 = lclsCl_cov_bil.Update_cl_cov_bil
		Else
			insPostDP049 = lclsCl_cov_bil.Delete_cl_cov_bil
		End If
		
		'UPGRADE_NOTE: Object lclsCl_cov_bil may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsCl_cov_bil = Nothing
		
insPostDP049_err: 
		If Err.Number Then
			insPostDP049 = False
		End If
		On Error GoTo 0
	End Function
	
	'% valCl_Cov_BilByProduct: verifica que existan conceptos de pago para la cobertura
	Public Function valCl_Cov_BilByProduct(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nModulec As Integer, ByVal nCover As Integer, ByVal dEffecdate As Date) As Boolean
		Dim lclsRemote As eRemoteDB.Execute
		
		On Error GoTo valCl_Cov_BilByProduct_Err
		
		lclsRemote = New eRemoteDB.Execute
		
		With lclsRemote
			.StoredProcedure = "valCl_cov_bil_exist"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nExists", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				valCl_Cov_BilByProduct = .Parameters("nExists").Value = 1
			End If
		End With
		
valCl_Cov_BilByProduct_Err: 
		If Err.Number Then
			valCl_Cov_BilByProduct = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsRemote may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsRemote = Nothing
	End Function
End Class






