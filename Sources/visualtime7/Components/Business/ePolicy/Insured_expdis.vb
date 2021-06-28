Option Strict Off
Option Explicit On
Public Class Insured_expdis
	'%-------------------------------------------------------%'
	'% $Workfile:: Insured_expdis.cls                       $%'
	'% $Author:: Nvaplat18                                  $%'
	'% $Date:: 19/11/03 23.30                               $%'
	'% $Revision:: 69                                       $%'
	'%-------------------------------------------------------%'
	'+
	'+ Estructura de tabla INSURED_EXPDIS al 09-25-2003 10:05:08
	'+         Property                Type         DBType   Size Scale  Prec  Null
	'+-----------------------------------------------------------------------------
	Public sCertype As String ' CHAR       1    0     0    N
	Public nBranch As Integer ' NUMBER     22   0     5    N
	Public nProduct As Integer ' NUMBER     22   0     5    N
	Public nPolicy As Double ' NUMBER     22   0     10   N
	Public nCertif As Double ' NUMBER     22   0     10   N
	Public nGroup_insu As Integer ' NUMBER     22   0     5    N
	Public sClient As String ' CHAR       14   0     0    N
	Public nDisexprc As Integer ' NUMBER     22   0     5    N
	Public sDisexpri As String ' CHAR       1    0     0    N
	Public nModulec As Integer ' NUMBER     22   0     5    N
	Public nCover As Integer ' NUMBER     22   0     5    N
	Public dEffecdate As Date ' DATE       7    0     0    N
	Public sUnit As String ' CHAR       1    0     0    N
	Public nRate As Double ' NUMBER     22   6     9    S
	Public nAmount As Double ' NUMBER     22   6     18   S
	Public dNulldate As Date ' DATE       7    0     0    S
	Public sPerm_temp As String ' CHAR       1    0     0    N
	Public dDate_fr As Date ' DATE       7    0     0    S
	Public dDate_to As Date ' DATE       7    0     0    S
	Public nAge As Integer ' NUMBER     22   0     5    S
	Public nNotenum As Integer ' NUMBER     22   0     10   S
	Public dCompdate As Date ' DATE       7    0     0    N
	Public nUsercode As Integer ' NUMBER     22   0     5    N
	Public nCause As Integer ' NUMBER     22   0     5    S
	Public sAgree As String ' CHAR       1    0     0    N
' - Nuevas variables asociadas al cálculo de recargo por actividad riesgosa HAF001. ( Incidente: I-017137 )
    Public sInitdate_Calc As String
    Public nActivity As Double
    Public nSport As Double
	' - Variable que guarda la transacción que se está ejecutando
	Public nTransaction As Integer
	
	' - Valores anteriores de la transacción
	Public nAmount_old As Double
	Public nPercent_old As Double
	
	'- Se declaran las variables auxiliares
	
	'- Variable que indica si existe en la tabla Insured_expdis
	Public sSel As String
	
	'- Variable que indica si existe en la tabla Insured_expdis
	Public sCoverBase As String
	
	'- Variable para almacenar la moneda del recargo/descuento/impuesto
	Public nCurrency As Integer
	
	'- Variable para asegurado sobre el cual aplica el recargo
	Public nRole As Roles.eRoles
	
	'-Variable con monto acumulado de tasas que suman para rating
	Public nTotalRate As Double
	
	'%Add: Crea un registro en la tabla
	Public Function Add() As Boolean
		Add = InsUpdInsured_expdis(1, nTransaction)
	End Function
	
	'%Update: Actualiza los datos de la tabla
	Public Function Update() As Boolean
		Update = InsUpdInsured_expdis(2, nTransaction)
	End Function
	
	'%Delete: Borra los datos de la tabla
	Public Function Delete() As Boolean
		Delete = InsUpdInsured_expdis(3, nTransaction)
	End Function
	'%Find: Lee los datos de la tabla
	Public Function Find(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal sClient As String, ByVal nDisexprc As Integer, ByVal nModulec As Integer, ByVal nCover As Integer, ByVal dEffecdate As Date, Optional ByVal lblnFind As Boolean = False) As Boolean
		Dim lrecreaInsured_expdis As eRemoteDB.Execute
		
		On Error GoTo Find_Err
		
		If lblnFind Or Me.sCertype <> sCertype Or Me.nBranch <> nBranch Or Me.nProduct <> nProduct Or Me.nPolicy <> nPolicy Or Me.nCertif <> nCertif Or Me.sClient <> sClient Or Me.nDisexprc <> nDisexprc Or Me.nModulec <> nModulec Or Me.nCover <> nCover Or Me.dEffecdate <> dEffecdate Then
			lrecreaInsured_expdis = New eRemoteDB.Execute
			With lrecreaInsured_expdis
				.StoredProcedure = "ReaInsured_expdis"
				.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nDisexprc", nDisexprc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If .Run Then
					Find = True
					Me.sSel = .FieldToClass("sSel")
					Me.sCertype = .FieldToClass("sCertype")
					Me.nBranch = .FieldToClass("nBranch")
					Me.nProduct = .FieldToClass("nProduct")
					Me.nPolicy = .FieldToClass("nPolicy")
					Me.nCertif = .FieldToClass("nCertif")
					Me.sClient = .FieldToClass("sClient")
					Me.nDisexprc = .FieldToClass("nDisexprc")
					Me.nModulec = .FieldToClass("nModulec")
					Me.nCover = .FieldToClass("nCover")
					Me.dEffecdate = .FieldToClass("dEffecdate")
					sDisexpri = .FieldToClass("sDisexpri")
					nRate = .FieldToClass("nRate")
					nAmount = .FieldToClass("nAmount")
					dNulldate = .FieldToClass("dNulldate")
					sPerm_temp = .FieldToClass("sPerm_Temp")
					sUnit = .FieldToClass("sUnit")
					dDate_fr = .FieldToClass("dDate_Fr")
					dDate_to = .FieldToClass("dDate_to")
					nAge = .FieldToClass("nAge")
					nNotenum = .FieldToClass("nNotenum")
					nCause = .FieldToClass("nCause")
					sAgree = .FieldToClass("sAgree")
					.RCloseRec()
				End If
			End With
		End If
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecreaInsured_expdis may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaInsured_expdis = Nothing
	End Function
	
	'%insValVI681Upd: Esta función se encarga de validar los campos de actualizacion de la pagina popup VI681
	Public Function insValVI681Upd(ByVal sCodispl As String, ByVal sAction As String, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal sClient As String, ByVal nDisexprc As Integer, ByVal nModulec As Integer, ByVal nCover As Integer, ByVal dEffecdate As Date, ByVal sDisexpri As String, ByVal sUnit As String, ByVal nRate As Double, ByVal nAmount As Double, ByVal sPerm_temp As String, ByVal dDate_fr As Date, ByVal dDate_to As Date, ByVal nAge As Integer, ByVal nNotenum As Integer, ByVal nDisexprc_old As Integer, ByVal nModulec_old As Integer, ByVal nCover_old As Integer) As String
		
		'- Se define el objeto para el manejo de la clase Product
		Dim lobjErrors As eFunctions.Errors
		Dim lobjValues As eFunctions.Values
		Dim lobjDisco_expr As eProduct.Disco_expr
		Dim lobjCertificat As ePolicy.Certificat
		Dim lblnError As Boolean
		Dim lblnErrorDat As Boolean
		Dim lintBranch As Integer
		Dim lintProduct As Integer
		
		lobjErrors = New eFunctions.Errors
		lobjValues = New eFunctions.Values
		lobjDisco_expr = New eProduct.Disco_expr
		lobjCertificat = New ePolicy.Certificat
		
		On Error GoTo insValVI681Upd_Err
		lblnError = False
		lblnErrorDat = False
		
		'+ Inicialización de variables nulas que deben tener valor
		nModulec = IIf(nModulec = eRemoteDB.Constants.intNull, 0, nModulec)
		nCover = IIf(nCover = eRemoteDB.Constants.intNull, 0, nCover)
		
		'+ Validación del Campo Unidad
		sUnit = IIf(sUnit = "1", "1", "2")
		
		If sClient = String.Empty Then
			Call lobjErrors.ErrorMessage(sCodispl, 2001)
		End If
		
		If nDisexprc = eRemoteDB.Constants.intNull Or nDisexprc = 0 Then
			Call lobjErrors.ErrorMessage(sCodispl, 3676)
		End If
		
		'+ Validación del Campo Monto
		If (nRate = eRemoteDB.Constants.intNull Or nRate = 0) And (nAge = eRemoteDB.Constants.intNull Or nAge = 0) Then
			If (nAmount = eRemoteDB.Constants.intNull Or nAmount = 0) Then
				Call lobjErrors.ErrorMessage(sCodispl, 60198)
			End If
		End If
		
		'+ Validaciónes
		With lobjDisco_expr
			If .Find(nBranch, nProduct, nDisexprc, dEffecdate) Then
				'+ Se permite aumentar el factor
				
				If nRate > .nRate Then
					If .sChanallo <> "1" And .sChanallo <> "3" Then
						Call lobjErrors.ErrorMessage(sCodispl, 60194)
					End If
				End If
				
				'+ porcentaje de aumento del factor
				If nRate > .nRate Then
					If (.sChanallo = "1" Or .sChanallo = "3") And .nDisexaddper <> eRemoteDB.Constants.intNull And .nDisexaddper > 0 Then
						If .nRate <> eRemoteDB.Constants.intNull And .nRate <> 0 Then
							If ((nRate - .nRate) / .nRate * 100) > .nDisexaddper Then
								Call lobjErrors.ErrorMessage(sCodispl, 60195,  ,  , "(" & .nDisexaddper & " % )")
							End If
						End If
					End If
				End If
				
				'+ se permite disminuir el factor
				If nRate < .nRate Then
					If .sChanallo <> "2" And .sChanallo <> "3" Then
						Call lobjErrors.ErrorMessage(sCodispl, 60196)
					End If
				End If
				
				'+ porcentaje de disminución del factor
				If nRate < .nRate Then
					If (.sChanallo = "2" Or .sChanallo = "3") And .nDisexsubper <> eRemoteDB.Constants.intNull And .nDisexsubper > 0 Then
						If .nRate <> eRemoteDB.Constants.intNull And .nRate <> 0 Then
							If ((.nRate - nRate) / .nRate * 100) > .nDisexsubper Then
								Call lobjErrors.ErrorMessage(sCodispl, 60197,  ,  , "(" & .nDisexsubper & " % )")
							End If
						End If
					End If
				End If
				
				'+ validación del campo Factor
				If (nAmount = eRemoteDB.Constants.intNull Or nAmount = 0) And (nAge = eRemoteDB.Constants.intNull Or nAge = 0) Then
					If nRate = eRemoteDB.Constants.intNull Or nRate = 0 Then
						Call lobjErrors.ErrorMessage(sCodispl, 60193)
					End If
				End If
				
				'+ Si no se permite aumentar el monto
				If nAmount > .nDisexpra Then
					If .sChanallo = "0" Or .sChanallo = "2" Then
						Call lobjErrors.ErrorMessage(sCodispl, 60199)
					End If
				End If
				
				'+ porcentaje de aumento del monto
				If nAmount > .nDisexpra Then
					If (.sChanallo = "1" Or .sChanallo = "3") Then
						If .nDisexaddper <> eRemoteDB.Constants.intNull And .nDisexaddper > 0 Then
							If .nDisexpra <> eRemoteDB.Constants.intNull And .nDisexpra <> 0 Then
								If ((nAmount - .nDisexpra) / .nDisexpra) * 100 > .nDisexaddper Then
									Call lobjErrors.ErrorMessage(sCodispl, 60200,  ,  , " ( " & .nDisexaddper & " ) ")
								End If
							End If
						End If
					End If
				End If
				
				'+ Se permite disminuir el monto
				If nAmount < .nDisexpra Then
					If .sChanallo <> "2" And .sChanallo <> "3" Then
						Call lobjErrors.ErrorMessage(sCodispl, 60201)
					End If
				End If
				
				'+ Porcentage de disminución del monto
				If nAmount < .nDisexpra Then
					If (.sChanallo = "2" Or .sChanallo = "3") Then
						If .nDisexsubper <> eRemoteDB.Constants.intNull And .nDisexsubper > 0 Then
							If .nDisexpra <> eRemoteDB.Constants.intNull And .nDisexpra <> 0 Then
								If ((.nDisexpra - nAmount) / .nDisexpra * 100) > .nDisexsubper Then
									Call lobjErrors.ErrorMessage(sCodispl, 60202,  ,  , " ( " & .nDisexsubper & " ) ")
								End If
							End If
						End If
					End If
				End If
			Else
				lblnError = True
			End If ' lobjDisco_expr.Find(nBranch, nProduct, nDisexprc, dEffecdate)
		End With
		
		'+ Indicador de 1-Permanente/2-Temporal ( si es blanco le mueve 2)
		sPerm_temp = IIf(sPerm_temp = "1", "1", "2")
		
		If dDate_fr <> eRemoteDB.Constants.dtmNull Then
			'+ Fecha desde no debe ser menor a fecha vigencia poliza
			If dDate_fr < dEffecdate Then
				Call lobjErrors.ErrorMessage(sCodispl, 60204)
				lblnErrorDat = True
			End If
		Else
			Call lobjErrors.ErrorMessage(sCodispl, 60217)
		End If
		
		If dDate_to <> eRemoteDB.Constants.dtmNull Then
			
			'+ Fecha Hasta debe ser menor o igual a fecha fin de vigencia de poliza/certiticado
			With lobjCertificat
				If .Find(sCertype, nBranch, nProduct, nPolicy, nCertif) Then
					If dDate_to > .dExpirdat And .dExpirdat <> eRemoteDB.Constants.dtmNull Then
						Call lobjErrors.ErrorMessage(sCodispl, 60206)
						lblnErrorDat = True
					End If
				End If
			End With
		Else
			If sPerm_temp <> "1" Then
				Call lobjErrors.ErrorMessage(sCodispl, 60218)
			End If
		End If
		
		If Not lblnErrorDat Then
			If dDate_to <> eRemoteDB.Constants.dtmNull And dDate_fr <> eRemoteDB.Constants.dtmNull Then
				'+ Fecha desde debe ser a Fecha hasta
				If dDate_fr > dDate_to Or dDate_fr = dDate_to Then
					Call lobjErrors.ErrorMessage(sCodispl, 60205)
				End If
				'+ Fecha hasta debe ser Mayor a Fecha desde
				If dDate_to < dDate_fr Or dDate_to = dDate_fr Then
					Call lobjErrors.ErrorMessage(sCodispl, 60207)
				End If
			End If
		End If
		
		'+ Se valida que no se duplique la relación descuento/cobertura
		If Not lblnError Then
			If (nDisexprc <> nDisexprc_old) Or (nModulec <> nModulec_old) Or (nCover <> nCover_old) Then
				If Find(sCertype, nBranch, nProduct, nPolicy, nCertif, sClient, nDisexprc, nModulec, nCover, dEffecdate, True) Then
					lblnError = True
					Call lobjErrors.ErrorMessage(sCodispl, 5102)
				End If
			End If
		End If

        If nCover = 0 Then
            Call lobjErrors.ErrorMessage(sCodispl, 3245)
        End If

		insValVI681Upd = lobjErrors.Confirm
		
insValVI681Upd_Err: 
		If Err.Number Then
			insValVI681Upd = "insValVI681Upd: " & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjErrors = Nothing
		'UPGRADE_NOTE: Object lobjValues may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjValues = Nothing
		'UPGRADE_NOTE: Object lobjDisco_expr may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjDisco_expr = Nothing
		'UPGRADE_NOTE: Object lobjCertificat may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjCertificat = Nothing
	End Function
	'%insValVI681: Esta función se encarga de validar los campos de de la pagina VI681
	Public Function insValVI681(ByVal sCodispl As String, ByVal sAction As String, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date) As String
		Dim lclsErrors As eFunctions.Errors
		Dim lclsRoles As ePolicy.Roles
		Dim lcolRoleses As ePolicy.Roleses
		Dim lclsRatings As eBranches.Ratings
		Dim nAge As Integer
		
		lclsErrors = New eFunctions.Errors
		lclsRoles = New ePolicy.Roles
		lcolRoleses = New ePolicy.Roleses
		lclsRatings = New eBranches.Ratings
		
		On Error GoTo insValVI681_Err
		
		mstrContent = "1"
		'+ Validación del Campo recargo
		
		If lcolRoleses.Find_by_Policy(sCertype, nBranch, nProduct, nPolicy, nCertif, "", dEffecdate, 2) Then
			For	Each lclsRoles In lcolRoleses
				If lclsRoles.CalInsuAge(nBranch, nProduct, dEffecdate, lclsRoles.dBirthdate, lclsRoles.sSexclien, lclsRoles.sSmoking) Then
					If Count(sCertype, nBranch, nProduct, nPolicy, nCertif, lclsRoles.sClient, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, dEffecdate) <= 0 Then
						If lclsRatings.InsValRange(nBranch, nProduct, dEffecdate, lclsRoles.nAge, lclsRoles.nAge) Then
							If lclsRoles.nRating > lclsRatings.nRating Then
								Call lclsErrors.ErrorMessage(sCodispl, 60190)
								Exit For
							End If
						End If
					Else
						mstrContent = "2"
					End If
				End If
			Next lclsRoles
		End If
		
		insValVI681 = lclsErrors.Confirm
		
insValVI681_Err: 
		If Err.Number Then
			insValVI681 = "insValVI681: " & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		'UPGRADE_NOTE: Object lclsRoles may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsRoles = Nothing
		'UPGRADE_NOTE: Object lcolRoleses may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lcolRoleses = Nothing
		'UPGRADE_NOTE: Object lclsRatings may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsRatings = Nothing
	End Function
	
	'%InsPostVI681Upd: Esta función realiza los cambios de BD según especificaciones funcionales
	'%                 de la transacción (VI681)
	'Public Function InsPostVI681Upd(ByVal sAction As String, ByVal nTransaction As Integer, ByVal nExist As Integer, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal sClient As String, ByVal nDisexprc As Integer, ByVal nModulec As Integer, ByVal nCover As Integer, ByVal dEffecdate As Date, ByVal sDisexpri As String, ByVal sUnit As String, ByVal nRate As Double, ByVal nAmount As Double, ByVal sPerm_temp As String, ByVal dDate_fr As Date, ByVal dDate_to As Date, ByVal nAge As Integer, ByVal sNotenum As String, ByVal nAmount_old As Double, ByVal nPercent_old As Double, ByVal nDisexprc_old As Integer, ByVal nModulec_old As Integer, ByVal nCover_old As Integer, ByVal nCause As Integer, ByVal sAgree As String, ByVal nUsercode As Integer, ByVal nCurrency As Integer, ByVal nRole As Roles.eRoles, ByVal nTotalRate As Double, ByVal sDisexpri_old As String, ByVal sCoveruse_old As String, ByVal sUnit_old As String, ByVal nCause_old As Integer, ByVal sCoveruse As String) As Boolean
	Public Function InsPostVI681Upd(ByVal sAction As String, ByVal nTransaction As Integer, ByVal nExist As Integer, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal sClient As String, ByVal nDisexprc As Integer, ByVal nModulec As Integer, ByVal nCover As Integer, ByVal dEffecdate As Date, ByVal sDisexpri As String, ByVal sUnit As String, ByVal nRate As Double, ByVal nAmount As Double, ByVal sPerm_temp As String, ByVal dDate_fr As Date, ByVal dDate_to As Date, ByVal nAge As Integer, ByVal sNotenum As String, ByVal nAmount_old As Double, ByVal nPercent_old As Double, ByVal nDisexprc_old As Integer, ByVal nModulec_old As Integer, ByVal nCover_old As Integer, ByVal nCause As Integer, ByVal sAgree As String, ByVal nUsercode As Integer, ByVal nCurrency As Integer, ByVal nRole As Roles.eRoles, ByVal nTotalRate As Double, ByVal sDisexpri_old As String, ByVal sCoveruse_old As String, ByVal sUnit_old As String, ByVal nCause_old As Integer, ByVal sCoveruse As String, ByVal sInit_Date As String, ByVal nActivity As Double, ByVal nSport As Double) As Boolean
		Dim lintAction As Integer
		Dim nNotenum As Integer
		Dim lintCommaNotenum As Integer
		Dim lclsProduct_li As eProduct.Product
		Dim lclsPolicyWin As ePolicy.Policy_Win
		
		lclsProduct_li = New eProduct.Product
		lclsPolicyWin = New ePolicy.Policy_Win
		
		On Error GoTo InsPostVI681Upd_Err
		
		lintCommaNotenum = InStr(sNotenum, ",")
		
		If lintCommaNotenum > 0 Then
			nNotenum = CInt(Left(sNotenum, lintCommaNotenum - 1))
		Else
			nNotenum = IIf(sNotenum = String.Empty, 0, sNotenum)
			sNotenum = ""
		End If
		
		sNotenum = Mid(sNotenum, lintCommaNotenum + 1)
		nModulec = IIf(nModulec = eRemoteDB.Constants.intNull, 0, nModulec)
		nCover = IIf(nCover = eRemoteDB.Constants.intNull, 0, nCover)
		sUnit = IIf(sUnit = "1", "1", "2")
		sPerm_temp = IIf(sPerm_temp = "1", "1", "2")
		
		With Me
			.sCertype = sCertype
			.nBranch = nBranch
			.nProduct = nProduct
			.nPolicy = nPolicy
			.nCertif = nCertif
			.sClient = sClient
			.nDisexprc = nDisexprc
			.nModulec = nModulec
			.nCover = nCover
			.dEffecdate = dEffecdate
			.nTransaction = nTransaction
			.sDisexpri = sDisexpri
			.sUnit = sUnit
			.nRate = nRate
			.nAmount = nAmount
			.sPerm_temp = sPerm_temp
			.dDate_fr = dDate_fr
			.dDate_to = dDate_to
			.nAge = nAge
			.nNotenum = nNotenum
			.nAmount_old = nAmount_old
			.nPercent_old = nPercent_old
			.nCause = nCause
			.sAgree = sAgree
			.nUsercode = nUsercode
			.nCurrency = nCurrency
			.nRole = nRole
			.nTotalRate = nTotalRate
			
		    .sInitdate_Calc = sInitdate_Calc
            .nActivity = nActivity
            .nSport = nSport
			If nDisexprc_old = eRemoteDB.Constants.intNull Then
				nDisexprc_old = nDisexprc
			End If
			
			If nModulec_old = eRemoteDB.Constants.intNull Then
				nModulec_old = nModulec
			End If
			
			If nCover_old = eRemoteDB.Constants.intNull Then
				nCover_old = nCover
			End If
			
			If sAction = "Del" Then
				lintAction = 3
			Else
				If nExist = 1 Then
					If (nDisexprc <> nDisexprc_old) Or (nModulec <> nModulec_old) Or (nCover <> nCover_old) Then
						lintAction = 4
					Else
						lintAction = 2
					End If
				Else
					lintAction = 1
				End If
			End If
			
			Select Case lintAction
				Case 1
					'+ Se crea el registro
					InsPostVI681Upd = .Add
					
					'+ Se modifica el registro
				Case 2
					If sDisexpri_old <> sDisexpri And sCoveruse_old <> sCoveruse Or sUnit_old <> sUnit Or nCause_old <> nCause Then
						If Not (sCoveruse = "1" And (sDisexpri = "1" Or sDisexpri = "4") And sUnit <> "1" And nCause = 1) Then
							If sCoveruse <> "" Then
								.nTotalRate = nTotalRate - nPercent_old
							End If
						Else
							.nTotalRate = nTotalRate + nRate
							.nPercent_old = 0
						End If
					End If
					
					InsPostVI681Upd = .Update
					
					'+ Se elimina el registro
				Case 3
					InsPostVI681Upd = .Delete
					
				Case 4
					'+ Se elimina la información del descuento/cobertura antiguo
					.nDisexprc = nDisexprc_old
					.nModulec = nModulec_old
					.nCover = nCover_old
					.nRate = 0
					.nPercent_old = nPercent_old
					InsPostVI681Upd = .Delete
					
					'+ Se crea la nueva información
					.nDisexprc = nDisexprc
					.nModulec = nModulec
					.nCover = nCover
					.nRate = nRate
					.nPercent_old = 0
					InsPostVI681Upd = .Add
			End Select
		End With
		
		Call lclsPolicyWin.Add_PolicyWin(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nUsercode, "VI1410", "3",  ,  ,  , False)
		
		
		'+ Actualiza la ventas como requerida o actualizadas
		If InsPostVI681Upd Then
			'+ Actualiza la ventas va595 como requerida
			With lclsProduct_li
				If .FindProduct_li(nBranch, nProduct, dEffecdate) Then
					If .nProdClas = 7 Then
						Call lclsPolicyWin.Add_PolicyWin(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nUsercode, "VA595", "3")
					End If
				End If
			End With
			
			Select Case lintAction
				Case 1, 2
					Call lclsPolicyWin.Add_PolicyWin(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nUsercode, "VI681", "2")
					
				Case 3, 4
					Call insValVI681("VI681", sAction, sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate)
					If mstrContent = "1" Then
						Call lclsPolicyWin.Add_PolicyWin(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nUsercode, "VI681", "1")
					End If
			End Select
		End If
		
InsPostVI681Upd_Err: 
		If Err.Number Then
			InsPostVI681Upd = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsProduct_li may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsProduct_li = Nothing
		'UPGRADE_NOTE: Object lclsPolicyWin may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsPolicyWin = Nothing
	End Function
	
	'%InsUpdInsured_expdis: Realiza la actualización de la tabla
	Private Function InsUpdInsured_expdis(ByVal nAction As Integer, ByVal nTransaction As Integer) As Boolean
		Dim lrecInsUpdInsured_expdis As eRemoteDB.Execute
		
		On Error GoTo InsUpdInsured_expdis_Err
		
		lrecInsUpdInsured_expdis = New eRemoteDB.Execute
		
		'+ Definición de parámetros para stored procedure 'InsUpdInsured_expdis'
		'+ Información leída el 23/01/02
		With lrecInsUpdInsured_expdis
			.StoredProcedure = "InsUpdInsured_expdis"
			.Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTransaction", nTransaction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDisexprc", nDisexprc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDisexpri", sDisexpri, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sUnit", sUnit, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRate", nRate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 9, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAmount", nAmount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sPerm_Temp", sPerm_temp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dDate_Fr", dDate_fr, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dDate_to", dDate_to, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAge", nAge, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nNotenum", nNotenum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAmount_old", nAmount_old, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPercent_old", nPercent_old, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 9, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCause", nCause, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAgree", sAgree, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRole", nRole, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTotalRate", nTotalRate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 9, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sInitdate_Calc", sInitdate_Calc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nSport", nSport, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nActivity", nActivity, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			InsUpdInsured_expdis = .Run(False)
		End With
		
InsUpdInsured_expdis_Err: 
		If Err.Number Then
			InsUpdInsured_expdis = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecInsUpdInsured_expdis may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecInsUpdInsured_expdis = Nothing
	End Function
	
	'%Count: Obtiene la cantidad de registros para la póliza
	Public Function Count(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal sClient As String, ByVal nModulec As Integer, ByVal nCover As Integer, ByVal dEffecdate As Date) As Integer
		Dim lrecReaInsured_expdis_Count As eRemoteDB.Execute
		Dim ncount As Integer
		
		On Error GoTo Count_Err
		
		lrecReaInsured_expdis_Count = New eRemoteDB.Execute
		
		'+Definición de parámetros para stored procedure 'reainsured_expdis_count'
		'+Información leída el 08/04/2002
		With lrecReaInsured_expdis_Count
			.StoredProcedure = "ReaInsured_expdis_Count"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCount", ncount, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 5, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				Count = .Parameters.Item("nCount").Value
			End If
		End With
		
Count_Err: 
		If Err.Number Then
			Count = 0
		End If
		'UPGRADE_NOTE: Object lrecReaInsured_expdis_Count may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecReaInsured_expdis_Count = Nothing
		On Error GoTo 0
	End Function
	
	'% insExistsPolicy: Retorna si hay datos para la poliza/certificado en Insured_expdis
	Public Function insExistsPolicy(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date) As Boolean
		'- Objeto de conección a base de datos
		Dim lclsinsured_expdis As eRemoteDB.Execute
		
		On Error GoTo insExistsPolicy_Err
		lclsinsured_expdis = New eRemoteDB.Execute
		
		'+ Definición de parámetros del procedimiento reaClause_count al 24-05-2002
		With lclsinsured_expdis
			.StoredProcedure = "reaInsured_expdis_exist"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nExist", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				insExistsPolicy = .Parameters("nExist").Value = 1
			End If
		End With
		
insExistsPolicy_Err: 
		If Err.Number Then
			insExistsPolicy = False
		End If
		'UPGRADE_NOTE: Object lclsinsured_expdis may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsinsured_expdis = Nothing
		On Error GoTo 0
	End Function
	
	'% Class_Initialize: se controla la apertura de la clase
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		sSel = String.Empty
		sCertype = String.Empty
		nBranch = eRemoteDB.Constants.intNull
		nProduct = eRemoteDB.Constants.intNull
		nPolicy = eRemoteDB.Constants.intNull
		nCertif = eRemoteDB.Constants.intNull
		nGroup_insu = eRemoteDB.Constants.intNull
		dEffecdate = eRemoteDB.Constants.dtmNull
		sClient = String.Empty
		nDisexprc = eRemoteDB.Constants.intNull
		nModulec = eRemoteDB.Constants.intNull
		nCover = eRemoteDB.Constants.intNull
		dEffecdate = eRemoteDB.Constants.dtmNull
		sDisexpri = String.Empty
		sUnit = String.Empty
		nRate = eRemoteDB.Constants.intNull
		nAmount = eRemoteDB.Constants.intNull
		dNulldate = eRemoteDB.Constants.dtmNull
		sPerm_temp = String.Empty
		dDate_fr = eRemoteDB.Constants.dtmNull
		dDate_to = eRemoteDB.Constants.dtmNull
		dCompdate = eRemoteDB.Constants.dtmNull
		nAmount_old = eRemoteDB.Constants.intNull
		nPercent_old = eRemoteDB.Constants.intNull
		nAge = eRemoteDB.Constants.intNull
		nNotenum = eRemoteDB.Constants.intNull
		nCause = eRemoteDB.Constants.intNull
		sAgree = String.Empty
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'% sContent: Obtiene el indicador de contenido de la transacción
	Public ReadOnly Property sContent() As String
		Get
			sContent = mstrContent
		End Get
	End Property
	
	'% nRateForRating: Entrega el monto para el Rating
	Public ReadOnly Property nRateForRating() As Double
		Get
			'+ Si se trata de un recargo (técnico o comercial), es la cobertura de fallecimiento
			'+ no es una tasa y la causa de aplicación del recargo es "condiciones de salud",
			'+ se actualiza el rating del asegurado con el factor indicado
			If sCoverBase = "1" And (sDisexpri = "1" Or sDisexpri = "4") And sUnit <> "1" And nCause = 1 Then
				nRateForRating = nRate
			Else
				nRateForRating = 0
			End If
		End Get
	End Property
End Class






