<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eCollection" %>
<script language="VB" runat="Server">
Dim mobjMantCollection As Object
Dim mclsrnullcondi As eCollection.rnullcondi
Dim mstrQueryString As String
Dim mstrErrors As String
Dim mobjValues As eFunctions.Values
Dim mstrString As Object
'+ Se define la contante para el manejo de errores en caso de advertencias
Dim mstrCommand As String



'% insValMantCollection: Se realizan las validaciones masivas de la forma
'--------------------------------------------------------------------------------------------
Function insValMantCollection() As String
	'--------------------------------------------------------------------------------------------
	Select Case Request.QueryString.Item("sCodispl")
		
		'**+ MCO002:Bill Cancellation Conditions
		'+MCO002: Codiciones de anulación para recibo
		
		Case "MCO002"
			With Request
				mclsrnullcondi = New eCollection.rnullcondi
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					insValMantCollection = mclsrnullcondi.InsValMCO002_k(.QueryString.Item("sCodispl"), mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdInteger), .QueryString.Item("Action"), mobjValues.StringToType(.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate))
				Else
					If .QueryString.Item("WindowType") = "PopUp" Then
						insValMantCollection = mclsrnullcondi.InsValMCO002(.QueryString.Item("sCodispl"), mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdInteger), .QueryString.Item("Action"), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("cbeNullcode"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdInteger), .Form.Item("cbePolitype"), .Form.Item("cbePolicy"), .Form.Item("cbeCertif"), mobjValues.StringToType(.Form.Item("cbeTratypei"), eFunctions.Values.eTypeData.etdInteger))
					End If
				End If
			End With
			
			'+ MCO505 - Convenios de pago por cliente.
			
		Case "MCO505"
			If CDbl(Request.QueryString.Item("nZone")) = 1 Then
				mobjMantCollection = New eCollection.Agreement
				
				With Request
					insValMantCollection = mobjMantCollection.insValHeaderMCO505_K(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("nMainAction"), Request.Form.Item("tctClient"))
					
					Session("sClient") = Request.Form.Item("tctClient")
					Session("nExist") = mobjMantCollection.nExist
					
				End With
			Else
				If Request.QueryString.Item("WindowType") = "PopUp" Then
					mobjMantCollection = New eCollection.Agreement
					With Request
						
						insValMantCollection = mobjMantCollection.insValMCO505(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("Action"), Session("sClient"), mobjValues.StringToType(Request.Form.Item("tcnCod_agree"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnQ_draft"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnMax_perc_dcto"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcdInit_date"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.Form.Item("tcdEnd_date"), eFunctions.Values.eTypeData.etdDate), Request.Form.Item("cbeStatregt"), mobjValues.StringToType(Request.Form.Item("cbeTypeagree"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("valIntermed"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("cbeAgency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("cbeType_rec"), eFunctions.Values.eTypeData.etdDouble))
						
					End With
				Else
					insValMantCollection = vbNullString
				End If
			End If
			
			'+MCO678: Mantencion tabla de comisiones de cobradores.
		Case "MCO678"
			If CDbl(Request.QueryString.Item("nZone")) = 1 Then
				mobjMantCollection = New eCollection.Collect_comm
				With Request
					insValMantCollection = mobjMantCollection.insValMCO678_K(.QueryString("sCodispl"), mobjValues.StringToType(.Form.Item("tcnCollectorType"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnContype"), eFunctions.Values.eTypeData.etdDouble, True), .Form.Item("optsCollecAsig"), mobjValues.StringToType(.Form.Item("tcnDaysIni"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnDaysEnd"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnCode"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate), .Form.Item("tctDescript"), .Form.Item("tctShort_des"), .Form.Item("cbeInChannel"))
				End With
			Else
				If Request.QueryString.Item("WindowType") = "PopUp" Then
					mobjMantCollection = New eCollection.Collect_comm
					With Request
						insValMantCollection = mobjMantCollection.insValMCO678(.QueryString("Action"), .QueryString("sCodispl"), Session("nCollectorType"), Session("nContype"), Session("sCollecAsig"), Session("nDaysIni"), Session("nDaysEnd"), Session("nCode"), Session("nInchannel"), Session("dEffecdate"), mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnInitRange"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnEndRange"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCommPercent"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeCurrency"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnCommAmount"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnMinAmount"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnMaxAmount"), eFunctions.Values.eTypeData.etdDouble, True))
					End With
				Else
					insValMantCollection = vbNullString
				End If
			End If
			
			'+MCO689: Mantencion tabla de Mantenimiento de Numeración de facturas 
		Case "MCO689"
			If CDbl(Request.QueryString.Item("nZone")) = 1 Then
				mobjMantCollection = New eCollection.Bills_Num
				With Request
					insValMantCollection = mobjMantCollection.insValMCO689_K(.QueryString("sCodispl"), mobjValues.StringToType(.Form.Item("cbeinsurarea"), eFunctions.Values.eTypeData.etdDouble, True), .Form.Item("optBillType"))
				End With
			Else
				If Request.QueryString.Item("WindowType") = "PopUp" Then
					mobjMantCollection = New eCollection.Bills_Num
					With Request
						insValMantCollection = mobjMantCollection.insValMCO689(.QueryString("sCodispl"), Session("nInsur_area"), Session("sBilltype"), mobjValues.StringToType(.Form.Item("tcnInitNumb"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnEndNumb"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnLastBill"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcdCompDate"), eFunctions.Values.eTypeData.etdDate, True))
					End With
				Else
					insValMantCollection = vbNullString
				End If
			End If
			
			'+MCO741: Convenios de Banco 
		Case "MCO741"
			If CDbl(Request.QueryString.Item("nZone")) = 1 Then
				mobjMantCollection = New eCollection.Bank_Agree
				insValMantCollection = vbNullString
			Else
				If Request.QueryString.Item("WindowType") = "PopUp" Then
					mobjMantCollection = New eCollection.Bank_Agree
					With Request
						insValMantCollection = mobjMantCollection.insValMCO741(.QueryString("sCodispl"), Session("sTyp_BankAgree"), mobjValues.StringToType(.Form.Item("cbeBank"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valConvenio"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctClient"))
						
					End With
				Else
					insValMantCollection = vbNullString
				End If
			End If
			
			'+MCO782: Bancos Multipac 
		Case "MCO782"
			If CDbl(Request.QueryString.Item("nZone")) = 1 Then
				mobjMantCollection = New eCollection.Bank_Agree
				With Request
					insValMantCollection = mobjMantCollection.insValMCO782_K(.QueryString("sCodispl"), mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeBank"), eFunctions.Values.eTypeData.etdDouble))
				End With
			Else
				If Request.QueryString.Item("WindowType") = "PopUp" Then
					mobjMantCollection = New eCollection.Bank_Agree
					With Request
						insValMantCollection = mobjMantCollection.insValMCO782(.QueryString("sCodispl"), .QueryString("Action"), mobjValues.StringToType(Session("cbeBank"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeBankAsoc"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdDate"), eFunctions.Values.eTypeData.etdDate))
						
					End With
				Else
					insValMantCollection = vbNullString
				End If
			End If
			mobjMantCollection = Nothing
			
			'+MCO734: Interes de mora 
		Case "MCO734"
			If CDbl(Request.QueryString.Item("nZone")) = 1 Then
				mobjMantCollection = New eCollection.Delay_Int
				insValMantCollection = vbNullString
			Else
				If Request.QueryString.Item("WindowType") = "PopUp" Then
					mobjMantCollection = New eCollection.Delay_Int
					With Request
						insValMantCollection = mobjMantCollection.insValMCO734(.QueryString("sCodispl"), .QueryString("Action"), mobjValues.StringToType(.Form.Item("tcnRangeIni"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnRangeEnd"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPercent"), eFunctions.Values.eTypeData.etdDouble))
					End With
				Else
					insValMantCollection = vbNullString
				End If
			End If
			
			'+ MCO827: Causa de rechazo por vía de pago y banco
		Case "MCO827"
			mobjMantCollection = New eCollection.Reject_cause
			With Request
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					insValMantCollection = mobjMantCollection.insvalMCO827_K(.QueryString("sCodispl"), mobjValues.StringToType(.Form.Item("cbeWay_pay"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valBank_code"), eFunctions.Values.eTypeData.etdDouble, True))
				Else
					insValMantCollection = vbNullString
					If .QueryString.Item("WindowType") = "PopUp" Then
						insValMantCollection = mobjMantCollection.insvalMCO827(.QueryString("sCodispl"), .QueryString("Action"), mobjValues.StringToType(.Form.Item("tcnReject_cause"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddBank_code"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddWay_pay"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctDescript"), .Form.Item("cbeStatregt"))
					End If
				End If
			End With
			
			'+MCO1424: Tabla de mantenimiento de archivos de cobranza PAC/TBK 
		Case "MCO1424"
			If CDbl(Request.QueryString.Item("nZone")) = 1 Then
'UPGRADE_NOTE: The 'eCollection.Filecollec' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
				mobjMantCollection = Server.CreateObject("eCollection.Filecollec")
				insValMantCollection = mobjMantCollection.insValMCO1424_K(Request.QueryString.Item("sCodispl"), mobjValues.StringToType(Request.Form.Item("cbeWay_pay"), eFunctions.Values.eTypeData.etdDouble, True))
				Response.Write(("<javascript> alert('entra') ; </javacript>"))
				
			Else
				If Request.QueryString.Item("WindowType") = "PopUp" Then
'UPGRADE_NOTE: The 'eCollection.Filecollec' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
					mobjMantCollection = Server.CreateObject("eCollection.Filecollec")
					With Request
						insValMantCollection = mobjMantCollection.insValMCO1424(.QueryString("sCodispl"), Session("nWay_pay"), mobjValues.StringToType(.Form.Item("cbeBank"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeFile"), eFunctions.Values.eTypeData.etdDouble))
						
					End With
				Else
					insValMantCollection = vbNullString
				End If
			End If
		Case Else
			insValMantCollection = "insValMantCollection: Código lógico no encontrado (" & Request.QueryString.Item("sCodispl") & ")"
	End Select
	mobjMantCollection = Nothing
End Function

'% insPostMantCollection: Se realizan las actualizaciones a las tablas
'--------------------------------------------------------------------------------------------
Function insPostMantCollection() As Boolean
        Dim lstrEndeavour As String
        Dim lstrNoCollection As String
	'--------------------------------------------------------------------------------------------
	Dim lblnPost As Boolean
	lblnPost = False
	Select Case Request.QueryString.Item("sCodispl")
		'**+ MCO002:Bill Cancellation Conditions
		'+MCO002: Codiciones de anulación para recibo
		Case "MCO002"
			With Request
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					Session("dEffecdate") = .Form.Item("tcdEffecdate")
					lblnPost = True
				Else
					If .QueryString.Item("WindowType") = "PopUp" Then
						lblnPost = mclsrnullcondi.InsPostMCO002(CDbl(.QueryString.Item("nZone")) = 1, .QueryString.Item("sCodispl"), mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdInteger), .QueryString.Item("Action"), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("cbeNullcode"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdInteger), .Form.Item("cbePolitype"), .Form.Item("cbePolicy"), .Form.Item("cbeCertif"), mobjValues.StringToType(.Form.Item("cbeTratypei"), eFunctions.Values.eTypeData.etdInteger))
					Else
						lblnPost = True
					End If
				End If
			End With
			
			'+ MCO505 - Convenios de pago por cliente.
		Case "MCO505"
			With Request
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					lblnPost = True
				Else
					If .QueryString.Item("WindowType") = "PopUp" Then
						mobjMantCollection = New eCollection.Agreement
						
                            If .Form.Item("chknocollection") = "1" Then
                                lstrNoCollection = .Form.Item("chknocollection")
                            Else
                                lstrNoCollection = "2"
                            End If
						lblnPost = mobjMantCollection.insPostMCO505(.QueryString("Action"), Session("sClient"), mobjValues.StringToType(.Form.Item("tcnCod_agree"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnQ_draft"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnMax_perc_dcto"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdInit_date"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcdEnd_date"), eFunctions.Values.eTypeData.etdDate), .Form.Item("cbeStatregt"), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("cbeTypeagree"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("valIntermed"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("cbeAgency"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("cbeType_rec"), eFunctions.Values.eTypeData.etdDouble, True), .Form.Item("tctFisrtName"), .Form.Item("tctLastName"), .Form.Item("tctsName"), mobjValues.StringToType(Request.Form.Item("tcnPosition"), eFunctions.Values.eTypeData.etdDouble, True), .Form.Item("tcnmail"), .Form.Item("tcnphone"), .Form.Item("tctsName_Agree"), lstrNoCollection)
					Else
						lblnPost = True
					End If
				End If
			End With
			
			'+MCO678: Tabla de comisiones de cobradores
		Case "MCO678"
			If Request.QueryString.Item("WindowType") = "PopUp" Then
				With Request
					mobjMantCollection = New eCollection.Collect_comm
					lblnPost = mobjMantCollection.insPostMCO678(Request.QueryString.Item("Action"), Session("nCollectorType"), Session("nContype"), Session("sCollecAsig"), Session("nDaysIni"), Session("nDaysEnd"), Session("nCode"), mobjValues.StringToType(Session("nInchannel"), eFunctions.Values.eTypeData.etdDouble), Session("dEffecdate"), mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnInitRange"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnEndRange"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCommPercent"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeCurrency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCommAmount"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnMinAmount"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnMaxAmount"), eFunctions.Values.eTypeData.etdDouble), Session("sDescript"), Session("sShort_des"))
				End With
			Else
				lblnPost = True
				Session("nInsur_area") = Request.Form.Item("cbeinsurarea")
				Session("nCollectorType") = Request.Form.Item("tcnCollectorType")
				Session("nContype") = Request.Form.Item("tcnContype")
				Session("sCollecAsig") = Request.Form.Item("optsCollecAsig")
				Session("nDaysIni") = Request.Form.Item("tcnDaysIni")
				Session("nDaysEnd") = Request.Form.Item("tcnDaysEnd")
				Session("nCode") = Request.Form.Item("tcnCode")
				Session("nInchannel") = Request.Form.Item("cbeInChannel")
				Session("dEffecdate") = Request.Form.Item("tcdEffecdate")
				Session("sDescript") = Request.Form.Item("tctDescript")
				Session("sShort_des") = Request.Form.Item("tctShort_des")
			End If
			
			'+MCO689: Tabla de mantención de Numeración de Facturas
		Case "MCO689"
			If Request.QueryString.Item("WindowType") = "PopUp" Then
				With Request
					mobjMantCollection = New eCollection.Bills_Num
					
					lblnPost = mobjMantCollection.insPostMCO689(Request.QueryString.Item("Action"), Session("nInsur_area"), Session("sBilltype"), mobjValues.StringToType(.Form.Item("tcnInitNumb"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnEndNumb"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnLastBill"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcdCompDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcdCompDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
				End With
			Else
				lblnPost = True
				Session("nInsur_area") = Request.Form.Item("cbeinsurarea")
				Session("sBilltype") = Request.Form.Item("optBillType")
			End If
			
			'+MCO741: Tabla de bancos en convenio
		Case "MCO741"
			If Request.QueryString.Item("WindowType") = "PopUp" Then
				
				With Request
					mobjMantCollection = New eCollection.Bank_Agree
					lblnPost = mobjMantCollection.insPostMCO741(.Form.Item("sAction"), Session("sTyp_BankAgree"), mobjValues.StringToType(.Form.Item("cbeBank"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valConvenio"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctClient"))
					
				End With
			Else
				lblnPost = True
				Session("sTyp_BankAgree") = Request.Form.Item("optTypBankAgree")
			End If
			
			'+MCO734: Tabla para interes de mora  
		Case "MCO734"
			If Request.QueryString.Item("WindowType") = "PopUp" Then
				With Request
					mobjMantCollection = New eCollection.Delay_Int
					lblnPost = mobjMantCollection.insPostMCO734(.QueryString("Action"), mobjValues.StringToType(.Form.Item("tcnRangeIni"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnRangeEnd"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPercent"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
				End With
			Else
				lblnPost = True
			End If
			
			'+MCO782: Bancos Multipac  
		Case "MCO782"
			If CDbl(Request.QueryString.Item("nZone")) = 1 Then
				mobjMantCollection = New eCollection.Bank_Agree
				Session("sTyp_BankAgree") = Request.Form.Item("optTypBankAgree")
				Session("cbeBank") = Request.Form.Item("cbeBank")
				
				lblnPost = True
			Else
				
				If Request.QueryString.Item("WindowType") = "PopUp" Then
					With Request
						mobjMantCollection = New eCollection.Bank_Agree
						lblnPost = mobjMantCollection.insPostMCO782(.QueryString("Action"), mobjValues.StringToType(Session("cbeBank"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeBankAsoc"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdDate"), eFunctions.Values.eTypeData.etdDate), Session("nUsercode"))
					End With
				Else
					lblnPost = True
				End If
			End If
			
			'+ MCO827: Causa de rechazo por vía de pago y banco
		Case "MCO827"
			With Request
				lblnPost = True
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					mstrQueryString = "&sCodispl=" & .QueryString.Item("sCodispl") & "&nWay_pay=" & .Form.Item("cbeWay_pay") & "&nBank_code=" & .Form.Item("valBank_code")
				Else
					If .QueryString.Item("WindowType") = "PopUp" Then
						mobjMantCollection = New eCollection.Reject_cause
						
						If .Form.Item("chkEndeavour") = "1" Then
							lstrEndeavour = .Form.Item("chkEndeavour")
						Else
							lstrEndeavour = "2"
						End If
						'Response.Write "<NOTSCRIPT>alert('"&.Form("chkEndeavour")&"')</" & "Script>"
						
						lblnPost = mobjMantCollection.inspostMCO827(.QueryString("Action"), mobjValues.StringToType(.Form.Item("tcnReject_cause"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddBank_code"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddWay_pay"), eFunctions.Values.eTypeData.etdDouble), Session("nUsercode"), .Form.Item("tctDescript"), .Form.Item("tctShort_des"), .Form.Item("cbeStatregt"), lstrEndeavour)
						mstrQueryString = "&nWay_pay=" & .Form.Item("hddWay_pay") & "&nBank_code=" & .Form.Item("hddBank_code")
					End If
				End If
			End With
			'+MCO1424: Tabla de mantenimiento de archivos de cobranza PAC/TBK 
		Case "MCO1424"
			If Request.QueryString.Item("WindowType") = "PopUp" Then
				
				'Response.Write"<script> alert(""" & Request.Form.ToSTring  & """)</" & "Script>"
				With Request
'UPGRADE_NOTE: The 'eCollection.Filecollec' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
					mobjMantCollection = Server.CreateObject("eCollection.Filecollec")
					lblnPost = mobjMantCollection.insPostMCO1424(Request.Form.Item("sAction"), mobjValues.StringToType(Session("nWay_pay"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("cbeBank"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("cbeFile"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
					mstrQueryString = "&nWay_pay=" & mobjValues.StringToType(Request.Form.Item("cbeWay_pay"), eFunctions.Values.eTypeData.etdDouble)
					lblnPost = True
				End With
			Else
				mstrQueryString = "&nWay_pay=" & mobjValues.StringToType(Session("nWay_pay"), eFunctions.Values.eTypeData.etdDouble)
				Session("nWay_pay") = Request.Form.Item("cbeWay_pay")
				lblnPost = True
			End If
	End Select
	mobjMantCollection = Nothing
	insPostMantCollection = lblnPost
End Function

</script>
<%Response.Expires = -1
mobjValues = New eFunctions.Values
mstrCommand = "sModule=Maintenance&sProject=MantCollection&sCodisplReload=" & Request.QueryString.Item("sCodispl")
%>
<HTML>
<HEAD>
<%
With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjValues.WindowsTitle("GE002"))
End With
%>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
	<SCRIPT>
//- Variable para el control de versiones
	     document.VssVersion="$$Revision: 5 $|$$Date: 20/10/04 3:30p $|$$Author: Nvapla10 $"
    </SCRIPT>

<SCRIPT>
//------------------------------------------------------------------------------------------
function NewLocation(Source,Codisp){
//------------------------------------------------------------------------------------------
    var lstrLocation = "";
    lstrLocation += Source.location;
    lstrLocation = lstrLocation.replace(/&OPENER=.*/,"") + "&OPENER=" + Codisp
    Source.location = lstrLocation
}
</SCRIPT>



	 
</HEAD>
<BODY>
<FORM id=form1 name=form1>
<%

'+ Si no se han validado los campos de la página

If Request.Form.Item("sCodisplReload") = vbNullString Then
	mstrErrors = insValMantCollection
	Session("sErrorTable") = mstrErrors
	Session("sForm") = Request.Form.ToString
Else
	Session("sErrorTable") = vbNullString
	Session("sForm") = vbNullString
End If

If mstrErrors > vbNullString Then
	With Response
		.Write("<SCRIPT LANGUAGE=JAVASCRIPT>")
		.Write("ShowPopUp(""/VTimeNet/Common/Errors.aspx?sSource=" & Server.URLEncode(mstrCommand) & "&sQueryString=" & Server.URLEncode(Request.Params.Get("Query_String")) & """, ""MantCollectionError"",660,330);document.location.href='/VTimeNet/common/blank.htm';")
		.Write(mobjValues.StatusControl(False, CShort(Request.QueryString.Item("nZone")), Request.QueryString.Item("WindowType")))
		.Write("</SCRIPT>")
	End With
Else
	If insPostMantCollection Then
		If Request.QueryString.Item("WindowType") <> "PopUp" Then
			If Request.QueryString.Item("nAction") = CStr(eFunctions.Menues.TypeActions.clngAcceptdataFinish) Then
				Response.Write("<SCRIPT>insReloadTop(false);</SCRIPT>")
			Else
				If Request.QueryString.Item("nZone") = "1" Then
					If Request.Form.Item("sCodisplReload") = vbNullString Then
						Response.Write("<SCRIPT>top.fraFolder.document.location=""" & Replace(UCase(Request.QueryString.Item("sCodispl")), "_K", "") & ".aspx?nMainAction=" & Request.QueryString.Item("nMainAction") & mstrQueryString & """;</SCRIPT>")
					Else
						Response.Write("<SCRIPT>window.close();opener.top.fraFolder.document.location=""" & Replace(UCase(Request.QueryString.Item("sCodispl")), "_K", "") & ".aspx?nMainAction=" & Request.QueryString.Item("nMainAction") & """;</SCRIPT>")
					End If
				Else
					Response.Write("<SCRIPT>insReloadTop(false);</SCRIPT>")
				End If
			End If
		Else
			'+ Se recarga la página que invocó la PopUp
			Select Case Request.QueryString.Item("sCodispl")
				Case "MCO734"
					Response.Write("<SCRIPT>top.opener.document.location.href='MCO734_K.aspx?ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&Reload=" & Request.Form.Item("chkContinue") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&nMainAction=301'</SCRIPT>")
				Case Else
					Response.Write("<SCRIPT>top.opener.document.location.href='" & Request.QueryString.Item("sCodispl") & ".aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & mstrQueryString & "&nMainAction=302'</SCRIPT>")
			End Select
		End If
	End If
End If

mobjMantCollection = Nothing
mobjValues = Nothing
%>
        </FORM>
    </BODY>
</HTML>

</BODY> 
</HTML> 





