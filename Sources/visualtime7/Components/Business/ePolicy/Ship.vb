Option Strict Off
Option Explicit On
Public Class Ship
	'**+Objective: Class that supports the table Execute it's content is:
	'**+Version: $$Revision: 2 $
	'+Objetivo: Clase que le da soporte a la tabla Execute cuyo contenido es:
	'+Version: $$Revision: 2 $
	'**+Objective: Properties according to the table 'Ship' in the system 16-05-2005
	'+Objetivo: Propiedades según la tabla 'Ship' en el sistema 16-05-2005
	Public sCertype As String
	Public nBranch As Integer
	Public nProduct As Integer
	Public nPolicy As Double
	Public nCertif As Double
	Public dEffecdate As Date
	Public dCompdate As Date
	Public dNulldate As Date
	Public dIssueDat As Date
	Public dExpirDat As Date
	Public dStartDate As Date
	Public sClient As String
	Public nCapital As Double
	Public nPremium As Double
	Public nGroup As Short
	Public nSituation As Short
	Public nTransaction As Short
	Public nNullCode As Short
	Public nUsercode As Integer
	Public nShipUse As Short
	Public sName As String
	Public sRegist As String
	Public nMaterial As Short
	Public sColor As String
	Public nShipType As Short
	Public sConstructor As String
	Public nConsYear As Short
	Public nEquivYear As Short
	Public dLastCareDate As Date
	Public sLastCarePlace As String
	Public nDepth As Double
	Public nLength As Double
	Public nWaters As Double
	Public nNumMotors As Short
	Public sModelMotors As String
	Public sSerialMotors As String
	Public nPower As Double
	Public nTRB As Double
	Public nTRN As Double
	Public nCapacity As Double
	Public nUnitMesureCode As Short
	Public sSeaPort As String
	Public sDotation As String
	Public sActionZone As String
	
	'**%Find: This method returns TRUE or FALSE depending if the records exists in the table "Ship"
	'%Find: Este metodo retorna VERDADERO o FALSO dependiendo de la existencia o no de registros en la
	'%tabla "Ship"
	Public Function Find(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal sCodispl As String) As Boolean
		Dim lrecreaShip As eRemoteDB.Execute
		

		lrecreaShip = New eRemoteDB.Execute
		With lrecreaShip
			.StoredProcedure = "reaShip"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCodispl", sCodispl, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Find = .Run
			If Find Then
				sCertype = .FieldToClass("sCertype")
				nBranch = .FieldToClass("nBranch")
				nProduct = .FieldToClass("nProduct")
				nPolicy = .FieldToClass("nPolicy")
				nCertif = .FieldToClass("nCertif")
				dEffecdate = .FieldToClass("dEffecdate")
				dCompdate = .FieldToClass("dCompdate")
				dNulldate = .FieldToClass("dNulldate")
				dIssueDat = .FieldToClass("dIssueDat")
				dExpirDat = .FieldToClass("dExpirDat")
				dStartDate = .FieldToClass("dStartdate")
				sClient = .FieldToClass("sClient")
				nCapital = .FieldToClass("nCapital")
				nPremium = .FieldToClass("nPremium")
				nGroup = .FieldToClass("nGroup")
				nSituation = .FieldToClass("nSituation")
				nTransaction = .FieldToClass("nTransactio")
				nNullCode = .FieldToClass("nNullCode")
				nUsercode = .FieldToClass("nUserCode")
				nShipUse = .FieldToClass("nShipUse")
				sName = .FieldToClass("sName")
				sRegist = .FieldToClass("sRegist")
				nMaterial = .FieldToClass("nMaterial")
				sColor = .FieldToClass("sColor")
				nShipType = .FieldToClass("nShipType")
				sConstructor = .FieldToClass("sConstructor")
				nConsYear = .FieldToClass("nConsYear")
				nEquivYear = .FieldToClass("nEquivYear")
				dLastCareDate = .FieldToClass("dLastCareDate")
				sLastCarePlace = .FieldToClass("sLastCarePlace")
				nDepth = .FieldToClass("nDepth")
				nLength = .FieldToClass("nLength")
				nWaters = .FieldToClass("nWaters")
				nNumMotors = .FieldToClass("nNumMotors")
				sModelMotors = .FieldToClass("sModelMotors")
				sSerialMotors = .FieldToClass("sSerialMotors")
				nPower = .FieldToClass("nPower")
				nTRB = .FieldToClass("NTRB")
				nTRN = .FieldToClass("nTRN")
				nCapacity = .FieldToClass("nCapacity")
				nUnitMesureCode = .FieldToClass("nUnitMesureCode")
				sSeaPort = .FieldToClass("sSeaPort")
				sDotation = .FieldToClass("sDotation")
				sActionZone = .FieldToClass("sActionZone")
				.RCloseRec()
			End If
		End With
		lrecreaShip = Nothing
		Exit Function
	End Function
	'**%Objective: Updates a registry to the table "Ship" using the key for this table.
	'%Objetivo: Actualiza un registro a la tabla "Ship" usando la clave para dicha tabla.
	Private Function Update(ByVal sCodispl As String, ByVal nAction As Short, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nUsercode As Integer, ByVal nShipUse As Short, ByVal sName As String, ByVal sRegist As String, ByVal nMaterial As Short, ByVal sColor As String, ByVal nShipType As Short, ByVal sConstructor As String, ByVal nConsYear As Short, ByVal nEquivYear As Short, ByVal dLastCareDate As Date, ByVal sLastCarePlace As String, ByVal nDepth As Double, ByVal nLength As Double, ByVal nWaters As Double, ByVal nNumMotors As Short, ByVal sModelMotors As String, ByVal sSerialMotors As String, ByVal nPower As Double, ByVal nTRB As Double, ByVal nTRN As Double, ByVal nCapacity As Double, ByVal nUnitMesureCode As Short, ByVal sSeaPort As String, ByVal sDotation As String, ByVal sActionZone As String) As Boolean
		Dim lclsShip As eRemoteDB.Execute
		

		lclsShip = New eRemoteDB.Execute
		
		'+ Define all parameters for the stored procedures 'insudb.updShip'. Generated on <VT:DATETIME>
		With lclsShip
			.StoredProcedure = "InsUpdShip"
			.Parameters.Add("sCodispl", sCodispl, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nShipUse", nShipUse, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sName", sName, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sRegist", sRegist, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nMaterial", nMaterial, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sColor", sColor, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 15, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nShipType", nShipType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sConstructor", sConstructor, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nConsYear", nConsYear, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nEquivYear", nEquivYear, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dLastCareDate", dLastCareDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sLastCarePlace", sLastCarePlace, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDepth", nDepth, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nLength", nLength, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nWaters", nWaters, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nNumMotors", nNumMotors, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sModelMotors", sModelMotors, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 100, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sSerialMotors", sSerialMotors, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 100, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPower", nPower, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTRB", nTRB, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTRN", nTRN, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCapacity", nCapacity, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUnitMesureCode", nUnitMesureCode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sSeaPort", sSeaPort, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDotation", sDotation, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 50, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sActionZone", sActionZone, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 100, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Update = .Run(False)
		End With
		lclsShip = Nothing
		
		Exit Function
	End Function
	'**%Objective: Validation of the data for the page details.
	'%Objetivo: Validación de los datos para la página detalle.
	Public Function InsValSH001(ByVal sCodispl As String, ByVal nMainAction As Integer, ByVal sAction As String, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nShipUse As Short, ByVal sName As String, ByVal sRegist As String, ByVal nMaterial As Short, ByVal sColor As String, ByVal nShipType As Short, ByVal sConstructor As String, ByVal nConsYear As Short, ByVal nEquivYear As Short, ByVal dLastCareDate As Date, ByVal sLastCarePlace As String, ByVal nDepth As Double, ByVal nLenght As Double, ByVal nWaters As Double, ByVal nNumMotors As Short, ByVal sModelMotors As String, ByVal sSerialMotors As String, ByVal nPower As Double, ByVal nTRB As Double, ByVal nTRN As Double, ByVal nCapacity As Double, ByVal nUnitMesureCode As Short, ByVal sSeaPort As String, ByVal sDotation As String, ByVal sActionZone As String) As String
		Dim lclsErrors As eFunctions.Errors
		

		lclsErrors = New eFunctions.Errors
		
		'+ Valida que este lleno el Uso de Embarcación
		If nShipUse = eRemoteDB.Constants.intNull Or nShipUse = 0 Then
			Call lclsErrors.ErrorMessage(sCodispl, 94027)
		End If
		
		'+ Valida que este lleno el Nombre de Embarcación
		If sName = String.Empty Then
			Call lclsErrors.ErrorMessage(sCodispl, 94028)
		End If
		
		'+ Valida que este lleno la Matrícula
		If sRegist = String.Empty Then
			Call lclsErrors.ErrorMessage(sCodispl, 94029)
		End If
		
		'+ Valida que este lleno el Material del Casco
		If nMaterial = eRemoteDB.Constants.intNull Or nMaterial = 0 Then
			Call lclsErrors.ErrorMessage(sCodispl, 94030)
		End If
		
		'+ Valida que este lleno el Color
		If sColor = String.Empty Then
			Call lclsErrors.ErrorMessage(sCodispl, 94031)
		End If
		
		'+ Valida que este lleno el Tipo de Embarcación
		If nShipType = eRemoteDB.Constants.intNull Or nShipType = 0 Then
			Call lclsErrors.ErrorMessage(sCodispl, 94032)
		End If
		
		'+ Valida que este lleno el Constructor
		If sConstructor = String.Empty Then
			Call lclsErrors.ErrorMessage(sCodispl, 94033)
		End If
		
		'+ Valida que este lleno el Año de Construcción
		If nConsYear = eRemoteDB.Constants.intNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 94034)
		End If
		
		'+ Valida que este lleno el Año Equivalente
		If nEquivYear = eRemoteDB.Constants.intNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 94035)
		End If
		
		'+ Valida que este lleno la Fecha de Última Carena
		'UPGRADE_WARNING: IsEmpty was upgraded to IsNothing and has a new behavior. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
		If IsNothing(dLastCareDate) Or (dLastCareDate = eRemoteDB.Constants.dtmNull) Then
			Call lclsErrors.ErrorMessage(sCodispl, 94036)
		End If
		
		'+ Valida que este lleno el Lugar de Última Carena
		If sLastCarePlace = String.Empty Then
			Call lclsErrors.ErrorMessage(sCodispl, 94037)
		End If
		
		'+ Valida que este lleno la Eslora
		If nLenght = eRemoteDB.Constants.intNull Or nLenght = 0 Then
			Call lclsErrors.ErrorMessage(sCodispl, 94038)
		End If
		
		'+ Valida que este lleno la Manga
		If nWaters = eRemoteDB.Constants.intNull Or nWaters = 0 Then
			Call lclsErrors.ErrorMessage(sCodispl, 94039)
		End If
		
		'+ Valida que este lleno el Puntal
		If nDepth = eRemoteDB.Constants.intNull Or nDepth = 0 Then
			Call lclsErrors.ErrorMessage(sCodispl, 94040)
		End If
		
		'+ Valida que este llena la Cantidad de Motores
		If nNumMotors = eRemoteDB.Constants.intNull Or nNumMotors = 0 Then
			Call lclsErrors.ErrorMessage(sCodispl, 94041)
		End If
		
		'+ Valida que este llena la Marca/Modelo de Motores
		If sModelMotors = String.Empty Then
			Call lclsErrors.ErrorMessage(sCodispl, 94042)
		End If
		
		'+ Valida que este llena la Capacidad de Carga
		If nCapacity = eRemoteDB.Constants.intNull Or nCapacity = 0 Then
			Call lclsErrors.ErrorMessage(sCodispl, 94043)
		End If
		
		'+ Valida que este llena la Unidad de Medida
		If nUnitMesureCode = eRemoteDB.Constants.intNull Or nUnitMesureCode = 0 Then
			Call lclsErrors.ErrorMessage(sCodispl, 94044)
		End If
		
		InsValSH001 = lclsErrors.Confirm
		lclsErrors = Nothing
		
		Exit Function
	End Function
	
	'**%Objective: Pass of the information introduced towards the layers of rules of business and access of data.
	'%Objetivo: Se encarga de llevar la información introducida hacia las capas de reglas de negocio y acceso de datos.
	Public Function InsPostSH001(ByVal sCodispl As String, ByVal nMainAction As Integer, ByVal sAction As String, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nUsercode As Integer, ByVal nShipUse As Short, ByVal sName As String, ByVal sRegist As String, ByVal nMaterial As Short, ByVal sColor As String, ByVal nShipType As Short, ByVal sConstructor As String, ByVal nConsYear As Short, ByVal nEquivYear As Short, ByVal dLastCareDate As Date, ByVal sLastCarePlace As String, ByVal nDepth As Double, ByVal nLength As Double, ByVal nWaters As Double, ByVal nNumMotors As Short, ByVal sModelMotors As String, ByVal sSerialMotors As String, ByVal nPower As Double, ByVal nTRB As Double, ByVal nTRN As Double, ByVal nCapacity As Double, ByVal nUnitMesureCode As Short, ByVal sSeaPort As String, ByVal sDotation As String, ByVal sActionZone As String) As Boolean
		Dim lclsPolicyWin As ePolicy.Policy_Win
		

		InsPostSH001 = Update(sCodispl, 0, sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nUsercode, nShipUse, sName, sRegist, nMaterial, sColor, nShipType, sConstructor, nConsYear, nEquivYear, dLastCareDate, sLastCarePlace, nDepth, nLength, nWaters, nNumMotors, sModelMotors, sSerialMotors, nPower, nTRB, nTRN, nCapacity, nUnitMesureCode, sSeaPort, sDotation, sActionZone)
		If InsPostSH001 Then
			lclsPolicyWin = New ePolicy.Policy_Win
			Call lclsPolicyWin.Add_PolicyWin(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nUsercode, "SH001", "2")
			lclsPolicyWin = Nothing
		End If
		
		Exit Function
	End Function
End Class











