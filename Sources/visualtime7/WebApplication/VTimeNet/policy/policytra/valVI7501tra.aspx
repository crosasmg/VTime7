<%@ Page Language="VB" explicit="true" ValidateRequest="false" %>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eSaapv" %>
<script language="VB" runat="Server">
Dim mobjNetFrameWork As eNetFrameWork.Layout

Dim mobjValues As eFunctions.Values
Dim mobjSaapv As Object
Dim mstrErrors As String

Dim mstrCommand As String

'% insValVI7501: Se realizan las validaciones masivas de cada una de las páginas.
'--------------------------------------------------------------------------------------------
Function insValVI7501() As String
	Dim lintOrigin As Byte
	'--------------------------------------------------------------------------------------------
	Dim lobjSaapv As eSaapv.Saapv
	Select Case Request.QueryString.Item("sCodispl")
		
		'+ VI7501_K:Encabezado Saapv
		Case "VI7501_K"
			mobjSaapv = New eSaapv.Saapv
			
			With Request
				insValVI7501 = mobjSaapv.insValVI7501(.QueryString("nMainAction"), mobjValues.StringToType(.Form("tcncod_saapv"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form("tcdissue_dat"), eFunctions.Values.eTypeData.etdDate, True), mobjValues.StringToType(.Form("cbeType_saapv"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form("cbestatus_saapv"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form("valInstitution"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form("cbetype_ameapv"), eFunctions.Values.eTypeData.etdDouble, True), .Form("optCertype"), mobjValues.StringToType(.Form("cbeBranch"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form("valProduct"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble, True), 0)
			End With
			mobjSaapv = Nothing
		Case "VI7501_A"
			mobjSaapv = New eSaapv.Saapv
			With Request
				insValVI7501 = mobjSaapv.insValVI7501_A(0, mobjValues.StringToType(.Form("tcncod_saapv"), eFunctions.Values.eTypeData.etdDouble, True), .Form("optCertype"), mobjValues.StringToType(.Form("cbeBranch"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form("valProduct"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form("tcnCertif"), eFunctions.Values.eTypeData.etdDouble, True), .Form("tctclient"), .Form("tctdescadd"), .Form("cbeSex"), mobjValues.StringToType(.Form("tcdBirthDate"), eFunctions.Values.eTypeData.etdDate, True), mobjValues.StringToType(.Form("cbeNationality"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form("cbeCivilsta"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form("cbeOccupat"), eFunctions.Values.eTypeData.etdDouble, True))
			End With
			mobjSaapv = Nothing
		Case "VI7501_B"
			mobjSaapv = New eSaapv.Saapv
			With Request
				insValVI7501 = mobjSaapv.insValVI7501_B(0, mobjValues.StringToType(.Form("tcncod_saapv"), eFunctions.Values.eTypeData.etdDouble, True), .Form("optCertype"), mobjValues.StringToType(.Form("cbeBranch"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form("valProduct"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form("tcnCertif"), eFunctions.Values.eTypeData.etdDouble, True), .Form("tctclient"), .Form("tctname"), .Form("tctse_mail"), .Form("tctphone"), mobjValues.StringToType(.Form("tcdRecepdat"), eFunctions.Values.eTypeData.etdDate, True), .Form("tctdescadd"), mobjValues.StringToType(CStr(Session("nType_saapv")), eFunctions.Values.eTypeData.etdLong, True), mobjValues.StringToType(CStr(Session("nInstitution")), eFunctions.Values.eTypeData.etdLong, True))
			End With
			mobjSaapv = Nothing
		Case "VI7501_C"
			insValVI7501 = vbNullString
		Case "VI7501_D"
			mobjSaapv = New eSaapv.Saapv
			With Request
				insValVI7501 = mobjSaapv.insValVI7501_D(mobjValues.StringToType(CStr(Session("nCod_saapv")), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form("cbeTax_regime"), eFunctions.Values.eTypeData.etdLong, True), mobjValues.StringToType(.Form("tcnAmount"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form("tcnAmount_uf"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form("tcnAmount_pct"), eFunctions.Values.eTypeData.etdDouble, True))
			End With
			mobjSaapv = Nothing
		Case "VI7501_E"
			mobjSaapv = New eSaapv.Saapv
			With Request
				insValVI7501 = mobjSaapv.insValVI7501_E(.QueryString("Action"), mobjValues.StringToType(.Form("cbeWay_pay"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form("tcnMonth"), eFunctions.Values.eTypeData.etdDouble, True))
			End With
			mobjSaapv = Nothing
		Case "VI7501_F"
			mobjSaapv = New eSaapv.Saapv_Transfer
			With Request
				If .QueryString("WindowType") = "PopUp" Then
					insValVI7501 = mobjSaapv.insValVI7501_F(.QueryString("Action"), mobjValues.StringToType(.Form("tcncod_saapv"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form("cbeFunds"), eFunctions.Values.eTypeData.etdLong, True), mobjValues.StringToType(.Form("cbeTax_regime"), eFunctions.Values.eTypeData.etdLong, True), .Form("cbeAfp_type"), mobjValues.StringToType(.Form("chkType"), eFunctions.Values.eTypeData.etdLong, True), mobjValues.StringToType(.Form("tcnSaving_Loc"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form("tcnSaving_UF"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form("tcnSaving_PCT"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(CStr(Session("nInstitution")), eFunctions.Values.eTypeData.etdLong, True))
					
				Else
					If CStr(Session("nType_saapv")) = "5" Then
						lobjSaapv = New eSaapv.Saapv
						
						insValVI7501 = lobjSaapv.insValVI7501_F(mobjValues.StringToType(.Form("valInstitution"), eFunctions.Values.eTypeData.etdLong, True))
						
						lobjSaapv = Nothing
					Else
						insValVI7501 = vbNullString
					End If
				End If
			End With
			mobjSaapv = Nothing
		Case "VI7501_G"
			lintOrigin = 1
			mobjSaapv = New eSaapv.Saapv_funds_pol
			With Request
				If .QueryString("WindowType") = "PopUp" Then
					insValVI7501 = mobjSaapv.insValVI7501_G(.QueryString("sCodispl"), mobjValues.StringToType(CStr(Session("nCod_saapv")), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form("tcnOrigin"), eFunctions.Values.eTypeData.etdLong, True), mobjValues.StringToType(CStr(Session("dEffecdate_saapv")), eFunctions.Values.eTypeData.etdDate), .Form("Sel"), .QueryString("WindowType"), mobjValues.StringToType(.Form("tcnFunds"), eFunctions.Values.eTypeData.etdLong, True), mobjValues.StringToType(.Form("tcnParticip"), eFunctions.Values.eTypeData.etdDouble, True), "2", mobjValues.StringToType(CStr(Session("nBranch_saapv")), eFunctions.Values.eTypeData.etdLong, True), mobjValues.StringToType(CStr(Session("nProduct_saapv")), eFunctions.Values.eTypeData.etdLong, True), mobjValues.StringToType(CStr(Session("nPolicy_saapv")), eFunctions.Values.eTypeData.etdDouble, True), 0, "2", .Form("chkGuarant"), mobjValues.StringToType(CStr(Session("nInstitution")), eFunctions.Values.eTypeData.etdLong, True))
				Else
					insValVI7501 = mobjSaapv.insValVI7501_G(.QueryString("sCodispl"), mobjValues.StringToType(CStr(Session("nCod_saapv")), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(CStr(lintOrigin), eFunctions.Values.eTypeData.etdLong, True), mobjValues.StringToType(CStr(Session("dEffecdate_saapv")), eFunctions.Values.eTypeData.etdDate), .Form("Sel"),  ,  ,  , "2", mobjValues.StringToType(CStr(Session("nBranch_saapv")), eFunctions.Values.eTypeData.etdLong, True), mobjValues.StringToType(CStr(Session("nProduct_saapv")), eFunctions.Values.eTypeData.etdLong, True), mobjValues.StringToType(CStr(Session("nPolicy_saapv")), eFunctions.Values.eTypeData.etdDouble, True), 0, "", .Form("chkGuarant"), mobjValues.StringToType(CStr(Session("nInstitution")), eFunctions.Values.eTypeData.etdLong, True))
				End If
			End With
			mobjSaapv = Nothing
		Case Else
			insValVI7501 = "insValVI7501: Código lógico no encontrado (" & Request.QueryString.Item("sCodispl") & ")"
	End Select
End Function

'% insPostVI7501: Se realizan las actualizaciones de las ventanas.
'--------------------------------------------------------------------------------------------
Function insPostVI7501() As Boolean
	'--------------------------------------------------------------------------------------------
	Dim lblnPost As Boolean
	
	lblnPost = True
	
	Dim lobjSaapv As eSaapv.Saapv
	Select Case Request.QueryString.Item("sCodispl")
		
		'+ VI7501_K:Encabezado Saapv
		Case "VI7501_K"
			
			mobjSaapv = New eSaapv.Saapv
			
			With Request
				'+ Se asignan los valores indicados en los campos de la página
				Session("nCod_saapv") = .Form("tcncod_saapv")
				Session("nType_saapv") = .Form("cbeType_saapv")
				Session("nInstitution") = .Form("valInstitution")
				
				If .Form("hddPuntual") = "1" Then
					Session("sCertype_saapv") = .Form("optCertype")
					Session("nBranch_saapv") = .Form("cbeBranch")
					Session("nProduct_saapv") = .Form("valProduct")
					Session("nPolicy_saapv") = .Form("tcnPolicy")
					Session("dEffecdate_saapv") = .Form("tcdissue_dat")
				End If
				Session("ddissue_dat") = .Form("tcdissue_dat")
				
				If .QueryString("nMainAction") = "401" Then
					Session("bQuery") = True
				Else
					Session("bQuery") = False
				End If
				
				lblnPost = mobjSaapv.insPosVI7501(.QueryString("nMainAction"), mobjValues.StringToType(.Form("tcncod_saapv"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form("tcdissue_dat"), eFunctions.Values.eTypeData.etdDate, True), mobjValues.StringToType(.Form("tcdLimitDate"), eFunctions.Values.eTypeData.etdDate, True), mobjValues.StringToType(.Form("cbeType_saapv"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form("cbestatus_saapv"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form("valInstitution"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form("cbetype_ameapv"), eFunctions.Values.eTypeData.etdDouble, True), .Form("optCertype"), mobjValues.StringToType(.Form("cbeBranch"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form("valProduct"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(CStr(Session("nUsercode")), eFunctions.Values.eTypeData.etdDouble, True))
				
			End With
			mobjSaapv = Nothing
		Case "VI7501_A"
			mobjSaapv = New eSaapv.Saapv
			With Request
				lblnPost = mobjSaapv.insPosVI7501_A(mobjValues.StringToType(CStr(Session("nCod_saapv")), eFunctions.Values.eTypeData.etdDouble, True), .Form("optCertype"), mobjValues.StringToType(.Form("cbeBranch"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form("valProduct"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form("tcnCertif"), eFunctions.Values.eTypeData.etdDouble, True), .Form("tctclient"), mobjValues.StringToType(CStr(Session("nUsercode")), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(CStr(Session("nInstitution")), eFunctions.Values.eTypeData.etdDouble, True))
			End With
			mobjSaapv = Nothing
		Case "VI7501_B"
			mobjSaapv = New eSaapv.Saapv
			With Request
				lblnPost = mobjSaapv.insPosVI7501_B(mobjValues.StringToType(CStr(Session("nCod_saapv")), eFunctions.Values.eTypeData.etdDouble, True), .Form("optCertype"), mobjValues.StringToType(.Form("cbeBranch"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form("valProduct"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form("tcnCertif"), eFunctions.Values.eTypeData.etdDouble, True), .Form("tctclient"), .Form("tctname"), .Form("tctse_mail"), .Form("tctphone"), mobjValues.StringToType(.Form("tcdRecepdat"), eFunctions.Values.eTypeData.etdDate, True), mobjValues.StringToType(CStr(Session("nUsercode")), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(CStr(Session("nInstitution")), eFunctions.Values.eTypeData.etdDouble, True))
			End With
			mobjSaapv = Nothing
		Case "VI7501_C"
			mobjSaapv = New eSaapv.Saapv
			With Request
				lblnPost = mobjSaapv.insPosVI7501_C(mobjValues.StringToType(CStr(Session("nCod_saapv")), eFunctions.Values.eTypeData.etdDouble, True), .Form("chkContributionAfp"), .Form("chkContributionIps"), mobjValues.StringToType(.Form("optEmployee"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(.Form("chkHealth"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(CStr(Session("nUsercode")), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(CStr(Session("nInstitution")), eFunctions.Values.eTypeData.etdDouble, True))
			End With
			mobjSaapv = Nothing
		Case "VI7501_D"
			mobjSaapv = New eSaapv.Saapv
			With Request
				lblnPost = mobjSaapv.insPosVI7501_D(mobjValues.StringToType(CStr(Session("nCod_saapv")), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form("cbeTax_regime"), eFunctions.Values.eTypeData.etdLong, True), mobjValues.StringToType(.Form("tcnAmount"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form("tcnAmount_uf"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form("tcnAmount_pct"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form("chkLumpsum"), eFunctions.Values.eTypeData.etdLong, True), mobjValues.StringToType(.Form("tcdEndDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form("cbeOrigin"), eFunctions.Values.eTypeData.etdLong, True), mobjValues.StringToType(CStr(Session("nUsercode")), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(CStr(Session("nInstitution")), eFunctions.Values.eTypeData.etdDouble, True))
			End With
			mobjSaapv = Nothing
		Case "VI7501_E"
			mobjSaapv = New eSaapv.Saapv
			With Request
				lblnPost = mobjSaapv.insPosVI7501_E(mobjValues.StringToType(CStr(Session("nCod_saapv")), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form("cbeWay_pay"), eFunctions.Values.eTypeData.etdLong, True), mobjValues.StringToType(.Form("tcnMonth"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(CStr(Session("nUsercode")), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(CStr(Session("nInstitution")), eFunctions.Values.eTypeData.etdDouble, True))
			End With
			mobjSaapv = Nothing
		Case "VI7501_F"
			mobjSaapv = New eSaapv.Saapv_Transfer
			With Request
				If .QueryString("WindowType") = "PopUp" Then
					
					lblnPost = mobjSaapv.insPostVI7501_F(.QueryString("Action"), mobjValues.StringToType(.Form("tcncod_saapv"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form("cbeFunds"), eFunctions.Values.eTypeData.etdLong, True), mobjValues.StringToType(.Form("cbeTax_regime"), eFunctions.Values.eTypeData.etdLong, True), .Form("cbeAfp_type"), mobjValues.StringToType(.Form("chkType"), eFunctions.Values.eTypeData.etdLong, True), mobjValues.StringToType(.Form("tcnSaving_Loc"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form("tcnSaving_UF"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form("tcnSaving_PCT"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(CStr(Session("nUsercode")), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(CStr(Session("nInstitution")), eFunctions.Values.eTypeData.etdLong, True))
				Else
					If CStr(Session("nType_saapv")) = "5" Then
						lobjSaapv = New eSaapv.Saapv
						
						lblnPost = lobjSaapv.insPosVI7501_F(mobjValues.StringToType(CStr(Session("nCod_saapv")), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(CStr(Session("nInstitution")), eFunctions.Values.eTypeData.etdLong, True), mobjValues.StringToType(CStr(Session("nUsercode")), eFunctions.Values.eTypeData.etdLong, True), mobjValues.StringToType(.Form("valInstitution"), eFunctions.Values.eTypeData.etdLong, True))
						lobjSaapv = Nothing
					Else
						lblnPost = True
					End If
				End If
			End With
			mobjSaapv = Nothing
		Case "VI7501_G"
			mobjSaapv = New eSaapv.Saapv_funds_pol
			With Request
				If .QueryString("WindowType") = "PopUp" Then
					lblnPost = mobjSaapv.insPostVI7501_G(.QueryString("Action"), mobjValues.StringToType(CStr(Session("nCod_saapv")), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form("tcnFunds"), eFunctions.Values.eTypeData.etdLong, True), mobjValues.StringToType(.Form("tcnOrigin"), eFunctions.Values.eTypeData.etdLong, True), mobjValues.StringToType(CStr(Session("dEffecdate_saapv")), eFunctions.Values.eTypeData.etdDate), "2", mobjValues.StringToType(CStr(Session("nBranch_saapv")), eFunctions.Values.eTypeData.etdLong, True), mobjValues.StringToType(CStr(Session("nProduct_saapv")), eFunctions.Values.eTypeData.etdLong, True), mobjValues.StringToType(CStr(Session("nPolicy_saapv")), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(CStr(Session("nCertif_saapv")), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form("tcnParticip"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(CStr(Session("nUsercode")), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(.Form("hddnQuan_avail"), eFunctions.Values.eTypeData.etdDouble, True), .Form("hddsActivefound"), mobjValues.StringToType(.Form("hddnIntproy"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form("hddnIntproyvar"), eFunctions.Values.eTypeData.etdDouble, True), .QueryString("sSel"), mobjValues.StringToType(CStr(Session("nInstitution")), eFunctions.Values.eTypeData.etdLong, True))
				Else
					lblnPost = True
				End If
			End With
			mobjSaapv = Nothing
	End Select
	
	insPostVI7501 = lblnPost
End Function

'% insFinish: Se activa cuando la acción es Finalizar.
'--------------------------------------------------------------------------------------------
Function insFinish() As Boolean
	'--------------------------------------------------------------------------------------------
	'+ Se verifica que no existan páginas marcadas como requeridas en la secuencia
	Response.Write("<SCRIPT>insvalTabs()</" & "Script>")
	insFinish = True
End Function

</script>
<%Response.Expires = -1
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("valVI7501tra")

mobjValues = New eFunctions.Values
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")

mobjValues.sCodisplPage = "valVI7501tra"
mstrCommand = "&sModule=Policy&sProject=Policytra&sCodisplReload=" & Request.QueryString.Item("sCodispl")
%>
<HTML>
<HEAD>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
    <%=mobjValues.StyleSheet()%>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0">
<!-- #INCLUDE VIRTUAL="~/VTimeNet/Includes/General.aspx" -->
<!-- #INCLUDE VIRTUAL="~/VTimeNet/Includes/Constantes.aspx" -->

</HEAD>
<BODY>
<%
If Request.QueryString.Item("nAction") <> eFunctions.Menues.TypeActions.clngAcceptdataFinish Then
	'**+ If the fields of the page have not been validated.  
	'+ Si no se han validado los campos de la página.
	'+ Si no se han validado los campos de la página
	If Request.Form("sCodisplReload") = vbNullString Then
		mstrErrors = insValVI7501
		Session("sErrorTable") = mstrErrors
		Session("sForm") = Request.Form.ToString
	Else
		Session("sErrorTable") = vbNullString
		Session("sForm") = vbNullString

	End If
	
	If mstrErrors > vbNullString Then
		With Response
			.Write("<SCRIPT LANGUAGE=JAVASCRIPT>")
            .Write("ShowPopUp(""/VTimeNet/Common/Errors.aspx?sSource=" & Server.URLEncode(mstrCommand) & "&sQueryString=" & Server.URLEncode(Request.Params.Get("Query_String")) & "&sValPage=VI7501tra" & """, ""PolicyRepErrors"",660,330);")
            .Write(mobjValues.StatusControl(False, Request.QueryString.Item("nZone"), Request.QueryString.Item("WindowType")))
			.Write("</SCRIPT>")
		End With
	Else
		If insPostVI7501 Then
			If Request.QueryString.Item("WindowType") <> "PopUp" Then
				'**+ One moves automatically to the following page.  
				'+ Se mueve automáticamente a la siguiente página.
				If Request.Form.Item("sCodisplReload") = vbNullString Then
					'Response.Write("<SCRIPT>top.frames['fraSequence'].document.location='/VTimeNet/Policy/PolicyTra/Sequence.aspx?nAction=" & Request.QueryString.Item("nMainAction") & "&sGoToNext=Yes&nOpener=" & Request.QueryString.Item("sCodispl") & "';</SCRIPT>")
				    Response.Write("<SCRIPT>top.frames['fraSequence'].document.location=""/VTimeNet/Policy/PolicyTra/Sequence.aspx?nAction=" & Request.QueryString.Item("nMainAction") & "&sGoToNext=Yes&nOpener=" & Request.QueryString.Item("sCodispl") & """;</SCRIPT>")
                Else
					Response.Write("<SCRIPT>window.close();opener.top.frames['fraSequence'].document.location=""/VTimeNet/Policy/PolicyTra/Sequence.aspx?nAction=" & Request.QueryString.Item("nMainAction") & "&sGoToNext=Yes&nOpener=" & Request.QueryString.Item("sCodispl") & """;</SCRIPT>")
				End If
			Else
				'If Request.Form("sCodisplReload") = vbNullString Then
				'    Response.Write "<NOTSCRIPT>window.close();opener.top.frames['fraSequence'].document.location='/VTimeNet/Policy/PolicyTra/Sequence.aspx?nAction=" & Request.QueryString.Item("nMainAction") & "&sGoToNext=NO&nOpener=" & Request.QueryString.Item("sCodispl") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&sSel=" & Request.Form("Sel") & "';</SCRIPT>"
				'Else
				'	Response.Write "<NOTSCRIPT>window.close();opener.opener.top.frames['fraSequence'].document.location='/VTimeNet/Policy/PolicyTra/Sequence.aspx?nAction=" & Request.QueryString.Item("nMainAction") & "&sGoToNext=NO&nOpener=" & Request.QueryString.Item("sCodispl") & "&sSel=" & Request.Form("Sel") & "';</SCRIPT>"
				'End If
				
				'**+ The page is recharged that invoked the PopUp.  
				'+ Se recarga la página que invocó la PopUp.
				
				Select Case Request.QueryString.Item("sCodispl")
					Case "CA036A"
						Response.Write("<SCRIPT>opener.document.location.href='CA036A.aspx?Reload=" & Request.Form("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=0" & Request.QueryString.Item("ReloadIndex") & "&nMainAction=" & Request.QueryString.Item("nMainAction") & "&Index=" & Request.QueryString.Item("Index") & "'</SCRIPT>")
					Case "VI7501_F"
						Response.Write("<SCRIPT>top.opener.document.location.href='" & Request.QueryString.Item("sCodispl") & ".aspx?Reload=" & Request.Form("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("Index") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&nInstitut_origin=" & Request.QueryString.Item("nInstitut_origin") & "'</SCRIPT>")
					Case "VI7501_G"
						Response.Write("<SCRIPT>top.opener.document.location.href='" & Request.QueryString.Item("sCodispl") & ".aspx?Reload=" & Request.Form("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("Index") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "'</SCRIPT>")
				End Select
			End If
		End If
	End If
Else
	If insFinish Then
		Response.Write("<SCRIPT>top.location.reload();</SCRIPT>")
	End If
End If

mobjSaapv = Nothing
mobjValues = Nothing
%>
</BODY>
</HTML>
<%
Call mobjNetFrameWork.FinishPage("valVI7501tra")
mobjNetFrameWork = Nothing
%>




