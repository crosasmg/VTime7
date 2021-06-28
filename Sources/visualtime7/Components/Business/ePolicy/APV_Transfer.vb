Option Strict Off
Option Explicit On
Public Class APV_Transfer
	'%-------------------------------------------------------%'
	'% $Workfile:: APV_Transfer.cls                         $%'
	'% $Author:: Nvaplat41                                  $%'
	'% $Date:: 9/10/03 19.01                                $%'
	'% $Revision:: 10                                       $%'
	'%-------------------------------------------------------%'
	
	'Column_Name                    Type                  Length Prec Scale Nullable
	'------------------------------ --------------------- ------ ---- ----- --------
	Public sCertype As String ' CHAR       1    0     0        N
	Public nBranch As Integer ' NUMBER     0    0     5        N
	Public nProduct As Integer ' NUMBER     0    0     5        N
	Public nPolicy As Double ' NUMBER     0    0     10       N
	Public nCertif As Double ' NUMBER     0    0     10       N
	Public dEffecdate As Date ' DATE       7    0     0        N
	Public nInstitution As Integer
	Public nOrigin As Integer
	Public nType_transf As Integer ' NUMBER     0    0     5        N
	Public nAmount_Peso As Double ' NUMBER     0    2     12       N
	Public nAmount_UF As Double ' NUMBER     0    2     12       N
	Public dNulldate As Date ' DATE       7    0     0        S
	Private mlngUsercode As Integer ' NUMBER     0    0     5        N
	Public nTyp_ProfitWorker As Short ' NUMBER
	
	'-Variables auxiliares
	'-Variable que guarda la acción póliza que se esta ejecutando
	Private mlngTransaction As Integer
	
	'-Variables para la validación de pago de primera prima
	Public nFirstPremium As Double
	Public nPremprop As Double
	
	
	'% InsUpdAPV_Transfer: Realiza la actualización de la tabla
	Private Function InsUpdAPV_Transfer(ByVal nAction As Integer) As Boolean
		Dim lrecInsUpdAPV_Transfer As eRemoteDB.Execute
		
		On Error GoTo InsUpdAPV_Transfer_Err
		
		lrecInsUpdAPV_Transfer = New eRemoteDB.Execute
		
		With lrecInsUpdAPV_Transfer
			.StoredProcedure = "InsUpdAPV_Transfer"
			
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInstitution", nInstitution, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOrigin", nOrigin, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nType_Transf", nType_transf, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAmount_Peso", nAmount_Peso, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAmount_UF", nAmount_UF, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dNulldate", dNulldate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", mlngUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 1, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTransaction", mlngTransaction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTyp_ProfitWorker", nTyp_ProfitWorker, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			InsUpdAPV_Transfer = .Run(False)
		End With
		
InsUpdAPV_Transfer_Err: 
		If Err.Number Then
			InsUpdAPV_Transfer = False
		End If
		
		'UPGRADE_NOTE: Object lrecInsUpdAPV_Transfer may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecInsUpdAPV_Transfer = Nothing
		
		On Error GoTo 0
	End Function
	
	'%Add: Crea un registro en la tabla
	Public Function Add() As Boolean
		Add = InsUpdAPV_Transfer(1)
	End Function
	
	'%Update: Actualiza un registro en la tabla
	Public Function Update() As Boolean
		Update = InsUpdAPV_Transfer(2)
	End Function
	
	'%Delete: Borra los datos de la tabla
	Public Function Delete() As Boolean
		Delete = InsUpdAPV_Transfer(3)
	End Function
	
	'%InsValVI7005Upd: Validaciones de la transacción
	Public Function InsValVI7005Upd(ByVal sCodispl As String, ByVal sAction As String, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nInstitution As Integer, ByVal nOrigin As Integer, ByVal nAmount_Peso As Double, Optional ByVal nTyp_ProfitWorker As Short = 0) As String
		Dim lclsErrors As eFunctions.Errors
		Dim lcolAPV_Transfers As ePolicy.APV_Transfers
		Dim lblnError As Boolean
		
		On Error GoTo InsValVI7005Upd_Err
		lclsErrors = New eFunctions.Errors
		lcolAPV_Transfers = New ePolicy.APV_Transfers
		
		With lclsErrors
			If sAction = "Add" Then
				If nInstitution <= 0 Then
					.ErrorMessage(sCodispl, 70127)
				End If
				
				If nOrigin <= 0 Then
					.ErrorMessage(sCodispl, 70090)
				Else
					If nInstitution > 0 And nTyp_ProfitWorker > 0 Then
						'If lcolAPV_Transfers.Find(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nInstitution, nOrigin) Then
						'    .ErrorMessage sCodispl, 70129
						'End If
						If insValExistProfit(sCertype, nBranch, nProduct, nPolicy, nCertif, nInstitution, nOrigin, dEffecdate, nTyp_ProfitWorker) Then
							.ErrorMessage(sCodispl, 80144)
						End If
					End If
				End If
			End If
			
			If nAmount_Peso <= 0 Then
				.ErrorMessage(sCodispl, 70130)
			End If
			
			InsValVI7005Upd = .Confirm
		End With
		
InsValVI7005Upd_Err: 
		If Err.Number Then
			InsValVI7005Upd = "InsValVI7005Upd: " & Err.Description
		End If
		
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		'UPGRADE_NOTE: Object lcolAPV_Transfers may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lcolAPV_Transfers = Nothing
		
		On Error GoTo 0
	End Function
	
	'%InitValues: Inicializa los valores de las variables publicas de la clase
	Private Sub InitValues()
		sCertype = String.Empty
		nBranch = eRemoteDB.Constants.intNull
		nProduct = eRemoteDB.Constants.intNull
		nPolicy = eRemoteDB.Constants.intNull
		nCertif = eRemoteDB.Constants.intNull
		dEffecdate = eRemoteDB.Constants.dtmNull
		nInstitution = eRemoteDB.Constants.intNull
		nOrigin = eRemoteDB.Constants.intNull
		nType_transf = eRemoteDB.Constants.intNull
		nAmount_Peso = eRemoteDB.Constants.intNull
		nAmount_UF = eRemoteDB.Constants.intNull
		dNulldate = eRemoteDB.Constants.dtmNull
		mlngUsercode = eRemoteDB.Constants.intNull
		mlngTransaction = eRemoteDB.Constants.intNull
	End Sub
	
	'%Class_Initialize: Se ejecuta cuando se instancia la clase
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		Call InitValues()
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'%insPostVI7005Upd: Ejecuta el post de la transacción Planes de Ahorros(VI7005)
	Public Function insPostVI7005Upd(ByVal sAction As String, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nInstitution As Integer, ByVal nOrigin As Integer, ByVal nType_transf As Integer, ByVal nAmount_Peso As Double, ByVal nAmount_UF As Double, ByVal dNulldate As Date, ByVal nUsercode As Integer, ByVal nTransaction As Integer, ByVal nTyp_ProfitWorker As Short) As Boolean
		Dim lclsPolicy_Win As Policy_Win
        Dim lcolAPV_Transfers As ePolicy.APV_Transfers
        insPostVI7005Upd = False
		
		On Error GoTo insPostVI7005Upd_Err
		
		With Me
			.sCertype = sCertype
			.nBranch = nBranch
			.nProduct = nProduct
			.nPolicy = nPolicy
			.nCertif = nCertif
			.dEffecdate = dEffecdate
			.nInstitution = nInstitution
			.nOrigin = nOrigin
			.nType_transf = nType_transf
			.nAmount_Peso = nAmount_Peso
			.nAmount_UF = nAmount_UF
			.dNulldate = dNulldate
			.nTyp_ProfitWorker = nTyp_ProfitWorker
			
			mlngTransaction = nTransaction
			mlngUsercode = nUsercode
		End With
		
		Select Case sAction
			Case "Add"
				insPostVI7005Upd = Add
			Case "Update"
				insPostVI7005Upd = Update
			Case "Del"
				insPostVI7005Upd = Delete
		End Select
		
		If insPostVI7005Upd Then
			lclsPolicy_Win = New Policy_Win
			lcolAPV_Transfers = New ePolicy.APV_Transfers
			
			'+ Se actualiza la tabla Policy_Win
			
			If lcolAPV_Transfers.Find(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, 0, 0, 0) Then
				lclsPolicy_Win.Add_PolicyWin(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nUsercode, "VI7005", "2")
			Else
				lclsPolicy_Win.Add_PolicyWin(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nUsercode, "VI7005", "1")
			End If
		End If
		
insPostVI7005Upd_Err: 
		If Err.Number Then
			insPostVI7005Upd = False
		End If
		
		On Error GoTo 0
		
		'UPGRADE_NOTE: Object lclsPolicy_Win may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsPolicy_Win = Nothing
		'UPGRADE_NOTE: Object lcolAPV_Transfers may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lcolAPV_Transfers = Nothing
	End Function
	
	'% insValExistProfit: Verifica que para una misma entidad-origen, no exista el mismo régimen.
	Public Function insValExistProfit(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nInstitution As Double, ByVal nOrigin As Short, ByVal dEffecdate As Date, ByVal nTyp_ProfitWorker As Short) As Boolean
		Dim lclsAPV_Transfer As eRemoteDB.Execute
		
		On Error GoTo insValExistProfit_Err
		
		lclsAPV_Transfer = New eRemoteDB.Execute
		
		With lclsAPV_Transfer
			.StoredProcedure = "REAEXISTPROFIT_WORKER"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInstitution", nInstitution, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOrigin", nOrigin, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTyp_ProfitWorker", nTyp_ProfitWorker, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nExist", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(False) Then
				insValExistProfit = (.Parameters("nExist").Value = 1)
			Else
				insValExistProfit = False
			End If
		End With
		
insValExistProfit_Err: 
		If Err.Number Then
			insValExistProfit = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsAPV_Transfer may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsAPV_Transfer = Nothing
	End Function
	
	
	
	Public Function ReaPropQuotPremium(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date) As Boolean
		Dim lrecReaPropQuotPremium As eRemoteDB.Execute
		
		'+Definición de parámetros para stored procedure 'ReaPropQuotPremium'
		'+Información leída el 21/03/2003
		On Error GoTo ReaPropQuotPremium_Err
		lrecReaPropQuotPremium = New eRemoteDB.Execute
		With lrecReaPropQuotPremium
			.StoredProcedure = "ReaPropQuotPremium"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", "", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCodispl", "", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTransaction", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				ReaPropQuotPremium = True
				nFirstPremium = .FieldToClass("nFirstPremium")
				nPremprop = .FieldToClass("nPremprop")
				.RCloseRec()
			End If
		End With
		
ReaPropQuotPremium_Err: 
		If Err.Number Then
			ReaPropQuotPremium = False
		End If
		'UPGRADE_NOTE: Object lrecReaPropQuotPremium may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecReaPropQuotPremium = Nothing
		On Error GoTo 0
	End Function
End Class






