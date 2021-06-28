Option Strict Off
Option Explicit On
Public Class Saapv_funds_pol
	
	'+ Column_Name
	'------------------------------
	Public nCod_saapv As Double
	Public nFunds As Integer
	Public dEffecdate As Date
	Public nOrigin As Integer
	Public sCertype As String
	Public nBranch As Integer
	Public nProduct As Integer
	Public nPolicy As Double
	Public nCertif As Double
	Public dNulldate As Date
	Public nParticip As Double
	Public nUsercode As Integer
	Public sReaddress As String
	Public nQuan_avail As Double
	Public sActivfound As String
	Public nIntproy As Double
	Public nIntproyvar As Double
	Public sSel As String
	Public sDescript As String
	Public nPartic_min As Double
	Public sDesOrigin As String
	Public nSelected As Integer
	Public sGuarantee As String
	Public nBuy_cost As Double
	Public nSell_cost As Double
	Public nInstitution As Integer
	
	Public Function Delete() As Boolean
		Dim lrecdelSaapv_funds_pol As eRemoteDB.Execute
		
		On Error GoTo ErrorHandler
		lrecdelSaapv_funds_pol = New eRemoteDB.Execute
		
		Delete = True
		
		With lrecdelSaapv_funds_pol
			.StoredProcedure = "insVI7501_G_pkg.delSaapv_funds_pol"
			
			.Parameters.Add("nCod_saapv", nCod_saapv, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nFunds", nFunds, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOrigin", nOrigin, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInstitution", nInstitution, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			Delete = .Run(False)
		End With
		
		lrecdelSaapv_funds_pol = Nothing
		
		Exit Function
ErrorHandler: 
		lrecdelSaapv_funds_pol = Nothing
		
		Delete = False
	End Function
	
	Public Function Update() As Boolean
		Dim lrecupdSaapv_funds_pol As eRemoteDB.Execute
		
		On Error GoTo ErrorHandler
		lrecupdSaapv_funds_pol = New eRemoteDB.Execute
		
		Update = True
		
		With lrecupdSaapv_funds_pol
			.StoredProcedure = "insVI7501_G_pkg.updSaapv_funds_pol"
			
			.Parameters.Add("nCod_saapv", nCod_saapv, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nFunds", nFunds, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOrigin", nOrigin, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nParticip", nParticip, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nQuan_avail", nQuan_avail, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sActivfound", sActivfound, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nIntproy", nIntproy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nIntproyvar", nIntproyvar, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sSel", sSel, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInstitution", nInstitution, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			Update = .Run(False)
		End With
		
		lrecupdSaapv_funds_pol = Nothing
		
		Exit Function
ErrorHandler: 
		lrecupdSaapv_funds_pol = Nothing
		
		Update = False
	End Function
	
	Public Function insValVI7501_G(ByVal sCodispl As String, ByVal nCod_saapv As Double, ByVal nOrigin As Integer, ByVal dEffecdate As Date, ByVal sSelected As String, Optional ByVal sWindowType As String = "", Optional ByVal nFunds As Integer = 0, Optional ByVal nParticip As Double = 0, Optional ByVal sCertype As String = "", Optional ByVal nBranch As Integer = 0, Optional ByVal nProduct As Integer = 0, Optional ByVal nPolicy As Double = 0, Optional ByVal nCertif As Double = 0, Optional ByVal sRedirection As String = "", Optional ByVal sGuarantee As String = "", Optional ByVal nInstitution As Integer = 0) As String
		Dim lblnValVI7501_G As Boolean
		Dim lclsProduct As Object
		Dim lclsFunds As Object
		Dim lclsErrors As Object
		Dim lclsvalfield As Object
		Dim lcolFundss As Object
		Dim lintParticip As Integer
		Dim lintReaddress As Integer
		Dim lbnlParticip As Boolean
		Dim lblnPartic_min As Boolean
		Dim lcolSaapv_funds_pols As Saapv_funds_pols
		Dim lclsSaapv_funds_pol As Saapv_funds_pol
		
		On Error GoTo ErrorHandler
		
		lclsProduct = eRemoteDB.NetHelper.CreateClassInstance("eProduct.Product")
		lclsFunds = eRemoteDB.NetHelper.CreateClassInstance("ePolicy.Funds")
		lclsErrors = eRemoteDB.NetHelper.CreateClassInstance("eFunctions.Errors")
		lclsvalfield = eRemoteDB.NetHelper.CreateClassInstance("eFunctions.valField")
		lcolFundss = eRemoteDB.NetHelper.CreateClassInstance("ePolicy.Fundss")
		lclsSaapv_funds_pol = New Saapv_funds_pol
		lcolSaapv_funds_pols = New Saapv_funds_pols
		
		lclsvalfield.objErr = lclsErrors
		
		lintParticip = 0
		lintReaddress = 0
		lblnValVI7501_G = True
		
		If sWindowType = "PopUp" Then
			
			'**+ Validacion para los fondos de garantia
			If sGuarantee = "1" Then
				Call lclsErrors.ErrorMessage(sCodispl, 750141)
				lblnValVI7501_G = False
			End If
			
			'**+ Validations of the field " % Particip".
			'+ Validación del campo " % Participación".
			
			'**+ If the fund is selected the % of participation must be entered
			'+ Si el fondo está seleccionado la partcipación debe estar llena
			
			If nParticip = eRemoteDB.Constants.intNull Then
				Call lclsErrors.ErrorMessage(sCodispl, 3402)
				lblnValVI7501_G = False
			Else
				With lclsvalfield
					.ErrEmpty = 1937
					.Min = 1
					.Max = 100
					.EqualMin = True
					.EqualMax = True
					.Descript = "Participación"
				End With
				
				If Not lclsvalfield.ValNumber(nParticip) Then
					lblnValVI7501_G = False
				Else
					If lclsProduct.FindProduct_li(nBranch, nProduct, dEffecdate) Then
						If lcolFundss.Find(nBranch, nProduct, dEffecdate) Then
							For	Each lclsFunds In lcolFundss
								With lclsFunds
									If .nFunds = nFunds Then
										If .nParticip <> nParticip Then
											If lclsProduct.sUlfchani = "2" Then
												lbnlParticip = True
												lblnValVI7501_G = False
											End If
										End If
										
										If .nPartic_min > nParticip Then
											lblnPartic_min = True
											lblnValVI7501_G = False
										End If
									End If
								End With
							Next lclsFunds
						End If
						
						If Not lbnlParticip Then
							If sRedirection = "1" Then
								If lcolSaapv_funds_pols.Find(nCod_saapv, nOrigin, dEffecdate, nBranch, nProduct, nPolicy, nCertif, "2", nUsercode, nInstitution) Then
									For	Each lclsSaapv_funds_pol In lcolSaapv_funds_pols
										With lclsSaapv_funds_pol
											If .nCod_saapv = nCod_saapv And .nFunds = nFunds And .nOrigin = nOrigin Then
												If .nParticip <> nParticip And .sSel = "1" Then
													lintReaddress = lintReaddress + 1
												End If
												
												If lclsProduct.nUlrmaxqu < lintReaddress Then
													'**+ More redirection permitted by the policy in the product designer must not be permitted
													'+ No debe aceptar más redirecciones de las permitidas por póliza en el diseñador de productos
													
													Call lclsErrors.ErrorMessage(sCodispl, 17008)
													
													lblnValVI7501_G = True
												End If
											End If
										End With
									Next lclsSaapv_funds_pol
								End If
							End If
						End If
					End If
				End If
			End If
			
			'**+ The percentage of participation must not be inferior to the minimum defined
			'**+ in the product designer
			'+ El porcentaje de participación no puede ser menor que el mínimo definido en
			'+ el diseñador de productos
			
			If lblnPartic_min Then
				Call lclsErrors.ErrorMessage(sCodispl, 17004)
			End If
			
			'**+ If in the product designer was specify that fund must not be modified
			'+ Si en el diseñador de productos se especificó que no se pueden cambiar
			'+ los fondos, no se puede cambiar este
			
			If lbnlParticip Then
				Call lclsErrors.ErrorMessage(sCodispl, 11128)
			End If
		Else
			If sSelected = String.Empty Then
				Call lclsErrors.ErrorMessage(sCodispl, 11084)
			Else
				If lcolSaapv_funds_pols.Find(nCod_saapv, nOrigin, dEffecdate, nBranch, nProduct, nPolicy, nCertif, "2",  , nInstitution) Then
					For	Each lclsSaapv_funds_pol In lcolSaapv_funds_pols
						With lclsSaapv_funds_pol
							If .sSel = "1" Then
								lintParticip = lintParticip + .nParticip
							End If
						End With
					Next lclsSaapv_funds_pol
					
					If lintParticip <> 100 Then
						Call lclsErrors.ErrorMessage(sCodispl, 11130)
					End If
				End If
			End If
		End If
		
		insValVI7501_G = lclsErrors.Confirm
		
		lclsProduct = Nothing
		lclsSaapv_funds_pol = Nothing
		lclsErrors = Nothing
		lclsvalfield = Nothing
		lcolFundss = Nothing
		lcolSaapv_funds_pols = Nothing
		
		Exit Function
ErrorHandler: 
		lclsProduct = Nothing
		lclsSaapv_funds_pol = Nothing
		lclsErrors = Nothing
		lclsvalfield = Nothing
		lcolFundss = Nothing
		lcolSaapv_funds_pols = Nothing
	End Function
	
	Public Function insPostVI7501_G(ByVal sAction As String, ByVal nCod_saapv As Double, ByVal nFunds As Integer, ByVal nOrigin As Integer, ByVal dEffecdate As Date, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nParticip As Integer, ByVal nUsercode As Integer, ByVal nQuan_avail As Double, ByVal sActivfound As String, ByVal nIntproy As Double, ByVal nIntproyvar As Double, ByVal sSel As String, ByVal nInstitution As Integer) As Boolean
		Dim lcolSaapv_funds_pols As Saapv_funds_pols
		
		On Error GoTo ErrorHandler
		
		lcolSaapv_funds_pols = New Saapv_funds_pols
		
		insPostVI7501_G = True
		
		With Me
			.nCod_saapv = nCod_saapv
			.nFunds = nFunds
			.nOrigin = nOrigin
			.dEffecdate = dEffecdate
			.sCertype = sCertype
			.nBranch = nBranch
			.nProduct = nProduct
			.nPolicy = nPolicy
			.nCertif = nCertif
			.nParticip = nParticip
			.nUsercode = nUsercode
			.nQuan_avail = nQuan_avail
			.sActivfound = sActivfound
			.nIntproy = nIntproy
			.nIntproyvar = nIntproyvar
			.sSel = sSel
			.nInstitution = nInstitution
			
			If sAction <> "Del" Then
				.Update()
			Else
				.Delete()
			End If
		End With
		
		lcolSaapv_funds_pols = Nothing
		
		Exit Function
ErrorHandler: 
		lcolSaapv_funds_pols = Nothing
		
		insPostVI7501_G = False
	End Function
End Class






