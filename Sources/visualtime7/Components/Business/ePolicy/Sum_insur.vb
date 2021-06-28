Option Strict Off
Option Explicit On
Public Class Sum_insur
	'%-------------------------------------------------------%'
	'% $Workfile:: Sum_insur.cls                            $%'
	'% $Author:: Nvaplat41                                  $%'
	'% $Date:: 9/10/03 19.01                                $%'
	'% $Revision:: 17                                       $%'
	'%-------------------------------------------------------%'
	
	'+ Column_name            Type                         Computed     Length      Prec  Scale Nullable      TrimTrailingBlanks    FixedLenNullInSource
	'+ --------------------------------------------------------------------------------------------------------------------------------------------------
	Public sCertype As String 'char          no           1                       no            yes                   no
	Public nBranch As Integer 'smallint      no           2           5     0     no            (n/a)                 (n/a)
	Public nProduct As Integer 'smallint      no           2           5     0     no            (n/a)                 (n/a)
	Public nPolicy As Double 'int           no           4           10    0     no            (n/a)                 (n/a)
	Public nCertif As Double 'int           no           4           10    0     no            (n/a)                 (n/a)
	Public nSumins_cod As Integer 'smallint      no           2           5     0     no            (n/a)                 (n/a)
	Public dEffecdate As Date 'datetime      no           8                       no            (n/a)                 (n/a)
	Public nSumins_real As Double 'decimal       no           9           12    0     yes           (n/a)                 (n/a)
	Public nSum_insur As Double 'decimal       no           9           12    0     yes           (n/a)                 (n/a)
	Public nCoinsuran As Double 'decimal       no           5           5     2     yes           (n/a)                 (n/a)
	Public nCurrency As Integer 'smallint      no           2           5     0     yes           (n/a)                 (n/a)
	Public dNulldate As Date 'datetime      no           8                       yes           (n/a)                 (n/a)
	Public nTransactio As Integer 'smallint      no           2           5     0     yes           (n/a)                 (n/a)
	Public nUsercode As Integer 'smallint      no           2           5     0     yes           (n/a)                 (n/a)
	
	Public nAction As Integer
	
	'- Variable para tomar el valor de la descripciòn
	Public sDescript As String
	Public nCode As Integer
	Public sCurrency As String
	
	'+Variables para la transacción cal963
	Public dStartdate As Date
	Public dExpirdate As Date
	Public nPremium_tmp As Double
	Public nPremium_Real As Double
	Public nPremium_ajust As Double
	
	
	
	'% insValCA009Upd: Esta función realiza las validaciones de los capitales básicos asegurados
	Public Function insValCA009Upd(ByVal sCodispl As String, ByVal sDescript As String, ByVal nSumins_real As Double, ByVal nCoinsuran As Double, ByVal nSum_insur As Double) As String
		Dim lclsErrors As eFunctions.Errors
		
		On Error GoTo insValCA009Upd_err
		lclsErrors = New eFunctions.Errors
		
		'+ Se validan todos los campos numéricos
		With lclsErrors
			If nCoinsuran <> 0 And nCoinsuran > 100 Then
				.ErrorMessage(sCodispl, 3579)
			End If
			
			If nSumins_real > 0 Then
				If nSumins_real <> eRemoteDB.Constants.intNull Then
					If nCoinsuran = eRemoteDB.Constants.intNull Then
						.ErrorMessage(sCodispl, 3496)
					End If
				End If
				
				If nSumins_real <> eRemoteDB.Constants.intNull Then
					If nSum_insur = eRemoteDB.Constants.intNull Then
						nSum_insur = (nSumins_real * nCoinsuran) / 100
					End If
				End If
			ElseIf nSumins_real = 0 Then 
				.ErrorMessage(sCodispl, 55964)
			End If
			
			insValCA009Upd = .Confirm
		End With
		
insValCA009Upd_err: 
		If Err.Number Then
			insValCA009Upd = "insValCA009Upd: " & Err.Description
		End If
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		On Error GoTo 0
	End Function
	
	'% insValCA009: Este metodo se encarga de realizar las validaciones descritas en el funcional
	'% de la ventana "CA009"
	Public Function insValCA009(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nCurrency As Integer) As String
		Dim lclsErrors As eFunctions.Errors
		Dim lblnError As Boolean
		Dim lclsSum_insur As Sum_insur
		Dim lcolSum_insurs As Sum_insurs
		
		lclsErrors = New eFunctions.Errors
		lcolSum_insurs = New ePolicy.Sum_insurs
		On Error GoTo insValCA009_err
		
		With lclsErrors
			lblnError = False
			'+ Se realiza el llamado a la funcion Find, para verificar si existen capitales asegurados
			If lcolSum_insurs.Find(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nCurrency) Then
				lblnError = True
				For	Each lclsSum_insur In lcolSum_insurs
					If lclsSum_insur.nSumins_real <> eRemoteDB.Constants.intNull And lclsSum_insur.nSumins_real <> 0 Then
						lblnError = False
						Exit For
					End If
				Next lclsSum_insur
			End If
			
			If lblnError Then
				.ErrorMessage("CA009", 3040)
			End If
			
			insValCA009 = .Confirm()
		End With
		
insValCA009_err: 
		If Err.Number Then
			insValCA009 = "insValCA009: " & Err.Description
		End If
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		'UPGRADE_NOTE: Object lcolSum_insurs may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lcolSum_insurs = Nothing
		'UPGRADE_NOTE: Object lclsSum_insur may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsSum_insur = Nothing
		On Error GoTo 0
	End Function
	
	'% insPostCA009: Realiza la acutalización de la tabla "Sum_insur"
	Public Function insPostCA009(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nUsercode As Integer, ByVal nSumins_cod As Integer, ByVal nSumins_real As Double, ByVal nSum_insur As Double, ByVal nCoinsuran As Double, ByVal nCurrency As Integer, ByVal sTransaction As String, ByVal sAction As String, ByVal dNulldate As Date, ByVal nSum_insur_old As Double, ByVal sPolitype As String, ByVal sBrancht As String) As Boolean
		Dim lclsPolicy_Win As ePolicy.Policy_Win
		Dim lintAction As Integer
		Dim lblnLife As Boolean
		Dim lblnUpdPw As Boolean
		
		On Error GoTo insPostCA009_Err
		Select Case sAction
			Case "Add"
				lblnUpdPw = True
				lintAction = 1
			Case "Update"
				lintAction = 2
				If nSum_insur <> nSum_insur_old Then
					lblnUpdPw = True
				End If
			Case "Del"
				lblnUpdPw = True
				lintAction = 3
		End Select
		
		insPostCA009 = True
		If lblnUpdPw Then
			insPostCA009 = InsUpdSum_insur(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nUsercode, nSumins_cod, nSumins_real, nSum_insur, nCoinsuran, nCurrency, sTransaction, lintAction, dNulldate)
			
			If insPostCA009 Then
				lclsPolicy_Win = New ePolicy.Policy_Win
				Call lclsPolicy_Win.Add_PolicyWin(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nUsercode, "CA009", "2")
				
				If sPolitype = "1" Or nCertif > 0 Then
					If ((sBrancht = CStr(eProduct.Product.pmBrancht.pmlife) Or sBrancht = CStr(eProduct.Product.pmBrancht.pmNotTraditionalLife)) And (sPolitype = "1" Or (sPolitype <> "1" And nCertif > 0))) Then
						lblnLife = True
					End If
					insPostCA009 = lclsPolicy_Win.Add_PolicyWin(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nUsercode, "CA014", "3",  ,  , lblnLife, False)
				Else
					insPostCA009 = lclsPolicy_Win.Add_PolicyWin(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nUsercode, "CA014A", "3",  ,  ,  , False)
				End If
				'UPGRADE_NOTE: Object lclsPolicy_Win may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
				lclsPolicy_Win = Nothing
			End If
		End If
		
insPostCA009_Err: 
		If Err.Number Then
			insPostCA009 = False
		End If
		On Error GoTo 0
	End Function
	
	'% InsUpdSum_insur: Recorre la colección y actualiza los datos en la tabla
	Private Function InsUpdSum_insur(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nUsercode As Integer, ByVal nSumins_cod As Integer, ByVal nSumins_real As Double, ByVal nSum_insur As Double, ByVal nCoinsuran As Double, ByVal nCurrency As Integer, ByVal sTransaction As String, ByVal nAction As Integer, ByVal dNulldate As Date) As Boolean
		Dim lrecinsSum_insur As eRemoteDB.Execute
		
		On Error GoTo InsUpdSum_insur_Err
		lrecinsSum_insur = New eRemoteDB.Execute
		
		'+ Definición de parámetros para stored procedure 'insudb.insSum_insur'
		'+ Información leída el 27/11/2000 08:53:58 a.m.
		With lrecinsSum_insur
			.StoredProcedure = "insSum_insur"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nSumins_cod", nSumins_cod, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nSumins_real", nSumins_real, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nSum_insur", nSum_insur, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCoinsuran", nCoinsuran, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sTransaction", sTransaction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 2, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dNulldate", dNulldate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			InsUpdSum_insur = .Run(False)
		End With
		
InsUpdSum_insur_Err: 
		If Err.Number Then
			InsUpdSum_insur = False
		End If
		'UPGRADE_NOTE: Object lrecinsSum_insur may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsSum_insur = Nothing
		On Error GoTo 0
	End Function
End Class






