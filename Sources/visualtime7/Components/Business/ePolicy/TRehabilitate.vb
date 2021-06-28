Option Strict Off
Option Explicit On
Public Class TRehabilitate
	'%-------------------------------------------------------%'
	'% $Workfile:: TRehabilitate.cls                        $%'
	'% $Author:: Nvaplat28                                  $%'
	'% $Date:: 30/09/04 18.11                               $%'
	'% $Revision:: 12                                       $%'
	'%-------------------------------------------------------%'
	
	'+
	'+ Estructura de tabla TRehabilitate al 02-07-2002 12:17:27
	'+     Property                Type         DBType   Size Scale  Prec  Null
	'+-------------------------------------------------------------------------
	Public sKey As String ' CHAR       20   0     0    S
	Public nBranch As Integer ' NUMBER     22   0     5    S
	Public nProduct As Integer ' NUMBER     22   0     5    S
	Public nPolicy As Double ' NUMBER     22   0     10   S
	Public nCertif As Double ' NUMBER     22   0     10   S
	Public nStatquota As Integer ' NUMBER     22   0     5    S
	Public nCapital As Integer ' NUMBER     22   0     12   S
	Public sCurrency As String ' CHAR       30   0     0    S
	Public nOffice As Integer ' NUMBER     22   0     5    S
	Public sNameexec As String ' CHAR       15   0     0    S
	Public dEffecdate As Date ' DATE       7    0     0    S
	Public sClient_age As String ' CHAR       14   0     0    S
	Public sCliename_age As String ' CHAR       60   0     0    S
	Public nPayfreq As Integer ' NUMBER     22   0     5    S
	Public nId As Integer ' NUMBER     22   0     20   S
	Public nReceipt_a As Integer ' NUMBER     22   0     10   S
	Public dEffecdate_a As Date ' DATE       7    0     0    S
	Public nPremium_a As Double ' NUMBER     22   2     10   S
	Public sClient_a As String ' CHAR       14   0     0    S
	Public sCliename_a As String ' CHAR       60   0     0    S
	Public nReceipt_d As Integer ' NUMBER     22   0     10   S
	Public dEffecdate_d As Date ' DATE       7    0     0    S
	Public nPremium_d As Double ' NUMBER     22   2     10   S
	Public sTratypei As String ' CHAR       30   0     0    S
	Public sClient_d As String ' CHAR       14   0     0    S
	Public sCliename_d As String ' CHAR       60   0     0    S
	Public nPremium_tot As Double ' NUMBER     22   2     10   S
	'%InsUpdTRehabilitate: Se encarga de actualizar la tabla TRehabilitate
	Private Function InsUpdTRehabilitate(ByVal nAction As Integer) As Boolean
		
	End Function
	
	'%Add: Crea un registro en la tabla
	Public Function Add() As Boolean
		Add = InsUpdTRehabilitate(1)
	End Function
	
	'%Update: Actualiza un registro en la tabla
	Public Function Update() As Boolean
		Update = InsUpdTRehabilitate(2)
	End Function
	
	'%Delete: Borra un registro en la tabla
	Public Function Delete() As Boolean
		Delete = InsUpdTRehabilitate(3)
	End Function
	
	'%inscalrehabilitate: Borra un registro en la tabla
	Public Function Inscalrehabilitate(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nTypeExec As Integer, ByVal nUsercode As Integer, Optional ByVal nRehaReceipt As Integer = 0, Optional ByVal nProccess As Short = 0) As Boolean
		
		Dim l_sKey As String
		Dim lrecInscalrehabilitate As eRemoteDB.Execute
		
		lrecInscalrehabilitate = New eRemoteDB.Execute
		
		On Error GoTo lrecInscalrehabilitate_Err
		
		With lrecInscalrehabilitate
			.StoredProcedure = "insCalrehabilitate"
			.Parameters.Add("sCertype", "2", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTypeexec", nTypeExec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRehaReceipt", nRehaReceipt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sKey", "", eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eFunctions.Parameter.eRmtDataType.rdbVarchar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProccess", nProccess, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				Inscalrehabilitate = True
				Me.sKey = .Parameters("sKey").Value
			End If
		End With
		'UPGRADE_NOTE: Object lrecInscalrehabilitate may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecInscalrehabilitate = Nothing
lrecInscalrehabilitate_Err: 
		If Err.Number Then
			Inscalrehabilitate = False
		End If
	End Function
End Class






