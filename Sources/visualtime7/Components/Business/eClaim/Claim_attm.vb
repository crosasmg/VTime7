Option Strict Off
Option Explicit On
Public Class Claim_attm
	'%-------------------------------------------------------%'
	'% $Workfile:: Claim_attm.cls                           $%'
	'% $Author:: Nvaplat61                                  $%'
	'% $Date:: 23/10/03 18.04                               $%'
	'% $Revision:: 10                                       $%'
	'%-------------------------------------------------------%'
	Public nClaim As Double
	Public nCase_num As Integer
	Public nDeman_type As Integer
	Public dEffecdate As Date
	Public sIllness As String
	Public dInit_Illdate As Date
	Public nService As Integer
	Public dNulldate As Date
	Public nUsercode As Integer
	Public sClientProf As String
	Public sClient As String
	Public nClinic As Integer
	Public nProf As Integer
	Public sHealth_system As String
	Public sHealth_sys_other As String
	
	Public nStatusInstance As Integer
	
	'% Find:
	Public Function Find(ByVal nClaim As Double, ByVal nCase_num As Integer, ByVal nDeman_type As Integer, ByVal dEffecdate As Date) As Boolean
		Dim lrecreaClaim_attm As eRemoteDB.Execute
		
		On Error GoTo Find_Err
		
		lrecreaClaim_attm = New eRemoteDB.Execute
		
		'+ Definición de parámetros para stored procedure 'insudb.reaClaim_attm'
		'+ Información leída el 14/07/2001 05:53:10 p.m.
		
		With lrecreaClaim_attm
			.StoredProcedure = "reaClaim_attm"
			.Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCase_num", nCase_num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDeman_type", nDeman_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Me.nClaim = .FieldToClass("nClaim")
				Me.nCase_num = .FieldToClass("nCase_num")
				Me.nDeman_type = .FieldToClass("nDeman_type")
				sClient = .FieldToClass("sClient")
				Me.dEffecdate = .FieldToClass("dEffecdate")
				sIllness = .FieldToClass("sIllness")
				dInit_Illdate = .FieldToClass("dInit_illdate")
				nService = .FieldToClass("nService")
				dNulldate = .FieldToClass("dNulldate")
				sClientProf = .FieldToClass("sClientProf")
				sHealth_sys_other = .FieldToClass("sHealth_sys_other")
				sHealth_system = .FieldToClass("sHealth_system")
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
		On Error GoTo 0
		lrecreaClaim_attm = Nothing
	End Function
	
	'% Add:
	Public Function Add() As Boolean
		Dim lrecinsClaim_attm As eRemoteDB.Execute
		
		On Error GoTo Add_err
		
		lrecinsClaim_attm = New eRemoteDB.Execute
		
		'+ Definición de parámetros para stored procedure 'insudb.insClaim_attm'
		'+ Información leída el 14/07/2001 05:59:55 p.m.
		
		With lrecinsClaim_attm
			.StoredProcedure = "insClaim_attm"
			.Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCase_num", nCase_num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDeman_type", nDeman_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sIllness", sIllness, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dInit_illdate", dInit_Illdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nService", IIf(nService = 0, eRemoteDB.Constants.intNull, nService), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClientProf", sClientProf, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sHealth_system", sHealth_system, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sHealth_sys_other", sHealth_sys_other, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 32, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUserCode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Add = .Run(False)
		End With
		
Add_err: 
		If Err.Number Then
			Add = False
		End If
		On Error GoTo 0
		lrecinsClaim_attm = Nothing
	End Function
	
	'% insValSI028: En esta funcion se realizan las validaciones correspondientes a los campos
	'%              de la ventana.
	Public Function insValSI028(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Integer, ByVal nCertif As Integer, ByVal dEffecdate As Date, ByVal nClaim As Double, ByVal nCase_num As Integer, ByVal nDeman_type As Integer, ByVal sIllness As String, ByVal nClinic As Integer, ByVal sClient As String, ByVal sLastName As String, ByVal sFirstName As String, ByVal nService As Integer, ByVal nProf As Integer, ByVal sClientProf As String, ByVal sLastNameCP As String, ByVal sFirstNameCP As String, ByVal dInitIlldate As Date, ByVal sHealth_system As String, ByVal sHealth_sys_other As String) As String
		
		Dim lrecInsValSI028 As eRemoteDB.Execute
		Dim lobjErrors As eFunctions.Errors
		Dim lstrError As String
		
		
		On Error GoTo insvalSI028_err
		
		lrecInsValSI028 = New eRemoteDB.Execute
		With lrecInsValSI028
			.StoredProcedure = "InsSi028pkg.InsValSI028"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCase_num", nCase_num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDeman_type", nDeman_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sIllness", sIllness, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nHospital", nClinic, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sLastname", sLastName, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 19, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sFirstName", sFirstName, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 19, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nService", IIf(nService = 0, eRemoteDB.Constants.intNull, nService), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProvider", nProf, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClientProf", sClientProf, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sLastNameCP", sLastNameCP, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 19, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sFirstNameCP", sFirstNameCP, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 19, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dInit_Illdate", dInitIlldate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sHealth_system", sHealth_system, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sHealth_sys_other", sHealth_sys_other, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 32, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("Arrayerrors", String.Empty, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 4000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Run(False)
			lstrError = .Parameters("Arrayerrors").Value
			
			If lstrError <> String.Empty Then
				lobjErrors = New eFunctions.Errors
				With lobjErrors
					.ErrorMessage("SI028",  ,  ,  ,  ,  , lstrError)
					insValSI028 = lobjErrors.Confirm
				End With
				lobjErrors = Nothing
			End If
			
		End With
		
insvalSI028_err: 
		If Err.Number Then
			insValSI028 = "insValSI028: " & Err.Description
		End If
		On Error GoTo 0
		
	End Function
	
	'% insValTab_am_cli: función para validar la no existencia del código de clínica
	'%                   en la tabla de exclusiones de clínica excluidas para el producto
	Private Function insValTab_am_cli(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, ByVal nClinic As Integer) As Boolean
		Dim lclsTab_am_cli As Tab_am_cli
		lclsTab_am_cli = New Tab_am_cli
		
		insValTab_am_cli = False
		
		If lclsTab_am_cli.Find_Count(nBranch, nProduct, dEffecdate) Then
			If lclsTab_am_cli.nHospital = 0 Then
				insValTab_am_cli = True
			ElseIf lclsTab_am_cli.nHospital > 0 Then 
				If lclsTab_am_cli.Find(nBranch, nProduct, nClinic, dEffecdate) Then
					insValTab_am_cli = True
				End If
			End If
		End If
		lclsTab_am_cli = Nothing
	End Function
	
	'% insValSI028Upd: En esta funcion se realizan las validaciones correspondientes a los
	'%                 del grid de la ventana.
	Public Function insValSI028Upd(ByVal nClaim As Double, ByVal nCase_num As Integer, ByVal nDeman_type As Integer, ByVal dEffecdate As Date, ByVal sDescript As String, ByVal nStatus As Integer, ByVal sAction As String) As String
		Dim lobjErrors As eFunctions.Errors
		Dim lclsValues As eFunctions.Values
		Dim lclsCl_diagnostic As Cl_diagnostic
		Dim lcolCl_diagnostic As Cl_diagnostics
        Dim lstrError As String = ""
        Dim lstrSep As String
		
		On Error GoTo insvalSI028Upd_err
		
		lstrSep = "||"
		
		lclsValues = New eFunctions.Values
		lcolCl_diagnostic = New Cl_diagnostics
		
		nCase_num = IIf(nCase_num = eRemoteDB.Constants.intNull, 0, nCase_num)
		nDeman_type = IIf(nDeman_type = eRemoteDB.Constants.intNull, 0, nDeman_type)
		
		'+Si no se ha registrado ningún diagnóstico, se envía el error
		If sAction = "Add" Then
			If lcolCl_diagnostic.Find(nClaim, nCase_num, nDeman_type, dEffecdate) Then
				For	Each lclsCl_diagnostic In lcolCl_diagnostic
					If lclsCl_diagnostic.dDiag_date = dEffecdate Then
						lstrError = lstrError & "||" & "4348"
						Exit For
					End If
				Next lclsCl_diagnostic
			End If
		End If
		
		'+Se valida Campo Fecha
		If dEffecdate = eRemoteDB.Constants.dtmNull Then
			lstrError = lstrError & "||" & "4349"
		End If
		
		'+Se valida Campo Descripción
		If sDescript = String.Empty Then
			lstrError = lstrError & "||" & "4350"
		End If
		
		
		'+ Se valida el campo estado
		If nStatus = 0 Or nStatus = eRemoteDB.Constants.intNull Then
			lstrError = lstrError & "||" & "10826"
		End If
		
		If lstrError <> String.Empty Then
			lstrError = Mid(lstrError, 3)
			lobjErrors = New eFunctions.Errors
			With lobjErrors
				.ErrorMessage("SI028",  ,  ,  ,  ,  , lstrError)
				insValSI028Upd = .Confirm()
			End With
			lobjErrors = Nothing
		Else
			insValSI028Upd = lstrError
		End If
		
insvalSI028Upd_err: 
		If Err.Number Then
			insValSI028Upd = "insvalSI028Upd: " & Err.Description
		End If
		On Error GoTo 0
		
		lclsValues = Nothing
	End Function
	
	'% insPreSI028: se realiza la lectura de los datos de la página
	Public Function insPreSI028(ByVal nClaim As Double, ByVal nCase_num As Integer, ByVal nDeman_type As Integer, ByVal dEffecdate As Date, Optional ByVal sReloadAction As String = "", Optional ByVal sIllness As String = "", Optional ByVal sClient As String = "", Optional ByVal nService As Integer = 0, Optional ByVal sClientProf As String = "", Optional ByVal dInit_Illdate As Date = #12:00:00 AM#, Optional ByVal sHealth_sys_other As String = "") As Boolean
		Dim lclsTab_Provider As Tab_Provider
		
		On Error GoTo insPreSI028_Err
		
		lclsTab_Provider = New Tab_Provider
		
		'+ Si no se está recargando la página
		If sReloadAction = String.Empty Then
			If Find(nClaim, nCase_num, nDeman_type, dEffecdate) Then
				insPreSI028 = True
			End If
		Else
			insPreSI028 = True
			Me.sIllness = sIllness
			Me.sClient = sClient
			Me.nService = nService
			Me.sClientProf = sClientProf
			Me.dInit_Illdate = dInit_Illdate
			Me.sHealth_sys_other = sHealth_sys_other
		End If
		
		nClinic = IIf(lclsTab_Provider.FindProviderByCode(eRemoteDB.Constants.intNull, 1, Me.sClient), lclsTab_Provider.nProvider, eRemoteDB.Constants.intNull)
		
		nProf = IIf(lclsTab_Provider.FindProviderByCode(eRemoteDB.Constants.intNull, 3, Me.sClientProf), lclsTab_Provider.nProvider, eRemoteDB.Constants.intNull)
		
insPreSI028_Err: 
		If Err.Number Then
			insPreSI028 = False
		End If
		On Error GoTo 0
	End Function
	
	'% insPostSI028: se realizan las actualizaciones de la página
	Public Function insPostSI028(ByVal nClaim As Double, ByVal nCase_num As Integer, ByVal nDeman_type As Integer, ByVal sClient As String, ByVal sLastName As String, ByVal sLastName2 As String, ByVal sFirstName As String, ByVal dEffecdate As Date, ByVal sIllness As String, ByVal dInit_Illdate As Date, ByVal nService As Integer, ByVal sClientProf As String, ByVal sLastNameCP As String, ByVal sLastName2CP As String, ByVal sFirstNameCP As String, ByVal sHealth_system As String, ByVal sHealth_sys_other As String, ByVal nUsercode As Integer) As Boolean
		Dim lrecInsPostSI028 As eRemoteDB.Execute
		Dim lclsClaim_win As Object
		Dim lclsClient As eClient.Client
		Dim lclsClaim As eClaim.Claim
		Dim lobjErrors As eFunctions.Errors
		Dim lstrError As String
		Dim nStatus As Short
		
		On Error GoTo insPostSI028_err
		
		insPostSI028 = True
		'+ Se registra el cliente en el sistema, con los datos mínimos en caso de no existir.
		'+ Para el caso de "Clínica" se registra como persona "Jurídica"
		
		lrecInsPostSI028 = New eRemoteDB.Execute
		With lrecInsPostSI028
			.StoredProcedure = "InsSi028pkg.InsPosSI028"
			.Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCase_num", nCase_num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDeman_type", nDeman_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sLastname", sLastName, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 19, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sLastname2", sLastName2, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 19, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sFirstName", sFirstName, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 19, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sIllness", sIllness, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dInit_illdate", dInit_Illdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nService", IIf(nService = 0, eRemoteDB.Constants.intNull, nService), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClientProf", sClientProf, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sLastNameCP", sLastNameCP, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 19, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sLastName2CP", sLastName2CP, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 19, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sFirstNameCP", sFirstNameCP, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 19, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sHealth_system", sHealth_system, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sHealth_sys_other", sHealth_sys_other, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 32, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUserCode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nStatus", 1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Run(False)
			
			insPostSI028 = .Parameters("nStatus").Value = 1
			
		End With
		
		
insPostSI028_err: 
		If Err.Number Then
			insPostSI028 = False
		End If
		On Error GoTo 0
		lclsClaim_win = Nothing
	End Function
	
	'% insPostSI028Upd: se realizan las actualizaciones de la página
	Public Function insPostSI028Upd(ByVal nClaim As Double, ByVal nCase_num As Integer, ByVal nDeman_type As Integer, ByVal dDiag_date As Date, ByVal dEffecdate As Date, ByVal nUsercode As Integer, ByVal sAction As String, Optional ByVal sDescript As String = "", Optional ByVal nNotenum As Integer = 0, Optional ByVal nEvalStat As Integer = 0) As Boolean
		Dim lclsDiagnostic As Cl_diagnostic
		
		On Error GoTo insPostSI028Upd_err
		
		lclsDiagnostic = New Cl_diagnostic
		With lclsDiagnostic
			.nClaim = nClaim
			.nCase_num = nCase_num
			.nDeman_type = nDeman_type
			.dDiag_date = dDiag_date
			.dEffecdate = dEffecdate
			.sDescript = sDescript
			.nNotenum = nNotenum
			.nEvalStat = nEvalStat
			.nUsercode = nUsercode
			
			'+Si la acción es insertar o actualizar
			If sAction = "Add" Or sAction = "Update" Then
				.nStatusInstance = 1
				
				'+Si la acción es eliminar
			Else
				.nStatusInstance = 2
			End If
			
			insPostSI028Upd = .Add
		End With
		
insPostSI028Upd_err: 
		If Err.Number Then
			insPostSI028Upd = False
		End If
		On Error GoTo 0
	End Function
End Class






