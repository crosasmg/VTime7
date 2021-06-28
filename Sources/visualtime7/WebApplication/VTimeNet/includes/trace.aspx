<%@ Import namespace="eAgenda" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eNetFrameWork" %>
<script language="VB" runat="Server">

Const eIniVal As String = "0"
Const eEndVal As String = "1"
Const eIniPost As String = "2"
Const eEndPost As String = "3"
Const eIniPaint As String = "4"
Const eEndPaint As String = "5"

'% insGetFields2: toma los valores de los parámetros que recibe en el QueryString o el Form
'--------------------------------------------------------------------------------------------    
Function insGetFields2(ByRef sNameField As String, ByRef rArray As String, ByRef nIdentifier As Byte) As Object
	'--------------------------------------------------------------------------------------------
	
	Dim lintCount As Integer
	Dim sValueControl As Object
	Dim sNameControl As String
	Dim sArrFields() As String
	Dim lintMax As Integer
	Dim lintEqual As Integer
	'- Campos que posee el String recibido en el QueryString o Form    
	Dim lstrForm As String
	
	
	lintCount = 1
	
	lstrForm = rArray
	
	
	'+El ciclo que se presenta a continuación, carga todos los campos de la forma que llamo a la
	'+ventana de errores, como campos ocultos en la misma. 
	
        sArrFields = lstrForm.Split("&")
	lintMax = UBound(sArrFields)
	For lintCount = 0 To lintMax
		
		'+Si existian dos "&" seguidos, la casilla de la matriz pudo quedar vacia
		If (sArrFields(lintCount) <> vbNullString) Then
			lintEqual = InStr(1, sArrFields(lintCount), "=")
			If lintEqual > 0 Then
				If nIdentifier = 1 Then
					sNameControl = Mid(sArrFields(lintCount), 2, lintEqual - 2)
					insGetFields2 = ""
				Else
					sNameControl = Mid(sArrFields(lintCount), 4, lintEqual - 4)
				End If
				If sNameControl = Mid(sNameField, 2, Len(sNameField)) Then
					
					sValueControl = Mid(sArrFields(lintCount), lintEqual + 1)
					sValueControl = Replace(sValueControl, "%2F", "/")
					sValueControl = Replace(sValueControl, "%2C", ",")
					sValueControl = Replace(sValueControl, "%F3", "ó")
					sValueControl = Replace(sValueControl, "+", " ")
					sValueControl = Replace(sValueControl, "%3F", "?")
					sValueControl = Replace(sValueControl, "%BF", "¿")
					sValueControl = Replace(sValueControl, "%ED", "í")
					sValueControl = Replace(sValueControl, "%E9", "é")
					sValueControl = Replace(sValueControl, "%0D%0A", "&#13")
					sValueControl = Replace(sValueControl, "%3A", ":")
					sValueControl = Replace(sValueControl, "%E1", "á")
					sValueControl = Replace(sValueControl, "%FA", "ú")
					sValueControl = Replace(sValueControl, "%28", "(")
					sValueControl = Replace(sValueControl, "%29", ")")
					sValueControl = Replace(sValueControl, "%F1", "ñ")
					sValueControl = Replace(sValueControl, "%D1", "Ñ")
					sValueControl = Replace(sValueControl, "%A0", " ")
				End If
				insGetFields2 = sValueControl
			End If
		End If
	Next 
	
End Function

'% insGetFields: Asigna valor al campo, cuyo nombre recibe por parámetro, de el QueryString 
'%				 el Form o el Session
'--------------------------------------------------------------------------------------------    
Function insGetFields(ByRef sNameField As String, ByRef rArrayForm As String, ByRef rArrayQString As String, ByRef nSession_ind As Byte) As Object
	'--------------------------------------------------------------------------------------------         
	
	If CStr(Session(sNameField)) <> vbNullString And nSession_ind = 1 Then
		insGetFields = Session(sNameField)
	Else
		If CStr(insGetFields2(sNameField, rArrayQString, 1)) > vbNullString Then
			insGetFields = insGetFields2(sNameField, rArrayQString, 1)
		Else
			If CStr(insGetFields2(sNameField, rArrayForm, 2)) > vbNullString Then
				insGetFields = insGetFields2(sNameField, rArrayForm, 2)
			Else
				insGetFields = vbNullString
			End If
		End If
	End If
End Function
'% insGetFields: Se encarga de llamar a la función que verifica la existencia de tareas/Mensajes
'%               de agenda
'--------------------------------------------------------------------------------------------        
Function VerifyAgenda(ByRef sCodispl As String, ByRef sModules As Object, ByRef rArrayForm As String, ByRef rArrayQString As String) As Boolean
	'--------------------------------------------------------------------------------------------            
	Dim mobjValues As eFunctions.Values
	
	Dim nTransaction As Object
	Dim nIntermed As Object
	Dim sClient As Object
	Dim sCertype As Object
	Dim nBranch As Object
	Dim nProduct As Object
	Dim nPolicy As Object
	Dim nCertif As Object
	Dim dEffecdate As Object
	Dim nReceipt As Object
	Dim nLed_compan As Object
	Dim sAcc_ledger As Object
	Dim sAux_accoun As Object
	Dim nVoucher As Object
	Dim nNumber As Object
	Dim nCompany As Object
	Dim nContrat As Object
	Dim nAcc_cash As Object
	Dim nCashnum As Object
	Dim nAcc_bank As Object
	Dim nRequest_nu As Object
	Dim sSche_code As Object
	Dim nClaim As Object
	Dim nDeman_type As Object
	Dim nCase_num As Object
	Dim nTransac As Object
	
	'**+ The variable to handling the class Rules is defined
	'+ Se define variable para el manejo de la clase Rules
	'Dim lclsRules As eAgenda.Rules
	
	'lclsRules = New eAgenda.Rules
	mobjValues = New eFunctions.Values
	
	Select Case sModules
		Case "AG"
			nIntermed = insGetFields("nIntermed", rArrayForm, rArrayQString, 1)
			nTransaction = insGetFields("nTransaction", rArrayForm, rArrayQString, 1)
			VerifyAgenda = True
			'VerifyAgenda = lclsRules.FindAddAgenda(sCodispl, mobjValues.StringToType(nTransaction, eFunctions.Values.eTypeData.etdLong), Session("nUsercode"), sModules, mobjValues.StringToType(nIntermed, eFunctions.Values.eTypeData.etdLong))
		Case "BC"
			sClient = insGetFields("sClient", rArrayForm, rArrayQString, 1)
			nTransaction = insGetFields("nTransaction", rArrayForm, rArrayQString, 1)
			VerifyAgenda = True
			'VerifyAgenda = lclsRules.FindAddAgenda(sCodispl, mobjValues.StringToType(nTransaction, eFunctions.Values.eTypeData.etdLong), Session("nUsercode"), sModules,  , sClient)
		Case "CA"
			sClient = insGetFields("sClient", rArrayForm, rArrayQString, 2)
			sCertype = insGetFields("sCertype", rArrayForm, rArrayQString, 1)
			nBranch = insGetFields("nBranch", rArrayForm, rArrayQString, 1)
			nProduct = insGetFields("nProduct", rArrayForm, rArrayQString, 1)
			nPolicy = insGetFields("nPolicy", rArrayForm, rArrayQString, 1)
			nCertif = insGetFields("nCertif", rArrayForm, rArrayQString, 1)
			dEffecdate = insGetFields("dEffecdate", rArrayForm, rArrayQString, 1)
			nReceipt = insGetFields("nReceipt", rArrayForm, rArrayQString, 1)
			nTransaction = insGetFields("nTransaction", rArrayForm, rArrayQString, 1)
			VerifyAgenda = True
			'Call lclsRules.FindAddAgenda(sCodispl, mobjValues.StringToType(nTransaction, eFunctions.Values.eTypeData.etdLong), Session("nUsercode"), sModules,  , sClient, sCertype, mobjValues.StringToType(nBranch, eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(nProduct, eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(nPolicy, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(nCertif, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(dEffecdate, eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(nReceipt, eFunctions.Values.eTypeData.etdDouble))
		Case "CO"
			nBranch = insGetFields("nBranch", rArrayForm, rArrayQString, 1)
			nPolicy = insGetFields("nPolicy", rArrayForm, rArrayQString, 1)
			nCertif = insGetFields("nCertif", rArrayForm, rArrayQString, 1)
			dEffecdate = insGetFields("dEffecdate", rArrayForm, rArrayQString, 1)
			nReceipt = insGetFields("nReceipt", rArrayForm, rArrayQString, 1)
			nTransaction = insGetFields("nTransaction", rArrayForm, rArrayQString, 1)
			VerifyAgenda = True
			'VerifyAgenda = lclsRules.FindAddAgenda(sCodispl, mobjValues.StringToType(nTransaction, eFunctions.Values.eTypeData.etdLong), Session("nUsercode"), sModules,  ,  ,  , mobjValues.StringToType(nBranch, eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(nPolicy, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(nCertif, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(dEffecdate, eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(nReceipt, eFunctions.Values.eTypeData.etdDouble))
		Case "CP"
			dEffecdate = insGetFields("dEffecdate", rArrayForm, rArrayQString, 1)
			nLed_compan = insGetFields("nLed_compan", rArrayForm, rArrayQString, 1)
			sAcc_ledger = insGetFields("sAcc_ledger", rArrayForm, rArrayQString, 1)
			sAux_accoun = insGetFields("sAux_accoun", rArrayForm, rArrayQString, 1)
			nVoucher = insGetFields("nVoucher", rArrayForm, rArrayQString, 1)
			nTransaction = insGetFields("nTransaction", rArrayForm, rArrayQString, 1)
			VerifyAgenda = True
			'VerifyAgenda = lclsRules.FindAddAgenda(sCodispl, mobjValues.StringToType(nTransaction, eFunctions.Values.eTypeData.etdLong), Session("nUsercode"), sModules,  ,  ,  ,  ,  ,  ,  , mobjValues.StringToType(dEffecdate, eFunctions.Values.eTypeData.etdDate),  , mobjValues.StringToType(nLed_compan, eFunctions.Values.eTypeData.etdLong), sAcc_ledger, sAux_accoun, mobjValues.StringToType(nVoucher, eFunctions.Values.eTypeData.etdLong))
		Case "CR"
			dEffecdate = insGetFields("dEffecdate", rArrayForm, rArrayQString, 1)
			nNumber = insGetFields("nNumber", rArrayForm, rArrayQString, 1)
			nCompany = insGetFields("nCompany", rArrayForm, rArrayQString, 1)
			nTransaction = insGetFields("nTransaction", rArrayForm, rArrayQString, 1)
			VerifyAgenda = True
			'VerifyAgenda = lclsRules.FindAddAgenda(sCodispl, mobjValues.StringToType(nTransaction, eFunctions.Values.eTypeData.etdLong), Session("nUsercode"), sModules,  ,  ,  ,  ,  ,  ,  , mobjValues.StringToType(dEffecdate, eFunctions.Values.eTypeData.etdDate),  ,  ,  ,  ,  ,  , mobjValues.StringToType(nNumber, eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(nCompany, eFunctions.Values.eTypeData.etdLong))
		Case "DP"
			nBranch = insGetFields("nBranch", rArrayForm, rArrayQString, 1)
			nProduct = insGetFields("nProduct", rArrayForm, rArrayQString, 1)
			nTransaction = insGetFields("nTransaction", rArrayForm, rArrayQString, 1)
			VerifyAgenda = True
			'VerifyAgenda = lclsRules.FindAddAgenda(sCodispl, mobjValues.StringToType(nTransaction, eFunctions.Values.eTypeData.etdLong), Session("nUsercode"), sModules,  ,  ,  , mobjValues.StringToType(nBranch, eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(nProduct, eFunctions.Values.eTypeData.etdLong))
		Case "FI"
			nContrat = insGetFields("nContrat", rArrayForm, rArrayQString, 1)
			nTransaction = insGetFields("nTransaction", rArrayForm, rArrayQString, 1)
			VerifyAgenda = True
			'VerifyAgenda = lclsRules.FindAddAgenda(sCodispl, mobjValues.StringToType(nTransaction, eFunctions.Values.eTypeData.etdLong), Session("nUsercode"), sModules,  ,  ,  ,  ,  ,  ,  ,  ,  ,  ,  ,  ,  , mobjValues.StringToType(nContrat, eFunctions.Values.eTypeData.etdLong))
		Case "GE"
			nIntermed = insGetFields("nIntermed", rArrayForm, rArrayQString, 2)
			sClient = insGetFields("sClient", rArrayForm, rArrayQString, 2)
			sCertype = insGetFields("sCertype", rArrayForm, rArrayQString, 2)
			nBranch = insGetFields("nBranch", rArrayForm, rArrayQString, 2)
			nProduct = insGetFields("nProduct", rArrayForm, rArrayQString, 2)
			nPolicy = insGetFields("nPolicy", rArrayForm, rArrayQString, 2)
			nCertif = insGetFields("nCertif", rArrayForm, rArrayQString, 2)
			dEffecdate = insGetFields("dEffecdate", rArrayForm, rArrayQString, 2)
			nTransaction = insGetFields("nTransaction", rArrayForm, rArrayQString, 2)
			VerifyAgenda = True
			'VerifyAgenda = lclsRules.FindAddAgenda(sCodispl, mobjValues.StringToType(nTransaction, eFunctions.Values.eTypeData.etdLong), Session("nUsercode"), sModules, mobjValues.StringToType(nIntermed, eFunctions.Values.eTypeData.etdLong), sClient, sCertype, mobjValues.StringToType(nBranch, eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(nProduct, eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(nPolicy, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(nCertif, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(dEffecdate, eFunctions.Values.eTypeData.etdDate))
		Case "OP"
			nAcc_cash = insGetFields("nAcc_cash", rArrayForm, rArrayQString, 1)
			nCashnum = insGetFields("nCashnum", rArrayForm, rArrayQString, 1)
			nAcc_bank = insGetFields("nAcc_bank", rArrayForm, rArrayQString, 1)
			nRequest_nu = insGetFields("nRequest_nu", rArrayForm, rArrayQString, 1)
			nTransaction = insGetFields("nTransaction", rArrayForm, rArrayQString, 1)
			VerifyAgenda = True
			'VerifyAgenda = lclsRules.FindAddAgenda(sCodispl, mobjValues.StringToType(nTransaction, eFunctions.Values.eTypeData.etdLong), Session("nUsercode"), sModules,  ,  ,  ,  ,  ,  ,  ,  ,  ,  ,  ,  ,  ,  ,  ,  , mobjValues.StringToType(nAcc_cash, eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(nCashnum, eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(nAcc_bank, eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(nRequest_nu, eFunctions.Values.eTypeData.etdLong))
		Case "SG"
			sSche_code = insGetFields("sSche_code", rArrayForm, rArrayQString, 1)
			nTransaction = insGetFields("nTransaction", rArrayForm, rArrayQString, 1)
			VerifyAgenda = True
			'VerifyAgenda = lclsRules.FindAddAgenda(sCodispl, mobjValues.StringToType(nTransaction, eFunctions.Values.eTypeData.etdLong), Session("nUsercode"), sModules,  ,  ,  ,  ,  ,  ,  ,  ,  ,  ,  ,  ,  ,  ,  ,  ,  ,  ,  ,  , sSche_code)
		Case "SI"
			sClient = insGetFields("sClient", rArrayForm, rArrayQString, 2)
			sCertype = insGetFields("sCertype", rArrayForm, rArrayQString, 1)
			nBranch = insGetFields("nBranch", rArrayForm, rArrayQString, 1)
			nProduct = insGetFields("nProduct", rArrayForm, rArrayQString, 1)
			nPolicy = insGetFields("nPolicy", rArrayForm, rArrayQString, 1)
			nCertif = insGetFields("nCertif", rArrayForm, rArrayQString, 1)
			dEffecdate = insGetFields("dEffecdate", rArrayForm, rArrayQString, 1)
			nClaim = insGetFields("nClaim", rArrayForm, rArrayQString, 1)
			nDeman_type = insGetFields("nDeman_type", rArrayForm, rArrayQString, 1)
			nCase_num = insGetFields("nCase_num", rArrayForm, rArrayQString, 1)
			nTransac = insGetFields("nTransac", rArrayForm, rArrayQString, 1)
			nTransaction = insGetFields("nTransaction", rArrayForm, rArrayQString, 1)
			If sCodispl = "SI021" And Not IsNumeric(Trim(nBranch)) Or Not IsNumeric(Trim(nProduct)) Then
				nBranch = insGetFields("nauxBranch", rArrayForm, rArrayQString, 1)
				nProduct = insGetFields("nauxProduct", rArrayForm, rArrayQString, 1)
			End If
            VerifyAgenda = True
			'VerifyAgenda = lclsRules.FindAddAgenda(sCodispl, mobjValues.StringToType(nTransaction, eFunctions.Values.eTypeData.etdLong), Session("nUsercode"), sModules,  , sClient, sCertype, mobjValues.StringToType(nBranch, eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(nProduct, eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(nPolicy, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(nCertif, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(dEffecdate, eFunctions.Values.eTypeData.etdDate),  ,  ,  ,  ,  ,  ,  ,  ,  ,  ,  ,  ,  , mobjValues.StringToType(nClaim, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(nDeman_type, eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(nCase_num, eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(nTransac, eFunctions.Values.eTypeData.etdLong))
		Case Else
			VerifyAgenda = False
	End Select
	
	'lclsRules = Nothing
	mobjValues = Nothing
End Function

'----------------------------------------------------------------------------------------------------------
Function VerifyStatistics(ByRef eEntryPoint As Object, ByRef sCodispl As String, ByRef nUserCode As Object, ByRef sModules As Object, ByRef rArrayForm As String, ByRef rArrayQString As String) As Boolean
	'----------------------------------------------------------------------------------------------------------
	VerifyStatistics = True
<%--    Dim mobjValues As eFunctions.Values
	
	Dim nAction As Object
	Dim nTransaction As Object
	Dim sClient As Object
	Dim sCertype As Object
	Dim nBranch As Object
	Dim nProduct As Object
	Dim nPolicy As Object
	Dim nCertif As Object
	Dim nContrat As Object
	Dim nClaim As Object
	Dim nCase_num As Object
	Dim nOffice As Object
	Dim nOfficeAgen As Object
	Dim nAgency As Object
	Dim lstrXMLStatistic As String
	
	'**+ The variable to handling the class Rules is defined
	'+ Se define variable para el manejo de la clase Rules
	Dim lclsStatisticType As eNetFrameWork.StatisticType
	
	lclsStatisticType = New eNetFrameWork.StatisticType
	mobjValues = New eFunctions.Values
	
	nAction = insGetFields("nAction", rArrayForm, rArrayQString, 1)
	
	nTransaction = vbNullString
	sClient = vbNullString
	sCertype = vbNullString
	nBranch = vbNullString
	nProduct = vbNullString
	nPolicy = vbNullString
	nCertif = vbNullString
	nContrat = vbNullString
	nClaim = vbNullString
	nCase_num = vbNullString
	nOffice = vbNullString
	nOfficeAgen = vbNullString
	nAgency = vbNullString
	
	If lclsStatisticType.FindCodispl_Cache(UCase(sCodispl)) Then
		If Mid(eEntryPoint, 1, 4) = "eIni" Then
			Select Case sModules
				Case "AG"
					nTransaction = insGetFields("nTransaction", rArrayForm, rArrayQString, 1)
					
				Case "BC"
					sClient = insGetFields("sClient", rArrayForm, rArrayQString, 1)
					nTransaction = insGetFields("nTransaction", rArrayForm, vbNullString, 1)
					
				Case "CA"
					sClient = insGetFields("nCode", rArrayForm, rArrayQString, 2)
					sCertype = insGetFields("sCertype", rArrayForm, rArrayQString, 1)
					nBranch = insGetFields("nBranch", rArrayForm, rArrayQString, 1)
					nProduct = insGetFields("nProduct", rArrayForm, rArrayQString, 1)
					nPolicy = insGetFields("nPolicy", rArrayForm, rArrayQString, 1)
					nCertif = insGetFields("nCertif", rArrayForm, rArrayQString, 1)
					nOffice = insGetFields("nOffice", rArrayForm, rArrayQString, 1)
					nOfficeAgen = insGetFields("nOfficeAgen", rArrayForm, rArrayQString, 1)
					nAgency = insGetFields("nAgency", rArrayForm, rArrayQString, 1)
					nTransaction = insGetFields("nTransaction", rArrayForm, rArrayQString, 1)
					
				Case "CO"
					nBranch = insGetFields("nBranch", rArrayForm, rArrayQString, 1)
					nPolicy = insGetFields("nPolicy", rArrayForm, rArrayQString, 1)
					nCertif = insGetFields("nCertif", rArrayForm, rArrayQString, 1)
					nTransaction = insGetFields("nTransaction", rArrayForm, rArrayQString, 1)
					
				Case "CP"
					nTransaction = insGetFields("nTransaction", rArrayForm, rArrayQString, 1)
					
				Case "CR"
					nTransaction = insGetFields("nTransaction", rArrayForm, rArrayQString, 1)
					
				Case "DP"
					nBranch = insGetFields("nBranch", rArrayForm, rArrayQString, 1)
					nProduct = insGetFields("nProduct", rArrayForm, rArrayQString, 1)
					nTransaction = insGetFields("nTransaction", rArrayForm, rArrayQString, 1)
					
				Case "FI"
					nContrat = insGetFields("nContrat", rArrayForm, rArrayQString, 1)
					nTransaction = insGetFields("nTransaction", rArrayForm, rArrayQString, 1)
					
				Case "GE"
					sClient = insGetFields("sClient", rArrayForm, rArrayQString, 2)
					sCertype = insGetFields("sCertype", rArrayForm, rArrayQString, 2)
					nBranch = insGetFields("nBranch", rArrayForm, rArrayQString, 2)
					nProduct = insGetFields("nProduct", rArrayForm, rArrayQString, 2)
					nPolicy = insGetFields("nPolicy", rArrayForm, rArrayQString, 2)
					nCertif = insGetFields("nCertif", rArrayForm, rArrayQString, 2)
					nTransaction = insGetFields("nTransaction", rArrayForm, rArrayQString, 2)
					
				Case "OP"
					nTransaction = insGetFields("nTransaction", rArrayForm, rArrayQString, 1)
					
				Case "SG"
					nTransaction = insGetFields("nTransaction", rArrayForm, rArrayQString, 1)
					
				Case "SI"
					sClient = insGetFields("nClientCode", rArrayForm, rArrayQString, 2)
					sCertype = insGetFields("sCertype", rArrayForm, rArrayQString, 1)
					nBranch = insGetFields("nBranch", rArrayForm, rArrayQString, 1)
					nProduct = insGetFields("nProduct", rArrayForm, rArrayQString, 1)
					nPolicy = insGetFields("nPolicy", rArrayForm, rArrayQString, 1)
					nCertif = insGetFields("nCertif", rArrayForm, rArrayQString, 1)
					nClaim = insGetFields("nClaim", rArrayForm, rArrayQString, 1)
					nCase_num = insGetFields("nCase_num", rArrayForm, rArrayQString, 1)
					nTransaction = insGetFields("nTransaction", rArrayForm, vbNullString, 1)
			End Select
		End If
		
		Select Case eEntryPoint
			Case "eIniVal"
				lstrXMLStatistic = lclsStatisticType.XMLStream_Node("Validaciones", Session("sTime_Statistic"), nUserCode, "eIniVal", mobjValues.StringToType(nTransaction, eFunctions.Values.eTypeData.etdInteger), sCertype, mobjValues.StringToType(nOffice, eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(nOfficeAgen, eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(nAgency, eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(nBranch, eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(nProduct, eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(nPolicy, eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(nCertif, eFunctions.Values.eTypeData.etdLong), sClient, mobjValues.StringToType(nClaim, eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(nCase_num, eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(nContrat, eFunctions.Values.eTypeData.etdLong))
				VerifyStatistics = lclsStatisticType.xMLStream_Statistic(lstrXMLStatistic, nUserCode)
			Case "eEndVal"
				lstrXMLStatistic = lclsStatisticType.XMLStream_Node("Validaciones", Session("sTime_Statistic"), nUserCode, "eEndVal", mobjValues.StringToType(nTransaction, eFunctions.Values.eTypeData.etdInteger), sCertype, mobjValues.StringToType(nOffice, eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(nOfficeAgen, eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(nAgency, eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(nBranch, eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(nProduct, eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(nPolicy, eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(nCertif, eFunctions.Values.eTypeData.etdLong), sClient, mobjValues.StringToType(nClaim, eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(nCase_num, eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(nContrat, eFunctions.Values.eTypeData.etdLong))
				VerifyStatistics = lclsStatisticType.xMLStream_Statistic(lstrXMLStatistic, nUserCode)
			Case "eIniPost"
				lstrXMLStatistic = lclsStatisticType.XMLStream_Node("Actualizaciones", Session("sTime_Statistic"), nUserCode, "eIniPost", mobjValues.StringToType(nTransaction, eFunctions.Values.eTypeData.etdInteger), sCertype, mobjValues.StringToType(nOffice, eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(nOfficeAgen, eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(nAgency, eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(nBranch, eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(nProduct, eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(nPolicy, eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(nCertif, eFunctions.Values.eTypeData.etdLong), sClient, mobjValues.StringToType(nClaim, eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(nCase_num, eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(nContrat, eFunctions.Values.eTypeData.etdLong))
				VerifyStatistics = lclsStatisticType.xMLStream_Statistic(lstrXMLStatistic, nUserCode)
			Case "eEndPost"
				If nAction = "392" And (lclsStatisticType.nStatisticType = 3 Or lclsStatisticType.nStatisticType = 4 Or lclsStatisticType.nStatisticType = 5) Then
					lstrXMLStatistic = lclsStatisticType.XMLStream_Node("Salvar", Session("sTime_Statistic"), nUserCode, "eEndPost", mobjValues.StringToType(nTransaction, eFunctions.Values.eTypeData.etdInteger), sCertype, mobjValues.StringToType(nOffice, eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(nOfficeAgen, eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(nAgency, eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(nBranch, eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(nProduct, eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(nPolicy, eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(nCertif, eFunctions.Values.eTypeData.etdLong), sClient, mobjValues.StringToType(nClaim, eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(nCase_num, eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(nContrat, eFunctions.Values.eTypeData.etdLong))
					VerifyStatistics = lclsStatisticType.xMLStream_Statistic(lstrXMLStatistic, nUserCode)
					VerifyStatistics = lclsStatisticType.xMLStream_Statistic("</Comienzo_Nodo>", nUserCode)
					VerifyStatistics = lclsStatisticType.xMLStream_Statistic("</Comienzo_Transaccion>", nUserCode)
					Session("sTime_Statistic") = vbNullString
				Else
					lstrXMLStatistic = lclsStatisticType.XMLStream_Node("Actualizaciones", Session("sTime_Statistic"), nUserCode, "eEndPost", mobjValues.StringToType(nTransaction, eFunctions.Values.eTypeData.etdInteger), sCertype, mobjValues.StringToType(nOffice, eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(nOfficeAgen, eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(nAgency, eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(nBranch, eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(nProduct, eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(nPolicy, eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(nCertif, eFunctions.Values.eTypeData.etdLong), sClient, mobjValues.StringToType(nClaim, eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(nCase_num, eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(nContrat, eFunctions.Values.eTypeData.etdLong))
					VerifyStatistics = lclsStatisticType.xMLStream_Statistic(lstrXMLStatistic, nUserCode)
					VerifyStatistics = lclsStatisticType.xMLStream_Statistic("</Comienzo_Nodo>", nUserCode)
				End If
		End Select
	End If
	VerifyStatistics = True
	lclsStatisticType = Nothing
	mobjValues = Nothing
--%>

End Function
'----------------------------------------------------------------------------------------------------------
Function insCommonFunction(ByVal sNamePage As Object, ByVal sCodispl As String, ByVal eEntryPoint As Object, ByVal rArrayForm As String, ByVal rArrayQString As String, ByVal rArraySession As Object, ByVal sModules As Object) As Object
	'----------------------------------------------------------------------------------------------------------
	Dim blnFindAddAgenda As Boolean
	Dim blnVerifyStatistics As Boolean
    Dim mobjNetFrameWork As eNetFrameWork.Layout

    mobjNetFrameWork = New eNetFrameWork.Layout
	
	
	Select Case eEntryPoint
		
		Case eIniVal
		    Call mobjNetFrameWork.BeginPage(sNamePage & "-Val")
			blnVerifyStatistics = VerifyStatistics("eIniVal", sCodispl, Session("nUserCode"), sModules, rArrayForm, rArrayQString)
		Case eEndVal
			blnVerifyStatistics = VerifyStatistics("eEndVal", sCodispl, Session("nUserCode"), sModules, rArrayForm, rArrayQString)
			Call mobjNetFrameWork.FinishPage(sNamePage & "-Val")
		Case eIniPost
		    Call mobjNetFrameWork.BeginPage(sNamePage & "-Post")
			blnVerifyStatistics = VerifyStatistics("eIniPost", sCodispl, Session("nUserCode"), sModules, rArrayForm, rArrayQString)
		Case eEndPost
			blnVerifyStatistics = VerifyStatistics("eEndPost", sCodispl, Session("nUserCode"), sModules, rArrayForm, rArrayQString)
			blnFindAddAgenda = VerifyAgenda(sCodispl, sModules, rArrayForm, rArrayQString)
			Call mobjNetFrameWork.FinishPage(sNamePage & "-Post")
			
                ' Disponible luego de la integración con el portal. mRojas 19/07/2009		
                'If GIT.visualtime.integration.WorkFlow.IsWorkflowActived() Then
                '    GIT.visualtime.integration.WorkFlow.ExecuteVTimeWorkflow(sCodispl)
                'End If			
			
	End Select
	
End Function

</script>








