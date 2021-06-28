Option Strict Off
Option Explicit On
Public Class Policy_security
	'%-------------------------------------------------------%'
	'% $Workfile:: Policy_security.cls                           $%'
	'% $Author:: Nvaplat37                                  $%'
	'% $Date:: 17/04/04 7:44p                               $%'
	'% $Revision:: 1                                        $%'
	'%-------------------------------------------------------%'
	'+
	'+ Estructura de tabla insudb.Policy_security
	'+     Property                Type         DBType   Size Scale  Prec  Null
	'+-------------------------------------------------------------------------
	Public nPolicy As Double ' NUMBER     22   0    10    N
	Public nUsercode As Integer ' NUMBER     22   0     5    N
	Public nUsercode_old As Integer ' NUMBER     22   0     5    N
	
	'%InsUpdPolicy_security: Se encarga de actualizar la tabla Policy_security
	Private Function InsUpdPolicy_security(ByVal nAction As Integer) As Boolean
		Dim lrecinsUpdPolicy_security As eRemoteDB.Execute
		
		On Error GoTo insUpdPolicy_security_Err
		
		lrecinsUpdPolicy_security = New eRemoteDB.Execute
		
		'+ Definición de store procedure insUpdadd_risk al 04-25-2002 16:04:41
		With lrecinsUpdPolicy_security
			.StoredProcedure = "insUpdPolicy_security"
			.Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode_old", nUsercode_old, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			InsUpdPolicy_security = .Run(False)
		End With
		
insUpdPolicy_security_Err: 
		If Err.Number Then
			InsUpdPolicy_security = False
		End If
		'UPGRADE_NOTE: Object lrecinsUpdPolicy_security may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsUpdPolicy_security = Nothing
		On Error GoTo 0
	End Function
	
	'%Add: Crea un registro en la tabla
	Public Function Add() As Boolean
		Add = InsUpdPolicy_security(1)
	End Function
	
	'%Update: Actualiza un registro en la tabla
	Public Function Update() As Boolean
		Update = InsUpdPolicy_security(2)
	End Function
	
	'%Delete: Borra un registro en la tabla
	Public Function Delete() As Boolean
		Delete = InsUpdPolicy_security(3)
	End Function
	
	'%Find: Lee los datos de la tabla
	Public Function Find(ByVal nPolicy As Double, ByVal nUsercode As Integer) As Boolean
		Dim lrecreapolicy_security As eRemoteDB.Execute
		
		On Error GoTo Find_Err
		
		lrecreapolicy_security = New eRemoteDB.Execute
		
		With lrecreapolicy_security
			.StoredProcedure = "reaPolicy_security"
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(True) Then
				Find = True
				nPolicy = nPolicy
				nUsercode = nUsercode
			Else
				Find = False
			End If
		End With
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		'UPGRADE_NOTE: Object lrecreapolicy_security may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreapolicy_security = Nothing
		On Error GoTo 0
		
	End Function
	'% insValSI754: Ejecuta las validaciones de la transacción
	Public Function insValSG852(ByVal sAction As String, ByVal nPolicy As Double, ByVal nUsercode As Integer, ByVal nUsercode_old As Integer) As String
		Dim lclsErrors As New eFunctions.Errors
		Dim lclsPolicy As Object
		
		On Error GoTo insValSG852_err
		
		If nPolicy = eRemoteDB.Constants.intNull Then
			Call lclsErrors.ErrorMessage("SG852", 3003)
		Else
			lclsPolicy = eRemoteDB.NetHelper.CreateClassInstance("ePolicy.Policy")
			If Not lclsPolicy.FindPolicybyPolicy("2", nPolicy) Then
				Call lclsErrors.ErrorMessage("SG852", 8071)
			End If
		End If
		
		If nUsercode = eRemoteDB.Constants.intNull Then
			Call lclsErrors.ErrorMessage("SG852", 12049)
		End If
		
		If nPolicy <> eRemoteDB.Constants.intNull And nUsercode <> eRemoteDB.Constants.intNull And nUsercode <> nUsercode_old Then
			If Find(nPolicy, nUsercode) Then
				Call lclsErrors.ErrorMessage("SG852", 10284)
			End If
		End If
		
		insValSG852 = lclsErrors.Confirm
		
insValSG852_err: 
		If Err.Number Then
			insValSG852 = "insValSG852: " & Err.Description
		End If
		
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		
		On Error GoTo 0
	End Function
	'%InsPostSG852: Ejecuta el post de la transacción
	'%              Mantenimiento de pólizas con acceso restringido (SG852)
	Public Function InsPostSG852(ByVal sAction As String, ByVal nPolicy As Double, ByVal nUsercode As Integer, ByVal nUsercode_old As Integer) As Boolean
		
		On Error GoTo InsPostSG852_Err
		
		With Me
			.nPolicy = nPolicy
			.nUsercode = nUsercode
			.nUsercode_old = nUsercode_old
		End With
		
		Select Case sAction
			Case "Add"
				InsPostSG852 = Add
			Case "Update"
				InsPostSG852 = Update
			Case "Del"
				InsPostSG852 = Delete
		End Select
		
InsPostSG852_Err: 
		If Err.Number Then
			InsPostSG852 = False
		End If
		On Error GoTo 0
	End Function
	Public Function ValPolicySecur(ByVal nPolicy As Double, ByVal nUsercode As Integer) As Boolean
		Dim lrecValPolicySecur As eRemoteDB.Execute
		
		lrecValPolicySecur = New eRemoteDB.Execute
		On Error GoTo ValPolicySecur_Err
		
		With lrecValPolicySecur
			.StoredProcedure = "InsValPolicy_Security"
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy_sec", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(False) Then
				ValPolicySecur = .Parameters.Item("nPolicy_sec").Value = 1
			End If
			
		End With
		
ValPolicySecur_Err: 
		If Err.Number Then
			ValPolicySecur = False
		End If
		'UPGRADE_NOTE: Object lrecValPolicySecur may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecValPolicySecur = Nothing
		On Error GoTo 0
	End Function
	
	'%Class_Initialize: Inicializa las propiedades cuando se instancia la clase
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		nPolicy = eRemoteDB.Constants.intNull
		nUsercode = eRemoteDB.Constants.intNull
		nUsercode_old = eRemoteDB.Constants.intNull
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
End Class






