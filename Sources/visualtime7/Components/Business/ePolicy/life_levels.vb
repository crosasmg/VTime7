Option Strict Off
Option Explicit On
Public Class life_levels
	'%-------------------------------------------------------%'
	'% $Workfile:: life_levels.cls                          $%'
	'% $Author:: Nvaplat41                                  $%'
	'% $Date:: 9/10/03 19.01                                $%'
	'% $Revision:: 34                                       $%'
	'%-------------------------------------------------------%'
	'+
	'+ Estructura de tabla insudb.life_levels al 02-05-2002 12:42:31
	'+     Property                Type         DBType   Size Scale  Prec  Null
	'+-------------------------------------------------------------------------
	Public sCertype As String ' CHAR       1    0     0    N
	Public nBranch As Integer ' NUMBER     22   0     5    N
	Public nProduct As Integer ' NUMBER     22   0     5    N
	Public nPolicy As Double ' NUMBER     22   0     10   N
	Public nCertif As Double ' NUMBER     22   0     10   N
	Public nGroup As Integer ' NUMBER     22   0     5    N
	Public sTyplevel As String ' CHAR       1    0     0    N
	Public nLevel As Integer ' NUMBER     22   0     5    N
	Public dEffecdate As Date ' DATE       7    0     0    N
	Public nId As Integer ' NUMBER     22   0     5    N
	Public nCapital As Double ' NUMBER     22   0     12   S
	Public nInsured As Integer ' NUMBER     22   0     5    S
	Public dNulldate As Date ' DATE       7    0     0    S
	Public dCompdate As Date ' DATE       7    0     0    N
	Public nCurrency As Integer ' NUMBER     22   0     5    S
	Public sClient As String ' CHAR       14   0     0    S
	Public nUsercode As Integer ' NUMBER     22   0     5    N
	
	'- Variable auxiliar
	Public nExists As Integer
	
	'%InsUpdlife_levels: Se encarga de actualizar la tabla life_levels
	Private Function InsUpdlife_levels(ByVal nAction As Integer) As Boolean
		Dim lrecinsUpdlife_levels As eRemoteDB.Execute
		On Error GoTo insUpdlife_levels_Err
		
		lrecinsUpdlife_levels = New eRemoteDB.Execute
		
		With lrecinsUpdlife_levels
			.StoredProcedure = "insUpdlife_levels"
			.Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nGroup", IIf(nGroup = eRemoteDB.Constants.intNull, 0, nGroup), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sTyplevel", sTyplevel, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nLevel", nLevel, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nId", nId, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCapital", nCapital, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInsured", nInsured, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			InsUpdlife_levels = .Run(False)
		End With
		
insUpdlife_levels_Err: 
		If Err.Number Then
			InsUpdlife_levels = False
		End If
		'UPGRADE_NOTE: Object lrecinsUpdlife_levels may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsUpdlife_levels = Nothing
		On Error GoTo 0
		
	End Function
	
	'%Add: Crea un registro en la tabla
	Public Function Add() As Boolean
		Add = InsUpdlife_levels(1)
	End Function
	
	'%Update: Actualiza un registro en la tabla
	Public Function Update() As Boolean
		Update = InsUpdlife_levels(2)
	End Function
	
	'%Delete: Borra un registro en la tabla
	Public Function Delete() As Boolean
		Delete = InsUpdlife_levels(3)
	End Function
	
	'%Find_levels: lee un registro en la tabla
	Public Function Find_levels(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nGroup As Integer, ByVal sTyplevel As String, ByVal nLevel As Integer, ByVal dEffecdate As Date, ByVal sClient As String) As Object
		Dim lrecreaLife_levels_a As eRemoteDB.Execute
		Dim lclsreaLife_levels_a As life_levels
		
		On Error GoTo reaLife_levels_a_Err
		
		lrecreaLife_levels_a = New eRemoteDB.Execute
		
		With lrecreaLife_levels_a
			.StoredProcedure = "reaLife_levels_a"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nGroup", IIf(nGroup = eRemoteDB.Constants.intNull, 0, nGroup), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sTyplevel", sTyplevel, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nLevel", nLevel, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(True) Then
				Find_levels = True
				Me.sCertype = .FieldToClass("sCertype")
				Me.nBranch = .FieldToClass("nBranch")
				Me.nProduct = .FieldToClass("nProduct")
				Me.nPolicy = .FieldToClass("nPolicy")
				Me.nCertif = .FieldToClass("nCertif")
				Me.nGroup = .FieldToClass("nGroup")
				Me.sTyplevel = .FieldToClass("sTyplevel")
				Me.nLevel = .FieldToClass("nLevel")
				Me.dEffecdate = .FieldToClass("dEffecdate")
				Me.nId = .FieldToClass("nId")
				Me.nCapital = .FieldToClass("nCapital")
				Me.nInsured = .FieldToClass("nInsured")
				Me.dNulldate = .FieldToClass("dNulldate")
				Me.nCurrency = .FieldToClass("nCurrency")
				Me.sClient = .FieldToClass("sClient")
			Else
				Find_levels = False
			End If
		End With
		
reaLife_levels_a_Err: 
		If Err.Number Then
			Find_levels = False
		End If
		'UPGRADE_NOTE: Object lrecreaLife_levels_a may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaLife_levels_a = Nothing
		On Error GoTo 0
		
	End Function
	
	'%InsValvi662: Validaciones de la transacción(Folder)
	'%              Tabla de control de prima mínima(vi662)
	Public Function InsValVI662(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nGroup As Integer, ByVal sTyplevel As String, ByVal nLevel As Integer, ByVal dEffecdate As Date, ByVal nCapital As Double, ByVal nInsured As Integer, ByVal nCurrency As Integer, ByVal sClient As String, ByVal nUsercode As Integer, ByVal sAction As String) As String
		Dim lclsErrors As eFunctions.Errors
		Dim lclsRoles As Roles
		
		On Error GoTo InsValvi662_Err
		lclsErrors = New eFunctions.Errors
		
		With lclsErrors
			If nLevel = eRemoteDB.Constants.intNull Then
				.ErrorMessage("VI662", 55684)
			End If
			
			'+Si el curso es básico
			If sTyplevel = "1" Then
				If nCertif = 0 And nCapital = eRemoteDB.Constants.intNull Then
					.ErrorMessage("VI662", 55680)
				End If
				If nCertif > 0 Then
					lclsRoles = New ePolicy.Roles
					If lclsRoles.Count_By_Role(sCertype, nBranch, nProduct, nPolicy, nCertif, Roles.eRoles.eRolChild, dEffecdate) < 1 Then
						If nInsured = eRemoteDB.Constants.intNull Then
							.ErrorMessage("VI662", 55681)
						End If
					Else
						If sClient = String.Empty Then
							.ErrorMessage("VI662", 55682)
						End If
					End If
					'+ Verifica si existe un registro el la tabla life_levels
					If sAction = "Add" Then
						If Find_levels(sCertype, nBranch, nProduct, nPolicy, nCertif, nGroup, sTyplevel, nLevel, dEffecdate, sClient) Then
							.ErrorMessage("VI662", 55693)
						End If
					End If
				Else
					'+ Verifica si existe un registro el la tabla life_levels
					If sAction = "Add" Then
						If Find_levels(sCertype, nBranch, nProduct, nPolicy, nCertif, nGroup, sTyplevel, nLevel, dEffecdate, CStr(eRemoteDB.Constants.intNull)) Then
							.ErrorMessage("VI662", 55904)
						End If
					End If
				End If
			Else
				'+ Si el curso es universitario
				If nCertif = 0 Then
					'+ Verifica si existe un registro el la tabla life_levels
					If sAction = "Add" Then
						If Find_levels(sCertype, nBranch, nProduct, nPolicy, nCertif, nGroup, sTyplevel, nLevel, dEffecdate, CStr(eRemoteDB.Constants.intNull)) Then
							.ErrorMessage("VI662", 55904)
						End If
					End If
					If nCapital = eRemoteDB.Constants.intNull Then
						.ErrorMessage("VI662", 55680)
					End If
				End If
			End If
			
			'+ Validación del campo moneda.
			If nCurrency <= 0 Then
				.ErrorMessage("VI662", 750024)
			End If
			
			InsValVI662 = .Confirm
		End With
		
InsValvi662_Err: 
		If Err.Number Then
			InsValVI662 = "InsValvi662: " & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		'UPGRADE_NOTE: Object lclsRoles may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsRoles = Nothing
		
	End Function
	
	'%InsPostvi662: Ejecuta el post de la transacción
	'%               Tabla de control de prima mínima(vi662)
	Public Function InsPostVI662(ByVal sAction As String, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nGroup As Integer, ByVal sTyplevel As String, ByVal nLevel As Integer, ByVal dEffecdate As Date, ByVal nId As Integer, ByVal nCapital As Double, ByVal nInsured As Integer, ByVal nCurrency As Integer, ByVal sClient As String, ByVal nUsercode As Integer, ByVal sPolitype As String, ByVal sBrancht As String) As Boolean
		Dim lclsPolicy_Win As Policy_Win
		Dim lblnLife As Boolean
		
		On Error GoTo InsPostvi662_Err
		With Me
			.sCertype = sCertype
			.nBranch = nBranch
			.nProduct = nProduct
			.nPolicy = nPolicy
			.nCertif = nCertif
			.nGroup = nGroup
			.sTyplevel = sTyplevel
			.nLevel = nLevel
			.dEffecdate = dEffecdate
			.nId = nId
			.nCapital = nCapital
			.nInsured = IIf(sClient = String.Empty, nInsured, 1)
			.nCurrency = nCurrency
			.sClient = sClient
			.nUsercode = nUsercode
		End With
		
		Select Case sAction
			Case "Add"
				InsPostVI662 = Add
			Case "Update"
				InsPostVI662 = Update
			Case "Del"
				InsPostVI662 = Delete
		End Select
		
		'+Se actualiza en Policy_Win la ventana con contenido
		lclsPolicy_Win = New Policy_Win
		If InsPostVI662 Then
			Call lclsPolicy_Win.Add_PolicyWin(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nUsercode, "VI662", "2")
			If sPolitype = "1" Or nCertif > 0 Then
				If ((sBrancht = CStr(eProduct.Product.pmBrancht.pmlife) Or sBrancht = CStr(eProduct.Product.pmBrancht.pmNotTraditionalLife)) And (sPolitype = "1" Or (sPolitype <> "1" And nCertif > 0))) Then
					lblnLife = True
				End If
				InsPostVI662 = lclsPolicy_Win.Add_PolicyWin(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nUsercode, "CA014", "3",  ,  , lblnLife, False)
			Else
				InsPostVI662 = lclsPolicy_Win.Add_PolicyWin(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nUsercode, "CA014A", "3",  ,  ,  , False)
			End If
		Else
			Call lclsPolicy_Win.Add_PolicyWin(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nUsercode, "VI662", "1")
		End If
		
InsPostvi662_Err: 
		If Err.Number Then
			InsPostVI662 = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsPolicy_Win may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsPolicy_Win = Nothing
	End Function
	
	'% valExistsLife_levels: Verifica la existencia de información en la tabla life_levels según los parámetros pasados.
	Public Function valExistsLife_levels() As Boolean
		Dim lrecLife_levels As eRemoteDB.Execute
		Dim lintExists As Integer
		
		On Error GoTo valExistsLife_levels_Err
		
		lrecLife_levels = New eRemoteDB.Execute
		
		With lrecLife_levels
			.StoredProcedure = "valExistslife_levels"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nGroup", nGroup, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sTyplevel", sTyplevel, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nLevel", nLevel, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nId", nId, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nExists", lintExists, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Run(False)
			valExistsLife_levels = (.Parameters("nExists").Value = 1)
			
		End With
		
valExistsLife_levels_Err: 
		If Err.Number Then
			valExistsLife_levels = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecLife_levels may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecLife_levels = Nothing
	End Function
	
	'%updLife_levels_CurAll: Se encarga de actualizar la tabla life_levels
	Public Function updLife_levels_CurAll() As Boolean
		Dim lrecLife_levels As eRemoteDB.Execute
		On Error GoTo updLife_levels_CurAll_Err
		
		lrecLife_levels = New eRemoteDB.Execute
		
		With lrecLife_levels
			.StoredProcedure = "updLife_levels_CurAll"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nGroup", nGroup, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			updLife_levels_CurAll = .Run(False)
		End With
		
updLife_levels_CurAll_Err: 
		If Err.Number Then
			updLife_levels_CurAll = False
		End If
		'UPGRADE_NOTE: Object lrecLife_levels may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecLife_levels = Nothing
		On Error GoTo 0
		
	End Function
	
	'%Class_Initialize: Inicializa las propiedades cuando se instancia la clase
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		sCertype = String.Empty
		nBranch = eRemoteDB.Constants.intNull
		nProduct = eRemoteDB.Constants.intNull
		nPolicy = eRemoteDB.Constants.intNull
		nCertif = eRemoteDB.Constants.intNull
		nGroup = eRemoteDB.Constants.intNull
		sTyplevel = String.Empty
		nLevel = eRemoteDB.Constants.intNull
		nId = eRemoteDB.Constants.intNull
		nCapital = eRemoteDB.Constants.intNull
		nInsured = eRemoteDB.Constants.intNull
		dNulldate = eRemoteDB.Constants.dtmNull
		dCompdate = eRemoteDB.Constants.dtmNull
		nCurrency = eRemoteDB.Constants.intNull
		sClient = String.Empty
		dEffecdate = eRemoteDB.Constants.dtmNull
		nUsercode = eRemoteDB.Constants.intNull
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
End Class






