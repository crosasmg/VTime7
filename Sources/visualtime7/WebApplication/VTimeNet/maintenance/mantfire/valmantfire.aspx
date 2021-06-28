<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eBranches" %>
<script language="VB" runat="Server">

Dim mstrErrors As String
Dim mstrCodispl As String
Dim mobjValues As eFunctions.Values
Dim mobjMantFire As Object
Dim mstrString As String

'+ Se define la contante para el manejo de errores en caso de advertencias
Dim mstrCommand As String


'% insvalMantFire: Validaciones masivas de la forma	
'--------------------------------------------------------------------------------------------
Function insvalMantFire() As String
	'--------------------------------------------------------------------------------------------
	Dim lobjTab_in_bus As eBranches.Tab_in_bus
	Dim lobjTar_firecat As eBranches.Tar_firecat
	Dim lobjTar_fire_fh As eBranches.Tar_fire_fh
	Dim lobjTar_cover_fh As eBranches.Tar_cover_fh
	Select Case Request.QueryString.Item("sCodispl")
		'+ Ingreso Detalle de Actividad	
		Case "MIN001"
			lobjTab_in_bus = New eBranches.Tab_in_bus
			If Request.QueryString.Item("WindowType") <> "PopUp" Then
				If CDbl(Request.QueryString.Item("nZone")) = 1 Then
					insvalMantFire = lobjTab_in_bus.insValHeaderMIN001("MIN001", mobjValues.StringToType(Request.Form.Item("nActivity"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble))
				End If
			Else
				insvalMantFire = lobjTab_in_bus.insValPopUpMIN001("MIN001", Request.QueryString.Item("Action"), mobjValues.StringToType(Request.Form.Item("nActivity"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("nDetailArt"), eFunctions.Values.eTypeData.etdDouble), Request.Form.Item("sDescript"), mobjValues.StringToType(Request.Form.Item("tcnNoteNum"), eFunctions.Values.eTypeData.etdDouble), Request.Form.Item("sShort_des"), Request.Form.Item("sStatregt"), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("nActivityType"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("nFamily"), eFunctions.Values.eTypeData.etdDouble, True))
			End If
			lobjTab_in_bus = Nothing
			
		Case "MIN003"
			lobjTar_firecat = New eBranches.Tar_firecat
			If CDbl(Request.QueryString.Item("nZone")) = 1 Then
				Session("dEffecdate") = Request.Form.Item("cbedate")
				insvalMantFire = lobjTar_firecat.insValMIN003_K("MIN003", mobjValues.StringToType(Request.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("cbeDate"), eFunctions.Values.eTypeData.etdDate))
			Else
				With Request
					If .QueryString.Item("WindowType") = "PopUp" Then
						insvalMantFire = lobjTar_firecat.insValMIN003Upd("MIN003", mobjValues.StringToType(.Form.Item("cbeActivityCat"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeConstCat"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("ntctRateBuild"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("ntctRateCont"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("ntctRateRC"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToDate(.Form.Item("dtctNullDate")), mobjValues.StringToDate(Session("dEffecdate")), Request.QueryString.Item("Action"))
					Else
						insvalMantFire = vbNullString
					End If
				End With
			End If
			lobjTar_firecat = Nothing
			
		Case "MIN651"
			lobjTar_fire_fh = New eBranches.Tar_fire_fh
			If CDbl(Request.QueryString.Item("nZone")) = 1 Then
				insvalMantFire = lobjTar_fire_fh.insvalMIN651_K("MIN651", mobjValues.StringToType(Request.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("valCover"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("valModulec"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("valCurrency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate))
				
			Else
				With Request
					If .QueryString.Item("WindowType") = "PopUp" Then
						
						insvalMantFire = lobjTar_fire_fh.insValMIN651Upd("MIN651", Request.QueryString.Item("Action"), mobjValues.StringToType(.Form.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("nModulec"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("nCover"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("nCurrency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("cbeConstcat"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCap_initial"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCap_end"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnRate"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnPremium"), eFunctions.Values.eTypeData.etdDouble))
					Else
						insvalMantFire = vbNullString
					End If
				End With
			End If
			lobjTar_fire_fh = Nothing
			
		Case "MIN652"
			lobjTar_cover_fh = New eBranches.Tar_cover_fh
			If CDbl(Request.QueryString.Item("nZone")) = 1 Then
				insvalMantFire = lobjTar_cover_fh.insvalMIN652_K("MIN652", mobjValues.StringToType(Request.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("valCover"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("valModulec"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("valCurrency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate))
				
			Else
				With Request
					If .QueryString.Item("WindowType") = "PopUp" Then
						
						insvalMantFire = lobjTar_cover_fh.insValMIN652("MIN652", Request.QueryString.Item("Action"), mobjValues.StringToType(Request.Form.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("nModulec"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("nCover"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("nCurrency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.Form.Item("cbeConstcat"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("cbeProvince"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("valMunicipality"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnCap_initial"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnCap_end"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("tcnRate"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("tcnPremium"), eFunctions.Values.eTypeData.etdDouble))
					Else
						insvalMantFire = vbNullString
					End If
				End With
			End If
			lobjTar_cover_fh = Nothing
			
		Case Else
			insvalMantFire = "insvalMantFire: Código lógico no encontrado (" & Request.QueryString.Item("sCodispl") & ")"
	End Select
End Function


'% inspostMantFire: Actualizaciones masivas a las tablas
'--------------------------------------------------------------------------------------------
Function inspostMantFire() As Boolean
	'--------------------------------------------------------------------------------------------
	Dim lblnPost As Boolean
	Dim lintFamily As Integer
	Dim lobjMantFire As Object
	
	lblnPost = False
	Dim lobjTab_in_bus As eBranches.Tab_in_bus
	Select Case Request.QueryString.Item("sCodispl")
		'+ Actualizar Detalle de Actividades	
		Case "MIN001"
			With Request
				If Request.QueryString.Item("WindowType") <> "PopUp" Then
					lblnPost = True
				Else
					mstrString = "&nActivity=" & Request.Form.Item("nActivity")
					lintFamily = mobjValues.StringToType(Request.Form.Item("nFamily"), eFunctions.Values.eTypeData.etdDouble)
					If lintFamily = 0 Then
						lintFamily = eRemoteDB.Constants.intNull
					End If
					
					lobjTab_in_bus = New eBranches.Tab_in_bus
					
					If Request.QueryString.Item("Action") = "Update" Or Request.QueryString.Item("Action") = "Add" Then
						lblnPost = lobjTab_in_bus.insPostMIN001("MIN001", Request.QueryString.Item("Action"), mobjValues.StringToType(Request.Form.Item("nActivity"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("nDetailArt"), eFunctions.Values.eTypeData.etdDouble), Request.Form.Item("sDescript"), mobjValues.StringToType(Request.Form.Item("tcnNoteNum"), eFunctions.Values.eTypeData.etdDouble, True), Request.Form.Item("sShort_des"), Request.Form.Item("sStatregt"), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("nActivityType"), eFunctions.Values.eTypeData.etdDouble), lintFamily)
						lobjTab_in_bus = Nothing
					Else
						lblnPost = True
					End If
				End If
			End With
			
		Case "MIN003"
			With Request
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					lblnPost = True
				Else
					If .QueryString.Item("nMainAction") <> CStr(eFunctions.Menues.TypeActions.clngActionQuery) Then
						If .QueryString.Item("WindowType") = "PopUp" Then
							lobjMantFire = New eBranches.Tar_firecat
							lblnPost = lobjMantFire.insPostMIN003(.Form.Item("sAction"), mobjValues.StringToType(.Form.Item("cbeActivityCat"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeConstCat"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToDate(Session("dEffecdate")), mobjValues.StringToType(.Form.Item("ntctRateBuild"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("ntctRateCont"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("ntctRateRC"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
							
							lobjMantFire = Nothing
						Else
							lblnPost = True
						End If
					Else
						lblnPost = True
					End If
				End If
			End With
			
		Case "MIN651"
			With Request
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					mstrString = "&nBranch=" & Request.Form.Item("cbeBranch") & "&nProduct=" & Request.Form.Item("valProduct") & "&nCover=" & Request.Form.Item("valCover") & "&nModulec=" & Request.Form.Item("valModulec") & "&nCurrency=" & Request.Form.Item("valCurrency") & "&dEffecdate=" & Request.Form.Item("tcdeffecdate")
					lblnPost = True
				Else
					mstrString = "&nBranch=" & Request.Form.Item("nBranch") & "&nProduct=" & Request.Form.Item("nProduct") & "&nCover=" & Request.Form.Item("nCover") & "&nModulec=" & Request.Form.Item("nModulec") & "&nCurrency=" & Request.Form.Item("nCurrency") & "&dEffecdate=" & Request.Form.Item("dEffecdate")
					If .QueryString.Item("nMainAction") <> CStr(eFunctions.Menues.TypeActions.clngActionQuery) Then
						If .QueryString.Item("WindowType") = "PopUp" Then
							lobjMantFire = New eBranches.Tar_fire_fh
							
							lblnPost = lobjMantFire.InsPostMIN651(.QueryString("Action"), mobjValues.StringToType(.Form.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("nModulec"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("nCover"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("nCurrency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("cbeConstcat"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCap_initial"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCap_end"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnRate"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPremium"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
							
							lobjMantFire = Nothing
						Else
							lblnPost = True
						End If
					Else
						lblnPost = True
					End If
				End If
			End With
			
		Case "MIN652"
			With Request
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					mstrString = "&nBranch=" & Request.Form.Item("cbeBranch") & "&nProduct=" & Request.Form.Item("valProduct") & "&nCover=" & Request.Form.Item("valCover") & "&nModulec=" & Request.Form.Item("valModulec") & "&nCurrency=" & Request.Form.Item("valCurrency") & "&dEffecdate=" & Request.Form.Item("tcdeffecdate")
					lblnPost = True
				Else
					mstrString = "&nBranch=" & Request.Form.Item("nBranch") & "&nProduct=" & Request.Form.Item("nProduct") & "&nCover=" & Request.Form.Item("nCover") & "&nModulec=" & Request.Form.Item("nModulec") & "&nCurrency=" & Request.Form.Item("nCurrency") & "&dEffecdate=" & Request.Form.Item("dEffecdate")
					If .QueryString.Item("nMainAction") <> CStr(eFunctions.Menues.TypeActions.clngActionQuery) Then
						If .QueryString.Item("WindowType") = "PopUp" Then
							lobjMantFire = New eBranches.Tar_cover_fh
							
							lblnPost = lobjMantFire.InsPostMIN652(.QueryString("Action"), mobjValues.StringToType(.Form.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("nModulec"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("nCover"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("nCurrency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("cbeConstcat"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeProvince"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valMunicipality"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnCap_initial"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCap_end"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnRate"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPremium"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
							lobjMantFire = Nothing
						Else
							lblnPost = True
						End If
					Else
						lblnPost = True
					End If
				End If
			End With
			
	End Select
	
	inspostMantFire = lblnPost
End Function

</script>
<%Response.Expires = -1
%>
<HTML>
<HEAD>
    <LINK REL="StyleSheet" TYPE="text/css" HREF="/VTimeNet/Common/Custom.css">
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>



	
<SCRIPT src="/VTimeNet/scripts/GenFunctions.js"> </SCRIPT>


<SCRIPT>
function CancelErrors(){self.history.go(-1)}
function NewLocation(Source,Codisp){
    var lstrLocation = "";
    lstrLocation += Source.location;
    lstrLocation = lstrLocation.replace(/&OPENER=.*/,"") + "&OPENER=" + Codisp;
    Source.location = lstrLocation
}
</SCRIPT>
</HEAD>

<BODY>

<%mstrCommand = "&sModule=Maintenance&sProject=MantFire&sCodisplReload=" & Request.QueryString.Item("sCodispl")

mobjValues = New eFunctions.Values

'+ Si no se han validado los campos de la página
If Request.Form.Item("sCodisplReload") = vbNullString Then
	mstrErrors = insvalMantFire
	Session("sErrorTable") = mstrErrors
	Session("sForm") = Request.Form.ToString
Else
	Session("sErrorTable") = vbNullString
	Session("sForm") = vbNullString
End If

If mstrErrors > vbNullString Then
	With Response
		.Write("<SCRIPT LANGUAGE=JAVASCRIPT>")
		.Write("ShowPopUp(""/VTimeNet/Common/Errors.aspx?sSource=" & Server.URLEncode(mstrCommand) & "&sQueryString=" & Server.URLEncode(Request.Params.Get("Query_String")) & """, ""MantFireError"",660,330);document.location.href='/VTimeNet/common/blank.htm';")
		.Write(mobjValues.StatusControl(False, CShort(Request.QueryString.Item("nZone")), Request.QueryString.Item("WindowType")))
		.Write("</SCRIPT>")
	End With
Else
	If inspostMantFire Then
		If Request.QueryString.Item("WindowType") <> "PopUp" Then
			If Request.QueryString.Item("nAction") = CStr(eFunctions.Menues.TypeActions.clngAcceptdataFinish) Then
				Select Case Request.QueryString.Item("sCodispl")
					Case "MIN001"
					Case "MIN003"
						mstrCodispl = "MIN003"
						Session("dEffecdate") = eRemoteDB.Constants.dtmnull
					Case Else
						Response.Write("<SCRIPT>insReloadTop(true,false)</SCRIPT>")
				End Select
				Response.Write("<SCRIPT>insReloadTop(true,false)</SCRIPT>")
			Else
				'   If Request.QueryString("sCodisplReload") > "" Then
				'	    Response.Write "<NOTSCRIPT>window.close();opener.top.fraFolder.document.location=""" & Replace(UCase(Request.QueryString("sCodispl")),"_K","") & ".aspx?nMainAction=" & Request.QueryString("nMainAction") & mstrString & """;</SCRIPT>"
				'   Else
				'	    Response.Write "<NOTSCRIPT>top.fraFolder.document.location=""" & Replace(UCase(Request.QueryString("sCodispl")),"_K","") & ".aspx?nMainAction=" & Request.QueryString("nMainAction") & mstrString & """;</SCRIPT>"
				'   End If
				
				If Request.QueryString.Item("nZone") = "1" Then
					If Request.Form.Item("sCodisplReload") = vbNullString Then
						Select Case Request.QueryString.Item("sCodispl")
							Case "MIN001"
								Response.Write("<SCRIPT>;self.history.go(-1);top.fraFolder.document.location=""" & Replace(UCase(Request.QueryString.Item("sCodispl")), "_K", "") & ".aspx?sCodispl=" & Request.QueryString.Item("sCodispl") & "&nMainAction=" & Request.QueryString.Item("nMainAction") & "&nActivity=" & Request.Form.Item("nActivity") & """;</SCRIPT>")
							Case Else
								Response.Write("<SCRIPT>top.fraFolder.document.location=""" & Replace(UCase(Request.QueryString.Item("sCodispl")), "_K", "") & ".aspx?nMainAction=" & Request.QueryString.Item("nMainAction") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & mstrString & """;</SCRIPT>")
						End Select
					Else
						Response.Write("<SCRIPT>window.close();opener.top.fraFolder.document.location=""" & Replace(UCase(Request.QueryString.Item("sCodispl")), "_K", "") & ".aspx?nMainAction=" & Request.QueryString.Item("nMainAction") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & mstrString & """;</SCRIPT>")
					End If
				Else
					Response.Write("<SCRIPT>top.fraHeader.document.location=""" & Replace(UCase(Request.QueryString.Item("sCodispl")), "_K", "") & ".aspx?nMainAction=" & Request.QueryString.Item("nMainAction") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & mstrString & """;</SCRIPT>")
				End If
			End If
		Else
			'+ Se recarga la página que invocó la PopUp
			Select Case Request.QueryString.Item("sCodispl")
				Case "MIN001"
					Response.Write("<SCRIPT>top.opener.document.location.href='" & Request.QueryString.Item("sCodispl") & ".aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&nMainAction=" & Request.QueryString.Item("nMainAction") & mstrString & "'</SCRIPT>")
				Case "MIN003"
					Response.Write("<SCRIPT>top.opener.document.location.href='" & Request.QueryString.Item("sCodispl") & ".aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&nMainAction=304" & "'</SCRIPT>")
				Case "MIN651"
					Response.Write("<SCRIPT>top.opener.document.location.href='" & Request.QueryString.Item("sCodispl") & ".aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&nMainAction=" & Request.QueryString.Item("nMainAction") & mstrString & "'</SCRIPT>")
				Case "MIN652"
					Response.Write("<SCRIPT>top.opener.document.location.href='" & Request.QueryString.Item("sCodispl") & ".aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&nMainAction=" & Request.QueryString.Item("nMainAction") & mstrString & "'</SCRIPT>")
				Case Else
					Response.Write("<SCRIPT>top.opener.document.location.href='" & Request.QueryString.Item("sCodispl") & ".aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&nMainAction=" & Request.QueryString.Item("nMainAction") & "'</SCRIPT>")
			End Select
		End If
	End If
	
	If Request.QueryString.Item("nMainAction") = "401" Then
		Session("bQuery") = True
	Else
		Session("bQuery") = False
	End If
End If

mobjValues = Nothing
mobjMantFire = Nothing
%>
</BODY>
</HTML>





