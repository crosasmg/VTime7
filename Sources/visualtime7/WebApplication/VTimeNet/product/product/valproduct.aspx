<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eProduct" %>
<script language="VB" runat="Server">

    '+ Se define la contante para el manejo de errores en caso de advertencias
    Dim mstrCommand As String

    '+ Variable para el manejo del QueryString
    Dim mstrQueryString As String

    Dim mstrErrors As String
    Dim mobjValues As eFunctions.Values


    '% insValProduct: Se realizan las validaciones masivas de la forma dependiendo del sCodispl.
    '--------------------------------------------------------------------------------------------
    Function insValProduct() As String
        '--------------------------------------------------------------------------------------------
        Dim lclsProduct As eProduct.Product
        Dim lclsMortality As eProduct.Mortality
        Dim lclsConmutativ As eProduct.Conmutativ
        Dim lclsRate_life As eProduct.Rate_life

        insValProduct = vbNullString

        Dim lclsBranches As eProduct.Branches
        Dim lclsTab_gencov As eProduct.Tab_gencov
        Select Case Request.QueryString.Item("sCodispl")

        '+ Ramos comerciales.
            Case "DP001"

                lclsBranches = New eProduct.Branches

                If Request.QueryString.Item("WindowType") = "PopUp" Then
                    insValProduct = lclsBranches.insValDP001_k(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("Action"), mobjValues.StringToType(Request.Form.Item("tcnBranch"), eFunctions.Values.eTypeData.etdDouble), Request.Form.Item("tctDescript"), Request.Form.Item("tctShort_des"), Request.Form.Item("tctTabname"), Request.Form.Item("cbeStatregt"))
                End If

                lclsBranches = Nothing

            '+ Productos de un Ramo comercial.
            Case "DP002"
                lclsProduct = New eProduct.Product

                If Request.QueryString.Item("nZone") = "1" Then
                    insValProduct = lclsProduct.insValDP002_k(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("Action"), mobjValues.StringToType(Request.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcdDate"), eFunctions.Values.eTypeData.etdDate))
                Else
                    If Request.QueryString.Item("WindowType") = "PopUp" Then
                        insValProduct = lclsProduct.insValDP002(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("Action"), Session("nBranch"), mobjValues.StringToType(Request.Form.Item("tcnProduct"), eFunctions.Values.eTypeData.etdDouble), Request.Form.Item("tctDescript"), Request.Form.Item("tctShort_des"), Request.Form.Item("cbeBrancht"), Request.Form.Item("cbeStatregt"))
                    End If
                End If

                lclsProduct = Nothing

            '+ Duplicar productos.
            Case "DP063"
                lclsProduct = New eProduct.Product

                If Request.QueryString.Item("WindowType") = "PopUp" Then
                    insValProduct = lclsProduct.insValDP063(Request.QueryString.Item("sCodispl"), eFunctions.Menues.TypeActions.clngActionDuplicateProduct, mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnProductNew"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcdDateNew"), eFunctions.Values.eTypeData.etdDate))
                End If

            '+ Consulta de coberturas genéricas.
            Case "DP039"
                With Request
                    If CDbl(.QueryString.Item("nZone")) = 1 Then
                        lclsTab_gencov = New eProduct.Tab_gencov
                        insValProduct = lclsTab_gencov.insValHeaderDP039("DP039", mobjValues.StringToType(.Form.Item("cbeCurrency"), eFunctions.Values.eTypeData.etdDouble))
                        lclsTab_gencov = Nothing
                    End If
                End With

            '+ Parámetros para la tabla de mortalidad.
            Case "DP013"
                lclsMortality = New eProduct.Mortality

                If Request.QueryString.Item("nZone") = "1" Then
                    insValProduct = lclsMortality.insValDP013_k(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("nMainAction"), Request.Form.Item("valMortalco"))
                Else
                    If Request.QueryString.Item("nZone") = "2" And CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) <> 401 Then
                        If Request.QueryString.Item("WindowType") <> "PopUp" Then
                            insValProduct = lclsMortality.insValDP013(Request.QueryString.Item("sCodispl"), Session("sMortalco"), mobjValues.StringToType(Request.Form.Item("tcnInit_Age"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnEnd_Age"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnLive_lx"), eFunctions.Values.eTypeData.etdDouble))
                        Else
                            insValProduct = lclsMortality.insValDP013UPD(Request.QueryString.Item("sCodispl"), mobjValues.StringToType(Request.Form.Item("tcnDeath_qx"), eFunctions.Values.eTypeData.etdDouble))
                        End If
                    End If
                End If

                lclsMortality = Nothing

            '+ Generación de valores conmutativos.
            Case "DP015"
                lclsConmutativ = New eProduct.Conmutativ

                insValProduct = lclsConmutativ.insValDP015_k(Request.QueryString.Item("sCodispl"), Request.Form.Item("valMortalco"), mobjValues.StringToType(Request.Form.Item("tcnInterest"), eFunctions.Values.eTypeData.etdDouble))

                lclsConmutativ = Nothing

            '+ Modificación de valores conmutativos.
            Case "DP016"
                lclsConmutativ = New eProduct.Conmutativ

                If Request.QueryString.Item("nZone") = "1" Then
                    insValProduct = lclsConmutativ.insValDP016_k(Request.QueryString.Item("sCodispl"), Request.Form.Item("valMortalco"), mobjValues.StringToType(Request.Form.Item("tcnInterest"), eFunctions.Values.eTypeData.etdDouble))
                Else
                    If Request.QueryString.Item("WindowType") = "PopUp" Then
                        insValProduct = lclsConmutativ.insValDP016(Request.QueryString.Item("sCodispl"), mobjValues.StringToType(Request.Form.Item("tcnDeath_dx"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnLiver_px"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnConmu_dx"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnConmu_cx"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnConmu_nx"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnConmu_mx"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnConmu_sx"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnConmu_rx"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnConmu_tx"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnConmu_vx"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnConmu_ex"), eFunctions.Values.eTypeData.etdDouble))
                    End If
                End If

                lclsConmutativ = Nothing

            '+ Tasas según edades para planes de vida.
            Case "DP017"
                lclsRate_life = New eProduct.Rate_life

                If Request.QueryString.Item("nZone") = "1" Then
                    insValProduct = lclsRate_life.insValDP017_k(Request.QueryString.Item("sCodispl"), CInt(Request.QueryString.Item("nMainAction")), mobjValues.StringToType(Request.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("valCover"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("tcdDate"), eFunctions.Values.eTypeData.etdDate))
                Else
                    If Request.QueryString.Item("WindowType") = "PopUp" Then
                        insValProduct = lclsRate_life.insValDP017(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("Action"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Session("nCover"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.Form.Item("tcnAgeStart"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnAgeEnd"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("tcnRatepure"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("tcnRatenoni"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("tcnRatenive"), eFunctions.Values.eTypeData.etdDouble, True))
                    End If
                End If

                lclsRate_life = Nothing
            Case Else
                insValProduct = "insValProduct: Código lógico no encontrado (" & Request.QueryString.Item("sCodispl") & ")"
        End Select
    End Function

    '% insPostProduct: Se realizan las actualizaciones a las tablas dependiendo del sCodispl de la 
    '% ventana.
    '--------------------------------------------------------------------------------------------
    Function insPostProduct() As Boolean
        '--------------------------------------------------------------------------------------------
        Dim lclsProduct As eProduct.Product
        Dim lclsMortality As eProduct.Mortality
        Dim lclsConmutativ As eProduct.Conmutativ
        Dim lclsRate_life As eProduct.Rate_life

        insPostProduct = True

        Dim lclsBranches As eProduct.Branches
        Select Case Request.QueryString.Item("sCodispl")

        '+ Ramos comerciales.
            Case "DP001"

                lclsBranches = New eProduct.Branches

                insPostProduct = lclsBranches.insPostDP001(Request.QueryString.Item("Action"), mobjValues.StringToType(Request.Form.Item("tcnBranch"), eFunctions.Values.eTypeData.etdDouble), Request.Form.Item("tctDescript"), Request.Form.Item("tctShort_des"), Request.Form.Item("tctTabName"), Request.Form.Item("cbeStatregt"), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nInsur_Area"), eFunctions.Values.eTypeData.etdInteger))
                lclsBranches = Nothing

            '+ Productos de un Ramo comercial.
            Case "DP002"
                If Request.QueryString.Item("WindowType") <> "PopUp" Then
                    Session("nBranch") = Request.Form.Item("cbeBranch")
                    Session("dEffecdate") = Request.Form.Item("tcdDate")
                Else
                    lclsProduct = New eProduct.Product

                    If Request.QueryString.Item("Action") = "Add" Then
                        insPostProduct = True
                        Session("DP003_sLinkSpecial") = "1"
                        Session("DP003_nBranch") = Session("nBranch")
                        Session("DP003_nBrancht") = Request.Form.Item("cbeBrancht")
                        Session("DP003_nProduct") = Request.Form.Item("tcnProduct")
                        Session("DP003_dEffecdate") = Session("dEffecdate")
                        Session("DP003_sDescript") = Request.Form.Item("tctDescript")
                        Session("DP003_sShort_des") = Request.Form.Item("tctShort_des")
                    Else
                        insPostProduct = lclsProduct.insPostDP002(Request.QueryString.Item("Action"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.Form.Item("tcnProduct"), eFunctions.Values.eTypeData.etdDouble), Request.Form.Item("tctDescript"), Request.Form.Item("tctShort_des"), Request.Form.Item("cbeBrancht"), Request.Form.Item("cbeStatregt"), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
                    End If
                End If

            '+ Duplicar productos.
            Case "DP063"
                lclsProduct = New eProduct.Product

                insPostProduct = lclsProduct.insPostDP063(CStr(eFunctions.Menues.TypeActions.clngActionDuplicateProduct), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnProductNew"), eFunctions.Values.eTypeData.etdDouble), CDate(Request.QueryString.Item("dEffecdate")), CDate(Request.Form.Item("tcdDateNew")), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))

            '+ Consulta de coberturas genéricas.
            Case "DP039"
                With Request
                    Session("nCurrency") = .Form.Item("cbeCurrency")
                    Session("nTypCov") = .Form.Item("optTypCov")
                End With

            '+ Parámetros para la tabla de mortalidad.
            Case "DP013"
                With Request
                    If Request.QueryString.Item("nZone") = "1" Then
                        Session("sMortalco") = Request.Form.Item("valMortalco")
                    Else
                        If Request.QueryString.Item("nZone") = "2" And CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) <> 401 Then
                            lclsMortality = New eProduct.Mortality
                            If Request.QueryString.Item("WindowType") <> "PopUp" Then
                                If Request.QueryString.Item("nAction") = CStr(eFunctions.Menues.TypeActions.clngAcceptdataFinish) Then
                                    insPostProduct = lclsMortality.insPostDP013(2, Session("sMortalco"), mobjValues.StringToType(Request.Form.Item("tcnAge"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnMonth"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnDeath_qx"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnLive_lx"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
                                Else
                                    insPostProduct = True
                                    mstrQueryString = "&sReLoadDP013=1" & "&nInitAge=" & .Form.Item("tcnInit_age") & "&nEndAge=" & .Form.Item("tcnEnd_age") & "&nInitAgeOld=" & .Form.Item("hddnInitAgeOld") & "&nEndAgeOld=" & .Form.Item("hddnEndAgeOld") & "&nLiveLx=" & .Form.Item("tcnLive_lx")
                                End If
                            Else
                                insPostProduct = lclsMortality.insPostDP013(mobjValues.StringToType(Request.Form.Item("Exist"), eFunctions.Values.eTypeData.etdDouble), Session("sMortalco"), mobjValues.StringToType(Request.Form.Item("tcnAge"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnMonth"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnDeath_qx"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnLivelxAux"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
                            End If
                        End If
                    End If
                End With

            '+ Generación de valores conmutativos.
            Case "DP015"
                lclsConmutativ = New eProduct.Conmutativ

                insPostProduct = lclsConmutativ.insPostDP015(Request.Form.Item("valMortalco"), mobjValues.StringToType(Request.Form.Item("tcnInterest"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))

            '+ Modificación de valores conmutativos.
            Case "DP016"
                If Request.QueryString.Item("WindowType") <> "PopUp" Then
                    Session("sMortalco") = Request.Form.Item("valMortalco")
                    Session("nInterest") = Request.Form.Item("tcnInterest")
                Else
                    lclsConmutativ = New eProduct.Conmutativ

                    insPostProduct = lclsConmutativ.insPostDP016(Session("sMortalco"), mobjValues.StringToType(Session("nInterest"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnAge"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnMonth"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnConmu_dx"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnConmu_cx"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnConmu_mx"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnConmu_nx"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnConmu_rx"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnConmu_sx"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnConmu_tx"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnDeath_dx"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnLiver_px"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnConmu_vx"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnConmu_ex"), eFunctions.Values.eTypeData.etdDouble))
                End If

            '+ Tasas según edades para planes de vida.
            Case "DP017"
                If Request.QueryString.Item("WindowType") <> "PopUp" Then
                    Session("nBranch") = Request.Form.Item("cbeBranch")
                    Session("nProduct") = Request.Form.Item("valProduct")
                    Session("nCover") = Request.Form.Item("valCover")
                    Session("dEffecdate") = Request.Form.Item("tcdDate")
                Else
                    lclsRate_life = New eProduct.Rate_life
                    insPostProduct = lclsRate_life.insPostDP017(Request.QueryString.Item("Action"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCover"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.Form.Item("tcnAgeStart"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnAgeEnd"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnRatepure"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnRatenoni"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnRatenive"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
                End If
            Case Else
                insPostProduct = False
        End Select
    End Function

</script>
<%Response.Expires = -1

mstrCommand = "&sModule=Product&sProject=Product&sCodisplReload=" & Request.QueryString.Item("sCodispl")
%>
<HTML>
<HEAD>
	<LINK REL="StyleSheet" TYPE="text/css" HREF="/VTimeNet/Common/Custom.css">
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>



	
</HEAD>
<BODY>
<SCRIPT>
//% CancelErrors: Va a la ventana anterior si se produce un error.
//------------------------------------------------------------------------------------
function CancelErrors(){
//------------------------------------------------------------------------------------
	self.history.go(-1)
}

//% NewLocation: Se posiciona en la página seleccionada. 
//------------------------------------------------------------------------------------
function NewLocation(Source,Codisp){
//------------------------------------------------------------------------------------
    var lstrLocation = "";
    
    lstrLocation += Source.location;
    lstrLocation = lstrLocation.replace(/&OPENER=.*/,"") + "&OPENER=" + Codisp;
    Source.location = lstrLocation
}
</SCRIPT>
<SCRIPT src="/VTimeNet/scripts/GenFunctions.js"> </SCRIPT>
<%
mobjValues = New eFunctions.Values

'+ Si no se han validado los campos de la página.
If Request.Form.Item("sCodisplReload") = vbNullString Then
	mstrErrors = insValProduct
	Session("sErrorTable") = mstrErrors
	Session("sForm") = Request.Form.ToString
Else
	Session("sErrorTable") = vbNullString
	Session("sForm") = vbNullString
End If

'+ Si se produce un error en la ventana envía las validaciones respectivas.
If mstrErrors > vbNullString Then
	With Response
		.Write("<SCRIPT LANGUAGE=JAVASCRIPT>")
		.Write("ShowPopUp(""/VTimeNet/Common/Errors.aspx?sSource=" & Server.URLEncode(mstrCommand) & "&sQueryString=" & Server.URLEncode(Request.Params.Get("Query_String")) & """, ""ProductErrors"",660,330);")
		If Request.QueryString.Item("sCodispl") = "DP063" Then
			.Write("self.history.go(-1);")
		Else
			.Write("document.location.href='/VTimeNet/common/blank.htm';")
		End If
		.Write(mobjValues.StatusControl(False, CShort(Request.QueryString.Item("nZone")), Request.QueryString.Item("WindowType")))
		.Write("</SCRIPT>")
	End With
Else
	
	'+ Si no se produce un ningún error en la ventana se realiza el llamado al Post.
	If insPostProduct Then
		If Request.QueryString.Item("WindowType") <> "PopUp" Then
			If Request.QueryString.Item("nAction") = CStr(eFunctions.Menues.TypeActions.clngAcceptdataFinish) Then
				If Request.Form.Item("sCodisplReload") = vbNullString Then
					Response.Write("<SCRIPT>top.document.location.reload();</SCRIPT>")
				Else
					Response.Write("<SCRIPT>window.close();opener.top.document.location.reload();</SCRIPT>")
				End If
			Else
				If Request.QueryString.Item("sCodispl") = "DP015" Then
					Response.Write("<SCRIPT>insReloadTop(false);</SCRIPT>")
				Else
					If Request.Form.Item("sCodisplReload") = vbNullString Then
						Response.Write("<SCRIPT>self.history.go(-1);top.fraFolder.document.location=""" & Replace(UCase(Request.QueryString.Item("sCodispl")), "_K", "") & ".aspx?nMainAction=" & Request.QueryString.Item("nMainAction") & "&sCodispl=" & Replace(UCase(Request.QueryString.Item("sCodispl")), "_K", "") & mstrQueryString & """" & ";</SCRIPT>")
					Else
						If CDbl(Request.QueryString.Item("nZone")) = 1 Then
							Response.Write("<SCRIPT>window.close();top.opener.top.fraHeader.document.location=""" & Request.QueryString.Item("sCodispl") & "_K.aspx?nMainAction=" & Request.QueryString.Item("nMainAction") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & """" & ";</SCRIPT>")
						Else
							Response.Write("<SCRIPT>window.close();top.opener.top.fraFolder.document.location=""" & Replace(UCase(Request.QueryString.Item("sCodispl")), "_K", "") & ".aspx?nMainAction=" & Request.QueryString.Item("nMainAction") & "&sCodispl=" & Replace(UCase(Request.QueryString.Item("sCodispl")), "_K", "") & mstrQueryString & """" & ";</SCRIPT>")
						End If
					End If
				End If
			End If
		Else
			
			'+ Se recarga la página que invocó la PopUp.
			Select Case Request.QueryString.Item("sCodispl")
				Case "DP001"
					Response.Write("<SCRIPT>top.opener.document.location.href='DP001_K.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&nMainAction=302'</SCRIPT>")
				Case "DP002"
					If Request.QueryString.Item("Action") = "Add" Then
						Response.Write("<SCRIPT>top.opener.document.location.href='/VTimeNet/common/GoTo.aspx?sCodispl=DP003_K'</SCRIPT>")
					Else
						Response.Write("<SCRIPT>top.opener.document.location.href='DP002.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&nMainAction=302'</SCRIPT>")
					End If
				Case "DP063"
					Response.Write("<SCRIPT>top.opener.document.location.href='DP002.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&nMainAction=310'</SCRIPT>")
				Case "DP039"
					Response.Write("<SCRIPT>top.opener.document.location.href='DP039_k.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&nMainAction=302'</SCRIPT>")
				Case "DP013"
					Response.Write("<SCRIPT>top.opener.document.location.href='DP013.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&nMainAction=302'</SCRIPT>")
				Case "DP016"
					Response.Write("<SCRIPT>top.opener.document.location.href='DP016.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&nMainAction=302'</SCRIPT>")
				Case "DP017"
					Response.Write("<SCRIPT>top.opener.document.location.href='DP017.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&nMainAction=302'</SCRIPT>")
			End Select
		End If
	End If
End If

mobjValues = Nothing
%>
</BODY>
</HTML>





