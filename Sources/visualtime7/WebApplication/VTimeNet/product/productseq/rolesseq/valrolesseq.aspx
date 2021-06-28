<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eProduct" %>
<%@ Import namespace="eGeneral" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Se define la contante para el manejo de errores en caso de advertencias
Dim mstrCommand As String

'- Variable para el manejo de los errores de la página, devueltos por insvalSequence
Dim mstrErrors As String

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjRolesSeq As Object


'% insvalSequence: Se realizan las validaciones masivas de las páginas
'--------------------------------------------------------------------------------------------
Function insvalSequence() As String
	'--------------------------------------------------------------------------------------------
	mobjRolesSeq = New eProduct.Tab_covrol
	Select Case Request.QueryString.Item("sCodispl")
		'+ GE101: Cancelación del proceso
		Case "GE101"
			insvalSequence = vbNullString
			
			'+ DP19AP: Condiciones del capital asegurado
		Case "DP19AP"
			With Request
				insvalSequence = mobjRolesSeq.InsValDP19AP("DP19AP", mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nModulec"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCover"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nRole"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), .Form.Item("OptCalc"), mobjValues.StringToType(.Form.Item("tcnCacalfix"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnCapbaspe"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valCacalcov"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnCapminim"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnCapmaxim"), eFunctions.Values.eTypeData.etdDouble, True), .Form.Item("tctRouprcal"))
			End With
			
			'+ DP19BP: Condiciones de prima y siniestros
		Case "DP19BP"
			With Request
				insvalSequence = mobjRolesSeq.InsValDP19BP("DP19BP", mobjValues.StringToType(.Form.Item("valCover_in"), eFunctions.Values.eTypeData.etdDouble, True), .Form.Item("tctRoupremi"), mobjValues.StringToType(.Form.Item("tcnPremirat"), eFunctions.Values.eTypeData.etdDouble, True), .Form.Item("tctCldeathi"), .Form.Item("tctClaccidi"), .Form.Item("tctClvehaci"), .Form.Item("tctClsurvii"), .Form.Item("tctClincapi"), .Form.Item("tctClinvali"), .Form.Item("tctClillness"), mobjValues.StringToType(CStr(0), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(CStr(0), eFunctions.Values.eTypeData.etdDouble, True), vbNullString, vbNullString, mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcnPremifix"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valid_table"), eFunctions.Values.eTypeData.etdDouble))
			End With
			
			'+ DP50AP: Duración y condiciones de renovación
		Case "DP50AP"
			With Request
				insvalSequence = mobjRolesSeq.InsValDP50AP("DP50AP", mobjValues.StringToType(.Form.Item("cbeTypDurins"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnDuratInd"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeTypDurpay"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnDuratPay"), eFunctions.Values.eTypeData.etdDouble, True), .Form.Item("tctRout_pay"), mobjValues.StringToType(.Form.Item("tcnAgemininsm"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnAgemaxinsm"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnAgemaxperm"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnAgemininsf"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnAgemaxinsf"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnAgemaxperf"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbenTypAgeMinM"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbenTypAgeMinF"), eFunctions.Values.eTypeData.etdDouble, True))
			End With
			
			'+ DP035B: Franquicia/deducible
		Case "DP035B"
			With Request
				insvalSequence = mobjRolesSeq.InsValDP035B("DP035B", .Form.Item("cbeFrantype"), .Form.Item("tctRoufranc"), mobjValues.StringToType(.Form.Item("tcnFrancrat"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnFrancFix"), eFunctions.Values.eTypeData.etdDouble, True), .Form.Item("cbeFrancApl"), mobjValues.StringToType(.Form.Item("tcnFrancMin"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnFrancMax"), eFunctions.Values.eTypeData.etdDouble, True), .Form.Item("cbeCaren_type"), mobjValues.StringToType(.Form.Item("tcnCaren_quan"), eFunctions.Values.eTypeData.etdDouble, True))
				
			End With
			
			'+ DP8003: Capitales por edad actuarial        
		Case "DP8003"
			mobjRolesSeq = New eProduct.Capital_age
			With Request
				If .QueryString.Item("WindowType") = "PopUp" Then
					insvalSequence = mobjRolesSeq.InsValDP8003(Request.QueryString.Item("sCodispl"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcnAge_end"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(.Form.Item("tcnAge_init"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(.Form.Item("tcnCapmaxim"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCapmini"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nModulec"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(Session("nCover"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(Session("nRole"), eFunctions.Values.eTypeData.etdLong), .QueryString("Action"))
				End If
			End With
			'Set mobjRolesSeq = Nothing
			
		Case Else
			insvalSequence = "insvalSequence: Código lógico no encontrado (" & Request.QueryString.Item("sCodispl") & ")"
	End Select
End Function

'% insPostSequence: Se realizan las actualizaciones de las ventanas
'--------------------------------------------------------------------------------------------
Function insPostSequence() As Boolean
	'--------------------------------------------------------------------------------------------
	Dim lblnPost As Boolean
	lblnPost = False
	
	mobjRolesSeq = New eProduct.Tab_covrol
	Select Case Request.QueryString.Item("sCodispl")
		'+ GE101: Cancelación del proceso
		Case "GE101"
			lblnPost = insCancel()
			
			'+ DP19AP: Condiciones del capital asegurado
		Case "DP19AP"
			With Request
				lblnPost = mobjRolesSeq.InsPostDP19AP(mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nModulec"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCover"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nRole"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), .Form.Item("OptCalc"), mobjValues.StringToType(.Form.Item("tcnCacalfix"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnCapbaspe"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valCacalcov"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valCacalcov_nRole"), eFunctions.Values.eTypeData.etdDouble, True), .Form.Item("OptAddsuini"), .Form.Item("tctRouprcal"), mobjValues.StringToType(.Form.Item("tcnCapminim"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnCapmaxim"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnCacalmul"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnChcaplev"), eFunctions.Values.eTypeData.etdDouble, True), .Form.Item("chkCh_typ_capAdd"), .Form.Item("chkCh_typ_capSub"), mobjValues.StringToType(.Form.Item("tcnRatecapadd"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnRatecapsub"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnCaMaxPer"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valCaMaxCov"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valCaMaxCov_nRole"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("chkLeg"), mobjValues.StringToType(.Form.Item("tcnQmonth_vig"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnQbetweenmod"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnQmax_mod"), eFunctions.Values.eTypeData.etdDouble, True), .Form.Item("tctROU_COND_CAP"))
			End With
			
			'+ DP19BP: Condiciones de prima y siniestros
		Case "DP19BP"
			With Request
                    lblnPost = mobjRolesSeq.InsPostDP19BP(mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nModulec"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCover"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nRole"), eFunctions.Values.eTypeData.etdDouble), Session("dEffecdate"), mobjValues.StringToType(.Form.Item("valCover_in"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valCover_in_nRole"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnPremirat"), eFunctions.Values.eTypeData.etdDouble, True), .Form.Item("tctRoupremi"), .Form.Item("chkChange_typAdd"), .Form.Item("chkChange_typSub"), mobjValues.StringToType(.Form.Item("tcnRatepreadd"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnRatepresub"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnChprelev"), eFunctions.Values.eTypeData.etdDouble, True), .Form.Item("tctClaccidi"), .Form.Item("tctCldeathi"), .Form.Item("tctClincapi"), .Form.Item("tctClinvali"), .Form.Item("tctClsurvii"), .Form.Item("tctClvehaci"), .Form.Item("tctClillness"), mobjValues.StringToType(CStr(0), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(CStr(0), eFunctions.Values.eTypeData.etdDouble, True), vbNullString, vbNullString, mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnMaxrent"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPremifix"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnPremimax"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valid_table"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnPercCostFP"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnRecCostFP"), eFunctions.Values.eTypeData.etdDouble, True), .Form.Item("chkTypeAge"), .Form.Item("tctRourate"))
			End With
			
			'+ DP50AP: Duración y condiciones de renovación
		Case "DP50AP"
			With Request
				lblnPost = mobjRolesSeq.InsPostDP50AP(mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nModulec"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCover"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nRole"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("cbeTypDurins"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnDuratInd"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeTypDurpay"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnDuratPay"), eFunctions.Values.eTypeData.etdDouble, True), .Form.Item("tctRout_pay"), mobjValues.StringToType(.Form.Item("tcnAgemininsm"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnAgemaxinsm"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnAgemaxperm"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnAgemininsf"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnAgemaxinsf"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnAgemaxperf"), eFunctions.Values.eTypeData.etdDouble, True), .Form.Item("chkRenewali"), .Form.Item("chkRevIndex"), .Form.Item("chkRechapri"), .Form.Item("tctRouchapr"), .Form.Item("tctRouchaca"), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbenTypAgeMinM"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbenTypAgeMinF"), eFunctions.Values.eTypeData.etdDouble))
			End With
			
			'+ DP035B: Franquicia/deducible
		Case "DP035B"
			With Request
				lblnPost = mobjRolesSeq.InsPostDP035B(mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nModulec"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCover"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nRole"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), .Form.Item("chkFDRequire"), .Form.Item("cbeFrantype"), .Form.Item("tctRoufranc"), mobjValues.StringToType(.Form.Item("tcnFrancrat"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnFrancFix"), eFunctions.Values.eTypeData.etdDouble, True), .Form.Item("cbeFrancApl"), mobjValues.StringToType(.Form.Item("tcnFrancMin"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnFrancMax"), eFunctions.Values.eTypeData.etdDouble, True), .Form.Item("chkFdChantypAdd"), .Form.Item("chkFdChantypSub"), mobjValues.StringToType(.Form.Item("tcnFDRateAdd"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnFDRateSub"), eFunctions.Values.eTypeData.etdDouble, True), .Form.Item("cbeCaren_type"), mobjValues.StringToType(.Form.Item("tcnCaren_quan"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnFDUserLev"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
				Response.Write(lblnPost)
			End With
			
			'+ DP8003: Capitales por edad actuarial			
		Case "DP8003"
			mobjRolesSeq = New eProduct.Capital_age
			
			With Request
				lblnPost = True
				If .QueryString.Item("WindowType") = "PopUp" Then
					lblnPost = mobjRolesSeq.InsPostDP8003(.QueryString("Action"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcnAge_end"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(.Form.Item("tcnAge_init"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(.Form.Item("tcnCapmaxim"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCapmini"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(Session("nModulec"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(Session("nCover"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(Session("nRole"), eFunctions.Values.eTypeData.etdLong))
				End If
				
			End With
			'Set mobjRolesSeq = Nothing
			
	End Select
	insPostSequence = lblnPost
End Function

'% insFinish: se activa al finalizar el proceso
'--------------------------------------------------------------------------------------------
Function insFinish() As Boolean
	'--------------------------------------------------------------------------------------------
	Dim lclsTab_covrol As eProduct.Tab_covrol
	Dim lclsGeneral As eGeneral.GeneralFunction
	
	lclsTab_covrol = New eProduct.Tab_covrol
	insFinish = True
	
	With lclsTab_covrol
		If .insvalSequence(mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nModulec"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCover"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nRole"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate)) Then
			insFinish = .InsFinishSequence(mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nModulec"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCover"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nRole"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
		Else
			insFinish = False
			lclsGeneral = New eGeneral.GeneralFunction
			Response.Write("<SCRIPT>")
			Response.Write("top.frames['fraFolder'].document.location.reload();")
			Response.Write("alert(""" & lclsGeneral.insLoadMessage(3902) & """);")
			Response.Write("</" & "Script>")
			lclsGeneral = Nothing
		End If
	End With
	lclsTab_covrol = Nothing
End Function

'% insCancel: se activa al finalizar el proceso
'--------------------------------------------------------------------------------------------
Private Function insCancel() As Boolean
	'--------------------------------------------------------------------------------------------
	insCancel = False
	
	If Request.Form.Item("optElim") = "Delete" Then
		Call mobjRolesSeq.insPostDP705("Del", Session("nBranch"), Session("nProduct"), Session("nModulec"), Session("nCover"), Session("nRole"), Session("dEffecdate"), vbNullString, Session("sBrancht"), vbNullString, vbNullString, 1, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, Session("nUsercode"))
		
	End If
	
	With Response
		.Write("<SCRIPT>")
		.Write("var lstrHref = '/VTimeNet/Product/ProductSeq/DP705.aspx?sOnSeq=1&sCodispl=DP705&nMainAction=302&nModulec=" & Session("nModulec") & "&nCover=" & Session("nCover") & "';")
		.Write("opener.top.close();")
		.Write("opener.top.opener.top.frames['fraFolder'].location.href=lstrHref;")
		.Write("top.close();")
		.Write("</" & "Script>")
	End With
End Function

</script>
<%Response.Expires = 0
mobjValues = New eFunctions.Values
mstrCommand = "sModule=Product&sProject=ProductSeq&sSubProject=RolesSeq&sCodisplReload=" & Request.QueryString.Item("sCodispl")
%>
<HTML>
<HEAD>
 	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
	<%=mobjValues.StyleSheet()%>



	
</HEAD>
<BODY>
<FORM ID=form1 NAME=form1>
<SCRIPT>
//% NewLocation:
//-------------------------------------------------------------------------------------------
function NewLocation(Source,Codisp){
//-------------------------------------------------------------------------------------------
    var lstrLocation = "";
    lstrLocation += Source.location;
    lstrLocation = lstrLocation.replace(/&OPENER=.*/,"") + "&OPENER=" + Codisp
    Source.location = lstrLocation
}
</SCRIPT>
<%
If Request.QueryString.Item("nAction") <> CStr(eFunctions.Menues.TypeActions.clngAcceptdataFinish) Then
	'+ Si no se han validado los campos de la página
	If Request.Form.Item("sCodisplReload") = vbNullString Then
		mstrErrors = insvalSequence
		Session("sErrorTable") = mstrErrors
		Session("sForm") = Request.Form.ToString
	Else
		Session("sErrorTable") = vbNullString
		Session("sForm") = vbNullString
	End If
	
	If mstrErrors > vbNullString Then
		With Response
			.Write("<SCRIPT LANGUAGE=JAVASCRIPT>")
			.Write("ShowPopUp(""/VTimeNet/Common/Errors.aspx?sSource=" & Server.URLEncode(mstrCommand) & "&sQueryString=" & Server.URLEncode(Request.Params.Get("Query_String")) & """, ""RolesSeqError"",660,330);document.location.href='/VTimeNet/common/blank.htm';")
			.Write(mobjValues.StatusControl(False, CShort(Request.QueryString.Item("nZone")), Request.QueryString.Item("WindowType")))
			.Write("</SCRIPT>")
		End With
	Else
		If insPostSequence Then
			If Request.QueryString.Item("WindowType") <> "PopUp" Then
				'+ Si se está tratando con un frame y no con la ventana principal de la secuencia, 
				'+ se mueve automaticamente a la siguiente página
				
				If Request.Form.Item("sCodisplReload") = vbNullString Then
					Response.Write("<SCRIPT>top.frames['fraSequence'].document.location='/VTimeNet/Product/ProductSeq/RolesSeq/Sequence.aspx?sGoToNext=Yes&nAction=" & Request.QueryString.Item("nMainAction") & "&nOpener=" & Request.QueryString.Item("sCodispl") & "';</SCRIPT>")
				Else
					Response.Write("<SCRIPT>window.close();opener.top.frames['fraSequence'].document.location='/VTimeNet/Product/ProductSeq/RolesSeq/Sequence.aspx?sGoToNext=Yes&nAction=" & Request.QueryString.Item("nMainAction") & "&nOpener=" & Request.QueryString.Item("sCodispl") & "';</SCRIPT>")
				End If
			Else
				'+ Se recarga la página que invocó la PopUp
				Select Case Request.QueryString.Item("sCodispl")
					Case "GE101"
						Response.Write("<SCRIPT>top.opener.top.close();</SCRIPT>")
					Case "DP8003"
						Response.Write("<SCRIPT>top.opener.document.location.href='" & Request.QueryString.Item("sCodispl") & ".aspx?sCodispl=" & Request.QueryString.Item("sCodispl") & "&Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("Index") & "&nMainAction=" & Request.QueryString.Item("nMainAction") & "'</SCRIPT>")
				End Select
			End If
		End If
	End If
Else
	'+ Se recarga la página principal de la secuencia
	If insFinish() Then
		With Response
			.Write("<SCRIPT>")
			.Write("var lstrHref = '/VTimeNet/Product/ProductSeq/DP705.aspx?sOnSeq=1&sCodispl=DP705&nMainAction=302&nModulec=" & Session("nModulec") & "&nCover=" & Session("nCover") & "';")
			.Write("top.opener.top.frames['fraFolder'].location.href=lstrHref;")
			.Write("top.close();")
			.Write("</SCRIPT>")
		End With
	End If
End If
mobjRolesSeq = Nothing
mobjValues = Nothing
%>
</FORM>
</BODY>
</HTML>





