Option Strict Off
Option Explicit On
Public Class Bank_Agree
	'%-------------------------------------------------------%'
	'% $Workfile:: Bank_Agree.cls                           $%'
	'% $Author:: Nvaplat51                                  $%'
	'% $Date:: 26/08/03 1:36p                               $%'
	'% $Revision:: 29                                       $%'
	'%-------------------------------------------------------%'
	'+     Column name                Type                            Length Prec  Scale Nullable TrimTrailingBlanks FixedLenNullInSource
	'+     -------------------------  ------------------------------- ------ ----- ----- -------- ------------------ ---------------------
	Public sType_BankAgree As String 'char       1                  no       yes                no
	Public nBank As Double 'number     10     0     0     no       (n/a)              (n/a)
	Public nAccount As Integer 'number     5      0     0     no       (n/a)              (n/a)
	Public dCompdate As Date 'date                          no       (n/a)              (n/a)
	Public nUsercode As Integer 'number     5      0     0     no       (n/a)              (n/a)
	Public nBank_Lider As Double 'number     5      0     0     no       (n/a)              (n/a)
	Public dAgree_Date As Date 'date                          no       (n/a)              (n/a)
	Public sClient As String
	
	'Variable de despligue
	Public sAcc_Number As String
	
	Public nTypeAgree As Integer
	
	'% Find: busca los datos correspondientes para un tipo de convenio
	Public Function Find(ByVal Type_BankAgree As String, ByVal Bank As Double, ByVal Account As Integer, Optional ByVal lblnFind As Boolean = False) As Boolean
		Dim lreaBank_Agree As eRemoteDB.Execute
		
		On Error GoTo Find_Err
		lreaBank_Agree = New eRemoteDB.Execute
		
		If (Type_BankAgree = sType_BankAgree) And (Account = nAccount) And Not lblnFind Then
			Find = True
		Else
			
			'+ Definición de parámetros para stored procedure 'insudb.reaBank_Agree'
			'+ Información leída el 11/01/2000 14:09:20
			With lreaBank_Agree
				.StoredProcedure = "reaBank_Agree"
				.Parameters.Add("sType_BankAgree", Type_BankAgree, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nBank", Bank, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nAccount", Account, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nTypeAgree", 1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				
				If .Run Then
					sType_BankAgree = .FieldToClass("sType_BankAgree")
					nBank = .FieldToClass("nBank")
					nAccount = .FieldToClass("nAccount")
					sAcc_Number = .FieldToClass("sAcc_Number")
					sClient = .FieldToClass("sClient")
					Find = True
					.RCloseRec()
				Else
					Find = False
				End If
			End With
		End If
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lreaBank_Agree may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lreaBank_Agree = Nothing
	End Function
	'% Find_Exist: Verifica si existe un convenio de banco.
	Public Function Find_Exist(ByVal Type_BankAgree As String, ByVal Bank As Double, ByVal Account As Integer, Optional ByVal nTypeAgree As Integer = 0, Optional ByVal bFind As Boolean = False) As Boolean
		Dim lrecreaBank_Agree As eRemoteDB.Execute
		Dim lintExists As Short
		
		On Error GoTo Find_Err
		
		If Type_BankAgree = Me.sType_BankAgree And Bank = Me.nBank And Account = Me.nAccount And nTypeAgree = Me.nTypeAgree And Not bFind Then
			Find_Exist = True
		Else
			lrecreaBank_Agree = New eRemoteDB.Execute
			
			With lrecreaBank_Agree
				.StoredProcedure = "valBank_Agree"
				.Parameters.Add("sType_BankAgree", Type_BankAgree, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nBank", Bank, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nAccount", Account, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nTypeAgree", nTypeAgree, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nExists", lintExists, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Run(False)
				Find_Exist = (.Parameters("nExists").Value = 1)
			End With
		End If
		
Find_Err: 
		If Err.Number Then
			Find_Exist = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecreaBank_Agree may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaBank_Agree = Nothing
	End Function
	
	Public Function Find_ExistMult(ByVal nBank_Lider As Double, ByVal nBank As Double, ByVal nType As Integer, Optional ByVal lblnFind As Boolean = False) As Boolean
		Dim lcount As Integer
		Dim lrecreaBank_Agree As eRemoteDB.Execute
		
		lrecreaBank_Agree = New eRemoteDB.Execute
		
		On Error GoTo Find_Err
		
		With lrecreaBank_Agree
			.StoredProcedure = "reaMultipac"
			.Parameters.Add("nBank_Lider", nBank_Lider, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBank", nBank, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("ntype", nType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 1, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Find_ExistMult = True
			Else
				Find_ExistMult = False
			End If
		End With
		
Find_Err: 
		If Err.Number Then
			Find_ExistMult = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecreaBank_Agree may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaBank_Agree = Nothing
	End Function
	'% Add: Agrega un registro a la tabla de bancos en convenio
	Public Function Add() As Boolean
		Dim lcreBank_Agree As eRemoteDB.Execute
		
		On Error GoTo Add_err
		lcreBank_Agree = New eRemoteDB.Execute
		
		'+ Definición de parámetros para stored procedure 'insudb.creBank_Agree'
		'+ Información leída el 11/10/2001
		
		With lcreBank_Agree
			.StoredProcedure = "creBank_Agree"
			
			.Parameters.Add("sType_BankAgree", sType_BankAgree, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBank", nBank, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAccount", nAccount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			Add = .Run(False)
		End With
Add_err: 
		If Err.Number Then
			Add = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lcreBank_Agree may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lcreBank_Agree = Nothing
	End Function
	'% AddMultipac: Agrega un registro a la tabla de bancos en Multipac
	Public Function AddMultipac() As Boolean
		Dim lcreMultipac As eRemoteDB.Execute
		
		On Error GoTo AddMultipac_err
		lcreMultipac = New eRemoteDB.Execute
		
		With lcreMultipac
			.StoredProcedure = "creMultipac"
			.Parameters.Add("nBank_Lider", Me.nBank_Lider, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBank", Me.nBank, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dAgree_Date", Me.dAgree_Date, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", Me.nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			AddMultipac = .Run(False)
		End With
		
AddMultipac_err: 
		If Err.Number Then
			AddMultipac = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lcreMultipac may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lcreMultipac = Nothing
	End Function
	'% UpdMultipac: Agrega un registro a la tabla de bancos en Multipac
	Public Function UpdMultipac() As Boolean
		Dim lcreMultipac As eRemoteDB.Execute
		
		On Error GoTo UpdMultipac_err
		lcreMultipac = New eRemoteDB.Execute
		
		With lcreMultipac
			.StoredProcedure = "updMultipac"
			.Parameters.Add("nBank_Lider", Me.nBank_Lider, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBank", Me.nBank, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dAgree_Date", Me.dAgree_Date, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			UpdMultipac = .Run(False)
		End With
		
UpdMultipac_err: 
		If Err.Number Then
			UpdMultipac = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lcreMultipac may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lcreMultipac = Nothing
	End Function
	'% Delete: Elimina un registro de la tabla bank_agree
	Public Function Delete() As Boolean
		Dim ldelBank_Agree As eRemoteDB.Execute
		
		On Error GoTo Delete_Err
		ldelBank_Agree = New eRemoteDB.Execute
		
		'+ Definición de parámetros para stored procedure 'insudb.delBank_Agree'
		'+ Información leída el 11/10/2001
		
		With ldelBank_Agree
			.StoredProcedure = "delBank_Agree"
			.Parameters.Add("sType_BankAgree", sType_BankAgree, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBank", nBank, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAccount", nAccount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Delete = .Run(False)
		End With
Delete_Err: 
		If Err.Number Then
			Delete = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object ldelBank_Agree may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		ldelBank_Agree = Nothing
	End Function
	'% DelMultipac: Elimina un registro de la tabla Multipac
	Public Function DelMultipac() As Boolean
		Dim ldelMultipac As eRemoteDB.Execute
		
		On Error GoTo DelMultipac_Err
		ldelMultipac = New eRemoteDB.Execute
		
		With ldelMultipac
			.StoredProcedure = "delMultipac"
			.Parameters.Add("nBank_Lider", Me.nBank_Lider, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBank", Me.nBank, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			DelMultipac = .Run(False)
		End With
		
DelMultipac_Err: 
		If Err.Number Then
			DelMultipac = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object ldelMultipac may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		ldelMultipac = Nothing
	End Function
	'%insValMCO741: Esta función se encarga de validar los datos introducidos en la zona de detalle
	'%para forma.
	Public Function insValMCO741(ByVal sCodispl As String, ByVal sType_BankAgree As String, ByVal nBank As Double, ByVal nAccount As Integer, ByVal sClient As String) As String
		Dim lerrTime As eFunctions.Errors
		
		Dim clsTypeAgree As Integer
		
		lerrTime = New eFunctions.Errors
		
		On Error GoTo insValMCO741_Err
		
		'+Se efectuan las validaciones concernientes al código de banco
		If nBank = 0 Or nBank = eRemoteDB.Constants.intNull Then
			Call lerrTime.ErrorMessage(sCodispl, 7004)
		End If
		
		'+Se realizan las validaciones concernientes al código de cta. bancaria.
		If nAccount = 0 Or nAccount = eRemoteDB.Constants.intNull Then
			Call lerrTime.ErrorMessage(sCodispl, 55004)
		End If
		
		'+ Se realizan las validaciones del código de cleinte
		
		If sClient = String.Empty Then
			Call lerrTime.ErrorMessage(sCodispl, 21118)
		End If
		
		'+Se realizan las validaciones concernientes a la existencia del registro.
		Dim clsBank_Agree As eCollection.Bank_Agree
		clsBank_Agree = New eCollection.Bank_Agree
		
		'+Se setea variable para saber si existe más de un convenio para el mismo
		'+tipo de convenio
		
		clsTypeAgree = 1
		
		'+Se valida que un mismo tipo de convenio no exista para un banco
		If clsBank_Agree.Find_Exist(sType_BankAgree, nBank, nAccount, clsTypeAgree) Then
			Call lerrTime.ErrorMessage(sCodispl, 55971)
		Else
			'+Se valida la existencia del registro.
			If clsBank_Agree.Find_Exist(sType_BankAgree, nBank, nAccount) Then
				Call lerrTime.ErrorMessage(sCodispl, 55553)
			End If
		End If
		
		insValMCO741 = lerrTime.Confirm
		
insValMCO741_Err: 
		If Err.Number Then
			insValMCO741 = "insValMCO741: " & Err.Description
		End If
		
		On Error GoTo 0
		'UPGRADE_NOTE: Object clsBank_Agree may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		clsBank_Agree = Nothing
		'UPGRADE_NOTE: Object lerrTime may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lerrTime = Nothing
	End Function
	'%insValMCO782_K: Esta función se encarga de validar los datos del banco multipac
	Public Function insValMCO782_K(ByVal sCodispl As String, ByVal nAction As Integer, ByVal nBank As Double) As String
		Dim lerrTime As eFunctions.Errors
		
		lerrTime = New eFunctions.Errors
		
		On Error GoTo insValMCO782_K_Err
		
		'+Se efectuan las validaciones concernientes al código de banco
		If nBank = 0 Or nBank = eRemoteDB.Constants.intNull Then
			Call lerrTime.ErrorMessage(sCodispl, 60493)
		End If
		
		'+Se valida si la acción a realizar es registrar
		'+Se valida que banco lider no este registrado como asociado en Multipac.
		Dim clsMultipac As eCollection.Bank_Agree
		If nAction = 301 Then
			clsMultipac = New eCollection.Bank_Agree
			If clsMultipac.Find_ExistMult(0, nBank, 2) Then
				Call lerrTime.ErrorMessage(sCodispl, 60494)
			End If
		End If
		
		insValMCO782_K = lerrTime.Confirm
		
insValMCO782_K_Err: 
		If Err.Number Then
			insValMCO782_K = "insValMCO782_K: " & Err.Description
		End If
		
		On Error GoTo 0
		'UPGRADE_NOTE: Object clsMultipac may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		clsMultipac = Nothing
		'UPGRADE_NOTE: Object lerrTime may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lerrTime = Nothing
	End Function
	'%insValMCO782: Esta función se encarga de validar los datos antes de insertar el registro en Multipac
	Public Function insValMCO782(ByVal sCodispl As String, ByVal sAction As String, ByVal nBank_Lider As Double, ByVal nBank As Double, ByVal dAgree_Date As Date) As String
		Dim lerrTime As eFunctions.Errors
		Dim clsMultipac As eCollection.Bank_Agree
		
		lerrTime = New eFunctions.Errors
		clsMultipac = New eCollection.Bank_Agree
		
		On Error GoTo insValMCO782_Err
		
		'+Se efectuan las validaciones concernientes al código de banco asociado
		If nBank = 0 Or nBank = eRemoteDB.Constants.intNull Then
			Call lerrTime.ErrorMessage(sCodispl, 60497)
		Else
			If sAction = "Add" Then
				If nBank = nBank_Lider Then
					Call lerrTime.ErrorMessage(sCodispl, 60507)
				Else
					'+Se valida que banco no este registrado como banco asociado Tipo = 2
					If clsMultipac.Find_ExistMult(0, nBank, 2) Then
						Call lerrTime.ErrorMessage(sCodispl, 60494)
					End If
					'+Se valida que banco asociado no este registrado como banco lider Tipo = 1
					If clsMultipac.Find_ExistMult(0, nBank, 1) Then
						Call lerrTime.ErrorMessage(sCodispl, 60508)
					End If
				End If
			End If
		End If
		
		'+Se valida que la fecha de registro no sea null
		If dAgree_Date = eRemoteDB.Constants.dtmNull Then
			Call lerrTime.ErrorMessage(sCodispl, 60498)
		Else
			'+Se valida que la fecha de registro no sea menor a la fecha actual
			If dAgree_Date < Today Then
				Call lerrTime.ErrorMessage(sCodispl, 60499)
			End If
		End If
		
		insValMCO782 = lerrTime.Confirm
		
insValMCO782_Err: 
		If Err.Number Then
			insValMCO782 = "insValMCO782: " & Err.Description
		End If
		
		On Error GoTo 0
		'UPGRADE_NOTE: Object clsMultipac may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		clsMultipac = Nothing
		'UPGRADE_NOTE: Object lerrTime may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lerrTime = Nothing
	End Function
	'*InsPostMCO741: Esta función se encarga de crear/eliminar los registros
	'*correspondientes en la tabla Bank_agree
	Public Function insPostMCO741(ByVal sAction As String, Optional ByVal sType_BankAgree As String = "", Optional ByVal nBank As Double = 0, Optional ByVal nAccount As Integer = 0, Optional ByVal nUsercode As Integer = 0, Optional ByVal sClient As String = "") As Boolean
		
		On Error GoTo insPostMCO741_err
		
		Me.sType_BankAgree = sType_BankAgree
		
		If nBank = eRemoteDB.Constants.intNull Then
			Me.nBank = 0
		Else
			Me.nBank = nBank
		End If
		
		If nAccount = eRemoteDB.Constants.intNull Then
			Me.nAccount = 0
		Else
			Me.nAccount = nAccount
		End If
		
		Me.sClient = sClient
		Me.nUsercode = nUsercode
		
		insPostMCO741 = True
		
		Select Case sAction
			
			'+Si la opción seleccionada es Registrar
			
			Case "Add"
				insPostMCO741 = Add()
				
				'+Si la opción seleccionada es Eliminar
				
			Case "Del"
				insPostMCO741 = Delete()
				
		End Select
		
insPostMCO741_err: 
		If Err.Number Then
			insPostMCO741 = False
		End If
		On Error GoTo 0
		
	End Function
	
	'*InsPostMCO782: Esta función se encarga de crear/eliminar los registros
	'*correspondientes en la tabla Multipac
	Public Function insPostMCO782(ByVal sAction As String, Optional ByVal nBank_Lider As Double = 0, Optional ByVal nBank As Double = 0, Optional ByVal dAgree_Date As Date = #12:00:00 AM#, Optional ByVal nUsercode As Integer = 0) As Boolean
		
		On Error GoTo insPostMCO782_err
		
		If nBank = eRemoteDB.Constants.intNull Then
			Me.nBank = 0
		Else
			Me.nBank = nBank
		End If
		
		If nBank_Lider = eRemoteDB.Constants.intNull Then
			Me.nBank_Lider = 0
		Else
			Me.nBank_Lider = nBank_Lider
		End If
		
		Me.dAgree_Date = dAgree_Date
		
		Me.nUsercode = nUsercode
		
		insPostMCO782 = True
		
		Select Case sAction
			
			'+Si la opción seleccionada es Registrar
			
			Case "Add"
				insPostMCO782 = AddMultipac()
				
				'+Si la opción seleccionada es Eliminar
				
			Case "Del"
				insPostMCO782 = DelMultipac()
				
				'+Si la opción seleccionada es Modificar
				
			Case "Update"
				
				insPostMCO782 = UpdMultipac()
				
		End Select
		
insPostMCO782_err: 
		If Err.Number Then
			insPostMCO782 = False
		End If
		On Error GoTo 0
		
	End Function
	
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		Me.nBank = eRemoteDB.Constants.intNull
		Me.nBank_Lider = eRemoteDB.Constants.intNull
		Me.dAgree_Date = eRemoteDB.Constants.dtmNull
		Me.nUsercode = eRemoteDB.Constants.intNull
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
End Class






