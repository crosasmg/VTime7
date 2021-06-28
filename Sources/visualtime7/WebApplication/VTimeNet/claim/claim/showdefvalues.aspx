<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<script language="VB" runat="Server">
''dim eRemoteDB.Constants.intNull As String = ""

Dim mobjValues As eFunctions.Values


'% ClaimClient: Busca el certificado del asegudao en la tabla roles 
'-------------------------------------------------------------------------------------------- 
Sub ClaimClient()
	'-------------------------------------------------------------------------------------------- 
	Dim lclsRoles As ePolicy.Roles
	lclsRoles = New ePolicy.Roles
	
        If lclsRoles.Find("2", mobjValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Constants.intNull, 2, Request.QueryString.Item("sClient"), mobjValues.StringToType(Request.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate), True) Then
		
            Response.Write("top.fraFolder.document.forms[0].tctCodeAseg.value='" & lclsRoles.SCLIENT & "';")
            Response.Write("top.fraFolder.document.forms[0].tctCodeAseg_Digit.value='" & lclsRoles.sDigit & "';")
            Response.Write("top.fraFolder.UpdateDiv('tctCodeAseg_Name','" & Replace(lclsRoles.sCliename, "'", "´") & "','Normal');")
		
            Response.Write("top.fraFolder.document.forms[0].tcdBirthdat.value='" & mobjValues.TypeToString(lclsRoles.dBirthdate, eFunctions.Values.eTypeData.etdDate) & "';")
		
            If Request.QueryString.Item("ncertif") = "0" Or IsNothing(Request.QueryString.Item("ncertif")) Then
                Response.Write("top.fraFolder.document.forms[0].tcnCertif.value='" & lclsRoles.nCertif & "';")
                Response.Write("top.fraFolder.document.forms[0].valCover.Parameters.Param6.sValue='" & lclsRoles.nCertif & "';")
            End If
		
            Response.Write("top.fraFolder.document.forms[0].valCover.Parameters.Param5.sValue='" & Request.QueryString.Item("nPolicy") & "';")
            Response.Write("top.fraFolder.document.forms[0].valIllness.Parameters.Param3.sValue='" & Request.QueryString.Item("nPolicy") & "';")
            Response.Write("top.fraFolder.document.forms[0].valIllness.Parameters.Param4.sValue='" & lclsRoles.nCertif & "';")
		
            Response.Write("top.fraFolder.document.forms[0].tctClient.value='" & lclsRoles.SCLIENT & "';")
            Response.Write("top.fraFolder.document.forms[0].tctClient_Digit.value='" & lclsRoles.sDigit & "';")
            Response.Write("top.fraFolder.UpdateDiv('tctClient_Name','" & Replace(lclsRoles.sCliename, "'", "´") & "','Normal');")
        Else
            Response.Write("alert('Adv. El cliente no corresponde a la póliza');")
        End If
	
	lclsRoles = Nothing
End Sub

'% insShowExchange: se busca el factor de cambio para una moneda 
'%                                  Se utiliza para el campo Moneda de la página SI008.aspx 
'-------------------------------------------------------------------------------------------- 
Private Sub insShowExchange()
	'-------------------------------------------------------------------------------------------- 
	Dim lclsExchange As eGeneral.Exchange
	lclsExchange = New eGeneral.Exchange
	
	With lclsExchange
		If .Find(mobjValues.StringToType(Request.QueryString.Item("nCurrency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("dValdate"), eFunctions.Values.eTypeData.etdDate)) Then
			Response.Write("top.fraFolder.document.forms[0].tcnExchange.value = " & .nExchange & ";")
		Else
			Response.Write("top.fraFolder.document.forms[0].tcnExchange.value = '0';")
		End If
	End With
	lclsExchange = Nothing
	Response.Write("top.fraFolder.insSubmitPage();")
End Sub

'% insShowExchange_1: Se busca el factor de cambio para una moneda dada una fecha de valoración. 
'%                                    Se utiliza para el campo "Factor de Cambio" de la página SI738.aspx 
'----------------------------------------------------------------------------------------------- 
Private Sub insShowExchange_1()
	'----------------------------------------------------------------------------------------------- 
	Dim lclsExchange As eGeneral.Exchange
	lclsExchange = New eGeneral.Exchange
	
	With lclsExchange
		If .Find(mobjValues.StringToType(Request.QueryString.Item("nCurrency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("dValdate"), eFunctions.Values.eTypeData.etdDate)) Then
			Response.Write("top.fraFolder.document.forms[0].tcnExchange.value = " & .nExchange & ";")
			If .nExchange = 1 Then
				Response.Write("top.fraFolder.document.forms[0].tcnExchange.disabled = true;")
			Else
				Response.Write("top.fraFolder.document.forms[0].tcnExchange.disabled = false;")
			End If
		Else
			Response.Write("top.fraFolder.document.forms[0].tcnExchange.value = '0';")
		End If
	End With
	lclsExchange = Nothing
End Sub

'% insParamToConcept: se pasan los párametros al campo Concepto de pago 
'%                                    Se utiliza para el campo Cobertura de la página SI008.aspx 
'-------------------------------------------------------------------------------------------- 
Private Sub insParamToConcept()
	'-------------------------------------------------------------------------------------------- 
	Dim lclsCl_cover As eClaim.Cl_cover
	Dim lclsClaim As eClaim.Claim
	
	lclsClaim = New eClaim.Claim
	lclsCl_cover = New eClaim.Cl_cover
	
	With lclsClaim
		If .Find(Session("nClaim")) Then
            '+ Se le pasa String.Empty al parametro sClient porque dentro del metodo no se usa
			If lclsCl_cover.Find_Policy(Session("nClaim"), Request.QueryString.Item("nModulec"), Request.QueryString.Item("nCover"), String.Empty, Session("nCase_num"), Session("nDeman_type"), .nBranch, .nProduct, .nPolicy, .nCertif, .dOccurdat) Then
				With Response
					'+ Se pasa el parámetro a Concepto de pago: nModulec 
					.Write("opener.document.forms[0].nConcept.Parameters.Param1.sValue=" & lclsCl_cover.nModulec & ";")
					
					'+ Se asigna la moneda de la cobertura al campo Hidden 
					.Write("opener.document.forms[0].nCoverCurrency.value=" & lclsCl_cover.nCurrency & ";")
					
					'+ Se asigna el grupo asegurado al que pertenece la     cobertura                                       
					.Write("opener.document.forms[0].nGroup_insu.value=" & lclsCl_cover.nGroup_insu & ";")
					
				End With
			End If
		End If
	End With
	
	lclsCl_cover = Nothing
	lclsClaim = Nothing
End Sub

'% insDeletePayCla: Se eliminan los datos asociados al pago del siniestro 
'%                                  Se utiliza para el campo Moneda y Tipo de Pago de la página SI008.aspx 
'-------------------------------------------------------------------------------------------- 
Private Sub insDeletePayCla()
	'-------------------------------------------------------------------------------------------- 
	Dim lclsT_PayCla As eClaim.T_PayCla
	lclsT_PayCla = New eClaim.T_PayCla
	
	If lclsT_PayCla.DeleteByCase(Session("nClaim"), Session("nCase_num"), Session("nDeman_type")) Then
		Response.Write("opener.top.fraFolder.document.location='SI008.aspx';")
	End If
	lclsT_PayCla = Nothing
End Sub

'% insExpandCodeClient: Se expande el código del cliente 
'%                                      Se utiliza para el campo Cliente de la página SI008.aspx 
'-------------------------------------------------------------------------------------------- 
Private Sub insExpandCodeClient()
	'-------------------------------------------------------------------------------------------- 
	Dim lstrClient As String
	Dim lclsClient As eClient.Client
	lclsClient = New eClient.Client
	
	lstrClient = lclsClient.ExpandCode(Request.QueryString.Item("sClient"))
	Response.Write("opener.document.forms[0].valClient.value=""" & lstrClient & """;")
	
	lclsClient = Nothing
End Sub

'% insReaClientRole: 
'-------------------------------------------------------------------------------------------- 
Private Sub insReaClientRole()
	Dim eFunctions As Object
	'-------------------------------------------------------------------------------------------- 
	Dim lclsClaim As eClaim.Claim
	Dim lclsRoles As ePolicy.Roles
	Dim lclsClaimBenef As eClaim.ClaimBenef
	
	lclsRoles = New ePolicy.Roles
	lclsClaim = New eClaim.Claim
	
	With lclsClaim
		If .Find(Session("nClaim")) Then
			'+aqui el find si en cliente 
			If lclsRoles.Find(.sCertype, .nBranch, .nProduct, .nPolicy, .nCertif, CShort(Request.QueryString.Item("nRole")), vbNullString, mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate)) Then
				Response.Write("top.fraFolder.document.forms[0].valClient.value=""" & lclsRoles.sClient & """;")
				
				Response.Write("top.fraFolder.UpdateDiv(""valClientDesc"",""" & lclsRoles.sCliename & """);")
				
				'+Se busca el Titular de la orden de pago y el destino del cheque. 
				lclsClaimBenef = New eClaim.ClaimBenef
				
				With lclsClaimBenef
					If .Find_client(mobjValues.StringToType(Session("nClaim"), eFunctions.Values.eTypeData.etdDouble), lclsRoles.sClient, mobjValues.StringToType(Session("nCase_num"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nDeman_type"), eFunctions.Values.eTypeData.etdDouble)) Then
						
						If lclsClaimBenef.nOffice_pay <> eRemoteDB.Constants.intNull Then
							Response.Write("top.fraFolder.document.forms[0].cbeOffice_Pay.value=""" & lclsClaimBenef.nOffice_pay & """;")
							
						Else
							Response.Write("top.fraFolder.document.forms[0].cbeOffice_Pay.value=""" & lclsClaim.nOffice_pay & """;")
							
						End If
						
						If lclsClaimBenef.sClient_rep <> vbNullString Then
							Response.Write("top.fraFolder.document.forms[0].tctClient_rep.value=""" & lclsClaimBenef.sClient_rep & """;")
							
						Else
							Response.Write("top.fraFolder.document.forms[0].tctClient_rep.value=""" & lclsRoles.sClient & """;")
							
						End If
						
					End If
				End With
				
				lclsClaimBenef = Nothing
				
			End If
		End If
	End With
	
	lclsRoles = Nothing
	lclsClaim = Nothing
End Sub

'% insDP051_Claim: Se encarga de guardar en la variable de session DP051_nClaim el 
'                  siniestro de le pantalla SI051 y llevarlo como consulta a la SI001_K 
'-------------------------------------------------------------------------------------------- 
Private Sub insDP051_Claim()
	'-------------------------------------------------------------------------------------------- 
	Session("DP051_nClaim") = Request.QueryString.Item("nClaim")
End Sub
'% insFindDataClaim: Encuentra Poliza, Ramo , Producto del siniestro. 
'-------------------------------------------------------------------------------------------- 
Private Sub insFindDataClaim()
	'-------------------------------------------------------------------------------------------- 
	Dim lclsClaim As eClaim.Claim
	lclsClaim = New eClaim.Claim
	
	If lclsClaim.Find(mobjValues.StringToType(Request.QueryString.Item("nClaim"), eFunctions.Values.eTypeData.etdDouble)) Then
		
		Response.Write("top.fraHeader.document.forms[0].tcnPolicy.value='" & lclsClaim.nPolicy & "';")
		Response.Write("top.fraHeader.document.forms[0].tcnPolicy.disabled='" & False & "';")
		Response.Write("top.fraHeader.document.forms[0].cbeBranch.value='" & lclsClaim.nBranch & "';")
		Response.Write("top.fraHeader.document.forms[0].cbeBranch.disabled='" & False & "';")
            Response.Write("top.fraHeader.document.forms[0].valProduct.Parameters.Param1.sValue=" & lclsClaim.nBranch & ";")
            Response.Write("top.fraHeader.document.forms[0].valProduct.value='" & lclsClaim.nProduct & "';")
            Response.Write("top.fraHeader.$('#valProduct').change();")

	Else
		Response.Write("alert('Adv. Siniestro no esta registrado');")
	End If
	lclsClaim = Nothing
End Sub

'% insSI021: Se encarga de guardar en variables session los datos para la SI_007_2 y la OP006 
'-------------------------------------------------------------------------------------------- 
Private Sub insSI021()
	'-------------------------------------------------------------------------------------------- 
	Session("nClaim") = mobjValues.StringToType(Request.QueryString.Item("nClaim"), eFunctions.Values.eTypeData.etdDouble)
	Session("nPolicy") = mobjValues.StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble)
	Session("nBranch") = mobjValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble)
	Session("nProduct") = mobjValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble)
	Session("nCertif") = Request.QueryString.Item("nCertif")
	Session("dEffecdate") = mobjValues.StringToType(Today, eFunctions.Values.eTypeData.etdDate)
	Session("nCase_Num") = Request.QueryString.Item("nCase")
	Session("nDeman_type") = Request.QueryString.Item("nDeman_type")
	Session("sClient") = Request.QueryString.Item("sClient")
	
	'- Variables usadas para el llamado a la SI008 
	Session("SI008_dPayType") = Today
	Session("SI008_nClaim") = Request.QueryString.Item("nClaim")
	Session("SI008_nCaseNum") = Request.QueryString.Item("nCaseNum")
	Session("SI008_nPayType") = Request.QueryString.Item("nPayType")
	Session("SI007_Codispl") = "SI021"
	
End Sub

'% insProvider: 
'-------------------------------------------------------------------------------------------- 
Private Sub insProvider()
	'-------------------------------------------------------------------------------------------- 
	Dim lclsClaim As eClaim.Prof_ord
	lclsClaim = New eClaim.Prof_ord
	
	With lclsClaim
		If .FindProviderOrder(CShort(Request.QueryString.Item("nZone"))) Then
			If .nProvider <> 0 Then
				Response.Write("with (top.frames['fraFolder'].document.forms[0]){")
				Response.Write("    cbeProvider.value= " & mobjValues.TypeToString(.nProvider, eFunctions.Values.eTypeData.etdDouble) & ";")
				Response.Write("    cbeProvider.disabled=true;")
				Response.Write("    document.btncbeProvider.disabled=true;")
				Response.Write("    top.frames['fraFolder'].UpdateDiv('cbeProviderDesc','" & .sProviderName & "');")
				
				Response.Write("}")
			Else
				Response.Write("with (top.frames['fraFolder'].document.forms[0]){")
				Response.Write("    cbeProvider.value=0;")
				Response.Write("    cbeProvider.disabled=false;")
				Response.Write("    document.btncbeProvider.disabled=false;")
				Response.Write("    top.frames['fraFolder'].UpdateDiv('cbeProviderDesc','');")
				Response.Write("}")
			End If
		End If
		
	End With
	
	lclsClaim = Nothing
End Sub

'% insSI008_K: Se encarga de buscar los valores que deben ser mostrados por el sistema 
'%             asociados al siniestro. 
'-------------------------------------------------------------------------------------------- 
Private Sub insSI008_K()
	'-------------------------------------------------------------------------------------------- 
	Dim lclsClaim As eClaim.Claim
	lclsClaim = New eClaim.Claim
	
	With lclsClaim
		If .FindControl(Request.QueryString.Item("nClaim")) Then
			Response.Write("with(top.frames['fraHeader'].document.forms[0]){")
			Response.Write("tctBranch.value='" & .sBranchDesc & "';")
			Response.Write("tcnBranch.value='" & .nBranch & "';")
			Response.Write("tctProduct.value='" & .sProductDesc & "';")
			Response.Write("tcnProduct.value='" & .nProduct & "';")
			Response.Write("tcnPolicy.value='" & .nPolicy & "';")
			Response.Write("}")
		End If
		
	End With
	lclsClaim = Nothing
End Sub

'%insShowDemandat: Se obtiene el código del denunciante y la sucursal asociada a la relación (SI738) 
'%---------------------------------------------------------------------------------------------------------- 
Private Sub insShowDemandat()
	'%---------------------------------------------------------------------------------------------------------- 
	Dim lclsClaim As eClaim.Claim
	Dim lclsClaim_master As eClaim.Claim_Master
	
	lclsClaim = New eClaim.Claim
	lclsClaim_master = New eClaim.Claim_Master
	
	'+ Se obtiene el código del denunciante (reclamante). 
	If lclsClaim_master.Find(mobjValues.StringToType(Request.QueryString.Item("nBordereaux_cl"), eFunctions.Values.eTypeData.etdDouble)) Then
		Response.Write("top.fraFolder.document.forms[0].tctClientCode.value = '" & lclsClaim_master.sClient & "';")
		
	End If
	
	'+ Se obtiene la sucursal asociada al reclamante de la relación. 
	If lclsClaim.Find(mobjValues.StringToType(Request.QueryString.Item("nClaim"), eFunctions.Values.eTypeData.etdDouble)) Then
		Response.Write("top.fraFolder.document.forms[0].cbeOfficePay.value =" & lclsClaim.nOffice & ";")
	End If
	
	lclsClaim_master = Nothing
	
End Sub
'% ShowServiceOrderData: Muestra la data relacionada con una orden de servicio específica - ACM - 18/06/2002 
'----------------------------------------------------------------------------------------------------------- 
Private Sub ShowServiceOrderData()
	'----------------------------------------------------------------------------------------------------------- 
	Dim lclsProf_ord As eClaim.Prof_ord
	Dim lclsAuto As ePolicy.Automobile
	Dim lclsFunctions As eFunctions.Values
	Dim lclsClaimBenef As eClaim.Claimbenef
	Dim lclsClaim_Thir As eClaim.Claim_Thir
	Dim lstrVeh_Brand As String
	Dim lstrVeh_Model As String
	Dim lstrFirstCase As String
	Dim lstrCase() As String
	Dim lintCase_num As String
	Dim lintDeman_type As String
	Dim lstrClient As String
	Dim lclsFire_budget As eClaim.Fire_budget
	
	lclsFunctions = New eFunctions.Values
	lclsClaimBenef = New eClaim.Claimbenef
	
	lstrFirstCase = Request.QueryString.Item("nCaseNumber")
	
	If lstrFirstCase <> vbNullString Then
		lstrCase = Split(lstrFirstCase, "/")
		lintCase_num = lstrCase(0)
		lintDeman_type = lstrCase(1)
		lstrClient = lstrCase(2)
	End If
	
    If Request.QueryString.Item("nServiceOrder") <> vbNullString AndAlso lclsFunctions.StringToType(Request.QueryString.Item("nServiceOrder"), eFunctions.Values.eTypeData.etdDouble) > 0 Then
		
		lclsFire_budget = New eClaim.Fire_budget
		If lclsFire_budget.InsValBranch_prof_ord(mobjValues.StringToType(Request.QueryString.Item("nServiceOrder"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nClaim"), eFunctions.Values.eTypeData.etdDouble)) Then
			
			Response.Write("try {")
			Response.Write("top.fraHeader.ShowDiv('lblauto','hide');")
			Response.Write("top.fraHeader.document.forms[0].tcnBranch_Fire.value=1;}catch(error){}")
		Else
			Response.Write("try {")
			Response.Write("top.fraHeader.ShowDiv('lblauto','show');")
			Response.Write("top.fraHeader.document.forms[0].tcnBranch_Fire.value=0;}catch(error){}")
			'+ Se verifica si la figura del cliente es "Tercero", para buscar los datos asociados al vehículo del mismo 
			If lclsClaimBenef.FindBenef(mobjValues.StringToType(Request.QueryString.Item("nClaim"), eFunctions.Values.eTypeData.etdDouble), lstrClient, mobjValues.StringToType(lintCase_num, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(lintDeman_type, eFunctions.Values.eTypeData.etdDouble), 3) Then
				
				lclsClaim_Thir = New eClaim.Claim_Thir
				If lclsClaim_Thir.Find(Request.QueryString.Item("nClaim"), lintCase_num, lintDeman_type) Then
					Response.Write("top.fraHeader.UpdateDiv('tctChasisCode','" & lclsClaim_Thir.sChassis & "', 'Normal');")
					
					Response.Write("top.fraHeader.UpdateDiv('cbeMark', '" & lclsClaim_Thir.sDesMark & "', 'Normal');")
					
					Response.Write("top.fraHeader.UpdateDiv('cbeModel','" & lclsClaim_Thir.sVehModel & "', 'Normal');")
					
					Response.Write("top.fraHeader.UpdateDiv('tcnYear','" & mobjValues.TypeToString(lclsClaim_Thir.nYear, eFunctions.Values.eTypeData.etdDouble) & "', 'Normal');")
					
				End If
				lclsClaim_Thir = Nothing
			Else
				'+ Se obtienen los datos del vehículo asegurado 
				lclsAuto = New ePolicy.Automobile
				If lclsAuto.Find(Session("tctCertype"), mobjValues.StringToType(Session("tcnBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("tcnProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("tcnCertif"), eFunctions.Values.eTypeData.etdDouble), Today) Then
					
					Response.Write("top.fraHeader.UpdateDiv('tctChasisCode','" & lclsAuto.sChassis & "', 'Normal');")
					
					
					If lclsAuto.Find_Tab_au_veh(lclsAuto.sVehCode) Then
						lstrVeh_Brand = lclsFunctions.getMessage(lclsAuto.nVehBrand, "Table7042")
						lstrVeh_Model = lclsFunctions.getMessage(mobjValues.StringToType(lclsAuto.sVehCode, eFunctions.Values.eTypeData.etdDouble), "Tab_au_Veh")
						
						
						Response.Write("top.fraHeader.UpdateDiv('cbeMark', '" & lstrVeh_Brand & "', 'Normal');")
						
						Response.Write("top.fraHeader.UpdateDiv('cbeModel','" & lstrVeh_Model & "', 'Normal');")
						
						Response.Write("top.fraHeader.UpdateDiv('tcnYear', " & lclsAuto.nYear & ", 'Normal');")
						
					End If
					lclsAuto = Nothing
				End If
			End If
			
			If Request.QueryString.Item("sForm") <> "SI011" Then
				lclsProf_ord = New eClaim.Prof_ord
				If lclsProf_ord.Find_nServ(Request.QueryString.Item("nServiceOrder")) Then
					Response.Write("top.fraHeader.document.forms[0].tcnTypeOrder.value=" & lclsProf_ord.nOrderType & ";")
					
					Response.Write("top.fraHeader.document.forms[0].tctStateOrder.value='" & mobjValues.TypeToString(lclsProf_ord.nStatus_ord, eFunctions.Values.eTypeData.etdDouble) & "';")
					
					Response.Write("top.fraHeader.document.forms[0].tcnTransaction.value=" & lclsProf_ord.nTransac & ";")
					
				End If
				lclsProf_ord = Nothing
			End If
		End If
		
		lclsFunctions = Nothing
		lclsClaimBenef = Nothing
		lclsFire_budget = Nothing
	End If
End Sub

'% insReaClient_rep: Se realiza la busqueda del representante del beneficiario y la sucursal destino 
'-------------------------------------------------------------------------------------------- 
Private Sub insReaClient_rep()
	'-------------------------------------------------------------------------------------------- 
	Dim lclsClaimBenef As eClaim.ClaimBenef
	Dim lclsClaim As eClaim.Claim
	
	lclsClaimBenef = New eClaim.ClaimBenef
	lclsClaim = New eClaim.Claim
	
	With lclsClaimBenef
		If .Find_client(mobjValues.StringToType(Session("nClaim"), eFunctions.Values.eTypeData.etdDouble), Request.QueryString.Item("sClient"), mobjValues.StringToType(Session("nCase_num"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nDeman_type"), eFunctions.Values.eTypeData.etdDouble)) Then
			
			If lclsClaimBenef.nOffice_pay <> eRemoteDB.Constants.intNull Then
				Response.Write("top.fraFolder.document.forms[0].cbeOffice_Pay.value=""" & lclsClaimBenef.nOffice_pay & """;")
				
			Else
				lclsClaim = New eClaim.Claim
				With lclsClaim
					If .Find(Session("nClaim")) Then
						Response.Write("top.fraFolder.document.forms[0].cbeOffice_Pay.value=""" & lclsClaim.nOffice_pay & """;")
						
					End If
				End With
				lclsClaim = Nothing
			End If
			
			If lclsClaimBenef.sClient_rep <> vbNullString Then
				Response.Write("top.fraFolder.document.forms[0].tctClient_rep.value=""" & lclsClaimBenef.sClient_rep & """;")
				
			Else
				Response.Write("top.fraFolder.document.forms[0].tctClient_rep.value=""" & Request.QueryString.Item("sClient") & """;")
				
			End If
			
		End If
	End With
	
	lclsClaimBenef = Nothing
End Sub

'% ShowAccount: Se realiza la busqueda de la cuenta y el numero de credito asociado a la poliza en tratamiento 
'-------------------------------------------------------------------------------------------- 
Private Sub ShowAccount()
	'-------------------------------------------------------------------------------------------- 
	Dim lclsLife As ePolicy.Life
	lclsLife = New ePolicy.Life
	
	Dim lclsRoles As ePolicy.Roles
	lclsRoles = New ePolicy.Roles
	
	With lclsLife
		If .Find("2", Request.QueryString.Item("nBranch"), Request.QueryString.Item("nProduct"), Request.QueryString.Item("nPolicy"), Request.QueryString.Item("nCertif"), Today) Then
			Response.Write("top.fraFolder.document.forms[0].tctCredit.value = '" & .sCreditnum & "';")
			Response.Write("top.fraFolder.document.forms[0].tctAccount.value = '" & .sAccnum & "';")
		Else
			Response.Write("top.fraFolder.document.forms[0].tctCredit.value = '0';")
			Response.Write("top.fraFolder.document.forms[0].tctAccount.value = '0';")
		End If
	End With
	
	If lclsRoles.Find("2", mobjValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nCertif"), eFunctions.Values.eTypeData.etdDouble), 2, "", mobjValues.StringToType(Request.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate), True) Then
		Response.Write("top.fraFolder.document.forms[0].tctClientCollect.value='" & lclsRoles.sClient & "';")
		Response.Write("top.fraFolder.document.forms[0].tctCodeAseg.value='" & lclsRoles.sClient & "';")
		Response.Write("top.fraFolder.document.forms[0].tctCodeAseg_Digit.value='" & lclsRoles.sDigit & "';")
		Response.Write("top.fraFolder.$('#tctCodeAseg_Digit').change();")
	End If
	
	lclsLife = Nothing
	lclsRoles = Nothing
	
	
End Sub

'%ShowProvider: Se realiza la busqueda del proveedor asociado a la cobertura generica 
'-------------------------------------------------------------------------------------------- 
Private Sub ShowProvider()
	'-------------------------------------------------------------------------------------------- 
	Dim lclsCover As eClaim.Cl_cover
	Dim lclsClaim As eClaim.Claim
	Dim lclsClient As eClient.Client
	Dim lstrClient As String
	Dim lstrDigit As String
	
	lclsCover = New eClaim.Cl_cover
	lclsClaim = New eClaim.Claim
	lclsClient = New eClient.Client
	
	lstrClient = lclsCover.Find_CoverProvider(mobjValues.StringToDate(Request.QueryString.Item("dEffecdate")), Request.QueryString.Item("nBranch"), Request.QueryString.Item("nProduct"), Request.QueryString.Item("nCover"))
	
	lstrDigit = lclsClaim.CalcDigit(lstrClient)
	
	Call lclsClient.FindClientName(lstrClient)
	
	If lstrClient <> "" Then
		Response.Write("top.fraHeader.document.forms[0].tctClientCollect.value = '" & lstrClient & "';")
		Response.Write("top.fraHeader.document.forms[0].tctClientCollect_Digit.value = '" & lstrDigit & "';")
		Response.Write("top.fraHeader.UpdateDiv('lblCliename','" & lclsClient.sCliename & "');")
	End If
	lclsCover = Nothing
	lclsClaim = Nothing
	lclsClient = Nothing
End Sub

'%ShowRelation: Se realiza el cálculo del nuevo numero de relacion. 
'-------------------------------------------------------------------------------------------- 
Private Sub ShowRelation()
	'-------------------------------------------------------------------------------------------- 
	Dim lclsClaim_master As eClaim.Claim_Master
    Dim lclcClaim_window As Dictionary(Of String, String).KeyCollection
	Dim llngRelation As Short
	Dim lblnFind As Boolean
	Dim lintCount As Integer
	
	lblnFind = False
	
	'+ Si se esta indicando un grupo, se realiza la busqueda de la relacion asociada al grupo. 
	
	If Request.QueryString.Item("Form") = "PopUp" Then
		If CDbl(Request.QueryString.Item("tcnGroup")) = 0 Then
			Response.Write("top.fraFolder.document.forms[0].tcnRelation.value = '0';")
			lblnFind = True
		Else
			If Not IsNothing(Session("mobjCollecRelation")) Then
				lclcClaim_window = Session("mobjCollecRelation").Keys
				For lintCount = 0 To Session("mobjCollecRelation").count - 1
					If Request.QueryString.Item("tcnGroup") = Session("mobjCollecRelation")(lclcClaim_window(lintCount)) Then
						lclsClaim_master = New eClaim.Claim_Master
						If lclsClaim_master.FindSI737(lclcClaim_window(lintCount)) Then
							Response.Write("top.fraFolder.document.forms[0].tcnRelation.value = '" & lclsClaim_master.nBordereaux_cl & "';")
							
							lblnFind = True
							Exit For
						End If
					End If
				Next 
			End If
		End If
	End If
	
	If Not lblnFind Then
		lclsClaim_master = New eClaim.Claim_master
		llngRelation = lclsClaim_master.CalNumberRelation(Session("nUsercode"))
		
		If llngRelation <> -1 Then
			If Request.QueryString.Item("Form") <> "PopUp" Then
				Response.Write("top.fraHeader.document.forms[0].tcnRelat.value = '" & llngRelation & "';")
			Else
				Response.Write("top.fraFolder.document.forms[0].tcnRelation.value = '" & llngRelation & "';")
			End If
		End If
	End If
	lclsClaim_master = Nothing
End Sub

'%ShowCurrency(). Muestra la moneda asociada a la poliza en tratamiento 
'-------------------------------------------------------------------------------------------- 
Private Sub ShowCurrency()
	'-------------------------------------------------------------------------------------------- 
	Dim lclsCurrenpol As eClaim.Claim_master
	Dim lclsPolicy As ePolicy.Policy
	Dim lclsRoles As ePolicy.Roles
	Dim lstrCurrency As String
	Dim lintBranch As String
	Dim lintProduct As String
	
	lclsPolicy = New ePolicy.Policy
	If lclsPolicy.FindPolicybyPolicy("2", Request.QueryString.Item("nPolicy")) Then
		
		lintBranch = mobjValues.TypeToString(lclsPolicy.nBranch, eFunctions.Values.eTypeData.etdLong)
		lintProduct = mobjValues.TypeToString(lclsPolicy.nProduct, eFunctions.Values.eTypeData.etdLong)
		Response.Write("top.fraHeader.document.forms[0].valCover.Parameters.Param3.sValue='" & lintBranch & "';")
		Response.Write("top.fraHeader.document.forms[0].valCover.Parameters.Param4.sValue='" & lintProduct & "';")
		
		Response.Write("top.fraHeader.document.forms[0].cbeBranch.value = '" & lintBranch & "';")
		Response.Write("top.fraHeader.document.forms[0].valProduct.value = '" & lintProduct & "';")
		
		lclsCurrenpol = New eClaim.Claim_master
		lclsRoles = New ePolicy.Roles
		
		lstrCurrency = lclsCurrenpol.ShowCurrency(mobjValues.StringToDate(Request.QueryString.Item("dEffecdate")), mobjValues.StringToType(lintBranch, eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(lintProduct, eFunctions.Values.eTypeData.etdLong), Request.QueryString.Item("nPolicy"), Request.QueryString.Item("nCertif"))
		
		If lstrCurrency <> "" Then
			Response.Write("top.fraHeader.document.forms[0].cbeCurrency.value = '" & lstrCurrency & "';")
		End If
		
		Response.Write("top.fraHeader.document.forms[0].hddPoliType.value = '" & lclsPolicy.sPolitype & "';")
		
		If lclsPolicy.nOffice > 0 Then
			Response.Write("top.fraHeader.document.forms[0].cbeOfficeAgen.Parameters.Param1.sValue =" & lclsPolicy.nOffice & ";")
			
			Response.Write("top.fraHeader.document.forms[0].cbeAgency.Parameters.Param1.sValue =" & lclsPolicy.nOffice & ";")
			
			Response.Write("top.fraHeader.document.forms[0].cbeOffice.value=" & lclsPolicy.nOffice & ";")
		End If
		If lclsPolicy.nOfficeAgen > 0 Then
			Response.Write("top.fraHeader.document.forms[0].cbeAgency.Parameters.Param2.sValue =" & lclsPolicy.nOfficeAgen & ";")
			
			Response.Write("top.fraHeader.document.forms[0].cbeOfficeAgen.value=" & lclsPolicy.nOfficeAgen & ";")
			
			Response.Write("top.fraHeader.$('#cbeOfficeAgen').change();")
		End If
		If lclsPolicy.nAgency > 0 Then
			Response.Write("top.fraHeader.document.forms[0].cbeAgency.value=" & lclsPolicy.nAgency & ";")
			Response.Write("top.fraHeader.$('#cbeAgency').change();")
		End If
		
		If lclsRoles.Find("2", mobjValues.StringToType(lintBranch, eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(lintProduct, eFunctions.Values.eTypeData.etdLong), Request.QueryString.Item("nPolicy"), Request.QueryString.Item("nCertif"), 1, "", mobjValues.StringToDate(Request.QueryString.Item("dEffecdate")), True) Then
			Response.Write("top.fraHeader.document.forms[0].tctClientCollect.value='" & lclsRoles.sClient & "';")
			
			Response.Write("top.fraHeader.document.forms[0].tctClientCollect_Digit.value='" & lclsRoles.sDigit & "';")
			
			Response.Write("top.fraHeader.UpdateDiv('lblCliename','" & lclsRoles.sCliename & "');")
		End If
	End If
	
	lclsCurrenpol = Nothing
	lclsRoles = Nothing
	lclsPolicy = Nothing
End Sub

'%ShowBrancht(). Realiza la busqueda del tipo de ramo asociado al ramo producto en tratamiento 
'-------------------------------------------------------------------------------------------- 
Private Sub ShowBrancht()
	'-------------------------------------------------------------------------------------------- 
	Dim lclsProduct As eProduct.Product
	Dim ldtmEffecdate As Date
	
	ldtmEffecdate = Today
	lclsProduct = New eProduct.Product
	If lclsProduct.Find(Request.QueryString.Item("nBranch"), Request.QueryString.Item("nProduct"), ldtmEffecdate) Then
		Response.Write("top.fraHeader.document.forms[0].hddBrancht.value = '" & lclsProduct.sBrancht & "';")
	End If
	lclsProduct = Nothing
End Sub

'% PoliType:Se busca el tipo de la poliza. 
'-------------------------------------------------------------------------------------------- 
Private Sub PoliType()
	'-------------------------------------------------------------------------------------------- 
	
	Dim lclsPolicy As ePolicy.Policy
	lclsPolicy = New ePolicy.Policy
	
	With lclsPolicy
		If .Find(Request.QueryString.Item("sCertype"), Request.QueryString.Item("nBranch"), Request.QueryString.Item("nProduct"), Request.QueryString.Item("nPolicy"), True) Then
			If .sPolitype = "1" Then
				Response.Write("top.fraHeader.document.forms[0].tcnCertif.value='0';")
				Response.Write("top.fraHeader.document.forms[0].tcnCertif.disabled=true;")
			Else
				Response.Write("top.fraHeader.document.forms[0].tcnCertif.value='';")
				Response.Write("top.fraHeader.document.forms[0].tcnCertif.disabled=false;")
			End If
		Else
			Response.Write("top.fraHeader.document.forms[0].tcnCertif.value='0';")
			Response.Write("top.fraHeader.document.forms[0].tcnCertif.disabled=true;")
		End If
	End With
	lclsPolicy = Nothing
End Sub

'% Claim_SI774: Obtiene los valores básicos del siniestro a ser manejados dentro de la 
'%              transacción SI774 
'-------------------------------------------------------------------------------------------- 
Private Sub Claim_SI774()
	'-------------------------------------------------------------------------------------------- 
	Dim lclsClaim As eClaim.Claim
	
	lclsClaim = New eClaim.Claim
	If lclsClaim.Find(mobjValues.StringToType(Request.QueryString.Item("nClaim"), eFunctions.Values.eTypeData.etdDouble)) Then
		Session("tctCertype") = lclsClaim.sCertype
		Session("tcnBranch") = lclsClaim.nBranch
		Session("tcnProduct") = lclsClaim.nProduct
		Session("tcnPolicy") = lclsClaim.nPolicy
		Session("tcnCertif") = lclsClaim.nCertif
	End If
	
	lclsClaim = Nothing
	
End Sub

'% Claim_SI830: Obtiene los valores básicos del siniestro a ser manejados dentro de la 
'%              transacción SI830_k y SI831_K  
'-------------------------------------------------------------------------------------------- 
Private Sub Claim_SI830()
	'-------------------------------------------------------------------------------------------- 
	Dim lclsClaim As eClaim.Claim
	lclsClaim = New eClaim.Claim
	If lclsClaim.Find(mobjValues.StringToType(Request.QueryString.Item("nClaim"), eFunctions.Values.eTypeData.etdDouble)) Then
		Response.Write("top.fraHeader.document.forms[0].tcnPolicy.value = '" & lclsClaim.nPolicy & "';")
		Response.Write("top.fraHeader.ReloadPage();")
	End If
	lclsClaim = Nothing
End Sub

'% UpdQuot_parts:Actualiza el campo ssel de la tabla Quot_parts 
'-------------------------------------------------------------------------------------------- 
Private Sub UpdQuot_parts()
	'-------------------------------------------------------------------------------------------- 
	
	Dim lobjClaim As eClaim.Quot_parts
	lobjClaim = New eClaim.Quot_parts
	
	Call lobjClaim.Update(mobjValues.StringToType(Request.QueryString.Item("nServ_order"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nId"), eFunctions.Values.eTypeData.etdDouble), Request.QueryString.Item("sSel"), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
	
	lobjClaim = Nothing
End Sub

'% InsQuot_Auto: Actualiza el campo ssel de la tabla Quot_Auto 
'-------------------------------------------------------------------------------------------- 
Private Sub InsQuot_Auto()
	'-------------------------------------------------------------------------------------------- 
	Dim lclsQuot_Auto As eClaim.Quot_Auto
	lclsQuot_Auto = New eClaim.Quot_Auto
	
	Call lclsQuot_Auto.UpdateSel(mobjValues.StringToType(Request.QueryString.Item("nServ_ord"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nId"), eFunctions.Values.eTypeData.etdDouble), Request.QueryString.Item("sSel"), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
	
	lclsQuot_Auto = Nothing
End Sub

'% UpdateCase:Actualiza el combo de los Casos 
'-------------------------------------------------------------------------------------------- 
Private Sub UpdateCase()
	'-------------------------------------------------------------------------------------------- 
	
	Dim lobjTables As eFunctions.Tables
	lobjTables = New eFunctions.Tables
	
	lobjTables.Parameters.Add("nClaim", Request.QueryString.Item("nClaim"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	
	
	If lobjTables.reaTable("tabClaim_cases") Then
		Response.Write("top.fraHeader.document.forms[0].cbeCase.disabled=false;")
		Response.Write("top.fraHeader.document.forms[0].cbeCase.options.length=0;")
		Do While Not lobjTables.EOF
			Response.Write("var option = new Option('" & lobjTables.Fields("sDescript") & "','" & lobjTables.Fields("sKey") & "');")
			
			Response.Write("top.fraHeader.document.forms[0].cbeCase.options.add(option," & lobjTables.Fields("sKey") & ");")
			
			lobjTables.NextRecord()
		Loop 
	Else
		Response.Write("top.fraHeader.document.forms[0].cbeCase.options.length=0;")
		Response.Write("top.fraHeader.document.forms[0].cbeCase.disabled=true;")
		Response.Write("top.fraHeader.document.forms[0].tcnCase_num.value=-32768;")
		Response.Write("top.fraHeader.document.forms[0].tcnDeman_Type.value=-32768;")
	End If
	
	'+Se asigna los paramétros al campo "valmovement" 
	Response.Write("if(top.fraHeader.document.forms[0].cbeCase.value!=''){ ")
	Response.Write("var tcnCaseNum = top.fraHeader.document.forms[0].cbeCase.value;")
	Response.Write("var tcnCaseNum = tcnCaseNum.indexOf('/');")
	Response.Write("var lstrCampo  = top.fraHeader.document.forms[0].cbeCase.value;")
	Response.Write("var lstrStart=lstrCampo.indexOf('/');")
	Response.Write("var lstrCampo1 = lstrCampo.substring(lstrStart+1,lstrCampo.length);")
	Response.Write("var lstrStart1 = lstrCampo1.indexOf('/');")
	Response.Write("var lstrDemanType = unescape(lstrCampo1.substring(0,lstrStart1));")
	Response.Write("top.fraHeader.document.forms[0].valMovement.Parameters.Param1.sValue = top.fraHeader.document.forms[0].tcnClaim.value;")
	
	Response.Write("top.fraHeader.document.forms[0].valMovement.Parameters.Param2.sValue = tcnCaseNum;")
	Response.Write("top.fraHeader.document.forms[0].tcnCase_num.value = tcnCaseNum;")
	Response.Write("top.fraHeader.document.forms[0].tcnDeman_Type.value = lstrDemanType;")
	Response.Write("top.fraHeader.document.forms[0].valMovement.disabled=false;")
	Response.Write("top.fraHeader.document.forms[0].btnvalMovement.disabled=false;")
	Response.Write("top.fraHeader.document.forms[0].valMovement.value='';")
	Response.Write("top.fraHeader.$('#valMovement').change();")
	Response.Write("}else{")
	Response.Write("top.fraHeader.document.forms[0].valMovement.disabled=true;")
	Response.Write("top.fraHeader.document.forms[0].btnvalMovement.disabled=true;")
	Response.Write("}")
	lobjTables = Nothing
End Sub

'% insChangeTotalLoss: se habilita/deshabilita el campo Pérdida total de la SI004 
'%                       Se utiliza para el campo Causa de la página SI004.aspx 
'-------------------------------------------------------------------------------------------- 
Private Sub insChangeTotalLoss()
	'-------------------------------------------------------------------------------------------- 
	Dim lclsClaim_caus As eClaim.Claim_caus
	Dim lclsProduct As eProduct.Product
	
	lclsClaim_caus = New eClaim.Claim_caus
	lclsProduct = New eProduct.Product
	
	'+ Si el tipo de ramo es de vida y la causa del siniestro es muerte (1), muerte violenta (2) 
	'+ o suicidio o si el tipo de ramo es automóvil y la causa es robo (1), se marca el tipo de pérdida como 
	'+ total y no se deja modificar su contenido 
	If CStr(Session("nBranch")) = "" Then
		Session("nBranch") = Request.QueryString.Item("nBranch")
	End If
	If CStr(Session("nProduct")) = "" Then
		Session("nProduct") = Request.QueryString.Item("nProduct")
	End If
	With Response
		If lclsClaim_caus.Find(Session("nBranch"), Session("nProduct"), mobjValues.StringToType(Request.QueryString.Item("nClaimCaus"), eFunctions.Values.eTypeData.etdDouble)) Then
			
			If lclsClaim_caus.sClaimtyp = "2" Then
				.Write("top.fraFolder.document.forms[0].chkTotalLoss.checked=true;")
				.Write("top.fraFolder.document.forms[0].chkTotalLoss.disabled=true;")
			Else
				If lclsClaim_caus.sClaimtyp = "1" Then
					.Write("top.fraFolder.document.forms[0].chkTotalLoss.checked=false;")
					.Write("top.fraFolder.document.forms[0].chkTotalLoss.disabled=true;")
				End If
			End If
			
			If lclsProduct.Find(Session("nBranch"), Session("nProduct"), Session("dEffecdate")) Then
				Session("sBrancht") = lclsProduct.sBrancht
				If lclsProduct.sBrancht = eProduct.Product.pmBrancht.pmlife Or lclsProduct.sBrancht = eProduct.Product.pmBrancht.pmAuto Then
					If lclsClaim_caus.sClaimtyp = "3" Then
						.Write("top.fraFolder.document.forms[0].chkTotalLoss.disabled=false;")
						.Write("top.fraFolder.document.forms[0].chkTotalLoss.checked=false;")
					End If
				Else
					If (lclsProduct.sBrancht = eProduct.Product.pmBrancht.pmlife Or lclsProduct.sBrancht = 2) And (Request.QueryString.Item("nClaimCaus") = "3" Or Request.QueryString.Item("nClaimCaus") = "4") Then
						.Write("top.fraFolder.document.forms[0].chkTotalLoss.disabled=true;")
						.Write("top.fraFolder.document.forms[0].chkTotalLoss.checked=true;")
					Else
						.Write("top.fraFolder.document.forms[0].chkTotalLoss.disabled=false;")
						.Write("top.fraFolder.document.forms[0].chkTotalLoss.checked=false;")
					End If
				End If
			End If
		End If
	End With
	
	lclsClaim_caus = Nothing
	lclsProduct = Nothing
End Sub

'%insEnabledCertif: Habilita/inhabilita el campo "Item" según el tipo de póliza (Tx. SI737) 
'---------------------------------------------------------------------------------------------------- 
Private Sub insEnabledCertif()
	'---------------------------------------------------------------------------------------------------- 
	Dim lclsPolicy As ePolicy.Policy
	lclsPolicy = New ePolicy.Policy
	
	If lclsPolicy.Find("2", mobjValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdInteger), _
                        mobjValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), _
                        mobjValues.StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble), _
                        True) Then
		
		If lclsPolicy.sPolitype <> "1" Then
			Response.Write("top.fraFolder.document.forms[0].tcnCertif.disabled = false;")
		Else
			Response.Write("top.fraFolder.document.forms[0].tcnCertif.disabled = true;")
		End If
		Response.Write("top.fraFolder.document.forms[0].tctCodeAseg.disabled = false;")
		Response.Write("top.fraFolder.document.forms[0].tctCodeAseg_Digit.disabled = false;")
		Response.Write("top.fraFolder.document.forms[0].btntctCodeAseg.disabled = false;")
	End If
	
	lclsPolicy = Nothing
End Sub

'%InsConvertAmountRev: 
'-------------------------------------------------------------------------------------------- 
Private Sub InsConvertAmountRev()
	'-------------------------------------------------------------------------------------------- 
	Dim lclsConvert As eGeneral.Exchange
	Dim ldblCurAmount As Object
	Dim nAmount As Object
	Dim nCurrency As Byte
	Dim nPolicyCurr As Byte
	Dim ldblExchange As Object
	Dim dValdate As Object
	
	nAmount = mobjValues.StringToType(Request.QueryString.Item("nAmount"), eFunctions.Values.eTypeData.etdDouble)
	nCurrency = mobjValues.StringToType(Request.QueryString.Item("nCurrency"), eFunctions.Values.eTypeData.etdDouble)
	nPolicyCurr = mobjValues.StringToType(Request.QueryString.Item("nPolicyCurr"), eFunctions.Values.eTypeData.etdDouble)
	dValdate = mobjValues.StringToType(Request.QueryString.Item("dValdate"), eFunctions.Values.eTypeData.etdDate)
	
	lclsConvert = New eGeneral.Exchange
	
	ldblCurAmount = nAmount
	If nCurrency > 0 And nPolicyCurr > 0 Then
		If nCurrency <> nPolicyCurr Then
			Call lclsConvert.Convert(0, nAmount, nCurrency, nPolicyCurr, dValdate, 0)
			ldblCurAmount = lclsConvert.pdblResult
		End If
		
		Call lclsConvert.Find(mobjValues.StringToType(Request.QueryString.Item("nPolicyCurr"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("dValdate"), eFunctions.Values.eTypeData.etdDate))
		ldblExchange = lclsConvert.nExchange
		
		If lclsConvert.Find(mobjValues.StringToType(Request.QueryString.Item("nCurrency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("dValdate"), eFunctions.Values.eTypeData.etdDate)) Then
			Response.Write("top.fraFolder.document.forms[0].tcnExchange.value = '" & mobjValues.TypeToString(ldblExchange / lclsConvert.nExchange, eFunctions.Values.eTypeData.etdDouble, True, 6) & "';")
			
		Else
			Response.Write("top.fraFolder.document.forms[0].tcnExchange.value = '1';")
		End If
	End If
	
	Response.Write("top.fraFolder.document.forms[0].tcnAmountPay.value = '" & mobjValues.TypeToString(ldblCurAmount, eFunctions.Values.eTypeData.etdDouble, True, 6) & "';")
	
	lclsConvert = Nothing
End Sub



'%InsCalAmountPay: 
'-------------------------------------------------------------------------------------------- 
Private Sub InsCalAmountPay()
	'-------------------------------------------------------------------------------------------- 
	Dim ldblAmount As Double
	Dim lintCurrency As Integer
	Dim lintPolicyCurr As Integer
	Dim dValdate As Date
	
	ldblAmount = mobjValues.StringToType(Request.QueryString.Item("nAmount"), eFunctions.Values.eTypeData.etdDouble)
	lintCurrency = mobjValues.StringToType(Request.QueryString.Item("nCurrency"), eFunctions.Values.eTypeData.etdInteger)
	lintPolicyCurr = mobjValues.StringToType(Request.QueryString.Item("nPolicyCurr"), eFunctions.Values.eTypeData.etdInteger)
	dValdate = mobjValues.StringToType(Request.QueryString.Item("dValdate"), eFunctions.Values.eTypeData.etdDate)
	
	Call InsConvertAmount(ldblAmount, lintCurrency, lintPolicyCurr, dValdate)
End Sub

'%InsConvertAmount: 
'-------------------------------------------------------------------------------------------- 
Private Sub InsConvertAmount(ByRef nAmount As Object, ByRef nCurrency As Byte, ByRef nPolicyCurr As Byte, ByRef dValdate As Object)
	'-------------------------------------------------------------------------------------------- 
	Dim lclsConvert As eGeneral.Exchange
	Dim ldblCurAmount As Object
	Dim ldblExchange As Object
	lclsConvert = New eGeneral.Exchange
	
	ldblCurAmount = nAmount
	If nCurrency > 0 And nPolicyCurr > 0 Then
		If nCurrency <> nPolicyCurr Then
			Call lclsConvert.Convert(0, nAmount, nPolicyCurr, nCurrency, dValdate, 0)
			ldblCurAmount = lclsConvert.pdblResult
		End If
		Call lclsConvert.Find(mobjValues.StringToType(Request.QueryString.Item("nPolicyCurr"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("dValdate"), eFunctions.Values.eTypeData.etdDate))
		ldblExchange = lclsConvert.nExchange
		If lclsConvert.Find(mobjValues.StringToType(Request.QueryString.Item("nCurrency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("dValdate"), eFunctions.Values.eTypeData.etdDate)) Then
			
			Response.Write("top.fraFolder.document.forms[0].tcnExchange.value = '" & mobjValues.TypeToString(ldblExchange / lclsConvert.nExchange, eFunctions.Values.eTypeData.etdDouble, True, 6) & "';")
			
		Else
			Response.Write("top.fraFolder.document.forms[0].tcnExchange.value = '1';")
		End If
	Else
		Response.Write("top.fraFolder.document.forms[0].tcnExchange.value = '0';")
	End If
	
	Response.Write("top.fraFolder.document.forms[0].tcnAmountPayCurrPay.value = '" & mobjValues.TypeToString(ldblCurAmount, eFunctions.Values.eTypeData.etdDouble, True, 6) & "';")
	
	lclsConvert = Nothing
End Sub

'%ExpandCodeRut: Asigna el valor expandido del código de RUT del taller (SI775) 
'-------------------------------------------------------------------------------------------------- 
Private Sub ExpandCodeRut()
	'-------------------------------------------------------------------------------------------------- 
	
	Dim lstrClient As String
	Dim lclsClient As eClient.Client
	lclsClient = New eClient.Client
	
	lstrClient = lclsClient.ExpandCode(Request.QueryString.Item("sWorkshClient"))
	Response.Write("top.fraFolder.document.forms[0].cbeWorkshClient.value ='" & lstrClient & "';")
	
	lclsClient = Nothing
End Sub

'% insShowCertif: Habilita o deshabilita el campo nCertif dependiendo del tipo de póliza pasada como parámetro. 
'-------------------------------------------------------------------------------------------- 
Private Sub insShowCertif()
	'-------------------------------------------------------------------------------------------- 
	Dim lclsPolicy As ePolicy.Policy
	lclsPolicy = New ePolicy.Policy
	With Response
		.Write("with(top.fraHeader.document.forms[0]){")
		If lclsPolicy.FindPolicybyPolicy("2", mobjValues.StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble)) Then
			
			.Write("cbeBranch.value=" & lclsPolicy.nBranch & ";")
			.Write("valProduct.Parameters.Param1.sValue=" & lclsPolicy.nBranch & ";")
			.Write("valProduct.value=" & lclsPolicy.nProduct & ";")
			.Write("valProduct.disabled=false;")
			.Write("top.fraHeader.$('#valProduct').change();")
			.Write("valProduct.disabled=true;")
			'				If lclsPolicy.sPolitype = "1" Or ( lclsPolicy.sPolitype = "2" And lclsPolicy.sColinvot = "2" ) Then
			If lclsPolicy.sPolitype = "1" Then
				Response.Write("tcnCertif.disabled=true;")
				Response.Write("tcnCertif.value='0';")
			Else
				Response.Write("tcnCertif.disabled=false;")
				Response.Write("tcnCertif.value='';")
			End If
			Response.Write("tcdDate_origi.value='" & lclsPolicy.dDate_origi & "';")
		Else
			.Write("cbeBranch.value='';")
			.Write("valProduct.Parameters.Param1.sValue=0;")
			.Write("valProduct.value='';")
			.Write("UpdateDiv('valProductDesc', '');")
			.Write("tcnCertif.disabled=true;")
			.Write("tcnCertif.value='';")
		End If
		.Write("}")
	End With
	lclsPolicy = Nothing
End Sub

'% insShowIVA: se busca el valor correspondiente al IVA para una fecha 
'-------------------------------------------------------------------------------------------- 
Private Sub insShowIVA()
	'-------------------------------------------------------------------------------------------- 
	Dim lclsTax_Fixval As eAgent.Tax_Fixval
	lclsTax_Fixval = New eAgent.Tax_Fixval
	
	'+ Se obtiene el porcentaje fijo de IVA (Tabla Tax_Fixval) 
	If lclsTax_Fixval.Find(1, mobjValues.StringToType(Request.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate)) Then
		Response.Write("top.fraFolder.document.forms[0].tcnIVA.value='" & mobjValues.StringToType(lclsTax_Fixval.nPercent, eFunctions.Values.eTypeData.etdDouble, True) & "';")
		
		Response.Write("top.fraFolder.$('#tcnAmount_Labor').change();")
	End If
	lclsTax_Fixval = Nothing
End Sub

'% insShowClientConSI831: se busca los datos del client de la transacción si831 
'-------------------------------------------------------------------------------------------- 
Private Sub insShowClientConSI831()
	'-------------------------------------------------------------------------------------------- 
	Dim lclsClient As eClient.Client
	Dim lclsBuy_Auto As eClaim.Buy_Auto
	Dim lclsAddress As eGeneralForm.Address
	Dim lclsAddresss As Object
	
	Dim lstrClient As String
	Dim skeyaddress As String
	Dim mstrDescadd As String
	Dim mstrMunicipality As String
	Dim mstrPhone As String
	
	lstrClient = vbNullString
	mstrDescadd = vbNullString
	mstrPhone = vbNullString
	mstrMunicipality = vbNullString
	lclsClient = New eClient.Client
	If lclsClient.Find(Request.QueryString.Item("sClient")) Then
		lstrClient = lclsClient.sCliename
		lclsAddress = New eGeneralForm.Address
		skeyaddress = "2" & Request.QueryString.Item("sClient")

		If lclsAddress.Find(skeyaddress, 2, Today,  , True) Then
			mstrDescadd = Mid(lclsAddress.sDescadd, 1, 50)
			mstrMunicipality = mobjValues.TypeToString(lclsAddress.nMunicipality, eFunctions.Values.eTypeData.etdLong)
			lclsBuy_Auto = New eClaim.Buy_Auto

			If lclsBuy_Auto.FindPhone(skeyaddress, 1, 2, Today) Then
				If lclsBuy_Auto.nArea_code = eRemoteDB.Constants.intNull Then
					mstrPhone = lclsBuy_Auto.sPhone
				Else
					mstrPhone = mobjValues.TypeToString(lclsBuy_Auto.nArea_code, eFunctions.Values.eTypeData.etdLong) & "-" & lclsBuy_Auto.sPhone
				End If
				mstrPhone = Mid(mstrPhone, 1, 11)
			End If
			lclsBuy_Auto = Nothing
		End If
		lclsAddress = Nothing
	End If
	lclsClient = Nothing
	
	Response.Write("top.fraFolder.document.forms[0].tctNombreCon.value='" & lstrClient & "';")
	Response.Write("top.fraFolder.document.forms[0].tctAdd_Contact.value='" & mstrDescadd & "';")
	Response.Write("top.fraFolder.document.forms[0].tctAdd_Contact.value='" & mstrDescadd & "';")
	If mstrMunicipality = vbNullString Then
		Response.Write("top.fraFolder.document.forms[0].cbeProvince.value='" & vbNullString & "';")
		Response.Write("top.fraFolder.document.forms[0].valLocal.value='" & vbNullString & "';")
	End If
	Response.Write("top.fraFolder.document.forms[0].valMunicipality.value='" & mstrMunicipality & "';")
	Response.Write("top.fraFolder.document.forms[0].tctPhone_Cont.value='" & mstrPhone & "';")
	
	If mstrMunicipality <> vbNullString Then
		Response.Write("top.fraFolder.$('#valMunicipality').change();")
		'           Response.Write "top.fraFolder.document.forms[0].valMunicipality.disabled = true;" 
	End If
End Sub

'% insShowClientConSI831: se busca los datos del client de la transacción si831 
'-------------------------------------------------------------------------------------------- 
Private Sub showPolicy()
	Dim mintProduct As Object
	Dim mintBranch As Object
	'-------------------------------------------------------------------------------------------- 
	Dim lclsPolicy As ePolicy.Policy
	Dim lclsRoles As ePolicy.Roles
	
	lclsPolicy = New ePolicy.Policy
	lclsRoles = New ePolicy.Roles
	
	With lclsPolicy
            'If .Find_DatPolicy("2", mobjValues.StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble)) Then
            If .Find("2", mobjValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble)) Then
                mintBranch = lclsPolicy.nBranch
                mintProduct = lclsPolicy.nProduct
                If (CDbl(Request.QueryString.Item("nBranch")) = 0) Or (Request.QueryString.Item("nBranch") <> lclsPolicy.nBranch) Then
                    Response.Write("top.fraHeader.document.forms[0].cbeBranch.value='" & lclsPolicy.nBranch & "';")
                    Response.Write("top.fraHeader.document.forms[0].cbeBranch.onchange();")
                End If
			
                If (Trim(Request.QueryString.Item("nProduct")) = "") Or (mobjValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble) <> lclsPolicy.nProduct) Then
                    Response.Write("top.fraHeader.document.forms[0].valProduct.value='" & lclsPolicy.nProduct & "';")
                    Response.Write("top.fraHeader.$('#valProduct').change();")
                End If
			
                If lclsRoles.Find("2", mobjValues.StringToType(lclsPolicy.nBranch, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(lclsPolicy.nProduct, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble), 0, 1, "", Today, True) Then
				
                    Response.Write("top.fraHeader.document.forms[0].tctClient.value='" & lclsRoles.SCLIENT & "';")
                    Response.Write("top.fraHeader.document.forms[0].tctClient.disabled=true;")
                    Response.Write("top.fraHeader.document.forms[0].tctClient_Digit.value='" & lclsRoles.sDigit & "';")
                    Response.Write("top.fraHeader.document.forms[0].tctClient_Digit.disabled=true;")
                    Response.Write("top.fraHeader.UpdateDiv('tctCliename','" & lclsRoles.sCliename & "');")
                End If
            Else
                Response.Write("top.fraHeader.document.forms[0].tctClient.value='" & String.Empty & "';")
                Response.Write("top.fraHeader.document.forms[0].tctClient_Digit.value='" & String.Empty & "';")
                Response.Write("top.fraHeader.UpdateDiv('tctCliename','" & String.Empty & "');")
            End If
        End With
	
	lclsRoles = Nothing
	
End Sub
'% insShowClaimData: Muestra los datos del siniestro.
'%                   Se utiliza para el campo Siniestro de la página SI001_K.aspx
'-------------------------------------------------------------------------------------------- 
Private Sub insShowPolicyData()
	'--------------------------------------------------------------------------------------------   
	Dim lclsPolicy_shw As ePolicy.Policy
	Dim lclsRoles As ePolicy.Roles
	
	lclsRoles = New ePolicy.Roles
	lclsPolicy_shw = New ePolicy.Policy
	
	If lclsPolicy_shw.Find("2", mobjValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble), True) Then
		With Response
			.Write("top.fraHeader.document.forms[0].cbeBranch.value=" & lclsPolicy_shw.nBranch & ";")
			.Write("top.fraHeader.document.forms[0].valProduct.Parameters.Param1.sValue =" & lclsPolicy_shw.nBranch & ";")
			.Write("top.fraHeader.document.forms[0].valProduct.value=" & lclsPolicy_shw.nProduct & ";")
			.Write("top.fraHeader.$('#valProduct').change();")
			.Write("top.fraHeader.document.forms[0].tcnPolicy.value=" & lclsPolicy_shw.nPolicy & ";")
			
			If lclsRoles.Find("2", mobjValues.StringToType(lclsPolicy_shw.nBranch, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(lclsPolicy_shw.nProduct, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(lclsPolicy_shw.nPolicy, eFunctions.Values.eTypeData.etdDouble), 0, 1, "", mobjValues.StringToType(Request.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate), True) Then
				
				Response.Write("top.fraHeader.document.forms[0].tctClientCollect.value='" & lclsRoles.sClient & "';")
				Response.Write("top.fraHeader.document.forms[0].tctClientCollect.disabled=true;")
				Response.Write("top.fraHeader.document.forms[0].tctClientCollect_Digit.value='" & lclsRoles.sDigit & "';")
				Response.Write("top.fraHeader.document.forms[0].tctClientCollect_Digit.disabled=true;")
				Response.Write("top.fraHeader.UpdateDiv('lblCliename','" & lclsRoles.sCliename & "');")
			End If
		End With
	End If
	
	lclsRoles = Nothing
	lclsPolicy_shw = Nothing
End Sub

</script>
<%Response.Expires = -1
mobjValues = New eFunctions.Values
%> 
<HTML> 
<HEAD> 
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT> 

 
</HEAD> 
<BODY> 
        <FORM NAME="ShowValue"> 
        </FORM> 
</BODY> 
</HTML> 
<%Response.Write("<SCRIPT>")
Select Case Request.QueryString.Item("Field")
	Case "ShowCertif"
		insShowCertif()
	Case "Exchange"
		Call insShowExchange()
	Case "Exchange_1"
		Call insShowExchange_1()
	Case "Cover"
		Call insParamToConcept()
	Case "PayType", "Currency"
		Call insDeletePayCla()
	Case "Client"
		Call insExpandCodeClient()
	Case "Role"
		Call insReaClientRole()
	Case "DP051_Claim"
		Call insDP051_Claim()
	Case "SI021"
		Call insSI021()
	Case "cbeZone"
		Call insProvider()
	Case "Claim"
		Call insSI008_K()
	Case "Demandant"
		Call insShowDemandat()
	Case "ServiceOrder"
		Call ShowServiceOrderData()
	Case "Client_rep"
		Call insReaClient_rep()
	Case "Account"
		Call ShowAccount()
	Case "ClientCover"
		Call ShowProvider()
	Case "Relation"
		Call ShowRelation()
	Case "CurrenPol"
		Call ShowCurrency()
	Case "Brancht"
		Call ShowBrancht()
	Case "SIC001"
		If Request.QueryString.Item("nPolicy") <> eRemoteDB.Constants.intNull Then
			Call PoliType()
		End If
	Case "Claim_SI774"
		Call Claim_SI774()
	Case "Claim_SI830"
		Call Claim_SI830()
	Case "Claim_SI831"
		Call Claim_SI830()
	Case "UpdateCase"
		Call UpdateCase()
	Case "Quot_parts"
		Call UpdQuot_parts()
	Case "ClaimCaus"
		Call insChangeTotalLoss()
	Case "Policy"
		Call insEnabledCertif()
	Case "AmountPay"
		Call InsCalAmountPay()
	Case "WorkShop"
		Call ExpandCodeRut()
	Case "tcnAmountPayCurrPay"
		Call InsConvertAmountRev()
	Case "ShowIVA"
		Call insShowIVA()
	Case "Quot_Auto"
		Call InsQuot_Auto()
	Case "ClientConSI831"
		Call insShowClientConSI831()
	Case "ClaimClient"
		Call ClaimClient()
	Case "ShowClaim"
		Call insFindDataClaim()
	Case "onChangePolicy"
		Call showPolicy()
	Case "ClaimData"
		Call insShowPolicyData()
End Select

Response.Write(mobjValues.CloseShowDefValues(Request.QueryString.Item("sFrameCaller")))
Response.Write("</SCRIPT>")

mobjValues = Nothing

%> 

   

