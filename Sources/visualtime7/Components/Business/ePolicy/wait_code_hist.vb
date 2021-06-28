Option Strict Off
Option Explicit On
Public Class wait_code_hist
	'%-------------------------------------------------------%'
	'% $Workfile:: wait_code_hist.cls                           $%'
	'% $Author:: rzambrano                                  $%'
	'% $Date:: 22/03/07 13.09                                $%'
	'% $Revision:: 1                                        $%'
	'%-------------------------------------------------------%'
	
	'**- Public variable declaration of the class
	'- Declaración de variables Públicas de la Clase
	
	Public sCertype As String
	Public nBranch As Integer
	Public nProduct As Integer
	Public nPolicy As Double
	Public nCertif As Double
	Public nWait_code As Integer
	Public dEffecdate As Date
	Public sCompdate As String
	Public nSeq As Double
	Public nUsercode As Integer
	
	
	'- Declaración de variables Públicas Auxiliares de la Clase
	'- En la variables siguientes se van a guardar las descripciones de
	'- de Ramo, producto y Causa, estos valores vienen Stored Procedure insudb.reawait_code_hist
	
	Public sRamo As String
	Public sProducto As String
	Public sCausal As String
	Public sUsuario As String
	
	'% insValCAC958: Realiza la validación de los campos a actualizar en la ventana CAC958
	Public Function insValCAC958(ByVal sCodispl As String, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double) As String
		Dim lintSubscript As Integer
		Dim lblError As Boolean
		Dim lobjErrors As eFunctions.Errors
		Dim lclsProduct As eProduct.Product
		Dim lclsPolicy As ePolicy.Policy
		Dim lclsCertificat As ePolicy.Certificat
		
		
		
		On Error GoTo insValCAC958_Err
		
		lobjErrors = New eFunctions.Errors
		
		If (nBranch < 0) Then
			Call lobjErrors.ErrorMessage(sCodispl, 1022)
			lblError = True
		End If
		
		If nProduct <= 0 Then
			Call lobjErrors.ErrorMessage(sCodispl, 1014)
			lblError = True
		Else
			If nBranch > 0 Then
				lclsProduct = New eProduct.Product
				If Not lclsProduct.insValProdMaster(nBranch, nProduct) Then
					Call lobjErrors.ErrorMessage(sCodispl, 1011)
					lblError = True
				End If
				'UPGRADE_NOTE: Object lclsProduct may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
				lclsProduct = Nothing
			End If
		End If
		
		If (nPolicy < 0) Then
			Call lobjErrors.ErrorMessage(sCodispl, 3003)
			lblError = True
		Else
			lclsPolicy = New ePolicy.Policy
			If Not (lclsPolicy.Find(sCertype, nBranch, nProduct, nPolicy)) Then
				Call lobjErrors.ErrorMessage(sCodispl, 3001)
				lblError = True
			End If
			'UPGRADE_NOTE: Object lclsPolicy may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			lclsPolicy = Nothing
		End If
		
		If Not (lblError) And (nCertif >= 0) Then
			lclsCertificat = New Certificat
			If Not lclsCertificat.Find(sCertype, nBranch, nProduct, nPolicy, nCertif) Then
				Call lobjErrors.ErrorMessage(sCodispl, 3010)
				lblError = True
			End If
		End If
		
		
		
		If Not (lblError) Then
			If Not InsExists(sCertype, nBranch, nProduct, nPolicy) Then
				Call lobjErrors.ErrorMessage(sCodispl, 1073)
			End If
		End If
		
		insValCAC958 = lobjErrors.Confirm
		
insValCAC958_Err: 
		If Err.Number Then
			insValCAC958 = "insValCAC958: " & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjErrors = Nothing
	End Function
	
	
	'%InsExists: Verifica la existencia de regisros en la tabla wait_code_cost
	Public Function InsExists(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double) As Boolean
		Dim lrecwait_code_hist As eRemoteDB.Execute
		Dim nExists As Integer
		Dim lintExist As Integer
		
		On Error GoTo InsExists_Err
		
		nExists = 0
		InsExists = True
		
		lrecwait_code_hist = New eRemoteDB.Execute
		
		With lrecwait_code_hist
			.StoredProcedure = "INSEXISTS_WAIT_CODE_HIST"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nExists", nExists, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(False) Then
				lintExist = .Parameters.Item("nExists").Value
				If lintExist > 0 Then
					InsExists = True
				Else
					InsExists = False
				End If
			Else
				InsExists = False
			End If
		End With
		
		'UPGRADE_NOTE: Object lrecwait_code_hist may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecwait_code_hist = Nothing
		
InsExists_Err: 
		If Err.Number Then
			InsExists = False
		End If
		On Error GoTo 0
	End Function
End Class






