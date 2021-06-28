Option Strict Off
Option Explicit On
Public Class Nopayroll
	'%-------------------------------------------------------%'
	'% $Workfile:: Nopayroll.cls                            $%'
	'% $Author:: Nvaplat41                                  $%'
	'% $Date:: 9/10/03 19.01                                $%'
	'% $Revision:: 21                                       $%'
	'%-------------------------------------------------------%'
	
	'-
	'- Estructura de tabla nopayroll al 09-09-2002 12:26:07
	'-  Property                       Type         DBType   Size Scale  Prec  Null
	Public sCertype As String ' CHAR       1    0     0    N
	Public nBranch As Integer ' NUMBER     22   0     5    N
	Public nProduct As Integer ' NUMBER     22   0     5    N
	Public nPolicy As Double ' NUMBER     22   0     10   N
	Public nCertif As Double ' NUMBER     22   0     10   N
	Public nModulec As Integer ' NUMBER     22   0     5    N
	Public nCover As Integer ' NUMBER     22   0     5    N
	Public nRole As Integer ' NUMBER     22   0     5    N
	Public nGroup As Integer ' NUMBER     22   0     5    N
	Public dEffecdate As Date ' DATE       7    0     0    N
	Public nMovement As Integer ' NUMBER     22   0     10   S
	Public dNulldate As Date ' DATE       7    0     0    S
	Public nQlifes As Integer ' NUMBER     22   0     5    S
	Public dCompdate As Date ' DATE       7    0     0    N
	Public nUsercode As Integer ' NUMBER     22   0     5    N
	
	'- Variables auxiliares
	
	Public mcolNopayroll As Nopayrolls
	
	'% Find: se buscan los datos de la tabla
	Public Function Find(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nModulec As Integer, ByVal nCover As Integer, ByVal nRole As Integer, ByVal nGroup As Integer, ByVal dEffecdate As Date) As Boolean
		Dim lrecRemote As eRemoteDB.Execute
		
		On Error GoTo Find_Err
		
		lrecRemote = New eRemoteDB.Execute
		
		'+Definición de parámetros para stored procedure 'reaNopayroll'
		'+Información leída el 06/02/2002
		
		With lrecRemote
			.StoredProcedure = "reaNopayroll"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRole", nRole, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nGroup", nGroup, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(True) Then
				Me.sCertype = sCertype
				Me.nBranch = nBranch
				Me.nProduct = nProduct
				Me.nPolicy = nPolicy
				Me.nCertif = nCertif
				Me.nModulec = nModulec
				Me.nCover = nCover
				Me.nRole = nRole
				Me.nGroup = nGroup
				Me.dEffecdate = dEffecdate
				nMovement = .FieldToClass("nMovement")
				nQlifes = .FieldToClass("nQlifes")
				.RCloseRec()
				Find = True
			Else
				Find = False
			End If
		End With
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
		
		'UPGRADE_NOTE: Object lrecRemote may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecRemote = Nothing
	End Function
	
	'% inspostVI811: se realizan las actualizaciones de la página
	Public Function inspostVI811(ByVal sAction As String, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nModulec As Integer, ByVal nCover As Integer, ByVal nRole As Integer, ByVal nGroup As Integer, ByVal dEffecdate As Date, ByVal nQlifes As Integer, ByVal nUsercode As Integer) As Boolean
		Dim lclsPolicy_Win As Policy_Win
		On Error GoTo inspostVI811_err
		lclsPolicy_Win = New Policy_Win
		
		With Me
			.sCertype = sCertype
			.nBranch = nBranch
			.nProduct = nProduct
			.nPolicy = nPolicy
			.nCertif = nCertif
			.nModulec = IIf(nModulec = eRemoteDB.Constants.intNull, 0, nModulec)
			.nCover = nCover
			.nRole = nRole
			.nGroup = IIf(nGroup = eRemoteDB.Constants.intNull, 0, nGroup)
			.dEffecdate = dEffecdate
			.nQlifes = nQlifes
			.nUsercode = nUsercode
		End With
		
		Select Case sAction
			Case "Add"
				inspostVI811 = Add
			Case "Update"
				inspostVI811 = Update(2)
			Case "Del"
				inspostVI811 = Delete
		End Select
		
		If inspostVI811 Then
			If Find_Exist(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate) Then
				inspostVI811 = lclsPolicy_Win.Add_PolicyWin(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nUsercode, "VI811", "2")
			Else
				inspostVI811 = lclsPolicy_Win.Add_PolicyWin(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nUsercode, "VI811", "1")
			End If
		End If
		
inspostVI811_err: 
		If Err.Number Then
			inspostVI811 = False
		End If
		On Error GoTo 0
		
		'UPGRADE_NOTE: Object lclsPolicy_Win may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsPolicy_Win = Nothing
	End Function
	
	'% insvalVI811Upd: se realizan las validaciones de la parte repetitiva de la página
	Public Function insvalVI811Upd(ByVal sAction As String, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nGroup As Integer, ByVal nModulec As Integer, ByVal nCover As Integer, ByVal nQLife As Integer, ByVal nRole As Integer) As String
		Dim lclsErrors As eFunctions.Errors
		Dim lclsValues As eFunctions.Values
		Dim lclsGroups As Groups
		Dim lclsModul_co_gp As Modul_co_gp
		Dim lclsNopayroll As Nopayroll
		
		On Error GoTo insvalVI811Upd_err
		
		lclsErrors = New eFunctions.Errors
		lclsValues = New eFunctions.Values
		lclsGroups = New Groups
		lclsModul_co_gp = New Modul_co_gp
		lclsNopayroll = New Nopayroll
		
		If nGroup = eRemoteDB.Constants.intNull Then
			If lclsGroups.valGroupExist(sCertype, nBranch, nProduct, nPolicy, dEffecdate) Then
				'+ Si se definieron grupos para la póliza, debe estar lleno
				Call lclsErrors.ErrorMessage("VI811", 3308)
			End If
		End If
		
		If nModulec = eRemoteDB.Constants.intNull Then
			If lclsModul_co_gp.valExistsModul_O(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nGroup) Then
				'+ Si se definieron grupos para la póliza, debe estar lleno
				Call lclsErrors.ErrorMessage("VI811", 3678)
			End If
		End If
		
		'+ Campo cobertura.
		If nCover = eRemoteDB.Constants.intNull Then
			Call lclsErrors.ErrorMessage("VI811", 3245)
		Else
			With lclsValues.Parameters
				.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Add("sCovergen", "2", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			End With
			'+ Debe estar asociado al ramo/producto/módulo
			If Not lclsValues.IsValid("TabGen_cover3", CStr(nCover), True) Then
				Call lclsErrors.ErrorMessage("VI811", 55707)
			End If
		End If
		
		'+ Campo Tipo.
		If nRole = eRemoteDB.Constants.intNull Then
			Call lclsErrors.ErrorMessage("VI811", 10241)
		Else
			With lclsValues.Parameters
				.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			End With
			'+ Debe estar asociado al ramo/producto/módulo/cobertura
			If Not lclsValues.IsValid("tabTab_covrol3", CStr(nRole), True) Then
				Call lclsErrors.ErrorMessage("VI811", 55708)
			End If
		End If
		
		'+ Campo Cantidad.
		If nQLife = eRemoteDB.Constants.intNull Then
			Call lclsErrors.ErrorMessage("VI811", 3332)
		Else
			If nQLife <= 0 Then
				Call lclsErrors.ErrorMessage("VI811", 55709)
			End If
		End If
		
		nModulec = IIf(nModulec = eRemoteDB.Constants.intNull, 0, nModulec)
		nGroup = IIf(nGroup = eRemoteDB.Constants.intNull, 0, nGroup)
		
		If sAction = "Add" Then
			'+ El registro no debe existir en la tabla
			If Find(sCertype, nBranch, nProduct, nPolicy, nCertif, nModulec, nCover, nRole, nGroup, dEffecdate) Then
				Call lclsErrors.ErrorMessage("VI811", 55710)
			End If
		End If
		
		insvalVI811Upd = lclsErrors.Confirm
		
insvalVI811Upd_err: 
		If Err.Number Then
			insvalVI811Upd = insvalVI811Upd & Err.Description
		End If
		On Error GoTo 0
		
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		'UPGRADE_NOTE: Object lclsGroups may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsGroups = Nothing
		'UPGRADE_NOTE: Object lclsModul_co_gp may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsModul_co_gp = Nothing
		'UPGRADE_NOTE: Object lclsNopayroll may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsNopayroll = Nothing
		'UPGRADE_NOTE: Object lclsValues may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsValues = Nothing
	End Function
	
	'% insvalVI811: se realizan las validaciones de la página
	Public Function insvalVI811(ByVal sAction As String, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nGroup As Integer, ByVal nModulec As Integer) As String
		Dim lclsErrors As eFunctions.Errors
		Dim lclsValues As eFunctions.Values
		Dim lclsGroups As Groups
		Dim lclsModul_co_gp As Modul_co_gp
		
		On Error GoTo insvalVI811_err
		
		lclsErrors = New eFunctions.Errors
		lclsValues = New eFunctions.Values
		lclsGroups = New Groups
		lclsModul_co_gp = New Modul_co_gp
		
		If nGroup = eRemoteDB.Constants.intNull Then
			If lclsGroups.valGroupExist(sCertype, nBranch, nProduct, nPolicy, dEffecdate) Then
				'+ Si se definieron grupos para la póliza, debe estar lleno
				Call lclsErrors.ErrorMessage("VI811", 3308)
			End If
		End If
		
		If nModulec = eRemoteDB.Constants.intNull Then
			If lclsModul_co_gp.valExistsModul_O(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nGroup) Then
				'+ Si se definieron grupos para la póliza, debe estar lleno
				Call lclsErrors.ErrorMessage("VI811", 3678)
			End If
		End If
		'+ Se valida que se hayan ingresado registros al aceptar
		If Not Find_Exist(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate) Then
			Call lclsErrors.ErrorMessage("VI811", 55901)
		End If
		
		insvalVI811 = lclsErrors.Confirm
		
insvalVI811_err: 
		If Err.Number Then
			insvalVI811 = insvalVI811 & Err.Description
		End If
		On Error GoTo 0
		
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		'UPGRADE_NOTE: Object lclsGroups may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsGroups = Nothing
		'UPGRADE_NOTE: Object lclsModul_co_gp may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsModul_co_gp = Nothing
		'UPGRADE_NOTE: Object lclsValues may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsValues = Nothing
	End Function
	
	
	'% Add: se inserta un registro en la tabla
	Private Function Add() As Boolean
		Add = Update(1)
	End Function
	
	'% Delete: se elimina un registro en la tabla
	Private Function Delete() As Boolean
		Delete = Update(3)
	End Function
	
	'% Update: actualiza la informacón de la tabla
	Private Function Update(Optional ByVal nAction As Integer = 0) As Boolean
		Dim lclsExecute As eRemoteDB.Execute
		On Error GoTo Update_Err
		lclsExecute = New eRemoteDB.Execute
		
		'+Definición de parámetros para stored procedure 'insupdNopayroll'
		'+Información leída el 06/02/2002
		
		With lclsExecute
			.StoredProcedure = "insupdNopayroll"
			.Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRole", nRole, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nGroup", nGroup, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nQlifes", nQlifes, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				Update = True
			End If
		End With
		
Update_Err: 
		If Err.Number Then
			Update = False
		End If
		'UPGRADE_NOTE: Object lclsExecute may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsExecute = Nothing
	End Function
	
	'% Find_Exist: se busca si quedan registros en la tabla
	Public Function Find_Exist(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date) As Boolean
		Dim lrecreaNopayroll_exist As eRemoteDB.Execute
		
		On Error GoTo reaNopayroll_exist_Err
		
		lrecreaNopayroll_exist = New eRemoteDB.Execute
		
		'+
		'+ Definición de store procedure reaNopayroll_exist al 09-10-2002 12:40:06
		'+
		With lrecreaNopayroll_exist
			.StoredProcedure = "reaNopayroll_exist"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(True) Then
				Find_Exist = True
			Else
				Find_Exist = False
			End If
		End With
		
reaNopayroll_exist_Err: 
		If Err.Number Then
			Find_Exist = False
		End If
		
		'UPGRADE_NOTE: Object lrecreaNopayroll_exist may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaNopayroll_exist = Nothing
		On Error GoTo 0
	End Function
	
	'* Class_Initialize: se controla la apertura de la clase
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		mcolNopayroll = New Nopayrolls
		sCertype = String.Empty
		nBranch = eRemoteDB.Constants.intNull
		nProduct = eRemoteDB.Constants.intNull
		nPolicy = eRemoteDB.Constants.intNull
		nCertif = eRemoteDB.Constants.intNull
		nModulec = eRemoteDB.Constants.intNull
		nCover = eRemoteDB.Constants.intNull
		nRole = eRemoteDB.Constants.intNull
		nGroup = eRemoteDB.Constants.intNull
		dEffecdate = eRemoteDB.Constants.dtmNull
		nMovement = eRemoteDB.Constants.intNull
		nQlifes = eRemoteDB.Constants.intNull
		nUsercode = eRemoteDB.Constants.intNull
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'* Class_Terminate: Se controla la destrucción de la clase
	'UPGRADE_NOTE: Class_Terminate was upgraded to Class_Terminate_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Terminate_Renamed()
		'UPGRADE_NOTE: Object mcolNopayroll may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mcolNopayroll = Nothing
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
End Class






