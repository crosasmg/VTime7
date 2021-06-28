<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eClaim" %>
<%@ Import namespace="ePolicy" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'+ Se define la contante para el manejo de errores en caso de advertencias
Dim mstrCommand As String

'- Variable para el manejo de los errores de la página, devueltos por insvalSequence
Dim mstrErrors As String

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjProf_ordseq As Object


'% insvalSequence: Se realizan las validaciones masivas de las páginas
'--------------------------------------------------------------------------------------------
Function insvalSequence() As String
	'--------------------------------------------------------------------------------------------
	
	Dim lclsClaimProf_ord As eClaim.Claim
	Dim lclsCertificatProf_ord As ePolicy.Certificat
	Select Case Request.QueryString.Item("sCodispl")
		
		'+ OS590_K : Ordenes de servicio
		Case "OS590_K"
			
			mobjProf_ordseq = New eClaim.Prof_ord
			
			insvalSequence = mobjProf_ordseq.insValOS590(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("nMainAction"), mobjValues.StringToType(Request.Form.Item("tctServ_order"), eFunctions.Values.eTypeData.etdDouble))
			
			Session("nServ_order") = Request.Form.Item("tctserv_order")
			Session("nBranch") = mobjProf_ordseq.nBranch
			Session("sCertype") = mobjProf_ordseq.sCertype
			Session("nProduct") = mobjProf_ordseq.nProduct
			Session("nPolicy") = mobjProf_ordseq.nPolicy
			Session("nCertif") = mobjProf_ordseq.nCertif
			'Depende del origen se busca la fecha de la ultima modificación
			If mobjProf_ordseq.nOrdClass = 3 Then
				
				lclsClaimProf_ord = New eClaim.Claim
				
				Call lclsClaimProf_ord.Find(mobjProf_ordseq.nClaim)
				
				Session("dEffecdate") = lclsClaimProf_ord.dDecladat
				
				lclsClaimProf_ord = Nothing
			Else
				
				lclsCertificatProf_ord = New ePolicy.Certificat
				
				Call lclsCertificatProf_ord.Find(mobjProf_ordseq.sCertype, mobjProf_ordseq.nBranch, mobjProf_ordseq.nProduct, mobjProf_ordseq.nPolicy, mobjProf_ordseq.nCertif, True)
				
				Session("dEffecdate") = lclsCertificatProf_ord.dChangdat
				
				lclsCertificatProf_ord = Nothing
			End If
			Session("nOrdClass") = mobjProf_ordseq.nOrdClass
			Session("CallSequence") = "Prof_ord"
			
			mobjProf_ordseq = Nothing
			
			
			'+OS591: Aniversario de coberturas (Productos de Vida)
		Case "OS591"
			With Request
				mobjProf_ordseq = New eClaim.Auto_damage
				insvalSequence = mobjProf_ordseq.insValOS591("OS591", Request.QueryString.Item("Action"), Session("Nserv_order"), mobjValues.StringToType(.Form.Item("cbenpart_auto"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbendamag_auto"), eFunctions.Values.eTypeData.etdDouble))
				mobjProf_ordseq = Nothing
			End With
			
		Case "OS590", "OS592_1", "OS592_2", "OS592_3", "OS592_4", "OS592_5", "CA010", "IN010", "AU001"
			insvalSequence = vbNullString
			
		Case Else
			insvalSequence = "insvalSequence: Código lógico no encontrado (" & Request.QueryString.Item("sCodispl") & ")"
	End Select
End Function

'% insPostSequence: Se realizan las actualizaciones de las ventanas
'--------------------------------------------------------------------------------------------
Function insPostSequence() As Boolean
	Dim lstrsurban As String
	Dim lstrsserver As String
	Dim lstrscorner As String
	Dim lssubway As String
	Dim lstrsSnow As String
	Dim lstrsFallplane As String
	Dim lstrsWind As String
	Dim lstrsShockauto As String
	Dim lstrsriverbed As String
	Dim lstrsstratobj As String
	Dim lstrsSea As String
	Dim lstrsterrefy As String
	Dim lstrsStorm As String
	Dim lstrsInflu_risk As String
	Dim lstrsAirport As String
	'--------------------------------------------------------------------------------------------
	Dim lblnPost As Boolean
	lblnPost = False
	
	Select Case Request.QueryString.Item("sCodispl")
		
		'+ OS590_K : Ordenes de servicio	
		Case "OS590_K"
			lblnPost = True
			
		Case "OS590"
			mobjProf_ordseq = New eClaim.Prof_ord
			lblnPost = mobjProf_ordseq.InsPostOS590Upd(Session("nServ_order"), mobjValues.StringToType(Request.Form.Item("tcdMadedate"), eFunctions.Values.eTypeData.etdDate), Request.Form.Item("tctPlace"), mobjValues.StringToType(Request.Form.Item("cbeMunicipality"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("cbeStatus_ord"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull)
			mobjProf_ordseq = Nothing
			
		Case "OS591"
			With Request
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					lblnPost = True
				Else
					If .QueryString.Item("nMainAction") <> CStr(eFunctions.Menues.TypeActions.clngActionQuery) Then
						If .QueryString.Item("WindowType") = "PopUp" Then
							mobjProf_ordseq = New eClaim.Auto_damage
							lblnPost = mobjProf_ordseq.InsPostOS591(.QueryString("Action"), mobjValues.StringToType(Session("nServ_order"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbenpart_auto"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbendamag_auto"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbendamage_magnif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tctndeduc"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
							mobjProf_ordseq = Nothing
						Else
							lblnPost = True
						End If
					Else
						lblnPost = True
					End If
				End If
			End With
			
		Case "OS592_1"
			With Request
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					lblnPost = True
				Else
					If .QueryString.Item("nMainAction") <> CStr(eFunctions.Menues.TypeActions.clngActionQuery) Then
						mobjProf_ordseq = New eClaim.Construction
						If .Form.Item("chkSubway") = "1" Then
							lssubway = "1"
						Else
							lssubway = "2"
						End If
						lblnPost = mobjProf_ordseq.InsPostOS592_1("Update", mobjValues.StringToType(Session("nServ_order"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnArea"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnOldness"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("optSta_local"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeStructure_wall"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeStruct_wallint"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeRoofType"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeStructure_type"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeStruct_mezz"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeSideCloseType"), eFunctions.Values.eTypeData.etdDouble, True), lssubway, mobjValues.StringToType(.Form.Item("tcnFloor"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnTotalFloor"), eFunctions.Values.eTypeData.etdDouble, True))
						mobjProf_ordseq = Nothing
					Else
						lblnPost = True
					End If
				End If
			End With
			
			'+ OS592_2: Riesgo de incendio
		Case "OS592_2"
			With Request
				lblnPost = True
				If .QueryString.Item("nMainAction") <> CStr(eFunctions.Menues.TypeActions.clngActionQuery) Then
					mobjProf_ordseq = New eClaim.Fire_risk
					lblnPost = mobjProf_ordseq.insPostOS592_2(mobjValues.StringToType(Session("nServ_order"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("cbeType"), .Form.Item("optSta_local"), Session("nUsercode"))
					mobjProf_ordseq = Nothing
				End If
			End With
			
		Case "OS592_3"
			With Request
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					lblnPost = True
				Else
					If .QueryString.Item("nMainAction") <> CStr(eFunctions.Menues.TypeActions.clngActionQuery) Then
						mobjProf_ordseq = New eClaim.Theft_risk
						If .Form.Item("chkcorner") = "1" Then
							lstrscorner = "1"
						Else
							lstrscorner = "2"
						End If
						If .Form.Item("chkurban") = "1" Then
							lstrsurban = "1"
						Else
							lstrsurban = "2"
						End If
						If .Form.Item("chkserver") = "1" Then
							lstrsserver = "1"
						Else
							lstrsserver = "2"
						End If
						lblnPost = mobjProf_ordseq.InsPostOS592_3("Update", mobjValues.StringToType(Session("nServ_order"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeSector_type"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeLevel_sector"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeLock_type"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("optwinprot"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeprotec_type"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("optsta_elecpub"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("optsta_elecpriv"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcndist_pol"), eFunctions.Values.eTypeData.etdDouble, True), lstrscorner, lstrsurban, lstrsserver, mobjValues.StringToType(.Form.Item("tcnnum_inhab"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnnum_beds"), eFunctions.Values.eTypeData.etdDouble, True))
						mobjProf_ordseq = Nothing
					Else
						lblnPost = True
					End If
				End If
			End With
		Case "OS592_4"
			With Request
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					lblnPost = True
				Else
					If .QueryString.Item("nMainAction") <> CStr(eFunctions.Menues.TypeActions.clngActionQuery) Then
						mobjProf_ordseq = New eClaim.Add_risk
						If .Form.Item("chkriverbed") = "1" Then
							lstrsriverbed = "1"
						Else
							lstrsriverbed = "2"
						End If
						If .Form.Item("chkInflu_risk") = "1" Then
							lstrsInflu_risk = "1"
						Else
							lstrsInflu_risk = "2"
						End If
						If .Form.Item("chkstratobj") = "1" Then
							lstrsstratobj = "1"
						Else
							lstrsstratobj = "2"
						End If
						If .Form.Item("chkterrefy") = "1" Then
							lstrsterrefy = "1"
						Else
							lstrsterrefy = "2"
						End If
						If .Form.Item("chkStorm") = "1" Then
							lstrsStorm = "1"
						Else
							lstrsStorm = "2"
						End If
						If .Form.Item("chkSnow") = "1" Then
							lstrsSnow = "1"
						Else
							lstrsSnow = "2"
						End If
						If .Form.Item("chkShockauto") = "1" Then
							lstrsShockauto = "1"
						Else
							lstrsShockauto = "2"
						End If
						If .Form.Item("chkFallplane") = "1" Then
							lstrsFallplane = "1"
						Else
							lstrsFallplane = "2"
						End If
						If .Form.Item("chkWind") = "1" Then
							lstrsWind = "1"
						Else
							lstrsWind = "2"
						End If
						If .Form.Item("chkAirport") = "1" Then
							lstrsAirport = "1"
						Else
							lstrsAirport = "2"
						End If
						If .Form.Item("chkSea") = "1" Then
							lstrsSea = "1"
						Else
							lstrsSea = "2"
						End If
						lblnPost = mobjProf_ordseq.InsPostOS592_4("Update", mobjValues.StringToType(Session("nServ_order"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeEarthquake"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeDamage"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeContainrisk"), eFunctions.Values.eTypeData.etdDouble, True), lstrsriverbed, mobjValues.StringToType(.Form.Item("tcnDist_river"), eFunctions.Values.eTypeData.etdDouble, True), lstrsInflu_risk, mobjValues.StringToType(.Form.Item("cbeInundat"), eFunctions.Values.eTypeData.etdDouble, True), lstrsstratobj, lstrsterrefy, mobjValues.StringToType(.Form.Item("cbeWaterpipe"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeDam_waterpipe"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeSewerpipe"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeDam_Sewerpipe"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeStatroof"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeDamroof"), eFunctions.Values.eTypeData.etdDouble, True), lstrsStorm, lstrsSnow, lstrsShockauto, lstrsFallplane, lstrsWind, lstrsAirport, mobjValues.StringToType(.Form.Item("tcndistair"), eFunctions.Values.eTypeData.etdDouble, True), lstrsSea, mobjValues.StringToType(.Form.Item("tcndistsea"), eFunctions.Values.eTypeData.etdDouble, True))
						mobjProf_ordseq = Nothing
					Else
						lblnPost = True
					End If
				End If
			End With
			
		Case "OS592_5"
			With Request
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					lblnPost = True
				Else
					If .QueryString.Item("nMainAction") <> CStr(eFunctions.Menues.TypeActions.clngActionQuery) Then
						If .QueryString.Item("WindowType") = "PopUp" Then
							mobjProf_ordseq = New eClaim.Adjacence
							lblnPost = mobjProf_ordseq.InsPostOS592_5(.QueryString("Action"), mobjValues.StringToType(Session("nServ_order"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbencardinal"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctdescript"), .Form.Item("tctmat_divid"), mobjValues.StringToType(.Form.Item("tcndistant"), eFunctions.Values.eTypeData.etdDouble, True))
							mobjProf_ordseq = Nothing
						Else
							lblnPost = True
						End If
					Else
						lblnPost = True
					End If
				End If
			End With
			
		Case "IN010"
			lblnPost = True
			
		Case "AU001"
			lblnPost = True
			
		Case "CA010"
			lblnPost = True
	End Select
	insPostSequence = lblnPost
End Function

'% insFinish: se activa al finalizar el proceso
'--------------------------------------------------------------------------------------------
Function insFinish() As Boolean
	'--------------------------------------------------------------------------------------------
	insFinish = True
End Function

</script>
<%Response.Expires = -1
mobjValues = New eFunctions.Values

mstrCommand = "&sModule=Prof_ord&sProject=Prof_ordseq&sCodisplReload=" & Request.QueryString.Item("sCodispl")
%>
<HTML>
<HEAD>
<SCRIPT>
//+ Variable para el control de versiones
        document.VssVersion="$$Revision: 3 $|$$Date: 15/10/03 18.00 $"
</SCRIPT>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
	<%=mobjValues.StyleSheet()%>



	
</HEAD>
<BODY>
<FORM ID=FORM1 NAME=FORM1>
<%
'+ Si no se han validado los campos de la página
If Request.Form.Item("sCodisplReload") = vbNullString Then
	mstrErrors = insvalSequence
	Session("sErrorTable") = mstrErrors
	Session("sForm") = Request.Form.ToString
Else
	Session("sErrorTable") = vbNullString
	Session("sForm") = vbNullString
End If

If Request.QueryString.Item("nAction") <> CStr(eFunctions.Menues.TypeActions.clngAcceptdataFinish) Then
	If mstrErrors > vbNullString Then
		With Response
			.Write("<SCRIPT LANGUAGE=JAVASCRIPT>")
			.Write("ShowPopUp(""/VTimeNet/Common/Errors.aspx?sSource=" & Server.URLEncode(mstrCommand) & "&sQueryString=" & Server.URLEncode(Request.Params.Get("Query_String")) & """, ""Prof_ordseqError"",660,330);")
			.Write(mobjValues.StatusControl(False, CShort(Request.QueryString.Item("nZone")), Request.QueryString.Item("WindowType")))
			.Write("</SCRIPT>")
		End With
	Else
		If insPostSequence Then
			If Request.QueryString.Item("WindowType") <> "PopUp" Then
				'+ Si se está tratando con un frame y no con la ventana principal de la secuencia, 
				'+ se mueve automaticamente a la siguiente página
				If Request.Form.Item("sCodisplReload") = vbNullString Then
					Response.Write("<SCRIPT>top.frames['fraSequence'].document.location='/VTimeNet/Prof_ord/Prof_ordseq/Sequence.aspx?nAction=" & Request.QueryString.Item("nMainAction") & "&nOpener=" & Request.QueryString.Item("sCodispl") & "&nMainAction=" & Request.QueryString.Item("nMainAction") & mstrCommand & "';</SCRIPT>")
				Else
					Response.Write("<SCRIPT>window.close();opener.top.frames['fraSequence'].document.location='/VTimeNet/Prof_ord/Prof_ordseq/Sequence.aspx?nMainAction=" & Request.QueryString.Item("nAction") & "&nOpener=" & Request.QueryString.Item("sCodispl") & mstrCommand & "';</SCRIPT>")
				End If
			Else
				'+ Se recarga la página que invocó la PopUp
				Select Case Request.QueryString.Item("sCodispl")
					Case "OS591"
						Response.Write("<SCRIPT>top.opener.document.location.href='" & Request.QueryString.Item("sCodispl") & ".aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&nMainAction=" & Request.QueryString.Item("nMainAction") & "'</SCRIPT>")
					Case "OS592_5"
						Response.Write("<SCRIPT>top.opener.document.location.href='" & Request.QueryString.Item("sCodispl") & ".aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&nMainAction=" & Request.QueryString.Item("nMainAction") & "'</SCRIPT>")
				End Select
			End If
		End If
	End If
Else
	'+ Se recarga la página principal de la secuencia
	Session("CallSequence") = vbNullString
	
	'+Se limpia variable de session debido a que la CA010 tambien esta en la secuencia
	'+de cartera y al momento de emitir una orden de servicio e ir a la secuencia de cartera 
	'+este estaba mostrando el numero de orden que tenia grabado en la secuencia.
	Session("nServ_order") = vbNullString
	If insFinish() Then
		With Response
			.Write("<SCRIPT>")
			.Write("insReloadTop(false)")
			.Write("</SCRIPT>")
		End With
	End If
End If
mobjProf_ordseq = Nothing
mobjValues = Nothing
%>
</FORM>
</BODY>
</HTML>






