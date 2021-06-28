Option Strict Off
Option Explicit On
Public Class Saapv
	'%-------------------------------------------------------%'
	'% $Workfile:: Saapv.cls                           $%'
	'% $Author:: Nvaplat53                                  $%'
	'% $Date:: 8/09/04 4:17p                                $%'
	'% $Revision:: 77                                       $%'
	'%-------------------------------------------------------%'
	
	'+ Propiedades según la tabla en el sistema el 28/01/2000
	'+ El campo llave corresponde a nIntermed.
	
	'+
	'+         Property                Type         DBType   Size Scale  Prec  Null
	'+-----------------------------------------------------------------------------
	
	Public nCod_saapv As Double ' NUMBER     22   2     4    S
	Public nType_saapv As Integer ' NUMBER     22   0     5    N
	Public dissue_dat As Date ' NUMBER     22   0     10   N
	Public dLimitDate As Date ' NUMBER     22   0     10   N
	Public nstatus_saapv As Integer ' NUMBER     22   0     5    N
	Public nInstitution As Integer ' CHAR       1    0     0    N
	Public ntype_ameapv As Integer ' NUMBER     22   0     5    N
	Public WithInformation As String
	Public sCertype As String ' CHAR       1    0     0    N
	Public nBranch As Integer ' NUMBER     22   0     5    N
	Public nProduct As Integer ' NUMBER     22   0     5    N
	Public nPolicy As Double ' NUMBER     22   0     10   N
	Public sIndContributios_Afp As String
	Public sIndContributios_Ips As String
	Public nType_employee As Integer
	Public nInd_health As Integer
	Public nWay_pay As Integer
	Public nYearMonthDesc As Double
	Public sClient As String
	Public sClient_employer As String
	Public sLegalname As String
	Public sDescAdd As String
	Public nMunicipality As Integer
	Public nLocal As Integer
	Public nProvince As Integer
	Public sSe_mail As String
	Public sPhone_pa As String
	Public sPhone_co As String
	Public sPhone_ce As String
	Public sRrhh_name As String
	Public sRrhh_email As String
	Public sRrhh_phone As String
	Public dRecepDat As Date
	Public dBirthDat As Date
	Public sSexclien As String
	Public nCivilSta As Integer
	Public nSpeciality As Integer
	Public nNationality As Integer
	Public nTax_regime As Integer
	Public nAmount As Double
	Public nAmount_uf As Double
	Public nAmount_pct As Double
	Public nInd_lumpsum As Integer
	Public dEnddate As Date
	Public nOrigin As Integer
	Public nInstitut_origin As Integer
	Public nPolicy2 As Double ' NUMBER     22   0     10   N
	Public Scertype2 As String
	Public nBranch2 As Double
	Public nProduct2 As Double
	
	Public Enum etypeImageSequence
		eEmpty = 0
		eOK = 1
		eRequired = 2
		eDeniedS = 3
		eDeniedOK = 4
		eDeniedReq = 5
	End Enum
	
	'% insValVI7501: Esta función se encarga de validar los datos introducidos en la ventana VI7501
	Public Function insValVI7501(ByVal sAction As String, ByVal nCod_saapv As Double, ByVal dissue_dat As Date, ByVal nType_saapv As Integer, ByVal nstatus_saapv As Integer, ByVal nInstitution As Integer, ByVal ntype_ameapv As Double, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nSequence As Short) As String
		Dim lrecinsValVI7501 As eRemoteDB.Execute
		Dim lclsErrors As Object
        Dim lstrErrors As String = ""

        On Error GoTo insValVI7501_Err
        lrecinsValVI7501 = New eRemoteDB.Execute
		lclsErrors = eRemoteDB.NetHelper.CreateClassInstance("eFunctions.Errors")
		
		
		With lrecinsValVI7501
			.StoredProcedure = "insVi7501pkg.insvalVi7501"
			.Parameters.Add("sAction", sAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("ncod_saapv", nCod_saapv, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dissue_dat", dissue_dat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("ntype_saapv", nType_saapv, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nstatus_saapv", nstatus_saapv, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("ninstitution", nInstitution, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("ntype_ameapv", ntype_ameapv, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nSequence", nSequence, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("Arrayerrors", String.Empty, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 4000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				lstrErrors = .Parameters("Arrayerrors").Value
			End If
		End With
		
		'+Validaciones masivas
		Call lclsErrors.ErrorMessage("VI7501",  ,  ,  ,  ,  , lstrErrors)
		
		insValVI7501 = lclsErrors.Confirm
		
insValVI7501_Err: 
		If Err.Number Then
			insValVI7501 = "insValVI7501: " & Err.Description
		End If
		
		lrecinsValVI7501 = Nothing
		lclsErrors = Nothing
		
		On Error GoTo 0
	End Function
	
	'% insValVI7501_A: Esta función se encarga de validar los datos introducidos en la ventana VI7501_A
	Public Function insValVI7501_A(ByVal sAction As String, ByVal nCod_saapv As Double, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal sClient As String, ByVal sDescAdd As String, ByVal sSexclien As String, ByVal dBirthDat As Date, ByVal nNationality As Integer, ByVal nCilSta As Integer, ByVal nSpeciality As Integer) As String
		Dim lrecinsValVI7501_A As eRemoteDB.Execute
		Dim lclsErrors As Object
        Dim lstrErrors As String = ""

        On Error GoTo insValVI7501A_Err
		lrecinsValVI7501_A = New eRemoteDB.Execute
		lclsErrors = eRemoteDB.NetHelper.CreateClassInstance("eFunctions.Errors")
		
		
		With lrecinsValVI7501_A
			.StoredProcedure = "INSVALVI7501_A"
			.Parameters.Add("ncod_saapv", nCod_saapv, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDescAdd", sDescAdd, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 60, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sSexclien", sSexclien, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 2, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dBirthDat", dBirthDat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nNationality", nNationality, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCilSta", nCilSta, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nSpeciality", nSpeciality, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("Arrayerrors", String.Empty, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 4000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				lstrErrors = .Parameters("Arrayerrors").Value
			End If
		End With
		
		'+Validaciones masivas
		Call lclsErrors.ErrorMessage("VI7501_A",  ,  ,  ,  ,  , lstrErrors)
		
		insValVI7501_A = lclsErrors.Confirm
		
insValVI7501A_Err: 
		If Err.Number Then
			insValVI7501_A = "insValVI7501_A: " & Err.Description
		End If
		
		lrecinsValVI7501_A = Nothing
		lclsErrors = Nothing
		
		On Error GoTo 0
	End Function
	
	'% insValVI7501_B: Esta función se encarga de validar los datos introducidos en la ventana VI7501_B
	Public Function insValVI7501_B(ByVal sAction As String, ByVal nCod_saapv As Double, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal sClient As String, ByVal sRrhh_name As String, ByVal sRrhh_email As String, ByVal sRrhh_phone As String, ByVal dRecepDat As Date, ByVal sDescAdd As String, ByVal nType_saapv As Integer, ByVal nInstitution As Integer) As String
		Dim lrecinsValVI7501_B As eRemoteDB.Execute
		Dim lclsErrors As Object
        Dim lstrErrors As String = ""

        On Error GoTo insValVI7501B_Err
		lrecinsValVI7501_B = New eRemoteDB.Execute
		lclsErrors = eRemoteDB.NetHelper.CreateClassInstance("eFunctions.Errors")
		
		
		With lrecinsValVI7501_B
			.StoredProcedure = "INSVALVI7501_B"
			.Parameters.Add("ncod_saapv", nCod_saapv, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sRrhh_name", sRrhh_name, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 60, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sRrhh_email", sRrhh_email, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 60, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sRrhh_phone", sRrhh_phone, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 16, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dRecepDat", dRecepDat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDescAdd", sDescAdd, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 60, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nType_saapv", nType_saapv, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInstitution", nInstitution, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("Arrayerrors", String.Empty, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 4000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				lstrErrors = .Parameters("Arrayerrors").Value
			End If
		End With
		
		'+Validaciones masivas
		Call lclsErrors.ErrorMessage("VI7501_B",  ,  ,  ,  ,  , lstrErrors)
		
		insValVI7501_B = lclsErrors.Confirm
		
insValVI7501B_Err: 
		If Err.Number Then
			insValVI7501_B = "insValVI7501_B: " & Err.Description
		End If
		
		lrecinsValVI7501_B = Nothing
		lclsErrors = Nothing
		
		On Error GoTo 0
	End Function
	
	'% insPosVI7501: Esta función se encarga de actualizar la tabla
	Public Function insPosVI7501(ByVal sAction As String, ByVal nCod_saapv As Double, ByVal dissue_dat As Date, ByVal dLimitDate As Date, ByVal nType_saapv As Integer, ByVal nstatus_saapv As Integer, ByVal nInstitution As Integer, ByVal ntype_ameapv As Double, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nUsercode As Double) As Boolean
		Dim lrecinsPostVI7501 As eRemoteDB.Execute
		
		
		On Error GoTo insPostVI7501_Err
		lrecinsPostVI7501 = New eRemoteDB.Execute
		
		
		With lrecinsPostVI7501
			
			.StoredProcedure = "insVi7501pkg.inspostVi7501"
			.Parameters.Add("sAction", sAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("ncod_saapv", nCod_saapv, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dissue_dat", dissue_dat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dLimitDate", dLimitDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("ntype_saapv", nType_saapv, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nstatus_saapv", nstatus_saapv, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("ninstitution", nInstitution, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("ntype_ameapv", ntype_ameapv, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			insPosVI7501 = .Run(False)
		End With
		
insPostVI7501_Err: 
		If Err.Number Then
			insPosVI7501 = False
		End If
		
		lrecinsPostVI7501 = Nothing
		
		On Error GoTo 0
	End Function
	
	
	
	
	'%LoadTabs: arma la secuencia en código HTML
	Public Function LoadTabs(ByVal nCod_saapv As Double, ByVal nAction As Integer, ByVal sUserSchema As String, ByVal nUsercode As Integer, ByVal nInstitution As Integer) As String
		Const CN_WINDOWS As String = "VI7501_AVI7501_BVI7501_CVI7501_DVI7501_EVI7501_FVI7501_G"
		Dim lrecWindows As eRemoteDB.Query
		Dim mintPageImage As etypeImageSequence
		Dim lintCountWindows As Integer

        Dim lstrCodisp As String = ""
        Dim lstrCodispl As String
        Dim lstrShort_desc As String = ""
        Dim lblnContent As Boolean
		Dim lblnRequired As Boolean
		
		Dim lstrHTMLCode As String
		
		Dim lclsSequence As Object
		
		On Error GoTo LoadTabs_err
		
		lclsSequence = eRemoteDB.NetHelper.CreateClassInstance("eFunctions.Sequence")
		lrecWindows = New eRemoteDB.Query
		
		lstrHTMLCode = String.Empty
		
		Call ValRequired(nCod_saapv, nInstitution)
		Call Find(nCod_saapv, nInstitution)
		
		lstrHTMLCode = lclsSequence.makeTable
		lintCountWindows = 1
		lstrCodispl = Mid(CN_WINDOWS, lintCountWindows, 8)
		Do While Trim(lstrCodispl) <> String.Empty
			
			lblnRequired = False
			
			'+ Se asignan los valores a las variables de contenido
			If InStr(1, WithInformation, Trim(lstrCodispl)) <> 0 Then
				lblnContent = True
				If InStr(1, WithInformation, Trim(lstrCodispl) & "|2") <> 0 Then
					lblnContent = True
				Else
					lblnRequired = True
				End If
				
			Else
				lblnContent = False
			End If
			
			'+ Se asignan los valores a las variables de descripcion
			
			If lrecWindows.OpenQuery("windows", "sCodisp, sShort_des", "scodispl='" & Trim(lstrCodispl) & "'") Then
				lstrCodisp = lrecWindows.FieldToClass("sCodisp")
				lstrShort_desc = lrecWindows.FieldToClass("sShort_des")
				lrecWindows.CloseQuery()
			End If
			
			
			'+ Se busca la imagen a colocar en los links
			If Not lblnContent Then
				mintPageImage = etypeImageSequence.eEmpty
			Else
				If Not lblnRequired Then
					mintPageImage = etypeImageSequence.eOK
				Else
					mintPageImage = etypeImageSequence.eRequired
				End If
			End If
			
			lstrHTMLCode = lstrHTMLCode & lclsSequence.makeRow(lstrCodisp, lstrCodispl, nAction, lstrShort_desc, mintPageImage)
			'+ Se mueve al siguiente registro encontrado
			lintCountWindows = lintCountWindows + 8
			lstrCodispl = Mid(CN_WINDOWS, lintCountWindows, 8)
		Loop 
		
		lstrHTMLCode = lstrHTMLCode & lclsSequence.closeTable()
		
		LoadTabs = lstrHTMLCode
		
		
		Exit Function
LoadTabs_err: 
		LoadTabs = "LoadTabs: " & Err.Description
		lclsSequence = Nothing
		lrecWindows = Nothing
		
		On Error GoTo 0
	End Function
	
	Private Sub Class_Initialize_Renamed()
		
		nCod_saapv = eRemoteDB.Constants.intNull
		nType_saapv = eRemoteDB.Constants.intNull
		dissue_dat = eRemoteDB.Constants.dtmNull
		nstatus_saapv = eRemoteDB.Constants.intNull
		ntype_ameapv = eRemoteDB.Constants.intNull
		nInstitution = eRemoteDB.Constants.intNull
		ntype_ameapv = eRemoteDB.Constants.intNull
		WithInformation = String.Empty
		sCertype = String.Empty
		nBranch = eRemoteDB.Constants.intNull
		nProduct = eRemoteDB.Constants.intNull
		nPolicy = eRemoteDB.Constants.intNull
		sIndContributios_Afp = String.Empty
		sIndContributios_Ips = String.Empty
		nType_employee = eRemoteDB.Constants.intNull
		nInd_health = eRemoteDB.Constants.intNull
		nWay_pay = eRemoteDB.Constants.intNull
		nYearMonthDesc = eRemoteDB.Constants.intNull
		sClient = String.Empty
		sClient_employer = String.Empty
		sLegalname = String.Empty
		sDescAdd = String.Empty
		nMunicipality = eRemoteDB.Constants.intNull
		nLocal = eRemoteDB.Constants.intNull
		nProvince = eRemoteDB.Constants.intNull
		sSe_mail = String.Empty
		sPhone_pa = String.Empty
		sPhone_co = String.Empty
		sPhone_ce = String.Empty
		sRrhh_name = String.Empty
		sRrhh_email = String.Empty
		sRrhh_phone = String.Empty
		dRecepDat = eRemoteDB.Constants.dtmNull
		dBirthDat = eRemoteDB.Constants.dtmNull
		sSexclien = String.Empty
		nCivilSta = eRemoteDB.Constants.intNull
		nSpeciality = eRemoteDB.Constants.intNull
		nNationality = eRemoteDB.Constants.intNull
		nTax_regime = eRemoteDB.Constants.intNull
		nAmount = eRemoteDB.Constants.intNull
		nAmount_uf = eRemoteDB.Constants.intNull
		nAmount_pct = eRemoteDB.Constants.intNull
		nInd_lumpsum = eRemoteDB.Constants.intNull
		dEnddate = eRemoteDB.Constants.dtmNull
		
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	Public Function ValRequired(ByVal nCod_saapv As Double, ByVal nInstitution As Integer) As Boolean
		Dim lrecinsValRequired_ncod_saapv As eRemoteDB.Execute
		lrecinsValRequired_ncod_saapv = New eRemoteDB.Execute
		
		On Error GoTo ValRequired_Err
		'Definición de parámetros para stored procedure 'insudb.insValRequired_Interm'
		'Información leída el 05/02/2001 16.21.47
		
		With lrecinsValRequired_ncod_saapv
			.StoredProcedure = "insVi7501pkg.Find_ValRequired"
			.Parameters.Add("ncod_saapv", nCod_saapv, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInstitution", nInstitution, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(True) Then
				Me.WithInformation = .FieldToClass("WithInformation")
				
				ValRequired = True
				.RCloseRec()
			Else
				ValRequired = False
			End If
		End With
		
		
		
ValRequired_Err: 
		If Err.Number Then
			ValRequired = False
		End If
		lrecinsValRequired_ncod_saapv = Nothing
		On Error GoTo 0
	End Function
	
	Public Function Find(ByVal nCod_saapv As Double, ByVal nInstitution As Integer) As Boolean
		Dim lrecinsValRequired_ncod_saapv As eRemoteDB.Execute
		lrecinsValRequired_ncod_saapv = New eRemoteDB.Execute
		
		On Error GoTo Find_Err
		'Definición de parámetros para stored procedure 'insudb.insValRequired_Interm'
		'Información leída el 05/02/2001 16.21.47
		
		With lrecinsValRequired_ncod_saapv
			.StoredProcedure = "insVi7501pkg.Find"
			.Parameters.Add("ncod_saapv", nCod_saapv, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInstitution", nInstitution, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(True) Then
				Me.nCod_saapv = nCod_saapv
				Me.nType_saapv = .FieldToClass("ntype_saapv")
				Me.dissue_dat = .FieldToClass("dissue_dat")
				Me.dLimitDate = .FieldToClass("dLimitDate")
				Me.nstatus_saapv = .FieldToClass("nstatus_saapv")
				Me.ntype_ameapv = .FieldToClass("ntype_ameapv")
				Me.nInstitution = .FieldToClass("ninstitution")
				Me.sCertype = .FieldToClass("sCertype")
				Me.nBranch = .FieldToClass("nBranch")
				Me.nProduct = .FieldToClass("nProduct")
				Me.nPolicy = .FieldToClass("nPolicy")
				Me.sIndContributios_Afp = .FieldToClass("sIndContributios_Afp")
				Me.sIndContributios_Ips = .FieldToClass("sIndContributios_Ips")
				Me.nType_employee = .FieldToClass("nType_employee")
				Me.nInd_health = .FieldToClass("nInd_health")
				Me.nWay_pay = .FieldToClass("nWay_pay")
				Me.nYearMonthDesc = .FieldToClass("nYearMonthDesc")
				Me.nTax_regime = .FieldToClass("nTax_regime")
				Me.nAmount = .FieldToClass("nAmount")
				Me.nAmount_uf = .FieldToClass("nAmount_uf")
				Me.nAmount_pct = .FieldToClass("nAmount_pct")
				Me.nInd_lumpsum = .FieldToClass("nInd_lumpsum")
				Me.dEnddate = .FieldToClass("dEnddate")
				Me.nOrigin = .FieldToClass("nOrigin")
				Me.nInstitut_origin = .FieldToClass("nInstitut_origin")
				
				Find = True
				.RCloseRec()
			Else
				Find = False
			End If
		End With
		
		
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		lrecinsValRequired_ncod_saapv = Nothing
		On Error GoTo 0
	End Function
	
	
	Public Function Find_insure(ByVal nCod_saapv As Double, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal sClient As String, ByVal nInstitution As Integer) As Boolean
		Dim lrecinsValRequired_ncod_saapv As eRemoteDB.Execute
		lrecinsValRequired_ncod_saapv = New eRemoteDB.Execute
		
		On Error GoTo Find_Err
		'Definición de parámetros para stored procedure 'insudb.insValRequired_Interm'
		'Información leída el 05/02/2001 16.21.47
		
		With lrecinsValRequired_ncod_saapv
			.StoredProcedure = "REAVI7501_A"
			.Parameters.Add("ncod_saapv", nCod_saapv, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInstitution", nInstitution, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(True) Then
				Me.nCod_saapv = nCod_saapv
				Me.sClient = .FieldToClass("sClient")
				Me.dBirthDat = .FieldToClass("dBirthDat")
				Me.sSexclien = .FieldToClass("sSexclien")
				Me.nCivilSta = .FieldToClass("nCivilsta")
				Me.nSpeciality = .FieldToClass("nSpeciality")
				Me.nNationality = .FieldToClass("nNationality")
				Me.sDescAdd = .FieldToClass("sDescadd")
				Me.nMunicipality = .FieldToClass("nMunicipality")
				Me.nLocal = .FieldToClass("nLocal")
				Me.nProvince = .FieldToClass("nProvince")
				Me.sSe_mail = .FieldToClass("sSe_mail")
				Me.sPhone_pa = .FieldToClass("sPhone_pa")
				Me.sPhone_co = .FieldToClass("sPhone_co")
				Me.sPhone_ce = .FieldToClass("sPhone_ce")
				
				Find_insure = True
				.RCloseRec()
			Else
				Find_insure = False
			End If
		End With
		
		
		
Find_Err: 
		If Err.Number Then
			Find_insure = False
		End If
		lrecinsValRequired_ncod_saapv = Nothing
		On Error GoTo 0
	End Function
	
	Public Function Find_Employ(ByVal nCod_saapv As Double, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal sClient As String, ByVal nInstitution As Integer) As Boolean
		Dim lrecinsValRequired_ncod_saapv As eRemoteDB.Execute
		lrecinsValRequired_ncod_saapv = New eRemoteDB.Execute
		
		On Error GoTo Find_Err
		'Definición de parámetros para stored procedure 'insudb.insValRequired_Interm'
		'Información leída el 05/02/2001 16.21.47
		
		With lrecinsValRequired_ncod_saapv
			.StoredProcedure = "REAVI7501_B"
			.Parameters.Add("ncod_saapv", nCod_saapv, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInstitution", nInstitution, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(True) Then
				Me.nCod_saapv = nCod_saapv
				Me.sClient_employer = .FieldToClass("sClient_employer")
				Me.sLegalname = .FieldToClass("sLegalname")
				Me.sDescAdd = .FieldToClass("sDescadd")
				Me.nMunicipality = .FieldToClass("nMunicipality")
				Me.nLocal = .FieldToClass("nLocal")
				Me.nProvince = .FieldToClass("nProvince")
				Me.sRrhh_name = .FieldToClass("sRrhh_name")
				Me.sRrhh_email = .FieldToClass("sRrhh_email")
				Me.sRrhh_phone = .FieldToClass("sRrhh_phone")
				Me.dRecepDat = .FieldToClass("dRecepdat")
				
				Find_Employ = True
				.RCloseRec()
			Else
				Find_Employ = False
			End If
		End With
		
		
		
Find_Err: 
		If Err.Number Then
			Find_Employ = False
		End If
		lrecinsValRequired_ncod_saapv = Nothing
		On Error GoTo 0
	End Function
	
	
	'% insPosVI7501_C: Esta función se encarga de actualizar la tabla
	Public Function insPosVI7501_C(ByVal nCod_saapv As Double, ByVal sIndContributios_Afp As String, ByVal sIndContributios_Ips As String, ByVal nType_employee As Integer, ByVal nInd_health As Integer, ByVal nUsercode As Integer, ByVal nInstitution As Integer) As Boolean
		Dim lrecinsPostVI7501_C As eRemoteDB.Execute
		
		
		On Error GoTo insPostVI7501_C_Err
		lrecinsPostVI7501_C = New eRemoteDB.Execute
		
		
		With lrecinsPostVI7501_C
			
			.StoredProcedure = "insVi7501pkg.inspostVi7501_C"
			.Parameters.Add("ncod_saapv", nCod_saapv, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sIndContributios_Afp", sIndContributios_Afp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sIndContributios_Ips", sIndContributios_Ips, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nType_employee", nType_employee, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInd_health", nInd_health, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInstitution", nInstitution, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			insPosVI7501_C = .Run(False)
		End With
		
insPostVI7501_C_Err: 
		If Err.Number Then
			insPosVI7501_C = False
		End If
		
		lrecinsPostVI7501_C = Nothing
		
		On Error GoTo 0
	End Function
	
	'% insPosVI7501_A: Esta función se encarga de actualizar la tabla
	Public Function insPosVI7501_A(ByVal nCod_saapv As Double, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal sClient As String, ByVal nUsercode As Integer, ByVal nInstitution As Integer) As Boolean
		Dim lrecinsPostVI7501_A As eRemoteDB.Execute
		
		
		On Error GoTo insPostVI7501_A_Err
		lrecinsPostVI7501_A = New eRemoteDB.Execute
		
		
		With lrecinsPostVI7501_A
			
			.StoredProcedure = "INSPOSTVI7501_A"
			.Parameters.Add("ncod_saapv", nCod_saapv, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInstitution", nInstitution, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			insPosVI7501_A = .Run(False)
		End With
		
insPostVI7501_A_Err: 
		If Err.Number Then
			insPosVI7501_A = False
		End If
		
		lrecinsPostVI7501_A = Nothing
		
		On Error GoTo 0
	End Function
	'% insPosVI7501_B: Esta función se encarga de actualizar la tabla
	Public Function insPosVI7501_B(ByVal nCod_saapv As Double, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal sClient As String, ByVal sRrhh_name As String, ByVal sRrhh_email As String, ByVal sRrhh_phone As String, ByVal dRecepDat As Date, ByVal nUsercode As Integer, ByVal nInstitution As Integer) As Boolean
		Dim lrecinsPostVI7501_B As eRemoteDB.Execute
		
		
		On Error GoTo insPostVI7501_B_Err
		lrecinsPostVI7501_B = New eRemoteDB.Execute
		
		
		With lrecinsPostVI7501_B
			
			.StoredProcedure = "INSPOSTVI7501_B"
			.Parameters.Add("ncod_saapv", nCod_saapv, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sRrhh_name", sRrhh_name, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 60, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sRrhh_email", sRrhh_email, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 60, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sRrhh_phone", sRrhh_phone, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 16, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dRecepDat", dRecepDat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInstitution", nInstitution, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			insPosVI7501_B = .Run(False)
		End With
		
insPostVI7501_B_Err: 
		If Err.Number Then
			insPosVI7501_B = False
		End If
		
		lrecinsPostVI7501_B = Nothing
		
		On Error GoTo 0
	End Function
	
	'% insPosVI7501_E: Esta función se encarga de actualizar la tabla
	Public Function insPosVI7501_E(ByVal nCod_saapv As Double, ByVal nWay_pay As Integer, ByVal nYearMonthDesc As Double, ByVal nUsercode As Integer, ByVal nInstitution As Integer) As Boolean
		Dim lrecinsPostVI7501_E As eRemoteDB.Execute
		
		
		On Error GoTo insPostVI7501_E_Err
		lrecinsPostVI7501_E = New eRemoteDB.Execute
		
		
		With lrecinsPostVI7501_E
			
			.StoredProcedure = "insVi7501pkg.inspostVi7501_E"
			.Parameters.Add("ncod_saapv", nCod_saapv, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nWay_pay", nWay_pay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nYearMonthDesc", nYearMonthDesc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInstitution", nInstitution, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			insPosVI7501_E = .Run(False)
		End With
		
insPostVI7501_E_Err: 
		If Err.Number Then
			insPosVI7501_E = False
		End If
		
		lrecinsPostVI7501_E = Nothing
		
		On Error GoTo 0
	End Function
	
	'% insValVI7501_E: Esta función se encarga de validar los datos introducidos en la ventana VI7501_E
	Public Function insValVI7501_E(ByVal sAction As String, ByVal nWay_pay As Integer, ByVal nYearMonthDesc As Double) As String
		Dim lrecinsValVI7501_E As eRemoteDB.Execute
		Dim lclsErrors As Object
        Dim lstrErrors As String = ""

        On Error GoTo insValVI7501_E_Err
		lrecinsValVI7501_E = New eRemoteDB.Execute
		lclsErrors = eRemoteDB.NetHelper.CreateClassInstance("eFunctions.Errors")
		
		
		With lrecinsValVI7501_E
			.StoredProcedure = "insVi7501pkg.insvalVi7501_E"
			.Parameters.Add("sAction", sAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nWay_pay", nWay_pay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nYearMonthDesc", nYearMonthDesc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("Arrayerrors", String.Empty, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 4000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				lstrErrors = .Parameters("Arrayerrors").Value
			End If
		End With
		
		'+Validaciones masivas
		Call lclsErrors.ErrorMessage("VI7501_E",  ,  ,  ,  ,  , lstrErrors)
		
		insValVI7501_E = lclsErrors.Confirm
		
insValVI7501_E_Err: 
		If Err.Number Then
			insValVI7501_E = "insValVI7501_E: " & Err.Description
		End If
		
		lrecinsValVI7501_E = Nothing
		lclsErrors = Nothing
		
		On Error GoTo 0
	End Function
	
	
	'% insPosVI7501_D: Esta función se encarga de actualizar la tabla
	Public Function insPosVI7501_D(ByVal nCod_saapv As Double, ByVal nTax_regime As Integer, ByVal nAmount As Double, ByVal nAmount_uf As Double, ByVal nAmount_pct As Double, ByVal nInd_lumpsum As Integer, ByVal dEnddate As Date, ByVal nOrigin As Integer, ByVal nUsercode As Integer, ByVal nInstitution As Integer) As Boolean
		Dim lrecinsPostVI7501_D As eRemoteDB.Execute
		
		
		On Error GoTo insPostVI7501_D_Err
		lrecinsPostVI7501_D = New eRemoteDB.Execute
		
		
		With lrecinsPostVI7501_D
			
			.StoredProcedure = "insVi7501pkg.inspostVi7501_D"
			.Parameters.Add("ncod_saapv", nCod_saapv, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTax_regime", nTax_regime, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAmount", nAmount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAmount_uf", nAmount_uf, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAmount_pct", nAmount_pct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInd_lumpsum", nInd_lumpsum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEnddate", dEnddate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOrigin", nOrigin, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInstitution", nInstitution, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			insPosVI7501_D = .Run(False)
		End With
		
insPostVI7501_D_Err: 
		If Err.Number Then
			insPosVI7501_D = False
		End If
		
		lrecinsPostVI7501_D = Nothing
		
		On Error GoTo 0
	End Function
	
	'% insValVI7501_D: Esta función se encarga de validar los datos introducidos en la ventana VI7501_D
	Public Function insValVI7501_D(ByVal sAction As String, ByVal nTax_regime As Integer, ByVal nAmount As Double, ByVal nAmount_uf As Double, ByVal nAmount_pct As Double) As String
		Dim lrecinsValVI7501_D As eRemoteDB.Execute
		Dim lclsErrors As Object
        Dim lstrErrors As String = ""

        On Error GoTo insValVI7501_D_Err
		lrecinsValVI7501_D = New eRemoteDB.Execute
		lclsErrors = eRemoteDB.NetHelper.CreateClassInstance("eFunctions.Errors")
		
		
		With lrecinsValVI7501_D
			.StoredProcedure = "insVi7501pkg.insvalVi7501_D"
			.Parameters.Add("sAction", sAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTax_regime", nTax_regime, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAmount", nAmount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAmount_uf", nAmount_uf, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAmount_pct", nAmount_pct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 6, 9, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("Arrayerrors", String.Empty, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 4000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				lstrErrors = .Parameters("Arrayerrors").Value
			End If
		End With
		
		'+Validaciones masivas
		Call lclsErrors.ErrorMessage("VI7501_D",  ,  ,  ,  ,  , lstrErrors)
		
		insValVI7501_D = lclsErrors.Confirm
		
insValVI7501_D_Err: 
		If Err.Number Then
			insValVI7501_D = "insValVI7501_D: " & Err.Description
		End If
		
		lrecinsValVI7501_D = Nothing
		lclsErrors = Nothing
		
		On Error GoTo 0
	End Function
	
	'%Find: Este metodo carga la coleccion de elementos de la tabla "XXXXXX" devolviendo Verdadero o
	'%falso, dependiendo de la existencia de los registros.
	Public Function Find_policy(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double) As Boolean
		Dim lrecFind As eRemoteDB.Execute
		Dim lintTotalRecords As Integer
		
		On Error GoTo Find_Err
		
		lrecFind = New eRemoteDB.Execute
		
		With lrecFind
			.StoredProcedure = "insVi7501pkg.FIND_POLICY"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(True) Then
				nCod_saapv = .FieldToClass("ncod_saapv")
				nType_saapv = .FieldToClass("nType_saapv")
				dissue_dat = .FieldToClass("dissue_dat")
				nstatus_saapv = .FieldToClass("nstatus_saapv")
				nInstitution = .FieldToClass("nInstitution")
				ntype_ameapv = .FieldToClass("NTYPE_AMEAPV")
				dLimitDate = .FieldToClass("DLIMITDATE")
				Scertype2 = .FieldToClass("Scertype")
				nBranch2 = .FieldToClass("nBranch")
				nProduct2 = .FieldToClass("nProduct")
				nPolicy2 = .FieldToClass("npolicy2")
				
				Find_policy = True
				.RCloseRec()
			Else
				Find_policy = False
			End If
		End With
		
Find_Err: 
		If Err.Number Then
			Find_policy = False
		End If
		On Error GoTo 0
		lrecFind = Nothing
	End Function
	
	'%Find: Este metodo carga la coleccion de elementos de la tabla "XXXXXX" devolviendo Verdadero o
	'%falso, dependiendo de la existencia de los registros.
	Public Function Upd_policy(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nCod_saapv As Double, ByVal dEffecdate As Date, ByVal nInstitution As Integer) As Boolean
		Dim lrecFind As eRemoteDB.Execute
		Dim lintTotalRecords As Integer
		
		On Error GoTo Find_Err
		
		lrecFind = New eRemoteDB.Execute
		
		With lrecFind
			.StoredProcedure = "insVi7501pkg.UPD_POLICY"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCod_saapv", nCod_saapv, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInstitution", nInstitution, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				Upd_policy = True
			Else
				Upd_policy = False
			End If
		End With
		
Find_Err: 
		If Err.Number Then
			Upd_policy = False
		End If
		On Error GoTo 0
		lrecFind = Nothing
	End Function
	
	'% insLimitDate: Esta función se encarga de validar los datos introducidos en la ventana VI7501
	Public Function LimitDate(ByVal dissue_dat As Date, ByVal nType_saapv As Integer) As Date
		Dim lrecLimitDate As eRemoteDB.Execute
		
		On Error GoTo LimitDate_Err
		
		lrecLimitDate = New eRemoteDB.Execute
		
		With lrecLimitDate
			.StoredProcedure = "insVi7501pkg.InsLimitDate_Saapv"
			.Parameters.Add("dissue_dat", dissue_dat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("ntype_saapv", nType_saapv, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dLimitDate", Today, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				LimitDate = .Parameters("dLimitDate").Value
			End If
		End With
		
LimitDate_Err: 
		If Err.Number Then
			LimitDate = CDate("LimitDate: " & Err.Description)
		End If
		
		lrecLimitDate = Nothing
		
		On Error GoTo 0
	End Function
	
	'% insValVI7500: Esta función se encarga de validar los datos introducidos en la ventana VI7500
	Public Function insValVI7500(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nCod_saapv As Double, ByVal nInstitution As Integer) As String
		Dim lrecinsValVI7500 As eRemoteDB.Execute
		Dim lclsErrors As Object
        Dim lstrErrors As String = ""

        On Error GoTo insValVI7500_Err
		lrecinsValVI7500 = New eRemoteDB.Execute
		lclsErrors = eRemoteDB.NetHelper.CreateClassInstance("eFunctions.Errors")
		
		
		With lrecinsValVI7500
			.StoredProcedure = "insVi7501pkg.insvalVi7500"
			.Parameters.Add("nCod_saapv", nCod_saapv, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInstitution", nInstitution, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("Arrayerrors", String.Empty, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 4000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				lstrErrors = .Parameters("Arrayerrors").Value
			End If
		End With
		
		'+Validaciones masivas
		Call lclsErrors.ErrorMessage("VI7500",  ,  ,  ,  ,  , lstrErrors)
		
		insValVI7500 = lclsErrors.Confirm
		
insValVI7500_Err: 
		If Err.Number Then
			insValVI7500 = "insValVI7500: " & Err.Description
		End If
		
		lrecinsValVI7500 = Nothing
		lclsErrors = Nothing
		
		On Error GoTo 0
	End Function
	
	'% insPosVI7501_F: Esta función se encarga de actualizar la tabla SAAPV (Parte puntual de la Tx)
	Public Function insPosVI7501_F(ByVal nCod_saapv As Double, ByVal nInstitution As Integer, ByVal nUsercode As Integer, ByVal nInstitut_origin As Integer) As Boolean
		Dim lrecinsPostVI7501_F As eRemoteDB.Execute
		
		
		On Error GoTo insPostVI7501_F_Err
		lrecinsPostVI7501_F = New eRemoteDB.Execute
		
		
		With lrecinsPostVI7501_F
			
			.StoredProcedure = "insVi7501pkg.inspostVi7501_F"
			.Parameters.Add("ncod_saapv", nCod_saapv, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInstitution", nInstitution, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInstitut_origin", nInstitut_origin, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			insPosVI7501_F = .Run(False)
		End With
		
insPostVI7501_F_Err: 
		If Err.Number Then
			insPosVI7501_F = False
		End If
		
		lrecinsPostVI7501_F = Nothing
		
		On Error GoTo 0
	End Function
	
	'% insValVI7501_F: Esta función se encarga de validar los datos introducidos en la ventana VI7501_F (Parte puntual)
	Public Function insValVI7501_F(ByVal nInstitut_origin As Integer) As String
		Dim lrecinsValVI7501_F As eRemoteDB.Execute
		Dim lclsErrors As Object
        Dim lstrErrors As String = ""

        On Error GoTo insValVI7501_F_Err
		lrecinsValVI7501_F = New eRemoteDB.Execute
		lclsErrors = eRemoteDB.NetHelper.CreateClassInstance("eFunctions.Errors")
		
		
		With lrecinsValVI7501_F
			.StoredProcedure = "insVi7501pkg.insvalVi7501_F"
			.Parameters.Add("nInstitut_origin", nInstitut_origin, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("Arrayerrors", String.Empty, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 4000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				lstrErrors = .Parameters("Arrayerrors").Value
			End If
		End With
		
		'+Validaciones masivas
		Call lclsErrors.ErrorMessage("VI7501_F",  ,  ,  ,  ,  , lstrErrors)
		
		insValVI7501_F = lclsErrors.Confirm
		
insValVI7501_F_Err: 
		If Err.Number Then
			insValVI7501_F = "insValVI7501_F: " & Err.Description
		End If
		
		lrecinsValVI7501_F = Nothing
		lclsErrors = Nothing
		
		On Error GoTo 0
	End Function
End Class






