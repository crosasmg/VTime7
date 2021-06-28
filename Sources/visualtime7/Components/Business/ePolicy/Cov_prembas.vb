Option Strict Off
Option Explicit On
Public Class Cov_prembas
	'%-------------------------------------------------------%'
	'% $Workfile:: Cov_prembas.cls                          $%'
	'% $Author:: Ljimenez                                   $%'
	'% $Date:: 13/10/08 19.01                               $%'
	'% $Revision:: 1                                        $%'
	'%-------------------------------------------------------%'
	
	'+ Definición de la tabla COV_PREMBAS
	'+ Column_Name                                   Type      Length  Prec  Scale Nullable
	'----------------------------- --------------- - -------- ------- ----- ------ --------
	Public sCertype As String ' CHAR           1              No
	Public nBranch As Integer ' NUMBER        22     5      0 No
	Public nProduct As Integer ' NUMBER        22     5      0 No
	Public nPolicy As Double ' NUMBER        22    10      0 No
	Public nCertif As Double ' NUMBER        22    10      0 No
	Public nGroup_insu As Integer ' NUMBER        22     5      0 No
	Public nModulec As Integer ' NUMBER        22     5      0 No
	Public nCover As Integer ' NUMBER        22     5      0 No
	Public sClient As String ' CHAR          14              No
	Public dEffecdate As Date ' DATE           7              No
	Public nCapital As Double ' NUMBER        22    12      0 Yes
	Public nPremium As Double ' NUMBER        22    12      0 Yes
	Public nRate As Double ' NUMBER        22     5      2 Yes
	Public dCompdate As Date ' DATE           7              No
	Public dNulldate As Date ' DATE           7              Yes
	Public nUsercode As Integer ' NUMBER        22     5      0 No
	
	Public nCurrency As Integer
	Public nCountCurrency As Integer
	Public mclsRoles As Roles
	Public mclsCurren_pol As Curren_pol
	Public sDescript As String
	Public nCountRoles As Integer
	
	'%insPreVI8001: Genera la informacion que debe ser mostrada en la ventana
	Public Function insPreVI8001(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nTransaction As Integer, ByVal nUsercode As Integer, ByVal sClient As String) As Boolean
		Dim lreccreaCov_prembas As eRemoteDB.Execute
		
		On Error GoTo insPreVI8001_err
		
		lreccreaCov_prembas = New eRemoteDB.Execute
		
		With lreccreaCov_prembas
			.StoredProcedure = "insPreVI8001"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTransaction", nTransaction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			insPreVI8001 = .Run(False)
		End With
		
		'UPGRADE_NOTE: Object lreccreaCov_prembas may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lreccreaCov_prembas = Nothing
		
insPreVI8001_err: 
		If Err.Number Then
			insPreVI8001 = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lreccreaCov_prembas may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lreccreaCov_prembas = Nothing
	End Function
	
	'%InsPreVI8001_A: Esta función obtiene los valores iniciales de la VI8001
	Public Function InsPreVI8001_A(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nCurrency As Integer, ByVal sClient As String) As Boolean
		
		On Error GoTo InsPreVI8001_A_Err
		
		InsPreVI8001_A = True
		
		'+ Se obtiene las monedas asociadas a la póliza
		mclsCurren_pol = New Curren_pol
		
		With mclsCurren_pol
			If .Find(nPolicy, nBranch, nProduct, sCertype, nCertif, dEffecdate) Then
				If nCurrency > 0 Then
					Me.nCurrency = nCurrency
					.nCurrency = nCurrency
				Else
					Me.nCurrency = 0
					If .IsLocal Then
						Me.nCurrency = 1
					Else
						Call .Val_Curren_pol(0)
					End If
					Me.nCurrency = .nCurrency
				End If
				Me.nCountCurrency = .CountCurrenPol + 1
			End If
		End With
		
		mclsRoles = New Roles
		
		'+ Se obtienen los datos asociados al cliente
		With mclsRoles
			If .Find(sCertype, nBranch, nProduct, nPolicy, nCertif, 2, sClient, dEffecdate) Then
				
				Call .CalInsuAge(nBranch, nProduct, dEffecdate, .dBirthdate, .sSexclien, .sSmoking, .nRole)
				
				If sClient = String.Empty Then
					sClient = .sClient
				End If
				
				nCountRoles = .Count_By_Role(sCertype, nBranch, nProduct, nPolicy, nCertif, 2, dEffecdate)
			End If
		End With
		
		
InsPreVI8001_A_Err: 
		If Err.Number Then
			InsPreVI8001_A = False
		End If
		
		On Error GoTo 0
	End Function
End Class






