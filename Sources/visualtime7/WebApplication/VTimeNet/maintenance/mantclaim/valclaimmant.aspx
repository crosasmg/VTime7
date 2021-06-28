<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eClaim" %>
<%@ Import namespace="eBranches" %>
<script language="VB" runat="Server">

'+ Se define la variable para el pase de valores a los campos de encabezado

Dim mstrErrors As String
Dim mobjValues As eFunctions.Values
Dim mstrString As String
Dim mobjMantClaim As Object
Dim mstrQueryString As String
Dim mintBranch As Object

'+ Se define la contante para el manejo de errores en caso de advertencias

Dim mstrCommand As String


'% insvalMantClaim: Se realizan las validaciones masivas de la forma	
'--------------------------------------------------------------------------------------------
Function insvalMantClaim() As String
	'--------------------------------------------------------------------------------------------
	Select Case Request.QueryString.Item("sCodispl")
		
		Case "MSI001"
			mobjMantClaim = New eClaim.Tab_wincla
			With Request
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					insvalMantClaim = mobjMantClaim.insValSI001_K(.QueryString("Action"), mobjValues.StringToType(.Form.Item("optBussines"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeBrancht"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeTranType"), eFunctions.Values.eTypeData.etdDouble))
				Else
					insvalMantClaim = mobjMantClaim.insValSI001(.QueryString("sCodispl"), .Form.GetValues("Sel").Length)
				End If
			End With
			
			'+MSI010: Tabla de causas de siniestro
		Case "MSI010"
			If CDbl(Request.QueryString.Item("nZone")) = 1 Then
				mobjMantClaim = New eClaim.Claim_caus
				With Request
					insvalMantClaim = mobjMantClaim.insValMSI010_K(.QueryString("sCodispl"), mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble))
				End With
			Else
				If Request.QueryString.Item("WindowType") = "PopUp" Then
					mobjMantClaim = New eClaim.Claim_caus
					With Request
						insvalMantClaim = mobjMantClaim.insValMSI010(.QueryString("sCodispl"), .Form.Item("sAction"), 1, mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCausecod"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctDescript"), .Form.Item("tctShort_des"), .Form.Item("chkPart_loss"), .Form.Item("chkTotal_loss"), .Form.Item("cbeStatregt"))
					End With
				Else
					insvalMantClaim = vbNullString
				End If
			End If
			mobjMantClaim = Nothing
			
			'+ MSI011 Tabla de Proveedores
		Case "MSI011"
			mobjMantClaim = New eClaim.Tab_Provider
			With Request
				If .QueryString.Item("WindowType") = "PopUp" And .QueryString.Item("nMainAction") <> CStr(eFunctions.Menues.TypeActions.clngActionQuery) Then
					insvalMantClaim = mobjMantClaim.insValMSI011_K(.QueryString("sCodispl"), .QueryString("Action"), mobjValues.StringToType(.Form.Item("tcnCodigo"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeTypProvider"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("dtcClient"), mobjValues.StringToDate(.Form.Item("tcdDateInitial")), mobjValues.StringToDate(.Form.Item("tcdDateEnd")), .Form.Item("cbeState"), mobjValues.StringToType(.Form.Item("cbenOffice"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnMax_serv_ord"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbenTypeSupport"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPer_disc"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("chkConcesionary"), Session("nUsercode"))
				Else
					insvalMantClaim = vbNullString
				End If
			End With
			
			'+MSI014: Tabla de daños posibles 
		Case "MSI014"
			mobjMantClaim = New eBranches.Tab_damage
			If CDbl(Request.QueryString.Item("nZone")) = 1 Then
				With Request
					insvalMantClaim = mobjMantClaim.insValMSI014_K(.QueryString("sCodispl"), mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("cbeBranch"))
				End With
			Else
				If Request.QueryString.Item("WindowType") = "PopUp" Then
					mobjMantClaim = New eBranches.Tab_damage
					With Request
						insvalMantClaim = mobjMantClaim.insValMSI014(.QueryString("sCodispl"), .Form.Item("sAction"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnDamage_cod"), eFunctions.Values.eTypeData.etdDouble, True), .Form.Item("tctDescript"), .Form.Item("tctShort_des"), .Form.Item("cbeStatregt"))
					End With
				Else
					insvalMantClaim = vbNullString
				End If
			End If
			mobjMantClaim = Nothing
			
		Case "MSI015"
			mobjMantClaim = New eClaim.Tab_docu
			If CDbl(Request.QueryString.Item("nZone")) = 1 Then
				With Request
					insvalMantClaim = mobjMantClaim.insValMSI015_k(.QueryString("sCodispl"), mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble))
				End With
			Else
				'+ Si la acciòn es duplicar tabla
				With Request
					insvalMantClaim = mobjMantClaim.insValMSI015_k(.QueryString("sCodispl"), mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeBranchdes"), eFunctions.Values.eTypeData.etdDouble))
				End With
				
				If Request.QueryString.Item("WindowType") <> "PopUp" And insvalMantClaim = vbNullString Then
					With Request
						'+ Si la acciòn es duplicar tabla
						mobjMantClaim.nUsercode = Session("nUsercode")
						insvalMantClaim = mobjMantClaim.insDuplicarMSI015(mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nModulec"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCover"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCodcauscl"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeBranchdes"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeProductdes"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeModulecdes"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeCoverdes"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeCoscauscldes"), eFunctions.Values.eTypeData.etdDouble))
					End With
					
				End If
				
				If Request.QueryString.Item("WindowType") = "PopUp" Then
					mobjMantClaim = New eClaim.Tab_docu
					With Request
						mobjMantClaim.nBranch = Session("nBranch")
						insvalMantClaim = mobjMantClaim.insValMSI015(.QueryString("sCodispl"), .QueryString("Action"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nModulec"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCover"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("ncauscodcl"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tctnDoc_code"), eFunctions.Values.eTypeData.etdDouble, True), .Form.Item("sDescript"), .Form.Item("tctsShort_des"), .Form.Item("tctsStratregt"), mobjValues.StringToType(.Form.Item("tcnDays_presc"), eFunctions.Values.eTypeData.etdDouble))
					End With
				Else
					insvalMantClaim = vbNullString
				End If
			End If
			
		Case "MSI016"
			mobjMantClaim = New eClaim.Tab_ClaRevcond
			With Request
				If .QueryString.Item("WindowType") = "PopUp" And .QueryString.Item("nMainAction") <> CStr(eFunctions.Menues.TypeActions.clngActionQuery) Then
					insvalMantClaim = mobjMantClaim.insValMSI016_K(.QueryString("sCodispl"), mobjValues.StringToType(.Form.Item("cbeOper_type"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeGen_Opera"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("chkInd_rev"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbePay_ind"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeRec_esp_in"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeRecover_in"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeReserve_in"), eFunctions.Values.eTypeData.etdDouble), Session("nUsercode"), .QueryString("Action"))
				Else
					insvalMantClaim = vbNullString
				End If
			End With
			
			'+MSI019: Grupos Asociados a un Proveedor			
		Case "MSI019"
			insvalMantClaim = vbNullString
			
			'+MSI035: Ramos en los que participa un proveedor 
		Case "MSI035"
			insvalMantClaim = vbNullString
			
			'+MSI647: Zonas asociadas a un proveedor 
		Case "MSI647"
			mobjMantClaim = New eClaim.Tab_prov_zone
			With Request
				If .QueryString.Item("WindowType") = "PopUp" Then
					insvalMantClaim = mobjMantClaim.insValMSI647(.QueryString("sCodispl"), .QueryString("Action"), mobjValues.StringToType(Request.QueryString.Item("nProvider"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbenZone"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnOrder"), eFunctions.Values.eTypeData.etdDouble))
					
				Else
					insvalMantClaim = vbNullString
				End If
			End With
			
			'+ MOS661: Tipos de órdenes de servicios profesionales
		Case "MOS661"
			mobjMantClaim = New eClaim.Ord_type
			With Request
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					insvalMantClaim = mobjMantClaim.insValMOS661_k(.QueryString("sCodispl"), mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valCurrency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate))
				Else
					If .QueryString.Item("WindowType") = "PopUp" Then
						mstrQueryString = "&nCurrency=" & Request.Form.Item("valCurrency") & "&dEffecdate=" & Request.Form.Item("tcdEffecdate")
						
						insvalMantClaim = mobjMantClaim.insValMOS661(.QueryString("sCodispl"), Request.QueryString.Item("Action"), mobjValues.StringToType(.Form.Item("hddnCurrency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hdddEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("cbeOrd_typeCost"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnAmount"), eFunctions.Values.eTypeData.etdDouble))
					Else
						insvalMantClaim = vbNullString
					End If
				End If
			End With
			
			'+MSI559: Tabla de Fonasa 
		Case "MSI559"
			mobjMantClaim = New eClaim.Tab_Fonasa
			If CDbl(Request.QueryString.Item("nZone")) = 1 Then
				With Request
					insvalMantClaim = mobjMantClaim.insValMSI559_K("SI559", mobjValues.StringToType(.Form.Item("tcnYear"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeCurrency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate))
				End With
			Else
				With Request
					If .QueryString.Item("WindowType") = "PopUp" Then
						insvalMantClaim = mobjMantClaim.insValMSI559("MSI559", .QueryString("Action"), mobjValues.StringToType(Session("nYear"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCurrency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), .Form.Item("tctService"), .Form.Item("tctSubService"), mobjValues.StringToType(.Form.Item("tcnAmount"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctDescript"))
					Else
						insvalMantClaim = vbNullString
					End If
				End With
			End If
			
		Case Else
			insvalMantClaim = "insvalMantClaim: Código lógico no encontrado (" & Request.QueryString.Item("sCodispl") & ")"
	End Select
End Function

'% insPostMantClaim: Se realizan las actualizaciones a las tablas
'--------------------------------------------------------------------------------------------
Function insPostMantClaim() As Boolean
	Dim lstrClaimPay As String
	'--------------------------------------------------------------------------------------------
	Dim lblnPost As Boolean
	Dim lblnFirst As Boolean
	Dim lintFirst As Byte
	Dim lintIndex As Integer
	Dim lintCheck As Object
	
	lblnPost = False
	
	Select Case Request.QueryString.Item("sCodispl")
		
		'+MSI001: Secuencia de Ventanas para Siniestro
		Case "MSI001"
			With Request
				If .QueryString.Item("nMainAction") <> "401" And CDbl(.QueryString.Item("nZone")) <> 1 Then
					lintIndex = 0
					If Not IsNothing(.Form.Item("hddsSel")) Then
						For	Each lintCheck In .Form.GetValues("hddsSel")
							lintIndex = lintIndex + 1
							If lintCheck <> eRemoteDB.Constants.intNull Or lintCheck <> 0 Then
								lblnPost = mobjMantClaim.insPostSI001(mobjValues.StringToType(.QueryString.Item("nTraTypec"), eFunctions.Values.eTypeData.etdDouble), .QueryString("sBrancht"), .QueryString("sBussityp"), .Form.GetValues("hddsExist").GetValue(lintIndex - 1), .Form.GetValues("hddsSel").GetValue(lintIndex - 1), mobjValues.StringToType(.Form.GetValues("hddnSequence").GetValue(lintIndex - 1), eFunctions.Values.eTypeData.etdDouble), .Form.GetValues("hddsCodispl").GetValue(lintIndex - 1), "1", .Form.GetValues("hddsRequire").GetValue(lintIndex - 1), Session("nUsercode"))
							End If
						Next lintCheck
					End If
				Else
					mstrString = "&nTraTypec=" & Request.Form.Item("cbeTranType") & "&sBrancht=" & Request.Form.Item("cbeBrancht") & "&sBussityp=" & Request.Form.Item("optBussines")
					lblnPost = True
				End If
			End With
			
			'+MSI010: Tabla de causas de siniestro
		Case "MSI010"
			With Request
				mobjMantClaim = New eClaim.Claim_caus
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					'+ Se toma el ultimo valor consultado a ser duplicado 
					'+ y se guarda en la variable de sesion nLastBranch y nLastProduct
					If CDbl(.QueryString.Item("nMainAction")) = 401 Then
						Session("nLastBranch") = .Form.Item("cbeBranch")
						If mobjValues.StringToType(Request.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble) = eRemoteDB.Constants.intNull Then
							Session("nLastProduct") = 0
						Else
							Session("nLastProduct") = .Form.Item("valProduct")
						End If
					End If
					'+ Se toma el valor que se va al que se va a duplicar.
					Session("nBranch") = .Form.Item("cbeBranch")
					If mobjValues.StringToType(Request.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble) = eRemoteDB.Constants.intNull Then
						Session("nProduct") = 0
					Else
						Session("nProduct") = .Form.Item("valProduct")
					End If
					'+ Si la accion es duplicar
					If CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 306 Then
						lblnPost = mobjMantClaim.insDuplicarMSI010(Session("nLastBranch"), Session("nBranch"), Session("nProduct"), Session("nLastProduct"), mobjValues.StringToType(Session("nUserCode"), eFunctions.Values.eTypeData.etdDouble))
					Else
						lblnPost = True
					End If
				Else
					If .QueryString.Item("WindowType") = "PopUp" Then
						lblnPost = mobjMantClaim.insPostMSI010(.Form.Item("sAction"), 1, mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCausecod"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctDescript"), .Form.Item("tctShort_des"), .Form.Item("cbeStatregt"), .Form.Item("chkPart_loss"), .Form.Item("chkTotal_loss"), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
					Else
						lblnPost = True
						Session("nBranch") = .Form.Item("cbeBranch")
						Session("nProduct") = .Form.Item("valProduct")
					End If
				End If
				mobjMantClaim = Nothing
			End With
			
			'+MSI011: Tabla de Proveedores
		Case "MSI011"
			With Request
				mobjMantClaim = New eClaim.Tab_Provider
				If .QueryString.Item("WindowType") = "PopUp" Then
					If .QueryString.Item("WindowType") = "PopUp" And .QueryString.Item("nMainAction") <> CStr(eFunctions.Menues.TypeActions.clngActionQuery) Then
						lblnPost = mobjMantClaim.inspostMSI011_K(.QueryString("sCodispl"), .QueryString("Action"), mobjValues.StringToType(.Form.Item("tcnCodigo"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeTypProvider"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("dtcClient"), mobjValues.StringToDate(.Form.Item("tcdDateInitial")), mobjValues.StringToDate(.Form.Item("tcdDateEnd")), .Form.Item("cbeState"), mobjValues.StringToType(.Form.Item("cbenBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbenOffice"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnMax_serv_ord"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbenTypeSupport"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPer_disc"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("chkConcesionary"), Session("nUsercode"))
						
					Else
						lblnPost = True
					End If
				Else
					lblnPost = True
				End If
			End With
			
			'+MSI014: Tabla de daños ocasionados
		Case "MSI014"
			With Request
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					mobjMantClaim = New eBranches.Tab_damage
					
					'+ Se toma el ultimo valor consultado a ser duplicado 
					'+ y se guarda en la variable de sesion nLastBranch.
					If CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401 Then
						Session("nLastBranch") = .Form.Item("cbeBranch")
					End If
					'+ Se toma el valor que se va al que se va a duplicar.
					Session("nBranch") = .Form.Item("cbeBranch")
					'+ Si la accion es duplicar
					If CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 306 Then
						lblnPost = mobjMantClaim.insDuplicarMSI014(Session("nLastBranch"), Session("nBranch"), mobjValues.StringToType(Session("nUserCode"), eFunctions.Values.eTypeData.etdDouble))
					Else
						lblnPost = True
					End If
				Else
					If .QueryString.Item("WindowType") = "PopUp" Then
						mobjMantClaim = New eBranches.Tab_damage
						lblnPost = mobjMantClaim.insPostMSI014(.Form.Item("sAction"), 1, mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnDamage_cod"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctDescript"), .Form.Item("tctShort_des"), .Form.Item("cbeStatregt"), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
					Else
						lblnPost = True
					End If
				End If
				mobjMantClaim = Nothing
			End With
			
			'+MSI015: Tabla de Documentos de Siniestros
		Case "MSI015"
			If CDbl(Request.QueryString.Item("nZone")) = 1 Then
				lblnPost = True
				Session("nBranch") = Request.Form.Item("cbeBranch")
				Session("nProduct") = Request.Form.Item("valproduct")
				Session("nModulec") = Request.Form.Item("cbeModulec")
				Session("nCover") = Request.Form.Item("cbeCover")
				Session("ncauscodcl") = Request.Form.Item("cbeCauscodcl")
			Else
				If Request.QueryString.Item("WindowType") = "PopUp" Then
					mobjMantClaim = New eClaim.Tab_docu
					
					If Request.Form.Item("chkClaimPay") = vbNullString Then
						lstrClaimPay = "0"
					Else
						lstrClaimPay = "1"
					End If
					
					
					With Request
						lblnPost = mobjMantClaim.insPostMSI015(.QueryString("sCodispl"), .QueryString("Action"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nModulec"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCover"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCauscodcl"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctnDoc_code"), lstrClaimPay, .Form.Item("sDescript"), .Form.Item("tctsShort_des"), .Form.Item("tctsStratregt"), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnDays_presc"), eFunctions.Values.eTypeData.etdDouble))
						
						
						
					End With
				Else
					lblnPost = True
				End If
				Session("nBranch") = Session("nBranch")
			End If
			
			'+MSI016: Tabla de Documentos de Siniestros			
		Case "MSI016"
			With Request
				mobjMantClaim = New eClaim.Tab_ClaRevcond
				If .QueryString.Item("WindowType") = "PopUp" Then
					If .QueryString.Item("WindowType") = "PopUp" And .QueryString.Item("nMainAction") <> CStr(eFunctions.Menues.TypeActions.clngActionQuery) Then
						
						lblnPost = mobjMantClaim.insPostMSI016_K(.QueryString("sCodispl"), .QueryString("Action"), mobjValues.StringToType(.Form.Item("cbeOper_type"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeGen_Opera"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("chkInd_rev"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbePay_ind"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeRec_esp_in"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeRecover_in"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeReserve_in"), eFunctions.Values.eTypeData.etdDouble), Session("nUsercode"))
						
					Else
						lblnPost = True
					End If
				Else
					lblnPost = True
				End If
			End With
			
			'+MSI019: Grupos Asociados a un Proveedor			
		Case "MSI019"
			lblnPost = True
			
			'+MSI035: Ramos en los que participa un proveedor 
		Case "MSI035"
			With Request
				mobjMantClaim = New eClaim.Tab_Provider
				lblnFirst = True
				
				For lintIndex = 1 To .Form.Item("tcnCheck").Length
					If .Form.GetValues("tcnCheck").GetValue(lintIndex - 1) = "1" Then
						If lblnFirst Then
							lintFirst = 1
							lblnFirst = False
						Else
							lintFirst = 2
						End If
						lblnPost = mobjMantClaim.Cre_Prov_Branch(mobjValues.StringToType(CStr(lintFirst), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nTypeProv"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nProvider"), eFunctions.Values.eTypeData.etdDouble), .QueryString("sClient"), mobjValues.StringToType(.Form.GetValues("tcnBranch").GetValue(lintIndex - 1), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
						
						
					Else
						lblnPost = True
					End If
				Next 
			End With
			
			
			'+MSI647: Zonas asociadas a un proveedor			
		Case "MSI647"
			With Request
				mobjMantClaim = New eClaim.Tab_prov_zone
				If .QueryString.Item("WindowType") = "PopUp" Then
					lblnPost = mobjMantClaim.insPostMSI647Upd(.QueryString("Action"), mobjValues.StringToType(Request.QueryString.Item("nProvider"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbenZone"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnOrder"), eFunctions.Values.eTypeData.etdDouble), Session("nUsercode"))
				Else
					lblnPost = True
				End If
			End With
			
			'+ MOS661: Tipos de órdenes de servicios profesionales
		Case "MOS661"
			With Request
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					mstrQueryString = "&nCurrency=" & Request.Form.Item("valCurrency") & "&dEffecdate=" & Request.Form.Item("tcdEffecdate")
					lblnPost = True
				Else
					mstrQueryString = "&nCurrency=" & Request.Form.Item("hddnCurrency") & "&dEffecdate=" & Request.Form.Item("hdddEffecdate")
					
					If .QueryString.Item("WindowType") = "PopUp" Then
						lblnPost = mobjMantClaim.insPostMOS661Upd(Request.QueryString.Item("Action"), mobjValues.StringToType(.Form.Item("hddnCurrency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hdddEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("cbeOrd_typeCost"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnAmount"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
						
					Else
						lblnPost = True
					End If
					
					lblnPost = True
				End If
			End With
			
			'+MSI559: Tabla de Fonasa 
		Case "MSI559"
			With Request
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					lblnPost = True
					Session("nYear") = .Form.Item("tcnYear")
					Session("nCurrency") = .Form.Item("cbeCurrency")
					Session("dEffecdate") = .Form.Item("tcdEffecdate")
				Else
					If .QueryString.Item("WindowType") = "PopUp" Then
						lblnPost = mobjMantClaim.insPostMSI559(.Form.Item("sAction"), mobjValues.StringToType(Session("nYear"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctService"), .Form.Item("tctSubService"), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCurrency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnAmount"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctDescript"))
					Else
						lblnPost = True
					End If
				End If
			End With
			
	End Select
	insPostMantClaim = lblnPost
End Function

</script>
<%Response.Expires = 0
%>

<HTML>
<HEAD>
	<LINK REL="StyleSheet" TYPE="text/css" HREF="/VTimeNet/Common/Custom.css">
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>




<SCRIPT SRC="/VTimeNet/scripts/GenFunctions.js"> </SCRIPT>
<SCRIPT>
//-Variable para el control de Versiones
document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 15:53 $|$$Author: Nvaplat61 $"
</SCRIPT>
</HEAD>

	<%
If CDbl(Request.QueryString.Item("nZone")) = 1 Then
	%><BODY>
	<%	
Else
	%><BODY CLASS="Header">
	<%	
End If

mstrCommand = "&sModule=Maintenance&sProject=MantClaim&sCodisplReload=" & Request.QueryString.Item("sCodispl")

mobjValues = New eFunctions.Values

'+ Si no se han validado los campos de la página

If Request.Form.Item("sCodisplReload") = vbNullString Then
	mstrErrors = insvalMantClaim
	Session("sErrorTable") = mstrErrors
	Session("sForm") = Request.Form.ToString
Else
	Session("sErrorTable") = vbNullString
	Session("sForm") = vbNullString
End If

If mstrErrors > vbNullString Then
	With Response
		.Write("<SCRIPT LANGUAGE=JAVASCRIPT>")
		.Write("ShowPopUp(""/VTimeNet/Common/Errors.aspx?sSource=" & Server.URLEncode(mstrCommand) & "&sQueryString=" & Server.URLEncode(Request.Params.Get("Query_String")) & """, ""MantClaimError"",660,330);self.document.location.href='/VTimeNet/Common/Blank.htm';")
		.Write(mobjValues.StatusControl(False, CShort(Request.QueryString.Item("nZone")), Request.QueryString.Item("WindowType")))
		.Write("</SCRIPT>")
	End With
Else
	If insPostMantClaim() Then
		If Request.QueryString.Item("WindowType") <> "PopUp" Then
			If Request.QueryString.Item("nAction") = CStr(eFunctions.Menues.TypeActions.clngAcceptdataFinish) Then
				Response.Write("<SCRIPT>top.document.location.reload();</SCRIPT>")
			Else
				If Request.QueryString.Item("nZone") = "1" Then
					If Request.Form.Item("sCodisplReload") = vbNullString Then
						Response.Write("<SCRIPT>;self.history.go(-1);top.fraFolder.document.location=""" & Replace(UCase(Request.QueryString.Item("sCodispl")), "_K", "") & ".aspx?nMainAction=" & Request.QueryString.Item("nMainAction") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & mstrString & mstrQueryString & """;</SCRIPT>")
					Else
						Response.Write("<SCRIPT>window.close();opener.top.fraFolder.document.location=""" & Replace(UCase(Request.QueryString.Item("sCodispl")), "_K", "") & ".aspx?nMainAction=" & Request.QueryString.Item("nMainAction") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & mstrString & """;</SCRIPT>")
					End If
				Else
					If Request.QueryString.Item("sCodispl") = "MSI035" Then
						Response.Write("<SCRIPT>window.close();</SCRIPT>")
					Else
						Response.Write("<SCRIPT>;self.history.go(-1);top.fraHeader.document.location=""" & Replace(UCase(Request.QueryString.Item("sCodispl")), "_K", "") & ".aspx?nMainAction=" & Request.QueryString.Item("nMainAction") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & """;</SCRIPT>")
					End If
				End If
			End If
			'+ Se mueve automaticamente a la siguiente página
		Else
			Select Case Request.QueryString.Item("sCodispl")
				Case "MSI010"
					Response.Write("<SCRIPT>top.opener.document.location.href='MSI010.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&nMainAction=302 '</SCRIPT>")
					'+ Tabla de Proveedores
				Case "MSI011"
					Response.Write("<SCRIPT>top.opener.document.location.href='MSI011_K.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&nMainAction=" & Request.QueryString.Item("nMainAction") & "'</SCRIPT>")
				Case "MSI014"
					Response.Write("<SCRIPT>top.opener.document.location.href='MSI014.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&nMainAction=302 '</SCRIPT>")
				Case "MSI015"
					Response.Write("<SCRIPT>top.opener.document.location.href='MSI015.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&nMainAction=" & Request.QueryString.Item("nMainAction") & "'</SCRIPT>")
				Case "MSI016"
					Response.Write("<SCRIPT>top.opener.document.location.href='MSI016_K.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&nMainAction=" & Request.QueryString.Item("nMainAction") & "'</SCRIPT>")
				Case "MSI019"
					Response.Write("<SCRIPT>top.opener.document.location.href='MSI019_K.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&nMainAction=" & Request.QueryString.Item("nMainAction") & "'</SCRIPT>")
					'+ Zonas asociadas a un proveedor
				Case "MSI647"
					Response.Write("<SCRIPT>top.opener.document.location.href='MSI647_k.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&nProvider=" & Request.QueryString.Item("nProvider") & "'</SCRIPT>")
					'+ Tipos de órdenes de servicios profesionales
				Case "MOS661"
					Response.Write("<SCRIPT>top.opener.document.location.href='MOS661.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & mstrQueryString & "'</SCRIPT>")
				Case "MSI559"
					Response.Write("<SCRIPT>top.opener.document.location.href='MSI559.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&nMainAction=" & Request.QueryString.Item("nMainAction") & "'</SCRIPT>")
				Case ""
					Response.Write("<SCRIPT>top.opener.document.location.href='_K.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&nMainAction=" & Request.QueryString.Item("nMainAction") & "'</SCRIPT>")
			End Select
		End If
	End If
End If

mobjValues = Nothing
mobjMantClaim = Nothing
%>
</BODY>
</HTML>




