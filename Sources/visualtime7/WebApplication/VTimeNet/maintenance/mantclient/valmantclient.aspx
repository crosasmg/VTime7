<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eClient" %>
<script language="VB" runat="Server">

Dim mobjMantClient As Object
Dim mstrErrors As String
Dim mobjValues As eFunctions.Values
Dim mstrString As String
Dim mstrCommand As String


'% insValMantClient: Se realizan las validaciones masivas de la forma
'--------------------------------------------------------------------------------------------
Function insValMantClient() As String
	'--------------------------------------------------------------------------------------------
	Select Case Request.QueryString.Item("sCodispl")
		Case "MBC001"
			mobjMantClient = New eClient.Tab_Wincli
			If CDbl(Request.QueryString.Item("nZone")) = 1 Then
				With Request
					insValMantClient = mobjMantClient.insValMBC001_K(.Form.Item("optTypClie"), .Form.Item("optTransa"))
				End With
			Else
				With Request
					insValMantClient = mobjMantClient.insValMBC001(Request.Form.GetValues("Sel").Length)
				End With
			End If
			
			'+MBC003: 
		Case "MBC003"
			mobjMantClient = New eClient.Tab_relat
			If Request.QueryString.Item("WindowType") = "PopUp" Then
				insValMantClient = mobjMantClient.insValMBC003(Request.QueryString.Item("sCodispl"), mobjValues.StringToType(Request.Form.Item("cbeRelaship"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("cbeRel_target"), eFunctions.Values.eTypeData.etdDouble), Request.Form.Item("cbeStatregt"), Request.QueryString.Item("Action"))
			Else
				insValMantClient = vbNullString
			End If
			
			'+MBC667: 
		Case "MBC667"
			mobjMantClient = New eClient.Tab_req_doc
			If Request.QueryString.Item("WindowType") = "PopUp" Then
				insValMantClient = mobjMantClient.insValMBC667(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("Action"), mobjValues.StringToType(Request.Form.Item("cbenTypeDoc"), eFunctions.Values.eTypeData.etdDouble, True), Request.Form.Item("chksRequire"), mobjValues.StringToType(Request.Form.Item("tcnQDays"), eFunctions.Values.eTypeData.etdDouble), Request.Form.Item("cbesStatregt"), mobjValues.StringToType(Request.Form.Item("tcnCost"), eFunctions.Values.eTypeData.etdDouble))
			Else
				insValMantClient = vbNullString
			End If
			
		Case Else
			insValMantClient = "insValMantClient: Código lógico no encontrado (" & Request.QueryString.Item("sCodispl") & ")"
	End Select
End Function

'% insPostMantAgent: Se realizan las actualizaciones a las tablas
'--------------------------------------------------------------------------------------------
Function insPostMantClient() As Boolean
	'--------------------------------------------------------------------------------------------
	Dim lblnPost As Boolean
	Dim lintIndex As Short
	Dim lintCheck As Object
	Dim lstrRequire As Object
	
	lblnPost = False
	With Request
		Select Case Request.QueryString.Item("sCodispl")
			'+MBC001:Sec. de ventanas para el tr. de clientes
			Case "MBC001"
				If .QueryString.Item("nMainAction") <> "401" And CDbl(.QueryString.Item("nZone")) <> 1 Then
					lintIndex = 0
					If Not IsNothing(.Form.Item("hddsSel")) Then
						For	Each lintCheck In .Form.GetValues("hddsSel")
							lintIndex = lintIndex + 1
							If lintCheck <> eRemoteDB.Constants.intNull Or lintCheck <> 0 Then
								lblnPost = mobjMantClient.InsPostMBC001(.Form.GetValues("hddsExist").GetValue(lintIndex - 1), .Form.GetValues("hddsSel").GetValue(lintIndex - 1), .QueryString("sType_clie"), .QueryString("sType_seq"), .Form.GetValues("hddnSequence").GetValue(lintIndex - 1), .Form.GetValues("hddsCodispl").GetValue(lintIndex - 1), "1", .Form.GetValues("hddsRequire").GetValue(lintIndex - 1), Session("nUsercode"))
								
							End If
						Next lintCheck
					End If
				Else
					mstrString = "&sType_clie=" & Request.Form.Item("optTypClie") & "&sType_seq=" & Request.Form.Item("optTransa")
					lblnPost = True
				End If
				
			Case "MBC003"
				If .QueryString.Item("WindowType") = "PopUp" Then
					lblnPost = mobjMantClient.insPostMBC003(.QueryString("Action"), mobjValues.StringToType(.Form.Item("cbeRelaship"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeRel_target"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("cbeStatregt"), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
					
					
				Else
					lblnPost = True
				End If
				
			Case "MBC667"
				
				If .QueryString.Item("WindowType") = "PopUp" Then
					lblnPost = mobjMantClient.insPostMBC667(Request.QueryString.Item("Action"), mobjValues.StringToType(Request.Form.Item("cbenTypeDoc"), eFunctions.Values.eTypeData.etdDouble), Request.Form.Item("chksRequire"), mobjValues.StringToType(Request.Form.Item("tcnQDays"), eFunctions.Values.eTypeData.etdDouble), Request.Form.Item("cbesStatregt"), mobjValues.StringToType(Request.Form.Item("tcnCost"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
					
				Else
					lblnPost = True
				End If
				
		End Select
	End With
	insPostMantClient = lblnPost
End Function

</script>
<%Response.Expires = 0

mobjValues = New eFunctions.Values
mstrCommand = "sModule=Maintenance&sProject=MantClient&sCodisplReload=" & Request.QueryString.Item("sCodispl")
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
//+ Variable para el control de versiones
    document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 15:55 $"
    
//% NewLocation: se recalcula el URL de la página
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
<FORM id=form1 name="valMantClient">
<%
'+ Si no se han validado los campos de la página
If Request.Form.Item("sCodisplReload") = vbNullString Then
	mstrErrors = insValMantClient
	Session("sErrorTable") = mstrErrors
	Session("sForm") = Request.Form.ToString
Else
	Session("sErrorTable") = vbNullString
	Session("sForm") = vbNullString
End If

If mstrErrors > vbNullString Then
	With Response
		.Write("<SCRIPT LANGUAGE=JAVASCRIPT>")
		.Write("ShowPopUp(""/VTimeNet/Common/Errors.aspx?sSource=" & Server.URLEncode(mstrCommand) & "&sQueryString=" & Server.URLEncode(Request.Params.Get("Query_String")) & """, ""MantClientError"",660,330);document.location.href='/VTimeNet/common/blank.htm';")
		.Write(mobjValues.StatusControl(False, CShort(Request.QueryString.Item("nZone")), Request.QueryString.Item("WindowType")))
		.Write("</SCRIPT>")
	End With
Else
	If insPostMantClient Then
		If Request.QueryString.Item("WindowType") <> "PopUp" Then
			If Request.QueryString.Item("nAction") = CStr(eFunctions.Menues.TypeActions.clngAcceptdataFinish) Then
				Response.Write("<SCRIPT>top.document.location.reload();</SCRIPT>")
			Else
				If Request.QueryString.Item("nZone") = "1" Then
					If Request.Form.Item("sCodisplReload") = vbNullString Then
						Response.Write("<SCRIPT>;self.history.go(-1);top.fraFolder.document.location=""" & Replace(UCase(Request.QueryString.Item("sCodispl")), "_K", "") & ".aspx?nMainAction=" & Request.QueryString.Item("nMainAction") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & mstrString & """;</SCRIPT>")
					Else
						Response.Write("<SCRIPT>window.close();opener.top.fraFolder.document.location=""" & Replace(UCase(Request.QueryString.Item("sCodispl")), "_K", "") & ".aspx?nMainAction=" & Request.QueryString.Item("nMainAction") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & mstrString & """;</SCRIPT>")
					End If
				Else
					Response.Write("<SCRIPT>;self.history.go(-1);top.fraHeader.document.location=""" & Replace(UCase(Request.QueryString.Item("sCodispl")), "_K", "") & ".aspx?nMainAction=" & Request.QueryString.Item("nMainAction") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & """;</SCRIPT>")
				End If
			End If
		Else
			'+ Se recarga la página que invocó la PopUp
			Select Case Request.QueryString.Item("sCodispl")
				Case "MBC001"
					Response.Write("<SCRIPT>top.opener.document.location.href='MBC001.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&nMainAction=302'</SCRIPT>")
				Case "MBC003"
					Response.Write("<SCRIPT>top.opener.document.location.href='MBC003_k.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&nMainAction=302'</SCRIPT>")
				Case "MBC667"
					Response.Write("<SCRIPT>top.opener.document.location.href='" & Request.QueryString.Item("sCodispl") & "_k.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&nMainAction=302'</SCRIPT>")
			End Select
		End If
	End If
End If
mobjMantClient = Nothing
mobjValues = Nothing
%>
</FORM>
</BODY>
</HTML>
</BODY>
</HTML>





