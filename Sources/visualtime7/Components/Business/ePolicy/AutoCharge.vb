Option Strict Off
Option Explicit On
Public Class AutoCharge
	'%-------------------------------------------------------%'
	'% $Workfile:: AutoCharge.cls                           $%'
	'% $Author:: Nvaplat15                                  $%'
	'% $Date:: 8/10/04 12.43                                $%'
	'% $Revision:: 96                                       $%'
	'%-------------------------------------------------------%'
	
	'*Este nódulo de clase, contiene las funciones que se encargan de actualizar las
	'*Ventanas de la secuencia de póliza, de manera automática
	
	'+Se define la variable que contiene las monedas permitidas para la póliza
	Private mobjCurren_pol As Curren_pol
	
	'%AutoUpdCA014: Llena automaticamente la información de coberturas
    Private Function AutoUpdCA014(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nGroup As Integer, ByVal nCurrency As Integer, ByVal dEffecdate As Date, ByVal dNulldate As Date, ByVal nTransaction As Integer, ByVal sCodispl As String, ByVal nUsercode As Integer, ByVal sBrancht As String, ByVal sPolitype As String, ByVal nProdClas As Integer, ByVal nSessionId As String, ByVal nType_amend As Integer) As Boolean
        Dim lcolTCovers As TCovers
        Dim lclsTCover As TCover
        Dim lclsCover As Cover = New Cover
        Dim lclsRoles As Roles = New Roles
        Dim lstrError As String = String.Empty
        Dim lstrKey As String = String.Empty
        Dim lintCurrency As Integer
        Dim lstrCodisplAux As String = String.Empty
        Dim lstrClient As String = String.Empty
        Dim lblnUpdCover As Boolean

        lcolTCovers = New TCovers
        lstrCodisplAux = sCodispl
        lintCurrency = insLoadCurrency(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nCurrency)

        '+Se construye el key que se va a utilizar en los registros temporales
        lstrKey = lcolTCovers.sKey(nUsercode, nSessionId)

        '+Se llama a la funcion FindCoverPolicy, que se encarga de realizar el cálculo de las coberturas
        If lcolTCovers.FindCoverPolicy(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, lintCurrency, nGroup, sCodispl, nUsercode, dNulldate, nTransaction, eRemoteDB.Constants.intNull, String.Empty, sBrancht, nProdClas, lstrKey, "1", Nothing, , lblnUpdCover, nType_amend) Then


            If lblnUpdCover Then
                '+Por cada registro encontrado, se valida que los datos estén correctos
                For Each lclsTCover In lcolTCovers
                    lstrError = String.Empty

                    '+Sólo se toman en cuenta los registros seleccionados
                    If lclsTCover.nSel(lcolTCovers.bDataFound) = 1 Then
                        If lclsCover Is Nothing Then
                            lclsCover = New Cover
                            lclsCover.sCodispl = lstrCodisplAux
                        End If

                        If lclsRoles Is Nothing Then
                            lclsRoles = New Roles
                            With lclsRoles
                                If .Find(sCertype, nBranch, nProduct, nPolicy, nCertif, lclsTCover.nRole, lclsTCover.sClient, dEffecdate) Then

                                    If sBrancht = "1" Then
                                        lstrClient = String.Empty
                                        Call .CalInsuAge(nBranch, nProduct, dEffecdate, .dBirthdate, .sSexclien, .sSmoking)
                                    Else
                                        lstrClient = lclsRoles.SCLIENT
                                    End If
                                End If
                            End With
                        End If

                        With lclsTCover
                            lstrError = lclsCover.InsValCA014Upd(sCodispl, sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, .nModulec, .nCover, .nCapital, .nRatecove, .nPremium, .nCurrency, nGroup, nTransaction, .sFrandedi, .sFrancApl, .nRate, .nFixamount, .nMinamount, CInt(.sWait_type), .nCapital_o, .nRateCove_o, .nPremium_o, .nMaxamount, .nDiscount, .nDisc_Amoun, .nRole, sBrancht, .nWait_quan, lclsRoles.nAge, .nAgeminins, .nAgemaxins, .nAgemaxper, .sClient, .nCauseupd, nProdClas, lstrKey, .nAgemininsf, .nAgemaxinsf, .nAgemaxperf, .nBranch_rei, .nDurinsur, .nTypDurins, .sExist, .nRateCla, .nFixAmoCla, .nMinAmoCla, .nMaxamount, .nDiscCla, .nDisc_AmoCla, .nFrancDays)
                            If lstrError <> String.Empty Then
                                Exit For
                            End If
                        End With
                    End If
                Next lclsTCover

                '+Si no se registraron validaciones en la forma, se ejecuta el post de la forma CA014 para escribir las coberturas.
                If lstrError = String.Empty Then
                    If Not lclsCover Is Nothing Then
                        AutoUpdCA014 = lclsCover.InsPostCA014(lstrKey, sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nGroup, CInt(CStr(nTransaction)), dNulldate, eRemoteDB.Constants.intNull, lstrClient, sBrancht, nProdClas, nUsercode, sCodispl)
                    End If
                End If
            End If
        End If
        'UPGRADE_NOTE: Object lcolTCovers may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lcolTCovers = Nothing
        'UPGRADE_NOTE: Object lclsRoles may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsRoles = Nothing
        'UPGRADE_NOTE: Object lclsTCover may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsTCover = Nothing
        'UPGRADE_NOTE: Object lclsCover may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsCover = Nothing
    End Function

    '+ AutoUpdCA013: Actualiza de forma automática los registros requeridos por
    '+ la ventana CA013 (MÓDULOS DE LA PÓLIZA)
    Private Function AutoUpdCA013(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nGroup As Integer, ByVal dEffecdate As Date, ByVal dNulldate As Date, ByVal nCurrency As Integer, ByVal nSelect As Integer, ByVal nUsercode As Integer, ByVal sBrancht As String, Optional ByVal sTransaction As String = "1") As Boolean
        Dim lclsModules As ePolicy.Modules
        Dim lstrValidate As String = String.Empty
        Dim lintIndex As Integer
        Dim lstrSel As String = String.Empty
        Dim lstrModules As String = String.Empty

        lclsModules = New ePolicy.Modules
        With lclsModules
            If .InsPreCA013("CA013", sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nGroup, dNulldate, nUsercode, 0) Then
                For lintIndex = 0 To .CountModules
                    If .ModuleItem(lintIndex) Then
                        lstrSel = lstrSel & ", " & "1"
                        lstrModules = lstrModules & ", " & .nModulec
                    End If
                Next

                lstrSel = Mid(lstrSel, 3)
                lstrModules = Mid(lstrModules, 3)
                lstrValidate = .InsValCA013("CA013", sCertype, nBranch, nProduct, nPolicy, nCertif, lstrModules, lstrSel, .nCurrency, dEffecdate, .CountModules)

                If lstrValidate = String.Empty Then
                    AutoUpdCA013 = .InsPostCA013("CA013", sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nUsercode)

                End If
            End If
        End With
        'UPGRADE_NOTE: Object lclsModules may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsModules = Nothing
    End Function

    '%AutoUpdCA016: Actualiza de forma automática los registros requeridos po
    Private Function AutoUpdCA016(ByVal sCodispl As String, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal dNulldate As Date, ByVal nTransaction As Integer, ByVal nGroup As Integer, ByVal nUsercode As Integer) As Boolean
        Dim lcolDisc_xprem As Disc_xprems
        Dim lclsDisc_xprem As Disc_xprem
        Dim lclsValDisc_xprem As Disc_xprem = New Disc_xprem
        Dim lstrError As String = String.Empty
        Dim lstrSel As String = String.Empty
        Dim lstrCode As String = String.Empty
        Dim lstrExist As String = String.Empty
        Dim lstrCurrency As String = String.Empty
        Dim lstrAmount As String = String.Empty
        Dim lstrPercent As String = String.Empty
        Dim lstrCause As String = String.Empty
        Dim lstrAgree As String = String.Empty
        Dim lstrDiSexPri As String = String.Empty

        On Error GoTo AutoUpdCA016_Err
        lcolDisc_xprem = New Disc_xprems
        If lcolDisc_xprem.insPreCA016(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nGroup, nTransaction) Then

            '+Se realizan las validaciones puntuales de cada rec/desc/imp.
            For Each lclsDisc_xprem In lcolDisc_xprem
                With lclsDisc_xprem
                    If .sSel = "1" Then
                        If lclsValDisc_xprem Is Nothing Then
                            lclsValDisc_xprem = New Disc_xprem
                        End If
                        lstrError = lclsValDisc_xprem.InsValCA016upd(sCodispl, .sChanallo, .nPercent, .nAmount, .nOriPercent, .nOriAmount, .nDisexaddper, .nDisexsubper)
                        If lstrError <> String.Empty Then
                            Exit For
                        End If
                    End If
                    lstrSel = lstrSel & ", " & .sSel
                    lstrCode = lstrCode & ", " & .nDisc_code
                    lstrExist = lstrExist & ", " & .nExist
                    lstrCurrency = lstrCurrency & ", " & .nCurrency
                    lstrAmount = lstrAmount & ", " & .nAmount
                    lstrPercent = lstrPercent & ", " & .nPercent
                    lstrCause = lstrCause & ", " & .nCause
                    lstrAgree = lstrAgree & ", " & .sAgree
                    lstrDiSexPri = lstrDiSexPri & " , " & .sDisexpri
                End With
            Next lclsDisc_xprem

            '+Se realizan las validaciones masivas de los rec/desc/imp.
            lstrSel = Mid(lstrSel, 3)
            lstrCode = Mid(lstrCode, 3)
            lstrExist = Mid(lstrExist, 3)
            lstrCurrency = Mid(lstrCurrency, 3)
            lstrAmount = Mid(lstrAmount, 2)
            lstrPercent = Mid(lstrPercent, 2)
            lstrPercent = Mid(lstrPercent, 3)
            If lstrError = String.Empty Then
                If lclsValDisc_xprem Is Nothing Then
                    lclsValDisc_xprem = New Disc_xprem
                End If
                lstrError = lclsValDisc_xprem.InsValCA016(sCodispl, sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, lstrCode, lstrSel, lstrPercent, lstrDiSexPri)
            End If

            If lstrError = String.Empty Then
                If lclsValDisc_xprem Is Nothing Then
                    lclsValDisc_xprem = New Disc_xprem
                End If
                AutoUpdCA016 = lclsValDisc_xprem.InsPostCA016(sCodispl, lstrSel, lstrExist, sCertype, nBranch, nProduct, nPolicy, nGroup, nCertif, dEffecdate, lstrCurrency, dNulldate, lstrCode, lstrAmount, lstrPercent, lstrCause, lstrAgree, nUsercode, nTransaction)
            End If
        End If

AutoUpdCA016_Err:
        If Err.Number Then
            AutoUpdCA016 = False
        End If
        'UPGRADE_NOTE: Object lcolDisc_xprem may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lcolDisc_xprem = Nothing
        'UPGRADE_NOTE: Object lclsDisc_xprem may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsDisc_xprem = Nothing
        'UPGRADE_NOTE: Object lclsValDisc_xprem may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsValDisc_xprem = Nothing
        On Error GoTo 0
    End Function
	
	'%AutoUpdCA017: Actualiza de forma automática los registros requeridos po
	'%la ventana CA017 (Recibo de emisión) - ANDREW - 12/02/2001
	Private Function AutoUpdCA017(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal dNulldate As Date, ByVal nTransaction As Integer, ByVal nUsercode As Integer, ByVal sBrancht As String) As Boolean
		Dim mobjPremium As Object
		Dim lclsPolicyWin As Policy_Win
		
		On Error GoTo AutoUpdCA017_Err
		
		mobjPremium = eRemoteDB.NetHelper.CreateClassInstance("eCollection.Premium")
		
		If mobjPremium.InsPreCA017(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, dNulldate, nTransaction, nUsercode, sBrancht) Then
			
			AutoUpdCA017 = True
			
			lclsPolicyWin = New Policy_Win
			
			Call lclsPolicyWin.Add_PolicyWin(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nUsercode, "CA017", "2")
			
			'UPGRADE_NOTE: Object lclsPolicyWin may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			lclsPolicyWin = Nothing
			
		End If
		
AutoUpdCA017_Err: 
		If Err.Number Then
			AutoUpdCA017 = False
		End If
		On Error GoTo 0
		
		'UPGRADE_NOTE: Object mobjPremium may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mobjPremium = Nothing
		
	End Function
	
	'%insLoadCurrency. Esta funcion se encarga de buscar la moneda a utilizar para las ventanas automáticas
	Private Function insLoadCurrency(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, Optional ByVal nCurrency As Integer = 0) As Integer
		If nCurrency = eRemoteDB.Constants.intNull Or nCurrency = 0 Then
			
			insLoadCurrency = 0
			If mobjCurren_pol Is Nothing Then
				mobjCurren_pol = New Curren_pol
				Call mobjCurren_pol.Find(nPolicy, nBranch, nProduct, sCertype, nCertif, dEffecdate)
			End If
			If mobjCurren_pol.IsLocal Then
				insLoadCurrency = 1
			Else
				If mobjCurren_pol.Val_Curren_pol(0) Then
					insLoadCurrency = mobjCurren_pol.nCurrency
				End If
			End If
		End If
	End Function
	
	'%AutoUpdCA021: Actualiza de forma automática la distribución del Reaseguro
	Private Function AutoUpdCA021(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nUsercode As Integer, ByVal sBrancht As String, ByVal nTransaction As Short) As Boolean
		Dim lclsPolicy_Win As ePolicy.Policy_Win
		Dim lrecreaBranchr As eRemoteDB.Execute
		Dim sRequired As Object
		
		Call AutoDeleteCA021(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nTransaction, nUsercode)
		
		lrecreaBranchr = New eRemoteDB.Execute
		
		With lrecreaBranchr
			.StoredProcedure = "reaCoverBranchRGenLif"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCoinsushared", 100, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sBrancht", sBrancht, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			AutoUpdCA021 = .Run
			
			'Si no encuentra registros no se coloca requerida.
			sRequired = "4"
			
			If AutoUpdCA021 Then
				sRequired = "3" 'Si encuentra registros se coloca como requerida.
			End If
		End With
		
		'+Se actualiza la ventana como requerida y sin contenido.
		lclsPolicy_Win = New Policy_Win
		Call lclsPolicy_Win.Add_PolicyWin(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nUsercode, "CA021", sRequired)
		
		'UPGRADE_NOTE: Object lrecreaBranchr may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaBranchr = Nothing
		'UPGRADE_NOTE: Object lclsPolicy_Win may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsPolicy_Win = Nothing
	End Function
	
	'%AutoUpdCA021: Actualiza de forma automática la distribucion del Reaseguro
	Public Function AutoDeleteCA021(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nTransaction As Integer, ByVal nUsercode As Short) As Boolean
		Dim lclsReinsuran As Reinsuran
		Dim lclsPolicyWin As Policy_Win
		
		AutoDeleteCA021 = True
		
		lclsPolicyWin = New Policy_Win
		With lclsPolicyWin
			If .Find_Codispl(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, "CA021") Then
				If .Find_Sequen_Pol(CStr(nTransaction), sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, "") Then
					lclsReinsuran = New Reinsuran
					
					With lclsReinsuran
						.sCertype = sCertype
						.nBranch = nBranch
						.nProduct = nProduct
						.nPolicy = nPolicy
						.nCertif = nCertif
						.dEffecdate = dEffecdate
						.nUsercode = nUsercode
						.nTransaction = nTransaction
						
						Select Case nTransaction
							'+ Si la transacción en ejecucion es cotizacion, propuesta, emision, recuperacion de
							'+ poliza o certificado de una poliza colectiva o multilocalidad.
							Case 1, 2, 3, 4, 5, 6, 7, 28, 29, 30, 31
								AutoDeleteCA021 = .Delete(True)
								'+ Si la transacción es cotización de modificacion, propuesta de modificacion, modificacion,
							Case 13, 15, 24, 25, 26, 27
								AutoDeleteCA021 = .Update(True)
						End Select
					End With
					
					'UPGRADE_NOTE: Object lclsReinsuran may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					lclsReinsuran = Nothing
					If .sContent = "2" Then
						Call lclsPolicyWin.Add_PolicyWin(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nUsercode, "CA021", "1")
					End If
				End If
			End If
		End With
		'UPGRADE_NOTE: Object lclsPolicyWin may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsPolicyWin = Nothing
	End Function
	
	'% AutoUpdVA595: Actualiza automáticamente la pagina VA595, Ilustración de VidActiva
	Private Function AutoUpdVA595(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal dNulldate As Date, ByVal nTransaction As Integer, ByVal nUsercode As Integer, ByVal nSessionId As String) As Boolean
		Dim insValSequence As String
		Dim lclsActivelife As Activelife
		Dim lclsProjectlife As Projectlife
		
		lclsActivelife = New Activelife
		lclsProjectlife = New Projectlife
		
		insValSequence = String.Empty
		AutoUpdVA595 = False
		
		'+ Se ejecuta el pre de pagina VA595
		Call lclsActivelife.InsPreVA595(String.Empty, sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, 0, nUsercode)
		
		'+ Se ejecuta la secuencia de validaciones.
		insValSequence = lclsProjectlife.InsValVA595("VA595", nBranch, nProduct, dEffecdate, lclsActivelife.nPremfreq, lclsActivelife.nPremDeal, lclsActivelife.nPremimin, lclsActivelife.nPremiumbas, lclsActivelife.nAmountcontr, lclsActivelife.nPrsugest, nTransaction)
		
		'+ Si no existen errores, se procede a actualizar datos.
		If insValSequence = String.Empty Then
			AutoUpdVA595 = lclsProjectlife.InsPostVA595(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, dNulldate, nUsercode, nSessionId, nTransaction, lclsActivelife.nPremiumbas, lclsActivelife.nPremDeal, lclsActivelife.nVPprdeal, lclsActivelife.nPrsugest, lclsActivelife.nVPprsug, lclsActivelife.sInsCalPre, "2", "2", "2", lclsActivelife.nInsurtime)
		End If
		
		'UPGRADE_NOTE: Object lclsActivelife may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsActivelife = Nothing
		'UPGRADE_NOTE: Object lclsProjectlife may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsProjectlife = Nothing
	End Function
	
	'% AutoUpdVI021: Actualiza automáticamente la pagina VI021, Documentos requeridos
	Private Function AutoUpdVI021(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal dNulldate As Date, ByVal nSessionId As String, ByVal nUsercode As Integer, ByVal nTransaction As Integer) As Boolean
		Dim lclsLife_docu As Life_docu
		Dim lclsLife_docus As Life_docu
		
		On Error GoTo AutoUpdVI021_Err
		AutoUpdVI021 = True
		lclsLife_docu = New Life_docu
		'+Se obtiene los datos de la transacción
		lclsLife_docu.sDel_docu = "1"
		If lclsLife_docu.InsPreVI021(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nSessionId, nUsercode, nTransaction, String.Empty) Then
			
			If lclsLife_docu.nErrorNum <= 0 Then
				'+Se realiza la validación de los registros
				For	Each lclsLife_docus In lclsLife_docu.mcolLife_docus
					If lclsLife_docu.InsValVI021Upd("VI021", lclsLife_docus.nStat_docReq, lclsLife_docus.dRecep_date, lclsLife_docus.dDate_to, lclsLife_docus.dDatevig, lclsLife_docus.dDatefree) > String.Empty Then
						
						AutoUpdVI021 = False
						Exit For
					End If
				Next lclsLife_docus
				
				'+Se realiza la actualización de los registros
				If AutoUpdVI021 Then
					AutoUpdVI021 = lclsLife_docu.InsPostVI021(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, dNulldate, nUsercode, nTransaction, lclsLife_docu.sKey, lclsLife_docu.nEval, lclsLife_docu.nStatus_eval)
					
				End If
			End If
		End If
		'UPGRADE_NOTE: Object lclsLife_docu may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsLife_docu = Nothing
		
AutoUpdVI021_Err: 
		If Err.Number Then
			AutoUpdVI021 = False
		End If
	End Function
	
	'% AutoUpdCodispl: Realiza la actualización automática de la transacción indicada
	Private Function AutoUpdCodispl(ByVal sCodispl As String, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nGroup As Integer, ByVal sPolitype As String, ByVal dEffecdate As Date, ByVal dNulldate As Date, ByVal nTransaction As Integer, ByVal nUsercode As Integer, ByVal sBrancht As String, ByVal nSessionId As String, ByVal sBussityp As String, ByVal nType_amend As Integer) As Boolean
		Dim lclsProduct As eProduct.Product
		Dim lblnPost As Boolean
		
		On Error GoTo AutoUpdCodispl_Err
		Select Case sCodispl
			Case "CA013"
				lblnPost = AutoUpdCA013(sCertype, nBranch, nProduct, nPolicy, nCertif, nGroup, dEffecdate, dNulldate, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, nUsercode, sBrancht)
				
			Case "CA014"
				lclsProduct = New eProduct.Product
				If sBrancht = CStr(eProduct.Product.pmBrancht.pmlife) Or sBrancht = CStr(eProduct.Product.pmBrancht.pmNotTraditionalLife) Then
					lclsProduct.FindProduct_li(nBranch, nProduct, dEffecdate)
				End If
				lblnPost = AutoUpdCA014(sCertype, nBranch, nProduct, nPolicy, nCertif, nGroup, eRemoteDB.Constants.intNull, dEffecdate, dNulldate, nTransaction, "CA014", nUsercode, sBrancht, sPolitype, lclsProduct.nProdClas, nSessionId, nType_amend)
				
			Case "CA016"
				lblnPost = AutoUpdCA016("CA016", sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, dNulldate, nTransaction, nGroup, nUsercode)
				
			Case "CA017"
				lblnPost = AutoUpdCA017(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, dNulldate, nTransaction, nUsercode, sBrancht)
				
			Case "CA021"
				lblnPost = AutoUpdCA021(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nUsercode, sBrancht, nTransaction)
				
			Case "VA595"
				lblnPost = AutoUpdVA595(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, dNulldate, nTransaction, nUsercode, nSessionId)
				
			Case "VI021"
				lblnPost = AutoUpdVI021(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, dNulldate, nSessionId, nUsercode, nTransaction)
		End Select
		
		AutoUpdCodispl = lblnPost
AutoUpdCodispl_Err: 
		If Err.Number Then
			AutoUpdCodispl = True
		End If
		'UPGRADE_NOTE: Object lclsProduct may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsProduct = Nothing
		On Error GoTo 0
	End Function
	
	'% AutoUpdGeneral: Realiza la actualización automática de las transacciones de póliza
	Public Function AutoUpdGeneral(ByVal sCodispl As String, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nGroup As Integer, ByVal sPolitype As String, ByVal dEffecdate As Date, ByVal dNulldate As Date, ByVal nTransaction As Integer, ByVal nUsercode As Integer, ByVal sBrancht As String, ByVal nSessionId As String, ByVal sBussityp As String, ByVal nType_amend As Integer) As Boolean
		Dim lclsPolicy_Win As ePolicy.Policy_Win
		Dim lstrCodisplAll As String
		Dim lblnPost As Boolean
		Dim llngCountItem As Integer
		Dim llngIndex As Integer
		
		lclsPolicy_Win = New ePolicy.Policy_Win
		AutoUpdGeneral = True
		lstrCodisplAll = lclsPolicy_Win.getCodisplArr(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate)
		If lstrCodisplAll > String.Empty Then
			If lclsPolicy_Win.InsGetAutomaticWindows(sCodispl, CStr(nTransaction), nBranch, nProduct, dEffecdate, sPolitype, sBussityp) Then
				'+ Se tratan cada una de las ventanas
				llngCountItem = lclsPolicy_Win.CountItem
				For llngIndex = 0 To llngCountItem
					If lclsPolicy_Win.Item(llngIndex) Then
						If lstrCodisplAll Like "*|" & lclsPolicy_Win.sCodispl & "|*" Then
							lblnPost = True
							'+Se valida que todas las subcarpetas de coberturas tengan contenido
							If sCodispl = "CA014" Then
								lblnPost = UpdAutoCA014(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, sBrancht, sPolitype, lclsPolicy_Win)
							End If
							If lblnPost Then
								If lclsPolicy_Win.sAutomatic = "1" Then
									lblnPost = AutoUpdCodispl(lclsPolicy_Win.sCodispl, sCertype, nBranch, nProduct, nPolicy, nCertif, nGroup, sPolitype, dEffecdate, dNulldate, nTransaction, nUsercode, sBrancht, nSessionId, sBussityp, nType_amend)
								Else
									If lclsPolicy_Win.sRequire = "1" Then
										lblnPost = False
									End If
								End If
							End If
							If Not lblnPost Then
								Exit For
							End If
						End If
					End If
				Next 
			End If
		End If
		'UPGRADE_NOTE: Object lclsPolicy_Win may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsPolicy_Win = Nothing
	End Function
	
	'%UpdAutoCA014: Verifica si todas las subcarpetas de la CA014 tienen contenido para ejecutar
	'%              la actualización automática de las transacciones dependientes
	Private Function UpdAutoCA014(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal sBrancht As String, ByVal sPolitype As String, ByRef lclsPolicy_Win As ePolicy.Policy_Win) As Boolean
		Dim lblnUpd As Boolean
		Dim lintTop As Integer
		Dim lintIndex As Integer
		Dim lintCount As Integer
		Dim lstrV_conpolic As String
		Dim lstrV_winpolic As String
		Dim lstrCodispl As String
		Dim lstrContent As String
		Dim lblnLife As Boolean
		Dim lblnDestroy As Boolean
		
		On Error GoTo UpdAutoCA014_Err
		If (sBrancht = CStr(eProduct.Product.pmBrancht.pmlife) Or sBrancht = CStr(eProduct.Product.pmBrancht.pmNotTraditionalLife)) And (sPolitype = "1" Or (sPolitype = "2" And nCertif > 0)) Then
			lblnLife = True
		End If
		
		If lclsPolicy_Win Is Nothing Then
			lblnDestroy = True
			lclsPolicy_Win = New Policy_Win
		End If
		If lclsPolicy_Win.Find(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate) Then
			
			lstrV_conpolic = lclsPolicy_Win.sV_conpolic
			lstrV_winpolic = lclsPolicy_Win.sV_winpolic
			lintTop = Len(Trim(lstrV_conpolic)) - 1
			lintIndex = 1
			lblnUpd = True
			'+ los posibles valores de sContent son:
			'+      1 -  Sin Contenido
			'+      2 -  Con Contenido
			'+      3 -  Sin Contenido y Requerida para la poliza/certificado
			'+      4 -  Sin Contenido y No requerida para la poliza/certificado
			'+      5 -  Con Contenido y Requerida para la poliza/certificado
			'+      6 -  Con Contenido y No requerida para la poliza/certificado
			For lintCount = 0 To lintTop
				lstrCodispl = Trim(Mid(lstrV_winpolic, lintCount * 8 + 1, 8))
				If InStr(1, lstrCodispl, "CA014", CompareMethod.Text) > 0 Then
					If lstrCodispl = "CA014" And lblnLife Then
						lstrContent = "2"
					Else
						lstrContent = Mid(lstrV_conpolic, lintCount + 1, 1)
					End If
					If lstrContent = "1" Or lstrContent = "3" Or lstrContent = "4" Then
						lblnUpd = False
						Exit For
					End If
				End If
			Next lintCount
		End If
		
		UpdAutoCA014 = lblnUpd
		
UpdAutoCA014_Err: 
		If Err.Number Then
			UpdAutoCA014 = False
		End If
		If lblnDestroy Then
			'UPGRADE_NOTE: Object lclsPolicy_Win may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			lclsPolicy_Win = Nothing
		End If
		On Error GoTo 0
	End Function
	
	'%Class_Terminate: Se destruye la clase
	'UPGRADE_NOTE: Class_Terminate was upgraded to Class_Terminate_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Terminate_Renamed()
		'UPGRADE_NOTE: Object mobjCurren_pol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mobjCurren_pol = Nothing
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
	
	'% InsAutoUpdGeneral: Realiza la actualización automática de las transacciones de póliza
	Public Function InsAutoUpdGeneral(ByVal sCodispl As String, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nGroup As Integer, ByVal sPolitype As String, ByVal dEffecdate As Date, ByVal dNulldate As Date, ByVal nTransaction As Integer, ByVal nUsercode As Integer, ByVal sBrancht As String, ByVal nSessionId As String, ByVal sBussityp As String, ByVal nType_amend As Integer) As Boolean
        Dim lrecExecute As eRemoteDB.Execute
        '+ Definición de store procedure insPostca013a al 07-27-2002 16:29:40
		On Error GoTo InsAutoUpdGeneral_Err
		lrecExecute = New eRemoteDB.Execute
		With lrecExecute
			.StoredProcedure = "insAutoUpdGeneralPKG.AutoUpdGeneral"
			.Parameters.Add("sCodispl", sCodispl, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nGroup", nGroup, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sPolitype", sPolitype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dNulldate", dNulldate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTransaction", nTransaction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sBrancht", sBrancht, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nSessionId", eRemoteDB.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sBussityp", sBussityp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nType_amend", nType_amend, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			InsAutoUpdGeneral = .Run(False)
		End With
		
InsAutoUpdGeneral_Err: 
		If Err.Number Then
			InsAutoUpdGeneral = False
		End If
		'UPGRADE_NOTE: Object lrecExecute may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecExecute = Nothing
	End Function
End Class






