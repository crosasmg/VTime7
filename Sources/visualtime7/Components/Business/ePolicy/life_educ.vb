Option Strict Off
Option Explicit On
Public Class life_educ
	'%-------------------------------------------------------%'
	'% $Workfile:: life_educ.cls                            $%'
	'% $Author:: Nvaplat41                                  $%'
	'% $Date:: 9/10/03 19.01                                $%'
	'% $Revision:: 38                                       $%'
	'%-------------------------------------------------------%'
	'+ Estructura de tabla insudb.life_educ al 19/03/2002
	'+ Los campos llave corresponden a sCertype , nBranch, nProduct, nPolicy, nCertif, nGroup, dEffecdate
	
	'+ Property                     Type          DBType       Size Scale Prec Null
	'+ ---------------------------- ------------- ------------ ---- ----- ---- ----
	Public sCertype As String ' CHAR       1    0     0    N
	Public nBranch As Integer ' NUMBER     22   0     5    N
	Public nProduct As Integer ' NUMBER     22   0     5    N
	Public nPolicy As Double ' NUMBER     22   0     10   N
	Public nCertif As Double ' NUMBER     22   0     10   N
	Public nGroup As Integer ' NUMBER     22   0     5    N
	Public dEffecdate As Date ' DATE       7    0     0    N
	Public sTypinsur As String ' CHAR       1    0     0    S
	Public nPercentsec As Double ' NUMBER     22   2     5    S
	Public sPrebasic As String ' CHAR       1    0     0    S
	Public sHighschool As String ' CHAR       1    0     0    S
	Public dNulldate As Date ' DATE       7    0     0    S
	Public dCompdate As Date ' DATE       7    0     0    N
	Public nUsercode As Integer ' NUMBER     22   0     5    N
	Public nCapital As Double ' NUMBER     0    0     12   N
	Public nCost_Anual As Double
	
	
	'- Variables auxiliares
	Public nSituation As Integer
	Public sCurren_pol As String
	Public nCurrency As Integer
	Public sTypenom As String
	Public bGroups As Boolean
	Public bSituation As Boolean
	Public bTipnom As Boolean
	
	'%Find: Lee los datos de la tabla
	Public Function Find(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nGroup As Integer, ByVal dEffecdate As Date) As Object
		Dim lrecreaLife_educ As eRemoteDB.Execute
		
		On Error GoTo reaLife_educ_Err
		
		lrecreaLife_educ = New eRemoteDB.Execute
		
		Find = False
		'+ Definición de store procedure reaLife_educ al 02-05-2002 12:17:03
		With lrecreaLife_educ
			.StoredProcedure = "reaLife_educ"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nGroup", IIf(nGroup = eRemoteDB.Constants.intNull, 0, nGroup), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run() Then
				Me.sCertype = .FieldToClass("sCertype")
				Me.nBranch = .FieldToClass("nBranch")
				Me.nProduct = .FieldToClass("nProduct")
				Me.nPolicy = .FieldToClass("nPolicy")
				Me.nCertif = .FieldToClass("nCertif")
				Me.nGroup = .FieldToClass("nGroup")
				Me.dEffecdate = .FieldToClass("dEffecdate")
				sTypinsur = .FieldToClass("sTypinsur")
				nPercentsec = .FieldToClass("nPercentsec")
				sPrebasic = .FieldToClass("sPrebasic")
				sHighschool = .FieldToClass("sHighschool")
				dNulldate = .FieldToClass("dNulldate")
				nCapital = .FieldToClass("nCapital")
				nCost_Anual = .FieldToClass("nCost_Anual")
				nSituation = .FieldToClass("nSituation")
				Find = True
			End If
		End With
		
reaLife_educ_Err: 
		If Err.Number Then
			Find = False
		End If
		'UPGRADE_NOTE: Object lrecreaLife_educ may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaLife_educ = Nothing
		On Error GoTo 0
	End Function
	
	'% Update: Actualiza los registros en la tabla
	Private Function Update() As Boolean
		Dim lrecinsUpdlife_educ As eRemoteDB.Execute
		
		On Error GoTo Update_Err
		
		lrecinsUpdlife_educ = New eRemoteDB.Execute
		
		'+ Definición de store procedure insUpdlife_educ al 02-05-2002 18:10:41
		With lrecinsUpdlife_educ
			.StoredProcedure = "insUpdlife_educ"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nGroup", IIf(nGroup = eRemoteDB.Constants.intNull, 0, nGroup), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sTypinsur", sTypinsur, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPercentsec", nPercentsec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sPrebasic", sPrebasic, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sHighschool", sHighschool, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCapital", nCapital, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCost_Anual", nCost_Anual, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Update = .Run(False)
		End With
		
Update_Err: 
		If Err.Number Then
			Update = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecinsUpdlife_educ may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsUpdlife_educ = Nothing
	End Function
	
	'%InsValvi662: Validaciones de la transacción(Folder)
	Public Function InsValVI662(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nGroup As Integer, ByVal sTypinsur As String, ByVal nPercentsec As Double, ByVal nCurrency As Integer, ByVal nSituation As Integer, Optional ByVal dEffecdate As Date = eRemoteDB.Constants.dtmNull) As String
		Dim lclsErrors As eFunctions.Errors
		Dim lclsGroup As ePolicy.Groupss
		Dim lclsSituation As ePolicy.Situations
		
		On Error GoTo InsValvi662_Err
		lclsErrors = New eFunctions.Errors
		
		With lclsErrors
			If (sCertype = "3" Or sCertype = "4" Or sCertype = "5") Or nCertif > 0 Then
				lclsGroup = New ePolicy.Groupss
				If nGroup = eRemoteDB.Constants.intNull Then
					If lclsGroup.Find(sCertype, nBranch, nProduct, nPolicy, dEffecdate, nGroup) Then
						.ErrorMessage("VI662", 10152)
					End If
				Else
					If Not lclsGroup.Find(sCertype, nBranch, nProduct, nPolicy, dEffecdate, nGroup) Then
						.ErrorMessage("VI662", 3136)
					End If
				End If
			End If
			
			'+ Si se está tratando un certificado y se definieron situaciones de riesgo para la póliza situación de riesgo debe estar lleno
			If nCertif > 0 Then
				lclsSituation = New ePolicy.Situations
				If lclsSituation.Find(sCertype, nBranch, nProduct, nPolicy) Then
					If nSituation = eRemoteDB.Constants.intNull Then
						.ErrorMessage("VI662", 55957)
					End If
				End If
				'UPGRADE_NOTE: Object lclsSituation may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
				lclsSituation = Nothing
			End If
			
			If sTypinsur = "2" And nPercentsec <= 0 Then
				.ErrorMessage("VI662", 55679)
			End If
			
			'+ Validación del campo moneda.
			If nCurrency <= 0 Then
				.ErrorMessage("VI662", 10107)
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
		'UPGRADE_NOTE: Object lclsGroup may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsGroup = Nothing
		
	End Function
	
	'%InsPostvi662: Ejecuta el post de la transacción
	Public Function InsPostVI662(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nGroup As Integer, ByVal nSituation As Integer, ByVal dEffecdate As Date, ByVal sTypinsur As String, ByVal nPercentsec As Double, ByVal sPrebasic As String, ByVal sHighschool As String, ByVal nCapital As Double, ByVal nCurrency As Integer, ByVal nTransaction As Integer, ByVal sPolitype As String, ByVal sTypenom As String, ByVal nUsercode As Integer, ByVal nCost_Anual As Double, ByVal sBrancht As String) As Boolean
		Dim lclsPolicy_Win As Policy_Win
		Dim lclsPolicy As ePolicy.Policy
		Dim lclsCertificat As ePolicy.Certificat
		Dim lclslife_levels As ePolicy.life_levels
		Dim lblnLife As Boolean
		
		On Error GoTo InsPostvi662_Err
		
		With Me
			.sCertype = sCertype
			.nBranch = nBranch
			.nProduct = nProduct
			.nPolicy = nPolicy
			.nCertif = nCertif
			.nGroup = nGroup
			.dEffecdate = dEffecdate
			.sTypinsur = sTypinsur
			.nPercentsec = nPercentsec
			.sPrebasic = IIf(sPrebasic = String.Empty, "2", sPrebasic)
			.sHighschool = IIf(sHighschool = String.Empty, "2", sHighschool)
			.nCapital = nCapital
			.nCost_Anual = nCost_Anual
			.nUsercode = nUsercode
		End With
		
		InsPostVI662 = Update
		
		If InsPostVI662 Then
			lclslife_levels = New ePolicy.life_levels
			With lclslife_levels
				.sCertype = sCertype
				.nBranch = nBranch
				.nProduct = nProduct
				.nPolicy = nPolicy
				.nCertif = nCertif
				
				.nGroup = nGroup
				.sTyplevel = String.Empty
				.nLevel = eRemoteDB.Constants.intNull
				.nId = eRemoteDB.Constants.intNull
				.nUsercode = nUsercode
				.dEffecdate = dEffecdate
				'+ Si existe información de niveles
				If .valExistsLife_levels Then
					.nCurrency = nCurrency
					'+ Se actualiza la moneda para todos los niveles
					Call .updLife_levels_CurAll()
				End If
			End With
			
			'+ Si se trata de una póliza matriz
			If sPolitype <> "1" And nCertif = 0 Then
				'+ Si se trata de una 1)emisión, 3)recuperación, 4)Cotización-poliza, 24)cotización de modificación, 28)cotización de renovación.
				If nTransaction = 1 Or nTransaction = 3 Or nTransaction = 4 Or nTransaction = 24 Or nTransaction = 28 Then
					lclsPolicy = New ePolicy.Policy
					'+ Se actualiza el campo: tipo de nómina
					With lclsPolicy
						.sCertype = sCertype
						.nBranch = nBranch
						.nProduct = nProduct
						.nPolicy = nPolicy
						.sTypenom = sTypenom
						.nUsercode = nUsercode
						Call .UpdatePolicy_sTypeNom()
					End With
				End If
			End If
		End If
		
		If InsPostVI662 Then
			'+ Se actualiza el grupo y la situación en la tabla de datos particulares.
			lclsCertificat = New ePolicy.Certificat
			With lclsCertificat
				.sCertype = sCertype
				.nBranch = nBranch
				.nProduct = nProduct
				.nPolicy = nPolicy
				.nCertif = nCertif
				.dEffecdate = dEffecdate
				.nSituation = IIf(nSituation = 0, eRemoteDB.Constants.intNull, nSituation)
				.nGroup = nGroup
				.nUsercode = nUsercode
				Call .UpdateDatPart()
			End With
		End If
		
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
		'UPGRADE_NOTE: Object lclsPolicy may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsPolicy = Nothing
		'UPGRADE_NOTE: Object lclsPolicy_Win may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsPolicy_Win = Nothing
		'UPGRADE_NOTE: Object lclsCertificat may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsCertificat = Nothing
		'UPGRADE_NOTE: Object lclslife_levels may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclslife_levels = Nothing
		On Error GoTo 0
	End Function
	
	'%insPreVI662: Lee los datos de la tabla
	Public Function insPreVI662(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nGroup As Integer, ByVal nCurrency As Integer, ByVal dEffecdate As Date, ByVal nTransaction As Integer) As Object
		Dim lclsPolicy As ePolicy.Policy
		Dim lclsCurren_pol As ePolicy.Curren_pol
		Dim lclsGroups As ePolicy.Groups
		Dim lclsSituation As ePolicy.Situation
		
		On Error GoTo insPreVI662_Err
		
		'+ Si se trata de una 1)emisión, 3)recuperación, 4)Cotización-poliza, 24)cotización de modificación, 28)cotización de renovación, 18) Re-Emisión de póliza, 19) Re-Emisión de certificado
		If nTransaction = 1 Or nTransaction = 3 Or nTransaction = 4 Or nTransaction = 18 Or nTransaction = 19 Or nTransaction = 24 Or nTransaction = 28 Then
			lclsPolicy = New ePolicy.Policy
			'+ Se obtiene el tipo de nómina
			If lclsPolicy.Find(sCertype, nBranch, nProduct, nPolicy) Then
				Me.sTypenom = lclsPolicy.sTypenom
			End If
		End If
		
		lclsCurren_pol = New ePolicy.Curren_pol
		If lclsCurren_pol.FindOneOrLocal(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate) Then
			sCurren_pol = lclsCurren_pol.Charge_Combo
			
			If nCurrency = eRemoteDB.Constants.intNull Then
				nCurrency = lclsCurren_pol.nCurrency
			End If
			Me.nCurrency = nCurrency
		End If
		
		lclsGroups = New ePolicy.Groups
		Me.bGroups = lclsGroups.valGroupExist(sCertype, nBranch, nProduct, nPolicy, dEffecdate)
		lclsSituation = New ePolicy.Situation
		Me.bSituation = lclsSituation.valExistsSituation(sCertype, nBranch, nProduct, nPolicy, eRemoteDB.Constants.intNull)
		
		insPreVI662 = Find(sCertype, nBranch, nProduct, nPolicy, nCertif, nGroup, dEffecdate)
		
		If nTransaction = 4 Or nTransaction = 24 Or nTransaction = 28 Then
			Me.bTipnom = True
		Else
			Me.bTipnom = False
		End If
		
insPreVI662_Err: 
		If Err.Number Then
			insPreVI662 = False
		End If
		'UPGRADE_NOTE: Object lclsCurren_pol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsCurren_pol = Nothing
		'UPGRADE_NOTE: Object lclsPolicy may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsPolicy = Nothing
		'UPGRADE_NOTE: Object lclsGroups may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsGroups = Nothing
		'UPGRADE_NOTE: Object lclsSituation may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsSituation = Nothing
		On Error GoTo 0
	End Function
	
	'% Class_Initialize: Inicializa las propiedades cuando se instancia la clase
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		sCertype = String.Empty
		nBranch = eRemoteDB.Constants.intNull
		nProduct = eRemoteDB.Constants.intNull
		nPolicy = eRemoteDB.Constants.intNull
		nCertif = eRemoteDB.Constants.intNull
		nGroup = eRemoteDB.Constants.intNull
		dEffecdate = eRemoteDB.Constants.dtmNull
		sTypinsur = String.Empty
		nPercentsec = eRemoteDB.Constants.intNull
		sPrebasic = String.Empty
		sHighschool = String.Empty
		dNulldate = eRemoteDB.Constants.dtmNull
		dCompdate = eRemoteDB.Constants.dtmNull
		nCapital = eRemoteDB.Constants.intNull
		nCost_Anual = eRemoteDB.Constants.intNull
		nUsercode = eRemoteDB.Constants.intNull
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
End Class






