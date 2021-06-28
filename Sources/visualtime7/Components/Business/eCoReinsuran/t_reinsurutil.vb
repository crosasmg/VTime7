Option Strict Off
Option Explicit On
Public Class t_reinsurutil
	'+ Estructura de tabla tar_cesrisk
	'+     Property                Type         DBType   Size Scale  Prec  Null
	'+-------------------------------------------------------------------------
	Public nUsercode As Integer ' NUMBER     22   0     5    N
	Public nTotam_out As Double
	Public nTotam_in As Double
	Public nRes_risklast As Double
	Public nRes_risk As Double
	Public nRes_cllast As Double
	Public nRes_cl As Double
	Public nReser_cl As Double
	Public nNumber As Integer ' NUMBER     22   0     5    N
	Public nCurrency As Integer ' NUMBER     22   0     5    N
	Public nCompany_rei As Integer ' NUMBER     22   0     5    N
	Public nCompany_ced As Integer ' NUMBER     22   0     5    N
	Public nAm_comm As Double
	Public nAmount_pr As Double
	Public nAmountutil As Double
	Public nAmountpart As Double
	Public nAmlastneg As Double
	Public nAmadmin As Double
	Public dStartdate As Date ' DATE       7    0     0    N
	Public dEndDate As Date ' DATE       7    0     0    N
	
	Public nCessprem_o As Double
	Public nAmount_o As Double
	Public nCed_amnt_o As Double
	Public Function insPreCRL893(ByVal nCompany As Integer, ByVal nNumber As Integer, ByVal dStartproc As Date, ByVal dEndProc As Date, ByVal sExecute As String) As Boolean
		
		Dim lrecinsPreCRL893 As eRemoteDB.Execute
		
		lrecinsPreCRL893 = New eRemoteDB.Execute
		
		'+Definición de parámetros para stored procedure 'insudb.rea_sil838'
		With lrecinsPreCRL893
			.StoredProcedure = "InsRea_CRL893"
			.Parameters.Add("nCompany", nCompany, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nNumber", nNumber, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sExecute", sExecute, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dStarproc", dStartproc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEndProc", dEndProc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("ncessprem_o", nCessprem_o, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nced_amnt_o", nCed_amnt_o, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("namount_o", nAmount_o, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(False) Then
				nCessprem_o = .Parameters("ncessprem_o").Value
				nCed_amnt_o = .Parameters("nCed_amnt_o").Value
				nAmount_o = .Parameters("nAmount_o").Value
				
				nAmount_pr = nCessprem_o
				nAm_comm = nAmount_pr * (2 / 100)
				nReser_cl = nCed_amnt_o
				
				nAmadmin = nAmount_pr * (7.5 / 100)
				nRes_cl = nAmount_o
				
				
				insPreCRL893 = True
			Else
				insPreCRL893 = False
			End If
		End With
		
		'UPGRADE_NOTE: Object lrecinsPreCRL893 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsPreCRL893 = Nothing
		
	End Function
	Public Function insPostCRL893(ByVal nCompany As Integer, ByVal nNumber As Integer, ByVal dStartproc As Date, ByVal dEndProc As Date, ByVal nUsercode As Integer, ByVal nAmount_pr As Double, ByVal nRes_risklast As Double, ByVal nRes_cllast As Double, ByVal nTotam_in As Double, ByVal nAm_comm As Double, ByVal nReser_cl As Double, ByVal nAmadmin As Double, ByVal nRes_risk As Double, ByVal nRes_cl As Double, ByVal nAmlastneg As Double, ByVal nTotam_out As Double, ByVal nAmountutil As Double, ByVal nAmountpart As Double, ByVal nTypeproc As Short) As Boolean
		Dim lrecinsPostCRL893 As eRemoteDB.Execute
		
		lrecinsPostCRL893 = New eRemoteDB.Execute
		
		With lrecinsPostCRL893
			.StoredProcedure = "INSUPDCRL893"
			.Parameters.Add("nCompany", nCompany, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nNumber", nNumber, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dStartdate", dStartproc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEnddate", dEndProc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("namount_pr", nAmount_pr, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nres_risklast", nRes_risklast, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nres_cllast", nRes_cllast, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("ntotam_in", nTotam_in, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nam_comm", nAm_comm, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nreser_cl", nReser_cl, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("namadmin", nAmadmin, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nres_risk", nRes_risk, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nres_cl", nRes_cl, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("namlastneg", nAmlastneg, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("ntotam_out", nTotam_out, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("namountutil", nAmountutil, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("namountpart", nAmountpart, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTypeproc", nTypeproc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(False) Then
				insPostCRL893 = True
			Else
				insPostCRL893 = False
			End If
		End With
		'UPGRADE_NOTE: Object lrecinsPostCRL893 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsPostCRL893 = Nothing
	End Function
	
	
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		
		nUsercode = eRemoteDB.Constants.intNull
		nTotam_out = eRemoteDB.Constants.intNull
		nTotam_in = eRemoteDB.Constants.intNull
		nRes_risklast = eRemoteDB.Constants.intNull
		nRes_risk = eRemoteDB.Constants.intNull
		nRes_cllast = eRemoteDB.Constants.intNull
		nRes_cl = eRemoteDB.Constants.intNull
		nReser_cl = eRemoteDB.Constants.intNull
		nNumber = eRemoteDB.Constants.intNull
		nCurrency = eRemoteDB.Constants.intNull
		nCompany_rei = eRemoteDB.Constants.intNull
		nCompany_ced = eRemoteDB.Constants.intNull
		nAm_comm = eRemoteDB.Constants.intNull
		nAmount_pr = eRemoteDB.Constants.intNull
		nAmountutil = eRemoteDB.Constants.intNull
		nAmountpart = eRemoteDB.Constants.intNull
		nAmlastneg = eRemoteDB.Constants.intNull
		nAmadmin = eRemoteDB.Constants.intNull
		dStartdate = System.Date.FromOADate(eRemoteDB.Constants.intNull)
		dEndDate = System.Date.FromOADate(eRemoteDB.Constants.intNull)
		
		nCessprem_o = eRemoteDB.Constants.intNull
		nAmount_o = eRemoteDB.Constants.intNull
		nCed_amnt_o = eRemoteDB.Constants.intNull
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
End Class






