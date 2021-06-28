<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eGeneralForm" %>
<%@ Import namespace="eClient" %>
<%@ Import namespace="ePolicy" %>
<%@ Import namespace="eClaim" %>
<script language="VB" runat="Server">


'- Variable que contiene los metodos de diseño de páginas
Dim mobjValues As eFunctions.Values


'- Variable que contiene el número de nota en tratamiento	
Dim mlngNotenum As Object

'- Se define la contante para el manejo de errores en caso de advertencias
Dim mstrCommand As String

'- Variable definida para guardar rutas	
Dim mstrPath As String

'- Se define la variable para el manejo de errores
Dim mstrErrors As String

'- Variable del objeto de telefonos	
Dim mobjPhones As eGeneralForm.GeneralForm

'- Variable del objeto de dirección
Dim mobjAddress As eGeneralForm.GeneralForm


'% insvalGeneralForm: Se realizan las validaciones masivas de la forma
'--------------------------------------------------------------------------------------------
Function insvalGeneralForm() As String
	'--------------------------------------------------------------------------------------------
	insvalGeneralForm = ""
	Dim lclsNotes As eGeneralForm.GeneralForm
	Select Case Request.QueryString.Item("sCodispl")
		Case "SCA2-1", "SCA2-9", "SCA2-L", "SCA2-H", "SCA2-A", "SCA2-8", "SCA2-5", "SCA2-K", "SCA2-M", "SCA2-I", "SCA2-J", "SCA2-G", "SCA2-S", "SCA2-Y", "SCA2-X", "SCA2-F", "SCA2-3", "SCA2-T", "SCA2-6", "SCA2-808", "SCA804", "SCA2-810", "SCA2-W", "SCA649", "SCA2-10", "SCA2-N", "SCA2-11", "SCA2-818", "SCA2-B"
			'+ Ventana de Notas
			If Request.QueryString.Item("WindowType") = "PopUp" Then
				lclsNotes = New eGeneralForm.GeneralForm
				With Request
					insvalGeneralForm = lclsNotes.insValSCA002(Request.QueryString.Item("sCodispl"), "Note", .Form.Item("tctDescript"), .Form.Item("tcdCompdate"), .Form.Item("tcdNulldate"), .Form.Item("tcttDs_text"))
				End With
				lclsNotes = Nothing
			End If
			
			'+ Ventana de Direcciones      
		Case "SCA101", "SCA108", "SCA110", "SCA102", "SCA735", "SCA778"
			If Request.QueryString.Item("nMainAction") <> CStr(eFunctions.Menues.TypeActions.clngActionQuery) Then
				If Request.QueryString.Item("WindowType") <> "PopUp" Then
					mobjAddress = New eGeneralForm.GeneralForm
					insvalGeneralForm = insvalGeneralForm & mobjAddress.insValSCA001(Request.QueryString.Item("sCodispl"), Request.Form.Item("tctRecType"), Request.Form.Item("txtAddress"), mobjValues.StringToType(Request.Form.Item("tcnZipCode"), eFunctions.Values.eTypeData.etdDouble), Request.Form.Item("valLocal"), Request.Form.Item("cbeCountry"), Request.Form.Item("tcnLonCardinG"), Request.Form.Item("tcnLonCardinM"), Request.Form.Item("tcnLonCardinS"), Request.Form.Item("tcnLatCardinG"), Request.Form.Item("tcnLatCardinM"), Request.Form.Item("tcnlatCardinS"), Request.Form.Item("valMunicipality"), Request.Form.Item("chkdeldir"), Session("sClient"), Session("SCA101_dEffecDate"), Request.Form.Item("tctPobox"), Request.Form.Item("chkInfor"), Request.Form.Item("tctE_mail"), Request.Form.Item("cbeProvince"), vbNullString, mobjValues.StringToType(Request.QueryString.Item("nSendAddr"), eFunctions.Values.eTypeData.etdInteger))
					mobjAddress = Nothing
				Else
					mobjPhones = New eGeneralForm.GeneralForm
					insvalGeneralForm = mobjPhones.insValPhones(Request.QueryString.Item("sCodispl"), Request.Form.Item("tcnRecowner"), Request.Form.Item("tctKeyAddress"), Request.Form.Item("tcnKeyPhones"), Request.Form.Item("tcnArea"), Session("dEffecdate"), Request.Form.Item("tctPhone"), Request.Form.Item("tcnOrder"), Request.Form.Item("tcnExtensi1"), Request.Form.Item("cbePhoneType"), Request.Form.Item("tcnExtensi2"), Request.QueryString.Item("Action"))
					mobjPhones = Nothing
				End If
			End If
		Case Else
			insvalGeneralForm = "insvalGeneralForm: Código lógico no encontrado (" & Request.QueryString.Item("sCodispl") & ")"
	End Select
End Function

'% insPostGeneralForm: Se realizan las actualizaciones de las ventanas
'--------------------------------------------------------------------------------------------
Function insPostGeneralForm() As Boolean
	'--------------------------------------------------------------------------------------------
	'- Variable que obtine el true o false del metodo.	
	Dim lblnPost As Boolean
	
	'- Variable del objeto de cliente	
	Dim lobjClient As Object
	
	'- Variable del objeto de casos de siniestros			
	Dim lclsClaim_case As Object
	
	'- Variable del objeto de las vantanas de la secuencia siniestro	
	Dim lclsCases_win As eClaim.Cases_win
	
	'- Variable del objeto de las notas	
	Dim lclsPostNotes As eGeneralForm.GeneralForm
	
	'- Variable del objeto de funciones de las paginas      
	Dim lobjValues As eFunctions.Values
	
	lobjValues = New eFunctions.Values
	
	lblnPost = True
	Dim lobjPhone As eGeneralForm.Phone
	Dim lobjAddress As eGeneralForm.Address
	Select Case Request.QueryString.Item("sCodispl")
		Case "SCA2-1", "SCA2-9", "SCA2-L", "SCA2-H", "SCA2-A", "SCA2-8", "SCA2-5", "SCA2-K", "SCA2-M", "SCA2-I", "SCA2-J", "SCA2-G", "SCA2-S", "SCA2-Y", "SCA2-X", "SCA2-F", "SCA2-3", "SCA2-T", "SCA2-6", "SCA2-808", "SCA804", "SCA2-810", "SCA2-W", "SCA649", "SCA2-10", "SCA2-N", "SCA2-11", "SCA2-818", "SCA2-B"
			'+ Ventana de Notas
			lclsPostNotes = New eGeneralForm.GeneralForm
			
			If Request.QueryString.Item("WindowType") = "PopUp" Then
				With lclsPostNotes
					.sCertype = Session("sCertype")
					.nBranch = lobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble)
					.nProduct = lobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble)
					.nPolicy = lobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble)
					.nCertif = lobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble)
					.nDeman_type = lobjValues.StringToType(Session("nDeman_type"), eFunctions.Values.eTypeData.etdDouble)
					.nCase_num = lobjValues.StringToType(Session("nCase_num"), eFunctions.Values.eTypeData.etdDouble)
					.dEffecdate = lobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate)
					.nClause = lobjValues.StringToType(Request.Form.Item("nClause"), eFunctions.Values.eTypeData.etdDouble)
					.nId = lobjValues.StringToType(Request.Form.Item("nID"), eFunctions.Values.eTypeData.etdDouble)
					.nServ_order = Session("nServ_order")
					.sLicense_ty = Request.Form.Item("sLicense_ty")
					.sRegist = Request.Form.Item("sRegist")
				End With
				With Request
					
					'+ Realiza las actualizaciones de las notas según corresponda el Codispl.
					lblnPost = lclsPostNotes.insPostGeneralNotes(.QueryString.Item("sCodispl"), .QueryString.Item("Action"), Session("sClient"), lobjValues.StringToType(Session("nClaim"), eFunctions.Values.eTypeData.etdDouble), .QueryString.Item("WindowType"), lobjValues.StringToType(.Form.Item("tcnNotenum"), eFunctions.Values.eTypeData.etdDouble), lobjValues.StringToType(.Form.Item("tcnConsec"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctDescript"), lobjValues.StringToType(.Form.Item("tcdCompdate"), eFunctions.Values.eTypeData.etdDate), lobjValues.StringToType(.Form.Item("tcdNulldate"), eFunctions.Values.eTypeData.etdDate), .Form.Item("tcttDs_text"), lobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), lobjValues.StringToType(.Form.Item("nRectype"), eFunctions.Values.eTypeData.etdDouble))
					mlngNotenum = lclsPostNotes.nNotenum
				End With
			End If
			lclsPostNotes = Nothing
			
			'+ Ventana de Direcciones
		Case "SCA101", "SCA108", "SCA110", "SCA102", "SCA735", "SCA778"
			If Request.QueryString.Item("nMainAction") <> CStr(eFunctions.Menues.TypeActions.clngActionQuery) Then
				If Request.QueryString.Item("WindowType") = "PopUp" Then
					lobjPhone = New eGeneralForm.Phone
					
					
					Select Case Request.QueryString.Item("Action")
						Case "Add"
							With lobjPhone
								.nRecowner = CInt(Request.Form.Item("tcnRecowner"))
								.sKeyAddress = Request.Form.Item("tctKeyAddress")
								.nKeyPhones = CInt(Request.Form.Item("tcnOrder"))
								.nArea_code = lobjValues.StringToType(Request.Form.Item("tcnArea"), eFunctions.Values.eTypeData.etdDouble, True)
								If CStr(Session("dInpdate")) <> vbNullString And Request.QueryString.Item("sCodispl") = "SCA101" Then
									.dEffecdate = Session("dInpdate")
								Else
									.dEffecdate = Session("SCA101_dEffecDate")
								End If
								.sPhone = Request.Form.Item("tctPhone")
								.nOrder = CInt(Request.Form.Item("tcnOrder"))
								If Trim(Request.Form.Item("tcnExtensi1")) <> vbNullString Then
									.nExtens1 = CInt(Request.Form.Item("tcnExtensi1"))
								End If
								.nPhone_type = lobjValues.StringToType(Request.Form.Item("cbePhoneType"), eFunctions.Values.eTypeData.etdDouble, True)
								
								If Trim(Request.Form.Item("tcnExtensi2")) <> vbNullString Then
									.nExtens2 = CInt(Request.Form.Item("tcnExtensi2"))
								End If
								.nUsercode = Session("nUserCode")
								lblnPost = .Add
							End With
							
						Case "Update"
							With lobjPhone
								.nRecowner = CInt(Request.Form.Item("tcnRecowner"))
								.sKeyAddress = Request.Form.Item("tctKeyAddress")
								.nKeyPhones = CInt(Request.Form.Item("tcnOrder"))
								.nArea_code = lobjValues.StringToType(Request.Form.Item("tcnArea"), eFunctions.Values.eTypeData.etdDouble, True)
								.sPhone = Request.Form.Item("tctPhone")
								.nOrder = CInt(Request.Form.Item("tcnOrder"))
								If Trim(Request.Form.Item("tcnExtensi1")) <> vbNullString Then
									.nExtens1 = CInt(Request.Form.Item("tcnExtensi1"))
								End If
								If Trim(Request.Form.Item("tcnExtensi2")) <> vbNullString Then
									.nExtens2 = CInt(Request.Form.Item("tcnExtensi2"))
								End If
								.nPhone_type = lobjValues.StringToType(Request.Form.Item("cbePhoneType"), eFunctions.Values.eTypeData.etdDouble, True)
								
								If CStr(Session("dInpdate")) <> vbNullString And Request.QueryString.Item("sCodispl") = "SCA101" Then
									.dEffecdate = Session("dInpdate")
								Else
									.dEffecdate = Session("SCA101_dEffecDate")
								End If
								
								.nUsercode = Session("nUserCode")
								lblnPost = .Update
							End With
					End Select
					lobjPhone = Nothing
				Else
					
					'+ Request.QueryString("WindowType") <> "PopUp"  
					lobjAddress = New eGeneralForm.Address
					With lobjAddress
						If Request.QueryString.Item("sCodispl") = "SCA102" Or Request.QueryString.Item("sCodispl") = "SCA108" Then
							.dEffecdate = Session("SCA101_dEffecDate")
						Else
							.dEffecdate = Today
						End If
						
						If CStr(Session("dInpdate")) <> vbNullString And Request.QueryString.Item("sCodispl") = "SCA101" Then
							.dEffecdate = Session("dInpdate")
						End If
						.nRecowner = CInt(Request.Form.Item("tcnRecOwner"))
						.sKeyAddress = Request.Form.Item("tctKeyAddress")
						.sRecType = Request.Form.Item("tctRecType")
						.sStreet = Request.Form.Item("txtAddress")
						.sClient = Session("sClient")
						.sE_mail = Request.Form.Item("tctE_mail")
						.nLat_grade = lobjValues.StringToType(Request.Form.Item("tcnLatCardinG"), eFunctions.Values.eTypeData.etdDouble)
						.nLon_grade = lobjValues.StringToType(Request.Form.Item("tcnLonCardinG"), eFunctions.Values.eTypeData.etdDouble)
						.nLat_minute = lobjValues.StringToType(Request.Form.Item("tcnLatCardinM"), eFunctions.Values.eTypeData.etdDouble)
						.nLon_minute = lobjValues.StringToType(Request.Form.Item("tcnLonCardinM"), eFunctions.Values.eTypeData.etdDouble)
						.nLat_second = lobjValues.StringToType(Request.Form.Item("tcnLatCardinS"), eFunctions.Values.eTypeData.etdDouble)
						.nLon_second = lobjValues.StringToType(Request.Form.Item("tcnLonCardinS"), eFunctions.Values.eTypeData.etdDouble)
						
						.nCountry = CInt(Request.Form.Item("cbeCountry"))
						.nLocal = CInt(Request.Form.Item("ValLocal"))
						
						.nZip_code = lobjValues.StringToType(Request.Form.Item("tcnZipCode"), eFunctions.Values.eTypeData.etdDouble)
						.nProvince = CInt(Request.Form.Item("cbeProvince"))
						.nUsercode = Session("nUsercode")
						.nMunicipality = CInt(Request.Form.Item("valMunicipality"))
						.sInfor = Request.Form.Item("chkInfor")
						.sBuild = Request.Form.Item("tctBuild")
						.nFloor = lobjValues.StringToType(Request.Form.Item("tcnFloor"), eFunctions.Values.eTypeData.etdDouble)
						.sDepartment = Request.Form.Item("tctDepartment")
						.sPopulation = Request.Form.Item("tctPopulation")
						.sPobox = Request.Form.Item("tctPobox")
						.sDescadd = Request.Form.Item("tctDescadd")
						
						If Request.QueryString.Item("sCodispl") = "SCA108" Then
							.nBranch = lobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble)
							.nProduct = lobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble)
							.sCertype = Session("sCertype")
							.nPolicy = lobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble)
							.nCertif = lobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble)
						End If
						If Request.QueryString.Item("sCodispl") = "SCA110" Or Request.QueryString.Item("sCodispl") = "SCA735" Or Request.QueryString.Item("sCodispl") = "SCA778" Then
							.nClaim = lobjValues.StringToType(Session("nClaim"), eFunctions.Values.eTypeData.etdDouble)
						End If
						If Request.QueryString.Item("sCodispl") = "SCA102" Then
							.nBranch = lobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble)
							.nProduct = lobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble)
							.sCertype = Session("sCertype")
							.nPolicy = lobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble)
							.nCertif = lobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble)
						End If
						
						If Request.QueryString.Item("sCodispl") = "SCA101" And Request.Form.Item("chkdeldir") = "1" Then
							.Delete()
						Else
							.Update()
						End If
						
						
						If CStr(Session("nUsercode")) = "9933" Then
							
							Response.Write("<SCRIPT>")
							Response.Write("alert ('Hola Actualizo');")
							Response.Write("</" & "Script>")
							
							lobjAddress.UpdatePhones(Request.Form.Item("tctKeyAddress"), CShort(Request.Form.Item("tcnRecOwner")), lobjAddress.dEffecdate, lobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
							
						End If
						
						
					End With
					lobjAddress = Nothing
				End If
			End If
	End Select
	
	If lblnPost And Request.QueryString.Item("nZone") = "2" Then
		Select Case Request.QueryString.Item("sCodispl")
			'+ Se actualiza ventana clientes, Client_Win
			Case "SCA2-9", "SCA101", "SCA10-2"
				lobjClient = New eClient.ClientWin
				If Request.QueryString.Item("sCodispl") = "SCA2-9" Then
					If mlngNotenum = vbNullString Then
						If Request.QueryString.Item("WindowType") = "PopUp" Then
							lobjClient.insUpdClient_win(Session("sClient"), CStr(Request.QueryString.Item("sCodispl")), "1")
						End If
					Else
						lobjClient.insUpdClient_win(Session("sClient"), CStr(Request.QueryString.Item("sCodispl")), "2")
					End If
				Else
					lobjClient.insUpdClient_win(Session("sClient"), CStr(Request.QueryString.Item("sCodispl")), "2")
				End If
				
				lobjClient = Nothing
				
				'+ Se actualiza ventana poliza, PolicyWin
			Case "SCA2-1", "SCA108", "SCA102", "SCA2-808", "SCA2-F"
				If Request.QueryString.Item("WindowType") <> "PopUp" Then
					lobjClient = New ePolicy.Policy_Win
					With lobjValues
						Call lobjClient.Add_PolicyWin(Session("sCertype"), .StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), .StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), .StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble), .StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), .StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), .StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), Request.QueryString.Item("sCodispl"), "2")
					End With
				Else
					lobjClient = New ePolicy.Policy_Win
					With lobjValues
						Call lobjClient.Add_PolicyWin(Session("sCertype"), .StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), .StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), .StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble), .StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), .StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), .StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), Request.QueryString.Item("sCodispl"), "2")
					End With
					lobjClient = Nothing
				End If
				
				'+ Se actualiza ventana siniestros, Claim_win
			Case "SCA110", "SCA2-8", "SCA735", "SCA778"
				If Request.QueryString.Item("WindowType") <> "PopUp" Then
					lobjClient = New eClaim.Claim_win
					Call lobjClient.Add_Claim_win(Session("nClaim"), Request.QueryString.Item("sCodispl"), "2", lobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
					lobjClient = Nothing
				End If
				
			Case "SCA2-5", "SCA2-S", "SCA2-6"
				If Request.QueryString.Item("WindowType") <> "PopUp" Then
					lclsCases_win = New eClaim.Cases_win
					Call lclsCases_win.Add_Cases_win(lobjValues.StringToType(Session("nClaim"), eFunctions.Values.eTypeData.etdDouble), lobjValues.StringToType(Session("nCase_num"), eFunctions.Values.eTypeData.etdDouble), lobjValues.StringToType(Session("nDeman_type"), eFunctions.Values.eTypeData.etdDouble), Request.QueryString.Item("sCodispl"), "2", lobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
					lclsCases_win = Nothing
				End If
		End Select
	End If
	insPostGeneralForm = lblnPost
	lobjValues = Nothing
End Function

'% insGetSource: se arma la dirección general en caso de advertencias
'--------------------------------------------------------------------------------------------
Private Sub insGetSource()
	'--------------------------------------------------------------------------------------------
	'- Variable de los modulos	
	Dim lstrModule As String
	
	'- Variable de los proyectos	
	Dim lstrProject As String
	
	Select Case Request.QueryString.Item("sCodispl")
		Case "SCA101", "SCA2-7", "SCA2-9", "SCA2-L"
			lstrModule = "Client"
			lstrProject = "ClientSeq"
			mstrPath = "/VTimeNet/Client/ClientSeq/"
			
			'+ Secuencia de Póliza.   		
		Case "SCA102", "SCA108", "SCA2-1", "SCA2-2", "SCA2-3", "SCA2-4", "SCA2-A", "SCA2-B", "SCA2-D", "SCA2-H", "SCA2-R", "SCA2-T", "SCA2-U", "SCA2-L", "SCA2-808", "SCA2-F", "SCA2-818"
			lstrModule = "Policy"
			lstrProject = "PolicySeq"
			mstrPath = "/VTimeNet/Policy/PolicySeq/"
			
			'+ Transacciones de póliza
		Case "SCA2-11"
			lstrModule = "Policy"
			lstrProject = "PolicyTra"
			mstrPath = "/VTimeNet/Policy/PolicyTra/"
			
			'+ Secuencia de Siniestros.   					
		Case "SCA110", "SCA2-8", "SCA2-V", "SCA735", "SCA778"
			lstrModule = "Claim"
			lstrProject = "ClaimSeq"
			mstrPath = "/VTimeNet/Claim/ClaimSeq/"
			
			'+ Subsecuencia de Casos de Siniestros.   
		Case "SCA2-5", "SCA2-K", "SCA2-J", "SCA2-S", "SCA2-6", "SCA2-10", "SCA2-N"
			lstrModule = "Claim"
			lstrProject = "CaseSeq"
			mstrPath = "/VTimeNet/Claim/CaseSeq/"
			
			'+ Solicitud de Ordenes de Servicio	    
		Case "SCA2-W"
			lstrModule = "Claim"
			lstrProject = "Claim"
			mstrPath = "/VTimeNet/Claim/Claim/"
			
		Case "SCA649"
			lstrModule = "Prof_ord"
			lstrProject = "Prof_ordseq"
			mstrPath = "/VTimeNet/Prof_ord/Prof_ordseq/"
	End Select
	mstrCommand = "&sModule=" & lstrModule & "&sProject=" & lstrProject & "&sCodisplReload=" & Request.QueryString.Item("sCodispl")
End Sub

'% insFinish: se activa al finalizar el proceso
'--------------------------------------------------------------------------------------------
Function insFinish() As Boolean
	'--------------------------------------------------------------------------------------------
	Dim lclsValidate As Object
	
	insFinish = True
	
	'+ Si no se han validado los campos de la página
	Dim lclsClientWin As eClient.ClientWin
	If Request.Form.Item("sCodisplReload") = vbNullString Then
		Select Case Request.QueryString.Item("sCodispl")
			
			'+ Secuencia de Clientes.   
			Case "SCA101", "SCA2-7", "SCA2-9", "SCA2-L"
				
				lclsClientWin = New eClient.ClientWin
				lclsValidate = New eGeneralForm.GeneralForm
				
				'+ Se verifica que no existan ventanas requeridas para la secuencia
				If lclsClientWin.IsPageRequired(Session("sClient"), CInt(Request.QueryString.Item("nMainAction"))) Then
					mstrErrors = lclsValidate.insValGE101("ClientSeq")
				End If
				lclsClientWin = Nothing
				
				'+ Subsecuencia de Casos de Siniestros.   
			Case "SCA2-5", "SCA2-K", "SCA2-J", "SCA2-S", "SCA2-6", "SCA2-10", "SCA2-N"
				lclsValidate = New eClaim.Claim_cases
				
				'+ Se verifica que no existan ventanas requeridas para la secuencia
				mstrErrors = lclsValidate.insValSI099(Session("nClaim"), Session("nCase_num"), Session("nDeman_type"))
		End Select
	End If
	
	Dim lclsClaim_case As eClaim.Claim_case
	If mstrErrors > vbNullString Then
		insFinish = False
		Session("sErrorTable") = mstrErrors
		Session("sForm") = Request.Form.ToString
		With Response
			.Write("<SCRIPT LANGUAGE=JAVASCRIPT>")
			.Write("ShowPopUp(""/VTimeNet/Common/Errors.aspx?sSource=" & Server.URLEncode(mstrCommand) & "&sQueryString=" & Server.URLEncode(Request.Params.Get("Query_String")) & """, ""GeneralFormError"",660,330);")
			.Write(mobjValues.StatusControl(False, CShort(Request.QueryString.Item("nZone")), Request.QueryString.Item("WindowType")))
			.Write("</" & "Script>")
		End With
	Else
		If Session("bQuery") = False Then
			Select Case Request.QueryString.Item("sCodispl")
				
				'+ Subsecuencia de Casos de Siniestros.   
				Case "SCA2-5", "SCA2-K", "SCA2-J", "SCA2-S", "SCA2-6", "SCA2-10", "SCA2-N"
					
					lclsClaim_case = New eClaim.Claim_case
					
					With lclsClaim_case
						If .Find(Session("nClaim"), Session("nCase_num"), Session("nDeman_type")) Then
							If .sStaReserve = "6" Then
								.sStaReserve = "2"
							End If
							insFinish = .UpdatesStareserve(.nClaim, .nDeman_type, .nCase_num, .sStaReserve)
						End If
					End With
					lclsClaim_case = Nothing
			End Select
		End If
	End If
	lclsValidate = Nothing
End Function

</script>
<%Response.Expires = -1
%>



	
<HTML>
<HEAD>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<%mobjValues = New eFunctions.Values
Response.Write(mobjValues.StyleSheet())
Response.Write("<SCRIPT>")
%>
//+ Variable para el control de versiones
	document.VssVersion="$$Revision: 6 $|$$Date: 15/10/03 16.34 $" 

//% CancelErrors: regresa a la ventana que invocó los errores
//-------------------------------------------------------------------------------------------
function CancelErrors(){
	self.history.go(-1)
}
//-------------------------------------------------------------------------------------------

//% NewLocation: Se mueve a la siguiente ventana de la secuencia
//-------------------------------------------------------------------------------------------
function NewLocation(Source,Codisp){
//-------------------------------------------------------------------------------------------
    var lstrLocation = "";
    lstrLocation += Source.location;
    lstrLocation = lstrLocation.replace(/&OPENER=.*/,"") + "&OPENER=" + Codisp
    Source.location = lstrLocation
}
</SCRIPT>
</HEAD>
<BODY>
<%
Call insGetSource()

'+ Si no se han validado los campos de la página
If Request.Form.Item("sCodisplReload") = vbNullString Then
	mstrErrors = insvalGeneralForm
	Session("sErrorTable") = mstrErrors
Else
	Session("sErrorTable") = vbNullString
End If

If Request.QueryString.Item("nAction") <> CStr(eFunctions.Menues.TypeActions.clngAcceptdataFinish) Then
	If mstrErrors > vbNullString Then
		With Response
			.Write("<SCRIPT LANGUAGE=JAVASCRIPT>")
			.Write("ShowPopUp(""/VTimeNet/Common/Errors.aspx?sForm=" & Server.URLEncode(Request.Form.ToString) & Server.URLEncode(mstrCommand) & "&sQueryString=" & Server.URLEncode(Request.Params.Get("Query_String")) & """,""ClaimErrors"",660,330);document.location.href='/VTimeNet/common/blank.htm';")
			mobjValues = New eFunctions.Values
			.Write(mobjValues.StatusControl(False, CShort(Request.QueryString.Item("nZone")), Request.QueryString.Item("WindowType")))
			.Write("</SCRIPT>")
		End With
	Else
		If insPostGeneralForm Then
			If Request.QueryString.Item("sOnSeq") = "1" Then
				If Request.QueryString.Item("WindowType") <> "PopUp" Then
					Response.Write("<SCRIPT>top.frames['fraSequence'].document.location='" & mstrPath & "Sequence.aspx?nAction=" & Request.QueryString.Item("nMainAction") & "&sGoToNext=Yes&nOpener=" & Request.QueryString.Item("sCodispl") & "';</SCRIPT>")
				Else
					Response.Write("<SCRIPT>top.opener.top.frames['fraSequence'].document.location='" & mstrPath & "Sequence.aspx?nAction=" & Request.QueryString.Item("nMainAction") & "&nOpener=" & Request.QueryString.Item("sCodispl") & "&sGoToNext=NO" & "';</SCRIPT>")
					Select Case Request.QueryString.Item("sCodispl")
						Case "SCA2-1", "SCA2-9", "SCA2-L", "SCA2-H", "SCA2-A", "SCA2-8", "SCA2-5", "SCA2-K", "SCA2-M", "SCA2-I", "SCA2-G", "SCA2-S", "SCA2-Y", "SCA2-X", "SCA2-F", "SCA2-3", "SCA2-T", "SCA2-N", "SCA2-6", "SCA2-808", "SCA804", "SCA2-W", "SCA649", "SCA2-10", "SCA2-N", "SCA2-11", "SCA2-818", "SCA2-B"
							Response.Write("<SCRIPT>top.opener.document.location.href='SCA002.aspx?sCodispl=" & Request.Form.Item("sCodispl") & "&Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&nNotenum=" & mlngNotenum & "&WindowType=PopUp" & "&nMainAction=" & Request.QueryString.Item("nMainAction") & "&sOnSeq=" & Request.QueryString.Item("sOnSeq") & "&nIndexNotenum=" & Request.QueryString.Item("nIndexNotenum") & "'</SCRIPT>")
						Case "SCA101", "SCA108", "SCA110", "SCA735", "SCA778"
							Response.Write("<SCRIPT>top.opener.document.location.href='SCA001.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("Index") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&nMainAction=" & Request.QueryString.Item("nMainAction") & "&sOnSeq=" & Request.QueryString.Item("sOnSeq") & "&sRecType=" & Request.Form.Item("tctRecType") & "'</SCRIPT>")
					End Select
				End If
			Else
				'+ Se recarga la página que invocó la PopUp
				Select Case Request.QueryString.Item("sCodispl")
					Case "SCA2-1", "SCA2-9", "SCA2-L", "SCA2-H", "SCA2-A", "SCA2-8", "SCA2-5", "SCA2-K", "SCA2-M", "SCA2-I", "SCA2-J", "SCA2-S", "SCA2-Y", "SCA2-X", "SCA2-F", "SCA2-3", "SCA2-T", "SCA2-O", "SCA2-N", "SCA2-6", "SCA2-808", "SCA804", "SCA2-810", "SCA2-W", "SCA649", "SCA2-10", "SCA2-N", "SCA2-11", "SCA2-818", "SCA2-B"
						Response.Write("<SCRIPT>top.opener.document.location.href='SCA002.aspx?sCodispl=" & Request.Form.Item("sCodispl") & "&Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&nNotenum=" & mlngNotenum & "&WindowType=" & Request.QueryString.Item("WindowType") & "&nMainAction=" & Request.QueryString.Item("nMainAction") & "&sOnSeq=" & Request.Form.Item("sOnSeq") & "&nIndexNotenum=" & Request.QueryString.Item("nIndexNotenum") & "'</SCRIPT>")
						
					Case "SCA2-G"
						Response.Write("<SCRIPT>top.opener.document.location.href='SCA002.aspx?sCodispl=" & Request.Form.Item("sCodispl") & "&Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&nNotenum=" & mlngNotenum & "&WindowType=" & Request.QueryString.Item("WindowType") & "&nMainAction=" & Request.QueryString.Item("nMainAction") & "&sOnSeq=" & Request.Form.Item("sOnSeq") & "&nIndexNotenum=" & Request.QueryString.Item("nIndexNotenum") & "&nClause=" & Request.Form.Item("nClause") & "'</SCRIPT>")
						
					Case "SCA101", "SCA108", "SCA110", "SCA102", "SCA735", "SCA778"
						Response.Write("<SCRIPT>top.opener.document.location.href='SCA001.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=0" & Request.QueryString.Item("Index") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&nMainAction=" & Request.QueryString.Item("nMainAction") & "&sOnSeq=" & Request.QueryString.Item("sOnSeq") & "&sRecType=" & Request.Form.Item("tctRecType") & "'</SCRIPT>")
				End Select
			End If
		End If
	End If
Else
	If (Request.QueryString.Item("sCodispl") <> "SCA2-5" And Request.QueryString.Item("sCodispl") <> "SCA2-S") Then
		If insFinish() Then
			Response.Write("<SCRIPT>top.location.reload();</SCRIPT>")
		End If
	Else
		If Session("bQuery") = True Then
			With Response
				.Write("<SCRIPT>")
				.Write("top.opener.document.location.href='/VTimeNet/Claim/ClaimSeq/SI016.aspx?sOnSeq=1&ReloadBySeqCase=True';")
				.Write("top.close()")
				.Write("</SCRIPT>")
			End With
		Else
			'+ Se recarga la página principal de la secuencia		
			If insFinish() Then
				With Response
					.Write("<SCRIPT>")
					.Write("top.opener.document.location.href='/VTimeNet/Claim/ClaimSeq/SI016.aspx?sOnSeq=1&ReloadBySeqCase=True';")
					.Write("top.close()")
					.Write("</SCRIPT>")
				End With
			End If
		End If
	End If
End If
mobjValues = Nothing
%>
</BODY>
</HTML>





