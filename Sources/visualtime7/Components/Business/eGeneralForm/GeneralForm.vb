Option Strict Off
Option Explicit On
Public Class GeneralForm
	'%-------------------------------------------------------%'
	'% $Workfile:: GeneralForm.cls                          $%'
	'% $Author:: Nvaplat37                                  $%'
	'% $Date:: 24/11/03 4:00p                               $%'
	'% $Revision:: 16                                       $%'
	'%-------------------------------------------------------%'
	
	'+ Variable que indica el número de nota en tratamiento
	Public nNotenum As Integer
	
	'+ Variable públicas para el manejo final de las imágenes
	Public sClient As String
	Public nClaim As Double
	Public nServ_order As Double
	
	Public sCertype As String
	Public nBranch As Integer
	Public nProduct As Integer
	Public nPolicy As Integer
	Public nCertif As Integer
	Public nDeman_type As Integer
	Public nCase_num As Integer
	Public nClause As Integer
	Public dEffecdate As Date
	Public nId As Integer
	Public sRegist As String
	Public sLicense_ty As String
    Public nTransaction As Short

    Public sStyleNotes As String
	
	'- Se define la variable para verificar si el campo "Ubicación" de la ventana de imágenes
	'- tiene contenido
	Const CN_NOTEMPTY As String = "Con contenido"
	
	Private Enum eTypeRecType
		cstrComercial = 1 '+Dirección Comercial
		cstrParticular = 2 '+Dirección Particular
		cstrCasilla = 3 '+Dirección Casilla
	End Enum
	
	'% insPostGeneralNotes: Actualiza los datos correspondiente a las Notas
	Public Function insPostGeneralNotes(ByVal sCodispl As String, ByVal sAction As String, ByVal sClient As String, ByVal nClaim As Double, ByVal sWindowType As String, ByVal nNotenum As Integer, ByVal nConsec As Integer, Optional ByVal sDescript As String = "", Optional ByVal dCompdate As Date = #12:00:00 AM#, Optional ByVal dNulldate As Date = #12:00:00 AM#, Optional ByVal tDs_text As String = "", Optional ByVal nUsercode As Integer = 0, Optional ByVal nRectype As Integer = 0, Optional ByVal nTransac As Integer = 0) As Boolean
		Dim lblnPost As Boolean
		Dim llngNotenum As Integer
		
		Dim lclsClient As Object
		Dim lclsClaim As Object
		Dim lclsClaim_his As Object
		Dim lclsPolicy As Object
		Dim lclsCertificat As Object
		Dim lclsAuto_db As Object
		Dim lclsNotess As eGeneralForm.Notess
		Dim lclsPolicy_Win As Object
		
		On Error GoTo insPostGeneralNotes_Err
		
		'+ Objeto general para las actualizaciones necesarias sobre (clase)_win
		'+ con el objeto de actualizar el contenido de la página.
		Dim lobjXwin As Object
		
		If LCase(sWindowType) <> "popup" Then
			insPostGeneralNotes = True
		Else
			insPostGeneralNotes = insPostNotes(sAction, sClient, CStr(nNotenum), CStr(nConsec), sDescript, dCompdate, dNulldate, tDs_text, CStr(nUsercode), CStr(nRectype))
			llngNotenum = Me.nNotenum
			
			'+ Se actualiza el número de nota asociado al texto libre del beneficiario
			If sCodispl = "SCA2-1" Then
				lclsCertificat = eRemoteDB.NetHelper.CreateClassInstance("ePolicy.Certificat")
				With lclsCertificat
					lblnPost = .insUpdNote_Benef(sCertype, nBranch, nProduct, nPolicy, nCertif, nUsercode, llngNotenum)
				End With
				'+ Se actualiza el número de nota asociado al texto libre del beneficiario
			ElseIf sCodispl = "SCA2-4" Then 
				lclsPolicy = eRemoteDB.NetHelper.CreateClassInstance("ePolicy.Policy")
				With lclsPolicy
					If .Find(sCertype, nBranch, nProduct, nPolicy) Then
						.nNote_adend = llngNotenum
						lblnPost = .Add()
					End If
				End With
				
				'+ Se actualiza el número de nota asociado al cliente
			ElseIf sCodispl = "SCA2-9" Then 
				lclsClient = eRemoteDB.NetHelper.CreateClassInstance("eClient.Client")
				With lclsClient
					.sClient = sClient
					.nUsercode = nUsercode
					lblnPost = .UpdateNoteNum(llngNotenum)
				End With
				
				'+ Se actualiza el número de nota asociado al siniestro
			ElseIf sCodispl = "SCA2-8" Then 
				lclsClaim = eRemoteDB.NetHelper.CreateClassInstance("eClaim.Claim")
				With lclsClaim
					If .Find(nClaim) Then
						.nNotenum = llngNotenum
						lblnPost = .Update()
					End If
				End With
				'+ Se actualiza el número de nota asociado al movimiento de rechazo de siniestro
			ElseIf sCodispl = "SCA2-961" Then 
				lclsClaim = eRemoteDB.NetHelper.CreateClassInstance("eClaim.Claim")
				With lclsClaim
					If .Find(nClaim) Then
						lclsClaim_his = eRemoteDB.NetHelper.CreateClassInstance("eClaim.Claim_his")
						lclsClaim_his.nClaim = nClaim
						lclsClaim_his.nNotenum = llngNotenum
						lclsClaim_his.nTransac = nTransac
						lclsClaim_his.nUsercode = nUsercode
                        lblnPost = lclsClaim_his.Update_Notes()
                        .nNotenum = llngNotenum
                        .Update()
                    End If
				End With
				'+ Se actualiza el número de nota asociado al comentario de la póliza
			ElseIf sCodispl = "SCA2-3" Then 
				lclsPolicy = eRemoteDB.NetHelper.CreateClassInstance("ePolicy.Policy")
				With lclsPolicy
					If .Find(sCertype, nBranch, nProduct, nPolicy) Then
						.nNote_comme = llngNotenum
						lblnPost = .Add()
						lclsNotess = New Notess
                        lclsPolicy_Win = eRemoteDB.NetHelper.CreateClassInstance("ePolicy.Policy_Win")
						If lclsNotess.Find(llngNotenum) Then
							Call lclsPolicy_Win.Add_PolicyWin(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nUsercode, "SCA2-3", "2")
						Else
							Call lclsPolicy_Win.Add_PolicyWin(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nUsercode, "SCA2-3", "1")
						End If
					End If
				End With
				
				'+ Descripción de bienes asegurables
			ElseIf sCodispl = "SCA2-H" Then 
				lclsCertificat = eRemoteDB.NetHelper.CreateClassInstance("ePolicy.Certificat")
				With lclsCertificat
					If .Find(sCertype, nBranch, nProduct, nPolicy, nCertif) Then
						.nNote_drisk = llngNotenum
						lblnPost = .Update()
						lclsNotess = New Notess
						lclsPolicy_Win = eRemoteDB.NetHelper.CreateClassInstance("ePolicy.Policy_Win")
						If lclsNotess.Find(llngNotenum) Then
							Call lclsPolicy_Win.Add_PolicyWin(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nUsercode, "SCA2-H", "2")
						Else
							Call lclsPolicy_Win.Add_PolicyWin(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nUsercode, "SCA2-H", "1")
						End If
					End If
				End With
				
				'+ SCA2-5 : Nota de daños ocurridos al vehículo
				'+ SCA2-S : Notas de los casos de siniestros
			ElseIf sCodispl = "SCA2-5" Or sCodispl = "SCA2-S" Or sCodispl = "SCA2-6" Then 
				lclsClaim = eRemoteDB.NetHelper.CreateClassInstance("eClaim.Claim_case")
				With lclsClaim
					.nUsercode = nUsercode
					lblnPost = .UpdatenNoteDama(nClaim, nDeman_type, nCase_num, llngNotenum)
					
					If lblnPost Then
						lobjXwin = eRemoteDB.NetHelper.CreateClassInstance("eClaim.Cases_win")
						Call lobjXwin.Add_Cases_win(nClaim, nCase_num, nDeman_type, sCodispl, "2", nUsercode)
					End If
				End With
				
				'+SCA2-10 : Nota de declaración del asegurado
			ElseIf sCodispl = "SCA2-10" Then 
                lclsClaim = eRemoteDB.NetHelper.CreateClassInstance("eClaim.Claim_auto")
				With lclsClaim
					.nUsercode = nUsercode
					lblnPost = .UpdateNoteNum(nClaim, nDeman_type, nCase_num, llngNotenum)
				End With
				
				'+ Se actualiza el número de nota asociado a la base de datos de automoviles
			ElseIf sCodispl = "SCA2-M" Then 
				lclsAuto_db = eRemoteDB.NetHelper.CreateClassInstance("ePolicy.Auto_db")
				With lclsAuto_db
					If sAction = "Delete" Then
						llngNotenum = numNull
					Else
						If .Find_db1(Me.sLicense_ty, Me.sRegist) Then
							.nNotenum = llngNotenum
						End If
					End If
					.Update()
				End With
				
				'+ Se actualiza el número de nota asociado a la orden de servicio
			ElseIf sCodispl = "SCA649" Then 
				lclsClaim = eRemoteDB.NetHelper.CreateClassInstance("eClaim.Prof_ord")
				With lclsClaim
					If sAction = "Delete" Then
						llngNotenum = numNull
					End If
					If .Find_nServ(nServ_order) Then
						.InsPostOS590Upd(nServ_order, .dMade_date, .sPlace, .nMunicipality, .nStatus_ord, nUsercode, .nImagenum, llngNotenum)
					End If
				End With
				
				lclsCertificat = eRemoteDB.NetHelper.CreateClassInstance("ePolicy.Certificat")
				
				'+ Se actualiza en número de nota en el certificado de la póliza
				With lclsCertificat
					If .Find(Me.sCertype, Me.nBranch, Me.nProduct, Me.nPolicy, Me.nCertif) Then
						If sAction = "Delete" Then
							llngNotenum = numNull
						End If
						.nNote_drisk = llngNotenum
						.Update()
					End If
				End With
				
				'+ Se actualiza el número de nota asociado a un registro de historio de una póliza
			ElseIf sCodispl = "SCA2-810" Then 
				lclsPolicy = eRemoteDB.NetHelper.CreateClassInstance("ePolicy.Policy_his")
				With lclsPolicy
					lblnPost = .updPolHisnNotenum(sCertype, nBranch, nProduct, nPolicy, nCertif, nUsercode, llngNotenum, numNull, numNull)
				End With
				
				'+ Se actualiza el número de nota de cobertura a la póliza
			ElseIf sCodispl = "SCA2-F" Then 
				lclsPolicy = eRemoteDB.NetHelper.CreateClassInstance("ePolicy.Policy")
				With lclsPolicy
					If .Find(Me.sCertype, Me.nBranch, Me.nProduct, Me.nPolicy) Then
						.nNote_cover = llngNotenum
						lblnPost = .Add()
					End If
				End With
            ElseIf sCodispl = "SCA2-WC"
                Dim warranty_cli As Object = eRemoteDB.NetHelper.CreateClassInstance("eClient.Warranty_Cli") 

                'lblnPost = warranty_cli.UpdateNoteNum(ByVal sClient As String, ByVal sDocWarranty_Cli As String, ByVal nTypeWarranty_Cli As Integer, ByVal nNoteNum As Integer, ByVal nUserCode As Integer)
			End If
		End If
		
insPostGeneralNotes_Err: 
		If Err.Number Then
			insPostGeneralNotes = False
		End If
		'UPGRADE_NOTE: Object lobjXwin may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjXwin = Nothing
		'UPGRADE_NOTE: Object lclsClient may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsClient = Nothing
		'UPGRADE_NOTE: Object lclsClaim may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsClaim = Nothing
		'UPGRADE_NOTE: Object lclsClaim_his may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsClaim_his = Nothing
		'UPGRADE_NOTE: Object lclsPolicy may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsPolicy = Nothing
		'UPGRADE_NOTE: Object lclsCertificat may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsCertificat = Nothing
		'UPGRADE_NOTE: Object lclsAuto_db may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsAuto_db = Nothing
		'UPGRADE_NOTE: Object lclsNotess may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsNotess = Nothing
		'UPGRADE_NOTE: Object lclsPolicy_Win may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsPolicy_Win = Nothing
		On Error GoTo 0
	End Function
	
	'%insValSCA001. Esta function se encarga de realizar las validadiones corespondientes
	'% de la ventana de direcciones SCA001.
	Public Function insValSCA001(ByVal sCodispl As String, ByVal sRecType As String, ByVal sStreet As String, ByVal nZipCode As Double, ByVal sLocalCode As String, ByVal sCountry As String, ByVal sLonCardinG As String, ByVal sLonCardinM As String, ByVal sLonCardinS As String, ByVal sLatCardinG As String, ByVal sLatCardinM As String, ByVal sLatCardinS As String, ByVal sMunicipality As String, ByVal nDeldirec As Integer, ByVal sClient As String, ByVal dEffecdate As Date, ByVal sPobox As String, ByVal sInfor As String, ByVal sE_mail As String, ByVal sProvince As String, ByVal sBuild As String, ByVal nSendAddr As Short) As String
		Dim lobjError As eFunctions.Errors
		Dim lbCancel As Boolean
		Dim lstrMess As String
		Dim lobjValues As eFunctions.Values
		Dim lobjAddresss As eGeneralForm.Addresss
		Dim lobjRoles As Object
		Dim lbSwitch As Boolean
		Dim lstrKeyAddress As String
		Dim lstrRecType As eTypeRecType
		Dim lblnValidate As Boolean
		Dim lobjPolicy As Object
		
		'- Se agrega para la lectura a address hoja 416
		Dim lobjAddress As eGeneralForm.Address
		
		'- Se agrega para creacion de llave hoja 416
		Dim lstrKey As String
		
		On Error GoTo insValSCA001_Err
		
		lobjValues = New eFunctions.Values
		lobjError = New eFunctions.Errors
		
		lbSwitch = True
		
		insValSCA001 = String.Empty
		
		If sRecType = String.Empty Then
			sRecType = CStr(eTypeRecType.cstrComercial)
		End If
		
		lstrRecType = CShort(sRecType)
		
		If sRecType = CStr(eTypeRecType.cstrComercial) Then
			lstrMess = " (Comercial)"
		ElseIf sRecType = CStr(eTypeRecType.cstrParticular) Then 
			lstrMess = " (Particular)"
		ElseIf sRecType = CStr(eTypeRecType.cstrCasilla) Then 
			lstrMess = " (Casilla)"
		Else
			lstrMess = String.Empty
		End If
		
		'+ Validación de existencia de poliza para el cliente si se está modificando dirección
		If sCodispl = "SCA101" Then
			lobjAddresss = New Addresss
			With lobjAddresss
				lstrKeyAddress = .ConstructKeyAddress(2, lstrRecType, "", 0, 0, 0, 0, 0, sClient, 0, 0, 0, 0)
				lobjAddress = New Address
				With lobjAddress
					If .Find(lstrKeyAddress, 2, dEffecdate, True) Then
						lobjRoles = eRemoteDB.NetHelper.CreateClassInstance("ePolicy.Roles")
						With lobjRoles
							If .valExistsRoles_Pol("2", sClient, dEffecdate) Then
								Call lobjError.ErrorMessage(sCodispl, 55647)
							End If
						End With
					End If
				End With
			End With
		End If
		
		lblnValidate = True
		
		If sCodispl = "SCA102" Then
			If nTransaction = 12 Or nTransaction = 14 Then
				'+ Si se trata de un endoso, y la dirección de envío no es por "Por póliza",
				'+ no se deben realizar las validaciones de los campos de la página
				If nSendAddr <> 4 Then
					lobjPolicy = eRemoteDB.NetHelper.CreateClassInstance("ePolicy.Policy")
					If lobjPolicy.Find(sCertype, nBranch, nProduct, nPolicy) Then
						sClient = lobjPolicy.sClient
					End If
					'UPGRADE_NOTE: Object lobjPolicy may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					lobjPolicy = Nothing
					
					lblnValidate = False
					
					'+ Se invierten los valores porque la tabla que maneja la dirección de envío está invertida
					If nSendAddr = eTypeRecType.cstrComercial Then
						lstrRecType = eTypeRecType.cstrParticular
					ElseIf nSendAddr = eTypeRecType.cstrParticular Then 
						lstrRecType = eTypeRecType.cstrComercial
					End If
					
					lobjAddresss = New Addresss
					lstrKeyAddress = lobjAddresss.ConstructKeyAddress(Address.eTypeRecOwner.clngClientAddress, lstrRecType, String.Empty, 0, 0, 0, 0, 0, sClient)
					lobjAddress = New Address
					If Not lobjAddress.Find(lstrKeyAddress, Address.eTypeRecOwner.clngClientAddress, dEffecdate, True) Then
						'+ El cliente debe tener dirección asociada
						Call lobjError.ErrorMessage(sCodispl, 56171)
					End If
				End If
			End If
		End If
		
		If lblnValidate Then
			
			'+ Validación del número de estructura de la dirección
			If sBuild = String.Empty Then
				Call lobjError.ErrorMessage(sCodispl, 1012,  , eFunctions.Errors.TextAlign.LeftAling, "Número: ")
			End If
			
			'+ Validación del numero de Casilla
			If sPobox = String.Empty And sRecType = "3" Then
				Call lobjError.ErrorMessage(sCodispl, 55648)
			End If
			
			'+ Validacion de la Comuna
			If CInt("0" & sMunicipality) = 0 Then
                If lobjError.ErrorMessage(sCodispl, 1970) > String.Empty Then
                    lbCancel = True
                End If
            Else
                If Not lobjValues.IsValid("tabmunicipality", sMunicipality) Then
                    If lobjError.ErrorMessage(sCodispl, 1971) > String.Empty Then
                        lbCancel = True
                    End If
                End If
            End If
			
			'+ Validación de la Localidad
			If CShort("0" & sLocalCode) = 0 Then
				If lobjError.ErrorMessage(sCodispl, 1907,  , 1, lstrMess) > String.Empty Then
					lbCancel = True
				End If
			Else
				lobjValues = eRemoteDB.NetHelper.CreateClassInstance("eFunctions.Values")
				If Not lobjValues.IsValid("tabTab_locat", sLocalCode) Then
					If lobjError.ErrorMessage(sCodispl, 80008,  , 1, lstrMess) > String.Empty Then
						lbCancel = True
					End If
				End If
				'UPGRADE_NOTE: Object lobjValues may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
				lobjValues = Nothing
			End If
			
			'+ Validación del país
			If CShort("0" & sCountry) = 0 Then
				If lobjError.ErrorMessage(sCodispl, 1049,  , 1, lstrMess) > String.Empty Then
					lbCancel = True
				End If
			End If
			
			'+ Validación del país
			If CShort("0" & sProvince) = 0 Then
				If lobjError.ErrorMessage(sCodispl, 1910,  , 1, lstrMess) > String.Empty Then
					lbCancel = True
				End If
			Else
				lobjValues = eRemoteDB.NetHelper.CreateClassInstance("eFunctions.Values")
				If Not lobjValues.IsValid("tab_Province", sProvince) Then
					If lobjError.ErrorMessage(sCodispl, 1911,  , 1, lstrMess) > String.Empty Then
						lbCancel = True
					End If
				End If
				'UPGRADE_NOTE: Object lobjValues may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
				lobjValues = Nothing
			End If
			
			'+ Validación cuando el rectype sea particular o comercial
			If sRecType <> "3" Then
				
				'+ Validación de la direccion de envio
				If sInfor <> String.Empty Then
					lobjAddress = New Address
					If lobjAddress.Find_sInfor_Count(sClient, sRecType, dEffecdate) Then
						Call lobjError.ErrorMessage(sCodispl, 55738)
					End If
				End If
				
				'+ Validación para poder borrar la dirección(no debe estar asociada a una póliza)
				If nDeldirec = 1 Then
					lobjAddress = New Address
					If lobjAddress.Find_Address_Certif(sClient, sRecType, dEffecdate, 1, True) Then
						If lobjAddress.nCount > 0 Then
							Call lobjError.ErrorMessage(sCodispl, 38047)
						End If
					End If
					
					If lobjAddress.Find_Address_Certif(sClient, sRecType, dEffecdate, 2, True) Then
						If lobjAddress.nCount < 2 Then
							Call lobjError.ErrorMessage(sCodispl, 38048)
						End If
					End If
				End If
				
				If sE_mail <> String.Empty Then
					If InStr(sE_mail, "@") = 0 Then
						Call lobjError.ErrorMessage(sCodispl, 55739)
					End If
				End If
				
				'+ Validación de la Calle
				If RTrim(sStreet) = String.Empty Then
					If lobjError.ErrorMessage(sCodispl, 1906,  , 1, lstrMess) > String.Empty Then
						lbCancel = True
					End If
				End If
				
				'+ Si se indica coordenadas se deben validar
				If Trim(sLonCardinG) <> String.Empty Or Trim(sLonCardinM) <> String.Empty Or Trim(sLonCardinS) <> String.Empty Or Trim(sLatCardinG) <> String.Empty Or Trim(sLatCardinM) <> String.Empty Or Trim(sLatCardinS) <> String.Empty Then
					
					'+ Validación de la longitud
					If Trim(sLonCardinG) = String.Empty Then
						If lobjError.ErrorMessage(sCodispl, 1917,  , 1, lstrMess) > String.Empty Then
						End If
					Else
						If Not IsNumeric("0" & Trim(sLonCardinG)) Then
							If lobjError.ErrorMessage(sCodispl, 1917,  , 1, lstrMess) > String.Empty Then
							End If
						Else
							If CInt(Trim("0" & sLonCardinG)) < 0 Or CInt(Trim("0" & sLonCardinG)) > 180 Then
								If lobjError.ErrorMessage(sCodispl, 1917,  , 1, lstrMess) > String.Empty Then
									lbCancel = True
								End If
							End If
						End If
					End If
					
					If Trim(sLonCardinM) = String.Empty Then
						If lobjError.ErrorMessage(sCodispl, 80013,  , 1, lstrMess) > String.Empty Then
							lbCancel = True
						End If
					Else
						If Not IsNumeric(Trim(sLonCardinM)) Or Not IsNumeric(Trim(sLonCardinG)) Then
							If lobjError.ErrorMessage(sCodispl, 80013,  , 1, lstrMess) > String.Empty Then
							End If
						Else
							If CDbl(Trim(sLonCardinM)) < 0 Or CDbl(Trim(sLonCardinM)) > 60 Then
								If lobjError.ErrorMessage(sCodispl, 80013,  , 1, lstrMess) > String.Empty Then
								End If
							Else
								If CDbl(Trim(sLonCardinM)) > 0 And CInt(Trim(sLonCardinG)) > 179 Then
									If lobjError.ErrorMessage(sCodispl, 1916,  , 1, lstrMess) > String.Empty Then
									End If
								End If
							End If
						End If
					End If
					
					If Trim(sLonCardinS) = String.Empty Then
						If lobjError.ErrorMessage(sCodispl, 80014,  , 1, lstrMess) > String.Empty Then
						End If
					Else
						If Not IsNumeric(Trim(sLonCardinS)) Or Not IsNumeric(Trim(sLonCardinG)) Then
							If lobjError.ErrorMessage(sCodispl, 80014,  , 1, lstrMess) > String.Empty Then
							End If
						Else
							If CInt(Trim(sLonCardinS)) < 0 Or CInt(Trim(sLonCardinS)) > 60 Then
								If lobjError.ErrorMessage(sCodispl, 80014,  , 1, lstrMess) > String.Empty Then
								End If
							Else
								If CInt(Trim(sLonCardinS)) > 0 And CInt(Trim(sLonCardinG)) > 179 Then
									If lobjError.ErrorMessage(sCodispl, 1916,  , 1, lstrMess) > String.Empty Then
									End If
								End If
							End If
						End If
					End If
					
					'+ Validacion de la latitud
					If Trim(sLatCardinG) = String.Empty Then
						If lobjError.ErrorMessage(sCodispl, 80012,  , 1, lstrMess) > String.Empty Then
						End If
					Else
						If Not IsNumeric(Trim(sLatCardinG)) Then
							If lobjError.ErrorMessage(sCodispl, 80012,  , 1, lstrMess) > String.Empty Then
							End If
						Else
							If CInt(Trim(sLatCardinG)) < 0 Or CInt(Trim(sLatCardinG)) > 90 Then
								If lobjError.ErrorMessage(sCodispl, 80012,  , 1, lstrMess) > String.Empty Then
								End If
							End If
						End If
					End If
					
					If Trim(sLatCardinM) = String.Empty Then
						If lobjError.ErrorMessage(sCodispl, 80013,  , 1, lstrMess) > String.Empty Then
						End If
					Else
						If Not IsNumeric(Trim(sLatCardinM)) Or Not IsNumeric(Trim(sLatCardinG)) Then
							If lobjError.ErrorMessage(sCodispl, 80013,  , 1, lstrMess) > String.Empty Then
							End If
						Else
							If CDbl(Trim(sLatCardinM)) < 0 Or CDbl(Trim(sLatCardinM)) > 60 Then
								If lobjError.ErrorMessage(sCodispl, 80013,  , 1, lstrMess) > String.Empty Then
								End If
							Else
								If CDbl(Trim(sLatCardinM)) > 0 And CInt(Trim(sLatCardinG)) > 89 Then
									lbSwitch = False
									If lobjError.ErrorMessage(sCodispl, 1915,  , 1, lstrMess) > String.Empty Then
									End If
								End If
							End If
						End If
					End If
					
					If Trim(sLatCardinS) = String.Empty Then
						If lobjError.ErrorMessage(sCodispl, 80014,  , 1, lstrMess) > String.Empty Then
						End If
					Else
						If Not IsNumeric(Trim(sLatCardinS)) Or Not IsNumeric(Trim(sLatCardinG)) Then
							If lobjError.ErrorMessage(sCodispl, 80014,  , 1, lstrMess) > String.Empty Then
							End If
						Else
							If CInt(Trim(sLatCardinS)) < 0 Or CInt(Trim(sLatCardinS)) > 60 Then
								If lobjError.ErrorMessage(sCodispl, 80014,  , 1, lstrMess) > String.Empty Then
								End If
							Else
								If lbSwitch Then
									If CInt(Trim(sLatCardinS)) > 0 And CInt(Trim(sLatCardinG)) > 89 Then
										If lobjError.ErrorMessage(sCodispl, 1915,  , 1, lstrMess) > String.Empty Then
										End If
									End If
								End If
							End If
						End If
					End If
				End If
			End If
		End If
		
		insValSCA001 = lobjError.Confirm
		
insValSCA001_Err: 
		If Err.Number Then
			insValSCA001 = "<P>" & Err.Description & "</P>"
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lobjError may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjError = Nothing
		'UPGRADE_NOTE: Object lobjRoles may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjRoles = Nothing
		'UPGRADE_NOTE: Object lobjAddresss may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjAddresss = Nothing
		'UPGRADE_NOTE: Object lobjAddress may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjAddress = Nothing
	End Function
	
	'% insValPhones: se realizan las validaciones -puntuales y/o masivas- que se realizan a los
	'%              campos del frame.
    Public Function insValPhones(ByVal sCodispl As String, ByVal nRecOwner As String, ByVal sKeyAddress As String, ByVal nKeyPhones As String, ByVal nArea_code As String, ByVal dEffecdate As String, ByVal sPhone As String, ByVal nOrder As String, ByVal nExtens1 As String, ByVal nPhone_type As String, ByVal nExtens2 As String, ByVal sAction As String) As String
        Dim lobjFilter As eFunctions.Values
        Dim lstrCodispl As String
        Dim lobjErrors As Object
        Dim lobjPhone As Object
        Dim lcolPhones As Object
        lobjErrors = New eFunctions.Errors ' eRemoteDB.NetHelper.CreateClassInstance("eFunctions.Errors")
        lcolPhones = eRemoteDB.NetHelper.CreateClassInstance("eGeneralForm.Phones")

        lobjPhone = eRemoteDB.NetHelper.CreateClassInstance("eGeneralForm.Phone")
        lobjFilter = New eFunctions.Values 'CreateObject ("eFunctions.")
        lstrCodispl = sCodispl

        '+ Validaciones del campo <orden>
        '+ Se verifica que sea distinto de cero
        If lobjFilter.StringToType(nOrder, eFunctions.Values.eTypeData.etdInteger) = 0 Then
            lobjErrors.ErrorMessage(lstrCodispl, 3664)
            '+ Se verifica que sea distinto de null
        ElseIf nOrder = String.Empty And (nArea_code <> String.Empty Or sPhone <> String.Empty Or nExtens1 <> String.Empty Or nPhone_type <> String.Empty Or nExtens2 <> String.Empty) Then
            lobjErrors.ErrorMessage(lstrCodispl, 1084)
            '+ Se valida que no estè duplicado
        ElseIf sAction = "Add" Then
            If lcolPhones.Find(nRecOwner, sKeyAddress, Today) Then
                For Each lobjPhone In lcolPhones
                    If lobjPhone.nOrder = lobjFilter.StringToType(nOrder, eFunctions.Values.eTypeData.etdInteger) Then
                        lobjErrors.ErrorMessage(lstrCodispl, 80029)
                        Exit For
                    End If
                Next lobjPhone
            End If
        End If

        '+ Validaciones del campo <Tipo>
        '    If nPhone_type = String.Empty Or lobjFilter.StringToType(nPhone_type, etdInteger) = 0 Then
        '        lobjErrors.ErrorMessage lstrCodispl, 1919
        '    End If

        '+ Validaciones del campo <Còdigo del àrea>
        '    If nArea_code = String.Empty Or lobjFilter.StringToType(nArea_code, etdInteger) = 0 Then
        '        lobjErrors.ErrorMessage lstrCodispl, 1920
        '    End If

        '+ Validaciones del campo <Número telefónico>
        '+ Se valida que no estè vacio
        If Trim(sPhone) = String.Empty Then
            lobjErrors.ErrorMessage(lstrCodispl, 1921)
        Else
            '+ Se valida que la combinaciòn àrea-telèfono no estè registrada
            If sAction = "Add" Then
                If lcolPhones.Find(nRecOwner, sKeyAddress, Today) Then
                    For Each lobjPhone In lcolPhones
                        If lobjPhone.nArea_code = lobjFilter.StringToType(nArea_code, eFunctions.Values.eTypeData.etdInteger) And lobjPhone.sPhone = sPhone Then
                            lobjErrors.ErrorMessage(lstrCodispl, 1936)
                            Exit For
                        End If
                    Next lobjPhone
                End If
            End If
        End If

        '+ Se valida que si se indica orden, teléfono debe estar lleno
        If nOrder <> String.Empty And Trim(sPhone) = String.Empty Then
            lobjErrors.ErrorMessage(lstrCodispl, 55889)
        End If


        insValPhones = lobjErrors.Confirm
        'UPGRADE_NOTE: Object lobjFilter may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lobjFilter = Nothing
        'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lobjErrors = Nothing
        'UPGRADE_NOTE: Object lobjPhone may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lobjPhone = Nothing
        'UPGRADE_NOTE: Object lcolPhones may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lcolPhones = Nothing

insValSCA101_Err:
        If Err.Number Then
            insValPhones = CStr(False)
        End If
        On Error GoTo 0
    End Function
	
	'% insValSCA002: se realizan las validaciones puntuales y masivas del frame
	Public Function insValSCA002(ByVal sCodispl As String, ByVal sType As String, ByVal sDescript As String, ByVal dCompdate As String, ByVal dNulldate As String, Optional ByVal tDs_text As String = "", Optional ByVal sSource As String = "", Optional ByVal sWindowType As String = "", Optional ByVal nCountNote As Integer = 0) As String
        Dim lstrMessage As String = ""
        Dim lclsQuery As eRemoteDB.Query
		Dim lobjErrors As eFunctions.Errors
		Dim lclsvalField As eFunctions.valField
		Dim llnglenght As Integer
        Dim lclsWindows As eSecurity.Windows
		
		On Error GoTo insValSCA002_Err
		
		lobjErrors = New eFunctions.Errors
        lclsWindows = New eSecurity.Windows
		
		If sWindowType = "PopUp" Then
			'+ Validacion del campo Fecha límite
			If dNulldate <> String.Empty Then
				lclsvalField = New eFunctions.valField
				If lclsvalField.ValDate(dNulldate,  , eFunctions.valField.eTypeValField.onlyvalid) Then
					
					'+ Debe ser mayor a la fecha de creación
					If CDate(dCompdate) >= CDate(dNulldate) Then
						Call lobjErrors.ErrorMessage(sCodispl, 2086)
					End If
				End If
				'UPGRADE_NOTE: Object lclsvalField may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
				lclsvalField = Nothing
			End If
			
			'+ Validacion del campo Descripción - Debe estar lleno
			If sDescript = String.Empty Then
				Call lobjErrors.ErrorMessage(sCodispl, 3872)
			End If
			
			'+ Validacion del campo Detalle
			
			'+ El campo Detalle debe estar lleno
			If sType = "Note" Then
				If tDs_text = String.Empty Then
					Call lobjErrors.ErrorMessage(sCodispl, 3873)
                Else
                    '+ Si se esta registrando una nota de rechazo y el producto es SOAP
                    If lclsWindows.reaWindows(sCodispl) Then
                        If lclsWindows.nLength_Notes > 0 Then
                            If tDs_text.Length > lclsWindows.nLength_Notes Then
                                Call lobjErrors.ErrorMessage(sCodispl, 99154, , eFunctions.Errors.TextAlign.RigthAling, " Máximo: " & lclsWindows.nLength_Notes.ToString & " carácteres")
				End If
                        End If
                    End If

                End If
			Else
				
				'+ Si se está tratando con imágenes, el campo Ubicación debe estar lleno
				If sSource = String.Empty Then
					lclsQuery = New eRemoteDB.Query
					With lclsQuery
						If .OpenQuery("Table563", "sDescript", "nCodigInt=101") Then
							lstrMessage = .FieldToClass("sDescript") & ": "
							.CloseQuery()
						End If
					End With
					Call lobjErrors.ErrorMessage(sCodispl, 1925,  , eFunctions.Errors.TextAlign.LeftAling, lstrMessage)
					'UPGRADE_NOTE: Object lclsQuery may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					lclsQuery = Nothing
				Else
					'+ Valida que el archivo seleccionado sea de un formato válido (BMP - GIF - JPG)
					If Not Trim(UCase(sSource)) = "CON CONTENIDO" Then
						llnglenght = Len(Trim(sSource))
						If UCase(Mid(Trim(sSource), llnglenght - 2, llnglenght)) <> "BMP" And UCase(Mid(Trim(sSource), llnglenght - 2, llnglenght)) <> "GIF" And UCase(Mid(Trim(sSource), llnglenght - 2, llnglenght)) <> "JPG" Then
							Call lobjErrors.ErrorMessage(sCodispl, 55990)
						End If
					End If
				End If
			End If
		Else
			'+ valida la ventana "SCA2_1"(beneficiarios de poliza)
			If sCodispl = "SCA2-1" Then
				If nCountNote = 0 Then
					Call lobjErrors.ErrorMessage(sCodispl, 707009)
				End If
			End If
		End If
		
		insValSCA002 = lobjErrors.Confirm
		
		'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjErrors = Nothing
        lclsWindows = Nothing
		
insValSCA002_Err: 
		If Err.Number Then
			insValSCA002 = insValSCA002 & "insValSCA002: " & Err.Description
		End If
		On Error GoTo 0
	End Function
	
	'% insPostNotes: Actualiza los datos correspondiente a las Notas
	Public Function insPostNotes(ByVal Action As String, ByVal sClient As String, ByVal nNotenum As String, ByVal nConsec As String, Optional ByVal sDescript As String = "", Optional ByVal dCompdate As Date = #12:00:00 AM#, Optional ByVal dNulldate As Date = #12:00:00 AM#, Optional ByVal tDs_text As String = "", Optional ByVal nUsercode As String = "", Optional ByVal nRectype As String = "") As Boolean
		Dim lclsNotes As Notes
		Dim lclsValues As eFunctions.Values
		
		On Error GoTo insPostNotes_Err
		
		lclsNotes = New Notes
		lclsValues = New eFunctions.Values
		
		With lclsNotes
			.sClient = sClient
			.nUsercode = lclsValues.StringToType(nUsercode, eFunctions.Values.eTypeData.etdLong)
			.nNotenum = lclsValues.StringToType(nNotenum, eFunctions.Values.eTypeData.etdLong)
			.nConsec = lclsValues.StringToType(nConsec, eFunctions.Values.eTypeData.etdLong)
			.sDescript = sDescript
			.dCompdate = dCompdate
			.dNulldate = dNulldate
			.tDs_text = tDs_text
			.nRectype = lclsValues.StringToType(nRectype, eFunctions.Values.eTypeData.etdLong)

            '.sStyleNotes = sStyleNotes

            Select Case Action
				Case "Add"
					insPostNotes = .Add
				Case "Update"
					insPostNotes = .Update
				Case "Delete"
					insPostNotes = .Delete
			End Select
			Me.nNotenum = .nNotenum
		End With
		
		'UPGRADE_NOTE: Object lclsValues may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsValues = Nothing
		'UPGRADE_NOTE: Object lclsNotes may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsNotes = Nothing
		
insPostNotes_Err: 
		If Err.Number Then
			insPostNotes = False
		End If
		On Error GoTo 0
	End Function
	
	
	'--------------------------------------------------
    Public Function insPostPhones(ByVal sCodispl As Object, ByVal nRecOwner As Object, ByVal sKeyAddress As Object, ByVal nKeyPhones As Object, ByVal nArea_code As Object, ByVal dEffecdate As Object, ByVal sPhone As Object, ByVal nOrder As Object, ByVal nExtens1 As Object, ByVal nPhone_type As Object, ByVal nExtens2 As Object, ByVal sAction As Object) As Boolean
        '--------------------------------------------------
        Dim lobjPhone As Phone
        lobjPhone = New Phone
        Select Case sAction
            Case "Add"
                With lobjPhone
                    .dEffecdate = dEffecdate
                    .nArea_code = nArea_code
                    .nExtens1 = nExtens1
                    .nExtens2 = nExtens2
                    .nKeyPhones = nKeyPhones
                    .nOrder = nOrder
                    .nPhone_type = nPhone_type
                    .nRecowner = nRecOwner
                    '                .nUsercode = nUsercode
                    .sKeyAddress = sKeyAddress
                    .sPhone = sPhone
                    .Add()
                End With
            Case "Update"
                With lobjPhone
                    .Find(sKeyAddress, nKeyPhones, Address.eTypeRecOwner.clngClientAddress, Today)
                    .dEffecdate = dEffecdate
                    .nArea_code = nArea_code
                    .nExtens1 = nExtens1
                    .nExtens2 = nExtens2
                    .nKeyPhones = nKeyPhones
                    .nOrder = nOrder
                    .nPhone_type = nPhone_type
                    .nRecowner = nRecOwner
                    '                .nUsercode = nUsercode
                    .sKeyAddress = sKeyAddress
                    .sPhone = sPhone
                    .Update()
                End With
            Case "Delete"
                With lobjPhone
                    .Find(sKeyAddress, nKeyPhones, Address.eTypeRecOwner.clngClientAddress, Today)
                    .Delete()
                End With
                insPostPhones = True
        End Select
    End Function
	
	'% insPostImages: Actualiza los datos correspondiente a las imagenes
	Public Function insPostImages(ByVal Action As String, ByVal sCodispl As String, ByVal nImagenum As Integer, ByVal nConsec As Integer, Optional ByVal sDescript As String = "", Optional ByVal dCompdate As Date = #12:00:00 AM#, Optional ByVal dNulldate As Date = #12:00:00 AM#, Optional ByVal iImage As String = "", Optional ByVal nUsercode As Integer = 0, Optional ByVal nRectype As Integer = 0) As Boolean
		Dim lcolImagess As Imagess
		Dim lclsImages As Images
		Dim lclsClaim As Object
		Dim lclsClient As Object
		Dim lblnResp As Boolean
		
		On Error GoTo insPostImages_Err
		
		lclsImages = New Images
		lcolImagess = New Imagess
		
		With lclsImages
			.sClient = sClient
			.nUsercode = nUsercode
			.nImagenum = nImagenum
			.nConsec = nConsec
			.sDescript = sDescript
			.dCompdate = dCompdate
			.dNulldate = dNulldate
			.nRectype = nRectype
			
			Select Case Action
				Case "Add"
					insPostImages = .Add
				Case "Update"
					insPostImages = .Update
				Case "Del"
					insPostImages = .Delete
			End Select
		End With
		
		Select Case sCodispl
			Case "SCA10-1"
				'+ Si encuentra imagenes asociadas, actualiza Claim_win
				If lcolImagess.Find(nImagenum) Then
					lclsClaim = eRemoteDB.NetHelper.CreateClassInstance("eClaim.Claim_win")
					Call lclsClaim.Add_Claim_win(nClaim, sCodispl, "2", nUsercode)
				Else
					'+ Se actualiza el siniestro
					lclsClaim = eRemoteDB.NetHelper.CreateClassInstance("eClaim.Claim")
					With lclsClaim
						If .Find(nClaim) Then
							.nImagenum = numNull
							.nUsercode = nUsercode
							.Update()
						End If
						
						lclsClaim = eRemoteDB.NetHelper.CreateClassInstance("eClaim.Claim_win")
						Call lclsClaim.Add_Claim_win(nClaim, sCodispl, "1", nUsercode)
					End With
				End If
				
			Case "SCA10-2"
				'+ Si encuentra imagenes asociadas, actualiza Client_win
				If lcolImagess.Find(nImagenum) Then
					lclsClient = eRemoteDB.NetHelper.CreateClassInstance("eClient.ClientWin")
					lclsClient.insUpdClient_win(sClient, sCodispl, "2")
				Else
					'+ Se actualiza el cliente
					lclsClient = eRemoteDB.NetHelper.CreateClassInstance("eClient.ClientWin")
					lclsClient.insUpdClient_win(sClient, sCodispl, "1")
					
					lclsClient = eRemoteDB.NetHelper.CreateClassInstance("eClient.Client")
					With lclsClient
						.sClient = sClient
						.nUsercode = nUsercode
						Call .UpdateImageNum(numNull)
					End With
				End If
			Case "SCA593"
				'+ Si encuentra imagenes asociadas
				If lcolImagess.Find(nImagenum) Then
					'                Set lclsClient = eRemoteDB.NetHelper.CreateClassInstance("eClient.ClientWin")
					'                lclsClient.insUpdClient_win sClient, sCodispl, "2"
				Else
					'+ Se actualiza la orden de servicio
					'                Set lclsClaim = eRemoteDB.NetHelper.CreateClassInstance("eClaim.ClientWin")
					'                lclsClaim.insUpdClient_win sClient, sCodispl, "1"
					
					lclsClaim = eRemoteDB.NetHelper.CreateClassInstance("eClaim.Prof_ord")
					lclsClaim.Find_nServ(nServ_order)
					lblnResp = lclsClaim.InsPostOS590Upd(nServ_order, lclsClaim.dMade_date, lclsClaim.sPlace, lclsClaim.nMunicipality, lclsClaim.nStatus_ord, nUsercode, numNull, lclsClaim.nNotenum)
				End If
		End Select
		'UPGRADE_NOTE: Object lcolImagess may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lcolImagess = Nothing
		'UPGRADE_NOTE: Object lclsImages may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsImages = Nothing
		'UPGRADE_NOTE: Object lclsClient may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsClient = Nothing
		'UPGRADE_NOTE: Object lclsClaim may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsClaim = Nothing
		
insPostImages_Err: 
		If Err.Number Then
			insPostImages = False
		End If
		On Error GoTo 0
	End Function
	
	'% insValGE101: se realizan las validaciones de la página
	Public Function insValGE101(ByVal sProject As String, Optional ByVal nErrornum As Integer = 0) As String
		Dim lobjErrors As eFunctions.Errors
		
		On Error GoTo insValGE101_Err
		
		lobjErrors = New eFunctions.Errors
		
		Select Case sProject
			Case "ClientSeq"
				
				'+ Se envía la validación para indicar que faltan ventanas requeridas sin información
				
				Call lobjErrors.ErrorMessage("GE101", 3902)
				
			Case "PolicySeq"
				Call lobjErrors.ErrorMessage("CA001", nErrornum)
				
			Case "TransacSeq"
				Call lobjErrors.ErrorMessage("GE101", 3902)
				
            Case "AgentSeq"
                Call lobjErrors.ErrorMessage("AG550", 3902)

		End Select
		
		insValGE101 = lobjErrors.Confirm
		
		'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjErrors = Nothing
		
insValGE101_Err: 
		If Err.Number Then
			insValGE101 = insValGE101 & "insValGE101: " & Err.Description
		End If
		On Error GoTo 0
	End Function
	
	'%InitValues: Se inicializan los valores de las variables públicas de la clase
	Private Sub InitValues()
		nNotenum = numNull
		sClient = String.Empty
		nClaim = numNull
		nServ_order = numNull
		sCertype = String.Empty
		nBranch = numNull
		nProduct = numNull
		nPolicy = numNull
		nCertif = numNull
		nDeman_type = numNull
		nCase_num = numNull
		nClause = numNull
		dEffecdate = CDate(Nothing)
		nId = numNull
		sRegist = String.Empty
		sLicense_ty = String.Empty
	End Sub
	
	'%Class_Initialize: Se controla la creación del objeto de la clase
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		Call InitValues()
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
End Class






