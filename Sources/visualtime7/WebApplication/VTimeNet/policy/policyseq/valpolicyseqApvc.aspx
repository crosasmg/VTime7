<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="ePolicy" %>
<%@ Import namespace="eApvc" %>
<script language="VB" runat="Server">
'Dim insFinish() As Boolean
Dim mobjPolicySeq As ePolicy.ValPolicySeq
Dim mstrErrors As String
Dim mstrLocationCA001 As String
Dim mobjValues As eFunctions.Values
Dim lclsPolicy As Object
Dim mstrScript As String
Dim lintCurrency As Object
Dim llngPayfreq As Object

'- Se define la contante para el manejo de errores en caso de advertencias
Dim mstrCommand As String

'- Variable para el manejo del QueryString  
Dim mstrQueryString As String

Dim mblnCreateInsured As Object

'-Variable para indicar si ya se ejecutaron las validaciones
Dim mblnReload As Boolean
Dim lclsRefresh As ePolicy.ValPolicySeq
Dim mstrTotalPrima As Double


'% insvalSequence: Se realizan las validaciones masivas de la forma
'--------------------------------------------------------------------------------------------
Function insvalSequence() As String
	Dim sActivefound As String
	Dim sActivefound_P As String
	'--------------------------------------------------------------------------------------------
	Dim lintIntermedia As Object
	Dim lintIntermediaOld As Object
	Dim lstrClient As Object
	Dim lstrClientOld As Object
	
	'    mobjNetFrameWork.BeginProcess "ValSequence|" & Request.QueryString("sCodispl")
	Dim mobjApvc As eApvc.Life_Apvc
	Dim lclsFunds_Pol As ePolicy.Funds_Pol
	Dim lclsPolicy_Win As ePolicy.Policy_Win
	Dim lclsFunds_CO_P As eApvc.Funds_CO_P
	Dim Life_Apvc As eApvc.Life_Apvc
	Select Case Request.QueryString.Item("sCodispl")
		'+ VI641: Criterios para seleción de riesgo
		Case "CA001"
			Session("PageRetCA050") = "CA001"
			With Request
				insvalSequence = mobjPolicySeq.insValCA001(.QueryString.Item("sCodispl"), mobjValues.StringToType(.Form.Item("cbeTransactio"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("cbeOffice"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPolicyDest"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCertificat"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("optType"), mobjValues.StringToType(.Form.Item("tcdLedgerDate"), eFunctions.Values.eTypeData.etdDate), Session("nUsercode"), mobjValues.StringToType(.Form.Item("tcdExpDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("cbeAgency"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeOfficeAgen"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeSellChannel"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valType_amend"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnServ_order"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnQuotProp"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnDigit"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnProp_reg"), eFunctions.Values.eTypeData.etdDouble))
				
				'            Response.Write "<NOTSCRIPT>alert('" & insvalSequence & "');</" & "Script>"
				'APVC INCLUCION DE VALIDACION INICO
				If insvalSequence = vbNullString Then
					mobjApvc = New eApvc.Life_Apvc
					insvalSequence = mobjApvc.insValCA001("0", mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCertificat"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate), .QueryString.Item("sCodispl"), Session("nUsercode"), mobjValues.StringToType(.Form.Item("cbeTransactio"), eFunctions.Values.eTypeData.etdDouble, True))
					
					mobjApvc = Nothing
				End If
				'APVC INCLUCION DE VALIDACION FIN 
				
				
				
				
			End With
			
			
			
			'**+ VI006:	Investments Funds.
			'+ VI006: Fondos de inversiones.
			
		Case "VI006"
			
			
			lclsFunds_Pol = New ePolicy.Funds_Pol
			
			With Request
				If .QueryString.Item("WindowType") = "PopUp" Then
					If CBool(.Form.Item("chkActivFound")) Then
						sActivefound = "1"
					Else
						sActivefound = "2"
					End If
					
					insvalSequence = vbNullString
					
					insvalSequence = lclsFunds_Pol.insValVI006(.QueryString.Item("sCodispl"), .Form.Item("Sel"), "Popup", mobjValues.StringToType(.Form.Item("tcnFunds"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPartic_min"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnParticip"), eFunctions.Values.eTypeData.etdDouble), Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), Session("nTransaction"), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), "2", sActivefound, mobjValues.StringToType(.Form.Item("tcnOrigin"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnIntProy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnIntProyVar"), eFunctions.Values.eTypeData.etdDouble))
					
					lclsPolicy_Win = New ePolicy.Policy_Win
					
					Call lclsPolicy_Win.Add_PolicyWin(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), Session("nUsercode"), "VI006", "1")
					lclsPolicy_Win = Nothing
				Else
					
					insvalSequence = lclsFunds_Pol.insValVI006(.QueryString.Item("sCodispl"), .Form.Item("Sel"),  ,  ,  ,  , Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), Session("nTransaction"), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), vbNullString, vbNullString)
					lclsPolicy_Win = New ePolicy.Policy_Win
					
					Call lclsPolicy_Win.Add_PolicyWin(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), Session("nUsercode"), "VI006", "1")
					lclsPolicy_Win = Nothing
					
				End If
				
				lclsFunds_Pol = Nothing
			End With
			
			'+ VI006A: Fondos de inversiones por póliza matríz.
		Case "VI006A"
			
			
			lclsFunds_CO_P = New eApvc.Funds_CO_P
			
			With Request
				If .QueryString.Item("WindowType") = "PopUp" Then
					If CBool(.Form.Item("chkActivFound")) Then
						sActivefound_P = "1"
					Else
						sActivefound_P = "2"
					End If
					
					insvalSequence = vbNullString
					
					insvalSequence = lclsFunds_CO_P.insValVI006A(.QueryString.Item("sCodispl"), .Form.Item("Sel"), "Popup", mobjValues.StringToType(.Form.Item("tcnFunds"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPartic_min"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnParticip"), eFunctions.Values.eTypeData.etdDouble), Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), Session("nTransaction"), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), "2", sActivefound_P, mobjValues.StringToType(.Form.Item("tcnOrigin"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnIntProy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnIntProyVar"), eFunctions.Values.eTypeData.etdDouble))
					
					
					lclsPolicy_Win = New ePolicy.Policy_Win
					
					Call lclsPolicy_Win.Add_PolicyWin(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), Session("nUsercode"), "VI006A", "1")
					lclsPolicy_Win = Nothing
					
				Else
					
					insvalSequence = lclsFunds_CO_P.insValVI006A(.QueryString.Item("sCodispl"), .Form.Item("Sel"),  ,  ,  ,  , Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), Session("nTransaction"), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), vbNullString, vbNullString)
					
					lclsPolicy_Win = New ePolicy.Policy_Win
					
					Call lclsPolicy_Win.Add_PolicyWin(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), Session("nUsercode"), "VI006A", "1")
					lclsPolicy_Win = Nothing
				End If
				
			End With
			lclsFunds_CO_P = Nothing
			
			'+ CA200: datos particulares apvc.
		Case "CA200" ' Session("nTransaction"),           
			
			Life_Apvc = New eApvc.Life_Apvc
			With Request
				
				insvalSequence = Life_Apvc.insValca200(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tctnpercentsalary"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tctnPrem_max"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tctnPrem_min"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("chkspremium"), mobjValues.StringToType(.Form.Item("tctnMinstay"), eFunctions.Values.eTypeData.etdLong), 0, mobjValues.StringToType(.Form.Item("cbeBankExt"), eFunctions.Values.eTypeData.etdLong), .Form.Item("tctsAccount"), mobjValues.StringToType(.Form.Item("tctnPremiumc"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(.Form.Item("tctnPercentiumc"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbenTyp_profitworker"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(Session("nusercode"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(.Form.Item("cbenCurrencyempl"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(.Form.Item("tctnAmountpren"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tctnStay"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(.Form.Item("tctnamountsalary"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(.Form.Item("tctnPercentnprent"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(.Form.Item("cbenCurrencywork"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(.Form.Item("cbeTyp_Account"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(.Form.Item("valOption"), eFunctions.Values.eTypeData.etdDouble, True))
				
				'                                                 
				lclsPolicy_Win = New ePolicy.Policy_Win
				
				Call lclsPolicy_Win.Add_PolicyWin(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), Session("nUsercode"), "CA200", "1")
				lclsPolicy_Win = Nothing
				
			End With
			Life_Apvc = Nothing
		Case Else
			insvalSequence = "insvalSequence: Código lógico no encontrado (" & Request.QueryString.Item("sCodispl") & ")"
			
	End Select
	'mobjNetFrameWork.FinishProcess "ValSequence|" & Request.QueryString("sCodispl")
End Function

'% insPostSequence: Se realizan las actualizaciones de las ventanas
'--------------------------------------------------------------------------------------------
Function insPostSequence() As Boolean
	Dim sActivefound As String
	Dim sActivefound_P As String
	'--------------------------------------------------------------------------------------------
	Dim lintIntermedia As Object
	Dim lintIntermediaOld As Object
	Dim lstrClient As Object
	Dim lstrClientOld As Object
	Dim lblnPost As Boolean
	Dim lclsPolicy_Win As ePolicy.Policy_Win
	Dim lclsErrors As Object
	Dim lobjDocuments As Object
	Dim llngTariff As Object
	
	lblnPost = True
	
	
	'    mobjNetFrameWork.BeginProcess "PostSequence|" & Request.QueryString("sCodispl")
	Dim lclsFunds_Pol As ePolicy.Funds_Pol
	Dim lclsFunds_CO_P As eApvc.Funds_CO_P
	Dim Life_Apvc As eApvc.Life_Apvc
	Select Case Request.QueryString.Item("sCodispl")
		
		
		'**+ VI006:	Investments Funds.
		'+ VI006: Fondos de inversiones.
		
		Case "VI006"
			
			lclsFunds_Pol = New ePolicy.Funds_Pol
			
			With Request
				If .QueryString.Item("WindowType") = "PopUp" Then
					If CBool(.Form.Item("chkActivFound")) Then
						sActivefound = "1"
					Else
						sActivefound = "2"
					End If
					
					Call lclsFunds_Pol.insPostVI006(.QueryString.Item("sCodispl"), .QueryString.Item("Action"), mobjValues.StringToType(.Form.Item("tcnFunds"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnParticip"), eFunctions.Values.eTypeData.etdDouble), Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), Session("nUsercode"), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("dNulldate"), eFunctions.Values.eTypeData.etdDate), Session("nTransaction"), sActivefound, "2", mobjValues.StringToType(.Form.Item("tcnOrigin"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnIntProy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnIntProyVar"), eFunctions.Values.eTypeData.etdDouble))
					
					lclsPolicy_Win = New ePolicy.Policy_Win
					
					Call lclsPolicy_Win.Add_PolicyWin(Session("scertype"), Session("nBranch"), Session("nProduct"), Session("npolicy"), Session("ncertif"), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), Session("nUsercode"), "VI006", "1")
					
				Else
					lblnPost = True
					
					lclsPolicy_Win = New ePolicy.Policy_Win
					
					Call lclsPolicy_Win.Add_PolicyWin(Session("scertype"), Session("nBranch"), Session("nProduct"), Session("npolicy"), Session("ncertif"), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), Session("nUsercode"), "VI006", "2")
					
				End If
				
				lclsFunds_Pol = Nothing
				lclsPolicy_Win = Nothing
			End With
			
		Case "VI006A"
			
			lclsFunds_CO_P = New eApvc.Funds_CO_P
			
			With Request
				If .QueryString.Item("WindowType") = "PopUp" Then
					If CBool(.Form.Item("chkActivFound")) Then
						sActivefound_P = "1"
					Else
						sActivefound_P = "2"
					End If
					
					Call lclsFunds_CO_P.insPostVI006A(.QueryString.Item("sCodispl"), .QueryString.Item("Action"), mobjValues.StringToType(.Form.Item("tcnFunds"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnParticip"), eFunctions.Values.eTypeData.etdDouble), Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), Session("nUsercode"), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("dNulldate"), eFunctions.Values.eTypeData.etdDate), Session("nTransaction"), sActivefound_P, "2", mobjValues.StringToType(.Form.Item("tcnOrigin"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnIntProy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnIntProyVar"), eFunctions.Values.eTypeData.etdDouble))
					
					lclsPolicy_Win = New ePolicy.Policy_Win
					
					Call lclsPolicy_Win.Add_PolicyWin(Session("scertype"), Session("nBranch"), Session("nProduct"), Session("npolicy"), Session("ncertif"), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), Session("nUsercode"), "VI006A", "1")
				Else
					lblnPost = True
					
					lclsPolicy_Win = New ePolicy.Policy_Win
					
					Call lclsPolicy_Win.Add_PolicyWin(Session("scertype"), Session("nBranch"), Session("nProduct"), Session("npolicy"), Session("ncertif"), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), Session("nUsercode"), "VI006A", "2")
				End If
				
				lclsFunds_CO_P = Nothing
				lclsPolicy_Win = Nothing
				
			End With
			
			'+     CA200 DATOS PARTICUALRES APVC
		Case "CA200"
			Life_Apvc = New eApvc.Life_Apvc
			With Request
				lblnPost = Life_Apvc.inspostca200(CInt(.QueryString.Item("Action")), Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tctnpercentsalary"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tctnPrem_max"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tctnPrem_min"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("chkspremium"), mobjValues.StringToType(.Form.Item("tctnMinstay"), eFunctions.Values.eTypeData.etdLong), 0, mobjValues.StringToType(.Form.Item("cbeBankExt"), eFunctions.Values.eTypeData.etdLong), .Form.Item("tctsAccount"), mobjValues.StringToType(.Form.Item("tctnPremiumc"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(.Form.Item("tctnPercentiumc"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbenTyp_profitworker"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(Session("nusercode"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(.Form.Item("cbenCurrencyempl"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(.Form.Item("tctnAmountpren"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tctnStay"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(.Form.Item("tctnamountsalary"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(.Form.Item("tctnPercentnprent"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(.Form.Item("cbenCurrencywork"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(.Form.Item("cbeTyp_Account"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(.Form.Item("valOption"), eFunctions.Values.eTypeData.etdDouble, True))
				
				
				'                                                 
				lclsPolicy_Win = New ePolicy.Policy_Win
				
				Call lclsPolicy_Win.Add_PolicyWin(Session("scertype"), Session("nBranch"), Session("nProduct"), Session("npolicy"), Session("ncertif"), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), Session("nUsercode"), "CA200", "2")
				lclsPolicy_Win = Nothing
				
			End With
			Life_Apvc = Nothing
	End Select
	
	'+Se ejecutan las ventana automaticas
	'    mobjNetFrameWork.FinishProcess "PostSequence|" & Request.QueryString("sCodispl")
	If lblnPost And Request.QueryString.Item("WindowType") <> "PopUp" Then
		Call insGeneralAuto(Request.QueryString.Item("sCodispl"))
	End If
	lclsPolicy_Win = Nothing
	insPostSequence = lblnPost
End Function
'+Esta función carga automáticamente con contenido las ventanas correspondientes dependiendo de la que se esté tratando.
'------------------------------------------------------------------
Private Sub insGeneralAuto(ByVal sCodispl As String)
	'------------------------------------------------------------------
	Dim lclsAutoCharge As ePolicy.AutoCharge
	
	'    mobjNetFrameWork.BeginProcess "AutoUpdGeneral-" & sCodispl
	lclsAutoCharge = New ePolicy.AutoCharge
	Call lclsAutoCharge.InsAutoUpdGeneral(sCodispl, Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), mobjValues.StringToType(Session("nGroup"), eFunctions.Values.eTypeData.etdLong), Session("sPoliType"), mobjValues.StringToDate(Session("dEffecdate")), mobjValues.StringToDate(Session("dNulldate")), Session("nTransaction"), Session("nUsercode"), Session("sBrancht"), Session("SessionId"), Session("sBussityp"), eRemoteDB.Constants.intNull)
	lclsAutoCharge = Nothing
	'    mobjNetFrameWork.FinishProcess "AutoUpdGeneral-" & sCodispl
End Sub

</script>
<%Response.Expires = -1441
'^Begin Header Block VisualTimer Utility 1.1 31/3/03 19.42.20
'Dim mobjNetFrameWork 
'mobjNetFrameWork = Server.CreateObject("eNetFrameWork.Layout")
'    mobjNetFrameWork.sSessionID = Session.SessionID
'mobjNetFrameWork.nUsercode = Session("nUsercode")
'Call mobjNetFrameWork.BeginPage("ValPolicySeq")
'~End Header Block VisualTimer Utility
Response.CacheControl = "private"

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.42.55
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = Request.QueryString.Item("sCodispl")
mstrCommand = "sModule=Policy&sProject=PolicySeq&sCodisplReload=" & Request.QueryString.Item("sCodispl")
'+ se limpia variable de session
Session("nFinish") = ""

%> 
<HTML>
<HEAD>
<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></SCRIPT>





	


<SCRIPT>
//+ Variable para el control de versiones 
    document.VssVersion="$$Revision: 25 $|$$Date: 6/06/06 4:49p $|$$Author: Fmendoza $"

	var mintTpremium = "";
//%NewLocation: se recalcula el URL de la página
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
<FORM ID="valPolicySeq" NAME="valPolicySeq">
<%

mobjPolicySeq = New ePolicy.ValPolicySeq

'- Se define la variable para almacenar la nueva dirección de la CA001
mstrLocationCA001 = vbNullString

'+ Si no se han validado los campos de la página
If Request.Form.Item("sCodisplReload") = vbNullString Then
	mstrErrors = insvalSequence
	Session("sErrorTable") = mstrErrors
	Session("sForm") = Request.Form.ToString
	mblnReload = False
Else
	mblnReload = True
	Session("sErrorTable") = vbNullString
	Session("sForm") = vbNullString
End If

If mstrErrors > vbNullString Then
	With Response
		.Write("<SCRIPT LANGUAGE=JAVASCRIPT>")
		If Request.QueryString.Item("ActionType") = "Check" Then
			.Write("ShowPopUp(""/VTimeNet/Common/Errors.aspx?sSource=" & Server.URLEncode(mstrCommand) & "&sQueryString=" & Server.URLEncode(Request.Params.Get("Query_String")) & "&ActionType=" & Request.QueryString.Item("ActionType") & "&nIndex=" & Request.QueryString.Item("nIndex") & "&nMainAction=" & Request.QueryString.Item("nMainAction") & mstrQueryString & """, ""PolicySeqError"",660,330);")
		Else
			.Write("ShowPopUp(""/VTimeNet/Common/Errors.aspx?sSource=" & Server.URLEncode(mstrCommand) & "&sQueryString=" & Server.URLEncode(Request.Params.Get("Query_String")) & mstrQueryString & """, ""PolicySeqError"",660,330);")
			If Request.QueryString.Item("sCodispl") <> "CA021" Then
				.Write(mobjValues.StatusControl(False, CShort(Request.QueryString.Item("nZone")), Request.QueryString.Item("WindowType")))
			End If
		End If
		.Write("</SCRIPT>")
	End With
Else
	
	If Request.QueryString.Item("nAction") <> CStr(eFunctions.Menues.TypeActions.clngAcceptdataFinish) Then
		If insPostSequence Then
			
			If Request.QueryString.Item("WindowType") <> "PopUp" Then
				
				'+ Si se está tratando con un frame y no con la ventana principal de la secuencia, 
				'+ se mueve automaticamente a la siguiente página
				If mstrLocationCA001 = vbNullString Then
					
					'+ Validacion para cuando la CA012 llama a la sequencia desde el modulo "Ordenes profesionales".
					If CStr(Session("CallSequence")) <> "Prof_ord" Then
						lclsRefresh = New ePolicy.ValPolicySeq
						
						Response.Write(lclsRefresh.RefreshSequence(Request.QueryString.Item("sCodispl") & Request.QueryString.Item("nIndexCover"), Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), Session("sBrancht"), Session("sPolitype"), "Yes"))
						lclsRefresh = Nothing
					Else
						If Request.Form.Item("sCodisplReload") = vbNullString Then
							Response.Write("<SCRIPT>top.frames['fraSequence'].document.location='/VTimeNet/Prof_ord/Prof_ordseq/Sequence.aspx?nAction=" & Request.QueryString.Item("nMainAction") & "&nOpener=" & Request.QueryString.Item("sCodispl") & "&nMainAction=" & Request.QueryString.Item("nMainAction") & "';</SCRIPT>")
						Else
							Response.Write("<SCRIPT>window.close();opener.top.frames['fraSequence'].document.location='/VTimeNet/Prof_ord/Prof_ordseq/Sequence.aspx?nMainAction=" & Request.QueryString.Item("nAction") & "&nOpener=" & Request.QueryString.Item("sCodispl") & "';</SCRIPT>")
						End If
					End If
				Else
					'+ Se carga nuevamente la ventana principal de la secuencia
					If mblnReload Then
						Response.Write("<SCRIPT>window.close();opener.top.document.location=" & mstrLocationCA001 & ";</SCRIPT>")
					Else
						Response.Write("<SCRIPT>top.document.location=" & mstrLocationCA001 & ";</SCRIPT>")
					End If
				End If
				If Request.QueryString.Item("nZone") = "1" Then
					Response.Write("<SCRIPT LANGUAGE=JAVASCRIPT>self.history.go(-1)</SCRIPT>")
				End If
			Else
				If Request.QueryString.Item("sCodispl") <> "CA014" And Request.QueryString.Item("sCodispl") <> "CA014A" And Request.QueryString.Item("sCodispl") <> "VI021" And Request.QueryString.Item("sCodispl") <> "OS001_K" And Request.QueryString.Item("sCodispl") <> "CA027" And Request.QueryString.Item("sCodispl") <> "VI662" Then
					If Request.QueryString.Item("sCodispl") = "CA025" Then
						If mblnReload Then
							Response.Write("<SCRIPT>top.opener.top.opener.top.frames['fraSequence'].document.location='Sequence.aspx?nAction=" & Request.QueryString.Item("nMainAction") & "&nOpener=" & Request.QueryString.Item("sCodispl") & "&sGoToNext=NO" & "';</SCRIPT>")
						Else
							Response.Write("<SCRIPT>top.opener.top.frames['fraSequence'].document.location='Sequence.aspx?nAction=" & Request.QueryString.Item("nMainAction") & "&nOpener=" & Request.QueryString.Item("sCodispl") & "&sGoToNext=NO" & "';</SCRIPT>")
						End If
					Else
						lclsRefresh = New ePolicy.ValPolicySeq
						Response.Write(lclsRefresh.RefreshSequence(Request.QueryString.Item("sCodispl"), Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), Session("sBrancht"), Session("sPolitype"), "No"))
						lclsRefresh = Nothing
					End If
				End If
				Select Case Request.QueryString.Item("sCodispl")
					'+ Si se trata de Fin de proceso, se recarga la ventana principal de la secuencia
					Case "GE101"
						Response.Write("<SCRIPT>top.opener.top.document.location.href=" & mstrLocationCA001 & ";</SCRIPT>")
						'+ Emisión de recibo automático                                
					Case "CA027"
						Response.Write("<SCRIPT>top.close();</SCRIPT>")
					Case "CA020"
						Response.Write("<SCRIPT>top.opener.document.location.href='" & Request.QueryString.Item("sCodispl") & ".aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&sOnSeq=1" & "&nMainAction=" & Request.QueryString.Item("nMainAction") & "&nOwnShare=" & Request.Form.Item("hddOwnShare") & "&nExpenses=" & Request.Form.Item("hddExpenses") & "'</SCRIPT>")
					Case "CA658"
						Response.Write("<SCRIPT>top.opener.document.location.href='" & Request.QueryString.Item("sCodispl") & "Frame.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&sOnSeq=1" & "&nMainAction=" & Request.QueryString.Item("nMainAction") & "&nOptAge=" & Request.Form.Item("OptAge") & "'</SCRIPT>")
					Case "CA024"
						Response.Write("<SCRIPT>top.opener.document.location.href='CA024.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&sInd_comm=" & Request.Form.Item("hddInd_Comm") & "&sConcoll=" & Request.Form.Item("hddConColl") & "&nCommityp=" & Session("hddsType") & "&nPercent=" & Request.Form.Item("hddtcnPercent") & "'</SCRIPT>")
					Case "CA021"
						If mblnReload Then
							Response.Write("<SCRIPT>window.close();top.opener.top.opener.top.frames['fraFolder'].document.location.href='" & Request.QueryString.Item("sCodispl") & ".aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&sOnSeq=1" & Request.Form.Item("tctSetting") & "&sKeep=1&nBranchRei=" & Request.Form.Item("cbeBranchrei") & "&nModulec=" & Request.Form.Item("tcnModulec") & "&nCover=" & Request.Form.Item("valCover") & "&sClient=" & Request.Form.Item("valClient") & "&sPopupT=" & Request.Form.Item("tctPopUpT") & mstrQueryString & "'</SCRIPT>")
						Else
							Response.Write("<SCRIPT>top.opener.document.location.href='" & Request.QueryString.Item("sCodispl") & ".aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&sOnSeq=1" & Request.Form.Item("tctSetting") & "&sKeep=1&nBranchRei=" & Request.Form.Item("cbeBranchrei") & "&nModulec=" & Request.Form.Item("tcnModulec") & "&nCover=" & Request.Form.Item("valCover") & "&sClient=" & Request.Form.Item("valClient") & "&sPopupT=" & Request.Form.Item("tctPopUpT") & mstrQueryString & "'</SCRIPT>")
						End If
					Case "CA021A"
						If mblnReload Then
							Response.Write("<SCRIPT>window.close();top.opener.top.opener.top.frames['fraFolder'].document.location.href='" & Request.QueryString.Item("sCodispl") & ".aspx?" & Request.Params.Get("Query_String") & "'</SCRIPT>")
						Else
							Response.Write("<SCRIPT>top.opener.document.location.href='" & Request.QueryString.Item("sCodispl") & ".aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&sOnSeq=1" & Request.Form.Item("tctSetting") & "&sKeep=1&nBranchRei=" & Request.Form.Item("cbeBranchrei") & "&nCover=" & Request.Form.Item("nCover") & mstrQueryString & "'</SCRIPT>")
						End If
					Case "VI811"
						Response.Write("<SCRIPT>top.opener.document.location.href='" & Request.QueryString.Item("sCodispl") & ".aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("Index") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&nGroups=" & Request.Form.Item("valGroups") & "&nModulec=" & Request.Form.Item("valModulec") & "'</SCRIPT>")
					Case "VI681"
						Response.Write("<SCRIPT>top.opener.document.location.href='" & Request.QueryString.Item("sCodispl") & ".aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("Index") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&Reload=2&sClient=" & Request.Form.Item("hddsClient") & "&nRole=" & Request.Form.Item("hddnRole") & "'</SCRIPT>")
						'+ Cuadro de valores garantizados
					Case "VI732"
						Response.Write("<SCRIPT>top.opener.document.location.href='" & Request.QueryString.Item("sCodispl") & ".aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&sOnSeq=1" & "&nMainAction=" & Request.QueryString.Item("nMainAction") & "&sAut_guarval=" & Request.Form.Item("hddAut_guarval") & "&nCurrency=" & Request.Form.Item("cbeCurrency") & "'</SCRIPT>")
					Case "AM002"
						Response.Write("<SCRIPT>top.opener.document.location.href='" & Request.QueryString.Item("sCodispl") & ".aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&sOnSeq=1" & "&nTariff=" & Request.Form.Item("tcnTariff") & "&nGroup=" & Request.Form.Item("tcnGroup") & "&nRole=" & Request.Form.Item("tcnRole") & "&nModulec=" & Request.Form.Item("tcnModulec") & "&nCover=" & Request.Form.Item("tcnCover") & "&nMainAction=" & Request.QueryString.Item("nMainAction") & "'</SCRIPT>")
					Case "AM003"
						Response.Write("<SCRIPT>top.opener.document.location.href='" & Request.QueryString.Item("sCodispl") & ".aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&sOnSeq=1" & "&nMainAction=" & Request.QueryString.Item("nMainAction") & "&nTariff=" & Request.QueryString.Item("nTariff") & "&nCover=" & Request.QueryString.Item("nCover") & "&nRole=" & Request.QueryString.Item("nRole") & "&sClient=" & Request.QueryString.Item("sClient") & "&sIllness=" & Request.QueryString.Item("sIllness") & "&nGroup=" & Request.QueryString.Item("nGroup") & "&nModulec=" & Request.QueryString.Item("nModulec") & "&nLimitH=" & Request.QueryString.Item("nLimitH") & "&sAutoRestit=" & Request.QueryString.Item("sAutoRestit") & "'</SCRIPT>")
					Case "VI666"
						Response.Write("<SCRIPT>top.opener.document.location.href='" & Request.QueryString.Item("sCodispl") & ".aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("Index") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&sReloadPage=1" & mstrQueryString & "'</SCRIPT>")
					Case "CA025"
						With Request
							Response.Write(mobjValues.GetUrl(eFunctions.Values.eUrlType.cstrGrid, mblnReload, .QueryString.Item("sCodisp"), .QueryString.Item("sCodispl"), "1", .Form.Item("chkContinue"), .QueryString.Item("Action"), .QueryString.Item("ReloadIndex"), .QueryString.Item("nMainAction"), .QueryString.Item("sWindowDescript"), .QueryString.Item("nWindowTy"), mstrQueryString))
						End With
					Case "CA014", "CA014A"
						If Request.QueryString.Item("ActionType") = "Check" Then
							Response.Write("<SCRIPT>")
							Response.Write("setPointer('');")
							If Request.QueryString.Item("sCodispl") = "CA014" Then
								If mblnReload Then
									mstrScript = mstrScript & "top.opener."
								End If
								If Request.QueryString.Item("sCodisplori") <> "VI7011" Then
									mstrScript = mstrScript & "top.frames['fraFolder'].InsCalTotalPremium();"
								End If
							End If
							If mblnReload Then
								mstrScript = mstrScript & "window.close();"
							End If
							Response.Write(mstrScript)
							If Request.QueryString.Item("sCodisplori") = "VI7011" Then
								mstrTotalPrima = mobjValues.StringToType(Request.QueryString.Item("TotalPrima"), eFunctions.Values.eTypeData.etdDouble)
								If Request.QueryString.Item("Action") = "Del" Then
									mstrTotalPrima = mstrTotalPrima - mobjValues.StringToType(Request.QueryString.Item("nPremium"), eFunctions.Values.eTypeData.etdDouble)
								Else
									mstrTotalPrima = mstrTotalPrima + mobjValues.StringToType(Request.QueryString.Item("nPremium"), eFunctions.Values.eTypeData.etdDouble)
								End If
								Response.Write("mintTpremium = " & mstrTotalPrima & ";")
								Response.Write("top.frames['fraFolder'].InsCalTotalPremium(mintTpremium);")
							End If
							Response.Write("</SCRIPT>")
						Else
							If mblnReload Then
								Response.Write("<SCRIPT>window.close();top.opener.top.opener.top.frames['fraFolder'].document.location.href='" & Request.QueryString.Item("sCodispl") & ".aspx?sCodispl=" & Request.QueryString.Item("sCodispl") & "&Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("Index") & "&nMainAction=" & Request.QueryString.Item("nMainAction") & mstrQueryString & "'</SCRIPT>")
							Else
								Response.Write("<SCRIPT>top.opener.document.location.href='" & Request.QueryString.Item("sCodispl") & ".aspx?sCodispl=" & Request.QueryString.Item("sCodispl") & "&Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("Index") & "&nMainAction=" & Request.QueryString.Item("nMainAction") & mstrQueryString & "'</SCRIPT>")
							End If
						End If
					Case "CA016", "CA016A"
						Response.Write("<SCRIPT>top.opener.document.location.href='CA016.aspx?sCodispl=" & Request.QueryString.Item("sCodispl") & "&Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("Index") & mstrQueryString & "'</SCRIPT>")
					Case "VI7011"
						Response.Write("<SCRIPT>top.opener.document.location.href='VI7011.aspx?sCodispl=" & Request.QueryString.Item("sCodispl") & "&Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("Index") & mstrQueryString & "'</SCRIPT>")
					Case "OS001_K"
						Response.Write("<SCRIPT>top.opener.document.location.href='OS001.aspx?sCodispl=" & Request.QueryString.Item("sCodispl") & "&Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("Index") & mstrQueryString & "'</SCRIPT>")
					Case "CA748"
						Response.Write("<SCRIPT>top.opener.document.location.href='" & Request.QueryString.Item("sCodispl") & ".aspx?sCodispl=" & Request.QueryString.Item("sCodispl") & "&Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("Index") & "&nMainAction=" & Request.QueryString.Item("nMainAction") & "&sOnSeq=" & Request.QueryString.Item("sOnSeq") & mstrQueryString & "'</SCRIPT>")
					Case "CA013", "CA013A"
						Response.Write("<SCRIPT>top.opener.document.location.href='CA013.aspx?sCodispl=" & Request.QueryString.Item("sCodispl") & "&Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("Index") & "&nMainAction=" & Request.QueryString.Item("nMainAction") & mstrQueryString & "'</SCRIPT>")
					Case "VI7003"
						If mblnReload Then
							Response.Write("<SCRIPT>window.close();top.opener.top.opener.top.frames['fraFolder'].document.location.href='" & Request.QueryString.Item("sCodispl") & ".aspx?sCodispl=" & Request.QueryString.Item("sCodispl") & "&Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("Index") & "&nMainAction=304" & mstrQueryString & "'</SCRIPT>")
						Else
							Response.Write("<SCRIPT>top.opener.document.location.href='VI7003.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&nMainAction=" & Request.QueryString.Item("nMainAction") & "&sOnSeq=1" & "'</SCRIPT>")
						End If
					Case "VI7005"
						Response.Write("<SCRIPT>top.opener.document.location.href='VI7005.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&nMainAction=" & Request.QueryString.Item("nMainAction") & "&sOnSeq=1" & "'</SCRIPT>")
					Case "CA100"
						If mblnReload Then
							Response.Write("<SCRIPT>window.close();top.opener.top.opener.top.frames['fraFolder'].document.location.href='" & Request.QueryString.Item("sCodispl") & ".aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&sOnSeq=1" & Request.Form.Item("tctSetting") & "&nModulec=" & Request.QueryString.Item("nModulec") & "&nCover=" & Request.QueryString.Item("nCover") & "&nGroup=" & Request.QueryString.Item("nGroup") & mstrQueryString & "'</SCRIPT>")
						Else
							Response.Write("<SCRIPT>top.opener.document.location.href='" & Request.QueryString.Item("sCodispl") & ".aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&sOnSeq=1" & "&nModulec=" & Request.QueryString.Item("nModulec") & "&nCover=" & Request.QueryString.Item("nCover") & "&nGroup=" & Request.QueryString.Item("nGroup") & mstrQueryString & "'</SCRIPT>")
						End If
					Case Else
						If mblnReload Then
							Response.Write("<SCRIPT>window.close();top.opener.top.opener.top.frames['fraFolder'].document.location.href='" & Request.QueryString.Item("sCodispl") & ".aspx?sCodispl=" & Request.QueryString.Item("sCodispl") & "&Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("Index") & "&nMainAction=304" & mstrQueryString & "'</SCRIPT>")
						Else
							Response.Write("<SCRIPT>top.opener.document.location.href='" & Request.QueryString.Item("sCodispl") & ".aspx?sCodispl=" & Request.QueryString.Item("sCodispl") & "&Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("Index") & "&nMainAction=304" & mstrQueryString & "'</SCRIPT>")
						End If
				End Select
			End If
		Else
			If Not CBool(IIf(IsNothing(Request.Form.Item("hddbPuntual")), False, Request.Form.Item("hddbPuntual"))) Then
				Response.Write("<SCRIPT>alert('No se pudo realizar la actualización');</SCRIPT>")
			End If
		End If
	Else
		If Request.QueryString.Item("nAction") = CStr(eFunctions.Menues.TypeActions.clngAcceptdataFinish) Then
			'+ Se recarga la página principal de la secuencia
			If CStr(Session("CallSequence")) = "Prof_ord" Then
				mstrLocationCA001 = "'/VTimeNet/Common/secWHeader.aspx?sCodispl=OS590&sProject=Prof_ordseq&sModule=Prof_ord'"
				Response.Write("<SCRIPT>top.document.location=" & mstrLocationCA001 & ";</SCRIPT>")
			Else
				'If CBool(insFinish()) Then
                If True Then
					If Request.Form.Item("sCodisplReload") = "CA048" Then
						Response.Write("<SCRIPT>window.close();top.opener.top.document.location=" & mstrLocationCA001 & ";</SCRIPT>")
					Else
						If Request.QueryString.Item("sCodispl") = "CA048" Then
							mstrLocationCA001 = "'/VTimeNet/Common/secWHeader.aspx?sCodispl=CA001&sProject=PolicySeq&sModule=Policy&sConfig=&nAction=0" & Request.QueryString.Item("nMainAction") & "&bMenu=1'"
							Response.Write("<SCRIPT>top.opener.top.document.location=" & mstrLocationCA001 & ";</SCRIPT>")
						ElseIf Request.QueryString.Item("sCodispl") = "CA050" Then 
							Response.Write("<SCRIPT>top.opener.top.document.location=" & mstrLocationCA001 & ";</SCRIPT>")
						End If
					End If
				Else
					Response.Write("<SCRIPT>alert('No se pudo realizar la actualización final');</SCRIPT>")
				End If
			End If
		End If
	End If
End If
mobjPolicySeq = Nothing
mobjValues = Nothing
%>
        </FORM>
    </BODY>
</HTML>
<%







'

%>
<%'^Begin Footer Block VisualTimer Utility 1.1 31/3/03 19.42.55
'Call mobjNetFrameWork.FinishPage("ValPolicySeq")
'mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>





