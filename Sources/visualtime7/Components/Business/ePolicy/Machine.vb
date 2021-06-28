Option Strict Off
Option Explicit On
Public Class Machine
	'**+Objective: Class that supports the table Machine it's content is:
	'**+Version: $$Revision: 1 $
	'+Objetivo: Clase que le da soporte a la tabla Machine cuyo contenido es:
	'+Version: $$Revision: 1 $
	
	'**+Objective: Properties according to the table 'Machine' in the system 01/06/2005
	'+Objetivo: Propiedades según la tabla 'Machine' en el sistema 01/06/2005
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
	Public nTransactio As Short
	Public nNullCode As Short
	Public nUsercode As Integer
	
	'**%Find: This method returns TRUE or FALSE depending if the records exists in the table "Machine"
	'%Find: Este metodo retorna VERDADERO o FALSO dependiendo de la existencia o no de registros en la
	'%tabla "Machine"
	Public Function Find(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal sCodispl As String) As Boolean
		Dim lrecreaMachine As eRemoteDB.Execute
		

		lrecreaMachine = New eRemoteDB.Execute
		With lrecreaMachine
			.StoredProcedure = "reaMachine"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCodispl", sCodispl, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Find = .Run
			If Find Then
				Me.sCertype = sCertype
				Me.nBranch = nBranch
				Me.nProduct = nProduct
				Me.nPolicy = nPolicy
				Me.nCertif = nCertif
				Me.dEffecdate = dEffecdate
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
				nTransactio = .FieldToClass("nTransactio")
				nNullCode = .FieldToClass("nNullCode")
				nUsercode = .FieldToClass("nUserCode")
				.RCloseRec()
			End If
		End With
		lrecreaMachine = Nothing
		
		Exit Function
	End Function
	
	'**%Objective: Updates a registry to the table "Machine" using the key for this table.
	'%Objetivo: Actualiza un registro a la tabla "Machine" usando la clave para dicha tabla.
	Private Function Update(ByVal sCodispl As String, ByVal nAction As Short, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nUsercode As Integer) As Boolean
		Dim lclsMachine As eRemoteDB.Execute
		

		lclsMachine = New eRemoteDB.Execute
		
		'+ Define all parameters for the stored procedures 'InsUpdMachine'. Generated on 01/06/2005
		With lclsMachine
			.StoredProcedure = "InsUpdMachine"
			.Parameters.Add("sCodispl", sCodispl, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Update = .Run(False)
		End With
		lclsMachine = Nothing
		
		Exit Function
	End Function
	
	'**%Objective: Validation of the data for the page details.
	'%Objetivo: Validación de los datos para la página detalle.
	Public Function InsValRM001(ByVal sCodispl As String, ByVal nMainAction As Integer, ByVal sAction As String, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date) As String
		Dim lclsErrors As eFunctions.Errors
		

		lclsErrors = New eFunctions.Errors
		
		InsValRM001 = lclsErrors.Confirm
		lclsErrors = Nothing
		
		Exit Function
	End Function
	'**%Objective: Pass of the information introduced towards the layers of rules of business and access of data.
	'%Objetivo: Se encarga de llevar la información introducida hacia las capas de reglas de negocio y acceso de datos.
	Public Function InsPostRM001(ByVal sCodispl As String, ByVal nMainAction As Integer, ByVal sAction As String, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nUsercode As Integer) As Boolean
		Dim lclsPolicyWin As Policy_Win
		

		InsPostRM001 = Update(sCodispl, 0, sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nUsercode)
		
		'+  En el caso de datos particulares, se actualiza la tabla de secuencia de la poliza
		If InsPostRM001 Then
			lclsPolicyWin = New ePolicy.Policy_Win
			Call lclsPolicyWin.Add_PolicyWin(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nUsercode, "RM001", "2")
			lclsPolicyWin = Nothing
		End If
		
		Exit Function
	End Function
End Class











