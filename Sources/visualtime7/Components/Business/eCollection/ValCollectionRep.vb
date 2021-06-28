Option Strict Off
Option Explicit On
Public Class ValCollectionRep
	'%-------------------------------------------------------%'
	'% $Workfile:: ValCollectionRep.cls                         $%'
	'% $Author:: Nvaplat53                                  $%'
	'% $Date:: 29/06/04 5:48p                               $%'
	'% $Revision:: 5                                       $%'
	'%-------------------------------------------------------%'
	
	Public P_SKEY As String
	
	'% insPostCOL895:
	Public Function insPostCOL895(ByVal dDateFrom As Date, ByVal dDateTo As Date, ByVal lintUsercode As Integer, ByVal lintCompany As Integer) As Boolean
		
		Dim lrecinsPostCOL895 As eRemoteDB.Execute
		
		lrecinsPostCOL895 = New eRemoteDB.Execute
		
		'+Definición de parámetros para stored procedure 'insudb.updLoans_int'
		With lrecinsPostCOL895
			.StoredProcedure = "rea_col895"
			.Parameters.Add("dDateFrom", dDateFrom, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dDateTo", dDateTo, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", lintUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercomp", lintCompany, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("P_SKEY", P_SKEY, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				P_SKEY = .Parameters("P_SKEY").Value
				insPostCOL895 = True
			Else
				insPostCOL895 = False
			End If
			
		End With
		
		'UPGRADE_NOTE: Object lrecinsPostCOL895 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsPostCOL895 = Nothing
		
	End Function
	'% insPostCOL1162: Informe de cartera y compromisos impagos a una fecha
	Public Function insPostCOL1162(ByVal dParam_fecha As Date, ByVal nOffice As Double, ByVal nOfficeagen As Double, ByVal nAgency As Double, ByVal nIntermed As Double) As Boolean
		Dim lrecinsPostCOL1162 As eRemoteDB.Execute
		lrecinsPostCOL1162 = New eRemoteDB.Execute
		
		With lrecinsPostCOL1162
			.StoredProcedure = "rea_col1162"
			.Parameters.Add("dParam_fecha", dParam_fecha, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOffice", nOffice, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOfficeagen", nOfficeagen, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAgency", nAgency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nIntermed", nIntermed, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("P_SKEY", P_SKEY, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				P_SKEY = .Parameters("P_SKEY").Value
				insPostCOL1162 = True
			Else
				insPostCOL1162 = False
			End If
		End With
		'UPGRADE_NOTE: Object lrecinsPostCOL1162 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsPostCOL1162 = Nothing
	End Function
	
	Public Function insPostCOC897(ByVal nPolicy As Double) As Boolean
		
		Dim lrecinsPostCOC897 As eRemoteDB.Execute
		
		lrecinsPostCOC897 = New eRemoteDB.Execute
		
		'+Definición de parámetros para stored procedure 'insudb.updLoans_int'
		With lrecinsPostCOC897
			.StoredProcedure = "REA_COC897"
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("P_SKEY", P_SKEY, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				P_SKEY = .Parameters("P_SKEY").Value
				insPostCOC897 = True
			Else
				insPostCOC897 = False
			End If
			
		End With
		
		'UPGRADE_NOTE: Object lrecinsPostCOC897 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsPostCOC897 = Nothing
		
	End Function
	
	Public Function InsPostCOL971(ByVal dDateFrom As Date, ByVal dDateTo As Date) As Boolean
		
		Dim lrecInsPostCOL971 As eRemoteDB.Execute
		
		lrecInsPostCOL971 = New eRemoteDB.Execute
		
		'+Definición de parámetros para stored procedure 'insudb.updLoans_int'
		With lrecInsPostCOL971
			.StoredProcedure = "INSCOL971"
			.Parameters.Add("dDateFrom", dDateFrom, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dDateTo", dDateTo, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("P_SKEY", P_SKEY, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				P_SKEY = .Parameters("P_SKEY").Value
				InsPostCOL971 = True
			Else
				InsPostCOL971 = False
			End If
			
		End With
		
		'UPGRADE_NOTE: Object lrecInsPostCOL971 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecInsPostCOL971 = Nothing
		
	End Function
	
	'% insPostCOL895:
	Public Function insPostCOL831(ByVal dDateFrom As Date, ByVal dDateEnd As Date, ByVal nBill_Day_Ini As Integer, ByVal nBill_Day_End As Integer) As Boolean
		
		Dim lrecinsPostCOL831 As eRemoteDB.Execute
		
		lrecinsPostCOL831 = New eRemoteDB.Execute
		
		'+Definición de parámetros para stored procedure 'insudb.updLoans_int'
		With lrecinsPostCOL831
			.StoredProcedure = "rea_col831"
			.Parameters.Add("dDateFrom", dDateFrom, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dDateEnd", dDateEnd, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nbill_day_ini", nBill_Day_Ini, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nbill_day_end", nBill_Day_End, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("P_SKEY", P_SKEY, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				P_SKEY = .Parameters("P_SKEY").Value
				insPostCOL831 = True
			Else
				insPostCOL831 = False
			End If
			
		End With
		
		'UPGRADE_NOTE: Object lrecinsPostCOL831 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsPostCOL831 = Nothing
		
	End Function
End Class






