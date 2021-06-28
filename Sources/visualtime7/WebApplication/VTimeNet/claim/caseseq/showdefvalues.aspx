<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eBranches" %>
<%@ Import namespace="eClaim" %>
<%@ Import namespace="eClient" %>
<%@ Import namespace="ePolicy" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 11.33.46
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility
Dim mobjValues As eFunctions.Values
Private C_MESSAGE_55983 As String = New eGeneral.GeneralFunction().insLoadMessage(55983)


'% ShowAutoDetail: busca los datos asociados a un auto
'--------------------------------------------------------------------------------------------
Private Sub ShowAutoDetail()
	'--------------------------------------------------------------------------------------------
	Dim lobjAuto_bd As eBranches.Auto_db
	Dim lstrStatus As String
	
	lobjAuto_bd = New eBranches.Auto_db
	
	With lobjAuto_bd
		If .Find_AutoDB_Exists(Request.QueryString("nType"), Request.QueryString("sLicense"), Request.QueryString("sRegister")) Then
			
			Response.Write("with(opener.document.forms[0]){" & "tctRegister.value='" & .sRegist & "';" & "tctChassis.value='" & .sChassis & "';" & "tctMotor.value='" & .sMotor & "';" & "if(tctColor.value==""""){" & "tctColor.value='" & .sColor & "';" & "}" & "if(valVehCode.value==""""){" & "valVehCode.value='" & .sVehcode & "';" & "opener.UpdateDiv(""lblMarkVeh"",'" & .sDesMark & "','Normal');" & "opener.UpdateDiv(""lblModelVeh"",'" & .sVehModel & "','Normal');" & "}")
			
			If .nVestatus = 1 Then
				lstrStatus = "true"
			Else
				lstrStatus = "false"
			End If
			
			Response.Write("opener.document.forms[0].tctDigit.disabled=true;")
			If .sChassis = "" Then
				Response.Write("opener.document.forms[0].tctChassis.disabled=false;")
			Else
				Response.Write("opener.document.forms[0].tctChassis.disabled=true;")
			End If
			
			If .sMotor = "" Then
				Response.Write("opener.document.forms[0].tctMotor.disabled=false;")
			Else
				Response.Write("opener.document.forms[0].tctMotor.disabled=true;")
			End If
			
			If .sColor = "" Then
				Response.Write("opener.document.forms[0].tctColor.disabled=false;")
			Else
				Response.Write("opener.document.forms[0].tctColor.disabled=true;")
			End If
			
			'			If .sVehcode = "" Then
			'			   Response.Write "opener.document.forms[0].valVehCode.disabled=false;"
			'			   Response.Write "opener.document.forms[0].btnvalVehCode.disabled=false;"
			'			Else   
			'			   Response.Write "opener.document.forms[0].valVehCode.disabled=true;"
			'			   Response.Write "opener.document.forms[0].btnvalVehCode.disabled=true;"
			'			End If
			
			
			Response.Write("}")
		Else
			Response.Write("opener.document.forms[0].tctChassis.disabled=false;" & "opener.document.forms[0].tctMotor.disabled=false;" & "opener.document.forms[0].tctColor.disabled=false;" & "opener.document.forms[0].valVehCode.disabled=false;" & "opener.document.forms[0].tctDigit.disabled=false;" & "opener.document.forms[0].btnvalVehCode.disabled=false;")
		End If
	End With
	
	'UPGRADE_NOTE: Object lobjAuto_bd may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
	lobjAuto_bd = Nothing
End Sub

'% ShowAmount: calcula el importe de indemnización
'%			   utilizado en el campo "Tipo de siniestro" de la página SI024
'--------------------------------------------------------------------------------------------
Private Sub ShowAmount()
	'--------------------------------------------------------------------------------------------
	Dim lclsLife_claim As eClaim.Life_claim
	Dim lclsProduct_li As eProduct.Product
        
	Dim ldblIndemnity As String
    Dim ldblAmountAdjustCapital As String 
	Dim lintIndAdjustCapital As String 
        
	lclsLife_claim = New eClaim.Life_claim
    lclsProduct_li = New eProduct.Product        
	
	ldblIndemnity = Request.QueryString("gmnIndemn")
    ldblAmountAdjustCapital = Request.QueryString("nAmountAdjustCapital")
    lintIndAdjustCapital = Request.QueryString("IndAdjustCapital")
	
	With lclsLife_claim
		.nGrowth_RateI = mobjValues.StringToType(Request.QueryString("nGRI"), eFunctions.Values.eTypeData.etdDouble)
		.nGrowth_RateE = mobjValues.StringToType(Request.QueryString("nGRE"), eFunctions.Values.eTypeData.etdDouble)
            
		If .CalAmount(CDbl(Session("nClaim")), CInt(Session("nCase_num")), CInt(Session("nDeman_type")), Request.QueryString("Cla_li_typ"), mobjValues.StringToType(ldblIndemnity, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(lintIndAdjustCapital, eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(ldblAmountAdjustCapital, eFunctions.Values.eTypeData.etdDouble)) Then
			
			With Response
				
				.Write("top.fraFolder.document.forms[0].gmnAdv_paymen.value='" & mobjValues.TypeToString(lclsLife_claim.nAdv_paymen, eFunctions.Values.eTypeData.etdDouble, True, 6) & "';")
				
				.Write("top.fraFolder.document.forms[0].gmnSalvage.value='" & mobjValues.TypeToString(lclsLife_claim.nSalvage, eFunctions.Values.eTypeData.etdDouble, True, 6) & "';")
				.Write("top.fraFolder.document.forms[0].gmnCapital.value='" & mobjValues.TypeToString(lclsLife_claim.nCapital, eFunctions.Values.eTypeData.etdDouble, True, 6) & "';")
				
				.Write("top.fraFolder.document.forms[0].gmnIndemn.value='" & mobjValues.TypeToString(lclsLife_claim.nIndemnity, eFunctions.Values.eTypeData.etdDouble, True, 6) & "';")
				.Write("top.fraFolder.document.forms[0].cbeCurrency.value='" & mobjValues.TypeToString(lclsLife_claim.nCurrency, eFunctions.Values.eTypeData.etdDouble) & "';")
				
				If lclsLife_claim.nIndemnity <> 0 Then
					'.Write "top.fraFolder.document.forms[0].gmnIndemn.value='" & mobjValues.TypeToString(lclsLife_claim.nIndemnity - lclsLife_claim.nAdv_paymen, eFunctions.Values.eTypeData.etdDouble, True, 6) & "';"
					.Write("top.fraFolder.document.forms[0].gmnIndemn.value='" & mobjValues.TypeToString(lclsLife_claim.nIndemnity, eFunctions.Values.eTypeData.etdDouble, True, 6) & "';")
                        
                    Call lclsProduct_li.FindProduct_li(CInt(Session("nBranch")), CInt(Session("nProduct")), Today, True)
                        
                    If lclsProduct_li.sApv = "1" Then
					    .Write("top.fraFolder.document.forms[0].gmnIndemn.disabled = true;")
                    Else
                        .Write("top.fraFolder.document.forms[0].gmnIndemn.disabled = false;")    
                    End If 
                        
				End If
				.Write("top.fraFolder.insCalAPVCapital(" & lclsLife_claim.nCapital & ");")
				
			End With
		End If
	End With
	'UPGRADE_NOTE: Object lclsLife_claim may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
	lclsLife_claim = Nothing
End Sub

'% ShowClient: muestra el cliente asociado al código del proveedor.
'%			   utilizado en los campos "Médico tratante" y "Clínica" de la página SI028
'--------------------------------------------------------------------------------------------
Private Sub ShowClient()
	'--------------------------------------------------------------------------------------------
	Dim lclsTab_Provider As eClaim.Tab_Provider
	Dim lclsClient As eClient.Client
	Dim lstrClient As String
	
	lclsClient = New eClient.Client
	lclsTab_Provider = New eClaim.Tab_Provider
	
	lstrClient = lclsClient.ExpandCode(Request.QueryString("sClient"))
	
	With Response
		If lclsTab_Provider.FindProviderByCode(Request.QueryString("nProvider"), Request.QueryString("nTypeProv"), lstrClient) Then
			
			
			If Request.QueryString("ShowClient") Then
				.Write("opener.document.forms[0]." & Request.QueryString("sFieldName") & ".value='" & lclsTab_Provider.sClient & "';")
				If Request.QueryString("sFieldName") <> "valClinic" And Request.QueryString("sFieldName") <> "valProf" Then
					.Write("opener.document.forms[0]." & Request.QueryString("sFieldName") & "_Digit" & ".value='" & lclsTab_Provider.sDigit & "';")
				End If
			Else
				
				.Write("opener.document.forms[0]." & Request.QueryString("sFieldName") & ".value=" & lclsTab_Provider.nProvider & ";")
				If Request.QueryString("sFieldName") <> "valClinic" And Request.QueryString("sFieldName") <> "valProf" Then
					.Write("opener.document.forms[0]." & Request.QueryString("sFieldName") & "_Digit" & ".value='" & lclsTab_Provider.sDigit & "';")
				End If
			End If
			
			'.Write("opener.document.forms[0].dtcClientProf_Digit" & ".value='" & lclsTab_Provider.sDigit & "';")
			'.Write("opener.$('#dtcClientProf_Digit').change();")
			.Write("opener.$('#" & Request.QueryString("sFieldName") & "').change();")
			
			If Request.QueryString("sFieldName") <> "valClinic" And Request.QueryString("sFieldName") <> "valProf" Then
				
				.Write("opener.document.forms[0]." & Request.QueryString("sFieldName") & "_Digit" & ".value='" & lclsTab_Provider.sDigit & "';")
				.Write("opener.$('#" & Request.QueryString("sFieldName") & "_Digit')" & ".change();")
			End If
		Else
			
			.Write("opener.document.forms[0]." & Request.QueryString("sFieldName") & ".value='';")
			.Write("opener.$('#" & Request.QueryString("sFieldName") & "').change();")
		End If
		
		
		If lclsTab_Provider.sClient <> "" Then
			lstrClient = lclsClient.ExpandCode(lclsTab_Provider.sClient)
		Else
			lstrClient = lclsClient.ExpandCode(Request.QueryString("sClient"))
		End If
		
		If lclsClient.Find(lstrClient) Then
			.Write("opener.document.forms[0].tctLastNameProf.value='" & Replace(lclsClient.sLastname, "'", "´") & "';")
			.Write("opener.document.forms[0].tctLastName2Prof.value='" & Replace(lclsClient.sLastName2, "'", "´") & "';")
			.Write("opener.document.forms[0].tctFirstNameProf.value='" & Replace(lclsClient.sFirstName, "'", "´") & "';")
			.Write("opener.document.forms[0].tctLastNameProf.disabled=true;")
			.Write("opener.document.forms[0].tctLastName2Prof.disabled=true;")
			.Write("opener.document.forms[0].tctFirstNameProf.disabled=true;")
		Else
			.Write("opener.document.forms[0].tctLastNameProf.value='';")
			.Write("opener.document.forms[0].tctLastName2Prof.value='';")
			.Write("opener.document.forms[0].tctFirstNameProf.value='';")
			.Write("opener.document.forms[0].tctLastNameProf.disabled=false;")
			.Write("opener.document.forms[0].tctLastName2Prof.disabled=false;")
			.Write("opener.document.forms[0].tctFirstNameProf.disabled=false;")
		End If
	End With
	
	'UPGRADE_NOTE: Object lclsTab_Provider may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
	lclsTab_Provider = Nothing
	'UPGRADE_NOTE: Object lclsClient may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
	lclsClient = Nothing
	
End Sub

'% CheckCreClient: Verifica si el cliente se encuentra previamente registrado para recuperar
'%                 sus datos; en caso contrario es generado automáticamente.
'--------------------------------------------------------------------------------------------
Private Sub CheckCreClient()
	'--------------------------------------------------------------------------------------------
	Dim lclsClient As eClient.Client
	Dim lstrClient As String
	lclsClient = New eClient.Client
	
	lstrClient = lclsClient.ExpandCode(Request.QueryString("sClient"))
	
	With Response
		.Write("top.fraFolder.document.forms[0].tctFatherLastName.value='';")
		.Write("top.fraFolder.document.forms[0].tctMotherLastName.value='';")
		.Write("top.fraFolder.document.forms[0].tctNames.value='';")
            .Write("top.fraFolder.document.forms[0].dtcBirthdayDate.value='';")
		.Write("top.fraFolder.document.forms[0].tctLicense.value='';")
            .Write("top.fraFolder.document.forms[0].tcdDriverDate.value='';")
		
		If Not lclsClient.Find(lstrClient) Then
			.Write("top.fraFolder.document.forms[0].tctFatherLastName.disabled=false;")
			.Write("top.fraFolder.document.forms[0].tctMotherLastName.disabled=false;")
			.Write("top.fraFolder.document.forms[0].tctNames.disabled=false;")
			.Write("top.fraFolder.document.forms[0].dtcBirthdayDate.disabled=false;")
                .Write("top.fraFolder.document.forms[0].tctLicense.disabled=false;")
                .Write("top.fraFolder.document.forms[0].btn_dtcBirthdayDate.disabled=false;")
			.Write("top.fraFolder.document.forms[0].btn_tcdDriverDate.disabled=false;")
			.Write("top.fraFolder.document.forms[0].tcdDriverDate.disabled=false;")
		Else
			.Write("top.fraFolder.document.forms[0].tctFatherLastName.disabled=true;")
			.Write("top.fraFolder.document.forms[0].tctMotherLastName.disabled=true;")
			.Write("top.fraFolder.document.forms[0].tctNames.disabled=true;")
			.Write("top.fraFolder.document.forms[0].dtcBirthdayDate.disabled=true;")
			
			.Write("top.fraFolder.document.forms[0].tctFatherLastName.value='" & Replace(lclsClient.sLastname, "'", "´") & "';")
			.Write("top.fraFolder.document.forms[0].tctMotherLastName.value='" & Replace(lclsClient.sLastName2, "'", "´") & "';")
			.Write("top.fraFolder.document.forms[0].tctNames.value='" & Replace(lclsClient.sFirstName, "'", "´") & "';")
			.Write("top.fraFolder.document.forms[0].dtcBirthdayDate.value='" & mobjValues.TypeToString(lclsClient.dBirthdat, eFunctions.Values.eTypeData.etdDate) & "';")
			.Write("top.fraFolder.document.forms[0].btn_dtcBirthdayDate.disabled=true;")
			'+Manejo para la informacion de la licencia
			If lclsClient.sLicense <> "" Then
				.Write("top.fraFolder.document.forms[0].tctLicense.disabled=true;")
				.Write("top.fraFolder.document.forms[0].tctLicense.value='" & lclsClient.sLicense & "';")
			Else
				.Write("top.fraFolder.document.forms[0].tctLicense.disabled=false;")
				.Write("top.fraFolder.document.forms[0].tcdDriverDate.disabled=false;")
			End If
			
			If lclsClient.dDriverdat <> eRemoteDB.Constants.dtmNull Then
				.Write("top.fraFolder.document.forms[0].tcdDriverDate.disabled=true;")
				.Write("top.fraFolder.document.forms[0].tcdDriverDate.value='" & mobjValues.TypeToString(lclsClient.dDriverdat, eFunctions.Values.eTypeData.etdDate) & "';")
			Else
				.Write("top.fraFolder.document.forms[0].btn_tcdDriverDate.disabled=false;")
			End If
		End If
	End With
	
	'UPGRADE_NOTE: Object lclsClient may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
	lclsClient = Nothing
	
End Sub

'% CheckCreWitness: Verifica si el testigo se encuentra previamente registrado para recuperar
'%                  sus datos; en caso contrario es generado automáticamente.
'--------------------------------------------------------------------------------------------
Private Sub CheckCreWitness()
	'--------------------------------------------------------------------------------------------
	Dim lclsClient As eClient.Client
	lclsClient = New eClient.Client
	
	With Response
		.Write("top.fraFolder.document.forms[0].tctFatherLastNameWitness.value='';")
		.Write("top.fraFolder.document.forms[0].tctMotherLastNameWitness.value='';")
		.Write("top.fraFolder.document.forms[0].tctNamesWitness.value='';")
		
		If lclsClient.Find(Request.QueryString("sClient")) Then
			.Write("top.fraFolder.document.forms[0].tctFatherLastNameWitness.disabled=true;")
			.Write("top.fraFolder.document.forms[0].tctMotherLastNameWitness.disabled=true;")
			.Write("top.fraFolder.document.forms[0].tctNamesWitness.disabled=true;")
			
			.Write("top.fraFolder.document.forms[0].tctFatherLastNameWitness.value='" & Replace(lclsClient.sLastname, "'", "´") & "';")
			.Write("top.fraFolder.document.forms[0].tctMotherLastNameWitness.value='" & Replace(lclsClient.sLastName2, "'", "´") & "';")
			.Write("top.fraFolder.document.forms[0].tctNamesWitness.value='" & Replace(lclsClient.sFirstName, "'", "´") & "';")
		Else
			.Write("top.fraFolder.document.forms[0].tctFatherLastNameWitness.disabled=false;")
			.Write("top.fraFolder.document.forms[0].tctMotherLastNameWitness.disabled=false;")
			.Write("top.fraFolder.document.forms[0].tctNamesWitness.disabled=false;")
		End If
	End With
	
	'UPGRADE_NOTE: Object lclsClient may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
	lclsClient = Nothing
	
End Sub

'% FindDateIndem: Busca la fecha hasta la cual el beneficiario recibe pensión
'--------------------------------------------------------------------------------------------
Private Sub FindDateIndem()
	'--------------------------------------------------------------------------------------------
	Dim lclsLifeClaim As eClaim.Life_claim
	lclsLifeClaim = New eClaim.Life_claim

	With Response
		If lclsLifeClaim.CalculateDateIndem("2", CInt(Session("nBranch")), CInt(Session("nProduct")), CInt(Session("nPolicy")), CInt(Session("nCertif")), CDate(Session("dEffecdate"))) Then
			.Write("top.fraFolder.document.forms[0].gmdEnd_date.value='" & mobjValues.TypeToString(lclsLifeClaim.dDateIndem, eFunctions.Values.eTypeData.etdDate) & "';")
			.Write("top.fraFolder.document.forms[0].gmdEnd_date.disabled=false;")
		End If
	End With
	'UPGRADE_NOTE: Object lclsLifeClaim may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
	lclsLifeClaim = Nothing
End Sub

'% insShowDigit: Muestra el digito verificador de la patente ingresada
'--------------------------------------------------------------------------------------------
Sub insShowDigit()
	'--------------------------------------------------------------------------------------------
	Dim lclsAuto As ePolicy.Automobile
	
	lclsAuto = New ePolicy.Automobile
	
	If Request.QueryString("sLicense_ty") = "1" Then
		With Response
			If lclsAuto.InsCalDigitSerie(Request.QueryString("sRegist")) Then
				.Write("top.frames['fraFolder'].document.forms[0].tctDigit.value=""" & Trim(lclsAuto.sDigit) & """;")
			Else
				.Write("top.frames['fraFolder'].document.forms[0].tctDigit.value=""" & """;")
                .Write("alert(""Err 55983: " & C_MESSAGE_55983 & """);")
			End If
		End With
	End If
	'UPGRADE_NOTE: Object lclsAuto may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
	lclsAuto = Nothing
End Sub

'% insShowAuto: se muestran los datos asociados al auto seleccionado
'%              Se utiliza para el campo Código del vehiculo de la página AU001.aspx
'--------------------------------------------------------------------------------------------
Sub insShowAuto()
	'--------------------------------------------------------------------------------------------
	Dim lclsAuto As ePolicy.Automobile
	Dim lclsPolicyWin As Object
	
	lclsAuto = New ePolicy.Automobile
	If Request.QueryString("sVehcode") <> "" Then
		If lclsAuto.Find_Tab_au_veh(Request.QueryString("sVehcode")) Then
			With Response
				' Actualiza la marca del vehículo : Obtiene la marca
				.Write("top.frames['fraFolder'].UpdateDiv(""lblMarkVeh"",'" & Trim(lclsAuto.sDesBrand) & "','Normal');")
				' Actualiza el modelo del vehículo : Obtiene el modelo
				.Write("top.frames['fraFolder'].UpdateDiv(""lblModelVeh"",'" & Trim(lclsAuto.sVehmodel1) & "','Normal');")
			End With
		Else
			With Response
				' Actualiza la marca del vehículo : Obtiene la marca
				.Write("top.frames['fraFolder'].UpdateDiv(""lblMarkVeh"",'" & "" & "','Normal');")
				' Actualiza el modelo del vehículo : Obtiene el modelo
				.Write("top.frames['fraFolder'].UpdateDiv(""lblModelVeh"",'" & "" & "','Normal');")
			End With
		End If
	End If
	'UPGRADE_NOTE: Object lclsAuto may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
	lclsAuto = Nothing
End Sub

'% insCalIndemnity: calculo de la indemnizacion para siniestros de producto universitario
'--------------------------------------------------------------------------------------------
Sub insCalIndemnity()
	'--------------------------------------------------------------------------------------------
	Dim lclsClaim As eClaim.ClaimBenef
	Dim lclsPolicyWin As Object
	
	lclsClaim = New eClaim.ClaimBenef
	If lclsClaim.CalIndemnity(CStr(Session("sCertype")), CInt(Session("nBranch")), CInt(Session("nProduct")), CDbl(Session("nPolicy")), CDbl(Session("nCertif")), CDate(Session("dEffecdate")), CDbl(Session("nClaim")), CInt(Session("nCase_num")), CInt(Session("nDeman_type")), mobjValues.StringToType(Request.QueryString("nClaimType"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(Request.QueryString("nIndemnity"), eFunctions.Values.eTypeData.etdLong), CInt(Session("nTransaction"))) Then
		With Response
			.Write("top.fraFolder.document.forms[0].gmdInit_date.value='" & mobjValues.TypeToString(lclsClaim.dInit_date, True) & "';")
			.Write("top.fraFolder.document.forms[0].gmdEnd_date.value='" & mobjValues.TypeToString(lclsClaim.dEnd_date, eFunctions.Values.eTypeData.etdDate, True) & "';")
			.Write("top.fraFolder.document.forms[0].gmnMonth_amo.value='" & mobjValues.TypeToString(lclsClaim.nRent, eFunctions.Values.eTypeData.etdDouble, True, 6) & "';")
			.Write("top.fraFolder.document.forms[0].gmnIndemn.value='" & mobjValues.TypeToString(lclsClaim.nAmount, eFunctions.Values.eTypeData.etdDouble, True, 6) & "';")
			'.Write("top.fraFolder.document.forms[0].gmnCapital.value='" & mobjValues.TypeToString(lclsClaim.nAmount, eFunctions.Values.eTypeData.etdDouble, True, 6) & "';")
			.Write("top.fraFolder.document.forms[0].cbeCurrency.value='" & lclsClaim.nCurrency & "';")
			.Write("top.fraFolder.document.forms[0].cbePayFreq.value='" & lclsClaim.nPayFreq & "';")
            .Write("top.fraFolder.document.forms[0].gmdInit_date.disabled =false;")
            .Write("top.fraFolder.document.forms[0].btn_gmdInit_date.disabled =false;")
            .Write("top.fraFolder.document.forms[0].btn_gmdEnd_date.disabled =false;")
            .Write("top.fraFolder.document.forms[0].gmdEnd_date.disabled =false;")
		End With
	Else
		With Response
			.Write("top.fraFolder.document.forms[0].gmdInit_date.value='';")
			.Write("top.fraFolder.document.forms[0].gmdEnd_date.value='';")
			.Write("top.fraFolder.document.forms[0].gmnMonth_amo.value='';")
			.Write("top.fraFolder.document.forms[0].cbePayFreq.value='';")
			.Write("top.fraFolder.document.forms[0].gmnIndemn.value='';")
			'.Write("top.fraFolder.document.forms[0].gmnCapital.value='';")
			.Write("top.fraFolder.document.forms[0].cbeCurrency.value='';")
            .Write("top.fraFolder.document.forms[0].gmdInit_date.disabled =true;")
            .Write("top.fraFolder.document.forms[0].btn_gmdInit_date.disabled =true;")
            .Write("top.fraFolder.document.forms[0].btn_gmdEnd_date.disabled =true;")
            .Write("top.fraFolder.document.forms[0].gmdEnd_date.disabled =true;")
                
		End With
		
	End If
	'UPGRADE_NOTE: Object lclsClaim may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
	lclsClaim = Nothing
End Sub

'% insPostSI024D: Se realiza el manejo de la ventana PopUp asociada al grid
'--------------------------------------------------------------------------------------------
Private Sub insPostSI024D()
	'--------------------------------------------------------------------------------------------
	Dim lclsClaimDisability As eClaim.ClaimDisability
	
	Dim sAction As String
	Dim nPercent As Double
	
	With Request
		If .QueryString("nAction") = 1 Then
			sAction = "Add"
		Else
			sAction = "Del"
		End If
		
		lclsClaimDisability = New eClaim.ClaimDisability
		
		Call lclsClaimDisability.insPostSI024D(sAction, mobjValues.StringToType(CStr(Session("nClaim")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(Session("nCase_num")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(Session("nDeman_Type")), eFunctions.Values.eTypeData.etdDouble), .QueryString("nCovergen"), .QueryString("nDisability"), mobjValues.StringToType(.QueryString("nRate"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(Session("nUsercode")), eFunctions.Values.eTypeData.etdDouble))
		
		'UPGRADE_NOTE: Object lclsClaimDisability may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
		lclsClaimDisability = Nothing
		
		lclsClaimDisability = New eClaim.ClaimDisability
		
		nPercent = lclsClaimDisability.insCalPercentDisability(mobjValues.StringToType(CStr(Session("nClaim")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(Session("nCase_num")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(Session("nDeman_Type")), eFunctions.Values.eTypeData.etdDouble))
		
		Response.Write("top.opener.top.frames['fraFolder'].document.forms[0].gmnDisabilityRate.value='" & mobjValues.TypeToString(nPercent, eFunctions.Values.eTypeData.etdDouble, True, 2) & "';")
		Response.Write("top.opener.top.frames['fraFolder'].$('#gmnDisabilityRate').change();")
		
		'UPGRADE_NOTE: Object lclsClaimDisability may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
		lclsClaimDisability = Nothing
	End With
End Sub

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("showdefvalues")
mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.33.47
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "showdefvalues"
Response.Write(mobjValues.StyleSheet())
%>
<HTML>
<HEAD>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>

<SCRIPT>
//+ Variable para el control de versiones 
    document.VssVersion="$$Revision: 4 $|$$Date: 25-03-13 7:33 $|$$Author: Jrengifo $"
</SCRIPT>
</HEAD>
<BODY>
	<FORM NAME="ShowValues">
	</FORM>
</BODY>
</HTML>
<%

Response.Write("<SCRIPT>")
Select Case Request.QueryString("Field")
	Case "Auto_db"
		Call ShowAutoDetail()
	Case "IndemAmount"
		Call ShowAmount()
	Case "ProviderCode"
		Call ShowClient()
	Case "CheckClient"
		Call CheckCreClient()
	Case "CheckWitness"
		Call CheckCreWitness()
	Case "DateIndem"
		Call FindDateIndem()
	Case "Digit"
		Call insShowDigit()
	Case "Auto"
		Call insShowAuto()
	Case "CalIndemnity"
		Call insCalIndemnity()
	Case "insPostSI024D"
		Call insPostSI024D()
End Select

Response.Write(mobjValues.CloseShowDefValues(Request.QueryString("sFrameCaller")))
Response.Write("</SCRIPT>")

'UPGRADE_NOTE: Object mobjValues may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjValues = Nothing
%>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 11.33.47
Call mobjNetFrameWork.FinishPage("showdefvalues")
'UPGRADE_NOTE: Object mobjNetFrameWork may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




