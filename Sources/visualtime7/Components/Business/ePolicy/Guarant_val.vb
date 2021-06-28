Option Strict Off
Option Explicit On
Public Class Guarant_val
	'%-------------------------------------------------------%'
	'% $Workfile:: Guarant_val.cls                          $%'
	'% $Author:: Nvaplat22                                  $%'
	'% $Date:: 6/08/04 12:25p                               $%'
	'% $Revision:: 66                                       $%'
	'%-------------------------------------------------------%'
	
	'+ Column_Name         Type
	'--------------------- ---------
	Public sCertype As String
	Public nBranch As Integer
	Public nProduct As Integer
	Public nPolicy As Double
	Public nCertif As Double
	Public nYear As Integer
	Public nAge As Integer
	Public dEffecdate As Date
	Public nMonth As Integer
	Public nCurrency As Integer
	Public dCompdate As Date
	Public nPro_year As Integer
	Public nResc_val As Double
	Public nSald_val As Double
	Public nSaldvalkm As Double
	Public dNulldate As Date
	Public nUsercode As Integer
	Public nDefamount As Double
	Public nDeferred As Integer
	Public nSal_tax As Double
	Public nPeriod_cov As Integer
	
	'+ Variables auxiliares
	
	'- Forma de generación de los valores garantizados de la póliza
	'+ 1.- Automática
	'+ 2.- Manual
	
	Public sAut_guarval As String
	
	'- Variable para el manejo de la colección asociada a la tabla
	
	Public mcolGuarant_val As Guarant_vals
	
	'- Variables para el control de errores en la carga de la página
	
	Public nError As Integer
	Public bError As Boolean
	
	'-Descripcion de tipos de diferido
	Public sDeferred_Desc As String
	
	'% inspreVI732: se controla el acceso a la página
	Public Sub inspreVI732(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal sAut_guarval As String, ByVal nCurrency As Integer, ByVal nUsercode As Integer, ByVal sDelete As String, ByVal bQuery As Boolean)
		Dim lclsCertificat As Certificat
		Dim lclsCurren_pol As Curren_pol
		Dim lblnValid As Boolean
		
		lclsCertificat = New Certificat
		lclsCurren_pol = New Curren_pol
		
		bError = False
		lblnValid = True
		
		With Me
			If sAut_guarval = String.Empty Or nCurrency = eRemoteDB.Constants.intNull Then
				If lclsCertificat.Find(sCertype, nBranch, nProduct, nPolicy, nCertif) Then
					.sAut_guarval = lclsCertificat.sAut_guarval
				End If
				If .sAut_guarval = String.Empty Then
					'+ Se asigna el cálculo autómatico por defecto
					.sAut_guarval = "1"
				End If
				With lclsCurren_pol
					If lclsCurren_pol.Find(nPolicy, nBranch, nProduct, sCertype, nCertif, dEffecdate) Then
						.nCurrency = 0
						If lclsCurren_pol.IsLocal Then
							Me.nCurrency = 1
						Else
							Call lclsCurren_pol.Val_Curren_pol(0)
						End If
						Me.nCurrency = lclsCurren_pol.nCurrency
					Else
						'+ La póliza no tiene monedas asignadas
						nError = 3738
						bError = True
					End If
				End With
			Else
				.nCurrency = nCurrency
				.sAut_guarval = sAut_guarval
			End If
			
			If Not bError Then
				'+ Se eliminan todos los datos asociados a la póliza
				If Not bQuery Then
					If sDelete <> String.Empty Then
						lblnValid = DeleteAll(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate)
					End If
				End If
				
				If lblnValid Then
					'+ Se realiza el cálculo automático
					If Not bQuery Then
						If .sAut_guarval = "1" Then
							lblnValid = inscalGuarant_val(sCertype, nBranch, nProduct, nPolicy, nCertif, CStr(dEffecdate), nUsercode)
						End If
					End If
					If lblnValid Then
						Call mcolGuarant_val.Find(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, .nCurrency)
					End If
				End If
			End If
		End With
		
		'UPGRADE_NOTE: Object lclsCertificat may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsCertificat = Nothing
		'UPGRADE_NOTE: Object lclsCurren_pol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsCurren_pol = Nothing
	End Sub
	
	'% Update: Se agrega un registro en la tabla
	Private Function Add() As Boolean
		Add = Update(1)
	End Function
	
	'% Delete: Se elimina un registro en la tabla
	Private Function Delete() As Boolean
		Delete = Update(3)
	End Function
	
	'% Update: Se actualiza el registro en la tabla
	Private Function Update(ByVal nAction As Integer) As Boolean
		Dim lclsRemote As eRemoteDB.Execute
		
		On Error GoTo Update_Err
		
		lclsRemote = New eRemoteDB.Execute
		
		With lclsRemote
			.StoredProcedure = "insupdGuarant_val"
			.Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nYear", nYear, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAge", nAge, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nMonth", nMonth, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPro_year", nPro_year, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nResc_val", nResc_val, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nSald_val", nSald_val, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nSaldvalkm", nSaldvalkm, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDefamount", nDefamount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDeferred", nDeferred, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nSal_tax", nSal_tax, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 4, 6, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPeriod_cov", nPeriod_cov, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Update = .Run(False)
		End With
		
Update_Err: 
		If Err.Number Then
			Update = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsRemote may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsRemote = Nothing
	End Function
	
	'% insvalVI732: se realizan las validaciones de la página
	Public Function insvalVI732(ByVal sCodispl As String, ByVal sAction As String, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nYear As Integer, ByVal nMonth As Integer, ByVal nAge As Integer, ByVal nResc_val As Double) As String
		Dim lclsErrors As eFunctions.Errors
		Dim lblnValid As Boolean
		
		On Error GoTo insvalVI732_Err
		
		lclsErrors = New eFunctions.Errors
		
		lblnValid = True
		
		With lclsErrors
			'+ El año de la póliza debe estar lleno
			If nYear = eRemoteDB.Constants.intNull Then
				Call .ErrorMessage(sCodispl, 9091)
				lblnValid = False
			End If
			
			'+ El mes de la póliza debe estar lleno
			If nMonth = eRemoteDB.Constants.intNull Then
				Call .ErrorMessage(sCodispl, 1925,  , eFunctions.Errors.TextAlign.LeftAling, "Mes póliza:")
				lblnValid = False
			End If
			
			'+ La edad actuarial debe estar llena
			If nAge = eRemoteDB.Constants.intNull Then
				Call .ErrorMessage(sCodispl, 1925,  , eFunctions.Errors.TextAlign.LeftAling, "Edad actuarial:")
				lblnValid = False
			End If
			
			'+ Si los campos anteriores tienen información, el valor de rescate debe estar lleno
			If lblnValid Then
				If nResc_val = eRemoteDB.Constants.intNull Then
					Call .ErrorMessage(sCodispl, 60015)
				End If
				
				If sAction = "Add" Then
					'+ El registro no puede estar duplicado
					If Find(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nAge, nYear, nMonth) Then
						Call .ErrorMessage(sCodispl, 55899)
					End If
				End If
			End If
			
			insvalVI732 = .Confirm
		End With
		
insvalVI732_Err: 
		If Err.Number Then
			insvalVI732 = "insvalVI732: " & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
	End Function
	
	'% inspostVI732: se actualizan los datos en la tabla
	Public Function inspostVI732(ByVal sAction As String, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal sAut_guarval As String, Optional ByVal nCurrency As Integer = 0, Optional ByVal nYear As Integer = 0, Optional ByVal nAge As Integer = 0, Optional ByVal nMonth As Integer = 0, Optional ByVal nPro_year As Integer = 0, Optional ByVal nResc_val As Double = 0, Optional ByVal nSald_val As Double = 0, Optional ByVal nSaldvalkm As Double = 0, Optional ByVal nUsercode As Integer = 0, Optional ByVal nDefamount As Double = 0, Optional ByVal nDeferred As Integer = 0, Optional ByVal nSal_tax As Double = 0, Optional ByVal nPeriod_cov As Integer = 0) As Boolean
		Dim lclsCertificat As Certificat
		Dim lclsPolicy_Win As Policy_Win
		
		On Error GoTo insPostVi732_err
		
		With Me
			.sCertype = sCertype
			.nBranch = nBranch
			.nProduct = nProduct
			.nPolicy = nPolicy
			.nCertif = nCertif
			.dEffecdate = dEffecdate
			.nYear = nYear
			.nAge = nAge
			.nMonth = nMonth
			.nCurrency = nCurrency
			.nPro_year = nPro_year
			.nResc_val = nResc_val
			.nSald_val = nSald_val
			.nSaldvalkm = nSaldvalkm
			.nUsercode = nUsercode
			.nDefamount = nDefamount
			.nDeferred = nDeferred
			.nSal_tax = nSal_tax
			.nPeriod_cov = nPeriod_cov
		End With
		
		Select Case sAction
			Case "Add"
				inspostVI732 = Add
				
			Case "Update"
				inspostVI732 = Update(2)
				
			Case "Del"
				inspostVI732 = Delete
				
			Case Else
				lclsCertificat = New Certificat
				If lclsCertificat.Find(sCertype, nBranch, nProduct, nPolicy, nCertif) Then
					lclsCertificat.sAut_guarval = sAut_guarval
					inspostVI732 = lclsCertificat.Update
					If inspostVI732 Then
						lclsPolicy_Win = New Policy_Win
						inspostVI732 = lclsPolicy_Win.Add_PolicyWin(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nUsercode, "VI732", "2")
					End If
				End If
		End Select
		
insPostVi732_err: 
		If Err.Number Then
			inspostVI732 = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsCertificat may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsCertificat = Nothing
		'UPGRADE_NOTE: Object lclsPolicy_Win may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsPolicy_Win = Nothing
	End Function
	
	'% Find: se realiza la lectura de la tabla
	Public Function Find(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nAge As Integer, ByVal nYear As Integer, ByVal nMonth As Integer) As Boolean
		Dim lrecreaGuarant_val As eRemoteDB.Execute
		
		On Error GoTo Find_Err
		
		lrecreaGuarant_val = New eRemoteDB.Execute
		
		With lrecreaGuarant_val
			.StoredProcedure = "reaGuarant_val"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAge", nAge, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nYear", nYear, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nMonth", nMonth, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				nYear = .FieldToClass("nYear")
				nAge = .FieldToClass("nAge")
				nMonth = .FieldToClass("nMonth")
				nPro_year = .FieldToClass("nPro_year")
				nDefamount = .FieldToClass("nDefamount")
				nResc_val = .FieldToClass("nResc_val")
				nSald_val = .FieldToClass("nSald_val")
				nSaldvalkm = .FieldToClass("nSaldvalkm")
				nDeferred = .FieldToClass("nDeferred")
				nSal_tax = .FieldToClass("nSal_tax")
				nPeriod_cov = .FieldToClass("nPeriod_cov")
				Find = True
				.RCloseRec()
			End If
		End With
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecreaGuarant_val may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaGuarant_val = Nothing
	End Function
	
	'% DeleteAll: se eliminan todos los registros asociados a la póliza
	Public Function DeleteAll(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date) As Boolean
		Dim lclsRemote As eRemoteDB.Execute
		
		On Error GoTo DeleteAll_err
		
		lclsRemote = New eRemoteDB.Execute
		
		With lclsRemote
			.StoredProcedure = "delGuarant_val_policy"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			DeleteAll = .Run(False)
		End With
		
DeleteAll_err: 
		If Err.Number Then
			DeleteAll = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsRemote may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsRemote = Nothing
	End Function
	
	'% inscalGuarant_val: se ejecuta el cálculo de los valores garantizados
	Private Function inscalGuarant_val(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As String, ByVal nUsercode As Integer) As Boolean
		Dim linsrutinguaran_val As eRemoteDB.Execute
		
		On Error GoTo inscalGuarant_val_err
		
		linsrutinguaran_val = New eRemoteDB.Execute
		
		With linsrutinguaran_val
			.StoredProcedure = "insroutinguaran_val"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			inscalGuarant_val = .Run(False)
		End With
		
inscalGuarant_val_err: 
		If Err.Number Then
			inscalGuarant_val = False
		End If
		'UPGRADE_NOTE: Object linsrutinguaran_val may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		linsrutinguaran_val = Nothing
	End Function
	
	'% insvalVIC732: se realizan las validaciones de la página
	Public Function insvalVIC732(ByVal sCodispl As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date) As String
		Dim lclsErrors As eFunctions.Errors
		Dim lclsPolicy As Policy
		Dim lclsCertificat As Certificat
		Dim lblnValid As Boolean
		
		On Error GoTo insvalVIC732_Err
		
		lclsErrors = New eFunctions.Errors
		lclsPolicy = New Policy
		lclsCertificat = New Certificat
		
		lblnValid = True
		With lclsErrors
			'+ El ramo debe estar lleno
			If nBranch = eRemoteDB.Constants.intNull Then
				Call .ErrorMessage(sCodispl, 1012,  , eFunctions.Errors.TextAlign.LeftAling, "Ramo:")
				lblnValid = False
			End If
			
			'+ El producto debe estar lleno
			If nProduct = eRemoteDB.Constants.intNull Then
				Call .ErrorMessage(sCodispl, 1012,  , eFunctions.Errors.TextAlign.LeftAling, "Producto:")
				lblnValid = False
			End If
			
			'+ La póliza debe estar llena
			If nPolicy = eRemoteDB.Constants.intNull Then
				Call .ErrorMessage(sCodispl, 1012,  , eFunctions.Errors.TextAlign.LeftAling, "Póliza:")
			Else
				If lblnValid Then
					If lclsPolicy.Find("2", nBranch, nProduct, nPolicy) Then
						If nCertif = eRemoteDB.Constants.intNull Then
							If lclsPolicy.sPolitype = "2" Then
								'+ Si se trata de una póliza colectiva, el certificado debe estar lleno
								Call .ErrorMessage(sCodispl, 1012,  , eFunctions.Errors.TextAlign.LeftAling, "Certificado:")
							End If
						Else
							If lclsCertificat.Find("2", nBranch, nProduct, nPolicy, nCertif) Then
								'+ El certificado debe tener estado válido
								If lclsCertificat.sStatusva = "3" Or lclsCertificat.sStatusva = "2" Then
									Call .ErrorMessage(sCodispl, 3883)
								End If
							Else
								'+ Debe existir el certificado
								Call .ErrorMessage(sCodispl, 13908)
							End If
						End If
						
						'+ La póliza debe tener estado válido
						If lclsPolicy.sStatus_pol = "3" Or lclsPolicy.sStatus_pol = "2" Then
							Call .ErrorMessage(sCodispl, 3882)
						End If
					Else
						'+ La póliza debe existir en el sistema
						Call .ErrorMessage(sCodispl, 3001)
					End If
				End If
			End If
			
			'+ La fecha debe estar llena
			If dEffecdate = eRemoteDB.Constants.dtmNull Then
				Call .ErrorMessage(sCodispl, 1012,  , eFunctions.Errors.TextAlign.LeftAling, "Fecha de efecto:")
			End If
			
			insvalVIC732 = .Confirm
		End With
		
insvalVIC732_Err: 
		If Err.Number Then
			insvalVIC732 = "insvalVIC732: " & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		'UPGRADE_NOTE: Object lclsPolicy may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsPolicy = Nothing
		'UPGRADE_NOTE: Object lclsCertificat may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsCertificat = Nothing
	End Function
	
	'% inspreVIC732: se controla el acceso a la zona repetitiva de la página
	Public Sub inspreVIC732(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date)
		Dim lclsCertificat As Certificat
		Dim lclsCurren_pol As Curren_pol
		
		lclsCertificat = New Certificat
		lclsCurren_pol = New Curren_pol
		
		nCertif = IIf(nCertif = eRemoteDB.Constants.intNull, 0, nCertif)
		With Me
			If lclsCertificat.Find(sCertype, nBranch, nProduct, nPolicy, nCertif) Then
				.sAut_guarval = lclsCertificat.sAut_guarval
			End If
			If .sAut_guarval = String.Empty Then
				'+ Se asigna el cálculo autómatico por defecto
				.sAut_guarval = "1"
			End If
			With lclsCurren_pol
				'+ Se busca la moneda de la póliza
				If lclsCurren_pol.Find(nPolicy, nBranch, nProduct, sCertype, nCertif, dEffecdate) Then
					.nCurrency = 0
					If lclsCurren_pol.IsLocal Then
						Me.nCurrency = 1
					Else
						Call lclsCurren_pol.Val_Curren_pol(0)
					End If
					Me.nCurrency = lclsCurren_pol.nCurrency
				End If
			End With
			
			Call mcolGuarant_val.Find(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, .nCurrency)
		End With
		
		'UPGRADE_NOTE: Object lclsCertificat may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsCertificat = Nothing
		'UPGRADE_NOTE: Object lclsCurren_pol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsCurren_pol = Nothing
	End Sub
	
	'* Class_Initialize: se controla el acceso a la clase
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		mcolGuarant_val = New Guarant_vals
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'* Class_Terminate: se controla el acceso a la clase
	'UPGRADE_NOTE: Class_Terminate was upgraded to Class_Terminate_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Terminate_Renamed()
		'UPGRADE_NOTE: Object mcolGuarant_val may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mcolGuarant_val = Nothing
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
End Class






