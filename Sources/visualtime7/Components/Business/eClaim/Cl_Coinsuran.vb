Option Strict Off
Option Explicit On
Public Class Cl_Coinsuran
	'- Propiedades públicas de la clase, según estructura de la tabla Cl_Coinsuran
	
	'Name                                      Null?    Type
	'----------------------------------------- -------- ----------------------------
	Public nCase_num As Integer 'NOT NULL NUMBER(5)
	Public nDeman_type As Integer 'NOT NULL NUMBER(5)
	Public nCompany As Integer 'NOT NULL NUMBER(5)
	Public dEffecdate As Date 'NOT NULL DATE
	Public nExpenses As Double 'Number(4, 2)
	Public nClaim As Double 'NOT NULL NUMBER(10)
	Public nShare As Double 'Number(4, 2)
	Public nUsercode As Integer 'Number(5)
	
	Public sSel As String
	
	'+Nombre de compañia
	Public sCompany As String
	'% insValSI754: Ejecuta las validaciones de la transacción
	Public Function insValSI754(ByVal ldblShare As Double, ByVal llngCompany As Integer, ByVal llngFirstCompany As Integer) As String
        Dim sCodispl As Object = New Object
        Dim lclsErrors As New eFunctions.Errors
		
		On Error GoTo insValSI754_err
		
		'+ Se ejecutan las validaciones de los campos % de participación propia (3067) y
		'+ % de participación (3069) - 01/07/2002
		
		If ldblShare <= 0 Or ldblShare = eRemoteDB.Constants.intNull Then
			If llngCompany = llngFirstCompany Then
				Call lclsErrors.ErrorMessage("SI754", 3067)
			Else
				Call lclsErrors.ErrorMessage("SI754", 3069)
			End If
		ElseIf llngCompany = llngFirstCompany Then 
			If ldblShare > 100 Then
				Call lclsErrors.ErrorMessage(sCodispl, 11239)
			End If
		ElseIf ldblShare >= 100 Then 
			Call lclsErrors.ErrorMessage(sCodispl, 9992)
		End If
		
		insValSI754 = lclsErrors.Confirm
		
insValSI754_err: 
		If Err.Number Then
			insValSI754 = "insValSI754: " & Err.Description
		End If
		
		lclsErrors = Nothing
		
		On Error GoTo 0
	End Function
	
	'**% insValShareTotal: make the validation to share
	'% insValShareTotal: se realizan las validaciones del campo porcentaje
	Public Function insValShareTotal(ByVal sCodispl As String, ByVal nShareTotal As Double) As String
		Dim lclsErrors As eFunctions.Errors
		Dim lcolcl_Reinsurans As eClaim.cl_Reinsurans
		
		lclsErrors = New eFunctions.Errors
		
		On Error GoTo insValShareTotal_Err
		
		If nShareTotal <> 100 And nShareTotal <> 0 Then
			Call lclsErrors.ErrorMessage(sCodispl, 3070)
		End If
		
		insValShareTotal = lclsErrors.Confirm
		
insValShareTotal_Err: 
		If Err.Number Then
			insValShareTotal = insValShareTotal & Err.Description
		End If
		On Error GoTo 0
	End Function
	
	'% insPostSI754: Ejecuta las acciones básicas de actualización sobre la tabla Cl_Coinsuran - ACM - 03/07/2002
	Public Function insPostSI754(ByVal sAction As String, ByVal nClaimNumber As Double, ByVal nCase_Number As Integer, ByVal nDemandant_Type As Integer, ByVal dEffecdate As Date, ByVal sCompanyCode As String, ByVal sShareAmount As String, ByVal sExpenses As String, ByVal nUsercode As Integer, ByVal sSel As String) As Boolean
		Dim lrecinsPostsi754 As eRemoteDB.Execute
		On Error GoTo insPostsi754_Err
		
		lrecinsPostsi754 = New eRemoteDB.Execute
		
		'+
		'+ Definición de store procedure insPostsi754 al 07-23-2003 16:34:44
		'+
		With lrecinsPostsi754
			.StoredProcedure = "InsSI754PKG.insPostSI754"
			.Parameters.Add("sAction", sAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nClaimnumber", nClaimNumber, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCase_number", nCase_Number, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDemandant_type", nDemandant_Type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sSel", sSel, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 4000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCompanycode", sCompanyCode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 4000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sExpenses", sExpenses, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 4000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sShareamount", sShareAmount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 4000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			insPostSI754 = .Run(False)
		End With
		
insPostsi754_Err: 
		If Err.Number Then
			insPostSI754 = False
		End If
		lrecinsPostsi754 = Nothing
		On Error GoTo 0
	End Function
	
	'% UpdateSI754: Actualiza los registros sobre la tabla Cl_Coinsuran - 04/07/2002
	Private Function UpdateSI754(ByVal nAction As Integer) As Boolean
		Dim lrecinsUpdate_Cl_Coinsuran As New eRemoteDB.Execute
		
		On Error GoTo UpdateSI754_err
		
		With lrecinsUpdate_Cl_Coinsuran
			.StoredProcedure = "insUpdate_Cl_Coinsuran"
			.Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 5, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nClaim", Me.nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCaseNumber", Me.nCase_num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDemandant_Type", Me.nDeman_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", Me.dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCompanyCode", Me.nCompany, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 5, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nExpenses", Me.nExpenses, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 10, 0, 2, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", Me.nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 5, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nShareAmount", Me.nShare, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 10, 0, 2, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			UpdateSI754 = .Run(False)
		End With
		
UpdateSI754_err: 
		If Err.Number Then
			UpdateSI754 = False
		End If
		
		lrecinsUpdate_Cl_Coinsuran = Nothing
		
		On Error GoTo 0
	End Function
	
	Private Sub Class_Initialize_Renamed()
		
		nCase_num = eRemoteDB.Constants.intNull
		nDeman_type = eRemoteDB.Constants.intNull
		nCompany = eRemoteDB.Constants.intNull
		dEffecdate = eRemoteDB.Constants.dtmNull
		nExpenses = eRemoteDB.Constants.intNull
		nClaim = eRemoteDB.Constants.intNull
		nShare = eRemoteDB.Constants.intNull
		nUsercode = eRemoteDB.Constants.intNull
		sSel = CStr(eRemoteDB.Constants.strNull)
		
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
End Class






