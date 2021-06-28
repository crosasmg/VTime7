<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eGeneralForm" %>
<%@ Import namespace="eClient" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="ePolicy" %>
<script language="VB" runat="Server">

'- Variable que contiene el número de nota en tratamiento	
Dim mlngNotenum As Object

'+ Se define la contante para el manejo de errores en caso de advertencias
Dim mstrCommand As String

Dim mstrErrors As String
Dim mobjPhones As eGeneralForm.GeneralForm
Dim mobjAddress As eGeneralForm.GeneralForm


'% insvalGeneralForm: Se realizan las validaciones masivas de la forma
'--------------------------------------------------------------------------------------------
Function insvalGeneralForm() As String
	'--------------------------------------------------------------------------------------------
	Dim lclsNotes As eGeneralForm.GeneralForm
	Select Case Request.QueryString.Item("sCodispl")
		Case "SCA2-9", "SCA2-L", "SCA2-H", "SCA2-A", "SCA2-T"
			'+ Ventana de Notas
			lclsNotes = New eGeneralForm.GeneralForm
			With Request
				insvalGeneralForm = lclsNotes.insValSCA002(Request.QueryString.Item("sCodispl"), "Note", .Form.Item("sDescript"), .Form.Item("dCompdate"), .Form.Item("dNulldate"), .Form.Item("tDs_text"))
			End With
			lclsNotes = Nothing
			
			'+ Ventana de direcciones      
		Case "SCA101", "SCA108"
			If Request.QueryString.Item("WindowType") <> "PopUp" Then
				mobjAddress = New eGeneralForm.GeneralForm
				insvalGeneralForm = insvalGeneralForm & mobjAddress.insValSCA001(Request.QueryString.Item("sCodispl"), "2", Request.Form.Item("txtAddress"), Request.Form.Item("valZipCode"), Request.Form.Item("valLocal"), Request.Form.Item("cbeCountry"), Request.Form.Item("tcnLonCardinG"), Request.Form.Item("tcnLonCardinM"), Request.Form.Item("tcnLonCardinS"), Request.Form.Item("tcnLatCardinG"), Request.Form.Item("tcnLatCardinM"), Request.Form.Item("tcnlatCardinS"), VbNullString, eRemoteDB.Constants.intNull, VbNullString, Date.MinValue, VbNullString, VbNullString, VbNullString, VbNullString, VbNullString, eRemoteDB.Constants.intNull)
				
				mobjAddress = Nothing
			Else
				mobjPhones = New eGeneralForm.GeneralForm
				insvalGeneralForm = mobjPhones.insValPhones("SCA101", Request.QueryString.Item("nRecowner"), Request.QueryString.Item("sKeyAddress"), Request.QueryString.Item("nOrder"), Request.Form.Item("tcnArea"), CStr(Today), Request.Form.Item("tctPhone"), Request.Form.Item("tcnOrder"), Request.Form.Item("tcnExtensi1"), Request.Form.Item("cbePhoneType"), Request.Form.Item("tcnExtensi2"), Request.QueryString.Item("Action"))
				mobjPhones = Nothing
			End If
		Case Else
			insvalGeneralForm = "insvalGeneralForm: Código lógico no encontrado (" & Request.QueryString.Item("sCodispl") & ")"
	End Select
End Function

'% insPostGeneralForm: Se realizan las actualizaciones de las ventanas
'--------------------------------------------------------------------------------------------
Function insPostGeneralForm() As Boolean
	'--------------------------------------------------------------------------------------------
	Dim lblnPost As Boolean
	Dim lobjClient As Object
	Dim lobjValues As eFunctions.Values
	lblnPost = True
	
	Dim lclsPostNotes As eGeneralForm.GeneralForm
	Dim lclsClient As eClient.Client
	Dim lclsValues As eFunctions.Values
	Dim lobjPhone As eGeneralForm.Phone
	Dim lobjAddress As eGeneralForm.Address
	Select Case Request.QueryString.Item("sCodispl")
		Case "SCA2-9", "SCA2-L", "SCA2-H", "SCA2-A", "SCA2-T"
			'+ Ventana de Notas	
			lclsPostNotes = New eGeneralForm.GeneralForm
			With Request
				lblnPost = lclsPostNotes.insPostNotes(.QueryString.Item("Action"), Session("sClient"), .Form.Item("nNotenum"), .Form.Item("nConsec"), .Form.Item("sDescript"), CDate(.Form.Item("dCompdate")), CDate(.Form.Item("dNulldate")), .Form.Item("tDs_text"), .Form.Item("nUsercode"), .Form.Item("nRectype"))
			End With
			mlngNotenum = lclsPostNotes.nNotenum
			lclsPostNotes = Nothing
			
			'+ Colocar sólo los Codispl que sean un frame de la secuencia
			If Request.QueryString.Item("sCodispl") = "SCA2-9" Then
				
				lclsClient = New eClient.Client
				lclsValues = New eFunctions.Values
				
				With lclsClient
					.sClient = Session("sClient")
					.nUsercode = lclsValues.StringToType(Session("nUsercode"), 2)
					lblnPost = .UpdateNoteNum(mlngNotenum)
				End With
				
				lclsClient = Nothing
				lclsValues = Nothing
			End If
			
			'+ Ventana de direcciones
		Case "SCA101", "SCA108"
			If Request.QueryString.Item("WindowType") = "PopUp" Then
				lobjPhone = New eGeneralForm.Phone
				insPostGeneralForm = False
				
				Select Case Request.QueryString.Item("Action")
					Case "Add"
						With lobjPhone
							.nRecowner = CInt(Request.QueryString.Item("nRecowner"))
							.sKeyAddress = Request.QueryString.Item("sKeyAddress")
							.nKeyPhones = CInt(Request.Form.Item("tcnOrder"))
							.nArea_code = CInt(Request.Form.Item("tcnArea"))
							.dEffecdate = Session("SCA101_dEffecDate")
							.sPhone = Request.Form.Item("tctPhone")
							.nOrder = CInt(Request.Form.Item("tcnOrder"))
							If Trim(Request.Form.Item("tcnExtensi1")) <> vbNullString Then
								.nExtens1 = CInt(Request.Form.Item("tcnExtensi1"))
							End If
							.nPhone_type = CInt(Request.Form.Item("cbePhoneType"))
							If Trim(Request.Form.Item("tcnExtensi2")) <> vbNullString Then
								.nExtens2 = CInt(Request.Form.Item("tcnExtensi2"))
							End If
							.nUsercode = Session("nUserCode")
							lblnPost = .Add
						End With
						
					Case "Update"
						With lobjPhone
							.Find(Request.QueryString.Item("sKeyAddress"), CInt(Request.Form.Item("tcnOrder")), CShort(Request.QueryString.Item("nRecowner")), Session("SCA101_dEffecDate"))
							.nArea_code = CInt(Request.Form.Item("tcnArea"))
							.dEffecdate = Session("SCA101_dEffecDate")
							.sPhone = Request.Form.Item("tctPhone")
							.nOrder = CInt(Request.Form.Item("tcnOrder"))
							If Trim(Request.Form.Item("tcnExtensi1")) <> vbNullString Then
								.nExtens1 = CInt(Request.Form.Item("tcnExtensi1"))
							End If
							.nPhone_type = CInt(Request.Form.Item("cbePhoneType"))
							If Trim(Request.Form.Item("tcnExtensi2")) <> vbNullString Then
								.nExtens2 = CInt(Request.Form.Item("tcnExtensi2"))
							End If
							.nUsercode = Session("nUserCode")
							lblnPost = .Update
						End With
				End Select
				lobjPhone = Nothing
			Else
				lobjAddress = New eGeneralForm.Address
				With lobjAddress
					.dEffecdate = Today
					.nRecowner = CInt(Request.QueryString.Item("nRecowner"))
					.sKeyAddress = Request.QueryString.Item("sKeyAddress")
					.sRecType = Request.QueryString.Item("sRecType")
					.sStreet = Request.Form.Item("txtAddress")
					.sClient = Session("sClient")
					.sE_mail = Request.Form.Item("tctE_mail")
					.nLat_grade = CInt(Request.Form.Item("tcnLatCardinG"))
					.nLon_grade = CInt(Request.Form.Item("tcnLonCardinG"))
					.nLat_minute = CInt(Request.Form.Item("tcnLatCardinM"))
					.nLon_minute = CInt(Request.Form.Item("tcnLonCardinM"))
					.nLat_second = CDbl(Request.Form.Item("tcnLatCardinS"))
					.nLon_second = CDbl(Request.Form.Item("tcnLonCardinS"))
					.nCountry = CInt(Request.Form.Item("cbeCountry"))
					.nLocal = CInt(Request.Form.Item("ValLocal"))
					.nZip_code = CDbl(Request.Form.Item("valZipCode"))
					.nProvince = CInt(Request.Form.Item("tcnProvince"))
					.nUsercode = Session("nUsercode")
					If Request.QueryString.Item("sCodispl") = "SCA108" Then
						lobjValues = New eFunctions.Values
						.nBranch = lobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble)
						.nProduct = lobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble)
						.sCertype = Session("sCertype")
						.nPolicy = lobjValues.StringToType(Session("npolicy"), eFunctions.Values.eTypeData.etdDouble)
						.nCertif = lobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble)
					End If
					.Update()
				End With
				lobjAddress = Nothing
			End If
	End Select
	
	If lblnPost And Request.QueryString.Item("nZone") = "2" Then
		Select Case Request.QueryString.Item("sCodispl")
			Case "SCA2-9", "SCA101", "SCA10-2"
				'+ Se actualiza Client_Win
				lobjClient = New eClient.ClientWin
				lobjClient.insUpdClient_win(Session("sClient"), CStr(Request.QueryString.Item("sCodispl")), "2")
				lobjClient = Nothing
			Case "SCA108"
				lobjClient = New ePolicy.Policy_Win
				With lobjValues
					Call lobjClient.Add_PolicyWin(Session("sCertype"), .StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), .StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), .StringToType(Session("npolicy"), eFunctions.Values.eTypeData.etdDouble), .StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), .StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), .StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), Request.QueryString.Item("sCodispl"), "2")
				End With
				lobjValues = Nothing
				lobjClient = Nothing
		End Select
	End If
	insPostGeneralForm = lblnPost
End Function

'% insGetSource: se arma la dirección general en caso de advertencias
'--------------------------------------------------------------------------------------------
Private Sub insGetSource()
	'--------------------------------------------------------------------------------------------
	Dim lstrModule As String
	Dim lstrProject As String
	Select Case Request.QueryString.Item("sCodispl")
		Case "SCA101", "SCA2-A", "SCA2-9"
			lstrModule = "Client"
			lstrProject = "ClientSeq"
		Case "SCA108", "SCA2-L", "SCA2-H"
			lstrModule = "Policy"
			lstrProject = "PolicySeq"
	End Select
	mstrCommand = "&sModule=" & lstrModule & "&sProject=" & lstrProject & "&sCodisplReload=" & Request.QueryString.Item("sCodispl")
End Sub

</script>
<%Response.Expires = -1
Response.CacheControl = "private"
%>



	

<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<%Response.Write("<SCRIPT>")%>
//% CancelErrors: regresa a la ventana que invocó los errores
//-------------------------------------------------------------------------------------------
function CancelErrors(){self.history.go(-1)}
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
<%
Call insGetSource()

'+ Si no se han validado los campos de la página
If Request.Form.Item("sCodisplReload") = vbNullString Then
	mstrErrors = insvalGeneralForm
End If

If mstrErrors > vbNullString Then
	With Response
		.Write("<SCRIPT LANGUAGE=JAVASCRIPT>")
		.Write("ShowPopUp(""/VTimeNet/Common/Errors.aspx?sCommand=" & server.URLEncode(mstrErrors) & "&sForm=" & server.URLEncode(Request.Form.ToString) & server.URLEncode(mstrCommand) & "&sQueryString=" & server.URLEncode(Request.Params.Get("Query_String")) & """);")
		.Write("self.history.go(-1)")
		.Write("</SCRIPT>")
	End With
Else
	If insPostGeneralForm Then
		If Request.QueryString.Item("sOnSeq") = "1" Then
			If Request.QueryString.Item("WindowType") <> "PopUp" Then
				Response.Write("<SCRIPT>top.frames['fraSequence'].document.location='Sequence.aspx?nAction=" & Request.QueryString.Item("nMainAction") & "&sGoToNext=Yes&nOpener=" & Request.QueryString.Item("sCodispl") & "';</SCRIPT>")
			Else
				Response.Write("<SCRIPT>opener.top.frames['fraSequence'].document.location='/VTimeNet/Client/ClientSeq/Sequence.aspx?nAction=" & Request.QueryString.Item("nMainAction") & "&nOpener=" & Request.QueryString.Item("sCodispl") & "&sGoToNext=NO" & "';</SCRIPT>")
				Select Case Request.QueryString.Item("sCodispl")
					Case "SCA2-9", "SCA2-L", "SCA2-H", "SCA2-A", "SCA2-T"
						Response.Write("<SCRIPT>opener.document.location.href='SCA002.aspx?sCodispl=" & Request.Form.Item("sCodispl") & "&Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=0" & Request.QueryString.Item("ReloadIndex") & "&nNotenum=0" & mlngNotenum & "&WindowType=PopUp" & "&nMainAction=" & Request.QueryString.Item("nMainAction") & "&sOnSeq=" & Request.QueryString.Item("sOnSeq") & "&nIndexNotenum=" & Request.QueryString.Item("nIndexNotenum") & "'</SCRIPT>")
					Case "SCA101"
						Response.Write("<SCRIPT>opener.document.location.href='SCA001.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=0" & Request.QueryString.Item("ReloadIndex") & "&sCodispl=SCA101" & "&nMainAction=" & Request.QueryString.Item("nMainAction") & "&sOnSeq=" & Request.QueryString.Item("sOnSeq") & "&sRecType=" & Request.QueryString.Item("sRecType") & "'</SCRIPT>")
				End Select
			End If
		Else
			
			'+ Se recarga la página que invocó la PopUp
			
			Select Case Request.QueryString.Item("sCodispl")
				Case "SCA2-9", "SCA2-L", "SCA2-H", "SCA2-A", "SCA2-T"
					Response.Write("<SCRIPT>opener.document.location.href='SCA002.aspx?sCodispl=" & Request.Form.Item("sCodispl") & "&Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=0" & Request.QueryString.Item("ReloadIndex") & "&nNotenum=0" & mlngNotenum & "&WindowType=PopUp" & "&nMainAction=" & Request.QueryString.Item("nMainAction") & "&sOnSeq=" & Request.QueryString.Item("sOnSeq") & "&nIndexNotenum=" & Request.QueryString.Item("nIndexNotenum") & "'</SCRIPT>")
				Case "SCA101"
					Response.Write("<SCRIPT>opener.document.location.href='SCA001.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=0" & Request.QueryString.Item("ReloadIndex") & "&sCodispl=SCA101" & "&nMainAction=" & Request.QueryString.Item("nMainAction") & "&sOnSeq= " & Request.QueryString.Item("sOnSeq") & "'</SCRIPT>")
			End Select
		End If
	End If
End If
%>





