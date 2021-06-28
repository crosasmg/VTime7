Option Strict Off
Option Explicit On
Public Class Saapv_pol
	'%-------------------------------------------------------%'
	'% $Workfile:: Saapv_pol.cls                           $%'
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
	Public sCertype As String ' CHAR       1    0     0    N
	Public nBranch As Integer ' NUMBER     22   0     5    N
	Public nProduct As Integer ' NUMBER     22   0     5    N
	Public nPolicy As Double ' NUMBER     22   0     10   N
	Public nCertif As Double ' NUMBER     22   0     10   N
	Public nType_saapv As Integer ' NUMBER     22   0     5    N
	Public dissue_dat As Date ' NUMBER     22   0     10   N
	Public nstatus_saapv As Integer ' NUMBER     22   0     5    N
	Public scheckamend As String
	Public scheckrequest As String
	Public nBordereaux As Double
	Public nAmount_Rel As Double
	Public sStatus As String
	Public sAutodif As String
	Public nNotenum As Double
	Public nInstitution As Integer
	Public ntype_ameapv As Double ' NUMBER     22   0     10   N
	Public dLimitDate As Date
	
	'% insValVI7501: Esta función se encarga de validar los datos introducidos en la ventana VI7501
	Public Function insValVI7501(ByVal sAction As String, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nCod_saapv As Double, ByVal nInstitution As Integer) As String
        Dim lrecinsValVI7501 As eRemoteDB.Execute
		Dim lclsErrors As Object
        Dim lstrErrors As String = ""

        On Error GoTo insValVI7501_Err
		lrecinsValVI7501 = New eRemoteDB.Execute
		lclsErrors = eRemoteDB.NetHelper.CreateClassInstance("eFunctions.Errors")
		
		
		With lrecinsValVI7501
			.StoredProcedure = "insVi7501pkg.insvalVi7501"
			.Parameters.Add("sAction", sAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("ncod_saapv", nCod_saapv, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInstitution", nInstitution, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
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
	
	
	'% insValVI7502: Esta función se encarga de validar los datos introducidos en la ventana VI7502
	Public Function insValVI7502(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal sClient As String, ByVal nCod_saapv As Double) As String
		Dim lrecinsValVI7502 As eRemoteDB.Execute
		Dim lclsErrors As Object
        Dim lstrErrors As String = ""

        On Error GoTo insValVI7502_Err
		lrecinsValVI7502 = New eRemoteDB.Execute
		lclsErrors = eRemoteDB.NetHelper.CreateClassInstance("eFunctions.Errors")
		
		
		With lrecinsValVI7502
			.StoredProcedure = "insVi7502pkg.INSVALVI7502"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("ncod_saapv", nCod_saapv, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("Arrayerrors", String.Empty, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 4000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				lstrErrors = .Parameters("Arrayerrors").Value
			End If
		End With
		
		'+Validaciones masivas
		Call lclsErrors.ErrorMessage("VI7502",  ,  ,  ,  ,  , lstrErrors)
		
		insValVI7502 = lclsErrors.Confirm
		
insValVI7502_Err: 
		If Err.Number Then
			insValVI7502 = "insValVI7502: " & Err.Description
		End If
		
		lrecinsValVI7502 = Nothing
		lclsErrors = Nothing
		
		On Error GoTo 0
	End Function
	
	'% insValVI7502: Esta función se encarga de validar los datos introducidos en la ventana VI7502
	Public Function insValVI7502upd(ByVal nCod_saapv As Double, ByVal nstatus_saapv As Double, ByVal sAutodif As String, ByVal nInstitution As Integer) As String
		Dim lrecinsValVI7502 As eRemoteDB.Execute
		Dim lclsErrors As Object
        Dim lstrErrors As String = ""

        On Error GoTo insValVI7502_Err
		lrecinsValVI7502 = New eRemoteDB.Execute
		lclsErrors = eRemoteDB.NetHelper.CreateClassInstance("eFunctions.Errors")
		
		
		With lrecinsValVI7502
			.StoredProcedure = "insVi7502pkg.INSVALVI7502UPD"
			.Parameters.Add("ncod_saapv", nCod_saapv, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nstatus_saapv", nstatus_saapv, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAutodif", sAutodif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 2, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInstitution", nInstitution, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("Arrayerrors", String.Empty, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 4000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(False) Then
				lstrErrors = .Parameters("Arrayerrors").Value
			End If
		End With
		
		'+Validaciones masivas
		Call lclsErrors.ErrorMessage("VI7502",  ,  ,  ,  ,  , lstrErrors)
		
		insValVI7502upd = lclsErrors.Confirm
		
insValVI7502_Err: 
		If Err.Number Then
			insValVI7502upd = "insValVI7502upd: " & Err.Description
		End If
		
		lrecinsValVI7502 = Nothing
		lclsErrors = Nothing
		
		On Error GoTo 0
	End Function
	
	'% insPostMAG010: Esta función se encarga de actualizar la tabla
	Public Function insPosVI7501(ByVal sAction As String, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nCod_saapv As Double, ByVal nInstitution As Integer) As Boolean
		Dim lrecinsPostVI7501 As eRemoteDB.Execute
		
		
		On Error GoTo insPostVI7501_Err
		lrecinsPostVI7501 = New eRemoteDB.Execute
		
		
		With lrecinsPostVI7501
			
			.StoredProcedure = "insVi7501pkg.inspostVi7501"
			.Parameters.Add("sAction", sAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("ncod_saapv", nCod_saapv, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInstitution", nInstitution, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			insPosVI7501 = .Run(False)
		End With
		
insPostVI7501_Err: 
		If Err.Number Then
			insPosVI7501 = False
		End If
		
		lrecinsPostVI7501 = Nothing
		
		On Error GoTo 0
	End Function
	
	Public Function insPostVI7502(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nCod_saapv As Double, ByVal nstatus_saapv As Double, ByVal nUsercode As Double, ByVal sAutodif As String, ByVal nNotenum As Double, ByVal nInstitution As Integer) As Boolean
		Dim lrecinsPostVI7502 As eRemoteDB.Execute
		
		
		On Error GoTo insPostVI7502_Err
		lrecinsPostVI7502 = New eRemoteDB.Execute
		
		
		With lrecinsPostVI7502
			
			.StoredProcedure = "insVi7502pkg.INSPOSTVI7502"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("ncod_saapv", nCod_saapv, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nstatus_saapv", nstatus_saapv, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAutodif", sAutodif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nNotenum", nNotenum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInstitution", nInstitution, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			insPostVI7502 = .Run(False)
		End With
		
insPostVI7502_Err: 
		If Err.Number Then
			insPostVI7502 = False
		End If
		
		lrecinsPostVI7502 = Nothing
		
		On Error GoTo 0
	End Function
	
	
	
	Private Sub Class_Initialize_Renamed()
		
		nCod_saapv = eRemoteDB.Constants.intNull
		sCertype = String.Empty
		nBranch = eRemoteDB.Constants.intNull
		nProduct = eRemoteDB.Constants.intNull
		nPolicy = eRemoteDB.Constants.intNull
		nCertif = eRemoteDB.Constants.intNull
		nType_saapv = eRemoteDB.Constants.intNull
		nstatus_saapv = eRemoteDB.Constants.intNull
		scheckamend = String.Empty
		scheckrequest = String.Empty
		nInstitution = eRemoteDB.Constants.intNull
		
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
End Class






