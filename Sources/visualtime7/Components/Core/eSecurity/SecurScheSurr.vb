Option Strict Off
Option Explicit On
Public Class SecurScheSurr
	
	Public sScheCode As String
	Public nTypeResc As Integer
	Public dCompdate As String
	Public sModDateR As String
	Public sRescTot As String
	Public sRescPar As String
	Public sTypExecut As String
	Public sModDateP As String
	Public nUserCode As Integer
	Public nTypeRescV As Integer
	Public sModDateRV As String
	Public sRescTotV As String
	Public sRescParv As String
	Public sTypExecutV As String
	Public sAnulRec As String
	Public sModDatePV As String
	Public sRequest As String
	Public sReport As String
	Public nValueTyp As Integer
	
	Public cVNTBranches As Collection
	Public cVNTReasons As Collection
	Public cVNTPayWays As Collection
	
	Public cVTBranches As Collection
	Public cVTRoles As Collection
	Public cVTPayWays As Collection
	
	Public Enum eLifeType
		none = 0
		Life = 1
		VNT = 2
	End Enum
	
	Public ReadOnly Property sTypeExecutP() As String
		Get
			sTypeExecutP = IIf(sTypExecut = "1", "1", "2")
		End Get
	End Property
	
	Public ReadOnly Property sTypeExecutD() As String
		Get
			sTypeExecutD = IIf(sTypExecut = "2", "1", "2")
		End Get
	End Property
	
	Public ReadOnly Property sTypeExecutB() As String
		Get
			sTypeExecutB = IIf(sTypExecut = "3", "1", "2")
		End Get
	End Property
	
	Public ReadOnly Property bAllowsDefinitiveExecutions() As Boolean
		Get
			bAllowsDefinitiveExecutions = sTypExecut = "2" Or sTypExecut = "3"
		End Get
	End Property
	
	Public ReadOnly Property bAllowsPreliminaryExecutions() As Boolean
		Get
			bAllowsPreliminaryExecutions = sTypExecut = "1" Or sTypExecut = "3"
		End Get
	End Property
	
	Public ReadOnly Property bAllowsDefinitiveExecutionsV() As Boolean
		Get
			bAllowsDefinitiveExecutionsV = sTypExecutV = "2" Or sTypExecutV = "3"
		End Get
	End Property
	
	Public ReadOnly Property bAllowsPreliminaryExecutionsV() As Boolean
		Get
			bAllowsPreliminaryExecutionsV = sTypExecutV = "1" Or sTypExecutV = "3"
		End Get
	End Property
	
	Public ReadOnly Property sTypeExecutVP() As String
		Get
			sTypeExecutVP = IIf(sTypExecutV = "1", "1", "2")
		End Get
	End Property
	
	Public ReadOnly Property sTypeExecutVD() As String
		Get
			sTypeExecutVD = IIf(sTypExecutV = "2", "1", "2")
		End Get
	End Property
	
	Public ReadOnly Property sTypeExecutVB() As String
		Get
			sTypeExecutVB = IIf(sTypExecutV = "3", "1", "2")
		End Get
	End Property
	
	
	Public ReadOnly Property sRequestY() As String
		Get
			sRequestY = IIf(sRequest = "1", "1", "2")
		End Get
	End Property
	
	Public ReadOnly Property sRequestN() As String
		Get
			sRequestN = IIf(sRequest = "2", "1", "2")
		End Get
	End Property
	
	Public ReadOnly Property sRequestB() As String
		Get
			sRequestB = IIf(sRequest = "3", "1", "2")
		End Get
	End Property
	
	Public ReadOnly Property sAnulRecY() As String
		Get
			sAnulRecY = IIf(sAnulRec = "1", "1", "2")
		End Get
	End Property
	
	Public ReadOnly Property sAnulRecN() As String
		Get
			sAnulRecN = IIf(sAnulRec = "2", "1", "2")
		End Get
	End Property
	
	Public ReadOnly Property sAnulRecB() As String
		Get
			sAnulRecB = IIf(sAnulRec = "3", "1", "2")
		End Get
	End Property
	
	
	Public ReadOnly Property sReportY() As String
		Get
			sReportY = IIf(sReport = "1", "1", "2")
		End Get
	End Property
	
	Public ReadOnly Property sReportN() As String
		Get
			sReportN = IIf(sReport = "2", "1", "2")
		End Get
	End Property
	
	Public ReadOnly Property sReportB() As String
		Get
			sReportB = IIf(sReport = "3", "1", "2")
		End Get
	End Property
	
	
	'%Find: Este metodo retorna VERDADERO o FALSO dependiendo de la existencia o no de registros en la
	'%tabla "Secur_sche"
	Private Function FindItems(ByVal sSche_code As String, ByRef oCollection As Collection, ByRef sSPName As String, ByRef nType As eLifeType) As Boolean
		Dim lrecvalSchema As eRemoteDB.Execute
		Dim lobjItem As GenericItem
		
		On Error GoTo ErrorHandler
		
		If Not oCollection Is Nothing Then
			'UPGRADE_NOTE: Object oCollection may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			oCollection = Nothing
		End If
		oCollection = New Collection
		
		lrecvalSchema = New eRemoteDB.Execute
		With lrecvalSchema
			.StoredProcedure = sSPName
			.Parameters.Add("sSche_Code", sSche_code, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 6, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If nType <> eLifeType.none Then
				.Parameters.Add("sLife", nType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			End If
			If .Run Then
				FindItems = True
				
				Do While Not .EOF
					lobjItem = New GenericItem
					
					lobjItem.nId = .FieldToClass("nId")
					lobjItem.bSelected = .FieldToClass("bSelected")
					lobjItem.sDescript = .FieldToClass("sDescript")
					oCollection.Add(lobjItem)
					
					.RNext()
				Loop 
			End If
			.RCloseRec()
		End With
		
		'UPGRADE_NOTE: Object lrecvalSchema may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecvalSchema = Nothing
		Exit Function
		
ErrorHandler: 
		'UPGRADE_WARNING: Array has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
		ProcError("SecurScheSurr.FindItems(sSche_code, nType, oCollection, sSPName)", New Object(){sSche_code, nType, oCollection, sSPName})
		
	End Function
	
	
	Public Function FindVNTBranches(ByVal sSche_code As String) As Boolean
		FindVNTBranches = FindItems(sSche_code, cVNTBranches, "reaSCHE_SURR_BRANCH", (eLifeType.VNT))
	End Function
	
	Public Function FindVTBranches(ByVal sSche_code As String) As Boolean
		FindVTBranches = FindItems(sSche_code, cVTBranches, "reaSCHE_SURR_BRANCH", (eLifeType.Life))
	End Function
	
	Public Function FindVNTReasons(ByVal sSche_code As String) As Boolean
		FindVNTReasons = FindItems(sSche_code, cVNTReasons, "reaSCHE_SURR_REASON", (eLifeType.none))
	End Function
	
	Public Function FindVNTPayWays(ByVal sSche_code As String) As Boolean
		FindVNTPayWays = FindItems(sSche_code, cVNTPayWays, "reaSCHE_SURR_PAYMENT", (eLifeType.VNT))
	End Function
	
	Public Function FindVTPayWays(ByVal sSche_code As String) As Boolean
		FindVTPayWays = FindItems(sSche_code, cVTPayWays, "reaSCHE_SURR_PAYMENT", (eLifeType.Life))
	End Function
	
	Public Function FindVTRoles(ByVal sSche_code As String) As Boolean
		FindVTRoles = FindItems(sSche_code, cVTRoles, "reaSCHE_SURR_ROLES", (eLifeType.none))
	End Function
	
	
	'%Find: Este metodo retorna VERDADERO o FALSO dependiendo de la existencia o no de registros en la
	'%tabla "Secur_sche"
	Public Function Find(ByVal sSche_code As String, ByRef bGetAllInfo As Boolean) As Boolean
		Dim lrecvalSchema As eRemoteDB.Execute
		
		On Error GoTo ErrorHandler
		
		
		lrecvalSchema = New eRemoteDB.Execute
		With lrecvalSchema
			.StoredProcedure = "REASECUR_SCHE_SURR"
			.Parameters.Add("sSche_Code", sSche_code, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 6, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Find = True
				sScheCode = .FieldToClass("SSCHE_CODE")
				nTypeResc = .FieldToClass("nTypeResc")
				dCompdate = .FieldToClass("dCompdate")
				sModDateR = .FieldToClass("SMOD_DATE_R")
				sRescTot = .FieldToClass("SRESC_TOT")
				sRescPar = .FieldToClass("SRESC_PAR")
				sTypExecut = .FieldToClass("sTypExecut")
				sModDateP = .FieldToClass("SMOD_DATE_P")
				nUserCode = .FieldToClass("nUsercode")
				nTypeRescV = .FieldToClass("nTypeRescV")
				sModDateRV = .FieldToClass("SMOD_DATE_RV")
				sRescTotV = .FieldToClass("SRESC_TOTV")
				sRescParv = .FieldToClass("SRESC_PARV")
				sTypExecutV = .FieldToClass("sTypExecutV")
				sAnulRec = .FieldToClass("sAnulRec")
				sModDatePV = .FieldToClass("SMOD_DATE_PV")
				sRequest = .FieldToClass("sRequest")
				sReport = .FieldToClass("sReport")
				nValueTyp = .FieldToClass("NVALUE_TYP")
				
				If bGetAllInfo Then
					Call FindVNTBranches(sSche_code)
					Call FindVNTReasons(sSche_code)
					Call FindVNTPayWays(sSche_code)
				End If
			End If
		End With
		
		'UPGRADE_NOTE: Object lrecvalSchema may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecvalSchema = Nothing
		
		Exit Function
		
ErrorHandler: 
		'UPGRADE_WARNING: Array has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
		ProcError("SecurScheSurr.Find(sSche_code)", New Object(){sSche_code})
	End Function
	
	'PostSG021:
	Public Function PostSG021(ByVal sScheCode As String, ByVal nTypeResc As Integer, ByVal sModDateR As String, ByVal sRescTot As String, ByVal sRescPar As String, ByVal sTypExecut As String, ByVal sModDateP As String, ByVal nUserCode As Integer, ByVal nTypeRescV As Integer, ByVal sModDateRV As String, ByVal sRescTotV As String, ByVal sRescParv As String, ByVal sTypExecutV As String, ByVal sAnulRec As String, ByVal sModDatePV As String, ByVal sRequest As String, ByVal sReport As String, ByVal nValueTyp As Integer, ByVal sReasonList As String, ByVal sPayWayListV As String, ByVal sBranchListV As String, ByVal sPayWayListVNT As String, ByVal sBranchListVNT As String, ByVal sRoleListVNT As String) As Boolean
		Dim lrecPostSG021 As eRemoteDB.Execute
		On Error GoTo err_h
		
		lrecPostSG021 = New eRemoteDB.Execute
		
		With lrecPostSG021
			
			.StoredProcedure = "INSPOSTSG021"
			
			.Parameters.Add("SSCHE_CODE", sScheCode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 6, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("NTYPERESC", nTypeResc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("SMOD_DATE_R", sModDateR, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("SRESC_TOT", sRescTot, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("SRESC_PAR", sRescPar, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("STYPEXECUT", sTypExecut, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("SMOD_DATE_P", sModDateP, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("NUSERCODE", nUserCode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("NTYPERESCV", nTypeRescV, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("SMOD_DATE_RV", sModDateRV, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("SRESC_TOTV", sRescTotV, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("SRESC_PARV", sRescParv, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("STYPEXECUTV", sTypExecutV, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("SANULREC", sAnulRec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("SMOD_DATE_PV", sModDatePV, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("SREQUEST", sRequest, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("SREPORT", sReport, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("NVALUE_TYP", nValueTyp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("SREASONLIST", sReasonList, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 200, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("SPAYWAYLISTV", sPayWayListV, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 200, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("SBRANCHLISTV", sBranchListV, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 200, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("SPAYWAYLISTVNT", sPayWayListVNT, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 200, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("SBRANCHLISTVNT", sBranchListVNT, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 200, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("SROLELISTVNT", sRoleListVNT, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 200, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			
			PostSG021 = .Run(False)
		End With
		
		'UPGRADE_NOTE: Object lrecPostSG021 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecPostSG021 = Nothing
		
		
		
		Exit Function
err_h: 
		'UPGRADE_WARNING: Array has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
		ProcError("SecurScheSurr.PostSG021(sScheCode, nTypeResc, sModDateR, sRescTot, sRescPar, sTypExecut, sModDateP, nTypeRescV, sModDateRV, sRescTotV, sRescParv, sTypExecutV, sAnulRec, sModDatePV, sRequest, sReport, nValueTyp, sReasonList, sPayWayListV, sBranchListV, sPayWayListVNT,  sBranchListVNT,  sRoleListVNT)", New Object(){sScheCode, nTypeResc, sModDateR, sRescTot, sRescPar, sTypExecut, sModDateP, nTypeRescV, sModDateRV, sRescTotV, sRescParv, sTypExecutV, sAnulRec, sModDatePV, sRequest, sReport, nValueTyp, sReasonList, sPayWayListV, sBranchListV, sPayWayListVNT, sBranchListVNT, sRoleListVNT})
		
		
	End Function
	
	'ValSG021:
	Public Function ValSG021(ByVal sScheCode As String, ByVal nTypeResc As Integer, ByVal sModDateR As String, ByVal sRescTot As String, ByVal sRescPar As String, ByVal sTypExecut As String, ByVal sModDateP As String, ByVal nUserCode As Integer, ByVal nTypeRescV As Integer, ByVal sModDateRV As String, ByVal sRescTotV As String, ByVal sRescParv As String, ByVal sTypExecutV As String, ByVal sAnulRec As String, ByVal sModDatePV As String, ByVal sRequest As String, ByVal sReport As String, ByVal nValueTyp As Integer, ByVal sReasonList As String, ByVal sPayWayListV As String, ByVal sBranchListV As String, ByVal sPayWayListVNT As String, ByVal sBranchListVNT As String, ByVal sRoleListVNT As String) As String
		On Error GoTo err_h
		
		
		Exit Function
err_h: 
		'UPGRADE_WARNING: Array has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
		ProcError("SecurScheSurr.ValSG021(sScheCode, nTypeResc, sModDateR, sRescTot, sRescPar, sTypExecut, sModDateP, nTypeRescV, sModDateRV, sRescTotV, sRescParv, sTypExecutV, sAnulRec, sModDatePV, sRequest, sReport, nValueTyp, sReasonList, sPayWayListV, sBranchListV, sPayWayListVNT,  sBranchListVNT,  sRoleListVNT)", New Object(){sScheCode, nTypeResc, sModDateR, sRescTot, sRescPar, sTypExecut, sModDateP, nTypeRescV, sModDateRV, sRescTotV, sRescParv, sTypExecutV, sAnulRec, sModDatePV, sRequest, sReport, nValueTyp, sReasonList, sPayWayListV, sBranchListV, sPayWayListVNT, sBranchListVNT, sRoleListVNT})
		
	End Function
	
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		sTypExecut = "1"
		sTypExecutV = "1"
		sRequest = "1"
		sAnulRec = "1"
		sReport = "1"
		
		
		
		
		
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
End Class






