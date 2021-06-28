<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="ePolicy" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 10.27.21
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

'- Variable que guarda los errors devueltos por el InsValPolicyQue
Dim mstrErrors As String

'- Objeto para el manejo de las funciones generales
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo de las rutinas de VAL y POST de las transacciones
Dim mobjPolicyQue As Object

'- Variable auxiliar para pase de valores del encabezado al folder
Dim mstrQueryString As String

'- Se define la contante para el manejo de errores en caso de advertencias
Dim mstrCommand As String


'% InsValPolicyQue: Se realizan las validaciones de las formas
'--------------------------------------------------------------------------------------------
Function InsValPolicyQue() As String
	'--------------------------------------------------------------------------------------------
	Select Case Request.QueryString.Item("sCodispl")
		
		'+ CAC002: Consulta de Base de datos de vehiculos
		Case "CAC002"
			With Request
				InsValPolicyQue = ""
			End With
			
			'+ CAC003: Consulta de pólizas o recibos pendientes de impresión
		Case "CAC003"
			InsValPolicyQue = ""
			
			'+ CAC005: Consulta de Ubicación del riesgo.
		Case "CAC005"
			With Request
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					mobjPolicyQue = New ePolicy.Policy
					InsValPolicyQue = mobjPolicyQue.InsValCAC005_K("CAC005", mobjValues.StringToType(.Form.Item("cbeProvince"), eFunctions.Values.eTypeData.etdDouble, True))
				End If
			End With
			
			'+ CAC001: Consulta de Ubicación del riesgo.
		Case "CAC001"
			If CDbl(Request.QueryString.Item("nZone")) = 1 Then
				mobjPolicyQue = New ePolicy.Certificat
				With Request
					InsValPolicyQue = mobjPolicyQue.insValCAC001_k("CAC001", .Form.Item("optCertype"), mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valCurrency"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("optExecute"))
					mobjPolicyQue = Nothing
				End With
			End If
			
			'+ CAC011: Historia de una poliza
		Case "CAC011"
			With Request
				If .QueryString.Item("nZone") = "1" Then
					mobjPolicyQue = New ePolicy.Policy_his
					InsValPolicyQue = mobjPolicyQue.InsValCAC011(.QueryString("sCodispl"), .Form.Item("cbeCertype"), mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnCertif"), eFunctions.Values.eTypeData.etdDouble, True))
					mobjPolicyQue = Nothing
				End If
			End With
			
			'+ VAC610: Desglose de un movimiento del valor póliza
		Case "VAC610"
			With Request
				If .QueryString.Item("nZone") = "1" Then
					mobjPolicyQue = New ePolicy.Move_accpol
					
					If CStr(Session("sPoliType")) = "1" Then
						InsValPolicyQue = mobjPolicyQue.insValVAC610_K(.QueryString("sCodispl"), mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble, True), 0, mobjValues.StringToType(.Form.Item("tcnMovement"), eFunctions.Values.eTypeData.etdDouble, True))
					Else
						InsValPolicyQue = mobjPolicyQue.insValVAC610_K(.QueryString("sCodispl"), mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnMovement"), eFunctions.Values.eTypeData.etdDouble, True))
					End If
					
					mobjPolicyQue = Nothing
				End If
			End With
			
			'+ VAC609: Consulta de Valor Póliza
		Case "VAC609"
			With Request
				If .QueryString.Item("nZone") = "1" Then
					mobjPolicyQue = New ePolicy.Move_accpol
					InsValPolicyQue = mobjPolicyQue.insValVAC609_K(.QueryString("sCodispl"), mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnCertif"), eFunctions.Values.eTypeData.etdDouble))
					mobjPolicyQue = Nothing
				End If
			End With
			
			'+ VIC732: Consulta de valores garantizados
		Case "VIC732"
			With Request
				If .QueryString.Item("nZone") = "1" Then
					mobjPolicyQue = New ePolicy.Guarant_val
					InsValPolicyQue = mobjPolicyQue.insvalVIC732(.QueryString("sCodispl"), mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate))
				End If
			End With
			
			'+ CAC958: Consulta Bitacora de pólizas incompletas
		Case "CAC958"
			With Request
				If .QueryString.Item("nZone") = "1" Then
					mobjPolicyQue = New ePolicy.wait_code_hist
					InsValPolicyQue = mobjPolicyQue.insValCAC958(.QueryString("sCodispl"), .Form.Item("tctCertype"), mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnCertif"), eFunctions.Values.eTypeData.etdDouble, True))
					mobjPolicyQue = Nothing
				End If
				
			End With
			
			'+ SOC001: Consulta de Distribucion de folios.
		Case "SOC001"
			With Request
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					mobjPolicyQue = New ePolicy.Folios_Agent
					InsValPolicyQue = mobjPolicyQue.InsValSOC001_K(.QueryString("sCodispl"), mobjValues.StringToType(.Form.Item("tcnYear"), eFunctions.Values.eTypeData.etdInteger, True))
				End If
			End With
		Case Else
			InsValPolicyQue = "InsValPolicyQue: Código lógico no encontrado (" & Request.QueryString.Item("sCodispl") & ")"
	End Select
End Function

'% insPostPolicyQue: Se realizan las actualizaciones a las tablas
'--------------------------------------------------------------------------------------------
Function insPostPolicyQue() As Boolean
	'--------------------------------------------------------------------------------------------
	Dim lblnPost As Boolean
	
	lblnPost = True
	
	Select Case Request.QueryString.Item("sCodispl")
		
		'+ CAC002: Consulta de Base de datos de vehiculos
		Case "CAC002"
			With Request
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					Session("nOffice") = .Form.Item("valOffice")
					Session("nBranch") = .Form.Item("cbeBranch")
					Session("nProduct") = .Form.Item("valProduct")
					Session("nInterm") = .Form.Item("valIntermed")
					Session("nOption") = .Form.Item("option")
				End If
			End With
			
			'+ CAC003: Consulta de pólizas o recibos pendientes de impresión
		Case "CAC003"
			With Request
				Session("nOffice") = .Form.Item("cbeOffice")
				Session("nBranch") = .Form.Item("cbeBranch")
				Session("nOption") = .Form.Item("optPolicy")
			End With
			
			'+ CAC005: Consulta de Ubicación del riesgo.
		Case "CAC005"
			With Request
				mstrQueryString = "&nProvince=" & .Form.Item("cbeProvince") & "&nLocal=" & .Form.Item("valLocal") & "&nMunicipality=" & .Form.Item("valMunicipality") & "&dEffecdate=" & .Form.Item("tcdEffecdate") & "&sCondition=" & .Form.Item("sBranchCondition")
			End With
			
		Case "CAC001"
			With Request
				mstrQueryString = "&sCertype=" & .Form.Item("optCertype") & "&sState=" & .Form.Item("optExecute") & "&nBranch=" & .Form.Item("cbeBranch") & "&nPolicy=" & .Form.Item("tcnPolicy") & "&nProduct=" & .Form.Item("valProduct") & "&nCurrrent=" & .Form.Item("valCurrency") & "&dStartdate=" & .Form.Item("tcdEffecdate") & "&sCreditnum=" & .Form.Item("tctCreditnum") & "&sAccnum=" & .Form.Item("tctAccnum") & "&sClient=" & .Form.Item("tctClient")
			End With
			
			'+ CAC011: Historia de una poliza
		Case "CAC011"
			With Request
				If .QueryString.Item("nZone") = "1" Then
					mstrQueryString = "&sCertype=" & .Form.Item("cbeCertype") & "&nBranch=" & .Form.Item("cbeBranch") & "&nProduct=" & .Form.Item("valProduct") & "&nPolicy=" & .Form.Item("tcnPolicy") & "&nCertif=" & .Form.Item("tcnCertif") & "&dEffecdate=" & .Form.Item("tcdEffecdate") & "&nType_amend=" & .Form.Item("valTypeAmend") & "&hddPolicy=" & .Form.Item("hddPolicy") & "&nTransaction=" & .Form.Item("hddTransaction")
					
				End If
			End With
			
			'+ VAC610: Desglose de un movimiento del valor póliza
		Case "VAC610"
			With Request
				If .QueryString.Item("nZone") = "1" Then
					mstrQueryString = "&sCertype=2" & "&nBranch=" & .Form.Item("cbeBranch") & "&nProduct=" & .Form.Item("valProduct") & "&nPolicy=" & .Form.Item("tcnPolicy") & "&dEffecdate=" & .Form.Item("tcdEffecdate") & "&nMovement=" & .Form.Item("tcnMovement") & "&sMoveType=" & Session("sMoveType") & "&dMoveDate=" & Session("dMoveDate") & "&nDivAmount=" & Session("nDivAmount") & "&nDivReceipt=" & Session("nDivReceipt") & "&sDivCurrency=" & Session("sDivCurrency")
					
					If CStr(Session("sPoliType")) = "1" Then
						mstrQueryString = mstrQueryString & "&nCertif=0"
					Else
						mstrQueryString = mstrQueryString & "&nCertif=" & .Form.Item("tcnCertif")
					End If
					
				End If
			End With
			
			'+ VAC609: Consulta de Valor Poliza
		Case "VAC609"
			With Request
				If .QueryString.Item("nZone") = "1" Then
					mstrQueryString = "&sCertype=2"
					mstrQueryString = mstrQueryString & "&nBranch=" & .Form.Item("cbeBranch")
					mstrQueryString = mstrQueryString & "&nProduct=" & .Form.Item("valProduct")
					mstrQueryString = mstrQueryString & "&nPolicy=" & .Form.Item("tcnPolicy")
					If Not (IsNothing(.Form.Item("tcnCertif")) Or IsDbNull(.Form.Item("tcnCertif")) Or IsNothing(.Form.Item("tcnCertif"))) Then
						mstrQueryString = mstrQueryString & "&nCertif=" & .Form.Item("tcnCertif")
					Else
						mstrQueryString = mstrQueryString & "&nCertif=0"
					End If
					mstrQueryString = mstrQueryString & "&dMovedate=" & .Form.Item("tcdMoveDate")
				End If
			End With
			
			'+ VIC732: Consulta de valores garantizados
		Case "VIC732"
			With Request
				mstrQueryString = "&nBranch=" & .Form.Item("cbeBranch") & "&nProduct=" & .Form.Item("valProduct") & "&nPolicy=" & .Form.Item("tcnPolicy") & "&nCertif=" & .Form.Item("tcnCertif") & "&dEffecdate=" & .Form.Item("tcdEffecdate")
			End With
			
			'+ CAC958: Consulta de Base de datos de vehiculos
		Case "CAC958"
			With Request
				mstrQueryString = "&nBranch=" & .Form.Item("cbeBranch") & "&nProduct=" & .Form.Item("valProduct") & "&nPolicy=" & .Form.Item("tcnPolicy") & "&nCertif=" & .Form.Item("tcnCertif") & "&sCertype=" & .Form.Item("tctCertype")
				
			End With
			'+ SOC001: Consulta de distribucion de folios
		Case "SOC001"
			With Request
				If .QueryString.Item("nZone") = "1" Then
					mstrQueryString = "&nYear=" & .Form.Item("tcnYear")
				End If
			End With
	End Select
	
	insPostPolicyQue = lblnPost
End Function

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("valpolicyque")

mstrCommand = "&sModule=Policy&sProject=PolicyQue&sCodisplReload=" & Request.QueryString.Item("sCodispl")

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.27.21
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "valpolicyque"
%>
<HTML>
<HEAD>
	<LINK REL="StyleSheet" TYPE="text/css" HREF="/VTimeNet/Common/Custom.css">
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
<SCRIPT SRC="/VTimeNet/Scripts/GenFunctions.js"> </SCRIPT>



		
</HEAD>
<BODY>
<%
'+ Si no se han validado los campos de la página...
If Request.Form.Item("sCodisplReload") = vbNullString Then
	mstrErrors = InsValPolicyQue
	Session("sErrorTable") = mstrErrors
	Session("sForm") = Request.Form.ToString
Else
	Session("sErrorTable") = vbNullString
	Session("sForm") = vbNullString
End If

If mstrErrors > vbNullString Then
	With Response
		.Write("<SCRIPT LANGUAGE=JAVASCRIPT>")
		.Write("ShowPopUp(""/VTimeNet/Common/Errors.aspx?sForm=" & Server.URLEncode(Request.Form.ToString) & Server.URLEncode(mstrCommand) & "&sQueryString=" & Server.URLEncode(Request.Params.Get("Query_String")) & """,""PolicyQueError"",660,330);document.location.href='/VTimeNet/common/blank.htm';")
		.Write(mobjValues.StatusControl(False, CShort(Request.QueryString.Item("nZone")), Request.QueryString.Item("WindowType")))
		.Write("</SCRIPT>")
	End With
Else
	If insPostPolicyQue() Then
		If Request.QueryString.Item("WindowType") <> "PopUp" Then
			If Request.QueryString.Item("nAction") = CStr(eFunctions.Menues.TypeActions.clngAcceptdataFinish) Then
				Response.Write("<SCRIPT>top.document.location.reload();</SCRIPT>")
			Else
				If Request.QueryString.Item("sCodispl") <> "OP004" Then
					Response.Write("<SCRIPT>top.fraFolder.document.location=""" & Replace(UCase(Request.QueryString.Item("sCodispl")), "_K", "") & ".aspx?nMainAction=" & Request.QueryString.Item("nMainAction") & mstrQueryString & """;</SCRIPT>")
				Else
					Response.Write("<SCRIPT>insReloadTop(true,false)</SCRIPT>")
				End If
			End If
		End If
	End If
End If

mobjValues = Nothing
mobjPolicyQue = Nothing
%>
</BODY>
</HTML>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 10.27.21
Call mobjNetFrameWork.FinishPage("valpolicyque")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




