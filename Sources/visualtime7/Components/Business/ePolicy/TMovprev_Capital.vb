Option Strict Off
Option Explicit On
Public Class TMovprev_Capital
	'%-------------------------------------------------------%'
	'% $Workfile:: TMovprev_Capital.cls                     $%'
	'% $Author:: Nvaplat41                                  $%'
	'% $Date:: 9/10/03 19.02                                $%'
	'% $Revision:: 24                                       $%'
	'%-------------------------------------------------------%'
	'+
	'+ Estructura de tabla TIMETMP.TMOVPREV_CAPITAL al 12-20-2001 17:25:41
	'+     Property                Type         DBType   Size Scale  Prec  Null
	'+-------------------------------------------------------------------------
	Public sCertype As String ' CHAR       1    0     0    N
	Public nBranch As Integer ' NUMBER     22   0     5    N
	Public nProduct As Integer ' NUMBER     22   0     5    N
	Public nPolicy As Double ' NUMBER     22   0     10   N
	Public nCertif As Double ' NUMBER     22   0     10   N
	Public nId As Integer ' NUMBER     22   0     5    N
	Public nReceipt As Integer ' NUMBER     22   0     10   N
	Public nCover As Integer ' NUMBER     22   0     5    N
	Public sClient As String ' CHAR       14   0     0    N
	Public nPremium As Double ' NUMBER     22   2     10   S
	Public nPercent As Double ' NUMBER     22   6     9    S
	Public nCost As Double ' NUMBER     22   6     9    S
	Public nCapital As Double ' NUMBER     22   0     12   S
	Public nCapitaltot As Double ' NUMBER     22   0     12   S
	Public nSurrAmount As Double ' NUMBER     22   2     12   S
	Public nTypemov As Integer ' NUMBER     22   0     5    S
	Public dEffecdate As Date ' DATE       7    0     0    N
	Public nUsercode As Integer ' NUMBER     22   0     5    N
	Public sKey As String ' CHAR       20   0     0    N
	' variable utilizada en la grid VI806
	Public nPayfreq As Integer
	'-Se definen las constantes para el manejo del tipo de registro (Póliza)
	
	
	
	'Public losCamposLlave
	'Public losCamposTodos
	
	'%InsUpdTMovprev_Capital: Se encarga de actualizar la tabla TMovprev_Capital
	Private Function InsUpdTMovprev_Capital(ByVal nAction As Integer) As Boolean
		
	End Function
	
	'%Add: Crea un registro en la tabla
	Public Function Add() As Boolean
		Add = InsUpdTMovprev_Capital(1)
	End Function
	
	'%Update: Actualiza un registro en la tabla
	Public Function Update() As Boolean
		Update = InsUpdTMovprev_Capital(2)
	End Function
	
	'%Delete: Borra un registro en la tabla
	Public Function Delete() As Boolean
		Delete = InsUpdTMovprev_Capital(3)
	End Function
	
	'%Find: Lee los datos de la tabla
	Public Function Find(ByVal losCamposLlave As Object, Optional ByVal lblnFind As Boolean = False) As Boolean
	End Function
	
	'%InsValEffecdate: Valida la fecha de efecto de la transacción
	Public Function InsValEffecdate(ByVal losCamposLlave As Object, ByVal dEffecdate As Date) As Boolean
		
		
	End Function
	
	'%InsValVI806_K: Validaciones de la transacción(Header)
	Public Function InsValVI806_K(ByVal sCodispl As String, ByVal nAction As Integer, ByVal losCamposLlave As Object, ByVal dEffecdate As Date) As String
		Dim lclsErrors As eFunctions.Errors
		
		On Error GoTo InsValVI806_K_Err
		lclsErrors = New eFunctions.Errors
		
		With lclsErrors
			'+ Se valida el Campo Fecha
			If dEffecdate = eRemoteDB.Constants.dtmNull Then
				.ErrorMessage(sCodispl, 99999)
			Else
				If nAction = eFunctions.Menues.TypeActions.clngActionUpdate Or nAction = eFunctions.Menues.TypeActions.clngActionadd Then
					'If Not InsValEffecdate(dEffecdate) Then
					'    .ErrorMessage sCodispl, 10869
					'End If
				End If
			End If
			
			InsValVI806_K = .Confirm
		End With
		
InsValVI806_K_Err: 
		If Err.Number Then
			InsValVI806_K = "InsValVI806_K: " & Err.Description
		End If
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		On Error GoTo 0
	End Function
	
	'%InsValVI806: Validaciones de la transacción Solicitud de Pólizas a capitalizar
	'%             Capitalización de Fondos (Previsión y Retiro - VI806)
	Public Function InsValVI806(ByVal sCodispl As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date) As String
		Dim lclsErrors As eFunctions.Errors
		Dim lclsProduct As eProduct.Product
		Dim lclsPolicy As ePolicy.Policy
		Dim lclsCertificat As ePolicy.Certificat
		Dim lblnPolicy As Boolean
		Dim lblnCertificat As Boolean
		
		
		On Error GoTo InsValVI806_Err
		lclsErrors = New eFunctions.Errors
		lclsProduct = New eProduct.Product
		Call lclsProduct.FindProdMaster(nBranch, nProduct)
		
		Dim lstrBrancht As Object
		With lclsErrors
			lstrBrancht = lclsProduct.sBrancht
			
			'+ Validar Ramo
			If nBranch = eRemoteDB.Constants.intNull Or nBranch = 0 Then
				.ErrorMessage(sCodispl, 1022)
			End If
			
			'+ Validar Producto
			If nProduct = eRemoteDB.Constants.intNull Or nProduct = 0 Then
				.ErrorMessage(sCodispl, 1014)
			Else
				If (lclsProduct.sBrancht <> 1 And lclsProduct.sBrancht <> 5) Then
					.ErrorMessage(sCodispl, 3987)
				End If
			End If
			
			'+Se valida el campo póliza (es opcional)
			If nPolicy <> eRemoteDB.Constants.intNull Then
				lclsPolicy = New ePolicy.Policy
				lblnPolicy = lclsPolicy.Find(CStr(Constantes.ePolCertype.cstrPolicy), nBranch, nProduct, nPolicy, True)
				If Not lblnPolicy Then
					.ErrorMessage(sCodispl, 3001)
				Else
					'+ Se valida el Certificado
					lclsCertificat = New ePolicy.Certificat
					lblnCertificat = lclsCertificat.Find(CStr(Constantes.ePolCertype.cstrPolicy), nBranch, nProduct, nPolicy, nCertif, True)
					If Not lblnCertificat Then
						.ErrorMessage(sCodispl, 13908)
					End If
				End If
			End If
			
			'+ Validar fecha de vigencia de ejecución del proceso
			If dEffecdate = eRemoteDB.Constants.dtmNull Then
				.ErrorMessage(sCodispl, 3404)
			End If
			InsValVI806 = .Confirm
		End With
		
InsValVI806_Err: 
		If Err.Number Then
			InsValVI806 = "InsValVI806: " & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		'UPGRADE_NOTE: Object lclsProduct may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsProduct = Nothing
		'UPGRADE_NOTE: Object lclsPolicy may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsPolicy = Nothing
		'UPGRADE_NOTE: Object lclsCertificat may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsCertificat = Nothing
	End Function
	
	'%InsPostVI806: Ejecuta el post de la transacción
	'%               Capitalización de Fondos (Previsión y Retiro VI806)
	Public Function InsPostVI806(ByVal sCodispl As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, ByVal nUsercode As Integer, ByVal sKey As String, Optional ByVal nCertif As Double = 0, Optional ByVal nPolicy As Double = 0) As Boolean
		On Error GoTo InsPostVI806_Err
		InsPostVI806 = crea_TMovprev_capital(sCodispl, nBranch, nProduct, dEffecdate, nUsercode, sKey, nCertif, nPolicy)
InsPostVI806_Err: 
		If Err.Number Then
			InsPostVI806 = False
		End If
		On Error GoTo 0
	End Function
	
	'%Class_Initialize: Inicializa las propiedades cuando se instancia la clase
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		'    losCamposTodos = NumNull
		'    dEffecdate = dtmNull
		'    nUsercode = NumNull
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'%crea_TMovprev_capital. Este metodo se encarga de creaar y/o actualizar el registro de tCover
	Public Function crea_TMovprev_capital(ByVal sCodispl As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, ByVal nUsercode As Integer, ByVal sKey As String, Optional ByVal nCertif As Double = 0, Optional ByVal nPolicy As Double = 0) As Boolean
		Dim lcrea_TMovprev_capital As eRemoteDB.Execute
		
		On Error GoTo Add_err
		
		lcrea_TMovprev_capital = New eRemoteDB.Execute
		
		'+Definición de parámetros para stored procedure 'crecovert'
		'+Información leída el 12/12/01
		With lcrea_TMovprev_capital
			.StoredProcedure = "REACERTIFICAT_DNEXTRECEIP"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			crea_TMovprev_capital = .Run(False)
		End With
		
Add_err: 
		If Err.Number Then
			crea_TMovprev_capital = False
		End If
		'UPGRADE_NOTE: Object lcrea_TMovprev_capital may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lcrea_TMovprev_capital = Nothing
		On Error GoTo 0
	End Function
	
	'%Copy_TMovprev_capital.
	Public Function Copy_TMovprev_capital(ByVal sKey As String) As Boolean
		Dim lCopy_TMovprev_capital As eRemoteDB.Execute
		
		On Error GoTo Copy_TMovprev_capital_Err
		
		lCopy_TMovprev_capital = New eRemoteDB.Execute
		
		'+Definición de parámetros para stored procedure 'crecovert'
		'+Información leída el 12/12/01
		With lCopy_TMovprev_capital
			.StoredProcedure = "INSMOVPREV_CAPITAL"
			.Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Copy_TMovprev_capital = .Run(False)
		End With
		
Copy_TMovprev_capital_Err: 
		If Err.Number Then
			Copy_TMovprev_capital = False
		End If
		'UPGRADE_NOTE: Object lCopy_TMovprev_capital may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lCopy_TMovprev_capital = Nothing
		On Error GoTo 0
	End Function
End Class






